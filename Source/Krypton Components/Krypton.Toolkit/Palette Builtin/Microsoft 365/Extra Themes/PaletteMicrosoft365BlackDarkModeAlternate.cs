#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege, et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

#region Class: PaletteMicrosoft365BlackDarkModeAlternate
public class PaletteMicrosoft365BlackDarkModeAlternate : PaletteMicrosoft365BlackDarkModeAlternateBase
{
    #region Static Fields

    #region Colors

    private static readonly Color _tabRowBackgroundColor = Color.FromArgb(31, 31, 31);

    #endregion

    #region Ribbon Specific Colors

    private static readonly Color _ribbonAppButtonDarkColor = Color.FromArgb(31, 31, 31);

    private static readonly Color _ribbonAppButtonLightColor = Color.FromArgb(69, 69, 69);

    private static readonly Color _ribbonAppButtonTextColor = Color.White;

    #endregion

    private static readonly ImageList _checkBoxList;
    private static readonly ImageList _galleryButtonList;

    #region Images
    private static readonly Image?[] _radioButtonArray;
    private static readonly Image? _blackDropDownButton = Office2010ArrowResources.Office2010BlackDropDownButton;
    private static readonly Image? _contextMenuSubMenu = Office2010ArrowResources.Office2010BlackContextMenuSub;
    private static readonly Image _formCloseNormal = Office2010ControlBoxResources.Office2010BlackCloseNormal;
    private static readonly Image _formCloseDisabled = Office2010ControlBoxResources.Office2010BlackCloseDisabled;
    private static readonly Image _formCloseActive = Office2010ControlBoxResources.Office2010BlackCloseActive;
    private static readonly Image _formClosePressed = Office2010ControlBoxResources.Office2010BlackClosePressed;
    private static readonly Image _formMaximiseNormal = Office2010ControlBoxResources.Office2010BackMaximiseNormal;
    private static readonly Image _formMaximiseDisabled = Office2010ControlBoxResources.Office2010BlackMaximiseDisabled;
    private static readonly Image _formMaximiseActive = Office2010ControlBoxResources.Office2010BlackMaximiseActive;
    private static readonly Image _formMaximisePressed = Office2010ControlBoxResources.Office2010BlackMaximisePressed;
    private static readonly Image _formMinimiseNormal = Office2010ControlBoxResources.Office2010BlackMinimiseNormal;
    private static readonly Image _formMinimiseActive = Office2010ControlBoxResources.Office2010BlackMinimiseActive;
    private static readonly Image _formMinimiseDisabled = Office2010ControlBoxResources.Office2010BlackMinimiseDisabled;
    private static readonly Image _formMinimisePressed = Office2010ControlBoxResources.Office2010BlackMinimisePressed;
    private static readonly Image _formRestoreNormal = Office2010ControlBoxResources.Office2010BlackRestoreNormal;
    private static readonly Image _formRestoreDisabled = Office2010ControlBoxResources.Office2010BlackRestoreDisabled;
    private static readonly Image _formRestoreActive = Office2010ControlBoxResources.Office2010BlackRestoreActive;
    private static readonly Image _formRestorePressed = Office2010ControlBoxResources.Office2010BlackRestorePressed;
    private static readonly Image _formHelpNormal = Microsoft365ControlBoxResources.Microsoft365HelpIconNormal;
    private static readonly Image _formHelpActive = Microsoft365ControlBoxResources.Microsoft365HelpIconHover;
    private static readonly Image _formHelpPressed = Microsoft365ControlBoxResources.Microsoft365HelpIconPressed;
    private static readonly Image _formHelpDisabled = Microsoft365ControlBoxResources.Microsoft365HelpIconDisabled;
    private static readonly Image _buttonSpecPendantClose = Office2010MDIImageResources.Office2010ButtonMDICloseBlack;
    private static readonly Image _buttonSpecPendantMin = Office2010MDIImageResources.Office2010ButtonMDIMinBlack;
    private static readonly Image _buttonSpecPendantRestore = Office2010MDIImageResources.Office2010ButtonMDIRestoreBlack;
    private static readonly Image _buttonSpecRibbonMinimize = RibbonArrowImageResources.RibbonUp2010Black;
    private static readonly Image _buttonSpecRibbonExpand = RibbonArrowImageResources.RibbonDown2010Black;

    #region Integrated Toolbar Images

    private static readonly Image _integratedToolbarNewNormal = Office2019ToolbarImageResources.Office2019ToolbarNewNormal;

    private static readonly Image _integratedToolbarNewDisabled = Office2019ToolbarImageResources.Office2019ToolbarNewDisabled;

    private static readonly Image _integratedToolbarOpenNormal = Office2019ToolbarImageResources.Office2019ToolbarOpenNormal;

    private static readonly Image _integratedToolbarOpenDisabled = Office2019ToolbarImageResources.Office2019ToolbarOpenDisabled;

    private static readonly Image _integratedToolbarSaveAllNormal = Office2019ToolbarImageResources.Office2019ToolbarSaveAllNormal;

    private static readonly Image _integratedToolbarSaveAllDisabled = Office2019ToolbarImageResources.Office2019ToolbarSaveAllDisabled;

    private static readonly Image _integratedToolbarSaveAsNormal = Office2019ToolbarImageResources.Office2019ToolbarSaveAsNormal;

    private static readonly Image _integratedToolbarSaveAsDisabled = Office2019ToolbarImageResources.Office2019ToolbarSaveAsDisabled;

    private static readonly Image _integratedToolbarSaveNormal = Office2019ToolbarImageResources.Office2019ToolbarSaveNormal;

    private static readonly Image _integratedToolbarSaveDisabled = Office2019ToolbarImageResources.Office2019ToolbarSaveDisabled;

    private static readonly Image _integratedToolbarCutNormal = Office2019ToolbarImageResources.Office2019ToolbarCutNormal;

    private static readonly Image _integratedToolbarCutDisabled = Office2019ToolbarImageResources.Office2019ToolbarCutDisabled;

    private static readonly Image _integratedToolbarCopyNormal = Office2019ToolbarImageResources.Office2019ToolbarCopyNormal;

    private static readonly Image _integratedToolbarCopyDisabled = Office2019ToolbarImageResources.Office2019ToolbarCopyDisabled;

    private static readonly Image _integratedToolbarPasteNormal = Office2019ToolbarImageResources.Office2019ToolbarPasteNormal;

    private static readonly Image _integratedToolbarPasteDisabled = Office2019ToolbarImageResources.Office2019ToolbarPasteDisabled;

    private static readonly Image _integratedToolbarUndoNormal = Office2019ToolbarImageResources.Office2019ToolbarUndoNormal;

    private static readonly Image _integratedToolbarUndoDisabled = Office2019ToolbarImageResources.Office2019ToolbarUndoDisabled;

    private static readonly Image _integratedToolbarRedoNormal = Office2019ToolbarImageResources.Office2019ToolbarRedoNormal;

    private static readonly Image _integratedToolbarRedoDisabled = Office2019ToolbarImageResources.Office2019ToolbarRedoDisabled;

    private static readonly Image _integratedToolbarPageSetupNormal = Office2019ToolbarImageResources.Office2019ToolbarPageSetupNormal;

    private static readonly Image _integratedToolbarPageSetupDisabled = Office2019ToolbarImageResources.Office2019ToolbarPageSetupDisabled;

    private static readonly Image _integratedToolbarPrintPreviewNormal = Office2019ToolbarImageResources.Office2019ToolbarPrintPreviewNormal;

    private static readonly Image _integratedToolbarPrintPreviewDisabled = Office2019ToolbarImageResources.Office2019ToolbarPrintPreviewDisabled;

    private static readonly Image _integratedToolbarPrintNormal = Office2019ToolbarImageResources.Office2019ToolbarPrintNormal;

    private static readonly Image _integratedToolbarPrintDisabled = Office2019ToolbarImageResources.Office2019ToolbarPrintDisabled;

    private static readonly Image _integratedToolbarQuickPrintNormal = Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal;

    private static readonly Image _integratedToolbarQuickPrintDisabled = Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled;

    #endregion

    #endregion

    #region Colour Arrays
    private static readonly Color _disabledRibbonText = Color.FromArgb(205, 205, 205);

    private static readonly Color[] _trackBarColors =
    [
        Color.FromArgb(17, 17, 17), // Tick marks
        Color.FromArgb(37, 37, 37), // Top track
        Color.FromArgb(174, 174, 174), // Bottom track
        Color.FromArgb(131, 132, 132), // Fill track
        Color.FromArgb(64, Color.White), // Outside position
        Color.FromArgb(35, 35, 35) // Border (normal) position
    ];

    private static readonly Color[] _schemeBaseColors =
    [
        Color.White, // TextLabelControl
        Color.White, // TextButtonNormal
        Color.White, // TextButtonChecked
        Color.FromArgb(137, 135, 133), // ButtonNormalBorder1
        Color.FromArgb(127, 125, 123), // ButtonNormalBorder2
        Color.FromArgb(31, 31, 31), // ButtonNormalBack1
        Color.FromArgb(15, 15, 15), // ButtonNormalBack2
        Color.FromArgb(164, 163, 163), // ButtonNormalDefaultBack1
        Color.FromArgb(114, 114, 114), // ButtonNormalDefaultBack2
        Color.FromArgb(204, 208, 214), // ButtonNormalNavigatorBack1
        Color.FromArgb(229, 232, 236), // ButtonNormalNavigatorBack2
        Color.FromArgb(31, 31, 31), // PanelClient
        Color.FromArgb(15, 15, 15), // PanelAlternative
        Color.White, // ControlBorder
        Color.FromArgb(167, 167, 167), // SeparatorHighBorder1
        Color.FromArgb(119, 119, 119), // SeparatorHighBorder2
        Color.FromArgb(31, 31, 31), // HeaderPrimaryBack1
        Color.FromArgb(15, 15, 15), // HeaderPrimaryBack2
        Color.FromArgb(164, 163, 163), // HeaderSecondaryBack1
        Color.FromArgb(114, 114, 114), // HeaderSecondaryBack2
        Color.White, // HeaderText
        Color.White, // StatusStripText
        Color.FromArgb(155, 163, 167), // ButtonBorder
        Color.FromArgb(221, 224, 227), // SeparatorLight
        Color.FromArgb(145, 153, 164), // SeparatorDark
        Color.FromArgb(228, 228, 228), // GripLight
        Color.FromArgb(77, 77, 77), // GripDark
        Color.FromArgb(15, 15, 15), // ToolStripBack
        Color.FromArgb(15, 15, 15), // StatusStripLight
        Color.FromArgb(31, 31, 31), // StatusStripDark
        Color.FromArgb(15, 15, 15), // ImageMargin
        Color.FromArgb(31, 31, 31), // ToolStripBegin
        Color.FromArgb(33, 33, 33), // ToolStripMiddle
        Color.FromArgb(54, 54, 54), // ToolStripEnd
        Color.FromArgb(178, 183, 191), // OverflowBegin
        Color.FromArgb(139, 147, 158), // OverflowMiddle
        Color.FromArgb(76, 83, 92), // OverflowEnd
        Color.FromArgb(76, 83, 92), // ToolStripBorder
        Color.FromArgb(47, 47, 47), // FormBorderActive
        Color.FromArgb(146, 146, 146), // FormBorderInactive
        Color.FromArgb(77, 77, 77), // FormBorderActiveLight
        Color.FromArgb(102, 102, 102), // FormBorderActiveDark
        Color.FromArgb(153, 153, 153), // FormBorderInactiveLight
        Color.FromArgb(171, 171, 171), // FormBorderInactiveDark
        Color.FromArgb(65, 65, 65), // FormBorderHeaderActive
        Color.FromArgb(154, 154, 154), // FormBorderHeaderInactive
        Color.FromArgb(42, 43, 43), // FormBorderHeaderActive1
        Color.FromArgb(74, 74, 74), // FormBorderHeaderActive2
        Color.FromArgb(146, 146, 146), // FormBorderHeaderInctive1
        Color.FromArgb(158, 158, 158), // FormBorderHeaderInctive2
        Color.FromArgb(255, 255, 255), // FormHeaderShortActive
        Color.FromArgb(41, 41, 41), // FormHeaderShortInactive
        Color.White, // FormHeaderLongActive
        Color.FromArgb(225, 225, 225), // FormHeaderLongInactive
        Color.FromArgb(88, 95, 104), // FormButtonBorderTrack
        Color.FromArgb(91, 105, 123), // FormButtonBack1Track
        Color.FromArgb(173, 199, 214), // FormButtonBack2Track
        Color.FromArgb(18, 18, 18), // FormButtonBorderPressed
        Color.FromArgb(0, 0, 0), // FormButtonBack1Pressed
        Color.FromArgb(65, 83, 102), // FormButtonBack2Pressed
        Color.White, // TextButtonFormNormal
        Color.White, // TextButtonFormTracking
        Color.White, // TextButtonFormPressed
        Color.White, // LinkNotVisitedOverrideControl
        Color.Purple, // LinkVisitedOverrideControl
        Color.Red, // LinkPressedOverrideControl
        Color.FromArgb(180, 210, 255), // LinkNotVisitedOverridePanel
        Color.Violet, // LinkVisitedOverridePanel
        Color.FromArgb(255, 90, 90), // LinkPressedOverridePanel
        Color.White, // TextLabelPanel
        Color.White, // RibbonTabTextNormal
        Color.FromArgb(15, 15, 15), // RibbonTabTextChecked
        Color.FromArgb(190, 190, 190), // RibbonTabSelected1
        Color.FromArgb(199, 250, 254), // RibbonTabSelected2
        Color.FromArgb(238, 239, 241), // RibbonTabSelected3
        Color.FromArgb(241, 241, 241), // RibbonTabSelected4
        Color.FromArgb(213, 217, 223), // RibbonTabSelected5
        Color.FromArgb(159, 156, 150), // RibbonTabTracking1
        Color.FromArgb(235, 194, 39), // RibbonTabTracking2
        Color.FromArgb(255, 255, 189), // RibbonTabHighlight1
        Color.FromArgb(249, 237, 198), // RibbonTabHighlight2
        Color.FromArgb(218, 185, 127), // RibbonTabHighlight3
        Color.FromArgb(254, 209, 94), // RibbonTabHighlight4
        Color.FromArgb(123, 111, 68), // RibbonTabHighlight5
        Color.FromArgb(54, 54, 54), // RibbonTabSeparatorColor
        Color.FromArgb(190, 190, 190), // RibbonGroupsArea1
        Color.FromArgb(210, 210, 210), // RibbonGroupsArea2
        Color.FromArgb(180, 187, 197), // RibbonGroupsArea3
        Color.FromArgb(235, 235, 235), // RibbonGroupsArea4
        Color.FromArgb(215, 219, 224), // RibbonGroupsArea5
        Color.FromArgb(174, 176, 180), // RibbonGroupBorder1
        Color.FromArgb(132, 132, 132), // RibbonGroupBorder2
        Color.FromArgb(182, 184, 184), // RibbonGroupTitle1
        Color.FromArgb(159, 160, 160), // RibbonGroupTitle2
        Color.FromArgb(183, 183, 183), // RibbonGroupBorderContext1
        Color.FromArgb(131, 131, 131), // RibbonGroupBorderContext2
        Color.FromArgb(190, 190, 190), // RibbonGroupTitleContext1
        Color.FromArgb(161, 161, 161), // RibbonGroupTitleContext2
        Color.FromArgb(101, 104, 112), // RibbonGroupDialogDark
        Color.FromArgb(235, 235, 235), // RibbonGroupDialogLight
        Color.FromArgb(170, 171, 171), // RibbonGroupTitleTracking1
        Color.FromArgb(109, 110, 110), // RibbonGroupTitleTracking2
        Color.FromArgb(79, 79, 79), // RibbonMinimizeBarDark
        Color.FromArgb(98, 98, 98), // RibbonMinimizeBarLight
        Color.FromArgb(182, 183, 183), // RibbonGroupCollapsedBorder1
        Color.FromArgb(112, 112, 112), // RibbonGroupCollapsedBorder2
        Color.FromArgb(64, Color.White), // RibbonGroupCollapsedBorder3
        Color.FromArgb(217, 217, 217), // RibbonGroupCollapsedBorder4
        Color.FromArgb(244, 244, 245), // RibbonGroupCollapsedBack1
        Color.FromArgb(200, 205, 212), // RibbonGroupCollapsedBack2
        Color.FromArgb(185, 192, 201), // RibbonGroupCollapsedBack3
        Color.FromArgb(235, 235, 235), // RibbonGroupCollapsedBack4
        Color.FromArgb(188, 193, 214), // RibbonGroupCollapsedBorderT1
        Color.FromArgb(116, 141, 187), // RibbonGroupCollapsedBorderT2
        Color.FromArgb(192, Color.White), // RibbonGroupCollapsedBorderT3
        Color.White, // RibbonGroupCollapsedBorderT4
        Color.FromArgb(246, 246, 246), // RibbonGroupCollapsedBackT1
        Color.FromArgb(214, 220, 228), // RibbonGroupCollapsedBackT2
        Color.FromArgb(203, 210, 221), // RibbonGroupCollapsedBackT3
        Color.FromArgb(235, 235, 235), // RibbonGroupCollapsedBackT4
        Color.FromArgb(160, 160, 160), // RibbonGroupFrameBorder1
        Color.FromArgb(194, 194, 194), // RibbonGroupFrameBorder2
        Color.FromArgb(239, 240, 241), // RibbonGroupFrameInside1
        Color.FromArgb(222, 225, 229), // RibbonGroupFrameInside2
        Color.FromArgb(214, 218, 223), // RibbonGroupFrameInside3
        Color.FromArgb(222, 225, 230), // RibbonGroupFrameInside4
        Color.FromArgb(255, 255, 255), // RibbonGroupCollapsedText (Old value 70, 70, 70)
        Color.FromArgb(158, 163, 172), // AlternatePressedBack1
        Color.FromArgb(212, 215, 216), // AlternatePressedBack2
        Color.FromArgb(124, 125, 125), // AlternatePressedBorder1
        Color.FromArgb(186, 186, 186), // AlternatePressedBorder2
        Color.FromArgb(43, 55, 67), // FormButtonBack1Checked
        Color.FromArgb(106, 122, 140), // FormButtonBack2Checked
        Color.FromArgb(18, 18, 18), // FormButtonBorderCheck
        Color.FromArgb(33, 45, 57), // FormButtonBack1CheckTrack
        Color.FromArgb(136, 152, 170), // FormButtonBack2CheckTrack
        Color.FromArgb(55, 55, 55), // RibbonQATMini1
        Color.FromArgb(100, 100, 100), // RibbonQATMini2
        Color.FromArgb(73, 73, 73), // RibbonQATMini3
        Color.FromArgb(12, Color.White), // RibbonQATMini4
        Color.FromArgb(14, Color.White), // RibbonQATMini5
        Color.FromArgb(100, 100, 100), // RibbonQATMini1I
        Color.FromArgb(170, 170, 170), // RibbonQATMini2I
        Color.FromArgb(140, 140, 140), // RibbonQATMini3I
        Color.FromArgb(12, Color.White), // RibbonQATMini4I
        Color.FromArgb(14, Color.White), // RibbonQATMini5I
        Color.FromArgb(141, 144, 147), // RibbonQATFullbar1
        Color.FromArgb(133, 135, 137), // RibbonQATFullbar2
        Color.FromArgb(93, 96, 100), // RibbonQATFullbar3
        Color.FromArgb(103, 103, 103), // RibbonQATButtonDark
        Color.FromArgb(225, 225, 225), // RibbonQATButtonLight
        Color.FromArgb(118, 128, 142), // RibbonQATOverflow1
        Color.FromArgb(55, 60, 67), // RibbonQATOverflow2
        Color.FromArgb(163, 168,
            170), // RibbonGroupSeparatorDark
        Color.FromArgb(230, 233,
            235), // RibbonGroupSeparatorLight
        Color.FromArgb(210, 217,
            219), // ButtonClusterButtonBack1
        Color.FromArgb(214, 222,
            223), // ButtonClusterButtonBack2
        Color.FromArgb(179, 188,
            191), // ButtonClusterButtonBorder1
        Color.FromArgb(145, 156,
            159), // ButtonClusterButtonBorder2
        Color.FromArgb(235, 235, 235), // NavigatorMiniBackColor
        Color.FromArgb(31, 31, 31), // GridListNormal1
        Color.FromArgb(15, 15, 15), // GridListNormal2
        Color.FromArgb(15, 15, 15), // GridListPressed1
        Color.FromArgb(15, 15, 15), // GridListPressed2
        Color.FromArgb(33, 33, 33), // GridListSelected
        Color.FromArgb(31, 31, 31), // GridSheetColNormal1
        Color.FromArgb(15, 15, 15), // GridSheetColNormal2
        Color.FromArgb(224, 224, 224), // GridSheetColPressed1
        Color.FromArgb(195, 195, 195), // GridSheetColPressed2
        Color.FromArgb(91, 91, 91), // GridSheetColSelected1
        Color.FromArgb(33, 33, 33), // GridSheetColSelected2
        Color.FromArgb(237, 237, 237), // GridSheetRowNormal
        Color.FromArgb(196, 196, 196), // GridSheetRowPressed
        Color.FromArgb(15, 15, 15), // GridSheetRowSelected
        Color.FromArgb(188, 195, 209), // GridDataCellBorder
        Color.FromArgb(91, 91, 91), // GridDataCellSelected
        Color.White, // InputControlTextNormal
        Color.FromArgb(172, 168, 153), // InputControlTextDisabled
        Color.FromArgb(137, 137, 137), // InputControlBorderNormal
        Color.FromArgb(204, 204, 204), // InputControlBorderDisabled
        Color.FromArgb(31, 31, 31), // InputControlBackNormal
        SystemColors.Control, // InputControlBackDisabled
        Color.FromArgb(78, 78, 80), // InputControlBackInactive
        Color.White, // InputDropDownNormal1
        Color.FromArgb(100, 100, 100), // InputDropDownNormal2
        Color.FromArgb(82, 82, 82), // InputDropDownDisabled1
        Color.FromArgb(95, 95, 95), // InputDropDownDisabled2
        Color.FromArgb(15, 15, 15), // ContextMenuHeading
        Color.White, // ContextMenuHeadingText
        Color.FromArgb(54, 54, 54), // ContextMenuImageColumn
        Color.FromArgb(31, 31, 31), // AppButtonBack1
        Color.FromArgb(15, 15, 15), // AppButtonBack2
        Color.FromArgb(67, 66, 65), // AppButtonBorder
        Color.FromArgb(78, 78, 79), // AppButtonOuter1
        Color.FromArgb(47, 47, 47), // AppButtonOuter2
        Color.FromArgb(64, 64, 64), // AppButtonOuter3
        Color.FromArgb(107, 108, 113), // AppButtonInner1
        Color.FromArgb(67, 66, 65), // AppButtonInner2
        Color.FromArgb(38, 38, 38), // AppButtonMenuDocs
        Color.White, // AppButtonMenuDocsText
        Color.FromArgb(240, 241, 242), // SeparatorHighInternalBorder1
        Color.FromArgb(195, 200, 206), // SeparatorHighInternalBorder2
        Color.FromArgb(172, 172, 172), // RibbonGalleryBorder
        Color.FromArgb(218, 226, 226), // RibbonGalleryBackNormal
        Color.FromArgb(247, 247, 247), // RibbonGalleryBackTracking
        Color.FromArgb(195, 200, 209), // RibbonGalleryBack1
        Color.FromArgb(217, 220, 224), // RibbonGalleryBack2
        GlobalStaticValues.EMPTY_COLOR, // RibbonTabTracking3
        GlobalStaticValues.EMPTY_COLOR, // RibbonTabTracking4
        GlobalStaticValues.EMPTY_COLOR, // RibbonGroupBorder3
        GlobalStaticValues.EMPTY_COLOR, // RibbonGroupBorder4
        GlobalStaticValues.EMPTY_COLOR, // RibbonGroupBorder5
        Color.FromArgb(255, 255, 255), // RibbonGroupTitleText
        Color.FromArgb(225, 225, 225), // RibbonDropArrowLight
        Color.FromArgb(103, 103, 103), // RibbonDropArrowDark
        Color.FromArgb(137, 137, 137), // HeaderDockInactiveBack1
        Color.FromArgb(125, 125, 125), // HeaderDockInactiveBack2
        Color.FromArgb(46, 46, 46), // ButtonNavigatorBorder
        Color.White, // ButtonNavigatorText
        Color.FromArgb(76, 76, 76), // ButtonNavigatorTrack1
        Color.FromArgb(147, 147, 143), // ButtonNavigatorTrack2
        Color.FromArgb(66, 66, 66), // ButtonNavigatorPressed1
        Color.FromArgb(148, 148, 143), // ButtonNavigatorPressed2
        Color.FromArgb(91, 91, 91), // ButtonNavigatorChecked1
        Color.FromArgb(73, 73, 73), // ButtonNavigatorChecked2
        Color.FromArgb(201, 201, 201) // ToolTipBottom
    ];

    #endregion

    #endregion

    #region Constructors
    static PaletteMicrosoft365BlackDarkModeAlternate()
    {
        _checkBoxList = new ImageList
        {
            ImageSize = new Size(13, 13),
            ColorDepth = ColorDepth.Depth24Bit
        };

        _checkBoxList.Images.AddStrip(CheckBoxStripResources.CheckBoxStrip2010Black);

        _galleryButtonList = new ImageList
        {
            ImageSize = new Size(13, 7),
            ColorDepth = ColorDepth.Depth24Bit,
            TransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR
        };

        _galleryButtonList.Images.AddStrip(GalleryImageResources.Gallery2010);

        _radioButtonArray =
        [
            Office2010RadioButtonImageResources.RadioButton2010BlueD,
            Office2010RadioButtonImageResources.RadioButton2010SilverN,
            Office2010RadioButtonImageResources.RadioButton2010BlueT,
            Office2010RadioButtonImageResources.RadioButton2010BlueP,
            Office2010RadioButtonImageResources.RadioButton2010BlueDC,
            Office2010RadioButtonImageResources.RadioButton2010SilverNC,
            Office2010RadioButtonImageResources.RadioButton2010SilverTC,
            Office2010RadioButtonImageResources.RadioButton2010SilverPC
        ];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaletteMicrosoft365BlackDarkModeAlternate"/> class.
    /// </summary>
    public PaletteMicrosoft365BlackDarkModeAlternate() : base(_schemeBaseColors, _checkBoxList, _galleryButtonList, _radioButtonArray, _trackBarColors)
    {

    }
    #endregion

    #region Images
    /// <summary>
    /// Gets an image indicating a sub-menu on a context menu item.
    /// </summary>
    /// <returns>
    /// Appropriate image for drawing; otherwise null.
    /// </returns>
    public override Image? GetContextMenuSubMenuImage() => _contextMenuSubMenu;

    #endregion

    #region ButtonSpec
    /// <summary>
    /// Gets the image to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <param name="state">State for which image is required.</param>
    /// <returns>
    /// Image value.
    /// </returns>
    public override Image? GetButtonSpecImage(PaletteButtonSpecStyle style, PaletteState state) => style switch
    {
        PaletteButtonSpecStyle.PendantClose => _buttonSpecPendantClose,
        PaletteButtonSpecStyle.PendantMin => _buttonSpecPendantMin,
        PaletteButtonSpecStyle.PendantRestore => _buttonSpecPendantRestore,
        PaletteButtonSpecStyle.FormClose => state switch
        {
            PaletteState.Tracking => _formCloseActive,
            PaletteState.Normal => _formCloseNormal,
            PaletteState.Pressed => _formClosePressed,
            _ => _formCloseDisabled
        },
        PaletteButtonSpecStyle.FormMin => state switch
        {
            PaletteState.Normal => _formMinimiseNormal,
            PaletteState.Tracking => _formMinimiseActive,
            PaletteState.Pressed => _formMinimisePressed,
            _ => _formMinimiseDisabled
        },
        PaletteButtonSpecStyle.FormMax => state switch
        {
            PaletteState.Normal => _formMaximiseNormal,
            PaletteState.Tracking => _formMaximiseActive,
            PaletteState.Pressed => _formMaximisePressed,
            _ => _formMaximiseDisabled
        },
        PaletteButtonSpecStyle.FormRestore => state switch
        {
            PaletteState.Normal => _formRestoreNormal,
            PaletteState.Tracking => _formRestoreActive,
            PaletteState.Pressed => _formRestorePressed,
            _ => _formRestoreDisabled
        },
        PaletteButtonSpecStyle.FormHelp => state switch
        {
            PaletteState.Tracking => _formHelpActive,
            PaletteState.Pressed => _formHelpPressed,
            PaletteState.Normal => _formHelpNormal,
            _ => _formHelpDisabled
        },
        PaletteButtonSpecStyle.New => state switch
        {
            PaletteState.Normal => _integratedToolbarNewNormal,
            PaletteState.Disabled => _integratedToolbarNewDisabled,
            _ => _integratedToolbarNewDisabled
        },
        PaletteButtonSpecStyle.Open => state switch
        {
            PaletteState.Normal => _integratedToolbarOpenNormal,
            PaletteState.Disabled => _integratedToolbarOpenDisabled,
            _ => _integratedToolbarOpenDisabled
        },
        PaletteButtonSpecStyle.SaveAll => state switch
        {
            PaletteState.Normal => _integratedToolbarSaveAllNormal,
            PaletteState.Disabled => _integratedToolbarSaveAllDisabled,
            _ => _integratedToolbarSaveAllDisabled
        },
        PaletteButtonSpecStyle.SaveAs => state switch
        {
            PaletteState.Normal => _integratedToolbarSaveAsNormal,
            PaletteState.Disabled => _integratedToolbarSaveAsDisabled,
            _ => _integratedToolbarSaveAsDisabled
        },
        PaletteButtonSpecStyle.Save => state switch
        {
            PaletteState.Normal => _integratedToolbarSaveNormal,
            PaletteState.Disabled => _integratedToolbarSaveDisabled,
            _ => _integratedToolbarSaveDisabled
        },
        PaletteButtonSpecStyle.Cut => state switch
        {
            PaletteState.Normal => _integratedToolbarCutNormal,
            PaletteState.Disabled => _integratedToolbarCutDisabled,
            _ => _integratedToolbarCutDisabled
        },
        PaletteButtonSpecStyle.Copy => state switch
        {
            PaletteState.Normal => _integratedToolbarCopyNormal,
            PaletteState.Disabled => _integratedToolbarCopyDisabled,
            _ => _integratedToolbarCopyDisabled
        },
        PaletteButtonSpecStyle.Paste => state switch
        {
            PaletteState.Normal => _integratedToolbarPasteNormal,
            PaletteState.Disabled => _integratedToolbarPasteDisabled,
            _ => _integratedToolbarPasteDisabled
        },
        PaletteButtonSpecStyle.Undo => state switch
        {
            PaletteState.Normal => _integratedToolbarUndoNormal,
            PaletteState.Disabled => _integratedToolbarUndoDisabled,
            _ => _integratedToolbarUndoDisabled
        },
        PaletteButtonSpecStyle.Redo => state switch
        {
            PaletteState.Normal => _integratedToolbarRedoNormal,
            PaletteState.Disabled => _integratedToolbarRedoDisabled,
            _ => _integratedToolbarRedoDisabled
        },
        PaletteButtonSpecStyle.PageSetup => state switch
        {
            PaletteState.Normal => _integratedToolbarPageSetupNormal,
            PaletteState.Disabled => _integratedToolbarPageSetupDisabled,
            _ => _integratedToolbarPageSetupDisabled
        },
        PaletteButtonSpecStyle.PrintPreview => state switch
        {
            PaletteState.Normal => _integratedToolbarPrintPreviewNormal,
            PaletteState.Disabled => _integratedToolbarPrintPreviewDisabled,
            _ => _integratedToolbarPrintPreviewDisabled
        },
        PaletteButtonSpecStyle.Print => state switch
        {
            PaletteState.Normal => _integratedToolbarPrintNormal,
            PaletteState.Disabled => _integratedToolbarPrintDisabled,
            _ => _integratedToolbarPrintDisabled
        },
        PaletteButtonSpecStyle.QuickPrint => state switch
        {
            PaletteState.Normal => _integratedToolbarQuickPrintNormal,
            PaletteState.Disabled => _integratedToolbarQuickPrintDisabled,
            _ => _integratedToolbarQuickPrintDisabled
        },
        PaletteButtonSpecStyle.RibbonMinimize => _buttonSpecRibbonMinimize,
        PaletteButtonSpecStyle.RibbonExpand => _buttonSpecRibbonExpand,
        _ => base.GetButtonSpecImage(style, state)
    };
    #endregion

    #region Tab Row Background

    /// <inheritdoc />
    public override Color GetRibbonTabRowGradientColor1(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) =>
        GlobalStaticValues.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) =>
        GlobalStaticValues.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) => _tabRowBackgroundColor;

    /// <inheritdoc />
    public override float GetRibbonTabRowGradientRaftingAngle(PaletteState state) => -1;

    #endregion

    #region AppButton Colors

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabBottomColor(PaletteState state) => _ribbonAppButtonDarkColor;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTopColor(PaletteState state) => _ribbonAppButtonLightColor;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTextColor(PaletteState state) => _ribbonAppButtonTextColor;

    #endregion
}
#endregion

#region Class: KryptonColorTable365BlackDarkModeAlternate
/// <summary>
/// Provide KryptonColorTable365BlackDarkModeAlternate values using an array of Color values as the source.
/// </summary>
public class KryptonColorTable365BlackDarkModeAlternate : KryptonColorTable
{
    #region Static Fields
    private static readonly Color _contextMenuBackground = Color.FromArgb(31, 31, 31);
    private static readonly Color _menuBorder = Color.FromArgb(167, 171, 176);
    private static readonly Color _checkBackground = Color.FromArgb(252, 241, 194);
    private static readonly Color _buttonSelectedBegin = Color.FromArgb(91, 91, 91);
    private static readonly Color _buttonSelectedEnd = Color.FromArgb(89, 89, 89);
    private static readonly Color _buttonPressedBegin = Color.FromArgb(91, 91, 91);
    private static readonly Color _buttonPressedEnd = Color.FromArgb(91, 91, 91);
    private static readonly Color _buttonCheckedBegin = Color.FromArgb(91, 91, 91);
    private static readonly Color _buttonCheckedEnd = Color.FromArgb(91, 91, 91);
    private static readonly Color _menuItemSelectedBegin = Color.FromArgb(91, 91, 91);
    private static readonly Color _menuItemSelectedEnd = Color.FromArgb(89, 89, 89);
    private static Font _menuToolFont;
    private static Font _statusFont;
    #endregion

    #region Instance Fields
    private readonly Color[] _colors;
    private readonly InheritBool _roundedEdges;
    #endregion

    #region Identity
    [SecuritySafeCritical]
    static KryptonColorTable365BlackDarkModeAlternate()
    {
        // Get the font settings from the system
        DefineFonts();

        // We need to notice when system color settings change
        SystemEvents.UserPreferenceChanged += OnUserPreferenceChanged;
    }

    /// <summary>
    /// Initialize a new instance of the KryptonColorTable2010 class.
    /// </summary>
    /// <param name="colors">Source of </param>
    /// <param name="roundedEdges">Should have rounded edges.</param>
    /// <param name="palette">Associated palette instance.</param>
    public KryptonColorTable365BlackDarkModeAlternate([DisallowNull] Color[] colors,
        InheritBool roundedEdges, PaletteBase palette)
        : base(palette)
    {
        Debug.Assert(colors != null);
        if (colors != null)
        {
            _colors = colors;
        }
        _roundedEdges = roundedEdges;
    }
    #endregion

    #region Colors
    /// <summary>
    /// Gets the raw set of colors.
    /// </summary>
    public Color[] Colors => _colors;

    #endregion

    #region RoundedEdges
    /// <summary>
    /// Gets a value indicating if rounded edges are required.
    /// </summary>
    public override InheritBool UseRoundedEdges => _roundedEdges;

    #endregion

    #region ButtonPressed
    #region ButtonPressedBorder
    /// <summary>
    /// Gets the border color for a button being pressed.
    /// </summary>
    public override Color ButtonPressedBorder => _colors[(int)SchemeBaseColors.ButtonBorder];

    #endregion

    #region ButtonPressedGradientBegin
    /// <summary>
    /// Gets the background starting color for a button being pressed.
    /// </summary>
    public override Color ButtonPressedGradientBegin => _buttonPressedBegin;

    #endregion

    #region ButtonPressedGradientMiddle
    /// <summary>
    /// Gets the background middle color for a button being pressed.
    /// </summary>
    public override Color ButtonPressedGradientMiddle => _buttonPressedBegin;

    #endregion

    #region ButtonPressedGradientEnd
    /// <summary>
    /// Gets the background ending color for a button being pressed.
    /// </summary>
    public override Color ButtonPressedGradientEnd => _buttonPressedEnd;

    #endregion

    #region ButtonPressedHighlight
    /// <summary>
    /// Gets the highlight background for a pressed button.
    /// </summary>
    public override Color ButtonPressedHighlight => _buttonPressedBegin;

    #endregion

    #region ButtonPressedHighlightBorder
    /// <summary>
    /// Gets the highlight border for a pressed button.
    /// </summary>
    public override Color ButtonPressedHighlightBorder => _colors[(int)SchemeBaseColors.ButtonBorder];

    #endregion
    #endregion

    #region ButtonSelected
    #region ButtonSelectedBorder
    /// <summary>
    /// Gets the border color for a button being selected.
    /// </summary>
    public override Color ButtonSelectedBorder => _colors[(int)SchemeBaseColors.ButtonBorder];

    #endregion

    #region ButtonSelectedGradientBegin
    /// <summary>
    /// Gets the background starting color for a button being selected.
    /// </summary>
    public override Color ButtonSelectedGradientBegin => _buttonSelectedBegin;

    #endregion

    #region ButtonSelectedGradientMiddle
    /// <summary>
    /// Gets the background middle color for a button being selected.
    /// </summary>
    public override Color ButtonSelectedGradientMiddle => _buttonSelectedBegin;

    #endregion

    #region ButtonSelectedGradientEnd
    /// <summary>
    /// Gets the background ending color for a button being selected.
    /// </summary>
    public override Color ButtonSelectedGradientEnd => _buttonSelectedEnd;

    #endregion

    #region ButtonSelectedHighlight
    /// <summary>
    /// Gets the highlight background for a selected button.
    /// </summary>
    public override Color ButtonSelectedHighlight => _buttonSelectedBegin;

    #endregion

    #region ButtonSelectedHighlightBorder
    /// <summary>
    /// Gets the highlight border for a selected button.
    /// </summary>
    public override Color ButtonSelectedHighlightBorder => _colors[(int)SchemeBaseColors.ButtonBorder];

    #endregion
    #endregion

    #region ButtonChecked
    #region ButtonCheckedGradientBegin
    /// <summary>
    /// Gets the background starting color for a checked button.
    /// </summary>
    public override Color ButtonCheckedGradientBegin => _buttonCheckedBegin;

    #endregion

    #region ButtonCheckedGradientMiddle
    /// <summary>
    /// Gets the background middle color for a checked button.
    /// </summary>
    public override Color ButtonCheckedGradientMiddle => _buttonCheckedBegin;

    #endregion

    #region ButtonCheckedGradientEnd
    /// <summary>
    /// Gets the background ending color for a checked button.
    /// </summary>
    public override Color ButtonCheckedGradientEnd => _buttonCheckedEnd;

    #endregion

    #region ButtonCheckedHighlight
    /// <summary>
    /// Gets the highlight background for a checked button.
    /// </summary>
    public override Color ButtonCheckedHighlight => _buttonCheckedBegin;

    #endregion

    #region ButtonCheckedHighlightBorder
    /// <summary>
    /// Gets the highlight border for a checked button.
    /// </summary>
    public override Color ButtonCheckedHighlightBorder => _colors[(int)SchemeBaseColors.ButtonBorder];

    #endregion
    #endregion

    #region Check
    #region CheckBackground
    /// <summary>
    /// Get background of the check mark area.
    /// </summary>
    public override Color CheckBackground => _checkBackground;

    #endregion

    #region CheckBackground
    /// <summary>
    /// Get background of a pressed check mark area.
    /// </summary>
    public override Color CheckPressedBackground => _checkBackground;

    #endregion

    #region CheckBackground
    /// <summary>
    /// Get background of a selected check mark area.
    /// </summary>
    public override Color CheckSelectedBackground => _checkBackground;

    #endregion
    #endregion

    #region Grip
    #region GripLight
    /// <summary>
    /// Gets the light color used to draw grips.
    /// </summary>
    public override Color GripLight => _colors[(int)SchemeBaseColors.GripLight];

    #endregion

    #region GripDark
    /// <summary>
    /// Gets the dark color used to draw grips.
    /// </summary>
    public override Color GripDark => _colors[(int)SchemeBaseColors.GripDark];

    #endregion
    #endregion

    #region ImageMargin
    #region ImageMarginGradientBegin
    /// <summary>
    /// Gets the starting color for the context menu margin.
    /// </summary>
    public override Color ImageMarginGradientBegin => _colors[(int)SchemeBaseColors.ImageMargin];

    #endregion

    #region ImageMarginGradientMiddle
    /// <summary>
    /// Gets the middle color for the context menu margin.
    /// </summary>
    public override Color ImageMarginGradientMiddle => _colors[(int)SchemeBaseColors.ImageMargin];

    #endregion

    #region ImageMarginGradientEnd
    /// <summary>
    /// Gets the ending color for the context menu margin.
    /// </summary>
    public override Color ImageMarginGradientEnd => _colors[(int)SchemeBaseColors.ImageMargin];

    #endregion

    #region ImageMarginRevealedGradientBegin
    /// <summary>
    /// Gets the starting color for the context menu margin revealed.
    /// </summary>
    public override Color ImageMarginRevealedGradientBegin => _colors[(int)SchemeBaseColors.ImageMargin];

    #endregion

    #region ImageMarginRevealedGradientMiddle
    /// <summary>
    /// Gets the middle color for the context menu margin revealed.
    /// </summary>
    public override Color ImageMarginRevealedGradientMiddle => _colors[(int)SchemeBaseColors.ImageMargin];

    #endregion

    #region ImageMarginRevealedGradientEnd
    /// <summary>
    /// Gets the ending color for the context menu margin revealed.
    /// </summary>
    public override Color ImageMarginRevealedGradientEnd => _colors[(int)SchemeBaseColors.ImageMargin];

    #endregion
    #endregion

    #region MenuBorder
    /// <summary>
    /// Gets the color of the border around menus.
    /// </summary>
    public override Color MenuBorder => _menuBorder;

    #endregion

    #region MenuItem
    #region MenuItemBorder
    /// <summary>
    /// Gets the border color for around the menu item.
    /// </summary>
    public override Color MenuItemBorder => _menuBorder;

    #endregion

    #region MenuItemSelected
    /// <summary>
    /// Gets the color of a selected menu item.
    /// </summary>
    public override Color MenuItemSelected => _colors[(int)SchemeBaseColors.ButtonBorder];

    #endregion

    #region MenuItemPressedGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used when a top-level ToolStripMenuItem is pressed down.
    /// </summary>
    public override Color MenuItemPressedGradientBegin => _colors[(int)SchemeBaseColors.ToolStripBegin];

    #endregion

    #region MenuItemPressedGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used when a top-level ToolStripMenuItem is pressed down.
    /// </summary>
    public override Color MenuItemPressedGradientEnd => _colors[(int)SchemeBaseColors.ToolStripEnd];

    #endregion

    #region MenuItemPressedGradientMiddle
    /// <summary>
    /// Gets the middle color of the gradient used when a top-level ToolStripMenuItem is pressed down.
    /// </summary>
    public override Color MenuItemPressedGradientMiddle => _colors[(int)SchemeBaseColors.ToolStripMiddle];

    #endregion

    #region MenuItemSelectedGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used when the ToolStripMenuItem is selected.
    /// </summary>
    public override Color MenuItemSelectedGradientBegin => _menuItemSelectedBegin;

    #endregion

    #region MenuItemSelectedGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used when the ToolStripMenuItem is selected.
    /// </summary>
    public override Color MenuItemSelectedGradientEnd => _menuItemSelectedEnd;

    #endregion
    #endregion

    #region MenuStrip
    #region MenuStripGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used in the MenuStrip.
    /// </summary>
    public override Color MenuStripGradientBegin => _colors[(int)SchemeBaseColors.ToolStripBack];

    #endregion

    #region MenuStripGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used in the MenuStrip.
    /// </summary>
    public override Color MenuStripGradientEnd => _colors[(int)SchemeBaseColors.ToolStripBack];

    #endregion

    #endregion

    #region OverflowButton
    #region OverflowButtonGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used in the ToolStripOverflowButton.
    /// </summary>
    public override Color OverflowButtonGradientBegin => _colors[(int)SchemeBaseColors.OverflowBegin];

    #endregion

    #region OverflowButtonGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used in the ToolStripOverflowButton.
    /// </summary>
    public override Color OverflowButtonGradientEnd => _colors[(int)SchemeBaseColors.OverflowEnd];

    #endregion

    #region OverflowButtonGradientMiddle
    /// <summary>
    /// Gets the middle color of the gradient used in the ToolStripOverflowButton.
    /// </summary>
    public override Color OverflowButtonGradientMiddle => _colors[(int)SchemeBaseColors.OverflowMiddle];

    #endregion
    #endregion

    #region RaftingContainer
    #region RaftingContainerGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used in the ToolStripContainer.
    /// </summary>
    public override Color RaftingContainerGradientBegin => _colors[(int)SchemeBaseColors.ToolStripBack];

    #endregion

    #region RaftingContainerGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used in the ToolStripContainer.
    /// </summary>
    public override Color RaftingContainerGradientEnd => _colors[(int)SchemeBaseColors.ToolStripBack];

    #endregion

    #endregion

    #region Separator
    #region SeparatorLight
    /// <summary>
    /// Gets the light separator color.
    /// </summary>
    public override Color SeparatorLight => _colors[(int)SchemeBaseColors.SeparatorLight];

    #endregion

    #region SeparatorDark
    /// <summary>
    /// Gets the dark separator color.
    /// </summary>
    public override Color SeparatorDark => _colors[(int)SchemeBaseColors.SeparatorDark];

    #endregion
    #endregion

    #region StatusStrip
    #region StatusStripGradientBegin
    /// <summary>
    /// Gets the starting color for the status strip background.
    /// </summary>
    public override Color StatusStripGradientBegin => _colors[(int)SchemeBaseColors.StatusStripLight];

    #endregion

    #region StatusStripGradientEnd
    /// <summary>
    /// Gets the ending color for the status strip background.
    /// </summary>
    public override Color StatusStripGradientEnd => _colors[(int)SchemeBaseColors.StatusStripDark];

    #endregion
    #endregion

    #region Text
    #region MenuItemText
    /// <summary>
    /// Gets the text color used on the menu items.
    /// </summary>
    public override Color MenuItemText => _colors[(int)SchemeBaseColors.TextButtonNormal];

    #endregion

    #region MenuStripText
    /// <summary>
    /// Gets the text color used on the menu strip.
    /// </summary>
    public override Color MenuStripText => _colors[(int)SchemeBaseColors.StatusStripText];

    #endregion

    #region ToolStripText
    /// <summary>
    /// Gets the text color used on the tool strip.
    /// </summary>
    public override Color ToolStripText => _colors[(int)SchemeBaseColors.StatusStripText];

    #endregion

    #region StatusStripText
    /// <summary>
    /// Gets the text color used on the status strip.
    /// </summary>
    public override Color StatusStripText => _colors[(int)SchemeBaseColors.StatusStripText];

    #endregion

    #region MenuStripFont
    /// <summary>
    /// Gets the font used on the menu strip.
    /// </summary>
    public override Font MenuStripFont => _menuToolFont;

    #endregion

    #region ToolStripFont
    /// <summary>
    /// Gets the font used on the tool strip.
    /// </summary>
    public override Font ToolStripFont => _menuToolFont;

    #endregion

    #region StatusStripFont
    /// <summary>
    /// Gets the font used on the status strip.
    /// </summary>
    public override Font StatusStripFont => _statusFont;

    #endregion
    #endregion

    #region ToolStrip
    #region ToolStripBorder
    /// <summary>
    /// Gets the border color to use on the bottom edge of the ToolStrip.
    /// </summary>
    public override Color ToolStripBorder => _colors[(int)SchemeBaseColors.ToolStripBorder];

    #endregion

    #region ToolStripContentPanelGradientBegin
    /// <summary>
    /// Gets the starting color for the content panel background.
    /// </summary>
    public override Color ToolStripContentPanelGradientBegin => _colors[(int)SchemeBaseColors.ToolStripBack];

    #endregion

    #region ToolStripContentPanelGradientEnd
    /// <summary>
    /// Gets the ending color for the content panel background.
    /// </summary>
    public override Color ToolStripContentPanelGradientEnd => _colors[(int)SchemeBaseColors.ToolStripBack];

    #endregion

    #region ToolStripDropDownBackground
    /// <summary>
    /// Gets the background color for drop-down menus.
    /// </summary>
    public override Color ToolStripDropDownBackground => _contextMenuBackground;

    #endregion

    #region ToolStripGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used in the ToolStrip background.
    /// </summary>
    public override Color ToolStripGradientBegin => _colors[(int)SchemeBaseColors.ToolStripBegin];

    #endregion

    #region ToolStripGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used in the ToolStrip background.
    /// </summary>
    public override Color ToolStripGradientEnd => _colors[(int)SchemeBaseColors.ToolStripEnd];

    #endregion

    #region ToolStripGradientMiddle
    /// <summary>
    /// Gets the middle color of the gradient used in the ToolStrip background.
    /// </summary>
    public override Color ToolStripGradientMiddle => _colors[(int)SchemeBaseColors.ToolStripMiddle];

    #endregion

    #region ToolStripPanelGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used in the ToolStripPanel.
    /// </summary>
    public override Color ToolStripPanelGradientBegin => _colors[(int)SchemeBaseColors.ToolStripBack];

    #endregion

    #region ToolStripPanelGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used in the ToolStripPanel.
    /// </summary>
    public override Color ToolStripPanelGradientEnd => _colors[(int)SchemeBaseColors.ToolStripBack];

    #endregion
    #endregion

    #region Implementation
    private static void DefineFonts()
    {
        // Create new font using system information
        // TODO: Should be using base font
        _menuToolFont = new Font(@"Segoe UI", SystemFonts.MenuFont!.SizeInPoints!, FontStyle.Regular);
        _statusFont = new Font(@"Segoe UI", SystemFonts.StatusFont!.SizeInPoints!, FontStyle.Regular);
    }

    private static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e) =>
        // Update fonts to reflect any change in system settings
        DefineFonts();

    #endregion
}
#endregion
