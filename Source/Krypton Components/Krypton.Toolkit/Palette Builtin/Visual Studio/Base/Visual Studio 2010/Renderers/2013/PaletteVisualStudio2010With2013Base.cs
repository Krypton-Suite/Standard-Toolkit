#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Provides a base for Visual Studio palettes.</summary>
/// <seealso cref="PaletteBase" />
public abstract class PaletteVisualStudio2010With2013Base : PaletteBase
{
    #region Static Fields

    #region Padding

    private static readonly Padding _contentPaddingGrid = new Padding(2, 1, 2, 1);
    private static readonly Padding _contentPaddingHeader1 = new Padding(2, 1, 2, 1);
    private static readonly Padding _contentPaddingHeader2 = new Padding(2, 1, 2, 1);
    private static readonly Padding _contentPaddingDock = new Padding(2, 2, 2, 1);
    private static readonly Padding _contentPaddingCalendar = new Padding(2);
    //private static readonly Padding _contentPaddingHeaderForm = new Padding(owningForm!.RealWindowBorders.Left, owningForm!.RealWindowBorders.Bottom / 2, 0, 0);
    private static readonly Padding _contentPaddingLabel = new Padding(3, 1, 3, 1);
    private static readonly Padding _contentPaddingLabel2 = new Padding(8, 2, 8, 2);
    private static readonly Padding _contentPaddingButtonInputControl = new Padding(0);
    private static readonly Padding _contentPaddingButton12 = new Padding(1);
    private static readonly Padding _contentPaddingButton3 = new Padding(1, 0, 1, 0);
    private static readonly Padding _contentPaddingButton4 = new Padding(4, 3, 4, 3);
    private static readonly Padding _contentPaddingButton5 = new Padding(3, 3, 3, 2);
    private static readonly Padding _contentPaddingButton6 = new Padding(3);
    private static readonly Padding _contentPaddingButton7 = new Padding(1, 1, 0, 1);
    private static readonly Padding _contentPaddingButtonForm = new Padding(0);
    private static readonly Padding _contentPaddingButtonGallery = new Padding(1, 0, 1, 0);
    private static readonly Padding _contentPaddingButtonListItem = new Padding(0);
    private static readonly Padding _contentPaddingToolTip = new Padding(2);
    private static readonly Padding _contentPaddingSuperTip = new Padding(4);
    private static readonly Padding _contentPaddingKeyTip = new Padding(0, -1, 0, -3);
    private static readonly Padding _contentPaddingContextMenuHeading = new Padding(8, 2, 8, 0);
    private static readonly Padding _contentPaddingContextMenuImage = new Padding(0);
    private static readonly Padding _contentPaddingContextMenuItemText = new Padding(9, 1, 7, 0);
    private static readonly Padding _contentPaddingContextMenuItemTextAlt = new Padding(7, 1, 6, 0);
    private static readonly Padding _contentPaddingContextMenuItemShortcutText = new Padding(3, 1, 4, 0);
    private static readonly Padding _metricPaddingRibbon = new Padding(0, 1, 1, 1);
    private static readonly Padding _metricPaddingRibbonAppButton = new Padding(3, 0, 3, 0);
    private static readonly Padding _metricPaddingHeader = new Padding(0, 3, 1, 3);
    //private static readonly Padding _metricPaddingHeaderForm = new Padding(0, owningForm!.RealWindowBorders.Right, 0, 0);//, 3, 0, -3); // Move the Maximised Form buttons down a bit
    private static readonly Padding _metricPaddingInputControl = new Padding(0, 1, 0, 1);
    private static readonly Padding _metricPaddingBarInside = new Padding(3);
    private static readonly Padding _metricPaddingBarTabs = new Padding(0);
    private static readonly Padding _metricPaddingBarOutside = new Padding(0, 0, 0, 3);
    private static readonly Padding _metricPaddingPageButtons = new Padding(1, 3, 1, 3);

    #endregion

    #region Images

    private static readonly Image? _treeExpandWhite = TreeItemImageResources.TreeExpandWhite;
    private static readonly Image? _treeCollapseBlack = TreeItemImageResources.TreeCollapseBlack;

    private static readonly Image? _disabledDropDown = DropDownArrowImageResources.DisabledDropDownButton;
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
    private static readonly Image _buttonSpecPendantClose = Office2010MDIImageResources.Office2010ButtonMDIClose;
    private static readonly Image _buttonSpecPendantMin = Office2010MDIImageResources.Office2010ButtonMDIMin;
    private static readonly Image _buttonSpecPendantRestore = Office2010MDIImageResources.Office2010ButtonMDIRestore;
    private static readonly Image _buttonSpecWorkspaceMaximize = ProfessionalControlBoxResources.ProfessionalMaximize;
    private static readonly Image _buttonSpecWorkspaceRestore = GenericProfessionalImageResources.ProfessionalRestore;
    private static readonly Image _buttonSpecRibbonMinimize = RibbonArrowImageResources.RibbonUp2010;
    private static readonly Image _buttonSpecRibbonExpand = RibbonArrowImageResources.RibbonDown2010;
    private static readonly Image? _contextMenuChecked = GenericOffice2007ImageResources.Office2007Checked;
    private static readonly Image? _contextMenuIndeterminate = GenericOffice2007ImageResources.Office2007Indeterminate;

    #region Integrated Toolbar Images

    private static readonly Image _integratedToolbarNewNormal = Office2019ToolbarImageResources.Office2019ToolbarNewNormal;

    private static readonly Image _integratedToolbarOpenNormal = Office2019ToolbarImageResources.Office2019ToolbarOpenNormal;

    private static readonly Image _integratedToolbarSaveAllNormal = Office2019ToolbarImageResources.Office2019ToolbarSaveAllNormal;

    private static readonly Image _integratedToolbarSaveAsNormal = Office2019ToolbarImageResources.Office2019ToolbarSaveAsNormal;

    private static readonly Image _integratedToolbarSaveNormal = Office2019ToolbarImageResources.Office2019ToolbarSaveNormal;

    private static readonly Image _integratedToolbarCutNormal = Office2019ToolbarImageResources.Office2019ToolbarCutNormal;

    private static readonly Image _integratedToolbarCopyNormal = Office2019ToolbarImageResources.Office2019ToolbarCopyNormal;

    private static readonly Image _integratedToolbarPasteNormal = Office2019ToolbarImageResources.Office2019ToolbarPasteNormal;

    private static readonly Image _integratedToolbarUndoNormal = Office2019ToolbarImageResources.Office2019ToolbarUndoNormal;

    private static readonly Image _integratedToolbarRedoNormal = Office2019ToolbarImageResources.Office2019ToolbarRedoNormal;

    private static readonly Image _integratedToolbarPageSetupNormal = Office2019ToolbarImageResources.Office2019ToolbarPageSetupNormal;

    private static readonly Image _integratedToolbarPrintPreviewNormal = Office2019ToolbarImageResources.Office2019ToolbarPrintPreviewNormal;

    private static readonly Image _integratedToolbarPrintNormal = Office2019ToolbarImageResources.Office2019ToolbarPrintNormal;

    private static readonly Image _integratedToolbarQuickPrintNormal = Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal;

    #endregion

    #endregion

    #region Colors

    private static readonly Color _gridTextColor = Color.Black;
    private static readonly Color _disabledText2 = Color.FromArgb(128, 128, 128);
    private static readonly Color _disabledText = Color.FromArgb(167, 167, 167);
    private static readonly Color _disabledBack = Color.FromArgb(235, 235, 235);
    private static readonly Color _disabledBorder = Color.FromArgb(212, 212, 212);
    private static readonly Color _disabledGlyphDark = Color.FromArgb(183, 183, 183);
    private static readonly Color _disabledGlyphLight = Color.FromArgb(237, 237, 237);
    private static readonly Color _contextCheckedTabBorder1 = Color.FromArgb(223, 119, 0);
    private static readonly Color _contextCheckedTabBorder2 = Color.FromArgb(230, 190, 129);
    private static readonly Color _contextCheckedTabBorder3 = Color.FromArgb(220, 202, 171);
    private static readonly Color _contextCheckedTabBorder4 = Color.FromArgb(255, 252, 247);
    private static readonly Color _contextTabSeparator = Color.White;
    private static readonly Color _contextTextColor = Color.White;
    private static readonly Color _todayBorder = Color.FromArgb(187, 85, 3);
    private static readonly Color _toolTipBack1 = Color.FromArgb(255, 255, 255);
    private static readonly Color _toolTipBack2 = Color.FromArgb(201, 217, 239);
    private static readonly Color _toolTipBorder = Color.FromArgb(118, 118, 118);
    private static readonly Color _toolTipText = Color.FromArgb(76, 76, 76);
    private static readonly Color _contextMenuBack = Color.FromArgb(188, 199, 216);
    private static readonly Color _contextMenuBorder = Color.FromArgb(134, 134, 134);
    private static readonly Color _contextMenuHeadingBorder = Color.FromArgb(197, 197, 197);
    private static readonly Color _contextMenuImageBackChecked = Color.FromArgb(252, 241, 194);
    private static readonly Color _contextMenuImageBorderChecked = Color.FromArgb(242, 149, 54);
    private static readonly Color _formCloseBorderTracking = Color.FromArgb(155, 61, 61);
    private static readonly Color _formCloseBorderPressed = Color.FromArgb(155, 61, 61);
    private static readonly Color _formCloseBorderCheckedNormal = Color.FromArgb(155, 61, 61);
    private static readonly Color _formCloseTracking1 = Color.FromArgb(255, 132, 130);
    private static readonly Color _formCloseTracking2 = Color.FromArgb(227, 97, 98);
    private static readonly Color _formClosePressed1 = Color.FromArgb(242, 119, 118);
    private static readonly Color _formClosePressed2 = Color.FromArgb(206, 85, 84);
    private static readonly Color _formCloseChecked1 = Color.FromArgb(255, 132, 130);
    private static readonly Color _formCloseChecked2 = Color.FromArgb(255, 132, 130);
    private static readonly Color _formCloseCheckedTracking1 = Color.FromArgb(255, 132, 130);
    private static readonly Color _formCloseCheckedTracking2 = Color.FromArgb(255, 132, 130);

    #endregion

    #region Color Arrays

    private static readonly Color[] _appButtonNormal =
    [
        Color.FromArgb(243, 245, 248),
        Color.FromArgb(214, 220, 231),
        Color.FromArgb(188, 198, 211),
        Color.FromArgb(254, 254, 255),
        Color.FromArgb(206, 213, 225)
    ];

    private static readonly Color[] _appButtonTrack =
    [
        Color.FromArgb(255, 251, 230),
        Color.FromArgb(248, 230, 143),
        Color.FromArgb(238, 213, 126),
        Color.FromArgb(254, 247, 129),
        Color.FromArgb(240, 201, 41)
    ];

    private static readonly Color[] _appButtonPressed =
    [
        Color.FromArgb(235, 227, 196),
        Color.FromArgb(228, 198, 149),
        Color.FromArgb(166, 97, 7),
        Color.FromArgb(242, 155, 57),
        Color.FromArgb(236, 136, 9)
    ];

    private static readonly Color[] _buttonBorderColors =
    [
        Color.FromArgb(180, 180, 180), // Button, Disabled, Border
        Color.FromArgb(205, 230, 247), // Button, Tracking, Border 1
        Color.FromArgb(205, 230, 247), // Button, Tracking, Border 2
        Color.FromArgb(146, 192, 244), // Button, Pressed, Border 1
        Color.FromArgb(146, 192, 244), // Button, Pressed, Border 2
        Color.FromArgb(146, 192, 244), // Button, Checked, Border 1
        Color.FromArgb(146, 192, 244) // Button, Checked, Border 2
    ];

    private static readonly Color[] _buttonBackColors =
    [
        Color.FromArgb(250, 250, 250), // Button, Disabled, Back 1
        Color.FromArgb(250, 250, 250), // Button, Disabled, Back 2
        Color.FromArgb(205, 230, 247), // Button, Tracking, Back 1
        Color.FromArgb(205, 230, 247), // Button, Tracking, Back 2
        Color.FromArgb(146, 192, 244), // Button, Pressed, Back 1
        Color.FromArgb(146, 192, 244), // Button, Pressed, Back 2
        Color.FromArgb(146, 192, 244), // Button, Checked, Back 1
        Color.FromArgb(146, 192, 244), // Button, Checked, Back 2
        Color.FromArgb(255, 225, 104), // Button, Checked Tracking, Back 1
        Color.FromArgb(255, 249, 196) // Button, Checked Tracking, Back 2
    ];

    #endregion

    #endregion

    #region Instance Fields

    /// <inheritdoc/>
    protected override Color[] SchemeColors => _ribbonColors;
    private readonly Color[] _ribbonColors;

    protected readonly KryptonColorSchemeBase? BaseColors;
    private KryptonVisualStudio2010With2013ColorTable? _table;

    private readonly ImageList _checkBoxList;
    private readonly ImageList _galleryButtonList;
    private readonly Image?[] _radioButtonArray;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="PaletteVisualStudio2010With2013Base" /> class.</summary>
    /// <param name="schemeColors">The scheme colours.</param>
    /// <param name="checkBoxList">The check box list.</param>
    /// <param name="galleryButtonList">The gallery button list.</param>
    /// <param name="radioButtonArray">The radio button array.</param>
    /// <param name="trackBarColors">The track bar colours.</param>
    [System.Obsolete("Color[] constructor is deprecated and will be removed in V110. Use KryptonColorSchemeBase overload.", false)]
    public PaletteVisualStudio2010With2013Base([DisallowNull] Color[] schemeColors,
        [DisallowNull] ImageList checkBoxList,
        [DisallowNull] ImageList galleryButtonList,
        [DisallowNull] Image?[] radioButtonArray, Color[] trackBarColors)
    {
        Debug.Assert(schemeColors != null);
        Debug.Assert(checkBoxList != null);
        Debug.Assert(galleryButtonList != null);
        Debug.Assert(radioButtonArray != null);

        ThemeName = nameof(PaletteVisualStudio2010With2013Base);

        if (schemeColors != null)
        {
            _ribbonColors = schemeColors;
        }

        if (checkBoxList != null)
        {
            _checkBoxList = checkBoxList;
        }

        if (galleryButtonList != null)
        {
            _galleryButtonList = galleryButtonList;
        }

        if (radioButtonArray != null)
        {
            _radioButtonArray = radioButtonArray;
        }

        DefineFonts();
    }

    /// <summary>
    /// Overload that accepts a KryptonColorSchemeBase instance and forwards colours to the main constructor.
    /// </summary>
    // TODO this should be merged into main constructor once all palettes
    // have their own KryptonColorSchemeBase-derived class
    protected PaletteVisualStudio2010With2013Base(
        [DisallowNull] KryptonColorSchemeBase scheme,
        [DisallowNull] ImageList checkBoxList,
        [DisallowNull] ImageList galleryButtonList,
        [DisallowNull] Image?[] radioButtonArray)
        : this(scheme.ToArray(),
               checkBoxList,
               galleryButtonList,
               radioButtonArray,
               scheme.ToTrackBarArray())
    {
        BaseColors = scheme;
    }

    #endregion

    #region Renderer
    /// <summary>Gets the renderer to use for this palette.</summary>
    /// <returns> Renderer to use for drawing palette settings.</returns>
    public override IRenderer GetRenderer() => KryptonManager.RenderVisualStudio2010With2013;

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
            PaletteBackStyle.ButtonLowProfile or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonCalendarDay or PaletteBackStyle.ButtonNavigatorOverflow or PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose => state switch
            {
                PaletteState.Disabled or PaletteState.Normal or PaletteState.NormalDefaultOverride => InheritBool.False,
                _ => InheritBool.True
            },
            PaletteBackStyle.ContextMenuItemImage or PaletteBackStyle.ContextMenuItemHighlight => state switch
            {
                PaletteState.Normal or PaletteState.NormalDefaultOverride => InheritBool.False,
                _ => InheritBool.True
            },
            PaletteBackStyle.ButtonInputControl => state is PaletteState.Disabled or PaletteState.Normal ? InheritBool.False : InheritBool.True,
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
            PaletteBackStyle.TabHighProfile or PaletteBackStyle.TabStandardProfile or PaletteBackStyle.TabLowProfile or PaletteBackStyle.TabOneNote or PaletteBackStyle.TabDock or PaletteBackStyle.TabDockAutoHidden or PaletteBackStyle.TabCustom1 or PaletteBackStyle.TabCustom2 or PaletteBackStyle.TabCustom3 or PaletteBackStyle.PanelClient or PaletteBackStyle.PanelRibbonInactive or PaletteBackStyle.PanelAlternate or PaletteBackStyle.PanelCustom1 or PaletteBackStyle.PanelCustom2 or PaletteBackStyle.PanelCustom3 or PaletteBackStyle.SeparatorHighInternalProfile or PaletteBackStyle.SeparatorHighProfile or PaletteBackStyle.SeparatorLowProfile or PaletteBackStyle.SeparatorCustom1 or PaletteBackStyle.SeparatorCustom2 or PaletteBackStyle.SeparatorCustom3 or PaletteBackStyle.Control or PaletteBackStyle.ControlClient or PaletteBackStyle.ControlAlternate or PaletteBackStyle.ControlGroupBox or PaletteBackStyle.ControlToolTip or PaletteBackStyle.ControlRibbon or PaletteBackStyle.ControlRibbonAppMenu or PaletteBackStyle.ControlCustom1 or PaletteBackStyle.ControlCustom2 or PaletteBackStyle.ControlCustom3 or PaletteBackStyle.ContextMenuOuter or PaletteBackStyle.ContextMenuInner or PaletteBackStyle.ContextMenuHeading or PaletteBackStyle.ContextMenuSeparator or PaletteBackStyle.ContextMenuItemSplit or PaletteBackStyle.ContextMenuItemImageColumn or PaletteBackStyle.ContextMenuItemImage or PaletteBackStyle.ContextMenuItemHighlight or PaletteBackStyle.InputControlStandalone or PaletteBackStyle.InputControlRibbon or PaletteBackStyle.InputControlCustom1 or PaletteBackStyle.InputControlCustom2 or PaletteBackStyle.InputControlCustom3 or PaletteBackStyle.FormMain or PaletteBackStyle.FormCustom1 or PaletteBackStyle.FormCustom2 or PaletteBackStyle.FormCustom3 or PaletteBackStyle.HeaderPrimary or PaletteBackStyle.HeaderDockInactive or PaletteBackStyle.HeaderDockActive or PaletteBackStyle.HeaderSecondary or PaletteBackStyle.HeaderForm or PaletteBackStyle.HeaderCalendar or PaletteBackStyle.HeaderCustom1 or PaletteBackStyle.HeaderCustom2 or PaletteBackStyle.HeaderCustom3 or PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonGallery or PaletteBackStyle.ButtonAlternate or PaletteBackStyle.ButtonLowProfile or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonCalendarDay or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonCluster or PaletteBackStyle.ButtonNavigatorStack or PaletteBackStyle.ButtonNavigatorOverflow or PaletteBackStyle.ButtonNavigatorMini or PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose or PaletteBackStyle.ButtonCustom1 or PaletteBackStyle.ButtonCustom2 or PaletteBackStyle.ButtonCustom3 or PaletteBackStyle.ButtonInputControl or PaletteBackStyle.GridBackgroundList or PaletteBackStyle.GridBackgroundSheet or PaletteBackStyle.GridBackgroundCustom1
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
            case PaletteBackStyle.GridHeaderColumnCustom1:
            case PaletteBackStyle.GridHeaderColumnCustom2:
            case PaletteBackStyle.GridHeaderColumnCustom3:
            case PaletteBackStyle.GridHeaderRowList:
            case PaletteBackStyle.GridHeaderRowCustom1:
            case PaletteBackStyle.GridHeaderRowCustom2:
            case PaletteBackStyle.GridHeaderRowCustom3:
                return state switch
                {
                    PaletteState.Disabled => _disabledBack,
                    PaletteState.Pressed => _ribbonColors[(int)SchemeBaseColors.GridListPressed1],
                    PaletteState.CheckedNormal => _ribbonColors[(int)SchemeBaseColors.GridListSelected],
                    _ => _ribbonColors[(int)SchemeBaseColors.GridListNormal1]
                };
            case PaletteBackStyle.GridHeaderColumnSheet:
                return state switch
                {
                    PaletteState.Disabled => _disabledBack,
                    PaletteState.Tracking or PaletteState.Pressed => _ribbonColors[(int)SchemeBaseColors.GridSheetColPressed1],
                    PaletteState.CheckedNormal => _ribbonColors[(int)SchemeBaseColors.GridSheetColSelected1],
                    _ => _ribbonColors[(int)SchemeBaseColors.GridSheetColNormal1]
                };
            case PaletteBackStyle.GridHeaderRowSheet:
                return state switch
                {
                    PaletteState.Disabled => _disabledBack,
                    PaletteState.Tracking or PaletteState.Pressed => _ribbonColors[(int)SchemeBaseColors.GridSheetRowPressed],
                    PaletteState.CheckedNormal => _ribbonColors[(int)SchemeBaseColors.GridSheetRowSelected],
                    _ => _ribbonColors[(int)SchemeBaseColors.GridSheetRowNormal]
                };
            case PaletteBackStyle.GridDataCellList:
            case PaletteBackStyle.GridDataCellCustom1:
            case PaletteBackStyle.GridDataCellCustom2:
            case PaletteBackStyle.GridDataCellCustom3:
                return state == PaletteState.CheckedNormal ? _ribbonColors[(int)SchemeBaseColors.GridDataCellSelected] : SystemColors.Window;

            case PaletteBackStyle.GridDataCellSheet:
                return state == PaletteState.CheckedNormal ? _buttonBackColors[6] : SystemColors.Window;

            case PaletteBackStyle.TabHighProfile:
            case PaletteBackStyle.TabStandardProfile:
            case PaletteBackStyle.TabLowProfile:
            case PaletteBackStyle.TabOneNote:
            case PaletteBackStyle.TabCustom1:
            case PaletteBackStyle.TabCustom2:
            case PaletteBackStyle.TabCustom3:
                switch (state)
                {
                    case PaletteState.Disabled:
                        return style == PaletteBackStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : _disabledBack;

                    case PaletteState.Normal:
                        return style == PaletteBackStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : SystemColors.Window;

                    case PaletteState.Pressed:
                    case PaletteState.Tracking:
                        switch (style)
                        {
                            case PaletteBackStyle.TabLowProfile:
                                return GlobalStaticValues.EMPTY_COLOR;
                            case PaletteBackStyle.TabHighProfile:
                                return state == PaletteState.Tracking ? _buttonBackColors[2] : _buttonBackColors[4];
                            default:
                                return SystemColors.Window;
                        }

                    case PaletteState.CheckedNormal:
                    case PaletteState.CheckedPressed:
                    case PaletteState.CheckedTracking:
                        if (style == PaletteBackStyle.TabHighProfile)
                        {
                            if (state == PaletteState.CheckedNormal)
                            {
                                return _buttonBackColors[6];
                            }
                            else
                            {
                                return state == PaletteState.CheckedPressed ? _buttonBackColors[4] : _buttonBackColors[8];
                            }
                        }
                        else
                        {
                            return SystemColors.Window;
                        }

                    default:
                        throw DebugTools.NotImplemented(state.ToString());
                }
            case PaletteBackStyle.TabDock:
            case PaletteBackStyle.TabDockAutoHidden:
                return state switch
                {
                    PaletteState.Disabled => _disabledBack,
                    PaletteState.Normal or PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking or PaletteState.Pressed or PaletteState.Tracking => SystemColors.Window,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBackStyle.HeaderForm:
                return state == PaletteState.Disabled
                    ? _ribbonColors[(int)SchemeBaseColors.FormBorderHeaderInactive1]
                    : _ribbonColors[(int)SchemeBaseColors.FormBorderHeaderActive1];

            case PaletteBackStyle.HeaderCalendar:
                return state == PaletteState.Disabled
                    ? _ribbonColors[(int)SchemeBaseColors.HeaderPrimaryBack1]
                    : _ribbonColors[(int)SchemeBaseColors.HeaderPrimaryBack2];

            case PaletteBackStyle.HeaderPrimary:
            case PaletteBackStyle.HeaderCustom1:
            case PaletteBackStyle.HeaderCustom2:
            case PaletteBackStyle.HeaderCustom3:
                return state == PaletteState.Disabled ? _disabledBack : _ribbonColors[(int)SchemeBaseColors.HeaderPrimaryBack1];

            case PaletteBackStyle.HeaderDockInactive:
                return state == PaletteState.Disabled ? _disabledBack : _ribbonColors[(int)SchemeBaseColors.HeaderDockInactiveBack1];

            case PaletteBackStyle.HeaderDockActive:
                return state == PaletteState.Disabled ? _disabledBack : _buttonBackColors[6];

            case PaletteBackStyle.HeaderSecondary:
                return state == PaletteState.Disabled ? _disabledBack : _ribbonColors[(int)SchemeBaseColors.HeaderSecondaryBack1];

            case PaletteBackStyle.SeparatorHighInternalProfile:
                return state == PaletteState.Disabled ? _disabledBack : _ribbonColors[(int)SchemeBaseColors.SeparatorHighInternalBorder1];

            case PaletteBackStyle.SeparatorHighProfile:
                return state == PaletteState.Disabled ? _disabledBack : _ribbonColors[(int)SchemeBaseColors.SeparatorHighBorder1];

            case PaletteBackStyle.SeparatorLowProfile:
            case PaletteBackStyle.SeparatorCustom1:
            case PaletteBackStyle.SeparatorCustom2:
            case PaletteBackStyle.SeparatorCustom3:
            case PaletteBackStyle.PanelClient:
            case PaletteBackStyle.PanelCustom1:
            case PaletteBackStyle.PanelCustom2:
            case PaletteBackStyle.PanelCustom3:
            case PaletteBackStyle.ControlGroupBox:
            case PaletteBackStyle.GridBackgroundList:
            case PaletteBackStyle.GridBackgroundSheet:
            case PaletteBackStyle.GridBackgroundCustom1:
            case PaletteBackStyle.GridBackgroundCustom2:
            case PaletteBackStyle.GridBackgroundCustom3:
                return _ribbonColors[(int)SchemeBaseColors.PanelClient];
            case PaletteBackStyle.PanelAlternate:
                return _ribbonColors[(int)SchemeBaseColors.PanelAlternative];
            case PaletteBackStyle.PanelRibbonInactive:
                return _ribbonColors[(int)SchemeBaseColors.FormBorderInactiveLight];
            case PaletteBackStyle.FormMain:
            case PaletteBackStyle.FormCustom1:
            case PaletteBackStyle.FormCustom2:
            case PaletteBackStyle.FormCustom3:
                return state == PaletteState.Disabled
                    ? _ribbonColors[(int)SchemeBaseColors.FormBorderInactiveLight]
                    : _ribbonColors[(int)SchemeBaseColors.FormBorderActiveLight];
            case PaletteBackStyle.Control:
            case PaletteBackStyle.ControlClient:
            case PaletteBackStyle.ControlAlternate:
            case PaletteBackStyle.ControlCustom1:
            case PaletteBackStyle.ControlCustom2:
            case PaletteBackStyle.ControlCustom3:
                return SystemColors.Window;
            case PaletteBackStyle.InputControlStandalone:
            case PaletteBackStyle.InputControlRibbon:
            case PaletteBackStyle.InputControlCustom1:
            case PaletteBackStyle.InputControlCustom2:
            case PaletteBackStyle.InputControlCustom3:
                if (state == PaletteState.Disabled)
                {
                    return _ribbonColors[(int)SchemeBaseColors.InputControlBackDisabled];
                }
                else
                {
                    return (state == PaletteState.Tracking) || (style == PaletteBackStyle.InputControlStandalone)
                        ? _ribbonColors[(int)SchemeBaseColors.InputControlBackNormal]
                        : _ribbonColors[(int)SchemeBaseColors.InputControlBackInactive];
                }
            case PaletteBackStyle.ControlRibbon:
                return _ribbonColors[(int)SchemeBaseColors.RibbonTabSelected4];
            case PaletteBackStyle.ControlRibbonAppMenu:
                return _ribbonColors[(int)SchemeBaseColors.AppButtonBack1];
            case PaletteBackStyle.ControlToolTip:
                return _toolTipBack1;
            case PaletteBackStyle.ContextMenuOuter:
                return _contextMenuBack;
            case PaletteBackStyle.ContextMenuSeparator:
            case PaletteBackStyle.ContextMenuItemSplit:
                return state switch
                {
                    PaletteState.Tracking => _buttonBackColors[2],
                    _ => _contextMenuBack
                };
            case PaletteBackStyle.ContextMenuInner:
                return _contextMenuBack;
            case PaletteBackStyle.ContextMenuHeading:
                return _ribbonColors[(int)SchemeBaseColors.ContextMenuHeadingBack];
            case PaletteBackStyle.ContextMenuItemImageColumn:
                return _ribbonColors[(int)SchemeBaseColors.ContextMenuImageColumn];
            case PaletteBackStyle.ContextMenuItemImage:
                return _contextMenuImageBackChecked;
            case PaletteBackStyle.ButtonForm:
                return state switch
                {
                    PaletteState.Disabled or PaletteState.Normal or PaletteState.NormalDefaultOverride => GlobalStaticValues.EMPTY_COLOR,
                    PaletteState.CheckedNormal => _ribbonColors[(int)SchemeBaseColors.FormButtonBack1Checked],
                    PaletteState.Tracking => _ribbonColors[(int)SchemeBaseColors.FormButtonBack1Track],
                    PaletteState.CheckedTracking => _ribbonColors[(int)SchemeBaseColors.FormButtonBack1CheckTrack],
                    PaletteState.Pressed or PaletteState.CheckedPressed => _ribbonColors[(int)SchemeBaseColors.FormButtonBack1Pressed],
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBackStyle.ButtonFormClose:
                return state switch
                {
                    PaletteState.Disabled or PaletteState.Normal or PaletteState.NormalDefaultOverride => GlobalStaticValues.EMPTY_COLOR,
                    PaletteState.CheckedNormal => _formCloseChecked1,
                    PaletteState.Tracking => _formCloseTracking1,
                    PaletteState.CheckedTracking => _formCloseCheckedTracking1,
                    PaletteState.Pressed or PaletteState.CheckedPressed => _formClosePressed1,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBackStyle.ButtonStandalone:
            case PaletteBackStyle.ButtonGallery:
            case PaletteBackStyle.ButtonAlternate:
            case PaletteBackStyle.ButtonLowProfile:
            case PaletteBackStyle.ButtonBreadCrumb:
            case PaletteBackStyle.ButtonListItem:
            case PaletteBackStyle.ButtonCommand:
            case PaletteBackStyle.ButtonButtonSpec:
            case PaletteBackStyle.ButtonCalendarDay:
            case PaletteBackStyle.ButtonCluster:
            case PaletteBackStyle.ButtonCustom1:
            case PaletteBackStyle.ButtonCustom2:
            case PaletteBackStyle.ButtonCustom3:
            case PaletteBackStyle.ButtonInputControl:
            case PaletteBackStyle.ContextMenuItemHighlight:
                return state switch
                {
                    PaletteState.Disabled => style == PaletteBackStyle.ButtonGallery ? _ribbonColors[(int)SchemeBaseColors.RibbonGalleryBack1] : _disabledBack,
                    PaletteState.Normal => _ribbonColors[(int)SchemeBaseColors.ButtonNormalBack1],
                    PaletteState.NormalDefaultOverride => _ribbonColors[(int)SchemeBaseColors.ButtonNormalDefaultBack1],
                    PaletteState.CheckedNormal => style == PaletteBackStyle.ButtonInputControl
                        ? _ribbonColors[(int)SchemeBaseColors.ButtonNormalBack1]
                        : _buttonBackColors[6],
                    PaletteState.Tracking => _buttonBackColors[2],
                    PaletteState.Pressed or PaletteState.CheckedPressed => _buttonBackColors[4],
                    PaletteState.CheckedTracking => style == PaletteBackStyle.ButtonInputControl
                        ? _ribbonColors[(int)SchemeBaseColors.ButtonNormalBack1]
                        : _buttonBackColors[8],
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBackStyle.ButtonNavigatorStack:
            case PaletteBackStyle.ButtonNavigatorOverflow:
            case PaletteBackStyle.ButtonNavigatorMini:
                return state switch
                {
                    PaletteState.Disabled => _buttonBackColors[1],
                    PaletteState.Tracking => _ribbonColors[(int)SchemeBaseColors.ButtonNavigatorTrack1],
                    PaletteState.Pressed => _ribbonColors[(int)SchemeBaseColors.ButtonNavigatorPressed1],
                    PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed => _ribbonColors[(int)SchemeBaseColors.ButtonNavigatorChecked1],
                    _ => _ribbonColors[(int)SchemeBaseColors.ButtonNormalNavigatorBack1]
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
            case PaletteBackStyle.GridHeaderColumnCustom1:
            case PaletteBackStyle.GridHeaderColumnCustom2:
            case PaletteBackStyle.GridHeaderColumnCustom3:
            case PaletteBackStyle.GridHeaderRowList:
            case PaletteBackStyle.GridHeaderRowCustom1:
            case PaletteBackStyle.GridHeaderRowCustom2:
            case PaletteBackStyle.GridHeaderRowCustom3:
                return state switch
                {
                    PaletteState.Disabled => _disabledBack,
                    PaletteState.Pressed => _ribbonColors[(int)SchemeBaseColors.GridListPressed2],
                    PaletteState.CheckedNormal => _ribbonColors[(int)SchemeBaseColors.GridListSelected],
                    _ => _ribbonColors[(int)SchemeBaseColors.GridListNormal2]
                };
            case PaletteBackStyle.GridHeaderColumnSheet:
                return state switch
                {
                    PaletteState.Disabled => _disabledBack,
                    PaletteState.Tracking or PaletteState.Pressed => _ribbonColors[(int)SchemeBaseColors.GridSheetColPressed2],
                    PaletteState.CheckedNormal => _ribbonColors[(int)SchemeBaseColors.GridSheetColSelected2],
                    _ => _ribbonColors[(int)SchemeBaseColors.GridSheetColNormal2]
                };
            case PaletteBackStyle.GridHeaderRowSheet:
                return state switch
                {
                    PaletteState.Disabled => _disabledBack,
                    PaletteState.Tracking or PaletteState.Pressed => _ribbonColors[(int)SchemeBaseColors.GridSheetRowPressed],
                    PaletteState.CheckedNormal => _ribbonColors[(int)SchemeBaseColors.GridSheetRowSelected],
                    _ => _ribbonColors[(int)SchemeBaseColors.GridSheetRowNormal]
                };
            case PaletteBackStyle.GridDataCellList:
            case PaletteBackStyle.GridDataCellCustom1:
            case PaletteBackStyle.GridDataCellCustom2:
            case PaletteBackStyle.GridDataCellCustom3:
                return state == PaletteState.CheckedNormal ? _ribbonColors[(int)SchemeBaseColors.GridDataCellSelected] : SystemColors.Window;

            case PaletteBackStyle.GridDataCellSheet:
                return state == PaletteState.CheckedNormal ? _buttonBackColors[7] : SystemColors.Window;

            case PaletteBackStyle.TabHighProfile:
            case PaletteBackStyle.TabStandardProfile:
            case PaletteBackStyle.TabLowProfile:
            case PaletteBackStyle.TabOneNote:
            case PaletteBackStyle.TabCustom1:
            case PaletteBackStyle.TabCustom2:
            case PaletteBackStyle.TabCustom3:
                return state switch
                {
                    PaletteState.Disabled => style == PaletteBackStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : _disabledBack,
                    PaletteState.Normal => style == PaletteBackStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : _ribbonColors[(int)SchemeBaseColors.ButtonNormalBack2],
                    PaletteState.Tracking or PaletteState.Pressed => style == PaletteBackStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : SystemColors.Window,
                    PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => SystemColors.Window,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBackStyle.TabDock:
                return state switch
                {
                    PaletteState.Disabled => _disabledBack,
                    PaletteState.Normal => _ribbonColors[(int)SchemeBaseColors.HeaderDockInactiveBack1],
                    PaletteState.Tracking or PaletteState.Pressed => _buttonBackColors[4],
                    PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => SystemColors.Window,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBackStyle.TabDockAutoHidden:
                return state switch
                {
                    PaletteState.Disabled => _disabledBack,
                    PaletteState.Normal or PaletteState.CheckedNormal => _ribbonColors[(int)SchemeBaseColors.HeaderDockInactiveBack1],
                    PaletteState.Tracking or PaletteState.CheckedTracking or PaletteState.Pressed or PaletteState.CheckedPressed => _buttonBackColors[4],
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBackStyle.HeaderForm:
                return state == PaletteState.Disabled
                    ? _ribbonColors[(int)SchemeBaseColors.FormBorderHeaderInactive2]
                    : _ribbonColors[(int)SchemeBaseColors.FormBorderHeaderActive2];

            case PaletteBackStyle.HeaderCalendar:
                return state == PaletteState.Disabled
                    ? _ribbonColors[(int)SchemeBaseColors.HeaderPrimaryBack1]
                    : _ribbonColors[(int)SchemeBaseColors.HeaderPrimaryBack2];

            case PaletteBackStyle.HeaderPrimary:
            case PaletteBackStyle.HeaderCustom1:
            case PaletteBackStyle.HeaderCustom2:
            case PaletteBackStyle.HeaderCustom3:
                return state == PaletteState.Disabled ? _disabledBack : _ribbonColors[(int)SchemeBaseColors.HeaderPrimaryBack2];

            case PaletteBackStyle.HeaderDockInactive:
                return state == PaletteState.Disabled ? _disabledBack : _ribbonColors[(int)SchemeBaseColors.HeaderDockInactiveBack2];

            case PaletteBackStyle.HeaderDockActive:
                return state == PaletteState.Disabled ? _disabledBack : _buttonBackColors[7];

            case PaletteBackStyle.HeaderSecondary:
                return state == PaletteState.Disabled ? _disabledBack : _ribbonColors[(int)SchemeBaseColors.HeaderSecondaryBack2];

            case PaletteBackStyle.SeparatorHighInternalProfile:
                return state == PaletteState.Disabled ? _disabledBack : _ribbonColors[(int)SchemeBaseColors.SeparatorHighInternalBorder2];

            case PaletteBackStyle.SeparatorHighProfile:
                return state == PaletteState.Disabled ? _disabledBack : _ribbonColors[(int)SchemeBaseColors.SeparatorHighBorder2];

            case PaletteBackStyle.SeparatorLowProfile:
            case PaletteBackStyle.SeparatorCustom1:
            case PaletteBackStyle.SeparatorCustom2:
            case PaletteBackStyle.SeparatorCustom3:
            case PaletteBackStyle.PanelClient:
            case PaletteBackStyle.PanelCustom1:
            case PaletteBackStyle.PanelCustom2:
            case PaletteBackStyle.PanelCustom3:
            case PaletteBackStyle.ControlGroupBox:
            case PaletteBackStyle.GridBackgroundList:
            case PaletteBackStyle.GridBackgroundSheet:
            case PaletteBackStyle.GridBackgroundCustom1:
            case PaletteBackStyle.GridBackgroundCustom2:
            case PaletteBackStyle.GridBackgroundCustom3:
                return _ribbonColors[(int)SchemeBaseColors.PanelClient];
            case PaletteBackStyle.PanelAlternate:
                return _ribbonColors[(int)SchemeBaseColors.PanelAlternative];
            case PaletteBackStyle.PanelRibbonInactive:
                return _ribbonColors[(int)SchemeBaseColors.FormBorderInactiveDark];
            case PaletteBackStyle.FormMain:
            case PaletteBackStyle.FormCustom1:
            case PaletteBackStyle.FormCustom2:
            case PaletteBackStyle.FormCustom3:
                return state == PaletteState.Disabled
                    ? _ribbonColors[(int)SchemeBaseColors.FormBorderInactiveDark]
                    : _ribbonColors[(int)SchemeBaseColors.FormBorderActiveDark];
            case PaletteBackStyle.Control:
            case PaletteBackStyle.ControlClient:
            case PaletteBackStyle.ControlAlternate:
            case PaletteBackStyle.ControlCustom1:
            case PaletteBackStyle.ControlCustom2:
            case PaletteBackStyle.ControlCustom3:
                return SystemColors.Window;
            case PaletteBackStyle.InputControlStandalone:
            case PaletteBackStyle.InputControlRibbon:
            case PaletteBackStyle.InputControlCustom1:
            case PaletteBackStyle.InputControlCustom2:
            case PaletteBackStyle.InputControlCustom3:
                if (state == PaletteState.Disabled)
                {
                    return _ribbonColors[(int)SchemeBaseColors.InputControlBackDisabled];
                }
                else
                {
                    return (state == PaletteState.Tracking) || (style == PaletteBackStyle.InputControlStandalone)
                        ? _ribbonColors[(int)SchemeBaseColors.InputControlBackNormal]
                        : _ribbonColors[(int)SchemeBaseColors.InputControlBackInactive];
                }
            case PaletteBackStyle.ControlRibbon:
                return _ribbonColors[(int)SchemeBaseColors.RibbonTabSelected4];
            case PaletteBackStyle.ControlRibbonAppMenu:
                return _ribbonColors[(int)SchemeBaseColors.AppButtonBack2];
            case PaletteBackStyle.ControlToolTip:
                return _ribbonColors[(int)SchemeBaseColors.ToolTipBottom];
            case PaletteBackStyle.ContextMenuOuter:
                return _contextMenuBack;
            case PaletteBackStyle.ContextMenuSeparator:
            case PaletteBackStyle.ContextMenuItemSplit:
                return state switch
                {
                    PaletteState.Tracking => _buttonBackColors[3],
                    _ => _contextMenuBack
                };
            case PaletteBackStyle.ContextMenuInner:
                return _contextMenuBack;
            case PaletteBackStyle.ContextMenuHeading:
                return _ribbonColors[(int)SchemeBaseColors.ContextMenuHeadingBack];
            case PaletteBackStyle.ContextMenuItemImageColumn:
                return _ribbonColors[(int)SchemeBaseColors.ContextMenuImageColumn];
            case PaletteBackStyle.ContextMenuItemImage:
                return _contextMenuImageBackChecked;
            case PaletteBackStyle.ButtonForm:
                return state switch
                {
                    PaletteState.Disabled or PaletteState.Normal or PaletteState.NormalDefaultOverride => GlobalStaticValues.EMPTY_COLOR,
                    PaletteState.CheckedNormal => _ribbonColors[(int)SchemeBaseColors.FormButtonBack2Checked],
                    PaletteState.Tracking => _ribbonColors[(int)SchemeBaseColors.FormButtonBack2Track],
                    PaletteState.CheckedTracking => _ribbonColors[(int)SchemeBaseColors.FormButtonBack2CheckTrack],
                    PaletteState.Pressed or PaletteState.CheckedPressed => _ribbonColors[(int)SchemeBaseColors.FormButtonBack2Pressed],
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBackStyle.ButtonFormClose:
                return state switch
                {
                    PaletteState.Disabled or PaletteState.Normal or PaletteState.NormalDefaultOverride => GlobalStaticValues.EMPTY_COLOR,
                    PaletteState.CheckedNormal => _formCloseChecked2,
                    PaletteState.Tracking => _formCloseTracking2,
                    PaletteState.CheckedTracking => _formCloseCheckedTracking2,
                    PaletteState.Pressed or PaletteState.CheckedPressed => _formClosePressed2,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBackStyle.ButtonStandalone:
            case PaletteBackStyle.ButtonGallery:
            case PaletteBackStyle.ButtonAlternate:
            case PaletteBackStyle.ButtonLowProfile:
            case PaletteBackStyle.ButtonBreadCrumb:
            case PaletteBackStyle.ButtonListItem:
            case PaletteBackStyle.ButtonCommand:
            case PaletteBackStyle.ButtonButtonSpec:
            case PaletteBackStyle.ButtonCalendarDay:
            case PaletteBackStyle.ButtonCluster:
            case PaletteBackStyle.ButtonCustom1:
            case PaletteBackStyle.ButtonCustom2:
            case PaletteBackStyle.ButtonCustom3:
            case PaletteBackStyle.ButtonInputControl:
            case PaletteBackStyle.ContextMenuItemHighlight:
                return state switch
                {
                    PaletteState.Disabled => style == PaletteBackStyle.ButtonGallery ? _ribbonColors[(int)SchemeBaseColors.RibbonGalleryBack1] : _buttonBackColors[1],
                    PaletteState.Normal => _ribbonColors[(int)SchemeBaseColors.ButtonNormalBack2],
                    PaletteState.NormalDefaultOverride => style is PaletteBackStyle.ButtonLowProfile or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ContextMenuItemHighlight
                        ? GlobalStaticValues.EMPTY_COLOR
                        : _ribbonColors[(int)SchemeBaseColors.ButtonNormalDefaultBack2],
                    PaletteState.CheckedNormal => style == PaletteBackStyle.ButtonInputControl
                        ? _ribbonColors[(int)SchemeBaseColors.ButtonNormalBack2]
                        : _buttonBackColors[7],
                    PaletteState.Tracking => _buttonBackColors[3],
                    PaletteState.Pressed or PaletteState.CheckedPressed => _buttonBackColors[5],
                    PaletteState.CheckedTracking => style == PaletteBackStyle.ButtonInputControl
                        ? _ribbonColors[(int)SchemeBaseColors.ButtonNormalBack1]
                        : _buttonBackColors[9],
                    _ => throw DebugTools.NotImplemented(state.ToString())
                };
            case PaletteBackStyle.ButtonNavigatorStack:
            case PaletteBackStyle.ButtonNavigatorOverflow:
            case PaletteBackStyle.ButtonNavigatorMini:
                return state switch
                {
                    PaletteState.Disabled => _buttonBackColors[1],
                    PaletteState.Tracking => _ribbonColors[(int)SchemeBaseColors.ButtonNavigatorTrack2],
                    PaletteState.Pressed => _ribbonColors[(int)SchemeBaseColors.ButtonNavigatorPressed2],
                    PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed => _ribbonColors[(int)SchemeBaseColors.ButtonNavigatorChecked2],
                    _ => _ribbonColors[(int)SchemeBaseColors.ButtonNormalNavigatorBack2]
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
            PaletteBackStyle.HeaderForm => PaletteColorStyle.Rounding5,
            PaletteBackStyle.GridHeaderColumnList or PaletteBackStyle.GridHeaderColumnCustom1 or PaletteBackStyle.GridHeaderColumnCustom2 or PaletteBackStyle.GridHeaderColumnCustom3 => PaletteColorStyle.Rounded,
            PaletteBackStyle.GridHeaderRowList or PaletteBackStyle.GridHeaderRowCustom1 or PaletteBackStyle.GridHeaderRowCustom2 or PaletteBackStyle.GridHeaderRowCustom3 => PaletteColorStyle.Linear,
            PaletteBackStyle.GridHeaderColumnSheet or PaletteBackStyle.GridHeaderRowSheet => PaletteColorStyle.Linear,
            PaletteBackStyle.GridDataCellList or PaletteBackStyle.GridDataCellCustom1 or PaletteBackStyle.GridDataCellCustom2 or PaletteBackStyle.GridDataCellCustom3 => PaletteColorStyle.Solid,
            PaletteBackStyle.GridDataCellSheet => PaletteColorStyle.ExpertChecked,
            PaletteBackStyle.TabHighProfile or PaletteBackStyle.TabCustom1 or PaletteBackStyle.TabCustom2 or PaletteBackStyle.TabCustom3 => state switch
            {
                PaletteState.Tracking or PaletteState.Pressed or PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed => PaletteColorStyle.GlassFade,
                _ => PaletteColorStyle.QuarterPhase
            },
            PaletteBackStyle.TabStandardProfile => state switch
            {
                PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed => PaletteColorStyle.Solid,
                PaletteState.Tracking or PaletteState.Pressed => PaletteColorStyle.GlassFade,
                _ => PaletteColorStyle.QuarterPhase
            },
            PaletteBackStyle.TabLowProfile => PaletteColorStyle.Solid,
            PaletteBackStyle.TabOneNote or PaletteBackStyle.TabDock or PaletteBackStyle.TabDockAutoHidden => PaletteColorStyle.Linear,
            PaletteBackStyle.PanelClient or PaletteBackStyle.PanelRibbonInactive or PaletteBackStyle.PanelAlternate or PaletteBackStyle.PanelCustom1 or PaletteBackStyle.PanelCustom2 or PaletteBackStyle.PanelCustom3 or PaletteBackStyle.SeparatorLowProfile or PaletteBackStyle.SeparatorCustom1 or PaletteBackStyle.SeparatorCustom2 or PaletteBackStyle.SeparatorCustom3 or PaletteBackStyle.Control or PaletteBackStyle.ControlClient or PaletteBackStyle.ControlAlternate or PaletteBackStyle.ControlGroupBox or PaletteBackStyle.ControlRibbon or PaletteBackStyle.ContextMenuOuter or PaletteBackStyle.ContextMenuInner or PaletteBackStyle.ControlCustom1 or PaletteBackStyle.ControlCustom2 or PaletteBackStyle.ControlCustom3 or PaletteBackStyle.ContextMenuHeading or PaletteBackStyle.ContextMenuItemImageColumn or PaletteBackStyle.InputControlStandalone or PaletteBackStyle.InputControlRibbon or PaletteBackStyle.InputControlCustom1 or PaletteBackStyle.InputControlCustom2 or PaletteBackStyle.InputControlCustom3 or PaletteBackStyle.GridBackgroundList or PaletteBackStyle.GridBackgroundSheet or PaletteBackStyle.GridBackgroundCustom1
                or PaletteBackStyle.GridBackgroundCustom2
                or PaletteBackStyle.GridBackgroundCustom3
                or PaletteBackStyle.HeaderCalendar or PaletteBackStyle.ButtonCalendarDay => PaletteColorStyle.Solid,
            PaletteBackStyle.ControlRibbonAppMenu => PaletteColorStyle.Switch90,
            PaletteBackStyle.ContextMenuSeparator or PaletteBackStyle.ContextMenuItemSplit => state == PaletteState.Tracking ? PaletteColorStyle.GlassTrackingFull : PaletteColorStyle.Solid,
            PaletteBackStyle.ControlToolTip => PaletteColorStyle.Linear,
            PaletteBackStyle.FormMain or PaletteBackStyle.FormCustom1 or PaletteBackStyle.FormCustom2 or PaletteBackStyle.FormCustom3 => PaletteColorStyle.SolidAllLine,
            PaletteBackStyle.SeparatorHighProfile => PaletteColorStyle.RoundedTopLight,
            PaletteBackStyle.SeparatorHighInternalProfile => PaletteColorStyle.Linear,
            PaletteBackStyle.HeaderPrimary or PaletteBackStyle.HeaderDockInactive or PaletteBackStyle.HeaderSecondary or PaletteBackStyle.HeaderCustom1 or PaletteBackStyle.HeaderCustom2 or PaletteBackStyle.HeaderCustom3 or PaletteBackStyle.HeaderDockActive => PaletteColorStyle.Rounded,
            PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose => state switch
            {
                PaletteState.Disabled or PaletteState.Normal or PaletteState.NormalDefaultOverride or PaletteState.CheckedNormal or PaletteState.Tracking or PaletteState.CheckedTracking => PaletteColorStyle.Linear,
                PaletteState.Pressed or PaletteState.CheckedPressed => PaletteColorStyle.LinearShadow,
                _ => throw DebugTools.NotImplemented(state.ToString())
            },
            PaletteBackStyle.ButtonAlternate or PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonLowProfile or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonCluster or PaletteBackStyle.ButtonGallery or PaletteBackStyle.ButtonCustom1 or PaletteBackStyle.ButtonCustom2 or PaletteBackStyle.ButtonCustom3 or PaletteBackStyle.ButtonInputControl or PaletteBackStyle.ContextMenuItemHighlight => state switch
            {
                PaletteState.Disabled => PaletteColorStyle.Solid,
                PaletteState.Normal => PaletteColorStyle.Linear,
                PaletteState.Tracking => PaletteColorStyle.ExpertTracking,
                PaletteState.Pressed or PaletteState.CheckedPressed => PaletteColorStyle.ExpertPressed,
                PaletteState.CheckedNormal => PaletteColorStyle.ExpertChecked,
                PaletteState.CheckedTracking => PaletteColorStyle.ExpertCheckedTracking,
                _ => throw DebugTools.NotImplemented(state.ToString())
            },
            PaletteBackStyle.ContextMenuItemImage => PaletteColorStyle.Solid,
            PaletteBackStyle.ButtonNavigatorStack or PaletteBackStyle.ButtonNavigatorOverflow or PaletteBackStyle.ButtonNavigatorMini => state switch
            {
                PaletteState.Tracking or PaletteState.Pressed => PaletteColorStyle.SolidAllLine,
                PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed => PaletteColorStyle.ExpertSquareHighlight,
                _ => PaletteColorStyle.Solid
            },
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
            PaletteBackStyle.Control or PaletteBackStyle.ControlClient or PaletteBackStyle.ControlAlternate or PaletteBackStyle.ControlGroupBox or PaletteBackStyle.ControlRibbon or PaletteBackStyle.ControlRibbonAppMenu or PaletteBackStyle.ControlCustom1 or PaletteBackStyle.ControlCustom2 or PaletteBackStyle.ControlCustom3 or PaletteBackStyle.InputControlStandalone or PaletteBackStyle.InputControlRibbon or PaletteBackStyle.InputControlCustom1 or PaletteBackStyle.InputControlCustom2 or PaletteBackStyle.InputControlCustom3 or PaletteBackStyle.FormMain or PaletteBackStyle.FormCustom1 or PaletteBackStyle.FormCustom2 or PaletteBackStyle.FormCustom3 or PaletteBackStyle.PanelClient or PaletteBackStyle.PanelRibbonInactive or PaletteBackStyle.PanelAlternate or PaletteBackStyle.PanelCustom1 or PaletteBackStyle.PanelCustom2 or PaletteBackStyle.PanelCustom3 or PaletteBackStyle.GridBackgroundList or PaletteBackStyle.GridBackgroundSheet or PaletteBackStyle.GridBackgroundCustom1
                or PaletteBackStyle.GridBackgroundCustom2
                or PaletteBackStyle.GridBackgroundCustom3
                => PaletteRectangleAlign.Control,
            PaletteBackStyle.ControlToolTip or PaletteBackStyle.SeparatorLowProfile or PaletteBackStyle.SeparatorHighInternalProfile or PaletteBackStyle.SeparatorHighProfile or PaletteBackStyle.SeparatorCustom1 or PaletteBackStyle.SeparatorCustom2 or PaletteBackStyle.SeparatorCustom3 or PaletteBackStyle.HeaderPrimary or PaletteBackStyle.HeaderDockInactive or PaletteBackStyle.HeaderDockActive or PaletteBackStyle.HeaderCalendar or PaletteBackStyle.HeaderSecondary or PaletteBackStyle.HeaderForm or PaletteBackStyle.HeaderCustom1 or PaletteBackStyle.HeaderCustom2 or PaletteBackStyle.HeaderCustom3 or PaletteBackStyle.TabHighProfile or PaletteBackStyle.TabStandardProfile or PaletteBackStyle.TabLowProfile or PaletteBackStyle.TabOneNote or PaletteBackStyle.TabDock or PaletteBackStyle.TabDockAutoHidden or PaletteBackStyle.TabCustom1 or PaletteBackStyle.TabCustom2 or PaletteBackStyle.TabCustom3 or PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonGallery or PaletteBackStyle.ButtonAlternate or PaletteBackStyle.ButtonLowProfile or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonCalendarDay or PaletteBackStyle.ButtonCluster or PaletteBackStyle.ButtonNavigatorStack or PaletteBackStyle.ButtonNavigatorOverflow or PaletteBackStyle.ButtonNavigatorMini or PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose or PaletteBackStyle.ButtonCustom1 or PaletteBackStyle.ButtonCustom2 or PaletteBackStyle.ButtonCustom3 or PaletteBackStyle.ButtonInputControl or PaletteBackStyle.GridHeaderColumnList or PaletteBackStyle.GridHeaderColumnSheet or PaletteBackStyle.GridHeaderColumnCustom1 or PaletteBackStyle.GridHeaderColumnCustom2 or PaletteBackStyle.GridHeaderColumnCustom3 or PaletteBackStyle.GridHeaderRowList or PaletteBackStyle.GridHeaderRowSheet or PaletteBackStyle.GridHeaderRowCustom1 or PaletteBackStyle.GridHeaderRowCustom2 or PaletteBackStyle.GridHeaderRowCustom3 or PaletteBackStyle.GridDataCellList or PaletteBackStyle.GridDataCellSheet or PaletteBackStyle.GridDataCellCustom1 or PaletteBackStyle.GridDataCellCustom2 or PaletteBackStyle.GridDataCellCustom3 or PaletteBackStyle.ContextMenuItemImage or PaletteBackStyle.ContextMenuItemHighlight or PaletteBackStyle.ContextMenuOuter or PaletteBackStyle.ContextMenuInner or PaletteBackStyle.ContextMenuHeading or PaletteBackStyle.ContextMenuSeparator or PaletteBackStyle.ContextMenuItemSplit or PaletteBackStyle.ContextMenuItemImageColumn => PaletteRectangleAlign.Local,
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
            PaletteBackStyle.PanelClient or PaletteBackStyle.PanelRibbonInactive or PaletteBackStyle.PanelAlternate or PaletteBackStyle.PanelCustom1 or PaletteBackStyle.PanelCustom2 or PaletteBackStyle.PanelCustom3 or PaletteBackStyle.SeparatorLowProfile or PaletteBackStyle.SeparatorHighInternalProfile or PaletteBackStyle.SeparatorHighProfile or PaletteBackStyle.SeparatorCustom1 or PaletteBackStyle.SeparatorCustom2 or PaletteBackStyle.SeparatorCustom3 or PaletteBackStyle.Control or PaletteBackStyle.ControlClient or PaletteBackStyle.ControlAlternate or PaletteBackStyle.ControlGroupBox or PaletteBackStyle.ControlToolTip or PaletteBackStyle.ControlRibbon or PaletteBackStyle.ControlRibbonAppMenu or PaletteBackStyle.ControlCustom1 or PaletteBackStyle.ControlCustom2 or PaletteBackStyle.ControlCustom3 or PaletteBackStyle.ContextMenuOuter or PaletteBackStyle.ContextMenuInner or PaletteBackStyle.ContextMenuHeading or PaletteBackStyle.ContextMenuSeparator or PaletteBackStyle.ContextMenuItemSplit or PaletteBackStyle.ContextMenuItemImageColumn or PaletteBackStyle.InputControlStandalone or PaletteBackStyle.InputControlRibbon or PaletteBackStyle.InputControlCustom1 or PaletteBackStyle.InputControlCustom2 or PaletteBackStyle.InputControlCustom3 or PaletteBackStyle.FormMain or PaletteBackStyle.FormCustom1 or PaletteBackStyle.FormCustom2 or PaletteBackStyle.FormCustom3 or PaletteBackStyle.HeaderPrimary or PaletteBackStyle.HeaderDockInactive or PaletteBackStyle.HeaderDockActive or PaletteBackStyle.HeaderCalendar or PaletteBackStyle.HeaderSecondary or PaletteBackStyle.HeaderForm or PaletteBackStyle.HeaderCustom1 or PaletteBackStyle.HeaderCustom2 or PaletteBackStyle.HeaderCustom3 or PaletteBackStyle.TabHighProfile or PaletteBackStyle.TabStandardProfile or PaletteBackStyle.TabLowProfile or PaletteBackStyle.TabOneNote or PaletteBackStyle.TabDock or PaletteBackStyle.TabDockAutoHidden or PaletteBackStyle.TabCustom1 or PaletteBackStyle.TabCustom2 or PaletteBackStyle.TabCustom3 or PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonGallery or PaletteBackStyle.ButtonAlternate or PaletteBackStyle.ButtonLowProfile or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonCalendarDay or PaletteBackStyle.ButtonCluster or PaletteBackStyle.ButtonNavigatorStack or PaletteBackStyle.ButtonNavigatorOverflow or PaletteBackStyle.ButtonNavigatorMini or PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose or PaletteBackStyle.ButtonCustom1 or PaletteBackStyle.ButtonCustom2 or PaletteBackStyle.ButtonCustom3 or PaletteBackStyle.ButtonInputControl or PaletteBackStyle.ContextMenuItemImage or PaletteBackStyle.ContextMenuItemHighlight or PaletteBackStyle.GridBackgroundList or PaletteBackStyle.GridBackgroundSheet or PaletteBackStyle.GridBackgroundCustom1
                or PaletteBackStyle.GridBackgroundCustom2
                or PaletteBackStyle.GridBackgroundCustom3
                or PaletteBackStyle.GridHeaderColumnList or PaletteBackStyle.GridHeaderColumnSheet or PaletteBackStyle.GridHeaderColumnCustom1 or PaletteBackStyle.GridHeaderColumnCustom2 or PaletteBackStyle.GridHeaderColumnCustom3 or PaletteBackStyle.GridHeaderRowList or PaletteBackStyle.GridHeaderRowSheet or PaletteBackStyle.GridHeaderRowCustom1 or PaletteBackStyle.GridHeaderRowCustom2 or PaletteBackStyle.GridHeaderRowCustom3 or PaletteBackStyle.GridDataCellList or PaletteBackStyle.GridDataCellSheet or PaletteBackStyle.GridDataCellCustom1 or PaletteBackStyle.GridDataCellCustom2 or PaletteBackStyle.GridDataCellCustom3 => 90f,
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
            PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ContextMenuInner => InheritBool.False,
            PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => InheritBool.True,
            PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonInputControl => state switch
            {
                PaletteState.Disabled or PaletteState.Normal or PaletteState.NormalDefaultOverride => InheritBool.False,
                _ => InheritBool.True
            },
            PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight => state switch
            {
                PaletteState.Normal or PaletteState.NormalDefaultOverride => InheritBool.False,
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
            PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => PaletteDrawBorders.All,
            PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 => PaletteDrawBorders.All,
            PaletteBorderStyle.ContextMenuHeading => PaletteDrawBorders.Bottom,
            PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit => PaletteDrawBorders.Top,
            PaletteBorderStyle.ContextMenuItemImageColumn => PaletteDrawBorders.Right,
            PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ContextMenuInner => PaletteDrawBorders.None,
            PaletteBorderStyle.HeaderForm => PaletteDrawBorders.None,
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
            PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => PaletteGraphicsHint.AntiAlias,
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
        if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
        {
            // Check for the calendar day today override
            if (state == PaletteState.TodayOverride)
            {
                if (style == PaletteBorderStyle.ButtonCalendarDay)
                {
                    return state == PaletteState.Disabled ? _disabledBorder : _todayBorder;
                }
            }

            return GlobalStaticValues.EMPTY_COLOR;
        }

        return style switch
        {
            PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 => state switch
            {
                PaletteState.Disabled => style == PaletteBorderStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : _disabledBorder,
                PaletteState.Normal or PaletteState.Tracking or PaletteState.Pressed => style == PaletteBorderStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : _ribbonColors[(int)SchemeBaseColors.ButtonNormalBorder],
                PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => _ribbonColors[(int)SchemeBaseColors.ControlBorder],
                _ => throw DebugTools.NotImplemented(state.ToString())
            },
            PaletteBorderStyle.TabDock => state switch
            {
                PaletteState.Disabled => _disabledBorder,
                PaletteState.Normal => _ribbonColors[(int)SchemeBaseColors.ButtonNormalBorder],
                PaletteState.Tracking or PaletteState.Pressed => _buttonBorderColors[2],
                PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => _ribbonColors[(int)SchemeBaseColors.ControlBorder],
                _ => throw DebugTools.NotImplemented(state.ToString())
            },
            PaletteBorderStyle.TabDockAutoHidden => state switch
            {
                PaletteState.Disabled => _disabledBorder,
                PaletteState.Normal or PaletteState.CheckedNormal => _ribbonColors[(int)SchemeBaseColors.ButtonNormalBorder],
                PaletteState.Tracking or PaletteState.CheckedTracking or PaletteState.Pressed or PaletteState.CheckedPressed => _buttonBorderColors[2],
                _ => throw DebugTools.NotImplemented(state.ToString())
            },
            PaletteBorderStyle.HeaderCalendar => state == PaletteState.Disabled
                ? _ribbonColors[(int)SchemeBaseColors.HeaderPrimaryBack1]
                : _ribbonColors[(int)SchemeBaseColors.HeaderPrimaryBack2],
            PaletteBorderStyle.HeaderForm => state == PaletteState.Disabled
                ? _ribbonColors[(int)SchemeBaseColors.FormBorderHeaderInactive]
                : _ribbonColors[(int)SchemeBaseColors.FormBorderHeaderActive],
            PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 => state == PaletteState.Disabled ? _disabledBorder : _ribbonColors[(int)SchemeBaseColors.ControlBorder],
            PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuItemImageColumn => _contextMenuHeadingBorder,
            PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit => state switch
            {
                PaletteState.Disabled => _buttonBorderColors[0],
                PaletteState.Tracking => _buttonBorderColors[1],
                _ => _contextMenuHeadingBorder
            },
            PaletteBorderStyle.ContextMenuItemImage => _contextMenuImageBorderChecked,
            PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 => state == PaletteState.Disabled
                ? _ribbonColors[(int)SchemeBaseColors.InputControlBorderDisabled]
                : _ribbonColors[(int)SchemeBaseColors.InputControlBorderNormal],
            PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => state == PaletteState.Disabled ? _disabledBorder : _ribbonColors[(int)SchemeBaseColors.GridDataCellBorder],
            PaletteBorderStyle.ControlRibbon => state == PaletteState.Disabled ? _disabledBorder : _ribbonColors[(int)SchemeBaseColors.RibbonGroupsArea1],
            PaletteBorderStyle.ControlRibbonAppMenu => state == PaletteState.Disabled ? _disabledBorder : _ribbonColors[(int)SchemeBaseColors.AppButtonBorder],
            PaletteBorderStyle.ContextMenuOuter => _contextMenuBorder,
            PaletteBorderStyle.ContextMenuInner => _contextMenuBack,
            PaletteBorderStyle.ControlToolTip => state == PaletteState.Disabled ? _disabledBorder : _toolTipBorder,
            PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 => state == PaletteState.Disabled
                ? _ribbonColors[(int)SchemeBaseColors.FormBorderInactive]
                : _ribbonColors[(int)SchemeBaseColors.FormBorderActive],
            PaletteBorderStyle.ButtonForm => state switch
            {
                PaletteState.Disabled or PaletteState.Normal or PaletteState.NormalDefaultOverride => GlobalStaticValues.EMPTY_COLOR,
                PaletteState.CheckedNormal => _ribbonColors[(int)SchemeBaseColors.FormButtonBorderCheck],
                PaletteState.Tracking or PaletteState.CheckedTracking => _ribbonColors[(int)SchemeBaseColors.FormButtonBorderTrack],
                PaletteState.Pressed or PaletteState.CheckedPressed => _ribbonColors[(int)SchemeBaseColors.FormButtonBorderPressed],
                _ => throw DebugTools.NotImplemented(state.ToString())
            },
            PaletteBorderStyle.ButtonFormClose => state switch
            {
                PaletteState.Disabled or PaletteState.Normal or PaletteState.NormalDefaultOverride => GlobalStaticValues.EMPTY_COLOR,
                PaletteState.CheckedNormal => _formCloseBorderCheckedNormal,
                PaletteState.Tracking or PaletteState.CheckedTracking => _formCloseBorderTracking,
                PaletteState.Pressed or PaletteState.CheckedPressed => _formCloseBorderPressed,
                _ => throw DebugTools.NotImplemented(state.ToString())
            },
            PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ContextMenuItemHighlight => state switch
            {
                PaletteState.Disabled => style == PaletteBorderStyle.ButtonGallery
                    ? _ribbonColors[(int)SchemeBaseColors.RibbonGalleryBack2]
                    : _buttonBorderColors[0],
                PaletteState.Normal => _ribbonColors[(int)SchemeBaseColors.ButtonNormalBorder],
                PaletteState.NormalDefaultOverride => style is PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ContextMenuItemHighlight
                    ? GlobalStaticValues.EMPTY_COLOR
                    : _ribbonColors[(int)SchemeBaseColors.ButtonNormalDefaultBorder],
                PaletteState.CheckedNormal => _buttonBorderColors[5],
                PaletteState.Tracking => _buttonBorderColors[1],
                PaletteState.Pressed or PaletteState.CheckedPressed => _buttonBorderColors[3],
                PaletteState.CheckedTracking => _buttonBorderColors[3],
                _ => throw DebugTools.NotImplemented(state.ToString())
            },
            PaletteBorderStyle.ButtonInputControl => state switch
            {
                PaletteState.Disabled => _buttonBorderColors[0],
                PaletteState.Normal or PaletteState.CheckedNormal or PaletteState.CheckedTracking => _ribbonColors[(int)SchemeBaseColors.ButtonNormalBorder],
                PaletteState.Tracking => _buttonBorderColors[1],
                PaletteState.Pressed or PaletteState.CheckedPressed => _buttonBorderColors[3],
                _ => throw DebugTools.NotImplemented(state.ToString())
            },
            PaletteBorderStyle.ButtonCalendarDay => state switch
            {
                PaletteState.Disabled => _disabledBack,
                PaletteState.Normal => _ribbonColors[(int)SchemeBaseColors.ButtonNormalBack1],
                PaletteState.NormalDefaultOverride => _ribbonColors[(int)SchemeBaseColors.ButtonNormalDefaultBack1],
                PaletteState.CheckedNormal => _buttonBackColors[6],
                PaletteState.Tracking => _buttonBackColors[2],
                PaletteState.Pressed or PaletteState.CheckedPressed => _buttonBackColors[4],
                PaletteState.CheckedTracking => _buttonBackColors[8],
                _ => throw DebugTools.NotImplemented(state.ToString())
            },
            PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini => _ribbonColors[(int)SchemeBaseColors.ButtonNavigatorBorder],
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
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
            // Check for the calendar day today override
            if (state == PaletteState.TodayOverride)
            {
                if (style == PaletteBorderStyle.ButtonCalendarDay)
                {
                    return state == PaletteState.Disabled ? _disabledBorder : _todayBorder;
                }
            }

            return GlobalStaticValues.EMPTY_COLOR;
        }

        return style switch
        {
            PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 => state switch
            {
                PaletteState.Disabled => style == PaletteBorderStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : _disabledBorder,
                PaletteState.Normal or PaletteState.Tracking or PaletteState.Pressed => style == PaletteBorderStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : _ribbonColors[(int)SchemeBaseColors.ButtonNormalBorder],
                PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => _ribbonColors[(int)SchemeBaseColors.ControlBorder],
                _ => throw DebugTools.NotImplemented(state.ToString())
            },
            PaletteBorderStyle.TabDock => state switch
            {
                PaletteState.Disabled => _disabledBorder,
                PaletteState.Normal => _ribbonColors[(int)SchemeBaseColors.ButtonNormalBorder],
                PaletteState.Tracking or PaletteState.Pressed => _buttonBorderColors[2],
                PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => _ribbonColors[(int)SchemeBaseColors.ControlBorder],
                _ => throw DebugTools.NotImplemented(state.ToString())
            },
            PaletteBorderStyle.TabDockAutoHidden => state switch
            {
                PaletteState.Disabled => _disabledBorder,
                PaletteState.Normal or PaletteState.CheckedNormal => _ribbonColors[(int)SchemeBaseColors.ButtonNormalBorder],
                PaletteState.Tracking or PaletteState.CheckedTracking or PaletteState.Pressed or PaletteState.CheckedPressed => _buttonBorderColors[2],
                _ => throw DebugTools.NotImplemented(state.ToString())
            },
            PaletteBorderStyle.HeaderForm => state == PaletteState.Disabled
                ? _ribbonColors[(int)SchemeBaseColors.FormBorderHeaderInactive]
                : _ribbonColors[(int)SchemeBaseColors.FormBorderHeaderActive],
            PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 => state == PaletteState.Disabled ? _disabledBorder : _ribbonColors[(int)SchemeBaseColors.ControlBorder],
            PaletteBorderStyle.HeaderCalendar => state == PaletteState.Disabled
                ? _ribbonColors[(int)SchemeBaseColors.HeaderPrimaryBack1]
                : _ribbonColors[(int)SchemeBaseColors.HeaderPrimaryBack2],
            PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuItemImageColumn => _contextMenuHeadingBorder,
            PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit => state switch
            {
                PaletteState.Disabled => _buttonBorderColors[0],
                PaletteState.Tracking => _buttonBorderColors[2],
                _ => _contextMenuHeadingBorder
            },
            PaletteBorderStyle.ContextMenuItemImage => _contextMenuImageBorderChecked,
            PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 => state == PaletteState.Disabled
                ? _ribbonColors[(int)SchemeBaseColors.InputControlBorderDisabled]
                : _ribbonColors[(int)SchemeBaseColors.InputControlBorderNormal],
            PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => state == PaletteState.Disabled ? _disabledBorder : _ribbonColors[(int)SchemeBaseColors.GridDataCellBorder],
            PaletteBorderStyle.ControlRibbon => state == PaletteState.Disabled ? _disabledBorder : _ribbonColors[(int)SchemeBaseColors.RibbonGroupsArea1],
            PaletteBorderStyle.ControlRibbonAppMenu => state == PaletteState.Disabled ? _disabledBorder : _ribbonColors[(int)SchemeBaseColors.AppButtonBorder],
            PaletteBorderStyle.ContextMenuOuter => _contextMenuBorder,
            PaletteBorderStyle.ContextMenuInner => _contextMenuBack,
            PaletteBorderStyle.ControlToolTip => state == PaletteState.Disabled ? _disabledBorder : _toolTipBorder,
            PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 => state == PaletteState.Disabled
                ? _ribbonColors[(int)SchemeBaseColors.FormBorderInactive]
                : _ribbonColors[(int)SchemeBaseColors.FormBorderActive],
            PaletteBorderStyle.ButtonForm => state switch
            {
                PaletteState.Disabled or PaletteState.Normal or PaletteState.NormalDefaultOverride => GlobalStaticValues.EMPTY_COLOR,
                PaletteState.CheckedNormal => _ribbonColors[(int)SchemeBaseColors.FormButtonBorderCheck],
                PaletteState.Tracking or PaletteState.CheckedTracking => _ribbonColors[(int)SchemeBaseColors.FormButtonBorderTrack],
                PaletteState.Pressed or PaletteState.CheckedPressed => _ribbonColors[(int)SchemeBaseColors.FormButtonBorderPressed],
                _ => throw DebugTools.NotImplemented(state.ToString())
            },
            PaletteBorderStyle.ButtonFormClose => state switch
            {
                PaletteState.Disabled or PaletteState.Normal or PaletteState.NormalDefaultOverride => GlobalStaticValues.EMPTY_COLOR,
                PaletteState.CheckedNormal => _formCloseBorderCheckedNormal,
                PaletteState.Tracking or PaletteState.CheckedTracking => _formCloseBorderTracking,
                PaletteState.Pressed or PaletteState.CheckedPressed => _formCloseBorderPressed,
                _ => throw DebugTools.NotImplemented(state.ToString())
            },
            PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ContextMenuItemHighlight => state switch
            {
                PaletteState.Disabled => style == PaletteBorderStyle.ButtonGallery
                    ? _ribbonColors[(int)SchemeBaseColors.RibbonGalleryBack2]
                    : _buttonBorderColors[0],
                PaletteState.Normal => _ribbonColors[(int)SchemeBaseColors.ButtonNormalBorder],
                PaletteState.NormalDefaultOverride => _ribbonColors[(int)SchemeBaseColors.ButtonNormalDefaultBorder],
                PaletteState.CheckedNormal => _buttonBorderColors[6],
                PaletteState.Tracking => _buttonBorderColors[2],
                PaletteState.Pressed or PaletteState.CheckedPressed => _buttonBorderColors[4],
                PaletteState.CheckedTracking => _buttonBorderColors[4],
                _ => throw DebugTools.NotImplemented(state.ToString())
            },
            PaletteBorderStyle.ButtonInputControl => state switch
            {
                PaletteState.Disabled => _buttonBorderColors[0],
                PaletteState.Normal or PaletteState.CheckedNormal or PaletteState.CheckedTracking => _ribbonColors[(int)SchemeBaseColors.ButtonNormalBorder],
                PaletteState.Tracking => _buttonBorderColors[2],
                PaletteState.Pressed or PaletteState.CheckedPressed => _buttonBorderColors[4],
                _ => throw DebugTools.NotImplemented(state.ToString())
            },
            PaletteBorderStyle.ButtonCalendarDay => state switch
            {
                PaletteState.Disabled => _disabledBack,
                PaletteState.Normal => _ribbonColors[(int)SchemeBaseColors.ButtonNormalBack1],
                PaletteState.NormalDefaultOverride => _ribbonColors[(int)SchemeBaseColors.ButtonNormalDefaultBack1],
                PaletteState.CheckedNormal => _buttonBackColors[6],
                PaletteState.Tracking => _buttonBackColors[2],
                PaletteState.Pressed or PaletteState.CheckedPressed => _buttonBackColors[4],
                PaletteState.CheckedTracking => _buttonBackColors[8],
                _ => throw DebugTools.NotImplemented(state.ToString())
            },
            PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini => _ribbonColors[(int)SchemeBaseColors.ButtonNavigatorBorder],
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
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
        if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
        {
            return PaletteColorStyle.Inherit;
        }

        return style switch
        {
            PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 => PaletteColorStyle.Sigma,
            PaletteBorderStyle.TabDock => state switch
            {
                PaletteState.Tracking or PaletteState.Pressed => PaletteColorStyle.Solid,
                _ => PaletteColorStyle.Sigma
            },
            PaletteBorderStyle.TabDockAutoHidden => state switch
            {
                PaletteState.Tracking or PaletteState.CheckedTracking or PaletteState.Pressed or PaletteState.CheckedPressed => PaletteColorStyle.Solid,
                _ => PaletteColorStyle.Sigma
            },
            PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.ButtonCalendarDay => PaletteColorStyle.Solid,
            PaletteBorderStyle.ContextMenuItemSplit => state == PaletteState.Tracking ? PaletteColorStyle.Sigma : PaletteColorStyle.Solid,
            PaletteBorderStyle.ContextMenuSeparator => PaletteColorStyle.Dashed,
            PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ContextMenuItemHighlight => state switch
            {
                PaletteState.Normal => PaletteColorStyle.Solid,
                PaletteState.Disabled or PaletteState.NormalDefaultOverride => PaletteColorStyle.Solid,
                _ => PaletteColorStyle.Linear
            },
            PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose => PaletteColorStyle.Solid,
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
            PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 => PaletteRectangleAlign.Control,
            PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit => PaletteRectangleAlign.Local,
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
            PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ContextMenuInner => 0,
            PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => 1,
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
            return GlobalStaticValues.DEFAULT_PRIMARY_CORNER_ROUNDING_VALUE;
        }

        return style switch
        {
            PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini => 0,
            PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ContextMenuItemImage => 1,
            PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuItemHighlight => 0,//Changed 2013
            PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlGroupBox => 3,
            PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 => 0,//Changed 2013
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
            PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => null,
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
            PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => PaletteImageStyle.Tile,
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
            PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => PaletteRectangleAlign.Local,
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
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRelativeAlign.Near,
            PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText => PaletteRelativeAlign.Center,
            PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl => PaletteRelativeAlign.Center,
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
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRelativeAlign.Center,
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
            PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => state == PaletteState.Disabled ? PaletteImageEffect.Disabled : PaletteImageEffect.Normal,
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
            return (state == PaletteState.BoldedOverride) && (style == PaletteContentStyle.ButtonCalendarDay)
                ? CalendarBoldFont
                : null;
        }

        return style switch
        {
            PaletteContentStyle.HeaderForm => HeaderFormFont,
            PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.ButtonCommand => Header1ShortFont,
            PaletteContentStyle.LabelSuperTip or PaletteContentStyle.ContextMenuHeading => SuperToolFont,
            PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemShortcutText => Header2ShortFont,
            PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelBoldPanel => BoldFont,
            PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelItalicControl => ItalicFont,
            PaletteContentStyle.ContextMenuItemTextAlternate => SuperToolFont,
            PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden => TabFontNormal,
            PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 => state switch
            {
                PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => TabFontSelected,
                _ => TabFontNormal
            },
            PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl => ButtonFont,
            PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow => ButtonFontNavigatorStack,
            PaletteContentStyle.ButtonNavigatorMini => ButtonFontNavigatorMini,
            PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 or PaletteContentStyle.HeaderCalendar => GridFont,
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

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteTextHint.ClearTypeGridFit,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
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
            PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.HeaderForm => PaletteTextHotkeyPrefix.Show,
            PaletteContentStyle.ButtonListItem or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemShortcutText => PaletteTextHotkeyPrefix.None,
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
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => InheritBool.True,
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
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteTextTrim.EllipsisCharacter,
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
            PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRelativeAlign.Near,
            PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.HeaderCalendar => PaletteRelativeAlign.Center,
            PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl => PaletteRelativeAlign.Center,
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
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRelativeAlign.Center,
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
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRelativeAlign.Near,
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
            return style switch
            {
                PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl => state switch
                {
                    PaletteState.LinkNotVisitedOverride => _ribbonColors[
                        (int)SchemeBaseColors.LinkNotVisitedOverrideControl],
                    PaletteState.LinkVisitedOverride => _ribbonColors[
                        (int)SchemeBaseColors.LinkVisitedOverrideControl],
                    PaletteState.LinkPressedOverride => _ribbonColors[
                        (int)SchemeBaseColors.LinkPressedOverrideControl],
                    _ => GlobalStaticValues.EMPTY_COLOR
                },
                PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption => state switch
                {
                    PaletteState.LinkNotVisitedOverride => _ribbonColors[
                        (int)SchemeBaseColors.LinkNotVisitedOverridePanel],
                    PaletteState.LinkVisitedOverride => _ribbonColors[
                        (int)SchemeBaseColors.LinkVisitedOverridePanel],
                    PaletteState.LinkPressedOverride => _ribbonColors[
                        (int)SchemeBaseColors.LinkPressedOverridePanel],
                    _ => GlobalStaticValues.EMPTY_COLOR
                },
                _ => GlobalStaticValues.EMPTY_COLOR
            };
        }

        switch (style)
        {
            case PaletteContentStyle.HeaderForm:
                return state == PaletteState.Disabled
                    ? _ribbonColors[(int)SchemeBaseColors.FormHeaderShortInactive]
                    : _ribbonColors[(int)SchemeBaseColors.FormHeaderShortActive];
        }

        if ((state == PaletteState.Disabled) &&
            (style != PaletteContentStyle.LabelToolTip) &&
            (style != PaletteContentStyle.LabelSuperTip) &&
            (style != PaletteContentStyle.LabelKeyTip) &&
            (style != PaletteContentStyle.InputControlStandalone) &&
            (style != PaletteContentStyle.InputControlRibbon) &&
            (style != PaletteContentStyle.InputControlCustom1) &&
            (style != PaletteContentStyle.InputControlCustom2) &&
            (style != PaletteContentStyle.InputControlCustom3) &&
            (style != PaletteContentStyle.ButtonInputControl) &&
            (style != PaletteContentStyle.ButtonCalendarDay))
        {
            return _disabledText;
        }

        return style switch
        {
            PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 or PaletteContentStyle.HeaderCalendar => _gridTextColor,
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 => _ribbonColors[(int)SchemeBaseColors.HeaderText],
            PaletteContentStyle.HeaderDockActive => Color.Black,
            PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 => state == PaletteState.Disabled
                ? _ribbonColors[(int)SchemeBaseColors.InputControlTextDisabled]
                : _ribbonColors[(int)SchemeBaseColors.InputControlTextNormal],
            PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption => _ribbonColors[(int)SchemeBaseColors.TextLabelPanel],
            PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.ContextMenuItemTextAlternate => _ribbonColors[(int)SchemeBaseColors.TextLabelControl],
            // TODO: Add the line below to *all* bases
            PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelAlternateControl => _ribbonColors[(int)SchemeBaseColors.StatusStripText],
            PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip => _toolTipText,
            PaletteContentStyle.ContextMenuHeading => _ribbonColors[(int)SchemeBaseColors.ContextMenuHeadingText],
            PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 => state != PaletteState.Normal
                ? _ribbonColors[(int)SchemeBaseColors.TextButtonChecked]
                : _ribbonColors[(int)SchemeBaseColors.TextButtonNormal],
            PaletteContentStyle.TabDockAutoHidden => _ribbonColors[(int)SchemeBaseColors.TextButtonNormal],
            PaletteContentStyle.ButtonCalendarDay => state == PaletteState.Disabled ? _disabledText2 : Color.Black,
            PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonButtonSpec => state switch
            {
                PaletteState.Normal => style == PaletteContentStyle.ButtonListItem
                    ? _ribbonColors[(int)SchemeBaseColors.TextLabelControl]
                    : _ribbonColors[(int)SchemeBaseColors.TextLabelPanel],
                PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed => _ribbonColors[(int)SchemeBaseColors.TextButtonChecked],
                _ => _ribbonColors[(int)SchemeBaseColors.TextButtonNormal]
            },
            PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose => state switch
            {
                PaletteState.Tracking or PaletteState.CheckedTracking => _ribbonColors[(int)SchemeBaseColors.TextButtonFormTracking],
                PaletteState.Pressed or PaletteState.CheckedPressed or PaletteState.CheckedNormal => _ribbonColors[(int)SchemeBaseColors.TextButtonFormPressed],
                _ => _ribbonColors[(int)SchemeBaseColors.TextButtonFormNormal]
            },
            PaletteContentStyle.ButtonInputControl => state != PaletteState.Disabled
                ? _ribbonColors[(int)SchemeBaseColors.InputDropDownNormal1]
                : _ribbonColors[(int)SchemeBaseColors.InputDropDownDisabled1],
            PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow => state != PaletteState.Normal
                ? _ribbonColors[(int)SchemeBaseColors.ButtonNavigatorText]
                : _ribbonColors[(int)SchemeBaseColors.TextButtonNormal],
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
                return state == PaletteState.Disabled
                    ? _ribbonColors[(int)SchemeBaseColors.FormHeaderShortInactive]
                    : _ribbonColors[(int)SchemeBaseColors.FormHeaderShortActive];
        }

        if ((state == PaletteState.Disabled) &&
            (style != PaletteContentStyle.LabelToolTip) &&
            (style != PaletteContentStyle.LabelSuperTip) &&
            (style != PaletteContentStyle.LabelKeyTip) &&
            (style != PaletteContentStyle.InputControlStandalone) &&
            (style != PaletteContentStyle.InputControlRibbon) &&
            (style != PaletteContentStyle.InputControlCustom1) &&
            (style != PaletteContentStyle.InputControlCustom2) &&
            (style != PaletteContentStyle.InputControlCustom3) &&
            (style != PaletteContentStyle.ButtonInputControl) &&
            (style != PaletteContentStyle.ButtonCalendarDay))
        {
            return _disabledText;
        }

        return style switch
        {
            PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 or PaletteContentStyle.HeaderCalendar => _gridTextColor,
            PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 => _ribbonColors[(int)SchemeBaseColors.HeaderText],
            PaletteContentStyle.HeaderDockActive => Color.Black,
            PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 => state == PaletteState.Disabled
                ? _ribbonColors[(int)SchemeBaseColors.InputControlTextDisabled]
                : _ribbonColors[(int)SchemeBaseColors.InputControlTextNormal],
            PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption => _ribbonColors[(int)SchemeBaseColors.TextLabelPanel],
            PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText => _ribbonColors[(int)SchemeBaseColors.TextLabelControl],
            PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip => _toolTipText,
            PaletteContentStyle.ContextMenuHeading => _ribbonColors[(int)SchemeBaseColors.ContextMenuHeadingText],
            PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 => state != PaletteState.Normal
                ? _ribbonColors[(int)SchemeBaseColors.TextButtonChecked]
                : _ribbonColors[(int)SchemeBaseColors.TextButtonNormal],
            PaletteContentStyle.TabDockAutoHidden => _ribbonColors[(int)SchemeBaseColors.TextButtonNormal],
            PaletteContentStyle.ButtonCalendarDay => state == PaletteState.Disabled ? _disabledText2 : Color.Black,
            PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonButtonSpec => state switch
            {
                PaletteState.Normal => style == PaletteContentStyle.ButtonListItem
                    ? _ribbonColors[(int)SchemeBaseColors.TextLabelControl]
                    : _ribbonColors[(int)SchemeBaseColors.TextLabelPanel],
                PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed => _ribbonColors[(int)SchemeBaseColors.TextButtonChecked],
                _ => _ribbonColors[(int)SchemeBaseColors.TextButtonNormal]
            },
            PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose => state switch
            {
                PaletteState.Tracking or PaletteState.CheckedTracking => _ribbonColors[(int)SchemeBaseColors.TextButtonFormTracking],
                PaletteState.Pressed or PaletteState.CheckedPressed => _ribbonColors[(int)SchemeBaseColors.TextButtonFormPressed],
                _ => _ribbonColors[(int)SchemeBaseColors.TextButtonFormNormal]
            },
            PaletteContentStyle.ButtonInputControl => state != PaletteState.Disabled
                ? _ribbonColors[(int)SchemeBaseColors.InputDropDownNormal2]
                : _ribbonColors[(int)SchemeBaseColors.InputDropDownDisabled2],
            PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow => state != PaletteState.Normal
                ? _ribbonColors[(int)SchemeBaseColors.ButtonNavigatorText]
                : _ribbonColors[(int)SchemeBaseColors.TextButtonNormal],
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
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteColorStyle.Solid,
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
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRectangleAlign.Local,
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
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => 90f,
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
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => null,
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
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteImageStyle.TileFlipXY,
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
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRectangleAlign.Local,
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
            PaletteContentStyle.ButtonCalendarDay => CalendarFont,
            PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 or PaletteContentStyle.HeaderCalendar => GridFont,
            PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 => Header1LongFont,
            PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.HeaderSecondary => Header2LongFont,
            PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden => TabFontNormal,
            PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 => state switch
            {
                PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => TabFontSelected,
                _ => TabFontNormal
            },
            PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl => ButtonFont,
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

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteTextHint.ClearTypeGridFit,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
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
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => InheritBool.True,
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
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteTextTrim.EllipsisCharacter,
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
            PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl => PaletteTextHotkeyPrefix.Show,
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
            PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl => PaletteRelativeAlign.Center,
            PaletteContentStyle.ButtonCalendarDay => PaletteRelativeAlign.Far,
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
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRelativeAlign.Center,
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
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRelativeAlign.Center,
            PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ButtonCommand => PaletteRelativeAlign.Near,
            PaletteContentStyle.ContextMenuItemShortcutText => PaletteRelativeAlign.Far,
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
                return state == PaletteState.Disabled
                    ? _ribbonColors[(int)SchemeBaseColors.FormHeaderLongInactive]
                    : _ribbonColors[(int)SchemeBaseColors.FormHeaderLongActive];
        }

        if ((state == PaletteState.Disabled) &&
            (style != PaletteContentStyle.LabelToolTip) &&
            (style != PaletteContentStyle.LabelSuperTip) &&
            (style != PaletteContentStyle.LabelKeyTip) &&
            (style != PaletteContentStyle.InputControlStandalone) &&
            (style != PaletteContentStyle.InputControlRibbon) &&
            (style != PaletteContentStyle.InputControlCustom1) &&
            (style != PaletteContentStyle.InputControlCustom2) &&
            (style != PaletteContentStyle.InputControlCustom3) &&
            (style != PaletteContentStyle.ButtonInputControl))
        {
            return _disabledText;
        }

        return style switch
        {
            PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 or PaletteContentStyle.HeaderCalendar => _gridTextColor,
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 => _ribbonColors[(int)SchemeBaseColors.HeaderText],
            PaletteContentStyle.HeaderDockActive => Color.Black,
            PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 => state == PaletteState.Disabled
                ? _ribbonColors[(int)SchemeBaseColors.InputControlTextDisabled]
                : _ribbonColors[(int)SchemeBaseColors.InputControlTextNormal],
            PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption => _ribbonColors[(int)SchemeBaseColors.TextLabelPanel],
            PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.ContextMenuItemTextAlternate => _ribbonColors[(int)SchemeBaseColors.TextLabelControl],
            PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip => _toolTipText,
            PaletteContentStyle.ContextMenuHeading => _ribbonColors[(int)SchemeBaseColors.ContextMenuHeadingText],
            PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 => state != PaletteState.Normal
                ? _ribbonColors[(int)SchemeBaseColors.TextButtonChecked]
                : _ribbonColors[(int)SchemeBaseColors.TextButtonNormal],
            PaletteContentStyle.TabDockAutoHidden => _ribbonColors[(int)SchemeBaseColors.TextButtonNormal],
            PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay => state switch
            {
                PaletteState.Normal => style == PaletteContentStyle.ButtonListItem
                    ? _ribbonColors[(int)SchemeBaseColors.TextLabelControl]
                    : _ribbonColors[(int)SchemeBaseColors.TextLabelPanel],
                PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed => _ribbonColors[(int)SchemeBaseColors.TextButtonChecked],
                _ => _ribbonColors[(int)SchemeBaseColors.TextButtonNormal]
            },
            PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose => state switch
            {
                PaletteState.Tracking or PaletteState.CheckedTracking => _ribbonColors[(int)SchemeBaseColors.TextButtonFormTracking],
                PaletteState.Pressed or PaletteState.CheckedPressed => _ribbonColors[(int)SchemeBaseColors.TextButtonFormPressed],
                _ => _ribbonColors[(int)SchemeBaseColors.TextButtonFormNormal]
            },
            PaletteContentStyle.ButtonInputControl => state != PaletteState.Disabled
                ? _ribbonColors[(int)SchemeBaseColors.InputDropDownNormal1]
                : _ribbonColors[(int)SchemeBaseColors.InputDropDownDisabled1],
            PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow => state != PaletteState.Normal
                ? _ribbonColors[(int)SchemeBaseColors.ButtonNavigatorText]
                : _ribbonColors[(int)SchemeBaseColors.TextButtonNormal],
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
                return state == PaletteState.Disabled
                    ? _ribbonColors[(int)SchemeBaseColors.FormHeaderLongInactive]
                    : _ribbonColors[(int)SchemeBaseColors.FormHeaderLongActive];
        }

        if ((state == PaletteState.Disabled) &&
            (style != PaletteContentStyle.LabelToolTip) &&
            (style != PaletteContentStyle.LabelSuperTip) &&
            (style != PaletteContentStyle.LabelKeyTip) &&
            (style != PaletteContentStyle.InputControlStandalone) &&
            (style != PaletteContentStyle.InputControlRibbon) &&
            (style != PaletteContentStyle.InputControlCustom1) &&
            (style != PaletteContentStyle.InputControlCustom2) &&
            (style != PaletteContentStyle.InputControlCustom3) &&
            (style != PaletteContentStyle.ButtonInputControl))
        {
            return _disabledText;
        }

        return style switch
        {
            PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 or PaletteContentStyle.HeaderCalendar => _gridTextColor,
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 => _ribbonColors[(int)SchemeBaseColors.HeaderText],
            PaletteContentStyle.HeaderDockActive => Color.Black,
            PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 => state == PaletteState.Disabled
                ? _ribbonColors[(int)SchemeBaseColors.InputControlTextDisabled]
                : _ribbonColors[(int)SchemeBaseColors.InputControlTextNormal],
            PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption => _ribbonColors[(int)SchemeBaseColors.TextLabelPanel],
            PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText => _ribbonColors[(int)SchemeBaseColors.TextLabelControl],
            PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip => _toolTipText,
            PaletteContentStyle.ContextMenuHeading => _ribbonColors[(int)SchemeBaseColors.ContextMenuHeadingText],
            PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 => state != PaletteState.Normal
                ? _ribbonColors[(int)SchemeBaseColors.TextButtonChecked]
                : _ribbonColors[(int)SchemeBaseColors.TextButtonNormal],
            PaletteContentStyle.TabDockAutoHidden => _ribbonColors[(int)SchemeBaseColors.TextButtonNormal],
            PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay => state switch
            {
                PaletteState.Normal => style == PaletteContentStyle.ButtonListItem
                    ? _ribbonColors[(int)SchemeBaseColors.TextLabelControl]
                    : _ribbonColors[(int)SchemeBaseColors.TextLabelPanel],
                PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed => _ribbonColors[(int)SchemeBaseColors.TextButtonChecked],
                _ => _ribbonColors[(int)SchemeBaseColors.TextButtonNormal]
            },
            PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose => state switch
            {
                PaletteState.Tracking or PaletteState.CheckedTracking => _ribbonColors[(int)SchemeBaseColors.TextButtonFormTracking],
                PaletteState.Pressed or PaletteState.CheckedPressed => _ribbonColors[(int)SchemeBaseColors.TextButtonFormPressed],
                _ => _ribbonColors[(int)SchemeBaseColors.TextButtonFormNormal]
            },
            PaletteContentStyle.ButtonInputControl => state != PaletteState.Disabled
                ? _ribbonColors[(int)SchemeBaseColors.InputDropDownNormal2]
                : _ribbonColors[(int)SchemeBaseColors.InputDropDownDisabled2],
            PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow => state != PaletteState.Normal
                ? _ribbonColors[(int)SchemeBaseColors.ButtonNavigatorText]
                : _ribbonColors[(int)SchemeBaseColors.TextButtonNormal],
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
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteColorStyle.Solid,
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
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRectangleAlign.Local,
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
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => 90f,
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
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => null,
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
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteImageStyle.TileFlipXY,
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
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRectangleAlign.Local,
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
            case PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive:
                return _contentPaddingDock;
            case PaletteContentStyle.HeaderSecondary:
                return _contentPaddingHeader2;
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
            case PaletteContentStyle.ContextMenuItemImage:
                return _contentPaddingContextMenuImage;
            case PaletteContentStyle.LabelToolTip:
                return _contentPaddingToolTip;
            case PaletteContentStyle.LabelSuperTip:
                return _contentPaddingSuperTip;
            case PaletteContentStyle.LabelKeyTip:
                return _contentPaddingKeyTip;
            case PaletteContentStyle.ContextMenuHeading:
                return _contentPaddingContextMenuHeading;
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
            case PaletteContentStyle.ButtonInputControl or PaletteContentStyle.ButtonCalendarDay:
                return _contentPaddingButtonInputControl;
            case PaletteContentStyle.ButtonButtonSpec:
                return _contentPaddingButton3;
            case PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow:
                return _contentPaddingButton4;
            case PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose:
                return _contentPaddingButtonForm;
            case PaletteContentStyle.ButtonGallery:
                return _contentPaddingButtonGallery;
            case PaletteContentStyle.ButtonListItem:
                return _contentPaddingButtonListItem;
            case PaletteContentStyle.ButtonBreadCrumb:
                return _contentPaddingButton6;
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
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => 1,
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
            case PaletteMetricBool.RibbonTabsSpareCaption:
                return InheritBool.True;
            case PaletteMetricBool.TreeViewLines:
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
            case PaletteMetricPadding.HeaderGroupPaddingPrimary:
            case PaletteMetricPadding.HeaderGroupPaddingSecondary:
            case PaletteMetricPadding.HeaderGroupPaddingDockInactive:
            case PaletteMetricPadding.HeaderGroupPaddingDockActive:
            case PaletteMetricPadding.SeparatorPaddingLowProfile:
            case PaletteMetricPadding.SeparatorPaddingHighInternalProfile:
            case PaletteMetricPadding.SeparatorPaddingHighProfile:
            case PaletteMetricPadding.SeparatorPaddingCustom1:
            case PaletteMetricPadding.SeparatorPaddingCustom2:
            case PaletteMetricPadding.SeparatorPaddingCustom3:
            case PaletteMetricPadding.ContextMenuItemHighlight:
            case PaletteMetricPadding.ContextMenuItemsCollection:
            case PaletteMetricPadding.ContextMenuItemOuter:
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
    public override Image? GetTreeViewImage(bool expanded) => expanded ? _treeCollapseBlack : _treeExpandWhite;

    /// <summary>
    /// Gets a check box image appropriate for the provided state.
    /// </summary>
    /// <param name="enabled">Is the check box enabled.</param>
    /// <param name="checkState">Is the check box checked/unchecked/indeterminate.</param>
    /// <param name="tracking">Is the check box being hot tracked.</param>
    /// <param name="pressed">Is the check box being pressed.</param>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public override Image? GetCheckBoxImage(bool enabled, CheckState checkState, bool tracking, bool pressed)
    {
        switch (checkState)
        {
            default:
            case CheckState.Unchecked:
                if (!enabled)
                {
                    return _checkBoxList.Images[0];
                }
                else if (pressed)
                {
                    return _checkBoxList.Images[3];
                }
                else
                {
                    return tracking ? _checkBoxList.Images[2] : _checkBoxList.Images[1];
                }

            case CheckState.Checked:
                if (!enabled)
                {
                    return _checkBoxList.Images[4];
                }
                else if (pressed)
                {
                    return _checkBoxList.Images[7];
                }
                else
                {
                    return tracking ? _checkBoxList.Images[6] : _checkBoxList.Images[5];
                }

            case CheckState.Indeterminate:
                if (!enabled)
                {
                    return _checkBoxList.Images[8];
                }
                else if (pressed)
                {
                    return _checkBoxList.Images[11];
                }
                else
                {
                    return tracking ? _checkBoxList.Images[10] : _checkBoxList.Images[9];
                }
        }
    }

    /// <summary>
    /// Gets a check box image appropriate for the provided state.
    /// </summary>
    /// <param name="enabled">Is the radio button enabled.</param>
    /// <param name="checkState">Is the radio button checked.</param>
    /// <param name="tracking">Is the radio button being hot tracked.</param>
    /// <param name="pressed">Is the radio button being pressed.</param>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public override Image? GetRadioButtonImage(bool enabled, bool checkState, bool tracking, bool pressed)
    {
        if (!checkState)
        {
            if (!enabled)
            {
                return _radioButtonArray[0];
            }
            else if (pressed)
            {
                return _radioButtonArray[3];
            }
            else
            {
                return tracking ? _radioButtonArray[2] : _radioButtonArray[1];
            }
        }
        else
        {
            if (!enabled)
            {
                return _radioButtonArray[4];
            }
            else if (pressed)
            {
                return _radioButtonArray[7];
            }
            else
            {
                return tracking ? _radioButtonArray[6] : _radioButtonArray[5];
            }
        }
    }

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
    /// Gets a check box image appropriate for the provided state.
    /// </summary>
    /// <param name="button">Enum of the button to fetch.</param>
    /// <param name="state">State of the button to fetch.</param>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public override Image? GetGalleryButtonImage(PaletteRibbonGalleryButton button, PaletteState state) => button switch
    {
        PaletteRibbonGalleryButton.Up => _galleryButtonList.Images[1],
        PaletteRibbonGalleryButton.DropDown => _galleryButtonList.Images[2],
        _ => _galleryButtonList.Images[0]
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
            case PaletteButtonSpecStyle.PendantClose:
                return _buttonSpecPendantClose;
            case PaletteButtonSpecStyle.PendantMin:
                return _buttonSpecPendantMin;
            case PaletteButtonSpecStyle.PendantRestore:
                return _buttonSpecPendantRestore;
            case PaletteButtonSpecStyle.WorkspaceMaximize:
                return _buttonSpecWorkspaceMaximize;
            case PaletteButtonSpecStyle.WorkspaceRestore:
                return _buttonSpecWorkspaceRestore;
            case PaletteButtonSpecStyle.RibbonMinimize:
                return _buttonSpecRibbonMinimize;
            case PaletteButtonSpecStyle.RibbonExpand:
                return _buttonSpecRibbonExpand;
            case PaletteButtonSpecStyle.New:
                return _integratedToolbarNewNormal;
            case PaletteButtonSpecStyle.Open:
                return _integratedToolbarOpenNormal;
            case PaletteButtonSpecStyle.Save:
                return _integratedToolbarSaveNormal;
            case PaletteButtonSpecStyle.SaveAs:
                return _integratedToolbarSaveAsNormal;
            case PaletteButtonSpecStyle.SaveAll:
                return _integratedToolbarSaveAllNormal;
            case PaletteButtonSpecStyle.Cut:
                return _integratedToolbarCutNormal;
            case PaletteButtonSpecStyle.Copy:
                return _integratedToolbarCopyNormal;
            case PaletteButtonSpecStyle.Paste:
                return _integratedToolbarPasteNormal;
            case PaletteButtonSpecStyle.Undo:
                return _integratedToolbarUndoNormal;
            case PaletteButtonSpecStyle.Redo:
                return _integratedToolbarRedoNormal;
            case PaletteButtonSpecStyle.PageSetup:
                return _integratedToolbarPageSetupNormal;
            case PaletteButtonSpecStyle.PrintPreview:
                return _integratedToolbarPrintPreviewNormal;
            case PaletteButtonSpecStyle.Print:
                return _integratedToolbarPrintNormal;
            case PaletteButtonSpecStyle.QuickPrint:
                return _integratedToolbarQuickPrintNormal;
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
    public override PaletteRibbonShape GetRibbonShape() => PaletteRibbonShape.VisualStudio2010;

    /// <summary>
    /// Gets the text alignment for the ribbon context text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override PaletteRelativeAlign GetRibbonContextTextAlign(PaletteState state) => PaletteRelativeAlign.Center;

    /// <summary>
    /// Gets the font for the ribbon context text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font GetRibbonContextTextFont(PaletteState state) => RibbonTabContextFont!;

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
    public override Color GetRibbonDropArrowLight(PaletteState state) => _ribbonColors[(int)SchemeBaseColors.RibbonDropArrowLight];

    /// <summary>
    /// Gets the color for the drop arrow dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonDropArrowDark(PaletteState state) => _ribbonColors[(int)SchemeBaseColors.RibbonDropArrowDark];

    /// <summary>
    /// Gets the color for the dialog launcher dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonGroupDialogDark(PaletteState state) => _ribbonColors[(int)SchemeBaseColors.RibbonGroupDialogDark];

    /// <summary>
    /// Gets the color for the dialog launcher light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonGroupDialogLight(PaletteState state) => _ribbonColors[(int)SchemeBaseColors.RibbonGroupDialogLight];

    /// <summary>
    /// Gets the color for the group separator dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonGroupSeparatorDark(PaletteState state) => _ribbonColors[(int)SchemeBaseColors.RibbonGroupSeparatorDark];

    /// <summary>
    /// Gets the color for the group separator light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonGroupSeparatorLight(PaletteState state) => _ribbonColors[(int)SchemeBaseColors.RibbonGroupSeparatorLight];

    /// <summary>
    /// Gets the color for the minimize bar dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonMinimizeBarDark(PaletteState state) => _ribbonColors[(int)SchemeBaseColors.RibbonMinimizeBarDark];

    /// <summary>
    /// Gets the color for the minimize bar light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonMinimizeBarLight(PaletteState state) => _ribbonColors[(int)SchemeBaseColors.RibbonMinimizeBarLight];

    /// <summary>
    /// Gets the color for the tab separator.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonTabSeparatorColor(PaletteState state) => _ribbonColors[(int)SchemeBaseColors.RibbonTabSeparatorColor];

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
    public override Font GetRibbonTextFont(PaletteState state) => RibbonTabFont!;

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
    public override Color GetRibbonQATButtonDark(PaletteState state) => _ribbonColors[(int)SchemeBaseColors.RibbonQATButtonDark];

    /// <summary>
    /// Gets the color for the extra QAT button light content color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonQATButtonLight(PaletteState state) => _ribbonColors[(int)SchemeBaseColors.RibbonQATButtonLight];

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
                    PaletteState.Disabled => _disabledBack,
                    PaletteState.Tracking => _ribbonColors[(int)SchemeBaseColors.RibbonGalleryBackTracking],
                    _ => _ribbonColors[(int)SchemeBaseColors.RibbonGalleryBackNormal]
                };
            case PaletteRibbonBackStyle.RibbonGalleryBorder:
                return state switch
                {
                    PaletteState.Disabled => _disabledBorder,
                    _ => _ribbonColors[(int)SchemeBaseColors.RibbonGalleryBorder]
                };
            case PaletteRibbonBackStyle.RibbonAppMenuDocs:
                return _ribbonColors[(int)SchemeBaseColors.AppButtonMenuDocsBack];
            case PaletteRibbonBackStyle.RibbonAppMenuInner:
                return _ribbonColors[(int)SchemeBaseColors.AppButtonInner1];
            case PaletteRibbonBackStyle.RibbonAppMenuOuter:
                return _ribbonColors[(int)SchemeBaseColors.AppButtonOuter1];
            case PaletteRibbonBackStyle.RibbonQATMinibar:
                return state == PaletteState.Normal
                    ? _ribbonColors[(int)SchemeBaseColors.RibbonQATMini1]
                    : _ribbonColors[(int)SchemeBaseColors.RibbonQATMini1I];

            case PaletteRibbonBackStyle.RibbonQATFullbar:
                return _ribbonColors[(int)SchemeBaseColors.RibbonQATFullbar1];
            case PaletteRibbonBackStyle.RibbonQATOverflow:
                return _ribbonColors[(int)SchemeBaseColors.RibbonQATOverflow1];
            case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
                return _ribbonColors[(int)SchemeBaseColors.RibbonGroupFrameBorder1];
            case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                return _ribbonColors[(int)SchemeBaseColors.RibbonGroupFrameInside1];
            case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
                return GlobalStaticValues.EMPTY_COLOR;
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
                        return _ribbonColors[(int)SchemeBaseColors.RibbonGroupBorder1];
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        DebugTools.NotImplemented(state.ToString());
                        break;
                }
                break;
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
                return state == PaletteState.ContextCheckedNormal ? GlobalStaticValues.EMPTY_COLOR : _ribbonColors[(int)SchemeBaseColors.RibbonGroupsArea1];

            case PaletteRibbonBackStyle.RibbonTab:
                switch (state)
                {
                    case PaletteState.Tracking:
                    case PaletteState.Pressed:
                    case PaletteState.ContextTracking:
                        return _ribbonColors[(int)SchemeBaseColors.RibbonTabTracking1];
                    case PaletteState.CheckedNormal:
                    case PaletteState.CheckedTracking:
                    case PaletteState.CheckedPressed:
                    case PaletteState.ContextCheckedNormal:
                    case PaletteState.ContextCheckedTracking:
                        return _ribbonColors[(int)SchemeBaseColors.RibbonTabSelected1];
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
                return _ribbonColors[(int)SchemeBaseColors.AppButtonInner2];
            case PaletteRibbonBackStyle.RibbonAppMenuOuter:
                return _ribbonColors[(int)SchemeBaseColors.AppButtonOuter2];
            case PaletteRibbonBackStyle.RibbonQATMinibar:
                return state == PaletteState.Normal
                    ? _ribbonColors[(int)SchemeBaseColors.RibbonQATMini2]
                    : _ribbonColors[(int)SchemeBaseColors.RibbonQATMini2I];

            case PaletteRibbonBackStyle.RibbonQATFullbar:
                return _ribbonColors[(int)SchemeBaseColors.RibbonQATFullbar2];
            case PaletteRibbonBackStyle.RibbonQATOverflow:
                return _ribbonColors[(int)SchemeBaseColors.RibbonQATOverflow2];
            case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
                return _ribbonColors[(int)SchemeBaseColors.RibbonGroupFrameBorder2];
            case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                return _ribbonColors[(int)SchemeBaseColors.RibbonGroupFrameInside2];
            case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
                switch (state)
                {
                    case PaletteState.Normal:
                        return _ribbonColors[(int)SchemeBaseColors.RibbonGroupTitle2];
                    case PaletteState.ContextNormal:
                        return _ribbonColors[(int)SchemeBaseColors.RibbonGroupTitleContext2];
                    case PaletteState.Tracking:
                    case PaletteState.ContextTracking:
                        return _ribbonColors[(int)SchemeBaseColors.RibbonGroupTitleTracking2];
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        DebugTools.NotImplemented(state.ToString());
                        break;
                }
                break;
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
                        return _ribbonColors[(int)SchemeBaseColors.RibbonGroupBorder2];
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        DebugTools.NotImplemented(state.ToString());
                        break;
                }
                break;
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
                return _ribbonColors[(int)SchemeBaseColors.RibbonGroupsArea2];
            case PaletteRibbonBackStyle.RibbonTab:
                switch (state)
                {
                    case PaletteState.Tracking:
                    case PaletteState.Pressed:
                    case PaletteState.ContextTracking:
                        return _ribbonColors[(int)SchemeBaseColors.RibbonTabTracking2];
                    case PaletteState.CheckedNormal:
                    case PaletteState.CheckedTracking:
                    case PaletteState.CheckedPressed:
                    case PaletteState.ContextCheckedTracking:
                    case PaletteState.ContextCheckedNormal:
                        return _ribbonColors[(int)SchemeBaseColors.RibbonTabSelected2];
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
            case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
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
                return _ribbonColors[(int)SchemeBaseColors.AppButtonOuter3];
            case PaletteRibbonBackStyle.RibbonQATMinibar:
                return state == PaletteState.Normal
                    ? _ribbonColors[(int)SchemeBaseColors.RibbonQATMini3]
                    : _ribbonColors[(int)SchemeBaseColors.RibbonQATMini3I];

            case PaletteRibbonBackStyle.RibbonQATFullbar:
                return _ribbonColors[(int)SchemeBaseColors.RibbonQATFullbar3];
            case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                return _ribbonColors[(int)SchemeBaseColors.RibbonGroupBorder3];
            case PaletteRibbonBackStyle.RibbonAppMenuDocs:
            case PaletteRibbonBackStyle.RibbonAppMenuInner:
            case PaletteRibbonBackStyle.RibbonQATOverflow:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
            case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
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
                return _ribbonColors[(int)SchemeBaseColors.RibbonGroupsArea3];
            case PaletteRibbonBackStyle.RibbonTab:
                switch (state)
                {
                    case PaletteState.Tracking:
                    case PaletteState.Pressed:
                    case PaletteState.ContextTracking:
                        return _ribbonColors[(int)SchemeBaseColors.RibbonTabTracking3];
                    case PaletteState.CheckedNormal:
                    case PaletteState.CheckedTracking:
                    case PaletteState.CheckedPressed:
                    case PaletteState.ContextCheckedNormal:
                    case PaletteState.ContextCheckedTracking:
                        return _ribbonColors[(int)SchemeBaseColors.RibbonTabSelected3];
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
                    ? _ribbonColors[(int)SchemeBaseColors.RibbonQATMini4]
                    : _ribbonColors[(int)SchemeBaseColors.RibbonQATMini4I];

            case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                return _ribbonColors[(int)SchemeBaseColors.RibbonGroupBorder4];
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
                return _ribbonColors[(int)SchemeBaseColors.RibbonGroupsArea4];
            case PaletteRibbonBackStyle.RibbonTab:
                switch (state)
                {
                    case PaletteState.Tracking:
                    case PaletteState.Pressed:
                    case PaletteState.ContextTracking:
                        return _ribbonColors[(int)SchemeBaseColors.RibbonTabTracking4];
                    case PaletteState.CheckedNormal:
                    case PaletteState.CheckedTracking:
                    case PaletteState.CheckedPressed:
                    case PaletteState.ContextCheckedNormal:
                    case PaletteState.ContextCheckedTracking:
                        return _ribbonColors[(int)SchemeBaseColors.RibbonTabSelected4];
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
            case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                return _ribbonColors[(int)SchemeBaseColors.RibbonGroupBorder5];
            case PaletteRibbonBackStyle.RibbonQATMinibar:
                return state == PaletteState.Normal
                    ? _ribbonColors[(int)SchemeBaseColors.RibbonQATMini5]
                    : _ribbonColors[(int)SchemeBaseColors.RibbonQATMini5I];

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
                return _ribbonColors[(int)SchemeBaseColors.RibbonGroupsArea5];
            case PaletteRibbonBackStyle.RibbonTab:
                switch (state)
                {
                    case PaletteState.Disabled:
                        return _disabledText;
                    case PaletteState.Pressed:
                        return _ribbonColors[(int)SchemeBaseColors.RibbonTabTracking2];
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
            case PaletteRibbonTextStyle.RibbonAppMenuDocsTitle:
            case PaletteRibbonTextStyle.RibbonAppMenuDocsEntry:
                return _ribbonColors[(int)SchemeBaseColors.AppButtonMenuDocsText];
            case PaletteRibbonTextStyle.RibbonGroupNormalTitle:
                return state switch
                {
                    PaletteState.Disabled => _disabledText,
                    _ => _ribbonColors[(int)SchemeBaseColors.RibbonGroupTitleText]
                };
            case PaletteRibbonTextStyle.RibbonTab:
                return state switch
                {
                    PaletteState.Disabled => _disabledText,
                    PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking or PaletteState.ContextCheckedNormal or PaletteState.ContextCheckedTracking or PaletteState.FocusOverride => _ribbonColors[(int)SchemeBaseColors.RibbonTabTextChecked],
                    _ => _ribbonColors[(int)SchemeBaseColors.RibbonTabTextNormal]
                };
            case PaletteRibbonTextStyle.RibbonGroupCollapsedText:
                return _ribbonColors[(int)SchemeBaseColors.RibbonGroupCollapsedText];
            case PaletteRibbonTextStyle.RibbonGroupButtonText:
            case PaletteRibbonTextStyle.RibbonGroupLabelText:
            case PaletteRibbonTextStyle.RibbonGroupCheckBoxText:
            case PaletteRibbonTextStyle.RibbonGroupRadioButtonText:
                return state == PaletteState.Disabled ? _disabledText : _ribbonColors[(int)SchemeBaseColors.RibbonGroupCollapsedText];

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
                return _ribbonColors![(int)SchemeBaseColors.TrackBarTickMarks];
            case PaletteElement.TrackBarTrack:
                return _ribbonColors![(int)SchemeBaseColors.TrackBarTopTrack];
            case PaletteElement.TrackBarPosition:
                return state switch
                {
                    PaletteState.Disabled => GlobalStaticValues.EMPTY_COLOR,
                    _ => BaseColors!.TrackBarOutsidePosition
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
                return _ribbonColors![(int)SchemeBaseColors.TrackBarTickMarks];
            case PaletteElement.TrackBarTrack:
                return _ribbonColors![(int)SchemeBaseColors.TrackBarBottomTrack];
            case PaletteElement.TrackBarPosition:
                return state switch
                {
                    PaletteState.Disabled => ControlPaint.Light(_ribbonColors[(int)SchemeBaseColors.ButtonNormalBorder]),
                    PaletteState.Normal => _ribbonColors[(int)SchemeBaseColors.ButtonNormalBorder],
                    PaletteState.Tracking or PaletteState.FocusOverride => _buttonBorderColors[1],
                    PaletteState.Pressed => _buttonBorderColors[3],
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
                return _ribbonColors![(int)SchemeBaseColors.TrackBarTickMarks];
            case PaletteElement.TrackBarTrack:
                return _ribbonColors![(int)SchemeBaseColors.TrackBarFillTrack];
            case PaletteElement.TrackBarPosition:
                return state switch
                {
                    PaletteState.Disabled => ControlPaint.LightLight(
                        _ribbonColors[(int)SchemeBaseColors.ButtonNormalBack1]),
                    PaletteState.Normal => ControlPaint.Light(
                        _ribbonColors[(int)SchemeBaseColors.ButtonNormalBack1]),
                    PaletteState.Tracking => ControlPaint.Light(_buttonBackColors[2]),
                    PaletteState.Pressed => ControlPaint.Light(_buttonBackColors[4]),
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

                return _ribbonColors![(int)SchemeBaseColors.TrackBarTickMarks];
            case PaletteElement.TrackBarTrack:
                if (CommonHelper.IsOverrideState(state))
                {
                    return GlobalStaticValues.EMPTY_COLOR;
                }

                return _ribbonColors![(int)SchemeBaseColors.TrackBarFillTrack];
            case PaletteElement.TrackBarPosition:
                if (CommonHelper.IsOverrideStateExclude(state, PaletteState.FocusOverride))
                {
                    return GlobalStaticValues.EMPTY_COLOR;
                }

                return state switch
                {
                    PaletteState.Disabled => ControlPaint.LightLight(_ribbonColors[(int)SchemeBaseColors.ButtonNormalBack1]),
                    PaletteState.Normal => _ribbonColors[(int)SchemeBaseColors.ButtonNormalBack1],
                    PaletteState.Tracking or PaletteState.FocusOverride => _buttonBackColors[2],
                    PaletteState.Pressed => _buttonBackColors[4],
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

                return _ribbonColors![(int)SchemeBaseColors.TrackBarTickMarks];
            case PaletteElement.TrackBarTrack:
                if (CommonHelper.IsOverrideState(state))
                {
                    return GlobalStaticValues.EMPTY_COLOR;
                }

                return _ribbonColors![(int)SchemeBaseColors.TrackBarFillTrack];
            case PaletteElement.TrackBarPosition:
                if (CommonHelper.IsOverrideStateExclude(state, PaletteState.FocusOverride))
                {
                    return GlobalStaticValues.EMPTY_COLOR;
                }

                return state switch
                {
                    PaletteState.Disabled => ControlPaint.LightLight(_ribbonColors[(int)SchemeBaseColors.ButtonNormalBack1]),
                    PaletteState.Normal => _ribbonColors[(int)SchemeBaseColors.ButtonNormalBack2],
                    PaletteState.Tracking or PaletteState.FocusOverride => _buttonBackColors[3],
                    PaletteState.Pressed => _buttonBackColors[5],
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
    public override KryptonColorTable ColorTable
    {
        get
        {
            _table ??= new KryptonVisualStudio2010With2013ColorTable(_ribbonColors, InheritBool.True, this);

            return _table;
        }
    }
    #endregion

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

        // Update fonts to reflect any change in system settings
        DefineFonts();

        base.OnUserPreferenceChanged(sender, e);
    }
    #endregion

}