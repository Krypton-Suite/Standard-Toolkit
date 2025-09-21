#region BSD License
/*
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Event arguments for backstage page selection events.
/// </summary>
public class BackstagePageEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the BackstagePageEventArgs class.
    /// </summary>
    /// <param name="page">The selected backstage page.</param>
    public BackstagePageEventArgs(KryptonRibbonBackstagePage? page) => Page = page;
    #endregion

    #region Public
    /// <summary>
    /// Gets the selected backstage page.
    /// </summary>
    public KryptonRibbonBackstagePage? Page { get; }
    #endregion
}
