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
// ReSharper disable EventNeverSubscribedTo.Global

namespace Krypton.Toolkit
{
    /// <summary>
    /// Hosts a collection of KryptonDataGridViewTextBoxCell cells.
    /// </summary>
    [Designer("Krypton.Toolkit.KryptonTextBoxColumnDesigner, Krypton.Toolkit")]
    [ToolboxBitmap(typeof(KryptonDataGridViewTextBoxColumn), "ToolboxBitmaps.KryptonTextBox.bmp")]
    public class KryptonDataGridViewTextBoxColumn : KryptonDataGridViewIconColumn
    {
        #region Instance Fields

        #endregion

        #region Events
        /// <summary>
        /// Occurs when the user clicks a button spec.
        /// </summary>
        public event EventHandler<DataGridViewButtonSpecClickEventArgs> ButtonSpecClick;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewTextBoxColumn class.
        /// </summary>
        public KryptonDataGridViewTextBoxColumn()
            : base(new KryptonDataGridViewTextBoxCell())
        {
            ButtonSpecs = new DataGridViewColumnSpecCollection(this);
            SortMode = DataGridViewColumnSortMode.Automatic;
        }

        /// <summary>
        /// Returns a String that represents the current Object.
        /// </summary>
        /// <returns>A String that represents the current Object.</returns>
        public override string ToString()
        {
            StringBuilder builder = new(0x40);
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
            KryptonDataGridViewTextBoxColumn cloned = base.Clone() as KryptonDataGridViewTextBoxColumn;

            // Move the button specs over to the new clone
            foreach (ButtonSpec bs in ButtonSpecs)
            {
                cloned.ButtonSpecs.Add(bs.Clone());
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
        /// Gets or sets the maximum number of characters that can be entered into the text box.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(typeof(int), "32767")]
        public int MaxInputLength
        {
            get =>
                TextBoxCellTemplate == null
                    ? throw new InvalidOperationException("KryptonDataGridViewTextBoxColumn cell template required")
                    : TextBoxCellTemplate.MaxInputLength;

            set
            {
                if (MaxInputLength != value)
                {
                    TextBoxCellTemplate.MaxInputLength = value;
                    if (DataGridView != null)
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
        [DefaultValue(typeof(DataGridViewColumnSortMode), "Automatic")]
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
        public override DataGridViewCell CellTemplate
        {
            get => base.CellTemplate;

            set
            {
                if ((value != null) && value is not KryptonDataGridViewTextBoxCell)
                {
                    throw new InvalidCastException("Can only assign a object of type KryptonDataGridViewTextBoxCell");
                }

                base.CellTemplate = value;
            }
        }

        /// <summary>
        /// Make sure that when the style is set that the datagrid respects the values
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("DataGridView Column DefaultCell Style\r\nIf you set wrap mode, then this will ensure the DataRows are set to display the wrapped text!")]
        public override DataGridViewCellStyle DefaultCellStyle
        {
            get => base.DefaultCellStyle;

            set
            {
                base.DefaultCellStyle = value;
                if ((value.WrapMode != DataGridViewTriState.True)
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
        /// Gets the collection of the button specifications.
        /// </summary>
        [Category("Data")]
        [Description("Set of extra button specs to appear with control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DataGridViewColumnSpecCollection ButtonSpecs { get; }


        /// <summary>
        /// Replicates the Multiline property of the KryptonDataGridViewTextBoxCell cell type.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Indicates whether the text in the editing control can span more than one line.")]
        public bool Multiline
        {
            get =>
                TextBoxCellTemplate == null
                    ? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.")
                    : TextBoxCellTemplate.Multiline;
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
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Indicates whether the editing control uses the multiline string editor widget.")]
        public bool MultilineStringEditor
        {
            get =>
                TextBoxCellTemplate == null
                    ? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.")
                    : TextBoxCellTemplate.MultilineStringEditor;
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
        private KryptonDataGridViewTextBoxCell TextBoxCellTemplate => (KryptonDataGridViewTextBoxCell)CellTemplate;

        #endregion

        #region Internal
        internal void PerformButtonSpecClick(DataGridViewButtonSpecClickEventArgs args) => ButtonSpecClick?.Invoke(this, args);

        #endregion
    }
}