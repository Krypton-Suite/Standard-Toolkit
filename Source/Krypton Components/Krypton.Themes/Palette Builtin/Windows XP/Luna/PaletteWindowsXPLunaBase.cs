#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Themes;

/// <summary>
/// Base palette for Windows XP Luna (and Luna-derived Embedded) visual styles.
/// </summary>
public abstract class PaletteWindowsXPLunaBase : PaletteProfessionalSystem
{
    private KryptonColorTable? _lunaColorTable;

    /// <summary>Gets the extracted Windows XP Luna resource palette.</summary>
    protected abstract LunaPalette Luna { get; }

    /// <inheritdoc />
    public override IRenderer GetRenderer() => KryptonManager.RenderWindowsXPLuna;

    /// <inheritdoc />
    public override KryptonColorTable ColorTable =>
        TryGetLunaScheme(out _)
            ? _lunaColorTable ??= new KryptonWindowsXPLunaColorTable(this)
            : base.ColorTable;

    /// <inheritdoc />
    public override Color GetBackColor1(PaletteBackStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
        {
            return SharedStaticVariables.EMPTY_COLOR;
        }

        return style switch
        {
            PaletteBackStyle.ButtonListItem when TryGetLunaScheme(out _) => ListItemColor(state),

            PaletteBackStyle.Control or PaletteBackStyle.ControlClient or PaletteBackStyle.ControlAlternate
                or PaletteBackStyle.ControlCustom1 or PaletteBackStyle.ControlCustom2 or PaletteBackStyle.ControlCustom3
                => state == PaletteState.Disabled ? Luna.ControlDisabled : Luna.Control,

            PaletteBackStyle.PanelClient or PaletteBackStyle.PanelAlternate or PaletteBackStyle.PanelCustom1
                or PaletteBackStyle.PanelCustom2 or PaletteBackStyle.PanelCustom3
                => Luna.Control,

            PaletteBackStyle.InputControlStandalone or PaletteBackStyle.InputControlRibbon
                or PaletteBackStyle.InputControlCustom1 or PaletteBackStyle.InputControlCustom2
                or PaletteBackStyle.InputControlCustom3
                => state == PaletteState.Disabled ? Luna.ControlDisabled : Luna.EditFill,

            PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonGallery or PaletteBackStyle.ButtonAlternate
                or PaletteBackStyle.ButtonLowProfile or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem
                or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonCluster
                or PaletteBackStyle.ButtonNavigatorStack or PaletteBackStyle.ButtonNavigatorOverflow
                or PaletteBackStyle.ButtonNavigatorMini or PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose
                or PaletteBackStyle.ButtonCustom1 or PaletteBackStyle.ButtonCustom2 or PaletteBackStyle.ButtonCustom3
                or PaletteBackStyle.ButtonInputControl or PaletteBackStyle.ButtonCalendarDay
                => GetButtonBackColor(state),

            PaletteBackStyle.ContextMenuOuter or PaletteBackStyle.ContextMenuInner
                => Luna.Control,

            PaletteBackStyle.ContextMenuItemHighlight when TryGetLunaScheme(out _)
                => state is PaletteState.Tracking or PaletteState.CheckedTracking or PaletteState.CheckedNormal
                    ? Luna.Highlight
                    : SharedStaticVariables.EMPTY_COLOR,

            PaletteBackStyle.ContextMenuItemHighlight
                => state is PaletteState.Tracking or PaletteState.CheckedTracking ? Luna.ButtonTracking : SharedStaticVariables.EMPTY_COLOR,

            PaletteBackStyle.ContextMenuItemImageColumn => Luna.Control,

            PaletteBackStyle.FormMain or PaletteBackStyle.FormCustom1 or PaletteBackStyle.FormCustom2
                or PaletteBackStyle.FormCustom3 or PaletteBackStyle.HeaderForm
                => state == PaletteState.Disabled ? Luna.InactiveCaptionFill2 : Luna.CaptionFill2,

            PaletteBackStyle.HeaderPrimary or PaletteBackStyle.HeaderSecondary or PaletteBackStyle.HeaderCustom1
                or PaletteBackStyle.HeaderCustom2 or PaletteBackStyle.HeaderCustom3
                => Luna.Control,

            PaletteBackStyle.GridDataCellList or PaletteBackStyle.GridDataCellSheet
                or PaletteBackStyle.GridDataCellCustom1 or PaletteBackStyle.GridDataCellCustom2
                or PaletteBackStyle.GridDataCellCustom3
                => Luna.EditFill,

            PaletteBackStyle.GridHeaderColumnList or PaletteBackStyle.GridHeaderColumnSheet
                or PaletteBackStyle.GridHeaderColumnCustom1 or PaletteBackStyle.GridHeaderColumnCustom2
                or PaletteBackStyle.GridHeaderColumnCustom3 or PaletteBackStyle.GridHeaderRowList
                or PaletteBackStyle.GridHeaderRowSheet or PaletteBackStyle.GridHeaderRowCustom1
                or PaletteBackStyle.GridHeaderRowCustom2 or PaletteBackStyle.GridHeaderRowCustom3
                => Luna.Control,

            PaletteBackStyle.ControlToolTip => Luna.ToolTipFill,
            _ => base.GetBackColor1(style, state)
        };
    }

    /// <inheritdoc />
    public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
        {
            return SharedStaticVariables.EMPTY_COLOR;
        }

        return style switch
        {
            PaletteBackStyle.ButtonListItem when TryGetLunaScheme(out _) => ListItemColor(state),

            PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonGallery or PaletteBackStyle.ButtonAlternate
                or PaletteBackStyle.ButtonLowProfile or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem
                or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonCluster
                or PaletteBackStyle.ButtonNavigatorStack or PaletteBackStyle.ButtonNavigatorOverflow
                or PaletteBackStyle.ButtonNavigatorMini or PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose
                or PaletteBackStyle.ButtonCustom1 or PaletteBackStyle.ButtonCustom2 or PaletteBackStyle.ButtonCustom3
                or PaletteBackStyle.ButtonInputControl or PaletteBackStyle.ButtonCalendarDay
                => GetButtonBackColor2(state),
            PaletteBackStyle.FormMain or PaletteBackStyle.FormCustom1 or PaletteBackStyle.FormCustom2
                or PaletteBackStyle.FormCustom3 or PaletteBackStyle.HeaderForm
                => state == PaletteState.Disabled ? Luna.InactiveCaptionFill : Luna.CaptionFill,
            PaletteBackStyle.ControlToolTip => Luna.ToolTipFill,
            _ => base.GetBackColor2(style, state)
        };
    }

    /// <inheritdoc />
    public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteColorStyle.Inherit;
        }

        return style switch
        {
            PaletteBackStyle.ButtonListItem when TryGetLunaScheme(out _) => PaletteColorStyle.Solid,

            PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonGallery or PaletteBackStyle.ButtonAlternate
                or PaletteBackStyle.ButtonLowProfile or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem
                or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonCluster
                or PaletteBackStyle.ButtonNavigatorStack or PaletteBackStyle.ButtonNavigatorOverflow
                or PaletteBackStyle.ButtonNavigatorMini or PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose
                or PaletteBackStyle.ButtonCustom1 or PaletteBackStyle.ButtonCustom2 or PaletteBackStyle.ButtonCustom3
                or PaletteBackStyle.ButtonInputControl or PaletteBackStyle.ButtonCalendarDay
                => PaletteColorStyle.Linear,
            PaletteBackStyle.FormMain or PaletteBackStyle.FormCustom1 or PaletteBackStyle.FormCustom2
                or PaletteBackStyle.FormCustom3 or PaletteBackStyle.HeaderForm
                or PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonGallery or PaletteBackStyle.ButtonAlternate
                or PaletteBackStyle.ButtonLowProfile or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem
                or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonCluster
                or PaletteBackStyle.ButtonNavigatorStack or PaletteBackStyle.ButtonNavigatorOverflow
                or PaletteBackStyle.ButtonNavigatorMini or PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose
                or PaletteBackStyle.ButtonCustom1 or PaletteBackStyle.ButtonCustom2 or PaletteBackStyle.ButtonCustom3
                or PaletteBackStyle.ButtonInputControl or PaletteBackStyle.ButtonCalendarDay
                => PaletteColorStyle.Linear,
            PaletteBackStyle.ContextMenuItemHighlight or PaletteBackStyle.ControlToolTip
                => PaletteColorStyle.Solid,
            _ => base.GetBackColorStyle(style, state)
        };
    }

    /// <inheritdoc />
    public override Color GetBorderColor1(PaletteBorderStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
        {
            return SharedStaticVariables.EMPTY_COLOR;
        }

        return style switch
        {
            PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate
                or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem
                or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCluster
                or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow
                or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose
                or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3
                or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ButtonCalendarDay
                => state == PaletteState.Disabled ? Luna.DisabledText : Luna.ButtonBorder,

            PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon
                or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2
                or PaletteBorderStyle.InputControlCustom3
                => state == PaletteState.Disabled ? Luna.DisabledText : Luna.ControlBorder,

            PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox
                or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3
                or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner
                or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator
                or PaletteBorderStyle.ContextMenuItemSplit
                or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2
                or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderForm
                => Luna.EdgeShadow,
            _ => base.GetBorderColor1(style, state)
        };
    }

    /// <inheritdoc />
    public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
        {
            return SharedStaticVariables.EMPTY_COLOR;
        }

        return style switch
        {
            PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate
                or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem
                or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCluster
                or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow
                or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose
                or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3
                or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ButtonCalendarDay
                => state == PaletteState.Disabled ? Luna.DisabledText : Luna.EdgeHighlight,

            PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon
                or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2
                or PaletteBorderStyle.InputControlCustom3
                => state == PaletteState.Disabled ? Luna.DisabledText : Luna.EdgeHighlight,
            _ => base.GetBorderColor2(style, state)
        };
    }

    /// <inheritdoc />
    public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            return base.GetContentShortTextColor1(style, state);
        }

        if (TryGetLunaScheme(out _))
        {
            if (style == PaletteContentStyle.HeaderForm)
            {
                return state == PaletteState.Disabled ? Luna.InactiveCaptionText : Luna.CaptionText;
            }

            if (style == PaletteContentStyle.LabelGroupBoxCaption)
            {
                return Luna.GroupBoxText;
            }

            if (IsHighlightText(style, state))
            {
                return Luna.HighlightText;
            }
        }

        if (state == PaletteState.Disabled)
        {
            return Luna.DisabledText;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderSecondary
                or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2
                or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.HeaderForm
                => Luna.CaptionText,
            PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip
                => Luna.ToolTipText,
            _ => Luna.Text
        };
    }

    /// <inheritdoc />
    public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state) =>
        GetContentShortTextColor1(style, state);

    /// <inheritdoc />
    public override Image? GetBackImage(PaletteBackStyle style, PaletteState state)
    {
        if (!TryGetLunaScheme(out WindowsXPLunaScheme scheme) || CommonHelper.IsOverrideState(state))
        {
            return base.GetBackImage(style, state);
        }

        var part = BackPart(style);
        return part.HasValue ? WindowsXPLunaArt.Sheet(scheme, part.Value) : base.GetBackImage(style, state);
    }

    /// <inheritdoc />
    public override Image? GetBorderImage(PaletteBorderStyle style, PaletteState state)
    {
        if (!TryGetLunaScheme(out WindowsXPLunaScheme scheme) || CommonHelper.IsOverrideState(state))
        {
            return base.GetBorderImage(style, state);
        }

        return IsFormFrame(style)
            ? WindowsXPLunaArt.Sheet(scheme, WindowsXPLunaPart.FrameLeft)
            : base.GetBorderImage(style, state);
    }

    /// <inheritdoc />
    public override PaletteCornerRounding GetBorderCornerRounding(PaletteBorderStyle style, PaletteState state)
    {
        // The caption sheet's rounded corner is about six pixels. The bottom corners stay square.
        if (IsFormFrame(style) && TryGetLunaScheme(out _))
        {
            return new PaletteCornerRounding(6f, 6f, 0f, 0f);
        }

        return base.GetBorderCornerRounding(style, state);
    }

    /// <inheritdoc />
    public override int GetBorderWidth(PaletteBorderStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            return -1;
        }

        if (!TryGetLunaScheme(out _))
        {
            return base.GetBorderWidth(style, state);
        }

        if (IsFormFrame(style))
        {
            return WindowsXPLunaArt.FrameThickness;
        }

        if (style == PaletteBorderStyle.HeaderForm || IsSpriteBorder(style))
        {
            return 0;
        }

        return base.GetBorderWidth(style, state);
    }

    /// <inheritdoc />
    public override Image? GetCheckBoxImage(bool enabled, CheckState checkState, bool tracking, bool pressed) =>
        TryGetLunaScheme(out WindowsXPLunaScheme scheme)
            ? WindowsXPLunaArt.CheckBox(scheme, enabled, checkState, tracking, pressed)
            : base.GetCheckBoxImage(enabled, checkState, tracking, pressed);

    /// <inheritdoc />
    public override Image? GetRadioButtonImage(bool enabled, bool checkState, bool tracking, bool pressed) =>
        TryGetLunaScheme(out WindowsXPLunaScheme scheme)
            ? WindowsXPLunaArt.Radio(scheme, enabled, checkState, tracking, pressed)
            : base.GetRadioButtonImage(enabled, checkState, tracking, pressed);

    /// <inheritdoc />
    public override Image? GetButtonSpecImage(PaletteButtonSpecStyle style, PaletteState state) =>
        TryGetLunaScheme(out WindowsXPLunaScheme scheme) && GlyphPart(style) is WindowsXPLunaPart part
            ? WindowsXPLunaArt.Glyph(scheme, part, state)
            : base.GetButtonSpecImage(style, state);

    private bool TryGetLunaScheme(out WindowsXPLunaScheme scheme)
    {
        if (this is IWindowsXPLunaScheme luna)
        {
            scheme = luna.Scheme;
            return true;
        }

        scheme = default;
        return false;
    }

    private Color ListItemColor(PaletteState state) =>
        state is PaletteState.Tracking or PaletteState.Pressed or PaletteState.CheckedNormal
            or PaletteState.CheckedTracking or PaletteState.CheckedPressed
            ? Luna.Highlight
            : SharedStaticVariables.EMPTY_COLOR;

    private static bool IsHighlightText(PaletteContentStyle style, PaletteState state)
    {
        if (state is not (PaletteState.Tracking or PaletteState.Pressed or PaletteState.CheckedNormal
            or PaletteState.CheckedTracking or PaletteState.CheckedPressed))
        {
            return false;
        }

        return style is PaletteContentStyle.ButtonListItem
            or PaletteContentStyle.ContextMenuItemTextStandard
            or PaletteContentStyle.ContextMenuItemTextAlternate
            or PaletteContentStyle.ContextMenuItemShortcutText;
    }

    private static WindowsXPLunaPart? BackPart(PaletteBackStyle style) => style switch
    {
        PaletteBackStyle.HeaderForm => WindowsXPLunaPart.Caption,
        PaletteBackStyle.ButtonForm => WindowsXPLunaPart.CaptionButton,
        PaletteBackStyle.ButtonFormClose => WindowsXPLunaPart.CloseButton,
        PaletteBackStyle.ButtonInputControl => WindowsXPLunaPart.ComboButton,
        PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonGallery or PaletteBackStyle.ButtonAlternate
            or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonCluster
            or PaletteBackStyle.ButtonNavigatorStack or PaletteBackStyle.ButtonNavigatorOverflow
            or PaletteBackStyle.ButtonNavigatorMini or PaletteBackStyle.ButtonCalendarDay
            or PaletteBackStyle.ButtonCustom1 or PaletteBackStyle.ButtonCustom2 or PaletteBackStyle.ButtonCustom3
            => WindowsXPLunaPart.Button,
        _ => null
    };

    private static bool IsFormFrame(PaletteBorderStyle style) =>
        style is PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1
            or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3;

    private static bool IsSpriteBorder(PaletteBorderStyle style) =>
        style is PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery
            or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonCommand
            or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack
            or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini
            or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonCustom1
            or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3
            or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose
            or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ButtonListItem;

    private static WindowsXPLunaPart? GlyphPart(PaletteButtonSpecStyle style) => style switch
    {
        PaletteButtonSpecStyle.FormMin => WindowsXPLunaPart.MinimizeGlyph,
        PaletteButtonSpecStyle.FormMax => WindowsXPLunaPart.MaximizeGlyph,
        PaletteButtonSpecStyle.FormRestore => WindowsXPLunaPart.RestoreGlyph,
        PaletteButtonSpecStyle.FormClose => WindowsXPLunaPart.CloseGlyph,
        PaletteButtonSpecStyle.FormHelp => WindowsXPLunaPart.HelpGlyph,
        _ => null
    };

    private Color GetButtonBackColor(PaletteState state) => state switch
    {
        PaletteState.Disabled => Luna.ControlDisabled,
        PaletteState.Tracking or PaletteState.CheckedTracking => Luna.ButtonTracking,
        PaletteState.Pressed or PaletteState.CheckedPressed => Luna.ButtonPressed,
        PaletteState.CheckedNormal => Luna.ButtonChecked,
        _ => Luna.ButtonFace
    };

    private Color GetButtonBackColor2(PaletteState state) => state switch
    {
        PaletteState.Disabled => Luna.ControlDisabled,
        PaletteState.Tracking or PaletteState.CheckedTracking => Luna.ButtonTracking2,
        PaletteState.Pressed or PaletteState.CheckedPressed => Luna.ButtonPressed2,
        PaletteState.CheckedNormal => Luna.ButtonChecked2,
        _ => Luna.ButtonFace2
    };
}
