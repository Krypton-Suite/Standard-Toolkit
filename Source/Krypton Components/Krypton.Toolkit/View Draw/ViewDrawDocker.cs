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
/// Extends the ViewDrawCanvas by applying a docking style for each child.
/// </summary>
public class ViewDrawDocker : ViewDrawCanvas
{
    #region Instance Fields
    private readonly PaletteMetricBool _metricOverlay;
    private readonly ViewDockStyleLookup _childDocking;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawDocker class.
    /// </summary>
    public ViewDrawDocker()
        : this(null, null, null, PaletteMetricBool.None)
    {
    }

    /// <summary>
    /// Initialize a new instance of the ViewDrawDocker class.
    /// </summary>
    /// <param name="paletteBack">Palette source for the background.</param>        
    /// <param name="paletteBorder">Palette source for the border.</param>
    public ViewDrawDocker(IPaletteBack paletteBack,
        IPaletteBorder paletteBorder)
        : this(paletteBack, paletteBorder, null, PaletteMetricBool.None)
    {
    }

    /// <summary>
    /// Initialize a new instance of the ViewDrawDocker class.
    /// </summary>
    /// <param name="paletteBack">Palette source for the background.</param>        
    /// <param name="paletteBorder">Palette source for the border.</param>
    /// <param name="paletteMetric">Palette source for metrics.</param>
    public ViewDrawDocker(IPaletteBack paletteBack,
        IPaletteBorder paletteBorder,
        IPaletteMetric? paletteMetric)
        : this(paletteBack, paletteBorder, paletteMetric, PaletteMetricBool.None)
    {
    }

    /// <summary>
    /// Initialize a new instance of the ViewDrawDocker class.
    /// </summary>
    /// <param name="paletteBack">Palette source for the background.</param>        
    /// <param name="paletteBorder">Palette source for the border.</param>
    /// <param name="paletteMetric">Palette source for metrics.</param>
    /// <param name="metricOverlay">Metric to use for border overlay.</param>
    public ViewDrawDocker(IPaletteBack? paletteBack,
        IPaletteBorder? paletteBorder,
        IPaletteMetric? paletteMetric,
        PaletteMetricBool metricOverlay)
        : this(paletteBack, paletteBorder,
            paletteMetric, metricOverlay,
            PaletteMetricPadding.None, VisualOrientation.Top)
    {
    }

    /// <summary>
    /// Initialize a new instance of the ViewDrawDocker class.
    /// </summary>
    /// <param name="paletteBack">Palette source for the background.</param>        
    /// <param name="paletteBorder">Palette source for the border.</param>
    /// <param name="paletteMetric">Palette source for metric values.</param>
    /// <param name="metricOverlay">Metric to use for border overlay.</param>
    /// <param name="metricPadding">Metric used to get padding values.</param>
    /// <param name="orientation">Visual orientation of the content.</param>
    public ViewDrawDocker(IPaletteBack? paletteBack,
        IPaletteBorder? paletteBorder,
        IPaletteMetric? paletteMetric,
        PaletteMetricBool metricOverlay,
        PaletteMetricPadding metricPadding,
        VisualOrientation orientation)
        : base(paletteBack, paletteBorder, paletteMetric, metricPadding, orientation)
    {
        // Cache the starting values
        _metricOverlay = metricOverlay;

        // Create other state
        _childDocking = new ViewDockStyleLookup();
        FillRectangle = Rectangle.Empty;
        IgnoreBorderSpace = false;
        RemoveChildBorders = false;
        PreferredSizeAll = false;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawDocker:{Id}";

    #endregion

    #region IgnoreBorderSpace
    /// <summary>
    /// Gets and sets a value indicating if border space should be ignored in working out preferred size.
    /// </summary>
    public bool IgnoreBorderSpace { get; set; }

    #endregion

    #region IgnoreAllBorderAndPadding
    /// <summary>
    /// Gets and sets a value indicating if border space should be ignored in working out preferred size.
    /// </summary>
    public bool IgnoreAllBorderAndPadding { get; set; }

    #endregion

    #region RemoveChildBorders
    /// <summary>
    /// Gets and sets a value indicating if borders for docking edged children should be removed to prevent double borders.
    /// </summary>
    public bool RemoveChildBorders { get; set; }

    #endregion

    #region ForceBorderFirst
    /// <summary>
    /// Gets and sets a value indicating if the border should be forced to draw first.
    /// </summary>
    public bool ForceBorderFirst { get; set; }

    #endregion

    #region PreferredSizeAll
    /// <summary>
    /// Gets and sets a value indicating if calculating the preferred size should include visible and invisible children.
    /// </summary>
    public bool PreferredSizeAll { get; set; }

    #endregion

    #region DrawBorderLast
    /// <summary>
    /// Gets the drawing of the border before or after children.
    /// </summary>
    public override bool DrawBorderLast
    {
        get
        {
            if (ForceBorderFirst)
            {
                return false;
            }
            else
            {
                if (_paletteMetric != null && _metricOverlay != PaletteMetricBool.None)
                {
                    var overlay = _paletteMetric.GetMetricBool(ElementState, _metricOverlay);
                    return overlay == InheritBool.False;
                }
                else
                {
                    return base.DrawBorderLast;
                }
            }
        }
    }
    #endregion

    #region FillRect
    /// <summary>
    /// Gets the fill rectangle left after positioning all children.
    /// </summary>
    public Rectangle FillRectangle { get; private set; }

    #endregion

    #region Dock
    /// <summary>
    /// Gets the dock setting for the provided child instance.
    /// </summary>
    /// <param name="child">Child view element.</param>
    /// <returns>Docking setting.</returns>
    public ViewDockStyle GetDock([DisallowNull] ViewBase child)
    {
        Debug.Assert(child != null);

        // Does this element exist in the lookup?
        if (!_childDocking.ContainsKey(child!))
        {
            // No, so add with a default value
            _childDocking.Add(child!, ViewDockStyle.Top);
        }

        return _childDocking[child!];
    }

    /// <summary>
    /// Sets the dock setting for the provided child instance.
    /// </summary>
    /// <param name="child">Child view element.</param>
    /// <param name="dock">DockStyle setting.</param>
    public void SetDock([DisallowNull] ViewBase child, ViewDockStyle dock)
    {
        Debug.Assert(child is not null);

        if (child is null)
        {
            throw new ArgumentNullException(nameof(child));
        }

        // If the lookup is not already defined
        if (!_childDocking.ContainsKey(child!))
        {
            // Then just add the value
            _childDocking.Add(child!, dock);
        }
        else
        {
            // Overwrite the existing value
            _childDocking[child] = dock;
        }
    }
    #endregion

    #region Collection
    /// <summary>
    /// Append a view to the collection.
    /// </summary>
    /// <param name="item">ViewBase reference.</param>
    /// <param name="dock">DockStyle setting.</param>
    public void Add(ViewBase item, ViewDockStyle dock)
    {
        // Add the child to the view
        Add(item);

        // Set the initial docking for the new element
        SetDock(item, dock);
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

        // Check with the base canvas first
        if (base.EvalTransparentPaint(context!))
        {
            return true;
        }

        // If drawing the other elements over the top of the border
        // then we need to check each element as any of them could
        // have an impact on the transparent drawing.
        if (!DrawBorderLast)
        {
            // Check each child that is docked against an edge
            foreach (var child in Reverse().Where(child => child.Visible))
            {
                switch (GetDock(child))
                {
                    case ViewDockStyle.Top:
                    case ViewDockStyle.Bottom:
                    case ViewDockStyle.Left:
                    case ViewDockStyle.Right:
                        if (child.EvalTransparentPaint(context))
                        {
                            return true;
                        }

                        break;
                }
            }
        }

        // Could not find anything that needs transparent painting
        return false;
    }

    #endregion

    #region Layout
    /// <summary>
    /// Gets the size required for all except the contents.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public Size GetNonChildSize([DisallowNull] ViewLayoutContext context)
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

        // Remember the original display rectangle provided
        var originalRect = context!.DisplayRectangle;
        var displayRect = context.DisplayRectangle;

        // Border size that is not applied to preferred size
        var borderSize = Size.Empty;

        // Accumulate the size that must be provided by docking edges and then filler
        var preferredSize = Size.Empty;

        // Track the minimize size needed to satisfy the docking edges only
        var minimumSize = Size.Empty;

        if (!IgnoreAllBorderAndPadding)
        {
            // Apply space the border takes up
            if (IgnoreBorderSpace)
            {
                borderSize = CommonHelper.ApplyPadding(Orientation, borderSize, context.Renderer.RenderStandardBorder.GetBorderDisplayPadding(_paletteBorder!, State, Orientation));
            }
            else
            {
                var padding = context.Renderer.RenderStandardBorder.GetBorderDisplayPadding(_paletteBorder!, State, Orientation);
                preferredSize = CommonHelper.ApplyPadding(Orientation, preferredSize, padding);
                displayRect = CommonHelper.ApplyPadding(Orientation, displayRect, padding);
            }

            // Do we have a metric source for additional padding?
            if (_paletteMetric != null && _metricPadding != PaletteMetricPadding.None)
            {
                // Apply padding needed outside the border of the canvas
                var padding = _paletteMetric.GetMetricPadding(context.Control as KryptonForm, State, _metricPadding);
                preferredSize = CommonHelper.ApplyPadding(Orientation, preferredSize, padding);
                displayRect = CommonHelper.ApplyPadding(Orientation, displayRect, padding);
            }
        }

        // Put back the original display rect
        context.DisplayRectangle = originalRect;

        // Enforce the minimum values from the other docking edge sizes
        preferredSize.Width = Math.Max(preferredSize.Width, minimumSize.Width);
        preferredSize.Height = Math.Max(preferredSize.Height, minimumSize.Height);

        // Enforce the border sizing as the minimum
        preferredSize.Width = Math.Max(preferredSize.Width, borderSize.Width);
        preferredSize.Height = Math.Max(preferredSize.Height, borderSize.Height);

        return preferredSize;
    }

    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
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

        // Remember the original display rectangle provided
        var originalRect = context!.DisplayRectangle;
        var displayRect = context.DisplayRectangle;

        // Border size that is not applied to preferred size
        var borderSize = Size.Empty;

        // Accumulate the size that must be provided by docking edges and then filler
        var preferredSize = Size.Empty;

        // Track the minimize size needed to satisfy the docking edges only
        var minimumSize = Size.Empty;

        if (!IgnoreAllBorderAndPadding)
        {
            // Apply space the border takes up
            if (IgnoreBorderSpace)
            {
                borderSize = CommonHelper.ApplyPadding(Orientation, borderSize, context.Renderer.RenderStandardBorder.GetBorderDisplayPadding(_paletteBorder, State, Orientation));
            }
            else
            {
                var padding = context.Renderer.RenderStandardBorder.GetBorderDisplayPadding(_paletteBorder, State, Orientation);
                preferredSize = CommonHelper.ApplyPadding(Orientation, preferredSize, padding);
                displayRect = CommonHelper.ApplyPadding(Orientation, displayRect, padding);
            }

            // Do we have a metric source for additional padding?
            if (_paletteMetric != null && _metricPadding != PaletteMetricPadding.None)
            {
                // Apply padding needed outside the border of the canvas
                var padding = _paletteMetric.GetMetricPadding(context.Control as KryptonForm, State, _metricPadding);
                preferredSize = CommonHelper.ApplyPadding(Orientation, preferredSize, padding);
                displayRect = CommonHelper.ApplyPadding(Orientation, displayRect, padding);
            }
        }

        var leftEdges = PaletteDrawBorders.All;
        var rightEdges = PaletteDrawBorders.All;
        var topEdges = PaletteDrawBorders.All;
        var bottomEdges = PaletteDrawBorders.All;
        var fillEdges = PaletteDrawBorders.All;

        // Check for edge docking children
        foreach (var child in Reverse())
        {
            // Only position visible children that are not 'fill'
            if ((child.Visible || PreferredSizeAll) && GetDock(child) != ViewDockStyle.Fill)
            {
                // Prevent children from showing adjacent borders that are not needed
                UpdateChildBorders(child, context, ref leftEdges, ref rightEdges,
                    ref topEdges, ref bottomEdges, ref fillEdges);

                // Update with latest calculated display rectangle
                context.DisplayRectangle = displayRect;

                // Get the preferred size of the child
                var childSize = child.GetPreferredSize(context);

                // Apply size requests from edge docking children
                switch (OrientateDock(GetDock(child)))
                {
                    case ViewDockStyle.Top:
                        preferredSize.Height += childSize.Height;
                        displayRect.Y += childSize.Height;
                        displayRect.Height -= childSize.Height;

                        if (minimumSize.Width < childSize.Width)
                        {
                            minimumSize.Width = childSize.Width;
                        }

                        break;
                    case ViewDockStyle.Bottom:
                        preferredSize.Height += childSize.Height;
                        displayRect.Height -= childSize.Height;

                        if (minimumSize.Width < childSize.Width)
                        {
                            minimumSize.Width = childSize.Width;
                        }

                        break;
                    case ViewDockStyle.Left:
                        preferredSize.Width += childSize.Width;
                        displayRect.X += childSize.Width;
                        displayRect.Width -= childSize.Width;

                        if (minimumSize.Height < childSize.Height)
                        {
                            minimumSize.Height = childSize.Height;
                        }

                        break;
                    case ViewDockStyle.Right:
                        preferredSize.Width += childSize.Width;
                        displayRect.Width -= childSize.Width;

                        if (minimumSize.Height < childSize.Height)
                        {
                            minimumSize.Height = childSize.Height;
                        }

                        break;
                }
            }
        }

        // Check for the fill child last
        foreach (var child in Reverse())
        {
            // Only interested in a visible 'fill' child
            if ((child.Visible || PreferredSizeAll) && GetDock(child) == ViewDockStyle.Fill)
            {
                // Prevent children from showing adjacent borders that are not needed
                UpdateChildBorders(child, context, ref leftEdges, ref rightEdges,
                    ref topEdges, ref bottomEdges, ref fillEdges);

                // Update with latest calculated display rectangle
                context.DisplayRectangle = displayRect;

                // Get the preferred size of the child
                var childSize = child.GetPreferredSize(context);

                // Add on the preferred size of the filler
                preferredSize.Width += childSize.Width;
                preferredSize.Height += childSize.Height;
            }
        }

        // Put back the original display rect
        context.DisplayRectangle = originalRect;

        // Enforce the minimum values from the other docking edge sizes
        preferredSize.Width = Math.Max(preferredSize.Width, minimumSize.Width);
        preferredSize.Height = Math.Max(preferredSize.Height, minimumSize.Height);

        // Enforce the border sizing as the minimum
        preferredSize.Width = Math.Max(preferredSize.Width, borderSize.Width);
        preferredSize.Height = Math.Max(preferredSize.Height, borderSize.Height);

        return preferredSize;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
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

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;

        if (!IgnoreAllBorderAndPadding)
        {
            // Do we have a metric source for additional padding?
            if (_paletteMetric != null && _metricPadding != PaletteMetricPadding.None)
            {
                // Get the padding to be applied before the canvas drawing
                var outerPadding = _paletteMetric.GetMetricPadding(context.Control as KryptonForm, State, _metricPadding);
                ClientRectangle = CommonHelper.ApplyPadding(Orientation, ClientRectangle, outerPadding);
            }
        }

        // Space available for children begins with our space
        var fillerRect = ClientRectangle;
        context.DisplayRectangle = fillerRect;

        // By default, all the children need to draw all their borders
        var leftEdges = PaletteDrawBorders.All;
        var rightEdges = PaletteDrawBorders.All;
        var topEdges = PaletteDrawBorders.All;
        var bottomEdges = PaletteDrawBorders.All;
        var fillEdges = PaletteDrawBorders.All;

        // Position all except the filler
        foreach (var child in Reverse())
        {
            // Only position visible children
            if (child.Visible && GetDock(child) != ViewDockStyle.Fill)
            {
                // Prevent children from showing adjacent borders that are not needed
                UpdateChildBorders(child, context, ref leftEdges, ref rightEdges,
                    ref topEdges, ref bottomEdges, ref fillEdges);

                // Get the preferred size of the child
                var childSize = child.GetPreferredSize(context);

                // Position the child inside the available space
                switch (CalculateDock(OrientateDock(GetDock(child)), context.Control))
                {
                    case ViewDockStyle.Top:
                        context.DisplayRectangle = fillerRect with { Height = childSize.Height };
                        fillerRect.Height -= childSize.Height;
                        fillerRect.Y += childSize.Height;
                        break;
                    case ViewDockStyle.Bottom:
                        context.DisplayRectangle = fillerRect with { Y = fillerRect.Bottom - childSize.Height, Height = childSize.Height };
                        fillerRect.Height -= childSize.Height;
                        break;
                    case ViewDockStyle.Left:
                        context.DisplayRectangle = fillerRect with { Width = childSize.Width };
                        fillerRect.Width -= childSize.Width;
                        fillerRect.X += childSize.Width;
                        break;
                    case ViewDockStyle.Right:
                        context.DisplayRectangle = fillerRect with { X = fillerRect.Right - childSize.Width, Width = childSize.Width };
                        fillerRect.Width -= childSize.Width;
                        break;
                }

                // Layout child in the provided space
                child.Layout(context);
            }
        }

        var borderRect = ClientRectangle;
        var padding = Padding.Empty;

        if (!IgnoreAllBorderAndPadding)
        {
            // Find the actual width of the border as we need to compare this to the calculating border
            // padding to work out how far from corners we can ignore the calculated border padding and 
            // instead use the actual width only.
            var borderWidth = _paletteBorder!.GetBorderWidth(State);

            // Update padding to reflect the orientation we are using
            padding = context.Renderer.RenderStandardBorder.GetBorderDisplayPadding(_paletteBorder, State, Orientation);
            padding = CommonHelper.OrientatePadding(Orientation, padding);

            // If docking content extends beyond the border rounding effects then we can adjust 
            // the padding back so that it lines against the edge and not the rounding edge
            padding = AdjustPaddingForDockers(padding, fillerRect, borderWidth);
        }

        // Apply the padding to the border rectangle
        borderRect = new Rectangle(borderRect.X + padding.Left, borderRect.Y + padding.Top,
            borderRect.Width - padding.Horizontal, borderRect.Height - padding.Vertical);

        // We need to ensure the filler is within the border rectangle
        if (fillerRect.X < borderRect.X)
        {
            fillerRect.Width -= borderRect.X - fillerRect.X;
            fillerRect.X = borderRect.X;
        }

        if (fillerRect.Y < borderRect.Y)
        {
            fillerRect.Height -= borderRect.Y - fillerRect.Y;
            fillerRect.Y = borderRect.Y;
        }

        if (fillerRect.Right > borderRect.Right)
        {
            fillerRect.Width -= fillerRect.Right - borderRect.Right;
        }

        if (fillerRect.Bottom > borderRect.Bottom)
        {
            fillerRect.Height -= fillerRect.Bottom - borderRect.Bottom;
        }

        // Position any filler last
        foreach (var child in Reverse())
        {
            // Only position visible children
            if (child.Visible && GetDock(child) == ViewDockStyle.Fill)
            {
                // Prevent children from showing adjacent borders that are not needed
                UpdateChildBorders(child, context, ref leftEdges, ref rightEdges,
                    ref topEdges, ref bottomEdges, ref fillEdges);

                // Give the filler the remaining space
                context.DisplayRectangle = fillerRect;

                // Layout child in the provided space
                child.Layout(context);
            }
        }

        // Put back the original display value now we have finished
        context.DisplayRectangle = ClientRectangle;

        // The fill rectangle is the space left over after all children are positioned
        FillRectangle = fillerRect;
    }

    private void UpdateChildBorders(ViewBase child,
        ViewLayoutContext context,
        ref PaletteDrawBorders leftEdges,
        ref PaletteDrawBorders rightEdges,
        ref PaletteDrawBorders topEdges,
        ref PaletteDrawBorders bottomEdges,
        ref PaletteDrawBorders fillEdges)
    {
        // Do we need to calculate if the child should remove any borders?
        if (RemoveChildBorders)
        {
            // Check if the view is a canvas
            var childCanvas = child as ViewDrawCanvas;

            // Docking edge determines calculation
            switch (CalculateDock(GetDock(child), context.Control!))
            {
                case ViewDockStyle.Top:
                    if (childCanvas != null)
                    {
                        childCanvas.MaxBorderEdges = CommonHelper.ReverseOrientateDrawBorders(topEdges, childCanvas.Orientation);
                    }

                    // Remove top edges from subsequent children
                    leftEdges &= PaletteDrawBorders.BottomLeftRight;
                    rightEdges &= PaletteDrawBorders.BottomLeftRight;
                    topEdges &= PaletteDrawBorders.BottomLeftRight;
                    break;

                case ViewDockStyle.Bottom:
                    if (childCanvas != null)
                    {
                        childCanvas.MaxBorderEdges = CommonHelper.ReverseOrientateDrawBorders(bottomEdges, childCanvas.Orientation);
                    }

                    // Remove bottom edges from subsequent children
                    leftEdges &= PaletteDrawBorders.TopLeftRight;
                    rightEdges &= PaletteDrawBorders.TopLeftRight;
                    bottomEdges &= PaletteDrawBorders.TopLeftRight;
                    break;

                case ViewDockStyle.Left:
                    if (childCanvas != null)
                    {
                        childCanvas.MaxBorderEdges = CommonHelper.ReverseOrientateDrawBorders(leftEdges, childCanvas.Orientation);
                    }

                    // Remove left edges from subsequent children
                    topEdges &= PaletteDrawBorders.TopBottomRight;
                    bottomEdges &= PaletteDrawBorders.TopBottomRight;
                    leftEdges &= PaletteDrawBorders.TopBottomRight;
                    break;

                case ViewDockStyle.Right:
                    if (childCanvas != null)
                    {
                        childCanvas.MaxBorderEdges = CommonHelper.ReverseOrientateDrawBorders(rightEdges, childCanvas.Orientation);
                    }

                    // Remove right edges from subsequent children
                    topEdges &= PaletteDrawBorders.TopBottomLeft;
                    bottomEdges &= PaletteDrawBorders.TopBottomLeft;
                    rightEdges &= PaletteDrawBorders.TopBottomLeft;
                    break;
            }
        }
    }

    private Padding AdjustPaddingForDockers(Padding padding,
        Rectangle fillerRect,
        int borderWidth)
    {
        // Find the distance the filler rect is displayed inside the client area
        var topDiff = fillerRect.Y;
        var leftDiff = fillerRect.X;
        var bottomDiff = ClientRectangle.Bottom - fillerRect.Bottom;
        var rightDiff = ClientRectangle.Right - fillerRect.Right;

        // Calculate how far the rounding effects work
        var pullBackTop = padding.Top * 2;
        var pullBackBottom = padding.Bottom * 2;
        var pullBackLeft = padding.Left * 2;
        var pullBackRight = padding.Right * 2;

        if (padding.Left > borderWidth &&
            topDiff >= pullBackTop && topDiff >= padding.Top &&
            bottomDiff >= pullBackBottom && bottomDiff >= padding.Bottom)
        {
            padding.Left = borderWidth;
        }

        if (padding.Right > borderWidth &&
            topDiff >= pullBackTop && topDiff >= padding.Top &&
            bottomDiff >= pullBackBottom && bottomDiff >= padding.Bottom)
        {
            padding.Right = borderWidth;
        }

        if (padding.Top > borderWidth &&
            leftDiff >= pullBackLeft && leftDiff >= padding.Left &&
            rightDiff >= pullBackRight && rightDiff >= padding.Right)
        {
            padding.Top = borderWidth;
        }

        if (padding.Bottom > borderWidth &&
            leftDiff >= pullBackLeft && leftDiff >= padding.Left &&
            rightDiff >= pullBackRight && rightDiff >= padding.Right)
        {
            padding.Bottom = borderWidth;
        }

        return padding;
    }
    #endregion

    #region Implementation
    /// <summary>
    /// Find the actual docking to apply for the specified RightToLeft setting.
    /// </summary>
    /// <param name="ds">Docking style.</param>
    /// <param name="control">Control for which the setting is needed.</param>
    /// <returns>Calculated docking to actual use.</returns>
    protected ViewDockStyle CalculateDock(ViewDockStyle ds, Control? control)
    {
        // Do we need to adjust to reflect right to left layout?
        if (CommonHelper.GetRightToLeftLayout(control!) && control!.RightToLeft == RightToLeft.Yes)
        {
            // Only need to invert the left and right sides
            ds = ds switch
            {
                ViewDockStyle.Left => ViewDockStyle.Right,
                ViewDockStyle.Right => ViewDockStyle.Left,
                _ => ds
            };
        }

        return ds;
    }

    /// <summary>
    /// Update the incoming dock style to reflect our orientation.
    /// </summary>
    /// <param name="style">Incoming dock style.</param>
    /// <returns>Orientation adjusted dock style.</returns>
    protected ViewDockStyle OrientateDock(ViewDockStyle style)
    {
        switch (Orientation)
        {
            case VisualOrientation.Top:
                // Nothing to do, as top is the standard setting
                break;
            case VisualOrientation.Left:
                switch (style)
                {
                    case ViewDockStyle.Top:
                        return ViewDockStyle.Left;
                    case ViewDockStyle.Left:
                        return ViewDockStyle.Bottom;
                    case ViewDockStyle.Right:
                        return ViewDockStyle.Top;
                    case ViewDockStyle.Bottom:
                        return ViewDockStyle.Right;
                }
                break;
            case VisualOrientation.Right:
                switch (style)
                {
                    case ViewDockStyle.Top:
                        return ViewDockStyle.Right;
                    case ViewDockStyle.Left:
                        return ViewDockStyle.Top;
                    case ViewDockStyle.Right:
                        return ViewDockStyle.Bottom;
                    case ViewDockStyle.Bottom:
                        return ViewDockStyle.Left;
                }
                break;
            case VisualOrientation.Bottom:
                switch (style)
                {
                    case ViewDockStyle.Top:
                        return ViewDockStyle.Bottom;
                    case ViewDockStyle.Left:
                        return ViewDockStyle.Right;
                    case ViewDockStyle.Right:
                        return ViewDockStyle.Left;
                    case ViewDockStyle.Bottom:
                        return ViewDockStyle.Top;
                }
                break;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(Orientation.ToString());
                break;
        }

        // No change required
        return style;
    }
    #endregion
}