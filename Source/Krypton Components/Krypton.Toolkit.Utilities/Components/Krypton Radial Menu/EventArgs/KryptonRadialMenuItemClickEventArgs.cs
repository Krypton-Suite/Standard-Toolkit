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
/// Event data for a radial menu item click.
/// </summary>
public class KryptonRadialMenuItemClickEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonRadialMenuItemClickEventArgs"/> class.
    /// </summary>
    /// <param name="item">The item that was clicked.</param>
    public KryptonRadialMenuItemClickEventArgs(KryptonRadialMenuItemBase item) => Item = item;

    /// <summary>
    /// Gets the item that was clicked.
    /// </summary>
    public KryptonRadialMenuItemBase Item { get; }
}
