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

internal class KryptonPanelActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonPanel _panel;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPanelActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonPanelActionList(KryptonPanelDesigner owner)
        : base(owner.Component)
    {
        // Remember the panel instance
        _panel = (owner.Component as KryptonPanel)!;

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion
        
    #region Public
    /// <summary>
    /// Gets and sets the panel background style.
    /// </summary>
    public PaletteBackStyle PanelBackStyle
    {
        get => _panel.PanelBackStyle;

        set 
        {
            if (_panel.PanelBackStyle != value)
            {
                _service?.OnComponentChanged(_panel, null, _panel.PanelBackStyle, value);
                _panel.PanelBackStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _panel.PaletteMode;

        set 
        {
            if (_panel.PaletteMode != value)
            {
                _service?.OnComponentChanged(_panel, null, _panel.PaletteMode, value);
                _panel.PaletteMode = value;
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
        if (_panel != null)
        {
            // Add the list of panel specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(PanelBackStyle), @"Back style", nameof(Appearance), @"Background style"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }
            
        return actions;
    }
    #endregion
}