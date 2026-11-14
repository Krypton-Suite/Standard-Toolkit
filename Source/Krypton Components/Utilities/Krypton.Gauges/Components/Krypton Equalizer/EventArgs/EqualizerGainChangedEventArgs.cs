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
/// Provides the band index and gain values for <see cref="KryptonEqualizer.GainChanged"/>.
/// </summary>
public sealed class EqualizerGainChangedEventArgs : EventArgs
{
    /// <summary>
    /// Initialize a new instance of the <see cref="EqualizerGainChangedEventArgs"/> class.
    /// </summary>
    /// <param name="bandIndex">Zero-based band index.</param>
    /// <param name="oldGain">Previous gain in decibels.</param>
    /// <param name="newGain">New gain in decibels.</param>
    public EqualizerGainChangedEventArgs(int bandIndex, double oldGain, double newGain)
    {
        BandIndex = bandIndex;
        OldGain = oldGain;
        NewGain = newGain;
    }

    /// <summary>
    /// Gets the zero-based band index.
    /// </summary>
    public int BandIndex { get; }

    /// <summary>
    /// Gets the previous gain in decibels.
    /// </summary>
    public double OldGain { get; }

    /// <summary>
    /// Gets the new gain in decibels.
    /// </summary>
    public double NewGain { get; }
}
