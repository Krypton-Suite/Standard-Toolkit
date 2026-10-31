#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.IO;
using System.Media;

namespace TestForm;

partial class KryptonVuMeterDemo
{
    private const int MusicRate = 22050;
    private const int MusicWindow = 1102;
    private const int MusicEighths = 32;
    private const int MusicEighth = 5510;

    private SoundPlayer? _musicPlayer;
    private MemoryStream? _musicStream;
    private Timer? _musicTimer;
    private byte[]? _musicWav;
    private double[]? _musicLevels;
    private byte[]? _stereoWav;
    private double[]? _stereoLeft;
    private double[]? _stereoRight;
    private double[]? _playLeft;
    private double[]? _playRight;
    private int _musicStarted;

    /// <summary>
    /// Play the generated loop and move both meters from its level.
    /// </summary>
    /// <returns><see langword="true"/> when playback started.</returns>
    private bool StartMusic()
    {
        StopMusic();
        try
        {
            if (_musicWav == null || _musicLevels == null)
            {
                BuildMusic(out byte[] wav, out double[] levels);
                _musicWav = wav;
                _musicLevels = levels;
            }

            _playLeft = _musicLevels;
            _playRight = _musicLevels;
            PlayWav(_musicWav);
            return true;
        }
        catch (Exception ex)
        {
            StopMusic();
            klblLevel.Values.Text = ex.Message;
            return false;
        }
    }

    /// <summary>
    /// Play the stereo file. The analog meter follows the left channel and the segments follow the right.
    /// </summary>
    /// <returns><see langword="true"/> when playback started.</returns>
    private bool StartStereo()
    {
        StopMusic();
        try
        {
            if (_stereoWav == null || _stereoLeft == null || _stereoRight == null)
            {
                BuildStereo(out byte[] wav, out double[] left, out double[] right);
                _stereoWav = wav;
                _stereoLeft = left;
                _stereoRight = right;
            }

            _playLeft = _stereoLeft;
            _playRight = _stereoRight;
            PlayWav(_stereoWav);
            return true;
        }
        catch (Exception ex)
        {
            StopMusic();
            klblLevel.Values.Text = ex.Message;
            return false;
        }
    }

    private void PlayWav(byte[] wav)
    {
        _musicStream = new MemoryStream(wav, false);
        _musicPlayer = new SoundPlayer(_musicStream);
        _musicPlayer.Load();
        _musicPlayer.PlayLooping();
        _musicStarted = Environment.TickCount;
        _musicTimer = new Timer { Interval = 50 };
        _musicTimer.Tick += OnMusicTick;
        _musicTimer.Start();
    }

    /// <summary>
    /// Stop the loop and release the player.
    /// </summary>
    private void StopMusic()
    {
        if (_musicTimer != null)
        {
            _musicTimer.Stop();
            _musicTimer.Tick -= OnMusicTick;
            _musicTimer.Dispose();
            _musicTimer = null;
        }

        if (_musicPlayer != null)
        {
            _musicPlayer.Stop();
            _musicPlayer.Dispose();
            _musicPlayer = null;
        }

        if (_musicStream != null)
        {
            _musicStream.Dispose();
            _musicStream = null;
        }
    }

    private void OnMusicTick(object? sender, EventArgs e)
    {
        if (_playLeft == null || _playRight == null || _playLeft.Length == 0 || _playRight.Length == 0)
        {
            return;
        }

        int elapsed = Environment.TickCount - _musicStarted;
        if (elapsed < 0)
        {
            elapsed = 0;
        }

        int window = elapsed / 50;
        _sync = true;
        kvumAnalog.Value = _playLeft[window % _playLeft.Length];
        kvumSegments.Value = _playRight[window % _playRight.Length];
        UpdateLevelLabel();
        _sync = false;
    }

    /// <summary>
    /// Build an eight-second tune in memory. Playback is loud enough to hear, and the meter
    /// levels keep that tune's dynamics with the loudest moment near +1 VU.
    /// </summary>
    private static void BuildMusic(out byte[] wav, out double[] levels)
    {
        int[] melody =
        {
            64, 0, 67, 0, 72, 0, 0, 0,
            67, 0, 64, 0, 60, 0, 0, 0,
            60, 64, 67, 72, 76, 72, 67, 64,
            72, 0, 67, 0, 64, 0, 60, 0
        };
        double[] velocity =
        {
            0.35, 0, 0.40, 0, 0.55, 0, 0, 0,
            0.40, 0, 0.45, 0, 0.30, 0, 0, 0,
            0.50, 0.60, 0.75, 0.90, 1.00, 0.85, 0.70, 0.55,
            1.00, 0, 0.60, 0, 0.40, 0, 0.25, 0
        };

        var samples = new double[MusicEighths * MusicEighth];
        for (int note = 0; note < MusicEighths; note++)
        {
            if (melody[note] == 0)
            {
                continue;
            }

            int start = note * MusicEighth;
            int gate = (int)(MusicEighth * 0.82);
            double amp = 0.22 + (0.55 * velocity[note]);
            AddTone(samples, start, gate, Midi(melody[note]), amp);
            AddTone(samples, start, gate, Midi(melody[note] - 12), amp * 0.35);
            if (note == 24)
            {
                AddTone(samples, start, MusicEighth * 2, Midi(60), 0.28);
                AddTone(samples, start, MusicEighth * 2, Midi(64), 0.22);
                AddTone(samples, start, MusicEighth * 2, Midi(67), 0.22);
            }
        }

        double peak = 0d;
        for (int i = 0; i < samples.Length; i++)
        {
            double abs = Math.Abs(samples[i]);
            if (abs > peak)
            {
                peak = abs;
            }
        }

        double gain = peak > 1e-6 ? 0.45 / peak : 0d;
        int windows = samples.Length / MusicWindow;
        var rms = new double[windows];
        double loudest = 0d;
        var pcm = new short[windows * MusicWindow];
        for (int window = 0; window < windows; window++)
        {
            double sum = 0d;
            int origin = window * MusicWindow;
            for (int i = 0; i < MusicWindow; i++)
            {
                double sample = samples[origin + i] * gain;
                if (sample > 1d)
                {
                    sample = 1d;
                }
                else if (sample < -1d)
                {
                    sample = -1d;
                }

                pcm[origin + i] = (short)(sample * 32767d);
                sum += sample * sample;
            }

            rms[window] = Math.Sqrt(sum / MusicWindow);
            if (rms[window] > loudest)
            {
                loudest = rms[window];
            }
        }

        levels = new double[windows];
        double loudestDb = loudest < 1e-8 ? -120d : 20d * Math.Log10(loudest);
        for (int window = 0; window < windows; window++)
        {
            double dbfs = rms[window] < 1e-8 ? -120d : 20d * Math.Log10(rms[window]);
            double vu = dbfs - loudestDb + 1d;
            if (vu < -20d)
            {
                vu = -20d;
            }
            else if (vu > 3d)
            {
                vu = 3d;
            }

            levels[window] = vu;
        }

        wav = WriteWav(pcm, MusicRate, 1);
    }

    /// <summary>
    /// Build an eight-second stereo file. The left channel plays alone, then the right channel,
    /// then the two parts answer each other. Both meters share one loudness scale.
    /// </summary>
    private static void BuildStereo(out byte[] wav, out double[] leftLevels, out double[] rightLevels)
    {
        int[] leftNotes =
        {
            64, 67, 72, 76, 72, 67, 64, 60,
            0, 0, 0, 0, 0, 0, 0, 0,
            60, 64, 67, 72, 0, 67, 0, 64,
            72, 0, 67, 0, 64, 0, 60, 0
        };
        int[] rightNotes =
        {
            0, 0, 0, 0, 0, 0, 0, 0,
            67, 71, 74, 79, 74, 71, 67, 62,
            67, 71, 74, 79, 74, 0, 71, 0,
            0, 76, 0, 71, 0, 67, 0, 64
        };

        var left = new double[MusicEighths * MusicEighth];
        var right = new double[left.Length];
        RenderNotes(left, leftNotes);
        RenderNotes(right, rightNotes);
        double peak = Math.Max(PeakAbs(left), PeakAbs(right));
        double gain = peak > 1e-6 ? 0.45 / peak : 0d;
        Measure(left, gain, out short[] leftPcm, out double[] leftRms, out double leftPeak);
        Measure(right, gain, out short[] rightPcm, out double[] rightRms, out double rightPeak);
        double loudest = Math.Max(leftPeak, rightPeak);
        leftLevels = ToVu(leftRms, loudest);
        rightLevels = ToVu(rightRms, loudest);

        var interleaved = new short[leftPcm.Length * 2];
        for (int i = 0; i < leftPcm.Length; i++)
        {
            interleaved[i * 2] = leftPcm[i];
            interleaved[(i * 2) + 1] = rightPcm[i];
        }

        wav = WriteWav(interleaved, MusicRate, 2);
    }

    private static void RenderNotes(double[] samples, int[] notes)
    {
        for (int note = 0; note < notes.Length; note++)
        {
            if (notes[note] == 0)
            {
                continue;
            }

            int start = note * MusicEighth;
            int gate = (int)(MusicEighth * 0.82);
            AddTone(samples, start, gate, Midi(notes[note]), 0.7);
        }
    }

    private static double PeakAbs(double[] samples)
    {
        double peak = 0d;
        for (int i = 0; i < samples.Length; i++)
        {
            double abs = Math.Abs(samples[i]);
            if (abs > peak)
            {
                peak = abs;
            }
        }

        return peak;
    }

    private static void Measure(double[] samples, double gain, out short[] pcm, out double[] rms, out double peakRms)
    {
        int windows = samples.Length / MusicWindow;
        pcm = new short[windows * MusicWindow];
        rms = new double[windows];
        peakRms = 0d;
        for (int window = 0; window < windows; window++)
        {
            double sum = 0d;
            int origin = window * MusicWindow;
            for (int i = 0; i < MusicWindow; i++)
            {
                double sample = samples[origin + i] * gain;
                if (sample > 1d)
                {
                    sample = 1d;
                }
                else if (sample < -1d)
                {
                    sample = -1d;
                }

                pcm[origin + i] = (short)(sample * 32767d);
                sum += sample * sample;
            }

            rms[window] = Math.Sqrt(sum / MusicWindow);
            if (rms[window] > peakRms)
            {
                peakRms = rms[window];
            }
        }
    }

    private static double[] ToVu(double[] rms, double loudest)
    {
        var levels = new double[rms.Length];
        double loudestDb = loudest < 1e-8 ? -120d : 20d * Math.Log10(loudest);
        for (int window = 0; window < rms.Length; window++)
        {
            double dbfs = rms[window] < 1e-8 ? -120d : 20d * Math.Log10(rms[window]);
            double vu = dbfs - loudestDb + 1d;
            if (vu < -20d)
            {
                vu = -20d;
            }
            else if (vu > 3d)
            {
                vu = 3d;
            }

            levels[window] = vu;
        }

        return levels;
    }

    private static void AddTone(double[] samples, int start, int length, double frequency, double amplitude)
    {
        int end = Math.Min(samples.Length, start + length);
        int attack = Math.Min(length / 10, MusicRate / 100);
        int release = Math.Min(length / 4, MusicRate / 25);
        if (attack < 1)
        {
            attack = 1;
        }

        for (int i = start; i < end; i++)
        {
            int age = i - start;
            double envelope = 1d;
            int remaining = end - i;
            if (age < attack)
            {
                envelope = age / (double)attack;
            }
            else if (remaining < release)
            {
                envelope = remaining / (double)release;
            }

            double phase = 2d * Math.PI * frequency * age / MusicRate;
            samples[i] += (Math.Sin(phase) + (0.18 * Math.Sin(phase * 2d))) * amplitude * envelope;
        }
    }

    private static double Midi(int note) => 440d * Math.Pow(2d, (note - 69) / 12d);

    private static byte[] WriteWav(short[] pcm, int rate, int channels)
    {
        int dataBytes = pcm.Length * 2;
        int blockAlign = channels * 2;
        var buffer = new byte[44 + dataBytes];
        buffer[0] = (byte)'R';
        buffer[1] = (byte)'I';
        buffer[2] = (byte)'F';
        buffer[3] = (byte)'F';
        WriteInt(buffer, 4, 36 + dataBytes);
        buffer[8] = (byte)'W';
        buffer[9] = (byte)'A';
        buffer[10] = (byte)'V';
        buffer[11] = (byte)'E';
        buffer[12] = (byte)'f';
        buffer[13] = (byte)'m';
        buffer[14] = (byte)'t';
        buffer[15] = (byte)' ';
        WriteInt(buffer, 16, 16);
        WriteShort(buffer, 20, 1);
        WriteShort(buffer, 22, channels);
        WriteInt(buffer, 24, rate);
        WriteInt(buffer, 28, rate * blockAlign);
        WriteShort(buffer, 32, blockAlign);
        WriteShort(buffer, 34, 16);
        buffer[36] = (byte)'d';
        buffer[37] = (byte)'a';
        buffer[38] = (byte)'t';
        buffer[39] = (byte)'a';
        WriteInt(buffer, 40, dataBytes);
        Buffer.BlockCopy(pcm, 0, buffer, 44, dataBytes);
        return buffer;
    }

    private static void WriteInt(byte[] buffer, int offset, int value)
    {
        buffer[offset] = (byte)value;
        buffer[offset + 1] = (byte)(value >> 8);
        buffer[offset + 2] = (byte)(value >> 16);
        buffer[offset + 3] = (byte)(value >> 24);
    }

    private static void WriteShort(byte[] buffer, int offset, int value)
    {
        buffer[offset] = (byte)value;
        buffer[offset + 1] = (byte)(value >> 8);
    }
}
