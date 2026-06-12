#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Creates <see cref="KryptonCustomPaletteBase"/> instances from builtin macOS palettes for export or app-specific tuning.
/// </summary>
public static class MacOSCustomPaletteHelper
{
    /// <summary>
    /// Gets the display name used when exporting a macOS builtin palette.
    /// </summary>
    public static string GetThemeDisplayName(PaletteMode mode) => mode switch
    {
        PaletteMode.MacOSLight => @"macOS - Light",
        PaletteMode.MacOSDark => @"macOS - Dark",
        _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, @"Expected MacOSLight or MacOSDark.")
    };

    /// <summary>
    /// Builds a custom palette populated from the specified builtin macOS palette and renderer.
    /// </summary>
    /// <param name="mode">Either <see cref="PaletteMode.MacOSLight"/> or <see cref="PaletteMode.MacOSDark"/>.</param>
    /// <returns>A populated <see cref="KryptonCustomPaletteBase"/> instance.</returns>
    public static KryptonCustomPaletteBase CreateCustomPalette(PaletteMode mode)
    {
        if (mode != PaletteMode.MacOSLight && mode != PaletteMode.MacOSDark)
        {
            throw new ArgumentOutOfRangeException(nameof(mode), mode, @"Expected MacOSLight or MacOSDark.");
        }

        var custom = new KryptonCustomPaletteBase
        {
            BasePalette = KryptonManager.GetPaletteForMode(mode),
            BaseRenderMode = RendererMode.MacOS
        };
        custom.PopulateFromBase(silent: true);
        custom.SetPaletteName(GetThemeDisplayName(mode));
        return custom;
    }

    /// <summary>
    /// Exports a builtin macOS palette to an XML file compatible with <see cref="KryptonCustomPaletteBase.Import(string, bool)"/>.
    /// </summary>
    /// <param name="mode">Either <see cref="PaletteMode.MacOSLight"/> or <see cref="PaletteMode.MacOSDark"/>.</param>
    /// <param name="filePath">Destination path (typically .xml).</param>
    /// <param name="ignoreDefaults">When true, omits properties that match base defaults.</param>
    public static void ExportToFile(PaletteMode mode, string filePath, bool ignoreDefaults = true)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException(@"A file path is required.", nameof(filePath));
        }

        var custom = CreateCustomPalette(mode);
        custom.Export(filePath, ignoreDefaults, silent: true);
    }
}