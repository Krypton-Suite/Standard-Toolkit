#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Defines reset behaviour for strongly-typed custom string sets registered with <see cref="KryptonCustomStringSetRegistry"/>.
/// </summary>
public interface IKryptonCustomStringSet
{
    /// <summary>
    /// Gets a value indicating if all the strings are default values.
    /// </summary>
    bool IsDefault { get; }

    /// <summary>
    /// Resets all strings to default values.
    /// </summary>
    void Reset();
}
