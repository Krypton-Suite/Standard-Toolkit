#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class TimedCache<TKey, TItem>(TimeSpan expirationTime) where TKey : notnull
{
    private readonly Dictionary<TKey, (DateTime expiresAt, TItem)> _cache = new Dictionary<TKey, (DateTime expiresAt, TItem)>();
    private readonly object _cacheLock = new object(); // Supposed to be faster than `ReaderWriterLockSlim`
    private DateTime _lastExpirationScan;

    public void Remove(TKey key)
    {
        // TODO: Should use concurrency !
        _cache.Remove(key);
    }

    public TItem GetOrCreate(TKey key, Func<TItem> createItem)
    {
        DateTime utcNow = DateTime.UtcNow;
        TItem cacheItem;
        try
        {
            Monitor.Enter(_cacheLock);
            // Look for cache key.
            cacheItem = !_cache.TryGetValue(key, out var cacheEntry) 
                ? createItem() 
                : cacheEntry.Item2;
            // Make sure timestamp is updated
            Add(key, cacheItem);
        }
        finally
        {
            // Ensure that the lock is released.
            Monitor.Exit(_cacheLock);
        }
        StartScanForExpiredItemsIfNeeded(utcNow);
        return cacheItem;
    }

    /// <summary>Adds the specified key and value to the dictionary.</summary>
    public void Add(TKey key, TItem value)
    {
        _cache[key] = (DateTime.UtcNow + expirationTime, value);
    }

    // If sufficient time has elapsed then a scan is initiated on a background task.
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void StartScanForExpiredItemsIfNeeded(DateTime utcNow)
    {
        if (expirationTime < utcNow - _lastExpirationScan)
        {
            ScheduleTask();
        }

        void ScheduleTask()
        {
            Task.Factory.StartNew(state => ScanForExpiredItems(), this,
                CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ScanForExpiredItems()
    {
        // Check to see if we are already scheduled, and do not enter again
        if ( Monitor.TryEnter(_cacheLock) )
        {
            _lastExpirationScan = DateTime.UtcNow;
            foreach (var kvp in _cache.ToList())
            {
                (DateTime expiresAt, _) = kvp.Value;

                // TODO: Should use concurrency !
                if (expiresAt < _lastExpirationScan)
                {
                    _cache.Remove(kvp.Key);
                }
            }

            Monitor.Exit(_cacheLock);
        }
    }

}