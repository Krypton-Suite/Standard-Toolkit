#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Shared enum-cycling engine used by <see cref="KryptonEnumButton"/> and
/// <see cref="KryptonEnumCommandLinkButton"/>. Owns the enum type, the active (filtered and ordered)
/// list of values, the selected index, and the clamp / wrap-around cycling maths. It intentionally
/// knows nothing about display text or events - the host controls resolve text and raise events so
/// that both controls stay behaviourally identical while rendering differently.
/// </summary>
internal sealed class EnumButtonValueCycler
{
    #region Instance Fields

    private Type? _enumType;
    private FieldInfo[] _allFields;
    private FieldInfo[] _fields;
    private object[] _values;
    private int _selectedIndex;
    private bool _wrapAround;
    private EnumButtonSortOrder _sortOrder;
    private object[]? _excludedValues;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="EnumButtonValueCycler" /> class.</summary>
    public EnumButtonValueCycler()
    {
        _allFields = Array.Empty<FieldInfo>();
        _fields = Array.Empty<FieldInfo>();
        _values = Array.Empty<object>();
        _selectedIndex = 0;
        _wrapAround = true;
        _sortOrder = EnumButtonSortOrder.Declaration;
    }

    #endregion

    #region Public

    /// <summary>Gets the current enumeration type, or <see langword="null"/> when unset.</summary>
    public Type? EnumType => _enumType;

    /// <summary>Gets the number of active (cyclable) values.</summary>
    public int Count => _values.Length;

    /// <summary>Gets the current selected index.</summary>
    public int SelectedIndex => _selectedIndex;

    /// <summary>Gets the current selected value, or <see langword="null"/> when there are no values.</summary>
    public object? SelectedValue => _selectedIndex >= 0 && _selectedIndex < _values.Length ? _values[_selectedIndex] : null;

    /// <summary>Gets the field backing the current selected value, or <see langword="null"/>.</summary>
    public FieldInfo? SelectedField => _selectedIndex >= 0 && _selectedIndex < _fields.Length ? _fields[_selectedIndex] : null;

    /// <summary>Gets or sets a value indicating whether cycling wraps around at the ends.</summary>
    public bool WrapAround
    {
        get => _wrapAround;
        set => _wrapAround = value;
    }

    /// <summary>Gets the field at the supplied active index, or <see langword="null"/>.</summary>
    /// <param name="index">The active index.</param>
    public FieldInfo? FieldAt(int index) => index >= 0 && index < _fields.Length ? _fields[index] : null;

    /// <summary>Gets the value at the supplied active index, or <see langword="null"/>.</summary>
    /// <param name="index">The active index.</param>
    public object? ValueAt(int index) => index >= 0 && index < _values.Length ? _values[index] : null;

    /// <summary>Sets the enumeration type, rebuilding the active value list.</summary>
    /// <param name="value">The enum type, or <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if the type changed and the host should refresh its text.</returns>
    /// <exception cref="ArgumentException">The supplied type is not an enumeration type.</exception>
    public bool SetEnumType(Type? value)
    {
        if (value is not null && !value.IsEnum)
        {
            throw new ArgumentException($@"The type '{value.FullName}' is not an enumeration type.", nameof(value));
        }

        if (ReferenceEquals(_enumType, value))
        {
            return false;
        }

        // A genuine switch between two different enum types resets to the first value; the initial
        // assignment (previous type was null) keeps the current index so designer-serialized
        // SelectedIndex values survive.
        var hadPreviousType = _enumType is not null;

        _enumType = value;
        _allFields = value is null
            ? Array.Empty<FieldInfo>()
            : value.GetFields(BindingFlags.Public | BindingFlags.Static);

        RebuildActive();

        _selectedIndex = hadPreviousType ? Clamp(0) : Clamp(_selectedIndex);

        return true;
    }

    /// <summary>Gets or sets the sort order of the active value list, preserving the current value.</summary>
    public EnumButtonSortOrder SortOrder
    {
        get => _sortOrder;
        set
        {
            if (_sortOrder != value)
            {
                _sortOrder = value;
                RebuildPreservingSelection();
            }
        }
    }

    /// <summary>Gets or sets the values excluded from the cycle, preserving the current value where possible.</summary>
    public IReadOnlyList<object>? ExcludedValues
    {
        get => _excludedValues;
        set
        {
            _excludedValues = value is null || value.Count == 0 ? null : value.ToArray();
            RebuildPreservingSelection();
        }
    }

    /// <summary>Clamps an index into the valid active range.</summary>
    /// <param name="index">The requested index.</param>
    /// <returns>A valid index, or <c>0</c> when there are no values.</returns>
    public int Clamp(int index)
    {
        if (_values.Length == 0)
        {
            return 0;
        }

        if (index < 0)
        {
            return 0;
        }

        return index >= _values.Length ? _values.Length - 1 : index;
    }

    /// <summary>Computes the target index after cycling by the supplied direction, without committing.</summary>
    /// <param name="direction">+1 to move forwards, -1 to move backwards.</param>
    /// <returns>The target index (equal to <see cref="SelectedIndex"/> when there are no values).</returns>
    public int GetCycleTargetIndex(int direction)
    {
        if (_values.Length == 0)
        {
            return _selectedIndex;
        }

        var next = _selectedIndex + direction;

        if (_wrapAround)
        {
            // Positive modulo so negative indexes wrap to the end.
            next = ((next % _values.Length) + _values.Length) % _values.Length;
        }
        else if (next < 0)
        {
            next = 0;
        }
        else if (next >= _values.Length)
        {
            next = _values.Length - 1;
        }

        return next;
    }

    /// <summary>Commits a new selected index (clamped).</summary>
    /// <param name="index">The requested index.</param>
    /// <returns><see langword="true"/> if the selected index actually changed.</returns>
    public bool CommitIndex(int index)
    {
        var clamped = Clamp(index);
        if (clamped == _selectedIndex)
        {
            return false;
        }

        _selectedIndex = clamped;
        return true;
    }

    /// <summary>Finds the active index of the supplied value.</summary>
    /// <param name="value">The value to locate.</param>
    /// <returns>The active index, or <c>-1</c> when not present.</returns>
    public int IndexOfValue(object? value) => value is null ? -1 : Array.IndexOf(_values, value);

    #endregion

    #region Implementation

    /// <summary>Rebuilds the active list, then re-locates (or clamps) the previously selected value.</summary>
    private void RebuildPreservingSelection()
    {
        var current = SelectedValue;
        RebuildActive();
        _selectedIndex = current is null ? Clamp(_selectedIndex) : Clamp(Math.Max(0, IndexOfValue(current)));
    }

    /// <summary>Rebuilds the active field / value arrays from the declared members, applying excludes and ordering.</summary>
    private void RebuildActive()
    {
        if (_enumType is null || _allFields.Length == 0)
        {
            _fields = Array.Empty<FieldInfo>();
            _values = Array.Empty<object>();
            return;
        }

        var active = new List<FieldInfo>(_allFields.Length);
        foreach (var field in _allFields)
        {
            if (_excludedValues is not null && Array.IndexOf(_excludedValues, field.GetValue(null)) >= 0)
            {
                continue;
            }

            active.Add(field);
        }

        switch (_sortOrder)
        {
            case EnumButtonSortOrder.Value:
                active.Sort(static (a, b) => Comparer<object>.Default.Compare(a.GetValue(null), b.GetValue(null)));
                break;
            case EnumButtonSortOrder.Alphabetical:
                active.Sort(static (a, b) => string.Compare(a.Name, b.Name, StringComparison.CurrentCulture));
                break;
            case EnumButtonSortOrder.Declaration:
            default:
                // Keep declaration order.
                break;
        }

        _fields = active.ToArray();
        _values = new object[_fields.Length];
        for (var i = 0; i < _fields.Length; i++)
        {
            _values[i] = _fields[i].GetValue(null)!;
        }
    }

    #endregion
}
