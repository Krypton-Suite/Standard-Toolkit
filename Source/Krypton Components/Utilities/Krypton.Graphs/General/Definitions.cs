#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Graphs;

/// <summary>
/// Which transfer-speed graph colour is inherited from <see cref="PaletteElement.TransferSpeedGraph"/>.
/// </summary>
internal enum TransferSpeedGraphPaletteColor
{
    /// <summary>Plot background (<see cref="IPaletteElementColor.GetElementColor1"/> of the palette element).</summary>
    Plot = 1,

    /// <summary>Series fill (palette element Color2).</summary>
    Series = 2,

    /// <summary>Grid lines (palette element Color3).</summary>
    Grid = 3,

    /// <summary>Chart frame border (palette element Color4).</summary>
    Border = 4,

    /// <summary>Speed-label text (palette element Color5).</summary>
    Label = 5
}


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
