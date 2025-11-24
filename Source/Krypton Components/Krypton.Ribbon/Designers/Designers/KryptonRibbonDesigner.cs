#region BSD License
/*
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Ribbon;

internal class KryptonRibbonDesigner : ParentControlDesigner
{
    #region Instance Fields
    private KryptonRibbon? _ribbon;
    private IDesignerHost? _designerHost;
    private ISelectionService? _selectionService;
    private IComponentChangeService? _changeService;
    private DesignerVerbCollection _verbs = [];
    private DesignerVerb _toggleHelpersVerb;
    private DesignerVerb _addTabVerb;
    private DesignerVerb _clearTabsVerb;
    private bool _lastHitTest;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRibbonDesigner class.
    /// </summary>
    public KryptonRibbonDesigner() =>
        // The resizing handles around the control need to change depending on the
        // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
        // do not get the resizing handles, otherwise you do.
        AutoResizeHandles = true;

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
        _ribbon = component as KryptonRibbon ?? throw new ArgumentNullException(nameof(_ribbon));

        if (_ribbon.GetViewManager() is ViewManager viewManager && viewManager is not null)
        {
            // Hook into ribbon events
            viewManager.MouseUpProcessed += OnRibbonMouseUp;
            viewManager.DoubleClickProcessed += OnRibbonDoubleClick;
            _ribbon.SelectedTabChanged += OnSelectedTabChanged;
            _ribbon.DesignTimeAddTab += OnAddTab;
        }

        // Get access to the services
        _designerHost = (IDesignerHost?)GetService(typeof(IDesignerHost)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(_designerHost)));
        _changeService = (IComponentChangeService?)GetService(typeof(IComponentChangeService)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(_changeService)));
        _selectionService = (ISelectionService?)GetService(typeof(ISelectionService)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(_selectionService)));

        // We need to know when we are being removed
        _changeService.ComponentRemoving += OnComponentRemoving;
        _changeService.ComponentChanged += OnComponentChanged;
    }

    /// <summary>
    /// Indicates whether the specified control can be a child of the control managed by a designer.
    /// </summary>
    /// <param name="control">The Control to test.</param>
    /// <returns>true if the specified control can be a child of the control managed by this designer; otherwise, false.</returns>
    public override bool CanParent(Control control) =>
        // We never allow anything to be added to the ribbon
        false;

    /// <summary>
    /// Gets the collection of components associated with the component managed by the designer.
    /// </summary>
    public override ICollection AssociatedComponents
    {
        get
        {
            // Create a new collection for both values
            var compound = new ArrayList(base.AssociatedComponents);

            compound.AddRange(_ribbon!.ButtonSpecs);
            compound.AddRange(_ribbon.QATButtons);
            compound.AddRange(_ribbon.RibbonContexts);
            compound.AddRange(_ribbon.RibbonFileAppButton.AppButtonMenuItems);
            compound.AddRange(_ribbon.RibbonFileAppButton.AppButtonRecentDocs);
            compound.AddRange(_ribbon.RibbonFileAppButton.AppButtonSpecs);

            // Add all the objects for each tab
            foreach (KryptonRibbonTab ribbonTab in _ribbon.RibbonTabs)
            {
                compound.Add(ribbonTab);
            }

            return compound;
        }
    }

    /// <summary>
    ///  Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            // Create a collection of action lists
            var actionLists = new DesignerActionListCollection
            {
                // Add the ribbon specific list
                new KryptonRibbonActionList(this)
            };

            return actionLists;
        }
    }

    /// <summary>
    /// Gets the design-time verbs supported by the component that is associated with the designer.
    /// </summary>
    public override DesignerVerbCollection Verbs
    {
        get
        {
            // Create verbs first time around
            if (_verbs.Count == 0)
            {
                _toggleHelpersVerb = new DesignerVerb(@"Toggle Helpers", OnToggleHelpers);
                _addTabVerb = new DesignerVerb(@"Add Tab", OnAddTab);
                _clearTabsVerb = new DesignerVerb(@"Clear Tabs", OnClearTabs);
                _verbs.AddRange(new[] { _toggleHelpersVerb, _addTabVerb, _clearTabsVerb });
            }

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
                if (_ribbon is not null)
                {
                    if (_ribbon.GetViewManager() is ViewManager viewManager && viewManager is not null)
                    {
                        // Unhook from navigator events
                        viewManager.MouseUpProcessed -= OnRibbonMouseUp;
                        viewManager.DoubleClickProcessed -= OnRibbonDoubleClick;
                        _ribbon.SelectedTabChanged -= OnSelectedTabChanged;
                        _ribbon.DesignTimeAddTab -= OnAddTab;
                    }
                }

                if (_changeService is not null)
                {
                    // Unhook from events
                    _changeService.ComponentRemoving -= OnComponentRemoving;
                    _changeService.ComponentChanged -= OnComponentChanged;
                }
            }
        }
        finally
        {
            // Must let base class do standard stuff
            base.Dispose(disposing);
        }
    }

    /// <summary>
    /// Indicates whether a mouse click at the specified point should be handled by the control.
    /// </summary>
    /// <param name="point">A Point indicating the position at which the mouse was clicked, in screen coordinates.</param>
    /// <returns>true if a click at the specified point is to be handled by the control; otherwise, false.</returns>
    protected override bool GetHitTest(Point point)
    {
        // Ask the control if it wants to process the point
        var ret = _ribbon!.DesignerGetHitTest(_ribbon.PointToClient(point));

        // If the ribbon does not want the mouse point then make sure the 
        // tracking element is informed that the mouse has left the control
        if (!ret && _lastHitTest)
        {
            _ribbon.DesignerMouseLeave();
        }

        // Cache the last answer recovered
        _lastHitTest = ret;

        return ret;
    }

    /// <summary>
    /// Receives a call when the mouse leaves the control. 
    /// </summary>
    protected override void OnMouseLeave()
    {
        _ribbon!.DesignerMouseLeave();
        base.OnMouseLeave();
    }

    /// <summary>
    /// Called when a drag-and-drop operation enters the control designer view.
    /// </summary>
    /// <param name="de">A DragEventArgs that provides data for the event.</param>
    protected override void OnDragEnter(DragEventArgs de) => de.Effect = DragDropEffects.None;

    /// <summary>
    /// Called when a drag-and-drop object is dragged over the control designer view.
    /// </summary>
    /// <param name="de">A DragEventArgs that provides data for the event.</param>
    protected override void OnDragOver(DragEventArgs de) => de.Effect = DragDropEffects.None;

    /// <summary>
    /// Called when a drag-and-drop object is dropped onto the control designer view.
    /// </summary>
    /// <param name="de">A DragEventArgs that provides data for the event.</param>
    protected override void OnDragDrop(DragEventArgs de) => de.Effect = DragDropEffects.None;
    #endregion    

    #region Implementation
    private void OnSelectedTabChanged(object? sender, EventArgs e)
    {
        // Notify a change in the selected tab value, marks the form as dirty
        MemberDescriptor? propertyTab = TypeDescriptor.GetProperties(_ribbon!)[@"SelectedTab"];
        RaiseComponentChanging(propertyTab);
        RaiseComponentChanged(propertyTab, null, null);
    }

    private void UpdateVerbStatus()
    {
        if (_verbs.Count != 0 && _ribbon is not null)
        {
            _clearTabsVerb.Enabled = _ribbon.RibbonTabs.Count > 0;
        }
    }

    private void OnToggleHelpers(object? sender, EventArgs e) =>
        // Invert the current toggle helper mode
        _ribbon!.InDesignHelperMode = !_ribbon.InDesignHelperMode;

    private void OnAddTab(object? sender, EventArgs e)
    {
        if (_designerHost is null)
        {
            throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(_designerHost)));
        }

        if (_ribbon is null)
        {
            throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(_ribbon)));
        }

        // Use a transaction to support undo/redo actions
        DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbon AddTab");

        try
        {
            // Get access to the tabs property
            MemberDescriptor? propertyPages = TypeDescriptor.GetProperties(_ribbon)[@"RibbonTabs"];

            RaiseComponentChanging(propertyPages);

            // Get designer to create the new tab component
            var page = (KryptonRibbonTab)_designerHost.CreateComponent(typeof(KryptonRibbonTab));
            _ribbon.RibbonTabs.Add(page);

            RaiseComponentChanged(propertyPages, null, null);
        }
        finally
        {
            // If we managed to create the transaction, then do it
            transaction?.Commit();

            UpdateVerbStatus();
        }
    }

    private void OnClearTabs(object? sender, EventArgs e)
    {
        if (_designerHost is null)
        {
            throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(_designerHost)));
        }

        if (_ribbon is null)
        {
            throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(_ribbon)));
        }

        // Use a transaction to support undo/redo actions
        DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbon ClearTabs");

        try
        {
            // Get access to the tabs property
            MemberDescriptor? propertyPages = TypeDescriptor.GetProperties(_ribbon)[@"RibbonTabs"];

            RaiseComponentChanging(propertyPages);

            // Need access to host in order to delete a component
            var host = (IDesignerHost?)GetService(typeof(IDesignerHost)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("host"));

            // We need to remove all the tabs from the ribbon
            for (var i = _ribbon.RibbonTabs.Count - 1; i >= 0; i--)
            {
                KryptonRibbonTab tab = _ribbon.RibbonTabs[i];
                _ribbon.RibbonTabs.Remove(tab);
                host.DestroyComponent(tab);
            }

            RaiseComponentChanged(propertyPages, null, null);
        }
        finally
        {
            // If we managed to create the transaction, then do it
            transaction?.Commit();

            UpdateVerbStatus();
        }
    }

    private void OnRibbonMouseUp(object? sender, MouseEventArgs e)
    {
        // Get any component associated with the current mouse position
        if (_ribbon is not null
            && _ribbon.DesignerComponentFromPoint(new Point(e.X, e.Y)) is Component component
            && component is not null)
        { 
            // Select the component
            ArrayList selectionList = [component];

            _selectionService!.SetSelectedComponents(selectionList, SelectionTypes.Auto);

            // Force the layout to be updated for any change in selection
            _ribbon.PerformLayout();
        }
    }

    private void OnRibbonDoubleClick(object sender, Point pt)
    {
        // Get any component associated with the current mouse position

        // We are only interested in the contained components and not the ribbon control
        if ( _ribbon is not null
             && _ribbon.DesignerComponentFromPoint(pt) is Component component
             && component is not null
             && component is not System.Windows.Forms.Control)
        {
            // Get the designer for the component
            IDesigner? designer = _designerHost!.GetDesigner(component);

            // Request code for the default event be generated
            designer?.DoDefaultAction();
        }
    }

    private void OnComponentChanged(object? sender, ComponentChangedEventArgs e) => UpdateVerbStatus();

    private void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        // If our control is being removed
        if (e.Component == _ribbon)
        {
            // Need access to host in order to delete a component
            var host = (IDesignerHost?)GetService(typeof(IDesignerHost)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("host"));

            // We need to remove all the button spec instances
            for (var i = _ribbon!.ButtonSpecs.Count - 1; i >= 0; i--)
            {
                ButtonSpec spec = _ribbon.ButtonSpecs[i];
                _ribbon.ButtonSpecs.Remove(spec);
                host.DestroyComponent(spec);
            }

            // We need to remove all the QAT button specifications
            for (var i = _ribbon.QATButtons.Count - 1; i >= 0; i--)
            {
                Component button = _ribbon.QATButtons[i];
                _ribbon.QATButtons.Remove(button);
                host.DestroyComponent(button);
            }

            // We need to remove all the ribbon context instances
            for (var i = _ribbon.RibbonContexts.Count - 1; i >= 0; i--)
            {
                KryptonRibbonContext context = _ribbon.RibbonContexts[i];
                _ribbon.RibbonContexts.Remove(context);
                host.DestroyComponent(context);
            }

            // We need to remove all the ribbon tab instances
            for (var i = _ribbon.RibbonTabs.Count - 1; i >= 0; i--)
            {
                KryptonRibbonTab tab = _ribbon.RibbonTabs[i];
                _ribbon.RibbonTabs.Remove(tab);
                host.DestroyComponent(tab);
            }
        }
    }
    #endregion
}