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
/// Defines the editing control for the DataGridViewKryptonDateTimePickerCell custom cell type.
/// </summary>
[ToolboxItem(false)]
public class KryptonDataGridViewDateTimePickerEditingControl : KryptonDateTimePicker,
    IDataGridViewEditingControl, IKryptonDataGridViewEditingControl
{
    #region Static Fields
    private static readonly DateTimeConverter _dtc = new DateTimeConverter();
    #endregion

    #region Instance Fields
    private DataGridView? _dataGridView;
    private bool _valueChanged;

    #endregion

    #region Identity
    /// <summary>
    /// Initalize a new instance of the KryptonDataGridViewDateTimePickerEditingControl class.
    /// </summary>
    public KryptonDataGridViewDateTimePickerEditingControl()
    {
        TabStop = false;
        StateCommon.Border.Width = 0;
        StateCommon.Border.Draw = InheritBool.False;
        ShowBorder = false;
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
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public virtual object EditingControlFormattedValue
    {
        // [AllowNull] removes warning CS8767 and allows to write null
        // although the interface defines the property as non-nullable

        get => GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting);

        set
        {
            if ((value == null) || (value == DBNull.Value))
            {
                ValueNullable = value;
            }
            else
            {
                var formattedValue = value as string;

                if (string.IsNullOrEmpty(formattedValue))
                {
                    ValueNullable = (formattedValue == string.Empty) ? null : value;
                }
                else
                {
                    Value = (DateTime)_dtc.ConvertFromInvariantString(formattedValue!)!;
                }
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

    /// <inheritdoc/>
    public void OnKryptonDataGridViewPaletteModeChanged(object? sender, EventArgs e)
    {
        KryptonDataGridViewUtilities.OnKryptonDataGridViewPaletteModeChanged(sender, this);
    }

    /// <summary>
    /// Called by the grid to give the editing control a chance to prepare itself for the editing session.
    /// </summary>
    public virtual void PrepareEditingControlForEdit(bool selectAll)
    {
    }

    /// <summary>
    /// Method called by the grid before the editing control is shown so it can adapt to the provided cell style.
    /// </summary>
    public virtual void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
    {
        StateCommon.Content.Font = dataGridViewCellStyle.Font;
        StateCommon.Content.Color1 = dataGridViewCellStyle.ForeColor;
        StateCommon.Back.Color1 = dataGridViewCellStyle.BackColor;
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
    public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context) => 
        (ValueNullable is null) || (ValueNullable == DBNull.Value) ? string.Empty : _dtc.ConvertToInvariantString(Value)!;

    #endregion

    #region Protected
    /// <summary>
    /// Listen to the ValueNullableChanged notification to forward the change to the grid.
    /// </summary>
    protected override void OnValueNullableChanged(EventArgs e)
    {
        base.OnValueNullableChanged(e);

        if (Focused)
        {
            NotifyDataGridViewOfValueChange();
        }
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