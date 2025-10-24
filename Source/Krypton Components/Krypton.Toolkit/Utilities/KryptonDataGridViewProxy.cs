#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public class KryptonDataGridViewProxy
{
    #region Instance Fields

    private readonly KryptonDataGridView _dataGridView;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonDataGridViewProxy" /> class.</summary>
    /// <param name="dataGridView">The data grid view.</param>
    public KryptonDataGridViewProxy(KryptonDataGridView dataGridView)
    {
        _dataGridView = dataGridView;
    }

    #endregion

    #region Public

    [Category("Appearance")]
    [Description("The height, in pixels, of the column headers row.")]
    public int ColumnHeadersHeight
    {
        get => _dataGridView.ColumnHeadersHeight;
        set => _dataGridView.ColumnHeadersHeight = value;
    }

    [Category("Appearance")]
    [Description("Indicates whether the column headers row is displayed.")]
    public bool ColumnHeadersVisible
    {
        get => _dataGridView.ColumnHeadersVisible;
        set => _dataGridView.ColumnHeadersVisible = value;
    }

    [Category("Appearance")]
    [Description("Indicates whether the column that contains row headers is displayed.")]
    public bool RowHeadersVisible
    {
        get => _dataGridView.RowHeadersVisible;
        set => _dataGridView.RowHeadersVisible = value;
    }

    [Category("Appearance")]
    [Description("Indicates whether to show cell errors.")]
    public bool ShowCellErrors
    {
        get => _dataGridView.ShowCellErrors;
        set => _dataGridView.ShowCellErrors = value;
    }

    [Category("Appearance")]
    [Description("Indicates whether or not ToolTips will show when the mouse pointer pauses on a cell.")]
    public bool ShowCellToolTips
    {
        get => _dataGridView.ShowCellToolTips;
        set => _dataGridView.ShowCellToolTips = value;
    }

    [Category("Appearance")]
    [Description("Indicates whether or not the editing glyph is visible in the row header of the cell being edited.")]
    public bool ShowEditingIcon
    {
        get => _dataGridView.ShowEditingIcon;
        set => _dataGridView.ShowEditingIcon = value;
    }

    [Category("Appearance")]
    [Description("Indicates whether row headers will display error glyphs for each row that contains a data entry error.")]
    public bool ShowRowErrors
    {
        get => _dataGridView.ShowRowErrors;
        set => _dataGridView.ShowRowErrors = value;
    }

    [Category("Behavior")]
    [Description("Indicates whether the option to add rows is displayed to the user.")]
    public bool AllowUserToAddRows
    {
        get => _dataGridView.AllowUserToAddRows;
        set => _dataGridView.AllowUserToAddRows = value;
    }

    [Category("Behavior")]
    [Description("Indicates whether the user is allowed to delete rows from the DataGridView.")]
    public bool AllowUserToDeleteRows
    {
        get => _dataGridView.AllowUserToDeleteRows;
        set => _dataGridView.AllowUserToDeleteRows = value;
    }

    [Category("Behavior")]
    [Description("Indicates whether manual column repositioning is enabled.")]
    public bool AllowUserToOrderColumns
    {
        get => _dataGridView.AllowUserToOrderColumns;
        set => _dataGridView.AllowUserToOrderColumns = value;
    }

    [Category("Behavior")]
    [Description("Indicates whether users can resize columns.")]
    public bool AllowUserToResizeColumns
    {
        get => _dataGridView.AllowUserToResizeColumns;
        set => _dataGridView.AllowUserToResizeColumns = value;
    }

    [Category("Behavior")]
    [Description("Indicates whether users can resize rows.")]
    public bool AllowUserToResizeRows
    {
        get => _dataGridView.AllowUserToResizeRows;
        set => _dataGridView.AllowUserToResizeRows = value;
    }

    [Category("Behavior")]
    [Description("Determines the behavior for adjusting the column headers height.")]
    public DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode
    {
        get => _dataGridView.ColumnHeadersHeightSizeMode;
        set => _dataGridView.ColumnHeadersHeightSizeMode = value;
    }

    [Category("Behavior")]
    [Description("Identifies the mode that determines the cell editing is started.")]
    public DataGridViewEditMode EditMode
    {
        get => _dataGridView.EditMode;
        set => _dataGridView.EditMode = value;
    }

    [Category("Behavior")]
    [Description("Indicates whether the user is allowed to selected more than one cell, row or column of hte DataGridView at a time.")]
    public bool MultiSelect
    {
        get => _dataGridView.MultiSelect;
        set => _dataGridView.MultiSelect = value;
    }

    [Category("Behavior")]
    [Description("Indicates whether the user can edit the cells of the DataGridView control.")]
    public bool ReadOnly
    {
        get => _dataGridView.ReadOnly;
        set => _dataGridView.ReadOnly = value;
    }

    [Category("Behavior")]
    [Description("Determine the behavior for adjusting the row headers width.")]
    public DataGridViewRowHeadersWidthSizeMode RowHeadersWidthSizeMode
    {
        get => _dataGridView.RowHeadersWidthSizeMode;
        set => _dataGridView.RowHeadersWidthSizeMode = value;
    }

    [Category("Behavior")]
    [Description("Indicates how the cells of the DataGridView can be selected.")]
    public DataGridViewSelectionMode SelectionMode
    {
        get => _dataGridView.SelectionMode;
        set => _dataGridView.SelectionMode = value;
    }

    [Category("Behavior")]
    [Description("Indicates whether the TAB key moves the focus to the next control in the tab order rather than moving focus to the next cell in the control.")]
    public bool StandardTab
    {
        get => _dataGridView.StandardTab;
        set => _dataGridView.StandardTab = value;
    }

    [Category("Layout")]
    [Description("Determines the auto size mode for the visible columns.")]
    public DataGridViewAutoSizeColumnsMode AutoSizeColumnsMode
    {
        get => _dataGridView.AutoSizeColumnsMode;
        set => _dataGridView.AutoSizeColumnsMode = value;
    }

    [Category("Layout")]
    [Description("Determines the auto size mode for the visible rows.")]
    public DataGridViewAutoSizeRowsMode AutoSizeRowsMode
    {
        get => _dataGridView.AutoSizeRowsMode;
        set => _dataGridView.AutoSizeRowsMode = value;
    }

    [Category("Layout")]
    [Description("The width, in pixels, of the column that contains the row headers.")]
    public int RowHeadersWidth
    {
        get => _dataGridView.RowHeadersWidth;
        set => _dataGridView.RowHeadersWidth = value;
    }

    [Category("Layout")]
    [Description("The type of scrollbars to display.")]
    public ScrollBars ScrollBars
    {
        get => _dataGridView.ScrollBars;
        set => _dataGridView.ScrollBars = value;
    }

    [Category("Visuals")]
    [Description("Determine if the outer borders of the grid cells are drawn.")]
    public bool HideOuterBorders
    {
        get => _dataGridView.HideOuterBorders;
        set => _dataGridView.HideOuterBorders = value;
    }

    [Category("Visuals")]
    [Description("Overrides for defining common data grid view appearance that other states can override.")]
    public PaletteDataGridViewRedirect StateCommon => _dataGridView.StateCommon;

    [Category("Visuals")]
    [Description("Overrides for defining disabled data grid view appearance.")]
    public PaletteDataGridViewAll StateDisabled => _dataGridView.StateDisabled;

    [Category("Visuals")]
    [Description("Overrides for defining normal data grid view appearance.")]
    public PaletteDataGridViewAll StateNormal => _dataGridView.StateNormal;

    [Category("Visuals")]
    [Description("Overrides for defining tracking data grid view appearance.")]
    public PaletteDataGridViewHeaders StateTracking => _dataGridView.StateTracking;

    [Category("Visuals")]
    [Description("Overrides for defining pressed data grid view appearance.")]
    public PaletteDataGridViewHeaders StatePressed => _dataGridView.StatePressed;

    [Category("Visuals")]
    [Description("Overrides for defining selected data grid view appearance.")]
    public PaletteDataGridViewCells StateSelected => _dataGridView.StateSelected;

    [Category("Visuals")]
    [Description("Set of grid styles.")]
    public DataGridViewStyles GridStyles => _dataGridView.GridStyles;

    [Category("Visuals")]
    [Description("Palette applied to drawing.")]
    public PaletteMode PaletteMode
    {
        get => _dataGridView.PaletteMode;
        set => _dataGridView.PaletteMode = value;
    }

    [Category("Layout")]
    [Description("The size of the control is pixels.")]
    public Size Size
    {
        get => _dataGridView.Size;
        set => _dataGridView.Size = value;
    }

    [Category("Layout")]
    [Description("The location of the control in pixels.")]
    public Point Location
    {
        get => _dataGridView.Location;
        set => _dataGridView.Location = value;
    }

    [Category("Behavior")]
    [Description("Indicates whether the control is enabled.")]
    public bool Enabled
    {
        get => _dataGridView.Enabled;
        set => _dataGridView.Enabled = value;
    }

    #endregion
}