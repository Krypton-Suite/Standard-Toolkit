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
/// How <see cref="KryptonTransferSpeedGraph"/> formats overlay sample values.
/// </summary>
public enum TransferGraphSampleDisplay
{
    /// <summary>Bytes per second (appends /s).</summary>
    Rate = 0,

    /// <summary>Absolute byte count.</summary>
    Bytes = 1,

    /// <summary>Percent complete (0–100).</summary>
    Percent = 2
}
