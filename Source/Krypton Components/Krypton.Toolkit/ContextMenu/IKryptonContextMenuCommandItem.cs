#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Identifies a context menu item that can share a <see cref="KryptonCommand"/> with other items.
/// </summary>
public interface IKryptonContextMenuCommandItem
{
    /// <summary>
    /// Gets the command associated with the menu item.
    /// </summary>
    KryptonCommand? KryptonCommand { get; }

    /// <summary>
    /// Gets the optional parameter used to discriminate between items that share the same command.
    /// </summary>
    object? CommandParameter { get; }
}