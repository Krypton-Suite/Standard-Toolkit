#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Extension methods for integrating Font Awesome icons with Krypton Navigator pages.
/// </summary>
public static class NavigatorExtensions
{
    /// <summary>
    /// Sets Font Awesome icons on a Navigator page for different image sizes.
    /// </summary>
    /// <param name="page">The KryptonPage instance.</param>
    /// <param name="icon">The Font Awesome icon enum value.</param>
    /// <param name="smallSize">The size for the small image. If 0, uses default size.</param>
    /// <param name="mediumSize">The size for the medium image. If 0, uses smallSize * 1.5.</param>
    /// <param name="largeSize">The size for the large image. If 0, uses smallSize * 2.</param>
    /// <param name="color">The color of the icons. If null, uses the default color.</param>
    /// <param name="style">The Font Awesome style. If null, uses the default style.</param>
    /// <returns>The KryptonPage instance for method chaining.</returns>
    public static KryptonPage SetFontAwesomeIcons(this KryptonPage page, FontAwesomeIcon icon,
        int smallSize = 16, int mediumSize = 0, int largeSize = 0,
        Color? color = null, FontAwesomeStyle? style = null)
    {
        if (page == null)
        {
            throw new ArgumentNullException(nameof(page));
        }

        smallSize = smallSize > 0 ? smallSize : FontAwesomeHelper.DefaultSize;
        mediumSize = mediumSize > 0 ? mediumSize : (int)(smallSize * 1.5);
        largeSize = largeSize > 0 ? largeSize : smallSize * 2;

        var iconColor = color ?? FontAwesomeHelper.DefaultColor;
        var iconStyle = style ?? FontAwesomeHelper.DefaultStyle;

        // Set small image
        var smallBitmap = FontAwesomeHelper.RenderIcon(icon, smallSize, iconColor, iconStyle);
        if (smallBitmap != null)
        {
            page.ImageSmall?.Dispose();
            page.ImageSmall = smallBitmap;
        }

        // Set medium image
        var mediumBitmap = FontAwesomeHelper.RenderIcon(icon, mediumSize, iconColor, iconStyle);
        if (mediumBitmap != null)
        {
            page.ImageMedium?.Dispose();
            page.ImageMedium = mediumBitmap;
        }

        // Set large image
        var largeBitmap = FontAwesomeHelper.RenderIcon(icon, largeSize, iconColor, iconStyle);
        if (largeBitmap != null)
        {
            page.ImageLarge?.Dispose();
            page.ImageLarge = largeBitmap;
        }

        return page;
    }

    /// <summary>
    /// Sets Font Awesome icons on a Navigator page using icon name.
    /// </summary>
    /// <param name="page">The KryptonPage instance.</param>
    /// <param name="iconName">The Font Awesome icon name.</param>
    /// <param name="smallSize">The size for the small image. If 0, uses default size.</param>
    /// <param name="mediumSize">The size for the medium image. If 0, uses smallSize * 1.5.</param>
    /// <param name="largeSize">The size for the large image. If 0, uses smallSize * 2.</param>
    /// <param name="color">The color of the icons. If null, uses the default color.</param>
    /// <param name="style">The Font Awesome style. If null, uses the default style.</param>
    /// <returns>The KryptonPage instance for method chaining.</returns>
    public static KryptonPage SetFontAwesomeIcons(this KryptonPage page, string iconName,
        int smallSize = 16, int mediumSize = 0, int largeSize = 0,
        Color? color = null, FontAwesomeStyle? style = null)
    {
        if (page == null)
        {
            throw new ArgumentNullException(nameof(page));
        }

        smallSize = smallSize > 0 ? smallSize : FontAwesomeHelper.DefaultSize;
        mediumSize = mediumSize > 0 ? mediumSize : (int)(smallSize * 1.5);
        largeSize = largeSize > 0 ? largeSize : smallSize * 2;

        var iconColor = color ?? FontAwesomeHelper.DefaultColor;
        var iconStyle = style ?? FontAwesomeHelper.DefaultStyle;

        // Set small image
        var smallBitmap = FontAwesomeHelper.RenderIcon(iconName, smallSize, iconColor, iconStyle);
        if (smallBitmap != null)
        {
            page.ImageSmall?.Dispose();
            page.ImageSmall = smallBitmap;
        }

        // Set medium image
        var mediumBitmap = FontAwesomeHelper.RenderIcon(iconName, mediumSize, iconColor, iconStyle);
        if (mediumBitmap != null)
        {
            page.ImageMedium?.Dispose();
            page.ImageMedium = mediumBitmap;
        }

        // Set large image
        var largeBitmap = FontAwesomeHelper.RenderIcon(iconName, largeSize, iconColor, iconStyle);
        if (largeBitmap != null)
        {
            page.ImageLarge?.Dispose();
            page.ImageLarge = largeBitmap;
        }

        return page;
    }

    /// <summary>
    /// Sets a Font Awesome icon as the tooltip image for a Navigator page.
    /// </summary>
    /// <param name="page">The KryptonPage instance.</param>
    /// <param name="icon">The Font Awesome icon enum value.</param>
    /// <param name="size">The size of the icon. If 0, uses default size.</param>
    /// <param name="color">The color of the icon. If null, uses the default color.</param>
    /// <param name="style">The Font Awesome style. If null, uses the default style.</param>
    /// <returns>The KryptonPage instance for method chaining.</returns>
    public static KryptonPage SetFontAwesomeToolTipIcon(this KryptonPage page, FontAwesomeIcon icon,
        int size = 0, Color? color = null, FontAwesomeStyle? style = null)
    {
        if (page == null)
        {
            throw new ArgumentNullException(nameof(page));
        }

        var iconBitmap = FontAwesomeHelper.RenderIcon(icon, size, color, style);
        if (iconBitmap != null)
        {
            page.ToolTipImage?.Dispose();
            page.ToolTipImage = iconBitmap;
        }

        return page;
    }

    /// <summary>
    /// Sets a Font Awesome icon as the tooltip image for a Navigator page using icon name.
    /// </summary>
    /// <param name="page">The KryptonPage instance.</param>
    /// <param name="iconName">The Font Awesome icon name.</param>
    /// <param name="size">The size of the icon. If 0, uses default size.</param>
    /// <param name="color">The color of the icon. If null, uses the default color.</param>
    /// <param name="style">The Font Awesome style. If null, uses the default style.</param>
    /// <returns>The KryptonPage instance for method chaining.</returns>
    public static KryptonPage SetFontAwesomeToolTipIcon(this KryptonPage page, string iconName,
        int size = 0, Color? color = null, FontAwesomeStyle? style = null)
    {
        if (page == null)
        {
            throw new ArgumentNullException(nameof(page));
        }

        var iconBitmap = FontAwesomeHelper.RenderIcon(iconName, size, color, style);
        if (iconBitmap != null)
        {
            page.ToolTipImage?.Dispose();
            page.ToolTipImage = iconBitmap;
        }

        return page;
    }
}
