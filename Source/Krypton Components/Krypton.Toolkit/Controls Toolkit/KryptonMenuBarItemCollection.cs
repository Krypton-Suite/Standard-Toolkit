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
/// Restricted collection of top-level items for <see cref="KryptonMenuBar"/>.
/// Only <see cref="KryptonContextMenuItem"/> and <see cref="KryptonContextMenuSeparator"/> are allowed.
/// </summary>
[Editor(typeof(KryptonMenuBarItemCollectionEditor), typeof(UITypeEditor))]
public class KryptonMenuBarItemCollection : TypedRestrictCollection<KryptonContextMenuItemBase>
{
    #region Static Fields

    private static readonly Type[] _types =
    [
        typeof(KryptonContextMenuItem),
        typeof(KryptonContextMenuSeparator)
    ];

    #endregion

    #region Restrict

    /// <inheritdoc />
    public override Type[] RestrictTypes => _types;

    #endregion

    #region Shortcuts

    /// <summary>
    /// Tests each top-level item and its descendants for a matching shortcut.
    /// </summary>
    /// <param name="keyData">Key data to check against shortcut definitions.</param>
    /// <returns>True if a shortcut was handled; otherwise false.</returns>
    public bool ProcessShortcut(Keys keyData)
    {
        foreach (KryptonContextMenuItemBase item in this)
        {
            if (!item.Visible)
            {
                continue;
            }

            if (item.ProcessShortcut(keyData))
            {
                return true;
            }

            if (item is KryptonContextMenuItem menuItem && menuItem.Items.ProcessShortcut(keyData))
            {
                return true;
            }
        }

        return false;
    }

    #endregion
}
