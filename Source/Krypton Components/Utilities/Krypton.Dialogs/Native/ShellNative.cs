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
/// Shell display-name formats for <see cref="IShellItem.GetDisplayName"/>.
/// </summary>
internal enum SIGDN : uint
{
    /// <summary>The display name relative to the parent, suitable for a UI label.</summary>
    NORMALDISPLAY = 0,

    /// <summary>The file-system path.</summary>
    FILESYSPATH = 0x80058000
}

/// <summary>
/// Flags passed to <see cref="IOperationsProgressDialog.StartProgressDialog"/>.
/// </summary>
[Flags]
internal enum OPPROGDLGF : uint
{
    /// <summary>No extra progress-dialog behaviour.</summary>
    DEFAULT = 0x00000000,

    /// <summary>The operation can be paused.</summary>
    ENABLEPAUSE = 0x00000080,

    /// <summary>The operation can be undone from the dialog.</summary>
    ALLOWUNDO = 0x00000100,

    /// <summary>Do not show the source path.</summary>
    DONTDISPLAYSOURCEPATH = 0x00000200,

    /// <summary>Do not show the destination path.</summary>
    DONTDISPLAYDESTPATH = 0x00000400,

    /// <summary>Do not show estimates longer than a day.</summary>
    NOMULTIDAYESTIMATES = 0x00000800,

    /// <summary>Do not show the location line.</summary>
    DONTDISPLAYLOCATIONS = 0x00001000
}

/// <summary>
/// Mode bits for <see cref="IOperationsProgressDialog.SetMode"/>.
/// </summary>
[Flags]
internal enum PDMODE : uint
{
    /// <summary>Default progress mode.</summary>
    DEFAULT = 0x00000000,

    /// <summary>The operation is running.</summary>
    RUN = 0x00000001,

    /// <summary>The shell is still calculating the work.</summary>
    PREFLIGHT = 0x00000002,

    /// <summary>The operation is undoing.</summary>
    UNDOING = 0x00000004,

    /// <summary>Errors are blocking progress.</summary>
    ERRORSBLOCKING = 0x00000008,

    /// <summary>Length is unknown. Show a marquee.</summary>
    INDETERMINATE = 0x00000010
}

/// <summary>
/// Values returned from <see cref="IOperationsProgressDialog.GetOperationStatus"/>.
/// </summary>
internal enum PDOPSTATUS
{
    /// <summary>The operation is running.</summary>
    RUNNING = 1,

    /// <summary>The user paused the operation.</summary>
    PAUSED = 2,

    /// <summary>The user cancelled the operation.</summary>
    CANCELLED = 3,

    /// <summary>The user stopped the operation.</summary>
    STOPPED = 4,

    /// <summary>The operation is blocked on errors.</summary>
    ERRORS = 5
}

/// <summary>
/// Verb reported by <see cref="IOperationsProgressDialog.SetOperation"/>.
/// </summary>
internal enum SPACTION
{
    /// <summary>No action.</summary>
    NONE = 0,

    /// <summary>Items are being moved.</summary>
    MOVING = 1,

    /// <summary>Items are being copied.</summary>
    COPYING = 2,

    /// <summary>Items are being sent to the Recycle Bin.</summary>
    RECYCLING = 3,

    /// <summary>Attributes are being applied.</summary>
    APPLYINGATTRIBS = 4,

    /// <summary>A download is in progress.</summary>
    DOWNLOADING = 5,

    /// <summary>An Internet search is in progress.</summary>
    SEARCHING_INTERNET = 6,

    /// <summary>The shell is calculating the operation.</summary>
    CALCULATING = 7,

    /// <summary>An upload is in progress.</summary>
    UPLOADING = 8,

    /// <summary>A local search is in progress.</summary>
    SEARCHING_FILES = 9,

    /// <summary>Items are being deleted.</summary>
    DELETING = 10,

    /// <summary>Items are being renamed.</summary>
    RENAMING = 11,

    /// <summary>A format is in progress.</summary>
    FORMATTING = 12,

    /// <summary>A cross-volume move is copying then deleting.</summary>
    COPY_MOVING = 13
}

/// <summary>
/// <c>FOF_*</c> and <c>FOFX_*</c> flags for <see cref="IFileOperation.SetOperationFlags"/>.
/// </summary>
[Flags]
internal enum FILEOP_FLAGS : uint
{
    /// <summary>Do not display a progress dialog. Not used: it suppresses <see cref="IFileOperation.SetProgressDialog"/>.</summary>
    SILENT = 0x0004,

    /// <summary>Rename the destination when the name already exists.</summary>
    RENAMEONCOLLISION = 0x0008,

    /// <summary>Answer Yes to every shell confirmation.</summary>
    NOCONFIRMATION = 0x0010,

    /// <summary>Keep undo information. Delete then goes to the Recycle Bin.</summary>
    ALLOWUNDO = 0x0040,

    /// <summary>Do not ask before creating a directory.</summary>
    NOCONFIRMMKDIR = 0x0200,

    /// <summary>Do not show shell error UI. Per-item failures are reported to the sink.</summary>
    NOERRORUI = 0x0400,

    /// <summary>Warn before a permanent delete. Overrides <see cref="NOCONFIRMATION"/>.</summary>
    WANTNUKEWARNING = 0x4000,

    /// <summary>Still show the elevation prompt when <see cref="NOERRORUI"/> is set.</summary>
    SHOWELEVATIONPROMPT = 0x00040000,

    /// <summary>Send deleted items to the Recycle Bin (Windows 8 and later).</summary>
    RECYCLEONDELETE = 0x00080000
}

/// <summary>
/// Shell item used as the source or destination of a file operation.
/// </summary>
[ComImport]
[Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IShellItem
{
    /// <summary>Binds to a handler. Present so the vtable matches the shell.</summary>
    void BindToHandler(IntPtr pbc, ref Guid bhid, ref Guid riid, out IntPtr ppv);

    /// <summary>Gets the parent item.</summary>
    void GetParent(out IShellItem ppsi);

    /// <summary>Gets a display name in the requested format.</summary>
    void GetDisplayName(SIGDN sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);

    /// <summary>Gets attribute bits.</summary>
    void GetAttributes(uint sfgaoMask, out uint psfgaoAttribs);

    /// <summary>Compares two items.</summary>
    void Compare(IShellItem psi, uint hint, out int piOrder);
}

/// <summary>
/// Progress UI the shell calls while <see cref="IFileOperation.PerformOperations"/> runs.
/// </summary>
[ComImport]
[Guid("0C9FB851-E5C9-43EB-A370-F0677B13874C")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[ComVisible(true)]
internal interface IOperationsProgressDialog
{
    /// <summary>Shows the progress dialog.</summary>
    void StartProgressDialog(IntPtr hwndOwner, OPPROGDLGF flags);

    /// <summary>Hides the progress dialog.</summary>
    void StopProgressDialog();

    /// <summary>Sets the verb shown in the dialog.</summary>
    void SetOperation(SPACTION action);

    /// <summary>Sets running, preflight, undo, or indeterminate mode.</summary>
    void SetMode(PDMODE mode);

    /// <summary>Updates byte, item, and point progress.</summary>
    void UpdateProgress(ulong ullPointsCurrent, ulong ullPointsTotal, ulong ullSizeCurrent, ulong ullSizeTotal, ulong ullItemsCurrent, ulong ullItemsTotal);

    /// <summary>Sets the source, target, and current item.</summary>
    void UpdateLocations(IShellItem psiSource, IShellItem psiTarget, IShellItem psiItem);

    /// <summary>Resets the elapsed-time clock.</summary>
    void ResetTimer();

    /// <summary>Pauses the elapsed-time clock.</summary>
    void PauseTimer();

    /// <summary>Resumes the elapsed-time clock.</summary>
    void ResumeTimer();

    /// <summary>Returns elapsed and remaining time in milliseconds.</summary>
    void GetMilliseconds(out ulong pullElapsed, out ulong pullRemaining);

    /// <summary>Returns whether the user is running, paused, or cancelled.</summary>
    PDOPSTATUS GetOperationStatus();
}

/// <summary>
/// Per-item notifications for an <see cref="IFileOperation"/>.
/// </summary>
[ComImport]
[Guid("04b0f1a7-9490-44bc-96e1-4296a31252e2")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[ComVisible(true)]
internal interface IFileOperationProgressSink
{
    /// <summary>Called once before the batch starts.</summary>
    void StartOperations();

    /// <summary>Called once after the batch finishes.</summary>
    void FinishOperations(int hrResult);

    /// <summary>Called before one rename.</summary>
    void PreRenameItem(uint dwFlags, IShellItem psiItem, [MarshalAs(UnmanagedType.LPWStr)] string pszNewName);

    /// <summary>Called after one rename.</summary>
    void PostRenameItem(uint dwFlags, IShellItem psiItem, [MarshalAs(UnmanagedType.LPWStr)] string pszNewName, int hrRename, IShellItem psiNewlyCreated);

    /// <summary>Called before one move.</summary>
    void PreMoveItem(uint dwFlags, IShellItem psiItem, IShellItem psiDestinationFolder, [MarshalAs(UnmanagedType.LPWStr)] string pszNewName);

    /// <summary>Called after one move.</summary>
    void PostMoveItem(uint dwFlags, IShellItem psiItem, IShellItem psiDestinationFolder, [MarshalAs(UnmanagedType.LPWStr)] string pszNewName, int hrMove, IShellItem psiNewlyCreated);

    /// <summary>Called before one copy.</summary>
    void PreCopyItem(uint dwFlags, IShellItem psiItem, IShellItem psiDestinationFolder, [MarshalAs(UnmanagedType.LPWStr)] string pszNewName);

    /// <summary>Called after one copy.</summary>
    void PostCopyItem(uint dwFlags, IShellItem psiItem, IShellItem psiDestinationFolder, [MarshalAs(UnmanagedType.LPWStr)] string pszNewName, int hrCopy, IShellItem psiNewlyCreated);

    /// <summary>Called before one delete.</summary>
    void PreDeleteItem(uint dwFlags, IShellItem psiItem);

    /// <summary>Called after one delete.</summary>
    void PostDeleteItem(uint dwFlags, IShellItem psiItem, int hrDelete, IShellItem psiNewlyCreated);

    /// <summary>Called before a new item is created.</summary>
    void PreNewItem(uint dwFlags, IShellItem psiDestinationFolder, [MarshalAs(UnmanagedType.LPWStr)] string pszNewName);

    /// <summary>Called after a new item is created.</summary>
    void PostNewItem(uint dwFlags, IShellItem psiDestinationFolder, [MarshalAs(UnmanagedType.LPWStr)] string pszNewName, [MarshalAs(UnmanagedType.LPWStr)] string pszTemplateName, uint dwFileAttributes, int hrNew, IShellItem psiNewItem);

    /// <summary>Coarse work-unit progress. The progress dialog receives the detailed counts.</summary>
    void UpdateProgress(uint iWorkTotal, uint iWorkSoFar);

    /// <summary>Resets the sink timer.</summary>
    void ResetTimer();

    /// <summary>Pauses the sink timer.</summary>
    void PauseTimer();

    /// <summary>Resumes the sink timer.</summary>
    void ResumeTimer();
}

/// <summary>
/// Vista and later file-operation engine. Method order matches <c>shobjidl_core.h</c>.
/// </summary>
[ComImport]
[Guid("947aab5f-0a5c-4c13-b4d6-4bf7836fc9f8")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IFileOperation
{
    /// <summary>Subscribes a sink. The return value is the advise cookie.</summary>
    uint Advise(IFileOperationProgressSink pfops);

    /// <summary>Removes a sink.</summary>
    void Unadvise(uint dwCookie);

    /// <summary>Sets <c>FOF</c> / <c>FOFX</c> flags for the batch.</summary>
    void SetOperationFlags(FILEOP_FLAGS dwOperationFlags);

    /// <summary>Not implemented by the shell. Kept so the vtable stays aligned.</summary>
    void SetProgressMessage([MarshalAs(UnmanagedType.LPWStr)] string pszMessage);

    /// <summary>Replaces the Explorer progress window for this operation only.</summary>
    void SetProgressDialog(IOperationsProgressDialog popd);

    /// <summary>Sets properties to apply. Not used by this assembly.</summary>
    void SetProperties(IntPtr pproparray);

    /// <summary>Owner window for shell UI such as the elevation prompt.</summary>
    void SetOwnerWindow(IntPtr hwndOwner);

    /// <summary>Applies the declared properties to one item.</summary>
    void ApplyPropertiesToItem(IShellItem psiItem);

    /// <summary>Applies the declared properties to a set of items.</summary>
    void ApplyPropertiesToItems(IntPtr punkItems);

    /// <summary>Queues a rename.</summary>
    void RenameItem(IShellItem psiItem, [MarshalAs(UnmanagedType.LPWStr)] string pszNewName, IFileOperationProgressSink? pfopsItem);

    /// <summary>Queues a rename of several items.</summary>
    void RenameItems(IntPtr pUnkItems, [MarshalAs(UnmanagedType.LPWStr)] string pszNewName);

    /// <summary>Queues a move. A null new name keeps the source name.</summary>
    void MoveItem(IShellItem psiItem, IShellItem psiDestinationFolder, [MarshalAs(UnmanagedType.LPWStr)] string? pszNewName, IFileOperationProgressSink? pfopsItem);

    /// <summary>Queues a move of several items.</summary>
    void MoveItems(IntPtr punkItems, IShellItem psiDestinationFolder);

    /// <summary>Queues a copy. A null new name keeps the source name.</summary>
    void CopyItem(IShellItem psiItem, IShellItem psiDestinationFolder, [MarshalAs(UnmanagedType.LPWStr)] string? pszCopyName, IFileOperationProgressSink? pfopsItem);

    /// <summary>Queues a copy of several items.</summary>
    void CopyItems(IntPtr punkItems, IShellItem psiDestinationFolder);

    /// <summary>Queues a delete.</summary>
    void DeleteItem(IShellItem psiItem, IFileOperationProgressSink? pfopsItem);

    /// <summary>Queues a delete of several items.</summary>
    void DeleteItems(IntPtr punkItems);

    /// <summary>Queues creation of a new item.</summary>
    void NewItem(IShellItem psiDestinationFolder, uint dwFileAttributes, [MarshalAs(UnmanagedType.LPWStr)] string pszName, [MarshalAs(UnmanagedType.LPWStr)] string pszTemplateName, IFileOperationProgressSink pfopsItem);

    /// <summary>Runs every queued action.</summary>
    void PerformOperations();

    /// <summary>True when the user or the system stopped the batch early.</summary>
    [return: MarshalAs(UnmanagedType.Bool)]
    bool GetAnyOperationsAborted();
}

/// <summary>
/// <c>CLSID_FileOperation</c>.
/// </summary>
[ComImport]
[Guid("3ad05575-8857-4850-9277-11b85bdb8e09")]
[ClassInterface(ClassInterfaceType.None)]
internal class ShellFileOperation
{
}

/// <summary>
/// P/Invoke surface used by <see cref="FileOperationRunner"/>.
/// </summary>
internal static class ShellNative
{
    private static readonly Guid ItemIid = new Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe");

    /// <summary>Creates a shell item from a file-system path.</summary>
    [DllImport("shell32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true)]
    private static extern int SHCreateItemFromParsingName(
        [MarshalAs(UnmanagedType.LPWStr)] string pszPath,
        IntPtr pbc,
        ref Guid riid,
        [MarshalAs(UnmanagedType.Interface)] out IShellItem ppv);

    /// <summary>
    /// Creates an <see cref="IShellItem"/> for <paramref name="path"/>.
    /// </summary>
    /// <param name="path">Absolute file-system path. The target does not have to exist yet.</param>
    /// <returns>The shell item. The caller releases it.</returns>
    internal static IShellItem CreateItem(string path)
    {
        Guid iid = ItemIid;
        IShellItem item;
        int hr = SHCreateItemFromParsingName(path, IntPtr.Zero, ref iid, out item);
        if (hr < 0)
        {
            Marshal.ThrowExceptionForHR(hr);
        }

        return item;
    }

    /// <summary>Reads a display name. Returns an empty string when the shell cannot name the item.</summary>
    internal static string DisplayName(IShellItem item, SIGDN format)
    {
        if (item == null)
        {
            return string.Empty;
        }

        try
        {
            string name;
            item.GetDisplayName(format, out name);
            return name ?? string.Empty;
        }
        catch (COMException)
        {
            return string.Empty;
        }
    }

    /// <summary>Releases a COM object when it is one.</summary>
    internal static void Release(object com)
    {
        if (com != null && Marshal.IsComObject(com))
        {
            Marshal.ReleaseComObject(com);
        }
    }
}
