#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonHeaderGroupActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonHeaderGroupDesigner _owner;
    private readonly KryptonHeaderGroup _headerGroup;
    private readonly IComponentChangeService? _service;
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
        _owner = owner;
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
            var header1Visible = _headerGroup.HeaderVisiblePrimary;
            var header2Visible = _headerGroup.HeaderVisibleSecondary;
            var text1 = header1Visible ? "Hide primary header" : "Show primary header";
            var text2 = header2Visible ? "Hide secondary header" : "Show secondary header";

            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(GroupBackStyle), @"Back style", nameof(Appearance), @"Background style"));
            actions.Add(new DesignerActionPropertyItem(nameof(GroupBorderStyle), @"Border style", nameof(Appearance), @"Border style"));
            actions.Add(new DesignerActionHeaderItem(@"Primary Header"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(text1, OnTogglePrimaryHeader), "Primary Header"));
            actions.Add(new DesignerActionPropertyItem(nameof(HeaderStylePrimary), @"Style", @"Primary Header", @"Primary header style"));
            actions.Add(new DesignerActionPropertyItem(nameof(HeaderPositionPrimary), @"Position", @"Primary Header", @"Primary header position"));
            actions.Add(new DesignerActionHeaderItem(@"Secondary Header"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(text2, OnToggleSecondaryHeader), "Secondary Header"));
            actions.Add(new DesignerActionPropertyItem(nameof(HeaderStyleSecondary), @"Style", @"Secondary Header", @"Secondary header style"));
            actions.Add(new DesignerActionPropertyItem(nameof(HeaderPositionSecondary), @"Position", @"Secondary Header", @"Secondary header position"));
            actions.Add(new DesignerActionHeaderItem(@"Actions"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Add ButtonSpec", OnAddButtonSpec), @"Actions"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion

    #region Implementation
    private void OnTogglePrimaryHeader(object? sender, EventArgs e) => _owner.TogglePrimaryHeader();

    private void OnToggleSecondaryHeader(object? sender, EventArgs e) => _owner.ToggleSecondaryHeader();

    private void OnAddButtonSpec(object? sender, EventArgs e) => _owner.AddButtonSpec();
    #endregion
}