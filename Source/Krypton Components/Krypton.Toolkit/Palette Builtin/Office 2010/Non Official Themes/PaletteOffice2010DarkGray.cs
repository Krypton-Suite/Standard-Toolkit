#region BSD License
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
    public class PaletteOffice2010DarkGray : PaletteOffice2010Base
    {
        #region Static Fields

        #region Colors

        private readonly Color _tabRowBackgroundGradientRaftingDarkColor = Color.FromArgb(51, 51, 51);

        private readonly Color _tabRowBackgroundGradientRaftingLightColor = Color.FromArgb(212, 212, 212);

        #endregion

        #region Ribbon Specific Colors

        private static readonly Color _ribbonAppButtonDarkColor = Color.FromArgb(51, 51, 51);

        private static readonly Color _ribbonAppButtonLightColor = Color.FromArgb(140, 140, 140);

        private static readonly Color _ribbonAppButtonTextColor = SystemColors.Control;

        #endregion

        #region Rafting

        private readonly float _gradientRafting = GlobalStaticValues.DEFAULT_RAFTING_RIBBON_TAB_BACKGROUND_GRADIENT;

        #endregion

        #region Image Lists

        private static readonly ImageList _checkBoxList;
        private static readonly ImageList _galleryButtonList;

        #endregion

        #region Images

        private static readonly Image?[] _radioButtonArray;
        private static readonly Image? _silverDropDownButton = Office2010ArrowResources.Office2010BlueDropDownButton;
        private static readonly Image? _contextMenuSubMenu = Office2010ArrowResources.Office2010BlueContextMenuSub;
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
        private static readonly Image _formHelpNormal = Office2010ControlBoxResources.Office2010HelpIconNormal;
        private static readonly Image _formHelpActive = Office2010ControlBoxResources.Office2010HelpIconHover;
        private static readonly Image _formHelpPressed = Office2010ControlBoxResources.Office2010HelpIconPressed;
        private static readonly Image _formHelpDisabled = Office2010ControlBoxResources.Office2010HelpIconDisabled;

        #endregion

        #region Colour Arrays

        private static readonly Color[] _trackBarColors =
        [
            Color.FromArgb(170, 170, 170), // Tick marks
            Color.FromArgb(166, 170, 175), // Top track
            Color.FromArgb(226, 220, 235), // Bottom track
            Color.FromArgb(206, 200, 215), // Fill track
            Color.FromArgb(64, Color.White), // Outside position
            Color.FromArgb(80, 81, 82) // Border (normal) position
        ];

        private static readonly Color[] _schemeOfficeColors =
        [
            Color.FromArgb(119, 119, 119), // TextLabelControl
            Color.FromArgb(255, 255, 255), // TextButtonNormal
            Color.Black, // TextButtonChecked
            Color.FromArgb(141, 148, 157), // ButtonNormalBorder1
            Color.FromArgb(131, 138, 147), // ButtonNormalBorder2
            Color.FromArgb(212, 212, 212), // ButtonNormalBack1
            Color.FromArgb(222, 222, 222), // ButtonNormalBack2
            Color.FromArgb(186, 185, 206), // ButtonNormalDefaultBack1
            Color.FromArgb(222, 226, 236), // ButtonNormalDefaultBack2
            Color.FromArgb(202, 204, 214), // ButtonNormalNavigatorBack1
            Color.FromArgb(222, 226, 236), // ButtonNormalNavigatorBack2
            Color.FromArgb(212, 212, 212), // PanelClient
            Color.FromArgb(51, 51, 51), // PanelAlternative
            Color.FromArgb(111, 112, 116), // ControlBorder
            Color.FromArgb(240, 241, 242), // SeparatorHighBorder1
            Color.FromArgb(195, 200, 206), // SeparatorHighBorder2
            Color.FromArgb(246, 247, 248), // HeaderPrimaryBack1
            Color.FromArgb(218, 223, 230), // HeaderPrimaryBack2
            Color.FromArgb(213, 219, 231), // HeaderSecondaryBack1
            Color.FromArgb(213, 219, 231), // HeaderSecondaryBack2
            Color.FromArgb(21, 66, 139), // HeaderText
            Color.FromArgb(255, 255, 255), // StatusStripText
            Color.FromArgb(155, 163, 167), // ButtonBorder
            Color.FromArgb(153, 153, 153), // SeparatorLight
            Color.FromArgb(101, 101, 101), // SeparatorDark
            Color.FromArgb(153, 153, 153), // GripLight
            Color.FromArgb(101, 101, 101), // GripDark
            Color.FromArgb(139, 139, 139), // ToolStripBack
            Color.FromArgb(153, 153, 153), // StatusStripLight
            Color.FromArgb(101, 101, 101), // StatusStripDark
            Color.FromArgb(239, 239, 239), // ImageMargin
            Color.FromArgb(153, 153, 153), // ToolStripBegin
            Color.FromArgb(126, 126, 126), // ToolStripMiddle
            Color.FromArgb(101, 101, 101), // ToolStripEnd
            Color.FromArgb(153, 153, 153), // OverflowBegin
            Color.FromArgb(126, 126, 126), // OverflowMiddle
            Color.FromArgb(101, 101, 101), // OverflowEnd
            Color.FromArgb(124, 124, 148), // ToolStripBorder
            Color.FromArgb(114, 120, 128), // FormBorderActive
            Color.FromArgb(180, 185, 192), // FormBorderInactive
            Color.FromArgb(222, 221, 222), // FormBorderActiveLight
            Color.FromArgb(187, 186, 186), // FormBorderActiveDark
            Color.FromArgb(240, 240, 240), // FormBorderInactiveLight
            Color.FromArgb(224, 224, 224), // FormBorderInactiveDark
            Color.FromArgb(172, 175, 183), // FormBorderHeaderActive
            Color.FromArgb(182, 181, 181), // FormBorderHeaderInactive
            Color.FromArgb(192, 195, 202), // FormBorderHeaderActive1
            Color.FromArgb(240, 243, 250), // FormBorderHeaderActive2
            Color.FromArgb(217, 219, 225), // FormBorderHeaderInctive1
            Color.FromArgb(244, 247, 251), // FormBorderHeaderInctive2
            Color.FromArgb(53, 110, 170), // FormHeaderShortActive
            Color.FromArgb(138, 138, 138), // FormHeaderShortInactive
            Color.FromArgb(92, 98, 106), // FormHeaderLongActive
            Color.FromArgb(138, 138, 138), // FormHeaderLongInactive
            Color.FromArgb(189, 199, 212), // FormButtonBorderTrack
            Color.FromArgb(222, 230, 242), // FormButtonBack1Track
            Color.FromArgb(255, 255, 255), // FormButtonBack2Track
            Color.FromArgb(149, 154, 160), // FormButtonBorderPressed
            Color.FromArgb(125, 131, 140), // FormButtonBack1Pressed
            Color.FromArgb(213, 226, 233), // FormButtonBack2Pressed
            Color.Black, // TextButtonFormNormal
            Color.Black, // TextButtonFormTracking
            Color.Black, // TextButtonFormPressed
            Color.Silver, // LinkNotVisitedOverrideControl
            Color.Purple, // LinkVisitedOverrideControl
            Color.Red, // LinkPressedOverrideControl
            Color.Silver, // LinkNotVisitedOverridePanel
            Color.Purple, // LinkVisitedOverridePanel
            Color.Red, // LinkPressedOverridePanel
            Color.FromArgb(119, 119, 119), // TextLabelPanel
            Color.FromArgb(68, 68, 68), // RibbonTabTextNormal
            Color.FromArgb(68, 68, 68), // RibbonTabTextChecked
            Color.FromArgb(190, 190, 190), // RibbonTabSelected1
            Color.FromArgb(198, 250, 255), // RibbonTabSelected2
            Color.FromArgb(247, 248, 249), // RibbonTabSelected3
            Color.FromArgb(245, 245, 247), // RibbonTabSelected4
            Color.FromArgb(239, 234, 241), // RibbonTabSelected5
            Color.FromArgb(189, 190, 193), // RibbonTabTracking1
            Color.FromArgb(255, 180, 86), // RibbonTabTracking2
            Color.FromArgb(255, 255, 189), // RibbonTabHighlight1
            Color.FromArgb(249, 237, 198), // RibbonTabHighlight2
            Color.FromArgb(218, 185, 127), // RibbonTabHighlight3
            Color.FromArgb(254, 209, 94), // RibbonTabHighlight4
            Color.FromArgb(205, 209, 180), // RibbonTabHighlight5
            Color.FromArgb(175, 176, 179), // RibbonTabSeparatorColor
            Color.FromArgb(243, 243, 243), // RibbonGroupsArea1
            Color.FromArgb(192, 192, 192), // RibbonGroupsArea2
            Color.FromArgb(141, 141, 141), // RibbonGroupsArea3
            Color.FromArgb(135, 135, 135), // RibbonGroupsArea4
            Color.FromArgb(130, 130, 130), // RibbonGroupsArea5
            Color.FromArgb(209, 209, 209), // RibbonGroupBorder1
            Color.FromArgb(158, 158, 158), // RibbonGroupBorder2
            Color.FromArgb(223, 227, 239), // RibbonGroupTitle1
            Color.FromArgb(195, 199, 209), // RibbonGroupTitle2
            Color.FromArgb(183, 183, 183), // RibbonGroupBorderContext1
            Color.FromArgb(131, 131, 131), // RibbonGroupBorderContext2
            Color.FromArgb(223, 227, 239), // RibbonGroupTitleContext1
            Color.FromArgb(195, 199, 209), // RibbonGroupTitleContext2
            Color.FromArgb(101, 104, 112), // RibbonGroupDialogDark
            Color.FromArgb(242, 242, 242), // RibbonGroupDialogLight
            Color.FromArgb(222, 226, 238), // RibbonGroupTitleTracking1
            Color.FromArgb(179, 185, 199), // RibbonGroupTitleTracking2
            Color.FromArgb(128, 128, 128), // RibbonMinimizeBarDark
            Color.FromArgb(220, 225, 235), // RibbonMinimizeBarLight
            Color.FromArgb(183, 183, 183), // RibbonGroupCollapsedBorder1
            Color.FromArgb(145, 145, 145), // RibbonGroupCollapsedBorder2
            Color.FromArgb(64, Color.White), // RibbonGroupCollapsedBorder3
            Color.FromArgb(225, 227, 227), // RibbonGroupCollapsedBorder4
            Color.FromArgb(242, 246, 246), // RibbonGroupCollapsedBack1
            Color.FromArgb(207, 212, 220), // RibbonGroupCollapsedBack2
            Color.FromArgb(196, 203, 214), // RibbonGroupCollapsedBack3
            Color.FromArgb(234, 235, 235), // RibbonGroupCollapsedBack4
            Color.FromArgb(188, 193, 213), // RibbonGroupCollapsedBorderT1
            Color.FromArgb(142, 178, 179), // RibbonGroupCollapsedBorderT2
            Color.FromArgb(192, Color.White), // RibbonGroupCollapsedBorderT3
            Color.White, // RibbonGroupCollapsedBorderT4
            Color.FromArgb(245, 248, 248), // RibbonGroupCollapsedBackT1
            Color.FromArgb(242, 244, 247), // RibbonGroupCollapsedBackT2
            Color.FromArgb(238, 241, 245), // RibbonGroupCollapsedBackT3
            Color.FromArgb(234, 235, 235), // RibbonGroupCollapsedBackT4
            Color.FromArgb(160, 160, 160), // RibbonGroupFrameBorder1
            Color.FromArgb(209, 209, 209), // RibbonGroupFrameBorder2
            Color.FromArgb(239, 242, 243), // RibbonGroupFrameInside1
            Color.FromArgb(226, 229, 234), // RibbonGroupFrameInside2
            Color.FromArgb(220, 224, 231), // RibbonGroupFrameInside3
            Color.FromArgb(232, 234, 238), // RibbonGroupFrameInside4
            Color.FromArgb(68, 68, 68), // RibbonGroupCollapsedText
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
            Color.FromArgb(217, 222, 230), // RibbonQATFullbar1
            Color.FromArgb(214, 219, 227), // RibbonQATFullbar2
            Color.FromArgb(194, 201, 212), // RibbonQATFullbar3
            Color.FromArgb(103, 103, 103), // RibbonQATButtonDark
            Color.FromArgb(225, 225, 225), // RibbonQATButtonLight
            Color.FromArgb(219, 218, 228), // RibbonQATOverflow1
            Color.FromArgb(55, 100, 160), // RibbonQATOverflow2
            Color.FromArgb(173, 177, 181), // RibbonGroupSeparatorDark
            Color.FromArgb(232, 235, 237), // RibbonGroupSeparatorLight
            Color.FromArgb(231, 234, 238), // ButtonClusterButtonBack1
            Color.FromArgb(241, 243, 243), // ButtonClusterButtonBack2
            Color.FromArgb(197, 198, 199), // ButtonClusterButtonBorder1
            Color.FromArgb(157, 158, 159), // ButtonClusterButtonBorder2
            Color.FromArgb(238, 238, 244), // NavigatorMiniBackColor
            Color.White, // GridListNormal1
            Color.FromArgb(212, 215, 219), // GridListNormal2
            Color.FromArgb(210, 213, 218), // GridListPressed1
            Color.FromArgb(252, 253, 253), // GridListPressed2
            Color.FromArgb(186, 189, 194), // GridListSelected
            Color.FromArgb(241, 243, 243), // GridSheetColNormal1
            Color.FromArgb(200, 201, 202), // GridSheetColNormal2
            Color.FromArgb(208, 208, 208), // GridSheetColPressed1
            Color.FromArgb(166, 166, 166), // GridSheetColPressed2
            Color.FromArgb(255, 204, 153), // GridSheetColSelected1
            Color.FromArgb(255, 155, 104), // GridSheetColSelected2
            Color.FromArgb(231, 231, 231), // GridSheetRowNormal
            Color.FromArgb(184, 191, 196), // GridSheetRowPressed
            Color.FromArgb(245, 199, 149), // GridSheetRowSelected
            Color.FromArgb(188, 195, 209), // GridDataCellBorder
            Color.FromArgb(194, 217, 240), // GridDataCellSelected
            Color.Black, // InputControlTextNormal
            Color.FromArgb(172, 168, 153), // InputControlTextDisabled
            Color.FromArgb(169, 177, 184), // InputControlBorderNormal
            Color.FromArgb(177, 187, 198), // InputControlBorderDisabled
            Color.FromArgb(255, 255, 255), // InputControlBackNormal
            SystemColors.Control, // InputControlBackDisabled
            Color.FromArgb(232, 234, 236), // InputControlBackInactive
            Color.FromArgb(124, 124, 124), // InputDropDownNormal1
            Color.FromArgb(255, 248, 203), // InputDropDownNormal2
            Color.FromArgb(172, 168, 153), // InputDropDownDisabled1
            Color.Transparent, // InputDropDownDisabled2
            Color.FromArgb(235, 235, 235), // ContextMenuHeading
            Color.FromArgb(68, 68, 68), // ContextMenuHeadingText
            Color.FromArgb(239, 239, 239), // ContextMenuImageColumn
            Color.FromArgb(51, 51, 51), // AppButtonBack1
            Color.FromArgb(50, 50, 50), // AppButtonBack2
            Color.FromArgb(169, 174, 180), // AppButtonBorder
            Color.FromArgb(207, 212, 217), // AppButtonOuter1
            Color.FromArgb(194, 200, 208), // AppButtonOuter2
            Color.FromArgb(217, 221, 226), // AppButtonOuter3
            Color.FromArgb(250, 250, 250), // AppButtonInner1
            Color.FromArgb(169, 174, 180), // AppButtonInner2
            Color.FromArgb(153, 153, 153), // AppButtonMenuDocs
            Color.FromArgb(68, 68, 68), // AppButtonMenuDocsText
            Color.FromArgb(168, 167, 191), // SeparatorHighInternalBorder1
            Color.FromArgb(119, 118, 151), // SeparatorHighInternalBorder2
            Color.FromArgb(169, 177, 184), // RibbonGalleryBorder
            Color.FromArgb(232, 234, 236), // RibbonGalleryBackNormal
            Color.FromArgb(240, 241, 242), // RibbonGalleryBackTracking
            Color.FromArgb(195, 200, 209), // RibbonGalleryBack1
            Color.FromArgb(217, 220, 224), // RibbonGalleryBack2
            GlobalStaticValues.EMPTY_COLOR, // RibbonTabTracking3
            GlobalStaticValues.EMPTY_COLOR, // RibbonTabTracking4
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupBorder3
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupBorder4
            GlobalStaticValues.EMPTY_COLOR, // RibbonGroupBorder5
            Color.FromArgb(119, 119, 119), // RibbonGroupTitleText
            GlobalStaticValues.EMPTY_COLOR, // RibbonDropArrowLight
            GlobalStaticValues.EMPTY_COLOR, // RibbonDropArrowDark
            Color.FromArgb(237, 242, 248), // HeaderDockInactiveBack1
            Color.FromArgb(207, 213, 220), // HeaderDockInactiveBack2
            Color.FromArgb(161, 169, 179), // ButtonNavigatorBorder
            Color.FromArgb(68, 68, 68), // ButtonNavigatorText
            Color.FromArgb(207, 213, 220), // ButtonNavigatorTrack1
            Color.FromArgb(232, 234, 238), // ButtonNavigatorTrack2
            Color.FromArgb(191, 196, 202), // ButtonNavigatorPressed1
            Color.FromArgb(225, 226, 230), // ButtonNavigatorPressed2
            Color.FromArgb(222, 227, 234), // ButtonNavigatorChecked1
            Color.FromArgb(206, 214, 221), // ButtonNavigatorChecked2
            Color.FromArgb(221, 221, 221) // ToolTipBottom
        ];

        #endregion

        #endregion

        #region Identity
        static PaletteOffice2010DarkGray()
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
        /// Initialize a new instance of the PaletteOffice2010DarkGray class.
        /// </summary>
        public PaletteOffice2010DarkGray()
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
                                                     _ => base.GetButtonSpecImage(style, state)
                                                 };
        #endregion

        #region Tab Row Background

        /// <inheritdoc />
        public override Color GetRibbonTabRowGradientColor1(PaletteState state) =>
            GlobalStaticValues.TAB_ROW_GRADIENT_FIRST_COLOR;

        /// <inheritdoc />
        public override Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) =>
            _tabRowBackgroundGradientRaftingDarkColor;

        /// <inheritdoc />
        public override Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) =>
            _tabRowBackgroundGradientRaftingLightColor;

        /// <inheritdoc />
        public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

        /// <inheritdoc />
        public override float GetRibbonTabRowGradientRaftingAngle(PaletteState state) => _gradientRafting;

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
