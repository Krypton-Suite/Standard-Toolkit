#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2026. All rights reserved.
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

        return style switch
        {
            PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonAlternate or PaletteBackStyle.ButtonLowProfile
                or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem
                or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonCluster
                or PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose or PaletteBackStyle.ButtonCustom1
                or PaletteBackStyle.ButtonCustom2 or PaletteBackStyle.ButtonCustom3 =>
                // Force flat solid fills for buttons
                PaletteColorStyle.Solid,
            PaletteBackStyle.ContextMenuOuter or PaletteBackStyle.ContextMenuInner
                or PaletteBackStyle.ContextMenuItemHighlight => PaletteColorStyle.Solid,
            PaletteBackStyle.ControlClient or PaletteBackStyle.ControlAlternate or PaletteBackStyle.ControlCustom1
                or PaletteBackStyle.ControlCustom2 or PaletteBackStyle.ControlCustom3 or PaletteBackStyle.HeaderPrimary
                or PaletteBackStyle.HeaderSecondary or PaletteBackStyle.HeaderDockActive
                or PaletteBackStyle.HeaderDockInactive or PaletteBackStyle.HeaderForm or PaletteBackStyle.HeaderCalendar
                or PaletteBackStyle.HeaderCustom1 or PaletteBackStyle.HeaderCustom2 or PaletteBackStyle.HeaderCustom3
                or PaletteBackStyle.FormMain or PaletteBackStyle.InputControlStandalone
                or PaletteBackStyle.InputControlRibbon or PaletteBackStyle.InputControlCustom1
                or PaletteBackStyle.InputControlCustom2
                or PaletteBackStyle.InputControlCustom3 => PaletteColorStyle.Solid,
            _ => base.GetBackColorStyle(style, state)
        };
    }

    /// <inheritdoc />
    public override PaletteGraphicsHint GetBackGraphicsHint(PaletteBackStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteGraphicsHint.Inherit;
        }

        return style switch
        {
            PaletteBackStyle.ContextMenuOuter or PaletteBackStyle.ContextMenuInner
                or PaletteBackStyle.ContextMenuItemHighlight => PaletteGraphicsHint.None,
            PaletteBackStyle.ControlClient or PaletteBackStyle.ControlAlternate or PaletteBackStyle.ControlCustom1
                or PaletteBackStyle.ControlCustom2 or PaletteBackStyle.ControlCustom3 or PaletteBackStyle.HeaderPrimary
                or PaletteBackStyle.HeaderSecondary or PaletteBackStyle.HeaderDockActive
                or PaletteBackStyle.HeaderDockInactive or PaletteBackStyle.HeaderForm or PaletteBackStyle.HeaderCalendar
                or PaletteBackStyle.HeaderCustom1 or PaletteBackStyle.HeaderCustom2 or PaletteBackStyle.HeaderCustom3
                or PaletteBackStyle.FormMain or PaletteBackStyle.InputControlStandalone
                or PaletteBackStyle.InputControlRibbon or PaletteBackStyle.InputControlCustom1
                or PaletteBackStyle.InputControlCustom2
                or PaletteBackStyle.InputControlCustom3 => PaletteGraphicsHint.None,
            _ => base.GetBackGraphicsHint(style, state)
        };
    }

    /// <inheritdoc />
    public override Color GetBackColor1(PaletteBackStyle style, PaletteState state)
    {
        // Force dark popups and context menus without touching scheme files
        return style switch
        {
            // Buttons: use neutral surface color; state-specific nuances are small and handled by BorderColor/2
            PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonAlternate or PaletteBackStyle.ButtonLowProfile
                or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem
                or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonCluster
                or PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose or PaletteBackStyle.ButtonCustom1
                or PaletteBackStyle.ButtonCustom2 or PaletteBackStyle.ButtonCustom3 =>
                // Neutral container/background for Material with state overlays
                GetMaterialButtonBackColor(state),
            PaletteBackStyle.HeaderForm or PaletteBackStyle.HeaderPrimary =>
                // Flat, solid header background from scheme
                BaseColors?.HeaderPrimaryBack1 ?? SystemColors.ControlDark,
            // Ensure dropdown/popups that use ControlClient (e.g. ComboBox list) pick up
            // the scheme panel color instead of the base Microsoft 365 window color.
            PaletteBackStyle.ControlClient => base.GetBackColor1(PaletteBackStyle.PanelClient, state),
            // DataGridView data cells use the surface background; selected uses scheme highlight
            PaletteBackStyle.GridDataCellList or PaletteBackStyle.GridDataCellSheet
                or PaletteBackStyle.GridDataCellCustom1 or PaletteBackStyle.GridDataCellCustom2
                or PaletteBackStyle.GridDataCellCustom3 => state == PaletteState.CheckedNormal
                    ? BaseColors?.GridDataCellSelected ?? base.GetBackColor1(style, state)
                    : BaseColors?.PanelClient ?? base.GetBackColor1(PaletteBackStyle.PanelClient, state),
            PaletteBackStyle.ContextMenuOuter or PaletteBackStyle.ContextMenuInner =>
                // Use scheme surface so Light stays light and Dark stays dark
                BaseColors?.PanelClient ?? base.GetBackColor1(PaletteBackStyle.PanelClient, state),
            PaletteBackStyle.ContextMenuItemHighlight =>
                // Use the same subtle overlay model as buttons for hover/pressed highlight
                GetMaterialButtonBackColor(state),
            _ => base.GetBackColor1(style, state)
        };
    }

    /// <inheritdoc />
    public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
    {
        return style switch
        {
            PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonAlternate or PaletteBackStyle.ButtonLowProfile
                or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem
                or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonCluster
                or PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose or PaletteBackStyle.ButtonCustom1
                or PaletteBackStyle.ButtonCustom2
                or PaletteBackStyle.ButtonCustom3 => GetMaterialButtonBackColor(state),
            PaletteBackStyle.HeaderForm or PaletteBackStyle.HeaderPrimary => BaseColors?.HeaderPrimaryBack2 ??
                                                                             SystemColors.ControlDarkDark,
            PaletteBackStyle.ControlClient => base.GetBackColor2(PaletteBackStyle.PanelClient, state),
            PaletteBackStyle.GridDataCellList or PaletteBackStyle.GridDataCellSheet
                or PaletteBackStyle.GridDataCellCustom1 or PaletteBackStyle.GridDataCellCustom2
                or PaletteBackStyle.GridDataCellCustom3 => state == PaletteState.CheckedNormal
                    ? BaseColors?.GridDataCellSelected ?? base.GetBackColor2(style, state)
                    : BaseColors?.PanelClient ?? base.GetBackColor2(PaletteBackStyle.PanelClient, state),
            PaletteBackStyle.ContextMenuOuter or PaletteBackStyle.ContextMenuInner => BaseColors?.PanelAlternative ??
                base.GetBackColor2(PaletteBackStyle.PanelClient, state),
            PaletteBackStyle.ContextMenuItemHighlight => GetMaterialButtonBackColor(state),
            _ => base.GetBackColor2(style, state)
        };
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

        return metric switch
        {
            PaletteMetricInt.PageButtonInset or PaletteMetricInt.RibbonTabGap
                or PaletteMetricInt.HeaderButtonEdgeInsetCalendar => 1,
            PaletteMetricInt.CheckButtonGap => 3,
            PaletteMetricInt.HeaderButtonEdgeInsetForm => 0,
            PaletteMetricInt.HeaderButtonEdgeInsetFormRight => 0,
            PaletteMetricInt.HeaderButtonEdgeInsetInputControl => 2 // keep buttons close to edges for Material
            ,
            PaletteMetricInt.HeaderButtonEdgeInsetPrimary or PaletteMetricInt.HeaderButtonEdgeInsetSecondary
                or PaletteMetricInt.HeaderButtonEdgeInsetDockInactive
                or PaletteMetricInt.HeaderButtonEdgeInsetDockActive or PaletteMetricInt.HeaderButtonEdgeInsetCustom1
                or PaletteMetricInt.HeaderButtonEdgeInsetCustom2 or PaletteMetricInt.HeaderButtonEdgeInsetCustom3
                or PaletteMetricInt.BarButtonEdgeOutside or PaletteMetricInt.BarButtonEdgeInside => 1,
            PaletteMetricInt.None => 0,
            PaletteMetricInt.DropDownArrowBaseSize => DropDownArrowGlyphDefaults.DefaultBaseSizeAt96Dpi,
            _ => base.GetMetricInt(owningForm, state, metric)
        };
    }

    /// <inheritdoc />
    public override Padding GetMetricPadding(KryptonForm? owningForm, PaletteState state, PaletteMetricPadding metric)
    {
        return metric switch
        {
            PaletteMetricPadding.HeaderButtonPaddingForm => new Padding(0),
            PaletteMetricPadding.PageButtonPadding => _metricPaddingPageButtons,
            PaletteMetricPadding.BarPaddingTabs => _metricPaddingBarTabs,
            PaletteMetricPadding.BarPaddingInside or PaletteMetricPadding.BarPaddingOnly => _metricPaddingBarInside,
            PaletteMetricPadding.BarPaddingOutside =>
                // Remove top/left outer padding around bar items to avoid stray chrome lines
                Padding.Empty,
            PaletteMetricPadding.RibbonButtonPadding => _metricPaddingRibbon,
            PaletteMetricPadding.RibbonAppButton => _metricPaddingRibbonAppButton,
            PaletteMetricPadding.HeaderButtonPaddingInputControl => _metricPaddingInputControl,
            PaletteMetricPadding.HeaderButtonPaddingPrimary or PaletteMetricPadding.HeaderButtonPaddingSecondary
                or PaletteMetricPadding.HeaderButtonPaddingDockInactive
                or PaletteMetricPadding.HeaderButtonPaddingDockActive or PaletteMetricPadding.HeaderButtonPaddingCustom1
                or PaletteMetricPadding.HeaderButtonPaddingCustom2 or PaletteMetricPadding.HeaderButtonPaddingCustom3
                or PaletteMetricPadding.HeaderButtonPaddingCalendar
                or PaletteMetricPadding.BarButtonPadding => _metricPaddingHeader,
            PaletteMetricPadding.HeaderGroupPaddingPrimary or PaletteMetricPadding.HeaderGroupPaddingSecondary
                or PaletteMetricPadding.HeaderGroupPaddingDockInactive
                or PaletteMetricPadding.HeaderGroupPaddingDockActive or PaletteMetricPadding.SeparatorPaddingLowProfile
                or PaletteMetricPadding.SeparatorPaddingHighInternalProfile
                or PaletteMetricPadding.SeparatorPaddingHighProfile or PaletteMetricPadding.SeparatorPaddingCustom1
                or PaletteMetricPadding.SeparatorPaddingCustom2 or PaletteMetricPadding.SeparatorPaddingCustom3
                or PaletteMetricPadding.ContextMenuItemHighlight or PaletteMetricPadding.ContextMenuItemsCollection
                or PaletteMetricPadding.ContextMenuItemOuter => Padding.Empty,
            _ => base.GetMetricPadding(owningForm, state, metric)
        };
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
        return style switch
        {
            PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery
                or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile
                or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem
                or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec
                or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonForm
                or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1
                or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3
                or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow
                or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonCalendarDay
                or PaletteBorderStyle.ButtonInputControl => true,
            _ => false
        };
    }

    private static bool IsAnyInputBorderStyle(PaletteBorderStyle style)
    {
        return style switch
        {
            PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon
                or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2
                or PaletteBorderStyle.InputControlCustom3 => true,
            _ => false
        };
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
            return GlobalStaticConstants.DEFAULT_PRIMARY_CORNER_ROUNDING_VALUE;
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
        return style switch
        {
            // Ensure KryptonDataGridView header text follows scheme header text
            // ToolStrip/Context menu item text should follow header text (white in Material Dark)
            PaletteContentStyle.ButtonListItem or PaletteContentStyle.HeaderForm
                or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet
                or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2
                or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList
                or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1
                or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3
                or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate
                or PaletteContentStyle.ContextMenuItemShortcutText => BaseColors?.HeaderText ??
                                                                      base.GetContentShortTextColor1(style, state),
            // Data cells: always use on-surface text (white in Material Dark, dark in Light)
            PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet
                or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2
                or PaletteContentStyle.GridDataCellCustom3 => BaseColors?.TextLabelControl ??
                                                              base.GetContentShortTextColor1(style, state),
            _ => base.GetContentShortTextColor1(style, state)
        };
    }

    /// <inheritdoc />
    public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
    {
        return style switch
        {
            PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet
                or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2
                or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList
                or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1
                or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3
                or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate
                or PaletteContentStyle.ContextMenuItemShortcutText => BaseColors?.HeaderText ??
                                                                      base.GetContentShortTextColor2(style, state),
            // Data cells: always use on-surface text secondary color (often same as Color1)
            PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet
                or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2
                or PaletteContentStyle.GridDataCellCustom3 => BaseColors?.TextLabelControl ??
                                                              base.GetContentShortTextColor2(style, state),
            _ => base.GetContentShortTextColor2(style, state)
        };
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
