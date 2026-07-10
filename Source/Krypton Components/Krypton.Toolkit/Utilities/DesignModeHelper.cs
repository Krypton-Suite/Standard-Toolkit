#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2025 - 2026. All rights reserved.
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

    /// <summary>
    /// Gets whether designer-editor footers should include the local theme selector.
    /// </summary>
    /// <remarks>
    /// The selector is hidden when hosted in Visual Studio 2022 (major version 17) where footer
    /// space is constrained and the edited component palette is already resolved from context.
    /// </remarks>
    public static bool IncludeDesignerEditorThemeSelector { get; }

    #endregion

    #region Identity

    /// <summary>
    /// Initializes the <see cref="DesignModeHelper"/> class.
    /// </summary>
    static DesignModeHelper()
    {
        IsInDesignMode = LicenseManager.UsageMode == LicenseUsageMode.Designtime &&
                         Process.GetCurrentProcess().ProcessName == "devenv";
        IncludeDesignerEditorThemeSelector = !IsInDesignMode
            || !TryGetHostingVisualStudioMajorVersion(out var majorVersion)
            || majorVersion != 17;
    }

    #endregion

    #region Implementation

    private static bool TryGetHostingVisualStudioMajorVersion(out int majorVersion)
    {
        majorVersion = 0;

        try
        {
            var fileName = Process.GetCurrentProcess().MainModule?.FileName;
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            var productVersion = FileVersionInfo.GetVersionInfo(fileName).ProductVersion;
            if (string.IsNullOrEmpty(productVersion))
            {
                return false;
            }

            var separator = productVersion.IndexOf('.');
            var majorPart = separator < 0 ? productVersion : productVersion.Substring(0, separator);
            return int.TryParse(majorPart, out majorVersion);
        }
        catch
        {
            return false;
        }
    }

    #endregion
}