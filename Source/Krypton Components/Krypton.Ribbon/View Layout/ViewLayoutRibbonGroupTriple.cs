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
/// Extends the ViewComposite by creating and laying out elements to represent ribbon group triple.
/// </summary>
internal class ViewLayoutRibbonGroupTriple : ViewComposite,
    IRibbonViewGroupContainerView
{
    #region Type Definitions
    private class ItemToView : Dictionary<IRibbonGroupItem, ViewBase>;
    private class ViewToSize : Dictionary<ViewBase, Size>;
    #endregion

    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private readonly KryptonRibbonGroupTriple _ribbonTriple;
    private ViewDrawRibbonDesignGroupTriple _viewAddItem;
    private readonly NeedPaintHandler _needPaint;
    private GroupItemSize _currentSize;
    private ItemToView _itemToView;
    private readonly ViewToSize _smallCache;
    private readonly ViewToSize _mediumCache;
    private readonly ViewToSize _largeCache;
    private int _smallWidest;
    private int _mediumWidest;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutRibbonGroupTriple class.
    /// </summary>
    /// <param name="ribbon">Owning ribbon control instance.</param>
    /// <param name="ribbonTriple">Reference to triple definition.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ViewLayoutRibbonGroupTriple([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] KryptonRibbonGroupTriple? ribbonTriple,
        [DisallowNull] NeedPaintHandler? needPaint)
    {
        Debug.Assert(ribbon is not null);
        Debug.Assert(ribbonTriple is not null);
        Debug.Assert(needPaint is not null);

        // Cache references
        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));
        _ribbonTriple = ribbonTriple ?? throw new ArgumentNullException(nameof(ribbonTriple));
        _needPaint = needPaint ?? throw new ArgumentNullException(nameof(needPaint));

        // Associate the component with this view element for design time selection
        Component = _ribbonTriple;

        // Use hashtables to store relationships
        _itemToView = new ItemToView();
        _smallCache = new ViewToSize();
        _mediumCache = new ViewToSize();
        _largeCache = new ViewToSize();

        // Get the initial size used for sizing and positioning
        SetCurrentSize(ribbonTriple!.ItemSizeCurrent);

        // Hook into changes in the ribbon triple definition
        _ribbonTriple!.PropertyChanged += OnTriplePropertyChanged;
        _ribbonTriple.TripleView = this;

        // At design time we want to track the mouse and show feedback
        if (_ribbon!.InDesignMode)
        {
            var controller = new ViewHightlightController(this, needPaint!);
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
        $"ViewLayoutRibbonGroupTriple:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Must unhook to prevent memory leaks
            _ribbonTriple.PropertyChanged -= OnTriplePropertyChanged;
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

        // Scan all the children, which must be items
        foreach (ViewBase child in this)
        {
            // Only interested in visible children!
            if (child.Visible)
            {
                // Cast to correct type
                if (child is IRibbonViewGroupItemView item)
                {
                    // Already matched means we need the next item we come across,
                    // otherwise we continue with the attempt to find next
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
        foreach (ViewBase child in Reverse())
        {
            // Only interested in visible children!
            if (child.Visible)
            {
                // Cast to correct type
                if (child is IRibbonViewGroupItemView item)
                {
                    // Already matched means we need the next item we come across,
                    // otherwise we continue with the attempt to find previous
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
    public void GetGroupKeyTips(KeyTipInfoList keyTipList)
    {
        // Scan all the children, which must be items
        foreach (ViewBase child in this)
        {
            // Only interested in visible children!
            if (child.Visible)
            {
                if (child is IRibbonViewGroupItemView item)
                {
                    item.GetGroupKeyTips(keyTipList, IndexOf(child) + 1);
                }
            }
        }
    }
    #endregion

    #region Layout
    /// <summary>
    /// Gets an array of the allowed possible sizes of the container.
    /// </summary>
    /// <param name="context">Context used to calculate the sizes.</param>
    /// <returns>Array of size values.</returns>
    public ItemSizeWidth[] GetPossibleSizes(ViewLayoutContext context)
    {
        // Sync child elements to the current group items
        SyncChildrenToRibbonGroupItems();

        // Create a list of results
        var results = new List<ItemSizeWidth>();

        // Are we allowed to be in the large size?
        if (_ribbonTriple.ItemSizeMaximum == GroupItemSize.Large)
        {
            ApplySize(GroupItemSize.Large);
            results.Add(new ItemSizeWidth(GroupItemSize.Large, GetPreferredSize(context).Width));
        }

        // Are we allowed to be in the medium size?
        if (((int)_ribbonTriple.ItemSizeMaximum >= (int)GroupItemSize.Medium) &&
            ((int)_ribbonTriple.ItemSizeMinimum <= (int)GroupItemSize.Medium))
        {
            ApplySize(GroupItemSize.Medium);
            var mediumWidth = new ItemSizeWidth(GroupItemSize.Medium, GetPreferredSize(context).Width);

            if (_ribbon.InDesignHelperMode)
            {
                // Only add if we are the first calculation, as in design mode we
                // always provide a single possible size which is the largest item
                if (results.Count == 0)
                {
                    results.Add(mediumWidth);
                }
            }
            else
            {
                // Only add the medium size if there is no other entry or we are
                // smaller than the existing size and so represent a useful shrinkage
                if ((results.Count == 0) || (results[0].Width > mediumWidth.Width))
                {
                    results.Add(mediumWidth);
                }
            }
        }

        // Are we allowed to be in the small size?
        if (_ribbonTriple.ItemSizeMinimum == GroupItemSize.Small)
        {
            ApplySize(GroupItemSize.Small);
            var smallWidth = new ItemSizeWidth(GroupItemSize.Small, GetPreferredSize(context).Width);

            if (_ribbon.InDesignHelperMode)
            {
                // Only add if we are the first calculation, as in design mode we
                // always provide a single possible size which is the largest item
                if (results.Count == 0)
                {
                    results.Add(smallWidth);
                }
            }
            else
            {
                // Only add the small size if there is no other entry or we are
                // smaller than the existing size and so represent a useful shrinkage
                if ((results.Count == 0) || (results[results.Count - 1].Width > smallWidth.Width))
                {
                    results.Add(smallWidth);
                }
            }
        }

        // Ensure original value is put back
        ResetSize();

        return results.ToArray();
    }

    /// <summary>
    /// Update the group with the provided sizing solution.
    /// </summary>
    /// <param name="size">Value for the container.</param>
    public void SetSolutionSize(ItemSizeWidth size) =>
        // Update the container definition, which itself will then
        // update all the child items inside the container for us
        _ribbonTriple.ItemSizeCurrent = size.GroupItemSize;

    /// <summary>
    /// Reset the container back to its requested size.
    /// </summary>
    public void ResetSolutionSize()
    {
        // Restore the container back to the defined size
        _ribbonTriple.ItemSizeCurrent = _ribbonTriple.ItemSizeMaximum;
        ApplySize(_ribbonTriple.ItemSizeCurrent);
    }

    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        // Clear down the preferred size caches
        switch (_currentSize)
        {
            case GroupItemSize.Small:
                _smallCache.Clear();
                _smallWidest = 0;
                break;
            case GroupItemSize.Medium:
                _mediumCache.Clear();
                _mediumWidest = 0;
                break;
            case GroupItemSize.Large:
                _largeCache.Clear();
                break;
        }

        // Sync child elements to the current group items
        SyncChildrenToRibbonGroupItems();

        var preferredSize = Size.Empty;

        // Are we sizing horizontal or vertical?
        var horizontal = _currentSize == GroupItemSize.Large;

        // Find total width and maximum height across all child elements
        for (var i = 0; i < Count; i++)
        {
            ViewBase? child = this[i];

            // Only interested in visible items
            if (child!.Visible)
            {
                // Cache preferred size of the child
                Size childSize = child.GetPreferredSize(context);

                // Add into the size cache for use in layout code
                switch (_currentSize)
                {
                    case GroupItemSize.Small:
                        _smallCache.Add(child, childSize);
                        _smallWidest = Math.Max(_smallWidest, childSize.Width);
                        break;
                    case GroupItemSize.Medium:
                        _mediumCache.Add(child, childSize);
                        _mediumWidest = Math.Max(_mediumWidest, childSize.Width);
                        break;
                    case GroupItemSize.Large:
                        _largeCache.Add(child, childSize);
                        break;
                }

                if (horizontal)
                {
                    // If not the first item positioned
                    if (preferredSize.Width > 0)
                    {
                        // Add on a single pixel spacing gap
                        preferredSize.Width++;
                    }

                    // Always add on to the width
                    preferredSize.Width += childSize.Width;

                    // Find maximum height encountered
                    preferredSize.Height = Math.Max(preferredSize.Height, childSize.Height);
                }
                else
                {
                    // Always add on to the width
                    preferredSize.Height += childSize.Height;

                    // Find maximum height encountered
                    preferredSize.Width = Math.Max(preferredSize.Width, childSize.Width);
                }
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

        // Are we sizing horizontal or vertical?
        var horizontal = _currentSize == GroupItemSize.Large;
        var widest = _currentSize == GroupItemSize.Small ? _smallWidest : _mediumWidest;

        // Are there any children to layout?
        if (Count > 0)
        {
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
                    // Get the cached size of this view
                    var childSize = Size.Empty;
                    switch (_currentSize)
                    {
                        case GroupItemSize.Small:
                            childSize = _smallCache[child];
                            break;
                        case GroupItemSize.Medium:
                            childSize = _mediumCache[child];
                            break;
                        case GroupItemSize.Large:
                            childSize = _largeCache[child];
                            break;
                    }

                    if (horizontal)
                    {
                        // Define display rectangle for the group
                        context.DisplayRectangle = new Rectangle(x, y, childSize.Width, ClientHeight);

                        // Position the element
                        this[i]?.Layout(context);

                        // Move across to next position (add 1 extra as the spacing gap)
                        x += childSize.Width + 1;
                    }
                    else
                    {
                        // Define display rectangle for the group
                        switch (_ribbonTriple.ItemAlignment)
                        {
                            case RibbonItemAlignment.Near:
                                context.DisplayRectangle = new Rectangle(x, y, childSize.Width, childSize.Height);
                                break;
                            case RibbonItemAlignment.Center:
                                context.DisplayRectangle = new Rectangle(x + ((widest - childSize.Width) / 2), y, childSize.Width, childSize.Height);
                                break;
                            case RibbonItemAlignment.Far:
                                context.DisplayRectangle = new Rectangle(x + widest - childSize.Width, y, childSize.Width, childSize.Height);
                                break;
                        }

                        // Position the element
                        this[i]?.Layout(context);

                        // Move down to next position
                        y += childSize.Height;
                    }
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
    private void ApplySize(GroupItemSize size)
    {
        foreach (ViewBase item in this)
        {
            if (item is IRibbonViewGroupItemView viewItem)
            {
                viewItem.SetGroupItemSize(size);
            }
        }

        SetCurrentSize(size);
    }

    private void ResetSize()
    {
        foreach (ViewBase item in this)
        {
            if (item is IRibbonViewGroupItemView viewItem)
            {
                viewItem.ResetGroupItemSize();
            }
        }

        SetCurrentSize(_ribbonTriple.ItemSizeCurrent);
    }

    private void SetCurrentSize(GroupItemSize size)
    {
        _currentSize = size;

        if (_viewAddItem != null)
        {
            _viewAddItem.CurrentSize = size;
        }
    }

    private void SyncChildrenToRibbonGroupItems()
    {
        // Remove all child elements
        Clear();

        var regenerate = new ItemToView();

        // Add a view element for each group item
        foreach (IRibbonGroupItem item in _ribbonTriple.Items!)
        {
            ViewBase itemView;

            // Do we already have a view for this item definition
            if (_itemToView.ContainsKey(item))
            {
                itemView = _itemToView[item];

                // Remove from lookup as we do not want to delete this view
                _itemToView.Remove(item);
            }
            else
            {
                // Ask the item definition to return an appropriate view
                itemView = item.CreateView(_ribbon, _needPaint);
            }

            // Update the visible state of the item
            itemView.Visible = _ribbon.InDesignHelperMode || item.Visible;

            // We need to keep this association
            regenerate.Add(item, itemView);

            Add(itemView);
        }

        // When in design time help mode and there is room for another item
        if (_ribbon.InDesignHelperMode && (Count < 3))
        {
            // Create the design time 'Add Tab' first time it is needed
            _viewAddItem ??= new ViewDrawRibbonDesignGroupTriple(_ribbon, 
                _ribbonTriple, 
                _currentSize,
                _needPaint);

            // Always add at end of the list of items
            Add(_viewAddItem);
        }

        // Dispose of all the items no longer needed
        foreach (ViewBase view in _itemToView.Values)
        {
            view.Dispose();
        }

        // Use the latest hashtable
        _itemToView = regenerate;
    }

    private void OnTriplePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        var updateLayout = false;

        switch (e.PropertyName)
        {
            case nameof(Visible):
            case "ItemAlignment":
                updateLayout = true;
                break;
            case "ItemSizeMinimum":
            case "ItemSizeMaximum":
            case "ItemSizeCurrent":
                // Update with the latest sizing value
                SetCurrentSize(_ribbonTriple.ItemSizeCurrent);
                updateLayout = true;
                break;
        }

        if (updateLayout)
        {
            // If we are on the currently selected tab then...
            if ((_ribbonTriple.RibbonTab != null) &&
                (_ribbon.SelectedTab == _ribbonTriple.RibbonTab))
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
            _ribbonTriple.OnDesignTimeContextMenu(e);
        }
    }
    #endregion
}