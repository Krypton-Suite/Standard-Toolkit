#region BSD License
/*
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Adapts designer collection overrides between in-process <see cref="ICollection"/>
/// and WinForms Designer SDK <c>IReadOnlyCollection{IComponent}</c>.
/// </summary>
#if KRYPTON_WINFORMS_DESIGNER_SDK
public static class KryptonDesignerSdkCompat
#else
internal static class KryptonDesignerSdkCompat
#endif
{
#if KRYPTON_WINFORMS_DESIGNER_SDK
    /// <summary>
    /// Copies associated design-time components into an SDK-compatible collection.
    /// </summary>
    /// <param name="source">Existing associated-component sequence.</param>
    /// <returns>A read-only collection of <see cref="IComponent"/> instances.</returns>
    public static IReadOnlyCollection<IComponent> Associated(IEnumerable? source)
    {
        if (source is IReadOnlyCollection<IComponent> typed)
        {
            return typed;
        }

        var list = new List<IComponent>();
        if (source is null)
        {
            return list;
        }

        foreach (object item in source)
        {
            if (item is IComponent component)
            {
                list.Add(component);
            }
        }

        return list;
    }

    /// <summary>
    /// Returns <paramref name="preferred"/> when present; otherwise <paramref name="fallback"/>.
    /// </summary>
    /// <param name="preferred">Preferred associated components, or <see langword="null"/>.</param>
    /// <param name="fallback">Fallback associated components.</param>
    /// <returns>A read-only collection of <see cref="IComponent"/> instances.</returns>
    public static IReadOnlyCollection<IComponent> Associated(IEnumerable? preferred, IEnumerable fallback) =>
        Associated(preferred ?? fallback);
#else
    /// <summary>
    /// Returns an <see cref="ICollection"/> for in-process designers.
    /// </summary>
    /// <param name="source">Existing associated-component sequence.</param>
    /// <returns>The original collection when possible; otherwise a new <see cref="ArrayList"/>.</returns>
    internal static ICollection Associated(IEnumerable? source)
    {
        if (source is ICollection collection)
        {
            return collection;
        }

        var list = new ArrayList();
        if (source is not null)
        {
            foreach (object item in source)
            {
                list.Add(item);
            }
        }

        return list;
    }

    /// <summary>
    /// Returns <paramref name="preferred"/> when present; otherwise <paramref name="fallback"/>.
    /// </summary>
    /// <param name="preferred">Preferred associated components, or <see langword="null"/>.</param>
    /// <param name="fallback">Fallback associated components.</param>
    /// <returns>An <see cref="ICollection"/> of associated components.</returns>
    internal static ICollection Associated(IEnumerable? preferred, IEnumerable fallback) =>
        Associated(preferred ?? fallback);
#endif

    /// <summary>
    /// Copies a sequence into an <see cref="ArrayList"/>.
    /// SDK <c>AssociatedComponents</c> is not an <see cref="ICollection"/>, so the ArrayList constructor cannot take it.
    /// </summary>
    /// <param name="source">Source sequence.</param>
    /// <returns>A new <see cref="ArrayList"/> containing the source items.</returns>
    public static ArrayList ToArrayList(IEnumerable? source)
    {
        var list = new ArrayList();
        if (source is null)
        {
            return list;
        }

        foreach (object item in source)
        {
            list.Add(item);
        }

        return list;
    }
}
