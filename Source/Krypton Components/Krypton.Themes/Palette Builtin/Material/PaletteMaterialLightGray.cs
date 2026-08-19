#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) et al. 2026. All rights reserved.
 */
#endregion

namespace Krypton.Themes;

/// <summary>
/// Material renderer using the Office 2013 Light Grey chrome colour map.
/// </summary>
public class PaletteMaterialLightGray : PaletteMaterialBase
{
    private static readonly ImageList _checkBoxList;
    private static readonly ImageList _galleryButtonList;
    private static readonly Image?[] _radioButtonArray;
    private static readonly PaletteOffice2013LightGray _forwardChrome = new PaletteOffice2013LightGray();
    private static Image? _contextMenuChecked;
    private static Image? _contextMenuIndeterminate;
    private static Image? _contextMenuSubMenu;
    private readonly Color _tabRowBackgroundColor = Color.FromArgb(230, 230, 230);

    static PaletteMaterialLightGray()
    {
        _checkBoxList = new ImageList
        {
            ImageSize = new Size(13, 13),
            ColorDepth = ColorDepth.Depth32Bit
        };

        var scheme = new PaletteOffice2013LightGray_BaseScheme();
        var glyphPalette = MaterialSelectionGlyphFactory.FromScheme(scheme, isDarkSurface: false);
        var cbStrip = MaterialSelectionGlyphFactory.CreateCheckBoxStrip(glyphPalette, _checkBoxList.ImageSize);
        for (int i = 0; i < cbStrip.Length; i++)
        {
            _checkBoxList.Images.Add(cbStrip[i]);
        }

        _galleryButtonList = new ImageList
        {
            ImageSize = new Size(13, 7),
            ColorDepth = ColorDepth.Depth24Bit,
            TransparentColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR
        };
        _galleryButtonList.Images.AddStrip(GalleryImageResources.Gallery2010);

        _radioButtonArray = MaterialSelectionGlyphFactory.CreateRadioButtonArray(glyphPalette, new Size(13, 13));
        _contextMenuChecked = MaterialSelectionGlyphFactory.CreateMenuCheckedGlyph(glyphPalette, new Size(16, 16), true);
        _contextMenuIndeterminate = MaterialSelectionGlyphFactory.CreateMenuIndeterminateGlyph(glyphPalette, new Size(16, 16), true);
        _contextMenuSubMenu = MaterialSelectionGlyphFactory.CreateMenuSubMenuArrow(glyphPalette, new Size(16, 16));
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteMaterialLightGray"/> class.
    /// </summary>
    public PaletteMaterialLightGray()
        : base(new PaletteOffice2013LightGray_BaseScheme(), _checkBoxList, _galleryButtonList, _radioButtonArray)
    {
        ThemeName = "Material - Light Gray";
    }

    /// <inheritdoc />
    protected override bool IsDarkSurface() => false;

    /// <inheritdoc />
    public override Image? GetContextMenuCheckedImage() => _contextMenuChecked;

    /// <inheritdoc />
    public override Image? GetContextMenuIndeterminateImage() => _contextMenuIndeterminate;

    /// <inheritdoc />
    public override Image? GetContextMenuSubMenuImage() => _contextMenuSubMenu;

    /// <inheritdoc />
    public override Image? GetButtonSpecImage(PaletteButtonSpecStyle style, PaletteState state) =>
        _forwardChrome.GetButtonSpecImage(style, state);

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabBottomColor(PaletteState state) =>
        SharedStaticVariables.DEFAULT_RIBBON_FILE_APP_TAB_BOTTOM_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTopColor(PaletteState state) =>
        SharedStaticVariables.DEFAULT_RIBBON_FILE_APP_TAB_TOP_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTextColor(PaletteState state) =>
        SharedStaticVariables.DEFAULT_RIBBON_FILE_APP_TAB_TEXT_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonTabRowGradientColor1(PaletteState state) => SharedStaticVariables.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) =>
        SharedStaticVariables.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) =>
        SharedStaticVariables.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) => _tabRowBackgroundColor;

    /// <inheritdoc />
    public override float GetRibbonTabRowGradientRaftingAngle(PaletteState state) => -1;
}
