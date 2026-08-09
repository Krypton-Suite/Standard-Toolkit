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
/// Application-level helper that can present a <see cref="KryptonContextMenu"/> as a radial menu.
/// </summary>
/// <remarks>
/// This is an opt-in DX-style preference for call sites that use <see cref="Show"/> instead of
/// <see cref="KryptonContextMenu.Show(object)"/>. It does not rewrite every toolkit context-menu host.
/// Live dual-hosting of the same item instance in linear and radial UIs is not supported; imports are projections.
/// </remarks>
public static class KryptonRadialMenuPresenter
{
    private static readonly ConditionalWeakTable<KryptonContextMenu, KryptonRadialMenu> Cache = new ConditionalWeakTable<KryptonContextMenu, KryptonRadialMenu>();

    /// <summary>
    /// Gets or sets whether <see cref="Show"/> should display a radial projection instead of the linear context menu.
    /// </summary>
    public static bool PreferRadialContextMenus { get; set; }

    /// <summary>
    /// Shows <paramref name="menu"/> either as a linear context menu or as a live-synced radial projection,
    /// depending on <see cref="PreferRadialContextMenus"/>.
    /// </summary>
    /// <param name="menu">Source context menu.</param>
    /// <param name="caller">Caller reference passed to the menu.</param>
    /// <param name="screenPt">Screen location for the menu.</param>
    /// <returns>True when the menu became displayed.</returns>
    public static bool Show(KryptonContextMenu menu, object caller, Point screenPt)
    {
        if (menu == null)
        {
            throw new ArgumentNullException(nameof(menu));
        }

        if (!PreferRadialContextMenus)
        {
            return menu.Show(caller, screenPt);
        }

        return GetOrCreateProjection(menu).Show(caller, screenPt);
    }

    /// <summary>
    /// Shows <paramref name="menu"/> relative to a control client point.
    /// </summary>
    /// <param name="menu">Source context menu.</param>
    /// <param name="control">Control providing client coordinates.</param>
    /// <param name="clientPt">Client point inside <paramref name="control"/>.</param>
    /// <returns>True when the menu became displayed.</returns>
    public static bool Show(KryptonContextMenu menu, Control control, Point clientPt)
    {
        if (control == null)
        {
            throw new ArgumentNullException(nameof(control));
        }

        return Show(menu, control, control.PointToScreen(clientPt));
    }

    /// <summary>
    /// Gets a cached live-synced radial projection for <paramref name="menu"/>, creating one when needed.
    /// </summary>
    /// <param name="menu">Source context menu.</param>
    /// <returns>Radial menu projection.</returns>
    public static KryptonRadialMenu GetOrCreateProjection(KryptonContextMenu menu)
    {
        if (menu == null)
        {
            throw new ArgumentNullException(nameof(menu));
        }

        if (Cache.TryGetValue(menu, out var existing))
        {
            return existing;
        }

        var radial = KryptonRadialMenu.FromContextMenu(menu, liveSync: true);
        Cache.Add(menu, radial);
        return radial;
    }
}
