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
/// Event arguments for page close requests where handlers can override the close action before it proceeds.
/// </summary>
public class CloseRequestEventArgs : UniqueNameEventArgs
{
    #region Identity
    /// <summary>
    /// Sets the page unique name and initial close request action for the event.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page associated with the close request.</param>
    /// <param name="closeRequest">Initial close action proposed by the docking manager.</param>
    public CloseRequestEventArgs(string uniqueName, DockingCloseRequest closeRequest)
        : base(uniqueName) =>
        CloseRequest = closeRequest;

    #endregion

    #region Public
    /// <summary>
    /// Close action to perform; handlers may change this value before the close request proceeds.
    /// </summary>
    public DockingCloseRequest CloseRequest { get; set; }

    #endregion
}
