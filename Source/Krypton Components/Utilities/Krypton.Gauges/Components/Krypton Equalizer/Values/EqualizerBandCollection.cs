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
/// Read-only view of the bands owned by <see cref="EqualizerValues"/>.
/// Band count is changed through <see cref="EqualizerValues.BandCount"/>.
/// </summary>
public sealed class EqualizerBandCollection : IReadOnlyList<EqualizerBand>
{
    private readonly EqualizerValues _owner;

    internal EqualizerBandCollection(EqualizerValues owner) => _owner = owner;

    /// <inheritdoc />
    public int Count => _owner.BandList.Count;

    /// <inheritdoc />
    public EqualizerBand this[int index] => _owner.BandList[index];

    /// <inheritdoc />
    public IEnumerator<EqualizerBand> GetEnumerator() => _owner.BandList.GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}
