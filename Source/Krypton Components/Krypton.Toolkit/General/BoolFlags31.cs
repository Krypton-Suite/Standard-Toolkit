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

namespace Krypton.Toolkit;

/// <summary>
/// Manages a collection of 31 boolean flags.
/// </summary>
public struct BoolFlags31
{
    #region Public
    /// <summary>
    /// Gets and sets the entire flags value.
    /// </summary>
    public int Flags { get; set; }

    /// <summary>
    /// Set all the provided flags to true.
    /// </summary>
    /// <param name="flags">Flags to set.</param>
    /// <return>Set of flags that have changed in value.</return>
    public int SetFlags(int flags)
    {
        var before = Flags;

        // Set all the provided flags
        Flags |= flags;

        // Return set of flags that have changed value
        return before ^ Flags;
    }

    /// <summary>
    /// Clear all the provided flags to false.
    /// </summary>
    /// <param name="flags">Flags to clear.</param>
    /// <return>Set of flags that have changed in value.</return>
    public int ClearFlags(int flags)
    {
        var before = Flags;

        // Clear all the provided flags
        Flags &= ~flags;

        // Return set of flags that have changed value
        return before ^ Flags;
    }

    /// <summary>
    /// Are all the provided flags set to true.
    /// </summary>
    /// <param name="flags">Flags to test.</param>
    /// <returns>True if all flags are set; otherwise false.</returns>
    public bool AreFlagsSet(int flags) => (Flags & flags) == flags;

    #endregion
}