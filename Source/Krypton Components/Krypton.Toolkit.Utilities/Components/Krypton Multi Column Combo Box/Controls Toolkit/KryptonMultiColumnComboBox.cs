#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Provides a ComboBox-style control whose drop-down hosts a multi-column
/// <see cref="KryptonDataGridView"/>. Built on <see cref="KryptonComboBoxUserControl"/>.
/// Implements feature request
/// <a href="https://github.com/Krypton-Suite/Standard-Toolkit/issues/4237">#4237</a>.
/// </summary>
/// <remarks>
/// The closed editor shows a single line of text from <see cref="DisplayMember"/> (or the first
/// visible cell). The drop-down shows a themed grid with optional headers. Data binding mirrors a
/// combo via <see cref="DataSource"/>, <see cref="DisplayMember"/>, <see cref="ValueMember"/> and
/// <see cref="SelectedValue"/>. Extra <see cref="KryptonTextBox.ButtonSpecs"/> and palette /
/// rounded-corner styling are inherited from the editor.
/// Documentation: <c>Documents/Development/Krypton-MultiColumnComboBox-Developer-Guide.md</c>.
/// </remarks>
[LookupBindingProperties(nameof(DataSource), nameof(DisplayMember), nameof(ValueMember), nameof(SelectedValue))]
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonComboBox), "ToolboxBitmaps.KryptonComboBox.bmp")]
[DefaultEvent(nameof(SelectedIndexChanged))]
[DefaultProperty(nameof(DataSource))]
[DefaultBindingProperty(nameof(SelectedValue))]
[Designer(typeof(KryptonMultiColumnComboBoxDesigner))]
[DesignerCategory(@"code")]
[Description(@"A ComboBox-style control whose drop-down shows multiple columns in a grid.")]
public class KryptonMultiColumnComboBox : KryptonComboBoxUserControl
{
    #region Static Fields

    private const int DefaultDropDownWidth = 380;
    private const int DefaultDropDownHeight = 220;

    #endregion

    #region Instance Fields

    private readonly KryptonMultiColumnComboBoxDropDown _dropDown;
    private readonly KryptonMultiColumnComboBoxColumnCollection _columns;
    private string _displayMember = string.Empty;
    private string _valueMember = string.Empty;
    private string _displayFormat = string.Empty;
    private bool _autoGenerateColumns = true;
    private bool _commitOnRowClick = true;
    private int _selectedIndex = -1;
    private object? _pendingSelectedValue;
    private bool _hasPendingSelectedValue;
    private bool _suspendColumnLayout;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when <see cref="SelectedIndex"/> changes after a commit or programmatic selection.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the selected index changes.")]
    public event EventHandler? SelectedIndexChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonMultiColumnComboBox"/> class.
    /// </summary>
    public KryptonMultiColumnComboBox()
    {
        ReadOnlyEditor = true;
        DropDownResizable = true;
        DropDownWidth = DefaultDropDownWidth;
        DropDownHeight = DefaultDropDownHeight;

        _columns = new KryptonMultiColumnComboBoxColumnCollection(this);
        _dropDown = new KryptonMultiColumnComboBoxDropDown(this);
        base.DropContent = _dropDown;

        ValueCommitted += OnDropDownValueCommitted;
        SyncDropDownBindingContext();
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            ValueCommitted -= OnDropDownValueCommitted;
        }

        base.Dispose(disposing);
    }

    /// <inheritdoc />
    protected override void OnBindingContextChanged(EventArgs e)
    {
        base.OnBindingContextChanged(e);
        SyncDropDownBindingContext();
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the data source for the drop-down grid.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates the list that the drop-down will use for its rows.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [AttributeProvider(typeof(IListSource))]
    public object? DataSource
    {
        get => _dropDown.DataSource;
        set
        {
            if (ReferenceEquals(_dropDown.DataSource, value))
            {
                return;
            }

            _dropDown.DataSource = value;
        }
    }

    /// <summary>
    /// Gets or sets the property path shown in the editor (and used for type-ahead filtering).
    /// </summary>
    [Category(@"Data")]
    [Description(@"Property shown in the editor and used when filtering as you type.")]
    // ToDo V120 LTS: Migrate designer editor to a Krypton-themed equivalent (replaces System.Windows.Forms.Design.DataMemberFieldEditor).
    [Editor(@"System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", typeof(UITypeEditor))]
    [DefaultValue("")]
    public string DisplayMember
    {
        get => _displayMember;
        set
        {
            string normalized = value ?? string.Empty;
            if (_displayMember == normalized)
            {
                return;
            }

            _displayMember = normalized;
            RefreshEditorTextFromSelection();
        }
    }

    /// <summary>
    /// Gets or sets the property path that supplies <see cref="SelectedValue"/>.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Property used as the logical selected value.")]
    // ToDo V120 LTS: Migrate designer editor to a Krypton-themed equivalent (replaces System.Windows.Forms.Design.DataMemberFieldEditor).
    [Editor(@"System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", typeof(UITypeEditor))]
    [DefaultValue("")]
    public string ValueMember
    {
        get => _valueMember;
        set => _valueMember = value ?? string.Empty;
    }

    /// <summary>
    /// Gets or sets an optional composite format string applied to the display value
    /// (for example <c>{0} ({1})</c> is not supported; use <c>{0}</c> with <see cref="DisplayMember"/>).
    /// </summary>
    [Category(@"Data")]
    [Description(@"Optional format string applied to the DisplayMember value in the editor.")]
    [DefaultValue("")]
    public string DisplayFormat
    {
        get => _displayFormat;
        set
        {
            string normalized = value ?? string.Empty;
            if (_displayFormat == normalized)
            {
                return;
            }

            _displayFormat = normalized;
            RefreshEditorTextFromSelection();
        }
    }

    /// <summary>
    /// Gets the collection of drop-down columns. When empty, columns are generated from
    /// <see cref="DataSource"/> when <see cref="AutoGenerateColumns"/> is <see langword="true"/>.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Columns shown in the drop-down grid.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    // ToDo V120 LTS: Migrate designer editor to KryptonDesignerCollectionForm (replaces System.ComponentModel.Design.CollectionEditor).
    [Editor(@"System.ComponentModel.Design.CollectionEditor, System.Design", typeof(UITypeEditor))]
    [MergableProperty(false)]
    public KryptonMultiColumnComboBoxColumnCollection Columns => _columns;

    private bool ShouldSerializeColumns() => _columns.Count > 0;

    /// <summary>
    /// Gets or sets a value indicating whether the drop-down generates columns from
    /// <see cref="DataSource"/> when <see cref="Columns"/> is empty.
    /// </summary>
    [Category(@"Data")]
    [Description(@"When true and Columns is empty, the drop-down generates columns from the data source.")]
    [DefaultValue(true)]
    public bool AutoGenerateColumns
    {
        get => _autoGenerateColumns;
        set
        {
            if (_autoGenerateColumns == value)
            {
                return;
            }

            _autoGenerateColumns = value;
            _dropDown.ApplyColumnLayout();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether column headers are shown in the drop-down.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Whether column headers are shown in the drop-down.")]
    [DefaultValue(true)]
    public bool ColumnHeadersVisible
    {
        get => _dropDown.ColumnHeadersVisible;
        set => _dropDown.ColumnHeadersVisible = value;
    }

    /// <summary>
    /// Gets or sets how drop-down columns are sized.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"How drop-down columns are sized. Width on Columns applies when this is None.")]
    [DefaultValue(DataGridViewAutoSizeColumnsMode.None)]
    public DataGridViewAutoSizeColumnsMode AutoSizeColumnsMode
    {
        get => _dropDown.AutoSizeColumnsMode;
        set => _dropDown.AutoSizeColumnsMode = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether a left-click on a data row commits the selection.
    /// Header and scrollbar clicks never commit.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"When true, a left-click on a data row commits the selection.")]
    [DefaultValue(true)]
    public bool CommitOnRowClick
    {
        get => _commitOnRowClick;
        set => _commitOnRowClick = value;
    }

    /// <summary>
    /// Gets the hosted <see cref="KryptonDataGridView"/>. Do not reparent this control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonDataGridView Grid => _dropDown;

    /// <summary>
    /// Gets or sets the zero-based index of the selected row, or <c>-1</c> when nothing is selected.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectedIndex
    {
        get => _selectedIndex;
        set
        {
            if (value < -1)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            if (value >= _dropDown.Rows.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            DataGridViewRow? row = value < 0 ? null : _dropDown.Rows[value];
            if (row is { IsNewRow: true })
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            ApplyRowSelection(row, fireEvent: true);
        }
    }

    /// <summary>
    /// Gets or sets the bound item for the selected row.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Bindable(true)]
    public object? SelectedItem
    {
        get
        {
            DataGridViewRow? row = GetSelectedRow();
            return row == null ? null : GetRowItem(row);
        }
        set
        {
            if (value == null)
            {
                ApplyRowSelection(null, fireEvent: true);
                return;
            }

            DataGridViewRow? match = FindRowByItem(value);
            ApplyRowSelection(match, fireEvent: true);
        }
    }

    /// <summary>
    /// Gets the currently selected drop-down row, or <see langword="null"/>.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataGridViewRow? SelectedRow => GetSelectedRow();

    /// <summary>
    /// Gets or sets the logical selected value (the <see cref="ValueMember"/> of the selected row,
    /// or the bound item when <see cref="ValueMember"/> is empty).
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Bindable(true)]
    public new object? SelectedValue
    {
        get
        {
            DataGridViewRow? row = GetSelectedRow();
            return row == null ? null : GetRowValue(row);
        }
        set
        {
            _pendingSelectedValue = value;
            _hasPendingSelectedValue = true;
            ApplyPendingSelection();
        }
    }

    /// <summary>
    /// Hidden. The grid drop-down is fixed for this control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Control? DropContent
    {
        get => base.DropContent;
        set => base.DropContent = value;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Formats the specified row for display in the editor.
    /// </summary>
    /// <param name="row">The row to format.</param>
    /// <returns>Editor text for the row.</returns>
    public string FormatRowDisplay(DataGridViewRow row)
    {
        if (row == null)
        {
            return string.Empty;
        }

        object? item = GetRowItem(row);
        object? display = string.IsNullOrEmpty(_displayMember)
            ? GetFirstVisibleCellValue(row)
            : GetMemberValue(item, _displayMember);

        if (!string.IsNullOrEmpty(_displayFormat))
        {
            try
            {
                return string.Format(CultureInfo.CurrentCulture, _displayFormat, display);
            }
            catch (FormatException)
            {
                // Fall through to the unformatted display text.
            }
        }

        return Convert.ToString(display, CultureInfo.CurrentCulture) ?? string.Empty;
    }

    #endregion

    #region Protected

    /// <summary>
    /// Raises the <see cref="SelectedIndexChanged"/> event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
    protected virtual void OnSelectedIndexChanged(EventArgs e) => SelectedIndexChanged?.Invoke(this, e);

    #endregion

    #region Internal

    /// <summary>
    /// Rebuilds drop-down columns after the <see cref="Columns"/> collection changes.
    /// </summary>
    internal void NotifyColumnsCollectionChanged()
    {
        if (_suspendColumnLayout)
        {
            return;
        }

        if (_columns.Count > 0 && _autoGenerateColumns)
        {
            _autoGenerateColumns = false;
        }

        _suspendColumnLayout = true;
        try
        {
            _dropDown.ApplyColumnLayout();
        }
        finally
        {
            _suspendColumnLayout = false;
        }
    }

    /// <summary>
    /// Rebuilds drop-down columns after an individual column property changes.
    /// </summary>
    internal void NotifyColumnChanged()
    {
        if (_suspendColumnLayout || _autoGenerateColumns)
        {
            return;
        }

        _dropDown.ApplyColumnLayout();
    }

    /// <summary>
    /// Commits <paramref name="row"/> into the editor and closes the drop-down.
    /// </summary>
    /// <param name="row">The row to commit.</param>
    internal void CommitRow(DataGridViewRow row)
    {
        _hasPendingSelectedValue = false;
        ApplyRowSelection(row, fireEvent: true);
        _dropDown.PublishCommit(GetRowValue(row), FormatRowDisplay(row));
    }

    /// <summary>
    /// Selects the current <see cref="SelectedIndex"/> / <see cref="SelectedValue"/> row in the grid.
    /// </summary>
    internal void SynchronizeDropDownSelection()
    {
        ApplyPendingSelection();
        _dropDown.SelectRow(GetSelectedRow());
    }

    /// <summary>
    /// Called when the drop-down finishes binding so a pending <see cref="SelectedValue"/> can apply.
    /// </summary>
    internal void OnDropDownDataBindingComplete() => ApplyPendingSelection();

    /// <summary>
    /// Returns the logical value for <paramref name="row"/>.
    /// </summary>
    /// <param name="row">A data row in the drop-down.</param>
    /// <returns>The <see cref="ValueMember"/> value, or the bound item.</returns>
    internal object? GetRowValue(DataGridViewRow row) => GetMemberValue(GetRowItem(row), _valueMember);

    #endregion

    #region Implementation

    private void OnDropDownValueCommitted(object? sender, KryptonDropDownCommitEventArgs e)
    {
        DataGridViewRow? row = FindRowByValue(e.Value) ?? _dropDown.CurrentRow;
        int newIndex = row is { IsNewRow: false } ? row.Index : -1;
        if (newIndex == _selectedIndex)
        {
            return;
        }

        _selectedIndex = newIndex;
        OnSelectedIndexChanged(EventArgs.Empty);
    }

    private void SyncDropDownBindingContext()
    {
        if (_dropDown is null)
        {
            return;
        }

        _dropDown.BindingContext = BindingContext;
    }

    private void ApplyPendingSelection()
    {
        if (!_hasPendingSelectedValue)
        {
            return;
        }

        if (_pendingSelectedValue == null)
        {
            _hasPendingSelectedValue = false;
            ApplyRowSelection(null, fireEvent: true);
            return;
        }

        if (!HasDataRows())
        {
            return;
        }

        DataGridViewRow? match = FindRowByValue(_pendingSelectedValue);
        _hasPendingSelectedValue = false;
        ApplyRowSelection(match, fireEvent: true);
    }

    private void ApplyRowSelection(DataGridViewRow? row, bool fireEvent)
    {
        int newIndex = row is { IsNewRow: false } ? row.Index : -1;
        bool changed = newIndex != _selectedIndex;
        _selectedIndex = newIndex;
        _dropDown.SelectRow(row);
        RefreshEditorTextFromSelection();

        if (fireEvent && changed)
        {
            OnSelectedIndexChanged(EventArgs.Empty);
        }
    }

    private void RefreshEditorTextFromSelection()
    {
        DataGridViewRow? row = GetSelectedRow();
        bool previousAutoOpen = AutoOpenOnType;
        AutoOpenOnType = false;
        try
        {
            Text = row == null ? string.Empty : FormatRowDisplay(row);
        }
        finally
        {
            AutoOpenOnType = previousAutoOpen;
        }
    }

    private DataGridViewRow? GetSelectedRow()
    {
        if (_selectedIndex >= 0 && _selectedIndex < _dropDown.Rows.Count)
        {
            DataGridViewRow row = _dropDown.Rows[_selectedIndex];
            return row.IsNewRow ? null : row;
        }

        return _dropDown.CurrentRow is { IsNewRow: false } current ? current : null;
    }

    private DataGridViewRow? FindRowByValue(object? value)
    {
        foreach (DataGridViewRow row in _dropDown.Rows)
        {
            if (!row.IsNewRow && ValuesEqual(GetRowValue(row), value))
            {
                return row;
            }
        }

        return null;
    }

    private DataGridViewRow? FindRowByItem(object item)
    {
        foreach (DataGridViewRow row in _dropDown.Rows)
        {
            if (row.IsNewRow)
            {
                continue;
            }

            object? rowItem = GetRowItem(row);
            if (ReferenceEquals(rowItem, item) || Equals(rowItem, item))
            {
                return row;
            }
        }

        return null;
    }

    private bool HasDataRows()
    {
        foreach (DataGridViewRow row in _dropDown.Rows)
        {
            if (!row.IsNewRow)
            {
                return true;
            }
        }

        return false;
    }

    private static object? GetRowItem(DataGridViewRow row) => row.DataBoundItem ?? (object)row;

    private static object? GetFirstVisibleCellValue(DataGridViewRow row)
    {
        foreach (DataGridViewCell cell in row.Cells)
        {
            if (cell.OwningColumn is { Visible: true })
            {
                return cell.FormattedValue ?? cell.Value;
            }
        }

        return row.Cells.Count > 0 ? row.Cells[0].FormattedValue ?? row.Cells[0].Value : null;
    }

    private static object? GetMemberValue(object? item, string? member)
    {
        if (item == null)
        {
            return null;
        }

        if (string.IsNullOrEmpty(member))
        {
            return item;
        }

        PropertyDescriptor? descriptor = TypeDescriptor.GetProperties(item).Find(member, ignoreCase: true);
        return descriptor == null ? item : descriptor.GetValue(item);
    }

    private static bool ValuesEqual(object? left, object? right)
    {
        if (Equals(left, right))
        {
            return true;
        }

        if (left == null || right == null)
        {
            return false;
        }

        try
        {
            object converted = Convert.ChangeType(right, left.GetType(), CultureInfo.CurrentCulture);
            return Equals(left, converted);
        }
        catch (Exception ex) when (ex is InvalidCastException or FormatException or OverflowException)
        {
            return false;
        }
    }

    #endregion
}
