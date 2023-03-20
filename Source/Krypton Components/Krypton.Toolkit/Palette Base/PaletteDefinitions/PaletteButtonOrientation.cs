namespace Krypton.Toolkit;

/// <summary>
/// Specifies the orientation of a button specification.
/// </summary>
[TypeConverter(typeof(PaletteButtonOrientationConverter))]
public enum PaletteButtonOrientation
{
    /// <summary>
    /// Specifies orientation should be inherited.
    /// </summary>
    Inherit,

    /// <summary>
    /// Specifies orientation should automatically match the concept of use.
    /// </summary>
    Auto,

    /// <summary>
    /// Specifies the button is orientated in a vertical top down manner.
    /// </summary>
    FixedTop,

    /// <summary>
    /// Specifies the button is orientated in a vertical bottom upwards manner.
    /// </summary>
    FixedBottom,

    /// <summary>
    /// Specifies the button is orientated in a horizontal left to right manner.
    /// </summary>
    FixedLeft,

    /// <summary>
    /// Specifies the button is orientated in a horizontal right to left manner.
    /// </summary>
    FixedRight
}