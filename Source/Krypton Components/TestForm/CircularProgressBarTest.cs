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
/// Comprehensive demonstration of <see cref="KryptonCircularProgressBar"/> palette integration,
/// animation, tri-state thresholds, and layout options.
/// </summary>
public partial class CircularProgressBarTest : KryptonForm
{
    private readonly System.Windows.Forms.Timer _simulationTimer;
    private bool _simulationRunning;

    public CircularProgressBarTest()
    {
        InitializeComponent();

        _simulationTimer = new System.Windows.Forms.Timer { Interval = 80 };
        _simulationTimer.Tick += OnSimulationTick;
    }

    private void CircularProgressBarTest_Load(object? sender, EventArgs e)
    {
        Icon = SystemIcons.Application;

        PopulateComboFromEnum(kcmbStyle, typeof(ProgressBarStyle));
        kcmbStyle.SelectedItem = ProgressBarStyle.Continuous;

        PopulateComboFromEnum(kcmbValueBackColorStyle, typeof(PaletteColorStyle));
        kcmbValueBackColorStyle.SelectedItem = kcpbMain.ValueBackColorStyle;

        PopulateComboFromEnum(kcmbAnimationFunction, typeof(WinFormAnimation.KnownAnimationFunctions));
        kcmbAnimationFunction.SelectedItem = kcpbMain.AnimationFunction;

        kcpbMain.Value = ktrkValue.Value;
        kcpbDisabled.Value = 40;
        kcpbMarquee.Style = ProgressBarStyle.Marquee;

        knudMinimum.Value = kcpbMain.Minimum;
        knudMaximum.Value = kcpbMain.Maximum;
        knudAnimationSpeed.Value = kcpbMain.AnimationSpeed;
        knudMarqueeSpeed.Value = kcpbMarquee.MarqueeAnimationSpeed;

        txtSuperscript.Text = kcpbMain.SuperscriptText;
        txtSubscript.Text = kcpbMain.SubscriptText;
        kchkUseValueAsText.Checked = kcpbMain.UseValueAsText;
        kchkTriState.Checked = kcpbMain.TriStateValues.UseTriStateColors;
        kchkAutoThreshold.Checked = kcpbMain.TriStateValues.AutoCalculateThresholdValues;
        kchkEnabled.Checked = kcpbMain.Enabled;

        kryptonPropertyGrid1.SelectedObject = kcpbMain;

        UpdateValueLabel(kcpbMain.Value);
        UpdateStatusLabel();
        UpdateSimulationButtons();
    }

    private void CircularProgressBarTest_FormClosed(object? sender, FormClosedEventArgs e)
    {
        _simulationTimer.Stop();
    }

    private static void PopulateComboFromEnum(KryptonComboBox combo, Type enumType)
    {
        combo.Items.Clear();
        foreach (object value in Enum.GetValues(enumType))
        {
            combo.Items.Add(value);
        }
    }

    private void OnSimulationTick(object? sender, EventArgs e)
    {
        int next = kcpbMain.Value + kcpbMain.Step;
        if (next > kcpbMain.Maximum)
        {
            next = kcpbMain.Minimum;
        }

        kcpbMain.Value = next;
        ktrkValue.Value = Math.Max(ktrkValue.Minimum, Math.Min(ktrkValue.Maximum, next));
    }

    private void UpdateValueLabel(int value) => klblValue.Values.Text = $@"Value: {value} / {kcpbMain.Maximum}";

    private void UpdateStatusLabel()
    {
        klblStatus.Values.Text = _simulationRunning
            ? @"Simulation running — value advances automatically."
            : @"Adjust controls or use the property grid to explore palette states.";
    }

    private void ktrkValue_ValueChanged(object? sender, EventArgs e)
    {
        kcpbMain.Value = ktrkValue.Value;
        UpdateValueLabel(kcpbMain.Value);
    }

    private void kbtnIncrement_Click(object? sender, EventArgs e) => kcpbMain.Increment(kcpbMain.Step);

    private void kbtnDecrement_Click(object? sender, EventArgs e) => kcpbMain.Increment(-kcpbMain.Step);

    private void kbtnReset_Click(object? sender, EventArgs e)
    {
        StopSimulation();
        kcpbMain.Value = kcpbMain.Minimum;
        ktrkValue.Value = kcpbMain.Minimum;
        UpdateValueLabel(kcpbMain.Value);
    }

    private void kbtnStartSimulation_Click(object? sender, EventArgs e)
    {
        _simulationRunning = true;
        _simulationTimer.Start();
        UpdateStatusLabel();
        UpdateSimulationButtons();
    }

    private void kbtnStopSimulation_Click(object? sender, EventArgs e) => StopSimulation();

    private void StopSimulation()
    {
        _simulationRunning = false;
        _simulationTimer.Stop();
        UpdateStatusLabel();
        UpdateSimulationButtons();
    }

    private void UpdateSimulationButtons()
    {
        kbtnStartSimulation.Enabled = !_simulationRunning;
        kbtnStopSimulation.Enabled = _simulationRunning;
    }

    private void knudMinimum_ValueChanged(object? sender, EventArgs e)
    {
        kcpbMain.Minimum = (int)knudMinimum.Value;
        ktrkValue.Minimum = kcpbMain.Minimum;
        ktrkValue.Value = Math.Max(ktrkValue.Minimum, ktrkValue.Value);
    }

    private void knudMaximum_ValueChanged(object? sender, EventArgs e)
    {
        kcpbMain.Maximum = (int)knudMaximum.Value;
        ktrkValue.Maximum = kcpbMain.Maximum;
        ktrkValue.Value = Math.Min(ktrkValue.Maximum, ktrkValue.Value);
        UpdateValueLabel(kcpbMain.Value);
    }

    private void kcmbStyle_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kcmbStyle.SelectedItem is ProgressBarStyle style)
        {
            kcpbMain.Style = style;
        }
    }

    private void knudAnimationSpeed_ValueChanged(object? sender, EventArgs e)
    {
        kcpbMain.AnimationSpeed = (int)knudAnimationSpeed.Value;
    }

    private void kcmbAnimationFunction_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kcmbAnimationFunction.SelectedItem is WinFormAnimation.KnownAnimationFunctions function)
        {
            kcpbMain.AnimationFunction = function;
        }
    }

    private void knudMarqueeSpeed_ValueChanged(object? sender, EventArgs e)
    {
        kcpbMarquee.MarqueeAnimationSpeed = (int)knudMarqueeSpeed.Value;
    }

    private void txtSuperscript_TextChanged(object? sender, EventArgs e) => kcpbMain.SuperscriptText = txtSuperscript.Text;

    private void txtSubscript_TextChanged(object? sender, EventArgs e) => kcpbMain.SubscriptText = txtSubscript.Text;

    private void kchkUseValueAsText_CheckedChanged(object? sender, EventArgs e)
    {
        kcpbMain.UseValueAsText = kchkUseValueAsText.Checked;
        if (kchkUseValueAsText.Checked)
        {
            kcpbMain.Text = $@"{kcpbMain.Value}%";
        }
    }

    private void kchkTriState_CheckedChanged(object? sender, EventArgs e)
    {
        kcpbMain.TriStateValues.UseTriStateColors = kchkTriState.Checked;
    }

    private void kchkAutoThreshold_CheckedChanged(object? sender, EventArgs e)
    {
        kcpbMain.TriStateValues.AutoCalculateThresholdValues = kchkAutoThreshold.Checked;
    }

    private void kbtnTriStatePreset_Click(object? sender, EventArgs e)
    {
        kcpbMain.TriStateValues.UseTriStateColors = true;
        kcpbMain.TriStateValues.AutoCalculateThresholdValues = true;
        kcpbMain.TriStateValues.LowThresholdValues.StateCommon.Back.Color1 = Color.IndianRed;
        kcpbMain.TriStateValues.MediumThresholdValues.StateCommon.Back.Color1 = Color.Goldenrod;
        kcpbMain.TriStateValues.HighThresholdValues.StateCommon.Back.Color1 = Color.MediumSeaGreen;
        kchkTriState.Checked = true;
        kchkAutoThreshold.Checked = true;
        kcpbMain.Value = 80;
        ktrkValue.Value = 80;
    }

    private void kcmbValueBackColorStyle_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kcmbValueBackColorStyle.SelectedItem is PaletteColorStyle style)
        {
            kcpbMain.ValueBackColorStyle = style;
        }
    }

    private void kcbtnProgressColor_SelectedColorChanged(object? sender, ColorEventArgs e)
    {
        kcpbMain.StateCommon.Back.Color1 = e.Color;
    }

    private void kcbtnOuterRingColor_SelectedColorChanged(object? sender, ColorEventArgs e)
    {
        kcpbMain.OuterRingStateCommon.Back.Color1 = e.Color;
    }

    private void kcbtnInnerRingColor_SelectedColorChanged(object? sender, ColorEventArgs e)
    {
        kcpbMain.InnerRingStateCommon.Back.Color1 = e.Color;
    }

    private void kcbtnMainTextColor_SelectedColorChanged(object? sender, ColorEventArgs e)
    {
        kcpbMain.StateNormal.Content.ShortText.Color1 = e.Color;
    }

    private void kchkEnabled_CheckedChanged(object? sender, EventArgs e)
    {
        kcpbMain.Enabled = kchkEnabled.Checked;
    }

    private void kbtnSelectMainInGrid_Click(object? sender, EventArgs e) => kryptonPropertyGrid1.SelectedObject = kcpbMain;

    private void kbtnSelectMarqueeInGrid_Click(object? sender, EventArgs e) => kryptonPropertyGrid1.SelectedObject = kcpbMarquee;

    private void kbtnSelectDisabledInGrid_Click(object? sender, EventArgs e) => kryptonPropertyGrid1.SelectedObject = kcpbDisabled;
}
