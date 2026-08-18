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
/// Mouse hits pass through so the overlay can confirm or cancel the pick.
/// </summary>
internal sealed class VisualScreenColorPickerKryptonFlyoutForm : Form
{
    private const int WsExNoActivate = 0x08000000;
    private const int WsExToolWindow = 0x00000080;
    private const int WsExTopMost = 0x00000008;
    private const int WmNcHitTest = 0x0084;
    private const int HtTransparent = -1;

    private readonly VisualScreenColorPickerKryptonFlyout _flyout;

    internal VisualScreenColorPickerKryptonFlyoutForm(KryptonCustomPaletteBase? palette,
        KryptonScreenColorPickerColorFormat visibleFormats)
    {
        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.Manual;
        ShowInTaskbar = false;
        TopMost = true;
        ControlBox = false;
        MaximizeBox = false;
        MinimizeBox = false;
        SetStyle(ControlStyles.Selectable, false);

        _flyout = new VisualScreenColorPickerKryptonFlyout(visibleFormats);
        _flyout.ApplyPalette(palette);
        _flyout.Dock = DockStyle.Fill;
        Controls.Add(_flyout);
        Controls.Add(_flyout.ReadoutPanel);
        ClientSize = _flyout.PreferredFlyoutSize;
    }

    /// <summary>Gets the pixel size of the flyout control.</summary>
    internal Size FlyoutSize => ClientSize;

    internal void UpdateSample(Bitmap screenshot, Point samplePoint, Color color, int magnifierSize, int zoom)
    {
        Size nextSize = VisualScreenColorPickerKryptonFlyout.CalculateSize(magnifierSize, zoom, _flyout.VisibleFormats);
        _flyout.UpdateSample(screenshot, samplePoint, color, magnifierSize, zoom);
        if (ClientSize != nextSize)
        {
            ClientSize = nextSize;
        }
    }

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

    protected override void WndProc(ref Message m)
    {
        if (m.Msg == WmNcHitTest)
        {
            // Hits pass through to the overlay so left-click still samples.
            m.Result = (IntPtr)HtTransparent;
            return;
        }

        base.WndProc(ref m);
    }
}

/// <summary>
/// Themed magnifier flyout that follows the cursor during a screen pick.
/// The header shows the nearest known colour name when that format is enabled;
/// remaining formats live in a panel under the preview.
/// </summary>
internal sealed class VisualScreenColorPickerKryptonFlyout : KryptonHeaderGroup
{
    private const int ChromeWidth = 24;
    private const int ChromeHeight = 56;
    private const int LineHeight = 20;
    private const int ReadoutPadding = 12;
    private const int MinimumWidth = 280;

    private readonly KryptonScreenColorPickerColorFormat _visibleFormats;
    private readonly MagnifierCanvas _canvas;
    private readonly KryptonPanel _readoutPanel;
    private readonly Panel _swatch;
    private readonly TableLayoutPanel _textStack;
    private readonly KryptonLabel[] _formatLabels;
    private readonly KryptonLabel _metaLabel;

    internal VisualScreenColorPickerKryptonFlyout(KryptonScreenColorPickerColorFormat visibleFormats)
    {
        _visibleFormats = ScreenColorPickerColorFormatter.Normalize(visibleFormats);
        int panelLines = ScreenColorPickerColorFormatter.CountPanelLines(_visibleFormats, includeKnownName: false);

        ((ISupportInitialize)this).BeginInit();
        ((ISupportInitialize)Panel).BeginInit();
        SuspendLayout();

        TabStop = false;
        HeaderVisibleSecondary = false;
        UseKryptonScrollbars = false;
        ValuesPrimary.Heading = ScreenColorPickerColorFormatter.FormatKnownName(Color.Black);
        ValuesPrimary.Description = string.Empty;
        ValuesPrimary.Image = Properties.Resources.ColorPickerHeadingImage;

        _canvas = new MagnifierCanvas
        {
            Dock = DockStyle.Fill,
            TabStop = false
        };

        _swatch = new Panel
        {
            Dock = DockStyle.Left,
            Width = 36,
            Margin = new Padding(0),
            TabStop = false,
            BackColor = Color.Black
        };

        _formatLabels = new KryptonLabel[panelLines];
        _textStack = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = panelLines + 1,
            TabStop = false,
            Padding = new Padding(8, 2, 4, 2),
            BackColor = Color.Transparent
        };

        for (int i = 0; i < panelLines; i++)
        {
            _textStack.RowStyles.Add(new RowStyle(SizeType.Absolute, LineHeight));
            KryptonLabel label = CreateReadoutLabel(i == 0 ? LabelStyle.BoldPanel : LabelStyle.NormalPanel, string.Empty);
            _formatLabels[i] = label;
            _textStack.Controls.Add(label, 0, i);
        }

        _textStack.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
        _metaLabel = CreateReadoutLabel(LabelStyle.NormalPanel,
            KryptonScreenColorPicker.Strings.FormatMagnifierMeta(KryptonScreenColorPicker.DefaultZoom,
                KryptonScreenColorPicker.DefaultMagnifierSize));
        _textStack.Controls.Add(_metaLabel, 0, panelLines);

        _readoutPanel = new KryptonPanel
        {
            Dock = DockStyle.Bottom,
            Height = CalculateReadoutHeight(panelLines),
            TabStop = false,
            Padding = new Padding(4)
        };
        _readoutPanel.PanelBackStyle = PaletteBackStyle.PanelAlternate;
        _readoutPanel.Controls.Add(_textStack);
        _readoutPanel.Controls.Add(_swatch);

        Panel.AutoScroll = false;
        Panel.Padding = new Padding(4);
        Panel.Controls.Add(_canvas);

        ((ISupportInitialize)Panel).EndInit();
        ((ISupportInitialize)this).EndInit();
        ResumeLayout(false);
    }

    internal KryptonScreenColorPickerColorFormat VisibleFormats => _visibleFormats;

    internal KryptonPanel ReadoutPanel => _readoutPanel;

    internal Size PreferredFlyoutSize =>
        CalculateSize(KryptonScreenColorPicker.DefaultMagnifierSize, KryptonScreenColorPicker.DefaultZoom, _visibleFormats);

    internal static Size CalculateSize(int magnifierSize, int zoom, KryptonScreenColorPickerColorFormat visibleFormats)
    {
        int mag = magnifierSize * zoom;
        int panelLines = ScreenColorPickerColorFormatter.CountPanelLines(visibleFormats, includeKnownName: false);
        return new Size(Math.Max(mag + ChromeWidth, MinimumWidth),
            mag + ChromeHeight + CalculateReadoutHeight(panelLines));
    }

    private static int CalculateReadoutHeight(int panelLines) =>
        Math.Max(48, ReadoutPadding + ((panelLines + 1) * LineHeight));

    internal void ApplyPalette(KryptonCustomPaletteBase? palette)
    {
        LocalCustomPalette = palette;
        _readoutPanel.Palette = palette;
        for (int i = 0; i < _formatLabels.Length; i++)
        {
            _formatLabels[i].LocalCustomPalette = palette;
        }

        _metaLabel.LocalCustomPalette = palette;
    }

    internal void UpdateSample(Bitmap screenshot, Point samplePoint, Color color, int magnifierSize, int zoom)
    {
        bool showKnownName = (_visibleFormats & KryptonScreenColorPickerColorFormat.KnownName) ==
                             KryptonScreenColorPickerColorFormat.KnownName;
        ValuesPrimary.Heading = showKnownName
            ? ScreenColorPickerColorFormatter.FormatKnownName(color)
            : ScreenColorPickerColorFormatter.FormatHex(color);
        ValuesPrimary.Description = string.Empty;
        _swatch.BackColor = Color.FromArgb(255, color.R, color.G, color.B);

        string[] lines = ScreenColorPickerColorFormatter.BuildReadoutLines(color, _visibleFormats, includeKnownName: false);
        int count = Math.Min(lines.Length, _formatLabels.Length);
        for (int i = 0; i < count; i++)
        {
            _formatLabels[i].Values.Text = lines[i];
        }

        _metaLabel.Values.Text = KryptonScreenColorPicker.Strings.FormatMagnifierMeta(zoom, magnifierSize);
        _canvas.SetSample(screenshot, samplePoint, magnifierSize, zoom);
    }

    private static KryptonLabel CreateReadoutLabel(LabelStyle style, string text)
    {
        var label = new KryptonLabel
        {
            Dock = DockStyle.Fill,
            TabStop = false,
            LabelStyle = style
        };
        label.Values.Text = text;
        return label;
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
