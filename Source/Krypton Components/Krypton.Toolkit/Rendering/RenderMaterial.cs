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

        var renderer = new KryptonOffice2010Renderer(colorPalette.ColorTable)
        {
            RoundedEdges = colorPalette.ColorTable.UseRoundedEdges != InheritBool.False
        };

        return renderer;
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
        if (palette == null)
        {
            throw new ArgumentNullException(nameof(palette));
        }

        // Respect palette intent: if no edges requested or width is 0, skip entirely
        var edges = palette.GetBorderDrawBorders(state);
        if (!CommonHelper.HasABorder(edges))
        {
            return;
        }

        var width = palette.GetBorderWidth(state);
        if (width <= 0)
        {
            return;
        }

        using (GraphicsPath path = GetBorderPath(context, rect, palette, orientation, state))
        using (var pen = new Pen(palette.GetBorderColor1(state), Math.Max(1, width)))
        {
            var g = context.Graphics;
            var old = g.SmoothingMode;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
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
        if (TryGetFlatRectPath(context, palette, rect, state, out var flatPath))
        {
            return flatPath!;
        }

        // Otherwise, defer to base behavior for rounded/visible borders
        return base.GetBackPath(context, rect, palette, orientation, state);
    }

    /// <inheritdoc />
    public override GraphicsPath GetBorderPath([DisallowNull] RenderContext context,
        Rectangle rect,
        [DisallowNull] IPaletteBorder palette,
        VisualOrientation orientation,
        PaletteState state)
    {
        if (TryGetFlatRectPath(context, palette, rect, state, out var flatPath))
        {
            return flatPath!;
        }

        return base.GetBorderPath(context, rect, palette, orientation, state);
    }

    private static bool TryGetFlatRectPath([DisallowNull] RenderContext context,
        [DisallowNull] IPaletteBorder palette,
        Rectangle rect,
        PaletteState state,
        out GraphicsPath? path)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (palette == null)
        {
            throw new ArgumentNullException(nameof(palette));
        }

        var draw = palette.GetBorderDraw(state);
        var edges = palette.GetBorderDrawBorders(state);
        var width = palette.GetBorderWidth(state);
        if (draw != InheritBool.True || !CommonHelper.HasABorder(edges) || width <= 0)
        {
            // If the rect is invalid (can happen during first switch), do not return a path
            if (rect.Width <= 0 || rect.Height <= 0)
            {
                path = null;
                return false;
            }

            path = new GraphicsPath();
            path.AddRectangle(rect);
            return true;
        }

        path = null;
        return false;
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
        if (palette == null)
        {
            throw new ArgumentNullException(nameof(palette));
        }

        // Flatten to a solid fill using the primary back color
        using var brush = new SolidBrush(palette.GetBackColor1(state));
        context.Graphics.FillPath(brush, path);
        return null;
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
