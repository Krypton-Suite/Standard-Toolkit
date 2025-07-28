#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides the Black color scheme variant of the Office 2010 palette.
/// </summary>
public class PaletteOffice2010Black : PaletteOffice2010BlackBase
{
    #region Static Fields

    #region Colors

    private readonly Color _tabRowBackgroundGradientRaftingDarkColor = Color.FromArgb(71, 71, 71);

    private readonly Color _tabRowBackgroundGradientRaftingLightColor = Color.FromArgb(113, 113, 113);

    #endregion

    #region Ribbon Specific Colors

    //private static readonly Color _ribbonAppButtonDarkColor = GlobalStaticValues.DEFAULT_RIBBON_FILE_APP_TAB_BOTTOM_COLOR;

    //private static readonly Color _ribbonAppButtonLightColor = GlobalStaticValues.DEFAULT_RIBBON_FILE_APP_TAB_TOP_COLOR;

    //private static readonly Color _ribbonAppButtonTextColor = GlobalStaticValues.DEFAULT_RIBBON_FILE_APP_TAB_TEXT_COLOR;
    private static readonly Color _ribbonAppButtonDarkColor = Color.FromArgb(41, 41, 41);

    private static readonly Color _ribbonAppButtonLightColor = Color.FromArgb(79, 79, 79);

    private static readonly Color _ribbonAppButtonTextColor = Color.White;

    #endregion

    #region Rafting

    private readonly float _gradientRafting = GlobalStaticValues.DEFAULT_RAFTING_RIBBON_TAB_BACKGROUND_GRADIENT;

    #endregion

    #region Image Lists

    private static readonly ImageList _checkBoxList;
    private static readonly ImageList _galleryButtonList;

    #endregion

    #region Image Array

    private static readonly Image?[] _radioButtonArray;

    #endregion

    #region Images

    private static readonly Image? _blackDropDownButton = Office2010ArrowResources.Office2010BlackDropDownButton;
    private static readonly Image? _contextMenuSubMenu = Office2010ArrowResources.Office2010BlackContextMenuSub;
    private static readonly Image _formCloseNormal = Office2010ControlBoxResources.Office2010BlackCloseNormal;
    private static readonly Image _formCloseDisabled = Office2010ControlBoxResources.Office2010BlackCloseDisabled;
    private static readonly Image _formCloseActive = Office2010ControlBoxResources.Office2010BlackCloseActive;
    private static readonly Image _formClosePressed = Office2010ControlBoxResources.Office2010BlackClosePressed;
    private static readonly Image _formMaximiseNormal = Office2010ControlBoxResources.Office2010BackMaximiseNormal;
    private static readonly Image _formMaximiseDisabled = Office2010ControlBoxResources.Office2010BlackMaximiseDisabled;
    private static readonly Image _formMaximiseActive = Office2010ControlBoxResources.Office2010BlackMaximiseActive;
    private static readonly Image _formMaximisePressed = Office2010ControlBoxResources.Office2010BlackMaximisePressed;
    private static readonly Image _formMinimiseNormal = Office2010ControlBoxResources.Office2010BlackMinimiseNormal;
    private static readonly Image _formMinimiseActive = Office2010ControlBoxResources.Office2010BlackMinimiseActive;
    private static readonly Image _formMinimiseDisabled = Office2010ControlBoxResources.Office2010BlackMinimiseDisabled;
    private static readonly Image _formMinimisePressed = Office2010ControlBoxResources.Office2010BlackMinimisePressed;
    private static readonly Image _formRestoreNormal = Office2010ControlBoxResources.Office2010BlackRestoreNormal;
    private static readonly Image _formRestoreDisabled = Office2010ControlBoxResources.Office2010BlackRestoreDisabled;
    private static readonly Image _formRestoreActive = Office2010ControlBoxResources.Office2010BlackRestoreActive;
    private static readonly Image _formRestorePressed = Office2010ControlBoxResources.Office2010BlackRestorePressed;
    private static readonly Image _formHelpNormal = Office2010ControlBoxResources.Office2010HelpIconNormal;
    private static readonly Image _formHelpActive = Office2010ControlBoxResources.Office2010HelpIconHover;
    private static readonly Image _formHelpPressed = Office2010ControlBoxResources.Office2010HelpIconPressed;
    private static readonly Image _formHelpDisabled = Office2010ControlBoxResources.Office2010HelpIconDisabled;
    private static readonly Image _buttonSpecPendantClose = Office2010MDIImageResources.Office2010ButtonMDICloseBlack;
    private static readonly Image _buttonSpecPendantMin = Office2010MDIImageResources.Office2010ButtonMDIMinBlack;
    private static readonly Image _buttonSpecPendantRestore = Office2010MDIImageResources.Office2010ButtonMDIRestoreBlack;
    private static readonly Image _buttonSpecRibbonMinimize = RibbonArrowImageResources.RibbonUp2010Black;
    private static readonly Image _buttonSpecRibbonExpand = RibbonArrowImageResources.RibbonDown2010Black;

    #endregion

    #region Colour Arrays

    private static readonly Color _disabledText = Color.FromArgb(167, 167, 167);

    #endregion

    #endregion

    #region Identity

    static PaletteOffice2010Black()
    {
        _checkBoxList = new ImageList
        {
            ImageSize = new Size(13, 13),
            ColorDepth = ColorDepth.Depth24Bit
        };
        _checkBoxList.Images.AddStrip(CheckBoxStripResources.CheckBoxStrip2010Black);
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
            Office2010RadioButtonImageResources.RadioButton2010SilverN,
            Office2010RadioButtonImageResources.RadioButton2010BlueT,
            Office2010RadioButtonImageResources.RadioButton2010BlueP,
            Office2010RadioButtonImageResources.RadioButton2010BlueDC,
            Office2010RadioButtonImageResources.RadioButton2010SilverNC,
            Office2010RadioButtonImageResources.RadioButton2010SilverTC,
            Office2010RadioButtonImageResources.RadioButton2010SilverPC
        ];
    }

    /// <summary>
    /// Initialize a new instance of the PaletteOffice2010Black class.
    /// </summary>
    public PaletteOffice2010Black()
        : base(
        new PaletteOffice2010Black_BaseScheme(),
        _checkBoxList,
        _galleryButtonList,
        _radioButtonArray)
    {
    }

    #endregion

    #region Images

    /// <summary>
    /// Gets an image indicating a sub-menu on a context menu item.
    /// </summary>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public override Image? GetContextMenuSubMenuImage() => _contextMenuSubMenu;

    #endregion

    #region ButtonSpec

    /// <summary>
    /// Gets the image to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <param name="state">State for which image is required.</param>
    /// <returns>Image value.</returns>
    public override Image? GetButtonSpecImage(PaletteButtonSpecStyle style,
        PaletteState state) => style switch
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

    #endregion

    #region Tab Row Background

    /// <inheritdoc />
    public override Color GetRibbonTabRowGradientColor1(PaletteState state) =>
        GlobalStaticValues.TAB_ROW_GRADIENT_FIRST_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) =>
        _tabRowBackgroundGradientRaftingDarkColor;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) =>
        _tabRowBackgroundGradientRaftingLightColor;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

    /// <inheritdoc />
    public override float GetRibbonTabRowGradientRaftingAngle(PaletteState state) => _gradientRafting;

    #endregion

    #region AppButton Colors

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabBottomColor(PaletteState state) => _ribbonAppButtonDarkColor;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTopColor(PaletteState state) => _ribbonAppButtonLightColor;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTextColor(PaletteState state) => _ribbonAppButtonTextColor;

    #endregion
}