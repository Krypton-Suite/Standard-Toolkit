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
/// macOS-inspired renderer: Material flat language with rounded controls and segmented tabs.
/// </summary>
public class RenderMacOS : RenderMaterial
{
    private const float TabTopCornerRadius = 6f;
    private const float RibbonTabTopCornerRadius = 5f;

    /// <inheritdoc />
    public override ToolStripRenderer RenderToolStrip([DisallowNull] PaletteBase? colorPalette)
    {
        if (colorPalette == null)
        {
            throw new ArgumentNullException(nameof(colorPalette));
        }

        return new KryptonMacOSRenderer(colorPalette.ColorTable);
    }

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

        if (rect.Width <= 0 || rect.Height <= 0)
        {
            return;
        }

        bool selected = state is PaletteState.CheckedNormal or PaletteState.CheckedTracking;
        if (selected)
        {
            var fill = palette.GetBorderColor2(state);
            if (fill == GlobalStaticVariables.EMPTY_COLOR || fill.IsEmpty)
            {
                fill = palette.GetBorderColor1(state);
            }

            using var brush = new SolidBrush(fill);
            using var path = CreateTabTopRoundedPath(rect, TabTopCornerRadius);
            context.Graphics.FillPath(brush, path);

            var accent = palette.GetBorderColor1(state);
            using var pen = new Pen(accent, Math.Max(2f, palette.GetBorderWidth(state)));
            var y = rect.Bottom - (pen.Width / 2f);
            context.Graphics.DrawLine(pen, rect.Left + TabTopCornerRadius, y, rect.Right - TabTopCornerRadius, y);
            return;
        }

        if (state is PaletteState.Tracking or PaletteState.Pressed)
        {
            using var brush = new SolidBrush(Color.FromArgb(24, palette.GetBorderColor1(state)));
            context.Graphics.FillRectangle(brush, rect);
        }
    }

    /// <inheritdoc />
    public override IDisposable? DrawRibbonBack(PaletteRibbonShape shape,
        RenderContext context,
        Rectangle rect,
        PaletteState state,
        IPaletteRibbonBack palette,
        VisualOrientation orientation,
        IDisposable? memento)
    {
        if (shape == PaletteRibbonShape.MacOS
            && palette.GetRibbonBackColorStyle(state) == PaletteRibbonColorStyle.Solid
            && rect is { Width: > 0, Height: > 0 })
        {
            using var brush = new SolidBrush(palette.GetRibbonBackColor1(state));
            context.Graphics.FillRectangle(brush, rect);
            return memento;
        }

        return base.DrawRibbonBack(shape, context, rect, state, palette, orientation, memento);
    }

    /// <inheritdoc />
    protected override IDisposable? DrawRibbonTabSelected2010(RenderContext context,
        Rectangle rect,
        PaletteState state,
        IPaletteRibbonBack palette,
        VisualOrientation orientation,
        IDisposable? memento,
        bool standard)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (palette == null)
        {
            throw new ArgumentNullException(nameof(palette));
        }

        if (rect is { Width: > 0, Height: > 0 })
        {
            using var brush = new SolidBrush(palette.GetRibbonBackColor1(state));
            using var path = CreateRibbonTabTopRoundedPath(rect, RibbonTabTopCornerRadius);
            context.Graphics.FillPath(brush, path);
        }

        return memento;
    }

    /// <inheritdoc />
    protected override IDisposable? DrawRibbonTabTracking2010(PaletteRibbonShape shape,
        RenderContext context,
        Rectangle rect,
        PaletteState state,
        IPaletteRibbonBack palette,
        VisualOrientation orientation,
        IDisposable? memento,
        bool standard)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (palette == null)
        {
            throw new ArgumentNullException(nameof(palette));
        }

        if (rect is { Width: > 0, Height: > 0 })
        {
            var back = palette.GetRibbonBackColor1(state);
            using var brush = new SolidBrush(Color.FromArgb(shape == PaletteRibbonShape.MacOS ? 48 : 32, back));
            using var path = CreateRibbonTabTopRoundedPath(rect, RibbonTabTopCornerRadius);
            context.Graphics.FillPath(brush, path);
        }

        return memento;
    }

    /// <inheritdoc />
    public override void DrawRibbonClusterEdge(PaletteRibbonShape shape,
        RenderContext context,
        Rectangle displayRect,
        IPaletteBack paletteBack,
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

    private static GraphicsPath CreateRibbonTabTopRoundedPath(Rectangle rect, float radius)
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

        path.AddLine(rect.Left, rect.Bottom, rect.Left, rect.Top + radius);
        path.AddArc(rect.Left, rect.Top, d, d, 180, 90);
        path.AddArc(rect.Right - d, rect.Top, d, d, 270, 90);
        path.AddLine(rect.Right, rect.Top + radius, rect.Right, rect.Bottom);
        path.CloseFigure();
        return path;
    }

    private static GraphicsPath CreateTabTopRoundedPath(Rectangle rect, float radius)
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

        path.AddLine(rect.Left, rect.Bottom, rect.Left, rect.Top + radius);
        path.AddArc(rect.Left, rect.Top, d, d, 180, 90);
        path.AddArc(rect.Right - d, rect.Top, d, d, 270, 90);
        path.AddLine(rect.Right, rect.Top + radius, rect.Right, rect.Bottom);
        path.CloseFigure();
        return path;
    }
}