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

internal class KryptonRibbonGroupLabelDesigner : ComponentDesigner
{
    #region Instance Fields
    private IDesignerHost _designerHost;
    private IComponentChangeService _changeService;
    private KryptonRibbonGroupLabel _ribbonLabel;
    private DesignerVerbCollection _verbs;
    private DesignerVerb _toggleHelpersVerb;
    private DesignerVerb _moveFirstVerb;
    private DesignerVerb _movePrevVerb;
    private DesignerVerb _moveNextVerb;
    private DesignerVerb _moveLastVerb;
    private DesignerVerb _deleteLabelVerb;
    private ContextMenuStrip? _cms;
    private ToolStripMenuItem _toggleHelpersMenu;
    private ToolStripMenuItem _visibleMenu;
    private ToolStripMenuItem _enabledMenu;
    private ToolStripMenuItem _moveFirstMenu;
    private ToolStripMenuItem _movePreviousMenu;
    private ToolStripMenuItem _moveNextMenu;
    private ToolStripMenuItem _moveLastMenu;
    private ToolStripMenuItem _deleteLabelMenu;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRibbonGroupLabelDesigner class.
    /// </summary>
    public KryptonRibbonGroupLabelDesigner()
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
        _ribbonLabel = component as KryptonRibbonGroupLabel ?? throw new ArgumentNullException(nameof(component));

        if (_ribbonLabel != null)
        {
            _ribbonLabel.DesignTimeContextMenu += OnContextMenu;
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
                _ribbonLabel.DesignTimeContextMenu -= OnContextMenu;
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
            _moveFirstVerb = new DesignerVerb(@"Move Label First", OnMoveFirst);
            _movePrevVerb = new DesignerVerb(@"Move Label Previous", OnMovePrevious);
            _moveNextVerb = new DesignerVerb(@"Move Label Next", OnMoveNext);
            _moveLastVerb = new DesignerVerb(@"Move Label Last", OnMoveLast);
            _deleteLabelVerb = new DesignerVerb(@"Delete Label", OnDeleteLabel);
            _verbs.AddRange(new[] { _toggleHelpersVerb, _moveFirstVerb, _movePrevVerb,
                _moveNextVerb, _moveLastVerb, _deleteLabelVerb });
        }

        var moveFirst = false;
        var movePrev = false;
        var moveNext = false;
        var moveLast = false;

        if (_ribbonLabel.Ribbon != null)
        {
            var items = ParentItems ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));

            moveFirst = items.IndexOf(_ribbonLabel) > 0;
            movePrev = items.IndexOf(_ribbonLabel) > 0;
            moveNext = items.IndexOf(_ribbonLabel) < (items.Count - 1);
            moveLast = items.IndexOf(_ribbonLabel) < (items.Count - 1);
        }

        _moveFirstVerb.Enabled = moveFirst;
        _movePrevVerb.Enabled = movePrev;
        _moveNextVerb.Enabled = moveNext;
        _moveLastVerb.Enabled = moveLast;
    }

    private void OnToggleHelpers(object? sender, EventArgs e)
    {
        // Invert the current toggle helper mode
        if (_ribbonLabel.Ribbon != null)
        {
            _ribbonLabel.Ribbon.InDesignHelperMode = !_ribbonLabel.Ribbon.InDesignHelperMode;
        }
    }

    private void OnMoveFirst(object? sender, EventArgs e)
    {
        if (_ribbonLabel.Ribbon is not null)
        {
            // Get access to the parent collection of items
            var items = ParentItems ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupLabel MoveFirst");

            try
            {
                if (_ribbonLabel.RibbonContainer is not null)
                {
                    // Get access to the Items property
                    MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonLabel.RibbonContainer)[@"Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the label
                    items.Remove(_ribbonLabel);
                    items.Insert(0, _ribbonLabel);
                    UpdateVerbStatus();

                    RaiseComponentChanged(propertyItems, null, null);
                }
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
        if (_ribbonLabel.Ribbon is not null)
        {
            // Get access to the parent collection of items
            var items = ParentItems ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupLabel MovePrevious");

            try
            {
                if (_ribbonLabel.RibbonContainer is not null)
                {
                    // Get access to the Items property
                    MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonLabel.RibbonContainer)[@"Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the triple
                    var index = items.IndexOf(_ribbonLabel) - 1;
                    index = Math.Max(index, 0);
                    items.Remove(_ribbonLabel);
                    items.Insert(index, _ribbonLabel);
                    UpdateVerbStatus();

                    RaiseComponentChanged(propertyItems, null, null);
                }
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
        if (_ribbonLabel.Ribbon is not null)
        {
            // Get access to the parent collection of items
            var items = ParentItems  ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupLabel MoveNext");

            try
            {
                if (_ribbonLabel.RibbonContainer is not null)
                {
                    // Get access to the Items property
                    MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonLabel.RibbonContainer)[@"Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the triple
                    var index = items.IndexOf(_ribbonLabel) + 1;
                    index = Math.Min(index, items.Count - 1);
                    items.Remove(_ribbonLabel);
                    items.Insert(index, _ribbonLabel);
                    UpdateVerbStatus();

                    RaiseComponentChanged(propertyItems, null, null);
                }
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
        if (_ribbonLabel.Ribbon is not null)
        {
            // Get access to the parent collection of items
            var items = ParentItems ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupLabel MoveLast");

            try
            {
                if (_ribbonLabel.RibbonContainer is not null)
                {
                    // Get access to the Items property
                    MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonLabel.RibbonContainer)[@"Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the triple
                    items.Remove(_ribbonLabel);
                    items.Insert(items.Count, _ribbonLabel);
                    UpdateVerbStatus();

                    RaiseComponentChanged(propertyItems, null, null);
                }
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnDeleteLabel(object? sender, EventArgs e)
    {
        if (_ribbonLabel.Ribbon is not null)
        {
            // Get access to the parent collection of items
            var items = ParentItems ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupLabel DeleteLabel");

            try
            {
                if (_ribbonLabel.RibbonContainer is not null)
                {
                    // Get access to the Items property
                    MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonLabel.RibbonContainer)[@"Items"];

                    // Remove the ribbon group from the ribbon tab
                    RaiseComponentChanging(null);
                    RaiseComponentChanging(propertyItems);

                    // Remove the label from the group
                    items.Remove(_ribbonLabel);

                    // Get designer to destroy it
                    _designerHost.DestroyComponent(_ribbonLabel);

                    RaiseComponentChanged(propertyItems, null, null);
                    RaiseComponentChanged(null, null, null);
                }
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
        if (_ribbonLabel.Ribbon is not null)
        {
            _changeService.OnComponentChanged(_ribbonLabel, null, _ribbonLabel.Visible, !_ribbonLabel.Visible);
            _ribbonLabel.Visible = !_ribbonLabel.Visible;
        }
    }

    private void OnEnabled(object? sender, EventArgs e)
    {
        if (_ribbonLabel.Ribbon is not null)
        {
            _changeService.OnComponentChanged(_ribbonLabel, null, _ribbonLabel.Enabled, !_ribbonLabel.Enabled);
            _ribbonLabel.Enabled = !_ribbonLabel.Enabled;
        }
    }

    private void OnComponentChanged(object? sender, ComponentChangedEventArgs e) => UpdateVerbStatus();

    private void OnContextMenu(object? sender, MouseEventArgs e)
    {
        if (_ribbonLabel.Ribbon is not null)
        {
            // Create the menu strip the first time around
            if (_cms == null)
            {
                _cms = new ContextMenuStrip();
                _toggleHelpersMenu = new ToolStripMenuItem("Design Helpers", null, OnToggleHelpers);
                _visibleMenu = new ToolStripMenuItem("Visible", null, OnVisible);
                _enabledMenu = new ToolStripMenuItem("Enabled", null, OnEnabled);
                _moveFirstMenu = new ToolStripMenuItem("Move Label First", GenericImageResources.MoveFirst, OnMoveFirst);
                _movePreviousMenu = new ToolStripMenuItem("Move Label Previous", GenericImageResources.MovePrevious, OnMovePrevious);
                _moveNextMenu = new ToolStripMenuItem("Move Label Next", GenericImageResources.MoveNext, OnMoveNext);
                _moveLastMenu = new ToolStripMenuItem("Move Label Last", GenericImageResources.MoveLast, OnMoveLast);
                _deleteLabelMenu = new ToolStripMenuItem("Delete Label", GenericImageResources.Delete, OnDeleteLabel);
                _cms.Items.AddRange(new ToolStripItem[] { _toggleHelpersMenu, new ToolStripSeparator(),
                    _visibleMenu, _enabledMenu, new ToolStripSeparator(),
                    _moveFirstMenu, _movePreviousMenu, _moveNextMenu, _moveLastMenu, new ToolStripSeparator(),
                    _deleteLabelMenu });
            }

            // Update verbs to work out correct enable states
            UpdateVerbStatus();

            // Update menu items state from versb
            _toggleHelpersMenu.Checked = _ribbonLabel.Ribbon.InDesignHelperMode;
            _visibleMenu.Checked = _ribbonLabel.Visible;
            _enabledMenu.Checked = _ribbonLabel.Enabled;
            _moveFirstMenu.Enabled = _moveFirstVerb.Enabled;
            _movePreviousMenu.Enabled = _movePrevVerb.Enabled;
            _moveNextMenu.Enabled = _moveNextVerb.Enabled;
            _moveLastMenu.Enabled = _moveLastVerb.Enabled;

            // Show the context menu
            if (CommonHelper.ValidContextMenuStrip(_cms))
            {
                Point screenPt = _ribbonLabel.Ribbon.ViewRectangleToPoint(_ribbonLabel.LabelView);
                VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, screenPt);
            }
        }
    }

    private TypedRestrictCollection<KryptonRibbonGroupItem>? ParentItems
    {
        get
        {
            switch (_ribbonLabel.RibbonContainer)
            {
                case KryptonRibbonGroupTriple triple:
                    return triple.Items;
                case KryptonRibbonGroupLines lines:
                    return lines.Items;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(_ribbonLabel.RibbonContainer!.ToString());
                    return null;
            }
        }
    }
    #endregion
}