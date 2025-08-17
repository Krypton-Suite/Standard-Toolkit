#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Base palette for Material family variants. Uses Microsoft 365 color wiring while applying
/// the Material renderer and denser metrics.
/// </summary>
public abstract class PaletteMaterialBase : PaletteMicrosoft365Base
{
    #region Material Tokens (first pass)
    private static Color BlendOverlay(Color baseColor, Color overlayColor, float overlayWeight)
        => CommonHelper.MergeColors(baseColor, 1f - overlayWeight, overlayColor, overlayWeight);

    protected abstract bool IsDarkSurface();

    private Color TokenSurface => BaseColors != null ? BaseColors.PanelClient : SystemColors.Control;
    private Color TokenOnSurface => BaseColors != null ? BaseColors.TextLabelControl : SystemColors.ControlText;
    private Color TokenOutline => BaseColors != null ? BaseColors.ControlBorder : SystemColors.ControlDark;
    private Color TokenPrimary => BaseColors != null ? BaseColors.TextButtonNormal : SystemColors.HotTrack;

    private Color GetMaterialButtonBackColor(PaletteState state)
    {
        var surface = TokenSurface;

        // Subtle state overlays per Material guidance (approximate)
        float overlay = state switch
        {
            PaletteState.Tracking => 0.06f,
            PaletteState.Pressed or PaletteState.CheckedPressed => 0.12f,
            PaletteState.CheckedNormal => 0.08f,
            PaletteState.CheckedTracking => 0.10f,
            _ => 0f
        };

        if (overlay <= 0f)
        {
            return surface;
        }

        var overlayColor = IsDarkSurface() ? Color.White : Color.Black;
        return BlendOverlay(surface, overlayColor, overlay);
    }
    #endregion

    #region Padding (Material compact)
    private static readonly Padding _metricPaddingHeader = new Padding(0, 2, 0, 2);
    private static readonly Padding _metricPaddingInputControl = new Padding(0);
    private static readonly Padding _metricPaddingBarInside = new Padding(2);
    private static readonly Padding _metricPaddingBarTabs = new Padding(0);
    private static readonly Padding _metricPaddingBarOutside = new Padding(0, 0, 0, 2);
    private static readonly Padding _metricPaddingPageButtons = new Padding(1, 2, 1, 2);
    private static readonly Padding _metricPaddingRibbon = new Padding(0, 1, 1, 1);
    private static readonly Padding _metricPaddingRibbonAppButton = new Padding(3, 0, 3, 0);
    #endregion

    #region Identity
    /// <summary>
    /// Initializes a new instance of the <see cref="PaletteMaterialBase"/> class.
    /// </summary>
    protected PaletteMaterialBase(
        [DisallowNull] KryptonColorSchemeBase scheme,
        [DisallowNull] ImageList checkBoxList,
        [DisallowNull] ImageList galleryButtonList,
        [DisallowNull] Image?[] radioButtonArray)
        : base(scheme, checkBoxList, galleryButtonList, radioButtonArray)
    {
        // Disable ripple by default
        RippleEffect = false;
    }
    #endregion

    #region Renderer
    /// <inheritdoc />
    public override IRenderer GetRenderer() => KryptonManager.RenderMaterial;
    public override InheritBool UseThemeFormChromeBorderWidth => InheritBool.True;
    #endregion

    #region Back
    /// <inheritdoc />
    public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteColorStyle.Inherit;
        }

        switch (style)
        {
            case PaletteBackStyle.ButtonStandalone:
            case PaletteBackStyle.ButtonAlternate:
            case PaletteBackStyle.ButtonLowProfile:
            case PaletteBackStyle.ButtonBreadCrumb:
            case PaletteBackStyle.ButtonListItem:
            case PaletteBackStyle.ButtonCommand:
            case PaletteBackStyle.ButtonButtonSpec:
            case PaletteBackStyle.ButtonCluster:
            case PaletteBackStyle.ButtonForm:
            case PaletteBackStyle.ButtonFormClose:
            case PaletteBackStyle.ButtonCustom1:
            case PaletteBackStyle.ButtonCustom2:
            case PaletteBackStyle.ButtonCustom3:
                // Force flat solid fills for buttons
                return PaletteColorStyle.Solid;
            case PaletteBackStyle.ContextMenuOuter:
            case PaletteBackStyle.ContextMenuInner:
            case PaletteBackStyle.ContextMenuItemHighlight:
                return PaletteColorStyle.Solid;
            case PaletteBackStyle.ControlClient:
            case PaletteBackStyle.ControlAlternate:
            case PaletteBackStyle.ControlCustom1:
            case PaletteBackStyle.ControlCustom2:
            case PaletteBackStyle.ControlCustom3:
            case PaletteBackStyle.HeaderPrimary:
            case PaletteBackStyle.HeaderSecondary:
            case PaletteBackStyle.HeaderDockActive:
            case PaletteBackStyle.HeaderDockInactive:
            case PaletteBackStyle.HeaderForm:
            case PaletteBackStyle.HeaderCalendar:
            case PaletteBackStyle.HeaderCustom1:
            case PaletteBackStyle.HeaderCustom2:
            case PaletteBackStyle.HeaderCustom3:
            case PaletteBackStyle.FormMain:
            case PaletteBackStyle.InputControlStandalone:
            case PaletteBackStyle.InputControlRibbon:
            case PaletteBackStyle.InputControlCustom1:
            case PaletteBackStyle.InputControlCustom2:
            case PaletteBackStyle.InputControlCustom3:
                return PaletteColorStyle.Solid;
            default:
                return base.GetBackColorStyle(style, state);
        }
    }

    /// <inheritdoc />
    public override PaletteGraphicsHint GetBackGraphicsHint(PaletteBackStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteGraphicsHint.Inherit;
        }

        switch (style)
        {
            case PaletteBackStyle.ContextMenuOuter:
            case PaletteBackStyle.ContextMenuInner:
            case PaletteBackStyle.ContextMenuItemHighlight:
                return PaletteGraphicsHint.None;
            case PaletteBackStyle.ControlClient:
            case PaletteBackStyle.ControlAlternate:
            case PaletteBackStyle.ControlCustom1:
            case PaletteBackStyle.ControlCustom2:
            case PaletteBackStyle.ControlCustom3:
            case PaletteBackStyle.HeaderPrimary:
            case PaletteBackStyle.HeaderSecondary:
            case PaletteBackStyle.HeaderDockActive:
            case PaletteBackStyle.HeaderDockInactive:
            case PaletteBackStyle.HeaderForm:
            case PaletteBackStyle.HeaderCalendar:
            case PaletteBackStyle.HeaderCustom1:
            case PaletteBackStyle.HeaderCustom2:
            case PaletteBackStyle.HeaderCustom3:
            case PaletteBackStyle.FormMain:
            case PaletteBackStyle.InputControlStandalone:
            case PaletteBackStyle.InputControlRibbon:
            case PaletteBackStyle.InputControlCustom1:
            case PaletteBackStyle.InputControlCustom2:
            case PaletteBackStyle.InputControlCustom3:
                return PaletteGraphicsHint.None;
            default:
                return base.GetBackGraphicsHint(style, state);
        }
    }

    /// <inheritdoc />
    public override Color GetBackColor1(PaletteBackStyle style, PaletteState state)
    {
        // Force dark popups and context menus without touching scheme files
        switch (style)
        {
            // Buttons: use neutral surface color; state-specific nuances are small and handled by BorderColor/2
            case PaletteBackStyle.ButtonStandalone:
            case PaletteBackStyle.ButtonAlternate:
            case PaletteBackStyle.ButtonLowProfile:
            case PaletteBackStyle.ButtonBreadCrumb:
            case PaletteBackStyle.ButtonListItem:
            case PaletteBackStyle.ButtonCommand:
            case PaletteBackStyle.ButtonButtonSpec:
            case PaletteBackStyle.ButtonCluster:
            case PaletteBackStyle.ButtonForm:
            case PaletteBackStyle.ButtonFormClose:
            case PaletteBackStyle.ButtonCustom1:
            case PaletteBackStyle.ButtonCustom2:
            case PaletteBackStyle.ButtonCustom3:
                // Neutral container/background for Material with state overlays
                return GetMaterialButtonBackColor(state);
            case PaletteBackStyle.HeaderForm:
            case PaletteBackStyle.HeaderPrimary:
                // Flat, solid header background from scheme
                return BaseColors?.HeaderPrimaryBack1 ?? SystemColors.ControlDark;
            // Ensure dropdown/popups that use ControlClient (e.g. ComboBox list) pick up
            // the scheme panel color instead of the base Microsoft 365 window color.
            case PaletteBackStyle.ControlClient:
                return base.GetBackColor1(PaletteBackStyle.PanelClient, state);
            // DataGridView data cells should use the surface background (dark in Material Dark)
            case PaletteBackStyle.GridDataCellList:
            case PaletteBackStyle.GridDataCellSheet:
            case PaletteBackStyle.GridDataCellCustom1:
            case PaletteBackStyle.GridDataCellCustom2:
            case PaletteBackStyle.GridDataCellCustom3:
                return BaseColors?.PanelClient ?? base.GetBackColor1(PaletteBackStyle.PanelClient, state);
            case PaletteBackStyle.ContextMenuOuter:
            case PaletteBackStyle.ContextMenuInner:
                // Use scheme surface so Light stays light and Dark stays dark
                return BaseColors?.PanelClient ?? base.GetBackColor1(PaletteBackStyle.PanelClient, state);
            case PaletteBackStyle.ContextMenuItemHighlight:
                // Use the same subtle overlay model as buttons for hover/pressed highlight
                return GetMaterialButtonBackColor(state);
            default:
                return base.GetBackColor1(style, state);
        }
    }

    /// <inheritdoc />
    public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
    {
        switch (style)
        {
            case PaletteBackStyle.ButtonStandalone:
            case PaletteBackStyle.ButtonAlternate:
            case PaletteBackStyle.ButtonLowProfile:
            case PaletteBackStyle.ButtonBreadCrumb:
            case PaletteBackStyle.ButtonListItem:
            case PaletteBackStyle.ButtonCommand:
            case PaletteBackStyle.ButtonButtonSpec:
            case PaletteBackStyle.ButtonCluster:
            case PaletteBackStyle.ButtonForm:
            case PaletteBackStyle.ButtonFormClose:
            case PaletteBackStyle.ButtonCustom1:
            case PaletteBackStyle.ButtonCustom2:
            case PaletteBackStyle.ButtonCustom3:
                return GetMaterialButtonBackColor(state);
            case PaletteBackStyle.HeaderForm:
            case PaletteBackStyle.HeaderPrimary:
                return BaseColors?.HeaderPrimaryBack2 ?? SystemColors.ControlDarkDark;
            case PaletteBackStyle.ControlClient:
                return base.GetBackColor2(PaletteBackStyle.PanelClient, state);
            case PaletteBackStyle.GridDataCellList:
            case PaletteBackStyle.GridDataCellSheet:
            case PaletteBackStyle.GridDataCellCustom1:
            case PaletteBackStyle.GridDataCellCustom2:
            case PaletteBackStyle.GridDataCellCustom3:
                return BaseColors?.PanelClient ?? base.GetBackColor2(PaletteBackStyle.PanelClient, state);
            case PaletteBackStyle.ContextMenuOuter:
            case PaletteBackStyle.ContextMenuInner:
                return BaseColors?.PanelAlternative ?? base.GetBackColor2(PaletteBackStyle.PanelClient, state);
            case PaletteBackStyle.ContextMenuItemHighlight:
                return GetMaterialButtonBackColor(state);
            default:
                return base.GetBackColor2(style, state);
        }
    }
    #endregion

    #region Content
    /// <inheritdoc />
    public override int GetContentAdjacentGap(PaletteContentStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            return -1;
        }

        return style switch
        {
            PaletteContentStyle.LabelSuperTip => 5,
            _ => 0
        };
    }
    #endregion

    #region Metric
    /// <inheritdoc />
    public override int GetMetricInt(KryptonForm? owningForm, PaletteState state, PaletteMetricInt metric)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            return -1;
        }

        switch (metric)
        {
            case PaletteMetricInt.PageButtonInset:
            case PaletteMetricInt.RibbonTabGap:
            case PaletteMetricInt.HeaderButtonEdgeInsetCalendar:
                return 1;
            case PaletteMetricInt.CheckButtonGap:
                return 3;
            case PaletteMetricInt.HeaderButtonEdgeInsetForm:
                return 0;
            case PaletteMetricInt.HeaderButtonEdgeInsetInputControl:
                return 2; // keep buttons close to edges for Material
            case PaletteMetricInt.HeaderButtonEdgeInsetPrimary:
            case PaletteMetricInt.HeaderButtonEdgeInsetSecondary:
            case PaletteMetricInt.HeaderButtonEdgeInsetDockInactive:
            case PaletteMetricInt.HeaderButtonEdgeInsetDockActive:
            case PaletteMetricInt.HeaderButtonEdgeInsetCustom1:
            case PaletteMetricInt.HeaderButtonEdgeInsetCustom2:
            case PaletteMetricInt.HeaderButtonEdgeInsetCustom3:
            case PaletteMetricInt.BarButtonEdgeOutside:
            case PaletteMetricInt.BarButtonEdgeInside:
                return 1;
            case PaletteMetricInt.None:
                return 0;
            default:
                return base.GetMetricInt(owningForm, state, metric);
        }
    }

    /// <inheritdoc />
    public override Padding GetMetricPadding(KryptonForm? owningForm, PaletteState state, PaletteMetricPadding metric)
    {
        switch (metric)
        {
            case PaletteMetricPadding.HeaderButtonPaddingForm:
                if (owningForm == null)
                {
                    return new Padding();
                }
                return new Padding(0, owningForm!.RealWindowBorders.Right, 0, 0);
            case PaletteMetricPadding.PageButtonPadding:
                return _metricPaddingPageButtons;
            case PaletteMetricPadding.BarPaddingTabs:
                return _metricPaddingBarTabs;
            case PaletteMetricPadding.BarPaddingInside:
            case PaletteMetricPadding.BarPaddingOnly:
                return _metricPaddingBarInside;
            case PaletteMetricPadding.BarPaddingOutside:
                // Remove top/left outer padding around bar items to avoid stray chrome lines
                return Padding.Empty;
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
                return base.GetMetricPadding(owningForm, state, metric);
        }
    }
    #endregion

    #region Border
    /// <inheritdoc />
    public override InheritBool GetBorderDraw(PaletteBorderStyle style, PaletteState state)
    {
        // Suppress container/docking chrome under Material
        if (style == PaletteBorderStyle.ControlClient || style == PaletteBorderStyle.ControlAlternate)
        {
            return InheritBool.False;
        }

        if (CommonHelper.IsOverrideState(state))
        {
            // Input focus override stays visible to provide underline
            if (state == PaletteState.FocusOverride && IsAnyInputBorderStyle(style))
            {
                return InheritBool.True;
            }

            // For all button styles, suppress borders through overrides (e.g., NormalDefaultOverride)
            if (IsAnyButtonBorderStyle(style))
            {
                return InheritBool.False;
            }

            return InheritBool.Inherit;
        }

        if (IsAnyButtonBorderStyle(style))
        {
            var interactiveMask = PaletteState.Tracking | PaletteState.Pressed | PaletteState.Checked;
            bool isInteractive = (state & interactiveMask) != 0;
            return isInteractive ? InheritBool.True : InheritBool.False;
        }

        return base.GetBorderDraw(style, state);
    }

    /// <inheritdoc />
    public override PaletteGraphicsHint GetBorderGraphicsHint(PaletteBorderStyle style, PaletteState state)
    {
        // Keep Material crisp/flat; no smoothing/blur/shadow
        return PaletteGraphicsHint.None;
    }

    private static bool IsAnyButtonBorderStyle(PaletteBorderStyle style)
    {
        switch (style)
        {
            case PaletteBorderStyle.ButtonStandalone:
            case PaletteBorderStyle.ButtonGallery:
            case PaletteBorderStyle.ButtonAlternate:
            case PaletteBorderStyle.ButtonLowProfile:
            case PaletteBorderStyle.ButtonBreadCrumb:
            case PaletteBorderStyle.ButtonListItem:
            case PaletteBorderStyle.ButtonCommand:
            case PaletteBorderStyle.ButtonButtonSpec:
            case PaletteBorderStyle.ButtonCluster:
            case PaletteBorderStyle.ButtonForm:
            case PaletteBorderStyle.ButtonFormClose:
            case PaletteBorderStyle.ButtonCustom1:
            case PaletteBorderStyle.ButtonCustom2:
            case PaletteBorderStyle.ButtonCustom3:
            case PaletteBorderStyle.ButtonNavigatorStack:
            case PaletteBorderStyle.ButtonNavigatorOverflow:
            case PaletteBorderStyle.ButtonNavigatorMini:
            case PaletteBorderStyle.ButtonCalendarDay:
            case PaletteBorderStyle.ButtonInputControl:
                return true;
            default:
                return false;
        }
    }

    private static bool IsAnyInputBorderStyle(PaletteBorderStyle style)
    {
        switch (style)
        {
            case PaletteBorderStyle.InputControlStandalone:
            case PaletteBorderStyle.InputControlRibbon:
            case PaletteBorderStyle.InputControlCustom1:
            case PaletteBorderStyle.InputControlCustom2:
            case PaletteBorderStyle.InputControlCustom3:
                return true;
            default:
                return false;
        }
    }
    /// <inheritdoc />
    public override PaletteDrawBorders GetBorderDrawBorders(PaletteBorderStyle style, PaletteState state)
    {
        // Suppress container borders entirely under Material
        if (style == PaletteBorderStyle.ControlClient || style == PaletteBorderStyle.ControlAlternate)
        {
            return PaletteDrawBorders.None;
        }

        // Route input controls to bottom-only to achieve underline focus style
        if (IsAnyInputBorderStyle(style))
        {
            // Respect focus override explicitly
            if (state == PaletteState.FocusOverride)
            {
                return PaletteDrawBorders.Bottom;
            }
            return PaletteDrawBorders.Bottom;
        }

        // Buttons: hide borders unless interactive (hover/pressed/checked), including override states
        if (IsAnyButtonBorderStyle(style))
        {
            var interactiveMask = PaletteState.Tracking | PaletteState.Pressed | PaletteState.Checked;
            bool isInteractive = (state & interactiveMask) != 0;
            // For override states (e.g., NormalDefaultOverride) treat as non-interactive
            if (CommonHelper.IsOverrideState(state) && state != PaletteState.FocusOverride)
            {
                isInteractive = false;
            }
            return isInteractive ? PaletteDrawBorders.All : PaletteDrawBorders.None;
        }

        return base.GetBorderDrawBorders(style, state);
    }

    /// <inheritdoc />
    public override int GetBorderWidth(PaletteBorderStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            // Promote focus underline thickness for inputs
            if (state == PaletteState.FocusOverride && IsAnyInputBorderStyle(style))
            {
                return 2;
            }

            // All button styles remain borderless through any override state
            if (IsAnyButtonBorderStyle(style))
            {
                return 0;
            }

            return -1;
        }

        // Normal path
        if (IsAnyButtonBorderStyle(style))
        {
            // Borderless when not interactive; outline only on hover/pressed/checked (context variants included)
            var interactiveMask = PaletteState.Tracking | PaletteState.Pressed | PaletteState.Checked;
            bool isInteractive = (state & interactiveMask) != 0;
            return isInteractive ? 1 : 0;
        }

        if (style == PaletteBorderStyle.FormMain || style == PaletteBorderStyle.HeaderForm)
        {
            // Ensure a visible 1px window outline for Material so the title bar is not lost
            return 1;
        }

        if (IsAnyInputBorderStyle(style))
        {
            // Underline style: draw only bottom edge; thicker when tracking/pressed/focused overrides route here
            return state is PaletteState.Disabled or PaletteState.Normal ? 1 : 2;
        }

        return base.GetBorderWidth(style, state);
    }

    /// <inheritdoc />
    public override float GetBorderRounding(PaletteBorderStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            return GlobalStaticValues.DEFAULT_PRIMARY_CORNER_ROUNDING_VALUE;
        }

        if (style == PaletteBorderStyle.FormMain || style == PaletteBorderStyle.HeaderForm)
        {
            // Keep form chrome flat per Material.
            return 0f;
        }

        if (IsAnyButtonBorderStyle(style) || IsAnyInputBorderStyle(style))
        {
            // Buttons and inputs: rectangular in Material pass here
            return 0f;
        }

        return base.GetBorderRounding(style, state);
    }

    /// <inheritdoc />
    public override Color GetBorderColor1(PaletteBorderStyle style, PaletteState state)
    {
        // Use scheme-defined form border colors for proper window outline visibility
        if (style == PaletteBorderStyle.FormMain || style == PaletteBorderStyle.HeaderForm)
        {
            return base.GetBorderColor1(style, state);
        }

        if (IsAnyButtonBorderStyle(style) || IsAnyInputBorderStyle(style))
        {
            // For Material theme, use the panel background color for "invisible" borders
            // This prevents white lines in dark themes when borders are technically present but shouldn't be visible
            return BaseColors?.PanelClient ?? Color.Transparent;
        }

        return base.GetBorderColor1(style, state);
    }

    /// <inheritdoc />
    public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
    {
        if (style == PaletteBorderStyle.FormMain || style == PaletteBorderStyle.HeaderForm)
        {
            return base.GetBorderColor2(style, state);
        }

        if (IsAnyButtonBorderStyle(style) || IsAnyInputBorderStyle(style))
        {
            // For Material theme, use the panel background color for "invisible" borders
            // This prevents white lines in dark themes when borders are technically present but shouldn't be visible
            return BaseColors?.PanelClient ?? Color.Transparent;
        }

        return base.GetBorderColor2(style, state);
    }
    #endregion

    #region Content (Material tweaks)
    /// <inheritdoc />
    public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
    {
        switch (style)
        {
            // Ensure KryptonDataGridView header text follows scheme header text
            // ToolStrip/Context menu item text should follow header text (white in Material Dark)
            case PaletteContentStyle.ButtonListItem:
            case PaletteContentStyle.HeaderForm:
            case PaletteContentStyle.GridHeaderColumnList:
            case PaletteContentStyle.GridHeaderColumnSheet:
            case PaletteContentStyle.GridHeaderColumnCustom1:
            case PaletteContentStyle.GridHeaderColumnCustom2:
            case PaletteContentStyle.GridHeaderColumnCustom3:
            case PaletteContentStyle.GridHeaderRowList:
            case PaletteContentStyle.GridHeaderRowSheet:
            case PaletteContentStyle.GridHeaderRowCustom1:
            case PaletteContentStyle.GridHeaderRowCustom2:
            case PaletteContentStyle.GridHeaderRowCustom3:
            case PaletteContentStyle.ContextMenuItemTextStandard:
            case PaletteContentStyle.ContextMenuItemTextAlternate:
            case PaletteContentStyle.ContextMenuItemShortcutText:
                return BaseColors?.HeaderText ?? base.GetContentShortTextColor1(style, state);

            // Data cells: always use on-surface text (white in Material Dark, dark in Light)
            case PaletteContentStyle.GridDataCellList:
            case PaletteContentStyle.GridDataCellSheet:
            case PaletteContentStyle.GridDataCellCustom1:
            case PaletteContentStyle.GridDataCellCustom2:
            case PaletteContentStyle.GridDataCellCustom3:
                return BaseColors?.TextLabelControl ?? base.GetContentShortTextColor1(style, state);
        }

        return base.GetContentShortTextColor1(style, state);
    }

    /// <inheritdoc />
    public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
    {
        switch (style)
        {
            case PaletteContentStyle.GridHeaderColumnList:
            case PaletteContentStyle.GridHeaderColumnSheet:
            case PaletteContentStyle.GridHeaderColumnCustom1:
            case PaletteContentStyle.GridHeaderColumnCustom2:
            case PaletteContentStyle.GridHeaderColumnCustom3:
            case PaletteContentStyle.GridHeaderRowList:
            case PaletteContentStyle.GridHeaderRowSheet:
            case PaletteContentStyle.GridHeaderRowCustom1:
            case PaletteContentStyle.GridHeaderRowCustom2:
            case PaletteContentStyle.GridHeaderRowCustom3:
            case PaletteContentStyle.ContextMenuItemTextStandard:
            case PaletteContentStyle.ContextMenuItemTextAlternate:
            case PaletteContentStyle.ContextMenuItemShortcutText:
                return BaseColors?.HeaderText ?? base.GetContentShortTextColor2(style, state);

            // Data cells: always use on-surface text secondary color (often same as Color1)
            case PaletteContentStyle.GridDataCellList:
            case PaletteContentStyle.GridDataCellSheet:
            case PaletteContentStyle.GridDataCellCustom1:
            case PaletteContentStyle.GridDataCellCustom2:
            case PaletteContentStyle.GridDataCellCustom3:
                return BaseColors?.TextLabelControl ?? base.GetContentShortTextColor2(style, state);
        }

        return base.GetContentShortTextColor2(style, state);
    }

    /// <inheritdoc />
    public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state)
    {
        switch (style)
        {
            // Match header foregrounds for long text as well
            // ToolStrip/Context menu long text
            case PaletteContentStyle.GridHeaderColumnList:
            case PaletteContentStyle.GridHeaderColumnSheet:
            case PaletteContentStyle.GridHeaderColumnCustom1:
            case PaletteContentStyle.GridHeaderColumnCustom2:
            case PaletteContentStyle.GridHeaderColumnCustom3:
            case PaletteContentStyle.GridHeaderRowList:
            case PaletteContentStyle.GridHeaderRowSheet:
            case PaletteContentStyle.GridHeaderRowCustom1:
            case PaletteContentStyle.GridHeaderRowCustom2:
            case PaletteContentStyle.GridHeaderRowCustom3:
            case PaletteContentStyle.ContextMenuItemTextStandard:
            case PaletteContentStyle.ContextMenuItemTextAlternate:
            case PaletteContentStyle.ContextMenuItemShortcutText:
                return BaseColors?.HeaderText ?? base.GetContentLongTextColor1(style, state);

            // Selected cells with long text: keep contrast consistent
            case PaletteContentStyle.GridDataCellList:
            case PaletteContentStyle.GridDataCellSheet:
            case PaletteContentStyle.GridDataCellCustom1:
            case PaletteContentStyle.GridDataCellCustom2:
            case PaletteContentStyle.GridDataCellCustom3:
                if (state is PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed)
                {
                    return BaseColors?.HeaderText ?? base.GetContentLongTextColor1(style, state);
                }
                break;
        }

        return base.GetContentLongTextColor1(style, state);
    }

    /// <inheritdoc />
    public override Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state)
    {
        switch (style)
        {
            case PaletteContentStyle.GridHeaderColumnList:
            case PaletteContentStyle.GridHeaderColumnSheet:
            case PaletteContentStyle.GridHeaderColumnCustom1:
            case PaletteContentStyle.GridHeaderColumnCustom2:
            case PaletteContentStyle.GridHeaderColumnCustom3:
            case PaletteContentStyle.GridHeaderRowList:
            case PaletteContentStyle.GridHeaderRowSheet:
            case PaletteContentStyle.GridHeaderRowCustom1:
            case PaletteContentStyle.GridHeaderRowCustom2:
            case PaletteContentStyle.GridHeaderRowCustom3:
            case PaletteContentStyle.ContextMenuItemTextStandard:
            case PaletteContentStyle.ContextMenuItemTextAlternate:
            case PaletteContentStyle.ContextMenuItemShortcutText:
                return BaseColors?.HeaderText ?? base.GetContentLongTextColor2(style, state);

            case PaletteContentStyle.GridDataCellList:
            case PaletteContentStyle.GridDataCellSheet:
            case PaletteContentStyle.GridDataCellCustom1:
            case PaletteContentStyle.GridDataCellCustom2:
            case PaletteContentStyle.GridDataCellCustom3:
                if (state is PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed)
                {
                    return BaseColors?.HeaderText ?? base.GetContentLongTextColor2(style, state);
                }
                break;
        }

        return base.GetContentLongTextColor2(style, state);
    }
    #endregion
}
