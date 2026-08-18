#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
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

            KryptonExceptionHandler.CaptureException(e, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
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
            KryptonExceptionHandler.CaptureException(e, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);

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
            ThrowHelper.ThrowArgumentNullException(nameof(filePath));
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
            KryptonExceptionHandler.CaptureException(ex, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);

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
                return ThrowHelper.ThrowArgumentOutOfRangeException<Image?>(nameof(iconType), iconType, null);
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
            return iconId switch
            {
                ImageresIconID.Shield or ImageresIconID.ShieldAlt => GetUACShieldFallbackIcon(targetSize,
                    selectionStrategy),
                _ => null
            };
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
    public static Image? GetThemeBasedShieldImage(Size targetSize)
    {
        var currentTheme = KryptonManager.CurrentGlobalPaletteMode;

        switch (KryptonThemeChrome.GetShieldIconStyle(currentTheme))
        {
            case KryptonThemeShieldIconStyle.Vista:
                return GetWindowsVistaShieldImage(targetSize);
            case KryptonThemeShieldIconStyle.Windows7:
                return GetWindows7And8xShieldImage(targetSize);
            case KryptonThemeShieldIconStyle.Windows10:
                return OSUtilities.IsAtLeastWindowsEleven
                    ? GetWindows11ShieldImage(targetSize)
                    : GetWindows10ShieldImage(targetSize);
            default:
                return GetOSBasedShieldImage(targetSize);
        }
    }

    /// <summary>Gets a UAC shield image based on the current OS.</summary>
    /// <param name="targetSize">The target size.</param>
    /// <returns>The shield image, or null if not available.</returns>
    public static Image? GetOSBasedShieldImage(Size targetSize)
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

    /// <summary>
    /// Composites an optional overlay (badge) image onto a main image.
    /// Position and scale behaviour match content overlay rendering in <see cref="RenderStandard"/>.
    /// The caller owns the returned <see cref="Bitmap"/> and must dispose it.
    /// </summary>
    /// <param name="main">The base image. Must not be null.</param>
    /// <param name="overlay">The overlay image. When null, a clone of <paramref name="main"/> is returned.</param>
    /// <param name="position">Corner placement of the overlay relative to <paramref name="main"/>.</param>
    /// <param name="scaleMode">How the overlay is sized relative to <paramref name="main"/>.</param>
    /// <param name="scaleFactor">Scale factor used by <see cref="OverlayImageScaleMode.Percentage"/> and <see cref="OverlayImageScaleMode.ProportionalToMain"/>.</param>
    /// <param name="fixedSize">Size used when <paramref name="scaleMode"/> is <see cref="OverlayImageScaleMode.FixedSize"/>.</param>
    /// <param name="transparentColor">Colour treated as transparent when drawing the overlay; use <see cref="Color.Empty"/> for none.</param>
    /// <param name="rightToLeft">When true, Left/Right corners are mirrored for RTL layouts.</param>
    /// <returns>A new bitmap the size of <paramref name="main"/>, or null if composition fails.</returns>
    public static Bitmap? ComposeOverlayImage(Image main,
        Image? overlay,
        OverlayImagePosition position,
        OverlayImageScaleMode scaleMode,
        float scaleFactor,
        Size fixedSize,
        Color transparentColor,
        bool rightToLeft)
    {
        if (main == null)
        {
            throw new ArgumentNullException(nameof(main));
        }

        try
        {
            var result = new Bitmap(main.Width, main.Height);
            result.SetResolution(main.HorizontalResolution, main.VerticalResolution);

            using (var graphics = Graphics.FromImage(result))
            {
                graphics.CompositingMode = CompositingMode.SourceOver;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.SmoothingMode = SmoothingMode.HighQuality;

                graphics.DrawImage(main, new Rectangle(0, 0, main.Width, main.Height));

                if (overlay == null)
                {
                    return result;
                }

                Size overlaySize = CalculateOverlaySize(main.Size, overlay.Size, scaleMode, scaleFactor, fixedSize);
                OverlayImagePosition drawPosition = MirrorOverlayPosition(position, rightToLeft);
                Point location = CalculateOverlayLocation(main.Size, overlaySize, drawPosition);

                var destRect = new Rectangle(location, overlaySize);
                if (transparentColor.IsEmpty)
                {
                    graphics.DrawImage(overlay, destRect);
                }
                else
                {
                    using var attributes = new ImageAttributes();
                    attributes.SetColorKey(transparentColor, transparentColor);
                    graphics.DrawImage(overlay, destRect, 0, 0, overlay.Width, overlay.Height, GraphicsUnit.Pixel, attributes);
                }
            }

            return result;
        }
        catch (Exception e)
        {
            KryptonExceptionHandler.CaptureException(e, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
            return null;
        }
    }

    /// <summary>
    /// Composites <paramref name="overlay"/> onto <paramref name="main"/> when the overlay is non-empty.
    /// Applies dialog-friendly defaults for zero scale factor / empty fixed size.
    /// </summary>
    /// <param name="main">The base image.</param>
    /// <param name="overlay">Overlay settings; ignored when <see cref="KryptonOverlayImage.IsEmpty"/>.</param>
    /// <param name="rightToLeft">Whether Left/Right corners should be mirrored.</param>
    /// <returns>A new composed bitmap, or null when no overlay applies or composition fails.</returns>
    public static Bitmap? TryComposeOverlay(Image? main, KryptonOverlayImage overlay, bool rightToLeft)
    {
        if (main == null || overlay.IsEmpty)
        {
            return null;
        }

        float scaleFactor = overlay.ScaleFactor > 0f
            ? overlay.ScaleFactor
            : KryptonOverlayImage.DefaultScaleFactor;
        Size fixedSize = overlay.FixedSize.IsEmpty
            ? KryptonOverlayImage.DefaultFixedSize
            : overlay.FixedSize;
        OverlayImageScaleMode scaleMode = overlay.ScaleMode == OverlayImageScaleMode.None
            && overlay.ScaleFactor <= 0f
            ? OverlayImageScaleMode.Percentage
            : overlay.ScaleMode;

        return ComposeOverlayImage(main, overlay.Image, overlay.Position, scaleMode, scaleFactor, fixedSize,
            overlay.ImageTransparentColor, rightToLeft);
    }

    private static Size CalculateOverlaySize(Size mainSize, Size originalOverlaySize, OverlayImageScaleMode scaleMode,
        float scaleFactor, Size fixedSize)
    {
        Size overlaySize = originalOverlaySize;

        switch (scaleMode)
        {
            case OverlayImageScaleMode.None:
                overlaySize = originalOverlaySize;
                break;

            case OverlayImageScaleMode.Percentage:
            {
                float mainImageMinDim = Math.Min(mainSize.Width, mainSize.Height);
                float targetSize = mainImageMinDim * scaleFactor;
                if (targetSize > 0 && originalOverlaySize.Width > 0 && originalOverlaySize.Height > 0)
                {
                    float scale = Math.Min(
                        targetSize / originalOverlaySize.Width,
                        targetSize / originalOverlaySize.Height);
                    overlaySize = new Size(
                        (int)(originalOverlaySize.Width * scale),
                        (int)(originalOverlaySize.Height * scale));
                }

                break;
            }

            case OverlayImageScaleMode.FixedSize:
                overlaySize = fixedSize;
                break;

            case OverlayImageScaleMode.ProportionalToMain:
            {
                float propMainImageMinDim = Math.Min(mainSize.Width, mainSize.Height);
                float propTargetSize = propMainImageMinDim * scaleFactor;
                if (propTargetSize > 0 && originalOverlaySize.Width > 0 && originalOverlaySize.Height > 0)
                {
                    float propScale = Math.Min(
                        propTargetSize / originalOverlaySize.Width,
                        propTargetSize / originalOverlaySize.Height);
                    overlaySize = new Size(
                        (int)(originalOverlaySize.Width * propScale),
                        (int)(originalOverlaySize.Height * propScale));
                }

                break;
            }
        }

        return new Size(Math.Max(1, overlaySize.Width), Math.Max(1, overlaySize.Height));
    }

    private static OverlayImagePosition MirrorOverlayPosition(OverlayImagePosition position, bool rightToLeft)
    {
        if (!rightToLeft)
        {
            return position;
        }

        return position switch
        {
            OverlayImagePosition.TopLeft => OverlayImagePosition.TopRight,
            OverlayImagePosition.TopRight => OverlayImagePosition.TopLeft,
            OverlayImagePosition.BottomLeft => OverlayImagePosition.BottomRight,
            OverlayImagePosition.BottomRight => OverlayImagePosition.BottomLeft,
            _ => position
        };
    }

    private static Point CalculateOverlayLocation(Size mainSize, Size overlaySize, OverlayImagePosition position)
    {
        switch (position)
        {
            case OverlayImagePosition.TopLeft:
                return new Point(0, 0);
            case OverlayImagePosition.TopRight:
                return new Point(mainSize.Width - overlaySize.Width, 0);
            case OverlayImagePosition.BottomLeft:
                return new Point(0, mainSize.Height - overlaySize.Height);
            case OverlayImagePosition.BottomRight:
            default:
                return new Point(mainSize.Width - overlaySize.Width, mainSize.Height - overlaySize.Height);
        }
    }

    #endregion
}
