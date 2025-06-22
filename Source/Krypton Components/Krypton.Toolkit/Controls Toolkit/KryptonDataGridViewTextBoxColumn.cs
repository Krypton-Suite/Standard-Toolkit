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

// ReSharper disable MemberCanBeInternal
// ReSharper disable EventNeverSubscribedTo.Global

namespace Krypton.Toolkit;

/// <summary>
/// Hosts a collection of KryptonDataGridViewTextBoxCell cells.
/// </summary>
[ToolboxBitmap(typeof(KryptonDataGridViewTextBoxColumn), "ToolboxBitmaps.KryptonTextBox.bmp")]
public class KryptonDataGridViewTextBoxColumn : KryptonDataGridViewIconColumn
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDataGridViewTextBoxColumn class.
    /// </summary>
    public KryptonDataGridViewTextBoxColumn()
        : base(new KryptonDataGridViewTextBoxCell()) =>
        SortMode = DataGridViewColumnSortMode.Automatic;

    /// <summary>
    /// Returns a String that represents the current Object.
    /// </summary>
    /// <returns>A String that represents the current Object.</returns>
    public override string ToString()
    {
        var builder = new StringBuilder(0x40);
        builder.Append("KryptonDataGridViewTextBoxColumn { Name=");
        // ReSharper disable RedundantBaseQualifier
        builder.Append(base.Name);
        builder.Append(", Index=");
        builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
        // ReSharper restore RedundantBaseQualifier
        builder.Append(" }");
        return builder.ToString();
    }

    /// <summary>
    /// Create a cloned copy of the column.
    /// </summary>
    /// <returns></returns>
    public override object Clone()
    {
        var cloned = base.Clone() as KryptonDataGridViewTextBoxColumn ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("coned"));

        cloned.Multiline = Multiline;
        cloned.MultilineStringEditor = MultilineStringEditor;

        return cloned;
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public
    /// <summary>
    /// Gets or sets the maximum number of characters that can be entered into the text box.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(32767)]
    public int MaxInputLength
    {
        get =>
            TextBoxCellTemplate?.MaxInputLength ?? throw new InvalidOperationException("KryptonDataGridViewTextBoxColumn cell template required");

        set
        {
            if (MaxInputLength != value)
            {
                TextBoxCellTemplate!.MaxInputLength = value;

                if (DataGridView is not null)
                {
                    DataGridViewRowCollection rows = DataGridView.Rows;
                    var count = rows.Count;
                    for (var i = 0; i < count; i++)
                    {
                        if (rows.SharedRow(i).Cells[Index] is DataGridViewTextBoxCell cell)
                        {
                            cell.MaxInputLength = value;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets the sort mode for the column.
    /// </summary>
    [DefaultValue(DataGridViewColumnSortMode.Automatic)]
    public new DataGridViewColumnSortMode SortMode
    {
        get => base.SortMode;
        set => base.SortMode = value;
    }

    /// <summary>
    /// Gets or sets the template used to model cell appearance.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override DataGridViewCell? CellTemplate
    {
        // base.CellTemplate can be null for getter and setter

        get => base.CellTemplate;

        set
        {
            if ((value is not null) && value is not KryptonDataGridViewTextBoxCell)
            {
                throw new InvalidCastException("Can only assign a object of type KryptonDataGridViewTextBoxCell");
            }

            base.CellTemplate = (KryptonDataGridViewTextBoxCell)value!;
        }
    }

    /// <summary>
    /// Make sure that when the style is set that the datagrid respects the values
    /// </summary>
    [Browsable(true)]
    [Category(@"Appearance")]
    [Description(@"DataGridView Column DefaultCell Style\r\nIf you set wrap mode, then this will ensure the DataRows are set to display the wrapped text!")]
    [AllowNull]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public override DataGridViewCellStyle DefaultCellStyle
    {
        // base.DefaultCellStyle will take a null value and handle it.
        // [NotNull] if the base getter encounters a null value it will always return a DefaultCellStyle

        get => base.DefaultCellStyle;

        set
        {
            base.DefaultCellStyle = value;

            if (value is null
                || value.WrapMode != DataGridViewTriState.True
                || DataGridView == null)
            {
                return;
            }

            // https://stackoverflow.com/questions/16514352/multiple-lines-in-a-datagridview-cell/16514393
            switch (DataGridView.AutoSizeRowsMode)
            {
                case DataGridViewAutoSizeRowsMode.AllCells:
                case DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders:
                case DataGridViewAutoSizeRowsMode.DisplayedCells:
                case DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders:
                    break;
                case DataGridViewAutoSizeRowsMode.AllHeaders:
                    DataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                    break;
                case DataGridViewAutoSizeRowsMode.DisplayedHeaders:
                    DataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                    break;
                case DataGridViewAutoSizeRowsMode.None:
                    DataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
                    break;
            }
        }
    }

    /// <summary>
    /// Replicates the Multiline property of the KryptonDataGridViewTextBoxCell cell type.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description(@"Indicates whether the text in the editing control can span more than one line.")]
    public bool Multiline
    {
        get =>
            TextBoxCellTemplate?.Multiline ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (TextBoxCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            TextBoxCellTemplate.Multiline = value;
            if (DataGridView != null)
            {
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewTextBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetMultiline(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Replicates the MultilineStringEditor property of the KryptonDataGridViewTextBoxCell cell type.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description(@"Indicates whether the editing control uses the multiline string editor widget.")]
    public bool MultilineStringEditor
    {
        get =>
            TextBoxCellTemplate?.MultilineStringEditor ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (TextBoxCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            TextBoxCellTemplate.MultilineStringEditor = value;
            if (DataGridView != null)
            {
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewTextBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetMultilineStringEditor(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }


    #endregion

    #region Private
    private KryptonDataGridViewTextBoxCell? TextBoxCellTemplate => CellTemplate as KryptonDataGridViewTextBoxCell;

    #endregion

}