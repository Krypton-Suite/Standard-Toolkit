#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Allows the manipulation of graphics.</summary>
public static class GraphicsExtensions
{
    #region Implementation

    /// <summary>Loads the icon.</summary>
    /// <param name="type">The type of icon.</param>
    /// <param name="size">The size.</param>
    /// <returns>The icon.</returns>
    /// <exception cref="System.PlatformNotSupportedException"></exception>
    public static Icon? LoadIcon(IconType type, Size size)
    {
        var hIcon = ImageNativeMethods.LoadImage(IntPtr.Zero, $"#{(int)type}", 1, size.Width, size.Height, 0);

        return hIcon == IntPtr.Zero ? null : Icon.FromHandle(hIcon);
    }

    /// <summary>Returns an icon representation of an image that is contained in the specified file.</summary>
    /// <param name="executablePath"></param>
    /// <returns></returns>
    public static Icon? ExtractIconFromFilePath(string? executablePath)
    {
        Icon? result = null;

        try
        {
            if (executablePath != null)
            {
                result = Icon.ExtractAssociatedIcon(executablePath);
            }
        }
        catch (Exception e)
        {
            //Console.WriteLine("Unable to extract the icon from the binary");

            KryptonExceptionHandler.CaptureException(e, showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);
        }

        return result;
    }

    /// <summary>Icon sizes.</summary>
    public enum SystemIconSize
    {
        Small = 0,
        Medium = 1,
        Large = 2,
        Custom = 3
    }

    /*
    /// <summary>
    /// Loads the icon.
    /// </summary>
    /// <param name="type">The type of icon.</param>
    /// <param name="size">The size.</param>
    /// <returns>The icon.</returns>
    /// <exception cref="PlatformNotSupportedException"></exception>
    public static Icon LoadIcon(IconType type, Size size)
    {
        IntPtr hIcon = PI.LoadImage(IntPtr.Zero, "#" + (int)type, 1, size.Width, size.Height, 0);
        return hIcon == IntPtr.Zero ? null : Icon.FromHandle(hIcon);
    }
    */

    /// <summary>Resize the image to the specified width and height. Copied from: https://stackoverflow.com/questions/1922040/how-to-resize-an-image-c-sharp</summary>
    /// <param name="sourceImage">The image to resize.</param>
    /// <param name="imageSize">The size that you want to resize the image to.</param>
    /// <returns>The resized image.</returns>
    public static Bitmap? ScaleImage(Image? sourceImage, Size? imageSize)
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

    /// <summary>Scales the image.</summary>
    /// <param name="image">The image.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public static Bitmap? ScaleImage(Image? image, int width, int height) => ScaleImage(image, new Size(width, height));

    /// <summary>Sets the icon.</summary>
    /// <param name="image">The image.</param>
    /// <param name="size">The size.</param>
    public static Image SetIcon(Image image, Size size) => new Bitmap(image, size);

    /// <summary>Extracts an icon from a DLL.
    /// Code from https://www.pinvoke.net/default.aspx/shell32.extracticonex
    /// </summary>
    /// <param name="filePath">The file path to ingest.</param>
    /// <param name="imageIndex">Index of the image.</param>
    /// <param name="largeIcon">if set to <c>true</c> [large icon].</param>
    /// <returns>A specified icon from a chosen DLL file.</returns>
    public static Icon? ExtractIcon(string filePath, int imageIndex, bool largeIcon = true)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            throw new ArgumentNullException(nameof(filePath));
        }

        var hIconEx = new IntPtr[] { IntPtr.Zero };
        try
        {
            int readIconCount = largeIcon
                ? ImageNativeMethods.ExtractIconEx(filePath, -imageIndex, hIconEx, null, 1)
                : ImageNativeMethods.ExtractIconEx(filePath, -imageIndex, null, hIconEx, 1);
            if (readIconCount > 0 && hIconEx[0] != IntPtr.Zero)
            {
                // GET FIRST EXTRACTED ICON
                Icon? extractedIcon = Icon.FromHandle(hIconEx[0]).Clone() as Icon;

                return extractedIcon;
            }
            else
            {
                // NO ICONS READ
                return null;
            }
        }
        catch (Exception ex)
        {
            KryptonExceptionHandler.CaptureException(ex, showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);

            // /* EXTRACT ICON ERROR */
            //// BUBBLE UP
            //throw new ApplicationException("Could not extract icon", ex);
            return null;
        }
        finally
        {
            // RELEASE RESOURCES
            foreach (IntPtr ptr in hIconEx)
            {
                if (ptr != IntPtr.Zero)
                {
                    ImageNativeMethods.DestroyIcon(ptr);
                }
            }
        }
    }

    /// <summary>Gets the size of the screen.</summary>
    /// <returns></returns>
    public static Size GetScreenSize() =>
        new Size(Screen.PrimaryScreen!.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

    /// <summary>Gets the working area.</summary>
    /// <returns></returns>
    public static Rectangle GetWorkingArea() => Screen.PrimaryScreen!.WorkingArea;

    /// <summary>Gets the type of the krypton message box image.</summary>
    /// <param name="iconType">Type of the icon.</param>
    /// <param name="imageSize">Size of the image.</param>
    /// <param name="customImage">The custom image.</param>
    /// <returns>The image, based on the type chosen.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">iconType - null</exception>
    public static Image? GetKryptonMessageBoxImageType(KryptonMessageBoxIcon iconType, Size? imageSize,
        Image? customImage = null)
    {
        var newSize = imageSize ?? new Size(32, 32);

        switch (iconType)
        {
            case KryptonMessageBoxIcon.None:
                return null;
            case KryptonMessageBoxIcon.Hand:
                return MessageBoxImageResources.GenericHand;
            case KryptonMessageBoxIcon.SystemHand:
                return ScaleImage(SystemIcons.Hand.ToBitmap(), newSize);
            case KryptonMessageBoxIcon.Question:
                return MessageBoxImageResources.GenericQuestion;
            case KryptonMessageBoxIcon.SystemQuestion:
                return ScaleImage(SystemIcons.Question.ToBitmap(), newSize);
            case KryptonMessageBoxIcon.Exclamation:
                return MessageBoxImageResources.GenericWarning;
            case KryptonMessageBoxIcon.Warning:
            case KryptonMessageBoxIcon.SystemExclamation:
                return ScaleImage(SystemIcons.Exclamation.ToBitmap(), newSize);
            case KryptonMessageBoxIcon.Asterisk:
                return MessageBoxImageResources.GenericAsterisk;
            case KryptonMessageBoxIcon.SystemAsterisk:
                return ScaleImage(SystemIcons.Asterisk.ToBitmap(), newSize);
            case KryptonMessageBoxIcon.Stop:
                return MessageBoxImageResources.GenericStop;
            case KryptonMessageBoxIcon.Error:
                return MessageBoxImageResources.GenericCritical;
            case KryptonMessageBoxIcon.Information:
                return MessageBoxImageResources.GenericInformation;
            case KryptonMessageBoxIcon.Shield:
            {
                var messageBoxShieldIcon = ExtractIconFromImageresInternal(ImageresIconID.Shield);
                return messageBoxShieldIcon?.ToBitmap();
            }
            case KryptonMessageBoxIcon.WindowsLogo:
                if (OSUtilities.IsAtLeastWindowsEleven)
                {
                    return MessageBoxImageResources.Windows11;
                }
                else if (OSUtilities.IsWindowsTen || OSUtilities.IsWindowsEightPointOne || OSUtilities.IsWindowsEight)
                {
                    return MessageBoxImageResources.Windows_8_and_10_Logo;
                }
                else
                {
                    return ScaleImage(SystemIcons.WinLogo.ToBitmap(), newSize);
                }
            case KryptonMessageBoxIcon.Application:
                return ScaleImage(customImage, newSize) ?? ScaleImage(SystemIcons.Application.ToBitmap(), newSize);
            case KryptonMessageBoxIcon.SystemApplication:
                return ScaleImage(SystemIcons.Application.ToBitmap(), newSize);
            default:
                DebugTools.NotImplemented(iconType.ToString());
                throw new ArgumentOutOfRangeException(nameof(iconType), iconType, null);
        }
    }

    /// <summary>Gets the type of the toast notification icon.</summary>
    /// <param name="notificationIconType">Type of the notification icon.</param>
    /// <param name="customImage">The custom image.</param>
    /// <param name="customSize">Size of the custom.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException">notificationIconType - null</exception>
    public static Image? GetToastNotificationIconType(KryptonToastNotificationIcon notificationIconType,
        Image? customImage = null, Size? customSize = null)
    {
        Size newSize = customSize ?? new Size(128, 128);

        switch (notificationIconType)
        {
            case KryptonToastNotificationIcon.None:
                return null;
            case KryptonToastNotificationIcon.Hand:
                return ToastNotificationImageResources.Toast_Notification_Hand_128_x_128;
            case KryptonToastNotificationIcon.SystemHand:
                return ScaleImage(SystemIcons.Hand.ToBitmap(), newSize);
            case KryptonToastNotificationIcon.Question:
                return ToastNotificationImageResources.Toast_Notification_Question_128_x_128;
            case KryptonToastNotificationIcon.SystemQuestion:
                return ScaleImage(SystemIcons.Question.ToBitmap(), newSize);
            case KryptonToastNotificationIcon.Exclamation:
            case KryptonToastNotificationIcon.SystemExclamation:
            case KryptonToastNotificationIcon.Warning:
                return ToastNotificationImageResources.Toast_Notification_Warning_128_x_115;
            case KryptonToastNotificationIcon.Asterisk:
                return ToastNotificationImageResources.Toast_Notification_Asterisk_128_x_128;
            case KryptonToastNotificationIcon.Error:
                return ToastNotificationImageResources.Toast_Notification_Critical_128_x_128;
            case KryptonToastNotificationIcon.SystemAsterisk:
                return ScaleImage(SystemIcons.Asterisk.ToBitmap(), newSize);
            case KryptonToastNotificationIcon.Stop:
                return ToastNotificationImageResources.Toast_Notification_Stop_128_x_128;
            case KryptonToastNotificationIcon.Information:
                return ToastNotificationImageResources.Toast_Notification_Information_128_x_128;
            case KryptonToastNotificationIcon.Shield:
            {
                var messageBoxShieldIcon = ExtractIconFromImageresInternal(ImageresIconID.Shield, IconSize.Huge);
                return messageBoxShieldIcon?.ToBitmap();
            }
            case KryptonToastNotificationIcon.WindowsLogo:
                if (OSUtilities.IsAtLeastWindowsEleven)
                {
                    return ToastNotificationImageResources.Toast_Notification_Windows_11_128_x_128;
                }
                else if (OSUtilities.IsWindowsTen || OSUtilities.IsWindowsEightPointOne || OSUtilities.IsWindowsEight)
                {
                    return ToastNotificationImageResources.Toast_Notification_Windows_10_128_x_121;
                }
                else
                {
                    return ScaleImage(SystemIcons.WinLogo.ToBitmap(), newSize);
                }
            case KryptonToastNotificationIcon.Application:
                return customImage != null
                    ? ScaleImage(customImage, newSize)
                    : ScaleImage(SystemIcons.Application.ToBitmap(), newSize);
            case KryptonToastNotificationIcon.SystemApplication:
                return ScaleImage(SystemIcons.Application.ToBitmap(), newSize);
            case KryptonToastNotificationIcon.Ok:
                return ToastNotificationImageResources.Toast_Notification_Ok_128_x_128;
            case KryptonToastNotificationIcon.Custom:
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
        KryptonToastNotificationIcon? notificationIconType,
        Icon? applicationIcon = null,
        Image? customImage = null,
        Size? customSize = null)
    {
        if (notificationIconType is null)
        {
            return null;
        }

        // If asking for Application, prefer the provided applicationIcon converted to Bitmap.
        Image? customForMapping = notificationIconType == KryptonToastNotificationIcon.Application
            ? (applicationIcon?.ToBitmap() ?? customImage)
            : customImage;

        Image? resolved = GetToastNotificationIconType(notificationIconType.Value, customForMapping, customSize);
        if (resolved == null)
        {
            return null;
        }

        return resolved as Bitmap ?? new Bitmap(resolved);
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
        var icon = ExtractIcon(Libraries.Imageres, (int)iconId, isLargeIcon);
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
                shieldImage = GetThemeBasedShieldImage(targetSize);
            }
            else
            {
                // Use OS-based selection (default behavior)
                shieldImage = GetOSBasedShieldImage(targetSize);
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

    /// <summary>Gets a Windows 11 UAC shield image at the specified size.</summary>
    /// <param name="targetSize">The target size.</param>
    /// <returns>The shield image, or null if not available.</returns>
    private static Image? GetWindows11ShieldImage(Size targetSize)
    {
        return targetSize.Width switch
        {
            8 => Windows11UACShieldImageResources.Windows_11_UAC_Shield_16_x_16, // Use 16x16 for 8x8
            16 => Windows11UACShieldImageResources.Windows_11_UAC_Shield_16_x_16,
            20 => Windows11UACShieldImageResources.Windows_11_UAC_Shield_20_x_20,
            24 => Windows11UACShieldImageResources.Windows_11_UAC_Shield_24_x_24,
            32 => Windows11UACShieldImageResources.Windows_11_UAC_Shield_32_x_32,
            40 => Windows11UACShieldImageResources.Windows_11_UAC_Shield_40_x_40,
            48 => Windows11UACShieldImageResources.Windows_11_UAC_Shield_48_x_48,
            64 => Windows11UACShieldImageResources.Windows_11_UAC_Shield_64_x_64,
            96 => Windows11UACShieldImageResources.Windows_11_UAC_Shield_64_x_64, // Use 64x64 for 96x96
            128 => Windows11UACShieldImageResources.Windows_11_UAC_Shield_64_x_64, // Use 64x64 for 128x128
            192 => Windows11UACShieldImageResources.Windows_11_UAC_Shield_256_x_256, // Use 256x256 for 192x192
            256 => Windows11UACShieldImageResources.Windows_11_UAC_Shield_256_x_256,
            _ => Windows11UACShieldImageResources.Windows_11_UAC_Shield_32_x_32 // Default to 32x32
        };
    }

    /// <summary>Gets a Windows 10 UAC shield image at the specified size.</summary>
    /// <param name="targetSize">The target size.</param>
    /// <returns>The shield image, or null if not available.</returns>
    private static Image? GetWindows10ShieldImage(Size targetSize)
    {
        return targetSize.Width switch
        {
            8 => Windows10UACShieldImageResources.Windows_10_UAC_Shield_16_x_16, // Use 16x16 for 8x8
            16 => Windows10UACShieldImageResources.Windows_10_UAC_Shield_16_x_16,
            20 => Windows10UACShieldImageResources.Windows_10_UAC_Shield_20_x_20,
            24 => Windows10UACShieldImageResources.Windows_10_UAC_Shield_24_x_24,
            32 => Windows10UACShieldImageResources.Windows_10_UAC_Shield_32_x_32,
            40 => Windows10UACShieldImageResources.Windows_10_UAC_Shield_40_x_40,
            48 => Windows10UACShieldImageResources.Windows_10_UAC_Shield_48_x_48,
            64 => Windows10UACShieldImageResources.Windows_10_UAC_Shield_64_x_64,
            96 => Windows10UACShieldImageResources.Windows_10_UAC_Shield_64_x_64, // Use 64x64 for 96x96
            128 => Windows10UACShieldImageResources.Windows_10_UAC_Shield_64_x_64, // Use 64x64 for 128x128
            192 => Windows10UACShieldImageResources.Windows_10_UAC_Shield_256_x_256, // Use 256x256 for 192x192
            256 => Windows10UACShieldImageResources.Windows_10_UAC_Shield_256_x_256,
            _ => Windows10UACShieldImageResources.Windows_10_UAC_Shield_32_x_32 // Default to 32x32
        };
    }

    /// <summary>Gets a Windows 7/8.x UAC shield image at the specified size.</summary>
    /// <param name="targetSize">The target size.</param>
    /// <returns>The shield image, or null if not available.</returns>
    private static Image? GetWindows7And8xShieldImage(Size targetSize)
    {
        return targetSize.Width switch
        {
            8 => Windows7And8xUACShieldImageResources.Windows_7_and_8x_UAC_Shield_8_x_8,
            16 => Windows7And8xUACShieldImageResources.Windows_7_and_8x_UAC_Shield_16_x_16,
            24 => Windows7And8xUACShieldImageResources.Windows_7_and_8x_UAC_Shield_24_x_24,
            32 => Windows7And8xUACShieldImageResources.Windows_7_and_8x_UAC_Shield_32_x_32,
            48 => Windows7And8xUACShieldImageResources.Windows_7_and_8x_UAC_Shield_48_x_48,
            64 => Windows7And8xUACShieldImageResources.Windows_7_and_8x_UAC_Shield_64_x_64,
            96 => Windows7And8xUACShieldImageResources.Windows_7_and_8x_UAC_Shield_64_x_64, // Use 64x64 for 96x96
            128 => Windows7And8xUACShieldImageResources.Windows_7_and_8x_UAC_Shield_128_x_128,
            192 => Windows7And8xUACShieldImageResources.Windows_7_and_8x_UAC_Shield_256_x_256, // Use 256x256 for 192x192
            256 => Windows7And8xUACShieldImageResources.Windows_7_and_8x_UAC_Shield_256_x_256,
            _ => Windows7And8xUACShieldImageResources.Windows_7_and_8x_UAC_Shield_32_x_32 // Default to 32x32
        };
    }

    /// <summary>Gets a UAC shield image based on the current Krypton theme.</summary>
    /// <param name="targetSize">The target size.</param>
    /// <returns>The shield image, or null if not available.</returns>
    private static Image? GetThemeBasedShieldImage(Size targetSize)
    {
        var currentTheme = KryptonManager.CurrentGlobalPaletteMode;

        // Map themes to appropriate Windows versions
        if (IsVistaCompatibleTheme(currentTheme))
        {
            return GetWindowsVistaShieldImage(targetSize);
        }
        else if (IsWindows7CompatibleTheme(currentTheme))
        {
            return GetWindows7And8xShieldImage(targetSize);
        }
        else if (IsWindows10CompatibleTheme(currentTheme))
        {
            // Prefer Windows 11 icons for modern themes, fallback to Windows 10
            if (OSUtilities.IsAtLeastWindowsEleven)
            {
                return GetWindows11ShieldImage(targetSize);
            }
            else
            {
                return GetWindows10ShieldImage(targetSize);
            }
        }
        else
        {
            // Default to OS-based selection for unknown themes
            return GetOSBasedShieldImage(targetSize);
        }
    }

    /// <summary>Gets a UAC shield image based on the current OS.</summary>
    /// <param name="targetSize">The target size.</param>
    /// <returns>The shield image, or null if not available.</returns>
    private static Image? GetOSBasedShieldImage(Size targetSize)
    {
        // Get the appropriate shield image based on OS
        if (OSUtilities.IsAtLeastWindowsEleven)
        {
            return GetWindows11ShieldImage(targetSize);
        }
        else if (OSUtilities.IsWindowsTen)
        {
            return GetWindows10ShieldImage(targetSize);
        }
        else if (OSUtilities.IsWindowsEightPointOne || OSUtilities.IsWindowsEight || OSUtilities.IsWindowsSeven)
        {
            return GetWindows7And8xShieldImage(targetSize);
        }
        else
        {
            return GetWindowsVistaShieldImage(targetSize);
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

    /// <summary>Gets a Windows Vista UAC shield image at the specified size.</summary>
    /// <param name="targetSize">The target size.</param>
    /// <returns>The shield image, or null if not available.</returns>
    private static Image? GetWindowsVistaShieldImage(Size targetSize)
    {
        return targetSize.Width switch
        {
            8 => WindowsVistaUACShieldImageResources.Windows_Vista_UAC_Shield_8_x_8,
            16 => WindowsVistaUACShieldImageResources.Windows_Vista_UAC_Shield_16_x_16,
            24 => WindowsVistaUACShieldImageResources.Windows_Vista_UAC_Shield_24_x_24,
            32 => WindowsVistaUACShieldImageResources.Windows_Vista_UAC_Shield_32_x_32,
            48 => WindowsVistaUACShieldImageResources.Windows_Vista_UAC_Shield_48_x_48,
            64 => WindowsVistaUACShieldImageResources.Windows_Vista_UAC_Shield_32_x_32, // Use 32x32 for 64x64
            96 => WindowsVistaUACShieldImageResources.Windows_Vista_UAC_Shield_128_x_128, // Use 128x128 for 96x96
            128 => WindowsVistaUACShieldImageResources.Windows_Vista_UAC_Shield_128_x_128,
            192 => WindowsVistaUACShieldImageResources.Windows_Vista_UAC_Shield_256_x_256, // Use 256x256 for 192x192
            256 => WindowsVistaUACShieldImageResources.Windows_Vista_UAC_Shield_256_x_256,
            _ => WindowsVistaUACShieldImageResources.Windows_Vista_UAC_Shield_32_x_32 // Default to 32x32
        };
    }


    #endregion
}