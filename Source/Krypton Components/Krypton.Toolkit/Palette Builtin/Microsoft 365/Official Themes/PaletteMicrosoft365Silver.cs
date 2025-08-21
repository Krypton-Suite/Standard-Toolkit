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

public class PaletteMicrosoft365Silver : PaletteMicrosoft365Base
{
    #region Static Fields

    #region Colors

    private static readonly Color _tabRowBackgroundColor = Color.FromArgb(227, 230, 232);

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
    private static readonly Image _formHelpNormal = Microsoft365ControlBoxResources.Microsoft365HelpIconNormal;
    private static readonly Image _formHelpActive = Microsoft365ControlBoxResources.Microsoft365HelpIconHover;
    private static readonly Image _formHelpPressed = Microsoft365ControlBoxResources.Microsoft365HelpIconPressed;
    private static readonly Image _formHelpDisabled = Microsoft365ControlBoxResources.Microsoft365HelpIconDisabled;

    #region Integrated Toolbar Images

    private static readonly Image _integratedToolbarNewNormal = Office2019ToolbarImageResources.Office2019ToolbarNewNormal;

    private static readonly Image _integratedToolbarNewDisabled = Office2019ToolbarImageResources.Office2019ToolbarNewDisabled;

    private static readonly Image _integratedToolbarOpenNormal = Office2019ToolbarImageResources.Office2019ToolbarOpenNormal;

    private static readonly Image _integratedToolbarOpenDisabled = Office2019ToolbarImageResources.Office2019ToolbarOpenDisabled;

    private static readonly Image _integratedToolbarSaveAllNormal = Office2019ToolbarImageResources.Office2019ToolbarSaveAllNormal;

    private static readonly Image _integratedToolbarSaveAllDisabled = Office2019ToolbarImageResources.Office2019ToolbarSaveAllDisabled;

    private static readonly Image _integratedToolbarSaveAsNormal = Office2019ToolbarImageResources.Office2019ToolbarSaveAsNormal;

    private static readonly Image _integratedToolbarSaveAsDisabled = Office2019ToolbarImageResources.Office2019ToolbarSaveAsDisabled;

    private static readonly Image _integratedToolbarSaveNormal = Office2019ToolbarImageResources.Office2019ToolbarSaveNormal;

    private static readonly Image _integratedToolbarSaveDisabled = Office2019ToolbarImageResources.Office2019ToolbarSaveDisabled;

    private static readonly Image _integratedToolbarCutNormal = Office2019ToolbarImageResources.Office2019ToolbarCutNormal;

    private static readonly Image _integratedToolbarCutDisabled = Office2019ToolbarImageResources.Office2019ToolbarCutDisabled;

    private static readonly Image _integratedToolbarCopyNormal = Office2019ToolbarImageResources.Office2019ToolbarCopyNormal;

    private static readonly Image _integratedToolbarCopyDisabled = Office2019ToolbarImageResources.Office2019ToolbarCopyDisabled;

    private static readonly Image _integratedToolbarPasteNormal = Office2019ToolbarImageResources.Office2019ToolbarPasteNormal;

    private static readonly Image _integratedToolbarPasteDisabled = Office2019ToolbarImageResources.Office2019ToolbarPasteDisabled;

    private static readonly Image _integratedToolbarUndoNormal = Office2019ToolbarImageResources.Office2019ToolbarUndoNormal;

    private static readonly Image _integratedToolbarUndoDisabled = Office2019ToolbarImageResources.Office2019ToolbarUndoDisabled;

    private static readonly Image _integratedToolbarRedoNormal = Office2019ToolbarImageResources.Office2019ToolbarRedoNormal;

    private static readonly Image _integratedToolbarRedoDisabled = Office2019ToolbarImageResources.Office2019ToolbarRedoDisabled;

    private static readonly Image _integratedToolbarPageSetupNormal = Office2019ToolbarImageResources.Office2019ToolbarPageSetupNormal;

    private static readonly Image _integratedToolbarPageSetupDisabled = Office2019ToolbarImageResources.Office2019ToolbarPageSetupDisabled;

    private static readonly Image _integratedToolbarPrintPreviewNormal = Office2019ToolbarImageResources.Office2019ToolbarPrintPreviewNormal;

    private static readonly Image _integratedToolbarPrintPreviewDisabled = Office2019ToolbarImageResources.Office2019ToolbarPrintPreviewDisabled;

    private static readonly Image _integratedToolbarPrintNormal = Office2019ToolbarImageResources.Office2019ToolbarPrintNormal;

    private static readonly Image _integratedToolbarPrintDisabled = Office2019ToolbarImageResources.Office2019ToolbarPrintDisabled;

    private static readonly Image _integratedToolbarQuickPrintNormal = Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal;

    private static readonly Image _integratedToolbarQuickPrintDisabled = Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled;

    #endregion

    #endregion

    #endregion Static Fields

    #region Constructors

    static PaletteMicrosoft365Silver()
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
    /// Initializes a new instance of the <see cref="PaletteMicrosoft365Silver"/> class.
    /// </summary>
    public PaletteMicrosoft365Silver() : base(
        new PaletteMicrosoft365Silver_BaseScheme(),
        _checkBoxList,
        _galleryButtonList,
        _radioButtonArray)
    {
    }

    #endregion Constructors

    #region Images

    /// <summary>
    /// Gets an image indicating a sub-menu on a context menu item.
    /// </summary>
    /// <returns>
    /// Appropriate image for drawing; otherwise null.
    /// </returns>
    public override Image? GetContextMenuSubMenuImage() => _contextMenuSubMenu;

    #endregion

    #region ButtonSpec

    /// <summary>
    /// Gets the image to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <param name="state">State for which image is required.</param>
    /// <returns>
    /// Image value.
    /// </returns>
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
        PaletteButtonSpecStyle.New => state switch
        {
            PaletteState.Normal => _integratedToolbarNewNormal,
            PaletteState.Disabled => _integratedToolbarNewDisabled,
            _ => _integratedToolbarNewDisabled
        },
        PaletteButtonSpecStyle.Open => state switch
        {
            PaletteState.Normal => _integratedToolbarOpenNormal,
            PaletteState.Disabled => _integratedToolbarOpenDisabled,
            _ => _integratedToolbarOpenDisabled
        },
        PaletteButtonSpecStyle.SaveAll => state switch
        {
            PaletteState.Normal => _integratedToolbarSaveAllNormal,
            PaletteState.Disabled => _integratedToolbarSaveAllDisabled,
            _ => _integratedToolbarSaveAllDisabled
        },
        PaletteButtonSpecStyle.SaveAs => state switch
        {
            PaletteState.Normal => _integratedToolbarSaveAsNormal,
            PaletteState.Disabled => _integratedToolbarSaveAsDisabled,
            _ => _integratedToolbarSaveAsDisabled
        },
        PaletteButtonSpecStyle.Save => state switch
        {
            PaletteState.Normal => _integratedToolbarSaveNormal,
            PaletteState.Disabled => _integratedToolbarSaveDisabled,
            _ => _integratedToolbarSaveDisabled
        },
        PaletteButtonSpecStyle.Cut => state switch
        {
            PaletteState.Normal => _integratedToolbarCutNormal,
            PaletteState.Disabled => _integratedToolbarCutDisabled,
            _ => _integratedToolbarCutDisabled
        },
        PaletteButtonSpecStyle.Copy => state switch
        {
            PaletteState.Normal => _integratedToolbarCopyNormal,
            PaletteState.Disabled => _integratedToolbarCopyDisabled,
            _ => _integratedToolbarCopyDisabled
        },
        PaletteButtonSpecStyle.Paste => state switch
        {
            PaletteState.Normal => _integratedToolbarPasteNormal,
            PaletteState.Disabled => _integratedToolbarPasteDisabled,
            _ => _integratedToolbarPasteDisabled
        },
        PaletteButtonSpecStyle.Undo => state switch
        {
            PaletteState.Normal => _integratedToolbarUndoNormal,
            PaletteState.Disabled => _integratedToolbarUndoDisabled,
            _ => _integratedToolbarUndoDisabled
        },
        PaletteButtonSpecStyle.Redo => state switch
        {
            PaletteState.Normal => _integratedToolbarRedoNormal,
            PaletteState.Disabled => _integratedToolbarRedoDisabled,
            _ => _integratedToolbarRedoDisabled
        },
        PaletteButtonSpecStyle.PageSetup => state switch
        {
            PaletteState.Normal => _integratedToolbarPageSetupNormal,
            PaletteState.Disabled => _integratedToolbarPageSetupDisabled,
            _ => _integratedToolbarPageSetupDisabled
        },
        PaletteButtonSpecStyle.PrintPreview => state switch
        {
            PaletteState.Normal => _integratedToolbarPrintPreviewNormal,
            PaletteState.Disabled => _integratedToolbarPrintPreviewDisabled,
            _ => _integratedToolbarPrintPreviewDisabled
        },
        PaletteButtonSpecStyle.Print => state switch
        {
            PaletteState.Normal => _integratedToolbarPrintNormal,
            PaletteState.Disabled => _integratedToolbarPrintDisabled,
            _ => _integratedToolbarPrintDisabled
        },
        PaletteButtonSpecStyle.QuickPrint => state switch
        {
            PaletteState.Normal => _integratedToolbarQuickPrintNormal,
            PaletteState.Disabled => _integratedToolbarQuickPrintDisabled,
            _ => _integratedToolbarQuickPrintDisabled
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