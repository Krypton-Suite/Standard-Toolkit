#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Krypton-themed designer editor for <see cref="TreeNodeCollection"/>.
/// </summary>
public sealed class KryptonDesignerTreeNodeCollectionEditor : KryptonDesignerCollectionEditor
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerTreeNodeCollectionEditor"/> class.
    /// </summary>
    public KryptonDesignerTreeNodeCollectionEditor()
        : base(typeof(TreeNode))
    {
    }
    #endregion

    #region Protected
    /// <inheritdoc />
    protected override KryptonDesignerCollectionForm CreateKryptonDesignerCollectionForm() =>
        new KryptonDesignerTreeNodeCollectionForm(this);
    #endregion
}
