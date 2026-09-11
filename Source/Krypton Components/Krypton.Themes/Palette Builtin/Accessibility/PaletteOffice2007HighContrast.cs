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
/// Office 2007 renderer with high-contrast colours (#4168).
/// </summary>
public class PaletteOffice2007HighContrast : PaletteOffice2007Base
{
    private static readonly ImageList _checkBoxList;
    private static readonly ImageList _galleryButtonList;
    private static readonly Image?[] _radioButtonArray;

    private static readonly Image? _dropDownButton = GenericImageResources.BlueDropDownButton;
    private static readonly Image _closeNormal = Office2007ControlBoxResources.Office2007ControlBoxBlackCloseNormal;
    private static readonly Image _closeActive = Office2007ControlBoxResources.Office2007ControlBoxBlackCloseActive;
    private static readonly Image _closeDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlackCloseDisabled;
    private static readonly Image _closePressed = Office2007ControlBoxResources.Office2007ControlBoxBlackClosePressed;
    private static readonly Image _maxNormal = Office2007ControlBoxResources.Office2007ControlBoxBlackMaximiseNormal;
    private static readonly Image _maxActive = Office2007ControlBoxResources.Office2007ControlBoxBlackMaximiseActive;
    private static readonly Image _maxDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlackMaximiseDisabled;
    private static readonly Image _maxPressed = Office2007ControlBoxResources.Office2007ControlBoxBlackMaximisePressed;
    private static readonly Image _minNormal = Office2007ControlBoxResources.Office2007ControlBoxBlackMinimiseNormal;
    private static readonly Image _minActive = Office2007ControlBoxResources.Office2007ControlBoxBlackMinimiseActive;
    private static readonly Image _minDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlackMinimiseDisabled;
    private static readonly Image _minPressed = Office2007ControlBoxResources.Office2007ControlBoxBlackMinimisePessed;
    private static readonly Image _restoreNormal = Office2007ControlBoxResources.Office2007ControlBoxBlackRestoreNormal;
    private static readonly Image _restoreActive = Office2007ControlBoxResources.Office2007ControlBoxBlackRestoreActive;
    private static readonly Image _restoreDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlackRestoreDisabled;
    private static readonly Image _restorePressed = Office2007ControlBoxResources.Office2007ControlBoxBlackRestorePressed;
    private static readonly Image _helpNormal = Office2007ControlBoxResources.Office2007HelpIconNormal;
    private static readonly Image _helpActive = Office2007ControlBoxResources.Office2007HelpIconHover;
    private static readonly Image _helpDisabled = Office2007ControlBoxResources.Office2007HelpIconDisabled;
    private static readonly Image _helpPressed = Office2007ControlBoxResources.Office2007HelpIconPressed;
    private static readonly Image? _contextMenuSubMenu = GenericImageResources.BlueContextMenuSub;

    static PaletteOffice2007HighContrast()
    {
        _checkBoxList = new ImageList { ImageSize = new Size(13, 13), ColorDepth = ColorDepth.Depth24Bit };
        _checkBoxList.Images.AddStrip(CheckBoxStripResources.CheckBoxStrip2007Black);
        _galleryButtonList = new ImageList
        {
            ImageSize = new Size(13, 7),
            ColorDepth = ColorDepth.Depth24Bit,
            TransparentColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR
        };
        _galleryButtonList.Images.AddStrip(GalleryImageResources.GalleryBlue);
        _radioButtonArray =
        [
            Office2007RadioButtonImageResources.RadioButton2007BlueD,
            Office2007RadioButtonImageResources.RadioButton2007BlueN,
            Office2007RadioButtonImageResources.RadioButton2007BlueT,
            Office2007RadioButtonImageResources.RadioButton2007BlueP,
            Office2007RadioButtonImageResources.RadioButton2007BlueDC,
            Office2007RadioButtonImageResources.RadioButton2007BlueNC,
            Office2007RadioButtonImageResources.RadioButton2007BlueTC,
            Office2007RadioButtonImageResources.RadioButton2007BluePC
        ];
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteOffice2007HighContrast"/> class.
    /// </summary>
    public PaletteOffice2007HighContrast()
        : base("Office 2007 - High Contrast", new PaletteHighContrast_BaseScheme(), _checkBoxList, _galleryButtonList, _radioButtonArray)
    {
        ThemeName = nameof(PaletteOffice2007HighContrast);
        AccessibilityPaletteAccents.ApplyHighContrast(this);
    }

    /// <inheritdoc />
    public override Color GetBackColor1(PaletteBackStyle style, PaletteState state)
    {
        Color? surface = AccessibilityHighContrastChrome.TryGetSurfaceBack(style);
        return surface ?? base.GetBackColor1(style, state);
    }

    /// <inheritdoc />
    public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
    {
        Color? surface = AccessibilityHighContrastChrome.TryGetSurfaceBack(style);
        return surface ?? base.GetBackColor2(style, state);
    }


    /// <summary>
    /// Gets the color background drawing style.
    /// </summary>
    public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state) => style switch
    {
        PaletteBackStyle.HeaderForm => PaletteColorStyle.Rounding4,
        _ => base.GetBackColorStyle(style, state)
    };
    public override Image? GetContextMenuSubMenuImage() => _contextMenuSubMenu;

    public override Image? GetSizeGripImage(RightToLeft isRtl) => SizeGripStyleResources.Office2007BlueGripStyle;

    public override Image? GetButtonSpecImage(PaletteButtonSpecStyle style, PaletteState state) => style switch
    {
        PaletteButtonSpecStyle.FormClose => state switch
        {
            PaletteState.Disabled => _closeDisabled,
            PaletteState.Tracking => _closeActive,
            PaletteState.Pressed => _closePressed,
            _ => _closeNormal
        },
        PaletteButtonSpecStyle.FormMin => state switch
        {
            PaletteState.Disabled => _minDisabled,
            PaletteState.Tracking => _minActive,
            PaletteState.Pressed => _minPressed,
            _ => _minNormal
        },
        PaletteButtonSpecStyle.FormMax => state switch
        {
            PaletteState.Disabled => _maxDisabled,
            PaletteState.Tracking => _maxActive,
            PaletteState.Pressed => _maxPressed,
            _ => _maxNormal
        },
        PaletteButtonSpecStyle.FormRestore => state switch
        {
            PaletteState.Disabled => _restoreDisabled,
            PaletteState.Tracking => _restoreActive,
            PaletteState.Pressed => _restorePressed,
            _ => _restoreNormal
        },
        PaletteButtonSpecStyle.FormHelp => state switch
        {
            PaletteState.Disabled => _helpDisabled,
            PaletteState.Tracking => _helpActive,
            PaletteState.Pressed => _helpPressed,
            _ => _helpNormal
        },
        _ => base.GetButtonSpecImage(style, state)
    };
public override Color GetRibbonTabRowGradientColor1(PaletteState state) => SharedStaticVariables.EMPTY_COLOR;
    public override Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) => SharedStaticVariables.EMPTY_COLOR;
    public override Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) => SharedStaticVariables.EMPTY_COLOR;
    public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) => SharedStaticVariables.EMPTY_COLOR;
    public override float GetRibbonTabRowGradientRaftingAngle(PaletteState state) => -1;
    public override Color GetRibbonFileAppTabBottomColor(PaletteState state) => SharedStaticVariables.EMPTY_COLOR;
    public override Color GetRibbonFileAppTabTopColor(PaletteState state) => SharedStaticVariables.EMPTY_COLOR;
    public override Color GetRibbonFileAppTabTextColor(PaletteState state) => SharedStaticVariables.EMPTY_COLOR;
}
