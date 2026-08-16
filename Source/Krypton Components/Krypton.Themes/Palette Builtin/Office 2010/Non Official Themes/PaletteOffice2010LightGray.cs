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
/// Office 2010 renderer using the Office 2013 Light Grey chrome colour map.
/// </summary>
public class PaletteOffice2010LightGray : PaletteOffice2010Base
{
    #region Static Fields

    private readonly Color _tabRowBackgroundColor = Color.FromArgb(230, 230, 230);

    private static readonly Color _ribbonAppButtonDarkColor = SharedStaticVariables.DEFAULT_RIBBON_FILE_APP_TAB_BOTTOM_COLOR;
    private static readonly Color _ribbonAppButtonLightColor = SharedStaticVariables.DEFAULT_RIBBON_FILE_APP_TAB_TOP_COLOR;
    private static readonly Color _ribbonAppButtonTextColor = SharedStaticVariables.DEFAULT_RIBBON_FILE_APP_TAB_TEXT_COLOR;

    private static readonly ImageList _checkBoxList;
    private static readonly ImageList _galleryButtonList;
    private static readonly Image?[] _radioButtonArray;
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
    private static readonly Image _formHelpNormal = Office2010ControlBoxResources.Office2010HelpIconNormal;
    private static readonly Image _formHelpActive = Office2010ControlBoxResources.Office2010HelpIconHover;
    private static readonly Image _formHelpPressed = Office2010ControlBoxResources.Office2010HelpIconPressed;
    private static readonly Image _formHelpDisabled = Office2010ControlBoxResources.Office2010HelpIconDisabled;

    #endregion

    static PaletteOffice2010LightGray()
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
            TransparentColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR
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
    /// Initialize a new instance of the <see cref="PaletteOffice2010LightGray"/> class.
    /// </summary>
    public PaletteOffice2010LightGray()
        : base(
            new PaletteOffice2013LightGray_BaseScheme(),
            _checkBoxList,
            _galleryButtonList,
            _radioButtonArray)
    {
    }

    /// <inheritdoc />
    public override Image? GetContextMenuSubMenuImage() => _contextMenuSubMenu;

    /// <inheritdoc />
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
