#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Demonstrates <see cref="KryptonKnob"/> palette integration, interaction, and scale options.
/// </summary>
public partial class KryptonKnobDemo : KryptonForm
{
    public KryptonKnobDemo()
    {
        InitializeComponent();
    }

    private void KryptonKnobDemo_Load(object? sender, EventArgs e)
    {
        Icon = SystemIcons.Application;

        kknobMain.Value = 35;
        ktrkValue.Value = kknobMain.Value;
        knudMinimum.Value = kknobMain.Minimum;
        knudMaximum.Value = kknobMain.Maximum;
        knudLargeChange.Value = kknobMain.LargeChange;
        knudSmallChange.Value = kknobMain.SmallChange;

        kchkShowLargeScale.Checked = kknobMain.ShowLargeScale;
        kchkShowSmallScale.Checked = kknobMain.ShowSmallScale;
        kchkEnabled.Checked = kknobMain.Enabled;

        PopulateComboFromEnum(kcmbIndicatorShape, typeof(KnobIndicatorShape));
        kcmbIndicatorShape.SelectedItem = kknobMain.IndicatorShape;
        knudIndicatorSize.Value = kknobMain.IndicatorSize;

        PopulateComboFromEnum(kcmbKnobStyle, typeof(KnobStyle));
        kcmbKnobStyle.SelectedItem = kknobMain.KnobStyle;

        kknobDisabled.Value = 60;
        kknobDisabled.Enabled = false;

        kryptonPropertyGrid1.SelectedObject = kknobMain;
        UpdateValueLabel(kknobMain.Value);
        UpdateStatus(@"Drag the knob, use arrow keys when focused, or try different knob and indicator styles.");
    }

    private static void PopulateComboFromEnum(KryptonComboBox combo, Type enumType)
    {
        combo.Items.Clear();
        foreach (object value in Enum.GetValues(enumType))
        {
            combo.Items.Add(value);
        }
    }

    private void UpdateValueLabel(int value) =>
        klblValue.Values.Text = $@"Value: {value}  (range {kknobMain.Minimum}–{kknobMain.Maximum})";

    private void UpdateStatus(string message) => klblStatus.Values.Text = message;

    private void kknobMain_KnobValueChanged(object? sender, KnobValueChangedEventArgs e)
    {
        if (ktrkValue.Value != e.Value)
        {
            ktrkValue.Value = Math.Max(ktrkValue.Minimum, Math.Min(ktrkValue.Maximum, e.Value));
        }

        UpdateValueLabel(e.Value);
    }

    private void kknobMain_Scroll(object? sender, EventArgs e) =>
        UpdateStatus(@"User changed knob value (Scroll event).");

    private void ktrkValue_ValueChanged(object? sender, EventArgs e)
    {
        if (kknobMain.Value != ktrkValue.Value)
        {
            kknobMain.Value = ktrkValue.Value;
        }

        UpdateValueLabel(ktrkValue.Value);
    }

    private void knudMinimum_ValueChanged(object? sender, EventArgs e)
    {
        kknobMain.Minimum = (int)knudMinimum.Value;
        ktrkValue.Minimum = kknobMain.Minimum;
        ktrkValue.Value = Math.Max(ktrkValue.Minimum, ktrkValue.Value);
    }

    private void knudMaximum_ValueChanged(object? sender, EventArgs e)
    {
        kknobMain.Maximum = (int)knudMaximum.Value;
        ktrkValue.Maximum = kknobMain.Maximum;
        ktrkValue.Value = Math.Min(ktrkValue.Maximum, ktrkValue.Value);
        UpdateValueLabel(kknobMain.Value);
    }

    private void knudLargeChange_ValueChanged(object? sender, EventArgs e) =>
        kknobMain.LargeChange = (int)knudLargeChange.Value;

    private void knudSmallChange_ValueChanged(object? sender, EventArgs e) =>
        kknobMain.SmallChange = (int)knudSmallChange.Value;

    private void kchkShowLargeScale_CheckedChanged(object? sender, EventArgs e) =>
        kknobMain.ShowLargeScale = kchkShowLargeScale.Checked;

    private void kchkShowSmallScale_CheckedChanged(object? sender, EventArgs e) =>
        kknobMain.ShowSmallScale = kchkShowSmallScale.Checked;

    private void kchkEnabled_CheckedChanged(object? sender, EventArgs e) =>
        kknobMain.Enabled = kchkEnabled.Checked;

    private void kcmbIndicatorShape_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kcmbIndicatorShape.SelectedItem is KnobIndicatorShape shape)
        {
            kknobMain.IndicatorShape = shape;
            UpdateStatus($@"Indicator shape set to {shape}.");
        }
    }

    private void knudIndicatorSize_ValueChanged(object? sender, EventArgs e) =>
        kknobMain.IndicatorSize = (int)knudIndicatorSize.Value;

    private void kcmbKnobStyle_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kcmbKnobStyle.SelectedItem is KnobStyle style)
        {
            kknobMain.KnobStyle = style;
            UpdateStatus($@"Knob style set to {style}.");
        }
    }

    private void kbtnIndicatorDot_Click(object? sender, EventArgs e)
    {
        kknobMain.IndicatorCustomPath = null;
        kknobMain.IndicatorCustomPoints = null;
        kknobMain.IndicatorShape = KnobIndicatorShape.Dot;
        kcmbIndicatorShape.SelectedItem = KnobIndicatorShape.Dot;
        UpdateStatus(@"Indicator shape set to Dot (flat, no bevel).");
    }

    private void kbtnIndicatorChevron_Click(object? sender, EventArgs e)
    {
        kknobMain.IndicatorCustomPath = null;
        kknobMain.IndicatorCustomPoints = null;
        kknobMain.IndicatorShape = KnobIndicatorShape.Chevron;
        kcmbIndicatorShape.SelectedItem = KnobIndicatorShape.Chevron;
        UpdateStatus(@"Indicator shape set to Chevron.");
    }

    private void kbtnIndicatorCustomStar_Click(object? sender, EventArgs e)
    {
        kknobMain.IndicatorCustomPath = null;
        kknobMain.IndicatorCustomPoints = CreateUnitStarPoints(5);
        kknobMain.IndicatorShape = KnobIndicatorShape.Custom;
        kknobMain.IndicatorSize = Math.Max(kknobMain.IndicatorSize, 12);
        knudIndicatorSize.Value = kknobMain.IndicatorSize;
        kcmbIndicatorShape.SelectedItem = KnobIndicatorShape.Custom;
        UpdateStatus(@"Indicator shape set to Custom using a normalized 5-point star polygon.");
    }

    private static PointF[] CreateUnitStarPoints(int points)
    {
        var verts = new PointF[points * 2];
        const double outer = 1.0;
        const double inner = 0.42;
        for (var i = 0; i < points * 2; i++)
        {
            var radius = i % 2 == 0 ? outer : inner;
            var angle = i * Math.PI / points;
            verts[i] = new PointF((float)(Math.Cos(angle) * radius), (float)(Math.Sin(angle) * radius));
        }

        return verts;
    }

    private void kbtnReset_Click(object? sender, EventArgs e)
    {
        kknobMain.Value = kknobMain.Minimum;
        ktrkValue.Value = kknobMain.Value;
        UpdateValueLabel(kknobMain.Value);
        UpdateStatus(@"Value reset to minimum.");
    }

    private void kbtnSelectKnobInGrid_Click(object? sender, EventArgs e) =>
        kryptonPropertyGrid1.SelectedObject = kknobMain;

    private void kbtnFaceColorPreset_Click(object? sender, EventArgs e)
    {
        kknobMain.StateCommon.Face.Color1 = Color.MediumSlateBlue;
        kknobMain.StateCommon.Face.Color2 = Color.MidnightBlue;
        kknobMain.StateTracking.Face.Color1 = Color.CornflowerBlue;
        kknobMain.StatePressed.Face.Color1 = Color.RoyalBlue;
        kknobMain.StateCommon.Indicator.Color1 = Color.Gold;
        kknobMain.StateCommon.Tick.ResetColor1();
        UpdateStatus(@"Applied custom face, tracking, pressed, and indicator colours. Scale ticks use palette text colour unless overridden via StateCommon.Tick.");
    }

    private void kbtnPopulateFromTheme_Click(object? sender, EventArgs e)
    {
        kknobMain.StateCommon.PopulateFromBase(PaletteState.Normal);
        kknobMain.StateNormal.PopulateFromBase(PaletteState.Normal);
        kknobMain.StateTracking.PopulateFromBase(PaletteState.Tracking);
        kknobMain.StatePressed.PopulateFromBase(PaletteState.Pressed);
        UpdateStatus(@"Repopulated palette states from the current global theme.");
    }

    private void kbtnIndustrialYellow_Click(object? sender, EventArgs e)
    {
        kknobMain.Appearance.Backplate.ApplyYellowPanelPreset();
        kknobMain.KnobStyle = KnobStyle.Industrial;
        kknobMain.IndicatorShape = KnobIndicatorShape.Dot;
        kknobMain.IndicatorSize = 8;
        kknobMain.StateCommon.Face.Color1 = Color.FromArgb(200, 20, 30);
        kknobMain.StateCommon.Face.Color2 = Color.FromArgb(120, 0, 10);
        kknobMain.StateCommon.Indicator.Color1 = Color.White;
        kknobMain.ShowLargeScale = false;
        kcmbKnobStyle.SelectedItem = KnobStyle.Industrial;
        kcmbIndicatorShape.SelectedItem = KnobIndicatorShape.Dot;
        UpdateStatus(@"Yellow rounded industrial panel with red knob and white dot indicator.");
    }

    private void kbtnIndustrialSilver_Click(object? sender, EventArgs e)
    {
        kknobMain.Appearance.Backplate.ApplySilverPanelPreset();
        kknobMain.KnobStyle = KnobStyle.Industrial;
        kknobMain.IndicatorShape = KnobIndicatorShape.Bar;
        kknobMain.IndicatorSize = 10;
        kknobMain.StateCommon.Face.Color1 = Color.FromArgb(190, 25, 35);
        kknobMain.StateCommon.Face.Color2 = Color.FromArgb(110, 0, 15);
        kknobMain.StateCommon.Indicator.Color1 = Color.White;
        kknobMain.ShowLargeScale = false;
        kcmbKnobStyle.SelectedItem = KnobStyle.Industrial;
        kcmbIndicatorShape.SelectedItem = KnobIndicatorShape.Bar;
        UpdateStatus(@"Silver square industrial panel with red knob and bar stripe indicator.");
    }

    private void kbtnIndustrialBlack_Click(object? sender, EventArgs e)
    {
        kknobMain.Appearance.Backplate.ApplyBlackPanelPreset();
        kknobMain.KnobStyle = KnobStyle.Industrial;
        kknobMain.IndicatorShape = KnobIndicatorShape.GlowDot;
        kknobMain.IndicatorSize = 10;
        kknobMain.StateCommon.Face.Color1 = Color.FromArgb(70, 70, 75);
        kknobMain.StateCommon.Face.Color2 = Color.FromArgb(25, 25, 28);
        kknobMain.StateCommon.Indicator.Color1 = Color.FromArgb(255, 40, 30);
        kknobMain.ShowLargeScale = false;
        kcmbKnobStyle.SelectedItem = KnobStyle.Industrial;
        kcmbIndicatorShape.SelectedItem = KnobIndicatorShape.GlowDot;
        UpdateStatus(@"Black circular industrial panel with matte knob and glowing red LED indicator.");
    }
}
