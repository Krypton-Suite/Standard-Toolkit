#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Centralized helper for retrieving and managing UAC shield icons through the theme system.
/// </summary>
public static class UacShieldIconHelper
{
    #region Public Methods

    /// <summary>
    /// Gets the system UAC shield icon using Windows API calls.
    /// </summary>
    /// <returns>The system shield icon or null if not available.</returns>
    public static Icon? GetShieldIcon()
    {
        try
        {
            // Check if we're on Windows Vista or later (UAC was introduced in Vista)
            if (Environment.OSVersion.Version.Major < 6)
            {
                return null;
            }

            // Use SHGetStockIconInfo to get the system shield icon
            var shinfo = new PI.SHSTOCKICONINFO();
            shinfo.cbSize = (uint)Marshal.SizeOf(typeof(PI.SHSTOCKICONINFO));

            int result = PI.SHGetStockIconInfo(PI.SIID_SHIELD, 
                PI.SHGSI_ICON | PI.SHGSI_LARGEICON, ref shinfo);
            if (result == 0 && shinfo.hIcon != IntPtr.Zero)
            {
                try 
                { 
                    return (Icon)Icon.FromHandle(shinfo.hIcon).Clone(); 
                }
                finally 
                { 
                    PI.DestroyIcon(shinfo.hIcon); 
                }
            }

            // Try small icon if large failed
            result = PI.SHGetStockIconInfo(PI.SIID_SHIELD, 
                PI.SHGSI_ICON | PI.SHGSI_SMALLICON, ref shinfo);
            if (result == 0 && shinfo.hIcon != IntPtr.Zero)
            {
                try 
                { 
                    return (Icon)Icon.FromHandle(shinfo.hIcon).Clone(); 
                }
                finally 
                { 
                    PI.DestroyIcon(shinfo.hIcon); 
                }
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Gets a theme-appropriate UAC shield icon based on the current palette mode.
    /// </summary>
    /// <param name="paletteMode">The current palette mode.</param>
    /// <param name="size">The desired icon size.</param>
    /// <returns>A theme-appropriate shield icon or null if not available.</returns>
    public static Icon? GetThemeAwareShieldIcon(PaletteMode paletteMode, UACShieldIconSize size = UACShieldIconSize.Small)
    {
        // First try to get the system icon (most appropriate for current OS)
        var systemIcon = GetShieldIcon();
        if (systemIcon != null)
        {
            return ScaleIconToSize(systemIcon, size);
        }

        // Fallback to theme-specific icons based on palette mode
        return GetThemeSpecificShieldIcon(paletteMode, size);
    }

    /// <summary>
    /// Gets a DPI-aware system shield icon at the specified size.
    /// </summary>
    /// <param name="size">The desired icon size.</param>
    /// <returns>A DPI-aware system shield icon or null if not available.</returns>
    public static Icon? GetSystemShieldIconAtSize(int size)
    {
        try
        {
            var baseIcon = GetShieldIcon();
            if (baseIcon == null)
            {
                return null;
            }

            // Scale the icon to the requested size with DPI awareness
            using (var bitmap = new Bitmap(size, size))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawIcon(baseIcon, new Rectangle(0, 0, size, size));
                return CreateIconFromBitmap(bitmap);
            }
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Gets a theme-specific shield icon based on the palette mode.
    /// </summary>
    /// <param name="paletteMode">The current palette mode.</param>
    /// <param name="size">The desired icon size.</param>
    /// <returns>A theme-specific shield icon.</returns>
    private static Icon? GetThemeSpecificShieldIcon(PaletteMode paletteMode, UACShieldIconSize size)
    {
        // Determine the appropriate icon based on theme characteristics
        // rather than hardcoded OS detection
        var iconResource = GetIconResourceForTheme(paletteMode);
        if (iconResource != null)
        {
            var icon = CreateIconFromBitmap(iconResource);
            return icon != null ? ScaleIconToSize(icon, size) : null;
        }

        // Ultimate fallback to system icon
        return SystemIcons.Shield;
    }

    /// <summary>
    /// Gets the appropriate icon resource based on theme characteristics.
    /// </summary>
    /// <param name="paletteMode">The current palette mode.</param>
    /// <returns>The appropriate icon resource or null.</returns>
    private static Bitmap? GetIconResourceForTheme(PaletteMode paletteMode)
    {
        // Use theme characteristics instead of OS detection
        return paletteMode switch
        {
            // Modern themes (Microsoft 365, etc.) use Windows 11 style
            PaletteMode.Microsoft365White or PaletteMode.Microsoft365Blue or 
                PaletteMode.Microsoft365Black or PaletteMode.Microsoft365BlackDarkMode or
                PaletteMode.Microsoft365BlackDarkModeAlternate or
                PaletteMode.Microsoft365BlueDarkMode or PaletteMode.Microsoft365BlueLightMode or 
                PaletteMode.Microsoft365Silver or PaletteMode.Microsoft365SilverDarkMode or PaletteMode.Microsoft365SilverLightMode or
                PaletteMode.MaterialDark or PaletteMode.MaterialDarkRipple or PaletteMode.MaterialLight or PaletteMode.MaterialLightRipple or
            PaletteMode.SparkleBlue or PaletteMode.SparkleOrange or
            PaletteMode.SparklePurple  =>
                UACShieldIconResources.UACShieldWindows11,

            // Office 2010-2016 themes use Windows 10 style
            PaletteMode.Office2010Blue or PaletteMode.Office2010BlueDarkMode or
            PaletteMode.Office2010BlueLightMode or PaletteMode.Office2010Silver or
            PaletteMode.Office2010SilverDarkMode or PaletteMode.Office2010SilverLightMode or
            PaletteMode.Office2010White or PaletteMode.Office2010Black or
            PaletteMode.Office2010BlackDarkMode or PaletteMode.Office2013White  =>
                UACShieldIconResources.UACShieldWindows10,

            // Legacy themes (Office 2007, Professional) use Windows 7/8 style
            PaletteMode.Office2007Blue or PaletteMode.Office2007BlueDarkMode or
            PaletteMode.Office2007BlueLightMode or PaletteMode.Office2007Silver or
            PaletteMode.Office2007SilverDarkMode or PaletteMode.Office2007SilverLightMode or
            PaletteMode.Office2007White or PaletteMode.Office2007Black or
            PaletteMode.Office2007BlackDarkMode or PaletteMode.ProfessionalOffice2003 or
            PaletteMode.ProfessionalSystem =>
                UACShieldIconResources.UACShieldWindows7881,

            // Default to Windows 11 style for unknown themes
            _ => UACShieldIconResources.UACShieldWindows11
        };
    }

    /// <summary>
    /// Scales an icon to the specified size.
    /// </summary>
    /// <param name="icon">The icon to scale.</param>
    /// <param name="size">The target size.</param>
    /// <returns>A scaled icon.</returns>
    private static Icon? ScaleIconToSize(Icon icon, UACShieldIconSize size)
    {
        var targetSize = size switch
        {
            UACShieldIconSize.ExtraSmall => 16,
            UACShieldIconSize.Small => 32,
            UACShieldIconSize.Medium => 64,
            UACShieldIconSize.Large => 128,
            UACShieldIconSize.ExtraLarge => 256,
            _ => 32
        };

        return GetSystemShieldIconAtSize(targetSize);
    }

    /// <summary>
    /// Creates an icon from a bitmap without leaking GDI handles.
    /// </summary>
    /// <param name="bitmap">The bitmap to convert.</param>
    /// <returns>The created icon or null if failed.</returns>
    private static Icon? CreateIconFromBitmap(Bitmap? bitmap)
    {
        if (bitmap == null) return null;
        
        IntPtr hIcon = IntPtr.Zero;
        try
        {
            hIcon = bitmap.GetHicon();
            using (var icon = Icon.FromHandle(hIcon))
            {
                return (Icon)icon.Clone();
            }
        }
        catch
        {
            return null;
        }
        finally
        {
            if (hIcon != IntPtr.Zero)
            {
                PI.DestroyIcon(hIcon);
            }
        }
    }

    #endregion
}
