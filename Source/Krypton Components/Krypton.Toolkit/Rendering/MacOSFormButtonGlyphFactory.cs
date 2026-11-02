#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Draws macOS traffic-light window control glyphs for form button specs.
/// </summary>
internal static class MacOSFormButtonGlyphFactory
{
    internal enum TrafficLightKind
    {
        Close,
        Minimize,
        Zoom
    }

    private static readonly Color CloseNormal = Color.FromArgb(255, 95, 87);
    private static readonly Color CloseHover = Color.FromArgb(255, 112, 105);
    private static readonly Color ClosePressed = Color.FromArgb(232, 78, 70);
    private static readonly Color MinimizeNormal = Color.FromArgb(255, 189, 46);
    private static readonly Color MinimizeHover = Color.FromArgb(255, 200, 74);
    private static readonly Color MinimizePressed = Color.FromArgb(224, 158, 0);
    private static readonly Color ZoomNormal = Color.FromArgb(40, 200, 64);
    private static readonly Color ZoomHover = Color.FromArgb(72, 214, 92);
    private static readonly Color ZoomPressed = Color.FromArgb(24, 166, 48);
    /// <summary>Inactive window traffic lights (macOS dims all three equally).</summary>
    private static readonly Color InactiveLight = Color.FromArgb(198, 198, 200);

    private static readonly Color InactiveDark = Color.FromArgb(96, 96, 100);

    internal static Image CreateTrafficLight(TrafficLightKind kind, PaletteState state, Size size, bool isDarkSurface)
    {
        var bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
        using var g = Graphics.FromImage(bmp);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
        g.Clear(Color.Transparent);

        if (!TryGetFillColor(kind, state, isDarkSurface, out Color fill))
        {
            return bmp;
        }

        float diameter = Math.Min(size.Width, size.Height) - 2f;
        float x = (size.Width - diameter) / 2f;
        float y = (size.Height - diameter) / 2f;
        using (var brush = new SolidBrush(fill))
        {
            g.FillEllipse(brush, x, y, diameter, diameter);
        }

        if (state is PaletteState.Tracking or PaletteState.Pressed or PaletteState.CheckedTracking or PaletteState.CheckedPressed)
        {
            DrawSymbol(g, kind, new RectangleF(x, y, diameter, diameter), isDarkSurface);
        }

        return bmp;
    }

    private static void DrawSymbol(Graphics g, TrafficLightKind kind, RectangleF circle, bool isDarkSurface)
    {
        using var pen = new Pen(isDarkSurface ? Color.FromArgb(40, 0, 0, 0) : Color.FromArgb(64, 0, 0, 0), 1.2f);
        float cx = circle.Left + (circle.Width / 2f);
        float cy = circle.Top + (circle.Height / 2f);
        float r = circle.Width * 0.18f;

        switch (kind)
        {
            case TrafficLightKind.Close:
                g.DrawLine(pen, cx - r, cy - r, cx + r, cy + r);
                g.DrawLine(pen, cx + r, cy - r, cx - r, cy + r);
                break;
            case TrafficLightKind.Minimize:
                g.DrawLine(pen, cx - r, cy, cx + r, cy);
                break;
            case TrafficLightKind.Zoom:
                g.DrawLine(pen, cx, cy - r, cx, cy + r);
                g.DrawLine(pen, cx - r, cy, cx + r, cy);
                break;
        }
    }

    private static bool TryGetFillColor(TrafficLightKind kind, PaletteState state, bool isDarkSurface, out Color fill)
    {
        if (state == PaletteState.Disabled)
        {
            fill = isDarkSurface ? InactiveDark : InactiveLight;
            return true;
        }

        bool pressed = state is PaletteState.Pressed or PaletteState.CheckedPressed;
        bool tracking = state is PaletteState.Tracking or PaletteState.CheckedTracking;

        switch (kind)
        {
            case TrafficLightKind.Close:
                fill = pressed ? ClosePressed : tracking ? CloseHover : CloseNormal;
                return true;
            case TrafficLightKind.Minimize:
                fill = pressed ? MinimizePressed : tracking ? MinimizeHover : MinimizeNormal;
                return true;
            case TrafficLightKind.Zoom:
                fill = pressed ? ZoomPressed : tracking ? ZoomHover : ZoomNormal;
                return true;
            default:
                fill = Color.Empty;
                return false;
        }
    }
}