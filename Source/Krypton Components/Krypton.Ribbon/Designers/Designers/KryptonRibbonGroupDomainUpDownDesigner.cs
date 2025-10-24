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

internal class KryptonRibbonGroupDomainUpDownDesigner : ComponentDesigner, IKryptonDesignObject
{
    #region Instance Fields
    private IDesignerHost _designerHost;
    private IComponentChangeService _changeService;
    private KryptonRibbonGroupDomainUpDown _ribbonDomainUpDown;
    private DesignerVerbCollection _verbs;
    private DesignerVerb _toggleHelpersVerb;
    private DesignerVerb _moveFirstVerb;
    private DesignerVerb _movePrevVerb;
    private DesignerVerb _moveNextVerb;
    private DesignerVerb _moveLastVerb;
    private DesignerVerb _deleteDomainUpDownVerb;
    private ContextMenuStrip? _cms;
    private ToolStripMenuItem _toggleHelpersMenu;
    private ToolStripMenuItem _visibleMenu;
    private ToolStripMenuItem _moveFirstMenu;
    private ToolStripMenuItem _movePreviousMenu;
    private ToolStripMenuItem _moveNextMenu;
    private ToolStripMenuItem _moveLastMenu;
    private ToolStripMenuItem _deleteDomainUpDownMenu;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRibbonGroupDomainUpDownDesigner class.
    /// </summary>
    public KryptonRibbonGroupDomainUpDownDesigner()
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

        Debug.Assert(component is not null);

        // Cast to correct type
        _ribbonDomainUpDown = component as KryptonRibbonGroupDomainUpDown ?? throw new ArgumentNullException(nameof(component));

        if (_ribbonDomainUpDown is not null)
        {
            _ribbonDomainUpDown.DomainUpDownDesigner = this;

            // Update designer properties with actual starting values
            Visible = _ribbonDomainUpDown.Visible;
            Enabled = _ribbonDomainUpDown.Enabled;

            // Update visible/enabled to always be showing/enabled at design time
            _ribbonDomainUpDown.Visible = true;
            _ribbonDomainUpDown.Enabled = true;

            // Tell the embedded domain up-down control it is in design mode
            _ribbonDomainUpDown.DomainUpDown!.InRibbonDesignMode = true;

            // Hook into events
            _ribbonDomainUpDown.DesignTimeContextMenu += OnContextMenu;
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

    /// <summary>
    /// Gets and sets if the object is enabled.
    /// </summary>
    public bool DesignEnabled
    {
        get => Enabled;
        set => Enabled = value;
    }

    /// <summary>
    /// Gets and sets if the object is visible.
    /// </summary>
    public bool DesignVisible
    {
        get => Visible;
        set => Visible = value;
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
                _ribbonDomainUpDown.DesignTimeContextMenu -= OnContextMenu;
                _changeService.ComponentChanged -= OnComponentChanged;
            }
        }
        finally
        {
            // Must let base class do standard stuff
            base.Dispose(disposing);
        }
    }

    /// <summary>
    /// Adjusts the set of properties the component exposes through a TypeDescriptor.
    /// </summary>
    /// <param name="properties">An IDictionary containing the properties for the class of the component.</param>
    protected override void PreFilterProperties(IDictionary properties)
    {
        base.PreFilterProperties(properties);

        // Setup the array of properties we override
        var attributes = Array.Empty<Attribute>();
        string[] strArray = [nameof(Visible), nameof(Enabled)];

        // Adjust our list of properties
        for (var i = 0; i < strArray.Length; i++)
        {
            var descrip = (PropertyDescriptor?)properties[strArray[i]];
            if (descrip != null)
            {
                properties[strArray[i]] = TypeDescriptor.CreateProperty(typeof(KryptonRibbonGroupDomainUpDownDesigner), descrip, attributes);
            }
        }
    }
    #endregion

    #region Internal
    internal bool Visible { get; set; }

    internal bool Enabled { get; set; }

    #endregion

    #region Implementation
    private void ResetVisible() => Visible = true;

    private bool ShouldSerializeVisible() => !Visible;

    private void ResetEnabled() => Enabled = true;

    private bool ShouldSerializeEnabled() => !Enabled;

    private void UpdateVerbStatus()
    {
        // Create verbs first time around
        if (_verbs == null)
        {
            _verbs = [];
            _toggleHelpersVerb = new DesignerVerb(@"Toggle Helpers", OnToggleHelpers);
            _moveFirstVerb = new DesignerVerb(@"Move DomainUpDown First", OnMoveFirst);
            _movePrevVerb = new DesignerVerb(@"Move DomainUpDown Previous", OnMovePrevious);
            _moveNextVerb = new DesignerVerb(@"Move DomainUpDown Next", OnMoveNext);
            _moveLastVerb = new DesignerVerb(@"Move DomainUpDown Last", OnMoveLast);
            _deleteDomainUpDownVerb = new DesignerVerb(@"Delete DomainUpDown", OnDeleteDomainUpDown);
            _verbs.AddRange(new[] { _toggleHelpersVerb, _moveFirstVerb, _movePrevVerb,
                _moveNextVerb, _moveLastVerb, _deleteDomainUpDownVerb });
        }

        var moveFirst = false;
        var movePrev = false;
        var moveNext = false;
        var moveLast = false;

        if (_ribbonDomainUpDown.Ribbon is not null)
        {
            var items = ParentItems ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));
            moveFirst = items.IndexOf(_ribbonDomainUpDown) > 0;
            movePrev = items.IndexOf(_ribbonDomainUpDown) > 0;
            moveNext = items.IndexOf(_ribbonDomainUpDown) < (items.Count - 1);
            moveLast = items.IndexOf(_ribbonDomainUpDown) < (items.Count - 1);
        }

        _moveFirstVerb.Enabled = moveFirst;
        _movePrevVerb.Enabled = movePrev;
        _moveNextVerb.Enabled = moveNext;
        _moveLastVerb.Enabled = moveLast;
    }

    private void OnToggleHelpers(object? sender, EventArgs e)
    {
        // Invert the current toggle helper mode
        if (_ribbonDomainUpDown.Ribbon is not null)
        {
            _ribbonDomainUpDown.Ribbon.InDesignHelperMode = !_ribbonDomainUpDown.Ribbon.InDesignHelperMode;
        }
    }

    private void OnMoveFirst(object? sender, EventArgs e)
    {
        if (_ribbonDomainUpDown.Ribbon is not null)
        {
            // Get access to the parent collection of items
            var items = ParentItems ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupDomainUpDown MoveFirst");

            try
            {
                if (_ribbonDomainUpDown.RibbonContainer is not null)
                {
                    // Get access to the Items property
                    MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonDomainUpDown.RibbonContainer)[@"Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the numeric up-down
                    items.Remove(_ribbonDomainUpDown);
                    items.Insert(0, _ribbonDomainUpDown);
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
        if (_ribbonDomainUpDown.Ribbon is not null)
        {
            // Get access to the parent collection of items
            var items = ParentItems ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupDomainUpDown MovePrevious");

            try
            {
                if (_ribbonDomainUpDown.RibbonContainer is not null)
                {
                    // Get access to the Items property
                    MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonDomainUpDown.RibbonContainer)[@"Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the numeric up-down
                    var index = items.IndexOf(_ribbonDomainUpDown) - 1;
                    index = Math.Max(index, 0);
                    items.Remove(_ribbonDomainUpDown);
                    items.Insert(index, _ribbonDomainUpDown);
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
        if (_ribbonDomainUpDown.Ribbon is not null)
        {
            // Get access to the parent collection of items
            var items = ParentItems ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupDomainUpDown MoveNext");

            try
            {
                if (_ribbonDomainUpDown.RibbonContainer is not null)
                {
                    // Get access to the Items property
                    MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonDomainUpDown.RibbonContainer)[@"Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the numeric up-down
                    var index = items.IndexOf(_ribbonDomainUpDown) + 1;
                    index = Math.Min(index, items.Count - 1);
                    items.Remove(_ribbonDomainUpDown);
                    items.Insert(index, _ribbonDomainUpDown);
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
        if (_ribbonDomainUpDown.Ribbon is not null)
        {
            // Get access to the parent collection of items
            var items = ParentItems ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupDomainUpDown MoveLast");

            try
            {
                if (_ribbonDomainUpDown.RibbonContainer is not null)
                {
                    // Get access to the Items property
                    MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonDomainUpDown.RibbonContainer)[@"Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the numeric up-down
                    items.Remove(_ribbonDomainUpDown);
                    items.Insert(items.Count, _ribbonDomainUpDown);
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

    private void OnDeleteDomainUpDown(object? sender, EventArgs e)
    {
        if (_ribbonDomainUpDown.Ribbon is not null)
        {
            // Get access to the parent collection of items
            var items = ParentItems ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupDomainUpDown DeleteDomainUpDown");

            try
            {
                if (_ribbonDomainUpDown.RibbonContainer is not null)
                {
                    // Get access to the Items property
                    MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonDomainUpDown.RibbonContainer)[@"Items"];

                    RaiseComponentChanging(null);
                    RaiseComponentChanging(propertyItems);

                    // Remove the numeric up-down from the group
                    items.Remove(_ribbonDomainUpDown);

                    // Get designer to destroy it
                    _designerHost.DestroyComponent(_ribbonDomainUpDown);

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

    private void OnEnabled(object? sender, EventArgs e)
    {
        if (_ribbonDomainUpDown.Ribbon is not null)
        {
            PropertyDescriptor? propertyEnabled = TypeDescriptor.GetProperties(_ribbonDomainUpDown)[nameof(Enabled)] ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("propertyEnabled"));
            var oldValue = (bool?)propertyEnabled.GetValue(_ribbonDomainUpDown);
            var newValue = !oldValue;
            _changeService.OnComponentChanged(_ribbonDomainUpDown, null, oldValue, newValue);
            propertyEnabled.SetValue(_ribbonDomainUpDown, newValue);
        }
    }

    private void OnVisible(object? sender, EventArgs e)
    {
        if (_ribbonDomainUpDown.Ribbon is not null)
        {
            PropertyDescriptor? propertyVisible = TypeDescriptor.GetProperties(_ribbonDomainUpDown)[nameof(Visible)] ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("propertyVisible"));
            var oldValue = (bool?)propertyVisible.GetValue(_ribbonDomainUpDown);
            var newValue = !oldValue;
            _changeService.OnComponentChanged(_ribbonDomainUpDown, null, oldValue, newValue);
            propertyVisible.SetValue(_ribbonDomainUpDown, newValue);
        }
    }

    private void OnComponentChanged(object? sender, ComponentChangedEventArgs e) => UpdateVerbStatus();

    private void OnContextMenu(object? sender, MouseEventArgs e)
    {
        if (_ribbonDomainUpDown.Ribbon is not null)
        {
            // Create the menu strip the first time around
            if (_cms == null)
            {
                _cms = new ContextMenuStrip();
                _toggleHelpersMenu = new ToolStripMenuItem("Design Helpers", null, OnToggleHelpers);
                _visibleMenu = new ToolStripMenuItem("Visible", null, OnVisible);
                _moveFirstMenu = new ToolStripMenuItem("Move DomainUpDown First", GenericImageResources.MoveFirst, OnMoveFirst);
                _movePreviousMenu = new ToolStripMenuItem("Move DomainUpDown Previous", GenericImageResources.MovePrevious, OnMovePrevious);
                _moveNextMenu = new ToolStripMenuItem("Move DomainUpDown Next", GenericImageResources.MoveNext, OnMoveNext);
                _moveLastMenu = new ToolStripMenuItem("Move DomainUpDown Last", GenericImageResources.MoveLast, OnMoveLast);
                _deleteDomainUpDownMenu = new ToolStripMenuItem("Delete DomainUpDown", GenericImageResources.Delete, OnDeleteDomainUpDown);
                _cms.Items.AddRange(new ToolStripItem[] { _toggleHelpersMenu, new ToolStripSeparator(),
                    _visibleMenu, new ToolStripSeparator(),
                    _moveFirstMenu, _movePreviousMenu, _moveNextMenu, _moveLastMenu, new ToolStripSeparator(),
                    _deleteDomainUpDownMenu });
            }

            // Update verbs to work out correct enable states
            UpdateVerbStatus();

            // Update menu items state from versb
            _toggleHelpersMenu.Checked = _ribbonDomainUpDown.Ribbon.InDesignHelperMode;
            _visibleMenu.Checked = Visible;
            _moveFirstMenu.Enabled = _moveFirstVerb.Enabled;
            _movePreviousMenu.Enabled = _movePrevVerb.Enabled;
            _moveNextMenu.Enabled = _moveNextVerb.Enabled;
            _moveLastMenu.Enabled = _moveLastVerb.Enabled;

            // Show the context menu
            if (CommonHelper.ValidContextMenuStrip(_cms))
            {
                Point screenPt = _ribbonDomainUpDown.Ribbon.ViewRectangleToPoint(_ribbonDomainUpDown.DomainUpDownView);
                VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, screenPt);
            }
        }
    }

    private TypedRestrictCollection<KryptonRibbonGroupItem>? ParentItems
    {
        get
        {
            switch (_ribbonDomainUpDown.RibbonContainer)
            {
                case KryptonRibbonGroupTriple triple:
                    return triple.Items;
                case KryptonRibbonGroupLines lines:
                    return lines.Items;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(_ribbonDomainUpDown.RibbonContainer!.ToString());
                    return null;
            }
        }
    }
    #endregion
}