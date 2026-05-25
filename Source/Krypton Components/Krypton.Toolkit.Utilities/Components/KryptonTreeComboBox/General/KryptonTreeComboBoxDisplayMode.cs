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
/// Specifies how the committed tree node is formatted in the editor text of a
/// <see cref="KryptonTreeComboBox"/>.
/// </summary>
public enum KryptonTreeComboBoxDisplayMode
{
    /// <summary>
    /// Shows the selected node's <see cref="TreeNode.Text"/> only.
    /// </summary>
    LeafText = 0,

    /// <summary>
    /// Shows the node's <see cref="TreeNode.FullPath"/> using <see cref="KryptonTreeComboBox.PathSeparator"/>.
    /// </summary>
    FullPath = 1,

    /// <summary>
    /// Shows ancestor nodes joined by <see cref="KryptonTreeComboBox.BreadcrumbSeparator"/>.
    /// </summary>
    Breadcrumb = 2
}
