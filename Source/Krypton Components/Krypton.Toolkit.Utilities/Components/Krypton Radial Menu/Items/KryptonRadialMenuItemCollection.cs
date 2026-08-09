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
/// Collection of <see cref="KryptonRadialMenuItemBase"/> instances for a radial menu.
/// </summary>
[Editor(typeof(KryptonRadialMenuItemCollectionEditor), typeof(UITypeEditor))]
public class KryptonRadialMenuItemCollection : TypedRestrictCollection<KryptonRadialMenuItemBase>
{
    #region Static Fields

    private static readonly Type[] _types =
    [
        typeof(KryptonRadialMenuItem),
        typeof(KryptonRadialMenuSliderItem),
        typeof(KryptonRadialMenuColorPaletteItem),
        typeof(KryptonRadialMenuFontListItem)
    ];

    #endregion

    #region Restrict

    /// <inheritdoc />
    public override Type[] RestrictTypes => _types;

    #endregion

    #region Public

    /// <summary>
    /// Returns the visible items in collection order.
    /// </summary>
    /// <returns>Visible items.</returns>
    public IEnumerable<KryptonRadialMenuItemBase> GetVisibleItems() =>
        this.Where(static item => item.Visible);

    #endregion
}
