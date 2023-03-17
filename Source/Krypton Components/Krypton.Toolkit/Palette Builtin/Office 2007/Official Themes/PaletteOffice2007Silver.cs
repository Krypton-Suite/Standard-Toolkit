﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Provides the Silver color scheme variant of the Office 2007 palette.
    /// </summary>
    public class PaletteOffice2007Silver : PaletteOffice2007Base
    {
        #region Static Fields
        private static readonly ImageList _checkBoxList;
        private static readonly ImageList _galleryButtonList;
        private static readonly Image[] _radioButtonArray;

        #region Images

        private static readonly Image _silverDropDownButton = GenericImageResources.SilverDropDownButton;
        private static readonly Image _silverCloseNormal = Office2007ControlBoxResources.Office2007ControlBoxSilverCloseNormal_24_x_24;
        private static readonly Image _silverCloseHover = Office2007ControlBoxResources.Office2007ControlBoxSilverCloseHover_24_x_24;
        private static readonly Image _silverCloseDisabled = Office2007ControlBoxResources.Office2007ControlBoxSilverCloseDisabled_24_x_24;
        private static readonly Image _silverClosePressed = Office2007ControlBoxResources.Office2007ControlBoxSilverClosePressed_24_x_24;
        private static readonly Image _silverMaximiseNormal = Office2007ControlBoxResources.Office2007ControlBoxSilverMaximiseNormal_24_x_24;
        private static readonly Image _silverMaximiseHover = Office2007ControlBoxResources.Office2007ControlBoxSilverMaximiseHover_24_x_24;
        private static readonly Image _silverMaximiseDisabled = Office2007ControlBoxResources.Office2007ControlBoxSilverMaximiseDisabled_24_x_24;
        private static readonly Image _silverMaximisePressed = Office2007ControlBoxResources.Office2007ControlBoxSilverMaximisePressed_24_x_24;
        private static readonly Image _silverMinimiseNormal = Office2007ControlBoxResources.Office2007ControlBoxSilverMinimiseNormal_24_x_24;
        private static readonly Image _silverMinimiseHover = Office2007ControlBoxResources.Office2007ControlBoxSilverMinimiseHover_24_x_24;
        private static readonly Image _silverMinimiseDisabled = Office2007ControlBoxResources.Office2007ControlBoxSilverMinimiseDisabled_24_x_24;
        private static readonly Image _silverMinimisePressed = Office2007ControlBoxResources.Office2007ControlBoxSilverMinimisePessed_24_x_24;
        private static readonly Image _silverRestoreNormal = Office2007ControlBoxResources.Office2007ControlBoxSilverRestoreNormal_24_x_24;
        private static readonly Image _silverRestoreHover = Office2007ControlBoxResources.Office2007ControlBoxSilverRestoreHover_24_x_24;
        private static readonly Image _silverRestoreDisabled = Office2007ControlBoxResources.Office2007ControlBoxSilverRestoreDisabled_24_x_24;
        private static readonly Image _silverRestorePressed = Office2007ControlBoxResources.Office2007ControlBoxSilverRestorePressed_24_x_24;
        private static readonly Image _silverHelpNormal = HelpIconResources.Office2007HelpIconNormal;
        private static readonly Image _silverHelpHover = HelpIconResources.Office2007HelpIconHover;
        private static readonly Image _silverHelpPressed = HelpIconResources.Office2007HelpIconPressed;
        private static readonly Image _silverHelpDisabled = HelpIconResources.Office2007HelpIconDisabled;
        private static readonly Image _contextMenuSubMenu = GenericImageResources.SilverContextMenuSub;

        #region Toolbar Images

        private static readonly Image _formToolbarButtonSpecNewNormal = Office2007ToolbarImageResources.Office2007ToolbarNewNormal;
        private static readonly Image _formToolbarButtonSpecNewActive = Office2007ToolbarImageResources.Office2007ToolbarNewNormal;
        private static readonly Image _formToolbarButtonSpecNewDisabled = Office2007ToolbarImageResources.Office2007ToolbarNewDisabled;

        private static readonly Image _formToolbarButtonSpecOpenNormal = Office2007ToolbarImageResources.Office2007ToolbarOpenNormal;
        private static readonly Image _formToolbarButtonSpecOpenActive = Office2007ToolbarImageResources.Office2007ToolbarOpenNormal;
        private static readonly Image _formToolbarButtonSpecOpenDisabled = Office2007ToolbarImageResources.Office2007ToolbarOpenDisabled;

        private static readonly Image _formToolbarButtonSpecSaveNormal = Office2007ToolbarImageResources.Office2007ToolbarSaveNormal;
        private static readonly Image _formToolbarButtonSpecSaveActive = Office2007ToolbarImageResources.Office2007ToolbarSaveNormal;
        private static readonly Image _formToolbarButtonSpecSaveDisabled = Office2007ToolbarImageResources.Office2007ToolbarSaveDisabled;

        private static readonly Image _formToolbarButtonSpecSaveAllNormal = Office2007ToolbarImageResources.Office2007ToolbarSaveAllNormal;
        private static readonly Image _formToolbarButtonSpecSaveAllActive = Office2007ToolbarImageResources.Office2007ToolbarSaveAllNormal;
        private static readonly Image _formToolbarButtonSpecSaveAllDisabled = Office2007ToolbarImageResources.Office2007ToolbarSaveAllDisabled;

        private static readonly Image _formToolbarButtonSpecSaveAsNormal = Office2007ToolbarImageResources.Office2007ToolbarSaveAsNormal;
        private static readonly Image _formToolbarButtonSpecSaveAsActive = Office2007ToolbarImageResources.Office2007ToolbarSaveAsNormal;
        private static readonly Image _formToolbarButtonSpecSaveAsDisabled = Office2007ToolbarImageResources.Office2007ToolbarSaveAsDisabled;

        private static readonly Image _formToolbarButtonSpecCutNormal = Office2007ToolbarImageResources.Office2007ToolbarCutNormal;
        private static readonly Image _formToolbarButtonSpecCutActive = Office2007ToolbarImageResources.Office2007ToolbarCutNormal;
        private static readonly Image _formToolbarButtonSpecCutDisabled = Office2007ToolbarImageResources.Office2007ToolbarCutDisabled;

        private static readonly Image _formToolbarButtonSpecCopyNormal = Office2007ToolbarImageResources.Office2007ToolbarCopyNormal;
        private static readonly Image _formToolbarButtonSpecCopyActive = Office2007ToolbarImageResources.Office2007ToolbarCopyNormal;
        private static readonly Image _formToolbarButtonSpecCopyDisabled = Office2007ToolbarImageResources.Office2007ToolbarCopyDisabled;

        private static readonly Image _formToolbarButtonSpecPasteNormal = Office2007ToolbarImageResources.Office2007ToolbarPasteNormal;
        private static readonly Image _formToolbarButtonSpecPasteActive = Office2007ToolbarImageResources.Office2007ToolbarPasteNormal;
        private static readonly Image _formToolbarButtonSpecPasteDisabled = Office2007ToolbarImageResources.Office2007ToolbarPasteDisabled;

        private static readonly Image _formToolbarButtonSpecUndoNormal = Office2007ToolbarImageResources.Office2007ToolbarUndoNormal;
        private static readonly Image _formToolbarButtonSpecUndoActive = Office2007ToolbarImageResources.Office2007ToolbarUndoNormal;
        private static readonly Image _formToolbarButtonSpecUndoDisabled = Office2007ToolbarImageResources.Office2007ToolbarUndoDisabled;

        private static readonly Image _formToolbarButtonSpecRedoNormal = Office2007ToolbarImageResources.Office2007ToolbarRedoNormal;
        private static readonly Image _formToolbarButtonSpecRedoActive = Office2007ToolbarImageResources.Office2007ToolbarRedoNormal;
        private static readonly Image _formToolbarButtonSpecRedoDisabled = Office2007ToolbarImageResources.Office2007ToolbarRedoDisabled;

        private static readonly Image _formToolbarButtonSpecPageSetupNormal = Office2007ToolbarImageResources.Office2007ToolbarPageSetupNormal;
        private static readonly Image _formToolbarButtonSpecPageSetupActive = Office2007ToolbarImageResources.Office2007ToolbarPageSetupNormal;
        private static readonly Image _formToolbarButtonSpecPageSetupDisabled = Office2007ToolbarImageResources.Office2007ToolbarPageSetupDisabled;

        private static readonly Image _formToolbarButtonSpecPrintPreviewNormal = Office2007ToolbarImageResources.Office2007ToolbarPrintPreviewNormal;
        private static readonly Image _formToolbarButtonSpecPrintPreviewActive = Office2007ToolbarImageResources.Office2007ToolbarPrintPreviewNormal;
        private static readonly Image _formToolbarButtonSpecPrintPreviewDisabled = Office2007ToolbarImageResources.Office2007ToolbarPrintPreviewDisabled;

        private static readonly Image _formToolbarButtonSpecPrintNormal = Office2007ToolbarImageResources.Office2007ToolbarPrintNormal;
        private static readonly Image _formToolbarButtonSpecPrintActive = Office2007ToolbarImageResources.Office2007ToolbarPrintNormal;
        private static readonly Image _formToolbarButtonSpecPrintDisabled = Office2007ToolbarImageResources.Office2007ToolbarPrintDisabled;

        private static readonly Image _formToolbarButtonSpecQuickPrintNormal = Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal;
        private static readonly Image _formToolbarButtonSpecQuickPrintActive = Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal;
        private static readonly Image _formToolbarButtonSpecQuickPrintDisabled = Office2007ToolbarImageResources.Office2007ToolbarQuickPrintDisabled;

        #endregion

        #endregion

        private static readonly Color[] _trackBarColors = { Color.FromArgb(130, 130, 130),      // Tick marks
                                                                        Color.FromArgb(156, 160, 165),      // Top track
                                                                        Color.FromArgb(226, 220, 235),      // Bottom track
                                                                        Color.FromArgb(196, 190, 205),      // Fill track
                                                                        Color.FromArgb(64, Color.White),    // Outside position
                                                                        Color.FromArgb(80, 81, 82)          // Border (normal) position
                                                                      };
        private static readonly Color[] _schemeOfficeColors = { Color.FromArgb( 56,  63,  70),    // TextLabelControl
                                                                      Color.FromArgb( 56,  63,  70),    // TextButtonNormal
                                                                      Color.Black,                      // TextButtonChecked
                                                                      Color.FromArgb(141, 148, 157),    // ButtonNormalBorder1
                                                                      Color.FromArgb(131, 138, 147),    // ButtonNormalBorder2
                                                                      Color.FromArgb(203, 210, 219),    // ButtonNormalBack1
                                                                      Color.FromArgb(240, 244, 249),    // ButtonNormalBack2
                                                                      Color.FromArgb(186, 185, 206),    // ButtonNormalDefaultBack1
                                                                      Color.FromArgb(222, 226, 236),    // ButtonNormalDefaultBack2
                                                                      Color.FromArgb(202, 204, 214),    // ButtonNormalNavigatorBack1
                                                                      Color.FromArgb(222, 226, 236),    // ButtonNormalNavigatorBack2
                                                                      Color.FromArgb(208, 212, 221),    // PanelClient
                                                                      Color.FromArgb(200, 204, 211),    // PanelAlternative
                                                                      Color.FromArgb(111, 112, 116),    // ControlBorder
                                                                      Color.FromArgb(240, 241, 242),    // SeparatorHighBorder1
                                                                      Color.FromArgb(195, 200, 206),    // SeparatorHighBorder2
                                                                      Color.FromArgb(246, 247, 248),    // HeaderPrimaryBack1
                                                                      Color.FromArgb(218, 223, 230),    // HeaderPrimaryBack2
                                                                      Color.FromArgb(213, 219, 231),    // HeaderSecondaryBack1
                                                                      Color.FromArgb(213, 219, 231),    // HeaderSecondaryBack2
                                                                      Color.FromArgb( 21,  66, 139),    // HeaderText
                                                                      Color.FromArgb( 46,  53,  62),    // StatusStripText
                                                                      Color.FromArgb(155, 163, 167),    // ButtonBorder
                                                                      Color.FromArgb(255, 255, 255),    // SeparatorLight
                                                                      Color.FromArgb(110, 109, 143),    // SeparatorDark
                                                                      Color.FromArgb(248, 248, 248),    // GripLight
                                                                      Color.FromArgb(112, 118, 126),    // GripDark
                                                                      Color.FromArgb(208, 212, 221),    // ToolStripBack
                                                                      Color.FromArgb(230, 232, 237),    // StatusStripLight
                                                                      Color.FromArgb(189, 195, 202),    // StatusStripDark
                                                                      Color.FromArgb(239, 239, 239),    // ImageMargin
                                                                      Color.FromArgb(243, 244, 250),    // ToolStripBegin
                                                                      Color.FromArgb(218, 219, 231),    // ToolStripMiddle
                                                                      Color.FromArgb(173, 171, 201),    // ToolStripEnd
                                                                      Color.FromArgb(179, 178, 200),    // OverflowBegin
                                                                      Color.FromArgb(152, 151, 177),    // OverflowMiddle
                                                                      Color.FromArgb(124, 124, 148),    // OverflowEnd
                                                                      Color.FromArgb(124, 124, 148),    // ToolStripBorder
                                                                      Color.FromArgb(114, 120, 128),    // FormBorderActive
                                                                      Color.FromArgb(180, 185, 192),    // FormBorderInactive
                                                                      Color.FromArgb(222, 221, 222),    // FormBorderActiveLight
                                                                      Color.FromArgb(187, 186, 186),    // FormBorderActiveDark
                                                                      Color.FromArgb(240, 240, 240),    // FormBorderInactiveLight
                                                                      Color.FromArgb(224, 224, 224),    // FormBorderInactiveDark
                                                                      Color.FromArgb(172, 175, 183),    // FormBorderHeaderActive
                                                                      Color.FromArgb(182, 181, 181),    // FormBorderHeaderInactive
                                                                      Color.FromArgb(192, 195, 202),    // FormBorderHeaderActive1
                                                                      Color.FromArgb(240, 243, 250),    // FormBorderHeaderActive2
                                                                      Color.FromArgb(217, 219, 225),    // FormBorderHeaderInctive1
                                                                      Color.FromArgb(244, 247, 251),    // FormBorderHeaderInctive2
                                                                      Color.FromArgb( 53, 110, 170),    // FormHeaderShortActive
                                                                      Color.FromArgb(138, 138, 138),    // FormHeaderShortInactive
                                                                      Color.FromArgb( 92,  98, 106),    // FormHeaderLongActive
                                                                      Color.FromArgb(138, 138, 138),    // FormHeaderLongInactive
                                                                      Color.FromArgb(189, 199, 212),    // FormButtonBorderTrack
                                                                      Color.FromArgb(222, 230, 242),    // FormButtonBack1Track
                                                                      Color.FromArgb(255, 255, 255),    // FormButtonBack2Track
                                                                      Color.FromArgb(149, 154, 160),    // FormButtonBorderPressed
                                                                      Color.FromArgb(125, 131, 140),    // FormButtonBack1Pressed
                                                                      Color.FromArgb(213, 226, 233),    // FormButtonBack2Pressed
                                                                      Color.Black,                      // TextButtonFormNormal
                                                                      Color.Black,                      // TextButtonFormTracking
                                                                      Color.Black,                      // TextButtonFormPressed
                                                                      Color.Silver,                       // LinkNotVisitedOverrideControl
                                                                      Color.Purple,                     // LinkVisitedOverrideControl
                                                                      Color.Red,                        // LinkPressedOverrideControl
                                                                      Color.Silver,                       // LinkNotVisitedOverridePanel
                                                                      Color.Purple,                     // LinkVisitedOverridePanel
                                                                      Color.Red,                        // LinkPressedOverridePanel
                                                                      Color.FromArgb( 56,  63,  70),    // TextLabelPanel
                                                                      Color.FromArgb( 76,  83,  92),    // RibbonTabTextNormal
                                                                      Color.FromArgb( 76,  83,  92),    // RibbonTabTextChecked
                                                                      Color.FromArgb(190, 190, 190),    // RibbonTabSelected1
                                                                      Color.FromArgb(198, 250, 255),    // RibbonTabSelected2
                                                                      Color.FromArgb(247, 248, 249),    // RibbonTabSelected3
                                                                      Color.FromArgb(245, 245, 247),    // RibbonTabSelected4
                                                                      Color.FromArgb(239, 234, 241),    // RibbonTabSelected5
                                                                      Color.FromArgb(189, 190, 193),    // RibbonTabTracking1
                                                                      Color.FromArgb(255, 180,  86),    // RibbonTabTracking2
                                                                      Color.FromArgb(255, 255, 189),    // RibbonTabHighlight1
                                                                      Color.FromArgb(249, 237, 198),    // RibbonTabHighlight2
                                                                      Color.FromArgb(218, 185, 127),    // RibbonTabHighlight3
                                                                      Color.FromArgb(254, 209,  94),    // RibbonTabHighlight4
                                                                      Color.FromArgb(205, 209, 180),    // RibbonTabHighlight5
                                                                      Color.FromArgb(175, 176, 179),    // RibbonTabSeparatorColor
                                                                      Color.FromArgb(190, 190, 190),    // RibbonGroupsArea1
                                                                      Color.FromArgb(210, 210, 210),    // RibbonGroupsArea2
                                                                      Color.FromArgb(213, 219, 231),    // RibbonGroupsArea3
                                                                      Color.FromArgb(249, 249, 249),    // RibbonGroupsArea4
                                                                      Color.FromArgb(243, 245, 249),    // RibbonGroupsArea5
                                                                      Color.FromArgb(189, 191, 193),    // RibbonGroupBorder1
                                                                      Color.FromArgb(133, 133, 133),    // RibbonGroupBorder2
                                                                      Color.FromArgb(223, 227, 239),    // RibbonGroupTitle1
                                                                      Color.FromArgb(195, 199, 209),    // RibbonGroupTitle2
                                                                      Color.FromArgb(183, 183, 183),    // RibbonGroupBorderContext1
                                                                      Color.FromArgb(131, 131, 131),    // RibbonGroupBorderContext2
                                                                      Color.FromArgb(223, 227, 239),    // RibbonGroupTitleContext1
                                                                      Color.FromArgb(195, 199, 209),    // RibbonGroupTitleContext2
                                                                      Color.FromArgb(101, 104, 112),    // RibbonGroupDialogDark
                                                                      Color.FromArgb(242, 242, 242),    // RibbonGroupDialogLight
                                                                      Color.FromArgb(222, 226, 238),    // RibbonGroupTitleTracking1
                                                                      Color.FromArgb(179, 185, 199),    // RibbonGroupTitleTracking2
                                                                      Color.FromArgb(128, 128, 128),    // RibbonMinimizeBarDark
                                                                      Color.FromArgb(220, 225, 235),    // RibbonMinimizeBarLight
                                                                      Color.FromArgb(183, 183, 183),    // RibbonGroupCollapsedBorder1
                                                                      Color.FromArgb(145, 145, 145),    // RibbonGroupCollapsedBorder2
                                                                      Color.FromArgb(64, Color.White),  // RibbonGroupCollapsedBorder3
                                                                      Color.FromArgb(225, 227, 227),    // RibbonGroupCollapsedBorder4
                                                                      Color.FromArgb(242, 246, 246),    // RibbonGroupCollapsedBack1
                                                                      Color.FromArgb(207, 212, 220),    // RibbonGroupCollapsedBack2
                                                                      Color.FromArgb(196, 203, 214),    // RibbonGroupCollapsedBack3
                                                                      Color.FromArgb(234, 235, 235),    // RibbonGroupCollapsedBack4
                                                                      Color.FromArgb(188, 193, 213),    // RibbonGroupCollapsedBorderT1
                                                                      Color.FromArgb(142, 178, 179),    // RibbonGroupCollapsedBorderT2
                                                                      Color.FromArgb(192, Color.White), // RibbonGroupCollapsedBorderT3
                                                                      Color.White,                      // RibbonGroupCollapsedBorderT4
                                                                      Color.FromArgb(245, 248, 248),    // RibbonGroupCollapsedBackT1
                                                                      Color.FromArgb(242, 244, 247),    // RibbonGroupCollapsedBackT2
                                                                      Color.FromArgb(238, 241, 245),    // RibbonGroupCollapsedBackT3
                                                                      Color.FromArgb(234, 235, 235),    // RibbonGroupCollapsedBackT4
                                                                      Color.FromArgb(160, 160, 160),    // RibbonGroupFrameBorder1
                                                                      Color.FromArgb(209, 209, 209),    // RibbonGroupFrameBorder2
                                                                      Color.FromArgb(239, 242, 243),    // RibbonGroupFrameInside1
                                                                      Color.FromArgb(226, 229, 234),    // RibbonGroupFrameInside2
                                                                      Color.FromArgb(220, 224, 231),    // RibbonGroupFrameInside3
                                                                      Color.FromArgb(232, 234, 238),    // RibbonGroupFrameInside4
                                                                      Color.FromArgb( 76,  83,  92),    // RibbonGroupCollapsedText         
                                                                      Color.FromArgb(179, 185, 195),    // AlternatePressedBack1
                                                                      Color.FromArgb(216, 224, 224),    // AlternatePressedBack2
                                                                      Color.FromArgb(125, 125, 125),    // AlternatePressedBorder1
                                                                      Color.FromArgb(186, 186, 186),    // AlternatePressedBorder2
                                                                      Color.FromArgb(157, 166, 174),    // FormButtonBack1Checked
                                                                      Color.FromArgb(222, 230, 242),    // FormButtonBack2Checked
                                                                      Color.FromArgb(149, 154, 160),    // FormButtonBorderCheck
                                                                      Color.FromArgb(147, 156, 164),    // FormButtonBack1CheckTrack
                                                                      Color.FromArgb(237, 245, 250),    // FormButtonBack2CheckTrack
                                                                      Color.FromArgb(180, 180, 180),    // RibbonQATMini1
                                                                      Color.FromArgb(210, 215, 221),    // RibbonQATMini2
                                                                      Color.FromArgb(195, 200, 206),    // RibbonQATMini3
                                                                      Color.FromArgb(10, Color.White),  // RibbonQATMini4
                                                                      Color.FromArgb(32, Color.White),  // RibbonQATMini5                                                       
                                                                      Color.FromArgb(200, 200, 200),    // RibbonQATMini1I
                                                                      Color.FromArgb(233, 234, 238),    // RibbonQATMini2I
                                                                      Color.FromArgb(223, 224, 228),    // RibbonQATMini3I
                                                                      Color.FromArgb(10, Color.White),  // RibbonQATMini4I
                                                                      Color.FromArgb(32, Color.White),  // RibbonQATMini5I                                                       
                                                                      Color.FromArgb(217, 222, 230),    // RibbonQATFullbar1                                                      
                                                                      Color.FromArgb(214, 219, 227),    // RibbonQATFullbar2                                                      
                                                                      Color.FromArgb(194, 201, 212),    // RibbonQATFullbar3                                                      
                                                                      Color.FromArgb(103, 103, 103),    // RibbonQATButtonDark                                                      
                                                                      Color.FromArgb(225, 225, 225),    // RibbonQATButtonLight                                                      
                                                                      Color.FromArgb(219, 218, 228),    // RibbonQATOverflow1                                                      
                                                                      Color.FromArgb( 55, 100, 160),    // RibbonQATOverflow2                                                      
                                                                      Color.FromArgb(173, 177, 181),    // RibbonGroupSeparatorDark                                                      
                                                                      Color.FromArgb(232, 235, 237),    // RibbonGroupSeparatorLight                                                      
                                                                      Color.FromArgb(231, 234, 238),    // ButtonClusterButtonBack1                                                      
                                                                      Color.FromArgb(241, 243, 243),    // ButtonClusterButtonBack2                                                      
                                                                      Color.FromArgb(197, 198, 199),    // ButtonClusterButtonBorder1                                                      
                                                                      Color.FromArgb(157, 158, 159),    // ButtonClusterButtonBorder2                                                      
                                                                      Color.FromArgb(238, 238, 244),    // NavigatorMiniBackColor                                                    
                                                                      Color.White,                      // GridListNormal1                                                    
                                                                      Color.FromArgb(212, 215, 219),    // GridListNormal2                                                    
                                                                      Color.FromArgb(210, 213, 218),    // GridListPressed1                                                    
                                                                      Color.FromArgb(252, 253, 253),    // GridListPressed2                                                    
                                                                      Color.FromArgb(186, 189, 194),    // GridListSelected                                                    
                                                                      Color.FromArgb(241, 243, 243),    // GridSheetColNormal1                                                    
                                                                      Color.FromArgb(200, 201, 202),    // GridSheetColNormal2                                                    
                                                                      Color.FromArgb(208, 208, 208),    // GridSheetColPressed1                                                    
                                                                      Color.FromArgb(166, 166, 166),    // GridSheetColPressed2                                                    
                                                                      Color.FromArgb(255, 204, 153),    // GridSheetColSelected1
                                                                      Color.FromArgb(255, 155, 104),    // GridSheetColSelected2
                                                                      Color.FromArgb(231, 231, 231),    // GridSheetRowNormal                                                   
                                                                      Color.FromArgb(184, 191, 196),    // GridSheetRowPressed
                                                                      Color.FromArgb(245, 199, 149),    // GridSheetRowSelected
                                                                      Color.FromArgb(188, 195, 209),    // GridDataCellBorder
                                                                      Color.FromArgb(194, 217, 240),    // GridDataCellSelected
                                                                      Color.Black,                      // InputControlTextNormal
                                                                      Color.FromArgb(172, 168, 153),    // InputControlTextDisabled
                                                                      Color.FromArgb(169, 177, 184),    // InputControlBorderNormal
                                                                      Color.FromArgb(177, 187, 198),    // InputControlBorderDisabled
                                                                      Color.FromArgb(255, 255, 255),    // InputControlBackNormal
                                                                      SystemColors.Control,             // InputControlBackDisabled
                                                                      Color.FromArgb(232, 234, 236),    // InputControlBackInactive
                                                                      Color.FromArgb(124, 124, 124),    // InputDropDownNormal1
                                                                      Color.FromArgb(255, 248, 203),    // InputDropDownNormal2
                                                                      Color.FromArgb(172, 168, 153),    // InputDropDownDisabled1
                                                                      Color.Transparent,                // InputDropDownDisabled2
                                                                      Color.FromArgb(235, 235, 235),    // ContextMenuHeading
                                                                      Color.FromArgb( 76,  83,  92),    // ContextMenuHeadingText
                                                                      Color.FromArgb(239, 239, 239),    // ContextMenuImageColumn
                                                                      Color.FromArgb(250, 250, 250),    // AppButtonBack1
                                                                      Color.FromArgb(217, 226, 230),    // AppButtonBack2
                                                                      Color.FromArgb(169, 174, 180),    // AppButtonBorder
                                                                      Color.FromArgb(207, 212, 217),    // AppButtonOuter1
                                                                      Color.FromArgb(194, 200, 208),    // AppButtonOuter2
                                                                      Color.FromArgb(217, 221, 226),    // AppButtonOuter3
                                                                      Color.FromArgb(250, 250, 250),    // AppButtonInner1
                                                                      Color.FromArgb(169, 174, 180),    // AppButtonInner2
                                                                      Color.FromArgb(241, 242, 245),    // AppButtonMenuDocs
                                                                      Color.FromArgb(76,   83,  92),    // AppButtonMenuDocsText
                                                                      Color.FromArgb(168, 167, 191),    // SeparatorHighInternalBorder1
                                                                      Color.FromArgb(119, 118, 151),    // SeparatorHighInternalBorder2
                                                                      Color.FromArgb(169, 177, 184),    // RibbonGalleryBorder
                                                                      Color.FromArgb(232, 234, 236),    // RibbonGalleryBackNormal
                                                                      Color.FromArgb(240, 241, 242),    // RibbonGalleryBackTracking
                                                                      Color.FromArgb(195, 200, 209),    // RibbonGalleryBack1
                                                                      Color.FromArgb(217, 220, 224),    // RibbonGalleryBack2
                                                                      Color.Empty,                      // RibbonTabTracking3
                                                                      Color.Empty,                      // RibbonTabTracking4
                                                                      Color.Empty,                      // RibbonGroupBorder3
                                                                      Color.Empty,                      // RibbonGroupBorder4
                                                                      Color.Empty,                      // RibbonDropArrowLight
                                                                      Color.Empty // RibbonDropArrowDark
                                                                };
        #endregion

        #region Identity
        static PaletteOffice2007Silver()
        {
            _checkBoxList = new ImageList
            {
                ImageSize = new Size(13, 13),
                ColorDepth = ColorDepth.Depth24Bit
            };
            _checkBoxList.Images.AddStrip(CheckBoxStripResources.CheckBoxStrip2007Silver);
            _galleryButtonList = new ImageList
            {
                ImageSize = new Size(13, 7),
                ColorDepth = ColorDepth.Depth24Bit,
                TransparentColor = Color.Magenta
            };
            _galleryButtonList.Images.AddStrip(GalleryImageResources.GallerySilverBlack);
            _radioButtonArray = new Image[]{Office2007BlueRadioButtonResources.RadioButton2007BlueD,
                                            Office2007SilverRadioButtonResources.RadioButton2007SilverN,
                                            Office2007SilverRadioButtonResources.RadioButton2007SilverT,
                                            Office2007SilverRadioButtonResources.RadioButton2007SilverP,
                                            Office2007BlueRadioButtonResources.RadioButton2007BlueDC,
                                            Office2007SilverRadioButtonResources.RadioButton2007SilverNC,
                                            Office2007SilverRadioButtonResources.RadioButton2007SilverTC,
                                            Office2007SilverRadioButtonResources.RadioButton2007SilverPC};
        }

        /// <summary>
        /// Initialize a new instance of the PaletteOffice2007Silver class.
        /// </summary>
        public PaletteOffice2007Silver()
            : base("Office 2007 - Silver",
                    _schemeOfficeColors,
                    _checkBoxList,
                    _galleryButtonList,
                    _radioButtonArray,
                    _trackBarColors)
        {
        }
        #endregion

        #region Back
        /// <summary>
        /// Gets the color background drawing style.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state)
        {
            return style switch
            {
                PaletteBackStyle.HeaderForm => PaletteColorStyle.Rounding2,
                _ => base.GetBackColorStyle(style, state)
            };
        }
        #endregion

        #region Images
        /// <summary>
        /// Gets a drop down button image appropriate for the provided state.
        /// </summary>
        /// <param name="state">PaletteState for which image is required.</param>
        public override Image? GetDropDownButtonImage(PaletteState state) => state != PaletteState.Disabled ? _silverDropDownButton : base.GetDropDownButtonImage(state);

        /// <summary>
        /// Gets an image indicating a sub-menu on a context menu item.
        /// </summary>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image? GetContextMenuSubMenuImage() => _contextMenuSubMenu;

        #endregion

        #region ButtonSpec
        /// <summary>
        /// Gets the image to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <param name="state">State for which image is required.</param>
        /// <returns>Image value.</returns>
        public override Image? GetButtonSpecImage(PaletteButtonSpecStyle style,
                                                 PaletteState state)
        {
            return style switch
            {
                PaletteButtonSpecStyle.FormClose => state switch
                {
                    PaletteState.Disabled => _silverCloseDisabled,
                    PaletteState.Tracking => _silverCloseHover,
                    PaletteState.Pressed => _silverClosePressed,
                    _ => _silverCloseNormal
                },
                PaletteButtonSpecStyle.FormMin => state switch
                {
                    PaletteState.Disabled => _silverMinimiseDisabled,
                    PaletteState.Tracking => _silverMinimiseHover,
                    PaletteState.Pressed => _silverMinimisePressed,
                    _ => _silverMinimiseNormal
                },
                PaletteButtonSpecStyle.FormMax => state switch
                {
                    PaletteState.Disabled => _silverMaximiseDisabled,
                    PaletteState.Tracking => _silverMaximiseHover,
                    PaletteState.Pressed => _silverMaximisePressed,
                    _ => _silverMaximiseNormal
                },
                PaletteButtonSpecStyle.FormRestore => state switch
                {
                    PaletteState.Disabled => _silverRestoreDisabled,
                    PaletteState.Tracking => _silverRestoreHover,
                    PaletteState.Pressed => _silverRestorePressed,
                    _ => _silverRestoreNormal
                },
                PaletteButtonSpecStyle.FormHelp => state switch
                {
                    PaletteState.Disabled => _silverHelpDisabled,
                    PaletteState.Tracking => _silverHelpHover,
                    PaletteState.Pressed => _silverHelpPressed,
                    _ => _silverHelpNormal
                },
                PaletteButtonSpecStyle.New => state switch
                {
                    PaletteState.Tracking => _formToolbarButtonSpecNewActive,
                    PaletteState.Normal => _formToolbarButtonSpecNewNormal,
                    _ => _formToolbarButtonSpecNewDisabled
                },
                PaletteButtonSpecStyle.Open => state switch
                {
                    PaletteState.Tracking => _formToolbarButtonSpecOpenActive,
                    PaletteState.Normal => _formToolbarButtonSpecOpenNormal,
                    _ => _formToolbarButtonSpecOpenDisabled
                },
                PaletteButtonSpecStyle.Save => state switch
                {
                    PaletteState.Tracking => _formToolbarButtonSpecSaveActive,
                    PaletteState.Normal => _formToolbarButtonSpecSaveNormal,
                    _ => _formToolbarButtonSpecSaveDisabled
                },
                PaletteButtonSpecStyle.SaveAs => state switch
                {
                    PaletteState.Tracking => _formToolbarButtonSpecSaveAsActive,
                    PaletteState.Normal => _formToolbarButtonSpecSaveAsNormal,
                    _ => _formToolbarButtonSpecSaveAsDisabled
                },
                PaletteButtonSpecStyle.SaveAll => state switch
                {
                    PaletteState.Tracking => _formToolbarButtonSpecSaveAllActive,
                    PaletteState.Normal => _formToolbarButtonSpecSaveAllNormal,
                    _ => _formToolbarButtonSpecSaveAllDisabled
                },
                PaletteButtonSpecStyle.Cut => state switch
                {
                    PaletteState.Tracking => _formToolbarButtonSpecCutActive,
                    PaletteState.Normal => _formToolbarButtonSpecCutNormal,
                    _ => _formToolbarButtonSpecCutDisabled
                },
                PaletteButtonSpecStyle.Copy => state switch
                {
                    PaletteState.Tracking => _formToolbarButtonSpecCopyActive,
                    PaletteState.Normal => _formToolbarButtonSpecCopyNormal,
                    _ => _formToolbarButtonSpecCopyDisabled
                },
                PaletteButtonSpecStyle.Paste => state switch
                {
                    PaletteState.Tracking => _formToolbarButtonSpecPasteActive,
                    PaletteState.Normal => _formToolbarButtonSpecPasteNormal,
                    _ => _formToolbarButtonSpecPasteDisabled
                },
                PaletteButtonSpecStyle.Undo => state switch
                {
                    PaletteState.Tracking => _formToolbarButtonSpecUndoActive,
                    PaletteState.Normal => _formToolbarButtonSpecUndoNormal,
                    _ => _formToolbarButtonSpecUndoDisabled
                },
                PaletteButtonSpecStyle.Redo => state switch
                {
                    PaletteState.Tracking => _formToolbarButtonSpecRedoActive,
                    PaletteState.Normal => _formToolbarButtonSpecRedoNormal,
                    _ => _formToolbarButtonSpecRedoDisabled
                },
                PaletteButtonSpecStyle.PageSetup => state switch
                {
                    PaletteState.Tracking => _formToolbarButtonSpecPageSetupActive,
                    PaletteState.Normal => _formToolbarButtonSpecPageSetupNormal,
                    _ => _formToolbarButtonSpecPageSetupDisabled
                },
                PaletteButtonSpecStyle.PrintPreview => state switch
                {
                    PaletteState.Tracking => _formToolbarButtonSpecPrintPreviewActive,
                    PaletteState.Normal => _formToolbarButtonSpecPrintPreviewNormal,
                    _ => _formToolbarButtonSpecPrintPreviewDisabled
                },
                PaletteButtonSpecStyle.Print => state switch
                {
                    PaletteState.Tracking => _formToolbarButtonSpecPrintActive,
                    PaletteState.Normal => _formToolbarButtonSpecPrintNormal,
                    _ => _formToolbarButtonSpecPrintDisabled
                },
                PaletteButtonSpecStyle.QuickPrint => state switch
                {
                    PaletteState.Tracking => _formToolbarButtonSpecQuickPrintActive,
                    PaletteState.Normal => _formToolbarButtonSpecQuickPrintNormal,
                    _ => _formToolbarButtonSpecQuickPrintDisabled
                },
                _ => base.GetButtonSpecImage(style, state)
            };
        }
        #endregion    
    }
}
