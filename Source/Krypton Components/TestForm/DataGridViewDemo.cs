#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class DataGridViewDemo : KryptonForm
{
    private bool _prevEnableHeadersVisualStylesChecked;
    private bool _suppressOptionSync;
    public DataGridViewDemo()
    {
        InitializeComponent();
    }

    private void DataGridViewDemo_Load(object sender, EventArgs e)
    {
        // Designer now sets all control state, columns, and checkbox defaults.
        _prevEnableHeadersVisualStylesChecked = kchkEnableHeadersVisualStyles.Checked;

        kdgvMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

        // Fill grid with test data **dynamically** so the order doesn't matter.
        // This allows to add/edit columns in designer without changing below code!
        var textBoxCols = new List<int>();
        var datePickerCols = new List<int>();
        var comboBoxCols = new List<int>();
        var numericUpDownCols = new List<int>();
        var domainUpDownCols = new List<int>();
        var checkBoxCols = new List<int>();
        var maskedTextBoxCols = new List<int>();
        var progressCols = new List<int>();
        var ratingCols = new List<int>();

        for (int ci = 0; ci < kdgvMain.Columns.Count; ci++)
        {
            var c = kdgvMain.Columns[ci];
            if (c is KryptonDataGridViewTextBoxColumn) { textBoxCols.Add(ci); continue; }
            if (c is KryptonDataGridViewDateTimePickerColumn) { datePickerCols.Add(ci); continue; }
            if (c is KryptonDataGridViewComboBoxColumn) { comboBoxCols.Add(ci); continue; }
            if (c is KryptonDataGridViewNumericUpDownColumn) { numericUpDownCols.Add(ci); continue; }
            if (c is KryptonDataGridViewDomainUpDownColumn) { domainUpDownCols.Add(ci); continue; }
            if (c is KryptonDataGridViewCheckBoxColumn) { checkBoxCols.Add(ci); continue; }
            if (c is KryptonDataGridViewMaskedTextBoxColumn) { maskedTextBoxCols.Add(ci); continue; }
            if (c is KryptonDataGridViewProgressColumn) { progressCols.Add(ci); continue; }
            if (c is KryptonDataGridViewRatingColumn) { ratingCols.Add(ci); continue; }
        }

        for (int i = 1; i <= 100; i++)
        {
            var values = new object[kdgvMain.Columns.Count];

            if (textBoxCols.Count > 0) values[textBoxCols[0]] = i;                       // Id
            if (textBoxCols.Count > 1) values[textBoxCols[1]] = $"Item {i}";             // Name
            if (numericUpDownCols.Count > 0) values[numericUpDownCols[0]] = i * 2;       // Quantity
            if (datePickerCols.Count > 0) values[datePickerCols[0]] = DateTime.Today.AddDays(i); // Date
            if (checkBoxCols.Count > 0) values[checkBoxCols[0]] = i % 2 == 0;            // Active
            if (comboBoxCols.Count > 0) values[comboBoxCols[0]] = (i % 3 == 0) ? "C" : (i % 2 == 0 ? "B" : "A"); // Combo
            if (maskedTextBoxCols.Count > 0) values[maskedTextBoxCols[0]] = string.Format(CultureInfo.InvariantCulture, "{0:00}-{1:00}", i % 100, (i * 2) % 100); // Masked
            if (domainUpDownCols.Count > 0) values[domainUpDownCols[0]] = (i % 3 == 0) ? "High" : (i % 3 == 1 ? "Low" : "Medium"); // Domain
            if (progressCols.Count > 0) values[progressCols[0]] = Math.Min(1m, (decimal)i / 50m); // Progress
            if (ratingCols.Count > 0) values[ratingCols[0]] = (byte)(i % 11);            // Rating

            kdgvMain.Rows.Add(values);
        }

        kdgvMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        kcmbAutoSizeColumnsMode.SelectedItem = "Fill";

        // Seed column-specific settings/content for new demo columns
        if (colCombo.Items.Count == 0)
        {
            colCombo.Items.AddRange(new object[] { "A", "B", "C" });
        }
        if (string.IsNullOrEmpty(colMasked.Mask))
        {
            colMasked.Mask = "00-00";
        }
        if (colDomain.Items.Count == 0)
        {
            colDomain.Items.AddRange(new string[] { "Low", "Medium", "High" });
        }
        if (colRating.RatingMaximum == 0)
        {
            colRating.RatingMaximum = 10;
        }

        // Guard future column additions against incompatible SelectionMode
        kdgvMain.ColumnAdded += kdgvMain_ColumnAdded;

        // Align numeric minimums to the grid's effective limits
        // Enforce documented grid minimum for row headers width (4)
        knudRowHeadersWidth.Value = kdgvMain.RowHeadersWidth;
        if (knudRowHeadersWidth.Minimum < 4)
        {
            knudRowHeadersWidth.Minimum = 4;
            if (knudRowHeadersWidth.Value < knudRowHeadersWidth.Minimum)
            {
                knudRowHeadersWidth.Value = knudRowHeadersWidth.Minimum;
            }
        }

        knudColumnHeadersHeight.Value = kdgvMain.ColumnHeadersHeight;
        if (knudColumnHeadersHeight.Minimum < 0)
        {
            knudColumnHeadersHeight.Minimum = 0;
            if (knudColumnHeadersHeight.Value < knudColumnHeadersHeight.Minimum)
            {
                knudColumnHeadersHeight.Value = knudColumnHeadersHeight.Minimum;
            }
        }
    }

    private void kchkAllowUserToAddRows_CheckedChanged(object sender, EventArgs e)
    {
        kdgvMain.AllowUserToAddRows = kchkAllowUserToAddRows.Checked;
    }

    private void kchkAllowUserToDeleteRows_CheckedChanged(object sender, EventArgs e)
    {
        kdgvMain.AllowUserToDeleteRows = kchkAllowUserToDeleteRows.Checked;
    }

    private void kchkAllowUserToResizeColumns_CheckedChanged(object sender, EventArgs e)
    {
        kdgvMain.AllowUserToResizeColumns = kchkAllowUserToResizeColumns.Checked;
    }

    private void kchkAllowUserToResizeRows_CheckedChanged(object sender, EventArgs e)
    {
        kdgvMain.AllowUserToResizeRows = kchkAllowUserToResizeRows.Checked;
    }

    private void kchkMultiSelect_CheckedChanged(object sender, EventArgs e)
    {
        kdgvMain.MultiSelect = kchkMultiSelect.Checked;
    }

    private void kchkReadOnly_CheckedChanged(object sender, EventArgs e)
    {
        kdgvMain.ReadOnly = kchkReadOnly.Checked;
    }

    private void kchkRowHeadersVisible_CheckedChanged(object sender, EventArgs e)
    {
        kdgvMain.RowHeadersVisible = kchkRowHeadersVisible.Checked;

        // If user hides row headers while RowHeaderSelect is active, switch to FullRowSelect
        if (!kchkRowHeadersVisible.Checked && kdgvMain.SelectionMode == DataGridViewSelectionMode.RowHeaderSelect)
        {
            _suppressOptionSync = true;
            kdgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            kcmbSelectionMode.SelectedItem = nameof(DataGridViewSelectionMode.FullRowSelect);
            _suppressOptionSync = false;
        }
    }

    private void kchkColumnHeadersVisible_CheckedChanged(object sender, EventArgs e)
    {
        kdgvMain.ColumnHeadersVisible = kchkColumnHeadersVisible.Checked;

        // If user hides column headers while ColumnHeaderSelect is active, switch to FullColumnSelect
        if (!kchkColumnHeadersVisible.Checked && kdgvMain.SelectionMode == DataGridViewSelectionMode.ColumnHeaderSelect)
        {
            _suppressOptionSync = true;
            kdgvMain.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
            kcmbSelectionMode.SelectedItem = nameof(DataGridViewSelectionMode.FullColumnSelect);
            _suppressOptionSync = false;
        }
    }

    private void kchkEnableHeadersVisualStyles_CheckedChanged(object sender, EventArgs e)
    {
        kdgvMain.EnableHeadersVisualStyles = kchkEnableHeadersVisualStyles.Checked;
    }

    private void kchkShowGridLines_CheckedChanged(object sender, EventArgs e)
    {
        kdgvMain.StateCommon.DataCell.Border.DrawBorders =
            kchkShowGridLines.Checked
                ? PaletteDrawBorders.All
                : PaletteDrawBorders.None;
        kdgvMain.StateCommon.HeaderRow.Border.DrawBorders = kdgvMain.StateCommon.DataCell.Border.DrawBorders;
        kdgvMain.StateCommon.HeaderColumn.Border.DrawBorders = kdgvMain.StateCommon.DataCell.Border.DrawBorders;

        kdgvMain.HideOuterBorders = !kchkShowGridLines.Checked;
    }

    private void kcmbAutoSizeColumnsMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (kcmbAutoSizeColumnsMode.SelectedItem is string name)
        {
            kdgvMain.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)Enum.Parse(typeof(DataGridViewAutoSizeColumnsMode), name);
        }
    }

    private void kcmbSelectionMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_suppressOptionSync)
        {
            return;
        }

        if (kcmbSelectionMode.SelectedItem is string name)
        {
            var newMode = (DataGridViewSelectionMode)Enum.Parse(typeof(DataGridViewSelectionMode), name);
            EnsureSelectionModeCompatibility(newMode);
            kdgvMain.SelectionMode = newMode;
        }
    }

    private void kcmbEditMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (kcmbEditMode.SelectedItem is string name)
        {
            kdgvMain.EditMode = (DataGridViewEditMode)Enum.Parse(typeof(DataGridViewEditMode), name);
        }
    }

    private void kcmbScrollBars_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (kcmbScrollBars.SelectedItem is string name)
        {
            kdgvMain.ScrollBars = (ScrollBars)Enum.Parse(typeof(ScrollBars), name);
        }
    }

    private void kcmbAutoSizeRowsMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (kcmbAutoSizeRowsMode.SelectedItem is string name)
        {
            kdgvMain.AutoSizeRowsMode = (DataGridViewAutoSizeRowsMode)Enum.Parse(typeof(DataGridViewAutoSizeRowsMode), name);
        }
    }

    private void knudRowHeadersWidth_ValueChanged(object sender, EventArgs e)
    {
        if (_suppressOptionSync)
        {
            return;
        }
        var requested = (int)knudRowHeadersWidth.Value;
        var safe = Math.Max(4, requested);
        if (safe != requested)
        {
            _suppressOptionSync = true;
            knudRowHeadersWidth.Value = safe;
            _suppressOptionSync = false;
        }
        kdgvMain.RowHeadersWidth = safe;
    }

    private void knudColumnHeadersHeight_ValueChanged(object sender, EventArgs e)
    {
        if (_suppressOptionSync)
        {
            return;
        }
        var requested = (int)knudColumnHeadersHeight.Value;
        kdgvMain.ColumnHeadersHeight = requested;
        var effective = kdgvMain.ColumnHeadersHeight;
        if (effective != requested)
        {
            _suppressOptionSync = true;
            knudColumnHeadersHeight.Value = effective;
            _suppressOptionSync = false;
        }
    }

    private void kdgvMain_RowHeadersWidthChanged(object sender, EventArgs e)
    {
        _suppressOptionSync = true;
        knudRowHeadersWidth.Value = kdgvMain.RowHeadersWidth;
        _suppressOptionSync = false;
    }

    private void kdgvMain_ColumnHeadersHeightChanged(object sender, EventArgs e)
    {
        _suppressOptionSync = true;
        knudColumnHeadersHeight.Value = kdgvMain.ColumnHeadersHeight;
        _suppressOptionSync = false;
    }

    private void kdgvMain_ColumnAdded(object? sender, DataGridViewColumnEventArgs e)
    {
        if (kdgvMain.SelectionMode == DataGridViewSelectionMode.ColumnHeaderSelect && e.Column.SortMode == DataGridViewColumnSortMode.Automatic)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.Programmatic;
        }
    }

    private void EnsureSelectionModeCompatibility(DataGridViewSelectionMode newMode)
    {
        if (newMode == DataGridViewSelectionMode.ColumnHeaderSelect)
        {
            // Ensure headers are visible for header selection
            if (!kdgvMain.ColumnHeadersVisible)
            {
                _suppressOptionSync = true;
                kdgvMain.ColumnHeadersVisible = true;
                kchkColumnHeadersVisible.Checked = true;
                _suppressOptionSync = false;
            }

            foreach (DataGridViewColumn column in kdgvMain.Columns)
            {
                if (column.SortMode == DataGridViewColumnSortMode.Automatic)
                {
                    column.SortMode = DataGridViewColumnSortMode.Programmatic;
                }
            }
        }
        else if (newMode == DataGridViewSelectionMode.RowHeaderSelect)
        {
            // Ensure row headers are visible for header selection
            if (!kdgvMain.RowHeadersVisible)
            {
                _suppressOptionSync = true;
                kdgvMain.RowHeadersVisible = true;
                kchkRowHeadersVisible.Checked = true;
                _suppressOptionSync = false;
            }
        }
    }

    private void kcbGridRtl_CheckedChanged(object sender, EventArgs e)
    {
        kdgvMain.RightToLeft = kdgvMain.RightToLeft == RightToLeft.Yes ? RightToLeft.No : RightToLeft.Yes;
    }

    private void kdgvMain_EditingControlButtonSpecClick(object sender, DataGridViewButtonSpecClickEventArgs e)
    {
        if (e.Cell is KryptonDataGridViewTextBoxCell textCell)
        {
            switch (e.ButtonSpec.Type)
            {
                case PaletteButtonSpecStyle.FormClose:
                    if (textCell.Value is string cellValue)
                    {
                        textCell.Value = cellValue == "[redacted]" ? "" : "[redacted]";
                    }
                    break;
            }
        }
    }
}
