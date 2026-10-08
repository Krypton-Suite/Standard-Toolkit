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
/// Windows XP Royale Noir scheme tokens (dark chrome with orange accents).
/// </summary>
public sealed class PaletteWindowsXPRoyaleNoir_BaseScheme : PaletteWindowsXPDarkVisualStyle_BaseScheme
{
    private static readonly Color AccentOrange = Color.FromArgb(232, 92, 0);
    private static readonly Color AccentOrangeLight = Color.FromArgb(255, 140, 50);

    public PaletteWindowsXPRoyaleNoir_BaseScheme()
    {
        ButtonNormalDefaultBack1 = Color.FromArgb(180, 85, 20);
        ButtonNormalDefaultBack2 = AccentOrangeLight;
        ButtonNormalDefaultBorder = AccentOrange;

        GridListSelected = Color.FromArgb(200, 100, 30);
        GridDataCellSelected = Color.FromArgb(120, 70, 30);

        FormBorderHeaderActive1 = Color.FromArgb(80, 45, 15);
        FormBorderHeaderActive2 = Color.FromArgb(40, 40, 40);

        RibbonTabSelected1 = Color.FromArgb(120, 70, 25);
        RibbonTabSelected2 = AccentOrangeLight;
        RibbonTabTracking2 = AccentOrange;

        LinkNotVisitedOverrideControl = AccentOrangeLight;
        LinkNotVisitedOverridePanel = AccentOrangeLight;
    }
}
