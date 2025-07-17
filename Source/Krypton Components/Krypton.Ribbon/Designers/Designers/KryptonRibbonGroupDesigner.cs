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

internal class KryptonRibbonGroupDesigner : ComponentDesigner
{
    #region Instance Fields
    private IDesignerHost _designerHost;
    private IComponentChangeService _changeService;
    private KryptonRibbonGroup _ribbonGroup;
    private DesignerVerbCollection _verbs;
    private DesignerVerb _toggleHelpersVerb;
    private DesignerVerb _moveFirstVerb;
    private DesignerVerb _movePrevVerb;
    private DesignerVerb _moveNextVerb;
    private DesignerVerb _moveLastVerb;
    private DesignerVerb _addTripleVerb;
    private DesignerVerb _addLinesVerb;
    private DesignerVerb _addSepVerb;
    private DesignerVerb _addGalleryVerb;
    private DesignerVerb _clearItemsVerb;
    private DesignerVerb _deleteGroupVerb;
    private ContextMenuStrip? _cms;
    private ToolStripMenuItem _toggleHelpersMenu;
    private ToolStripMenuItem _visibleMenu;
    private ToolStripMenuItem _collapsableMenu;
    private ToolStripMenuItem _dialogLauncherMenu;
    private ToolStripMenuItem _moveFirstMenu;
    private ToolStripMenuItem _movePreviousMenu;
    private ToolStripMenuItem _moveNextMenu;
    private ToolStripMenuItem _moveLastMenu;
    private ToolStripMenuItem _moveToTabMenu;
    private ToolStripMenuItem _addTripleMenu;
    private ToolStripMenuItem _addLinesMenu;
    private ToolStripMenuItem _addSeparatorMenu;
    private ToolStripMenuItem _addGalleryMenu;
    private ToolStripMenuItem _clearItemsMenu;
    private ToolStripMenuItem _deleteGroupMenu;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRibbonGroupDesigner class.
    /// </summary>
    public KryptonRibbonGroupDesigner()
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
        _ribbonGroup = component as KryptonRibbonGroup ?? throw new ArgumentNullException(nameof(component));
        if (_ribbonGroup is not null)
        {
            _ribbonGroup.DesignTimeAddTriple += OnAddTriple;
            _ribbonGroup.DesignTimeAddLines += OnAddLines;
            _ribbonGroup.DesignTimeAddSeparator += OnAddSep;
            _ribbonGroup.DesignTimeAddGallery += OnAddGallery;
            _ribbonGroup.DesignTimeContextMenu += OnContextMenu;
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
            compound.AddRange(_ribbonGroup.Items);
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
            return _verbs;
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
                // Unhook from events
                _ribbonGroup.DesignTimeAddTriple -= OnAddTriple;
                _ribbonGroup.DesignTimeAddLines -= OnAddLines;
                _ribbonGroup.DesignTimeAddSeparator -= OnAddSep;
                _ribbonGroup.DesignTimeAddGallery -= OnAddGallery;
                _ribbonGroup.DesignTimeContextMenu -= OnContextMenu;
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
        if (_verbs == null)
        {
            _verbs = [];
            _toggleHelpersVerb = new DesignerVerb(@"Toggle Helpers", OnToggleHelpers);
            _moveFirstVerb = new DesignerVerb(@"Move Group First", OnMoveFirst);
            _movePrevVerb = new DesignerVerb(@"Move Group Previous", OnMovePrevious);
            _moveNextVerb = new DesignerVerb(@"Move Group Next", OnMoveNext);
            _moveLastVerb = new DesignerVerb(@"Move Group Last", OnMoveLast);
            _addTripleVerb = new DesignerVerb(@"Add Triple", OnAddTriple);
            _addLinesVerb = new DesignerVerb(@"Add Lines", OnAddLines);
            _addSepVerb = new DesignerVerb(@"Add Separator", OnAddSep);
            _addGalleryVerb = new DesignerVerb(@"Add Gallery", OnAddGallery);
            _clearItemsVerb = new DesignerVerb(@"Clear Items", OnClearItems);
            _deleteGroupVerb = new DesignerVerb(@"Delete Group", OnDeleteGroup);
            _verbs.AddRange(new[] { _toggleHelpersVerb, _moveFirstVerb, _movePrevVerb, _moveNextVerb, _moveLastVerb,
                _addTripleVerb, _addLinesVerb, _addSepVerb, _addGalleryVerb, _clearItemsVerb, _deleteGroupVerb });
        }

        var moveFirst = false;
        var movePrev = false;
        var moveNext = false;
        var moveLast = false;
        var clearItems = false;

        if ((_ribbonGroup.Ribbon is not null) 
            && _ribbonGroup.RibbonTab is not null
            && _ribbonGroup.RibbonTab.Groups.Contains(_ribbonGroup))
        {
            moveFirst = _ribbonGroup.RibbonTab.Groups.IndexOf(_ribbonGroup) > 0;
            movePrev = _ribbonGroup.RibbonTab.Groups.IndexOf(_ribbonGroup) > 0;
            moveNext = _ribbonGroup.RibbonTab.Groups.IndexOf(_ribbonGroup) < (_ribbonGroup.RibbonTab.Groups.Count - 1);
            moveLast = _ribbonGroup.RibbonTab.Groups.IndexOf(_ribbonGroup) < (_ribbonGroup.RibbonTab.Groups.Count - 1);
            clearItems = _ribbonGroup.Items.Count > 0;
        }

        _moveFirstVerb.Enabled = moveFirst;
        _movePrevVerb.Enabled = movePrev;
        _moveNextVerb.Enabled = moveNext;
        _moveLastVerb.Enabled = moveLast;
        _clearItemsVerb.Enabled = clearItems;
    }

    private void OnToggleHelpers(object? sender, EventArgs e)
    {
        // Invert the current toggle helper mode
        if (_ribbonGroup.Ribbon != null)
        {
            _ribbonGroup.Ribbon.InDesignHelperMode = !_ribbonGroup.Ribbon.InDesignHelperMode;
        }
    }

    private void OnMoveFirst(object? sender, EventArgs e)
    {
        if ((_ribbonGroup.Ribbon is not null) 
            && _ribbonGroup.RibbonTab is not null
            && _ribbonGroup.RibbonTab.Groups.Contains(_ribbonGroup))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroup MoveFirst");

            try
            {
                // Get access to the Group property
                MemberDescriptor? propertyGroups = TypeDescriptor.GetProperties(_ribbonGroup.RibbonTab)[@"Groups"];

                RaiseComponentChanging(propertyGroups);

                // Move position of the group
                KryptonRibbonTab ribbonTab = _ribbonGroup.RibbonTab;
                ribbonTab.Groups.Remove(_ribbonGroup);
                ribbonTab.Groups.Insert(0, _ribbonGroup);
                UpdateVerbStatus();

                RaiseComponentChanged(propertyGroups, null, null);
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
        if ((_ribbonGroup.Ribbon is not null) 
            && _ribbonGroup.RibbonTab is not null
            && _ribbonGroup.RibbonTab.Groups.Contains(_ribbonGroup))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroup MovePrevious");

            try
            {
                // Get access to the Group property
                MemberDescriptor? propertyGroups = TypeDescriptor.GetProperties(_ribbonGroup.RibbonTab)[@"Groups"];

                RaiseComponentChanging(propertyGroups);

                // Move position of the group
                KryptonRibbonTab ribbonTab = _ribbonGroup.RibbonTab;
                var index = ribbonTab.Groups.IndexOf(_ribbonGroup) - 1;
                index = Math.Max(index, 0);
                ribbonTab.Groups.Remove(_ribbonGroup);
                ribbonTab.Groups.Insert(index, _ribbonGroup);
                UpdateVerbStatus();

                RaiseComponentChanged(propertyGroups, null, null);
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
        if ((_ribbonGroup.Ribbon is not null) 
            && _ribbonGroup.RibbonTab is not null
            && _ribbonGroup.RibbonTab.Groups.Contains(_ribbonGroup))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroup MoveNext");

            try
            {
                // Get access to the Group property
                MemberDescriptor? propertyGroups = TypeDescriptor.GetProperties(_ribbonGroup.RibbonTab)[@"Groups"];

                RaiseComponentChanging(propertyGroups);

                // Move position of the group
                KryptonRibbonTab ribbonTab = _ribbonGroup.RibbonTab;
                var index = ribbonTab.Groups.IndexOf(_ribbonGroup) + 1;
                index = Math.Min(index, ribbonTab.Groups.Count - 1);
                ribbonTab.Groups.Remove(_ribbonGroup);
                ribbonTab.Groups.Insert(index, _ribbonGroup);
                UpdateVerbStatus();

                RaiseComponentChanged(propertyGroups, null, null);
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
        if ((_ribbonGroup.Ribbon is not null) 
            && _ribbonGroup.RibbonTab is not null
            && _ribbonGroup.RibbonTab.Groups.Contains(_ribbonGroup))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroup MoveLast");

            try
            {
                // Get access to the Group property
                MemberDescriptor? propertyGroups = TypeDescriptor.GetProperties(_ribbonGroup.RibbonTab)[@"Groups"];

                RaiseComponentChanging(propertyGroups);

                // Move position of the group
                KryptonRibbonTab ribbonTab = _ribbonGroup.RibbonTab;
                ribbonTab.Groups.Remove(_ribbonGroup);
                ribbonTab.Groups.Insert(ribbonTab.Groups.Count, _ribbonGroup);
                UpdateVerbStatus();

                RaiseComponentChanged(propertyGroups, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnAddTriple(object? sender, EventArgs e)
    {
        if ((_ribbonGroup.Ribbon != null) 
            && _ribbonGroup.RibbonTab is not null
            && _ribbonGroup.RibbonTab.Groups.Contains(_ribbonGroup))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroup AddTriple");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonGroup)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create the new triple component
                var triple = (KryptonRibbonGroupTriple)_designerHost.CreateComponent(typeof(KryptonRibbonGroupTriple));
                _ribbonGroup.Items.Add(triple);

                // Get access to the Triple.Items property
                MemberDescriptor? propertyTripleItems = TypeDescriptor.GetProperties(triple)[@"Items"];

                RaiseComponentChanging(propertyTripleItems);

                // Get designer to create three new button components
                var button1 = (KryptonRibbonGroupButton)_designerHost.CreateComponent(typeof(KryptonRibbonGroupButton));
                var button2 = (KryptonRibbonGroupButton)_designerHost.CreateComponent(typeof(KryptonRibbonGroupButton));
                var button3 = (KryptonRibbonGroupButton)_designerHost.CreateComponent(typeof(KryptonRibbonGroupButton));
                triple.Items!.Add(button1);
                triple.Items.Add(button2);
                triple.Items.Add(button3);

                RaiseComponentChanged(propertyTripleItems, null, null);
                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnAddLines(object? sender, EventArgs e)
    {
        if ((_ribbonGroup.Ribbon is not null) 
            && _ribbonGroup.RibbonTab is not null
            && _ribbonGroup.RibbonTab.Groups.Contains(_ribbonGroup))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroup AddLines");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonGroup)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create the new lines component
                var lines = (KryptonRibbonGroupLines)_designerHost.CreateComponent(typeof(KryptonRibbonGroupLines)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("lines"));
                _ribbonGroup.Items.Add(lines);

                // Get access to the Lines.Items property
                MemberDescriptor? propertyLinesItems = TypeDescriptor.GetProperties(lines)[@"Items"];

                RaiseComponentChanging(propertyLinesItems);

                // Get designer to create three new button components
                if (lines.Items is not null)
                {
                    var button1 = (KryptonRibbonGroupButton)_designerHost.CreateComponent(typeof(KryptonRibbonGroupButton));
                    var button2 = (KryptonRibbonGroupButton)_designerHost.CreateComponent(typeof(KryptonRibbonGroupButton));
                    lines.Items.Add(button1);
                    lines.Items.Add(button2);
                }

                RaiseComponentChanged(propertyLinesItems, null, null);
                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnAddSep(object? sender, EventArgs e)
    {
        if ((_ribbonGroup.Ribbon is not null) 
            && _ribbonGroup.RibbonTab is not null
            && _ribbonGroup.RibbonTab.Groups.Contains(_ribbonGroup))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroup AddSep");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonGroup)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create the new separator component
                var sep = (KryptonRibbonGroupSeparator)_designerHost.CreateComponent(typeof(KryptonRibbonGroupSeparator));
                _ribbonGroup.Items.Add(sep);

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnAddGallery(object? sender, EventArgs e)
    {
        if ((_ribbonGroup.Ribbon is not null) 
            && _ribbonGroup.RibbonTab is not null
            && _ribbonGroup.RibbonTab.Groups.Contains(_ribbonGroup))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroup AddGallery");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonGroup)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create the new gallery component
                var gallery = (KryptonRibbonGroupGallery)_designerHost.CreateComponent(typeof(KryptonRibbonGroupGallery));
                _ribbonGroup.Items.Add(gallery);

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnClearItems(object? sender, EventArgs e)
    {
        if ((_ribbonGroup.Ribbon is not null) 
            && _ribbonGroup.RibbonTab is not null
            && _ribbonGroup.RibbonTab.Groups.Contains(_ribbonGroup))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroup ClearItems");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonGroup)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Need access to host in order to delete a component
                var host = (IDesignerHost?)GetService(typeof(IDesignerHost)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("host"));

                // We need to remove all the items from the tab
                for (var i = _ribbonGroup.Items.Count - 1; i >= 0; i--)
                {
                    KryptonRibbonGroupContainer item = _ribbonGroup.Items[i];
                    _ribbonGroup.Items.Remove(item);
                    host.DestroyComponent(item);
                }

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnDeleteGroup(object? sender, EventArgs e)
    {
        if ((_ribbonGroup.Ribbon is not null)
            && _ribbonGroup.RibbonTab is not null
            && _ribbonGroup.RibbonTab.Groups.Contains(_ribbonGroup))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroup DeleteGroup");

            try
            {
                // Get access to the Groups property
                MemberDescriptor? propertyGroups = TypeDescriptor.GetProperties(_ribbonGroup.RibbonTab)[@"Groups"];

                // Remove the ribbon tab from the ribbon
                RaiseComponentChanging(null);
                RaiseComponentChanging(propertyGroups);

                // Remove the page from the ribbon
                _ribbonGroup.RibbonTab.Groups.Remove(_ribbonGroup);

                // Get designer to destroy it
                _designerHost.DestroyComponent(_ribbonGroup);

                RaiseComponentChanged(propertyGroups, null, null);
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
        if ((_ribbonGroup.Ribbon is not null) 
            && _ribbonGroup.RibbonTab is not null
            && _ribbonGroup.RibbonTab.Groups.Contains(_ribbonGroup))
        {
            _changeService.OnComponentChanged(_ribbonGroup, null, _ribbonGroup.Visible, !_ribbonGroup.Visible);
            _ribbonGroup.Visible = !_ribbonGroup.Visible;
        }
    }

    private void OnCollapsable(object? sender, EventArgs e)
    {
        if ((_ribbonGroup.Ribbon is not null) 
            && _ribbonGroup.RibbonTab is not null
            && _ribbonGroup.RibbonTab.Groups.Contains(_ribbonGroup))
        {
            _changeService.OnComponentChanged(_ribbonGroup, null, _ribbonGroup.AllowCollapsed, !_ribbonGroup.AllowCollapsed);
            _ribbonGroup.AllowCollapsed = !_ribbonGroup.AllowCollapsed;
        }
    }

    private void OnDialogLauncher(object? sender, EventArgs e)
    {
        if ((_ribbonGroup.Ribbon is not null) 
            && _ribbonGroup.RibbonTab is not null
            && _ribbonGroup.RibbonTab.Groups.Contains(_ribbonGroup))
        {
            _changeService.OnComponentChanged(_ribbonGroup, null, _ribbonGroup.DialogBoxLauncher, !_ribbonGroup.DialogBoxLauncher);
            _ribbonGroup.DialogBoxLauncher = !_ribbonGroup.DialogBoxLauncher;
        }
    }

    private void OnComponentChanged(object? sender, ComponentChangedEventArgs e) => UpdateVerbStatus();

    private void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        // If our group is being removed
        if (e.Component == _ribbonGroup)
        {
            // Need access to host in order to delete a component
            var host = (IDesignerHost?)GetService(typeof(IDesignerHost)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("host"));

            // We need to remove all containers from the group
            for (var j = _ribbonGroup.Items.Count - 1; j >= 0; j--)
            {
                var item = _ribbonGroup.Items[j] as KryptonRibbonGroupContainer;
                _ribbonGroup.Items.Remove(item);
                host.DestroyComponent(item);
            }
        }
    }

    private void OnContextMenu(object? sender, MouseEventArgs e)
    {
        if ((_ribbonGroup.Ribbon is not null)
            && _ribbonGroup.RibbonTab is not null
            && _ribbonGroup.RibbonTab.Groups.Contains(_ribbonGroup))
        {
            // Create the menu strip the first time around
            if (_cms == null)
            {
                _cms = new ContextMenuStrip();
                _toggleHelpersMenu = new ToolStripMenuItem("Design Helpers", null, OnToggleHelpers);
                _visibleMenu = new ToolStripMenuItem("Visible", null, OnVisible);
                _collapsableMenu = new ToolStripMenuItem("Allow Collapsed", null, OnCollapsable);
                _dialogLauncherMenu = new ToolStripMenuItem("Dialog Launcher", null, OnDialogLauncher);
                _moveFirstMenu = new ToolStripMenuItem("Move Group First", GenericImageResources.MoveFirst, OnMoveFirst);
                _movePreviousMenu = new ToolStripMenuItem("Move Group Previous", GenericImageResources.MovePrevious, OnMovePrevious);
                _moveNextMenu = new ToolStripMenuItem("Move Group Next", GenericImageResources.MoveNext, OnMoveNext);
                _moveLastMenu = new ToolStripMenuItem("Move Group Last", GenericImageResources.MoveLast, OnMoveLast);
                _moveToTabMenu = new ToolStripMenuItem("Move Group To Tab");
                _addTripleMenu = new ToolStripMenuItem("Add Triple", GenericImageResources.KryptonRibbonGroupTriple, OnAddTriple);
                _addLinesMenu = new ToolStripMenuItem("Add Lines", GenericImageResources.KryptonRibbonGroupLines, OnAddLines);
                _addSeparatorMenu = new ToolStripMenuItem("Add Separator", GenericImageResources.KryptonRibbonGroupSeparator, OnAddSep);
                _addGalleryMenu = new ToolStripMenuItem("Add Gallery", GenericImageResources.KryptonGallery, OnAddGallery);
                _clearItemsMenu = new ToolStripMenuItem("Clear Items", null, OnClearItems);
                _deleteGroupMenu = new ToolStripMenuItem("Delete Group", GenericImageResources.Delete, OnDeleteGroup);
                _cms.Items.AddRange(new ToolStripItem[] { _toggleHelpersMenu, new ToolStripSeparator(),
                    _visibleMenu, _collapsableMenu, _dialogLauncherMenu, new ToolStripSeparator(),
                    _moveFirstMenu, _movePreviousMenu, _moveNextMenu, _moveLastMenu, new ToolStripSeparator(),
                    _moveToTabMenu, new ToolStripSeparator(),
                    _addTripleMenu, _addLinesMenu, _addSeparatorMenu, _addGalleryMenu, new ToolStripSeparator(),
                    _clearItemsMenu, new ToolStripSeparator(),
                    _deleteGroupMenu });

                _addTripleMenu.ImageTransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
                _addLinesMenu.ImageTransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
                _addSeparatorMenu.ImageTransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
                _addGalleryMenu.ImageTransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
            }

            // Update verbs to work out correct enable states
            UpdateVerbStatus();

            // Update sub menu options available for the 'Move To Tab'
            UpdateMoveToTab();

            // Update menu items state from verbs
            _toggleHelpersMenu.Checked = _ribbonGroup.Ribbon.InDesignHelperMode;
            _visibleMenu.Checked = _ribbonGroup.Visible;
            _collapsableMenu.Checked = _ribbonGroup.AllowCollapsed;
            _dialogLauncherMenu.Checked = _ribbonGroup.DialogBoxLauncher;
            _moveFirstMenu.Enabled = _moveFirstVerb.Enabled;
            _movePreviousMenu.Enabled = _movePrevVerb.Enabled;
            _moveNextMenu.Enabled = _moveNextVerb.Enabled;
            _moveLastMenu.Enabled = _moveLastVerb.Enabled;
            _moveToTabMenu.Enabled = _moveToTabMenu.DropDownItems.Count > 0;
            _clearItemsMenu.Enabled = _clearItemsVerb.Enabled;

            // Show the context menu
            if (CommonHelper.ValidContextMenuStrip(_cms))
            {
                Point screenPt = _ribbonGroup.Ribbon.ViewRectangleToPoint(_ribbonGroup.GroupView);
                VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, screenPt);
            }
        }
    }

    private void UpdateMoveToTab()
    {
        // Remove any existing child items
        _moveToTabMenu.DropDownItems.Clear();

        if (_ribbonGroup.Ribbon != null)
        {
            // Create a new item per tab in the ribbon control
            foreach (KryptonRibbonTab tab in _ribbonGroup.Ribbon.RibbonTabs)
            {
                // Cannot move to ourself, so ignore ourself
                if (tab != _ribbonGroup.RibbonTab)
                {
                    // Create menu item for the tab
                    var tabMenuItem = new ToolStripMenuItem
                    {
                        Text = tab.Text,
                        Tag = tab
                    };

                    // Hook into selection of the menu item
                    tabMenuItem.Click += OnMoveToTab;

                    // Add to end of the list of options
                    _moveToTabMenu.DropDownItems.Add(tabMenuItem);
                }
            }
        }
    }

    private void OnMoveToTab(object? sender, EventArgs e)
    {
        if ((_ribbonGroup.Ribbon is not null) 
            && _ribbonGroup.RibbonTab is not null
            && _ribbonGroup.RibbonTab.Groups.Contains(_ribbonGroup))
        {
            // Cast to correct type
            var tabMenuItem = sender as ToolStripMenuItem ?? throw new ArgumentNullException(nameof(sender));

            // Get access to the destination tab
            var destination = tabMenuItem.Tag as KryptonRibbonTab ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(tabMenuItem.Tag)));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroup MoveTabTo");

            try
            {
                // Get access to the Groups property
                MemberDescriptor? oldGroups = TypeDescriptor.GetProperties(_ribbonGroup.RibbonTab)[@"Groups"];
                MemberDescriptor? newGroups = TypeDescriptor.GetProperties(destination)[@"Groups"];
                MemberDescriptor? newGroupsTab = TypeDescriptor.GetProperties(_ribbonGroup.Ribbon)[@"RibbonTabs"];

                // Remove the ribbon tab from the ribbon
                RaiseComponentChanging(null);
                RaiseComponentChanging(oldGroups);
                RaiseComponentChanging(newGroups);
                RaiseComponentChanging(newGroupsTab);

                // Remove group from current group
                _ribbonGroup.RibbonTab.Groups.Remove(_ribbonGroup);

                // Append to the new destination group
                destination.Groups.Add(_ribbonGroup);

                RaiseComponentChanged(newGroupsTab, null, null);
                RaiseComponentChanged(newGroups, null, null);
                RaiseComponentChanged(oldGroups, null, null);
                RaiseComponentChanged(null, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }
    #endregion
}