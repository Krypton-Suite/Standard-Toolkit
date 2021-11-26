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
    /// Extends the professional renderer to provide Office365 style additions.
    /// </summary>
    /// <seealso cref="RenderOffice2010" />
    public class RenderOffice365 : RenderOffice2010
    {
        #region Static Variables
        private static readonly float BORDER_PERCENT = 0.6f;

        private static readonly float WHITE_PERCENT = 0.4f;
        #endregion

        #region Constructor
        static RenderOffice365()
        {

        }
        #endregion

        #region RenderRibbon Overrides        
        /// <summary>
        /// Perform drawing of a ribbon cluster edge.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Render context.</param>
        /// <param name="displayRect">Display area available for drawing.</param>
        /// <param name="paletteBack">Palette used for recovering drawing details.</param>
        /// <param name="state">State associated with rendering.</param>
        public override void DrawRibbonClusterEdge(PaletteRibbonShape shape, RenderContext context, Rectangle displayRect, IPaletteBack paletteBack, PaletteState state)
        {
            Debug.Assert(context != null);

            Debug.Assert(paletteBack != null);

            // Get the first border color, and then lighten it by merging with white
            Color borderColour = paletteBack.GetBackColor1(state), lightColour = CommonHelper.MergeColors(borderColour, BORDER_PERCENT, Color.White, WHITE_PERCENT);

            // Draw inside of the border edge in a lighter version of the border
            using SolidBrush drawBrush = new(lightColour);
            context.Graphics.FillRectangle(drawBrush, displayRect);
        }
        #endregion

        #region IRenderer Overrides        
        /// <summary>
        /// Renders the tool strip.
        /// </summary>
        /// <param name="colourPalette">The colour palette.</param>
        /// <returns></returns>
        public override ToolStripRenderer RenderToolStrip(IPalette colourPalette)
        {
            Debug.Assert(colourPalette != null);

            // Validate passed parameter
            if (colourPalette == null)
            {
                throw new ArgumentNullException(nameof(colourPalette));
            }

            KryptonOffice365Renderer renderer = new(colourPalette.ColorTable)
            {
                RoundedEdges = colourPalette.ColorTable.UseRoundedEdges != InheritBool.False
            };

            return renderer;
        }
        #endregion

        #region Implementation

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected override IDisposable DrawRibbonTabContext(RenderContext context, Rectangle rect, IPaletteRibbonGeneral paletteGeneral, IPaletteRibbonBack paletteBack, IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = paletteGeneral.GetRibbonTabSeparatorContextColor(PaletteState.Normal);
                Color c2 = paletteBack.GetRibbonBackColor5(PaletteState.ContextCheckedNormal);

                var generate = true;
                MementoRibbonTabContextOffice2010 cache;

                // Access a cache instance and decide if cache resources need generating
                if (memento is MementoRibbonTabContextOffice2010 office2010)
                {
                    cache = office2010;
                    generate = !cache.UseCachedValues(rect, c1, c2);
                }
                else
                {
                    memento?.Dispose();

                    cache = new MementoRibbonTabContextOffice2010(rect, c1, c2);
                    memento = cache;
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    cache.borderOuterPen = new Pen(c1);
                    cache.borderInnerPen = new Pen(CommonHelper.MergeColors(Color.Black, 0.1f, c2, 0.9f));
                    cache.topBrush = new SolidBrush(c2);
                    Color lightC2 = ControlPaint.Light(c2);
                    cache.bottomBrush = new LinearGradientBrush(new RectangleF(rect.X - 1, rect.Y, rect.Width + 2, rect.Height + 1),
                                                                Color.FromArgb(128, lightC2), Color.FromArgb(64, lightC2), 90f);
                }

                // Draw the left and right borders
                context.Graphics.DrawLine(cache.borderOuterPen, rect.X, rect.Y, rect.X, rect.Bottom);
                context.Graphics.DrawLine(cache.borderInnerPen, rect.X + 1, rect.Y, rect.X + 1, rect.Bottom - 1);
                context.Graphics.DrawLine(cache.borderOuterPen, rect.Right - 1, rect.Y, rect.Right - 1, rect.Bottom - 1);
                context.Graphics.DrawLine(cache.borderInnerPen, rect.Right - 2, rect.Y, rect.Right - 2, rect.Bottom - 1);

                // Draw the solid block of colour at the top
                context.Graphics.FillRectangle(cache.topBrush, rect.X + 2, rect.Y, rect.Width - 4, 4);

                // Draw the gradient to the bottom
                context.Graphics.FillRectangle(cache.bottomBrush, rect.X + 2, rect.Y + 4, rect.Width - 4, rect.Height - 4);
            }

            return memento;
        }

        /// <summary>
        /// Draw the application tab.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Target rectangle.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="baseColor1">Base color1 used for drawing the ribbon tab.</param>
        /// <param name="baseColor2">Base color2 used for drawing the ribbon tab.</param>
        /// <param name="memento">Cached values to use when drawing.</param>
        public override IDisposable DrawRibbonApplicationTab(PaletteRibbonShape shape, RenderContext context, Rectangle rect, PaletteState state, Color baseColor1, Color baseColor2, IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                var generate = true;
                MementoRibbonAppTab2013 cache;

                // Access a cache instance and decide if cache resources need generating
                if (memento is MementoRibbonAppTab2013 tab2013)
                {
                    cache = tab2013;
                    generate = !cache.UseCachedValues(rect, baseColor1);
                }
                else
                {
                    memento?.Dispose();

                    cache = new MementoRibbonAppTab2013(rect, baseColor1);
                    memento = cache;
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    // Create common paths to all the app tab states
                    cache.GeneratePaths(rect, state);
                    //cache.borderPen = new Pen(baseColor1);

                    // Create state specific colors/brushes/pens
                    cache.insideFillBrush = state switch
                    {
                        PaletteState.Normal =>
                            //cache.borderBrush = new SolidBrush(baseColor1);
                            new SolidBrush(baseColor1),
                        PaletteState.Tracking => new SolidBrush(baseColor2),
                        PaletteState.Tracking | PaletteState.FocusOverride => new SolidBrush(
                            ControlPaint.LightLight(baseColor2)),
                        PaletteState.Pressed => new SolidBrush(baseColor2),
                        _ => cache.insideFillBrush
                    };
                }

                // Fill the entire tab area and then add a border around the edge
                //context.Graphics.FillPath(cache.borderBrush, cache.borderFillPath);

                // Draw the outside border
                //using (AntiAlias aa = new AntiAlias(context.Graphics))
                //    context.Graphics.DrawPath(cache.borderPen, cache.borderPath);

                // Fill inside area
                //context.Graphics.FillPath(cache.insideFillBrush, cache.insideFillPath);
                context.Graphics.FillRectangle(cache.insideFillBrush, cache.rect);

                // Draw highlight over bottom half
                //using (Clipping clip = new Clipping(context.Graphics, cache.insideFillPath))
                //    context.Graphics.FillPath(cache.highlightBrush, cache.highlightPath);
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected override IDisposable DrawRibbonTabSelected2010(RenderContext context, Rectangle rect, PaletteState state, IPaletteRibbonBack palette, VisualOrientation orientation, IDisposable memento, bool standard)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);
                Color c3 = palette.GetRibbonBackColor3(state);
                Color c4 = palette.GetRibbonBackColor4(state);
                Color c5 = palette.GetRibbonBackColor5(state);

                var generate = true;
                MementoRibbonTabSelected2010 cache;

                // Access a cache instance and decide if cache resources need generating
                if (memento is MementoRibbonTabSelected2010 selected2010)
                {
                    cache = selected2010;
                    generate = !cache.UseCachedValues(rect, c1, c2, c3, c4, c5, orientation);
                }
                else
                {
                    memento?.Dispose();

                    cache = new MementoRibbonTabSelected2010(rect, c1, c2, c3, c4, c5, orientation);
                    memento = cache;
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    // If we have a context color to use then modify the drawing colors
                    if (c5 != Color.Empty)
                    {
                        if (!standard)
                        {
                            c5 = CommonHelper.MergeColors(c5, 0.65f, Color.Black, 0.35f);
                        }

                        c1 = Color.FromArgb(196, c5);
                    }

                    switch (orientation)
                    {
                        case VisualOrientation.Top:
                            DrawRibbonTabSelectedTop2010(rect, c2, c3, c5, cache);
                            break;
                        case VisualOrientation.Left:
                            DrawRibbonTabSelectedLeft2010(rect, c2, c3, c5, cache);
                            break;
                        case VisualOrientation.Right:
                            DrawRibbonTabSelectedRight2010(rect, c2, c3, c5, cache);
                            break;
                        case VisualOrientation.Bottom:
                            DrawRibbonTabSelectedBottom2010(rect, c2, c3, c5, cache);
                            break;
                    }

                    cache.outsidePen = new Pen(c1);
                    cache.centerPen = new Pen(c4);
                }

                context.Graphics.FillRectangle(cache.centerBrush, cache.rect);
                //context.Graphics.FillPath(cache.centerBrush, cache.outsidePath);

                //if (c5 != Color.Empty)
                //    context.Graphics.FillPath(cache.insideBrush, cache.insidePath);

                //using (AntiAlias aa = new AntiAlias(context.Graphics))
                //    context.Graphics.DrawPath(cache.outsidePen, cache.outsidePath);
                context.Graphics.DrawRectangle(cache.outsidePen, cache.rect);

                //switch (orientation)
                //{
                //    case VisualOrientation.Top:
                //        DrawRibbonTabSelectedTopDraw2010(rect, cache, context.Graphics);
                //        break;
                //    case VisualOrientation.Left:
                //        DrawRibbonTabSelectedLeftDraw2010(rect, cache, context.Graphics);
                //        break;
                //    case VisualOrientation.Right:
                //        DrawRibbonTabSelectedRightDraw2010(rect, cache, context.Graphics);
                //        break;
                //    case VisualOrientation.Bottom:
                //        DrawRibbonTabSelectedBottomDraw2010(rect, cache, context.Graphics);
                //        break;
                //}
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected override IDisposable DrawRibbonTabTracking2010(PaletteRibbonShape shape, RenderContext context, Rectangle rect, PaletteState state, IPaletteRibbonBack palette, VisualOrientation orientation, IDisposable memento, bool standard)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);
                Color c3 = palette.GetRibbonBackColor3(state);
                Color c4 = palette.GetRibbonBackColor4(state);
                Color c5 = palette.GetRibbonBackColor5(state);

                var generate = true;
                MementoRibbonTabTracking2010 cache;

                // Access a cache instance and decide if cache resources need generating
                if (memento is MementoRibbonTabTracking2010 tracking2010)
                {
                    cache = tracking2010;
                    generate = !cache.UseCachedValues(rect, c1, c2, c3, c4, orientation);
                }
                else
                {
                    memento?.Dispose();

                    cache = new MementoRibbonTabTracking2010(rect, c1, c2, c3, c4, orientation);
                    memento = cache;
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    // If c5 has a colour then use that to highlight the tab
                    if (c5 != Color.Empty)
                    {
                        if (!standard)
                        {
                            c5 = CommonHelper.MergeColors(c5, 0.65f, Color.Black, 0.35f);
                        }

                        c1 = c5;
                        c2 = CommonHelper.MergeColors(c2, 0.8f, ControlPaint.Light(c5), 0.2f);
                        c3 = CommonHelper.MergeColors(c3, 0.7f, c5, 0.3f);
                    }

                    switch (orientation)
                    {
                        case VisualOrientation.Top:
                            DrawRibbonTabTrackingTop2010(rect, c3, c4, cache);
                            break;
                        case VisualOrientation.Left:
                            DrawRibbonTabTrackingLeft2010(rect, c3, c4, cache);
                            break;
                        case VisualOrientation.Right:
                            DrawRibbonTabTrackingRight2010(rect, c3, c4, cache);
                            break;
                        case VisualOrientation.Bottom:
                            DrawRibbonTabTrackingBottom2010(rect, c3, c4, cache);
                            break;
                    }

                    cache.outsidePen = new Pen(c1);
                    cache.outsideBrush = new SolidBrush(c2);
                }

                // Fill the full background
                //context.Graphics.FillPath(cache.outsideBrush, cache.outsidePath);
                context.Graphics.FillRectangle(cache.outsideBrush, cache.rect);

                // Draw the border
                //using (AntiAlias aa = new AntiAlias(context.Graphics))
                //    context.Graphics.DrawPath(cache.outsidePen, cache.borderPath);
                context.Graphics.DrawRectangle(cache.outsidePen, cache.rect);

                // Fill the inside area
                //context.Graphics.FillPath(cache.insideBrush, cache.insidePath);
            }

            return memento;
        }
    }
    #endregion
}