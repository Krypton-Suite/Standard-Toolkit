#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Gets access to specific information about the client operating system.</summary>
public class OSUtilities
{
    #region Static Identity
    static OSUtilities()
    {
        OsVersionInfo = new OsVersionInfo();
        Refresh();
    }
    #endregion

    #region Implementation
    // Note: Update these, once a new public upgrade becomes GA

    /// <summary>Gets a value indicating whether the client version is Windows 7.</summary>
    /// <value><c>true</c> if the client version is Windows 7; otherwise, <c>false</c>.</value>
    public static bool IsWindowsSeven { get; private set; }

    /// <summary>Gets a value indicating whether the client version is Windows 8.</summary>
    /// <value><c>true</c> if the client version is Windows 8; otherwise, <c>false</c>.</value>
    public static bool IsWindowsEight { get; private set; }

    /// <summary>Gets a value indicating whether the client version is Windows 8.1.</summary>
    /// <value><c>true</c> if the client version is Windows 8.1; otherwise, <c>false</c>.</value>
    public static bool IsWindowsEightPointOne { get; private set; }

    /// <summary>Gets a value indicating whether the client version is Windows 10.</summary>
    /// <value><c>true</c> if the client version is Windows 10; otherwise, <c>false</c>.</value>
    public static bool IsWindowsTen { get; private set; }

    /// <summary>Gets a value indicating whether the client version is Windows 11.</summary>
    /// <value><c>true</c> if the client version is Windows 11; otherwise, <c>false</c>.</value>
    public static bool IsWindowsEleven { get; private set; }

    /// <summary>Gets a value indicating whether the client version is Windows 11.</summary>
    /// <value><c>true</c> if the client version is Windows 11; otherwise, <c>false</c>.</value>
    public static bool IsAtLeastWindowsEleven { get; private set; }

    /// <summary>Gets a value indicating whether the client is a 64 bit operating system.</summary>
    /// <value><c>true</c> if the client is a 64 bit operating system; otherwise, <c>false</c>.</value>
    public static bool Is64BitOperatingSystem { get; private set; }

    /// <summary>OSVersionInfo data obtained from the Windows Api call RtlGetVersion.</summary>
    public static OsVersionInfo OsVersionInfo { get; private set; }

    /// <summary> Rereads the version info. </summary>
    public static void Refresh()
    {
        // First update OsVersionEx data
        OsVersionInfo.Refresh();

        // Set the properties
        IsWindowsSeven = Environment.OSVersion.Version is { Major: 6, Minor: 1 };
        IsWindowsEight = Environment.OSVersion.Version is { Major: 6, Minor: 2 };
        IsWindowsEightPointOne = Environment.OSVersion.Version is { Major: 6, Minor: 3 };
        IsWindowsTen = OsVersionInfo is { MajorVersion: 10, BuildNumber: <= 19045 }; 
        IsWindowsEleven = OsVersionInfo is { MajorVersion: 10, BuildNumber: > 19045 }; // needs an update when the next release comes out.
        IsAtLeastWindowsEleven = OsVersionInfo is { MajorVersion: >= 10, BuildNumber: > 19045 }; // needs an update when the next release comes out.
        Is64BitOperatingSystem = Environment.Is64BitOperatingSystem;
    }
    #endregion
}

/// <summary>
/// Version data obtained via the RtlGetVersion API call.<br/>
/// Used by static class OSUtilities
/// </summary>
public class OsVersionInfo
{
    // Call refresh before first use / after instantiation.
    public void Refresh()
    {
        PI.OSVERSIONINFOEX osvi = new()
        {
            dwOSVersionInfoSize = (uint)Marshal.SizeOf<PI.OSVERSIONINFOEX>()
        };
        PI.RtlGetVersion(ref osvi);

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
