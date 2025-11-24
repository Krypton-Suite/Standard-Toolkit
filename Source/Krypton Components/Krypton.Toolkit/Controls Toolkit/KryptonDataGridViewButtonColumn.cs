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
/// Hosts a collection of KryptonDataGridViewButtonCell cells.
/// </summary>
[ToolboxBitmap(typeof(KryptonDataGridViewButtonColumn), "ToolboxBitmaps.KryptonButton.bmp")]
public class KryptonDataGridViewButtonColumn : KryptonDataGridViewIconColumn
{
    #region Static Fields
    private MethodInfo? _miColumnCommonChange;
    private PropertyInfo _piUseColumnTextForButtonValueInternal;
    #endregion

    #region Instance Fields
    private string? _text;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDataGridViewButtonColumn class.
    /// </summary>
    public KryptonDataGridViewButtonColumn()
        : base(new KryptonDataGridViewButtonCell())
    {
        var style = new DataGridViewCellStyle
        {
            Alignment = DataGridViewContentAlignment.MiddleCenter
        };
        DefaultCellStyle = style;
    }

    /// <summary>
    /// Returns a String that represents the current Object.
    /// </summary>
    /// <returns>A String that represents the current Object.</returns>
    public override string ToString()
    {
        var builder = new StringBuilder(0x40);
        builder.Append(@"KryptonDataGridViewButtonColumn { Name=");
        // ReSharper disable RedundantBaseQualifier
        builder.Append(base.Name);
        builder.Append(@", Index=");
        builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
        // ReSharper restore RedundantBaseQualifier
        builder.Append(@" }");
        return builder.ToString();
    }

    /// <summary>
    /// This member overrides DataGridViewButtonColumn.Clone.
    /// </summary>
    /// <returns>New object instance.</returns>
    public override object Clone()
    {
        // Create a new instance
        var clone = base.Clone() as KryptonDataGridViewButtonColumn ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("clone"));
        clone.Text = Text;

        return clone;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets or sets the template used to model cell appearance.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override DataGridViewCell? CellTemplate
    {
        /*
         Text from the base property, shows that it can be null
         //
         // Summary:
         //     Gets or sets the template used to create new cells.
         //
         // Returns:
         //     A System.Windows.Forms.DataGridViewCell that all other cells in the column are
         //     modeled after. The default is null.
        */
 
        get => base.CellTemplate;

        set
        {
            if ((value != null) && value is not KryptonDataGridViewButtonCell)
            {
                throw new InvalidCastException(@"Can only assign a object of type KryptonDataGridViewButtonCell");
            }

            base.CellTemplate = value;
        }
    }

    /// <summary>
    /// Gets or sets the column's default cell style.
    /// </summary>
    [Browsable(true)]
    [Category(@"Appearance")]
    [AllowNull]
    public override DataGridViewCellStyle DefaultCellStyle
    {
        // Data type made non-nullable again to keep it inline with the underlying virtual base method 
        // Added [AllowNull] attribute since the base can take null as a value

        get => base.DefaultCellStyle;
        set => base.DefaultCellStyle = value;
    }

    /// <summary>
    /// Gets or sets the default text Displayed on the button cell.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(null)]
    public string? Text
    {
        get => _text;
        set
        {
            if (!string.Equals(value, _text, StringComparison.Ordinal))
            {
                _text = value;
                if (DataGridView != null)
                {
                    if (UseColumnTextForButtonValue)
                    {
                        ColumnCommonChange(Index);
                    }
                    else
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        var count = rows.Count;
                        for (var i = 0; i < count; i++)
                        {
                            if ((rows.SharedRow(i).Cells[Index] is KryptonDataGridViewButtonCell
                                {
                                    UseColumnTextForButtonValue: true
                                }))
                            {
                                ColumnCommonChange(Index);
                                return;
                            }
                        }
                        DataGridView.InvalidateColumn(Index);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the Text property value is Displayed as the button text for cells in this column.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(false)]
    public bool UseColumnTextForButtonValue
    {
        get =>
            (CellTemplate as KryptonDataGridViewButtonCell)?.UseColumnTextForButtonValue ?? throw new InvalidOperationException(@"KryptonDataGridViewButtonColumn cell template required");

        set
        {
            if (UseColumnTextForButtonValue != value)
            {
                SetUseColumnTextForButtonValueInternal(CellTemplate!, value);
                if (DataGridView != null)
                {
                    DataGridViewRowCollection rows = DataGridView.Rows;
                    var count = rows.Count;
                    for (var i = 0; i < count; i++)
                    {
                        DataGridViewButtonCell? cell = rows.SharedRow(i).Cells[Index] as KryptonDataGridViewButtonCell;
                        if (cell != null)
                        {
                            SetUseColumnTextForButtonValueInternal(cell, value);
                        }
                    }
                    ColumnCommonChange(Index);
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the Text property value is Displayed as the button text for cells in this column.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(ButtonStyle.Standalone)]
    public ButtonStyle ButtonStyle
    {
        get =>
            (CellTemplate as KryptonDataGridViewButtonCell)?.ButtonStyle ?? throw new InvalidOperationException(@"KryptonDataGridViewButtonColumn cell template required");

        set
        {
            if (ButtonStyle != value)
            {
                (CellTemplate as KryptonDataGridViewButtonCell)!.ButtonStyleInternal = value;

                if (DataGridView != null)
                {
                    DataGridViewRowCollection rows = DataGridView.Rows;
                    var count = rows.Count;
                    for (var i = 0; i < count; i++)
                    {
                        if (rows.SharedRow(i).Cells[Index] is KryptonDataGridViewButtonCell cell)
                        {
                            cell.ButtonStyleInternal = value;
                        }
                    }
                    ColumnCommonChange(Index);
                }
            }
        }
    }
    #endregion

    #region Private
    private bool ShouldSerializeDefaultCellStyle()
    {
        if (!HasDefaultCellStyle)
        {
            return false;
        }

        DataGridViewCellStyle defaultCellStyle = DefaultCellStyle!;

        return !defaultCellStyle.BackColor.IsEmpty 
               || !defaultCellStyle.ForeColor.IsEmpty 
               || !defaultCellStyle.SelectionBackColor.IsEmpty 
               || !defaultCellStyle.SelectionForeColor.IsEmpty 
               || defaultCellStyle.Font != null 
               || !defaultCellStyle.IsNullValueDefault 
               || !defaultCellStyle.IsDataSourceNullValueDefault 
               || !string.IsNullOrEmpty(defaultCellStyle.Format) 
               || !defaultCellStyle.FormatProvider.Equals(CultureInfo.CurrentCulture) 
               || defaultCellStyle.Alignment != DataGridViewContentAlignment.MiddleCenter 
               || defaultCellStyle.WrapMode != DataGridViewTriState.NotSet 
               || defaultCellStyle.Tag != null
               || !defaultCellStyle.Padding.Equals(Padding.Empty);
    }

    private void ColumnCommonChange(int columnIndex)
    {
        // Only need to cache reflection info the first time around
        if (_miColumnCommonChange == null)
        {
            // Cache access to the internal method 'OnColumnCommonChange'
            _miColumnCommonChange = typeof(DataGridView).GetMethod(@"OnColumnCommonChange", BindingFlags.Instance |
                BindingFlags.NonPublic |
                BindingFlags.GetField);

        }

        _miColumnCommonChange?.Invoke(DataGridView, [columnIndex]);
    }

    private void SetUseColumnTextForButtonValueInternal(object instance, bool value)
    {
        // Only need to cache reflection info the first time around
        if (_piUseColumnTextForButtonValueInternal == null)
        {
            // Cache access to the internal property sette 'UseColumnTextForButtonValueInternal'
            _piUseColumnTextForButtonValueInternal = typeof(DataGridViewButtonCell).GetProperty(@"UseColumnTextForButtonValueInternal", BindingFlags.Instance |
                BindingFlags.NonPublic |
                BindingFlags.SetProperty)!;

        }

        _piUseColumnTextForButtonValueInternal?.SetValue(instance, value, null);
    }
    #endregion
}