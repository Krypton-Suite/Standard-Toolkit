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
/// Event payload listing the unique names of docking pages involved in the raising action.
/// </summary>
public class UniqueNamesEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Captures the unique names supplied when the event is raised.
    /// </summary>
    /// <param name="uniqueNames">Unique names of the pages associated with the event.</param>
    public UniqueNamesEventArgs(IReadOnlyList<string> uniqueNames) => UniqueNames = uniqueNames;

    #endregion

    #region Public
    /// <summary>
    /// Unique names of the pages; assigned at construction and not modified afterward.
    /// </summary>
    public IReadOnlyList<string> UniqueNames { get; }

    #endregion
}
