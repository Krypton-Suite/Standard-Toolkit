#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Themes;

/// <summary>Windows XP Embedded palette.</summary>
public sealed class PaletteWindowsXPEmbedded : PaletteWindowsXPLunaBase
{
    /// <summary>Initialize a new instance of the <see cref="PaletteWindowsXPEmbedded"/> class.</summary>
    public PaletteWindowsXPEmbedded() => ThemeName = nameof(PaletteWindowsXPEmbedded);

    /// <inheritdoc />
    public override IRenderer GetRenderer() => KryptonManager.RenderWindowsXPEmbedded;

    /// <inheritdoc />
    protected override LunaPalette Luna => EmbeddedColors.Palette;
}
