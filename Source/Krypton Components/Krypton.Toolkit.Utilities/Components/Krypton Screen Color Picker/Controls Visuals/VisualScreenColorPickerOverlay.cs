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
    private const int BannerHeight = 32;
    private const int RefreshIntervalMs = 16;
    private const int VkSnapshot = 0x2C;
    private const int WmKeyDown = 0x0100;
    private const int WmSysKeyDown = 0x0104;
    private const int MagnifierStep = 2;

    private static readonly Color TransparentKey = Color.Magenta;

    private readonly Bitmap _sample;
    private readonly Rectangle _virtualScreen;
    private readonly KryptonScreenColorPickerFlyoutStyle _flyoutStyle;
    private readonly KryptonScreenColorPickerColorFormat _visibleFormats;
    private readonly VisualScreenColorPickerKryptonFlyoutForm? _kryptonFlyoutForm;
    private readonly VisualScreenColorPickerClassicFlyoutForm? _classicFlyoutForm;
    private readonly KryptonPanel? _kryptonBanner;
    private readonly System.Windows.Forms.Timer _refreshTimer;
    private int _zoom;
    private int _gridSize;
    private Color _hoverColor = Color.Black;
    private Point _lastSampleCursor = new Point(int.MinValue, int.MinValue);

    internal VisualScreenColorPickerOverlay(
        KryptonScreenColorPickerFlyoutStyle flyoutStyle,
        KryptonCustomPaletteBase? palette,
        int magnifierSize,
        int zoom,
        KryptonScreenColorPickerColorFormat visibleFormats)
    {
        _sample = new Bitmap(KryptonScreenColorPicker.MaximumMagnifierSize,
            KryptonScreenColorPicker.MaximumMagnifierSize, PixelFormat.Format32bppArgb);
        _virtualScreen = SystemInformation.VirtualScreen;
        _flyoutStyle = flyoutStyle;
        _visibleFormats = ScreenColorPickerColorFormatter.Normalize(visibleFormats);
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
        Text = KryptonScreenColorPicker.Strings.OverlayTitle;

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
            _kryptonFlyoutForm = new VisualScreenColorPickerKryptonFlyoutForm(palette, _visibleFormats);
        }
        else
        {
            _classicFlyoutForm = new VisualScreenColorPickerClassicFlyoutForm(_visibleFormats);
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
        UpdateSampleFromCursor(forceLiveCapture: true);
        PositionFlyout();
        if (_flyoutStyle == KryptonScreenColorPickerFlyoutStyle.Krypton)
        {
            _kryptonFlyoutForm?.Show(this);
        }
        else
        {
            _classicFlyoutForm?.Show(this);
            Invalidate(new Rectangle(0, 0, Width, BannerHeight));
        }

        EnsureCapture();
        _refreshTimer.Start();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        UpdateSampleFromCursor(forceLiveCapture: false);
        PositionFlyout();
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
        int delta = e.Delta > 0 ? MagnifierStep : -MagnifierStep;
        if ((ModifierKeys & Keys.Control) == Keys.Control)
        {
            ChangeMagnifierSize(delta);
        }
        else
        {
            ChangeZoom(delta);
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

        if (TryHandleMagnifierKeys(keyData))
        {
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
        else if (TryHandleMagnifierKeys(e.KeyData))
        {
            e.Handled = true;
        }
    }

    private bool TryHandleMagnifierKeys(Keys keyData)
    {
        Keys key = keyData & Keys.KeyCode;
        bool control = (keyData & Keys.Control) == Keys.Control;
        switch (key)
        {
            case Keys.Oemplus:
            case Keys.Add:
                if (control)
                {
                    ChangeMagnifierSize(MagnifierStep);
                }
                else
                {
                    ChangeZoom(MagnifierStep);
                }

                return true;
            case Keys.OemMinus:
            case Keys.Subtract:
                if (control)
                {
                    ChangeMagnifierSize(-MagnifierStep);
                }
                else
                {
                    ChangeZoom(-MagnifierStep);
                }

                return true;
            case Keys.OemCloseBrackets:
                ChangeMagnifierSize(MagnifierStep);
                return true;
            case Keys.OemOpenBrackets:
                ChangeMagnifierSize(-MagnifierStep);
                return true;
            case Keys.PageUp:
            case Keys.Up:
                if (control)
                {
                    ChangeMagnifierSize(MagnifierStep);
                }
                else
                {
                    ChangeZoom(MagnifierStep);
                }

                return true;
            case Keys.PageDown:
            case Keys.Down:
                if (control)
                {
                    ChangeMagnifierSize(-MagnifierStep);
                }
                else
                {
                    ChangeZoom(-MagnifierStep);
                }

                return true;
            default:
                return false;
        }
    }

    private void ChangeZoom(int delta)
    {
        int nextZoom = KryptonScreenColorPicker.ClampZoom(_zoom + delta);
        if (nextZoom == _zoom)
        {
            return;
        }

        ApplyMagnifierChange(() => _zoom = nextZoom);
    }

    private void ChangeMagnifierSize(int delta)
    {
        int nextSize = KryptonScreenColorPicker.ClampMagnifierSize(_gridSize + delta);
        if (nextSize == _gridSize)
        {
            return;
        }

        ApplyMagnifierChange(() => _gridSize = nextSize);
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
            _classicFlyoutForm?.Close();
            _classicFlyoutForm?.Dispose();
            _sample.Dispose();
        }

        base.Dispose(disposing);
    }

    protected override void OnMouseCaptureChanged(EventArgs e)
    {
        base.OnMouseCaptureChanged(e);
        if (Visible && !IsDisposed && DialogResult == DialogResult.None)
        {
            EnsureCapture();
        }
    }

    private void EnsureCapture()
    {
        if (!Capture)
        {
            Capture = true;
        }
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
        EnsureCapture();
        UpdateSampleFromCursor(forceLiveCapture: true);
        PositionFlyout();
    }

    private void UpdateSampleFromCursor(bool forceLiveCapture)
    {
        Point cursor = Cursor.Position;
        if (!forceLiveCapture && cursor == _lastSampleCursor)
        {
            return;
        }

        _lastSampleCursor = cursor;
        int size = _gridSize;
        int half = size / 2;
        var requested = new Rectangle(cursor.X - half, cursor.Y - half, size, size);
        Rectangle visible = Rectangle.Intersect(requested, _virtualScreen);

        using (Graphics graphics = Graphics.FromImage(_sample))
        {
            if (visible.Width != size || visible.Height != size)
            {
                graphics.Clear(Color.Black);
            }

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

    private void PositionFlyout()
    {
        if (_kryptonFlyoutForm != null)
        {
            _kryptonFlyoutForm.UpdateSample(_sample, SampleCenter, _hoverColor, _gridSize, _zoom);
            MoveFlyout(_kryptonFlyoutForm, _kryptonFlyoutForm.FlyoutSize);
            return;
        }

        if (_classicFlyoutForm != null)
        {
            _classicFlyoutForm.UpdateSample(_sample, SampleCenter, _hoverColor, _gridSize, _zoom);
            MoveFlyout(_classicFlyoutForm, _classicFlyoutForm.FlyoutSize);
        }
    }

    private void MoveFlyout(Form flyout, Size size)
    {
        Point cursor = Cursor.Position;
        int left = cursor.X + 4;
        int top = cursor.Y + 4;
        left = Math.Max(_virtualScreen.Left + 8,
            Math.Min(left, _virtualScreen.Right - size.Width - 8));
        top = Math.Max(_virtualScreen.Top + BannerHeight + 8,
            Math.Min(top, _virtualScreen.Bottom - size.Height - 8));

        var nextLocation = new Point(left, top);
        if (flyout.Location != nextLocation)
        {
            flyout.Location = nextLocation;
        }
    }

    private void ApplyMagnifierChange(Action apply)
    {
        apply();
        UpdateSampleFromCursor(forceLiveCapture: true);
        PositionFlyout();
    }

    private void DrawClassicBanner(Graphics graphics)
    {
        var banner = new Rectangle(0, 0, ClientSize.Width, BannerHeight);
        using (var fill = new SolidBrush(Color.FromArgb(200, 20, 20, 20)))
        {
            graphics.FillRectangle(fill, banner);
        }

        string text = KryptonScreenColorPicker.Strings.OverlayInstructions;
        using (var font = new Font("Segoe UI", 9.75f, FontStyle.Regular))
        using (var brush = new SolidBrush(Color.White))
        {
            SizeF size = graphics.MeasureString(text, font);
            graphics.DrawString(text, font, brush, (ClientSize.Width - size.Width) / 2f, (BannerHeight - size.Height) / 2f);
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
        label.Values.Text = KryptonScreenColorPicker.Strings.OverlayInstructions;
        panel.Controls.Add(label);
        return panel;
    }
}
