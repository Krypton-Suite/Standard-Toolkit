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
/// Office 2024 Dark Gray ribbon on the Microsoft 365 Dark Gray control chrome.
/// </summary>
public class PaletteOffice2024DarkGray : PaletteMicrosoft365DarkGray
{
    private static readonly Office2024RibbonChrome _chrome = new Office2024RibbonChrome(
        Color.FromArgb(45, 45, 45),
        Color.FromArgb(180, 180, 180),
        Color.White,
        Color.FromArgb(230, 230, 230),
        Color.FromArgb(62, 62, 62),
        Color.FromArgb(170, 170, 170));

    /// <inheritdoc />
    public override IRenderer GetRenderer() => KryptonManager.RenderOffice2024;

    /// <inheritdoc />
    public override PaletteRibbonShape GetRibbonShape() => PaletteRibbonShape.Office2024;

    /// <inheritdoc />
    public override PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteRibbonBackStyle style, PaletteState state) =>
        _chrome.AdjustBackColorStyle(style, state, base.GetRibbonBackColorStyle(style, state));

    /// <inheritdoc />
    public override Color GetRibbonBackColor1(PaletteRibbonBackStyle style, PaletteState state) =>
        _chrome.TryGetBackColor1(style, state, out Color color) ? color : base.GetRibbonBackColor1(style, state);

    /// <inheritdoc />
    public override Color GetRibbonTextColor(PaletteRibbonTextStyle style, PaletteState state) =>
        _chrome.TryGetTextColor(style, state, GetBackColor1(PaletteBackStyle.PanelClient, state), out Color color)
            ? color
            : base.GetRibbonTextColor(style, state);

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) =>
        GetBackColor1(PaletteBackStyle.PanelClient, state);

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTopColor(PaletteState state) =>
        _chrome.MarkOn(GetBackColor1(PaletteBackStyle.PanelClient, state));

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabBottomColor(PaletteState state) => _chrome.Hover;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTextColor(PaletteState state) =>
        _chrome.TabTextOn(state == PaletteState.Pressed ? PaletteState.CheckedNormal : state, GetBackColor1(PaletteBackStyle.PanelClient, state));
}
