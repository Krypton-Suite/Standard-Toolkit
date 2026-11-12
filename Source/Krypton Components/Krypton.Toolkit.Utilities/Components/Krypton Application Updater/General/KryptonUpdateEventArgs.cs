#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>Raised before a feed is contacted. Set <see cref="CancelEventArgs.Cancel"/> to skip the check.</summary>
public sealed class KryptonUpdateCheckingEventArgs : CancelEventArgs
{
}

/// <summary>
/// Raised when a newer package is available and before the default prompt or automatic download.
/// Set <see cref="CancelEventArgs.Cancel"/> to handle the package yourself via <see cref="KryptonApplicationUpdater.DownloadAsync(KryptonUpdatePackage, CancellationToken)"/> and <see cref="KryptonApplicationUpdater.ApplyAsync"/>.
/// </summary>
public sealed class KryptonUpdateAvailableEventArgs : CancelEventArgs
{
    /// <summary>Initializes the event data.</summary>
    /// <param name="package">The newer package.</param>
    /// <param name="currentVersion">Version the component compared against.</param>
    public KryptonUpdateAvailableEventArgs(KryptonUpdatePackage package, Version currentVersion)
    {
        ThrowHelper.ThrowIfNull(package);
        ThrowHelper.ThrowIfNull(currentVersion);
        Package = package;
        CurrentVersion = currentVersion;
    }

    /// <summary>Gets the newer package.</summary>
    public KryptonUpdatePackage Package { get; }

    /// <summary>Gets the version treated as already installed.</summary>
    public Version CurrentVersion { get; }
}

/// <summary>Bytes copied for one package download.</summary>
public sealed class KryptonUpdateDownloadProgress
{
    /// <summary>Initializes a progress value.</summary>
    /// <param name="bytesReceived">Bytes written so far.</param>
    /// <param name="totalBytes">Content length when the server sent one; otherwise null.</param>
    public KryptonUpdateDownloadProgress(long bytesReceived, long? totalBytes)
    {
        BytesReceived = bytesReceived;
        TotalBytes = totalBytes;
    }

    /// <summary>Gets the number of bytes written.</summary>
    public long BytesReceived { get; }

    /// <summary>Gets the total size, or null when it is unknown.</summary>
    public long? TotalBytes { get; }
}

/// <summary>
/// Raised while a package is downloading.
/// Set <see cref="CancelEventArgs.Cancel"/> to stop the download.
/// The callback may run on the UI thread.
/// </summary>
public sealed class KryptonUpdateDownloadProgressEventArgs : CancelEventArgs
{
    /// <summary>Initializes the event data.</summary>
    /// <param name="package">Package being downloaded.</param>
    /// <param name="progress">Bytes received.</param>
    public KryptonUpdateDownloadProgressEventArgs(KryptonUpdatePackage package, KryptonUpdateDownloadProgress progress)
    {
        ThrowHelper.ThrowIfNull(package);
        ThrowHelper.ThrowIfNull(progress);
        Package = package;
        Progress = progress;
    }

    /// <summary>Gets the package being downloaded.</summary>
    public KryptonUpdatePackage Package { get; }

    /// <summary>Gets the byte counts.</summary>
    public KryptonUpdateDownloadProgress Progress { get; }
}

/// <summary>
/// Raised after the file has been verified and before it is launched or extracted.
/// Set <see cref="CancelEventArgs.Cancel"/> to keep the file and apply it yourself.
/// </summary>
public sealed class KryptonUpdateApplyingEventArgs : CancelEventArgs
{
    /// <summary>Initializes the event data.</summary>
    /// <param name="package">Package about to be applied.</param>
    /// <param name="downloadedPath">Verified local file.</param>
    public KryptonUpdateApplyingEventArgs(KryptonUpdatePackage package, string downloadedPath)
    {
        ThrowHelper.ThrowIfNull(package);
        DownloadedPath = downloadedPath ?? string.Empty;
        Package = package;
    }

    /// <summary>Gets the package about to be applied.</summary>
    public KryptonUpdatePackage Package { get; }

    /// <summary>Gets the verified local file.</summary>
    public string DownloadedPath { get; }
}

/// <summary>Raised when a check, download, verification, or apply fails.</summary>
public sealed class KryptonUpdateFailedEventArgs : EventArgs
{
    /// <summary>Initializes the event data.</summary>
    /// <param name="exception">The failure.</param>
    public KryptonUpdateFailedEventArgs(Exception exception)
    {
        ThrowHelper.ThrowIfNull(exception);
        Exception = exception;
        Message = exception.Message;
    }

    /// <summary>Gets the failure.</summary>
    public Exception Exception { get; }

    /// <summary>Gets <see cref="Exception.Message"/>.</summary>
    public string Message { get; }
}

/// <summary>Raised when a check finishes, a version is postponed, or a version is skipped.</summary>
public sealed class KryptonUpdatePackageEventArgs : EventArgs
{
    /// <summary>Initializes the event data.</summary>
    /// <param name="package">Package involved, or null when nothing newer was found.</param>
    /// <param name="status">The outcome.</param>
    public KryptonUpdatePackageEventArgs(KryptonUpdatePackage? package, KryptonUpdateCheckStatus status)
    {
        Package = package;
        Status = status;
    }

    /// <summary>Gets the package, or null.</summary>
    public KryptonUpdatePackage? Package { get; }

    /// <summary>Gets the outcome.</summary>
    public KryptonUpdateCheckStatus Status { get; }
}
