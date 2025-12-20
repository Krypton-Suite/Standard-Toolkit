#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.Data;

namespace Krypton.Utilities;

internal class SuggestionPopup : KryptonForm
{
    private KryptonListBox? _listBox;
    private KryptonDataGridView? _dataGridView;
    private readonly List<object> _suggestions;
    private DataTable? _dataTable;
    private readonly KryptonSearchBox _owner;

    private readonly SearchSuggestionDisplayType? _suggestionDisplayType;

    public event EventHandler<SuggestionSelectedEventArgs>? SuggestionSelected;

    public int SuggestionCount => _suggestions.Count;

    public SuggestionPopup(KryptonSearchBox owner)
    {
        _suggestions = new List<object>();

        _owner = owner;

        _suggestionDisplayType = owner.SearchBoxValues.SuggestionDisplayType;

        FormBorderStyle = FormBorderStyle.None;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.Manual;
        TopMost = true;
        BackColor = Color.White;
        Padding = new Padding(1);

        // Prevent focus stealing
        SetStyle(ControlStyles.Selectable, false);

        // Prevent activation
        SetStyle(ControlStyles.UserPaint, true);

        // Create the appropriate control based on display type
        if (_suggestionDisplayType == SearchSuggestionDisplayType.DataGridView)
        {
            // Create DataTable for suggestions with columns from definitions
            _dataTable = new DataTable();

            // Add columns based on owner's column definitions
            foreach (var colDef in _owner.DataGridViewColumns)
            {
                _dataTable.Columns.Add(colDef.DataPropertyName, typeof(object));
            }

            _dataGridView = new KryptonDataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ColumnHeadersVisible = _owner.DataGridViewColumns.Count > 1, // Show headers if multiple columns
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ShowCellToolTips = false,
                ScrollBars = ScrollBars.None,
                TabStop = false
            };

            // Add columns based on definitions
            foreach (var colDef in _owner.DataGridViewColumns)
            {
                var column = new DataGridViewTextBoxColumn
                {
                    Name = colDef.Name,
                    DataPropertyName = colDef.DataPropertyName,
                    HeaderText = colDef.HeaderText,
                    AutoSizeMode = colDef.AutoSizeMode,
                    ReadOnly = true
                };

                if (colDef.Width > 0)
                {
                    column.Width = colDef.Width;
                }

                _dataGridView.Columns.Add(column);
            }

            _dataGridView.DataSource = _dataTable;

            // Hide outer borders - KryptonDataGridView already sets BorderStyle.None in constructor
            // We can use HideOuterBorders to hide cell borders if needed
            _dataGridView.HideOuterBorders = true;

            // Handle mouse events
            _dataGridView.CellMouseDown += OnDataGridViewCellMouseDown;
            _dataGridView.CellMouseClick += OnDataGridViewCellMouseClick;
            _dataGridView.CellDoubleClick += OnDataGridViewCellDoubleClick;
            _dataGridView.KeyDown += OnDataGridViewKeyDown;

            Controls.Add(_dataGridView);
        }
        else
        {
            // Use ListBox
            _listBox = new KryptonListBox
            {
                Dock = DockStyle.Fill
            };
            // Set border to not draw - use DrawBorders property instead
            _listBox.StateCommon.Border.DrawBorders = PaletteDrawBorders.None;
            _listBox.MouseDown += OnSuggestionListBoxMouseDown;
            _listBox.MouseClick += OnSuggestionListBoxMouseClick;
            _listBox.DoubleClick += OnSuggestionListBoxDoubleClick;

            Controls.Add(_listBox);
        }

        // Inherit palette from owner
        if (owner.PaletteMode != PaletteMode.Custom)
        {
            PaletteMode = owner.PaletteMode;
        }
        else if (owner.LocalCustomPalette != null)
        {
            LocalCustomPalette = owner.LocalCustomPalette;
        }
    }

    public void SetSearchSuggestions(List<object> suggestions)
    {
        _suggestions.Clear();
        _suggestions.AddRange(suggestions);

        if (_suggestionDisplayType == SearchSuggestionDisplayType.DataGridView)
        {
            if (_dataTable != null && _dataGridView != null)
            {
                _dataTable.Rows.Clear();
                foreach (var suggestion in suggestions)
                {
                    // Create row with values for each column
                    var rowValues = new object[_owner.DataGridViewColumns.Count];
                    for (int i = 0; i < _owner.DataGridViewColumns.Count; i++)
                    {
                        var colDef = _owner.DataGridViewColumns[i];
                        if (colDef.ValueExtractor != null)
                        {
                            rowValues[i] = colDef.ValueExtractor(suggestion) ?? string.Empty;
                        }
                        else
                        {
                            // Default: extract text
                            rowValues[i] = GetSuggestionText(suggestion);
                        }
                    }
                    _dataTable.Rows.Add(rowValues);
                }
            }
        }
        else
        {
            if (_listBox != null)
            {
                _listBox.BeginUpdate();
                _listBox.Items.Clear();
                // Add items - can be strings or IContentValues
                foreach (var suggestion in suggestions)
                {
                    _listBox.Items.Add(suggestion);
                }
                _listBox.EndUpdate();
            }
        }
    }

    public void HighlightIndex(int index)
    {
        if (_suggestionDisplayType == SearchSuggestionDisplayType.DataGridView)
        {
            if (_dataGridView != null && index >= 0 && index < _dataGridView.Rows.Count)
            {
                _dataGridView.ClearSelection();
                _dataGridView.Rows[index].Selected = true;
                _dataGridView.CurrentCell = _dataGridView.Rows[index].Cells[0];

                // Scroll to make the selected row visible
                _dataGridView.FirstDisplayedScrollingRowIndex = Math.Max(0, Math.Min(index, _dataGridView.RowCount - 1));
            }
        }
        else
        {
            if (_listBox != null && index >= 0 && index < _listBox.Items.Count)
            {
                _listBox.SelectedIndex = index;
                _listBox.TopIndex = Math.Max(0, index - 2);
            }
        }
    }

    private string GetSuggestionText(object suggestion)
    {
        if (suggestion is IContentValues contentValues)
        {
            return contentValues.GetShortText() ?? string.Empty;
        }
        return suggestion.ToString() ?? string.Empty;
    }

    public object? GetSuggestion(int index)
    {
        if (index >= 0 && index < _suggestions.Count)
        {
            return _suggestions[index];
        }
        return null;
    }

    public string? GetSuggestionText(int index)
    {
        var suggestion = GetSuggestion(index);
        if (suggestion == null)
        {
            return null;
        }

        if (suggestion is IContentValues contentValues)
        {
            return contentValues.GetShortText();
        }
        return suggestion.ToString();
    }

    public void Show(Point location, int width)
    {
        int height;

        if (_suggestionDisplayType == SearchSuggestionDisplayType.DataGridView)
        {
            if (_dataGridView == null)
            {
                return;
            }

            // Calculate height based on row count (max 8 rows visible)
            var rowHeight = _dataGridView.Rows.Count > 0 ? _dataGridView.Rows[0].Height : 22;
            var rowCount = Math.Min(_suggestions.Count, 8);
            height = (rowCount * rowHeight) + 2;
        }
        else
        {
            if (_listBox == null)
            {
                return;
            }

            // Calculate height based on item count (max 8 items visible)
            var itemHeight = _suggestions.Count > 0 ? _listBox.GetItemHeight(0) : 20;
            var itemCount = Math.Min(_suggestions.Count, 8);
            height = (itemCount * itemHeight) + 2;
        }

        Size = new Size(width, height);
        Location = location;

        // Create handle if needed
        if (!IsHandleCreated)
        {
            CreateHandle();
        }

        // Show the form without activating it (doesn't steal focus)
        PI.ShowWindow(Handle, PI.ShowWindowCommands.SW_SHOWNOACTIVATE);
    }

    private void OnSuggestionListBoxMouseDown(object? sender, MouseEventArgs e)
    {
        if (_listBox != null && e.Button == MouseButtons.Left)
        {
            var index = _listBox.IndexFromPoint(e.Location);
            if (index >= 0)
            {
                // Single click selects the suggestion immediately
                OnSuggestionSelected(index);
            }
        }
    }

    private void OnSuggestionListBoxMouseClick(object? sender, MouseEventArgs e)
    {
        if (_listBox != null && e.Button == MouseButtons.Left)
        {
            var index = _listBox.IndexFromPoint(e.Location);
            if (index >= 0)
            {
                OnSuggestionSelected(index);
            }
        }
    }

    private void OnSuggestionListBoxDoubleClick(object? sender, EventArgs e)
    {
        if (_listBox != null && _listBox.SelectedIndex >= 0)
        {
            OnSuggestionSelected(_listBox.SelectedIndex);
        }
    }

    private void OnSuggestionSelected(int index)
    {
        var suggestionObject = GetSuggestion(index);
        var suggestionText = GetSuggestionText(index);
        var args = new SuggestionSelectedEventArgs(index)
        {
            Suggestion = suggestionText,
            SuggestionObject = suggestionObject
        };
        SuggestionSelected?.Invoke(this, args);
        Hide();
    }

    public bool HasFocus()
    {
        if (ContainsFocus)
        {
            return true;
        }

        // Check if any child control has focus
        return _suggestionDisplayType == SearchSuggestionDisplayType.DataGridView
            ? _dataGridView != null && _dataGridView.Focused
            : _listBox != null && _listBox.Focused;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_listBox != null)
            {
                _listBox.MouseDown -= OnSuggestionListBoxMouseDown;
                _listBox.MouseClick -= OnSuggestionListBoxMouseClick;
                _listBox.DoubleClick -= OnSuggestionListBoxDoubleClick;
                _listBox.Dispose();
                _listBox = null;
            }

            if (_dataGridView != null)
            {
                _dataGridView.CellMouseDown -= OnDataGridViewCellMouseDown;
                _dataGridView.CellMouseClick -= OnDataGridViewCellMouseClick;
                _dataGridView.CellDoubleClick -= OnDataGridViewCellDoubleClick;
                _dataGridView.KeyDown -= OnDataGridViewKeyDown;
                _dataGridView.Dispose();
                _dataGridView = null;
            }

            if (_dataTable != null)
            {
                _dataTable.Dispose();
                _dataTable = null;
            }
        }

        base.Dispose(disposing);
    }

    protected override void SetVisibleCore(bool value)
    {
        if (!IsDisposed && value)
        {
            // Prevent the form from activating when it becomes visible
            if (IsHandleCreated && Handle != IntPtr.Zero)
            {
                PI.ShowWindow(Handle, PI.ShowWindowCommands.SW_SHOWNOACTIVATE);
            }
            return;
        }

        base.SetVisibleCore(value);
    }

    protected override void WndProc(ref Message m)
    {
        // Prevent activation messages from stealing focus
        if (m.Msg == PI.WM_.ACTIVATE || m.Msg == PI.WM_.MOUSEACTIVATE)
        {
            m.Result = IntPtr.Zero;
            return;
        }

        base.WndProc(ref m);
    }

    private void OnDataGridViewCellMouseDown(object? sender, DataGridViewCellMouseEventArgs e)
    {
        if (_dataGridView != null && e.Button == MouseButtons.Left && e.RowIndex >= 0)
        {
            // Single click selects the suggestion immediately
            OnSuggestionSelected(e.RowIndex);
        }
    }

    private void OnDataGridViewCellMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
    {
        // Also handle MouseClick as a fallback
        if (_dataGridView != null && e.Button == MouseButtons.Left && e.RowIndex >= 0)
        {
            OnSuggestionSelected(e.RowIndex);
        }
    }

    private void OnDataGridViewCellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        // Also handle double click for consistency
        if (_dataGridView != null && e.RowIndex >= 0)
        {
            OnSuggestionSelected(e.RowIndex);
        }
    }

    private void OnDataGridViewKeyDown(object? sender, KeyEventArgs e)
    {
        // Handle Enter key to select the current row
        if (e.KeyCode == Keys.Enter && _dataGridView != null && _dataGridView.CurrentRow != null)
        {
            OnSuggestionSelected(_dataGridView.CurrentRow.Index);
            e.Handled = true;
        }
    }
}