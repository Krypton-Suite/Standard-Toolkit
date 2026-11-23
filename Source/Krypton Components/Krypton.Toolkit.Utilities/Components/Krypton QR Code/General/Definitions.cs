#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

#region Enum QRErrorCorrectionLevel

/// <summary>
/// Error correction level for QR codes. Higher levels allow more data recovery but reduce data capacity.
/// </summary>
public enum QRErrorCorrectionLevel
{
    /// <summary>~7% recovery. Maximum data capacity.</summary>
    L = 0,

    /// <summary>~15% recovery. Good balance.</summary>
    M = 1,

    /// <summary>~25% recovery. Better durability.</summary>
    Q = 2,

    /// <summary>~30% recovery. Highest durability, least capacity.</summary>
    H = 3
}

#endregion