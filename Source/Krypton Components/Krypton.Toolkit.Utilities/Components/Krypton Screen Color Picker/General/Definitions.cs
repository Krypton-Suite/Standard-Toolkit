#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

#region Enum KryptonScreenColorPickerColorFormat

/// <summary>
/// Colour representations that can be shown on the screen colour picker flyout.
/// Combine flags to show more than one format. Use
/// <see cref="KryptonScreenColorPicker.VisibleColorFormats"/> to choose which are visible.
/// </summary>
[Flags]
public enum KryptonScreenColorPickerColorFormat
{
    /// <summary>
    /// No colour formats.
    /// </summary>
    None = 0,

    /// <summary>
    /// Hexadecimal RGB, for example <c>#E5F1FA</c>.
    /// </summary>
    Hex = 1,

    /// <summary>
    /// Hexadecimal ARGB, for example <c>#FFE5F1FA</c>.
    /// </summary>
    HexAlpha = 2,

    /// <summary>
    /// Hexadecimal integer, for example <c>0xE5F1FA</c>.
    /// </summary>
    HexInteger = 4,

    /// <summary>
    /// RGB triplet, for example <c>RGB(229, 241, 250)</c>.
    /// </summary>
    Rgb = 8,

    /// <summary>
    /// RGBA quadruplet, for example <c>RGBA(229, 241, 250, 255)</c>.
    /// </summary>
    Rgba = 16,

    /// <summary>
    /// HSL triplet, for example <c>HSL(204, 60%, 94%)</c>.
    /// </summary>
    Hsl = 32,

    /// <summary>
    /// HSV triplet, for example <c>HSV(204, 8%, 98%)</c>.
    /// </summary>
    Hsv = 64,

    /// <summary>
    /// CMYK percentages, for example <c>CMYK(8%, 4%, 0%, 2%)</c>.
    /// </summary>
    Cmyk = 128,

    /// <summary>
    /// Win32 <c>COLORREF</c> decimal value.
    /// </summary>
    Decimal = 256,

    /// <summary>
    /// Unit RGB vector in 0–1, for example <c>0.898, 0.945, 0.980</c>.
    /// </summary>
    Vector = 512,

    /// <summary>
    /// Nearest web <see cref="KnownColor"/> name, for example <c>AliceBlue</c>.
    /// </summary>
    KnownName = 1024
}

#endregion

#region Enum KryptonScreenColorPickerFlyoutStyle

/// <summary>
/// Chrome used for the magnifier flyout that follows the cursor while picking.
/// </summary>
public enum KryptonScreenColorPickerFlyoutStyle
{
    /// <summary>
    /// Painted PowerToys-style dark flyout (independent of the current Krypton palette).
    /// </summary>
    Classic = 0,

    /// <summary>
    /// Themed <see cref="KryptonHeaderGroup"/> flyout that follows the current (or local custom) palette.
    /// </summary>
    Krypton = 1
}

#endregion