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
/// Windows XP Zune scheme tokens (dark chrome with Zune orange accents).
/// </summary>
public sealed class PaletteWindowsXPZune_BaseScheme : PaletteWindowsXPDarkVisualStyle_BaseScheme
{
    private static readonly Color ZuneOrange = Color.FromArgb(247, 148, 29);
    private static readonly Color ZuneOrangeBright = Color.FromArgb(255, 170, 60);

    public PaletteWindowsXPZune_BaseScheme()
    {
        PanelClient = Color.FromArgb(30, 30, 30);
        PanelAlternative = Color.FromArgb(24, 24, 24);

        ButtonNormalDefaultBack1 = Color.FromArgb(200, 110, 20);
        ButtonNormalDefaultBack2 = ZuneOrangeBright;
        ButtonNormalDefaultBorder = ZuneOrange;
        ButtonNormalBack1 = Color.FromArgb(65, 65, 65);
        ButtonNormalBack2 = Color.FromArgb(85, 85, 85);

        GridListSelected = ZuneOrange;
        GridDataCellSelected = Color.FromArgb(140, 80, 20);
        GridListPressed1 = ZuneOrangeBright;
        GridListPressed2 = Color.FromArgb(255, 190, 100);

        FormHeaderShortActive = Color.FromArgb(18, 18, 18);
        FormHeaderLongActive = Color.FromArgb(247, 148, 29);
        FormBorderHeaderActive2 = ZuneOrange;

        RibbonTabSelected1 = Color.FromArgb(140, 80, 20);
        RibbonTabSelected2 = ZuneOrangeBright;
        RibbonTabTracking2 = ZuneOrange;
        RibbonGroupsArea5 = ZuneOrange;

        StatusStripLight = Color.FromArgb(45, 45, 45);
        StatusStripDark = Color.FromArgb(24, 24, 24);

        LinkNotVisitedOverrideControl = ZuneOrangeBright;
        LinkNotVisitedOverridePanel = ZuneOrangeBright;
        LinkPressedOverrideControl = Color.FromArgb(255, 200, 120);
        LinkPressedOverridePanel = Color.FromArgb(255, 200, 120);
    }
}
