#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Collection of SystemMenuItem objects that supports designer serialization and change notifications.
/// </summary>
[TypeConverter(typeof(CollectionConverter))]
[ListBindable(false)]
public class SystemMenuItemCollection : TypedCollection<SystemMenuItemValues>
{


    #region Identity
    /// <summary>
    /// Initialize a new instance of the SystemMenuItemCollection class.
    /// </summary>
    public SystemMenuItemCollection()
    {
    }
    #endregion


    #region Public Methods
    /// <summary>
    /// Adds a new menu item with the specified text.
    /// </summary>
    /// <param name="text">The text for the menu item.</param>
    /// <returns>The newly added menu item.</returns>
    public SystemMenuItemValues Add(string text)
    {
        var item = new SystemMenuItemValues(text);
        Add(item);
        return item;
    }

    /// <summary>
    /// Adds a new menu item with the specified text and shortcut.
    /// </summary>
    /// <param name="text">The text for the menu item.</param>
    /// <param name="shortcut">The keyboard shortcut text.</param>
    /// <returns>The newly added menu item.</returns>
    public SystemMenuItemValues Add(string text, string shortcut)
    {
        var item = new SystemMenuItemValues(text, shortcut);
        Add(item);
        return item;
    }

    /// <summary>
    /// Returns a string representation of the collection.
    /// </summary>
    /// <returns>A string showing the number of items in the collection.</returns>
    public override string ToString()
    {
        return Count == 0 ? string.Empty : "Modified";
    }

    /// <summary>
    /// Gets a value indicating if any items should be serialized.
    /// </summary>
    /// <returns>True if any items should be serialized; otherwise false.</returns>
    public bool ShouldSerialize()
    {
        var shouldSerialize = Count > 0;
        return shouldSerialize;
    }

    /// <summary>
    /// Resets the collection to its default state (empty).
    /// </summary>
    public void Reset()
    {
        Clear();
    }

    #endregion
}