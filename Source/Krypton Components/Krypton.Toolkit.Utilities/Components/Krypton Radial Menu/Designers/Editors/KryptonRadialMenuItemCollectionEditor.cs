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
/// Krypton-themed collection editor for <see cref="KryptonRadialMenuItemCollection"/>.
/// </summary>
public class KryptonRadialMenuItemCollectionEditor : KryptonDesignerStandardCollectionEditor
{
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRadialMenuItemCollectionEditor"/> class.
    /// </summary>
    public KryptonRadialMenuItemCollectionEditor()
        : base(typeof(KryptonRadialMenuItemCollection))
    {
    }

    /// <inheritdoc />
    protected override Type[] CreateNewItemTypes() =>
    [
        typeof(KryptonRadialMenuItem),
        typeof(KryptonRadialMenuSliderItem),
        typeof(KryptonRadialMenuColorPaletteItem),
        typeof(KryptonRadialMenuFontListItem)
    ];

    /// <inheritdoc />
    protected override Type CreateCollectionItemType() => typeof(KryptonRadialMenuItem);
}
