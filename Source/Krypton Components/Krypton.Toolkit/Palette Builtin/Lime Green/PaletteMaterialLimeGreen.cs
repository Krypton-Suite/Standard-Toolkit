#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) et al. 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Material Light Lime Green palette — flat Material metrics/renderer with Lime Green accents.
/// </summary>
public class PaletteMaterialLimeGreen : PaletteMaterialBase
{
    private static readonly ImageList _checkBoxList;
    private static readonly ImageList _galleryButtonList;
    private static readonly Image?[] _radioButtonArray;
    private static readonly PaletteMicrosoft365BlueLightMode _forward365 = new PaletteMicrosoft365BlueLightMode();
    private static Image? _contextMenuChecked;
    private static Image? _contextMenuIndeterminate;
    private static Image? _contextMenuSubMenu;

    static PaletteMaterialLimeGreen()
    {
        _checkBoxList = new ImageList
        {
            ImageSize = new Size(13, 13),
            ColorDepth = ColorDepth.Depth32Bit
        };

        var scheme = LimeGreenSchemeHelper.Create(new PaletteMaterialLight_BaseScheme(), dark: false);
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

        LimeGreenSchemeHelper.RegisterButtonStateColors<PaletteMaterialLimeGreen>(dark: false);
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteMaterialLimeGreen"/> class.
    /// </summary>
    public PaletteMaterialLimeGreen()
        : base(
            LimeGreenSchemeHelper.Create(new PaletteMaterialLight_BaseScheme(), dark: false),
            _checkBoxList,
            _galleryButtonList,
            _radioButtonArray)
    {
        ThemeName = "Material - Lime Green";
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
        _forward365.GetButtonSpecImage(style, state);

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabBottomColor(PaletteState state) =>
        _forward365.GetRibbonFileAppTabBottomColor(state);

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTopColor(PaletteState state) =>
        _forward365.GetRibbonFileAppTabTopColor(state);

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTextColor(PaletteState state) =>
        _forward365.GetRibbonFileAppTabTextColor(state);

    /// <inheritdoc />
    public override Color GetRibbonTabRowGradientColor1(PaletteState state) =>
        _forward365.GetRibbonTabRowGradientColor1(state);

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) =>
        _forward365.GetRibbonTabRowBackgroundGradientRaftingDark(state);

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) =>
        _forward365.GetRibbonTabRowBackgroundGradientRaftingLight(state);

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) =>
        _forward365.GetRibbonTabRowBackgroundSolidColor(state);

    /// <inheritdoc />
    public override float GetRibbonTabRowGradientRaftingAngle(PaletteState state) =>
        _forward365.GetRibbonTabRowGradientRaftingAngle(state);

    /// <inheritdoc />
    public override Color GetBackColor1(PaletteBackStyle style, PaletteState state) =>
        IsLimeFilledButton(style) ? LimeGreenSchemeHelper.GetMaterialButtonBack(state) : base.GetBackColor1(style, state);

    /// <inheritdoc />
    public override Color GetBackColor2(PaletteBackStyle style, PaletteState state) =>
        IsLimeFilledButton(style) ? LimeGreenSchemeHelper.GetMaterialButtonBack(state) : base.GetBackColor2(style, state);

    /// <inheritdoc />
    public override Color GetBorderColor1(PaletteBorderStyle style, PaletteState state) =>
        IsLimeFilledButtonBorder(style) ? LimeGreenSchemeHelper.GetMaterialButtonBorder(state) : base.GetBorderColor1(style, state);

    /// <inheritdoc />
    public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state) =>
        IsLimeFilledButtonBorder(style) ? LimeGreenSchemeHelper.GetMaterialButtonBorder(state) : base.GetBorderColor2(style, state);

    /// <inheritdoc />
    public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state) =>
        IsLimeButtonContent(style)
            ? (state == PaletteState.Disabled ? Color.FromArgb(0x80, 0x80, 0x80) : Color.Black)
            : base.GetContentShortTextColor1(style, state);

    /// <inheritdoc />
    public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state) =>
        GetContentShortTextColor1(style, state);

    private static bool IsLimeFilledButton(PaletteBackStyle style) => style switch
    {
        PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonAlternate
            or PaletteBackStyle.ButtonCluster or PaletteBackStyle.ButtonCustom1
            or PaletteBackStyle.ButtonCustom2 or PaletteBackStyle.ButtonCustom3
            or PaletteBackStyle.ButtonCommand => true,
        _ => false
    };

    private static bool IsLimeFilledButtonBorder(PaletteBorderStyle style) => style switch
    {
        PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonAlternate
            or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonCustom1
            or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3
            or PaletteBorderStyle.ButtonCommand => true,
        _ => false
    };

    private static bool IsLimeButtonContent(PaletteContentStyle style) => style switch
    {
        PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonAlternate
            or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonCustom1
            or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3
            or PaletteContentStyle.ButtonCommand => true,
        _ => false
    };
}
