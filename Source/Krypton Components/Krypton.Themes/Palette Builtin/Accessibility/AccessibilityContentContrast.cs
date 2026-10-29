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
/// Content-colour overrides where shared scheme slots cannot satisfy both light and dark chrome
/// (e.g. dark-blue default buttons vs orange checked; pale secondary headers vs white <c>HeaderText</c>).
/// </summary>
internal static class AccessibilityContentContrast
{
    private static readonly Color DarkOnLight = Color.FromArgb(30, 57, 91);

    /// <summary>
    /// Deuteranopia: white text on dark-blue default/accept; dark text on pale secondary headers.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Element state.</param>
    /// <returns>Override colour, or <c>null</c> to keep the base palette value.</returns>
    public static Color? TryGetDeuteranopiaText(PaletteContentStyle style, PaletteState state) =>
        TryGetDefaultAcceptAndSecondaryHeaderText(style, state);

    /// <summary>
    /// Protanopia: white text on dark-blue default/accept; dark text on pale secondary headers.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Element state.</param>
    /// <returns>Override colour, or <c>null</c> to keep the base palette value.</returns>
    public static Color? TryGetProtanopiaText(PaletteContentStyle style, PaletteState state) =>
        TryGetDefaultAcceptAndSecondaryHeaderText(style, state);

    private static Color? TryGetDefaultAcceptAndSecondaryHeaderText(PaletteContentStyle style, PaletteState state)
    {
        if (IsSecondaryHeader(style))
        {
            return DarkOnLight;
        }

        // M365/Office GetContent* return EMPTY for override states (except link labels). The AcceptButton
        // uses NormalDefaultOverride for the dark default fill, then content falls back to Normal → dark
        // TextButtonNormal unless we supply white here.
        if (IsButtonFamily(style) && state == PaletteState.NormalDefaultOverride)
        {
            return Color.White;
        }

        return null;
    }

    private static bool IsSecondaryHeader(PaletteContentStyle style) =>
        style == PaletteContentStyle.HeaderSecondary;

    private static bool IsButtonFamily(PaletteContentStyle style)
    {
        switch (style)
        {
            case PaletteContentStyle.ButtonStandalone:
            case PaletteContentStyle.ButtonGallery:
            case PaletteContentStyle.ButtonAlternate:
            case PaletteContentStyle.ButtonCluster:
            case PaletteContentStyle.ButtonCustom1:
            case PaletteContentStyle.ButtonCustom2:
            case PaletteContentStyle.ButtonCustom3:
                return true;
            default:
                return false;
        }
    }
}
