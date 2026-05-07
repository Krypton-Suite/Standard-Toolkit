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

internal class KryptonBreadCrumbActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonBreadCrumb _breadCrumb;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonBreadCrumbActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonBreadCrumbActionList(KryptonBreadCrumbDesigner owner)
        : base(owner.Component)
    {
        // Remember the bread crumb control instance
        _breadCrumb = (owner.Component as KryptonBreadCrumb)!;

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the background drawing style.
    /// </summary>
    public PaletteBackStyle ControlBackStyle
    {
        get => _breadCrumb.ControlBackStyle;

        set
        {
            if (_breadCrumb.ControlBackStyle != value)
            {
                _service?.OnComponentChanged(_breadCrumb, null, _breadCrumb.ControlBackStyle, value);
                _breadCrumb.ControlBackStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the border drawing style.
    /// </summary>
    public PaletteBorderStyle ControlBorderStyle
    {
        get => _breadCrumb.ControlBorderStyle;

        set
        {
            if (_breadCrumb.ControlBorderStyle != value)
            {
                _service?.OnComponentChanged(_breadCrumb, null, _breadCrumb.ControlBorderStyle, value);
                _breadCrumb.ControlBorderStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the crumb drawing style.
    /// </summary>
    public ButtonStyle CrumbButtonStyle
    {
        get => _breadCrumb.CrumbButtonStyle;

        set
        {
            if (_breadCrumb.CrumbButtonStyle != value)
            {
                _service?.OnComponentChanged(_breadCrumb, null, _breadCrumb.CrumbButtonStyle, value);
                _breadCrumb.CrumbButtonStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _breadCrumb.PaletteMode;

        set
        {
            if (_breadCrumb.PaletteMode != value)
            {
                _service?.OnComponentChanged(_breadCrumb, null, _breadCrumb.PaletteMode, value);
                _breadCrumb.PaletteMode = value;
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
        if (_breadCrumb != null)
        {
            // Add the list of bread crumb specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(ControlBackStyle), @"Back Style", nameof(Appearance), @"Background drawing style."));
            actions.Add(new DesignerActionPropertyItem(nameof(ControlBorderStyle), @"Border Style", nameof(Appearance), @"Border drawing style."));
            actions.Add(new DesignerActionPropertyItem(nameof(CrumbButtonStyle), @"Crumb Style", nameof(Appearance), @"Crumb drawing style."));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}