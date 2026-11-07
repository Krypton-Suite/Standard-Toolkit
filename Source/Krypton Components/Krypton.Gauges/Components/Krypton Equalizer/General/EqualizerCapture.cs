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
/// Reads the default capture endpoint, runs an FFT, and posts band levels to the equalizer.
/// </summary>
internal sealed class EqualizerCapture : IDisposable
{
    private readonly Func<EqualizerCaptureSample> _readSample;
    private readonly Action<int, double[]> _onLevels;
    private readonly WasapiCapturePump _pump;
    private readonly double[] _window = new double[EqualizerFft.WindowSize];
    private readonly double[] _real = new double[EqualizerFft.WindowSize];
    private readonly double[] _imag = new double[EqualizerFft.WindowSize];
    private readonly double[] _levels = new double[EqualizerScale.MaximumBandCount];
    private int _filled;
    private bool _disposed;

    internal EqualizerCapture(Control ui,
        Func<EqualizerCaptureSample> readSample,
        Action<int, double[]> onLevels,
        Action<Exception> onFailed)
    {
        _readSample = readSample;
        _onLevels = onLevels;
        _pump = new WasapiCapturePump(ui, ReadGeneration, OnPacket, onFailed, "KryptonEqualizerCapture");
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _pump.Dispose();
    }

    private int ReadGeneration()
    {
        EqualizerCaptureSample sample = _readSample();
        return sample.Generation;
    }

    private void OnPacket(int generation, IntPtr data, uint frames, uint flags, WasapiWaveFormat info, ref byte[] packet)
    {
        EqualizerCaptureSample sample = _readSample();
        if (sample.Generation < 0 || sample.Generation != generation || sample.Frequencies.Length == 0)
        {
            return;
        }

        if ((flags & WasapiCapturePump.SilentFlag) != 0)
        {
            for (int i = 0; i < sample.Frequencies.Length; i++)
            {
                _levels[i] = 0d;
            }

            PostLevels(generation, sample.Frequencies.Length);
            return;
        }

        if (frames == 0 || info.BlockAlign <= 0)
        {
            return;
        }

        WasapiPcmReader.CopyPacket(data, frames, info, ref packet);
        for (int frame = 0; frame < frames; frame++)
        {
            int frameOffset = frame * info.BlockAlign;
            double amplitude = WasapiPcmReader.ReadFrame(packet, frameOffset, info, sample.Channel);
            _window[_filled++] = amplitude;
            if (_filled < EqualizerFft.WindowSize)
            {
                continue;
            }

            Array.Copy(_window, _real, EqualizerFft.WindowSize);
            Array.Clear(_imag, 0, EqualizerFft.WindowSize);
            EqualizerFft.ApplyHann(_real, EqualizerFft.WindowSize);
            EqualizerFft.Forward(_real, _imag, EqualizerFft.WindowSize);
            EqualizerFft.MapToBands(_real, _imag, EqualizerFft.WindowSize, info.SampleRate,
                sample.Frequencies, sample.CalibrationOffsetDb, _levels);

            // 50% overlap for smoother spectrum updates.
            int hop = EqualizerFft.WindowSize / 2;
            Array.Copy(_window, hop, _window, 0, hop);
            _filled = hop;
            PostLevels(generation, sample.Frequencies.Length);
        }
    }

    private void PostLevels(int generation, int bandCount)
    {
        var copy = new double[bandCount];
        Array.Copy(_levels, copy, bandCount);
        _pump.PostToUi(() => _onLevels(generation, copy));
    }
}

/// <summary>
/// Snapshot of equalizer capture settings read on the audio thread.
/// </summary>
internal readonly struct EqualizerCaptureSample
{
    internal EqualizerCaptureSample(int generation, VuMeterChannel channel, double calibrationOffsetDb, double[] frequencies)
    {
        Generation = generation;
        Channel = channel;
        CalibrationOffsetDb = calibrationOffsetDb;
        Frequencies = frequencies;
    }

    internal int Generation { get; }

    internal VuMeterChannel Channel { get; }

    internal double CalibrationOffsetDb { get; }

    internal double[] Frequencies { get; }
}
