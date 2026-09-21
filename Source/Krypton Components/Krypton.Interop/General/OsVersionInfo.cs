#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Interop;

/// <summary>
/// Version data obtained via the RtlGetVersion API call.<br/>
/// Used by static class OSUtilities
/// </summary>
public class OsVersionInfo
{
    // Call refresh before first use / after instantiation.
    public void Refresh()
    {
        NativeFunctions.OSVERSIONINFOEX osvi = new()
        {
            dwOSVersionInfoSize = (uint)Marshal.SizeOf<NativeFunctions.OSVERSIONINFOEX>()
        };
        NativeFunctions.RtlGetVersion(ref osvi);

        MajorVersion = ((int)osvi.dwMajorVersion);
        MinorVersion = ((int)osvi.dwMinorVersion);
        BuildNumber = ((int)osvi.dwBuildNumber);
        PlatformId = ((int)osvi.dwPlatformId);
        CSDVersion = osvi.szCSDVersion;
        ServicePackMajor = ((short)osvi.wServicePackMajor);
        ServicePackMinor = ((short)osvi.wServicePackMinor);
        SuiteMask = ((short)osvi.wSuiteMask);
        ProductType = osvi.wProductType;
    }

    public int MajorVersion { get; private set; }
    public int MinorVersion { get; private set; }
    public int BuildNumber { get; private set; }
    public int PlatformId { get; private set; }
    public string CSDVersion { get; private set; }
    public short ServicePackMajor { get; private set; }
    public short ServicePackMinor { get; private set; }
    public short SuiteMask { get; private set; }
    public byte ProductType { get; private set; }
}