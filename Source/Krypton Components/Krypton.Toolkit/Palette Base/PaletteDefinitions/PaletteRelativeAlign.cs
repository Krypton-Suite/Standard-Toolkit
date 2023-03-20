namespace Krypton.Toolkit;

/// <summary>
/// Specifies a relative alignment position.
/// </summary>
public enum PaletteRelativeAlign
{
    /// <summary>
    /// Specifies relative alignment should be inherited.
    /// </summary>
    Inherit = -1,

    /// <summary>
    /// Specifies a relative alignment of near.
    /// </summary>
    Near = 0,

    /// <summary>
    /// Specifies a relative alignment of center.
    /// </summary>
    Center = 1,

    /// <summary>
    /// Specifies a relative alignment of far.
    /// </summary>
    Far = 2
}