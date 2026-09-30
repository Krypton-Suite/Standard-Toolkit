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
/// Windows XP Royale (Energy Blue) scheme tokens.
/// </summary>
public sealed class PaletteWindowsXPRoyale_BaseScheme : PaletteWindowsXPLunaBlue_BaseScheme
{
    public PaletteWindowsXPRoyale_BaseScheme()
    {
        TextLabelControl = Color.FromArgb( 16,  52, 112);
        TextButtonNormal = Color.FromArgb( 16,  52, 112);
        TextLabelPanel = Color.FromArgb( 16,  52, 112);
        HeaderText = Color.FromArgb( 16,  52, 112);
        StatusStripText = Color.White;

        FormHeaderShortActive = Color.FromArgb( 30,  74, 158);
        FormHeaderLongActive = Color.FromArgb( 60, 138, 217);
        FormBorderActive = Color.FromArgb( 20,  60, 130);
        FormBorderHeaderActive1 = Color.FromArgb( 80, 150, 230);
        FormBorderHeaderActive2 = Color.FromArgb( 30,  74, 158);

        ButtonNormalBack1 = Color.FromArgb(140, 190, 240);
        ButtonNormalBack2 = Color.FromArgb(210, 235, 255);
        ButtonNormalDefaultBack1 = Color.FromArgb( 91, 162, 232);
        ButtonNormalDefaultBack2 = Color.FromArgb(170, 220, 255);
        ButtonNormalBorder = Color.FromArgb( 70, 130, 200);

        PanelClient = Color.FromArgb(236, 233, 216);
        ToolStripBack = Color.FromArgb(230, 240, 252);
        ToolStripBegin = Color.FromArgb(220, 235, 255);
        ToolStripMiddle = Color.FromArgb(190, 220, 250);
        ToolStripEnd = Color.FromArgb(120, 170, 230);

        StatusStripLight = Color.FromArgb( 45,  95, 170);
        StatusStripDark = Color.FromArgb( 25,  70, 140);

        GridListSelected = Color.FromArgb( 51, 153, 255);
        GridListNormal2 = Color.FromArgb(210, 230, 255);

        RibbonGroupsArea1 = Color.FromArgb(230, 240, 252);
        RibbonGroupsArea2 = Color.FromArgb(236, 244, 255);
        RibbonGroupsArea3 = Color.FromArgb(200, 225, 250);
    }
}
