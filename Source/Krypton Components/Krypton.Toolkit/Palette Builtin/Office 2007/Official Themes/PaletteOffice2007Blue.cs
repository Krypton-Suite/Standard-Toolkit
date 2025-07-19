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
/// Provides the Blue color scheme variant of the Office 2007 palette.
/// </summary>
public class PaletteOffice2007Blue : PaletteOffice2007Base
{
    #region Static Fields

    #region Image List

    private static readonly ImageList _checkBoxList;
    private static readonly ImageList _galleryButtonList;

    #endregion

    #region Image Array

    private static readonly Image?[] _radioButtonArray;

    #endregion

    #region Images

    private static readonly Image? _blueDropDownButton = GenericImageResources.BlueDropDownButton;
    private static readonly Image _blueCloseNormal = Office2007ControlBoxResources.Office2007ControlBoxBlueCloseNormal;
    private static readonly Image _blueCloseActive = Office2007ControlBoxResources.Office2007ControlBoxBlueCloseActive;
    private static readonly Image _blueCloseDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlueCloseDisabled;
    private static readonly Image _blueClosePressed = Office2007ControlBoxResources.Office2007ControlBoxBlueClosePressed;
    private static readonly Image _blueMaximiseNormal = Office2007ControlBoxResources.Office2007ControlBoxBlueMaximiseNormal;
    private static readonly Image _blueMaximiseActive = Office2007ControlBoxResources.Office2007ControlBoxBlueMaximiseActive;
    private static readonly Image _blueMaximiseDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlueMaximiseDisabled;
    private static readonly Image _blueMaximisePressed = Office2007ControlBoxResources.Office2007ControlBoxBlueMaximisePressed;
    private static readonly Image _blueMinimiseNormal = Office2007ControlBoxResources.Office2007ControlBoxBlueMinimiseNormal;
    private static readonly Image _blueMinimiseActive = Office2007ControlBoxResources.Office2007ControlBoxBlueMinimiseActive;
    private static readonly Image _blueMinimiseDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlueMinimiseDisabled;
    private static readonly Image _blueMinimisePressed = Office2007ControlBoxResources.Office2007ControlBoxBlueMinimisePessed;
    private static readonly Image _blueRestoreNormal = Office2007ControlBoxResources.Office2007ControlBoxBlueRestoreNormal;
    private static readonly Image _blueRestoreActive = Office2007ControlBoxResources.Office2007ControlBoxBlueRestoreActive;
    private static readonly Image _blueRestoreDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlueRestoreDisabled;
    private static readonly Image _blueRestorePressed = Office2007ControlBoxResources.Office2007ControlBoxBlueRestorePressed;
    private static readonly Image _blueHelpNormal = Office2007ControlBoxResources.Office2007HelpIconNormal;
    private static readonly Image _blueHelpActive = Office2007ControlBoxResources.Office2007HelpIconHover;
    private static readonly Image _blueHelpDisabled = Office2007ControlBoxResources.Office2007HelpIconDisabled;
    private static readonly Image _blueHelpPressed = Office2007ControlBoxResources.Office2007HelpIconPressed;
    private static readonly Image? _contextMenuSubMenu = GenericImageResources.BlueContextMenuSub;

    #endregion

    #region Color Arrays

    #endregion

    #endregion

    #region Identity
    static PaletteOffice2007Blue()
    {
        _checkBoxList = new ImageList
        {
            ImageSize = new Size(13, 13),
            ColorDepth = ColorDepth.Depth24Bit
        };
        _checkBoxList.Images.AddStrip(CheckBoxStripResources.CheckBoxStrip2007Blue);
        _galleryButtonList = new ImageList
        {
            ImageSize = new Size(13, 7),
            ColorDepth = ColorDepth.Depth24Bit,
            TransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR
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
    /// Initialize a new instance of the PaletteOffice2007Blue class.
    /// </summary>
    public PaletteOffice2007Blue()
        : base(
        "Office 2007 - Blue",
        new PaletteOffice2007Blue_BaseScheme(),
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
        PaletteBackStyle.HeaderForm => PaletteColorStyle.Rounding4,
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
            PaletteState.Disabled => _blueCloseDisabled,
            PaletteState.Tracking => _blueCloseActive,
            PaletteState.Pressed => _blueClosePressed,
            _ => _blueCloseNormal
        },
        PaletteButtonSpecStyle.FormMin => state switch
        {
            PaletteState.Disabled => _blueMinimiseDisabled,
            PaletteState.Tracking => _blueMinimiseActive,
            PaletteState.Pressed => _blueMinimisePressed,
            _ => _blueMinimiseNormal
        },
        PaletteButtonSpecStyle.FormMax => state switch
        {
            PaletteState.Disabled => _blueMaximiseDisabled,
            PaletteState.Tracking => _blueMaximiseActive,
            PaletteState.Pressed => _blueMaximisePressed,
            _ => _blueMaximiseNormal
        },
        PaletteButtonSpecStyle.FormRestore => state switch
        {
            PaletteState.Disabled => _blueRestoreDisabled,
            PaletteState.Tracking => _blueRestoreActive,
            PaletteState.Pressed => _blueRestorePressed,
            _ => _blueRestoreNormal
        },
        PaletteButtonSpecStyle.FormHelp => state switch
        {
            PaletteState.Disabled => _blueHelpDisabled,
            PaletteState.Tracking => _blueHelpActive,
            PaletteState.Pressed => _blueHelpPressed,
            _ => _blueHelpNormal
        },
        _ => base.GetButtonSpecImage(style, state)
    };
    #endregion

    #region RibbonBack
    /// <summary>
    /// Gets the method used to draw the background of a ribbon item.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteRibbonBackStyle value.</returns>
    public override PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteRibbonBackStyle style, PaletteState state)
    {
        switch (style)
        {
            case PaletteRibbonBackStyle.RibbonGroupArea:
                if (state == PaletteState.CheckedNormal)
                {
                    return PaletteRibbonColorStyle.RibbonGroupAreaBorder;
                }

                break;
            case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
                if (state == PaletteState.Tracking)
                {
                    return PaletteRibbonColorStyle.RibbonGroupNormalBorderTrackingLight;
                }

                break;
        }

        return base.GetRibbonBackColorStyle(style, state);
    }
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