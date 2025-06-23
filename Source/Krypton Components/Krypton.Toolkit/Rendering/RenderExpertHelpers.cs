#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Helper functions for drawing in the glass styles.
/// </summary>
internal class RenderExpertHelpers
{
    #region Static Fields
    private static readonly Blend _rounded1Blend;
    private static readonly Blend _rounded2Blend;
    private const float ITEM_CUT = 1.7f;

    #endregion

    #region Identity
    static RenderExpertHelpers()
    {
        _rounded1Blend = new Blend
        {
            Positions = [0.0f, 0.1f, 1.0f],
            Factors = [0.0f, 1.0f, 1.0f]
        };

        _rounded2Blend = new Blend
        {
            Positions = [0.0f, 0.50f, 0.75f, 1.0f],
            Factors = [0.0f, 1.0f, 1.0f, 1.0f]
        };
    }
    #endregion

    #region Static Public
    /// <summary>
    /// Draw a background for an expert style button with tracking effect.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackExpertTracking(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento)
    {
        using var clip = new Clipping(context.Graphics, path);
        MementoDouble cache;

        if (memento is MementoDouble mementoDouble)
        {
            cache = mementoDouble;
        }
        else
        {
            memento?.Dispose();

            cache = new MementoDouble();
            memento = cache;
        }

        cache.First = DrawBackExpert(rect, 
            CommonHelper.MergeColors(backColor1, 0.35f, Color.White, 0.65f),
            CommonHelper.MergeColors(backColor2, 0.53f, Color.White, 0.65f), 
            orientation, context.Graphics, memento, true, true);
                
        cache.Second = DrawBackExpert(rect, backColor1, backColor2, orientation, context.Graphics, memento, false, true);

        return cache;
    }

    /// <summary>
    /// Draw a background for an expert style button with pressed effect.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackExpertPressed(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento)
    {
        using var clip = new Clipping(context.Graphics, path);
        // Cannot draw a zero length rectangle
        if (rect is { Width: > 0, Height: > 0 })
        {
            var generate = true;
            MementoBackExpertShadow cache;

            // Access a cache instance and decide if cache resources need generating
            if (memento is MementoBackExpertShadow expertShadow)
            {
                cache = expertShadow;
                generate = !cache.UseCachedValues(rect, backColor1, backColor2);
            }
            else
            {
                memento?.Dispose();

                cache = new MementoBackExpertShadow(rect, backColor1, backColor2);
                memento = cache;
            }

            // Do we need to generate the contents of the cache?
            if (generate)
            {
                rect.X -= 1;
                rect.Y -= 1;
                rect.Width += 2;
                rect.Height += 2;

                // Dispose of existing values
                cache.Dispose();
                cache.Path1 = CreateBorderPath(rect, ITEM_CUT);
                cache.Path2 = CreateBorderPath(new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2), ITEM_CUT);
                cache.Path3 = CreateBorderPath(new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - 4, rect.Height - 4), ITEM_CUT);
                cache.Brush1 = new SolidBrush(CommonHelper.MergeColors(backColor2, 0.4f, backColor1, 0.6f));
                cache.Brush2 = new SolidBrush(CommonHelper.MergeColors(backColor2, 0.2f, backColor1, 0.8f));
                cache.Brush3 = new SolidBrush(backColor1);
            }

            using var aa = new AntiAlias(context.Graphics);
            context.Graphics.FillRectangle(cache.Brush3!, rect);
            context.Graphics.FillPath(cache.Brush1!, cache.Path1!);
            context.Graphics.FillPath(cache.Brush2!, cache.Path2!);
            context.Graphics.FillPath(cache.Brush3!, cache.Path3!);
        }

        return memento;
    }

    /// <summary>
    /// Draw a background for an expert style button that is checked.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackExpertChecked(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento)
    {
        using var clip = new Clipping(context.Graphics, path);
        // Draw the expert background which is gradient with highlight at bottom
        return DrawBackExpert(rect, backColor1, backColor2, orientation, context.Graphics, memento, true, false);
    }

    /// <summary>
    /// Draw a background for an expert style button that is checked and tracking.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackExpertCheckedTracking(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento)
    {
        using var clip = new Clipping(context.Graphics, path);
        MementoDouble cache;

        if (memento is MementoDouble mementoDouble)
        {
            cache = mementoDouble;
        }
        else
        {
            memento?.Dispose();

            cache = new MementoDouble();
            memento = cache;
        }

        cache.First = DrawBackExpert(rect,
            backColor1,//CommonHelper.MergeColors(backColor1, 0.5f, Color.White, 0.5f),
            backColor2,//CommonHelper.MergeColors(backColor2, 0.5f, Color.White, 0.5f),
            orientation, context.Graphics, memento, true, false);

        cache.Second = DrawBackExpert(rect, backColor1, backColor2, orientation, context.Graphics, memento, false, false);

        return cache;
    }

    /// <summary>
    /// Draw a background for an expert style button has a square inside with highlight.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    /// <param name="light">Use the 'light' variation.</param>
    public static IDisposable? DrawBackExpertSquareHighlight(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento,
        bool light)
    {
        using var clip = new Clipping(context.Graphics, path);
        // Cannot draw a zero length rectangle
        if (rect is { Width: > 0, Height: > 0 })
        {
            var generate = true;
            MementoBackExpertSquareHighlight cache;

            // Access a cache instance and decide if cache resources need generating
            if (memento is MementoBackExpertSquareHighlight highlight)
            {
                cache = highlight;
                generate = !cache.UseCachedValues(rect, backColor1, backColor2, orientation);
            }
            else
            {
                memento?.Dispose();

                cache = new MementoBackExpertSquareHighlight(rect, backColor1, backColor2, orientation);
                memento = cache;
            }

            // Do we need to generate the contents of the cache?
            if (generate)
            {
                // Dispose of existing values
                cache.Dispose();

                cache.BackBrush = new SolidBrush(CommonHelper.WhitenColor(backColor1, 0.8f, 0.8f, 0.8f));
                cache.InnerRect = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);

                RectangleF ellipseRect;
                PointF ellipseCenter;
                var ellipseWidth = Math.Max(1, rect.Width / 8);
                var ellipseHeight = Math.Max(1, rect.Height / 8);

                switch (orientation)
                {
                    default:
                    case VisualOrientation.Top:
                        cache.InnerBrush = new LinearGradientBrush(cache.InnerRect, backColor1, backColor2, 90f);
                        ellipseRect = new RectangleF(rect.Left, rect.Top + (ellipseHeight * 2), rect.Width, ellipseHeight * 12);
                        ellipseCenter = new PointF(ellipseRect.Left + (ellipseRect.Width / 2), ellipseRect.Bottom);
                        break;
                    case VisualOrientation.Bottom:
                        cache.InnerBrush = new LinearGradientBrush(cache.InnerRect, backColor1, backColor2, 270f);
                        ellipseRect = new RectangleF(rect.Left, rect.Top - (ellipseHeight * 6), rect.Width, ellipseHeight * 12);
                        ellipseCenter = new PointF(ellipseRect.Left + (ellipseRect.Width / 2), ellipseRect.Top);
                        break;
                    case VisualOrientation.Left:
                        cache.InnerBrush = new LinearGradientBrush(cache.InnerRect, backColor1, backColor2, 180f);
                        ellipseRect = new RectangleF(rect.Left + (ellipseHeight * 2), rect.Top, ellipseWidth * 12, rect.Height);
                        ellipseCenter = new PointF(ellipseRect.Right, ellipseRect.Top + (ellipseRect.Height / 2));
                        break;
                    case VisualOrientation.Right:
                        cache.InnerBrush = new LinearGradientBrush(rect, backColor1, backColor2, 0f);
                        ellipseRect = new RectangleF(rect.Left - (ellipseHeight * 6), rect.Top, ellipseWidth * 12, rect.Height);
                        ellipseCenter = new PointF(ellipseRect.Left, ellipseRect.Top + (ellipseRect.Height / 2));
                        break;
                }

                cache.InnerBrush.SetSigmaBellShape(0.5f);
                cache.EllipsePath = new GraphicsPath();
                cache.EllipsePath.AddEllipse(ellipseRect);
                cache.InsideLighten = new PathGradientBrush(cache.EllipsePath)
                {
                    CenterPoint = ellipseCenter,
                    CenterColor = light ? Color.FromArgb(64, Color.White) : Color.FromArgb(128, Color.White),
                    Blend = _rounded2Blend,
                    SurroundColors = [Color.Transparent]
                };
            }

            context.Graphics.FillRectangle(cache.BackBrush!, rect);
            context.Graphics.FillRectangle(cache.InnerBrush!, cache.InnerRect);
            context.Graphics.FillRectangle(cache.InsideLighten!, cache.InnerRect);
        }

        return memento;
    }
    #endregion

    #region Implementation
    private static IDisposable DrawBackSolid(RectangleF drawRect,
        Color color1,
        Graphics g,
        IDisposable memento)
    {
        // Cannot draw a zero length rectangle
        if (drawRect is { Width: > 0, Height: > 0 })
        {
            var generate = true;
            MementoBackSolid cache;

            // Access a cache instance and decide if cache resources need generating
            if (memento is MementoBackSolid backSolid)
            {
                cache = backSolid;
                generate = !cache.UseCachedValues(drawRect, color1);
            }
            else
            {
                memento.Dispose();

                cache = new MementoBackSolid(drawRect, color1);
                memento = cache;
            }

            // Do we need to generate the contents of the cache?
            if (generate)
            {
                // Dispose of existing values
                cache.Dispose();
                cache.SolidBrush = new SolidBrush(color1);
            }

            if (cache.SolidBrush != null)
            {
                g.FillRectangle(cache.SolidBrush, drawRect);
            }
        }

        return memento;
    }

    private static IDisposable? DrawBackExpert(Rectangle drawRect,
        Color color1,
        Color color2,
        VisualOrientation orientation,
        Graphics? g,
        IDisposable? memento,
        bool total,
        bool tracking)
    {
        if (g is not null)
        {
            // Cannot draw a zero length rectangle
            if (drawRect is { Width: > 0, Height: > 0 })
            {
                var generate = true;
                MementoBackExpertChecked cache;

                // Access a cache instance and decide if cache resources need generating
                if (memento is MementoBackExpertChecked expertChecked)
                {
                    cache = expertChecked;
                    generate = !cache.UseCachedValues(drawRect, color1, color2, orientation);
                }
                else
                {
                    memento?.Dispose();

                    cache = new MementoBackExpertChecked(drawRect, color1, color2, orientation);
                    memento = cache;
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    // If not drawing total area... 
                    if (!total)
                    {
                        // Update to draw the inside area instead
                        drawRect.Inflate(-1, -1);

                        cache.DrawRect = drawRect;
                        cache.ClipPath = new GraphicsPath();
                        cache.ClipPath.AddLine(drawRect.X + 1, drawRect.Y, drawRect.Right - 1, drawRect.Y);
                        cache.ClipPath.AddLine(drawRect.Right - 1, drawRect.Y, drawRect.Right, drawRect.Y + 1);
                        cache.ClipPath.AddLine(drawRect.Right, drawRect.Y + 1, drawRect.Right, drawRect.Bottom - 2);
                        cache.ClipPath.AddLine(drawRect.Right, drawRect.Bottom - 2, drawRect.Right - 2, drawRect.Bottom);
                        cache.ClipPath.AddLine(drawRect.Right - 2, drawRect.Bottom, drawRect.Left + 1, drawRect.Bottom);
                        cache.ClipPath.AddLine(drawRect.Left + 1, drawRect.Bottom, drawRect.Left, drawRect.Bottom - 2);
                        cache.ClipPath.AddLine(drawRect.Left, drawRect.Bottom - 2, drawRect.Left, drawRect.Y + 1);
                        cache.ClipPath.AddLine(drawRect.Left, drawRect.Y + 1, drawRect.X + 1, drawRect.Y);
                    }
                    else
                    {
                        cache.ClipPath = new GraphicsPath();
                        cache.ClipPath.AddRectangle(drawRect);
                    }

                    // Create rectangle that covers the enter area
                    var gradientRect = new RectangleF(drawRect.X - 1, drawRect.Y - 1, drawRect.Width + 2,
                        drawRect.Height + 2);

                    // Cannot draw a zero length rectangle
                    if (gradientRect is { Width: > 0, Height: > 0 })
                    {
                        // Draw entire area in a gradient color effect
                        cache.EntireBrush = new LinearGradientBrush(gradientRect, CommonHelper.WhitenColor(color1, 0.92f, 0.92f, 0.92f), color1, AngleFromOrientation(orientation))
                        {
                            Blend = _rounded1Blend
                        };
                    }

                    RectangleF ellipseRect;
                    PointF ellipseCenter;
                    var ellipseHeight = Math.Max(1, drawRect.Height / 4);
                    var ellipseWidth = Math.Max(1, tracking ? drawRect.Width : drawRect.Width / 4);

                    // Ellipse is based on the orientation
                    switch (orientation)
                    {
                        default:
                        case VisualOrientation.Top:
                            ellipseRect = new RectangleF(drawRect.Left - ellipseWidth, drawRect.Bottom - ellipseHeight, drawRect.Width + (ellipseWidth * 2), ellipseHeight * 2);
                            ellipseCenter = new PointF(ellipseRect.Left + (ellipseRect.Width / 2), ellipseRect.Bottom);
                            break;
                        case VisualOrientation.Bottom:
                            ellipseRect = new RectangleF(drawRect.Left - ellipseWidth, drawRect.Top - ellipseHeight, drawRect.Width + (ellipseWidth * 2), ellipseHeight * 2);
                            ellipseCenter = new PointF(ellipseRect.Left + (ellipseRect.Width / 2), ellipseRect.Top);
                            break;
                        case VisualOrientation.Left:
                            ellipseRect = new RectangleF(drawRect.Right - ellipseWidth, drawRect.Top - ellipseHeight, ellipseWidth * 2, drawRect.Height + (ellipseHeight * 2));
                            ellipseCenter = new PointF(ellipseRect.Right, ellipseRect.Top + (ellipseRect.Height / 2));
                            break;
                        case VisualOrientation.Right:
                            ellipseRect = new RectangleF(drawRect.Left - ellipseWidth, drawRect.Top - ellipseHeight, ellipseWidth * 2, drawRect.Height + (ellipseHeight * 2));
                            ellipseCenter = new PointF(ellipseRect.Left, ellipseRect.Top + (ellipseRect.Height / 2));
                            break;
                    }

                    cache.EllipsePath = new GraphicsPath();
                    cache.EllipsePath.AddEllipse(ellipseRect);
                    cache.InsideLighten = new PathGradientBrush(cache.EllipsePath)
                    {
                        CenterPoint = ellipseCenter,
                        CenterColor = color2,
                        Blend = _rounded2Blend,
                        SurroundColors = [Color.Transparent]
                    };
                }

                if (cache.EntireBrush != null)
                {
                    using var clip = new Clipping(g, cache.ClipPath!);
                    g.FillRectangle(cache.EntireBrush, cache!.DrawRect);
                    g.FillPath(cache.InsideLighten!, cache.EllipsePath!);
                }
            }
        }

        return memento;
    }  

    private static GraphicsPath CreateBorderPath(Rectangle rect, float cut)
    {
        // Drawing lines requires we draw inside the area we want
        rect.Width--;
        rect.Height--;

        // Create path using a simple set of lines that cut the corner
        var path = new GraphicsPath();
        path.AddLine(rect.Left + cut, rect.Top, rect.Right - cut, rect.Top);
        path.AddLine(rect.Right - cut, rect.Top, rect.Right, rect.Top + cut);
        path.AddLine(rect.Right, rect.Top + cut, rect.Right, rect.Bottom - cut);
        path.AddLine(rect.Right, rect.Bottom - cut, rect.Right - cut, rect.Bottom);
        path.AddLine(rect.Right - cut, rect.Bottom, rect.Left + cut, rect.Bottom);
        path.AddLine(rect.Left + cut, rect.Bottom, rect.Left, rect.Bottom - cut);
        path.AddLine(rect.Left, rect.Bottom - cut, rect.Left, rect.Top + cut);
        path.AddLine(rect.Left, rect.Top + cut, rect.Left + cut, rect.Top);
        return path;
    }

    private static float AngleFromOrientation(VisualOrientation orientation) => orientation switch
    {
        VisualOrientation.Bottom => 270f,
        VisualOrientation.Left => 0f,
        VisualOrientation.Right => 180,
        _ => 90f
    };
    #endregion
}