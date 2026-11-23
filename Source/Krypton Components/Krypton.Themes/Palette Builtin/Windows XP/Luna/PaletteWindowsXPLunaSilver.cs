#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Themes;

/// <summary>Windows XP Luna Silver (Metallic) palette.</summary>
public sealed class PaletteWindowsXPLunaSilver : PaletteWindowsXPLunaBase, IWindowsXPLunaScheme
{
    /// <summary>Initialize a new instance of the <see cref="PaletteWindowsXPLunaSilver"/> class.</summary>
    public PaletteWindowsXPLunaSilver() => ThemeName = nameof(PaletteWindowsXPLunaSilver);

    /// <inheritdoc />
    protected override LunaPalette Luna => LunaColors.Silver;

    /// <inheritdoc />
    WindowsXPLunaScheme IWindowsXPLunaScheme.Scheme => WindowsXPLunaScheme.Silver;
}
