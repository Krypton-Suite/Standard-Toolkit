#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.Linq;

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Gauges;

public class KryptonGenericMusic
{
    #region Static fields

    private const int MusicRate = 22050;
    private const int MusicWindow = 1102;
    private const int MusicEighth = 5510;

    #endregion

    #region Instance Fields

    private bool _sync;
    private SoundPlayer? _musicPlayer;
    private MemoryStream? _musicStream;
    private Timer? _musicTimer;
    private byte[]? _musicWav;
    private double[]? _musicLevels;
    private int _musicSeconds;
    private int _musicBass = -1;
    private byte[]? _stereoWav;
    private double[]? _stereoLeft;
    private double[]? _stereoRight;
    private int _stereoSeconds;
    private int _stereoBass = -1;
    private double[]? _playLeft;
    private double[]? _playRight;
    private int _musicStarted;

    private readonly KryptonVuMeter _analogMeter;
    private readonly KryptonVuMeter _segmentsMeter;
    private readonly KryptonVuMeter _leftMeter;
    private readonly KryptonVuMeter _rightMeter;
    private bool _stereoPair;

    private readonly KryptonLabel _leveLabel;
    private readonly KryptonCheckBox _stereoCheckBox;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a music display with the specified synchronization setting and controls.
    /// </summary>
    /// <param name="sync">Indicates whether meter updates are synchronized.</param>
    /// <param name="analogMeter">The analog VU meter.</param>
    /// <param name="segmentsMeter">The segmented VU meter.</param>
    /// <param name="leftMeter">The left-channel analog meter.</param>
    /// <param name="rightMeter">The right-channel analog meter.</param>
    /// <param name="levelLabel">The label that displays the audio level.</param>
    /// <param name="stereoCheckBox">The check box that indicates stereo mode.</param>
    public KryptonGenericMusic(bool sync, KryptonVuMeter analogMeter, KryptonVuMeter segmentsMeter, KryptonVuMeter leftMeter, KryptonVuMeter rightMeter, KryptonLabel levelLabel, KryptonCheckBox stereoCheckBox)
    {
        _sync = sync;
        _analogMeter = analogMeter;
        _segmentsMeter = segmentsMeter;
        _leftMeter = leftMeter;
        _rightMeter = rightMeter;
        _leveLabel = levelLabel;
        _stereoCheckBox = stereoCheckBox;
    }

    #endregion

    #region Implementation

    /// <summary>
    /// Play the generated loop and move both meters from its level.
    /// </summary>
    /// <returns><see langword="true"/> when playback started.</returns>
    public bool StartMusic() => StartMusic(600);

    /// <summary>
    /// Play the generated loop for <paramref name="seconds"/> and move both meters from its level.
    /// </summary>
    /// <param name="seconds">Length of one pass before the loop repeats.</param>
    /// <returns><see langword="true"/> when playback started.</returns>
    public bool StartMusic(int seconds) => StartMusic(seconds, 50);

    /// <summary>
    /// Play the generated loop for <paramref name="seconds"/> at the given bass amount.
    /// </summary>
    /// <param name="seconds">Length of one pass before the loop repeats.</param>
    /// <param name="bass">Bass amount from 0 (none) to 100 (twice the normal line). 50 is the normal mix.</param>
    /// <returns><see langword="true"/> when playback started.</returns>
    public bool StartMusic(int seconds, int bass)
    {
        StopMusic();
        bass = ClampBass(bass);
        try
        {
            if (_musicWav == null || _musicLevels == null || _musicSeconds != seconds || _musicBass != bass)
            {
                BuildMusic(seconds, bass, out byte[] wav, out double[] levels);
                _musicWav = wav;
                _musicLevels = levels;
                _musicSeconds = seconds;
                _musicBass = bass;
            }

            _playLeft = _musicLevels;
            _playRight = _musicLevels;
            _stereoPair = false;
            PlayWav(_musicWav);
            return true;
        }
        catch (Exception ex)
        {
            StopMusic();
            _leveLabel.Values.Text = ex.Message;
            return false;
        }
    }

    /// <summary>
    /// Play the stereo file. The left meter follows the left channel and the right meter follows the right.
    /// </summary>
    /// <returns><see langword="true"/> when playback started.</returns>
    public bool StartStereo() => StartStereo(600);

    /// <summary>
    /// Play the stereo file for <paramref name="seconds"/>. The left meter follows the left channel and the right meter follows the right.
    /// </summary>
    /// <param name="seconds">Length of one pass before the loop repeats.</param>
    /// <returns><see langword="true"/> when playback started.</returns>
    public bool StartStereo(int seconds) => StartStereo(seconds, 50);

    /// <summary>
    /// Play the stereo file for <paramref name="seconds"/> at the given bass amount.
    /// </summary>
    /// <param name="seconds">Length of one pass before the loop repeats.</param>
    /// <param name="bass">Bass amount from 0 (none) to 100 (twice the normal line). 50 is the normal mix.</param>
    /// <returns><see langword="true"/> when playback started.</returns>
    public bool StartStereo(int seconds, int bass)
    {
        StopMusic();
        bass = ClampBass(bass);
        try
        {
            if (_stereoWav == null || _stereoLeft == null || _stereoRight == null || _stereoSeconds != seconds || _stereoBass != bass)
            {
                BuildStereo(seconds, bass, out byte[] wav, out double[] left, out double[] right);
                _stereoWav = wav;
                _stereoLeft = left;
                _stereoRight = right;
                _stereoSeconds = seconds;
                _stereoBass = bass;
            }

            _playLeft = _stereoLeft;
            _playRight = _stereoRight;
            _stereoPair = true;
            PlayWav(_stereoWav);
            return true;
        }
        catch (Exception ex)
        {
            StopMusic();
            _leveLabel.Values.Text = ex.Message;
            return false;
        }
    }

    /// <summary>
    /// Plays the specified WAV audio in a continuous loop.
    /// </summary>
    /// <param name="wav">A byte array containing WAV audio data.</param>
    public void PlayWav(byte[] wav)
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
    public void StopMusic()
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

    /// <summary>
    /// Updates the left and right level meters from the current music samples.
    /// </summary>
    /// <remarks>Samples are advanced every 50 milliseconds and repeat when the end of either sample array is
    /// reached. No update occurs if either array is null or empty.</remarks>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    public void OnMusicTick(object? sender, EventArgs e)
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
        double left = _playLeft[window % _playLeft.Length];
        double right = _playRight[window % _playRight.Length];
        _sync = true;
        _leftMeter.Value = left;
        _rightMeter.Value = right;
        if (!_stereoPair)
        {
            _analogMeter.Value = left;
            _segmentsMeter.Value = right;
        }

        UpdateLevelLabel();
        _sync = false;
    }

    /// <summary>
    /// Build a thirty-two second tune in memory. A verse, a chorus, and a cadence sit over a bass line.
    /// Playback is loud enough to hear, and the meter levels keep that tune's dynamics with the loudest moment near +1 VU.
    /// </summary>
    public static void BuildMusic(out byte[] wav, out double[] levels) => BuildMusic(32, 50, out wav, out levels);

    /// <summary>
    /// Build the tune repeated out to <paramref name="seconds"/>. A bass line and a lower octave sit under the melody.
    /// The loudest moment is placed near +1 VU.
    /// </summary>
    /// <param name="seconds">Length of the generated loop.</param>
    /// <param name="wav">PCM wave for <see cref="System.Media.SoundPlayer"/>.</param>
    /// <param name="levels">One VU reading per playback window.</param>
    public static void BuildMusic(int seconds, out byte[] wav, out double[] levels) =>
        BuildMusic(seconds, 50, out wav, out levels);

    /// <summary>
    /// Build the tune repeated out to <paramref name="seconds"/> with the given bass amount.
    /// The loudest moment is placed near +1 VU.
    /// </summary>
    /// <param name="seconds">Length of the generated loop.</param>
    /// <param name="bass">Bass amount from 0 (none) to 100 (twice the normal line). 50 is the normal mix.</param>
    /// <param name="wav">PCM wave for <see cref="System.Media.SoundPlayer"/>.</param>
    /// <param name="levels">One VU reading per playback window.</param>
    public static void BuildMusic(int seconds, int bass, out byte[] wav, out double[] levels)
    {
        int[] melody = Bars(
            [64, 0, 67, 0, 72, 0, 67, 64],
            [67, 0, 64, 0, 60, 62, 64, 0],
            [64, 67, 72, 76, 79, 76, 72, 67],
            [72, 0, 67, 0, 64, 0, 60, 0],
            [69, 72, 76, 72, 69, 0, 67, 69],
            [65, 69, 72, 69, 65, 0, 64, 65],
            [67, 71, 74, 79, 74, 71, 67, 62],
            [64, 0, 62, 0, 60, 0, 0, 0],
            [72, 76, 79, 76, 72, 67, 64, 67],
            [69, 72, 76, 72, 69, 64, 60, 64],
            [65, 69, 72, 77, 72, 69, 65, 60],
            [67, 71, 74, 79, 77, 76, 74, 72],
            [76, 74, 72, 67, 64, 0, 67, 0],
            [69, 67, 64, 62, 60, 0, 62, 64],
            [67, 64, 62, 60, 0, 0, 0, 0],
            [60, 0, 0, 0, 0, 0, 0, 0]);
        int[] bassLine = Bars(
            [36, 0, 0, 0, 36, 0, 0, 0],
            [36, 0, 0, 0, 43, 0, 0, 0],
            [36, 0, 0, 0, 36, 0, 0, 0],
            [43, 0, 0, 0, 41, 0, 0, 0],
            [45, 0, 0, 0, 45, 0, 0, 0],
            [41, 0, 0, 0, 41, 0, 0, 0],
            [43, 0, 0, 0, 43, 0, 0, 0],
            [36, 0, 0, 0, 43, 0, 0, 0],
            [36, 0, 0, 0, 36, 0, 0, 0],
            [45, 0, 0, 0, 45, 0, 0, 0],
            [41, 0, 0, 0, 41, 0, 0, 0],
            [43, 0, 0, 0, 43, 0, 0, 0],
            [36, 0, 0, 0, 45, 0, 0, 0],
            [41, 0, 0, 0, 43, 0, 0, 0],
            [36, 0, 0, 0, 36, 0, 0, 0],
            [36, 0, 0, 0, 0, 0, 0, 0]);
        double[] gain = Envelope(melody.Length,
            0.55, 0.6, 1.0, 0.65,
            0.7, 0.7, 0.9, 0.45,
            0.85, 0.8, 0.75, 0.8,
            0.6, 0.5, 0.4, 0.3);

        var samples = new double[SamplesForSeconds(seconds)];
        int phrase = melody.Length * MusicEighth;
        double bassAmount = ClampBass(bass) / 50d;
        for (int origin = 0, cycle = 0; origin < samples.Length; origin += phrase, cycle++)
        {
            int shift = PhraseShift(cycle);
            int[] cycleMelody = shift == 0 ? melody : Transpose(melody, shift);
            int[] cycleBass = shift == 0 ? bassLine : Transpose(bassLine, shift);
            RenderVoice(samples, cycleMelody, gain, 0.5, 1, origin);
            RenderVoice(samples, Transpose(cycleMelody, -12), gain, 0.16, 1, origin);
            RenderVoice(samples, IntervalBetween(cycleMelody, 7, 8, 12), gain, 0.18, 1, origin);
            if (bassAmount > 0d)
            {
                double bassLevel = (cycle % 2 == 0 ? 0.62 : 0.78) * bassAmount;
                RenderVoice(samples, cycleBass, gain, bassLevel, 2, origin);
                RenderVoice(samples, Transpose(cycleBass, -12), gain, bassLevel * 0.55, 4, origin);
            }
        }

        double peak = samples.Select(t => Math.Abs(t)).Prepend(0d).Max();

        double playbackGain = peak > 1e-6 ? 0.45 / peak : 0d;
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
                double sample = samples[origin + i] * playbackGain;
                sample = sample switch
                {
                    > 1d => 1d,
                    < -1d => -1d,
                    _ => sample
                };

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
            vu = vu switch
            {
                < -20d => -20d,
                > 3d => 3d,
                _ => vu
            };

            levels[window] = vu;
        }

        wav = WriteWav(pcm, MusicRate, 1);
    }

    /// <summary>
    /// Build a thirty-two second stereo file. Each channel has its own verse, then the two answer
    /// and finally play together. Both meters share one loudness scale.
    /// </summary>
    public static void BuildStereo(out byte[] wav, out double[] leftLevels, out double[] rightLevels) =>
        BuildStereo(32, 50, out wav, out leftLevels, out rightLevels);

    /// <summary>
    /// Build the stereo tune repeated out to <paramref name="seconds"/>. Bass follows whichever channel is playing.
    /// Both meters share one loudness scale.
    /// </summary>
    /// <param name="seconds">Length of the generated loop.</param>
    /// <param name="wav">Interleaved stereo PCM wave.</param>
    /// <param name="leftLevels">Left-channel VU readings, one per playback window.</param>
    /// <param name="rightLevels">Right-channel VU readings, one per playback window.</param>
    public static void BuildStereo(int seconds, out byte[] wav, out double[] leftLevels, out double[] rightLevels) =>
        BuildStereo(seconds, 50, out wav, out leftLevels, out rightLevels);

    /// <summary>
    /// Build the stereo tune repeated out to <paramref name="seconds"/> with the given bass amount.
    /// Bass follows whichever channel is playing. Both meters share one loudness scale.
    /// </summary>
    /// <param name="seconds">Length of the generated loop.</param>
    /// <param name="bass">Bass amount from 0 (none) to 100 (twice the normal line). 50 is the normal mix.</param>
    /// <param name="wav">Interleaved stereo PCM wave.</param>
    /// <param name="leftLevels">Left-channel VU readings, one per playback window.</param>
    /// <param name="rightLevels">Right-channel VU readings, one per playback window.</param>
    public static void BuildStereo(int seconds, int bass, out byte[] wav, out double[] leftLevels, out double[] rightLevels)
    {
        int[] leftNotes = Bars(
            [64, 67, 72, 76, 79, 76, 72, 67],
            [72, 0, 67, 64, 60, 64, 67, 0],
            [0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0],
            [69, 72, 76, 72, 69, 67, 64, 0],
            [0, 0, 0, 0, 0, 0, 0, 0],
            [64, 0, 67, 0, 72, 0, 76, 0],
            [0, 67, 0, 72, 0, 67, 0, 64],
            [72, 76, 79, 76, 72, 67, 64, 67],
            [69, 72, 76, 72, 69, 64, 60, 64],
            [65, 69, 72, 77, 72, 69, 65, 60],
            [67, 71, 74, 79, 74, 71, 67, 64],
            [0, 76, 0, 72, 0, 67, 0, 64],
            [0, 69, 0, 65, 0, 64, 0, 60],
            [64, 67, 72, 67, 64, 60, 0, 0],
            [60, 0, 0, 0, 0, 0, 0, 0]);
        int[] rightNotes = Bars(
            [0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0],
            [67, 71, 74, 79, 76, 74, 71, 67],
            [74, 0, 71, 67, 62, 67, 71, 0],
            [0, 0, 0, 0, 0, 0, 0, 0],
            [65, 69, 72, 69, 65, 64, 60, 0],
            [0, 60, 0, 64, 0, 67, 0, 72],
            [71, 0, 74, 0, 71, 0, 67, 0],
            [67, 71, 74, 71, 67, 64, 60, 64],
            [64, 67, 72, 67, 64, 60, 57, 60],
            [60, 64, 67, 72, 67, 64, 60, 57],
            [62, 67, 71, 74, 71, 67, 62, 59],
            [76, 0, 79, 0, 76, 0, 74, 0],
            [72, 0, 69, 0, 67, 0, 65, 0],
            [67, 64, 60, 64, 67, 72, 0, 0],
            [67, 0, 0, 0, 0, 0, 0, 0]);
        double[] solo = Envelope(leftNotes.Length,
            1.0, 0.95, 1.0, 0.95,
            0.85, 0.85, 0.7, 0.7,
            0.72, 0.7, 0.68, 0.72,
            0.6, 0.55, 0.5, 0.35);

        int[] bassLine = Bars(
            [36, 0, 0, 0, 36, 0, 0, 0],
            [36, 0, 0, 0, 43, 0, 0, 0],
            [43, 0, 0, 0, 43, 0, 0, 0],
            [43, 0, 0, 0, 41, 0, 0, 0],
            [45, 0, 0, 0, 45, 0, 0, 0],
            [41, 0, 0, 0, 41, 0, 0, 0],
            [36, 0, 0, 0, 43, 0, 0, 0],
            [43, 0, 0, 0, 43, 0, 0, 0],
            [36, 0, 0, 0, 36, 0, 0, 0],
            [45, 0, 0, 0, 45, 0, 0, 0],
            [41, 0, 0, 0, 41, 0, 0, 0],
            [43, 0, 0, 0, 43, 0, 0, 0],
            [36, 0, 0, 0, 45, 0, 0, 0],
            [41, 0, 0, 0, 43, 0, 0, 0],
            [36, 0, 0, 0, 36, 0, 0, 0],
            [36, 0, 0, 0, 0, 0, 0, 0]);
        int[] leftBass = NotesUnder(leftNotes, bassLine);
        int[] rightBass = NotesUnder(rightNotes, bassLine);
        var left = new double[SamplesForSeconds(seconds)];
        var right = new double[left.Length];
        int phrase = leftNotes.Length * MusicEighth;
        double bassAmount = ClampBass(bass) / 50d;
        for (int origin = 0, cycle = 0; origin < left.Length; origin += phrase, cycle++)
        {
            int shift = PhraseShift(cycle);
            int[] cycleLeft = shift == 0 ? leftNotes : Transpose(leftNotes, shift);
            int[] cycleRight = shift == 0 ? rightNotes : Transpose(rightNotes, shift);
            int[] cycleLeftBass = shift == 0 ? leftBass : Transpose(leftBass, shift);
            int[] cycleRightBass = shift == 0 ? rightBass : Transpose(rightBass, shift);
            RenderVoice(left, cycleLeft, solo, 0.62, 1, origin);
            RenderVoice(right, cycleRight, solo, 0.62, 1, origin);
            if (bassAmount > 0d)
            {
                double bassLevel = (cycle % 2 == 0 ? 0.5 : 0.66) * bassAmount;
                RenderVoice(left, cycleLeftBass, solo, bassLevel, 2, origin);
                RenderVoice(right, cycleRightBass, solo, bassLevel, 2, origin);
                RenderVoice(left, Transpose(cycleLeftBass, -12), solo, bassLevel * 0.5, 4, origin);
                RenderVoice(right, Transpose(cycleRightBass, -12), solo, bassLevel * 0.5, 4, origin);
            }
        }
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

    /// <summary>
    /// Adds a tone to the sample buffer for each nonzero note.
    /// </summary>
    /// <param name="samples">The sample buffer to which tones are added.</param>
    /// <param name="notes">The note values to render; zero entries are skipped.</param>
    public static void RenderNotes(double[] samples, int[] notes)
    {
        var gain = new double[notes.Length];
        for (int note = 0; note < notes.Length; note++)
        {
            gain[note] = 1d;
        }

        RenderVoice(samples, notes, gain, 0.7, 1);
    }

    /// <summary>
    /// Joins one-bar groups of eighth notes into a single score.
    /// </summary>
    private static int[] Bars(params int[][] bars)
    {
        int count = 0;
        for (int bar = 0; bar < bars.Length; bar++)
        {
            count += bars[bar].Length;
        }

        var score = new int[count];
        int offset = 0;
        for (int bar = 0; bar < bars.Length; bar++)
        {
            Array.Copy(bars[bar], 0, score, offset, bars[bar].Length);
            offset += bars[bar].Length;
        }

        return score;
    }

    /// <summary>
    /// Builds a per-eighth gain from one level per bar. Each bar uses the same eight-note contour.
    /// </summary>
    private static double[] Envelope(int count, params double[] barGain)
    {
        double[] contour = [0.72, 0.45, 0.9, 0.5, 1d, 0.55, 0.8, 0.4];
        var gain = new double[count];
        for (int note = 0; note < count; note++)
        {
            int bar = note / 8;
            double level = bar < barGain.Length ? barGain[bar] : barGain[barGain.Length - 1];
            gain[note] = contour[note % 8] * level;
        }

        return gain;
    }

    /// <summary>
    /// Semitone offset for one phrase. Nineteen phrases is just over ten minutes, and this list does not repeat inside that span.
    /// </summary>
    private static int PhraseShift(int cycle)
    {
        int[] shifts = { 0, 2, -2, 5, 3, -3, 7, 4, -5, 9, 1, -1, 6, -4, 8, -6, 10, 3, 5 };
        int index = cycle % shifts.Length;
        return index < 0 ? shifts[index + shifts.Length] : shifts[index];
    }

    /// <summary>
    /// Shifts every sounding note by <paramref name="semitones"/>.
    /// </summary>
    private static int[] Transpose(int[] notes, int semitones)
    {
        var shifted = new int[notes.Length];
        for (int note = 0; note < notes.Length; note++)
        {
            shifted[note] = notes[note] == 0 ? 0 : notes[note] + semitones;
        }

        return shifted;
    }

    /// <summary>
    /// Copies notes inside a bar range and shifts them by <paramref name="semitones"/>. Bars outside the range stay silent.
    /// </summary>
    private static int[] IntervalBetween(int[] notes, int semitones, int firstBar, int lastBar)
    {
        var shifted = new int[notes.Length];
        for (int note = 0; note < notes.Length; note++)
        {
            int bar = note / 8;
            shifted[note] = notes[note] != 0 && bar >= firstBar && bar < lastBar ? notes[note] + semitones : 0;
        }

        return shifted;
    }

    /// <summary>
    /// Keeps a bass slider value inside 0 (none) to 100 (twice the normal line).
    /// </summary>
    private static int ClampBass(int bass)
    {
        if (bass < 0)
        {
            return 0;
        }

        return bass > 100 ? 100 : bass;
    }

    /// <summary>
    /// Number of samples covering at least <paramref name="seconds"/>, rounded up to a whole meter window.
    /// Ten minutes is the longest length.
    /// </summary>
    private static int SamplesForSeconds(int seconds)
    {
        if (seconds < 1)
        {
            seconds = 1;
        }
        else if (seconds > 600)
        {
            seconds = 600;
        }

        long needed = (long)seconds * MusicRate;
        int windows = (int)((needed + MusicWindow - 1) / MusicWindow);
        return windows < 1 ? MusicWindow : windows * MusicWindow;
    }

    /// <summary>
    /// Keeps <paramref name="source"/> only in bars where <paramref name="mask"/> has a note, so bass follows the active channel.
    /// </summary>
    private static int[] NotesUnder(int[] mask, int[] source)
    {
        var notes = new int[mask.Length];
        for (int bar = 0; bar < mask.Length; bar += 8)
        {
            bool sounding = false;
            int end = Math.Min(mask.Length, bar + 8);
            for (int note = bar; note < end; note++)
            {
                if (mask[note] != 0)
                {
                    sounding = true;
                    break;
                }
            }

            if (!sounding)
            {
                continue;
            }

            for (int note = bar; note < end && note < source.Length; note++)
            {
                notes[note] = source[note];
            }
        }

        return notes;
    }

    /// <summary>
    /// Renders a voice. Repeated notes sustain, and each attack is scaled by <paramref name="gain"/>.
    /// </summary>
    private static void RenderVoice(double[] samples, int[] notes, double[] gain, double amplitude, int holdEighths) =>
        RenderVoice(samples, notes, gain, amplitude, holdEighths, 0);

    /// <summary>
    /// Renders a voice starting at <paramref name="origin"/> so a phrase can be repeated through a longer buffer.
    /// </summary>
    private static void RenderVoice(double[] samples, int[] notes, double[] gain, double amplitude, int holdEighths, int origin)
    {
        int hold = holdEighths < 1 ? 1 : holdEighths;
        for (int note = 0; note < notes.Length; note++)
        {
            if (notes[note] == 0 || note > 0 && notes[note] == notes[note - 1])
            {
                continue;
            }

            int length = 1;
            while (note + length < notes.Length && notes[note + length] == notes[note])
            {
                length++;
            }

            if (length < hold)
            {
                length = hold;
            }

            int start = origin + (note * MusicEighth);
            if (start >= samples.Length)
            {
                return;
            }

            int gate = (int)(MusicEighth * length * 0.88);
            double level = note < gain.Length ? gain[note] : 1d;
            AddTone(samples, start, gate, Midi(notes[note]), amplitude * level);
        }
    }

    /// <summary>
    /// Returns the largest absolute value in the sample array.
    /// </summary>
    /// <param name="samples">The sample values to inspect.</param>
    /// <returns>The largest absolute sample value, or zero if the array is empty.</returns>
    public static double PeakAbs(double[] samples) =>
        samples.Select(Math.Abs).Prepend(0d).Max();

    /// <summary>
    /// Converts sample windows to clipped 16-bit PCM and calculates their RMS levels.
    /// </summary>
    /// <remarks>Only complete windows of MusicWindow samples are processed; any remaining samples are
    /// omitted.</remarks>
    /// <param name="samples">The input audio samples.</param>
    /// <param name="gain">The multiplier applied to each sample before clipping.</param>
    /// <param name="pcm">Receives the converted signed 16-bit PCM samples.</param>
    /// <param name="rms">Receives the normalized RMS level of each complete window.</param>
    /// <param name="peakRms">Receives the highest RMS level among the complete windows, or 0 if there are none.</param>
    public static void Measure(double[] samples, double gain, out short[] pcm, out double[] rms, out double peakRms)
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
                sample = sample switch
                {
                    > 1d => 1d,
                    < -1d => -1d,
                    _ => sample
                };

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

    /// <summary>
    /// Converts RMS amplitudes to VU levels relative to the loudest amplitude.
    /// </summary>
    /// <remarks>Amplitudes below 1e-8 are treated as -120 dBFS. Each level is normalized to the loudest
    /// amplitude with a 1 dB offset.</remarks>
    /// <param name="rms">The RMS amplitude values to convert.</param>
    /// <param name="loudest">The loudest amplitude used as the reference level.</param>
    /// <returns>An array of VU levels, clamped to the range from -20 to 3.</returns>
    public static double[] ToVu(double[] rms, double loudest)
    {
        var levels = new double[rms.Length];
        double loudestDb = loudest < 1e-8 ? -120d : 20d * Math.Log10(loudest);
        for (int window = 0; window < rms.Length; window++)
        {
            double dbfs = rms[window] < 1e-8 ? -120d : 20d * Math.Log10(rms[window]);
            double vu = dbfs - loudestDb + 1d;
            vu = vu switch
            {
                < -20d => -20d,
                > 3d => 3d,
                _ => vu
            };

            levels[window] = vu;
        }

        return levels;
    }

    /// <summary>
    /// Adds a synthesized tone to a range of audio samples.
    /// </summary>
    /// <remarks>Adds a fundamental sine wave and a quieter second harmonic to the buffer, with attack and
    /// release envelopes.</remarks>
    /// <param name="samples">The sample buffer to modify.</param>
    /// <param name="start">The index of the first sample to modify.</param>
    /// <param name="length">The number of samples to generate, limited by the buffer's length.</param>
    /// <param name="frequency">The tone frequency in hertz.</param>
    /// <param name="amplitude">The tone amplitude.</param>
    public static void AddTone(double[] samples, int start, int length, double frequency, double amplitude)
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

    /// <summary>
    /// Gets the frequency in hertz for a MIDI note number, using A4 as 440 Hz.
    /// </summary>
    /// <remarks>Each increase of 12 note numbers doubles the frequency.</remarks>
    /// <param name="note">The MIDI note number.</param>
    /// <returns>The frequency of the note in hertz.</returns>
    public static double Midi(int note) => 440d * Math.Pow(2d, (note - 69) / 12d);

    /// <summary>
    /// Creates a RIFF/WAVE byte array containing 16-bit PCM audio.
    /// </summary>
    /// <remarks>The WAV data chunk uses 16-bit samples.</remarks>
    /// <param name="pcm">The PCM samples to encode.</param>
    /// <param name="rate">The sample rate in samples per second.</param>
    /// <param name="channels">The number of audio channels.</param>
    /// <returns>A byte array containing the WAV header and PCM sample data.</returns>
    public static byte[] WriteWav(short[] pcm, int rate, int channels)
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

    /// <summary>
    /// Writes a 32-bit integer to the buffer in little-endian byte order.
    /// </summary>
    /// <param name="buffer">The buffer that receives the integer.</param>
    /// <param name="offset">The zero-based index of the first byte to write.</param>
    /// <param name="value">The integer to write.</param>
    public static void WriteInt(byte[] buffer, int offset, int value)
    {
        buffer[offset] = (byte)value;
        buffer[offset + 1] = (byte)(value >> 8);
        buffer[offset + 2] = (byte)(value >> 16);
        buffer[offset + 3] = (byte)(value >> 24);
    }

    /// <summary>
    /// Writes the 16-bit value to the buffer in little-endian byte order.
    /// </summary>
    /// <param name="buffer">The destination buffer.</param>
    /// <param name="offset">The index of the first byte to write.</param>
    /// <param name="value">The value to write; only its least significant 16 bits are written.</param>
    public static void WriteShort(byte[] buffer, int offset, int value)
    {
        buffer[offset] = (byte)value;
        buffer[offset + 1] = (byte)(value >> 8);
    }

    /// <summary>
    /// Updates the level label with the target or left and right meter values, formatted to one decimal place using the
    /// current culture.
    /// </summary>
    public void UpdateLevelLabel()
    {
        if (_stereoCheckBox.Checked)
        {
            _leveLabel.Values.Text = $@"Left {_leftMeter.Value.ToString("0.0", CultureInfo.CurrentCulture)} / Right {_rightMeter.Value.ToString("0.0", CultureInfo.CurrentCulture)} VU";
            return;
        }

        _leveLabel.Values.Text = $@"Target {_analogMeter.Value.ToString("0.0", CultureInfo.CurrentCulture)} VU";
    }

    #endregion
}