﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2024. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    #region Class: PaletteOffice2007BlueLightMode
    /// <summary>
    /// Provides the Blue color scheme variant of the Office 2007 palette.
    /// </summary>
    public class PaletteOffice2007BlueLightMode : PaletteOffice2007BlueLightModeBase
    {
        #region Static Fields
        private static readonly ImageList _checkBoxList;
        private static readonly ImageList _galleryButtonList;
        private static readonly Image?[] _radioButtonArray;

        #region Images

        private static readonly Image? _blueDropDownButton = GenericImageResources.BlueDropDownButton;
        private static readonly Image _blueCloseNormal = Office2007ControlBoxResources.Office2007ControlBoxBlueCloseNormal;
        private static readonly Image _blueCloseActive = Office2007ControlBoxResources.Office2007ControlBoxBlueCloseActive;
        private static readonly Image _blueCloseDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlueCloseDisabled;
        private static readonly Image _blueClosePressed = Office2007ControlBoxResources.Office2007ControlBoxBlueClosePressed;
        private static readonly Image _blueMaximiseNormal = Office2007ControlBoxResources.Office2007ControlBoxBlueMaximiseNormal;
        private static readonly Image _blueMaximiseActive = Office2007ControlBoxResources.Office2007ControlBoxBlueMaximiseActive;
        private static readonly Image _blueMaximiseDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlueMaximiseDisabled;
        private static readonly Image _blueMaximisePressed = Office2007ControlBoxResources.Office2007ControlBoxBlueMaximisePressed;
        private static readonly Image _blueMinimiseNormal = Office2007ControlBoxResources.Office2007ControlBoxBlueMinimiseNormal;
        private static readonly Image _blueMinimiseActive = Office2007ControlBoxResources.Office2007ControlBoxBlueMinimiseActive;
        private static readonly Image _blueMinimiseDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlueMinimiseDisabled;
        private static readonly Image _blueMinimisePressed = Office2007ControlBoxResources.Office2007ControlBoxBlueMinimisePessed;
        private static readonly Image _blueRestoreNormal = Office2007ControlBoxResources.Office2007ControlBoxBlueRestoreNormal;
        private static readonly Image _blueRestoreActive = Office2007ControlBoxResources.Office2007ControlBoxBlueRestoreActive;
        private static readonly Image _blueRestoreDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlueRestoreDisabled;
        private static readonly Image _blueRestorePressed = Office2007ControlBoxResources.Office2007ControlBoxBlueRestorePressed;
        private static readonly Image _blueHelpNormal = Office2007ControlBoxResources.Office2007HelpIconNormal;
        private static readonly Image _blueHelpActive = Office2007ControlBoxResources.Office2007HelpIconHover;
        private static readonly Image _blueHelpDisabled = Office2007ControlBoxResources.Office2007HelpIconDisabled;
        private static readonly Image _blueHelpPressed = Office2007ControlBoxResources.Office2007HelpIconPressed;
        private static readonly Image? _contextMenuSubMenu = GenericImageResources.BlueContextMenuSub;

        #endregion

        #region Colour Arrays

        private static readonly Color[] _trackBarColors =
        [
            Color.FromArgb(116, 150, 194), // Tick marks
            Color.FromArgb(116, 150, 194), // Top track
            Color.FromArgb(152, 190, 241), // Bottom track
            Color.FromArgb(142, 180, 231), // Fill track
            Color.FromArgb(64, Color.White), // Outside position
            Color.FromArgb(63, 101, 152) // Border (normal) position
        ];

        private static readonly Color[] _schemeOfficeColors =
        [
            Color.FromArgb(21, 66, 139), // TextLabelControl
            Color.FromArgb(21, 66, 139), // TextButtonNormal
            Color.Blue, // TextButtonChecked
            Color.FromArgb(161, 189, 207), // ButtonNormalBorder
            Color.FromArgb(121, 157, 182), // ButtonNormalDefaultBorder
            Color.FromArgb(230, 239, 249), // ButtonNormalBack1
            Color.FromArgb(209, 226, 244), // ButtonNormalBack2
            Color.FromArgb(123, 192, 232), // ButtonNormalDefaultBack1
            Color.FromArgb(177, 252, 255), // ButtonNormalDefaultBack2
            Color.FromArgb(178, 214, 255), // ButtonNormalNavigatorBack1
            Color.FromArgb(202, 229, 255), // ButtonNormalNavigatorBack2
            Color.FromArgb(230, 239, 249), // PanelClient
            Color.FromArgb(209, 226, 244), // PanelAlternative
            Color.FromArgb(101, 147, 207), // ControlBorder
            Color.FromArgb(227, 239, 255), // SeparatorHighBorder1
            Color.FromArgb(182, 214, 255), // SeparatorHighBorder2
            Color.FromArgb(230, 239, 249), // HeaderPrimaryBack1
            Color.FromArgb(209, 226, 244), // HeaderPrimaryBack2
            Color.FromArgb(214, 232, 255), // HeaderSecondaryBack1
            Color.FromArgb(214, 232, 255), // HeaderSecondaryBack2
            Color.FromArgb(21, 66, 139), // HeaderText
            Color.FromArgb(21, 66, 139), // StatusStripText
            Color.FromArgb(121, 153, 194), // ButtonBorder
            Color.FromArgb(255, 255, 255), // SeparatorLight
            Color.FromArgb(154, 198, 255), // SeparatorDark
            Color.FromArgb(248, 248, 248), // GripLight
            Color.FromArgb(114, 152, 204), // GripDark
            Color.FromArgb(209, 226, 244), // ToolStripBack
            Color.FromArgb(230, 239, 249), // StatusStripLight
            Color.FromArgb(209, 226, 244), // StatusStripDark
            Color.FromArgb(209, 226, 244), // ImageMargin
            Color.FromArgb(230, 239, 249), // ToolStripBegin
            Color.FromArgb(168, 200, 234), // ToolStripMiddle
            Color.FromArgb(147, 187, 229), // ToolStripEnd
            Color.FromArgb(167, 204, 251), // OverflowBegin
            Color.FromArgb(167, 204, 251), // OverflowMiddle
            Color.FromArgb(101, 147, 207), // OverflowEnd
            Color.FromArgb(111, 157, 217), // ToolStripBorder
            Color.FromArgb(230, 239, 249), // FormBorderActive
            Color.FromArgb(209, 226, 244), // FormBorderInactive
            Color.FromArgb(230, 239, 249), // FormBorderActiveLight
            Color.FromArgb(209, 226, 244), // FormBorderActiveDark
            Color.FromArgb(204, 216, 232), // FormBorderInactiveLight
            Color.FromArgb(212, 222, 236), // FormBorderInactiveDark
            Color.FromArgb(230, 239, 249), // FormBorderHeaderActive
            Color.FromArgb(209, 226, 244), // FormBorderHeaderInactive
            Color.FromArgb(230, 239, 249), // FormBorderHeaderActive1
            Color.FromArgb(228, 239, 253), // FormBorderHeaderActive2
            Color.FromArgb(209, 226, 244), // FormBorderHeaderInctive1
            Color.FromArgb(227, 232, 239), // FormBorderHeaderInctive2
            Color.FromArgb(62, 106, 184), // FormHeaderShortActive
            Color.FromArgb(160, 160, 160), // FormHeaderShortInactive
            Color.FromArgb(105, 112, 121), // FormHeaderLongActive
            Color.FromArgb(160, 160, 160), // FormHeaderLongInactive
            Color.FromArgb(158, 193, 241), // FormButtonBorderTrack
            Color.FromArgb(210, 228, 254), // FormButtonBack1Track
            Color.FromArgb(255, 255, 255), // FormButtonBack2Track
            Color.FromArgb(162, 191, 227), // FormButtonBorderPressed
            Color.FromArgb(132, 178, 233), // FormButtonBack1Pressed
            Color.FromArgb(192, 231, 252), // FormButtonBack2Pressed
            Color.FromArgb(21, 66, 139), // TextButtonFormNormal
            Color.FromArgb(21, 66, 139), // TextButtonFormTracking
            Color.FromArgb(21, 66, 139), // TextButtonFormPressed
            Color.Blue, // LinkNotVisitedOverrideControl
            Color.Purple, // LinkVisitedOverrideControl
            Color.Red, // LinkPressedOverrideControl
            Color.Blue, // LinkNotVisitedOverridePanel
            Color.Purple, // LinkVisitedOverridePanel
            Color.Red, // LinkPressedOverridePanel
            Color.FromArgb(21, 66, 139), // TextLabelPanel
            Color.FromArgb(21, 66, 139), // RibbonTabTextNormal
            Color.FromArgb(21, 66, 139), // RibbonTabTextChecked
            Color.FromArgb(145, 180, 228), // RibbonTabSelected1
            Color.FromArgb(209, 251, 255), // RibbonTabSelected2
            Color.FromArgb(246, 250, 255), // RibbonTabSelected3
            Color.FromArgb(239, 246, 254), // RibbonTabSelected4
            Color.FromArgb(222, 232, 245), // RibbonTabSelected5
            Color.FromArgb(153, 187, 232), // RibbonTabTracking1
            Color.FromArgb(255, 180, 86), // RibbonTabTracking2
            Color.FromArgb(255, 255, 189), // RibbonTabHighlight1
            Color.FromArgb(249, 237, 198), // RibbonTabHighlight2
            Color.FromArgb(218, 185, 127), // RibbonTabHighlight3
            Color.FromArgb(254, 209, 94), // RibbonTabHighlight4
            Color.FromArgb(205, 209, 180), // RibbonTabHighlight5
            Color.FromArgb(116, 153, 203), // RibbonTabSeparatorColor
            Color.FromArgb(219, 230, 244), // RibbonGroupsArea1
            Color.FromArgb(231, 242, 255), // RibbonGroupsArea2
            Color.FromArgb(201, 217, 237), // RibbonGroupsArea3
            Color.FromArgb(192, 249, 255), // RibbonGroupsArea4
            Color.FromArgb(141, 178, 227), // RibbonGroupsArea5
            Color.FromArgb(197, 210, 223), // RibbonGroupBorder1
            Color.FromArgb(158, 191, 219), // RibbonGroupBorder2
            Color.FromArgb(193, 216, 242), // RibbonGroupTitle1
            Color.FromArgb(193, 216, 242), // RibbonGroupTitle2
            Color.FromArgb(202, 202, 202), // RibbonGroupBorderContext1
            Color.FromArgb(196, 196, 196), // RibbonGroupBorderContext2
            Color.FromArgb(223, 223, 245), // RibbonGroupTitleContext1
            Color.FromArgb(210, 221, 242), // RibbonGroupTitleContext2
            Color.FromArgb(102, 142, 175), // RibbonGroupDialogDark
            Color.FromArgb(254, 254, 255), // RibbonGroupDialogLight
            Color.FromArgb(200, 224, 255), // RibbonGroupTitleTracking1
            Color.FromArgb(214, 237, 255), // RibbonGroupTitleTracking2
            Color.FromArgb(155, 187, 227), // RibbonMinimizeBarDark
            Color.FromArgb(213, 226, 243), // RibbonMinimizeBarLight
            Color.FromArgb(165, 191, 213), // RibbonGroupCollapsedBorder1
            Color.FromArgb(148, 185, 213), // RibbonGroupCollapsedBorder2
            Color.FromArgb(64, Color.White), // RibbonGroupCollapsedBorder3
            Color.FromArgb(202, 244, 254), // RibbonGroupCollapsedBorder4
            Color.FromArgb(221, 233, 249), // RibbonGroupCollapsedBack1
            Color.FromArgb(199, 218, 243), // RibbonGroupCollapsedBack2
            Color.FromArgb(186, 209, 240), // RibbonGroupCollapsedBack3
            Color.FromArgb(214, 238, 252), // RibbonGroupCollapsedBack4
            Color.FromArgb(186, 205, 225), // RibbonGroupCollapsedBorderT1
            Color.FromArgb(177, 230, 235), // RibbonGroupCollapsedBorderT2
            Color.FromArgb(192, Color.White), // RibbonGroupCollapsedBorderT3
            Color.FromArgb(247, 251, 254), // RibbonGroupCollapsedBorderT4
            Color.FromArgb(240, 244, 250), // RibbonGroupCollapsedBackT1
            Color.FromArgb(226, 234, 245), // RibbonGroupCollapsedBackT2
            Color.FromArgb(216, 227, 241), // RibbonGroupCollapsedBackT3
            Color.FromArgb(214, 237, 253), // RibbonGroupCollapsedBackT4
            Color.FromArgb(170, 195, 217), // RibbonGroupFrameBorder1
            Color.FromArgb(195, 217, 242), // RibbonGroupFrameBorder2
            Color.FromArgb(227, 237, 250), // RibbonGroupFrameInside1
            Color.FromArgb(221, 233, 248), // RibbonGroupFrameInside2
            Color.FromArgb(214, 228, 246), // RibbonGroupFrameInside3
            Color.FromArgb(227, 236, 248), // RibbonGroupFrameInside4
            Color.FromArgb(21, 66, 139), // RibbonGroupCollapsedText
            Color.FromArgb(118, 153, 200), // AlternatePressedBack1
            Color.FromArgb(184, 215, 253), // AlternatePressedBack2
            Color.FromArgb(135, 156, 175), // AlternatePressedBorder1
            Color.FromArgb(177, 198, 216), // AlternatePressedBorder2
            Color.FromArgb(150, 194, 239), // FormButtonBack1Checked
            Color.FromArgb(210, 228, 254), // FormButtonBack2Checked
            Color.FromArgb(158, 193, 241), // FormButtonBorderCheck
            Color.FromArgb(140, 184, 229), // FormButtonBack1CheckTrack
            Color.FromArgb(225, 241, 255), // FormButtonBack2CheckTrack
            Color.FromArgb(154, 179, 213), // RibbonQATMini1
            Color.FromArgb(219, 231, 247), // RibbonQATMini2
            Color.FromArgb(195, 213, 236), // RibbonQATMini3
            Color.FromArgb(128, Color.White), // RibbonQATMini4
            Color.FromArgb(72, Color.White), // RibbonQATMini5                                                       
            Color.FromArgb(153, 176, 206), // RibbonQATMini1I
            Color.FromArgb(226, 233, 241), // RibbonQATMini2I
            Color.FromArgb(198, 210, 226), // RibbonQATMini3I
            Color.FromArgb(128, Color.White), // RibbonQATMini4I
            Color.FromArgb(72, Color.White), // RibbonQATMini5I                                                      
            Color.FromArgb(178, 205, 237), // RibbonQATFullbar1                                                      
            Color.FromArgb(170, 197, 234), // RibbonQATFullbar2                                                      
            Color.FromArgb(126, 161, 205), // RibbonQATFullbar3                                                      
            Color.FromArgb(86, 125, 177), // RibbonQATButtonDark                                                      
            Color.FromArgb(234, 242, 249), // RibbonQATButtonLight                                                      
            Color.FromArgb(192, 220, 255), // RibbonQATOverflow1                                                      
            Color.FromArgb(55, 100, 160), // RibbonQATOverflow2                                                      
            Color.FromArgb(140, 172,
                211), // RibbonGroupSeparatorDark                                                      
            Color.FromArgb(248, 250,
                252), // RibbonGroupSeparatorLight                                                      
            Color.FromArgb(192, 212,
                241), // ButtonClusterButtonBack1                                                      
            Color.FromArgb(200, 219,
                238), // ButtonClusterButtonBack2                                                      
            Color.FromArgb(155, 183,
                224), // ButtonClusterButtonBorder1                                                      
            Color.FromArgb(117, 150,
                191), // ButtonClusterButtonBorder2                                                      
            Color.FromArgb(213, 228, 242), // NavigatorMiniBackColor                                                    
            Color.FromArgb(230, 239, 249), // GridListNormal1                                                    
            Color.FromArgb(209, 226, 244), // GridListNormal2                                                    
            Color.FromArgb(209, 226, 244), // GridListPressed1                                                    
            Color.FromArgb(252, 253, 255), // GridListPressed2                                                    
            Color.FromArgb(168, 200, 234), // GridListSelected                                                    
            Color.FromArgb(230, 239, 249), // GridSheetColNormal1                                                    
            Color.FromArgb(209, 226, 244), // GridSheetColNormal2                                                    
            Color.FromArgb(223, 226, 228), // GridSheetColPressed1                                                    
            Color.FromArgb(188, 197, 210), // GridSheetColPressed2                                                    
            Color.FromArgb(188, 213, 239), // GridSheetColSelected1
            Color.FromArgb(168, 200, 234), // GridSheetColSelected2
            Color.FromArgb(228, 236, 247), // GridSheetRowNormal                                                   
            Color.FromArgb(187, 196, 209), // GridSheetRowPressed
            Color.FromArgb(255, 213, 141), // GridSheetRowSelected
            Color.FromArgb(188, 195, 209), // GridDataCellBorder
            Color.FromArgb(188, 213, 239), // GridDataCellSelected
            Color.Blue, // InputControlTextNormal
            Color.FromArgb(77, 77, 77), // InputControlTextDisabled
            Color.FromArgb(171, 193, 222), // InputControlBorderNormal
            Color.FromArgb(177, 187, 198), // InputControlBorderDisabled
            Color.FromArgb(230, 239, 249), // InputControlBackNormal
            SystemColors.Control, // InputControlBackDisabled
            Color.FromArgb(234, 242, 251), // InputControlBackInactive
            Color.FromArgb(188, 213, 239), // InputDropDownNormal1
            Color.FromArgb(255, 248, 203), // InputDropDownNormal2
            Color.FromArgb(77, 77, 77), // InputDropDownDisabled1
            Color.Transparent, // InputDropDownDisabled2
            Color.FromArgb(209, 226, 244), // ContextMenuHeadingBack
            Color.FromArgb(0, 21, 110), // ContextMenuHeadingText
            Color.FromArgb(209, 226, 244), // ContextMenuImageColumn
            Color.FromArgb(230, 239, 249), // AppButtonBack1
            Color.FromArgb(209, 226, 244), // AppButtonBack2
            Color.FromArgb(155, 175, 202), // AppButtonBorder
            Color.FromArgb(189, 211, 238), // AppButtonOuter1
            Color.FromArgb(176, 201, 234), // AppButtonOuter2
            Color.FromArgb(207, 224, 245), // AppButtonOuter3
            Color.White, // AppButtonInner1
            Color.FromArgb(155, 175, 202), // AppButtonInner2
            Color.FromArgb(209, 226, 244), // AppButtonMenuDocs
            Color.FromArgb(0, 21, 110), // AppButtonMenuDocsText
            Color.FromArgb(227, 239, 255), // SeparatorHighInternalBorder1
            Color.FromArgb(182, 214, 255), // SeparatorHighInternalBorder2
            Color.FromArgb(185, 208, 237), // RibbonGalleryBorder
            Color.FromArgb(212, 230, 248), // RibbonGalleryBackNormal
            Color.FromArgb(236, 243, 251), // RibbonGalleryBackTracking
            Color.FromArgb(193, 213, 241), // RibbonGalleryBack1
            Color.FromArgb(215, 233, 251), // RibbonGalleryBack2
            GlobalStaticValues.EMPTY_COLOR, // RibbonTabTracking3
            GlobalStaticValues.EMPTY_COLOR, // RibbonTabTracking4
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupBorder3
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupBorder4
            GlobalStaticValues.EMPTY_COLOR, // RibbonDropArrowLight
            GlobalStaticValues.EMPTY_COLOR // RibbonDropArrowDark
        ];

        #endregion

        #endregion

        #region Identity
        static PaletteOffice2007BlueLightMode()
        {
            _checkBoxList = new ImageList
            {
                ImageSize = new Size(13, 13),
                ColorDepth = ColorDepth.Depth24Bit
            };
            _checkBoxList.Images.AddStrip(CheckBoxStripResources.CheckBoxStrip2007Blue);
            _galleryButtonList = new ImageList
            {
                ImageSize = new Size(13, 7),
                ColorDepth = ColorDepth.Depth24Bit,
                TransparentColor = Color.Magenta
            };
            _galleryButtonList.Images.AddStrip(GalleryImageResources.GalleryBlue);
            _radioButtonArray =
            [
                Office2007RadioButtonImageResources.RadioButton2007BlueD,
                Office2007RadioButtonImageResources.RadioButton2007BlueN,
                Office2007RadioButtonImageResources.RadioButton2007BlueT,
                Office2007RadioButtonImageResources.RadioButton2007BlueP,
                Office2007RadioButtonImageResources.RadioButton2007BlueDC,
                Office2007RadioButtonImageResources.RadioButton2007BlueNC,
                Office2007RadioButtonImageResources.RadioButton2007BlueTC,
                Office2007RadioButtonImageResources.RadioButton2007BluePC
            ];
        }

        /// <summary>
        /// Initialize a new instance of the PaletteOffice2007BlueLightMode class.
        /// </summary>
        public PaletteOffice2007BlueLightMode()
            : base(_schemeOfficeColors,
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
        public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state) => style switch
        {
            PaletteBackStyle.HeaderForm => PaletteColorStyle.Rounding4,
            _ => base.GetBackColorStyle(style, state)
        };
        #endregion

        #region Images
        /// <summary>
        /// Gets a drop down button image appropriate for the provided state.
        /// </summary>
        /// <param name="state">PaletteState for which image is required.</param>
        public override Image? GetDropDownButtonImage(PaletteState state) => state != PaletteState.Disabled ? _blueDropDownButton : base.GetDropDownButtonImage(state);

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
                                                         PaletteState.Disabled => _blueCloseDisabled,
                                                         PaletteState.Tracking => _blueCloseActive,
                                                         PaletteState.Pressed => _blueClosePressed,
                                                         _ => _blueCloseNormal
                                                     },
                                                     PaletteButtonSpecStyle.FormMin => state switch
                                                     {
                                                         PaletteState.Disabled => _blueMinimiseDisabled,
                                                         PaletteState.Tracking => _blueMinimiseActive,
                                                         PaletteState.Pressed => _blueMinimisePressed,
                                                         _ => _blueMinimiseNormal
                                                     },
                                                     PaletteButtonSpecStyle.FormMax => state switch
                                                     {
                                                         PaletteState.Disabled => _blueMaximiseDisabled,
                                                         PaletteState.Tracking => _blueMaximiseActive,
                                                         PaletteState.Pressed => _blueMaximisePressed,
                                                         _ => _blueMaximiseNormal
                                                     },
                                                     PaletteButtonSpecStyle.FormRestore => state switch
                                                     {
                                                         PaletteState.Disabled => _blueRestoreDisabled,
                                                         PaletteState.Tracking => _blueRestoreActive,
                                                         PaletteState.Pressed => _blueRestorePressed,
                                                         _ => _blueRestoreNormal
                                                     },
                                                     PaletteButtonSpecStyle.FormHelp => state switch
                                                     {
                                                         PaletteState.Disabled => _blueHelpDisabled,
                                                         PaletteState.Tracking => _blueHelpActive,
                                                         PaletteState.Pressed => _blueHelpPressed,
                                                         _ => _blueHelpNormal
                                                     },
                                                     _ => base.GetButtonSpecImage(style, state)
                                                 };
        #endregion

        #region RibbonBack
        /// <summary>
        /// Gets the method used to draw the background of a ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteRibbonBackStyle value.</returns>
        public override PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteRibbonBackStyle style, PaletteState state)
        {
            switch (style)
            {
                case PaletteRibbonBackStyle.RibbonGroupArea:
                    if (state == PaletteState.CheckedNormal)
                    {
                        return PaletteRibbonColorStyle.RibbonGroupAreaBorder;
                    }

                    break;
                case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
                    if (state == PaletteState.Tracking)
                    {
                        return PaletteRibbonColorStyle.RibbonGroupNormalBorderTrackingLight;
                    }

                    break;
            }

            return base.GetRibbonBackColorStyle(style, state);
        }
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
        public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

        /// <inheritdoc />
        public override float GetRibbonTabRowGradientRaftingAngle(PaletteState state) => -1;

        #endregion

        #region AppButton Colors

        /// <inheritdoc />
        public override Color GetRibbonFileAppTabBottomColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

        /// <inheritdoc />
        public override Color GetRibbonFileAppTabTopColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

        /// <inheritdoc />
        public override Color GetRibbonFileAppTabTextColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

        #endregion
    }
    #endregion

    #region Class: PaletteOffice2007BlueLightModeBase
    /// <summary>
    /// Provides a professional appearance using colors/fonts generated from system settings.
    /// </summary>
    public abstract class PaletteOffice2007BlueLightModeBase : PaletteBase
    {
        #region Static Fields

        #region Padding

        private static readonly Padding _contentPaddingGrid = new Padding(2, 1, 2, 1);
        private static readonly Padding _contentPaddingHeader1 = new Padding(2, 1, 2, 1);
        private static readonly Padding _contentPaddingHeader2 = new Padding(2, 1, 2, 1);
        private static readonly Padding _contentPaddingDock = new Padding(2, 2, 2, 1);
        private static readonly Padding _contentPaddingCalendar = new Padding(2);
        private static readonly Padding _contentPaddingHeaderForm = new Padding(10, 6, 3, 0); // 10 is from the RealWindowFrameSize +1
        private static readonly Padding _contentPaddingLabel = new Padding(3, 1, 3, 1);
        private static readonly Padding _contentPaddingLabel2 = new Padding(8, 2, 8, 2);
        private static readonly Padding _contentPaddingButtonCalendar = new Padding(-1);
        private static readonly Padding _contentPaddingButtonInputControl = new Padding(0);
        private static readonly Padding _contentPaddingButton12 = new Padding(1);
        private static readonly Padding _contentPaddingButton3 = new Padding(1, 0, 1, 0);
        private static readonly Padding _contentPaddingButton4 = new Padding(3, 2, 3, 2);
        private static readonly Padding _contentPaddingButton5 = new Padding(3, 3, 3, 2);
        private static readonly Padding _contentPaddingButton6 = new Padding(3);
        private static readonly Padding _contentPaddingButton7 = new Padding(1, 1, 0, 1);
        private static readonly Padding _contentPaddingButtonForm = new Padding(0);
        private static readonly Padding _contentPaddingButtonGallery = new Padding(1, 0, 1, 0);
        private static readonly Padding _contentPaddingButtonListItem = new Padding(0, -1, 0, -1);
        private static readonly Padding _contentPaddingToolTip = new Padding(2);
        private static readonly Padding _contentPaddingSuperTip = new Padding(4);
        private static readonly Padding _contentPaddingKeyTip = new Padding(0, -1, 0, -3);
        private static readonly Padding _contentPaddingContextMenuHeading = new Padding(8, 2, 8, 0);
        private static readonly Padding _contentPaddingContextMenuImage = new Padding(0);
        private static readonly Padding _contentPaddingContextMenuItemText = new Padding(9, 1, 7, 0);
        private static readonly Padding _contentPaddingContextMenuItemTextAlt = new Padding(7, 1, 6, 0);
        private static readonly Padding _contentPaddingContextMenuItemShortcutText = new Padding(3, 1, 4, 0);
        private static readonly Padding _metricPaddingRibbon = new Padding(0, 1, 1, 1);
        private static readonly Padding _metricPaddingRibbonAppButton = new Padding(3, 0, 3, 0);
        private static readonly Padding _metricPaddingHeader = new Padding(0, 3, 1, 3);
        private static readonly Padding _metricPaddingHeaderForm = new Padding(0, 3, 0, -3); // Move the Maximised Form buttons down a bit
        private static readonly Padding _metricPaddingInputControl = new Padding(0, 1, 0, 1);
        private static readonly Padding _metricPaddingBarInside = new Padding(3);
        private static readonly Padding _metricPaddingBarTabs = new Padding(0);
        private static readonly Padding _metricPaddingBarOutside = new Padding(0, 0, 0, 3);
        private static readonly Padding _metricPaddingPageButtons = new Padding(1, 3, 1, 3);

        #endregion

        #region Images

        private static readonly Image? _treePlus = TreeItemImageResources.TreePlusBox;
        private static readonly Image? _treeMinus = TreeItemImageResources.TreeMinusBox;

        private static readonly Image? _disabledDropDown = DropDownArrowImageResources.DisabledDropDownButton;
        private static readonly Image _buttonSpecClose = ProfessionalButtonSpecResources.ProfessionalCloseButton;
        private static readonly Image _buttonSpecContext = GenericProfessionalImageResources.ProfessionalContextButton;
        private static readonly Image _buttonSpecNext = GenericProfessionalImageResources.ProfessionalNextButton;
        private static readonly Image _buttonSpecPrevious = GenericProfessionalImageResources.ProfessionalPreviousButton;
        private static readonly Image _buttonSpecArrowLeft = GenericProfessionalImageResources.ProfessionalArrowLeftButton;
        private static readonly Image _buttonSpecArrowRight = GenericProfessionalImageResources.ProfessionalArrowRightButton;
        private static readonly Image _buttonSpecArrowUp = GenericProfessionalImageResources.ProfessionalArrowUpButton;
        private static readonly Image _buttonSpecArrowDown = GenericProfessionalImageResources.ProfessionalArrowDownButton;
        private static readonly Image _buttonSpecDropDown = GenericProfessionalImageResources.ProfessionalDropDownButton;
        private static readonly Image _buttonSpecPinVertical = ProfessionalPinImageResources.ProfessionalPinVerticalButton;
        private static readonly Image _buttonSpecPinHorizontal = ProfessionalPinImageResources.ProfessionalPinHorizontalButton;
        private static readonly Image _buttonSpecPendantClose = GenericMDIImageResources.MdiClose;
        private static readonly Image _buttonSpecPendantMin = GenericMDIImageResources.MdiMin;
        private static readonly Image _buttonSpecPendantRestore = GenericMDIImageResources.MdiRestore;
        private static readonly Image _buttonSpecWorkspaceMaximize = ProfessionalControlBoxResources.ProfessionalMaximize;
        private static readonly Image _buttonSpecWorkspaceRestore = GenericProfessionalImageResources.ProfessionalRestore;
        private static readonly Image _buttonSpecRibbonMinimize = GenericMDIImageResources.MdiRibbonMinimize;
        private static readonly Image _buttonSpecRibbonExpand = GenericMDIImageResources.MdiRibbonExpand;
        private static readonly Image? _contextMenuChecked = GenericOffice2007ImageResources.Office2007Checked;
        private static readonly Image? _contextMenuIndeterminate = GenericOffice2007ImageResources.Office2007Indeterminate;

        #region Integrated Tool Bar Images

        private static readonly Image _integratedToolbarNewNormal = Office2007ToolbarImageResources.Office2007ToolbarNewNormal;

        private static readonly Image _integratedToolbarOpenNormal = Office2007ToolbarImageResources.Office2007ToolbarOpenNormal;

        private static readonly Image _integratedToolbarSaveAllNormal = Office2007ToolbarImageResources.Office2007ToolbarSaveAllNormal;

        private static readonly Image _integratedToolbarSaveAsNormal = Office2007ToolbarImageResources.Office2007ToolbarSaveAsNormal;

        private static readonly Image _integratedToolbarSaveNormal = Office2007ToolbarImageResources.Office2007ToolbarSaveNormal;

        private static readonly Image _integratedToolbarCutNormal = Office2007ToolbarImageResources.Office2007ToolbarCutNormal;

        private static readonly Image _integratedToolbarCopyNormal = Office2007ToolbarImageResources.Office2007ToolbarCopyNormal;

        private static readonly Image _integratedToolbarPasteNormal = Office2007ToolbarImageResources.Office2007ToolbarPasteNormal;

        private static readonly Image _integratedToolbarUndoNormal = Office2007ToolbarImageResources.Office2007ToolbarUndoNormal;

        private static readonly Image _integratedToolbarRedoNormal = Office2007ToolbarImageResources.Office2007ToolbarRedoNormal;

        private static readonly Image _integratedToolbarPageSetupNormal = Office2007ToolbarImageResources.Office2007ToolbarPageSetupNormal;

        private static readonly Image _integratedToolbarPrintPreviewNormal = Office2007ToolbarImageResources.Office2007ToolbarPrintPreviewNormal;

        private static readonly Image _integratedToolbarPrintNormal = Office2007ToolbarImageResources.Office2007ToolbarPrintNormal;

        private static readonly Image _integratedToolbarQuickPrintNormal = Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal;

        #endregion

        #endregion

        #region Colours

        private static readonly Color _gridTextColor = Color.Black;
        private static readonly Color _colorWhite192 = Color.FromArgb(192, 192, 192);
        private static readonly Color _lightGray = Color.FromArgb(242, 242, 242);
        private static readonly Color _disabledText2 = Color.FromArgb(77, 77, 77);
        private static readonly Color _disabledText = Color.FromArgb(115, 115, 115);
        private static readonly Color _disabledBack = Color.FromArgb(235, 235, 235);
        private static readonly Color _disabledBack2 = Color.FromArgb(240, 240, 240);
        private static readonly Color _disabledBorder = Color.FromArgb(212, 212, 212);
        private static readonly Color _disabledGlyphDark = Color.FromArgb(183, 183, 183);
        private static readonly Color _disabledGlyphLight = Color.FromArgb(237, 237, 237);
        private static readonly Color _contextCheckedTabBorder = Color.FromArgb(194, 194, 194);
        private static readonly Color _contextCheckedTabFill = Color.FromArgb(238, 181, 30);
        private static readonly Color _contextGroupAreaBorder = Color.FromArgb(194, 194, 194);
        private static readonly Color _contextGroupAreaInside = Color.FromArgb(254, 254, 254);
        private static readonly Color _contextGroupFrameTop = Color.FromArgb(200, 249, 249, 249);
        private static readonly Color _contextGroupFrameBottom = Color.FromArgb(249, 249, 249);
        private static readonly Color _contextTabSeparator = Color.FromArgb(32, Color.Black);
        private static readonly Color _todayBorder = Color.FromArgb(187, 85, 3);
        private static readonly Color _focusTabFill = Color.FromArgb(248, 201, 90);
        private static readonly Color _toolTipBack1 = Color.FromArgb(255, 255, 255);
        private static readonly Color _toolTipBack2 = Color.FromArgb(201, 217, 239);
        private static readonly Color _toolTipBorder = Color.FromArgb(118, 118, 118);
        private static readonly Color _toolTipText = Color.FromArgb(76, 76, 76);
        private static readonly Color _contextMenuInnerBack = Color.FromArgb(230, 239, 249);
        private static readonly Color _contextMenuOuterBack = Color.FromArgb(209, 226, 244);
        private static readonly Color _contextMenuBorder = Color.FromArgb(134, 134, 134);
        private static readonly Color _contextMenuHeadingBorder = Color.FromArgb(197, 197, 197);
        private static readonly Color _contextMenuImageBackChecked = Color.FromArgb(255, 227, 149);
        private static readonly Color _contextMenuImageBorderChecked = Color.FromArgb(242, 149, 54);

        #endregion

        #region Colour Arrays

        private static readonly Color[] _ribbonGroupCollapsedBackContext = [Color.FromArgb(48, 255, 255, 255), Color.FromArgb(235, 235, 235)
        ];
        private static readonly Color[] _ribbonGroupCollapsedBackContextTracking = [Color.FromArgb(48, 255, 255, 255), Color.FromArgb(235, 235, 235)
        ];
        private static readonly Color[] _ribbonGroupCollapsedBorderContext = [Color.FromArgb(128, 199, 199, 199), Color.FromArgb(199, 199, 199), Color.FromArgb(48, 255, 255, 255), Color.FromArgb(235, 235, 235)
        ];
        private static readonly Color[] _ribbonGroupCollapsedBorderContextTracking = [Color.FromArgb(128, 168, 184, 196), Color.FromArgb(168, 184, 196), Color.FromArgb(48, 255, 255, 255), Color.FromArgb(192, 207, 220)
        ];
        private static readonly Color[] _appButtonNormal = [Color.FromArgb(243, 245, 248), Color.FromArgb(214, 220, 231), Color.FromArgb(188, 198, 211), Color.FromArgb(254, 254, 255), Color.FromArgb(206, 213, 225)
        ];
        private static readonly Color[] _appButtonTrack = [Color.FromArgb(255, 251, 230), Color.FromArgb(248, 230, 143), Color.FromArgb(238, 213, 126), Color.FromArgb(254, 247, 129), Color.FromArgb(240, 201, 41)
        ];
        private static readonly Color[] _appButtonPressed = [Color.FromArgb(235, 227, 196), Color.FromArgb(228, 198, 149), Color.FromArgb(166, 97, 7), Color.FromArgb(242, 155, 57), Color.FromArgb(236, 136, 9)
        ];
        private static readonly Color[] _buttonBorderColors =
        [
            Color.FromArgb(212, 212, 212), // Button, Disabled, Border
                                                                Color.FromArgb(168, 200, 234), // Button, Tracking, Border 1
                                                                Color.FromArgb(170, 210, 245), // Button, Tracking, Border 2
                                                                Color.FromArgb(155, 205, 230), // Button, Pressed, Border 1
                                                                Color.FromArgb(150, 200, 225), // Button, Pressed, Border 2
                                                                Color.FromArgb(148, 197,  220), // Button, Checked, Border 1
                                                                Color.FromArgb(160, 205, 240)  // Button, Checked, Border 2
        ];
        private static readonly Color[] _buttonBackColors =
        [
            Color.FromArgb(221, 221, 221), // Button, Disabled, Back 1
                                                                Color.FromArgb(236, 236, 236), // Button, Disabled, Back 2
                                                                Color.FromArgb(188, 213, 239), // Button, Tracking, Back 1
                                                                Color.FromArgb(168, 200, 234), // Button, Tracking, Back 2
                                                                Color.FromArgb(209, 226, 244), // Button, Pressed, Back 1
                                                                Color.FromArgb(188, 213, 239), // Button, Pressed, Back 2
                                                                Color.FromArgb(189, 213, 233), // Button, Checked, Back 1
                                                                Color.FromArgb(188, 213, 239), // Button, Checked, Back 2
                                                                Color.FromArgb(209, 226, 244), // Button, Checked Tracking, Back 1
                                                                Color.FromArgb(210, 226, 244)  // Button, Checked Tracking, Back 2
        ];

        #endregion

        #endregion

        #region Instance Fields
        private KryptonColorTable2007BlueLightMode? _table;
        private readonly Color[] _ribbonColours;
        private readonly Color[] _trackBarColours;
        private readonly ImageList _checkBoxList;
        private readonly ImageList _galleryButtonList;
        private readonly Image?[] _radioButtonArray;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteOffice2007BlueLightModeBase class.
        /// </summary>
        /// <param name="schemeColors">Array of palette specific colors.</param>
        /// <param name="checkBoxList">List of images for check box.</param>
        /// <param name="galleryButtonList">List of images for gallery buttons.</param>
        /// <param name="radioButtonArray">Array of images for radio button.</param>
        /// <param name="trackBarColors">Array of track bar specific colors.</param>
        protected PaletteOffice2007BlueLightModeBase([DisallowNull] Color[] schemeColors,
                                     [DisallowNull] ImageList checkBoxList,
                                     [DisallowNull] ImageList galleryButtonList,
                                     [DisallowNull] Image?[] radioButtonArray,
                                     Color[] trackBarColors)
        {
            Debug.Assert(schemeColors != null);
            Debug.Assert(checkBoxList != null);
            Debug.Assert(galleryButtonList != null);
            Debug.Assert(radioButtonArray != null);

            // Remember incoming sets of values
            ThemeName = nameof(PaletteOffice2007BlueLightModeBase);

            if (schemeColors != null)
            {
                _ribbonColours = schemeColors;
            }
            if (checkBoxList != null)
            {
                _checkBoxList = checkBoxList;
            }
            if (galleryButtonList != null)
            {
                _galleryButtonList = galleryButtonList;
            }
            if (radioButtonArray != null)
            {
                _radioButtonArray = radioButtonArray;
            }
            if (trackBarColors != null)
            {
                _trackBarColours = trackBarColors;
            }

            // Get the font settings from the system
            DefineFonts();
        }
        #endregion

        #region Renderer
        /// <summary>
        /// Gets the renderer to use for this palette.
        /// </summary>
        /// <returns>Renderer to use for drawing palette settings.</returns>
        public override IRenderer GetRenderer() =>
            // We always want the professional renderer
            KryptonManager.RenderOffice2007;

        #endregion

        #region Back
        /// <summary>
        /// Gets a value indicating if background should be drawn.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetBackDraw(PaletteBackStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return InheritBool.Inherit;
            }

            return style switch
            {
                PaletteBackStyle.SeparatorLowProfile or PaletteBackStyle.SeparatorCustom1 or PaletteBackStyle.SeparatorCustom2 or PaletteBackStyle.SeparatorCustom3 => InheritBool.False,
                PaletteBackStyle.ButtonLowProfile or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonCalendarDay or PaletteBackStyle.ButtonNavigatorOverflow or PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose => state switch
                {
                    PaletteState.Disabled or PaletteState.Normal or PaletteState.NormalDefaultOverride => InheritBool.False,
                    _ => InheritBool.True
                },
                PaletteBackStyle.ContextMenuItemImage or PaletteBackStyle.ContextMenuItemHighlight => state switch
                {
                    PaletteState.Normal or PaletteState.NormalDefaultOverride => InheritBool.False,
                    _ => InheritBool.True
                },
                PaletteBackStyle.ButtonInputControl => state is PaletteState.Disabled or PaletteState.Normal ? InheritBool.False : InheritBool.True,
                _ => InheritBool.True // Default to drawing the background
            };
        }

        /// <summary>
        /// Gets the graphics drawing hint for the background.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>PaletteGraphicsHint value.</returns>
        public override PaletteGraphicsHint GetBackGraphicsHint(PaletteBackStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteGraphicsHint.Inherit;
            }

            return style switch
            {
                PaletteBackStyle.TabHighProfile
                    or PaletteBackStyle.TabStandardProfile
                    or PaletteBackStyle.TabLowProfile
                    or PaletteBackStyle.TabOneNote
                    or PaletteBackStyle.TabDock
                    or PaletteBackStyle.TabDockAutoHidden
                    or PaletteBackStyle.TabCustom1
                    or PaletteBackStyle.TabCustom2
                    or PaletteBackStyle.TabCustom3
                    or PaletteBackStyle.PanelClient
                    or PaletteBackStyle.PanelRibbonInactive
                    or PaletteBackStyle.PanelAlternate
                    or PaletteBackStyle.PanelCustom1
                    or PaletteBackStyle.PanelCustom2
                    or PaletteBackStyle.PanelCustom3
                    or PaletteBackStyle.SeparatorHighInternalProfile
                    or PaletteBackStyle.SeparatorHighProfile
                    or PaletteBackStyle.SeparatorLowProfile
                    or PaletteBackStyle.SeparatorCustom1
                    or PaletteBackStyle.SeparatorCustom2
                    or PaletteBackStyle.SeparatorCustom3
                    or PaletteBackStyle.ControlClient
                    or PaletteBackStyle.ControlAlternate
                    or PaletteBackStyle.ControlGroupBox
                    or PaletteBackStyle.ControlToolTip
                    or PaletteBackStyle.ControlRibbon
                    or PaletteBackStyle.ControlRibbonAppMenu
                    or PaletteBackStyle.ControlCustom1
                    or PaletteBackStyle.ControlCustom2
                    or PaletteBackStyle.ControlCustom3
                    or PaletteBackStyle.ContextMenuOuter
                    or PaletteBackStyle.ContextMenuInner
                    or PaletteBackStyle.ContextMenuHeading
                    or PaletteBackStyle.ContextMenuSeparator
                    or PaletteBackStyle.ContextMenuItemSplit
                    or PaletteBackStyle.ContextMenuItemImageColumn
                    or PaletteBackStyle.ContextMenuItemImage
                    or PaletteBackStyle.ContextMenuItemHighlight
                    or PaletteBackStyle.InputControlStandalone
                    or PaletteBackStyle.InputControlRibbon
                    or PaletteBackStyle.InputControlCustom1
                    or PaletteBackStyle.InputControlCustom2
                    or PaletteBackStyle.InputControlCustom3
                    or PaletteBackStyle.FormMain
                    or PaletteBackStyle.FormCustom1
                    or PaletteBackStyle.FormCustom2
                    or PaletteBackStyle.FormCustom3
                    or PaletteBackStyle.HeaderPrimary
                    or PaletteBackStyle.HeaderDockInactive
                    or PaletteBackStyle.HeaderDockActive
                    or PaletteBackStyle.HeaderSecondary
                    or PaletteBackStyle.HeaderForm
                    or PaletteBackStyle.HeaderCalendar
                    or PaletteBackStyle.HeaderCustom1
                    or PaletteBackStyle.HeaderCustom2
                    or PaletteBackStyle.HeaderCustom3
                    or PaletteBackStyle.ButtonStandalone
                    or PaletteBackStyle.ButtonGallery
                    or PaletteBackStyle.ButtonAlternate
                    or PaletteBackStyle.ButtonLowProfile
                    or PaletteBackStyle.ButtonBreadCrumb
                    or PaletteBackStyle.ButtonListItem
                    or PaletteBackStyle.ButtonCommand
                    or PaletteBackStyle.ButtonCalendarDay
                    or PaletteBackStyle.ButtonButtonSpec
                    or PaletteBackStyle.ButtonCluster
                    or PaletteBackStyle.ButtonNavigatorStack
                    or PaletteBackStyle.ButtonNavigatorOverflow
                    or PaletteBackStyle.ButtonNavigatorMini
                    or PaletteBackStyle.ButtonForm
                    or PaletteBackStyle.ButtonFormClose
                    or PaletteBackStyle.ButtonCustom1
                    or PaletteBackStyle.ButtonCustom2
                    or PaletteBackStyle.ButtonCustom3
                    or PaletteBackStyle.ButtonInputControl
                    or PaletteBackStyle.GridBackgroundList
                    or PaletteBackStyle.GridBackgroundSheet
                                        or PaletteBackStyle.GridBackgroundCustom1
                    or PaletteBackStyle.GridBackgroundCustom2
                    or PaletteBackStyle.GridBackgroundCustom3

                    or PaletteBackStyle.GridBackgroundCustom2
                    or PaletteBackStyle.GridBackgroundCustom3
                    or PaletteBackStyle.GridHeaderColumnList
                    or PaletteBackStyle.GridHeaderColumnSheet
                    or PaletteBackStyle.GridHeaderColumnCustom1
                    or PaletteBackStyle.GridHeaderColumnCustom2
                    or PaletteBackStyle.GridHeaderColumnCustom3
                    or PaletteBackStyle.GridHeaderRowList
                    or PaletteBackStyle.GridHeaderRowSheet
                    or PaletteBackStyle.GridHeaderRowCustom1
                    or PaletteBackStyle.GridHeaderRowCustom2
                    or PaletteBackStyle.GridHeaderRowCustom3
                    or PaletteBackStyle.GridDataCellList
                    or PaletteBackStyle.GridDataCellSheet
                    or PaletteBackStyle.GridDataCellCustom1
                    or PaletteBackStyle.GridDataCellCustom2
                    or PaletteBackStyle.GridDataCellCustom3 => PaletteGraphicsHint.None,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the first background color.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Color value.</returns>
        public override Color GetBackColor1(PaletteBackStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
            {
                return GlobalStaticValues.EMPTY_COLOR;
            }

            switch (style)
            {
                case PaletteBackStyle.GridHeaderColumnList:
                case PaletteBackStyle.GridHeaderColumnCustom1:
                case PaletteBackStyle.GridHeaderColumnCustom2:
                case PaletteBackStyle.GridHeaderColumnCustom3:
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowCustom1:
                case PaletteBackStyle.GridHeaderRowCustom2:
                case PaletteBackStyle.GridHeaderRowCustom3:
                    return state switch
                    {
                        PaletteState.Disabled => _disabledBack,
                        PaletteState.Pressed => _ribbonColours[(int)SchemeOfficeColors.GridListPressed1],
                        PaletteState.CheckedNormal => _ribbonColours[(int)SchemeOfficeColors.GridListSelected],
                        _ => _ribbonColours[(int)SchemeOfficeColors.GridListNormal1]
                    };
                case PaletteBackStyle.GridHeaderColumnSheet:
                    return state switch
                    {
                        PaletteState.Disabled => _disabledBack,
                        PaletteState.Tracking or PaletteState.Pressed => _ribbonColours[(int)SchemeOfficeColors.GridSheetColPressed1],
                        PaletteState.CheckedNormal => _ribbonColours[(int)SchemeOfficeColors.GridSheetColSelected1],
                        _ => _ribbonColours[(int)SchemeOfficeColors.GridSheetColNormal1]
                    };
                case PaletteBackStyle.GridHeaderRowSheet:
                    return state switch
                    {
                        PaletteState.Disabled => _disabledBack,
                        PaletteState.Tracking or PaletteState.Pressed => _ribbonColours[(int)SchemeOfficeColors.GridSheetRowPressed],
                        PaletteState.CheckedNormal => _ribbonColours[(int)SchemeOfficeColors.GridSheetRowSelected],
                        _ => _ribbonColours[(int)SchemeOfficeColors.GridSheetRowNormal]
                    };
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellCustom1:
                case PaletteBackStyle.GridDataCellCustom2:
                case PaletteBackStyle.GridDataCellCustom3:
                    return state == PaletteState.CheckedNormal ? _ribbonColours[(int)SchemeOfficeColors.GridDataCellSelected] : _ribbonColours[(int)SchemeOfficeColors.PanelAlternative];

                case PaletteBackStyle.GridDataCellSheet:
                    return state == PaletteState.CheckedNormal ? _buttonBackColors[6] : _ribbonColours[(int)SchemeOfficeColors.PanelAlternative];

                case PaletteBackStyle.TabHighProfile:
                case PaletteBackStyle.TabStandardProfile:
                case PaletteBackStyle.TabLowProfile:
                case PaletteBackStyle.TabOneNote:
                case PaletteBackStyle.TabCustom1:
                case PaletteBackStyle.TabCustom2:
                case PaletteBackStyle.TabCustom3:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return style == PaletteBackStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : _disabledBack;

                        case PaletteState.Normal:
                            return style == PaletteBackStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : SystemColors.Window;

                        case PaletteState.Pressed:
                        case PaletteState.Tracking:
                            return style switch
                            {
                                PaletteBackStyle.TabLowProfile => GlobalStaticValues.EMPTY_COLOR,
                                PaletteBackStyle.TabHighProfile => state == PaletteState.Tracking
                                    ? _buttonBackColors[2]
                                    : _buttonBackColors[4],
                                _ => SystemColors.Window
                            };

                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            if (style == PaletteBackStyle.TabHighProfile)
                            {
                                return state switch
                                {
                                    PaletteState.CheckedNormal => _buttonBackColors[6],
                                    PaletteState.CheckedPressed => _buttonBackColors[4],
                                    _ => _buttonBackColors[8]
                                };
                            }
                            else
                            {
                                return SystemColors.Window;
                            }

                        default:
                            throw DebugTools.NotImplemented(state.ToString());
                    }
                case PaletteBackStyle.TabDock:
                case PaletteBackStyle.TabDockAutoHidden:
                    return state switch
                    {
                        PaletteState.Disabled => _disabledBack,
                        PaletteState.Normal or PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking or PaletteState.Pressed or PaletteState.Tracking => SystemColors.Window,
                        _ => throw DebugTools.NotImplemented(state.ToString())
                    };
                case PaletteBackStyle.HeaderForm:
                    return state == PaletteState.Disabled
                        ? _ribbonColours[(int)SchemeOfficeColors.FormBorderHeaderInactive1]
                        : _ribbonColours[(int)SchemeOfficeColors.FormBorderHeaderActive1];

                case PaletteBackStyle.HeaderCalendar:
                    return state == PaletteState.Disabled
                        ? _ribbonColours[(int)SchemeOfficeColors.HeaderPrimaryBack1]
                        : _ribbonColours[(int)SchemeOfficeColors.HeaderPrimaryBack2];

                case PaletteBackStyle.HeaderPrimary:
                case PaletteBackStyle.HeaderDockInactive:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                case PaletteBackStyle.HeaderCustom3:
                    return state == PaletteState.Disabled ? _disabledBack : _ribbonColours[(int)SchemeOfficeColors.HeaderPrimaryBack1];

                case PaletteBackStyle.HeaderDockActive:
                    return state == PaletteState.Disabled ? _disabledBack : _buttonBackColors[6];

                case PaletteBackStyle.HeaderSecondary:
                    return state == PaletteState.Disabled ? _disabledBack : _ribbonColours[(int)SchemeOfficeColors.HeaderSecondaryBack1];

                case PaletteBackStyle.SeparatorHighInternalProfile:
                    return state == PaletteState.Disabled ? _disabledBack : _ribbonColours[(int)SchemeOfficeColors.SeparatorHighInternalBorder1];

                case PaletteBackStyle.SeparatorHighProfile:
                    return state == PaletteState.Disabled ? _disabledBack : _ribbonColours[(int)SchemeOfficeColors.SeparatorHighBorder1];

                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorCustom1:
                case PaletteBackStyle.SeparatorCustom2:
                case PaletteBackStyle.SeparatorCustom3:
                case PaletteBackStyle.PanelClient:
                    // Note: Alter these to control the backgrounds
                    if (state == PaletteState.Disabled)
                    {
                        return _disabledBack;
                    }
                    else
                    {
                        return _ribbonColours[(int)SchemeOfficeColors.PanelClient];
                    }
                case PaletteBackStyle.PanelRibbonInactive:
                case PaletteBackStyle.PanelCustom1:
                case PaletteBackStyle.PanelCustom2:
                case PaletteBackStyle.PanelCustom3:
                case PaletteBackStyle.ControlGroupBox:
                case PaletteBackStyle.GridBackgroundList:
                case PaletteBackStyle.GridBackgroundSheet:
                case PaletteBackStyle.GridBackgroundCustom1:
                case PaletteBackStyle.GridBackgroundCustom2:
                case PaletteBackStyle.GridBackgroundCustom3:
                    return _ribbonColours[(int)SchemeOfficeColors.PanelClient];
                case PaletteBackStyle.PanelAlternate:
                    // Note: Alter these to control the backgrounds
                    if (state == PaletteState.Disabled)
                    {
                        return _disabledBack;
                    }
                    else
                    {
                        return _ribbonColours[(int)SchemeOfficeColors.PanelAlternative];
                    }
                case PaletteBackStyle.FormMain:
                case PaletteBackStyle.FormCustom1:
                case PaletteBackStyle.FormCustom2:
                case PaletteBackStyle.FormCustom3:
                    return state == PaletteState.Disabled
                        ? _ribbonColours[(int)SchemeOfficeColors.FormBorderInactiveLight]
                        : _ribbonColours[(int)SchemeOfficeColors.FormBorderActiveLight];

                case PaletteBackStyle.ControlClient:
                    // Note: Alter these to control the backgrounds
                    if (state == PaletteState.Disabled)
                    {
                        return _disabledBack;
                    }
                    else
                    {
                        return _ribbonColours[(int)SchemeOfficeColors.PanelClient];
                    }
                case PaletteBackStyle.ControlAlternate:
                    // Note: Alter these to control the backgrounds
                    if (state == PaletteState.Disabled)
                    {
                        return _disabledBack;
                    }
                    else
                    {
                        return _ribbonColours[(int)SchemeOfficeColors.PanelAlternative];
                    }
                case PaletteBackStyle.ControlCustom1:
                case PaletteBackStyle.ControlCustom2:
                case PaletteBackStyle.ControlCustom3:
                    // Note: This controls the input control dropdown background
                    return _ribbonColours[(int)SchemeOfficeColors.PanelClient];
                case PaletteBackStyle.InputControlStandalone:
                case PaletteBackStyle.InputControlRibbon:
                case PaletteBackStyle.InputControlCustom1:
                case PaletteBackStyle.InputControlCustom2:
                case PaletteBackStyle.InputControlCustom3:
                    if (state == PaletteState.Disabled)
                    {
                        return _ribbonColours[(int)SchemeOfficeColors.InputControlBackDisabled];
                    }
                    else
                    {
                        return (state == PaletteState.Tracking) || (style == PaletteBackStyle.InputControlStandalone)
                            ? _ribbonColours[(int)SchemeOfficeColors.InputControlBackNormal]
                            : _ribbonColours[(int)SchemeOfficeColors.InputControlBackInactive];
                    }
                case PaletteBackStyle.ControlRibbon:
                    return _ribbonColours[(int)SchemeOfficeColors.RibbonTabSelected4];
                case PaletteBackStyle.ControlRibbonAppMenu:
                    return _ribbonColours[(int)SchemeOfficeColors.AppButtonBack1];
                case PaletteBackStyle.ControlToolTip:
                    return _toolTipBack1;
                case PaletteBackStyle.ContextMenuOuter:
                    return _contextMenuOuterBack;
                case PaletteBackStyle.ContextMenuSeparator:
                case PaletteBackStyle.ContextMenuItemSplit:
                    return state switch
                    {
                        PaletteState.Tracking => _buttonBackColors[2],
                        _ => _contextMenuOuterBack
                    };
                case PaletteBackStyle.ContextMenuInner:
                    return _contextMenuInnerBack;
                case PaletteBackStyle.ContextMenuHeading:
                    return _ribbonColours[(int)SchemeOfficeColors.ContextMenuHeadingBack];
                case PaletteBackStyle.ContextMenuItemImageColumn:
                    return _ribbonColours[(int)SchemeOfficeColors.ContextMenuImageColumn];
                case PaletteBackStyle.ContextMenuItemImage:
                    return _contextMenuImageBackChecked;
                case PaletteBackStyle.ButtonGallery:
                    return state switch
                    {
                        PaletteState.Disabled => _disabledBack,
                        PaletteState.Normal or PaletteState.NormalDefaultOverride or PaletteState.CheckedNormal => _ribbonColours[(int)SchemeOfficeColors.RibbonGalleryBack1],
                        PaletteState.Tracking or PaletteState.CheckedTracking => _buttonBackColors[2],
                        PaletteState.Pressed or PaletteState.CheckedPressed => _buttonBackColors[4],
                        _ => throw DebugTools.NotImplemented(state.ToString())
                    };
                case PaletteBackStyle.ButtonForm:
                case PaletteBackStyle.ButtonFormClose:
                    return state switch
                    {
                        PaletteState.Disabled or PaletteState.Normal or PaletteState.NormalDefaultOverride => GlobalStaticValues.EMPTY_COLOR,
                        PaletteState.CheckedNormal => _ribbonColours[(int)SchemeOfficeColors.FormButtonBack1Checked],
                        PaletteState.Tracking => _ribbonColours[(int)SchemeOfficeColors.FormButtonBack1Track],
                        PaletteState.CheckedTracking => _ribbonColours[(int)SchemeOfficeColors.FormButtonBack1CheckTrack],
                        PaletteState.Pressed or PaletteState.CheckedPressed => _ribbonColours[(int)SchemeOfficeColors.FormButtonBack1Pressed],
                        _ => throw DebugTools.NotImplemented(state.ToString())
                    };
                case PaletteBackStyle.ButtonStandalone:
                case PaletteBackStyle.ButtonAlternate:
                case PaletteBackStyle.ButtonLowProfile:
                case PaletteBackStyle.ButtonBreadCrumb:
                case PaletteBackStyle.ButtonListItem:
                case PaletteBackStyle.ButtonCommand:
                case PaletteBackStyle.ButtonButtonSpec:
                case PaletteBackStyle.ButtonCalendarDay:
                case PaletteBackStyle.ButtonCluster:
                case PaletteBackStyle.ButtonNavigatorStack:
                case PaletteBackStyle.ButtonNavigatorOverflow:
                case PaletteBackStyle.ButtonNavigatorMini:
                case PaletteBackStyle.ButtonCustom1:
                case PaletteBackStyle.ButtonCustom2:
                case PaletteBackStyle.ButtonCustom3:
                case PaletteBackStyle.ButtonInputControl:
                case PaletteBackStyle.ContextMenuItemHighlight:
                    return state switch
                    {
                        PaletteState.Disabled => _disabledBack,
                        PaletteState.Normal => style switch
                        {
                            PaletteBackStyle.ButtonNavigatorStack => _ribbonColours[
                                (int)SchemeOfficeColors.ButtonNormalNavigatorBack1],
                            PaletteBackStyle.ButtonNavigatorMini => _ribbonColours[
                                (int)SchemeOfficeColors.NavigatorMiniBackColor],
                            PaletteBackStyle.ButtonCluster => _ribbonColours[
                                (int)SchemeOfficeColors.ButtonClusterButtonBack1],
                            _ => _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBack1]
                        },
                        PaletteState.NormalDefaultOverride => _ribbonColours[(int)SchemeOfficeColors.ButtonNormalDefaultBack1],
                        PaletteState.CheckedNormal => style == PaletteBackStyle.ButtonInputControl ? _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBack1] : _buttonBackColors[6],
                        PaletteState.Tracking => _buttonBackColors[2],
                        PaletteState.Pressed or PaletteState.CheckedPressed => style == PaletteBackStyle.ButtonAlternate
                                                    ? _ribbonColours[(int)SchemeOfficeColors.AlternatePressedBack1]
                                                    : _buttonBackColors[4],
                        PaletteState.CheckedTracking => style == PaletteBackStyle.ButtonInputControl ? _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBack1] : _buttonBackColors[8],
                        _ => throw DebugTools.NotImplemented(state.ToString())
                    };
                default:
                    throw DebugTools.NotImplemented(style.ToString());
            }
        }

        /// <summary>
        /// Gets the second back color.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Color value.</returns>
        public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
            {
                return GlobalStaticValues.EMPTY_COLOR;
            }

            switch (style)
            {
                case PaletteBackStyle.GridHeaderColumnList:
                case PaletteBackStyle.GridHeaderColumnCustom1:
                case PaletteBackStyle.GridHeaderColumnCustom2:
                case PaletteBackStyle.GridHeaderColumnCustom3:
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowCustom1:
                case PaletteBackStyle.GridHeaderRowCustom2:
                case PaletteBackStyle.GridHeaderRowCustom3:
                    return state switch
                    {
                        PaletteState.Disabled => _disabledBack,
                        PaletteState.Pressed => _ribbonColours[(int)SchemeOfficeColors.GridListPressed2],
                        PaletteState.CheckedNormal => _ribbonColours[(int)SchemeOfficeColors.GridListSelected],
                        _ => _ribbonColours[(int)SchemeOfficeColors.GridListNormal2]
                    };
                case PaletteBackStyle.GridHeaderColumnSheet:
                    return state switch
                    {
                        PaletteState.Disabled => _disabledBack,
                        PaletteState.Tracking or PaletteState.Pressed => _ribbonColours[(int)SchemeOfficeColors.GridSheetColPressed2],
                        PaletteState.CheckedNormal => _ribbonColours[(int)SchemeOfficeColors.GridSheetColSelected2],
                        _ => _ribbonColours[(int)SchemeOfficeColors.GridSheetColNormal2]
                    };
                case PaletteBackStyle.GridHeaderRowSheet:
                    return state switch
                    {
                        PaletteState.Disabled => _disabledBack,
                        PaletteState.Tracking or PaletteState.Pressed => _ribbonColours[(int)SchemeOfficeColors.GridSheetRowPressed],
                        PaletteState.CheckedNormal => _ribbonColours[(int)SchemeOfficeColors.GridSheetRowSelected],
                        _ => _ribbonColours[(int)SchemeOfficeColors.GridSheetRowNormal]
                    };
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellCustom1:
                case PaletteBackStyle.GridDataCellCustom2:
                case PaletteBackStyle.GridDataCellCustom3:
                    return state == PaletteState.CheckedNormal ? _ribbonColours[(int)SchemeOfficeColors.GridDataCellSelected] : _ribbonColours[(int)SchemeOfficeColors.PanelAlternative];

                case PaletteBackStyle.GridDataCellSheet:
                    return state == PaletteState.CheckedNormal ? _buttonBackColors[7] : _ribbonColours[(int)SchemeOfficeColors.PanelAlternative];

                case PaletteBackStyle.TabHighProfile:
                case PaletteBackStyle.TabStandardProfile:
                case PaletteBackStyle.TabLowProfile:
                case PaletteBackStyle.TabOneNote:
                case PaletteBackStyle.TabCustom1:
                case PaletteBackStyle.TabCustom2:
                case PaletteBackStyle.TabCustom3:
                    return state switch
                    {
                        PaletteState.Disabled => style == PaletteBackStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : _disabledBack,
                        PaletteState.Normal => style == PaletteBackStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBack2],
                        PaletteState.Tracking or PaletteState.Pressed => style == PaletteBackStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : SystemColors.Window,
                        PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => SystemColors.Window,
                        _ => throw DebugTools.NotImplemented(state.ToString())
                    };
                case PaletteBackStyle.TabDock:
                    return state switch
                    {
                        PaletteState.Disabled => _disabledBack,
                        PaletteState.Normal => _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBack2],
                        PaletteState.Tracking or PaletteState.Pressed => _buttonBackColors[3],
                        PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => SystemColors.Window,
                        _ => throw DebugTools.NotImplemented(state.ToString())
                    };
                case PaletteBackStyle.TabDockAutoHidden:
                    return state switch
                    {
                        PaletteState.Disabled => _disabledBack,
                        PaletteState.Normal or PaletteState.CheckedNormal => _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBack2],
                        PaletteState.Tracking or PaletteState.CheckedTracking or PaletteState.Pressed or PaletteState.CheckedPressed => _buttonBackColors[3],
                        _ => throw DebugTools.NotImplemented(state.ToString())
                    };
                case PaletteBackStyle.HeaderForm:
                    return state == PaletteState.Disabled
                        ? _ribbonColours[(int)SchemeOfficeColors.FormBorderHeaderInactive2]
                        : _ribbonColours[(int)SchemeOfficeColors.FormBorderHeaderActive2];

                case PaletteBackStyle.HeaderCalendar:
                    return state == PaletteState.Disabled
                        ? _ribbonColours[(int)SchemeOfficeColors.HeaderPrimaryBack1]
                        : _ribbonColours[(int)SchemeOfficeColors.HeaderPrimaryBack2];

                case PaletteBackStyle.HeaderPrimary:
                case PaletteBackStyle.HeaderDockInactive:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                case PaletteBackStyle.HeaderCustom3:
                    return state == PaletteState.Disabled ? _disabledBack : _ribbonColours[(int)SchemeOfficeColors.HeaderPrimaryBack2];

                case PaletteBackStyle.HeaderDockActive:
                    return state == PaletteState.Disabled ? _disabledBack : _buttonBackColors[7];

                case PaletteBackStyle.HeaderSecondary:
                    return state == PaletteState.Disabled ? _disabledBack : _ribbonColours[(int)SchemeOfficeColors.HeaderSecondaryBack2];

                case PaletteBackStyle.SeparatorHighInternalProfile:
                    return state == PaletteState.Disabled ? _disabledBack : _ribbonColours[(int)SchemeOfficeColors.SeparatorHighInternalBorder2];

                case PaletteBackStyle.SeparatorHighProfile:
                    return state == PaletteState.Disabled ? _disabledBack : _ribbonColours[(int)SchemeOfficeColors.SeparatorHighBorder2];

                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorCustom1:
                case PaletteBackStyle.SeparatorCustom2:
                case PaletteBackStyle.SeparatorCustom3:
                case PaletteBackStyle.PanelClient:
                    // Note: Alter these to control the backgrounds
                    if (state == PaletteState.Disabled)
                    {
                        return _disabledBack;
                    }
                    else
                    {
                        return _ribbonColours[(int)SchemeOfficeColors.PanelClient];
                    }
                case PaletteBackStyle.PanelRibbonInactive:
                case PaletteBackStyle.PanelCustom1:
                case PaletteBackStyle.PanelCustom2:
                case PaletteBackStyle.PanelCustom3:
                case PaletteBackStyle.ControlGroupBox:
                case PaletteBackStyle.GridBackgroundList:
                case PaletteBackStyle.GridBackgroundSheet:
                case PaletteBackStyle.GridBackgroundCustom1:
                case PaletteBackStyle.GridBackgroundCustom2:
                case PaletteBackStyle.GridBackgroundCustom3:
                    return _ribbonColours[(int)SchemeOfficeColors.PanelClient];
                case PaletteBackStyle.PanelAlternate:
                    // Note: Alter these to control the backgrounds
                    if (state == PaletteState.Disabled)
                    {
                        return _disabledBack;
                    }
                    else
                    {
                        return _ribbonColours[(int)SchemeOfficeColors.PanelAlternative];
                    }
                case PaletteBackStyle.FormMain:
                case PaletteBackStyle.FormCustom1:
                case PaletteBackStyle.FormCustom2:
                case PaletteBackStyle.FormCustom3:
                    return state == PaletteState.Disabled
                        ? _ribbonColours[(int)SchemeOfficeColors.FormBorderInactiveDark]
                        : _ribbonColours[(int)SchemeOfficeColors.FormBorderActiveDark];

                case PaletteBackStyle.ControlClient:
                    // Note: Alter these to control the backgrounds
                    if (state == PaletteState.Disabled)
                    {
                        return _disabledBack;
                    }
                    else
                    {
                        return _ribbonColours[(int)SchemeOfficeColors.PanelClient];
                    }
                case PaletteBackStyle.ControlAlternate:
                    // Note: Alter these to control the backgrounds
                    if (state == PaletteState.Disabled)
                    {
                        return _disabledBack;
                    }
                    else
                    {
                        return _ribbonColours[(int)SchemeOfficeColors.PanelAlternative];
                    }
                case PaletteBackStyle.ControlCustom1:
                case PaletteBackStyle.ControlCustom2:
                case PaletteBackStyle.ControlCustom3:
                    // Note: This controls the input control dropdown background
                    return _ribbonColours[(int)SchemeOfficeColors.PanelClient];
                case PaletteBackStyle.InputControlStandalone:
                case PaletteBackStyle.InputControlRibbon:
                case PaletteBackStyle.InputControlCustom1:
                case PaletteBackStyle.InputControlCustom2:
                case PaletteBackStyle.InputControlCustom3:
                    if (state == PaletteState.Disabled)
                    {
                        return _ribbonColours[(int)SchemeOfficeColors.InputControlBackDisabled];
                    }
                    else
                    {
                        return (state == PaletteState.Tracking) || (style == PaletteBackStyle.InputControlStandalone)
                            ? _ribbonColours[(int)SchemeOfficeColors.InputControlBackNormal]
                            : _ribbonColours[(int)SchemeOfficeColors.InputControlBackInactive];
                    }
                case PaletteBackStyle.ControlRibbon:
                    return _ribbonColours[(int)SchemeOfficeColors.RibbonTabSelected4];
                case PaletteBackStyle.ControlRibbonAppMenu:
                    return _ribbonColours[(int)SchemeOfficeColors.AppButtonBack2];
                case PaletteBackStyle.ControlToolTip:
                    return _toolTipBack2;
                case PaletteBackStyle.ContextMenuOuter:
                    return _contextMenuOuterBack;
                case PaletteBackStyle.ContextMenuSeparator:
                case PaletteBackStyle.ContextMenuItemSplit:
                    return state == PaletteState.Tracking ? _buttonBackColors[3] : _contextMenuOuterBack;
                case PaletteBackStyle.ContextMenuInner:
                    return _contextMenuInnerBack;
                case PaletteBackStyle.ContextMenuHeading:
                    return _ribbonColours[(int)SchemeOfficeColors.ContextMenuHeadingBack];
                case PaletteBackStyle.ContextMenuItemImageColumn:
                    return _ribbonColours[(int)SchemeOfficeColors.ContextMenuImageColumn];
                case PaletteBackStyle.ContextMenuItemImage:
                    return _contextMenuImageBackChecked;
                case PaletteBackStyle.ButtonGallery:
                    return state switch
                    {
                        PaletteState.Disabled => _disabledBack2,
                        PaletteState.Normal or PaletteState.NormalDefaultOverride or PaletteState.CheckedNormal => _ribbonColours[(int)SchemeOfficeColors.RibbonGalleryBack2],
                        PaletteState.Tracking or PaletteState.CheckedTracking => _buttonBackColors[2],
                        PaletteState.Pressed or PaletteState.CheckedPressed => _buttonBackColors[4],
                        _ => throw DebugTools.NotImplemented(state.ToString())
                    };
                case PaletteBackStyle.ButtonForm:
                case PaletteBackStyle.ButtonFormClose:
                    return state switch
                    {
                        PaletteState.Disabled or PaletteState.Normal or PaletteState.NormalDefaultOverride => GlobalStaticValues.EMPTY_COLOR,
                        PaletteState.CheckedNormal => _ribbonColours[(int)SchemeOfficeColors.FormButtonBack2Checked],
                        PaletteState.Tracking => _ribbonColours[(int)SchemeOfficeColors.FormButtonBack2Track],
                        PaletteState.CheckedTracking => _ribbonColours[(int)SchemeOfficeColors.FormButtonBack2CheckTrack],
                        PaletteState.Pressed or PaletteState.CheckedPressed => _ribbonColours[(int)SchemeOfficeColors.FormButtonBack2Pressed],
                        _ => throw DebugTools.NotImplemented(state.ToString())
                    };
                case PaletteBackStyle.ButtonStandalone:
                case PaletteBackStyle.ButtonAlternate:
                case PaletteBackStyle.ButtonLowProfile:
                case PaletteBackStyle.ButtonBreadCrumb:
                case PaletteBackStyle.ButtonListItem:
                case PaletteBackStyle.ButtonCommand:
                case PaletteBackStyle.ButtonButtonSpec:
                case PaletteBackStyle.ButtonCalendarDay:
                case PaletteBackStyle.ButtonCluster:
                case PaletteBackStyle.ButtonNavigatorStack:
                case PaletteBackStyle.ButtonNavigatorOverflow:
                case PaletteBackStyle.ButtonNavigatorMini:
                case PaletteBackStyle.ButtonCustom1:
                case PaletteBackStyle.ButtonCustom2:
                case PaletteBackStyle.ButtonCustom3:
                case PaletteBackStyle.ButtonInputControl:
                case PaletteBackStyle.ContextMenuItemHighlight:
                    return state switch
                    {
                        PaletteState.Disabled => _buttonBackColors[1],
                        PaletteState.Normal => style switch
                        {
                            PaletteBackStyle.ButtonNavigatorStack => _ribbonColours[
                                (int)SchemeOfficeColors.ButtonNormalNavigatorBack2],
                            PaletteBackStyle.ButtonNavigatorMini => _ribbonColours[
                                (int)SchemeOfficeColors.NavigatorMiniBackColor],
                            PaletteBackStyle.ButtonCluster => _ribbonColours[
                                (int)SchemeOfficeColors.ButtonClusterButtonBack2],
                            _ => _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBack2]
                        },
                        PaletteState.NormalDefaultOverride => style switch
                        {
                            PaletteBackStyle.ButtonLowProfile or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ContextMenuItemHighlight => GlobalStaticValues.EMPTY_COLOR,
                            _ => _ribbonColours[(int)SchemeOfficeColors.ButtonNormalDefaultBack2]
                        },
                        PaletteState.CheckedNormal => style == PaletteBackStyle.ButtonInputControl ? _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBack2] : _buttonBackColors[7],
                        PaletteState.Tracking => _buttonBackColors[3],
                        PaletteState.Pressed or PaletteState.CheckedPressed => style == PaletteBackStyle.ButtonAlternate
                                                ? _ribbonColours[(int)SchemeOfficeColors.AlternatePressedBack2]
                                                : _buttonBackColors[5],
                        PaletteState.CheckedTracking => style == PaletteBackStyle.ButtonInputControl ? _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBack1] : _buttonBackColors[9],
                        _ => throw DebugTools.NotImplemented(state.ToString())
                    };
                default:
                    throw DebugTools.NotImplemented(style.ToString());
            }
        }

        /// <summary>
        /// Gets the color background drawing style.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteColorStyle.Inherit;
            }

            return style switch
            {
                PaletteBackStyle.GridHeaderColumnList or PaletteBackStyle.GridHeaderColumnCustom1 or PaletteBackStyle.GridHeaderColumnCustom2 or PaletteBackStyle.GridHeaderColumnCustom3 => PaletteColorStyle.RoundedTopLeftWhite,
                PaletteBackStyle.GridHeaderRowList or PaletteBackStyle.GridHeaderRowCustom1 or PaletteBackStyle.GridHeaderRowCustom2 or PaletteBackStyle.GridHeaderRowCustom3 => state == PaletteState.CheckedNormal ? PaletteColorStyle.Linear : PaletteColorStyle.RoundedTopLeftWhite,
                PaletteBackStyle.GridHeaderColumnSheet or PaletteBackStyle.GridHeaderRowSheet => PaletteColorStyle.Linear,
                PaletteBackStyle.GridDataCellList or PaletteBackStyle.GridDataCellCustom1 or PaletteBackStyle.GridDataCellCustom2 or PaletteBackStyle.GridDataCellCustom3 => PaletteColorStyle.Solid,
                PaletteBackStyle.GridDataCellSheet => PaletteColorStyle.GlassCheckedSimple,
                PaletteBackStyle.TabHighProfile or PaletteBackStyle.TabCustom1 or PaletteBackStyle.TabCustom2 or PaletteBackStyle.TabCustom3 => state switch
                {
                    PaletteState.Tracking or PaletteState.Pressed or PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed => PaletteColorStyle.GlassFade,
                    _ => PaletteColorStyle.QuarterPhase
                },
                PaletteBackStyle.TabStandardProfile => state switch
                {
                    PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed => PaletteColorStyle.Solid,
                    PaletteState.Tracking or PaletteState.Pressed => PaletteColorStyle.GlassFade,
                    _ => PaletteColorStyle.QuarterPhase
                },
                PaletteBackStyle.TabLowProfile => PaletteColorStyle.Solid,
                PaletteBackStyle.TabOneNote or PaletteBackStyle.TabDock or PaletteBackStyle.TabDockAutoHidden => PaletteColorStyle.OneNote,
                PaletteBackStyle.PanelClient
                    or PaletteBackStyle.PanelRibbonInactive
                    or PaletteBackStyle.PanelAlternate
                    or PaletteBackStyle.PanelCustom1
                    or PaletteBackStyle.PanelCustom2
                    or PaletteBackStyle.PanelCustom3
                    or PaletteBackStyle.SeparatorLowProfile
                    or PaletteBackStyle.SeparatorCustom1
                    or PaletteBackStyle.SeparatorCustom2
                    or PaletteBackStyle.SeparatorCustom3
                    or PaletteBackStyle.ControlClient
                    or PaletteBackStyle.ControlAlternate
                    or PaletteBackStyle.ControlGroupBox
                    or PaletteBackStyle.ControlRibbon
                    or PaletteBackStyle.ContextMenuOuter
                    or PaletteBackStyle.ContextMenuInner
                    or PaletteBackStyle.ControlCustom1
                    or PaletteBackStyle.ControlCustom2
                    or PaletteBackStyle.ControlCustom3
                    or PaletteBackStyle.ContextMenuHeading
                    or PaletteBackStyle.ContextMenuItemImageColumn
                    or PaletteBackStyle.InputControlStandalone
                    or PaletteBackStyle.InputControlRibbon
                    or PaletteBackStyle.InputControlCustom1
                    or PaletteBackStyle.InputControlCustom2
                    or PaletteBackStyle.InputControlCustom3
                    or PaletteBackStyle.GridBackgroundList
                    or PaletteBackStyle.GridBackgroundSheet
                                        or PaletteBackStyle.GridBackgroundCustom1
                    or PaletteBackStyle.GridBackgroundCustom2
                    or PaletteBackStyle.GridBackgroundCustom3

                    or PaletteBackStyle.GridBackgroundCustom2
                    or PaletteBackStyle.GridBackgroundCustom3
                    or PaletteBackStyle.HeaderCalendar
                    or PaletteBackStyle.ButtonCalendarDay => PaletteColorStyle.Solid,
                PaletteBackStyle.ControlRibbonAppMenu => PaletteColorStyle.Switch90,
                PaletteBackStyle.ContextMenuSeparator or PaletteBackStyle.ContextMenuItemSplit => state == PaletteState.Tracking ? PaletteColorStyle.GlassTrackingFull : PaletteColorStyle.Solid,
                PaletteBackStyle.ControlToolTip => PaletteColorStyle.Linear,
                PaletteBackStyle.FormMain or PaletteBackStyle.FormCustom1 or PaletteBackStyle.FormCustom2 or PaletteBackStyle.FormCustom3 => PaletteColorStyle.SolidInside,
                PaletteBackStyle.SeparatorHighInternalProfile or PaletteBackStyle.SeparatorHighProfile => PaletteColorStyle.RoundedTopLight,
                PaletteBackStyle.HeaderPrimary
                    or PaletteBackStyle.HeaderDockInactive
                    or PaletteBackStyle.HeaderSecondary
                    or PaletteBackStyle.HeaderCustom1
                    or PaletteBackStyle.HeaderCustom2
                    or PaletteBackStyle.HeaderCustom3 => PaletteColorStyle.RoundedTopLeftWhite,
                PaletteBackStyle.HeaderDockActive => PaletteColorStyle.Rounded,
                PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose => state switch
                {
                    PaletteState.Disabled or PaletteState.Normal or PaletteState.CheckedNormal => PaletteColorStyle.GlassNormalFull,
                    PaletteState.Tracking or PaletteState.CheckedTracking => PaletteColorStyle.GlassCenter,
                    PaletteState.Pressed or PaletteState.CheckedPressed => PaletteColorStyle.GlassBottom,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                },
                PaletteBackStyle.ButtonGallery => state switch
                {
                    PaletteState.Disabled or PaletteState.Normal or PaletteState.NormalDefaultOverride or PaletteState.CheckedNormal => PaletteColorStyle.GlassSimpleFull,
                    PaletteState.Tracking or PaletteState.CheckedTracking => PaletteColorStyle.GlassTrackingFull,
                    PaletteState.Pressed or PaletteState.CheckedPressed => PaletteColorStyle.GlassPressedFull,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                },
                PaletteBackStyle.ButtonAlternate => state switch
                {
                    PaletteState.Disabled or PaletteState.Normal => PaletteColorStyle.GlassNormalStump,
                    PaletteState.Tracking => PaletteColorStyle.GlassTrackingStump,
                    PaletteState.Pressed or PaletteState.CheckedPressed => PaletteColorStyle.GlassPressedStump,
                    PaletteState.CheckedNormal => PaletteColorStyle.GlassCheckedStump,
                    PaletteState.CheckedTracking => PaletteColorStyle.GlassCheckedTrackingStump,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                },
                PaletteBackStyle.ButtonStandalone
                    or PaletteBackStyle.ButtonLowProfile
                    or PaletteBackStyle.ButtonBreadCrumb
                    or PaletteBackStyle.ButtonListItem
                    or PaletteBackStyle.ButtonCommand
                    or PaletteBackStyle.ButtonButtonSpec
                    or PaletteBackStyle.ButtonCluster
                    or PaletteBackStyle.ButtonCustom1
                    or PaletteBackStyle.ButtonCustom2
                    or PaletteBackStyle.ButtonCustom3
                    or PaletteBackStyle.ContextMenuItemHighlight => state switch
                    {
                        PaletteState.Disabled or PaletteState.Normal => style == PaletteBackStyle.ButtonCluster ? PaletteColorStyle.GlassThreeEdge : PaletteColorStyle.GlassNormalFull,
                        PaletteState.Tracking => PaletteColorStyle.GlassTrackingFull,
                        PaletteState.Pressed or PaletteState.CheckedPressed => PaletteColorStyle.GlassPressedFull,
                        PaletteState.CheckedNormal => PaletteColorStyle.GlassCheckedFull,
                        PaletteState.CheckedTracking => PaletteColorStyle.GlassCheckedTrackingFull,
                        _ => throw DebugTools.NotImplemented(state.ToString())
                    },
                PaletteBackStyle.ContextMenuItemImage => PaletteColorStyle.Solid,
                PaletteBackStyle.ButtonNavigatorStack or PaletteBackStyle.ButtonNavigatorOverflow or PaletteBackStyle.ButtonInputControl => state switch
                {
                    PaletteState.Disabled or PaletteState.Normal => PaletteColorStyle.GlassNormalSimple,
                    PaletteState.Tracking => PaletteColorStyle.GlassTrackingSimple,
                    PaletteState.Pressed or PaletteState.CheckedPressed => PaletteColorStyle.GlassPressedSimple,
                    PaletteState.CheckedNormal => PaletteColorStyle.GlassCheckedSimple,
                    PaletteState.CheckedTracking => PaletteColorStyle.GlassCheckedTrackingSimple,
                    _ => throw DebugTools.NotImplemented(state.ToString())
                },
                PaletteBackStyle.ButtonNavigatorMini => state switch
                {
                    PaletteState.Normal => PaletteColorStyle.RoundedTopLeftWhite,
                    _ => PaletteColorStyle.Solid
                },
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the color alignment.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetBackColorAlign(PaletteBackStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteRectangleAlign.Inherit;
            }

            return style switch
            {
                PaletteBackStyle.ControlClient
                    or PaletteBackStyle.ControlAlternate
                    or PaletteBackStyle.ControlGroupBox
                    or PaletteBackStyle.ControlRibbon
                    or PaletteBackStyle.ControlRibbonAppMenu
                    or PaletteBackStyle.ControlCustom1
                    or PaletteBackStyle.ControlCustom2
                    or PaletteBackStyle.ControlCustom3
                    or PaletteBackStyle.InputControlStandalone
                    or PaletteBackStyle.InputControlRibbon
                    or PaletteBackStyle.InputControlCustom1
                    or PaletteBackStyle.InputControlCustom2
                    or PaletteBackStyle.InputControlCustom3
                    or PaletteBackStyle.FormMain
                    or PaletteBackStyle.FormCustom1
                    or PaletteBackStyle.FormCustom2
                    or PaletteBackStyle.FormCustom3
                    or PaletteBackStyle.PanelClient
                    or PaletteBackStyle.PanelRibbonInactive
                    or PaletteBackStyle.PanelAlternate
                    or PaletteBackStyle.PanelCustom1
                    or PaletteBackStyle.PanelCustom2
                    or PaletteBackStyle.PanelCustom3
                    or PaletteBackStyle.GridBackgroundList
                    or PaletteBackStyle.GridBackgroundSheet
                                        or PaletteBackStyle.GridBackgroundCustom1
                    or PaletteBackStyle.GridBackgroundCustom2
                    or PaletteBackStyle.GridBackgroundCustom3

                    or PaletteBackStyle.GridBackgroundCustom2
                    or PaletteBackStyle.GridBackgroundCustom3 => PaletteRectangleAlign.Control,
                PaletteBackStyle.ControlToolTip
                    or PaletteBackStyle.SeparatorLowProfile
                    or PaletteBackStyle.SeparatorHighInternalProfile
                    or PaletteBackStyle.SeparatorHighProfile
                    or PaletteBackStyle.SeparatorCustom1
                    or PaletteBackStyle.SeparatorCustom2
                    or PaletteBackStyle.SeparatorCustom3
                    or PaletteBackStyle.HeaderPrimary
                    or PaletteBackStyle.HeaderDockInactive
                    or PaletteBackStyle.HeaderDockActive
                    or PaletteBackStyle.HeaderCalendar
                    or PaletteBackStyle.HeaderSecondary
                    or PaletteBackStyle.HeaderForm
                    or PaletteBackStyle.HeaderCustom1
                    or PaletteBackStyle.HeaderCustom2
                    or PaletteBackStyle.HeaderCustom3
                    or PaletteBackStyle.TabHighProfile
                    or PaletteBackStyle.TabStandardProfile
                    or PaletteBackStyle.TabLowProfile
                    or PaletteBackStyle.TabOneNote
                    or PaletteBackStyle.TabDock
                    or PaletteBackStyle.TabDockAutoHidden
                    or PaletteBackStyle.TabCustom1
                    or PaletteBackStyle.TabCustom2
                    or PaletteBackStyle.TabCustom3
                    or PaletteBackStyle.ButtonStandalone
                    or PaletteBackStyle.ButtonGallery
                    or PaletteBackStyle.ButtonAlternate
                    or PaletteBackStyle.ButtonLowProfile
                    or PaletteBackStyle.ButtonBreadCrumb
                    or PaletteBackStyle.ButtonListItem
                    or PaletteBackStyle.ButtonCommand
                    or PaletteBackStyle.ButtonButtonSpec
                    or PaletteBackStyle.ButtonCalendarDay
                    or PaletteBackStyle.ButtonCluster
                    or PaletteBackStyle.ButtonNavigatorStack
                    or PaletteBackStyle.ButtonNavigatorOverflow
                    or PaletteBackStyle.ButtonNavigatorMini
                    or PaletteBackStyle.ButtonForm
                    or PaletteBackStyle.ButtonFormClose
                    or PaletteBackStyle.ButtonCustom1
                    or PaletteBackStyle.ButtonCustom2
                    or PaletteBackStyle.ButtonCustom3
                    or PaletteBackStyle.ButtonInputControl
                    or PaletteBackStyle.GridHeaderColumnList
                    or PaletteBackStyle.GridHeaderColumnSheet
                    or PaletteBackStyle.GridHeaderColumnCustom1
                    or PaletteBackStyle.GridHeaderColumnCustom2
                    or PaletteBackStyle.GridHeaderColumnCustom3
                    or PaletteBackStyle.GridHeaderRowList
                    or PaletteBackStyle.GridHeaderRowSheet
                    or PaletteBackStyle.GridHeaderRowCustom1
                    or PaletteBackStyle.GridHeaderRowCustom2
                    or PaletteBackStyle.GridHeaderRowCustom3
                    or PaletteBackStyle.GridDataCellList
                    or PaletteBackStyle.GridDataCellSheet
                    or PaletteBackStyle.GridDataCellCustom1
                    or PaletteBackStyle.GridDataCellCustom2
                    or PaletteBackStyle.GridDataCellCustom3
                    or PaletteBackStyle.ContextMenuItemImage
                    or PaletteBackStyle.ContextMenuItemHighlight
                    or PaletteBackStyle.ContextMenuOuter
                    or PaletteBackStyle.ContextMenuInner
                    or PaletteBackStyle.ContextMenuHeading
                    or PaletteBackStyle.ContextMenuSeparator
                    or PaletteBackStyle.ContextMenuItemSplit
                    or PaletteBackStyle.ContextMenuItemImageColumn => PaletteRectangleAlign.Local,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the color background angle.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetBackColorAngle(PaletteBackStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return -1f;
            }

            return style switch
            {
                PaletteBackStyle.PanelClient
                    or PaletteBackStyle.PanelRibbonInactive
                    or PaletteBackStyle.PanelAlternate
                    or PaletteBackStyle.PanelCustom1
                    or PaletteBackStyle.PanelCustom2
                    or PaletteBackStyle.PanelCustom3
                    or PaletteBackStyle.SeparatorLowProfile
                    or PaletteBackStyle.SeparatorHighInternalProfile
                    or PaletteBackStyle.SeparatorHighProfile
                    or PaletteBackStyle.SeparatorCustom1
                    or PaletteBackStyle.SeparatorCustom2
                    or PaletteBackStyle.SeparatorCustom3
                    or PaletteBackStyle.ControlClient
                    or PaletteBackStyle.ControlAlternate
                    or PaletteBackStyle.ControlGroupBox
                    or PaletteBackStyle.ControlToolTip
                    or PaletteBackStyle.ControlRibbon
                    or PaletteBackStyle.ControlRibbonAppMenu
                    or PaletteBackStyle.ControlCustom1
                    or PaletteBackStyle.ControlCustom2
                    or PaletteBackStyle.ControlCustom3
                    or PaletteBackStyle.ContextMenuOuter
                    or PaletteBackStyle.ContextMenuInner
                    or PaletteBackStyle.ContextMenuHeading
                    or PaletteBackStyle.ContextMenuSeparator
                    or PaletteBackStyle.ContextMenuItemSplit
                    or PaletteBackStyle.ContextMenuItemImageColumn
                    or PaletteBackStyle.InputControlStandalone
                    or PaletteBackStyle.InputControlRibbon
                    or PaletteBackStyle.InputControlCustom1
                    or PaletteBackStyle.InputControlCustom2
                    or PaletteBackStyle.InputControlCustom3
                    or PaletteBackStyle.FormMain
                    or PaletteBackStyle.FormCustom1
                    or PaletteBackStyle.FormCustom2
                    or PaletteBackStyle.FormCustom3
                    or PaletteBackStyle.HeaderPrimary
                    or PaletteBackStyle.HeaderDockInactive
                    or PaletteBackStyle.HeaderDockActive
                    or PaletteBackStyle.HeaderCalendar
                    or PaletteBackStyle.HeaderSecondary
                    or PaletteBackStyle.HeaderForm
                    or PaletteBackStyle.HeaderCustom1
                    or PaletteBackStyle.HeaderCustom2
                    or PaletteBackStyle.HeaderCustom3
                    or PaletteBackStyle.TabHighProfile
                    or PaletteBackStyle.TabStandardProfile
                    or PaletteBackStyle.TabLowProfile
                    or PaletteBackStyle.TabOneNote
                    or PaletteBackStyle.TabDock
                    or PaletteBackStyle.TabDockAutoHidden
                    or PaletteBackStyle.TabCustom1
                    or PaletteBackStyle.TabCustom2
                    or PaletteBackStyle.TabCustom3
                    or PaletteBackStyle.ButtonStandalone
                    or PaletteBackStyle.ButtonGallery
                    or PaletteBackStyle.ButtonAlternate
                    or PaletteBackStyle.ButtonLowProfile
                    or PaletteBackStyle.ButtonBreadCrumb
                    or PaletteBackStyle.ButtonListItem
                    or PaletteBackStyle.ButtonCommand
                    or PaletteBackStyle.ButtonButtonSpec
                    or PaletteBackStyle.ButtonCalendarDay
                    or PaletteBackStyle.ButtonCluster
                    or PaletteBackStyle.ButtonNavigatorStack
                    or PaletteBackStyle.ButtonNavigatorOverflow
                    or PaletteBackStyle.ButtonNavigatorMini
                    or PaletteBackStyle.ButtonForm
                    or PaletteBackStyle.ButtonFormClose
                    or PaletteBackStyle.ButtonCustom1
                    or PaletteBackStyle.ButtonCustom2
                    or PaletteBackStyle.ButtonCustom3
                    or PaletteBackStyle.ButtonInputControl
                    or PaletteBackStyle.ContextMenuItemImage
                    or PaletteBackStyle.ContextMenuItemHighlight
                    or PaletteBackStyle.GridBackgroundList
                    or PaletteBackStyle.GridBackgroundSheet
                                        or PaletteBackStyle.GridBackgroundCustom1
                    or PaletteBackStyle.GridBackgroundCustom2
                    or PaletteBackStyle.GridBackgroundCustom3

                    or PaletteBackStyle.GridBackgroundCustom2
                    or PaletteBackStyle.GridBackgroundCustom3
                    or PaletteBackStyle.GridHeaderColumnList
                    or PaletteBackStyle.GridHeaderColumnSheet
                    or PaletteBackStyle.GridHeaderColumnCustom1
                    or PaletteBackStyle.GridHeaderColumnCustom2
                    or PaletteBackStyle.GridHeaderColumnCustom3
                    or PaletteBackStyle.GridHeaderRowList
                    or PaletteBackStyle.GridHeaderRowSheet
                    or PaletteBackStyle.GridHeaderRowCustom1
                    or PaletteBackStyle.GridHeaderRowCustom2
                    or PaletteBackStyle.GridHeaderRowCustom3
                    or PaletteBackStyle.GridDataCellList
                    or PaletteBackStyle.GridDataCellSheet
                    or PaletteBackStyle.GridDataCellCustom1
                    or PaletteBackStyle.GridDataCellCustom2
                    or PaletteBackStyle.GridDataCellCustom3 => 90f,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets a background image.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Image instance.</returns>
        public override Image? GetBackImage(PaletteBackStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return null;
            }

            return style switch
            {
                PaletteBackStyle.PanelClient
                    or PaletteBackStyle.PanelRibbonInactive
                    or PaletteBackStyle.PanelAlternate
                    or PaletteBackStyle.PanelCustom1
                    or PaletteBackStyle.PanelCustom2
                    or PaletteBackStyle.PanelCustom3
                    or PaletteBackStyle.SeparatorLowProfile
                    or PaletteBackStyle.SeparatorHighInternalProfile
                    or PaletteBackStyle.SeparatorHighProfile
                    or PaletteBackStyle.SeparatorCustom1
                    or PaletteBackStyle.SeparatorCustom2
                    or PaletteBackStyle.SeparatorCustom3
                    or PaletteBackStyle.ControlClient
                    or PaletteBackStyle.ControlAlternate
                    or PaletteBackStyle.ControlGroupBox
                    or PaletteBackStyle.ControlToolTip
                    or PaletteBackStyle.ControlRibbon
                    or PaletteBackStyle.ControlRibbonAppMenu
                    or PaletteBackStyle.ControlCustom1
                    or PaletteBackStyle.ControlCustom2
                    or PaletteBackStyle.ControlCustom3
                    or PaletteBackStyle.ContextMenuOuter
                    or PaletteBackStyle.ContextMenuInner
                    or PaletteBackStyle.ContextMenuHeading
                    or PaletteBackStyle.ContextMenuSeparator
                    or PaletteBackStyle.ContextMenuItemSplit
                    or PaletteBackStyle.ContextMenuItemImageColumn
                    or PaletteBackStyle.InputControlStandalone
                    or PaletteBackStyle.InputControlRibbon
                    or PaletteBackStyle.InputControlCustom1
                    or PaletteBackStyle.InputControlCustom2
                    or PaletteBackStyle.InputControlCustom3
                    or PaletteBackStyle.FormMain
                    or PaletteBackStyle.FormCustom1
                    or PaletteBackStyle.FormCustom2
                    or PaletteBackStyle.FormCustom3
                    or PaletteBackStyle.HeaderPrimary
                    or PaletteBackStyle.HeaderDockInactive
                    or PaletteBackStyle.HeaderDockActive
                    or PaletteBackStyle.HeaderCalendar
                    or PaletteBackStyle.HeaderSecondary
                    or PaletteBackStyle.HeaderForm
                    or PaletteBackStyle.HeaderCustom1
                    or PaletteBackStyle.HeaderCustom2
                    or PaletteBackStyle.HeaderCustom3
                    or PaletteBackStyle.TabHighProfile
                    or PaletteBackStyle.TabStandardProfile
                    or PaletteBackStyle.TabLowProfile
                    or PaletteBackStyle.TabOneNote
                    or PaletteBackStyle.TabDock
                    or PaletteBackStyle.TabDockAutoHidden
                    or PaletteBackStyle.TabCustom1
                    or PaletteBackStyle.TabCustom2
                    or PaletteBackStyle.TabCustom3
                    or PaletteBackStyle.ButtonStandalone
                    or PaletteBackStyle.ButtonGallery
                    or PaletteBackStyle.ButtonAlternate
                    or PaletteBackStyle.ButtonLowProfile
                    or PaletteBackStyle.ButtonBreadCrumb
                    or PaletteBackStyle.ButtonListItem
                    or PaletteBackStyle.ButtonCommand
                    or PaletteBackStyle.ButtonButtonSpec
                    or PaletteBackStyle.ButtonCalendarDay
                    or PaletteBackStyle.ButtonCluster
                    or PaletteBackStyle.ButtonNavigatorStack
                    or PaletteBackStyle.ButtonNavigatorOverflow
                    or PaletteBackStyle.ButtonNavigatorMini
                    or PaletteBackStyle.ButtonForm
                    or PaletteBackStyle.ButtonFormClose
                    or PaletteBackStyle.ButtonCustom1
                    or PaletteBackStyle.ButtonCustom2
                    or PaletteBackStyle.ButtonCustom3
                    or PaletteBackStyle.ButtonInputControl
                    or PaletteBackStyle.ContextMenuItemImage
                    or PaletteBackStyle.ContextMenuItemHighlight
                    or PaletteBackStyle.GridBackgroundList
                    or PaletteBackStyle.GridBackgroundSheet
                                        or PaletteBackStyle.GridBackgroundCustom1
                    or PaletteBackStyle.GridBackgroundCustom2
                    or PaletteBackStyle.GridBackgroundCustom3

                    or PaletteBackStyle.GridBackgroundCustom2
                    or PaletteBackStyle.GridBackgroundCustom3
                    or PaletteBackStyle.GridHeaderColumnList
                    or PaletteBackStyle.GridHeaderColumnSheet
                    or PaletteBackStyle.GridHeaderColumnCustom1
                    or PaletteBackStyle.GridHeaderColumnCustom2
                    or PaletteBackStyle.GridHeaderColumnCustom3
                    or PaletteBackStyle.GridHeaderRowList
                    or PaletteBackStyle.GridHeaderRowSheet
                    or PaletteBackStyle.GridHeaderRowCustom1
                    or PaletteBackStyle.GridHeaderRowCustom2
                    or PaletteBackStyle.GridHeaderRowCustom3
                    or PaletteBackStyle.GridDataCellList
                    or PaletteBackStyle.GridDataCellSheet
                    or PaletteBackStyle.GridDataCellCustom1
                    or PaletteBackStyle.GridDataCellCustom2
                    or PaletteBackStyle.GridDataCellCustom3 => null,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetBackImageStyle(PaletteBackStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteImageStyle.Inherit;
            }

            return style switch
            {
                PaletteBackStyle.PanelClient
                    or PaletteBackStyle.PanelRibbonInactive
                    or PaletteBackStyle.PanelAlternate
                    or PaletteBackStyle.PanelCustom1
                    or PaletteBackStyle.PanelCustom2
                    or PaletteBackStyle.PanelCustom3
                    or PaletteBackStyle.SeparatorLowProfile
                    or PaletteBackStyle.SeparatorHighInternalProfile
                    or PaletteBackStyle.SeparatorHighProfile
                    or PaletteBackStyle.SeparatorCustom1
                    or PaletteBackStyle.SeparatorCustom2
                    or PaletteBackStyle.SeparatorCustom3
                    or PaletteBackStyle.ControlClient
                    or PaletteBackStyle.ControlAlternate
                    or PaletteBackStyle.ControlGroupBox
                    or PaletteBackStyle.ControlToolTip
                    or PaletteBackStyle.ControlRibbon
                    or PaletteBackStyle.ControlRibbonAppMenu
                    or PaletteBackStyle.ControlCustom1
                    or PaletteBackStyle.ControlCustom2
                    or PaletteBackStyle.ControlCustom3
                    or PaletteBackStyle.ContextMenuOuter
                    or PaletteBackStyle.ContextMenuInner
                    or PaletteBackStyle.ContextMenuHeading
                    or PaletteBackStyle.ContextMenuSeparator
                    or PaletteBackStyle.ContextMenuItemSplit
                    or PaletteBackStyle.ContextMenuItemImageColumn
                    or PaletteBackStyle.InputControlStandalone
                    or PaletteBackStyle.InputControlRibbon
                    or PaletteBackStyle.InputControlCustom1
                    or PaletteBackStyle.InputControlCustom2
                    or PaletteBackStyle.InputControlCustom3
                    or PaletteBackStyle.FormMain
                    or PaletteBackStyle.FormCustom1
                    or PaletteBackStyle.FormCustom2
                    or PaletteBackStyle.FormCustom3
                    or PaletteBackStyle.HeaderPrimary
                    or PaletteBackStyle.HeaderDockInactive
                    or PaletteBackStyle.HeaderDockActive
                    or PaletteBackStyle.HeaderCalendar
                    or PaletteBackStyle.HeaderSecondary
                    or PaletteBackStyle.HeaderForm
                    or PaletteBackStyle.HeaderCustom1
                    or PaletteBackStyle.HeaderCustom2
                    or PaletteBackStyle.HeaderCustom3
                    or PaletteBackStyle.TabHighProfile
                    or PaletteBackStyle.TabStandardProfile
                    or PaletteBackStyle.TabLowProfile
                    or PaletteBackStyle.TabOneNote
                    or PaletteBackStyle.TabDock
                    or PaletteBackStyle.TabDockAutoHidden
                    or PaletteBackStyle.TabCustom1
                    or PaletteBackStyle.TabCustom2
                    or PaletteBackStyle.TabCustom3
                    or PaletteBackStyle.ButtonStandalone
                    or PaletteBackStyle.ButtonGallery
                    or PaletteBackStyle.ButtonAlternate
                    or PaletteBackStyle.ButtonLowProfile
                    or PaletteBackStyle.ButtonBreadCrumb
                    or PaletteBackStyle.ButtonListItem
                    or PaletteBackStyle.ButtonCommand
                    or PaletteBackStyle.ButtonButtonSpec
                    or PaletteBackStyle.ButtonCalendarDay
                    or PaletteBackStyle.ButtonCluster
                    or PaletteBackStyle.ButtonNavigatorStack
                    or PaletteBackStyle.ButtonNavigatorOverflow
                    or PaletteBackStyle.ButtonNavigatorMini
                    or PaletteBackStyle.ButtonForm
                    or PaletteBackStyle.ButtonFormClose
                    or PaletteBackStyle.ButtonCustom1
                    or PaletteBackStyle.ButtonCustom2
                    or PaletteBackStyle.ButtonCustom3
                    or PaletteBackStyle.ButtonInputControl
                    or PaletteBackStyle.ContextMenuItemImage
                    or PaletteBackStyle.ContextMenuItemHighlight
                    or PaletteBackStyle.GridBackgroundList
                    or PaletteBackStyle.GridBackgroundSheet
                                        or PaletteBackStyle.GridBackgroundCustom1
                    or PaletteBackStyle.GridBackgroundCustom2
                    or PaletteBackStyle.GridBackgroundCustom3

                    or PaletteBackStyle.GridBackgroundCustom2
                    or PaletteBackStyle.GridBackgroundCustom3
                    or PaletteBackStyle.GridHeaderColumnList
                    or PaletteBackStyle.GridHeaderColumnSheet
                    or PaletteBackStyle.GridHeaderColumnCustom1
                    or PaletteBackStyle.GridHeaderColumnCustom2
                    or PaletteBackStyle.GridHeaderColumnCustom3
                    or PaletteBackStyle.GridHeaderRowList
                    or PaletteBackStyle.GridHeaderRowSheet
                    or PaletteBackStyle.GridHeaderRowCustom1
                    or PaletteBackStyle.GridHeaderRowCustom2
                    or PaletteBackStyle.GridHeaderRowCustom3
                    or PaletteBackStyle.GridDataCellList
                    or PaletteBackStyle.GridDataCellSheet
                    or PaletteBackStyle.GridDataCellCustom1
                    or PaletteBackStyle.GridDataCellCustom2
                    or PaletteBackStyle.GridDataCellCustom3 => PaletteImageStyle.Tile,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the image alignment.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetBackImageAlign(PaletteBackStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteRectangleAlign.Inherit;
            }

            return style switch
            {
                PaletteBackStyle.PanelClient
                    or PaletteBackStyle.PanelRibbonInactive
                    or PaletteBackStyle.PanelAlternate
                    or PaletteBackStyle.PanelCustom1
                    or PaletteBackStyle.PanelCustom2
                    or PaletteBackStyle.PanelCustom3
                    or PaletteBackStyle.SeparatorLowProfile
                    or PaletteBackStyle.SeparatorHighInternalProfile
                    or PaletteBackStyle.SeparatorHighProfile
                    or PaletteBackStyle.SeparatorCustom1
                    or PaletteBackStyle.SeparatorCustom2
                    or PaletteBackStyle.SeparatorCustom3
                    or PaletteBackStyle.ControlClient
                    or PaletteBackStyle.ControlAlternate
                    or PaletteBackStyle.ControlGroupBox
                    or PaletteBackStyle.ControlToolTip
                    or PaletteBackStyle.ControlRibbon
                    or PaletteBackStyle.ControlRibbonAppMenu
                    or PaletteBackStyle.ControlCustom1
                    or PaletteBackStyle.ControlCustom2
                    or PaletteBackStyle.ControlCustom3
                    or PaletteBackStyle.ContextMenuOuter
                    or PaletteBackStyle.ContextMenuInner
                    or PaletteBackStyle.ContextMenuHeading
                    or PaletteBackStyle.ContextMenuSeparator
                    or PaletteBackStyle.ContextMenuItemSplit
                    or PaletteBackStyle.ContextMenuItemImageColumn
                    or PaletteBackStyle.InputControlStandalone
                    or PaletteBackStyle.InputControlRibbon
                    or PaletteBackStyle.InputControlCustom1
                    or PaletteBackStyle.InputControlCustom2
                    or PaletteBackStyle.InputControlCustom3
                    or PaletteBackStyle.FormMain
                    or PaletteBackStyle.FormCustom1
                    or PaletteBackStyle.FormCustom2
                    or PaletteBackStyle.FormCustom3
                    or PaletteBackStyle.HeaderPrimary
                    or PaletteBackStyle.HeaderDockInactive
                    or PaletteBackStyle.HeaderDockActive
                    or PaletteBackStyle.HeaderCalendar
                    or PaletteBackStyle.HeaderSecondary
                    or PaletteBackStyle.HeaderForm
                    or PaletteBackStyle.HeaderCustom1
                    or PaletteBackStyle.HeaderCustom2
                    or PaletteBackStyle.HeaderCustom3
                    or PaletteBackStyle.TabHighProfile
                    or PaletteBackStyle.TabStandardProfile
                    or PaletteBackStyle.TabLowProfile
                    or PaletteBackStyle.TabOneNote
                    or PaletteBackStyle.TabDock
                    or PaletteBackStyle.TabDockAutoHidden
                    or PaletteBackStyle.TabCustom1
                    or PaletteBackStyle.TabCustom2
                    or PaletteBackStyle.TabCustom3
                    or PaletteBackStyle.ButtonStandalone
                    or PaletteBackStyle.ButtonGallery
                    or PaletteBackStyle.ButtonAlternate
                    or PaletteBackStyle.ButtonLowProfile
                    or PaletteBackStyle.ButtonBreadCrumb
                    or PaletteBackStyle.ButtonListItem
                    or PaletteBackStyle.ButtonCommand
                    or PaletteBackStyle.ButtonButtonSpec
                    or PaletteBackStyle.ButtonCalendarDay
                    or PaletteBackStyle.ButtonCluster
                    or PaletteBackStyle.ButtonNavigatorStack
                    or PaletteBackStyle.ButtonNavigatorOverflow
                    or PaletteBackStyle.ButtonNavigatorMini
                    or PaletteBackStyle.ButtonForm
                    or PaletteBackStyle.ButtonFormClose
                    or PaletteBackStyle.ButtonCustom1
                    or PaletteBackStyle.ButtonCustom2
                    or PaletteBackStyle.ButtonCustom3
                    or PaletteBackStyle.ButtonInputControl
                    or PaletteBackStyle.ContextMenuItemImage
                    or PaletteBackStyle.ContextMenuItemHighlight
                    or PaletteBackStyle.GridBackgroundList
                    or PaletteBackStyle.GridBackgroundSheet
                                        or PaletteBackStyle.GridBackgroundCustom1
                    or PaletteBackStyle.GridBackgroundCustom2
                    or PaletteBackStyle.GridBackgroundCustom3

                    or PaletteBackStyle.GridBackgroundCustom2
                    or PaletteBackStyle.GridBackgroundCustom3
                    or PaletteBackStyle.GridHeaderColumnList
                    or PaletteBackStyle.GridHeaderColumnSheet
                    or PaletteBackStyle.GridHeaderColumnCustom1
                    or PaletteBackStyle.GridHeaderColumnCustom2
                    or PaletteBackStyle.GridHeaderColumnCustom3
                    or PaletteBackStyle.GridHeaderRowList
                    or PaletteBackStyle.GridHeaderRowSheet
                    or PaletteBackStyle.GridHeaderRowCustom1
                    or PaletteBackStyle.GridHeaderRowCustom2
                    or PaletteBackStyle.GridHeaderRowCustom3
                    or PaletteBackStyle.GridDataCellList
                    or PaletteBackStyle.GridDataCellSheet
                    or PaletteBackStyle.GridDataCellCustom1
                    or PaletteBackStyle.GridDataCellCustom2
                    or PaletteBackStyle.GridDataCellCustom3 => PaletteRectangleAlign.Local,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }
        #endregion

        #region Border

        /// <summary>
        /// Gets a value indicating if border should be drawn.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetBorderDraw(PaletteBorderStyle style, PaletteState state)
        {
            // Check for the calendar day today override
            if (state == PaletteState.TodayOverride)
            {
                if (style == PaletteBorderStyle.ButtonCalendarDay)
                {
                    return InheritBool.True;
                }
            }

            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return InheritBool.Inherit;
            }

            return style switch
            {
                PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ContextMenuInner => InheritBool.False,
                PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => InheritBool.True,
                PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose => state switch
                {
                    PaletteState.Disabled or PaletteState.Normal or PaletteState.NormalDefaultOverride => InheritBool.False,
                    _ => InheritBool.True
                },
                PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight => state switch
                {
                    PaletteState.Normal or PaletteState.NormalDefaultOverride => InheritBool.False,
                    _ => InheritBool.True
                },
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets a value indicating which borders to draw.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>PaletteDrawBorders value.</returns>
        public override PaletteDrawBorders GetBorderDrawBorders(PaletteBorderStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteDrawBorders.Inherit;
            }

            return style switch
            {
                PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => PaletteDrawBorders.All,
                PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 => PaletteDrawBorders.All,
                PaletteBorderStyle.ContextMenuHeading => PaletteDrawBorders.Bottom,
                PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit => PaletteDrawBorders.Top,
                PaletteBorderStyle.ContextMenuItemImageColumn => PaletteDrawBorders.Right,
                PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ContextMenuInner => PaletteDrawBorders.None,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the graphics drawing hint for the border.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>PaletteGraphicsHint value.</returns>
        public override PaletteGraphicsHint GetBorderGraphicsHint(PaletteBorderStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteGraphicsHint.Inherit;
            }

            return style switch
            {
                PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => PaletteGraphicsHint.AntiAlias,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the first border color.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Color value.</returns>
        public override Color GetBorderColor1(PaletteBorderStyle style, PaletteState state)
        {
            if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
            {
                // Check for the calendar day today override
                if (state == PaletteState.TodayOverride)
                {
                    if (style == PaletteBorderStyle.ButtonCalendarDay)
                    {
                        return state == PaletteState.Disabled ? _disabledBorder : _todayBorder;
                    }
                }

                return GlobalStaticValues.EMPTY_COLOR;
            }

            return style switch
            {
                PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 => state switch
                {
                    PaletteState.Disabled => style == PaletteBorderStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : _disabledBorder,
                    PaletteState.Normal or PaletteState.Tracking or PaletteState.Pressed => style == PaletteBorderStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBorder],
                    PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => _ribbonColours[(int)SchemeOfficeColors.ControlBorder],
                    _ => throw DebugTools.NotImplemented(state.ToString())
                },
                PaletteBorderStyle.TabDock => state switch
                {
                    PaletteState.Disabled => _disabledBorder,
                    PaletteState.Normal => _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBorder],
                    PaletteState.Tracking or PaletteState.Pressed => _buttonBorderColors[2],
                    PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => _ribbonColours[(int)SchemeOfficeColors.ControlBorder],
                    _ => throw DebugTools.NotImplemented(state.ToString())
                },
                PaletteBorderStyle.TabDockAutoHidden => state switch
                {
                    PaletteState.Disabled => _disabledBorder,
                    PaletteState.Normal or PaletteState.CheckedNormal => _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBorder],
                    PaletteState.Tracking or PaletteState.CheckedTracking or PaletteState.Pressed or PaletteState.CheckedPressed => _buttonBorderColors[2],
                    _ => throw DebugTools.NotImplemented(state.ToString())
                },
                PaletteBorderStyle.HeaderCalendar => state == PaletteState.Disabled
                                                    ? _ribbonColours[(int)SchemeOfficeColors.HeaderPrimaryBack1]
                                                    : _ribbonColours[(int)SchemeOfficeColors.HeaderPrimaryBack2],
                PaletteBorderStyle.HeaderForm => state == PaletteState.Disabled
                                                ? _ribbonColours[(int)SchemeOfficeColors.FormBorderHeaderInactive]
                                                : _ribbonColours[(int)SchemeOfficeColors.FormBorderHeaderActive],
                PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 => state == PaletteState.Disabled ? _disabledBorder : _ribbonColours[(int)SchemeOfficeColors.ControlBorder],
                PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuItemImageColumn => _contextMenuHeadingBorder,
                PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit => state switch
                {
                    PaletteState.Disabled => _buttonBorderColors[0],
                    PaletteState.Tracking => _buttonBorderColors[1],
                    _ => _contextMenuHeadingBorder
                },
                PaletteBorderStyle.ContextMenuItemImage => _contextMenuImageBorderChecked,
                PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 => state == PaletteState.Disabled
? _ribbonColours[(int)SchemeOfficeColors.InputControlBorderDisabled]
: _ribbonColours[(int)SchemeOfficeColors.InputControlBorderNormal],
                PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => state == PaletteState.Disabled ? _disabledBorder : _ribbonColours[(int)SchemeOfficeColors.GridDataCellBorder],
                PaletteBorderStyle.ControlRibbon => state == PaletteState.Disabled ? _disabledBorder : _ribbonColours[(int)SchemeOfficeColors.RibbonGroupsArea5],
                PaletteBorderStyle.ControlRibbonAppMenu => state == PaletteState.Disabled ? _disabledBorder : _ribbonColours[(int)SchemeOfficeColors.AppButtonBorder],
                PaletteBorderStyle.ContextMenuOuter => _contextMenuBorder,
                PaletteBorderStyle.ContextMenuInner => _contextMenuInnerBack,
                PaletteBorderStyle.ControlToolTip => state == PaletteState.Disabled ? _disabledBorder : _toolTipBorder,
                PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 => state == PaletteState.Disabled
                                                    ? _ribbonColours[(int)SchemeOfficeColors.FormBorderInactive]
                                                    : _ribbonColours[(int)SchemeOfficeColors.FormBorderActive],
                PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose => state switch
                {
                    PaletteState.Disabled or PaletteState.Normal or PaletteState.NormalDefaultOverride => GlobalStaticValues.EMPTY_COLOR,
                    PaletteState.CheckedNormal => _ribbonColours[(int)SchemeOfficeColors.FormButtonBorderCheck],
                    PaletteState.Tracking or PaletteState.CheckedTracking => _ribbonColours[(int)SchemeOfficeColors.FormButtonBorderTrack],
                    PaletteState.Pressed or PaletteState.CheckedPressed => _ribbonColours[(int)SchemeOfficeColors.FormButtonBorderPressed],
                    _ => throw DebugTools.NotImplemented(state.ToString())
                },
                PaletteBorderStyle.ButtonGallery => state switch
                {
                    PaletteState.Disabled => _disabledBorder,
                    PaletteState.Normal or PaletteState.NormalDefaultOverride or PaletteState.CheckedNormal => _ribbonColours[(int)SchemeOfficeColors.RibbonGalleryBorder],
                    PaletteState.Tracking or PaletteState.CheckedTracking => _buttonBorderColors[1],
                    PaletteState.Pressed or PaletteState.CheckedPressed => _buttonBorderColors[3],
                    _ => throw DebugTools.NotImplemented(state.ToString())
                },
                PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ContextMenuItemHighlight => state switch
                {
                    PaletteState.Disabled => _buttonBorderColors[0],
                    PaletteState.Normal => style == PaletteBorderStyle.ButtonCluster
                                         ? _ribbonColours[(int)SchemeOfficeColors.ButtonClusterButtonBorder1]
                                         : _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBorder],
                    PaletteState.NormalDefaultOverride => style is PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ContextMenuItemHighlight
                                                            ? GlobalStaticValues.EMPTY_COLOR
                                                            : _ribbonColours[(int)SchemeOfficeColors.ButtonNormalDefaultBorder],
                    PaletteState.CheckedNormal => _buttonBorderColors[5],
                    PaletteState.Tracking => _buttonBorderColors[1],
                    PaletteState.Pressed or PaletteState.CheckedPressed => style == PaletteBorderStyle.ButtonAlternate
                                            ? _ribbonColours[(int)SchemeOfficeColors.AlternatePressedBorder1]
                                            : _buttonBorderColors[3],
                    PaletteState.CheckedTracking => _buttonBorderColors[3],
                    _ => throw DebugTools.NotImplemented(state.ToString())
                },
                PaletteBorderStyle.ButtonCalendarDay => state switch
                {
                    PaletteState.Disabled => _disabledBack,
                    PaletteState.Normal => _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBack1],
                    PaletteState.NormalDefaultOverride => _ribbonColours[(int)SchemeOfficeColors.ButtonNormalDefaultBack1],
                    PaletteState.CheckedNormal => _buttonBackColors[6],
                    PaletteState.Tracking => _buttonBackColors[2],
                    PaletteState.Pressed or PaletteState.CheckedPressed => _buttonBackColors[4],
                    PaletteState.CheckedTracking => _buttonBackColors[8],
                    _ => throw DebugTools.NotImplemented(state.ToString())
                },
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the second border color.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Color value.</returns>
        public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
        {
            if (CommonHelper.IsOverrideState(state))
            {
                // Check for the calendar day today override
                if (state == PaletteState.TodayOverride)
                {
                    if (style == PaletteBorderStyle.ButtonCalendarDay)
                    {
                        return state == PaletteState.Disabled ? _disabledBorder : _todayBorder;
                    }
                }

                return GlobalStaticValues.EMPTY_COLOR;
            }

            return style switch
            {
                PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 => state switch
                {
                    PaletteState.Disabled => style == PaletteBorderStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : _disabledBorder,
                    PaletteState.Normal or PaletteState.Tracking or PaletteState.Pressed => style == PaletteBorderStyle.TabLowProfile ? GlobalStaticValues.EMPTY_COLOR : _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBorder],
                    PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => _ribbonColours[(int)SchemeOfficeColors.ControlBorder],
                    _ => throw DebugTools.NotImplemented(state.ToString())
                },
                PaletteBorderStyle.TabDock => state switch
                {
                    PaletteState.Disabled => _disabledBorder,
                    PaletteState.Normal => _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBorder],
                    PaletteState.Tracking or PaletteState.Pressed => _buttonBorderColors[2],
                    PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => _ribbonColours[(int)SchemeOfficeColors.ControlBorder],
                    _ => throw DebugTools.NotImplemented(state.ToString())
                },
                PaletteBorderStyle.TabDockAutoHidden => state switch
                {
                    PaletteState.Disabled => _disabledBorder,
                    PaletteState.Normal or PaletteState.CheckedNormal => _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBorder],
                    PaletteState.Tracking or PaletteState.CheckedTracking or PaletteState.Pressed or PaletteState.CheckedPressed => _buttonBorderColors[2],
                    _ => throw DebugTools.NotImplemented(state.ToString())
                },
                PaletteBorderStyle.HeaderForm => state == PaletteState.Disabled
                                                    ? _ribbonColours[(int)SchemeOfficeColors.FormBorderHeaderInactive]
                                                    : _ribbonColours[(int)SchemeOfficeColors.FormBorderHeaderActive],
                PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 => state == PaletteState.Disabled ? _disabledBorder : _ribbonColours[(int)SchemeOfficeColors.ControlBorder],
                PaletteBorderStyle.HeaderCalendar => state == PaletteState.Disabled
                                                    ? _ribbonColours[(int)SchemeOfficeColors.HeaderPrimaryBack1]
                                                    : _ribbonColours[(int)SchemeOfficeColors.HeaderPrimaryBack2],
                PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuItemImageColumn => _contextMenuHeadingBorder,
                PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit => state switch
                {
                    PaletteState.Disabled => _buttonBorderColors[0],
                    PaletteState.Tracking => _buttonBorderColors[2],
                    _ => _contextMenuHeadingBorder
                },
                PaletteBorderStyle.ContextMenuItemImage => _contextMenuImageBorderChecked,
                PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 => state == PaletteState.Disabled
? _ribbonColours[(int)SchemeOfficeColors.InputControlBorderDisabled]
: _ribbonColours[(int)SchemeOfficeColors.InputControlBorderNormal],
                PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => state == PaletteState.Disabled ? _disabledBorder : _ribbonColours[(int)SchemeOfficeColors.GridDataCellBorder],
                PaletteBorderStyle.ControlRibbon => state == PaletteState.Disabled ? _disabledBorder : _ribbonColours[(int)SchemeOfficeColors.RibbonGroupsArea5],
                PaletteBorderStyle.ControlRibbonAppMenu => state == PaletteState.Disabled ? _disabledBorder : _ribbonColours[(int)SchemeOfficeColors.AppButtonBorder],
                PaletteBorderStyle.ContextMenuOuter => _contextMenuBorder,
                PaletteBorderStyle.ContextMenuInner => _contextMenuInnerBack,
                PaletteBorderStyle.ControlToolTip => state == PaletteState.Disabled ? _disabledBorder : _toolTipBorder,
                PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 => state == PaletteState.Disabled
                                                    ? _ribbonColours[(int)SchemeOfficeColors.FormBorderInactive]
                                                    : _ribbonColours[(int)SchemeOfficeColors.FormBorderActive],
                PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose => state switch
                {
                    PaletteState.Disabled or PaletteState.Normal or PaletteState.NormalDefaultOverride => GlobalStaticValues.EMPTY_COLOR,
                    PaletteState.CheckedNormal => _ribbonColours[(int)SchemeOfficeColors.FormButtonBorderCheck],
                    PaletteState.Tracking or PaletteState.CheckedTracking => _ribbonColours[(int)SchemeOfficeColors.FormButtonBorderTrack],
                    PaletteState.Pressed or PaletteState.CheckedPressed => _ribbonColours[(int)SchemeOfficeColors.FormButtonBorderPressed],
                    _ => throw DebugTools.NotImplemented(state.ToString())
                },
                PaletteBorderStyle.ButtonGallery => state switch
                {
                    PaletteState.Disabled => _disabledBorder,
                    PaletteState.Normal or PaletteState.NormalDefaultOverride or PaletteState.CheckedNormal => _ribbonColours[(int)SchemeOfficeColors.RibbonGalleryBorder],
                    PaletteState.Tracking or PaletteState.CheckedTracking => _buttonBorderColors[1],
                    PaletteState.Pressed or PaletteState.CheckedPressed => _buttonBorderColors[3],
                    _ => throw DebugTools.NotImplemented(state.ToString())
                },
                PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ContextMenuItemHighlight => state switch
                {
                    PaletteState.Disabled => _buttonBorderColors[0],
                    PaletteState.Normal => style == PaletteBorderStyle.ButtonCluster
                                             ? _ribbonColours[(int)SchemeOfficeColors.ButtonClusterButtonBorder2]
                                             : _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBorder],
                    PaletteState.NormalDefaultOverride => _ribbonColours[(int)SchemeOfficeColors.ButtonNormalDefaultBorder],
                    PaletteState.CheckedNormal => _buttonBorderColors[6],
                    PaletteState.Tracking => _buttonBorderColors[2],
                    PaletteState.Pressed or PaletteState.CheckedPressed => style == PaletteBorderStyle.ButtonAlternate
                                            ? _ribbonColours[(int)SchemeOfficeColors.AlternatePressedBorder2]
                                            : _buttonBorderColors[4],
                    PaletteState.CheckedTracking => _buttonBorderColors[4],
                    _ => throw DebugTools.NotImplemented(state.ToString())
                },
                PaletteBorderStyle.ButtonCalendarDay => state switch
                {
                    PaletteState.Disabled => _disabledBack,
                    PaletteState.Normal => _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBack1],
                    PaletteState.NormalDefaultOverride => _ribbonColours[(int)SchemeOfficeColors.ButtonNormalDefaultBack1],
                    PaletteState.CheckedNormal => _buttonBackColors[6],
                    PaletteState.Tracking => _buttonBackColors[2],
                    PaletteState.Pressed or PaletteState.CheckedPressed => _buttonBackColors[4],
                    PaletteState.CheckedTracking => _buttonBackColors[8],
                    _ => throw DebugTools.NotImplemented(state.ToString())
                },
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the color border drawing style.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetBorderColorStyle(PaletteBorderStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
            {
                return PaletteColorStyle.Inherit;
            }

            return style switch
            {
                PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 => PaletteColorStyle.Sigma,
                PaletteBorderStyle.TabDock => state switch
                {
                    PaletteState.Tracking or PaletteState.Pressed => PaletteColorStyle.Solid,
                    _ => PaletteColorStyle.Sigma
                },
                PaletteBorderStyle.TabDockAutoHidden => state switch
                {
                    PaletteState.Tracking or PaletteState.CheckedTracking or PaletteState.Pressed or PaletteState.CheckedPressed => PaletteColorStyle.Solid,
                    _ => PaletteColorStyle.Sigma
                },
                PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.ButtonCalendarDay => PaletteColorStyle.Solid,
                PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit => state == PaletteState.Tracking ? PaletteColorStyle.Sigma : PaletteColorStyle.Solid,
                PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ContextMenuItemHighlight => state switch
                {
                    PaletteState.Normal => style == PaletteBorderStyle.ButtonCluster ? PaletteColorStyle.Sigma : PaletteColorStyle.Solid,
                    PaletteState.Disabled or PaletteState.NormalDefaultOverride => PaletteColorStyle.Solid,
                    PaletteState.Pressed or PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed => PaletteColorStyle.Linear,
                    _ => PaletteColorStyle.Sigma
                },
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the color border alignment.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetBorderColorAlign(PaletteBorderStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteRectangleAlign.Inherit;
            }

            return style switch
            {
                PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 => PaletteRectangleAlign.Control,
                PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit => PaletteRectangleAlign.Local,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the color border angle.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetBorderColorAngle(PaletteBorderStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return -1f;
            }

            return style switch
            {
                PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => 90f,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the border width.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Integer width.</returns>
        public override int GetBorderWidth(PaletteBorderStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return -1;
            }

            return style switch
            {
                PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.ContextMenuInner => 0,
                PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => 1,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the border corner rounding.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Float rounding.</returns>
        public override float GetBorderRounding(PaletteBorderStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return GlobalStaticValues.DEFAULT_PRIMARY_CORNER_ROUNDING_VALUE;
            }

            return style switch
            {
                PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 or PaletteBorderStyle.ButtonCalendarDay => 0,
                PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ContextMenuItemImage => 1,
                PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuItemHighlight => 2,
                PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlGroupBox => 3,
                PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 => 5,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets a border image.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Image instance.</returns>
        public override Image? GetBorderImage(PaletteBorderStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return null;
            }

            return style switch
            {
                PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => null,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the border image style.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetBorderImageStyle(PaletteBorderStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteImageStyle.Inherit;
            }

            return style switch
            {
                PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom1 or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => PaletteImageStyle.Tile,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the image border alignment.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetBorderImageAlign(PaletteBorderStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteRectangleAlign.Inherit;
            }

            return style switch
            {
                PaletteBorderStyle.SeparatorLowProfile or PaletteBorderStyle.SeparatorHighInternalProfile or PaletteBorderStyle.SeparatorHighProfile or PaletteBorderStyle.SeparatorCustom1 or PaletteBorderStyle.SeparatorCustom2 or PaletteBorderStyle.SeparatorCustom3 or PaletteBorderStyle.ControlClient or PaletteBorderStyle.ControlAlternate or PaletteBorderStyle.ControlGroupBox or PaletteBorderStyle.ControlToolTip or PaletteBorderStyle.ControlRibbon or PaletteBorderStyle.ControlRibbonAppMenu or PaletteBorderStyle.ControlCustom1 or PaletteBorderStyle.ControlCustom2 or PaletteBorderStyle.ControlCustom3 or PaletteBorderStyle.ContextMenuOuter or PaletteBorderStyle.ContextMenuInner or PaletteBorderStyle.ContextMenuHeading or PaletteBorderStyle.ContextMenuSeparator or PaletteBorderStyle.ContextMenuItemSplit or PaletteBorderStyle.ContextMenuItemImage or PaletteBorderStyle.ContextMenuItemImageColumn or PaletteBorderStyle.ContextMenuItemHighlight or PaletteBorderStyle.InputControlStandalone or PaletteBorderStyle.InputControlRibbon or PaletteBorderStyle.InputControlCustom1 or PaletteBorderStyle.InputControlCustom2 or PaletteBorderStyle.InputControlCustom3 or PaletteBorderStyle.FormMain or PaletteBorderStyle.FormCustom2 or PaletteBorderStyle.FormCustom3 or PaletteBorderStyle.HeaderPrimary or PaletteBorderStyle.HeaderDockInactive or PaletteBorderStyle.HeaderDockActive or PaletteBorderStyle.HeaderCalendar or PaletteBorderStyle.HeaderSecondary or PaletteBorderStyle.HeaderForm or PaletteBorderStyle.HeaderCustom1 or PaletteBorderStyle.HeaderCustom2 or PaletteBorderStyle.HeaderCustom3 or PaletteBorderStyle.TabHighProfile or PaletteBorderStyle.TabStandardProfile or PaletteBorderStyle.TabLowProfile or PaletteBorderStyle.TabOneNote or PaletteBorderStyle.TabDock or PaletteBorderStyle.TabDockAutoHidden or PaletteBorderStyle.TabCustom1 or PaletteBorderStyle.TabCustom2 or PaletteBorderStyle.TabCustom3 or PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonGallery or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonListItem or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay or PaletteBorderStyle.ButtonCluster or PaletteBorderStyle.ButtonNavigatorStack or PaletteBorderStyle.ButtonNavigatorOverflow or PaletteBorderStyle.ButtonNavigatorMini or PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 or PaletteBorderStyle.ButtonInputControl or PaletteBorderStyle.GridHeaderColumnList or PaletteBorderStyle.GridHeaderColumnSheet or PaletteBorderStyle.GridHeaderColumnCustom1 or PaletteBorderStyle.GridHeaderColumnCustom2 or PaletteBorderStyle.GridHeaderColumnCustom3 or PaletteBorderStyle.GridHeaderRowList or PaletteBorderStyle.GridHeaderRowSheet or PaletteBorderStyle.GridHeaderRowCustom1 or PaletteBorderStyle.GridHeaderRowCustom2 or PaletteBorderStyle.GridHeaderRowCustom3 or PaletteBorderStyle.GridDataCellList or PaletteBorderStyle.GridDataCellSheet or PaletteBorderStyle.GridDataCellCustom1 or PaletteBorderStyle.GridDataCellCustom2 or PaletteBorderStyle.GridDataCellCustom3 => PaletteRectangleAlign.Local,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }
        #endregion

        #region Content
        /// <summary>
        /// Gets a value indicating if content should be drawn.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetContentDraw(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return InheritBool.Inherit;
            }

            // Always draw everything
            return InheritBool.True;
        }

        /// <summary>
        /// Gets a value indicating if content should be drawn with focus indication.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetContentDrawFocus(PaletteContentStyle style, PaletteState state)
        {
            // By default, the focus override shows the focus!
            if (state == PaletteState.FocusOverride)
            {
                return InheritBool.True;
            }

            // We do not override the other override states
            if (CommonHelper.IsOverrideState(state))
            {
                return InheritBool.Inherit;
            }

            // By default, never show the focus indication, we let individual controls
            // override this functionality as required by the controls requirements
            return InheritBool.False;
        }

        /// <summary>
        /// Gets the horizontal relative alignment of the image.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentImageH(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteRelativeAlign.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRelativeAlign.Near,
                PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText => PaletteRelativeAlign.Center,
                PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl => PaletteRelativeAlign.Center,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the vertical relative alignment of the image.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentImageV(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteRelativeAlign.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRelativeAlign.Center,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the effect applied to drawing of the image.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>PaletteImageEffect value.</returns>
        public override PaletteImageEffect GetContentImageEffect(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteImageEffect.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => state == PaletteState.Disabled ? PaletteImageEffect.Disabled : PaletteImageEffect.Normal,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the image color to remap into another color.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Color value.</returns>
        public override Color GetContentImageColorMap(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return GlobalStaticValues.EMPTY_COLOR;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => GlobalStaticValues.EMPTY_COLOR,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the color to use in place of the image map color.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Color value.</returns>
        public override Color GetContentImageColorTo(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return GlobalStaticValues.EMPTY_COLOR;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => GlobalStaticValues.EMPTY_COLOR,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the image color that should be transparent.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Color value.</returns>
        public override Color GetContentImageColorTransparent(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return GlobalStaticValues.EMPTY_COLOR;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => GlobalStaticValues.EMPTY_COLOR,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the font for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Font value.</returns>
        public override Font? GetContentShortTextFont(PaletteContentStyle style, PaletteState state)
        {
            if (CommonHelper.IsOverrideState(state))
            {
                return (state == PaletteState.BoldedOverride) && (style == PaletteContentStyle.ButtonCalendarDay) ? CalendarBoldFont : null;
            }

            return style switch
            {
                PaletteContentStyle.HeaderForm => HeaderFormFont,
                PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.ButtonCommand => Header1ShortFont,
                PaletteContentStyle.LabelSuperTip or PaletteContentStyle.ContextMenuHeading => SuperToolFont,
                PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemShortcutText => Header2ShortFont,
                PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelBoldPanel => BoldFont,
                PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelItalicPanel => ItalicFont,
                PaletteContentStyle.ContextMenuItemTextAlternate => SuperToolFont,
                PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden => TabFontNormal,
                PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 => state switch
                {
                    PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => TabFontSelected,
                    _ => TabFontNormal
                },
                PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl => ButtonFont,
                PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow => ButtonFontNavigatorStack,
                PaletteContentStyle.ButtonNavigatorMini => ButtonFontNavigatorMini,
                PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 or PaletteContentStyle.HeaderCalendar => GridFont,
                PaletteContentStyle.ButtonCalendarDay => CalendarFont,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the font for the short text by generating a new font instance.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font? GetContentShortTextNewFont(PaletteContentStyle style, PaletteState state)
        {
            DefineFonts();
            return GetContentShortTextFont(style, state);
        }

        /// <summary>
        /// Gets the rendering hint for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>PaletteTextHint value.</returns>
        public override PaletteTextHint GetContentShortTextHint(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteTextHint.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteTextHint.ClearTypeGridFit,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the prefix drawing setting for short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>PaletteTextPrefix value.</returns>
        public override PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteTextHotkeyPrefix.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.HeaderForm => PaletteTextHotkeyPrefix.Show,
                PaletteContentStyle.ButtonListItem or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemShortcutText => PaletteTextHotkeyPrefix.None,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the flag indicating if multiline text is allowed for short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetContentShortTextMultiLine(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return InheritBool.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => InheritBool.True,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the text trimming to use for short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>PaletteTextTrim value.</returns>
        public override PaletteTextTrim GetContentShortTextTrim(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteTextTrim.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteTextTrim.EllipsisCharacter,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the horizontal relative alignment of the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentShortTextH(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteRelativeAlign.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRelativeAlign.Near,
                PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.HeaderCalendar => PaletteRelativeAlign.Center,
                PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl => PaletteRelativeAlign.Center,
                PaletteContentStyle.ContextMenuItemShortcutText => PaletteRelativeAlign.Far,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the vertical relative alignment of the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentShortTextV(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteRelativeAlign.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRelativeAlign.Center,
                PaletteContentStyle.LabelSuperTip => PaletteRelativeAlign.Near,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the horizontal relative alignment of multiline short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteRelativeAlign.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRelativeAlign.Near,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
        {
            // Always work out value for an override state
            if (CommonHelper.IsOverrideState(state))
            {
                return style switch
                {
                    PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl => state switch
                    {
                        PaletteState.LinkNotVisitedOverride => _ribbonColours[
                            (int)SchemeOfficeColors.LinkNotVisitedOverrideControl],
                        PaletteState.LinkVisitedOverride => _ribbonColours[
                            (int)SchemeOfficeColors.LinkVisitedOverrideControl],
                        PaletteState.LinkPressedOverride => _ribbonColours[
                            (int)SchemeOfficeColors.LinkPressedOverrideControl],
                        _ => GlobalStaticValues.EMPTY_COLOR
                    },
                    PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption => state switch
                    {
                        PaletteState.LinkNotVisitedOverride => _ribbonColours[
                            (int)SchemeOfficeColors.LinkNotVisitedOverridePanel],
                        PaletteState.LinkVisitedOverride => _ribbonColours[
                            (int)SchemeOfficeColors.LinkVisitedOverridePanel],
                        PaletteState.LinkPressedOverride => _ribbonColours[
                            (int)SchemeOfficeColors.LinkPressedOverridePanel],
                        _ => GlobalStaticValues.EMPTY_COLOR
                    },
                    _ => GlobalStaticValues.EMPTY_COLOR
                };
            }

            switch (style)
            {
                case PaletteContentStyle.HeaderForm:
                    return state == PaletteState.Disabled
                        ? _ribbonColours[(int)SchemeOfficeColors.FormHeaderShortInactive]
                        : _ribbonColours[(int)SchemeOfficeColors.FormHeaderShortActive];
            }

            if ((state == PaletteState.Disabled) &&
                (style != PaletteContentStyle.LabelToolTip) &&
                (style != PaletteContentStyle.LabelSuperTip) &&
                (style != PaletteContentStyle.LabelKeyTip) &&
                (style != PaletteContentStyle.InputControlStandalone) &&
                (style != PaletteContentStyle.InputControlRibbon) &&
                (style != PaletteContentStyle.InputControlCustom1) &&
                (style != PaletteContentStyle.InputControlCustom2) &&
                (style != PaletteContentStyle.InputControlCustom3) &&
                (style != PaletteContentStyle.ButtonInputControl) &&
                (style != PaletteContentStyle.ButtonCalendarDay))
            {
                return _disabledText;
            }

            return style switch
            {
                PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 or PaletteContentStyle.HeaderCalendar => _gridTextColor,
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 => _ribbonColours[(int)SchemeOfficeColors.HeaderText],
                PaletteContentStyle.HeaderDockActive => Color.Black,
                PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 => state == PaletteState.Disabled
? _ribbonColours[(int)SchemeOfficeColors.InputControlTextDisabled]
: _ribbonColours[(int)SchemeOfficeColors.InputControlTextNormal],
                PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption => _ribbonColours[(int)SchemeOfficeColors.TextLabelPanel],
                PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.ContextMenuItemTextAlternate => _ribbonColours[(int)SchemeOfficeColors.TextLabelControl],
                PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip => _toolTipText,
                PaletteContentStyle.ContextMenuHeading => _ribbonColours[(int)SchemeOfficeColors.ContextMenuHeadingText],
                PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 => (state & PaletteState.Checked) == PaletteState.Checked
? _ribbonColours[(int)SchemeOfficeColors.TextButtonChecked]
: _ribbonColours[(int)SchemeOfficeColors.TextButtonNormal],
                PaletteContentStyle.TabDockAutoHidden => _ribbonColours[(int)SchemeOfficeColors.TextButtonNormal],
                PaletteContentStyle.ButtonCalendarDay => state == PaletteState.Disabled ? _disabledText2 : Color.Black,
                PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonButtonSpec => state switch
                {
                    PaletteState.Normal => style == PaletteContentStyle.ButtonListItem
                                                   ? _ribbonColours[(int)SchemeOfficeColors.TextLabelControl]
                                                   : _ribbonColours[(int)SchemeOfficeColors.TextLabelPanel],
                    PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed => _ribbonColours[(int)SchemeOfficeColors.TextButtonChecked],
                    _ => _ribbonColours[(int)SchemeOfficeColors.TextButtonNormal]
                },
                PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose => state switch
                {
                    PaletteState.Tracking or PaletteState.CheckedTracking => _ribbonColours[(int)SchemeOfficeColors.TextButtonFormTracking],
                    PaletteState.Pressed or PaletteState.CheckedPressed or PaletteState.CheckedNormal => _ribbonColours[(int)SchemeOfficeColors.TextButtonFormPressed],
                    _ => _ribbonColours[(int)SchemeOfficeColors.TextButtonFormNormal]
                },
                PaletteContentStyle.ButtonInputControl => state != PaletteState.Disabled
? _ribbonColours[(int)SchemeOfficeColors.InputDropDownNormal1]
: _ribbonColours[(int)SchemeOfficeColors.InputDropDownDisabled1],
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return GlobalStaticValues.EMPTY_COLOR;
            }

            switch (style)
            {
                case PaletteContentStyle.HeaderForm:
                    return state == PaletteState.Disabled
                        ? _ribbonColours[(int)SchemeOfficeColors.FormHeaderShortInactive]
                        : _ribbonColours[(int)SchemeOfficeColors.FormHeaderShortActive];
            }

            if ((state == PaletteState.Disabled) &&
                (style != PaletteContentStyle.LabelToolTip) &&
                (style != PaletteContentStyle.LabelSuperTip) &&
                (style != PaletteContentStyle.LabelKeyTip) &&
                (style != PaletteContentStyle.InputControlStandalone) &&
                (style != PaletteContentStyle.InputControlRibbon) &&
                (style != PaletteContentStyle.InputControlCustom1) &&
                (style != PaletteContentStyle.InputControlCustom2) &&
                (style != PaletteContentStyle.InputControlCustom3) &&
                (style != PaletteContentStyle.ButtonInputControl) &&
                (style != PaletteContentStyle.ButtonCalendarDay))
            {
                return _disabledText;
            }

            return style switch
            {
                PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 or PaletteContentStyle.HeaderCalendar => _gridTextColor,
                PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 => _ribbonColours[(int)SchemeOfficeColors.HeaderText],
                PaletteContentStyle.HeaderDockActive => Color.Black,
                PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 => state == PaletteState.Disabled
? _ribbonColours[(int)SchemeOfficeColors.InputControlTextDisabled]
: _ribbonColours[(int)SchemeOfficeColors.InputControlTextNormal],
                PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption => _ribbonColours[(int)SchemeOfficeColors.TextLabelPanel],
                PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText => _ribbonColours[(int)SchemeOfficeColors.TextLabelControl],
                PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip => _toolTipText,
                PaletteContentStyle.ContextMenuHeading => _ribbonColours[(int)SchemeOfficeColors.ContextMenuHeadingText],
                PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 => (state & PaletteState.Checked) == PaletteState.Checked
? _ribbonColours[(int)SchemeOfficeColors.TextButtonChecked]
: _ribbonColours[(int)SchemeOfficeColors.TextButtonNormal],
                PaletteContentStyle.TabDockAutoHidden => _ribbonColours[(int)SchemeOfficeColors.TextButtonNormal],
                PaletteContentStyle.ButtonCalendarDay => state == PaletteState.Disabled ? _disabledText2 : Color.Black,
                PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonButtonSpec => state switch
                {
                    PaletteState.Normal => style == PaletteContentStyle.ButtonListItem
                                                   ? _ribbonColours[(int)SchemeOfficeColors.TextLabelControl]
                                                   : _ribbonColours[(int)SchemeOfficeColors.TextLabelPanel],
                    PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed => _ribbonColours[(int)SchemeOfficeColors.TextButtonChecked],
                    _ => _ribbonColours[(int)SchemeOfficeColors.TextButtonNormal]
                },
                PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose => state switch
                {
                    PaletteState.Tracking or PaletteState.CheckedTracking => _ribbonColours[(int)SchemeOfficeColors.TextButtonFormTracking],
                    PaletteState.Pressed or PaletteState.CheckedPressed => _ribbonColours[(int)SchemeOfficeColors.TextButtonFormPressed],
                    _ => _ribbonColours[(int)SchemeOfficeColors.TextButtonFormNormal]
                },
                PaletteContentStyle.ButtonInputControl => state != PaletteState.Disabled
? _ribbonColours[(int)SchemeOfficeColors.InputDropDownNormal2]
: _ribbonColours[(int)SchemeOfficeColors.InputDropDownDisabled2],
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the color drawing style for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetContentShortTextColorStyle(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteColorStyle.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteColorStyle.Solid,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the color alignment for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetContentShortTextColorAlign(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteRectangleAlign.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRectangleAlign.Local,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the color background angle for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetContentShortTextColorAngle(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return -1f;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => 90f,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets a background image for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Image instance.</returns>
        public override Image? GetContentShortTextImage(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return null;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => null,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetContentShortTextImageStyle(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteImageStyle.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteImageStyle.TileFlipXY,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the image alignment for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetContentShortTextImageAlign(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteRectangleAlign.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRectangleAlign.Local,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the font for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Font value.</returns>
        public override Font? GetContentLongTextFont(PaletteContentStyle style, PaletteState state)
        {
            if (CommonHelper.IsOverrideState(state))
            {
                return (state == PaletteState.BoldedOverride) && (style == PaletteContentStyle.ButtonCalendarDay) ? CalendarBoldFont : null;
            }

            return style switch
            {
                PaletteContentStyle.ButtonCalendarDay => CalendarFont,
                PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 or PaletteContentStyle.HeaderCalendar => GridFont,
                PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 => Header1LongFont,
                PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.HeaderSecondary => Header2LongFont,
                PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden => TabFontNormal,
                PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 => state switch
                {
                    PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking => TabFontSelected,
                    _ => TabFontNormal
                },
                PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl => ButtonFont,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the font for the long text by generating a new font instance.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font? GetContentLongTextNewFont(PaletteContentStyle style, PaletteState state)
        {
            DefineFonts();
            return GetContentLongTextFont(style, state);
        }

        /// <summary>
        /// Gets the rendering hint for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>PaletteTextHint value.</returns>
        public override PaletteTextHint GetContentLongTextHint(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteTextHint.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteTextHint.ClearTypeGridFit,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the flag indicating if multiline text is allowed for long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetContentLongTextMultiLine(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return InheritBool.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => InheritBool.True,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the text trimming to use for long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>PaletteTextTrim value.</returns>
        public override PaletteTextTrim GetContentLongTextTrim(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteTextTrim.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteTextTrim.EllipsisCharacter,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the prefix drawing setting for long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>PaletteTextPrefix value.</returns>
        public override PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteTextHotkeyPrefix.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl => PaletteTextHotkeyPrefix.Show,
                PaletteContentStyle.ButtonListItem or PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteTextHotkeyPrefix.None,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the horizontal relative alignment of the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentLongTextH(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteRelativeAlign.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.ContextMenuItemTextAlternate => PaletteRelativeAlign.Near,
                PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRelativeAlign.Far,
                PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl => PaletteRelativeAlign.Center,
                PaletteContentStyle.ButtonCalendarDay => PaletteRelativeAlign.Far,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the vertical relative alignment of the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentLongTextV(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteRelativeAlign.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRelativeAlign.Center,
                PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.ContextMenuItemTextAlternate => PaletteRelativeAlign.Far,
                PaletteContentStyle.LabelSuperTip => PaletteRelativeAlign.Center,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the horizontal relative alignment of multiline long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteRelativeAlign.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRelativeAlign.Center,
                PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ButtonCommand => PaletteRelativeAlign.Near,
                PaletteContentStyle.ContextMenuItemShortcutText => PaletteRelativeAlign.Far,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return GlobalStaticValues.EMPTY_COLOR;
            }

            switch (style)
            {
                case PaletteContentStyle.HeaderForm:
                    return state == PaletteState.Disabled
                        ? _ribbonColours[(int)SchemeOfficeColors.FormHeaderLongInactive]
                        : _ribbonColours[(int)SchemeOfficeColors.FormHeaderLongActive];
            }

            if ((state == PaletteState.Disabled) &&
                (style != PaletteContentStyle.LabelToolTip) &&
                (style != PaletteContentStyle.LabelSuperTip) &&
                (style != PaletteContentStyle.LabelKeyTip) &&
                (style != PaletteContentStyle.InputControlStandalone) &&
                (style != PaletteContentStyle.InputControlRibbon) &&
                (style != PaletteContentStyle.InputControlCustom1) &&
                (style != PaletteContentStyle.InputControlCustom2) &&
                (style != PaletteContentStyle.InputControlCustom3) &&
                (style != PaletteContentStyle.ButtonInputControl))
            {
                return _disabledText;
            }

            return style switch
            {
                PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 or PaletteContentStyle.HeaderCalendar => _gridTextColor,
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 => _ribbonColours[(int)SchemeOfficeColors.HeaderText],
                PaletteContentStyle.HeaderDockActive => Color.Black,
                PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 => state == PaletteState.Disabled
? _ribbonColours[(int)SchemeOfficeColors.InputControlTextDisabled]
: _ribbonColours[(int)SchemeOfficeColors.InputControlTextNormal],
                PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption => _ribbonColours[(int)SchemeOfficeColors.TextLabelPanel],
                PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.ContextMenuItemTextAlternate => _ribbonColours[(int)SchemeOfficeColors.TextLabelControl],
                PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip => _toolTipText,
                PaletteContentStyle.ContextMenuHeading => _ribbonColours[(int)SchemeOfficeColors.ContextMenuHeadingText],
                PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 => (state & PaletteState.Checked) == PaletteState.Checked
? _ribbonColours[(int)SchemeOfficeColors.TextButtonChecked]
: _ribbonColours[(int)SchemeOfficeColors.TextButtonNormal],
                PaletteContentStyle.TabDockAutoHidden => _ribbonColours[(int)SchemeOfficeColors.TextButtonNormal],
                PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay => state switch
                {
                    PaletteState.Normal => style == PaletteContentStyle.ButtonListItem
                                                   ? _ribbonColours[(int)SchemeOfficeColors.TextLabelControl]
                                                   : _ribbonColours[(int)SchemeOfficeColors.TextLabelPanel],
                    PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed => _ribbonColours[(int)SchemeOfficeColors.TextButtonChecked],
                    _ => _ribbonColours[(int)SchemeOfficeColors.TextButtonNormal]
                },
                PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose => state switch
                {
                    PaletteState.Tracking or PaletteState.CheckedTracking => _ribbonColours[(int)SchemeOfficeColors.TextButtonFormTracking],
                    PaletteState.Pressed or PaletteState.CheckedPressed => _ribbonColours[(int)SchemeOfficeColors.TextButtonFormPressed],
                    _ => _ribbonColours[(int)SchemeOfficeColors.TextButtonFormNormal]
                },
                PaletteContentStyle.ButtonInputControl => state != PaletteState.Disabled
? _ribbonColours[(int)SchemeOfficeColors.InputDropDownNormal1]
: _ribbonColours[(int)SchemeOfficeColors.InputDropDownDisabled1],
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return GlobalStaticValues.EMPTY_COLOR;
            }

            switch (style)
            {
                case PaletteContentStyle.HeaderForm:
                    return state == PaletteState.Disabled
                        ? _ribbonColours[(int)SchemeOfficeColors.FormHeaderLongInactive]
                        : _ribbonColours[(int)SchemeOfficeColors.FormHeaderLongActive];
            }

            if ((state == PaletteState.Disabled) &&
                (style != PaletteContentStyle.LabelToolTip) &&
                (style != PaletteContentStyle.LabelSuperTip) &&
                (style != PaletteContentStyle.LabelKeyTip) &&
                (style != PaletteContentStyle.InputControlStandalone) &&
                (style != PaletteContentStyle.InputControlRibbon) &&
                (style != PaletteContentStyle.InputControlCustom1) &&
                (style != PaletteContentStyle.InputControlCustom2) &&
                (style != PaletteContentStyle.InputControlCustom3) &&
                (style != PaletteContentStyle.ButtonInputControl))
            {
                return _disabledText;
            }

            return style switch
            {
                PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 or PaletteContentStyle.HeaderCalendar => _gridTextColor,
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 => _ribbonColours[(int)SchemeOfficeColors.HeaderText],
                PaletteContentStyle.HeaderDockActive => Color.Black,
                PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 => state == PaletteState.Disabled
? _ribbonColours[(int)SchemeOfficeColors.InputControlTextDisabled]
: _ribbonColours[(int)SchemeOfficeColors.InputControlTextNormal],
                PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption => _ribbonColours[(int)SchemeOfficeColors.TextLabelPanel],
                PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText => _ribbonColours[(int)SchemeOfficeColors.TextLabelControl],
                PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip => _toolTipText,
                PaletteContentStyle.ContextMenuHeading => _ribbonColours[(int)SchemeOfficeColors.ContextMenuHeadingText],
                PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 => (state & PaletteState.Checked) == PaletteState.Checked
? _ribbonColours[(int)SchemeOfficeColors.TextButtonChecked]
: _ribbonColours[(int)SchemeOfficeColors.TextButtonNormal],
                PaletteContentStyle.TabDockAutoHidden => _ribbonColours[(int)SchemeOfficeColors.TextButtonNormal],
                PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay => state switch
                {
                    PaletteState.Normal => style == PaletteContentStyle.ButtonListItem
                                                   ? _ribbonColours[(int)SchemeOfficeColors.TextLabelControl]
                                                   : _ribbonColours[(int)SchemeOfficeColors.TextLabelPanel],
                    PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed => _ribbonColours[(int)SchemeOfficeColors.TextButtonChecked],
                    _ => _ribbonColours[(int)SchemeOfficeColors.TextButtonNormal]
                },
                PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose => state switch
                {
                    PaletteState.Tracking or PaletteState.CheckedTracking => _ribbonColours[(int)SchemeOfficeColors.TextButtonFormTracking],
                    PaletteState.Pressed or PaletteState.CheckedPressed => _ribbonColours[(int)SchemeOfficeColors.TextButtonFormPressed],
                    _ => _ribbonColours[(int)SchemeOfficeColors.TextButtonFormNormal]
                },
                PaletteContentStyle.ButtonInputControl => state != PaletteState.Disabled
? _ribbonColours[(int)SchemeOfficeColors.InputDropDownNormal2]
: _ribbonColours[(int)SchemeOfficeColors.InputDropDownDisabled2],
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the color drawing style for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetContentLongTextColorStyle(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteColorStyle.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteColorStyle.Solid,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the color alignment for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetContentLongTextColorAlign(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteRectangleAlign.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRectangleAlign.Local,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the color background angle for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetContentLongTextColorAngle(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return -1f;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => 90f,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets a background image for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Image instance.</returns>
        public override Image? GetContentLongTextImage(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return null;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => null,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the background image style for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetContentLongTextImageStyle(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteImageStyle.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteImageStyle.TileFlipXY,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the image alignment for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetContentLongTextImageAlign(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return PaletteRectangleAlign.Inherit;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelSuperTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => PaletteRectangleAlign.Local,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the padding between the border and content drawing.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Padding value.</returns>
        public override Padding GetContentPadding(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return CommonHelper.InheritPadding;
            }

            return style switch
            {
                PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => _contentPaddingGrid,
                PaletteContentStyle.HeaderForm => _contentPaddingHeaderForm,
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 => _contentPaddingHeader1,
                PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive => _contentPaddingDock,
                PaletteContentStyle.HeaderSecondary => _contentPaddingHeader2,
                PaletteContentStyle.HeaderCalendar => _contentPaddingCalendar,
                PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 => _contentPaddingLabel,
                PaletteContentStyle.LabelGroupBoxCaption => _contentPaddingLabel2,
                PaletteContentStyle.ContextMenuItemTextStandard => _contentPaddingContextMenuItemText,
                PaletteContentStyle.ContextMenuItemTextAlternate => _contentPaddingContextMenuItemTextAlt,
                PaletteContentStyle.ContextMenuItemShortcutText => _contentPaddingContextMenuItemShortcutText,
                PaletteContentStyle.ContextMenuItemImage => _contentPaddingContextMenuImage,
                PaletteContentStyle.LabelToolTip => _contentPaddingToolTip,
                PaletteContentStyle.LabelSuperTip => _contentPaddingSuperTip,
                PaletteContentStyle.LabelKeyTip => _contentPaddingKeyTip,
                PaletteContentStyle.ContextMenuHeading => _contentPaddingContextMenuHeading,
                PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 => InputControlPadding,
                PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 => _contentPaddingButton12,
                PaletteContentStyle.ButtonInputControl or PaletteContentStyle.ButtonCalendarDay => _contentPaddingButtonInputControl,
                PaletteContentStyle.ButtonButtonSpec => _contentPaddingButton3,
                PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow => _contentPaddingButton4,
                PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose => _contentPaddingButtonForm,
                PaletteContentStyle.ButtonGallery => _contentPaddingButtonGallery,
                PaletteContentStyle.ButtonListItem => _contentPaddingButtonListItem,
                PaletteContentStyle.ButtonBreadCrumb => _contentPaddingButton6,
                PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 => _contentPaddingButton5,
                PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden => _contentPaddingButton7,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }

        /// <summary>
        /// Gets the padding between adjacent content items.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Integer value.</returns>
        public override int GetContentAdjacentGap(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return -1;
            }

            return style switch
            {
                PaletteContentStyle.HeaderPrimary or PaletteContentStyle.HeaderDockInactive or PaletteContentStyle.HeaderDockActive or PaletteContentStyle.HeaderCalendar or PaletteContentStyle.HeaderSecondary or PaletteContentStyle.HeaderForm or PaletteContentStyle.HeaderCustom1 or PaletteContentStyle.HeaderCustom2 or PaletteContentStyle.HeaderCustom3 or PaletteContentStyle.LabelNormalControl or PaletteContentStyle.LabelBoldControl or PaletteContentStyle.LabelItalicControl or PaletteContentStyle.LabelTitleControl or PaletteContentStyle.LabelNormalPanel or PaletteContentStyle.LabelBoldPanel or PaletteContentStyle.LabelItalicPanel or PaletteContentStyle.LabelTitlePanel or PaletteContentStyle.LabelGroupBoxCaption or PaletteContentStyle.LabelToolTip or PaletteContentStyle.LabelKeyTip or PaletteContentStyle.LabelCustom1 or PaletteContentStyle.LabelCustom2 or PaletteContentStyle.LabelCustom3 or PaletteContentStyle.ContextMenuHeading or PaletteContentStyle.ContextMenuItemImage or PaletteContentStyle.ContextMenuItemTextStandard or PaletteContentStyle.ContextMenuItemTextAlternate or PaletteContentStyle.ContextMenuItemShortcutText or PaletteContentStyle.InputControlStandalone or PaletteContentStyle.InputControlRibbon or PaletteContentStyle.InputControlCustom1 or PaletteContentStyle.InputControlCustom2 or PaletteContentStyle.InputControlCustom3 or PaletteContentStyle.TabHighProfile or PaletteContentStyle.TabStandardProfile or PaletteContentStyle.TabLowProfile or PaletteContentStyle.TabOneNote or PaletteContentStyle.TabDock or PaletteContentStyle.TabDockAutoHidden or PaletteContentStyle.TabCustom1 or PaletteContentStyle.TabCustom2 or PaletteContentStyle.TabCustom3 or PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonGallery or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonListItem or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay or PaletteContentStyle.ButtonCluster or PaletteContentStyle.ButtonNavigatorMini or PaletteContentStyle.ButtonNavigatorStack or PaletteContentStyle.ButtonNavigatorOverflow or PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 or PaletteContentStyle.ButtonInputControl or PaletteContentStyle.GridHeaderColumnList or PaletteContentStyle.GridHeaderColumnSheet or PaletteContentStyle.GridHeaderColumnCustom1 or PaletteContentStyle.GridHeaderColumnCustom2 or PaletteContentStyle.GridHeaderColumnCustom3 or PaletteContentStyle.GridHeaderRowList or PaletteContentStyle.GridHeaderRowSheet or PaletteContentStyle.GridHeaderRowCustom1 or PaletteContentStyle.GridHeaderRowCustom2 or PaletteContentStyle.GridHeaderRowCustom3 or PaletteContentStyle.GridDataCellList or PaletteContentStyle.GridDataCellSheet or PaletteContentStyle.GridDataCellCustom1 or PaletteContentStyle.GridDataCellCustom2 or PaletteContentStyle.GridDataCellCustom3 => 1,
                PaletteContentStyle.LabelSuperTip => 5,
                _ => throw new ArgumentOutOfRangeException(nameof(style))
            };
        }
        #endregion

        #region Metric
        /// <summary>
        /// Gets an integer metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Integer value.</returns>
        public override int GetMetricInt(PaletteState state, PaletteMetricInt metric)
        {
            switch (metric)
            {
                case PaletteMetricInt.PageButtonInset:
                case PaletteMetricInt.RibbonTabGap:
                case PaletteMetricInt.HeaderButtonEdgeInsetCalendar:
                    return 2;
                case PaletteMetricInt.CheckButtonGap:
                    return 5;
                case PaletteMetricInt.HeaderButtonEdgeInsetForm:
                    return 9; // Needs to be the RealWindowBorderWidth Offset - No idea how to get it at this point
                case PaletteMetricInt.HeaderButtonEdgeInsetInputControl:
                    return 1;
                case PaletteMetricInt.HeaderButtonEdgeInsetPrimary:
                case PaletteMetricInt.HeaderButtonEdgeInsetSecondary:
                case PaletteMetricInt.HeaderButtonEdgeInsetDockInactive:
                case PaletteMetricInt.HeaderButtonEdgeInsetDockActive:
                case PaletteMetricInt.HeaderButtonEdgeInsetCustom1:
                case PaletteMetricInt.HeaderButtonEdgeInsetCustom2:
                case PaletteMetricInt.HeaderButtonEdgeInsetCustom3:
                case PaletteMetricInt.BarButtonEdgeOutside:
                case PaletteMetricInt.BarButtonEdgeInside:
                    return 3;
                case PaletteMetricInt.None:
                    return 0;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(metric.ToString());
                    break;
            }

            return -1;
        }

        /// <summary>
        /// Gets a boolean metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric)
        {
            switch (metric)
            {
                case PaletteMetricBool.HeaderGroupOverlay:
                case PaletteMetricBool.SplitWithFading:
                    return InheritBool.True;
                case PaletteMetricBool.RibbonTabsSpareCaption:
                case PaletteMetricBool.TreeViewLines:
                    return InheritBool.False;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(metric.ToString());
                    break;
            }

            return InheritBool.Inherit;
        }

        /// <summary>
        /// Gets a padding metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Padding value.</returns>
        public override Padding GetMetricPadding(PaletteState state, PaletteMetricPadding metric)
        {
            switch (metric)
            {
                case PaletteMetricPadding.PageButtonPadding:
                    return _metricPaddingPageButtons;
                case PaletteMetricPadding.BarPaddingTabs:
                    return _metricPaddingBarTabs;
                case PaletteMetricPadding.BarPaddingInside:
                case PaletteMetricPadding.BarPaddingOnly:
                    return _metricPaddingBarInside;
                case PaletteMetricPadding.BarPaddingOutside:
                    return _metricPaddingBarOutside;
                case PaletteMetricPadding.HeaderButtonPaddingForm:
                    return _metricPaddingHeaderForm;
                case PaletteMetricPadding.RibbonButtonPadding:
                    return _metricPaddingRibbon;
                case PaletteMetricPadding.RibbonAppButton:
                    return _metricPaddingRibbonAppButton;
                case PaletteMetricPadding.HeaderButtonPaddingInputControl:
                    return _metricPaddingInputControl;
                case PaletteMetricPadding.HeaderButtonPaddingPrimary:
                case PaletteMetricPadding.HeaderButtonPaddingSecondary:
                case PaletteMetricPadding.HeaderButtonPaddingDockInactive:
                case PaletteMetricPadding.HeaderButtonPaddingDockActive:
                case PaletteMetricPadding.HeaderButtonPaddingCustom1:
                case PaletteMetricPadding.HeaderButtonPaddingCustom2:
                case PaletteMetricPadding.HeaderButtonPaddingCustom3:
                case PaletteMetricPadding.HeaderButtonPaddingCalendar:
                case PaletteMetricPadding.BarButtonPadding:
                    return _metricPaddingHeader;
                case PaletteMetricPadding.HeaderGroupPaddingPrimary:
                case PaletteMetricPadding.HeaderGroupPaddingSecondary:
                case PaletteMetricPadding.HeaderGroupPaddingDockInactive:
                case PaletteMetricPadding.HeaderGroupPaddingDockActive:
                case PaletteMetricPadding.SeparatorPaddingLowProfile:
                case PaletteMetricPadding.SeparatorPaddingHighInternalProfile:
                case PaletteMetricPadding.SeparatorPaddingHighProfile:
                case PaletteMetricPadding.SeparatorPaddingCustom1:
                case PaletteMetricPadding.SeparatorPaddingCustom2:
                case PaletteMetricPadding.SeparatorPaddingCustom3:
                case PaletteMetricPadding.ContextMenuItemHighlight:
                case PaletteMetricPadding.ContextMenuItemsCollection:
                case PaletteMetricPadding.ContextMenuItemOuter:
                    return Padding.Empty;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(metric.ToString());
                    break;
            }

            return Padding.Empty;
        }
        #endregion

        #region Images
        /// <summary>
        /// Gets a tree view image appropriate for the provided state.
        /// </summary>
        /// <param name="expanded">Is the node expanded</param>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image? GetTreeViewImage(bool expanded) => expanded ? _treeMinus : _treePlus;

        /// <summary>
        /// Gets a check box image appropriate for the provided state.
        /// </summary>
        /// <param name="enabled">Is the check box enabled.</param>
        /// <param name="checkState">Is the check box checked/unchecked/indeterminate.</param>
        /// <param name="tracking">Is the check box being hot tracked.</param>
        /// <param name="pressed">Is the check box being pressed.</param>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image? GetCheckBoxImage(bool enabled, CheckState checkState, bool tracking, bool pressed)
        {
            switch (checkState)
            {
                default:
                case CheckState.Unchecked:
                    if (!enabled)
                    {
                        return _checkBoxList.Images[0];
                    }
                    else if (pressed)
                    {
                        return _checkBoxList.Images[3];
                    }
                    else
                    {
                        return tracking ? _checkBoxList.Images[2] : _checkBoxList.Images[1];
                    }

                case CheckState.Checked:
                    if (!enabled)
                    {
                        return _checkBoxList.Images[4];
                    }
                    else if (pressed)
                    {
                        return _checkBoxList.Images[7];
                    }
                    else
                    {
                        return tracking ? _checkBoxList.Images[6] : _checkBoxList.Images[5];
                    }

                case CheckState.Indeterminate:
                    if (!enabled)
                    {
                        return _checkBoxList.Images[8];
                    }
                    else if (pressed)
                    {
                        return _checkBoxList.Images[11];
                    }
                    else
                    {
                        return tracking ? _checkBoxList.Images[10] : _checkBoxList.Images[9];
                    }
            }
        }

        /// <summary>
        /// Gets a check box image appropriate for the provided state.
        /// </summary>
        /// <param name="enabled">Is the radio button enabled.</param>
        /// <param name="checkState">Is the radio button checked.</param>
        /// <param name="tracking">Is the radio button being hot tracked.</param>
        /// <param name="pressed">Is the radio button being pressed.</param>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image? GetRadioButtonImage(bool enabled, bool checkState, bool tracking, bool pressed)
        {
            if (!checkState)
            {
                if (!enabled)
                {
                    return _radioButtonArray[0];
                }
                else if (pressed)
                {
                    return _radioButtonArray[3];
                }
                else
                {
                    return tracking ? _radioButtonArray[2] : _radioButtonArray[1];
                }
            }
            else
            {
                if (!enabled)
                {
                    return _radioButtonArray[4];
                }
                else if (pressed)
                {
                    return _radioButtonArray[7];
                }
                else
                {
                    return tracking ? _radioButtonArray[6] : _radioButtonArray[5];
                }
            }
        }

        /// <summary>
        /// Gets a drop down button image appropriate for the provided state.
        /// </summary>
        /// <param name="state">PaletteState for which image is required.</param>
        public override Image? GetDropDownButtonImage(PaletteState state) => _disabledDropDown;

        /// <summary>
        /// Gets a checked image appropriate for a context menu item.
        /// </summary>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image? GetContextMenuCheckedImage() => _contextMenuChecked;

        /// <summary>
        /// Gets a indeterminate image appropriate for a context menu item.
        /// </summary>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image? GetContextMenuIndeterminateImage() => _contextMenuIndeterminate;

        /// <summary>
        /// Gets a check box image appropriate for the provided state.
        /// </summary>
        /// <param name="button">Enum of the button to fetch.</param>
        /// <param name="state">State of the button to fetch.</param>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image? GetGalleryButtonImage(PaletteRibbonGalleryButton button, PaletteState state) => button switch
        {
            PaletteRibbonGalleryButton.Up => _galleryButtonList.Images[1],
            PaletteRibbonGalleryButton.DropDown => _galleryButtonList.Images[2],
            _ => _galleryButtonList.Images[0]
        };
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
            switch (style)
            {
                case PaletteButtonSpecStyle.Close:
                    return _buttonSpecClose;
                case PaletteButtonSpecStyle.Context:
                    return _buttonSpecContext;
                case PaletteButtonSpecStyle.Next:
                    return _buttonSpecNext;
                case PaletteButtonSpecStyle.Previous:
                    return _buttonSpecPrevious;
                case PaletteButtonSpecStyle.ArrowLeft:
                    return _buttonSpecArrowLeft;
                case PaletteButtonSpecStyle.ArrowRight:
                    return _buttonSpecArrowRight;
                case PaletteButtonSpecStyle.ArrowUp:
                    return _buttonSpecArrowUp;
                case PaletteButtonSpecStyle.ArrowDown:
                    return _buttonSpecArrowDown;
                case PaletteButtonSpecStyle.DropDown:
                    return _buttonSpecDropDown;
                case PaletteButtonSpecStyle.PinVertical:
                    return _buttonSpecPinVertical;
                case PaletteButtonSpecStyle.PinHorizontal:
                    return _buttonSpecPinHorizontal;
                case PaletteButtonSpecStyle.PendantClose:
                    return _buttonSpecPendantClose;
                case PaletteButtonSpecStyle.PendantMin:
                    return _buttonSpecPendantMin;
                case PaletteButtonSpecStyle.PendantRestore:
                    return _buttonSpecPendantRestore;
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                    return _buttonSpecWorkspaceMaximize;
                case PaletteButtonSpecStyle.WorkspaceRestore:
                    return _buttonSpecWorkspaceRestore;
                case PaletteButtonSpecStyle.RibbonMinimize:
                    return _buttonSpecRibbonMinimize;
                case PaletteButtonSpecStyle.RibbonExpand:
                    return _buttonSpecRibbonExpand;
                case PaletteButtonSpecStyle.New:
                    return _integratedToolbarNewNormal;
                case PaletteButtonSpecStyle.Open:
                    return _integratedToolbarOpenNormal;
                case PaletteButtonSpecStyle.Save:
                    return _integratedToolbarSaveNormal;
                case PaletteButtonSpecStyle.SaveAs:
                    return _integratedToolbarSaveAsNormal;
                case PaletteButtonSpecStyle.SaveAll:
                    return _integratedToolbarSaveAllNormal;
                case PaletteButtonSpecStyle.Cut:
                    return _integratedToolbarCutNormal;
                case PaletteButtonSpecStyle.Copy:
                    return _integratedToolbarCopyNormal;
                case PaletteButtonSpecStyle.Paste:
                    return _integratedToolbarPasteNormal;
                case PaletteButtonSpecStyle.Undo:
                    return _integratedToolbarUndoNormal;
                case PaletteButtonSpecStyle.Redo:
                    return _integratedToolbarRedoNormal;
                case PaletteButtonSpecStyle.PageSetup:
                    return _integratedToolbarPageSetupNormal;
                case PaletteButtonSpecStyle.PrintPreview:
                    return _integratedToolbarPrintPreviewNormal;
                case PaletteButtonSpecStyle.Print:
                    return _integratedToolbarPrintNormal;
                case PaletteButtonSpecStyle.QuickPrint:
                    return _integratedToolbarQuickPrintNormal;
                case PaletteButtonSpecStyle.Generic:
                    return null;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    throw DebugTools.NotImplemented(style.ToString());
            }
        }

        #endregion

        #region RibbonGeneral
        /// <summary>
        /// Gets the ribbon shape that should be used.
        /// </summary>
        /// <returns>Ribbon shape value.</returns>
        public override PaletteRibbonShape GetRibbonShape() => PaletteRibbonShape.Office2007;

        /// <summary>
        /// Gets the text alignment for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override PaletteRelativeAlign GetRibbonContextTextAlign(PaletteState state) => PaletteRelativeAlign.Near;

        /// <summary>
        /// Gets the font for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetRibbonContextTextFont(PaletteState state) => RibbonTabFont!;

        /// <summary>
        /// Gets the color for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Color GetRibbonContextTextColor(PaletteState state) => _ribbonColours[(int)SchemeOfficeColors.RibbonTabTextNormal];

        /// <summary>
        /// Gets the dark disabled color used for ribbon glyphs.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonDisabledDark(PaletteState state) => _disabledGlyphDark;

        /// <summary>
        /// Gets the light disabled color used for ribbon glyphs.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonDisabledLight(PaletteState state) => _disabledGlyphLight;

        /// <summary>
        /// Gets the color for the drop arrow light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonDropArrowLight(PaletteState state) => _ribbonColours[(int)SchemeOfficeColors.RibbonGroupDialogLight];

        /// <summary>
        /// Gets the color for the drop arrow dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonDropArrowDark(PaletteState state) => _ribbonColours[(int)SchemeOfficeColors.RibbonGroupDialogDark];

        /// <summary>
        /// Gets the color for the dialog launcher dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonGroupDialogDark(PaletteState state) => _ribbonColours[(int)SchemeOfficeColors.RibbonGroupDialogDark];

        /// <summary>
        /// Gets the color for the dialog launcher light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonGroupDialogLight(PaletteState state) => _ribbonColours[(int)SchemeOfficeColors.RibbonGroupDialogLight];

        /// <summary>
        /// Gets the color for the group separator dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonGroupSeparatorDark(PaletteState state) => _ribbonColours[(int)SchemeOfficeColors.RibbonGroupSeparatorDark];

        /// <summary>
        /// Gets the color for the group separator light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonGroupSeparatorLight(PaletteState state) => _ribbonColours[(int)SchemeOfficeColors.RibbonGroupSeparatorLight];

        /// <summary>
        /// Gets the color for the minimize bar dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonMinimizeBarDark(PaletteState state) => _ribbonColours[(int)SchemeOfficeColors.RibbonMinimizeBarDark];

        /// <summary>
        /// Gets the color for the minimize bar light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonMinimizeBarLight(PaletteState state) => _ribbonColours[(int)SchemeOfficeColors.RibbonMinimizeBarLight];

        /// <summary>
        /// Gets the color for the tab separator.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonTabSeparatorColor(PaletteState state) => _ribbonColours[(int)SchemeOfficeColors.RibbonTabSeparatorColor];

        /// <summary>
        /// Gets the color for the tab context separators.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonTabSeparatorContextColor(PaletteState state) => _contextTabSeparator;

        /// <summary>
        /// Gets the font for the ribbon text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetRibbonTextFont(PaletteState state) => RibbonTabFont!;

        /// <summary>
        /// Gets the rendering hint for the ribbon font.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextHint value.</returns>
        public override PaletteTextHint GetRibbonTextHint(PaletteState state) => PaletteTextHint.ClearTypeGridFit;

        /// <summary>
        /// Gets the color for the extra QAT button dark content color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonQATButtonDark(PaletteState state) => _ribbonColours[(int)SchemeOfficeColors.RibbonQATButtonDark];

        /// <summary>
        /// Gets the color for the extra QAT button light content color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonQATButtonLight(PaletteState state) => _ribbonColours[(int)SchemeOfficeColors.RibbonQATButtonLight];

        #endregion

        #region RibbonBack
        /// <summary>
        /// Gets the method used to draw the background of a ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteRibbonBackStyle value.</returns>
        public override PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteRibbonBackStyle style, PaletteState state)
        {
            switch (style)
            {
                case PaletteRibbonBackStyle.RibbonAppMenuDocs:
                    return PaletteRibbonColorStyle.Solid;
                case PaletteRibbonBackStyle.RibbonAppMenuInner:
                    return PaletteRibbonColorStyle.RibbonAppMenuInner;
                case PaletteRibbonBackStyle.RibbonAppMenuOuter:
                    return PaletteRibbonColorStyle.RibbonAppMenuOuter;
                case PaletteRibbonBackStyle.RibbonQATMinibar:
                    return state == PaletteState.CheckedNormal
                        ? PaletteRibbonColorStyle.RibbonQATMinibarDouble
                        : PaletteRibbonColorStyle.RibbonQATMinibarSingle;

                case PaletteRibbonBackStyle.RibbonQATFullbar:
                    return PaletteRibbonColorStyle.RibbonQATFullbarRound;
                case PaletteRibbonBackStyle.RibbonQATOverflow:
                    return PaletteRibbonColorStyle.RibbonQATOverflow;
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
                    return PaletteRibbonColorStyle.RibbonGroupCollapsedFrameBorder;
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                    return PaletteRibbonColorStyle.RibbonGroupCollapsedBorder;
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                    return state switch
                    {
                        PaletteState.ContextNormal or PaletteState.ContextTracking => PaletteRibbonColorStyle.RibbonGroupGradientOne,
                        _ => PaletteRibbonColorStyle.RibbonGroupCollapsedFrameBack
                    };

                case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
                    switch (state)
                    {
                        case PaletteState.Normal:
                        case PaletteState.Tracking:
                            return PaletteRibbonColorStyle.RibbonGroupGradientTwo;
                        case PaletteState.ContextNormal:
                        case PaletteState.ContextTracking:
                            return PaletteRibbonColorStyle.RibbonGroupGradientOne;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
                    switch (state)
                    {
                        case PaletteState.Normal:
                        case PaletteState.ContextNormal:
                            return PaletteRibbonColorStyle.RibbonGroupNormalBorder;
                        case PaletteState.Tracking:
                        case PaletteState.ContextTracking:
                            return PaletteRibbonColorStyle.RibbonGroupNormalBorderTracking;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
                    return PaletteRibbonColorStyle.RibbonGroupNormalTitle;
                case PaletteRibbonBackStyle.RibbonGroupArea:
                    switch (state)
                    {
                        case PaletteState.Normal:
                        case PaletteState.CheckedNormal:
                        case PaletteState.ContextNormal:
                        case PaletteState.ContextCheckedNormal:
                            return PaletteRibbonColorStyle.Empty;
                        case PaletteState.Tracking:
                        case PaletteState.ContextTracking:
                        case PaletteState.ContextCheckedTracking:
                            return PaletteRibbonColorStyle.RibbonGroupNormalTrackingLight;
                        case PaletteState.FocusOverride:
                            return PaletteRibbonColorStyle.RibbonTabFocus2010;
                        case PaletteState.ContextPressed:
                            return PaletteRibbonColorStyle.RibbonGroupNormalPressedDark;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                case PaletteRibbonBackStyle.RibbonTab:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                            return PaletteRibbonColorStyle.Empty;
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                        case PaletteState.ContextTracking:
                            return PaletteRibbonColorStyle.RibbonTabTracking2007;
                        case PaletteState.CheckedNormal:
                            return PaletteRibbonColorStyle.RibbonTabSelected2007;
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return PaletteRibbonColorStyle.RibbonTabHighlight;
                        case PaletteState.ContextCheckedNormal:
                        case PaletteState.ContextCheckedTracking:
                        case PaletteState.FocusOverride:
                            return PaletteRibbonColorStyle.RibbonTabContextSelected;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(style.ToString());
                    break;
            }

            return PaletteRibbonColorStyle.Empty;
        }

        /// <summary>
        /// Gets the first background color for the ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor1(PaletteRibbonBackStyle style, PaletteState state)
        {
            switch (style)
            {
                case PaletteRibbonBackStyle.RibbonGalleryBack:
                    return state switch
                    {
                        PaletteState.Disabled => _disabledBack,
                        PaletteState.Tracking => _ribbonColours[(int)SchemeOfficeColors.RibbonGalleryBackTracking],
                        _ => _ribbonColours[(int)SchemeOfficeColors.RibbonGalleryBackNormal]
                    };
                case PaletteRibbonBackStyle.RibbonGalleryBorder:
                    return state switch
                    {
                        PaletteState.Disabled => _disabledBorder,
                        _ => _ribbonColours[(int)SchemeOfficeColors.RibbonGalleryBorder]
                    };
                case PaletteRibbonBackStyle.RibbonAppMenuDocs:
                    return _ribbonColours[(int)SchemeOfficeColors.AppButtonMenuDocsBack];
                case PaletteRibbonBackStyle.RibbonAppMenuInner:
                    return _ribbonColours[(int)SchemeOfficeColors.AppButtonInner1];
                case PaletteRibbonBackStyle.RibbonAppMenuOuter:
                    return _ribbonColours[(int)SchemeOfficeColors.AppButtonOuter1];
                case PaletteRibbonBackStyle.RibbonQATMinibar:
                    return state == PaletteState.Normal
                        ? _ribbonColours[(int)SchemeOfficeColors.RibbonQATMini1]
                        : _ribbonColours[(int)SchemeOfficeColors.RibbonQATMini1I];

                case PaletteRibbonBackStyle.RibbonQATFullbar:
                    return _ribbonColours[(int)SchemeOfficeColors.RibbonQATFullbar1];
                case PaletteRibbonBackStyle.RibbonQATOverflow:
                    return _ribbonColours[(int)SchemeOfficeColors.RibbonQATOverflow1];
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
                    return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupFrameBorder1];
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupCollapsedBorder1];
                        case PaletteState.Tracking:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupCollapsedBorderT1];
                        case PaletteState.ContextNormal:
                            return _ribbonGroupCollapsedBorderContext[0];
                        case PaletteState.ContextTracking:
                        case PaletteState.Pressed:
                            return _ribbonGroupCollapsedBorderContextTracking[0];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                    return state switch
                    {
                        PaletteState.ContextNormal or PaletteState.ContextTracking => _contextGroupFrameTop,
                        _ => _ribbonColours[(int)SchemeOfficeColors.RibbonGroupFrameInside1]
                    };
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupCollapsedBack1];
                        case PaletteState.Tracking:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupCollapsedBackT1];
                        case PaletteState.ContextNormal:
                            return _ribbonGroupCollapsedBackContext[0];
                        case PaletteState.ContextTracking:
                            return _ribbonGroupCollapsedBackContextTracking[0];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupTitle1];
                        case PaletteState.ContextNormal:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupTitleContext1];
                        case PaletteState.Tracking:
                        case PaletteState.ContextTracking:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupTitleTracking1];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
                    switch (state)
                    {
                        case PaletteState.Normal:
                        case PaletteState.Tracking:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupBorder1];
                        case PaletteState.ContextNormal:
                        case PaletteState.ContextTracking:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupBorderContext1];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                case PaletteRibbonBackStyle.RibbonAppButton:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _appButtonNormal[0];
                        case PaletteState.Tracking:
                            return _appButtonTrack[0];
                        case PaletteState.Pressed:
                            return _appButtonPressed[0];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                case PaletteRibbonBackStyle.RibbonGroupArea:
                    return state == PaletteState.ContextCheckedNormal
                        ? _contextGroupAreaBorder
                        : _ribbonColours[(int)SchemeOfficeColors.RibbonGroupsArea5];

                case PaletteRibbonBackStyle.RibbonTab:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                        case PaletteState.ContextTracking:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonTabTracking1];
                        case PaletteState.CheckedNormal:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonTabSelected1];
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonTabHighlight1];
                        case PaletteState.ContextCheckedTracking:
                        case PaletteState.ContextCheckedNormal:
                        case PaletteState.FocusOverride:
                            return _contextCheckedTabBorder;
                        case PaletteState.Normal:
                            return GlobalStaticValues.EMPTY_COLOR;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(style.ToString());
                    break;
            }

            return Color.Red;
        }

        /// <summary>
        /// Gets the second background color for the ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor2(PaletteRibbonBackStyle style, PaletteState state)
        {
            switch (style)
            {
                case PaletteRibbonBackStyle.RibbonAppMenuInner:
                    return _ribbonColours[(int)SchemeOfficeColors.AppButtonInner2];
                case PaletteRibbonBackStyle.RibbonAppMenuOuter:
                    return _ribbonColours[(int)SchemeOfficeColors.AppButtonOuter2];
                case PaletteRibbonBackStyle.RibbonQATMinibar:
                    return state == PaletteState.Normal
                        ? _ribbonColours[(int)SchemeOfficeColors.RibbonQATMini2]
                        : _ribbonColours[(int)SchemeOfficeColors.RibbonQATMini2I];

                case PaletteRibbonBackStyle.RibbonQATFullbar:
                    return _ribbonColours[(int)SchemeOfficeColors.RibbonQATFullbar2];
                case PaletteRibbonBackStyle.RibbonQATOverflow:
                    return _ribbonColours[(int)SchemeOfficeColors.RibbonQATOverflow2];
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
                    return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupFrameBorder2];
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupCollapsedBorder2];
                        case PaletteState.Tracking:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupCollapsedBorderT2];
                        case PaletteState.ContextNormal:
                            return _ribbonGroupCollapsedBorderContext[1];
                        case PaletteState.ContextTracking:
                        case PaletteState.Pressed:
                            return _ribbonGroupCollapsedBorderContextTracking[1];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                    return state switch
                    {
                        PaletteState.ContextNormal or PaletteState.ContextTracking => _contextGroupFrameBottom,
                        _ => _ribbonColours[(int)SchemeOfficeColors.RibbonGroupFrameInside2]
                    };
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupCollapsedBack2];
                        case PaletteState.Tracking:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupCollapsedBackT2];
                        case PaletteState.ContextNormal:
                            return _ribbonGroupCollapsedBackContext[1];
                        case PaletteState.ContextTracking:
                            return _ribbonGroupCollapsedBackContextTracking[1];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupTitle2];
                        case PaletteState.ContextNormal:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupTitleContext2];
                        case PaletteState.Tracking:
                        case PaletteState.ContextTracking:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupTitleTracking2];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
                    switch (state)
                    {
                        case PaletteState.Normal:
                        case PaletteState.Tracking:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupBorder2];
                        case PaletteState.ContextNormal:
                        case PaletteState.ContextTracking:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupBorderContext2];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                case PaletteRibbonBackStyle.RibbonAppButton:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _appButtonNormal[1];
                        case PaletteState.Tracking:
                            return _appButtonTrack[1];
                        case PaletteState.Pressed:
                            return _appButtonPressed[1];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                case PaletteRibbonBackStyle.RibbonGroupArea:
                    return state == PaletteState.ContextCheckedNormal
                        ? _contextGroupAreaInside
                        : _ribbonColours[(int)SchemeOfficeColors.RibbonGroupsArea4];

                case PaletteRibbonBackStyle.RibbonTab:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonTabTracking2];
                        case PaletteState.CheckedNormal:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonTabSelected2];
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonTabHighlight2];
                        case PaletteState.ContextCheckedTracking:
                            return _contextCheckedTabFill;
                        case PaletteState.FocusOverride:
                            return _focusTabFill;
                        case PaletteState.ContextTracking:
                        case PaletteState.ContextCheckedNormal:
                            return GlobalStaticValues.EMPTY_COLOR;
                        case PaletteState.Normal:
                            return GlobalStaticValues.EMPTY_COLOR;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                case PaletteRibbonBackStyle.RibbonAppMenuDocs:
                case PaletteRibbonBackStyle.RibbonGalleryBack:
                case PaletteRibbonBackStyle.RibbonGalleryBorder:
                    return GlobalStaticValues.EMPTY_COLOR;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(style.ToString());
                    break;
            }

            return Color.Red;
        }

        /// <summary>
        /// Gets the third background color for the ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor3(PaletteRibbonBackStyle style, PaletteState state)
        {
            switch (style)
            {
                case PaletteRibbonBackStyle.RibbonAppMenuOuter:
                    return _ribbonColours[(int)SchemeOfficeColors.AppButtonOuter3];
                case PaletteRibbonBackStyle.RibbonQATMinibar:
                    return state == PaletteState.Normal
                        ? _ribbonColours[(int)SchemeOfficeColors.RibbonQATMini3]
                        : _ribbonColours[(int)SchemeOfficeColors.RibbonQATMini3I];

                case PaletteRibbonBackStyle.RibbonQATFullbar:
                    return _ribbonColours[(int)SchemeOfficeColors.RibbonQATFullbar3];
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupCollapsedBorder3];
                        case PaletteState.Tracking:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupCollapsedBorderT3];
                        case PaletteState.ContextNormal:
                            return _ribbonGroupCollapsedBorderContext[2];
                        case PaletteState.ContextTracking:
                        case PaletteState.Pressed:
                            return _ribbonGroupCollapsedBorderContextTracking[2];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                    return state switch
                    {
                        PaletteState.ContextNormal or PaletteState.ContextTracking => GlobalStaticValues.EMPTY_COLOR,
                        _ => _ribbonColours[(int)SchemeOfficeColors.RibbonGroupFrameInside3]
                    };
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupCollapsedBack3];
                        case PaletteState.Tracking:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupCollapsedBackT3];
                        case PaletteState.ContextNormal:
                        case PaletteState.ContextTracking:
                            return GlobalStaticValues.EMPTY_COLOR;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                case PaletteRibbonBackStyle.RibbonAppMenuDocs:
                case PaletteRibbonBackStyle.RibbonAppMenuInner:
                case PaletteRibbonBackStyle.RibbonQATOverflow:
                case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
                case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
                case PaletteRibbonBackStyle.RibbonGalleryBack:
                case PaletteRibbonBackStyle.RibbonGalleryBorder:
                    return GlobalStaticValues.EMPTY_COLOR;
                case PaletteRibbonBackStyle.RibbonAppButton:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _appButtonNormal[2];
                        case PaletteState.Tracking:
                            return _appButtonTrack[2];
                        case PaletteState.Pressed:
                            return _appButtonPressed[2];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                case PaletteRibbonBackStyle.RibbonGroupArea:
                    return state == PaletteState.ContextCheckedNormal ? GlobalStaticValues.EMPTY_COLOR : _ribbonColours[(int)SchemeOfficeColors.RibbonGroupsArea3];

                case PaletteRibbonBackStyle.RibbonTab:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonTabTracking2];
                        case PaletteState.CheckedNormal:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonTabSelected3];
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonTabHighlight3];
                        case PaletteState.ContextTracking:
                        case PaletteState.ContextCheckedNormal:
                        case PaletteState.ContextCheckedTracking:
                        case PaletteState.FocusOverride:
                        case PaletteState.Normal:
                            return GlobalStaticValues.EMPTY_COLOR;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(style.ToString());
                    break;
            }

            return Color.Red;
        }

        /// <summary>
        /// Gets the fourth background color for the ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor4(PaletteRibbonBackStyle style, PaletteState state)
        {
            switch (style)
            {
                case PaletteRibbonBackStyle.RibbonQATMinibar:
                    return state == PaletteState.Normal
                        ? _ribbonColours[(int)SchemeOfficeColors.RibbonQATMini4]
                        : _ribbonColours[(int)SchemeOfficeColors.RibbonQATMini4I];

                case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupCollapsedBorder4];
                        case PaletteState.Tracking:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupCollapsedBorderT4];
                        case PaletteState.ContextNormal:
                            return _ribbonGroupCollapsedBorderContext[3];
                        case PaletteState.ContextTracking:
                        case PaletteState.Pressed:
                            return _ribbonGroupCollapsedBorderContextTracking[3];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                    return state switch
                    {
                        PaletteState.ContextNormal or PaletteState.ContextTracking => GlobalStaticValues.EMPTY_COLOR,
                        _ => _ribbonColours[(int)SchemeOfficeColors.RibbonGroupFrameInside4]
                    };
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupCollapsedBack4];
                        case PaletteState.Tracking:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupCollapsedBackT4];
                        case PaletteState.ContextNormal:
                        case PaletteState.ContextTracking:
                            return GlobalStaticValues.EMPTY_COLOR;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                case PaletteRibbonBackStyle.RibbonAppMenuDocs:
                case PaletteRibbonBackStyle.RibbonAppMenuInner:
                case PaletteRibbonBackStyle.RibbonAppMenuOuter:
                case PaletteRibbonBackStyle.RibbonQATFullbar:
                case PaletteRibbonBackStyle.RibbonQATOverflow:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
                case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
                case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
                case PaletteRibbonBackStyle.RibbonGalleryBack:
                case PaletteRibbonBackStyle.RibbonGalleryBorder:
                    return GlobalStaticValues.EMPTY_COLOR;
                case PaletteRibbonBackStyle.RibbonAppButton:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _appButtonNormal[3];
                        case PaletteState.Tracking:
                            return _appButtonTrack[3];
                        case PaletteState.Pressed:
                            return _appButtonPressed[3];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                case PaletteRibbonBackStyle.RibbonGroupArea:
                    return state == PaletteState.ContextCheckedNormal ? _lightGray : _ribbonColours[(int)SchemeOfficeColors.RibbonGroupsArea2];

                case PaletteRibbonBackStyle.RibbonTab:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonTabTracking2];
                        case PaletteState.CheckedNormal:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonTabSelected4];
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonTabHighlight4];
                        case PaletteState.ContextTracking:
                        case PaletteState.ContextCheckedNormal:
                        case PaletteState.ContextCheckedTracking:
                        case PaletteState.FocusOverride:
                        case PaletteState.Normal:
                            return GlobalStaticValues.EMPTY_COLOR;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(style.ToString());
                    break;
            }

            return Color.Red;
        }

        /// <summary>
        /// Gets the fifth background color for the ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor5(PaletteRibbonBackStyle style, PaletteState state)
        {
            switch (style)
            {
                case PaletteRibbonBackStyle.RibbonAppMenuDocs:
                case PaletteRibbonBackStyle.RibbonAppMenuInner:
                case PaletteRibbonBackStyle.RibbonAppMenuOuter:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
                case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
                case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
                case PaletteRibbonBackStyle.RibbonQATFullbar:
                case PaletteRibbonBackStyle.RibbonQATOverflow:
                case PaletteRibbonBackStyle.RibbonGalleryBack:
                case PaletteRibbonBackStyle.RibbonGalleryBorder:
                    return GlobalStaticValues.EMPTY_COLOR;
                case PaletteRibbonBackStyle.RibbonQATMinibar:
                    return state == PaletteState.Normal
                        ? _ribbonColours[(int)SchemeOfficeColors.RibbonQATMini5]
                        : _ribbonColours[(int)SchemeOfficeColors.RibbonQATMini5I];

                case PaletteRibbonBackStyle.RibbonAppButton:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _appButtonNormal[4];
                        case PaletteState.Tracking:
                            return _appButtonTrack[4];
                        case PaletteState.Pressed:
                            return _appButtonPressed[4];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                case PaletteRibbonBackStyle.RibbonGroupArea:
                    return state == PaletteState.ContextCheckedNormal ? Color.White : _ribbonColours[(int)SchemeOfficeColors.RibbonGroupsArea1];

                case PaletteRibbonBackStyle.RibbonTab:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledText;
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonTabTracking2];
                        case PaletteState.CheckedNormal:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonTabSelected5];
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _ribbonColours[(int)SchemeOfficeColors.RibbonTabHighlight5];
                        case PaletteState.ContextTracking:
                        case PaletteState.ContextCheckedNormal:
                        case PaletteState.ContextCheckedTracking:
                        case PaletteState.FocusOverride:
                        case PaletteState.Normal:
                            return GlobalStaticValues.EMPTY_COLOR;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(state.ToString());
                            break;
                    }
                    break;

                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(style.ToString());
                    break;
            }

            return Color.Red;
        }
        #endregion

        #region RibbonText
        /// <summary>
        /// Gets the =color for the item text.
        /// </summary>
        /// <param name="style">Text style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonTextColor(PaletteRibbonTextStyle style, PaletteState state)
        {
            switch (style)
            {
                case PaletteRibbonTextStyle.RibbonAppMenuDocsTitle:
                case PaletteRibbonTextStyle.RibbonAppMenuDocsEntry:
                    return _ribbonColours[(int)SchemeOfficeColors.AppButtonMenuDocsText];
                case PaletteRibbonTextStyle.RibbonTab:
                case PaletteRibbonTextStyle.RibbonGroupNormalTitle:
                    return state switch
                    {
                        PaletteState.Disabled => _disabledText,
                        PaletteState.CheckedNormal or PaletteState.CheckedPressed or PaletteState.CheckedTracking or PaletteState.ContextCheckedNormal or PaletteState.ContextCheckedTracking or PaletteState.FocusOverride => _ribbonColours[(int)SchemeOfficeColors.RibbonTabTextChecked],
                        _ => _ribbonColours[(int)SchemeOfficeColors.RibbonTabTextNormal]
                    };
                case PaletteRibbonTextStyle.RibbonGroupCollapsedText:
                    return _ribbonColours[(int)SchemeOfficeColors.RibbonGroupCollapsedText];
                case PaletteRibbonTextStyle.RibbonGroupButtonText:
                case PaletteRibbonTextStyle.RibbonGroupLabelText:
                case PaletteRibbonTextStyle.RibbonGroupCheckBoxText:
                case PaletteRibbonTextStyle.RibbonGroupRadioButtonText:
                    return state == PaletteState.Disabled ? _disabledText : _ribbonColours[(int)SchemeOfficeColors.RibbonGroupCollapsedText];

                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(style.ToString());
                    break;
            }

            return Color.Red;
        }
        #endregion

        #region ElementColor
        /// <summary>
        /// Gets the first element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetElementColor1(PaletteElement element, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
            {
                return GlobalStaticValues.EMPTY_COLOR;
            }

            switch (element)
            {
                case PaletteElement.TrackBarTick:
                    return _trackBarColours[0];
                case PaletteElement.TrackBarTrack:
                    return _trackBarColours[1];
                case PaletteElement.TrackBarPosition:
                    return state switch
                    {
                        PaletteState.Disabled => GlobalStaticValues.EMPTY_COLOR,
                        _ => _trackBarColours[4]
                    };
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(element.ToString());
                    break;
            }

            return Color.Red;
        }

        /// <summary>
        /// Gets the second element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Color value.</returns>
        public override Color GetElementColor2(PaletteElement element, PaletteState state)
        {
            if (CommonHelper.IsOverrideState(state))
            {
                return GlobalStaticValues.EMPTY_COLOR;
            }

            switch (element)
            {
                case PaletteElement.TrackBarTick:
                    return _trackBarColours[0];
                case PaletteElement.TrackBarTrack:
                    return _trackBarColours[2];
                case PaletteElement.TrackBarPosition:
                    return state switch
                    {
                        PaletteState.Disabled => ControlPaint.Light(
                            _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBorder]),
                        PaletteState.Normal => _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBorder],
                        PaletteState.Tracking => _buttonBorderColors[1],
                        PaletteState.Pressed => _buttonBorderColors[3],
                        _ => throw DebugTools.NotImplemented(state.ToString())
                    };
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(element.ToString());
                    break;
            }

            return Color.Red;
        }

        /// <summary>
        /// Gets the third element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Color value.</returns>
        public override Color GetElementColor3(PaletteElement element, PaletteState state)
        {
            if (CommonHelper.IsOverrideState(state))
            {
                return GlobalStaticValues.EMPTY_COLOR;
            }

            switch (element)
            {
                case PaletteElement.TrackBarTick:
                    return _trackBarColours[0];
                case PaletteElement.TrackBarTrack:
                    return _trackBarColours[3];
                case PaletteElement.TrackBarPosition:
                    return state switch
                    {
                        PaletteState.Disabled => ControlPaint.LightLight(
                            _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBack1]),
                        PaletteState.Normal => ControlPaint.Light(
                            _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBack1]),
                        PaletteState.Tracking => ControlPaint.Light(_buttonBackColors[2]),
                        PaletteState.Pressed => ControlPaint.Light(_buttonBackColors[4]),
                        _ => throw DebugTools.NotImplemented(state.ToString())
                    };

                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(element.ToString());
                    break;
            }

            return Color.Red;
        }

        /// <summary>
        /// Gets the fourth element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Color value.</returns>
        public override Color GetElementColor4(PaletteElement element, PaletteState state)
        {
            switch (element)
            {
                case PaletteElement.TrackBarTick:
                    if (CommonHelper.IsOverrideState(state))
                    {
                        return GlobalStaticValues.EMPTY_COLOR;
                    }

                    return _trackBarColours[0];
                case PaletteElement.TrackBarTrack:
                    if (CommonHelper.IsOverrideState(state))
                    {
                        return GlobalStaticValues.EMPTY_COLOR;
                    }

                    return _trackBarColours[3];
                case PaletteElement.TrackBarPosition:
                    if (CommonHelper.IsOverrideStateExclude(state, PaletteState.FocusOverride))
                    {
                        return GlobalStaticValues.EMPTY_COLOR;
                    }

                    return state switch
                    {
                        PaletteState.Disabled => ControlPaint.LightLight(_ribbonColours[(int)SchemeOfficeColors.ButtonNormalBack1]),
                        PaletteState.Normal => _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBack1],
                        PaletteState.Tracking or PaletteState.FocusOverride => _buttonBackColors[2],
                        PaletteState.Pressed => _buttonBackColors[4],
                        _ => throw DebugTools.NotImplemented(state.ToString())
                    };
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(element.ToString());
                    break;
            }

            return Color.Red;
        }

        /// <summary>
        /// Gets the fifth element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Color value.</returns>
        public override Color GetElementColor5(PaletteElement element, PaletteState state)
        {
            switch (element)
            {
                case PaletteElement.TrackBarTick:
                    if (CommonHelper.IsOverrideState(state))
                    {
                        return GlobalStaticValues.EMPTY_COLOR;
                    }

                    return _trackBarColours[0];
                case PaletteElement.TrackBarTrack:
                    if (CommonHelper.IsOverrideState(state))
                    {
                        return GlobalStaticValues.EMPTY_COLOR;
                    }

                    return _trackBarColours[3];
                case PaletteElement.TrackBarPosition:
                    if (CommonHelper.IsOverrideStateExclude(state, PaletteState.FocusOverride))
                    {
                        return GlobalStaticValues.EMPTY_COLOR;
                    }

                    return state switch
                    {
                        PaletteState.Disabled => ControlPaint.LightLight(_ribbonColours[(int)SchemeOfficeColors.ButtonNormalBack1]),
                        PaletteState.Normal => _ribbonColours[(int)SchemeOfficeColors.ButtonNormalBack2],
                        PaletteState.Tracking or PaletteState.FocusOverride => _buttonBackColors[3],
                        PaletteState.Pressed => _buttonBackColors[5],
                        _ => throw DebugTools.NotImplemented(state.ToString())
                    };
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(element.ToString());
                    break;
            }

            return Color.Red;
        }
        #endregion

        #region ColorTable
        /// <summary>
        /// Gets access to the color table instance.
        /// </summary>
        public override KryptonColorTable ColorTable => _table ??= new KryptonColorTable2007BlueLightMode(_ribbonColours, InheritBool.True, this);

        #endregion

        #region OnUserPreferenceChanged
        /// <summary>
        /// Handle a change in the user preferences.
        /// </summary>
        /// <param name="sender">Source of event.</param>
        /// <param name="e">Event data.</param>
        protected override void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            // Remove the current table, so it gets regenerated when next requested
            _table = null;

            // Update fonts to reflect any change in system settings
            DefineFonts();

            base.OnUserPreferenceChanged(sender, e);
        }
        #endregion

    }
    #endregion

    #region Class: KryptonColorTable2007BlueLightMode
    /// <summary>
    /// Provide KryptonColorTable2007BlueLightMode values using an array of Color values as the source.
    /// </summary>
    public class KryptonColorTable2007BlueLightMode : KryptonColorTable
    {
        #region Static Fields
        private static readonly Color _menuBorder = Color.FromArgb(134, 134, 134);
        private static readonly Color _menuItemSelectedBegin = Color.FromArgb(209, 226, 244);
        private static readonly Color _menuItemSelectedEnd = Color.FromArgb(230, 239, 249);
        private static readonly Color _contextMenuBackground = Color.FromArgb(230, 239, 249);
        private static readonly Color _checkBackground = Color.FromArgb(189, 213, 239);
        private static readonly Color _buttonSelectedBegin = Color.FromArgb(209, 226, 244);
        private static readonly Color _buttonSelectedEnd = Color.FromArgb(230, 239, 249);
        private static readonly Color _buttonPressedBegin = Color.FromArgb(209, 226, 244);
        private static readonly Color _buttonPressedEnd = Color.FromArgb(230, 239, 249);
        private static readonly Color _buttonCheckedBegin = Color.FromArgb(209, 226, 244);
        private static readonly Color _buttonCheckedEnd = Color.FromArgb(230, 239, 249);
        private static Font _menuToolFont;
        private static Font _statusFont;
        #endregion

        #region Identity
        static KryptonColorTable2007BlueLightMode()
        {
            // Get the font settings from the system
            DefineFonts();

            // We need to notice when system color settings change
            SystemEvents.UserPreferenceChanged += OnUserPreferenceChanged;
        }

        /// <summary>
        /// Initialize a new instance of the KryptonColorTable2007BlueLightMode class.
        /// </summary>
        /// <param name="colours">Source of colours</param>
        /// <param name="roundedEdges">Should have rounded edges.</param>
        /// <param name="palette">Associated palette instance.</param>
        public KryptonColorTable2007BlueLightMode([DisallowNull] Color[] colours,
                                     InheritBool roundedEdges,
                                     PaletteBase palette)
            : base(palette)
        {
            Debug.Assert(colours != null);
            if (colours != null)
            {
                Colours = colours;
            }
            UseRoundedEdges = roundedEdges;
        }
        #endregion

        #region Colors
        /// <summary>
        /// Gets the raw set of colors.
        /// </summary>
        public Color[] Colours { get; }

        #endregion

        #region RoundedEdges
        /// <summary>
        /// Gets a value indicating if rounded edges are required.
        /// </summary>
        public override InheritBool UseRoundedEdges { get; }

        #endregion

        #region ButtonPressed
        #region ButtonPressedBorder
        /// <summary>
        /// Gets the border color for a button being pressed.
        /// </summary>
        public override Color ButtonPressedBorder => Colours[(int)SchemeOfficeColors.ButtonBorder];

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
        public override Color ButtonPressedHighlightBorder => Colours[(int)SchemeOfficeColors.ButtonBorder];

        #endregion
        #endregion

        #region ButtonSelected
        #region ButtonSelectedBorder
        /// <summary>
        /// Gets the border color for a button being selected.
        /// </summary>
        public override Color ButtonSelectedBorder => Colours[(int)SchemeOfficeColors.ButtonBorder];

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
        public override Color ButtonSelectedHighlightBorder => Colours[(int)SchemeOfficeColors.ButtonBorder];

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
        public override Color ButtonCheckedHighlightBorder => Colours[(int)SchemeOfficeColors.ButtonBorder];

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
        public override Color GripLight => Colours[(int)SchemeOfficeColors.GripLight];

        #endregion

        #region GripDark
        /// <summary>
        /// Gets the dark color used to draw grips.
        /// </summary>
        public override Color GripDark => Colours[(int)SchemeOfficeColors.GripDark];

        #endregion
        #endregion

        #region ImageMargin
        #region ImageMarginGradientBegin
        /// <summary>
        /// Gets the starting color for the context menu margin.
        /// </summary>
        public override Color ImageMarginGradientBegin => Colours[(int)SchemeOfficeColors.ImageMargin];

        #endregion

        #region ImageMarginGradientMiddle
        /// <summary>
        /// Gets the middle color for the context menu margin.
        /// </summary>
        public override Color ImageMarginGradientMiddle => Colours[(int)SchemeOfficeColors.ImageMargin];

        #endregion

        #region ImageMarginGradientEnd
        /// <summary>
        /// Gets the ending color for the context menu margin.
        /// </summary>
        public override Color ImageMarginGradientEnd => Colours[(int)SchemeOfficeColors.ImageMargin];

        #endregion

        #region ImageMarginRevealedGradientBegin
        /// <summary>
        /// Gets the starting color for the context menu margin revealed.
        /// </summary>
        public override Color ImageMarginRevealedGradientBegin => Colours[(int)SchemeOfficeColors.ImageMargin];

        #endregion

        #region ImageMarginRevealedGradientMiddle
        /// <summary>
        /// Gets the middle color for the context menu margin revealed.
        /// </summary>
        public override Color ImageMarginRevealedGradientMiddle => Colours[(int)SchemeOfficeColors.ImageMargin];

        #endregion

        #region ImageMarginRevealedGradientEnd
        /// <summary>
        /// Gets the ending color for the context menu margin revealed.
        /// </summary>
        public override Color ImageMarginRevealedGradientEnd => Colours[(int)SchemeOfficeColors.ImageMargin];

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
        public override Color MenuItemSelected => Colours[(int)SchemeOfficeColors.ButtonBorder];

        #endregion

        #region MenuItemPressedGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used when a top-level ToolStripMenuItem is pressed down.
        /// </summary>
        public override Color MenuItemPressedGradientBegin => Colours[(int)SchemeOfficeColors.ToolStripBegin];

        #endregion

        #region MenuItemPressedGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used when a top-level ToolStripMenuItem is pressed down.
        /// </summary>
        public override Color MenuItemPressedGradientEnd => Colours[(int)SchemeOfficeColors.ToolStripEnd];

        #endregion

        #region MenuItemPressedGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used when a top-level ToolStripMenuItem is pressed down.
        /// </summary>
        public override Color MenuItemPressedGradientMiddle => Colours[(int)SchemeOfficeColors.ToolStripMiddle];

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
        public override Color MenuStripGradientBegin => Colours[(int)SchemeOfficeColors.ToolStripBack];

        #endregion

        #region MenuStripGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the MenuStrip.
        /// </summary>
        public override Color MenuStripGradientEnd => Colours[(int)SchemeOfficeColors.ToolStripBack];

        #endregion

        #endregion

        #region OverflowButton
        #region OverflowButtonGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the ToolStripOverflowButton.
        /// </summary>
        public override Color OverflowButtonGradientBegin => Colours[(int)SchemeOfficeColors.OverflowBegin];

        #endregion

        #region OverflowButtonGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStripOverflowButton.
        /// </summary>
        public override Color OverflowButtonGradientEnd => Colours[(int)SchemeOfficeColors.OverflowEnd];

        #endregion

        #region OverflowButtonGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used in the ToolStripOverflowButton.
        /// </summary>
        public override Color OverflowButtonGradientMiddle => Colours[(int)SchemeOfficeColors.OverflowMiddle];

        #endregion
        #endregion

        #region RaftingContainer
        #region RaftingContainerGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the ToolStripContainer.
        /// </summary>
        public override Color RaftingContainerGradientBegin => Colours[(int)SchemeOfficeColors.ToolStripBack];

        #endregion

        #region RaftingContainerGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStripContainer.
        /// </summary>
        public override Color RaftingContainerGradientEnd => Colours[(int)SchemeOfficeColors.ToolStripBack];

        #endregion

        #endregion

        #region Separator
        #region SeparatorLight
        /// <summary>
        /// Gets the light separator color.
        /// </summary>
        public override Color SeparatorLight => Colours[(int)SchemeOfficeColors.SeparatorLight];

        #endregion

        #region SeparatorDark
        /// <summary>
        /// Gets the dark separator color.
        /// </summary>
        public override Color SeparatorDark => Colours[(int)SchemeOfficeColors.SeparatorDark];

        #endregion
        #endregion

        #region StatusStrip
        #region StatusStripGradientBegin
        /// <summary>
        /// Gets the starting color for the status strip background.
        /// </summary>
        public override Color StatusStripGradientBegin => Colours[(int)SchemeOfficeColors.StatusStripLight];

        #endregion

        #region StatusStripGradientEnd
        /// <summary>
        /// Gets the ending color for the status strip background.
        /// </summary>
        public override Color StatusStripGradientEnd => Colours[(int)SchemeOfficeColors.StatusStripDark];

        #endregion
        #endregion

        #region Text
        #region MenuItemText
        /// <summary>
        /// Gets the text color used on the menu items.
        /// </summary>
        public override Color MenuItemText => Colours[(int)SchemeOfficeColors.TextButtonNormal];

        #endregion

        #region MenuStripText
        /// <summary>
        /// Gets the text color used on the menu strip.
        /// </summary>
        public override Color MenuStripText => Colours[(int)SchemeOfficeColors.TextLabelPanel];

        #endregion

        #region ToolStripText
        /// <summary>
        /// Gets the text color used on the tool strip.
        /// </summary>
        public override Color ToolStripText => Colours[(int)SchemeOfficeColors.TextButtonNormal];

        #endregion

        #region StatusStripText
        /// <summary>
        /// Gets the text color used on the status strip.
        /// </summary>
        public override Color StatusStripText => Colours[(int)SchemeOfficeColors.StatusStripText];

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
        public override Color ToolStripBorder => Colours[(int)SchemeOfficeColors.ToolStripBorder];

        #endregion

        #region ToolStripContentPanelGradientBegin
        /// <summary>
        /// Gets the starting color for the content panel background.
        /// </summary>
        public override Color ToolStripContentPanelGradientBegin => Colours[(int)SchemeOfficeColors.ToolStripBack];

        #endregion

        #region ToolStripContentPanelGradientEnd
        /// <summary>
        /// Gets the ending color for the content panel background.
        /// </summary>
        public override Color ToolStripContentPanelGradientEnd => Colours[(int)SchemeOfficeColors.ToolStripBack];

        #endregion

        #region ToolStripDropDownBackground
        /// <summary>
        /// Gets the background color for drop down menus.
        /// </summary>
        public override Color ToolStripDropDownBackground => _contextMenuBackground;

        #endregion

        #region ToolStripGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the ToolStrip background.
        /// </summary>
        public override Color ToolStripGradientBegin => Colours[(int)SchemeOfficeColors.ToolStripBegin];

        #endregion

        #region ToolStripGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStrip background.
        /// </summary>
        public override Color ToolStripGradientEnd => Colours[(int)SchemeOfficeColors.ToolStripEnd];

        #endregion

        #region ToolStripGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used in the ToolStrip background.
        /// </summary>
        public override Color ToolStripGradientMiddle => Colours[(int)SchemeOfficeColors.ToolStripMiddle];

        #endregion

        #region ToolStripPanelGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the ToolStripPanel.
        /// </summary>
        public override Color ToolStripPanelGradientBegin => Colours[(int)SchemeOfficeColors.ToolStripBack];

        #endregion

        #region ToolStripPanelGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStripPanel.
        /// </summary>
        public override Color ToolStripPanelGradientEnd => Colours[(int)SchemeOfficeColors.ToolStripBack];

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
}
