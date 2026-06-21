#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Mac OS X Aqua-inspired palette (classic gel buttons and pinstripe chrome).
/// </summary>
public class PaletteMacOSXAqua : PaletteMacOSXAquaBase
{
    #region Static Fields

    private readonly Color _tabRowBackgroundGradientRaftingDarkColor = Color.FromArgb(198, 210, 228);

    private readonly Color _tabRowBackgroundGradientRaftingLightColor = Color.FromArgb(214, 224, 238);

    private static readonly Color _ribbonAppButtonDarkColor = Color.FromArgb(0, 88, 176);

    private static readonly Color _ribbonAppButtonLightColor = Color.FromArgb(74, 141, 244);

    private static readonly Color _ribbonAppButtonTextColor = Color.White;

    private readonly float _gradientRafting = GlobalStaticConstants.DEFAULT_RAFTING_RIBBON_TAB_BACKGROUND_GRADIENT;

    private static readonly ImageList _checkBoxList;
    private static readonly ImageList _galleryButtonList;
    private static readonly Image?[] _radioButtonArray;
    private static readonly Image? _contextMenuSubMenu = Office2010ArrowResources.Office2010BlueContextMenuSub;

    #endregion

    #region Identity

    static PaletteMacOSXAqua()
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
            TransparentColor = GlobalStaticVariables.TRANSPARENCY_KEY_COLOR
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

    public PaletteMacOSXAqua()
        : base(new PaletteMacOSXAqua_BaseScheme(), _checkBoxList, _galleryButtonList, _radioButtonArray)
    {
        ThemeName = nameof(PaletteMacOSXAqua);
    }

    #endregion

    #region Images

    public override Image? GetContextMenuSubMenuImage() => _contextMenuSubMenu;

    #endregion

    #region Tab Row Background

    public override Color GetRibbonTabRowGradientColor1(PaletteState state) =>
        GlobalStaticVariables.TAB_ROW_GRADIENT_FIRST_COLOR;

    public override Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) =>
        _tabRowBackgroundGradientRaftingDarkColor;

    public override Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) =>
        _tabRowBackgroundGradientRaftingLightColor;

    public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) => GlobalStaticVariables.EMPTY_COLOR;

    public override float GetRibbonTabRowGradientRaftingAngle(PaletteState state) => _gradientRafting;

    #endregion

    #region AppButton Colors

    public override Color GetRibbonFileAppTabBottomColor(PaletteState state) => _ribbonAppButtonDarkColor;

    public override Color GetRibbonFileAppTabTopColor(PaletteState state) => _ribbonAppButtonLightColor;

    public override Color GetRibbonFileAppTabTextColor(PaletteState state) => _ribbonAppButtonTextColor;

    #endregion

    #region Images

    public override Image? GetSizeGripImage(RightToLeft isRtl) => SizeGripStyleResources.Office2010BlueGripStyle;

    #endregion
}