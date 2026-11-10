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
/// Raised when one or more spectrum band levels change.
/// </summary>
public sealed class EqualizerLevelsChangedEventArgs : EventArgs
{
    /// <summary>
    /// Shared empty instance.
    /// </summary>
    public new static readonly EqualizerLevelsChangedEventArgs Empty = new EqualizerLevelsChangedEventArgs();
}
