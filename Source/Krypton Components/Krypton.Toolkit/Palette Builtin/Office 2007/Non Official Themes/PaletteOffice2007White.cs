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

public class PaletteOffice2007White : PaletteOffice2007Base
{
    #region Static Fields

    #region Image Lists

    private static readonly ImageList _checkBoxList;
    private static readonly ImageList _galleryButtonList;

    #endregion

    #region Image Array

    private static readonly Image?[] _radioButtonArray;

    #endregion

    #region Images

    private static readonly Image? _silverDropDownButton = GenericImageResources.SilverDropDownButton;
    private static readonly Image _silverCloseNormal = Office2007ControlBoxResources.Office2007ControlBoxSilverCloseNormal;
    private static readonly Image _silverCloseActive = Office2007ControlBoxResources.Office2007ControlBoxSilverCloseActive;
    private static readonly Image _silverCloseDisabled = Office2007ControlBoxResources.Office2007ControlBoxSilverCloseDisabled;
    private static readonly Image _silverClosePressed = Office2007ControlBoxResources.Office2007ControlBoxSilverClosePressed;
    private static readonly Image _silverMaximiseNormal = Office2007ControlBoxResources.Office2007ControlBoxSilverMaximiseNormal;
    private static readonly Image _silverMaximiseActive = Office2007ControlBoxResources.Office2007ControlBoxSilverMaximiseActive;
    private static readonly Image _silverMaximiseDisabled = Office2007ControlBoxResources.Office2007ControlBoxSilverMaximiseDisabled;
    private static readonly Image _silverMaximisePressed = Office2007ControlBoxResources.Office2007ControlBoxSilverMaximisePressed;
    private static readonly Image _silverMinimiseNormal = Office2007ControlBoxResources.Office2007ControlBoxSilverMinimiseNormal;
    private static readonly Image _silverMinimiseActive = Office2007ControlBoxResources.Office2007ControlBoxSilverMinimiseActive;
    private static readonly Image _silverMinimiseDisabled = Office2007ControlBoxResources.Office2007ControlBoxSilverMinimiseDisabled;
    private static readonly Image _silverMinimisePressed = Office2007ControlBoxResources.Office2007ControlBoxSilverMinimisePessed;
    private static readonly Image _silverRestoreNormal = Office2007ControlBoxResources.Office2007ControlBoxSilverRestoreNormal;
    private static readonly Image _silverRestoreActive = Office2007ControlBoxResources.Office2007ControlBoxSilverRestoreActive;
    private static readonly Image _silverRestoreDisabled = Office2007ControlBoxResources.Office2007ControlBoxSilverRestoreDisabled;
    private static readonly Image _silverRestorePressed = Office2007ControlBoxResources.Office2007ControlBoxSilverRestorePressed;
    private static readonly Image _silverHelpNormal = Office2007ControlBoxResources.Office2007HelpIconNormal;
    private static readonly Image _silverHelpActive = Office2007ControlBoxResources.Office2007HelpIconHover;
    private static readonly Image _silverHelpDisabled = Office2007ControlBoxResources.Office2007HelpIconDisabled;
    private static readonly Image _silverHelpPressed = Office2007ControlBoxResources.Office2007HelpIconPressed;
    private static readonly Image? _contextMenuSubMenu = GenericImageResources.SilverContextMenuSub;

    #endregion

    #region Color Arrays

    #endregion
    #endregion

    #region Identity
    static PaletteOffice2007White()
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
            TransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR
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
    /// Initialize a new instance of the PaletteOffice2007Silver class.
    /// </summary>
    public PaletteOffice2007White()
        : base(
        "Office 2007 - White",
        new PaletteOffice2007White_BaseScheme(),
        _checkBoxList,
        _galleryButtonList,
        _radioButtonArray)
    {
    }
    #endregion

    #region Back
    /// <summary>
    /// Gets the color background drawing style.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state) => style switch
    {
        PaletteBackStyle.HeaderForm => PaletteColorStyle.Rounding2,
        _ => base.GetBackColorStyle(style, state)
    };
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
            PaletteState.Disabled => _silverCloseDisabled,
            PaletteState.Tracking => _silverCloseActive,
            PaletteState.Pressed => _silverClosePressed,
            _ => _silverCloseNormal
        },
        PaletteButtonSpecStyle.FormMin => state switch
        {
            PaletteState.Disabled => _silverMinimiseDisabled,
            PaletteState.Tracking => _silverMinimiseActive,
            PaletteState.Pressed => _silverMinimisePressed,
            _ => _silverMinimiseNormal
        },
        PaletteButtonSpecStyle.FormMax => state switch
        {
            PaletteState.Disabled => _silverMaximiseDisabled,
            PaletteState.Tracking => _silverMaximiseActive,
            PaletteState.Pressed => _silverMaximisePressed,
            _ => _silverMaximiseNormal
        },
        PaletteButtonSpecStyle.FormRestore => state switch
        {
            PaletteState.Disabled => _silverRestoreDisabled,
            PaletteState.Tracking => _silverRestoreActive,
            PaletteState.Pressed => _silverRestorePressed,
            _ => _silverRestoreNormal
        },
        PaletteButtonSpecStyle.FormHelp => state switch
        {
            PaletteState.Disabled => _silverHelpDisabled,
            PaletteState.Tracking => _silverHelpActive,
            PaletteState.Pressed => _silverHelpPressed,
            _ => _silverHelpNormal
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
    public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

    /// <inheritdoc />
    public override float GetRibbonTabRowGradientRaftingAngle(PaletteState state) => -1;

    #endregion

    #region AppButton Colors

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabBottomColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTopColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTextColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

    #endregion
}