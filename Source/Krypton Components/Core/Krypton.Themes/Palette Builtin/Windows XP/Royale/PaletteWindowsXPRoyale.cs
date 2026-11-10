#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Themes;

/// <summary>Windows XP Royale (Energy Blue) palette.</summary>
public sealed class PaletteWindowsXPRoyale : PaletteWindowsXPRoyaleBase, IWindowsXPSpriteTheme
{
    /// <summary>Initialize a new instance of the <see cref="PaletteWindowsXPRoyale"/> class.</summary>
    public PaletteWindowsXPRoyale() => ThemeName = nameof(PaletteWindowsXPRoyale);

    /// <inheritdoc />
    protected override RoyalePalette Royale => RoyaleColors.EnergyBlue;

    /// <inheritdoc />
    WindowsXPSpriteTheme IWindowsXPSpriteTheme.Theme => WindowsXPSpriteTheme.Royale;
}
