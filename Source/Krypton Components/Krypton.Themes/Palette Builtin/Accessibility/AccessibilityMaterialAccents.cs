#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Themes;

/// <summary>
/// Material button chrome for accessibility themes. Material ignores M365 <c>SetArrayColor</c>
/// button LUTs and paints flat surface overlays, so a11y accents are applied here instead.
/// </summary>
internal static class AccessibilityMaterialAccents
{
    /// <summary>
    /// Accent fill colours for Material accessibility button states.
    /// </summary>
    internal readonly struct AccentSet
    {
        public AccentSet(Color primary, Color secondary, Color secondaryLight, Color secondaryDeep, Color track, Color pressed)
        {
            Primary = primary;
            Secondary = secondary;
            SecondaryLight = secondaryLight;
            SecondaryDeep = secondaryDeep;
            Track = track;
            Pressed = pressed;
        }

        public Color Primary { get; }
        public Color Secondary { get; }
        public Color SecondaryLight { get; }
        public Color SecondaryDeep { get; }
        public Color Track { get; }
        public Color Pressed { get; }
    }

    public static AccentSet HighContrast { get; } = new AccentSet(
        primary: Color.FromArgb(0, 255, 0),
        secondary: Color.FromArgb(255, 255, 0),
        secondaryLight: Color.FromArgb(0, 255, 255),
        secondaryDeep: Color.FromArgb(0, 200, 0),
        track: Color.FromArgb(0, 220, 220),
        pressed: Color.FromArgb(220, 220, 0));

    public static AccentSet Deuteranopia { get; } = new AccentSet(
        primary: Color.FromArgb(0, 114, 178),
        secondary: Color.FromArgb(180, 110, 0),
        secondaryLight: Color.FromArgb(255, 200, 80),
        secondaryDeep: Color.FromArgb(150, 90, 0),
        track: Color.FromArgb(255, 220, 150),
        pressed: Color.FromArgb(200, 130, 0));

    public static AccentSet Protanopia { get; } = new AccentSet(
        primary: Color.FromArgb(0, 90, 181),
        secondary: Color.FromArgb(153, 79, 0),
        secondaryLight: Color.FromArgb(200, 130, 50),
        secondaryDeep: Color.FromArgb(120, 60, 0),
        track: Color.FromArgb(235, 205, 170),
        pressed: Color.FromArgb(130, 65, 0));

    /// <summary>
    /// Returns an accent fill for Material button chrome, or <c>null</c> to keep the base surface overlay.
    /// </summary>
    public static Color? TryGetButtonBack(PaletteState state, AccentSet accents)
    {
        switch (state)
        {
            case PaletteState.NormalDefaultOverride:
                return accents.Primary;
            case PaletteState.CheckedNormal:
                return accents.Secondary;
            case PaletteState.CheckedTracking:
                return accents.SecondaryLight;
            case PaletteState.CheckedPressed:
                return accents.SecondaryDeep;
            case PaletteState.Tracking:
                return accents.Track;
            case PaletteState.Pressed:
                return accents.Pressed;
            default:
                return null;
        }
    }

    /// <summary>
    /// Returns true when <paramref name="style"/> uses Material button chrome.
    /// </summary>
    public static bool IsButtonBackStyle(PaletteBackStyle style)
    {
        switch (style)
        {
            case PaletteBackStyle.ButtonStandalone:
            case PaletteBackStyle.ButtonAlternate:
            case PaletteBackStyle.ButtonLowProfile:
            case PaletteBackStyle.ButtonBreadCrumb:
            case PaletteBackStyle.ButtonListItem:
            case PaletteBackStyle.ButtonCommand:
            case PaletteBackStyle.ButtonButtonSpec:
            case PaletteBackStyle.ButtonCluster:
            case PaletteBackStyle.ButtonCustom1:
            case PaletteBackStyle.ButtonCustom2:
            case PaletteBackStyle.ButtonCustom3:
            case PaletteBackStyle.ContextMenuItemHighlight:
                return true;
            default:
                return false;
        }
    }
}