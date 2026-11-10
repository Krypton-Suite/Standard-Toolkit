#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Charting;

/// <summary>
/// How a <see cref="KryptonTimeline"/> places text and icons around the connector.
/// </summary>
public enum TimelineLayout
{
    /// <summary>
    /// Start text on one side of the connector and end text on the other. The icon sits on the connector.
    /// </summary>
    Alternate = 0,

    /// <summary>
    /// Start and end text stack on one side of the connector.
    /// </summary>
    Compact = 1,

    /// <summary>
    /// Same sides as <see cref="Alternate"/>, with the icon pulled to the start of each item.
    /// </summary>
    SnapIcon = 2
}
