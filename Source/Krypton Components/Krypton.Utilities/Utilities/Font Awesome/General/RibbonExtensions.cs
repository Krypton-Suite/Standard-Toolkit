#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Extension methods for integrating Font Awesome icons with Krypton Ribbon components.
/// </summary>
public static class RibbonExtensions
{
    /// <summary>
    /// Sets Font Awesome icons on a Ribbon Group Button for both small and large sizes.
    /// </summary>
    /// <param name="button">The KryptonRibbonGroupButton instance.</param>
    /// <param name="icon">The Font Awesome icon enum value.</param>
    /// <param name="smallSize">The size for the small image. If 0, uses default size (16px).</param>
    /// <param name="largeSize">The size for the large image. If 0, uses smallSize * 2.</param>
    /// <param name="color">The color of the icons. If null, uses the default color.</param>
    /// <param name="style">The Font Awesome style. If null, uses the default style.</param>
    /// <returns>The KryptonRibbonGroupButton instance for method chaining.</returns>
    public static KryptonRibbonGroupButton SetFontAwesomeIcons(this KryptonRibbonGroupButton button, FontAwesomeIcon icon,
        int smallSize = 16, int largeSize = 0, Color? color = null, FontAwesomeStyle? style = null)
    {
        if (button == null)
        {
            throw new ArgumentNullException(nameof(button));
        }

        smallSize = smallSize > 0 ? smallSize : FontAwesomeHelper.DefaultSize;
        largeSize = largeSize > 0 ? largeSize : smallSize * 2;

        var iconColor = color ?? FontAwesomeHelper.DefaultColor;
        var iconStyle = style ?? FontAwesomeHelper.DefaultStyle;

        // Set small image
        var smallBitmap = FontAwesomeHelper.RenderIcon(icon, smallSize, iconColor, iconStyle);
        if (smallBitmap != null)
        {
            button.ImageSmall = smallBitmap;
        }

        // Set large image
        var largeBitmap = FontAwesomeHelper.RenderIcon(icon, largeSize, iconColor, iconStyle);
        if (largeBitmap != null)
        {
            button.ImageLarge = largeBitmap;
        }

        return button;
    }

    /// <summary>
    /// Sets Font Awesome icons on a Ribbon Group Button using icon name.
    /// </summary>
    /// <param name="button">The KryptonRibbonGroupButton instance.</param>
    /// <param name="iconName">The Font Awesome icon name.</param>
    /// <param name="smallSize">The size for the small image. If 0, uses default size (16px).</param>
    /// <param name="largeSize">The size for the large image. If 0, uses smallSize * 2.</param>
    /// <param name="color">The color of the icons. If null, uses the default color.</param>
    /// <param name="style">The Font Awesome style. If null, uses the default style.</param>
    /// <returns>The KryptonRibbonGroupButton instance for method chaining.</returns>
    public static KryptonRibbonGroupButton SetFontAwesomeIcons(this KryptonRibbonGroupButton button, string iconName,
        int smallSize = 16, int largeSize = 0, Color? color = null, FontAwesomeStyle? style = null)
    {
        if (button == null)
        {
            throw new ArgumentNullException(nameof(button));
        }

        smallSize = smallSize > 0 ? smallSize : FontAwesomeHelper.DefaultSize;
        largeSize = largeSize > 0 ? largeSize : smallSize * 2;

        var iconColor = color ?? FontAwesomeHelper.DefaultColor;
        var iconStyle = style ?? FontAwesomeHelper.DefaultStyle;

        // Set small image
        var smallBitmap = FontAwesomeHelper.RenderIcon(iconName, smallSize, iconColor, iconStyle);
        if (smallBitmap != null)
        {
            button.ImageSmall = smallBitmap;
        }

        // Set large image
        var largeBitmap = FontAwesomeHelper.RenderIcon(iconName, largeSize, iconColor, iconStyle);
        if (largeBitmap != null)
        {
            button.ImageLarge = largeBitmap;
        }

        return button;
    }
}
