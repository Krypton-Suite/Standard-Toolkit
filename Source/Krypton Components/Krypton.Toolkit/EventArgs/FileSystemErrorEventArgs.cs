#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides data for the FileSystemError event.
/// </summary>
public class FileSystemErrorEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the FileSystemErrorEventArgs class.
    /// </summary>
    /// <param name="path">The path where the error occurred.</param>
    /// <param name="exception">The exception that occurred.</param>
    public FileSystemErrorEventArgs(string path, Exception exception)
    {
        Path = path;
        Exception = exception;
    }

    /// <summary>
    /// Gets the path where the error occurred.
    /// </summary>
    public string Path { get; }

    /// <summary>
    /// Gets the exception that occurred.
    /// </summary>
    public Exception Exception { get; }
}
