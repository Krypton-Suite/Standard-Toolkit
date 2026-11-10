#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Collection editor restricted to top-level <see cref="KryptonMenuBar"/> items.
/// </summary>
public class KryptonMenuBarItemCollectionEditor : KryptonDesignerCollectionEditor
{
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonMenuBarItemCollectionEditor"/> class.
    /// </summary>
    public KryptonMenuBarItemCollectionEditor()
        : base(typeof(KryptonMenuBarItemCollection))
    {
    }

    /// <inheritdoc />
    protected override VisualDesignerCollectionForm CreateKryptonDesignerCollectionForm() =>
        KryptonContextMenuCollectionEditor.CreateCollectionForm(this);

    /// <inheritdoc />
    protected override Type[] CreateNewItemTypes() =>
    [
        typeof(KryptonContextMenuItem),
        typeof(KryptonContextMenuSeparator)
    ];
}
