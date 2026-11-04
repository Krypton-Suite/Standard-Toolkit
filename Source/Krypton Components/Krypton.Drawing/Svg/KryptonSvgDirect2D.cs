#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Drawing;

/// <summary>
/// Parsed SVG drawn by a shared Direct2D device context.
/// </summary>
internal sealed class KryptonSvgDirect2DDocument : IKryptonSvgDocument
{
    private readonly string _svg;

    private KryptonSvgDirect2DDocument(string svg, SizeF intrinsicDipSize)
    {
        _svg = svg;
        IntrinsicDipSize = intrinsicDipSize;
    }

    /// <inheritdoc />
    public SizeF IntrinsicDipSize { get; }

    internal static KryptonSvgDirect2DDocument? TryOpen(string svg, out string? error)
    {
        if (!KryptonSvgIntrinsicSize.TryGet(svg, out var width, out var height))
        {
            error = "The SVG document has no drawable bounds.";
            return null;
        }

        // Parse once up front so a bad document sets SvgError before the first paint.
        if (!KryptonSvgDirect2D.TryParse(svg, out error))
        {
            return null;
        }

        return new KryptonSvgDirect2DDocument(svg, new SizeF(width, height));
    }

    /// <inheritdoc />
    public Bitmap Rasterize(int pixelWidth, int pixelHeight) => KryptonSvgDirect2D.Rasterize(_svg, pixelWidth, pixelHeight);

    /// <inheritdoc />
    public void Dispose()
    {
    }
}

/// <summary>
/// Process-wide Direct2D SVG session. Created once, on the UI thread, and reused for every picture.
/// </summary>
internal static class KryptonSvgDirect2D
{
    // Vtable slots include IUnknown. Counts are the PURE methods in d2d1.h, d2d1_1.h, and d2d1_3.h.
    private const int SlotCopyFromBitmap = 8;
    private const int SlotMap = 14;
    private const int SlotUnmap = 15;
    private const int SlotClear = 47;
    private const int SlotBeginDraw = 48;
    private const int SlotEndDraw = 49;
    private const int SlotSetDpi = 51;
    private const int SlotCreateBitmap = 57;
    private const int SlotSetTarget = 74;
    private const int SlotCreateSvgDocument = 115;
    private const int SlotDrawSvgDocument = 116;
    private const int SlotFactoryCreateDevice = 17;
    private const int SlotDevice5CreateContext = 19;

    private const int FormatBgra = 87;
    private const int AlphaPremultiplied = 1;
    private const int BitmapTarget = 1;
    private const int BitmapCpuRead = 4 | 2;
    private const int MapRead = 1;
    private const int DriverHardware = 1;
    private const int DriverWarp = 5;
    private const uint BgraSupport = 0x20;
    private const uint SdkVersion = 7;

    private static readonly object Gate = new object();
    private static Session? _session;
    private static bool _unavailable;

    internal static bool IsAvailable
    {
        get
        {
            lock (Gate)
            {
                return Ensure();
            }
        }
    }

    internal static bool TryParse(string svg, out string? error)
    {
        lock (Gate)
        {
            if (!Ensure() || _session == null)
            {
                error = "Direct2D SVG is not available on this version of Windows.";
                return false;
            }

            if (!_session.TryCreateDocument(svg, 1, 1, out var document, out error))
            {
                return false;
            }

            Release(document);
            error = null;
            return true;
        }
    }

    internal static Bitmap Rasterize(string svg, int pixelWidth, int pixelHeight)
    {
        lock (Gate)
        {
            if (!Ensure() || _session == null)
            {
                throw new InvalidOperationException("Direct2D SVG is not available on this version of Windows.");
            }

            return _session.Draw(svg, pixelWidth, pixelHeight);
        }
    }

    private static bool Ensure()
    {
        if (_session != null)
        {
            return true;
        }

        if (_unavailable)
        {
            return false;
        }

        _session = Session.TryCreate();
        _unavailable = _session == null;
        return _session != null;
    }

    private static long PackSize(float width, float height)
    {
        // D2D1_SIZE_F and D2D1_SIZE_U are 8-byte structs passed in one integer register.
        var widthBits = unchecked((uint)BitConverter.ToInt32(BitConverter.GetBytes(width), 0));
        var heightBits = unchecked((uint)BitConverter.ToInt32(BitConverter.GetBytes(height), 0));
        return widthBits | ((long)heightBits << 32);
    }

    private static long PackPixels(int width, int height) => (uint)width | ((long)(uint)height << 32);

    private static void Release(IntPtr comObject)
    {
        if (comObject == IntPtr.Zero)
        {
            return;
        }

        var vtable = Marshal.ReadIntPtr(comObject);
        var release = Marshal.ReadIntPtr(vtable, IntPtr.Size * 2);
        var fn = (ReleaseFn)Marshal.GetDelegateForFunctionPointer(release, typeof(ReleaseFn));
        fn(comObject);
    }

    private static int QueryInterface(IntPtr comObject, ref Guid iid, out IntPtr result)
    {
        var vtable = Marshal.ReadIntPtr(comObject);
        var query = Marshal.ReadIntPtr(vtable, 0);
        var fn = (QueryFn)Marshal.GetDelegateForFunctionPointer(query, typeof(QueryFn));
        return fn(comObject, ref iid, out result);
    }

    private static T DelegateAt<T>(IntPtr comObject, int slot)
        where T : class
    {
        var vtable = Marshal.ReadIntPtr(comObject);
        var method = Marshal.ReadIntPtr(vtable, slot * IntPtr.Size);
        return (T)(object)Marshal.GetDelegateForFunctionPointer(method, typeof(T));
    }

    private sealed class Session
    {
        private readonly IntPtr _context;
        private readonly CreateSvgDocumentFn _createSvg;
        private readonly DrawSvgDocumentFn _drawSvg;
        private readonly SetTargetFn _setTarget;
        private readonly ClearFn _clear;
        private readonly BeginDrawFn _beginDraw;
        private readonly EndDrawFn _endDraw;
        private readonly CreateBitmapFn _createBitmap;

        private Session(
            IntPtr context,
            CreateSvgDocumentFn createSvg,
            DrawSvgDocumentFn drawSvg,
            SetTargetFn setTarget,
            ClearFn clear,
            BeginDrawFn beginDraw,
            EndDrawFn endDraw,
            CreateBitmapFn createBitmap)
        {
            _context = context;
            _createSvg = createSvg;
            _drawSvg = drawSvg;
            _setTarget = setTarget;
            _clear = clear;
            _beginDraw = beginDraw;
            _endDraw = endDraw;
            _createBitmap = createBitmap;
        }

        internal static Session? TryCreate()
        {
            var factory = IntPtr.Zero;
            var d3d = IntPtr.Zero;
            var d3dContext = IntPtr.Zero;
            var dxgi = IntPtr.Zero;
            var device = IntPtr.Zero;
            var device5 = IntPtr.Zero;
            var context = IntPtr.Zero;
            var keepContext = false;
            try
            {
                var factoryId = new Guid("bb12d362-daee-4b9a-aa1d-14ba401cfa1f");
                if (D2D1CreateFactory(0, ref factoryId, IntPtr.Zero, out factory) < 0 || factory == IntPtr.Zero)
                {
                    return null;
                }

                if (!TryCreateD3DDevice(out d3d, out d3dContext))
                {
                    return null;
                }

                var dxgiId = new Guid("54ec77fa-1377-44e6-8c32-88fd5f44c84c");
                if (QueryInterface(d3d, ref dxgiId, out dxgi) < 0 || dxgi == IntPtr.Zero)
                {
                    return null;
                }

                if (DelegateAt<CreateDeviceFn>(factory, SlotFactoryCreateDevice)(factory, dxgi, out device) < 0 || device == IntPtr.Zero)
                {
                    return null;
                }

                var device5Id = new Guid("d55ba0a4-6405-4694-aef5-08ee1a4358b4");
                if (QueryInterface(device, ref device5Id, out device5) < 0 || device5 == IntPtr.Zero)
                {
                    return null;
                }

                if (DelegateAt<CreateDeviceContextFn>(device5, SlotDevice5CreateContext)(device5, 0, out context) < 0 || context == IntPtr.Zero)
                {
                    return null;
                }

                // Keep 1 DIP equal to 1 bitmap pixel. The control already scales by DeviceDpi.
                DelegateAt<SetDpiFn>(context, SlotSetDpi)(context, 96f, 96f);

                // A 1x1 CPU-readable bitmap confirms this device can hand pixels back to GDI+.
                var staging = CreateBitmap(context, 1, 1, BitmapCpuRead);
                if (staging == IntPtr.Zero)
                {
                    return null;
                }

                Release(staging);
                keepContext = true;
                return new Session(
                    context,
                    DelegateAt<CreateSvgDocumentFn>(context, SlotCreateSvgDocument),
                    DelegateAt<DrawSvgDocumentFn>(context, SlotDrawSvgDocument),
                    DelegateAt<SetTargetFn>(context, SlotSetTarget),
                    DelegateAt<ClearFn>(context, SlotClear),
                    DelegateAt<BeginDrawFn>(context, SlotBeginDraw),
                    DelegateAt<EndDrawFn>(context, SlotEndDraw),
                    DelegateAt<CreateBitmapFn>(context, SlotCreateBitmap));
            }
            finally
            {
                if (!keepContext)
                {
                    Release(context);
                }

                Release(device5);
                Release(device);
                Release(dxgi);
                Release(d3dContext);
                Release(d3d);
                Release(factory);
            }
        }

        internal bool TryCreateDocument(string svg, int width, int height, out IntPtr document, out string? error)
        {
            document = IntPtr.Zero;
            var bytes = Encoding.UTF8.GetBytes(svg);
            var stream = SHCreateMemStream(bytes, (uint)bytes.Length);
            if (stream == IntPtr.Zero)
            {
                error = "The SVG document could not be parsed.";
                return false;
            }

            try
            {
                var hr = _createSvg(_context, stream, PackSize(Math.Max(width, 1), Math.Max(height, 1)), out document);
                if (hr < 0 || document == IntPtr.Zero)
                {
                    Release(document);
                    document = IntPtr.Zero;
                    error = "The SVG document could not be parsed.";
                    return false;
                }

                error = null;
                return true;
            }
            finally
            {
                Release(stream);
            }
        }

        internal Bitmap Draw(string svg, int pixelWidth, int pixelHeight)
        {
            if (!TryCreateDocument(svg, pixelWidth, pixelHeight, out var document, out var error))
            {
                throw new InvalidOperationException(error ?? "The SVG document could not be parsed.");
            }

            var target = IntPtr.Zero;
            var staging = IntPtr.Zero;
            var mapped = false;
            try
            {
                target = CreateOwnBitmap(pixelWidth, pixelHeight, BitmapTarget);
                _setTarget(_context, target);
                _beginDraw(_context);
                _clear(_context, IntPtr.Zero);
                _drawSvg(_context, document);
                var hr = _endDraw(_context, IntPtr.Zero, IntPtr.Zero);
                _setTarget(_context, IntPtr.Zero);
                if (hr < 0)
                {
                    throw new InvalidOperationException("The SVG document could not be drawn.");
                }

                // A target bitmap cannot be mapped. Copy into a CPU-readable bitmap, then read the pixels.
                staging = CreateOwnBitmap(pixelWidth, pixelHeight, BitmapCpuRead);
                // CopyFromBitmap, Map, and Unmap live on the bitmap, not the device context.
                var copy = DelegateAt<CopyFromBitmapFn>(staging, SlotCopyFromBitmap);
                if (copy(staging, IntPtr.Zero, target, IntPtr.Zero) < 0)
                {
                    throw new InvalidOperationException("The SVG document could not be drawn.");
                }

                var mappedRect = new MappedRect();
                var map = DelegateAt<MapFn>(staging, SlotMap);
                if (map(staging, MapRead, ref mappedRect) < 0 || mappedRect.Bits == IntPtr.Zero)
                {
                    throw new InvalidOperationException("The SVG document could not be drawn.");
                }

                mapped = true;
                return CopyToGdiBitmap(mappedRect, pixelWidth, pixelHeight);
            }
            finally
            {
                if (mapped)
                {
                    DelegateAt<UnmapFn>(staging, SlotUnmap)(staging);
                }

                Release(staging);
                Release(target);
                Release(document);
            }
        }

        private IntPtr CreateOwnBitmap(int width, int height, int options)
        {
            var bitmap = CreateBitmap(_context, width, height, options, _createBitmap);
            if (bitmap == IntPtr.Zero)
            {
                throw new InvalidOperationException("The SVG document could not be drawn.");
            }

            return bitmap;
        }
    }

    private static IntPtr CreateBitmap(IntPtr context, int width, int height, int options)
        => CreateBitmap(context, width, height, options, DelegateAt<CreateBitmapFn>(context, SlotCreateBitmap));

    private static IntPtr CreateBitmap(IntPtr context, int width, int height, int options, CreateBitmapFn create)
    {
        var props = new[]
        {
            new BitmapProperties1
            {
                Format = FormatBgra,
                AlphaMode = AlphaPremultiplied,
                DpiX = 96f,
                DpiY = 96f,
                Options = options
            }
        };
        var pin = GCHandle.Alloc(props, GCHandleType.Pinned);
        try
        {
            var hr = create(context, PackPixels(width, height), IntPtr.Zero, 0, pin.AddrOfPinnedObject(), out var bitmap);
            return hr < 0 ? IntPtr.Zero : bitmap;
        }
        finally
        {
            pin.Free();
        }
    }

    private static Bitmap CopyToGdiBitmap(MappedRect source, int width, int height)
    {
        var bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
        var rowBytes = width * 4;
        var row = new byte[rowBytes];
        var bits = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
        try
        {
            for (var y = 0; y < height; y++)
            {
                Marshal.Copy(IntPtr.Add(source.Bits, y * (int)source.Pitch), row, 0, rowBytes);
                var destRow = bits.Stride < 0 ? height - 1 - y : y;
                Marshal.Copy(row, 0, IntPtr.Add(bits.Scan0, destRow * bits.Stride), rowBytes);
            }
        }
        finally
        {
            bitmap.UnlockBits(bits);
        }

        return bitmap;
    }

    private static bool TryCreateD3DDevice(out IntPtr device, out IntPtr context)
    {
        if (D3D11CreateDevice(IntPtr.Zero, DriverHardware, IntPtr.Zero, BgraSupport, IntPtr.Zero, 0, SdkVersion, out device, out _, out context) >= 0
            && device != IntPtr.Zero)
        {
            return true;
        }

        Release(device);
        Release(context);
        device = IntPtr.Zero;
        context = IntPtr.Zero;
        if (D3D11CreateDevice(IntPtr.Zero, DriverWarp, IntPtr.Zero, BgraSupport, IntPtr.Zero, 0, SdkVersion, out device, out _, out context) >= 0
            && device != IntPtr.Zero)
        {
            return true;
        }

        Release(device);
        Release(context);
        device = IntPtr.Zero;
        context = IntPtr.Zero;
        return false;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct BitmapProperties1
    {
        [FieldOffset(0)] public int Format;
        [FieldOffset(4)] public int AlphaMode;
        [FieldOffset(8)] public float DpiX;
        [FieldOffset(12)] public float DpiY;
        [FieldOffset(16)] public int Options;
        [FieldOffset(24)] public IntPtr ColorContext;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct MappedRect
    {
        [FieldOffset(0)] public uint Pitch;
        [FieldOffset(8)] public IntPtr Bits;
    }

    [DllImport("d2d1.dll")]
    private static extern int D2D1CreateFactory(uint factoryType, ref Guid riid, IntPtr factoryOptions, out IntPtr factory);

    [DllImport("d3d11.dll")]
    private static extern int D3D11CreateDevice(
        IntPtr adapter,
        int driverType,
        IntPtr software,
        uint flags,
        IntPtr featureLevels,
        uint featureLevelCount,
        uint sdkVersion,
        out IntPtr device,
        out int featureLevel,
        out IntPtr immediateContext);

    [DllImport("shlwapi.dll", ExactSpelling = true)]
    private static extern IntPtr SHCreateMemStream(byte[] initial, uint size);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate uint ReleaseFn(IntPtr self);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int QueryFn(IntPtr self, ref Guid iid, out IntPtr result);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int CreateDeviceFn(IntPtr self, IntPtr dxgiDevice, out IntPtr device);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int CreateDeviceContextFn(IntPtr self, int options, out IntPtr context);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void SetDpiFn(IntPtr self, float dpiX, float dpiY);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int CreateSvgDocumentFn(IntPtr self, IntPtr stream, long viewport, out IntPtr document);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void DrawSvgDocumentFn(IntPtr self, IntPtr document);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void SetTargetFn(IntPtr self, IntPtr image);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void ClearFn(IntPtr self, IntPtr color);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void BeginDrawFn(IntPtr self);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int EndDrawFn(IntPtr self, IntPtr tag1, IntPtr tag2);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int CreateBitmapFn(IntPtr self, long size, IntPtr source, uint pitch, IntPtr properties, out IntPtr bitmap);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int CopyFromBitmapFn(IntPtr self, IntPtr destPoint, IntPtr bitmap, IntPtr sourceRect);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int MapFn(IntPtr self, int options, ref MappedRect mapped);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int UnmapFn(IntPtr self);
}
