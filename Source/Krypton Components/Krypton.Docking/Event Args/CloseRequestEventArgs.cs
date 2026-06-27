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
/// Event payload for <c>PageCloseRequest</c> where handlers choose how each named page is closed.
/// </summary>
public class CloseRequestEventArgs : UniqueNameEventArgs
{
    #region Identity
    /// <summary>
    /// Captures the page unique name and initial close action for the close request.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page associated with the event.</param>
    /// <param name="closeRequest">Initial close action; typically the docking manager default.</param>
    public CloseRequestEventArgs(string uniqueName, DockingCloseRequest closeRequest)
        : base(uniqueName) =>
        CloseRequest = closeRequest;

    #endregion

    #region Public
    /// <summary>
    /// Close action applied to the named page; handlers may change this before the manager acts on it.
    /// </summary>
    public DockingCloseRequest CloseRequest { get; set; }

    #endregion
}
