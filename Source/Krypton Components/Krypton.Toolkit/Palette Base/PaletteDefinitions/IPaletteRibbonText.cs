namespace Krypton.Toolkit;

/// <summary>
/// Exposes a palette source for ribbon text specifications.
/// </summary>
public interface IPaletteRibbonText
{
    /// <summary>
    /// Gets the tab color for the item text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    Color GetRibbonTextColor(PaletteState state);
}