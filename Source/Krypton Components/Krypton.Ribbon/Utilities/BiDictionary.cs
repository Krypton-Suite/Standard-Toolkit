#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2022 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Taken and then modified from
/// https://stackoverflow.com/questions/255341/getting-multiple-keys-of-specified-value-of-a-generic-dictionary/255638#255638
/// </summary>
/// <typeparam name="TFirst"></typeparam>
/// <typeparam name="TSecond"></typeparam>
internal class BiDictionary<TFirst, TSecond>
{
    private static readonly IList<TFirst> _emptyFirstList = Array.Empty<TFirst>();
    private static readonly IList<TSecond> _emptySecondList = Array.Empty<TSecond>();

#pragma warning disable CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.
    private readonly IDictionary<TFirst, TSecond> _firstToSecond = new Dictionary<TFirst, TSecond>();
    private readonly IDictionary<TSecond, TFirst> _secondToFirst = new Dictionary<TSecond, TFirst>();
#pragma warning restore CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.


    public BiDictionary(IDictionary<TFirst, TSecond> dictionary)
    {
        if (dictionary == null)
        {
            throw new ArgumentNullException(nameof(dictionary));
        }

        foreach (KeyValuePair<TFirst, TSecond> keyValuePair in dictionary)
        {
            Add(keyValuePair.Key, keyValuePair.Value);
        }
    }

    public void Add(TFirst first, TSecond second)
    {
        _firstToSecond.Add(first, second);
        _secondToFirst.Add(second, first);
    }

    // Note potential ambiguity using indexers (e.g. mapping from int to int)
    // Hence the methods as well...
    public TSecond this[TFirst first] => GetByFirst(first);

    public TFirst this[TSecond second] => GetBySecond(second);

    public TSecond GetByFirst(TFirst first)
    {
        _firstToSecond.TryGetValue(first, out var second);
        return second!;
    }

    public TFirst GetBySecond(TSecond second)
    {
        _secondToFirst.TryGetValue(second, out var first);
        return first!;
    }

    public ICollection<TFirst> GetAllFirsts() => _firstToSecond.Keys;
}