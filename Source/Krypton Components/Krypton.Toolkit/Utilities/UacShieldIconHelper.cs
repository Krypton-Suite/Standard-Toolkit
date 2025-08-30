#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege & Ahmed Abdelhameed et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Centralized helper for retrieving and managing system icons from imageres.dll through the theme system.
/// 
/// This class provides access to high-quality, native Windows system icons that automatically
/// adapt to the current theme and DPI settings. It's the recommended way to get system icons
/// instead of using hardcoded resources.
/// 
/// System icon constants are defined in <see cref="PI.SIID"/> enum (PlatformInvoke.cs).
/// 
/// Example usage:
/// <code>
/// // Get a shield icon for UAC elevation
/// var shieldIcon = UacShieldIconHelper.GetSystemIcon(PI.SIID.SHIELD, 32);
/// 
/// // Get a theme-aware info icon
/// var infoIcon = UacShieldIconHelper.GetThemeAwareSystemIcon(
///     PI.SIID.INFO, 
///     KryptonManager.CurrentGlobalPaletteMode, 
///     UACShieldIconSize.Medium);
/// 
/// // Get any system icon at a specific size
/// var warningIcon = UacShieldIconHelper.GetSystemIconAtSize(PI.SIID.WARNING, 64);
/// 
/// // Get icons at custom pixel sizes
/// var customSizeIcon = UacShieldIconHelper.GetThemeAwareSystemIconAtSize(
///     PI.SIID.INFO,
///     KryptonManager.CurrentGlobalPaletteMode,
///     48); // 48x48 pixels
/// 
/// // Get multiple sizes at once
/// var multipleSizes = UacShieldIconHelper.GetSystemIconMultipleSizes(
///     PI.SIID.SHIELD,
///     KryptonManager.CurrentGlobalPaletteMode);
/// 
/// // Get specific custom pixel sizes
/// var customPixelSizes = UacShieldIconHelper.GetSystemIconMultiplePixelSizes(
///     PI.SIID.INFO,
///     KryptonManager.CurrentGlobalPaletteMode,
///     24, 48, 96, 192); // Multiple custom sizes
/// </code>
/// </summary>
public static class UacShieldIconHelper
{
    #region Public Methods

    /// <summary>
    /// Gets a theme-aware shield icon as a Bitmap, with fallback to Windows 10 shield resource.
    /// This method centralizes the common pattern used throughout the codebase.
    /// </summary>
    /// <param name="size">Optional size for the shield icon. If null, uses default size.</param>
    /// <returns>A Bitmap containing the theme-aware shield icon or fallback resource.</returns>
    public static Bitmap GetThemeAwareShieldIconBitmap(UACShieldIconSize? size = null)
    {
        var currentPaletteMode = KryptonManager.CurrentGlobalPaletteMode;
        var shieldIcon = GetThemeAwareShieldIcon(currentPaletteMode, size ?? UACShieldIconSize.Small);
        return shieldIcon?.ToBitmap() ?? Windows11UACShieldIconResources.Windows_11_UAC_Shield_16_x_16;
    }


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

            int result = PI.SHGetStockIconInfo((int)PI.SIID.SHIELD, 
                (int)(PI.SHGSI.ICON | PI.SHGSI.LARGEICON), ref shinfo);
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
            result = PI.SHGetStockIconInfo((int)PI.SIID.SHIELD, 
                (int)(PI.SHGSI.ICON | PI.SHGSI.SMALLICON), ref shinfo);
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

    /// <summary>
    /// Gets a system icon from imageres.dll using the specified stock icon ID.
    /// </summary>
    /// <param name="stockIconId">The stock icon ID to retrieve.</param>
    /// <param name="largeIcon">The desired icon size (large or small).</param>
    /// <returns>The system icon or null if not available.</returns>
    internal static Icon? GetSystemIcon(PI.SIID stockIconId, bool largeIcon = true)
    {
        try
        {
            // Check if we're on Windows Vista or later
            if (Environment.OSVersion.Version.Major < 6)
            {
                return null;
            }

            var shinfo = new PI.SHSTOCKICONINFO();
            shinfo.cbSize = (uint)Marshal.SizeOf(typeof(PI.SHSTOCKICONINFO));

            var flags = (int)(PI.SHGSI.ICON | (largeIcon ? PI.SHGSI.LARGEICON : PI.SHGSI.SMALLICON));
            int result = PI.SHGetStockIconInfo((int)stockIconId, flags, ref shinfo);
            
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
    /// Gets a system icon from imageres.dll at the specified size.
    /// </summary>
    /// <param name="stockIconId">The stock icon ID to retrieve.</param>
    /// <param name="targetSize">The target size in pixels.</param>
    /// <returns>The system icon at the specified size or null if not available.</returns>
    internal static Icon? GetSystemIconAtSize(PI.SIID stockIconId, int targetSize)
    {
        try
        {
            var baseIcon = GetSystemIcon(stockIconId, targetSize >= 32);
            if (baseIcon == null)
            {
                return null;
            }

            // If the icon is already the right size, return it
            if (baseIcon.Size.Width == targetSize && baseIcon.Size.Height == targetSize)
            {
                return baseIcon;
            }

            // Scale the icon to the requested size with DPI awareness
            using (var bitmap = new Bitmap(targetSize, targetSize))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawIcon(baseIcon, new Rectangle(0, 0, targetSize, targetSize));
                return CreateIconFromBitmap(bitmap);
            }
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Gets a theme-aware system icon that automatically adapts to the current theme.
    /// </summary>
    /// <param name="stockIconId">The stock icon ID to retrieve.</param>
    /// <param name="paletteMode">The current palette mode for theme awareness.</param>
    /// <param name="size">The desired icon size.</param>
    /// <returns>A theme-aware system icon or null if not available.</returns>
    internal static Icon? GetThemeAwareSystemIcon(PI.SIID stockIconId, PaletteMode paletteMode, UACShieldIconSize size = UACShieldIconSize.Small)
    {
        // First try to get the system icon from imageres.dll
        var systemIcon = GetSystemIcon(stockIconId, size >= UACShieldIconSize.Medium);
        if (systemIcon != null)
        {
            return ScaleIconToSize(systemIcon, size);
        }

        // Fallback to theme-specific icons based on palette mode
        return GetThemeSpecificIcon(stockIconId, paletteMode, size);
    }

    /// <summary>
    /// Gets a theme-aware system icon at a specific pixel size.
    /// </summary>
    /// <param name="stockIconId">The stock icon ID to retrieve.</param>
    /// <param name="paletteMode">The current palette mode for theme awareness.</param>
    /// <param name="pixelSize">The desired icon size in pixels.</param>
    /// <returns>A theme-aware system icon at the specified size or null if not available.</returns>
    internal static Icon? GetThemeAwareSystemIconAtSize(PI.SIID stockIconId, PaletteMode paletteMode, int pixelSize)
    {
        // First try to get the system icon from imageres.dll at the exact size
        var systemIcon = GetSystemIconAtSize(stockIconId, pixelSize);
        if (systemIcon != null)
        {
            return systemIcon;
        }

        // Fallback to theme-specific icons based on palette mode
        var fallbackIcon = GetThemeSpecificIcon(stockIconId, paletteMode, GetUACShieldIconSizeFromPixels(pixelSize));
        if (fallbackIcon != null)
        {
            // Scale the fallback icon to the exact pixel size
            return ScaleIconToExactSize(fallbackIcon, pixelSize);
        }

        return null;
    }

    /// <summary>
    /// Gets multiple sizes of the same system icon for different UI contexts.
    /// </summary>
    /// <param name="stockIconId">The stock icon ID to retrieve.</param>
    /// <param name="paletteMode">The current palette mode for theme awareness.</param>
    /// <returns>A dictionary of icons at different sizes.</returns>
    internal static Dictionary<UACShieldIconSize, Icon?> GetSystemIconMultipleSizes(PI.SIID stockIconId, PaletteMode paletteMode)
    {
        var result = new Dictionary<UACShieldIconSize, Icon?>();
        
        foreach (UACShieldIconSize size in Enum.GetValues(typeof(UACShieldIconSize)))
        {
            result[size] = GetThemeAwareSystemIcon(stockIconId, paletteMode, size);
        }
        
        return result;
    }

    /// <summary>
    /// Gets multiple pixel sizes of the same system icon for different UI contexts.
    /// </summary>
    /// <param name="stockIconId">The stock icon ID to retrieve.</param>
    /// <param name="paletteMode">The current palette mode for theme awareness.</param>
    /// <param name="pixelSizes">Array of desired pixel sizes.</param>
    /// <returns>A dictionary of icons at the specified pixel sizes.</returns>
    internal static Dictionary<int, Icon?> GetSystemIconMultiplePixelSizes(PI.SIID stockIconId, PaletteMode paletteMode, params int[] pixelSizes)
    {
        var result = new Dictionary<int, Icon?>();
        
        foreach (int pixelSize in pixelSizes)
        {
            result[pixelSize] = GetThemeAwareSystemIconAtSize(stockIconId, paletteMode, pixelSize);
        }
        
        return result;
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
        return GetShieldIcon();
    }

    /// <summary>
    /// Gets a theme-specific icon based on the palette mode.
    /// </summary>
    /// <param name="stockIconId">The stock icon ID.</param>
    /// <param name="paletteMode">The current palette mode.</param>
    /// <param name="size">The desired icon size.</param>
    /// <returns>A theme-specific icon.</returns>
    private static Icon? GetThemeSpecificIcon(PI.SIID stockIconId, PaletteMode paletteMode, UACShieldIconSize size)
    {
        // For now, fall back to the system icon
        // In the future, this could be enhanced to provide theme-specific versions
        return GetSystemIcon(stockIconId, size >= UACShieldIconSize.Medium);
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
                Windows11UACShieldIconResources.Windows_11_UAC_Shield_16_x_16,

            // Office 2010-2016 themes use Windows 10 style
            PaletteMode.Office2010Blue or PaletteMode.Office2010BlueDarkMode or
            PaletteMode.Office2010BlueLightMode or PaletteMode.Office2010Silver or
            PaletteMode.Office2010SilverDarkMode or PaletteMode.Office2010SilverLightMode or
            PaletteMode.Office2010White or PaletteMode.Office2010Black or
            PaletteMode.Office2010BlackDarkMode or PaletteMode.Office2013White  =>
                Windows10UACShieldIconResources.Windows_10_UAC_Shield_16_x_16,

            // Legacy themes (Office 2007, Professional) use Windows 7/8 style
            PaletteMode.Office2007Blue or PaletteMode.Office2007BlueDarkMode or
            PaletteMode.Office2007BlueLightMode or PaletteMode.Office2007Silver or
            PaletteMode.Office2007SilverDarkMode or PaletteMode.Office2007SilverLightMode or
            PaletteMode.Office2007White or PaletteMode.Office2007Black or
            PaletteMode.Office2007BlackDarkMode or PaletteMode.ProfessionalOffice2003 or
            PaletteMode.ProfessionalSystem =>
                UACShieldIconResources.UACShieldWindows7881,

            // Default to Windows 11 style for unknown themes
            _ => Windows11UACShieldIconResources.Windows_11_UAC_Shield_16_x_16
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
    /// Scales an icon to an exact pixel size.
    /// </summary>
    /// <param name="icon">The icon to scale.</param>
    /// <param name="pixelSize">The target size in pixels.</param>
    /// <returns>A scaled icon.</returns>
    private static Icon? ScaleIconToExactSize(Icon icon, int pixelSize)
    {
        try
        {
            // If the icon is already the right size, return it
            if (icon.Size.Width == pixelSize && icon.Size.Height == pixelSize)
            {
                return icon;
            }

            // Scale the icon to the requested size with DPI awareness
            using (var bitmap = new Bitmap(pixelSize, pixelSize))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawIcon(icon, new Rectangle(0, 0, pixelSize, pixelSize));
                return CreateIconFromBitmap(bitmap);
            }
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Converts a pixel size to the closest UACShieldIconSize enum value.
    /// </summary>
    /// <param name="pixelSize">The pixel size to convert.</param>
    /// <returns>The closest UACShieldIconSize enum value.</returns>
    private static UACShieldIconSize GetUACShieldIconSizeFromPixels(int pixelSize)
    {
        return pixelSize switch
        {
            <= 16 => UACShieldIconSize.ExtraSmall,
            <= 32 => UACShieldIconSize.Small,
            <= 64 => UACShieldIconSize.Medium,
            <= 128 => UACShieldIconSize.Large,
            _ => UACShieldIconSize.ExtraLarge
        };
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
