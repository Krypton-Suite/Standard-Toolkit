#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// High-contrast overrides for family bases that hardcode white for
/// <see cref="PaletteBackStyle.ControlClient"/> / context-menu backs (combo drop-downs, etc.).
/// </summary>
internal static class AccessibilityHighContrastChrome
{
    /// <summary>
    /// Returns a black surface colour for drop-down / control-client chrome, otherwise <c>null</c>.
    /// </summary>
    /// <param name="style">Requested background style.</param>
    /// <returns>Black when the style should follow high-contrast surfaces; otherwise <c>null</c>.</returns>
    public static Color? TryGetSurfaceBack(PaletteBackStyle style)
    {
        switch (style)
        {
            case PaletteBackStyle.Control:
            case PaletteBackStyle.ControlClient:
            case PaletteBackStyle.ControlAlternate:
            case PaletteBackStyle.ControlCustom1:
            case PaletteBackStyle.ControlCustom2:
            case PaletteBackStyle.ControlCustom3:
            case PaletteBackStyle.ContextMenuOuter:
            case PaletteBackStyle.ContextMenuInner:
                return Color.Black;
            default:
                return null;
        }
    }
}
