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
/// Defines the editing control for the DataGridViewCustomCell custom cell type.
/// </summary>
[ToolboxItem(false)]
public class KryptonDataGridViewCustomEditingControl : KryptonTextBox,
    IDataGridViewEditingControl, IKryptonDataGridViewEditingControl
{
    #region Instance Fields
    private DataGridView? _dataGridView;
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
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual DataGridView? EditingControlDataGridView
    {
        get => _dataGridView;
        set
        {
            // (un)subscribing must be performed before _dataGridView is updated.
            KryptonDataGridViewUtilities.OnKryptonDataGridViewEditingControlDataGridViewChanged(_dataGridView, value, OnKryptonDataGridViewPaletteModeChanged);

            _dataGridView = value;

            // Trigger a manual palette check
            KryptonDataGridViewUtilities.OnKryptonDataGridViewPaletteModeChanged(_dataGridView, this);
        }
    }

    /// <summary>
    /// Property which represents the current formatted value of the editing control
    /// <para>Allows null as input, but null will saved as an empty string.</para>
    /// </summary>
    [AllowNull]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
    public virtual int EditingControlRowIndex
    {
        get => _rowIndex;
        set => _rowIndex = value;
    }

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

    /// <inheritdoc/>
    public void OnKryptonDataGridViewPaletteModeChanged(object? sender, EventArgs e)
    {
        KryptonDataGridViewUtilities.OnKryptonDataGridViewPaletteModeChanged(sender, this);
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
                if (Controls[0] is TextBox textBox)
                {
                    // If the end of the selection is at the end of the string, let the DataGridView treat the key message
                    if ((RightToLeft == RightToLeft.No && !(textBox.SelectionLength == 0 && textBox.SelectionStart == textBox.Text.Length)) ||
                        (RightToLeft == RightToLeft.Yes && !(textBox is { SelectionLength: 0, SelectionStart: 0 })))
                    {
                        return true;
                    }
                }
                break;
            }
            case Keys.Left:
            {
                if (Controls[0] is TextBox textBox)
                {
                    // If the end of the selection is at the begining of the string or if the entire text is selected 
                    // and we did not start editing, send this character to the dataGridView, else process the key message
                    if ((RightToLeft == RightToLeft.No && !(textBox is { SelectionLength: 0, SelectionStart: 0 })) ||
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
                if (Controls[0] is TextBox textBox)
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
                if (Controls[0] is TextBox textBox)
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
    public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context) => Text;

    /// <summary>
    /// Called by the grid to give the editing control a chance to prepare itself for the editing session.
    /// </summary>
    public virtual void PrepareEditingControlForEdit(bool selectAll)
    {
        if (Controls[0] is TextBox textBox)
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
        if (Controls[0] is TextBox textBox)
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