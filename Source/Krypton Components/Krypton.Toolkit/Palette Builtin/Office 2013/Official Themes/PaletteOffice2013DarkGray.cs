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
/// Provides the dark gray color scheme variant of the Office 2013 palette.
/// </summary>
public class PaletteOffice2013DarkGray : PaletteOffice2013Base
{
    #region Static Fields

    #region Colors

    private readonly Color _tabRowBackgroundColor = Color.FromArgb(212, 212, 212);

    #endregion

    #region Ribbon Specific Colors

    private static readonly Color _ribbonAppButtonDarkColor = Color.FromArgb(51, 51, 51);

    private static readonly Color _ribbonAppButtonLightColor = Color.FromArgb(140, 140, 140);

    private static readonly Color _ribbonAppButtonTextColor = SystemColors.Control;

    #endregion

    #region Image Lists

    private static readonly ImageList _checkBoxList;
    private static readonly ImageList _galleryButtonList;

    #endregion

    #region Images

    private static readonly Image?[] _radioButtonArray;
    private static readonly Image? _silverDropDownButton = Office2010ArrowResources.Office2010BlueDropDownButton;
    private static readonly Image? _contextMenuSubMenu = Office2010ArrowResources.Office2010BlueContextMenuSub;
    private static readonly Image _formCloseNormal = Office2010ControlBoxResources.Office2010SilverCloseNormal;
    private static readonly Image _formCloseDisabled = Office2010ControlBoxResources.Office2010SilverCloseDisabled;
    private static readonly Image _formCloseActive = Office2010ControlBoxResources.Office2010SilverCloseActive;
    private static readonly Image _formClosePressed = Office2010ControlBoxResources.Office2010SilverClosePressed;
    private static readonly Image _formMaximiseNormal = Office2010ControlBoxResources.Office2010SilverMaximiseNormal;
    private static readonly Image _formMaximiseDisabled = Office2010ControlBoxResources.Office2010SilverMaximiseDisabled;
    private static readonly Image _formMaximiseActive = Office2010ControlBoxResources.Office2010SilverMaximiseActive;
    private static readonly Image _formMaximisePressed = Office2010ControlBoxResources.Office2010SilverMaximisePressed;
    private static readonly Image _formMinimiseNormal = Office2010ControlBoxResources.Office2010SilverMinimiseNormal;
    private static readonly Image _formMinimiseActive = Office2010ControlBoxResources.Office2010SilverMinimiseActive;
    private static readonly Image _formMinimiseDisabled = Office2010ControlBoxResources.Office2010SilverMinimiseDisabled;
    private static readonly Image _formMinimisePressed = Office2010ControlBoxResources.Office2010SilverMinimisePressed;
    private static readonly Image _formRestoreNormal = Office2010ControlBoxResources.Office2010SilverRestoreNormal;
    private static readonly Image _formRestoreDisabled = Office2010ControlBoxResources.Office2010SilverRestoreDisabled;
    private static readonly Image _formRestoreActive = Office2010ControlBoxResources.Office2010SilverRestoreActive;
    private static readonly Image _formRestorePressed = Office2010ControlBoxResources.Office2010SilverRestorePressed;
    private static readonly Image _formHelpNormal = Office2010ControlBoxResources.Office2010HelpIconNormal;
    private static readonly Image _formHelpActive = Office2010ControlBoxResources.Office2010HelpIconHover;
    private static readonly Image _formHelpPressed = Office2010ControlBoxResources.Office2010HelpIconPressed;
    private static readonly Image _formHelpDisabled = Office2010ControlBoxResources.Office2010HelpIconDisabled;

    #endregion

    #region Colour Arrays

    private static readonly Color[]? _trackBarColors =
    [
        Color.FromArgb(170, 170, 170), // Tick marks
        Color.FromArgb(166, 170, 175), // Top track
        Color.FromArgb(226, 220, 235), // Bottom track
        Color.FromArgb(206, 200, 215), // Fill track
        Color.FromArgb(64, Color.White), // Outside position
        Color.FromArgb(80, 81, 82) // Border (normal) position
    ];

    #endregion
    #endregion

    #region Identity
    static PaletteOffice2013DarkGray()
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
    /// Initialize a new instance of the PaletteOffice2013DarkGray class.
    /// </summary>
    public PaletteOffice2013DarkGray()
        : base(
        new PaletteOffice2013DarkGray_BaseScheme(),
        _checkBoxList,
        _galleryButtonList,
        _radioButtonArray)
    {
        ThemeName = nameof(PaletteOffice2013DarkGray);
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