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
/// Provides data for the DirectoryExpanding event.
/// </summary>
public class DirectoryExpandingEventArgs : CancelEventArgs
{
    /// <summary>
    /// Initializes a new instance of the DirectoryExpandingEventArgs class.
    /// </summary>
    /// <param name="directoryPath">The path of the directory being expanded.</param>
    public DirectoryExpandingEventArgs(string directoryPath)
    {
        DirectoryPath = directoryPath;
    }

    /// <summary>
    /// Gets the path of the directory being expanded.
    /// </summary>
    public string DirectoryPath { get; }
}
