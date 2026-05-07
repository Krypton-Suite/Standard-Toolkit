#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class DesignModeHelper
{
    #region Public

    /// <summary>
    /// Gets a value indicating whether the application is running inside the Visual Studio designer.
    /// </summary>
    public static bool IsInDesignMode { get; }

    #endregion

    #region Identity

    /// <summary>
    /// Initializes the <see cref="DesignModeHelper"/> class.
    /// </summary>
    static DesignModeHelper()
    {
        IsInDesignMode = LicenseManager.UsageMode == LicenseUsageMode.Designtime &&
                         Process.GetCurrentProcess().ProcessName == "devenv";
    }

    #endregion
}