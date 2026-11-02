#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Manual demo for GitHub #3381: vertical text alignment on <see cref="KryptonButton"/> with large corner rounding
/// relative to control height (wide pill bar, large font, centered text).
/// </summary>
public partial class Bug3381KryptonButtonRoundedTextCenteringDemo : KryptonForm
{
    public Bug3381KryptonButtonRoundedTextCenteringDemo()
    {
        InitializeComponent();
        BuildDemoLayout();
    }

    private void BuildDemoLayout()
    {
        _root.SuspendLayout();
        SuspendLayout();

        int row = 0;
        void AddRow(RowStyle style, Control c)
        {
            _root.RowStyles.Add(style);
            _root.Controls.Add(c, 0, row++);
        }

        AddRow(new RowStyle(SizeType.AutoSize), NewSectionHeader(@"1) Wide × short — matches reporter setup (large rounding vs height)"));
        var btnWideIssueShape = CreatePillButton(@"Начать", 40f, 100f);
        AddRow(new RowStyle(SizeType.Absolute, 102f), NewFillPanel(102, btnWideIssueShape));

        AddRow(new RowStyle(SizeType.AutoSize), NewSectionHeader(@"2) Two equal-height bars — different font sizes (metrics stress)"));
        var tlpPair = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            Margin = new Padding(0)
        };
        tlpPair.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
        tlpPair.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
        tlpPair.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
        var btnSideBySideLeft = CreatePillButton(@"Start — 22pt", 22f, 100f);
        var btnSideBySideRight = CreatePillButton(@"Начать — 40pt", 40f, 100f);
        tlpPair.Controls.Add(NewFillPanel(86, btnSideBySideLeft), 0, 0);
        tlpPair.Controls.Add(NewFillPanel(86, btnSideBySideRight), 1, 0);
        AddRow(new RowStyle(SizeType.Absolute, 86f), tlpPair);

        AddRow(new RowStyle(SizeType.AutoSize), NewSectionHeader(@"3) Tall narrow capsule (rounding clamped by width)"));
        var btnTallNarrow = CreatePillButton(@"OK", 28f, 72f);
        var tlpTall = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Height = 200,
            ColumnCount = 3,
            RowCount = 1,
            Margin = new Padding(0)
        };
        tlpTall.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
        tlpTall.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140f));
        tlpTall.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
        tlpTall.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
        tlpTall.Controls.Add(new Panel { Dock = DockStyle.Fill, Margin = new Padding(0) }, 0, 0);
        tlpTall.Controls.Add(btnTallNarrow, 1, 0);
        btnTallNarrow.Dock = DockStyle.Fill;
        tlpTall.Controls.Add(new Panel { Dock = DockStyle.Fill, Margin = new Padding(0) }, 2, 0);
        AddRow(new RowStyle(SizeType.Absolute, 200f), tlpTall);

        AddRow(new RowStyle(SizeType.AutoSize), NewSectionHeader(@"4) Low rounding baseline (rounding ≈ 4)"));
        var btnLowRounding = CreatePillButton(@"Centered reference", 18f, 4f);
        AddRow(new RowStyle(SizeType.Absolute, 52f), NewFillPanel(52, btnLowRounding));

        AddRow(new RowStyle(SizeType.AutoSize), NewSectionHeader(@"5) Live — rounding, TextV, font size, button height"));
        _btnLive = CreatePillButton(@"Live preview", 36f, 100f);
        AddRow(new RowStyle(SizeType.Percent, 100f), BuildLiveSectionPanel());

        var body = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0) };
        body.Controls.Add(_root);
        body.Controls.Add(_lblIntro);
        Controls.Add(body);
        _lblIntro.BringToFront();

        _root.ResumeLayout(false);
        ResumeLayout(false);
    }

    private Panel BuildLiveSectionPanel()
    {
        var wrap = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0) };

        _trackRounding = new TrackBar
        {
            Minimum = 0,
            Maximum = 120,
            TickFrequency = 10,
            SmallChange = 2,
            LargeChange = 10,
            Value = 100,
            Width = 360,
            Height = 42,
            Margin = new Padding(12, 4, 0, 0)
        };
        _lblRoundingValue = new Label
        {
            AutoSize = true,
            Text = @"Rounding: 100",
            Margin = new Padding(0, 10, 0, 0)
        };
        _trackRounding.ValueChanged += OnTrackRoundingValueChanged;

        _cmbTextV = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Width = 200,
            Margin = new Padding(12, 4, 0, 0)
        };
        _cmbTextV.Items.AddRange(new object[] { @"TextV: Center", @"TextV: Near (top)", @"TextV: Far (bottom)" });
        _cmbTextV.SelectedIndex = 0;
        _cmbTextV.SelectedIndexChanged += OnCmbTextVSelectedIndexChanged;

        _nudFontSize = new NumericUpDown
        {
            Minimum = 10,
            Maximum = 96,
            Value = 36,
            Width = 72,
            Margin = new Padding(8, 4, 0, 0)
        };
        _nudFontSize.ValueChanged += OnNudFontSizeValueChanged;

        _nudLiveHeight = new NumericUpDown
        {
            Minimum = 48,
            Maximum = 220,
            Value = 96,
            Width = 72,
            Margin = new Padding(8, 4, 0, 0)
        };
        _nudLiveHeight.ValueChanged += OnNudLiveHeightValueChanged;

        var topFlow = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = true,
            Padding = new Padding(0, 0, 0, 8)
        };
        topFlow.Controls.Add(_lblRoundingValue);
        topFlow.Controls.Add(_trackRounding);
        topFlow.Controls.Add(_cmbTextV);
        topFlow.Controls.Add(new Label { AutoSize = true, Text = @"Font (pt):", Margin = new Padding(12, 10, 0, 0) });
        topFlow.Controls.Add(_nudFontSize);
        topFlow.Controls.Add(new Label { AutoSize = true, Text = @"Btn height:", Margin = new Padding(12, 10, 0, 0) });
        topFlow.Controls.Add(_nudLiveHeight);

        _pLiveButtonHost = new Panel
        {
            Dock = DockStyle.Top,
            Height = 96,
            Margin = new Padding(0)
        };
        _pLiveButtonHost.Controls.Add(_btnLive);
        _btnLive.Dock = DockStyle.Fill;

        wrap.Controls.Add(_pLiveButtonHost);
        wrap.Controls.Add(topFlow);

        return wrap;
    }

    private static Control NewSectionHeader(string text) =>
        new KryptonLabel
        {
            Text = text,
            Margin = new Padding(0, 12, 0, 4),
            AutoSize = true
        };

    private static Panel NewFillPanel(int height, Control inner)
    {
        var p = new Panel { Height = height, Dock = DockStyle.Fill, Margin = new Padding(0) };
        p.Controls.Add(inner);
        inner.Dock = DockStyle.Fill;
        return p;
    }

    private void OnTrackRoundingValueChanged(object? sender, EventArgs e)
    {
        float v = _trackRounding.Value;
        _lblRoundingValue.Text = string.Format(CultureInfo.InvariantCulture, @"Rounding: {0:0}", v);
        ApplyRounding(_btnLive, v);
        RequestLiveRelayout();
    }

    private void OnCmbTextVSelectedIndexChanged(object? sender, EventArgs e)
    {
        PaletteRelativeAlign align = _cmbTextV.SelectedIndex switch
        {
            1 => PaletteRelativeAlign.Near,
            2 => PaletteRelativeAlign.Far,
            _ => PaletteRelativeAlign.Center
        };
        _btnLive.StateCommon.Content.ShortText.TextV = align;
        RequestLiveRelayout();
    }

    private void OnNudFontSizeValueChanged(object? sender, EventArgs e)
    {
        float pt = (float)_nudFontSize.Value;
        string familyName = _btnLive.StateCommon.Content.ShortText.Font?.FontFamily?.Name ?? FontFamily.GenericSansSerif.Name;
        _btnLive.StateCommon.Content.ShortText.Font = new Font(familyName, pt, FontStyle.Regular, GraphicsUnit.Point);
        RequestLiveRelayout();
    }

    private void OnNudLiveHeightValueChanged(object? sender, EventArgs e)
    {
        int h = (int)_nudLiveHeight.Value;
        _pLiveButtonHost.Height = Math.Max(48, h);
        RequestLiveRelayout();
    }

    private void RequestLiveRelayout()
    {
        _btnLive.Invalidate(true);
        _root.PerformLayout();
    }

    private static KryptonButton CreatePillButton(string text, float fontSizePt, float rounding)
    {
        var b = new KryptonButton
        {
            Values = { Text = text },
            AutoSize = false
        };
        StylePillSurface(b);
        b.StateCommon.Content.ShortText.Font = new Font(@"Segoe UI", fontSizePt, FontStyle.Regular, GraphicsUnit.Point);
        b.StateCommon.Content.ShortText.TextH = PaletteRelativeAlign.Center;
        b.StateCommon.Content.ShortText.TextV = PaletteRelativeAlign.Center;
        b.StateCommon.Content.ShortText.Hint = PaletteTextHint.AntiAlias;
        ApplyRounding(b, rounding);
        return b;
    }

    private static void StylePillSurface(KryptonButton b)
    {
        Color fill = Color.YellowGreen;
        Color edge = Color.YellowGreen;
        b.StateCommon.Back.Color1 = fill;
        b.StateCommon.Back.Color2 = fill;
        b.StateCommon.Border.Color1 = edge;
        b.StateCommon.Border.DrawBorders = PaletteDrawBorders.All;
        b.StateCommon.Border.Width = 2;
        b.StateCommon.Content.ShortText.Color1 = Color.White;
        b.OverrideDefault.Back.Color1 = fill;
        b.OverrideDefault.Back.Color2 = fill;
        b.OverrideDefault.Border.Color1 = edge;
        b.OverrideDefault.Border.DrawBorders = PaletteDrawBorders.All;
    }

    private static void ApplyRounding(KryptonButton b, float rounding)
    {
        b.StateCommon.Border.Rounding = rounding;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        OnNudLiveHeightValueChanged(null, EventArgs.Empty);
    }
}
