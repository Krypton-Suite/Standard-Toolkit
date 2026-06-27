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
/// Event arguments exposing multiple page unique names for bulk docking operations.
/// </summary>
public class UniqueNamesEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Stores the list of unique names supplied by the event source.
    /// </summary>
    /// <param name="uniqueNames">Read-only list of page unique names referenced by the event.</param>
    public UniqueNamesEventArgs(IReadOnlyList<string> uniqueNames) => UniqueNames = uniqueNames;

    #endregion

    #region Public
    /// <summary>
    /// Read-only list of page unique names referenced by the event.
    /// </summary>
    public IReadOnlyList<string> UniqueNames { get; }

    #endregion
}
