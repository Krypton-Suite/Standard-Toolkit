#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Lays out a context menu column with scroll up/down overflow items when content exceeds available height.
/// </summary>
internal class ViewLayoutContextMenuOverflowColumn : ViewLayoutStack
{
    #region Instance Fields
    private readonly List<ViewBase> _allItems = [];
    private readonly IContextMenuProvider _provider;
    private readonly ViewContextMenuManager _viewManager;
    private readonly NeedPaintHandler _needPaint;
    private ViewDrawMenuScrollButton? _scrollUp;
    private ViewDrawMenuScrollButton? _scrollDown;
    private int _topIndex;
    private int _maxContentHeight = int.MaxValue;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutContextMenuOverflowColumn class.
    /// </summary>
    /// <param name="provider">Provider of context menu information.</param>
    /// <param name="viewManager">Owning view manager.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ViewLayoutContextMenuOverflowColumn(IContextMenuProvider provider,
        ViewContextMenuManager viewManager,
        NeedPaintHandler needPaint)
        : base(false)
    {
        _provider = provider;
        _viewManager = viewManager;
        _needPaint = needPaint;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        $"ViewLayoutContextMenuOverflowColumn:{Id}";

    #endregion

    #region Public
    /// <summary>
    /// Takes ownership of the views from an existing column stack.
    /// </summary>
    /// <param name="column">Source column to adopt.</param>
    public void Adopt(ViewLayoutStack column)
    {
        while (column.Count > 0)
        {
            ViewBase child = column[0];
            column.RemoveAt(0);
            _allItems.Add(child);
        }

        Rebuild(null);
    }

    /// <summary>
    /// Sets the maximum height available for column content and rebuilds visible items.
    /// </summary>
    /// <param name="maxContentHeight">Maximum height in pixels.</param>
    /// <param name="context">Optional layout context for measurement.</param>
    public void SetMaxContentHeight(int maxContentHeight, ViewLayoutContext? context)
    {
        _maxContentHeight = Math.Max(1, maxContentHeight);
        Rebuild(context);
    }

    /// <summary>
    /// Removes overflow limits and shows all items.
    /// </summary>
    public void ClearMaxContentHeight() => SetMaxContentHeight(int.MaxValue, null);

    /// <summary>
    /// Scrolls the column up or down by one item.
    /// </summary>
    /// <param name="scrollUp">True to scroll up; otherwise scroll down.</param>
    /// <returns>True if the visible range changed.</returns>
    public bool Scroll(bool scrollUp)
    {
        if (scrollUp)
        {
            if (_topIndex <= 0)
            {
                return false;
            }

            _topIndex--;
            Rebuild(null);
            return true;
        }

        if (GetLastVisibleIndex(null) >= _allItems.Count - 1)
        {
            return false;
        }

        _topIndex++;
        Rebuild(null);
        return true;
    }

    /// <summary>
    /// Scrolls the column using the mouse wheel.
    /// </summary>
    /// <param name="delta">Mouse wheel delta.</param>
    /// <returns>True if scrolling was applied.</returns>
    public bool ScrollByWheel(int delta)
    {
        if (!IsOverflowActive(null))
        {
            return false;
        }

        var scrollLines = SystemInformation.MouseWheelScrollLines;
        if (scrollLines == 0)
        {
            return false;
        }

        var startTopIndex = _topIndex;
        var lineCount = scrollLines < 0 ? 1 : scrollLines;
        var scrollUp = delta > 0;

        for (var i = 0; i < lineCount; i++)
        {
            if (scrollUp)
            {
                if (_topIndex == 0)
                {
                    break;
                }

                _topIndex--;
            }
            else
            {
                if (!HasMoreBelow(null))
                {
                    break;
                }

                _topIndex++;
            }
        }

        if (_topIndex != startTopIndex)
        {
            Rebuild(null);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Gets a value indicating whether more items exist below the visible range.
    /// </summary>
    /// <param name="context">Optional layout context for measurement.</param>
    /// <returns>True if more items can be scrolled into view.</returns>
    public bool HasMoreBelow(ViewLayoutContext? context) =>
        GetLastVisibleIndex(context) < _allItems.Count - 1;

    /// <summary>
    /// Gets a value indicating whether items exist above the visible range.
    /// </summary>
    public bool HasMoreAbove => _topIndex > 0;

    /// <summary>
    /// Ensures the provided view is within the visible range.
    /// </summary>
    /// <param name="view">View to bring into view.</param>
    /// <param name="context">Layout context for measurement.</param>
    /// <returns>True if the visible range changed.</returns>
    public bool EnsureVisible(ViewBase view, ViewLayoutContext context)
    {
        var topIndexBefore = _topIndex;
        var index = _allItems.IndexOf(view);
        if (index < 0)
        {
            return false;
        }

        if (index < _topIndex)
        {
            _topIndex = index;
            Rebuild(context);
            return _topIndex != topIndexBefore;
        }

        var lastVisible = GetLastVisibleIndex(context);
        if (index > lastVisible)
        {
            while (GetLastVisibleIndex(context) < index && _topIndex < _allItems.Count - 1)
            {
                _topIndex++;
            }

            Rebuild(context);
        }

        return _topIndex != topIndexBefore;
    }

    /// <summary>
    /// Scrolls the column to show the first item.
    /// </summary>
    /// <param name="context">Optional layout context for measurement.</param>
    /// <returns>True if the visible range changed.</returns>
    public bool ScrollToStart(ViewLayoutContext? context)
    {
        if (_topIndex == 0)
        {
            return false;
        }

        _topIndex = 0;
        Rebuild(context);
        return true;
    }

    /// <summary>
    /// Scrolls the column to show the last item.
    /// </summary>
    /// <param name="context">Optional layout context for measurement.</param>
    /// <returns>True if the visible range changed.</returns>
    public bool ScrollToEnd(ViewLayoutContext? context)
    {
        if (!IsOverflowActive(context))
        {
            return false;
        }

        var topIndexBefore = _topIndex;
        while (HasMoreBelow(context))
        {
            _topIndex++;
        }

        if (_topIndex == topIndexBefore)
        {
            return false;
        }

        Rebuild(context);
        return true;
    }

    /// <summary>
    /// Gets the index of the view in the full item list.
    /// </summary>
    /// <param name="view">View to find.</param>
    /// <returns>Item index, or -1 if not found.</returns>
    public int GetItemIndex(ViewBase view) => _allItems.IndexOf(view);

    /// <summary>
    /// Gets the view at the specified index in the full item list.
    /// </summary>
    /// <param name="index">Item index.</param>
    /// <returns>View at the index, or null if out of range.</returns>
    public ViewBase? GetItemViewAt(int index) =>
        index >= 0 && index < _allItems.Count ? _allItems[index] : null;

    /// <summary>
    /// Determines if the view belongs to this column.
    /// </summary>
    /// <param name="view">View to test.</param>
    /// <returns>True if owned by this column.</returns>
    public bool ContainsView(ViewBase view) => _allItems.Contains(view);

    /// <summary>
    /// Determines if the target belongs to this column.
    /// </summary>
    /// <param name="target">Target to test.</param>
    /// <returns>True if owned by this column.</returns>
    public bool ContainsTarget(IContextMenuTarget target)
    {
        ViewBase active = target.GetActiveView();
        return ContainsView(active);
    }
    #endregion

    #region Layout
    /// <inheritdoc />
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        Rebuild(context);
        return base.GetPreferredSize(context);
    }
    #endregion

    #region Implementation
    private void Rebuild(ViewLayoutContext? context)
    {
        Clear();

        if (_allItems.Count == 0)
        {
            return;
        }

        if (!IsOverflowActive(context))
        {
            _topIndex = 0;
            foreach (ViewBase item in _allItems)
            {
                Add(item);
            }

            return;
        }

        var scrollHeight = GetScrollButtonHeight(context);
        var used = 0;

        if (_topIndex > 0)
        {
            Add(GetScrollUpButton());
            used += scrollHeight;
        }

        var index = _topIndex;
        var lastAdded = _topIndex - 1;

        while (index < _allItems.Count)
        {
            var itemHeight = MeasureItemHeight(_allItems[index], context);
            var reserveScrollDown = index < _allItems.Count - 1 ? scrollHeight : 0;

            if (used + itemHeight + reserveScrollDown > _maxContentHeight)
            {
                break;
            }

            Add(_allItems[index]);
            used += itemHeight;
            lastAdded = index;
            index++;
        }

        if (lastAdded < _allItems.Count - 1)
        {
            Add(GetScrollDownButton());
        }
    }

    private bool IsOverflowActive(ViewLayoutContext? context)
    {
        if (_maxContentHeight == int.MaxValue)
        {
            return false;
        }

        var total = 0;
        foreach (ViewBase item in _allItems)
        {
            total += MeasureItemHeight(item, context);
        }

        var scrollHeight = GetScrollButtonHeight(context);
        return total + (scrollHeight * 2) > _maxContentHeight;
    }

    private int GetLastVisibleIndex(ViewLayoutContext? context)
    {
        if (!IsOverflowActive(context))
        {
            return _allItems.Count - 1;
        }

        var scrollHeight = GetScrollButtonHeight(context);
        var used = _topIndex > 0 ? scrollHeight : 0;
        var index = _topIndex;
        var lastVisible = _topIndex - 1;

        while (index < _allItems.Count)
        {
            var itemHeight = MeasureItemHeight(_allItems[index], context);
            var reserveScrollDown = index < _allItems.Count - 1 ? scrollHeight : 0;

            if (used + itemHeight + reserveScrollDown > _maxContentHeight)
            {
                break;
            }

            used += itemHeight;
            lastVisible = index;
            index++;
        }

        return lastVisible;
    }

    private static int MeasureItemHeight(ViewBase item, ViewLayoutContext? context)
    {
        if (context == null)
        {
            return item.ClientHeight > 0 ? item.ClientHeight : SystemInformation.MenuHeight;
        }

        return item.GetPreferredSize(context).Height;
    }

    private int GetScrollButtonHeight(ViewLayoutContext? context) =>
        MeasureItemHeight(GetScrollUpButton(), context);

    private ViewDrawMenuScrollButton GetScrollUpButton()
    {
        if (_scrollUp == null)
        {
            _scrollUp = new ViewDrawMenuScrollButton(_provider, true)
            {
                OverflowColumn = this
            };
        }

        return _scrollUp;
    }

    private ViewDrawMenuScrollButton GetScrollDownButton()
    {
        if (_scrollDown == null)
        {
            _scrollDown = new ViewDrawMenuScrollButton(_provider, false)
            {
                OverflowColumn = this
            };
        }

        return _scrollDown;
    }
    #endregion
}
