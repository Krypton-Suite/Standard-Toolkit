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

internal class KryptonRibbonGroupCustomControlDesigner : ComponentDesigner, IKryptonDesignObject
{
    #region Instance Fields
    private IDesignerHost _designerHost;
    private IComponentChangeService _changeService;
    private KryptonRibbonGroupCustomControl? _ribbonCustomControl;
    private DesignerVerbCollection _verbs;
    private DesignerVerb _toggleHelpersVerb;
    private DesignerVerb _moveFirstVerb;
    private DesignerVerb _movePrevVerb;
    private DesignerVerb _moveNextVerb;
    private DesignerVerb _moveLastVerb;
    private DesignerVerb _deleteCustomControlVerb;
    private ContextMenuStrip? _cms;
    private ToolStripMenuItem _toggleHelpersMenu;
    private ToolStripMenuItem _visibleMenu;
    private ToolStripMenuItem _moveFirstMenu;
    private ToolStripMenuItem _movePreviousMenu;
    private ToolStripMenuItem _moveNextMenu;
    private ToolStripMenuItem _moveLastMenu;
    private ToolStripMenuItem _deleteCustomControlMenu;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRibbonGroupCustomControlDesigner class.
    /// </summary>
    public KryptonRibbonGroupCustomControlDesigner()
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
        _ribbonCustomControl = component as KryptonRibbonGroupCustomControl;
        if (_ribbonCustomControl != null)
        {
            _ribbonCustomControl.CustomControlDesigner = this;

            // Update designer properties with actual starting values
            Visible = _ribbonCustomControl.Visible;
            Enabled = _ribbonCustomControl.Enabled;

            // Update visible/enabled to always be showing/enabled at design time
            _ribbonCustomControl.Visible = true;
            _ribbonCustomControl.Enabled = true;

            // Hook into events
            _ribbonCustomControl.DesignTimeContextMenu += OnContextMenu;
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
                _ribbonCustomControl!.DesignTimeContextMenu -= OnContextMenu;
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
            if (properties[strArray[i]] is PropertyDescriptor descrip)
            {
                properties[strArray[i]] = TypeDescriptor.CreateProperty(typeof(KryptonRibbonGroupCustomControlDesigner), descrip, attributes);
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
            _moveFirstVerb = new DesignerVerb(@"Move Custom Control First", OnMoveFirst);
            _movePrevVerb = new DesignerVerb(@"Move Custom Control Previous", OnMovePrevious);
            _moveNextVerb = new DesignerVerb(@"Move Custom Control Next", OnMoveNext);
            _moveLastVerb = new DesignerVerb(@"Move Custom Control Last", OnMoveLast);
            _deleteCustomControlVerb = new DesignerVerb(@"Delete Custom Control", OnDeleteCustomControl);
            _verbs.AddRange(new[] { _toggleHelpersVerb, _moveFirstVerb, _movePrevVerb,
                _moveNextVerb, _moveLastVerb, _deleteCustomControlVerb });
        }

        var moveFirst = false;
        var movePrev = false;
        var moveNext = false;
        var moveLast = false;

        if (_ribbonCustomControl!.Ribbon != null)
        {
            var items = ParentItems;
            moveFirst = items!.IndexOf(_ribbonCustomControl) > 0;
            movePrev = items.IndexOf(_ribbonCustomControl) > 0;
            moveNext = items.IndexOf(_ribbonCustomControl) < (items.Count - 1);
            moveLast = items.IndexOf(_ribbonCustomControl) < (items.Count - 1);
        }

        _moveFirstVerb.Enabled = moveFirst;
        _movePrevVerb.Enabled = movePrev;
        _moveNextVerb.Enabled = moveNext;
        _moveLastVerb.Enabled = moveLast;
    }

    private void OnToggleHelpers(object? sender, EventArgs e)
    {
        // Invert the current toggle helper mode
        if (_ribbonCustomControl!.Ribbon != null)
        {
            _ribbonCustomControl.Ribbon.InDesignHelperMode = !_ribbonCustomControl.Ribbon.InDesignHelperMode;
        }
    }

    private void OnMoveFirst(object? sender, EventArgs e)
    {
        if (_ribbonCustomControl!.Ribbon != null)
        {
            // Get access to the parent collection of items
            var items = ParentItems;

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupCustomControl MoveFirst");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonCustomControl!.RibbonContainer!)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the custom control
                items?.Remove(_ribbonCustomControl);
                items?.Insert(0, _ribbonCustomControl);
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
        if (_ribbonCustomControl!.Ribbon != null)
        {
            // Get access to the parent collection of items
            var items = ParentItems;

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupCustomControl MovePrevious");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonCustomControl!.RibbonContainer!)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the custom control
                var index = items!.IndexOf(_ribbonCustomControl) - 1;
                index = Math.Max(index, 0);
                items?.Remove(_ribbonCustomControl);
                items?.Insert(index, _ribbonCustomControl);
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
        if (_ribbonCustomControl!.Ribbon != null)
        {
            // Get access to the parent collection of items
            var items = ParentItems;

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupCustomControl MoveNext");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonCustomControl!.RibbonContainer!)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the custom control
                var index = items!.IndexOf(_ribbonCustomControl) + 1;
                index = Math.Min(index, items.Count - 1);
                items?.Remove(_ribbonCustomControl);
                items?.Insert(index, _ribbonCustomControl);
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
        if (_ribbonCustomControl!.Ribbon != null)
        {
            // Get access to the parent collection of items
            var items = ParentItems;

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupCustomControl MoveLast");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonCustomControl!.RibbonContainer!)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the triple
                items?.Remove(_ribbonCustomControl);
                items?.Insert(items.Count, _ribbonCustomControl);
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

    private void OnDeleteCustomControl(object? sender, EventArgs e)
    {
        if (_ribbonCustomControl!.Ribbon != null)
        {
            // Get access to the parent collection of items
            var items = ParentItems;

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupCustomControl DeleteCustomControl");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonCustomControl!.RibbonContainer!)[@"Items"];

                // Remove the ribbon group from the ribbon tab
                RaiseComponentChanging(null);
                RaiseComponentChanging(propertyItems);

                // Remove the custom control from the group
                items?.Remove(_ribbonCustomControl);

                // Get designer to destroy it
                _designerHost.DestroyComponent(_ribbonCustomControl);

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

    private void OnEnabled(object? sender, EventArgs e)
    {
        if (_ribbonCustomControl!.Ribbon != null)
        {
            PropertyDescriptor? propertyEnabled = TypeDescriptor.GetProperties(_ribbonCustomControl)[nameof(Enabled)];
            var oldValue = (bool)propertyEnabled!.GetValue(_ribbonCustomControl)!;
            var newValue = !oldValue;
            _changeService.OnComponentChanged(_ribbonCustomControl, null, oldValue, newValue);
            propertyEnabled.SetValue(_ribbonCustomControl, newValue);
        }
    }

    private void OnVisible(object? sender, EventArgs e)
    {
        if (_ribbonCustomControl!.Ribbon != null)
        {
            PropertyDescriptor? propertyVisible = TypeDescriptor.GetProperties(_ribbonCustomControl)[nameof(Visible)];
            var oldValue = (bool)propertyVisible?.GetValue(_ribbonCustomControl)!;
            var newValue = !oldValue;
            _changeService.OnComponentChanged(_ribbonCustomControl, null, oldValue, newValue);
            propertyVisible.SetValue(_ribbonCustomControl, newValue);
        }
    }

    private void OnComponentChanged(object? sender, ComponentChangedEventArgs e) => UpdateVerbStatus();

    private void OnContextMenu(object? sender, MouseEventArgs e)
    {
        if (_ribbonCustomControl!.Ribbon != null)
        {
            // Create the menu strip the first time around
            if (_cms == null)
            {
                _cms = new ContextMenuStrip();
                _toggleHelpersMenu = new ToolStripMenuItem("Design Helpers", null, OnToggleHelpers);
                _visibleMenu = new ToolStripMenuItem("Visible", null, OnVisible);
                _moveFirstMenu = new ToolStripMenuItem("Move Custom Control First", GenericImageResources.MoveFirst, OnMoveFirst);
                _movePreviousMenu = new ToolStripMenuItem("Move Custom Control Previous", GenericImageResources.MovePrevious, OnMovePrevious);
                _moveNextMenu = new ToolStripMenuItem("Move Custom Control Next", GenericImageResources.MoveNext, OnMoveNext);
                _moveLastMenu = new ToolStripMenuItem("Move Custom Control Last", GenericImageResources.MoveLast, OnMoveLast);
                _deleteCustomControlMenu = new ToolStripMenuItem("Delete Custom Control", GenericImageResources.Delete, OnDeleteCustomControl);
                _cms.Items.AddRange(new ToolStripItem[] { _toggleHelpersMenu, new ToolStripSeparator(),
                    _visibleMenu, new ToolStripSeparator(),
                    _moveFirstMenu, _movePreviousMenu, _moveNextMenu, _moveLastMenu, new ToolStripSeparator(),
                    _deleteCustomControlMenu });
            }

            // Update verbs to work out correct enable states
            UpdateVerbStatus();

            // Update menu items state from versb
            _toggleHelpersMenu.Checked = _ribbonCustomControl.Ribbon.InDesignHelperMode;
            _visibleMenu.Checked = Visible;
            _moveFirstMenu.Enabled = _moveFirstVerb.Enabled;
            _movePreviousMenu.Enabled = _movePrevVerb.Enabled;
            _moveNextMenu.Enabled = _moveNextVerb.Enabled;
            _moveLastMenu.Enabled = _moveLastVerb.Enabled;

            // Show the context menu
            if (CommonHelper.ValidContextMenuStrip(_cms))
            {
                Point screenPt = _ribbonCustomControl.Ribbon.ViewRectangleToPoint(_ribbonCustomControl.CustomControlView);
                VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, screenPt);
            }
        }
    }

    private TypedRestrictCollection<KryptonRibbonGroupItem>? ParentItems
    {
        get
        {
            switch (_ribbonCustomControl!.RibbonContainer)
            {
                case KryptonRibbonGroupTriple triple:
                    return triple.Items;
                case KryptonRibbonGroupLines lines:
                    return lines.Items;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(_ribbonCustomControl.RibbonContainer!.ToString());
                    return null;
            }
        }
    }
    #endregion
}