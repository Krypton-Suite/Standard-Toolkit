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

internal class KryptonRibbonGroupDateTimePickerDesigner : ComponentDesigner, IKryptonDesignObject
{
    #region Instance Fields
    private IDesignerHost _designerHost;
    private IComponentChangeService _changeService;
    private KryptonRibbonGroupDateTimePicker _ribbonDateTimePicker;
    private DesignerVerbCollection _verbs;
    private DesignerVerb _toggleHelpersVerb;
    private DesignerVerb _moveFirstVerb;
    private DesignerVerb _movePrevVerb;
    private DesignerVerb _moveNextVerb;
    private DesignerVerb _moveLastVerb;
    private DesignerVerb _deleteDateTimePickerVerb;
    private ContextMenuStrip? _cms;
    private ToolStripMenuItem _toggleHelpersMenu;
    private ToolStripMenuItem _visibleMenu;
    private ToolStripMenuItem _moveFirstMenu;
    private ToolStripMenuItem _movePreviousMenu;
    private ToolStripMenuItem _moveNextMenu;
    private ToolStripMenuItem _moveLastMenu;
    private ToolStripMenuItem _deleteDateTimePickerMenu;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRibbonGroupDateTimePickerDesigner class.
    /// </summary>
    public KryptonRibbonGroupDateTimePickerDesigner()
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
        _ribbonDateTimePicker = component as KryptonRibbonGroupDateTimePicker ?? throw new ArgumentNullException(nameof(component));

        if (_ribbonDateTimePicker is not null && _ribbonDateTimePicker.DateTimePicker is not null)
        {
            _ribbonDateTimePicker.DateTimePickerDesigner = this;

            // Update designer properties with actual starting values
            Visible = _ribbonDateTimePicker.Visible;
            Enabled = _ribbonDateTimePicker.Enabled;

            // Update visible/enabled to always be showing/enabled at design time
            _ribbonDateTimePicker.Visible = true;
            _ribbonDateTimePicker.Enabled = true;

            // Tell the embedded text box it is in design mode
            _ribbonDateTimePicker.DateTimePicker.InRibbonDesignMode = true;

            // Hook into events
            _ribbonDateTimePicker.DesignTimeContextMenu += OnContextMenu;
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
                _ribbonDateTimePicker.DesignTimeContextMenu -= OnContextMenu;
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
                properties[strArray[i]] = TypeDescriptor.CreateProperty(typeof(KryptonRibbonGroupDateTimePickerDesigner), descrip, attributes);
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
            _moveFirstVerb = new DesignerVerb(@"Move DateTimePicker First", OnMoveFirst);
            _movePrevVerb = new DesignerVerb(@"Move DateTimePicker Previous", OnMovePrevious);
            _moveNextVerb = new DesignerVerb(@"Move DateTimePicker Next", OnMoveNext);
            _moveLastVerb = new DesignerVerb(@"Move DateTimePicker Last", OnMoveLast);
            _deleteDateTimePickerVerb = new DesignerVerb(@"Delete DateTimePicker", OnDeleteDateTimePicker);
            _verbs.AddRange(new[] { _toggleHelpersVerb, _moveFirstVerb, _movePrevVerb,
                _moveNextVerb, _moveLastVerb, _deleteDateTimePickerVerb });
        }

        var moveFirst = false;
        var movePrev = false;
        var moveNext = false;
        var moveLast = false;

        if (_ribbonDateTimePicker.Ribbon != null)
        {
            var items = ParentItems ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));
            moveFirst = items.IndexOf(_ribbonDateTimePicker) > 0;
            movePrev = items.IndexOf(_ribbonDateTimePicker) > 0;
            moveNext = items.IndexOf(_ribbonDateTimePicker) < (items.Count - 1);
            moveLast = items.IndexOf(_ribbonDateTimePicker) < (items.Count - 1);
        }

        _moveFirstVerb.Enabled = moveFirst;
        _movePrevVerb.Enabled = movePrev;
        _moveNextVerb.Enabled = moveNext;
        _moveLastVerb.Enabled = moveLast;
    }

    private void OnToggleHelpers(object? sender, EventArgs e)
    {
        // Invert the current toggle helper mode
        if (_ribbonDateTimePicker.Ribbon != null)
        {
            _ribbonDateTimePicker.Ribbon.InDesignHelperMode = !_ribbonDateTimePicker.Ribbon.InDesignHelperMode;
        }
    }

    private void OnMoveFirst(object? sender, EventArgs e)
    {
        if (_ribbonDateTimePicker.Ribbon is not null)
        {
            // Get access to the parent collection of items
            var items = ParentItems ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupDateTimePicker MoveFirst");

            try
            {
                if (_ribbonDateTimePicker.RibbonContainer is not null)
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonDateTimePicker.RibbonContainer)[@"Items"]!;

                    RaiseComponentChanging(propertyItems);

                    // Move position of the date time picker
                    items.Remove(_ribbonDateTimePicker);
                    items.Insert(0, _ribbonDateTimePicker);
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
        if (_ribbonDateTimePicker.Ribbon != null)
        {
            // Get access to the parent collection of items
            var items = ParentItems ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupDateTimePicker MovePrevious");

            try
            {
                if (_ribbonDateTimePicker.RibbonContainer is not null)
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonDateTimePicker.RibbonContainer)[@"Items"]!;

                    RaiseComponentChanging(propertyItems);

                    // Move position of the date time picker
                    var index = items.IndexOf(_ribbonDateTimePicker) - 1;
                    index = Math.Max(index, 0);
                    items.Remove(_ribbonDateTimePicker);
                    items.Insert(index, _ribbonDateTimePicker);
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
        if (_ribbonDateTimePicker.Ribbon != null)
        {
            // Get access to the parent collection of items
            var items = ParentItems ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupDateTimePicker MoveNext");

            try
            {
                if (_ribbonDateTimePicker.RibbonContainer is not null)
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonDateTimePicker.RibbonContainer)[@"Items"]!;

                    RaiseComponentChanging(propertyItems);

                    // Move position of the date time picker
                    var index = items.IndexOf(_ribbonDateTimePicker) + 1;
                    index = Math.Min(index, items.Count - 1);
                    items.Remove(_ribbonDateTimePicker);
                    items.Insert(index, _ribbonDateTimePicker);
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
        if (_ribbonDateTimePicker.Ribbon != null)
        {
            // Get access to the parent collection of items
            var items = ParentItems ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupDateTimePicker MoveLast");

            try
            {
                if (_ribbonDateTimePicker.RibbonContainer is not null)
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonDateTimePicker.RibbonContainer)[@"Items"]!;

                    RaiseComponentChanging(propertyItems);

                    // Move position of the date time picker
                    items.Remove(_ribbonDateTimePicker);
                    items.Insert(items.Count, _ribbonDateTimePicker);
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

    private void OnDeleteDateTimePicker(object? sender, EventArgs e)
    {
        if (_ribbonDateTimePicker.Ribbon is not null)
        {
            // Get access to the parent collection of items
            var items = ParentItems ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupDateTimePicker DeleteDateTimePicker");

            try
            {
                if (_ribbonDateTimePicker.RibbonContainer is not null)
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonDateTimePicker.RibbonContainer)[@"Items"]!;

                    RaiseComponentChanging(null);
                    RaiseComponentChanging(propertyItems);

                    // Remove the date time picker from the group
                    items.Remove(_ribbonDateTimePicker);

                    // Get designer to destroy it
                    _designerHost.DestroyComponent(_ribbonDateTimePicker);

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
        if (_ribbonDateTimePicker.Ribbon != null)
        {
            PropertyDescriptor propertyEnabled = TypeDescriptor.GetProperties(_ribbonDateTimePicker)[nameof(Enabled)] ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("propertyEnabled"));
            var oldValue = (bool?)propertyEnabled.GetValue(_ribbonDateTimePicker);
            var newValue = !oldValue;
            _changeService.OnComponentChanged(_ribbonDateTimePicker, null, oldValue, newValue);
            propertyEnabled.SetValue(_ribbonDateTimePicker, newValue);
        }
    }

    private void OnVisible(object? sender, EventArgs e)
    {
        if (_ribbonDateTimePicker.Ribbon != null)
        {
            PropertyDescriptor propertyVisible = TypeDescriptor.GetProperties(_ribbonDateTimePicker)[nameof(Visible)] ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("propertyVisible"));
            var oldValue = (bool?)propertyVisible.GetValue(_ribbonDateTimePicker);
            var newValue = !oldValue;
            _changeService.OnComponentChanged(_ribbonDateTimePicker, null, oldValue, newValue);
            propertyVisible.SetValue(_ribbonDateTimePicker, newValue);
        }
    }

    private void OnComponentChanged(object? sender, ComponentChangedEventArgs e) => UpdateVerbStatus();

    private void OnContextMenu(object? sender, MouseEventArgs e)
    {
        if (_ribbonDateTimePicker.Ribbon != null)
        {
            // Create the menu strip the first time around
            if (_cms == null)
            {
                _cms = new ContextMenuStrip();
                _toggleHelpersMenu = new ToolStripMenuItem("Design Helpers", null, OnToggleHelpers);
                _visibleMenu = new ToolStripMenuItem("Visible", null, OnVisible);
                _moveFirstMenu = new ToolStripMenuItem("Move DateTimePicker First", GenericImageResources.MoveFirst, OnMoveFirst);
                _movePreviousMenu = new ToolStripMenuItem("Move DateTimePicker Previous", GenericImageResources.MovePrevious, OnMovePrevious);
                _moveNextMenu = new ToolStripMenuItem("Move DateTimePicker Next", GenericImageResources.MoveNext, OnMoveNext);
                _moveLastMenu = new ToolStripMenuItem("Move DateTimePicker Last", GenericImageResources.MoveLast, OnMoveLast);
                _deleteDateTimePickerMenu = new ToolStripMenuItem("Delete DateTimePicker", GenericImageResources.Delete, OnDeleteDateTimePicker);
                _cms.Items.AddRange(new ToolStripItem[] { _toggleHelpersMenu, new ToolStripSeparator(),
                    _visibleMenu, new ToolStripSeparator(),
                    _moveFirstMenu, _movePreviousMenu, _moveNextMenu, _moveLastMenu, new ToolStripSeparator(),
                    _deleteDateTimePickerMenu });
            }

            // Update verbs to work out correct enable states
            UpdateVerbStatus();

            // Update menu items state from versb
            _toggleHelpersMenu.Checked = _ribbonDateTimePicker.Ribbon.InDesignHelperMode;
            _visibleMenu.Checked = Visible;
            _moveFirstMenu.Enabled = _moveFirstVerb.Enabled;
            _movePreviousMenu.Enabled = _movePrevVerb.Enabled;
            _moveNextMenu.Enabled = _moveNextVerb.Enabled;
            _moveLastMenu.Enabled = _moveLastVerb.Enabled;

            // Show the context menu
            if (CommonHelper.ValidContextMenuStrip(_cms))
            {
                Point screenPt = _ribbonDateTimePicker.Ribbon.ViewRectangleToPoint(_ribbonDateTimePicker.DateTimePickerView);
                VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, screenPt);
            }
        }
    }

    private TypedRestrictCollection<KryptonRibbonGroupItem>? ParentItems
    {
        get
        {
            switch (_ribbonDateTimePicker.RibbonContainer)
            {
                case KryptonRibbonGroupTriple triple:
                    return triple.Items;
                case KryptonRibbonGroupLines lines:
                    return lines.Items;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(_ribbonDateTimePicker.RibbonContainer!.ToString());
                    return null;
            }
        }
    }
    #endregion
}