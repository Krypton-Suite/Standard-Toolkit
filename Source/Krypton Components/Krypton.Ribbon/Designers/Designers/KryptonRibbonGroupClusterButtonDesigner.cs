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

internal class KryptonRibbonGroupClusterButtonDesigner : ComponentDesigner
{
    #region Instance Fields
    private IDesignerHost _designerHost;
    private IComponentChangeService _changeService;
    private KryptonRibbonGroupClusterButton? _ribbonButton;
    private DesignerVerbCollection _verbs;
    private DesignerVerb _toggleHelpersVerb;
    private DesignerVerb _moveFirstVerb;
    private DesignerVerb _movePrevVerb;
    private DesignerVerb _moveNextVerb;
    private DesignerVerb _moveLastVerb;
    private DesignerVerb _deleteButtonVerb;
    private ContextMenuStrip? _cms;
    private ToolStripMenuItem _toggleHelpersMenu;
    private ToolStripMenuItem _visibleMenu;
    private ToolStripMenuItem _enabledMenu;
    private ToolStripMenuItem _checkedMenu;
    private ToolStripMenuItem _typeMenu;
    private ToolStripMenuItem _typePushMenu;
    private ToolStripMenuItem _typeCheckMenu;
    private ToolStripMenuItem _typeDropDownMenu;
    private ToolStripMenuItem _typeSplitMenu;
    private ToolStripMenuItem _moveFirstMenu;
    private ToolStripMenuItem _movePreviousMenu;
    private ToolStripMenuItem _moveNextMenu;
    private ToolStripMenuItem _moveLastMenu;
    private ToolStripMenuItem _deleteButtonMenu;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRibbonGroupClusterButtonDesigner class.
    /// </summary>
    public KryptonRibbonGroupClusterButtonDesigner()
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
        _ribbonButton = component as KryptonRibbonGroupClusterButton;
        if (_ribbonButton != null)
        {
            _ribbonButton.DesignTimeContextMenu += OnContextMenu;
        }

        // Get access to the services
        _designerHost = (IDesignerHost?)GetService(typeof(IDesignerHost)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(_designerHost)));
        _changeService = (IComponentChangeService?)GetService(typeof(IComponentChangeService)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(_changeService)));

        // We need to know when we are being removed/changed
        _changeService.ComponentChanged += OnComponentChanged;
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
                _ribbonButton!.DesignTimeContextMenu -= OnContextMenu;
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
            _moveFirstVerb = new DesignerVerb(@"Move Cluster Button First", OnMoveFirst);
            _movePrevVerb = new DesignerVerb(@"Move Cluster Button Previous", OnMovePrevious);
            _moveNextVerb = new DesignerVerb(@"Move Cluster Button Next", OnMoveNext);
            _moveLastVerb = new DesignerVerb(@"Move Cluster Button Last", OnMoveLast);
            _deleteButtonVerb = new DesignerVerb(@"Delete Cluster Button", OnDeleteButton);
            _verbs.AddRange(new[] { _toggleHelpersVerb, _moveFirstVerb, _movePrevVerb,
                _moveNextVerb, _moveLastVerb, _deleteButtonVerb });
        }

        var moveFirst = false;
        var movePrev = false;
        var moveNext = false;
        var moveLast = false;

        if (_ribbonButton?.Ribbon != null)
        {
            // Cast container to the correct type
            var cluster = _ribbonButton.RibbonContainer as KryptonRibbonGroupCluster;

            moveFirst = cluster!.Items.IndexOf(_ribbonButton) > 0;
            movePrev = cluster.Items.IndexOf(_ribbonButton) > 0;
            moveNext = cluster.Items.IndexOf(_ribbonButton) < (cluster.Items.Count - 1);
            moveLast = cluster.Items.IndexOf(_ribbonButton) < (cluster.Items.Count - 1);
        }

        _moveFirstVerb.Enabled = moveFirst;
        _movePrevVerb.Enabled = movePrev;
        _moveNextVerb.Enabled = moveNext;
        _moveLastVerb.Enabled = moveLast;
    }

    private void OnToggleHelpers(object? sender, EventArgs e)
    {
        // Invert the current toggle helper mode
        if (_ribbonButton?.Ribbon != null)
        {
            _ribbonButton.Ribbon.InDesignHelperMode = !_ribbonButton.Ribbon.InDesignHelperMode;
        }
    }

    private void OnMoveFirst(object? sender, EventArgs e)
    {
        if (_ribbonButton?.Ribbon != null)
        {
            // Cast container to the correct type
            var cluster = _ribbonButton.RibbonContainer as KryptonRibbonGroupCluster;

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupClusterButton MoveFirst");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(cluster!)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the button
                cluster?.Items.Remove(_ribbonButton);
                cluster?.Items.Insert(0, _ribbonButton);
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
        if (_ribbonButton?.Ribbon != null)
        {
            // Cast container to the correct type
            var cluster = _ribbonButton.RibbonContainer as KryptonRibbonGroupCluster;

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupClusterButton MovePrevious");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(cluster!)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the triple
                var index = cluster!.Items.IndexOf(_ribbonButton) - 1;
                index = Math.Max(index, 0);
                cluster?.Items.Remove(_ribbonButton);
                cluster?.Items.Insert(index, _ribbonButton);
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
        if (_ribbonButton?.Ribbon != null)
        {
            // Cast container to the correct type
            var cluster = _ribbonButton.RibbonContainer as KryptonRibbonGroupCluster;

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupClusterButton MoveNext");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(cluster!)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the triple
                var index = cluster!.Items.IndexOf(_ribbonButton) + 1;
                index = Math.Min(index, cluster.Items.Count - 1);
                cluster?.Items.Remove(_ribbonButton);
                cluster?.Items.Insert(index, _ribbonButton);
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
        if (_ribbonButton?.Ribbon != null)
        {
            // Cast container to the correct type
            var cluster = _ribbonButton.RibbonContainer as KryptonRibbonGroupCluster;

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupClusterButton MoveLast");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(cluster!)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the triple
                cluster?.Items.Remove(_ribbonButton);
                cluster?.Items.Insert(cluster.Items.Count, _ribbonButton);
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

    private void OnDeleteButton(object? sender, EventArgs e)
    {
        if (_ribbonButton?.Ribbon != null)
        {
            // Cast container to the correct type
            var cluster = _ribbonButton.RibbonContainer as KryptonRibbonGroupCluster;

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupClusterButton DeleteButton");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(cluster!)[@"Items"];

                // Remove the ribbon group from the ribbon tab
                RaiseComponentChanging(null);
                RaiseComponentChanging(propertyItems);

                // Remove the button from the group
                cluster?.Items.Remove(_ribbonButton);

                // Get designer to destroy it
                _designerHost.DestroyComponent(_ribbonButton);

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
        if (_ribbonButton?.Ribbon != null)
        {
            _changeService.OnComponentChanged(_ribbonButton, null, _ribbonButton.Visible, !_ribbonButton.Visible);
            _ribbonButton.Visible = !_ribbonButton.Visible;
        }
    }

    private void OnEnabled(object? sender, EventArgs e)
    {
        if (_ribbonButton?.Ribbon != null)
        {
            _changeService.OnComponentChanged(_ribbonButton, null, _ribbonButton.Enabled, !_ribbonButton.Enabled);
            _ribbonButton.Enabled = !_ribbonButton.Enabled;
        }
    }

    private void OnChecked(object? sender, EventArgs e)
    {
        if (_ribbonButton?.Ribbon != null)
        {
            _changeService.OnComponentChanged(_ribbonButton, null, _ribbonButton.Checked, !_ribbonButton.Checked);
            _ribbonButton.Checked = !_ribbonButton.Checked;
        }
    }

    private void OnTypePush(object? sender, EventArgs e)
    {
        if (_ribbonButton?.Ribbon != null)
        {
            _changeService.OnComponentChanged(_ribbonButton, null, _ribbonButton.ButtonType, GroupButtonType.Push);
            _ribbonButton.ButtonType = GroupButtonType.Push;
        }
    }

    private void OnTypeCheck(object? sender, EventArgs e)
    {
        if (_ribbonButton?.Ribbon != null)
        {
            _changeService.OnComponentChanged(_ribbonButton, null, _ribbonButton.ButtonType, GroupButtonType.Check);
            _ribbonButton.ButtonType = GroupButtonType.Check;
        }
    }

    private void OnTypeDropDown(object? sender, EventArgs e)
    {
        if (_ribbonButton?.Ribbon != null)
        {
            _changeService.OnComponentChanged(_ribbonButton, null, _ribbonButton.ButtonType, GroupButtonType.DropDown);
            _ribbonButton.ButtonType = GroupButtonType.DropDown;
        }
    }

    private void OnTypeSplit(object? sender, EventArgs e)
    {
        if (_ribbonButton?.Ribbon != null)
        {
            _changeService.OnComponentChanged(_ribbonButton, null, _ribbonButton.ButtonType, GroupButtonType.Split);
            _ribbonButton.ButtonType = GroupButtonType.Split;
        }
    }

    private void OnComponentChanged(object? sender, ComponentChangedEventArgs e) => UpdateVerbStatus();

    private void OnContextMenu(object? sender, MouseEventArgs e)
    {
        if (_ribbonButton?.Ribbon != null)
        {
            // Create the menu strip the first time around
            if (_cms == null)
            {
                _cms = new ContextMenuStrip();
                _toggleHelpersMenu = new ToolStripMenuItem("Design Helpers", null, OnToggleHelpers);
                _visibleMenu = new ToolStripMenuItem("Visible", null, OnVisible);
                _enabledMenu = new ToolStripMenuItem("Enabled", null, OnEnabled);
                _checkedMenu = new ToolStripMenuItem("Checked", null, OnChecked);
                _typePushMenu = new ToolStripMenuItem("Push", null, OnTypePush);
                _typeCheckMenu = new ToolStripMenuItem("Check", null, OnTypeCheck);
                _typeDropDownMenu = new ToolStripMenuItem("DropDown", null, OnTypeDropDown);
                _typeSplitMenu = new ToolStripMenuItem("Split", null, OnTypeSplit);
                _typeMenu = new ToolStripMenuItem(nameof(Type));
                _typeMenu.DropDownItems.AddRange(new ToolStripItem[] { _typePushMenu, _typeCheckMenu, _typeDropDownMenu, _typeSplitMenu });
                _moveFirstMenu = new ToolStripMenuItem("Move Cluster Button First", GenericImageResources.MoveFirst, OnMoveFirst);
                _movePreviousMenu = new ToolStripMenuItem("Move Cluster Button Previous", GenericImageResources.MovePrevious, OnMovePrevious);
                _moveNextMenu = new ToolStripMenuItem("Move Cluster Button Next", GenericImageResources.MoveNext, OnMoveNext);
                _moveLastMenu = new ToolStripMenuItem("Move Cluster Button Last", GenericImageResources.MoveLast, OnMoveLast);
                _deleteButtonMenu = new ToolStripMenuItem("Delete Cluster Button", GenericImageResources.Delete, OnDeleteButton);
                _cms.Items.AddRange(new ToolStripItem[] { _toggleHelpersMenu, new ToolStripSeparator(),
                    _visibleMenu, _enabledMenu, _checkedMenu, _typeMenu, new ToolStripSeparator(),
                    _moveFirstMenu, _movePreviousMenu, _moveNextMenu, _moveLastMenu, new ToolStripSeparator(),
                    _deleteButtonMenu });
            }

            // Update verbs to work out correct enable states
            UpdateVerbStatus();

            // Update menu items state from verbs
            _toggleHelpersMenu.Checked = _ribbonButton.Ribbon.InDesignHelperMode;
            _visibleMenu.Checked = _ribbonButton.Visible;
            _enabledMenu.Checked = _ribbonButton.Enabled;
            _checkedMenu.Checked = _ribbonButton.Checked;
            _typePushMenu.Checked = _ribbonButton.ButtonType == GroupButtonType.Push;
            _typeCheckMenu.Checked = _ribbonButton.ButtonType == GroupButtonType.Check;
            _typeDropDownMenu.Checked = _ribbonButton.ButtonType == GroupButtonType.DropDown;
            _typeSplitMenu.Checked = _ribbonButton.ButtonType == GroupButtonType.Split;
            _moveFirstMenu.Enabled = _moveFirstVerb.Enabled;
            _movePreviousMenu.Enabled = _movePrevVerb.Enabled;
            _moveNextMenu.Enabled = _moveNextVerb.Enabled;
            _moveLastMenu.Enabled = _moveLastVerb.Enabled;

            // Show the context menu
            if (CommonHelper.ValidContextMenuStrip(_cms))
            {
                Point screenPt = _ribbonButton.Ribbon.ViewRectangleToPoint(_ribbonButton.ClusterButtonView);
                VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, screenPt);
            }
        }
    }
    #endregion
}