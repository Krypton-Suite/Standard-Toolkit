#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Full-screen colour-keyed overlay that live-samples the desktop and shows a Classic or Krypton magnifier flyout.
/// </summary>
internal sealed class VisualScreenColorPickerOverlay : Form
{
    private const int FooterHeight = 52;
    private const int BannerHeight = 32;
    private const int DirtyPadding = 8;
    private const int RefreshIntervalMs = 16;
    private const int VkSnapshot = 0x2C;
    private const int WmKeyDown = 0x0100;
    private const int WmSysKeyDown = 0x0104;
    private const string InstructionText =
        @"Click to pick  ·  Esc or right-click to cancel  ·  Wheel zooms  ·  Ctrl+wheel resizes  ·  F12 copies screenshot";

    private static readonly Color TransparentKey = Color.Magenta;

    private readonly Bitmap _sample;
    private readonly Rectangle _virtualScreen;
    private readonly KryptonScreenColorPickerFlyoutStyle _flyoutStyle;
    private readonly VisualScreenColorPickerKryptonFlyoutForm? _kryptonFlyoutForm;
    private readonly KryptonPanel? _kryptonBanner;
    private readonly System.Windows.Forms.Timer _refreshTimer;
    private int _zoom;
    private int _gridSize;
    private Color _hoverColor = Color.Black;
    private Rectangle _lastMagnifierBounds;

    internal VisualScreenColorPickerOverlay(
        KryptonScreenColorPickerFlyoutStyle flyoutStyle,
        KryptonCustomPaletteBase? palette,
        int magnifierSize,
        int zoom)
    {
        _sample = new Bitmap(KryptonScreenColorPicker.MaximumMagnifierSize,
            KryptonScreenColorPicker.MaximumMagnifierSize, PixelFormat.Format32bppArgb);
        _virtualScreen = SystemInformation.VirtualScreen;
        _flyoutStyle = flyoutStyle;
        _gridSize = KryptonScreenColorPicker.ClampMagnifierSize(magnifierSize);
        _zoom = KryptonScreenColorPicker.ClampZoom(zoom);

        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.Manual;
        Bounds = _virtualScreen;
        TopMost = true;
        ShowInTaskbar = false;
        KeyPreview = true;
        DoubleBuffered = true;
        AllowTransparency = true;
        TransparencyKey = TransparentKey;
        BackColor = TransparentKey;
        Cursor = Cursors.Cross;
        Text = @"Screen colour picker";

        SetStyle(ControlStyles.AllPaintingInWmPaint
                 | ControlStyles.UserPaint
                 | ControlStyles.OptimizedDoubleBuffer, true);

        _refreshTimer = new System.Windows.Forms.Timer
        {
            Interval = RefreshIntervalMs
        };
        _refreshTimer.Tick += RefreshTimer_Tick;

        if (_flyoutStyle == KryptonScreenColorPickerFlyoutStyle.Krypton)
        {
            _kryptonBanner = CreateKryptonBanner(palette);
            Controls.Add(_kryptonBanner);

            // Host the flyout in its own TopMost form to avoid erase artifacts on the transparent overlay.
            _kryptonFlyoutForm = new VisualScreenColorPickerKryptonFlyoutForm(palette);
        }
    }

    internal Color SelectedColor { get; private set; } = Color.Empty;

    internal int MagnifierSize => _gridSize;

    internal int Zoom => _zoom;

    private Point SampleCenter => new Point(_gridSize / 2, _gridSize / 2);

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams createParams = base.CreateParams;
            createParams.ExStyle |= 0x00000008; // WS_EX_TOPMOST
            return createParams;
        }
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        Capture = true;
        UpdateSampleFromCursor();
        if (_flyoutStyle == KryptonScreenColorPickerFlyoutStyle.Krypton)
        {
            PositionKryptonFlyout();
            _kryptonFlyoutForm?.Show(this);
        }
        else
        {
            _lastMagnifierBounds = GetClassicMagnifierBounds();
            InvalidateMagnifier(Rectangle.Empty, _lastMagnifierBounds);
        }

        _refreshTimer.Start();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        UpdateSampleFromCursor();
        if (_flyoutStyle == KryptonScreenColorPickerFlyoutStyle.Krypton)
        {
            PositionKryptonFlyout();
        }
        else
        {
            InvalidateMagnifier(_lastMagnifierBounds, GetClassicMagnifierBounds());
        }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if (e.Button == MouseButtons.Left)
        {
            Confirm();
        }
        else if (e.Button == MouseButtons.Right)
        {
            Cancel();
        }
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
        base.OnMouseWheel(e);
        int delta = e.Delta > 0 ? 2 : -2;
        bool resize = (ModifierKeys & Keys.Control) == Keys.Control;
        if (resize)
        {
            int nextSize = KryptonScreenColorPicker.ClampMagnifierSize(_gridSize + delta);
            if (nextSize == _gridSize)
            {
                return;
            }

            ApplyMagnifierChange(() => _gridSize = nextSize);
        }
        else
        {
            int nextZoom = KryptonScreenColorPicker.ClampZoom(_zoom + delta);
            if (nextZoom == _zoom)
            {
                return;
            }

            ApplyMagnifierChange(() => _zoom = nextZoom);
        }
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        Keys key = keyData & Keys.KeyCode;
        if (key == Keys.F12 || key == Keys.PrintScreen || key == Keys.Snapshot)
        {
            CopyVisibleOverlayToClipboard();
            return true;
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }

    protected override void WndProc(ref Message m)
    {
        if ((m.Msg == WmKeyDown || m.Msg == WmSysKeyDown) && (int)m.WParam == VkSnapshot)
        {
            CopyVisibleOverlayToClipboard();
            return;
        }

        base.WndProc(ref m);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        if (e.KeyCode == Keys.Escape)
        {
            Cancel();
            e.Handled = true;
        }
        else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
        {
            Confirm();
            e.Handled = true;
        }
        else if (e.KeyCode == Keys.F12 || e.KeyCode == Keys.PrintScreen || e.KeyCode == Keys.Snapshot)
        {
            CopyVisibleOverlayToClipboard();
            e.Handled = true;
        }
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        using (var brush = new SolidBrush(TransparentKey))
        {
            e.Graphics.FillRectangle(brush, e.ClipRectangle);
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        if (_flyoutStyle == KryptonScreenColorPickerFlyoutStyle.Classic)
        {
            DrawClassicBanner(e.Graphics);
            DrawClassicMagnifier(e.Graphics);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _refreshTimer.Stop();
            _refreshTimer.Dispose();
            _kryptonFlyoutForm?.Close();
            _kryptonFlyoutForm?.Dispose();
            _sample.Dispose();
        }

        base.Dispose(disposing);
    }

    private void Confirm()
    {
        SelectedColor = _hoverColor;
        DialogResult = DialogResult.OK;
        Close();
    }

    private void Cancel()
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void CopyVisibleOverlayToClipboard()
    {
        _refreshTimer.Stop();
        try
        {
            Update();
            Rectangle bounds = RectangleToScreen(ClientRectangle);
            if (bounds.Width <= 0 || bounds.Height <= 0)
            {
                return;
            }

            using (var bitmap = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(bounds.Left, bounds.Top, 0, 0, bounds.Size);
                }

                Clipboard.SetImage(bitmap);
            }
        }
        catch (ExternalException)
        {
            // Another process may own the clipboard.
        }
        finally
        {
            if (!IsDisposed)
            {
                Capture = true;
                _refreshTimer.Start();
            }
        }
    }

    private void RefreshTimer_Tick(object? sender, EventArgs e)
    {
        UpdateSampleFromCursor();
        if (_flyoutStyle == KryptonScreenColorPickerFlyoutStyle.Krypton)
        {
            PositionKryptonFlyout();
        }
        else
        {
            InvalidateMagnifier(_lastMagnifierBounds, GetClassicMagnifierBounds());
        }
    }

    private void InvalidateMagnifier(Rectangle previous, Rectangle current)
    {
        Rectangle dirty = previous.IsEmpty ? current : Rectangle.Union(previous, current);
        dirty.Inflate(DirtyPadding, DirtyPadding);
        Invalidate(dirty);
        _lastMagnifierBounds = current;
    }

    private void UpdateSampleFromCursor()
    {
        int size = _gridSize;
        int half = size / 2;
        Point cursor = Cursor.Position;
        var requested = new Rectangle(cursor.X - half, cursor.Y - half, size, size);
        Rectangle visible = Rectangle.Intersect(requested, _virtualScreen);

        using (Graphics graphics = Graphics.FromImage(_sample))
        {
            graphics.Clear(Color.Black);
            if (visible.Width > 0 && visible.Height > 0)
            {
                try
                {
                    graphics.CopyFromScreen(visible.Location,
                        new Point(visible.X - requested.X, visible.Y - requested.Y),
                        visible.Size);
                }
                catch (Exception)
                {
                    // Protected content or capture policy; neighbourhood stays black.
                }
            }
        }

        _hoverColor = _sample.GetPixel(half, half);
    }

    private void PositionKryptonFlyout()
    {
        if (_kryptonFlyoutForm is null)
        {
            return;
        }

        _kryptonFlyoutForm.UpdateSample(_sample, SampleCenter, _hoverColor, _gridSize, _zoom);
        Point cursor = Cursor.Position; // screen coordinates
        Size size = _kryptonFlyoutForm.FlyoutSize;

        // Place the flyout so the cursor sits just outside its top-left corner.
        int left = cursor.X + 4;
        int top = cursor.Y + 4;

        // Clamp to virtual screen so the flyout stays visible.
        left = Math.Max(_virtualScreen.Left + 8,
            Math.Min(left, _virtualScreen.Right - size.Width - 8));
        top = Math.Max(_virtualScreen.Top + BannerHeight + 8,
            Math.Min(top, _virtualScreen.Bottom - size.Height - 8));

        var nextLocation = new Point(left, top);
        if (_kryptonFlyoutForm.Location != nextLocation)
        {
            _kryptonFlyoutForm.Location = nextLocation;
        }
    }

    private void ApplyMagnifierChange(Action apply)
    {
        if (_flyoutStyle == KryptonScreenColorPickerFlyoutStyle.Krypton)
        {
            apply();
            UpdateSampleFromCursor();
            PositionKryptonFlyout();
        }
        else
        {
            Rectangle before = GetClassicMagnifierBounds();
            apply();
            UpdateSampleFromCursor();
            InvalidateMagnifier(before, GetClassicMagnifierBounds());
        }
    }

    private Rectangle GetClassicMagnifierBounds()
    {
        int mag = _gridSize * _zoom;
        int width = mag + 16;
        int height = mag + FooterHeight + 16;
        Point cursor = PointToClient(Cursor.Position);
        int half = _gridSize / 2;
        int halfZoom = half * _zoom;

        // Place the flyout so the cursor sits just outside its top-left corner.
        int left = cursor.X + 4;
        int top = cursor.Y + 4;

        int minLeft = 8;
        int maxLeft = ClientSize.Width - width - 8;
        if (maxLeft < minLeft)
        {
            left = minLeft;
        }
        else
        {
            left = Math.Max(minLeft, Math.Min(left, maxLeft));
        }

        // Banner is drawn from y=0..BannerHeight. Keep the magnifier below it.
        int minTop = BannerHeight;
        int maxTop = ClientSize.Height - height - 8;
        if (maxTop < minTop)
        {
            top = minTop;
        }
        else
        {
            top = Math.Max(minTop, Math.Min(top, maxTop));
        }
        return new Rectangle(left, top, width, height);
    }

    private void DrawClassicBanner(Graphics graphics)
    {
        var banner = new Rectangle(0, 0, ClientSize.Width, BannerHeight);
        using (var fill = new SolidBrush(Color.FromArgb(200, 20, 20, 20)))
        {
            graphics.FillRectangle(fill, banner);
        }

        string text = InstructionText;
        using (var font = new Font("Segoe UI", 9.75f, FontStyle.Regular))
        using (var brush = new SolidBrush(Color.White))
        {
            SizeF size = graphics.MeasureString(text, font);
            graphics.DrawString(text, font, brush, (ClientSize.Width - size.Width) / 2f, (BannerHeight - size.Height) / 2f);
        }
    }

    private void DrawClassicMagnifier(Graphics graphics)
    {
        Rectangle bounds = GetClassicMagnifierBounds();
        _lastMagnifierBounds = bounds;
        int mag = _gridSize * _zoom;
        var imageRect = new Rectangle(bounds.X + 8, bounds.Y + 8, mag, mag);
        var footerRect = new Rectangle(bounds.X + 8, imageRect.Bottom + 4, mag, FooterHeight - 12);

        using (var path = CreateRoundRect(bounds, 8))
        using (var fill = new SolidBrush(Color.FromArgb(230, 24, 24, 24)))
        using (var border = new Pen(Color.FromArgb(255, 80, 80, 80)))
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.FillPath(fill, path);
            graphics.DrawPath(border, path);
            graphics.SmoothingMode = SmoothingMode.None;
        }

        ScreenColorPickerMagnifierPainter.Draw(graphics, _sample, SampleCenter, imageRect, _gridSize, _zoom);

        var swatch = new Rectangle(footerRect.X, footerRect.Y, 36, footerRect.Height);
        using (var swatchBrush = new SolidBrush(_hoverColor))
        using (var swatchPen = new Pen(Color.White))
        {
            graphics.FillRectangle(swatchBrush, swatch);
            graphics.DrawRectangle(swatchPen, swatch);
        }

        string hex = ScreenColorPickerMagnifierPainter.FormatRgbHex(_hoverColor);
        string rgb = string.Format(CultureInfo.InvariantCulture, @"RGB({0}, {1}, {2})  ·  {3}x  ·  {4} src px",
            _hoverColor.R, _hoverColor.G, _hoverColor.B, _zoom, _gridSize);

        using (var font = new Font("Segoe UI", 9f, FontStyle.Bold))
        using (var small = new Font("Segoe UI", 8.25f, FontStyle.Regular))
        using (var brush = new SolidBrush(Color.White))
        {
            SizeF hexSize = graphics.MeasureString(hex, font);
            SizeF rgbSize = graphics.MeasureString(rgb, small);

            float textX = swatch.Right + 8;
            float minX = footerRect.X + 2;
            float maxX = footerRect.Right - Math.Max(hexSize.Width, rgbSize.Width) - 2;
            textX = Math.Max(minX, Math.Min(textX, maxX));

            float y1 = footerRect.Y + 6;
            float y2 = footerRect.Bottom - small.GetHeight() - 6;

            graphics.DrawString(hex, font, brush, textX, y1);
            graphics.DrawString(rgb, small, brush, textX, y2);
        }
    }

    private static KryptonPanel CreateKryptonBanner(KryptonCustomPaletteBase? palette)
    {
        var panel = new KryptonPanel
        {
            Dock = DockStyle.Top,
            Height = BannerHeight,
            TabStop = false
        };
        panel.Palette = palette;

        var label = new KryptonLabel
        {
            Dock = DockStyle.Fill,
            TabStop = false
        };
        label.LocalCustomPalette = palette;
        label.LabelStyle = LabelStyle.NormalControl;
        label.Values.Text = InstructionText;
        panel.Controls.Add(label);
        return panel;
    }

    private static GraphicsPath CreateRoundRect(Rectangle bounds, int radius)
    {
        int d = radius * 2;
        var path = new GraphicsPath();
        path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
        path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
        path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
        path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
        path.CloseFigure();
        return path;
    }
}
