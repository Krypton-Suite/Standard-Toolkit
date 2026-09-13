#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Creates <see cref="KryptonCustomPaletteBase"/> instances from builtin Windows XP visual style palettes for export or app-specific tuning.
/// </summary>
public static class WindowsXPVisualStyleCustomPaletteHelper
{
    /// <summary>
    /// Returns whether <paramref name="mode"/> is a built-in Windows XP visual style palette.
    /// </summary>
    public static bool IsWindowsXPVisualStyle(PaletteMode mode) => mode switch
    {
        PaletteMode.WindowsXPLunaBlue or PaletteMode.WindowsXPLunaOlive or PaletteMode.WindowsXPLunaSilver
            or PaletteMode.WindowsXPRoyale or PaletteMode.WindowsXPRoyaleNoir or PaletteMode.WindowsXPZune => true,
        _ => false
    };

    /// <summary>
    /// Gets the display name used when exporting a builtin Windows XP visual style palette.
    /// </summary>
    public static string GetThemeDisplayName(PaletteMode mode) => mode switch
    {
        PaletteMode.WindowsXPLunaBlue => @"Windows XP - Luna Blue",
        PaletteMode.WindowsXPLunaOlive => @"Windows XP - Luna Olive",
        PaletteMode.WindowsXPLunaSilver => @"Windows XP - Luna Silver",
        PaletteMode.WindowsXPRoyale => @"Windows XP - Royale",
        PaletteMode.WindowsXPRoyaleNoir => @"Windows XP - Royale Noir",
        PaletteMode.WindowsXPZune => @"Windows XP - Zune",
        _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, @"Expected a Windows XP visual style PaletteMode.")
    };

    /// <summary>
    /// Builds a custom palette populated from the specified builtin Windows XP visual style palette and renderer.
    /// </summary>
    /// <param name="mode">A built-in Windows XP visual style <see cref="PaletteMode"/> value.</param>
    /// <returns>A populated <see cref="KryptonCustomPaletteBase"/> instance.</returns>
    public static KryptonCustomPaletteBase CreateCustomPalette(PaletteMode mode)
    {
        if (!IsWindowsXPVisualStyle(mode))
        {
            throw new ArgumentOutOfRangeException(nameof(mode), mode, @"Expected a Windows XP visual style PaletteMode.");
        }

        var custom = new KryptonCustomPaletteBase
        {
            BasePalette = KryptonManager.GetPaletteForMode(mode),
            BaseRenderMode = RendererMode.WindowsXPLuna
        };
        custom.PopulateFromBase(silent: true);
        custom.SetPaletteName(GetThemeDisplayName(mode));
        return custom;
    }

    /// <summary>
    /// Exports a builtin Windows XP visual style palette to an XML file compatible with <see cref="KryptonCustomPaletteBase.Import(string, bool)"/>.
    /// </summary>
    /// <param name="mode">A built-in Windows XP visual style <see cref="PaletteMode"/> value.</param>
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
