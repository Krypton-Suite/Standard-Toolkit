#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege, et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides the White color scheme variant of the Office 2013 palette.
/// </summary>
public class PaletteOffice2013White : PaletteOffice2013WhiteBase
{
    #region Static Fields

    #region Colors

    private readonly Color _tabRowBackgroundColor = Color.White;

    #endregion

    #region Ribbon Specific Colors

    private static readonly Color _ribbonAppButtonDarkColor = GlobalStaticValues.DEFAULT_RIBBON_FILE_APP_TAB_BOTTOM_COLOR;

    private static readonly Color _ribbonAppButtonLightColor = GlobalStaticValues.DEFAULT_RIBBON_FILE_APP_TAB_TOP_COLOR;

    private static readonly Color _ribbonAppButtonTextColor = GlobalStaticValues.DEFAULT_RIBBON_FILE_APP_TAB_TEXT_COLOR;

    #endregion

    #region Image Lists

    private static readonly ImageList _checkBoxList;
    private static readonly ImageList _galleryButtonList;

    #endregion

    #region Image Array

    private static readonly Image?[] _radioButtonArray;

    #endregion

    #region Images

    private static readonly Image? _silverDropDownButton = Office2010ArrowResources.Office2010BlueDropDownButton;
    private static readonly Image? _contextMenuSubMenu = Office2010ArrowResources.Office2010BlueContextMenuSub;
    private static readonly Image _formCloseNormal = Office2013ControlBoxResources.Office2013CloseNormal;
    private static readonly Image _formCloseDisabled = Office2013ControlBoxResources.Office2013CloseDisabled;
    private static readonly Image _formCloseActive = Office2013ControlBoxResources.Office2013CloseActive;
    private static readonly Image _formClosePressed = Office2013ControlBoxResources.Office2013ClosePressed;
    private static readonly Image _formMaximiseNormal = Office2013ControlBoxResources.Office2013MaximiseNormal;
    private static readonly Image _formMaximiseDisabled = Office2013ControlBoxResources.Office2013MaximiseDisabled;
    private static readonly Image _formMaximiseActive = Office2013ControlBoxResources.Office2013MaximiseActive;
    private static readonly Image _formMaximisePressed = Office2013ControlBoxResources.Office2013MaximisePressed;
    private static readonly Image _formMinimiseNormal = Office2013ControlBoxResources.Office2013MinimiseNormal;
    private static readonly Image _formMinimiseActive = Office2013ControlBoxResources.Office2013MinimiseActive;
    private static readonly Image _formMinimiseDisabled = Office2013ControlBoxResources.Office2013MinimiseDisabled;
    private static readonly Image _formMinimisePressed = Office2013ControlBoxResources.Office2013MinimisePressed;
    private static readonly Image _formRestoreNormal = Office2013ControlBoxResources.Office2013RestoreNormal;
    private static readonly Image _formRestoreDisabled = Office2013ControlBoxResources.Office2013RestoreDisabled;
    private static readonly Image _formRestoreActive = Office2013ControlBoxResources.Office2013RestoreActive;
    private static readonly Image _formRestorePressed = Office2013ControlBoxResources.Office2013RestorePressed;
    private static readonly Image _formHelpNormal = Office2013ControlBoxResources.Office2013HelpNormal;
    private static readonly Image _formHelpActive = Office2013ControlBoxResources.Office2013HelpActive;
    private static readonly Image _formHelpPressed = Office2013ControlBoxResources.Office2013HelpPressed;
    private static readonly Image _formHelpDisabled = Office2013ControlBoxResources.Office2013HelpDisabled;

    #endregion

    #region Color Arrays

    #endregion

    #endregion Static Fields

    #region Identity

    static PaletteOffice2013White()
    {
        _checkBoxList = new ImageList
        {
            ImageSize = new Size(13, 13),
            ColorDepth = ColorDepth.Depth24Bit
        };
        _checkBoxList.Images.AddStrip(CheckBoxStripResources.CheckBoxStrip2010Silver);
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
    /// Initialize a new instance of the PaletteOffice2010Silver class.
    /// </summary>
    public PaletteOffice2013White()
        : base(
        new PaletteOffice2013White_BaseScheme(),
        _checkBoxList,
        _galleryButtonList,
        _radioButtonArray)
    {
        ThemeName = nameof(PaletteOffice2013White);
    }

    #endregion Identity

    #region Images

    /// <summary>
    /// Gets an image indicating a sub-menu on a context menu item.
    /// </summary>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public override Image? GetContextMenuSubMenuImage() => _contextMenuSubMenu;

    #endregion Images

    #region ButtonSpec

    /// <summary>
    /// Gets the image to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <param name="state">State for which image is required.</param>
    /// <returns>Image value.</returns>
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

    #endregion ButtonSpec

    #region Tab Row Background

    /// <inheritdoc />
    public override Color GetRibbonTabRowGradientColor1(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) =>
        GlobalStaticValues.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) =>
        GlobalStaticValues.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) => _tabRowBackgroundColor;

    /// <inheritdoc />
    public override float GetRibbonTabRowGradientRaftingAngle(PaletteState state) => -1;

    #endregion Tab Row Background

    #region AppButton Colors

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabBottomColor(PaletteState state) => _ribbonAppButtonDarkColor;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTopColor(PaletteState state) => _ribbonAppButtonLightColor;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTextColor(PaletteState state) => _ribbonAppButtonTextColor;

    #endregion AppButton Colors
}