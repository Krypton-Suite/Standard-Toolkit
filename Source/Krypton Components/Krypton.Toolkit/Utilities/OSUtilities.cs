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

namespace Krypton.Toolkit
{
    /// <summary>Gets access to specific information about the client operating system.</summary>
    public class OSUtilities
    {
        #region Private static vars
        // cache the result of RtlGetVersion
        private static bool? _isWindowsTen = null;
        // cache the result of RtlGetVersion
        private static bool? _isAtLeastWindowsEleven = null;
        #endregion

        #region Implementation

        // Note: Update these, once a new public upgrade becomes GA

        /// <summary>Gets a value indicating whether the client version is Windows 7.</summary>
        /// <value><c>true</c> if the client version is Windows 7; otherwise, <c>false</c>.</value>
        public static bool IsWindowsSeven => Environment.OSVersion.Version is { Major: 6, Minor: 1 };

        /// <summary>Gets a value indicating whether the client version is Windows 8.</summary>
        /// <value><c>true</c> if the client version is Windows 8; otherwise, <c>false</c>.</value>
        public static bool IsWindowsEight => Environment.OSVersion.Version is { Major: 6, Minor: 2 };

        /// <summary>Gets a value indicating whether the client version is Windows 8.1.</summary>
        /// <value><c>true</c> if the client version is Windows 8.1; otherwise, <c>false</c>.</value>
        public static bool IsWindowsEightPointOne => Environment.OSVersion.Version is { Major: 6, Minor: 3 };

        /// <summary>Gets a value indicating whether the client version is Windows 10.</summary>
        /// <value><c>true</c> if the client version is Windows 10; otherwise, <c>false</c>.</value>
        public static bool IsWindowsTen => _isWindowsTen ??= RtlGetVersion() is { dwMajorVersion: 10, dwBuildNumber: <= 19045 };

        /// <summary>Gets a value indicating whether the client version is Windows 11.</summary>
        /// <value><c>true</c> if the client version is Windows 11; otherwise, <c>false</c>.</value>
        public static bool IsAtLeastWindowsEleven => _isAtLeastWindowsEleven ??= RtlGetVersion() is { dwMajorVersion: >= 10, dwBuildNumber: > 19045 };

        /// <summary>Gets a value indicating whether the client is a 64 bit operating system.</summary>
        /// <value><c>true</c> if the client is a 64 bit operating system; otherwise, <c>false</c>.</value>
        public static bool Is64BitOperatingSystem => Environment.Is64BitOperatingSystem;

        /// <summary>
        /// Use is internal to this class only.
        /// </summary>
        /// <returns>PI.OSVERSIONINFOEX structure</returns>
        private static PI.OSVERSIONINFOEX RtlGetVersion()
        {
            PI.OSVERSIONINFOEX osvi = new()
            {
                dwOSVersionInfoSize = (uint)Marshal.SizeOf(typeof(PI.OSVERSIONINFOEX))
            };

            PI.RtlGetVersion(ref osvi);

            return osvi;
        }
        #endregion
    }
}