#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) et al. 2026. All rights reserved.
 */
#endregion

namespace Krypton.Themes;

/// <summary>
/// Accent packs for issue #1551 (Materialize Blue, Materialize Light Blue, Silver Dark Alternate).
/// </summary>
public enum MaterializeAccentKind
{
    /// <summary>Materialize CSS Blue palette (https://colorswall.com/palette/8).</summary>
    Blue,

    /// <summary>Materialize CSS Light Blue palette (https://colorswall.com/palette/13).</summary>
    LightBlue,

    /// <summary>Silver Dark Mode Alternate (near-black silver chrome).</summary>
    SilverDarkAlternate
}

/// <summary>
/// Builds Materialize Blue / Light Blue / Silver Dark Alternate variants of an existing
/// <see cref="KryptonColorSchemeBase"/>-derived scheme.
/// </summary>
public static class MaterializeSchemeHelper
{
    /// <summary>
    /// Creates an accented copy of <paramref name="source"/>.
    /// </summary>
    public static KryptonColorSchemeBase Create(KryptonColorSchemeBase source, MaterializeAccentKind kind, bool dark)
    {
        var target = new EmptySchemeBase();

        CopyColors(source, target);
        ApplyAccent(target, kind, dark);

        return target;
    }

    /// <summary>
    /// Copies every readable/writable <see cref="Color"/> property from <paramref name="source"/> onto
    /// <paramref name="target"/> by name, so the donor scheme's non-accent colours flow through untouched.
    /// </summary>
    private static void CopyColors(KryptonColorSchemeBase source, KryptonColorSchemeBase target)
    {
        Type targetType = target.GetType();

        foreach (PropertyInfo sourceProperty in source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (!sourceProperty.CanRead || !sourceProperty.CanWrite || sourceProperty.PropertyType != typeof(Color))
            {
                continue;
            }

            PropertyInfo? targetProperty = targetType.GetProperty(sourceProperty.Name);

            if (targetProperty != null && targetProperty.CanWrite)
            {
                targetProperty.SetValue(target, sourceProperty.GetValue(source));
            }
        }
    }

    private static void ApplyAccent(KryptonColorSchemeBase scheme, MaterializeAccentKind kind, bool dark)
    {
        if (dark || kind == MaterializeAccentKind.SilverDarkAlternate)
        {
            ApplyDark(scheme, kind);
        }
        else
        {
            ApplyLight(scheme, kind);
        }
    }

    /// <summary>
    /// Registers Lime Green Tracking / Pressed / Checked button back and border colours for
    /// <typeparamref name="TOwner"/> so <see cref="PaletteBase.GetArrayColor{TEnum}"/> resolves them
    /// ahead of the shared Office orange/gold defaults.
    /// </summary>
    /// <remarks>
    /// Builtin Office / Microsoft 365 bases draw standalone button Tracking / Pressed / Checked fills
    /// from the <see cref="ButtonBackColor"/> / <see cref="ButtonBorderColor"/> colour LUT, not from
    /// <see cref="KryptonColorSchemeBase.ButtonNormalBack1"/>. Call from each Lime Green palette's
    /// static constructor.
    /// </remarks>
    /// <typeparam name="TOwner">Concrete palette type that owns these LUT entries.</typeparam>
    /// <param name="kind">Accent pack.</param>
    /// <param name="dark">When <c>true</c>, disabled LUT slots use dark surfaces.</param>
    public static void RegisterButtonStateColors<TOwner>(MaterializeAccentKind kind, bool dark) where TOwner : PaletteBase
    {
        GetButtonLut(kind, dark,
            out Color normalBottom, out Color hoverTop, out Color hoverBottom, out Color hoverBorder,
            out Color selectedTop, out Color selectedBottom, out Color selectedBorder,
            out Color pressedTop, out Color pressedBottom, out Color pressedBorder,
            out Color disabledTop, out Color disabledBottom, out Color disabledBorder);

        // ButtonBackColor slots used by PaletteOffice*Base / PaletteMicrosoft365Base GetBackColor*:
        // Color1/2 disabled-ish, Color3/4 tracking, Color5/6 pressed, Color7/8 checked, Color9/10 checked+tracking.
        PaletteBase.RegisterColor<TOwner, ButtonBackColor>(ButtonBackColor.Color1, disabledTop);
        PaletteBase.RegisterColor<TOwner, ButtonBackColor>(ButtonBackColor.Color2, disabledBottom);
        PaletteBase.RegisterColor<TOwner, ButtonBackColor>(ButtonBackColor.Color3, hoverTop);
        PaletteBase.RegisterColor<TOwner, ButtonBackColor>(ButtonBackColor.Color4, hoverBottom);
        PaletteBase.RegisterColor<TOwner, ButtonBackColor>(ButtonBackColor.Color5, pressedTop);
        PaletteBase.RegisterColor<TOwner, ButtonBackColor>(ButtonBackColor.Color6, pressedBottom);
        PaletteBase.RegisterColor<TOwner, ButtonBackColor>(ButtonBackColor.Color7, selectedTop);
        PaletteBase.RegisterColor<TOwner, ButtonBackColor>(ButtonBackColor.Color8, selectedBottom);
        PaletteBase.RegisterColor<TOwner, ButtonBackColor>(ButtonBackColor.Color9, hoverTop);
        PaletteBase.RegisterColor<TOwner, ButtonBackColor>(ButtonBackColor.Color10, normalBottom);

        // ButtonBorderColor: Color1 disabled, Color2/3 tracking, Color4/5 pressed, Color6/7 checked.
        PaletteBase.RegisterColor<TOwner, ButtonBorderColor>(ButtonBorderColor.Color1, disabledBorder);
        PaletteBase.RegisterColor<TOwner, ButtonBorderColor>(ButtonBorderColor.Color2, hoverBorder);
        PaletteBase.RegisterColor<TOwner, ButtonBorderColor>(ButtonBorderColor.Color3, hoverBorder);
        PaletteBase.RegisterColor<TOwner, ButtonBorderColor>(ButtonBorderColor.Color4, pressedBorder);
        PaletteBase.RegisterColor<TOwner, ButtonBorderColor>(ButtonBorderColor.Color5, pressedBorder);
        PaletteBase.RegisterColor<TOwner, ButtonBorderColor>(ButtonBorderColor.Color6, selectedBorder);
        PaletteBase.RegisterColor<TOwner, ButtonBorderColor>(ButtonBorderColor.Color7, selectedBorder);

        // Ribbon application-button track / pressed gradients (still orange/gold on the family bases).
        PaletteBase.RegisterColor<TOwner, AppButtonTrackColor>(AppButtonTrackColor.Color1, hoverTop);
        PaletteBase.RegisterColor<TOwner, AppButtonTrackColor>(AppButtonTrackColor.Color2, hoverBottom);
        PaletteBase.RegisterColor<TOwner, AppButtonTrackColor>(AppButtonTrackColor.Color3, selectedTop);
        PaletteBase.RegisterColor<TOwner, AppButtonTrackColor>(AppButtonTrackColor.Color4, hoverTop);
        PaletteBase.RegisterColor<TOwner, AppButtonTrackColor>(AppButtonTrackColor.Color5, selectedBottom);

        PaletteBase.RegisterColor<TOwner, AppButtonPressedColor>(AppButtonPressedColor.Color1, pressedTop);
        PaletteBase.RegisterColor<TOwner, AppButtonPressedColor>(AppButtonPressedColor.Color2, pressedBottom);
        PaletteBase.RegisterColor<TOwner, AppButtonPressedColor>(AppButtonPressedColor.Color3, pressedBorder);
        PaletteBase.RegisterColor<TOwner, AppButtonPressedColor>(AppButtonPressedColor.Color4, pressedTop);
        PaletteBase.RegisterColor<TOwner, AppButtonPressedColor>(AppButtonPressedColor.Color5, pressedBottom);
    }

    private static void ApplyLight(KryptonColorSchemeBase scheme, MaterializeAccentKind kind)
    {
        GetLightRamp(kind,
            out Color buttonBack1, out Color buttonBack2, out Color buttonBorder,
            out Color panelClient, out Color panelAlt, out Color header1, out Color header2,
            out Color statusDark, out Color inactiveBorder, out Color inactiveFill,
            out Color hoverTop, out Color hoverBottom, out Color hoverBorder,
            out Color pressedTop, out Color pressedBottom, out Color pressedBorder,
            out Color link, out Color linkVisited, out Color disabledBack, out Color disabledBorder);

        scheme.TextButtonNormal = Color.Black;
        scheme.TextButtonChecked = Color.Black;

        scheme.ButtonNormalBack1 = buttonBack1;
        scheme.ButtonNormalBack2 = buttonBack2;
        scheme.ButtonNormalBorder = buttonBorder;
        scheme.ButtonNormalDefaultBack1 = buttonBack1;
        scheme.ButtonNormalDefaultBack2 = buttonBack2;
        scheme.ButtonNormalDefaultBorder = buttonBorder;
        scheme.ButtonNormalNavigatorBack1 = buttonBack1;
        scheme.ButtonNormalNavigatorBack2 = buttonBack2;
        scheme.ButtonBorder = pressedBorder;

        scheme.PanelClient = panelClient;
        scheme.PanelAlternative = panelAlt;
        scheme.ControlBorder = buttonBorder;

        scheme.HeaderPrimaryBack1 = header1;
        scheme.HeaderPrimaryBack2 = header2;
        scheme.HeaderSecondaryBack1 = buttonBack1;
        scheme.HeaderSecondaryBack2 = buttonBack2;
        scheme.HeaderText = Color.Black;

        scheme.StatusStripLight = header1;
        scheme.StatusStripDark = statusDark;
        scheme.StatusStripText = Color.Black;

        scheme.ToolStripBegin = buttonBack1;
        scheme.ToolStripMiddle = buttonBack2;
        scheme.ToolStripEnd = header2;

        scheme.FormBorderActive = buttonBorder;
        scheme.FormBorderInactive = inactiveBorder;
        scheme.FormBorderActiveLight = buttonBack1;
        scheme.FormBorderActiveDark = buttonBack2;
        scheme.FormBorderInactiveLight = inactiveFill;
        scheme.FormBorderInactiveDark = inactiveFill;

        scheme.FormBorderHeaderActive = hoverBorder;
        scheme.FormBorderHeaderInactive = inactiveBorder;
        scheme.FormBorderHeaderActive1 = hoverTop;
        scheme.FormBorderHeaderActive2 = header2;
        scheme.FormBorderHeaderInactive1 = inactiveFill;
        scheme.FormBorderHeaderInactive2 = inactiveFill;

        scheme.FormHeaderShortActive = Color.Black;
        scheme.FormHeaderLongActive = Color.Black;
        scheme.FormHeaderShortInactive = Color.FromArgb(0x80, 0x80, 0x80);
        scheme.FormHeaderLongInactive = Color.FromArgb(0x80, 0x80, 0x80);

        scheme.FormButtonBack1Track = hoverTop;
        scheme.FormButtonBack2Track = hoverBottom;
        scheme.FormButtonBorderTrack = hoverBorder;
        scheme.FormButtonBack1Pressed = pressedTop;
        scheme.FormButtonBack2Pressed = pressedBottom;
        scheme.FormButtonBorderPressed = pressedBorder;

        scheme.ButtonNavigatorTrack1 = hoverTop;
        scheme.ButtonNavigatorTrack2 = hoverBottom;
        scheme.ButtonNavigatorPressed1 = pressedTop;
        scheme.ButtonNavigatorPressed2 = pressedBottom;
        scheme.ButtonNavigatorChecked1 = header1;
        scheme.ButtonNavigatorChecked2 = header2;

        scheme.TextLabelControl = Color.Black;
        scheme.TextLabelPanel = Color.Black;
        scheme.TextListItem = Color.Black;
        scheme.ButtonTextTracking = Color.Black;
        scheme.InputControlTextNormal = Color.Black;
        scheme.InputControlTextDisabled = Color.FromArgb(0x80, 0x80, 0x80);
        scheme.InputControlBorderNormal = buttonBorder;
        scheme.InputControlBorderDisabled = disabledBorder;
        scheme.InputControlBackNormal = panelClient;
        scheme.InputControlBackDisabled = disabledBack;
        scheme.InputControlBackInactive = panelAlt;
        scheme.InputDropDownNormal1 = Color.Black;
        scheme.InputDropDownNormal2 = buttonBorder;
        scheme.InputDropDownDisabled1 = Color.FromArgb(0x80, 0x80, 0x80);
        scheme.InputDropDownDisabled2 = Color.Transparent;
        scheme.LinkNotVisitedOverrideControl = link;
        scheme.LinkVisitedOverrideControl = linkVisited;
        scheme.LinkPressedOverrideControl = pressedBorder;
        scheme.LinkNotVisitedOverridePanel = link;
        scheme.LinkVisitedOverridePanel = linkVisited;
        scheme.LinkPressedOverridePanel = pressedBorder;
        scheme.ToolStripBack = panelAlt;
        scheme.ImageMargin = panelAlt;
        scheme.ToolStripBorder = buttonBorder;
        scheme.GridListNormal1 = panelClient;
        scheme.GridListNormal2 = panelAlt;
        scheme.GridListSelected = header1;
        scheme.ContextMenuHeadingBack = header1;
        scheme.ContextMenuHeadingText = Color.Black;
        scheme.ContextMenuImageColumn = panelAlt;
        scheme.MenuItemText = Color.Black;
        scheme.MenuStripText = Color.Black;
    }

    /// <summary>
    /// Resolves the filled Lime Green button back colour for Material palettes, which otherwise use a
    /// neutral surface + overlay model that would hide the lime brand fills.
    /// </summary>
    /// <param name="kind">Accent pack.</param>
    /// <param name="state">Button palette state.</param>
    /// <returns>Solid fill appropriate for <paramref name="state"/>.</returns>
    public static Color GetMaterialButtonBack(MaterializeAccentKind kind, PaletteState state)
    {
        GetButtonLut(kind, dark: false,
            out Color normalBottom, out Color hoverTop, out Color hoverBottom, out Color hoverBorder,
            out Color selectedTop, out Color selectedBottom, out Color selectedBorder,
            out Color pressedTop, out Color pressedBottom, out Color pressedBorder,
            out Color disabledTop, out Color disabledBottom, out Color disabledBorder);

        return state switch
        {
            PaletteState.Disabled => disabledTop,
            PaletteState.Tracking or PaletteState.CheckedTracking => hoverTop,
            PaletteState.Pressed or PaletteState.CheckedPressed => pressedTop,
            PaletteState.CheckedNormal => selectedTop,
            _ => normalBottom
        };
    }

    /// <summary>
    /// Resolves the accent button border colour for Material interactive states.
    /// </summary>
    public static Color GetMaterialButtonBorder(MaterializeAccentKind kind, PaletteState state)
    {
        GetButtonLut(kind, dark: false,
            out Color normalBottom, out Color hoverTop, out Color hoverBottom, out Color hoverBorder,
            out Color selectedTop, out Color selectedBottom, out Color selectedBorder,
            out Color pressedTop, out Color pressedBottom, out Color pressedBorder,
            out Color disabledTop, out Color disabledBottom, out Color disabledBorder);

        return state switch
        {
            PaletteState.Disabled => disabledBorder,
            PaletteState.Tracking or PaletteState.CheckedTracking => hoverBorder,
            PaletteState.Pressed or PaletteState.CheckedPressed => pressedBorder,
            PaletteState.CheckedNormal => selectedBorder,
            _ => hoverBorder
        };
    }

    /// <summary>
    /// Applies the dark-mode Lime Green accents. Buttons keep the same bright lime accent colours as the
    /// light variant (with black text) so the "brand" colour reads consistently; every other chrome surface
    /// (panels, input/list controls, labels, grids, ribbon, menus, separators) moves to dark olive tones with
    /// pale lime text/borders. This is required because the Office 2007/2010 "Dark Mode" donor schemes still
    /// carry light-blue input/list/label colours that would otherwise leak through.
    /// </summary>
    private static void ApplyDark(KryptonColorSchemeBase scheme, MaterializeAccentKind kind)
    {
        GetDarkRamp(kind,
            out Color buttonBack1, out Color buttonBack2, out Color buttonBorder,
            out Color lightText, out Color mutedText,
            out Color panelDeep, out Color panelMid, out Color panelRaised, out Color panelHigh, out Color panelAccent,
            out Color borderMuted, out Color borderAccent, out Color linkLime,
            out Color selectedTop, out Color selectedBottom);

        Color buttonText = kind == MaterializeAccentKind.SilverDarkAlternate ? Color.White : Color.Black;

        scheme.TextButtonNormal = buttonText;
        scheme.TextButtonChecked = buttonText;
        scheme.ButtonTextTracking = buttonText;

        scheme.ButtonNormalBack1 = buttonBack1;
        scheme.ButtonNormalBack2 = buttonBack2;
        scheme.ButtonNormalBorder = buttonBorder;
        scheme.ButtonNormalDefaultBack1 = buttonBack1;
        scheme.ButtonNormalDefaultBack2 = buttonBack2;
        scheme.ButtonNormalDefaultBorder = buttonBorder;
        scheme.ButtonNormalNavigatorBack1 = buttonBack1;
        scheme.ButtonNormalNavigatorBack2 = buttonBack2;
        scheme.ButtonBorder = buttonBorder;

        scheme.PanelClient = panelDeep;
        scheme.PanelAlternative = panelMid;
        scheme.ControlBorder = borderAccent;

        scheme.HeaderPrimaryBack1 = panelHigh;
        scheme.HeaderPrimaryBack2 = panelAccent;
        scheme.HeaderSecondaryBack1 = panelRaised;
        scheme.HeaderSecondaryBack2 = panelAccent;
        scheme.HeaderText = lightText;
        scheme.HeaderDockInactiveBack1 = panelMid;
        scheme.HeaderDockInactiveBack2 = panelRaised;

        scheme.StatusStripLight = panelHigh;
        scheme.StatusStripDark = panelDeep;
        scheme.StatusStripText = lightText;

        scheme.ToolStripBack = panelMid;
        scheme.ToolStripBegin = panelHigh;
        scheme.ToolStripMiddle = panelRaised;
        scheme.ToolStripEnd = panelDeep;
        scheme.ToolStripBorder = borderMuted;
        scheme.ImageMargin = panelMid;
        scheme.OverflowBegin = panelHigh;
        scheme.OverflowMiddle = panelRaised;
        scheme.OverflowEnd = borderMuted;

        scheme.SeparatorLight = panelAccent;
        scheme.SeparatorDark = borderMuted;
        scheme.SeparatorHighBorder1 = panelHigh;
        scheme.SeparatorHighBorder2 = panelRaised;
        scheme.SeparatorHighInternalBorder1 = panelHigh;
        scheme.SeparatorHighInternalBorder2 = panelRaised;
        scheme.GripLight = lightText;
        scheme.GripDark = borderMuted;

        scheme.FormBorderActive = borderAccent;
        scheme.FormBorderInactive = borderMuted;
        scheme.FormBorderActiveLight = panelDeep;
        scheme.FormBorderActiveDark = panelHigh;
        scheme.FormBorderInactiveLight = panelMid;
        scheme.FormBorderInactiveDark = panelMid;

        scheme.FormBorderHeaderActive = borderAccent;
        scheme.FormBorderHeaderInactive = borderMuted;
        scheme.FormBorderHeaderActive1 = panelHigh;
        scheme.FormBorderHeaderActive2 = panelAccent;
        scheme.FormBorderHeaderInactive1 = panelMid;
        scheme.FormBorderHeaderInactive2 = panelMid;

        scheme.FormHeaderShortActive = lightText;
        scheme.FormHeaderLongActive = lightText;
        scheme.FormHeaderShortInactive = mutedText;
        scheme.FormHeaderLongInactive = mutedText;

        // Form caption buttons use dark olive greens on hover/press, with pale lime text for contrast.
        scheme.FormButtonBack1Track = panelHigh;
        scheme.FormButtonBack2Track = panelAccent;
        scheme.FormButtonBorderTrack = borderMuted;
        scheme.FormButtonBack1Pressed = panelDeep;
        scheme.FormButtonBack2Pressed = panelHigh;
        scheme.FormButtonBorderPressed = borderAccent;
        scheme.FormButtonBack1Checked = panelAccent;
        scheme.FormButtonBack2Checked = panelHigh;
        scheme.FormButtonBorderCheck = borderAccent;
        scheme.FormButtonBack1CheckTrack = panelHigh;
        scheme.FormButtonBack2CheckTrack = panelAccent;
        scheme.TextButtonFormNormal = lightText;
        scheme.TextButtonFormTracking = lightText;
        scheme.TextButtonFormPressed = lightText;

        // Labels / list text: Office 2007/2010 dark donors keep dark-blue text — force pale lime for contrast.
        scheme.TextLabelControl = lightText;
        scheme.TextLabelPanel = lightText;
        scheme.TextListItem = lightText;
        scheme.MenuItemText = lightText;
        scheme.MenuStripText = lightText;
        scheme.DisabledMenuItemText = mutedText;
        scheme.MenuMarginGradientStart = panelMid;
        scheme.MenuMarginGradientMiddle = panelRaised;
        scheme.MenuMarginGradientEnd = panelDeep;

        scheme.LinkNotVisitedOverrideControl = linkLime;
        scheme.LinkVisitedOverrideControl = buttonBorder;
        scheme.LinkPressedOverrideControl = buttonBack2;
        scheme.LinkNotVisitedOverridePanel = linkLime;
        scheme.LinkVisitedOverridePanel = buttonBorder;
        scheme.LinkPressedOverridePanel = buttonBack2;

        // Input / theme-list chrome — the light-blue leak in the screenshot.
        scheme.InputControlTextNormal = lightText;
        scheme.InputControlTextDisabled = mutedText;
        scheme.InputControlBorderNormal = borderAccent;
        scheme.InputControlBorderDisabled = borderMuted;
        scheme.InputControlBackNormal = panelDeep;
        scheme.InputControlBackDisabled = panelMid;
        scheme.InputControlBackInactive = panelRaised;
        scheme.InputDropDownNormal1 = lightText;
        scheme.InputDropDownNormal2 = mutedText;
        scheme.InputDropDownDisabled1 = mutedText;
        scheme.InputDropDownDisabled2 = borderMuted;

        scheme.ContextMenuHeadingBack = panelHigh;
        scheme.ContextMenuHeadingText = lightText;
        scheme.ContextMenuImageColumn = panelRaised;

        scheme.GridListNormal1 = panelDeep;
        scheme.GridListNormal2 = panelMid;
        scheme.GridListPressed1 = panelHigh;
        scheme.GridListPressed2 = panelAccent;
        scheme.GridListSelected = panelAccent;
        scheme.GridSheetColNormal1 = panelDeep;
        scheme.GridSheetColNormal2 = panelMid;
        scheme.GridSheetColPressed1 = panelHigh;
        scheme.GridSheetColPressed2 = panelAccent;
        scheme.GridSheetColSelected1 = selectedTop;
        scheme.GridSheetColSelected2 = selectedBottom;
        scheme.GridSheetRowNormal = panelRaised;
        scheme.GridSheetRowPressed = panelHigh;
        scheme.GridSheetRowSelected = panelAccent;
        scheme.GridDataCellBorder = borderMuted;
        scheme.GridDataCellSelected = panelHigh;

        scheme.NavigatorMiniBackColor = panelMid;
        scheme.ButtonNavigatorBorder = borderMuted;
        scheme.ButtonNavigatorText = lightText;
        scheme.ButtonNavigatorTrack1 = panelHigh;
        scheme.ButtonNavigatorTrack2 = panelAccent;
        scheme.ButtonNavigatorPressed1 = panelDeep;
        scheme.ButtonNavigatorPressed2 = panelHigh;
        scheme.ButtonNavigatorChecked1 = panelAccent;
        scheme.ButtonNavigatorChecked2 = panelHigh;

        scheme.AlternatePressedBack1 = panelHigh;
        scheme.AlternatePressedBack2 = panelAccent;
        scheme.AlternatePressedBorder1 = borderMuted;
        scheme.AlternatePressedBorder2 = borderAccent;

        scheme.ButtonClusterButtonBack1 = panelRaised;
        scheme.ButtonClusterButtonBack2 = panelHigh;
        scheme.ButtonClusterButtonBorder1 = borderMuted;
        scheme.ButtonClusterButtonBorder2 = borderAccent;

        scheme.AppButtonBack1 = panelDeep;
        scheme.AppButtonBack2 = panelMid;
        scheme.AppButtonBorder = borderMuted;
        scheme.AppButtonOuter1 = panelRaised;
        scheme.AppButtonOuter2 = panelMid;
        scheme.AppButtonOuter3 = panelHigh;
        scheme.AppButtonInner1 = panelAccent;
        scheme.AppButtonInner2 = borderMuted;
        scheme.AppButtonMenuDocsBack = panelMid;
        scheme.AppButtonMenuDocsText = lightText;

        // Ribbon surfaces — keep dark olive so a ribbon host does not flash the donor's light blue.
        scheme.RibbonTabTextNormal = lightText;
        scheme.RibbonTabTextChecked = Color.Black;
        scheme.RibbonTabSelected1 = buttonBack1;
        scheme.RibbonTabSelected2 = buttonBack2;
        scheme.RibbonTabSelected3 = buttonBack1;
        scheme.RibbonTabSelected4 = buttonBack2;
        scheme.RibbonTabSelected5 = panelDeep;
        scheme.RibbonTabTracking1 = panelHigh;
        scheme.RibbonTabTracking2 = panelAccent;
        scheme.RibbonTabTracking3 = panelAccent;
        scheme.RibbonTabTracking4 = panelHigh;
        scheme.RibbonTabHighlight1 = buttonBack1;
        scheme.RibbonTabHighlight2 = buttonBack2;
        scheme.RibbonTabHighlight3 = selectedBottom;
        scheme.RibbonTabHighlight4 = buttonBack2;
        scheme.RibbonTabHighlight5 = panelHigh;
        scheme.RibbonTabSeparatorColor = borderMuted;
        scheme.RibbonGroupsArea1 = panelRaised;
        scheme.RibbonGroupsArea2 = panelMid;
        scheme.RibbonGroupsArea3 = panelDeep;
        scheme.RibbonGroupsArea4 = panelHigh;
        scheme.RibbonGroupsArea5 = panelAccent;
        scheme.RibbonGroupBorder1 = borderMuted;
        scheme.RibbonGroupBorder2 = borderAccent;
        scheme.RibbonGroupBorder3 = borderMuted;
        scheme.RibbonGroupBorder4 = borderAccent;
        scheme.RibbonGroupTitle1 = panelHigh;
        scheme.RibbonGroupTitle2 = panelRaised;
        scheme.RibbonGroupTitleText = lightText;
        scheme.RibbonGroupBorderContext1 = borderMuted;
        scheme.RibbonGroupBorderContext2 = borderAccent;
        scheme.RibbonGroupTitleContext1 = panelHigh;
        scheme.RibbonGroupTitleContext2 = panelRaised;
        scheme.RibbonGroupDialogDark = borderAccent;
        scheme.RibbonGroupDialogLight = lightText;
        scheme.RibbonGroupTitleTracking1 = panelAccent;
        scheme.RibbonGroupTitleTracking2 = panelHigh;
        scheme.RibbonMinimizeBarDark = panelDeep;
        scheme.RibbonMinimizeBarLight = panelMid;
        scheme.RibbonGroupCollapsedBorder1 = borderMuted;
        scheme.RibbonGroupCollapsedBorder2 = borderAccent;
        scheme.RibbonGroupCollapsedBorder3 = Color.FromArgb(64, lightText);
        scheme.RibbonGroupCollapsedBorder4 = panelAccent;
        scheme.RibbonGroupCollapsedBack1 = panelRaised;
        scheme.RibbonGroupCollapsedBack2 = panelMid;
        scheme.RibbonGroupCollapsedBack3 = panelDeep;
        scheme.RibbonGroupCollapsedBack4 = panelHigh;
        scheme.RibbonGroupCollapsedBorderT1 = borderMuted;
        scheme.RibbonGroupCollapsedBorderT2 = borderAccent;
        scheme.RibbonGroupCollapsedBorderT3 = Color.FromArgb(96, lightText);
        scheme.RibbonGroupCollapsedBorderT4 = panelAccent;
        scheme.RibbonGroupCollapsedBackT1 = panelHigh;
        scheme.RibbonGroupCollapsedBackT2 = panelRaised;
        scheme.RibbonGroupCollapsedBackT3 = panelMid;
        scheme.RibbonGroupCollapsedBackT4 = panelAccent;
        scheme.RibbonGroupFrameBorder1 = borderMuted;
        scheme.RibbonGroupFrameBorder2 = borderAccent;
        scheme.RibbonGroupFrameInside1 = panelRaised;
        scheme.RibbonGroupFrameInside2 = panelMid;
        scheme.RibbonGroupFrameInside3 = panelDeep;
        scheme.RibbonGroupFrameInside4 = panelHigh;
        scheme.RibbonGroupCollapsedText = lightText;
        scheme.RibbonGroupTextTracking = lightText;
        scheme.RibbonGroupButtonText = lightText;
        scheme.RibbonGroupSeparatorDark = borderMuted;
        scheme.RibbonGroupSeparatorLight = panelAccent;
        scheme.RibbonQATMini1 = panelRaised;
        scheme.RibbonQATMini2 = panelHigh;
        scheme.RibbonQATMini3 = panelMid;
        scheme.RibbonQATMini4 = Color.FromArgb(128, lightText);
        scheme.RibbonQATMini5 = Color.FromArgb(72, lightText);
        scheme.RibbonQATMini1I = panelMid;
        scheme.RibbonQATMini2I = panelRaised;
        scheme.RibbonQATMini3I = panelDeep;
        scheme.RibbonQATMini4I = Color.FromArgb(128, lightText);
        scheme.RibbonQATMini5I = Color.FromArgb(72, lightText);
        scheme.RibbonQATFullbar1 = panelHigh;
        scheme.RibbonQATFullbar2 = panelRaised;
        scheme.RibbonQATFullbar3 = borderMuted;
        scheme.RibbonQATButtonDark = borderAccent;
        scheme.RibbonQATButtonLight = lightText;
        scheme.RibbonQATOverflow1 = panelHigh;
        scheme.RibbonQATOverflow2 = borderAccent;
        scheme.RibbonGalleryBorder = borderMuted;
        scheme.RibbonGalleryBackNormal = panelMid;
        scheme.RibbonGalleryBackTracking = panelRaised;
        scheme.RibbonGalleryBack1 = panelRaised;
        scheme.RibbonGalleryBack2 = panelHigh;
        scheme.RibbonDropArrowLight = lightText;
        scheme.RibbonDropArrowDark = borderAccent;

        scheme.ToolTipBottom = panelHigh;
        scheme.TrackBarTickMarks = borderMuted;
        scheme.TrackBarTopTrack = panelDeep;
        scheme.TrackBarBottomTrack = panelAccent;
        scheme.TrackBarFillTrack = borderAccent;
        scheme.TrackBarOutsidePosition = Color.FromArgb(64, lightText);
        scheme.TrackBarBorderPosition = borderMuted;
    }

    private static void GetLightRamp(MaterializeAccentKind kind,
        out Color buttonBack1, out Color buttonBack2, out Color buttonBorder,
        out Color panelClient, out Color panelAlt, out Color header1, out Color header2,
        out Color statusDark, out Color inactiveBorder, out Color inactiveFill,
        out Color hoverTop, out Color hoverBottom, out Color hoverBorder,
        out Color pressedTop, out Color pressedBottom, out Color pressedBorder,
        out Color link, out Color linkVisited, out Color disabledBack, out Color disabledBorder)
    {
        if (kind == MaterializeAccentKind.LightBlue)
        {
            // Materialize Light Blue https://colorswall.com/palette/13
            buttonBack1 = Color.FromArgb(0xB3, 0xE5, 0xFC);
            buttonBack2 = Color.FromArgb(0x4F, 0xC3, 0xF7);
            buttonBorder = Color.FromArgb(0x03, 0x9B, 0xE5);
            panelClient = Color.FromArgb(0xE1, 0xF5, 0xFE);
            panelAlt = Color.FromArgb(0xB3, 0xE5, 0xFC);
            header1 = Color.FromArgb(0x29, 0xB6, 0xF6);
            header2 = Color.FromArgb(0x03, 0xA9, 0xF4);
            statusDark = Color.FromArgb(0x02, 0x88, 0xD1);
            inactiveBorder = Color.FromArgb(0x81, 0xD4, 0xFA);
            inactiveFill = Color.FromArgb(0xE1, 0xF5, 0xFE);
            hoverTop = Color.FromArgb(0x81, 0xD4, 0xFA);
            hoverBottom = Color.FromArgb(0x29, 0xB6, 0xF6);
            hoverBorder = Color.FromArgb(0x03, 0x9B, 0xE5);
            pressedTop = Color.FromArgb(0x03, 0xA9, 0xF4);
            pressedBottom = Color.FromArgb(0x02, 0x88, 0xD1);
            pressedBorder = Color.FromArgb(0x01, 0x57, 0x9B);
            link = Color.FromArgb(0x02, 0x77, 0xBD);
            linkVisited = Color.FromArgb(0x01, 0x57, 0x9B);
            disabledBack = Color.FromArgb(0xE1, 0xF5, 0xFE);
            disabledBorder = Color.FromArgb(0xB3, 0xE5, 0xFC);
            return;
        }

        // Materialize Blue https://colorswall.com/palette/8 (default for Blue and unused light Silver)
        buttonBack1 = Color.FromArgb(0xBB, 0xDE, 0xFB);
        buttonBack2 = Color.FromArgb(0x64, 0xB5, 0xF6);
        buttonBorder = Color.FromArgb(0x1E, 0x88, 0xE5);
        panelClient = Color.FromArgb(0xE3, 0xF2, 0xFD);
        panelAlt = Color.FromArgb(0xBB, 0xDE, 0xFB);
        header1 = Color.FromArgb(0x42, 0xA5, 0xF5);
        header2 = Color.FromArgb(0x21, 0x96, 0xF3);
        statusDark = Color.FromArgb(0x19, 0x76, 0xD2);
        inactiveBorder = Color.FromArgb(0x90, 0xCA, 0xF9);
        inactiveFill = Color.FromArgb(0xE3, 0xF2, 0xFD);
        hoverTop = Color.FromArgb(0x90, 0xCA, 0xF9);
        hoverBottom = Color.FromArgb(0x42, 0xA5, 0xF5);
        hoverBorder = Color.FromArgb(0x1E, 0x88, 0xE5);
        pressedTop = Color.FromArgb(0x21, 0x96, 0xF3);
        pressedBottom = Color.FromArgb(0x19, 0x76, 0xD2);
        pressedBorder = Color.FromArgb(0x0D, 0x47, 0xA1);
        link = Color.FromArgb(0x15, 0x65, 0xC0);
        linkVisited = Color.FromArgb(0x0D, 0x47, 0xA1);
        disabledBack = Color.FromArgb(0xE3, 0xF2, 0xFD);
        disabledBorder = Color.FromArgb(0xBB, 0xDE, 0xFB);
    }

    private static void GetDarkRamp(MaterializeAccentKind kind,
        out Color buttonBack1, out Color buttonBack2, out Color buttonBorder,
        out Color lightText, out Color mutedText,
        out Color panelDeep, out Color panelMid, out Color panelRaised, out Color panelHigh, out Color panelAccent,
        out Color borderMuted, out Color borderAccent, out Color linkLime,
        out Color selectedTop, out Color selectedBottom)
    {
        if (kind == MaterializeAccentKind.SilverDarkAlternate)
        {
            buttonBack1 = Color.FromArgb(164, 163, 163);
            buttonBack2 = Color.FromArgb(114, 114, 114);
            buttonBorder = Color.FromArgb(137, 135, 133);
            lightText = Color.White;
            mutedText = Color.FromArgb(180, 180, 180);
            panelDeep = Color.FromArgb(15, 15, 15);
            panelMid = Color.FromArgb(31, 31, 31);
            panelRaised = Color.FromArgb(47, 47, 47);
            panelHigh = Color.FromArgb(65, 65, 65);
            panelAccent = Color.FromArgb(91, 91, 91);
            borderMuted = Color.FromArgb(76, 83, 92);
            borderAccent = Color.FromArgb(164, 163, 163);
            linkLime = Color.FromArgb(180, 210, 255);
            selectedTop = Color.FromArgb(190, 190, 190);
            selectedBottom = Color.FromArgb(91, 91, 91);
            return;
        }

        if (kind == MaterializeAccentKind.LightBlue)
        {
            buttonBack1 = Color.FromArgb(0xB3, 0xE5, 0xFC);
            buttonBack2 = Color.FromArgb(0x4F, 0xC3, 0xF7);
            buttonBorder = Color.FromArgb(0x03, 0x9B, 0xE5);
            lightText = Color.FromArgb(0xE1, 0xF5, 0xFE);
            mutedText = Color.FromArgb(0x81, 0xD4, 0xFA);
            panelDeep = Color.FromArgb(0x01, 0x57, 0x9B);
            panelMid = Color.FromArgb(0x02, 0x77, 0xBD);
            panelRaised = Color.FromArgb(0x02, 0x88, 0xD1);
            panelHigh = Color.FromArgb(0x03, 0x9B, 0xE5);
            panelAccent = Color.FromArgb(0x03, 0xA9, 0xF4);
            borderMuted = Color.FromArgb(0x02, 0x77, 0xBD);
            borderAccent = Color.FromArgb(0x4F, 0xC3, 0xF7);
            linkLime = Color.FromArgb(0x80, 0xD8, 0xFF);
            selectedTop = Color.FromArgb(0xB3, 0xE5, 0xFC);
            selectedBottom = Color.FromArgb(0x29, 0xB6, 0xF6);
            return;
        }

        buttonBack1 = Color.FromArgb(0xBB, 0xDE, 0xFB);
        buttonBack2 = Color.FromArgb(0x64, 0xB5, 0xF6);
        buttonBorder = Color.FromArgb(0x1E, 0x88, 0xE5);
        lightText = Color.FromArgb(0xE3, 0xF2, 0xFD);
        mutedText = Color.FromArgb(0x90, 0xCA, 0xF9);
        panelDeep = Color.FromArgb(0x0D, 0x47, 0xA1);
        panelMid = Color.FromArgb(0x15, 0x65, 0xC0);
        panelRaised = Color.FromArgb(0x19, 0x76, 0xD2);
        panelHigh = Color.FromArgb(0x1E, 0x88, 0xE5);
        panelAccent = Color.FromArgb(0x21, 0x96, 0xF3);
        borderMuted = Color.FromArgb(0x15, 0x65, 0xC0);
        borderAccent = Color.FromArgb(0x64, 0xB5, 0xF6);
        linkLime = Color.FromArgb(0x82, 0xB1, 0xFF);
        selectedTop = Color.FromArgb(0xBB, 0xDE, 0xFB);
        selectedBottom = Color.FromArgb(0x42, 0xA5, 0xF5);
    }

    private static void GetButtonLut(MaterializeAccentKind kind, bool dark,
        out Color normalBottom, out Color hoverTop, out Color hoverBottom, out Color hoverBorder,
        out Color selectedTop, out Color selectedBottom, out Color selectedBorder,
        out Color pressedTop, out Color pressedBottom, out Color pressedBorder,
        out Color disabledTop, out Color disabledBottom, out Color disabledBorder)
    {
        if (kind == MaterializeAccentKind.SilverDarkAlternate)
        {
            normalBottom = Color.FromArgb(114, 114, 114);
            hoverTop = Color.FromArgb(164, 163, 163);
            hoverBottom = Color.FromArgb(91, 91, 91);
            hoverBorder = Color.FromArgb(190, 190, 190);
            selectedTop = Color.FromArgb(190, 190, 190);
            selectedBottom = Color.FromArgb(91, 91, 91);
            selectedBorder = Color.FromArgb(213, 217, 223);
            pressedTop = Color.FromArgb(65, 65, 65);
            pressedBottom = Color.FromArgb(31, 31, 31);
            pressedBorder = Color.FromArgb(18, 18, 18);
            disabledTop = Color.FromArgb(47, 47, 47);
            disabledBottom = Color.FromArgb(31, 31, 31);
            disabledBorder = Color.FromArgb(76, 83, 92);
            return;
        }

        GetLightRamp(kind == MaterializeAccentKind.LightBlue ? MaterializeAccentKind.LightBlue : MaterializeAccentKind.Blue,
            out _, out Color buttonBack2, out Color buttonBorder,
            out Color panelClient, out _, out Color header1, out Color header2,
            out _, out _, out _,
            out Color hTop, out Color hBottom, out Color hBorder,
            out Color pTop, out Color pBottom, out Color pBorder,
            out _, out _, out Color disabledBack, out Color dBorder);

        normalBottom = buttonBack2;
        hoverTop = hTop;
        hoverBottom = hBottom;
        hoverBorder = hBorder;
        selectedTop = header1;
        selectedBottom = header2;
        selectedBorder = buttonBorder;
        pressedTop = pTop;
        pressedBottom = pBottom;
        pressedBorder = pBorder;
        disabledTop = dark ? Color.FromArgb(0x15, 0x65, 0xC0) : disabledBack;
        disabledBottom = dark ? Color.FromArgb(0x0D, 0x47, 0xA1) : panelClient;
        disabledBorder = dBorder;
    }
}
