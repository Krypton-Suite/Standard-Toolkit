#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Simple concurrent basic class to store the FocusLostMenuHelper items.<br/>
/// It deploys a ReadWriterLockSlim internally which supports multi concurrent read access.<br/>
/// </summary>
/// <typeparam name="T">Generic storage type for the list.</typeparam>
internal class ConcurrentSimpleList<T>
{
    private readonly ReaderWriterLockSlim _rwLock;
    private readonly List<T> _items;

    public ConcurrentSimpleList() : base()
    {
        _rwLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        _items = [];
    }

    /// <summary>
    /// Get/set items based on the index within  the list.
    /// </summary>
    /// <param name="index">The index of the item to get or set.</param>
    /// <returns></returns>
    public T this[int index]
    {
        get
        {
            _rwLock.EnterReadLock();
            T result = _items[index];
            _rwLock.ExitReadLock();

            return result;
        }

        set
        {
            _rwLock.EnterWriteLock();
            _items[index] = value;
            _rwLock.ExitWriteLock();
        }
    }

    /// <summary>
    /// Returns the number of items in the list.
    /// </summary>
    public int Count
    {
        get
        {
            _rwLock.EnterReadLock();
            int result = _items.Count;
            _rwLock.ExitReadLock();

            return result;
        }
    }

    /// <summary>
    /// Adds an object to the end of the list.
    /// </summary>
    /// <param name="item">The item to add.</param>
    public void Add(T item)
    {
        _rwLock.EnterWriteLock();
        _items.Add(item);
        _rwLock.ExitWriteLock();
    }

    /// <summary>
    /// Adds an object to the end of the list.
    /// </summary>
    /// <param name="item">True if the item was removed, otherwise false. It will also return false if the item was not found.</param>
    public bool Remove(T item)
    {
        _rwLock.EnterWriteLock();
        bool result = _items.Remove(item);
        _rwLock.ExitWriteLock();

        return result;
    }
}
