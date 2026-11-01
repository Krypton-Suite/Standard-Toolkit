#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Themes;

/// <summary>
/// Base palette for Windows XP Royale (and Royale-derived) visual styles.
/// </summary>
public abstract class PaletteWindowsXPRoyaleBase : PaletteProfessionalSystem
{
    /// <summary>Gets the extracted Windows XP Royale resource palette.</summary>
    protected abstract RoyalePalette Royale { get; }

    /// <inheritdoc />
    public override IRenderer GetRenderer() => KryptonManager.RenderWindowsXPRoyale;

    /// <inheritdoc />
    public override Color GetBackColor1(PaletteBackStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
        {
            return SharedStaticVariables.EMPTY_COLOR;
        }

        return style switch
        {
            PaletteBackStyle.Control or PaletteBackStyle.ControlClient or PaletteBackStyle.ControlAlternate
                or PaletteBackStyle.ControlCustom1 or PaletteBackStyle.ControlCustom2 or PaletteBackStyle.ControlCustom3
                => state == PaletteState.Disabled ? Royale.ControlDisabled : Royale.Control,

            PaletteBackStyle.PanelClient or PaletteBackStyle.PanelAlternate or PaletteBackStyle.PanelCustom1
                or PaletteBackStyle.PanelCustom2 or PaletteBackStyle.PanelCustom3
                => Royale.Control,

            PaletteBackStyle.InputControlStandalone or PaletteBackStyle.InputControlRibbon
                or PaletteBackStyle.InputControlCustom1 or PaletteBackStyle.InputControlCustom2
                or PaletteBackStyle.InputControlCustom3
                => state == PaletteState.Disabled ? Royale.ControlDisabled : Royale.EditFill,

            PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonGallery or PaletteBackStyle.ButtonAlternate
                or PaletteBackStyle.ButtonLowProfile or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem
                or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonCluster
                or PaletteBackStyle.ButtonNavigatorStack or PaletteBackStyle.ButtonNavigatorOverflow
                or PaletteBackStyle.ButtonNavigatorMini or PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose
                or PaletteBackStyle.ButtonCustom1 or PaletteBackStyle.ButtonCustom2 or PaletteBackStyle.ButtonCustom3
                or PaletteBackStyle.ButtonInputControl or PaletteBackStyle.ButtonCalendarDay
                => GetButtonBackColor(state),

            PaletteBackStyle.ContextMenuOuter or PaletteBackStyle.ContextMenuInner
                => Royale.Control,

            PaletteBackStyle.ContextMenuItemHighlight
                => state is PaletteState.Tracking or PaletteState.CheckedTracking ? Royale.ButtonTracking : SharedStaticVariables.EMPTY_COLOR,

            PaletteBackStyle.ContextMenuItemImageColumn => Royale.Control,

            PaletteBackStyle.FormMain or PaletteBackStyle.FormCustom1 or PaletteBackStyle.FormCustom2
                or PaletteBackStyle.FormCustom3 or PaletteBackStyle.HeaderForm
                => Royale.CaptionFill,

            PaletteBackStyle.HeaderPrimary or PaletteBackStyle.HeaderSecondary or PaletteBackStyle.HeaderCustom1
                or PaletteBackStyle.HeaderCustom2 or PaletteBackStyle.HeaderCustom3
                => Royale.Control,

            PaletteBackStyle.GridDataCellList or PaletteBackStyle.GridDataCellSheet
                or PaletteBackStyle.GridDataCellCustom1 or PaletteBackStyle.GridDataCellCustom2
                or PaletteBackStyle.GridDataCellCustom3
                => Royale.EditFill,

            PaletteBackStyle.GridHeaderColumnList or PaletteBackStyle.GridHeaderColumnSheet
                or PaletteBackStyle.GridHeaderColumnCustom1 or PaletteBackStyle.GridHeaderColumnCustom2
                or PaletteBackStyle.GridHeaderColumnCustom3 or PaletteBackStyle.GridHeaderRowList
                or PaletteBackStyle.GridHeaderRowSheet or PaletteBackStyle.GridHeaderRowCustom1
                or PaletteBackStyle.GridHeaderRowCustom2 or PaletteBackStyle.GridHeaderRowCustom3
                => Royale.Control,

            PaletteBackStyle.ControlToolTip => Royale.ToolTipFill,
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
                => Royale.CaptionFill,
            PaletteBackStyle.ControlToolTip => Royale.ToolTipFill,
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
            PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonGallery or PaletteBackStyle.ButtonAlternate
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
                => state == PaletteState.Disabled ? Royale.DisabledText : Royale.ButtonBorder,

            PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon
                or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2
                or PaletteBorderStyle.InputControlCustom3
                => state == PaletteState.Disabled ? Royale.DisabledText : Royale.ControlBorder,

            PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox
                or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3
                or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner
                or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator
                or PaletteBorderStyle.ContextMenuItemSplit
                or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2
                or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderForm
                => Royale.EdgeShadow,
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
                => state == PaletteState.Disabled ? Royale.DisabledText : Royale.EdgeHighlight,

            PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon
                or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2
                or PaletteBorderStyle.InputControlCustom3
                => state == PaletteState.Disabled ? Royale.DisabledText : Royale.EdgeHighlight,
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

        if (state == PaletteState.Disabled)
        {
            return Royale.DisabledText;
        }

        return style switch
        {
            PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderSecondary
                or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2
                or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.HeaderForm
                => Royale.CaptionText,
            PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip
                => Royale.ToolTipText,
            _ => Royale.Text
        };
    }

    /// <inheritdoc />
    public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state) =>
        GetContentShortTextColor1(style, state);

    /// <inheritdoc />
    public override Image? GetBackImage(PaletteBackStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            return base.GetBackImage(style, state);
        }

        if (!TryGetSpriteTheme(out WindowsXPSpriteTheme theme))
        {
            return base.GetBackImage(style, state);
        }

        WindowsXPLunaPart? part = BackPart(style);
        return part.HasValue
            ? WindowsXPLunaArt.Sheet(theme, part.Value)
            : base.GetBackImage(style, state);
    }

    /// <inheritdoc />
    public override Image? GetBorderImage(PaletteBorderStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            return base.GetBorderImage(style, state);
        }

        return TryGetSpriteTheme(out WindowsXPSpriteTheme theme) && IsFormFrame(style)
            ? WindowsXPLunaArt.Sheet(theme, WindowsXPLunaPart.FrameLeft)
            : base.GetBorderImage(style, state);
    }

    /// <inheritdoc />
    public override int GetBorderWidth(PaletteBorderStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            return -1;
        }

        if (!TryGetSpriteTheme(out WindowsXPSpriteTheme theme))
        {
            return base.GetBorderWidth(style, state);
        }

        if (IsFormFrame(style))
        {
            return WindowsXPLunaArt.FrameThicknessFor(theme);
        }

        if (style == PaletteBorderStyle.HeaderForm || IsSpriteBorder(style))
        {
            return 0;
        }

        return base.GetBorderWidth(style, state);
    }

    /// <inheritdoc />
    public override Image? GetCheckBoxImage(bool enabled, CheckState checkState, bool tracking, bool pressed) =>
        TryGetSpriteTheme(out WindowsXPSpriteTheme theme)
            ? WindowsXPLunaArt.CheckBox(theme, enabled, checkState, tracking, pressed)
            : base.GetCheckBoxImage(enabled, checkState, tracking, pressed);

    /// <inheritdoc />
    public override Image? GetRadioButtonImage(bool enabled, bool checkState, bool tracking, bool pressed) =>
        TryGetSpriteTheme(out WindowsXPSpriteTheme theme)
            ? WindowsXPLunaArt.Radio(theme, enabled, checkState, tracking, pressed)
            : base.GetRadioButtonImage(enabled, checkState, tracking, pressed);

    /// <inheritdoc />
    public override Image? GetButtonSpecImage(PaletteButtonSpecStyle style, PaletteState state) =>
        TryGetSpriteTheme(out WindowsXPSpriteTheme theme) && GlyphPart(style) is WindowsXPLunaPart part
            ? WindowsXPLunaArt.Glyph(theme, part, state)
            : base.GetButtonSpecImage(style, state);

    private bool TryGetSpriteTheme(out WindowsXPSpriteTheme theme)
    {
        if (this is IWindowsXPSpriteTheme sprite)
        {
            theme = sprite.Theme;
            return true;
        }

        theme = default;
        return false;
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
        PaletteState.Disabled => Royale.ControlDisabled,
        PaletteState.Tracking or PaletteState.CheckedTracking => Royale.ButtonTracking,
        PaletteState.Pressed or PaletteState.CheckedPressed => Royale.ButtonPressed,
        PaletteState.CheckedNormal => Royale.ButtonChecked,
        _ => Royale.ButtonFace
    };

    private Color GetButtonBackColor2(PaletteState state) => state switch
    {
        PaletteState.Disabled => Royale.ControlDisabled,
        PaletteState.Tracking or PaletteState.CheckedTracking => Royale.ButtonTracking2,
        PaletteState.Pressed or PaletteState.CheckedPressed => Royale.ButtonPressed2,
        PaletteState.CheckedNormal => Royale.ButtonChecked2,
        _ => Royale.ButtonFace2
    };
}
