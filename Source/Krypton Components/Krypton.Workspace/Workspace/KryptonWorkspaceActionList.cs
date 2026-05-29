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

namespace Krypton.Workspace;

internal class KryptonWorkspaceActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonWorkspace _workspace;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonWorkspaceActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonWorkspaceActionList(KryptonWorkspaceDesigner owner)
        : base(owner.Component)
    {
        // Remember designer and actual component instance being designed
        _workspace = (owner.Component as KryptonWorkspace)!;

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion
        
    #region Public
    /// <summary>
    /// Gets and sets the container background style.
    /// </summary>
    public PaletteBackStyle ContainerBackStyle
    {
        get => _workspace.ContainerBackStyle;

        set
        {
            if (_workspace.ContainerBackStyle != value)
            {
                _service?.OnComponentChanged(_workspace, null, _workspace.ContainerBackStyle, value);
                _workspace.ContainerBackStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the separator style.
    /// </summary>
    public SeparatorStyle SeparatorStyle
    {
        get => _workspace.SeparatorStyle;

        set
        {
            if (_workspace.SeparatorStyle != value)
            {
                _service?.OnComponentChanged(_workspace, null, _workspace.SeparatorStyle, value);
                _workspace.SeparatorStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets if resizing is allowed.
    /// </summary>
    public bool AllowResizing
    {
        get => _workspace.AllowResizing;

        set
        {
            if (_workspace.AllowResizing != value)
            {
                _service?.OnComponentChanged(_workspace, null, _workspace.AllowResizing, value);
                _workspace.AllowResizing = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets if flags for compacting the layout.
    /// </summary>
    public CompactFlags CompactFlags
    {
        get => _workspace.CompactFlags;

        set
        {
            if (_workspace.CompactFlags != value)
            {
                _service?.OnComponentChanged(_workspace, null, _workspace.CompactFlags, value);
                _workspace.CompactFlags = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _workspace.PaletteMode;

        set 
        {
            if (_workspace.PaletteMode != value)
            {
                _service?.OnComponentChanged(_workspace, null, _workspace.PaletteMode, value);
                _workspace.PaletteMode = value;
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
        var actions = new DesignerActionItemCollection();

        // This can be null when deleting a control instance at design time
        if (_workspace != null)
        {
            // Add the list of workspace specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(ContainerBackStyle), "BackStyle", nameof(Appearance), "Container background style"));
            actions.Add(new DesignerActionPropertyItem(nameof(SeparatorStyle), nameof(SeparatorStyle), nameof(Appearance), "Separator style"));
            actions.Add(new DesignerActionHeaderItem(nameof(Operation)));
            actions.Add(new DesignerActionPropertyItem(nameof(AllowResizing), nameof(AllowResizing), nameof(Operation), "Allow user to resize"));
            actions.Add(new DesignerActionPropertyItem(nameof(CompactFlags), nameof(CompactFlags), nameof(Operation), "Compacting flags"));
            actions.Add(new DesignerActionHeaderItem("Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), "Palette", "Visuals", "Palette applied to drawing"));
        }
            
        return actions;
    }
    #endregion
}