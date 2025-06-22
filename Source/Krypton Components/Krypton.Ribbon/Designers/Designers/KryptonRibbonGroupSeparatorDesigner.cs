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

internal class KryptonRibbonGroupSeparatorDesigner : ComponentDesigner
{
    #region Instance Fields
    private IDesignerHost _designerHost;
    private IComponentChangeService _changeService;
    private KryptonRibbonGroupSeparator _ribbonSeparator;
    private DesignerVerbCollection? _verbs;
    private DesignerVerb _toggleHelpersVerb;
    private DesignerVerb _moveFirstVerb;
    private DesignerVerb _movePrevVerb;
    private DesignerVerb _moveNextVerb;
    private DesignerVerb _moveLastVerb;
    private DesignerVerb _deleteSeparatorVerb;
    private ContextMenuStrip? _cms;
    private ToolStripMenuItem _toggleHelpersMenu;
    private ToolStripMenuItem _moveFirstMenu;
    private ToolStripMenuItem _movePreviousMenu;
    private ToolStripMenuItem _moveNextMenu;
    private ToolStripMenuItem _moveLastMenu;
    private ToolStripMenuItem _moveToGroupMenu;
    private ToolStripMenuItem _deleteSeparatorMenu;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRibbonGroupSeparatorDesigner class.
    /// </summary>
    public KryptonRibbonGroupSeparatorDesigner()
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
        _ribbonSeparator = component as KryptonRibbonGroupSeparator ?? throw new ArgumentNullException(nameof(component));

        if (_ribbonSeparator != null)
        {
            _ribbonSeparator.DesignTimeContextMenu += OnContextMenu;
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

            if (_verbs is null)
            {
                throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(_verbs)));
            }

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
                _ribbonSeparator.DesignTimeContextMenu -= OnContextMenu;
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
            _moveFirstVerb = new DesignerVerb(@"Move Separator First", OnMoveFirst);
            _movePrevVerb = new DesignerVerb(@"Move Separator Previous", OnMovePrevious);
            _moveNextVerb = new DesignerVerb(@"Move Separator Next", OnMoveNext);
            _moveLastVerb = new DesignerVerb(@"Move Separator Last", OnMoveLast);
            _deleteSeparatorVerb = new DesignerVerb(@"Delete Separator", OnDeleteSeparator);
            _verbs.AddRange(new[] { _toggleHelpersVerb, _moveFirstVerb, _movePrevVerb,
                _moveNextVerb, _moveLastVerb, _deleteSeparatorVerb });
        }

        if (_verbs != null)
        {
            var moveFirst = false;
            var movePrev = false;
            var moveNext = false;
            var moveLast = false;

            if ((_ribbonSeparator.Ribbon is not null)
                && _ribbonSeparator.RibbonGroup is not null
                && _ribbonSeparator.RibbonGroup.Items.Contains(_ribbonSeparator))
            {
                moveFirst = _ribbonSeparator.RibbonGroup.Items.IndexOf(_ribbonSeparator) > 0;
                movePrev = _ribbonSeparator.RibbonGroup.Items.IndexOf(_ribbonSeparator) > 0;
                moveNext = _ribbonSeparator.RibbonGroup.Items.IndexOf(_ribbonSeparator) < (_ribbonSeparator.RibbonGroup.Items.Count - 1);
                moveLast = _ribbonSeparator.RibbonGroup.Items.IndexOf(_ribbonSeparator) < (_ribbonSeparator.RibbonGroup.Items.Count - 1);
            }

            _moveFirstVerb.Enabled = moveFirst;
            _movePrevVerb.Enabled = movePrev;
            _moveNextVerb.Enabled = moveNext;
            _moveLastVerb.Enabled = moveLast;
        }
    }

    private void OnToggleHelpers(object? sender, EventArgs e)
    {
        // Invert the current toggle helper mode
        if (_ribbonSeparator.Ribbon != null)
        {
            _ribbonSeparator.Ribbon.InDesignHelperMode = !_ribbonSeparator.Ribbon.InDesignHelperMode;
        }
    }

    private void OnMoveFirst(object? sender, EventArgs e)
    {
        if ((_ribbonSeparator.Ribbon is not null) 
            && _ribbonSeparator.RibbonGroup is not null
            && _ribbonSeparator.RibbonGroup.Items.Contains(_ribbonSeparator))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupSeparator MoveFirst");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonSeparator.RibbonGroup)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the separator
                KryptonRibbonGroup ribbonGroup = _ribbonSeparator.RibbonGroup;
                ribbonGroup.Items.Remove(_ribbonSeparator);
                ribbonGroup.Items.Insert(0, _ribbonSeparator);
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
        if ((_ribbonSeparator.Ribbon is not null)
            && _ribbonSeparator.RibbonGroup is not null
            && _ribbonSeparator.RibbonGroup.Items.Contains(_ribbonSeparator))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupSeparator MovePrevious");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonSeparator.RibbonGroup)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the triple
                KryptonRibbonGroup ribbonGroup = _ribbonSeparator.RibbonGroup;
                var index = ribbonGroup.Items.IndexOf(_ribbonSeparator) - 1;
                index = Math.Max(index, 0);
                ribbonGroup.Items.Remove(_ribbonSeparator);
                ribbonGroup.Items.Insert(index, _ribbonSeparator);
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
        if ((_ribbonSeparator.Ribbon is not null)
            && _ribbonSeparator.RibbonGroup is not null
            && _ribbonSeparator.RibbonGroup.Items.Contains(_ribbonSeparator))

        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupSeparator MoveNext");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonSeparator.RibbonGroup)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the triple
                KryptonRibbonGroup ribbonGroup = _ribbonSeparator.RibbonGroup;
                var index = ribbonGroup.Items.IndexOf(_ribbonSeparator) + 1;
                index = Math.Min(index, ribbonGroup.Items.Count - 1);
                ribbonGroup.Items.Remove(_ribbonSeparator);
                ribbonGroup.Items.Insert(index, _ribbonSeparator);
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
        if ((_ribbonSeparator.Ribbon is not null)
            && _ribbonSeparator.RibbonGroup is not null
            && _ribbonSeparator.RibbonGroup.Items.Contains(_ribbonSeparator))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupSeparator MoveLast");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonSeparator.RibbonGroup)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the triple
                KryptonRibbonGroup ribbonGroup = _ribbonSeparator.RibbonGroup;
                ribbonGroup.Items.Remove(_ribbonSeparator);
                ribbonGroup.Items.Insert(ribbonGroup.Items.Count, _ribbonSeparator);
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

    private void OnDeleteSeparator(object? sender, EventArgs e)
    {
        if ((_ribbonSeparator.Ribbon is not null)
            && _ribbonSeparator.RibbonGroup is not null
            && _ribbonSeparator.RibbonGroup.Items.Contains(_ribbonSeparator))

        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupSeparator DeleteSeparator");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonSeparator.RibbonGroup)[@"Items"];

                // Remove the ribbon group from the ribbon tab
                RaiseComponentChanging(null);
                RaiseComponentChanging(propertyItems);

                // Remove the separator from the group
                _ribbonSeparator.RibbonGroup.Items.Remove(_ribbonSeparator);

                // Get designer to destroy it
                _designerHost.DestroyComponent(_ribbonSeparator);

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

    private void OnComponentChanged(object? sender, ComponentChangedEventArgs e) => UpdateVerbStatus();

    private void OnContextMenu(object? sender, MouseEventArgs e)
    {
        if ((_ribbonSeparator.Ribbon is not null)
            && _ribbonSeparator.RibbonGroup is not null
            && _ribbonSeparator.RibbonGroup.Items.Contains(_ribbonSeparator))
        {
            // Create the menu strip the first time around
            if (_cms == null)
            {
                _cms = new ContextMenuStrip();
                _toggleHelpersMenu = new ToolStripMenuItem("Design Helpers", null, OnToggleHelpers);
                _moveFirstMenu = new ToolStripMenuItem("Move Separator First", GenericImageResources.MoveFirst, OnMoveFirst);
                _movePreviousMenu = new ToolStripMenuItem("Move Separator Previous", GenericImageResources.MovePrevious, OnMovePrevious);
                _moveNextMenu = new ToolStripMenuItem("Move Separator Next", GenericImageResources.MoveNext, OnMoveNext);
                _moveLastMenu = new ToolStripMenuItem("Move Separator Last", GenericImageResources.MoveLast, OnMoveLast);
                _moveToGroupMenu = new ToolStripMenuItem("Move Separator To Group");
                _deleteSeparatorMenu = new ToolStripMenuItem("Delete Separator", GenericImageResources.Delete, OnDeleteSeparator);
                _cms.Items.AddRange(new ToolStripItem[] { _toggleHelpersMenu, new ToolStripSeparator(),
                    _moveFirstMenu, _movePreviousMenu, _moveNextMenu, _moveLastMenu, new ToolStripSeparator(),
                    _moveToGroupMenu, new ToolStripSeparator(),
                    _deleteSeparatorMenu });
            }

            // Update verbs to work out correct enable states
            UpdateVerbStatus();

            // Update sub menu options available for the 'Move To Group'
            UpdateMoveToGroup();

            // Update menu items state from versb
            _toggleHelpersMenu.Checked = _ribbonSeparator.Ribbon.InDesignHelperMode;
            _moveFirstMenu.Enabled = _moveFirstVerb.Enabled;
            _movePreviousMenu.Enabled = _movePrevVerb.Enabled;
            _moveNextMenu.Enabled = _moveNextVerb.Enabled;
            _moveLastMenu.Enabled = _moveLastVerb.Enabled;
            _moveToGroupMenu.Enabled = _moveToGroupMenu.DropDownItems.Count > 0;

            // Show the context menu
            if (CommonHelper.ValidContextMenuStrip(_cms))
            {
                Point screenPt = _ribbonSeparator.Ribbon.ViewRectangleToPoint(_ribbonSeparator.SeparatorView);
                VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, screenPt);
            }
        }
    }

    private void UpdateMoveToGroup()
    {
        // Remove any existing child items
        _moveToGroupMenu.DropDownItems.Clear();

        if (_ribbonSeparator.Ribbon is not null && _ribbonSeparator.RibbonTab is not null)
        {
            // Create a new item per group in the same ribbon tab
            foreach (KryptonRibbonGroup group in _ribbonSeparator.RibbonTab.Groups)
            {
                // Cannot move to ourself, so ignore outself
                if (group != _ribbonSeparator.RibbonGroup)
                {
                    // Create menu item for the group
                    var groupMenuItem = new ToolStripMenuItem
                    {
                        Text = $"{group.TextLine1} {group.TextLine2}",
                        Tag = group
                    };

                    // Hook into selection of the menu item
                    groupMenuItem.Click += OnMoveToGroup;

                    // Add to end of the list of options
                    _moveToGroupMenu.DropDownItems.Add(groupMenuItem);
                }
            }
        }
    }

    private void OnMoveToGroup(object? sender, EventArgs e)
    {
        if ((_ribbonSeparator.Ribbon is not null)
            && _ribbonSeparator.RibbonGroup is not null
            && _ribbonSeparator.RibbonGroup.Items.Contains(_ribbonSeparator))
        {
            // Cast to correct type
            var groupMenuItem = sender as ToolStripMenuItem ?? throw new ArgumentNullException(nameof(sender));

            // Get access to the destination tab
            var destination = groupMenuItem.Tag as KryptonRibbonGroup ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("destination"));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupSeparator MoveSeparatorToGroup");

            try
            {
                // Get access to the Groups property
                MemberDescriptor? oldItems = TypeDescriptor.GetProperties(_ribbonSeparator.RibbonGroup)[@"Items"];
                MemberDescriptor? newItems = TypeDescriptor.GetProperties(destination)[@"Items"];

                // Remove the ribbon tab from the ribbon
                RaiseComponentChanging(null);
                RaiseComponentChanging(oldItems);
                RaiseComponentChanging(newItems);

                // Remove group from current group
                _ribbonSeparator.RibbonGroup.Items.Remove(_ribbonSeparator);

                // Append to the new destination group
                destination.Items.Add(_ribbonSeparator);

                RaiseComponentChanged(newItems, null, null);
                RaiseComponentChanged(oldItems, null, null);
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