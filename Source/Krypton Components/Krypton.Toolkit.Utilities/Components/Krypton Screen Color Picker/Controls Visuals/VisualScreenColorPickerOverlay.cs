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
/// Full-screen overlay that shows a frozen desktop snapshot and a PowerToys-style magnifier.
/// </summary>
internal sealed class VisualScreenColorPickerOverlay : Form
{
    private const int SourceOdd = 11;
    private const int MinZoom = 6;
    private const int MaxZoom = 24;
    private const int FooterHeight = 52;
    private const int BannerHeight = 32;
    private const int DirtyPadding = 8;

    private readonly Bitmap _screenshot;
    private readonly Rectangle _virtualScreen;
    private int _zoom = 12;
    private Point _samplePoint;
    private Color _hoverColor = Color.Black;
    private Rectangle _lastMagnifierBounds;

    internal VisualScreenColorPickerOverlay(Bitmap screenshot)
    {
        ThrowHelper.ThrowIfNull(screenshot);

        _screenshot = screenshot;
        _virtualScreen = SystemInformation.VirtualScreen;

        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.Manual;
        Bounds = _virtualScreen;
        TopMost = true;
        ShowInTaskbar = false;
        KeyPreview = true;
        DoubleBuffered = true;
        Cursor = Cursors.Cross;
        BackColor = Color.Black;
        Text = @"Screen colour picker";

        SetStyle(ControlStyles.AllPaintingInWmPaint
                 | ControlStyles.UserPaint
                 | ControlStyles.OptimizedDoubleBuffer
                 | ControlStyles.Opaque, true);
    }

    internal Color SelectedColor { get; private set; } = Color.Empty;

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
        _lastMagnifierBounds = GetMagnifierBounds();
        Invalidate();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        UpdateSampleFromCursor();
        InvalidateMagnifier(_lastMagnifierBounds, GetMagnifierBounds());
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
        int next = Math.Max(MinZoom, Math.Min(MaxZoom, _zoom + delta));
        if (next == _zoom)
        {
            return;
        }

        Rectangle before = GetMagnifierBounds();
        _zoom = next;
        InvalidateMagnifier(before, GetMagnifierBounds());
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
    }

    protected override void OnPaintBackground(PaintEventArgs e) => RestoreScreenshot(e.Graphics, e.ClipRectangle);

    protected override void OnPaint(PaintEventArgs e)
    {
        RestoreScreenshot(e.Graphics, e.ClipRectangle);
        DrawBanner(e.Graphics);
        DrawMagnifier(e.Graphics);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _screenshot.Dispose();
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

    private void InvalidateMagnifier(Rectangle previous, Rectangle current)
    {
        Rectangle dirty = previous.IsEmpty ? current : Rectangle.Union(previous, current);
        dirty.Inflate(DirtyPadding, DirtyPadding);
        Invalidate(dirty);
        _lastMagnifierBounds = current;
    }

    private void RestoreScreenshot(Graphics graphics, Rectangle clip)
    {
        if (clip.Width <= 0 || clip.Height <= 0)
        {
            return;
        }

        var source = Rectangle.Intersect(clip, new Rectangle(0, 0, _screenshot.Width, _screenshot.Height));
        if (source.Width <= 0 || source.Height <= 0)
        {
            return;
        }

        graphics.CompositingMode = CompositingMode.SourceCopy;
        graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
        graphics.PixelOffsetMode = PixelOffsetMode.None;
        graphics.DrawImage(_screenshot, source, source, GraphicsUnit.Pixel);
        graphics.CompositingMode = CompositingMode.SourceOver;
    }

    private void UpdateSampleFromCursor()
    {
        Point screen = Cursor.Position;
        int x = screen.X - _virtualScreen.Left;
        int y = screen.Y - _virtualScreen.Top;
        x = Math.Max(0, Math.Min(_screenshot.Width - 1, x));
        y = Math.Max(0, Math.Min(_screenshot.Height - 1, y));
        _samplePoint = new Point(x, y);
        _hoverColor = _screenshot.GetPixel(x, y);
    }

    private Rectangle GetMagnifierBounds()
    {
        int mag = SourceOdd * _zoom;
        int width = mag + 16;
        int height = mag + FooterHeight + 16;
        Point cursor = PointToClient(Cursor.Position);
        int left = cursor.X + 28;
        int top = cursor.Y + 28;

        if (left + width > ClientSize.Width)
        {
            left = cursor.X - width - 28;
        }

        if (top + height > ClientSize.Height)
        {
            top = cursor.Y - height - 28;
        }

        left = Math.Max(8, left);
        top = Math.Max(BannerHeight + 8, top);
        return new Rectangle(left, top, width, height);
    }

    private void DrawBanner(Graphics graphics)
    {
        var banner = new Rectangle(0, 0, ClientSize.Width, BannerHeight);
        using (var fill = new SolidBrush(Color.FromArgb(200, 20, 20, 20)))
        {
            graphics.FillRectangle(fill, banner);
        }

        string text = @"Click to pick  ·  Esc or right-click to cancel  ·  Mouse wheel zooms";
        using (var font = new Font("Segoe UI", 9.75f, FontStyle.Regular))
        using (var brush = new SolidBrush(Color.White))
        {
            SizeF size = graphics.MeasureString(text, font);
            graphics.DrawString(text, font, brush, (ClientSize.Width - size.Width) / 2f, (BannerHeight - size.Height) / 2f);
        }
    }

    private void DrawMagnifier(Graphics graphics)
    {
        Rectangle bounds = GetMagnifierBounds();
        _lastMagnifierBounds = bounds;
        int mag = SourceOdd * _zoom;
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

        int half = SourceOdd / 2;
        var source = new Rectangle(_samplePoint.X - half, _samplePoint.Y - half, SourceOdd, SourceOdd);
        graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
        graphics.PixelOffsetMode = PixelOffsetMode.Half;
        graphics.DrawImage(_screenshot, imageRect, source, GraphicsUnit.Pixel);

        if (_zoom >= 8)
        {
            using (var grid = new Pen(Color.FromArgb(60, 255, 255, 255)))
            {
                for (int i = 1; i < SourceOdd; i++)
                {
                    int x = imageRect.X + (i * _zoom);
                    int y = imageRect.Y + (i * _zoom);
                    graphics.DrawLine(grid, x, imageRect.Top, x, imageRect.Bottom);
                    graphics.DrawLine(grid, imageRect.Left, y, imageRect.Right, y);
                }
            }
        }

        var center = new Rectangle(imageRect.X + (half * _zoom), imageRect.Y + (half * _zoom), _zoom, _zoom);
        using (var centerPen = new Pen(Color.White, 2f))
        {
            graphics.DrawRectangle(centerPen, center);
        }

        using (var blackPen = new Pen(Color.Black))
        {
            graphics.DrawRectangle(blackPen, Rectangle.Inflate(center, 1, 1));
        }

        var swatch = new Rectangle(footerRect.X, footerRect.Y, 36, footerRect.Height);
        using (var swatchBrush = new SolidBrush(_hoverColor))
        using (var swatchPen = new Pen(Color.White))
        {
            graphics.FillRectangle(swatchBrush, swatch);
            graphics.DrawRectangle(swatchPen, swatch);
        }

        string hex = FormatRgbHex(_hoverColor);
        string rgb = string.Format(CultureInfo.InvariantCulture, @"RGB({0}, {1}, {2})  ·  {3}x",
            _hoverColor.R, _hoverColor.G, _hoverColor.B, _zoom);

        using (var font = new Font("Segoe UI", 9f, FontStyle.Bold))
        using (var small = new Font("Segoe UI", 8.25f, FontStyle.Regular))
        using (var brush = new SolidBrush(Color.White))
        {
            float textX = swatch.Right + 8;
            graphics.DrawString(hex, font, brush, textX, footerRect.Y);
            graphics.DrawString(rgb, small, brush, textX, footerRect.Y + 20);
        }
    }

    private static string FormatRgbHex(Color color) =>
        string.Format(CultureInfo.InvariantCulture, "#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);

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
