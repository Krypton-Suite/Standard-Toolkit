#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal static class DropDownArrowGlyphMetrics
{
    internal const int DefaultBaseSize = DropDownArrowGlyphDefaults.DefaultBaseSizeAt96Dpi;

    /// <summary>
    /// Resolves the square pixel size for a drop-down arrow glyph at the current DPI,
    /// honouring <see cref="PaletteMetricInt.DropDownArrowBaseSize"/> and clamping to the available cell.
    /// </summary>
    internal static int ResolvePixelSize(float dpiY, Rectangle cellRect, Control? control, PaletteState state = PaletteState.Normal)
    {
        int baseSize = GetBaseSize(control, state);
        int square = Math.Min(cellRect.Width, cellRect.Height);
        square = Math.Min(square, baseSize);
        square = (int)(square * dpiY / 96f);
        return Math.Max(0, square);
    }

    internal static int ResolvePixelSize(Graphics g, Rectangle cellRect, Control? control, PaletteState state = PaletteState.Normal)
        => ResolvePixelSize(g.DpiY, cellRect, control, state);

    private static int GetBaseSize(Control? control, PaletteState state)
    {
        var paletteBase = KryptonManager.CurrentGlobalPalette;
        if (paletteBase == null)
        {
            return DefaultBaseSize;
        }

        KryptonForm? owningForm = FindOwningKryptonForm(control);
        int metric = paletteBase.GetMetricInt(owningForm, state, PaletteMetricInt.DropDownArrowBaseSize);
        return metric > 0 ? metric : DefaultBaseSize;
    }

    private static KryptonForm? FindOwningKryptonForm(Control? control)
    {
        for (Control? current = control; current != null; current = current.Parent)
        {
            if (current is KryptonForm form)
            {
                return form;
            }
        }

        return null;
    }
}
