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
/// Windows XP Luna silver (Metallic) palette.
/// </summary>
public class PaletteWindowsXPLunaSilver : PaletteWindowsXPLunaBase
{
    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteWindowsXPLunaSilver"/> class.
    /// </summary>
    public PaletteWindowsXPLunaSilver()
        : base(
            @"Windows XP - Luna Silver",
            new PaletteWindowsXPLunaSilver_BaseScheme(),
            WindowsXPLunaPaletteSharedAssets.SilverCheckBoxList,
            WindowsXPLunaPaletteSharedAssets.SilverGalleryButtonList,
            WindowsXPLunaPaletteSharedAssets.SilverRadioButtonArray,
            WindowsXPLunaPaletteSharedAssets.SilverContextMenuSubMenu,
            SizeGripStyleResources.Office2007SilverGripStyle,
            WindowsXPLunaPaletteSharedAssets.GetSilverFormButtonImage)
    {
        ThemeName = nameof(PaletteWindowsXPLunaSilver);
    }
}
