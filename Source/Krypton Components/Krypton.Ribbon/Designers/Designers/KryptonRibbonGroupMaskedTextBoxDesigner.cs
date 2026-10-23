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

internal class KryptonRibbonGroupMaskedTextBoxDesigner : ComponentDesigner, IKryptonDesignObject
{
    #region Instance Fields
    private IDesignerHost _designerHost;
    private IComponentChangeService _changeService;
    private KryptonRibbonGroupMaskedTextBox _ribbonMaskedTextBox;
    private DesignerVerbCollection _verbs;
    private DesignerVerb _toggleHelpersVerb;
    private DesignerVerb _moveFirstVerb;
    private DesignerVerb _movePrevVerb;
    private DesignerVerb _moveNextVerb;
    private DesignerVerb _moveLastVerb;
    private DesignerVerb _deleteTextBoxVerb;
    private ContextMenuStrip? _cms;
    private ToolStripMenuItem _toggleHelpersMenu;
    private ToolStripMenuItem _visibleMenu;
    private ToolStripMenuItem _moveFirstMenu;
    private ToolStripMenuItem _movePreviousMenu;
    private ToolStripMenuItem _moveNextMenu;
    private ToolStripMenuItem _moveLastMenu;
    private ToolStripMenuItem _deleteTextBoxMenu;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRibbonGroupMaskedTextBoxDesigner class.
    /// </summary>
    public KryptonRibbonGroupMaskedTextBoxDesigner()
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
        _ribbonMaskedTextBox = component as KryptonRibbonGroupMaskedTextBox ?? throw new ArgumentNullException(nameof(component));
            
        if (_ribbonMaskedTextBox is not null)
        {
            _ribbonMaskedTextBox.MaskedTextBoxDesigner = this;

            // Update designer properties with actual starting values
            Visible = _ribbonMaskedTextBox.Visible;
            Enabled = _ribbonMaskedTextBox.Enabled;

            // Update visible/enabled to always be showing/enabled at design time
            _ribbonMaskedTextBox.Visible = true;
            _ribbonMaskedTextBox.Enabled = true;

            // Tell the embedded text box it is in design mode
            _ribbonMaskedTextBox.MaskedTextBox.InRibbonDesignMode = true;

            // Hook into events
            _ribbonMaskedTextBox.DesignTimeContextMenu += OnContextMenu;
        }

        // Get access to the services
        _designerHost = (IDesignerHost?)GetService(typeof(IDesignerHost)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("_designerHost"));
        _changeService = (IComponentChangeService?)GetService(typeof(IComponentChangeService)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("_changeService"));

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
                _ribbonMaskedTextBox.DesignTimeContextMenu -= OnContextMenu;
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
                properties[strArray[i]] = TypeDescriptor.CreateProperty(typeof(KryptonRibbonGroupMaskedTextBoxDesigner), descrip, attributes);
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
            _moveFirstVerb = new DesignerVerb(@"Move TextBox First", OnMoveFirst);
            _movePrevVerb = new DesignerVerb(@"Move TextBox Previous", OnMovePrevious);
            _moveNextVerb = new DesignerVerb(@"Move TextBox Next", OnMoveNext);
            _moveLastVerb = new DesignerVerb(@"Move TextBox Last", OnMoveLast);
            _deleteTextBoxVerb = new DesignerVerb(@"Delete TextBox", OnDeleteTextBox);
            _verbs.AddRange(new[] { _toggleHelpersVerb, _moveFirstVerb, _movePrevVerb,
                _moveNextVerb, _moveLastVerb, _deleteTextBoxVerb });
        }

        var moveFirst = false;
        var movePrev = false;
        var moveNext = false;
        var moveLast = false;

        if (_ribbonMaskedTextBox.Ribbon != null)
        {
            var items = ParentItems ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));
            moveFirst = items.IndexOf(_ribbonMaskedTextBox) > 0;
            movePrev = items.IndexOf(_ribbonMaskedTextBox) > 0;
            moveNext = items.IndexOf(_ribbonMaskedTextBox) < (items.Count - 1);
            moveLast = items.IndexOf(_ribbonMaskedTextBox) < (items.Count - 1);
        }

        _moveFirstVerb.Enabled = moveFirst;
        _movePrevVerb.Enabled = movePrev;
        _moveNextVerb.Enabled = moveNext;
        _moveLastVerb.Enabled = moveLast;
    }

    private void OnToggleHelpers(object? sender, EventArgs e)
    {
        // Invert the current toggle helper mode
        if (_ribbonMaskedTextBox.Ribbon != null)
        {
            _ribbonMaskedTextBox.Ribbon.InDesignHelperMode = !_ribbonMaskedTextBox.Ribbon.InDesignHelperMode;
        }
    }

    private void OnMoveFirst(object? sender, EventArgs e)
    {
        if (_ribbonMaskedTextBox.Ribbon is not null)
        {
            // Get access to the parent collection of items
            var items = ParentItems ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupMaskedTextBox MoveFirst");

            try
            {
                if (_ribbonMaskedTextBox.RibbonContainer is not null)
                {
                    // Get access to the Items property
                    MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonMaskedTextBox.RibbonContainer)[@"Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the textbox
                    items.Remove(_ribbonMaskedTextBox);
                    items.Insert(0, _ribbonMaskedTextBox);
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
        if (_ribbonMaskedTextBox.Ribbon is not null)
        {
            // Get access to the parent collection of items
            var items = ParentItems ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupMaskedTextBox MovePrevious");

            try
            {
                if (_ribbonMaskedTextBox.RibbonContainer is not null)
                {
                    // Get access to the Items property
                    MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonMaskedTextBox.RibbonContainer)[@"Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the textbox
                    var index = items.IndexOf(_ribbonMaskedTextBox) - 1;
                    index = Math.Max(index, 0);
                    items.Remove(_ribbonMaskedTextBox);
                    items.Insert(index, _ribbonMaskedTextBox);
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
        if (_ribbonMaskedTextBox.Ribbon is not null)
        {
            // Get access to the parent collection of items
            var items = ParentItems ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupMaskedTextBox MoveNext");

            try
            {
                if (_ribbonMaskedTextBox.RibbonContainer is not null)
                {
                    // Get access to the Items property
                    MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonMaskedTextBox.RibbonContainer)[@"Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the textbox
                    var index = items.IndexOf(_ribbonMaskedTextBox) + 1;
                    index = Math.Min(index, items.Count - 1);
                    items.Remove(_ribbonMaskedTextBox);
                    items.Insert(index, _ribbonMaskedTextBox);
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
        if (_ribbonMaskedTextBox.Ribbon is not null)
        {
            // Get access to the parent collection of items
            var items = ParentItems ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupMaskedTextBox MoveLast");

            try
            {
                if (_ribbonMaskedTextBox.RibbonContainer is not null)
                {
                    // Get access to the Items property
                    MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonMaskedTextBox.RibbonContainer)[@"Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the textbox
                    items.Remove(_ribbonMaskedTextBox);
                    items.Insert(items.Count, _ribbonMaskedTextBox);
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

    private void OnDeleteTextBox(object? sender, EventArgs e)
    {
        if (_ribbonMaskedTextBox.Ribbon is not null)
        {
            // Get access to the parent collection of items
            var items = ParentItems ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("items"));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupMaskedTextBox DeleteTextBox");

            try
            {
                if (_ribbonMaskedTextBox.RibbonContainer is not null)
                {
                    // Get access to the Items property
                    MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonMaskedTextBox.RibbonContainer)[@"Items"];

                    RaiseComponentChanging(null);
                    RaiseComponentChanging(propertyItems);

                    // Remove the textbox from the group
                    items.Remove(_ribbonMaskedTextBox);

                    // Get designer to destroy it
                    _designerHost.DestroyComponent(_ribbonMaskedTextBox);

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
        if (_ribbonMaskedTextBox.Ribbon != null)
        {
            PropertyDescriptor? propertyEnabled = TypeDescriptor.GetProperties(_ribbonMaskedTextBox)[nameof(Enabled)] ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("propertyEnabled"));
            var oldValue = (bool?)propertyEnabled.GetValue(_ribbonMaskedTextBox);
            var newValue = !oldValue;
            _changeService.OnComponentChanged(_ribbonMaskedTextBox, null, oldValue, newValue);
            propertyEnabled.SetValue(_ribbonMaskedTextBox, newValue);
        }
    }

    private void OnVisible(object? sender, EventArgs e)
    {
        if (_ribbonMaskedTextBox.Ribbon != null)
        {
            PropertyDescriptor? propertyVisible = TypeDescriptor.GetProperties(_ribbonMaskedTextBox)[nameof(Visible)] ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("propertyVisible"));
            var oldValue = (bool?)propertyVisible.GetValue(_ribbonMaskedTextBox);
            var newValue = !oldValue;
            _changeService.OnComponentChanged(_ribbonMaskedTextBox, null, oldValue, newValue);
            propertyVisible.SetValue(_ribbonMaskedTextBox, newValue);
        }
    }

    private void OnComponentChanged(object? sender, ComponentChangedEventArgs e) => UpdateVerbStatus();

    private void OnContextMenu(object? sender, MouseEventArgs e)
    {
        if (_ribbonMaskedTextBox.Ribbon != null)
        {
            // Create the menu strip the first time around
            if (_cms == null)
            {
                _cms = new ContextMenuStrip();
                _toggleHelpersMenu = new ToolStripMenuItem("Design Helpers", null, OnToggleHelpers);
                _visibleMenu = new ToolStripMenuItem("Visible", null, OnVisible);
                _moveFirstMenu = new ToolStripMenuItem("Move TextBox First", GenericImageResources.MoveFirst, OnMoveFirst);
                _movePreviousMenu = new ToolStripMenuItem("Move TextBox Previous", GenericImageResources.MovePrevious, OnMovePrevious);
                _moveNextMenu = new ToolStripMenuItem("Move TextBox Next", GenericImageResources.MoveNext, OnMoveNext);
                _moveLastMenu = new ToolStripMenuItem("Move TextBox Last", GenericImageResources.MoveLast, OnMoveLast);
                _deleteTextBoxMenu = new ToolStripMenuItem("Delete TextBox", GenericImageResources.Delete, OnDeleteTextBox);
                _cms.Items.AddRange(new ToolStripItem[] { _toggleHelpersMenu, new ToolStripSeparator(),
                    _visibleMenu, new ToolStripSeparator(),
                    _moveFirstMenu, _movePreviousMenu, _moveNextMenu, _moveLastMenu, new ToolStripSeparator(),
                    _deleteTextBoxMenu });
            }

            // Update verbs to work out correct enable states
            UpdateVerbStatus();

            // Update menu items state from versb
            _toggleHelpersMenu.Checked = _ribbonMaskedTextBox.Ribbon.InDesignHelperMode;
            _visibleMenu.Checked = Visible;
            _moveFirstMenu.Enabled = _moveFirstVerb.Enabled;
            _movePreviousMenu.Enabled = _movePrevVerb.Enabled;
            _moveNextMenu.Enabled = _moveNextVerb.Enabled;
            _moveLastMenu.Enabled = _moveLastVerb.Enabled;

            // Show the context menu
            if (CommonHelper.ValidContextMenuStrip(_cms))
            {
                Point screenPt = _ribbonMaskedTextBox.Ribbon.ViewRectangleToPoint(_ribbonMaskedTextBox.MaskedTextBoxView);
                VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, screenPt);
            }
        }
    }

    private TypedRestrictCollection<KryptonRibbonGroupItem>? ParentItems
    {
        get
        {
            switch (_ribbonMaskedTextBox.RibbonContainer)
            {
                case KryptonRibbonGroupTriple triple:
                    return triple.Items;
                case KryptonRibbonGroupLines lines:
                    return lines.Items;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(_ribbonMaskedTextBox.RibbonContainer!.ToString());
                    return null;
            }
        }
    }
    #endregion
}