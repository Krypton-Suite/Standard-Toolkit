#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Gauges;

/// <summary>
/// Plays the shared <see cref="KryptonGenericMusic"/> loop and drives one or more
/// <see cref="KryptonEqualizer"/> controls from an FFT of the PCM.
/// </summary>
public sealed class EqualizerMusicPlayback : IDisposable
{
    private const int SampleRate = 22050;
    private const int TickMilliseconds = 40;

    private readonly List<KryptonEqualizer> _targets = new List<KryptonEqualizer>();
    private readonly double[] _real = new double[EqualizerFft.WindowSize];
    private readonly double[] _imag = new double[EqualizerFft.WindowSize];
    private readonly double[] _levels = new double[EqualizerScale.MaximumBandCount];
    private SoundPlayer? _player;
    private MemoryStream? _stream;
    private System.Windows.Forms.Timer? _timer;
    private short[]? _pcm;
    private int _started;
    private bool _disposed;

    /// <summary>
    /// Gets whether music is currently playing.
    /// </summary>
    public bool IsPlaying => _player != null;

    /// <summary>
    /// Registers an equalizer that receives spectrum levels while music plays.
    /// </summary>
    /// <param name="equalizer">Target equalizer.</param>
    public void AddTarget(KryptonEqualizer equalizer)
    {
        if (equalizer == null || _targets.Contains(equalizer))
        {
            return;
        }

        _targets.Add(equalizer);
    }

    /// <summary>
    /// Starts the generated music loop and begins pushing FFT band levels.
    /// </summary>
    /// <param name="seconds">Loop length in seconds.</param>
    /// <param name="bass">Bass amount from 0 to 100 (50 is the normal mix).</param>
    /// <returns><see langword="true"/> when playback started.</returns>
    public bool Start(int seconds = 60, int bass = 50)
    {
        if (_disposed || _targets.Count == 0)
        {
            return false;
        }

        Stop();
        try
        {
            KryptonGenericMusic.BuildMusic(seconds, bass, out byte[] wav, out _);
            _pcm = DecodeMonoPcm16(wav);
            if (_pcm == null || _pcm.Length < EqualizerFft.WindowSize)
            {
                return false;
            }

            _stream = new MemoryStream(wav, false);
            _player = new SoundPlayer(_stream);
            _player.Load();
            _player.PlayLooping();
            _started = Environment.TickCount;
            _timer = new System.Windows.Forms.Timer
            {
                Interval = TickMilliseconds
            };
            _timer.Tick += OnTick;
            _timer.Start();
            return true;
        }
        catch
        {
            Stop();
            return false;
        }
    }

    /// <summary>
    /// Stops playback and releases the player.
    /// </summary>
    public void Stop()
    {
        if (_timer != null)
        {
            _timer.Stop();
            _timer.Tick -= OnTick;
            _timer.Dispose();
            _timer = null;
        }

        if (_player != null)
        {
            _player.Stop();
            _player.Dispose();
            _player = null;
        }

        if (_stream != null)
        {
            _stream.Dispose();
            _stream = null;
        }

        _pcm = null;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        Stop();
        _targets.Clear();
    }

    private void OnTick(object? sender, EventArgs e)
    {
        if (_pcm == null || _targets.Count == 0)
        {
            return;
        }

        int elapsed = Environment.TickCount - _started;
        if (elapsed < 0)
        {
            elapsed = 0;
        }

        long sampleIndex = (long)elapsed * SampleRate / 1000L;
        int origin = (int)(sampleIndex % Math.Max(1, _pcm.Length - EqualizerFft.WindowSize));
        for (int i = 0; i < EqualizerFft.WindowSize; i++)
        {
            _real[i] = _pcm[origin + i] / 32768d;
            _imag[i] = 0d;
        }

        EqualizerFft.ApplyHann(_real, EqualizerFft.WindowSize);
        EqualizerFft.Forward(_real, _imag, EqualizerFft.WindowSize);

        for (int t = 0; t < _targets.Count; t++)
        {
            KryptonEqualizer equalizer = _targets[t];
            if (equalizer.IsDisposed || equalizer.Values.InputMode == EqualizerInputMode.Capture)
            {
                continue;
            }

            double[] frequencies = equalizer.Values.CopyFrequencies();
            EqualizerFft.MapToBands(_real, _imag, EqualizerFft.WindowSize, SampleRate,
                frequencies, equalizer.Values.CalibrationOffsetDb, _levels);
            var copy = new double[frequencies.Length];
            Array.Copy(_levels, copy, frequencies.Length);
            equalizer.SetLevels(copy);
        }
    }

    private static short[]? DecodeMonoPcm16(byte[] wav)
    {
        if (wav == null || wav.Length < 44)
        {
            return null;
        }

        int channels = BitConverter.ToInt16(wav, 22);
        int bits = BitConverter.ToInt16(wav, 34);
        if (channels != 1 || bits != 16)
        {
            return null;
        }

        int dataBytes = BitConverter.ToInt32(wav, 40);
        int sampleCount = dataBytes / 2;
        if (sampleCount <= 0 || 44 + dataBytes > wav.Length)
        {
            return null;
        }

        var pcm = new short[sampleCount];
        Buffer.BlockCopy(wav, 44, pcm, 0, dataBytes);
        return pcm;
    }
}
