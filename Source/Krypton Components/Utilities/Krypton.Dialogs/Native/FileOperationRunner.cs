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
/// Raises a per-item or engine failure on the UI thread.
/// </summary>
/// <param name="path">Item path, or empty when the failure is for the whole batch.</param>
/// <param name="hresult">Failure code.</param>
/// <param name="message">Message for the progress dialog and the <c>Error</c> event.</param>
internal delegate void FileErrorHandler(string path, int hresult, string message);

/// <summary>
/// Result of one <see cref="IFileOperation.PerformOperations"/> pass.
/// </summary>
internal enum FileOperationRunResult
{
    /// <summary>The shell finished the batch.</summary>
    Completed,

    /// <summary>The user cancelled or the shell aborted the batch.</summary>
    Cancelled,

    /// <summary>The engine could not be created or <c>PerformOperations</c> threw.</summary>
    Failed
}

/// <summary>
/// Runs <see cref="IFileOperation"/> on a dedicated STA thread so the UI pump stays free.
/// The progress form is created on the caller's STA thread and its handle exists before the worker starts.
/// </summary>
internal static class FileOperationRunner
{
    /// <summary>
    /// Queues <paramref name="items"/> and blocks the caller, pumping messages, until the worker finishes.
    /// </summary>
    internal static FileOperationRunResult Run(
        IList<ResolvedFileOperation> items,
        bool allowUndo,
        VisualFileOperationProgressForm form,
        FileOperationProgressState state,
        IntPtr ownerHandle,
        FileErrorHandler onError)
    {
        if (items.Count == 0)
        {
            return FileOperationRunResult.Completed;
        }

        state.AllowUndo = allowUndo;
        state.Status = (int)PDOPSTATUS.RUNNING;
        Exception? failure = null;
        int done = 0;

        var thread = new Thread(() =>
        {
            try
            {
                Execute(items, allowUndo, form, state, ownerHandle, onError);
            }
            catch (Exception ex)
            {
                failure = ex;
                state.AnyError = true;
            }
            finally
            {
                Interlocked.Exchange(ref done, 1);
            }
        });

        thread.SetApartmentState(ApartmentState.STA);
        thread.IsBackground = true;
        thread.Name = "KryptonFileOperation";
        thread.Start();

        while (Volatile.Read(ref done) == 0)
        {
            Application.DoEvents();
            Thread.Sleep(15);
        }

        // Drain a progress update posted just before the worker exited.
        Application.DoEvents();

        if (failure != null)
        {
            onError(string.Empty, Marshal.GetHRForException(failure), failure.Message);
            return FileOperationRunResult.Failed;
        }

        if (state.Status == (int)PDOPSTATUS.CANCELLED || state.Status == (int)PDOPSTATUS.STOPPED)
        {
            return FileOperationRunResult.Cancelled;
        }

        return FileOperationRunResult.Completed;
    }

    private static void Execute(
        IList<ResolvedFileOperation> items,
        bool allowUndo,
        VisualFileOperationProgressForm form,
        FileOperationProgressState state,
        IntPtr ownerHandle,
        FileErrorHandler onError)
    {
        var operation = (IFileOperation)new ShellFileOperation();
        var progress = new OperationsProgressDialog(form, form.Strings);
        var sink = new FileOperationSink(state, progress, (path, hr) => form.ReportError(path, hr, onError));
        var created = new List<object>();
        uint cookie = 0;

        try
        {
            cookie = operation.Advise(sink);
            FILEOP_FLAGS flags = FILEOP_FLAGS.NOCONFIRMATION | FILEOP_FLAGS.NOCONFIRMMKDIR | FILEOP_FLAGS.NOERRORUI | FILEOP_FLAGS.SHOWELEVATIONPROMPT;
            if (allowUndo)
            {
                flags |= FILEOP_FLAGS.ALLOWUNDO | FILEOP_FLAGS.RECYCLEONDELETE;
            }

            operation.SetOperationFlags(flags);
            if (ownerHandle != IntPtr.Zero)
            {
                operation.SetOwnerWindow(ownerHandle);
            }

            operation.SetProgressDialog(progress);

            for (int i = 0; i < items.Count; i++)
            {
                Queue(operation, items[i], created, form, onError);
            }

            operation.PerformOperations();
            if (operation.GetAnyOperationsAborted())
            {
                state.Status = (int)PDOPSTATUS.CANCELLED;
            }
        }
        finally
        {
            if (cookie != 0)
            {
                try
                {
                    operation.Unadvise(cookie);
                }
                catch (COMException)
                {
                }
            }

            for (int i = 0; i < created.Count; i++)
            {
                ShellNative.Release(created[i]);
            }

            ShellNative.Release(operation);
        }
    }

    private static void Queue(
        IFileOperation operation,
        ResolvedFileOperation item,
        List<object> created,
        VisualFileOperationProgressForm form,
        FileErrorHandler onError)
    {
        try
        {
            if (item.Kind == KryptonFileOperationKind.Copy || item.Kind == KryptonFileOperationKind.Move)
            {
                if (!Directory.Exists(item.DestinationDirectory))
                {
                    Directory.CreateDirectory(item.DestinationDirectory);
                }
            }

            IShellItem source = ShellNative.CreateItem(item.SourcePath);
            created.Add(source);

            if (item.Kind == KryptonFileOperationKind.Delete)
            {
                operation.DeleteItem(source, null);
                return;
            }

            if (item.Kind == KryptonFileOperationKind.Rename)
            {
                operation.RenameItem(source, item.NewName ?? string.Empty, null);
                return;
            }

            IShellItem destination = ShellNative.CreateItem(item.DestinationDirectory);
            created.Add(destination);
            if (item.Kind == KryptonFileOperationKind.Move)
            {
                operation.MoveItem(source, destination, item.NewName, null);
            }
            else
            {
                operation.CopyItem(source, destination, item.NewName, null);
            }
        }
        catch (COMException ex)
        {
            form.ReportError(item.SourcePath, ex.HResult, onError);
        }
        catch (IOException ex)
        {
            form.ReportError(item.SourcePath, Marshal.GetHRForException(ex), onError);
        }
        catch (UnauthorizedAccessException ex)
        {
            form.ReportError(item.SourcePath, Marshal.GetHRForException(ex), onError);
        }
    }
}
