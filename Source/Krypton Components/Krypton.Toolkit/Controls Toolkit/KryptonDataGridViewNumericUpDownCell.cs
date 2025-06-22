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
/// Defines a KryptonNumericUpDown cell type for the KryptonDataGridView control
/// </summary>
public class KryptonDataGridViewNumericUpDownCell : DataGridViewTextBoxCell
{
    #region Static Fields
    private const DataGridViewContentAlignment ANY_RIGHT = DataGridViewContentAlignment.TopRight | DataGridViewContentAlignment.MiddleRight | DataGridViewContentAlignment.BottomRight;
    private const DataGridViewContentAlignment ANY_CENTER = DataGridViewContentAlignment.TopCenter | DataGridViewContentAlignment.MiddleCenter | DataGridViewContentAlignment.BottomCenter;
    private static readonly Type _defaultEditType = typeof(KryptonDataGridViewNumericUpDownEditingControl);
    private static readonly Type _defaultValueType = typeof(decimal);
    #endregion

    #region Instance Fields
    private int _decimalPlaces;
    private decimal _increment;
    private decimal _minimum;
    private decimal _maximum;
    private bool _thousandsSeparator;
    private bool _hexadecimal;
    private bool _allowDecimals;
    private bool _trailingZeroes;
    #endregion

    #region Identity
    /// <summary>
    /// Constructor for the DataGridViewNumericUpDownCell cell type
    /// </summary>
    public KryptonDataGridViewNumericUpDownCell()
    {
        // Set the default values of the properties:
        _decimalPlaces = 0;
        _increment = decimal.One;
        _minimum = decimal.Zero;
        _maximum = (decimal)100.0;
        _thousandsSeparator = false;
        _hexadecimal = false;
    }

    /// <summary>
    /// Returns a standard textual representation of the cell.
    /// </summary>
    public override string ToString() =>
        $"DataGridViewNumericUpDownCell {{ ColumnIndex={ColumnIndex.ToString(CultureInfo.CurrentCulture)}, RowIndex={RowIndex.ToString(CultureInfo.CurrentCulture)} }}";

    #endregion

    #region Public
    /// <summary>
    /// Define the type of the cell's editing control
    /// </summary>
    public override Type EditType => _defaultEditType;

    /// <summary>
    /// The AllowDecimals property replicates the one from the KryptonNumericUpDown control
    /// </summary>
    [DefaultValue(true)]
    public bool AllowDecimals
    {
        get => _allowDecimals;
        set
        {
            if (_allowDecimals != value)
            {
                SetAllowDecimals(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The TrailingZeroes property replicates the one from the KryptonNumericUpDown control
    /// </summary>
    [DefaultValue(false)]
    public bool TrailingZeroes
    {
        get => _trailingZeroes;
        set
        {
            if (_trailingZeroes != value)
            {
                SetTrailingZeroes(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The DecimalPlaces property replicates the one from the KryptonNumericUpDown control
    /// </summary>
    [DefaultValue(0)]
    public int DecimalPlaces
    {
        get => _decimalPlaces;

        set
        {
            if (value is < 0 or > 99)
            {
                throw new ArgumentOutOfRangeException(nameof(DecimalPlaces), @"The DecimalPlaces property cannot be smaller than 0 or larger than 99.");
            }

            if (_decimalPlaces != value)
            {
                SetDecimalPlaces(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// Indicates wheather the numeric up-down should display its value in hexadecimal.
    /// </summary>
    public bool Hexadecimal
    {
        get => _hexadecimal;

        set
        {
            if (_hexadecimal != value)
            {
                SetHexadecimal(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The Increment property replicates the one from the KryptonNumericUpDown control
    /// </summary>
    public decimal Increment
    {
        get => _increment;

        set
        {
            if (value < (decimal)0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(Increment), @"The Increment property cannot be smaller than 0.");
            }

            SetIncrement(RowIndex, value);
        }
    }

    /// <summary>
    /// The Maximum property replicates the one from the KryptonNumericUpDown control
    /// </summary>
    public decimal Maximum
    {
        get => _maximum;

        set
        {
            if (_maximum != value)
            {
                SetMaximum(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The Minimum property replicates the one from the KryptonNumericUpDown control
    /// </summary>
    public decimal Minimum
    {
        get => _minimum;

        set
        {
            if (_minimum != value)
            {
                SetMinimum(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The ThousandsSeparator property replicates the one from the KryptonNumericUpDown control
    /// </summary>
    [DefaultValue(false)]
    public bool ThousandsSeparator
    {
        get => _thousandsSeparator;

        set
        {
            if (_thousandsSeparator != value)
            {
                SetThousandsSeparator(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// Returns the type of the cell's Value property
    /// </summary>
    public override Type ValueType => base.ValueType ?? _defaultValueType;

    /// <summary>
    /// Clones a DataGridViewNumericUpDownCell cell, copies all the custom properties.
    /// </summary>
    public override object Clone()
    {
        var dataGridViewCell = base.Clone() as KryptonDataGridViewNumericUpDownCell;
        if (dataGridViewCell != null)
        {
            dataGridViewCell.DecimalPlaces = DecimalPlaces;
            dataGridViewCell.Increment = Increment;
            dataGridViewCell.Maximum = Maximum;
            dataGridViewCell.Minimum = Minimum;
            dataGridViewCell.ThousandsSeparator = ThousandsSeparator;
            dataGridViewCell.Hexadecimal = Hexadecimal;
            dataGridViewCell.AllowDecimals = AllowDecimals;
            dataGridViewCell.TrailingZeroes = TrailingZeroes;
        }
        return dataGridViewCell!;
    }

    /// <summary>
    /// DetachEditingControl gets called by the DataGridView control when the editing session is ending
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public override void DetachEditingControl()
    {
        DataGridView dataGridView = DataGridView!;

        if (dataGridView?.EditingControl is null)
        {
            throw new InvalidOperationException("Cell is detached or its grid has no editing control.");
        }

        if (dataGridView.EditingControl is KryptonNumericUpDown numericUpDown)
        {
            if (OwningColumn is KryptonDataGridViewNumericUpDownColumn)
            {
                if (numericUpDown.Controls[0].Controls[1] is TextBox textBox)
                {
                    textBox.ClearUndo();
                }
            }
        }

        base.DetachEditingControl();
    }

    /// <summary>
    /// Custom implementation of the InitializeEditingControl function. This function is called by the DataGridView control 
    /// at the beginning of an editing session. It makes sure that the properties of the KryptonNumericUpDown editing control are 
    /// set according to the cell properties.
    /// </summary>
    public override void InitializeEditingControl(int rowIndex,
        object? initialFormattedValue,
        DataGridViewCellStyle dataGridViewCellStyle)
    {
        base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

        if (DataGridView!.EditingControl is KryptonNumericUpDown numericUpDown)
        {
            numericUpDown.DecimalPlaces = DecimalPlaces;
            numericUpDown.Increment = Increment;
            numericUpDown.Maximum = Maximum;
            numericUpDown.Minimum = Minimum;
            numericUpDown.ThousandsSeparator = ThousandsSeparator;
            numericUpDown.Hexadecimal = Hexadecimal;
            numericUpDown.Value = decimal.TryParse(Value?.ToString() ?? string.Empty, out decimal d)
                ? d     // restore the cell value 
                : 0m;   // if the cell value was null set to zero
        }
    }

    /// <summary>
    /// Custom implementation of the KeyEntersEditMode function. This function is called by the DataGridView control
    /// to decide whether a keystroke must start an editing session or not. In this case, a new session is started when
    /// a digit or negative sign key is hit.
    /// </summary>
    public override bool KeyEntersEditMode(KeyEventArgs e)
    {
        NumberFormatInfo numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;
        var negativeSignKey = Keys.None;
        var negativeSignStr = numberFormatInfo.NegativeSign;
        if (!string.IsNullOrEmpty(negativeSignStr) && (negativeSignStr.Length == 1))
        {
            negativeSignKey = (Keys)PI.VkKeyScan(negativeSignStr[0]);
        }

        return (char.IsDigit((char)e.KeyCode) ||
                e.KeyCode is >= Keys.NumPad0 and <= Keys.NumPad9 ||
                (negativeSignKey == e.KeyCode) ||
                (Keys.Subtract == e.KeyCode)) &&
               !e.Shift && e is { Alt: false, Control: false };
    }
    #endregion

    #region Protected
    ///<inheritdoc/>
    protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object? value,
        object? formattedValue, string? errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
    {
        if (DataGridView is not null
            && KryptonOwningColumn?.CellIndicatorImage is Image image)
        {
            int pos;
            Rectangle textArea;
            var righToLeft = DataGridView.RightToLeft == RightToLeft.Yes;

            if (righToLeft)
            {
                pos = cellBounds.Left;

                // The WinForms cell content always receives padding of one by default, custom padding is added tot that.
                textArea = new Rectangle(
                    1 + cellBounds.Left + cellStyle.Padding.Left + image.Width,
                    1 + cellBounds.Top + cellStyle.Padding.Top,
                    cellBounds.Width - cellStyle.Padding.Left - cellStyle.Padding.Right - image.Width - 3,
                    cellBounds.Height - cellStyle.Padding.Top - cellStyle.Padding.Bottom - 2);
            }
            else
            {
                pos = cellBounds.Right - image.Width;

                // The WinForms cell content always receives padding of one by default, custom padding is added tot that.
                textArea = new Rectangle(
                    1 + cellBounds.Left + cellStyle.Padding.Left,
                    1 + cellBounds.Top + cellStyle.Padding.Top,
                    cellBounds.Width - cellStyle.Padding.Left - cellStyle.Padding.Right - image.Width - 3,
                    cellBounds.Height - cellStyle.Padding.Top - cellStyle.Padding.Bottom - 2);
            }

            // When the Krypton column is part of a WinForms DataGridView let the default paint routine paint the cell.
            // Afterwards we paint the text and drop down image.
            if (DataGridView is DataGridView)
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, null, string.Empty, errorText, cellStyle, advancedBorderStyle, paintParts);
            }

            // Draw the drop down button, only if no ErrorText has been set.
            // If the ErrorText is set, only the error icon is shown. Otherwise both are painted on the same spot.
            string text = string.Empty;

            if (ErrorText.Length == 0)
            {
                graphics.DrawImage(image, new Point(pos, textArea.Top));

                if (DataGridView.Rows.SharedRow(rowIndex).Index != -1
                    && formattedValue is string str
                    && str.Length > 0)
                {
                    text = decimal.TryParse(str, out decimal d)
                        ? d.ToString(InheritedStyle.Format)
                        : str;
                }
            }
            else
            {
                text = ErrorText;
            }

            TextRenderer.DrawText(graphics, text, cellStyle.Font, textArea, cellStyle.ForeColor,
                KryptonDataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(righToLeft, cellStyle.Alignment, cellStyle.WrapMode));
        }
    }
    /// <summary>
    /// Customized implementation of the GetErrorIconBounds function in order to draw the potential 
    /// error icon next to the up/down buttons and not on top of them.
    /// </summary>
    protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
    {
        Rectangle errorIconBounds = base.GetErrorIconBounds(graphics, cellStyle, rowIndex);
        errorIconBounds.X = errorIconBounds.Left;

        return errorIconBounds;
    }

    /// <summary>
    /// Customized implementation of the GetFormattedValue function in order to include the decimal and thousand separator
    /// characters in the formatted representation of the cell value.
    /// </summary>
    protected override object GetFormattedValue(object? value,
        int rowIndex,
        ref DataGridViewCellStyle cellStyle,
        TypeConverter? valueTypeConverter,
        TypeConverter? formattedValueTypeConverter,
        DataGridViewDataErrorContexts context)
    {
        // By default, the base implementation converts the Decimal 1234.5 into the string "1234.5"
        var formattedValue = base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
        var formattedNumber = formattedValue as string;
        if (!string.IsNullOrEmpty(formattedNumber)
            && (value != null)
            && (value != DBNull.Value)
           )
        {
            var unformattedDecimal = Convert.ToDecimal(value);
            var formattedDecimal = Convert.ToDecimal(formattedNumber);
            if (unformattedDecimal == formattedDecimal)
            {
                if (!Hexadecimal && !TrailingZeroes)
                {
                    return formattedDecimal.ToString("0.##############################");
                }
                // The base implementation of GetFormattedValue (which triggers the CellFormatting event) did nothing else than 
                // the typical 1234.5 to "1234.5" conversion. But depending on the values of ThousandsSeparator and DecimalPlaces,
                // this may not be the actual string Displayed. The real formatted value may be "1,234.500"
                return formattedDecimal.ToString((ThousandsSeparator ? "N" : "F") + DecimalPlaces.ToString());
            }
        }
        return formattedValue!;
    }

    /// <summary>
    /// Custom implementation of the GetPreferredSize function.
    /// </summary>
    protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
    {
        return DataGridView == null 
            ? new Size(-1, -1) 
            : base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize);
    }
    #endregion

    #region Private
    private KryptonDataGridViewNumericUpDownEditingControl? EditingNumericUpDown => DataGridView!.EditingControl as KryptonDataGridViewNumericUpDownEditingControl;

    private decimal Constrain(decimal value)
    {
        if (value < _minimum)
        {
            value = _minimum;
        }

        if (value > _maximum)
        {
            value = _maximum;
        }

        return value;
    }

    private void OnCommonChange()
    {
        if (DataGridView is { IsDisposed: false, Disposing: false })
        {
            if (RowIndex == -1)
            {
                DataGridView.InvalidateColumn(ColumnIndex);
            }
            else
            {
                DataGridView.UpdateCellValue(ColumnIndex, RowIndex);
            }
        }
    }

    private bool OwnsEditingNumericUpDown(int rowIndex) =>
        rowIndex != -1 && DataGridView is { EditingControl: KryptonDataGridViewNumericUpDownEditingControl control }
                       && (rowIndex == ((IDataGridViewEditingControl)control).EditingControlRowIndex);

    #endregion

    #region Internal
    internal void SetAllowDecimals(int rowIndex, bool value)
    {
        _allowDecimals = value;
        if (OwnsEditingNumericUpDown(rowIndex))
        {
            EditingNumericUpDown!.AllowDecimals = value;
        }
    }

    internal void SetTrailingZeroes(int rowIndex, bool value)
    {
        _trailingZeroes = value;
        if (OwnsEditingNumericUpDown(rowIndex))
        {
            EditingNumericUpDown!.TrailingZeroes = value;
        }
    }

    internal void SetDecimalPlaces(int rowIndex, int value)
    {
        _decimalPlaces = value;
        if (OwnsEditingNumericUpDown(rowIndex))
        {
            EditingNumericUpDown!.DecimalPlaces = value;
        }
    }

    internal void SetHexadecimal(int rowIndex, bool value)
    {
        _hexadecimal = value;
        if (OwnsEditingNumericUpDown(rowIndex))
        {
            EditingNumericUpDown!.Hexadecimal = value;
        }
    }

    internal void SetIncrement(int rowIndex, decimal value)
    {
        _increment = value;
        if (OwnsEditingNumericUpDown(rowIndex))
        {
            EditingNumericUpDown!.Increment = value;
        }
    }

    internal void SetMaximum(int rowIndex, decimal value)
    {
        _maximum = value;
        if (_minimum > _maximum)
        {
            _minimum = _maximum;
        }

        var cellValue = GetValue(rowIndex);
        if (cellValue != null)
        {
            var currentValue = Convert.ToDecimal(cellValue);
            var constrainedValue = Constrain(currentValue);
            if (constrainedValue != currentValue)
            {
                SetValue(rowIndex, constrainedValue);
            }
        }

        if (OwnsEditingNumericUpDown(rowIndex))
        {
            EditingNumericUpDown!.Maximum = value;
        }
    }

    internal void SetMinimum(int rowIndex, decimal value)
    {
        _minimum = value;
        if (_minimum > _maximum)
        {
            _maximum = value;
        }

        var cellValue = GetValue(rowIndex);
        if (cellValue != null)
        {
            var currentValue = Convert.ToDecimal(cellValue);
            var constrainedValue = Constrain(currentValue);
            if (constrainedValue != currentValue)
            {
                SetValue(rowIndex, constrainedValue);
            }
        }

        if (OwnsEditingNumericUpDown(rowIndex))
        {
            EditingNumericUpDown!.Minimum = value;
        }
    }

    internal void SetThousandsSeparator(int rowIndex, bool value)
    {
        _thousandsSeparator = value;
        if (OwnsEditingNumericUpDown(rowIndex))
        {
            EditingNumericUpDown!.ThousandsSeparator = value;
        }
    }

    internal static HorizontalAlignment TranslateAlignment(DataGridViewContentAlignment align) => align switch
    {
        ANY_RIGHT => HorizontalAlignment.Right,
        ANY_CENTER => HorizontalAlignment.Center,
        _ => HorizontalAlignment.Left
    };

    /// <summary>
    /// Type casted version of OwningColumn
    /// </summary>
    internal KryptonDataGridViewNumericUpDownColumn? KryptonOwningColumn => OwningColumn as KryptonDataGridViewNumericUpDownColumn;
    #endregion
}