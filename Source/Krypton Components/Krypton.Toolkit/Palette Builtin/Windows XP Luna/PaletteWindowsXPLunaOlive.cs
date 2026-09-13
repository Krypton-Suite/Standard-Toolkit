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
/// Windows XP Luna olive (HomeStead) palette.
/// </summary>
public class PaletteWindowsXPLunaOlive : PaletteWindowsXPLunaBase
{
    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteWindowsXPLunaOlive"/> class.
    /// </summary>
    public PaletteWindowsXPLunaOlive()
        : base(
            @"Windows XP - Luna Olive",
            new PaletteWindowsXPLunaOlive_BaseScheme(),
            WindowsXPLunaPaletteSharedAssets.BlueCheckBoxList,
            WindowsXPLunaPaletteSharedAssets.BlueGalleryButtonList,
            WindowsXPLunaPaletteSharedAssets.BlueRadioButtonArray,
            WindowsXPLunaPaletteSharedAssets.BlueContextMenuSubMenu,
            SizeGripStyleResources.Office2007BlueGripStyle,
            WindowsXPLunaPaletteSharedAssets.GetOliveFormButtonImage)
    {
        ThemeName = nameof(PaletteWindowsXPLunaOlive);
    }
}
