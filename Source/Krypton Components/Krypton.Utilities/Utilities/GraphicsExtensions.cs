using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ToastImageResources = Krypton.Utilities.Components.KryptonToast.Resources.ToastImageResources;

namespace Krypton.Utilities;

public static class GraphicsExtensions
{
    /// <summary>Gets the type of the toast notification icon.</summary>
    /// <param name="notificationIconType">Type of the notification icon.</param>
    /// <param name="customImage">The custom image.</param>
    /// <param name="customSize">Size of the custom.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException">notificationIconType - null</exception>
    public static Image? GetToastNotificationIconType(KryptonToastIcon notificationIconType,
        Image? customImage = null, Size? customSize = null)
    {
        Size newSize = customSize ?? new Size(128, 128);

        switch (notificationIconType)
        {
            case KryptonToastIcon.None:
                return null;
            case KryptonToastIcon.Hand:
                return ToastImageResources.Toast_Hand_128_x_128;
            case KryptonToastIcon.SystemHand:
                return ScaleImage(SystemIcons.Hand.ToBitmap(), newSize);
            case KryptonToastIcon.Question:
                return ToastImageResources.Toast_Question_128_x_128;
            case KryptonToastIcon.SystemQuestion:
                return ScaleImage(SystemIcons.Question.ToBitmap(), newSize);
            case KryptonToastIcon.Exclamation:
            case KryptonToastIcon.SystemExclamation:
            case KryptonToastIcon.Warning:
                return ToastImageResources.Toast_Warning_128_x_115;
            case KryptonToastIcon.Asterisk:
                return ToastImageResources.Toast_Asterisk_128_x_128;
            case KryptonToastIcon.Error:
                return ToastImageResources.Toast_Critical_128_x_128;
            case KryptonToastIcon.SystemAsterisk:
                return ScaleImage(SystemIcons.Asterisk.ToBitmap(), newSize);
            case KryptonToastIcon.Stop:
                return ToastImageResources.Toast_Stop_128_x_128;
            case KryptonToastIcon.Information:
                return ToastImageResources.Toast_Information_128_x_128;
            case KryptonToastIcon.Shield:
                {
                    var messageBoxShieldIcon = ExtractIconFromImageresInternal(ImageresIconID.Shield, IconSize.Huge);
                    return messageBoxShieldIcon?.ToBitmap();
                }
            case KryptonToastIcon.WindowsLogo:
                if (OSUtilities.IsAtLeastWindowsEleven)
                {
                    return ToastImageResources.Toast_Windows_11_128_x_128;
                }
                else if (OSUtilities.IsWindowsTen || OSUtilities.IsWindowsEightPointOne || OSUtilities.IsWindowsEight)
                {
                    return ToastImageResources.Toast_Windows_10_128_x_121;
                }
                else
                {
                    return ScaleImage(SystemIcons.WinLogo.ToBitmap(), newSize);
                }
            case KryptonToastIcon.Application:
                return customImage != null
                    ? ScaleImage(customImage, newSize)
                    : ScaleImage(SystemIcons.Application.ToBitmap(), newSize);
            case KryptonToastIcon.SystemApplication:
                return ScaleImage(SystemIcons.Application.ToBitmap(), newSize);
            case KryptonToastIcon.Ok:
                return ToastImageResources.Toast_Ok_128_x_128;
            case KryptonToastIcon.Custom:
                return customImage != null ? ScaleImage(customImage, newSize) : null;
            default:
                DebugTools.NotImplemented(notificationIconType.ToString());
                throw new ArgumentOutOfRangeException(nameof(notificationIconType), notificationIconType, null);
        }
    }

    /// <summary>
    /// Returns a Bitmap for a toast notification icon, using existing mapping and optional scaling.
    /// Centralizes conversion to Bitmap to reduce duplication in forms that require Bitmap images.
    /// </summary>
    /// <param name="notificationIconType">Type of icon to resolve. If null, returns null.</param>
    /// <param name="applicationIcon">Optional application Icon used when the icon type is Application.</param>
    /// <param name="customImage">Optional custom image used when the icon type is Custom or Application.</param>
    /// <param name="customSize">Optional target size for system-derived images.</param>
    /// <returns>Bitmap or null.</returns>
    public static Bitmap? GetToastNotificationBitmap(
        KryptonToastIcon? notificationIconType,
        Icon? applicationIcon = null,
        Image? customImage = null,
        Size? customSize = null)
    {
        if (notificationIconType is null)
        {
            return null;
        }

        // If asking for Application, prefer the provided applicationIcon converted to Bitmap.
        Image? customForMapping = notificationIconType == KryptonToastIcon.Application
            ? (applicationIcon?.ToBitmap() ?? customImage)
            : customImage;

        Image? resolved = GetToastNotificationIconType(notificationIconType.Value, customForMapping, customSize);
        if (resolved == null)
        {
            return null;
        }

        return resolved as Bitmap ?? new Bitmap(resolved);
    }

    /// <summary>Resize the image to the specified width and height. Copied from: https://stackoverflow.com/questions/1922040/how-to-resize-an-image-c-sharp</summary>
    /// <param name="sourceImage">The image to resize.</param>
    /// <param name="imageSize">The size that you want to resize the image to.</param>
    /// <returns>The resized image.</returns>
    internal static Bitmap? ScaleImage(Image? sourceImage, Size? imageSize)
    {
        try
        {
            Size tmpSize = imageSize ?? new Size(16, 16);

            var destImage = new Bitmap(tmpSize.Width, tmpSize.Height);

            if (sourceImage != null)
            {
                destImage.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);

                using var graphics = Graphics.FromImage(destImage);
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using var wrapMode = new ImageAttributes();
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);

                var destRect = new Rectangle(0, 0, tmpSize.Width, tmpSize.Height);
                graphics.DrawImage(sourceImage, destRect, 0, 0, sourceImage.Width, sourceImage.Height,
                    GraphicsUnit.Pixel, wrapMode);
            }

            return destImage;
        }
        catch (Exception e)
        {
            KryptonExceptionHandler.CaptureException(e, showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);

            return null;
        }
    }

    /// <summary>Extracts an icon from imageres.dll using the specified icon ID and size.</summary>
    /// <param name="iconId">The icon ID from ImageresIconID enum.</param>
    /// <param name="iconSize">The size of the icon to extract. Defaults to Medium (32x32).</param>
    /// <param name="selectionStrategy">The strategy for selecting fallback icons. Defaults to OS-based selection.</param>
    /// <returns>The extracted icon, or null if extraction fails.</returns>
    public static Icon? ExtractIconFromImageres(int iconId, IconSize iconSize = IconSize.Medium, IconSelectionStrategy selectionStrategy = IconSelectionStrategy.OSBased) => ExtractIconFromImageresInternal((ImageresIconID)iconId, iconSize, selectionStrategy);

    /// <summary>Extracts an icon from imageres.dll using the specified icon ID and size.</summary>
    /// <param name="iconId">The icon ID from ImageresIconID enum.</param>
    /// <param name="iconSize">The size of the icon to extract. Defaults to Medium (32x32).</param>
    /// <param name="selectionStrategy">The strategy for selecting fallback icons. Defaults to OS-based selection.</param>
    /// <returns>The extracted icon, or null if extraction fails.</returns>
    internal static Icon? ExtractIconFromImageresInternal(ImageresIconID iconId, IconSize iconSize = IconSize.Medium, IconSelectionStrategy selectionStrategy = IconSelectionStrategy.OSBased)
    {
        var size = GetSizeFromIconSize(iconSize);
        var isLargeIcon = size.Width > 32; // Use large icon extraction for sizes larger than 32x32

        // Try to extract from imageres.dll first
        var icon = Krypton.Toolkit.GraphicsExtensions.ExtractIcon(Libraries.Imageres, (int)iconId, isLargeIcon);
        if (icon != null)
        {
            return icon;
        }

        // Fallback to embedded resources for specific icons
        return GetFallbackIconFromResources(iconId, size, selectionStrategy);
    }

    /// <summary>Gets the pixel size corresponding to an IconSize enum value.</summary>
    /// <param name="iconSize">The IconSize enum value.</param>
    /// <returns>The corresponding pixel size.</returns>
    private static Size GetSizeFromIconSize(IconSize iconSize) => new((int)iconSize, (int)iconSize);

    /// <summary>Gets a fallback icon from embedded resources when imageres.dll is not available.</summary>
    /// <param name="iconId">The icon ID that was requested.</param>
    /// <param name="targetSize">The target size for the icon.</param>
    /// <param name="selectionStrategy">The strategy for selecting fallback icons.</param>
    /// <returns>The fallback icon, or null if no suitable fallback is available.</returns>
    private static Icon? GetFallbackIconFromResources(ImageresIconID iconId, Size targetSize, IconSelectionStrategy selectionStrategy)
    {
        try
        {
            // Only provide fallbacks for specific icons that we have embedded resources for
            switch (iconId)
            {
                case ImageresIconID.Shield:
                case ImageresIconID.ShieldAlt:
                    return GetUACShieldFallbackIcon(targetSize, selectionStrategy);
                default:
                    // For other icons, we don't have embedded fallbacks
                    return null;
            }
        }
        catch (Exception)
        {
            // If fallback fails, return null
            return null;
        }
    }

    /// <summary>Gets a UAC shield icon from embedded resources based on the current OS or theme.</summary>
    /// <param name="targetSize">The target size for the icon.</param>
    /// <param name="selectionStrategy">The strategy for selecting the icon.</param>
    /// <returns>The UAC shield icon, or null if extraction fails.</returns>
    private static Icon? GetUACShieldFallbackIcon(Size targetSize, IconSelectionStrategy selectionStrategy)
    {
        try
        {
            Image? shieldImage;

            if (selectionStrategy == IconSelectionStrategy.ThemeBased)
            {
                // Use theme-based selection
                shieldImage = Krypton.Toolkit.GraphicsExtensions.GetThemeBasedShieldImage(targetSize);
            }
            else
            {
                // Use OS-based selection (default behavior)
                shieldImage = Krypton.Toolkit.GraphicsExtensions.GetOSBasedShieldImage(targetSize);
            }

            if (shieldImage != null)
            {
                // Convert to icon
                using var bitmap = new Bitmap(shieldImage);
                var iconHandle = bitmap.GetHicon();
                return Icon.FromHandle(iconHandle);
            }

            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>Determines if a theme is compatible with Windows Vista icon style.</summary>
    /// <param name="theme">The theme to check.</param>
    /// <returns>True if the theme should use Windows Vista icons.</returns>
    private static bool IsVistaCompatibleTheme(PaletteMode theme)
    {
        return theme switch
        {
            PaletteMode.ProfessionalSystem => true,
            PaletteMode.ProfessionalOffice2003 => true,
            PaletteMode.Office2007Blue => true,
            PaletteMode.Office2007BlueDarkMode => true,
            PaletteMode.Office2007BlueLightMode => true,
            PaletteMode.Office2007Silver => true,
            PaletteMode.Office2007SilverDarkMode => true,
            PaletteMode.Office2007SilverLightMode => true,
            PaletteMode.Office2007White => true,
            PaletteMode.Office2007Black => true,
            PaletteMode.Office2007BlackDarkMode => true,
            PaletteMode.SparkleBlue => true,
            PaletteMode.SparkleBlueDarkMode => true,
            PaletteMode.SparkleBlueLightMode => true,
            PaletteMode.SparkleOrange => true,
            PaletteMode.SparkleOrangeDarkMode => true,
            PaletteMode.SparkleOrangeLightMode => true,
            PaletteMode.SparklePurple => true,
            PaletteMode.SparklePurpleDarkMode => true,
            PaletteMode.SparklePurpleLightMode => true,
            _ => false
        };
    }

    /// <summary>Determines if a theme is compatible with Windows 7/8.x icon style.</summary>
    /// <param name="theme">The theme to check.</param>
    /// <returns>True if the theme should use Windows 7/8.x icons.</returns>
    private static bool IsWindows7CompatibleTheme(PaletteMode theme)
    {
        return theme switch
        {
            PaletteMode.Office2010Blue => true,
            PaletteMode.Office2010BlueDarkMode => true,
            PaletteMode.Office2010BlueLightMode => true,
            PaletteMode.Office2010Silver => true,
            PaletteMode.Office2010SilverDarkMode => true,
            PaletteMode.Office2010SilverLightMode => true,
            PaletteMode.Office2010White => true,
            PaletteMode.Office2010Black => true,
            PaletteMode.Office2010BlackDarkMode => true,
            PaletteMode.Office2013White => true,
            PaletteMode.VisualStudio2010Render2007 => true,
            PaletteMode.VisualStudio2010Render2010 => true,
            PaletteMode.VisualStudio2010Render2013 => true,
            PaletteMode.VisualStudio2010Render365 => true,
            _ => false
        };
    }

    /// <summary>Determines if a theme is compatible with Windows 10/11 icon style.</summary>
    /// <param name="theme">The theme to check.</param>
    /// <returns>True if the theme should use Windows 10/11 icons.</returns>
    private static bool IsWindows10CompatibleTheme(PaletteMode theme)
    {
        return theme switch
        {
            PaletteMode.Microsoft365Blue => true,
            PaletteMode.Microsoft365BlueDarkMode => true,
            PaletteMode.Microsoft365BlueLightMode => true,
            PaletteMode.Microsoft365Silver => true,
            PaletteMode.Microsoft365SilverDarkMode => true,
            PaletteMode.Microsoft365SilverLightMode => true,
            PaletteMode.Microsoft365White => true,
            PaletteMode.Microsoft365Black => true,
            PaletteMode.Microsoft365BlackDarkMode => true,
            PaletteMode.Microsoft365BlackDarkModeAlternate => true,
            PaletteMode.MaterialLight => true,
            PaletteMode.MaterialDark => true,
            PaletteMode.MaterialLightRipple => true,
            PaletteMode.MaterialDarkRipple => true,
            _ => false
        };
    }
}