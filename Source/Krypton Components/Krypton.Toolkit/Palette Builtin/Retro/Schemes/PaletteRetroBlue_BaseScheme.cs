#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Norton Commander style blue DOS palette (bright blue workspace, cyan highlights).
/// </summary>
public sealed class PaletteRetroBlue_BaseScheme : PaletteRetroGreen_BaseScheme
{
    private static readonly Color DosBlue = Color.FromArgb(0, 0, 170);
    private static readonly Color DosBlueDark = Color.FromArgb(0, 0, 128);
    private static readonly Color DosCyan = Color.FromArgb(0, 170, 170);
    private static readonly Color DosYellow = Color.FromArgb(255, 255, 0);

    public PaletteRetroBlue_BaseScheme()
    {
        PanelClient = DosBlue;
        PanelAlternative = Color.FromArgb(192, 192, 192);
        ButtonNormalBack1 = DosCyan;
        ButtonNormalBack2 = DosCyan;
        ButtonNormalNavigatorBack1 = DosBlue;
        ButtonNormalNavigatorBack2 = DosBlue;
        ButtonNavigatorTrack1 = DosCyan;
        ButtonNavigatorTrack2 = DosCyan;
        ButtonNavigatorChecked1 = DosCyan;
        ButtonNavigatorChecked2 = DosCyan;
        NavigatorMiniBackColor = DosBlue;

        TextLabelControl = Color.White;
        TextLabelPanel = Color.White;
        TextButtonNormal = Color.White;
        TextButtonChecked = DosYellow;
        TextButtonFormNormal = Color.White;
        TextButtonFormTracking = DosYellow;
        TextButtonFormPressed = DosYellow;
        ButtonNavigatorText = Color.White;

        StatusStripText = Color.White;
        FormBorderActive = DosBlue;
        FormBorderHeaderActive = DosBlue;
        FormHeaderShortActive = Color.White;
        FormHeaderLongActive = Color.White;

        GridListNormal1 = DosBlue;
        GridListNormal2 = DosBlueDark;
        GridListSelected = DosCyan;
        GridListPressed1 = DosCyan;
        GridListPressed2 = DosCyan;
        GridDataCellBorder = Color.Black;
        GridDataCellSelected = DosCyan;

        InputControlBackNormal = DosBlueDark;
        InputControlBackInactive = DosBlue;
        InputControlBackDisabled = DosBlueDark;
        InputControlTextNormal = Color.White;
        InputControlTextDisabled = Color.FromArgb(128, 128, 128);
        InputDropDownNormal1 = Color.White;
        InputDropDownDisabled1 = Color.FromArgb(128, 128, 128);

        ContextMenuHeadingBack = DosBlueDark;
        ContextMenuImageColumn = DosBlue;

        RibbonQATMini1 = DosBlue;
        RibbonQATMini2 = DosBlue;
        RibbonQATMini3 = Color.Black;
        RibbonQATMini4 = GlobalStaticVariables.EMPTY_COLOR;
        RibbonQATMini5 = GlobalStaticVariables.EMPTY_COLOR;
        RibbonQATMini1I = DosBlueDark;
        RibbonQATMini2I = DosBlueDark;
        RibbonQATMini3I = Color.Black;
        RibbonQATMini4I = GlobalStaticVariables.EMPTY_COLOR;
        RibbonQATMini5I = GlobalStaticVariables.EMPTY_COLOR;
        RibbonQATFullbar1 = DosBlue;
        RibbonQATFullbar2 = DosBlue;
        RibbonQATFullbar3 = Color.Black;
        RibbonQATButtonDark = Color.White;
        RibbonQATButtonLight = Color.FromArgb(192, 192, 192);
        RibbonQATOverflow1 = DosBlue;
        RibbonQATOverflow2 = DosBlue;

        ToolStripBack = DosBlueDark;
        HeaderPrimaryBack1 = DosBlueDark;
        HeaderPrimaryBack2 = DosBlue;
        HeaderText = Color.White;

        RibbonTabTextNormal = Color.White;
        RibbonTabTextChecked = Color.Black;
        RibbonGroupCollapsedText = Color.White;
        RibbonGroupTitleText = Color.White;
        ContextMenuHeadingText = Color.White;
        AppButtonMenuDocsText = Color.White;

        LinkNotVisitedOverridePanel = DosYellow;
        LinkVisitedOverridePanel = Color.FromArgb(255, 170, 255);
        LinkPressedOverridePanel = Color.FromArgb(255, 85, 85);
        LinkNotVisitedOverrideControl = DosYellow;
    }
}
