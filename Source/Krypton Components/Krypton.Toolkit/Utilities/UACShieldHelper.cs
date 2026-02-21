#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Helper class for extracting UAC shield icons from imageres.dll with fallback to local resources.
/// </summary>
public static class UACShieldHelper
{
    #region Public Methods

    /// <summary>
    /// Gets the OS-specific UAC shield icon, trying imageres.dll first, then falling back to local resources.
    /// </summary>
    /// <param name="size">The desired size of the shield icon.</param>
    /// <returns>The UAC shield icon as a Bitmap, or null if extraction fails.</returns>
    public static Bitmap? GetUACShieldIcon(IconSize size)
    {
        // Try to extract from imageres.dll first
        var icon = ExtractUACShieldFromImageres(size);
        if (icon != null)
        {
            return icon;
        }

        // Fallback to local resources
        return GetFallbackUACShieldIcon(size);
    }

    /// <summary>
    /// Gets the OS-specific UAC shield icon for the current OS.
    /// </summary>
    /// <param name="size">The desired size of the shield icon.</param>
    /// <returns>The UAC shield icon as a Bitmap.</returns>
    public static Bitmap GetOSSpecificUACShieldIcon(IconSize size)
    {
        // Try to extract from imageres.dll first
        var icon = ExtractUACShieldFromImageres(size);
        if (icon != null)
        {
            return icon;
        }

        // Fallback to OS-specific local resources
        if (OSUtilities.IsAtLeastWindowsEleven)
        {
            return ScaleImage(Windows11UACShieldImageResources.Windows_11_UAC_Shield_256_x_256, GetIconSize(size));
        }
        else if (OSUtilities.IsWindowsTen)
        {
            return ScaleImage(Windows10UACShieldImageResources.Windows_10_UAC_Shield_256_x_256, GetIconSize(size));
        }
        else if (OSUtilities.IsWindowsEightPointOne || OSUtilities.IsWindowsEight || OSUtilities.IsWindowsSeven)
        {
            return ScaleImage(Windows7And8xUACShieldImageResources.Windows_7_and_8x_UAC_Shield_256_x_256, GetIconSize(size));
        }
        else
        {
            // Final fallback to system icon
            return ScaleImage(SystemIcons.Shield.ToBitmap(), GetIconSize(size));
        }
    }

    /// <summary>
    /// Gets the UAC shield icon directly from imageres.dll at the specified size.
    /// </summary>
    /// <param name="size">The exact size to extract from imageres.dll.</param>
    /// <returns>The UAC shield icon as a Bitmap, or null if extraction fails.</returns>
    public static Bitmap? GetUACShieldFromImageres(Size size)
    {
        try
        {
            // Determine if we should extract as large icon based on size
            bool isLargeIcon = size.Width >= 32 || size.Height >= 32;

            // Try the primary UAC shield icon ID
            var icon = GraphicsExtensions.ExtractIcon(Libraries.Imageres, GlobalStaticValues.UAC_SHIELD_ICON_ID, isLargeIcon);

            if (icon != null)
            {
                var bitmap = new Bitmap(icon.ToBitmap(), size);

                icon.Dispose();
                
                return bitmap;
            }

            // Try the alternative UAC shield icon ID
            icon = GraphicsExtensions.ExtractIcon(Libraries.Imageres, GlobalStaticValues.UAC_SHIELD_ICON_ID_ALT, isLargeIcon);

            if (icon != null)
            {
                var bitmap = new Bitmap(icon.ToBitmap(), size);
                icon.Dispose();
                return bitmap;
            }

            return null;
        }
        catch (Exception)
        {
            // If extraction fails, return null to trigger fallback
            return null;
        }
    }

    /// <summary>
    /// Gets all available UAC shield icon sizes from imageres.dll.
    /// </summary>
    /// <returns>Array of available sizes, or empty array if extraction fails.</returns>
    public static Size[] GetAvailableImageresSizes()
    {
        var sizes = new List<Size>();

        // Common icon sizes in imageres.dll
        var commonSizes = new[] { 256, 192, 128, 96, 64, 48, 32, 24, 16, 8 };

        foreach (var dimension in commonSizes)
        {
            var size = new Size(dimension, dimension);

            var icon = GetUACShieldFromImageres(size);
            
            if (icon != null)
            {
                sizes.Add(size);
            
                icon.Dispose();
            }
        }

        return sizes.ToArray();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Extracts the UAC shield icon from imageres.dll at the exact size.
    /// </summary>
    /// <param name="size">The desired size of the shield icon.</param>
    /// <returns>The UAC shield icon as a Bitmap, or null if extraction fails.</returns>
    private static Bitmap? ExtractUACShieldFromImageres(IconSize size)
    {
        var targetSize = GetIconSize(size);
        
        return GetUACShieldFromImageres(targetSize);
    }

    /// <summary>
    /// Gets the fallback UAC shield icon from local resources.
    /// </summary>
    /// <param name="size">The desired size of the shield icon.</param>
    /// <returns>The UAC shield icon as a Bitmap.</returns>
    private static Bitmap GetFallbackUACShieldIcon(IconSize size) =>
        // Use OS-specific local resources
        GetOSSpecificUACShieldIcon(size);

    /// <summary>
    /// Gets the size for the specified UAC shield icon size.
    /// </summary>
    /// <param name="size">The UAC shield icon size.</param>
    /// <returns>The Size for the specified UAC shield icon size.</returns>
    private static Size GetIconSize(IconSize size)
    {
        return size switch
        {
            IconSize.Tiny => new Size(8, 8),
            IconSize.ExtraSmall => new Size(16, 16),
            IconSize.Small => new Size(24, 24),
            IconSize.MediumSmall => new Size(32, 32),
            IconSize.Medium => new Size(48, 48),
            IconSize.MediumLarge => new Size(64, 64),
            IconSize.Large => new Size(96, 96),
            IconSize.ExtraLarge => new Size(128, 128),
            IconSize.Huge => new Size(192, 192),
            IconSize.Maximum => new Size(256, 256),
            _ => new Size(32, 32)
        };
    }

    /// <summary>
    /// Scales an image to the specified size.
    /// </summary>
    /// <param name="image">The image to scale.</param>
    /// <param name="size">The target size.</param>
    /// <returns>The scaled image as a Bitmap.</returns>
    private static Bitmap ScaleImage(Image image, Size size)
    {
        var scaledImage = GraphicsExtensions.ScaleImage(image, size);
        
        return scaledImage ?? new Bitmap(image, size);
    }

    #endregion
}