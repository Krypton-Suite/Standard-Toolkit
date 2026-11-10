#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using ToastImageResources = Krypton.Toolkit.Utilities.Components.Krypton_Toast.Resources.ToastImageResources;

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Toast-specific graphics helpers. Image scaling and imageres extraction delegate to
/// <see cref="GraphicsExtensions"/>.
/// </summary>
public static class GraphicsExtensionUtilities
{
    public const int DEFAULT_TOAST_ICON_SIZE = 128;

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
                return GraphicsExtensions.ScaleImage(SystemIcons.Hand.ToBitmap(), newSize);
            case KryptonToastIcon.Question:
                return ToastImageResources.Toast_Question_128_x_128;
            case KryptonToastIcon.SystemQuestion:
                return GraphicsExtensions.ScaleImage(SystemIcons.Question.ToBitmap(), newSize);
            case KryptonToastIcon.Exclamation:
            case KryptonToastIcon.SystemExclamation:
            case KryptonToastIcon.Warning:
                return ToastImageResources.Toast_Warning_128_x_115;
            case KryptonToastIcon.Asterisk:
                return ToastImageResources.Toast_Asterisk_128_x_128;
            case KryptonToastIcon.Error:
                return ToastImageResources.Toast_Critical_128_x_128;
            case KryptonToastIcon.SystemAsterisk:
                return GraphicsExtensions.ScaleImage(SystemIcons.Asterisk.ToBitmap(), newSize);
            case KryptonToastIcon.Stop:
                return ToastImageResources.Toast_Stop_128_x_128;
            case KryptonToastIcon.Information:
                return ToastImageResources.Toast_Information_128_x_128;
            case KryptonToastIcon.Shield:
                {
                    var messageBoxShieldIcon = GraphicsExtensions.ExtractIconFromImageres((int)ImageresIconID.Shield, IconSize.Huge);
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
                    return GraphicsExtensions.ScaleImage(SystemIcons.WinLogo.ToBitmap(), newSize);
                }
            case KryptonToastIcon.Application:
                return customImage != null
                    ? GraphicsExtensions.ScaleImage(customImage, newSize)
                    : GraphicsExtensions.ScaleImage(SystemIcons.Application.ToBitmap(), newSize);
            case KryptonToastIcon.SystemApplication:
                return GraphicsExtensions.ScaleImage(SystemIcons.Application.ToBitmap(), newSize);
            case KryptonToastIcon.Ok:
                return ToastImageResources.Toast_Ok_128_x_128;
            case KryptonToastIcon.Custom:
                return customImage != null ? GraphicsExtensions.ScaleImage(customImage, newSize) : null;
            default:
                DebugTools.NotImplemented(notificationIconType.ToString());
                return ThrowHelper.ThrowArgumentOutOfRangeException<Image?>(nameof(notificationIconType), notificationIconType, null);
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

    /// <summary>Resolves a toast notification icon size from optional width and height values.</summary>
    /// <param name="width">Optional width.</param>
    /// <param name="height">Optional height.</param>
    /// <returns>A safe size for toast notification icon rendering.</returns>
    public static Size ResolveToastNotificationIconSize(int? width, int? height)
    {
        var resolvedWidth = width ?? DEFAULT_TOAST_ICON_SIZE;
        var resolvedHeight = height ?? DEFAULT_TOAST_ICON_SIZE;

        if (resolvedWidth <= 0)
        {
            resolvedWidth = DEFAULT_TOAST_ICON_SIZE;
        }

        if (resolvedHeight <= 0)
        {
            resolvedHeight = DEFAULT_TOAST_ICON_SIZE;
        }

        return new Size(resolvedWidth, resolvedHeight);
    }

    /// <summary>Extracts an icon from imageres.dll using the specified icon ID and size.</summary>
    /// <param name="iconId">The icon ID from ImageresIconID enum.</param>
    /// <param name="iconSize">The size of the icon to extract. Defaults to Medium (32x32).</param>
    /// <param name="selectionStrategy">The strategy for selecting fallback icons. Defaults to OS-based selection.</param>
    /// <returns>The extracted icon, or null if extraction fails.</returns>
    public static Icon? ExtractIconFromImageres(int iconId, IconSize iconSize = IconSize.Medium, IconSelectionStrategy selectionStrategy = IconSelectionStrategy.OSBased) =>
        GraphicsExtensions.ExtractIconFromImageres(iconId, iconSize, selectionStrategy);
}
