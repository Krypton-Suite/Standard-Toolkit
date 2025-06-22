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
/// Defines the editing control for the DataGridViewComboBoxCell custom cell type.
/// </summary>
[ToolboxItem(false)]
public class KryptonDataGridViewComboBoxEditingControl : KryptonComboBox,
    IDataGridViewEditingControl, IKryptonDataGridViewEditingControl
{
    #region Instance Fields
    private DataGridView? _dataGridView;
    private bool _valueChanged;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDataGridViewComboBoxEditingControl class.
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

        set
        {
            // #1800 correct to the standard of the Winforms counterpart
            // Text is only set if the cast is correct. null value will also be rejected.
            if (value is string str)
            {
                Text = str;
            }
        }
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
    /// Method called by the grid before the editing control is shown, so it can adapt to the provided cell style.
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
    public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey) => (keyData & Keys.KeyCode) switch
    {
        Keys.Right or Keys.Left or Keys.Down or Keys.Up or Keys.Home or Keys.Delete => true,
        _ => !dataGridViewWantsInputKey
    };

    /// <summary>
    /// Returns the current value of the editing control.
    /// </summary>
    public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
    {
        if (SelectedIndex > -1)
        {
            if (SelectedValue is not null
                && ValueMember is not null
                && ValueMember.Length > 0)
            {
                return SelectedValue.ToString() ?? string.Empty;
            }

            if (SelectedItem is not null)
            {
                return SelectedItem.ToString() ?? string.Empty;
            }
        }

        // For all other cases, return an empty string
        return string.Empty;
    }

    /// <summary>
    /// Called by the grid to give the editing control a chance to prepare itself for the editing session.
    /// </summary>
    public virtual void PrepareEditingControlForEdit(bool selectAll)
    {
    }

    /// <inheritdoc/>
    public void OnKryptonDataGridViewPaletteModeChanged(object? sender, EventArgs e)
    {
        KryptonDataGridViewUtilities.OnKryptonDataGridViewPaletteModeChanged(sender, this);
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
    protected override bool ProcessKeyEventArgs(ref Message m) => base.ProcessKeyEventArgs(ref m);

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