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


namespace Krypton.Toolkit
{
    /// <summary>
    /// Displays editable text information in a KryptonDataGridView control.
    /// </summary>
    public class KryptonDataGridViewTextBoxCell : DataGridViewTextBoxCell, IIconCell
    {
        #region Instance Fields
        private bool _multiline;
        private bool _multilineStringEditor;
        #endregion

        #region Static Fields
        private static readonly Type _defaultEditType = typeof(KryptonDataGridViewTextBoxEditingControl);
        private static readonly Type _defaultValueType = typeof(string);

        #endregion

        #region Identity

        /// <summary>
        /// The Multiline property replicates the one from the KryptonTextBox control
        /// </summary>
        [DefaultValue(false)]
        public bool Multiline
        {
            get => _multiline;
            set
            {
                if (_multiline != value)
                {
                    SetMultiline(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The MultilineStringEditor property replicates the one from the KryptonTextBox control
        /// </summary>
        [DefaultValue(false)]
        public bool MultilineStringEditor
        {
            get => _multilineStringEditor;
            set
            {
                if (_multilineStringEditor != value)
                {
                    SetMultilineStringEditor(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// Returns a standard textual representation of the cell.
        /// </summary>
        public override string ToString() =>
            $@"KryptonDataGridViewTextBoxCell {{{{ ColumnIndex={ColumnIndex.ToString(CultureInfo.CurrentCulture)}, RowIndex={RowIndex.ToString(CultureInfo.CurrentCulture)} }}";

        /// <summary>
        /// Creates an exact copy of this cell.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            KryptonDataGridViewTextBoxCell cloned = base.Clone() as KryptonDataGridViewTextBoxCell;
            foreach (IconSpec sp in IconSpecs)
            {
                cloned.IconSpecs.Add(sp.Clone() as IconSpec);
            }

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
        /// Define the type of the cell's editing control
        /// </summary>
        public override Type EditType => _defaultEditType;

        /// <summary>
        /// Returns the type of the cell's Value property
        /// </summary>
        public override Type ValueType => base.ValueType ?? _defaultValueType;

        /// <summary>
        /// DetachEditingControl gets called by the DataGridView control when the editing session is ending
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override void DetachEditingControl()
        {
            DataGridView dataGridView = DataGridView;
            if (dataGridView?.EditingControl == null)
            {
                throw new InvalidOperationException("Cell is detached or its grid has no editing control.");
            }

            if (dataGridView.EditingControl is KryptonTextBox kryptonTextBox)
            {
                if (OwningColumn is KryptonDataGridViewTextBoxColumn)
                {
                    foreach (ButtonSpec bs in kryptonTextBox.ButtonSpecs)
                    {
                        bs.Click -= OnButtonClick;
                    }

                    //kryptonTextBox.ButtonSpecs.Clear();

                    if (kryptonTextBox.Controls[0] is TextBox textBox)
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
            object initialFormattedValue,
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            if (DataGridView.EditingControl is KryptonTextBox textBox)
            {
                textBox.Text = initialFormattedValue is not string initialFormattedValueStr ? string.Empty : initialFormattedValueStr;

                DataGridViewTriState wrapMode = Style.WrapMode;
                if (wrapMode == DataGridViewTriState.NotSet)
                {
                    wrapMode = OwningColumn.DefaultCellStyle.WrapMode;
                }

                textBox.WordWrap = textBox.Multiline = wrapMode == DataGridViewTriState.True;

                if (OwningColumn is KryptonDataGridViewTextBoxColumn textBoxColumn)
                {
                    textBox.Multiline = textBoxColumn.Multiline;
                    textBox.MultilineStringEditor = textBoxColumn.MultilineStringEditor;
                    if (!textBox.MultilineStringEditor)
                    {
                        if (textBoxColumn.Multiline
                            || dataGridViewCellStyle.WrapMode == DataGridViewTriState.True
                           )
                        {
                            textBox.ScrollBars = ScrollBars.Vertical;
                        }
                    }
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
        private void OnButtonClick(object sender, EventArgs e)
        {
            KryptonDataGridViewTextBoxColumn textColumn = OwningColumn as KryptonDataGridViewTextBoxColumn;
            DataGridViewButtonSpecClickEventArgs args = new(textColumn, this, (ButtonSpecAny)sender);
            textColumn.PerformButtonSpecClick(args);
        }

        private KryptonDataGridViewTextBoxEditingControl EditingTextBox => DataGridView.EditingControl as KryptonDataGridViewTextBoxEditingControl;

        private Rectangle GetAdjustedEditingControlBounds(Rectangle editingControlBounds,
            DataGridViewCellStyle cellStyle)
        {
            // Adjust the vertical location of the editing control:
            var preferredHeight = DataGridView.EditingControl.GetPreferredSize(new Size(editingControlBounds.Width, 10000)).Height;
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

        private bool OwnsEditingTextBox(int rowIndex) =>
            rowIndex != -1 
            && DataGridView != null
            && (DataGridView.EditingControl is KryptonDataGridViewTextBoxEditingControl control)
                  && (rowIndex == ((IDataGridViewEditingControl)control).EditingControlRowIndex);

        private static bool PartPainted(DataGridViewPaintParts paintParts, DataGridViewPaintParts paintPart) => paintParts.HasFlag(paintPart);

        #endregion

        #region Internal
        internal void SetMultiline(int rowIndex, bool value)
        {
            _multiline = value;
            if (OwnsEditingTextBox(rowIndex))
            {
                EditingTextBox.Multiline = value;
            }
        }

        internal void SetMultilineStringEditor(int rowIndex, bool value)
        {
            _multilineStringEditor = value;
            if (OwnsEditingTextBox(rowIndex))
            {
                EditingTextBox.MultilineStringEditor = value;
            }
        }
        #endregion

        /// <summary>
        /// Gets the collection of the icon specifications.
        /// </summary>
        [Category(@"Data")]
        [Description(@"Set of extra icons to appear with control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<IconSpec> IconSpecs { get; } = new List<IconSpec>();
    }
}