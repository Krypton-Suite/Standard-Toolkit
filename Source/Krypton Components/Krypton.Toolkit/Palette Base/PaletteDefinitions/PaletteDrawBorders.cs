namespace Krypton.Toolkit;

/// <summary>
/// Specifies the an image is aligned.
/// </summary>
[Flags()]
[TypeConverter(typeof(PaletteDrawBordersConverter))]
public enum PaletteDrawBorders
{
    /// <summary>
    /// Specifies borders to draw should be inherited.
    /// </summary>
    Inherit = 0x10,

    /// <summary>
    /// Specifies that no borders are required.
    /// </summary>
    None = 0x00,

    /// <summary>
    /// Specifies the top border should be drawn.
    /// </summary>
    Top = 0x01,

    /// <summary>
    /// Specifies the bottom border should be drawn.
    /// </summary>
    Bottom = 0x02,

    /// <summary>
    /// Specifies the top and bottom border.
    /// </summary>
    TopBottom = 0x03,

    /// <summary>
    /// Specifies the left border should be drawn.
    /// </summary>
    Left = 0x04,

    /// <summary>
    /// Specifies the top and bottom border.
    /// </summary>
    TopLeft = 0x05,

    /// <summary>
    /// Specifies the left and bottom borders.
    /// </summary>
    BottomLeft = 0x06,

    /// <summary>
    /// Specifies the bottom and right borders.
    /// </summary>
    TopBottomLeft = 0x07,

    /// <summary>
    /// Specifies the right border should be drawn.
    /// </summary>
    Right = 0x08,

    /// <summary>
    /// Specifies the top and bottom border.
    /// </summary>
    TopRight = 0x09,

    /// <summary>
    /// Specifies the bottom and right borders.
    /// </summary>
    BottomRight = 0x0A,

    /// <summary>
    /// Specifies the bottom and right borders.
    /// </summary>
    TopBottomRight = 0x0B,

    /// <summary>
    /// Specifies the left and right borders.
    /// </summary>
    LeftRight = 0x0C,

    /// <summary>
    /// Specifies the bottom and right borders.
    /// </summary>
    TopLeftRight = 0x0D,

    /// <summary>
    /// Specifies the bottom and right borders.
    /// </summary>
    BottomLeftRight = 0x0E,

    /// <summary>
    /// Specifies that all borders be drawn.
    /// </summary>
    All = 0x0F
}