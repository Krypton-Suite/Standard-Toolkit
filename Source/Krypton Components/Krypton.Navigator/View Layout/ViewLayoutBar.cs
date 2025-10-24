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
/// View element that can size and position each page entry on the bar.
/// </summary>
internal class ViewLayoutBar : ViewComposite
{
    #region LineDetails
    private struct LineDetails
    {
        public readonly int InlineLength;
        public readonly int CrossLength;
        public readonly int StartIndex;
        public readonly int ItemCount;

        public LineDetails(int inlineLength,
            int crossLength,
            int startIndex,
            int itemCount)
        {
            InlineLength = inlineLength;
            CrossLength = crossLength;
            StartIndex = startIndex;
            ItemCount = itemCount;
        }
    }
    #endregion

    #region Instance Fields

    private IPaletteMetric? _paletteMetric;
    private PaletteMetricInt _metricGap;
    private List<LineDetails> _lineDetails;
    private Size[] _childSizes;
    private Size _maximumItem;
    private int _preferredOrientLength;

    #endregion

    #region Identity
    /// <summary>
    /// Initialise a new instance of the ViewLayoutBar class.
    /// </summary>
    /// <param name="itemSizing">Method used to calculate item size.</param>
    /// <param name="itemAlignment">Method used to align items within lines.</param>
    /// <param name="barMultiline">Multiline showing of items.</param>
    /// <param name="itemMinimumSize">Maximum allowed item size.</param>
    /// <param name="itemMaximumSize">Minimum allowed item size.</param>
    /// <param name="barMinimumHeight">Minimum height of the bar.</param>
    /// <param name="tabBorderStyle">Tab border style.</param>
    /// <param name="reorderSelectedLine">Should line with selection be reordered.</param>
    public ViewLayoutBar(BarItemSizing itemSizing,
        RelativePositionAlign itemAlignment,
        BarMultiline barMultiline,
        Size itemMinimumSize,
        Size itemMaximumSize,
        int barMinimumHeight,
        TabBorderStyle tabBorderStyle,
        bool reorderSelectedLine)
        : this(null, PaletteMetricInt.None, itemSizing,
            itemAlignment, barMultiline, itemMinimumSize,
            itemMaximumSize, barMinimumHeight, tabBorderStyle,
            reorderSelectedLine)
    {
    }

    /// <summary>
    /// Initialise a new instance of the ViewLayoutBar class.
    /// </summary>
    /// <param name="paletteMetric">Palette source for metric values.</param>
    /// <param name="metricGap">Metric for gap between each child item.</param>
    /// <param name="itemSizing">Method used to calculate item size.</param>
    /// <param name="itemAlignment">Method used to align items within lines.</param>
    /// <param name="barMultiline">Multiline showing of items.</param>
    /// <param name="itemMinimumSize">Maximum allowed item size.</param>
    /// <param name="itemMaximumSize">Minimum allowed item size.</param>
    /// <param name="barMinimumHeight">Minimum height of the bar.</param>
    /// <param name="reorderSelectedLine">Should line with selection be reordered.</param>
    public ViewLayoutBar(IPaletteMetric? paletteMetric,
        PaletteMetricInt metricGap,
        BarItemSizing itemSizing,
        RelativePositionAlign itemAlignment,
        BarMultiline barMultiline,
        Size itemMinimumSize,
        Size itemMaximumSize,
        int barMinimumHeight,
        bool reorderSelectedLine)
        : this(paletteMetric, metricGap, itemSizing,
            itemAlignment, barMultiline, itemMinimumSize,
            itemMaximumSize, barMinimumHeight,
            TabBorderStyle.RoundedOutsizeMedium,
            reorderSelectedLine)
    {
    }

    /// <summary>
    /// Initialise a new instance of the ViewLayoutBar class.
    /// </summary>
    /// <param name="paletteMetric">Palette source for metric values.</param>
    /// <param name="metricGap">Metric for gap between each child item.</param>
    /// <param name="itemSizing">Method used to calculate item size.</param>
    /// <param name="itemAlignment">Method used to align items within lines.</param>
    /// <param name="barMultiline">Multiline showing of items.</param>
    /// <param name="itemMinimumSize">Maximum allowed item size.</param>
    /// <param name="itemMaximumSize">Minimum allowed item size.</param>
    /// <param name="barMinimumHeight">Minimum height of the bar.</param>
    /// <param name="tabBorderStyle">Tab border style.</param>
    /// <param name="reorderSelectedLine">Should line with selection be reordered.</param>
    public ViewLayoutBar(IPaletteMetric? paletteMetric,
        PaletteMetricInt metricGap,
        BarItemSizing itemSizing,
        RelativePositionAlign itemAlignment,
        BarMultiline barMultiline,
        Size itemMinimumSize,
        Size itemMaximumSize,
        int barMinimumHeight,
        TabBorderStyle tabBorderStyle,
        bool reorderSelectedLine)
    {
        // Remember the source information
        _paletteMetric = paletteMetric;
        _metricGap = metricGap;
        BarItemSizing = itemSizing;
        ItemAlignment = itemAlignment;
        ItemMinimumSize = itemMinimumSize;
        ItemMaximumSize = itemMaximumSize;
        BarMinimumHeight = barMinimumHeight;
        TabBorderStyle = tabBorderStyle;
        BarMultiline = barMultiline;
        ReorderSelectedLine = reorderSelectedLine;

        // Default other state
        Orientation = VisualOrientation.Top;
        ItemOrientation = VisualOrientation.Top;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutBar:{Id}";

    #endregion

    #region ReorderSelectedLine
    /// <summary>
    /// Gets and sets the need to reorder the line with the selection.
    /// </summary>
    public bool ReorderSelectedLine { get; set; }

    #endregion

    #region BarItemSizing
    /// <summary>
    /// Gets and sets the method used to size bar items.
    /// </summary>
    public BarItemSizing BarItemSizing
    {
        [DebuggerStepThrough]
        get;
        set;
    }

    #endregion

    #region BarMinimumHeight
    /// <summary>
    /// Gets and sets the minimum height of the bar.
    /// </summary>
    public int BarMinimumHeight
    {
        [DebuggerStepThrough]
        get;
        set;
    }

    #endregion

    #region ItemMinimumSize
    /// <summary>
    /// Gets and sets the minimum size of item allowed.
    /// </summary>
    public Size ItemMinimumSize
    {
        [DebuggerStepThrough]
        get;
        set;
    }

    #endregion

    #region ItemMinimumSize
    /// <summary>
    /// Gets and sets the maximum size of item allowed.
    /// </summary>
    public Size ItemMaximumSize
    {
        [DebuggerStepThrough]
        get;
        set;
    }

    #endregion

    #region Orientation
    /// <summary>
    /// Gets and sets the bar orientation.
    /// </summary>
    public VisualOrientation Orientation
    {
        [DebuggerStepThrough]
        get;
        set;
    }

    #endregion

    #region ItemOrientation
    /// <summary>
    /// Gets and sets the item orientation.
    /// </summary>
    public VisualOrientation ItemOrientation
    {
        [DebuggerStepThrough]
        get;
        set;
    }

    #endregion

    #region ItemAlignment
    /// <summary>
    /// Gets and sets the item alignment.
    /// </summary>
    public RelativePositionAlign ItemAlignment
    {
        [DebuggerStepThrough]
        get;
        set;
    }

    #endregion

    #region BarMultiline
    /// <summary>
    /// Gets and sets a value indicating if multiple lines are allowed.
    /// </summary>
    public BarMultiline BarMultiline { get; set; }

    #endregion

    #region TabBorderStyle
    /// <summary>
    /// Gets and sets the tab border style to use when calculating item gaps.
    /// </summary>
    public TabBorderStyle TabBorderStyle { get; set; }

    #endregion

    #region SetMetrics
    /// <summary>
    /// Updates the metrics source and metric to use.
    /// </summary>
    /// <param name="paletteMetric">Source for acquiring metrics.</param>
    public void SetMetrics(IPaletteMetric? paletteMetric) => _paletteMetric = paletteMetric;

    /// <summary>
    /// Updates the metrics source and metric to use.
    /// </summary>
    /// <param name="paletteMetric">Palette source for metric values.</param>
    /// <param name="metricGap">Metric for gap between each child item.</param>
    public void SetMetrics(IPaletteMetric paletteMetric,
        PaletteMetricInt metricGap)
    {
        _paletteMetric = paletteMetric;
        _metricGap = metricGap;
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext? context)
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

        // Reset the largest child size to empty
        _maximumItem = Size.Empty;

        // Keep track of the total preferred size
        var preferredSize = Size.Empty;

        // Nothing to calculate if there are no children
        if (Count > 0)
        {
            // Default to no space between each child item
            // If we have a metric provider then get the child gap to use
            var gap = _paletteMetric?.GetMetricInt(null, State, _metricGap) ?? context.Renderer.RenderTabBorder.GetTabBorderSpacingGap(TabBorderStyle);

            // Line spacing gap can never be less than zero
            var lineGap = (gap < 0 ? 0 : gap);

            // Do we need to apply right to left by positioning children in reverse order?
            var reversed = (IsOneLine && !BarVertical && (context.Control!.RightToLeft == RightToLeft.Yes));

            // Allocate caching for size of each child element
            _childSizes = new Size[Count];

            // Find the child index of the selected page
            var selectedChildIndex = -1;

            // Find the size of each child in turn
            for (var i = 0; i < Count; i++)
            {
                // Get access to the indexed child
                ViewBase? child = this[reversed ? (Count - i - 1) : i];
                var checkItem = child as INavCheckItem;

                // Only examine visible children
                if (child!.Visible)
                {
                    // Cache child index of the selected page
                    if (checkItem!.Navigator.SelectedPage == checkItem.Page)
                    {
                        selectedChildIndex = i;
                    }

                    // Ask child for it's own preferred size
                    _childSizes[i] = child.GetPreferredSize(context);

                    // Enforce the minimum and maximum sizes
                    if (ItemVertical)
                    {
                        _childSizes[i].Width = Math.Max(Math.Min(_childSizes[i].Width, ItemMaximumSize.Height), ItemMinimumSize.Height);
                        _childSizes[i].Height = Math.Max(Math.Min(_childSizes[i].Height, ItemMaximumSize.Width), ItemMinimumSize.Width);
                    }
                    else
                    {
                        _childSizes[i].Width = Math.Max(Math.Min(_childSizes[i].Width, ItemMaximumSize.Width), ItemMinimumSize.Width);
                        _childSizes[i].Height = Math.Max(Math.Min(_childSizes[i].Height, ItemMaximumSize.Height), ItemMinimumSize.Height);
                    }

                    // Remember the largest child encountered
                    _maximumItem.Width = Math.Max(_childSizes[i].Width, _maximumItem.Width);
                    _maximumItem.Height = Math.Max(_childSizes[i].Height, _maximumItem.Height);
                }
            }

            // Apply the item sizing method
            switch (BarItemSizing)
            {
                case BarItemSizing.Individual:
                    // Do nothing, each item can be its own size
                    break;
                case BarItemSizing.SameHeight:
                    if (!BarVertical)
                    {
                        for (var i = 0; i < _childSizes.Length; i++)
                        {
                            if (!_childSizes[i].IsEmpty)
                            {
                                _childSizes[i].Height = _maximumItem.Height;
                            }
                        }
                    }
                    else
                    {
                        for (var i = 0; i < _childSizes.Length; i++)
                        {
                            if (!_childSizes[i].IsEmpty)
                            {
                                _childSizes[i].Width = _maximumItem.Width;
                            }
                        }
                    }
                    break;

                case BarItemSizing.SameWidth:
                    if (!BarVertical)
                    {
                        for (var i = 0; i < _childSizes.Length; i++)
                        {
                            if (!_childSizes[i].IsEmpty)
                            {
                                _childSizes[i].Width = _maximumItem.Width;
                            }
                        }
                    }
                    else
                    {
                        for (var i = 0; i < _childSizes.Length; i++)
                        {
                            if (!_childSizes[i].IsEmpty)
                            {
                                _childSizes[i].Height = _maximumItem.Height;
                            }
                        }
                    }
                    break;

                case BarItemSizing.SameWidthAndHeight:
                    for (var i = 0; i < _childSizes.Length; i++)
                    {
                        if (!_childSizes[i].IsEmpty)
                        {
                            _childSizes[i] = _maximumItem;
                        }
                    }
                    break;

                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(BarItemSizing.ToString());
                    break;
            }

            // Store a list of the individual line vectors (height or width depending on orientation)
            _lineDetails = new List<LineDetails>();

            var itemCount = 0;
            var startIndex = 0;
            var visibleItems = 0;

            if (BarVertical)
            {
                var yPos = 0;
                var yMaxPos = 0;
                var lineWidth = 0;
                _preferredOrientLength = 0;

                for (var i = 0; i < _childSizes.Length; i++)
                {
                    // Ignore invisible items, which are zero sized
                    if (!_childSizes[i].IsEmpty)
                    {
                        // If not the first visible item on line, then need a spacing gap
                        var yAdd = (visibleItems > 0) ? gap : 0;

                        // Add on the height of the child
                        yAdd += _childSizes[i].Height;

                        // Does this item extend beyond visible line? 
                        // (unless first item, we always have at least one item on a line)
                        if (!IsOneLine && (yPos > 0) && ((yPos + yAdd) > context.DisplayRectangle.Height))
                        {
                            // Remember the line metrics
                            _lineDetails.Add(new LineDetails(yPos, lineWidth, startIndex, itemCount));

                            // Track the widest line encountered
                            yMaxPos = Math.Max(yPos, yMaxPos);

                            // Reset back to start of the next line
                            yPos = 0;
                            itemCount = 0;
                            startIndex = i;
                            _preferredOrientLength += (lineGap + lineWidth);
                            lineWidth = 0;

                            // First item on new line does not need a spacing gap
                            yAdd = _childSizes[i].Height;
                        }

                        // Add on to the current line
                        yPos += yAdd;
                        visibleItems++;

                        // Track the tallest item on this line
                        if (lineWidth < _childSizes[i].Width)
                        {
                            lineWidth = _childSizes[i].Width;
                        }
                    }

                    // Visible and Invisible items are added to the item count
                    itemCount++;
                }

                // Add the last line to the height
                _preferredOrientLength += lineWidth;

                // Check if the last line is the tallest line
                yMaxPos = Math.Max(yPos, yMaxPos);

                // If we extended past end of the line
                if (yMaxPos > context.DisplayRectangle.Height)
                {
                    // If the mode requires we do not extend over the line
                    if (BarMultiline is BarMultiline.Shrinkline or BarMultiline.Exactline)
                    {
                        bool changed;

                        // Keep looping to reduce item sizes until all are zero sized or finished removing extra space
                        do
                        {
                            changed = false;

                            // Are there any items available for reducing?
                            if (visibleItems > 0)
                            {
                                // How much do we need to shrink each item by?
                                var shrink = Math.Max(1, (yMaxPos - context.DisplayRectangle.Height) / visibleItems);

                                // Reduce size of each item
                                for (var i = 0; i < _childSizes.Length; i++)
                                {
                                    // Cannot make smaller then zero height
                                    if (_childSizes[i].Height > 0)
                                    {
                                        // Reduce size
                                        var tempHeight = _childSizes[i].Height;
                                        _childSizes[i].Height -= shrink;

                                        // Prevent going smaller then zero
                                        if (_childSizes[i].Height <= 0)
                                        {
                                            _childSizes[i].Height = 0;
                                            visibleItems--;
                                        }

                                        // Reduce total width by the height removed from item
                                        yMaxPos -= (tempHeight - _childSizes[i].Height);

                                        // All reduction made, exit the loop
                                        if (yMaxPos <= context.DisplayRectangle.Height)
                                        {
                                            break;
                                        }

                                        changed = true;
                                    }
                                }
                            }
                        } while (changed && (yMaxPos > context.DisplayRectangle.Height));
                    }
                }

                // If we are shorter than the available height
                if (yMaxPos < context.DisplayRectangle.Height)
                {
                    // If the mode requires we extend to the end of the line
                    if (BarMultiline is BarMultiline.Expandline or BarMultiline.Exactline)
                    {
                        bool changed;

                        // Keep looping to expand item sizes until all extra space is allocated
                        do
                        {
                            changed = false;

                            // Are there any items available for expanding?
                            if (visibleItems > 0)
                            {
                                // How much do we need to expand each item by?
                                var expand = Math.Max(1, (context.DisplayRectangle.Height - yMaxPos) / visibleItems);

                                // Expand size of each item
                                for (var i = 0; i < _childSizes.Length; i++)
                                {
                                    // Expand size
                                    _childSizes[i].Height += expand;

                                    // Reduce free space by that allocated
                                    yMaxPos += expand;

                                    changed = true;

                                    // All expansion made, exit the loop
                                    if (yMaxPos >= context.DisplayRectangle.Height)
                                    {
                                        break;
                                    }
                                }
                            }
                        } while (changed && (yMaxPos < context.DisplayRectangle.Height));
                    }
                }

                // Remember the line metrics
                _lineDetails.Add(new LineDetails(yMaxPos, lineWidth, startIndex, itemCount));

                // Our preferred size is tall enough to show the longest line and total width
                preferredSize.Width = _preferredOrientLength;
                preferredSize.Height = yMaxPos;
            }
            else
            {
                var xPos = 0;
                var xMaxPos = 0;
                var lineHeight = 0;
                _preferredOrientLength = 0;

                for (var i = 0; i < _childSizes.Length; i++)
                {
                    // Ignore invisible items, which are zero sized
                    if (!_childSizes[i].IsEmpty)
                    {
                        // If not the first item on line, then need a spacing gap
                        var xAdd = (visibleItems > 0) ? gap : 0;

                        // Add on the width of the child
                        xAdd += _childSizes[i].Width;

                        // Does this item extend beyond visible line? 
                        // (unless first item, we always have at least one item on a line)
                        if (!IsOneLine && (xPos > 0) && ((xPos + xAdd) > context.DisplayRectangle.Width))
                        {
                            // Remember the line metrics
                            _lineDetails.Add(new LineDetails(xPos, lineHeight, startIndex, itemCount));

                            // Track the widest line encountered
                            xMaxPos = Math.Max(xPos, xMaxPos);

                            // Reset back to start of the next line
                            xPos = 0;
                            itemCount = 0;
                            startIndex = i;
                            _preferredOrientLength += (lineGap + lineHeight);
                            lineHeight = 0;

                            // First item on new line does not need a spacing gap
                            xAdd = _childSizes[i].Width;
                        }

                        // Add on to the current line
                        xPos += xAdd;
                        visibleItems++;

                        // Track the tallest item on this line
                        if (lineHeight < _childSizes[i].Height)
                        {
                            lineHeight = _childSizes[i].Height;
                        }
                    }

                    // Visible and Invisible items are added to the item count
                    itemCount++;
                }

                // Add the last line to the height
                _preferredOrientLength += lineHeight;

                // Check if the last line is the widest line
                xMaxPos = Math.Max(xPos, xMaxPos);

                // If we extended past end of the line
                if (xMaxPos > context.DisplayRectangle.Width)
                {
                    // If the mode requires we do not extend over the line
                    if (BarMultiline is BarMultiline.Shrinkline or BarMultiline.Exactline)
                    {
                        bool changed;

                        // Keep looping to reduce item sizes until all are zero sized or finished removing extra space
                        do
                        {
                            changed = false;

                            // Are there any items available for reducing?
                            if (visibleItems > 0)
                            {
                                // How much do we need to shrink each item by?
                                var shrink = Math.Max(1, (xMaxPos - context.DisplayRectangle.Width) / visibleItems);

                                // Reduce size of each item
                                for (var i = 0; i < _childSizes.Length; i++)
                                {
                                    // Cannot make smaller then zero width
                                    if (_childSizes[i].Width > 0)
                                    {
                                        // Reduce size
                                        var tempWidth = _childSizes[i].Width;
                                        _childSizes[i].Width -= shrink;

                                        // Prevent going smaller then zero
                                        if (_childSizes[i].Width <= 0)
                                        {
                                            _childSizes[i].Width = 0;
                                            visibleItems--;
                                        }

                                        // Reduce total width by the width removed from item
                                        xMaxPos -= (tempWidth - _childSizes[i].Width);

                                        // All reduction made, exit the loop
                                        if (xMaxPos <= context.DisplayRectangle.Width)
                                        {
                                            break;
                                        }

                                        changed = true;
                                    }
                                }
                            }
                        } while (changed && (xMaxPos > context.DisplayRectangle.Width));
                    }
                }

                // If we are shorter than the line width
                if (xMaxPos < context.DisplayRectangle.Width)
                {
                    // If the mode requires we extend to the end of the line
                    if (BarMultiline is BarMultiline.Expandline or BarMultiline.Exactline)
                    {
                        bool changed;

                        // Keep looping to expand item sizes until all the extra space is removed
                        do
                        {
                            changed = false;

                            // Are there any items available for reducing?
                            if (visibleItems > 0)
                            {
                                // How much do we need to expand each item by?
                                var expand = Math.Max(1, (context.DisplayRectangle.Width - xMaxPos) / visibleItems);

                                // Expand size of each item
                                for (var i = 0; i < _childSizes.Length; i++)
                                {
                                    // Expand size
                                    _childSizes[i].Width += expand;

                                    // Increase total width taken up by items
                                    xMaxPos += expand;

                                    changed = true;

                                    // All expansion made, exit the loop
                                    if (xMaxPos >= context.DisplayRectangle.Width)
                                    {
                                        break;
                                    }
                                }
                            }
                        } while (changed && (xMaxPos < context.DisplayRectangle.Width));
                    }
                }

                // Remember the line metrics
                _lineDetails.Add(new LineDetails(xMaxPos, lineHeight, startIndex, itemCount));

                // Our preferred size is tall enough to show the widest line and total height
                preferredSize.Width = xMaxPos;
                preferredSize.Height = _preferredOrientLength;
            }

            // Reverse the order of the lines when at top or left edge, as the 
            // items should be positioned from the inside edge moving outwards
            if (Orientation is VisualOrientation.Top or VisualOrientation.Left)
            {
                _lineDetails.Reverse();
            }

            // If we are using tabs then we need to move the line with the selection
            if (ReorderSelectedLine)
            {
                // Did we find a selected child index?
                if (selectedChildIndex >= 0)
                {
                    // Find the line details that contains this child index
                    for (var i = 0; i < _lineDetails.Count; i++)
                    {
                        // Is the selected item in the range of items for this line?
                        if ((selectedChildIndex >= _lineDetails[i].StartIndex) &&
                            (selectedChildIndex < (_lineDetails[i].StartIndex + _lineDetails[i].ItemCount)))
                        {
                            // Remove the line details
                            LineDetails ld = _lineDetails[i];
                            _lineDetails.RemoveAt(i);

                            if (Orientation is VisualOrientation.Top or VisualOrientation.Left)
                            {
                                // Move to end of the list
                                _lineDetails.Add(ld);
                            }
                            else
                            {
                                // Move to start of the list
                                _lineDetails.Insert(0, ld);
                            }
                        }
                    }
                }
            }
        }

        // Enforce the minimum height of the bar
        if (BarVertical)
        {
            preferredSize.Width = Math.Max(preferredSize.Width, BarMinimumHeight);
        }
        else
        {
            preferredSize.Height = Math.Max(preferredSize.Height, BarMinimumHeight);
        }

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
        ClientRectangle = context.DisplayRectangle;

        // Start laying out children from the top left

        // Nothing to calculate if there are no children
        if (Count > 0)
        {
            // Default to no space between each child item
            // If we have a metric provider then get the child gap to use
            var gap = _paletteMetric?.GetMetricInt(null, State, _metricGap) ?? context.Renderer.RenderTabBorder.GetTabBorderSpacingGap(TabBorderStyle);

            // Line spacing gap can never be less than zero
            var lineGap = (gap < 0 ? 0 : gap);

            var reverseAccess = false;
            var reversePosition = false;

            // Do we need to apply right to left by positioning children in reverse order?
            if (!BarVertical && (context.Control!.RightToLeft == RightToLeft.Yes))
            {
                if (IsOneLine)
                {
                    reverseAccess = true;
                }
                else
                {
                    reversePosition = true;
                }
            }

            if (BarVertical)
            {
                int xPos;

                // Ensure the left orientation is aligned towards right of bar area
                if (Orientation == VisualOrientation.Left)
                {
                    xPos = ClientLocation.X + Math.Max(0, ClientWidth - _preferredOrientLength);
                }
                else
                {
                    xPos = ClientLocation.X;
                }

                // Layout each line of buttons in turn
                foreach (LineDetails lineDetails in _lineDetails)
                {
                    // Get starting position for first button on the line
                    var yPos = FindStartingYPosition(context, lineDetails, reversePosition);

                    // Layout each button on the line
                    for (var i = 0; i < lineDetails.ItemCount; i++)
                    {
                        // Get the actual child index to use
                        var itemIndex = lineDetails.StartIndex + i;

                        // Ignore invisible items, which are zero sized
                        if (!_childSizes[itemIndex].IsEmpty)
                        {
                            // Get access to the indexed child
                            ViewBase? child = this[(reverseAccess ? (lineDetails.StartIndex + lineDetails.ItemCount) - 1 - i :
                                lineDetails.StartIndex + i)];

                            // Add on the height of the child
                            var yAdd = _childSizes[itemIndex].Height;

                            var xPosition = xPos;
                            var yPosition = (reversePosition ? yPos - _childSizes[itemIndex].Height : yPos);

                            // At the left edge, we need to ensure buttons are align by there right edges
                            if (Orientation == VisualOrientation.Left)
                            {
                                xPosition = (xPos + lineDetails.CrossLength) - _childSizes[itemIndex].Width;
                            }

                            // Create the rectangle that shows all of the check button
                            context.DisplayRectangle = new Rectangle(new Point(xPosition, yPosition), _childSizes[itemIndex]);

                            // Ask the child to layout
                            child?.Layout(context);

                            // Move to next child position
                            if (reversePosition)
                            {
                                yPos -= (yAdd + gap);
                            }
                            else
                            {
                                yPos += (yAdd + gap);
                            }
                        }
                    }

                    // Move across to the next line
                    xPos += (lineGap + lineDetails.CrossLength);
                }
            }
            else
            {
                int yPos;

                // Ensure the top orientation is aligned towards bottom of bar area
                if (Orientation == VisualOrientation.Top)
                {
                    yPos = ClientLocation.Y + Math.Max(0, ClientHeight - _preferredOrientLength);
                }
                else
                {
                    yPos = ClientLocation.Y;
                }

                // Layout each line of buttons in turn
                foreach (LineDetails lineDetails in _lineDetails)
                {
                    // Get starting position for first button on the line
                    var xPos = FindStartingXPosition(context, lineDetails, reversePosition);

                    // Layout each button on the line
                    for (var i = 0; i < lineDetails.ItemCount; i++)
                    {
                        // Get the actual child index to use
                        var itemIndex = lineDetails.StartIndex + i;

                        // Ignore invisible items, which are zero sized
                        if (!_childSizes[itemIndex].IsEmpty)
                        {
                            // Get access to the indexed child
                            ViewBase? child = this[(reverseAccess ? (lineDetails.StartIndex + lineDetails.ItemCount) - 1 - i :
                                lineDetails.StartIndex + i)];

                            // Add on the width of the child
                            var xAdd = _childSizes[itemIndex].Width;

                            var yPosition = yPos;
                            var xPosition = (reversePosition ? xPos - _childSizes[itemIndex].Width : xPos);

                            // At the top edge, we need to ensure buttons are align by there bottom edges
                            if (Orientation == VisualOrientation.Top)
                            {
                                yPosition = (yPos + lineDetails.CrossLength) - _childSizes[itemIndex].Height;
                            }

                            // Create the rectangle that shows all of the check button
                            context.DisplayRectangle = new Rectangle(new Point(xPosition, yPosition), _childSizes[itemIndex]);

                            // Ask the child to layout
                            child?.Layout(context);

                            // Move to next child position
                            if (reversePosition)
                            {
                                xPos -= (xAdd + gap);
                            }
                            else
                            {
                                xPos += (xAdd + gap);
                            }
                        }
                    }

                    // Move down to the next line
                    yPos += (lineGap + lineDetails.CrossLength);
                }
            }
        }

        // Put back the original display value now we have finished
        context.DisplayRectangle = ClientRectangle;
    }
    #endregion

    #region Implementation
    private bool BarVertical => Orientation is VisualOrientation.Left or VisualOrientation.Right;

    private bool ItemVertical => ItemOrientation is VisualOrientation.Left or VisualOrientation.Right;

    private bool IsOneLine => BarMultiline is BarMultiline.Singleline or BarMultiline.Shrinkline or BarMultiline.Expandline or BarMultiline.Exactline;

    private int FindStartingXPosition(ViewLayoutContext context,
        LineDetails lineDetails,
        bool reversePosition)
    {
        RelativePositionAlign align = ItemAlignment;

        // Do we need to apply right to left by aligning in opposite direction?
        if (IsOneLine && !BarVertical && (context.Control!.RightToLeft == RightToLeft.Yes))
        {
            switch (align)
            {
                case RelativePositionAlign.Near:
                    align = RelativePositionAlign.Far;
                    break;
                case RelativePositionAlign.Far:
                    align = RelativePositionAlign.Near;
                    break;
            }
        }

        switch (align)
        {
            case RelativePositionAlign.Near:
                return reversePosition ? ClientRectangle.Right : ClientLocation.X;

            case RelativePositionAlign.Center:
                return reversePosition
                    ? ClientRectangle.Right - ((ClientRectangle.Width - lineDetails.InlineLength) / 2)
                    : ClientLocation.X + ((ClientRectangle.Width - lineDetails.InlineLength) / 2);

            case RelativePositionAlign.Far:
                return reversePosition
                    ? ClientRectangle.Right - (ClientRectangle.Width - lineDetails.InlineLength)
                    : ClientLocation.X + (ClientRectangle.Width - lineDetails.InlineLength);

            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(align.ToString());
                return ClientLocation.X;
        }
    }

    private int FindStartingYPosition(ViewLayoutContext context,
        LineDetails lineDetails,
        bool reversePosition)
    {
        RelativePositionAlign align = ItemAlignment;

        // Do we need to apply right to left by aligning in opposite direction?
        if (IsOneLine && !BarVertical && (context.Control!.RightToLeft == RightToLeft.Yes))
        {
            switch (align)
            {
                case RelativePositionAlign.Near:
                    align = RelativePositionAlign.Far;
                    break;
                case RelativePositionAlign.Far:
                    align = RelativePositionAlign.Near;
                    break;
            }
        }

        switch (align)
        {
            case RelativePositionAlign.Near:
                return reversePosition ? ClientRectangle.Bottom : ClientLocation.Y;

            case RelativePositionAlign.Center:
                if (reversePosition)
                {
                    return ClientRectangle.Bottom - ((ClientRectangle.Height - lineDetails.InlineLength) / 2);
                }
                else
                {
                    return ClientLocation.Y + ((ClientRectangle.Height - lineDetails.InlineLength) / 2);
                }

            case RelativePositionAlign.Far:
                if (reversePosition)
                {
                    return ClientRectangle.Bottom - (ClientRectangle.Height - lineDetails.InlineLength);
                }
                else
                {
                    return ClientLocation.Y + (ClientRectangle.Height - lineDetails.InlineLength);
                }

            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(align.ToString());
                return ClientLocation.Y;
        }
    }
    #endregion
}