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

#region CompactFlags
/// <summary>
/// Specifies the compacting operations performed during layout.
/// </summary>
[Flags]
public enum CompactFlags
{
    /// <summary>
    /// Specifies that no compacting actions take place.
    /// </summary>
    None = 0,

    /// <summary>
    /// Specifies that cells with no pages be removed.
    /// </summary>
    RemoveEmptyCells = 1,

    /// <summary>
    /// Specifies that sequences with no children be removed.
    /// </summary>
    RemoveEmptySequences = 2,

    /// <summary>
    /// Specifies that a sequence with a single child replace the sequence with the child itself.
    /// </summary>
    PromoteLeafs = 4,

    /// <summary>
    /// Specifies that there should be at least one visible cell in the workspace and creates one if none are present.
    /// </summary>
    AtLeastOneVisibleCell = 8,

    /// <summary>
    /// Specifies that all compacting flags be applied.
    /// </summary>
    All = 15
}
#endregion

#region Interface IWorkspaceItem
/// <summary>
/// Interface for an individual bar check item.
/// </summary>
public interface IWorkspaceItem
{
    /// <summary>
    /// Occurs when a property changes that affects workspace layout.
    /// </summary>
    event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Occurs when the user clicks the maximize/restore button.
    /// </summary>
    event EventHandler? MaximizeRestoreClicked;

    /// <summary>
    /// Reference to owning workspace item.
    /// </summary>
    IWorkspaceItem? WorkspaceParent { get; }

    /// <summary>
    /// Should the item be Displayed in the workspace.
    /// </summary>
    bool WorkspaceAllowResizing { get; }

    /// <summary>
    /// Should the item be Displayed in the workspace.
    /// </summary>
    bool WorkspaceVisible { get; }

    /// <summary>
    /// Current pixel size of the item.
    /// </summary>
    Size WorkspaceActualSize { get; }

    /// <summary>
    /// Current preferred size of the item.
    /// </summary>
    Size WorkspacePreferredSize { get; }

    /// <summary>
    /// Get the defined star sizing value.
    /// </summary>
    StarSize WorkspaceStarSize { get; }

    /// <summary>
    /// Get the defined minimum size.
    /// </summary>
    Size WorkspaceMinSize { get; }

    /// <summary>
    /// Get the defined maximum size.
    /// </summary>
    Size WorkspaceMaxSize { get; }

    /// <summary>
    /// Should the item be disposed when it is removed from the workspace.
    /// </summary>
    bool DisposeOnRemove { get; }

    /// <summary>
    /// Perform any compacting actions allowed by the flags.
    /// </summary>
    /// <param name="flags">Set of compacting actions allowed.</param>
    void Compact(CompactFlags flags);
}
#endregion