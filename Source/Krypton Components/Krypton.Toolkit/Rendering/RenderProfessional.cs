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
/// Extends the standard renderer to provide Professional style additions.
/// </summary>
public class RenderProfessional : RenderStandard
{
    #region Static Fields

    private const int GRAB_SQUARE_LENGTH = 2;
    private const int GRAB_SQUARE_OFFSET = 1;
    private const int GRAB_SQUARE_TOTAL = 3;
    private const int GRAB_SQUARE_GAP = 1;
    private const int GRAB_SQUARE_MIN_SPACE = 5;
    private const int GRAB_SQUARE_COUNT = 5;
    private static readonly Color _grabHandleLight = Color.FromArgb(228, 255, 255, 255);
    private static readonly Color _grabHandleDark = Color.FromArgb(144, 0, 0, 0);
    #endregion

    #region RenderGlyph Overrides
    /// <summary>
    /// Perform drawing of a separator glyph.
    /// </summary>
    /// <param name="context">Render context.</param>
    /// <param name="displayRect">Display area available for drawing.</param>
    /// <param name="paletteBack">Background palette details.</param>
    /// <param name="paletteBorder">Border palette details.</param>
    /// <param name="orientation">Visual orientation of the content.</param>
    /// <param name="state">State associated with rendering.</param>
    /// <param name="canMove">Can the separator be moved.</param>
    public override void DrawSeparator(RenderContext context,
        Rectangle displayRect,
        IPaletteBack paletteBack,
        IPaletteBorder paletteBorder,
        Orientation orientation,
        PaletteState state,
        bool canMove)
    {
        // Let base class perform standard processing
        base.DrawSeparator(context,
            displayRect,
            paletteBack,
            paletteBorder,
            orientation,
            state,
            canMove);

        // If we are drawing the background then draw grab handles on top
        if (paletteBack.GetBackDraw(state) == InheritBool.True)
        {
            // Only draw grab handle if the user can move the separator
            if (canMove)
            {
                DrawGrabHandleGlyph(context, displayRect, orientation, state);
            }
        }
    }
    #endregion

    #region Implementation
    /// <summary>
    /// Internal rendering method.
    /// </summary>
    protected virtual void DrawGrabHandleGlyph(RenderContext context,
        Rectangle displayRect,
        Orientation orientation,
        PaletteState state)
    {
        // Is there enough room to draw the at least one grab handle?
        if (displayRect is { Height: >= GRAB_SQUARE_MIN_SPACE, Width: >= GRAB_SQUARE_MIN_SPACE })
        {
            // Reduce rectangle to remove the border around the display area edges
            displayRect.Inflate(-GRAB_SQUARE_GAP, -GRAB_SQUARE_GAP);

            // Find how much space is available for drawing grab handles in the orientation
            var orientationSpace = orientation == Orientation.Horizontal ? displayRect.Width : displayRect.Height;

            // Try to display the maximum allowed number of handles, but show less if not possible
            for (var i = GRAB_SQUARE_COUNT; i > 0; i--)
            {
                // Calculate how much space this number of grab handles takes up
                var requiredSpace = (i * GRAB_SQUARE_TOTAL) + (i > 1 ? (i - 1) * GRAB_SQUARE_GAP : 0);

                // Is there enough space all the grab handles?
                if (requiredSpace <= orientationSpace)
                {
                    // Find offset before showing the first handle
                    var offset = (orientationSpace - requiredSpace) / 2;

                    // Find location of first handle
                    Point draw = orientation == Orientation.Horizontal
                        ? new Point(displayRect.X + offset,
                            displayRect.Y + ((displayRect.Height - GRAB_SQUARE_TOTAL) / 2))
                        : new Point(displayRect.X + ((displayRect.Width - GRAB_SQUARE_TOTAL) / 2),
                            displayRect.Y + offset);

                    using Brush lightBrush = new SolidBrush(_grabHandleLight),
                        darkBrush = new SolidBrush(_grabHandleDark);
                    // Draw each grab handle in turn
                    for (var j = 0; j < i; j++)
                    {
                        // Draw the light colored square 
                        context.Graphics.FillRectangle(lightBrush,
                            draw.X + GRAB_SQUARE_OFFSET,
                            draw.Y + GRAB_SQUARE_OFFSET,
                            GRAB_SQUARE_LENGTH,
                            GRAB_SQUARE_LENGTH);

                        // Draw the dark colored square overlapping the dark
                        context.Graphics.FillRectangle(darkBrush,
                            draw.X,
                            draw.Y,
                            GRAB_SQUARE_LENGTH,
                            GRAB_SQUARE_LENGTH);

                        // Move to the next handle position
                        if (orientation == Orientation.Horizontal)
                        {
                            draw.X += GRAB_SQUARE_TOTAL + GRAB_SQUARE_GAP;
                        }
                        else
                        {
                            draw.Y += GRAB_SQUARE_TOTAL + GRAB_SQUARE_GAP;
                        }
                    }

                    // Finished
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Internal rendering method.
    /// </summary>
    protected override IDisposable? DrawRibbonTabContext(RenderContext context,
        Rectangle rect,
        IPaletteRibbonGeneral paletteGeneral,
        IPaletteRibbonBack paletteBack,
        IDisposable? memento)
    {
        if (rect is { Width: > 0, Height: > 0 })
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

                cache.BorderOuterPen = new Pen(c1);
                cache.BorderInnerPen = new Pen(CommonHelper.MergeColors(Color.Black, 0.1f, c2, 0.9f));
                cache.TopBrush = new SolidBrush(c2);
                Color lightC2 = ControlPaint.Light(c2);
                cache.BottomBrush = new LinearGradientBrush(new RectangleF(rect.X - 1, rect.Y, rect.Width + 2, rect.Height + 1),
                    Color.FromArgb(128, lightC2), Color.FromArgb(64, lightC2), 90f);
            }

            // Draw the left and right borders
            context.Graphics.DrawLine(cache.BorderOuterPen!, rect.X, rect.Y, rect.X, rect.Bottom);
            context.Graphics.DrawLine(cache.BorderInnerPen!, rect.X + 1, rect.Y, rect.X + 1, rect.Bottom - 1);
            context.Graphics.DrawLine(cache.BorderOuterPen!, rect.Right - 1, rect.Y, rect.Right - 1, rect.Bottom - 1);
            context.Graphics.DrawLine(cache.BorderInnerPen!, rect.Right - 2, rect.Y, rect.Right - 2, rect.Bottom - 1);
            
            // Draw the solid block of colour at the top
            context.Graphics.FillRectangle(cache.TopBrush!, rect.X + 2, rect.Y, rect.Width - 4, 4);

            // Draw the gradient to the bottom
            context.Graphics.FillRectangle(cache.BottomBrush!, rect.X + 2, rect.Y + 4, rect.Width - 4, rect.Height - 4);
        }

        return memento;
    }
    #endregion
}