#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provide inheritance of palette ribbon general properties.
/// </summary>
public abstract class PaletteRibbonFileAppTabInherit : GlobalId, IPaletteRibbonFileAppTab
{
    /// <inheritdoc />
    public abstract Color GetRibbonFileAppTabTopColor(PaletteState state);

    /// <inheritdoc />
    public abstract Color GetRibbonFileAppTabBottomColor(PaletteState state);

    /// <inheritdoc />
    public abstract Color GetRibbonFileAppTabTextColor(PaletteState state);
}