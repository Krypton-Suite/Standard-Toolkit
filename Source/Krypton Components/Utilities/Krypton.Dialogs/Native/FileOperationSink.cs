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
/// Reports per-item failures. A failed <c>Pre*</c> method cancels the whole batch, so this sink never returns an error.
/// </summary>
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
internal sealed class FileOperationSink : IFileOperationProgressSink
{
    private readonly FileOperationProgressState _state;
    private readonly OperationsProgressDialog _progress;
    private readonly Action<string, int> _onError;

    /// <summary>
    /// Creates a sink that posts failures through <paramref name="onError"/> on the UI thread.
    /// Location lines that the shell leaves blank are filled from the pre-item callbacks.
    /// </summary>
    internal FileOperationSink(FileOperationProgressState state, OperationsProgressDialog progress, Action<string, int> onError)
    {
        _state = state;
        _progress = progress;
        _onError = onError;
    }

    /// <inheritdoc />
    public void StartOperations()
    {
    }

    /// <inheritdoc />
    public void FinishOperations(int hrResult)
    {
        Report(string.Empty, hrResult);
    }

    /// <inheritdoc />
    public void PreRenameItem(uint dwFlags, IShellItem psiItem, string pszNewName)
    {
        _progress.NotePaths(ShellNative.DisplayName(psiItem, SIGDN.FILESYSPATH), pszNewName ?? string.Empty, string.Empty);
    }

    /// <inheritdoc />
    public void PostRenameItem(uint dwFlags, IShellItem psiItem, string pszNewName, int hrRename, IShellItem psiNewlyCreated)
    {
        Report(ShellNative.DisplayName(psiItem, SIGDN.FILESYSPATH), hrRename);
    }

    /// <inheritdoc />
    public void PreMoveItem(uint dwFlags, IShellItem psiItem, IShellItem psiDestinationFolder, string pszNewName)
    {
        NoteTransfer(psiItem, psiDestinationFolder, pszNewName);
    }

    /// <inheritdoc />
    public void PostMoveItem(uint dwFlags, IShellItem psiItem, IShellItem psiDestinationFolder, string pszNewName, int hrMove, IShellItem psiNewlyCreated)
    {
        Report(ShellNative.DisplayName(psiItem, SIGDN.FILESYSPATH), hrMove);
    }

    /// <inheritdoc />
    public void PreCopyItem(uint dwFlags, IShellItem psiItem, IShellItem psiDestinationFolder, string pszNewName)
    {
        NoteTransfer(psiItem, psiDestinationFolder, pszNewName);
    }

    /// <inheritdoc />
    public void PostCopyItem(uint dwFlags, IShellItem psiItem, IShellItem psiDestinationFolder, string pszNewName, int hrCopy, IShellItem psiNewlyCreated)
    {
        Report(ShellNative.DisplayName(psiItem, SIGDN.FILESYSPATH), hrCopy);
    }

    /// <inheritdoc />
    public void PreDeleteItem(uint dwFlags, IShellItem psiItem)
    {
        _progress.NotePaths(ShellNative.DisplayName(psiItem, SIGDN.FILESYSPATH), string.Empty, string.Empty);
    }

    /// <inheritdoc />
    public void PostDeleteItem(uint dwFlags, IShellItem psiItem, int hrDelete, IShellItem psiNewlyCreated)
    {
        Report(ShellNative.DisplayName(psiItem, SIGDN.FILESYSPATH), hrDelete);
    }

    /// <inheritdoc />
    public void PreNewItem(uint dwFlags, IShellItem psiDestinationFolder, string pszNewName)
    {
    }

    /// <inheritdoc />
    public void PostNewItem(uint dwFlags, IShellItem psiDestinationFolder, string pszNewName, string pszTemplateName, uint dwFileAttributes, int hrNew, IShellItem psiNewItem)
    {
        Report(ShellNative.DisplayName(psiNewItem, SIGDN.FILESYSPATH), hrNew);
    }

    /// <inheritdoc />
    public void UpdateProgress(uint iWorkTotal, uint iWorkSoFar)
    {
    }

    /// <inheritdoc />
    public void ResetTimer()
    {
    }

    /// <inheritdoc />
    public void PauseTimer()
    {
    }

    /// <inheritdoc />
    public void ResumeTimer()
    {
    }

    private void NoteTransfer(IShellItem source, IShellItem destinationFolder, string newName)
    {
        string sourcePath = ShellNative.DisplayName(source, SIGDN.FILESYSPATH);
        string folder = ShellNative.DisplayName(destinationFolder, SIGDN.FILESYSPATH);
        string target = folder;
        if (folder.Length > 0 && !string.IsNullOrEmpty(newName))
        {
            target = System.IO.Path.Combine(folder, newName);
        }

        string item = string.Empty;
        if (sourcePath.Length > 0)
        {
            item = System.IO.Path.GetFileName(sourcePath);
        }

        _progress.NotePaths(sourcePath, target, item);
    }

    private void Report(string path, int hr)
    {
        if (hr >= 0)
        {
            return;
        }

        _state.AnyError = true;
        try
        {
            _onError(path ?? string.Empty, hr);
        }
        catch (InvalidOperationException)
        {
        }
    }
}
