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
/// Extends the ViewDrawDocker for use in the KryptonGroupBox.
/// </summary>
public class ViewDrawGroupBoxDocker : ViewDrawDocker
{
    #region Instance Fields

    private Rectangle _cacheClientRect;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawGroupBoxDocker class.
    /// </summary>
    /// <param name="paletteBack">Palette source for the background.</param>        
    /// <param name="paletteBorder">Palette source for the border.</param>
    public ViewDrawGroupBoxDocker(IPaletteBack paletteBack,
        IPaletteBorder paletteBorder)
        : base(paletteBack, paletteBorder) =>
        CaptionOverlap = 0.5;

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawGroupBoxDocker:{Id}";

    #endregion

    #region CaptionOverlap
    /// <summary>
    /// Gets and the sets the percentage of overlap for the caption and group area.
    /// </summary>
    public double CaptionOverlap { get; set; }

    #endregion

    #region DrawBorderAfter
    /// <summary>
    /// Gets the drawing of the border before or after children.
    /// </summary>
    public override bool DrawBorderLast => false;

    #endregion

    #region Eval
    /// <summary>
    /// Evaluate the need for drawing transparent areas.
    /// </summary>
    /// <param name="context">Evaluation context.</param>
    /// <returns>True if transparent areas exist; otherwise false.</returns>
    public override bool EvalTransparentPaint(ViewContext context) => true;

    #endregion

    #region Paint

    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void RenderBefore([DisallowNull] RenderContext context)
    {
        Debug.Assert(context != null);

        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (this[0]!.Visible)
        {
            // The first and only child is the caption content
            var caption = this[0] as ViewDrawContent;

            // Cache the original client rectangle before we modify it
            _cacheClientRect = ClientRectangle;

            // Update canvas drawing area by the overlapping caption area
            var captionRect = caption!.ClientRectangle;
            switch (GetDock(caption))
            {
                case ViewDockStyle.Top:
                    if (captionRect.Height > 0)
                    {
                        var reduce = (int)(captionRect.Height * CaptionOverlap);
                        ClientRectangle = _cacheClientRect with { Y = _cacheClientRect.Y + reduce, Height = _cacheClientRect.Height - reduce };
                    }
                    break;
                case ViewDockStyle.Left:
                    if (captionRect.Width > 0)
                    {
                        var reduce = (int)(captionRect.Width * CaptionOverlap);
                        ClientRectangle = _cacheClientRect with { X = _cacheClientRect.X + reduce, Width = _cacheClientRect.Width - reduce };
                    }
                    break;
                case ViewDockStyle.Bottom:
                    if (captionRect.Height > 0)
                    {
                        var reduce = (int)(captionRect.Height * CaptionOverlap);
                        ClientRectangle = _cacheClientRect with { Height = _cacheClientRect.Height - reduce };
                    }
                    break;
                case ViewDockStyle.Right:
                    if (captionRect.Width > 0)
                    {
                        var reduce = (int)(captionRect.Width * CaptionOverlap);
                        ClientRectangle = _cacheClientRect with { Width = _cacheClientRect.Width - reduce };
                    }
                    break;
            }
        }

        base.RenderBefore(context);
    }

    /// <summary>
    /// Perform rendering after child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void RenderAfter([DisallowNull] RenderContext context)
    {
        Debug.Assert(context != null);

        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        base.RenderAfter(context);

        // Restore original client rectangle
        if (this[0]!.Visible)
        {
            ClientRectangle = _cacheClientRect;
        }
    }

    /// <summary>
    /// Draw the canvas border.
    /// </summary>
    /// <param name="context"></param>
    public override void RenderBorder(RenderContext context)
    {
        // The first and only child is the caption content
        var caption = this[0] as ViewDrawContent;

        // Remember current clipping region so we can put it back later
        var clipRegion = context.Graphics.Clip.Clone();

        // Exclude the image/short/long text rectangles from being drawn
        var excludeRegion = clipRegion.Clone();
        var imageRect = caption!.ImageRectangle(context);
        var shortRect = caption.ShortTextRect(context);
        var longRect = caption.LongTextRect(context);
        imageRect.Inflate(1, 1);
        shortRect.Inflate(1, 1);
        longRect.Inflate(1, 1);
        excludeRegion.Exclude(imageRect);
        excludeRegion.Exclude(shortRect);
        excludeRegion.Exclude(longRect);

        // Draw border and restore original clipping region
        context.Graphics.Clip = excludeRegion;
        base.RenderBorder(context);
        context.Graphics.Clip = clipRegion;
    }
    #endregion
}