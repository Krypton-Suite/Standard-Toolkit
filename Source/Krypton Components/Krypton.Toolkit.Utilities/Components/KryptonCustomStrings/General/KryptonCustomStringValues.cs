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
/// Exposes key/value custom strings that can be localised through <see cref="KryptonCustomStrings"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonCustomStringValues : GlobalId
{
    #region Instance Fields

    private readonly List<KryptonCustomStringEntry> _entries = new List<KryptonCustomStringEntry>();

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonCustomStringValues"/> class.
    /// </summary>
    public KryptonCustomStringValues()
    {
        Reset();
    }

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    /// <summary>
    /// Gets a value indicating if all the strings are default values.
    /// </summary>
    [Browsable(false)]
    public bool IsDefault => _entries.Count == 0;

    /// <summary>
    /// Gets the designer-serializable string entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Custom string entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public List<KryptonCustomStringEntry> Entries => _entries;

    private bool ShouldSerializeEntries() => _entries.Count > 0;

    /// <summary>
    /// Gets or sets the string value for the specified key.
    /// </summary>
    /// <param name="key">The string key.</param>
    /// <returns>The stored value, or an empty string when the key is not found.</returns>
    public string this[string key]
    {
        get
        {
            if (string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }

            foreach (KryptonCustomStringEntry entry in _entries)
            {
                if (string.Equals(entry.Key, key, StringComparison.Ordinal))
                {
                    return entry.Value;
                }
            }

            return string.Empty;
        }

        set => Set(key, value);
    }

    /// <summary>
    /// Sets the string value for the specified key.
    /// </summary>
    /// <param name="key">The string key.</param>
    /// <param name="value">The localizable value.</param>
    public void Set(string key, string value)
    {
        if (string.IsNullOrEmpty(key))
        {
            return;
        }

        string normalizedValue = value ?? string.Empty;

        foreach (KryptonCustomStringEntry entry in _entries)
        {
            if (string.Equals(entry.Key, key, StringComparison.Ordinal))
            {
                entry.Value = normalizedValue;
                return;
            }
        }

        _entries.Add(new KryptonCustomStringEntry
        {
            Key = key,
            Value = normalizedValue
        });
    }

    /// <summary>
    /// Attempts to get the string value for the specified key.
    /// </summary>
    /// <param name="key">The string key.</param>
    /// <param name="value">When this method returns, contains the value if the key was found.</param>
    /// <returns><c>true</c> if the key was found; otherwise, <c>false</c>.</returns>
    public bool TryGetValue(string key, out string value)
    {
        if (!string.IsNullOrEmpty(key))
        {
            foreach (KryptonCustomStringEntry entry in _entries)
            {
                if (string.Equals(entry.Key, key, StringComparison.Ordinal))
                {
                    value = entry.Value;
                    return true;
                }
            }
        }

        value = string.Empty;
        return false;
    }

    /// <summary>
    /// Removes the string value for the specified key.
    /// </summary>
    /// <param name="key">The string key.</param>
    /// <returns><c>true</c> if the key was removed; otherwise, <c>false</c>.</returns>
    public bool Remove(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            return false;
        }

        for (var index = 0; index < _entries.Count; index++)
        {
            if (string.Equals(_entries[index].Key, key, StringComparison.Ordinal))
            {
                _entries.RemoveAt(index);
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Removes all custom strings.
    /// </summary>
    public void Clear() => _entries.Clear();

    /// <summary>
    /// Resets all custom strings.
    /// </summary>
    public void Reset() => Clear();

    #endregion
}
