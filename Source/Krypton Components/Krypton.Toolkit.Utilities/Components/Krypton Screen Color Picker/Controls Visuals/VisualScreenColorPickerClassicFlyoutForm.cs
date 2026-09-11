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
/// Opaque owner-drawn Classic (PowerToys) magnifier hosted in its own window so the transparent
/// overlay does not have to erase and repaint a large region on every mouse move.
/// </summary>
internal sealed class VisualScreenColorPickerClassicFlyoutForm : Form
{
    #region Static Fields

    private const int WsExNoActivate = 0x08000000;
    private const int WsExToolWindow = 0x00000080;
    private const int WsExTopMost = 0x00000008;
    private const int WmNcHitTest = 0x0084;
    private const int HtTransparent = -1;
    private const int PaddingSize = 8;
    private const int MinimumPreview = 220;
    private const int LineHeight = 16;
    private const int FooterPadding = 16;
    private const int CornerRadius = 8;

    #endregion

    #region Instance Fields

    private readonly KryptonScreenColorPickerColorFormat _visibleFormats;
    private readonly int _footerHeight;
    private readonly Font _font;
    private readonly Font _bold;
    private readonly SolidBrush _backBrush;
    private readonly SolidBrush _textBrush;
    private readonly Pen _borderPen;
    private readonly Pen _swatchPen;

    private Bitmap? _sample;
    private Point _samplePoint;
    private Color _color = Color.Black;
    private int _gridSize = 11;
    private int _zoom = 12;
    private int _previewSize = MinimumPreview;
    private GraphicsPath? _roundPath;
    private Size _chromeSize;

    #endregion

    internal VisualScreenColorPickerClassicFlyoutForm(KryptonScreenColorPickerColorFormat visibleFormats)
    {
        _visibleFormats = ScreenColorPickerColorFormatter.Normalize(visibleFormats);
        int lines = ScreenColorPickerColorFormatter.CountPanelLines(_visibleFormats, includeKnownName: true);
        _footerHeight = FooterPadding + ((lines + 1) * LineHeight);

        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.Manual;
        ShowInTaskbar = false;
        TopMost = true;
        ControlBox = false;
        MaximizeBox = false;
        MinimizeBox = false;
        DoubleBuffered = true;
        BackColor = Color.FromArgb(36, 36, 36);
        SetStyle(ControlStyles.AllPaintingInWmPaint
                 | ControlStyles.UserPaint
                 | ControlStyles.OptimizedDoubleBuffer
                 | ControlStyles.Opaque, true);
        SetStyle(ControlStyles.Selectable, false);

        _font = new Font("Segoe UI", 8.25f, FontStyle.Regular);
        _bold = new Font("Segoe UI", 9f, FontStyle.Bold);
        _backBrush = new SolidBrush(Color.FromArgb(230, 24, 24, 24));
        _textBrush = new SolidBrush(Color.White);
        _borderPen = new Pen(Color.FromArgb(255, 80, 80, 80));
        _swatchPen = new Pen(Color.White);

        ClientSize = CalculateSize(KryptonScreenColorPicker.DefaultMagnifierSize, KryptonScreenColorPicker.DefaultZoom);
    }

    internal Size FlyoutSize => ClientSize;

    protected override bool ShowWithoutActivation => true;

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= WsExNoActivate | WsExToolWindow | WsExTopMost;
            return cp;
        }
    }

    internal void UpdateSample(Bitmap screenshot, Point samplePoint, Color color, int magnifierSize, int zoom)
    {
        _sample = screenshot;
        _samplePoint = samplePoint;
        _color = color;
        _gridSize = magnifierSize;
        _zoom = zoom;
        _previewSize = CalculatePreviewSize(magnifierSize, zoom);
        Size next = CalculateSize(magnifierSize, zoom);
        if (ClientSize != next)
        {
            ClientSize = next;
        }
        else
        {
            Invalidate();
        }
    }

    protected override void WndProc(ref Message m)
    {
        if (m.Msg == WmNcHitTest)
        {
            m.Result = (IntPtr)HtTransparent;
            return;
        }

        base.WndProc(ref m);
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        // Background is painted in OnPaint to avoid flicker.
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics graphics = e.Graphics;
        EnsureRoundPath(ClientSize);
        if (_roundPath is null)
        {
            return;
        }

        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.FillPath(_backBrush, _roundPath);
        graphics.DrawPath(_borderPen, _roundPath);
        graphics.SmoothingMode = SmoothingMode.None;

        var imageRect = new Rectangle(PaddingSize, PaddingSize, _previewSize, _previewSize);
        if (_sample != null)
        {
            ScreenColorPickerMagnifierPainter.Draw(graphics, _sample, _samplePoint, imageRect, _gridSize, _zoom);
        }

        var footerRect = new Rectangle(PaddingSize, imageRect.Bottom + 4, _previewSize, _footerHeight - 8);
        var swatch = new Rectangle(footerRect.X, footerRect.Y, 36, Math.Max(24, footerRect.Height - 4));
        using (var swatchBrush = new SolidBrush(_color))
        {
            graphics.FillRectangle(swatchBrush, swatch);
        }

        graphics.DrawRectangle(_swatchPen, swatch);

        string[] lines = ScreenColorPickerColorFormatter.BuildReadoutLines(_color, _visibleFormats, includeKnownName: true);
        string meta = KryptonScreenColorPicker.Strings.FormatMagnifierMeta(_zoom, _gridSize);
        float textX = swatch.Right + 8;
        float y = footerRect.Y + 2;
        for (int i = 0; i < lines.Length; i++)
        {
            graphics.DrawString(lines[i], i == 0 ? _bold : _font, _textBrush, textX, y);
            y += LineHeight;
        }

        graphics.DrawString(meta, _font, _textBrush, textX, y);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _font.Dispose();
            _bold.Dispose();
            _backBrush.Dispose();
            _textBrush.Dispose();
            _borderPen.Dispose();
            _swatchPen.Dispose();
            _roundPath?.Dispose();
        }

        base.Dispose(disposing);
    }

    private Size CalculateSize(int magnifierSize, int zoom)
    {
        int preview = CalculatePreviewSize(magnifierSize, zoom);
        return new Size(preview + (PaddingSize * 2), preview + _footerHeight + PaddingSize);
    }

    private static int CalculatePreviewSize(int magnifierSize, int zoom)
    {
        int odd = KryptonScreenColorPicker.ClampMagnifierSize(magnifierSize);
        int minCell = Math.Max(KryptonScreenColorPicker.ClampZoom(zoom),
            (MinimumPreview + odd - 1) / odd);
        return minCell * odd;
    }

    private void EnsureRoundPath(Size size)
    {
        if (_roundPath != null && _chromeSize == size)
        {
            return;
        }

        _roundPath?.Dispose();
        _chromeSize = size;
        _roundPath = CreateRoundRect(new Rectangle(0, 0, Math.Max(1, size.Width), Math.Max(1, size.Height)),
            CornerRadius);
        Region = new Region(_roundPath);
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
