#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

internal class KryptonRibbonGroupClusterDesigner : ComponentDesigner
{
    #region Instance Fields
    private IDesignerHost _designerHost;
    private IComponentChangeService _changeService;
    private KryptonRibbonGroupCluster? _ribbonCluster;
    private DesignerVerbCollection _verbs;
    private DesignerVerb _toggleHelpersVerb;
    private DesignerVerb _moveFirstVerb;
    private DesignerVerb _movePrevVerb;
    private DesignerVerb _moveNextVerb;
    private DesignerVerb _moveLastVerb;
    private DesignerVerb _addButtonVerb;
    private DesignerVerb _addColorButtonVerb;
    private DesignerVerb _clearItemsVerb;
    private DesignerVerb _deleteClusterVerb;
    private ContextMenuStrip? _cms;
    private ToolStripMenuItem _toggleHelpersMenu;
    private ToolStripMenuItem _visibleMenu;
    private ToolStripMenuItem _moveFirstMenu;
    private ToolStripMenuItem _movePreviousMenu;
    private ToolStripMenuItem _moveNextMenu;
    private ToolStripMenuItem _moveLastMenu;
    private ToolStripMenuItem _addButtonMenu;
    private ToolStripMenuItem _addColorButtonMenu;
    private ToolStripMenuItem _clearItemsMenu;
    private ToolStripMenuItem _deleteClusterMenu;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRibbonGroupClusterDesigner class.
    /// </summary>
    public KryptonRibbonGroupClusterDesigner()
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
        _ribbonCluster = component as KryptonRibbonGroupCluster;
        if (_ribbonCluster != null)
        {
            _ribbonCluster.DesignTimeAddButton += OnAddButton;
            _ribbonCluster.DesignTimeAddColorButton += OnAddColorButton;
            _ribbonCluster.DesignTimeContextMenu += OnContextMenu;
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
            compound.AddRange(_ribbonCluster!.Items);
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
                _ribbonCluster!.DesignTimeAddButton -= OnAddButton;
                _ribbonCluster.DesignTimeContextMenu -= OnContextMenu;
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
            _moveFirstVerb = new DesignerVerb(@"Move Cluster First", OnMoveFirst);
            _movePrevVerb = new DesignerVerb(@"Move Cluster Previous", OnMovePrevious);
            _moveNextVerb = new DesignerVerb(@"Move Cluster Next", OnMoveNext);
            _moveLastVerb = new DesignerVerb(@"Move Cluster Last", OnMoveLast);
            _addButtonVerb = new DesignerVerb(@"Add Button", OnAddButton);
            _addColorButtonVerb = new DesignerVerb(@"Add Color Button", OnAddColorButton);
            _clearItemsVerb = new DesignerVerb(@"Clear Items", OnClearItems);
            _deleteClusterVerb = new DesignerVerb(@"Delete Cluster", OnDeleteCluster);
            _verbs.AddRange(new[] { _toggleHelpersVerb, _moveFirstVerb, _movePrevVerb, _moveNextVerb, _moveLastVerb,
                _addButtonVerb, _addColorButtonVerb, _clearItemsVerb, _deleteClusterVerb });
        }

        var moveFirst = false;
        var movePrev = false;
        var moveNext = false;
        var moveLast = false;
        var clearItems = false;

        if (_ribbonCluster!.Ribbon != null)
        {
            // Cast container to the correct type
            var lines = _ribbonCluster.RibbonContainer as KryptonRibbonGroupLines;

            moveFirst = lines!.Items!.IndexOf(_ribbonCluster) > 0;
            movePrev = lines.Items!.IndexOf(_ribbonCluster) > 0;
            moveNext = lines.Items!.IndexOf(_ribbonCluster) < (lines.Items.Count - 1);
            moveLast = lines.Items!.IndexOf(_ribbonCluster) < (lines.Items.Count - 1);
            clearItems = _ribbonCluster.Items.Count > 0;
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
        if (_ribbonCluster!.Ribbon != null)
        {
            _ribbonCluster.Ribbon.InDesignHelperMode = !_ribbonCluster.Ribbon.InDesignHelperMode;
        }
    }

    private void OnMoveFirst(object? sender, EventArgs e)
    {
        if (_ribbonCluster!.Ribbon != null)
        {
            // Cast container to the correct type
            var lines = _ribbonCluster.RibbonContainer as KryptonRibbonGroupLines;

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupCluster MoveFirst");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(lines!)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the cluster
                lines!.Items!.Remove(_ribbonCluster);
                lines.Items!.Insert(0, _ribbonCluster);
                UpdateVerbStatus();

                RaiseComponentChanged(propertyItems, null, null);
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
        if (_ribbonCluster!.Ribbon != null)
        {
            // Cast container to the correct type
            var lines = _ribbonCluster.RibbonContainer as KryptonRibbonGroupLines;

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupCluster MovePrevious");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(lines!)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the cluster
                var index = lines!.Items!.IndexOf(_ribbonCluster) - 1;
                index = Math.Max(index, 0);
                lines.Items!.Remove(_ribbonCluster);
                lines.Items!.Insert(index, _ribbonCluster);
                UpdateVerbStatus();

                RaiseComponentChanged(propertyItems, null, null);
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
        if (_ribbonCluster!.Ribbon != null)
        {
            // Cast container to the correct type
            var lines = _ribbonCluster.RibbonContainer as KryptonRibbonGroupLines;

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupCluster MoveNext");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(lines!)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the cluster
                var index = lines!.Items!.IndexOf(_ribbonCluster) + 1;
                index = Math.Min(index, lines.Items.Count - 1);
                lines.Items!.Remove(_ribbonCluster);
                lines.Items!.Insert(index, _ribbonCluster);
                UpdateVerbStatus();

                RaiseComponentChanged(propertyItems, null, null);
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
        if (_ribbonCluster!.Ribbon != null)
        {
            // Cast container to the correct type
            var lines = _ribbonCluster.RibbonContainer as KryptonRibbonGroupLines;

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupCluster MoveLast");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(lines!)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the cluster
                lines!.Items!.Remove(_ribbonCluster);
                lines.Items!.Insert(lines.Items!.Count, _ribbonCluster);
                UpdateVerbStatus();

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnAddButton(object? sender, EventArgs e)
    {
        if (_ribbonCluster!.Ribbon != null)
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupCluster AddButton");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonCluster)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a cluster button item
                var button = (KryptonRibbonGroupClusterButton)_designerHost.CreateComponent(typeof(KryptonRibbonGroupClusterButton));
                _ribbonCluster.Items.Add(button);

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnAddColorButton(object? sender, EventArgs e)
    {
        if (_ribbonCluster!.Ribbon != null)
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupCluster AddColorButton");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonCluster)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a cluster color button item
                var button = (KryptonRibbonGroupClusterColorButton)_designerHost.CreateComponent(typeof(KryptonRibbonGroupClusterColorButton));
                _ribbonCluster.Items.Add(button);

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
        if (_ribbonCluster!.Ribbon != null)
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupCluster ClearItems");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonCluster)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Need access to host in order to delete a component
                var host = (IDesignerHost?)GetService(typeof(IDesignerHost)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("host"));

                // We need to remove all the buttons from the cluster group
                for (var i = _ribbonCluster.Items.Count - 1; i >= 0; i--)
                {
                    KryptonRibbonGroupItem item = _ribbonCluster.Items[i];
                    _ribbonCluster.Items.Remove(item);
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

    private void OnDeleteCluster(object? sender, EventArgs e)
    {
        if (_ribbonCluster!.Ribbon != null)
        {
            // Cast container to the correct type
            var lines = _ribbonCluster.RibbonContainer as KryptonRibbonGroupLines;

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupTriple DeleteTriple");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(lines!)[@"Items"];

                // Remove the ribbon group from the ribbon tab
                RaiseComponentChanging(null);
                RaiseComponentChanging(propertyItems);

                // Remove the cluster from the lines
                lines!.Items!.Remove(_ribbonCluster);

                // Get designer to destroy it
                _designerHost.DestroyComponent(_ribbonCluster);

                RaiseComponentChanged(propertyItems, null, null);
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
        if (_ribbonCluster!.Ribbon != null)
        {
            _changeService.OnComponentChanged(_ribbonCluster, null, _ribbonCluster.Visible, !_ribbonCluster.Visible);
            _ribbonCluster.Visible = !_ribbonCluster.Visible;
        }
    }

    private void OnComponentChanged(object? sender, ComponentChangedEventArgs e) => UpdateVerbStatus();

    private void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        // If our cluster is being removed
        if (e.Component == _ribbonCluster)
        {
            // Need access to host in order to delete a component
            var host = (IDesignerHost?)GetService(typeof(IDesignerHost)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("host"));

            // We need to remove all items from the cluster
            for (var j = _ribbonCluster!.Items.Count - 1; j >= 0; j--)
            {

                if (_ribbonCluster.Items[j] is IRibbonGroupItem item)
                {
                    _ribbonCluster.Items.Remove(item);
                    host.DestroyComponent((item as Component)!);
                }
                else
                {
                    var container = _ribbonCluster.Items[j] as IRibbonGroupContainer;
                    _ribbonCluster.Items.Remove(container!);
                    host.DestroyComponent((container as Component)!);
                }
            }
        }
    }

    private void OnContextMenu(object? sender, MouseEventArgs e)
    {
        if (_ribbonCluster!.Ribbon != null)
        {
            // Create the menu strip the first time around
            if (_cms == null)
            {
                _cms = new ContextMenuStrip();
                _toggleHelpersMenu = new ToolStripMenuItem("Design Helpers", null, OnToggleHelpers);
                _visibleMenu = new ToolStripMenuItem("Visible", null, OnVisible);
                _moveFirstMenu = new ToolStripMenuItem("Move Cluster First", GenericImageResources.MoveFirst, OnMoveFirst);
                _movePreviousMenu = new ToolStripMenuItem("Move Cluster Previous", GenericImageResources.MovePrevious, OnMovePrevious);
                _moveNextMenu = new ToolStripMenuItem("Move Cluster Next", GenericImageResources.MoveNext, OnMoveNext);
                _moveLastMenu = new ToolStripMenuItem("Move Cluster Last", GenericImageResources.MoveLast, OnMoveLast);
                _addButtonMenu = new ToolStripMenuItem("Add Button", GenericImageResources.KryptonRibbonGroupClusterButton, OnAddButton);
                _addColorButtonMenu = new ToolStripMenuItem("Add Color Button", GenericImageResources.KryptonRibbonGroupClusterColorButton, OnAddColorButton);
                _clearItemsMenu = new ToolStripMenuItem("Clear Items", null, OnClearItems);
                _deleteClusterMenu = new ToolStripMenuItem("Delete Cluster", GenericImageResources.Delete, OnDeleteCluster);
                _cms.Items.AddRange(new ToolStripItem[] { _toggleHelpersMenu, new ToolStripSeparator(),
                    _visibleMenu, new ToolStripSeparator(),
                    _moveFirstMenu, _movePreviousMenu, _moveNextMenu, _moveLastMenu, new ToolStripSeparator(),
                    _addButtonMenu, _addColorButtonMenu, new ToolStripSeparator(),
                    _clearItemsMenu, new ToolStripSeparator(),
                    _deleteClusterMenu });

                // Ensure add images have correct transparent background
                _addButtonMenu.ImageTransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
                _addColorButtonMenu.ImageTransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
            }

            // Update verbs to work out correct enable states
            UpdateVerbStatus();

            // Update menu items state from versb
            _toggleHelpersMenu.Checked = _ribbonCluster.Ribbon.InDesignHelperMode;
            _visibleMenu.Checked = _ribbonCluster.Visible;
            _moveFirstMenu.Enabled = _moveFirstVerb.Enabled;
            _movePreviousMenu.Enabled = _movePrevVerb.Enabled;
            _moveNextMenu.Enabled = _moveNextVerb.Enabled;
            _moveLastMenu.Enabled = _moveLastVerb.Enabled;
            _clearItemsMenu.Enabled = _clearItemsVerb.Enabled;

            // Show the context menu
            if (CommonHelper.ValidContextMenuStrip(_cms))
            {
                Point screenPt = _ribbonCluster.Ribbon.ViewRectangleToPoint(_ribbonCluster.ClusterView);
                VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, screenPt);
            }
        }
    }
    #endregion    
}