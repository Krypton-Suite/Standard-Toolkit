#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Shared static images and glyph lists for Retro palette variants.
/// </summary>
internal static class RetroPaletteSharedAssets
{
    internal static readonly ImageList CheckBoxList;

    internal static readonly ImageList GalleryButtonList;

    internal static readonly Image?[] RadioButtonArray;

    internal static readonly Image? ContextMenuSubMenu;

    internal static readonly Image? ContextMenuChecked;

    internal static readonly Image? ContextMenuIndeterminate;

    internal static readonly Image FormCloseNormal = Office2010ControlBoxResources.Office2010BlueCloseNormal;

    internal static readonly Image FormCloseDisabled = Office2010ControlBoxResources.Office2010BlueCloseDisabled;

    internal static readonly Image FormCloseActive = Office2010ControlBoxResources.Office2010BlueCloseActive;

    internal static readonly Image FormClosePressed = Office2010ControlBoxResources.Office2010BlueClosePressed;

    internal static readonly Image FormMaximiseNormal = Office2010ControlBoxResources.Office2010BlueMaximiseNormal;

    internal static readonly Image FormMaximiseDisabled = Office2010ControlBoxResources.Office2010BlueMaximiseDisabled;

    internal static readonly Image FormMaximiseActive = Office2010ControlBoxResources.Office2010BlueMaximiseActive;

    internal static readonly Image FormMaximisePressed = Office2010ControlBoxResources.Office2010BlueMaximisePressed;

    internal static readonly Image FormMinimiseNormal = Office2010ControlBoxResources.Office2010BlueMinimiseNormal;

    internal static readonly Image FormMinimiseActive = Office2010ControlBoxResources.Office2010BlueMinimiseActive;

    internal static readonly Image FormMinimiseDisabled = Office2010ControlBoxResources.Office2010BlueMinimiseDisabled;

    internal static readonly Image FormMinimisePressed = Office2010ControlBoxResources.Office2010BlueMinimisePressed;

    internal static readonly Image FormRestoreNormal = Office2010ControlBoxResources.Office2010BlueRestoreNormal;

    internal static readonly Image FormRestoreDisabled = Office2010ControlBoxResources.Office2010BlueRestoreDisabled;

    internal static readonly Image FormRestoreActive = Office2010ControlBoxResources.Office2010BlueRestoreActive;

    internal static readonly Image FormRestorePressed = Office2010ControlBoxResources.Office2010BlueRestorePressed;

    internal static readonly Image FormHelpNormal = Office2010ControlBoxResources.Office2010HelpIconNormal;

    internal static readonly Image FormHelpActive = Office2010ControlBoxResources.Office2010HelpIconHover;

    internal static readonly Image FormHelpPressed = Office2010ControlBoxResources.Office2010HelpIconPressed;

    internal static readonly Image FormHelpDisabled = Office2010ControlBoxResources.Office2010HelpIconDisabled;

    static RetroPaletteSharedAssets()
    {
        CheckBoxList = new ImageList
        {
            ImageSize = new Size(13, 13),
            ColorDepth = ColorDepth.Depth32Bit
        };

        Image[] cbStrip = RetroSelectionGlyphFactory.CreateCheckBoxStrip(CheckBoxList.ImageSize);
        for (int i = 0; i < cbStrip.Length; i++)
        {
            CheckBoxList.Images.Add(cbStrip[i]);
        }

        GalleryButtonList = new ImageList
        {
            ImageSize = new Size(13, 7),
            ColorDepth = ColorDepth.Depth24Bit,
            TransparentColor = GlobalStaticVariables.TRANSPARENCY_KEY_COLOR
        };
        GalleryButtonList.Images.AddStrip(GalleryImageResources.Gallery2010);

        RadioButtonArray = RetroSelectionGlyphFactory.CreateRadioButtonArray(new Size(13, 13));

        ContextMenuChecked = RetroSelectionGlyphFactory.CreateMenuCheckedGlyph(new Size(16, 16), true);
        ContextMenuIndeterminate = RetroSelectionGlyphFactory.CreateMenuIndeterminateGlyph(new Size(16, 16), true);
        ContextMenuSubMenu = RetroSelectionGlyphFactory.CreateMenuSubMenuArrow(new Size(16, 16));
    }
}
