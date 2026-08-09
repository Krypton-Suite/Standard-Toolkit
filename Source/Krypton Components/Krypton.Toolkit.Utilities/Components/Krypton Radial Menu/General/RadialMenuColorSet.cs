#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Resolved colours for painting a radial menu from a <see cref="PaletteBase"/> and optional value overrides.
/// </summary>
internal readonly struct RadialMenuColorSet
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RadialMenuColorSet"/> struct.
    /// </summary>
    public RadialMenuColorSet(
        Color surface,
        Color outerRing,
        Color sectorNormal,
        Color sectorTracking,
        Color sectorPressed,
        Color sectorChecked,
        Color sectorDisabled,
        Color border,
        Color borderTracking,
        Color text,
        Color textDisabled,
        Color center,
        Color centerGlyph,
        Color submenuMarker)
    {
        Surface = surface;
        OuterRing = outerRing;
        SectorNormal = sectorNormal;
        SectorTracking = sectorTracking;
        SectorPressed = sectorPressed;
        SectorChecked = sectorChecked;
        SectorDisabled = sectorDisabled;
        Border = border;
        BorderTracking = borderTracking;
        Text = text;
        TextDisabled = textDisabled;
        Center = center;
        CenterGlyph = centerGlyph;
        SubmenuMarker = submenuMarker;
    }

    public Color Surface { get; }
    public Color OuterRing { get; }
    public Color SectorNormal { get; }
    public Color SectorTracking { get; }
    public Color SectorPressed { get; }
    public Color SectorChecked { get; }
    public Color SectorDisabled { get; }
    public Color Border { get; }
    public Color BorderTracking { get; }
    public Color Text { get; }
    public Color TextDisabled { get; }
    public Color Center { get; }
    public Color CenterGlyph { get; }
    public Color SubmenuMarker { get; }

    /// <summary>
    /// Builds a colour set from the active palette and optional <see cref="KryptonRadialMenuValues"/> overrides.
    /// </summary>
    /// <param name="palette">Drawing palette; when null, uses the current global palette.</param>
    /// <param name="values">Menu values (MenuColor / SubMenuHoverColor overrides).</param>
    /// <returns>Resolved colour set.</returns>
    public static RadialMenuColorSet FromPalette(PaletteBase? palette, KryptonRadialMenuValues values)
    {
        palette ??= KryptonManager.CurrentGlobalPalette;

        // Slices use PanelClient; the outer ring uses PanelAlternate.
        var sectorNormal = SafeBack(palette, PaletteBackStyle.PanelClient, PaletteState.Normal, SystemColors.Window);
        var outerRing = SafeBack(palette, PaletteBackStyle.PanelAlternate, PaletteState.Normal, SystemColors.ControlDark);
        var surface = outerRing;
        var sectorTracking = SafeBack(palette, PaletteBackStyle.ContextMenuItemHighlight, PaletteState.Tracking, ControlPaint.Light(sectorNormal));
        var sectorPressed = SafeBack(palette, PaletteBackStyle.ContextMenuItemHighlight, PaletteState.Pressed, ControlPaint.Dark(sectorTracking));
        var sectorChecked = SafeBack(palette, PaletteBackStyle.ContextMenuItemHighlight, PaletteState.CheckedNormal, sectorTracking);
        var sectorDisabled = SafeBack(palette, PaletteBackStyle.PanelClient, PaletteState.Disabled, SystemColors.ControlLight);

        var border = SafeBorder(palette, PaletteBorderStyle.ControlClient, PaletteState.Normal, SystemColors.ControlDark);
        var borderTracking = values.SubMenuHoverColor.IsEmpty
            ? SafeBorder(palette, PaletteBorderStyle.ContextMenuItemHighlight, PaletteState.Tracking, border)
            : values.SubMenuHoverColor;

        var text = SafeContent(palette, PaletteContentStyle.LabelNormalControl, PaletteState.Normal, SystemColors.ControlText);
        var textDisabled = SafeContent(palette, PaletteContentStyle.LabelNormalControl, PaletteState.Disabled, SystemColors.GrayText);

        var center = values.MenuColor.IsEmpty
            ? SafeBack(palette, PaletteBackStyle.ButtonStandalone, PaletteState.Normal, SystemColors.Highlight)
            : values.MenuColor;
        var centerGlyph = SafeContent(palette, PaletteContentStyle.ButtonStandalone, PaletteState.Normal, SystemColors.HighlightText);
        var submenuMarker = SafeContent(palette, PaletteContentStyle.LabelNormalPanel, PaletteState.Normal, border);

        return new RadialMenuColorSet(
            surface,
            outerRing,
            sectorNormal,
            sectorTracking,
            sectorPressed,
            sectorChecked,
            sectorDisabled,
            border,
            borderTracking,
            text,
            textDisabled,
            center,
            centerGlyph,
            submenuMarker);
    }

    private static Color SafeBack(PaletteBase palette, PaletteBackStyle style, PaletteState state, Color fallback)
    {
        try
        {
            var color = palette.GetBackColor1(style, state);
            return color.IsEmpty ? fallback : color;
        }
        catch
        {
            return fallback;
        }
    }

    private static Color SafeBorder(PaletteBase palette, PaletteBorderStyle style, PaletteState state, Color fallback)
    {
        try
        {
            var color = palette.GetBorderColor1(style, state);
            return color.IsEmpty ? fallback : color;
        }
        catch
        {
            return fallback;
        }
    }

    private static Color SafeContent(PaletteBase palette, PaletteContentStyle style, PaletteState state, Color fallback)
    {
        try
        {
            var color = palette.GetContentShortTextColor1(style, state);
            return color.IsEmpty ? fallback : color;
        }
        catch
        {
            return fallback;
        }
    }
}
