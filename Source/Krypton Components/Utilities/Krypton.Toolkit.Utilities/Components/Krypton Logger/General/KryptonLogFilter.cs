#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Global minimum level plus per-category prefix overrides (longest matching prefix wins).
/// </summary>
internal sealed class KryptonLogFilter
{
    private readonly KryptonLogLevel _minimumLevel;
    private readonly (string Prefix, KryptonLogLevel Level)[] _overrides;

    public KryptonLogFilter(KryptonLogLevel minimumLevel, IList<(string Prefix, KryptonLogLevel Level)>? overrides)
    {
        _minimumLevel = minimumLevel;
        if (overrides == null || overrides.Count == 0)
        {
            _overrides = Array.Empty<(string, KryptonLogLevel)>();
            return;
        }

        var copy = new (string Prefix, KryptonLogLevel Level)[overrides.Count];
        for (var i = 0; i < overrides.Count; i++)
        {
            copy[i] = (overrides[i].Prefix ?? string.Empty, overrides[i].Level);
        }

        Array.Sort(copy, (a, b) => b.Prefix.Length.CompareTo(a.Prefix.Length));
        _overrides = copy;
    }

    public bool IsEnabled(string category, KryptonLogLevel level) =>
        level >= Resolve(category);

    private KryptonLogLevel Resolve(string category)
    {
        if (_overrides.Length == 0)
        {
            return _minimumLevel;
        }

        category ??= string.Empty;
        foreach (var item in _overrides)
        {
            if (category.StartsWith(item.Prefix, StringComparison.OrdinalIgnoreCase))
            {
                return item.Level;
            }
        }

        return _minimumLevel;
    }
}
