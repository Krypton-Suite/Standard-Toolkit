#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Themes;

/// <summary>Windows XP Zune palette.</summary>
public sealed class PaletteWindowsXPZune : PaletteWindowsXPRoyaleBase, IWindowsXPSpriteTheme
{
    /// <summary>Initialize a new instance of the <see cref="PaletteWindowsXPZune"/> class.</summary>
    public PaletteWindowsXPZune() => ThemeName = nameof(PaletteWindowsXPZune);

    /// <inheritdoc />
    public override IRenderer GetRenderer() => KryptonManager.RenderWindowsXPZune;

    /// <inheritdoc />
    protected override RoyalePalette Royale => WindowsXPZuneColors.Palette;

    /// <inheritdoc />
    WindowsXPSpriteTheme IWindowsXPSpriteTheme.Theme => WindowsXPSpriteTheme.Zune;
}
