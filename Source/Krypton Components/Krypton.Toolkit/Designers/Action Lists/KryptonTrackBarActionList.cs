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

internal class KryptonTrackBarActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonTrackBar _trackBar;
    private readonly IComponentChangeService? _service;
    private string _action;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonTrackBarActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonTrackBarActionList(KryptonTrackBarDesigner owner) 
        : base(owner.Component)
    {
        _trackBar = (owner.Component as KryptonTrackBar)!;

        // Assuming we were correctly passed an actual component...
        if (_trackBar != null)
        {
            // Get access to the actual Orientation property
            PropertyDescriptor? orientationProp = TypeDescriptor.GetProperties(_trackBar)[nameof(Orientation)];

            // If we succeeded in getting the property
            if (orientationProp != null)
            {
                // Decide on the next action to take given the current setting
                _action = (Orientation) orientationProp.GetValue(_trackBar)! == Orientation.Vertical
                    ? "Horizontal orientation"
                    : "Vertical orientation";
            }
        }

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _trackBar.PaletteMode;

        set 
        {
            if (_trackBar.PaletteMode != value)
            {
                _service?.OnComponentChanged(_trackBar, null, _trackBar.PaletteMode, value);
                _trackBar.PaletteMode = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the track bar tick style.
    /// </summary>
    public TickStyle TickStyle
    {
        get => _trackBar.TickStyle;

        set
        {
            if (_trackBar.TickStyle != value)
            {
                _service?.OnComponentChanged(_trackBar, null, _trackBar.TickStyle, value);
                _trackBar.TickStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the track bar size.
    /// </summary>
    public PaletteTrackBarSize TrackBarSize
    {
        get => _trackBar.TrackBarSize;

        set
        {
            if (_trackBar.TrackBarSize != value)
            {
                _service?.OnComponentChanged(_trackBar, null, _trackBar.TrackBarSize, value);
                _trackBar.TrackBarSize = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the track bar minium value.
    /// </summary>
    public int Minimum
    {
        get => _trackBar.Minimum;

        set
        {
            if (_trackBar.Minimum != value)
            {
                _service?.OnComponentChanged(_trackBar, null, _trackBar.Minimum, value);
                _trackBar.Minimum = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the track bar maximum value.
    /// </summary>
    public int Maximum
    {
        get => _trackBar.Maximum;

        set
        {
            if (_trackBar.Maximum != value)
            {
                _service?.OnComponentChanged(_trackBar, null, _trackBar.Maximum, value);
                _trackBar.Maximum = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the track bar small change value.
    /// </summary>
    public int SmallChange
    {
        get => _trackBar.SmallChange;

        set
        {
            if (_trackBar.SmallChange != value)
            {
                _service?.OnComponentChanged(_trackBar, null, _trackBar.SmallChange, value);
                _trackBar.SmallChange = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the track bar large change value.
    /// </summary>
    public int LargeChange
    {
        get => _trackBar.LargeChange;

        set
        {
            if (_trackBar.LargeChange != value)
            {
                _service?.OnComponentChanged(_trackBar, null, _trackBar.LargeChange, value);
                _trackBar.LargeChange = value;
            }
        }
    }
    #endregion

    #region Public Overrides
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        // Create a new collection for holding the single item we want to create
        var actions = new DesignerActionItemCollection();

        // This can be null when deleting a control instance at design time
        if (_trackBar != null)
        {
            // Add our own action to the end
            actions.Add(new DesignerActionHeaderItem(@"Layout"));
            actions.Add(new DesignerActionPropertyItem(nameof(TickStyle), @"Tick Style", @"Layout", @"Tick style"));
            actions.Add(new DesignerActionPropertyItem(nameof(TrackBarSize), @"TrackBar Size", @"Layout", @"Size of the track bar"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(_action, OnOrientationClick), "Layout"));
            actions.Add(new DesignerActionHeaderItem(@"Values"));
            actions.Add(new DesignerActionPropertyItem(nameof(Minimum), nameof(Minimum), @"Values", @"Minium value"));
            actions.Add(new DesignerActionPropertyItem(nameof(Maximum), nameof(Maximum), @"Values", @"Maximum value"));
            actions.Add(new DesignerActionPropertyItem(nameof(SmallChange), @"Small Change", @"Values", @"Small change value"));
            actions.Add(new DesignerActionPropertyItem(nameof(LargeChange), @"Large Change", @"Values", @"Large change value"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion

    #region Implementation
    private void OnOrientationClick(object? sender, EventArgs e)
    {
        // Cast to the correct type

        // Double check the source is the expected type
        if (sender is DesignerVerb verb)
        {
            // Decide on the new orientation required
            Orientation orientation = verb.Text.Equals("Horizontal orientation") ? Orientation.Horizontal : Orientation.Vertical;

            // Decide on the next action to take given the new setting
            _action = orientation == Orientation.Vertical ? "Horizontal orientation" : "Vertical orientation";

            // Get access to the actual Orientation property
            PropertyDescriptor? orientationProp = TypeDescriptor.GetProperties(_trackBar)[nameof(Orientation)];

            // If we succeeded in getting the property
            // Update the actual property with the new value
            orientationProp?.SetValue(_trackBar, orientation);

            // Get the user interface service associated with actions

            // If we managed to get it then request it update to reflect new action setting
            if (GetService(typeof(DesignerActionUIService)) is DesignerActionUIService service)
            {
                service.Refresh(_trackBar);
            }
        }
    }
    #endregion
}