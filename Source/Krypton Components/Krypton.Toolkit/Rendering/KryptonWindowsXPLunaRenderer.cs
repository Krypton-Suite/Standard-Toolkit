#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// ToolStrip rendering for Windows XP Luna palettes (flat beige bar, simple highlights).
/// </summary>
public sealed class KryptonWindowsXPLunaRenderer : KryptonProfessionalRenderer
{
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonWindowsXPLunaRenderer"/> class.
    /// </summary>
    /// <param name="kct">Color table sourced from the active palette.</param>
    public KryptonWindowsXPLunaRenderer([DisallowNull] KryptonColorTable kct)
        : base(kct)
    {
        RoundedEdges = false;
    }
}
