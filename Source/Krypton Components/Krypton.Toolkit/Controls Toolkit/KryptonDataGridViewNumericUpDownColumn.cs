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

namespace Krypton.Toolkit;

/// <summary>
/// Hosts a collection of KryptonDataGridViewNumericUpDownCell cells.
/// </summary>
[ToolboxBitmap(typeof(KryptonDataGridViewNumericUpDownColumn), "ToolboxBitmaps.KryptonNumericUpDown.bmp")]
public class KryptonDataGridViewNumericUpDownColumn : KryptonDataGridViewIconColumn
{
    #region Fields
    // Cell indicator image instance
    private KryptonDataGridViewCellIndicatorImage _kryptonDataGridViewCellIndicatorImage;
    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the KryptonDataGridViewNumericUpDownColumn class.
    /// </summary>
    public KryptonDataGridViewNumericUpDownColumn()
        : base(new KryptonDataGridViewNumericUpDownCell())
    {
        _kryptonDataGridViewCellIndicatorImage = new();
    }

    /// <summary>
    /// Returns a standard compact string representation of the column.
    /// </summary>
    public override string ToString()
    {
        var builder = new StringBuilder(0x40);
        builder.Append("KryptonDataGridViewNumericUpDownColumn { Name=");
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
        var cloned = base.Clone() as KryptonDataGridViewNumericUpDownColumn ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("cloned"));

        return cloned;
    }
    #endregion

    #region Public
    /// <summary>
    /// Represents the implicit cell that gets cloned when adding rows to the grid.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override DataGridViewCell? CellTemplate
    {
        get => base.CellTemplate;
        set
        {
            if ((value != null) && (value is not KryptonDataGridViewNumericUpDownCell))
            {
                throw new InvalidCastException("Value provided for CellTemplate must be of type KryptonDataGridViewNumericUpDownCell or derive from it.");
            }

            base.CellTemplate = value;
        }
    }

    /// <summary>
    /// Replicates the AllowDecimals property of the KryptonDataGridViewNumericUpDownCell cell type.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(true)]
    [Description(@"Indicates whether the control can accept decimal values, rather than integer values only.")]
    public bool AllowDecimals
    {
        get =>
            NumericUpDownCellTemplate?.AllowDecimals ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (NumericUpDownCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            NumericUpDownCellTemplate.AllowDecimals = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewNumericUpDownCell dataGridViewCell)
                    {
                        dataGridViewCell.SetAllowDecimals(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }

        }
    }

    /// <summary>
    /// Replicates the TrailingZeroes property of the KryptonDataGridViewNumericUpDownCell cell type.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(false)]
    [Description(@"Indicates whether the control will display traling zeroes.")]
    public bool TrailingZeroes
    {
        get =>
            NumericUpDownCellTemplate?.TrailingZeroes ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (NumericUpDownCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            NumericUpDownCellTemplate.TrailingZeroes = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewNumericUpDownCell dataGridViewCell)
                    {
                        dataGridViewCell.SetTrailingZeroes(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }

        }
    }
    /// <summary>
    /// Replicates the DecimalPlaces property of the KryptonDataGridViewNumericUpDownCell cell type.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(0)]
    [Description(@"Indicates the number of decimal places to display.")]
    public int DecimalPlaces
    {
        get =>
            NumericUpDownCellTemplate?.DecimalPlaces ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (NumericUpDownCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            NumericUpDownCellTemplate.DecimalPlaces = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewNumericUpDownCell dataGridViewCell)
                    {
                        dataGridViewCell.SetDecimalPlaces(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Gets or sets wheather the numeric up-down should display its value in hexadecimal.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(false)]
    [Description(@"Indicates wheather the numeric up-down should display its value in hexadecimal.")]
    public bool Hexadecimal
    {
        get =>
            NumericUpDownCellTemplate?.Hexadecimal ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (NumericUpDownCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            NumericUpDownCellTemplate.Hexadecimal = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewNumericUpDownCell dataGridViewCell)
                    {
                        dataGridViewCell.SetHexadecimal(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Replicates the Increment property of the DataGridViewNumericUpDownCell cell type.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates the amount to increment or decrement on each button click.")]
    public decimal Increment
    {
        get =>
            NumericUpDownCellTemplate?.Increment ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (NumericUpDownCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            NumericUpDownCellTemplate.Increment = value;
            if (DataGridView != null)
            {
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewNumericUpDownCell dataGridViewCell)
                    {
                        dataGridViewCell.SetIncrement(rowIndex, value);
                    }
                }
            }
        }
    }

    /// Indicates whether the Increment property should be persisted.
    private bool ShouldSerializeIncrement() => !Increment.Equals(decimal.One);

    /// <summary>
    /// Replicates the Maximum property of the KryptonDataGridViewNumericUpDownCell cell type.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates the maximum value for the numeric up-down cells.")]
    [RefreshProperties(RefreshProperties.All)]
    public decimal Maximum
    {
        get =>
            NumericUpDownCellTemplate?.Maximum ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (NumericUpDownCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            NumericUpDownCellTemplate.Maximum = value;
            if (DataGridView != null)
            {
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewNumericUpDownCell dataGridViewCell)
                    {
                        dataGridViewCell.SetMaximum(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// Indicates whether the Maximum property should be persisted.
    private bool ShouldSerializeMaximum() => !Maximum.Equals((decimal)100.0);

    /// <summary>
    /// Replicates the Minimum property of the KryptonDataGridViewNumericUpDownCell cell type.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates the minimum value for the numeric up-down cells.")]
    [RefreshProperties(RefreshProperties.All)]
    public decimal Minimum
    {
        get =>
            NumericUpDownCellTemplate?.Minimum ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (NumericUpDownCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            NumericUpDownCellTemplate.Minimum = value;
            if (DataGridView != null)
            {
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewNumericUpDownCell dataGridViewCell)
                    {
                        dataGridViewCell.SetMinimum(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// Indicates whether the Maximum property should be persisted.
    private bool ShouldSerializeMinimum() => !Minimum.Equals(decimal.Zero);

    /// <summary>
    /// Replicates the ThousandsSeparator property of the KryptonDataGridViewNumericUpDownCell cell type.
    /// </summary>
    [Category(@"Data")]
    [DefaultValue(false)]
    [Description(@"Indicates whether the thousands separator will be inserted between every three decimal digits.")]
    public bool ThousandsSeparator
    {
        get =>
            NumericUpDownCellTemplate?.ThousandsSeparator ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (NumericUpDownCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            NumericUpDownCellTemplate.ThousandsSeparator = value;
            if (DataGridView != null)
            {
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewNumericUpDownCell dataGridViewCell)
                    {
                        dataGridViewCell.SetThousandsSeparator(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }
    #endregion

    #region Private
    /// <summary>
    /// Small utility function that returns the template cell as a KryptonDataGridViewNumericUpDownCell
    /// </summary>
    private KryptonDataGridViewNumericUpDownCell? NumericUpDownCellTemplate => CellTemplate as KryptonDataGridViewNumericUpDownCell;
    #endregion

    #region Protected
    /// <inheritdoc/>
    protected override void OnDataGridViewChanged()
    {
        _kryptonDataGridViewCellIndicatorImage.DataGridView = DataGridView as KryptonDataGridView;
        base.OnDataGridViewChanged();
    }
    #endregion Protected

    #region Internal
    /// <summary>
    /// Provides the cell indicator images to the cells from from this column instance.<br/>
    /// For internal use only.
    /// </summary>
    internal Image? CellIndicatorImage => _kryptonDataGridViewCellIndicatorImage.Image;
    #endregion Internal

}