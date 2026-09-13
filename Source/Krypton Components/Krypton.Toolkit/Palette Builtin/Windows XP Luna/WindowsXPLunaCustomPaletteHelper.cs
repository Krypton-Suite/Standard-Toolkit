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
/// Creates <see cref="KryptonCustomPaletteBase"/> instances from builtin Windows XP Luna palettes for export or app-specific tuning.
/// </summary>
[Obsolete(@"Use WindowsXPVisualStyleCustomPaletteHelper for Luna, Royale, and Zune palettes.")]
public static class WindowsXPLunaCustomPaletteHelper
{
    /// <inheritdoc cref="WindowsXPVisualStyleCustomPaletteHelper.GetThemeDisplayName"/>
    public static string GetThemeDisplayName(PaletteMode mode) =>
        WindowsXPVisualStyleCustomPaletteHelper.GetThemeDisplayName(mode);

    /// <inheritdoc cref="WindowsXPVisualStyleCustomPaletteHelper.CreateCustomPalette"/>
    public static KryptonCustomPaletteBase CreateCustomPalette(PaletteMode mode) =>
        WindowsXPVisualStyleCustomPaletteHelper.CreateCustomPalette(mode);

    /// <inheritdoc cref="WindowsXPVisualStyleCustomPaletteHelper.ExportToFile"/>
    public static void ExportToFile(PaletteMode mode, string filePath, bool ignoreDefaults = true) =>
        WindowsXPVisualStyleCustomPaletteHelper.ExportToFile(mode, filePath, ignoreDefaults);
}
