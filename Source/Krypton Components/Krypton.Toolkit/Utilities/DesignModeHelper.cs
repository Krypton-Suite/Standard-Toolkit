#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    internal class DesignModeHelper
    {
        /// <summary>Determines whether [is in design mode].</summary>
        /// <returns><c>true</c> if [is in design mode]; otherwise, <c>false</c>.</returns>
        public static bool IsInDesignMode() =>
            // Check if the current application is running in design mode
            LicenseManager.UsageMode == LicenseUsageMode.Designtime && Process.GetCurrentProcess().ProcessName == "devenv";
    }
}
