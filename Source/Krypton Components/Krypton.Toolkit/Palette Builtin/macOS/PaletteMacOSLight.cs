#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// macOS-inspired light palette with traffic-light window controls and flat rounded rendering.
/// </summary>
public class PaletteMacOSLight : PaletteMacOSBase
{
    private static readonly ImageList _checkBoxList;
    private static readonly ImageList _galleryButtonList;
    private static readonly Image?[] _radioButtonArray;

    static PaletteMacOSLight()
    {
        _checkBoxList = new ImageList
        {
            ImageSize = new Size(13, 13),
            ColorDepth = ColorDepth.Depth32Bit
        };

        var scheme = new PaletteMacOSLight_BaseScheme();
        var lightPalette = MaterialSelectionGlyphFactory.FromScheme(scheme, isDarkSurface: false);

        var cbStrip = MaterialSelectionGlyphFactory.CreateCheckBoxStrip(lightPalette, _checkBoxList.ImageSize);
        for (int i = 0; i < cbStrip.Length; i++)
        {
            _checkBoxList.Images.Add(cbStrip[i]);
        }

        _galleryButtonList = new ImageList
        {
            ImageSize = new Size(13, 7),
            ColorDepth = ColorDepth.Depth24Bit,
            TransparentColor = GlobalStaticVariables.TRANSPARENCY_KEY_COLOR
        };
        _galleryButtonList.Images.AddStrip(GalleryImageResources.Gallery2010);

        _radioButtonArray = MaterialSelectionGlyphFactory.CreateRadioButtonArray(lightPalette, new Size(13, 13));

        _contextMenuChecked = MaterialSelectionGlyphFactory.CreateMenuCheckedGlyph(lightPalette, new Size(16, 16), true);
        _contextMenuIndeterminate = MaterialSelectionGlyphFactory.CreateMenuIndeterminateGlyph(lightPalette, new Size(16, 16), true);
        _contextMenuSubMenu = MaterialSelectionGlyphFactory.CreateMenuSubMenuArrow(lightPalette, new Size(16, 16));
    }

    public PaletteMacOSLight()
        : base(new PaletteMacOSLight_BaseScheme(), _checkBoxList, _galleryButtonList, _radioButtonArray)
    {
        ThemeName = nameof(PaletteMacOSLight);
    }

    protected override bool IsDarkSurface() => false;

    private static Image? _contextMenuChecked;
    private static Image? _contextMenuIndeterminate;
    private static Image? _contextMenuSubMenu;

    public override Image? GetContextMenuCheckedImage() => _contextMenuChecked;
    public override Image? GetContextMenuIndeterminateImage() => _contextMenuIndeterminate;
    public override Image? GetContextMenuSubMenuImage() => _contextMenuSubMenu;
}