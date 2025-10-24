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

internal class KryptonGroupActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonGroup _group;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonGroupActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonGroupActionList(KryptonGroupDesigner owner) 
        : base(owner.Component)
    {
        // Remember the group instance
        _group = (owner.Component as KryptonGroup)!;

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion
        
    #region Public
    /// <summary>
    /// Gets and sets the group background style.
    /// </summary>
    public PaletteBackStyle GroupBackStyle
    {
        get => _group.GroupBackStyle;

        set
        {
            if (_group.GroupBackStyle != value)
            {
                _service?.OnComponentChanged(_group, null, _group.GroupBackStyle, value);
                _group.GroupBackStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the group border style.
    /// </summary>
    public PaletteBorderStyle GroupBorderStyle
    {
        get => _group.GroupBorderStyle;

        set 
        {
            if (_group.GroupBorderStyle != value)
            {
                _service?.OnComponentChanged(_group, null, _group.GroupBorderStyle, value);
                _group.GroupBorderStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _group.PaletteMode;

        set 
        {
            if (_group.PaletteMode != value)
            {
                _service?.OnComponentChanged(_group, null, _group.PaletteMode, value);
                _group.PaletteMode = value;
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
        if (_group != null)
        {
            // Add the list of panel specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(GroupBackStyle), @"Back style", nameof(Appearance), @"Background style"));
            actions.Add(new DesignerActionPropertyItem(nameof(GroupBorderStyle), @"Border style", nameof(Appearance), @"Border style"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }
            
        return actions;
    }
    #endregion
}