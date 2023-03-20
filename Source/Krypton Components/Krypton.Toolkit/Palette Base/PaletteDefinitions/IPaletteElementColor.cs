namespace Krypton.Toolkit;

/// <summary>
/// Exposes a palette source for element colors.
/// </summary>
public interface IPaletteElementColor
{
    /// <summary>
    /// Gets the first color for the element.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    Color GetElementColor1(PaletteState state);

    /// <summary>
    /// Gets the second color for the element.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    Color GetElementColor2(PaletteState state);

    /// <summary>
    /// Gets the third color for the element.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    Color GetElementColor3(PaletteState state);

    /// <summary>
    /// Gets the fourth color for the element.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    Color GetElementColor4(PaletteState state);

    /// <summary>
    /// Gets the fifth color for the element.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    Color GetElementColor5(PaletteState state);
}