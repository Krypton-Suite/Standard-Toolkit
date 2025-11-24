#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Manage a collection of specified reference instances.
/// </summary>
public class TypedCollection<T> : IList,
    IList<T>,
    ICollection,
    ICollection<T> where T : class
{
    #region Instance Fields
    private readonly List<T> _list;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when an item is about to be added/inserted to the collection.
    /// </summary>
    public event TypedHandler<T>? Inserting;

    /// <summary>
    /// Occurs when an item has been added/inserted to the collection.
    /// </summary>
    public event TypedHandler<T>? Inserted;

    /// <summary>
    /// Occurs when an item is about to be removed from the collection.
    /// </summary>
    public event TypedHandler<T>? Removing;

    /// <summary>
    /// Occurs when an item is removed from the collection.
    /// </summary>
    public event TypedHandler<T>? Removed;

    /// <summary>
    /// Occurs when an items are about to be removed from the collection.
    /// </summary>
    public event EventHandler? Clearing;

    /// <summary>
    /// Occurs when an items have been removed from the collection.
    /// </summary>
    public event EventHandler? Cleared;

    /// <summary>
    /// Occurs when items have been reordered inside the collection.
    /// </summary>
    public event EventHandler? Reordered;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the TypedCollection class.
    /// </summary>
    public TypedCollection() =>
        // Create internal storage
        _list = new List<T>(4);

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() => $"{Count} TypedCollection";

    #endregion

    #region AddRange
    /// <summary>
    /// Append an array of items.
    /// </summary>
    /// <param name="itemArray">Array of items to add.</param>
    public virtual void AddRange(T[] itemArray)
    {
        // Just add each item in the array in turn
        foreach (T item in itemArray)
        {
            Add(item);
        }
    }
    #endregion

    #region IList
    /// <summary>
    /// Append an item to the collection.
    /// </summary>
    /// <param name="value">Object reference.</param>
    /// <returns>The position into which the new item was inserted.</returns>
    public virtual int Add(object? value)
    {
        // Use strongly typed implementation
        Add((value as T)!);

        // Index is the last item in the collection
        return Count - 1;
    }

    /// <summary>
    /// Determines whether the collection contains the item.
    /// </summary>
    /// <param name="value">Object reference.</param>
    /// <returns>True if item found; otherwise false.</returns>
    public bool Contains(object? value) =>
        // Use strongly typed implementation
        Contains((value as T)!);

    /// <summary>
    /// Determines the index of the specified item in the collection.
    /// </summary>
    /// <param name="value">Object reference.</param>
    /// <returns>-1 if not found; otherwise index position.</returns>
    public int IndexOf(object? value) =>
        // Use strongly typed implementation
        IndexOf((value as T)!);

    /// <summary>
    /// Inserts an item to the collection at the specified index.
    /// </summary>
    /// <param name="index">Insert index.</param>
    /// <param name="value">Object reference.</param>
    public virtual void Insert(int index, object? value) =>
        // Use strongly typed implementation
        Insert(index, (value as T)!);

    /// <summary>
    /// Gets a value indicating whether the collection has a fixed size. 
    /// </summary>
    public bool IsFixedSize => false;

    /// <summary>
    /// Removes first occurrence of specified item.
    /// </summary>
    /// <param name="value">Object reference.</param>
    public void Remove(object? value) =>
        // Use strongly typed implementation
        Remove((value as T)!);

    /// <summary>
    /// Gets or sets the item at the specified index.
    /// </summary>
    /// <param name="index">Object index.</param>
    /// <returns>Object at specified index.</returns>
    object? IList.this[int index]
    {
        get => _list[index];
        set => throw new NotImplementedException("Cannot set a collection index with a new value");
    }
    #endregion

    #region IList<T>
    /// <summary>
    /// Determines the index of the specified item in the collection.
    /// </summary>
    /// <param name="item">Item reference.</param>
    /// <returns>-1 if not found; otherwise index position.</returns>
    public int IndexOf([DisallowNull] T item)
    {
        Debug.Assert(item != null);
        return _list.IndexOf(item!);
    }

    /// <summary>
    /// Inserts an item to the collection at the specified index.
    /// </summary>
    /// <param name="index">Insert index.</param>
    /// <param name="item">Item reference.</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public virtual void Insert(int index, [DisallowNull] T item)
    {
        Debug.Assert(item != null);

        // We do not allow an empty ribbon tab to be added
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        // Not allow to add the same item more than once
        if (_list.Contains(item))
        {
            throw new ArgumentOutOfRangeException(nameof(item), @"Item already in collection");
        }

        // Generate before insert event
        OnInserting(new TypedCollectionEventArgs<T>(item, index));

        // Add into the internal collection
        _list.Insert(index, item);

        // Generate after insert event
        OnInserted(new TypedCollectionEventArgs<T>(item, index));
    }

    /// <summary>
    /// Removes the item at the specified index.
    /// </summary>
    /// <param name="index">Remove index.</param>
    public void RemoveAt(int index)
    {
        // Cache the item being removed
        T item = this[index];

        // Generate before remove event
        OnRemoving(new TypedCollectionEventArgs<T>(item, index));

        // Remove item from internal collection
        _list.RemoveAt(index);

        // Generate after remove event
        OnRemoved(new TypedCollectionEventArgs<T>(item, index));
    }

    /// <summary>
    /// Gets or sets the item at the specified index.
    /// </summary>
    /// <param name="index">Item index.</param>
    /// <returns>Item at specified index.</returns>
    public T this[int index]
    {
        get => _list[index];

        set => throw new NotImplementedException("Cannot set a collection index with a new value");
    }

    /// <summary>
    /// Gets the item with the provided unique name.
    /// </summary>
    /// <param name="name">Name of the ribbon tab instance.</param>
    /// <returns>Item at specified index.</returns>
    public virtual T? this[string name] => null;

    /// <summary>
    /// Move the source item to be immediately after the target item.
    /// </summary>
    /// <param name="source">Source item to be moved.</param>
    /// <param name="target">Target item to place the source item after.</param>
    public void MoveAfter(T source, T target)
    {
        _list.Remove(source);
        _list.Insert(_list.IndexOf(target) + 1, source);

        // Generate reorder event.
        OnReordered(EventArgs.Empty);
    }

    /// <summary>
    /// Move the source item to be immediately before the target item.
    /// </summary>
    /// <param name="source">Source item to be moved.</param>
    /// <param name="target">Target item to place the source item before.</param>
    public void MoveBefore(T source, T target)
    {
        _list.Remove(source);
        _list.Insert(_list.IndexOf(target), source);

        // Generate reorder event.
        OnReordered(EventArgs.Empty);
    }
    #endregion

    #region ICollection<T>

    /// <summary>
    /// Append an item to the collection.
    /// </summary>
    /// <param name="item">Item reference.</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public virtual void Add([DisallowNull] T item)
    {
        Debug.Assert(item != null);

        // We do not allow an empty item to be added
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        // Not allow to add the same item more than once
        if (_list.Contains(item))
        {
            throw new ArgumentOutOfRangeException(nameof(item), @"Item is already in collection");
        }

        // Generate before insert event
        OnInserting(new TypedCollectionEventArgs<T>(item, _list.Count));

        // Add to the internal collection
        _list.Add(item);

        // Generate inserted event
        OnInserted(new TypedCollectionEventArgs<T>(item, _list.Count - 1));
    }

    /// <summary>
    /// Remove all items from the collection.
    /// </summary>
    public void Clear()
    {
        // Generate before event
        OnClearing(EventArgs.Empty);

        // Remove all entries from internal collection
        _list.Clear();

        // Generate after event
        OnCleared(EventArgs.Empty);
    }

    /// <summary>
    /// Determines whether the collection contains the item.
    /// </summary>
    /// <param name="item">Item reference.</param>
    /// <returns>True if item found; otherwise false.</returns>
    public bool Contains(T? item) => item != null && _list.Contains(item);

    /// <summary>
    /// Copies items to specified array starting at particular index.
    /// </summary>
    /// <param name="array">Target array.</param>
    /// <param name="arrayIndex">Starting array index.</param>
    public void CopyTo([DisallowNull] T[] array, int arrayIndex)
    {
        Debug.Assert(array != null);
        _list.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Gets the number of items in collection.
    /// </summary>
    public int Count => _list.Count;

    /// <summary>
    /// Gets a value indicating whether the collection is read-only.
    /// </summary>
    public bool IsReadOnly => false;

    /// <summary>
    /// Removes first occurrence of specified item.
    /// </summary>
    /// <param name="item">Item reference.</param>
    /// <returns>True if removed; otherwise false.</returns>
    public virtual bool Remove([DisallowNull] T? item)
    {
        bool ret = false;

        if (item is not null)
        {
            Debug.Assert(item is not null);

            // Cache the index of the item
            var index = IndexOf(item!);

            // Generate before event
            OnRemoving(new TypedCollectionEventArgs<T>(item, index));

            // Remove from the internal list
            ret = _list.Remove(item!);

            // Generate after event
            OnRemoved(new TypedCollectionEventArgs<T>(item, index));
        }

        return ret;
    }
    #endregion

    #region ICollection

    /// <summary>
    /// Copies all the elements of the current collection to the specified Array. 
    /// </summary>
    /// <param name="array">The Array that is the destination of the elements copied from the collection.</param>
    /// <param name="index">The index in array at which copying begins.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public void CopyTo([DisallowNull] Array array, int index)
    {
        Debug.Assert(array != null);

        // Cannot pass a null target array
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        // Try and copy each item to the destination array
        foreach (T item in this)
        {
            array.SetValue(item, index++);
        }
    }

    /// <summary>
    /// Gets a value indicating whether access to the collection is synchronized (thread safe).
    /// </summary>
    public bool IsSynchronized => false;

    /// <summary>
    /// Gets an object that can be used to synchronize access to the collection. 
    /// </summary>
    public object SyncRoot => this;

    #endregion

    #region IEnumerable
    /// <summary>
    /// Shallow enumerate over items in the collection.
    /// </summary>
    /// <returns>Enumerator instance.</returns>
    public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

    /// <summary>
    /// Enumerate using non-generic interface.
    /// </summary>
    /// <returns>Enumerator instance.</returns>
    IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();

    #endregion

    #region Protected
    /// <summary>
    /// Raises the Inserting event.
    /// </summary>
    /// <param name="e">A KryptonRibbonTabEventArgs instance containing event data.</param>
    protected virtual void OnInserting(TypedCollectionEventArgs<T> e) => Inserting?.Invoke(this, e);

    /// <summary>
    /// Raises the Inserted event.
    /// </summary>
    /// <param name="e">A TypedCollectionEventArgs instance containing event data.</param>
    protected virtual void OnInserted(TypedCollectionEventArgs<T> e) => Inserted?.Invoke(this, e);

    /// <summary>
    /// Raises the Removing event.
    /// </summary>
    /// <param name="e">A TypedCollectionEventArgs instance containing event data.</param>
    protected virtual void OnRemoving(TypedCollectionEventArgs<T> e) => Removing?.Invoke(this, e);

    /// <summary>
    /// Raises the Removed event.
    /// </summary>
    /// <param name="e">A TypedCollectionEventArgs instance containing event data.</param>
    protected virtual void OnRemoved(TypedCollectionEventArgs<T> e) => Removed?.Invoke(this, e);

    /// <summary>
    /// Raises the Clearing event.
    /// </summary>
    /// <param name="e">An EventArgs instance containing event data.</param>
    protected virtual void OnClearing(EventArgs e) => Clearing?.Invoke(this, e);

    /// <summary>
    /// Raises the Cleared event.
    /// </summary>
    /// <param name="e">An EventArgs instance containing event data.</param>
    protected virtual void OnCleared(EventArgs e) => Cleared?.Invoke(this, e);

    /// <summary>
    /// Raises the Reordered event.
    /// </summary>
    /// <param name="e">An EventArgs instance containing event data.</param>
    protected virtual void OnReordered(EventArgs e) => Reordered?.Invoke(this, e);

    #endregion
}