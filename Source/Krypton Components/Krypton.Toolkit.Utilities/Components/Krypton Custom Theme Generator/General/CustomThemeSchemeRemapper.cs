#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Copies a donor <see cref="KryptonColorSchemeBase"/>, hue-shifts chromatic slots toward the seed,
/// then overwrites the Lime Green-equivalent accent set with derived tints and shades.
/// </summary>
internal static class CustomThemeSchemeRemapper
{
    internal static KryptonColorSchemeBase Remap(KryptonColorSchemeBase donor, CustomThemeAccentSet accents)
    {
        var target = new EmptySchemeBase();
        CopyColors(donor, target);
        ShiftSpectrum(donor, target, accents.Primary);
        ApplyAccents(target, accents);
        return target;
    }

    internal static CustomThemeAccentSet BuildAccents(KryptonCustomThemeSeed seed, bool dark)
    {
        Color primary = seed.Primary;
        Color secondary = seed.Secondary ?? CustomThemeColorMath.Analogous(primary, 30f);
        Color surface = seed.Surface ?? (dark
            ? CustomThemeColorMath.Darken(CommonHelper.MergeColors(Color.Black, 0.82f, primary, 0.18f), 0.05f)
            : CommonHelper.MergeColors(Color.White, 0.90f, primary, 0.10f));

        Color surfaceAlt = dark
            ? CustomThemeColorMath.Lighten(surface, 0.08f)
            : CustomThemeColorMath.Darken(surface, 0.06f);

        Color buttonBack1 = dark ? CustomThemeColorMath.Lighten(primary, 0.18f) : CustomThemeColorMath.Lighten(primary, 0.42f);
        Color buttonBack2 = dark ? primary : CustomThemeColorMath.Lighten(primary, 0.18f);
        Color buttonBorder = CustomThemeColorMath.Darken(primary, dark ? 0.08f : 0.18f);

        Color hoverTop = CustomThemeColorMath.Lighten(primary, dark ? 0.10f : 0.28f);
        Color hoverBottom = CustomThemeColorMath.Lighten(primary, dark ? 0.02f : 0.08f);
        Color hoverBorder = CustomThemeColorMath.Darken(primary, 0.12f);

        Color pressedTop = CustomThemeColorMath.Darken(primary, 0.06f);
        Color pressedBottom = CustomThemeColorMath.Darken(primary, 0.16f);
        Color pressedBorder = CustomThemeColorMath.Darken(primary, 0.24f);

        Color checkedTop = CustomThemeColorMath.Lighten(secondary, 0.12f);
        Color checkedBottom = secondary;
        Color checkedBorder = CustomThemeColorMath.Darken(secondary, 0.12f);

        Color disabledTop = dark ? CustomThemeColorMath.Lighten(surface, 0.10f) : CustomThemeColorMath.Darken(surface, 0.04f);
        Color disabledBottom = dark ? CustomThemeColorMath.Lighten(surface, 0.16f) : CustomThemeColorMath.Darken(surface, 0.10f);
        Color disabledBorder = dark ? CustomThemeColorMath.Lighten(surface, 0.22f) : CustomThemeColorMath.Darken(surface, 0.18f);

        Color onAccent = CustomThemeColorMath.ContrastText(buttonBack2);
        Color onSurface = CustomThemeColorMath.ContrastText(surface);
        Color muted = CustomThemeColorMath.MutedText(onSurface);

        Color headerBack1 = dark ? CustomThemeColorMath.Darken(secondary, 0.25f) : CustomThemeColorMath.Lighten(secondary, 0.22f);
        Color headerBack2 = dark ? CustomThemeColorMath.Darken(secondary, 0.08f) : secondary;

        return new CustomThemeAccentSet
        {
            Primary = primary,
            Secondary = secondary,
            Surface = surface,
            SurfaceAlt = surfaceAlt,
            ButtonBack1 = buttonBack1,
            ButtonBack2 = buttonBack2,
            ButtonBorder = buttonBorder,
            HoverTop = hoverTop,
            HoverBottom = hoverBottom,
            HoverBorder = hoverBorder,
            PressedTop = pressedTop,
            PressedBottom = pressedBottom,
            PressedBorder = pressedBorder,
            CheckedTop = checkedTop,
            CheckedBottom = checkedBottom,
            CheckedBorder = checkedBorder,
            DisabledTop = disabledTop,
            DisabledBottom = disabledBottom,
            DisabledBorder = disabledBorder,
            OnAccent = onAccent,
            OnSurface = onSurface,
            MutedText = muted,
            Link = dark ? CustomThemeColorMath.Lighten(primary, 0.18f) : CustomThemeColorMath.Darken(primary, 0.12f),
            LinkVisited = dark ? CustomThemeColorMath.Lighten(secondary, 0.10f) : CustomThemeColorMath.Darken(secondary, 0.08f),
            LinkPressed = buttonBack2,
            InputBack = dark ? CustomThemeColorMath.Darken(surface, 0.04f) : Color.White,
            InputBackDisabled = disabledTop,
            InputBorder = buttonBorder,
            HeaderBack1 = headerBack1,
            HeaderBack2 = headerBack2,
            HeaderSecondary1 = dark ? surfaceAlt : buttonBack1,
            HeaderSecondary2 = dark ? CustomThemeColorMath.Lighten(surface, 0.12f) : buttonBack2,
            FormBorder = buttonBorder,
            FormBorderInactive = disabledBorder,
            Dark = dark
        };
    }

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

    private static void ShiftSpectrum(KryptonColorSchemeBase donor, KryptonColorSchemeBase target, Color primary)
    {
        CustomThemeColorMath.ToHsl(primary, out float seedHue, out float seedSat, out _);
        float donorHue = ResolveDonorAccentHue(donor);
        float hueDelta = seedHue - donorHue;

        Type targetType = target.GetType();
        foreach (PropertyInfo property in targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (!property.CanRead || !property.CanWrite || property.PropertyType != typeof(Color))
            {
                continue;
            }

            object? current = property.GetValue(target);
            if (current is not Color source)
            {
                continue;
            }

            property.SetValue(target, CustomThemeColorMath.ShiftHue(source, hueDelta, seedSat));
        }
    }

    private static float ResolveDonorAccentHue(KryptonColorSchemeBase donor)
    {
        if (!CustomThemeColorMath.IsNeutral(donor.ButtonNormalBack2))
        {
            return donor.ButtonNormalBack2.GetHue();
        }

        if (!CustomThemeColorMath.IsNeutral(donor.HeaderPrimaryBack1))
        {
            return donor.HeaderPrimaryBack1.GetHue();
        }

        return 210f;
    }

    private static void ApplyAccents(KryptonColorSchemeBase scheme, CustomThemeAccentSet a)
    {
        scheme.TextButtonNormal = a.OnAccent;
        scheme.TextButtonChecked = a.OnAccent;
        scheme.ButtonTextTracking = a.OnAccent;

        scheme.ButtonNormalBack1 = a.ButtonBack1;
        scheme.ButtonNormalBack2 = a.ButtonBack2;
        scheme.ButtonNormalBorder = a.ButtonBorder;
        scheme.ButtonNormalDefaultBack1 = a.HoverTop;
        scheme.ButtonNormalDefaultBack2 = a.Primary;
        scheme.ButtonNormalDefaultBorder = a.ButtonBorder;
        scheme.ButtonNormalNavigatorBack1 = a.ButtonBack1;
        scheme.ButtonNormalNavigatorBack2 = a.ButtonBack2;
        scheme.ButtonBorder = a.PressedBorder;

        scheme.PanelClient = a.Surface;
        scheme.PanelAlternative = a.SurfaceAlt;
        scheme.ControlBorder = a.FormBorder;

        scheme.HeaderPrimaryBack1 = a.HeaderBack1;
        scheme.HeaderPrimaryBack2 = a.HeaderBack2;
        scheme.HeaderSecondaryBack1 = a.HeaderSecondary1;
        scheme.HeaderSecondaryBack2 = a.HeaderSecondary2;
        scheme.HeaderText = CustomThemeColorMath.ContrastText(a.HeaderBack2);

        scheme.StatusStripLight = a.HeaderBack1;
        scheme.StatusStripDark = a.HeaderBack2;
        scheme.StatusStripText = CustomThemeColorMath.ContrastText(a.HeaderBack2);

        scheme.ToolStripBegin = a.ButtonBack1;
        scheme.ToolStripMiddle = a.HeaderBack1;
        scheme.ToolStripEnd = a.HeaderBack2;
        scheme.ToolStripBack = a.SurfaceAlt;
        scheme.ToolStripBorder = a.ButtonBorder;
        scheme.ImageMargin = a.SurfaceAlt;

        scheme.FormBorderActive = a.FormBorder;
        scheme.FormBorderInactive = a.FormBorderInactive;
        scheme.FormBorderActiveLight = a.HeaderBack1;
        scheme.FormBorderActiveDark = a.HeaderBack2;
        scheme.FormBorderInactiveLight = a.DisabledTop;
        scheme.FormBorderInactiveDark = a.DisabledBottom;

        scheme.FormBorderHeaderActive = a.FormBorder;
        scheme.FormBorderHeaderInactive = a.FormBorderInactive;
        scheme.FormBorderHeaderActive1 = a.HeaderBack1;
        scheme.FormBorderHeaderActive2 = a.HeaderBack2;
        scheme.FormBorderHeaderInactive1 = a.DisabledTop;
        scheme.FormBorderHeaderInactive2 = a.DisabledBottom;

        scheme.FormHeaderShortActive = CustomThemeColorMath.ContrastText(a.HeaderBack2);
        scheme.FormHeaderLongActive = scheme.FormHeaderShortActive;
        scheme.FormHeaderShortInactive = a.MutedText;
        scheme.FormHeaderLongInactive = a.MutedText;

        scheme.FormButtonBack1Track = a.HoverTop;
        scheme.FormButtonBack2Track = a.HoverBottom;
        scheme.FormButtonBorderTrack = a.HoverBorder;
        scheme.FormButtonBack1Pressed = a.PressedTop;
        scheme.FormButtonBack2Pressed = a.PressedBottom;
        scheme.FormButtonBorderPressed = a.PressedBorder;
        scheme.FormButtonBack1Checked = a.CheckedTop;
        scheme.FormButtonBack2Checked = a.CheckedBottom;
        scheme.FormButtonBorderCheck = a.CheckedBorder;
        scheme.FormButtonBack1CheckTrack = a.HoverTop;
        scheme.FormButtonBack2CheckTrack = a.CheckedBottom;
        scheme.TextButtonFormNormal = scheme.FormHeaderShortActive;
        scheme.TextButtonFormTracking = CustomThemeColorMath.ContrastText(a.HoverBottom);
        scheme.TextButtonFormPressed = CustomThemeColorMath.ContrastText(a.PressedBottom);

        scheme.ButtonNavigatorTrack1 = a.HoverTop;
        scheme.ButtonNavigatorTrack2 = a.HoverBottom;
        scheme.ButtonNavigatorPressed1 = a.PressedTop;
        scheme.ButtonNavigatorPressed2 = a.PressedBottom;
        scheme.ButtonNavigatorChecked1 = a.CheckedTop;
        scheme.ButtonNavigatorChecked2 = a.CheckedBottom;

        scheme.TextLabelControl = a.OnSurface;
        scheme.TextLabelPanel = a.OnSurface;
        scheme.TextListItem = a.OnSurface;
        scheme.InputControlTextNormal = a.OnSurface;
        scheme.InputControlTextDisabled = a.MutedText;
        scheme.InputControlBorderNormal = a.InputBorder;
        scheme.InputControlBorderDisabled = a.DisabledBorder;
        scheme.InputControlBackNormal = a.InputBack;
        scheme.InputControlBackDisabled = a.InputBackDisabled;
        scheme.InputControlBackInactive = a.SurfaceAlt;
        scheme.InputDropDownNormal1 = a.OnSurface;
        scheme.InputDropDownNormal2 = a.InputBorder;
        scheme.InputDropDownDisabled1 = a.MutedText;
        scheme.InputDropDownDisabled2 = Color.Transparent;

        scheme.LinkNotVisitedOverrideControl = a.Link;
        scheme.LinkVisitedOverrideControl = a.LinkVisited;
        scheme.LinkPressedOverrideControl = a.LinkPressed;
        scheme.LinkNotVisitedOverridePanel = a.Link;
        scheme.LinkVisitedOverridePanel = a.LinkVisited;
        scheme.LinkPressedOverridePanel = a.LinkPressed;

        scheme.GridListNormal1 = a.Surface;
        scheme.GridListNormal2 = a.SurfaceAlt;
        scheme.GridListSelected = a.CheckedTop;
        scheme.ContextMenuHeadingBack = a.HeaderBack1;
        scheme.ContextMenuHeadingText = CustomThemeColorMath.ContrastText(a.HeaderBack1);
        scheme.ContextMenuImageColumn = a.SurfaceAlt;
        scheme.MenuItemText = a.OnSurface;
        scheme.MenuStripText = a.OnSurface;
        scheme.DisabledMenuItemText = a.MutedText;

        if (!a.Dark)
        {
            return;
        }

        scheme.OverflowBegin = a.HeaderBack1;
        scheme.OverflowMiddle = a.HeaderSecondary2;
        scheme.OverflowEnd = a.DisabledBorder;
        scheme.SeparatorLight = a.HeaderSecondary2;
        scheme.SeparatorDark = a.DisabledBorder;
        scheme.SeparatorHighBorder1 = a.HeaderBack1;
        scheme.SeparatorHighBorder2 = a.HeaderSecondary2;
        scheme.SeparatorHighInternalBorder1 = a.HeaderBack1;
        scheme.SeparatorHighInternalBorder2 = a.HeaderSecondary2;
        scheme.GripLight = a.OnSurface;
        scheme.GripDark = a.DisabledBorder;
        scheme.MenuMarginGradientStart = a.SurfaceAlt;
        scheme.MenuMarginGradientMiddle = a.HeaderSecondary1;
        scheme.MenuMarginGradientEnd = a.Surface;
        scheme.HeaderDockInactiveBack1 = a.SurfaceAlt;
        scheme.HeaderDockInactiveBack2 = a.HeaderSecondary2;
    }
}
