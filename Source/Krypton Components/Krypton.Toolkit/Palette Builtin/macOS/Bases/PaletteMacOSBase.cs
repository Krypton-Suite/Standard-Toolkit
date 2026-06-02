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
    public override PaletteRelativeEdgeAlign GetButtonSpecEdge(PaletteButtonSpecStyle style)
    {
        switch (style)
        {
            case PaletteButtonSpecStyle.FormClose:
            case PaletteButtonSpecStyle.FormMin:
            case PaletteButtonSpecStyle.FormMax:
            case PaletteButtonSpecStyle.FormRestore:
            case PaletteButtonSpecStyle.FormHelp:
                return PaletteRelativeEdgeAlign.Near;
            default:
                return base.GetButtonSpecEdge(style);
        }
    }

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

        if (IsMacButtonBorderStyle(style) || IsMacInputBorderStyle(style))
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
        if (IsMacTabBorderStyle(style))
        {
            if (state is PaletteState.CheckedNormal or PaletteState.CheckedTracking)
            {
                return BaseColors?.TextButtonNormal ?? Color.FromArgb(0, 122, 255);
            }

            return BaseColors?.ControlBorder ?? Color.FromArgb(198, 198, 200);
        }

        return base.GetBorderColor1(style, state);
    }

    /// <inheritdoc />
    public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
    {
        if (IsMacTabBorderStyle(style)
            && state is PaletteState.CheckedNormal or PaletteState.CheckedTracking)
        {
            return BaseColors?.PanelClient ?? Color.White;
        }

        return base.GetBorderColor2(style, state);
    }

    /// <inheritdoc />
    public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state)
    {
        if (style is PaletteBackStyle.HeaderForm or PaletteBackStyle.HeaderPrimary
            or PaletteBackStyle.HeaderSecondary or PaletteBackStyle.HeaderDockActive
            or PaletteBackStyle.HeaderDockInactive)
        {
            return PaletteColorStyle.Solid;
        }

        return base.GetBackColorStyle(style, state);
    }

    /// <inheritdoc />
    public override Color GetBackColor1(PaletteBackStyle style, PaletteState state)
    {
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

        return base.GetBackColor1(style, state);
    }

    /// <inheritdoc />
    public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
    {
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

        return base.GetBackColor2(style, state);
    }

    private static bool IsMacTabBorderStyle(PaletteBorderStyle style)
    {
        switch (style)
        {
            case PaletteBorderStyle.TabHighProfile:
            case PaletteBorderStyle.TabLowProfile:
            case PaletteBorderStyle.TabOneNote:
            case PaletteBorderStyle.TabDock:
            case PaletteBorderStyle.TabDockAutoHidden:
                return true;
            default:
                return false;
        }
    }

    private static bool IsMacButtonBorderStyle(PaletteBorderStyle style)
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

    private static bool IsMacInputBorderStyle(PaletteBorderStyle style)
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
}