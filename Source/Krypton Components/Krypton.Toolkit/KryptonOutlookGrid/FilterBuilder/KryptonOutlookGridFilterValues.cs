namespace Krypton.Toolkit
{
    //383, 23
    /// <summary>
    /// A UserControl that provides various input controls for filtering values based on data type and operator.
    /// It dynamically switches between text boxes, date pickers, and combo boxes.
    /// </summary>
    internal partial class KryptonOutlookGridFilterValues : UserControl
    {
        private KryptonTextBox TxtValue1 = new();
        private KryptonTextBox TxtValue2 = new();
        private KryptonLabel LblAnd = new();
        private KryptonDateTimePicker dtpValue1 = new();
        private KryptonDateTimePicker dtpValue2 = new();
        private KryptonComboBox commonValues = new();

        private bool _isTextBox = false;
        private bool _isDateBox = false;
        private bool _isComboBox = false;

        /// <summary>
        /// Occurs when the value of the filter control changes.
        /// </summary>
        public event EventHandler? ValueChanged;

        private object? _value1;
        /// <summary>
        /// Gets or sets the first filter value.
        /// The type of the value depends on the active control (TextBox, DateTimePicker, ComboBox).
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object? Value1
        {
            get
            {
                _value1 = null;
                if (_isTextBox)
                    _value1 = TxtValue1.Text;
                else if (_isDateBox)
                    _value1 = dtpValue1.Value;
                else if (_isComboBox)
                    _value1 = commonValues.SelectedValue;
                return _value1;
            }
            set
            {
                _value1 = value;
                if (_isTextBox)
                    TxtValue1.Text = value?.ToString() ?? string.Empty;
                else if (_isDateBox)
                    dtpValue1.Value = value is DateTime dateTime ? dateTime : DateTime.Now;
                else if (_isComboBox)
                    commonValues.SelectedValue = value;
            }
        }

        private object? _value2;
        /// <summary>
        /// Gets or sets the second filter value, used for "Between" or "Not Between" operations.
        /// The type of the value depends on the active control (TextBox, DateTimePicker).
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object? Value2
        {
            get
            {
                _value2 = null;
                if (_isTextBox)
                    _value2 = TxtValue2.Text;
                else if (_isDateBox)
                    _value2 = dtpValue2.Value;
                return _value2;
            }
            set
            {
                _value2 = value;
                if (_isTextBox)
                    TxtValue2.Text = value?.ToString() ?? string.Empty;
                else if (_isDateBox)
                    dtpValue2.Value = value is DateTime dateTime ? dateTime : DateTime.Now;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KryptonOutlookGridFilterValues"/> class.
        /// </summary>
        public KryptonOutlookGridFilterValues()
        {
            InitializeComponent();
            InitControls();
        }

        /// <summary>
        /// Updates the visibility and type of the input controls based on the specified data type,
        /// filter operator, and format (for dates).
        /// </summary>
        /// <param name="dataType">The data type of the column being filtered (e.g., "String", "Int32", "DateTime", "Boolean", "Image").</param>
        /// <param name="op">The filter operator as a string (e.g., "Equals", "Between").</param>
        /// <param name="format">An optional format string, primarily used for DateTime controls.</param>
        public void UpdateValueControls(string dataType, string op, string format)
        {
            SuspendLayout();

            KryptonOutlookGridFilterOperators Operator = op.ToEnum<KryptonOutlookGridFilterOperators>(); // Assumes ToEnum<T> extension method exists

            _isTextBox = false;
            _isDateBox = false;
            _isComboBox = false;
            LblAnd.Visible = false;
            TxtValue1.Visible = false;
            TxtValue2.Visible = false;
            dtpValue1.Visible = false;
            dtpValue2.Visible = false;
            commonValues.Visible = false;
            // For operators that don't require input values, hide all controls and return.
            switch (Operator)
            {
                case KryptonOutlookGridFilterOperators.IsEmpty or KryptonOutlookGridFilterOperators.IsNotEmpty or KryptonOutlookGridFilterOperators.IsNull or KryptonOutlookGridFilterOperators.IsNotNull:
                    return;
            }

            // Determine the actual Type from the dataType string
            var typ = Type.GetType($"System.{dataType}");
            if (typ == null && dataType.Equals(typeof(bool).Name, StringComparison.OrdinalIgnoreCase))
                typ = typeof(bool);
            else if (typ == null && dataType.Equals(typeof(Image).Name, StringComparison.OrdinalIgnoreCase))
                typ = typeof(Image);

            // Configure controls based on determined type
            if (typ == typeof(string) || typ.IsNumeric()) // Assumes IsNumeric() extension method exists
            {
                _isTextBox = true;
                InitTextBoxes(Operator);
            }
            else if (typ == typeof(DateTime))
            {
                _isDateBox = true;
                InitDateBoxes(Operator, format);
            }
            else if (typ == typeof(bool))
            {
                _isComboBox = true;
                InitBoolComboBox();
            }
            else if (typ == typeof(Image))
            {
                _isComboBox = true;
                InitImageComboBox();
            }
            else
            {
                // Default to text boxes if type is not recognized or handled
                InitTextBoxes(Operator);
            }
            ResumeLayout();
        }

        /// <summary>
        /// Initializes and displays the text box controls based on the filter operator.
        /// Shows two text boxes and an "And" label for "Between" operations.
        /// </summary>
        /// <param name="op">The current filter operator.</param>
        private void InitTextBoxes(KryptonOutlookGridFilterOperators op)
        {
            TpMain.Controls.Clear();
            TpMain.Controls.Add(TxtValue1, 0, 0);
            TxtValue1.Visible = true;
            if (op == KryptonOutlookGridFilterOperators.Between || op == KryptonOutlookGridFilterOperators.NotBetween)
            {
                TpMain.Controls.Add(LblAnd, 1, 0);
                TpMain.Controls.Add(TxtValue2, 2, 0);
                LblAnd.Visible = true;
                TxtValue2.Visible = true;
            }
        }

        /// <summary>
        /// Initializes and displays the date picker controls based on the filter operator and a specified format.
        /// Shows two date pickers and an "And" label for "Between" operations.
        /// </summary>
        /// <param name="op">The current filter operator.</param>
        /// <param name="format">The custom date format string for the date pickers (e.g., "dd/MM/yyyy").</param>
        private void InitDateBoxes(KryptonOutlookGridFilterOperators op, string format)
        {
            if (!string.IsNullOrEmpty(format))
            {
                dtpValue1.Format = DateTimePickerFormat.Custom;
                dtpValue1.CustomFormat = format;
                dtpValue2.Format = DateTimePickerFormat.Custom;
                dtpValue2.CustomFormat = format;
            }
            else
            {
                dtpValue1.Format = DateTimePickerFormat.Short;
                dtpValue2.Format = DateTimePickerFormat.Short;
            }
            TpMain.Controls.Clear();
            TpMain.Controls.Add(dtpValue1, 0, 0);
            dtpValue1.Visible = true;
            if (op == KryptonOutlookGridFilterOperators.Between || op == KryptonOutlookGridFilterOperators.NotBetween)
            {
                TpMain.Controls.Add(LblAnd, 1, 0);
                TpMain.Controls.Add(dtpValue2, 2, 0);
                LblAnd.Visible = true;
                dtpValue2.Visible = true;
            }
        }

        /// <summary>
        /// Initializes and displays a combo box for boolean values ("All", "Checked", "Uncheck").
        /// </summary>
        private void InitBoolComboBox()
        {
            Dictionary<object, object> items;
            items = new()
            {
                { "All", "All" },
                { "Checked", "Checked" },
                { "Uncheck", "Uncheck" }
            };
            TpMain.Controls.Clear();
            //commonValues.Items.Clear(); // This line is commented out in original, consider if it should be active.
            commonValues.DataSource = new BindingSource(items, null!);
            commonValues.ValueMember = "Key";
            commonValues.DisplayMember = "Value";
            TpMain.Controls.Add(commonValues, 0, 0);
            commonValues.Visible = true;
        }

        /// <summary>
        /// Initializes and displays a combo box for image presence ("All", "Has Image", "No Image").
        /// </summary>
        private void InitImageComboBox()
        {
            Dictionary<object, object> items;
            items = new()
            {
                { "All", "All" },
                { "Has Image", "Has Image" },
                { "No Image", "No Image" }
            };
            TpMain.Controls.Clear();
            //commonValues.Items.Clear(); // This line is commented out in original, consider if it should be active.
            commonValues.DataSource = new BindingSource(items, null!);
            commonValues.ValueMember = "Key";
            commonValues.DisplayMember = "Value";
            TpMain.Controls.Add(commonValues, 0, 0);
            commonValues.Visible = true;
        }

        /// <summary>
        /// Initializes the properties and event handlers for all internal controls (text boxes, labels, date pickers, combo box).
        /// </summary>
        private void InitControls()
        {
            //
            // TxtValue1
            //
            TxtValue1.CueHint.CueHintText = "Value 1";
            TxtValue1.Margin = new Padding(0);
            TxtValue1.MaxLength = 255;
            TxtValue1.Size = new Size(175, 23);
            TxtValue1.StateCommon.Content.Padding = new Padding(2, 2, 2, 3);
            TxtValue1.TabIndex = 0;
            TxtValue1.TextChanged += ControlValueChanged;
            //
            // TxtValue2
            //
            TxtValue2.CueHint.CueHintText = "Value 2";
            TxtValue2.Margin = new Padding(0);
            TxtValue2.MaxLength = 255;
            TxtValue2.Size = new Size(175, 23);
            TxtValue2.StateCommon.Content.Padding = new Padding(2, 2, 2, 3);
            TxtValue2.TabIndex = 2;
            TxtValue2.TextChanged += ControlValueChanged;
            //
            // LblAnd
            //
            LblAnd.Margin = new Padding(0);
            LblAnd.Size = new Size(33, 20);
            LblAnd.TabIndex = 1;
            LblAnd.Values.Text = "And";


            //
            // dtpValue1
            //
            dtpValue1.Margin = new Padding(0);
            dtpValue1.Size = new Size(175, 21);
            dtpValue1.TabIndex = 0;
            dtpValue1.ValueChanged += ControlValueChanged;
            //
            // dtpValue2
            //
            dtpValue2.Margin = new Padding(0);
            dtpValue2.Size = new Size(175, 21);
            dtpValue2.TabIndex = 0;
            dtpValue2.ValueChanged += ControlValueChanged;

            //
            // commonValues
            //
            commonValues.Size = new Size(175, 23);
            commonValues.DropDownWidth = commonValues.Width;
            commonValues.Margin = new Padding(0);
            commonValues.TabIndex = 0;
            commonValues.DropDownStyle = ComboBoxStyle.DropDownList;
            commonValues.SelectedValueChanged += ControlValueChanged;
        }

        /// <summary>
        /// Handles the ValueChanged event of the internal controls and re-raises it as the ValueChanged event of this control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        private void ControlValueChanged(object? sender, EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }
    }
}