#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Represents a group of floating toolbars that can be tabbed together.
/// </summary>
public class FloatingToolbarGroup
{
    #region Instance Fields

    private VisualFloatingToolbarTabbedContainerForm? _tabbedContainerForm;

    #endregion

    #region Public

    /// <summary>
    /// Gets the list of toolbars in this group.
    /// </summary>
    public List<KryptonFloatableToolStrip> Toolbars { get; } = [];

    /// <summary>
    /// Gets the list of menu strips in this group.
    /// </summary>
    public List<KryptonFloatableMenuStrip> MenuStrips { get; } = [];

    /// <summary>
    /// Gets or sets the name of the group.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether toolbars in this group are tabbed.
    /// </summary>
    public bool IsTabbed
    {
        get => _isTabbed;
        set
        {
            if (_isTabbed != value)
            {
                _isTabbed = value;
                UpdateTabbedState();
            }
        }
    }
    private bool _isTabbed;

    /// <summary>
    /// Gets or sets the index of the active tab.
    /// </summary>
    public int ActiveTabIndex { get; set; }

    /// <summary>
    /// Gets the tabbed container form (if tabbed).
    /// </summary>
    public VisualFloatingToolbarTabbedContainerForm? TabbedContainerForm => _tabbedContainerForm;

    /// <summary>
    /// Occurs when a toolbar is added to the group.
    /// </summary>
    public event EventHandler<ToolbarGroupEventArgs>? ToolbarAdded;

    /// <summary>
    /// Occurs when a toolbar is removed from the group.
    /// </summary>
    public event EventHandler<ToolbarGroupEventArgs>? ToolbarRemoved;

    /// <summary>
    /// Occurs when a menu strip is added to the group.
    /// </summary>
    public event EventHandler<MenuStripGroupEventArgs>? MenuStripAdded;

    /// <summary>
    /// Occurs when a menu strip is removed from the group.
    /// </summary>
    public event EventHandler<MenuStripGroupEventArgs>? MenuStripRemoved;

    /// <summary>
    /// Initializes a new instance of the FloatingToolbarGroup class.
    /// </summary>
    public FloatingToolbarGroup()
    {
    }

    /// <summary>
    /// Initializes a new instance of the FloatingToolbarGroup class with a name.
    /// </summary>
    /// <param name="name">The name of the group.</param>
    public FloatingToolbarGroup(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Adds a toolbar to the group.
    /// </summary>
    /// <param name="toolbar">The toolbar to add.</param>
    public void AddToolbar(KryptonFloatableToolStrip toolbar)
    {
        if (!Toolbars.Contains(toolbar))
        {
            Toolbars.Add(toolbar);
            
            if (_isTabbed && _tabbedContainerForm != null)
            {
                _tabbedContainerForm.AddToolbar(toolbar);
            }
            
            ToolbarAdded?.Invoke(this, new ToolbarGroupEventArgs(toolbar, this));
        }
    }

    /// <summary>
    /// Adds a menu strip to the group.
    /// </summary>
    /// <param name="menuStrip">The menu strip to add.</param>
    public void AddMenuStrip(KryptonFloatableMenuStrip menuStrip)
    {
        if (!MenuStrips.Contains(menuStrip))
        {
            MenuStrips.Add(menuStrip);
            
            if (_isTabbed && _tabbedContainerForm != null)
            {
                _tabbedContainerForm.AddMenuStrip(menuStrip);
            }
            
            MenuStripAdded?.Invoke(this, new MenuStripGroupEventArgs(menuStrip, this));
        }
    }

    /// <summary>
    /// Removes a toolbar from the group.
    /// </summary>
    /// <param name="toolbar">The toolbar to remove.</param>
    public bool RemoveToolbar(KryptonFloatableToolStrip toolbar)
    {
        bool removed = Toolbars.Remove(toolbar);
        
        if (removed)
        {
            if (_isTabbed && _tabbedContainerForm != null)
            {
                _tabbedContainerForm.RemoveToolbar(toolbar);
            }
            
            ToolbarRemoved?.Invoke(this, new ToolbarGroupEventArgs(toolbar, this));
        }
        
        return removed;
    }

    /// <summary>
    /// Removes a menu strip from the group.
    /// </summary>
    /// <param name="menuStrip">The menu strip to remove.</param>
    public bool RemoveMenuStrip(KryptonFloatableMenuStrip menuStrip)
    {
        bool removed = MenuStrips.Remove(menuStrip);
        
        if (removed)
        {
            if (_isTabbed && _tabbedContainerForm != null)
            {
                _tabbedContainerForm.RemoveMenuStrip(menuStrip);
            }
            
            MenuStripRemoved?.Invoke(this, new MenuStripGroupEventArgs(menuStrip, this));
        }
        
        return removed;
    }

    /// <summary>
    /// Gets the total count of controls in the group.
    /// </summary>
    public int Count => Toolbars.Count + MenuStrips.Count;

    /// <summary>
    /// Saves the group state for persistence.
    /// </summary>
    public FloatingToolbarGroupState SaveState()
    {
        return new FloatingToolbarGroupState
        {
            Name = Name,
            IsTabbed = IsTabbed,
            ActiveTabIndex = ActiveTabIndex,
            ToolbarNames = Toolbars.Select(t => t.Name).ToList(),
            MenuStripNames = MenuStrips.Select(m => m.Name).ToList(),
            Location = _tabbedContainerForm?.Location ?? Point.Empty,
            Size = _tabbedContainerForm?.Size ?? Size.Empty,
            IsFloating = _tabbedContainerForm != null && _tabbedContainerForm.Visible
        };
    }

    /// <summary>
    /// Loads the group state from persistence.
    /// </summary>
    public void LoadState(FloatingToolbarGroupState state, Dictionary<string, KryptonFloatableToolStrip> toolbars, Dictionary<string, KryptonFloatableMenuStrip> menuStrips)
    {
        Name = state.Name;
        IsTabbed = state.IsTabbed;
        ActiveTabIndex = state.ActiveTabIndex;

        // Clear existing
        Toolbars.Clear();
        MenuStrips.Clear();

        // Load toolbars
        foreach (var toolbarName in state.ToolbarNames)
        {
            if (toolbars.TryGetValue(toolbarName, out var toolbar))
            {
                AddToolbar(toolbar);
            }
        }

        // Load menu strips
        foreach (var menuStripName in state.MenuStripNames)
        {
            if (menuStrips.TryGetValue(menuStripName, out var menuStrip))
            {
                AddMenuStrip(menuStrip);
            }
        }

        // Restore floating state if needed
        if (state.IsFloating && _tabbedContainerForm != null)
        {
            _tabbedContainerForm.Location = state.Location;
            _tabbedContainerForm.Size = state.Size;
            _tabbedContainerForm.Show();
        }
    }

    private void UpdateTabbedState()
    {
        if (_isTabbed)
        {
            // Create tabbed container if needed
            if (_tabbedContainerForm == null)
            {
                _tabbedContainerForm = new VisualFloatingToolbarTabbedContainerForm
                {
                    Text = $"Group: {Name}",
                    Group = this
                };
            }

            // Add all toolbars and menu strips to container
            foreach (var toolbar in Toolbars)
            {
                _tabbedContainerForm.AddToolbar(toolbar);
            }

            foreach (var menuStrip in MenuStrips)
            {
                _tabbedContainerForm.AddMenuStrip(menuStrip);
            }

            _tabbedContainerForm.Show();
        }
        else
        {
            // Close tabbed container
            _tabbedContainerForm?.Close();
            _tabbedContainerForm = null;
        }
    }

    #endregion
}

/// <summary>
/// Event arguments for toolbar group events.
/// </summary>
public class ToolbarGroupEventArgs : EventArgs
{
    /// <summary>
    /// Gets the toolbar.
    /// </summary>
    public KryptonFloatableToolStrip Toolbar { get; }

    /// <summary>
    /// Gets the group.
    /// </summary>
    public FloatingToolbarGroup Group { get; }

    /// <summary>
    /// Initializes a new instance of the ToolbarGroupEventArgs class.
    /// </summary>
    public ToolbarGroupEventArgs(KryptonFloatableToolStrip toolbar, FloatingToolbarGroup group)
    {
        Toolbar = toolbar;
        Group = group;
    }
}

/// <summary>
/// Event arguments for menu strip group events.
/// </summary>
public class MenuStripGroupEventArgs : EventArgs
{
    /// <summary>
    /// Gets the menu strip.
    /// </summary>
    public KryptonFloatableMenuStrip MenuStrip { get; }

    /// <summary>
    /// Gets the group.
    /// </summary>
    public FloatingToolbarGroup Group { get; }

    /// <summary>
    /// Initializes a new instance of the MenuStripGroupEventArgs class.
    /// </summary>
    public MenuStripGroupEventArgs(KryptonFloatableMenuStrip menuStrip, FloatingToolbarGroup group)
    {
        MenuStrip = menuStrip;
        Group = group;
    }
}

/// <summary>
/// Manager for floating toolbar groups.
/// </summary>
public static class FloatingToolbarGroupManager
{
    private static readonly List<FloatingToolbarGroup> _groups = [];

    /// <summary>
    /// Gets all groups.
    /// </summary>
    public static IReadOnlyList<FloatingToolbarGroup> Groups => _groups.AsReadOnly();

    /// <summary>
    /// Creates a new group.
    /// </summary>
    /// <param name="name">The name of the group.</param>
    /// <returns>The created group.</returns>
    public static FloatingToolbarGroup CreateGroup(string name)
    {
        var group = new FloatingToolbarGroup(name);
        _groups.Add(group);
        return group;
    }

    /// <summary>
    /// Removes a group.
    /// </summary>
    /// <param name="group">The group to remove.</param>
    public static bool RemoveGroup(FloatingToolbarGroup group)
    {
        return _groups.Remove(group);
    }

    /// <summary>
    /// Finds a group by name.
    /// </summary>
    /// <param name="name">The name of the group.</param>
    /// <returns>The group, or null if not found.</returns>
    public static FloatingToolbarGroup? FindGroup(string name)
    {
        return _groups.FirstOrDefault(g => g.Name == name);
    }

    /// <summary>
    /// Finds the group containing a toolbar.
    /// </summary>
    /// <param name="toolbar">The toolbar to find.</param>
    /// <returns>The group containing the toolbar, or null if not found.</returns>
    public static FloatingToolbarGroup? FindGroupContaining(KryptonFloatableToolStrip toolbar)
    {
        return _groups.FirstOrDefault(g => g.Toolbars.Contains(toolbar));
    }

    /// <summary>
    /// Finds the group containing a menu strip.
    /// </summary>
    /// <param name="menuStrip">The menu strip to find.</param>
    /// <returns>The group containing the menu strip, or null if not found.</returns>
    public static FloatingToolbarGroup? FindGroupContaining(KryptonFloatableMenuStrip menuStrip)
    {
        return _groups.FirstOrDefault(g => g.MenuStrips.Contains(menuStrip));
    }

    /// <summary>
    /// Moves a toolbar from one group to another.
    /// </summary>
    /// <param name="toolbar">The toolbar to move.</param>
    /// <param name="targetGroup">The target group.</param>
    /// <returns>True if the move was successful; otherwise, false.</returns>
    public static bool MoveToolbarToGroup(KryptonFloatableToolStrip toolbar, FloatingToolbarGroup targetGroup)
    {
        var sourceGroup = FindGroupContaining(toolbar);
        
        if (sourceGroup != null)
        {
            sourceGroup.RemoveToolbar(toolbar);
        }
        
        targetGroup.AddToolbar(toolbar);
        return true;
    }

    /// <summary>
    /// Moves a menu strip from one group to another.
    /// </summary>
    /// <param name="menuStrip">The menu strip to move.</param>
    /// <param name="targetGroup">The target group.</param>
    /// <returns>True if the move was successful; otherwise, false.</returns>
    public static bool MoveMenuStripToGroup(KryptonFloatableMenuStrip menuStrip, FloatingToolbarGroup targetGroup)
    {
        var sourceGroup = FindGroupContaining(menuStrip);
        
        if (sourceGroup != null)
        {
            sourceGroup.RemoveMenuStrip(menuStrip);
        }
        
        targetGroup.AddMenuStrip(menuStrip);
        return true;
    }

    /// <summary>
    /// Saves all group states for persistence.
    /// </summary>
    public static FloatingToolbarGroupStateCollection SaveAllGroupStates()
    {
        var collection = new FloatingToolbarGroupStateCollection
        {
            GroupStates = _groups.Select(g => g.SaveState()).ToList()
        };
        return collection;
    }

    /// <summary>
    /// Loads group states from persistence.
    /// </summary>
    public static void LoadGroupStates(FloatingToolbarGroupStateCollection collection, Dictionary<string, KryptonFloatableToolStrip> toolbars, Dictionary<string, KryptonFloatableMenuStrip> menuStrips)
    {
        _groups.Clear();

        foreach (var state in collection.GroupStates)
        {
            var group = CreateGroup(state.Name);
            group.LoadState(state, toolbars, menuStrips);
        }
    }
}
