#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Provides the tag string for <see cref="KryptonTagInputControl"/> add and remove events.
/// </summary>
public class KryptonTagEventArgs : EventArgs
{
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonTagEventArgs"/> class.
    /// </summary>
    /// <param name="tag">The tag that was added or removed.</param>
    public KryptonTagEventArgs(string tag) => Tag = tag ?? string.Empty;

    /// <summary>
    /// Gets the tag that was added or removed.
    /// </summary>
    public string Tag { get; }
}
