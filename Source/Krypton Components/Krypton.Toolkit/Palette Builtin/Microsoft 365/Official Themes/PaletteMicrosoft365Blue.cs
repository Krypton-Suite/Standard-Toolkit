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

public class PaletteMicrosoft365Blue : PaletteMicrosoft365Base
{
    #region Static Fields

    #region Colors

    private static readonly Color _tabRowBackgroundColor = Color.FromArgb(187, 206, 230);

    #endregion

    #region Ribbon Specific Colors

    private static readonly Color _ribbonAppButtonDarkColor = SharedStaticVariables.DEFAULT_RIBBON_FILE_APP_TAB_BOTTOM_COLOR;

    private static readonly Color _ribbonAppButtonLightColor = SharedStaticVariables.DEFAULT_RIBBON_FILE_APP_TAB_TOP_COLOR;

    private static readonly Color _ribbonAppButtonTextColor = SharedStaticVariables.DEFAULT_RIBBON_FILE_APP_TAB_TEXT_COLOR;

    #endregion

    #region Image Lists

    // The base constructor needs a list. The real strips load the first time a checkbox, radio, or gallery asks.
    private static readonly ImageList _deferredImageList = new ImageList();
    private static readonly Image?[] _deferredRadioButtons = new Image?[8];

    private static ImageList? _checkBoxListCache;
    private static ImageList CheckBoxList
    {
        get
        {
            if (_checkBoxListCache == null)
            {
                var list = new ImageList
                {
                    ImageSize = new Size(13, 13),
                    ColorDepth = ColorDepth.Depth24Bit
                };
                list.Images.AddStrip(CheckBoxStripResources.CheckBoxStrip2010Blue);
                _checkBoxListCache = list;
            }

            return _checkBoxListCache;
        }
    }

    private static ImageList? _galleryButtonListCache;
    private static ImageList GalleryButtonList
    {
        get
        {
            if (_galleryButtonListCache == null)
            {
                var list = new ImageList
                {
                    ImageSize = new Size(13, 7),
                    ColorDepth = ColorDepth.Depth24Bit,
                    TransparentColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR
                };
                list.Images.AddStrip(GalleryImageResources.Gallery2010);
                _galleryButtonListCache = list;
            }

            return _galleryButtonListCache;
        }
    }

    private static Image?[]? _radioButtonArrayCache;
    private static Image?[] RadioButtonArray => _radioButtonArrayCache ??=
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

    #endregion

    #region Images

    // Caption glyphs stay eager so the first form can paint them. The context-menu arrow and the
    // integrated-toolbar strip wait until a caller asks, so a plain form does not decode them at type init.
    private static Image? _contextMenuSubMenuCache;
    private static Image? _contextMenuSubMenu => _contextMenuSubMenuCache ??= Office2010ArrowResources.Office2010BlueContextMenuSub;

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
    private static readonly Image _formHelpNormal = Microsoft365ControlBoxResources.Microsoft365HelpIconNormal;
    private static readonly Image _formHelpActive = Microsoft365ControlBoxResources.Microsoft365HelpIconHover;
    private static readonly Image _formHelpPressed = Microsoft365ControlBoxResources.Microsoft365HelpIconPressed;
    private static readonly Image _formHelpDisabled = Microsoft365ControlBoxResources.Microsoft365HelpIconDisabled;

    #region Integrated Toolbar Images

    private static Image? _integratedToolbarNewNormalCache;
    private static Image _integratedToolbarNewNormal => _integratedToolbarNewNormalCache ??= Office2019ToolbarImageResources.Office2019ToolbarNewNormal;

    private static Image? _integratedToolbarNewDisabledCache;
    private static Image _integratedToolbarNewDisabled => _integratedToolbarNewDisabledCache ??= Office2019ToolbarImageResources.Office2019ToolbarNewDisabled;

    private static Image? _integratedToolbarOpenNormalCache;
    private static Image _integratedToolbarOpenNormal => _integratedToolbarOpenNormalCache ??= Office2019ToolbarImageResources.Office2019ToolbarOpenNormal;

    private static Image? _integratedToolbarOpenDisabledCache;
    private static Image _integratedToolbarOpenDisabled => _integratedToolbarOpenDisabledCache ??= Office2019ToolbarImageResources.Office2019ToolbarOpenDisabled;

    private static Image? _integratedToolbarSaveAllNormalCache;
    private static Image _integratedToolbarSaveAllNormal => _integratedToolbarSaveAllNormalCache ??= Office2019ToolbarImageResources.Office2019ToolbarSaveAllNormal;

    private static Image? _integratedToolbarSaveAllDisabledCache;
    private static Image _integratedToolbarSaveAllDisabled => _integratedToolbarSaveAllDisabledCache ??= Office2019ToolbarImageResources.Office2019ToolbarSaveAllDisabled;

    private static Image? _integratedToolbarSaveAsNormalCache;
    private static Image _integratedToolbarSaveAsNormal => _integratedToolbarSaveAsNormalCache ??= Office2019ToolbarImageResources.Office2019ToolbarSaveAsNormal;

    private static Image? _integratedToolbarSaveAsDisabledCache;
    private static Image _integratedToolbarSaveAsDisabled => _integratedToolbarSaveAsDisabledCache ??= Office2019ToolbarImageResources.Office2019ToolbarSaveAsDisabled;

    private static Image? _integratedToolbarSaveNormalCache;
    private static Image _integratedToolbarSaveNormal => _integratedToolbarSaveNormalCache ??= Office2019ToolbarImageResources.Office2019ToolbarSaveNormal;

    private static Image? _integratedToolbarSaveDisabledCache;
    private static Image _integratedToolbarSaveDisabled => _integratedToolbarSaveDisabledCache ??= Office2019ToolbarImageResources.Office2019ToolbarSaveDisabled;

    private static Image? _integratedToolbarCutNormalCache;
    private static Image _integratedToolbarCutNormal => _integratedToolbarCutNormalCache ??= Office2019ToolbarImageResources.Office2019ToolbarCutNormal;

    private static Image? _integratedToolbarCutDisabledCache;
    private static Image _integratedToolbarCutDisabled => _integratedToolbarCutDisabledCache ??= Office2019ToolbarImageResources.Office2019ToolbarCutDisabled;

    private static Image? _integratedToolbarCopyNormalCache;
    private static Image _integratedToolbarCopyNormal => _integratedToolbarCopyNormalCache ??= Office2019ToolbarImageResources.Office2019ToolbarCopyNormal;

    private static Image? _integratedToolbarCopyDisabledCache;
    private static Image _integratedToolbarCopyDisabled => _integratedToolbarCopyDisabledCache ??= Office2019ToolbarImageResources.Office2019ToolbarCopyDisabled;

    private static Image? _integratedToolbarPasteNormalCache;
    private static Image _integratedToolbarPasteNormal => _integratedToolbarPasteNormalCache ??= Office2019ToolbarImageResources.Office2019ToolbarPasteNormal;

    private static Image? _integratedToolbarPasteDisabledCache;
    private static Image _integratedToolbarPasteDisabled => _integratedToolbarPasteDisabledCache ??= Office2019ToolbarImageResources.Office2019ToolbarPasteDisabled;

    private static Image? _integratedToolbarUndoNormalCache;
    private static Image _integratedToolbarUndoNormal => _integratedToolbarUndoNormalCache ??= Office2019ToolbarImageResources.Office2019ToolbarUndoNormal;

    private static Image? _integratedToolbarUndoDisabledCache;
    private static Image _integratedToolbarUndoDisabled => _integratedToolbarUndoDisabledCache ??= Office2019ToolbarImageResources.Office2019ToolbarUndoDisabled;

    private static Image? _integratedToolbarRedoNormalCache;
    private static Image _integratedToolbarRedoNormal => _integratedToolbarRedoNormalCache ??= Office2019ToolbarImageResources.Office2019ToolbarRedoNormal;

    private static Image? _integratedToolbarRedoDisabledCache;
    private static Image _integratedToolbarRedoDisabled => _integratedToolbarRedoDisabledCache ??= Office2019ToolbarImageResources.Office2019ToolbarRedoDisabled;

    private static Image? _integratedToolbarPageSetupNormalCache;
    private static Image _integratedToolbarPageSetupNormal => _integratedToolbarPageSetupNormalCache ??= Office2019ToolbarImageResources.Office2019ToolbarPageSetupNormal;

    private static Image? _integratedToolbarPageSetupDisabledCache;
    private static Image _integratedToolbarPageSetupDisabled => _integratedToolbarPageSetupDisabledCache ??= Office2019ToolbarImageResources.Office2019ToolbarPageSetupDisabled;

    private static Image? _integratedToolbarPrintPreviewNormalCache;
    private static Image _integratedToolbarPrintPreviewNormal => _integratedToolbarPrintPreviewNormalCache ??= Office2019ToolbarImageResources.Office2019ToolbarPrintPreviewNormal;

    private static Image? _integratedToolbarPrintPreviewDisabledCache;
    private static Image _integratedToolbarPrintPreviewDisabled => _integratedToolbarPrintPreviewDisabledCache ??= Office2019ToolbarImageResources.Office2019ToolbarPrintPreviewDisabled;

    private static Image? _integratedToolbarPrintNormalCache;
    private static Image _integratedToolbarPrintNormal => _integratedToolbarPrintNormalCache ??= Office2019ToolbarImageResources.Office2019ToolbarPrintNormal;

    private static Image? _integratedToolbarPrintDisabledCache;
    private static Image _integratedToolbarPrintDisabled => _integratedToolbarPrintDisabledCache ??= Office2019ToolbarImageResources.Office2019ToolbarPrintDisabled;

    private static Image? _integratedToolbarQuickPrintNormalCache;
    private static Image _integratedToolbarQuickPrintNormal => _integratedToolbarQuickPrintNormalCache ??= Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal;

    private static Image? _integratedToolbarQuickPrintDisabledCache;
    private static Image _integratedToolbarQuickPrintDisabled => _integratedToolbarQuickPrintDisabledCache ??= Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled;

    #endregion Integrated Toolbar Images

    #endregion Images

    #endregion Static Fields

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the PaletteMicrosoft365Blue class.
    /// </summary>
    public PaletteMicrosoft365Blue()
        : base(new PaletteMicrosoft365Blue_BaseScheme(), _deferredImageList, _deferredImageList, _deferredRadioButtons)
    {
    }

    #endregion

    #region Images

    /// <inheritdoc />
    public override Image? GetCheckBoxImage(bool enabled, CheckState checkState, bool tracking, bool pressed)
    {
        var images = CheckBoxList.Images;
        switch (checkState)
        {
            default:
            case CheckState.Unchecked:
                if (!enabled)
                {
                    return images[0];
                }

                return pressed ? images[3] : (tracking ? images[2] : images[1]);

            case CheckState.Checked:
                if (!enabled)
                {
                    return images[4];
                }

                return pressed ? images[7] : (tracking ? images[6] : images[5]);

            case CheckState.Indeterminate:
                if (!enabled)
                {
                    return images[8];
                }

                return pressed ? images[11] : (tracking ? images[10] : images[9]);
        }
    }

    /// <inheritdoc />
    public override Image? GetRadioButtonImage(bool enabled, bool checkState, bool tracking, bool pressed)
    {
        var images = RadioButtonArray;
        if (!checkState)
        {
            if (!enabled)
            {
                return images[0];
            }

            return pressed ? images[3] : (tracking ? images[2] : images[1]);
        }

        if (!enabled)
        {
            return images[4];
        }

        return pressed ? images[7] : (tracking ? images[6] : images[5]);
    }

    /// <inheritdoc />
    public override Image? GetGalleryButtonImage(PaletteRibbonGalleryButton button, PaletteState state)
    {
        var images = GalleryButtonList.Images;
        switch (button)
        {
            case PaletteRibbonGalleryButton.Up:
                return images[1];
            case PaletteRibbonGalleryButton.DropDown:
                return images[2];
            default:
                return images[0];
        }
    }

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
    #endregion#region Tab Row Background

    #region Tab Row Background

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