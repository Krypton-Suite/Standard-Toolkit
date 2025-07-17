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

namespace Krypton.Toolkit;

/// <summary>
/// Defines a KryptonComboBox cell type for the KryptonDataGridView control
/// </summary>
public class KryptonDataGridViewComboBoxCell : DataGridViewTextBoxCell
{
    #region Static Fields
    [ThreadStatic]
    private static readonly Type _defaultEditType = typeof(KryptonDataGridViewComboBoxEditingControl);
    private static readonly Type _defaultValueType = typeof(string);
    private static readonly Size _sizeLarge = new Size(10000, 10000);
    #endregion

    #region Instance Fields
    private ComboBoxStyle _dropDownStyle;
    private int _maxDropDownItems;
    private int _dropDownHeight;
    private int _dropDownWidth;
    private AutoCompleteMode _autoCompleteMode;
    private AutoCompleteSource _autoCompleteSource;
    private string _displayMember;
    private string _valueMember;
    private object? _dataSource;
    private List<object> _items;
    private string _selectedItemText;
    private bool _initialSelectedTextSet;
    #endregion

    #region Identity
    /// <summary>
    /// Constructor for the KryptonDataGridViewComboBoxCell cell type
    /// </summary>
    public KryptonDataGridViewComboBoxCell()
    {
        _items = [];
        _selectedItemText = string.Empty;
        _initialSelectedTextSet = false;

        _dropDownStyle = ComboBoxStyle.DropDown;
        _maxDropDownItems = 8;
        _dropDownHeight = 200;
        _dropDownWidth = 121;
        _autoCompleteMode = AutoCompleteMode.None;
        _autoCompleteSource = AutoCompleteSource.None;
        _displayMember = string.Empty;
        _valueMember = string.Empty;
    }

    /// <summary>
    /// Returns a standard textual representation of the cell.
    /// </summary>
    public override string ToString() =>
        $"KryptonDataGridViewComboBoxCell {{ ColumnIndex={ColumnIndex.ToString(CultureInfo.CurrentCulture)}, RowIndex={RowIndex.ToString(CultureInfo.CurrentCulture)} }}";

    #endregion

    #region Public
    /// <summary>
    /// Define the type of the cell's editing control
    /// </summary>
    public override Type EditType => _defaultEditType;

    /// <summary>
    /// Returns the type of the cell's Value property
    /// </summary>
    public override Type ValueType => base.ValueType ?? _defaultValueType;

    /// <summary>Gets the items in the combobox.</summary>
    /// <value>The items.</value>
    public List<object> Items => _items;

    /// <summary>
    /// Clones a DataGridViewComboBoxCell cell, copies all the custom properties.
    /// </summary>
    public override object Clone()
    {
        var dataGridViewCell = base.Clone() as KryptonDataGridViewComboBoxCell ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("dataGridViewCell"));

        dataGridViewCell.DropDownStyle = DropDownStyle;
        dataGridViewCell.DropDownHeight = DropDownHeight;
        dataGridViewCell.DropDownWidth = DropDownWidth;
        dataGridViewCell.MaxDropDownItems = MaxDropDownItems;
        dataGridViewCell.AutoCompleteMode = AutoCompleteMode;
        dataGridViewCell.AutoCompleteSource = AutoCompleteSource;
        dataGridViewCell.DisplayMember = DisplayMember;
        dataGridViewCell.ValueMember = ValueMember;
        dataGridViewCell.DataSource = DataSource;

        return dataGridViewCell!;
    }
    /// <summary>
    /// The DropDownStyle property replicates the one from the KryptonComboBox control
    /// </summary>
    [DefaultValue(0)]
    public ComboBoxStyle DropDownStyle {
        get => _dropDownStyle;

        set
        {
            if (value == ComboBoxStyle.Simple)
            {
                throw new ArgumentOutOfRangeException(nameof(DropDownStyle), ComboBoxStyle.Simple, @"The DropDownStyle property does not support the Simple style.");
            }

            if (_dropDownStyle != value)
            {
                SetDropDownStyle(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The MaxDropDownItems property replicates the one from the KryptonComboBox control
    /// </summary>
    [DefaultValue(8)]
    public int MaxDropDownItems {
        get => _maxDropDownItems;

        set
        {
            if (_maxDropDownItems != value)
            {
                SetMaxDropDownItems(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The DropDownHeight property replicates the one from the KryptonComboBox control
    /// </summary>
    [DefaultValue(200)]
    public int DropDownHeight {
        get => _dropDownHeight;

        set
        {
            if (_dropDownHeight != value)
            {
                SetDropDownHeight(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The DropDownWidth property replicates the one from the KryptonComboBox control
    /// </summary>
    [DefaultValue(121)]
    public int DropDownWidth {
        get => _dropDownWidth;

        set
        {
            if (DropDownWidth != value)
            {
                SetDropDownWidth(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The AutoCompleteMode property replicates the one from the KryptonComboBox control
    /// </summary>
    [DefaultValue(AutoCompleteMode.None)]
    public AutoCompleteMode AutoCompleteMode {
        get => _autoCompleteMode;

        set
        {
            if (AutoCompleteMode != value)
            {
                SetAutoCompleteMode(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The AutoCompleteSource property replicates the one from the KryptonComboBox control
    /// </summary>
    [DefaultValue(AutoCompleteSource.None)]
    public AutoCompleteSource AutoCompleteSource {
        get => _autoCompleteSource;

        set
        {
            if (AutoCompleteSource != value)
            {
                SetAutoCompleteSource(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The DisplayMember property replicates the one from the KryptonComboBox control
    /// </summary>
    [DefaultValue(@"")]
    public string DisplayMember {
        get => _displayMember;

        set
        {
            if (_displayMember != value)
            {
                SetDisplayMember(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The ValueMember property replicates the one from the KryptonComboBox control
    /// </summary>
    [DefaultValue(@"")]
    public string ValueMember {
        get => _valueMember;

        set
        {
            if (_valueMember != value)
            {
                SetValueMember(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// Gets and sets the list that this control will use to gets its items.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates the list that this control will use to gets its items.")]
    [AttributeProvider(typeof(IListSource))]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(null)]
    public object? DataSource {
        get => _dataSource;
        set
        {
            if (_dataSource != value)
            {
                SetDataSource(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// DetachEditingControl gets called by the DataGridView control when the editing session is ending
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public override void DetachEditingControl()
    {
        DataGridView? dataGridView = DataGridView;
        switch (dataGridView?.EditingControl)
        {
            case null:
                throw new InvalidOperationException(@"Cell is detached or its grid has no editing control.");
        }

        if (dataGridView.EditingControl is KryptonComboBox comboBox)
        {
            _selectedItemText = comboBox.Text;
            comboBox.DataSource = null;
        }
        else
        {
            _selectedItemText = string.Empty;
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

        if (DataGridView!.EditingControl is KryptonComboBox comboBox)
        {
            if (KryptonOwningColumn is not null && KryptonOwningColumn.DataSource is null)
            {
                var strings = new object[KryptonOwningColumn.Items.Count];

                for (var i = 0; i < strings.Length; i++)
                {
                    strings[i] = KryptonOwningColumn.Items[i];
                }
                comboBox.Items.Clear();
                comboBox.Items.AddRange(strings);

                var autoAppend = new string[KryptonOwningColumn.AutoCompleteCustomSource.Count];
                for (var j = 0; j < autoAppend.Length; j++)
                {
                    autoAppend[j] = KryptonOwningColumn.AutoCompleteCustomSource[j];
                }

                comboBox.AutoCompleteCustomSource.Clear();
                comboBox.AutoCompleteCustomSource.AddRange(autoAppend);
            }

            comboBox.DropDownStyle = DropDownStyle;
            comboBox.DropDownHeight = DropDownHeight;
            comboBox.DropDownWidth = DropDownWidth;
            comboBox.MaxDropDownItems = MaxDropDownItems;
            comboBox.AutoCompleteSource = AutoCompleteSource;
            comboBox.AutoCompleteMode = AutoCompleteMode;
            comboBox.DisplayMember = DisplayMember;
            comboBox.ValueMember = ValueMember;
            comboBox.DataSource = KryptonOwningColumn?.DataSource;

            // Restore the state, if needed.
            if (!(Value is DBNull || Value is null))
            {
                if (KryptonOwningColumn is not null
                    && KryptonOwningColumn.DataSource is not null
                    && ValueMember.Length > 0)
                {
                    comboBox.SelectedValue = Value;
                }
                else if (comboBox.Items.Count > 0)
                {
                    comboBox.SelectedIndex = comboBox.Items.IndexOf(Value.ToString());
                }
            }

            _selectedItemText = comboBox.Text;
        }
    }
    #endregion

    #region Protected

    ///<inheritdoc/>
    protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object? value,
        object? formattedValue, string? errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
    {
        if (!_initialSelectedTextSet)
        {
            _initialSelectedTextSet = SetInitialSelectedItemText(rowIndex);
        };

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
            string text;
            if (ErrorText.Length == 0)
            {
                graphics.DrawImage(image, new Point(pos, textArea.Top));
                text = _selectedItemText;
            }
            else
            {
                text = ErrorText;
            }

            // Cell display text
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

    private KryptonDataGridViewComboBoxEditingControl? EditingComboBox => DataGridView!.EditingControl as KryptonDataGridViewComboBoxEditingControl;

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

    private bool OwnsEditingComboBox(int rowIndex) =>
        rowIndex != -1
        && DataGridView is { EditingControl: KryptonDataGridViewComboBoxEditingControl control }
        && (rowIndex == ((IDataGridViewEditingControl)control).EditingControlRowIndex);
    #endregion

    #region Internal
    internal void SetDropDownStyle(int rowIndex, ComboBoxStyle value)
    {
        _dropDownStyle = value;
        if (OwnsEditingComboBox(rowIndex))
        {
            EditingComboBox!.DropDownStyle = value;
        }
    }

    internal void SetMaxDropDownItems(int rowIndex, int value)
    {
        _maxDropDownItems = value;
        if (OwnsEditingComboBox(rowIndex))
        {
            EditingComboBox!.MaxDropDownItems = value;
        }
    }

    internal void SetDropDownHeight(int rowIndex, int value)
    {
        _dropDownHeight = value;
        if (OwnsEditingComboBox(rowIndex))
        {
            EditingComboBox!.DropDownHeight = value;
        }
    }

    internal void SetDropDownWidth(int rowIndex, int value)
    {
        _dropDownWidth = value;
        if (OwnsEditingComboBox(rowIndex))
        {
            EditingComboBox!.DropDownWidth = value;
        }
    }

    internal void SetAutoCompleteMode(int rowIndex, AutoCompleteMode value)
    {
        _autoCompleteMode = value;
        if (OwnsEditingComboBox(rowIndex))
        {
            EditingComboBox!.AutoCompleteMode = value;
        }
    }

    internal void SetAutoCompleteSource(int rowIndex, AutoCompleteSource value)
    {
        _autoCompleteSource = value;
        if (OwnsEditingComboBox(rowIndex))
        {
            EditingComboBox!.AutoCompleteSource = value;
        }
    }

    internal void SetDisplayMember(int rowIndex, string value)
    {
        _displayMember = value;
        if (OwnsEditingComboBox(rowIndex))
        {
            EditingComboBox!.DisplayMember = value;
        }
    }

    internal void SetValueMember(int rowIndex, string value)
    {
        _valueMember = value;
        if (OwnsEditingComboBox(rowIndex))
        {
            EditingComboBox!.ValueMember = value;
        }
    }

    internal void SetDataSource(int rowIndex, object? value)
    {
        // Force a reread of the cell value and paint when the datasource changes
        ResetInitialSelectedItemText();

        _dataSource = value;
        if (OwnsEditingComboBox(rowIndex))
        {
            EditingComboBox!.DataSource = value;
        }
    }

    /// <summary>
    /// Type casted version of OwningColumn
    /// </summary>
    internal KryptonDataGridViewComboBoxColumn? KryptonOwningColumn => OwningColumn as KryptonDataGridViewComboBoxColumn;
    #endregion

    /// <summary>
    /// Resets the initial selected item text.
    /// </summary>
    internal void ResetInitialSelectedItemText()
    {
        _selectedItemText = string.Empty;
        _initialSelectedTextSet = false;
    }

    /// <summary>
    /// Sets the initial item text fetch from the cell value.<br/>
    /// If the cell has a datasource connected this routine retrieves the value that should be shown in the cell. Which usually is the DisplayMember value.<br/>
    /// If the items for the combo are supplied via the Items property, the cell value is checked to exist in the list for input consistency.
    /// </summary>
    /// <param name="rowIndex"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    internal bool SetInitialSelectedItemText(int rowIndex)
    {
        var value = GetValue(rowIndex);
        bool result = true;
        var dataSource = KryptonOwningColumn?.DataSource;

        // Documentation describing the behaviour for DisplayMember and ValueMember
        // https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.datagridviewcomboboxcolumn?view=windowsdesktop-9.0

        if (value is not (DBNull or null)
            && dataSource is not null
            && ValueMember.Length > 0)
        {
            BindingMemberInfo bindingMemberInfo = new(ValueMember);

            if (DataGridView is not null
                && DataGridView.BindingContext is not null
                && DataGridView.BindingContext[dataSource, bindingMemberInfo.BindingPath] is CurrencyManager currencyManager
                && currencyManager.List is IBindingList bindinglist
                && bindinglist.SupportsSearching)
            {
                if (currencyManager.GetItemProperties().Find(bindingMemberInfo.BindingField, true) is PropertyDescriptor propertyDescriptor)
                {
                    // Define the field used for displaymember.
                    // If DisplayMember is not set, Valuemember will be used for both.
                    var displayMember = DisplayMember.Length > 0
                        ? DisplayMember
                        : ValueMember;

                    // Find the index of the row that contains propertyDescriptor having value.
                    // The search stops on the first occurrence.
                    int index = bindinglist.Find(propertyDescriptor, value);

                    if (index != -1
                        && currencyManager.List[index] is System.Data.DataRowView dataRowView)
                    {
                        try
                        {
                            _selectedItemText = dataRowView[displayMember].ToString() ?? string.Empty;
                        }
                        catch
                        {
                            // Member is an unknow column
                            throw new ArgumentException($"The field '{displayMember}' specified as '{(DisplayMember.Length > 0 ? "DisplayMember" : "ValueMember")}' has not been found in the data source.");
                        }
                    }
                    else
                    {
                        // The row containing the given value was not found.
                        // Meaning there's a value mismatch between the combobox datasource and the cell value.
                        throw new ArgumentException($"The property descriptor '{propertyDescriptor.DisplayName}' having value '{value}' was not found in the data source.");
                    }
                }
                else
                {
                    throw new ArgumentException($"The field '{ValueMember}' specified as ValueMember has not been found in the data source.");
                }
            }
            else
            {
                _selectedItemText = string.Empty;
            }
        }
        else if (value is not (DBNull or null)
                 && dataSource is null
                 && KryptonOwningColumn?.Items.Count > 0)
        {
            // DataSource is null and Items is populated
            var valueStr = value.ToString();

            if (valueStr is not null
                && KryptonOwningColumn.Items.IndexOf(valueStr) == -1)
            {
                // The value was not found in the list
                throw new ArgumentException($"The cell value {valueStr} was not found in the list with drop-down items.");
            }

            _selectedItemText = valueStr ?? string.Empty;
        }
        else
        {
            _selectedItemText = value?.ToString() ?? string.Empty;
            result = false;
        }

        return result;
    }
}