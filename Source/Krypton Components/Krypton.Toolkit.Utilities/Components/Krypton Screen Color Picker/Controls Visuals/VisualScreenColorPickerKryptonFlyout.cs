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
/// Borderless TopMost host form that owns the <see cref="VisualScreenColorPickerKryptonFlyout"/> control
/// and moves it independently of the transparent overlay to avoid erase artifacts.
/// </summary>
internal sealed class VisualScreenColorPickerKryptonFlyoutForm : Form
{
    private readonly VisualScreenColorPickerKryptonFlyout _flyout;

    internal VisualScreenColorPickerKryptonFlyoutForm(KryptonCustomPaletteBase? palette)
    {
        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.Manual;
        ShowInTaskbar = false;
        TopMost = true;
        // Prevent the form from stealing focus from the overlay.
        SetStyle(ControlStyles.Selectable, false);

        _flyout = new VisualScreenColorPickerKryptonFlyout();
        _flyout.ApplyPalette(palette);
        _flyout.Dock = DockStyle.Fill;
        Controls.Add(_flyout);
    }

    /// <summary>Gets the pixel size of the flyout control.</summary>
    internal Size FlyoutSize => _flyout.Size;

    internal void UpdateSample(Bitmap screenshot, Point samplePoint, Color color, int magnifierSize, int zoom)
    {
        _flyout.UpdateSample(screenshot, samplePoint, color, magnifierSize, zoom);
        // Resize the host form to match the flyout after UpdateSample may have changed it.
        ClientSize = _flyout.Size;
    }

    protected override bool ShowWithoutActivation => true;

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            // WS_EX_NOACTIVATE — prevents the form from stealing focus.
            cp.ExStyle |= 0x08000000;
            return cp;
        }
    }
}

/// <summary>
/// Themed magnifier flyout that follows the cursor during a screen pick.
/// </summary>
internal sealed class VisualScreenColorPickerKryptonFlyout : KryptonHeaderGroup
{
    private readonly MagnifierCanvas _canvas;

    internal VisualScreenColorPickerKryptonFlyout()
    {
        ((ISupportInitialize)this).BeginInit();
        ((ISupportInitialize)Panel).BeginInit();
        SuspendLayout();

        TabStop = false;
        HeaderVisibleSecondary = false;
        ValuesPrimary.Heading = @"#000000";
        ValuesPrimary.Description = @"RGB(0, 0, 0)";

        _canvas = new MagnifierCanvas
        {
            Dock = DockStyle.Fill,
            TabStop = false
        };

        Panel.Padding = new Padding(4);
        Panel.Controls.Add(_canvas);

        ((ISupportInitialize)Panel).EndInit();
        ((ISupportInitialize)this).EndInit();
        ResumeLayout(false);
    }

    internal void ApplyPalette(KryptonCustomPaletteBase? palette) => LocalCustomPalette = palette;

    internal void UpdateSample(Bitmap screenshot, Point samplePoint, Color color, int magnifierSize, int zoom)
    {
        int mag = magnifierSize * zoom;
        var nextSize = new Size(mag + 24, mag + 72);
        if (Size != nextSize)
        {
            Size = nextSize;
        }
        ValuesPrimary.Heading = ScreenColorPickerMagnifierPainter.FormatRgbHex(color);
        ValuesPrimary.Description = string.Format(CultureInfo.InvariantCulture,
            @"RGB({0}, {1}, {2})  ·  {3}x  ·  {4} src px", color.R, color.G, color.B, zoom, magnifierSize);
        _canvas.SetSample(screenshot, samplePoint, magnifierSize, zoom);
    }

    private sealed class MagnifierCanvas : Panel
    {
        private Bitmap? _screenshot;
        private Point _samplePoint;
        private int _magnifierSize = 11;
        private int _zoom = 12;

        internal MagnifierCanvas()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint
                     | ControlStyles.UserPaint
                     | ControlStyles.OptimizedDoubleBuffer
                     | ControlStyles.Opaque, true);
            DoubleBuffered = true;
            BackColor = Color.Black;
        }

        internal void SetSample(Bitmap screenshot, Point samplePoint, int magnifierSize, int zoom)
        {
            _screenshot = screenshot;
            _samplePoint = samplePoint;
            _magnifierSize = magnifierSize;
            _zoom = zoom;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_screenshot is null)
            {
                return;
            }

            ScreenColorPickerMagnifierPainter.Draw(e.Graphics, _screenshot, _samplePoint, ClientRectangle, _magnifierSize, _zoom);
        }
    }
}
