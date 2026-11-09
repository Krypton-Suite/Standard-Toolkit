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
/// How <see cref="KryptonTransferProgressPanel.SetProgress"/> samples the progress chart.
/// </summary>
public enum TransferProgressSampleMode
{
    /// <summary>Plot percent complete (0–100) when total length is known; otherwise cumulative bytes.</summary>
    Auto = 0,

    /// <summary>Always plot percent complete (0 when total is unknown).</summary>
    Percent = 1,

    /// <summary>Always plot cumulative bytes received.</summary>
    Bytes = 2
}
