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

internal class KryptonHeaderActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonHeader _header;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonHeaderActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonHeaderActionList(KryptonHeaderDesigner owner)
        : base(owner.Component)
    {
        // Remember the header instance
        _header = (owner.Component as KryptonHeader)!;

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion
        
    #region Public
    /// <summary>
    /// Gets and sets the header style.
    /// </summary>
    public HeaderStyle HeaderStyle
    {
        get => _header.HeaderStyle;

        set 
        {
            if (_header.HeaderStyle != value)
            {
                _service?.OnComponentChanged(_header, null, _header.HeaderStyle, value);
                _header.HeaderStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the visual orientation.
    /// </summary>
    public VisualOrientation Orientation
    {
        get => _header.Orientation;

        set 
        {
            if (_header.Orientation != value)
            {
                _service?.OnComponentChanged(_header, null, _header.Orientation, value);
                _header.Orientation = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the header text.
    /// </summary>
    public string Heading
    {
        get => _header.Values.Heading;

        set 
        {
            if (_header.Values.Heading != value)
            {
                _service?.OnComponentChanged(_header, null, _header.Values.Heading, value);
                _header.Values.Heading = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the header description text.
    /// </summary>
    public string Description
    {
        get => _header.Values.Description;

        set 
        {
            if (_header.Values.Description != value)
            {
                _service?.OnComponentChanged(_header, null, _header.Values.Description, value);
                _header.Values.Description = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the header image.
    /// </summary>
    public Image? Image
    {
        get => _header.Values.Image;

        set 
        {
            if (_header.Values.Image != value)
            {
                _service?.OnComponentChanged(_header, null, _header.Values.Image, value);
                _header.Values.Image = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _header.PaletteMode;

        set 
        {
            if (_header.PaletteMode != value)
            {
                _service?.OnComponentChanged(_header, null, _header.PaletteMode, value);
                _header.PaletteMode = value;
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
        if (_header != null)
        {
            // Add the list of header specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(HeaderStyle), @"Style", nameof(Appearance), @"Header style"));
            actions.Add(new DesignerActionPropertyItem(nameof(Orientation), nameof(Orientation), nameof(Appearance), @"Header orientation"));
            actions.Add(new DesignerActionHeaderItem(@"Values"));
            actions.Add(new DesignerActionPropertyItem(nameof(Heading), nameof(Heading), @"Values", @"Heading text"));
            actions.Add(new DesignerActionPropertyItem(nameof(Description), nameof(Description), @"Values", @"Header description text"));
            actions.Add(new DesignerActionPropertyItem(nameof(Image), nameof(Image), @"Values", @"Heading image"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }
            
        return actions;
    }
    #endregion
}