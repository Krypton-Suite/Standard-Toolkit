#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Office 2007 renderer using the Office 2013 Dark Grey chrome colour map.
/// </summary>
public class PaletteOffice2007DarkGray : PaletteOffice2007Base
{
    #region Static Fields

    private readonly Color _tabRowBackgroundColor = Color.FromArgb(51, 51, 51);
    private static readonly Color _ribbonAppButtonDarkColor = Color.FromArgb(70, 70, 70);
    private static readonly Color _ribbonAppButtonLightColor = Color.FromArgb(51, 51, 51);
    private static readonly Color _ribbonAppButtonTextColor = Color.White;

    private static readonly ImageList _checkBoxList;
    private static readonly ImageList _galleryButtonList;
    private static readonly Image?[] _radioButtonArray;

    private static readonly Image _formCloseNormal = Office2007ControlBoxResources.Office2007ControlBoxBlackCloseNormal;
    private static readonly Image _formCloseActive = Office2007ControlBoxResources.Office2007ControlBoxBlackCloseActive;
    private static readonly Image _formCloseDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlackCloseDisabled;
    private static readonly Image _formClosePressed = Office2007ControlBoxResources.Office2007ControlBoxBlackClosePressed;
    private static readonly Image _formMaximiseNormal = Office2007ControlBoxResources.Office2007ControlBoxBlackMaximiseNormal;
    private static readonly Image _formMaximiseActive = Office2007ControlBoxResources.Office2007ControlBoxBlackMaximiseActive;
    private static readonly Image _formMaximiseDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlackMaximiseDisabled;
    private static readonly Image _formMaximisePressed = Office2007ControlBoxResources.Office2007ControlBoxBlackMaximisePressed;
    private static readonly Image _formMinimiseNormal = Office2007ControlBoxResources.Office2007ControlBoxBlackMinimiseNormal;
    private static readonly Image _formMinimiseActive = Office2007ControlBoxResources.Office2007ControlBoxBlackMinimiseActive;
    private static readonly Image _formMinimiseDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlackMinimiseDisabled;
    private static readonly Image _formMinimisePressed = Office2007ControlBoxResources.Office2007ControlBoxBlackMinimisePessed;
    private static readonly Image _formRestoreNormal = Office2007ControlBoxResources.Office2007ControlBoxBlackRestoreNormal;
    private static readonly Image _formRestoreActive = Office2007ControlBoxResources.Office2007ControlBoxBlackRestoreActive;
    private static readonly Image _formRestoreDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlackRestoreDisabled;
    private static readonly Image _formRestorePressed = Office2007ControlBoxResources.Office2007ControlBoxBlackRestorePressed;
    private static readonly Image _formHelpNormal = Office2007ControlBoxResources.Office2007HelpIconNormal;
    private static readonly Image _formHelpActive = Office2007ControlBoxResources.Office2007HelpIconHover;
    private static readonly Image _formHelpPressed = Office2007ControlBoxResources.Office2007HelpIconPressed;
    private static readonly Image _formHelpDisabled = Office2007ControlBoxResources.Office2007HelpIconDisabled;
    private static readonly Image? _contextMenuSubMenu = GenericImageResources.BlackContextMenuSub;

    #endregion

    #region Identity

    static PaletteOffice2007DarkGray()
    {
        _checkBoxList = new ImageList
        {
            ImageSize = new Size(13, 13),
            ColorDepth = ColorDepth.Depth24Bit
        };
        _checkBoxList.Images.AddStrip(CheckBoxStripResources.CheckBoxStrip2007Silver);
        _galleryButtonList = new ImageList
        {
            ImageSize = new Size(13, 7),
            ColorDepth = ColorDepth.Depth24Bit,
            TransparentColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR
        };
        _galleryButtonList.Images.AddStrip(GalleryImageResources.GallerySilverBlack);
        _radioButtonArray =
        [
            Office2007RadioButtonImageResources.RadioButton2007BlueD,
            Office2007RadioButtonImageResources.RadioButton2007SilverN,
            Office2007RadioButtonImageResources.RadioButton2007SilverT,
            Office2007RadioButtonImageResources.RadioButton2007SilverP,
            Office2007RadioButtonImageResources.RadioButton2007BlueDC,
            Office2007RadioButtonImageResources.RadioButton2007SilverNC,
            Office2007RadioButtonImageResources.RadioButton2007SilverTC,
            Office2007RadioButtonImageResources.RadioButton2007SilverPC
        ];
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteOffice2007DarkGray"/> class.
    /// </summary>
    public PaletteOffice2007DarkGray()
        : base(
            "Office 2007 - Dark Gray",
            new PaletteOffice2013DarkGray_BaseScheme(),
            _checkBoxList,
            _galleryButtonList,
            _radioButtonArray)
    {
    }

    #endregion

    #region Back

    /// <inheritdoc />
    public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state) => style switch
    {
        PaletteBackStyle.HeaderForm => PaletteColorStyle.Rounding2,
        _ => base.GetBackColorStyle(style, state)
    };

    #endregion

    /// <inheritdoc />
    public override Image? GetContextMenuSubMenuImage() => _contextMenuSubMenu;

    /// <inheritdoc />
    public override Image? GetButtonSpecImage(PaletteButtonSpecStyle style, PaletteState state) => style switch
    {
        PaletteButtonSpecStyle.FormClose => state switch
        {
            PaletteState.Disabled => _formCloseDisabled,
            PaletteState.Tracking => _formCloseActive,
            PaletteState.Pressed => _formClosePressed,
            _ => _formCloseNormal
        },
        PaletteButtonSpecStyle.FormMin => state switch
        {
            PaletteState.Disabled => _formMinimiseDisabled,
            PaletteState.Tracking => _formMinimiseActive,
            PaletteState.Pressed => _formMinimisePressed,
            _ => _formMinimiseNormal
        },
        PaletteButtonSpecStyle.FormMax => state switch
        {
            PaletteState.Disabled => _formMaximiseDisabled,
            PaletteState.Tracking => _formMaximiseActive,
            PaletteState.Pressed => _formMaximisePressed,
            _ => _formMaximiseNormal
        },
        PaletteButtonSpecStyle.FormRestore => state switch
        {
            PaletteState.Disabled => _formRestoreDisabled,
            PaletteState.Tracking => _formRestoreActive,
            PaletteState.Pressed => _formRestorePressed,
            _ => _formRestoreNormal
        },
        PaletteButtonSpecStyle.FormHelp => state switch
        {
            PaletteState.Disabled => _formHelpDisabled,
            PaletteState.Tracking => _formHelpActive,
            PaletteState.Pressed => _formHelpPressed,
            _ => _formHelpNormal
        },
        _ => base.GetButtonSpecImage(style, state)
    };

    /// <inheritdoc />
    public override PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteRibbonBackStyle style, PaletteState state) =>
        style == PaletteRibbonBackStyle.RibbonGroupArea
            ? PaletteRibbonColorStyle.Solid
            : base.GetRibbonBackColorStyle(style, state);

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

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabBottomColor(PaletteState state) => _ribbonAppButtonDarkColor;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTopColor(PaletteState state) => _ribbonAppButtonLightColor;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTextColor(PaletteState state) => _ribbonAppButtonTextColor;
}
