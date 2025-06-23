#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
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
            { result = Icon.ExtractAssociatedIcon(executablePath); }
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
    public static Image? GetKryptonMessageBoxImageType(KryptonMessageBoxIcon iconType, Size? imageSize, Image? customImage = null)
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
                if (OSUtilities.IsAtLeastWindowsEleven)
                {
                    return UACShieldIconResources.UACShieldWindows11;
                }
                else if (OSUtilities.IsWindowsTen)
                {
                    return UACShieldIconResources.UACShieldWindows10;
                }
                else
                {
                    return UACShieldIconResources.UACShieldWindows7881;
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
            case KryptonToastNotificationIcon.Error:
                return ToastNotificationImageResources.Toast_Notification_Asterisk_128_x_128;
            case KryptonToastNotificationIcon.SystemAsterisk:
                return ScaleImage(SystemIcons.Asterisk.ToBitmap(), newSize);
            case KryptonToastNotificationIcon.Stop:
                return ToastNotificationImageResources.Toast_Notification_Stop_128_x_128;
            case KryptonToastNotificationIcon.Information:
                return ToastNotificationImageResources.Toast_Notification_Information_128_x_128;
            case KryptonToastNotificationIcon.Shield:
                if (OSUtilities.IsAtLeastWindowsEleven)
                {
                    return ToastNotificationImageResources.Toast_Notification_UAC_Shield_Windows_11_128_x_128;
                }
                else if (OSUtilities.IsWindowsTen)
                {
                    return ToastNotificationImageResources.Toast_Notification_UAC_Shield_Windows_10_128_x_128;
                }
                else if (OSUtilities.IsWindowsEightPointOne || OSUtilities.IsWindowsEight || OSUtilities.IsWindowsSeven)
                {
                    return ToastNotificationImageResources.Toast_Notification_UAC_Shield_Windows_7_and_8_128_x_128;
                }
                else
                {
                    return ScaleImage(SystemIcons.Shield.ToBitmap(), newSize);
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
                    ? ScaleImage(customImage, newSize) : ScaleImage(SystemIcons.Application.ToBitmap(), newSize);
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
}
#endregion