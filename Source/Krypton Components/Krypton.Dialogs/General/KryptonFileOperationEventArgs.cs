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
/// Arguments for <see cref="KryptonFileOperation.ConfirmDelete"/> and <see cref="KryptonFileOperation.ConfirmPermanentDelete"/>.
/// </summary>
public sealed class KryptonFileDeleteConfirmEventArgs : EventArgs
{
    /// <summary>
    /// Creates arguments for one confirmation prompt.
    /// </summary>
    /// <param name="paths">Absolute paths in the batch.</param>
    /// <param name="recycle">True when the items would go to the Recycle Bin.</param>
    public KryptonFileDeleteConfirmEventArgs(IReadOnlyList<string> paths, bool recycle)
    {
        Paths = paths;
        Recycle = recycle;
    }

    /// <summary>Gets the paths the prompt covers.</summary>
    public IReadOnlyList<string> Paths { get; }

    /// <summary>Gets whether this prompt is for the Recycle Bin rather than a permanent delete.</summary>
    public bool Recycle { get; }

    /// <summary>Set to true to skip the stock dialog and use <see cref="Confirmed"/> and <see cref="Cancel"/>.</summary>
    public bool Handled { get; set; }

    /// <summary>Set to true when <see cref="Handled"/> is true and the batch should stop.</summary>
    public bool Cancel { get; set; }

    /// <summary>Set to true when <see cref="Handled"/> is true and the delete should continue.</summary>
    public bool Confirmed { get; set; }
}

/// <summary>
/// Arguments for <see cref="KryptonFileOperation.FileConflict"/>.
/// </summary>
public sealed class KryptonFileConflictEventArgs : EventArgs
{
    /// <summary>
    /// Creates arguments for one name collision.
    /// </summary>
    /// <param name="sourcePath">Absolute source path.</param>
    /// <param name="destinationPath">Absolute destination path that already exists or was already queued.</param>
    /// <param name="kind">Copy, move, or rename.</param>
    public KryptonFileConflictEventArgs(string sourcePath, string destinationPath, KryptonFileOperationKind kind)
    {
        SourcePath = sourcePath;
        DestinationPath = destinationPath;
        Kind = kind;
    }

    /// <summary>Gets the source path.</summary>
    public string SourcePath { get; }

    /// <summary>Gets the destination path that collided.</summary>
    public string DestinationPath { get; }

    /// <summary>Gets the queued operation.</summary>
    public KryptonFileOperationKind Kind { get; }

    /// <summary>Set to true to skip the stock dialog and use <see cref="Choice"/>.</summary>
    public bool Handled { get; set; }

    /// <summary>Choice used when <see cref="Handled"/> is true. The default is <see cref="KryptonFileConflictChoice.Cancel"/>.</summary>
    public KryptonFileConflictChoice Choice { get; set; } = KryptonFileConflictChoice.Cancel;

    /// <summary>When true, <see cref="Choice"/> is reused for later collisions in this <see cref="KryptonFileOperation.Perform"/> call.</summary>
    public bool ApplyToRemaining { get; set; }
}

/// <summary>
/// Arguments for <see cref="KryptonFileOperation.Error"/>.
/// </summary>
public sealed class KryptonFileOperationErrorEventArgs : EventArgs
{
    /// <summary>
    /// Creates arguments for one failure.
    /// </summary>
    /// <param name="path">Item path, or empty when the failure is for the whole batch.</param>
    /// <param name="hResult">Failure code.</param>
    /// <param name="message">Message shown in the progress dialog.</param>
    public KryptonFileOperationErrorEventArgs(string path, int hResult, string message)
    {
        Path = path;
        HResult = hResult;
        Message = message;
    }

    /// <summary>Gets the item path, or an empty string for a batch-level failure.</summary>
    public string Path { get; }

    /// <summary>Gets the failure code.</summary>
    public int HResult { get; }

    /// <summary>Gets the message.</summary>
    public string Message { get; }
}
