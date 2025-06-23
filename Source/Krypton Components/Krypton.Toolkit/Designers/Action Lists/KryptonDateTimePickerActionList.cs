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

internal class KryptonDateTimePickerActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonDateTimePicker _dateTimePicker;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDateTimePickerActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonDateTimePickerActionList(KryptonDateTimePickerDesigner owner)
        : base(owner.Component)
    {
        // Remember the bread crumb control instance
        _dateTimePicker = (owner.Component as KryptonDateTimePicker)!;

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Public
    /// <summary>Gets or sets the Krypton Context Menu.</summary>
    /// <value>The Krypton Context Menu.</value>
    public KryptonContextMenu? KryptonContextMenu
    {
        get => _dateTimePicker.KryptonContextMenu;

        set
        {
            if (_dateTimePicker.KryptonContextMenu != value)
            {
                _service?.OnComponentChanged(_dateTimePicker, null, _dateTimePicker.KryptonContextMenu, value);

                _dateTimePicker.KryptonContextMenu = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the display format.
    /// </summary>
    public DateTimePickerFormat Format
    {
        get => _dateTimePicker.Format;

        set
        {
            if (_dateTimePicker.Format != value)
            {
                _service?.OnComponentChanged(_dateTimePicker, null, _dateTimePicker.Format, value);
                _dateTimePicker.Format = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the display of up/down buttons.
    /// </summary>
    public bool ShowUpDown
    {
        get => _dateTimePicker.ShowUpDown;

        set
        {
            if (_dateTimePicker.ShowUpDown != value)
            {
                _service?.OnComponentChanged(_dateTimePicker, null, _dateTimePicker.ShowUpDown, value);
                _dateTimePicker.ShowUpDown = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the display of a check box.
    /// </summary>
    public bool ShowCheckBox
    {
        get => _dateTimePicker.ShowCheckBox;

        set
        {
            if (_dateTimePicker.ShowCheckBox != value)
            {
                _service?.OnComponentChanged(_dateTimePicker, null, _dateTimePicker.ShowCheckBox, value);
                _dateTimePicker.ShowCheckBox = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the checked state of the check box.
    /// </summary>
    public bool Checked
    {
        get => _dateTimePicker.Checked;

        set
        {
            if (_dateTimePicker.Checked != value)
            {
                _service?.OnComponentChanged(_dateTimePicker, null, _dateTimePicker.Checked, value);
                _dateTimePicker.Checked = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _dateTimePicker.PaletteMode;

        set
        {
            if (_dateTimePicker.PaletteMode != value)
            {
                _service?.OnComponentChanged(_dateTimePicker, null, _dateTimePicker.PaletteMode, value);
                _dateTimePicker.PaletteMode = value;
            }
        }
    }

    public Font Font
    {
        get => _dateTimePicker.StateCommon.Content.Font!;

        set
        {
            if (!Equals(_dateTimePicker.StateCommon.Content.Font, value))
            {
                _service?.OnComponentChanged(_dateTimePicker, null, _dateTimePicker.StateCommon.Content.Font, value);

                _dateTimePicker.StateCommon.Content.Font = value;
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
        if (_dateTimePicker != null)
        {
            // Add the list of bread crumb specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(KryptonContextMenu), @"Krypton Context Menu", nameof(Appearance), @"The Krypton Context Menu for the control."));
            actions.Add(new DesignerActionPropertyItem(nameof(Format), nameof(Format), nameof(Appearance), @"Decide what to display in the edit portion of the control"));
            actions.Add(new DesignerActionPropertyItem(nameof(ShowUpDown), nameof(ShowUpDown), nameof(Appearance), @"Display up and down buttons for modifying dates and times"));
            actions.Add(new DesignerActionPropertyItem(nameof(ShowCheckBox), nameof(ShowCheckBox), nameof(Appearance), @"Display a check box allowing the user to set the value is null"));
            actions.Add(new DesignerActionPropertyItem(nameof(Checked), nameof(Checked), nameof(Appearance), @"Is the current value null"));
            actions.Add(new DesignerActionPropertyItem(nameof(Font), nameof(Font), nameof(Appearance), @"The font for the date time picker."));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}