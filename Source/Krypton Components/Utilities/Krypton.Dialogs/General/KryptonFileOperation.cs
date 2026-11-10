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
/// Copies, moves, renames, and deletes files through <c>IFileOperation</c> and shows Krypton progress, conflict, and confirmation dialogs.
/// </summary>
/// <remarks>
/// <para>
/// Call <see cref="Perform"/> from an STA thread, normally the WinForms UI thread.
/// The shell engine runs on a background STA thread so the progress dialog can paint and accept Pause and Cancel.
/// </para>
/// <para>
/// Conflict and delete prompts run before anything is queued on the shell.
/// <c>IFileOperationProgressSink.PreCopyItem</c> cannot skip a single item: a failure cancels the whole batch.
/// <c>FOF_NOCONFIRMATION</c> would answer Yes to every shell prompt, so the Krypton dialogs decide first and only the chosen items are queued.
/// Explorer's own copy dialog is not replaced. Only operations started here use these dialogs.
/// </para>
/// <para>
/// A name passed as null keeps the source file name. Convenience methods queue one item and then call <see cref="Perform"/>, which also runs anything already queued.
/// </para>
/// </remarks>
[ToolboxItem(false)]
public sealed class KryptonFileOperation : IDisposable
{
    private readonly List<QueuedWork> _queue = new List<QueuedWork>();
    private readonly FileOperationProgressState _state = new FileOperationProgressState();
    private readonly KryptonFileOperationStrings _strings = new KryptonFileOperationStrings();
    private bool _running;
    private bool _disposed;

    /// <summary>Raised before the Recycle Bin confirmation. Set <see cref="KryptonFileDeleteConfirmEventArgs.Handled"/> to supply the answer.</summary>
    public event EventHandler<KryptonFileDeleteConfirmEventArgs>? ConfirmDelete;

    /// <summary>Raised before the permanent-delete confirmation. Set <see cref="KryptonFileDeleteConfirmEventArgs.Handled"/> to supply the answer.</summary>
    public event EventHandler<KryptonFileDeleteConfirmEventArgs>? ConfirmPermanentDelete;

    /// <summary>Raised when a destination name already exists. Set <see cref="KryptonFileConflictEventArgs.Handled"/> to supply the choice.</summary>
    public event EventHandler<KryptonFileConflictEventArgs>? FileConflict;

    /// <summary>Raised when one item or the engine fails. The progress dialog also shows <see cref="KryptonFileOperationErrorEventArgs.Message"/>.</summary>
    public event EventHandler<KryptonFileOperationErrorEventArgs>? Error;

    /// <summary>Gets the localizable dialog text.</summary>
    public KryptonFileOperationStrings Strings => _strings;

    /// <summary>Gets or sets the owner for the progress and confirmation dialogs.</summary>
    public IWin32Window? Owner { get; set; }

    /// <summary>
    /// Gets or sets whether progress, conflict, and delete dialogs show the top icon banner.
    /// </summary>
    /// <value><c>true</c> to show the banner (default); <c>false</c> to hide it and use body instruction text instead.</value>
    [DefaultValue(true)]
    [Category(@"Appearance")]
    [Description(@"Shows the Windows-style icon and main-instruction banner on file-operation dialogs.")]
    public bool ShowBanners { get; set; } = true;

    /// <summary>
    /// Queues a copy. A null or empty <paramref name="newName"/> keeps the source name.
    /// </summary>
    /// <param name="sourcePath">File or folder to copy.</param>
    /// <param name="destinationDirectory">Folder that will contain the copy.</param>
    /// <param name="newName">New file name, or null to keep the source name.</param>
    public void QueueCopy(string sourcePath, string destinationDirectory, string? newName = null)
    {
        QueueTransfer(KryptonFileOperationKind.Copy, sourcePath, destinationDirectory, newName);
    }

    /// <summary>
    /// Queues a move. A null or empty <paramref name="newName"/> keeps the source name.
    /// </summary>
    /// <param name="sourcePath">File or folder to move.</param>
    /// <param name="destinationDirectory">Folder that will contain the moved item.</param>
    /// <param name="newName">New file name, or null to keep the source name.</param>
    public void QueueMove(string sourcePath, string destinationDirectory, string? newName = null)
    {
        QueueTransfer(KryptonFileOperationKind.Move, sourcePath, destinationDirectory, newName);
    }

    /// <summary>Queues a rename in the item's current directory.</summary>
    /// <param name="path">File or folder to rename.</param>
    /// <param name="newName">New file name without a directory.</param>
    public void QueueRename(string path, string newName)
    {
        ThrowIfBusy();
        if (path == null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        if (newName == null)
        {
            throw new ArgumentNullException(nameof(newName));
        }

        if (path.Length == 0)
        {
            throw new ArgumentException("Path is empty.", nameof(path));
        }

        if (newName.Length == 0)
        {
            throw new ArgumentException("New name is empty.", nameof(newName));
        }

        RejectDirectoryName(newName, nameof(newName));
        _queue.Add(new QueuedWork
        {
            Kind = KryptonFileOperationKind.Rename,
            SourcePath = Path.GetFullPath(path),
            NewName = newName
        });
    }

    /// <summary>Queues a delete.</summary>
    /// <param name="path">File or folder to delete.</param>
    /// <param name="recycle">True to send the item to the Recycle Bin. False to delete it permanently.</param>
    public void QueueDelete(string path, bool recycle)
    {
        ThrowIfBusy();
        if (path == null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        if (path.Length == 0)
        {
            throw new ArgumentException("Path is empty.", nameof(path));
        }

        _queue.Add(new QueuedWork
        {
            Kind = KryptonFileOperationKind.Delete,
            SourcePath = Path.GetFullPath(path),
            Recycle = recycle
        });
    }

    /// <summary>Queues a copy and runs <see cref="Perform"/>.</summary>
    /// <param name="sourcePath">File or folder to copy.</param>
    /// <param name="destinationDirectory">Folder that will contain the copy.</param>
    /// <param name="newName">New file name, or null to keep the source name.</param>
    /// <returns>False when the user cancels or any item fails.</returns>
    public bool Copy(string sourcePath, string destinationDirectory, string? newName = null)
    {
        QueueCopy(sourcePath, destinationDirectory, newName);
        return Perform();
    }

    /// <summary>Queues a move and runs <see cref="Perform"/>.</summary>
    /// <param name="sourcePath">File or folder to move.</param>
    /// <param name="destinationDirectory">Folder that will contain the moved item.</param>
    /// <param name="newName">New file name, or null to keep the source name.</param>
    /// <returns>False when the user cancels or any item fails.</returns>
    public bool Move(string sourcePath, string destinationDirectory, string? newName = null)
    {
        QueueMove(sourcePath, destinationDirectory, newName);
        return Perform();
    }

    /// <summary>Queues a rename and runs <see cref="Perform"/>.</summary>
    /// <param name="path">File or folder to rename.</param>
    /// <param name="newName">New file name without a directory.</param>
    /// <returns>False when the user cancels or any item fails.</returns>
    public bool Rename(string path, string newName)
    {
        QueueRename(path, newName);
        return Perform();
    }

    /// <summary>Queues a delete and runs <see cref="Perform"/>.</summary>
    /// <param name="path">File or folder to delete.</param>
    /// <param name="recycle">True to send the item to the Recycle Bin. False to delete it permanently.</param>
    /// <returns>False when the user cancels or any item fails.</returns>
    public bool Delete(string path, bool recycle)
    {
        QueueDelete(path, recycle);
        return Perform();
    }

    /// <summary>
    /// Shows confirmation and conflict dialogs, then runs the queued work.
    /// </summary>
    /// <returns>False when the user cancels or any item fails. True when every queued item was skipped or completed.</returns>
    /// <exception cref="InvalidOperationException">The caller is not an STA thread, or a perform is already in progress.</exception>
    public bool Perform()
    {
        ThrowIfBusy();
        if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
        {
            throw new InvalidOperationException("KryptonFileOperation.Perform must be called from an STA thread.");
        }

        if (_queue.Count == 0)
        {
            return true;
        }

        QueuedWork[] snapshot = _queue.ToArray();
        _queue.Clear();
        _state.AnyError = false;
        _state.Status = (int)PDOPSTATUS.RUNNING;

        List<ResolvedFileOperation> undoable;
        List<ResolvedFileOperation> permanent;
        if (!TryPreflight(snapshot, out undoable, out permanent))
        {
            return false;
        }

        if (undoable.Count == 0 && permanent.Count == 0)
        {
            return !_state.AnyError;
        }

        return RunPasses(undoable, permanent);
    }

    /// <summary>Clears work that has not been performed.</summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _state.Status = (int)PDOPSTATUS.CANCELLED;
        _queue.Clear();
    }

    private bool RunPasses(List<ResolvedFileOperation> undoable, List<ResolvedFileOperation> permanent)
    {
        _running = true;
        VisualFileOperationProgressForm? form = null;
        try
        {
            IntPtr ownerHandle = IntPtr.Zero;
            if (Owner != null)
            {
                ownerHandle = Owner.Handle;
            }

            form = new VisualFileOperationProgressForm(_strings, _state, Owner, ShowBanners);
            // The handle must exist before the worker posts progress.
            _ = form.Handle;

            if (undoable.Count > 0)
            {
                FileOperationRunResult result = FileOperationRunner.Run(undoable, true, form, _state, ownerHandle, RaiseError);
                if (result == FileOperationRunResult.Cancelled)
                {
                    return false;
                }
            }

            if (permanent.Count > 0 &&
                _state.Status != (int)PDOPSTATUS.CANCELLED &&
                _state.Status != (int)PDOPSTATUS.STOPPED)
            {
                FileOperationRunResult result = FileOperationRunner.Run(permanent, false, form, _state, ownerHandle, RaiseError);
                if (result == FileOperationRunResult.Cancelled)
                {
                    return false;
                }
            }

            return !_state.AnyError &&
                   _state.Status != (int)PDOPSTATUS.CANCELLED &&
                   _state.Status != (int)PDOPSTATUS.STOPPED;
        }
        finally
        {
            _running = false;
            if (form != null)
            {
                form.AllowClose();
                form.Dispose();
            }
        }
    }

    private bool TryPreflight(QueuedWork[] snapshot, out List<ResolvedFileOperation> undoable, out List<ResolvedFileOperation> permanent)
    {
        undoable = new List<ResolvedFileOperation>();
        permanent = new List<ResolvedFileOperation>();

        var recycle = new List<string>();
        var destroy = new List<string>();
        for (int i = 0; i < snapshot.Length; i++)
        {
            QueuedWork work = snapshot[i];
            if (work.Kind != KryptonFileOperationKind.Delete)
            {
                continue;
            }

            if (!PathExists(work.SourcePath))
            {
                RaiseMissing(work.SourcePath);
                continue;
            }

            if (work.Recycle)
            {
                recycle.Add(work.SourcePath);
            }
            else
            {
                destroy.Add(work.SourcePath);
            }
        }

        if (recycle.Count > 0 && !Confirm(recycle, true))
        {
            return false;
        }

        if (destroy.Count > 0 && !Confirm(destroy, false))
        {
            return false;
        }

        var reserved = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        KryptonFileConflictChoice? sticky = null;
        for (int i = 0; i < snapshot.Length; i++)
        {
            QueuedWork work = snapshot[i];
            if (work.Kind == KryptonFileOperationKind.Delete)
            {
                continue;
            }

            if (!TryResolveTransfer(work, reserved, ref sticky, undoable))
            {
                return false;
            }
        }

        AddDeletes(recycle, undoable);
        AddDeletes(destroy, permanent);
        return true;
    }

    private bool Confirm(List<string> paths, bool recycle)
    {
        var args = new KryptonFileDeleteConfirmEventArgs(paths.AsReadOnly(), recycle);
        if (recycle)
        {
            ConfirmDelete?.Invoke(this, args);
        }
        else
        {
            ConfirmPermanentDelete?.Invoke(this, args);
        }

        if (args.Cancel)
        {
            return false;
        }

        if (args.Handled)
        {
            return args.Confirmed;
        }

        DialogResult result = recycle
            ? KryptonDeleteConfirmationDialog.Show(Owner, paths, _strings, ShowBanners)
            : KryptonPermanentDeleteDialog.Show(Owner, paths, _strings, ShowBanners);
        return result == DialogResult.Yes;
    }

    private bool TryResolveTransfer(
        QueuedWork work,
        HashSet<string> reserved,
        ref KryptonFileConflictChoice? sticky,
        List<ResolvedFileOperation> destination)
    {
        if (!PathExists(work.SourcePath))
        {
            RaiseMissing(work.SourcePath);
            return true;
        }

        string directory;
        string name;
        if (work.Kind == KryptonFileOperationKind.Rename)
        {
            directory = Path.GetDirectoryName(work.SourcePath) ?? string.Empty;
            name = work.NewName ?? string.Empty;
            if (directory.Length == 0 || name.Length == 0)
            {
                RaiseMissing(work.SourcePath);
                return true;
            }

            if (string.Equals(name, Path.GetFileName(work.SourcePath), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        else
        {
            directory = work.DestinationDirectory;
            name = string.IsNullOrEmpty(work.NewName) ? Path.GetFileName(work.SourcePath) : work.NewName!;
        }

        string fullDestination = Path.Combine(directory, name);
        if (string.Equals(fullDestination, work.SourcePath, StringComparison.OrdinalIgnoreCase))
        {
            RaiseError(work.SourcePath, unchecked((int)0x80070057), "Source and destination are the same.");
            return true;
        }

        bool collision = PathExists(fullDestination) || reserved.Contains(fullDestination);
        if (collision)
        {
            bool apply;
            KryptonFileConflictChoice choice;
            if (sticky.HasValue)
            {
                choice = sticky.Value;
                apply = false;
            }
            else if (!TryAskConflict(work, fullDestination, out choice, out apply))
            {
                return false;
            }

            if (apply && choice != KryptonFileConflictChoice.Cancel)
            {
                sticky = choice;
            }

            if (choice == KryptonFileConflictChoice.Cancel)
            {
                return false;
            }

            if (choice == KryptonFileConflictChoice.Skip)
            {
                return true;
            }

            if (choice == KryptonFileConflictChoice.KeepBoth)
            {
                name = AllocateKeepBothName(directory, name, reserved);
                fullDestination = Path.Combine(directory, name);
            }
        }

        reserved.Add(fullDestination);
        destination.Add(new ResolvedFileOperation
        {
            Kind = work.Kind,
            SourcePath = work.SourcePath,
            DestinationDirectory = directory,
            NewName = name
        });
        return true;
    }

    private bool TryAskConflict(QueuedWork work, string destinationPath, out KryptonFileConflictChoice choice, out bool apply)
    {
        var args = new KryptonFileConflictEventArgs(work.SourcePath, destinationPath, work.Kind);
        FileConflict?.Invoke(this, args);
        if (args.Handled)
        {
            choice = args.Choice;
            apply = args.ApplyToRemaining;
            return choice != KryptonFileConflictChoice.Cancel;
        }

        choice = KryptonFileConflictDialog.Show(Owner, work.SourcePath, destinationPath, _strings, out apply, ShowBanners);
        return choice != KryptonFileConflictChoice.Cancel;
    }

    private static void AddDeletes(List<string> paths, List<ResolvedFileOperation> destination)
    {
        for (int i = 0; i < paths.Count; i++)
        {
            destination.Add(new ResolvedFileOperation
            {
                Kind = KryptonFileOperationKind.Delete,
                SourcePath = paths[i]
            });
        }
    }

    private static string AllocateKeepBothName(string directory, string fileName, HashSet<string> reserved)
    {
        string stem = Path.GetFileNameWithoutExtension(fileName);
        string extension = Path.GetExtension(fileName);
        for (int n = 2; n < 10000; n++)
        {
            string candidate = stem + " (" + n.ToString(CultureInfo.InvariantCulture) + ")" + extension;
            string full = Path.Combine(directory, candidate);
            if (!PathExists(full) && !reserved.Contains(full))
            {
                return candidate;
            }
        }

        throw new IOException("Could not allocate a unique name for " + fileName + ".");
    }

    private void QueueTransfer(KryptonFileOperationKind kind, string sourcePath, string destinationDirectory, string? newName)
    {
        ThrowIfBusy();
        if (sourcePath == null)
        {
            throw new ArgumentNullException(nameof(sourcePath));
        }

        if (destinationDirectory == null)
        {
            throw new ArgumentNullException(nameof(destinationDirectory));
        }

        if (sourcePath.Length == 0)
        {
            throw new ArgumentException("Source path is empty.", nameof(sourcePath));
        }

        if (destinationDirectory.Length == 0)
        {
            throw new ArgumentException("Destination directory is empty.", nameof(destinationDirectory));
        }

        if (!string.IsNullOrEmpty(newName))
        {
            RejectDirectoryName(newName!, nameof(newName));
        }

        _queue.Add(new QueuedWork
        {
            Kind = kind,
            SourcePath = Path.GetFullPath(sourcePath),
            DestinationDirectory = Path.GetFullPath(destinationDirectory),
            NewName = string.IsNullOrEmpty(newName) ? null : newName
        });
    }

    private void RaiseMissing(string path)
    {
        RaiseError(path, unchecked((int)0x80070002), "The system cannot find the path specified.");
    }

    private void RaiseError(string path, int hresult, string message)
    {
        _state.AnyError = true;
        Error?.Invoke(this, new KryptonFileOperationErrorEventArgs(path ?? string.Empty, hresult, message));
    }

    private static bool PathExists(string path)
    {
        return File.Exists(path) || Directory.Exists(path);
    }

    private static void RejectDirectoryName(string name, string paramName)
    {
        if (name.IndexOfAny(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }) >= 0)
        {
            throw new ArgumentException("The new name must not contain a directory.", paramName);
        }
    }

    private void ThrowIfBusy()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(KryptonFileOperation));
        }

        if (_running)
        {
            throw new InvalidOperationException("A file operation is already running.");
        }
    }

    private sealed class QueuedWork
    {
        internal KryptonFileOperationKind Kind;
        internal string SourcePath = string.Empty;
        internal string DestinationDirectory = string.Empty;
        internal string? NewName;
        internal bool Recycle;
    }
}
