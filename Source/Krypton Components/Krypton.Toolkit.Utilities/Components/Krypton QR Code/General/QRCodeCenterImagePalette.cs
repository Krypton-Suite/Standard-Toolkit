#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Resolved palette image drawing settings for a QR code center image.
/// </summary>
public readonly struct QRCodeCenterImagePalette
{
    /// <summary>Initializes a new instance with palette image drawing disabled.</summary>
    public QRCodeCenterImagePalette(bool usePaletteColors)
    {
        UsePaletteColors = usePaletteColors;
        Effect = PaletteImageEffect.Normal;
        ColorMap = GlobalStaticVariables.EMPTY_COLOR;
        ColorTo = GlobalStaticVariables.EMPTY_COLOR;
        TransparentColor = GlobalStaticVariables.EMPTY_COLOR;
    }

    /// <summary>Initializes a new instance with resolved palette image values.</summary>
    public QRCodeCenterImagePalette(
        bool usePaletteColors,
        PaletteImageEffect effect,
        Color colorMap,
        Color colorTo,
        Color transparentColor)
    {
        UsePaletteColors = usePaletteColors;
        Effect = effect;
        ColorMap = colorMap;
        ColorTo = colorTo;
        TransparentColor = transparentColor;
    }

    /// <summary>Gets whether palette image drawing is active.</summary>
    public bool UsePaletteColors { get; }

    /// <summary>Gets the image effect to apply.</summary>
    public PaletteImageEffect Effect { get; }

    /// <summary>Gets the source color to remap.</summary>
    public Color ColorMap { get; }

    /// <summary>Gets the destination color for remapping.</summary>
    public Color ColorTo { get; }

    /// <summary>Gets the color that should be drawn transparent.</summary>
    public Color TransparentColor { get; }
}
