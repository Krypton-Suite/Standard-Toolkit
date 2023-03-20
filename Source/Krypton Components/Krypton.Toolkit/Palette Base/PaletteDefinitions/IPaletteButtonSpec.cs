namespace Krypton.Toolkit;

/// <summary>
/// Exposes a palette source button specifications.
/// </summary>
public interface IPaletteButtonSpec
{
    /// <summary>
    /// Gets the icon to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>Icon value.</returns>
    Icon? GetButtonSpecIcon(PaletteButtonSpecStyle style);

    /// <summary>
    /// Gets the image to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <param name="state">State for which image is required.</param>
    /// <returns>Image value.</returns>
    Image? GetButtonSpecImage(PaletteButtonSpecStyle style, PaletteState state);

    /// <summary>
    /// Gets the image transparent color.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>Color value.</returns>
    Color GetButtonSpecImageTransparentColor(PaletteButtonSpecStyle style);

    /// <summary>
    /// Gets the short text to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>String value.</returns>
    string? GetButtonSpecShortText(PaletteButtonSpecStyle style);

    /// <summary>
    /// Gets the long text to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>String value.</returns>
    string? GetButtonSpecLongText(PaletteButtonSpecStyle style);

    /// <summary>
    /// Gets the tooltip title text to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>String value.</returns>
    string GetButtonSpecToolTipTitle(PaletteButtonSpecStyle style);

    /// <summary>
    /// Gets the color to remap from the image to the container foreground.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>Color value.</returns>
    Color GetButtonSpecColorMap(PaletteButtonSpecStyle style);

    /// <summary>
    /// Gets the button style used for drawing the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>PaletteButtonStyle value.</returns>
    PaletteButtonStyle GetButtonSpecStyle(PaletteButtonSpecStyle style);

    /// <summary>
    /// Get the location for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>HeaderLocation value.</returns>
    HeaderLocation GetButtonSpecLocation(PaletteButtonSpecStyle style);

    /// <summary>
    /// Gets the edge to position the button against.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>PaletteRelativeEdgeAlign value.</returns>
    PaletteRelativeEdgeAlign GetButtonSpecEdge(PaletteButtonSpecStyle style);

    /// <summary>
    /// Gets the button orientation.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>PaletteButtonOrientation value.</returns>
    PaletteButtonOrientation GetButtonSpecOrientation(PaletteButtonSpecStyle style);
}