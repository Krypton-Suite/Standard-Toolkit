#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2024. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Gets the single instance of the PaletteMicrosoft365Black palette.
    /// </summary>
    public class PaletteMicrosoft365Black : PaletteMicrosoft365Base
    {
        #region Static Fields

        #region Colors

        private static readonly Color _tabRowBackgroundColor = Color.FromArgb(99, 99, 99);

        #endregion

        #region Ribbon Specific Colors

        private static readonly Color _ribbonAppButtonDarkColor = GlobalStaticValues.DEFAULT_RIBBON_FILE_APP_TAB_BOTTOM_COLOR;

        private static readonly Color _ribbonAppButtonLightColor = GlobalStaticValues.DEFAULT_RIBBON_FILE_APP_TAB_TOP_COLOR;

        private static readonly Color _ribbonAppButtonTextColor = GlobalStaticValues.DEFAULT_RIBBON_FILE_APP_TAB_TEXT_COLOR;

        #endregion

        #region Image Lists

        private static readonly ImageList _checkBoxList;
        private static readonly ImageList _galleryButtonList;

        #endregion

        #region Image Array

        private static readonly Image?[] _radioButtonArray;

        #endregion

        #region Images

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

        private static readonly Color[] _trackBarColors =
        [
            Color.FromArgb(17, 17, 17), // Tick marks
            Color.FromArgb(37, 37, 37), // Top track
            Color.FromArgb(174, 174, 174), // Bottom track
            Color.FromArgb(131, 132, 132), // Fill track
            Color.FromArgb(64, Color.White), // Outside position
            Color.FromArgb(35, 35, 35) // Border (normal) position
        ];


        private static readonly Color[] _schemeOfficeColors =
        [
            Color.FromArgb(70, 70, 70), // (76, 83, 92), // TextLabelControl
            Color.FromArgb(70, 70, 70), // TextButtonNormal
            Color.Black, // TextButtonChecked
            Color.FromArgb(106, 106, 106), // ButtonNormalBorder1
            Color.FromArgb(32, 32, 32), // ButtonNormalDefaultBorder
            Color.FromArgb(189, 189, 189), // ButtonNormalBack1
            Color.FromArgb(169, 169, 169), // ButtonNormalBack2
            Color.FromArgb(225, 225, 225), // ButtonNormalDefaultBack1
            Color.FromArgb(185, 185, 185), // ButtonNormalDefaultBack2
            Color.FromArgb(32, 32, 32), // ButtonNormalNavigatorBack1
            Color.FromArgb(32, 32, 32), // ButtonNormalNavigatorBack2
            Color.FromArgb(99, 99, 99), // PanelClient
            Color.FromArgb(61, 61, 61), // PanelAlternative
            Color.FromArgb(46, 46, 46), // ControlBorder
            Color.FromArgb(172, 172, 172), // SeparatorHighBorder1
            Color.FromArgb(111, 111, 111), // SeparatorHighBorder2
            Color.FromArgb(139, 139, 139), // HeaderPrimaryBack1
            Color.FromArgb(72, 72, 72),    // HeaderPrimaryBack2
            Color.FromArgb(190, 190, 190), // HeaderSecondaryBack1
            Color.FromArgb(145, 145, 145), // HeaderSecondaryBack2
            Color.Black, // HeaderText
            Color.FromArgb(226, 226, 226), // StatusStripText
            Color.FromArgb(236, 199, 87), // ButtonBorder
            Color.FromArgb(89, 89, 89), // SeparatorLight
            Color.Black, // SeparatorDark
            Color.FromArgb(89, 89, 89), // GripLight
            Color.FromArgb(27, 27, 27), // GripDark
            Color.FromArgb(113, 113, 113), // ToolStripBack
            Color.FromArgb(75, 75, 75), // StatusStripLight
            Color.FromArgb(50, 50, 50), // StatusStripDark
            Color.White, // ImageMargin
            Color.FromArgb(75, 75, 75), // ToolStripBegin
            Color.FromArgb(50, 50, 50), // ToolStripMiddle
            Color.FromArgb(50, 50, 50), // ToolStripEnd
            Color.FromArgb(44, 44, 44), // OverflowBegin
            Color.FromArgb(167, 167, 167), // OverflowMiddle
            Color.FromArgb(44, 44, 44), // OverflowEnd
            Color.FromArgb(44, 44, 44), // ToolStripBorder
            Color.FromArgb(99, 99, 99), // FormBorderActive
            Color.FromArgb(119, 119, 119), // FormBorderInactive
            Color.FromArgb(113, 113, 113), // FormBorderActiveLight
            Color.FromArgb(131, 131, 131), // FormBorderActiveDark
            Color.FromArgb(158, 158, 158), // FormBorderInactiveLight
            Color.FromArgb(158, 158, 158), // FormBorderInactiveDark
            Color.FromArgb(65, 65, 65), // FormBorderHeaderActive
            Color.FromArgb(154, 154, 154), // FormBorderHeaderInactive
            Color.FromArgb(121, 121, 121), // FormBorderHeaderActive1
            Color.FromArgb(113, 113, 113), // FormBorderHeaderActive2
            Color.FromArgb(158, 158, 158), // FormBorderHeaderInctive1
            Color.FromArgb(158, 158, 158), // FormBorderHeaderInctive2
            Color.FromArgb(226, 226, 226), // FormHeaderShortActive
            Color.FromArgb(212, 212, 212), // FormHeaderShortInactive
            Color.FromArgb(226, 226, 226), // FormHeaderLongActive
            Color.FromArgb(212, 212, 212), // FormHeaderLongInactive
            Color.FromArgb(81, 81, 81), // FormButtonBorderTrack
            Color.FromArgb(151, 151, 151), // FormButtonBack1Track
            Color.FromArgb(116, 116, 116), // FormButtonBack2Track
            Color.FromArgb(81, 81, 81), // FormButtonBorderPressed
            Color.FromArgb(113, 113, 113), // FormButtonBack1Pressed
            Color.FromArgb(93, 93, 93), // FormButtonBack2Pressed
            Color.FromArgb(70, 70, 70), // TextButtonFormNormal
            Color.FromArgb(255, 255, 255), // TextButtonFormTracking
            Color.FromArgb(255, 255, 255), // TextButtonFormPressed
            Color.Blue, // LinkNotVisitedOverrideControl
            Color.Purple, // LinkVisitedOverrideControl
            Color.Red, // LinkPressedOverrideControl
            Color.FromArgb(180, 210, 255), // LinkNotVisitedOverridePanel
            Color.Violet, // LinkVisitedOverridePanel
            Color.FromArgb(255, 90, 90), // LinkPressedOverridePanel
            Color.White, // TextLabelPanel
            //Color.FromArgb(226, 226, 226),    // RibbonTabTextNormal
            Color.White, // RibbonTabTextNormal
            Color.Black, // RibbonTabTextChecked
            Color.FromArgb(32, 32, 32),    // RibbonTabSelected1
            Color.FromArgb(201, 201, 201), // RibbonTabSelected2
            Color.FromArgb(192, 192, 192), // RibbonTabSelected3
            Color.FromArgb(192, 192, 192), // RibbonTabSelected4
            Color.FromArgb(192, 192, 192), // RibbonTabSelected5
            Color.FromArgb(32, 32, 32),    // RibbonTabTracking1
            Color.FromArgb(183, 183, 183), // RibbonTabTracking2
            Color.FromArgb(32, 32, 32),    // RibbonTabHighlight1
            Color.FromArgb(201, 201, 201), // RibbonTabHighlight2
            Color.FromArgb(192, 192, 192), // RibbonTabHighlight3
            Color.FromArgb(192, 192, 192), // RibbonTabHighlight4
            Color.FromArgb(192, 192, 192), // RibbonTabHighlight5
            Color.FromArgb(54, 54, 54), // RibbonTabSeparatorColor
            Color.FromArgb(32, 32, 32), // RibbonGroupsArea1
            Color.FromArgb(50, 50, 50), // RibbonGroupsArea2
            Color.FromArgb(32, 32, 32), // RibbonGroupsArea3
            Color.FromArgb(33, 33, 33), // RibbonGroupsArea4
            Color.FromArgb(33, 33, 33), // RibbonGroupsArea5
            Color.FromArgb(159, 159, 159), // RibbonGroupBorder1
            Color.FromArgb(194, 194, 194), // RibbonGroupBorder2
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupTitle1
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupTitle2
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupBorderContext1
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupBorderContext2
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupTitleContext1
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupTitleContext2
            Color.FromArgb(92, 92, 94), // RibbonGroupDialogDark
            Color.FromArgb(123, 125, 125), // RibbonGroupDialogLight
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupTitleTracking1
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupTitleTracking2
            Color.FromArgb(61, 61, 61), // RibbonMinimizeBarDark
            Color.FromArgb(99, 99, 99), // RibbonMinimizeBarLight
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupCollapsedBorder1
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupCollapsedBorder2
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupCollapsedBorder3
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupCollapsedBorder4
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupCollapsedBack1
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupCollapsedBack2
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupCollapsedBack3
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupCollapsedBack4
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupCollapsedBorderT1
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupCollapsedBorderT2
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupCollapsedBorderT3
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupCollapsedBorderT4
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupCollapsedBackT1
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupCollapsedBackT2
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupCollapsedBackT3
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupCollapsedBackT4
            Color.FromArgb(147, 147, 147), // RibbonGroupFrameBorder1
            Color.FromArgb(139, 139, 139), // RibbonGroupFrameBorder2
            Color.FromArgb(187, 187, 188), // RibbonGroupFrameInside1
            Color.FromArgb(167, 167, 168), // RibbonGroupFrameInside2
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupFrameInside3
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupFrameInside4
            Color.FromArgb(255, 255, 255), // RibbonGroupCollapsedText         
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
            Color.FromArgb(132, 132, 132), // RibbonQATFullbar1                                                      
            Color.FromArgb(121, 121, 121), // RibbonQATFullbar2                                                      
            Color.FromArgb(50, 49, 49), // RibbonQATFullbar3                                                      
            Color.FromArgb(90, 90, 90), // RibbonQATButtonDark                                                      
            Color.FromArgb(174, 174, 175), // RibbonQATButtonLight                                                      
            Color.FromArgb(161, 161, 161), // RibbonQATOverflow1                                                      
            Color.FromArgb(68, 68, 68), // RibbonQATOverflow2                                                      
            Color.FromArgb(82, 82, 82), // RibbonGroupSeparatorDark                                                      
            Color.FromArgb(190, 190, 190), // RibbonGroupSeparatorLight                                                      
            Color.FromArgb(210, 217, 219), // ButtonClusterButtonBack1                                                      
            Color.FromArgb(214, 222, 223), // ButtonClusterButtonBack2                                                      
            Color.FromArgb(179, 188, 191), // ButtonClusterButtonBorder1                                                      
            Color.FromArgb(145, 156, 159), // ButtonClusterButtonBorder2                                                      
            Color.FromArgb(235, 235, 235), // NavigatorMiniBackColor                                                    
            Color.FromArgb(205, 205, 205), // GridListNormal1                                                    
            Color.FromArgb(166, 166, 166), // GridListNormal2                                                    
            Color.FromArgb(166, 166, 166), // GridListPressed1                                                    
            Color.FromArgb(205, 205, 205), // GridListPressed2                                                    
            Color.FromArgb(150, 150, 150), // GridListSelected                                                    
            Color.FromArgb(220, 220, 220), // GridSheetColNormal1                                                    
            Color.FromArgb(200, 200, 200), // GridSheetColNormal2                                                    
            Color.FromArgb(255, 223, 107), // GridSheetColPressed1                                                    
            Color.FromArgb(255, 252, 230), // GridSheetColPressed2                                                    
            Color.FromArgb(255, 211, 89), // GridSheetColSelected1
            Color.FromArgb(255, 239, 113), // GridSheetColSelected2
            Color.FromArgb(205, 205, 205), // GridSheetRowNormal                                                   
            Color.FromArgb(255, 223, 107), // GridSheetRowPressed
            Color.FromArgb(245, 210, 87), // GridSheetRowSelected
            Color.FromArgb(218, 220, 221), // GridDataCellBorder
            Color.FromArgb(183, 219, 255), // GridDataCellSelected
            Color.FromArgb(70, 70, 70), // InputControlTextNormal
            Color.FromArgb(128, 128, 128), // InputControlTextDisabled
            Color.FromArgb(132, 132, 132), // InputControlBorderNormal
            Color.FromArgb(187, 187, 187), // InputControlBorderDisabled
            Color.FromArgb(255, 255, 255), // InputControlBackNormal
            Color.FromArgb(240, 240, 240), // InputControlBackDisabled
            Color.FromArgb(192, 192, 192), // InputControlBackInactive
            Color.Black, // InputDropDownNormal1
            Color.Transparent, // InputDropDownNormal2
            Color.FromArgb(172, 168, 153), // InputDropDownDisabled1
            Color.Transparent, // InputDropDownDisabled2
            Color.FromArgb(240, 242, 245), // ContextMenuHeadingBack
            Color.Black, // ContextMenuHeadingText
            Color.White, // ContextMenuImageColumn
            Color.FromArgb(70, 70, 70), // AppButtonBack1
            Color.FromArgb(70, 70, 70), // AppButtonBack2
            Color.FromArgb(50, 50, 50), // AppButtonBorder
            Color.FromArgb(70, 70, 70), // AppButtonOuter1
            Color.FromArgb(70, 70, 70), // AppButtonOuter2
            Color.FromArgb(70, 70, 70), // AppButtonOuter3
            GlobalStaticValues.EMPTY_COLOR, // AppButtonInner1
            Color.FromArgb(50, 50, 50), // AppButtonInner2
            Color.White, // AppButtonMenuDocs
            Color.Black, // AppButtonMenuDocsText
            Color.FromArgb(172, 172, 172), // SeparatorHighInternalBorder1
            Color.FromArgb(111, 111, 111), // SeparatorHighInternalBorder2
            Color.FromArgb(132, 132, 132), // RibbonGalleryBorder
            Color.FromArgb(187, 187, 187), // RibbonGalleryBackNormal
            Color.FromArgb(193, 193, 193), // RibbonGalleryBackTracking
            Color.FromArgb(176, 176, 176), // RibbonGalleryBack1
            Color.FromArgb(150, 150, 150), // RibbonGalleryBack2
            Color.FromArgb(148, 149, 151), // RibbonTabTracking3
            Color.FromArgb(127, 127, 127), // RibbonTabTracking4
            Color.FromArgb(82, 82, 82), // RibbonGroupBorder3
            Color.FromArgb(176, 176, 176), // RibbonGroupBorder4
            Color.FromArgb(178, 178, 178), // RibbonGroupBorder5
            Color.White, // FromArgb(36, 36, 36), // RibbonGroupTitleText
            Color.FromArgb(155, 157, 160), // RibbonDropArrowLight
            Color.FromArgb(27, 29, 40), // RibbonDropArrowDark
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
        static PaletteMicrosoft365Black()
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
                TransparentColor = Color.Magenta
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
        /// Initializes a new instance of the <see cref="PaletteMicrosoft365Black"/> class.
        /// </summary>
        public PaletteMicrosoft365Black() : base(_schemeOfficeColors, _checkBoxList, _galleryButtonList, _radioButtonArray, _trackBarColors)
        {

        }
        #endregion

        #region Images        
        /// <summary>
        /// Gets a drop down button image appropriate for the provided state.
        /// </summary>
        /// <param name="state">PaletteState for which image is required.</param>
        /// <returns></returns>
        public override Image? GetDropDownButtonImage(PaletteState state) => state != PaletteState.Disabled ? _blackDropDownButton : base.GetDropDownButtonImage(state);

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
            PaletteButtonSpecStyle.RibbonMinimize => _buttonSpecRibbonMinimize,
            PaletteButtonSpecStyle.RibbonExpand => _buttonSpecRibbonExpand,
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

}