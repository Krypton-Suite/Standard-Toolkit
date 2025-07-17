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

/// <summary>
/// Collection of workspace items.
/// </summary>
public class KryptonWorkspaceCollection : TypedRestrictCollection<Component>
{
    #region Instance Fields
    private readonly KryptonWorkspaceSequence _sequence;
    #endregion

    #region Static Fields
    private static readonly Type[] _types = { typeof(KryptonWorkspaceCell),
        typeof(KryptonWorkspaceSequence)};
    #endregion

    #region Events
    /// <summary>
    /// Occurs after a change has occurred to the collection.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Occurs when the user clicks the maximize/restore button.
    /// </summary>
    public event EventHandler? MaximizeRestoreClicked;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonWorkspaceCollection class.
    /// </summary>
    /// <param name="sequence">Reference to the owning sequence.</param>
    public KryptonWorkspaceCollection(KryptonWorkspaceSequence sequence) => _sequence = sequence;

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() => $"{Count} Children";

    #endregion

    #region Public
    /// <summary>
    /// Gets an array of types that the collection is restricted to contain.
    /// </summary>
    public override Type[] RestrictTypes => _types;

    /// <summary>
    /// Gets a value indicating if the collection or child collections contains a cell instance.
    /// </summary>
    public bool ContainsVisibleCell
    {
        get
        {
            foreach (Component c in this)
            {
                switch (c)
                {
                    // If we have a cell and that cell wants to be visible then we are done
                    case KryptonWorkspaceCell { WorkspaceVisible: true }:
                    // If we have a sequence and it is visible and contains a visible cell then we are done
                    case KryptonWorkspaceSequence { WorkspaceVisible: true, Children.ContainsVisibleCell: true }:
                        return true;
                }
            }

            return false;
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// Raises the Inserted event.
    /// </summary>
    /// <param name="e">A TypedCollectionEventArgs instance containing event data.</param>
    protected override void OnInserted(TypedCollectionEventArgs<Component> e)
    {
        base.OnInserted(e);
            
        if (e.Item is IWorkspaceItem workspaceItem)
        {
            workspaceItem.PropertyChanged += OnChildPropertyChanged;
            workspaceItem.MaximizeRestoreClicked += OnChildMaximizeRestoreClicked;
        }

        switch (e.Item)
        {
            case KryptonWorkspaceCell cell:
                cell.WorkspaceParent = _sequence;
                break;
            case KryptonWorkspaceSequence sequence:
                sequence.WorkspaceParent = _sequence;
                break;
        }

        OnPropertyChanged(@"Children");
    }

    /// <summary>
    /// Raises the Removed event.
    /// </summary>
    /// <param name="e">A TypedCollectionEventArgs instance containing event data.</param>
    protected override void OnRemoved(TypedCollectionEventArgs<Component> e)
    {
        base.OnRemoved(e);

        if (e.Item is IWorkspaceItem workspaceItem)
        {
            workspaceItem.PropertyChanged -= OnChildPropertyChanged;
            workspaceItem.MaximizeRestoreClicked -= OnChildMaximizeRestoreClicked;
        }

        switch (e.Item)
        {
            case KryptonWorkspaceCell cell:
                cell.WorkspaceParent = null;
                break;
            case KryptonWorkspaceSequence sequence:
                sequence.WorkspaceParent = null;
                break;
        }

        OnPropertyChanged(@"Children");
    }

    /// <summary>
    /// Raises the Clearing event.
    /// </summary>
    /// <param name="e">An EventArgs instance containing event data.</param>
    protected override void OnClearing(EventArgs e)
    {
        base.OnClearing(e);

        // Unhook from monitoring the child items
        foreach (Component c in this)
        {
            if (c is IWorkspaceItem workspaceItem)
            {
                workspaceItem.PropertyChanged -= OnChildPropertyChanged;
                workspaceItem.MaximizeRestoreClicked -= OnChildMaximizeRestoreClicked;
            }

            switch (c)
            {
                case KryptonWorkspaceCell cell:
                    cell.WorkspaceParent = null;
                    break;
                case KryptonWorkspaceSequence sequence:
                    sequence.WorkspaceParent = null;
                    break;
            }
        }
    }

    /// <summary>
    /// Raises the Cleared event.
    /// </summary>
    /// <param name="e">An EventArgs instance containing event data.</param>
    protected override void OnCleared(EventArgs e)
    {
        base.OnCleared(e);
        OnPropertyChanged(@"Children");
    }

    /// <summary>
    /// Handle a change in a child item.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Event arguments associated with the event.</param>
    protected void OnChildPropertyChanged(object? sender, PropertyChangedEventArgs e) => OnPropertyChanged(e);

    /// <summary>
    /// Handle a maximize/restore request from a child item.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Event arguments associated with the event.</param>
    protected void OnChildMaximizeRestoreClicked(object? sender, EventArgs e) => MaximizeRestoreClicked?.Invoke(sender, e);

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="property">Name of the property that has changed.</param>
    protected virtual void OnPropertyChanged(string property) => OnPropertyChanged(new PropertyChangedEventArgs(property));

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="e">Event arguments associated with the event.</param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);
    #endregion
}