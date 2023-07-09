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
    /// Gets the single instance of the ### palette.
    /// </summary>
    public class PaletteMicrosoft365White : PaletteMicrosoft365Base
    {
        #region Static Fields

        #region Image Lists

        private static readonly ImageList _checkBoxList;
        private static readonly ImageList _galleryButtonList;

        #endregion

        #region Image Array

        private static readonly Image[] _radioButtonArray;

        #endregion

        #region Images

        private static readonly Image _silverDropDownButton = Office2010ArrowResources.Office2010BlueDropDownButton;
        private static readonly Image _contextMenuSubMenu = Office2010ArrowResources.Office2010BlueContextMenuSub;
        private static readonly Image _formCloseNormal = Office2010ControlBoxResources.Office2010SilverCloseNormal;
        private static readonly Image _formCloseDisabled = Office2010ControlBoxResources.Office2010SilverCloseDisabled;
        private static readonly Image _formCloseActive = Office2010ControlBoxResources.Office2010SilverCloseActive;
        private static readonly Image _formClosePressed = Office2010ControlBoxResources.Office2010SilverClosePressed;
        private static readonly Image _formMaximiseNormal = Office2010ControlBoxResources.Office2010SilverMaximiseNormal;
        private static readonly Image _formMaximiseDisabled = Office2010ControlBoxResources.Office2010SilverMaximiseDisabled;
        private static readonly Image _formMaximiseActive = Office2010ControlBoxResources.Office2010SilverMaximiseActive;
        private static readonly Image _formMaximisePressed = Office2010ControlBoxResources.Office2010SilverMaximisePressed;
        private static readonly Image _formMinimiseNormal = Office2010ControlBoxResources.Office2010SilverMinimiseNormal;
        private static readonly Image _formMinimiseActive = Office2010ControlBoxResources.Office2010SilverMinimiseActive;
        private static readonly Image _formMinimiseDisabled = Office2010ControlBoxResources.Office2010SilverMinimiseDisabled;
        private static readonly Image _formMinimisePressed = Office2010ControlBoxResources.Office2010SilverMinimisePressed;
        private static readonly Image _formRestoreNormal = Office2010ControlBoxResources.Office2010SilverRestoreNormal;
        private static readonly Image _formRestoreDisabled = Office2010ControlBoxResources.Office2010SilverRestoreDisabled;
        private static readonly Image _formRestoreActive = Office2010ControlBoxResources.Office2010SilverRestoreActive;
        private static readonly Image _formRestorePressed = Office2010ControlBoxResources.Office2010SilverRestorePressed;
        private static readonly Image _formHelpNormal = Microsoft365ControlBoxResources.Microsoft365HelpIconNormal;
        private static readonly Image _formHelpActive = Microsoft365ControlBoxResources.Microsoft365HelpIconHover;
        private static readonly Image _formHelpPressed = Microsoft365ControlBoxResources.Microsoft365HelpIconPressed;
        private static readonly Image _formHelpDisabled = Microsoft365ControlBoxResources.Microsoft365HelpIconDisabled;

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
        {
            Color.Red, // Tick marks
            Color.FromArgb(166, 170, 175), // Top track
            Color.FromArgb(226, 220, 235), // Bottom track
            Color.FromArgb(206, 200, 215), // Fill track
            Color.FromArgb(64, Color.White), // Outside position
            Color.FromArgb(80, 81, 82) // Border (normal) position
        };


        private static readonly Color[] _schemeOfficeColors =
        {
            Color.FromArgb(59, 59, 59), // TextLabelControl
            Color.FromArgb(59, 59, 59), // TextButtonNormal
            Color.Black, // TextButtonChecked
            Color.FromArgb(170, 170, 170), // ButtonNormalBorder1 -n
            Color.FromArgb(170, 170, 170), // ButtonNormalDefaultBorder -n
            Color.FromArgb(253, 253, 253), // ButtonNormalBack1 -n
            Color.FromArgb(253, 253, 253), // ButtonNormalBack2 -n
            Color.FromArgb(235, 235, 235), // ButtonNormalDefaultBack1
            Color.FromArgb(195, 195, 195), // ButtonNormalDefaultBack2
            Color.FromArgb(207, 212, 218), // ButtonNormalNavigatorBack1
            Color.FromArgb(207, 212, 218), // ButtonNormalNavigatorBack2
            Color.White, // PanelClient -n
            Color.FromArgb(207, 212, 218), // PanelAlternative
            Color.FromArgb(213, 213, 213), // ControlBorder -n
            Color.FromArgb(250, 253, 255), // SeparatorHighBorder1
            Color.FromArgb(227, 232, 237), // SeparatorHighBorder2
            Color.FromArgb(255, 255, 255), // HeaderPrimaryBack1 -n
            Color.FromArgb(255, 255, 255), // HeaderPrimaryBack2 -n
            Color.FromArgb(255, 255, 255), // HeaderSecondaryBack1
            Color.FromArgb(255, 255, 255), // HeaderSecondaryBack2-n
            Color.FromArgb(59, 59, 59), // HeaderText
            Color.FromArgb(255, 255, 255), // StatusStripText
            Color.FromArgb(236, 199, 87), // ButtonBorder
            Color.FromArgb(247, 250, 252), // SeparatorLight
            Color.FromArgb(119, 123, 127), // SeparatorDark
            Color.FromArgb(191, 191, 191), // GripLight
            Color.FromArgb(191, 191, 191), // GripDark
            Color.FromArgb(227, 230, 232), // ToolStripBack
            Color.FromArgb(0, 114, 198), // StatusStripLight
            Color.FromArgb(0, 114, 198), // StatusStripDark
            Color.White, // ImageMargin
            Color.FromArgb(25, 71, 138), // ToolStripBegin
            Color.FromArgb(25, 71, 138), // ToolStripMiddle
            Color.FromArgb(25, 71, 138), // ToolStripEnd
            Color.FromArgb(147, 154, 163), // OverflowBegin
            Color.FromArgb(147, 154, 163), // OverflowMiddle
            Color.FromArgb(147, 154, 163), // OverflowEnd
            Color.FromArgb(147, 154, 163), // ToolStripBorder
            Color.FromArgb(0, 114, 198), // FormBorderActive -n
            Color.FromArgb(134, 139, 145), // FormBorderInactive
            Color.FromArgb(228, 230, 232), // FormBorderActiveLight
            Color.FromArgb(255, 255, 255), // FormBorderActiveDark
            Color.FromArgb(248, 247, 247), // FormBorderInactiveLight
            Color.FromArgb(248, 247, 247), // FormBorderInactiveDark
            Color.FromArgb(101, 109, 117), // FormBorderHeaderActive
            Color.FromArgb(134, 139, 145), // FormBorderHeaderInactive
            Color.FromArgb(235, 237, 240), // FormBorderHeaderActive1
            Color.FromArgb(228, 230, 232), // FormBorderHeaderActive2
            Color.FromArgb(248, 247, 247), // FormBorderHeaderInctive1
            Color.FromArgb(248, 247, 247), // FormBorderHeaderInctive2
            Color.FromArgb(59, 59, 59), // FormHeaderShortActive
            Color.FromArgb(138, 138, 138), // FormHeaderShortInactive
            Color.FromArgb(59, 59, 59), // FormHeaderLongActive
            Color.FromArgb(138, 138, 138), // FormHeaderLongInactive
            Color.FromArgb(166, 172, 179), // FormButtonBorderTrack
            Color.FromArgb(255, 255, 255), // FormButtonBack1Track
            Color.FromArgb(228, 228, 229), // FormButtonBack2Track
            Color.FromArgb(166, 172, 179), // FormButtonBorderPressed
            Color.FromArgb(223, 228, 235), // FormButtonBack1Pressed
            Color.FromArgb(188, 193, 200), // FormButtonBack2Pressed
            Color.Black, // TextButtonFormNormal
            Color.Black, // TextButtonFormTracking
            Color.Black, // TextButtonFormPressed
            Color.Blue, // LinkNotVisitedOverrideControl
            Color.Purple, // LinkVisitedOverrideControl
            Color.Red, // LinkPressedOverrideControl
            Color.Blue, // LinkNotVisitedOverridePanel
            Color.Purple, // LinkVisitedOverridePanel
            Color.Red, // LinkPressedOverridePanel
            Color.FromArgb(59, 59, 59), // TextLabelPanel
            Color.FromArgb(102, 102, 102), // RibbonTabTextNormal -n
            Color.FromArgb(0, 114, 198), // RibbonTabTextChecked -n
            Color.FromArgb(182, 186, 191), // RibbonTabSelected1
            Color.White, // RibbonTabSelected2
            Color.White, // RibbonTabSelected3
            Color.White, // RibbonTabSelected4
            Color.White, // RibbonTabSelected5
            Color.FromArgb(177, 181, 186), // RibbonTabTracking1
            Color.FromArgb(248, 249, 249), // RibbonTabTracking2
            Color.FromArgb(182, 186, 191), // RibbonTabHighlight1
            Color.White, // RibbonTabHighlight2
            Color.White, // RibbonTabHighlight3
            Color.White, // RibbonTabHighlight4
            Color.White, // RibbonTabHighlight5
            Color.FromArgb(182, 186, 191), // RibbonTabSeparatorColor
            Color.FromArgb(212, 212, 212), // RibbonGroupsArea1 -n
            Color.FromArgb(212, 212, 212), // RibbonGroupsArea2 -n
            Color.White, // RibbonGroupsArea3 -n
            Color.White, // RibbonGroupsArea4 -n
            Color.White, // RibbonGroupsArea5 -n
            Color.Empty, // RibbonGroupBorder1 -n
            Color.Empty, // RibbonGroupBorder2 -n
            Color.Empty, // RibbonGroupTitle1
            Color.Empty, // RibbonGroupTitle2
            Color.Empty, // RibbonGroupBorderContext1
            Color.Empty, // RibbonGroupBorderContext2
            Color.Empty, // RibbonGroupTitleContext1
            Color.Empty, // RibbonGroupTitleContext2
            Color.FromArgb(148, 149, 152), // RibbonGroupDialogDark
            Color.FromArgb(180, 182, 183), // RibbonGroupDialogLight
            Color.Empty, // RibbonGroupTitleTracking1
            Color.Empty, // RibbonGroupTitleTracking2
            Color.FromArgb(139, 144, 151), // RibbonMinimizeBarDark
            Color.FromArgb(205, 209, 214), // RibbonMinimizeBarLight
            Color.Empty, // RibbonGroupCollapsedBorder1
            Color.Empty, // RibbonGroupCollapsedBorder2
            Color.Empty, // RibbonGroupCollapsedBorder3
            Color.Empty, // RibbonGroupCollapsedBorder4
            Color.Empty, // RibbonGroupCollapsedBack1
            Color.Empty, // RibbonGroupCollapsedBack2
            Color.Empty, // RibbonGroupCollapsedBack3
            Color.Empty, // RibbonGroupCollapsedBack4
            Color.Empty, // RibbonGroupCollapsedBorderT1
            Color.Empty, // RibbonGroupCollapsedBorderT2
            Color.Empty, // RibbonGroupCollapsedBorderT3
            Color.Empty, // RibbonGroupCollapsedBorderT4
            Color.Empty, // RibbonGroupCollapsedBackT1
            Color.FromArgb(242, 244, 247), // RibbonGroupCollapsedBackT2
            Color.FromArgb(238, 241, 245), // RibbonGroupCollapsedBackT3
            Color.FromArgb(234, 235, 235), // RibbonGroupCollapsedBackT4
            Color.FromArgb(208, 212, 217), // RibbonGroupFrameBorder1
            Color.FromArgb(208, 212, 217), // RibbonGroupFrameBorder2
            Color.FromArgb(254, 254, 254), // RibbonGroupFrameInside1
            Color.FromArgb(254, 254, 254), // RibbonGroupFrameInside2
            Color.Empty, // RibbonGroupFrameInside3
            Color.Empty, // RibbonGroupFrameInside4
            Color.FromArgb(59, 59, 59), // RibbonGroupCollapsedText         
            Color.FromArgb(179, 185, 195), // AlternatePressedBack1
            Color.FromArgb(216, 224, 224), // AlternatePressedBack2
            Color.FromArgb(125, 125, 125), // AlternatePressedBorder1
            Color.FromArgb(186, 186, 186), // AlternatePressedBorder2
            Color.FromArgb(157, 166, 174), // FormButtonBack1Checked
            Color.FromArgb(222, 230, 242), // FormButtonBack2Checked
            Color.FromArgb(149, 154, 160), // FormButtonBorderCheck
            Color.FromArgb(147, 156, 164), // FormButtonBack1CheckTrack
            Color.FromArgb(237, 245, 250), // FormButtonBack2CheckTrack
            Color.FromArgb(180, 180, 180), // RibbonQATMini1
            Color.FromArgb(210, 215, 221), // RibbonQATMini2
            Color.FromArgb(195, 200, 206), // RibbonQATMini3
            Color.FromArgb(10, Color.White), // RibbonQATMini4
            Color.FromArgb(32, Color.White), // RibbonQATMini5                                                       
            Color.FromArgb(200, 200, 200), // RibbonQATMini1I
            Color.FromArgb(233, 234, 238), // RibbonQATMini2I
            Color.FromArgb(223, 224, 228), // RibbonQATMini3I
            Color.FromArgb(10, Color.White), // RibbonQATMini4I
            Color.FromArgb(32, Color.White), // RibbonQATMini5I                                                       
            Color.FromArgb(223, 227, 234), // RibbonQATFullbar1                                                      
            Color.FromArgb(213, 217, 222), // RibbonQATFullbar2                                                      
            Color.FromArgb(135, 140, 146), // RibbonQATFullbar3                                                      
            Color.FromArgb(90, 90, 90), // RibbonQATButtonDark                                                      
            Color.FromArgb(210, 212, 215), // RibbonQATButtonLight                                                      
            Color.FromArgb(233, 237, 241), // RibbonQATOverflow1                                                      
            Color.FromArgb(138, 144, 150), // RibbonQATOverflow2                                                      
            Color.FromArgb(191, 195,
                199), // RibbonGroupSeparatorDark                                                      
            Color.FromArgb(255, 255,
                255), // RibbonGroupSeparatorLight                                                      
            Color.FromArgb(231, 234,
                238), // ButtonClusterButtonBack1                                                      
            Color.FromArgb(241, 243,
                243), // ButtonClusterButtonBack2                                                      
            Color.FromArgb(197, 198,
                199), // ButtonClusterButtonBorder1                                                      
            Color.FromArgb(157, 158,
                159), // ButtonClusterButtonBorder2                                                      
            Color.FromArgb(238, 238, 244), // NavigatorMiniBackColor                                                    
            Color.White, // GridListNormal1                                                    
            Color.White, // GridListNormal2                                                    
            Color.FromArgb(203, 207, 212), // GridListPressed1                                                    
            Color.White, // GridListPressed2                                                    
            Color.FromArgb(186, 189, 194), // GridListSelected                                                    
            Color.FromArgb(238, 241, 247), // GridSheetColNormal1                                                    
            Color.FromArgb(218, 222, 227), // GridSheetColNormal2                                                    
            Color.FromArgb(255, 223, 107), // GridSheetColPressed1                                                    
            Color.FromArgb(255, 252, 230), // GridSheetColPressed2                                                    
            Color.FromArgb(255, 211, 89), // GridSheetColSelected1
            Color.FromArgb(255, 239, 113), // GridSheetColSelected2
            Color.FromArgb(223, 227, 232), // GridSheetRowNormal                                                   
            Color.FromArgb(255, 223, 107), // GridSheetRowPressed
            Color.FromArgb(245, 210, 87), // GridSheetRowSelected
            Color.FromArgb(218, 220, 221), // GridDataCellBorder
            Color.FromArgb(183, 219, 255), // GridDataCellSelected
            Color.Black, // InputControlTextNormal
            Color.FromArgb(168, 168, 168), // InputControlTextDisabled
            Color.FromArgb(212, 214, 217), // InputControlBorderNormal
            Color.FromArgb(187, 187, 187), // InputControlBorderDisabled
            Color.FromArgb(255, 255, 255), // InputControlBackNormal
            Color.FromArgb(240, 240, 240), // InputControlBackDisabled
            Color.FromArgb(247, 247, 247), // InputControlBackInactive
            Color.Black, // InputDropDownNormal1
            Color.Transparent, // InputDropDownNormal2
            Color.FromArgb(172, 168, 153), // InputDropDownDisabled1
            Color.Transparent, // InputDropDownDisabled2
            Color.FromArgb(240, 242, 245), // ContextMenuHeadingBack
            Color.FromArgb(59, 59, 59), // ContextMenuHeadingText
            Color.White, // ContextMenuImageColumn
            Color.FromArgb(224, 227, 231), // AppButtonBack1
            Color.FromArgb(224, 227, 231), // AppButtonBack2
            Color.FromArgb(135, 140, 146), // AppButtonBorder
            Color.FromArgb(224, 227, 231), // AppButtonOuter1
            Color.FromArgb(224, 227, 231), // AppButtonOuter2
            Color.FromArgb(224, 227, 231), // AppButtonOuter3
            Color.Empty, // AppButtonInner1
            Color.FromArgb(135, 140, 146), // AppButtonInner2
            Color.White, // AppButtonMenuDocs
            Color.Black, // AppButtonMenuDocsText
            Color.FromArgb(250, 253, 255), // SeparatorHighInternalBorder1
            Color.FromArgb(227, 232, 237), // SeparatorHighInternalBorder2
            Color.FromArgb(198, 202, 205), // RibbonGalleryBorder
            Color.FromArgb(255, 255, 255), // RibbonGalleryBackNormal
            Color.FromArgb(255, 255, 255), // RibbonGalleryBackTracking
            Color.FromArgb(250, 250, 250), // RibbonGalleryBack1
            Color.FromArgb(228, 231,
                235), // RibbonGalleryBack2                                                                                                                                      Color.FromArgb(177, 181, 186),    // RibbonTabTracking1
            Color.FromArgb(229, 231, 235), // RibbonTabTracking3
            Color.FromArgb(231, 233, 235), // RibbonTabTracking4
            Color.FromArgb(176, 182, 188), // RibbonGroupBorder3
            Color.FromArgb(246, 247, 248), // RibbonGroupBorder4
            Color.FromArgb(249, 250, 250), // RibbonGroupBorder5
            Color.FromArgb(102, 109, 124), // RibbonGroupTitleText
            Color.FromArgb(151, 156, 163), // RibbonDropArrowLight
            Color.FromArgb(39, 49, 60), // RibbonDropArrowDark
            Color.FromArgb(237, 242, 248), // HeaderDockInactiveBack1
            Color.FromArgb(207, 213, 220), // HeaderDockInactiveBack2
            Color.FromArgb(161, 169, 179), // ButtonNavigatorBorder
            Color.Black, // ButtonNavigatorText
            Color.FromArgb(207, 213, 220), // ButtonNavigatorTrack1
            Color.FromArgb(232, 234, 238), // ButtonNavigatorTrack2
            Color.FromArgb(191, 196, 202), // ButtonNavigatorPressed1
            Color.FromArgb(225, 226, 230), // ButtonNavigatorPressed2
            Color.FromArgb(222, 227, 234), // ButtonNavigatorChecked1
            Color.FromArgb(206, 214, 221), // ButtonNavigatorChecked2
            Color.FromArgb(221, 221,
                221) // ToolTipBottom                                                                      
        };

        #endregion
        #endregion

        #region Identity
        static PaletteMicrosoft365White()
        {
            _checkBoxList = new ImageList
            {
                ImageSize = new Size(13, 13),
                ColorDepth = ColorDepth.Depth24Bit
            };
            _checkBoxList.Images.AddStrip(CheckBoxStripResources.CheckBoxStrip2010Silver);
            _galleryButtonList = new ImageList
            {
                ImageSize = new Size(13, 7),
                ColorDepth = ColorDepth.Depth24Bit,
                TransparentColor = Color.Magenta
            };
            _galleryButtonList.Images.AddStrip(GalleryImageResources.Gallery2010);
            _radioButtonArray = new Image[]
            {
                Office2010RadioButtonImageResources.RadioButton2010BlueD,
                Office2010RadioButtonImageResources.RadioButton2010SilverN,
                Office2010RadioButtonImageResources.RadioButton2010BlueT,
                Office2010RadioButtonImageResources.RadioButton2010BlueP,
                Office2010RadioButtonImageResources.RadioButton2010BlueDC,
                Office2010RadioButtonImageResources.RadioButton2010SilverNC,
                Office2010RadioButtonImageResources.RadioButton2010SilverTC,
                Office2010RadioButtonImageResources.RadioButton2010SilverPC
            };
        }

        /// <summary>
        /// Initialize a new instance of the PaletteMicrosoft2010Silver class.
        /// </summary>
        public PaletteMicrosoft365White()
            : base(_schemeOfficeColors,
                   _checkBoxList,
                   _galleryButtonList,
                   _radioButtonArray,
                   _trackBarColors)
        {
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
                                                 PaletteState state) => style switch
                                                 {
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
                                                     _ => base.GetButtonSpecImage(style, state)
                                                 };
        #endregion
    }
}