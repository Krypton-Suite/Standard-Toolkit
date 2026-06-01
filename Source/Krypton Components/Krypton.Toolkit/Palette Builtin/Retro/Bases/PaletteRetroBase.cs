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
    private Font _retroControlFont;

    protected PaletteRetroBase(
        KryptonColorSchemeBase scheme,
        ImageList checkBoxList,
        ImageList galleryButtonList,
        Image?[] radioButtonArray)
        : base(scheme, checkBoxList, galleryButtonList, radioButtonArray)
    {
        _retroControlFont = CreateDefaultRetroControlFont();
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

    /// <summary>Gets or sets the font used for retro-styled control text.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DisallowNull]
    public Font RetroControlFont
    {
        get => _retroControlFont;
        set
        {
            var retroControlFont = new Font(value, value.Style);
            _retroControlFont.Dispose();
            _retroControlFont = retroControlFont;
            OnPalettePaint(this, new PaletteLayoutEventArgs(true, false));
        }
    }

    /// <summary>Gets the preferred text color on retro workspace surfaces.</summary>
    protected virtual Color WorkspaceTextColor => Color.Black;

    /// <summary>Gets the background color for retro alternate panels.</summary>
    protected virtual Color PanelAlternateBackColor => ChromeBackgroundColor;

    /// <summary>Gets the border color for retro group boxes.</summary>
    protected virtual Color GroupBoxBorderColor => Color.Black;

    /// <summary>Gets the border color for retro input controls and their adjacent buttons.</summary>
    protected virtual Color InputControlBorderColor => Color.FromArgb(192, 192, 192);

    /// <summary>Gets the background color for retro input controls.</summary>
    protected virtual Color InputControlBackColor => Color.White;

    /// <summary>Gets the text color for retro input controls.</summary>
    protected virtual Color InputControlTextColor => Color.Black;

    /// <summary>Gets the text and glyph color for retro input control buttons.</summary>
    protected virtual Color InputControlButtonTextColor => InputControlTextColor;

    /// <summary>Gets the background color for retro headers.</summary>
    protected virtual Color HeaderBackColor => ButtonDisabledColor;

    /// <summary>Gets the text color for retro headers.</summary>
    protected virtual Color HeaderTextColor => Color.Black;

    /// <summary>Gets the text color for the inherited form header, when overridden by a Retro variant.</summary>
    protected virtual Color FormHeaderTextColor => GlobalStaticVariables.EMPTY_COLOR;

    /// <summary>Gets the line color for retro separator edges.</summary>
    protected virtual Color SeparatorLineColor => Color.White;

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

    public override Font? GetContentShortTextFont(PaletteContentStyle style, PaletteState state)
    {
        if (UsesRetroControlFont(style, state))
        {
            return _retroControlFont;
        }

        return base.GetContentShortTextFont(style, state);
    }

    public override Font? GetContentLongTextFont(PaletteContentStyle style, PaletteState state)
    {
        if (UsesRetroControlFont(style, state))
        {
            return _retroControlFont;
        }

        return base.GetContentLongTextFont(style, state);
    }

    public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteColorStyle.Inherit;
        }

        if (RetroRenderHelper.UsesRetroPushButtonChrome(style)
            || IsRetroGridHeaderBack(style)
            || IsRetroGridDataCellBack(style)
            || IsRetroWorkspaceBack(style)
            || IsRetroInputControlBack(style)
            || IsRetroInputControlButtonBack(style)
            || IsRetroHeaderBack(style)
            || IsRetroSeparatorBack(style))
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

        if (TryGetRetroBackColor(style, state, out Color backColor))
        {
            return backColor;
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

        if (TryGetRetroBackColor(style, state, out Color backColor))
        {
            return backColor;
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
        if (IsRetroForcedFrameBorder(style, state))
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
        if (IsRetroForcedFrameBorder(style, state))
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
        if (IsRetroFrameBorder(style, state))
        {
            return GetRetroFrameBorderColor(style);
        }

        return base.GetBorderColor1(style, state);
    }

    public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
    {
        if (IsRetroFrameBorder(style, state))
        {
            return GetRetroFrameBorderColor(style);
        }

        return base.GetBorderColor2(style, state);
    }

    public override PaletteColorStyle GetBorderColorStyle(PaletteBorderStyle style, PaletteState state)
    {
        if (IsRetroFrameBorder(style, state))
        {
            return PaletteColorStyle.Solid;
        }

        return base.GetBorderColorStyle(style, state);
    }

    public override float GetBorderRounding(PaletteBorderStyle style, PaletteState state)
    {
        if (IsRetroFrameBorder(style, state))
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

        if (TryGetRetroInputControlButtonTextColor(style, state, out Color inputButtonColor))
        {
            return inputButtonColor;
        }

        if (TryGetRetroFormHeaderTextColor(style, state, out Color formHeaderColor))
        {
            return formHeaderColor;
        }

        if (IsRetroButtonContent(style, state))
        {
            return state == PaletteState.Disabled ? Color.FromArgb(64, 64, 64) : ButtonNormalTextColor;
        }

        if (TryGetRetroWorkspaceTextColor(style, state, out Color workspaceColor))
        {
            return workspaceColor;
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

        if (TryGetRetroInputControlButtonTextColor(style, state, out Color inputButtonColor))
        {
            return inputButtonColor;
        }

        if (TryGetRetroFormHeaderTextColor(style, state, out Color formHeaderColor))
        {
            return formHeaderColor;
        }

        if (IsRetroButtonContent(style, state))
        {
            return state == PaletteState.Disabled ? Color.FromArgb(64, 64, 64) : ButtonNormalTextColor;
        }

        if (TryGetRetroWorkspaceTextColor(style, state, out Color workspaceColor))
        {
            return workspaceColor;
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

        if (TryGetRetroInputControlButtonTextColor(style, state, out Color inputButtonColor))
        {
            return inputButtonColor;
        }

        if (TryGetRetroFormHeaderTextColor(style, state, out Color formHeaderColor))
        {
            return formHeaderColor;
        }

        if (TryGetRetroWorkspaceTextColor(style, state, out Color workspaceColor))
        {
            return workspaceColor;
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

        if (TryGetRetroInputControlButtonTextColor(style, state, out Color inputButtonColor))
        {
            return inputButtonColor;
        }

        if (TryGetRetroFormHeaderTextColor(style, state, out Color formHeaderColor))
        {
            return formHeaderColor;
        }

        if (TryGetRetroWorkspaceTextColor(style, state, out Color workspaceColor))
        {
            return workspaceColor;
        }

        Color color = base.GetContentLongTextColor2(style, state);
        return IsRetroWorkspaceContent(style) ? EnsureReadableOnWorkspace(color, state) : color;
    }

    public override Color GetContentImageColorMap(PaletteContentStyle style, PaletteState state)
    {
        return TryGetRetroInputControlButtonTextColor(style, state, out _)
            ? Color.Black
            : base.GetContentImageColorMap(style, state);
    }

    public override Color GetContentImageColorTo(PaletteContentStyle style, PaletteState state)
    {
        return TryGetRetroInputControlButtonTextColor(style, state, out Color inputButtonColor)
            ? inputButtonColor
            : base.GetContentImageColorTo(style, state);
    }

    protected virtual Color GetRetroGridDataCellNormalBackColor() => Color.White;

    protected virtual Color GetRetroGridDataCellNormalTextColor() => Color.Black;

    protected virtual Color GetRetroCommandButtonTextColor() => Color.Black;

    protected virtual Color GetRetroListItemSelectedBackColor() =>
        CommonHelper.MergeColors(ButtonFaceColor, 0.9f, Color.Black, 0.1f);

    protected virtual Color GetRetroListItemDisabledBackColor() => Color.FromArgb(190, 190, 190);

    protected virtual Color GetRetroListItemDisabledTextColor() => Color.FromArgb(96, 96, 96);

    private Color GetRetroFrameBorderColor(PaletteBorderStyle style)
    {
        if (style == PaletteBorderStyle.HeaderPrimary)
        {
            return SeparatorLineColor;
        }

        return IsRetroInputControlBorder(style) || IsRetroInputControlButtonBorder(style)
            ? InputControlBorderColor
            : GroupBoxBorderColor;
    }

    private static bool IsRetroButtonBorder(PaletteBorderStyle style, PaletteState state) =>
        !CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride)
        && RetroRenderHelper.UsesRetroPushButtonChrome(style);

    private static bool IsRetroForcedFrameBorder(PaletteBorderStyle style, PaletteState state) =>
        !CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride)
        && (style == PaletteBorderStyle.ControlGroupBox
            || IsRetroInputControlBorder(style)
            || IsRetroInputControlButtonBorder(style)
            || IsRetroHeaderBorder(style)
            || IsRetroSeparatorBorder(style));

    private static bool IsRetroFrameBorder(PaletteBorderStyle style, PaletteState state) =>
        !CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride)
        && (IsRetroControlBorder(style)
            || IsRetroContextMenuBorder(style)
            || IsRetroInputControlBorder(style)
            || IsRetroInputControlButtonBorder(style)
            || IsRetroGridBorder(style)
            || IsRetroHeaderBorder(style)
            || IsRetroSeparatorBorder(style));

    private static bool IsRetroControlBorder(PaletteBorderStyle style) =>
        style is PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate
            or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip
            or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu
            or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2
            or PaletteBorderStyle.ControlCustom3;

    private static bool IsRetroContextMenuBorder(PaletteBorderStyle style) =>
        style is PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner
            or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator
            or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemSplit
            or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.ContextMenuItemHighlight;

    private static bool IsRetroButtonContent(PaletteContentStyle style, PaletteState state) =>
        !CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride)
        && RetroRenderHelper.UsesRetroPushButtonChrome(style);

    private static bool IsRetroButton(PaletteBackStyle style, PaletteState state) =>
        !CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride)
        && RetroRenderHelper.UsesRetroPushButtonChrome(style);

    private static bool UsesRetroControlFont(PaletteContentStyle style, PaletteState state) =>
        !CommonHelper.IsOverrideState(state)
        && (IsRetroControlButtonContent(style)
            || IsRetroInputControlContent(style)
            || IsRetroInputControlButtonContent(style)
            || IsRetroLabelContent(style));

    private static bool IsRetroControlButtonContent(PaletteContentStyle style) =>
        style is PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery
            or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile
            or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem
            or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonCluster
            or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2
            or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl;

    private static Font CreateDefaultRetroControlFont() => new Font("Courier New", 10f, FontStyle.Regular);

    private static bool IsRetroGridHeaderBack(PaletteBackStyle style) =>
        style is PaletteBackStyle.GridHeaderColumnList or PaletteBackStyle.GridHeaderColumnCustom1
            or PaletteBackStyle.GridHeaderColumnCustom2 or PaletteBackStyle.GridHeaderColumnCustom3
            or PaletteBackStyle.GridHeaderRowList or PaletteBackStyle.GridHeaderRowCustom1
            or PaletteBackStyle.GridHeaderRowCustom2 or PaletteBackStyle.GridHeaderRowCustom3
            or PaletteBackStyle.GridHeaderColumnSheet or PaletteBackStyle.GridHeaderRowSheet;

    private static bool IsRetroGridDataCellBack(PaletteBackStyle style) =>
        style is PaletteBackStyle.GridDataCellList or PaletteBackStyle.GridDataCellCustom1
            or PaletteBackStyle.GridDataCellCustom2 or PaletteBackStyle.GridDataCellCustom3
            or PaletteBackStyle.GridDataCellSheet;

    private static bool IsRetroGridBackgroundBack(PaletteBackStyle style) =>
        style is PaletteBackStyle.GridBackgroundList or PaletteBackStyle.GridBackgroundSheet
            or PaletteBackStyle.GridBackgroundCustom1 or PaletteBackStyle.GridBackgroundCustom2
            or PaletteBackStyle.GridBackgroundCustom3;

    private static bool IsRetroInputControlBack(PaletteBackStyle style) =>
        style is PaletteBackStyle.InputControlStandalone or PaletteBackStyle.InputControlRibbon
            or PaletteBackStyle.InputControlCustom1 or PaletteBackStyle.InputControlCustom2
            or PaletteBackStyle.InputControlCustom3;

    private static bool IsRetroInputControlButtonBack(PaletteBackStyle style) =>
        style is PaletteBackStyle.ButtonInputControl or PaletteBackStyle.ButtonButtonSpec;

    private static bool IsRetroHeaderBack(PaletteBackStyle style) =>
        style is PaletteBackStyle.HeaderPrimary or PaletteBackStyle.HeaderSecondary
            or PaletteBackStyle.HeaderDockInactive or PaletteBackStyle.HeaderDockActive
            or PaletteBackStyle.HeaderCalendar
            or PaletteBackStyle.HeaderCustom1 or PaletteBackStyle.HeaderCustom2
            or PaletteBackStyle.HeaderCustom3;

    private static bool IsRetroSeparatorBack(PaletteBackStyle style) =>
        style is PaletteBackStyle.SeparatorLowProfile or PaletteBackStyle.SeparatorHighProfile
            or PaletteBackStyle.SeparatorHighInternalProfile or PaletteBackStyle.SeparatorCustom1
            or PaletteBackStyle.SeparatorCustom2 or PaletteBackStyle.SeparatorCustom3;

    private static bool IsRetroInputControlBorder(PaletteBorderStyle style) =>
        style is PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon
            or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2
            or PaletteBorderStyle.InputControlCustom3;

    private static bool IsRetroInputControlButtonBorder(PaletteBorderStyle style) =>
        style is PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ButtonButtonSpec;

    private static bool IsRetroGridBorder(PaletteBorderStyle style) =>
        style is PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet
            or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2
            or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList
            or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1
            or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3
            or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet
            or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2
            or PaletteBorderStyle.GridDataCellCustom3;

    private static bool IsRetroHeaderBorder(PaletteBorderStyle style) =>
        style is PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderSecondary
            or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive
            or PaletteBorderStyle.HeaderCalendar
            or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2
            or PaletteBorderStyle.HeaderCustom3;

    private static bool IsRetroSeparatorBorder(PaletteBorderStyle style) =>
        style is PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighProfile
            or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorCustom1
            or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3;

    private static bool IsRetroGridContent(PaletteContentStyle style) =>
        style is PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnCustom1
            or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3
            or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowCustom1
            or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3
            or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellCustom1
            or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3
            or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderRowSheet
            or PaletteContentStyle.GridDataCellSheet;

    private static bool IsRetroGridHeaderContent(PaletteContentStyle style) =>
        style is PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet
            or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2
            or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList
            or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1
            or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3;

    private static bool IsRetroGridDataCellContent(PaletteContentStyle style, PaletteState state) =>
        !CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride)
        && style is PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellCustom1
            or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3
            or PaletteContentStyle.GridDataCellSheet;

    private static bool IsRetroInputControlContent(PaletteContentStyle style) =>
        style is PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon
            or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2
            or PaletteContentStyle.InputControlCustom3;

    private static bool IsRetroInputControlButtonContent(PaletteContentStyle style) =>
        style is PaletteContentStyle.ButtonInputControl or PaletteContentStyle.ButtonButtonSpec;

    private static bool IsRetroHeaderContent(PaletteContentStyle style) =>
        style is PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderSecondary
            or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive
            or PaletteContentStyle.HeaderCalendar
            or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2
            or PaletteContentStyle.HeaderCustom3;

    private static bool IsRetroContextMenuContent(PaletteContentStyle style) =>
        style is PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage
            or PaletteContentStyle.ContextMenuItemTextStandard
            or PaletteContentStyle.ContextMenuItemTextAlternate
            or PaletteContentStyle.ContextMenuItemShortcutText;

    private static bool IsRetroLabelContent(PaletteContentStyle style) =>
        style is PaletteContentStyle.LabelAlternateControl or PaletteContentStyle.LabelNormalControl
            or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl
            or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelAlternatePanel
            or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel
            or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel
            or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip
            or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip
            or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2
            or PaletteContentStyle.LabelCustom3;

    private static bool IsRetroWorkspaceContent(PaletteContentStyle style) =>
        IsRetroGridContent(style) || IsRetroInputControlContent(style) || IsRetroHeaderContent(style)
        || IsRetroContextMenuContent(style) || IsRetroLabelContent(style);

    private static bool IsRetroWorkspaceBack(PaletteBackStyle style) =>
        style is PaletteBackStyle.ContextMenuOuter or PaletteBackStyle.ContextMenuInner
            or PaletteBackStyle.ContextMenuHeading or PaletteBackStyle.ContextMenuSeparator
            or PaletteBackStyle.ContextMenuItemImage or PaletteBackStyle.ContextMenuItemSplit
            or PaletteBackStyle.ContextMenuItemImageColumn or PaletteBackStyle.ContextMenuItemHighlight
            or PaletteBackStyle.PanelClient or PaletteBackStyle.PanelAlternate
            or PaletteBackStyle.PanelRibbonInactive or PaletteBackStyle.PanelCustom1
            or PaletteBackStyle.PanelCustom2 or PaletteBackStyle.PanelCustom3
            or PaletteBackStyle.ControlClient or PaletteBackStyle.ControlAlternate
            or PaletteBackStyle.ControlGroupBox or PaletteBackStyle.ControlToolTip
            or PaletteBackStyle.ControlRibbon or PaletteBackStyle.ControlRibbonAppMenu
            or PaletteBackStyle.ControlCustom1 or PaletteBackStyle.ControlCustom2
            or PaletteBackStyle.ControlCustom3;

    private bool TryGetRetroBackColor(PaletteBackStyle style, PaletteState state, out Color color)
    {
        if (TryGetRetroGroupBoxBackColor(style, out color))
        {
            return true;
        }

        color = GlobalStaticVariables.EMPTY_COLOR;

        if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
        {
            return false;
        }

        if (style == PaletteBackStyle.PanelAlternate)
        {
            color = PanelAlternateBackColor;
            return true;
        }

        if (TryGetRetroGridBackColor(style, state, out color))
        {
            return true;
        }

        if (IsRetroInputControlBack(style))
        {
            color = state == PaletteState.Disabled ? ButtonDisabledColor : InputControlBackColor;
            return true;
        }

        if (IsRetroInputControlButtonBack(style))
        {
            color = state == PaletteState.Disabled ? ButtonDisabledColor : ButtonFaceColor;
            return true;
        }

        if (IsRetroHeaderBack(style))
        {
            color = state switch
            {
                PaletteState.Pressed or PaletteState.Tracking or PaletteState.CheckedNormal
                    or PaletteState.CheckedTracking or PaletteState.CheckedPressed => ButtonFaceColor,
                PaletteState.Disabled => ButtonDisabledColor,
                _ => HeaderBackColor
            };
            return true;
        }

        if (IsRetroSeparatorBack(style))
        {
            color = GroupBoxBorderColor;
            return true;
        }

        return false;
    }

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

        if (IsRetroGridBackgroundBack(style) || IsRetroWorkspaceBack(style))
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
                or PaletteState.CheckedPressed or PaletteState.Pressed => GetRetroListItemSelectedBackColor(),
            PaletteState.Disabled => GetRetroListItemDisabledBackColor(),
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
            PaletteState.Disabled => GetRetroListItemDisabledTextColor(),
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

    private bool TryGetRetroInputControlButtonTextColor(PaletteContentStyle style, PaletteState state, out Color color)
    {
        color = GlobalStaticVariables.EMPTY_COLOR;

        if (!IsRetroInputControlButtonContent(style)
            || CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
        {
            return false;
        }

        color = state == PaletteState.Disabled ? Color.FromArgb(128, 128, 128) : InputControlButtonTextColor;
        return true;
    }

    private bool TryGetRetroFormHeaderTextColor(PaletteContentStyle style, PaletteState state, out Color color)
    {
        color = GlobalStaticVariables.EMPTY_COLOR;

        if (style != PaletteContentStyle.HeaderForm
            || CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride)
            || FormHeaderTextColor == GlobalStaticVariables.EMPTY_COLOR)
        {
            return false;
        }

        color = FormHeaderTextColor;
        return true;
    }

    private bool TryGetRetroWorkspaceTextColor(PaletteContentStyle style, PaletteState state, out Color color)
    {
        color = GlobalStaticVariables.EMPTY_COLOR;

        if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
        {
            return false;
        }

        if (state == PaletteState.Disabled)
        {
            color = Color.FromArgb(128, 128, 128);
            return IsRetroWorkspaceContent(style);
        }

        if (IsRetroGridDataCellContent(style, state))
        {
            color = state == PaletteState.CheckedNormal ? Color.Black : GetRetroGridDataCellNormalTextColor();
            return true;
        }

        if (IsRetroGridHeaderContent(style) || IsRetroHeaderContent(style))
        {
            color = HeaderTextColor;
            return true;
        }

        if (IsRetroInputControlContent(style))
        {
            color = InputControlTextColor;
            return true;
        }

        if (IsRetroContextMenuContent(style))
        {
            color = state switch
            {
                PaletteState.Tracking or PaletteState.CheckedTracking or PaletteState.CheckedNormal
                    or PaletteState.CheckedPressed or PaletteState.Pressed => ButtonNormalTextColor,
                _ => WorkspaceTextColor
            };
            return true;
        }

        if (IsRetroLabelContent(style))
        {
            color = WorkspaceTextColor;
            return true;
        }

        return false;
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

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _retroControlFont.Dispose();
        }

        base.Dispose(disposing);
    }
}
