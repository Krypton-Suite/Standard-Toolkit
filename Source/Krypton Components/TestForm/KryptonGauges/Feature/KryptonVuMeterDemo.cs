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
/// Exercises <see cref="KryptonVuMeter"/> analog and segmented faces, manual level, peak hold, and capture (issue #4655).
/// </summary>
public partial class KryptonVuMeterDemo : KryptonForm
{
    private bool _sync;

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonVuMeterDemo"/> class.
    /// </summary>
    public KryptonVuMeterDemo()
    {
        InitializeComponent();
    }

    private void KryptonVuMeterDemo_Load(object? sender, EventArgs e)
    {
        Icon = SystemIcons.Application;
        ktrkLevel.Minimum = -20;
        ktrkLevel.Maximum = 3;
        ktrkLevel.Value = -20;

        kcmbChannel.Items.AddRange(Enum.GetNames(typeof(VuMeterChannel)));
        kcmbChannel.SelectedItem = VuMeterChannel.Left.ToString();
        kcmbOrientation.Items.AddRange(Enum.GetNames(typeof(VisualOrientation)));
        kcmbOrientation.SelectedItem = VisualOrientation.Left.ToString();

        kvumDisabled.Value = 0d;
        kvumDisabled.Values.PeakHold = true;
        kpgMeter.SelectedObject = kvumAnalog;
        UpdateLevelLabel();
    }

    private void ktrkLevel_ValueChanged(object? sender, EventArgs e)
    {
        if (_sync || kchkCapture.Checked)
        {
            return;
        }

        _sync = true;
        kvumAnalog.Value = ktrkLevel.Value;
        kvumSegments.Value = ktrkLevel.Value;
        UpdateLevelLabel();
        _sync = false;
    }

    private void kvumAnalog_ValueChanged(object? sender, VuMeterValueChangedEventArgs e)
    {
        if (_sync)
        {
            return;
        }

        _sync = true;
        if (kchkCapture.Checked)
        {
            kvumSegments.Value = e.Value;
        }

        int stepped = (int)Math.Round(e.Value);
        if (!kchkCapture.Checked && stepped >= ktrkLevel.Minimum && stepped <= ktrkLevel.Maximum && ktrkLevel.Value != stepped)
        {
            ktrkLevel.Value = stepped;
        }

        UpdateLevelLabel();
        _sync = false;
    }

    private void kchkPeak_CheckedChanged(object? sender, EventArgs e)
    {
        kvumAnalog.Values.PeakHold = kchkPeak.Checked;
        kvumSegments.Values.PeakHold = kchkPeak.Checked;
    }

    private void kchkCapture_CheckedChanged(object? sender, EventArgs e)
    {
        ktrkLevel.Enabled = !kchkCapture.Checked;
        kcmbChannel.Enabled = kchkCapture.Checked;
        kvumAnalog.Values.InputMode = kchkCapture.Checked ? VuMeterInputMode.Capture : VuMeterInputMode.Manual;
        if (!kchkCapture.Checked)
        {
            kvumAnalog.Value = ktrkLevel.Value;
            kvumSegments.Value = ktrkLevel.Value;
        }
    }

    private void kvumAnalog_CaptureFailed(object? sender, VuMeterCaptureFailedEventArgs e)
    {
        kchkCapture.Checked = false;
        klblLevel.Values.Text = e.Message;
    }

    private void kcmbChannel_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kcmbChannel.SelectedItem is string name && Enum.TryParse(name, out VuMeterChannel channel))
        {
            kvumAnalog.Values.Channel = channel;
        }
    }

    private void kcmbOrientation_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kcmbOrientation.SelectedItem is string name && Enum.TryParse(name, out VisualOrientation orientation))
        {
            kvumSegments.Values.Orientation = orientation;
        }
    }

    private void kbtnSilence_Click(object? sender, EventArgs e)
    {
        kchkCapture.Checked = false;
        ktrkLevel.Value = ktrkLevel.Minimum;
    }

    private void kbtnZero_Click(object? sender, EventArgs e)
    {
        kchkCapture.Checked = false;
        ktrkLevel.Value = 0;
    }

    private void UpdateLevelLabel()
    {
        klblLevel.Values.Text = "Target " + kvumAnalog.Value.ToString("0.0", CultureInfo.CurrentCulture) + " VU";
    }
}
