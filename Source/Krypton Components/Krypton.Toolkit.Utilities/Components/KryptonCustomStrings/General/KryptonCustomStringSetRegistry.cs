#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Stores strongly-typed custom string sets registered through <see cref="KryptonCustomStrings"/>.
/// </summary>
public static class KryptonCustomStringSetRegistry
{
    #region Static Fields

    private static readonly Dictionary<string, GlobalId> _stringSets =
        new Dictionary<string, GlobalId>(StringComparer.Ordinal);

    private static readonly object _sync = new object();

    #endregion

    #region Public

    /// <summary>
    /// Registers or replaces a strongly-typed string set.
    /// </summary>
    /// <param name="name">The unique registration name.</param>
    /// <param name="stringSet">The string set instance.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="name"/> or <paramref name="stringSet"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is empty or <paramref name="stringSet"/> does not implement <see cref="IKryptonCustomStringSet"/>.</exception>
    public static void Register(string name, GlobalId stringSet)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (stringSet == null)
        {
            throw new ArgumentNullException(nameof(stringSet));
        }

        if (name.Length == 0)
        {
            throw new ArgumentException(@"Registration name cannot be empty.", nameof(name));
        }

        if (!(stringSet is IKryptonCustomStringSet))
        {
            throw new ArgumentException(@"String set must implement IKryptonCustomStringSet.", nameof(stringSet));
        }

        lock (_sync)
        {
            _stringSets[name] = stringSet;
        }
    }

    /// <summary>
    /// Gets a registered string set by name.
    /// </summary>
    /// <typeparam name="T">The expected string set type.</typeparam>
    /// <param name="name">The registration name.</param>
    /// <returns>The registered string set, or <c>null</c> when the name is not registered or the type does not match.</returns>
    public static T? Get<T>(string name)
        where T : GlobalId
    {
        if (TryGet(name, out GlobalId? stringSet) && stringSet is T typedStringSet)
        {
            return typedStringSet;
        }

        return default;
    }

    /// <summary>
    /// Attempts to get a registered string set by name.
    /// </summary>
    /// <param name="name">The registration name.</param>
    /// <param name="stringSet">When this method returns, contains the registered string set if found.</param>
    /// <returns><c>true</c> if the name was registered; otherwise, <c>false</c>.</returns>
    public static bool TryGet(string name, out GlobalId? stringSet)
    {
        if (!string.IsNullOrEmpty(name))
        {
            lock (_sync)
            {
                if (_stringSets.TryGetValue(name, out GlobalId? registeredStringSet))
                {
                    stringSet = registeredStringSet;
                    return true;
                }
            }
        }

        stringSet = null;
        return false;
    }

    /// <summary>
    /// Determines whether a string set is registered for the specified name.
    /// </summary>
    /// <param name="name">The registration name.</param>
    /// <returns><c>true</c> if the name is registered; otherwise, <c>false</c>.</returns>
    public static bool Contains(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return false;
        }

        lock (_sync)
        {
            return _stringSets.ContainsKey(name);
        }
    }

    /// <summary>
    /// Removes a registered string set.
    /// </summary>
    /// <param name="name">The registration name.</param>
    /// <returns><c>true</c> if the string set was removed; otherwise, <c>false</c>.</returns>
    public static bool Unregister(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return false;
        }

        lock (_sync)
        {
            return _stringSets.Remove(name);
        }
    }

    /// <summary>
    /// Resets all registered string sets to their default values.
    /// </summary>
    public static void ResetAll()
    {
        lock (_sync)
        {
            foreach (GlobalId stringSet in _stringSets.Values)
            {
                if (stringSet is IKryptonCustomStringSet resettableStringSet)
                {
                    resettableStringSet.Reset();
                }
            }
        }
    }

    #endregion
}
