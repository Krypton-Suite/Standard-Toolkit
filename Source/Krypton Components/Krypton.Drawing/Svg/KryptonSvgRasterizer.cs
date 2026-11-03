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
/// Selects how <see cref="KryptonSVGPictureBox"/> turns SVG markup into a bitmap.
/// </summary>
public enum KryptonSvgRasterizer
{
    /// <summary>
    /// Windows Direct2D <c>ID2D1SvgDocument</c>. Used on Windows 10 version 1703 and later.
    /// </summary>
    Direct2D = 0,

    /// <summary>
    /// Svg.Skia and SkiaSharp. Used on Windows 7, 8, and 8.1, and on Windows 10 builds before 1703.
    /// </summary>
    Skia = 1
}

/// <summary>
/// Chooses the SVG rasterizer once. <c>KRYPTON_SVG_RASTERIZER</c> (<c>Direct2D</c> or <c>Skia</c>) overrides the probe for tests.
/// </summary>
internal static class KryptonSvgRasterizerChoice
{
    private const int Windows10SvgBuild = 15063;

    private static KryptonSvgRasterizer? _active;

    internal static KryptonSvgRasterizer Active => _active ?? (_active = Resolve()).Value;

    private static KryptonSvgRasterizer Resolve()
    {
        var forced = Environment.GetEnvironmentVariable("KRYPTON_SVG_RASTERIZER");
        if (string.Equals(forced, "Direct2D", StringComparison.OrdinalIgnoreCase))
        {
            return KryptonSvgRasterizer.Direct2D;
        }

#if NETFRAMEWORK
        if (string.Equals(forced, "Skia", StringComparison.OrdinalIgnoreCase))
        {
            return KryptonSvgRasterizer.Skia;
        }

        // RtlGetVersion ignores the process manifest, which otherwise reports Windows 8.1.
        if (!IsWindows10Version1703OrLater() || !KryptonSvgDirect2D.IsAvailable)
        {
            return KryptonSvgRasterizer.Skia;
        }
#endif
        return KryptonSvgRasterizer.Direct2D;
    }

    private static bool IsWindows10Version1703OrLater()
    {
        var info = new RtlOsVersionInfo { dwOSVersionInfoSize = (uint)Marshal.SizeOf(typeof(RtlOsVersionInfo)) };
        if (RtlGetVersion(ref info) != 0)
        {
            return false;
        }

        return info.dwMajorVersion > 10
               || (info.dwMajorVersion == 10 && info.dwBuildNumber >= Windows10SvgBuild);
    }

    [DllImport("ntdll.dll")]
    private static extern int RtlGetVersion(ref RtlOsVersionInfo versionInfo);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct RtlOsVersionInfo
    {
        public uint dwOSVersionInfoSize;
        public uint dwMajorVersion;
        public uint dwMinorVersion;
        public uint dwBuildNumber;
        public uint dwPlatformId;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szCSDVersion;
    }
}
