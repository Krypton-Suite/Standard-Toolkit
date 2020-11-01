// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.550.0  
// *****************************************************************************

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Defines the editing control for the DataGridViewComboBoxCell custom cell type.
    /// </summary>
    [ToolboxItem(false)]
    public class KryptonDataGridViewComboBoxEditingControl : KryptonComboBox,
        IDataGridViewEditingControl
    {
        #region Instance Fields
        private DataGridView _dataGridView;
        private bool _valueChanged;

        #endregion

        #region Identity
        /// <summary>
        /// Initalize a new instance of the KryptonDataGridViewComboBoxEditingControl class.
        /// </summary>
        public KryptonDataGridViewComboBoxEditingControl()
        {
            TabStop = false;
            StateCommon.ComboBox.Border.Width = 0;
            StateCommon.ComboBox.Border.Draw = InheritBool.False;
            SetLayoutDisplayPadding(new Padding(0, 1, 1, 0));
        }
        #endregion

        #region Public
        /// <summary>
        /// Property which caches the grid that uses this editing control
        /// </summary>
        public virtual DataGridView EditingControlDataGridView
        {
            get => _dataGridView;
            set => _dataGridView = value;
        }

        /// <summary>
        /// Property which represents the current formatted value of the editing control
        /// </summary>
        public virtual object EditingControlFormattedValue
        {
            get => GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting);
            set => Text = (string)value;
        }

        /// <summary>
        /// Property which represents the row in which the editing control resides
        /// </summary>
        public virtual int EditingControlRowIndex { get; set; }

        /// <summary>
        /// Property which indicates whether the value of the editing control has changed or not
        /// </summary>
        public virtual bool EditingControlValueChanged
        {
            get => _valueChanged;
            set => _valueChanged = value;
        }

        /// <summary>
        /// Property which determines which cursor must be used for the editing panel, i.e. the parent of the editing control.
        /// </summary>
        public virtual Cursor EditingPanelCursor => Cursors.Default;

        /// <summary>
        /// Property which indicates whether the editing control needs to be repositioned when its value changes.
        /// </summary>
        public virtual bool RepositionEditingControlOnValueChange => false;

        /// <summary>
        /// Method called by the grid before the editing control is shown so it can adapt to the provided cell style.
        /// </summary>
        public virtual void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            StateCommon.ComboBox.Content.Font = dataGridViewCellStyle.Font;
            StateCommon.ComboBox.Content.Color1 = dataGridViewCellStyle.ForeColor;
            StateCommon.ComboBox.Back.Color1 = dataGridViewCellStyle.BackColor;
        }

        /// <summary>
        /// Method called by the grid on keystrokes to determine if the editing control is interested in the key or not.
        /// </summary>
        public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Down:
                case Keys.Up:
                case Keys.Home:
                case Keys.Delete:
                    return true;
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
        }
        #endregion

        #region Protected
        /// <summary>
        /// Listen to the TextChanged notification to forward the change to the grid.
        /// </summary>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (Focused)
            {
                NotifyDataGridViewOfValueChange();
            }
        }

        /// <summary>
        /// Listen to the SelectedIndexChanged notification to forward the change to the grid.
        /// </summary>
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            if (SelectedIndex != -1)
            {
                NotifyDataGridViewOfValueChange();
            }
        }

        /// <summary>
        /// A few keyboard messages need to be forwarded to the inner textbox of the
        /// KryptonComboBox control so that the first character pressed appears in it.
        /// </summary>
        protected override bool ProcessKeyEventArgs(ref Message m)
        {
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