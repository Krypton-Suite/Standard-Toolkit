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
    /// <summary>
    /// Provides the Black color scheme variant of the Office 2007 palette.
    /// </summary>
    public class PaletteOffice2007Black : PaletteOffice2007Base
    {
        #region Static Fields

        #region Image Lists

        private static readonly ImageList _checkBoxList;
        private static readonly ImageList _galleryButtonList;

        #endregion

        #region Image Array

        private static readonly Image?[] _radioButtonArray;

        #endregion

        #region Images

        private static readonly Image? _blackDropDownButton = GenericImageResources.BlackDropDownButton;
        private static readonly Image _blackCloseNormal = Office2007ControlBoxResources.Office2007ControlBoxBlackCloseNormal;
        private static readonly Image _blackCloseActive = Office2007ControlBoxResources.Office2007ControlBoxBlackCloseActive;
        private static readonly Image _blackCloseDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlackCloseDisabled;
        private static readonly Image _blackClosePressed = Office2007ControlBoxResources.Office2007ControlBoxBlackClosePressed;
        private static readonly Image _blackMaximiseNormal = Office2007ControlBoxResources.Office2007ControlBoxBlackMaximiseNormal;
        private static readonly Image _blackMaximiseActive = Office2007ControlBoxResources.Office2007ControlBoxBlackMaximiseActive;
        private static readonly Image _blackMaximiseDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlackMaximiseDisabled;
        private static readonly Image _blackMaximisePressed = Office2007ControlBoxResources.Office2007ControlBoxBlackMaximisePressed;
        private static readonly Image _blackMinimiseNormal = Office2007ControlBoxResources.Office2007ControlBoxBlackMinimiseNormal;
        private static readonly Image _blackMinimiseActive = Office2007ControlBoxResources.Office2007ControlBoxBlackMinimiseActive;
        private static readonly Image _blackMinimiseDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlackMinimiseDisabled;
        private static readonly Image _blackMinimisePressed = Office2007ControlBoxResources.Office2007ControlBoxBlueMinimisePessed;
        private static readonly Image _blackRestoreNormal = Office2007ControlBoxResources.Office2007ControlBoxBlackRestoreNormal;
        private static readonly Image _blackRestoreActive = Office2007ControlBoxResources.Office2007ControlBoxBlackRestoreActive;
        private static readonly Image _blackRestoreDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlackRestoreDisabled;
        private static readonly Image _blackRestorePressed = Office2007ControlBoxResources.Office2007ControlBoxBlackRestorePressed;
        private static readonly Image _blackHelpNormal = Office2007ControlBoxResources.Office2007HelpIconNormal;
        private static readonly Image _blackHelpActive = Office2007ControlBoxResources.Office2007HelpIconHover;
        private static readonly Image _blackHelpDisabled = Office2007ControlBoxResources.Office2007HelpIconDisabled;
        private static readonly Image _blackHelpPressed = Office2007ControlBoxResources.Office2007HelpIconPressed;
        private static readonly Image _blackRibbonMinimize = GenericImageResources.BlackButtonCollapse;
        private static readonly Image _blackRibbonExpand = GenericImageResources.BlackButtonExpand;
        private static readonly Image? _contextMenuSubMenu = GenericImageResources.BlackContextMenuSub;

        #endregion

        #region Colour Arrays

        private static readonly Color[] _trackBarColors =
        [
            Color.FromArgb(170, 170, 170), // Tick marks
            Color.FromArgb(37, 37, 37), // Top track
            Color.FromArgb(174, 174, 174), // Bottom track
            Color.FromArgb(131, 132, 132), // Fill track
            GlobalStaticValues.EMPTY_COLOR, // Outside position
            Color.FromArgb(35, 35, 35) // Border (normal) position
        ];

        private static readonly Color[] _schemeOfficeColors =
        [
            Color.FromArgb(70, 70, 70),  // (76, 83, 92), // TextLabelControl
            Color.FromArgb(70, 70, 70), // TextButtonNormal
            Color.Black, // TextButtonChecked
            Color.FromArgb(137, 135, 133), // ButtonNormalBorder1
            Color.FromArgb(127, 125, 123), // ButtonNormalBorder2
            Color.FromArgb(203, 213, 223), // ButtonNormalBack1
            Color.FromArgb(255, 255, 255), // ButtonNormalBack2
            Color.FromArgb(187, 192, 198), // ButtonNormalDefaultBack1
            Color.FromArgb(224, 227, 231), // ButtonNormalDefaultBack2
            Color.FromArgb(204, 208, 214), // ButtonNormalNavigatorBack1
            Color.FromArgb(229, 232, 236), // ButtonNormalNavigatorBack2
            Color.FromArgb(83, 83, 83), // PanelClient
            Color.FromArgb(70, 70, 70), // PanelAlternative
            Color.FromArgb(30, 30, 30), // ControlBorder
            Color.FromArgb(167, 167, 167), // SeparatorHighBorder1
            Color.FromArgb(119, 119, 119), // SeparatorHighBorder2
            Color.FromArgb(240, 241, 242), // HeaderPrimaryBack1
            Color.FromArgb(189, 193, 200), // HeaderPrimaryBack2
            Color.FromArgb(221, 224, 227), // HeaderSecondaryBack1
            Color.FromArgb(221, 224, 227), // HeaderSecondaryBack2
            Color.Black, // HeaderText
            Color.White, // StatusStripText
            Color.FromArgb(155, 163, 167), // ButtonBorder
            Color.FromArgb(221, 224, 227), // SeparatorLight
            Color.FromArgb(145, 153, 164), // SeparatorDark
            Color.FromArgb(228, 228, 228), // GripLight
            Color.FromArgb(77, 77, 77), // GripDark
            Color.FromArgb(83, 83, 83), // ToolStripBack
            Color.FromArgb(75, 75, 75), // StatusStripLight
            Color.FromArgb(50, 50, 50), // StatusStripDark
            Color.FromArgb(239, 239, 239), // ImageMargin
            Color.FromArgb(75, 75, 75), // ToolStripBegin
            Color.FromArgb(50, 50, 50), // ToolStripMiddle
            Color.FromArgb(50, 50, 50), // ToolStripEnd
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
            Color.FromArgb(174, 209, 255), // FormHeaderShortActive
            Color.FromArgb(225, 225, 225), // FormHeaderShortInactive
            Color.FromArgb(255, 255, 255), // FormHeaderLongActive
            Color.FromArgb(225, 225, 225), // FormHeaderLongInactive
            Color.FromArgb(88, 95, 104), // FormButtonBorderTrack
            Color.FromArgb(91, 105, 123), // FormButtonBack1Track
            Color.FromArgb(173, 199, 214), // FormButtonBack2Track
            Color.FromArgb(18, 18, 18), // FormButtonBorderPressed
            Color.FromArgb(0, 0, 0), // FormButtonBack1Pressed
            Color.FromArgb(65, 83, 102), // FormButtonBack2Pressed
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
            Color.White, // RibbonTabTextNormal
            Color.Black, // RibbonTabTextChecked
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
            Color.FromArgb(215, 219, 224), // RibbonGroupsArea1
            Color.FromArgb(235, 235, 235), // RibbonGroupsArea2
            Color.FromArgb(180, 187, 197), // RibbonGroupsArea3
            Color.FromArgb(210, 210, 210), // RibbonGroupsArea4
            Color.FromArgb(190, 190, 190), // RibbonGroupsArea5
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
            Color.FromArgb(70, 70, 70), // RibbonGroupCollapsedText         
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
            Color.White, // GridListNormal1                                                    
            Color.FromArgb(212, 215, 219), // GridListNormal2                                                    
            Color.FromArgb(210, 213, 218), // GridListPressed1                                                    
            Color.FromArgb(252, 253, 253), // GridListPressed2                                                    
            Color.FromArgb(186, 189, 194), // GridListSelected                                                    
            Color.FromArgb(248, 248, 248), // GridSheetColNormal1                                                    
            Color.FromArgb(222, 222, 222), // GridSheetColNormal2                                                    
            Color.FromArgb(224, 224, 224), // GridSheetColPressed1                                                    
            Color.FromArgb(195, 195, 195), // GridSheetColPressed2                                                    
            Color.FromArgb(249, 217, 159), // GridSheetColSelected1
            Color.FromArgb(241, 193, 95), // GridSheetColSelected2
            Color.FromArgb(237, 237, 237), // GridSheetRowNormal                                                   
            Color.FromArgb(196, 196, 196), // GridSheetRowPressed
            Color.FromArgb(255, 213, 141), // GridSheetRowSelected
            Color.FromArgb(188, 195, 209), // GridDataCellBorder
            Color.FromArgb(194, 217, 240), // GridDataCellSelected
            Color.Black, // InputControlTextNormal
            Color.FromArgb(172, 168, 153), // InputControlTextDisabled
            Color.FromArgb(137, 137, 137), // InputControlBorderNormal
            Color.FromArgb(204, 204, 204), // InputControlBorderDisabled
            Color.White, // InputControlBackNormal
            SystemColors.Control, // InputControlBackDisabled
            Color.FromArgb(232, 232, 232), // InputControlBackInactive
            Color.FromArgb(124, 124, 124), // InputDropDownNormal1
            Color.FromArgb(255, 248, 203), // InputDropDownNormal2
            Color.FromArgb(172, 168, 153), // InputDropDownDisabled1
            Color.Transparent, // InputDropDownDisabled2
            Color.FromArgb(235, 235, 235), // ContextMenuHeading
            Color.FromArgb(76, 83, 92), // ContextMenuHeadingText
            Color.FromArgb(239, 239, 239), // ContextMenuImageColumn
            Color.FromArgb(109, 108, 108), // AppButtonBack1
            Color.FromArgb(104, 103, 103), // AppButtonBack2
            Color.FromArgb(67, 66, 65), // AppButtonBorder
            Color.FromArgb(78, 78, 79), // AppButtonOuter1
            Color.FromArgb(47, 47, 47), // AppButtonOuter2
            Color.FromArgb(64, 64, 64), // AppButtonOuter3
            Color.FromArgb(107, 108, 113), // AppButtonInner1
            Color.FromArgb(67, 66, 65), // AppButtonInner2
            Color.FromArgb(233, 234, 238), // AppButtonMenuDocs
            Color.FromArgb(70, 70, 70), // AppButtonMenuDocsText
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
            Color.FromArgb(225, 225, 225), // RibbonDropArrowLight
            Color.FromArgb(103, 103, 103) // RibbonDropArrowDark
        ];


        #endregion

        #endregion

        #region Identity
        static PaletteOffice2007Black()
        {
            _checkBoxList = new ImageList
            {
                ImageSize = new Size(13, 13),
                ColorDepth = ColorDepth.Depth24Bit
            };
            _checkBoxList.Images.AddStrip(CheckBoxStripResources.CheckBoxStrip2007Black);
            _galleryButtonList = new ImageList
            {
                ImageSize = new Size(13, 7),
                ColorDepth = ColorDepth.Depth24Bit,
                TransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR
            };
            _galleryButtonList.Images.AddStrip(GalleryImageResources.GallerySilverBlack);
            _radioButtonArray =
            [
                Office2007RadioButtonImageResources.RadioButton2007BlueD,
                Office2007RadioButtonImageResources.RadioButton2007BlackN,
                Office2007RadioButtonImageResources.RadioButton2007BlackT,
                Office2007RadioButtonImageResources.RadioButton2007BlackP,
                Office2007RadioButtonImageResources.RadioButton2007BlueDC,
                Office2007RadioButtonImageResources.RadioButton2007BlackNC,
                Office2007RadioButtonImageResources.RadioButton2007BlackTC,
                Office2007RadioButtonImageResources.RadioButton2007BlackPC
            ];
        }

        /// <summary>
        /// Initialize a new instance of the PaletteOffice2007Black class.
        /// </summary>
        public PaletteOffice2007Black()
            : base("Office 2007 - Black",
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
        public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state) => style switch
        {
            PaletteBackStyle.ButtonForm => state switch
            {
                PaletteState.Tracking or PaletteState.CheckedTracking or PaletteState.Pressed or PaletteState.CheckedPressed => PaletteColorStyle.GlassBottom,
                _ => PaletteColorStyle.GlassNormalFull
            },
            PaletteBackStyle.HeaderForm => PaletteColorStyle.Rounding3,
            _ => base.GetBackColorStyle(style, state)
        };

        /// <summary>
        /// Gets the second back color.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
        {
            switch (style)
            {
                case PaletteBackStyle.TabDock:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _schemeOfficeColors[(int)SchemeOfficeColors.HeaderPrimaryBack1];
                    }
                    break;
            }

            return base.GetBackColor2(style, state);
        }
        #endregion

        #region Border
        /// <summary>
        /// Gets the first border color.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBorderColor1(PaletteBorderStyle style, PaletteState state)
        {
            switch (style)
            {
                case PaletteBorderStyle.TabDock:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _schemeOfficeColors[(int)SchemeOfficeColors.ControlBorder];
                    }
                    break;
            }

            return base.GetBorderColor1(style, state);
        }

        /// <summary>
        /// Gets the second border color.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
        {
            switch (style)
            {
                case PaletteBorderStyle.TabDock:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _schemeOfficeColors[(int)SchemeOfficeColors.ControlBorder];
                    }
                    break;
            }

            return base.GetBorderColor2(style, state);
        }
        #endregion

        #region Content
        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
        {
            if (style == PaletteContentStyle.ButtonForm)
            {
                switch (state)
                {
                    case PaletteState.FocusOverride:
                    case PaletteState.CheckedNormal:
                        return _schemeOfficeColors[(int)SchemeOfficeColors.TextButtonFormPressed];
                }
            }

            return base.GetContentShortTextColor1(style, state);
        }

        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
        {
            if (style == PaletteContentStyle.ButtonForm)
            {
                switch (state)
                {
                    case PaletteState.FocusOverride:
                    case PaletteState.CheckedNormal:
                        return _schemeOfficeColors[(int)SchemeOfficeColors.TextButtonFormPressed];
                }
            }

            return base.GetContentShortTextColor2(style, state);
        }

        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state)
        {
            if (style == PaletteContentStyle.ButtonForm)
            {
                switch (state)
                {
                    case PaletteState.FocusOverride:
                    case PaletteState.CheckedNormal:
                        return _schemeOfficeColors[(int)SchemeOfficeColors.TextButtonFormPressed];
                }
            }

            return base.GetContentLongTextColor1(style, state);
        }

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state)
        {
            if (style == PaletteContentStyle.ButtonForm)
            {
                switch (state)
                {
                    case PaletteState.FocusOverride:
                    case PaletteState.CheckedNormal:
                        return _schemeOfficeColors[(int)SchemeOfficeColors.TextButtonFormPressed];
                }
            }

            return base.GetContentLongTextColor2(style, state);
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
                                                         PaletteState.Disabled => _blackCloseDisabled,
                                                         PaletteState.Tracking => _blackCloseActive,
                                                         PaletteState.Pressed => _blackClosePressed,
                                                         _ => _blackCloseNormal
                                                     },
                                                     PaletteButtonSpecStyle.FormMin => state switch
                                                     {
                                                         PaletteState.Disabled => _blackMinimiseDisabled,
                                                         PaletteState.Tracking => _blackMinimiseActive,
                                                         PaletteState.Pressed => _blackMinimisePressed,
                                                         _ => _blackMinimiseNormal
                                                     },
                                                     PaletteButtonSpecStyle.FormMax => state switch
                                                     {
                                                         PaletteState.Disabled => _blackMaximiseDisabled,
                                                         PaletteState.Tracking => _blackMaximiseActive,
                                                         PaletteState.Pressed => _blackMaximisePressed,
                                                         _ => _blackMaximiseNormal
                                                     },
                                                     PaletteButtonSpecStyle.FormRestore => state switch
                                                     {
                                                         PaletteState.Disabled => _blackRestoreDisabled,
                                                         PaletteState.Tracking => _blackRestoreActive,
                                                         PaletteState.Pressed => _blackRestorePressed,
                                                         _ => _blackRestoreNormal
                                                     },
                                                     PaletteButtonSpecStyle.FormHelp => state switch
                                                     {
                                                         PaletteState.Disabled => _blackHelpDisabled,
                                                         PaletteState.Tracking => _blackHelpActive,
                                                         _ => _blackHelpNormal
                                                     },
                                                     PaletteButtonSpecStyle.RibbonMinimize => _blackRibbonMinimize,
                                                     PaletteButtonSpecStyle.RibbonExpand => _blackRibbonExpand,
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
                case PaletteRibbonBackStyle.RibbonTab:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.ContextTracking:
                            return PaletteRibbonColorStyle.RibbonTabGlowing;
                    }
                    break;
            }

            // Get style from the base class
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
}
