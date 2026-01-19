#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, Lesandro and tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Provides data for the DetachPreferencesChanged event.
/// </summary>
public class DetachPreferencesEventArgs : EventArgs
{
    /// <summary>
    /// Gets or sets a value indicating whether the ribbon is currently detached.
    /// </summary>
    public bool IsDetached { get; set; }

    /// <summary>
    /// Gets or sets the saved position of the floating window, or null if not saved.
    /// </summary>
    public Point? FloatingWindowPosition { get; set; }
}
