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
/// Office 2024 White ribbon on the Microsoft 365 White control chrome.
/// </summary>
public class PaletteOffice2024White : PaletteMicrosoft365White
{
    private static readonly Office2024RibbonChrome _chrome = new Office2024RibbonChrome(
        Color.White,
        Color.FromArgb(90, 90, 90),
        Color.FromArgb(32, 32, 32),
        Color.FromArgb(15, 108, 189),
        Color.FromArgb(243, 242, 241),
        Color.FromArgb(96, 94, 92));

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
