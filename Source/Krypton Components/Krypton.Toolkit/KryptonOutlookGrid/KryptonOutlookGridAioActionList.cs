#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides the smart tag actions for the KryptonAllInOneGrid designer.
/// </summary>
internal class KryptonOutlookGridAioActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonOutlookGridAio _allInOneGrid;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonOutlookGridAioActionList"/> class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonOutlookGridAioActionList(KryptonOutlookGridAioDesigner owner)
        : base(owner.Component)
    {
        _allInOneGrid = (owner.Component as KryptonOutlookGridAio)!;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets a value indicating whether the associated <see cref="KryptonOutlookGridGroupBox"/> is visible.
    /// This property acts as a proxy for the control's actual ShowGroupBox property.
    /// </summary>
    public bool ShowGroupBox
    {
        get => _allInOneGrid.ShowGroupBox;
        set
        {
            if (_allInOneGrid.ShowGroupBox != value)
            {
                _service?.OnComponentChanged(_allInOneGrid, null, _allInOneGrid.ShowGroupBox, value);
                _allInOneGrid.ShowGroupBox = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the search toolbar is visible.
    /// This property acts as a proxy for the control's actual ShowSearchToolBar property.
    /// </summary>
    public bool ShowSearchToolBar
    {
        get => _allInOneGrid.ShowSearchToolBar;
        set
        {
            if (_allInOneGrid.ShowSearchToolBar != value)
            {
                _service?.OnComponentChanged(_allInOneGrid, null, _allInOneGrid.ShowSearchToolBar, value);
                _allInOneGrid.ShowSearchToolBar = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether subtotal rows are visible in the OutlookGrid.
    /// This property acts as a proxy for the OutlookGrid's ShowSubTotal property.
    /// </summary>
    public bool ShowSubTotal
    {
        get => _allInOneGrid.OutlookGrid.ShowSubTotal;
        set
        {
            if (_allInOneGrid.OutlookGrid.ShowSubTotal != value)
            {
                _service?.OnComponentChanged(_allInOneGrid.OutlookGrid, null, _allInOneGrid.OutlookGrid.ShowSubTotal, value);
                _allInOneGrid.OutlookGrid.ShowSubTotal = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether grand total row is visible in the OutlookGrid.
    /// This property acts as a proxy for the OutlookGrid's ShowGrandTotal property.
    /// </summary>
    public bool ShowGrandTotal
    {
        get => _allInOneGrid.OutlookGrid.ShowGrandTotal;
        set
        {
            if (_allInOneGrid.OutlookGrid.ShowGrandTotal != value)
            {
                _service?.OnComponentChanged(_allInOneGrid.OutlookGrid, null, _allInOneGrid.OutlookGrid.ShowGrandTotal, value);
                _allInOneGrid.OutlookGrid.ShowGrandTotal = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the grand total row should be displayed at the bottom of the grid.
    /// This property acts as a proxy for the control's actual ShowSummaryGrid property.
    /// </summary>
    public bool ShowSummaryGrid
    {
        get => _allInOneGrid.ShowSummaryGrid;
        set
        {
            if (_allInOneGrid.ShowSummaryGrid != value)
            {
                _service?.OnComponentChanged(_allInOneGrid, null, _allInOneGrid.ShowSummaryGrid, value);
                _allInOneGrid.ShowSummaryGrid = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether searching is enabled on key press in the OutlookGrid.
    /// This property acts as a proxy for the OutlookGrid's EnableSearchOnKeyPress property.
    /// </summary>
    public bool EnableSearchOnKeyPress
    {
        get => _allInOneGrid.OutlookGrid.EnableSearchOnKeyPress;
        set
        {
            if (_allInOneGrid.OutlookGrid.EnableSearchOnKeyPress != value)
            {
                _service?.OnComponentChanged(_allInOneGrid.OutlookGrid, null, _allInOneGrid.OutlookGrid.EnableSearchOnKeyPress, value);
                _allInOneGrid.OutlookGrid.EnableSearchOnKeyPress = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether highlight search text is enabled in the OutlookGrid.
    /// This property acts as a proxy for the OutlookGrid's HighlightSearchText property.
    /// </summary>
    public bool HighlightSearchText
    {
        get => _allInOneGrid.OutlookGrid.HighlightSearchText;
        set
        {
            if (_allInOneGrid.OutlookGrid.HighlightSearchText != value)
            {
                _service?.OnComponentChanged(_allInOneGrid.OutlookGrid, null, _allInOneGrid.OutlookGrid.HighlightSearchText, value);
                _allInOneGrid.OutlookGrid.HighlightSearchText = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the column filter is visible in the OutlookGrid.
    /// This property acts as a proxy for the OutlookGrid's ShowColumnFilter property.
    /// </summary>
    public bool ShowColumnFilter
    {
        get => _allInOneGrid.OutlookGrid.ShowColumnFilter;
        set
        {
            if (_allInOneGrid.OutlookGrid.ShowColumnFilter != value)
            {
                _service?.OnComponentChanged(_allInOneGrid.OutlookGrid, null, _allInOneGrid.OutlookGrid.ShowColumnFilter, value);
                _allInOneGrid.OutlookGrid.ShowColumnFilter = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether live column filtering is enabled in the OutlookGrid.
    /// This property acts as a proxy for the OutlookGrid's LiveColumnFilter property.
    /// </summary>
    public bool LiveColumnFilter
    {
        get => _allInOneGrid.OutlookGrid.LiveColumnFilter;
        set
        {
            if (_allInOneGrid.OutlookGrid.LiveColumnFilter != value)
            {
                _service?.OnComponentChanged(_allInOneGrid.OutlookGrid, null, _allInOneGrid.OutlookGrid.LiveColumnFilter, value);
                _allInOneGrid.OutlookGrid.LiveColumnFilter = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether allow column context menu is enabled in the OutlookGrid.
    /// This property acts as a proxy for the OutlookGrid's AllowColumnContextMenu property.
    /// </summary>
    public bool AllowColumnContextMenu
    {
        get => _allInOneGrid.OutlookGrid.AllowColumnContextMenu;
        set
        {
            if (_allInOneGrid.OutlookGrid.AllowColumnContextMenu != value)
            {
                _service?.OnComponentChanged(_allInOneGrid.OutlookGrid, null, _allInOneGrid.OutlookGrid.AllowColumnContextMenu, value);
                _allInOneGrid.OutlookGrid.AllowColumnContextMenu = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether allow column context menu is enabled in the OutlookGrid.
    /// This property acts as a proxy for the OutlookGrid's AllowColumnContextMenu property.
    /// </summary>
    public bool AutoGenerateInternalColumns
    {
        get => _allInOneGrid.OutlookGrid.AutoGenerateInternalColumns;
        set
        {
            if (_allInOneGrid.OutlookGrid.AutoGenerateInternalColumns != value)
            {
                _service?.OnComponentChanged(_allInOneGrid.OutlookGrid, null, _allInOneGrid.OutlookGrid.AutoGenerateInternalColumns, value);
                _allInOneGrid.OutlookGrid.AutoGenerateInternalColumns = value;
            }
        }
    }

    #endregion

    #region Public Override
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();

        if (_allInOneGrid != null)
        {
            // Add Group Box Configuration actions
            actions.Add(new DesignerActionHeaderItem("Group Box Configuration"));
            actions.Add(new DesignerActionPropertyItem(nameof(ShowGroupBox), "Show Group Box", "Group Box Configuration", "Controls the visibility of the Group Box."));

            // Add Search Toolbar Configuration actions
            actions.Add(new DesignerActionHeaderItem("Search Toolbar Configuration"));
            actions.Add(new DesignerActionPropertyItem(nameof(ShowSearchToolBar), "Show Search Toolbar", "Search Toolbar Configuration", "Controls the visibility of the Search Toolbar."));

            // Add Total Row Configuration actions
            actions.Add(new DesignerActionHeaderItem("Summary Configuration"));
            actions.Add(new DesignerActionPropertyItem(nameof(ShowSubTotal), "Show Sub Totals", "Summary Configuration", "Controls the visibility of sub total rows."));
            actions.Add(new DesignerActionPropertyItem(nameof(ShowGrandTotal), "Show Grand Totals", "Summary Configuration", "Controls the visibility of grand total rows."));
            actions.Add(new DesignerActionPropertyItem(nameof(ShowSummaryGrid), "Show Grand Total at Bottom", "Summary Configuration", "Controls the visibility of the summary grid."));

            // Add Column Filtering actions
            actions.Add(new DesignerActionHeaderItem("Searching And Filtering"));
            actions.Add(new DesignerActionPropertyItem(nameof(EnableSearchOnKeyPress), "Enable Search on Key Press", "Searching And Filtering", "Enables or disables search functionality on key press."));
            actions.Add(new DesignerActionPropertyItem(nameof(HighlightSearchText), "Highlight Search Text", "Searching And Filtering", "Enables or disables paint search text functionality on search."));
            actions.Add(new DesignerActionPropertyItem(nameof(ShowColumnFilter), "Show Column Filter", "Searching And Filtering", "Controls the visibility of the column filter."));
            actions.Add(new DesignerActionPropertyItem(nameof(LiveColumnFilter), "Enable Live Column Filter", "Searching And Filtering", "Enables or disables live column filtering."));

            // Add General Configuration actions
            actions.Add(new DesignerActionHeaderItem("General Configuration"));
            actions.Add(new DesignerActionPropertyItem(nameof(AllowColumnContextMenu), "Allow Column Context Menu", "General Configuration", "Enables or disables  columns context menu."));
            actions.Add(new DesignerActionPropertyItem(nameof(AutoGenerateInternalColumns), "Auto Generate Internal Columns", "General Configuration", "Enables or disables auto generate internal columns"));

        }

        return actions;
    }
    #endregion
}