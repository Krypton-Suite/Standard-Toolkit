#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Navigator;

/// <summary>
/// Designer for the navigator instance.
/// </summary>
public class KryptonNavigatorDesigner : ParentControlDesigner
{
    #region Instance Fields
    private bool _lastHitTest;
    private bool _ignoreOnAddPage;
    private DesignerVerbCollection? _verbs;
    private DesignerVerb _verbAddPage;
    private DesignerVerb _verbRemovePage;
    private DesignerVerb _verbClearPages;
    private IDesignerHost _designerHost;
    private IComponentChangeService _changeService;
    private ISelectionService _selectionService;
    #endregion

    #region Public Overrides
    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    /// <param name="component">The IComponent to associate the designer with.</param>
    public override void Initialize([DisallowNull] IComponent component)
    {
        // Let base class do standard stuff
        base.Initialize(component);

        Debug.Assert(component != null);

        // The resizing handles around the control need to change depending on the
        // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
        // do not get the resizing handles, otherwise you do.
        AutoResizeHandles = true;

        // Cast to correct type
        Navigator = component as KryptonNavigator;
        if (Navigator != null)
        {

            // Must enable the child panel so that copy and paste of navigator
            // correctly copies across copies of the child pages. Also allows the
            // child panel to be viewed in the document outline and modified.
            EnableDesignMode(Navigator.ChildPanel!, "PageContainer");

            // Make sure that all the pages in control can be designed
            foreach (KryptonPage page in Navigator.Pages)
            {
                EnableDesignMode(page, page.Name);
            }

            // Monitor navigator events
            Navigator.GetViewManager()!.MouseDownProcessed += OnNavigatorMouseUp;
            Navigator.GetViewManager()!.DoubleClickProcessed += OnNavigatorDoubleClick;
            Navigator.Pages.Inserted += OnPageInserted;
            Navigator.Pages.Removed += OnPageRemoved;
            Navigator.Pages.Cleared += OnPagesCleared;
            Navigator!.SelectedPageChanged += OnSelectedPageChanged;
        }

        // Get access to the services
        _designerHost = (IDesignerHost?)GetService(typeof(IDesignerHost)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(_designerHost)));
        _changeService = (IComponentChangeService?)GetService(typeof(IComponentChangeService)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(_changeService)));
        _selectionService = (ISelectionService?)GetService(typeof(ISelectionService)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(_selectionService)));

        // We need to know when we are being removed
        _changeService.ComponentRemoving += OnComponentRemoving;
    }

    /// <summary>
    /// Initializes a newly created component. 
    /// </summary>
    /// <param name="defaultValues">A name/value dictionary of default values to apply to properties.</param>
    public override void InitializeNewComponent(IDictionary defaultValues)
    {
        // Let base class set the initial position and parent
        base.InitializeNewComponent(defaultValues);

        // Add a couple of pages
        _ignoreOnAddPage = true;
        OnAddPage(this, EventArgs.Empty);
        OnAddPage(this, EventArgs.Empty);
        _ignoreOnAddPage = false;

        // Get access to the Pages property
        if (Navigator != null)
        {
            MemberDescriptor? propertyPages = TypeDescriptor.GetProperties(Navigator)[@"Pages"];

            // Notify that the pages collection has been updated
            RaiseComponentChanging(propertyPages);
            RaiseComponentChanged(propertyPages, null, null);
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

                // Add the navigator specific list
                new KryptonNavigatorActionList(this)
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
            // First time around we create the verb collection
            if (_verbs == null)
            {
                // Cache verb instances so enabled state can be updated in future
                _verbAddPage = new DesignerVerb(@"Add Page", OnAddPage);
                _verbRemovePage = new DesignerVerb(@"Remove Page", OnRemovePage);
                _verbClearPages = new DesignerVerb(@"Clear Pages", OnClearPages);
                _verbs = new DesignerVerbCollection(new[] { _verbAddPage, _verbRemovePage, _verbClearPages });

                // Set correct initial state of the verbs
                UpdateVerbStatus();
            }

            return _verbs;
        }
    }

    /// <summary>
    /// Gets the collection of components associated with the component managed by the designer.
    /// </summary>
    public override ICollection AssociatedComponents
    {
        get
        {
            // Create a new compound array
            var compound = new ArrayList();

            // Add all the navigator components
            if (Navigator != null)
            {
                compound.AddRange(Navigator.Button.ButtonSpecs!);
                compound.AddRange(Navigator.Pages);
            }

            return compound;
        }
    }

    /// <summary>
    /// Indicates whether the specified control can be a child of the control managed by a designer.
    /// </summary>
    /// <param name="control">The Control to test.</param>
    /// <returns>true if the specified control can be a child of the control managed by this designer; otherwise, false.</returns>
    public override bool CanParent(Control control)
    {
        // Only a KryptonPage can be added as a child
        if (control is KryptonPage)
        {
            // Cannot add the same page more than once
            return !Navigator!.Controls.Contains(control);
        }

        return false;
    }

    /// <summary>
    /// Returns the internal control designer with the specified index in the ControlDesigner.
    /// </summary>
    /// <param name="internalControlIndex">A specified index to select the internal control designer. This index is zero-based.</param>
    /// <returns>A ControlDesigner at the specified index.</returns>
    public override ControlDesigner InternalControlDesigner(int internalControlIndex) => (ControlDesigner)_designerHost.GetDesigner(Navigator!.Pages[internalControlIndex])!;

    /// <summary>
    /// Returns the number of internal control designers in the ControlDesigner.
    /// </summary>
    /// <returns>The number of internal control designers in the ControlDesigner.</returns>
    public override int NumberOfInternalControlDesigners() => Navigator!.Pages.Count;

    /// <summary>
    /// Add a new page to the navigator.
    /// </summary>
    public void AddPage() => OnAddPage(this, EventArgs.Empty);

    /// <summary>
    /// Remove the current page from the navigator.
    /// </summary>
    public void RemovePage() => OnRemovePage(this, EventArgs.Empty);

    /// <summary>
    /// Remove all pages from the navigator.
    /// </summary>
    public void ClearPages() => OnClearPages(this, EventArgs.Empty);
    #endregion

    #region Protected Overrides
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
                // Unhook from navigator events
                if (Navigator != null)
                {
                    Navigator.GetViewManager()!.MouseUpProcessed -= OnNavigatorMouseUp;
                    Navigator.GetViewManager()!.DoubleClickProcessed -= OnNavigatorDoubleClick;
                    Navigator.Pages.Inserted -= OnPageInserted;
                    Navigator.Pages.Removed -= OnPageRemoved;
                    Navigator.Pages.Cleared -= OnPagesCleared;
                    Navigator.SelectedPageChanged -= OnSelectedPageChanged;
                }

                // Unhook from designer events
                _changeService.ComponentRemoving -= OnComponentRemoving;
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
        var ret = Navigator != null && Navigator.DesignerGetHitTest(Navigator.PointToClient(point));

        // If the navigator does not want the mouse point then make sure the 
        // tracking element is informed that the mouse has left the control
        if (!ret && _lastHitTest)
        {
            Navigator?.DesignerMouseLeave();
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
        Navigator?.DesignerMouseLeave();
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

    #region Protected Virtual
    // ReSharper disable VirtualMemberNeverOverridden.Global
    /// <summary>
    /// Gets access to the associated navigator instance.
    /// </summary>
    protected virtual KryptonNavigator? Navigator { get; private set; }

    /// <summary>
    /// Occurs when the component is being removed from the designer.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A ComponentEventArgs containing event data.</param>
    protected virtual void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        // If our control is being removed
        if (e.Component == Navigator)
        {
            // Need access to host in order to delete a component
            var host = (IDesignerHost)GetService(typeof(IDesignerHost))!;

            // We need to remove all the button spec instances
            if (Navigator != null)
            {
                for (var i = Navigator.Button.ButtonSpecs!.Count - 1; i >= 0; i--)
                {
                    ButtonSpec spec = Navigator.Button.ButtonSpecs[i];
                    _changeService.OnComponentChanging(Navigator, null);
                    Navigator.Button.ButtonSpecs.Remove(spec);
                    host.DestroyComponent(spec);
                    _changeService.OnComponentChanged(Navigator, null, null, null);
                }

                // We need to remove all the page instances
                for (var i = Navigator.Pages.Count - 1; i >= 0; i--)
                {
                    KryptonPage page = Navigator.Pages[i];
                    _changeService.OnComponentChanging(Navigator, null);
                    Navigator.Pages.Remove(page);
                    host.DestroyComponent(page);
                    _changeService.OnComponentChanged(Navigator, null, null, null);
                }
            }
        }
    }
    // ReSharper restore VirtualMemberNeverOverridden.Global
    #endregion

    #region Implementation
    private void OnAddPage(object? sender, EventArgs e)
    {
        // Use a transaction to support undo/redo actions
        DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonNavigator AddPage");

        try
        {
            // Get access to the Pages property
            if (Navigator != null)
            {
                MemberDescriptor? propertyPages = TypeDescriptor.GetProperties(Navigator)[@"Pages"];

                // Do we need to raise the changing notification?
                if (!_ignoreOnAddPage)
                {
                    RaiseComponentChanging(propertyPages);
                }

                // Get designer to create the new page component
                var page = (KryptonPage)_designerHost.CreateComponent(typeof(KryptonPage));

                // Get access to the Name and Text propertues of the new page
                PropertyDescriptor? propertyName = TypeDescriptor.GetProperties(page)[@"Name"];
                PropertyDescriptor? propertyText = TypeDescriptor.GetProperties(page)[@"Text"];

                // If we managed to get the property and it is the string we are expecting
                if ((propertyName != null) && (propertyName.PropertyType == typeof(string)) &&
                    (propertyText != null) && (propertyText.PropertyType == typeof(string)))
                {
                    // Grab the design time name
                    var name = (string)propertyName.GetValue(page)!;

                    // If the name is valid
                    if (name is { Length: > 0 })
                    {
                        // Use the design time name as the page text
                        propertyText.SetValue(page, name);
                    }
                }

                // Add a new defaulted page to the navigator
                Navigator.Pages.Add(page);

                // Do we need to raise the changed notification?
                if (!_ignoreOnAddPage)
                {
                    RaiseComponentChanged(propertyPages, null, null);
                }
            }
        }
        finally
        {
            // If we managed to create the transaction, then do it
            transaction.Commit();
        }
    }

    private void OnRemovePage(object? sender, EventArgs e)
    {
        // Use a transaction to support undo/redo actions
        DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonNavigator RemovePage");

        try
        {
            // Get access to the Pages property
            if (Navigator != null)
            {
                MemberDescriptor? propertyPages = TypeDescriptor.GetProperties(Navigator)[@"Pages"];

                // Remove the selected page from the navigator
                RaiseComponentChanging(propertyPages);

                // Get the page we are going to remove
                KryptonPage? removePage = Navigator.SelectedPage;

                // Get designer to destroy it
                if (removePage != null)
                {
                    _designerHost.DestroyComponent(removePage);
                }

                RaiseComponentChanged(propertyPages, null, null);
            }
        }
        finally
        {
            // If we managed to create the transaction, then do it
            transaction.Commit();
        }
    }

    private void OnClearPages(object? sender, EventArgs e)
    {
        if (KryptonMessageBox.Show(@"Are you sure that all pages should be removed?",
                @"Clear Pages",
                KryptonMessageBoxButtons.YesNo,
                KryptonMessageBoxIcon.Warning) == DialogResult.Yes)
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonNavigator RemovePage");

            try
            {
                // Get access to the Pages property
                if (Navigator != null)
                {
                    MemberDescriptor? propertyPages = TypeDescriptor.GetProperties(Navigator)[@"Pages"];

                    // Remove all pages from the navigator
                    RaiseComponentChanging(propertyPages);

                    // Get the designer to destroy each page in turn
                    for (var i = Navigator.Pages.Count; i > 0; i--)
                    {
                        _designerHost.DestroyComponent(Navigator.Pages[0]);
                    }

                    RaiseComponentChanged(propertyPages, null, null);
                }
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction.Commit();
            }
        }
    }

    private void OnPageInserted(object sender, TypedCollectionEventArgs<KryptonPage> e)
    {
        // Let the user design the new page surface
        if (e.Item != null)
        {
            EnableDesignMode(e.Item, e.Item.Name);
        }
        UpdateVerbStatus();
    }

    private void OnPageRemoved(object sender, TypedCollectionEventArgs<KryptonPage> e) => UpdateVerbStatus();

    private void OnPagesCleared(object? sender, EventArgs e) => UpdateVerbStatus();

    private void OnSelectedPageChanged(object? sender, EventArgs e)
    {
        MarkSelectionAsChanged();
        UpdateVerbStatus();
    }

    private void OnNavigatorMouseUp(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            // Get any component associated with the current mouse position
            Component? component = Navigator?.DesignerComponentFromPoint(new Point(e.X, e.Y));

            if (component != null)
            {
                // Force the layout to be update for any change in selection
                Navigator?.PerformLayout();

                // Select the component
                var selectionList = new ArrayList
                {
                    component
                };
                _selectionService.SetSelectedComponents(selectionList, SelectionTypes.Auto);
            }
        }
    }

    private void OnNavigatorDoubleClick(object sender, Point pt)
    {
        // Get any component associated with the current mouse position
        Component? component = Navigator?.DesignerComponentFromPoint(pt);

        // We are only interested in the button spec components and not the tab/check buttons
        if (component != null && component is not System.Windows.Forms.Control)
        {
            // Get the designer for the component
            IDesigner? designer = _designerHost.GetDesigner(component);

            // Request code for the default event be generated
            designer?.DoDefaultAction();
        }
    }

    private void UpdateVerbStatus()
    {
        // Can only update verbs, if the verbs have been created
        if (_verbs != null)
        {
            if (Navigator != null)
            {
                _verbRemovePage.Enabled = Navigator.SelectedPage != null;
                _verbClearPages.Enabled = Navigator.Pages.Count > 0;
            }
        }
    }

    private void MarkSelectionAsChanged()
    {
        // Get access to the SelectedPage property
        MemberDescriptor? propertyPage = TypeDescriptor.GetProperties(Navigator!)[@"SelectedPage"];

        // Notify that the selected page has been updated
        RaiseComponentChanging(propertyPage);
        RaiseComponentChanged(propertyPage, null, null);
    }
    #endregion
}