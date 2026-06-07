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
/// macOS-inspired dark palette with traffic-light window controls and flat rounded rendering.
/// </summary>
public class PaletteMacOSDark : PaletteMacOSBase
{
    private static readonly ImageList _checkBoxList;
    private static readonly ImageList _galleryButtonList;
    private static readonly Image?[] _radioButtonArray;

    static PaletteMacOSDark()
    {
        _checkBoxList = new ImageList
        {
            ImageSize = new Size(13, 13),
            ColorDepth = ColorDepth.Depth32Bit
        };

        var scheme = new PaletteMacOSDark_BaseScheme();
        var darkPalette = MaterialSelectionGlyphFactory.FromScheme(scheme, isDarkSurface: true);

        var cbStrip = MaterialSelectionGlyphFactory.CreateCheckBoxStrip(darkPalette, _checkBoxList.ImageSize);
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

        _radioButtonArray = MaterialSelectionGlyphFactory.CreateRadioButtonArray(darkPalette, new Size(13, 13));

        _contextMenuChecked = MaterialSelectionGlyphFactory.CreateMenuCheckedGlyph(darkPalette, new Size(16, 16), true);
        _contextMenuIndeterminate = MaterialSelectionGlyphFactory.CreateMenuIndeterminateGlyph(darkPalette, new Size(16, 16), true);
        _contextMenuSubMenu = MaterialSelectionGlyphFactory.CreateMenuSubMenuArrow(darkPalette, new Size(16, 16));
    }

    public PaletteMacOSDark()
        : base(new PaletteMacOSDark_BaseScheme(), _checkBoxList, _galleryButtonList, _radioButtonArray)
    {
        ThemeName = nameof(PaletteMacOSDark);
    }

    protected override bool IsDarkSurface() => true;

    private static Image? _contextMenuChecked;
    private static Image? _contextMenuIndeterminate;
    private static Image? _contextMenuSubMenu;

    public override Image? GetContextMenuCheckedImage() => _contextMenuChecked;
    public override Image? GetContextMenuIndeterminateImage() => _contextMenuIndeterminate;
    public override Image? GetContextMenuSubMenuImage() => _contextMenuSubMenu;

    public override Color GetBorderColor1(PaletteBorderStyle style, PaletteState state)
    {
        if (style == PaletteBorderStyle.GridDataCellList ||
            style == PaletteBorderStyle.GridDataCellSheet ||
            style == PaletteBorderStyle.GridDataCellCustom1 ||
            style == PaletteBorderStyle.GridDataCellCustom2 ||
            style == PaletteBorderStyle.GridDataCellCustom3 ||
            style == PaletteBorderStyle.GridHeaderColumnList ||
            style == PaletteBorderStyle.GridHeaderColumnSheet ||
            style == PaletteBorderStyle.GridHeaderColumnCustom1 ||
            style == PaletteBorderStyle.GridHeaderColumnCustom2 ||
            style == PaletteBorderStyle.GridHeaderColumnCustom3 ||
            style == PaletteBorderStyle.GridHeaderRowList ||
            style == PaletteBorderStyle.GridHeaderRowSheet ||
            style == PaletteBorderStyle.GridHeaderRowCustom1 ||
            style == PaletteBorderStyle.GridHeaderRowCustom2 ||
            style == PaletteBorderStyle.GridHeaderRowCustom3)
        {
            return BaseColors?.InputControlBorderNormal ?? Color.FromArgb(84, 84, 88);
        }

        return base.GetBorderColor1(style, state);
    }

    public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
    {
        if (style == PaletteBorderStyle.GridDataCellList ||
            style == PaletteBorderStyle.GridDataCellSheet ||
            style == PaletteBorderStyle.GridDataCellCustom1 ||
            style == PaletteBorderStyle.GridDataCellCustom2 ||
            style == PaletteBorderStyle.GridDataCellCustom3 ||
            style == PaletteBorderStyle.GridHeaderColumnList ||
            style == PaletteBorderStyle.GridHeaderColumnSheet ||
            style == PaletteBorderStyle.GridHeaderColumnCustom1 ||
            style == PaletteBorderStyle.GridHeaderColumnCustom2 ||
            style == PaletteBorderStyle.GridHeaderColumnCustom3 ||
            style == PaletteBorderStyle.GridHeaderRowList ||
            style == PaletteBorderStyle.GridHeaderRowSheet ||
            style == PaletteBorderStyle.GridHeaderRowCustom1 ||
            style == PaletteBorderStyle.GridHeaderRowCustom2 ||
            style == PaletteBorderStyle.GridHeaderRowCustom3)
        {
            return BaseColors?.InputControlBorderNormal ?? Color.FromArgb(84, 84, 88);
        }

        return base.GetBorderColor2(style, state);
    }
}