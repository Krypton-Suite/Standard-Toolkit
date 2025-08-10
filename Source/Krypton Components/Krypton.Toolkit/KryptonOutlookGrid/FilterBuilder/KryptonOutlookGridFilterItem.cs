namespace Krypton.Toolkit
{

    internal partial class KryptonOutlookGridFilterItem : UserControl, IKryptonOutlookGridFilterItem
    {

        #region Enums

        public enum FieldTypes
        {
            NumberExpression,
            DateExpression,
            StringExpression,
            DropDownExpression
        }

        #endregion Enums

        #region Delegates And Events

        public event KryptonOutlookGridFilterSelectedAndOrEventHandler? SelectedAndOr;
        public event KryptonOutlookGridFilterSelectedEndEventHandler? SelectedEnd;
        public event KryptonOutlookGridFilterSelectedDeleteEventHandler? SelectedDelete;
        public event KryptonOutlookGridFilterSelectedInsertEventHandler? SelectedInsert;
        public event KryptonOutlookGridFilterSelectedMakeSubgroupEventHandler? SelectedMakeSubgroup;
        public event KryptonOutlookGridFilterFilterChangedEventHandler? FilterChanged;

        #endregion Delegates And Events

        #region Private Variables

        private KryptonOutlookGridFilterField _fieldValue = null!;
        private List<KryptonOutlookGridFilterSourceColumn> _columns = null!;
        private string _dataType = string.Empty;

        private string _filter = string.Empty;
        private string _readableFilter = string.Empty;

        #endregion Private Variables

        #region Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<KryptonOutlookGridFilterSourceColumn> Columns
        {
            get { return (List<KryptonOutlookGridFilterSourceColumn>)ColumnsList.DataSource!; }
            set
            {
                _columns = value;
                ColumnsList.DataSource = _columns;
                ColumnsList.DisplayMember = nameof(KryptonOutlookGridFilterSourceColumn.Name);
                ColumnsList.ValueMember = nameof(KryptonOutlookGridFilterSourceColumn.Alias);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public KryptonOutlookGridFilterField FieldValue
        {
            get
            {
                GetValues();
                return _fieldValue;
            }
            set
            {
                _fieldValue = value;
                SetValues();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Filter
        {
            get
            {
                SetFilterString();
                return _filter;
            }
        }

        /// <summary>
        ///  A human readable filter string that represents the actual filter string
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ReadableFilter
        {
            get
            {
                SetFilterString();
                return _readableFilter;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public KryptonOutlookGridFilterItemMenuButton.Items SelectedMenuItem
        {
            get
            {
                return FilterMenu.Item;
            }
            set
            {
                FilterMenu.Item = value;
            }
        }

        /// <summary>
        ///   The boolean conjunction to follow the filter
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Conjunction
        {
            get
            {
                if (this.SelectedMenuItem == KryptonOutlookGridFilterItemMenuButton.Items.AndItem)
                {
                    return "AND";
                }
                else if (this.SelectedMenuItem == KryptonOutlookGridFilterItemMenuButton.Items.OrItem)
                {
                    return "OR";
                }
                else
                {
                    return "";
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string IntConjunction
        {
            get
            {
                if (this.SelectedMenuItem == KryptonOutlookGridFilterItemMenuButton.Items.AndItem)
                {
                    return Convert.ToString((int)KryptonOutlookGridFilterItemMenuButton.Items.AndItem);
                }
                else if (this.SelectedMenuItem == KryptonOutlookGridFilterItemMenuButton.Items.OrItem)
                {
                    return Convert.ToString((int)KryptonOutlookGridFilterItemMenuButton.Items.OrItem);
                }
                else
                {
                    return "";
                }
            }
        }

        #endregion Properties

        #region Constructors

        public KryptonOutlookGridFilterItem()
        {
            InitializeComponent();
            Margin = new Padding(0, 0, 0, 0);
            FilterMenu.Height = filterValues1.Height;// TxtValue1.Height;
            FilterMenu.Menu.Opening += FilterMenu_Opening;
            FilterMenu.SelectionChanged += FilterMenu_SelectionChanged;
            FilterMenu.SelectionChanging += FilterMenu_SelectionChanging;
            filterValues1.ValueChanged += FilterValues1_ValueChanged;
            this.SelectedMenuItem = KryptonOutlookGridFilterItemMenuButton.Items.EndItem;
        }

        public KryptonOutlookGridFilterItem(List<KryptonOutlookGridFilterSourceColumn> columns) : this()
        {
            Columns = columns;
            if (columns.Count == 1)
                ColumnsList.Visible = false;
        }

        public KryptonOutlookGridFilterItem(List<KryptonOutlookGridFilterSourceColumn> columns, KryptonOutlookGridFilterField field) : this(columns)
        {
            _fieldValue = field;
            SetValues();
        }

        #endregion Constructors

        #region Control Events

        private void ColumnsList_SelectedValueChanged(object sender, EventArgs e)
        {
            KryptonComboBox cb = (KryptonComboBox)sender;
            try
            {
                KryptonOutlookGridFilterSourceColumn? columnItem = (KryptonOutlookGridFilterSourceColumn?)cb.SelectedItem;
                if (columnItem != null)
                {
                    _dataType = columnItem.DataType;
                    UpdateOperatorComboBox(columnItem.DataType);
                    filterValues1.UpdateValueControls(columnItem.DataType, OperatorsList.SelectedItem.ToStringNull(), columnItem.Format);
                }
                FilterChanged?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception) { }
        }

        private void OperatorsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            KryptonComboBox cb = (KryptonComboBox)sender;
            try
            {
                KryptonOutlookGridFilterOperators Operator = cb.SelectedItem.ToStringNull().ToEnum<KryptonOutlookGridFilterOperators>();
                KryptonOutlookGridFilterSourceColumn columnItem = (KryptonOutlookGridFilterSourceColumn?)ColumnsList.SelectedItem!;
                filterValues1.UpdateValueControls(columnItem.DataType, Operator.ToString(), columnItem.Format);
                FilterChanged?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception) { }
        }

        private void FilterValues1_ValueChanged(object? sender, EventArgs e)
        {
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void TxtValue1_TextChanged(object sender, EventArgs e)
        {
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void TxtValue2_TextChanged(object sender, EventArgs e)
        {
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterMenu_Opening(object? sender, CancelEventArgs e)
        {
            if (!IsValid())
                e.Cancel = true;
        }

        private void FilterMenu_SelectionChanged(object sender, KryptonOutlookGridFilterMenuButtonSelectionChangedEventArgs e)
        {
            switch (e.NewSelectedIndex)
            {
                case (int)KryptonOutlookGridFilterItemMenuButton.Items.AndItem:
                case (int)KryptonOutlookGridFilterItemMenuButton.Items.OrItem:
                    SelectedAndOr?.Invoke(this, e);
                    break;

                case (int)KryptonOutlookGridFilterItemMenuButton.Items.EndItem:
                    SelectedEnd?.Invoke(this, e);
                    break;
            }
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterMenu_SelectionChanging(object sender, KryptonOutlookGridFilterMenuButtonSelectionChangingEventArgs e)
        {
            switch (e.NewSelectedIndex)
            {
                case (int)KryptonOutlookGridFilterItemMenuButton.Items.Delete:
                    e.Cancel = true;
                    SelectedDelete?.Invoke(this, e);
                    break;

                case (int)KryptonOutlookGridFilterItemMenuButton.Items.Insert:
                    e.Cancel = true;
                    SelectedInsert?.Invoke(this, e);
                    break;

                case (int)KryptonOutlookGridFilterItemMenuButton.Items.MakeSubgroup:
                    e.Cancel = true;
                    SelectedMakeSubgroup?.Invoke(this, e);
                    break;
            }
        }

        #endregion Control Events

        #region Private Methods

        private void SetValues()
        {
            ColumnsList.Text = _fieldValue.ColumnName;
            OperatorsList.Text = _fieldValue.ReadableOperator;
            filterValues1.Value1 = _fieldValue.Value1;
            filterValues1.Value2 = _fieldValue.Value2;
            SelectedMenuItem = (KryptonOutlookGridFilterItemMenuButton.Items)_fieldValue.ColumnConjunctionItem;
            FilterMenu.Item = (KryptonOutlookGridFilterItemMenuButton.Items)_fieldValue.ColumnConjunctionItem;
            FilterMenu.Text = SelectedMenuItem.GetDescription();
            _dataType = _fieldValue.DataType;
        }

        private void GetValues()
        {
            if (!IsValid(false))
            {
                _fieldValue = null!;
                return;
            }
            KryptonOutlookGridFilterSourceColumn fieldItem = (KryptonOutlookGridFilterSourceColumn)ColumnsList.SelectedItem!;
            string op = OperatorsList.SelectedItem.ToStringNull();
            var opEnum = op.ToEnum<KryptonOutlookGridFilterOperators>();
            var value1 = filterValues1.Value1.ToStringNull();
            var value2 = filterValues1.Value2.ToStringNull();
            if (fieldItem.DataType.Equals(typeof(bool).Name, StringComparison.OrdinalIgnoreCase))
            {
                if (value1.Equals("ALL", StringComparison.OrdinalIgnoreCase))
                {
                    _fieldValue = null!;
                    return;
                }
                if (value1.Equals("CHECKED", StringComparison.OrdinalIgnoreCase))
                    value1 = true.ToString();
                else if (value1.Equals("Uncheck", StringComparison.OrdinalIgnoreCase))
                    value1 = false.ToString();
            }
            else if (fieldItem.DataType.Equals(typeof(Image).Name, StringComparison.OrdinalIgnoreCase))
            {
                if (value1.Equals("ALL", StringComparison.OrdinalIgnoreCase))
                {
                    _fieldValue = null!;
                    return;
                }
                if (value1.Equals("HAS IMAGE", StringComparison.OrdinalIgnoreCase))
                {
                    opEnum = KryptonOutlookGridFilterOperators.IsNotNull;
                    value1 = string.Empty;
                }
                else if (value1.Equals("NO IMAGE", StringComparison.OrdinalIgnoreCase))
                {
                    opEnum = KryptonOutlookGridFilterOperators.IsNull;
                    value1 = false.ToString();
                }
            }

            _fieldValue = new()
            {
                DataType = _dataType,
                Operator = opEnum.GetDescription(),
                ReadableOperator = op,
                Value1 = value1,
                Value2 = value2,
                ColumnConjunction = SelectedMenuItem == KryptonOutlookGridFilterItemMenuButton.Items.AndItem || SelectedMenuItem == KryptonOutlookGridFilterItemMenuButton.Items.OrItem ? SelectedMenuItem.GetDescription() : string.Empty,
                ColumnConjunctionItem = (int)SelectedMenuItem,
                Filter = _filter,
                ColumnName = fieldItem.Name
            };
        }

        private void UpdateOperatorComboBox(string dataType)
        {
            var typ = Type.GetType($"System.{dataType}");
            if (typ == null && dataType.Equals(typeof(bool).Name, StringComparison.OrdinalIgnoreCase))
                typ = typeof(bool);
            else if (typ == null && dataType.Equals(typeof(Image).Name, StringComparison.OrdinalIgnoreCase))
                typ = typeof(Image);

            List<KryptonOutlookGridFilterOperators> operators;
            if (typ == typeof(string))
            {
                operators =
                [
                    KryptonOutlookGridFilterOperators.Equals,
                    KryptonOutlookGridFilterOperators.NotEquals,
                    KryptonOutlookGridFilterOperators.BeginsWith,
                    KryptonOutlookGridFilterOperators.NotBeginsWith,
                    KryptonOutlookGridFilterOperators.Contains,
                    KryptonOutlookGridFilterOperators.NotContains,
                    KryptonOutlookGridFilterOperators.EndsWith,
                    KryptonOutlookGridFilterOperators.NotEndsWith,
                    KryptonOutlookGridFilterOperators.IsEmpty,
                    KryptonOutlookGridFilterOperators.IsNotEmpty,
                    KryptonOutlookGridFilterOperators.IsNull,
                    KryptonOutlookGridFilterOperators.IsNotNull
                ];
            }
            else if (typ == typeof(DateTime) ||
                typ.IsNumeric())
            {
                operators =
                [
                    KryptonOutlookGridFilterOperators.Equals,
                    KryptonOutlookGridFilterOperators.NotEquals,
                    KryptonOutlookGridFilterOperators.LessThan,
                    KryptonOutlookGridFilterOperators.LessThanOrEqual,
                    KryptonOutlookGridFilterOperators.GreaterThan,
                    KryptonOutlookGridFilterOperators.GreaterThanOrEqual,
                    KryptonOutlookGridFilterOperators.Between,
                    KryptonOutlookGridFilterOperators.NotBetween,
                    KryptonOutlookGridFilterOperators.IsNull,
                    KryptonOutlookGridFilterOperators.IsNotNull
                ];
                /*if (typ != typeof(DateTime))
                {
                    operators.Add(FilterOperators.In);
                    operators.Add(FilterOperators.NotIn);
                }*/
            }
            else if (typ == typeof(bool))
            {
                operators =
                [
                    KryptonOutlookGridFilterOperators.Equals
                ];
            }
            else if (typ == typeof(Image))
            {
                operators =
                [
                    KryptonOutlookGridFilterOperators.Equals
                ];
            }
            else
            {
                operators =
                [
                    KryptonOutlookGridFilterOperators.Equals,
                    KryptonOutlookGridFilterOperators.NotEquals,
                    KryptonOutlookGridFilterOperators.IsNull,
                    KryptonOutlookGridFilterOperators.IsNotNull
                ];
            }

            OperatorsList.DataSource = operators;
        }

        private void SetFilterString()
        {
            try
            {
                KryptonOutlookGridFilterSourceColumn fieldItem = (KryptonOutlookGridFilterSourceColumn)ColumnsList.SelectedItem!;

                string op = OperatorsList.SelectedItem.ToStringNull();
                if (string.IsNullOrEmpty(op))
                    op = KryptonOutlookGridFilterOperators.Equals.ToString();

                string colName = fieldItem == null || fieldItem.Alias != ColumnsList.Text ? ColumnsList.Text : fieldItem != null ? fieldItem.Alias : string.Empty;
                _dataType = fieldItem == null ? _dataType : fieldItem.DataType;

                var value1 = filterValues1.Value1.ToStringNull();
                var value2 = filterValues1.Value2.ToStringNull();
                _readableFilter = HelperExtensions.GetReadableFilterString(string.Empty, colName, _dataType, op, value1, value2, true);
                if (IsValid(false))
                    _filter = HelperExtensions.GetFilterString(string.Empty, colName, _dataType, op, value1, value2);
                else
                    _filter = "";
            }
            catch (Exception)
            {
                _filter = "";
            }
        }

        private bool IsValid(bool showMsg = true)
        {
            KryptonOutlookGridFilterSourceColumn fieldItem = (KryptonOutlookGridFilterSourceColumn)ColumnsList.SelectedItem!;

            string colName = fieldItem.Name;

            if (string.IsNullOrEmpty(colName))
            {
                if (showMsg)
                {
                    KryptonMessageBox.Show("Select a column", "Validation Error");
                    ColumnsList.Focus();
                }
                return false;
            }
            string op = OperatorsList.SelectedItem.ToStringNull();
            if (string.IsNullOrEmpty(op))
            {
                if (showMsg)
                {
                    KryptonMessageBox.Show("Select an operator", "Validation Error");
                    OperatorsList.Focus();
                }
                return false;
            }
            var opEnum = op.ToEnum<KryptonOutlookGridFilterOperators>();
            switch (opEnum)
            {
                case KryptonOutlookGridFilterOperators.IsEmpty:
                case KryptonOutlookGridFilterOperators.IsNotEmpty:
                case KryptonOutlookGridFilterOperators.IsNull:
                case KryptonOutlookGridFilterOperators.IsNotNull:
                    break;
                default:

                    KryptonOutlookGridFilterSourceColumn columnItem = (KryptonOutlookGridFilterSourceColumn?)ColumnsList.SelectedItem!;
                    if (opEnum == KryptonOutlookGridFilterOperators.Equals && columnItem.DataType.Equals(typeof(bool).Name, StringComparison.OrdinalIgnoreCase))
                        return true;
                    else if (opEnum == KryptonOutlookGridFilterOperators.Equals && columnItem.DataType.Equals(typeof(Image).Name, StringComparison.OrdinalIgnoreCase))
                        return true;

                    if (filterValues1.Value1.ToStringNull().Trim().Length == 0)
                    {
                        if (showMsg)
                        {
                            KryptonMessageBox.Show("Enter a value", "Validation Error");
                            //TxtValue1.Focus();
                        }
                        return false;
                    }
                    break;
            }

            if (opEnum == KryptonOutlookGridFilterOperators.Between || opEnum == KryptonOutlookGridFilterOperators.NotBetween)
            {
                if (filterValues1.Value2.ToStringNull().Trim().Length == 0)
                {
                    if (showMsg)
                    {
                        KryptonMessageBox.Show("Enter a value2", "Validation Error");
                        //TxtValue2.Focus();
                    }
                    return false;
                }
            }

            return true;
        }

        #endregion Private Methods

    }
}
