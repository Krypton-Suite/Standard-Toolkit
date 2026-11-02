#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Lays out a <see cref="KryptonRibbonGroupStack"/> as columns of three, or as one large row that wraps.
/// </summary>
internal class ViewLayoutRibbonGroupStack : ViewComposite,
    IRibbonViewGroupContainerView
{
    #region Type Definitions
    private class ItemToView : Dictionary<IRibbonGroupItem, ViewBase>;
    #endregion

    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private readonly KryptonRibbonGroupStack _ribbonStack;
    private ViewDrawRibbonDesignGroupStack? _viewAddItem;
    private readonly NeedPaintHandler _needPaint;
    private GroupItemSize _currentSize;
    private ItemToView _itemToView;
    private readonly List<ViewBase> _visibleViews;
    private readonly List<Size> _visibleSizes;
    private readonly List<int> _columnWidths;
    private readonly int[] _rowHeights;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="ViewLayoutRibbonGroupStack"/> class.
    /// </summary>
    /// <param name="ribbon">Owning ribbon control instance.</param>
    /// <param name="ribbonStack">Reference to stack definition.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ViewLayoutRibbonGroupStack([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] KryptonRibbonGroupStack? ribbonStack,
        [DisallowNull] NeedPaintHandler? needPaint)
    {
        Debug.Assert(ribbon is not null);
        Debug.Assert(ribbonStack is not null);
        Debug.Assert(needPaint is not null);

        _ribbon = ribbon ?? ThrowHelper.ThrowArgumentNullException(ribbon);
        _ribbonStack = ribbonStack ?? ThrowHelper.ThrowArgumentNullException(ribbonStack);
        _needPaint = needPaint ?? ThrowHelper.ThrowArgumentNullException(needPaint);
        Component = _ribbonStack;

        _itemToView = new ItemToView();
        _visibleViews = new List<ViewBase>();
        _visibleSizes = new List<Size>();
        _columnWidths = new List<int>();
        _rowHeights = new int[3];

        ApplySize(ribbonStack!.ItemSizeCurrent);

        _ribbonStack.PropertyChanged += OnStackPropertyChanged;
        _ribbonStack.StackView = this;

        if (_ribbon.InDesignMode)
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
    public override string ToString() => $"ViewLayoutRibbonGroupStack:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _ribbonStack.PropertyChanged -= OnStackPropertyChanged;
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

        foreach (ViewBase child in this)
        {
            if (child.Visible && child is IRibbonViewGroupItemView item)
            {
                view = item.GetFirstFocusItem();
                if (view != null)
                {
                    break;
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

        foreach (ViewBase child in Reverse())
        {
            if (child.Visible && child is IRibbonViewGroupItemView item)
            {
                view = item.GetLastFocusItem();
                if (view != null)
                {
                    break;
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

        foreach (ViewBase child in this)
        {
            if (child.Visible && child is IRibbonViewGroupItemView item)
            {
                view = matched ? item.GetFirstFocusItem() : item.GetNextFocusItem(current, ref matched);
                if (view != null)
                {
                    break;
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

        foreach (ViewBase child in Reverse())
        {
            if (child.Visible && child is IRibbonViewGroupItemView item)
            {
                view = matched ? item.GetLastFocusItem() : item.GetPreviousFocusItem(current, ref matched);
                if (view != null)
                {
                    break;
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
        foreach (ViewBase child in this)
        {
            if (child.Visible && child is IRibbonViewGroupItemView item)
            {
                item.GetGroupKeyTips(keyTipList, IndexOf(child) + 1);
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
        SyncChildrenToRibbonGroupItems();

        var results = new List<ItemSizeWidth>();

        if (_ribbonStack.ItemSizeMaximum == GroupItemSize.Large)
        {
            ApplySize(GroupItemSize.Large);
            results.Add(new ItemSizeWidth(GroupItemSize.Large, GetPreferredSize(context).Width));
        }

        if (((int)_ribbonStack.ItemSizeMaximum >= (int)GroupItemSize.Medium) &&
            ((int)_ribbonStack.ItemSizeMinimum <= (int)GroupItemSize.Medium))
        {
            ApplySize(GroupItemSize.Medium);
            var mediumWidth = new ItemSizeWidth(GroupItemSize.Medium, GetPreferredSize(context).Width);

            if (_ribbon.InDesignHelperMode)
            {
                if (results.Count == 0)
                {
                    results.Add(mediumWidth);
                }
            }
            else if ((results.Count == 0) || (results[0].Width > mediumWidth.Width))
            {
                results.Add(mediumWidth);
            }
        }

        if (_ribbonStack.ItemSizeMinimum == GroupItemSize.Small)
        {
            ApplySize(GroupItemSize.Small);
            var smallWidth = new ItemSizeWidth(GroupItemSize.Small, GetPreferredSize(context).Width);

            if (_ribbon.InDesignHelperMode)
            {
                if (results.Count == 0)
                {
                    results.Add(smallWidth);
                }
            }
            else if ((results.Count == 0) || (results[results.Count - 1].Width > smallWidth.Width))
            {
                results.Add(smallWidth);
            }
        }

        ResetSize();
        return results.ToArray();
    }

    /// <summary>
    /// Update the group with the provided sizing solution.
    /// </summary>
    /// <param name="size">Value for the container.</param>
    public void SetSolutionSize(ItemSizeWidth size)
    {
        _ribbonStack.ItemSizeCurrent = size.GroupItemSize;
        ApplySize(size.GroupItemSize);
    }

    /// <summary>
    /// Reset the container back to its requested size.
    /// </summary>
    public void ResetSolutionSize()
    {
        _ribbonStack.ItemSizeCurrent = _ribbonStack.ItemSizeMaximum;
        ApplySize(_ribbonStack.ItemSizeCurrent);
    }

    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        SyncChildrenToRibbonGroupItems();
        Size preferredSize = UseHorizontalRow()
            ? MeasureHorizontal(context)
            : MeasureColumns(context);

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

        ClientRectangle = context!.DisplayRectangle;

        // GetPossibleSizes measures every allowed size and leaves the last trial in the cache.
        // Measure again at the size the ribbon selected before placing children.
        if (UseHorizontalRow())
        {
            MeasureHorizontal(context);
        }
        else
        {
            MeasureColumns(context);
        }

        if (_visibleViews.Count > 0)
        {
            if (UseHorizontalRow())
            {
                LayoutHorizontal(context);
            }
            else
            {
                LayoutColumns(context);
            }
        }

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
        if (_ribbon.InDesignHelperMode)
        {
            DesignTimeDraw.DrawFlapArea(_ribbon, context, ClientRectangle, State);
        }

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
    private bool UseHorizontalRow() =>
        (_ribbonStack.StackLayout == RibbonGroupStackLayout.Adaptive) &&
        (_currentSize == GroupItemSize.Large);

    private Size MeasureHorizontal(ViewLayoutContext context)
    {
        ClearMeasuredChildren();
        var preferredSize = Size.Empty;
        ViewBase? previousChild = null;

        for (var i = 0; i < Count; i++)
        {
            ViewBase? child = this[i];
            if ((child == null) || !child.Visible)
            {
                continue;
            }

            PrepareCluster(child, previousChild);
            Size childSize = child.GetPreferredSize(context);
            if (preferredSize.Width > 0)
            {
                preferredSize.Width++;
            }

            preferredSize.Width += childSize.Width;
            preferredSize.Height = Math.Max(preferredSize.Height, childSize.Height);
            _visibleViews.Add(child);
            _visibleSizes.Add(childSize);
            previousChild = child;
        }

        return preferredSize;
    }

    private Size MeasureColumns(ViewLayoutContext context)
    {
        ClearMeasuredChildren();
        ViewBase? previousChild = null;

        for (var i = 0; i < Count; i++)
        {
            ViewBase? child = this[i];
            if ((child == null) || !child.Visible)
            {
                continue;
            }

            PrepareCluster(child, previousChild);
            Size childSize = child.GetPreferredSize(context);
            var index = _visibleViews.Count;
            var column = index / 3;
            var row = index % 3;

            while (_columnWidths.Count <= column)
            {
                _columnWidths.Add(0);
            }

            _columnWidths[column] = Math.Max(_columnWidths[column], childSize.Width);
            _rowHeights[row] = Math.Max(_rowHeights[row], childSize.Height);
            _visibleViews.Add(child);
            _visibleSizes.Add(childSize);
            previousChild = child;
        }

        var width = 0;
        for (var column = 0; column < _columnWidths.Count; column++)
        {
            if (column > 0)
            {
                width++;
            }

            width += _columnWidths[column];
        }

        var usedRows = Math.Min(3, _visibleViews.Count);
        var height = 0;
        for (var row = 0; row < usedRows; row++)
        {
            height += _rowHeights[row];
        }

        return new Size(width, height);
    }

    private void LayoutHorizontal(ViewLayoutContext context)
    {
        var isRtl = RibbonRtlLayout.IsRtl(_ribbon);
        var x = RibbonRtlLayout.StartX(ClientRectangle, isRtl);
        var y = ClientLocation.Y;

        if (_ribbon.InDesignHelperMode && !isRtl)
        {
            x += DesignTimeDraw.FlapWidth;
        }

        for (var i = 0; i < _visibleViews.Count; i++)
        {
            Size childSize = _visibleSizes[i];
            context.DisplayRectangle = RibbonRtlLayout.NextItem(ref x, y, childSize.Width, childSize.Height, isRtl, 1);
            _visibleViews[i].Layout(context);
        }
    }

    private void LayoutColumns(ViewLayoutContext context)
    {
        var isRtl = RibbonRtlLayout.IsRtl(_ribbon);
        var x = RibbonRtlLayout.StartX(ClientRectangle, isRtl);
        if (_ribbon.InDesignHelperMode && !isRtl)
        {
            x += DesignTimeDraw.FlapWidth;
        }

        var columnCount = _columnWidths.Count;
        var columnX = new int[columnCount];
        var cursor = x;
        for (var column = 0; column < columnCount; column++)
        {
            if (isRtl)
            {
                cursor -= _columnWidths[column];
                columnX[column] = cursor;
                cursor--;
            }
            else
            {
                columnX[column] = cursor;
                cursor += _columnWidths[column] + 1;
            }
        }

        var rowY = new int[3];
        var y = ClientLocation.Y;
        for (var row = 0; row < 3; row++)
        {
            rowY[row] = y;
            y += _rowHeights[row];
        }

        var align = _ribbonStack.ItemAlignment;
        if (isRtl)
        {
            if (align == RibbonItemAlignment.Near)
            {
                align = RibbonItemAlignment.Far;
            }
            else if (align == RibbonItemAlignment.Far)
            {
                align = RibbonItemAlignment.Near;
            }
        }

        for (var i = 0; i < _visibleViews.Count; i++)
        {
            var column = i / 3;
            var row = i % 3;
            Size childSize = _visibleSizes[i];
            var columnWidth = _columnWidths[column];
            int itemX;
            if (align == RibbonItemAlignment.Center)
            {
                itemX = columnX[column] + ((columnWidth - childSize.Width) / 2);
            }
            else if (align == RibbonItemAlignment.Far)
            {
                itemX = columnX[column] + columnWidth - childSize.Width;
            }
            else
            {
                itemX = columnX[column];
            }

            context.DisplayRectangle = new Rectangle(itemX, rowY[row], childSize.Width, childSize.Height);
            _visibleViews[i].Layout(context);
        }
    }

    private static void PrepareCluster(ViewBase child, ViewBase? previousChild)
    {
        if (child is ViewLayoutRibbonGroupCluster clusterChild)
        {
            clusterChild.StartSeparator = (previousChild != null) && previousChild is not ViewLayoutRibbonGroupCluster;
            clusterChild.EndSeparator = true;
        }
    }

    private void ClearMeasuredChildren()
    {
        _visibleViews.Clear();
        _visibleSizes.Clear();
        _columnWidths.Clear();
        _rowHeights[0] = 0;
        _rowHeights[1] = 0;
        _rowHeights[2] = 0;
    }

    private void ApplySize(GroupItemSize size)
    {
        SetCurrentSize(size);
        var itemSize = _ribbonStack.ToChildSize(size);

        foreach (ViewBase item in this)
        {
            if (item is IRibbonViewGroupItemView viewItem)
            {
                viewItem.SetGroupItemSize(itemSize);
            }
        }
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

        SetCurrentSize(_ribbonStack.ItemSizeCurrent);
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
        Clear();

        var regenerate = new ItemToView();

        foreach (IRibbonGroupItem item in _ribbonStack.Items!)
        {
            ViewBase itemView;

            if (_itemToView.ContainsKey(item))
            {
                itemView = _itemToView[item];
                _itemToView.Remove(item);
            }
            else
            {
                itemView = item.CreateView(_ribbon, _needPaint);
            }

            itemView.Visible = _ribbon.InDesignHelperMode || item.Visible;
            regenerate.Add(item, itemView);
            Add(itemView);
        }

        if (_ribbon.InDesignHelperMode)
        {
            _viewAddItem ??= new ViewDrawRibbonDesignGroupStack(_ribbon,
                _ribbonStack,
                _currentSize,
                _needPaint);

            Add(_viewAddItem);
        }

        foreach (ViewBase view in _itemToView.Values)
        {
            view.Dispose();
        }

        _itemToView = regenerate;
    }

    private void OnStackPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        var updateLayout = false;

        switch (e.PropertyName)
        {
            case nameof(KryptonRibbonGroupStack.Visible):
            case nameof(KryptonRibbonGroupStack.ItemAlignment):
            case nameof(KryptonRibbonGroupStack.StackLayout):
                updateLayout = true;
                break;
            case "ItemSizeMinimum":
            case "ItemSizeMaximum":
            case "ItemSizeCurrent":
                SetCurrentSize(_ribbonStack.ItemSizeCurrent);
                updateLayout = true;
                break;
        }

        if (updateLayout &&
            (_ribbonStack.RibbonTab != null) &&
            (_ribbon.SelectedTab == _ribbonStack.RibbonTab))
        {
            OnNeedPaint(true);
        }
    }

    private void OnContextClick(object? sender, MouseEventArgs e)
    {
        if (_ribbon.InDesignMode)
        {
            _ribbonStack.OnDesignTimeContextMenu(e);
        }
    }
    #endregion
}
