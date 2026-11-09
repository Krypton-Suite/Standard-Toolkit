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
/// Reads the default capture endpoint on a background thread and posts VU levels back to the control.
/// Shared-mode WASAPI, with no extra audio package.
/// </summary>
internal sealed class VuMeterCapture : IDisposable
{
    private readonly Func<VuMeterCaptureSample> _readSample;
    private readonly Action<int, double> _onLevel;
    private readonly WasapiCapturePump _pump;
    private bool _disposed;

    internal VuMeterCapture(Control ui,
        Func<VuMeterCaptureSample> readSample,
        Action<int, double> onLevel,
        Action<Exception> onFailed)
    {
        _readSample = readSample;
        _onLevel = onLevel;
        _pump = new WasapiCapturePump(ui, ReadGeneration, OnPacket, onFailed, "KryptonVuMeterCapture");
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
        VuMeterCaptureSample sample = _readSample();
        return sample.Generation;
    }

    private void OnPacket(int generation, IntPtr data, uint frames, uint flags, WasapiWaveFormat info, ref byte[] packet)
    {
        VuMeterCaptureSample sample = _readSample();
        if (sample.Generation < 0 || sample.Generation != generation)
        {
            return;
        }

        double vu = (flags & WasapiCapturePump.SilentFlag) != 0
            ? VuMeterScale.ScaleMinimum
            : LevelFromBuffer(data, frames, info, sample, ref packet);
        _pump.PostToUi(() => _onLevel(generation, vu));
    }

    private static double LevelFromBuffer(IntPtr data, uint frames, WasapiWaveFormat info, VuMeterCaptureSample sample, ref byte[] packet)
    {
        if (frames == 0 || info.BlockAlign <= 0 || info.Channels <= 0)
        {
            return VuMeterScale.ScaleMinimum;
        }

        WasapiPcmReader.CopyPacket(data, frames, info, ref packet);
        double sum = 0d;
        int used = 0;
        for (int frame = 0; frame < frames; frame++)
        {
            int frameOffset = frame * info.BlockAlign;
            double amplitude = WasapiPcmReader.ReadFrame(packet, frameOffset, info, sample.Channel);
            sum += amplitude * amplitude;
            used++;
        }

        if (used == 0)
        {
            return VuMeterScale.ScaleMinimum;
        }

        double rms = Math.Sqrt(sum / used);
        if (rms < 0.000000001d)
        {
            return VuMeterScale.ScaleMinimum;
        }

        double dbfs = 20d * Math.Log10(rms);
        return dbfs - sample.CalibrationOffsetDb;
    }
}

/// <summary>
/// Snapshot of the capture settings read on the audio thread.
/// </summary>
internal readonly struct VuMeterCaptureSample
{
    internal VuMeterCaptureSample(int generation, VuMeterChannel channel, double calibrationOffsetDb)
    {
        Generation = generation;
        Channel = channel;
        CalibrationOffsetDb = calibrationOffsetDb;
    }

    internal int Generation { get; }

    internal VuMeterChannel Channel { get; }

    internal double CalibrationOffsetDb { get; }
}
