#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Extends the ViewComposite by creating and laying out elements to represent a ribbon group button cluster.
/// </summary>
internal class ViewLayoutRibbonGroupCluster : ViewComposite,
    IRibbonViewGroupItemView

{
    #region Type Definitions
    private class ItemToView : Dictionary<IRibbonGroupItem, ViewBase>;
    private class ViewToEdge : Dictionary<ViewBase, ViewDrawRibbonGroupClusterEdge>;
    private class ViewToSize : Dictionary<ViewBase, Size>;
    #endregion

    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private readonly KryptonRibbonGroupCluster _ribbonCluster;
    private ViewDrawRibbonDesignCluster _viewAddItem;
    private readonly ViewDrawRibbonGroupClusterSeparator _startSep;
    private readonly ViewDrawRibbonGroupClusterSeparator _endSep;
    private readonly PaletteBorderEdge _paletteBorderEdge;
    private PaletteRibbonShape? _lastShape;
    private readonly NeedPaintHandler? _needPaint;
    private ItemToView _itemToView;
    private ViewToEdge _viewToEdge;
    private readonly ViewToSize _viewToSizeMedium;
    private readonly ViewToSize _viewToSizeSmall;
    private GroupItemSize? _currentSize;
    private bool _startSepVisible;
    private bool _endSepVisible;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutRibbonGroupCluster class.
    /// </summary>
    /// <param name="ribbon">Owning ribbon control instance.</param>
    /// <param name="ribbonCluster">Reference to cluster definition.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ViewLayoutRibbonGroupCluster([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] KryptonRibbonGroupCluster? ribbonCluster,
        [DisallowNull] NeedPaintHandler? needPaint)
    {
        Debug.Assert(ribbon is not null);
        Debug.Assert(ribbonCluster is not null);
        Debug.Assert(needPaint is not null);

        // Cache references
        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));
        _ribbonCluster = ribbonCluster ?? throw new ArgumentNullException(nameof(ribbonCluster));
        _needPaint = needPaint ?? throw new ArgumentNullException(nameof(needPaint));
        _currentSize = GroupItemSize.Medium;

        // Associate the component with this view element for design time selection
        Component = _ribbonCluster;

        // Create the start and end separators
        _startSep = new ViewDrawRibbonGroupClusterSeparator(_ribbon, true);
        _endSep = new ViewDrawRibbonGroupClusterSeparator(_ribbon, false);
        _startSepVisible = false;
        _endSepVisible = false;

        // Create palette used to supply a width to a border edge view
        var borderEdgeRedirect = new PaletteBorderEdgeRedirect(_ribbon.StateCommon.RibbonGroupClusterButton.Border, needPaint);
        _paletteBorderEdge = new PaletteBorderEdge(borderEdgeRedirect, needPaint);
        _lastShape = PaletteRibbonShape.Office2007;

        // Use hashtable to store relationships
        _itemToView = new ItemToView();
        _viewToEdge = new ViewToEdge();
        _viewToSizeMedium = new ViewToSize();
        _viewToSizeSmall = new ViewToSize();

        // Hook into changes in the ribbon cluster definition
        _ribbonCluster.PropertyChanged += OnClusterPropertyChanged;
        _ribbonCluster.ClusterView = this;

        // At design time we want to track the mouse and show feedback
        if (_ribbon.InDesignMode)
        {
            var controller = new ViewHightlightController(this, needPaint);
            controller.ContextClick += OnContextClick;
            MouseController = controller;
        }
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutRibbonGroupCluster:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Must unhook to prevent memory leaks
            _ribbonCluster.PropertyChanged -= OnClusterPropertyChanged;
            _ribbonCluster.ClusterView = null;
        }

        base.Dispose(disposing);
    }
    #endregion

    #region GetFirstFocusItem
    /// <summary>
    /// Gets the first focus item from the container.
    /// </summary>
    /// <returns>ViewBase of item; otherwise false.</returns>
    public ViewBase GetFirstFocusItem()
    {
        ViewBase? view = null;

        // Scan all the children, which must be items
        foreach (ViewBase child in this)
        {
            // Only interested in visible children!
            if (child.Visible)
            {
                // Cast to correct type
                if (child is IRibbonViewGroupItemView item)
                {

                    // If it can provide a view, then use it
                    view = item.GetFirstFocusItem();
                    if (view != null)
                    {
                        break;
                    }
                }
            }
        }

        return view!;
    }
    #endregion

    #region GetLastFocusItem
    /// <summary>
    /// Gets the last focus item from the container.
    /// </summary>
    /// <returns>ViewBase of item; otherwise false.</returns>
    public ViewBase GetLastFocusItem()
    {
        ViewBase? view = null;

        // Scan all the children, which must be items
        foreach (ViewBase child in Reverse())
        {
            // Only interested in visible children!
            if (child.Visible)
            {
                // Cast to correct type
                if (child is IRibbonViewGroupItemView item)
                {

                    // If it can provide a view, then use it
                    view = item.GetLastFocusItem();
                    if (view != null)
                    {
                        break;
                    }
                }
            }
        }

        return view!;
    }
    #endregion

    #region GetNextFocusItem
    /// <summary>
    /// Gets the next focus item based on the current item as provided.
    /// </summary>
    /// <param name="current">The view that is currently focused.</param>
    /// <param name="matched">Has the current focus item been matched yet.</param>
    /// <returns>ViewBase of item; otherwise false.</returns>
    public ViewBase GetNextFocusItem(ViewBase current, ref bool matched)
    {
        ViewBase? view = null;

        // Scan all the children, which must be containers
        foreach (ViewBase child in this)
        {
            // Only interested in visible children!
            if (child.Visible)
            {
                // Cast to correct type
                if (child is IRibbonViewGroupItemView item)
                {
                    // If already matched, then we need to next item we find, 
                    // otherwise we are still looking for the next item
                    view = matched ? item.GetFirstFocusItem() : item.GetNextFocusItem(current, ref matched);

                    if (view != null)
                    {
                        break;
                    }
                }
            }
        }

        return view!;
    }
    #endregion

    #region GetPreviousFocusItem
    /// <summary>
    /// Gets the previous focus item based on the current item as provided.
    /// </summary>
    /// <param name="current">The view that is currently focused.</param>
    /// <param name="matched">Has the current focus item been matched yet.</param>
    /// <returns>ViewBase of item; otherwise false.</returns>
    public ViewBase GetPreviousFocusItem(ViewBase current, ref bool matched)
    {
        ViewBase? view = null;

        // Scan all the children, which must be containers
        foreach (ViewBase? child in Reverse())
        {
            // Only interested in visible children!
            if (child.Visible)
            {
                // Cast to correct type
                if (child is IRibbonViewGroupItemView item)
                {
                    // Already matched means we need the next item we come across,
                    // otherwise we continue with the attempt to find next
                    view = matched ? item.GetLastFocusItem() : item.GetPreviousFocusItem(current, ref matched);

                    if (view != null)
                    {
                        break;
                    }
                }
            }
        }

        return view!;
    }
    #endregion

    #region GetGroupKeyTips
    /// <summary>
    /// Gets the array of group level key tips.
    /// </summary>
    /// <param name="keyTipList">List to add new entries into.</param>
    /// <param name="lineHint">Provide hint to item about its location.</param>
    public void GetGroupKeyTips(KeyTipInfoList keyTipList, int lineHint)
    {
        // Scan all the children, which must be items
        foreach (ViewBase child in this)
        {
            // Only interested in visible children!
            if (child.Visible)
            {
                if (child is IRibbonViewGroupItemView item)
                {
                    item.GetGroupKeyTips(keyTipList, lineHint);
                }
            }
        }
    }
    #endregion

    #region StartSeparator
    /// <summary>
    /// Informs cluster if it needs a separator at the start.
    /// </summary>
    public bool StartSeparator
    {
        set => _startSepVisible = value;
    }
    #endregion

    #region EndSeparator
    /// <summary>
    /// Informs cluster if it needs a separator at the end.
    /// </summary>
    public bool EndSeparator
    {
        set => _endSepVisible = value;
    }
    #endregion

    #region Layout
    /// <summary>
    /// Override the group item size if possible.
    /// </summary>
    /// <param name="size">New size to use.</param>
    public void SetGroupItemSize(GroupItemSize size)
    {
        // Sync child elements to the current group items
        SyncChildrenToRibbonGroupItems();

        foreach (KryptonRibbonGroupItem item in _ribbonCluster.Items)
        {
            var viewItemSize = _itemToView[item] as IRibbonViewGroupItemView;
            viewItemSize?.SetGroupItemSize(size);
        }

        _currentSize = size;
    }

    /// <summary>
    /// Reset the group item size to the item definition.
    /// </summary>
    public void ResetGroupItemSize()
    {
        foreach (KryptonRibbonGroupItem item in _ribbonCluster.Items)
        {
            var viewItemSize = _itemToView[item] as IRibbonViewGroupItemView;
            viewItemSize?.ResetGroupItemSize();
        }

        // Our current size is based on the parent one
        var viewLines = Parent as ViewLayoutRibbonGroupLines;
        _currentSize = viewLines?.CurrentSize == GroupItemSize.Small ? GroupItemSize.Small : GroupItemSize.Medium;
    }

    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        // Sync child elements to the current group items
        SyncChildrenToRibbonGroupItems();

        ViewToSize? viewToSize;

        viewToSize = _currentSize == GroupItemSize.Small ? _viewToSizeSmall : _viewToSizeMedium;

        viewToSize.Clear();

        var preferredSize = Size.Empty;

        // Find total width and maximum height across all child elements
        for (var i = 0; i < Count; i++)
        {
            ViewBase? child = this[i];

            // Only interested in visible items
            if (child!.Visible)
            {
                // Cache preferred size of the child
                Size childSize = child.GetPreferredSize(context);

                // Cache the size for use in the layout
                viewToSize.Add(child, childSize);

                // Always add on to the width
                preferredSize.Width += childSize.Width;

                // Find maximum height encountered
                preferredSize.Height = Math.Max(preferredSize.Height, childSize.Height);
            }
        }

        // At design time we add space for the selection flap
        if (_ribbon.InDesignHelperMode)
        {
            preferredSize.Width += DesignTimeDraw.FlapWidth + DesignTimeDraw.SepWidth;
        }

        return preferredSize;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Store the provided client area
        ClientRectangle = context!.DisplayRectangle;

        // Define visible state of the separators
        _startSep.Visible = _startSepVisible && (_lastShape == PaletteRibbonShape.Office2010);
        _endSep.Visible = _endSepVisible && (_lastShape == PaletteRibbonShape.Office2010);

        // Are there any children to layout?
        if (Count > 0)
        {
            ViewToSize? viewToSize;

            viewToSize = _currentSize == GroupItemSize.Small ? _viewToSizeSmall : _viewToSizeMedium;

            var x = ClientLocation.X;
            var y = ClientLocation.Y;

            // At design time we reserve space at the left side for the selection flap
            if (_ribbon.InDesignHelperMode)
            {
                x += DesignTimeDraw.FlapWidth;
            }

            // Position each item from left/top to right/bottom 
            for (var i = 0; i < Count; i++)
            {
                ViewBase? child = this[i];

                // We only position visible items
                if (child!.Visible)
                {
                    // Cache preferred size of the child
                    Size childSize = viewToSize[child];

                    // Define display rectangle for the group
                    context.DisplayRectangle = new Rectangle(x, y, childSize.Width, ClientHeight);

                    // Position the element
                    this[i]?.Layout(context);

                    // Move across to next position
                    x += childSize.Width;
                }
            }
        }

        // Update the display rectangle we allocated for use by parent
        context.DisplayRectangle = ClientRectangle;
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore(RenderContext context)
    {
        // At design time we draw the selection flap
        if (_ribbon.InDesignHelperMode)
        {
            DesignTimeDraw.DrawFlapArea(_ribbon, context, ClientRectangle, State);
        }

        // Let base class draw contained items
        base.RenderBefore(context);
    }
    #endregion

    #region Protected
    /// <summary>
    /// Raises the NeedPaint event.
    /// </summary>
    /// <param name="needLayout">Does the palette change require a layout.</param>
    protected virtual void OnNeedPaint(bool needLayout)
    {
        if (_needPaint != null)
        {
            _needPaint(this, new NeedLayoutEventArgs(needLayout));

            if (needLayout)
            {
                _ribbon.PerformLayout();
            }
        }
    }
    #endregion

    #region Implementation
    private void SyncChildrenToRibbonGroupItems()
    {
        // Grab the shape of the ribbon
        _lastShape = _ribbon.RibbonShape;

        var itemEdgeVisible = _lastShape != PaletteRibbonShape.Office2010;
        var itemEdgeIgnoreNormal = _lastShape == PaletteRibbonShape.Office2010;
        var itemConstantBorder = _lastShape != PaletteRibbonShape.Office2010;
        var itemDrawNonTrackingAreas = _lastShape != PaletteRibbonShape.Office2010;

        // Remove all child elements
        Clear();

        // Always add the start separator as the first view element
        Add(_startSep);

        // Create new lookups which are up to date
        var regenView = new ItemToView();
        var regenEdge = new ViewToEdge();

        // Cache the first and last visible children
        ViewBase? viewFirst = null;
        ViewBase? viewLast = null;

        // Add a view element for each group item
        foreach (IRibbonGroupItem item in _ribbonCluster.Items)
        {
            ViewBase? itemView;
            ViewDrawRibbonGroupClusterEdge? itemEdge;

            // Do we already have a view for this item definition
            if (_itemToView.ContainsKey(item))
            {
                itemView = _itemToView[item];
                itemEdge = _viewToEdge[itemView];

                // Remove from lookups
                _itemToView.Remove(item);
                _viewToEdge.Remove(itemView);
            }
            else
            {
                // Ask the item definition to return an appropriate view
                itemView = item.CreateView(_ribbon, _needPaint!);

                // Create a border edge to go with the item view
                itemEdge = new ViewDrawRibbonGroupClusterEdge(_ribbon, _paletteBorderEdge);
            }

            // Update the visible state
            itemView.Visible = _ribbon.InDesignHelperMode || item.Visible;
            itemEdge.Visible = itemEdgeVisible && (_ribbon.InDesignHelperMode || item.Visible);

            // We need to remember associations
            regenView.Add(item, itemView);
            regenEdge.Add(itemView, itemEdge);

            Add(itemView);
            Add(itemEdge);

            // Update the cached first/last items
            if (itemView.Visible && (viewFirst == null))
            {
                viewFirst = itemView;
            }

            if (itemView.Visible)
            {
                viewLast = itemView;
            }
        }

        // Update the display borders for the visible items
        foreach (ViewBase item in regenView.Values)
        {
            // Only interested in visible items
            if (item.Visible)
            {
                if ((item is ViewDrawRibbonGroupClusterButton or ViewDrawRibbonGroupClusterColorButton))
                {
                    // By default each button shows only the top and bottom
                    PaletteDrawBorders maxBorders;

                    switch (_lastShape)
                    {
                        default:
                        case PaletteRibbonShape.Office2007:
                            maxBorders = PaletteDrawBorders.TopBottom;

                            // First and last items have extra borders
                            if (item == viewFirst)
                            {
                                // If first and last, it needs all borders
                                maxBorders = item == viewLast ? PaletteDrawBorders.All : PaletteDrawBorders.TopBottomLeft;
                            }
                            else if (item == viewLast)
                            {
                                maxBorders = PaletteDrawBorders.TopBottomRight;
                            }
                            break;
                        case PaletteRibbonShape.Office2010:
                            maxBorders = PaletteDrawBorders.All;
                            break;
                    }

                    // Remove the border edge after the last button
                    if (item == viewLast)
                    {
                        Remove(regenEdge[item]);
                    }

                    switch (item)
                    {
                        case ViewDrawRibbonGroupClusterButton clusterButton:
                            clusterButton.MaxBorderEdges = maxBorders;
                            clusterButton.BorderIgnoreNormal = itemEdgeIgnoreNormal;
                            clusterButton.ConstantBorder = itemConstantBorder;
                            clusterButton.DrawNonTrackingAreas = itemDrawNonTrackingAreas;
                            break;
                        case ViewDrawRibbonGroupClusterColorButton clusterColorButton:
                            clusterColorButton.MaxBorderEdges = maxBorders;
                            clusterColorButton.BorderIgnoreNormal = itemEdgeIgnoreNormal;
                            clusterColorButton.ConstantBorder = itemConstantBorder;
                            clusterColorButton.DrawNonTrackingAreas = itemDrawNonTrackingAreas;
                            break;
                    }
                }
            }
        }

        // Dispose of all the items no longer needed
        foreach (ViewBase view in _itemToView.Values)
        {
            view.Dispose();
        }

        foreach (var view in _viewToEdge.Values)
        {
            view.Dispose();
        }

        // Always add the end separator as the last view element (excluding any design time additions)
        Add(_endSep);

        // Define visible state of the separators
        _startSep.Visible = _lastShape == PaletteRibbonShape.Office2010;
        _endSep.Visible = _lastShape == PaletteRibbonShape.Office2010;

        // When in design time help mode
        if (_ribbon.InDesignHelperMode)
        {
            // Create the design time 'Item' first time it is needed
            _viewAddItem ??= new ViewDrawRibbonDesignCluster(_ribbon,
                _ribbonCluster,
                _needPaint!);

            // Always add at end of the list of items
            Add(_viewAddItem);
        }

        // Use the latest tables
        _itemToView = regenView;
        _viewToEdge = regenEdge;
    }

    private void OnClusterPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        var updateLayout = false;

        switch (e.PropertyName)
        {
            case nameof(Visible):
                updateLayout = true;
                break;
        }

        if (updateLayout)
        {
            // If we are on the currently selected tab then...
            if ((_ribbonCluster.RibbonTab != null) &&
                (_ribbon.SelectedTab == _ribbonCluster.RibbonTab))
            {
                // ...layout so the visible change is made
                OnNeedPaint(true);
            }
        }
    }

    private void OnContextClick(object? sender, MouseEventArgs e)
    {
        if (_ribbon.InDesignMode)
        {
            _ribbonCluster.OnDesignTimeContextMenu(e);
        }
    }
    #endregion
}