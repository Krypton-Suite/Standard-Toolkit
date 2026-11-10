#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Themes;

/// <summary>Windows XP Royale Noir palette.</summary>
public sealed class PaletteWindowsXPRoyaleNoir : PaletteWindowsXPRoyaleBase, IWindowsXPSpriteTheme
{
    /// <summary>Initialize a new instance of the <see cref="PaletteWindowsXPRoyaleNoir"/> class.</summary>
    public PaletteWindowsXPRoyaleNoir() => ThemeName = nameof(PaletteWindowsXPRoyaleNoir);

    /// <inheritdoc />
    public override IRenderer GetRenderer() => KryptonManager.RenderWindowsXPRoyaleNoir;

    /// <inheritdoc />
    protected override RoyalePalette Royale => RoyaleNoirColors.Palette;

    /// <inheritdoc />
    WindowsXPSpriteTheme IWindowsXPSpriteTheme.Theme => WindowsXPSpriteTheme.RoyaleNoir;
}
