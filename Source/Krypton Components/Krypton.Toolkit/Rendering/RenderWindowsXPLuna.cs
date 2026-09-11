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
/// Windows XP Luna renderer: classic bubbly controls on the Professional renderer base (not Office 2007).
/// </summary>
public sealed class RenderWindowsXPLuna : RenderProfessional
{
    private const float TabCornerRadius = 4f;

    #region IRenderer Overrides

    /// <inheritdoc />
    public override ToolStripRenderer RenderToolStrip([DisallowNull] PaletteBase? colorPalette)
    {
        if (colorPalette == null)
        {
            throw new ArgumentNullException(nameof(colorPalette));
        }

        return new KryptonWindowsXPLunaRenderer(colorPalette.ColorTable);
    }

    #endregion

    #region RenderStandard

    /// <inheritdoc />
    public override void DrawTabBorder(RenderContext context,
        Rectangle rect,
        IPaletteBorder palette,
        VisualOrientation orientation,
        PaletteState state,
        TabBorderStyle tabBorderStyle)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (palette == null)
        {
            throw new ArgumentNullException(nameof(palette));
        }

        if (rect.Width <= 0 || rect.Height <= 0
            || !WindowsXPLunaRenderHelper.IsLunaPalette(KryptonManager.CurrentGlobalPalette))
        {
            base.DrawTabBorder(context, rect, palette, orientation, state, tabBorderStyle);
            return;
        }

        DrawLunaTabBorder(context, rect, state);
    }

    /// <inheritdoc />
    public override IDisposable? DrawBack(RenderContext context,
        Rectangle rect,
        GraphicsPath path,
        IPaletteBack palette,
        VisualOrientation orientation,
        PaletteState state,
        IDisposable? memento)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (path == null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        if (palette == null)
        {
            throw new ArgumentNullException(nameof(palette));
        }

        if (rect.Width > 0 && rect.Height > 0)
        {
            if (WindowsXPLunaRenderHelper.IsLunaHeaderFormBack(palette, state))
            {
                DrawLunaHeaderFormBack(context, rect, path, palette, state);
                return memento;
            }

            if (WindowsXPLunaRenderHelper.IsLunaTabBack(palette, state))
            {
                DrawLunaTabBack(context, rect, path, palette, state);
                return memento;
            }

            if (WindowsXPLunaRenderHelper.IsLunaPushButtonBack(palette, state))
            {
                DrawLunaPushButtonBack(context, rect, path, palette, state);
                return memento;
            }
        }

        return base.DrawBack(context, rect, path, palette, orientation, state, memento);
    }

    #endregion

    #region Implementation

    private static void DrawLunaHeaderFormBack(RenderContext context,
        Rectangle rect,
        GraphicsPath path,
        IPaletteBack palette,
        PaletteState state)
    {
        var g = context.Graphics;
        Color top = palette.GetBackColor1(state);
        Color bottom = palette.GetBackColor2(state);

        if (state == PaletteState.Disabled)
        {
            top = CommonHelper.MergeColors(top, 0.5f, SystemColors.Control, 0.5f);
            bottom = CommonHelper.MergeColors(bottom, 0.5f, SystemColors.Control, 0.5f);
        }

        var region = g.Clip;
        g.SetClip(path, System.Drawing.Drawing2D.CombineMode.Intersect);

        using (var fill = new LinearGradientBrush(rect, top, bottom, 90f))
        {
            g.FillPath(fill, path);
        }

        int glossHeight = Math.Max(2, rect.Height / 4);
        var glossRect = new Rectangle(rect.X, rect.Y, rect.Width, glossHeight);
        using (var gloss = new LinearGradientBrush(glossRect, Color.FromArgb(96, Color.White), Color.FromArgb(0, Color.White), 90f))
        {
            g.FillRectangle(gloss, glossRect);
        }

        g.Clip = region;
    }

    private static void DrawLunaTabBack(RenderContext context,
        Rectangle rect,
        GraphicsPath path,
        IPaletteBack palette,
        PaletteState state)
    {
        var g = context.Graphics;
        bool selected = WindowsXPLunaRenderHelper.IsLunaTabSelected(state);
        Color face1 = palette.GetBackColor1(state);
        Color face2 = palette.GetBackColor2(state);

        if (state == PaletteState.Disabled)
        {
            face1 = CommonHelper.MergeColors(face1, 0.5f, SystemColors.Control, 0.5f);
            face2 = CommonHelper.MergeColors(face2, 0.5f, SystemColors.Control, 0.5f);
        }
        else if (!selected && state is PaletteState.Tracking or PaletteState.Pressed)
        {
            face1 = CommonHelper.MergeColors(face1, 0.85f, Color.White, 0.15f);
            face2 = CommonHelper.MergeColors(face2, 0.85f, Color.White, 0.15f);
        }

        using var tabPath = WindowsXPLunaRenderHelper.CreateTabTopRoundedPath(rect, TabCornerRadius);
        using (var fill = new LinearGradientBrush(rect, face2, face1, 90f))
        {
            g.FillPath(fill, tabPath);
        }
    }

    private static void DrawLunaTabBorder(RenderContext context, Rectangle rect, PaletteState state)
    {
        var g = context.Graphics;
        bool selected = WindowsXPLunaRenderHelper.IsLunaTabSelected(state);

        using var tabPath = WindowsXPLunaRenderHelper.CreateTabTopRoundedPath(rect, TabCornerRadius);
        using var lightPen = new Pen(WindowsXPLunaRenderHelper.TabBorderLight);
        using var darkPen = new Pen(WindowsXPLunaRenderHelper.TabBorderDark);

        if (selected)
        {
            g.DrawLine(lightPen, rect.Left + TabCornerRadius, rect.Top, rect.Right - TabCornerRadius, rect.Top);
            g.DrawLine(lightPen, rect.Left, rect.Top + TabCornerRadius, rect.Left, rect.Bottom);
            g.DrawLine(darkPen, rect.Right - 1, rect.Top + TabCornerRadius, rect.Right - 1, rect.Bottom);
            return;
        }

        g.DrawPath(darkPen, tabPath);
        g.DrawLine(lightPen, rect.Left + 1, rect.Top + 1, rect.Right - 2, rect.Top + 1);
    }

    private static void DrawLunaPushButtonBack(RenderContext context,
        Rectangle rect,
        GraphicsPath path,
        IPaletteBack palette,
        PaletteState state)
    {
        var g = context.Graphics;
        var oldSmoothing = g.SmoothingMode;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        try
        {
            Color face1 = palette.GetBackColor1(state);
            Color face2 = palette.GetBackColor2(state);
            Color top = CommonHelper.MergeColors(face2, 0.1f, Color.White, 0.9f);
            Color bottom = face1;

            switch (state)
            {
                case PaletteState.Pressed:
                case PaletteState.CheckedPressed:
                    top = CommonHelper.MergeColors(face1, 0.55f, Color.Black, 0.45f);
                    bottom = CommonHelper.MergeColors(face1, 0.75f, Color.Black, 0.25f);
                    break;
                case PaletteState.Tracking:
                case PaletteState.CheckedTracking:
                    top = CommonHelper.MergeColors(face2, 0.05f, Color.White, 0.95f);
                    bottom = CommonHelper.MergeColors(face1, 0.9f, Color.White, 0.1f);
                    break;
                case PaletteState.Disabled:
                    top = CommonHelper.MergeColors(face2, 0.5f, SystemColors.Control, 0.5f);
                    bottom = CommonHelper.MergeColors(face1, 0.5f, SystemColors.Control, 0.5f);
                    break;
            }

            using var bubblePath = WindowsXPLunaRenderHelper.CreateBubbleButtonPath(rect);
            var region = g.Clip;
            g.SetClip(bubblePath, System.Drawing.Drawing2D.CombineMode.Intersect);

            using (var fill = new LinearGradientBrush(rect, top, bottom, 90f))
            {
                g.FillPath(fill, bubblePath);
            }

            int glossHeight = Math.Max(3, rect.Height / 3);
            var glossRect = new Rectangle(rect.X, rect.Y, rect.Width, glossHeight);
            using (var gloss = new LinearGradientBrush(glossRect, Color.FromArgb(180, Color.White), Color.FromArgb(0, Color.White), 90f))
            {
                g.FillRectangle(gloss, glossRect);
            }

            g.Clip = region;

            Color lightEdge = Color.FromArgb(255, 255, 255);
            Color darkEdge = WindowsXPLunaRenderHelper.TabBorderDark;
            using var lightPen = new Pen(lightEdge);
            using var darkPen = new Pen(darkEdge);
            g.DrawPath(lightPen, bubblePath);
            g.DrawLine(darkPen, rect.Left + 2, rect.Bottom - 1, rect.Right - 3, rect.Bottom - 1);
            g.DrawLine(darkPen, rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom - 2);
        }
        finally
        {
            g.SmoothingMode = oldSmoothing;
        }
    }

    #endregion
}
