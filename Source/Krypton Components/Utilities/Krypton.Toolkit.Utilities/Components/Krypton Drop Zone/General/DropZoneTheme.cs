#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

internal sealed class DropZoneTheme
{
    public Color Back, Border, Accent, Text, DisabledText, Success, Danger, Warning, Info;

    public static DropZoneTheme FromGlobalPalette()
    {
        PaletteBase p = KryptonManager.CurrentGlobalPalette;
        Color back = Pick(p?.GetBackColor1(PaletteBackStyle.PanelClient, PaletteState.Normal), SystemColors.Control);
        Color border = Pick(p?.GetBorderColor1(PaletteBorderStyle.ControlClient, PaletteState.Normal), SystemColors.ControlDark);
        Color text = Pick(p?.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, PaletteState.Normal), SystemColors.ControlText);
        Color disabled = Pick(p?.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, PaletteState.Disabled), SystemColors.GrayText);
        Color accent = Pick(p?.GetBorderColor1(PaletteBorderStyle.ButtonStandalone, PaletteState.Tracking), SystemColors.Highlight);
        bool dark = Luminance(back) < 0.4;

        return new DropZoneTheme
        {
            Back = back,
            Border = border,
            Text = text,
            DisabledText = EnsureContrast(disabled, back, 3.0),
            Accent = EnsureContrast(accent, back, 3.0),
            Info = EnsureContrast(dark ? Color.FromArgb(96, 165, 250) : Color.FromArgb(37, 99, 235), back),
            Success = EnsureContrast(dark ? Color.FromArgb(87, 196, 110) : Color.FromArgb(30, 132, 73), back),
            Danger = EnsureContrast(dark ? Color.FromArgb(255, 118, 118) : Color.FromArgb(192, 43, 43), back),
            Warning = EnsureContrast(dark ? Color.FromArgb(255, 190, 80) : Color.FromArgb(176, 112, 0), back),
        };
    }

    private static Color Pick(Color? c, Color fallback) =>
        c.HasValue && !c.Value.IsEmpty && c.Value.A > 0 ? c.Value : fallback;

    private static double Luminance(Color c)
    {
        Func<byte, double> f = v => { double s = v / 255.0; return s <= 0.03928 ? s / 12.92 : Math.Pow((s + 0.055) / 1.055, 2.4); };
        return 0.2126 * f(c.R) + 0.7152 * f(c.G) + 0.0722 * f(c.B);
    }

    private static double Contrast(Color a, Color b)
    {
        double l1 = Luminance(a), l2 = Luminance(b);
        if (l1 < l2) { double t = l1; l1 = l2; l2 = t; }
        return (l1 + 0.05) / (l2 + 0.05);
    }

    private static Color Blend(Color a, Color b, double t) => Color.FromArgb(255,
        (int)(a.R + (b.R - a.R) * t), (int)(a.G + (b.G - a.G) * t), (int)(a.B + (b.B - a.B) * t));

    public static Color EnsureContrast(Color fg, Color bg, double min = 3.0)
    {
        Color target = Luminance(bg) < 0.4 ? Color.White : Color.Black;
        Color c = fg;
        for (int i = 0; i < 20 && Contrast(c, bg) < min; i++) c = Blend(c, target, 0.12);
        return c;
    }

    private static Color Ink(Color fill) => Luminance(fill) > 0.4 ? Color.Black : Color.White;

    /// <summary>Order: Queued, Uploading, Completed, Failed, Cancelled, Modified, Missing.</summary>
    public Bitmap[] CreateGlyphs(int size)
    {
        return new[]
        {
                Draw(size, (g, r, c, w) => { using (var pen = new Pen(DisabledText, w)) {                       // queued: clock
                    g.DrawEllipse(pen, r);
                    g.DrawLine(pen, c.X, c.Y, c.X, r.Top + r.Height * 0.25f);
                    g.DrawLine(pen, c.X, c.Y, c.X + r.Width * 0.22f, c.Y); } }),
                Draw(size, (g, r, c, w) => { Disc(g, r, Info); using (var pen = Stroke(Ink(Info), w)) {          // uploading: up arrow
                    g.DrawLine(pen, c.X, r.Bottom - r.Height * 0.28f, c.X, r.Top + r.Height * 0.3f);
                    g.DrawLines(pen, new[] { new PointF(c.X - r.Width * 0.2f, r.Top + r.Height * 0.46f),
                        new PointF(c.X, r.Top + r.Height * 0.26f), new PointF(c.X + r.Width * 0.2f, r.Top + r.Height * 0.46f) }); } }),
                Draw(size, (g, r, c, w) => { Disc(g, r, Success); using (var pen = Stroke(Ink(Success), w)) {    // done: check
                    g.DrawLines(pen, new[] { new PointF(r.Left + r.Width * 0.28f, c.Y), new PointF(r.Left + r.Width * 0.44f, r.Top + r.Height * 0.68f),
                        new PointF(r.Left + r.Width * 0.74f, r.Top + r.Height * 0.34f) }); } }),
                Draw(size, (g, r, c, w) => { Disc(g, r, Danger); using (var pen = Stroke(Ink(Danger), w)) {      // failed: X
                    g.DrawLine(pen, r.Left + r.Width * 0.32f, r.Top + r.Height * 0.32f, r.Left + r.Width * 0.68f, r.Top + r.Height * 0.68f);
                    g.DrawLine(pen, r.Left + r.Width * 0.68f, r.Top + r.Height * 0.32f, r.Left + r.Width * 0.32f, r.Top + r.Height * 0.68f); } }),
                Draw(size, (g, r, c, w) => RingSlash(g, r, DisabledText, w)),                                     // cancelled
                Draw(size, (g, r, c, w) => {                                                                       // modified: warning triangle
                    Disc3(g, new[] { new PointF(c.X, r.Top), new PointF(r.Right, r.Bottom), new PointF(r.Left, r.Bottom) }, Warning);
                    Color ink = Ink(Warning);
                    using (var pen = Stroke(ink, w)) g.DrawLine(pen, c.X, r.Top + r.Height * 0.42f, c.X, r.Top + r.Height * 0.68f);
                    using (var brush = new SolidBrush(ink)) g.FillEllipse(brush, c.X - w / 2, r.Top + r.Height * 0.78f, w, w); }),
                Draw(size, (g, r, c, w) => RingSlash(g, r, Danger, w)),                                           // missing
            };
    }

    private static Pen Stroke(Color color, float width) =>
        new Pen(color, width) { StartCap = LineCap.Round, EndCap = LineCap.Round, LineJoin = LineJoin.Round };

    private static void Disc(Graphics g, RectangleF r, Color color) { using (var b = new SolidBrush(color)) g.FillEllipse(b, r); }
    private static void Disc3(Graphics g, PointF[] pts, Color color) { using (var b = new SolidBrush(color)) g.FillPolygon(b, pts); }

    private static void RingSlash(Graphics g, RectangleF r, Color color, float w)
    {
        using (var pen = new Pen(color, w))
        {
            g.DrawEllipse(pen, r);
            g.DrawLine(pen, r.Left + r.Width * 0.22f, r.Bottom - r.Height * 0.22f, r.Right - r.Width * 0.22f, r.Top + r.Height * 0.22f);
        }
    }

    private static Bitmap Draw(int size, Action<Graphics, RectangleF, PointF, float> paint)
    {
        var bmp = new Bitmap(size, size, PixelFormat.Format32bppArgb);
        using (Graphics g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new RectangleF(1.5f, 1.5f, size - 3f, size - 3f);
            paint(g, rect, new PointF(size / 2f, size / 2f), Math.Max(1.5f, size / 10f));
        }
        return bmp;
    }
}