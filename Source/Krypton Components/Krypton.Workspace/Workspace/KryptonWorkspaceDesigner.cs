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

namespace Krypton.Workspace;

internal class KryptonWorkspaceDesigner : ParentControlDesigner
{
    #region Instance Fields
    private KryptonWorkspace? _workspace;
    private IComponentChangeService? _changeService;
    #endregion

    #region Public Overrides
    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    /// <param name="component">The IComponent to associate with the designer.</param>
    public override void Initialize([DisallowNull] IComponent component)
    {
        // Let base class do standard stuff
        base.Initialize(component);

        Debug.Assert(component != null);

        // The resizing handles around the control need to change depending on the
        // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
        // do not get the resizing handles, otherwise you do.
        AutoResizeHandles = true;

        // Remember the actual control being designed
        _workspace = component as KryptonWorkspace;

        // Get access to the services
        _changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;

        // We need to know when we are being removed/changed
        _changeService!.ComponentRemoving += OnComponentRemoving;
    }

    /// <summary>
    /// Gets the collection of components associated with the component managed by the designer.
    /// </summary>
    public override ICollection AssociatedComponents
    {
        get
        {
            var compound = new ArrayList();

            if (_workspace != null)
            {
                compound.AddRange(_workspace.Root.Children!);
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
                // Add the navigator specific list
                new KryptonWorkspaceActionList(this)
            };

            return actionLists;
        }
    }

    /// <summary>
    /// Indicates whether the specified control can be a child of the control managed by a designer.
    /// </summary>
    /// <param name="control">The Control to test.</param>
    /// <returns>true if the specified control can be a child of the control managed by this designer; otherwise, false.</returns>
    public override bool CanParent(Control control) => false;

    #endregion

    #region Protected
    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        _changeService!.ComponentRemoving -= OnComponentRemoving;

        // Ensure base class is always disposed
        base.Dispose(disposing);
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
    private void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        // If our workspace is being removed
        if (e is not null 
            && e.Component is not null 
            && e.Component.Equals(_workspace))
        {
            // Prevent layout being performed during removal of children otherwise the layout
            // code will cause the controls to be added back before they are actually destroyed
            _workspace?.SuspendLayout();

            // Need access to host in order to delete a component
            var host = GetService(typeof(IDesignerHost)) as IDesignerHost;

            // We need to remove all children from the workspace
            for (var i = _workspace!.Root.Children!.Count - 1; i >= 0; i--)
            {
                var comp = _workspace.Root.Children[i] as Component;

                // If the component is a control...
                if (comp is Control control)
                {
                    // We need to manually remove it from the workspace controls collection
                    var readOnlyControls = _workspace.Controls as KryptonReadOnlyControls;
                    readOnlyControls?.RemoveInternal(control);
                }

                host?.DestroyComponent(comp);

                // Must remove the child after it has been destroyed otherwise the component destroy method 
                // will not be able to climb the sequence chain to find the parent workspace instance
                _workspace.Root.Children.Remove(comp);
            }

            _workspace.ResumeLayout();
        }
    }
    #endregion
}