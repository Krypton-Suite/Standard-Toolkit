#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Action list for KryptonNumericUpDown using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonNumericUpDownExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonNumericUpDown _numericUpDown;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonNumericUpDownExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonNumericUpDownExtensibilityActionList(KryptonNumericUpDownExtensibilityDesigner owner)
        : base(owner)
    {
        // Remember the numericupdown instance
        _numericUpDown = (owner.Component as KryptonNumericUpDown)!;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the numericupdown style.
    /// </summary>
    public InputControlStyle InputControlStyle
    {
        get => _numericUpDown.InputControlStyle;
        set => SetPropertyValue(_numericUpDown, nameof(InputControlStyle), _numericUpDown.InputControlStyle, value, v => _numericUpDown.InputControlStyle = v);
    }

    /// <summary>
    /// Gets and sets the current value.
    /// </summary>
    public decimal Value
    {
        get => _numericUpDown.Value;
        set => SetPropertyValue(_numericUpDown, nameof(Value), _numericUpDown.Value, value, v => _numericUpDown.Value = v);
    }

    /// <summary>
    /// Gets and sets the minimum value.
    /// </summary>
    public decimal Minimum
    {
        get => _numericUpDown.Minimum;
        set => SetPropertyValue(_numericUpDown, nameof(Minimum), _numericUpDown.Minimum, value, v => _numericUpDown.Minimum = v);
    }

    /// <summary>
    /// Gets and sets the maximum value.
    /// </summary>
    public decimal Maximum
    {
        get => _numericUpDown.Maximum;
        set => SetPropertyValue(_numericUpDown, nameof(Maximum), _numericUpDown.Maximum, value, v => _numericUpDown.Maximum = v);
    }

    /// <summary>
    /// Gets and sets the increment value.
    /// </summary>
    public decimal Increment
    {
        get => _numericUpDown.Increment;
        set => SetPropertyValue(_numericUpDown, nameof(Increment), _numericUpDown.Increment, value, v => _numericUpDown.Increment = v);
    }

    /// <summary>
    /// Gets and sets the decimal places.
    /// </summary>
    public int DecimalPlaces
    {
        get => _numericUpDown.DecimalPlaces;
        set => SetPropertyValue(_numericUpDown, nameof(DecimalPlaces), _numericUpDown.DecimalPlaces, value, v => _numericUpDown.DecimalPlaces = v);
    }

    /// <summary>
    /// Gets and sets whether thousands separators are shown.
    /// </summary>
    public bool ThousandsSeparator
    {
        get => _numericUpDown.ThousandsSeparator;
        set => SetPropertyValue(_numericUpDown, nameof(ThousandsSeparator), _numericUpDown.ThousandsSeparator, value, v => _numericUpDown.ThousandsSeparator = v);
    }

    /// <summary>
    /// Gets and sets whether the control is read-only.
    /// </summary>
    public bool ReadOnly
    {
        get => _numericUpDown.ReadOnly;
        set => SetPropertyValue(_numericUpDown, nameof(ReadOnly), _numericUpDown.ReadOnly, value, v => _numericUpDown.ReadOnly = v);
    }
    #endregion

    #region Public Override
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        // Create a new collection for holding the single item we want to create
        var actions = new DesignerActionItemCollection();

        // This can be null when deleting a control instance at design time
        if (_numericUpDown != null)
        {
            // Add the list of numericupdown specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(InputControlStyle), @"Style", nameof(Appearance), @"Input control style"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Value), nameof(Value), @"Behavior", @"Current value"));
            actions.Add(new DesignerActionPropertyItem(nameof(Minimum), nameof(Minimum), @"Behavior", @"Minimum value"));
            actions.Add(new DesignerActionPropertyItem(nameof(Maximum), nameof(Maximum), @"Behavior", @"Maximum value"));
            actions.Add(new DesignerActionPropertyItem(nameof(Increment), nameof(Increment), @"Behavior", @"Increment value"));
            actions.Add(new DesignerActionPropertyItem(nameof(DecimalPlaces), nameof(DecimalPlaces), @"Behavior", @"Number of decimal places"));
            actions.Add(new DesignerActionPropertyItem(nameof(ThousandsSeparator), nameof(ThousandsSeparator), @"Behavior", @"Show thousands separators"));
            actions.Add(new DesignerActionPropertyItem(nameof(ReadOnly), nameof(ReadOnly), @"Behavior", @"Read-only mode"));
        }

        return actions;
    }
    #endregion
}
