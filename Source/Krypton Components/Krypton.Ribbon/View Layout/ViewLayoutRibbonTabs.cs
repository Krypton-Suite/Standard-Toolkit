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
/// Extends the ViewComposite by creating and laying out elements to represent ribbon tabs.
/// </summary>
internal class ViewLayoutRibbonTabs : ViewComposite
{
    #region Type Definitions
    private class ViewDrawRibbonTabList : List<ViewDrawRibbonTab>;
    private class ViewDrawRibbonTabSepList : List<ViewDrawRibbonTabSep>;
    private class ContextNameList : List<string>;
    #endregion

    #region Static Fields

    private const int TAB_MINWIDTH = 32;
    private const int TAB_EXCESS = 14;

    #endregion

    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private readonly ViewDrawRibbonTabList _tabCache;
    private readonly ViewDrawRibbonTabSepList _tabSepCache;
    private ViewDrawRibbonDesignTab _viewAddTab;
    private NeedPaintHandler _needPaint;
    private ContextNameList _cachedSelectedContext;
    private Size[] _cachedSizes;
    private int _cachedPreferredWidth;
    private int _cachedMinimumWidth;
    private int _cachedAllTabCount;
    private int _cachedNonContextTabCount;
    private bool _showSeparators;
    #endregion

    #region Identity
    static ViewLayoutRibbonTabs() => ContextTabSets = [];

    /// <summary>
    /// Initialize a new instance of the ViewLayoutRibbonTabs class.
    /// </summary>
    /// <param name="ribbon">Owning ribbon control instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ViewLayoutRibbonTabs([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] NeedPaintHandler? needPaint)
    {
        Debug.Assert(ribbon is not null);
        Debug.Assert(needPaint is not null);

        // Cache references
        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));
        _needPaint = needPaint ?? throw new ArgumentNullException(nameof(needPaint));

        // Create cache of draw elements
        _tabCache = [];
        _tabSepCache = [];
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutRibbonTabs:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Clear();

            foreach (ViewDrawRibbonTab tab in _tabCache)
            {
                tab.Dispose();
            }

            foreach (ViewDrawRibbonTabSep tabSep in _tabSepCache)
            {
                tabSep.Dispose();
            }

            _tabCache.Clear();
            _tabSepCache.Clear();
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

    #region ParentControl
    /// <summary>
    /// Gets and sets the parent control.
    /// </summary>
    public Control? ParentControl { get; set; }

    #endregion

    #region GetViewForSpare
    /// <summary>
    /// Gets access to the tabs spare area.
    /// </summary>
    public ViewLayoutRibbonTabsSpare GetViewForSpare { get; private set; }

    #endregion

    #region GetViewForRibbonTab
    /// <summary>
    /// Gets the view element for drawing the provided ribbon tab.
    /// </summary>
    /// <param name="ribbonTab">Tab for which view element is needed.</param>
    /// <returns>View element for tab; otherwise null.</returns>
    public ViewDrawRibbonTab? GetViewForRibbonTab(KryptonRibbonTab? ribbonTab) => ribbonTab is null
        ? null
        : _tabCache.FirstOrDefault(viewTab => viewTab.RibbonTab == ribbonTab);

    /// <summary>
    /// Gets the view element for drawing the first visible ribbon tab.
    /// </summary>
    /// <returns>View element for a tab; otherwise null.</returns>
    public ViewDrawRibbonTab GetViewForFirstRibbonTab()
    {
        foreach (ViewBase child in this)
        {
            if (child.Visible && (child is ViewDrawRibbonTab tab))
            {
                return tab;
            }
        }

        return null!;
    }

    /// <summary>
    /// Gets the view element for drawing the next tab after the provided one.
    /// </summary>
    /// <param name="ribbonTab">Current ribbon tab to use when searching.</param>
    /// <returns>View element for a tab; otherwise null.</returns>
    public ViewDrawRibbonTab GetViewForNextRibbonTab(KryptonRibbonTab ribbonTab)
    {
        var found = false;
        foreach (ViewBase child in this)
        {
            // Cast to correct type
            // Only interested in tab views
            if (child is ViewDrawRibbonTab viewTab)
            {

                // Wait until we see the provided tab, then first visible tab after it
                if (!found)
                {
                    found = viewTab.RibbonTab == ribbonTab;
                }
                else if (child.Visible)
                {
                    return viewTab;
                }
            }
        }

        return null!;
    }

    /// <summary>
    /// Gets the view element for drawing the previous tab from the provided one.
    /// </summary>
    /// <param name="ribbonTab">Current ribbon tab to use when searching.</param>
    /// <returns>View element for a tab; otherwise null.</returns>
    public ViewDrawRibbonTab GetViewForPreviousRibbonTab(KryptonRibbonTab ribbonTab)
    {
        var found = false;
        foreach (ViewBase child in Reverse())
        {
            // Cast to correct type
            // Only interested in tab views
            if (child is ViewDrawRibbonTab viewTab)
            {

                // Wait until we see the provided tab, then first visible tab after it
                if (!found)
                {
                    found = viewTab.RibbonTab == ribbonTab;
                }
                else if (child.Visible)
                {
                    return viewTab;
                }
            }
        }

        return null!;
    }

    /// <summary>
    /// Gets the view element for drawing the last visible ribbon tab.
    /// </summary>
    /// <returns>View element for a tab; otherwise null.</returns>
    public ViewDrawRibbonTab GetViewForLastRibbonTab()
    {
        foreach (ViewBase child in Reverse())
        {
            if (child.Visible && (child is ViewDrawRibbonTab tab))
            {
                return tab;
            }
        }

        return null!;
    }
    #endregion

    #region GetTabKeyTips
    /// <summary>
    /// Generate a key tip info for each visible tab.
    /// </summary>
    /// <returns>Array of KeyTipInfo instances.</returns>
    public KeyTipInfo[] GetTabKeyTips()
    {
        var keyTipList = new KeyTipInfoList();

        foreach (ViewBase child in this)
        {
            // Cast to correct type
            // Only interested in tab views
            if (child is ViewDrawRibbonTab viewTab)
            {
                // Get the screen location of the view tab
                Rectangle tabRect = viewTab.OwningControl!.RectangleToScreen(viewTab.ClientRectangle);

                // The keytip should be centered on the bottom center of the view
                var screenPt = new Point(tabRect.Left + (tabRect.Width / 2), tabRect.Bottom + 2);

                // Create new key tip that invokes the tab controller when selected
                keyTipList.Add(new KeyTipInfo(true, viewTab.RibbonTab!.KeyTip,
                    screenPt, viewTab.ClientRectangle,
                    viewTab.KeyTipTarget));
            }
        }

        return keyTipList.ToArray();
    }
    #endregion

    #region ContextTabSets
    /// <summary>
    /// Gets access to the collection of tab sets shown in the tabs area.
    /// </summary>
    public static ContextTabSetCollection ContextTabSets { get; }

    #endregion

    #region ProcessMouseWheel
    /// <summary>
    /// Process the mouse wheel change of selection.
    /// </summary>
    /// <param name="next">True if movement to next tab required; otherwise previous.</param>
    public void ProcessMouseWheel(bool next)
    {
        KryptonRibbonTab? selectTab = _ribbon.SelectedTab;

        // Scan to find the prev and next tabs
        var prevSelected = false;
        KryptonRibbonTab? prev = null;
        foreach (ViewBase child in this)
        {
            // Only interested in visible ribbon tabs
            if (child.Visible && (child is ViewDrawRibbonTab viewTab))
            {
                // Cast to correct type of view element

                // Is this element for the currently selected tab?
                if (viewTab.RibbonTab == _ribbon.SelectedTab)
                {
                    // If we want the previous tab to the currently selected one!
                    if (!next)
                    {
                        // And if we have a previous tab, then use it
                        if (prev != null)
                        {
                            selectTab = prev;
                        }
                        break;
                    }

                    prevSelected = true;
                }
                else
                {
                    // If we want the next tab and the previous was the selected one
                    if (next && prevSelected)
                    {
                        // Then this is the next one!
                        selectTab = viewTab.RibbonTab;
                        break;
                    }
                }

                prev = viewTab.RibbonTab;
            }
        }

        // Is there a change in selection?
        if (selectTab != null)
        {
            _ribbon.SelectedTab = selectTab;
        }
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        // Sync child elements to represent the current ribbon tabs collection setup
        SyncChildrenToRibbonTabs();

        var preferredSize = Size.Empty;

        // Reset cached variables
        _cachedSizes = new Size[Count];
        _cachedMinimumWidth = 0;
        _cachedAllTabCount = 0;
        _cachedNonContextTabCount = 0;

        // Find total width and maximum height across all child elements
        for (var i = 0; i < Count; i++)
        {
            ViewBase? child = this[i];

            // Only interested in visible items
            if (child!.Visible)
            {
                // Cache preferred size of the child
                _cachedSizes[i] = child.GetPreferredSize(context);

                // Only need extra processing for children that have some width
                if (_cachedSizes[i].Width > 0)
                {
                    // Always add on to the width
                    preferredSize.Width += _cachedSizes[i].Width;

                    var childHeight = _cachedSizes[i].Height;

                    switch (child)
                    {
                        case ViewDrawRibbonTab tab:
                        {
                            // Tabs need an extra pixel height as a separator gap
                            childHeight++;

                            // Cache number of tabs encountered
                            _cachedAllTabCount++;

                            // Cache number of non-context tabs encountered
                            if (string.IsNullOrEmpty(tab.RibbonTab?.ContextName))
                            {
                                _cachedNonContextTabCount++;
                            }

                            break;
                        }
                        case ViewDrawRibbonDesignTab:
                            // Tabs need an extra pixel height as a separator gap
                            childHeight++;

                            // Cache number of tabs encountered
                            _cachedAllTabCount++;
                            break;
                    }

                    // Find maximum height encountered
                    preferredSize.Height = Math.Max(preferredSize.Height, childHeight);

                    // Find the minimum allowed width for all children
                    _cachedMinimumWidth += Math.Min(_cachedSizes[i].Width, TAB_MINWIDTH);
                }
            }
        }

        // Remember total size requested to fit everything into view normally
        _cachedPreferredWidth = preferredSize.Width;

        // Minimum height is the height of a tab
        preferredSize.Height = Math.Max(preferredSize.Height, _ribbon.CalculatedValues.TabHeight);
            
        // Preferred with is the minimum allowed, so the parent scroller knows if scroll bars are needed
        preferredSize.Width = _cachedMinimumWidth;

        // If we have the tabs spare area...
        if (GetViewForSpare != null)
        {
            // Always take up the entire provided width as the spare will take up any remainder not used by actual tabs
            if (preferredSize.Width < context.DisplayRectangle.Width)
            {
                preferredSize.Width = context.DisplayRectangle.Width;
            }
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

        // Sync child elements to represent the current ribbon tabs collection setup
        SyncChildrenToRibbonTabs();

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;

        var x = ClientLocation.X;

        // Are there any children to layout?
        if (Count > 0)
        {
            // Modify the cached sizes so they are ideally sized for actual space
            var layoutSizes = AdjustSizesToFit();

            var y = ClientRectangle.Y;
            var bottom = ClientRectangle.Bottom;
            var height = ClientHeight;

            // Position each item from left to right taking up entire height
            for (var i = 0; i < Count; i++)
            {
                // Only interested in visible items
                if (layoutSizes[i].Width > 0)
                {
                    // Separators are made the full height, others are aligned on the bottom edge
                    switch (this[i])
                    {
                        case ViewDrawRibbonTabSep tabSep:
                            // Update separator with latest calculated need to draw
                            tabSep.Draw = _showSeparators;

                            context.DisplayRectangle = new Rectangle(x, y, layoutSizes[i].Width, height);
                            break;
                        case ViewDrawRibbonTab tab:
                            // Update checked state of the tab
                            tab.Checked = _ribbon.SelectedTab == tab.RibbonTab;

                            context.DisplayRectangle = new Rectangle(x, bottom - layoutSizes[i].Height, layoutSizes[i].Width, layoutSizes[i].Height);
                            break;
                        case ViewDrawRibbonDesignTab:
                            context.DisplayRectangle = new Rectangle(x, bottom - layoutSizes[i].Height, layoutSizes[i].Width, layoutSizes[i].Height);
                            break;
                    }

                    // Position the element
                    this[i]?.Layout(context);

                    // Move across to next position
                    x += layoutSizes[i].Width;
                }
            }
        }

        // Fill remainder space with the tabs spare element
        var customCaptionRect = Rectangle.Empty;
        if (GetViewForSpare != null)
        {
            GetViewForSpare.Visible = false;
            if (x < ClientRectangle.Right)
            {
                if (_ribbon.GetRedirector()?.GetMetricBool(PaletteState.Normal, PaletteMetricBool.RibbonTabsSpareCaption) == InheritBool.True)
                {
                    customCaptionRect = new Rectangle(x, ClientRectangle.Y, ClientRectangle.Right - x, ClientHeight);
                    context.DisplayRectangle = customCaptionRect;
                    GetViewForSpare.Visible = true;
                    GetViewForSpare.Layout(context);
                    x = ClientRectangle.Right;
                }
            }
        }

        // We have an owning form we need to update the custom area it treats as a caption
        if (_ribbon.CaptionArea?.KryptonForm != null)
        {
            if (!customCaptionRect.IsEmpty)
            {
                // Convert the rectangle to the owning form coordinates
                customCaptionRect = ParentControl!.RectangleToScreen(customCaptionRect);
                customCaptionRect = _ribbon.CaptionArea.KryptonForm.RectangleToClient(customCaptionRect);
            }

            _ribbon.CaptionArea.KryptonForm.CustomCaptionArea = customCaptionRect;
        }

        // Update our own size to reflect how wide we actually need to be for all the children
        ClientRectangle = new Rectangle(ClientLocation, new Size(x - ClientLocation.X, ClientHeight));

        // Update the display rectangle we allocated for use by parent
        context.DisplayRectangle = new Rectangle(ClientLocation, new Size(x - ClientLocation.X, ClientHeight));
    }
    #endregion

    #region Implementation
    private void SyncChildrenToRibbonTabs()
    {
        // Remove all child elements
        Clear();

        // Make sure we have enough cached elements
        if (_tabCache.Count < _ribbon.RibbonTabs.Count)
        {
            for (var i = _tabCache.Count; i < _ribbon.RibbonTabs.Count; i++)
            {
                _tabCache.Add(new ViewDrawRibbonTab(_ribbon, this, _needPaint));
            }
        }

        if (_tabSepCache.Count < _ribbon.RibbonTabs.Count)
        {
            for (var i = _tabSepCache.Count; i < _ribbon.RibbonTabs.Count; i++)
            {
                _tabSepCache.Add(new ViewDrawRibbonTabSep(_ribbon.StateCommon.RibbonGeneral));
            }
        }

        // Update from ribbon control in same order as display
        UpdateContextNameCache();

        // Clear down the list of tab sets
        ContextTabSets.Clear();

        // Add all tabs that do not have a context name
        AddTabsWithContextName(string.Empty);

        // Add each set of tabs that match each listed selected context name
        foreach (var contextName in _cachedSelectedContext
                     .Where(contextName => _ribbon.RibbonContexts[contextName] != null)
                )
        {
            AddTabsWithContextName(contextName);
        }
            
        // When in design time help mode
        if (_ribbon.InDesignHelperMode)
        {
            // Create the design time 'Add Tab' first time it is needed
            _viewAddTab ??= new ViewDrawRibbonDesignTab(_ribbon, _needPaint);

            // Always add at end of the list of tabs
            Add(_viewAddTab);
        }
        else
        {
            // At run time we add the filler that acts like title bar header
            GetViewForSpare ??= new ViewLayoutRibbonTabsSpare();

            // Always add at end of the list of tabs
            Add(GetViewForSpare);
        }
    }

    private void AddTabsWithContextName(string contextName)
    {
        ContextTabSet? cts = null;

        // Remove the ribbon tab reference from the draw tab
        // (Must do this for all tabs before setting them to the correct value)
        for (var i = 0; i < _ribbon.RibbonTabs.Count; i++)
        {
            KryptonRibbonTab ribbonTab = _ribbon.RibbonTabs[i];
            if (IsRibbonVisible(ribbonTab, contextName))
            {
                _tabCache[i].RibbonTab = null;
            }
        }

        // Add child elements appropriate for each ribbon tab
        for (var i = 0; i < _ribbon.RibbonTabs.Count; i++)
        {
            KryptonRibbonTab ribbonTab = _ribbon.RibbonTabs[i];
            if (IsRibbonVisible(ribbonTab, contextName))
            {
                // Get the matching indexed items
                ViewDrawRibbonTab drawTab = _tabCache[i];

                // Every tab needs a draw tab element followed by a separator
                Add(drawTab);
                Add(_tabSepCache[i]);

                // Associate the draw element with matching ribbon tab
                drawTab.RibbonTab = ribbonTab;

                // If we are dealing with a real context and not the empty one
                if (!string.IsNullOrEmpty(contextName))
                {
                    // Create tab set when first needed, otherwise this tab must be the last one
                    if (cts == null)
                    {
                        cts = new ContextTabSet(drawTab, _ribbon.RibbonContexts[ribbonTab.ContextName]!);
                    }
                    else
                    {
                        cts.UpdateLastTab(drawTab);
                    }
                }
            }
        }

        // If we created a new tab set, then add to the collection
        if (cts != null)
        {
            ContextTabSets.Add(cts);
        }
    }

    private Size[] AdjustSizesToFit()
    {
        // By default we do not need to have tab separators draw
        _showSeparators = false;

        var retSizes = new Size[_cachedSizes.Length];

        // Make a copy of the cached sizes
        for(var i=0; i<_cachedSizes.Length; i++)
        {
            retSizes[i] = _cachedSizes[i];
        }

        // Only need to shrink if total tab width is less than that available
        if (_cachedPreferredWidth > ClientWidth)
        {
            // If even the minimum width is more than that available
            if (_cachedMinimumWidth > ClientWidth)
            {
                // Reduce all the tabs to the minimum allowed
                for (var i = 0; i < retSizes.Length; i++)
                {
                    retSizes[i].Width = Math.Min(retSizes[i].Width, TAB_MINWIDTH);
                }

                // Must show separators as we are now taking space away that will 
                // cause truncation to the text of one or more of the tabs
                _showSeparators = true;
            }
            else
            {
                var totalWidth = _cachedPreferredWidth;

                /////////////////////////////////////////////////////////
                // Phase 1, remove excess padding from each tab instance
                // by getting rid of upto TAB_EXCESS from each tab entry
                /////////////////////////////////////////////////////////

                // Find out total space to remove (with a limit of TAB_EXCESS per tab instance)
                var remove = Math.Min(totalWidth - ClientWidth, _cachedNonContextTabCount * TAB_EXCESS);

                for (int i = 0, tabCount = _cachedNonContextTabCount; (i < retSizes.Length) && (tabCount > 0); i++)
                {
                    if (retSizes[i].Width > TAB_MINWIDTH)
                    {
                        // Remove an equal amount per tab (limited to TAB_EXCESS)
                        var shrink = Math.Min(remove / tabCount, TAB_EXCESS);
                        retSizes[i].Width -= shrink;
                        remove -= shrink;
                        totalWidth -= shrink;
                        tabCount--;
                    }
                }

                // If even more shrinkage is needed
                if (totalWidth > ClientWidth)
                {
                    // Must show separators as we are now taking space away that will 
                    // cause truncation to the text of one or more of the tabs
                    _showSeparators = true;

                    //////////////////////////////////////////////////////////
                    // Phase 2, shrink widest down to width of second largest
                    // and keep repeating until all tabs same width or all 
                    // required shrinkage has been performed
                    /////////////////////////////////////////////////////////

                    do
                    {
                        // Find the widest and second widest tabs
                        var widestTab = 0;
                        var secondTab = 0;
                        for (var i = 0; i < retSizes.Length; i++)
                        {
                            if (retSizes[i].Width > TAB_MINWIDTH 
                                && retSizes[i].Width > widestTab
                               )
                            {
                                secondTab = widestTab;
                                widestTab = retSizes[i].Width;
                            }
                        }

                        // If the widest tab is equal to the minimum, then nothing more to do
                        if (widestTab <= TAB_MINWIDTH)
                        {
                            break;
                        }

                        // Create a list of all tab indexes matching widest
                        var widestIndexes = new List<int>();
                        for (var i = 0; i < retSizes.Length; i++)
                        {
                            if (retSizes[i].Width == widestTab)
                            {
                                widestIndexes.Add(i);
                            }
                        }

                        // Maximum we can remove is the difference between widest and then second
                        // widest times by the number of tabs we are going to be shrinking
                        var maxRemove = (widestTab - secondTab) * widestIndexes.Count;

                        // Find total we need to remove to fit into display but limited to max
                        remove = Math.Min(maxRemove, totalWidth - ClientWidth);

                        for (int i = 0, tabCount = widestIndexes.Count; i < widestIndexes.Count; i++, tabCount--)
                        {
                            if (retSizes[widestIndexes[i]].Width > TAB_MINWIDTH)
                            {
                                // Remove an equal amount per tab (limited to TAB_EXCESS)
                                var shrink = Math.Min(remove / tabCount, TAB_EXCESS);
                                retSizes[widestIndexes[i]].Width -= shrink;
                                remove -= shrink;
                                totalWidth -= shrink;
                            }
                        }
                    } while (totalWidth > ClientWidth);
                }
            }
        }

        return retSizes;
    }

    private void UpdateContextNameCache()
    {
        // Create list first time around, otherwise clear it down
        if (_cachedSelectedContext == null)
        {
            _cachedSelectedContext = [];
        }
        else
        {
            _cachedSelectedContext.Clear();
        }

        // In design mode 
        if (_ribbon.InDesignHelperMode)
        {
            // All the defined ribbon contexts
            foreach (KryptonRibbonContext context in _ribbon.RibbonContexts)
            {
                _cachedSelectedContext.Add(context.ContextName);
            }
        }
        else
        {
            // If we have a list of context names, then cache them separately
            if (!string.IsNullOrEmpty(_ribbon.SelectedContext))
            {
                // Find the list of context names
                var contexts = _ribbon.SelectedContext.Split(',');

                // Only add each unique context name once
                foreach (var context in contexts)
                {
                    if (!_cachedSelectedContext.Contains(context))
                    {
                        _cachedSelectedContext.Add(context);
                    }
                }
            }
        }
    }

    private bool IsRibbonVisible(KryptonRibbonTab tab, string contextName)
    {
        // At design time we show all the tabs always
        if (tab.Visible || _ribbon.InDesignHelperMode)
        {
            // Tab must have a matching context name setting
            if (tab.ContextName.Equals(contextName))
            {
                return true;
            }
            else if (_ribbon.InDesignHelperMode)
            {
                // If the tab has context name and we are adding tabs with no
                // context name then check if the specified tab context name
                // exists. If not then add into the set of non-matching tabs
                if (!string.IsNullOrEmpty(tab.ContextName) &&
                    string.IsNullOrEmpty(contextName))
                {
                    return _ribbon.RibbonContexts[tab.ContextName] == null;
                }
            }
        }

        return false;
    }
    #endregion
}