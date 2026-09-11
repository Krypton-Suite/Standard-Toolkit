#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Themes;

/// <summary>
/// Builds and exports <see cref="KryptonCustomPaletteBase"/> instances from extra builtin palettes
/// shipped in <c>Krypton.Themes</c> (and from core catalog modes when those are requested).
/// </summary>
/// <remarks>
/// Persistence itself lives in <c>Krypton.Toolkit</c> because Toolkit cannot project-reference this
/// assembly. This helper is the Themes-side consumer of
/// <see cref="KryptonCustomPaletteBase.Export(string, bool, bool, KryptonPaletteFileFormat)"/>.
/// </remarks>
public static class KryptonThemeCustomPaletteHelper
{
    /// <summary>
    /// Creates a custom palette populated from the builtin implementation for <paramref name="mode"/>.
    /// </summary>
    /// <param name="mode">A catalogued <see cref="PaletteMode"/> (not <see cref="PaletteMode.Global"/> or <see cref="PaletteMode.Custom"/>).</param>
    /// <returns>A populated <see cref="KryptonCustomPaletteBase"/> ready to export or assign as a global custom palette.</returns>
    public static KryptonCustomPaletteBase CreateCustomPalette(PaletteMode mode)
    {
        if (mode == PaletteMode.Global || mode == PaletteMode.Custom)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException(nameof(mode), mode, @"Expected a catalogued builtin palette mode.");
        }

        if (mode == PaletteMode.MacOSLight || mode == PaletteMode.MacOSDark)
        {
            return MacOSCustomPaletteHelper.CreateCustomPalette(mode);
        }

        var custom = new KryptonCustomPaletteBase
        {
            BasePaletteMode = mode
        };
        custom.PopulateFromBase(silent: true);
        custom.SetPaletteName(KryptonThemeCatalog.GetDisplayName(mode));
        return custom;
    }

    /// <summary>
    /// Exports a builtin palette to a file. <c>.kthemex</c> and <c>.xml</c> write XML; <c>.ktheme</c> writes the optional native persist stream.
    /// </summary>
    /// <param name="mode">Catalogued builtin palette mode.</param>
    /// <param name="filePath">Destination path. Cannot be empty.</param>
    /// <param name="ignoreDefaults">When <see langword="true"/>, omits properties that match base defaults.</param>
    // ToDo V120 LTS: Stop documenting .xml destinations; ExportToFile should prefer .kthemex.
    public static void ExportToFile(PaletteMode mode, string filePath, bool ignoreDefaults = true)
    {
        ValidatePath(filePath);
        var custom = CreateCustomPalette(mode);
        custom.Export(filePath, ignoreDefaults, silent: true);
    }

    /// <summary>
    /// Exports a builtin palette using an explicit persist format.
    /// </summary>
    /// <param name="mode">Catalogued builtin palette mode.</param>
    /// <param name="filePath">Destination path. Cannot be empty.</param>
    /// <param name="format">XML, compressed-XML container, or native binary container.</param>
    /// <param name="ignoreDefaults">When <see langword="true"/>, omits properties that match base defaults.</param>
    public static void ExportToFile(PaletteMode mode, string filePath, KryptonPaletteFileFormat format, bool ignoreDefaults = true)
    {
        ValidatePath(filePath);
        var custom = CreateCustomPalette(mode);
        custom.Export(filePath, ignoreDefaults, silent: true, format);
    }

    private static void ValidatePath(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            ThrowHelper.ThrowArgumentException(@"A file path is required.", nameof(filePath));
        }
    }
}
