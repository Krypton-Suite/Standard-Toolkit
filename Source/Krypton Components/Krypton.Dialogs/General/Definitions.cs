#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Dialogs;

/// <summary>
/// Kind of work queued on <see cref="KryptonFileOperation"/>.
/// </summary>
public enum KryptonFileOperationKind
{
    /// <summary>Copy a file or folder.</summary>
    Copy,

    /// <summary>Move a file or folder.</summary>
    Move,

    /// <summary>Rename a file or folder in its current directory.</summary>
    Rename,

    /// <summary>Delete a file or folder, either to the Recycle Bin or permanently.</summary>
    Delete
}

/// <summary>
/// Choice from <see cref="KryptonFileConflictDialog"/>.
/// </summary>
public enum KryptonFileConflictChoice
{
    /// <summary>Overwrite the destination.</summary>
    Replace,

    /// <summary>Leave the destination and do not queue this item.</summary>
    Skip,

    /// <summary>Queue the item under a new name such as <c>Name (2).ext</c>.</summary>
    KeepBoth,

    /// <summary>Abandon the whole batch before the shell runs.</summary>
    Cancel
}
