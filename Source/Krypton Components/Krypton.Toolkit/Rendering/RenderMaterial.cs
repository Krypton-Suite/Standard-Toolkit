#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Renderer targeting a flat, dense Material visual language.
/// </summary>
/// <seealso cref="RenderOffice2010" />
public sealed class RenderMaterial : RenderOffice2010
{
    #region Constructor
    static RenderMaterial()
    {
    }
    #endregion

    #region RenderRibbon Overrides
    /// <inheritdoc />
    public override void DrawRibbonClusterEdge(PaletteRibbonShape shape,
        [DisallowNull] RenderContext context,
        Rectangle displayRect,
        [DisallowNull] IPaletteBack paletteBack,
        PaletteState state)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (paletteBack == null)
        {
            throw new ArgumentNullException(nameof(paletteBack));
        }

        using var drawBrush = new SolidBrush(paletteBack.GetBackColor1(state));
        context.Graphics.FillRectangle(drawBrush, displayRect);
    }
    #endregion

    #region IRenderer Overrides
    /// <inheritdoc />
    public override ToolStripRenderer RenderToolStrip([DisallowNull] PaletteBase? colorPalette)
    {
        if (colorPalette == null)
        {
            throw new ArgumentNullException(nameof(colorPalette));
        }

        return new KryptonMaterialRenderer(colorPalette.ColorTable);
    }
    #endregion

    #region RenderStandardBorder
    /// <inheritdoc />
    public override Padding GetBorderRawPadding(IPaletteBorder palette, PaletteState state, VisualOrientation orientation)
    {
        // Eliminate implicit interior gutters for Material to avoid stray top/left lines from layout canvases
        return Padding.Empty;
    }

    /// <inheritdoc />
    public override Padding GetBorderDisplayPadding(IPaletteBorder? palette, PaletteState state, VisualOrientation orientation)
    {
        // Prefer minimal interior insets; fall back to base for overrides or unknowns
        if (CommonHelper.IsOverrideState(state))
        {
            return base.GetBorderDisplayPadding(palette, state, orientation);
        }

        return Padding.Empty;
    }

    /// <inheritdoc />
    public override void DrawBorder(RenderContext context,
        Rectangle rect,
        IPaletteBorder palette,
        VisualOrientation orientation,
        PaletteState state)
    {
        #if DEBUG
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (palette is null)
        {
            throw new ArgumentNullException(nameof(palette));
        }

        if (rect.Width < 0 || rect.Height < 0)
        {
            return; // benign skip
        }
#endif
        if (palette == null)
        {
            throw new ArgumentNullException(nameof(palette));
        }

        // Respect palette intent: if no edges requested or width is 0, skip drawing
        var edges = palette.GetBorderDrawBorders(state);
        var width = palette.GetBorderWidth(state);
        if (!CommonHelper.HasABorder(edges) || width <= 0)
        {
            return;
        }

        // For crisp Material rectangles, avoid anti-alias corner artifacts when rounding is zero
        var rounding = palette.GetBorderRounding(state);
        if (rounding <= 0.0001f && rect.Width > 0 && rect.Height > 0)
        {
            var g = context.Graphics;
            var oldSmoothing = g.SmoothingMode;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            using (var pen = new Pen(palette.GetBorderColor1(state), Math.Max(1, palette.GetBorderWidth(state))))
            {
                var left = rect.Left;
                var right = rect.Right - 1;
                var top = rect.Top;
                var bottom = rect.Bottom - 1;

                if (CommonHelper.HasTopBorder(edges))
                {
                    g.DrawLine(pen, left, top, right, top);
                }
                if (CommonHelper.HasBottomBorder(edges))
                {
                    g.DrawLine(pen, left, bottom, right, bottom);
                }
                if (CommonHelper.HasLeftBorder(edges))
                {
                    g.DrawLine(pen, left, top, left, bottom);
                }
                if (CommonHelper.HasRightBorder(edges))
                {
                    g.DrawLine(pen, right, top, right, bottom);
                }
            }

            g.SmoothingMode = oldSmoothing;
            return;
        }

        using (GraphicsPath path = GetBorderPath(context, rect, palette, orientation, state))
        using (var pen = new Pen(palette.GetBorderColor1(state), Math.Max(1, width)))
        {
            var g = context.Graphics;
            var old = g.SmoothingMode;
            g.SmoothingMode = rounding <= 0.0001f
                ? System.Drawing.Drawing2D.SmoothingMode.None
                : System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawPath(pen, path);
            g.SmoothingMode = old;
        }
    }

                /// <inheritdoc />
    public override GraphicsPath GetBackPath([DisallowNull] RenderContext context,
        Rectangle rect,
        [DisallowNull] IPaletteBorder palette,
        VisualOrientation orientation,
        PaletteState state)
    {
        #if DEBUG
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (palette is null)
        {
            throw new ArgumentNullException(nameof(palette));
        }

        if (rect.Width < 0 || rect.Height < 0)
        {
            rect = Rectangle.Empty;
        }
#endif
        // For invalid rectangles, let base handle it
        if (rect.Width <= 0 || rect.Height <= 0)
        {
            return base.GetBackPath(context, rect, palette, orientation, state);
        }

        // Check if we need to draw a border
        var draw = palette.GetBorderDraw(state);
        var edges = palette.GetBorderDrawBorders(state);
        var width = palette.GetBorderWidth(state);

        if (draw != InheritBool.True || !CommonHelper.HasABorder(edges) || width <= 0)
        {
            // No border needed - return exact rectangle without any padding
            var path = new GraphicsPath();
            path.AddRectangle(rect);
            return path;
        }

        // Border is needed - use base implementation
        return base.GetBackPath(context, rect, palette, orientation, state);
    }

    /// <inheritdoc />
    public override GraphicsPath GetBorderPath([DisallowNull] RenderContext context,
        Rectangle rect,
        [DisallowNull] IPaletteBorder palette,
        VisualOrientation orientation,
        PaletteState state)
    {
        #if DEBUG
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (palette is null)
        {
            throw new ArgumentNullException(nameof(palette));
        }

        if (rect.Width < 0 || rect.Height < 0)
        {
            rect = Rectangle.Empty;
        }
#endif
        // For invalid rectangles, let base handle it
        if (rect.Width <= 0 || rect.Height <= 0)
        {
            return base.GetBorderPath(context, rect, palette, orientation, state);
        }

        // Check if we need to draw a border
        var draw = palette.GetBorderDraw(state);
        var edges = palette.GetBorderDrawBorders(state);
        var width = palette.GetBorderWidth(state);

        if (draw != InheritBool.True || !CommonHelper.HasABorder(edges) || width <= 0)
        {
            // No border needed - return exact rectangle
            var path = new GraphicsPath();
            path.AddRectangle(rect);
            return path;
        }

        // Border is needed - use base implementation
        return base.GetBorderPath(context, rect, palette, orientation, state);
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
        #if DEBUG
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (palette is null)
        {
            throw new ArgumentNullException(nameof(palette));
        }

        if (path is null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        if (rect.Width < 0 || rect.Height < 0)
        {
            rect = Rectangle.Empty;
        }
#endif
        if (palette == null)
        {
            throw new ArgumentNullException(nameof(palette));
        }

        // Respect palette color styles to allow controls (e.g., ProgressBar) to differentiate track vs. value areas
        return base.DrawBack(context, rect, path, palette, orientation, state, memento);
    }
    #endregion

    #region IRenderTabBorder overrides
    /// <inheritdoc />
    public override void DrawTabBorder(RenderContext context,
        Rectangle rect,
        IPaletteBorder palette,
        VisualOrientation orientation,
        PaletteState state,
        TabBorderStyle tabBorderStyle)
    {
        // Material: underline indicator for selected/active/hovered tabs only; otherwise, no border
        bool showUnderline = state is PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.Tracking or PaletteState.Pressed;
        if (!showUnderline)
        {
            return;
        }

        var underlineColor = palette.GetBorderColor1(state);
        var thickness = Math.Max(2, palette.GetBorderWidth(state));

        using var pen = new Pen(underlineColor, thickness);
        var g = context.Graphics;

        // Draw bottom underline regardless of orientation (tabs are usually top aligned)
        var y = rect.Bottom - (thickness / 2f);
        g.DrawLine(pen, rect.Left, y, rect.Right, y);
    }
    #endregion
}
