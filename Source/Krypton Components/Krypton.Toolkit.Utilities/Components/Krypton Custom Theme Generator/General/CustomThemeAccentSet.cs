#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Derived accent colours used both to overwrite scheme slots and to patch button LUT states after snapshot.
/// </summary>
internal sealed class CustomThemeAccentSet
{
    internal Color Primary { get; set; }
    internal Color Secondary { get; set; }
    internal Color Surface { get; set; }
    internal Color SurfaceAlt { get; set; }
    internal Color ButtonBack1 { get; set; }
    internal Color ButtonBack2 { get; set; }
    internal Color ButtonBorder { get; set; }
    internal Color HoverTop { get; set; }
    internal Color HoverBottom { get; set; }
    internal Color HoverBorder { get; set; }
    internal Color PressedTop { get; set; }
    internal Color PressedBottom { get; set; }
    internal Color PressedBorder { get; set; }
    internal Color CheckedTop { get; set; }
    internal Color CheckedBottom { get; set; }
    internal Color CheckedBorder { get; set; }
    internal Color DisabledTop { get; set; }
    internal Color DisabledBottom { get; set; }
    internal Color DisabledBorder { get; set; }
    internal Color OnAccent { get; set; }
    internal Color OnSurface { get; set; }
    internal Color MutedText { get; set; }
    internal Color Link { get; set; }
    internal Color LinkVisited { get; set; }
    internal Color LinkPressed { get; set; }
    internal Color InputBack { get; set; }
    internal Color InputBackDisabled { get; set; }
    internal Color InputBorder { get; set; }
    internal Color HeaderBack1 { get; set; }
    internal Color HeaderBack2 { get; set; }
    internal Color HeaderSecondary1 { get; set; }
    internal Color HeaderSecondary2 { get; set; }
    internal Color FormBorder { get; set; }
    internal Color FormBorderInactive { get; set; }
    internal bool Dark { get; set; }
}
