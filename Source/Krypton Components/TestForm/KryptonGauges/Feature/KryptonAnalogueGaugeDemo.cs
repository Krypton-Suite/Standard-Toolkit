#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Gauges;

namespace TestForm;

/// <summary>
/// Exercises circular, linear, rectangular, thermometer, and ampere-meter analogue gauges with shared ranges, animation, and interactive input (issue #4656).
/// </summary>
public partial class KryptonAnalogueGaugeDemo : KryptonForm
{
    private bool _sync;

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonAnalogueGaugeDemo"/> class.
    /// </summary>
    public KryptonAnalogueGaugeDemo()
    {
        InitializeComponent();
    }

    private void KryptonAnalogueGaugeDemo_Load(object? sender, EventArgs e)
    {
        Icon = SystemIcons.Application;
        ConfigureSharedRanges(kcgMain.CircularValues);
        ConfigureSharedRanges(klgMain.LinearValues);
        ConfigureSharedRanges(krgMain.RectangularValues);
        ConfigureSharedRanges(krgSegments.RectangularValues);
        ConfigureSharedRanges(kthmMain.ThermometerValues);

        kcgMain.CircularValues.Caption = "RPM";
        kcgMain.CircularValues.Units = "x1000";
        kcgMain.CircularValues.ShowPeak = true;
        kcgMain.Value = 45d;

        klgMain.LinearValues.Caption = "Torque";
        klgMain.LinearValues.Units = "Nm";
        klgMain.Value = 45d;

        krgMain.RectangularValues.Caption = "Load";
        krgMain.RectangularValues.Units = "%";
        krgMain.Value = 45d;

        krgSegments.RectangularValues.Style = RectangularGaugeStyle.Segmented;
        krgSegments.RectangularValues.Caption = "Segments";
        krgSegments.RectangularValues.ShowValueText = false;
        krgSegments.Value = 45d;

        kthmMain.ThermometerValues.Caption = "Temp";
        kthmMain.ThermometerValues.Units = "C";
        kthmMain.Value = 45d;

        kamAc.AmpereMeterValues.Caption = "AC";
        kamAc.AmpereMeterValues.Minimum = 0d;
        kamAc.AmpereMeterValues.Maximum = 10d;
        kamAc.AmpereMeterValues.Ranges.Clear();
        kamAc.AmpereMeterValues.Ranges.Add(new GaugeRange(7d, 8.5d, GaugeRangeSeverity.Warning) { Name = "Warning" });
        kamAc.AmpereMeterValues.Ranges.Add(new GaugeRange(8.5d, 10d, GaugeRangeSeverity.Error) { Name = "Error" });
        kamAc.Value = 4.5d;

        kamDc.AmpereMeterValues.Caption = "DC";
        kamDc.AmpereMeterValues.Minimum = -10d;
        kamDc.AmpereMeterValues.Maximum = 10d;
        kamDc.AmpereMeterValues.Ranges.Clear();
        kamDc.AmpereMeterValues.Ranges.Add(new GaugeRange(-10d, -7d, GaugeRangeSeverity.Error) { Name = "Neg" });
        kamDc.AmpereMeterValues.Ranges.Add(new GaugeRange(7d, 10d, GaugeRangeSeverity.Error) { Name = "Pos" });
        kamDc.Value = -1d;

        kcgInteractive.CircularValues.Caption = "Interactive";
        kcgInteractive.CircularValues.InputMode = GaugeInputMode.Interactive;
        kcgInteractive.Value = 30d;

        ktrkValue.Value = 45;
        kchkAnimate.Checked = true;
        kchkInteractive.Checked = false;
        klblValue.Text = "Value: 45";
        kpgGauge.SelectedObject = kcgMain;
    }

    private static void ConfigureSharedRanges(GaugeValues values)
    {
        values.Minimum = 0d;
        values.Maximum = 100d;
        values.Ranges.Clear();
        values.Ranges.Add(new GaugeRange(70d, 85d, GaugeRangeSeverity.Warning) { Name = "Warning" });
        values.Ranges.Add(new GaugeRange(85d, 100d, GaugeRangeSeverity.Error) { Name = "Error" });
    }

    private void ktrkValue_ValueChanged(object? sender, EventArgs e)
    {
        if (_sync)
        {
            return;
        }

        _sync = true;
        double value = ktrkValue.Value;
        kcgMain.Value = value;
        klgMain.Value = value;
        krgMain.Value = value;
        krgSegments.Value = value;
        kthmMain.Value = value;
        // Trackbar 0..100 → AC 0..10 A and DC −10..+10 A.
        kamAc.Value = value / 10d;
        kamDc.Value = (value - 50d) / 5d;
        if (!kchkInteractive.Checked)
        {
            kcgInteractive.Value = value;
        }

        klblValue.Text = "Value: " + value.ToString(CultureInfo.CurrentCulture);
        _sync = false;
    }

    private void OnGaugeValueChanged(object? sender, GaugeValueChangedEventArgs e)
    {
        if (_sync || sender != kcgInteractive)
        {
            return;
        }

        _sync = true;
        int rounded = (int)Math.Round(e.NewValue);
        if (ktrkValue.Value != rounded)
        {
            ktrkValue.Value = Math.Max(ktrkValue.Minimum, Math.Min(ktrkValue.Maximum, rounded));
        }

        klblValue.Text = "Value: " + e.NewValue.ToString("0.##", CultureInfo.CurrentCulture);
        _sync = false;
    }

    private void kchkAnimate_CheckedChanged(object? sender, EventArgs e)
    {
        bool animate = kchkAnimate.Checked;
        kcgMain.Values.Animate = animate;
        klgMain.Values.Animate = animate;
        krgMain.Values.Animate = animate;
        krgSegments.Values.Animate = animate;
        kthmMain.Values.Animate = animate;
        kamAc.Values.Animate = animate;
        kamDc.Values.Animate = animate;
        kcgInteractive.Values.Animate = animate;
    }

    private void kchkInteractive_CheckedChanged(object? sender, EventArgs e)
    {
        GaugeInputMode mode = kchkInteractive.Checked ? GaugeInputMode.Interactive : GaugeInputMode.Manual;
        kcgMain.Values.InputMode = mode;
        klgMain.Values.InputMode = mode;
        krgMain.Values.InputMode = mode;
        krgSegments.Values.InputMode = mode;
        kthmMain.Values.InputMode = mode;
        kamAc.Values.InputMode = mode;
        kamDc.Values.InputMode = mode;
        // Dedicated interactive sample stays interactive so the drag/keyboard path is always available.
        kcgInteractive.Values.InputMode = GaugeInputMode.Interactive;
    }

    private void kbtnSelectCircular_Click(object? sender, EventArgs e) => kpgGauge.SelectedObject = kcgMain;

    private void kbtnSelectLinear_Click(object? sender, EventArgs e) => kpgGauge.SelectedObject = klgMain;

    private void kbtnSelectRectangular_Click(object? sender, EventArgs e) => kpgGauge.SelectedObject = krgMain;

    private void kbtnSelectSegments_Click(object? sender, EventArgs e) => kpgGauge.SelectedObject = krgSegments;

    private void kbtnSelectThermometer_Click(object? sender, EventArgs e) => kpgGauge.SelectedObject = kthmMain;

    private void kbtnSelectAmpereAc_Click(object? sender, EventArgs e) => kpgGauge.SelectedObject = kamAc;

    private void kbtnSelectAmpereDc_Click(object? sender, EventArgs e) => kpgGauge.SelectedObject = kamDc;
}
