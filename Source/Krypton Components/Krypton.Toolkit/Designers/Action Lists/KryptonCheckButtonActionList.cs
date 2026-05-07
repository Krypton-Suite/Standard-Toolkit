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

internal class KryptonCheckButtonActionList : KryptonButtonActionList
{
    #region Instance Fields
    private readonly KryptonCheckButton _checkButton;
    private readonly IComponentChangeService? _service;
    private string _action;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCheckButtonActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonCheckButtonActionList(KryptonCheckButtonDesigner owner)
        : base(owner)
    {
        // Remember the button instance
        _checkButton = (owner.Component as KryptonCheckButton)!;

        // Assuming we were correctly passed an actual component...
        if (_checkButton != null)
        {
            // Get access to the actual Orientation property
            PropertyDescriptor? checkedProp = TypeDescriptor.GetProperties(_checkButton)[nameof(Checked)];

            // If we succeeded in getting the property
            if (checkedProp != null)
            {
                // Decide on the next action to take given the current setting
                _action = (bool)checkedProp.GetValue(_checkButton)! ? "Uncheck the button" : "Check the button";
            }
        }

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the checked state.
    /// </summary>
    public bool Checked
    {
        get => _checkButton.Checked;

        set
        {
            if ( _checkButton.Checked != value)
            {
                _service?.OnComponentChanged(_checkButton, null, _checkButton.Checked, value);
                _checkButton.Checked = value;
            }
        }
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
        if (_checkButton != null)
        {
            // Add the list of button specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(_action, OnCheckedClick), nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(ButtonStyle), @"Style", nameof(Appearance), @"Button style"));
            actions.Add(new DesignerActionPropertyItem(nameof(Orientation), nameof(Orientation), nameof(Appearance), @"Button orientation"));
            actions.Add(new DesignerActionHeaderItem(@"Values"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), nameof(Text), @"Values", @"Button text"));
            actions.Add(new DesignerActionPropertyItem(nameof(ExtraText), nameof(ExtraText), @"Values", @"Button extra text"));
            actions.Add(new DesignerActionPropertyItem(nameof(Image), nameof(Image), @"Values", @"Button image"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion

    #region Implementation
    private void OnCheckedClick(object? sender, EventArgs e)
    {
        // Cast to the correct type

        // Double check the source is the expected type
        if (sender is DesignerVerb verb)
        {
            // Decide on the new orientation required
            var isChecked = verb.Text.Equals(@"Uncheck the button");

            // Decide on the next action to take given the new setting
            _action = isChecked ? "Uncheck the button" : "Check the button";

            // Get access to the actual Orientation property
            PropertyDescriptor? checkedProp = TypeDescriptor.GetProperties( _checkButton)[nameof(Checked)];

            // If we succeeded in getting the property
            // Update the actual property with the new value
            checkedProp?.SetValue(_checkButton, !isChecked);

            // Get the user interface service associated with actions

            // If we managed to get it then request it update to reflect new action setting
            if (GetService(typeof(DesignerActionUIService)) is DesignerActionUIService service)
            {
                service.Refresh(_checkButton);
            }
        }
    }
    #endregion
}