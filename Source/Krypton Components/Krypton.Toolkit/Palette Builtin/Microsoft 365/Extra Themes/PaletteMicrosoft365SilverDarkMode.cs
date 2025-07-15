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

#region Class: PaletteMicrosoft365SilverDarkMode
public class PaletteMicrosoft365SilverDarkMode : PaletteMicrosoft365SilverDarkModeBase
{
    #region Static Fields

    #region Colors

    private static readonly Color _tabRowBackgroundColor = Color.FromArgb(119, 132, 161);

    #endregion

    #region Ribbon Specific Colors

    private static readonly Color _ribbonAppButtonDarkColor = Color.FromArgb(84, 96, 125);

    private static readonly Color _ribbonAppButtonLightColor = Color.FromArgb(109, 125, 163);

    private static readonly Color _ribbonAppButtonTextColor = Color.White;

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

    #endregion

    #region Constructors

    static PaletteMicrosoft365SilverDarkMode()
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
    /// Initializes a new instance of the <see cref="PaletteMicrosoft365SilverDarkMode"/> class.
    /// </summary>
    public PaletteMicrosoft365SilverDarkMode() : base(
        new PaletteMicrosoft365SilverDarkMode_BaseScheme(),
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
#endregion

#region Class: KryptonColorTable365SilverDarkMode
/// <summary>
/// Provide KryptonColorTable365SilverDarkMode values using an array of Color values as the source.
/// </summary>
public class KryptonColorTable365SilverDarkMode : KryptonColorTable
{
    #region Static Fields
    private static readonly Color _menuBorder = Color.FromArgb(134, 134, 134);
    private static readonly Color _menuItemSelectedBegin = Color.FromArgb(134, 134, 134);
    private static readonly Color _menuItemSelectedEnd = Color.FromArgb(83, 83, 83);
    private static readonly Color _contextMenuBackground = Color.FromArgb(119, 132, 161);
    private static readonly Color _checkBackground = Color.FromArgb(164, 172, 192);
    private static readonly Color _buttonSelectedBegin = Color.FromArgb(134, 134, 134);
    private static readonly Color _buttonSelectedEnd = Color.FromArgb(83, 83, 83);
    private static readonly Color _buttonPressedBegin = Color.FromArgb(134, 134, 134);
    private static readonly Color _buttonPressedEnd = Color.FromArgb(83, 83, 83);
    private static readonly Color _buttonCheckedBegin = Color.FromArgb(134, 134, 134);
    private static readonly Color _buttonCheckedEnd = Color.FromArgb(83, 83, 83);
    private static Font _menuToolFont;
    private static Font _statusFont;
    #endregion

    #region Instance Fields
    private readonly Color[] _colors;
    private readonly InheritBool _roundedEdges;
    #endregion

    #region Identity
    [SecuritySafeCritical]
    static KryptonColorTable365SilverDarkMode()
    {
        // Get the font settings from the system
        DefineFonts();

        // We need to notice when system color settings change
        SystemEvents.UserPreferenceChanged += OnUserPreferenceChanged;
    }

    /// <summary>
    /// Initialize a new instance of the KryptonColorTable365 class.
    /// </summary>
    /// <param name="colors">Source of </param>
    /// <param name="roundedEdges">Should have rounded edges.</param>
    /// <param name="palette">Associated palette instance.</param>
    public KryptonColorTable365SilverDarkMode([DisallowNull] Color[] colors,
        InheritBool roundedEdges, PaletteBase palette)
        : base(palette)
    {
        Debug.Assert(colors != null);
        if (colors != null)
        {
            _colors = colors;
        }
        _roundedEdges = roundedEdges;
    }
    #endregion

    #region Colors
    /// <summary>
    /// Gets the raw set of colors.
    /// </summary>
    public Color[] Colors => _colors;

    #endregion

    #region RoundedEdges
    /// <summary>
    /// Gets a value indicating if rounded edges are required.
    /// </summary>
    public override InheritBool UseRoundedEdges => _roundedEdges;

    #endregion

    #region ButtonPressed
    #region ButtonPressedBorder
    /// <summary>
    /// Gets the border color for a button being pressed.
    /// </summary>
    public override Color ButtonPressedBorder => _colors[(int)SchemeBaseColors.ButtonBorder];

    #endregion

    #region ButtonPressedGradientBegin
    /// <summary>
    /// Gets the background starting color for a button being pressed.
    /// </summary>
    public override Color ButtonPressedGradientBegin => _buttonPressedBegin;

    #endregion

    #region ButtonPressedGradientMiddle
    /// <summary>
    /// Gets the background middle color for a button being pressed.
    /// </summary>
    public override Color ButtonPressedGradientMiddle => _buttonPressedBegin;

    #endregion

    #region ButtonPressedGradientEnd
    /// <summary>
    /// Gets the background ending color for a button being pressed.
    /// </summary>
    public override Color ButtonPressedGradientEnd => _buttonPressedEnd;

    #endregion

    #region ButtonPressedHighlight
    /// <summary>
    /// Gets the highlight background for a pressed button.
    /// </summary>
    public override Color ButtonPressedHighlight => _buttonPressedBegin;

    #endregion

    #region ButtonPressedHighlightBorder
    /// <summary>
    /// Gets the highlight border for a pressed button.
    /// </summary>
    public override Color ButtonPressedHighlightBorder => _colors[(int)SchemeBaseColors.ButtonBorder];

    #endregion
    #endregion

    #region ButtonSelected
    #region ButtonSelectedBorder
    /// <summary>
    /// Gets the border color for a button being selected.
    /// </summary>
    public override Color ButtonSelectedBorder => _colors[(int)SchemeBaseColors.ButtonBorder];

    #endregion

    #region ButtonSelectedGradientBegin
    /// <summary>
    /// Gets the background starting color for a button being selected.
    /// </summary>
    public override Color ButtonSelectedGradientBegin => _buttonSelectedBegin;

    #endregion

    #region ButtonSelectedGradientMiddle
    /// <summary>
    /// Gets the background middle color for a button being selected.
    /// </summary>
    public override Color ButtonSelectedGradientMiddle => _buttonSelectedBegin;

    #endregion

    #region ButtonSelectedGradientEnd
    /// <summary>
    /// Gets the background ending color for a button being selected.
    /// </summary>
    public override Color ButtonSelectedGradientEnd => _buttonSelectedEnd;

    #endregion

    #region ButtonSelectedHighlight
    /// <summary>
    /// Gets the highlight background for a selected button.
    /// </summary>
    public override Color ButtonSelectedHighlight => _buttonSelectedBegin;

    #endregion

    #region ButtonSelectedHighlightBorder
    /// <summary>
    /// Gets the highlight border for a selected button.
    /// </summary>
    public override Color ButtonSelectedHighlightBorder => _colors[(int)SchemeBaseColors.ButtonBorder];

    #endregion
    #endregion

    #region ButtonChecked
    #region ButtonCheckedGradientBegin
    /// <summary>
    /// Gets the background starting color for a checked button.
    /// </summary>
    public override Color ButtonCheckedGradientBegin => _buttonCheckedBegin;

    #endregion

    #region ButtonCheckedGradientMiddle
    /// <summary>
    /// Gets the background middle color for a checked button.
    /// </summary>
    public override Color ButtonCheckedGradientMiddle => _buttonCheckedBegin;

    #endregion

    #region ButtonCheckedGradientEnd
    /// <summary>
    /// Gets the background ending color for a checked button.
    /// </summary>
    public override Color ButtonCheckedGradientEnd => _buttonCheckedEnd;

    #endregion

    #region ButtonCheckedHighlight
    /// <summary>
    /// Gets the highlight background for a checked button.
    /// </summary>
    public override Color ButtonCheckedHighlight => _buttonCheckedBegin;

    #endregion

    #region ButtonCheckedHighlightBorder
    /// <summary>
    /// Gets the highlight border for a checked button.
    /// </summary>
    public override Color ButtonCheckedHighlightBorder => _colors[(int)SchemeBaseColors.ButtonBorder];

    #endregion
    #endregion

    #region Check
    #region CheckBackground
    /// <summary>
    /// Get background of the check mark area.
    /// </summary>
    public override Color CheckBackground => _checkBackground;

    #endregion

    #region CheckBackground
    /// <summary>
    /// Get background of a pressed check mark area.
    /// </summary>
    public override Color CheckPressedBackground => _checkBackground;

    #endregion

    #region CheckBackground
    /// <summary>
    /// Get background of a selected check mark area.
    /// </summary>
    public override Color CheckSelectedBackground => _checkBackground;

    #endregion
    #endregion

    #region Grip
    #region GripLight
    /// <summary>
    /// Gets the light color used to draw grips.
    /// </summary>
    public override Color GripLight => _colors[(int)SchemeBaseColors.GripLight];

    #endregion

    #region GripDark
    /// <summary>
    /// Gets the dark color used to draw grips.
    /// </summary>
    public override Color GripDark => _colors[(int)SchemeBaseColors.GripDark];

    #endregion
    #endregion

    #region ImageMargin
    #region ImageMarginGradientBegin
    /// <summary>
    /// Gets the starting color for the context menu margin.
    /// </summary>
    public override Color ImageMarginGradientBegin => _colors[(int)SchemeBaseColors.ImageMargin];

    #endregion

    #region ImageMarginGradientMiddle
    /// <summary>
    /// Gets the middle color for the context menu margin.
    /// </summary>
    public override Color ImageMarginGradientMiddle => _colors[(int)SchemeBaseColors.ImageMargin];

    #endregion

    #region ImageMarginGradientEnd
    /// <summary>
    /// Gets the ending color for the context menu margin.
    /// </summary>
    public override Color ImageMarginGradientEnd => _colors[(int)SchemeBaseColors.ImageMargin];

    #endregion

    #region ImageMarginRevealedGradientBegin
    /// <summary>
    /// Gets the starting color for the context menu margin revealed.
    /// </summary>
    public override Color ImageMarginRevealedGradientBegin => _colors[(int)SchemeBaseColors.ImageMargin];

    #endregion

    #region ImageMarginRevealedGradientMiddle
    /// <summary>
    /// Gets the middle color for the context menu margin revealed.
    /// </summary>
    public override Color ImageMarginRevealedGradientMiddle => _colors[(int)SchemeBaseColors.ImageMargin];

    #endregion

    #region ImageMarginRevealedGradientEnd
    /// <summary>
    /// Gets the ending color for the context menu margin revealed.
    /// </summary>
    public override Color ImageMarginRevealedGradientEnd => _colors[(int)SchemeBaseColors.ImageMargin];

    #endregion
    #endregion

    #region MenuBorder
    /// <summary>
    /// Gets the color of the border around menus.
    /// </summary>
    public override Color MenuBorder => _menuBorder;

    #endregion

    #region MenuItem
    #region MenuItemBorder
    /// <summary>
    /// Gets the border color for around the menu item.
    /// </summary>
    public override Color MenuItemBorder => _menuBorder;

    #endregion

    #region MenuItemSelected
    /// <summary>
    /// Gets the color of a selected menu item.
    /// </summary>
    public override Color MenuItemSelected => _colors[(int)SchemeBaseColors.ButtonBorder];

    #endregion

    #region MenuItemPressedGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used when a top-level ToolStripMenuItem is pressed down.
    /// </summary>
    public override Color MenuItemPressedGradientBegin => _colors[(int)SchemeBaseColors.ToolStripBegin];

    #endregion

    #region MenuItemPressedGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used when a top-level ToolStripMenuItem is pressed down.
    /// </summary>
    public override Color MenuItemPressedGradientEnd => _colors[(int)SchemeBaseColors.ToolStripEnd];

    #endregion

    #region MenuItemPressedGradientMiddle
    /// <summary>
    /// Gets the middle color of the gradient used when a top-level ToolStripMenuItem is pressed down.
    /// </summary>
    public override Color MenuItemPressedGradientMiddle => _colors[(int)SchemeBaseColors.ToolStripMiddle];

    #endregion

    #region MenuItemSelectedGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used when the ToolStripMenuItem is selected.
    /// </summary>
    public override Color MenuItemSelectedGradientBegin => _menuItemSelectedBegin;

    #endregion

    #region MenuItemSelectedGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used when the ToolStripMenuItem is selected.
    /// </summary>
    public override Color MenuItemSelectedGradientEnd => _menuItemSelectedEnd;

    #endregion
    #endregion

    #region MenuStrip
    #region MenuStripGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used in the MenuStrip.
    /// </summary>
    public override Color MenuStripGradientBegin => _colors[(int)SchemeBaseColors.ToolStripBack];

    #endregion

    #region MenuStripGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used in the MenuStrip.
    /// </summary>
    public override Color MenuStripGradientEnd => _colors[(int)SchemeBaseColors.ToolStripBack];

    #endregion

    #endregion

    #region OverflowButton
    #region OverflowButtonGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used in the ToolStripOverflowButton.
    /// </summary>
    public override Color OverflowButtonGradientBegin => _colors[(int)SchemeBaseColors.OverflowBegin];

    #endregion

    #region OverflowButtonGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used in the ToolStripOverflowButton.
    /// </summary>
    public override Color OverflowButtonGradientEnd => _colors[(int)SchemeBaseColors.OverflowEnd];

    #endregion

    #region OverflowButtonGradientMiddle
    /// <summary>
    /// Gets the middle color of the gradient used in the ToolStripOverflowButton.
    /// </summary>
    public override Color OverflowButtonGradientMiddle => _colors[(int)SchemeBaseColors.OverflowMiddle];

    #endregion
    #endregion

    #region RaftingContainer
    #region RaftingContainerGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used in the ToolStripContainer.
    /// </summary>
    public override Color RaftingContainerGradientBegin => _colors[(int)SchemeBaseColors.ToolStripBack];

    #endregion

    #region RaftingContainerGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used in the ToolStripContainer.
    /// </summary>
    public override Color RaftingContainerGradientEnd => _colors[(int)SchemeBaseColors.ToolStripBack];

    #endregion

    #endregion

    #region Separator
    #region SeparatorLight
    /// <summary>
    /// Gets the light separator color.
    /// </summary>
    public override Color SeparatorLight => _colors[(int)SchemeBaseColors.SeparatorLight];

    #endregion

    #region SeparatorDark
    /// <summary>
    /// Gets the dark separator color.
    /// </summary>
    public override Color SeparatorDark => _colors[(int)SchemeBaseColors.SeparatorDark];

    #endregion
    #endregion

    #region StatusStrip
    #region StatusStripGradientBegin
    /// <summary>
    /// Gets the starting color for the status strip background.
    /// </summary>
    public override Color StatusStripGradientBegin => _colors[(int)SchemeBaseColors.StatusStripLight];

    #endregion

    #region StatusStripGradientEnd
    /// <summary>
    /// Gets the ending color for the status strip background.
    /// </summary>
    public override Color StatusStripGradientEnd => _colors[(int)SchemeBaseColors.StatusStripDark];

    #endregion
    #endregion

    #region Text
    #region MenuItemText
    /// <summary>
    /// Gets the text color used on the menu items.
    /// </summary>
    public override Color MenuItemText => _colors[(int)SchemeBaseColors.TextButtonNormal];

    #endregion

    #region MenuStripText
    /// <summary>
    /// Gets the text color used on the menu strip.
    /// </summary>
    public override Color MenuStripText => _colors[(int)SchemeBaseColors.StatusStripText];

    #endregion

    #region ToolStripText
    /// <summary>
    /// Gets the text color used on the tool strip.
    /// </summary>
    public override Color ToolStripText => _colors[(int)SchemeBaseColors.StatusStripText];

    #endregion

    #region StatusStripText
    /// <summary>
    /// Gets the text color used on the status strip.
    /// </summary>
    public override Color StatusStripText => _colors[(int)SchemeBaseColors.StatusStripText];

    #endregion

    #region MenuStripFont
    /// <summary>
    /// Gets the font used on the menu strip.
    /// </summary>
    public override Font MenuStripFont => _menuToolFont;

    #endregion

    #region ToolStripFont
    /// <summary>
    /// Gets the font used on the tool strip.
    /// </summary>
    public override Font ToolStripFont => _menuToolFont;

    #endregion

    #region StatusStripFont
    /// <summary>
    /// Gets the font used on the status strip.
    /// </summary>
    public override Font StatusStripFont => _statusFont;

    #endregion
    #endregion

    #region ToolStrip
    #region ToolStripBorder
    /// <summary>
    /// Gets the border color to use on the bottom edge of the ToolStrip.
    /// </summary>
    public override Color ToolStripBorder => _colors[(int)SchemeBaseColors.ToolStripBorder];

    #endregion

    #region ToolStripContentPanelGradientBegin
    /// <summary>
    /// Gets the starting color for the content panel background.
    /// </summary>
    public override Color ToolStripContentPanelGradientBegin => _colors[(int)SchemeBaseColors.ToolStripBack];

    #endregion

    #region ToolStripContentPanelGradientEnd
    /// <summary>
    /// Gets the ending color for the content panel background.
    /// </summary>
    public override Color ToolStripContentPanelGradientEnd => _colors[(int)SchemeBaseColors.ToolStripBack];

    #endregion

    #region ToolStripDropDownBackground
    /// <summary>
    /// Gets the background color for drop-down menus.
    /// </summary>
    public override Color ToolStripDropDownBackground => _contextMenuBackground;

    #endregion

    #region ToolStripGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used in the ToolStrip background.
    /// </summary>
    public override Color ToolStripGradientBegin => _colors[(int)SchemeBaseColors.ToolStripBegin];

    #endregion

    #region ToolStripGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used in the ToolStrip background.
    /// </summary>
    public override Color ToolStripGradientEnd => _colors[(int)SchemeBaseColors.ToolStripEnd];

    #endregion

    #region ToolStripGradientMiddle
    /// <summary>
    /// Gets the middle color of the gradient used in the ToolStrip background.
    /// </summary>
    public override Color ToolStripGradientMiddle => _colors[(int)SchemeBaseColors.ToolStripMiddle];

    #endregion

    #region ToolStripPanelGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used in the ToolStripPanel.
    /// </summary>
    public override Color ToolStripPanelGradientBegin => _colors[(int)SchemeBaseColors.ToolStripBack];

    #endregion

    #region ToolStripPanelGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used in the ToolStripPanel.
    /// </summary>
    public override Color ToolStripPanelGradientEnd => _colors[(int)SchemeBaseColors.ToolStripBack];

    #endregion
    #endregion

    #region Implementation
    private static void DefineFonts()
    {
        // Create new font using system information
        _menuToolFont = new Font(@"Segoe UI", SystemFonts.MenuFont!.SizeInPoints, FontStyle.Regular);
        _statusFont = new Font(@"Segoe UI", SystemFonts.StatusFont!.SizeInPoints, FontStyle.Regular);
    }

    private static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e) =>
        // Update fonts to reflect any change in system settings
        DefineFonts();

    #endregion
}
#endregion