// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Megakraken & Simon Coghlan(aka Smurf-IV) 2017 - 2020. All rights reserved. (https://github.com/Wagnerp/Krypton-Toolkit-Suite-NET-Core)
//  Version 5.500.0.0  www.ComponentFactory.com
// *****************************************************************************

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Defines the editing control for the DataGridViewCustomCell custom cell type.
    /// </summary>
    [ToolboxItem(false)]
    public class KryptonDataGridViewCustomEditingControl : KryptonTextBox,
        IDataGridViewEditingControl
    {
        #region Instance Fields
        private DataGridView _dataGridView;
        private bool _valueChanged;
        private int _rowIndex;
        #endregion

        #region Identity
        /// <summary>
        /// Initalize a new instance of the KryptonDataGridViewCustomEditingControl class.
        /// </summary>
        public KryptonDataGridViewCustomEditingControl()
        {
            TabStop = false;
            StateCommon.Border.Width = 0;
            StateCommon.Border.Draw = InheritBool.False;
            StateCommon.Content.Padding = new Padding(0);
        }
        #endregion

        #region Public
        /// <summary>
        /// Property which caches the grid that uses this editing control
        /// </summary>
        public virtual DataGridView EditingControlDataGridView
        {
            get { return _dataGridView; }
            set { _dataGridView = value; }
        }

        /// <summary>
        /// Property which represents the current formatted value of the editing control
        /// </summary>
        public virtual object EditingControlFormattedValue
        {
            get { return GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting); }
            set { Text = (string)value; }
        }

        /// <summary>
        /// Property which represents the row in which the editing control resides
        /// </summary>
        public virtual int EditingControlRowIndex
        {
            get { return _rowIndex; }
            set { _rowIndex = value; }
        }

        /// <summary>
        /// Property which indicates whether the value of the editing control has changed or not
        /// </summary>
        public virtual bool EditingControlValueChanged
        {
            get { return _valueChanged; }
            set { _valueChanged = value; }
        }

        /// <summary>
        /// Property which determines which cursor must be used for the editing panel, i.e. the parent of the editing control.
        /// </summary>
        public virtual Cursor EditingPanelCursor
        {
            get { return Cursors.Default; }
        }

        /// <summary>
        /// Property which indicates whether the editing control needs to be repositioned when its value changes.
        /// </summary>
        public virtual bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }

        /// <summary>
        /// Method called by the grid before the editing control is shown so it can adapt to the provided cell style.
        /// </summary>
        public virtual void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            StateCommon.Content.Font = dataGridViewCellStyle.Font;
            StateCommon.Content.Color1 = dataGridViewCellStyle.ForeColor;
            StateCommon.Back.Color1 = dataGridViewCellStyle.BackColor;
            TextAlign = KryptonDataGridViewNumericUpDownCell.TranslateAlignment(dataGridViewCellStyle.Alignment);
        }

        /// <summary>
        /// Method called by the grid on keystrokes to determine if the editing control is interested in the key or not.
        /// </summary>
        public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Right:
                {
                    TextBox textBox = Controls[0] as TextBox;
                    if (textBox != null)
                    {
                        // If the end of the selection is at the end of the string, let the DataGridView treat the key message
                        if ((RightToLeft == RightToLeft.No && !(textBox.SelectionLength == 0 && textBox.SelectionStart == textBox.Text.Length)) ||
                            (RightToLeft == RightToLeft.Yes && !(textBox.SelectionLength == 0 && textBox.SelectionStart == 0)))
                        {
                            return true;
                        }
                    }
                    break;
                }
                case Keys.Left:
                {
                    TextBox textBox = Controls[0] as TextBox;
                    if (textBox != null)
                    {
                        // If the end of the selection is at the begining of the string or if the entire text is selected 
                        // and we did not start editing, send this character to the dataGridView, else process the key message
                        if ((RightToLeft == RightToLeft.No && !(textBox.SelectionLength == 0 && textBox.SelectionStart == 0)) ||
                            (RightToLeft == RightToLeft.Yes && !(textBox.SelectionLength == 0 && textBox.SelectionStart == textBox.Text.Length)))
                        {
                            return true;
                        }
                    }
                    break;
                }
                case Keys.Down:
                case Keys.Up:
                    return true;
                case Keys.Home:
                case Keys.End:
                {
                    // Let the grid handle the key if the entire text is selected.
                    TextBox textBox = Controls[0] as TextBox;
                    if (textBox != null)
                    {
                        if (textBox.SelectionLength != textBox.Text.Length)
                        {
                            return true;
                        }
                    }
                    break;
                }
                case Keys.Delete:
                {
                    // Let the grid handle the key if the carret is at the end of the text.
                    TextBox textBox = Controls[0] as TextBox;
                    if (textBox != null)
                    {
                        if (textBox.SelectionLength > 0 ||
                            textBox.SelectionStart < textBox.Text.Length)
                        {
                            return true;
                        }
                    }
                    break;
                }
            }

            return !dataGridViewWantsInputKey;
        }

        /// <summary>
        /// Returns the current value of the editing control.
        /// </summary>
        public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return Text;
        }

        /// <summary>
        /// Called by the grid to give the editing control a chance to prepare itself for the editing session.
        /// </summary>
        public virtual void PrepareEditingControlForEdit(bool selectAll)
        {
            TextBox textBox = Controls[0] as TextBox;
            if (textBox != null)
            {
                if (selectAll)
                    textBox.SelectAll();
                else
                    textBox.SelectionStart = textBox.Text.Length;
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Listen to the TextChanged notification to forward the change to the grid.
        /// </summary>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            //            if (Focused)
            NotifyDataGridViewOfValueChange();
        }

        /// <summary>
        /// A few keyboard messages need to be forwarded to the inner textbox of the
        /// KryptonNumericUpDown control so that the first character pressed appears in it.
        /// </summary>
        protected override bool ProcessKeyEventArgs(ref Message m)
        {
            TextBox textBox = Controls[0] as TextBox;
            if (textBox != null)
            {
                PI.SendMessage(textBox.Handle, m.Msg, m.WParam, m.LParam);
                return true;
            }

            return base.ProcessKeyEventArgs(ref m);
        }
        #endregion

        #region Private
        private void NotifyDataGridViewOfValueChange()
        {
            if (!_valueChanged)
            {
                _valueChanged = true;
                _dataGridView.NotifyCurrentCellDirty(true);
            }
        }
        #endregion
    }
}