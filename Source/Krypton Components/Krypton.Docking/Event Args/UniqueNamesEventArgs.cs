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
/// Event arguments for events that need to provide a set of unique names.
/// </summary>
public class UniqueNamesEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the UniqueNamesEventArgs class.
    /// </summary>
    /// <param name="uniqueNames">Array of unique names.</param>
    public UniqueNamesEventArgs(IReadOnlyList<string> uniqueNames) => UniqueNames = uniqueNames;

    #endregion

    #region Public
    /// <summary>
    /// Gets the array of unique names associated with the event.
    /// </summary>
    public IReadOnlyList<string> UniqueNames { get; }

    #endregion
}