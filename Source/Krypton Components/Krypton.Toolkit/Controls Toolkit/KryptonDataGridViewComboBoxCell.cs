#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable MemberCanBeInternal

namespace Krypton.Toolkit
{
    /// <summary>
    /// Defines a KryptonComboBox cell type for the KryptonDataGridView control
    /// </summary>
    public class KryptonDataGridViewComboBoxCell : DataGridViewTextBoxCell
    {
        #region Static Fields
        [ThreadStatic]
        private static KryptonComboBox _paintingComboBox;
        private static readonly Type _defaultEditType = typeof(KryptonDataGridViewComboBoxEditingControl);
        private static readonly Type _defaultValueType = typeof(string);
        private static readonly Size _sizeLarge = new(10000, 10000);
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
        private object _dataSource;

        #endregion

        #region Identity
        /// <summary>
        /// Constructor for the KryptonDataGridViewComboBoxCell cell type
        /// </summary>
        public KryptonDataGridViewComboBoxCell()
        {
            // Create a thread specific KryptonComboBox control used for the painting of the non-edited cells
            if (_paintingComboBox == null)
            {
                _paintingComboBox = new KryptonComboBox();
                _paintingComboBox.SetLayoutDisplayPadding(new Padding(0, 1, 1, 0));
                _paintingComboBox.StateCommon.ComboBox.Border.Width = 0;
                _paintingComboBox.StateCommon.ComboBox.Border.Draw = InheritBool.False;
            }

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
            "KryptonDataGridViewComboBoxCell { ColumnIndex=" + ColumnIndex.ToString(CultureInfo.CurrentCulture) +
            ", RowIndex=" + RowIndex.ToString(CultureInfo.CurrentCulture) + " }";

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
        public ComboBox.ObjectCollection Items => _paintingComboBox.ComboBox.Items;

        /// <summary>
        /// Clones a DataGridViewComboBoxCell cell, copies all the custom properties.
        /// </summary>
        public override object Clone()
        {
            KryptonDataGridViewComboBoxCell dataGridViewCell = base.Clone() as KryptonDataGridViewComboBoxCell;
            if (dataGridViewCell != null)
            {
                dataGridViewCell.DropDownStyle = DropDownStyle;
                dataGridViewCell.DropDownHeight = DropDownHeight;
                dataGridViewCell.DropDownWidth = DropDownWidth;
                dataGridViewCell.MaxDropDownItems = MaxDropDownItems;
                dataGridViewCell.AutoCompleteMode = AutoCompleteMode;
                dataGridViewCell.AutoCompleteSource = AutoCompleteSource;
                dataGridViewCell.DisplayMember = DisplayMember;
                dataGridViewCell.ValueMember = ValueMember;
                dataGridViewCell.DataSource = DataSource;
            }
            return dataGridViewCell;
        }
        /// <summary>
        /// The DropDownStyle property replicates the one from the KryptonComboBox control
        /// </summary>
        [DefaultValue(0)]
        public ComboBoxStyle DropDownStyle
        {
            get => _dropDownStyle;

            set
            {
                if (value == ComboBoxStyle.Simple)
                {
                    throw new ArgumentOutOfRangeException(@"DropDownStyle", ComboBoxStyle.Simple, @"The DropDownStyle property does not support the Simple style.");
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
        public int MaxDropDownItems
        {
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
        public int DropDownHeight
        {
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
        public int DropDownWidth
        {
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
        [DefaultValue(121)]
        public AutoCompleteMode AutoCompleteMode
        {
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
        [DefaultValue(121)]
        public AutoCompleteSource AutoCompleteSource
        {
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
        public string DisplayMember
        {
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
        public string ValueMember
        {
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
        public object DataSource
        {
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
            DataGridView dataGridView = DataGridView;
            if (dataGridView?.EditingControl == null)
            {
                throw new InvalidOperationException(@"Cell is detached or its grid has no editing control.");
            }

            if (dataGridView.EditingControl is KryptonComboBox comboBox)
            {
                comboBox.DataSource = null;
            }

            base.DetachEditingControl();
        }

        /// <summary>
        /// Custom implementation of the InitializeEditingControl function. This function is called by the DataGridView control 
        /// at the beginning of an editing session. It makes sure that the properties of the KryptonNumericUpDown editing control are 
        /// set according to the cell properties.
        /// </summary>
        public override void InitializeEditingControl(int rowIndex,
            object initialFormattedValue,
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            if (DataGridView.EditingControl is KryptonComboBox comboBox)
            {
                if (OwningColumn is KryptonDataGridViewComboBoxColumn comboColumn)
                {
                    if (comboColumn.DataSource == null)
                    {
                        var strings = new object[comboColumn.Items.Count];

                        for (var i = 0; i < strings.Length; i++)
                        {
                            strings[i] = comboColumn.Items[i];
                        }

                        comboBox.Items.AddRange(strings);

                        var autoAppend = new string[comboColumn.AutoCompleteCustomSource.Count];
                        for (var j = 0; j < autoAppend.Length; j++)
                        {
                            autoAppend[j] = comboColumn.AutoCompleteCustomSource[j];
                        }

                        comboBox.AutoCompleteCustomSource.Clear();
                        comboBox.AutoCompleteCustomSource.AddRange(autoAppend);
                    }
                }

                comboBox.DropDownStyle = DropDownStyle;
                comboBox.DropDownHeight = DropDownHeight;
                comboBox.DropDownWidth = DropDownWidth;
                comboBox.MaxDropDownItems = MaxDropDownItems;
                comboBox.AutoCompleteSource = AutoCompleteSource;
                comboBox.AutoCompleteMode = AutoCompleteMode;
                comboBox.DisplayMember = DisplayMember;
                comboBox.ValueMember = ValueMember;
                comboBox.DataSource = DataSource;

                if (initialFormattedValue is not string initialFormattedValueStr)
                {
                    comboBox.Text = string.Empty;
                }
                else
                {
                    comboBox.Text = initialFormattedValueStr;
                }
            }
        }

        /// <summary>
        /// Custom implementation of the PositionEditingControl method called by the DataGridView control when it
        /// needs to relocate and/or resize the editing control.
        /// </summary>
        public override void PositionEditingControl(bool setLocation,
            bool setSize,
            Rectangle cellBounds,
            Rectangle cellClip,
            DataGridViewCellStyle cellStyle,
            bool singleVerticalBorderAdded,
            bool singleHorizontalBorderAdded,
            bool isFirstDisplayedColumn,
            bool isFirstDisplayedRow)
        {
            Rectangle editingControlBounds = PositionEditingPanel(cellBounds, cellClip, cellStyle,
                singleVerticalBorderAdded, singleHorizontalBorderAdded,
                isFirstDisplayedColumn, isFirstDisplayedRow);

            editingControlBounds = GetAdjustedEditingControlBounds(editingControlBounds, cellStyle);
            DataGridView.EditingControl.Location = new Point(editingControlBounds.X, editingControlBounds.Y);
            DataGridView.EditingControl.Size = new Size(editingControlBounds.Width, editingControlBounds.Height);
        }

        #endregion

        #region Protected
        /// <summary>
        /// Customized implementation of the GetErrorIconBounds function in order to draw the potential 
        /// error icon next to the up/down buttons and not on top of them.
        /// </summary>
        protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
        {
            const int BUTTONS_WIDTH = 16;

            Rectangle errorIconBounds = base.GetErrorIconBounds(graphics, cellStyle, rowIndex);
            if (DataGridView.RightToLeft == RightToLeft.Yes)
            {
                errorIconBounds.X = errorIconBounds.Left + BUTTONS_WIDTH;
            }
            else
            {
                errorIconBounds.X = errorIconBounds.Left - BUTTONS_WIDTH;
            }

            return errorIconBounds;
        }

        /// <summary>
        /// Custom implementation of the GetPreferredSize function.
        /// </summary>
        protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
        {
            if (DataGridView == null)
            {
                return new Size(-1, -1);
            }

            Size preferredSize = base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize);
            if (constraintSize.Width == 0)
            {
                const int BUTTONS_WIDTH = 16; // Account for the width of the up/down buttons.
                const int BUTTON_MARGIN = 8;  // Account for some blank pixels between the text and buttons.
                preferredSize.Width += BUTTONS_WIDTH + BUTTON_MARGIN;
            }

            return preferredSize;
        }
        #endregion

        #region Private

        private KryptonDataGridViewComboBoxEditingControl EditingComboBox => DataGridView.EditingControl as KryptonDataGridViewComboBoxEditingControl;

        private static Rectangle GetAdjustedEditingControlBounds(Rectangle editingControlBounds,
            DataGridViewCellStyle cellStyle)
        {
            // Adjust the vertical location of the editing control:
            var preferredHeight = _paintingComboBox.GetPreferredSize(_sizeLarge).Height + 2;
            if (preferredHeight < editingControlBounds.Height)
            {
                switch (cellStyle.Alignment)
                {
                    case DataGridViewContentAlignment.MiddleLeft:
                    case DataGridViewContentAlignment.MiddleCenter:
                    case DataGridViewContentAlignment.MiddleRight:
                        editingControlBounds.Y += (editingControlBounds.Height - preferredHeight) / 2;
                        break;
                    case DataGridViewContentAlignment.BottomLeft:
                    case DataGridViewContentAlignment.BottomCenter:
                    case DataGridViewContentAlignment.BottomRight:
                        editingControlBounds.Y += editingControlBounds.Height - preferredHeight;
                        break;
                }
            }

            return editingControlBounds;
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

        private bool OwnsEditingComboBox(int rowIndex) =>
            rowIndex != -1 
            && DataGridView is { EditingControl: KryptonDataGridViewComboBoxEditingControl control } 
            && (rowIndex == ((IDataGridViewEditingControl)control).EditingControlRowIndex);

        private static bool PartPainted(DataGridViewPaintParts paintParts, DataGridViewPaintParts paintPart) => (paintParts & paintPart) != 0;

        #endregion

        #region Internal
        internal void SetDropDownStyle(int rowIndex, ComboBoxStyle value)
        {
            _dropDownStyle = value;
            if (OwnsEditingComboBox(rowIndex))
            {
                EditingComboBox.DropDownStyle = value;
            }
        }

        internal void SetMaxDropDownItems(int rowIndex, int value)
        {
            _maxDropDownItems = value;
            if (OwnsEditingComboBox(rowIndex))
            {
                EditingComboBox.MaxDropDownItems = value;
            }
        }

        internal void SetDropDownHeight(int rowIndex, int value)
        {
            _dropDownHeight = value;
            if (OwnsEditingComboBox(rowIndex))
            {
                EditingComboBox.DropDownHeight = value;
            }
        }

        internal void SetDropDownWidth(int rowIndex, int value)
        {
            _dropDownWidth = value;
            if (OwnsEditingComboBox(rowIndex))
            {
                EditingComboBox.DropDownWidth = value;
            }
        }

        internal void SetAutoCompleteMode(int rowIndex, AutoCompleteMode value)
        {
            _autoCompleteMode = value;
            if (OwnsEditingComboBox(rowIndex))
            {
                EditingComboBox.AutoCompleteMode = value;
            }
        }

        internal void SetAutoCompleteSource(int rowIndex, AutoCompleteSource value)
        {
            _autoCompleteSource = value;
            if (OwnsEditingComboBox(rowIndex))
            {
                EditingComboBox.AutoCompleteSource = value;
            }
        }

        internal void SetDisplayMember(int rowIndex, string value)
        {
            _displayMember = value;
            if (OwnsEditingComboBox(rowIndex))
            {
                EditingComboBox.DisplayMember = value;
            }
        }

        internal void SetValueMember(int rowIndex, string value)
        {
            _valueMember = value;
            if (OwnsEditingComboBox(rowIndex))
            {
                EditingComboBox.ValueMember = value;
            }
        }

        internal void SetDataSource(int rowIndex, object value)
        {
            _dataSource = value;
            if (OwnsEditingComboBox(rowIndex))
            {
                EditingComboBox.DataSource = value;
            }
        }
        #endregion
    }
}

