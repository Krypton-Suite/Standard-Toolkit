#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Themes;

/// <summary>
/// Ribbon surface colours and style overrides shared by the Office 2024 palettes.
/// Control chrome stays on the Microsoft 365 base; only the ribbon back and text change.
/// </summary>
internal sealed class Office2024RibbonChrome
{
    /// <summary>
    /// Initializes the ribbon colours for one Office 2024 variant.
    /// </summary>
    /// <param name="body">Flat ribbon body, group area, and quick access toolbar.</param>
    /// <param name="tabText">Inactive tab and File text.</param>
    /// <param name="tabTextSelected">Selected tab and pressed File text.</param>
    /// <param name="underline">Selected-tab underline.</param>
    /// <param name="hover">Tab hover fill.</param>
    /// <param name="groupTitle">Group caption text.</param>
    public Office2024RibbonChrome(Color body, Color tabText, Color tabTextSelected, Color underline, Color hover, Color groupTitle)
    {
        Body = body;
        TabText = tabText;
        TabTextSelected = tabTextSelected;
        Underline = underline;
        Hover = hover;
        GroupTitle = groupTitle;
    }

    /// <summary>Flat ribbon body.</summary>
    public Color Body { get; }

    /// <summary>Inactive tab text.</summary>
    public Color TabText { get; }

    /// <summary>Selected tab text.</summary>
    public Color TabTextSelected { get; }

    /// <summary>Selected-tab underline.</summary>
    public Color Underline { get; }

    /// <summary>Tab hover fill.</summary>
    public Color Hover { get; }

    /// <summary>Group caption text.</summary>
    public Color GroupTitle { get; }

    /// <summary>
    /// Maps ribbon back styles onto the Office 2024 color styles, leaving other styles on the base palette.
    /// </summary>
    /// <param name="style">Ribbon background style.</param>
    /// <param name="state">Palette state.</param>
    /// <param name="inherited">Style returned by the Microsoft 365 base.</param>
    /// <returns>Office 2024 style when this surface owns the slot; otherwise <paramref name="inherited"/>.</returns>
    public PaletteRibbonColorStyle AdjustBackColorStyle(PaletteRibbonBackStyle style, PaletteState state, PaletteRibbonColorStyle inherited)
    {
        switch (style)
        {
            case PaletteRibbonBackStyle.RibbonTab:
                if (IsSelectedTab(state))
                {
                    return PaletteRibbonColorStyle.RibbonTabSelected2024;
                }

                if (IsTrackingTab(state) || state == PaletteState.FocusOverride)
                {
                    return PaletteRibbonColorStyle.RibbonTabTracking2024;
                }

                return PaletteRibbonColorStyle.Empty;

            case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
            case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                return state == PaletteState.Normal || state == PaletteState.ContextNormal
                    ? PaletteRibbonColorStyle.Empty
                    : inherited;

            case PaletteRibbonBackStyle.RibbonGroupArea:
                return PaletteRibbonColorStyle.RibbonGroupArea2024;

            case PaletteRibbonBackStyle.RibbonQATMinibar:
            case PaletteRibbonBackStyle.RibbonQATFullbar:
            case PaletteRibbonBackStyle.RibbonQATOverflow:
                return PaletteRibbonColorStyle.Solid;

            default:
                return inherited;
        }
    }

    /// <summary>
    /// Supplies color 1 for the Office 2024 ribbon surfaces.
    /// </summary>
    /// <param name="style">Ribbon background style.</param>
    /// <param name="state">Palette state.</param>
    /// <param name="tabStrip">Tab-strip colour used to keep selected and hover underlines visible.</param>
    /// <param name="color">Color when this method returns <see langword="true"/>.</param>
    /// <returns><see langword="true"/> when the Office 2024 chrome owns the color.</returns>
    public bool TryGetBackColor1(PaletteRibbonBackStyle style, PaletteState state, Color tabStrip, out Color color)
    {
        switch (style)
        {
            case PaletteRibbonBackStyle.RibbonTab:
                color = IsSelectedTab(state) ? TabUnderlineOn(tabStrip) : TabHoverLineOn(tabStrip);
                return true;
            case PaletteRibbonBackStyle.RibbonGroupArea:
            case PaletteRibbonBackStyle.RibbonQATMinibar:
            case PaletteRibbonBackStyle.RibbonQATFullbar:
            case PaletteRibbonBackStyle.RibbonQATOverflow:
                color = Body;
                return true;
            default:
                color = Color.Empty;
                return false;
        }
    }

    /// <summary>
    /// Tab text that stays readable on <paramref name="tabStrip"/>.
    /// </summary>
    /// <param name="state">Palette state.</param>
    /// <param name="tabStrip">Tab-strip colour, <see cref="PaletteBackStyle.PanelClient"/>.</param>
    /// <returns>Selected or inactive tab text.</returns>
    public Color TabTextOn(PaletteState state, Color tabStrip)
    {
        Color preferred = IsSelectedTab(state) ? TabTextSelected : TabText;
        return Readable(preferred, tabStrip, Luminance(tabStrip) >= 140000 ? Color.FromArgb(32, 32, 32) : Color.White);
    }

    /// <summary>
    /// Selected-tab underline that stays visible on <paramref name="tabStrip"/>.
    /// </summary>
    /// <param name="tabStrip">Tab-strip colour, <see cref="PaletteBackStyle.PanelClient"/>.</param>
    /// <returns>Underline colour.</returns>
    public Color MarkOn(Color tabStrip) =>
        Readable(Underline, tabStrip, Luminance(tabStrip) >= 140000 ? Color.FromArgb(15, 108, 189) : Color.White);

    /// <summary>
    /// Selected-tab line that stays visible on <paramref name="tabStrip"/> and distinct from the selected label.
    /// </summary>
    /// <param name="tabStrip">Tab-strip colour, <see cref="PaletteBackStyle.PanelClient"/>.</param>
    /// <returns>Underline colour.</returns>
    public Color TabUnderlineOn(Color tabStrip)
    {
        if (Contrast(Underline, tabStrip) >= 3d && !Similar(Underline, TabTextSelected))
        {
            return Underline;
        }

        bool lightStrip = Luminance(tabStrip) >= 140000;
        Color accent = lightStrip ? Color.FromArgb(15, 108, 189) : Color.FromArgb(180, 214, 250);
        if (Contrast(accent, tabStrip) >= 3d && !Similar(accent, TabTextSelected))
        {
            return accent;
        }

        Color alternate = lightStrip ? Color.FromArgb(15, 40, 80) : Color.White;
        return Contrast(alternate, tabStrip) >= 3d ? alternate : accent;
    }

    /// <summary>
    /// Grey line drawn under a tab while the pointer is over it.
    /// </summary>
    /// <param name="tabStrip">Tab-strip colour, <see cref="PaletteBackStyle.PanelClient"/>.</param>
    /// <returns>Hover line colour.</returns>
    public Color TabHoverLineOn(Color tabStrip)
    {
        bool lightStrip = Luminance(tabStrip) >= 140000;
        Color grey = lightStrip ? Color.FromArgb(120, 120, 120) : Color.FromArgb(190, 190, 190);
        if (Contrast(grey, tabStrip) >= 2.2d)
        {
            return grey;
        }

        return lightStrip ? Color.FromArgb(80, 80, 80) : Color.FromArgb(220, 220, 220);
    }

    /// <summary>
    /// Supplies ribbon text colours for tabs and group captions.
    /// </summary>
    /// <param name="style">Ribbon text style.</param>
    /// <param name="state">Palette state.</param>
    /// <param name="tabStrip">Tab-strip colour used for tab text. Group text ignores it.</param>
    /// <param name="color">Color when this method returns <see langword="true"/>.</param>
    /// <returns><see langword="true"/> when the Office 2024 chrome owns the color.</returns>
    public bool TryGetTextColor(PaletteRibbonTextStyle style, PaletteState state, Color tabStrip, out Color color)
    {
        if (state == PaletteState.Disabled)
        {
            color = Color.Empty;
            return false;
        }

        switch (style)
        {
            case PaletteRibbonTextStyle.RibbonTab:
                color = TabTextOn(state, tabStrip);
                return true;
            case PaletteRibbonTextStyle.RibbonGroupNormalTitle:
                color = GroupTitle;
                return true;
            case PaletteRibbonTextStyle.RibbonGroupButtonText:
            case PaletteRibbonTextStyle.RibbonGroupLabelText:
            case PaletteRibbonTextStyle.RibbonGroupCheckBoxText:
            case PaletteRibbonTextStyle.RibbonGroupRadioButtonText:
            case PaletteRibbonTextStyle.RibbonGroupCollapsedText:
                // Dark ribbons use a lighter selected-tab colour. Button text follows that so it stays readable on the flat body.
                color = ButtonText;
                return true;
            default:
                color = Color.Empty;
                return false;
        }
    }

    // ContextChecked* does not set PaletteState.Checked, but those tabs are still the selected tab.
    private static bool IsSelectedTab(PaletteState state) =>
        (state & PaletteState.Checked) == PaletteState.Checked
        || state == PaletteState.ContextCheckedNormal
        || state == PaletteState.ContextCheckedTracking;

    private static bool IsTrackingTab(PaletteState state) =>
        state == PaletteState.Tracking
        || state == PaletteState.Pressed
        || state == PaletteState.ContextTracking;

    private Color ButtonText => Luminance(TabTextSelected) > Luminance(TabText) ? TabTextSelected : TabText;

    private static Color Readable(Color preferred, Color background, Color fallback) =>
        Contrast(preferred, background) >= 3d ? preferred : fallback;

    private static double Contrast(Color a, Color b)
    {
        double lighter = Math.Max(RelativeLuminance(a), RelativeLuminance(b));
        double darker = Math.Min(RelativeLuminance(a), RelativeLuminance(b));
        return (lighter + 0.05d) / (darker + 0.05d);
    }

    private static double RelativeLuminance(Color color) =>
        (0.2126d * Channel(color.R)) + (0.7152d * Channel(color.G)) + (0.0722d * Channel(color.B));

    private static double Channel(int component)
    {
        double c = component / 255d;
        return c <= 0.03928d ? c / 12.92d : Math.Pow((c + 0.055d) / 1.055d, 2.4d);
    }

    private static int Luminance(Color color) => (color.R * 299) + (color.G * 587) + (color.B * 114);

    private static bool Similar(Color a, Color b) =>
        Math.Abs(a.R - b.R) < 24 && Math.Abs(a.G - b.G) < 24 && Math.Abs(a.B - b.B) < 24;
}
