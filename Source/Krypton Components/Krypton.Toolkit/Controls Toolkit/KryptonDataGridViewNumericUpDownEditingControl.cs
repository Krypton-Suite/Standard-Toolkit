﻿#region BSD License
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

namespace Krypton.Toolkit
{
    /// <summary>
    /// Defines the editing control for the DataGridViewNumericUpDownCell custom cell type.
    /// </summary>
    [ToolboxItem(false)]
    public class KryptonDataGridViewNumericUpDownEditingControl : KryptonNumericUpDown,
        IDataGridViewEditingControl
    {
        #region Instance Fields
        private DataGridView? _dataGridView;
        private bool _valueChanged;

        #endregion

        #region Identity
        /// <summary>
        /// Initalize a new instance of the KryptonDataGridViewNumericUpDownEditingControl class.
        /// </summary>
        public KryptonDataGridViewNumericUpDownEditingControl()
        {
            TabStop = false;
            StateCommon.Border.Width = 0;
            StateCommon.Border.Draw = InheritBool.False;
            SetLayoutDisplayPadding(new Padding(0, 0, 0, -1));
        }
        #endregion

        #region Public
        /// <summary>
        /// Property which caches the grid that uses this editing control
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual DataGridView? EditingControlDataGridView
        {
            get => _dataGridView;
            set => _dataGridView = value!;
        }

        /// <summary>
        /// Property which represents the current formatted value of the editing control
        /// <para>Allows null as input, but null will saved as an empty string.</para>
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [AllowNull]
        public virtual object EditingControlFormattedValue 
        {
            // [AllowNull] removes warning CS8767, but allows for null input, which is undesired.
            // The Text property is a non-nullable string and therefore null input
            // will be converted to String.Empty.

            get => GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting);
            set => Text = value is string str && str is not null
                ? str
                : string.Empty;
        }

        /// <summary>
        /// Property which represents the row in which the editing control resides
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int EditingControlRowIndex { get; set; }

        /// <summary>
        /// Property which indicates whether the value of the editing control has changed or not
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
                        if (Controls[0].Controls[1] is TextBox textBox)
                        {
                            // If the end of the selection is at the end of the string, let the DataGridView treat the key message
                            if (((RightToLeft == RightToLeft.No) && !((textBox.SelectionLength == 0) && (textBox.SelectionStart == textBox.Text.Length))) ||
                                ((RightToLeft == RightToLeft.Yes) && !(textBox is { SelectionLength: 0, SelectionStart: 0 })))
                            {
                                return true;
                            }
                        }
                        break;
                    }
                case Keys.Left:
                    {
                        if (Controls[0].Controls[1] is TextBox textBox)
                        {
                            // If the end of the selection is at the begining of the string or if the entire text is selected 
                            // and we did not start editing, send this character to the dataGridView, else process the key message
                            if (((RightToLeft == RightToLeft.No) && !(textBox is { SelectionLength: 0, SelectionStart: 0 })) ||
                                ((RightToLeft == RightToLeft.Yes) && !((textBox.SelectionLength == 0) && (textBox.SelectionStart == textBox.Text.Length))))
                            {
                                return true;
                            }
                        }
                        break;
                    }
                case Keys.Down:
                    if (Value > Minimum)
                    {
                        return true;
                    }

                    break;
                case Keys.Up:
                    if (Value < Maximum)
                    {
                        return true;
                    }

                    break;
                case Keys.Home:
                case Keys.End:
                    {
                        // Let the grid handle the key if the entire text is selected.
                        if (Controls[0].Controls[1] is TextBox textBox)
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
                        if (Controls[0].Controls[1] is TextBox textBox)
                        {
                            if ((textBox.SelectionLength > 0) ||
                                (textBox.SelectionStart < textBox.Text.Length))
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
            var userEdit = UserEdit;
            try
            {
                // Prevent the Value from being set to Maximum or Minimum when the cell is being painted.
                UserEdit = (context & DataGridViewDataErrorContexts.Display) == 0;
                return Value.ToString((ThousandsSeparator ? "N" : "F") + DecimalPlaces.ToString());
            }
            finally
            {
                UserEdit = userEdit;
            }
        }

        /// <summary>
        /// Called by the grid to give the editing control a chance to prepare itself for the editing session.
        /// </summary>
        public virtual void PrepareEditingControlForEdit(bool selectAll)
        {
            if (Controls[0].Controls[1] is TextBox textBox)
            {
                if (selectAll)
                {
                    textBox.SelectAll();
                }
                else
                {
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Listen to the KeyPress notification to know when the value changed, and notify the grid of the change.
        /// </summary>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            var notifyValueChange = false;
            if (char.IsDigit(e.KeyChar))
            {
                notifyValueChange = true;
            }
            else
            {
                NumberFormatInfo numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;
                var decimalSeparatorStr = numberFormatInfo.NumberDecimalSeparator;
                var groupSeparatorStr = numberFormatInfo.NumberGroupSeparator;
                var negativeSignStr = numberFormatInfo.NegativeSign;

                if (!string.IsNullOrEmpty(decimalSeparatorStr) && (decimalSeparatorStr.Length == 1))
                {
                    notifyValueChange = decimalSeparatorStr[0] == e.KeyChar;
                }

                if (!notifyValueChange && !string.IsNullOrEmpty(groupSeparatorStr) && (groupSeparatorStr.Length == 1))
                {
                    notifyValueChange = groupSeparatorStr[0] == e.KeyChar;
                }

                if (!notifyValueChange && !string.IsNullOrEmpty(negativeSignStr) && (negativeSignStr.Length == 1))
                {
                    notifyValueChange = negativeSignStr[0] == e.KeyChar;
                }
            }

            if (notifyValueChange)
            {
                NotifyDataGridViewOfValueChange();
            }
        }

        /// <summary>
        /// Listen to the ValueChanged notification to forward the change to the grid.
        /// </summary>
        protected override void OnValueChanged(EventArgs e)
        {
            base.OnValueChanged(e);

            if (Focused)
            {
                NotifyDataGridViewOfValueChange();
            }
        }

        /// <summary>
        /// A few keyboard messages need to be forwarded to the inner textbox of the
        /// KryptonNumericUpDown control so that the first character pressed appears in it.
        /// </summary>
        protected override bool ProcessKeyEventArgs(ref Message m)
        {
            if (Controls[0].Controls[1] is TextBox textBox)
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
                _dataGridView?.NotifyCurrentCellDirty(true);
            }
        }
        #endregion
    }
}