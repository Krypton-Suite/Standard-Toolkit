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
/// Exercises <see cref="KryptonEqualizer"/> bars and segments, manual levels, gain thumbs, peak hold, and capture (issue #4660).
/// </summary>
public partial class KryptonEqualizerDemo : KryptonForm
{
    private readonly System.Windows.Forms.Timer _simTimer;
    private readonly Random _random = new Random(4660);
    private bool _sync;

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonEqualizerDemo"/> class.
    /// </summary>
    public KryptonEqualizerDemo()
    {
        InitializeComponent();
        _simTimer = new System.Windows.Forms.Timer
        {
            Interval = 40
        };
        _simTimer.Tick += OnSimulateTick;
    }

    private void KryptonEqualizerDemo_Load(object? sender, EventArgs e)
    {
        Icon = SystemIcons.Application;
        kcmbChannel.Items.AddRange(Enum.GetNames(typeof(VuMeterChannel)));
        kcmbChannel.SelectedItem = nameof(VuMeterChannel.Mix);
        kcmbOrientation.Items.AddRange(Enum.GetNames(typeof(VisualOrientation)));
        kcmbOrientation.SelectedItem = nameof(VisualOrientation.Top);
        kcmbBands.Items.AddRange(new object[] { "5", "8", "10", "12", "16", "20" });
        kcmbBands.SelectedItem = "10";
        keqDisabled.Enabled = false;
        keqDisabled.Values.PeakHold = true;
        SeedDemoLevels(keqBars);
        SeedDemoLevels(keqSegments);
        SeedDemoLevels(keqDisabled);
        kpgEqualizer.SelectedObject = keqBars;
        UpdateStatus();
    }

    private void KryptonEqualizerDemo_FormClosed(object? sender, FormClosedEventArgs e)
    {
        _simTimer.Stop();
        _simTimer.Dispose();
    }

    private void kchkSimulate_CheckedChanged(object? sender, EventArgs e)
    {
        if (kchkSimulate.Checked)
        {
            kchkCapture.Checked = false;
            _simTimer.Start();
        }
        else
        {
            _simTimer.Stop();
        }
    }

    private void kchkCapture_CheckedChanged(object? sender, EventArgs e)
    {
        if (kchkCapture.Checked)
        {
            kchkSimulate.Checked = false;
            _simTimer.Stop();
        }

        EqualizerInputMode mode = kchkCapture.Checked ? EqualizerInputMode.Capture : EqualizerInputMode.Manual;
        keqBars.Values.InputMode = mode;
        keqSegments.Values.InputMode = mode;
        UpdateStatus();
    }

    private void kchkPeak_CheckedChanged(object? sender, EventArgs e)
    {
        keqBars.Values.PeakHold = kchkPeak.Checked;
        keqSegments.Values.PeakHold = kchkPeak.Checked;
        keqDisabled.Values.PeakHold = kchkPeak.Checked;
    }

    private void kchkInteractive_CheckedChanged(object? sender, EventArgs e)
    {
        keqBars.Values.Interactive = kchkInteractive.Checked;
        keqSegments.Values.Interactive = kchkInteractive.Checked;
    }

    private void kchkSegments_CheckedChanged(object? sender, EventArgs e)
    {
        keqBars.Values.Style = kchkSegments.Checked ? EqualizerStyle.Segments : EqualizerStyle.Bars;
        keqSegments.Values.Style = EqualizerStyle.Segments;
    }

    private void kcmbChannel_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kcmbChannel.SelectedItem is string name && Enum.TryParse(name, out VuMeterChannel channel))
        {
            keqBars.Values.Channel = channel;
            keqSegments.Values.Channel = channel;
        }
    }

    private void kcmbOrientation_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kcmbOrientation.SelectedItem is string name && Enum.TryParse(name, out VisualOrientation orientation))
        {
            keqBars.Values.Orientation = orientation;
            keqSegments.Values.Orientation = orientation;
        }
    }

    private void kcmbBands_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kcmbBands.SelectedItem is string text && int.TryParse(text, out int count))
        {
            keqBars.Values.BandCount = count;
            keqSegments.Values.BandCount = count;
            keqDisabled.Values.BandCount = count;
            SeedDemoLevels(keqDisabled);
        }
    }

    private void kbtnResetGains_Click(object? sender, EventArgs e)
    {
        foreach (EqualizerBand band in keqBars.Values.Bands)
        {
            band.Gain = 0d;
        }

        foreach (EqualizerBand band in keqSegments.Values.Bands)
        {
            band.Gain = 0d;
        }

        UpdateStatus();
    }

    private void kbtnSilence_Click(object? sender, EventArgs e)
    {
        kchkSimulate.Checked = false;
        kchkCapture.Checked = false;
        keqBars.SetLevels(new double[keqBars.Values.BandCount]);
        keqSegments.SetLevels(new double[keqSegments.Values.BandCount]);
        UpdateStatus();
    }

    private void keqBars_GainChanged(object? sender, EqualizerGainChangedEventArgs e) => UpdateStatus();

    private void keqBars_CaptureFailed(object? sender, EqualizerCaptureFailedEventArgs e)
    {
        kchkCapture.Checked = false;
        klblStatus.Text = "Capture failed: " + e.Message;
    }

    private void OnSimulateTick(object? sender, EventArgs e)
    {
        if (kchkCapture.Checked)
        {
            return;
        }

        double[] levels = new double[keqBars.Values.BandCount];
        double t = Environment.TickCount / 220d;
        for (int i = 0; i < levels.Length; i++)
        {
            double wave = 0.35d + (0.45d * Math.Abs(Math.Sin(t + (i * 0.55d))));
            double noise = 0.08d * _random.NextDouble();
            levels[i] = Math.Min(1d, wave + noise);
        }

        keqBars.SetLevels(levels);
        keqSegments.SetLevels(levels);
    }

    private static void SeedDemoLevels(KryptonEqualizer equalizer)
    {
        double[] levels = new double[equalizer.Values.BandCount];
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i] = 0.15d + (0.55d * (i / (double)Math.Max(1, levels.Length - 1)));
        }

        equalizer.SetLevels(levels);
    }

    private void UpdateStatus()
    {
        if (_sync)
        {
            return;
        }

        _sync = true;
        double[] gains = keqBars.GetGains();
        int mid = gains.Length == 0 ? 0 : gains.Length / 2;
        string gainText = gains.Length == 0
            ? "n/a"
            : gains[mid].ToString("0.0", System.Globalization.CultureInfo.CurrentCulture) + " dB";
        klblStatus.Text = "Bands " + keqBars.Values.BandCount +
                          " | Mid-band gain " + gainText +
                          " | Drag thumbs to change gain. Capture updates spectrum only.";
        _sync = false;
    }
}
