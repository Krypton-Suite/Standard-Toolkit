namespace Krypton.Toolkit;

/// <summary>
/// Specifies a text rendering hint.
/// </summary>
public enum PaletteTextHint
{
    /// <summary>
    /// Specifies text hint should be inherited.
    /// </summary>
    Inherit = -1,

    /// <summary>
    /// Specifies anti aliasing for text rendering.
    /// </summary>
    AntiAlias,

    /// <summary>
    /// Specifies anti aliasing with grid fit for text rendering.
    /// </summary>
    AntiAliasGridFit,

    /// <summary>
    /// Specifies clear type with grid fit for text rendering.
    /// </summary>
    ClearTypeGridFit,

    /// <summary>
    /// Specifies single bit per pixel for text rendering.
    /// </summary>
    SingleBitPerPixel,

    /// <summary>
    /// Specifies single bit for pixel with grid fit for text rendering.
    /// </summary>
    SingleBitPerPixelGridFit,

    /// <summary>
    /// Specifies system default setting for text rendering.
    /// </summary>
    SystemDefault
}