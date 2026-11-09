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
/// Scrolls a theme-aware area chart of transfer progress samples (percent or bytes).
/// </summary>
/// <remarks>
/// Part of <c>Krypton.Toolkit.Utilities</c>. Colours inherit from
/// <see cref="PaletteElement.TransferProgressGraph"/> via <see cref="KryptonTransferSpeedGraph.StateCommon"/>.
/// </remarks>
[ToolboxItem(true)]
[DefaultProperty(nameof(Values))]
[Description("Displays a scrolling transfer-progress area chart.")]
public class KryptonTransferProgressGraph : KryptonTransferSpeedGraph
{
    /// <summary>
    /// Creates an empty progress graph with the factory capacity.
    /// </summary>
    public KryptonTransferProgressGraph()
        : base(PaletteElement.TransferProgressGraph)
    {
    }
}
