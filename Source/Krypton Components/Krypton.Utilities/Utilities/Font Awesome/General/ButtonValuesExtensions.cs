#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Extension methods for integrating Font Awesome icons with Krypton ButtonValues.
/// </summary>
public static class ButtonValuesExtensions
{
    /// <summary>
    /// Sets a Font Awesome icon on the button using the icon name.
    /// </summary>
    /// <param name="buttonValues">The ButtonValues instance.</param>
    /// <param name="iconName">The Font Awesome icon name (e.g., "home", "user", "cog").</param>
    /// <param name="size">The size of the icon in pixels. If 0, uses the default size.</param>
    /// <param name="color">The color of the icon. If null, uses the default color.</param>
    /// <param name="style">The Font Awesome style. If null, uses the default style.</param>
    /// <returns>The ButtonValues instance for method chaining.</returns>
    public static ButtonValues SetFontAwesomeIcon(this ButtonValues buttonValues, string iconName, int size = 0, Color? color = null, FontAwesomeStyle? style = null)
    {
        if (buttonValues == null)
        {
            throw new ArgumentNullException(nameof(buttonValues));
        }

        var iconBitmap = FontAwesomeHelper.RenderIcon(iconName, size, color, style);
        if (iconBitmap != null)
        {
            if (buttonValues.Image is Bitmap previousBitmap)
            {
                previousBitmap.Dispose();
            }
            buttonValues.Image = iconBitmap;
        }

        return buttonValues;
    }

    /// <summary>
    /// Sets a Font Awesome icon on the button using the icon enum.
    /// </summary>
    /// <param name="buttonValues">The ButtonValues instance.</param>
    /// <param name="icon">The Font Awesome icon enum value.</param>
    /// <param name="size">The size of the icon in pixels. If 0, uses the default size.</param>
    /// <param name="color">The color of the icon. If null, uses the default color.</param>
    /// <param name="style">The Font Awesome style. If null, uses the default style.</param>
    /// <returns>The ButtonValues instance for method chaining.</returns>
    public static ButtonValues SetFontAwesomeIcon(this ButtonValues buttonValues, FontAwesomeIcon icon, int size = 0, Color? color = null, FontAwesomeStyle? style = null)
    {
        if (buttonValues == null)
        {
            throw new ArgumentNullException(nameof(buttonValues));
        }

        var iconBitmap = FontAwesomeHelper.RenderIcon(icon, size, color, style);
        if (iconBitmap != null)
        {
            if (buttonValues.Image is Bitmap previousBitmap)
            {
                previousBitmap.Dispose();
            }
            buttonValues.Image = iconBitmap;
        }

        return buttonValues;
    }

    /// <summary>
    /// Sets Font Awesome icons for different button states.
    /// </summary>
    /// <param name="buttonValues">The ButtonValues instance.</param>
    /// <param name="icon">The Font Awesome icon enum value.</param>
    /// <param name="normalColor">The color for the normal state.</param>
    /// <param name="disabledColor">The color for the disabled state. If null, uses a grayed version.</param>
    /// <param name="pressedColor">The color for the pressed state. If null, uses normal color.</param>
    /// <param name="trackingColor">The color for the tracking (hover) state. If null, uses normal color.</param>
    /// <param name="size">The size of the icon in pixels. If 0, uses the default size.</param>
    /// <param name="style">The Font Awesome style. If null, uses the default style.</param>
    /// <returns>The ButtonValues instance for method chaining.</returns>
    public static ButtonValues SetFontAwesomeIconStates(this ButtonValues buttonValues, FontAwesomeIcon icon,
        Color normalColor, Color? disabledColor = null, Color? pressedColor = null, Color? trackingColor = null,
        int size = 0, FontAwesomeStyle? style = null)
    {
        if (buttonValues == null)
        {
            throw new ArgumentNullException(nameof(buttonValues));
        }

        // Normal state
        var normalBitmap = FontAwesomeHelper.RenderIcon(icon, size, normalColor, style);
        if (normalBitmap != null)
        {
            if (buttonValues.Image is Bitmap previousImage)
            {
                previousImage.Dispose();
            }
            if (buttonValues.ImageStates.ImageNormal is Bitmap previousNormal)
            {
                previousNormal.Dispose();
            }
            buttonValues.Image = normalBitmap;
            buttonValues.ImageStates.ImageNormal = normalBitmap;
        }

        // Disabled state (grayed out)
        var disabledBitmap = FontAwesomeHelper.RenderIcon(icon, size,
            disabledColor ?? Color.FromArgb(128, normalColor), style);
        if (disabledBitmap != null)
        {
            if (buttonValues.ImageStates.ImageDisabled is Bitmap previousDisabled)
            {
                previousDisabled.Dispose();
            }
            buttonValues.ImageStates.ImageDisabled = disabledBitmap;
        }

        // Pressed state
        var pressedBitmap = FontAwesomeHelper.RenderIcon(icon, size, pressedColor ?? normalColor, style);
        if (pressedBitmap != null)
        {
            if (buttonValues.ImageStates.ImagePressed is Bitmap previousPressed)
            {
                previousPressed.Dispose();
            }
            buttonValues.ImageStates.ImagePressed = pressedBitmap;
        }

        // Tracking (hover) state
        var trackingBitmap = FontAwesomeHelper.RenderIcon(icon, size, trackingColor ?? normalColor, style);
        if (trackingBitmap != null)
        {
            if (buttonValues.ImageStates.ImageTracking is Bitmap previousTracking)
            {
                previousTracking.Dispose();
            }
            buttonValues.ImageStates.ImageTracking = trackingBitmap;
        }

        return buttonValues;
    }

    /// <summary>
    /// Sets Font Awesome icons for different button states using icon name.
    /// </summary>
    /// <param name="buttonValues">The ButtonValues instance.</param>
    /// <param name="iconName">The Font Awesome icon name.</param>
    /// <param name="normalColor">The color for the normal state.</param>
    /// <param name="disabledColor">The color for the disabled state. If null, uses a grayed version.</param>
    /// <param name="pressedColor">The color for the pressed state. If null, uses normal color.</param>
    /// <param name="trackingColor">The color for the tracking (hover) state. If null, uses normal color.</param>
    /// <param name="size">The size of the icon in pixels. If 0, uses the default size.</param>
    /// <param name="style">The Font Awesome style. If null, uses the default style.</param>
    /// <returns>The ButtonValues instance for method chaining.</returns>
    public static ButtonValues SetFontAwesomeIconStates(this ButtonValues buttonValues, string iconName,
        Color normalColor, Color? disabledColor = null, Color? pressedColor = null, Color? trackingColor = null,
        int size = 0, FontAwesomeStyle? style = null)
    {
        if (buttonValues == null)
        {
            throw new ArgumentNullException(nameof(buttonValues));
        }

        // Normal state
        var normalBitmap = FontAwesomeHelper.RenderIcon(iconName, size, normalColor, style);
        if (normalBitmap != null)
        {
            if (buttonValues.Image is Bitmap previousImage)
            {
                previousImage.Dispose();
            }
            if (buttonValues.ImageStates.ImageNormal is Bitmap previousNormal)
            {
                previousNormal.Dispose();
            }
            buttonValues.Image = normalBitmap;
            buttonValues.ImageStates.ImageNormal = normalBitmap;
        }

        // Disabled state (grayed out)
        var disabledBitmap = FontAwesomeHelper.RenderIcon(iconName, size,
            disabledColor ?? Color.FromArgb(128, normalColor), style);
        if (disabledBitmap != null)
        {
            if (buttonValues.ImageStates.ImageDisabled is Bitmap previousDisabled)
            {
                previousDisabled.Dispose();
            }
            buttonValues.ImageStates.ImageDisabled = disabledBitmap;
        }

        // Pressed state
        var pressedBitmap = FontAwesomeHelper.RenderIcon(iconName, size, pressedColor ?? normalColor, style);
        if (pressedBitmap != null)
        {
            if (buttonValues.ImageStates.ImagePressed is Bitmap previousPressed)
            {
                previousPressed.Dispose();
            }
            buttonValues.ImageStates.ImagePressed = pressedBitmap;
        }

        // Tracking (hover) state
        var trackingBitmap = FontAwesomeHelper.RenderIcon(iconName, size, trackingColor ?? normalColor, style);
        if (trackingBitmap != null)
        {
            if (buttonValues.ImageStates.ImageTracking is Bitmap previousTracking)
            {
                previousTracking.Dispose();
            }
            buttonValues.ImageStates.ImageTracking = trackingBitmap;
        }

        return buttonValues;
    }
}
