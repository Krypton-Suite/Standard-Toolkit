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

namespace Krypton.Navigator;

/// <summary>
/// View element that knows how to hide and show stacked items depending on available space.
/// </summary>
internal class ViewLayoutOutlookFull : ViewLayoutScrollViewport
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutOutlookFull class.
    /// </summary>
    /// <param name="viewBuilder">View builder reference.</param>
    /// <param name="rootControl">Top level visual control.</param>
    /// <param name="viewportFiller">View element to place inside viewport.</param>
    /// <param name="paletteBorderEdge">Palette for use with the border edge.</param>
    /// <param name="paletteMetrics">Palette source for metrics.</param>
    /// <param name="metricPadding">Metric used to get view padding.</param>
    /// <param name="metricOvers">Metric used to get overposition.</param>
    /// <param name="orientation">Orientation for the viewport children.</param>
    /// <param name="alignment">Alignment of the children within the viewport.</param>
    /// <param name="animateChange">Animate changes in the viewport.</param>
    /// <param name="vertical">Is the viewport vertical.</param>
    /// <param name="needPaintDelegate">Delegate for notifying paint requests.</param>
    public ViewLayoutOutlookFull([DisallowNull] ViewBuilderOutlookBase viewBuilder,
        VisualControl rootControl,
        ViewBase viewportFiller,
        PaletteBorderEdge paletteBorderEdge,
        IPaletteMetric? paletteMetrics,
        PaletteMetricPadding metricPadding,
        PaletteMetricInt metricOvers,
        VisualOrientation orientation,
        RelativePositionAlign alignment,
        bool animateChange,
        bool vertical,
        NeedPaintHandler needPaintDelegate)
        : base(rootControl, viewportFiller, paletteBorderEdge, paletteMetrics,
            metricPadding, metricOvers, orientation, alignment, animateChange,
            vertical, needPaintDelegate)
    {
        Debug.Assert(viewBuilder is not null);
        ViewBuilder = viewBuilder ?? throw new ArgumentNullException(nameof(viewBuilder));
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutOutlookFull:{Id}";

    #endregion

    #region ViewBuilder
    /// <summary>
    /// Gets access to the associated view builder.
    /// </summary>
    public ViewBuilderOutlookBase ViewBuilder
    {
        [DebuggerStepThrough]
        get;
    }

    #endregion

    #region Layout
    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout(ViewLayoutContext context)
    {
        bool relayout;
        bool canScrollV;
        bool canScrollH;

        // Update the enabled state of the scrollbars and contained control
        ViewControl.Enabled = Enabled;
        ScrollbarV.Enabled = Enabled;
        ScrollbarH.Enabled = Enabled;
        BorderEdgeV.Enabled = Enabled;
        BorderEdgeH.Enabled = Enabled;

        // Cache the starting viewport offsets
        Point originalOffset = Viewport.Offset;

        // Hide both scrollbars, in case having them both hidden
        // always enough content to be seen that none or only one
        // of them is required.
        BorderEdgeV.Visible = ScrollbarV.Visible = false;
        BorderEdgeH.Visible = ScrollbarH.Visible = false;

        // Get the the visible state before processing
        var beforeOverflowState = ViewBuilder.GetOverflowButtonStates();

        // Make all stacking items visible so all that can be shown will be
        ViewBuilder.UnShrinkAppropriatePages();

        // Do not actually change the layout of any child controls
        context.ViewManager!.DoNotLayoutControls = true;

        do
        {
            // Do we need to layout again?
            relayout = false;

            // Always reinstate the cached offset, so that if one of the cycles
            // limits the offset to a different value then subsequent cycles
            // will not remember that artificial limitation
            Viewport.Offset = originalOffset;

            // Make sure the viewport has extents calculated
            Viewport.GetPreferredSize(context);

            // Let base class perform a layout calculation
            DockerLayout(context);

            // Find the latest scrolling requirement
            canScrollV = Viewport.CanScrollV;
            canScrollH = Viewport.CanScrollH;

            // If we need to use vertical scrolling...
            if (canScrollV)
            {
                // Ask the view builder to try and hide stacking items to free up some vertical
                // space. We provide the amount of space required to remove the vertical scroll
                // bar from view, so only the minimum number of stacking items are removed.
                relayout = ViewBuilder.ShrinkVertical(Viewport.ScrollExtent.Height - Viewport.ClientSize.Height);
            }

            // Is there a change in vertical scrolling?
            if (canScrollV != ScrollbarV.Visible)
            {
                // Update the view elements
                ScrollbarV.Visible = canScrollV;
                BorderEdgeV.Visible = canScrollV;
                relayout = true;
            }

            // Is there a change in horizontally scrolling?
            if (canScrollH != ScrollbarH.Visible)
            {
                // Update the view elements
                ScrollbarH.Visible = canScrollH;
                BorderEdgeH.Visible = canScrollH;
                relayout = true;
            }

            // We short size the horizontal scrollbar if both bars are showing
            var needShortSize = (ScrollbarV.Visible && ScrollbarH.Visible);

            if (ScrollbarH.ShortSize != needShortSize)
            {
                // Update the scrollbar view and need layout to reflect resizing
                ScrollbarH.ShortSize = needShortSize;
                relayout = true;
            }

        } while (relayout);

        // Now all layouts have occurred we can actually move child controls
        context.ViewManager.DoNotLayoutControls = false;

        // Perform actual layout of child controls
        foreach (ViewBase child in this)
        {
            context.DisplayRectangle = child.ClientRectangle;
            child.Layout(context);
        }

        // Do we need to update the vertical scrolling values?
        if (canScrollV)
        {
            ScrollbarV.SetScrollValues(0, Viewport.ScrollExtent.Height - 1,
                1, Viewport.ClientSize.Height,
                Viewport.ScrollOffset.Y);
        }

        // Do we need to update the horizontal scrolling values?
        if (canScrollH)
        {
            ScrollbarH.SetScrollValues(0, Viewport.ScrollExtent.Width - 1,
                1, Viewport.ClientSize.Width,
                Viewport.ScrollOffset.X);
        }

        // If visible state of an overflow button has changed, need to relayout
        if (!beforeOverflowState.Equals(ViewBuilder.GetOverflowButtonStates()))
        {
            NeedPaint(true);
        }
    }
    #endregion
}