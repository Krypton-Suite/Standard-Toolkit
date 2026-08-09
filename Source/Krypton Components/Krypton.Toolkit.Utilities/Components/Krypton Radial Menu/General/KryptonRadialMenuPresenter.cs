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
/// When <see cref="PreferRadialContextMenus"/> is <c>true</c>, registers a soft hook on
/// <see cref="KryptonContextMenu.AlternativeShow"/> so normal <c>Show</c> call sites present a radial projection.
/// Imports are projections (not shared item instances).
/// </remarks>
public static class KryptonRadialMenuPresenter
{
    private static readonly ConditionalWeakTable<KryptonContextMenu, KryptonRadialMenu> Cache = new ConditionalWeakTable<KryptonContextMenu, KryptonRadialMenu>();
    private static bool _preferRadialContextMenus;
    private static readonly Func<KryptonContextMenu, object?, Rectangle, KryptonContextMenuPositionH, KryptonContextMenuPositionV, bool, bool, bool> Hook = TryShowRadial;

    /// <summary>
    /// Gets or sets whether context menus should display as radial projections when shown via <see cref="KryptonContextMenu.Show(object)"/>.
    /// </summary>
    public static bool PreferRadialContextMenus
    {
        get => _preferRadialContextMenus;
        set
        {
            if (_preferRadialContextMenus == value)
            {
                return;
            }

            _preferRadialContextMenus = value;
            if (value)
            {
                KryptonContextMenu.AlternativeShow = Hook;
            }
            else if (ReferenceEquals(KryptonContextMenu.AlternativeShow, Hook))
            {
                KryptonContextMenu.AlternativeShow = null;
            }
        }
    }

    /// <summary>
    /// Shows <paramref name="menu"/> either as a linear context menu or as a live-synced radial projection,
    /// depending on <see cref="PreferRadialContextMenus"/>.
    /// </summary>
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

    private static bool TryShowRadial(
        KryptonContextMenu? menu,
        object? caller,
        Rectangle screenRect,
        KryptonContextMenuPositionH horz,
        KryptonContextMenuPositionV vert,
        bool keyboardActivated,
        bool constrain)
    {
        if (!_preferRadialContextMenus || menu == null)
        {
            return false;
        }

        // Opening already raised by KryptonContextMenu.Show; show the radial projection at the rect centre.
        var screenPt = new Point(screenRect.X + (screenRect.Width / 2), screenRect.Y + (screenRect.Height / 2));
        if (screenRect is { Width: 0, Height: 0 })
        {
            screenPt = screenRect.Location;
        }

        return GetOrCreateProjection(menu).ShowPopup(caller, screenPt, animated: true);
    }
}
