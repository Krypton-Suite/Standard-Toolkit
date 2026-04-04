#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

// Control specific using statements

#if NETFRAMEWORK
using System.Web.Script.Serialization;
#else
using System.Text.Json;
#endif

namespace Krypton.Utilities;

/// <summary>
/// Extends <see cref="Krypton.Toolkit.KryptonDataGridView"/> with Excel-style column filtering, multi-column sort composition,
/// and optional integration with <see cref="KryptonAdvancedDataGridViewSearchToolBar"/> for in-grid search.
/// </summary>
/// <remarks>
/// <para>
/// Filter and sort are applied by composing expressions and assigning them to the data source:
/// <see cref="P:System.Windows.Forms.BindingSource.Filter"/> / <see cref="P:System.Windows.Forms.BindingSource.Sort"/> when <see cref="P:System.Windows.Forms.DataGridView.DataSource"/> is a <see cref="T:System.Windows.Forms.BindingSource"/>;
/// <see cref="P:System.Data.DataView.RowFilter"/> / <see cref="P:System.Data.DataView.Sort"/> when it is a <see cref="T:System.Data.DataView"/>;
/// or <see cref="P:System.Data.DataTable.DefaultView"/> row filter and sort when it is a <see cref="T:System.Data.DataTable"/> (non-null default view).
/// </para>
/// <para>
/// Use <see cref="SortStringChanged"/> and <see cref="FilterStringChanged"/> to observe changes; set <see cref="KryptonAdvancedDataGridView.SortEventArgs.Cancel"/> or <see cref="KryptonAdvancedDataGridView.FilterEventArgs.Cancel"/> to skip updating the bound source.
/// </para>
/// </remarks>
/// <seealso cref="KryptonAdvancedDataGridViewSearchToolBar"/>
/// <seealso cref="TranslationKey"/>
/// <seealso href="Documents/KryptonAdvancedDataGridView-Developer-Guide.md">KryptonAdvancedDataGridView developer guide</seealso>
[DesignerCategory("code")]
public class KryptonAdvancedDataGridView : KryptonDataGridView
{
    #region Instance Fields

    private readonly List<string> _sortOrderList = [];
    private readonly List<string> _filterOrderList = [];
    private readonly List<string> _filteredColumns = [];
    private List<MenuStrip> _menuStripToDispose = [];

    private bool _loadedFilter;
    private string? _sortString;
    private string? _filterString;

    private bool _sortStringChangedInvokeBeforeDatasourceUpdate = true;
    private bool _filterStringChangedInvokeBeforeDatasourceUpdate = true;

    #endregion

    #region Events

    /// <summary>
    /// Provides data for <see cref="KryptonAdvancedDataGridView.SortStringChanged"/>.
    /// </summary>
    public class SortEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the composed ADO.NET-style sort expression for the bound list (e.g. <see cref="P:System.Windows.Forms.BindingSource.Sort"/>).
        /// </summary>
        public string? SortString { get; set; }

        /// <summary>
        /// When <see langword="true"/>, the control does not assign the sort to the data source.
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>Initializes a new instance of the <see cref="SortEventArgs"/> class.</summary>
        public SortEventArgs()
        {
            SortString = null;
            Cancel = false;
        }
    }

    /// <summary>
    /// Provides data for <see cref="KryptonAdvancedDataGridView.FilterStringChanged"/>.
    /// </summary>
    public class FilterEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the composed row filter expression (e.g. <see cref="P:System.Windows.Forms.BindingSource.Filter"/> or <see cref="P:System.Data.DataView.RowFilter"/>).
        /// </summary>
        public string? FilterString { get; set; }

        /// <summary>
        /// When <see langword="true"/>, the control does not assign the filter to the data source.
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>Initializes a new instance of the <see cref="FilterEventArgs"/> class.</summary>
        public FilterEventArgs()
        {
            FilterString = null;
            Cancel = false;
        }
    }

    /// <summary>
    /// Occurs when the aggregate sort expression changes, before or after the data source is updated depending on <see cref="SortStringChangedInvokeBeforeDatasourceUpdate"/>.
    /// </summary>
    public event EventHandler<SortEventArgs>? SortStringChanged;

    /// <summary>
    /// Occurs when the aggregate filter expression changes, before or after the data source is updated depending on <see cref="FilterStringChangedInvokeBeforeDatasourceUpdate"/>.
    /// </summary>
    public event EventHandler<FilterEventArgs>? FilterStringChanged;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonAdvancedDataGridView"/> class.</summary>
    public KryptonAdvancedDataGridView()
    {
        //System.Windows.Forms.RightToLeft = System.Windows.Forms.RightToLeft.No;
    }

    #endregion

    #region Implementation

    /// <summary>
    /// Default UI strings for column header menus and the custom filter dialog. Keys are the string names of <see cref="TranslationKey"/> members (for example <c>nameof(TranslationKey.KryptonAdvancedDataGridViewSortTextAscending)</c>).
    /// Merge overrides with <see cref="SetTranslations"/> or <see cref="LoadTranslationsFromFile"/>.
    /// </summary>
    public static Dictionary<string, string> Translations = new()
    {
        { nameof(TranslationKey.KryptonAdvancedDataGridViewSortDateTimeAscending), "Sort Oldest to Newest" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewSortDateTimeDescending), "Sort Newest to Oldest" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewSortBoolAscending), "Sort by False/True" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewSortBoolDescending), "Sort by True/False" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewSortNumAscending), "Sort Smallest to Largest" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewSortNumDescending), "Sort Largest to Smallest" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewSortTextAscending), "Sort А to Z" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewSortTextDescending), "Sort Z to A" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewAddCustomFilter), "Add a Custom Filter" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewCustomFilter), "Custom Filter" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewClearFilter), "Clear Filter" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewClearSort), "Clear Sort" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewButtonFilter), "Filter" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewButtonUndoFilter), "Cancel" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewNodeSelectAll), "(Select All)" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewNodeSelectEmpty), "(Blanks)" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewNodeSelectTrue), "True" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewNodeSelectFalse), "False" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewFilterChecklistDisable), "Filter list is disabled" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewEquals), "equals" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewDoesNotEqual), "does not equal" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewEarlierThan), "earlier than" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewEarlierThanOrEqualTo), "earlier than or equal to" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewLaterThan), "later than"},
        { nameof(TranslationKey.KryptonAdvancedDataGridViewLaterThanOrEqualTo), "later than or equal to" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewBetween), "between" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewGreaterThan), "greater than" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewGreaterThanOrEqualTo), "greater than or equal to" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewLessThan), "less than" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewLessThanOrEqualTo), "less than or equal to" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewBeginsWith), "begins with" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewDoesNotBeginWith), "does not begin with" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewEndsWith), "ends with" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewDoesNotEndWith), "does not end with" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewContains), "contains" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewDoesNotContain), "does not contain" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewInvalidValue), "Invalid Value" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewFilterStringDescription), "Show rows where value {0} \"{1}\"" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewFormTitle), "Custom Filter" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewLabelColumnNameText), "Show rows where value" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewLabelAnd), "And" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewButtonOk), "OK" },
        { nameof(TranslationKey.KryptonAdvancedDataGridViewButtonCancel), "Cancel" }
    };

    #endregion

    #region Implemntation

    #region translations methods

    /// <summary>
    /// Merges entries into <see cref="Translations"/> for keys that already exist; unknown keys are ignored.
    /// </summary>
    /// <param name="translations">Localized strings, keyed by the same names as <see cref="TranslationKey"/>.</param>
    public static void SetTranslations(IDictionary<string, string>? translations)
    {
        //set localization strings
        if (translations != null)
        {
            foreach (KeyValuePair<string, string> translation in translations)
            {
                if (Translations.ContainsKey(translation.Key))
                {
                    Translations[translation.Key] = translation.Value;
                }
            }
        }
    }

    /// <summary>Returns the live <see cref="Translations"/> dictionary.</summary>
    /// <returns>The static translation map used by filter and sort UI.</returns>
    public static IDictionary<string, string> GetTranslations()
    {
        return Translations;
    }

    /// <summary>
    /// Loads a JSON object of string keys and string values, merges recognized keys, and fills any missing keys from <see cref="GetTranslations"/>.
    /// </summary>
    /// <param name="filename">Path to the JSON file.</param>
    /// <returns>A new dictionary suitable for passing to <see cref="SetTranslations"/>.</returns>
    public static IDictionary<string, string> LoadTranslationsFromFile(string filename)
    {
        IDictionary<string, string> ret = new Dictionary<string, string>();

        if (!String.IsNullOrEmpty(filename))
        {
            //deserialize the file
            try
            {
                string jsonText = File.ReadAllText(filename);
#if NETFRAMEWORK
                Dictionary<string, string> translations =
                    new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(jsonText);
#else
                Dictionary<string, string>? translations =
                    JsonSerializer.Deserialize<Dictionary<string, string>>(jsonText);
#endif
                foreach (KeyValuePair<string, string> translation in translations!)
                {
                    if (!ret.ContainsKey(translation.Key) && Translations.ContainsKey(translation.Key))
                    {
                        ret.Add(translation.Key, translation.Value);
                    }
                }
            }
            catch (Exception e)
            {
                KryptonExceptionHandler.CaptureException(e);
            }
        }

        //add default translations if not in files
        foreach (KeyValuePair<string, string> translation in GetTranslations())
        {
            if (!ret.ContainsKey(translation.Key))
            {
                ret.Add(translation.Key, translation.Value);
            }
        }

        return ret;
    }

    #endregion


    #region public Helper methods

    /// <summary>Sets <see cref="P:System.Windows.Forms.Control.DoubleBuffered"/> to <see langword="true"/> to reduce flicker.</summary>
    public void SetDoubleBuffered()
    {
        DoubleBuffered = true;
    }

    #endregion


    #region public Filter and Sort methods

    /// <summary>
    /// Gets or sets whether <see cref="SortStringChanged"/> is raised before (default) or after the sort is applied to the data source.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool SortStringChangedInvokeBeforeDatasourceUpdate
    {
        get => _sortStringChangedInvokeBeforeDatasourceUpdate;
        set => _sortStringChangedInvokeBeforeDatasourceUpdate = value;
    }

    /// <summary>
    /// Gets or sets whether <see cref="FilterStringChanged"/> is raised before (default) or after the filter is applied to the data source.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool FilterStringChangedInvokeBeforeDatasourceUpdate
    {
        get => _filterStringChangedInvokeBeforeDatasourceUpdate;
        set => _filterStringChangedInvokeBeforeDatasourceUpdate = value;
    }

    /// <summary>
    /// Disable a Filter and Sort on a DataGridViewColumn
    /// </summary>
    /// <param name="column"></param>
    public void DisableFilterAndSort(DataGridViewColumn column)
    {
        if (Columns.Contains(column))
        {
            if (column.HeaderCell is KryptonColumnHeaderCell cell)
            {
                if (cell.FilterAndSortEnabled && (cell.SortString!.Length > 0 || cell.FilterString!.Length > 0))
                {
                    CleanFilter(true);
                    cell.FilterAndSortEnabled = false;
                }
                else
                {
                    cell.FilterAndSortEnabled = false;
                }

                _filterOrderList.Remove(column.Name);
                _sortOrderList.Remove(column.Name);
                _filteredColumns.Remove(column.Name);
            }
        }
    }

    /// <summary>
    /// Enable a Filter and Sort on a DataGridViewColumn
    /// </summary>
    /// <param name="column"></param>
    public void EnableFilterAndSort(DataGridViewColumn column)
    {
        if (Columns.Contains(column))
        {
            if (column.HeaderCell is KryptonColumnHeaderCell cell)
            {
                if (!cell.FilterAndSortEnabled && (cell.FilterString!.Length > 0 || cell.SortString!.Length > 0))
                {
                    CleanFilter(true);
                }

                cell.FilterAndSortEnabled = true;
                _filteredColumns.Remove(column.Name);

                SetFilterDateAndTimeEnabled(column, cell.IsFilterDateAndTimeEnabled);
                SetSortEnabled(column, cell.IsSortEnabled);
                SetFilterEnabled(column, cell.IsFilterEnabled);
            }
            else
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
                cell = new KryptonColumnHeaderCell(column.HeaderCell, true);
                cell.SortChanged += Cell_SortChanged;
                cell.FilterChanged += Cell_FilterChanged;
                cell.FilterPopup += Cell_FilterPopup;
                column.MinimumWidth = cell.MinimumSize.Width;
                if (ColumnHeadersHeight < cell.MinimumSize.Height)
                {
                    ColumnHeadersHeight = cell.MinimumSize.Height;
                }

                column.HeaderCell = cell;
            }
        }
    }

    /// <summary>
    /// Enabled or disable Filter and Sort capabilities on a DataGridViewColumn
    /// </summary>
    /// <param name="column"></param>
    /// <param name="enabled"></param>
    public void SetFilterAndSortEnabled(DataGridViewColumn column, bool enabled)
    {
        if (enabled)
        {
            EnableFilterAndSort(column);
        }
        else
        {
            DisableFilterAndSort(column);
        }
    }

    /// <summary>
    /// Disable a Filter checklist on a DataGridViewColumn
    /// </summary>
    /// <param name="column"></param>
    public void DisableFilterChecklist(DataGridViewColumn? column)
    {
        if (column != null && Columns.Contains(column))
        {
            if (column.HeaderCell is KryptonColumnHeaderCell cell)
            {
                cell.SetFilterChecklistEnabled(false);
            }
        }
    }

    /// <summary>
    /// Enable a Filter checklist on a DataGridViewColumn
    /// </summary>
    /// <param name="column"></param>
    public void EnableFilterChecklist(DataGridViewColumn? column)
    {
        if (column != null && Columns.Contains(column))
        {
            if (column.HeaderCell is KryptonColumnHeaderCell cell)
            {
                cell.SetFilterChecklistEnabled(true);
            }
        }
    }

    /// <summary>
    /// Enabled or disable Filter checklist capabilities on a DataGridViewColumn
    /// </summary>
    /// <param name="column"></param>
    /// <param name="enabled"></param>
    public void SetFilterChecklistEnabled(DataGridViewColumn? column, bool enabled)
    {
        if (enabled)
        {
            EnableFilterChecklist(column);
        }
        else
        {
            DisableFilterChecklist(column);
        }
    }

    /// <summary>
    /// Set Filter checklist nodes max on a DataGridViewColumn
    /// </summary>
    /// <param name="column"></param>
    /// <param name="maxNodes"></param>
    public void SetFilterChecklistNodesMax(DataGridViewColumn column, int maxNodes)
    {
        if (Columns.Contains(column))
        {
            if (column.HeaderCell is KryptonColumnHeaderCell cell)
            {
                cell.SetFilterChecklistNodesMax(maxNodes);
            }
        }
    }

    /// <summary>
    /// Set Filter checklist nodes max
    /// </summary>
    /// <param name="maxNodes"></param>
    public void SetFilterChecklistNodesMax(int maxNodes)
    {
        foreach (KryptonColumnHeaderCell c in FilterableCells)
        {
            c.SetFilterChecklistNodesMax(maxNodes);
        }
    }

    /// <summary>
    /// Enable or disable Filter checklist nodes max on a DataGridViewColumn
    /// </summary>
    /// <param name="column"></param>
    /// <param name="enabled"></param>
    public void EnabledFilterChecklistNodesMax(DataGridViewColumn column, bool enabled)
    {
        if (Columns.Contains(column))
        {
            if (column.HeaderCell is KryptonColumnHeaderCell cell)
            {
                cell.EnabledFilterChecklistNodesMax(enabled);
            }
        }
    }

    /// <summary>
    /// Enable or disable Filter checklist nodes max
    /// </summary>
    /// <param name="enabled"></param>
    public void EnabledFilterChecklistNodesMax(bool enabled)
    {
        foreach (KryptonColumnHeaderCell c in FilterableCells)
        {
            c.EnabledFilterChecklistNodesMax(enabled);
        }
    }

    /// <summary>
    /// Disable a Filter custom on a DataGridViewColumn
    /// </summary>
    /// <param name="column"></param>
    public void DisableFilterCustom(DataGridViewColumn? column)
    {
        if (column != null && Columns.Contains(column))
        {
            if (column.HeaderCell is KryptonColumnHeaderCell cell)
            {
                cell.SetFilterCustomEnabled(false);
            }
        }
    }

    /// <summary>
    /// Enable a Filter custom on a DataGridViewColumn
    /// </summary>
    /// <param name="column"></param>
    public void EnableFilterCustom(DataGridViewColumn? column)
    {
        if (column != null && Columns.Contains(column))
        {
            if (column.HeaderCell is KryptonColumnHeaderCell cell)
            {
                cell.SetFilterCustomEnabled(true);
            }
        }
    }

    /// <summary>
    /// Enabled or disable Filter custom capabilities on a DataGridViewColumn
    /// </summary>
    /// <param name="column"></param>
    /// <param name="enabled"></param>
    public void SetFilterCustomEnabled(DataGridViewColumn? column, bool enabled)
    {
        if (enabled)
        {
            EnableFilterCustom(column);
        }
        else
        {
            DisableFilterCustom(column);
        }
    }

    /// <summary>
    /// Set nodes to enable TextChanged delay on filter checklist on a DataGridViewColumn
    /// </summary>
    /// <param name="column"></param>
    /// <param name="numNodes"></param>
    public void SetFilterChecklistTextFilterTextChangedDelayNodes(DataGridViewColumn? column, int numNodes)
    {
        if (column != null && Columns.Contains(column))
        {
            if (column.HeaderCell is KryptonColumnHeaderCell cell)
            {
                cell.TextFilterTextChangedDelayNodes = numNodes;
            }
        }
    }

    /// <summary>
    /// Set nodes to enable TextChanged delay on filter checklist
    /// </summary>
    /// <param name="numNodes"></param>
    public void SetFilterChecklistTextFilterTextChangedDelayNodes(int numNodes)
    {
        foreach (KryptonColumnHeaderCell c in FilterableCells)
        {
            c.TextFilterTextChangedDelayNodes = numNodes;
        }
    }

    /// <summary>
    /// Disable TextChanged delay on filter checklist on a DataGridViewColumn
    /// </summary>
    /// <param name="column"></param>
    public void SetFilterChecklistTextFilterTextChangedDelayDisabled(DataGridViewColumn column)
    {
        if (Columns.Contains(column))
        {
            if (column.HeaderCell is KryptonColumnHeaderCell cell)
            {
                cell.SetTextFilterTextChangedDelayNodesDisabled();
            }
        }
    }

    /// <summary>
    /// Disable TextChanged delay on filter checklist
    /// </summary>
    public void SetFilterChecklistTextFilterTextChangedDelayDisabled()
    {
        foreach (KryptonColumnHeaderCell c in FilterableCells)
        {
            c.SetTextFilterTextChangedDelayNodesDisabled();
        }
    }

    /// <summary>
    /// Set TextChanged delay milliseconds on filter checklist on a DataGridViewColumn
    /// </summary>
    /// <param name="column"></param>
    /// <param name="milliseconds"></param>
    public void SetFilterChecklistTextFilterTextChangedDelayMs(DataGridViewColumn? column, int milliseconds)
    {
        if (column != null && Columns.Contains(column))
        {
            if (column.HeaderCell is KryptonColumnHeaderCell cell)
            {
                cell.SetTextFilterTextChangedDelayMs(milliseconds);
            }
        }
    }

    /// <summary>
    /// Set TextChanged delay milliseconds on filter checklist
    /// </summary>
    public void SetFilterChecklistTextFilterTextChangedDelayMs(int milliseconds)
    {
        foreach (KryptonColumnHeaderCell c in FilterableCells)
        {
            c.SetTextFilterTextChangedDelayMs(milliseconds);
        }
    }

    /// <summary>
    /// Applies saved filter and sort expressions and marks header cells as loaded until the user changes filter state.
    /// </summary>
    /// <param name="filter">Aggregate filter string, or <see langword="null"/> to skip.</param>
    /// <param name="sorting">Aggregate sort string, or <see langword="null"/> to skip.</param>
    public void LoadFilterAndSort(string? filter, string? sorting)
    {
        foreach (KryptonColumnHeaderCell c in FilterableCells)
        {
            c.SetLoadedMode(true);
        }

        _filteredColumns.Clear();

        _filterOrderList.Clear();
        _sortOrderList.Clear();

        if (filter != null)
        {
            FilterString = filter;
        }

        if (sorting != null)
        {
            SortString = sorting;
        }

        _loadedFilter = true;
    }

    /// <summary>Clears loaded mode, internal order lists, and all column filter and sort state.</summary>
    public void CleanFilterAndSort()
    {
        foreach (KryptonColumnHeaderCell c in FilterableCells)
        {
            c.SetLoadedMode(false);
        }

        _filteredColumns.Clear();
        _filterOrderList.Clear();
        _sortOrderList.Clear();

        _loadedFilter = false;

        CleanFilter();
        CleanSort();
    }

    /// <summary>
    /// Enables or disables NOT IN-style semantics for checklist filters on all filterable columns.
    /// </summary>
    /// <param name="enabled"><see langword="true"/> to use NOT IN logic; otherwise <see langword="false"/>.</param>
    public void SetMenuStripFilterNotInLogic(bool enabled)
    {
        foreach (KryptonColumnHeaderCell c in FilterableCells)
        {
            c.IsMenuStripFilterNOTINLogicEnabled = enabled;
        }
    }

    /// <summary>
    /// Gets or sets whether new columns receive filter and sort UI. Also used when replacing header cells in <see cref="EnableFilterAndSort"/>.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool FilterAndSortEnabled
    {
        get => _filterAndSortEnabled;
        set => _filterAndSortEnabled = value;
    }
    private bool _filterAndSortEnabled = true;

    private bool _filterAndSortOnBitmapColumns;

    /// <summary>
    /// Gets or sets whether filter and sort UI is shown on columns whose <see cref="P:System.Windows.Forms.DataGridViewColumn.ValueType"/> is <see cref="T:System.Drawing.Bitmap"/>.
    /// Default is <see langword="false"/> (image columns have no header drop-down).
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool FilterAndSortOnBitmapColumns
    {
        get => _filterAndSortOnBitmapColumns;
        set
        {
            if (_filterAndSortOnBitmapColumns == value)
            {
                return;
            }

            _filterAndSortOnBitmapColumns = value;
            for (int i = 0; i < Columns.Count; i++)
            {
                InvalidateCell(i, -1);
            }
        }
    }

    #endregion


    #region public Sort methods

    /// <summary>
    /// Gets the composed multi-column sort expression. Applied to <see cref="P:System.Windows.Forms.BindingSource.Sort"/>,
    /// <see cref="P:System.Data.DataView.Sort"/>, or <see cref="P:System.Data.DataTable.DefaultView"/> when the data source matches.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? SortString
    {
        get => !string.IsNullOrEmpty(_sortString) ? _sortString : string.Empty;
        private set
        {
            string? old = value;
            if (old != _sortString)
            {
                _sortString = value;

                TriggerSortStringChanged();
            }
        }
    }

    /// <summary>
    /// Raises <see cref="SortStringChanged"/> (subject to <see cref="SortStringChangedInvokeBeforeDatasourceUpdate"/>) and applies the current sort to the data source when not canceled.
    /// </summary>
    public void TriggerSortStringChanged()
    {
        //call event handler if one is attached
        SortEventArgs sortEventArgs = new SortEventArgs
        {
            SortString = _sortString,
            Cancel = false
        };
        //invoke SortStringChanged
        if (_sortStringChangedInvokeBeforeDatasourceUpdate)
        {
            if (SortStringChanged != null)
            {
                SortStringChanged.Invoke(this, sortEventArgs);
            }
        }
        //sort datasource
        if (sortEventArgs.Cancel == false)
        {
            if (DataSource is BindingSource datasource)
            {
                datasource.Sort = sortEventArgs.SortString;
            }
            else if (DataSource is DataView dataView)
            {
                dataView.Sort = sortEventArgs.SortString ?? string.Empty;
            }
            else if (DataSource is DataTable { DefaultView: not null } dataTable)
            {
                dataTable.DefaultView.Sort = sortEventArgs.SortString ?? string.Empty;
            }
        }
        //invoke SortStringChanged
        if (!_sortStringChangedInvokeBeforeDatasourceUpdate)
        {
            if (SortStringChanged != null)
            {
                SortStringChanged.Invoke(this, sortEventArgs);
            }
        }
    }

    /// <summary>
    /// Enabled or disable Sort capabilities for a DataGridViewColumn
    /// </summary>
    /// <param name="column"></param>
    /// <param name="enabled"></param>
    public void SetSortEnabled(DataGridViewColumn? column, bool enabled)
    {
        if (column != null && Columns.Contains(column))
        {
            if (column.HeaderCell is KryptonColumnHeaderCell cell)
            {
                cell.SetSortEnabled(enabled);
            }
        }
    }

    /// <summary>
    /// Applies ascending sort for the column via the header menu logic.
    /// </summary>
    /// <param name="column">The column to sort, or <see langword="null"/> to no-op.</param>
    public void SortAscending(DataGridViewColumn? column)
    {
        if (column != null && Columns.Contains(column))
        {
            if (column.HeaderCell is KryptonColumnHeaderCell cell)
            {
                cell.SortASC();
            }
        }
    }

    /// <summary>
    /// Applies descending sort for the column via the header menu logic.
    /// </summary>
    /// <param name="column">The column to sort, or <see langword="null"/> to no-op.</param>
    public void SortDescending(DataGridViewColumn? column)
    {
        if (column != null && Columns.Contains(column))
        {
            if (column.HeaderCell is KryptonColumnHeaderCell cell)
            {
                cell.SortDESC();
            }
        }
    }

    /// <summary>
    /// Clean all Sort on specific column
    /// </summary>
    /// <param name="column"></param>
    /// <param name="fireEvent"></param>
    public void CleanSort(DataGridViewColumn? column, bool fireEvent)
    {
        if (column != null && Columns.Contains(column))
        {
            if (column.HeaderCell is KryptonColumnHeaderCell cell && FilterableCells.Contains(cell))
            {
                cell.CleanSort();
                //remove column from sorted list
                _sortOrderList.Remove(column.Name);
            }
        }

        if (fireEvent)
        {
            SortString = BuildSortString();
        }
        else
        {
            _sortString = BuildSortString();
        }
    }

    /// <summary>
    /// Clean all Sort on specific column
    /// </summary>
    /// <param name="column"></param>
    public void CleanSort(DataGridViewColumn? column)
    {
        CleanSort(column, true);
    }

    /// <summary>
    /// Clean all Sort on all columns
    /// </summary>
    /// <param name="fireEvent"></param>
    public void CleanSort(bool fireEvent)
    {
        foreach (KryptonColumnHeaderCell c in FilterableCells)
        {
            c.CleanSort();
        }

        _sortOrderList.Clear();

        if (fireEvent)
        {
            SortString = null;
        }
        else
        {
            _sortString = null;
        }
    }

    /// <summary>
    /// Clean all Sort on all columns
    /// </summary>
    public void CleanSort()
    {
        CleanSort(true);
    }

    #endregion


    #region public Filter methods

    /// <summary>
    /// Gets the composed row filter (multi-column conditions combined with AND). Applied to <see cref="P:System.Windows.Forms.BindingSource.Filter"/>, <see cref="P:System.Data.DataView.RowFilter"/>, or the table default view when supported.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? FilterString
    {
        get => !String.IsNullOrEmpty(_filterString) ? _filterString : "";
        private set
        {
            string? old = value;
            if (old != _filterString)
            {
                _filterString = value;

                TriggerFilterStringChanged();
            }
        }
    }

    /// <summary>
    /// Raises <see cref="FilterStringChanged"/> (subject to <see cref="FilterStringChangedInvokeBeforeDatasourceUpdate"/>) and applies the current filter to the data source when not canceled.
    /// </summary>
    public void TriggerFilterStringChanged()
    {
        //call event handler if one is attached
        FilterEventArgs filterEventArgs = new FilterEventArgs
        {
            FilterString = _filterString,
            Cancel = false
        };
        //invoke FilterStringChanged
        if (_filterStringChangedInvokeBeforeDatasourceUpdate)
        {
            if (FilterStringChanged != null)
            {
                FilterStringChanged.Invoke(this, filterEventArgs);
            }
        }
        //filter datasource
        if (filterEventArgs.Cancel == false)
        {
            if (DataSource is BindingSource bindingsource)
            {
                bindingsource.Filter = filterEventArgs.FilterString;
            }
            else if (DataSource is DataView dataview)
            {
                dataview.RowFilter = filterEventArgs.FilterString;
            }
            else if (DataSource is DataTable { DefaultView: not null } datatable)
            {
                datatable.DefaultView.RowFilter = filterEventArgs.FilterString;
            }
        }
        //invoke FilterStringChanged
        if (!_filterStringChangedInvokeBeforeDatasourceUpdate)
        {
            if (FilterStringChanged != null)
            {
                FilterStringChanged.Invoke(this, filterEventArgs);
            }
        }
    }

    /// <summary>
    /// Set FilterDateAndTime status for a DataGridViewColumn
    /// </summary>
    /// <param name="column"></param>
    /// <param name="enabled"></param>
    public void SetFilterDateAndTimeEnabled(DataGridViewColumn? column, bool enabled)
    {
        if (column != null && Columns.Contains(column))
        {
            if (column.HeaderCell is KryptonColumnHeaderCell cell)
            {
                cell.IsFilterDateAndTimeEnabled = enabled;
            }
        }
    }

    /// <summary>
    /// Enable or disable Filter capabilities for a DataGridViewColumn
    /// </summary>
    /// <param name="column"></param>
    /// <param name="enabled"></param>
    public void SetFilterEnabled(DataGridViewColumn column, bool enabled)
    {
        if (Columns.Contains(column))
        {
            if (column.HeaderCell is KryptonColumnHeaderCell cell)
            {
                cell.SetFilterEnabled(enabled);
            }
        }
    }

    /// <summary>
    /// Enable or disable Text filter on checklist remove node mode for a DataGridViewColumn
    /// </summary>
    /// <param name="column"></param>
    /// <param name="enabled"></param>
    public void SetChecklistTextFilterRemoveNodesOnSearchMode(DataGridViewColumn? column, bool enabled)
    {
        if (column != null && Columns.Contains(column))
        {
            if (column.HeaderCell is KryptonColumnHeaderCell cell)
            {
                cell.SetChecklistTextFilterRemoveNodesOnSearchMode(enabled);
            }
        }
    }

    /// <summary>
    /// Clean Filter on specific column
    /// </summary>
    /// <param name="column"></param>
    /// <param name="fireEvent"></param>
    public void CleanFilter(DataGridViewColumn column, bool fireEvent)
    {
        if (Columns.Contains(column))
        {
            if (column.HeaderCell is KryptonColumnHeaderCell cell)
            {
                cell.CleanFilter();
                //remove column from filtered list
                _filterOrderList.Remove(column.Name);
            }
        }

        if (fireEvent)
        {
            FilterString = BuildFilterString();
        }
        else
        {
            _filterString = BuildFilterString();
        }
    }

    /// <summary>
    /// Clean Filter on specific column
    /// </summary>
    /// <param name="column"></param>
    public void CleanFilter(DataGridViewColumn column)
    {
        CleanFilter(column, true);
    }

    /// <summary>
    /// Clean Filter on all columns
    /// </summary>
    /// <param name="fireEvent"></param>
    public void CleanFilter(bool fireEvent)
    {
        foreach (KryptonColumnHeaderCell c in FilterableCells)
        {
            c.CleanFilter();
        }
        _filterOrderList.Clear();

        if (fireEvent)
        {
            FilterString = null;
        }
        else
        {
            _filterString = null;
        }
    }

    /// <summary>Clears filters on all columns and raises the aggregate filter change.</summary>
    public void CleanFilter()
    {
        CleanFilter(true);
    }

    /// <summary>
    /// Set the text filter search nodes behaviour
    /// </summary>
    public void SetTextFilterRemoveNodesOnSearch(DataGridViewColumn? column, bool enabled)
    {
        if (column != null && Columns.Contains(column))
        {
            if (column.HeaderCell is KryptonColumnHeaderCell cell)
            {
                cell.DoesTextFilterRemoveNodesOnSearch = enabled;
            }
        }
    }

    /// <summary>
    /// Get the text filter search nodes behaviour
    /// </summary>
    public bool? GetTextFilterRemoveNodesOnSearch(DataGridViewColumn column)
    {
        bool? ret = null;
        if (Columns.Contains(column))
        {
            if (column.HeaderCell is KryptonColumnHeaderCell cell)
            {
                ret = cell.DoesTextFilterRemoveNodesOnSearch;
            }
        }
        return ret;
    }

    #endregion


    #region public Find methods

    /// <summary>
    /// Searches displayed cell text from a start position; typically used with <see cref="KryptonAdvancedDataGridViewSearchToolBar"/> for find-next behavior.
    /// </summary>
    /// <param name="valueToFind">Text to match against <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValue"/>.</param>
    /// <param name="columnName">Bound column <see cref="P:System.Windows.Forms.DataGridViewColumn.Name"/> to search, or <see langword="null"/> to search all visible columns.</param>
    /// <param name="rowIndex">Starting row index (clamped to zero or greater).</param>
    /// <param name="columnIndex">Starting column index when searching all columns.</param>
    /// <param name="isWholeWordSearch">When <see langword="true"/>, requires an exact match of the formatted string.</param>
    /// <param name="isCaseSensitive">When <see langword="true"/>, comparison is case-sensitive.</param>
    /// <returns>The first matching cell, or <see langword="null"/> if none.</returns>
    public DataGridViewCell? FindCell(string? valueToFind, string? columnName, int rowIndex, int columnIndex, bool isWholeWordSearch, bool isCaseSensitive)
    {
        if (valueToFind != null && RowCount > 0 && ColumnCount > 0 && (columnName == null || (Columns.Contains(columnName) && Columns[columnName]!.Visible)))
        {
            rowIndex = Math.Max(0, rowIndex);

            if (!isCaseSensitive)
            {
                valueToFind = valueToFind.ToLower();
            }

            if (columnName != null)
            {
                int c = Columns[columnName]!.Index;
                if (columnIndex > c)
                {
                    rowIndex++;
                }

                for (int r = rowIndex; r < RowCount; r++)
                {
                    string value = Rows[r].Cells[c].FormattedValue?.ToString() ?? string.Empty;
                    if (!isCaseSensitive)
                    {
                        value = value.ToLower();
                    }

                    if ((!isWholeWordSearch && value.Contains(valueToFind)) || value.Equals(valueToFind))
                    {
                        return Rows[r].Cells[c];
                    }
                }
            }
            else
            {
                columnIndex = Math.Max(0, columnIndex);

                for (int r = rowIndex; r < RowCount; r++)
                {
                    for (int c = columnIndex; c < ColumnCount; c++)
                    {
                        if (!Rows[r].Cells[c].Visible)
                        {
                            continue;
                        }

                        string value = Rows[r].Cells[c].FormattedValue?.ToString() ?? string.Empty;
                        if (!isCaseSensitive)
                        {
                            value = value.ToLower();
                        }

                        if ((!isWholeWordSearch && value.Contains(valueToFind)) || value.Equals(valueToFind))
                        {
                            return Rows[r].Cells[c];
                        }
                    }

                    columnIndex = 0;
                }
            }
        }

        return null;
    }

    #endregion


    #region public Cell methods

    /// <summary>Opens the filter and sort drop-down for the specified column programmatically.</summary>
    /// <param name="column">The column whose header menu should open.</param>
    public void ShowMenuStrip(DataGridViewColumn column)
    {
        if (Columns.Contains(column))
        {
            if (column.HeaderCell is KryptonColumnHeaderCell cell)
            {
                Cell_FilterPopup(cell, new ColumnHeaderCellEventArgs(cell.MenuStrip, column));
            }
        }
    }

    #endregion


    #region cells methods

    /// <summary>
    /// Get all columns
    /// </summary>
    private IEnumerable<KryptonColumnHeaderCell> FilterableCells =>
        from DataGridViewColumn c in Columns
        where c.HeaderCell is KryptonColumnHeaderCell
        select c.HeaderCell as KryptonColumnHeaderCell;

    #endregion


    #region column events

    /// <summary>
    /// Overriden  OnColumnAdded event
    /// </summary>
    /// <param name="e"></param>
    protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
    {
        e.Column.SortMode = DataGridViewColumnSortMode.Programmatic;
        KryptonColumnHeaderCell cell = new KryptonColumnHeaderCell(e.Column.HeaderCell, FilterAndSortEnabled);
        cell.SortChanged += Cell_SortChanged;
        cell.FilterChanged += Cell_FilterChanged;
        cell.FilterPopup += Cell_FilterPopup;
        e.Column.MinimumWidth = cell.MinimumSize.Width;
        if (ColumnHeadersHeight < cell.MinimumSize.Height)
        {
            ColumnHeadersHeight = cell.MinimumSize.Height;
        }

        e.Column.HeaderCell = cell;

        base.OnColumnAdded(e);
    }

    /// <summary>
    /// Overridden OnColumnRemoved event
    /// </summary>
    /// <param name="e"></param>
    protected override void OnColumnRemoved(DataGridViewColumnEventArgs e)
    {
        _filteredColumns.Remove(e.Column.Name);
        _filterOrderList.Remove(e.Column.Name);
        _sortOrderList.Remove(e.Column.Name);

        if (e.Column.HeaderCell is KryptonColumnHeaderCell cell)
        {
            cell.SortChanged -= Cell_SortChanged;
            cell.FilterChanged -= Cell_FilterChanged;
            cell.FilterPopup -= Cell_FilterPopup;

            cell.CleanEvents();
            if (!e.Column.IsDataBound)
            {
                cell.MenuStrip?.Dispose();
            }
            else if (cell.MenuStrip is { } menuStrip)
            {
                _menuStripToDispose.Add(menuStrip);
            }
        }
        base.OnColumnRemoved(e);
    }

    #endregion


    #region rows events

    /// <summary>
    /// Overridden OnRowsAdded event
    /// </summary>
    /// <param name="e"></param>
    protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            _filteredColumns.Clear();
        }

        base.OnRowsAdded(e);
    }

    /// <summary>
    /// Overridden OnRowsRemoved event
    /// </summary>
    /// <param name="e"></param>
    protected override void OnRowsRemoved(DataGridViewRowsRemovedEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            _filteredColumns.Clear();
        }

        base.OnRowsRemoved(e);
    }

    #endregion


    #region cell events

    /// <summary>
    /// Overridden OnCellValueChanged event
    /// </summary>
    /// <param name="e"></param>
    protected override void OnCellValueChanged(DataGridViewCellEventArgs e)
    {
        if (e is { RowIndex: >= 0, ColumnIndex: >= 0 })
        {
            _filteredColumns.Remove(Columns[e.ColumnIndex].Name);
        }

        base.OnCellValueChanged(e);
    }

    #endregion


    #region filter events

    /// <summary>
    /// Build the complete Filter string
    /// </summary>
    /// <returns></returns>
    private string BuildFilterString()
    {
        StringBuilder sb = new StringBuilder("");
        string appx = "";

        foreach (string filterOrder in _filterOrderList)
        {
            DataGridViewColumn? column = Columns[filterOrder];

            if (column?.HeaderCell is KryptonColumnHeaderCell cell)
            {
                if (cell.FilterAndSortEnabled && cell.ActiveFilterType != MenuStrip.FilterType.None)
                {
                    sb.AppendFormat(appx + "(" + cell.FilterString + ")", column.DataPropertyName);
                    appx = " AND ";
                }
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// FilterPopup event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Cell_FilterPopup(object sender, ColumnHeaderCellEventArgs e)
    {
        if (!Columns.Contains(e.Column) || e.FilterMenu is not { } filterMenu)
        {
            return;
        }

        DataGridViewColumn column = e.Column;

        Rectangle rect = GetCellDisplayRectangle(column.Index, -1, true);

        if (_filteredColumns.Contains(column.Name))
        {
            filterMenu.Show(this, rect.Left, rect.Bottom, false);
        }
        else
        {
            _filteredColumns.Add(column.Name);
            if (_filterOrderList.Any() && _filterOrderList.Last() == column.Name)
            {
                filterMenu.Show(this, rect.Left, rect.Bottom, true);
            }
            else
            {
                filterMenu.Show(this, rect.Left, rect.Bottom, MenuStrip.GetValuesForFilter(this, column.Name));
            }
        }
    }

    /// <summary>
    /// FilterChanged event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Cell_FilterChanged(object sender, ColumnHeaderCellEventArgs e)
    {
        if (!Columns.Contains(e.Column) || e.FilterMenu is not { } filterMenu)
        {
            return;
        }

        DataGridViewColumn column = e.Column;

        _filterOrderList.Remove(column.Name);
        if (filterMenu.ActiveFilterType != MenuStrip.FilterType.None)
        {
            _filterOrderList.Add(column.Name);
        }

        FilterString = BuildFilterString();

        if (_loadedFilter)
        {
            _loadedFilter = false;
            foreach (KryptonColumnHeaderCell c in FilterableCells.Where(f => f.MenuStrip != filterMenu))
            {
                c.SetLoadedMode(false);
            }
        }
    }

    #endregion


    #region sort events

    /// <summary>
    /// Build the complete Sort string
    /// </summary>
    /// <returns></returns>
    private string BuildSortString()
    {
        StringBuilder sb = new StringBuilder("");
        string appx = "";

        foreach (string sortOrder in _sortOrderList)
        {
            DataGridViewColumn? column = Columns[sortOrder];

            if (column?.HeaderCell is KryptonColumnHeaderCell cell)
            {
                if (cell.FilterAndSortEnabled && cell.ActiveSortType != MenuStrip.SortType.None)
                {
                    sb.AppendFormat(appx + cell.SortString, column.DataPropertyName);
                    appx = ", ";
                }
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// SortChanged event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Cell_SortChanged(object sender, ColumnHeaderCellEventArgs e)
    {
        if (!Columns.Contains(e.Column) || e.FilterMenu is not MenuStrip filterMenu)
        {
            return;
        }

        DataGridViewColumn column = e.Column;

        _sortOrderList.Remove(column.Name);
        if (filterMenu.ActiveSortType != MenuStrip.SortType.None)
        {
            _sortOrderList.Add(column.Name);
        }

        SortString = BuildSortString();
    }

    #endregion

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override void OnHandleDestroyed(EventArgs e)
    {
        foreach (DataGridViewColumn column in Columns)
        {
            if (column.HeaderCell is KryptonColumnHeaderCell cell)
            {
                cell.SortChanged -= Cell_SortChanged;
                cell.FilterChanged -= Cell_FilterChanged;
                cell.FilterPopup -= Cell_FilterPopup;
            }
        }

        foreach (MenuStrip menuStrip in _menuStripToDispose)
        {
            menuStrip.Dispose();
        }

        _menuStripToDispose.Clear();

        base.OnHandleDestroyed(e);
    }

    /// <inheritdoc />
    protected override void OnDataSourceChanged(EventArgs e)
    {
        foreach (DataGridViewColumn column in Columns)
        {
            KryptonColumnHeaderCell? cell = column.HeaderCell as KryptonColumnHeaderCell;

            _menuStripToDispose = _menuStripToDispose.Where(f => f != cell!.MenuStrip).ToList();
        }

        foreach (MenuStrip menuStrip in _menuStripToDispose)
        {
            menuStrip.Dispose();
        }

        _menuStripToDispose.Clear();
        base.OnDataSourceChanged(e);
    }

    #endregion
}
