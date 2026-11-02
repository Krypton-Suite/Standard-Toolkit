#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Themes;

/// <summary>
/// Material renderer with accessibility colours (#4168).
/// </summary>
public class PaletteMaterialHighContrast : PaletteMaterialBase
{
    private static readonly ImageList _checkBoxList;
    private static readonly ImageList _galleryButtonList;
    private static readonly Image?[] _radioButtonArray;
    private static Image? _contextMenuChecked;
    private static Image? _contextMenuIndeterminate;
    private static Image? _contextMenuSubMenu;

    private readonly AccessibilityMaterialAccents.AccentSet _accents = AccessibilityMaterialAccents.HighContrast;

    static PaletteMaterialHighContrast()
    {
        _checkBoxList = new ImageList
        {
            ImageSize = new Size(13, 13),
            ColorDepth = ColorDepth.Depth32Bit
        };

        var scheme = new PaletteHighContrast_BaseScheme();
        var glyphPalette = MaterialSelectionGlyphFactory.FromScheme(scheme, isDarkSurface: true);
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
    /// Initialize a new instance of the <see cref="PaletteMaterialHighContrast"/> class.
    /// </summary>
    public PaletteMaterialHighContrast()
        : base(new PaletteHighContrast_BaseScheme(), _checkBoxList, _galleryButtonList, _radioButtonArray)
    {
        ThemeName = nameof(PaletteMaterialHighContrast);
    }

    /// <inheritdoc />
    protected override bool IsDarkSurface() => true;

    /// <inheritdoc />
    public override Color GetBackColor1(PaletteBackStyle style, PaletteState state)
    {
        if (AccessibilityMaterialAccents.IsButtonBackStyle(style))
        {
            Color? accent = AccessibilityMaterialAccents.TryGetButtonBack(state, _accents);
            if (accent.HasValue)
            {
                return accent.Value;
            }
        }

        if (AccessibilityHighContrastChrome.TryGetSurfaceBack(style) is Color surface)
        {
            return surface;
        }
        return base.GetBackColor1(style, state);
    }

    /// <inheritdoc />
    public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
    {
        if (AccessibilityMaterialAccents.IsButtonBackStyle(style))
        {
            Color? accent = AccessibilityMaterialAccents.TryGetButtonBack(state, _accents);
            if (accent.HasValue)
            {
                return accent.Value;
            }
        }

        if (AccessibilityHighContrastChrome.TryGetSurfaceBack(style) is Color surface)
        {
            return surface;
        }
        return base.GetBackColor2(style, state);
    }

    /// <inheritdoc />
    public override Image? GetContextMenuCheckedImage() => _contextMenuChecked;

    /// <inheritdoc />
    public override Image? GetContextMenuIndeterminateImage() => _contextMenuIndeterminate;

    /// <inheritdoc />
    public override Image? GetContextMenuSubMenuImage() => _contextMenuSubMenu;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabBottomColor(PaletteState state) =>
        BaseColors?.HeaderPrimaryBack2 ?? Color.Black;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTopColor(PaletteState state) =>
        BaseColors?.HeaderPrimaryBack1 ?? Color.Black;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTextColor(PaletteState state) =>
        BaseColors?.HeaderText ?? Color.White;

    /// <inheritdoc />
    public override Color GetRibbonTabRowGradientColor1(PaletteState state) => SharedStaticVariables.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) =>
        BaseColors?.HeaderPrimaryBack2 ?? Color.Black;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) =>
        BaseColors?.HeaderPrimaryBack1 ?? Color.Black;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) =>
        BaseColors?.HeaderPrimaryBack1 ?? Color.Black;

    /// <inheritdoc />
    public override float GetRibbonTabRowGradientRaftingAngle(PaletteState state) =>
        SharedStaticConstants.DEFAULT_RAFTING_RIBBON_TAB_BACKGROUND_GRADIENT;
}