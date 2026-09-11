#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Event data for a Mini Toolbar item activation.
/// </summary>
public class KryptonMiniToolbarItemClickEventArgs : EventArgs
{
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonMiniToolbarItemClickEventArgs"/> class.
    /// </summary>
    /// <param name="item">The item that was activated.</param>
    public KryptonMiniToolbarItemClickEventArgs(KryptonMiniToolbarItemBase item) => Item = item;

    /// <summary>
    /// Gets the item that was activated.
    /// </summary>
    public KryptonMiniToolbarItemBase Item { get; }
}
