#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Docking;

/// <summary>
/// Event arguments for events that need to provide a store page reference.
/// </summary>
public class StorePageEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the StorePageEventArgs class.
    /// </summary>
    /// <param name="storePage">Reference to store page that is associated with the event.</param>
    public StorePageEventArgs(KryptonStorePage storePage) => StorePage = storePage;

    #endregion

    #region Public
    /// <summary>
    /// Gets a reference to store page that is associated with the event.
    /// </summary>
    public KryptonStorePage StorePage { get; }

    #endregion
}