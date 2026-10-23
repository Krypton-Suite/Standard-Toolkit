#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

internal static class AquaRenderHelper
{
    internal static bool IsAquaGelBack(IPaletteBack? palette, PaletteState state)
    {
        if (palette == null || CommonHelper.IsOverrideState(state))
        {
            return false;
        }

        return palette.GetBackColorStyle(state) == PaletteColorStyle.Linear;
    }

    internal static void DrawGelRectangle(Graphics g, Rectangle rect, Color top, Color bottom, float cornerRadius, bool glossyHighlight = false)
    {
        if (rect.Width <= 0 || rect.Height <= 0)
        {
            return;
        }

        var oldSmoothing = g.SmoothingMode;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        try
        {
            using var path = CreateRoundedRect(rect, cornerRadius);
            using var brush = new LinearGradientBrush(
                rect,
                top,
                bottom,
                LinearGradientMode.Vertical);
            g.FillPath(brush, path);

            if (glossyHighlight)
            {
                int highlightHeight = Math.Max(2, rect.Height / 2);
                var highlightRect = new Rectangle(rect.X, rect.Y, rect.Width, highlightHeight);
                using var highlightPath = CreateRoundedRect(highlightRect, cornerRadius);
                using var highlightBrush = new LinearGradientBrush(
                    highlightRect,
                    Color.FromArgb(150, Color.White),
                    Color.FromArgb(0, Color.White),
                    LinearGradientMode.Vertical);
                g.FillPath(highlightBrush, highlightPath);
            }
        }
        finally
        {
            g.SmoothingMode = oldSmoothing;
        }
    }

    internal static void DrawPinstripeHeader(Graphics g, Rectangle rect, Color stripe, Color gap)
    {
        if (rect.Width <= 0 || rect.Height <= 0)
        {
            return;
        }

        using var baseBrush = new SolidBrush(gap);
        g.FillRectangle(baseBrush, rect);

        using var pen = new Pen(stripe, 1f);
        for (int x = rect.Left + 2; x < rect.Right; x += 4)
        {
            g.DrawLine(pen, x, rect.Top, x, rect.Bottom - 1);
        }
    }

    private static GraphicsPath CreateRoundedRect(Rectangle rect, float radius)
    {
        var path = new GraphicsPath();
        if (rect.Width <= 0 || rect.Height <= 0)
        {
            return path;
        }

        float d = radius * 2f;
        if (d > rect.Width)
        {
            d = rect.Width;
        }

        if (d > rect.Height)
        {
            d = rect.Height;
        }

        path.AddArc(rect.Left, rect.Top, d, d, 180, 90);
        path.AddArc(rect.Right - d, rect.Top, d, d, 270, 90);
        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
        path.AddArc(rect.Left, rect.Bottom - d, d, d, 90, 90);
        path.CloseFigure();
        return path;
    }
}