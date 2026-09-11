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
/// Office 2013 renderer with protanopia-friendly colours (#4168).
/// </summary>
public class PaletteOffice2013Protanopia : PaletteOffice2013Base
{
    private readonly Color _tabRowBackgroundColor = Color.FromArgb(210, 200, 190);
    private static readonly Color _ribbonAppButtonDarkColor = Color.FromArgb(0, 70, 140);
    private static readonly Color _ribbonAppButtonLightColor = Color.FromArgb(0, 90, 181);
    private static readonly Color _ribbonAppButtonTextColor = Color.White;

    private static readonly ImageList _checkBoxList;
    private static readonly ImageList _galleryButtonList;
    private static readonly Image?[] _radioButtonArray;

    private static readonly Image? _contextMenuSubMenu = Office2010ArrowResources.Office2010BlueContextMenuSub;
    private static readonly Image _formCloseNormal = Office2010ControlBoxResources.Office2010BlueCloseNormal;
    private static readonly Image _formCloseDisabled = Office2010ControlBoxResources.Office2010BlueCloseDisabled;
    private static readonly Image _formCloseActive = Office2010ControlBoxResources.Office2010BlueCloseActive;
    private static readonly Image _formClosePressed = Office2010ControlBoxResources.Office2010BlueClosePressed;
    private static readonly Image _formMaximiseNormal = Office2010ControlBoxResources.Office2010BlueMaximiseNormal;
    private static readonly Image _formMaximiseDisabled = Office2010ControlBoxResources.Office2010BlueMaximiseDisabled;
    private static readonly Image _formMaximiseActive = Office2010ControlBoxResources.Office2010BlueMaximiseActive;
    private static readonly Image _formMaximisePressed = Office2010ControlBoxResources.Office2010BlueMaximisePressed;
    private static readonly Image _formMinimiseNormal = Office2010ControlBoxResources.Office2010BlueMinimiseNormal;
    private static readonly Image _formMinimiseActive = Office2010ControlBoxResources.Office2010BlueMinimiseActive;
    private static readonly Image _formMinimiseDisabled = Office2010ControlBoxResources.Office2010BlueMinimiseDisabled;
    private static readonly Image _formMinimisePressed = Office2010ControlBoxResources.Office2010BlueMinimisePressed;
    private static readonly Image _formRestoreNormal = Office2010ControlBoxResources.Office2010BlueRestoreNormal;
    private static readonly Image _formRestoreDisabled = Office2010ControlBoxResources.Office2010BlueRestoreDisabled;
    private static readonly Image _formRestoreActive = Office2010ControlBoxResources.Office2010BlueRestoreActive;
    private static readonly Image _formRestorePressed = Office2010ControlBoxResources.Office2010BlueRestorePressed;
    private static readonly Image _formHelpNormal = Office2010ControlBoxResources.Office2010HelpIconNormal;
    private static readonly Image _formHelpActive = Office2010ControlBoxResources.Office2010HelpIconHover;
    private static readonly Image _formHelpPressed = Office2010ControlBoxResources.Office2010HelpIconPressed;
    private static readonly Image _formHelpDisabled = Office2010ControlBoxResources.Office2010HelpIconDisabled;

    static PaletteOffice2013Protanopia()
    {
        _checkBoxList = new ImageList { ImageSize = new Size(13, 13), ColorDepth = ColorDepth.Depth24Bit };
        _checkBoxList.Images.AddStrip(CheckBoxStripResources.CheckBoxStrip2010Blue);
        _galleryButtonList = new ImageList
        {
            ImageSize = new Size(13, 7),
            ColorDepth = ColorDepth.Depth24Bit,
            TransparentColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR
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

    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteOffice2013Protanopia"/> class.
    /// </summary>
    public PaletteOffice2013Protanopia()
        : base(new PaletteProtanopia_BaseScheme(), _checkBoxList, _galleryButtonList, _radioButtonArray)
    {
        ThemeName = nameof(PaletteOffice2013Protanopia);
        AccessibilityPaletteAccents.ApplyProtanopia(this);
    }
    /// <inheritdoc />
    public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
    {
        Color? text = AccessibilityContentContrast.TryGetProtanopiaText(style, state);
        return text ?? base.GetContentShortTextColor1(style, state);
    }

    /// <inheritdoc />
    public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
    {
        Color? text = AccessibilityContentContrast.TryGetProtanopiaText(style, state);
        return text ?? base.GetContentShortTextColor2(style, state);
    }

    /// <inheritdoc />
    public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state)
    {
        Color? text = AccessibilityContentContrast.TryGetProtanopiaText(style, state);
        return text ?? base.GetContentLongTextColor1(style, state);
    }

    /// <inheritdoc />
    public override Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state)
    {
        Color? text = AccessibilityContentContrast.TryGetProtanopiaText(style, state);
        return text ?? base.GetContentLongTextColor2(style, state);
    }


    public override Image? GetContextMenuSubMenuImage() => _contextMenuSubMenu;

    public override Image? GetButtonSpecImage(PaletteButtonSpecStyle style, PaletteState state) => style switch
    {
        PaletteButtonSpecStyle.FormClose => state switch
        {
            PaletteState.Tracking => _formCloseActive,
            PaletteState.Normal => _formCloseNormal,
            PaletteState.Pressed => _formClosePressed,
            _ => _formCloseDisabled
        },
        PaletteButtonSpecStyle.FormMin => state switch
        {
            PaletteState.Normal => _formMinimiseNormal,
            PaletteState.Tracking => _formMinimiseActive,
            PaletteState.Pressed => _formMinimisePressed,
            _ => _formMinimiseDisabled
        },
        PaletteButtonSpecStyle.FormMax => state switch
        {
            PaletteState.Normal => _formMaximiseNormal,
            PaletteState.Tracking => _formMaximiseActive,
            PaletteState.Pressed => _formMaximisePressed,
            _ => _formMaximiseDisabled
        },
        PaletteButtonSpecStyle.FormRestore => state switch
        {
            PaletteState.Normal => _formRestoreNormal,
            PaletteState.Tracking => _formRestoreActive,
            PaletteState.Pressed => _formRestorePressed,
            _ => _formRestoreDisabled
        },
        PaletteButtonSpecStyle.FormHelp => state switch
        {
            PaletteState.Tracking => _formHelpActive,
            PaletteState.Pressed => _formHelpPressed,
            PaletteState.Normal => _formHelpNormal,
            _ => _formHelpDisabled
        },
        _ => base.GetButtonSpecImage(style, state)
    };

    public override Color GetRibbonTabRowGradientColor1(PaletteState state) => SharedStaticVariables.EMPTY_COLOR;
    public override Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) => SharedStaticVariables.EMPTY_COLOR;
    public override Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) => SharedStaticVariables.EMPTY_COLOR;
    public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) => _tabRowBackgroundColor;
    public override float GetRibbonTabRowGradientRaftingAngle(PaletteState state) => -1;
    public override Color GetRibbonFileAppTabBottomColor(PaletteState state) => _ribbonAppButtonDarkColor;
    public override Color GetRibbonFileAppTabTopColor(PaletteState state) => _ribbonAppButtonLightColor;
    public override Color GetRibbonFileAppTabTextColor(PaletteState state) => _ribbonAppButtonTextColor;
}