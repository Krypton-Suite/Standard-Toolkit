#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// How <see cref="KryptonTransferProgressPanel"/> arranges speed and progress charts.
/// </summary>
public enum TransferProgressChartLayout
{
    /// <summary>Separate speed and progress charts side by side.</summary>
    SideBySide = 0,

    /// <summary>Single dual-series chart (speed + optional progress).</summary>
    DualSeries = 1
}
