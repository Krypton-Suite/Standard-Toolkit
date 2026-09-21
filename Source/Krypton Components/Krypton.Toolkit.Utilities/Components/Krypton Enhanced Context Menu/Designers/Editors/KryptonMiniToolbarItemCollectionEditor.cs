#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Collection editor for Mini Toolbar items.
/// </summary>
public class KryptonMiniToolbarItemCollectionEditor : KryptonDesignerStandardCollectionEditor
{
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonMiniToolbarItemCollectionEditor"/> class.
    /// </summary>
    public KryptonMiniToolbarItemCollectionEditor()
        : base(typeof(KryptonMiniToolbarItemCollection))
    {
    }

    /// <inheritdoc />
    protected override Type[] CreateNewItemTypes() =>
    [
        typeof(KryptonMiniToolbarButton),
        typeof(KryptonMiniToolbarSplitButton),
        typeof(KryptonMiniToolbarComboBox),
        typeof(KryptonMiniToolbarSeparator),
        typeof(KryptonMiniToolbarGallery)
    ];

    /// <inheritdoc />
    protected override Type CreateCollectionItemType() => typeof(KryptonMiniToolbarButton);
}
