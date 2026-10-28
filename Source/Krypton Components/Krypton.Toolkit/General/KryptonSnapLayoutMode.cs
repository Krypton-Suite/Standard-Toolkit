#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Chooses which maximize-button layout menu a <see cref="KryptonForm"/> shows.
/// </summary>
/// <remarks>
/// <para>
/// <see cref="Inherit"/> follows <see cref="KryptonManager.GlobalSnapLayouts"/>.
/// <see cref="System"/> is that global default and keeps the Windows 11 snap menu.
/// Set the manager, or one form, to <see cref="Toolkit"/> for the palette menu.
/// Win+Z remains the shell menu in every mode.
/// </para>
/// </remarks>
public enum KryptonSnapLayoutMode
{
    /// <summary>
    /// Show the palette-drawn layout menu and snap this form to the chosen work-area zone.
    /// </summary>
    Toolkit = 0,

    /// <summary>
    /// Keep the shell maximize hit-test so Windows 11 draws its own snap menu.
    /// </summary>
    System = 1,

    /// <summary>
    /// Maximize and restore only. The shell snap menu is not requested.
    /// </summary>
    None = 2,

    /// <summary>
    /// Use <see cref="KryptonManager.GlobalSnapLayouts"/>. On the manager itself this is treated as <see cref="System"/>.
    /// </summary>
    Inherit = 3
}
