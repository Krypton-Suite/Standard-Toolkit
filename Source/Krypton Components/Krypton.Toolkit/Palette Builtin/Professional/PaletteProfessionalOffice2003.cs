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
/// Take into account the current theme when creating an Office 2003 appearance.
/// </summary>
public class PaletteProfessionalOffice2003 : PaletteOffice2003Base
{
    #region Static Fields
    private static readonly Color[] _colorsB =
    [
        Color.FromArgb( 89, 135, 214),   // Header1Begin
        Color.FromArgb(  4,  57, 148) // Header1End
    ];

    private static readonly Color[] _colorsG =
    [
        Color.FromArgb(175, 192, 130),   // Header1Begin
        Color.FromArgb( 99, 122,  69) // Header1End
    ];

    private static readonly Color[] _colorsS =
    [
        Color.FromArgb(168, 167, 191),   // Header1Begin
        Color.FromArgb(113, 112, 145) // Header1End
    ];
    #endregion

    #region Instance Fields
    protected readonly KryptonColorSchemeBase? BaseColors;
    private bool _usingOffice2003;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteProfessionalOffice2003 class.
    /// </summary>
    public PaletteProfessionalOffice2003()
    {
        ThemeName = nameof(PaletteProfessionalOffice2003);
    }
    #endregion

    #region ColorTable
    /// <summary>
    /// Generate an appropriate color table.
    /// </summary>
    /// <returns>KryptonColorTable instance.</returns>
    internal override KryptonProfessionalKCT GenerateColorTable(bool _)
    {
        if (Environment.OSVersion.Version.Major < 6)
        {
            // Are visual styles being used in this application?
            if (VisualStyleInformation.IsEnabledByUser)
            {
                // Is a predefined scheme being used?
                switch (VisualStyleInformation.ColorScheme)
                {
                    case @"NormalColor":
                        _usingOffice2003 = true;
                        return new KryptonProfessionalKCT(_colorsB, false, this);
                    case @"HomeStead":
                        _usingOffice2003 = true;
                        return new KryptonProfessionalKCT(_colorsG, false, this);
                    case @"Metallic":
                        _usingOffice2003 = true;
                        return new KryptonProfessionalKCT(_colorsS, false, this);
                }
            }
        }

        // Not using a recognized office 2003 color scheme
        _usingOffice2003 = false;

        // Not a recognized scheme, so get the base class to generate something
        // that looks sensible based on the current system settings
        return base.GenerateColorTable(true);
    }
    #endregion

    #region StandardBack
    /// <summary>
    /// Gets the color background drawing style.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state)
    {
        // Only override system palette if a recognized office 2003 color scheme is used
        if (_usingOffice2003)
        {
            switch (style)
            {
                case PaletteBackStyle.HeaderDockActive:
                case PaletteBackStyle.HeaderDockInactive:
                    return PaletteColorStyle.Solid;
            }
        }

        return base.GetBackColorStyle(style, state);
    }

    /// <summary>
    /// Gets the first background color.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBackColor1(PaletteBackStyle style, PaletteState state)
    {
        // Only override system palette if a recognized office 2003 color scheme is used
        if (_usingOffice2003)
        {
            switch (style)
            {
                case PaletteBackStyle.ContextMenuItemHighlight:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SystemColors.Control;
                        case PaletteState.Normal:
                            return GlobalStaticValues.EMPTY_COLOR;
                        case PaletteState.Tracking:
                            return ColorTable.MenuItemSelectedGradientBegin;
                    }
                    break;
                case PaletteBackStyle.HeaderDockInactive:
                    return state == PaletteState.Disabled ? SystemColors.Control : ColorTable.ButtonCheckedHighlight;

                case PaletteBackStyle.HeaderDockActive:
                    return state == PaletteState.Disabled ? SystemColors.Control : SystemColors.Highlight;
            }
        }

        return base.GetBackColor1(style, state);
    }

    /// <summary>
    /// Gets the second back color.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
    {
        // Only override system palette if a recognized office 2003 color scheme is used
        if (_usingOffice2003)
        {
            switch (style)
            {
                case PaletteBackStyle.ContextMenuItemHighlight:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SystemColors.Control;
                        case PaletteState.Normal:
                            return GlobalStaticValues.EMPTY_COLOR;
                        case PaletteState.Tracking:
                            return ColorTable.MenuItemSelectedGradientBegin;
                    }
                    break;
                case PaletteBackStyle.HeaderDockInactive:
                    return state == PaletteState.Disabled ? SystemColors.Control : ColorTable.ButtonCheckedHighlight;

                case PaletteBackStyle.HeaderDockActive:
                    return state == PaletteState.Disabled ? SystemColors.Control : SystemColors.Highlight;

                case PaletteBackStyle.TabDock:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SystemColors.Control;
                        case PaletteState.Normal:
                            return MergeColors(SystemColors.Window, 0.1f, ColorTable.ButtonCheckedHighlight, 0.9f);
                        case PaletteState.Pressed:
                        case PaletteState.Tracking:
                            return MergeColors(SystemColors.Window, 0.4f, ColorTable.ButtonCheckedGradientMiddle, 0.6f);
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return SystemColors.Window;
                    }
                    break;
            }
        }

        return base.GetBackColor2(style, state);
    }
    #endregion

    #region StandardContent
    /// <summary>
    /// Gets the first back color for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
    {
        // Only override system palette if a recognized office 2003 color scheme is used
        if (_usingOffice2003)
        {
            if (state == PaletteState.Disabled)
            {
                return SystemColors.ControlDark;
            }

            switch (style)
            {
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridHeaderRowCustom2:
                case PaletteContentStyle.GridHeaderRowCustom3:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderColumnCustom2:
                case PaletteContentStyle.GridHeaderColumnCustom3:
                case PaletteContentStyle.HeaderDockInactive:
                    return SystemColors.ControlText;
                case PaletteContentStyle.HeaderDockActive:
                    return SystemColors.ActiveCaptionText;
            }
        }

        // Get everything else from the base class implementation
        return base.GetContentShortTextColor1(style, state);
    }

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
    {
        // Only override system palette if a recognized office 2003 color scheme is used
        if (_usingOffice2003)
        {
            if (state == PaletteState.Disabled)
            {
                return SystemColors.ControlDark;
            }

            switch (style)
            {
                case PaletteContentStyle.HeaderDockInactive:
                    return SystemColors.ControlText;
                case PaletteContentStyle.HeaderDockActive:
                    return SystemColors.ActiveCaptionText;
            }
        }

        // Get everything else from the base class implementation
        return base.GetContentShortTextColor2(style, state);
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
    public override Color GetRibbonFileAppTabBottomColor(PaletteState state) => GlobalStaticValues.DEFAULT_RIBBON_FILE_APP_TAB_BOTTOM_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTopColor(PaletteState state) => GlobalStaticValues.DEFAULT_RIBBON_FILE_APP_TAB_TOP_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTextColor(PaletteState state) => GlobalStaticValues.DEFAULT_RIBBON_FILE_APP_TAB_TEXT_COLOR;

    #endregion
}

#region Class: PaletteOffice2003Base

/// <summary>
/// Provides a professional appearance using colors/fonts generated from system settings.
/// </summary>
public class PaletteOffice2003Base : PaletteBase
{
    #region Static Fields

    #region Padding

    private static readonly Padding _contentPaddingGrid = new Padding(2, 1, 2, 1);
    private static readonly Padding _contentPaddingHeader1 = new Padding(3, 2, 3, 2);
    private static readonly Padding _contentPaddingHeader2 = new Padding(3, 2, 3, 2);
    private static readonly Padding _contentPaddingHeader3 = new Padding(2, 1, 2, 1);
    private static readonly Padding _contentPaddingCalendar = new Padding(2);
    //private static readonly Padding _contentPaddingHeaderForm = new Padding(owningForm!.RealWindowBorders.Left, owningForm!.RealWindowBorders.Bottom / 2, 0, 0);
    private static readonly Padding _contentPaddingLabel = new Padding(3, 2, 3, 2);
    private static readonly Padding _contentPaddingLabel2 = new Padding(8, 2, 8, 2);
    private static readonly Padding _contentPaddingButtonCalendar = new Padding(0);
    private static readonly Padding _contentPaddingButtonInputControl = new Padding(1);
    private static readonly Padding _contentPaddingButton12 = new Padding(3, 2, 3, 2);
    private static readonly Padding _contentPaddingButton3 = new Padding(1);
    private static readonly Padding _contentPaddingButton4 = new Padding(4, 3, 4, 3);
    private static readonly Padding _contentPaddingButton5 = new Padding(3, 3, 3, 2);
    private static readonly Padding _contentPaddingButton6 = new Padding(3);
    private static readonly Padding _contentPaddingButton7 = new Padding(1, 1, 3, 1);
    private static readonly Padding _contentPaddingButtonForm = new Padding(0);
    private static readonly Padding _contentPaddingButtonGallery = new Padding(1, 0, 1, 0);
    private static readonly Padding _contentPaddingToolTip = new Padding(2);
    private static readonly Padding _contentPaddingSuperTip = new Padding(4);
    private static readonly Padding _contentPaddingKeyTip = new Padding(1, -1, 0, -2);
    private static readonly Padding _contentPaddingContextMenuHeading = new Padding(8, 2, 8, 0);
    private static readonly Padding _contentPaddingContextMenuImage = new Padding(1);
    private static readonly Padding _contentPaddingContextMenuItemText = new Padding(9, 1, 7, 0);
    private static readonly Padding _contentPaddingContextMenuItemTextAlt = new Padding(7, 1, 6, 0);
    private static readonly Padding _contentPaddingContextMenuItemShortcutText = new Padding(3, 1, 4, 0);
    private static readonly Padding _metricPaddingInputControl = new Padding(0, 1, 0, 1);
    private static readonly Padding _metricPaddingRibbon = new Padding(0, 1, 1, 1);
    private static readonly Padding _metricPaddingRibbonAppButton = new Padding(3, 0, 3, 0);
    private static readonly Padding _metricPaddingHeader = new Padding(0, 3, 1, 3);
    //private static readonly Padding _metricPaddingHeaderForm = new Padding(0, owningForm!.RealWindowBorders.Right, 0, 0);//, 3, 0, -3); // Move the Maximised Form buttons down a bit
    private static readonly Padding _metricPaddingBarInside = new Padding(3, 3, 3, 3);
    private static readonly Padding _metricPaddingBarTabs = new Padding(0, 0, 0, 0);
    private static readonly Padding _metricPaddingBarOutside = new Padding(0, 0, 0, 3);
    private static readonly Padding _metricPaddingPageButtons = new Padding(1, 3, 1, 3);
    private static readonly Padding _metricPaddingContextMenuItemHighlight = new Padding(1, 0, 1, 0);
    private static readonly Padding _metricPaddingContextMenuItemsCollection = new Padding(0, 1, 0, 1);

    #endregion

    #region Images

    private static readonly Image _buttonSpecClose = ProfessionalButtonSpecResources.ProfessionalCloseButton;
    private static readonly Image _buttonSpecContext = GenericProfessionalImageResources.ProfessionalContextButton;
    private static readonly Image _buttonSpecNext = GenericProfessionalImageResources.ProfessionalNextButton;
    private static readonly Image _buttonSpecPrevious = GenericProfessionalImageResources.ProfessionalPreviousButton;
    private static readonly Image _buttonSpecArrowLeft = GenericProfessionalImageResources.ProfessionalArrowLeftButton;
    private static readonly Image _buttonSpecArrowRight = GenericProfessionalImageResources.ProfessionalArrowRightButton;
    private static readonly Image _buttonSpecArrowUp = GenericProfessionalImageResources.ProfessionalArrowUpButton;
    private static readonly Image _buttonSpecArrowDown = GenericProfessionalImageResources.ProfessionalArrowDownButton;
    private static readonly Image _buttonSpecDropDown = GenericProfessionalImageResources.ProfessionalDropDownButton;
    private static readonly Image _buttonSpecPinVertical = ProfessionalPinImageResources.ProfessionalPinVerticalButton;
    private static readonly Image _buttonSpecPinHorizontal = ProfessionalPinImageResources.ProfessionalPinHorizontalButton;
    private static readonly Image _buttonSpecWorkspaceMaximize = ProfessionalControlBoxResources.ProfessionalMaximize;
    private static readonly Image _buttonSpecWorkspaceRestore = GenericProfessionalImageResources.ProfessionalRestore;
    //private static readonly Image _buttonSpecRibbonMinimize = RibbonArrowImageResources.RibbonUp2010;
    //private static readonly Image _buttonSpecRibbonExpand = RibbonArrowImageResources.RibbonDown2010;
    private static readonly Image _systemCloseNormal = ProfessionalControlBoxResources.ProfessionalButtonCloseNormal;
    private static readonly Image _systemCloseDisabled = GenericProfessionalImageResources.ProfessionalButtonCloseDisabled;
    private static readonly Image _systemMaximiseNormal = ProfessionalControlBoxResources.ProfessionalButtonMaxNormal;
    private static readonly Image _systemMaximiseDisabled = ProfessionalControlBoxResources.ProfessionalButtonMaxDisabled;
    private static readonly Image _systemMinimiseNormal = ProfessionalControlBoxResources.ProfessionalButtonMinNormal;
    private static readonly Image _systemMinimiseDisabled = ProfessionalControlBoxResources.ProfessionalButtonMinDisabled;
    private static readonly Image _systemRestoreNormal = ProfessionalControlBoxResources.ProfessionalButtonRestoreNormal;
    private static readonly Image _systemRestoreDisabled = ProfessionalControlBoxResources.ProfessionalButtonRestoreDisabled;
    private static readonly Image _systemHelpA = Office2003ControlBoxResources.Office2003HelpIconNormal;
    private static readonly Image _systemHelpI = Office2003ControlBoxResources.Office2003HelpIconDisabled;
    private static readonly Image _pendantCloseA = ProfessionalPendantImageResources.ProfessionalPendantCloseNormal;
    private static readonly Image _pendantCloseI = ProfessionalPendantImageResources.ProfessionalPendantCloseDisabled;
    private static readonly Image _pendantMinA = ProfessionalPendantImageResources.ProfessionalPendantMinNormal;
    private static readonly Image _pendantMinI = ProfessionalPendantImageResources.ProfessionalPendantMinDisabled;
    private static readonly Image _pendantRestoreA = ProfessionalPendantImageResources.ProfessionalPendantRestoreNormal;
    private static readonly Image _pendantRestoreI = ProfessionalPendantImageResources.ProfessionalPendantRestoreDisabled;
    private static readonly Image _pendantExpandA = ProfessionalPendantImageResources.ProfessionalPendantExpandNormal;
    private static readonly Image _pendantExpandI = ProfessionalPendantImageResources.ProfessionalPendantExpandDisabled;
    private static readonly Image _pendantMinimizeA = ProfessionalPendantImageResources.ProfessionalPendantMinimizeNormal;
    private static readonly Image _pendantMinimizeI = ProfessionalPendantImageResources.ProfessionalPendantMinimizeDisabled;
    private static readonly Image? _contextMenuChecked = GenericProfessionalImageResources.SystemChecked;
    private static readonly Image? _contextMenuIndeterminate = GenericProfessionalImageResources.SystemIndeterminate;
    private static readonly Image? _contextMenuSubMenu = GenericProfessionalImageResources.SystemContextMenuSub;
    private static readonly Image? _treeExpandPlus = TreeItemImageResources.TreeExpandPlus;
    private static readonly Image? _treeCollapseMinus = TreeItemImageResources.TreeCollapseMinus;

    #region Toolbar Images

    private static readonly Image _formToolbarButtonSpecNewNormal = Office2003ToolbarImageResources.Office2003ToolbarNewNormal;
    private static readonly Image _formToolbarButtonSpecNewActive = Office2003ToolbarImageResources.Office2003ToolbarNewNormal;
    private static readonly Image _formToolbarButtonSpecNewDisabled = Office2003ToolbarImageResources.Office2003ToolbarNewDisabled;

    private static readonly Image _formToolbarButtonSpecOpenNormal = Office2003ToolbarImageResources.Office2003ToolbarOpenNormal;
    private static readonly Image _formToolbarButtonSpecOpenActive = Office2003ToolbarImageResources.Office2003ToolbarOpenNormal;
    private static readonly Image _formToolbarButtonSpecOpenDisabled = Office2003ToolbarImageResources.Office2003ToolbarOpenDisabled;

    private static readonly Image _formToolbarButtonSpecSaveNormal = Office2003ToolbarImageResources.Office2003ToolbarSaveNormal;
    private static readonly Image _formToolbarButtonSpecSaveActive = Office2003ToolbarImageResources.Office2003ToolbarSaveNormal;
    private static readonly Image _formToolbarButtonSpecSaveDisabled = Office2003ToolbarImageResources.Office2003ToolbarSaveDisabled;

    private static readonly Image _formToolbarButtonSpecSaveAllNormal = Office2007ToolbarImageResources.Office2007ToolbarSaveAllNormal;
    private static readonly Image _formToolbarButtonSpecSaveAllActive = Office2007ToolbarImageResources.Office2007ToolbarSaveAllNormal;
    private static readonly Image _formToolbarButtonSpecSaveAllDisabled = Office2007ToolbarImageResources.Office2007ToolbarSaveAllDisabled;

    private static readonly Image _formToolbarButtonSpecSaveAsNormal = Office2007ToolbarImageResources.Office2007ToolbarSaveAsNormal;
    private static readonly Image _formToolbarButtonSpecSaveAsActive = Office2007ToolbarImageResources.Office2007ToolbarSaveAsNormal;
    private static readonly Image _formToolbarButtonSpecSaveAsDisabled = Office2007ToolbarImageResources.Office2007ToolbarSaveAsDisabled;

    private static readonly Image _formToolbarButtonSpecCutNormal = Office2003ToolbarImageResources.Office2003ToolbarCutNormal;
    private static readonly Image _formToolbarButtonSpecCutActive = Office2003ToolbarImageResources.Office2003ToolbarCutNormal;
    private static readonly Image _formToolbarButtonSpecCutDisabled = Office2003ToolbarImageResources.Office2003ToolbarCutDisabled;

    private static readonly Image _formToolbarButtonSpecCopyNormal = Office2003ToolbarImageResources.Office2003ToolbarCopyNormal;
    private static readonly Image _formToolbarButtonSpecCopyActive = Office2003ToolbarImageResources.Office2003ToolbarCopyNormal;
    private static readonly Image _formToolbarButtonSpecCopyDisabled = Office2003ToolbarImageResources.Office2003ToolbarCopyDisabled;

    private static readonly Image _formToolbarButtonSpecPasteNormal = Office2003ToolbarImageResources.Office2003ToolbarPasteNormal;
    private static readonly Image _formToolbarButtonSpecPasteActive = Office2003ToolbarImageResources.Office2003ToolbarPasteNormal;
    private static readonly Image _formToolbarButtonSpecPasteDisabled = Office2003ToolbarImageResources.Office2003ToolbarPasteDisabled;

    private static readonly Image _formToolbarButtonSpecUndoNormal = Office2003ToolbarImageResources.Office2003ToolbarUndoNormal;
    private static readonly Image _formToolbarButtonSpecUndoActive = Office2003ToolbarImageResources.Office2003ToolbarUndoNormal;
    private static readonly Image _formToolbarButtonSpecUndoDisabled = Office2003ToolbarImageResources.Office2003ToolbarUndoDisabled;

    private static readonly Image _formToolbarButtonSpecRedoNormal = Office2003ToolbarImageResources.Office2003ToolbarRedoNormal;
    private static readonly Image _formToolbarButtonSpecRedoActive = Office2003ToolbarImageResources.Office2003ToolbarRedoNormal;
    private static readonly Image _formToolbarButtonSpecRedoDisabled = Office2003ToolbarImageResources.Office2003ToolbarRedoDisabled;

    private static readonly Image _formToolbarButtonSpecPageSetupNormal = Office2007ToolbarImageResources.Office2007ToolbarPageSetupNormal;
    private static readonly Image _formToolbarButtonSpecPageSetupActive = Office2007ToolbarImageResources.Office2007ToolbarPageSetupNormal;
    private static readonly Image _formToolbarButtonSpecPageSetupDisabled = Office2007ToolbarImageResources.Office2007ToolbarPageSetupDisabled;

    private static readonly Image _formToolbarButtonSpecPrintPreviewNormal = Office2007ToolbarImageResources.Office2007ToolbarPrintPreviewNormal;
    private static readonly Image _formToolbarButtonSpecPrintPreviewActive = Office2007ToolbarImageResources.Office2007ToolbarPrintPreviewNormal;
    private static readonly Image _formToolbarButtonSpecPrintPreviewDisabled = Office2007ToolbarImageResources.Office2007ToolbarPrintPreviewDisabled;

    private static readonly Image _formToolbarButtonSpecPrintNormal = Office2007ToolbarImageResources.Office2007ToolbarPrintNormal;
    private static readonly Image _formToolbarButtonSpecPrintActive = Office2007ToolbarImageResources.Office2007ToolbarPrintNormal;
    private static readonly Image _formToolbarButtonSpecPrintDisabled = Office2007ToolbarImageResources.Office2007ToolbarPrintDisabled;

    private static readonly Image _formToolbarButtonSpecQuickPrintNormal = Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal;
    private static readonly Image _formToolbarButtonSpecQuickPrintActive = Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal;
    private static readonly Image _formToolbarButtonSpecQuickPrintDisabled = Office2007ToolbarImageResources.Office2007ToolbarQuickPrintDisabled;

    #endregion

    #endregion

    #region Colors

    private static readonly Color _contextTextColor = Color.White;
    //private static readonly Color _lightGray = Color.FromArgb(242, 242, 242);
    private static readonly Color _contextCheckedTabBorder1 = Color.FromArgb(223, 119, 0);
    private static readonly Color _contextCheckedTabBorder2 = Color.FromArgb(230, 190, 129);
    private static readonly Color _contextCheckedTabBorder3 = Color.FromArgb(220, 202, 171);
    private static readonly Color _contextCheckedTabBorder4 = Color.FromArgb(255, 252, 247);

    #endregion

    #endregion Static Fields

    #region Instance Fields

    protected readonly KryptonColorSchemeBase BaseColors;
    private KryptonProfessionalKCT? _table;

    private Image? _disabledDropDownImage;
    private Image? _normalDropDownImage;

    /// <inheritdoc/>
    protected override Color[] SchemeColors => _ribbonColors;
    private Color[] _ribbonColors;

    //private Color _disabledDropDownColor;
    //private Color _normalDropDownColor;
    private Color _disabledText;
    private Color _disabledGlyphDark;
    private Color _disabledGlyphLight;
    //private Color _contextCheckedTabBorder;
    //private Color _contextCheckedTabFill;
    private Color _contextGroupAreaBorder;
    //private Color _contextGroupAreaInside;
    //private Color _contextGroupFrameTop;
    //private Color _contextGroupFrameBottom;
    private Color _contextTabSeparator;
    //private Color _focusTabFill;
    private Color _toolTipBack1;
    private Color _toolTipBack2;
    private Color _toolTipBorder;
    private Color _toolTipText;
    //private Color[] _ribbonGroupCollapsedBackContext;
    //private Color[] _ribbonGroupCollapsedBackContextTracking;
    //private Color[] _ribbonGroupCollapsedBorderContext;
    //private Color[] _ribbonGroupCollapsedBorderContextTracking;
    private Color[] _appButtonNormal;
    private Color[] _appButtonTrack;
    private Color[] _appButtonPressed;
    private Image? _galleryImageUp;
    private Image? _galleryImageDown;
    private Image? _galleryImageDropDown;

    #endregion Instance Fields

    #region Identity

    /// <summary>
    /// Initialize a new instance of the PaletteOffice2003Base class.
    /// </summary>
    public PaletteOffice2003Base()
    {
        BaseColors = new PaletteProfessionalSystem_BaseScheme();

        ThemeName = nameof(PaletteOffice2003Base);

        // Get the font settings from the system
        DefineFonts();

        // Generate the myriad ribbon colors from system settings
        DefineRibbonColors();
    }

    #endregion

    #region Renderer

    /// <summary>
    /// Gets the renderer to use for this palette.
    /// </summary>
    /// <returns>Renderer to use for drawing palette settings.</returns>
    public override IRenderer GetRenderer() =>
        // We always want the professional renderer
        KryptonManager.RenderProfessional;

    #endregion

    #region Back

    /// <summary>
    /// Gets a value indicating if background should be drawn.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetBackDraw(PaletteBackStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return InheritBool.Inherit;
        }

        return style switch
        {
            PaletteBackStyle.SeparatorLowProfile or PaletteBackStyle.SeparatorCustom1 or PaletteBackStyle.SeparatorCustom2 or PaletteBackStyle.SeparatorCustom3 => InheritBool.False,
            PaletteBackStyle.ButtonInputControl or PaletteBackStyle.ContextMenuItemImage or PaletteBackStyle.ContextMenuItemHighlight => state switch
            {
                PaletteState.Normal or PaletteState.NormalDefaultOverride => InheritBool.False,
                _ => InheritBool.True
            },
            PaletteBackStyle.ButtonListItem or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonCalendarDay or PaletteBackStyle.ButtonLowProfile or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonNavigatorOverflow => state switch
            {
                PaletteState.Disabled or PaletteState.Normal or PaletteState.NormalDefaultOverride => InheritBool.False,
                _ => InheritBool.True
            },
            _ => InheritBool.True // Default to drawing the background
        };
    }

    /// <summary>
    /// Gets the graphics drawing hint for the background.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public override PaletteGraphicsHint GetBackGraphicsHint(PaletteBackStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteGraphicsHint.Inherit;
        }

        return style switch
        {
            PaletteBackStyle.TabHighProfile or PaletteBackStyle.TabStandardProfile or PaletteBackStyle.TabLowProfile or PaletteBackStyle.TabOneNote or PaletteBackStyle.TabDock or PaletteBackStyle.TabDockAutoHidden or PaletteBackStyle.TabCustom1 or PaletteBackStyle.TabCustom2 or PaletteBackStyle.TabCustom3 or PaletteBackStyle.PanelClient or PaletteBackStyle.PanelRibbonInactive or PaletteBackStyle.PanelCustom1 or PaletteBackStyle.PanelCustom2 or PaletteBackStyle.PanelCustom3 or PaletteBackStyle.SeparatorLowProfile or PaletteBackStyle.SeparatorCustom1 or PaletteBackStyle.SeparatorCustom2 or PaletteBackStyle.SeparatorCustom3 or PaletteBackStyle.PanelAlternate or PaletteBackStyle.Control or PaletteBackStyle.ControlClient or PaletteBackStyle.ControlAlternate or PaletteBackStyle.ControlGroupBox or PaletteBackStyle.ControlToolTip or PaletteBackStyle.ControlRibbon or PaletteBackStyle.ControlRibbonAppMenu or PaletteBackStyle.ControlCustom1 or PaletteBackStyle.ControlCustom2 or PaletteBackStyle.ControlCustom3 or PaletteBackStyle.ContextMenuOuter or PaletteBackStyle.ContextMenuInner or PaletteBackStyle.ContextMenuHeading or PaletteBackStyle.ContextMenuSeparator or PaletteBackStyle.ContextMenuItemSplit or PaletteBackStyle.ContextMenuItemImageColumn or PaletteBackStyle.ContextMenuItemImage or PaletteBackStyle.ContextMenuItemHighlight or PaletteBackStyle.InputControlStandalone or PaletteBackStyle.InputControlRibbon or PaletteBackStyle.InputControlCustom1 or PaletteBackStyle.InputControlCustom2 or PaletteBackStyle.InputControlCustom3 or PaletteBackStyle.FormMain or PaletteBackStyle.FormCustom1 or PaletteBackStyle.FormCustom2 or PaletteBackStyle.FormCustom3 or PaletteBackStyle.SeparatorHighInternalProfile or PaletteBackStyle.SeparatorHighProfile or PaletteBackStyle.HeaderPrimary or PaletteBackStyle.HeaderSecondary or PaletteBackStyle.HeaderDockInactive or PaletteBackStyle.HeaderDockActive or PaletteBackStyle.HeaderCalendar or PaletteBackStyle.HeaderForm or PaletteBackStyle.HeaderCustom1 or PaletteBackStyle.HeaderCustom2 or PaletteBackStyle.HeaderCustom3 or PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonGallery or PaletteBackStyle.ButtonAlternate or PaletteBackStyle.ButtonLowProfile or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonCalendarDay or PaletteBackStyle.ButtonCluster or PaletteBackStyle.ButtonNavigatorStack or PaletteBackStyle.ButtonNavigatorOverflow or PaletteBackStyle.ButtonNavigatorMini or PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose or PaletteBackStyle.ButtonCustom1 or PaletteBackStyle.ButtonCustom2 or PaletteBackStyle.ButtonCustom3 or PaletteBackStyle.ButtonInputControl or PaletteBackStyle.GridBackgroundList or PaletteBackStyle.GridBackgroundSheet or PaletteBackStyle.GridBackgroundCustom1
                or PaletteBackStyle.GridBackgroundCustom2
                or PaletteBackStyle.GridBackgroundCustom3
                or PaletteBackStyle.GridHeaderColumnList or PaletteBackStyle.GridHeaderColumnSheet or PaletteBackStyle.GridHeaderColumnCustom1 or PaletteBackStyle.GridHeaderColumnCustom2 or PaletteBackStyle.GridHeaderColumnCustom3 or PaletteBackStyle.GridHeaderRowList or PaletteBackStyle.GridHeaderRowSheet or PaletteBackStyle.GridHeaderRowCustom1 or PaletteBackStyle.GridHeaderRowCustom2 or PaletteBackStyle.GridHeaderRowCustom3 or PaletteBackStyle.GridDataCellList or PaletteBackStyle.GridDataCellSheet or PaletteBackStyle.GridDataCellCustom1 or PaletteBackStyle.GridDataCellCustom2 or PaletteBackStyle.GridDataCellCustom3 => PaletteGraphicsHint.None,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the first background color.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBackColor1(PaletteBackStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
        {
            return GlobalStaticValues.EMPTY_COLOR;
        }

        switch (style)
        {
            case PaletteBackStyle.GridHeaderColumnList:
            case PaletteBackStyle.GridHeaderColumnSheet:
            case PaletteBackStyle.GridHeaderColumnCustom1:
            case PaletteBackStyle.GridHeaderColumnCustom2:
            case PaletteBackStyle.GridHeaderColumnCustom3:
            case PaletteBackStyle.GridHeaderRowList:
            case PaletteBackStyle.GridHeaderRowSheet:
            case PaletteBackStyle.GridHeaderRowCustom1:
            case PaletteBackStyle.GridHeaderRowCustom2:
            case PaletteBackStyle.GridHeaderRowCustom3:
                return state switch
                {
                    PaletteState.Disabled => SystemColors.Control,
                    PaletteState.CheckedNormal => ColorTable.CheckBackground,
                    _ => ColorTable.MenuStripGradientBegin
                };
            case PaletteBackStyle.GridDataCellList:
            case PaletteBackStyle.GridDataCellSheet:
            case PaletteBackStyle.GridDataCellCustom1:
            case PaletteBackStyle.GridDataCellCustom2:
            case PaletteBackStyle.GridDataCellCustom3:
                return state == PaletteState.CheckedNormal ? ColorTable.ButtonPressedHighlight : SystemColors.Window;

            case PaletteBackStyle.FormMain:
            case PaletteBackStyle.FormCustom1:
            case PaletteBackStyle.FormCustom2:
            case PaletteBackStyle.FormCustom3:
            case PaletteBackStyle.HeaderForm:
                if (state == PaletteState.Disabled)
                {
                    return SystemColors.GradientInactiveCaption;
                }
                return SystemColors.GradientActiveCaption;// ColorTable.MenuStripGradientBegin;
            case PaletteBackStyle.PanelClient:
            case PaletteBackStyle.PanelRibbonInactive:
            case PaletteBackStyle.PanelCustom1:
            case PaletteBackStyle.PanelCustom2:
            case PaletteBackStyle.PanelCustom3:
            case PaletteBackStyle.ControlGroupBox:
            case PaletteBackStyle.SeparatorLowProfile:
            case PaletteBackStyle.SeparatorCustom1:
            case PaletteBackStyle.SeparatorCustom2:
            case PaletteBackStyle.SeparatorCustom3:
            case PaletteBackStyle.GridBackgroundList:
            case PaletteBackStyle.GridBackgroundSheet:
            case PaletteBackStyle.GridBackgroundCustom1:
            case PaletteBackStyle.GridBackgroundCustom2:
            case PaletteBackStyle.GridBackgroundCustom3:
                return ColorTable.MenuStripGradientEnd;
            case PaletteBackStyle.PanelAlternate:
                return ColorTable.MenuStripGradientBegin;
            case PaletteBackStyle.Control:
            case PaletteBackStyle.ControlClient:
            case PaletteBackStyle.ControlAlternate:
            case PaletteBackStyle.ControlCustom1:
            case PaletteBackStyle.ControlCustom2:
            case PaletteBackStyle.ControlCustom3:
                return SystemColors.Window;
            case PaletteBackStyle.ContextMenuHeading:
                return ColorTable.OverflowButtonGradientBegin;
            case PaletteBackStyle.ContextMenuSeparator:
            case PaletteBackStyle.ContextMenuItemSplit:
                return state switch
                {
                    PaletteState.Disabled => SystemColors.Control,
                    PaletteState.Tracking => ColorTable.ButtonSelectedBorder,
                    _ => ColorTable.MenuBorder
                };
            case PaletteBackStyle.ContextMenuItemImageColumn:
                return ColorTable.ImageMarginGradientBegin;
            case PaletteBackStyle.ContextMenuItemImage:
                return ColorTable.ButtonSelectedHighlight;
            case PaletteBackStyle.InputControlStandalone:
            case PaletteBackStyle.InputControlRibbon:
            case PaletteBackStyle.InputControlCustom1:
            case PaletteBackStyle.InputControlCustom2:
            case PaletteBackStyle.InputControlCustom3:
                return state == PaletteState.Disabled ? SystemColors.Control : SystemColors.Window;

            case PaletteBackStyle.ControlRibbon:
                return BaseColors!.RibbonTabSelected4;
            case PaletteBackStyle.ControlRibbonAppMenu:
                return BaseColors!.AppButtonBack1;
            case PaletteBackStyle.ContextMenuOuter:
            case PaletteBackStyle.ContextMenuInner:
                return ColorTable.ToolStripDropDownBackground;
            case PaletteBackStyle.SeparatorHighInternalProfile:
            case PaletteBackStyle.SeparatorHighProfile:
            case PaletteBackStyle.HeaderPrimary:
            case PaletteBackStyle.HeaderCalendar:
            case PaletteBackStyle.HeaderCustom1:
            case PaletteBackStyle.HeaderCustom2:
            case PaletteBackStyle.HeaderCustom3:
                return state == PaletteState.Disabled ? SystemColors.Control : Table.Header1Begin;

            case PaletteBackStyle.HeaderDockInactive:
                return state == PaletteState.Disabled ? SystemColors.Control : SystemColors.InactiveCaption;

            case PaletteBackStyle.HeaderDockActive:
                return state == PaletteState.Disabled ? SystemColors.Control : SystemColors.ActiveCaption;

            case PaletteBackStyle.HeaderSecondary:
                return state == PaletteState.Disabled ? SystemColors.Control : ColorTable.MenuStripGradientEnd;

            case PaletteBackStyle.TabHighProfile:
            case PaletteBackStyle.TabStandardProfile:
            case PaletteBackStyle.TabLowProfile:
            case PaletteBackStyle.TabOneNote:
            case PaletteBackStyle.TabCustom1:
            case PaletteBackStyle.TabCustom2:
            case PaletteBackStyle.TabCustom3:
                return state switch
                {
                    PaletteState.Disabled => style == PaletteBackStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : SystemColors.Control,
                    PaletteState.Normal => style == PaletteBackStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : SystemColors.Window,
                    PaletteState.Pressed or PaletteState.Tracking => style switch
                    {
                        PaletteBackStyle.TabLowProfile => GlobalStaticValues.EMPTY_COLOR,
                        PaletteBackStyle.TabHighProfile => ColorTable.ButtonPressedGradientMiddle,
                        _ => SystemColors.Window
                    },
                    PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => style == PaletteBackStyle.TabHighProfile ? ColorTable.ButtonPressedGradientMiddle : SystemColors.Window,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBackStyle.TabDock:
            case PaletteBackStyle.TabDockAutoHidden:
                return state switch
                {
                    PaletteState.Disabled => SystemColors.Control,
                    PaletteState.Normal => SystemColors.Window,
                    PaletteState.Pressed or PaletteState.Tracking or PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => SystemColors.Window,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBackStyle.ControlToolTip:
                return _toolTipBack1;
            case PaletteBackStyle.ButtonStandalone:
            case PaletteBackStyle.ButtonGallery:
            case PaletteBackStyle.ButtonAlternate:
            case PaletteBackStyle.ButtonLowProfile:
            case PaletteBackStyle.ButtonBreadCrumb:
            case PaletteBackStyle.ButtonListItem:
            case PaletteBackStyle.ButtonCommand:
            case PaletteBackStyle.ButtonButtonSpec:
            case PaletteBackStyle.ButtonCluster:
            case PaletteBackStyle.ButtonNavigatorStack:
            case PaletteBackStyle.ButtonNavigatorOverflow:
            case PaletteBackStyle.ButtonNavigatorMini:
            case PaletteBackStyle.ButtonForm:
            case PaletteBackStyle.ButtonFormClose:
            case PaletteBackStyle.ButtonCustom1:
            case PaletteBackStyle.ButtonCustom2:
            case PaletteBackStyle.ButtonCustom3:
                return state switch
                {
                    PaletteState.Disabled => SystemColors.Control,
                    PaletteState.Normal => ColorTable.MenuStripGradientEnd,
                    PaletteState.CheckedNormal => ColorTable.ButtonPressedGradientEnd,
                    PaletteState.NormalDefaultOverride => ColorTable.MenuStripGradientBegin,
                    PaletteState.Tracking => ColorTable.ButtonSelectedGradientBegin,
                    PaletteState.Pressed or PaletteState.CheckedPressed => style == PaletteBackStyle.ButtonAlternate ? ColorTable.SeparatorDark : ColorTable.ButtonPressedGradientBegin,
                    PaletteState.CheckedTracking => ColorTable.ButtonPressedGradientBegin,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBackStyle.ButtonCalendarDay:
                return state switch
                {
                    PaletteState.Disabled => SystemColors.Control,
                    PaletteState.Normal or PaletteState.NormalDefaultOverride => ColorTable.MenuStripGradientEnd,
                    PaletteState.CheckedNormal => ColorTable.ButtonPressedGradientEnd,
                    PaletteState.Tracking => ColorTable.ButtonSelectedGradientBegin,
                    PaletteState.Pressed or PaletteState.CheckedPressed or PaletteState.CheckedTracking => ColorTable.ButtonPressedGradientBegin,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBackStyle.ButtonInputControl:
                return state switch
                {
                    PaletteState.Disabled => SystemColors.Control,
                    PaletteState.Normal => ColorTable.MenuStripGradientEnd,
                    PaletteState.CheckedNormal => ColorTable.MenuStripGradientEnd,
                    PaletteState.CheckedPressed or PaletteState.CheckedTracking or PaletteState.Tracking => ColorTable.ButtonSelectedGradientBegin,
                    PaletteState.Pressed => ColorTable.ButtonPressedGradientBegin,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBackStyle.ContextMenuItemHighlight:
                return state switch
                {
                    PaletteState.Disabled => SystemColors.Control,
                    PaletteState.Normal => GlobalStaticValues.EMPTY_COLOR,
                    PaletteState.Tracking => ColorTable.MenuItemSelectedGradientBegin,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            default:
                throw DebugTools.NotImplemented(style.ToString());
        }
    }

    /// <summary>
    /// Gets the second back color.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
        {
            return GlobalStaticValues.EMPTY_COLOR;
        }

        switch (style)
        {
            case PaletteBackStyle.GridHeaderColumnList:
            case PaletteBackStyle.GridHeaderColumnSheet:
            case PaletteBackStyle.GridHeaderColumnCustom1:
            case PaletteBackStyle.GridHeaderColumnCustom2:
            case PaletteBackStyle.GridHeaderColumnCustom3:
            case PaletteBackStyle.GridHeaderRowList:
            case PaletteBackStyle.GridHeaderRowSheet:
            case PaletteBackStyle.GridHeaderRowCustom1:
            case PaletteBackStyle.GridHeaderRowCustom2:
            case PaletteBackStyle.GridHeaderRowCustom3:
                return state switch
                {
                    PaletteState.Disabled => SystemColors.Control,
                    PaletteState.CheckedNormal => ColorTable.CheckBackground,
                    _ => ColorTable.MenuStripGradientBegin
                };
            case PaletteBackStyle.GridDataCellList:
            case PaletteBackStyle.GridDataCellSheet:
            case PaletteBackStyle.GridDataCellCustom1:
            case PaletteBackStyle.GridDataCellCustom2:
            case PaletteBackStyle.GridDataCellCustom3:
                return state == PaletteState.CheckedNormal ? ColorTable.ButtonPressedHighlight : SystemColors.Window;

            case PaletteBackStyle.FormMain:
            case PaletteBackStyle.FormCustom1:
            case PaletteBackStyle.FormCustom2:
            case PaletteBackStyle.FormCustom3:
            case PaletteBackStyle.HeaderForm:
                if (state == PaletteState.Disabled)
                {
                    return SystemColors.InactiveCaption;
                }

                return SystemColors.ActiveCaption; //ColorTable.MenuStripGradientBegin;
            case PaletteBackStyle.PanelClient:
            case PaletteBackStyle.PanelRibbonInactive:
            case PaletteBackStyle.PanelCustom1:
            case PaletteBackStyle.PanelCustom2:
            case PaletteBackStyle.PanelCustom3:
            case PaletteBackStyle.ControlGroupBox:
            case PaletteBackStyle.SeparatorLowProfile:
            case PaletteBackStyle.SeparatorCustom1:
            case PaletteBackStyle.SeparatorCustom2:
            case PaletteBackStyle.SeparatorCustom3:
            case PaletteBackStyle.GridBackgroundList:
            case PaletteBackStyle.GridBackgroundSheet:
            case PaletteBackStyle.GridBackgroundCustom1:
            case PaletteBackStyle.GridBackgroundCustom2:
            case PaletteBackStyle.GridBackgroundCustom3:
                return ColorTable.MenuStripGradientEnd;
            case PaletteBackStyle.PanelAlternate:
                return ColorTable.MenuStripGradientBegin;
            case PaletteBackStyle.Control:
            case PaletteBackStyle.ControlClient:
            case PaletteBackStyle.ControlAlternate:
            case PaletteBackStyle.ControlCustom1:
            case PaletteBackStyle.ControlCustom2:
            case PaletteBackStyle.ControlCustom3:
                return SystemColors.Window;
            case PaletteBackStyle.ContextMenuHeading:
                return ColorTable.OverflowButtonGradientBegin;
            case PaletteBackStyle.ContextMenuSeparator:
            case PaletteBackStyle.ContextMenuItemSplit:
                return state switch
                {
                    PaletteState.Disabled => SystemColors.Control,
                    PaletteState.Tracking => ColorTable.ButtonSelectedBorder,
                    _ => ColorTable.MenuBorder
                };
            case PaletteBackStyle.ContextMenuItemImageColumn:
                return ColorTable.ImageMarginGradientEnd;
            case PaletteBackStyle.InputControlStandalone:
            case PaletteBackStyle.InputControlRibbon:
            case PaletteBackStyle.InputControlCustom1:
            case PaletteBackStyle.InputControlCustom2:
            case PaletteBackStyle.InputControlCustom3:
                return state == PaletteState.Disabled ? SystemColors.Control : SystemColors.Window;

            case PaletteBackStyle.ControlRibbon:
                return BaseColors!.RibbonTabSelected4;
            case PaletteBackStyle.ControlRibbonAppMenu:
                return BaseColors!.AppButtonBack2;
            case PaletteBackStyle.ContextMenuOuter:
            case PaletteBackStyle.ContextMenuInner:
                return ColorTable.ToolStripDropDownBackground;
            case PaletteBackStyle.ContextMenuItemImage:
                return ColorTable.ButtonSelectedHighlight;
            case PaletteBackStyle.SeparatorHighInternalProfile:
            case PaletteBackStyle.SeparatorHighProfile:
            case PaletteBackStyle.HeaderPrimary:
            case PaletteBackStyle.HeaderCalendar:
            case PaletteBackStyle.HeaderCustom1:
            case PaletteBackStyle.HeaderCustom2:
            case PaletteBackStyle.HeaderCustom3:
                return state == PaletteState.Disabled ? SystemColors.Control : Table.Header1End;

            case PaletteBackStyle.HeaderSecondary:
                return state == PaletteState.Disabled ? SystemColors.Control : ColorTable.MenuStripGradientBegin;

            case PaletteBackStyle.HeaderDockInactive:
                return state == PaletteState.Disabled ? SystemColors.Control : ControlPaint.Light(SystemColors.GradientInactiveCaption);

            case PaletteBackStyle.HeaderDockActive:
                return state == PaletteState.Disabled ? SystemColors.Control : ControlPaint.Light(SystemColors.GradientActiveCaption);

            case PaletteBackStyle.TabHighProfile:
            case PaletteBackStyle.TabStandardProfile:
            case PaletteBackStyle.TabLowProfile:
            case PaletteBackStyle.TabOneNote:
            case PaletteBackStyle.TabCustom1:
            case PaletteBackStyle.TabCustom2:
            case PaletteBackStyle.TabCustom3:
                return state switch
                {
                    PaletteState.Disabled => style == PaletteBackStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : SystemColors.Control,
                    PaletteState.Normal => style == PaletteBackStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : MergeColors(SystemColors.Window, 0.9f, SystemColors.ControlText, 0.1f),
                    PaletteState.Tracking or PaletteState.Pressed => style == PaletteBackStyle.TabLowProfile
                        ? GlobalStaticValues.EMPTY_COLOR
                        : MergeColors(SystemColors.Window, 0.95f, SystemColors.ControlText, 0.05f),
                    PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => SystemColors.Window,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBackStyle.TabDock:
                return state switch
                {
                    PaletteState.Disabled => SystemColors.Control,
                    PaletteState.Normal => MergeColors(SystemColors.Control, 0.8f, SystemColors.ControlDark, 0.2f),
                    PaletteState.Pressed or PaletteState.Tracking => MergeColors(SystemColors.Window, 0.8f, SystemColors.Highlight, 0.2f),
                    PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => SystemColors.Window,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBackStyle.TabDockAutoHidden:
                return state switch
                {
                    PaletteState.Disabled => SystemColors.Control,
                    PaletteState.Normal or PaletteState.CheckedNormal => MergeColors(SystemColors.Control, 0.8f, SystemColors.ControlDark, 0.2f),
                    PaletteState.Pressed or PaletteState.CheckedPressed or PaletteState.Tracking or PaletteState.CheckedTracking => MergeColors(SystemColors.Window, 0.8f, SystemColors.Highlight, 0.2f),
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBackStyle.ControlToolTip:
                return _toolTipBack2;
            case PaletteBackStyle.ButtonStandalone:
            case PaletteBackStyle.ButtonGallery:
            case PaletteBackStyle.ButtonAlternate:
            case PaletteBackStyle.ButtonLowProfile:
            case PaletteBackStyle.ButtonBreadCrumb:
            case PaletteBackStyle.ButtonListItem:
            case PaletteBackStyle.ButtonCommand:
            case PaletteBackStyle.ButtonButtonSpec:
            case PaletteBackStyle.ButtonCluster:
            case PaletteBackStyle.ButtonNavigatorStack:
            case PaletteBackStyle.ButtonNavigatorOverflow:
            case PaletteBackStyle.ButtonNavigatorMini:
            case PaletteBackStyle.ButtonForm:
            case PaletteBackStyle.ButtonFormClose:
            case PaletteBackStyle.ButtonCustom1:
            case PaletteBackStyle.ButtonCustom2:
            case PaletteBackStyle.ButtonCustom3:
                return state switch
                {
                    PaletteState.Disabled => SystemColors.Control,
                    PaletteState.Normal => ColorTable.MenuStripGradientBegin,
                    PaletteState.CheckedNormal => ColorTable.ButtonPressedGradientMiddle,
                    PaletteState.NormalDefaultOverride => ColorTable.MenuStripGradientBegin,
                    PaletteState.Tracking => ColorTable.ButtonSelectedGradientEnd,
                    PaletteState.Pressed or PaletteState.CheckedPressed => style == PaletteBackStyle.ButtonAlternate ? ColorTable.MenuStripGradientBegin : ColorTable.ButtonPressedGradientEnd,
                    PaletteState.CheckedTracking => ColorTable.ButtonPressedGradientEnd,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBackStyle.ButtonCalendarDay:
                return state switch
                {
                    PaletteState.Disabled => SystemColors.Control,
                    PaletteState.Normal or PaletteState.NormalDefaultOverride => ColorTable.MenuStripGradientEnd,
                    PaletteState.CheckedNormal => ColorTable.ButtonPressedGradientEnd,
                    PaletteState.Tracking => ColorTable.ButtonSelectedGradientBegin,
                    PaletteState.Pressed or PaletteState.CheckedPressed or PaletteState.CheckedTracking => ColorTable.ButtonPressedGradientBegin,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBackStyle.ButtonInputControl:
                return state switch
                {
                    PaletteState.Disabled => SystemColors.Control,
                    PaletteState.Normal => ColorTable.MenuStripGradientBegin,
                    PaletteState.CheckedNormal => ColorTable.MenuStripGradientBegin,
                    PaletteState.CheckedTracking or PaletteState.CheckedPressed or PaletteState.Tracking => ColorTable.ButtonSelectedGradientEnd,
                    PaletteState.Pressed => ColorTable.ButtonPressedGradientEnd,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBackStyle.ContextMenuItemHighlight:
                return state switch
                {
                    PaletteState.Disabled => SystemColors.Control,
                    PaletteState.Normal => GlobalStaticValues.EMPTY_COLOR,
                    PaletteState.Tracking => ColorTable.MenuItemSelectedGradientEnd,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            default:
                throw DebugTools.NotImplemented(style.ToString());
        }
    }

    /// <summary>
    /// Gets the color background drawing style.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteColorStyle.Inherit;
        }

        return style switch
        {
            PaletteBackStyle.ContextMenuItemImageColumn or PaletteBackStyle.ControlToolTip or PaletteBackStyle.HeaderDockInactive or PaletteBackStyle.HeaderDockActive or PaletteBackStyle.GridHeaderColumnList or PaletteBackStyle.GridHeaderColumnSheet or PaletteBackStyle.GridHeaderColumnCustom1 or PaletteBackStyle.GridHeaderColumnCustom2 or PaletteBackStyle.GridHeaderColumnCustom3 or PaletteBackStyle.GridHeaderRowList or PaletteBackStyle.GridHeaderRowSheet or PaletteBackStyle.GridHeaderRowCustom1 or PaletteBackStyle.GridHeaderRowCustom2 or PaletteBackStyle.GridHeaderRowCustom3 => PaletteColorStyle.Linear,
            PaletteBackStyle.GridDataCellList or PaletteBackStyle.GridDataCellSheet or PaletteBackStyle.GridDataCellCustom1 or PaletteBackStyle.GridDataCellCustom2 or PaletteBackStyle.GridDataCellCustom3 or PaletteBackStyle.PanelClient or PaletteBackStyle.PanelRibbonInactive or PaletteBackStyle.PanelAlternate or PaletteBackStyle.PanelCustom1 or PaletteBackStyle.PanelCustom2 or PaletteBackStyle.PanelCustom3 or PaletteBackStyle.SeparatorLowProfile or PaletteBackStyle.SeparatorCustom1 or PaletteBackStyle.SeparatorCustom2 or PaletteBackStyle.SeparatorCustom3 or PaletteBackStyle.Control or PaletteBackStyle.ControlClient or PaletteBackStyle.ControlAlternate or PaletteBackStyle.ControlGroupBox or PaletteBackStyle.ControlRibbon or PaletteBackStyle.ControlCustom1 or PaletteBackStyle.ControlCustom2 or PaletteBackStyle.ControlCustom3 or PaletteBackStyle.ContextMenuOuter or PaletteBackStyle.ContextMenuInner or PaletteBackStyle.ContextMenuHeading or PaletteBackStyle.ContextMenuSeparator or PaletteBackStyle.ContextMenuItemSplit or PaletteBackStyle.ContextMenuItemImage or PaletteBackStyle.InputControlStandalone or PaletteBackStyle.InputControlRibbon or PaletteBackStyle.InputControlCustom1 or PaletteBackStyle.InputControlCustom2 or PaletteBackStyle.InputControlCustom3 or PaletteBackStyle.FormMain or PaletteBackStyle.FormCustom1 or PaletteBackStyle.FormCustom2 or PaletteBackStyle.FormCustom3 or PaletteBackStyle.GridBackgroundList or PaletteBackStyle.GridBackgroundSheet or PaletteBackStyle.GridBackgroundCustom1
                or PaletteBackStyle.GridBackgroundCustom2
                or PaletteBackStyle.GridBackgroundCustom3
                or PaletteBackStyle.HeaderCalendar or PaletteBackStyle.ButtonNavigatorMini => PaletteColorStyle.Solid,
            PaletteBackStyle.ControlRibbonAppMenu => PaletteColorStyle.Switch90,
            PaletteBackStyle.SeparatorHighInternalProfile or PaletteBackStyle.SeparatorHighProfile or PaletteBackStyle.HeaderPrimary or PaletteBackStyle.HeaderSecondary or PaletteBackStyle.HeaderForm or PaletteBackStyle.HeaderCustom1 or PaletteBackStyle.HeaderCustom2 or PaletteBackStyle.HeaderCustom3 or PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonGallery or PaletteBackStyle.ButtonAlternate or PaletteBackStyle.ButtonLowProfile or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonCalendarDay or PaletteBackStyle.ButtonCluster or PaletteBackStyle.ButtonNavigatorStack or PaletteBackStyle.ButtonNavigatorOverflow or PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose or PaletteBackStyle.ButtonCustom1 or PaletteBackStyle.ButtonCustom2 or PaletteBackStyle.ButtonCustom3 or PaletteBackStyle.ButtonInputControl or PaletteBackStyle.ContextMenuItemHighlight => PaletteColorStyle.Rounded,
            PaletteBackStyle.TabStandardProfile or PaletteBackStyle.TabLowProfile or PaletteBackStyle.TabCustom1 or PaletteBackStyle.TabCustom2 or PaletteBackStyle.TabCustom3 or PaletteBackStyle.TabHighProfile => PaletteColorStyle.QuarterPhase,
            PaletteBackStyle.TabOneNote or PaletteBackStyle.TabDock or PaletteBackStyle.TabDockAutoHidden => PaletteColorStyle.OneNote,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the color alignment.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetBackColorAlign(PaletteBackStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteRectangleAlign.Inherit;
        }

        return style switch
        {
            PaletteBackStyle.Control or PaletteBackStyle.ControlClient or PaletteBackStyle.ControlAlternate or PaletteBackStyle.ControlGroupBox or PaletteBackStyle.ControlRibbon or PaletteBackStyle.ControlRibbonAppMenu or PaletteBackStyle.ControlCustom1 or PaletteBackStyle.ControlCustom2 or PaletteBackStyle.ControlCustom3 or PaletteBackStyle.ContextMenuOuter or PaletteBackStyle.ContextMenuInner or PaletteBackStyle.ContextMenuHeading or PaletteBackStyle.ContextMenuSeparator or PaletteBackStyle.ContextMenuItemSplit or PaletteBackStyle.InputControlStandalone or PaletteBackStyle.InputControlRibbon or PaletteBackStyle.InputControlCustom1 or PaletteBackStyle.InputControlCustom2 or PaletteBackStyle.InputControlCustom3 or PaletteBackStyle.FormMain or PaletteBackStyle.FormCustom1 or PaletteBackStyle.FormCustom2 or PaletteBackStyle.FormCustom3 or PaletteBackStyle.PanelClient or PaletteBackStyle.PanelRibbonInactive or PaletteBackStyle.PanelAlternate or PaletteBackStyle.PanelCustom1 or PaletteBackStyle.PanelCustom2 or PaletteBackStyle.PanelCustom3 or PaletteBackStyle.GridBackgroundList or PaletteBackStyle.GridBackgroundSheet or PaletteBackStyle.GridBackgroundCustom1
                or PaletteBackStyle.GridBackgroundCustom2
                or PaletteBackStyle.GridBackgroundCustom3
                => PaletteRectangleAlign.Control,
            PaletteBackStyle.ControlToolTip or PaletteBackStyle.SeparatorLowProfile or PaletteBackStyle.SeparatorHighInternalProfile or PaletteBackStyle.SeparatorHighProfile or PaletteBackStyle.SeparatorCustom1 or PaletteBackStyle.SeparatorCustom2 or PaletteBackStyle.SeparatorCustom3 or PaletteBackStyle.HeaderPrimary or PaletteBackStyle.HeaderDockInactive or PaletteBackStyle.HeaderDockActive or PaletteBackStyle.HeaderCalendar or PaletteBackStyle.HeaderSecondary or PaletteBackStyle.HeaderForm or PaletteBackStyle.HeaderCustom1 or PaletteBackStyle.HeaderCustom2 or PaletteBackStyle.HeaderCustom3 or PaletteBackStyle.TabHighProfile or PaletteBackStyle.TabStandardProfile or PaletteBackStyle.TabLowProfile or PaletteBackStyle.TabOneNote or PaletteBackStyle.TabDock or PaletteBackStyle.TabDockAutoHidden or PaletteBackStyle.TabCustom1 or PaletteBackStyle.TabCustom2 or PaletteBackStyle.TabCustom3 or PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonGallery or PaletteBackStyle.ButtonAlternate or PaletteBackStyle.ButtonLowProfile or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonCalendarDay or PaletteBackStyle.ButtonCluster or PaletteBackStyle.ButtonNavigatorStack or PaletteBackStyle.ButtonNavigatorOverflow or PaletteBackStyle.ButtonNavigatorMini or PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose or PaletteBackStyle.ButtonCustom1 or PaletteBackStyle.ButtonCustom2 or PaletteBackStyle.ButtonCustom3 or PaletteBackStyle.ButtonInputControl or PaletteBackStyle.ContextMenuItemImage or PaletteBackStyle.ContextMenuItemHighlight or PaletteBackStyle.GridHeaderColumnList or PaletteBackStyle.GridHeaderColumnSheet or PaletteBackStyle.GridHeaderColumnCustom1 or PaletteBackStyle.GridHeaderColumnCustom2 or PaletteBackStyle.GridHeaderColumnCustom3 or PaletteBackStyle.GridHeaderRowList or PaletteBackStyle.GridHeaderRowSheet or PaletteBackStyle.GridHeaderRowCustom1 or PaletteBackStyle.GridHeaderRowCustom2 or PaletteBackStyle.GridHeaderRowCustom3 or PaletteBackStyle.GridDataCellList or PaletteBackStyle.GridDataCellSheet or PaletteBackStyle.GridDataCellCustom1 or PaletteBackStyle.GridDataCellCustom2 or PaletteBackStyle.GridDataCellCustom3 or PaletteBackStyle.ContextMenuItemImageColumn => PaletteRectangleAlign.Local,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the color background angle.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetBackColorAngle(PaletteBackStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return -1f;
        }

        return style switch
        {
            PaletteBackStyle.PanelClient or PaletteBackStyle.PanelRibbonInactive or PaletteBackStyle.PanelAlternate or PaletteBackStyle.PanelCustom1 or PaletteBackStyle.PanelCustom2 or PaletteBackStyle.PanelCustom3 or PaletteBackStyle.SeparatorLowProfile or PaletteBackStyle.SeparatorHighInternalProfile or PaletteBackStyle.SeparatorHighProfile or PaletteBackStyle.SeparatorCustom1 or PaletteBackStyle.SeparatorCustom2 or PaletteBackStyle.SeparatorCustom3 or PaletteBackStyle.Control or PaletteBackStyle.ControlClient or PaletteBackStyle.ControlAlternate or PaletteBackStyle.ControlGroupBox or PaletteBackStyle.ControlToolTip or PaletteBackStyle.ControlRibbon or PaletteBackStyle.ControlRibbonAppMenu or PaletteBackStyle.ControlCustom1 or PaletteBackStyle.ControlCustom2 or PaletteBackStyle.ControlCustom3 or PaletteBackStyle.ContextMenuOuter or PaletteBackStyle.ContextMenuInner or PaletteBackStyle.ContextMenuHeading or PaletteBackStyle.ContextMenuSeparator or PaletteBackStyle.ContextMenuItemSplit or PaletteBackStyle.ContextMenuItemImage or PaletteBackStyle.ContextMenuItemHighlight or PaletteBackStyle.InputControlStandalone or PaletteBackStyle.InputControlRibbon or PaletteBackStyle.InputControlCustom1 or PaletteBackStyle.InputControlCustom2 or PaletteBackStyle.InputControlCustom3 or PaletteBackStyle.FormMain or PaletteBackStyle.FormCustom1 or PaletteBackStyle.FormCustom2 or PaletteBackStyle.FormCustom3 or PaletteBackStyle.HeaderPrimary or PaletteBackStyle.HeaderDockInactive or PaletteBackStyle.HeaderDockActive or PaletteBackStyle.HeaderCalendar or PaletteBackStyle.HeaderSecondary or PaletteBackStyle.HeaderForm or PaletteBackStyle.HeaderCustom1 or PaletteBackStyle.HeaderCustom2 or PaletteBackStyle.HeaderCustom3 or PaletteBackStyle.TabHighProfile or PaletteBackStyle.TabStandardProfile or PaletteBackStyle.TabLowProfile or PaletteBackStyle.TabOneNote or PaletteBackStyle.TabDock or PaletteBackStyle.TabDockAutoHidden or PaletteBackStyle.TabCustom1 or PaletteBackStyle.TabCustom2 or PaletteBackStyle.TabCustom3 or PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonGallery or PaletteBackStyle.ButtonAlternate or PaletteBackStyle.ButtonLowProfile or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonCalendarDay or PaletteBackStyle.ButtonCluster or PaletteBackStyle.ButtonNavigatorStack or PaletteBackStyle.ButtonNavigatorOverflow or PaletteBackStyle.ButtonNavigatorMini or PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose or PaletteBackStyle.ButtonCustom1 or PaletteBackStyle.ButtonCustom2 or PaletteBackStyle.ButtonCustom3 or PaletteBackStyle.ButtonInputControl or PaletteBackStyle.GridBackgroundList or PaletteBackStyle.GridBackgroundSheet or PaletteBackStyle.GridBackgroundCustom1
                or PaletteBackStyle.GridBackgroundCustom2
                or PaletteBackStyle.GridBackgroundCustom3
                or PaletteBackStyle.GridHeaderColumnList or PaletteBackStyle.GridHeaderColumnSheet or PaletteBackStyle.GridHeaderColumnCustom1 or PaletteBackStyle.GridHeaderColumnCustom2 or PaletteBackStyle.GridHeaderColumnCustom3 or PaletteBackStyle.GridHeaderRowList or PaletteBackStyle.GridHeaderRowSheet or PaletteBackStyle.GridHeaderRowCustom1 or PaletteBackStyle.GridHeaderRowCustom2 or PaletteBackStyle.GridHeaderRowCustom3 or PaletteBackStyle.GridDataCellList or PaletteBackStyle.GridDataCellSheet or PaletteBackStyle.GridDataCellCustom1 or PaletteBackStyle.GridDataCellCustom2 or PaletteBackStyle.GridDataCellCustom3 => 90f,
            PaletteBackStyle.ContextMenuItemImageColumn => 0f,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets a background image.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetBackImage(PaletteBackStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return null;
        }

        return style switch
        {
            PaletteBackStyle.PanelClient or PaletteBackStyle.PanelRibbonInactive or PaletteBackStyle.PanelAlternate or PaletteBackStyle.PanelCustom1 or PaletteBackStyle.PanelCustom2 or PaletteBackStyle.PanelCustom3 or PaletteBackStyle.SeparatorLowProfile or PaletteBackStyle.SeparatorHighInternalProfile or PaletteBackStyle.SeparatorHighProfile or PaletteBackStyle.SeparatorCustom1 or PaletteBackStyle.SeparatorCustom2 or PaletteBackStyle.SeparatorCustom3 or PaletteBackStyle.Control or PaletteBackStyle.ControlClient or PaletteBackStyle.ControlAlternate or PaletteBackStyle.ControlGroupBox or PaletteBackStyle.ControlToolTip or PaletteBackStyle.ControlRibbon or PaletteBackStyle.ControlRibbonAppMenu or PaletteBackStyle.ControlCustom1 or PaletteBackStyle.ControlCustom2 or PaletteBackStyle.ControlCustom3 or PaletteBackStyle.ContextMenuOuter or PaletteBackStyle.ContextMenuInner or PaletteBackStyle.ContextMenuHeading or PaletteBackStyle.ContextMenuSeparator or PaletteBackStyle.ContextMenuItemSplit or PaletteBackStyle.ContextMenuItemImageColumn or PaletteBackStyle.InputControlStandalone or PaletteBackStyle.InputControlRibbon or PaletteBackStyle.InputControlCustom1 or PaletteBackStyle.InputControlCustom2 or PaletteBackStyle.InputControlCustom3 or PaletteBackStyle.FormMain or PaletteBackStyle.FormCustom1 or PaletteBackStyle.FormCustom2 or PaletteBackStyle.FormCustom3 or PaletteBackStyle.HeaderPrimary or PaletteBackStyle.HeaderDockInactive or PaletteBackStyle.HeaderDockActive or PaletteBackStyle.HeaderCalendar or PaletteBackStyle.HeaderSecondary or PaletteBackStyle.HeaderForm or PaletteBackStyle.HeaderCustom1 or PaletteBackStyle.HeaderCustom2 or PaletteBackStyle.HeaderCustom3 or PaletteBackStyle.TabHighProfile or PaletteBackStyle.TabStandardProfile or PaletteBackStyle.TabLowProfile or PaletteBackStyle.TabOneNote or PaletteBackStyle.TabDock or PaletteBackStyle.TabDockAutoHidden or PaletteBackStyle.TabCustom1 or PaletteBackStyle.TabCustom2 or PaletteBackStyle.TabCustom3 or PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonGallery or PaletteBackStyle.ButtonAlternate or PaletteBackStyle.ButtonLowProfile or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonCalendarDay or PaletteBackStyle.ButtonCluster or PaletteBackStyle.ButtonNavigatorStack or PaletteBackStyle.ButtonNavigatorOverflow or PaletteBackStyle.ButtonNavigatorMini or PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose or PaletteBackStyle.ButtonCustom1 or PaletteBackStyle.ButtonCustom2 or PaletteBackStyle.ButtonCustom3 or PaletteBackStyle.ButtonInputControl or PaletteBackStyle.ContextMenuItemImage or PaletteBackStyle.ContextMenuItemHighlight or PaletteBackStyle.GridBackgroundList or PaletteBackStyle.GridBackgroundSheet or PaletteBackStyle.GridBackgroundCustom1
                or PaletteBackStyle.GridBackgroundCustom2
                or PaletteBackStyle.GridBackgroundCustom3
                or PaletteBackStyle.GridHeaderColumnList or PaletteBackStyle.GridHeaderColumnSheet or PaletteBackStyle.GridHeaderColumnCustom1 or PaletteBackStyle.GridHeaderColumnCustom2 or PaletteBackStyle.GridHeaderColumnCustom3 or PaletteBackStyle.GridHeaderRowList or PaletteBackStyle.GridHeaderRowSheet or PaletteBackStyle.GridHeaderRowCustom1 or PaletteBackStyle.GridHeaderRowCustom2 or PaletteBackStyle.GridHeaderRowCustom3 or PaletteBackStyle.GridDataCellList or PaletteBackStyle.GridDataCellSheet or PaletteBackStyle.GridDataCellCustom1 or PaletteBackStyle.GridDataCellCustom2 or PaletteBackStyle.GridDataCellCustom3 => null,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetBackImageStyle(PaletteBackStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteImageStyle.Inherit;
        }

        return style switch
        {
            PaletteBackStyle.PanelClient or PaletteBackStyle.PanelRibbonInactive or PaletteBackStyle.PanelAlternate or PaletteBackStyle.PanelCustom1 or PaletteBackStyle.PanelCustom2 or PaletteBackStyle.PanelCustom3 or PaletteBackStyle.SeparatorLowProfile or PaletteBackStyle.SeparatorHighInternalProfile or PaletteBackStyle.SeparatorHighProfile or PaletteBackStyle.SeparatorCustom1 or PaletteBackStyle.SeparatorCustom2 or PaletteBackStyle.SeparatorCustom3 or PaletteBackStyle.Control or PaletteBackStyle.ControlClient or PaletteBackStyle.ControlAlternate or PaletteBackStyle.ControlGroupBox or PaletteBackStyle.ControlToolTip or PaletteBackStyle.ControlRibbon or PaletteBackStyle.ControlRibbonAppMenu or PaletteBackStyle.ControlCustom1 or PaletteBackStyle.ControlCustom2 or PaletteBackStyle.ControlCustom3 or PaletteBackStyle.ContextMenuOuter or PaletteBackStyle.ContextMenuInner or PaletteBackStyle.ContextMenuHeading or PaletteBackStyle.ContextMenuSeparator or PaletteBackStyle.ContextMenuItemSplit or PaletteBackStyle.ContextMenuItemImageColumn or PaletteBackStyle.InputControlStandalone or PaletteBackStyle.InputControlRibbon or PaletteBackStyle.InputControlCustom1 or PaletteBackStyle.InputControlCustom2 or PaletteBackStyle.InputControlCustom3 or PaletteBackStyle.FormMain or PaletteBackStyle.FormCustom1 or PaletteBackStyle.FormCustom2 or PaletteBackStyle.FormCustom3 or PaletteBackStyle.HeaderPrimary or PaletteBackStyle.HeaderDockInactive or PaletteBackStyle.HeaderDockActive or PaletteBackStyle.HeaderCalendar or PaletteBackStyle.HeaderSecondary or PaletteBackStyle.HeaderForm or PaletteBackStyle.HeaderCustom1 or PaletteBackStyle.HeaderCustom2 or PaletteBackStyle.HeaderCustom3 or PaletteBackStyle.TabHighProfile or PaletteBackStyle.TabStandardProfile or PaletteBackStyle.TabLowProfile or PaletteBackStyle.TabOneNote or PaletteBackStyle.TabDock or PaletteBackStyle.TabDockAutoHidden or PaletteBackStyle.TabCustom1 or PaletteBackStyle.TabCustom2 or PaletteBackStyle.TabCustom3 or PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonGallery or PaletteBackStyle.ButtonAlternate or PaletteBackStyle.ButtonLowProfile or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonCalendarDay or PaletteBackStyle.ButtonCluster or PaletteBackStyle.ButtonNavigatorStack or PaletteBackStyle.ButtonNavigatorOverflow or PaletteBackStyle.ButtonNavigatorMini or PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose or PaletteBackStyle.ButtonCustom1 or PaletteBackStyle.ButtonCustom2 or PaletteBackStyle.ButtonCustom3 or PaletteBackStyle.ButtonInputControl or PaletteBackStyle.ContextMenuItemImage or PaletteBackStyle.ContextMenuItemHighlight or PaletteBackStyle.GridBackgroundList or PaletteBackStyle.GridBackgroundSheet or PaletteBackStyle.GridBackgroundCustom1
                or PaletteBackStyle.GridBackgroundCustom2
                or PaletteBackStyle.GridBackgroundCustom3
                or PaletteBackStyle.GridHeaderColumnList or PaletteBackStyle.GridHeaderColumnSheet or PaletteBackStyle.GridHeaderColumnCustom1 or PaletteBackStyle.GridHeaderColumnCustom2 or PaletteBackStyle.GridHeaderColumnCustom3 or PaletteBackStyle.GridHeaderRowList or PaletteBackStyle.GridHeaderRowSheet or PaletteBackStyle.GridHeaderRowCustom1 or PaletteBackStyle.GridHeaderRowCustom2 or PaletteBackStyle.GridHeaderRowCustom3 or PaletteBackStyle.GridDataCellList or PaletteBackStyle.GridDataCellSheet or PaletteBackStyle.GridDataCellCustom1 or PaletteBackStyle.GridDataCellCustom2 or PaletteBackStyle.GridDataCellCustom3 => PaletteImageStyle.Tile,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the image alignment.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetBackImageAlign(PaletteBackStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteRectangleAlign.Inherit;
        }

        return style switch
        {
            PaletteBackStyle.PanelClient or PaletteBackStyle.PanelRibbonInactive or PaletteBackStyle.PanelAlternate or PaletteBackStyle.PanelCustom1 or PaletteBackStyle.PanelCustom2 or PaletteBackStyle.PanelCustom3 or PaletteBackStyle.SeparatorLowProfile or PaletteBackStyle.SeparatorHighInternalProfile or PaletteBackStyle.SeparatorHighProfile or PaletteBackStyle.SeparatorCustom1 or PaletteBackStyle.SeparatorCustom2 or PaletteBackStyle.SeparatorCustom3 or PaletteBackStyle.Control or PaletteBackStyle.ControlClient or PaletteBackStyle.ControlAlternate or PaletteBackStyle.ControlGroupBox or PaletteBackStyle.ControlToolTip or PaletteBackStyle.ControlRibbon or PaletteBackStyle.ControlRibbonAppMenu or PaletteBackStyle.ControlCustom1 or PaletteBackStyle.ControlCustom2 or PaletteBackStyle.ControlCustom3 or PaletteBackStyle.ContextMenuOuter or PaletteBackStyle.ContextMenuInner or PaletteBackStyle.ContextMenuHeading or PaletteBackStyle.ContextMenuSeparator or PaletteBackStyle.ContextMenuItemSplit or PaletteBackStyle.ContextMenuItemImageColumn or PaletteBackStyle.InputControlStandalone or PaletteBackStyle.InputControlRibbon or PaletteBackStyle.InputControlCustom1 or PaletteBackStyle.InputControlCustom2 or PaletteBackStyle.InputControlCustom3 or PaletteBackStyle.FormMain or PaletteBackStyle.FormCustom1 or PaletteBackStyle.FormCustom2 or PaletteBackStyle.FormCustom3 or PaletteBackStyle.HeaderPrimary or PaletteBackStyle.HeaderDockInactive or PaletteBackStyle.HeaderDockActive or PaletteBackStyle.HeaderCalendar or PaletteBackStyle.HeaderSecondary or PaletteBackStyle.HeaderForm or PaletteBackStyle.HeaderCustom1 or PaletteBackStyle.HeaderCustom2 or PaletteBackStyle.HeaderCustom3 or PaletteBackStyle.TabHighProfile or PaletteBackStyle.TabStandardProfile or PaletteBackStyle.TabLowProfile or PaletteBackStyle.TabOneNote or PaletteBackStyle.TabDock or PaletteBackStyle.TabDockAutoHidden or PaletteBackStyle.TabCustom1 or PaletteBackStyle.TabCustom2 or PaletteBackStyle.TabCustom3 or PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonGallery or PaletteBackStyle.ButtonAlternate or PaletteBackStyle.ButtonLowProfile or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonCalendarDay or PaletteBackStyle.ButtonCluster or PaletteBackStyle.ButtonNavigatorStack or PaletteBackStyle.ButtonNavigatorOverflow or PaletteBackStyle.ButtonNavigatorMini or PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose or PaletteBackStyle.ButtonCustom1 or PaletteBackStyle.ButtonCustom2 or PaletteBackStyle.ButtonCustom3 or PaletteBackStyle.ButtonInputControl or PaletteBackStyle.ContextMenuItemImage or PaletteBackStyle.ContextMenuItemHighlight or PaletteBackStyle.GridBackgroundList or PaletteBackStyle.GridBackgroundSheet or PaletteBackStyle.GridBackgroundCustom1
                or PaletteBackStyle.GridBackgroundCustom2
                or PaletteBackStyle.GridBackgroundCustom3
                or PaletteBackStyle.GridHeaderColumnList or PaletteBackStyle.GridHeaderColumnSheet or PaletteBackStyle.GridHeaderColumnCustom1 or PaletteBackStyle.GridHeaderColumnCustom2 or PaletteBackStyle.GridHeaderColumnCustom3 or PaletteBackStyle.GridHeaderRowList or PaletteBackStyle.GridHeaderRowSheet or PaletteBackStyle.GridHeaderRowCustom1 or PaletteBackStyle.GridHeaderRowCustom2 or PaletteBackStyle.GridHeaderRowCustom3 or PaletteBackStyle.GridDataCellList or PaletteBackStyle.GridDataCellSheet or PaletteBackStyle.GridDataCellCustom1 or PaletteBackStyle.GridDataCellCustom2 or PaletteBackStyle.GridDataCellCustom3 => PaletteRectangleAlign.Local,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }
    #endregion

    #region Border

    /// <summary>
    /// Gets a value indicating if border should be drawn.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetBorderDraw(PaletteBorderStyle style, PaletteState state)
    {
        // Check for the calendar day today override
        if (state == PaletteState.TodayOverride)
        {
            if (style == PaletteBorderStyle.ButtonCalendarDay)
            {
                return InheritBool.True;
            }
        }

        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return InheritBool.Inherit;
        }

        return style switch
        {
            PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit => InheritBool.False,
            PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => InheritBool.True,
            PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight => state switch
            {
                PaletteState.Normal or PaletteState.NormalDefaultOverride => InheritBool.False,
                _ => InheritBool.True
            },
            PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow => InheritBool.False,
            PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb => state switch
            {
                PaletteState.Disabled or PaletteState.Normal or PaletteState.NormalDefaultOverride => InheritBool.False,
                _ => InheritBool.True
            },
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets a value indicating which borders to draw.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteDrawBorders value.</returns>
    public override PaletteDrawBorders GetBorderDrawBorders(PaletteBorderStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteDrawBorders.Inherit;
        }

        return style switch
        {
            PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => PaletteDrawBorders.All,
            PaletteBorderStyle.ContextMenuHeading => PaletteDrawBorders.Bottom,
            PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit => PaletteDrawBorders.None,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the graphics drawing hint for the border.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public override PaletteGraphicsHint GetBorderGraphicsHint(PaletteBorderStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteGraphicsHint.Inherit;
        }

        return style switch
        {
            PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 => PaletteGraphicsHint.AntiAlias,
            PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => PaletteGraphicsHint.None,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the first border color.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBorderColor1(PaletteBorderStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            if (state == PaletteState.TodayOverride)
            {
                if (style == PaletteBorderStyle.ButtonCalendarDay)
                {
                    return ColorTable.ButtonPressedBorder;
                }
            }

            return GlobalStaticValues.EMPTY_COLOR;
        }

        switch (style)
        {
            case PaletteBorderStyle.FormMain:
            case PaletteBorderStyle.FormCustom1:
            case PaletteBorderStyle.FormCustom2:
            case PaletteBorderStyle.FormCustom3:
            case PaletteBorderStyle.HeaderForm:
                if (state == PaletteState.Disabled)
                {
                    return SystemColors.InactiveCaption; // ColorTable.MenuBorder;
                }

                return SystemColors.ActiveCaption;// ColorTable.MenuBorder;
            case PaletteBorderStyle.ControlToolTip:
                return state == PaletteState.Disabled ? FadedColor(ColorTable.ButtonSelectedBorder) : _toolTipBorder;

            case PaletteBorderStyle.HeaderCalendar:
                return state == PaletteState.Disabled ? SystemColors.Control : Table.Header1Begin;

            case PaletteBorderStyle.SeparatorLowProfile:
            case PaletteBorderStyle.SeparatorHighInternalProfile:
            case PaletteBorderStyle.SeparatorHighProfile:
            case PaletteBorderStyle.SeparatorCustom1:
            case PaletteBorderStyle.SeparatorCustom2:
            case PaletteBorderStyle.SeparatorCustom3:
            case PaletteBorderStyle.ControlClient:
            case PaletteBorderStyle.ControlAlternate:
            case PaletteBorderStyle.ControlGroupBox:
            case PaletteBorderStyle.ControlCustom1:
            case PaletteBorderStyle.ControlCustom2:
            case PaletteBorderStyle.ControlCustom3:
            case PaletteBorderStyle.HeaderPrimary:
            case PaletteBorderStyle.HeaderDockInactive:
            case PaletteBorderStyle.HeaderDockActive:
            case PaletteBorderStyle.HeaderSecondary:
            case PaletteBorderStyle.HeaderCustom1:
            case PaletteBorderStyle.HeaderCustom2:
            case PaletteBorderStyle.HeaderCustom3:
            case PaletteBorderStyle.GridHeaderColumnList:
            case PaletteBorderStyle.GridHeaderColumnSheet:
            case PaletteBorderStyle.GridHeaderColumnCustom1:
            case PaletteBorderStyle.GridHeaderColumnCustom2:
            case PaletteBorderStyle.GridHeaderColumnCustom3:
            case PaletteBorderStyle.GridHeaderRowList:
            case PaletteBorderStyle.GridHeaderRowSheet:
            case PaletteBorderStyle.GridHeaderRowCustom1:
            case PaletteBorderStyle.GridHeaderRowCustom2:
            case PaletteBorderStyle.GridHeaderRowCustom3:
                return state == PaletteState.Disabled ? FadedColor(ColorTable.ButtonSelectedBorder) : ColorTable.MenuBorder;

            case PaletteBorderStyle.ContextMenuHeading:
                return ColorTable.MenuBorder;
            case PaletteBorderStyle.ContextMenuSeparator:
            case PaletteBorderStyle.ContextMenuItemSplit:
                return state switch
                {
                    PaletteState.Disabled => SystemColors.Control,
                    PaletteState.Tracking => ColorTable.ButtonSelectedBorder,
                    _ => ColorTable.MenuBorder
                };
            case PaletteBorderStyle.ContextMenuItemImageColumn:
                return ColorTable.ToolStripDropDownBackground;
            case PaletteBorderStyle.InputControlStandalone:
            case PaletteBorderStyle.InputControlCustom1:
            case PaletteBorderStyle.InputControlCustom2:
            case PaletteBorderStyle.InputControlCustom3:
                return state == PaletteState.Disabled ? FadedColor(ColorTable.ButtonSelectedBorder) : ColorTable.MenuBorder;

            case PaletteBorderStyle.InputControlRibbon:
                return state switch
                {
                    PaletteState.Disabled => FadedColor(ColorTable.ButtonSelectedBorder),
                    PaletteState.Normal => ColorTable.MenuStripGradientBegin,
                    _ => ColorTable.ButtonSelectedBorder
                };
            case PaletteBorderStyle.GridDataCellList:
            case PaletteBorderStyle.GridDataCellCustom1:
            case PaletteBorderStyle.GridDataCellCustom2:
            case PaletteBorderStyle.GridDataCellCustom3:
            case PaletteBorderStyle.GridDataCellSheet:
                return state == PaletteState.Disabled ? FadedColor(ColorTable.ButtonSelectedBorder) : ColorTable.SeparatorDark;

            case PaletteBorderStyle.ControlRibbon:
                return state == PaletteState.Disabled
                    ? FadedColor(ColorTable.ButtonSelectedBorder)
                    : BaseColors!.RibbonGroupsArea1;

            case PaletteBorderStyle.ControlRibbonAppMenu:
                return state == PaletteState.Disabled
                    ? FadedColor(BaseColors!.AppButtonBorder)
                    : BaseColors!.AppButtonBorder;

            case PaletteBorderStyle.ContextMenuOuter:
            case PaletteBorderStyle.ContextMenuInner:
                return ColorTable.MenuBorder;
            case PaletteBorderStyle.ContextMenuItemImage:
                return ColorTable.MenuItemBorder;
            case PaletteBorderStyle.TabHighProfile:
            case PaletteBorderStyle.TabStandardProfile:
            case PaletteBorderStyle.TabLowProfile:
            case PaletteBorderStyle.TabOneNote:
            case PaletteBorderStyle.TabCustom1:
            case PaletteBorderStyle.TabCustom2:
            case PaletteBorderStyle.TabCustom3:
                return state switch
                {
                    PaletteState.Disabled => style == PaletteBorderStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : FadedColor(ColorTable.ButtonSelectedBorder),
                    PaletteState.Normal or PaletteState.Tracking or PaletteState.Pressed => style == PaletteBorderStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : ColorTable.OverflowButtonGradientEnd,
                    PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => ColorTable.MenuBorder,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBorderStyle.TabDock:
                return state switch
                {
                    PaletteState.Disabled => FadedColor(ColorTable.ButtonSelectedBorder),
                    PaletteState.Normal => ColorTable.OverflowButtonGradientEnd,
                    PaletteState.Pressed or PaletteState.Tracking => MergeColors(ColorTable.OverflowButtonGradientEnd, 0.5f, SystemColors.Highlight, 0.5f),
                    PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => ColorTable.MenuBorder,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBorderStyle.TabDockAutoHidden:
                return state switch
                {
                    PaletteState.Disabled => FadedColor(ColorTable.ButtonSelectedBorder),
                    PaletteState.Normal or PaletteState.CheckedNormal => ColorTable.OverflowButtonGradientEnd,
                    PaletteState.Pressed or PaletteState.CheckedPressed or PaletteState.Tracking or PaletteState.CheckedTracking => MergeColors(ColorTable.OverflowButtonGradientEnd, 0.5f, SystemColors.Highlight, 0.5f),
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBorderStyle.ButtonStandalone:
            case PaletteBorderStyle.ButtonGallery:
            case PaletteBorderStyle.ButtonAlternate:
            case PaletteBorderStyle.ButtonLowProfile:
            case PaletteBorderStyle.ButtonBreadCrumb:
            case PaletteBorderStyle.ButtonListItem:
            case PaletteBorderStyle.ButtonCommand:
            case PaletteBorderStyle.ButtonButtonSpec:
            case PaletteBorderStyle.ButtonCluster:
            case PaletteBorderStyle.ButtonNavigatorStack:
            case PaletteBorderStyle.ButtonNavigatorOverflow:
            case PaletteBorderStyle.ButtonNavigatorMini:
            case PaletteBorderStyle.ButtonForm:
            case PaletteBorderStyle.ButtonFormClose:
            case PaletteBorderStyle.ButtonCustom1:
            case PaletteBorderStyle.ButtonCustom2:
            case PaletteBorderStyle.ButtonCustom3:
            case PaletteBorderStyle.ButtonInputControl:
            case PaletteBorderStyle.ContextMenuItemHighlight:
                return state switch
                {
                    PaletteState.Disabled => FadedColor(ColorTable.ButtonSelectedBorder),
                    PaletteState.Normal => ColorTable.MenuBorder,
                    PaletteState.CheckedNormal => ColorTable.MenuBorder,
                    PaletteState.Tracking => ColorTable.ButtonSelectedBorder,
                    PaletteState.Pressed or PaletteState.CheckedPressed => style == PaletteBorderStyle.ButtonAlternate ? ColorTable.SeparatorDark : ColorTable.ButtonPressedBorder,
                    PaletteState.CheckedTracking => ColorTable.ButtonPressedBorder,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBorderStyle.ButtonCalendarDay:
                return state switch
                {
                    PaletteState.Disabled => SystemColors.Control,
                    PaletteState.Normal => ColorTable.MenuStripGradientEnd,
                    PaletteState.CheckedNormal => ColorTable.ButtonPressedGradientEnd,
                    PaletteState.NormalDefaultOverride => ColorTable.MenuStripGradientBegin,
                    PaletteState.Tracking => ColorTable.ButtonSelectedGradientBegin,
                    PaletteState.Pressed or PaletteState.CheckedPressed => ColorTable.ButtonPressedGradientBegin,
                    PaletteState.CheckedTracking => ColorTable.ButtonPressedGradientBegin,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            default:
                throw DebugTools.NotImplemented(style.ToString());
        }
    }

    /// <summary>
    /// Gets the second border color.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            if (state == PaletteState.TodayOverride)
            {
                if (style == PaletteBorderStyle.ButtonCalendarDay)
                {
                    return ColorTable.ButtonPressedBorder;
                }
            }

            return GlobalStaticValues.EMPTY_COLOR;
        }

        switch (style)
        {
            case PaletteBorderStyle.FormMain:
            case PaletteBorderStyle.FormCustom1:
            case PaletteBorderStyle.FormCustom2:
            case PaletteBorderStyle.FormCustom3:
            case PaletteBorderStyle.HeaderForm:
                if (state == PaletteState.Disabled)
                {
                    return SystemColors.InactiveCaption;
                }

                return SystemColors.ActiveCaption;
            case PaletteBorderStyle.ControlToolTip:
                return state == PaletteState.Disabled ? FadedColor(ColorTable.ButtonSelectedBorder) : _toolTipBorder;

            case PaletteBorderStyle.SeparatorLowProfile:
            case PaletteBorderStyle.SeparatorHighInternalProfile:
            case PaletteBorderStyle.SeparatorHighProfile:
            case PaletteBorderStyle.SeparatorCustom1:
            case PaletteBorderStyle.SeparatorCustom2:
            case PaletteBorderStyle.SeparatorCustom3:
            case PaletteBorderStyle.ControlClient:
            case PaletteBorderStyle.ControlAlternate:
            case PaletteBorderStyle.ControlGroupBox:
            case PaletteBorderStyle.ControlCustom1:
            case PaletteBorderStyle.ControlCustom2:
            case PaletteBorderStyle.ControlCustom3:
            case PaletteBorderStyle.HeaderPrimary:
            case PaletteBorderStyle.HeaderDockInactive:
            case PaletteBorderStyle.HeaderDockActive:
            case PaletteBorderStyle.HeaderSecondary:
            case PaletteBorderStyle.HeaderCustom1:
            case PaletteBorderStyle.HeaderCustom2:
            case PaletteBorderStyle.HeaderCustom3:
            case PaletteBorderStyle.GridHeaderColumnList:
            case PaletteBorderStyle.GridHeaderColumnSheet:
            case PaletteBorderStyle.GridHeaderColumnCustom1:
            case PaletteBorderStyle.GridHeaderColumnCustom2:
            case PaletteBorderStyle.GridHeaderColumnCustom3:
            case PaletteBorderStyle.GridHeaderRowList:
            case PaletteBorderStyle.GridHeaderRowSheet:
            case PaletteBorderStyle.GridHeaderRowCustom1:
            case PaletteBorderStyle.GridHeaderRowCustom2:
            case PaletteBorderStyle.GridHeaderRowCustom3:
            case PaletteBorderStyle.GridDataCellList:
            case PaletteBorderStyle.GridDataCellSheet:
            case PaletteBorderStyle.GridDataCellCustom1:
            case PaletteBorderStyle.GridDataCellCustom2:
            case PaletteBorderStyle.GridDataCellCustom3:
                return state == PaletteState.Disabled ? FadedColor(ColorTable.ButtonSelectedBorder) : ColorTable.MenuBorder;

            case PaletteBorderStyle.HeaderCalendar:
                return state == PaletteState.Disabled ? SystemColors.Control : Table.Header1Begin;

            case PaletteBorderStyle.ContextMenuHeading:
                return ColorTable.MenuBorder;
            case PaletteBorderStyle.ContextMenuSeparator:
            case PaletteBorderStyle.ContextMenuItemSplit:
                return state switch
                {
                    PaletteState.Disabled => SystemColors.Control,
                    PaletteState.Tracking => ColorTable.ButtonSelectedBorder,
                    _ => ColorTable.MenuBorder
                };
            case PaletteBorderStyle.ContextMenuItemImageColumn:
                return ColorTable.ToolStripDropDownBackground;
            case PaletteBorderStyle.ContextMenuItemImage:
                return ColorTable.MenuItemBorder;
            case PaletteBorderStyle.InputControlStandalone:
            case PaletteBorderStyle.InputControlCustom1:
            case PaletteBorderStyle.InputControlCustom2:
            case PaletteBorderStyle.InputControlCustom3:
                return state == PaletteState.Disabled ? FadedColor(ColorTable.ButtonSelectedBorder) : ColorTable.MenuBorder;

            case PaletteBorderStyle.InputControlRibbon:
                return state switch
                {
                    PaletteState.Disabled => FadedColor(ColorTable.ButtonSelectedBorder),
                    PaletteState.Normal => ColorTable.MenuStripGradientBegin,
                    _ => ColorTable.ButtonSelectedBorder
                };
            case PaletteBorderStyle.ControlRibbon:
                return state == PaletteState.Disabled
                    ? FadedColor(ColorTable.ButtonSelectedBorder)
                    : BaseColors!.RibbonGroupsArea1;

            case PaletteBorderStyle.ControlRibbonAppMenu:
                return state == PaletteState.Disabled
                    ? FadedColor(BaseColors!.AppButtonBorder)
                    : BaseColors!.AppButtonBorder;

            case PaletteBorderStyle.ContextMenuOuter:
            case PaletteBorderStyle.ContextMenuInner:
                return ColorTable.MenuBorder;
            case PaletteBorderStyle.TabHighProfile:
            case PaletteBorderStyle.TabStandardProfile:
            case PaletteBorderStyle.TabLowProfile:
            case PaletteBorderStyle.TabOneNote:
            case PaletteBorderStyle.TabCustom1:
            case PaletteBorderStyle.TabCustom2:
            case PaletteBorderStyle.TabCustom3:
                return state switch
                {
                    PaletteState.Disabled => style == PaletteBorderStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : FadedColor(ColorTable.ButtonSelectedBorder),
                    PaletteState.Normal or PaletteState.Tracking or PaletteState.Pressed => style == PaletteBorderStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : ColorTable.ButtonPressedHighlightBorder,
                    PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => ColorTable.MenuBorder,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBorderStyle.TabDock:
                return state switch
                {
                    PaletteState.Disabled => FadedColor(ColorTable.ButtonSelectedBorder),
                    PaletteState.Normal => ColorTable.OverflowButtonGradientEnd,
                    PaletteState.Pressed or PaletteState.Tracking => MergeColors(ColorTable.OverflowButtonGradientEnd, 0.5f, SystemColors.Highlight, 0.5f),
                    PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => ColorTable.MenuBorder,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBorderStyle.TabDockAutoHidden:
                return state switch
                {
                    PaletteState.Disabled => FadedColor(ColorTable.ButtonSelectedBorder),
                    PaletteState.Normal or PaletteState.CheckedNormal => ColorTable.OverflowButtonGradientEnd,
                    PaletteState.Pressed or PaletteState.CheckedPressed or PaletteState.Tracking or PaletteState.CheckedTracking => MergeColors(ColorTable.OverflowButtonGradientEnd, 0.5f, SystemColors.Highlight, 0.5f),
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBorderStyle.ButtonStandalone:
            case PaletteBorderStyle.ButtonGallery:
            case PaletteBorderStyle.ButtonAlternate:
            case PaletteBorderStyle.ButtonLowProfile:
            case PaletteBorderStyle.ButtonBreadCrumb:
            case PaletteBorderStyle.ButtonListItem:
            case PaletteBorderStyle.ButtonCommand:
            case PaletteBorderStyle.ButtonButtonSpec:
            case PaletteBorderStyle.ButtonCluster:
            case PaletteBorderStyle.ButtonNavigatorStack:
            case PaletteBorderStyle.ButtonNavigatorOverflow:
            case PaletteBorderStyle.ButtonNavigatorMini:
            case PaletteBorderStyle.ButtonForm:
            case PaletteBorderStyle.ButtonFormClose:
            case PaletteBorderStyle.ButtonCustom1:
            case PaletteBorderStyle.ButtonCustom2:
            case PaletteBorderStyle.ButtonCustom3:
            case PaletteBorderStyle.ButtonInputControl:
            case PaletteBorderStyle.ContextMenuItemHighlight:
                return state switch
                {
                    PaletteState.Disabled => FadedColor(ColorTable.ButtonSelectedBorder),
                    PaletteState.Normal => ColorTable.MenuBorder,
                    PaletteState.CheckedNormal => ColorTable.MenuBorder,
                    PaletteState.Tracking => ColorTable.ButtonSelectedBorder,
                    PaletteState.Pressed or PaletteState.CheckedPressed => style == PaletteBorderStyle.ButtonAlternate ? ColorTable.SeparatorDark : ColorTable.ButtonPressedBorder,
                    PaletteState.CheckedTracking => ColorTable.ButtonPressedBorder,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBorderStyle.ButtonCalendarDay:
                return state switch
                {
                    PaletteState.Disabled => SystemColors.Control,
                    PaletteState.Normal => ColorTable.MenuStripGradientEnd,
                    PaletteState.CheckedNormal => ColorTable.ButtonPressedGradientEnd,
                    PaletteState.NormalDefaultOverride => ColorTable.MenuStripGradientBegin,
                    PaletteState.Tracking => ColorTable.ButtonSelectedGradientBegin,
                    PaletteState.Pressed or PaletteState.CheckedPressed => ColorTable.ButtonPressedGradientBegin,
                    PaletteState.CheckedTracking => ColorTable.ButtonPressedGradientBegin,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            default:
                throw DebugTools.NotImplemented(style.ToString());
        }
    }

    /// <summary>
    /// Gets the color border drawing style.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetBorderColorStyle(PaletteBorderStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteColorStyle.Inherit;
        }

        return style switch
        {
            PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => PaletteColorStyle.Solid,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the color border alignment.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetBorderColorAlign(PaletteBorderStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteRectangleAlign.Inherit;
        }

        return style switch
        {
            PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 => PaletteRectangleAlign.Control,
            PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => PaletteRectangleAlign.Local,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the color border angle.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetBorderColorAngle(PaletteBorderStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return -1f;
        }

        return style switch
        {
            PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => 90f,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the border width.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Integer width.</returns>
    public override int GetBorderWidth(PaletteBorderStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return -1;
        }

        return style switch
        {
            PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit => 0,
            PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => 1,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the border corner rounding.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Float rounding.</returns>
    public override float GetBorderRounding(PaletteBorderStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return GlobalStaticValues.DEFAULT_MATERIAL_THEME_CORNER_ROUNDING_VALUE;
        }

        return style switch
        {
            PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => 0,
            PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlGroupBox => 3,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets a border image.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetBorderImage(PaletteBorderStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return null;
        }

        return style switch
        {
            PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => null,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the border image style.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetBorderImageStyle(PaletteBorderStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteImageStyle.Inherit;
        }

        return style switch
        {
            PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => PaletteImageStyle.Tile,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the image border alignment.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetBorderImageAlign(PaletteBorderStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteRectangleAlign.Inherit;
        }

        return style switch
        {
            PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => PaletteRectangleAlign.Local,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }
    #endregion

    #region Content

    /// <summary>
    /// Gets a value indicating if content should be drawn.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentDraw(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return InheritBool.Inherit;
        }

        // Always draw everything
        return InheritBool.True;
    }

    /// <summary>
    /// Gets a value indicating if content should be drawn with focus indication.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentDrawFocus(PaletteContentStyle style, PaletteState state)
    {
        // By default, the focus override shows the focus!
        if (state == PaletteState.FocusOverride)
        {
            return InheritBool.True;
        }

        // We do not override the other override states
        if (CommonHelper.IsOverrideState(state))
        {
            return InheritBool.Inherit;
        }

        // By default, never show the focus indication, we let individual controls
        // override this functionality as required by the controls requirements
        return InheritBool.False;
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentImageH(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteRelativeAlign.Inherit;
        }

        return style switch
        {
            PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 or PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate => PaletteRelativeAlign.Near,
            PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ButtonNavigatorMini => PaletteRelativeAlign.Center,
            PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl => PaletteRelativeAlign.Center,
            PaletteContentStyle.ContextMenuItemShortcutText => PaletteRelativeAlign.Far,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the vertical relative alignment of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentImageV(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteRelativeAlign.Inherit;
        }

        return style switch
        {
            PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 or PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl => PaletteRelativeAlign.Center,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the effect applied to drawing of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteImageEffect value.</returns>
    public override PaletteImageEffect GetContentImageEffect(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteImageEffect.Inherit;
        }

        return style switch
        {
            PaletteContentStyle.HeaderForm => PaletteImageEffect.Normal,
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => state == PaletteState.Disabled ? PaletteImageEffect.Disabled : PaletteImageEffect.Normal,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the image color to remap into another color.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentImageColorMap(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return GlobalStaticValues.EMPTY_COLOR;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => GlobalStaticValues.EMPTY_COLOR,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the color to use in place of the image map color.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentImageColorTo(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return GlobalStaticValues.EMPTY_COLOR;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => GlobalStaticValues.EMPTY_COLOR,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the image color that should be transparent.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentImageColorTransparent(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return GlobalStaticValues.EMPTY_COLOR;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => GlobalStaticValues.EMPTY_COLOR,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the font for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font? GetContentShortTextFont(PaletteContentStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            return (state == PaletteState.BoldedOverride) && (style == PaletteContentStyle.ButtonCalendarDay) ? CalendarBoldFont : null;
        }

        return style switch
        {
            PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 or PaletteContentStyle.HeaderCalendar => GridFont,
            PaletteContentStyle.HeaderForm => HeaderFormFont,
            PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.ButtonCommand => Header1ShortFont,
            PaletteContentStyle.LabelSuperTip or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage => SuperToolFont,
            PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemShortcutText => Header2ShortFont,
            PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelBoldPanel => BoldFont,
            PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelItalicControl => ItalicFont,
            PaletteContentStyle.ContextMenuItemTextAlternate => SuperToolFont,
            PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden => TabFontNormal,
            PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 => state switch
            {
                PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => TabFontSelected,
                _ => TabFontNormal
            },
            PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl => ButtonFont,
            PaletteContentStyle.ButtonNavigatorMini => ButtonFontNavigatorMini,
            PaletteContentStyle.ButtonCalendarDay => CalendarFont,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the font for the short text by generating a new font instance.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font? GetContentShortTextNewFont(PaletteContentStyle style, PaletteState state)
    {
        DefineFonts();
        return GetContentShortTextFont(style, state);
    }

    /// <summary>
    /// Gets the rendering hint for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public override PaletteTextHint GetContentShortTextHint(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteTextHint.Inherit;
        }

        return PaletteTextHint.ClearTypeGridFit;
    }

    /// <summary>
    /// Gets the prefix drawing setting for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public override PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteTextHotkeyPrefix.Inherit;
        }

        return style switch
        {
            PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.HeaderForm => PaletteTextHotkeyPrefix.Show,
            PaletteContentStyle.ButtonListItem or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemShortcutText => PaletteTextHotkeyPrefix.None,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentShortTextMultiLine(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return InheritBool.Inherit;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => InheritBool.True,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the text trimming to use for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public override PaletteTextTrim GetContentShortTextTrim(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteTextTrim.Inherit;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteTextTrim.EllipsisCharacter,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextH(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteRelativeAlign.Inherit;
        }

        return style switch
        {
            PaletteContentStyle.HeaderForm
                or PaletteContentStyle.HeaderPrimary
                or PaletteContentStyle.HeaderDockInactive
                or PaletteContentStyle.HeaderDockActive
                or PaletteContentStyle.HeaderSecondary
                or PaletteContentStyle.HeaderCustom1
                or PaletteContentStyle.HeaderCustom2
                or PaletteContentStyle.HeaderCustom3
                or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRelativeAlign.Near,
            PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.HeaderCalendar => PaletteRelativeAlign.Center,
            PaletteContentStyle.ContextMenuItemShortcutText => PaletteRelativeAlign.Far,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the vertical relative alignment of the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextV(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteRelativeAlign.Inherit;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRelativeAlign.Center,
            PaletteContentStyle.LabelSuperTip => PaletteRelativeAlign.Near,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the horizontal relative alignment of multiline short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteRelativeAlign.Inherit;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRelativeAlign.Near,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the first back color for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
    {
        // Always work out value for an override state
        if (CommonHelper.IsOverrideState(state))
        {
            return state switch
            {
                PaletteState.LinkNotVisitedOverride => Color.Blue,
                PaletteState.LinkVisitedOverride => Color.Purple,
                PaletteState.LinkPressedOverride => Color.Red,
                _ => GlobalStaticValues.EMPTY_COLOR // All other override states do nothing
            };
        }

        switch (style)
        {
            case PaletteContentStyle.HeaderForm:
                if (state == PaletteState.Disabled)
                {
                    return SystemColors.InactiveCaptionText;
                }
                return SystemColors.ActiveCaptionText;
        }

        return style switch
        {
            PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip => SystemColors.InfoText,
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 => ColorTable.SeparatorLight,
            PaletteContentStyle.HeaderDockInactive => SystemColors.InactiveCaptionText,
            PaletteContentStyle.HeaderDockActive => SystemColors.ActiveCaptionText,
            PaletteContentStyle.HeaderSecondary or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => SystemColors.ControlText,
            PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 => state == PaletteState.CheckedNormal ? SystemColors.HighlightText : SystemColors.ControlText,
            PaletteContentStyle.TabDock => state switch
            {
                PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed => SystemColors.ControlText,
                _ => MergeColors(SystemColors.Window, 0.3f, SystemColors.ControlText, 0.7f)
            },
            PaletteContentStyle.TabDockAutoHidden => MergeColors(SystemColors.Window, 0.3f, SystemColors.ControlText, 0.7f),
            PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText => ColorTable.MenuItemText,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return GlobalStaticValues.EMPTY_COLOR;
        }

        switch (style)
        {
            case PaletteContentStyle.HeaderForm:
                return ColorTable.SeparatorLight;
        }

        if ((state == PaletteState.Disabled) &&
            (style != PaletteContentStyle.ButtonInputControl))
        {
            return SystemColors.InactiveCaptionText;
        }

        return style switch
        {
            PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip => _toolTipText,
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 => ColorTable.SeparatorLight,
            PaletteContentStyle.HeaderDockInactive => SystemColors.InactiveCaptionText,
            PaletteContentStyle.HeaderDockActive => SystemColors.ActiveCaptionText,
            PaletteContentStyle.HeaderSecondary or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => SystemColors.ControlText,
            PaletteContentStyle.TabDock => state switch
            {
                PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed => SystemColors.ControlText,
                _ => MergeColors(SystemColors.Window, 0.3f, SystemColors.ControlText, 0.7f)
            },
            PaletteContentStyle.TabDockAutoHidden => MergeColors(SystemColors.Window, 0.3f, SystemColors.ControlText, 0.7f),
            PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 => state == PaletteState.CheckedNormal ? SystemColors.HighlightText : SystemColors.ControlText,
            PaletteContentStyle.ButtonInputControl => Color.Transparent,
            PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText => ColorTable.MenuItemText,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the color drawing style for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetContentShortTextColorStyle(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteColorStyle.Inherit;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteColorStyle.Solid,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the color alignment for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetContentShortTextColorAlign(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteRectangleAlign.Inherit;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRectangleAlign.Local,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the color background angle for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetContentShortTextColorAngle(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return -1f;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => 90f,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets a background image for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetContentShortTextImage(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return null;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => null,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetContentShortTextImageStyle(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteImageStyle.Inherit;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteImageStyle.TileFlipXY,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the image alignment for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetContentShortTextImageAlign(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteRectangleAlign.Inherit;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRectangleAlign.Local,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the font for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font? GetContentLongTextFont(PaletteContentStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            return (state == PaletteState.BoldedOverride) && (style == PaletteContentStyle.ButtonCalendarDay) ? CalendarBoldFont : null;
        }

        return style switch
        {
            PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 or PaletteContentStyle.HeaderCalendar => GridFont,
            PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 => Header1LongFont,
            PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.HeaderSecondary => Header2LongFont,
            PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden => TabFontNormal,
            PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 => state switch
            {
                PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => TabFontSelected,
                _ => TabFontNormal
            },
            PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl => ButtonFont,
            PaletteContentStyle.ButtonCalendarDay => CalendarFont,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the font for the long text by generating a new font instance.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font? GetContentLongTextNewFont(PaletteContentStyle style, PaletteState state)
    {
        DefineFonts();
        return GetContentLongTextFont(style, state);
    }

    /// <summary>
    /// Gets the rendering hint for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public override PaletteTextHint GetContentLongTextHint(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteTextHint.Inherit;
        }

        return PaletteTextHint.ClearTypeGridFit;
    }

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentLongTextMultiLine(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return InheritBool.Inherit;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl => InheritBool.True,
            PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => InheritBool.False,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the text trimming to use for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public override PaletteTextTrim GetContentLongTextTrim(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteTextTrim.Inherit;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteTextTrim.EllipsisCharacter,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the prefix drawing setting for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public override PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteTextHotkeyPrefix.Inherit;
        }

        return style switch
        {
            PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl => PaletteTextHotkeyPrefix.Show,
            PaletteContentStyle.ButtonListItem or PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteTextHotkeyPrefix.None,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextH(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteRelativeAlign.Inherit;
        }

        return style switch
        {
            PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.ContextMenuItemTextAlternate => PaletteRelativeAlign.Near,
            PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRelativeAlign.Far,
            PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnSheet => PaletteRelativeAlign.Center,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the vertical relative alignment of the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextV(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteRelativeAlign.Inherit;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRelativeAlign.Center,
            PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.ContextMenuItemTextAlternate => PaletteRelativeAlign.Far,
            PaletteContentStyle.LabelSuperTip => PaletteRelativeAlign.Center,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the horizontal relative alignment of multiline long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteRelativeAlign.Inherit;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRelativeAlign.Center,
            PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ButtonCommand => PaletteRelativeAlign.Near,
            PaletteContentStyle.ContextMenuItemShortcutText => PaletteRelativeAlign.Near,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the first back color for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return GlobalStaticValues.EMPTY_COLOR;
        }

        switch (style)
        {
            case PaletteContentStyle.HeaderForm:
                return ColorTable.SeparatorLight;
        }

        if (state == PaletteState.Disabled)
        {
            return SystemColors.InactiveCaptionText;
        }

        return style switch
        {
            PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelKeyTip => _toolTipText,
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 => ColorTable.SeparatorLight,
            PaletteContentStyle.HeaderDockInactive => SystemColors.InactiveCaptionText,
            PaletteContentStyle.HeaderDockActive => SystemColors.ActiveCaptionText,
            PaletteContentStyle.HeaderSecondary or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => SystemColors.ControlText,
            PaletteContentStyle.TabDock => state switch
            {
                PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed => SystemColors.ControlText,
                _ => MergeColors(SystemColors.Window, 0.3f, SystemColors.ControlText, 0.7f)
            },
            PaletteContentStyle.TabDockAutoHidden => MergeColors(SystemColors.Window, 0.3f, SystemColors.ControlText, 0.7f),
            PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText => ColorTable.MenuItemText,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the second back color for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return GlobalStaticValues.EMPTY_COLOR;
        }

        switch (style)
        {
            case PaletteContentStyle.HeaderForm:
                return ColorTable.SeparatorLight;
        }

        if ((state == PaletteState.Disabled) &&
            (style != PaletteContentStyle.ButtonInputControl))
        {
            return SystemColors.ControlDark;
        }

        return style switch
        {
            PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelKeyTip => _toolTipText,
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 => ColorTable.SeparatorLight,
            PaletteContentStyle.HeaderDockInactive => SystemColors.InactiveCaptionText,
            PaletteContentStyle.HeaderDockActive => SystemColors.ActiveCaptionText,
            PaletteContentStyle.HeaderSecondary or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => SystemColors.ControlText,
            PaletteContentStyle.TabDock => state switch
            {
                PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed => SystemColors.ControlText,
                _ => MergeColors(SystemColors.Window, 0.3f, SystemColors.ControlText, 0.7f)
            },
            PaletteContentStyle.TabDockAutoHidden => MergeColors(SystemColors.Window, 0.3f, SystemColors.ControlText, 0.7f),
            PaletteContentStyle.ButtonInputControl => Color.Transparent,
            PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText => ColorTable.MenuItemText,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the color drawing style for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetContentLongTextColorStyle(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteColorStyle.Inherit;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteColorStyle.Solid,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the color alignment for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetContentLongTextColorAlign(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteRectangleAlign.Inherit;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRectangleAlign.Local,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the color background angle for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetContentLongTextColorAngle(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return -1f;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => 90f,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets a background image for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetContentLongTextImage(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return null;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => null,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the background image style for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetContentLongTextImageStyle(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteImageStyle.Inherit;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteImageStyle.TileFlipXY,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the image alignment for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetContentLongTextImageAlign(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteRectangleAlign.Inherit;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRectangleAlign.Local,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }

    /// <summary>
    /// Gets the padding between the border and content drawing.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Padding value.</returns>
    public override Padding GetBorderContentPadding(KryptonForm? owningForm, PaletteContentStyle style,
        PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return CommonHelper.InheritPadding;
        }

        switch (style)
        {
            case PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet
                or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2
                or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList
                or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1
                or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3
                or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet
                or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2
                or PaletteContentStyle.GridDataCellCustom3:
                return _contentPaddingGrid;
            case PaletteContentStyle.HeaderForm:
            {
                if (owningForm == null)
                {
                    return new Padding();
                }
                Padding borders = owningForm!.RealWindowBorders;
                return new Padding(borders.Left, borders.Bottom / 2, 0, 0);
            }
            case PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderCustom1
                or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3:
                return _contentPaddingHeader1;
            case PaletteContentStyle.HeaderSecondary:
                return _contentPaddingHeader2;
            case PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive:
                return _contentPaddingHeader3;
            case PaletteContentStyle.HeaderCalendar:
                return _contentPaddingCalendar;
            case PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl
                or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl
                or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel
                or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel
                or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2
                or PaletteContentStyle.LabelCustom3:
                return _contentPaddingLabel;
            case PaletteContentStyle.LabelGroupBoxCaption:
                return _contentPaddingLabel2;
            case PaletteContentStyle.ContextMenuItemTextStandard:
                return _contentPaddingContextMenuItemText;
            case PaletteContentStyle.ContextMenuItemTextAlternate:
                return _contentPaddingContextMenuItemTextAlt;
            case PaletteContentStyle.ContextMenuItemShortcutText:
                return _contentPaddingContextMenuItemShortcutText;
            case PaletteContentStyle.LabelToolTip:
                return _contentPaddingToolTip;
            case PaletteContentStyle.LabelSuperTip:
                return _contentPaddingSuperTip;
            case PaletteContentStyle.LabelKeyTip:
                return _contentPaddingKeyTip;
            case PaletteContentStyle.ContextMenuHeading:
                return _contentPaddingContextMenuHeading;
            case PaletteContentStyle.ContextMenuItemImage:
                return _contentPaddingContextMenuImage;
            case PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon
                or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2
                or PaletteContentStyle.InputControlCustom3:
                return InputControlPadding;
            case PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonCommand
                or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile
                or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini
                or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2
                or PaletteContentStyle.ButtonCustom3:
                return _contentPaddingButton12;
            case PaletteContentStyle.ButtonInputControl:
                return _contentPaddingButtonInputControl;
            case PaletteContentStyle.ButtonCalendarDay:
                return _contentPaddingButtonCalendar;
            case PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonListItem:
                return _contentPaddingButton3;
            case PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow:
                return _contentPaddingButton4;
            case PaletteContentStyle.ButtonBreadCrumb:
                return _contentPaddingButton6;
            case PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose:
                return _contentPaddingButtonForm;
            case PaletteContentStyle.ButtonGallery:
                return _contentPaddingButtonGallery;
            case PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile
                or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote
                or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2
                or PaletteContentStyle.TabCustom3:
                return _contentPaddingButton5;
            case PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden:
                return _contentPaddingButton7;
            default:
                throw new ArgumentOutOfRangeException(nameof(style));
        }
    }

    /// <summary>
    /// Gets the padding between adjacent content items.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Integer value.</returns>
    public override int GetContentAdjacentGap(PaletteContentStyle style, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return -1;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => 1,
            PaletteContentStyle.LabelSuperTip => 5,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }
    #endregion

    #region Metric

    /// <summary>
    /// Gets an integer metric value.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>Integer value.</returns>
    public override int GetMetricInt(KryptonForm? owningForm, PaletteState state, PaletteMetricInt metric)
    {
        switch (metric)
        {
            case PaletteMetricInt.PageButtonInset:
            case PaletteMetricInt.RibbonTabGap:
            case PaletteMetricInt.HeaderButtonEdgeInsetCalendar:
                return 2;
            case PaletteMetricInt.CheckButtonGap:
                return 5;
            case PaletteMetricInt.HeaderButtonEdgeInsetForm:
                if (owningForm == null)
                {
                    return 0;
                }
                return Math.Max(2, owningForm!.RealWindowBorders.Right);
            case PaletteMetricInt.HeaderButtonEdgeInsetInputControl:
                return 1;
            case PaletteMetricInt.HeaderButtonEdgeInsetPrimary:
            case PaletteMetricInt.HeaderButtonEdgeInsetSecondary:
            case PaletteMetricInt.HeaderButtonEdgeInsetDockInactive:
            case PaletteMetricInt.HeaderButtonEdgeInsetDockActive:
            case PaletteMetricInt.HeaderButtonEdgeInsetCustom1:
            case PaletteMetricInt.HeaderButtonEdgeInsetCustom2:
            case PaletteMetricInt.HeaderButtonEdgeInsetCustom3:
            case PaletteMetricInt.BarButtonEdgeOutside:
            case PaletteMetricInt.BarButtonEdgeInside:
                return 3;
            case PaletteMetricInt.None:
                return 0;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(metric.ToString());
                break;
        }

        return -1;
    }

    /// <summary>
    /// Gets a boolean metric value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric)
    {
        switch (metric)
        {
            case PaletteMetricBool.HeaderGroupOverlay:
            case PaletteMetricBool.SplitWithFading:
            case PaletteMetricBool.TreeViewLines:
                return InheritBool.True;
            case PaletteMetricBool.RibbonTabsSpareCaption:
                return InheritBool.False;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(metric.ToString());
                break;
        }

        return InheritBool.Inherit;
    }

    /// <summary>
    /// Gets a padding metric value.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>Padding value.</returns>
    public override Padding GetMetricPadding(KryptonForm? owningForm, PaletteState state,
        PaletteMetricPadding metric)
    {
        switch (metric)
        {
            case PaletteMetricPadding.PageButtonPadding:
                return _metricPaddingPageButtons;
            case PaletteMetricPadding.BarPaddingTabs:
                return _metricPaddingBarTabs;
            case PaletteMetricPadding.BarPaddingInside:
            case PaletteMetricPadding.BarPaddingOnly:
                return _metricPaddingBarInside;
            case PaletteMetricPadding.BarPaddingOutside:
                return _metricPaddingBarOutside;
            case PaletteMetricPadding.HeaderButtonPaddingForm:
                if (owningForm == null)
                {
                    return new Padding();
                }
                return new Padding(0, owningForm!.RealWindowBorders.Right, 0, 0);
            case PaletteMetricPadding.RibbonButtonPadding:
                return _metricPaddingRibbon;
            case PaletteMetricPadding.RibbonAppButton:
                return _metricPaddingRibbonAppButton;
            case PaletteMetricPadding.HeaderButtonPaddingInputControl:
                return _metricPaddingInputControl;
            case PaletteMetricPadding.HeaderButtonPaddingPrimary:
            case PaletteMetricPadding.HeaderButtonPaddingSecondary:
            case PaletteMetricPadding.HeaderButtonPaddingDockInactive:
            case PaletteMetricPadding.HeaderButtonPaddingDockActive:
            case PaletteMetricPadding.HeaderButtonPaddingCustom1:
            case PaletteMetricPadding.HeaderButtonPaddingCustom2:
            case PaletteMetricPadding.HeaderButtonPaddingCustom3:
            case PaletteMetricPadding.HeaderButtonPaddingCalendar:
            case PaletteMetricPadding.BarButtonPadding:
                return _metricPaddingHeader;
            case PaletteMetricPadding.ContextMenuItemHighlight:
                return _metricPaddingContextMenuItemHighlight;
            case PaletteMetricPadding.ContextMenuItemsCollection:
                return _metricPaddingContextMenuItemsCollection;
            case PaletteMetricPadding.ContextMenuItemOuter:
            case PaletteMetricPadding.HeaderGroupPaddingPrimary:
            case PaletteMetricPadding.HeaderGroupPaddingSecondary:
            case PaletteMetricPadding.HeaderGroupPaddingDockInactive:
            case PaletteMetricPadding.HeaderGroupPaddingDockActive:
            case PaletteMetricPadding.SeparatorPaddingLowProfile:
            case PaletteMetricPadding.SeparatorPaddingHighProfile:
            case PaletteMetricPadding.SeparatorPaddingHighInternalProfile:
            case PaletteMetricPadding.SeparatorPaddingCustom1:
            case PaletteMetricPadding.SeparatorPaddingCustom2:
            case PaletteMetricPadding.SeparatorPaddingCustom3:
                return Padding.Empty;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(metric.ToString());
                break;
        }

        return Padding.Empty;
    }
    #endregion

    #region Images

    /// <summary>
    /// Gets a tree view image appropriate for the provided state.
    /// </summary>
    /// <param name="expanded">Is the node expanded</param>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public override Image? GetTreeViewImage(bool expanded) => expanded ? _treeCollapseMinus : _treeExpandPlus;

    /// <summary>
    /// Gets a check box image appropriate for the provided state.
    /// </summary>
    /// <param name="enabled">Is the check box enabled.</param>
    /// <param name="checkState">Is the check box checked/unchecked/indeterminate.</param>
    /// <param name="tracking">Is the check box being hot tracked.</param>
    /// <param name="pressed">Is the check box being pressed.</param>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public override Image? GetCheckBoxImage(bool enabled, CheckState checkState, bool tracking, bool pressed) => null;  // null is intentional; Apparently ?!?!?

    /// <summary>
    /// Gets a check box image appropriate for the provided state.
    /// </summary>
    /// <param name="enabled">Is the radio button enabled.</param>
    /// <param name="checkState">Is the radio button checked.</param>
    /// <param name="tracking">Is the radio button being hot tracked.</param>
    /// <param name="pressed">Is the radio button being pressed.</param>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public override Image? GetRadioButtonImage(bool enabled, bool checkState, bool tracking, bool pressed) => null;  // null is intentional; Apparently ?!?!?

    /// <summary>
    /// Gets a checked image appropriate for a context menu item.
    /// </summary>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public override Image? GetContextMenuCheckedImage() => _contextMenuChecked;

    /// <summary>
    /// Gets a indeterminate image appropriate for a context menu item.
    /// </summary>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public override Image? GetContextMenuIndeterminateImage() => _contextMenuIndeterminate;

    /// <summary>
    /// Gets an image indicating a sub-menu on a context menu item.
    /// </summary>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public override Image? GetContextMenuSubMenuImage() => _contextMenuSubMenu;

    /// <summary>
    /// Gets a check box image appropriate for the provided state.
    /// </summary>
    /// <param name="button">Enum of the button to fetch.</param>
    /// <param name="state">State of the button to fetch.</param>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public override Image? GetGalleryButtonImage(PaletteRibbonGalleryButton button, PaletteState state) => button switch
    {
        PaletteRibbonGalleryButton.Down => _galleryImageDown ??= CreateGalleryDownImage(SystemColors.ControlText),
        PaletteRibbonGalleryButton.DropDown => _galleryImageDropDown ??= CreateGalleryDropDownImage(SystemColors.ControlText),
        _ => _galleryImageUp ??= CreateGalleryUpImage(SystemColors.ControlText)
    };
    #endregion

    #region ButtonSpec

    /// <summary>
    /// Gets the image to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <param name="state">State for which image is required.</param>
    /// <returns>Image value.</returns>
    public override Image? GetButtonSpecImage(PaletteButtonSpecStyle style,
        PaletteState state)
    {
        switch (style)
        {
            case PaletteButtonSpecStyle.Close:
                return _buttonSpecClose;
            case PaletteButtonSpecStyle.Context:
                return _buttonSpecContext;
            case PaletteButtonSpecStyle.Next:
                return _buttonSpecNext;
            case PaletteButtonSpecStyle.Previous:
                return _buttonSpecPrevious;
            case PaletteButtonSpecStyle.ArrowLeft:
                return _buttonSpecArrowLeft;
            case PaletteButtonSpecStyle.ArrowRight:
                return _buttonSpecArrowRight;
            case PaletteButtonSpecStyle.ArrowUp:
                return _buttonSpecArrowUp;
            case PaletteButtonSpecStyle.ArrowDown:
                return _buttonSpecArrowDown;
            case PaletteButtonSpecStyle.DropDown:
                return _buttonSpecDropDown;
            case PaletteButtonSpecStyle.PinVertical:
                return _buttonSpecPinVertical;
            case PaletteButtonSpecStyle.PinHorizontal:
                return _buttonSpecPinHorizontal;
            case PaletteButtonSpecStyle.WorkspaceMaximize:
                return _buttonSpecWorkspaceMaximize;
            case PaletteButtonSpecStyle.WorkspaceRestore:
                return _buttonSpecWorkspaceRestore;
            case PaletteButtonSpecStyle.RibbonMinimize:
                return state == PaletteState.Disabled ? _pendantMinimizeI : _pendantMinimizeA;

            case PaletteButtonSpecStyle.RibbonExpand:
                return state == PaletteState.Disabled ? _pendantExpandI : _pendantExpandA;

            case PaletteButtonSpecStyle.FormClose:
                return state == PaletteState.Disabled ? _systemCloseDisabled : _systemCloseNormal;

            case PaletteButtonSpecStyle.FormMin:
                return state == PaletteState.Disabled ? _systemMinimiseDisabled : _systemMinimiseNormal;

            case PaletteButtonSpecStyle.FormMax:
                return state == PaletteState.Disabled ? _systemMaximiseDisabled : _systemMaximiseNormal;

            case PaletteButtonSpecStyle.FormRestore:
                return state == PaletteState.Disabled ? _systemRestoreDisabled : _systemRestoreNormal;

            case PaletteButtonSpecStyle.FormHelp:
                return state == PaletteState.Disabled ? _systemHelpI : _systemHelpA;

            case PaletteButtonSpecStyle.PendantClose:
                return state == PaletteState.Disabled ? _pendantCloseI : _pendantCloseA;

            case PaletteButtonSpecStyle.PendantMin:
                return state == PaletteState.Disabled ? _pendantMinI : _pendantMinA;

            case PaletteButtonSpecStyle.PendantRestore:
                return state == PaletteState.Disabled ? _pendantRestoreI : _pendantRestoreA;
            case PaletteButtonSpecStyle.New:
                return state == PaletteState.Disabled ? _formToolbarButtonSpecNewDisabled : _formToolbarButtonSpecNewNormal;
            case PaletteButtonSpecStyle.Save:
                return state == PaletteState.Disabled ? _formToolbarButtonSpecSaveDisabled : _formToolbarButtonSpecSaveNormal;
            case PaletteButtonSpecStyle.SaveAs:
                return state == PaletteState.Disabled ? _formToolbarButtonSpecSaveAsDisabled : _formToolbarButtonSpecSaveAsNormal;
            case PaletteButtonSpecStyle.SaveAll:
                return state == PaletteState.Disabled ? _formToolbarButtonSpecSaveAllDisabled : _formToolbarButtonSpecSaveAllNormal;
            case PaletteButtonSpecStyle.Cut:
                return state == PaletteState.Disabled ? _formToolbarButtonSpecCutDisabled : _formToolbarButtonSpecCutNormal;
            case PaletteButtonSpecStyle.Copy:
                return state == PaletteState.Disabled ? _formToolbarButtonSpecCopyDisabled : _formToolbarButtonSpecCopyNormal;
            case PaletteButtonSpecStyle.Paste:
                return state == PaletteState.Disabled ? _formToolbarButtonSpecPasteDisabled : _formToolbarButtonSpecPasteNormal;
            case PaletteButtonSpecStyle.Undo:
                return state == PaletteState.Disabled ? _formToolbarButtonSpecUndoDisabled : _formToolbarButtonSpecUndoNormal;
            case PaletteButtonSpecStyle.Redo:
                return state == PaletteState.Disabled ? _formToolbarButtonSpecRedoDisabled : _formToolbarButtonSpecRedoNormal;
            case PaletteButtonSpecStyle.PageSetup:
                return state == PaletteState.Disabled ? _formToolbarButtonSpecPageSetupDisabled : _formToolbarButtonSpecPageSetupNormal;
            case PaletteButtonSpecStyle.PrintPreview:
                return state == PaletteState.Disabled ? _formToolbarButtonSpecPrintPreviewDisabled : _formToolbarButtonSpecPrintPreviewNormal;
            case PaletteButtonSpecStyle.Print:
                return state == PaletteState.Disabled ? _formToolbarButtonSpecPrintDisabled : _formToolbarButtonSpecPrintNormal;
            case PaletteButtonSpecStyle.QuickPrint:
                return state == PaletteState.Disabled ? _formToolbarButtonSpecQuickPrintDisabled : _formToolbarButtonSpecQuickPrintNormal;
            case PaletteButtonSpecStyle.Generic:
                return null;
            default:
                // Should never happen!
                Debug.Assert(false);
                throw DebugTools.NotImplemented(style.ToString());
        }
    }

    #endregion

    #region RibbonGeneral

    /// <summary>
    /// Gets the ribbon shape that should be used.
    /// </summary>
    /// <returns>Ribbon shape value.</returns>
    public override PaletteRibbonShape GetRibbonShape() => PaletteRibbonShape.Office2010;

    /// <summary>
    /// Gets the text alignment for the ribbon context text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override PaletteRelativeAlign GetRibbonContextTextAlign(PaletteState state) => PaletteRelativeAlign.Near;

    /// <summary>
    /// Gets the font for the ribbon context text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font GetRibbonContextTextFont(PaletteState state) => ButtonFont!;

    /// <summary>
    /// Gets the color for the ribbon context text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Color GetRibbonContextTextColor(PaletteState state) => _contextTextColor;

    /// <summary>
    /// Gets the dark disabled color used for ribbon glyphs.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonDisabledDark(PaletteState state) => _disabledGlyphDark;

    /// <summary>
    /// Gets the light disabled color used for ribbon glyphs.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonDisabledLight(PaletteState state) => _disabledGlyphLight;

    /// <summary>
    /// Gets the color for the drop arrow light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonDropArrowLight(PaletteState state) => BaseColors!.RibbonGroupDialogLight;

    /// <summary>
    /// Gets the color for the drop arrow dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonDropArrowDark(PaletteState state) => BaseColors!.RibbonGroupDialogDark;

    /// <summary>
    /// Gets the color for the dialog launcher dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonGroupDialogDark(PaletteState state) => BaseColors!.RibbonGroupDialogDark;

    /// <summary>
    /// Gets the color for the dialog launcher light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonGroupDialogLight(PaletteState state) => BaseColors!.RibbonGroupDialogLight;

    /// <summary>
    /// Gets the color for the group separator dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonGroupSeparatorDark(PaletteState state) => BaseColors!.RibbonGroupSeparatorDark;

    /// <summary>
    /// Gets the color for the group separator light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonGroupSeparatorLight(PaletteState state) => BaseColors!.RibbonGroupSeparatorLight;

    /// <summary>
    /// Gets the color for the minimize bar dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonMinimizeBarDark(PaletteState state) => BaseColors!.RibbonMinimizeBarDark;

    /// <summary>
    /// Gets the color for the minimize bar light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonMinimizeBarLight(PaletteState state) => BaseColors!.RibbonMinimizeBarLight;

    /// <summary>
    /// Gets the color for the tab separator.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonTabSeparatorColor(PaletteState state) => BaseColors!.RibbonTabSeparatorColor;

    /// <summary>
    /// Gets the color for the tab context separators.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonTabSeparatorContextColor(PaletteState state) => _contextTabSeparator;

    /// <summary>
    /// Gets the font for the ribbon text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font GetRibbonTextFont(PaletteState state) => ButtonFont!;

    /// <summary>
    /// Gets the rendering hint for the ribbon font.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public override PaletteTextHint GetRibbonTextHint(PaletteState state) => PaletteTextHint.ClearTypeGridFit;

    /// <summary>
    /// Gets the color for the extra QAT button dark content color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonQATButtonDark(PaletteState state) => BaseColors!.RibbonQATButtonDark;

    /// <summary>
    /// Gets the color for the extra QAT button light content color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonQATButtonLight(PaletteState state) => BaseColors!.RibbonQATButtonLight;

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
            case PaletteRibbonBackStyle.RibbonAppMenuDocs:
                return PaletteRibbonColorStyle.Solid;
            case PaletteRibbonBackStyle.RibbonAppMenuInner:
                return PaletteRibbonColorStyle.RibbonAppMenuInner;
            case PaletteRibbonBackStyle.RibbonAppMenuOuter:
                return PaletteRibbonColorStyle.RibbonAppMenuOuter;
            case PaletteRibbonBackStyle.RibbonQATMinibar:
                return state == PaletteState.CheckedNormal
                    ? PaletteRibbonColorStyle.RibbonQATMinibarDouble
                    : PaletteRibbonColorStyle.RibbonQATMinibarSingle;

            case PaletteRibbonBackStyle.RibbonQATFullbar:
                return PaletteRibbonColorStyle.RibbonQATFullbarSquare;
            case PaletteRibbonBackStyle.RibbonQATOverflow:
                return PaletteRibbonColorStyle.RibbonQATOverflow;
            case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
                return PaletteRibbonColorStyle.LinearBorder;
            case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                return state == PaletteState.Pressed ? PaletteRibbonColorStyle.Empty : PaletteRibbonColorStyle.Linear;

            case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                switch (state)
                {
                    case PaletteState.Normal:
                    case PaletteState.ContextNormal:
                        return PaletteRibbonColorStyle.RibbonGroupNormalBorderSep;
                    case PaletteState.Tracking:
                    case PaletteState.ContextTracking:
                        return PaletteRibbonColorStyle.RibbonGroupNormalBorderSepTrackingLight;
                    case PaletteState.Pressed:
                    case PaletteState.ContextPressed:
                        return PaletteRibbonColorStyle.RibbonGroupNormalBorderSepPressedLight;
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        DebugTools.NotImplemented(state.ToString());
                        break;
                }
                break;
            case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
                return PaletteRibbonColorStyle.Empty;
            case PaletteRibbonBackStyle.RibbonGroupArea:
                switch (state)
                {
                    case PaletteState.Normal:
                    case PaletteState.CheckedNormal:
                    case PaletteState.ContextNormal:
                    case PaletteState.ContextCheckedNormal:
                        return PaletteRibbonColorStyle.Empty;
                    case PaletteState.Tracking:
                    case PaletteState.ContextTracking:
                    case PaletteState.ContextCheckedTracking:
                        return PaletteRibbonColorStyle.RibbonGroupNormalTrackingLight;
                    case PaletteState.FocusOverride:
                        return PaletteRibbonColorStyle.RibbonTabFocus2010;
                    case PaletteState.ContextPressed:
                        return PaletteRibbonColorStyle.RibbonGroupNormalPressedDark;
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        DebugTools.NotImplemented(state.ToString());
                        break;
                }
                break;
            case PaletteRibbonBackStyle.RibbonTab:
                switch (state)
                {
                    case PaletteState.Disabled:
                    case PaletteState.Normal:
                        return PaletteRibbonColorStyle.Empty;
                    case PaletteState.Tracking:
                    case PaletteState.Pressed:
                    case PaletteState.ContextTracking:
                        return PaletteRibbonColorStyle.RibbonTabTracking2010;
                    case PaletteState.FocusOverride:
                        return PaletteRibbonColorStyle.RibbonTabFocus2010;
                    case PaletteState.CheckedNormal:
                    case PaletteState.CheckedTracking:
                    case PaletteState.CheckedPressed:
                    case PaletteState.ContextCheckedNormal:
                    case PaletteState.ContextCheckedTracking:
                        return PaletteRibbonColorStyle.RibbonTabSelected2010;
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        DebugTools.NotImplemented(state.ToString());
                        break;
                }
                break;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(style.ToString());
                break;
        }

        return PaletteRibbonColorStyle.Empty;
    }

    /// <summary>
    /// Gets the first background color for the ribbon item.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor1(PaletteRibbonBackStyle style, PaletteState state)
    {
        switch (style)
        {
            case PaletteRibbonBackStyle.RibbonGalleryBack:
                return state switch
                {
                    PaletteState.Disabled => SystemColors.Control,
                    PaletteState.Tracking => BaseColors!.RibbonGalleryBackTracking,
                    _ => BaseColors!.RibbonGalleryBackNormal
                };
            case PaletteRibbonBackStyle.RibbonGalleryBorder:
                return state switch
                {
                    PaletteState.Disabled => FadedColor(ColorTable.ButtonSelectedBorder),
                    _ => BaseColors!.RibbonGalleryBorder
                };
            case PaletteRibbonBackStyle.RibbonAppMenuDocs:
                return BaseColors!.AppButtonMenuDocsBack;
            case PaletteRibbonBackStyle.RibbonAppMenuInner:
                return BaseColors!.AppButtonInner1;
            case PaletteRibbonBackStyle.RibbonAppMenuOuter:
                return BaseColors!.AppButtonOuter1;
            case PaletteRibbonBackStyle.RibbonQATMinibar:
                return state == PaletteState.Normal
                    ? BaseColors!.RibbonQATMini1
                    : BaseColors!.RibbonQATMini1I;

            case PaletteRibbonBackStyle.RibbonQATFullbar:
                return BaseColors!.RibbonQATFullbar1;
            case PaletteRibbonBackStyle.RibbonQATOverflow:
                return BaseColors!.RibbonQATOverflow1;
            case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
                return BaseColors!.RibbonGroupFrameBorder1;
            case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                return BaseColors!.RibbonGroupFrameInside1;
            case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                switch (state)
                {
                    case PaletteState.Normal:
                    case PaletteState.Tracking:
                    case PaletteState.Pressed:
                    case PaletteState.ContextNormal:
                    case PaletteState.ContextTracking:
                    case PaletteState.ContextPressed:
                        return BaseColors!.RibbonGroupBorder1;
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        DebugTools.NotImplemented(state.ToString());
                        break;
                }
                break;
            case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
                return GlobalStaticValues.EMPTY_COLOR;
            case PaletteRibbonBackStyle.RibbonAppButton:
                switch (state)
                {
                    case PaletteState.Normal:
                        return _appButtonNormal[0];
                    case PaletteState.Tracking:
                        return _appButtonTrack[0];
                    case PaletteState.Pressed:
                        return _appButtonPressed[0];
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        DebugTools.NotImplemented(state.ToString());
                        break;
                }
                break;
            case PaletteRibbonBackStyle.RibbonGroupArea:
                return state == PaletteState.ContextCheckedNormal
                    ? _contextGroupAreaBorder
                    : BaseColors!.RibbonGroupsArea1;

            case PaletteRibbonBackStyle.RibbonTab:
                switch (state)
                {
                    case PaletteState.Tracking:
                    case PaletteState.Pressed:
                    case PaletteState.ContextTracking:
                        return BaseColors!.RibbonTabTracking1;
                    case PaletteState.CheckedNormal:
                    case PaletteState.CheckedTracking:
                    case PaletteState.CheckedPressed:
                    case PaletteState.ContextCheckedNormal:
                    case PaletteState.ContextCheckedTracking:
                        return BaseColors!.RibbonTabSelected1;
                    case PaletteState.FocusOverride:
                        return _contextCheckedTabBorder1;
                    case PaletteState.Normal:
                        return GlobalStaticValues.EMPTY_COLOR;
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        DebugTools.NotImplemented(state.ToString());
                        break;
                }
                break;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(style.ToString());
                break;
        }

        return Color.Red;
    }

    /// <summary>
    /// Gets the second background color for the ribbon item.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor2(PaletteRibbonBackStyle style, PaletteState state)
    {
        switch (style)
        {
            case PaletteRibbonBackStyle.RibbonAppMenuInner:
                return BaseColors!.AppButtonInner2;
            case PaletteRibbonBackStyle.RibbonAppMenuOuter:
                return BaseColors!.AppButtonOuter2;
            case PaletteRibbonBackStyle.RibbonQATMinibar:
                return state == PaletteState.Normal
                    ? BaseColors!.RibbonQATMini2
                    : BaseColors!.RibbonQATMini2I;

            case PaletteRibbonBackStyle.RibbonQATFullbar:
                return BaseColors!.RibbonQATFullbar2;
            case PaletteRibbonBackStyle.RibbonQATOverflow:
                return BaseColors!.RibbonQATOverflow2;
            case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
                return BaseColors!.RibbonGroupFrameBorder2;
            case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                return BaseColors!.RibbonGroupFrameInside2;
            case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                switch (state)
                {
                    case PaletteState.Normal:
                    case PaletteState.Tracking:
                    case PaletteState.Pressed:
                    case PaletteState.ContextNormal:
                    case PaletteState.ContextTracking:
                    case PaletteState.ContextPressed:
                        return BaseColors!.RibbonGroupBorder2;
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        DebugTools.NotImplemented(state.ToString());
                        break;
                }
                break;
            case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
                return GlobalStaticValues.EMPTY_COLOR;
            case PaletteRibbonBackStyle.RibbonAppButton:
                switch (state)
                {
                    case PaletteState.Normal:
                        return _appButtonNormal[1];
                    case PaletteState.Tracking:
                        return _appButtonTrack[1];
                    case PaletteState.Pressed:
                        return _appButtonPressed[1];
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        DebugTools.NotImplemented(state.ToString());
                        break;
                }
                break;
            case PaletteRibbonBackStyle.RibbonGroupArea:
                return BaseColors!.RibbonGroupsArea2;
            case PaletteRibbonBackStyle.RibbonTab:
                switch (state)
                {
                    case PaletteState.Tracking:
                    case PaletteState.Pressed:
                    case PaletteState.ContextTracking:
                        return BaseColors!.RibbonTabTracking2;
                    case PaletteState.CheckedNormal:
                    case PaletteState.CheckedTracking:
                    case PaletteState.CheckedPressed:
                    case PaletteState.ContextCheckedTracking:
                    case PaletteState.ContextCheckedNormal:
                        return BaseColors!.RibbonTabSelected2;
                    case PaletteState.FocusOverride:
                        return _contextCheckedTabBorder2;
                    case PaletteState.Normal:
                        return GlobalStaticValues.EMPTY_COLOR;
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        DebugTools.NotImplemented(state.ToString());
                        break;
                }
                break;
            case PaletteRibbonBackStyle.RibbonAppMenuDocs:
            case PaletteRibbonBackStyle.RibbonGalleryBack:
            case PaletteRibbonBackStyle.RibbonGalleryBorder:
                return GlobalStaticValues.EMPTY_COLOR;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(style.ToString());
                break;
        }

        return Color.Red;
    }

    /// <summary>
    /// Gets the third background color for the ribbon item.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor3(PaletteRibbonBackStyle style, PaletteState state)
    {
        switch (style)
        {
            case PaletteRibbonBackStyle.RibbonAppMenuOuter:
                return BaseColors!.AppButtonOuter3;
            case PaletteRibbonBackStyle.RibbonQATMinibar:
                return state == PaletteState.Normal
                    ? BaseColors!.RibbonQATMini3
                    : BaseColors!.RibbonQATMini3I;

            case PaletteRibbonBackStyle.RibbonQATFullbar:
                return BaseColors!.RibbonQATFullbar3;
            case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                return BaseColors!.RibbonGroupBorder3;
            case PaletteRibbonBackStyle.RibbonAppMenuDocs:
            case PaletteRibbonBackStyle.RibbonAppMenuInner:
            case PaletteRibbonBackStyle.RibbonQATOverflow:
            case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                return GlobalStaticValues.EMPTY_COLOR;
            case PaletteRibbonBackStyle.RibbonGalleryBack:
            case PaletteRibbonBackStyle.RibbonGalleryBorder:
                return GlobalStaticValues.EMPTY_COLOR;
            case PaletteRibbonBackStyle.RibbonAppButton:
                switch (state)
                {
                    case PaletteState.Normal:
                        return _appButtonNormal[2];
                    case PaletteState.Tracking:
                        return _appButtonTrack[2];
                    case PaletteState.Pressed:
                        return _appButtonPressed[2];
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        DebugTools.NotImplemented(state.ToString());
                        break;
                }
                break;
            case PaletteRibbonBackStyle.RibbonGroupArea:
                return BaseColors!.RibbonGroupsArea3;
            case PaletteRibbonBackStyle.RibbonTab:
                switch (state)
                {
                    case PaletteState.Tracking:
                    case PaletteState.Pressed:
                    case PaletteState.ContextTracking:
                        return BaseColors!.RibbonTabTracking3;
                    case PaletteState.CheckedNormal:
                    case PaletteState.CheckedTracking:
                    case PaletteState.CheckedPressed:
                    case PaletteState.ContextCheckedNormal:
                    case PaletteState.ContextCheckedTracking:
                        return BaseColors!.RibbonTabSelected3;
                    case PaletteState.FocusOverride:
                        return _contextCheckedTabBorder3;
                    case PaletteState.Normal:
                        return GlobalStaticValues.EMPTY_COLOR;
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        DebugTools.NotImplemented(state.ToString());
                        break;
                }
                break;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(style.ToString());
                break;
        }

        return Color.Red;
    }

    /// <summary>
    /// Gets the fourth background color for the ribbon item.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor4(PaletteRibbonBackStyle style, PaletteState state)
    {
        switch (style)
        {
            case PaletteRibbonBackStyle.RibbonQATMinibar:
                return state == PaletteState.Normal
                    ? BaseColors!.RibbonQATMini4
                    : BaseColors!.RibbonQATMini4I;

            case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                return BaseColors!.RibbonGroupBorder4;
            case PaletteRibbonBackStyle.RibbonAppMenuDocs:
            case PaletteRibbonBackStyle.RibbonAppMenuInner:
            case PaletteRibbonBackStyle.RibbonAppMenuOuter:
            case PaletteRibbonBackStyle.RibbonQATFullbar:
            case PaletteRibbonBackStyle.RibbonQATOverflow:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
            case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
            case PaletteRibbonBackStyle.RibbonGalleryBack:
            case PaletteRibbonBackStyle.RibbonGalleryBorder:
                return GlobalStaticValues.EMPTY_COLOR;
            case PaletteRibbonBackStyle.RibbonAppButton:
                switch (state)
                {
                    case PaletteState.Normal:
                        return _appButtonNormal[3];
                    case PaletteState.Tracking:
                        return _appButtonTrack[3];
                    case PaletteState.Pressed:
                        return _appButtonPressed[3];
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        DebugTools.NotImplemented(state.ToString());
                        break;
                }
                break;
            case PaletteRibbonBackStyle.RibbonGroupArea:
                return BaseColors!.RibbonGroupsArea4;
            case PaletteRibbonBackStyle.RibbonTab:
                switch (state)
                {
                    case PaletteState.Tracking:
                    case PaletteState.Pressed:
                    case PaletteState.ContextTracking:
                        return BaseColors!.RibbonTabTracking4;
                    case PaletteState.CheckedNormal:
                    case PaletteState.CheckedTracking:
                    case PaletteState.CheckedPressed:
                    case PaletteState.ContextCheckedNormal:
                    case PaletteState.ContextCheckedTracking:
                        return BaseColors!.RibbonTabSelected4;
                    case PaletteState.FocusOverride:
                        return _contextCheckedTabBorder4;
                    case PaletteState.Normal:
                        return GlobalStaticValues.EMPTY_COLOR;
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        DebugTools.NotImplemented(state.ToString());
                        break;
                }
                break;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(style.ToString());
                break;
        }

        return Color.Red;
    }

    /// <summary>
    /// Gets the fifth background color for the ribbon item.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor5(PaletteRibbonBackStyle style, PaletteState state)
    {
        switch (style)
        {
            case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                return BaseColors!.RibbonGroupBorder5;
            case PaletteRibbonBackStyle.RibbonAppMenuDocs:
            case PaletteRibbonBackStyle.RibbonAppMenuInner:
            case PaletteRibbonBackStyle.RibbonAppMenuOuter:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
            case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
            case PaletteRibbonBackStyle.RibbonQATFullbar:
            case PaletteRibbonBackStyle.RibbonQATOverflow:
            case PaletteRibbonBackStyle.RibbonGalleryBack:
            case PaletteRibbonBackStyle.RibbonGalleryBorder:
                return GlobalStaticValues.EMPTY_COLOR;
            case PaletteRibbonBackStyle.RibbonQATMinibar:
                return state == PaletteState.Normal
                    ? BaseColors!.RibbonQATMini5
                    : BaseColors!.RibbonQATMini5I;

            case PaletteRibbonBackStyle.RibbonAppButton:
                switch (state)
                {
                    case PaletteState.Normal:
                        return _appButtonNormal[4];
                    case PaletteState.Tracking:
                        return _appButtonTrack[4];
                    case PaletteState.Pressed:
                        return _appButtonPressed[4];
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        DebugTools.NotImplemented(state.ToString());
                        break;
                }
                break;
            case PaletteRibbonBackStyle.RibbonGroupArea:
                return BaseColors!.RibbonGroupsArea5;
            case PaletteRibbonBackStyle.RibbonTab:
                switch (state)
                {
                    case PaletteState.Disabled:
                        return _disabledText;
                    case PaletteState.Pressed:
                        return BaseColors!.RibbonTabTracking2;
                    case PaletteState.Tracking:
                    case PaletteState.CheckedNormal:
                    case PaletteState.CheckedTracking:
                    case PaletteState.CheckedPressed:
                    case PaletteState.ContextTracking:
                    case PaletteState.ContextCheckedNormal:
                    case PaletteState.ContextCheckedTracking:
                    case PaletteState.FocusOverride:
                    case PaletteState.Normal:
                        return GlobalStaticValues.EMPTY_COLOR;
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        DebugTools.NotImplemented(state.ToString());
                        break;
                }
                break;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(style.ToString());
                break;
        }

        return Color.Red;
    }
    #endregion

    #region RibbonText

    /// <summary>
    /// Gets the =color for the item text.
    /// </summary>
    /// <param name="style">Text style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonTextColor(PaletteRibbonTextStyle style, PaletteState state)
    {
        switch (style)
        {
            case PaletteRibbonTextStyle.RibbonAppMenuDocsEntry:
            case PaletteRibbonTextStyle.RibbonAppMenuDocsTitle:
                return SystemColors.ControlText;
            case PaletteRibbonTextStyle.RibbonGroupNormalTitle:
                return state switch
                {
                    PaletteState.Disabled => _disabledText,
                    _ => BaseColors!.RibbonGroupTitleText
                };
            case PaletteRibbonTextStyle.RibbonTab:
                return state switch
                {
                    PaletteState.Disabled => _disabledText,
                    PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking or PaletteState.ContextCheckedNormal or PaletteState.ContextCheckedTracking or PaletteState.FocusOverride => BaseColors!.RibbonTabTextChecked,
                    _ => BaseColors!.RibbonTabTextNormal
                };
            case PaletteRibbonTextStyle.RibbonGroupCollapsedText:
                return BaseColors!.RibbonGroupCollapsedText;
            case PaletteRibbonTextStyle.RibbonGroupButtonText:
            case PaletteRibbonTextStyle.RibbonGroupLabelText:
            case PaletteRibbonTextStyle.RibbonGroupCheckBoxText:
            case PaletteRibbonTextStyle.RibbonGroupRadioButtonText:
                return state == PaletteState.Disabled ? _disabledText : BaseColors!.RibbonGroupCollapsedText;

            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(style.ToString());
                break;
        }

        return Color.Red;
    }
    #endregion

    #region ElementColor

    /// <summary>
    /// Gets the first element color.
    /// </summary>
    /// <param name="element">Element for which color is required.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetElementColor1(PaletteElement element, PaletteState state)
    {
        // We do not provide override values
        if (CommonHelper.IsOverrideState(state))
        {
            return GlobalStaticValues.EMPTY_COLOR;
        }

        switch (element)
        {
            case PaletteElement.TrackBarTick:
                return ColorTable.SeparatorDark;
            case PaletteElement.TrackBarTrack:
                return ColorTable.OverflowButtonGradientEnd;
            case PaletteElement.TrackBarPosition:
                return Color.FromArgb(128, Color.White);
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(element.ToString());
                break;
        }

        return Color.Red;
    }

    /// <summary>
    /// Gets the second element color.
    /// </summary>
    /// <param name="element">Element for which color is required.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetElementColor2(PaletteElement element, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            return GlobalStaticValues.EMPTY_COLOR;
        }

        switch (element)
        {
            case PaletteElement.TrackBarTick:
                return ColorTable.SeparatorDark;
            case PaletteElement.TrackBarTrack:
                return ColorTable.MenuStripGradientBegin;
            case PaletteElement.TrackBarPosition:
                return state switch
                {
                    PaletteState.Disabled => ControlPaint.LightLight(ColorTable.MenuBorder),
                    PaletteState.Normal => ColorTable.MenuBorder,
                    PaletteState.Tracking => ColorTable.ButtonSelectedBorder,
                    PaletteState.Pressed => ColorTable.ButtonPressedBorder,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(element.ToString());
                break;
        }

        return Color.Red;
    }

    /// <summary>
    /// Gets the third element color.
    /// </summary>
    /// <param name="element">Element for which color is required.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetElementColor3(PaletteElement element, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            return GlobalStaticValues.EMPTY_COLOR;
        }

        switch (element)
        {
            case PaletteElement.TrackBarTick:
                return ColorTable.SeparatorDark;
            case PaletteElement.TrackBarTrack:
                return ColorTable.OverflowButtonGradientBegin;
            case PaletteElement.TrackBarPosition:
                return state switch
                {
                    PaletteState.Disabled => ControlPaint.LightLight(ColorTable.MenuStripGradientBegin),
                    PaletteState.Normal or PaletteState.FocusOverride => ControlPaint.Light(ColorTable.MenuStripGradientBegin),
                    PaletteState.Tracking => ControlPaint.Light(ColorTable.ButtonSelectedGradientBegin),
                    PaletteState.Pressed => ControlPaint.Light(ColorTable.ButtonPressedGradientBegin),
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(element.ToString());
                break;
        }

        return Color.Red;
    }

    /// <summary>
    /// Gets the fourth element color.
    /// </summary>
    /// <param name="element">Element for which color is required.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetElementColor4(PaletteElement element, PaletteState state)
    {
        switch (element)
        {
            case PaletteElement.TrackBarTick:
                if (CommonHelper.IsOverrideState(state))
                {
                    return GlobalStaticValues.EMPTY_COLOR;
                }

                return ColorTable.SeparatorDark;
            case PaletteElement.TrackBarTrack:
                if (CommonHelper.IsOverrideState(state))
                {
                    return GlobalStaticValues.EMPTY_COLOR;
                }

                return SystemColors.Control;
            case PaletteElement.TrackBarPosition:
                if (CommonHelper.IsOverrideStateExclude(state, PaletteState.FocusOverride))
                {
                    return GlobalStaticValues.EMPTY_COLOR;
                }

                return state switch
                {
                    PaletteState.Disabled => ControlPaint.LightLight(ColorTable.MenuStripGradientEnd),
                    PaletteState.Normal => ColorTable.MenuStripGradientEnd,
                    PaletteState.Tracking or PaletteState.FocusOverride => ColorTable.ButtonSelectedGradientBegin,
                    PaletteState.Pressed => ColorTable.ButtonPressedGradientBegin,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(element.ToString());
                break;
        }

        return Color.Red;
    }

    /// <summary>
    /// Gets the fifth element color.
    /// </summary>
    /// <param name="element">Element for which color is required.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetElementColor5(PaletteElement element, PaletteState state)
    {
        switch (element)
        {
            case PaletteElement.TrackBarTick:
                if (CommonHelper.IsOverrideState(state))
                {
                    return GlobalStaticValues.EMPTY_COLOR;
                }

                return ColorTable.SeparatorDark;
            case PaletteElement.TrackBarTrack:
                if (CommonHelper.IsOverrideState(state))
                {
                    return GlobalStaticValues.EMPTY_COLOR;
                }

                return SystemColors.Control;
            case PaletteElement.TrackBarPosition:
                if (CommonHelper.IsOverrideStateExclude(state, PaletteState.FocusOverride))
                {
                    return GlobalStaticValues.EMPTY_COLOR;
                }

                return state switch
                {
                    PaletteState.Disabled => ControlPaint.LightLight(ColorTable.MenuStripGradientBegin),
                    PaletteState.Normal => ColorTable.MenuStripGradientBegin,
                    PaletteState.Tracking or PaletteState.FocusOverride => ColorTable.ButtonSelectedGradientEnd,
                    PaletteState.Pressed => ColorTable.ButtonPressedGradientEnd,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(element.ToString());
                break;
        }

        return Color.Red;
    }
    #endregion

    #region ColorTable

    /// <summary>
    /// Gets access to the color table instance.
    /// </summary>
    public override KryptonColorTable ColorTable => Table;

    internal KryptonProfessionalKCT Table => _table ??= GenerateColorTable(false);

    /// <summary>
    /// Generate an appropriate color table.
    /// </summary>
    /// <returns>KryptonProfessionalKCT instance.</returns>
    internal virtual KryptonProfessionalKCT GenerateColorTable(bool useSystemColors)
    {
        // Create the color table to use as the base for getting krypton colors
        var kct = new KryptonColorTable(this)
        {

            // Always turn off the use of any theme specific colors
            UseSystemColors = useSystemColors
        };

        // Calculate the krypton specific colors
        Color[] colors =
        [
            kct.OverflowButtonGradientEnd,   // Header1Begin
            kct.OverflowButtonGradientEnd // Header1End
        ];

        // Create a krypton extension color table
        return new KryptonProfessionalKCT(colors, true, this);
    }

    #endregion ColorTable

    #region OnUserPreferenceChanged

    /// <summary>
    /// Handle a change in the user preferences.
    /// </summary>
    /// <param name="sender">Source of event.</param>
    /// <param name="e">Event data.</param>
    protected override void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
        // Remove the current table, so it gets regenerated when next requested
        _table = null;

        if (_disabledDropDownImage != null)
        {
            _disabledDropDownImage.Dispose();
            _disabledDropDownImage = null;
        }

        if (_normalDropDownImage != null)
        {
            _normalDropDownImage.Dispose();
            _normalDropDownImage = null;
        }

        if (_galleryImageUp != null)
        {
            _galleryImageUp.Dispose();
            _galleryImageUp = null;
        }

        if (_galleryImageDown != null)
        {
            _galleryImageDown.Dispose();
            _galleryImageDown = null;
        }

        if (_galleryImageDropDown != null)
        {
            _galleryImageDropDown.Dispose();
            _galleryImageDropDown = null;
        }

        // Update fonts to reflect any change in system settings
        DefineFonts();

        // Generate the myriad ribbon colors from system settings
        DefineRibbonColors();

        base.OnUserPreferenceChanged(sender, e);
    }

    #endregion OnUserPreferenceChanged

    #region Implementation

    private void DefineRibbonColors()
    {
        // Main values
        //Color groupLight = ColorTable.MenuStripGradientEnd;
        Color groupStart = ColorTable.RaftingContainerGradientBegin;
        Color groupEnd = ColorTable.MenuBorder;

        // Spot standard background colors and then tweak values,
        // so it looks good under the standard windows settings.
        switch (SystemColors.Control.ToArgb())
        {
            case -986896:   // Vista Aero/Basic
            case -1250856:  // XP Themes - Blue & Olive
            case -2039837:  // XP Themes - Silver
                //groupLight = MergeColors(groupLight, 0.93f, Color.Black, 0.07f);
                groupStart = MergeColors(groupStart, 0.93f, Color.Black, 0.07f);
                groupEnd = MergeColors(groupEnd, 0.93f, Color.Black, 0.07f);
                break;
            case -2830136:  // Windows Standard
            case -4144960:  // Windows Classic
                //groupLight = MergeColors(groupLight, 0.95f, Color.Black, 0.05f);
                groupStart = MergeColors(groupStart, 0.95f, Color.Black, 0.05f);
                groupEnd = MergeColors(groupEnd, 0.95f, Color.Black, 0.05f);
                break;
        }

        // Create colors, mainly by merging between two main values
        Color ribbonGroupsArea1 = MergeColors(groupStart, 0.80f, groupEnd, 0.20f);
        Color ribbonGroupsArea2 = MergeColors(groupStart, 0.20f, groupEnd, 0.80f);
        Color ribbonGroupsArea3 = MergeColors(groupStart, 0.10f, Color.White, 0.90f);
        Color ribbonGroupsArea4 = MergeColors(groupStart, 0.70f, Color.White, 0.30f);
        //Color ribbonGroupsArea5 = MergeColors(groupStart, 0.90f, groupEnd, 0.10f);
        Color ribbonGroupBorder1 = Color.FromArgb(128, Color.White);
        Color ribbonGroupBorder2 = Color.FromArgb(196, Color.White);
        Color ribbonGroupBorder3 = MergeColors(groupStart, 0.20f, groupEnd, 0.80f);
        Color ribbonGroupBorder4 = MergeColors(groupStart, 0.30f, Color.White, 0.70f);
        Color ribbonGroupBorder5 = Color.FromArgb(249, 250, 250);
        Color ribbonGroupFrameBorder1 = MergeColors(groupStart, 0.60f, groupEnd, 0.40f);
        Color ribbonGroupFrameInside1 = MergeColors(groupStart, 0.40f, Color.White, 0.60f);
        Color ribbonGroupTitleText = Color.FromArgb(152, SystemColors.ControlText);
        Color ribbonGroupDialogDark = Color.FromArgb(104, SystemColors.ControlText);
        Color ribbonGroupDialogLight = Color.FromArgb(72, SystemColors.ControlText);
        Color ribbonGroupSepDark = MergeColors(groupStart, 0.50f, groupEnd, 0.50f);
        Color ribbonMinimizeLight = MergeColors(ColorTable.MenuStripGradientEnd, 0.40f, Color.White, 0.60f);
        Color ribbonMinimizeDark = MergeColors(groupStart, 0.70f, groupEnd, 0.30f);
        Color ribbonTabSelected1 = MergeColors(groupStart, 0.80f, groupEnd, 0.20f);
        Color ribbonTabSelected2 = MergeColors(groupStart, 0.10f, Color.White, 0.90f);
        Color ribbonTabSelected3 = MergeColors(groupStart, 0.10f, Color.White, 0.90f);
        Color ribbonTabSelected4 = MergeColors(groupStart, 0.10f, Color.White, 0.90f);
        Color ribbonTabTracking1 = MergeColors(groupStart, 0.80f, groupEnd, 0.20f);
        Color ribbonTabTracking2 = MergeColors(groupStart, 0.20f, Color.White, 0.80f);
        Color ribbonTabTracking3 = MergeColors(groupStart, 0.50f, Color.White, 0.50f);
        Color ribbonTabTracking4 = MergeColors(groupStart, 0.75f, Color.White, 0.25f);
        //Color ribbonQATOverflowInside = MergeColors(ColorTable.MenuStripGradientEnd, 0.75f, groupStart, 0.25f);
        //Color ribbonQATOverflowInside2 = MergeColors(ColorTable.MenuStripGradientEnd, 0.65f, groupStart, 0.35f);
        //Color ribbonQATMini1 = MergeColors(groupStart, 0.70f, groupEnd, 0.30f);
        Color ribbonQATMini3 = MergeColors(groupStart, 0.90f, groupEnd, 0.10f);

        // Generate first set of ribbon colors
        _ribbonColors =
        [ // Non ribbon colors
            Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
            Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
            Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
            Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
            Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
            Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
            Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
            Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
            Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
            Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
            Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
            Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
            Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
            Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
            // Ribbon colors
            SystemColors.ControlText,     // RibbonTabTextNormal
            SystemColors.ControlText,     // RibbonTabTextChecked
            ribbonTabSelected1,           // RibbonTabSelected1
            ribbonTabSelected2,           // RibbonTabSelected2
            ribbonTabSelected3,           // RibbonTabSelected3
            ribbonTabSelected4,           // RibbonTabSelected4
            GlobalStaticValues.EMPTY_COLOR,  // RibbonTabSelected5
            ribbonTabTracking1,           // RibbonTabTracking1
            ribbonTabTracking2,           // RibbonTabTracking2
            Color.FromArgb(196, ColorTable.ButtonSelectedGradientMiddle), // RibbonTabHighlight1
            ColorTable.ButtonSelectedGradientMiddle,                      // RibbonTabHighlight2
            ColorTable.ButtonPressedGradientMiddle,                       // RibbonTabHighlight3
            ColorTable.ButtonPressedGradientMiddle,                       // RibbonTabHighlight4
            ColorTable.ButtonSelectedGradientMiddle,                      // RibbonTabHighlight5
            ColorTable.MenuBorder,        // RibbonTabSeparatorColor
            ribbonGroupsArea1,            // RibbonGroupsArea1
            ribbonGroupsArea2,            // RibbonGroupsArea2
            ribbonGroupsArea3,            // RibbonGroupsArea3
            ribbonGroupsArea4,            // RibbonGroupsArea4
            ribbonGroupsArea4,            // RibbonGroupsArea5
            ribbonGroupBorder1,           // RibbonGroupBorder1
            ribbonGroupBorder2,           // RibbonGroupBorder2
            Color.Red,                    // RibbonGroupTitle1
            Color.Red,                    // RibbonGroupTitle2
            ribbonGroupBorder1,           // RibbonGroupBorderContext1
            ribbonGroupBorder2,           // RibbonGroupBorderContext2
            Color.Red,                    // RibbonGroupTitleContext1
            Color.Red,                    // RibbonGroupTitleContext2
            ribbonGroupDialogDark,        // RibbonGroupDialogDark
            ribbonGroupDialogLight,       // RibbonGroupDialogLight
            Color.Red,                    // RibbonGroupTitleTracking1
            Color.Red,                    // RibbonGroupTitleTracking2
            ribbonMinimizeDark,           // RibbonMinimizeBarDark
            ribbonMinimizeLight,          // RibbonMinimizeBarLight
            ribbonGroupBorder1,           // RibbonGroupCollapsedBorder1
            ribbonGroupBorder2,           // RibbonGroupCollapsedBorder2
            ribbonGroupsArea4,            // RibbonGroupCollapsedBorder3
            ribbonGroupsArea2,            // RibbonGroupCollapsedBorder4
            ribbonGroupsArea4,            // RibbonGroupCollapsedBack1
            Color.Red,                    // RibbonGroupCollapsedBack2
            Color.Red,                    // RibbonGroupCollapsedBack3
            ribbonGroupsArea2,            // RibbonGroupCollapsedBack4
            Color.Red,                    // RibbonGroupCollapsedBorderT1
            Color.Red,                    // RibbonGroupCollapsedBorderT2
            Color.Red,                    // RibbonGroupCollapsedBorderT3
            Color.Red,                    // RibbonGroupCollapsedBorderT4
            Color.Red,                    // RibbonGroupCollapsedBackT1
            Color.Red,                    // RibbonGroupCollapsedBackT2
            Color.Red,                    // RibbonGroupCollapsedBackT3
            Color.Red,                    // RibbonGroupCollapsedBackT4
            ribbonGroupFrameBorder1,      // RibbonGroupFrameBorder1
            ribbonGroupFrameBorder1,      // RibbonGroupFrameBorder2
            ribbonGroupFrameInside1,      // RibbonGroupFrameInside1
            ribbonGroupFrameInside1,      // RibbonGroupFrameInside2
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupFrameInside3
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupFrameInside4
            SystemColors.ControlText,     // RibbonGroupCollapsedText
            SystemColors.ControlText,     // RibbonGroupButtonText
            // Non ribbon colors
            Color.Red, Color.Red, Color.Red,
            Color.Red, Color.Red, Color.Red,
            Color.Red, Color.Red, Color.Red,
            // Ribbon colors
            ColorTable.MenuBorder,            // RibbonQATMini1
            groupStart,                       // RibbonQATMini2
            ribbonQATMini3,                   // RibbonQATMini3
            Color.FromArgb(32, Color.White),  // RibbonQATMini4
            Color.FromArgb(32, Color.White),  // RibbonQATMini5
            ColorTable.MenuBorder,            // RibbonQATMini1I
            groupStart,                       // RibbonQATMini2I
            ribbonQATMini3,                   // RibbonQATMini3I
            Color.FromArgb(32, Color.White),  // RibbonQATMini4I
            Color.FromArgb(32, Color.White),  // RibbonQATMini5I
            groupStart,                       // RibbonQATFullbar1
            ribbonQATMini3,                   // RibbonQATFullbar2
            ribbonGroupsArea1,                // RibbonQATFullbar3
            SystemColors.ControlText,         // RibbonQATButtonDark
            SystemColors.ControlLight,        // RibbonQATButtonLight
            groupStart,                       // RibbonQATOverflow1
            ColorTable.MenuBorder,            // RibbonQATOverflow2
            ribbonGroupSepDark,               // RibbonGroupSeparatorDark
            ColorTable.GripLight,             // RibbonGroupSeparatorLight
            // Non ribbon colors
            Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
            Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
            Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
            Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
            Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
            Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
            Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
            SystemColors.Window,              // AppButtonBack1
            ribbonGroupsArea1,                // AppButtonBack2
            ColorTable.MenuBorder,            // AppButtonBorder
            ColorTable.SeparatorDark,         // AppButtonOuter1
            ColorTable.SeparatorDark,         // AppButtonOuter2
            ColorTable.StatusStripGradientBegin,     // AppButtonOuter3
            ColorTable.ToolStripDropDownBackground,  // AppButtonInner1
            ColorTable.MenuBorder,                   // AppButtonInner2
            ColorTable.ImageMarginGradientMiddle,    // AppButtonMenuDocs
            SystemColors.ControlText,                // AppButtonMenuDocsText
            // Non ribbon colors
            Color.Red, Color.Red,
            ColorTable.MenuBorder,            // RibbonGalleryBorder
            ribbonTabSelected4,               // RibbonGalleryBackNormal
            SystemColors.Window,              // RibbonGalleryBackTracking
            Color.Red,                        // RibbonGalleryBack1
            Color.Red,                        // RibbonGalleryBack2
            ribbonTabTracking3,               // RibbonTabTracking3
            ribbonTabTracking4,               // RibbonTabTracking4
            ribbonGroupBorder3,               // RibbonGroupBorder3
            ribbonGroupBorder4,               // RibbonGroupBorder4
            ribbonGroupBorder5,               // RibbonGroupBorder5
            ribbonGroupTitleText,             // RibbonGroupTitleText
            Color.Red,                        // RibbonDropArrowLight
            Color.Red,                        // RibbonDropArrowDark
            Color.Red,                        // HeaderDockInactiveBack1
            Color.Red,                        // HeaderDockInactiveBack2
            Color.Red,                        // ButtonNavigatorBorder
            Color.Red,                        // ButtonNavigatorText
            Color.Red,                        // ButtonNavigatorTrack1
            Color.Red,                        // ButtonNavigatorTrack2
            Color.Red,                        // ButtonNavigatorPressed1
            Color.Red,                        // ButtonNavigatorPressed2
            Color.Red,                        // ButtonNavigatorChecked1
            Color.Red // ButtonNavigatorChecked2
        ];

        // Generate second set of ribbon colors
        _disabledText = SystemColors.ControlDark;
        _disabledGlyphDark = Color.FromArgb(183, 183, 183);
        _disabledGlyphLight = Color.FromArgb(237, 237, 237);
        //_contextCheckedTabBorder = ribbonGroupsArea1;
        //_contextCheckedTabFill = ColorTable.CheckBackground;
        _contextGroupAreaBorder = ribbonGroupsArea1;
        //_contextGroupAreaInside = ribbonGroupsArea2;
        //_contextGroupFrameTop = Color.FromArgb(250, 250, 250);
        //_contextGroupFrameBottom = _contextGroupFrameTop;
        _contextTabSeparator = ColorTable.MenuBorder;
        //_focusTabFill = ColorTable.CheckBackground;
        _toolTipBack1 = SystemColors.Info;
        _toolTipBack2 = SystemColors.Info;
        _toolTipBorder = SystemColors.WindowFrame;
        _toolTipText = SystemColors.InfoText;
        //_disabledDropDownColor = GlobalStaticValues.EMPTY_COLOR;
        //_normalDropDownColor = GlobalStaticValues.EMPTY_COLOR;
        //_ribbonGroupCollapsedBackContext = new Color[] { Color.FromArgb(48, 235, 235, 235), Color.FromArgb(235, 235, 235) };
        //_ribbonGroupCollapsedBackContextTracking = _ribbonGroupCollapsedBackContext;
        //_ribbonGroupCollapsedBorderContext = new Color[] { Color.FromArgb(160, ribbonGroupBorder1), ribbonGroupBorder1, Color.FromArgb(48, ribbonGroupsArea4), ribbonGroupsArea4 };
        //_ribbonGroupCollapsedBorderContextTracking = new Color[] { Color.FromArgb(200, ribbonGroupBorder1), ribbonGroupBorder1, Color.FromArgb(48, ribbonGroupBorder1), Color.FromArgb(196, ribbonGroupBorder1) };
        Color highlight1 = MergeColors(Color.White, 0.50f, ColorTable.ButtonSelectedGradientEnd, 0.50f);
        Color highlight2 = MergeColors(Color.White, 0.25f, ColorTable.ButtonSelectedGradientEnd, 0.75f);
        Color highlight3 = MergeColors(Color.White, 0.60f, ColorTable.ButtonPressedGradientMiddle, 0.40f);
        Color highlight4 = MergeColors(Color.White, 0.25f, ColorTable.ButtonPressedGradientMiddle, 0.75f);
        //Color pressed3 = MergeColors(Color.White, 0.50f, ColorTable.CheckBackground, 0.50f);
        Color pressed4 = MergeColors(Color.White, 0.25f, ColorTable.CheckPressedBackground, 0.75f);
        _appButtonNormal = [ColorTable.SeparatorLight, ColorTable.ImageMarginGradientBegin, ColorTable.ImageMarginGradientMiddle, ColorTable.GripLight, ColorTable.ImageMarginGradientBegin
        ];
        _appButtonTrack = [highlight1, highlight2, ColorTable.ButtonSelectedGradientEnd, highlight3, highlight4];
        _appButtonPressed = [highlight1, pressed4, ColorTable.CheckPressedBackground, highlight2, pressed4];

        // Synchronize the strongly-typed color scheme with the generated ribbon colors
        {
            var schemeType = BaseColors.GetType();
            foreach (SchemeBaseColors role in Enum.GetValues(typeof(SchemeBaseColors)))
            {
                int index = (int)role;
                if (index < _ribbonColors.Length)
                {
                    var prop = schemeType.GetProperty(role.ToString(), System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                    prop?.SetValue(BaseColors, _ribbonColors[index]);
                }
            }
        }
    }

    /*
    private Image CreateDropDownImage(Color color)
    {
        // Create image that has an alpha channel
        Image image = new Bitmap(9, 9, PixelFormat.Format32bppArgb);

        // Use a graphics instance for drawing the image
        using Graphics g = Graphics.FromImage(image);
        // Draw a solid arrow
        using (var fill = new SolidBrush(color))
        {
            g.FillPolygon(fill, new Point[] { new Point(2, 3), new Point(4, 6), new Point(7, 3) });
        }

        // Draw semi-transparent outline around the arrow
        using var outline = new Pen(Color.FromArgb(128, color));
        g.DrawLines(outline, new Point[] { new Point(1, 3), new Point(4, 6), new Point(7, 3) });

        return image;
    }
    */

    private Image CreateGalleryUpImage(Color color)
    {
        // Create image that has an alpha channel
        Image image = new Bitmap(13, 7, PixelFormat.Format32bppArgb);

        // Use a graphics instance for drawing the image
        using Graphics g = Graphics.FromImage(image);
        // Draw a solid arrow
        using var fill = new SolidBrush(color);
        g.FillPolygon(fill, new Point[] { new Point(3, 6), new Point(6, 2), new Point(9, 6) });

        return image;
    }

    private Image CreateGalleryDownImage(Color color)
    {
        // Create image that has an alpha channel
        Image image = new Bitmap(13, 7, PixelFormat.Format32bppArgb);

        // Use a graphics instance for drawing the image
        using Graphics g = Graphics.FromImage(image);
        // Draw a solid arrow
        using var fill = new SolidBrush(color);
        g.FillPolygon(fill, new Point[] { new Point(4, 3), new Point(6, 6), new Point(9, 3) });

        return image;
    }

    private Image CreateGalleryDropDownImage(Color color)
    {
        // Create image that has an alpha channel
        Image image = new Bitmap(13, 7, PixelFormat.Format32bppArgb);

        // Use a graphics instance for drawing the image
        using Graphics g = Graphics.FromImage(image);
        // Draw a solid arrow
        using (var fill = new SolidBrush(color))
        {
            g.FillPolygon(fill, new Point[] { new Point(4, 3), new Point(6, 6), new Point(9, 3) });
        }

        // Draw the line above the arrow
        using var pen = new Pen(color);
        g.DrawLine(pen, 4, 1, 8, 1);

        return image;
    }

    #endregion Implementation

    #region Tab Row Background

    /// <inheritdoc />
    public override Color GetRibbonTabRowGradientColor1(PaletteState state) =>
	    GlobalStaticValues.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) =>
        GlobalStaticValues.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) =>
        GlobalStaticValues.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) =>
	    GlobalStaticValues.EMPTY_COLOR;

    /// <inheritdoc />
    public override float GetRibbonTabRowGradientRaftingAngle(PaletteState state) => -1;

    #endregion Tab Row Background

    #region AppButton Colors

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabBottomColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTopColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTextColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

    #endregion AppButton Colors
}

#endregion