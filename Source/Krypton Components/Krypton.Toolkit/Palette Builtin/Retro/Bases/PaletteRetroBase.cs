#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Base for RetroUI-inspired palettes (flat push buttons, monospace text, shared renderer).
/// </summary>
public abstract class PaletteRetroBase : PaletteVisualStudioBase
{
    protected PaletteRetroBase(
        KryptonColorSchemeBase scheme,
        ImageList checkBoxList,
        ImageList galleryButtonList,
        Image?[] radioButtonArray)
        : base(scheme, checkBoxList, galleryButtonList, radioButtonArray)
    {
        BaseFont = new Font("Courier New", 12f, FontStyle.Regular);
        ChromeBackgroundColor = scheme.PanelClient;
        ButtonFaceColor = scheme.ButtonNormalBack1;
        ButtonDisabledColor = scheme.PanelAlternative;
        ProgressValueColor = scheme.ButtonNormalBack1;
        ButtonNormalTextColor = Color.White;
    }

    /// <summary>Gets the workspace chrome color behind retro push buttons.</summary>
    public Color ChromeBackgroundColor { get; }

    /// <summary>Gets the flat push-button face color.</summary>
    public Color ButtonFaceColor { get; }

    /// <summary>Gets the disabled push-button / list-item color.</summary>
    public Color ButtonDisabledColor { get; }

    /// <summary>Gets the flat progress value fill color.</summary>
    public Color ProgressValueColor { get; }

    /// <summary>Gets the text color on retro push buttons.</summary>
    protected Color ButtonNormalTextColor { get; set; }

    /// <summary>Gets the preferred text color on retro workspace surfaces.</summary>
    protected virtual Color WorkspaceTextColor => Color.Black;

    /// <summary>Gets the border color for retro group boxes.</summary>
    protected virtual Color GroupBoxBorderColor => Color.Black;

    /// <summary>Gets the border and shadow color for retro push buttons.</summary>
    internal virtual Color RetroButtonFrameColor => Color.Black;

    public override IRenderer GetRenderer() => KryptonManager.RenderRetro;

    /// <summary>
    /// Gets or sets the block shadow depth in pixels for retro push buttons (0–32).
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Block shadow size in pixels for retro push buttons. Also adjusts content padding (unpressed: top reduced by half the shadow; pressed: caption offset by the full shadow depth).")]
    [DefaultValue(8)]
    public int ButtonShadowSize
    {
        get => RetroRenderHelper.ButtonShadowSize;
        set
        {
            int previous = RetroRenderHelper.ButtonShadowSize;
            int clamped = RetroRenderHelper.SetButtonShadowSize(value);
            if (clamped != previous)
            {
                OnPalettePaint(this, new PaletteLayoutEventArgs(true, true));
            }
        }
    }

    private bool ShouldSerializeButtonShadowSize() => ButtonShadowSize != 8;

    private void ResetButtonShadowSize() => ButtonShadowSize = 8;

    public override Padding GetBorderContentPadding(KryptonForm? owningForm, PaletteContentStyle style,
        PaletteState state)
    {
        Padding padding = base.GetBorderContentPadding(owningForm, style, state);

        if (CommonHelper.IsOverrideState(state)
            || !RetroRenderHelper.IsRetroButtonContentStyle(style, state))
        {
            return padding;
        }

        return RetroRenderHelper.AdjustRetroButtonContentPadding(padding, state);
    }

    public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteColorStyle.Inherit;
        }

        if (RetroRenderHelper.UsesRetroPushButtonChrome(style)
            || IsRetroGridHeaderBack(style)
            || IsRetroGridDataCellBack(style))
        {
            return PaletteColorStyle.Solid;
        }

        return base.GetBackColorStyle(style, state);
    }

    public override Color GetBackColor1(PaletteBackStyle style, PaletteState state)
    {
        if (TryGetRetroListItemBackColor(style, state, out Color listColor))
        {
            return listColor;
        }

        if (TryGetRetroGroupBoxBackColor(style, out Color groupBoxColor)
            || TryGetRetroGridBackColor(style, state, out groupBoxColor))
        {
            return groupBoxColor;
        }

        if (IsRetroButton(style, state))
        {
            if (state == PaletteState.Disabled)
            {
                return ButtonDisabledColor;
            }

            return ButtonFaceColor;
        }

        return base.GetBackColor1(style, state);
    }

    public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
    {
        if (TryGetRetroListItemBackColor(style, state, out Color listColor))
        {
            return listColor;
        }

        if (TryGetRetroGroupBoxBackColor(style, out Color groupBoxColor)
            || TryGetRetroGridBackColor(style, state, out groupBoxColor))
        {
            return groupBoxColor;
        }

        if (IsRetroButton(style, state))
        {
            if (state == PaletteState.Disabled)
            {
                return ButtonDisabledColor;
            }

            return ButtonFaceColor;
        }

        return base.GetBackColor2(style, state);
    }

    public override InheritBool GetBorderDraw(PaletteBorderStyle style, PaletteState state)
    {
        if (IsRetroGroupBoxBorder(style, state))
        {
            return InheritBool.True;
        }

        if (IsRetroButtonBorder(style, state))
        {
            return InheritBool.False;
        }

        return base.GetBorderDraw(style, state);
    }

    public override int GetBorderWidth(PaletteBorderStyle style, PaletteState state)
    {
        if (IsRetroGroupBoxBorder(style, state))
        {
            return 1;
        }

        if (IsRetroButtonBorder(style, state))
        {
            return 0;
        }

        return base.GetBorderWidth(style, state);
    }

    public override Color GetBorderColor1(PaletteBorderStyle style, PaletteState state)
    {
        if (IsRetroGroupBoxBorder(style, state))
        {
            return GroupBoxBorderColor;
        }

        if (IsRetroHeaderPrimaryBorder(style, state))
        {
            return GroupBoxBorderColor;
        }

        return base.GetBorderColor1(style, state);
    }

    public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
    {
        if (IsRetroGroupBoxBorder(style, state))
        {
            return GroupBoxBorderColor;
        }

        if (IsRetroHeaderPrimaryBorder(style, state))
        {
            return GroupBoxBorderColor;
        }

        return base.GetBorderColor2(style, state);
    }

    public override PaletteColorStyle GetBorderColorStyle(PaletteBorderStyle style, PaletteState state)
    {
        if (IsRetroHeaderPrimaryBorder(style, state))
        {
            return PaletteColorStyle.Solid;
        }

        return base.GetBorderColorStyle(style, state);
    }

    public override float GetBorderRounding(PaletteBorderStyle style, PaletteState state)
    {
        if (IsRetroGroupBoxBorder(style, state))
        {
            return 0f;
        }

        return base.GetBorderRounding(style, state);
    }

    public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
    {
        if (TryGetRetroListItemTextColor(style, state, out Color listColor))
        {
            return listColor;
        }

        if (TryGetRetroCommandButtonTextColor(style, state, out Color commandColor))
        {
            return commandColor;
        }

        if (IsRetroButtonContent(style, state))
        {
            return state == PaletteState.Disabled ? Color.FromArgb(64, 64, 64) : ButtonNormalTextColor;
        }

        if (IsRetroGridDataCellContent(style, state))
        {
            return state == PaletteState.CheckedNormal ? Color.Black : GetRetroGridDataCellNormalTextColor();
        }

        Color color = base.GetContentShortTextColor1(style, state);
        return IsRetroWorkspaceContent(style) ? EnsureReadableOnWorkspace(color, state) : color;
    }

    public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
    {
        if (TryGetRetroListItemTextColor(style, state, out Color listColor))
        {
            return listColor;
        }

        if (TryGetRetroCommandButtonTextColor(style, state, out Color commandColor))
        {
            return commandColor;
        }

        if (IsRetroButtonContent(style, state))
        {
            return state == PaletteState.Disabled ? Color.FromArgb(64, 64, 64) : ButtonNormalTextColor;
        }

        if (IsRetroGridDataCellContent(style, state))
        {
            return state == PaletteState.CheckedNormal ? Color.Black : GetRetroGridDataCellNormalTextColor();
        }

        Color color = base.GetContentShortTextColor2(style, state);
        return IsRetroWorkspaceContent(style) ? EnsureReadableOnWorkspace(color, state) : color;
    }

    public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state)
    {
        if (TryGetRetroCommandButtonTextColor(style, state, out Color commandColor))
        {
            return commandColor;
        }

        Color color = base.GetContentLongTextColor1(style, state);
        return IsRetroWorkspaceContent(style) ? EnsureReadableOnWorkspace(color, state) : color;
    }

    public override Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state)
    {
        if (TryGetRetroCommandButtonTextColor(style, state, out Color commandColor))
        {
            return commandColor;
        }

        Color color = base.GetContentLongTextColor2(style, state);
        return IsRetroWorkspaceContent(style) ? EnsureReadableOnWorkspace(color, state) : color;
    }

    protected virtual Color GetRetroGridDataCellNormalBackColor() => Color.White;

    protected virtual Color GetRetroGridDataCellNormalTextColor() => Color.Black;

    protected virtual Color GetRetroCommandButtonTextColor() => Color.Black;

    private static bool IsRetroButtonBorder(PaletteBorderStyle style, PaletteState state) =>
        !CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride)
        && RetroRenderHelper.UsesRetroPushButtonChrome(style);

    private static bool IsRetroGroupBoxBorder(PaletteBorderStyle style, PaletteState state) =>
        style == PaletteBorderStyle.ControlGroupBox
        && !CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride);

    private static bool IsRetroHeaderPrimaryBorder(PaletteBorderStyle style, PaletteState state) =>
        style == PaletteBorderStyle.HeaderPrimary
        && !CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride);

    private static bool IsRetroButtonContent(PaletteContentStyle style, PaletteState state) =>
        !CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride)
        && RetroRenderHelper.UsesRetroPushButtonChrome(style);

    private static bool IsRetroButton(PaletteBackStyle style, PaletteState state) =>
        !CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride)
        && RetroRenderHelper.UsesRetroPushButtonChrome(style);

    private static bool IsRetroGridHeaderBack(PaletteBackStyle style) =>
        style is PaletteBackStyle.GridHeaderColumnList or PaletteBackStyle.GridHeaderColumnCustom1
            or PaletteBackStyle.GridHeaderColumnCustom2 or PaletteBackStyle.GridHeaderColumnCustom3
            or PaletteBackStyle.GridHeaderRowList or PaletteBackStyle.GridHeaderRowCustom1
            or PaletteBackStyle.GridHeaderRowCustom2 or PaletteBackStyle.GridHeaderRowCustom3;

    private static bool IsRetroGridDataCellBack(PaletteBackStyle style) =>
        style is PaletteBackStyle.GridDataCellList or PaletteBackStyle.GridDataCellCustom1
            or PaletteBackStyle.GridDataCellCustom2 or PaletteBackStyle.GridDataCellCustom3;

    private static bool IsRetroGridContent(PaletteContentStyle style) =>
        style is PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnCustom1
            or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3
            or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowCustom1
            or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3
            or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellCustom1
            or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3;

    private static bool IsRetroGridDataCellContent(PaletteContentStyle style, PaletteState state) =>
        !CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride)
        && style is PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellCustom1
            or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3;

    private static bool IsRetroWorkspaceContent(PaletteContentStyle style) =>
        IsRetroGridContent(style) || style == PaletteContentStyle.LabelGroupBoxCaption;

    private static bool IsRetroWorkspaceBack(PaletteBackStyle style) =>
        style is PaletteBackStyle.ContextMenuOuter or PaletteBackStyle.ContextMenuInner
            or PaletteBackStyle.ContextMenuSeparator or PaletteBackStyle.ContextMenuItemSplit
            or PaletteBackStyle.PanelClient or PaletteBackStyle.PanelAlternate
            or PaletteBackStyle.ControlClient or PaletteBackStyle.ControlAlternate
            or PaletteBackStyle.ControlGroupBox or PaletteBackStyle.GridBackgroundList
            or PaletteBackStyle.GridBackgroundCustom1 or PaletteBackStyle.GridBackgroundCustom2
            or PaletteBackStyle.GridBackgroundCustom3;

    private bool TryGetRetroGroupBoxBackColor(PaletteBackStyle style, out Color color)
    {
        color = GlobalStaticVariables.EMPTY_COLOR;

        if (style != PaletteBackStyle.ControlGroupBox)
        {
            return false;
        }

        color = ChromeBackgroundColor;
        return true;
    }

    private bool TryGetRetroGridBackColor(PaletteBackStyle style, PaletteState state, out Color color)
    {
        color = GlobalStaticVariables.EMPTY_COLOR;

        if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
        {
            return false;
        }

        if (IsRetroGridDataCellBack(style))
        {
            color = state == PaletteState.CheckedNormal ? ButtonFaceColor : GetRetroGridDataCellNormalBackColor();
            return true;
        }

        if (IsRetroGridHeaderBack(style))
        {
            color = state switch
            {
                PaletteState.Pressed or PaletteState.Tracking or PaletteState.CheckedNormal
                    or PaletteState.CheckedTracking or PaletteState.CheckedPressed => ButtonFaceColor,
                PaletteState.Disabled => ButtonDisabledColor,
                _ => ButtonDisabledColor
            };
            return true;
        }

        if (IsRetroWorkspaceBack(style))
        {
            color = ChromeBackgroundColor;
            return true;
        }

        return false;
    }

    private bool TryGetRetroListItemBackColor(PaletteBackStyle style, PaletteState state, out Color color)
    {
        color = GlobalStaticVariables.EMPTY_COLOR;

        if (style != PaletteBackStyle.ButtonListItem
            || CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
        {
            return false;
        }

        color = state switch
        {
            PaletteState.Tracking or PaletteState.CheckedTracking or PaletteState.CheckedNormal
                or PaletteState.CheckedPressed or PaletteState.Pressed => ButtonFaceColor,
            PaletteState.Disabled => ButtonDisabledColor,
            _ => GetRetroListItemNormalBackColor()
        };
        return true;
    }

    private bool TryGetRetroListItemTextColor(PaletteContentStyle style, PaletteState state, out Color color)
    {
        color = GlobalStaticVariables.EMPTY_COLOR;

        if (style != PaletteContentStyle.ButtonListItem
            || CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
        {
            return false;
        }

        color = state switch
        {
            PaletteState.Tracking or PaletteState.CheckedTracking or PaletteState.CheckedNormal
                or PaletteState.CheckedPressed or PaletteState.Pressed => ButtonNormalTextColor,
            PaletteState.Disabled => Color.FromArgb(128, 128, 128),
            _ => GetRetroListItemNormalTextColor()
        };
        return true;
    }

    private bool TryGetRetroCommandButtonTextColor(PaletteContentStyle style, PaletteState state, out Color color)
    {
        color = GlobalStaticVariables.EMPTY_COLOR;

        if (style != PaletteContentStyle.ButtonCommand
            || CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
        {
            return false;
        }

        color = state == PaletteState.Disabled ? Color.FromArgb(64, 64, 64) : GetRetroCommandButtonTextColor();
        return true;
    }

    private Color EnsureReadableOnWorkspace(Color color, PaletteState state)
    {
        if (state == PaletteState.Disabled || color.IsEmpty || color == GlobalStaticVariables.EMPTY_COLOR)
        {
            return color;
        }

        return color.R + color.G + color.B < 384 ? WorkspaceTextColor : color;
    }

    protected virtual Color GetRetroListItemNormalBackColor() => Color.White;

    protected virtual Color GetRetroListItemNormalTextColor() => Color.Black;
}
