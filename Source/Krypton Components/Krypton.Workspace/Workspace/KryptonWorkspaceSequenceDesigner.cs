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

internal class KryptonWorkspaceSequenceDesigner : ComponentDesigner
{
    #region Instance Fields
    private KryptonWorkspaceSequence? _sequence;
    private IComponentChangeService? _changeService;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonWorkspaceSequenceDesigner class.
    /// </summary>
    public KryptonWorkspaceSequenceDesigner()
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
        _sequence = component as KryptonWorkspaceSequence;

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
            // Create a new compound array
            var compound = new ArrayList();

            // Add the list of collection items
            compound.AddRange(_sequence?.Children!);

            return compound;
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
                _changeService!.ComponentRemoving -= OnComponentRemoving;
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
    private void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        // If our sequence is being removed
        if (e.Component == _sequence)
        {
            // Need access to host in order to delete a component
            var host = GetService(typeof(IDesignerHost)) as IDesignerHost;

            // Climb the workspace item tree to get the top most sequence
            KryptonWorkspace? workspace = null;
            IWorkspaceItem? workspaceItem = _sequence;
            while (workspaceItem?.WorkspaceParent != null)
            {
                workspaceItem = workspaceItem.WorkspaceParent;
            }

            // Grab the workspace control that contains the top most sequence
            if (workspaceItem is KryptonWorkspaceSequence sequence)
            {
                workspace = sequence.WorkspaceControl;
            }

            // We need to remove all children from the sequence
            for (var j = _sequence!.Children!.Count - 1; j >= 0; j--)
            {
                var comp = _sequence.Children[j] as Component;

                if (comp is not null)
                {
                    // If the component is a control...
                    if ((comp is Control control) && (workspace != null))
                    {
                        // We need to manually remove it from the workspace controls collection
                        var readOnlyControls = (KryptonReadOnlyControls)workspace.Controls;
                        readOnlyControls.RemoveInternal(control);
                    }

                    host?.DestroyComponent(comp);

                    // Must remove the child after it has been destroyed otherwise the component destroy method 
                    // will not be able to climb the sequence chain to find the parent workspace instance
                    _sequence.Children.Remove(comp);
                }
            }
        }
    }
    #endregion
}