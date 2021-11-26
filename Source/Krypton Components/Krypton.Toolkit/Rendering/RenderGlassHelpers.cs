#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Toolkit
{
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
                Positions = new float[] { 0.0f, 0.33f, 0.66f, 1.0f },
                Factors = new float[] { 0.0f, 0.0f, 0.8f, 1.0f }
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
        public static IDisposable DrawBackGlassCenter(RenderContext context,
                                                      Rectangle rect,
                                                      Color backColor1,
                                                      Color backColor2,
                                                      VisualOrientation orientation,
                                                      GraphicsPath path,
                                                      IDisposable memento)
        {
            using Clipping clip = new(context.Graphics, path);
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
            cache.first = DrawBackLinearRadial(rect, false,
                ControlPaint.LightLight(backColor2),
                ControlPaint.Light(backColor2),
                ControlPaint.LightLight(backColor2),
                orientation, context.Graphics,
                cache.first);

            // Reduce size of the inside area
            rect.Inflate(-1, -1);

            // Draw the inside area as a glass effect
            cache.second = DrawBackGlassCenter(rect, backColor1, backColor2,
                _glassColorTopL, _glassColorBottomL,
                2f, 1f, orientation, context.Graphics,
                FULL_GLASS_LENGTH, cache.second);

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
        public static IDisposable DrawBackGlassBottom(RenderContext context,
                                                      Rectangle rect,
                                                      Color backColor1,
                                                      Color backColor2,
                                                      VisualOrientation orientation,
                                                      GraphicsPath path,
                                                      IDisposable memento)
        {
            using Clipping clip = new(context.Graphics, path);
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
            cache.first = DrawBackLinear(rect, false,
                ControlPaint.Light(backColor1),
                ControlPaint.LightLight(backColor1),
                orientation, context.Graphics,
                cache.first);

            // Reduce size on all but the upper edge
            ModifyRectByEdges(ref rect, 1, 0, 1, 1, orientation);

            // Draw the inside areas as a glass effect
            cache.second = DrawBackGlassRadial(rect, backColor1, backColor2,
                _glassColorTopD, _glassColorBottomD,
                3f, 1.1f, orientation, context.Graphics,
                FULL_GLASS_LENGTH, cache.second);

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
        public static IDisposable DrawBackGlassFade(RenderContext context,
                                                    Rectangle rect,
                                                    Color backColor1,
                                                    Color backColor2,
                                                    VisualOrientation orientation,
                                                    GraphicsPath path,
                                                    IDisposable memento)
        {
            using Clipping clip = new(context.Graphics, path);
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

            cache.first = DrawBackGlassFade(rect, rect,
                backColor1, backColor2,
                _glassColorTopL,
                _glassColorBottomL,
                orientation,
                context.Graphics,
                cache.first);

            cache.second = DrawBackDarkEdge(rect, ControlPaint.Dark(backColor1),
                3, orientation, context.Graphics, 
                cache.second);

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
        public static IDisposable DrawBackGlassSimpleFull(RenderContext context,
                                                          Rectangle rect,
                                                          Color backColor1,
                                                          Color backColor2,
                                                          VisualOrientation orientation,
                                                          GraphicsPath path,
                                                          IDisposable memento) =>
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
        public static IDisposable DrawBackGlassNormalFull(RenderContext context,
                                                          Rectangle rect,
                                                          Color backColor1,
                                                          Color backColor2,
                                                          VisualOrientation orientation,
                                                          GraphicsPath path,
                                                          IDisposable memento) =>
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
        public static IDisposable DrawBackGlassTrackingFull(RenderContext context,
                                                            Rectangle rect,
                                                            Color backColor1,
                                                            Color backColor2,
                                                            VisualOrientation orientation,
                                                            GraphicsPath path,
                                                            IDisposable memento) =>
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
        public static IDisposable DrawBackGlassCheckedFull(RenderContext context,
                                                           Rectangle rect,
                                                           Color backColor1,
                                                           Color backColor2,
                                                           VisualOrientation orientation,
                                                           GraphicsPath path,
                                                           IDisposable memento) =>
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
        public static IDisposable DrawBackGlassCheckedTrackingFull(RenderContext context,
                                                                   Rectangle rect,
                                                                   Color backColor1,
                                                                   Color backColor2,
                                                                   VisualOrientation orientation,
                                                                   GraphicsPath path,
                                                                   IDisposable memento) =>
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
        public static IDisposable DrawBackGlassPressedFull(RenderContext context,
                                                           Rectangle rect,
                                                           Color backColor1,
                                                           Color backColor2,
                                                           VisualOrientation orientation,
                                                           GraphicsPath path,
                                                           IDisposable memento) =>
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
        public static IDisposable DrawBackGlassNormalStump(RenderContext context,
                                                           Rectangle rect,
                                                           Color backColor1,
                                                           Color backColor2,
                                                           VisualOrientation orientation,
                                                           GraphicsPath path,
                                                           IDisposable memento) =>
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
        public static IDisposable DrawBackGlassTrackingStump(RenderContext context,
                                                             Rectangle rect,
                                                             Color backColor1,
                                                             Color backColor2,
                                                             VisualOrientation orientation,
                                                             GraphicsPath path,
                                                             IDisposable memento) =>
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
        public static IDisposable DrawBackGlassPressedStump(RenderContext context,
                                                            Rectangle rect,
                                                            Color backColor1,
                                                            Color backColor2,
                                                            VisualOrientation orientation,
                                                            GraphicsPath path,
                                                            IDisposable memento) =>
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
        public static IDisposable DrawBackGlassCheckedStump(RenderContext context,
                                                            Rectangle rect,
                                                            Color backColor1,
                                                            Color backColor2,
                                                            VisualOrientation orientation,
                                                            GraphicsPath path,
                                                            IDisposable memento) =>
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
        public static IDisposable DrawBackGlassCheckedTrackingStump(RenderContext context,
                                                                    Rectangle rect,
                                                                    Color backColor1,
                                                                    Color backColor2,
                                                                    VisualOrientation orientation,
                                                                    GraphicsPath path,
                                                                    IDisposable memento) =>
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
        public static IDisposable DrawBackGlassThreeEdge(RenderContext context,
                                                         Rectangle rect,
                                                         Color backColor1,
                                                         Color backColor2,
                                                         VisualOrientation orientation,
                                                         GraphicsPath path,
                                                         IDisposable memento)
        {
            using Clipping clip = new(context.Graphics, path);
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
                cache.colorA1L = CommonHelper.MergeColors(backColor1, 0.7f, Color.White, 0.3f);
                cache.colorA2L = CommonHelper.MergeColors(backColor2, 0.7f, Color.White, 0.3f);
                cache.colorA2LL = CommonHelper.MergeColors(cache.colorA2L, 0.8f, Color.White, 0.2f);
                cache.colorB2LL = CommonHelper.MergeColors(backColor2, 0.8f, Color.White, 0.2f);
                cache.rectB = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 1, rect.Height - 2);
            }

            // Draw entire area in a lighter version
            cache.first = DrawBackGlassLinear(rect, rect,
                cache.colorA1L, _glassColorLight,
                cache.colorA2L, cache.colorA2LL,
                orientation,
                context.Graphics,
                FULL_GLASS_LENGTH,
                cache.first);

                
            // Draw the inside area in the full color
            cache.second = DrawBackGlassLinear(cache.rectB, cache.rectB,
                backColor1, _glassColorLight,
                backColor2, cache.colorB2LL,
                orientation,
                context.Graphics,
                FULL_GLASS_LENGTH,
                cache.second);

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
        public static IDisposable DrawBackGlassNormalSimple(RenderContext context,
                                                            Rectangle rect,
                                                            Color backColor1,
                                                            Color backColor2,
                                                            VisualOrientation orientation,
                                                            GraphicsPath path, 
                                                            IDisposable memento)
        {
            using Clipping clip = new(context.Graphics, path);
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
        public static IDisposable DrawBackGlassTrackingSimple(RenderContext context,
                                                              Rectangle rect,
                                                              Color backColor1,
                                                              Color backColor2,
                                                              VisualOrientation orientation,
                                                              GraphicsPath path,
                                                              IDisposable memento)
        {
            using Clipping clip = new(context.Graphics, path);
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
        public static IDisposable DrawBackGlassCheckedSimple(RenderContext context,
                                                             Rectangle rect,
                                                             Color backColor1,
                                                             Color backColor2,
                                                             VisualOrientation orientation,
                                                             GraphicsPath path,
                                                             IDisposable memento)
        {
            using Clipping clip = new(context.Graphics, path);
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
        public static IDisposable DrawBackGlassCheckedTrackingSimple(RenderContext context,
                                                                     Rectangle rect,
                                                                     Color backColor1,
                                                                     Color backColor2,
                                                                     VisualOrientation orientation,
                                                                     GraphicsPath path,
                                                                     IDisposable memento)
        {
            using Clipping clip = new(context.Graphics, path);
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
        public static IDisposable DrawBackGlassPressedSimple(RenderContext context,
                                                             Rectangle rect,
                                                             Color backColor1,
                                                             Color backColor2,
                                                             VisualOrientation orientation,
                                                             GraphicsPath path,
                                                             IDisposable memento)
        {
            using Clipping clip = new(context.Graphics, path);
            // Draw the inside areas as a glass effect
            return DrawBackGlassRadial(rect, backColor1, backColor2,
                _glassColorTopD, _glassColorBottomD,
                3f, 1.1f, orientation, context.Graphics,
                FULL_GLASS_LENGTH, memento);
        }

        #endregion

        #region Implementation
        private static IDisposable DrawBackGlassSimplePercent(RenderContext context,
                                                              Rectangle rect,
                                                              Color backColor1,
                                                              Color backColor2,
                                                              VisualOrientation orientation,
                                                              GraphicsPath path,
                                                              float glassPercent,
                                                              IDisposable memento)
        {
            using Clipping clip = new(context.Graphics, path);
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
            RectangleF drawRect = new(rect.X, rect.Y, rect.Width, rect.Height);

            // Draw the border as a lighter version of the inside
            cache.first = DrawBackGlassLinear(drawRect, drawRect,
                backColor2,
                backColor2,
                _glassColorBottomDD,
                _glassColorBottomDD,
                orientation,
                context.Graphics,
                0,
                cache.first);

            // Reduce by 1 pixel on all edges to get the inside
            RectangleF insetRect = drawRect;
            insetRect.Inflate(-1f, -1f);

            // Draw the inside area
            cache.second = DrawBackGlassLinear(insetRect, drawRect,
                backColor1, 
                CommonHelper.MergeColors(backColor1, 0.5f, backColor2, 0.5f),
                _glassColorTopDD,
                _glassColorBottomDD,
                orientation,
                context.Graphics,
                glassPercent,
                cache.second);

            return memento;
        }

        private static IDisposable DrawBackGlassNormalPercent(RenderContext context,
                                                              Rectangle rect,
                                                              Color backColor1,
                                                              Color backColor2,
                                                              VisualOrientation orientation,
                                                              GraphicsPath path,
                                                              float glassPercent,
                                                              IDisposable memento)
        {
            using Clipping clip = new(context.Graphics, path);
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
            RectangleF drawRect = new(rect.X, rect.Y, rect.Width, rect.Height);

            // Draw the border as a lighter version of the inside
            cache.first = DrawBackGlassLinear(drawRect, drawRect,
                Color.White,
                Color.White,
                _glassColorTopL,
                _glassColorBottomL,
                orientation,
                context.Graphics,
                glassPercent,
                cache.first);

            // Reduce by 1 pixel on all edges to get the inside
            RectangleF insetRect = drawRect;
            insetRect.Inflate(-1f, -1f);

            // Draw the inside area
            cache.second = DrawBackGlassLinear(insetRect, drawRect,
                backColor1, backColor2,
                _glassColorTopL,
                _glassColorBottomL,
                orientation,
                context.Graphics,
                glassPercent,
                cache.second);

            return memento;
        }

        private static IDisposable DrawBackGlassTrackingPercent(RenderContext context,
                                                                Rectangle rect,
                                                                Color backColor1,
                                                                Color backColor2,
                                                                VisualOrientation orientation,
                                                                GraphicsPath path,
                                                                float glassPercent,
                                                                IDisposable memento)
        {
            using Clipping clip = new(context.Graphics, path);
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
            cache.first = DrawBackLinearRadial(rect, false,
                ControlPaint.LightLight(backColor2),
                ControlPaint.Light(backColor2),
                ControlPaint.LightLight(backColor2),
                orientation, context.Graphics,
                cache.first);

            // Reduce size of the inside area
            rect.Inflate(-1, -1);

            // Draw the inside area as a glass effect
            cache.second = DrawBackGlassRadial(rect, backColor1, backColor2,
                _glassColorTopL, _glassColorBottomL,
                2f, 1f, orientation, context.Graphics,
                glassPercent, cache.second);

            return memento;
        }

        private static IDisposable DrawBackGlassPressedPercent(RenderContext context,
                                                               Rectangle rect,
                                                               Color backColor1,
                                                               Color backColor2,
                                                               VisualOrientation orientation,
                                                               GraphicsPath path,
                                                               float glassPercent,
                                                               IDisposable memento)
        {
            using Clipping clip = new(context.Graphics, path);
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
            cache.first = DrawBackLinear(rect, false,
                ControlPaint.Light(backColor1),
                ControlPaint.LightLight(backColor1),
                orientation, context.Graphics,
                cache.first);

            // Reduce size on all but the upper edge
            ModifyRectByEdges(ref rect, 1, 0, 1, 1, orientation);

            // Draw the inside areas as a glass effect
            cache.second = DrawBackGlassRadial(rect, backColor1, backColor2,
                _glassColorTopD, _glassColorBottomD,
                3f, 1.1f, orientation, context.Graphics,
                glassPercent, cache.second);

            // Widen back to original
            ModifyRectByEdges(ref rect, -1, 0, -1, 0, orientation);

            cache.third = DrawBackDarkEdge(rect, ControlPaint.Dark(backColor1),
                3, orientation, context.Graphics,
                cache.third);

            return memento;
        }

        private static IDisposable DrawBackGlassCheckedPercent(RenderContext context,
                                                               Rectangle rect,
                                                               Color backColor1,
                                                               Color backColor2,
                                                               VisualOrientation orientation,
                                                               GraphicsPath path,
                                                               float glassPercent,
                                                               IDisposable memento)
        {
            using Clipping clip = new(context.Graphics, path);
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
            cache.first = DrawBackLinearRadial(rect, false,
                ControlPaint.Light(backColor1),
                ControlPaint.LightLight(backColor1),
                ControlPaint.LightLight(backColor1),
                orientation, context.Graphics,
                cache.first);

            // Reduce size on all but the upper edge
            ModifyRectByEdges(ref rect, 1, 0, 1, 1, orientation);

            // Draw the inside areas as a glass effect
            cache.second = DrawBackGlassRadial(rect, backColor1, backColor2,
                _glassColorTopL, _glassColorBottomL,
                6f, 1.2f, orientation, context.Graphics,
                glassPercent, cache.second);

            // Widen back to original
            ModifyRectByEdges(ref rect, -1, 0, -1, 0, orientation);

            // Draw a darker area for top edge
            cache.third = DrawBackDarkEdge(rect, ControlPaint.Dark(backColor1),
                3, orientation, context.Graphics,
                cache.third);

            return memento;
        }

        private static IDisposable DrawBackGlassCheckedTrackingPercent(RenderContext context,
                                                                       Rectangle rect,
                                                                       Color backColor1,
                                                                       Color backColor2,
                                                                       VisualOrientation orientation,
                                                                       GraphicsPath path,
                                                                       float glassPercent,
                                                                       IDisposable memento)
        {
            using Clipping clip = new(context.Graphics, path);
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
            cache.first = DrawBackLinear(rect, true,
                backColor2,
                ControlPaint.LightLight(backColor2),
                orientation,
                context.Graphics,
                cache.first);

            // Reduce size on all but the upper edge
            ModifyRectByEdges(ref rect, 1, 0, 1, 1, orientation);

            // Draw the inside areas as a glass effect
            cache.second = DrawBackGlassRadial(rect, backColor1, backColor2,
                _glassColorTopD, _glassColorBottomD,
                5f, 1.2f, orientation, context.Graphics,
                glassPercent, cache.second);

            // Widen back to original
            ModifyRectByEdges(ref rect, -1, 0, -1, 0, orientation);

            cache.third = DrawBackDarkEdge(rect, ControlPaint.Dark(backColor1),
                3, orientation, context.Graphics,
                cache.third);

            return memento;
        }

        private static IDisposable DrawBackLinearRadial(RectangleF drawRect,
                                                        bool sigma,
                                                        Color color1,
                                                        Color color2,
                                                        Color color3,
                                                        VisualOrientation orientation,
                                                        Graphics g,
                                                        IDisposable memento)
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
            cache.first = DrawBackLinear(drawRect, sigma, color1, color2, orientation, g, cache.first);

            var generate = true;
            MementoBackLinearRadial cacheThis;

            // Access a cache instance and decide if cache resources need generating
            if (cache.second is MementoBackLinearRadial linearRadial)
            {
                cacheThis = linearRadial;
                generate = !cacheThis.UseCachedValues(drawRect, color2, color3, orientation);
            }
            else
            {
                cache.second?.Dispose();

                cacheThis = new MementoBackLinearRadial(drawRect, color2, color3, orientation);
                cache.second = cacheThis;
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

                cacheThis.ellipseRect = ellipseRect;

                // Cannot draw a path that contains a zero sized element
                if ((ellipseRect.Width > 0) && (ellipseRect.Height > 0))
                {
                    cacheThis.path = new GraphicsPath();
                    cacheThis.path.AddEllipse(ellipseRect);
                    cacheThis.bottomBrush = new PathGradientBrush(cacheThis.path)
                    {
                        CenterColor = ControlPaint.Light(color3),
                        CenterPoint = centerPoint,
                        SurroundColors = new Color[] { color2 }
                    };
                }
            }

            if (cacheThis.bottomBrush != null)
            {
                g.FillRectangle(cacheThis.bottomBrush, cacheThis.ellipseRect);
            }

            return memento;
        }

        private static IDisposable DrawBackGlassRadial(RectangleF drawRect,
                                                       Color color1,
                                                       Color color2,
                                                       Color glassColor1,
                                                       Color glassColor2,
                                                       float factorX,
                                                       float factorY,
                                                       VisualOrientation orientation,
                                                       Graphics g,
                                                       float glassPercent,
                                                       IDisposable memento)
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
                                                      ref cache.first);

            var generate = true;
            MementoBackGlassRadial cacheThis;

            // Access a cache instance and decide if cache resources need generating
            if (cache.second is MementoBackGlassRadial glassRadial)
            {
                cacheThis = glassRadial;
                generate = !cacheThis.UseCachedValues(drawRect, color1, color2, factorX, factorY, orientation);
            }
            else
            {
                cache.second?.Dispose();

                cacheThis = new MementoBackGlassRadial(drawRect, color1, color2, factorX, factorY, orientation);
                cache.second = cacheThis;
            }

            // Do we need to generate the contents of the cache?
            if (generate)
            {
                // Dispose of existing values
                cacheThis.Dispose();

                // Find the bottom area rectangle

                RectangleF mainRect = orientation switch
                {
                    VisualOrientation.Right => new RectangleF(drawRect.X, drawRect.Y, drawRect.Width - glassRect.Width - 1, drawRect.Height),
                    VisualOrientation.Left => new RectangleF(glassRect.Right + 1, drawRect.Y, drawRect.Width - glassRect.Width - 1, drawRect.Height),
                    VisualOrientation.Bottom => new RectangleF(drawRect.X, drawRect.Y, drawRect.Width, drawRect.Height - glassRect.Height - 1),
                    VisualOrientation.Top => new RectangleF(drawRect.X, glassRect.Bottom + 1, drawRect.Width, drawRect.Height - glassRect.Height - 1),
                    _ => new RectangleF(drawRect.X, glassRect.Bottom + 1, drawRect.Width, drawRect.Height - glassRect.Height - 1)
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
                if ((doubleRect.Width > 0) && (doubleRect.Height > 0))
                {
                    // We use a path to create an ellipse for the light effect in the bottom of the area
                    cacheThis.path = new GraphicsPath();
                    cacheThis.path.AddEllipse(doubleRect);

                    // Create a brush from the path
                    cacheThis.bottomBrush = new PathGradientBrush(cacheThis.path)
                    {
                        CenterColor = color2,
                        CenterPoint = new PointF(doubleRect.X + (doubleRect.Width / 2), doubleRect.Y + (doubleRect.Height / 2)),
                        SurroundColors = new Color[] { color1 }
                    };
                    cacheThis.mainRect = mainRect;
                }
            }

            if (cacheThis.bottomBrush != null)
            {
                g.FillRectangle(cacheThis.bottomBrush, cacheThis.mainRect);
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
                                                       Graphics g,
                                                       float glassPercent,
                                                       IDisposable memento)
        {
            // Cannot draw a path that contains a zero sized element
            if ((drawRect.Width > 0) && (drawRect.Height > 0))
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
                                   ref cache.first);

                var generate = true;
                MementoBackGlassCenter cacheThis;

                // Access a cache instance and decide if cache resources need generating
                if (cache.second is MementoBackGlassCenter glassCenter)
                {
                    cacheThis = glassCenter;
                    generate = !cacheThis.UseCachedValues(drawRect, color2);
                }
                else
                {
                    cache.second?.Dispose();

                    cacheThis = new MementoBackGlassCenter(drawRect, color2);
                    cache.second = cacheThis;
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cacheThis.Dispose();

                    cacheThis.path = new GraphicsPath();
                    cacheThis.path.AddEllipse(drawRect);
                    cacheThis.bottomBrush = new PathGradientBrush(cacheThis.path)
                    {
                        CenterColor = color2,
                        CenterPoint = new PointF(drawRect.X + (drawRect.Width / 2), drawRect.Y + (drawRect.Height / 2)),
                        SurroundColors = new Color[] { Color.Transparent }
                    };
                }

                g.FillRectangle(cacheThis.bottomBrush, drawRect);
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
                                                     Graphics g,
                                                     IDisposable memento)
        {
            // Cannot draw a zero length rectangle
            if ((drawRect.Width > 0) && (drawRect.Height > 0) &&
                (outerRect.Width > 0) && (outerRect.Height > 0))
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
                    RectangleF gradientRect = new(drawRect.X - 1, drawRect.Y - 1, drawRect.Width + 2, drawRect.Height + 2);

                    // Cannot draw a zero sized rectangle
                    if ((gradientRect.Width > 0) && (gradientRect.Height > 0))
                    {
                        // Draw a gradient from first to second over the length, but use the
                        // first color for the first 33% of distance and fade over the rest
                        cache.mainBrush = new LinearGradientBrush(gradientRect, color1, color2, AngleFromOrientation(orientation))
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
                            glassRect = new RectangleF(drawRect.X, drawRect.Y, glassLength, drawRect.Height);
                            break;
                        case VisualOrientation.Right:
                            mainRect = new RectangleF(drawRect.X, drawRect.Y, drawRect.Width - glassLength, drawRect.Height);
                            glassRect = new RectangleF(mainRect.Right, drawRect.Y, glassLength, drawRect.Height);
                            break;
                        case VisualOrientation.Top:
                        default:
                            glassRect = new RectangleF(drawRect.X, drawRect.Y, drawRect.Width, glassLength);
                            break;
                        case VisualOrientation.Bottom:
                            mainRect = new RectangleF(drawRect.X, drawRect.Y, drawRect.Width, drawRect.Height - glassLength);
                            glassRect = new RectangleF(drawRect.X, mainRect.Bottom, drawRect.Width, glassLength);
                            break;
                    }

                    // Create gradient rectangles
                    RectangleF glassGradientRect = new(glassRect.X - 1, glassRect.Y - 1, glassRect.Width + 2, glassRect.Height + 2);

                    // Cannot draw a zero sized rectangle
                    if ((glassRect.Width > 0) && (glassRect.Height > 0) &&
                        (glassGradientRect.Width > 0) && (glassGradientRect.Height > 0))
                    {
                        // Use semi-transparent white colors to create the glass effect
                        cache.topBrush = new LinearGradientBrush(glassGradientRect, glassColor1, glassColor2, AngleFromOrientation(orientation));
                        cache.glassRect = glassRect;
                    }
                }

                if (cache.mainBrush != null)
                {
                    g.FillRectangle(cache.mainBrush, drawRect);
                }

                if (cache.topBrush != null)
                {
                    g.FillRectangle(cache.topBrush, cache.glassRect);
                }
            }

            return memento;
        }

        private static IDisposable DrawBackGlassLinear(RectangleF drawRect,
                                                       RectangleF outerRect,
                                                       Color color1,
                                                       Color color2,
                                                       Color glassColor1,
                                                       Color glassColor2,
                                                       VisualOrientation orientation,
                                                       Graphics g,
                                                       float glassPercent,
                                                       IDisposable memento)
        {
            // Cannot draw a zero length rectangle
            if ((drawRect.Width > 0) && (drawRect.Height > 0) &&
                (outerRect.Width > 0) && (outerRect.Height > 0))
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
                            glassRect = new RectangleF(drawRect.X, drawRect.Y, glassLength, drawRect.Height);
                            mainRect = new RectangleF(glassRect.Right + 1, drawRect.Y, drawRect.Width - glassRect.Width - 1, drawRect.Height);
                            break;
                        case VisualOrientation.Right:
                            mainRect = new RectangleF(drawRect.X, drawRect.Y, drawRect.Width - glassLength, drawRect.Height);
                            glassRect = new RectangleF(mainRect.Right, drawRect.Y, glassLength, drawRect.Height);
                            break;
                        case VisualOrientation.Top:
                        default:
                            glassRect = new RectangleF(drawRect.X, drawRect.Y, drawRect.Width, glassLength);
                            mainRect = new RectangleF(drawRect.X, glassRect.Bottom + 1, drawRect.Width, drawRect.Height - glassRect.Height - 1);
                            break;
                        case VisualOrientation.Bottom:
                            mainRect = new RectangleF(drawRect.X, drawRect.Y, drawRect.Width, drawRect.Height - glassLength);
                            glassRect = new RectangleF(drawRect.X, mainRect.Bottom, drawRect.Width, glassLength);
                            break;
                    }

                    cache.totalBrush = new SolidBrush(color1);
                    cache.glassRect = glassRect;
                    cache.mainRect = mainRect;

                    // Create gradient rectangles
                    RectangleF glassGradientRect = new(cache.glassRect.X - 1, cache.glassRect.Y - 1, cache.glassRect.Width + 2, cache.glassRect.Height + 2);
                    RectangleF mainGradientRect = new(cache.mainRect.X - 1, cache.mainRect.Y - 1, cache.mainRect.Width + 2, cache.mainRect.Height + 2);

                    // Cannot draw a zero length rectangle
                    if ((cache.glassRect.Width > 0) && (cache.glassRect.Height > 0) &&
                        (cache.mainRect.Width > 0) && (cache.mainRect.Height > 0) &&
                        (glassGradientRect.Width > 0) && (glassGradientRect.Height > 0) &&
                        (mainGradientRect.Width > 0) && (mainGradientRect.Height > 0))
                    {
                        cache.topBrush = new LinearGradientBrush(glassGradientRect, glassColor1, glassColor2, AngleFromOrientation(orientation));
                        cache.bottomBrush = new LinearGradientBrush(mainGradientRect, color1, color2, AngleFromOrientation(orientation));
                    }
                }

                // Draw entire area in a solid color
                g.FillRectangle(cache.totalBrush, drawRect);

                if ((cache.topBrush != null) && (cache.bottomBrush != null))
                {
                    g.FillRectangle(cache.topBrush, cache.glassRect);
                    g.FillRectangle(cache.bottomBrush, cache.mainRect);
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
                                                     Graphics g,
                                                     float glassPercent,
                                                     ref IDisposable memento)
        {
            // Cannot draw a zero length rectangle
            if ((drawRect.Width > 0) && (drawRect.Height > 0))
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
                    cache.totalBrush = new SolidBrush(color1);

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
                        VisualOrientation.Left => new RectangleF(drawRect.X, drawRect.Y, length, drawRect.Height),
                        VisualOrientation.Right => new RectangleF(drawRect.Right - length, drawRect.Y, length, drawRect.Height),
                        VisualOrientation.Bottom => new RectangleF(drawRect.X, drawRect.Bottom - length, drawRect.Width, length),
                        _ => new RectangleF(drawRect.X, drawRect.Y, drawRect.Width, length)
                    };

                    // Gradient rectangle is always a little bigger to prevent tiling at edges
                    RectangleF glassGradientRect = new(glassRect.X - 1, glassRect.Y - 1, glassRect.Width + 2, glassRect.Height + 2);

                    // Cannot draw a zero length rectangle
                    if ((glassGradientRect.Width > 0) && (glassGradientRect.Height > 0))
                    {
                        cache.glassBrush = new LinearGradientBrush(glassGradientRect, glassColor1, glassColor2, AngleFromOrientation(orientation));
                        cache.glassRect = glassRect;
                    }
                }

                g.FillRectangle(cache.totalBrush, drawRect);

                if (cache.glassBrush != null)
                {
                    g.FillRectangle(cache.glassBrush, cache.glassRect);
                    return cache.glassRect;
                }
            }

            return RectangleF.Empty;
        }

        private static IDisposable DrawBackLinear(RectangleF drawRect,
                                                  bool sigma,
                                                  Color color1,
                                                  Color color2,
                                                  VisualOrientation orientation,
                                                  Graphics g,
                                                  IDisposable memento)
        {
            // Cannot draw a zero length rectangle
            if ((drawRect.Width > 0) && (drawRect.Height > 0))
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
                    RectangleF gradientRect = new(drawRect.X - 1, drawRect.Y - 1, drawRect.Width + 2, drawRect.Height + 2);

                    // Cannot draw a zero length rectangle
                    if ((gradientRect.Width > 0) && (gradientRect.Height > 0))
                    {
                        // Draw entire area in a gradient color effect
                        cache.entireBrush = new LinearGradientBrush(gradientRect, color1, color2, AngleFromOrientation(orientation));

                        if (sigma)
                        {
                            cache.entireBrush.SetSigmaBellShape(0.5f);
                        }
                    }
                }

                if (cache.entireBrush != null)
                {
                    g.FillRectangle(cache.entireBrush, drawRect);
                }
            }

            return memento;
        }

        private static IDisposable DrawBackDarkEdge(RectangleF drawRect,
                                                    Color color1,
                                                    int thickness,
                                                    VisualOrientation orientation,
                                                    Graphics g,
                                                    IDisposable memento)
        {
            // Cannot draw a zero length rectangle
            if ((drawRect.Width > 0) && (drawRect.Height > 0))
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
                        RectangleF gradientRect = new(drawRect.X - 0.5f, drawRect.Y - 0.5f, drawRect.Width + 1, drawRect.Height + 1);

                        // Cannot draw a zero length rectangle
                        if ((gradientRect.Width > 0) && (gradientRect.Height > 0))
                        {
                            // Draw entire area in a gradient color effect
                            cache.entireBrush = new LinearGradientBrush(gradientRect, Color.FromArgb(64, color1), Color.Transparent, AngleFromOrientation(orientation));
                            cache.entireBrush.SetSigmaBellShape(1.0f);
                            cache.entireRect = drawRect;
                        }
                    }
                }

                if (cache.entireBrush != null)
                {
                    g.FillRectangle(cache.entireBrush, cache.entireRect);
                }
            }

            return memento;
        }

        private static bool VerticalOrientation(VisualOrientation orientation) =>
            (orientation == VisualOrientation.Top) ||
            (orientation == VisualOrientation.Bottom);

        private static float AngleFromOrientation(VisualOrientation orientation)
        {
            return orientation switch
            {
                VisualOrientation.Bottom => 270f,
                VisualOrientation.Left => 0f,
                VisualOrientation.Right => 180,
                VisualOrientation.Top => 90f,
                _ => 90f
            };
        }

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
}
