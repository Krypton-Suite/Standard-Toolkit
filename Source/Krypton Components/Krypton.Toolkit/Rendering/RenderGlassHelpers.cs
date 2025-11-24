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
public class RenderGlassHelpers
{
    #region Static Fields
    private static readonly Color _glassColorTopL = Color.FromArgb(208, Color.White);
    private static readonly Color _glassColorBottomL = Color.FromArgb(96, Color.White);
    private static readonly Color _glassColorTopD = Color.FromArgb(164, Color.White);
    private static readonly Color _glassColorBottomD = Color.FromArgb(64, Color.White);
    private static readonly Color _glassColorLight = Color.FromArgb(96, Color.White);
    private static readonly Color _glassColorTopDD = Color.FromArgb(128, Color.White);
    private static readonly Color _glassColorBottomDD = Color.FromArgb(48, Color.White);
    private static readonly Blend _glassFadeBlend;
    private const float FULL_GLASS_LENGTH = 0.45f;
    private const float STUMPY_GLASS_LENGTH = 0.19f;

    #endregion

    #region Identity
    static RenderGlassHelpers()
    {
        _glassFadeBlend = new Blend
        {
            Positions = [0.0f, 0.33f, 0.66f, 1.0f],
            Factors = [0.0f, 0.0f, 0.8f, 1.0f]
        };
    }
    #endregion

    #region Static Public
    /// <summary>
    /// Draw a background with glass effect where the fade is from the center.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackGlassCenter(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento)
    {
        using Clipping clip = new Clipping(context.Graphics, path);
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

        // Draw the one pixel border around the area
        cache.First = DrawBackLinearRadial(rect, false,
            ControlPaint.LightLight(backColor2),
            ControlPaint.Light(backColor2),
            ControlPaint.LightLight(backColor2),
            orientation, context.Graphics,
            cache.First!);

        // Reduce size of the inside area
        rect.Inflate(-1, -1);

        // Draw the inside area as a glass effect
        cache.Second = DrawBackGlassCenter(rect, backColor1, backColor2,
            _glassColorTopL, _glassColorBottomL,
            2f, 1f, orientation, context.Graphics,
            FULL_GLASS_LENGTH, cache.Second!);

        return memento;
    }

    /// <summary>
    /// Draw a background with glass effect where the fade is from the bottom.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackGlassBottom(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento)
    {
        using Clipping clip = new Clipping(context.Graphics, path);
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

        // Draw the one pixel border around the area
        cache.First = DrawBackLinear(rect, false,
            ControlPaint.Light(backColor1),
            ControlPaint.LightLight(backColor1),
            orientation, context.Graphics,
            cache.First!);

        // Reduce size on all but the upper edge
        ModifyRectByEdges(ref rect, 1, 0, 1, 1, orientation);

        // Draw the inside areas as a glass effect
        cache.Second = DrawBackGlassRadial(rect, backColor1, backColor2,
            _glassColorTopD, _glassColorBottomD,
            3f, 1.1f, orientation, context.Graphics,
            FULL_GLASS_LENGTH, cache.Second);

        return memento;
    }

    /// <summary>
    /// Draw a background in normal full glass effect but only over 50% of the background.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackGlassFade(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento)
    {
        using Clipping clip = new Clipping(context.Graphics, path);
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

        cache.First = DrawBackGlassFade(rect, rect,
            backColor1, backColor2,
            _glassColorTopL,
            _glassColorBottomL,
            orientation,
            context.Graphics,
            cache.First!);

        cache.Second = DrawBackDarkEdge(rect, ControlPaint.Dark(backColor1),
            3, orientation, context.Graphics, 
            cache.Second!);

        return memento;
    }

    /// <summary>
    /// Draw a background in simple glass effect.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackGlassSimpleFull(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento) =>
        DrawBackGlassSimplePercent(context, rect, 
            backColor1, backColor2, 
            orientation, path, 
            FULL_GLASS_LENGTH, memento);

    /// <summary>
    /// Draw a background in normal full glass effect.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackGlassNormalFull(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento) =>
        DrawBackGlassNormalPercent(context, rect,
            backColor1, backColor2,
            orientation, path,
            FULL_GLASS_LENGTH, memento);

    /// <summary>
    /// Draw a background in tracking full glass effect.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackGlassTrackingFull(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento) =>
        DrawBackGlassTrackingPercent(context, rect, 
            backColor1, backColor2,
            orientation, path,
            FULL_GLASS_LENGTH, memento);

    /// <summary>
    /// Draw a background in checked full glass effect.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackGlassCheckedFull(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento) =>
        DrawBackGlassCheckedPercent(context, rect, 
            backColor1, backColor2,
            orientation, path, 
            FULL_GLASS_LENGTH, memento);

    /// <summary>
    /// Draw a background in checked/tracking full glass effect.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackGlassCheckedTrackingFull(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento) =>
        DrawBackGlassCheckedTrackingPercent(context, rect, 
            backColor1, backColor2,
            orientation, path, 
            FULL_GLASS_LENGTH, memento);

    /// <summary>
    /// Draw a background in checked/pressed full glass effect.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackGlassPressedFull(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento) =>
        DrawBackGlassPressedPercent(context, rect, 
            backColor1, backColor2,
            orientation, path, 
            FULL_GLASS_LENGTH, memento);

    /// <summary>
    /// Draw a background in normal stumpy glass effect.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackGlassNormalStump(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento) =>
        DrawBackGlassNormalPercent(context, rect, 
            backColor1, backColor2,
            orientation, path, 
            STUMPY_GLASS_LENGTH, memento);

    /// <summary>
    /// Draw a background in tracking stumpy glass effect.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackGlassTrackingStump(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento) =>
        DrawBackGlassTrackingPercent(context, rect, 
            backColor1, backColor2,
            orientation, path, 
            STUMPY_GLASS_LENGTH, memento);

    /// <summary>
    /// Draw a background in checked/pressed stumpy glass effect.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackGlassPressedStump(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento) =>
        DrawBackGlassPressedPercent(context, rect, 
            backColor1, backColor2,
            orientation, path, 
            STUMPY_GLASS_LENGTH, memento);

    /// <summary>
    /// Draw a background in checked stumpy glass effect.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackGlassCheckedStump(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento) =>
        DrawBackGlassCheckedPercent(context, rect, 
            backColor1, backColor2,
            orientation, path, 
            STUMPY_GLASS_LENGTH, memento);

    /// <summary>
    /// Draw a background in checked/tracking stumpy glass effect.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackGlassCheckedTrackingStump(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento) =>
        DrawBackGlassCheckedTrackingPercent(context, rect, 
            backColor1, backColor2,
            orientation, path, 
            STUMPY_GLASS_LENGTH, memento);

    /// <summary>
    /// Draw a background in glass effect with three edges lighter.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackGlassThreeEdge(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento)
    {
        using Clipping clip = new Clipping(context.Graphics, path);
        var generate = true;
        MementoBackGlassThreeEdge cache;

        // Access a cache instance and decide if cache resources need generating
        if (memento is MementoBackGlassThreeEdge glassThreeEdge)
        {
            cache = glassThreeEdge;
            generate = !cache.UseCachedValues(rect, backColor1, backColor2, orientation);
        }
        else
        {
            memento?.Dispose();

            cache = new MementoBackGlassThreeEdge(rect, backColor1, backColor2, orientation);
            memento = cache;
        }

        // Do we need to generate the contents of the cache?
        if (generate)
        {
            // Dispose of existing values
            cache.Dispose();

            // Generate color values
            cache.ColorA1L = CommonHelper.MergeColors(backColor1, 0.7f, Color.White, 0.3f);
            cache.ColorA2L = CommonHelper.MergeColors(backColor2, 0.7f, Color.White, 0.3f);
            cache.ColorA2Ll = CommonHelper.MergeColors(cache.ColorA2L, 0.8f, Color.White, 0.2f);
            cache.ColorB2Ll = CommonHelper.MergeColors(backColor2, 0.8f, Color.White, 0.2f);
            cache.RectB = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 1, rect.Height - 2);
        }

        // Draw entire area in a lighter version
        cache.First = DrawBackGlassLinear(rect, rect,
            cache.ColorA1L, _glassColorLight,
            cache.ColorA2L, cache.ColorA2Ll,
            orientation,
            context.Graphics,
            FULL_GLASS_LENGTH,
            cache.First);

                
        // Draw the inside area in the full color
        cache.Second = DrawBackGlassLinear(cache.RectB, cache.RectB,
            backColor1, _glassColorLight,
            backColor2, cache.ColorB2Ll,
            orientation,
            context.Graphics,
            FULL_GLASS_LENGTH,
            cache.Second);

        return cache;
    }

    /// <summary>
    /// Draw a background in normal simple glass effect.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackGlassNormalSimple(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path, 
        IDisposable? memento)
    {
        using Clipping clip = new Clipping(context.Graphics, path);
        // Draw the inside area
        return DrawBackGlassLinear(rect, rect,
            backColor1, backColor2,
            _glassColorTopL,
            _glassColorBottomL,
            orientation,
            context.Graphics,
            FULL_GLASS_LENGTH,
            memento);
    }

    /// <summary>
    /// Draw a background in tracking simple glass effect.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackGlassTrackingSimple(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento)
    {
        using Clipping clip = new Clipping(context.Graphics, path);
        // Draw the inside area as a glass effect
        return DrawBackGlassRadial(rect, backColor1, backColor2,
            _glassColorTopL, _glassColorBottomL,
            2f, 1f, orientation, context.Graphics,
            FULL_GLASS_LENGTH, memento);
    }

    /// <summary>
    /// Draw a background in checked simple glass effect.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackGlassCheckedSimple(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento)
    {
        using Clipping clip = new Clipping(context.Graphics, path);
        // Draw the inside areas as a glass effect
        return DrawBackGlassRadial(rect, backColor1, backColor2,
            _glassColorTopL, _glassColorBottomL,
            6f, 1.2f, orientation, context.Graphics,
            FULL_GLASS_LENGTH, memento);
    }

    /// <summary>
    /// Draw a background in checked/tracking simple glass effect.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackGlassCheckedTrackingSimple(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento)
    {
        using Clipping clip = new Clipping(context.Graphics, path);
        // Draw the inside areas as a glass effect
        return DrawBackGlassRadial(rect, backColor1, backColor2,
            _glassColorTopD, _glassColorBottomD,
            5f, 1.2f, orientation, context.Graphics,
            FULL_GLASS_LENGTH, memento);
    }

    /// <summary>
    /// Draw a background in checked/pressed simple glass effect.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Rectangle to draw.</param>
    /// <param name="backColor1">First color.</param>
    /// <param name="backColor2">Second color.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="path">Clipping path.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public static IDisposable? DrawBackGlassPressedSimple(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        IDisposable? memento)
    {
        using Clipping clip = new Clipping(context.Graphics, path);
        // Draw the inside areas as a glass effect
        return DrawBackGlassRadial(rect, backColor1, backColor2,
            _glassColorTopD, _glassColorBottomD,
            3f, 1.1f, orientation, context.Graphics,
            FULL_GLASS_LENGTH, memento);
    }

    #endregion

    #region Implementation
    private static IDisposable? DrawBackGlassSimplePercent(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        float glassPercent,
        IDisposable? memento)
    {
        using Clipping clip = new Clipping(context.Graphics, path);
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

        // Get the drawing rectangle for the path
        RectangleF drawRect = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);

        // Draw the border as a lighter version of the inside
        cache.First = DrawBackGlassLinear(drawRect, drawRect,
            backColor2,
            backColor2,
            _glassColorBottomDD,
            _glassColorBottomDD,
            orientation,
            context.Graphics,
            0,
            cache.First);

        // Reduce by 1 pixel on all edges to get the inside
        RectangleF insetRect = drawRect;
        insetRect.Inflate(-1f, -1f);

        // Draw the inside area
        cache.Second = DrawBackGlassLinear(insetRect, drawRect,
            backColor1, 
            CommonHelper.MergeColors(backColor1, 0.5f, backColor2, 0.5f),
            _glassColorTopDD,
            _glassColorBottomDD,
            orientation,
            context.Graphics,
            glassPercent,
            cache.Second);

        return memento;
    }

    private static IDisposable? DrawBackGlassNormalPercent(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        float glassPercent,
        IDisposable? memento)
    {
        using Clipping clip = new Clipping(context.Graphics, path);
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

        // Get the drawing rectangle for the path
        RectangleF drawRect = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);

        // Draw the border as a lighter version of the inside
        cache.First = DrawBackGlassLinear(drawRect, drawRect,
            Color.White,
            Color.White,
            _glassColorTopL,
            _glassColorBottomL,
            orientation,
            context.Graphics,
            glassPercent,
            cache.First);

        // Reduce by 1 pixel on all edges to get the inside
        RectangleF insetRect = drawRect;
        insetRect.Inflate(-1f, -1f);

        // Draw the inside area
        cache.Second = DrawBackGlassLinear(insetRect, drawRect,
            backColor1, backColor2,
            _glassColorTopL,
            _glassColorBottomL,
            orientation,
            context.Graphics,
            glassPercent,
            cache.Second);

        return memento;
    }

    private static IDisposable? DrawBackGlassTrackingPercent(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        float glassPercent,
        IDisposable? memento)
    {
        using Clipping clip = new Clipping(context.Graphics, path);
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

        // Draw the one pixel border around the area
        cache.First = DrawBackLinearRadial(rect, false,
            ControlPaint.LightLight(backColor2),
            ControlPaint.Light(backColor2),
            ControlPaint.LightLight(backColor2),
            orientation, context.Graphics,
            cache.First!);

        // Reduce size of the inside area
        rect.Inflate(-1, -1);

        // Draw the inside area as a glass effect
        cache.Second = DrawBackGlassRadial(rect, backColor1, backColor2,
            _glassColorTopL, _glassColorBottomL,
            2f, 1f, orientation, context.Graphics,
            glassPercent, cache.Second);

        return memento;
    }

    private static IDisposable? DrawBackGlassPressedPercent(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        float glassPercent,
        IDisposable? memento)
    {
        using Clipping clip = new Clipping(context.Graphics, path);
        MementoTriple cache;

        if (memento is MementoTriple triple)
        {
            cache = triple;
        }
        else
        {
            memento?.Dispose();

            cache = new MementoTriple();
            memento = cache;
        }

        // Draw the one pixel border around the area
        cache.First = DrawBackLinear(rect, false,
            ControlPaint.Light(backColor1),
            ControlPaint.LightLight(backColor1),
            orientation, context.Graphics,
            cache.First!);

        // Reduce size on all but the upper edge
        ModifyRectByEdges(ref rect, 1, 0, 1, 1, orientation);

        // Draw the inside areas as a glass effect
        cache.Second = DrawBackGlassRadial(rect, backColor1, backColor2,
            _glassColorTopD, _glassColorBottomD,
            3f, 1.1f, orientation, context.Graphics,
            glassPercent, cache.Second);

        // Widen back to original
        ModifyRectByEdges(ref rect, -1, 0, -1, 0, orientation);

        cache.Third = DrawBackDarkEdge(rect, ControlPaint.Dark(backColor1),
            3, orientation, context.Graphics,
            cache.Third!);

        return memento;
    }

    private static IDisposable? DrawBackGlassCheckedPercent(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        float glassPercent,
        IDisposable? memento)
    {
        using Clipping clip = new Clipping(context.Graphics, path);
        MementoTriple cache;

        if (memento is MementoTriple triple)
        {
            cache = triple;
        }
        else
        {
            memento?.Dispose();

            cache = new MementoTriple();
            memento = cache;
        }

        // Draw the one pixel border around the area
        cache.First = DrawBackLinearRadial(rect, false,
            ControlPaint.Light(backColor1),
            ControlPaint.LightLight(backColor1),
            ControlPaint.LightLight(backColor1),
            orientation, context.Graphics,
            cache.First!);

        // Reduce size on all but the upper edge
        ModifyRectByEdges(ref rect, 1, 0, 1, 1, orientation);

        // Draw the inside areas as a glass effect
        cache.Second = DrawBackGlassRadial(rect, backColor1, backColor2,
            _glassColorTopL, _glassColorBottomL,
            6f, 1.2f, orientation, context.Graphics,
            glassPercent, cache.Second);

        // Widen back to original
        ModifyRectByEdges(ref rect, -1, 0, -1, 0, orientation);

        // Draw a darker area for top edge
        cache.Third = DrawBackDarkEdge(rect, ControlPaint.Dark(backColor1),
            3, orientation, context.Graphics,
            cache.Third!);

        return memento;
    }

    private static IDisposable? DrawBackGlassCheckedTrackingPercent(RenderContext context,
        Rectangle rect,
        Color backColor1,
        Color backColor2,
        VisualOrientation orientation,
        GraphicsPath path,
        float glassPercent,
        IDisposable? memento)
    {
        using Clipping clip = new Clipping(context.Graphics, path);
        MementoTriple cache;

        if (memento is MementoTriple triple)
        {
            cache = triple;
        }
        else
        {
            memento?.Dispose();

            cache = new MementoTriple();
            memento = cache;
        }

        // Draw the one pixel border around the area
        cache.First = DrawBackLinear(rect, true,
            backColor2,
            ControlPaint.LightLight(backColor2),
            orientation,
            context.Graphics,
            cache.First!);

        // Reduce size on all but the upper edge
        ModifyRectByEdges(ref rect, 1, 0, 1, 1, orientation);

        // Draw the inside areas as a glass effect
        cache.Second = DrawBackGlassRadial(rect, backColor1, backColor2,
            _glassColorTopD, _glassColorBottomD,
            5f, 1.2f, orientation, context.Graphics,
            glassPercent, cache.Second);

        // Widen back to original
        ModifyRectByEdges(ref rect, -1, 0, -1, 0, orientation);

        cache.Third = DrawBackDarkEdge(rect, ControlPaint.Dark(backColor1),
            3, orientation, context.Graphics,
            cache.Third!);

        return memento;
    }

    private static IDisposable DrawBackLinearRadial(RectangleF drawRect,
        bool sigma,
        Color color1,
        Color color2,
        Color color3,
        VisualOrientation orientation,
        Graphics? g,
        IDisposable memento)
    {
        if (g is not null)
        {
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

            // Draw entire background in linear gradient effect
            cache.First = DrawBackLinear(drawRect, sigma, color1, color2, orientation, g, cache.First!);

            var generate = true;
            MementoBackLinearRadial cacheThis;

            // Access a cache instance and decide if cache resources need generating
            if (cache.Second is MementoBackLinearRadial linearRadial)
            {
                cacheThis = linearRadial;
                generate = !cacheThis.UseCachedValues(drawRect, color2, color3, orientation);
            }
            else
            {
                cache.Second?.Dispose();

                cacheThis = new MementoBackLinearRadial(drawRect, color2, color3, orientation);
                cache.Second = cacheThis;
            }

            // Do we need to generate the contents of the cache?
            if (generate)
            {
                // Dispose of existing values
                cacheThis.Dispose();

                float third;

                // Find the 1/3 height used for the ellipse
                if (VerticalOrientation(orientation))
                {
                    third = drawRect.Height / 3;
                }
                else
                {
                    third = drawRect.Width / 3;
                }

                // Find the bottom area rectangle
                RectangleF ellipseRect;
                PointF centerPoint;

                switch (orientation)
                {
                    case VisualOrientation.Left:
                        ellipseRect = new RectangleF(drawRect.Right - third, drawRect.Y + 1, third, drawRect.Height - 2);
                        centerPoint = new PointF(ellipseRect.Right, ellipseRect.Y + (ellipseRect.Height / 2));
                        break;
                    case VisualOrientation.Right:
                        ellipseRect = new RectangleF(drawRect.X - 1, drawRect.Y + 1, third, drawRect.Height - 2);
                        centerPoint = new PointF(ellipseRect.Left, ellipseRect.Y + (ellipseRect.Height / 2));
                        break;
                    case VisualOrientation.Bottom:
                        ellipseRect = new RectangleF(drawRect.X + 1, drawRect.Y - 1, drawRect.Width - 2, third);
                        centerPoint = new PointF(ellipseRect.X + (ellipseRect.Width / 2), ellipseRect.Top);
                        break;
                    case VisualOrientation.Top:
                    default:
                        ellipseRect = new RectangleF(drawRect.X + 1, drawRect.Bottom - third, drawRect.Width - 2, third);
                        centerPoint = new PointF(ellipseRect.X + (ellipseRect.Width / 2), ellipseRect.Bottom);
                        break;
                }

                cacheThis.EllipseRect = ellipseRect;

                // Cannot draw a path that contains a zero sized element
                if (ellipseRect is { Width: > 0, Height: > 0 })
                {
                    cacheThis.Path = new GraphicsPath();
                    cacheThis.Path.AddEllipse(ellipseRect);
                    cacheThis.BottomBrush = new PathGradientBrush(cacheThis.Path)
                    {
                        CenterColor = ControlPaint.Light(color3),
                        CenterPoint = centerPoint,
                        SurroundColors = [color2]
                    };
                }
            }

            if (cacheThis.BottomBrush != null)
            {
                g.FillRectangle(cacheThis.BottomBrush, cacheThis.EllipseRect);
            }
        }

        return memento;
    }

    private static IDisposable? DrawBackGlassRadial(RectangleF drawRect,
        Color color1,
        Color color2,
        Color glassColor1,
        Color glassColor2,
        float factorX,
        float factorY,
        VisualOrientation orientation,
        Graphics? g,
        float glassPercent,
        IDisposable? memento)
    {
        if (g is not null)
        {
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

            // Draw the gradient effect background
            RectangleF glassRect = DrawBackGlassBasic(drawRect, color1, color2,
                glassColor1, glassColor2,
                factorX, factorY,
                orientation, g,
                glassPercent,
                ref cache.First!);

            var generate = true;
            MementoBackGlassRadial cacheThis;

            // Access a cache instance and decide if cache resources need generating
            if (cache.Second is MementoBackGlassRadial glassRadial)
            {
                cacheThis = glassRadial;
                generate = !cacheThis.UseCachedValues(drawRect, color1, color2, factorX, factorY, orientation);
            }
            else
            {
                cache.Second?.Dispose();

                cacheThis = new MementoBackGlassRadial(drawRect, color1, color2, factorX, factorY, orientation);
                cache.Second = cacheThis;
            }

            // Do we need to generate the contents of the cache?
            if (generate)
            {
                // Dispose of existing values
                cacheThis.Dispose();

                // Find the bottom area rectangle

                RectangleF mainRect = orientation switch
                {
                    VisualOrientation.Right => drawRect with { Width = drawRect.Width - glassRect.Width - 1 },
                    VisualOrientation.Left => drawRect with { X = glassRect.Right + 1, Width = drawRect.Width - glassRect.Width - 1 },
                    VisualOrientation.Bottom => drawRect with { Height = drawRect.Height - glassRect.Height - 1 },
                    VisualOrientation.Top => drawRect with { Y = glassRect.Bottom + 1, Height = drawRect.Height - glassRect.Height - 1 },
                    _ => drawRect with { Y = glassRect.Bottom + 1, Height = drawRect.Height - glassRect.Height - 1 }
                };

                RectangleF doubleRect;

                // Find the box that encloses the ellipse (ellipses is sized using the factorX, factorY)
                if (VerticalOrientation(orientation))
                {
                    var mainRectWidth = mainRect.Width * factorX;
                    var mainRectWidthOffset = (mainRectWidth - mainRect.Width) / 2;
                    var mainRectHeight = mainRect.Height * factorY;
                    float mainRectHeightOffset;

                    // Find orientation specific ellsipe rectangle
                    if (orientation == VisualOrientation.Top)
                    {
                        mainRectHeightOffset = (mainRectHeight - mainRect.Height) / 2;
                    }
                    else
                    {
                        mainRectHeightOffset = mainRectHeight + ((mainRectHeight - mainRect.Height) / 2);
                    }

                    doubleRect = new RectangleF(mainRect.X - mainRectWidthOffset,
                        mainRect.Y - mainRectHeightOffset,
                        mainRectWidth, mainRectHeight * 2);
                }
                else
                {
                    var mainRectHeight = mainRect.Height * factorX;
                    var mainRectHeightOffset = (mainRectHeight - mainRect.Height) / 2;
                    var mainRectWidth = mainRect.Width * factorY;
                    float mainRectWidthOffset;

                    // Find orientation specific ellsipe rectangle
                    if (orientation == VisualOrientation.Left)
                    {
                        mainRectWidthOffset = (mainRectWidth - mainRect.Width) / 2;
                    }
                    else
                    {
                        mainRectWidthOffset = mainRectWidth + ((mainRectWidth - mainRect.Width) / 2);
                    }

                    doubleRect = new RectangleF(mainRect.X - mainRectWidthOffset,
                        mainRect.Y - mainRectHeightOffset,
                        mainRectWidth * 2, mainRectHeight);
                }

                // Cannot draw a path that contains a zero sized element
                if (doubleRect is { Width: > 0, Height: > 0 })
                {
                    // We use a path to create an ellipse for the light effect in the bottom of the area
                    cacheThis.Path = new GraphicsPath();
                    cacheThis.Path.AddEllipse(doubleRect);

                    // Create a brush from the path
                    cacheThis.BottomBrush = new PathGradientBrush(cacheThis.Path)
                    {
                        CenterColor = color2,
                        CenterPoint = new PointF(doubleRect.X + (doubleRect.Width / 2), doubleRect.Y + (doubleRect.Height / 2)),
                        SurroundColors = [color1]
                    };
                    cacheThis.MainRect = mainRect;
                }
            }

            if (cacheThis.BottomBrush != null)
            {
                g.FillRectangle(cacheThis.BottomBrush, cacheThis.MainRect);
            }
        }

        return memento;
    }

    private static IDisposable DrawBackGlassCenter(RectangleF drawRect,
        Color color1,
        Color color2,
        Color glassColor1,
        Color glassColor2,
        float factorX,
        float factorY,
        VisualOrientation orientation,
        Graphics? g,
        float glassPercent,
        IDisposable memento)
    {
        if (g is not null)
        {
            // Cannot draw a path that contains a zero sized element
            if (drawRect is { Width: > 0, Height: > 0 })
            {
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

                // Draw the gradient effect background
                DrawBackGlassBasic(drawRect, color1, color2,
                    glassColor1, glassColor2,
                    factorX, factorY,
                    orientation, g,
                    glassPercent,
                    ref cache.First!);

                var generate = true;
                MementoBackGlassCenter cacheThis;

                // Access a cache instance and decide if cache resources need generating
                if (cache.Second is MementoBackGlassCenter glassCenter)
                {
                    cacheThis = glassCenter;
                    generate = !cacheThis.UseCachedValues(drawRect, color2);
                }
                else
                {
                    cache.Second?.Dispose();

                    cacheThis = new MementoBackGlassCenter(drawRect, color2);
                    cache.Second = cacheThis;
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cacheThis.Dispose();

                    cacheThis.Path = new GraphicsPath();
                    cacheThis.Path.AddEllipse(drawRect);
                    cacheThis.BottomBrush = new PathGradientBrush(cacheThis.Path)
                    {
                        CenterColor = color2,
                        CenterPoint = new PointF(drawRect.X + (drawRect.Width / 2), drawRect.Y + (drawRect.Height / 2)),
                        SurroundColors = [Color.Transparent]
                    };
                }

                g.FillRectangle(cacheThis.BottomBrush!, drawRect);
            }
        }

        return memento;
    }

    private static IDisposable DrawBackGlassFade(RectangleF drawRect,
        RectangleF outerRect,
        Color color1,
        Color color2,
        Color glassColor1,
        Color glassColor2,
        VisualOrientation orientation,
        Graphics? g,
        IDisposable memento)
    {
        if (g is not null)
        {
            // Cannot draw a zero length rectangle
            if (drawRect is { Width: > 0, Height: > 0 } &&
                outerRect is { Width: > 0, Height: > 0 })
            {
                var generate = true;
                MementoBackGlassFade cache;

                // Access a cache instance and decide if cache resources need generating
                if (memento is MementoBackGlassFade glassFade)
                {
                    cache = glassFade;
                    generate = !cache.UseCachedValues(drawRect, outerRect, color1, color2,
                        glassColor1, glassColor2, orientation);
                }
                else
                {
                    memento?.Dispose();

                    cache = new MementoBackGlassFade(drawRect, outerRect, color1, color2,
                        glassColor1, glassColor2, orientation);
                    memento = cache;
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    // Create gradient rect from the drawing rect
                    RectangleF gradientRect = new RectangleF(drawRect.X - 1, drawRect.Y - 1, drawRect.Width + 2,
                        drawRect.Height + 2);

                    // Cannot draw a zero sized rectangle
                    if (gradientRect is { Width: > 0, Height: > 0 })
                    {
                        // Draw a gradient from first to second over the length, but use the
                        // first color for the first 33% of distance and fade over the rest
                        cache.MainBrush = new LinearGradientBrush(gradientRect, color1, color2, AngleFromOrientation(orientation))
                        {
                            Blend = _glassFadeBlend
                        };
                    }

                    float glassLength;

                    // Glass covers 33% of the orienatated length
                    if (VerticalOrientation(orientation))
                    {
                        glassLength = (int)(outerRect.Height * 0.33f) + outerRect.Y - drawRect.Y;
                    }
                    else
                    {
                        glassLength = (int)(outerRect.Width * 0.33f) + outerRect.X - drawRect.X;
                    }

                    RectangleF glassRect;
                    RectangleF mainRect;

                    // Create rectangles that cover the glass and main area
                    switch (orientation)
                    {
                        case VisualOrientation.Left:
                            glassRect = drawRect with { Width = glassLength };
                            break;
                        case VisualOrientation.Right:
                            mainRect = drawRect with { Width = drawRect.Width - glassLength };
                            glassRect = drawRect with { X = mainRect.Right, Width = glassLength };
                            break;
                        case VisualOrientation.Top:
                        default:
                            glassRect = drawRect with { Height = glassLength };
                            break;
                        case VisualOrientation.Bottom:
                            mainRect = drawRect with { Height = drawRect.Height - glassLength };
                            glassRect = drawRect with { Y = mainRect.Bottom, Height = glassLength };
                            break;
                    }

                    // Create gradient rectangles
                    RectangleF glassGradientRect = new RectangleF(glassRect.X - 1, glassRect.Y - 1, glassRect.Width + 2,
                        glassRect.Height + 2);

                    // Cannot draw a zero sized rectangle
                    if (glassRect is { Width: > 0, Height: > 0 } &&
                        glassGradientRect is { Width: > 0, Height: > 0 })
                    {
                        // Use semi-transparent white colors to create the glass effect
                        cache.TopBrush = new LinearGradientBrush(glassGradientRect, glassColor1, glassColor2, AngleFromOrientation(orientation));
                        cache.GlassRect = glassRect;
                    }
                }

                if (cache.MainBrush != null)
                {
                    g.FillRectangle(cache.MainBrush, drawRect);
                }

                if (cache.TopBrush != null)
                {
                    g.FillRectangle(cache.TopBrush, cache.GlassRect);
                }
            }
        }

        return memento;
    }

    private static IDisposable? DrawBackGlassLinear(RectangleF drawRect,
        RectangleF outerRect,
        Color color1,
        Color color2,
        Color glassColor1,
        Color glassColor2,
        VisualOrientation orientation,
        Graphics? g,
        float glassPercent,
        IDisposable? memento)
    {
        if (g is not null)
        {
            // Cannot draw a zero length rectangle
            if (drawRect is { Width: > 0, Height: > 0 } &&
                outerRect is { Width: > 0, Height: > 0 })
            {
                var generate = true;
                MementoBackGlassLinear cache;

                // Access a cache instance and decide if cache resources need generating
                if (memento is MementoBackGlassLinear glassLinear)
                {
                    cache = glassLinear;
                    generate = !cache.UseCachedValues(drawRect, outerRect, color1, color2,
                        glassColor1, glassColor2, orientation, glassPercent);
                }
                else
                {
                    memento?.Dispose();

                    cache = new MementoBackGlassLinear(drawRect, outerRect, color1, color2,
                        glassColor1, glassColor2, orientation, glassPercent);
                    memento = cache;
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    RectangleF glassRect;
                    RectangleF mainRect;
                    float glassLength;

                    // Glass covers specified percentage of the orienatated length
                    if (VerticalOrientation(orientation))
                    {
                        glassLength = (int)(outerRect.Height * glassPercent) + outerRect.Y - drawRect.Y;
                    }
                    else
                    {
                        glassLength = (int)(outerRect.Width * glassPercent) + outerRect.X - drawRect.X;
                    }

                    // Create rectangles that cover the glass and main area
                    switch (orientation)
                    {
                        case VisualOrientation.Left:
                            glassRect = drawRect with { Width = glassLength };
                            mainRect = drawRect with { X = glassRect.Right + 1, Width = drawRect.Width - glassRect.Width - 1 };
                            break;
                        case VisualOrientation.Right:
                            mainRect = drawRect with { Width = drawRect.Width - glassLength };
                            glassRect = drawRect with { X = mainRect.Right, Width = glassLength };
                            break;
                        case VisualOrientation.Top:
                        default:
                            glassRect = drawRect with { Height = glassLength };
                            mainRect = drawRect with { Y = glassRect.Bottom + 1, Height = drawRect.Height - glassRect.Height - 1 };
                            break;
                        case VisualOrientation.Bottom:
                            mainRect = drawRect with { Height = drawRect.Height - glassLength };
                            glassRect = drawRect with { Y = mainRect.Bottom, Height = glassLength };
                            break;
                    }

                    cache.TotalBrush = new SolidBrush(color1);
                    cache.GlassRect = glassRect;
                    cache.MainRect = mainRect;

                    // Create gradient rectangles
                    RectangleF glassGradientRect = new RectangleF(cache.GlassRect.X - 1, cache.GlassRect.Y - 1,
                        cache.GlassRect.Width + 2, cache.GlassRect.Height + 2);
                    RectangleF mainGradientRect = new RectangleF(cache.MainRect.X - 1, cache.MainRect.Y - 1,
                        cache.MainRect.Width + 2, cache.MainRect.Height + 2);

                    // Cannot draw a zero length rectangle
                    if (cache.GlassRect is { Width: > 0, Height: > 0 } &&
                        cache.MainRect is { Width: > 0, Height: > 0 } &&
                        glassGradientRect is { Width: > 0, Height: > 0 } &&
                        mainGradientRect is { Width: > 0, Height: > 0 })
                    {
                        cache.TopBrush = new LinearGradientBrush(glassGradientRect, glassColor1, glassColor2, AngleFromOrientation(orientation));
                        cache.BottomBrush = new LinearGradientBrush(mainGradientRect, color1, color2, AngleFromOrientation(orientation));
                    }
                }

                // Draw entire area in a solid color
                g.FillRectangle(cache.TotalBrush!, drawRect);

                if (cache is { TopBrush: not null, BottomBrush: not null })
                {
                    g.FillRectangle(cache.TopBrush, cache.GlassRect);
                    g.FillRectangle(cache.BottomBrush, cache.MainRect);
                }
            }
        }

        return memento;
    }

    private static RectangleF DrawBackGlassBasic(RectangleF drawRect,
        Color color1,
        Color color2,
        Color glassColor1,
        Color glassColor2,
        float factorX,
        float factorY,
        VisualOrientation orientation,
        Graphics? g,
        float glassPercent,
        ref IDisposable memento)
    {
        if (g is not null)
        {
            // Cannot draw a zero length rectangle
            if (drawRect is { Width: > 0, Height: > 0 })
            {
                var generate = true;
                MementoBackGlassBasic cache;

                // Access a cache instance and decide if cache resources need generating
                if (memento is MementoBackGlassBasic glassBasic)
                {
                    cache = glassBasic;
                    generate = !cache.UseCachedValues(drawRect, color1, color2,
                        glassColor1, glassColor2,
                        factorX, factorY,
                        orientation, glassPercent);
                }
                else
                {
                    memento?.Dispose();

                    cache = new MementoBackGlassBasic(drawRect, color1, color2,
                        glassColor1, glassColor2,
                        factorX, factorY,
                        orientation, glassPercent);
                    memento = cache;
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    // Draw entire area in a solid color
                    cache.TotalBrush = new SolidBrush(color1);

                    int length;

                    if (VerticalOrientation(orientation))
                    {
                        length = (int)(drawRect.Height * glassPercent);
                    }
                    else
                    {
                        length = (int)(drawRect.Width * glassPercent);
                    }

                    var glassRect = orientation switch
                    {
                        VisualOrientation.Left => drawRect with { Width = length },
                        VisualOrientation.Right => drawRect with { X = drawRect.Right - length, Width = length },
                        VisualOrientation.Bottom => drawRect with { Y = drawRect.Bottom - length, Height = length },
                        _ => drawRect with { Height = length }
                    };

                    // Gradient rectangle is always a little bigger to prevent tiling at edges
                    RectangleF glassGradientRect = new RectangleF(glassRect.X - 1, glassRect.Y - 1, glassRect.Width + 2,
                        glassRect.Height + 2);

                    // Cannot draw a zero length rectangle
                    if (glassGradientRect is { Width: > 0, Height: > 0 })
                    {
                        cache.GlassBrush = new LinearGradientBrush(glassGradientRect, glassColor1, glassColor2, AngleFromOrientation(orientation));
                        cache.GlassRect = glassRect;
                    }
                }

                g.FillRectangle(cache.TotalBrush!, drawRect);

                if (cache.GlassBrush != null)
                {
                    g.FillRectangle(cache.GlassBrush, cache.GlassRect);
                    return cache.GlassRect;
                }
            }
        }

        return RectangleF.Empty;
    }

    private static IDisposable DrawBackLinear(RectangleF drawRect,
        bool sigma,
        Color color1,
        Color color2,
        VisualOrientation orientation,
        Graphics? g,
        IDisposable memento)
    {
        if (g is not null)
        {
            // Cannot draw a zero length rectangle
            if (drawRect is { Width: > 0, Height: > 0 })
            {
                var generate = true;
                MementoBackLinear cache;

                // Access a cache instance and decide if cache resources need generating
                if (memento is MementoBackLinear backLinear)
                {
                    cache = backLinear;
                    generate = !cache.UseCachedValues(drawRect, sigma, color1, color2, orientation);
                }
                else
                {
                    memento?.Dispose();

                    cache = new MementoBackLinear(drawRect, sigma, color1, color2, orientation);
                    memento = cache;
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    // Create rectangle that covers the enter area
                    RectangleF gradientRect = new RectangleF(drawRect.X - 1, drawRect.Y - 1, drawRect.Width + 2,
                        drawRect.Height + 2);

                    // Cannot draw a zero length rectangle
                    if (gradientRect is { Width: > 0, Height: > 0 })
                    {
                        // Draw entire area in a gradient color effect
                        cache.EntireBrush = new LinearGradientBrush(gradientRect, color1, color2, AngleFromOrientation(orientation));

                        if (sigma)
                        {
                            cache.EntireBrush.SetSigmaBellShape(0.5f);
                        }
                    }
                }

                if (cache.EntireBrush != null)
                {
                    g.FillRectangle(cache.EntireBrush, drawRect);
                }
            }
        }

        return memento;
    }

    private static IDisposable DrawBackDarkEdge(RectangleF drawRect,
        Color color1,
        int thickness,
        VisualOrientation orientation,
        Graphics? g,
        IDisposable memento)
    {
        if (g is not null)
        {
            // Cannot draw a zero length rectangle
            if (drawRect is { Width: > 0, Height: > 0 })
            {
                var generate = true;
                MementoBackDarkEdge cache;

                // Access a cache instance and decide if cache resources need generating
                if (memento is MementoBackDarkEdge darkEdge)
                {
                    cache = darkEdge;
                    generate = !cache.UseCachedValues(drawRect, color1, thickness, orientation);
                }
                else
                {
                    memento?.Dispose();

                    cache = new MementoBackDarkEdge(drawRect, color1, thickness, orientation);
                    memento = cache;
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    // If we need to scale down the dark thickness
                    if (VerticalOrientation(orientation))
                    {
                        if (drawRect.Height < 30)
                        {
                            thickness = (int)drawRect.Height / 10;
                        }
                    }
                    else
                    {
                        if (drawRect.Width < 30)
                        {
                            thickness = (int)drawRect.Width / 10;
                        }
                    }

                    // If there is something to draw
                    if (thickness >= 0)
                    {
                        // Alter rectangle to the drawing edge only
                        switch (orientation)
                        {
                            case VisualOrientation.Top:
                                drawRect.Height = thickness;
                                break;
                            case VisualOrientation.Left:
                                drawRect.Width = thickness;
                                break;
                            case VisualOrientation.Bottom:
                                drawRect.Y = drawRect.Bottom - thickness - 1;
                                drawRect.Height = thickness + 1;
                                break;
                            case VisualOrientation.Right:
                                drawRect.X = drawRect.Right - thickness - 1;
                                drawRect.Width = thickness + 1;
                                break;

                        }

                        // Create rectangle that covers the enter area
                        RectangleF gradientRect = new RectangleF(drawRect.X - 0.5f, drawRect.Y - 0.5f,
                            drawRect.Width + 1, drawRect.Height + 1);

                        // Cannot draw a zero length rectangle
                        if (gradientRect is { Width: > 0, Height: > 0 })
                        {
                            // Draw entire area in a gradient color effect
                            cache.EntireBrush = new LinearGradientBrush(gradientRect, Color.FromArgb(64, color1), Color.Transparent, AngleFromOrientation(orientation));
                            cache.EntireBrush.SetSigmaBellShape(1.0f);
                            cache.EntireRect = drawRect;
                        }
                    }
                }

                if (cache.EntireBrush != null)
                {
                    g.FillRectangle(cache.EntireBrush, cache.EntireRect);
                }
            }
        }

        return memento;
    }

    private static bool VerticalOrientation(VisualOrientation orientation) =>
        orientation is VisualOrientation.Top or VisualOrientation.Bottom;

    private static float AngleFromOrientation(VisualOrientation orientation) => orientation switch
    {
        VisualOrientation.Bottom => 270f,
        VisualOrientation.Left => 0f,
        VisualOrientation.Right => 180,
        VisualOrientation.Top => 90f,
        _ => 90f
    };

    private static void ModifyRectByEdges(ref Rectangle rect,
        int left,
        int top,
        int right,
        int bottom,
        VisualOrientation orientation)
    {
        switch (orientation)
        {
            case VisualOrientation.Top:
                rect.X += left;
                rect.Width -= left + right;
                rect.Y += top;
                rect.Height -= top + bottom;
                break;
            case VisualOrientation.Bottom:
                rect.X += left;
                rect.Width -= left + right;
                rect.Y += bottom;
                rect.Height -= top + bottom;
                break;
            case VisualOrientation.Left:
                rect.X += top;
                rect.Width -= top + bottom;
                rect.Y += right;
                rect.Height -= left + right;
                break;
            case VisualOrientation.Right:
                rect.X += bottom;
                rect.Width -= top + bottom;
                rect.Y += left;
                rect.Height -= left + right;
                break;
        }
    }
    #endregion
}