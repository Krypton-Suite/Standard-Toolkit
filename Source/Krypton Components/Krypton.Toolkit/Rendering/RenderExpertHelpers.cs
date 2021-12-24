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
                Positions = new[] { 0.0f, 0.1f, 1.0f },
                Factors = new[] { 0.0f, 1.0f, 1.0f }
            };

            _rounded2Blend = new Blend
            {
                Positions = new[] { 0.0f, 0.50f, 0.75f, 1.0f },
                Factors = new[] { 0.0f, 1.0f, 1.0f, 1.0f }
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
        public static IDisposable DrawBackExpertTracking(RenderContext context,
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

            cache.first = DrawBackExpert(rect, 
                CommonHelper.MergeColors(backColor1, 0.35f, Color.White, 0.65f),
                CommonHelper.MergeColors(backColor2, 0.53f, Color.White, 0.65f), 
                orientation, context.Graphics, memento, true, true);
                
            cache.second = DrawBackExpert(rect, backColor1, backColor2, orientation, context.Graphics, memento, false, true);

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
        public static IDisposable DrawBackExpertPressed(RenderContext context,
                                                        Rectangle rect,
                                                        Color backColor1,
                                                        Color backColor2,
                                                        VisualOrientation orientation,
                                                        GraphicsPath path,
                                                        IDisposable memento)
        {
            using Clipping clip = new(context.Graphics, path);
            // Cannot draw a zero length rectangle
            if ((rect.Width > 0) && (rect.Height > 0))
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
                    cache.path1 = CreateBorderPath(rect, ITEM_CUT);
                    cache.path2 = CreateBorderPath(new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2), ITEM_CUT);
                    cache.path3 = CreateBorderPath(new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - 4, rect.Height - 4), ITEM_CUT);
                    cache.brush1 = new SolidBrush(CommonHelper.MergeColors(backColor2, 0.4f, backColor1, 0.6f));
                    cache.brush2 = new SolidBrush(CommonHelper.MergeColors(backColor2, 0.2f, backColor1, 0.8f));
                    cache.brush3 = new SolidBrush(backColor1);
                }

                using AntiAlias aa = new(context.Graphics);
                context.Graphics.FillRectangle(cache.brush3, rect);
                context.Graphics.FillPath(cache.brush1, cache.path1);
                context.Graphics.FillPath(cache.brush2, cache.path2);
                context.Graphics.FillPath(cache.brush3, cache.path3);
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
        public static IDisposable DrawBackExpertChecked(RenderContext context,
                                                        Rectangle rect,
                                                        Color backColor1,
                                                        Color backColor2,
                                                        VisualOrientation orientation,
                                                        GraphicsPath path,
                                                        IDisposable memento)
        {
            using Clipping clip = new(context.Graphics, path);
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
        public static IDisposable DrawBackExpertCheckedTracking(RenderContext context,
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

            cache.first = DrawBackExpert(rect,
                CommonHelper.MergeColors(backColor1, 0.5f, Color.White, 0.5f),
                CommonHelper.MergeColors(backColor2, 0.5f, Color.White, 0.5f),
                orientation, context.Graphics, memento, true, false);

            cache.second = DrawBackExpert(rect, backColor1, backColor2, orientation, context.Graphics, memento, false, false);

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
        public static IDisposable DrawBackExpertSquareHighlight(RenderContext context,
                                                                Rectangle rect,
                                                                Color backColor1,
                                                                Color backColor2,
                                                                VisualOrientation orientation,
                                                                GraphicsPath path,
                                                                IDisposable memento,
                                                                bool light)
        {
            using Clipping clip = new(context.Graphics, path);
            // Cannot draw a zero length rectangle
            if ((rect.Width > 0) && (rect.Height > 0))
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

                    cache.backBrush = new SolidBrush(CommonHelper.WhitenColor(backColor1, 0.8f, 0.8f, 0.8f));
                    cache.innerRect = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);

                    RectangleF ellipseRect;
                    PointF ellipseCenter;
                    var ellipseWidth = Math.Max(1, rect.Width / 8);
                    var ellipseHeight = Math.Max(1, rect.Height / 8);

                    switch (orientation)
                    {
                        default:
                        case VisualOrientation.Top:
                            cache.innerBrush = new LinearGradientBrush(cache.innerRect, backColor1, backColor2, 90f);
                            ellipseRect = new RectangleF(rect.Left, rect.Top + (ellipseHeight * 2), rect.Width, ellipseHeight * 12);
                            ellipseCenter = new PointF(ellipseRect.Left + (ellipseRect.Width / 2), ellipseRect.Bottom);
                            break;
                        case VisualOrientation.Bottom:
                            cache.innerBrush = new LinearGradientBrush(cache.innerRect, backColor1, backColor2, 270f);
                            ellipseRect = new RectangleF(rect.Left, rect.Top - (ellipseHeight * 6), rect.Width, ellipseHeight * 12);
                            ellipseCenter = new PointF(ellipseRect.Left + (ellipseRect.Width / 2), ellipseRect.Top);
                            break;
                        case VisualOrientation.Left:
                            cache.innerBrush = new LinearGradientBrush(cache.innerRect, backColor1, backColor2, 180f);
                            ellipseRect = new RectangleF(rect.Left + (ellipseHeight * 2), rect.Top, ellipseWidth * 12, rect.Height);
                            ellipseCenter = new PointF(ellipseRect.Right, ellipseRect.Top + (ellipseRect.Height / 2));
                            break;
                        case VisualOrientation.Right:
                            cache.innerBrush = new LinearGradientBrush(rect, backColor1, backColor2, 0f);
                            ellipseRect = new RectangleF(rect.Left - (ellipseHeight * 6), rect.Top, ellipseWidth * 12, rect.Height);
                            ellipseCenter = new PointF(ellipseRect.Left, ellipseRect.Top + (ellipseRect.Height / 2));
                            break;
                    }

                    cache.innerBrush.SetSigmaBellShape(0.5f);
                    cache.ellipsePath = new GraphicsPath();
                    cache.ellipsePath.AddEllipse(ellipseRect);
                    cache.insideLighten = new PathGradientBrush(cache.ellipsePath)
                    {
                        CenterPoint = ellipseCenter,
                        CenterColor = light ? Color.FromArgb(64, Color.White) : Color.FromArgb(128, Color.White),
                        Blend = _rounded2Blend,
                        SurroundColors = new[] { Color.Transparent }
                    };
                }

                context.Graphics.FillRectangle(cache.backBrush, rect);
                context.Graphics.FillRectangle(cache.innerBrush, cache.innerRect);
                context.Graphics.FillRectangle(cache.insideLighten, cache.innerRect);
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
            if ((drawRect.Width > 0) && (drawRect.Height > 0))
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
                    memento?.Dispose();

                    cache = new MementoBackSolid(drawRect, color1);
                    memento = cache;
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();
                    cache.solidBrush = new SolidBrush(color1);
                }

                if (cache.solidBrush != null)
                {
                    g.FillRectangle(cache.solidBrush, drawRect);
                }
            }

            return memento;
        }

        private static IDisposable DrawBackExpert(Rectangle drawRect,
                                                  Color color1,
                                                  Color color2,
                                                  VisualOrientation orientation,
                                                  Graphics g,
                                                  IDisposable memento,
                                                  bool total,
                                                  bool tracking)
        {
            // Cannot draw a zero length rectangle
            if ((drawRect.Width > 0) && (drawRect.Height > 0))
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

                        cache.drawRect = drawRect;
                        cache.clipPath = new GraphicsPath();
                        cache.clipPath.AddLine(drawRect.X + 1, drawRect.Y, drawRect.Right - 1, drawRect.Y);
                        cache.clipPath.AddLine(drawRect.Right - 1, drawRect.Y, drawRect.Right, drawRect.Y + 1);
                        cache.clipPath.AddLine(drawRect.Right, drawRect.Y + 1, drawRect.Right, drawRect.Bottom - 2);
                        cache.clipPath.AddLine(drawRect.Right, drawRect.Bottom - 2, drawRect.Right - 2, drawRect.Bottom);
                        cache.clipPath.AddLine(drawRect.Right - 2, drawRect.Bottom, drawRect.Left + 1, drawRect.Bottom);
                        cache.clipPath.AddLine(drawRect.Left + 1, drawRect.Bottom, drawRect.Left, drawRect.Bottom - 2);
                        cache.clipPath.AddLine(drawRect.Left, drawRect.Bottom - 2, drawRect.Left, drawRect.Y + 1);
                        cache.clipPath.AddLine(drawRect.Left, drawRect.Y + 1, drawRect.X + 1, drawRect.Y);
                    }
                    else
                    {
                        cache.clipPath = new GraphicsPath();
                        cache.clipPath.AddRectangle(drawRect);
                    }

                    // Create rectangle that covers the enter area
                    RectangleF gradientRect = new(drawRect.X - 1, drawRect.Y - 1, drawRect.Width + 2, drawRect.Height + 2);

                    // Cannot draw a zero length rectangle
                    if ((gradientRect.Width > 0) && (gradientRect.Height > 0))
                    {
                        // Draw entire area in a gradient color effect
                        cache.entireBrush = new LinearGradientBrush(gradientRect, CommonHelper.WhitenColor(color1, 0.92f, 0.92f, 0.92f), color1, AngleFromOrientation(orientation))
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

                    cache.ellipsePath = new GraphicsPath();
                    cache.ellipsePath.AddEllipse(ellipseRect);
                    cache.insideLighten = new PathGradientBrush(cache.ellipsePath)
                    {
                        CenterPoint = ellipseCenter,
                        CenterColor = color2,
                        Blend = _rounded2Blend,
                        SurroundColors = new[] { Color.Transparent }
                    };
                }

                if (cache.entireBrush != null)
                {
                    using Clipping clip = new(g, cache.clipPath);
                    g.FillRectangle(cache.entireBrush, cache.drawRect);
                    g.FillPath(cache.insideLighten, cache.ellipsePath);
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
            GraphicsPath path = new();
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

        private static float AngleFromOrientation(VisualOrientation orientation)
        {
            return orientation switch
            {
                VisualOrientation.Bottom => 270f,
                VisualOrientation.Left => 0f,
                VisualOrientation.Right => 180,
                _ => 90f
            };
        }
        #endregion
    }
}
