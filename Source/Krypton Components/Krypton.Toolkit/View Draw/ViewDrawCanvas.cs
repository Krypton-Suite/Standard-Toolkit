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
/// View element that applies padding to the drawing of a border and background.
/// </summary>
public class ViewDrawCanvas : ViewComposite
{
    #region Instance Fields
    internal IPaletteBack? _paletteBack;
    internal IPaletteBorder? _paletteBorder;
    internal IPaletteMetric? _paletteMetric;
    internal PaletteMetricPadding _metricPadding;
    private IDisposable? _mementoBack;
    private PaletteBorderInheritForced? _borderForced;
    private Region _clipRegion;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawCanvas class.
    /// </summary>
    /// <param name="paletteBack">Palette source for the background.</param>        
    /// <param name="paletteBorder">Palette source for the border.</param>
    /// <param name="orientation">Visual orientation of the content.</param>
    public ViewDrawCanvas(IPaletteBack paletteBack,
        IPaletteBorder paletteBorder,
        VisualOrientation orientation)
        : this(paletteBack, 
            paletteBorder, 
            null, 
            PaletteMetricPadding.HeaderGroupPaddingPrimary, 
            orientation)
    {
    }

    /// <summary>
    /// Initialize a new instance of the ViewDrawCanvas class.
    /// </summary>
    /// <param name="paletteBack">Palette source for the background.</param>        
    /// <param name="paletteBorder">Palette source for the border.</param>
    /// <param name="paletteMetric">Palette source for metric values.</param>
    /// <param name="metricPadding">Metric used to get padding values.</param>
    /// <param name="orientation">Visual orientation of the content.</param>
    public ViewDrawCanvas(IPaletteBack? paletteBack,
        IPaletteBorder? paletteBorder,
        IPaletteMetric? paletteMetric,
        PaletteMetricPadding metricPadding,
        VisualOrientation orientation)
    {
        // Cache the starting values
        _paletteBorder = paletteBorder;
        _paletteBack = paletteBack;
        _paletteMetric = paletteMetric;
        _metricPadding = metricPadding;
        Orientation = orientation;
        IncludeBorderEdge = orientation;
        ApplyIncludeBorderEdge = false;
        DrawTabBorder = false;
        DrawCanvas = true;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawCanvas:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_mementoBack != null)
            {
                _mementoBack.Dispose();
                _mementoBack = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region PaletteBack
    /// <summary>
    /// Gets access to the currently used background palette.
    /// </summary>
    public IPaletteBack? PaletteBack
    {
        [DebuggerStepThrough]
        get => _paletteBack;
    }
    #endregion

    #region PaletteBorder
    /// <summary>
    /// Gets access to the currently used border palette.
    /// </summary>
    public IPaletteBorder? PaletteBorder
    {
        [DebuggerStepThrough]
        get => _paletteBorder;
    }
    #endregion

    #region PaletteMetric
    /// <summary>
    /// Gets access to the currently used metric palette.
    /// </summary>
    public IPaletteMetric? PaletteMetric
    {
        [DebuggerStepThrough]
        get => _paletteMetric;
    }
    #endregion

    #region SetPalettes
    /// <summary>
    /// Update the source palettes for drawing.
    /// </summary>
    /// <param name="paletteBack">Palette source for the background.</param>        
    /// <param name="paletteBorder">Palette source for the border.</param>
    public virtual void SetPalettes([DisallowNull] IPaletteBack paletteBack,
        [DisallowNull] IPaletteBorder paletteBorder) =>
        SetPalettes(paletteBack, paletteBorder, null);

    /// <summary>
    /// Update the source palettes for drawing.
    /// </summary>
    /// <param name="paletteBack">Palette source for the background.</param>        
    /// <param name="paletteBorder">Palette source for the border.</param>
    /// <param name="paletteMetric">Palette source for the metric.</param>
    public virtual void SetPalettes([DisallowNull] IPaletteBack paletteBack, 
        [DisallowNull] IPaletteBorder paletteBorder,
        IPaletteMetric? paletteMetric)
    {
        Debug.Assert(paletteBorder is not null);
        Debug.Assert(paletteBack is not null);

        if (paletteBack is null)
        {
            throw new ArgumentNullException(nameof(paletteBack));
        }

        if(paletteBorder is null)
        {
            throw new ArgumentNullException(nameof(paletteBorder));
        }

        // Use newly provided palettes
        _paletteBack = paletteBack;

        // If not using a forced override decorator, then just store the new border palette
        // otherwise we update the decorator with the palette as the new inheritance to use
        if (_borderForced == null)
        {
            _paletteBorder = paletteBorder;
        }
        else
        {
            _borderForced.SetInherit(paletteBorder!);
        }

        _paletteMetric = paletteMetric!;
    }
    #endregion

    #region Orientation
    /// <summary>
    /// Gets and sets the visual orientation.
    /// </summary>
    public VisualOrientation Orientation
    {
        [DebuggerStepThrough]
        get;
        set;
    }

    #endregion

    #region DrawTabBorder
    /// <summary>
    /// Gets and sets if the border should be drawn as a tab border.
    /// </summary>
    public bool DrawTabBorder { get; set; }

    #endregion

    #region TabBorderStyle
    /// <summary>
    /// Gets and sets the tab border style to use.
    /// </summary>
    public TabBorderStyle TabBorderStyle { get; set; }

    #endregion

    #region IncludeBorderEdge
    /// <summary>
    /// Determines which border for the orientation is always drawn regardless of max border edges.
    /// </summary>
    public VisualOrientation IncludeBorderEdge { get; set; }

    #endregion

    #region ApplyIncludeBorderEdge
    /// <summary>
    /// Determines if the border for the orientation is always drawn regardless of max border edges.
    /// </summary>
    public bool ApplyIncludeBorderEdge { get; set; }

    #endregion

    #region MaxBorderEdges
    /// <summary>
    /// Gets and sets the maximum edges allowed.
    /// </summary>
    public PaletteDrawBorders MaxBorderEdges
    {
        get => _borderForced?.MaxBorderEdges ?? PaletteDrawBorders.All;

        set 
        {
            // If the decorator object used to override the border palette is not created...
            if (_borderForced == null)
            {
                // Then create it and pass the existing border palette as the inheritance
                _borderForced = new PaletteBorderInheritForced(_paletteBorder);

                // Now we want to always use the forced version instead
                _paletteBorder = _borderForced;
            }

            if (ApplyIncludeBorderEdge)
            {
                switch (IncludeBorderEdge)
                {
                    case VisualOrientation.Top:
                        value |= PaletteDrawBorders.Top;
                        break;
                    case VisualOrientation.Bottom:
                        value |= PaletteDrawBorders.Bottom;
                        break;
                    case VisualOrientation.Left:
                        value |= PaletteDrawBorders.Left;
                        break;
                    case VisualOrientation.Right:
                        value |= PaletteDrawBorders.Right;
                        break;
                }
            }

            _borderForced.MaxBorderEdges = value; 
        }
    }
    #endregion

    #region ForceGraphicsHint
    /// <summary>
    /// Gets and sets the forced value for the graphics hint.
    /// </summary>
    public PaletteGraphicsHint ForceGraphicsHint
    {
        get => _borderForced?.ForceGraphicsHint ?? PaletteGraphicsHint.Inherit;

        set 
        {
            // If the decorator object used to override the border palette is not created...
            if (_borderForced == null)
            {
                // Then create it and pass the existing border palette as the inheritance
                _borderForced = new PaletteBorderInheritForced(_paletteBorder);

                // Now we want to always use the forced version instead
                _paletteBorder = _borderForced;
            }

            _borderForced.ForceGraphicsHint = value; 
        }
    }
    #endregion

    #region DrawBorderAfter
    /// <summary>
    /// Gets the drawing of the border before or after children.
    /// </summary>
    public virtual bool DrawBorderLast => true;

    #endregion

    #region DrawCanvas
    /// <summary>
    /// Gets and sets if the canvas should 
    /// </summary>
    public bool DrawCanvas { get; set; }

    #endregion

    #region DrawOnComposition
    /// <summary>
    /// Gets and sets a value indicating if the canvas is drawing on composition.
    /// </summary>
    public bool DrawCanvasOnComposition { get; set; }

    #endregion

    #region GetOuterBorderPath
    /// <summary>
    /// Gets a path that describes the outside of the border.
    /// </summary>
    /// <param name="context">Context used by the renderer.</param>
    /// <returns>Path instance.</returns>
    public GraphicsPath? GetOuterBorderPath(RenderContext context)
    {
        return (_paletteBorder is null && context.Renderer is not null)
            ? context.Renderer.RenderStandardBorder.GetOutsideBorderPath(context, ClientRectangle, _paletteBorder, Orientation, State)
            : null;  // No palette details to use
    }
    #endregion

    #region Eval
    /// <summary>
    /// Evaluate the need for drawing transparent areas.
    /// </summary>
    /// <param name="context">Evaluation context.</param>
    /// <returns>True if transparent areas exist; otherwise false.</returns>
    public override bool EvalTransparentPaint([DisallowNull] ViewContext context)
    {
        Debug.Assert(context is not null);

        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // Ask the renderer to evaluate the given palette
        return context.Renderer.EvalTransparentPaint(_paletteBack!, _paletteBorder, State);
    }

    #endregion

    #region Layout

    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context is not null);

        // Validate incoming reference
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // Let base class find preferred size of the children
        Size preferredSize = base.GetPreferredSize(context);

        // Apply space the border takes up
        preferredSize = CommonHelper.ApplyPadding(Orientation, preferredSize,
            DrawTabBorder
                ? context.Renderer.RenderTabBorder.GetTabBorderDisplayPadding(context, _paletteBorder!, State, Orientation,
                    TabBorderStyle)
                : context.Renderer.RenderStandardBorder.GetBorderDisplayPadding(_paletteBorder!, State, Orientation));

        // Do we have a metric source for additional padding?
        if (_paletteMetric != null)
        {
            // Apply padding needed outside the border of the canvas
            preferredSize = CommonHelper.ApplyPadding(Orientation, preferredSize, _paletteMetric.GetMetricPadding(context.Control as KryptonForm, State, _metricPadding));
        }

        return preferredSize;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context is not null);

        // Validate incoming reference
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // We take on all the available display area
        ClientRectangle = context.DisplayRectangle;

        // Do we have a metric source for additional padding?
        if (_paletteMetric != null)
        {
            // Get the padding to be applied before the canvas drawing
            Padding outerPadding = _paletteMetric.GetMetricPadding(context.Control as KryptonForm, State, _metricPadding);

            // Apply the padding to the client rectangle
            ClientRectangle = CommonHelper.ApplyPadding(Orientation, ClientRectangle, outerPadding);
        }

        // Calculate how much space the border takes up
        Padding padding = DrawTabBorder
            ? context.Renderer.RenderTabBorder.GetTabBorderDisplayPadding(context, _paletteBorder!, State,
                Orientation, TabBorderStyle)
            : context.Renderer.RenderStandardBorder.GetBorderDisplayPadding(_paletteBorder!, State, Orientation);

        // Apply the padding to the client rectangle
        context.DisplayRectangle = CommonHelper.ApplyPadding(Orientation, ClientRectangle, padding);

        // Let child elements layout
        base.Layout(context);

        // Put back the original display value now we have finished
        context.DisplayRectangle = ClientRectangle;
    }
    #endregion

    #region Paint

    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void RenderBefore([DisallowNull] RenderContext context) 
    {
        Debug.Assert(context is not null);

        // Validate incoming reference
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // Do we need to draw the background?
        if (DrawCanvas && (_paletteBack!.GetBackDraw(State) == InheritBool.True))
        {
            GraphicsPath borderPath;
            Padding borderPadding;

            // Ask the border renderer for a path that encloses the border
            if (DrawTabBorder)
            {
                borderPath = context.Renderer.RenderTabBorder.GetTabBackPath(context, ClientRectangle, _paletteBorder!, Orientation, State, TabBorderStyle);
                borderPadding = Padding.Empty;
            }
            else
            {
                borderPath = context.Renderer.RenderStandardBorder.GetBackPath(context, ClientRectangle, _paletteBorder!, Orientation, State);
                borderPadding = context.Renderer.RenderStandardBorder.GetBorderRawPadding(_paletteBorder!, State, Orientation);
            }

            // Apply the padding depending on the orientation
            Rectangle enclosingRect = CommonHelper.ApplyPadding(Orientation, ClientRectangle, borderPadding);

            // Render the background inside the border path
            using var gh = new GraphicsHint(context.Graphics, _paletteBorder!.GetBorderGraphicsHint(State));
            _mementoBack = context.Renderer.RenderStandardBack.DrawBack(context, enclosingRect, borderPath, _paletteBack, Orientation, State, _mementoBack);

            borderPath.Dispose();
        }

        if (DrawCanvas && (_paletteBorder != null)
           )
        {
            // Do we draw the border before the children?
            if (!DrawBorderLast)
            {
                RenderBorder(context);
            }
            else
            {
                // Drawing border afterwards, and so clip children to prevent drawing
                // over the corners if they are rounded.  We only clip children if the 
                // border is drawn afterwards.

                // Remember the current clipping region
                _clipRegion = context.Graphics.Clip.Clone();

                // Restrict the clipping to the area inside the canvas border
                using GraphicsPath borderPath = DrawTabBorder 
                    ? context.Renderer.RenderTabBorder.GetTabBorderPath(context, ClientRectangle, _paletteBorder, Orientation, State, TabBorderStyle) 
                    : context.Renderer.RenderStandardBorder.GetBorderPath(context, ClientRectangle, _paletteBorder, Orientation, State);

                // Create a new region the same as the existing clipping region
                var combineRegion = new Region(borderPath);

                // Reduce clipping region down by our border path
                combineRegion.Intersect(_clipRegion);
                context.Graphics.Clip = combineRegion;
            }
        }
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

        if (DrawCanvas && (_paletteBorder != null))
        {
            // Do we draw the border after the children?
            if (DrawBorderLast)
            {
                // Set the clipping region back to original setting
                Region oldRegion = context.Graphics.Clip;
                context.Graphics.Clip = _clipRegion;

                // Remember to dispose of the temporary region, no longer needed
                oldRegion.Dispose();

                RenderBorder(context);
            }
        }
    }

    /// <summary>
    /// Draw the canvas border.
    /// </summary>
    /// <param name="context"></param>
    public virtual void RenderBorder([DisallowNull] RenderContext context)
    {
        Debug.Assert(context is not null);

        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // Do we need to draw the border?
        if (_paletteBorder!.GetBorderDraw(State) == InheritBool.True)
        {
            // Render the border over the background and children
            if (DrawTabBorder)
            {
                context?.Renderer.RenderTabBorder.DrawTabBorder(context, ClientRectangle, _paletteBorder, Orientation, State, TabBorderStyle);
            }
            else
            {
                context?.Renderer.RenderStandardBorder.DrawBorder(context, ClientRectangle, _paletteBorder, Orientation, State);
            }
        }
    }
    #endregion
}