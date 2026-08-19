#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Themes;

namespace TestForm;

/// <summary>
/// Selects a Lime Green builtin palette variant (Office / Microsoft 365 / Material structural bases).
/// </summary>
internal enum LimeGreenThemeFamily
{
    /// <summary>Office 2007 Blue chrome and gradients.</summary>
    Office2007,

    /// <summary>Office 2010 Blue chrome and gradients.</summary>
    Office2010,

    /// <summary>Microsoft 365 White chrome (flat modern).</summary>
    Microsoft365,

    /// <summary>Material Light chrome with lime filled buttons.</summary>
    Material,

    /// <summary>Material Light chrome with Ripple.</summary>
    MaterialRipple,

    /// <summary>Office 2007 Blue Dark Mode chrome.</summary>
    Office2007Dark,

    /// <summary>Office 2010 Blue Dark Mode chrome.</summary>
    Office2010Dark,

    /// <summary>Microsoft 365 Black Dark Mode chrome.</summary>
    Microsoft365Dark,

    /// <summary>Material Dark chrome with lime filled buttons.</summary>
    MaterialDark,

    /// <summary>Material Dark chrome with Ripple.</summary>
    MaterialDarkRipple
}

/// <summary>
/// Maps <see cref="LimeGreenThemeFamily"/> values onto the builtin Lime Green <see cref="PaletteMode"/> entries
/// (see <c>Palette Builtin/Lime Green</c> in Krypton.Toolkit) and provides small helpers for the TestForm demo
/// (apply / reset / export). The colour work itself lives entirely in the builtin palette classes
/// (<see cref="PaletteOffice2007LimeGreen"/>, <see cref="PaletteMaterialLimeGreen"/>
/// and siblings) via <c>LimeGreenSchemeHelper</c>.
/// </summary>
internal static class LimeGreenButtonThemeHelper
{
    /// <summary>
    /// Gets whether <paramref name="family"/> is a dark chrome variant.
    /// </summary>
    /// <param name="family">Lime theme family.</param>
    /// <returns><c>true</c> for dark families.</returns>
    public static bool IsDark(LimeGreenThemeFamily family) =>
        family == LimeGreenThemeFamily.Office2007Dark
        || family == LimeGreenThemeFamily.Office2010Dark
        || family == LimeGreenThemeFamily.Microsoft365Dark
        || family == LimeGreenThemeFamily.MaterialDark
        || family == LimeGreenThemeFamily.MaterialDarkRipple;

    /// <summary>
    /// Gets the builtin Lime Green <see cref="PaletteMode"/> for <paramref name="family"/>.
    /// </summary>
    /// <param name="family">Lime theme family.</param>
    /// <returns>Matching Lime Green palette mode.</returns>
    public static PaletteMode GetPaletteMode(LimeGreenThemeFamily family) => family switch
    {
        LimeGreenThemeFamily.Office2007 => PaletteMode.Office2007LimeGreen,
        LimeGreenThemeFamily.Office2010 => PaletteMode.Office2010LimeGreen,
        LimeGreenThemeFamily.Microsoft365 => PaletteMode.Microsoft365LimeGreen,
        LimeGreenThemeFamily.Material => PaletteMode.MaterialLimeGreen,
        LimeGreenThemeFamily.MaterialRipple => PaletteMode.MaterialLimeGreenRipple,
        LimeGreenThemeFamily.Office2007Dark => PaletteMode.Office2007LimeGreenDark,
        LimeGreenThemeFamily.Office2010Dark => PaletteMode.Office2010LimeGreenDark,
        LimeGreenThemeFamily.Microsoft365Dark => PaletteMode.Microsoft365LimeGreenDark,
        LimeGreenThemeFamily.MaterialDark => PaletteMode.MaterialLimeGreenDark,
        LimeGreenThemeFamily.MaterialDarkRipple => PaletteMode.MaterialLimeGreenDarkRipple,
        _ => throw new ArgumentOutOfRangeException(nameof(family), family, null)
    };

    /// <summary>
    /// Gets the structural base (non-lime) <see cref="PaletteMode"/> that <paramref name="family"/> is derived
    /// from, used by the demo's "Reset to base" action.
    /// </summary>
    /// <param name="family">Lime theme family.</param>
    /// <returns>Matching Office / Microsoft 365 / Material palette mode.</returns>
    public static PaletteMode GetBasePaletteMode(LimeGreenThemeFamily family) => family switch
    {
        LimeGreenThemeFamily.Office2007 => PaletteMode.Office2007Blue,
        LimeGreenThemeFamily.Office2010 => PaletteMode.Office2010Blue,
        LimeGreenThemeFamily.Microsoft365 => PaletteMode.Microsoft365White,
        LimeGreenThemeFamily.Material => PaletteMode.MaterialLight,
        LimeGreenThemeFamily.MaterialRipple => PaletteMode.MaterialLightRipple,
        LimeGreenThemeFamily.Office2007Dark => PaletteMode.Office2007BlueDarkMode,
        LimeGreenThemeFamily.Office2010Dark => PaletteMode.Office2010BlueDarkMode,
        LimeGreenThemeFamily.Microsoft365Dark => PaletteMode.Microsoft365BlackDarkMode,
        LimeGreenThemeFamily.MaterialDark => PaletteMode.MaterialDark,
        LimeGreenThemeFamily.MaterialDarkRipple => PaletteMode.MaterialDarkRipple,
        _ => throw new ArgumentOutOfRangeException(nameof(family), family, null)
    };

    /// <summary>
    /// Gets the display name for a lime theme family, as shown in theme selectors.
    /// </summary>
    /// <param name="family">Lime theme family.</param>
    /// <returns>Name registered against the family's <see cref="PaletteMode"/> in <c>PaletteModeStrings</c>.</returns>
    public static string GetPaletteName(LimeGreenThemeFamily family) =>
        ThemeManager.ReturnPaletteModeAsString(GetPaletteMode(family));

    /// <summary>
    /// Gets a suggested export file name for <paramref name="family"/>.
    /// </summary>
    /// <param name="family">Lime theme family.</param>
    /// <returns>File name including <c>.xml</c>.</returns>
    public static string GetExportFileName(LimeGreenThemeFamily family) => family switch
    {
        LimeGreenThemeFamily.Office2007 => @"LimeGreen-Office2007.xml",
        LimeGreenThemeFamily.Office2010 => @"LimeGreen-Office2010.xml",
        LimeGreenThemeFamily.Microsoft365 => @"LimeGreen-Microsoft365.xml",
        LimeGreenThemeFamily.Material => @"LimeGreen-Material.xml",
        LimeGreenThemeFamily.MaterialRipple => @"LimeGreen-Material-Ripple.xml",
        LimeGreenThemeFamily.Office2007Dark => @"LimeGreen-Office2007-Dark.xml",
        LimeGreenThemeFamily.Office2010Dark => @"LimeGreen-Office2010-Dark.xml",
        LimeGreenThemeFamily.Microsoft365Dark => @"LimeGreen-Microsoft365-Dark.xml",
        LimeGreenThemeFamily.MaterialDark => @"LimeGreen-Material-Dark.xml",
        LimeGreenThemeFamily.MaterialDarkRipple => @"LimeGreen-Material-Dark-Ripple.xml",
        _ => throw new ArgumentOutOfRangeException(nameof(family), family, null)
    };

    /// <summary>
    /// Builds a <see cref="KryptonCustomPaletteBase"/> populated from the builtin Lime Green palette for
    /// <paramref name="family"/>, for use with <see cref="KryptonCustomPaletteBase.Export(string, bool, bool)"/>.
    /// </summary>
    /// <param name="family">Lime theme family.</param>
    /// <returns>A populated <see cref="KryptonCustomPaletteBase"/> instance, ready to export.</returns>
    public static KryptonCustomPaletteBase CreateExportPalette(LimeGreenThemeFamily family)
    {
        var custom = new KryptonCustomPaletteBase
        {
            BasePaletteMode = GetPaletteMode(family)
        };
        custom.PopulateFromBase(silent: true);
        custom.SetPaletteName(GetPaletteName(family));
        return custom;
    }
}
