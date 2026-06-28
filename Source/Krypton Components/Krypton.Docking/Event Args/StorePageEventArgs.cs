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
/// Event arguments that expose a store page reference for docking store and restore operations.
/// </summary>
public class StorePageEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Associates the store page with the event for page restore or persistence workflows.
    /// </summary>
    /// <param name="storePage">Store page instance referenced by the event.</param>
    public StorePageEventArgs(KryptonStorePage storePage) => StorePage = storePage;

    #endregion

    #region Public
    /// <summary>
    /// Store page instance referenced by the event for page restore or persistence workflows.
    /// </summary>
    public KryptonStorePage StorePage { get; }

    #endregion
}
