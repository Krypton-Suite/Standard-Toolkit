#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Collection editor for <see cref="KryptonFormCaptionItemCollection"/>.
/// </summary>
public sealed class KryptonFormCaptionItemCollectionEditor : KryptonDesignerStandardCollectionEditor
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonFormCaptionItemCollectionEditor"/> class.
    /// </summary>
    public KryptonFormCaptionItemCollectionEditor()
        : base(typeof(KryptonFormCaptionItem))
    {
    }

    /// <inheritdoc />
    /// <remarks>
    /// <see cref="CollectionEditor"/> treats the constructor type as the collection type and
    /// otherwise falls back to <see cref="object"/> for new items. Return the item type explicitly.
    /// </remarks>
    protected override Type[] CreateNewItemTypes() => new[] { DesignerCollectionItemType };

    /// <inheritdoc />
    protected override Type CreateCollectionItemType() => DesignerCollectionItemType;

    /// <inheritdoc />
    protected override VisualDesignerCollectionForm CreateKryptonDesignerCollectionForm() =>
        new VisualCaptionItemCollectionForm(this);
}
