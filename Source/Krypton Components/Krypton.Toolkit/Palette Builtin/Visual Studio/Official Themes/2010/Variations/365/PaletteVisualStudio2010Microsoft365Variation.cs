#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Provides the Visual Studio 2010 colour theme, based on the Microsoft 365 renderer.</summary>
/// <seealso cref="Krypton.Toolkit.PaletteVisualStudio2010With365Base" />
public class PaletteVisualStudio2010Microsoft365Variation : PaletteVisualStudio2010With365Base
{
    #region Static Fields

    #region Color

    private readonly Color _tabRowBackgroundColor = Color.FromArgb(188, 199, 216);

    #endregion

    #region Ribbon Specific Colors

    private static readonly Color _ribbonAppButtonDarkColor = Color.FromArgb(16, 22, 33);

    private static readonly Color _ribbonAppButtonLightColor = Color.FromArgb(53, 73, 109);

    private static readonly Color _ribbonAppButtonTextColor = Color.White;

    #endregion

    #region Image Lists

    private static readonly ImageList _checkBoxList;
    private static readonly ImageList _galleryButtonList;

    #endregion

    #region Images

    private static readonly Image?[] _radioButtonArray;
    private static readonly Image? _blueDropDownButton = Office2010ArrowResources.Office2010BlueDropDownButton;
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

    #endregion

    #region Colour Arrays

    #endregion

    #endregion

    #region Identity

    static PaletteVisualStudio2010Microsoft365Variation()
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
            TransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR
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

    /// <summary>Initializes a new instance of the <see cref="PaletteVisualStudio2010Microsoft365Variation" /> class.</summary>
    public PaletteVisualStudio2010Microsoft365Variation()
        : base(
        new PaletteVisualStudio2010Microsoft365Variation_BaseScheme(),
        _checkBoxList,
        _galleryButtonList,
        _radioButtonArray)
    {
        ThemeName = nameof(PaletteVisualStudio2010Microsoft365Variation);
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