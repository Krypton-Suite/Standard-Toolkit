#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Docking;

/// <summary>
/// Event payload associating a store page with the raising action.
/// </summary>
public class StorePageEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Captures the store page associated with the event.
    /// </summary>
    /// <param name="storePage">Store page associated with the event.</param>
    public StorePageEventArgs(KryptonStorePage storePage) => StorePage = storePage;

    #endregion

    #region Public
    /// <summary>
    /// Store page associated with the event; assigned at construction and not modified afterward.
    /// </summary>
    public KryptonStorePage StorePage { get; }

    #endregion
}
