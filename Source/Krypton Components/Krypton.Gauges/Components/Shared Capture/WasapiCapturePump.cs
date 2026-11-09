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
/// Opens the default WASAPI capture endpoint on a background thread and delivers PCM packets.
/// Shared by the VU meter and equalizer. No extra audio package.
/// </summary>
internal sealed class WasapiCapturePump : IDisposable
{
    private const int DataFlowCapture = 1;
    private const int RoleConsole = 0;
    private const int ClsCtxAll = 23;
    private const int CoInitMultithreaded = 0;
    private const int RpcEChangedMode = unchecked((int)0x80010106);
    private const ushort WaveFormatPcm = 1;
    private const ushort WaveFormatIeeeFloat = 3;
    private const ushort WaveFormatExtensible = 0xFFFE;

    private static readonly Guid SubTypeIeeeFloat = new Guid("00000003-0000-0010-8000-00aa00389b71");
    private static readonly Guid SubTypePcm = new Guid("00000001-0000-0010-8000-00aa00389b71");

    /// <summary>
    /// Silent-packet flag from <c>IAudioCaptureClient.GetBuffer</c>.
    /// </summary>
    internal const uint SilentFlag = 0x2;

    private readonly Control _ui;
    private readonly Func<int> _readGeneration;
    private readonly WasapiCapturePacketHandler _onPacket;
    private readonly Action<Exception> _onFailed;
    private readonly Thread _thread;
    private volatile bool _stop;
    private bool _disposed;

    /// <summary>
    /// Initialize a new instance of the <see cref="WasapiCapturePump"/> class.
    /// </summary>
    /// <param name="ui">Control used to marshal failures and to decide when the handle is gone.</param>
    /// <param name="readGeneration">Returns the active generation, or a negative value to stop the pump.</param>
    /// <param name="onPacket">Invoked on the capture thread for each non-empty buffer.</param>
    /// <param name="onFailed">Invoked on the UI thread when the pump cannot continue.</param>
    /// <param name="threadName">Background thread name.</param>
    internal WasapiCapturePump(Control ui,
        Func<int> readGeneration,
        WasapiCapturePacketHandler onPacket,
        Action<Exception> onFailed,
        string threadName)
    {
        _ui = ui;
        _readGeneration = readGeneration;
        _onPacket = onPacket;
        _onFailed = onFailed;
        _thread = new Thread(CaptureLoop)
        {
            IsBackground = true,
            Name = threadName
        };
        _thread.Start();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _stop = true;
        if (_thread.IsAlive)
        {
            _thread.Join(1000);
        }
    }

    /// <summary>
    /// Posts <paramref name="action"/> onto the UI thread when the control handle is still alive.
    /// </summary>
    /// <param name="action">Work to run on the UI thread.</param>
    internal void PostToUi(Action action)
    {
        if (_stop || _ui.IsDisposed || !_ui.IsHandleCreated)
        {
            return;
        }

        try
        {
            _ui.BeginInvoke(action);
        }
        catch (InvalidOperationException)
        {
            // The control handle went away between the check and the post.
        }
    }

    private void CaptureLoop()
    {
        int coHr = CoInitializeEx(IntPtr.Zero, CoInitMultithreaded);
        bool uninitialize = coHr >= 0;
        object? enumerator = null;
        object? device = null;
        IAudioClient? client = null;
        IAudioCaptureClient? capture = null;
        IntPtr format = IntPtr.Zero;
        bool started = false;

        try
        {
            if (coHr < 0 && coHr != RpcEChangedMode)
            {
                throw new InvalidOperationException("COM could not be initialized for capture (0x" + coHr.ToString("X8", CultureInfo.InvariantCulture) + ").");
            }

            enumerator = new MmDeviceEnumeratorComObject();
            var devices = (IMmDeviceEnumerator)enumerator;
            int hr = devices.GetDefaultAudioEndpoint(DataFlowCapture, RoleConsole, out IMmDevice endpoint);
            if (hr < 0 || endpoint == null)
            {
                throw new InvalidOperationException("No default recording device is available (0x" + hr.ToString("X8", CultureInfo.InvariantCulture) + ").");
            }

            device = endpoint;
            Guid clientId = typeof(IAudioClient).GUID;
            hr = endpoint.Activate(ref clientId, ClsCtxAll, IntPtr.Zero, out client);
            if (hr < 0 || client == null)
            {
                throw new InvalidOperationException("The recording device could not be opened (0x" + hr.ToString("X8", CultureInfo.InvariantCulture) + ").");
            }

            hr = client.GetMixFormat(out format);
            if (hr < 0 || format == IntPtr.Zero)
            {
                throw new InvalidOperationException("The recording format could not be read (0x" + hr.ToString("X8", CultureInfo.InvariantCulture) + ").");
            }

            WasapiWaveFormat info = ReadFormat(format);
            long period = 1000000;
            hr = client.GetDevicePeriod(out long defaultPeriod, out _);
            if (hr >= 0 && defaultPeriod > 0)
            {
                period = ((1000000 + defaultPeriod - 1) / defaultPeriod) * defaultPeriod;
            }

            hr = client.Initialize(0, 0, period, 0, format, IntPtr.Zero);
            CoTaskMemFree(format);
            format = IntPtr.Zero;
            if (hr < 0)
            {
                throw new InvalidOperationException("The recording client could not start (0x" + hr.ToString("X8", CultureInfo.InvariantCulture) + ").");
            }

            Guid captureId = typeof(IAudioCaptureClient).GUID;
            hr = client.GetService(ref captureId, out capture);
            if (hr < 0 || capture == null)
            {
                throw new InvalidOperationException("Capture buffers are not available (0x" + hr.ToString("X8", CultureInfo.InvariantCulture) + ").");
            }

            hr = client.Start();
            if (hr < 0)
            {
                throw new InvalidOperationException("Recording could not be started (0x" + hr.ToString("X8", CultureInfo.InvariantCulture) + ").");
            }

            started = true;
            byte[] packet = Array.Empty<byte>();
            while (!_stop)
            {
                hr = capture.GetNextPacketSize(out uint frames);
                if (hr < 0)
                {
                    throw new InvalidOperationException("Recording was interrupted (0x" + hr.ToString("X8", CultureInfo.InvariantCulture) + ").");
                }

                if (frames == 0)
                {
                    Thread.Sleep(10);
                    continue;
                }

                hr = capture.GetBuffer(out IntPtr data, out uint count, out uint flags, out _, out _);
                if (hr < 0)
                {
                    throw new InvalidOperationException("A recording buffer could not be read (0x" + hr.ToString("X8", CultureInfo.InvariantCulture) + ").");
                }

                try
                {
                    int generation = _readGeneration();
                    if (generation < 0)
                    {
                        break;
                    }

                    _onPacket(generation, data, count, flags, info, ref packet);
                }
                finally
                {
                    capture.ReleaseBuffer(count);
                }
            }
        }
        catch (Exception ex)
        {
            if (!_stop)
            {
                PostToUi(() => _onFailed(ex));
            }
        }
        finally
        {
            if (started && client != null)
            {
                try
                {
                    client.Stop();
                }
                catch (COMException)
                {
                    // The device can already be gone when capture stops.
                }
            }

            if (format != IntPtr.Zero)
            {
                CoTaskMemFree(format);
            }

            Release(capture);
            Release(client);
            Release(device);
            Release(enumerator);
            if (uninitialize)
            {
                CoUninitialize();
            }
        }
    }

    private static WasapiWaveFormat ReadFormat(IntPtr format)
    {
        ushort formatTag = (ushort)Marshal.ReadInt16(format, 0);
        ushort channels = (ushort)Marshal.ReadInt16(format, 2);
        int sampleRate = Marshal.ReadInt32(format, 4);
        ushort blockAlign = (ushort)Marshal.ReadInt16(format, 12);
        ushort bits = (ushort)Marshal.ReadInt16(format, 14);
        ushort extra = (ushort)Marshal.ReadInt16(format, 16);
        bool isFloat = formatTag == WaveFormatIeeeFloat;
        if (formatTag == WaveFormatExtensible && extra >= 22)
        {
            var subType = (Guid)Marshal.PtrToStructure(IntPtr.Add(format, 24), typeof(Guid))!;
            isFloat = subType == SubTypeIeeeFloat;
            if (!isFloat && subType != SubTypePcm)
            {
                throw new InvalidOperationException("The recording format is not PCM or floating point.");
            }
        }
        else if (formatTag != WaveFormatPcm && formatTag != WaveFormatIeeeFloat)
        {
            throw new InvalidOperationException("The recording format is not PCM or floating point.");
        }

        int bytesPerSample = bits <= 16 ? 2 : 4;
        if (channels == 0 || blockAlign == 0 || sampleRate <= 0)
        {
            throw new InvalidOperationException("The recording format is empty.");
        }

        return new WasapiWaveFormat(channels, blockAlign, bits, bytesPerSample, isFloat, sampleRate);
    }

    private static void Release(object? comObject)
    {
        if (comObject != null && Marshal.IsComObject(comObject))
        {
            Marshal.ReleaseComObject(comObject);
        }
    }

    [DllImport("ole32.dll", ExactSpelling = true)]
    private static extern int CoInitializeEx(IntPtr reserved, uint coInit);

    [DllImport("ole32.dll", ExactSpelling = true)]
    private static extern void CoUninitialize();

    [DllImport("ole32.dll", ExactSpelling = true)]
    private static extern void CoTaskMemFree(IntPtr pointer);
}

/// <summary>
/// Receives one WASAPI capture packet on the audio thread.
/// </summary>
/// <param name="generation">Capture generation from the owning control.</param>
/// <param name="data">Native buffer pointer.</param>
/// <param name="frames">Number of frames in the buffer.</param>
/// <param name="flags">Buffer flags (see <see cref="WasapiCapturePump.SilentFlag"/>).</param>
/// <param name="format">Mix format of the capture client.</param>
/// <param name="packet">Reusable managed copy buffer.</param>
internal delegate void WasapiCapturePacketHandler(int generation, IntPtr data, uint frames, uint flags, WasapiWaveFormat format, ref byte[] packet);

/// <summary>
/// Mix-format fields used while decoding capture buffers.
/// </summary>
internal readonly struct WasapiWaveFormat
{
    internal WasapiWaveFormat(int channels, int blockAlign, int bitsPerSample, int bytesPerSample, bool isFloat, int sampleRate)
    {
        Channels = channels;
        BlockAlign = blockAlign;
        BitsPerSample = bitsPerSample;
        BytesPerSample = bytesPerSample;
        IsFloat = isFloat;
        SampleRate = sampleRate;
    }

    internal int Channels { get; }

    internal int BlockAlign { get; }

    internal int BitsPerSample { get; }

    internal int BytesPerSample { get; }

    internal bool IsFloat { get; }

    internal int SampleRate { get; }
}

/// <summary>
/// Decodes PCM/float frames from a WASAPI capture buffer.
/// </summary>
internal static class WasapiPcmReader
{
    /// <summary>
    /// Copies <paramref name="data"/> into <paramref name="packet"/> and returns the byte count.
    /// </summary>
    internal static int CopyPacket(IntPtr data, uint frames, WasapiWaveFormat format, ref byte[] packet)
    {
        if (frames == 0 || format.BlockAlign <= 0)
        {
            return 0;
        }

        int byteCount = checked((int)frames * format.BlockAlign);
        if (packet.Length < byteCount)
        {
            packet = new byte[byteCount];
        }

        Marshal.Copy(data, packet, 0, byteCount);
        return byteCount;
    }

    /// <summary>
    /// Reads one frame amplitude for <paramref name="channel"/>.
    /// </summary>
    internal static double ReadFrame(byte[] packet, int frameOffset, WasapiWaveFormat format, VuMeterChannel channel)
    {
        if (channel == VuMeterChannel.Mix)
        {
            double sum = 0d;
            for (int index = 0; index < format.Channels; index++)
            {
                sum += ReadSample(packet, frameOffset + (index * format.BytesPerSample), format);
            }

            return sum / format.Channels;
        }

        int channelIndex = channel == VuMeterChannel.Right && format.Channels > 1 ? 1 : 0;
        return ReadSample(packet, frameOffset + (channelIndex * format.BytesPerSample), format);
    }

    private static double ReadSample(byte[] packet, int offset, WasapiWaveFormat format)
    {
        if (offset < 0 || offset + format.BytesPerSample > packet.Length)
        {
            return 0d;
        }

        if (format.IsFloat)
        {
            return BitConverter.ToSingle(packet, offset);
        }

        if (format.BitsPerSample <= 16)
        {
            return BitConverter.ToInt16(packet, offset) / 32768d;
        }

        return BitConverter.ToInt32(packet, offset) / 2147483648d;
    }
}

[ComImport]
[Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
internal class MmDeviceEnumeratorComObject
{
}

[ComImport]
[Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IMmDeviceEnumerator
{
    [PreserveSig]
    int EnumAudioEndpoints(int dataFlow, int stateMask, out IntPtr devices);

    [PreserveSig]
    int GetDefaultAudioEndpoint(int dataFlow, int role, out IMmDevice device);

    [PreserveSig]
    int GetDevice([MarshalAs(UnmanagedType.LPWStr)] string id, out IMmDevice device);

    [PreserveSig]
    int RegisterEndpointNotificationCallback(IntPtr client);

    [PreserveSig]
    int UnregisterEndpointNotificationCallback(IntPtr client);
}

[ComImport]
[Guid("D666063F-1587-4E43-81F1-B948E807363F")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IMmDevice
{
    [PreserveSig]
    int Activate(ref Guid interfaceId, int classContext, IntPtr activationParams, out IAudioClient client);

    [PreserveSig]
    int OpenPropertyStore(int access, out IntPtr store);

    [PreserveSig]
    int GetId(out IntPtr id);

    [PreserveSig]
    int GetState(out int state);
}

[ComImport]
[Guid("1CB9AD4C-DBFA-4c32-B178-C2F568A703B2")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IAudioClient
{
    [PreserveSig]
    int Initialize(int shareMode, uint streamFlags, long bufferDuration, long periodicity, IntPtr format, IntPtr sessionGuid);

    [PreserveSig]
    int GetBufferSize(out uint bufferFrames);

    [PreserveSig]
    int GetStreamLatency(out long latency);

    [PreserveSig]
    int GetCurrentPadding(out uint paddingFrames);

    [PreserveSig]
    int IsFormatSupported(int shareMode, IntPtr format, out IntPtr closestMatch);

    [PreserveSig]
    int GetMixFormat(out IntPtr deviceFormat);

    [PreserveSig]
    int GetDevicePeriod(out long defaultPeriod, out long minimumPeriod);

    [PreserveSig]
    int Start();

    [PreserveSig]
    int Stop();

    [PreserveSig]
    int Reset();

    [PreserveSig]
    int SetEventHandle(IntPtr eventHandle);

    [PreserveSig]
    int GetService(ref Guid interfaceId, out IAudioCaptureClient service);
}

[ComImport]
[Guid("C8ADBD64-E71E-48a0-A4DE-185C395CD317")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IAudioCaptureClient
{
    [PreserveSig]
    int GetBuffer(out IntPtr data, out uint frameCount, out uint flags, out ulong devicePosition, out ulong counterPosition);

    [PreserveSig]
    int ReleaseBuffer(uint frameCount);

    [PreserveSig]
    int GetNextPacketSize(out uint frameCount);
}
