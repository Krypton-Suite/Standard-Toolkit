namespace Krypton.Toolkit;

/// <summary>
/// Access to the back and border palettes plus metrics.
/// </summary>
public interface IPaletteSeparator : IPaletteDouble
{
    /// <summary>
    /// Gets the palette source for acquiring metrics.
    /// </summary>
    IPaletteMetric PaletteMetric { get; }
}