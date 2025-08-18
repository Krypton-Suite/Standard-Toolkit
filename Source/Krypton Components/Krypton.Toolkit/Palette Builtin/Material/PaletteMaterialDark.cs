#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Material Dark palette variant.
/// Temporarily reuses Microsoft 365 Black image lists and scheme while selecting the Material renderer.
/// </summary>
public class PaletteMaterialDark : PaletteMaterialBase
{
    private static readonly ImageList _checkBoxList;
    private static readonly ImageList _galleryButtonList;
    private static readonly Image?[] _radioButtonArray;

    private static readonly PaletteMicrosoft365BlackDarkMode _forward365 = new PaletteMicrosoft365BlackDarkMode();

    static PaletteMaterialDark()
    {
        _checkBoxList = new ImageList
        {
            ImageSize = new Size(13, 13),
            ColorDepth = ColorDepth.Depth32Bit
        };

        // Build glyph palette dynamically from the Material Dark scheme instead of hard-coding
        var scheme = new PaletteMaterialDark_BaseScheme();
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
            TransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR
        };
        _galleryButtonList.Images.AddStrip(GalleryImageResources.Gallery2010);

        _radioButtonArray = MaterialSelectionGlyphFactory.CreateRadioButtonArray(darkPalette, new Size(13, 13));

        // Override context menu glyphs to Material
        _contextMenuChecked = MaterialSelectionGlyphFactory.CreateMenuCheckedGlyph(darkPalette, new Size(16, 16), true);
        _contextMenuIndeterminate = MaterialSelectionGlyphFactory.CreateMenuIndeterminateGlyph(darkPalette, new Size(16, 16), true);
        _contextMenuSubMenu = MaterialSelectionGlyphFactory.CreateMenuSubMenuArrow(darkPalette, new Size(16, 16));
    }

    public PaletteMaterialDark()
        : base(new PaletteMaterialDark_BaseScheme(), _checkBoxList, _galleryButtonList, _radioButtonArray)
    {
    }

    protected override bool IsDarkSurface() => true;

    private static Image? _contextMenuChecked;
    private static Image? _contextMenuIndeterminate;
    private static Image? _contextMenuSubMenu;

    public override Image? GetContextMenuCheckedImage() => _contextMenuChecked;
    public override Image? GetContextMenuIndeterminateImage() => _contextMenuIndeterminate;
    public override Image? GetContextMenuSubMenuImage() => _contextMenuSubMenu;
    public override Image? GetButtonSpecImage(PaletteButtonSpecStyle style, PaletteState state) => _forward365.GetButtonSpecImage(style, state);
    public override Color GetRibbonFileAppTabBottomColor(PaletteState state) => _forward365.GetRibbonFileAppTabBottomColor(state);
    public override Color GetRibbonFileAppTabTopColor(PaletteState state) => _forward365.GetRibbonFileAppTabTopColor(state);
    public override Color GetRibbonFileAppTabTextColor(PaletteState state) => _forward365.GetRibbonFileAppTabTextColor(state);
    public override Color GetRibbonTabRowGradientColor1(PaletteState state) => _forward365.GetRibbonTabRowGradientColor1(state);
    public override Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) => _forward365.GetRibbonTabRowBackgroundGradientRaftingDark(state);
    public override Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) => _forward365.GetRibbonTabRowBackgroundGradientRaftingLight(state);
    public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) => _forward365.GetRibbonTabRowBackgroundSolidColor(state);
    public override float GetRibbonTabRowGradientRaftingAngle(PaletteState state) => _forward365.GetRibbonTabRowGradientRaftingAngle(state);

    /// <summary>
    /// Ensure grid lines use a neutral gray on dark surfaces instead of appearing white.
    /// </summary>
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
            // Reuse the theme's input border neutral gray for grid lines
            return BaseColors?.InputControlBorderNormal ?? Color.FromArgb(77, 77, 77);
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
            return BaseColors?.InputControlBorderNormal ?? Color.FromArgb(77, 77, 77);
        }

        return base.GetBorderColor2(style, state);
    }
}
