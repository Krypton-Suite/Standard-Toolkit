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
[TypeConverter(typeof(ExpandableObjectConverter))]
public class SystemMenuItemCollection : ObservableCollection<SystemMenuItemValues>, IList
{


    #region Identity
    /// <summary>
    /// Initialize a new instance of the SystemMenuItemCollection class.
    /// </summary>
    public SystemMenuItemCollection()
    {
    }
    #endregion

    #region Overrides
    /// <summary>
    /// Raises the CollectionChanged event.
    /// </summary>
    /// <param name="e">Event arguments.</param>
    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        base.OnCollectionChanged(e);
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

    #region IList Implementation
    /// <summary>
    /// Gets a value indicating whether the collection has a fixed size.
    /// </summary>
    bool IList.IsFixedSize => false;

    /// <summary>
    /// Gets a value indicating whether the collection is read-only.
    /// </summary>
    bool IList.IsReadOnly => false;

    /// <summary>
    /// Gets or sets the element at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the element to get or set.</param>
    /// <returns>The element at the specified index.</returns>
    object? IList.this[int index]
    {
        get => this[index];
        set => this[index] = (SystemMenuItemValues)value!;
    }

    /// <summary>
    /// Adds an item to the collection.
    /// </summary>
    /// <param name="value">The object to add to the collection.</param>
    /// <returns>The position into which the new element was inserted.</returns>
    int IList.Add(object? value)
    {
        Add((SystemMenuItemValues)value!);
        return Count - 1;
    }

    /// <summary>
    /// Removes all items from the collection.
    /// </summary>
    void IList.Clear()
    {
        Clear();
    }

    /// <summary>
    /// Determines whether the collection contains a specific value.
    /// </summary>
    /// <param name="value">The object to locate in the collection.</param>
    /// <returns>True if the object is found in the collection; otherwise, false.</returns>
    bool IList.Contains(object? value)
    {
        return Contains((SystemMenuItemValues)value!);
    }

    /// <summary>
    /// Determines the index of a specific item in the collection.
    /// </summary>
    /// <param name="value">The object to locate in the collection.</param>
    /// <returns>The index of value if found in the collection; otherwise, -1.</returns>
    int IList.IndexOf(object? value)
    {
        return IndexOf((SystemMenuItemValues)value!);
    }

    /// <summary>
    /// Inserts an item to the collection at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index at which value should be inserted.</param>
    /// <param name="value">The object to insert into the collection.</param>
    void IList.Insert(int index, object? value)
    {
        Insert(index, (SystemMenuItemValues)value!);
    }

    /// <summary>
    /// Removes the first occurrence of a specific object from the collection.
    /// </summary>
    /// <param name="value">The object to remove from the collection.</param>
    void IList.Remove(object? value)
    {
        Remove((SystemMenuItemValues)value!);
    }

    /// <summary>
    /// Removes the item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item to remove.</param>
    void IList.RemoveAt(int index)
    {
        RemoveAt(index);
    }
    #endregion
    #endregion
}