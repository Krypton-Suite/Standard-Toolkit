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
/// Drop-down <see cref="KryptonDataGridView"/> for <see cref="KryptonMultiColumnComboBox"/>.
/// </summary>
/// <remarks>
/// Hosted directly as <see cref="KryptonComboBoxUserControl.DropContent"/> (not inside a plain
/// <see cref="UserControl"/>) so Krypton layout and painting run correctly inside the popup.
/// </remarks>
[ToolboxItem(false)]
internal sealed class KryptonMultiColumnComboBoxDropDown : KryptonDataGridView,
    IKryptonDropDownUserControl,
    IKryptonDropDownFilterable
{
    #region Instance Fields

    private readonly KryptonMultiColumnComboBox _owner;
    private string _filterText = string.Empty;
    private bool _applyingFilter;

    #endregion

    #region Events

    public event EventHandler<KryptonDropDownCommitEventArgs>? CommitValue;

    public event EventHandler? RequestClose;

    #endregion

    #region Identity

    public KryptonMultiColumnComboBoxDropDown(KryptonMultiColumnComboBox owner)
    {
        _owner = owner;

        AllowUserToAddRows = false;
        AllowUserToDeleteRows = false;
        AllowUserToOrderColumns = false;
        AllowUserToResizeRows = false;
        ReadOnly = true;
        RowHeadersVisible = false;
        MultiSelect = false;
        SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        EditMode = DataGridViewEditMode.EditProgrammatically;
        BorderStyle = BorderStyle.None;
        AutoGenerateColumns = true;
        PaletteMode = owner.PaletteMode;

        CellMouseUp += OnGridCellMouseUp;
        DataError += OnGridDataError;
        DataBindingComplete += OnGridDataBindingComplete;
    }

    #endregion

    #region Public

    /// <summary>
    /// Raises <see cref="CommitValue"/> so the host editor updates and the popup can close.
    /// </summary>
    /// <param name="value">Logical value (ValueMember or bound item).</param>
    /// <param name="displayText">Text written into the editor.</param>
    internal void PublishCommit(object? value, string? displayText) =>
        CommitValue?.Invoke(this, new KryptonDropDownCommitEventArgs(value, displayText));

    /// <summary>
    /// Aligns palette and layout after the popup is shown.
    /// </summary>
    internal void EnsureDropDownLayout()
    {
        PaletteMode = _owner.PaletteMode;

        if (IsHandleCreated)
        {
            PerformLayout();
            Invalidate();
        }
    }

    /// <summary>
    /// Selects <paramref name="row"/> without committing, scrolling it into view when possible.
    /// </summary>
    /// <param name="row">Row to select; ignored when <see langword="null"/> or a new-row placeholder.</param>
    internal void SelectRow(DataGridViewRow? row)
    {
        if (row == null || row.IsNewRow || row.Index < 0 || row.Index >= Rows.Count)
        {
            ClearSelection();
            CurrentCell = null;
            return;
        }

        if (!row.Visible)
        {
            row.Visible = true;
        }

        DataGridViewCell? cell = GetFirstVisibleCell(row);
        if (cell != null)
        {
            CurrentCell = cell;
        }

        row.Selected = true;

        try
        {
            FirstDisplayedScrollingRowIndex = row.Index;
        }
        catch (InvalidOperationException)
        {
            // The row may be filtered out or the grid may not have a display rectangle yet.
        }
    }

    /// <summary>
    /// Rebuilds <see cref="DataGridView.Columns"/> from the owner's column collection when
    /// auto-generation is off.
    /// </summary>
    internal void ApplyColumnLayout()
    {
        bool autoGenerate = _owner.AutoGenerateColumns || _owner.Columns.Count == 0;
        AutoGenerateColumns = autoGenerate;

        if (autoGenerate)
        {
            return;
        }

        object? dataSource = DataSource;
        DataSource = null;
        Columns.Clear();

        for (int i = 0; i < _owner.Columns.Count; i++)
        {
            KryptonMultiColumnComboBoxColumn definition = _owner.Columns[i];
            string name = definition.Name;
            if (string.IsNullOrEmpty(name))
            {
                name = string.IsNullOrEmpty(definition.DataPropertyName)
                    ? $"Column{i}"
                    : definition.DataPropertyName;
            }

            string headerText = string.IsNullOrEmpty(definition.HeaderText)
                ? definition.DataPropertyName
                : definition.HeaderText;

            var column = new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = headerText,
                DataPropertyName = definition.DataPropertyName,
                Width = definition.Width,
                Visible = definition.Visible,
                ReadOnly = true
            };

            if (definition.Alignment != DataGridViewContentAlignment.NotSet)
            {
                column.DefaultCellStyle.Alignment = definition.Alignment;
            }

            if (!string.IsNullOrEmpty(definition.Format))
            {
                column.DefaultCellStyle.Format = definition.Format;
            }

            Columns.Add(column);
        }

        DataSource = dataSource;
    }

    #endregion

    #region IKryptonDropDownUserControl

    public Size GetPreferredDropSize(Size proposedSize) => proposedSize.IsEmpty ? Size.Empty : proposedSize;

    public void OnDropDownOpening(object owner)
    {
        EnsureDropDownLayout();

        if (!_owner.AutoOpenOnType)
        {
            ApplyFilter(string.Empty);
        }

        _owner.SynchronizeDropDownSelection();
    }

    public void OnDropDownOpened(object owner)
    {
        EnsureDropDownLayout();

        if (!_owner.AutoOpenOnType)
        {
            Focus();
        }

        _owner.SynchronizeDropDownSelection();
    }

    public void OnDropDownClosing(object owner, ref bool cancel)
    {
    }

    public void OnDropDownClosed(object owner)
    {
        if (!string.IsNullOrEmpty(_filterText))
        {
            ApplyFilter(string.Empty);
        }
    }

    #endregion

    #region IKryptonDropDownFilterable

    public bool ApplyFilter(string text)
    {
        _filterText = text ?? string.Empty;
        _applyingFilter = true;

        CurrencyManager? currencyManager = GetCurrencyManager();
        currencyManager?.SuspendBinding();

        try
        {
            DataGridViewRow? current = CurrentRow;
            if (current != null && current.Index >= 0)
            {
                CurrentCell = null;
            }

            bool anyVisible = false;
            DataGridViewRow? firstVisible = null;

            foreach (DataGridViewRow row in Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                bool visible = RowMatchesFilter(row);
                if (row.Visible != visible)
                {
                    row.Visible = visible;
                }

                if (visible)
                {
                    anyVisible = true;
                    firstVisible ??= row;
                }
            }

            if (anyVisible)
            {
                DataGridViewRow? restore = current != null && current.Visible ? current : firstVisible;
                SelectRow(restore);
            }
            else
            {
                ClearSelection();
            }

            return anyVisible || !HasDataRows();
        }
        finally
        {
            currencyManager?.ResumeBinding();
            _applyingFilter = false;
        }
    }

    public void NavigateSelection(int direction)
    {
        if (direction == 0 || Rows.Count == 0)
        {
            return;
        }

        var visible = new List<DataGridViewRow>();
        foreach (DataGridViewRow row in Rows)
        {
            if (!row.IsNewRow && row.Visible)
            {
                visible.Add(row);
            }
        }

        if (visible.Count == 0)
        {
            return;
        }

        int currentIndex = -1;
        if (CurrentRow != null)
        {
            currentIndex = visible.IndexOf(CurrentRow);
        }

        int next = currentIndex < 0
            ? direction > 0 ? 0 : visible.Count - 1
            : Math.Max(0, Math.Min(visible.Count - 1, currentIndex + direction));

        SelectRow(visible[next]);
    }

    public bool CommitSelection()
    {
        DataGridViewRow? row = CurrentRow;
        if (row == null || row.IsNewRow || !row.Visible)
        {
            return false;
        }

        _owner.CommitRow(row);
        return true;
    }

    #endregion

    #region Protected Override

    /// <inheritdoc />
    protected override bool ProcessDataGridViewKey(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            CommitSelection();
            return true;
        }

        if (e.KeyCode == Keys.Escape)
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
            return true;
        }

        return base.ProcessDataGridViewKey(e);
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            CellMouseUp -= OnGridCellMouseUp;
            DataError -= OnGridDataError;
            DataBindingComplete -= OnGridDataBindingComplete;
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Implementation

    private void OnGridCellMouseUp(object? sender, DataGridViewCellMouseEventArgs e)
    {
        if (_applyingFilter || !_owner.CommitOnRowClick)
        {
            return;
        }

        if (e.Button != MouseButtons.Left || e.RowIndex < 0 || e.RowIndex >= Rows.Count)
        {
            return;
        }

        DataGridViewRow row = Rows[e.RowIndex];
        if (row.IsNewRow || !row.Visible)
        {
            return;
        }

        _owner.CommitRow(row);
    }

    private void OnGridDataError(object? sender, DataGridViewDataErrorEventArgs e) => e.ThrowException = false;

    private void OnGridDataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e) =>
        _owner.OnDropDownDataBindingComplete();

    private bool RowMatchesFilter(DataGridViewRow row)
    {
        if (string.IsNullOrEmpty(_filterText))
        {
            return true;
        }

        string display = _owner.FormatRowDisplay(row) ?? string.Empty;
        return display.IndexOf(_filterText, StringComparison.CurrentCultureIgnoreCase) >= 0;
    }

    private bool HasDataRows()
    {
        foreach (DataGridViewRow row in Rows)
        {
            if (!row.IsNewRow)
            {
                return true;
            }
        }

        return false;
    }

    private CurrencyManager? GetCurrencyManager()
    {
        if (DataSource == null || BindingContext == null)
        {
            return null;
        }

        string dataMember = DataMember ?? string.Empty;

        try
        {
            return BindingContext[DataSource, dataMember] as CurrencyManager;
        }
        catch (Exception ex) when (ex is ArgumentException or IndexOutOfRangeException)
        {
            return null;
        }
    }

    private static DataGridViewCell? GetFirstVisibleCell(DataGridViewRow row)
    {
        foreach (DataGridViewCell cell in row.Cells)
        {
            if (cell.OwningColumn is { Visible: true })
            {
                return cell;
            }
        }

        return row.Cells.Count > 0 ? row.Cells[0] : null;
    }

    #endregion
}
