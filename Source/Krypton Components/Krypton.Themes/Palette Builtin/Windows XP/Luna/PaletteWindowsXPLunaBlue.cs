#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Themes;

/// <summary>Windows XP Luna Blue (NormalColor) palette.</summary>
public sealed class PaletteWindowsXPLunaBlue : PaletteWindowsXPLunaBase, IWindowsXPLunaScheme
{
    /// <summary>Initialize a new instance of the <see cref="PaletteWindowsXPLunaBlue"/> class.</summary>
    public PaletteWindowsXPLunaBlue() => ThemeName = nameof(PaletteWindowsXPLunaBlue);

    /// <inheritdoc />
    protected override LunaPalette Luna => LunaColors.Blue;

    /// <inheritdoc />
    WindowsXPLunaScheme IWindowsXPLunaScheme.Scheme => WindowsXPLunaScheme.Blue;
}
