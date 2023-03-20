namespace Krypton.Toolkit;

/// <summary>
/// Specifies how drag feedback is presented.
/// </summary>
public enum PaletteDragFeedback
{
    /// <summary>
    /// Draw drag drop feedback as just blocks that are highlighted based on hot areas. 
    /// </summary>
    Block,

    /// <summary>
    /// Draw drag drop feedback as square indicators.
    /// </summary>
    Square,

    /// <summary>
    /// Draw drag drop feedback as rounded indicators.
    /// </summary>
    Rounded,

    /// <summary>
    /// Draw drag drop feedback using the inherited value.
    /// </summary>
    Inherit
}