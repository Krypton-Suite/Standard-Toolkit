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
/// Specifies which nodes may be selected in a <see cref="KryptonTreeComboBox"/> drop-down.
/// </summary>
public enum KryptonTreeComboBoxSelectMode
{
    /// <summary>
    /// Only leaf nodes (nodes without children) can be committed.
    /// </summary>
    LeafOnly = 0,

    /// <summary>
    /// Any node, including grouping/parent nodes, can be committed.
    /// </summary>
    AnyNode = 1
}
