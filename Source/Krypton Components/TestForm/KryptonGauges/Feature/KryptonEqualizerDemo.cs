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
/// Exercises <see cref="KryptonEqualizer"/> bars and segments, manual levels, generated music + FFT,
/// gain thumbs, peak hold, and capture (issue #4660).
/// </summary>
public partial class KryptonEqualizerDemo : KryptonForm
{
    private readonly System.Windows.Forms.Timer _simTimer;
    private readonly EqualizerMusicPlayback _music = new EqualizerMusicPlayback();
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
        _music.AddTarget(keqBars);
        _music.AddTarget(keqSegments);
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
        UpdateTransportEnabled();
        UpdateStatus();
    }

    private void KryptonEqualizerDemo_FormClosed(object? sender, FormClosedEventArgs e)
    {
        _simTimer.Stop();
        _simTimer.Dispose();
        _music.Dispose();
    }

    private void kchkSimulate_CheckedChanged(object? sender, EventArgs e)
    {
        if (kchkSimulate.Checked)
        {
            kchkMusic.Checked = false;
            kchkCapture.Checked = false;
            _music.Stop();
            _simTimer.Start();
        }
        else
        {
            _simTimer.Stop();
        }

        UpdateTransportEnabled();
        UpdateStatus();
    }

    private void kchkMusic_CheckedChanged(object? sender, EventArgs e)
    {
        if (kchkMusic.Checked)
        {
            kchkSimulate.Checked = false;
            kchkCapture.Checked = false;
            _simTimer.Stop();
            if (!_music.Start(60, 50))
            {
                kchkMusic.Checked = false;
                klblStatus.Text = "Music could not start.";
            }
        }
        else
        {
            _music.Stop();
        }

        UpdateTransportEnabled();
        UpdateStatus();
    }

    private void kchkCapture_CheckedChanged(object? sender, EventArgs e)
    {
        if (kchkCapture.Checked)
        {
            kchkSimulate.Checked = false;
            kchkMusic.Checked = false;
            _simTimer.Stop();
            _music.Stop();
        }

        EqualizerInputMode mode = kchkCapture.Checked ? EqualizerInputMode.Capture : EqualizerInputMode.Manual;
        keqBars.Values.InputMode = mode;
        keqSegments.Values.InputMode = mode;
        UpdateTransportEnabled();
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
        kchkMusic.Checked = false;
        kchkCapture.Checked = false;
        _simTimer.Stop();
        _music.Stop();
        keqBars.SetLevels(new double[keqBars.Values.BandCount]);
        keqSegments.SetLevels(new double[keqSegments.Values.BandCount]);
        UpdateTransportEnabled();
        UpdateStatus();
    }

    private void keqBars_GainChanged(object? sender, EqualizerGainChangedEventArgs e) => UpdateStatus();

    private void keqBars_CaptureFailed(object? sender, EqualizerCaptureFailedEventArgs e)
    {
        kchkCapture.Checked = false;
        klblStatus.Text = "Capture failed: " + e.Message;
        UpdateTransportEnabled();
    }

    private void OnSimulateTick(object? sender, EventArgs e)
    {
        if (kchkCapture.Checked || kchkMusic.Checked)
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

    private void UpdateTransportEnabled()
    {
        bool playingMusic = kchkMusic.Checked;
        bool capturing = kchkCapture.Checked;
        bool simulating = kchkSimulate.Checked;
        kchkSimulate.Enabled = !playingMusic && !capturing;
        kchkMusic.Enabled = !simulating && !capturing;
        kchkCapture.Enabled = !playingMusic && !simulating;
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
        string source = kchkMusic.Checked
            ? "Playing generated music (FFT)"
            : kchkSimulate.Checked
                ? "Synthetic simulate"
                : kchkCapture.Checked
                    ? "Capture"
                    : "Manual";
        klblStatus.Text = source + " | Bands " + keqBars.Values.BandCount +
                          " | Mid-band gain " + gainText +
                          " | Drag thumbs to change gain.";
        _sync = false;
    }
}
