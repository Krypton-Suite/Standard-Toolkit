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
/// Extends the ViewComposite by creating and laying out elements to represent ribbon groups.
/// </summary>
internal class ViewLayoutRibbonGroups : ViewComposite
{
    #region Classes
    private class GroupToView : Dictionary<KryptonRibbonGroup, ViewDrawRibbonGroup>;
    private class ViewDrawRibbonGroupSepList : List<ViewLayoutRibbonSeparator>;
    #endregion

    #region Statis Fields

    private const int SEP_LENGTH_2007 = 2;
    private const int SEP_LENGTH_2010 = 0;

    #endregion

    #region Instance Fields
    private readonly KryptonRibbon? _ribbon;
    private readonly KryptonRibbonTab _ribbonTab;
    private NeedPaintHandler _needPaint;
    private ViewDrawRibbonDesignGroup _viewAddGroup;
    private GroupToView _groupToView;
    private readonly ViewDrawRibbonGroupSepList _groupSepCache;
    private int[] _groupWidths;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutRibbonGroups class.
    /// </summary>
    /// <param name="ribbon">Owning ribbon control instance.</param>
    /// <param name="ribbonTab">RibbonTab to organize groups.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ViewLayoutRibbonGroups([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] KryptonRibbonTab? ribbonTab,
        [DisallowNull] NeedPaintHandler? needPaint)
    {
        Debug.Assert(ribbon is not null);
        Debug.Assert(ribbonTab is not null);
        Debug.Assert(needPaint is not null);

        // Cache references
        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));
        _ribbonTab = ribbonTab ?? throw new ArgumentNullException(nameof(ribbonTab));
        _needPaint = needPaint ?? throw new ArgumentNullException(nameof(needPaint));

        // Create initial lookup table
        _groupToView = new GroupToView();

        // Create cache of group separator elements
        _groupSepCache = [];
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutRibbonGroups:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Clear();

            foreach (ViewDrawRibbonGroup ribGroup in _groupToView.Values)
            {
                ribGroup.Dispose();
            }

            foreach (ViewLayoutRibbonSeparator sep in _groupSepCache)
            {
                sep.Dispose();
            }

            _groupToView.Clear();
            _groupSepCache.Clear();
        }

        base.Dispose(disposing);
    }
    #endregion

    #region NeedPaintDelegate
    /// <summary>
    /// Set the new paint delegate to use for painting requests.
    /// </summary>
    public NeedPaintHandler NeedPaintDelegate
    {
        set => _needPaint = value;
    }
    #endregion

    #region ViewGroupFromPoint
    /// <summary>
    /// Gets the view element group that the provided point is inside.
    /// </summary>
    /// <param name="pt">Mouse point.</param>
    /// <returns>Reference if inside a group; otherwise null.</returns>
    public ViewDrawRibbonGroup? ViewGroupFromPoint(Point pt)
    {
        // Parent element should be a view layout
        var layoutControl = Parent as ViewLayoutControl;

        // Get the location of the child control it contains
        Point layoutLocation = layoutControl!.ChildControl!.Location;

        // Adjust the incoming point for the location of the child control
        pt.X -= layoutLocation.X;
        pt.Y -= layoutLocation.Y;

        // Search the child collection for matching group elements
        foreach (ViewBase child in this)
        {
            // Ignore hidden elements
            if (child.Visible)
            {

                // Only interested in group instances (not separators or others)
                if (child is ViewDrawRibbonGroup ribGroup)
                {
                    // Does this group match?
                    if (ribGroup.ClientRectangle.Contains(pt))
                    {
                        return ribGroup;
                    }
                }
            }
        }

        return null;
    }
    #endregion

    #region GetGroupKeyTips
    /// <summary>
    /// Gets the array of group level key tips.
    /// </summary>
    /// <returns>Array of KeyTipInfo; otherwise null.</returns>
    public KeyTipInfo[] GetGroupKeyTips()
    {
        var keyTipList = new KeyTipInfoList();

        // Ask each visible group to add its own key tips
        foreach (ViewDrawRibbonGroup ribGroup in _groupToView.Values)
        {
            if (ribGroup.Visible)
            {
                ribGroup.GetGroupKeyTips(keyTipList);
            }
        }

        return keyTipList.ToArray();
    }
    #endregion

    #region GetFirstFocusItem
    /// <summary>
    /// Gets the first focus item from the groups.
    /// </summary>
    /// <returns>ViewBase of item; otherwise false.</returns>
    public ViewBase GetFirstFocusItem()
    {
        ViewBase? view = null;

        // Search each group until one of them returns a focus item
        foreach (ViewDrawRibbonGroup group in _groupToView.Values)
        {
            view = group.GetFirstFocusItem();
            if (view != null)
            {
                break;
            }
        }

        return view!;
    }
    #endregion

    #region GetLastFocusItem
    /// <summary>
    /// Gets the last focus item from the groups.
    /// </summary>
    /// <returns>ViewBase of item; otherwise false.</returns>
    public ViewBase GetLastFocusItem()
    {
        ViewBase? view = null;

        var groups = new ViewDrawRibbonGroup[_groupToView.Count];
        _groupToView.Values.CopyTo(groups, 0);

        // Search each group until one of them returns a focus item
        for (var i = groups.Length - 1; i >= 0; i--)
        {
            view = groups[i].GetLastFocusItem();
            if (view != null)
            {
                break;
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
    /// <returns>ViewBase of item; otherwise false.</returns>
    public ViewBase GetNextFocusItem(ViewBase current)
    {
        ViewBase? view = null;
        var matched = false;

        // Search each group until one of them returns a focus item
        foreach (ViewDrawRibbonGroup ribGroup in _groupToView.Values)
        {
            // Already matched means we need the next item we come across,
            // otherwise we continue with the attempt to find next
            view = matched ? ribGroup.GetFirstFocusItem() : ribGroup.GetNextFocusItem(current, ref matched);

            if (view != null)
            {
                break;
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
    /// <returns>ViewBase of item; otherwise false.</returns>
    public ViewBase GetPreviousFocusItem(ViewBase current)
    {
        ViewBase? view = null;
        var matched = false;

        var groups = new ViewDrawRibbonGroup[_groupToView.Count];
        _groupToView.Values.CopyTo(groups, 0);

        // Search each group until one of them returns a focus item
        for (var i = groups.Length - 1; i >= 0; i--)
        {
            // Already matched means we need the next item we come across,
            // otherwise we continue with the attempt to find previous
            view = matched 
                ? groups[i].GetLastFocusItem() 
                : groups[i].GetPreviousFocusItem(current, ref matched);

            if (view != null)
            {
                break;
            }
        }

        return view!;
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        // Sync to represent the current ribbon groups for tab
        SyncChildrenToRibbonGroups();

        // Find best size for groups to fill available space
        return new Size(AdjustGroupStateToMatchSpace(context), _ribbon!.CalculatedValues.GroupHeight);
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;

        var x = ClientLocation.X;

        // Are there any children to layout?
        if (Count > 0)
        {
            var y = ClientLocation.Y;
            var height = ClientHeight;

            // Position each item from left to right taking up entire height
            for (int i = 0, j = 0; i < Count; i++)
            {
                ViewBase? child = this[i];

                // We only position visible items
                if (child!.Visible)
                {
                    // Cache preferred size of the child

                    // If a group then pull in the cached value
                    Size childSize = child is ViewDrawRibbonGroup
                        ? new Size(_groupWidths[j++], _ribbon!.CalculatedValues.GroupHeight)
                        : this[i]!.GetPreferredSize(context);

                    // Only interested in items with some width
                    if (childSize.Width > 0)
                    {
                        // Define display rectangle for the group
                        context.DisplayRectangle = new Rectangle(x, y, childSize.Width, height);

                        // Position the element
                        this[i]?.Layout(context);

                        // Move across to next position
                        x += childSize.Width;
                    }
                }
            }
        }

        // Update our own size to reflect how wide we actually need to be for all the children
        ClientRectangle = new Rectangle(ClientLocation, new Size(x - ClientLocation.X, ClientHeight));

        // Update the display rectangle we allocated for use by parent
        context.DisplayRectangle = new Rectangle(ClientLocation, new Size(x - ClientLocation.X, ClientHeight));
    }
    #endregion

    #region Implementation
    private Size SeparatorSize
    {
        get
        {
            var retSize = Size.Empty;

            if (_ribbon != null)
            {
                retSize = _ribbon.RibbonShape switch
                {
                    PaletteRibbonShape.Office2010 => new Size(SEP_LENGTH_2010, SEP_LENGTH_2010),
                    _ => new Size(SEP_LENGTH_2007, SEP_LENGTH_2007)
                };
            }

            return retSize;
        }
    }

    private void SyncChildrenToRibbonGroups()
    {
        // Remove all child elements
        Clear();

        // Create a new lookup that reflects any changes in groups
        var regenerate = new GroupToView();

        // Make sure we have a view element to match each group
        foreach (KryptonRibbonGroup ribGroup in _ribbonTab.Groups)
        {
            ViewDrawRibbonGroup? view = null;

            // Get the currently cached view for the group
            if (_groupToView.TryGetValue(ribGroup, out ViewDrawRibbonGroup? value))
            {
                view = value;
            }

            // If a new group, create a view for it now
            view ??= new ViewDrawRibbonGroup(_ribbon!, ribGroup, _needPaint);

            // Add to the lookup for future reference
            regenerate.Add(ribGroup, view);
        }

        if (_groupSepCache.Count < _ribbonTab.Groups.Count)
        {
            for (var i = _groupSepCache.Count; i < _ribbonTab.Groups.Count; i++)
            {
                _groupSepCache.Add(new ViewLayoutRibbonSeparator(0, true));
            }
        }

        // Update size of all separators to match ribbon shape
        Size sepSize = SeparatorSize;
        foreach (ViewLayoutRibbonSeparator sep in _groupSepCache)
        {
            sep.SeparatorSize = sepSize;
        }

        // We ignore the first separator
        var ignoreSep = true;

        // Add child elements appropriate for each ribbon group
        for (var i = 0; i < _ribbonTab.Groups.Count; i++)
        {
            KryptonRibbonGroup ribbonGroup = _ribbonTab.Groups[i];

            // Only make the separator visible if the group is and not the first sep
            var groupVisible = _ribbon!.InDesignHelperMode || ribbonGroup.Visible;
            _groupSepCache[i].Visible = groupVisible && !ignoreSep;
            regenerate[ribbonGroup].Visible = groupVisible;

            // Only add a separator for the second group onwards
            if (groupVisible && ignoreSep)
            {
                ignoreSep = false;
            }

            Add(_groupSepCache[i]);
            Add(regenerate[ribbonGroup]);

            // Remove entries we still are using
            if (_groupToView.ContainsKey(ribbonGroup))
            {
                _groupToView.Remove(ribbonGroup);
            }
        }

        // When in design time help mode
        if (_ribbon is { InDesignHelperMode: true })
        {
            // Create the design time 'Add Group' first time it is needed
            _viewAddGroup ??= new ViewDrawRibbonDesignGroup(_ribbon, _needPaint);

            // Always add at end of the list of groups
            Add(_viewAddGroup);
        }

        // Dispose of views no longer required
        foreach (ViewDrawRibbonGroup ribGroup in _groupToView.Values)
        {
            ribGroup.Dispose();
        }

        // No longer need the old lookup
        _groupToView = regenerate;
    }

    private int AdjustGroupStateToMatchSpace(ViewLayoutContext context)
    {
        var listWidths = new List<GroupSizeWidth[]>();
        var listGroups = new List<IRibbonViewGroupSize>();

        // Scan all groups
        var pixelGaps = 0;
        var maxEntries = 0;
        foreach (ViewBase child in this)
        {
            if (child.Visible)
            {
                // Cast child view to correct interface
                // Only interested in children that are actually groups
                if (child is IRibbonViewGroupSize childSize)
                {

                    // Find list of possible sizes for this group
                    var widths = childSize.GetPossibleSizes(context);

                    // Track how many extra pixels are needed for inter group gaps
                    pixelGaps += SEP_LENGTH_2007;

                    // Add into list of all container values
                    listWidths.Add(widths);
                    listGroups.Add(childSize);

                    // Track the longest list found
                    maxEntries = Math.Max(maxEntries, widths.Length);
                }
            }
        }

        var bestWidth = 0;
        var availableWidth = context.DisplayRectangle.Width;
        int[]? bestIndexes = null;
        var permIndexes = new List<int>();

        // Scan each horizontal slice of the 2D array of values
        for (var i = 0; i < maxEntries; i++)
        {
            // Move from right to left creating a permutation each time
            for (var j = listWidths.Count - 1; j >= 0; j--)
            {
                // Does this cell actually exist?
                if (listWidths[j].Length > i)
                {
                    // Starting width is the pixel gaps
                    var permTotalWidth = pixelGaps;
                    permIndexes.Clear();

                    // Generate permutation by taking cell values
                    for (var k = listWidths.Count - 1; k >= 0; k--)
                    {
                        // If we are on the left of the 'j' cell then move up a level
                        var index = i + (k > j ? 1 : 0);

                        // Limit check the index to available height
                        index = Math.Min(index, listWidths[k].Length - 1);
                        permIndexes.Insert(0, index);

                        // Find width and size of the entry
                        var width = listWidths[k][index].Width;

                        // Track the total width of this permutation
                        permTotalWidth += width;
                    }

                    // We record this as the best match so far, if either it is the first permutation
                    // tried or if closest to filling the entire available width of the client area
                    if ((permTotalWidth > bestWidth) && (permTotalWidth <= availableWidth))
                    {
                        bestWidth = permTotalWidth;
                        bestIndexes = permIndexes.ToArray();
                    }
                }
            }
        }

        // If we have a best fit solution
        if (bestWidth > 0)
        {
            // Use the best discovered solution and push it back to the groups
            _groupWidths = new int[listGroups.Count];
            for (var i = 0; i < listGroups.Count; i++)
            {
                _groupWidths[i] = listWidths[i][bestIndexes![i]].Width;
                listGroups[i].SetSolutionSize(listWidths[i][bestIndexes[i]].Sizing);
            }
        }
        else
        {
            // Use the smallest solution and push it back to the groups
            _groupWidths = new int[listGroups.Count];
            for (var i = 0; i < listGroups.Count; i++)
            {
                _groupWidths[i] = listWidths[i][listWidths[i].Length - 1].Width;
                listGroups[i].SetSolutionSize(listWidths[i][listWidths[i].Length - 1].Sizing);
            }
        }

        return bestWidth;
    }
    #endregion
}