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

internal class KryptonSeparatorActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonSeparator _separator;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonSeparatorActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonSeparatorActionList(KryptonSeparatorDesigner owner)
        : base(owner.Component)
    {
        // Remember the link label instance
        _separator = (owner.Component as KryptonSeparator)!;

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion
        
    #region Public
    /// <summary>
    /// Gets and sets the separator style.
    /// </summary>
    public SeparatorStyle SeparatorStyle
    {
        get => _separator.SeparatorStyle;

        set 
        {
            if (_separator.SeparatorStyle != value)
            {
                _service?.OnComponentChanged(_separator, null, _separator.SeparatorStyle, value);
                _separator.SeparatorStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the visual orientation.
    /// </summary>
    public Orientation Orientation
    {
        get => _separator.Orientation;

        set
        {
            if (_separator.Orientation != value)
            {
                _service?.OnComponentChanged(_separator, null, _separator.Orientation, value);
                _separator.Orientation = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _separator.PaletteMode;

        set 
        {
            if (_separator.PaletteMode != value)
            {
                _service?.OnComponentChanged(_separator, null, _separator.PaletteMode, value);
                _separator.PaletteMode = value;
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
        if (_separator != null)
        {
            // Add the list of label specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(SeparatorStyle), @"Style", nameof(Appearance), @"Separator style"));
            actions.Add(new DesignerActionPropertyItem(nameof(Orientation), nameof(Orientation), nameof(Appearance), @"Visual orientation"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }
            
        return actions;
    }
    #endregion   
}