namespace Krypton.Toolkit;

/// <summary>
/// Access to the double of back and border palettes.
/// </summary>
public interface IPaletteDouble
{
    /// <summary>
    /// Gets the background palette.
    /// </summary>
    IPaletteBack PaletteBack { get; }

    /// <summary>
    /// Gets the border palette.
    /// </summary>
    IPaletteBorder? PaletteBorder { get; }
}