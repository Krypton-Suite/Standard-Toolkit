#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2026. All rights reserved.
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
        ButtonNormalTextColor = Color.White;
    }

    /// <summary>Gets the workspace chrome color behind retro push buttons.</summary>
    public Color ChromeBackgroundColor { get; }

    /// <summary>Gets the flat push-button face color.</summary>
    public Color ButtonFaceColor { get; }

    /// <summary>Gets the disabled push-button / list-item color.</summary>
    public Color ButtonDisabledColor { get; }

    /// <summary>Gets the text color on retro push buttons.</summary>
    protected Color ButtonNormalTextColor { get; set; }

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

        if (RetroRenderHelper.UsesRetroPushButtonChrome(style))
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
        if (IsRetroButtonBorder(style, state))
        {
            return InheritBool.False;
        }

        return base.GetBorderDraw(style, state);
    }

    public override int GetBorderWidth(PaletteBorderStyle style, PaletteState state)
    {
        if (IsRetroButtonBorder(style, state))
        {
            return 0;
        }

        return base.GetBorderWidth(style, state);
    }

    public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
    {
        if (TryGetRetroListItemTextColor(style, state, out Color listColor))
        {
            return listColor;
        }

        if (IsRetroButtonContent(style, state))
        {
            return state == PaletteState.Disabled ? Color.FromArgb(64, 64, 64) : ButtonNormalTextColor;
        }

        return base.GetContentShortTextColor1(style, state);
    }

    public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
    {
        if (TryGetRetroListItemTextColor(style, state, out Color listColor))
        {
            return listColor;
        }

        if (IsRetroButtonContent(style, state))
        {
            return state == PaletteState.Disabled ? Color.FromArgb(64, 64, 64) : ButtonNormalTextColor;
        }

        return base.GetContentShortTextColor2(style, state);
    }

    private static bool IsRetroButtonBorder(PaletteBorderStyle style, PaletteState state) =>
        !CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride)
        && RetroRenderHelper.UsesRetroPushButtonChrome(style);

    private static bool IsRetroButtonContent(PaletteContentStyle style, PaletteState state) =>
        !CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride)
        && RetroRenderHelper.UsesRetroPushButtonChrome(style);

    private static bool IsRetroButton(PaletteBackStyle style, PaletteState state) =>
        !CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride)
        && RetroRenderHelper.UsesRetroPushButtonChrome(style);

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

    protected virtual Color GetRetroListItemNormalBackColor() => Color.White;

    protected virtual Color GetRetroListItemNormalTextColor() => Color.Black;
}
