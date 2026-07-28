#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator.Utilities;

/// <summary>
/// Collection of <see cref="NavigatorTabGroup"/> catalog entries for caption tab grouping.
/// </summary>
public class NavigatorTabGroupCollection : TypedCollection<NavigatorTabGroup>
{
    /// <summary>
    /// Gets the group with the given id, or null if not found.
    /// </summary>
    /// <param name="id">Group id.</param>
    public new NavigatorTabGroup? this[string id]
    {
        get
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            foreach (NavigatorTabGroup group in this)
            {
                if (string.Equals(group.Id, id, StringComparison.Ordinal))
                {
                    return group;
                }
            }

            return null;
        }
    }

    /// <summary>
    /// Returns true when a group with the given id exists.
    /// </summary>
    public bool ContainsId(string id) => this[id] != null;

    /// <summary>
    /// Copies group definitions from another collection (by id), replacing matching entries.
    /// </summary>
    public void CopyFrom(NavigatorTabGroupCollection source)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        foreach (NavigatorTabGroup group in source)
        {
            NavigatorTabGroup? existing = this[group.Id];
            if (existing != null)
            {
                existing.Title = group.Title;
                existing.Color = group.Color;
                existing.Collapsed = group.Collapsed;
            }
            else
            {
                Add(group.Clone());
            }
        }
    }
}
