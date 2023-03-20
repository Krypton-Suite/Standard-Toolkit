namespace Krypton.Toolkit;

/// <summary>
/// Access to the triple of back, border and content palettes.
/// </summary>
public interface IPaletteTriple
{
    /// <summary>
    /// Gets the background palette.
    /// </summary>
    IPaletteBack PaletteBack { get; }

    /// <summary>
    /// Gets the border palette.
    /// </summary>
    IPaletteBorder? PaletteBorder { get; }

    /// <summary>
    /// Gets the content palette.
    /// </summary>
    IPaletteContent? PaletteContent { get; }
}