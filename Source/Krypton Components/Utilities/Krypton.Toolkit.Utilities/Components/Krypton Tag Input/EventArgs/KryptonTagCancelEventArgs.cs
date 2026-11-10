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
/// Provides the proposed tag and a cancel flag for <see cref="KryptonTagInputControl.TagAdding"/>.
/// </summary>
public class KryptonTagCancelEventArgs : CancelEventArgs
{
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonTagCancelEventArgs"/> class.
    /// </summary>
    /// <param name="tag">The tag that is about to be added.</param>
    public KryptonTagCancelEventArgs(string tag) => Tag = tag ?? string.Empty;

    /// <summary>
    /// Gets the tag that is about to be added.
    /// </summary>
    public string Tag { get; }
}
