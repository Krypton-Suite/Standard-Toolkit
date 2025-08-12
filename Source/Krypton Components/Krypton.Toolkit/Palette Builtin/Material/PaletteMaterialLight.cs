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
            ColorDepth = ColorDepth.Depth24Bit
        };
        _checkBoxList.Images.AddStrip(CheckBoxStripResources.CheckBoxStrip2010Blue);

        _galleryButtonList = new ImageList
        {
            ImageSize = new Size(13, 7),
            ColorDepth = ColorDepth.Depth24Bit,
            TransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR
        };
        _galleryButtonList.Images.AddStrip(GalleryImageResources.Gallery2010);

        _radioButtonArray =
        [
            Office2010RadioButtonImageResources.RadioButton2010BlueD,
            Office2010RadioButtonImageResources.RadioButton2010BlueN,
            Office2010RadioButtonImageResources.RadioButton2010BlueT,
            Office2010RadioButtonImageResources.RadioButton2010BlueP,
            Office2010RadioButtonImageResources.RadioButton2010BlueDC,
            Office2010RadioButtonImageResources.RadioButton2010BlueNC,
            Office2010RadioButtonImageResources.RadioButton2010BlueTC,
            Office2010RadioButtonImageResources.RadioButton2010BluePC
        ];
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
