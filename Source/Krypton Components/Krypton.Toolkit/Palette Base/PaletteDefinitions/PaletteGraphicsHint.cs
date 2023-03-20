namespace Krypton.Toolkit;

/// <summary>
/// Specifies a graphics rendering hint.
/// </summary>
public enum PaletteGraphicsHint
{
    /// <summary>
    /// Specifies graphics hint should be inherited.
    /// </summary>
    Inherit = -1,

    /// <summary>
    /// Specifies no smoothing for graphics rendering.
    /// </summary>
    None,

    /// <summary>
    /// Specifies anti aliasing for graphics rendering.
    /// </summary>
    AntiAlias,

    /// <summary>Specifies no antialiasing.</summary>
    HighSpeed,
    /// <summary>Specifies antialiased rendering.</summary>
    HighQuality

}