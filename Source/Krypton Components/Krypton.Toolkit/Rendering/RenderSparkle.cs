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
    /// Extends the professional renderer to provide Sparkle style additions.
    /// </summary>
    public class RenderSparkle : RenderProfessional
    {
        #region Static Fields
        private static readonly Blend _ribbonGroup5Blend;
        private static readonly Blend _ribbonGroup6Blend;
        private static readonly Blend _ribbonGroup7Blend;
        #endregion

        #region Identity
        static RenderSparkle()
        {
            _ribbonGroup5Blend = new Blend
            {
                Factors = new[] { 0.0f, 0.0f, 1.0f },
                Positions = new[] { 0.0f, 0.5f, 1.0f }
            };

            _ribbonGroup6Blend = new Blend
            {
                Factors = new[] { 0.0f, 0.0f, 0.75f, 1.0f },
                Positions = new[] { 0.0f, 0.1f, 0.45f, 1.0f }
            };

            _ribbonGroup7Blend = new Blend
            {
                Factors = new[] { 0.0f, 1.0f, 1.0f, 0.0f },
                Positions = new[] { 0.0f, 0.15f, 0.85f, 1.0f }
            };
        }
        #endregion

        #region RenderRibbon Overrides
        /// <summary>
        /// Draw the background of a ribbon element.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Target rectangle.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="palette">Palette used for sourcing settings.</param>
        /// <param name="orientation">Orientation for drawing.</param>
        /// <param name="composition">Drawing onto a composition element.</param>
        /// <param name="memento">Cached values to use when drawing.</param>
        public override IDisposable DrawRibbonBack(PaletteRibbonShape shape,
                                                   RenderContext context,
                                                   Rectangle rect,
                                                   PaletteState state,
                                                   IPaletteRibbonBack palette,
                                                   VisualOrientation orientation,
                                                   bool composition,
                                                   IDisposable memento)
        {
            // Note is the incoming state is detailed we are drawing inside a popup
            var showingInPopup = (state & PaletteState.FocusOverride) == PaletteState.FocusOverride;
            if (showingInPopup)
            {
                state &= ~PaletteState.FocusOverride;
            }

            return palette.GetRibbonBackColorStyle(state) switch
            {
                PaletteRibbonColorStyle.RibbonGroupNormalBorderTracking => DrawRibbonGroupNormalBorder(context, rect, state, palette, true, false, memento),
                PaletteRibbonColorStyle.RibbonGroupAreaBorder => DrawRibbonGroupAreaBorder1And2(context, rect, state, palette, false, true, memento),
                PaletteRibbonColorStyle.RibbonGroupAreaBorder2 => DrawRibbonGroupAreaBorder1And2(context, rect, state, palette, true, true, memento),
                _ => base.DrawRibbonBack(shape, context, rect, state, palette, orientation, composition, memento)
            };
        }

        /// <summary>
        /// Draw a context ribbon tab title.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Target rectangle.</param>
        /// <param name="paletteGeneral">Palette used for general ribbon settings.</param>
        /// <param name="paletteBack">Palette used for background ribbon settings.</param>
        /// <param name="memento">Cached storage for drawing objects.</param>
        public override IDisposable DrawRibbonTabContextTitle(PaletteRibbonShape shape,
                                                              RenderContext context,
                                                              Rectangle rect,
                                                              IPaletteRibbonGeneral paletteGeneral,
                                                              IPaletteRibbonBack paletteBack,
                                                              IDisposable memento) =>
            DrawRibbonTabContext(context, rect, paletteGeneral, paletteBack, memento);

        /// <summary>
        /// Draw the application button.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Target rectangle.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="palette">Palette used for sourcing settings.</param>
        /// <param name="memento">Cached storage for drawing objects.</param>
        public override IDisposable DrawRibbonApplicationButton(PaletteRibbonShape shape,
                                                                RenderContext context,
                                                                Rectangle rect,
                                                                PaletteState state,
                                                                IPaletteRibbonBack palette,
                                                                IDisposable memento) =>
            DrawRibbonAppButton(shape, context, rect, state, palette, true, memento);

        /// <summary>
        /// Perform drawing of a ribbon drop arrow glyph.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Render context.</param>
        /// <param name="displayRect">Display area available for drawing.</param>
        /// <param name="paletteGeneral">General ribbon palette details.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public override void DrawRibbonDropArrow(PaletteRibbonShape shape,
                                                 RenderContext context,
                                                 Rectangle displayRect,
                                                 IPaletteRibbonGeneral paletteGeneral,
                                                 PaletteState state)
        {
            Debug.Assert(context != null);
            Debug.Assert(paletteGeneral != null);

            // Validate parameter references
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (paletteGeneral == null)
            {
                throw new ArgumentNullException(nameof(paletteGeneral));
            }

            Color darkColor = state == PaletteState.Disabled ? paletteGeneral.GetRibbonDisabledDark(state) :
                                                                paletteGeneral.GetRibbonGroupDialogDark(state);

            Color lightColor = state == PaletteState.Disabled ? paletteGeneral.GetRibbonDisabledLight(state) :
                                                                 paletteGeneral.GetRibbonGroupDialogLight(state);

            using Pen darkPen = new(darkColor),
                lightPen = new(lightColor);
            context.Graphics.DrawLine(lightPen, displayRect.Left, displayRect.Top + 1, displayRect.Left + 2, displayRect.Top + 3);
            context.Graphics.DrawLine(lightPen, displayRect.Left + 2, displayRect.Top + 3, displayRect.Left + 4, displayRect.Top + 1);
            context.Graphics.DrawLine(lightPen, displayRect.Left + 4, displayRect.Top + 1, displayRect.Left + 1, displayRect.Top + 1);
            context.Graphics.DrawLine(lightPen, displayRect.Left + 1, displayRect.Top + 1, displayRect.Left + 2, displayRect.Top + 2);
            context.Graphics.DrawLine(darkPen, displayRect.Left, displayRect.Top + 2, displayRect.Left + 2, displayRect.Top + 4);
            context.Graphics.DrawLine(darkPen, displayRect.Left + 2, displayRect.Top + 4, displayRect.Left + 4, displayRect.Top + 2);
        }
        #endregion

        #region RenderGlyph Overrides

        /// <summary>
        /// Draw a drop down grid appropriate for a input control.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="cellRect">Available drawing rectangle space.</param>
        /// <param name="paletteContent">Content palette for getting colors.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public override void DrawInputControlDropDownGlyph(RenderContext context,
                                                           Rectangle cellRect,
                                                           IPaletteContent paletteContent,
                                                           PaletteState state)
        {
            Debug.Assert(context != null);
            Debug.Assert(paletteContent != null);

            // Validate parameter references
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (paletteContent == null)
            {
                throw new ArgumentNullException(nameof(paletteContent));
            }

            Color c1 = paletteContent.GetContentShortTextColor1(state);

            // Find the top left starting position for drawing lines
            var xStart = cellRect.Left + ((cellRect.Right - cellRect.Left - 4) / 2);
            var yStart = cellRect.Top + ((cellRect.Bottom - cellRect.Top - 3) / 2);

            using Pen darkPen = new(c1);
            context.Graphics.DrawLine(darkPen, xStart, yStart, xStart + 4, yStart);
            context.Graphics.DrawLine(darkPen, xStart + 1, yStart + 1, xStart + 3, yStart + 1);
            context.Graphics.DrawLine(darkPen, xStart + 2, yStart + 2, xStart + 2, yStart + 1);
        }

        /// <summary>
        /// Draw a numeric up button image appropriate for a input control.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="cellRect">Available drawing rectangle space.</param>
        /// <param name="paletteContent">Content palette for getting colors.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public override void DrawInputControlNumericUpGlyph(RenderContext context,
                                                            Rectangle cellRect,
                                                            IPaletteContent paletteContent,
                                                            PaletteState state)
        {
            Debug.Assert(context != null);
            Debug.Assert(paletteContent != null);

            // Validate parameter references
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (paletteContent == null)
            {
                throw new ArgumentNullException(nameof(paletteContent));
            }

            Color c1 = paletteContent.GetContentShortTextColor1(state);

            // Find the top left starting position for drawing lines
            var xStart = cellRect.Left + ((cellRect.Right - cellRect.Left - 4) / 2);
            var yStart = cellRect.Top + ((cellRect.Bottom - cellRect.Top - 3) / 2);

            using Pen darkPen = new(c1);
            context.Graphics.DrawLine(darkPen, xStart, yStart + 3, xStart + 4, yStart + 3);
            context.Graphics.DrawLine(darkPen, xStart + 1, yStart + 2, xStart + 3, yStart + 2);
            context.Graphics.DrawLine(darkPen, xStart + 2, yStart + 2, xStart + 2, yStart + 1);
        }

        /// <summary>
        /// Draw a numeric down button image appropriate for a input control.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="cellRect">Available drawing rectangle space.</param>
        /// <param name="paletteContent">Content palette for getting colors.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public override void DrawInputControlNumericDownGlyph(RenderContext context,
                                                              Rectangle cellRect,
                                                              IPaletteContent paletteContent,
                                                              PaletteState state)
        {
            Debug.Assert(context != null);
            Debug.Assert(paletteContent != null);

            // Validate parameter references
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (paletteContent == null)
            {
                throw new ArgumentNullException(nameof(paletteContent));
            }

            Color c1 = paletteContent.GetContentShortTextColor1(state);

            // Find the top left starting position for drawing lines
            var xStart = cellRect.Left + ((cellRect.Right - cellRect.Left - 4) / 2);
            var yStart = cellRect.Top + ((cellRect.Bottom - cellRect.Top - 3) / 2);

            using Pen darkPen = new(c1);
            context.Graphics.DrawLine(darkPen, xStart, yStart, xStart + 4, yStart);
            context.Graphics.DrawLine(darkPen, xStart + 1, yStart + 1, xStart + 3, yStart + 1);
            context.Graphics.DrawLine(darkPen, xStart + 2, yStart + 2, xStart + 2, yStart + 1);
        }
        #endregion

        #region IRenderer Overrides

        /// <summary>
        /// Gets a renderer for drawing the toolstrips.
        /// </summary>
        /// <param name="colorPalette">Color palette to use when rendering toolstrip.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public override ToolStripRenderer RenderToolStrip(IPalette colorPalette)
        {
            Debug.Assert(colorPalette != null);

            // Validate incoming parameter
            if (colorPalette == null)
            {
                throw new ArgumentNullException(nameof(colorPalette));
            }

            // Use the professional renderer but pull colors from the palette
            KryptonSparkleRenderer renderer = new(colorPalette.ColorTable)
            {

                // Setup the need to use rounded corners
                RoundedEdges = colorPalette.ColorTable.UseRoundedEdges != InheritBool.False
            };

            return renderer;
        }
        #endregion

        #region Implementation
        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected override IDisposable DrawRibbonTabContext(RenderContext context,
                                                            Rectangle rect,
                                                            IPaletteRibbonGeneral paletteGeneral,
                                                            IPaletteRibbonBack paletteBack,
                                                            IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = paletteGeneral.GetRibbonTabSeparatorContextColor(PaletteState.Normal);
                Color c2 = paletteBack.GetRibbonBackColor5(PaletteState.ContextCheckedNormal);

                var generate = true;
                MementoRibbonTabContextOffice cache;

                // Access a cache instance and decide if cache resources need generating
                if (memento is MementoRibbonTabContextOffice office)
                {
                    cache = office;
                    generate = !cache.UseCachedValues(rect, c1, c2);
                }
                else
                {
                    memento?.Dispose();

                    cache = new MementoRibbonTabContextOffice(rect, c1, c2);
                    memento = cache;
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    Rectangle borderRect = new(rect.X - 1, rect.Y - 1, rect.Width + 2, rect.Height + 2);
                    cache.fillRect = new Rectangle(rect.X + 1, rect.Y, rect.Width - 2, rect.Height - 1);

                    LinearGradientBrush borderBrush = new(borderRect, c1, Color.Transparent, 270f)
                    {
                        Blend = _ribbonGroup5Blend
                    };
                    cache.borderPen = new Pen(borderBrush);

                    LinearGradientBrush underlineBrush = new(borderRect, Color.Transparent, Color.FromArgb(200, c2), 0f)
                    {
                        Blend = _ribbonGroup7Blend
                    };
                    cache.underlinePen = new Pen(underlineBrush);

                    cache.fillBrush = new LinearGradientBrush(borderRect, Color.FromArgb(106, c2), Color.Transparent, 270f)
                    {
                        Blend = _ribbonGroup6Blend
                    };
                }

                // Draw the left and right border lines
                context.Graphics.DrawLine(cache.borderPen, rect.X, rect.Y, rect.X, rect.Bottom - 1);
                context.Graphics.DrawLine(cache.borderPen, rect.Right - 1, rect.Y, rect.Right - 1, rect.Bottom - 1);

                // Fill the inner area with a gradient context specific color
                context.Graphics.FillRectangle(cache.fillBrush, cache.fillRect);

                // Overdraw the brighter line at bottom
                context.Graphics.DrawLine(cache.underlinePen, rect.X + 1, rect.Bottom - 2, rect.Right - 2, rect.Bottom - 2);
            }

            return memento;
        }
        #endregion
    }
}
