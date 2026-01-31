#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// View element that applies padding to the drawing of a border and background.
/// </summary>
public class ViewDrawSplitCanvas : ViewComposite
{
    #region Instance Fields

    private readonly PaletteMetricPadding _metricPadding;
    private readonly PaletteBackInheritForced _paletteBackDraw;
    private readonly PaletteBackLightenColors _paletteBackLight;
    private IDisposable? _mementoBack;
    private PaletteBorderInheritForced? _borderForced;
    private Region? _clipRegion;
    private Rectangle _splitRectangle;
    private System.Windows.Forms.Timer? _rippleTimer;
    private readonly List<Ripple> _ripples = new List<Ripple>();

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawSplitCanvas class.
    /// </summary>
    /// <param name="paletteBack">Palette source for the background.</param>
    /// <param name="paletteBorder">Palette source for the border.</param>
    /// <param name="orientation">Visual orientation of the content.</param>
    public ViewDrawSplitCanvas(IPaletteBack paletteBack,
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
    /// Initialize a new instance of the ViewDrawSplitCanvas class.
    /// </summary>
    /// <param name="paletteBack">Palette source for the background.</param>
    /// <param name="paletteBorder">Palette source for the border.</param>
    /// <param name="paletteMetric">Palette source for metric values.</param>
    /// <param name="metricPadding">Matric used to get padding values.</param>
    /// <param name="orientation">Visual orientation of the content.</param>
    public ViewDrawSplitCanvas(IPaletteBack paletteBack,
        IPaletteBorder paletteBorder,
        IPaletteMetric? paletteMetric,
        PaletteMetricPadding metricPadding,
        VisualOrientation orientation)
    {
        // Cache the starting values
        PaletteBorder = paletteBorder;
        PaletteBack = paletteBack;
        _paletteBackDraw = new PaletteBackInheritForced(PaletteBack)
        {
            ForceDraw = InheritBool.True
        };
        _paletteBackLight = new PaletteBackLightenColors(PaletteBack);
        PaletteMetric = paletteMetric;
        _metricPadding = metricPadding;
        Orientation = orientation;
        DrawTabBorder = false;
        DrawCanvas = true;
        Splitter = false;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    // Return the class name and instance identifier
    public override string ToString() => $"ViewDrawSplitCanvas:{Id}";

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

            if (_clipRegion != null)
            {
                _clipRegion.Dispose();
                _clipRegion = null;
            }

            if (_rippleTimer != null)
            {
                _rippleTimer.Stop();
                _rippleTimer.Dispose();
                _rippleTimer = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region PaletteBack
    /// <summary>
    /// Gets access to the currently used background palette.
    /// </summary>
    public IPaletteBack PaletteBack
    {
        [DebuggerStepThrough]
        get;
        private set;
    }

    #endregion

    #region PaletteBorder
    /// <summary>
    /// Gets access to the currently used border palette.
    /// </summary>
    public IPaletteBorder? PaletteBorder
    {
        [DebuggerStepThrough]
        get;
        private set;
    }

    #endregion

    #region PaletteMetric
    /// <summary>
    /// Gets access to the currently used metric palette.
    /// </summary>
    public IPaletteMetric? PaletteMetric
    {
        [DebuggerStepThrough]
        get;
        private set;
    }

    #endregion

    #region SplitRectangle
    /// <summary>
    /// Gets and sets the split area of the canvas.
    /// </summary>
    public Rectangle SplitRectangle
    {
        get => _splitRectangle;

        set
        {
            _splitRectangle = value;

            if (FindMouseController() is ButtonController controller)
            {
                controller.SplitRectangle = value;
            }
        }
    }
    #endregion

    #region NonSplitRectangle
    /// <summary>
    /// Gets and sets the non split area of the canvas.
    /// </summary>
    public Rectangle NonSplitRectangle { get; set; }

    #endregion

    #region Splitter
    /// <summary>
    /// Is the canvas split into a normal and splitter area.
    /// </summary>
    public bool Splitter { get; set; }

    #endregion

    #region SetPalettes
    /// <summary>
    /// Update the source palettes for drawing.
    /// </summary>
    /// <param name="paletteBack">Palette source for the background.</param>
    /// <param name="paletteBorder">Palette source for the border.</param>
    public virtual void SetPalettes(IPaletteBack paletteBack,
        IPaletteBorder paletteBorder) => SetPalettes(paletteBack, paletteBorder, PaletteMetric);

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
        Debug.Assert(paletteBorder != null);
        Debug.Assert(paletteBack != null);

        // Use newly provided palettes
        PaletteBack = paletteBack!;
        _paletteBackDraw.SetInherit(paletteBack!);
        _paletteBackLight.Inherit = paletteBack!;

        // If not using a forced override decorator, then just store the new border palette
        // otherwise we update the decorator with the palette as the new inheritance to use
        if (_borderForced == null)
        {
            PaletteBorder = paletteBorder;
        }
        else
        {
            _borderForced.SetInherit(paletteBorder!);
        }

        PaletteMetric = paletteMetric;
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
                _borderForced = new PaletteBorderInheritForced(PaletteBorder);

                // Now we want to always use the forced version instead
                PaletteBorder = _borderForced;
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
                _borderForced = new PaletteBorderInheritForced(PaletteBorder);

                // Now we want to always use the forced version instead
                PaletteBorder = _borderForced;
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

    #region GetOuterBorderPath
    /// <summary>
    /// Gets a path that describes the outside of the border.
    /// </summary>
    /// <param name="context">Context used by the renderer.</param>
    /// <returns>Path instance.</returns>
    public GraphicsPath? GetOuterBorderPath([DisallowNull] RenderContext context)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        if (PaletteBorder != null)
        {
            return context.Renderer.RenderStandardBorder.GetOutsideBorderPath(context, ClientRectangle,
                PaletteBorder, Orientation,
                State);
        }

        // No palette details to use
        return null;
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
        return context!.Renderer.EvalTransparentPaint(PaletteBack, PaletteBorder, State);
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
                ? context.Renderer.RenderTabBorder.GetTabBorderDisplayPadding(context, PaletteBorder!, State, Orientation,
                    TabBorderStyle)
                : context.Renderer.RenderStandardBorder.GetBorderDisplayPadding(PaletteBorder!, State, Orientation));

        // Do we have a metric source for additional padding?
        if (PaletteMetric != null && _metricPadding != PaletteMetricPadding.None)
        {
            // Apply padding needed outside the border of the canvas
            preferredSize = CommonHelper.ApplyPadding(Orientation, preferredSize, PaletteMetric.GetMetricPadding(context.Control as KryptonForm, State, _metricPadding));
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
        if (PaletteMetric != null && _metricPadding != PaletteMetricPadding.None)
        {
            // Get the padding to be applied before the canvas drawing
            Padding outerPadding = PaletteMetric.GetMetricPadding(context.Control as KryptonForm, State, _metricPadding);

            // Apply the padding to the client rectangle
            ClientRectangle = CommonHelper.ApplyPadding(Orientation, ClientRectangle, outerPadding);
        }

        // Calculate how much space the border takes up
        var padding = DrawTabBorder
            ? context.Renderer.RenderTabBorder.GetTabBorderDisplayPadding(context, PaletteBorder!, State,
                Orientation, TabBorderStyle)
            : context.Renderer.RenderStandardBorder.GetBorderDisplayPadding(PaletteBorder!, State, Orientation);

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

        // Skip if we have no drawable area (can happen briefly during theme toggles)
        if (ClientRectangle.Width <= 0 || ClientRectangle.Height <= 0)
        {
            return;
        }

        RenderBackground(context, ClientRectangle);

        if (DrawCanvas && PaletteBorder != null &&
            ClientRectangle.Width > 0 &&
            ClientRectangle.Height > 0)
        {
            // Do we draw the border before the children?
            if (!DrawBorderLast)
            {
                RenderBorder(context, ClientRectangle);
                return;
            }

            // Drawing border afterwards. Only clip children when a real border will be drawn
            // and the client rectangle is valid. This avoids 1px gutters in Material normal state
            // and prevents issues during early layout when sizes can be 0x0.
            var rect = ClientRectangle;
            var draw = PaletteBorder.GetBorderDraw(State);
            var edges = PaletteBorder.GetBorderDrawBorders(State);
            var width = PaletteBorder.GetBorderWidth(State);
            if (!ThemeChangeCoordinator.InProgress && rect.Width > 0 && rect.Height > 0 && draw == InheritBool.True && CommonHelper.HasABorder(edges) && width > 0)
            {
                // Remember the current clipping region
                _clipRegion = context.Graphics.Clip.Clone();

                // Restrict the clipping to the area inside the canvas border
                if (ClientRectangle.Width <= 0 || ClientRectangle.Height <= 0)
                {
                    return;
                }
                GraphicsPath borderPath = DrawTabBorder
                    ? context.Renderer.RenderTabBorder.GetTabBorderPath(context, rect, PaletteBorder,
                        Orientation, State, TabBorderStyle)
                    : context.Renderer.RenderStandardBorder.GetBorderPath(context, rect, PaletteBorder,
                        Orientation, State);

                // Create a new region the same as the existing clipping region
                using (var combineRegion = new Region(borderPath))
                {
                    // Reduce clipping region down by our border path
                    combineRegion.Intersect(_clipRegion);
                    context.Graphics.Clip = combineRegion;
                }

                borderPath.Dispose();
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

        if (DrawCanvas && PaletteBorder != null &&
            ClientRectangle.Width > 0 &&
            ClientRectangle.Height > 0)
        {
            // Do we draw the border after the children?
            if (DrawBorderLast)
            {
                // Draw ripple overlay before the border when enabled for Material renderer
                // Avoid any extra clipping or overlay painting while theme switching is in progress
                if (_ripples.Count > 0 && context.Renderer is RenderMaterial &&
                    KryptonManager.CurrentGlobalPalette?.RippleEffect == true &&
                    !ThemeChangeCoordinator.InProgress)
                {
                    // Clip to the inside of the border/back path to keep the ripple bounded
                    GraphicsPath borderPath = DrawTabBorder
                        ? context.Renderer.RenderTabBorder.GetTabBackPath(context, ClientRectangle, PaletteBorder!, Orientation, State, TabBorderStyle)
                        : context.Renderer.RenderStandardBorder.GetBackPath(context, ClientRectangle, PaletteBorder!, Orientation, State);

                    using (var clip = new Clipping(context.Graphics, borderPath))
                    {
                        // Choose on-surface overlay color based on background brightness
                        Color surface = PaletteBack.GetBackColor1(State);
                        bool isDark = surface.GetBrightness() < 0.5f;
                        Color baseColor = isDark ? Color.White : Color.Black;

                        int now = Environment.TickCount;
                        float maxRadius = (float)Math.Sqrt((ClientWidth * ClientWidth) + (ClientHeight * ClientHeight));

                        for (int i = 0; i < _ripples.Count; i++)
                        {
                            Ripple r = _ripples[i];
                            float t = Math.Min(1f, (now - r.TicksStart) / (float)r.DurationMs);
                            int alpha = (int)(255f * (1f - t) * 0.20f);
                            float radius = t * maxRadius;

                            using var brush = new SolidBrush(Color.FromArgb(Math.Max(0, Math.Min(255, alpha)), baseColor));
                            RectangleF rect = new RectangleF(r.Origin.X - radius, r.Origin.Y - radius, radius * 2f, radius * 2f);
                            context.Graphics.FillEllipse(brush, rect);
                        }
                    }

                    borderPath.Dispose();
                }

                // Restore the clipping region only if we changed it
                if (_clipRegion != null)
                {
                    var oldRegion = context.Graphics.Clip;
                    context.Graphics.Clip = _clipRegion;
                    _clipRegion = null;
                    // Remember to dispose of the temporary region, no longer needed
                    oldRegion.Dispose();
                }

                RenderBorder(context, ClientRectangle);
            }
        }
    }
    #endregion

    #region Implementation
    private void RenderBackground(RenderContext context, Rectangle rect)
    {
        // Do we need to draw the background?
        if (DrawCanvas && PaletteBack.GetBackDraw(State) == InheritBool.True)
        {
            // During theme/palette changes, avoid split clipping path work to reduce GDI churn
            // If a theme change is in progress, still draw normally here; higher-level guards already prevent risky clipping
            if (Splitter)
            {
                var mouseInSplit = MouseInSplit;
                switch (State)
                {
                    case PaletteState.Tracking:
                    {
                        using (var clipToSplitter = new Clipping(context.Graphics, NonSplitRectangle))
                        {
                            if (SplitWithFading)
                            {
                                DrawBackground(context, rect, mouseInSplit ? _paletteBackLight : PaletteBack,
                                    PaletteBorder!, PaletteState.Tracking);
                            }
                            else
                            {
                                DrawBackground(context, rect, mouseInSplit ? _paletteBackDraw : PaletteBack,
                                    PaletteBorder!, mouseInSplit ? PaletteState.Normal : PaletteState.Tracking);
                            }
                        }

                        using (var clipToSplitter = new Clipping(context.Graphics, _splitRectangle))
                        {
                            if (SplitWithFading)
                            {
                                DrawBackground(context, rect, mouseInSplit ? PaletteBack : _paletteBackLight,
                                    PaletteBorder!, PaletteState.Tracking);
                            }
                            else
                            {
                                DrawBackground(context, rect, mouseInSplit ? PaletteBack : _paletteBackDraw,
                                    PaletteBorder!, mouseInSplit ? PaletteState.Tracking : PaletteState.Normal);
                            }
                        }
                    }
                        break;
                    case PaletteState.Pressed:
                    {
                        using (var clipToSplitter = new Clipping(context.Graphics, _splitRectangle))
                        {
                            if (SplitWithFading)
                            {
                                DrawBackground(context, rect, mouseInSplit ? PaletteBack : _paletteBackLight,
                                    PaletteBorder!, mouseInSplit ? PaletteState.Pressed : PaletteState.Tracking);
                            }
                            else
                            {
                                DrawBackground(context, rect, mouseInSplit ? PaletteBack : _paletteBackDraw,
                                    PaletteBorder!, mouseInSplit ? PaletteState.Pressed : PaletteState.Normal);
                            }
                        }

                        using (var clipToSplitter = new Clipping(context.Graphics, NonSplitRectangle))
                        {
                            if (SplitWithFading)
                            {
                                DrawBackground(context, rect, mouseInSplit ? _paletteBackLight : PaletteBack,
                                    PaletteBorder!, mouseInSplit ? PaletteState.Tracking : PaletteState.Pressed);
                            }
                            else
                            {
                                DrawBackground(context, rect, mouseInSplit ? _paletteBackDraw : PaletteBack,
                                    PaletteBorder!, mouseInSplit ? PaletteState.Normal : PaletteState.Pressed);
                            }
                        }
                    }
                        break;
                    default:
                        DrawBackground(context, rect, PaletteBack, PaletteBorder!, State);
                        break;
                }
            }
            else
            {
                DrawBackground(context, rect, PaletteBack, PaletteBorder!, State);
            }
        }
    }

    private void RenderBorder([DisallowNull] RenderContext context, Rectangle rect)
    {
        Debug.Assert(context != null);

        // Do we need to draw the border?
        if (PaletteBorder!.GetBorderDraw(State) == InheritBool.True)
        {
            if (Splitter)
            {
                var mouseInSplit = MouseInSplit;
                switch (State)
                {
                    case PaletteState.Tracking:
                        DrawBorder(context!, rect, PaletteBorder, PaletteState.Tracking);
                        break;
                    case PaletteState.Pressed:
                    {
                        DrawBorder(context!, rect, PaletteBorder, PaletteState.Tracking);

                        using var clipToSplitter = new Clipping(context!.Graphics,
                            mouseInSplit ? _splitRectangle : NonSplitRectangle);
                        DrawBorder(context, rect, PaletteBorder, PaletteState.Pressed);
                    }
                        break;
                    default:
                        DrawBorder(context!, rect, PaletteBorder, State);
                        break;
                }
            }
            else
            {
                DrawBorder(context!, rect, PaletteBorder, State);
            }
        }
    }

    private void DrawBackground([DisallowNull] RenderContext context,
        Rectangle rect,
        IPaletteBack paletteBack,
        IPaletteBorder paletteBorder,
        PaletteState state)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        GraphicsPath borderPath;
        Padding borderPadding;

        // Ask the border renderer for a path that encloses the border
        if (DrawTabBorder)
        {
            borderPath = context.Renderer.RenderTabBorder.GetTabBackPath(context, rect, paletteBorder, Orientation, state, TabBorderStyle);
            borderPadding = Padding.Empty;
        }
        else
        {
            borderPath = context.Renderer.RenderStandardBorder.GetBackPath(context, rect, paletteBorder, Orientation, state);
            borderPadding = context.Renderer.RenderStandardBorder.GetBorderRawPadding(paletteBorder, state, Orientation);
        }

        // Apply the padding depending on the orientation
        Rectangle enclosingRect = CommonHelper.ApplyPadding(Orientation, rect, borderPadding);

        // Render the background inside the border path
        using var gh = new GraphicsHint(context.Graphics, paletteBorder.GetBorderGraphicsHint(state));
        _mementoBack = context.Renderer.RenderStandardBack.DrawBack(context, enclosingRect, borderPath, paletteBack, Orientation, state, _mementoBack);

        borderPath.Dispose();
    }

    private void DrawBorder([DisallowNull] RenderContext context,
        Rectangle rect,
        IPaletteBorder paletteBorder,
        PaletteState state)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // Render the border over the background and children
        if (DrawTabBorder)
        {
            context.Renderer.RenderTabBorder.DrawTabBorder(context, rect, PaletteBorder!, Orientation, state, TabBorderStyle);
        }
        else
        {
            context.Renderer.RenderStandardBorder.DrawBorder(context, rect, PaletteBorder!, Orientation, state);
        }
    }

    private bool MouseInSplit
    {
        get
        {
            if (FindMouseController() is ButtonController controller)
            {
                if (!controller.MousePoint.Equals(CommonHelper.NullPoint))
                {
                    return _splitRectangle.Contains(controller.MousePoint);
                }
            }

            return false;
        }
    }

    private bool SplitWithFading => PaletteMetric == null ||
                                    PaletteMetric.GetMetricBool(State, PaletteMetricBool.SplitWithFading) ==
                                    InheritBool.True;

    #endregion

    #region Ripple
    private sealed class Ripple
    {
        public PointF Origin;
        public int TicksStart;
        public int DurationMs;
    }

    /// <summary>
    /// Starts a ripple animation from the specified control-relative origin.
    /// </summary>
    /// <param name="origin">Point in control coordinates.</param>
    public void StartRipple(Point origin)
    {
        // Guard against invalid size
        if (ClientRectangle.Width <= 0 || ClientRectangle.Height <= 0)
        {
            return;
        }

        // Convert from control coordinates to this canvas local coordinates
        var local = new Point(origin.X - ClientLocation.X, origin.Y - ClientLocation.Y);
        var ripple = new Ripple
        {
            Origin = new PointF(local.X, local.Y),
            TicksStart = Environment.TickCount,
            DurationMs = 300
        };

        _ripples.Add(ripple);

        if (_rippleTimer == null)
        {
            _rippleTimer = new System.Windows.Forms.Timer
            {
                Interval = 16
            };
            _rippleTimer.Tick += OnRippleTick;
        }

        if (!_rippleTimer.Enabled)
        {
            _rippleTimer.Start();
        }
    }

    /// <summary>
    /// Cancels all in-flight ripples.
    /// </summary>
    public void CancelRipples()
    {
        _ripples.Clear();
        if (_rippleTimer != null)
        {
            _rippleTimer.Stop();
        }
        // Request a repaint to clear any remaining visuals
        (FindMouseController() as ButtonController)?.PerformNeedPaint();
    }

    private void OnRippleTick(object? sender, EventArgs e)
    {
        int now = Environment.TickCount;
        // Remove completed ripples
        _ripples.RemoveAll(r => (now - r.TicksStart) >= r.DurationMs);

        if (_ripples.Count == 0 && _rippleTimer != null)
        {
            _rippleTimer.Stop();
        }

        // Invalidate to continue animation
        (FindMouseController() as ButtonController)?.PerformNeedPaint();
    }
    #endregion
}