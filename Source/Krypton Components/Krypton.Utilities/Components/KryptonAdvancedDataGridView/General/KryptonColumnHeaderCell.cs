#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

internal class KryptonColumnHeaderCell : DataGridViewColumnHeaderCell
{
    #region Instance Fields

    private Image _filterImage = Properties.Resources.ColumnHeader_UnFiltered;
    private Size _filterButtonImageSize = new Size(16, 16);
    private bool _filterButtonPressed = false;
    private bool _filterButtonOver = false;
    private Rectangle _filterButtonOffsetBounds = Rectangle.Empty;
    private Rectangle _filterButtonImageBounds = Rectangle.Empty;
    private Padding _filterButtonMargin = new Padding(3, 4, 3, 4);
    private bool _filterEnabled = false;

    /// <summary>
    /// Get the MenuStrip for this ColumnHeaderCell
    /// </summary>
    public MenuStrip? MenuStrip { get; private set; }


    #endregion

    #region Constants

    /// <summary>
    /// Default behaviour for Date and Time filter
    /// </summary>
    private const bool FILTER_DATE_AND_TIME_DEFAULT_ENABLED = false;

    #endregion

    #region Events

    public event ColumnHeaderCellEventHandler? FilterPopup;
    public event ColumnHeaderCellEventHandler? SortChanged;
    public event ColumnHeaderCellEventHandler? FilterChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonColumnHeaderCell"/> class.
    /// </summary>
    /// <param name="oldCell">The old cell.</param>
    /// <param name="filterEnabled">if set to <c>true</c> [filter enabled].</param>
    public KryptonColumnHeaderCell(DataGridViewColumnHeaderCell oldCell, bool filterEnabled) : base()
    {
        Tag = oldCell.Tag;

        ErrorText = oldCell.ErrorText;

        ToolTipText = oldCell.ToolTipText;

        Value = oldCell.Value;

        ValueType = oldCell.ValueType;

        ContextMenuStrip = oldCell.ContextMenuStrip;

        Style = oldCell.Style;

        _filterEnabled = filterEnabled;

        if (oldCell is KryptonColumnHeaderCell { MenuStrip: not null } oldCellt)
        {
            MenuStrip = oldCellt.MenuStrip;
            _filterImage = oldCellt._filterImage;
            _filterButtonPressed = oldCellt._filterButtonPressed;
            _filterButtonOver = oldCellt._filterButtonOver;
            _filterButtonOffsetBounds = oldCellt._filterButtonOffsetBounds;
            _filterButtonImageBounds = oldCellt._filterButtonImageBounds;
            MenuStrip.FilterChanged += new EventHandler(MenuStrip_FilterChanged);
            MenuStrip.SortChanged += new EventHandler(MenuStrip_SortChanged);
        }
        else
        {
            Type dataType = oldCell.OwningColumn?.ValueType ?? typeof(object);
            MenuStrip = new MenuStrip(dataType);
            MenuStrip.FilterChanged += new EventHandler(MenuStrip_FilterChanged);
            MenuStrip.SortChanged += new EventHandler(MenuStrip_SortChanged);
        }

        IsFilterDateAndTimeEnabled = FILTER_DATE_AND_TIME_DEFAULT_ENABLED;
        IsSortEnabled = true;
        IsFilterEnabled = true;
        IsFilterChecklistEnabled = true;
    }

    ~KryptonColumnHeaderCell()
    {
        if (MenuStrip != null)
        {
            MenuStrip.FilterChanged -= MenuStrip_FilterChanged;
            MenuStrip.SortChanged -= MenuStrip_SortChanged;
        }
    }

    #endregion

    #region Implementation

    /// <summary>
    /// Get or Set the Filter and Sort enabled status
    /// </summary>
    public bool FilterAndSortEnabled
    {
        get => _filterEnabled;
        set
        {
            if (!value)
            {
                _filterButtonPressed = false;
                _filterButtonOver = false;
            }

            if (value != _filterEnabled)
            {
                _filterEnabled = value;
                bool refreshed = false;
                if (MenuStrip?.FilterString!.Length > 0)
                {
                    MenuStrip_FilterChanged(this, EventArgs.Empty);
                    refreshed = true;
                }
                if (MenuStrip?.SortString!.Length > 0)
                {
                    MenuStrip_SortChanged(this, EventArgs.Empty);
                    refreshed = true;
                }
                if (!refreshed)
                {
                    RepaintCell();
                }
            }
        }
    }

    /// <summary>
    /// Set or Unset the Filter and Sort to Loaded mode
    /// </summary>
    /// <param name="enabled"></param>
    public void SetLoadedMode(bool enabled)
    {
        MenuStrip?.SetLoadedMode(enabled);
        RefreshImage();
        RepaintCell();
    }

    /// <summary>
    /// Clean Sort
    /// </summary>
    public void CleanSort()
    {
        if (MenuStrip != null && FilterAndSortEnabled)
        {
            MenuStrip.CleanSort();
            RefreshImage();
            RepaintCell();
        }
    }

    /// <summary>
    /// Clean Filter
    /// </summary>
    public void CleanFilter()
    {
        if (MenuStrip != null && FilterAndSortEnabled)
        {
            MenuStrip.CleanFilter();
            RefreshImage();
            RepaintCell();
        }
    }

    /// <summary>
    /// Sort ASC
    /// </summary>
    public void SortASC()
    {
        if (MenuStrip != null && FilterAndSortEnabled)
        {
            MenuStrip.SortAsc();
        }
    }

    /// <summary>
    /// Sort DESC
    /// </summary>
    public void SortDESC()
    {
        if (MenuStrip != null && FilterAndSortEnabled)
        {
            MenuStrip.SortDesc();
        }
    }

    /// <summary>
    /// Clone the ColumnHeaderCell
    /// </summary>
    /// <returns></returns>
    public override object Clone()
    {
        return new KryptonColumnHeaderCell(this, FilterAndSortEnabled);
    }

    /// <summary>
    /// Get the MenuStrip SortType
    /// </summary>
    public MenuStrip.SortType ActiveSortType
    {
        get
        {
            if (MenuStrip != null && FilterAndSortEnabled)
            {
                return MenuStrip.ActiveSortType;
            }
            else
            {
                return MenuStrip.SortType.None;
            }
        }
    }

    /// <summary>
    /// Get the MenuStrip FilterType
    /// </summary>
    public MenuStrip.FilterType ActiveFilterType
    {
        get
        {
            if (MenuStrip != null && FilterAndSortEnabled)
            {
                return MenuStrip.ActiveFilterType;
            }
            else
            {
                return MenuStrip.FilterType.None;
            }
        }
    }

    /// <summary>
    /// Get the Sort string
    /// </summary>
    public string? SortString
    {
        get
        {
            if (MenuStrip != null && FilterAndSortEnabled)
            {
                return MenuStrip.SortString;
            }
            else
            {
                return "";
            }
        }
    }

    /// <summary>
    /// Get the Filter string
    /// </summary>
    public string? FilterString
    {
        get
        {
            if (MenuStrip != null && FilterAndSortEnabled)
            {
                return MenuStrip.FilterString;
            }
            else
            {
                return "";
            }
        }
    }

    /// <summary>
    /// Get the Minimum size
    /// </summary>
    public Size MinimumSize =>
        new(_filterButtonImageSize.Width + _filterButtonMargin.Left + _filterButtonMargin.Right,
            _filterButtonImageSize.Height + _filterButtonMargin.Bottom + _filterButtonMargin.Top);

    /// <summary>
    /// Get or Set the Sort enabled status
    /// </summary>
    public bool IsSortEnabled
    {
        get => MenuStrip is { IsSortEnabled: true };
        set => MenuStrip?.IsSortEnabled = value;
    }

    /// <summary>
    /// Get or Set the Filter enabled status
    /// </summary>
    public bool IsFilterEnabled
    {
        get => MenuStrip is { IsFilterEnabled: true };
        set => MenuStrip?.IsFilterEnabled = value;
    }

    /// <summary>
    /// Get or Set the Filter enabled status
    /// </summary>
    public bool IsFilterChecklistEnabled
    {
        get => MenuStrip is { IsFilterChecklistEnabled: true };
        set => MenuStrip?.IsFilterChecklistEnabled = value;
    }

    /// <summary>
    /// Get or Set the FilterDateAndTime enabled status
    /// </summary>
    public bool IsFilterDateAndTimeEnabled
    {
        get => MenuStrip is { IsFilterDateAndTimeEnabled: true };
        set => MenuStrip?.IsFilterDateAndTimeEnabled = value;
    }

    /// <summary>
    /// Get or Set the NOT IN logic for Filter
    /// </summary>
    public bool IsMenuStripFilterNOTINLogicEnabled
    {
        get => MenuStrip is { IsFilterNotinLogicEnabled: true };
        set => MenuStrip?.IsFilterNotinLogicEnabled = value;
    }

    /// <summary>
    /// Set the text filter search nodes behaviour
    /// </summary>
    public bool DoesTextFilterRemoveNodesOnSearch
    {
        get => MenuStrip is { DoesTextFilterRemoveNodesOnSearch: true };
        set => MenuStrip?.DoesTextFilterRemoveNodesOnSearch = value;
    }

    /// <summary>
    /// Number of nodes to enable the TextChanged delay on text filter
    /// </summary>
    public int TextFilterTextChangedDelayNodes
    {
        get => MenuStrip!.TextFilterTextChangedDelayNodes;
        set => MenuStrip?.TextFilterTextChangedDelayNodes = value;
    }

    /// <summary>
    /// Enabled or disable Sort capabilities
    /// </summary>
    /// <param name="enabled"></param>
    public void SetSortEnabled(bool enabled)
    {
        if (MenuStrip != null)
        {
            MenuStrip.IsSortEnabled = enabled;
            MenuStrip.SetSortEnabled(enabled);
        }
    }

    /// <summary>
    /// Enable or disable Filter capabilities
    /// </summary>
    /// <param name="enabled"></param>
    public void SetFilterEnabled(bool enabled)
    {
        if (MenuStrip != null)
        {
            MenuStrip.IsFilterEnabled = enabled;
            MenuStrip.SetFilterEnabled(enabled);
        }
    }

    /// <summary>
    /// Enable or disable Filter checklist capabilities
    /// </summary>
    /// <param name="enabled"></param>
    public void SetFilterChecklistEnabled(bool enabled)
    {
        if (MenuStrip != null)
        {
            MenuStrip.IsFilterChecklistEnabled = enabled;
            MenuStrip.SetFilterChecklistEnabled(enabled);
        }
    }

    /// <summary>
    /// Set Filter checklist nodes max
    /// </summary>
    /// <param name="maxNodes"></param>
    public void SetFilterChecklistNodesMax(int maxNodes)
    {
        if (maxNodes >= 0)
        {
            MenuStrip?.MaxChecklistNodes = maxNodes;
        }
    }

    /// <summary>
    /// Enable or disable Filter checklist nodes max
    /// </summary>
    /// <param name="enabled"></param>
    public void EnabledFilterChecklistNodesMax(bool enabled)
    {
        if (MenuStrip is { MaxChecklistNodes: 0 } && enabled)
        {
            MenuStrip.MaxChecklistNodes = MenuStrip.DefaultMaxChecklistNodes;
        }
        else if (MenuStrip is not { MaxChecklistNodes: 0 } && !enabled)
        {
            MenuStrip?.MaxChecklistNodes = 0;
        }
    }

    /// <summary>
    /// Enable or disable Filter custom capabilities
    /// </summary>
    /// <param name="enabled"></param>
    public void SetFilterCustomEnabled(bool enabled)
    {
        if (MenuStrip != null)
        {
            MenuStrip.IsFilterCustomEnabled = enabled;
            MenuStrip.SetFilterCustomEnabled(enabled);
        }
    }

    /// <summary>
    /// Enable or disable Text filter on checklist remove node mode
    /// </summary>
    /// <param name="enabled"></param>
    public void SetChecklistTextFilterRemoveNodesOnSearchMode(bool enabled) => MenuStrip?.SetChecklistTextFilterRemoveNodesOnSearchMode(enabled);

    /// <summary>
    /// Disable text filter TextChanged delay
    /// </summary>
    public void SetTextFilterTextChangedDelayNodesDisabled() => MenuStrip?.SetTextFilterTextChangedDelayNodesDisabled();

    /// <summary>
    /// Set text filter TextChanged delay milliseconds
    /// </summary>
    public void SetTextFilterTextChangedDelayMs(int milliseconds) => MenuStrip?.TextFilterTextChangedDelayMs = milliseconds;

    #endregion
    
    #region MenuStrip Events

    /// <summary>
    /// OnFilterChanged event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MenuStrip_FilterChanged(object? sender, EventArgs e)
    {
        RefreshImage();
        RepaintCell();
        if (FilterAndSortEnabled && FilterChanged != null && OwningColumn != null)
        {
            FilterChanged(this, new ColumnHeaderCellEventArgs(MenuStrip, OwningColumn));
        }
    }

    /// <summary>
    /// OnSortChanged event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MenuStrip_SortChanged(object? sender, EventArgs e)
    {
        RefreshImage();
        RepaintCell();
        if (FilterAndSortEnabled && SortChanged != null && OwningColumn != null)
        {
            SortChanged(this, new ColumnHeaderCellEventArgs(MenuStrip, OwningColumn));
        }
    }

    /// <summary>
    /// Clean attached events
    /// </summary>
    public void CleanEvents()
    {
        MenuStrip?.FilterChanged -= MenuStrip_FilterChanged;
        MenuStrip?.SortChanged -= MenuStrip_SortChanged;
    }


    #endregion
    
    #region Paint Methods

    /// <summary>
    /// Repaint the Cell
    /// </summary>
    private void RepaintCell()
    {
        if (Displayed && DataGridView != null)
        {
            DataGridView.InvalidateCell(this);
        }
    }

    /// <summary>
    /// Refresh the Cell image
    /// </summary>
    private void RefreshImage()
    {
        if (ActiveFilterType == MenuStrip.FilterType.Loaded)
        {
            _filterImage = Properties.Resources.ColumnHeader_SavedFilters;
        }
        else
        {
            if (ActiveFilterType == MenuStrip.FilterType.None)
            {
                if (ActiveSortType == MenuStrip.SortType.None)
                {
                    _filterImage = Properties.Resources.ColumnHeader_UnFiltered;
                }
                else if (ActiveSortType == MenuStrip.SortType.Asc)
                {
                    _filterImage = Properties.Resources.ColumnHeader_OrderedASC;
                }
                else
                {
                    _filterImage = Properties.Resources.ColumnHeader_OrderedDESC;
                }
            }
            else
            {
                if (ActiveSortType == MenuStrip.SortType.None)
                {
                    _filterImage = Properties.Resources.ColumnHeader_Filtered;
                }
                else if (ActiveSortType == MenuStrip.SortType.Asc)
                {
                    _filterImage = Properties.Resources.ColumnHeader_FilteredAndOrderedASC;
                }
                else
                {
                    _filterImage = Properties.Resources.ColumnHeader_FilteredAndOrderedDESC;
                }
            }
        }
    }

    /// <summary>
    /// Pain method
    /// </summary>
    /// <param name="graphics"></param>
    /// <param name="clipBounds"></param>
    /// <param name="cellBounds"></param>
    /// <param name="rowIndex"></param>
    /// <param name="cellState"></param>
    /// <param name="value"></param>
    /// <param name="formattedValue"></param>
    /// <param name="errorText"></param>
    /// <param name="cellStyle"></param>
    /// <param name="advancedBorderStyle"></param>
    /// <param name="paintParts"></param>
    protected override void Paint(
        Graphics graphics,
        Rectangle clipBounds,
        Rectangle cellBounds,
        int rowIndex,
        DataGridViewElementStates cellState,
        object? value,
        object? formattedValue,
        string? errorText,
        DataGridViewCellStyle cellStyle,
        DataGridViewAdvancedBorderStyle advancedBorderStyle,
        DataGridViewPaintParts paintParts)
    {
        if (SortGlyphDirection != SortOrder.None)
        {
            SortGlyphDirection = SortOrder.None;
        }

        base.Paint(graphics, clipBounds, cellBounds, rowIndex,
            cellState, value, formattedValue,
            errorText, cellStyle, advancedBorderStyle, paintParts);

        // By default, skip Bitmap columns unless KryptonAdvancedDataGridView.FilterAndSortOnBitmapColumns is true
        if (OwningColumn?.ValueType == typeof(Bitmap) &&
            (DataGridView is not KryptonAdvancedDataGridView advancedGrid || !advancedGrid.FilterAndSortOnBitmapColumns))
        {
            return;
        }

        if (FilterAndSortEnabled && paintParts.HasFlag(DataGridViewPaintParts.ContentBackground))
        {
            _filterButtonOffsetBounds = GetFilterBounds(true);
            _filterButtonImageBounds = GetFilterBounds(false);
            Rectangle buttonBounds = _filterButtonOffsetBounds;
            if (clipBounds.IntersectsWith(buttonBounds))
            {
                ControlPaint.DrawBorder(graphics, buttonBounds, Color.Gray, ButtonBorderStyle.Solid);
                buttonBounds.Inflate(-1, -1);
                using (Brush b = new SolidBrush(_filterButtonOver ? Color.WhiteSmoke : Color.White))
                    graphics.FillRectangle(b, buttonBounds);
                graphics.DrawImage(_filterImage, buttonBounds);
            }
        }
    }

    /// <summary>
    /// Get the ColumnHeaderCell Bounds
    /// </summary>
    /// <param name="withOffset"></param>
    /// <returns></returns>
    private Rectangle GetFilterBounds(bool withOffset = true)
    {
        Rectangle cell = DataGridView!.GetCellDisplayRectangle(ColumnIndex, -1, false);

        Point p = new Point(
            (withOffset ? cell.Right : cell.Width) - _filterButtonImageSize.Width - _filterButtonMargin.Right,
            (withOffset ? cell.Bottom : cell.Height) - _filterButtonImageSize.Height - _filterButtonMargin.Bottom);

        return new Rectangle(p, _filterButtonImageSize);
    }

    #endregion

    #region Mouse Events

    /// <summary>
    /// OnMouseMove event
    /// </summary>
    /// <param name="e"></param>
    protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
    {
        if (FilterAndSortEnabled)
        {
            if (_filterButtonImageBounds.Contains(e.X, e.Y) && !_filterButtonOver)
            {
                _filterButtonOver = true;
                RepaintCell();
            }
            else if (!_filterButtonImageBounds.Contains(e.X, e.Y) && _filterButtonOver)
            {
                _filterButtonOver = false;
                RepaintCell();
            }
        }
        base.OnMouseMove(e);
    }

    /// <summary>
    /// OnMouseDown event
    /// </summary>
    /// <param name="e"></param>
    protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
    {
        if (FilterAndSortEnabled && _filterButtonImageBounds.Contains(e.X, e.Y))
        {
            if (e.Button == MouseButtons.Left && !_filterButtonPressed)
            {
                _filterButtonPressed = true;
                _filterButtonOver = true;
                RepaintCell();
            }
        }
        else
        {
            base.OnMouseDown(e);
        }
    }

    /// <summary>
    /// OnMouseUp event
    /// </summary>
    /// <param name="e"></param>
    protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
    {
        if (FilterAndSortEnabled && e.Button == MouseButtons.Left && _filterButtonPressed)
        {
            _filterButtonPressed = false;
            _filterButtonOver = false;
            RepaintCell();
            if (_filterButtonImageBounds.Contains(e.X, e.Y) && FilterPopup != null && OwningColumn != null)
            {
                FilterPopup(this, new ColumnHeaderCellEventArgs(MenuStrip, OwningColumn));
            }
        }
        base.OnMouseUp(e);
    }

    /// <summary>
    /// OnMouseLeave event
    /// </summary>
    /// <param name="rowIndex"></param>
    protected override void OnMouseLeave(int rowIndex)
    {
        if (FilterAndSortEnabled && _filterButtonOver)
        {
            _filterButtonOver = false;
            RepaintCell();
        }

        base.OnMouseLeave(rowIndex);
    }

    #endregion
}
