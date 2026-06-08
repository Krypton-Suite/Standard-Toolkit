#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Base for macOS palette variants: Material metrics, <see cref="RenderMacOS"/>, traffic-light chrome, rounded controls.
/// </summary>
public abstract class PaletteMacOSBase : PaletteMaterialBase
{
    private const float ControlCornerRadius = 6f;
    private const float WindowCornerRadius = 12f;

    protected PaletteMacOSBase(
        KryptonColorSchemeBase scheme,
        ImageList checkBoxList,
        ImageList galleryButtonList,
        Image?[] radioButtonArray)
        : base(scheme, checkBoxList, galleryButtonList, radioButtonArray)
    {
        RippleEffect = false;
    }

    /// <summary>
    /// Tint used with DWM blur-behind to suggest macOS title-bar vibrancy on Windows.
    /// </summary>
    internal Color GetTitleBarBlurTintColor()
    {
        Color surface = BaseColors?.PanelClient ?? Color.White;
        byte alpha = (byte)(IsDarkSurface() ? 210 : 230);
        return Color.FromArgb(alpha, surface);
    }

    /// <inheritdoc />
    public override IRenderer GetRenderer() => KryptonManager.RenderMacOS;

    /// <inheritdoc />
    public override PaletteRibbonShape GetRibbonShape() => PaletteRibbonShape.MacOS;

    /// <inheritdoc />
    public override PaletteRelativeEdgeAlign GetButtonSpecEdge(PaletteButtonSpecStyle style) =>
        style switch
        {
            PaletteButtonSpecStyle.FormClose or PaletteButtonSpecStyle.FormMin or PaletteButtonSpecStyle.FormMax
                or PaletteButtonSpecStyle.FormRestore
                or PaletteButtonSpecStyle.FormHelp => PaletteRelativeEdgeAlign.Near,
            _ => base.GetButtonSpecEdge(style)
        };

    /// <inheritdoc />
    public override int GetMetricInt(KryptonForm? owningForm, PaletteState state, PaletteMetricInt metric)
    {
        if (!CommonHelper.IsOverrideState(state))
        {
            switch (metric)
            {
                case PaletteMetricInt.HeaderButtonEdgeInsetForm:
                    return 12;
                case PaletteMetricInt.HeaderButtonEdgeInsetFormRight:
                    return 10;
                case PaletteMetricInt.RibbonTabGap:
                    return 4;
            }
        }

        return base.GetMetricInt(owningForm, state, metric);
    }

    /// <inheritdoc />
    public override Padding GetMetricPadding(KryptonForm? owningForm, PaletteState state, PaletteMetricPadding metric)
    {
        if (metric == PaletteMetricPadding.HeaderButtonPaddingForm)
        {
            return new Padding(4, 6, 4, 6);
        }

        return base.GetMetricPadding(owningForm, state, metric);
    }

    /// <inheritdoc />
    public override Image? GetButtonSpecImage(PaletteButtonSpecStyle style, PaletteState state) =>
        MacOSPaletteSharedAssets.GetFormButtonImage(style, state, IsDarkSurface()) ?? base.GetButtonSpecImage(style, state);

    /// <inheritdoc />
    public override int GetBorderWidth(PaletteBorderStyle style, PaletteState state)
    {
        if (style is PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose)
        {
            return 0;
        }

        if (!CommonHelper.IsOverrideState(state) && style == PaletteBorderStyle.ControlGroupBox)
        {
            return 1;
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

        if (style is PaletteBorderStyle.FormMain or PaletteBorderStyle.HeaderForm)
        {
            return WindowCornerRadius;
        }

        if (IsMacOSButtonBorderStyle(style) || IsMacOSInputBorderStyle(style))
        {
            return ControlCornerRadius;
        }

        if (style is PaletteBorderStyle.TabHighProfile
            or PaletteBorderStyle.TabLowProfile
            or PaletteBorderStyle.TabOneNote
            or PaletteBorderStyle.TabDock
            or PaletteBorderStyle.TabDockAutoHidden)
        {
            return ControlCornerRadius;
        }

        return base.GetBorderRounding(style, state);
    }

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabBottomColor(PaletteState state) => BaseColors?.PanelAlternative ?? Color.White;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTopColor(PaletteState state) => BaseColors?.PanelClient ?? Color.White;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTextColor(PaletteState state) => BaseColors?.TextLabelControl ?? Color.Black;

    /// <inheritdoc />
    public override Color GetRibbonTabRowGradientColor1(PaletteState state) => BaseColors?.PanelClient ?? Color.White;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) => BaseColors?.PanelAlternative ?? Color.White;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) => BaseColors?.PanelClient ?? Color.White;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) => BaseColors?.PanelClient ?? Color.White;

    /// <inheritdoc />
    public override float GetRibbonTabRowGradientRaftingAngle(PaletteState state) => 0f;

    /// <inheritdoc />
    public override Color GetBorderColor1(PaletteBorderStyle style, PaletteState state)
    {
        if (state == PaletteState.NormalDefaultOverride && IsMacOSPushButtonBorderStyle(style))
        {
            return BaseColors?.ButtonNormalDefaultBorder ?? GetMacOSAccentBlueColor1();
        }

        if (!CommonHelper.IsOverrideState(state))
        {
            if (style == PaletteBorderStyle.ControlGroupBox)
            {
                return BaseColors?.ControlBorder ?? Color.FromArgb(174, 174, 178);
            }

            if (IsMacOSTabBorderStyle(style))
            {
                if (state is PaletteState.CheckedNormal or PaletteState.CheckedTracking)
                {
                    return BaseColors?.TextButtonChecked ?? Color.FromArgb(0, 122, 255);
                }

                return BaseColors?.ControlBorder ?? Color.FromArgb(198, 198, 200);
            }
        }

        return base.GetBorderColor1(style, state);
    }

    /// <inheritdoc />
    public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
    {
        if (state == PaletteState.NormalDefaultOverride && IsMacOSPushButtonBorderStyle(style))
        {
            return BaseColors?.ButtonNormalDefaultBorder ?? GetMacOSAccentBlueColor1();
        }

        if (IsMacOSTabBorderStyle(style)
            && state is PaletteState.CheckedNormal or PaletteState.CheckedTracking)
        {
            return BaseColors?.PanelClient ?? Color.White;
        }

        return base.GetBorderColor2(style, state);
    }

    /// <inheritdoc />
    public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state)
    {
        if (!CommonHelper.IsOverrideState(state) && IsMacOSTabBackStyle(style))
        {
            return PaletteColorStyle.Solid;
        }

        if (style is PaletteBackStyle.HeaderForm or PaletteBackStyle.HeaderPrimary
            or PaletteBackStyle.HeaderSecondary or PaletteBackStyle.HeaderDockActive
            or PaletteBackStyle.HeaderDockInactive)
        {
            return PaletteColorStyle.Solid;
        }

        if (!CommonHelper.IsOverrideState(state)
            && IsMacOSListSelectionState(state)
            && (IsMacOSListSelectionBackStyle(style) || IsMacOSGridDataCellBackStyle(style)))
        {
            return PaletteColorStyle.Solid;
        }

        return base.GetBackColorStyle(style, state);
    }

    /// <inheritdoc />
    public override Color GetBackColor1(PaletteBackStyle style, PaletteState state)
    {
        if (!CommonHelper.IsOverrideState(state) && IsMacOSTabBackStyle(style))
        {
            return state switch
            {
                PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed
                    => BaseColors?.PanelClient ?? base.GetBackColor1(style, state),
                PaletteState.Normal or PaletteState.Tracking or PaletteState.Pressed
                    => BaseColors?.PanelAlternative ?? base.GetBackColor1(style, state),
                _ => base.GetBackColor1(style, state)
            };
        }

        if (style is PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose)
        {
            return Color.Transparent;
        }

        if (style is PaletteBackStyle.HeaderForm or PaletteBackStyle.HeaderPrimary)
        {
            Color surface = BaseColors?.PanelClient ?? base.GetBackColor1(style, state);
            byte alpha = (byte)(IsDarkSurface() ? 235 : 248);
            return Color.FromArgb(alpha, surface);
        }

        if (style is PaletteBackStyle.HeaderSecondary or PaletteBackStyle.HeaderDockActive or PaletteBackStyle.HeaderDockInactive)
        {
            return BaseColors?.PanelAlternative ?? base.GetBackColor1(style, state);
        }

        if (!CommonHelper.IsOverrideState(state) && IsMacOSListSelectionState(state))
        {
            if (IsMacOSListSelectionBackStyle(style) || IsMacOSGridDataCellBackStyle(style))
            {
                return GetMacOSAccentBlueColor1();
            }
        }

        if (state == PaletteState.NormalDefaultOverride && IsMacOSPushButtonBackStyle(style))
        {
            return GetMacOSAccentBlueColor1();
        }

        return base.GetBackColor1(style, state);
    }

    /// <inheritdoc />
    public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
    {
        if (!CommonHelper.IsOverrideState(state) && IsMacOSTabBackStyle(style))
        {
            return GetBackColor1(style, state);
        }

        if (style is PaletteBackStyle.HeaderForm or PaletteBackStyle.HeaderPrimary)
        {
            Color surface = BaseColors?.PanelClient ?? base.GetBackColor2(style, state);
            byte alpha = (byte)(IsDarkSurface() ? 235 : 248);
            return Color.FromArgb(alpha, surface);
        }

        if (style is PaletteBackStyle.HeaderSecondary or PaletteBackStyle.HeaderDockActive or PaletteBackStyle.HeaderDockInactive)
        {
            return BaseColors?.PanelAlternative ?? base.GetBackColor2(style, state);
        }

        if (!CommonHelper.IsOverrideState(state) && IsMacOSListSelectionState(state))
        {
            if (IsMacOSListSelectionBackStyle(style) || IsMacOSGridDataCellBackStyle(style))
            {
                return GetMacOSAccentBlueColor1();
            }
        }

        if (state == PaletteState.NormalDefaultOverride && IsMacOSPushButtonBackStyle(style))
        {
            return GetMacOSAccentBlueColor2();
        }

        return base.GetBackColor2(style, state);
    }

    public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
    {
        if (!CommonHelper.IsOverrideState(state) && IsMacOSTabContentStyle(style))
        {
            return state is PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed
                ? BaseColors?.TextLabelControl ?? base.GetContentShortTextColor1(style, state)
                : BaseColors?.TextButtonNormal ?? base.GetContentShortTextColor1(style, state);
        }

        if (!CommonHelper.IsOverrideState(state)
            && (IsMacOSListSelectionContentStyle(style) || IsMacOSGridDataCellContentStyle(style))
            && IsMacOSListSelectionState(state))
        {
            return GetMacOSAccentBlueTextColor();
        }

        if (state == PaletteState.NormalDefaultOverride && IsMacOSPushButtonContentStyle(style))
        {
            return GetMacOSAccentBlueTextColor();
        }

        return base.GetContentShortTextColor1(style, state);
    }

    public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
    {
        if (!CommonHelper.IsOverrideState(state)
            && (IsMacOSListSelectionContentStyle(style) || IsMacOSGridDataCellContentStyle(style))
            && IsMacOSListSelectionState(state))
        {
            return GetMacOSAccentBlueTextColor();
        }

        return base.GetContentShortTextColor2(style, state);
    }

    public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state)
    {
        if (!CommonHelper.IsOverrideState(state)
            && (IsMacOSListSelectionContentStyle(style) || IsMacOSGridDataCellContentStyle(style))
            && IsMacOSListSelectionState(state))
        {
            return GetMacOSAccentBlueTextColor();
        }

        return base.GetContentLongTextColor1(style, state);
    }

    public override Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state)
    {
        if (!CommonHelper.IsOverrideState(state)
            && (IsMacOSListSelectionContentStyle(style) || IsMacOSGridDataCellContentStyle(style))
            && IsMacOSListSelectionState(state))
        {
            return GetMacOSAccentBlueTextColor();
        }

        return base.GetContentLongTextColor2(style, state);
    }

    private static bool IsMacOSTabBorderStyle(PaletteBorderStyle style) =>
        style switch
        {
            PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote
                or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden => true,
            _ => false
        };

    private static bool IsMacOSButtonBorderStyle(PaletteBorderStyle style) =>
        style switch
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

    private static bool IsMacOSInputBorderStyle(PaletteBorderStyle style) =>
        style switch
        {
            PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon
                or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2
                or PaletteBorderStyle.InputControlCustom3 => true,
            _ => false
        };

    private Color GetMacOSAccentBlueColor1() =>
       BaseColors?.ButtonNormalDefaultBack1 ?? (IsDarkSurface() ? Color.FromArgb(10, 132, 255) : Color.FromArgb(0, 122, 255));

    private Color GetMacOSAccentBlueColor2() =>
        BaseColors?.ButtonNormalDefaultBack2 ?? GetMacOSAccentBlueColor1();

    private static Color GetMacOSAccentBlueTextColor() => Color.White;

    private static bool IsMacOSListSelectionState(PaletteState state) =>
        state is PaletteState.Tracking
            or PaletteState.Pressed
            or PaletteState.CheckedNormal
            or PaletteState.CheckedTracking
            or PaletteState.CheckedPressed;

    private static bool IsMacOSListSelectionBackStyle(PaletteBackStyle style) =>
        style is PaletteBackStyle.ContextMenuItemHighlight or PaletteBackStyle.ButtonListItem;

    private static bool IsMacOSGridDataCellBackStyle(PaletteBackStyle style) =>
        style is PaletteBackStyle.GridDataCellList
            or PaletteBackStyle.GridDataCellSheet
            or PaletteBackStyle.GridDataCellCustom1
            or PaletteBackStyle.GridDataCellCustom2
            or PaletteBackStyle.GridDataCellCustom3;

    private static bool IsMacOSGridDataCellContentStyle(PaletteContentStyle style) =>  
        style is PaletteContentStyle.GridDataCellList
            or PaletteContentStyle.GridDataCellSheet
            or PaletteContentStyle.GridDataCellCustom1
            or PaletteContentStyle.GridDataCellCustom2
            or PaletteContentStyle.GridDataCellCustom3;

    private static bool IsMacOSListSelectionContentStyle(PaletteContentStyle style) =>
        style is PaletteContentStyle.ButtonListItem
            or PaletteContentStyle.ContextMenuItemTextStandard
            or PaletteContentStyle.ContextMenuItemTextAlternate
            or PaletteContentStyle.ContextMenuItemShortcutText;

    private static bool IsMacOSPushButtonBackStyle(PaletteBackStyle style) =>
        style is PaletteBackStyle.ButtonStandalone
            or PaletteBackStyle.ButtonGallery
            or PaletteBackStyle.ButtonAlternate
            or PaletteBackStyle.ButtonLowProfile
            or PaletteBackStyle.ButtonBreadCrumb
            or PaletteBackStyle.ButtonCommand
            or PaletteBackStyle.ButtonButtonSpec
            or PaletteBackStyle.ButtonCluster
            or PaletteBackStyle.ButtonCustom1
            or PaletteBackStyle.ButtonCustom2
            or PaletteBackStyle.ButtonCustom3;

    private static bool IsMacOSPushButtonBorderStyle(PaletteBorderStyle style) =>
        style is PaletteBorderStyle.ButtonStandalone
            or PaletteBorderStyle.ButtonGallery
            or PaletteBorderStyle.ButtonAlternate
            or PaletteBorderStyle.ButtonLowProfile
            or PaletteBorderStyle.ButtonBreadCrumb
            or PaletteBorderStyle.ButtonCommand
            or PaletteBorderStyle.ButtonButtonSpec
            or PaletteBorderStyle.ButtonCluster
            or PaletteBorderStyle.ButtonCustom1
            or PaletteBorderStyle.ButtonCustom2
            or PaletteBorderStyle.ButtonCustom3;

    private static bool IsMacOSPushButtonContentStyle(PaletteContentStyle style) =>
        style is PaletteContentStyle.ButtonStandalone
            or PaletteContentStyle.ButtonGallery
            or PaletteContentStyle.ButtonAlternate
            or PaletteContentStyle.ButtonLowProfile
            or PaletteContentStyle.ButtonBreadCrumb
            or PaletteContentStyle.ButtonCommand
            or PaletteContentStyle.ButtonButtonSpec
            or PaletteContentStyle.ButtonCluster
            or PaletteContentStyle.ButtonCustom1
            or PaletteContentStyle.ButtonCustom2
            or PaletteContentStyle.ButtonCustom3;

    private static bool IsMacOSTabBackStyle(PaletteBackStyle style) =>
        style switch
        {
            PaletteBackStyle.TabHighProfile or PaletteBackStyle.TabStandardProfile
                or PaletteBackStyle.TabLowProfile or PaletteBackStyle.TabOneNote
                or PaletteBackStyle.TabDock or PaletteBackStyle.TabDockAutoHidden
                or PaletteBackStyle.TabCustom1 or PaletteBackStyle.TabCustom2
                or PaletteBackStyle.TabCustom3 => true,
            _ => false,
        };

    private static bool IsMacOSTabContentStyle(PaletteContentStyle style) =>
        style switch
        {
            PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile
                or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock
                or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1
                or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 => true,
            _ => false
        };
}