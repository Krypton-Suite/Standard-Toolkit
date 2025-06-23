#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

// ReSharper disable UnusedMember.Global

namespace Krypton.Toolkit;

/// <summary>
/// Hosts a collection of KryptonDataGridViewComboBoxCell cells.
/// </summary>
[ToolboxBitmap(typeof(KryptonDataGridViewComboBoxColumn), "ToolboxBitmaps.KryptonComboBox.bmp")]
public partial class KryptonDataGridViewComboBoxColumn : KryptonDataGridViewIconColumn
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDataGridViewComboBoxColumn class.
    /// </summary>
    public KryptonDataGridViewComboBoxColumn()
        : base(new KryptonDataGridViewComboBoxCell())
    {
        Items = [];
        AutoCompleteCustomSource = [];
        _kryptonDataGridViewCellIndicatorImage = new();
    }

    /// <inheritdoc/>
    protected override void OnDataGridViewChanged()
    {
        _kryptonDataGridViewCellIndicatorImage.DataGridView = DataGridView as KryptonDataGridView;
        base.OnDataGridViewChanged();
    }

    /// <summary>
    /// Returns a standard compact string representation of the column.
    /// </summary>
    public override string ToString()
    {
        var builder = new StringBuilder(0x40);
        builder.Append(@"KryptonDataGridViewComboBoxColumn { Name=");
        // ReSharper disable RedundantBaseQualifier
        builder.Append(base.Name);
        builder.Append(@", Index=");
        builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
        // ReSharper restore RedundantBaseQualifier
        builder.Append(@" }");
        return builder.ToString();
    }

    /// <summary>
    /// Create a cloned copy of the column.
    /// </summary>
    /// <returns></returns>
    public override object Clone()
    {
        var cloned = base.Clone() as KryptonDataGridViewComboBoxColumn ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("clone"));

        cloned.Items.AddRange(Items);

        // Convert collection of strings to an array
        var strings = new string[AutoCompleteCustomSource.Count];
        for (var i = 0; i < strings.Length; i++)
        {
            strings[i] = AutoCompleteCustomSource[i];
        }

        cloned.AutoCompleteCustomSource.AddRange(strings);

        return cloned;
    }
    #endregion

    #region Public
    /// <summary>
    /// Represents the implicit cell that gets cloned when adding rows to the grid.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override DataGridViewCell? CellTemplate {
        get => base.CellTemplate;

        set
        {
            if ((value != null)
                && value is not KryptonDataGridViewComboBoxCell
               )
            {
                throw new InvalidCastException(@"Value provided for CellTemplate must be of type KryptonDataGridViewComboBoxCell or derive from it.");
            }

            base.CellTemplate = value;
        }
    }

    /// <summary>
    /// Gets the collection of allowable items of the domain up down.
    /// </summary>
    [Category(@"Data")]
    [Description(@"The allowable items of the domain up down.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(@"System.Windows.Forms.Design.StringCollectionEditor", typeof(UITypeEditor))]
    [Localizable(true)]
    public List<object> Items { get; }

    private bool ShouldSerializeItems() => Items.Any();

    /// <summary>
    /// Gets and sets the appearance and functionality of the KryptonComboBox.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Controls the appearance and functionality of the KryptonComboBox.")]
    [DefaultValue(ComboBoxStyle.DropDown)]
    [RefreshProperties(RefreshProperties.Repaint)]
    public ComboBoxStyle DropDownStyle {
        get =>
            ComboBoxCellTemplate?.DropDownStyle ?? throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

        set
        {
            if (ComboBoxCellTemplate == null)
            {
                throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            ComboBoxCellTemplate.DropDownStyle = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewComboBoxCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewComboBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetDropDownStyle(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Gets and sets the maximum number of entries to display in the drop-down list.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The maximum number of entries to display in the drop-down list.")]
    [Localizable(true)]
    [DefaultValue(8)]
    public int MaxDropDownItems {
        get =>
            ComboBoxCellTemplate?.MaxDropDownItems ?? throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

        set
        {
            if (ComboBoxCellTemplate == null)
            {
                throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            ComboBoxCellTemplate.MaxDropDownItems = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewComboBoxCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewComboBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetMaxDropDownItems(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Gets and sets the height, in pixels, of the drop-down box in a KryptonComboBox.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The height, in pixels, of the drop-down box in a KryptonComboBox.")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DefaultValue(200)]
    [Browsable(true)]
    public int DropDownHeight {
        get =>
            ComboBoxCellTemplate?.DropDownHeight ?? throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

        set
        {
            if (ComboBoxCellTemplate == null)
            {
                throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            ComboBoxCellTemplate.DropDownHeight = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewComboBoxCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewComboBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetMaxDropDownItems(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Gets and sets the width, in pixels, of the drop-down box in a KryptonComboBox.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The width, in pixels, of the drop-down box in a KryptonComboBox.")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int DropDownWidth {
        get =>
            ComboBoxCellTemplate?.DropDownWidth ?? throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

        set
        {
            if (ComboBoxCellTemplate == null)
            {
                throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            ComboBoxCellTemplate.DropDownWidth = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewComboBoxCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewComboBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetDropDownWidth(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Gets or sets the StringCollection to use when the AutoCompleteSource property is set to CustomSource.
    /// </summary>
    [Description(@"The StringCollection to use when the AutoCompleteSource property is set to CustomSource.")]
    [Editor(@"System.Windows.Forms.Design.ListControlStringCollectionEditor", typeof(UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Localizable(true)]
    [Browsable(true)]
    public AutoCompleteStringCollection AutoCompleteCustomSource { get; }

    private bool ShouldSerializeAutoCompleteCustomSource() => AutoCompleteCustomSource.Count > 0;

    /// <summary>
    /// Gets or sets the text completion behavior of the combobox.
    /// </summary>
    [Description(@"Indicates the text completion behavior of the combobox.")]
    [DefaultValue(AutoCompleteMode.None)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    public AutoCompleteMode AutoCompleteMode {
        get =>
            ComboBoxCellTemplate?.AutoCompleteMode ?? throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

        set
        {
            if (ComboBoxCellTemplate == null)
            {
                throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            ComboBoxCellTemplate.AutoCompleteMode = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewComboBoxCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewComboBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetAutoCompleteMode(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Gets or sets the autocomplete source, which can be one of the values from AutoCompleteSource enumeration.
    /// </summary>
    [Description(@"The autocomplete source, which can be one of the values from AutoCompleteSource enumeration.")]
    [DefaultValue(AutoCompleteSource.None)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    public AutoCompleteSource AutoCompleteSource {
        get =>
            ComboBoxCellTemplate?.AutoCompleteSource ?? throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

        set
        {
            if (ComboBoxCellTemplate == null)
            {
                throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            ComboBoxCellTemplate.AutoCompleteSource = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewComboBoxCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewComboBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetAutoCompleteSource(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Gets and sets the appearance and functionality of the KryptonComboBox.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates the property to display for the items in this control.")]
    [TypeConverter(@"System.Windows.Forms.Design.DataMemberFieldConverter")]
    [Editor(@"System.Windows.Forms.Design.DataMemberFieldEditor", typeof(UITypeEditor))]
    [DefaultValue(@"")]
    public string DisplayMember {
        get =>
            ComboBoxCellTemplate == null
                ? throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.")
                : ComboBoxCellTemplate.DisplayMember;

        set
        {
            if (ComboBoxCellTemplate == null)
            {
                throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            ComboBoxCellTemplate.DisplayMember = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewComboBoxCell cells in the column accordingly.
                DataGridViewRow dataGridViewRow;
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewComboBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetDisplayMember(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Gets and sets the appearance and functionality of the KryptonComboBox.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates the property to display for the items in this control.")]
    [TypeConverter(@"System.Windows.Forms.Design.DataMemberFieldConverter")]
    [Editor(@"System.Windows.Forms.Design.DataMemberFieldEditor", typeof(UITypeEditor))]
    [DefaultValue(@"")]
    public string ValueMember {
        get =>
            ComboBoxCellTemplate == null
                ? throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.")
                : ComboBoxCellTemplate.ValueMember;

        set
        {
            if (ComboBoxCellTemplate == null)
            {
                throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            ComboBoxCellTemplate.ValueMember = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewComboBoxCell cells in the column accordingly.
                DataGridViewRow dataGridViewRow;
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewComboBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetValueMember(rowIndex, value);
                    }
                }
                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// "Indicates the Datasource for the items in this control.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates the Datasource for the items in this control.")]
    [TypeConverter(@"System.Windows.Forms.Design.DataSourceConverter")]
    [Editor(@"System.Windows.Forms.Design.DataSourceListEditor", typeof(UITypeEditor))]
    [DefaultValue(null)]
    public object? DataSource {

        get =>
            ComboBoxCellTemplate == null
                ? throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.")
                : ComboBoxCellTemplate.DataSource;

        set
        {
            if (ComboBoxCellTemplate == null)
            {
                throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            ComboBoxCellTemplate.DataSource = value;

            if (DataGridView is not null)
            {
                DataGridViewRow dataGridViewRow;
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                int rowCount = dataGridViewRows.Count;
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewComboBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.DataSource = value;
                    }
                }
            }
        }
    }
    #endregion

    #region Private
    /// <summary>
    /// Small utility function that returns the template cell as a KryptonDataGridViewComboBoxCell
    /// </summary>
    private KryptonDataGridViewComboBoxCell? ComboBoxCellTemplate => CellTemplate as KryptonDataGridViewComboBoxCell;
    // Cell indicator image instance
    private KryptonDataGridViewCellIndicatorImage _kryptonDataGridViewCellIndicatorImage;

    #endregion

    #region Internal
    /// <summary>
    /// Provides the cell indicator images to the cells from from this column instance.<br/>
    /// For internal use only.
    /// </summary>
    internal Image? CellIndicatorImage => _kryptonDataGridViewCellIndicatorImage.Image;
    #endregion Internal

}