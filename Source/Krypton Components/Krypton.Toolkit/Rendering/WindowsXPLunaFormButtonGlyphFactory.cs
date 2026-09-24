#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Draws Windows XP Luna-style caption buttons (red close, blue chrome min/max).
/// </summary>
internal static class WindowsXPLunaFormButtonGlyphFactory
{
    internal enum CaptionButtonKind
    {
        Close,
        Minimize,
        Maximize,
        Restore
    }

    internal enum LunaChromeVariant
    {
        Blue,
        Olive,
        Silver,
        Dark
    }

    private static readonly Color CloseNormal = Color.FromArgb(212, 60, 43);
    private static readonly Color CloseHover = Color.FromArgb(232, 84, 67);
    private static readonly Color ClosePressed = Color.FromArgb(184, 44, 32);
    private static readonly Color CloseDisabled = Color.FromArgb(160, 148, 140);

    internal static Image Create(CaptionButtonKind kind, PaletteState state, Size size, LunaChromeVariant variant)
    {
        var bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
        using var g = Graphics.FromImage(bmp);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(Color.Transparent);

        var rect = new RectangleF(1f, 1f, size.Width - 2f, size.Height - 2f);
        if (rect.Width < 4f || rect.Height < 4f)
        {
            return bmp;
        }

        if (kind == CaptionButtonKind.Close)
        {
            DrawCloseButton(g, rect, state);
        }
        else
        {
            DrawChromeButton(g, rect, state, variant);
            DrawChromeGlyph(g, kind, rect, state);
        }

        return bmp;
    }

    private static void DrawCloseButton(Graphics g, RectangleF rect, PaletteState state)
    {
        Color fill = state switch
        {
            PaletteState.Disabled => CloseDisabled,
            PaletteState.Tracking or PaletteState.CheckedTracking => CloseHover,
            PaletteState.Pressed or PaletteState.CheckedPressed => ClosePressed,
            _ => CloseNormal
        };

        using var path = CreateRoundedRect(rect, 2.5f);
        using (var brush = new SolidBrush(fill))
        {
            g.FillPath(brush, path);
        }

        if (state != PaletteState.Disabled)
        {
            Color glyph = state == PaletteState.Pressed ? Color.FromArgb(220, 255, 255, 255) : Color.White;
            DrawCloseGlyph(g, rect, glyph);
        }
    }

    private static void DrawChromeButton(Graphics g, RectangleF rect, PaletteState state, LunaChromeVariant variant)
    {
        GetChromeColors(variant, state, out Color top, out Color bottom, out Color border);

        using var path = CreateRoundedRect(rect, 2.5f);
        using (var brush = new LinearGradientBrush(rect, top, bottom, 90f))
        {
            g.FillPath(brush, path);
        }

        using var pen = new Pen(border);
        g.DrawPath(pen, path);
    }

    private static void DrawChromeGlyph(Graphics g, CaptionButtonKind kind, RectangleF rect, PaletteState state)
    {
        if (state == PaletteState.Disabled)
        {
            return;
        }

        Color glyph = state == PaletteState.Pressed
            ? Color.FromArgb(200, 255, 255, 255)
            : Color.White;
        using var pen = new Pen(glyph, 1.6f)
        {
            StartCap = LineCap.Round,
            EndCap = LineCap.Round
        };

        float cx = rect.Left + (rect.Width / 2f);
        float cy = rect.Top + (rect.Height / 2f);
        float w = rect.Width * 0.28f;
        float h = rect.Height * 0.22f;

        switch (kind)
        {
            case CaptionButtonKind.Minimize:
                g.DrawLine(pen, cx - w, cy + h, cx + w, cy + h);
                break;
            case CaptionButtonKind.Maximize:
                g.DrawRectangle(pen, cx - w, cy - h, w * 2f, h * 2f);
                break;
            case CaptionButtonKind.Restore:
                g.DrawRectangle(pen, cx - w + 2f, cy - h - 1f, w * 2f - 2f, h * 2f - 2f);
                g.DrawLine(pen, cx - w - 1f, cy - h + 2f, cx + w - 3f, cy - h + 2f);
                g.DrawLine(pen, cx - w - 1f, cy - h + 2f, cx - w - 1f, cy + h - 2f);
                break;
        }
    }

    private static void DrawCloseGlyph(Graphics g, RectangleF rect, Color color)
    {
        using var pen = new Pen(color, 1.8f)
        {
            StartCap = LineCap.Round,
            EndCap = LineCap.Round
        };
        float cx = rect.Left + (rect.Width / 2f);
        float cy = rect.Top + (rect.Height / 2f);
        float r = Math.Min(rect.Width, rect.Height) * 0.18f;
        g.DrawLine(pen, cx - r, cy - r, cx + r, cy + r);
        g.DrawLine(pen, cx + r, cy - r, cx - r, cy + r);
    }

    private static void GetChromeColors(LunaChromeVariant variant, PaletteState state, out Color top, out Color bottom, out Color border)
    {
        if (state == PaletteState.Disabled)
        {
            top = Color.FromArgb(192, 188, 180);
            bottom = Color.FromArgb(172, 168, 160);
            border = Color.FromArgb(140, 136, 128);
            return;
        }

        switch (variant)
        {
            case LunaChromeVariant.Olive:
                top = state == PaletteState.Pressed ? Color.FromArgb(120, 140, 80) : Color.FromArgb(175, 192, 130);
                bottom = state == PaletteState.Pressed ? Color.FromArgb(90, 110, 60) : Color.FromArgb(99, 122, 69);
                border = Color.FromArgb(70, 90, 50);
                break;
            case LunaChromeVariant.Silver:
                top = state == PaletteState.Pressed ? Color.FromArgb(170, 170, 178) : Color.FromArgb(210, 210, 218);
                bottom = state == PaletteState.Pressed ? Color.FromArgb(140, 140, 150) : Color.FromArgb(168, 167, 191);
                border = Color.FromArgb(110, 110, 120);
                break;
            case LunaChromeVariant.Dark:
                top = state == PaletteState.Pressed ? Color.FromArgb(70, 70, 70) : Color.FromArgb(96, 96, 96);
                bottom = state == PaletteState.Pressed ? Color.FromArgb(45, 45, 45) : Color.FromArgb(58, 58, 58);
                border = Color.FromArgb(30, 30, 30);
                break;
            default:
                top = state == PaletteState.Pressed ? Color.FromArgb(0, 70, 170) : Color.FromArgb(123, 162, 231);
                bottom = state == PaletteState.Pressed ? Color.FromArgb(0, 45, 120) : Color.FromArgb(0, 90, 181);
                border = Color.FromArgb(0, 60, 140);
                break;
        }

        if (state is PaletteState.Tracking or PaletteState.CheckedTracking)
        {
            top = CommonHelper.MergeColors(top, 0.7f, Color.White, 0.3f);
        }
    }

    private static GraphicsPath CreateRoundedRect(RectangleF rect, float radius)
    {
        var path = new GraphicsPath();
        float d = radius * 2f;
        path.AddArc(rect.X, rect.Y, d, d, 180, 90);
        path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
        path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
        path.CloseFigure();
        return path;
    }
}
