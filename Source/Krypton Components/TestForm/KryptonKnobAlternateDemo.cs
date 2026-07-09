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
/// Demonstrates <see cref="KryptonKnobAlternate"/> palette integration, graduations, and interaction.
/// </summary>
public partial class KryptonKnobAlternateDemo : KryptonForm
{
    public KryptonKnobAlternateDemo()
    {
        InitializeComponent();
    }

    private void KryptonKnobAlternateDemo_Load(object? sender, EventArgs e)
    {
        Icon = SystemIcons.Application;

        kknobMain.Value = 42;
        ktrkValue.Value = kknobMain.Value;
        knudMinimum.Value = kknobMain.Minimum;
        knudMaximum.Value = kknobMain.Maximum;
        knudLargeChange.Value = kknobMain.LargeChange;
        knudSmallChange.Value = kknobMain.SmallChange;
        knudScaleDivisions.Value = kknobMain.ScaleDivisions;
        knudScaleSubDivisions.Value = kknobMain.ScaleSubDivisions;
        knudStartAngle.Value = (decimal)kknobMain.StartAngle;
        knudEndAngle.Value = (decimal)kknobMain.EndAngle;
        knudMouseWheelPartitions.Value = kknobMain.MouseWheelBarPartitions;

        kchkShowLargeScale.Checked = kknobMain.ShowLargeScale;
        kchkShowSmallScale.Checked = kknobMain.ShowSmallScale;
        kchkDrawDivInside.Checked = kknobMain.DrawDivInside;
        kchkScaleFontAutoSize.Checked = kknobMain.ScaleTypefaceAutoSize;
        kchkEnabled.Checked = kknobMain.Enabled;

        PopulateComboFromEnum(kcmbIndicatorShape, typeof(KnobIndicatorShape));
        kcmbIndicatorShape.SelectedItem = kknobMain.IndicatorShape;

        kknobDisabled.Value = 65;
        kknobDisabled.Enabled = false;

        kryptonPropertyGrid1.SelectedObject = kknobMain;
        UpdateValueLabel(kknobMain.Value);
        UpdateStatus(@"Click once to focus, click again and drag to rotate. Arrow keys work when focused. Numeric graduations use ScaleDivisions.");
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

    private void kknobMain_ValueChanged(object? sender, KnobValueChangedEventArgs e)
    {
        if (ktrkValue.Value != e.Value)
        {
            ktrkValue.Value = Math.Max(ktrkValue.Minimum, Math.Min(ktrkValue.Maximum, e.Value));
        }

        UpdateValueLabel(e.Value);
    }

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

    private void knudScaleDivisions_ValueChanged(object? sender, EventArgs e) =>
        kknobMain.ScaleDivisions = (int)knudScaleDivisions.Value;

    private void knudScaleSubDivisions_ValueChanged(object? sender, EventArgs e) =>
        kknobMain.ScaleSubDivisions = (int)knudScaleSubDivisions.Value;

    private void knudStartAngle_ValueChanged(object? sender, EventArgs e) =>
        kknobMain.StartAngle = (float)knudStartAngle.Value;

    private void knudEndAngle_ValueChanged(object? sender, EventArgs e) =>
        kknobMain.EndAngle = (float)knudEndAngle.Value;

    private void knudMouseWheelPartitions_ValueChanged(object? sender, EventArgs e) =>
        kknobMain.MouseWheelBarPartitions = (int)knudMouseWheelPartitions.Value;

    private void kchkShowLargeScale_CheckedChanged(object? sender, EventArgs e) =>
        kknobMain.ShowLargeScale = kchkShowLargeScale.Checked;

    private void kchkShowSmallScale_CheckedChanged(object? sender, EventArgs e) =>
        kknobMain.ShowSmallScale = kchkShowSmallScale.Checked;

    private void kchkDrawDivInside_CheckedChanged(object? sender, EventArgs e) =>
        kknobMain.DrawDivInside = kchkDrawDivInside.Checked;

    private void kchkScaleFontAutoSize_CheckedChanged(object? sender, EventArgs e) =>
        kknobMain.ScaleTypefaceAutoSize = kchkScaleFontAutoSize.Checked;

    private void kchkEnabled_CheckedChanged(object? sender, EventArgs e) =>
        kknobMain.Enabled = kchkEnabled.Checked;

    private void kcmbIndicatorShape_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kcmbIndicatorShape.SelectedItem is KnobIndicatorShape shape)
        {
            kknobMain.IndicatorShape = shape;
            UpdateStatus($@"Indicator shape set to {shape}. Circle and Line are fully supported on the enhanced knob.");
        }
    }

    private void kbtnIndicatorCircle_Click(object? sender, EventArgs e)
    {
        kknobMain.IndicatorShape = KnobIndicatorShape.Circle;
        kcmbIndicatorShape.SelectedItem = KnobIndicatorShape.Circle;
        UpdateStatus(@"Indicator set to Circle (inset gradient marker).");
    }

    private void kbtnIndicatorLine_Click(object? sender, EventArgs e)
    {
        kknobMain.IndicatorShape = KnobIndicatorShape.Line;
        kcmbIndicatorShape.SelectedItem = KnobIndicatorShape.Line;
        UpdateStatus(@"Indicator set to Line (radial needle).");
    }

    private void kbtnHalfCircleArc_Click(object? sender, EventArgs e)
    {
        kknobMain.StartAngle = 180;
        kknobMain.EndAngle = 360;
        knudStartAngle.Value = 180;
        knudEndAngle.Value = 360;
        UpdateStatus(@"Arc set to 180°–360° (top semicircle).");
    }

    private void kbtnFullCircleArc_Click(object? sender, EventArgs e)
    {
        kknobMain.StartAngle = 90;
        kknobMain.EndAngle = 450;
        knudStartAngle.Value = 90;
        knudEndAngle.Value = 450;
        UpdateStatus(@"Arc set to 90°–450° (full circle sweep).");
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
        kknobMain.StateCommon.Face.Color1 = Color.MediumSeaGreen;
        kknobMain.StateCommon.Face.Color2 = Color.DarkGreen;
        kknobMain.StateTracking.Face.Color1 = Color.LightGreen;
        kknobMain.StatePressed.Face.Color1 = Color.ForestGreen;
        kknobMain.StateCommon.Indicator.Color1 = Color.Orange;
        kknobMain.StateCommon.Tick.Color1 = Color.DimGray;
        UpdateStatus(@"Applied custom face, tracking, pressed, indicator, and tick colours via State### properties.");
    }

    private void kbtnLegacyColourPreset_Click(object? sender, EventArgs e)
    {
        kknobMain.KnobBackColour = Color.WhiteSmoke;
        kknobMain.PointerColour = Color.SteelBlue;
        kknobMain.ScaleColour = Color.Gray;
        UpdateStatus(@"Set KnobBackColour, PointerColour, and ScaleColour (legacy wrappers over StateCommon).");
    }

    private void kbtnPopulateFromTheme_Click(object? sender, EventArgs e)
    {
        kknobMain.StateCommon.PopulateFromBase(PaletteState.Normal);
        kknobMain.StateNormal.PopulateFromBase(PaletteState.Normal);
        kknobMain.StateDisabled.PopulateFromBase(PaletteState.Disabled);
        kknobMain.StateTracking.PopulateFromBase(PaletteState.Tracking);
        kknobMain.StatePressed.PopulateFromBase(PaletteState.Pressed);
        UpdateStatus(@"Repopulated palette states from the current global theme.");
    }

    private void kbtnIndustrialYellow_Click(object? sender, EventArgs e)
    {
        kknobMain.Backplate.ApplyYellowPanelPreset();
        kknobMain.KnobStyle = KnobStyle.Industrial;
        kknobMain.IndicatorShape = KnobIndicatorShape.Dot;
        kknobMain.StateCommon.Face.Color1 = Color.FromArgb(200, 20, 30);
        kknobMain.StateCommon.Face.Color2 = Color.FromArgb(120, 0, 10);
        kknobMain.StateCommon.Indicator.Color1 = Color.White;
        kknobMain.ShowLargeScale = false;
        kcmbIndicatorShape.SelectedItem = KnobIndicatorShape.Dot;
        UpdateStatus(@"Yellow rounded industrial panel with red knob and white dot indicator.");
    }

    private void kbtnIndustrialSilver_Click(object? sender, EventArgs e)
    {
        kknobMain.Backplate.ApplySilverPanelPreset();
        kknobMain.KnobStyle = KnobStyle.Industrial;
        kknobMain.IndicatorShape = KnobIndicatorShape.Bar;
        kknobMain.StateCommon.Face.Color1 = Color.FromArgb(190, 25, 35);
        kknobMain.StateCommon.Face.Color2 = Color.FromArgb(110, 0, 15);
        kknobMain.StateCommon.Indicator.Color1 = Color.White;
        kknobMain.ShowLargeScale = false;
        kcmbIndicatorShape.SelectedItem = KnobIndicatorShape.Bar;
        UpdateStatus(@"Silver square industrial panel with red knob and bar stripe indicator.");
    }

    private void kbtnIndustrialBlack_Click(object? sender, EventArgs e)
    {
        kknobMain.Backplate.ApplyBlackPanelPreset();
        kknobMain.KnobStyle = KnobStyle.Industrial;
        kknobMain.IndicatorShape = KnobIndicatorShape.GlowDot;
        kknobMain.StateCommon.Face.Color1 = Color.FromArgb(70, 70, 75);
        kknobMain.StateCommon.Face.Color2 = Color.FromArgb(25, 25, 28);
        kknobMain.StateCommon.Indicator.Color1 = Color.FromArgb(255, 40, 30);
        kknobMain.ShowLargeScale = false;
        kcmbIndicatorShape.SelectedItem = KnobIndicatorShape.GlowDot;
        UpdateStatus(@"Black circular industrial panel with matte knob and glowing red LED indicator.");
    }

    private void kbtnPlateRunStop_Click(object? sender, EventArgs e)
    {
        kknobMain.PlateLabels.ApplyRunStopPreset();
        UpdateStatus(@"Applied RUN / STOP plate labels at the knob arc endpoints.");
    }

    private void kbtnPlateOffOn_Click(object? sender, EventArgs e)
    {
        kknobMain.PlateLabels.ApplyOffOnPreset();
        UpdateStatus(@"Applied OFF / ON plate labels at the knob arc endpoints.");
    }
}
