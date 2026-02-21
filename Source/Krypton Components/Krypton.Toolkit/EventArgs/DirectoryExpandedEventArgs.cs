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
/// Provides data for the DirectoryExpanded event.
/// </summary>
public class DirectoryExpandedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the DirectoryExpandedEventArgs class.
    /// </summary>
    /// <param name="path">The path that was expanded or selected.</param>
    public DirectoryExpandedEventArgs(string path)
    {
        Path = path;
    }

    /// <summary>
    /// Gets the path that was expanded or selected.
    /// </summary>
    public string Path { get; }
}
