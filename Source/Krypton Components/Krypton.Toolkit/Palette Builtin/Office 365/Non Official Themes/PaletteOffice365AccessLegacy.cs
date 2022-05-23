namespace Krypton.Toolkit
{
    public class PaletteOffice365Accesslegacy : PaletteOffice365Base
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

        private static readonly Image _blueDropDownButton = Office2010Arrows._2010BlueDropDownButton;
        private static readonly Image _contextMenuSubMenu = Office2010Arrows._2010BlueContextMenuSub;
        private static readonly Image _formCloseNormal = Office2010ControlBoxResources.Office2010BlueCloseNormal_25_x_23;
        private static readonly Image _formCloseDisabled = Office2010ControlBoxResources.Office2010BlueCloseDisabled_25_x_23;
        private static readonly Image _formCloseHover = Office2010ControlBoxResources.Office2010BlueCloseHover_25_x_23;
        private static readonly Image _formClosePressed = Office2010ControlBoxResources.Office2010BlueClosePressed_25_x_23;
        private static readonly Image _formMaximiseNormal = Office2010ControlBoxResources.Office2010BlueMaximiseNormal_25_x_23;
        private static readonly Image _formMaximiseDisabled = Office2010ControlBoxResources.Office2010BlueMaximiseDisabled_25_x_23;
        private static readonly Image _formMaximiseHover = Office2010ControlBoxResources.Office2010BlueMaximiseHover_25_x_23;
        private static readonly Image _formMaximisePressed = Office2010ControlBoxResources.Office2010BlueMaximisePressed_13_x_12;
        private static readonly Image _formMinimiseNormal = Office2010ControlBoxResources.Office2010BlueMinimiseNormal_25_x_23;
        private static readonly Image _formMinimiseHover = Office2010ControlBoxResources.Office2010BlueMinimiseHover_25_x_23;
        private static readonly Image _formMinimiseDisabled = Office2010ControlBoxResources.Office2010BlueMinimiseDisabled_25_x_23;
        private static readonly Image _formMinimisePressed = Office2010ControlBoxResources.Office2010BlueMinimisePressed_25_x_23;
        private static readonly Image _formRestoreNormal = Office2010ControlBoxResources.Office2010BlueRestoreNormal_25_x_23;
        private static readonly Image _formRestoreDisabled = Office2010ControlBoxResources.Office2010BlueRestoreDisabled_25_x_23;
        private static readonly Image _formRestoreHover = Office2010ControlBoxResources.Office2010BlueRestoreHover_25_x_23;
        private static readonly Image _formRestorePressed = Office2010ControlBoxResources.Office2010BlueRestorePressed_25_x_23;
        private static readonly Image _formHelpNormal = HelpIconResources.Office365HelpIconNormal;
        private static readonly Image _formHelpHover = HelpIconResources.Office365HelpIconHover;
        private static readonly Image _formHelpPressed = HelpIconResources.Office365HelpIconPressed;
        private static readonly Image _formHelpDisabled = HelpIconResources.Office365HelpIconDisabled;

        #endregion

        #region Colour Arrays

        private static readonly Color[] _trackBarColors =
        {
            Color.FromArgb(160, 61, 103), // Tick marks
            Color.FromArgb(160, 61, 102), // Top track
            Color.FromArgb(180, 89, 128), // Bottom track
            Color.FromArgb(180, 89, 128), // Fill track
            Color.FromArgb(64, Color.White), // Outside position
            Color.FromArgb(163, 65, 106) // Border (normal) position
        };

        private static readonly Color[] _schemeOfficeColors =
        {
            Color.FromArgb(255, 255, 255), // TextLabelControl
            Color.FromArgb(255, 255, 255), // TextButtonNormal
            Color.Blue, // TextButtonChecked
            Color.FromArgb(210, 140, 170), // ButtonNormalBorder
            Color.FromArgb(155, 60, 102), // ButtonNormalDefaultBorder
            Color.FromArgb(219, 148, 179), // ButtonNormalBack1
            Color.FromArgb(235, 192, 211), // ButtonNormalBack2
            Color.FromArgb(170, 60, 104), // ButtonNormalDefaultBack1
            Color.FromArgb(221, 150, 170), // ButtonNormalDefaultBack2
            Color.FromArgb(175, 60, 105), // ButtonNormalNavigatorBack1
            Color.FromArgb(210, 100, 150), // ButtonNormalNavigatorBack2
            Color.FromArgb(189, 99, 138), // PanelClient
            Color.FromArgb(154, 55, 98), // PanelAlternative
            Color.FromArgb(160, 60, 107), // ControlBorder
            Color.FromArgb(225, 105, 145), // SeparatorHighBorder1
            Color.FromArgb(182, 105, 130), // SeparatorHighBorder2
            Color.FromArgb(230, 100, 125), // HeaderPrimaryBack1
            Color.FromArgb(210, 150, 160), // HeaderPrimaryBack2
            Color.FromArgb(214, 232, 255), // HeaderSecondaryBack1
            Color.FromArgb(214, 232, 255), // HeaderSecondaryBack2
            Color.FromArgb(255, 255, 255), // HeaderText
            Color.FromArgb(255, 255, 255), // StatusStripText
            Color.FromArgb(161, 62, 100), // ButtonBorder
            Color.FromArgb(255, 255, 255), // SeparatorLight
            Color.FromArgb(150, 98, 100), // SeparatorDark
            Color.FromArgb(248, 248, 248), // GripLight
            Color.FromArgb(164, 52, 104), // GripDark
            Color.FromArgb(189, 99, 138), // ToolStripBack
            Color.FromArgb(245, 195, 199), // StatusStripLight
            Color.FromArgb(172, 61, 108), // StatusStripDark
            Color.FromArgb(233, 238, 238), // ImageMargin
            Color.FromArgb(230, 190, 200), // ToolStripBegin
            Color.FromArgb(225, 185, 195), // ToolStripMiddle
            Color.FromArgb(195, 104, 143), // ToolStripEnd
            Color.FromArgb(215, 110, 160), // OverflowBegin
            Color.FromArgb(220, 120, 170), // OverflowMiddle
            Color.FromArgb(189, 60, 100), // OverflowEnd
            Color.FromArgb(170, 60, 90), // ToolStripBorder
            Color.FromArgb(160, 59, 90), // FormBorderActive
            Color.FromArgb(192, 198, 206), // FormBorderInactive
            Color.FromArgb(220, 160, 170), // FormBorderActiveLight
            Color.FromArgb(150, 60, 105), // FormBorderActiveDark
            Color.FromArgb(204, 170, 170), // FormBorderInactiveLight
            Color.FromArgb(230, 70, 105), // FormBorderInactiveDark
            Color.FromArgb(221, 233, 248), // FormBorderHeaderActive
            Color.FromArgb(223, 229, 237), // FormBorderHeaderInactive
            Color.FromArgb(215, 115, 160), // FormBorderHeaderActive1
            Color.FromArgb(228, 239, 253), // FormBorderHeaderActive2
            Color.FromArgb(220, 150, 170), // FormBorderHeaderInctive1
            Color.FromArgb(227, 232, 239), // FormBorderHeaderInctive2
            Color.FromArgb(140, 50, 90), // FormHeaderShortActive
            Color.FromArgb(160, 160, 160), // FormHeaderShortInactive
            Color.FromArgb(105, 112, 121), // FormHeaderLongActive
            Color.FromArgb(160, 160, 160), // FormHeaderLongInactive
            Color.FromArgb(250, 95, 140), // FormButtonBorderTrack
            Color.FromArgb(210, 228, 254), // FormButtonBack1Track
            Color.FromArgb(255, 255, 255), // FormButtonBack2Track
            Color.FromArgb(220, 155, 199), // FormButtonBorderPressed
            Color.FromArgb(240, 180, 230), // FormButtonBack1Pressed
            Color.FromArgb(245, 185, 195), // FormButtonBack2Pressed
            Color.FromArgb(255, 255, 255), // TextButtonFormNormal
            Color.FromArgb(255, 255, 255), // TextButtonFormTracking
            Color.FromArgb(255, 255, 255), // TextButtonFormPressed
            Color.Blue, // LinkNotVisitedOverrideControl
            Color.Purple, // LinkVisitedOverrideControl
            Color.Red, // LinkPressedOverrideControl
            Color.Blue, // LinkNotVisitedOverridePanel
            Color.Purple, // LinkVisitedOverridePanel
            Color.Red, // LinkPressedOverridePanel
            Color.FromArgb(255, 255, 255), // TextLabelPanel
            Color.FromArgb(255, 255, 255), // RibbonTabTextNormal
            Color.FromArgb(255, 255, 255), // RibbonTabTextChecked
            Color.FromArgb(150, 60, 95), // RibbonTabSelected1
            Color.FromArgb(235, 155, 160), // RibbonTabSelected2
            Color.FromArgb(246, 250, 255), // RibbonTabSelected3
            Color.FromArgb(239, 246, 254), // RibbonTabSelected4
            Color.FromArgb(222, 232, 245), // RibbonTabSelected5
            Color.FromArgb(145, 50, 90), // RibbonTabTracking1
            Color.FromArgb(255, 180, 86), // RibbonTabTracking2
            Color.FromArgb(255, 255, 189), // RibbonTabHighlight1
            Color.FromArgb(249, 237, 198), // RibbonTabHighlight2
            Color.FromArgb(218, 185, 127), // RibbonTabHighlight3
            Color.FromArgb(254, 209, 94), // RibbonTabHighlight4
            Color.FromArgb(205, 209, 180), // RibbonTabHighlight5
            Color.FromArgb(116, 50, 95), // RibbonTabSeparatorColor
            Color.FromArgb(120, 55, 80), // RibbonGroupsArea1
            Color.FromArgb(235, 155, 165), // RibbonGroupsArea2
            Color.FromArgb(201, 145, 160), // RibbonGroupsArea3
            Color.FromArgb(231, 242, 255), // RibbonGroupsArea4
            Color.FromArgb(219, 230, 244), // RibbonGroupsArea5
            Color.FromArgb(150, 70, 90), // RibbonGroupBorder1
            Color.FromArgb(230, 150, 170), // RibbonGroupBorder2
            Color.FromArgb(235, 155, 175), // RibbonGroupTitle1
            Color.FromArgb(235, 155, 175), // RibbonGroupTitle2
            Color.FromArgb(202, 202, 202), // RibbonGroupBorderContext1
            Color.FromArgb(196, 196, 196), // RibbonGroupBorderContext2
            Color.FromArgb(223, 223, 245), // RibbonGroupTitleContext1
            Color.FromArgb(210, 221, 242), // RibbonGroupTitleContext2
            Color.FromArgb(145, 60, 90), // RibbonGroupDialogDark
            Color.FromArgb(254, 254, 255), // RibbonGroupDialogLight
            Color.FromArgb(200, 150, 180), // RibbonGroupTitleTracking1
            Color.FromArgb(214, 160, 200), // RibbonGroupTitleTracking2
            Color.FromArgb(175, 65, 95), // RibbonMinimizeBarDark
            Color.FromArgb(213, 165, 190), // RibbonMinimizeBarLight
            Color.FromArgb(240, 152, 190), // RibbonGroupCollapsedBorder1
            Color.FromArgb(248, 165, 180), // RibbonGroupCollapsedBorder2
            Color.FromArgb(64, Color.White), // RibbonGroupCollapsedBorder3
            Color.FromArgb(202, 165, 170), // RibbonGroupCollapsedBorder4
            Color.FromArgb(221, 233, 249), // RibbonGroupCollapsedBack1
            Color.FromArgb(201, 160, 165), // RibbonGroupCollapsedBack2
            Color.FromArgb(199, 165, 170), // RibbonGroupCollapsedBack3
            Color.FromArgb(214, 238, 252), // RibbonGroupCollapsedBack4
            Color.FromArgb(200, 160, 150), // RibbonGroupCollapsedBorderT1
            Color.FromArgb(215, 170, 185), // RibbonGroupCollapsedBorderT2
            Color.FromArgb(192, Color.White), // RibbonGroupCollapsedBorderT3
            Color.FromArgb(247, 251, 254), // RibbonGroupCollapsedBorderT4
            Color.FromArgb(240, 244, 250), // RibbonGroupCollapsedBackT1
            Color.FromArgb(226, 234, 245), // RibbonGroupCollapsedBackT2
            Color.FromArgb(216, 227, 241), // RibbonGroupCollapsedBackT3
            Color.FromArgb(214, 237, 253), // RibbonGroupCollapsedBackT4
            Color.FromArgb(240, 150, 185), // RibbonGroupFrameBorder1
            Color.FromArgb(245, 155, 195), // RibbonGroupFrameBorder2
            Color.FromArgb(227, 237, 250), // RibbonGroupFrameInside1
            Color.FromArgb(221, 233, 248), // RibbonGroupFrameInside2
            Color.FromArgb(214, 228, 246), // RibbonGroupFrameInside3
            Color.FromArgb(227, 236, 248), // RibbonGroupFrameInside4
            Color.FromArgb(255, 255, 255), // RibbonGroupCollapsedText
            Color.FromArgb(178, 70, 98), // AlternatePressedBack1
            Color.FromArgb(220, 155, 190), // AlternatePressedBack2
            Color.FromArgb(235, 160, 185), // AlternatePressedBorder1
            Color.FromArgb(240, 188, 195), // AlternatePressedBorder2
            Color.FromArgb(255, 150, 170), // FormButtonBack1Checked
            Color.FromArgb(255, 160, 180), // FormButtonBack2Checked
            Color.FromArgb(158, 65, 95), // FormButtonBorderCheck
            Color.FromArgb(240, 184, 199), // FormButtonBack1CheckTrack
            Color.FromArgb(225, 241, 255), // FormButtonBack2CheckTrack
            Color.FromArgb(237, 167, 177), // RibbonQATMini1
            Color.FromArgb(219, 231, 247), // RibbonQATMini2
            Color.FromArgb(240, 160, 195), // RibbonQATMini3
            Color.FromArgb(128, Color.White), // RibbonQATMini4
            Color.FromArgb(72, Color.White), // RibbonQATMini5                              
            Color.FromArgb(175, 65, 85), // RibbonQATMini1I
            Color.FromArgb(226, 233, 241), // RibbonQATMini2I
            Color.FromArgb(230, 165, 170), // RibbonQATMini3I
            Color.FromArgb(128, Color.White), // RibbonQATMini4I
            Color.FromArgb(72, Color.White), // RibbonQATMini5I                                                      
            Color.FromArgb(245, 155, 185), // RibbonQATFullbar1                                                      
            Color.FromArgb(250, 160, 190), // RibbonQATFullbar2                                                      
            Color.FromArgb(126, 65, 95), // RibbonQATFullbar3                                                      
            Color.FromArgb(145, 55, 95), // RibbonQATButtonDark                                                      
            Color.FromArgb(234, 242, 249), // RibbonQATButtonLight                                                      
            Color.FromArgb(225, 155, 185), // RibbonQATOverflow1                                                      
            Color.FromArgb(153, 50, 80), // RibbonQATOverflow2                                                      
            Color.FromArgb(195, 76, 93), // RibbonGroupSeparatorDark                                                      
            Color.FromArgb(248, 250, 252), // RibbonGroupSeparatorLight                                                                   
            Color.FromArgb(240, 162, 185), // ButtonClusterButtonBack1                                                      
            Color.FromArgb(238, 169, 178), // ButtonClusterButtonBack2                                                      
            Color.FromArgb(155, 83, 94), // ButtonClusterButtonBorder1                                                      
            Color.FromArgb(175, 60, 91), // ButtonClusterButtonBorder2                                                      
            Color.FromArgb(233, 168, 182), // NavigatorMiniBackColor                                                    
            Color.White, // GridListNormal1                                                    
            Color.FromArgb(245, 165, 190), // GridListNormal2                                                    
            Color.FromArgb(240, 160, 185), // GridListPressed1                                                    
            Color.FromArgb(252, 253, 255), // GridListPressed2                                                    
            Color.FromArgb(250, 145, 180), // GridListSelected                                                    
            Color.FromArgb(249, 252, 253), // GridSheetColNormal1                                                    
            Color.FromArgb(211, 219, 233), // GridSheetColNormal2                                                    
            Color.FromArgb(223, 226, 228), // GridSheetColPressed1                   
            Color.FromArgb(210, 162, 180), // GridSheetColPressed2                                                    
            Color.FromArgb(249, 217, 159), // GridSheetColSelected1
            Color.FromArgb(241, 193, 95), // GridSheetColSelected2
            Color.FromArgb(228, 236, 247), // GridSheetRowNormal                                                   
            Color.FromArgb(240, 156, 180), // GridSheetRowPressed
            Color.FromArgb(255, 213, 141), // GridSheetRowSelected
            Color.FromArgb(188, 195, 209), // GridDataCellBorder
            Color.FromArgb(194, 147, 150), // GridDataCellSelected
            Color.Blue, // InputControlTextNormal
            Color.FromArgb(172, 168, 153), // InputControlTextDisabled
            Color.FromArgb(196, 153, 178), // InputControlBorderNormal
            Color.FromArgb(177, 187, 198), // InputControlBorderDisabled
            Color.FromArgb(255, 255, 255), // InputControlBackNormal
            SystemColors.Control, // InputControlBackDisabled
            Color.FromArgb(234, 242, 251), // InputControlBackInactive
            Color.FromArgb(126, 50, 77), // InputDropDownNormal1
            Color.FromArgb(255, 248, 203), // InputDropDownNormal2
            Color.FromArgb(172, 168, 153), // InputDropDownDisabled1
            Color.Transparent, // InputDropDownDisabled2
            Color.FromArgb(221, 231, 238), // ContextMenuHeadingBack
            Color.FromArgb(130, 21, 70), // ContextMenuHeadingText
            Color.FromArgb(233, 238, 238), // ContextMenuImageColumn
            Color.White, // AppButtonBack1
            Color.FromArgb(201, 138, 170), // AppButtonBack2
            Color.FromArgb(210, 145, 172), // AppButtonBorder
            Color.FromArgb(215, 140, 168), // AppButtonOuter1
            Color.FromArgb(216, 146, 167), // AppButtonOuter2
            Color.FromArgb(207, 147, 168), // AppButtonOuter3
            Color.White, // AppButtonInner1
            Color.FromArgb(217, 148, 162), // AppButtonInner2
            Color.FromArgb(233, 234, 238), // AppButtonMenuDocs
            Color.FromArgb(140, 21, 76), // AppButtonMenuDocsText
            Color.FromArgb(227, 239, 255), // SeparatorHighInternalBorder1
            Color.FromArgb(210, 164, 175), // SeparatorHighInternalBorder2
            Color.FromArgb(215, 172, 180), // RibbonGalleryBorder
            Color.FromArgb(212, 130, 160), // RibbonGalleryBackNormal
            Color.FromArgb(236, 243, 251), // RibbonGalleryBackTracking
            Color.FromArgb(240, 145, 158), // RibbonGalleryBack1
            Color.FromArgb(235, 140, 151), // RibbonGalleryBack2
            Color.FromArgb(214, 222, 234), // RibbonTabTracking3
            Color.FromArgb(200, 215, 233), // RibbonTabTracking4
            Color.FromArgb(147, 167, 195), // RibbonGroupBorder3
            Color.FromArgb(226, 236, 247), // RibbonGroupBorder4
            Color.FromArgb(251, 251, 252), // RibbonGroupBorder5
            Color.FromArgb(56, 78, 115), // RibbonGroupTitleText
            Color.FromArgb(151, 156, 163), // RibbonDropArrowLight
            Color.FromArgb(39, 49, 60), // RibbonDropArrowDark
            Color.FromArgb(208, 226, 248), // HeaderDockInactiveBack1
            Color.FromArgb(178, 196, 218), // HeaderDockInactiveBack2
            Color.FromArgb(133, 158, 191), // ButtonNavigatorBorder
            Color.FromArgb(0, 25, 56), // ButtonNavigatorText
            Color.FromArgb(177, 198, 224), // ButtonNavigatorTrack1
            Color.FromArgb(211, 224, 240), // ButtonNavigatorTrack2
            Color.FromArgb(148, 174, 205), // ButtonNavigatorPressed1
            Color.FromArgb(198, 214, 231), // ButtonNavigatorPressed2
            Color.FromArgb(200, 219, 240), // ButtonNavigatorChecked1
            Color.FromArgb(177, 201, 228), // ButtonNavigatorChecked2
            Color.FromArgb(201, 217, 239) // ToolTipBottom                                                                      
        };

        #endregion
        #endregion

        #region Constructors
        static PaletteOffice365Accesslegacy()
        {
            _checkBoxList = new ImageList
            {
                ImageSize = new Size(13, 13),
                ColorDepth = ColorDepth.Depth24Bit
            };
            _checkBoxList.Images.AddStrip(CheckBoxStripResources.CheckBoxStrip2010Blue);
            _galleryButtonList = new ImageList
            {
                ImageSize = new Size(13, 7),
                ColorDepth = ColorDepth.Depth24Bit,
                TransparentColor = Color.Magenta
            };
            _galleryButtonList.Images.AddStrip(GalleryImageResources.Gallery2010);
            _radioButtonArray = new Image[]{Office2010BlueRadioButtonResources.RadioButton2010BlueD,
                Office2010BlueRadioButtonResources.RadioButton2010BlueN,
                Office2010BlueRadioButtonResources.RadioButton2010BlueT,
                Office2010BlueRadioButtonResources.RadioButton2010BlueP,
                Office2010BlueRadioButtonResources.RadioButton2010BlueDC,
                Office2010BlueRadioButtonResources.RadioButton2010BlueNC,
                Office2010BlueRadioButtonResources.RadioButton2010BlueTC,
                Office2010BlueRadioButtonResources.RadioButton2010BluePC};
        }

        /// <summary>
        /// Initializes a new instance of the PaletteOffice365Blue class.
        /// </summary>
        public PaletteOffice365Accesslegacy() : base(_schemeOfficeColors, _checkBoxList, _galleryButtonList, _radioButtonArray, _trackBarColors)
        {

        }
        #endregion

        #region Images        
        /// <summary>
        /// Gets a drop down button image appropriate for the provided state.
        /// </summary>
        /// <param name="state">PaletteState for which image is required.</param>
        /// <returns></returns>
        public override Image GetDropDownButtonImage(PaletteState state) => state != PaletteState.Disabled ? _blueDropDownButton : base.GetDropDownButtonImage(state);

        /// <summary>
        /// Gets an image indicating a sub-menu on a context menu item.
        /// </summary>
        /// <returns>
        /// Appropriate image for drawing; otherwise null.
        /// </returns>
        public override Image GetContextMenuSubMenuImage() => _contextMenuSubMenu;

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
        public override Image GetButtonSpecImage(PaletteButtonSpecStyle style, PaletteState state)
        {
            return style switch
            {
                PaletteButtonSpecStyle.FormClose => state switch
                {
                    PaletteState.Tracking => _formCloseHover,
                    PaletteState.Normal => _formCloseNormal,
                    PaletteState.Pressed => _formClosePressed,
                    _ => _formCloseDisabled
                },
                PaletteButtonSpecStyle.FormMin => state switch
                {
                    PaletteState.Normal => _formMinimiseNormal,
                    PaletteState.Tracking => _formMinimiseHover,
                    PaletteState.Pressed => _formMinimisePressed,
                    _ => _formMinimiseDisabled
                },
                PaletteButtonSpecStyle.FormMax => state switch
                {
                    PaletteState.Normal => _formMaximiseNormal,
                    PaletteState.Tracking => _formMaximiseHover,
                    PaletteState.Pressed => _formMaximisePressed,
                    _ => _formMaximiseDisabled
                },
                PaletteButtonSpecStyle.FormRestore => state switch
                {
                    PaletteState.Normal => _formRestoreNormal,
                    PaletteState.Tracking => _formRestoreHover,
                    PaletteState.Pressed => _formRestorePressed,
                    _ => _formRestoreDisabled
                },
                PaletteButtonSpecStyle.FormHelp => state switch
                {
                    PaletteState.Tracking => _formHelpHover,
                    PaletteState.Pressed => _formHelpPressed,
                    PaletteState.Normal => _formHelpNormal,
                    _ => _formHelpDisabled
                },
                _ => base.GetButtonSpecImage(style, state)
            };
        }
        #endregion
    }
}
