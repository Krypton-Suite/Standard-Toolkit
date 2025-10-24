#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Ribbon;

internal class KryptonRibbonTabDesigner : ComponentDesigner
{
    #region Instance Fields
    private IDesignerHost _designerHost;
    private IComponentChangeService _changeService;
    private KryptonRibbonTab? _ribbonTab;
    private DesignerVerbCollection? _verbs;
    private DesignerVerb _toggleHelpersVerb;
    private DesignerVerb _moveFirstVerb;
    private DesignerVerb _movePrevVerb;
    private DesignerVerb _moveNextVerb;
    private DesignerVerb _moveLastVerb;
    private DesignerVerb _addGroupVerb;
    private DesignerVerb _clearGroupsVerb;
    private DesignerVerb _deleteTabVerb;
    private ContextMenuStrip? _cms;
    private ToolStripMenuItem _toggleHelpersMenu;
    private ToolStripMenuItem _visibleMenu;
    private ToolStripMenuItem _moveFirstMenu;
    private ToolStripMenuItem _movePreviousMenu;
    private ToolStripMenuItem _moveNextMenu;
    private ToolStripMenuItem _moveLastMenu;
    private ToolStripMenuItem _addGroupMenu;
    private ToolStripMenuItem _clearGroupsMenu;
    private ToolStripMenuItem _deleteTabMenu;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRibbonTabDesigner class.
    /// </summary>
    public KryptonRibbonTabDesigner()
    {
    }
    #endregion

    #region Public
    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    /// <param name="component">The IComponent to associate the designer with.</param>
    public override void Initialize([DisallowNull] IComponent component)
    {
        // Let base class do standard stuff
        base.Initialize(component);

        Debug.Assert(component != null);

        // Cast to correct type
        _ribbonTab = component as KryptonRibbonTab;
        if (_ribbonTab != null)
        {
            _ribbonTab.DesignTimeAddGroup += OnAddGroup;
            _ribbonTab.DesignTimeContextMenu += OnContextMenu;
        }

        // Get access to the services
        _designerHost = (IDesignerHost?)GetService(typeof(IDesignerHost)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(_designerHost)));
        _changeService = (IComponentChangeService?)GetService(typeof(IComponentChangeService)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(_changeService)));

        // We need to know when we are being removed/changed
        _changeService.ComponentRemoving += OnComponentRemoving;
        _changeService.ComponentChanged += OnComponentChanged;
    }

    /// <summary>
    /// Gets the collection of components associated with the component managed by the designer.
    /// </summary>
    public override ICollection AssociatedComponents
    {
        get
        {
            var compound = new ArrayList(base.AssociatedComponents);
                
            if (_ribbonTab is not null)
            {
                compound.AddRange(_ribbonTab.Groups);
            }

            return compound;
        }
    }

    /// <summary>
    /// Gets the design-time verbs supported by the component that is associated with the designer.
    /// </summary>
    public override DesignerVerbCollection Verbs
    {
        get
        {
            UpdateVerbStatus();
            return _verbs!;
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// Releases all resources used by the component. 
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        try
        {
            if (disposing)
            {
                // Kill the menu strip
                if (_cms != null)
                {
                    _cms.Dispose();
                    _cms = null;
                }

                // Unhook from events
                if (_ribbonTab is not null)
                {
                    _ribbonTab.DesignTimeAddGroup -= OnAddGroup;
                    _ribbonTab.DesignTimeContextMenu -= OnContextMenu;
                }

                _changeService.ComponentRemoving -= OnComponentRemoving;
                _changeService.ComponentChanged -= OnComponentChanged;
            }
        }
        finally
        {
            // Must let base class do standard stuff
            base.Dispose(disposing);
        }
    }
    #endregion

    #region Implementation
    private void UpdateVerbStatus()
    {
        // Create verbs first time around
        if (_verbs is null)
        {
            _verbs = [];
            _toggleHelpersVerb = new DesignerVerb(@"Toggle Helpers", OnToggleHelpers);
            _moveFirstVerb = new DesignerVerb(@"Move First", OnMoveFirst);
            _movePrevVerb = new DesignerVerb(@"Move Previous", OnMovePrevious);
            _moveNextVerb = new DesignerVerb(@"Move Next", OnMoveNext);
            _moveLastVerb = new DesignerVerb(@"Move Last", OnMoveLast);
            _addGroupVerb = new DesignerVerb(@"Add Group", OnAddGroup);
            _clearGroupsVerb = new DesignerVerb(@"Clear Groups", OnClearGroups);
            _deleteTabVerb = new DesignerVerb(@"Delete Tab", OnDeleteTab);
            _verbs.AddRange(new[] { _toggleHelpersVerb, _moveFirstVerb, _movePrevVerb, _moveNextVerb, _moveLastVerb, _addGroupVerb, _clearGroupsVerb, _deleteTabVerb });
        }

        var moveFirst = false;
        var movePrev = false;
        var moveNext = false;
        var moveLast = false;
        var clearGroups = false;

        if ((_ribbonTab?.Ribbon is not null) && _ribbonTab.Ribbon.RibbonTabs.Contains(_ribbonTab))
        {
            moveFirst = _ribbonTab.Ribbon.RibbonTabs.IndexOf(_ribbonTab) > 0;
            movePrev = _ribbonTab.Ribbon.RibbonTabs.IndexOf(_ribbonTab) > 0;
            moveNext = _ribbonTab.Ribbon.RibbonTabs.IndexOf(_ribbonTab) < (_ribbonTab.Ribbon.RibbonTabs.Count - 1);
            moveLast = _ribbonTab.Ribbon.RibbonTabs.IndexOf(_ribbonTab) < (_ribbonTab.Ribbon.RibbonTabs.Count - 1);
            clearGroups = _ribbonTab.Groups.Count > 0;
        }

        _moveFirstVerb.Enabled = moveFirst;
        _movePrevVerb.Enabled = movePrev;
        _moveNextVerb.Enabled = moveNext;
        _moveLastVerb.Enabled = moveLast;
        _clearGroupsVerb.Enabled = clearGroups;
    }

    private void OnToggleHelpers(object? sender, EventArgs e)
    {
        // Invert the current toggle helper mode
        if (_ribbonTab?.Ribbon != null)
        {
            _ribbonTab.Ribbon.InDesignHelperMode = !_ribbonTab.Ribbon.InDesignHelperMode;
        }
    }

    private void OnMoveFirst(object? sender, EventArgs e)
    {
        if ((_ribbonTab?.Ribbon != null) && _ribbonTab.Ribbon.RibbonTabs.Contains(_ribbonTab))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonTab MoveFirst");

            try
            {
                // Get access to the RibbonTabs property
                MemberDescriptor? propertyTabs = TypeDescriptor.GetProperties(_ribbonTab.Ribbon)[@"RibbonTabs"];

                RaiseComponentChanging(propertyTabs);

                // Move position of the tab
                KryptonRibbon ribbon = _ribbonTab.Ribbon;
                ribbon.RibbonTabs.Remove(_ribbonTab);
                ribbon.RibbonTabs.Insert(0, _ribbonTab);
                ribbon.SelectedTab = _ribbonTab;
                UpdateVerbStatus();

                RaiseComponentChanged(propertyTabs, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnMovePrevious(object? sender, EventArgs e)
    {
        if ((_ribbonTab is not null)
            && _ribbonTab.Ribbon != null
            && _ribbonTab.Ribbon.RibbonTabs.Contains(_ribbonTab))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonTab MoveNext");

            try
            {
                // Get access to the RibbonTabs property
                MemberDescriptor? propertyTabs = TypeDescriptor.GetProperties(_ribbonTab.Ribbon)[@"RibbonTabs"];

                RaiseComponentChanging(propertyTabs);

                // Move position of the tab
                KryptonRibbon ribbon = _ribbonTab.Ribbon;
                var index = ribbon.RibbonTabs.IndexOf(_ribbonTab) - 1;
                index = Math.Max(index, 0);
                ribbon.RibbonTabs.Remove(_ribbonTab);
                ribbon.RibbonTabs.Insert(index, _ribbonTab);
                ribbon.SelectedTab = _ribbonTab;
                UpdateVerbStatus();

                RaiseComponentChanged(propertyTabs, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnMoveNext(object? sender, EventArgs e)
    {
        if ((_ribbonTab is not null)
            && _ribbonTab.Ribbon != null
            && _ribbonTab.Ribbon.RibbonTabs.Contains(_ribbonTab))

        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonTab MovePrevious");

            try
            {
                // Get access to the RibbonTabs property
                MemberDescriptor? propertyTabs = TypeDescriptor.GetProperties(_ribbonTab.Ribbon)[@"RibbonTabs"];

                RaiseComponentChanging(propertyTabs);

                // Move position of the tab
                KryptonRibbon ribbon = _ribbonTab.Ribbon;
                var index = ribbon.RibbonTabs.IndexOf(_ribbonTab) + 1;
                index = Math.Min(index, ribbon.RibbonTabs.Count - 1);
                ribbon.RibbonTabs.Remove(_ribbonTab);
                ribbon.RibbonTabs.Insert(index, _ribbonTab);
                ribbon.SelectedTab = _ribbonTab;
                UpdateVerbStatus();

                RaiseComponentChanged(propertyTabs, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnMoveLast(object? sender, EventArgs e)
    {
        if ((_ribbonTab is not null)
            && _ribbonTab.Ribbon != null
            && _ribbonTab.Ribbon.RibbonTabs.Contains(_ribbonTab))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonTab MoveLast");

            try
            {
                // Get access to the RibbonTabs property
                MemberDescriptor? propertyTabs = TypeDescriptor.GetProperties(_ribbonTab.Ribbon)[@"RibbonTabs"];

                RaiseComponentChanging(propertyTabs);

                // Move position of the tab
                KryptonRibbon ribbon = _ribbonTab.Ribbon;
                ribbon.RibbonTabs.Remove(_ribbonTab);
                ribbon.RibbonTabs.Insert(ribbon.RibbonTabs.Count, _ribbonTab);
                ribbon.SelectedTab = _ribbonTab;
                UpdateVerbStatus();

                RaiseComponentChanged(propertyTabs, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnAddGroup(object? sender, EventArgs e)
    {
        if ((_ribbonTab is not null)
            && _ribbonTab.Ribbon != null
            && _ribbonTab.Ribbon.RibbonTabs.Contains(_ribbonTab))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonTab AddGroup");

            try
            {
                // Get access to the Groups property
                MemberDescriptor? propertyGroups = TypeDescriptor.GetProperties(_ribbonTab)[@"Groups"];

                RaiseComponentChanging(propertyGroups);

                // Get designer to create the new group component
                var group = (KryptonRibbonGroup)_designerHost.CreateComponent(typeof(KryptonRibbonGroup));
                _ribbonTab.Groups.Add(group);

                RaiseComponentChanged(propertyGroups, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnClearGroups(object? sender, EventArgs e)
    {
        if ((_ribbonTab is not null)
            && _ribbonTab.Ribbon != null
            && _ribbonTab.Ribbon.RibbonTabs.Contains(_ribbonTab))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonTab ClearGroups");

            try
            {
                // Get access to the Groups property
                MemberDescriptor? propertyGroups = TypeDescriptor.GetProperties(_ribbonTab)[@"Groups"];

                RaiseComponentChanging(propertyGroups);

                // Need access to host in order to delete a component
                var host = (IDesignerHost?)GetService(typeof(IDesignerHost)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("host"));

                // We need to remove all the groups from the tab
                for (var i = _ribbonTab.Groups.Count - 1; i >= 0; i--)
                {
                    KryptonRibbonGroup group = _ribbonTab.Groups[i];
                    _ribbonTab.Groups.Remove(group);
                    host.DestroyComponent(group);
                }

                RaiseComponentChanged(propertyGroups, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnDeleteTab(object? sender, EventArgs e)
    {
        if ((_ribbonTab is not null)
            && _ribbonTab.Ribbon != null
            && _ribbonTab.Ribbon.RibbonTabs.Contains(_ribbonTab))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonTab DeleteTab");

            try
            {
                // Get access to the RibbonTabs property
                MemberDescriptor? propertyTabs = TypeDescriptor.GetProperties(_ribbonTab.Ribbon)[@"RibbonTabs"];

                // Remove the ribbon tab from the ribbon
                RaiseComponentChanging(null);
                RaiseComponentChanging(propertyTabs);

                // Remove the page from the ribbon
                _ribbonTab.Ribbon.RibbonTabs.Remove(_ribbonTab);

                // Get designer to destroy it
                _designerHost.DestroyComponent(_ribbonTab);

                RaiseComponentChanged(propertyTabs, null, null);
                RaiseComponentChanged(null, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnVisible(object? sender, EventArgs e)
    {
        if ((_ribbonTab is not null)
            && _ribbonTab.Ribbon != null
            && _ribbonTab.Ribbon.RibbonTabs.Contains(_ribbonTab))
        {
            _changeService.OnComponentChanged(_ribbonTab, null, _ribbonTab.Visible, !_ribbonTab.Visible);
            _ribbonTab.Visible = !_ribbonTab.Visible;
        }
    }

    private void OnComponentChanged(object? sender, ComponentChangedEventArgs e) => UpdateVerbStatus();

    private void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        // If our tab is being removed
        if ( _ribbonTab is not null && e.Component == _ribbonTab)
        {
            // Need access to host in order to delete a component
            var host = (IDesignerHost?)GetService(typeof(IDesignerHost)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("host"));

            // We need to remove all the groups from the tab
            for (var i = _ribbonTab.Groups.Count - 1; i >= 0; i--)
            {
                KryptonRibbonGroup group = _ribbonTab.Groups[i];
                _ribbonTab.Groups.Remove(group);
                host.DestroyComponent(group);
            }
        }
    }

    private void OnContextMenu(object? sender, MouseEventArgs e)
    {
        if ((_ribbonTab is not null)
            && _ribbonTab.Ribbon != null
            && _ribbonTab.Ribbon.RibbonTabs.Contains(_ribbonTab))
        {
            // Create the menu strip the first time around
            if (_cms is null)
            {
                _cms = new ContextMenuStrip();
                _toggleHelpersMenu = new ToolStripMenuItem("Design Helpers", null, OnToggleHelpers);
                _visibleMenu = new ToolStripMenuItem("Visible", null, OnVisible);
                _moveFirstMenu = new ToolStripMenuItem("Move First", GenericImageResources.MoveFirst, OnMoveFirst);
                _movePreviousMenu = new ToolStripMenuItem("Move Previous", GenericImageResources.MovePrevious, OnMovePrevious);
                _moveNextMenu = new ToolStripMenuItem("Move Next", GenericImageResources.MoveNext, OnMoveNext);
                _moveLastMenu = new ToolStripMenuItem("Move Last", GenericImageResources.MoveLast, OnMoveLast);
                _addGroupMenu = new ToolStripMenuItem("Add Group", GenericImageResources.KryptonRibbonGroup, OnAddGroup);
                _clearGroupsMenu = new ToolStripMenuItem("Clear Groups", null, OnClearGroups);
                _deleteTabMenu = new ToolStripMenuItem("Delete Tab", GenericImageResources.Delete, OnDeleteTab);
                _cms.Items.AddRange(new ToolStripItem[] { _toggleHelpersMenu, new ToolStripSeparator(),
                    _visibleMenu, new ToolStripSeparator(),
                    _moveFirstMenu, _movePreviousMenu, _moveNextMenu, _moveLastMenu, new ToolStripSeparator(),
                    _addGroupMenu, new ToolStripSeparator(),
                    _clearGroupsMenu, new ToolStripSeparator(),
                    _deleteTabMenu });

                _addGroupMenu.ImageTransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
            }

            // Update verbs to work out correct enable states
            UpdateVerbStatus();

            // Update menu items state from verbs
            _toggleHelpersMenu.Checked = _ribbonTab.Ribbon.InDesignHelperMode;
            _visibleMenu.Checked = _ribbonTab.Visible;
            _moveFirstMenu.Enabled = _moveFirstVerb.Enabled;
            _movePreviousMenu.Enabled = _movePrevVerb.Enabled;
            _moveNextMenu.Enabled = _moveNextVerb.Enabled;
            _moveLastMenu.Enabled = _moveLastVerb.Enabled;
            _clearGroupsMenu.Enabled = _clearGroupsVerb.Enabled;

            // Convert from ribbon to screen coordinates
            if (CommonHelper.ValidContextMenuStrip(_cms))
            {
                Point screenPt = _ribbonTab.Ribbon.ViewRectangleToPoint(_ribbonTab.TabView);
                VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, screenPt);
            }
        }
    }
    #endregion
}