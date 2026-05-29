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

internal class KryptonHeaderGroupActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonHeaderGroup _headerGroup;
    private readonly IComponentChangeService? _service;
    private DesignerVerb _visible1;
    private DesignerVerb _visible2;
    private string _text1;
    private string _text2;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonHeaderGroupActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonHeaderGroupActionList(KryptonHeaderGroupDesigner owner)
        : base(owner.Component)
    {
        // Remember the panel instance
        _headerGroup = (owner.Component as KryptonHeaderGroup)!;

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
        get => _headerGroup.GroupBackStyle;

        set 
        {
            if (_headerGroup.GroupBackStyle != value)
            {
                _service?.OnComponentChanged(_headerGroup, null, _headerGroup.GroupBackStyle, value);
                _headerGroup.GroupBackStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the group border style.
    /// </summary>
    public PaletteBorderStyle GroupBorderStyle
    {
        get => _headerGroup.GroupBorderStyle;

        set 
        {
            if (_headerGroup.GroupBorderStyle != value)
            {
                _service?.OnComponentChanged(_headerGroup, null, _headerGroup.GroupBorderStyle, value);
                _headerGroup.GroupBorderStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the primary header style.
    /// </summary>
    public HeaderStyle HeaderStylePrimary
    {
        get => _headerGroup.HeaderStylePrimary;

        set 
        { 
            if (_headerGroup.HeaderStylePrimary != value)
            {
                _service?.OnComponentChanged(_headerGroup, null, _headerGroup.HeaderStylePrimary, value);
                _headerGroup.HeaderStylePrimary = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the secondary header style.
    /// </summary>
    public HeaderStyle HeaderStyleSecondary
    {
        get => _headerGroup.HeaderStyleSecondary;

        set 
        {
            if (_headerGroup.HeaderStyleSecondary != value)
            {
                _service?.OnComponentChanged(_headerGroup, null, _headerGroup.HeaderStyleSecondary, value);
                _headerGroup.HeaderStyleSecondary = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the primary header position.
    /// </summary>
    public VisualOrientation HeaderPositionPrimary
    {
        get => _headerGroup.HeaderPositionPrimary;

        set 
        {
            if (_headerGroup.HeaderPositionPrimary != value)
            {
                _service?.OnComponentChanged(_headerGroup, null, _headerGroup.HeaderPositionPrimary, value);
                _headerGroup.HeaderPositionPrimary = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the secondary header position.
    /// </summary>
    public VisualOrientation HeaderPositionSecondary
    {
        get => _headerGroup.HeaderPositionSecondary;

        set 
        {
            if (_headerGroup.HeaderPositionSecondary != value)
            {
                _service?.OnComponentChanged(_headerGroup, null, _headerGroup.HeaderPositionSecondary, value);
                _headerGroup.HeaderPositionSecondary = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _headerGroup.PaletteMode;

        set 
        {
            if (_headerGroup.PaletteMode != value)
            {
                _service?.OnComponentChanged(_headerGroup, null, _headerGroup.PaletteMode, value);
                _headerGroup.PaletteMode = value;
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
        if (_headerGroup != null)
        {
            // Get the current visible state of the headers
            var header1Visible = _headerGroup.HeaderVisiblePrimary;
            var header2Visible = _headerGroup.HeaderVisibleSecondary;

            // Decide on the initial text values
            _text1 = header1Visible ? "Hide primary header" : "Show primary header";
            _text2 = header2Visible ? "Hide secondary header" : "Show secondary header";

            // Create the two verbs for toggling the header visibility
            _visible1 = new DesignerVerb(_text1, OnVisibleClick);
            _visible2 = new DesignerVerb(_text2, OnVisibleClick);

            // Add the list of panel specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(GroupBackStyle), @"Back style", nameof(Appearance), @"Background style"));
            actions.Add(new DesignerActionPropertyItem(nameof(GroupBorderStyle), @"Border style", nameof(Appearance), @"Border style"));
            actions.Add(new DesignerActionHeaderItem(@"Primary Header"));
            actions.Add(new KryptonDesignerActionItem(_visible1, "Primary Header"));
            actions.Add(new DesignerActionPropertyItem(nameof(HeaderStylePrimary), @"Style", @"Primary Header", @"Primary header style"));
            actions.Add(new DesignerActionPropertyItem(nameof(HeaderPositionPrimary), @"Position", @"Primary Header", @"Primary header position"));
            actions.Add(new DesignerActionHeaderItem(@"Secondary Header"));
            actions.Add(new KryptonDesignerActionItem(_visible2, "Secondary Header"));
            actions.Add(new DesignerActionPropertyItem(nameof(HeaderStyleSecondary), @"Style", @"Secondary Header", @"Secondary header style"));
            actions.Add(new DesignerActionPropertyItem(nameof(HeaderPositionSecondary), @"Position", @"Secondary Header", @"Secondary header position"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion

    #region Implementation
    private void OnVisibleClick(object? sender, EventArgs e)
    {
        // Cast to the correct type
        var verb = sender as DesignerVerb;

        // Find out if this is the first or second header verb
        var header1 = verb == _visible1;

        // The new visible value should be the opposite of the current value
        var newVisible = !(header1 ? _headerGroup.HeaderVisiblePrimary : _headerGroup.HeaderVisibleSecondary);

        // Assign the new text to the correct header text
        if (header1)
        {
            _text1 = newVisible ? "Hide primary header" : "Show primary header";
        }
        else
        {
            _text2 = newVisible ? "Hide secondary header" : "Show secondary header";
        }

        if (header1)
        {
            _headerGroup.HeaderVisiblePrimary = newVisible;
        }
        else
        {
            _headerGroup.HeaderVisibleSecondary = newVisible;
        }

        // Get the user interface service associated with actions

        // If we managed to get it then request it update to reflect new action setting
        if (GetService(typeof(DesignerActionUIService)) is DesignerActionUIService service)
        {
            service.Refresh(_headerGroup);
        }
    }
    #endregion
}