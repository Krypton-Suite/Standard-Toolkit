namespace Krypton.Toolkit;

/// <summary>
/// Specifies how an image is drawn.
/// </summary>
[TypeConverter(typeof(PaletteImageEffectConverter))]
public enum PaletteImageEffect
{
    /// <summary>
    /// Specifies effect should be inherited.
    /// </summary>
    Inherit,

    /// <summary>
    /// Specifies image is drawn without modification.
    /// </summary>
    Normal,

    /// <summary>
    /// Specifies image is drawn to look disabled.
    /// </summary>
    Disabled,

    /// <summary>
    /// Specifies image is drawn converted to a grayscale.
    /// </summary>
    GrayScale,

    /// <summary>
    /// Specifies image is drawn converted to a grayscale except for red.
    /// </summary>
    GrayScaleRed,

    /// <summary>
    /// Specifies image is drawn converted to a grayscale except for green.
    /// </summary>
    GrayScaleGreen,

    /// <summary>
    /// Specifies image is drawn converted to a grayscale except for blue.
    /// </summary>
    GrayScaleBlue,

    /// <summary>
    /// Specifies image is drawn slightly lighter.
    /// </summary>
    Light,

    /// <summary>
    /// Specifies image is drawn much lighter.
    /// </summary>
    LightLight,

    /// <summary>
    /// Specifies image is drawn slightly darker.
    /// </summary>
    Dark,

    /// <summary>
    /// Specifies image is drawn much darker.
    /// </summary>
    DarkDark
}