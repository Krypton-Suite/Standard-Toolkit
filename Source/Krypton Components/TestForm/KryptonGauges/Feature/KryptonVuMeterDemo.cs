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
/// Exercises <see cref="KryptonVuMeter"/> analog and segmented faces, manual level, a music loop, a stereo file, peak hold, and capture (issue #4655).
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
        if (_sync || kchkCapture.Checked || Playing)
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
        if (!kchkCapture.Checked && !Playing && stepped >= ktrkLevel.Minimum && stepped <= ktrkLevel.Maximum && ktrkLevel.Value != stepped)
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
        if (_sync)
        {
            return;
        }

        kcmbChannel.Enabled = kchkCapture.Checked;
        kvumAnalog.Values.InputMode = kchkCapture.Checked ? VuMeterInputMode.Capture : VuMeterInputMode.Manual;
        if (!kchkCapture.Checked)
        {
            kvumAnalog.Value = ktrkLevel.Value;
            kvumSegments.Value = ktrkLevel.Value;
        }

        ApplyTransport();
    }

    private void kchkMusic_CheckedChanged(object? sender, EventArgs e)
    {
        if (_sync)
        {
            return;
        }

        if (kchkMusic.Checked)
        {
            if (kchkCapture.Checked)
            {
                _sync = true;
                kchkCapture.Checked = false;
                _sync = false;
            }

            kvumAnalog.Values.InputMode = VuMeterInputMode.Manual;
            if (kchkStereo.Checked)
            {
                _sync = true;
                kchkStereo.Checked = false;
                _sync = false;
            }

            SetStereoCaptions(false);
            if (!StartMusic())
            {
                _sync = true;
                kchkMusic.Checked = false;
                _sync = false;
            }
        }
        else
        {
            StopMusic();
            RestoreSliderLevel();
        }

        ApplyTransport();
    }

    private void kchkStereo_CheckedChanged(object? sender, EventArgs e)
    {
        if (_sync)
        {
            return;
        }

        if (kchkStereo.Checked)
        {
            if (kchkCapture.Checked)
            {
                _sync = true;
                kchkCapture.Checked = false;
                _sync = false;
            }

            if (kchkMusic.Checked)
            {
                _sync = true;
                kchkMusic.Checked = false;
                _sync = false;
            }

            kvumAnalog.Values.InputMode = VuMeterInputMode.Manual;
            if (!StartStereo())
            {
                _sync = true;
                kchkStereo.Checked = false;
                _sync = false;
                SetStereoCaptions(false);
            }
            else
            {
                SetStereoCaptions(true);
            }
        }
        else
        {
            StopMusic();
            SetStereoCaptions(false);
            RestoreSliderLevel();
        }

        ApplyTransport();
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
        kchkMusic.Checked = false;
        kchkStereo.Checked = false;
        kchkCapture.Checked = false;
        ktrkLevel.Value = ktrkLevel.Minimum;
    }

    private void kbtnZero_Click(object? sender, EventArgs e)
    {
        kchkMusic.Checked = false;
        kchkStereo.Checked = false;
        kchkCapture.Checked = false;
        ktrkLevel.Value = 0;
    }

    /// <summary>
    /// Stop playback when the demo closes.
    /// </summary>
    /// <param name="e">Form closed arguments.</param>
    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        StopMusic();
        base.OnFormClosed(e);
    }

    private bool Playing => kchkMusic.Checked || kchkStereo.Checked;

    private void ApplyTransport()
    {
        ktrkLevel.Enabled = !kchkCapture.Checked && !Playing;
        kchkMusic.Enabled = !kchkCapture.Checked && !kchkStereo.Checked;
        kchkStereo.Enabled = !kchkCapture.Checked && !kchkMusic.Checked;
        kchkCapture.Enabled = !Playing;
    }

    private void RestoreSliderLevel()
    {
        if (kchkCapture.Checked)
        {
            return;
        }

        _sync = true;
        kvumAnalog.Value = ktrkLevel.Value;
        kvumSegments.Value = ktrkLevel.Value;
        UpdateLevelLabel();
        _sync = false;
    }

    private void SetStereoCaptions(bool stereo)
    {
        klblAnalog.Values.Text = stereo ? "Left channel" : "Analog";
        klblSegments.Values.Text = stereo ? "Right channel" : "Segments";
    }

    private void UpdateLevelLabel()
    {
        if (kchkStereo.Checked)
        {
            klblLevel.Values.Text = "Left " + kvumAnalog.Value.ToString("0.0", CultureInfo.CurrentCulture)
                + " / Right " + kvumSegments.Value.ToString("0.0", CultureInfo.CurrentCulture) + " VU";
            return;
        }

        klblLevel.Values.Text = "Target " + kvumAnalog.Value.ToString("0.0", CultureInfo.CurrentCulture) + " VU";
    }
}
