#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Material Light palette variant.
/// Temporarily reuses 365 Blue image lists; provides Material light scheme values (currently copied from 365).
/// </summary>
public sealed class PaletteMaterialLight : PaletteMaterialBase
{
    private static readonly ImageList _checkBoxList;
    private static readonly ImageList _galleryButtonList;
    private static readonly Image?[] _radioButtonArray;

    private static readonly PaletteMicrosoft365BlueLightMode _forward365 = new PaletteMicrosoft365BlueLightMode();

    static PaletteMaterialLight()
    {
        _checkBoxList = new ImageList
        {
            ImageSize = new Size(13, 13),
            ColorDepth = ColorDepth.Depth32Bit
        };

        var lightPalette = new MaterialSelectionGlyphFactory.MaterialGlyphPalette(
            outline: Color.FromArgb(0x60, 0x60, 0x60),
            primary: Color.FromArgb(0x1F, 0x6E, 0xC1),
            onPrimary: Color.White,
            disabled: Color.FromArgb(0xA0, 0xA0, 0xA0));

        var cbStrip = MaterialSelectionGlyphFactory.CreateCheckBoxStrip(lightPalette, _checkBoxList.ImageSize);
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

        _radioButtonArray = MaterialSelectionGlyphFactory.CreateRadioButtonArray(lightPalette, new Size(13, 13));
    }

    public PaletteMaterialLight()
        : base(new PaletteMaterialLight_BaseScheme(), _checkBoxList, _galleryButtonList, _radioButtonArray)
    {
    }

    public override Image? GetContextMenuSubMenuImage() => _forward365.GetContextMenuSubMenuImage();
    public override Image? GetButtonSpecImage(PaletteButtonSpecStyle style, PaletteState state) => _forward365.GetButtonSpecImage(style, state);
    public override Color GetRibbonFileAppTabBottomColor(PaletteState state) => _forward365.GetRibbonFileAppTabBottomColor(state);
    public override Color GetRibbonFileAppTabTopColor(PaletteState state) => _forward365.GetRibbonFileAppTabTopColor(state);
    public override Color GetRibbonFileAppTabTextColor(PaletteState state) => _forward365.GetRibbonFileAppTabTextColor(state);
    public override Color GetRibbonTabRowGradientColor1(PaletteState state) => _forward365.GetRibbonTabRowGradientColor1(state);
    public override Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) => _forward365.GetRibbonTabRowBackgroundGradientRaftingDark(state);
    public override Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) => _forward365.GetRibbonTabRowBackgroundGradientRaftingLight(state);
    public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) => _forward365.GetRibbonTabRowBackgroundSolidColor(state);
    public override float GetRibbonTabRowGradientRaftingAngle(PaletteState state) => _forward365.GetRibbonTabRowGradientRaftingAngle(state);
}
