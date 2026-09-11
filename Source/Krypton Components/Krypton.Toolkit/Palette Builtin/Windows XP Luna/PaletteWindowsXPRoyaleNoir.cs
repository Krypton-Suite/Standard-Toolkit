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
/// Windows XP Royale Noir palette.
/// </summary>
public class PaletteWindowsXPRoyaleNoir : PaletteWindowsXPLunaBase
{
    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteWindowsXPRoyaleNoir"/> class.
    /// </summary>
    public PaletteWindowsXPRoyaleNoir()
        : base(
            @"Windows XP - Royale Noir",
            new PaletteWindowsXPRoyaleNoir_BaseScheme(),
            WindowsXPLunaPaletteSharedAssets.BlackCheckBoxList,
            WindowsXPLunaPaletteSharedAssets.BlackGalleryButtonList,
            WindowsXPLunaPaletteSharedAssets.BlackRadioButtonArray,
            WindowsXPLunaPaletteSharedAssets.BlackContextMenuSubMenu,
            SizeGripStyleResources.Office2007BlackGripStyle,
            WindowsXPLunaPaletteSharedAssets.GetBlackFormButtonImage)
    {
        ThemeName = nameof(PaletteWindowsXPRoyaleNoir);
    }
}
