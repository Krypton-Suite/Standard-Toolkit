#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Base palette for Material family variants. Uses Microsoft 365 color wiring while applying
/// the Material renderer and denser metrics.
/// </summary>
public abstract class PaletteMaterialBase : PaletteMicrosoft365Base
{
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
            case PaletteBackStyle.ContextMenuOuter:
            case PaletteBackStyle.ContextMenuInner:
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
            case PaletteBackStyle.HeaderForm:
            case PaletteBackStyle.HeaderPrimary:
                // Flat, solid header background from scheme
                return BaseColors.HeaderPrimaryBack1;
            // Ensure dropdown/popups that use ControlClient (e.g. ComboBox list) pick up
            // the scheme panel color instead of the base Microsoft 365 window color.
            case PaletteBackStyle.ControlClient:
                return base.GetBackColor1(PaletteBackStyle.PanelClient, state);
            case PaletteBackStyle.ContextMenuOuter:
            case PaletteBackStyle.ContextMenuInner:
                return Color.FromArgb(32, 32, 32);
            default:
                return base.GetBackColor1(style, state);
        }
    }

    /// <inheritdoc />
    public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
    {
        switch (style)
        {
            case PaletteBackStyle.HeaderForm:
            case PaletteBackStyle.HeaderPrimary:
                return BaseColors.HeaderPrimaryBack2;
            case PaletteBackStyle.ControlClient:
                return base.GetBackColor2(PaletteBackStyle.PanelClient, state);
            case PaletteBackStyle.ContextMenuOuter:
            case PaletteBackStyle.ContextMenuInner:
                return Color.FromArgb(48, 48, 48);
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
                return base.GetMetricPadding(owningForm, state, metric);
        }
    }
    #endregion

    #region Border
    /// <inheritdoc />
    public override int GetBorderWidth(PaletteBorderStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            return -1;
        }

        switch (style)
        {
            case PaletteBorderStyle.FormMain:
            case PaletteBorderStyle.HeaderForm:
                // Hide visual chrome; hit-testing is handled separately to retain resize.
                return 0;
            default:
                return base.GetBorderWidth(style, state);
        }
    }

    /// <inheritdoc />
    public override float GetBorderRounding(PaletteBorderStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            return GlobalStaticValues.DEFAULT_PRIMARY_CORNER_ROUNDING_VALUE;
        }

        switch (style)
        {
            case PaletteBorderStyle.FormMain:
            case PaletteBorderStyle.HeaderForm:
                // Keep form chrome flat per Material.
                return 0f;
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
            case PaletteBorderStyle.InputControlStandalone:
            case PaletteBorderStyle.InputControlRibbon:
            case PaletteBorderStyle.InputControlCustom1:
            case PaletteBorderStyle.InputControlCustom2:
            case PaletteBorderStyle.InputControlCustom3:
                return 4f;
            default:
                return base.GetBorderRounding(style, state);
        }
    }

    /// <inheritdoc />
    public override Color GetBorderColor1(PaletteBorderStyle style, PaletteState state)
    {
        // Keep form border color aligned with header background to avoid a contrasting edge
        switch (style)
        {
            case PaletteBorderStyle.FormMain:
            case PaletteBorderStyle.HeaderForm:
                return GetBackColor1(PaletteBackStyle.HeaderForm, state);
            default:
                return base.GetBorderColor1(style, state);
        }
    }

    /// <inheritdoc />
    public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
    {
        switch (style)
        {
            case PaletteBorderStyle.FormMain:
            case PaletteBorderStyle.HeaderForm:
                return GetBackColor2(PaletteBackStyle.HeaderForm, state);
            default:
                return base.GetBorderColor2(style, state);
        }
    }
    #endregion
}
