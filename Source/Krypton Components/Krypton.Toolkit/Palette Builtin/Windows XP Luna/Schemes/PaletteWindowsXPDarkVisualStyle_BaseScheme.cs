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
/// Shared dark chrome tokens for Windows XP Royale Noir and Zune visual styles.
/// </summary>
public class PaletteWindowsXPDarkVisualStyle_BaseScheme : PaletteWindowsXPLunaBlue_BaseScheme
{
    protected PaletteWindowsXPDarkVisualStyle_BaseScheme()
    {
        Color panel = Color.FromArgb(43, 43, 43);
        Color panelAlt = Color.FromArgb(30, 30, 30);
        Color text = Color.FromArgb(220, 220, 220);
        Color buttonFace1 = Color.FromArgb(74, 74, 74);
        Color buttonFace2 = Color.FromArgb(96, 96, 96);

        TextLabelControl = text;
        TextButtonNormal = text;
        TextLabelPanel = text;
        TextButtonChecked = text;
        HeaderText = text;
        StatusStripText = text;
        TextButtonFormNormal = text;
        TextButtonFormTracking = text;
        TextButtonFormPressed = text;

        PanelClient = panel;
        PanelAlternative = panelAlt;
        ControlBorder = Color.FromArgb(96, 96, 96);

        FormHeaderShortActive = Color.FromArgb(26, 26, 26);
        FormHeaderLongActive = Color.FromArgb(64, 64, 64);
        FormHeaderShortInactive = Color.FromArgb(80, 80, 80);
        FormHeaderLongInactive = Color.FromArgb(96, 96, 96);
        FormBorderActive = Color.FromArgb(20, 20, 20);
        FormBorderInactive = Color.FromArgb(70, 70, 70);
        FormBorderHeaderActive1 = Color.FromArgb(48, 48, 48);
        FormBorderHeaderActive2 = Color.FromArgb(26, 26, 26);

        ButtonNormalBack1 = buttonFace1;
        ButtonNormalBack2 = buttonFace2;
        ButtonNormalBorder = Color.FromArgb(110, 110, 110);
        ButtonNormalDefaultBorder = Color.FromArgb(120, 120, 120);

        ToolStripBack = panel;
        ToolStripBegin = Color.FromArgb(55, 55, 55);
        ToolStripMiddle = Color.FromArgb(48, 48, 48);
        ToolStripEnd = Color.FromArgb(35, 35, 35);
        StatusStripLight = Color.FromArgb(55, 55, 55);
        StatusStripDark = Color.FromArgb(35, 35, 35);

        HeaderPrimaryBack1 = Color.FromArgb(58, 58, 58);
        HeaderPrimaryBack2 = Color.FromArgb(42, 42, 42);
        HeaderSecondaryBack1 = Color.FromArgb(50, 50, 50);
        HeaderSecondaryBack2 = Color.FromArgb(50, 50, 50);

        GridListNormal1 = Color.FromArgb(50, 50, 50);
        GridListNormal2 = Color.FromArgb(43, 43, 43);
        GridDataCellBorder = Color.FromArgb(70, 70, 70);

        InputControlBackNormal = panelAlt;
        InputControlBackInactive = panel;
        InputControlTextNormal = text;
        InputControlBorderNormal = Color.FromArgb(96, 96, 96);

        RibbonGroupsArea1 = Color.FromArgb(48, 48, 48);
        RibbonGroupsArea2 = Color.FromArgb(43, 43, 43);
        RibbonGroupsArea3 = Color.FromArgb(38, 38, 38);

        ContextMenuHeadingBack = Color.FromArgb(55, 55, 55);
        ContextMenuHeadingText = text;
        ImageMargin = Color.FromArgb(40, 40, 40);
    }
}
