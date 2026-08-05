#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) et al. 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Builds the Lime Green accent variant of an existing <see cref="KryptonColorSchemeBase"/>-derived scheme.
/// </summary>
/// <remarks>
/// The helper starts from a donor scheme (e.g. an Office 2007/2010 or Microsoft 365 built-in scheme), copies
/// every <see cref="Color"/> property across via reflection so unrelated chrome (rounding, ribbon gallery
/// colours, grid colours, etc.) is preserved, and then overwrites the button/panel/header/form accent colours
/// with the Lime Green palette. Call <see cref="RegisterButtonStateColors{TOwner}"/> from each Lime Green
/// palette static constructor so Tracking / Pressed / Checked button fills (and ribbon app-button track/press
/// gradients) also use lime instead of the shared Office orange/gold LUT defaults. This keeps the
/// family-specific "shape" of each donor while giving a single, consistent accent colour set that can be
/// reused across Office 2007, Office 2010 and Microsoft 365 palettes.
/// </remarks>
public static class LimeGreenSchemeHelper
{
    /// <summary>
    /// Creates a Lime Green accented copy of <paramref name="source"/>.
    /// </summary>
    /// <param name="source">The donor scheme whose non-accent colours (rounding, grid, ribbon, etc.) are preserved.</param>
    /// <param name="dark">When <c>true</c>, applies the dark-mode Lime Green accents; otherwise applies the light-mode accents.</param>
    /// <returns>A new <see cref="KryptonColorSchemeBase"/> instance with Lime Green accents applied.</returns>
    public static KryptonColorSchemeBase Create(KryptonColorSchemeBase source, bool dark)
    {
        var target = new EmptySchemeBase();

        CopyColors(source, target);
        ApplyLime(target, dark);

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

    /// <summary>
    /// Overwrites the button, panel, header, status strip and form-chrome accent colours with the
    /// Lime Green palette, in either its light or dark variant.
    /// </summary>
    private static void ApplyLime(KryptonColorSchemeBase scheme, bool dark)
    {
        if (dark)
        {
            ApplyLimeDark(scheme);
        }
        else
        {
            ApplyLimeLight(scheme);
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
    /// <typeparam name="TOwner">Concrete Lime Green palette type that owns these LUT entries.</typeparam>
    /// <param name="dark">When <c>true</c>, disabled LUT slots use dark olive; Tracking / Pressed / Checked stay bright lime accents.</param>
    public static void RegisterButtonStateColors<TOwner>(bool dark) where TOwner : PaletteBase
    {
        // Bright lime Tracking / Pressed / Checked match the sample screenshots on both chrome variants.
        // Dark caption/navigator track-press colours live on the scheme (FormButton* / ButtonNavigator*).
        Color normalBottom = Color.FromArgb(0xD7, 0xEE, 0x76);
        Color hoverTop = Color.FromArgb(0xD7, 0xEB, 0x6E);
        Color hoverBottom = Color.FromArgb(0xBE, 0xD8, 0x49);
        Color hoverBorder = Color.FromArgb(0xB3, 0xCA, 0x59);
        Color selectedTop = Color.FromArgb(0xD9, 0xEC, 0x77);
        Color selectedBottom = Color.FromArgb(0xBD, 0xD6, 0x49);
        Color selectedBorder = Color.FromArgb(0xB4, 0xCA, 0x5B);
        Color pressedTop = Color.FromArgb(0xC5, 0xDC, 0x54);
        Color pressedBottom = Color.FromArgb(0xC4, 0xDC, 0x43);
        Color pressedBorder = Color.FromArgb(0xA4, 0xB8, 0x3D);
        Color disabledTop = dark ? Color.FromArgb(0x2A, 0x30, 0x22) : Color.FromArgb(0xE8, 0xEC, 0xD0);
        Color disabledBottom = dark ? Color.FromArgb(0x35, 0x3C, 0x28) : Color.FromArgb(0xD8, 0xDC, 0xB8);
        Color disabledBorder = dark ? Color.FromArgb(0x55, 0x60, 0x3A) : Color.FromArgb(0xC0, 0xC4, 0xA0);

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

    /// <summary>
    /// Applies the light-mode Lime Green accents (bright lime buttons/headers over a pale lime wash).
    /// </summary>
    private static void ApplyLimeLight(KryptonColorSchemeBase scheme)
    {
        Color buttonBack1 = Color.FromArgb(0xE2, 0xFB, 0xAF);
        Color buttonBack2 = Color.FromArgb(0xD7, 0xEE, 0x76);
        Color buttonBorder = Color.FromArgb(0xA8, 0xBC, 0x4D);

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
        scheme.ButtonBorder = Color.FromArgb(0xA4, 0xB8, 0x3D);

        scheme.PanelClient = Color.FromArgb(0xF7, 0xFB, 0xEA);
        scheme.PanelAlternative = Color.FromArgb(0xEA, 0xF5, 0xC8);
        scheme.ControlBorder = Color.FromArgb(0xA8, 0xBC, 0x4D);

        scheme.HeaderPrimaryBack1 = Color.FromArgb(0xD9, 0xEC, 0x77);
        scheme.HeaderPrimaryBack2 = Color.FromArgb(0xBD, 0xD6, 0x49);
        scheme.HeaderSecondaryBack1 = buttonBack1;
        scheme.HeaderSecondaryBack2 = buttonBack2;
        scheme.HeaderText = Color.Black;

        scheme.StatusStripLight = Color.FromArgb(0xD9, 0xEC, 0x77);
        scheme.StatusStripDark = Color.FromArgb(0xC4, 0xDC, 0x43);
        scheme.StatusStripText = Color.Black;

        scheme.ToolStripBegin = Color.FromArgb(0xE2, 0xFB, 0xAF);
        scheme.ToolStripMiddle = Color.FromArgb(0xD7, 0xEE, 0x76);
        scheme.ToolStripEnd = Color.FromArgb(0xBD, 0xD6, 0x49);

        scheme.FormBorderActive = Color.FromArgb(0xA8, 0xBC, 0x4D);
        scheme.FormBorderInactive = Color.FromArgb(0xC5, 0xCE, 0x9A);
        scheme.FormBorderActiveLight = Color.FromArgb(0xE2, 0xFB, 0xAF);
        scheme.FormBorderActiveDark = Color.FromArgb(0xD7, 0xEE, 0x76);
        scheme.FormBorderInactiveLight = Color.FromArgb(0xE8, 0xF0, 0xC8);
        scheme.FormBorderInactiveDark = Color.FromArgb(0xE8, 0xF0, 0xC8);

        scheme.FormBorderHeaderActive = Color.FromArgb(0xB3, 0xCA, 0x59);
        scheme.FormBorderHeaderInactive = Color.FromArgb(0xC5, 0xCE, 0x9A);
        scheme.FormBorderHeaderActive1 = Color.FromArgb(0xD7, 0xEB, 0x6E);
        scheme.FormBorderHeaderActive2 = Color.FromArgb(0xBD, 0xD6, 0x49);
        scheme.FormBorderHeaderInactive1 = Color.FromArgb(0xE8, 0xF0, 0xC8);
        scheme.FormBorderHeaderInactive2 = Color.FromArgb(0xE8, 0xF0, 0xC8);

        scheme.FormHeaderShortActive = Color.Black;
        scheme.FormHeaderLongActive = Color.Black;
        scheme.FormHeaderShortInactive = Color.FromArgb(0x80, 0x80, 0x80);
        scheme.FormHeaderLongInactive = Color.FromArgb(0x80, 0x80, 0x80);

        scheme.FormButtonBack1Track = Color.FromArgb(0xD7, 0xEB, 0x6E);
        scheme.FormButtonBack2Track = Color.FromArgb(0xBE, 0xD8, 0x49);
        scheme.FormButtonBorderTrack = Color.FromArgb(0xB3, 0xCA, 0x59);
        scheme.FormButtonBack1Pressed = Color.FromArgb(0xC5, 0xDC, 0x54);
        scheme.FormButtonBack2Pressed = Color.FromArgb(0xC4, 0xDC, 0x43);
        scheme.FormButtonBorderPressed = Color.FromArgb(0xA4, 0xB8, 0x3D);

        // Navigator buttons follow the same hover/pressed/selected progression as the form caption buttons.
        scheme.ButtonNavigatorTrack1 = Color.FromArgb(0xD7, 0xEB, 0x6E);
        scheme.ButtonNavigatorTrack2 = Color.FromArgb(0xBE, 0xD8, 0x49);
        scheme.ButtonNavigatorPressed1 = Color.FromArgb(0xC5, 0xDC, 0x54);
        scheme.ButtonNavigatorPressed2 = Color.FromArgb(0xC4, 0xDC, 0x43);
        scheme.ButtonNavigatorChecked1 = Color.FromArgb(0xD9, 0xEC, 0x77);
        scheme.ButtonNavigatorChecked2 = Color.FromArgb(0xBD, 0xD6, 0x49);
    }

    /// <summary>
    /// Applies the dark-mode Lime Green accents. Buttons keep the same bright lime accent colours as the
    /// light variant (with black text) so the "brand" colour reads consistently; every other chrome surface
    /// (panels, input/list controls, labels, grids, ribbon, menus, separators) moves to dark olive tones with
    /// pale lime text/borders. This is required because the Office 2007/2010 "Dark Mode" donor schemes still
    /// carry light-blue input/list/label colours that would otherwise leak through.
    /// </summary>
    private static void ApplyLimeDark(KryptonColorSchemeBase scheme)
    {
        Color buttonBack1 = Color.FromArgb(0xE2, 0xFB, 0xAF);
        Color buttonBack2 = Color.FromArgb(0xD7, 0xEE, 0x76);
        Color buttonBorder = Color.FromArgb(0xA8, 0xBC, 0x4D);
        Color lightText = Color.FromArgb(0xE8, 0xF5, 0xC0);
        Color mutedText = Color.FromArgb(0xA0, 0xA8, 0x88);
        Color panelDeep = Color.FromArgb(0x1A, 0x1F, 0x14);
        Color panelMid = Color.FromArgb(0x25, 0x2B, 0x1C);
        Color panelRaised = Color.FromArgb(0x2E, 0x38, 0x1C);
        Color panelHigh = Color.FromArgb(0x3A, 0x4A, 0x1E);
        Color panelAccent = Color.FromArgb(0x4A, 0x5C, 0x24);
        Color borderMuted = Color.FromArgb(0x55, 0x60, 0x3A);
        Color borderAccent = Color.FromArgb(0x7A, 0x8C, 0x3A);
        Color linkLime = Color.FromArgb(0xC4, 0xDC, 0x43);
        Color selectedTop = Color.FromArgb(0xD9, 0xEC, 0x77);
        Color selectedBottom = Color.FromArgb(0xBD, 0xD6, 0x49);

        // Buttons stay the same bright lime as the light variant so the accent colour is unmistakable.
        scheme.TextButtonNormal = Color.Black;
        scheme.TextButtonChecked = Color.Black;
        scheme.ButtonTextTracking = Color.Black;

        scheme.ButtonNormalBack1 = buttonBack1;
        scheme.ButtonNormalBack2 = buttonBack2;
        scheme.ButtonNormalBorder = buttonBorder;
        scheme.ButtonNormalDefaultBack1 = buttonBack1;
        scheme.ButtonNormalDefaultBack2 = buttonBack2;
        scheme.ButtonNormalDefaultBorder = buttonBorder;
        scheme.ButtonNormalNavigatorBack1 = buttonBack1;
        scheme.ButtonNormalNavigatorBack2 = buttonBack2;
        scheme.ButtonBorder = Color.FromArgb(0xA4, 0xB8, 0x3D);

        scheme.PanelClient = panelDeep;
        scheme.PanelAlternative = panelMid;
        scheme.ControlBorder = borderAccent;

        scheme.HeaderPrimaryBack1 = Color.FromArgb(0x3F, 0x50, 0x20);
        scheme.HeaderPrimaryBack2 = Color.FromArgb(0x52, 0x66, 0x28);
        scheme.HeaderSecondaryBack1 = panelRaised;
        scheme.HeaderSecondaryBack2 = panelAccent;
        scheme.HeaderText = lightText;
        scheme.HeaderDockInactiveBack1 = panelMid;
        scheme.HeaderDockInactiveBack2 = panelRaised;

        scheme.StatusStripLight = panelHigh;
        scheme.StatusStripDark = Color.FromArgb(0x2A, 0x35, 0x18);
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
        scheme.FormBorderActiveLight = Color.FromArgb(0x2A, 0x35, 0x18);
        scheme.FormBorderActiveDark = Color.FromArgb(0x3A, 0x4A, 0x20);
        scheme.FormBorderInactiveLight = Color.FromArgb(0x2A, 0x30, 0x22);
        scheme.FormBorderInactiveDark = Color.FromArgb(0x2A, 0x30, 0x22);

        scheme.FormBorderHeaderActive = borderAccent;
        scheme.FormBorderHeaderInactive = borderMuted;
        scheme.FormBorderHeaderActive1 = panelHigh;
        scheme.FormBorderHeaderActive2 = panelAccent;
        scheme.FormBorderHeaderInactive1 = Color.FromArgb(0x2A, 0x30, 0x22);
        scheme.FormBorderHeaderInactive2 = Color.FromArgb(0x2A, 0x30, 0x22);

        scheme.FormHeaderShortActive = lightText;
        scheme.FormHeaderLongActive = lightText;
        scheme.FormHeaderShortInactive = mutedText;
        scheme.FormHeaderLongInactive = mutedText;

        // Form caption buttons use dark olive greens on hover/press, with pale lime text for contrast.
        scheme.FormButtonBack1Track = panelHigh;
        scheme.FormButtonBack2Track = panelAccent;
        scheme.FormButtonBorderTrack = borderMuted;
        scheme.FormButtonBack1Pressed = Color.FromArgb(0x2A, 0x35, 0x18);
        scheme.FormButtonBack2Pressed = Color.FromArgb(0x3A, 0x4A, 0x20);
        scheme.FormButtonBorderPressed = borderAccent;
        scheme.FormButtonBack1Checked = panelAccent;
        scheme.FormButtonBack2Checked = panelHigh;
        scheme.FormButtonBorderCheck = borderAccent;
        scheme.FormButtonBack1CheckTrack = panelHigh;
        scheme.FormButtonBack2CheckTrack = Color.FromArgb(0x52, 0x66, 0x28);
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
        scheme.LinkVisitedOverrideControl = Color.FromArgb(0xA8, 0xBC, 0x4D);
        scheme.LinkPressedOverrideControl = buttonBack2;
        scheme.LinkNotVisitedOverridePanel = linkLime;
        scheme.LinkVisitedOverridePanel = Color.FromArgb(0xA8, 0xBC, 0x4D);
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
        scheme.GridListSelected = Color.FromArgb(0x52, 0x66, 0x28);
        scheme.GridSheetColNormal1 = panelDeep;
        scheme.GridSheetColNormal2 = panelMid;
        scheme.GridSheetColPressed1 = panelHigh;
        scheme.GridSheetColPressed2 = panelAccent;
        scheme.GridSheetColSelected1 = selectedTop;
        scheme.GridSheetColSelected2 = selectedBottom;
        scheme.GridSheetRowNormal = panelRaised;
        scheme.GridSheetRowPressed = panelHigh;
        scheme.GridSheetRowSelected = Color.FromArgb(0x52, 0x66, 0x28);
        scheme.GridDataCellBorder = borderMuted;
        scheme.GridDataCellSelected = Color.FromArgb(0x3F, 0x50, 0x20);

        scheme.NavigatorMiniBackColor = panelMid;
        scheme.ButtonNavigatorBorder = borderMuted;
        scheme.ButtonNavigatorText = lightText;
        scheme.ButtonNavigatorTrack1 = panelHigh;
        scheme.ButtonNavigatorTrack2 = panelAccent;
        scheme.ButtonNavigatorPressed1 = Color.FromArgb(0x2A, 0x35, 0x18);
        scheme.ButtonNavigatorPressed2 = Color.FromArgb(0x3A, 0x4A, 0x20);
        scheme.ButtonNavigatorChecked1 = Color.FromArgb(0x52, 0x66, 0x28);
        scheme.ButtonNavigatorChecked2 = Color.FromArgb(0x3F, 0x50, 0x20);

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
        scheme.RibbonTabHighlight3 = Color.FromArgb(0xBD, 0xD6, 0x49);
        scheme.RibbonTabHighlight4 = Color.FromArgb(0xC4, 0xDC, 0x43);
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
}
