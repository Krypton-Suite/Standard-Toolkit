#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Gauges;

/// <summary>
/// Collection of coloured bands for an analogue gauge.
/// </summary>
public class GaugeRangeCollection : CollectionBase
{
    #region Instance Fields

    private readonly GaugeValues _owner;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="GaugeRangeCollection"/> class.
    /// </summary>
    /// <param name="owner">Owning values storage.</param>
    public GaugeRangeCollection(GaugeValues owner) => _owner = owner;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the range at the specified index.
    /// </summary>
    /// <param name="index">Zero-based index.</param>
    public GaugeRange this[int index]
    {
        get => (GaugeRange)List[index]!;
        set
        {
            value.SetOwner(this);
            List[index] = value;
        }
    }

    /// <summary>
    /// Adds a range to the collection.
    /// </summary>
    /// <param name="range">Range to add.</param>
    /// <returns>Index of the new item.</returns>
    public int Add(GaugeRange range)
    {
        range.SetOwner(this);
        return List.Add(range);
    }

    /// <summary>
    /// Inserts a range at the specified index.
    /// </summary>
    /// <param name="index">Zero-based index.</param>
    /// <param name="range">Range to insert.</param>
    public void Insert(int index, GaugeRange range)
    {
        range.SetOwner(this);
        List.Insert(index, range);
    }

    /// <summary>
    /// Removes a range from the collection.
    /// </summary>
    /// <param name="range">Range to remove.</param>
    public void Remove(GaugeRange range) => List.Remove(range);

    /// <summary>
    /// Returns whether the collection contains the range.
    /// </summary>
    /// <param name="range">Range to locate.</param>
    public bool Contains(GaugeRange range) => List.Contains(range);

    /// <summary>
    /// Copies the ranges into an array.
    /// </summary>
    /// <param name="array">Destination array.</param>
    /// <param name="index">Starting index in <paramref name="array"/>.</param>
    public void CopyTo(GaugeRange[] array, int index) => List.CopyTo(array, index);

    #endregion

    #region Internal

    internal void OnRangeChanged() => _owner.OnRangesChanged();

    internal bool IsDefault => Count == 0;

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override void OnClearComplete()
    {
        base.OnClearComplete();
        OnRangeChanged();
    }

    /// <inheritdoc />
    protected override void OnInsertComplete(int index, object? value)
    {
        base.OnInsertComplete(index, value);
        if (value is GaugeRange range)
        {
            range.SetOwner(this);
        }

        OnRangeChanged();
    }

    /// <inheritdoc />
    protected override void OnRemoveComplete(int index, object? value)
    {
        base.OnRemoveComplete(index, value);
        if (value is GaugeRange range)
        {
            range.SetOwner(null);
        }

        OnRangeChanged();
    }

    /// <inheritdoc />
    protected override void OnSetComplete(int index, object? oldValue, object? newValue)
    {
        base.OnSetComplete(index, oldValue, newValue);
        if (oldValue is GaugeRange oldRange)
        {
            oldRange.SetOwner(null);
        }

        if (newValue is GaugeRange newRange)
        {
            newRange.SetOwner(this);
        }

        OnRangeChanged();
    }

    #endregion
}
