#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

public sealed class FileEntry : INotifyPropertyChanged
{
    private volatile string _hash;
    private UploadStatus _status;
    private int _progress;
    private string _statusMessage;
    private StaleState _stale;

    public string Path { get; }
    public string Name { get; }
    public string Extension { get; }
    public long Size { get; }
    public DateTime Modified { get; }
    public bool IsTemporary { get; }
    public string RejectionReason { get; internal set; }
    public object Tag { get; set; }

    public UploadStatus Status => _status;
    public int Progress => _progress;
    public string StatusMessage => _statusMessage;
    public StaleState Stale => _stale;
    public string ContentHash => _hash;

    public event PropertyChangedEventHandler PropertyChanged;

    internal FileEntry(string path, long size, DateTime modified, string rejectionReason, bool isTemporary)
    {
        Path = path;
        Name = System.IO.Path.GetFileName(path);
        Extension = System.IO.Path.GetExtension(path).ToLowerInvariant();
        Size = size;
        Modified = modified;
        RejectionReason = rejectionReason;
        IsTemporary = isTemporary;
    }

    public static FileEntry FromDisk(string path, string rejectionReason = null)
    {
        long size = 0;
        DateTime modified = DateTime.MinValue;
        try
        {
            var info = new FileInfo(path);
            size = info.Length;
            modified = info.LastWriteTime;
        }
        catch (Exception ex)
        {
            rejectionReason = rejectionReason ?? DropZoneStrings.Format("Reject_Unreadable", ex.Message);
        }
        return new FileEntry(path, size, modified, rejectionReason, false);
    }

    internal static FileEntry Temporary(string tempPath, long size, DateTime modified, string rejectionReason) =>
        new FileEntry(tempPath, size, modified, rejectionReason, true);

    // Called on the UI thread by FileDropCollection.
    internal void SetStatus(UploadStatus status, int progress, string message)
    {
        _status = status;
        _progress = progress;
        _statusMessage = message;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Status)));
    }

    internal bool SetStale(StaleState state)
    {
        if (_stale == state)
        {
            return false;
        }

        _stale = state;
        if (state == StaleState.Modified)
        {
            _hash = null; // cached hash no longer trustworthy
        }

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Stale)));
        return true;
    }

    /// <summary>SHA-256 of the file, cached. Returns null if the file can't be read.</summary>
    public string GetOrComputeHash(CancellationToken token)
    {
        string h = _hash;
        if (h != null)
        {
            return h;
        }

        try
        {
            using (var sha = SHA256.Create())
            using (var fs = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete, 81920))
            {
                var buffer = new byte[81920];
                int n;
                while ((n = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    token.ThrowIfCancellationRequested();
                    sha.TransformBlock(buffer, 0, n, null, 0);
                }
                sha.TransformFinalBlock(buffer, 0, 0);
                h = BitConverter.ToString(sha.Hash);
            }
        }
        catch (OperationCanceledException) { throw; }
        catch (Exception) { return null; }
        _hash = h;
        return h;
    }
}