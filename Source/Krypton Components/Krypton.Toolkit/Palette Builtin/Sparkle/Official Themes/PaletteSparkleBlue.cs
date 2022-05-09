#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Toolkit
{
    #region Class: PaletteSparkleBlue
    /// <summary>
	/// Provides a fixed blue variantion on the sparkle appearance.
	/// </summary>
	public class PaletteSparkleBlue : PaletteSparkleBlueBase
    {
        #region Static Fields
        private static readonly ImageList _checkBoxList;
        private static readonly Image[] _radioButtonArray;

        private static readonly Color[] _appButtonNormal = new Color[] { Color.FromArgb(243, 245, 248),
                                                                         Color.FromArgb(214, 220, 231),
                                                                         Color.FromArgb(188, 198, 211),
                                                                         Color.FromArgb(254, 254, 255),
                                                                         Color.FromArgb(206, 213, 225) };

        private static readonly Color[] _appButtonTrack = new Color[] { Color.FromArgb(215, 239, 245),
                                                                        Color.FromArgb(146, 214, 238),
                                                                        Color.FromArgb(60, 155, 201),
                                                                        Color.FromArgb(93, 201, 248),
                                                                        Color.FromArgb(25, 168, 238) };

        private static readonly Color[] _appButtonPressed = new Color[] { Color.FromArgb(196, 227, 235),
                                                                          Color.FromArgb(149, 198, 228),
                                                                          Color.FromArgb(7, 97, 166),
                                                                          Color.FromArgb(57, 155, 242),
                                                                          Color.FromArgb(9, 136, 236) };

        private static readonly Color[] _ribbonGroupCollapsedBorderContextTracking = new Color[] { Color.FromArgb(128, 168, 184, 196),
                                                                                                   Color.FromArgb(168, 184, 196),
                                                                                                   Color.FromArgb(48, 255, 255, 255),
                                                                                                   Color.FromArgb(192, 207, 220) };

        private static readonly Color[] _sparkleColors = new Color[] { Color.FromArgb(99, 108, 135),        // 0 _colorDark99
                                                                       Color.FromArgb(86, 94, 118),         // 1 _colorDark86
                                                                       Color.FromArgb(72, 81, 102),         // 2 _colorDark72
                                                                       Color.FromArgb(45, 45, 45),          // 3 _colorDark45
                                                                       Color.FromArgb(27, 31, 38),          // 4 _colorDark27
                                                                       Color.FromArgb(20, 21, 23),          // 5 _colorDark20
                                                                       Color.FromArgb(19, 37, 61),          // 6 _buttonTrackBack1
                                                                       Color.FromArgb(60, 129, 206),        // 7 _buttonTrackBack2
                                                                       Color.FromArgb(13, 30, 52),          // 8 _buttonPressBack1
                                                                       Color.FromArgb(125, 205, 248),       // 9 _buttonPressBack2
                                                                       Color.FromArgb(28, 66, 160),         // 10 _buttonCheckBack1
                                                                       Color.FromArgb(87, 198, 239),        // 11 _buttonCheckBack2
                                                                       Color.FromArgb(14, 65, 204),         // 12 _buttonCheckTrackBack1
                                                                       Color.FromArgb(112, 212, 255),       // 13 _buttonCheckTrackBack2
                                                                       Color.FromArgb(27, 65, 160),         // 14 _buttonCheckPressBack1
                                                                       Color.FromArgb(51, 153, 255),        // 15 _colorBlue
                                                                       Color.FromArgb(29, 89, 131),         // 16 _menuItemHeading
                                                                       Color.FromArgb(164, 225, 236, 244),  // 17 _menuItemTrackBack1
                                                                       Color.FromArgb(164, 181, 215, 231),  // 18 _menuItemTrackBack2
                                                                       Color.FromArgb(164, 91, 187, 230),   // 19 _menuItemTrackBorder
                                                                       Color.FromArgb(220, 229, 244),       // 20 _menuItemCheckedBack
                                                                       Color.FromArgb(185, 191, 230),       // 21 _menuItemCheckedBorder
                                                                       Color.FromArgb(57, 66, 102),         // 22 _buttonBack2
                                                                       Color.FromArgb(57, 175, 250),        // 23 _buttonDefaultBack
                                                                       Color.FromArgb(177, 219, 242),       // 24 _gridHeaderTracking1
                                                                       Color.FromArgb(180, 218, 242),       // 25 _gridHeaderTracking2
                                                                       Color.FromArgb(145, 198, 228),       // 26 _gridHeaderPressed1
                                                                       Color.FromArgb(148, 197, 228),       // 27 _gridHeaderPressed2
                                                                       Color.FromArgb(190, 190, 190),       // 28 _gridCellBorder
                                                                       Color.FromArgb(79, 180, 239),        // 29 _tabCheckedNormal
                                                                       Color.FromArgb(48, 89, 146),         // 30 _ribbonFrameBorder1
                                                                       Color.FromArgb(85, 132, 196),        // 31 _ribbonFrameBorder1
                                                                       Color.FromArgb(209, 220, 235),       // 32 _ribbonFrameBack1
                                                                       Color.FromArgb(202, 211, 222),       // 33 _ribbonFrameBack2
                                                                       Color.FromArgb(176, 196, 222),       // 34 _ribbonFrameBack3
                                                                       Color.FromArgb(82, 120, 213),        // 35 _ribbonFrameBack3
                                                                       Color.FromArgb(72, 110, 213),        // 36 _contextCheckedTabFill
                                                                       Color.FromArgb(10, 20, 255),         // 37 _focusTabFill
                                                                     };

        private static readonly Color[] _ribbonColors = new Color[] { Color.FromArgb( 76,  83,  92),    // TextLabelControl
                                                                      Color.FromArgb( 70,  70,  70),    // TextButtonNormal
                                                                      Color.Black,                      // TextButtonChecked
                                                                      Color.FromArgb(137, 135, 133),    // ButtonNormalBorder1
                                                                      Color.FromArgb(127, 125, 123),    // ButtonNormalBorder2
                                                                      Color.FromArgb(203, 213, 223),    // ButtonNormalBack1
                                                                      Color.FromArgb(255, 255, 255),    // ButtonNormalBack2
                                                                      Color.FromArgb(187, 192, 198),    // ButtonNormalDefaultBack1
                                                                      Color.FromArgb(224, 227, 231),    // ButtonNormalDefaultBack2
                                                                      Color.FromArgb(204, 208, 214),    // ButtonNormalNavigatorBack1
                                                                      Color.FromArgb(229, 232, 236),    // ButtonNormalNavigatorBack2
                                                                      Color.FromArgb( 83,  83,  83),    // PanelClient
                                                                      Color.FromArgb( 70,  70,  70),    // PanelAlternative
                                                                      Color.FromArgb( 30,  30,  30),    // ControlBorder
                                                                      Color.FromArgb(167, 167, 167),    // SeparatorHighBorder1
                                                                      Color.FromArgb(119, 119, 119),    // SeparatorHighBorder2
                                                                      Color.FromArgb(240, 241, 242),    // HeaderPrimaryBack1
                                                                      Color.FromArgb(189, 193, 200),    // HeaderPrimaryBack2
                                                                      Color.FromArgb(221, 224, 227),    // HeaderSecondaryBack1
                                                                      Color.FromArgb(221, 224, 227),    // HeaderSecondaryBack2
                                                                      Color.Black,                      // HeaderText
                                                                      Color.White,                      // StatusStripText
                                                                      Color.FromArgb(155, 163, 167),    // ButtonBorder
                                                                      Color.FromArgb(200, 200, 200),    // SeparatorLight
                                                                      Color.FromArgb(86, 94, 118),      // SeparatorDark
                                                                      Color.FromArgb(190, 190, 190),    // GripLight
                                                                      Color.Black,                      // GripDark
                                                                      Color.FromArgb(99, 108, 135),     // ToolStripBack
                                                                      Color.FromArgb(99, 108, 135),     // StatusStripLight
                                                                      Color.Black,                      // StatusStripDark
                                                                      Color.FromArgb(240, 240, 240),    // ImageMargin
                                                                      Color.FromArgb(200, 200, 200),    // ToolStripBegin
                                                                      Color.FromArgb(99, 108, 135),     // ToolStripMiddle
                                                                      Color.FromArgb(72, 81, 102),      // ToolStripEnd
                                                                      Color.FromArgb(72, 81, 102),      // OverflowBegin
                                                                      Color.FromArgb(72, 81, 102),      // OverflowMiddle
                                                                      Color.FromArgb( 30,  30,  30),    // OverflowEnd
                                                                      Color.FromArgb( 30,  30,  30),    // ToolStripBorder
                                                                      Color.FromArgb( 47,  47,  47),    // FormBorderActive
                                                                      Color.FromArgb(146, 146, 146),    // FormBorderInactive
                                                                      Color.FromArgb( 77,  77,  77),    // FormBorderActiveLight
                                                                      Color.FromArgb(102, 102, 102),    // FormBorderActiveDark
                                                                      Color.FromArgb(153, 153, 153),    // FormBorderInactiveLight
                                                                      Color.FromArgb(171, 171, 171),    // FormBorderInactiveDark
                                                                      Color.FromArgb( 65,  65,  65),    // FormBorderHeaderActive
                                                                      Color.FromArgb(154, 154, 154),    // FormBorderHeaderInactive
                                                                      Color.FromArgb( 42,  43,  43),    // FormBorderHeaderActive1
                                                                      Color.FromArgb( 74,  74,  74),    // FormBorderHeaderActive2
                                                                      Color.FromArgb(146, 146, 146),    // FormBorderHeaderInctive1
                                                                      Color.FromArgb(158, 158, 158),    // FormBorderHeaderInctive2
                                                                      Color.FromArgb(174, 209, 255),    // FormHeaderShortActive
                                                                      Color.FromArgb(225, 225, 225),    // FormHeaderShortInactive
                                                                      Color.FromArgb(255, 255, 255),    // FormHeaderLongActive
                                                                      Color.FromArgb(225, 225, 225),    // FormHeaderLongInactive
                                                                      Color.FromArgb( 88,  95, 104),    // FormButtonBorderTrack
                                                                      Color.FromArgb( 91, 105, 123),    // FormButtonBack1Track
                                                                      Color.FromArgb(173, 199, 214),    // FormButtonBack2Track
                                                                      Color.FromArgb( 18,  18,  18),    // FormButtonBorderPressed
                                                                      Color.FromArgb(  0,   0,   0),    // FormButtonBack1Pressed
                                                                      Color.FromArgb( 65,  83, 102),    // FormButtonBack2Pressed
                                                                      Color.FromArgb( 70,  70,  70),    // TextButtonFormNormal
                                                                      Color.FromArgb(255, 255, 255),    // TextButtonFormTracking
                                                                      Color.FromArgb(255, 255, 255),    // TextButtonFormPressed
                                                                      Color.Blue,                       // LinkNotVisitedOverrideControl
                                                                      Color.Purple,                     // LinkVisitedOverrideControl
                                                                      Color.Red,                        // LinkPressedOverrideControl
                                                                      Color.FromArgb(180, 210, 255),    // LinkNotVisitedOverridePanel
                                                                      Color.Violet,                     // LinkVisitedOverridePanel
                                                                      Color.FromArgb(255,  90,  90),    // LinkPressedOverridePanel
                                                                      Color.White,                      // TextLabelPanel
                                                                      Color.White,                      // RibbonTabTextNormal
                                                                      Color.Black,                      // RibbonTabTextChecked
                                                                      Color.Black,                      // RibbonTabSelected1
                                                                      Color.Silver,                     // RibbonTabSelected2
                                                                      Color.FromArgb(177, 177, 188),    // RibbonTabSelected3
                                                                      Color.FromArgb(167, 167, 178),    // RibbonTabSelected4
                                                                      Color.FromArgb(137, 137, 148),    // RibbonTabSelected5
                                                                      Color.FromArgb(159, 156, 150),    // RibbonTabTracking1
                                                                      Color.FromArgb(200, 200, 255),    // RibbonTabTracking2
                                                                      Color.Black,                      // RibbonTabHighlight1
                                                                      Color.FromArgb(107, 145, 238),    // RibbonTabHighlight2
                                                                      Color.FromArgb( 97, 135, 228),    // RibbonTabHighlight3
                                                                      Color.FromArgb( 82, 120, 213),    // RibbonTabHighlight4
                                                                      Color.FromArgb(137, 137, 148),    // RibbonTabHighlight5
                                                                      Color.Black,                      // RibbonTabSeparatorColor
                                                                      Color.Black,                      // RibbonGroupsArea1
                                                                      Color.Black,                      // RibbonGroupsArea2
                                                                      Color.FromArgb( 96,  96, 110),    // RibbonGroupsArea3
                                                                      Color.FromArgb(140, 140, 150),    // RibbonGroupsArea4
                                                                      Color.FromArgb(140, 140, 150),    // RibbonGroupsArea5
                                                                      Color.Black,                      // RibbonGroupBorder1
                                                                      Color.Black,                      // RibbonGroupBorder2
                                                                      Color.DimGray,                    // RibbonGroupTitle1
                                                                      Color.Black,                      // RibbonGroupTitle2
                                                                      Color.Black,                      // RibbonGroupBorderContext1
                                                                      Color.Black,                      // RibbonGroupBorderContext2
                                                                      Color.DimGray,                    // RibbonGroupTitleContext1
                                                                      Color.Black,                      // RibbonGroupTitleContext2
                                                                      Color.Black,                      // RibbonGroupDialogDark
                                                                      Color.White,                      // RibbonGroupDialogLight
                                                                      Color.FromArgb(120, 120, 120),    // RibbonGroupTitleTracking1
                                                                      Color.FromArgb(65, 65, 65),       // RibbonGroupTitleTracking2
                                                                      Color.FromArgb(27, 31, 38),       // RibbonMinimizeBarDark
                                                                      Color.FromArgb(150, 150, 150),    // RibbonMinimizeBarLight
                                                                      Color.Black,                      // RibbonGroupCollapsedBorder1
                                                                      Color.Black,                      // RibbonGroupCollapsedBorder2
                                                                      Color.FromArgb( 75,  78,  85),    // RibbonGroupCollapsedBorder3
                                                                      Color.FromArgb(129, 133, 145),    // RibbonGroupCollapsedBorder4
                                                                      Color.FromArgb(167, 167, 168),    // RibbonGroupCollapsedBack1
                                                                      Color.FromArgb( 93,  93,  95),    // RibbonGroupCollapsedBack2
                                                                      Color.FromArgb( 20,  21,  23),    // RibbonGroupCollapsedBack3
                                                                      Color.FromArgb( 52,  60,  92),    // RibbonGroupCollapsedBack4
                                                                      Color.Black,                      // RibbonGroupCollapsedBorderT1
                                                                      Color.Black,                      // RibbonGroupCollapsedBorderT2
                                                                      Color.FromArgb( 48,  89, 146),    // RibbonGroupCollapsedBorderT3
                                                                      Color.FromArgb( 85, 132, 196),    // RibbonGroupCollapsedBorderT4
                                                                      Color.FromArgb(166, 173, 182),    // RibbonGroupCollapsedBackT1
                                                                      Color.FromArgb( 92, 105, 121),    // RibbonGroupCollapsedBackT2
                                                                      Color.FromArgb( 19,  37,  61),    // RibbonGroupCollapsedBackT3
                                                                      Color.FromArgb( 56, 119, 191),    // RibbonGroupCollapsedBackT4
                                                                      Color.Black,                      // RibbonGroupFrameBorder1
                                                                      Color.Gray,                       // RibbonGroupFrameBorder2
                                                                      Color.FromArgb(225, 225, 225),    // RibbonGroupFrameInside1
                                                                      Color.FromArgb(170, 170, 170),    // RibbonGroupFrameInside2
                                                                      Color.FromArgb(150, 150, 150),    // RibbonGroupFrameInside3
                                                                      Color.FromArgb(205, 205, 205),    // RibbonGroupFrameInside4
                                                                      Color.White,                      // RibbonGroupCollapsedText         
                                                                      Color.FromArgb(158, 163, 172),    // AlternatePressedBack1
                                                                      Color.FromArgb(212, 215, 216),    // AlternatePressedBack2
                                                                      Color.FromArgb(124, 125, 125),    // AlternatePressedBorder1
                                                                      Color.FromArgb(186, 186, 186),    // AlternatePressedBorder2
                                                                      Color.FromArgb( 43,  55,  67),    // FormButtonBack1Checked
                                                                      Color.FromArgb(106, 122, 140),    // FormButtonBack2Checked
                                                                      Color.FromArgb( 18,  18,  18),    // FormButtonBorderCheck
                                                                      Color.FromArgb( 33,  45,  57),    // FormButtonBack1CheckTrack
                                                                      Color.FromArgb(136, 152, 170),    // FormButtonBack2CheckTrack
                                                                      Color.FromArgb(20, 21, 23),       // RibbonQATMini1
                                                                      Color.FromArgb(150, 150, 150),    // RibbonQATMini2
                                                                      Color.FromArgb(45, 45, 45),       // RibbonQATMini3
                                                                      Color.FromArgb(14, Color.White),  // RibbonQATMini4
                                                                      Color.FromArgb(14, Color.White),  // RibbonQATMini5
                                                                      Color.Black,                      // RibbonQATMini1I
                                                                      Color.Black,                      // RibbonQATMini2I
                                                                      Color.Black,                      // RibbonQATMini3I
                                                                      Color.FromArgb(14, Color.White),  // RibbonQATMini4I
                                                                      Color.FromArgb(14, Color.White),  // RibbonQATMini5I
                                                                      Color.FromArgb(150, 150, 150),    // RibbonQATFullbar1                                                      
                                                                      Color.FromArgb(45, 45, 45),       // RibbonQATFullbar2                                                      
                                                                      Color.FromArgb(20, 21, 23),       // RibbonQATFullbar3                                                      
                                                                      Color.Black,                      // RibbonQATButtonDark                                                      
                                                                      Color.White,                      // RibbonQATButtonLight                                                      
                                                                      Color.FromArgb(240, 240, 240),    // RibbonQATOverflow1                                                      
                                                                      Color.Black,                      // RibbonQATOverflow2                                                      
                                                                      Color.Gray,                       // RibbonGroupSeparatorDark                                                      
                                                                      Color.Black,                      // RibbonGroupSeparatorLight                                                      
                                                                      Color.FromArgb(210, 217, 219),    // ButtonClusterButtonBack1                                                      
                                                                      Color.FromArgb(214, 222, 223),    // ButtonClusterButtonBack2                                                      
                                                                      Color.FromArgb(179, 188, 191),    // ButtonClusterButtonBorder1                                                      
                                                                      Color.FromArgb(145, 156, 159),    // ButtonClusterButtonBorder2                                                      
                                                                      Color.FromArgb(235, 235, 235),    // NavigatorMiniBackColor                                                    
                                                                      Color.White,                      // GridListNormal1                                                    
                                                                      Color.FromArgb(212, 215, 219),    // GridListNormal2                                                    
                                                                      Color.FromArgb(210, 213, 218),    // GridListPressed1                                                    
                                                                      Color.FromArgb(252, 253, 253),    // GridListPressed2                                                    
                                                                      Color.FromArgb(186, 189, 194),    // GridListSelected                                                    
                                                                      Color.FromArgb(248, 248, 248),    // GridSheetColNormal1                                                    
                                                                      Color.FromArgb(222, 222, 222),    // GridSheetColNormal2                                                    
                                                                      Color.FromArgb(224, 224, 224),    // GridSheetColPressed1                                                    
                                                                      Color.FromArgb(195, 195, 195),    // GridSheetColPressed2                                                    
                                                                      Color.FromArgb(249, 217, 159),    // GridSheetColSelected1
                                                                      Color.FromArgb(241, 193,  95),    // GridSheetColSelected2
                                                                      Color.FromArgb(237, 237, 237),    // GridSheetRowNormal                                                   
                                                                      Color.FromArgb(196, 196, 196),    // GridSheetRowPressed
                                                                      Color.FromArgb(255, 213, 141),    // GridSheetRowSelected
                                                                      Color.FromArgb(188, 195, 209),    // GridDataCellBorder
                                                                      Color.FromArgb(194, 217, 240),    // GridDataCellSelected
                                                                      Color.Black,                      // InputControlTextNormal
                                                                      Color.FromArgb(172, 168, 153),    // InputControlTextDisabled
                                                                      Color.FromArgb(137, 137, 137),    // InputControlBorderNormal
                                                                      Color.FromArgb(204, 204, 204),    // InputControlBorderDisabled
                                                                      Color.White,                      // InputControlBackNormal
                                                                      SystemColors.Control,             // InputControlBackDisabled
                                                                      Color.FromArgb(232, 232, 232),    // InputControlBackInactive
                                                                      Color.FromArgb(124, 124, 124),    // InputDropDownNormal1
                                                                      Color.FromArgb(255, 248, 203),    // InputDropDownNormal2
                                                                      Color.FromArgb(172, 168, 153),    // InputDropDownDisabled1
                                                                      Color.Transparent,                // InputDropDownDisabled2
                                                                      Color.FromArgb(235, 235, 235),    // ContextMenuHeading
                                                                      Color.FromArgb( 76,  83,  92),    // ContextMenuHeadingText
                                                                      Color.FromArgb(239, 239, 239),    // ContextMenuImageColumn
                                                                      Color.FromArgb(109, 108, 108),    // AppButtonBack1
                                                                      Color.FromArgb(104, 103, 103),    // AppButtonBack2
                                                                      Color.Black,                      // AppButtonBorder
                                                                      Color.FromArgb(20, 21, 23),       // AppButtonOuter1
                                                                      Color.Black,                      // AppButtonOuter2
                                                                      Color.FromArgb(20, 21, 23),       // AppButtonOuter3
                                                                      Color.FromArgb(20, 21, 23),       // AppButtonInner1
                                                                      Color.Black,                      // AppButtonInner2
                                                                      Color.FromArgb(237, 237, 242),    // AppButtonMenuDocs
                                                                      Color.Black,                      // AppButtonMenuDocsText
                                                                      Color.FromArgb(240, 241, 242),    // SeparatorHighInternalBorder1
                                                                      Color.FromArgb(195, 200, 206),    // SeparatorHighInternalBorder2
                                                                      Color.Black,                      // RibbonGalleryBorder
                                                                      Color.FromArgb(215, 215, 215),    // RibbonGalleryBackNormal
                                                                      Color.FromArgb(238, 238, 238),    // RibbonGalleryBackTracking
                                                                      Color.FromArgb(195, 200, 209),    // RibbonGalleryBack1
                                                                      Color.FromArgb(217, 220, 224),    // RibbonGalleryBack2
                                                                      Color.Empty,                      // RibbonTabTracking3
                                                                      Color.Empty,                      // RibbonTabTracking4
                                                                      Color.Empty,                      // RibbonGroupBorder3
                                                                      Color.Empty,                      // RibbonGroupBorder4
                                                                    };
        #endregion

        #region Identity
        static PaletteSparkleBlue()
        {
            _checkBoxList = new ImageList();
            _checkBoxList.ImageSize = new Size(13, 13);
            _checkBoxList.ColorDepth = ColorDepth.Depth24Bit;
            _checkBoxList.Images.AddStrip(CheckBoxStripResources.CheckBoxStripSparkle);
            _radioButtonArray = new Image[]{SparkleBlueRadioButtonResources.RadioButtonSparkleD,
                                            SparkleBlueRadioButtonResources.RadioButtonSparkleN,
                                            SparkleBlueRadioButtonResources.RadioButtonSparkleT,
                                            SparkleGeneralRadioButtonResources.RadioButtonSparkleP,
                                            SparkleBlueRadioButtonResources.RadioButtonSparkleDC,
                                            SparkleBlueRadioButtonResources.RadioButtonSparkleNC,
                                            SparkleBlueRadioButtonResources.RadioButtonSparkleTC,
                                            SparkleGeneralRadioButtonResources.RadioButtonSparklePC};
        }

        /// <summary>
        /// Initialize a new instance of the PaletteSparkleBlue class.
        /// </summary>
        public PaletteSparkleBlue()
            : base(_ribbonColors, _sparkleColors,
                   _appButtonNormal, _appButtonTrack, _appButtonPressed,
                   _ribbonGroupCollapsedBorderContextTracking,
                   _checkBoxList, _radioButtonArray)
        {
        }
        #endregion
    }
    #endregion

    #region Class: PaletteSparkleBlueBase
    /// <summary>
	/// Provides a fixed blue variantion on the sparkle appearance.
	/// </summary>
    public class PaletteSparkleBlueBase : PaletteBase
    {
        #region Static Fields
        private static readonly Padding _contentPaddingGrid = new Padding(2, 1, 2, 1);
        private static readonly Padding _contentPaddingHeader1 = new Padding(3, 2, 2, 2);
        private static readonly Padding _contentPaddingHeader2 = new Padding(3, 2, 2, 2);
        private static readonly Padding _contentPaddingHeader3 = new Padding(2, 1, 2, 1);
        private static readonly Padding _contentPaddingCalendar = new Padding(2);
        private static readonly Padding _contentPaddingHeaderForm = new Padding(5, -3, 3, -3);
        private static readonly Padding _contentPaddingLabel = new Padding(3, 1, 3, 1);
        private static readonly Padding _contentPaddingLabel2 = new Padding(8, 2, 8, 2);
        private static readonly Padding _contentPaddingButtonInputControl = new Padding(0);
        private static readonly Padding _contentPaddingButton12 = new Padding(1);
        private static readonly Padding _contentPaddingButton3 = new Padding(1, 0, 1, 0);
        private static readonly Padding _contentPaddingButton4 = new Padding(3, 2, 3, 1);
        private static readonly Padding _contentPaddingButton5 = new Padding(3, 3, 3, 1);
        private static readonly Padding _contentPaddingButton6 = new Padding(3);
        private static readonly Padding _contentPaddingButton7 = new Padding(1, 1, 0, 1);
        private static readonly Padding _contentPaddingButtonForm = new Padding(0);
        private static readonly Padding _contentPaddingButtonGallery = new Padding(3, 0, 3, 0);
        private static readonly Padding _contentPaddingButtonListItem = new Padding(0, -1, 0, -1);
        private static readonly Padding _contentPaddingToolTip = new Padding(2);
        private static readonly Padding _contentPaddingSuperTip = new Padding(4);
        private static readonly Padding _contentPaddingKeyTip = new Padding(0, -1, 0, -3);
        private static readonly Padding _contentPaddingContextMenuHeading = new Padding(8, 2, 8, 0);
        private static readonly Padding _contentPaddingContextMenuImage = new Padding(0);
        private static readonly Padding _contentPaddingContextMenuItemText = new Padding(9, 1, 7, 0);
        private static readonly Padding _contentPaddingContextMenuItemTextAlt = new Padding(7, 1, 6, 0);
        private static readonly Padding _contentPaddingContextMenuItemShortcutText = new Padding(3, 1, 4, 0);
        private static readonly Padding _metricPaddingMenuOuter = new Padding(1);
        private static readonly Padding _metricPaddingRibbon = new Padding(0, 1, 1, 1);
        private static readonly Padding _metricPaddingRibbonAppButton = new Padding(3, 0, 3, 0);
        private static readonly Padding _metricPaddingHeader = new Padding(0, 3, 1, 3);
        private static readonly Padding _metricPaddingHeaderForm = new Padding(0);
        private static readonly Padding _metricPaddingInputControl = new Padding(0, 1, 0, 1);
        private static readonly Padding _metricPaddingBarInside = new Padding(3);
        private static readonly Padding _metricPaddingBarTabs = new Padding(0);
        private static readonly Padding _metricPaddingBarOutside = new Padding(0, 0, 0, 3);
        private static readonly Padding _metricPaddingPageButtons = new Padding(1, 3, 1, 3);
        private static readonly Padding _metricPaddingContextMenuItemHighlight = new Padding(1, 0, 1, 0);

        private static readonly Image _disabledDropDown = DropDownArrowImageResources.DisabledDropDownButton2;
        private static readonly Image _disabledDropUp = DropDownArrowImageResources.DisabledDropUpButton;
        private static readonly Image _disabledGalleryDrop = DropDownArrowImageResources.DisabledGalleryDropButton;
        private static readonly Image _buttonSpecClose = WhiteImageResources.WhiteCloseButton;
        private static readonly Image _buttonSpecContext = WhiteImageResources.WhiteContextButton;
        private static readonly Image _buttonSpecNext = WhiteImageResources.WhiteNextButton;
        private static readonly Image _buttonSpecPrevious = WhiteImageResources.WhitePreviousButton;
        private static readonly Image _buttonSpecArrowLeft = WhiteImageResources.WhiteArrowLeftButton;
        private static readonly Image _buttonSpecArrowRight = WhiteImageResources.WhiteArrowRightButton;
        private static readonly Image _buttonSpecArrowUp = WhiteImageResources.WhiteArrowUpButton;
        private static readonly Image _buttonSpecArrowDown = WhiteImageResources.WhiteArrowDownButton;
        private static readonly Image _buttonSpecDropDown = WhiteImageResources.WhiteDropDownButton;
        private static readonly Image _buttonSpecPinVertical = WhiteImageResources.WhitePinVerticalButton;
        private static readonly Image _buttonSpecPinHorizontal = WhiteImageResources.WhitePinHorizontalButton;
        private static readonly Image _buttonSpecPendantClose = WhiteImageResources.WhitePendantClosenormal;
        private static readonly Image _buttonSpecPendantMin = WhiteImageResources.WhitePendantMinnormal;
        private static readonly Image _buttonSpecPendantRestore = WhiteImageResources.WhitePendantRestorenormal;
        private static readonly Image _buttonSpecWorkspaceMaximize = WhiteImageResources.WhiteMaximize;
        private static readonly Image _buttonSpecWorkspaceRestore = WhiteImageResources.WhiteRestore;
        private static readonly Image _buttonSpecRibbonMinimize = WhiteImageResources.WhitePendantRibbonMinimize;
        private static readonly Image _buttonSpecRibbonExpand = WhiteImageResources.WhitePendantRibbonExpand;
        private static readonly Image _sparkleDropDownOutlineButton = GenericSparkleImageResources.SparkleDropDownOutlineButton;
        private static readonly Image _sparkleDropDownButton = GenericSparkleImageResources.SparkleDropDownButton;
        private static readonly Image _sparkleDropUpButton = GenericSparkleImageResources.SparkleDropUpButton;
        private static readonly Image _sparkleGalleryDropButton = GenericSparkleImageResources.SparkleGalleryDropButton;
        private static readonly Image _sparkleCloseA = SparkleControlBoxResources.SparkleButtonCloseNormal;
        private static readonly Image _sparkleCloseI = SparkleControlBoxResources.SparkleButtonCloseDisabled;
        private static readonly Image _sparkleMaxA = SparkleControlBoxResources.SparkleButtonMaxNormal;
        private static readonly Image _sparkleMaxI = SparkleControlBoxResources.SparkleButtonMaxDisabled;
        private static readonly Image _sparkleMinA = SparkleControlBoxResources.SparkleButtonMinNormal;
        private static readonly Image _sparkleMinI = SparkleControlBoxResources.SparkleButtonMinDisabled;
        private static readonly Image _sparkleRestoreA = SparkleControlBoxResources.SparkleButtonRestoreNormal;
        private static readonly Image _sparkleRestoreI = SparkleControlBoxResources.SparkleButtonRestoreDisabled;
        private static readonly Image _sparkleHelpA = HelpIconResources.Office2010HelpIconNormal;
        private static readonly Image _sparkleHelpHover = HelpIconResources.Office2007HelpIconPressed;
        private static readonly Image _sparkleHelpI = HelpIconResources.Office365HelpIconDisabled;
        private static readonly Image _contextMenuChecked = GenericSparkleImageResources.SparkleGrayChecked;
        private static readonly Image _contextMenuIndeterminate = SparkleGeneralRadioButtonResources.RadioButtonSparkleGrayIndeterminate;
        private static readonly Image _contextMenuSubMenu = GenericImageResources.BlackContextMenuSub;
        private static readonly Image _treeExpandWhite = TreeItemImageResources.TreeExpandWhite;
        private static readonly Image _treeCollapseBlack = TreeItemImageResources.TreeCollapseBlack;

        private static readonly Color _disabledText = Color.FromArgb(160, 160, 160);
        private static readonly Color _disabledBack = Color.FromArgb(224, 224, 224);
        private static readonly Color _disabledBack2 = Color.FromArgb(240, 240, 240);
        private static readonly Color _disabledBorder = Color.FromArgb(212, 212, 212);
        private static readonly Color _disabledGlyphDark = Color.FromArgb(183, 183, 183);
        private static readonly Color _disabledGlyphLight = Color.FromArgb(237, 237, 237);
        private static readonly Color _contextGroupFrameTop = Color.FromArgb(200, 249, 249, 249);
        private static readonly Color _contextGroupFrameBottom = Color.FromArgb(249, 249, 249);
        private static readonly Color _ribbonFrameBack4 = Color.White;
        private static readonly Color _toolTipBack1 = Color.FromArgb(255, 255, 234);
        private static readonly Color _toolTipBack2 = Color.FromArgb(255, 255, 204);
        private static readonly Color _contextMenuInnerBack = Color.FromArgb(250, 250, 250);
        private static readonly Color _contextMenuOuterBack = Color.FromArgb(245, 245, 245);
        private static readonly Color _contextMenuBorder = Color.FromArgb(134, 134, 134);
        private static readonly Color _contextMenuHeadingBorder = Color.FromArgb(197, 197, 197);
        private static readonly Color _contextMenuImageBackChecked = Color.FromArgb(255, 227, 149);
        private static readonly Color _contextMenuImageBorderChecked = Color.FromArgb(242, 149, 54);
        private static readonly Color[] _ribbonGroupCollapsedBackContext = new Color[] { Color.FromArgb(48, 255, 255, 255), Color.FromArgb(235, 235, 235) };
        private static readonly Color[] _ribbonGroupCollapsedBackContextTracking = new Color[] { Color.FromArgb(48, 255, 255, 255), Color.FromArgb(235, 235, 235) };
        private static readonly Color[] _ribbonGroupCollapsedBorderContext = new Color[] { Color.FromArgb(128, 199, 199, 199), Color.FromArgb(199, 199, 199), Color.FromArgb(48, 255, 255, 255), Color.FromArgb(235, 235, 235) };
        private static readonly Color[] _trackBarColors = new Color[] { Color.FromArgb(180, 180, 180), Color.FromArgb(33, 37, 50), Color.FromArgb(126, 131, 142), Color.FromArgb(99, 99, 99), Color.FromArgb(32, Color.White), Color.FromArgb(35, 35, 35) };
        private static readonly Color _inputControlTextDisabled = Color.FromArgb(172, 168, 153);
        private static readonly Color _colorDark00 = Color.Black;
        private static readonly Color _colorWhite119 = Color.FromArgb(119, 119, 119);
        private static readonly Color _colorWhite128 = Color.FromArgb(128, 128, 128);
        private static readonly Color _colorWhite150 = Color.FromArgb(150, 150, 150);
        private static readonly Color _colorWhite167 = Color.FromArgb(167, 167, 167);
        private static readonly Color _colorWhite192 = Color.FromArgb(192, 192, 192);
        private static readonly Color _colorWhite215 = Color.FromArgb(215, 219, 225);
        private static readonly Color _colorWhite220 = Color.FromArgb(220, 220, 220);
        private static readonly Color _colorWhite224 = Color.FromArgb(224, 224, 224);
        private static readonly Color _colorWhite238 = Color.FromArgb(238, 243, 250);
        private static readonly Color _colorWhite240 = Color.FromArgb(240, 240, 240);
        private static readonly Color _colorWhite245 = Color.FromArgb(245, 245, 245);
        private static readonly Color _colorWhite255 = Color.White;
        private static readonly Color _gridHeaderNormal1 = Color.FromArgb(210, 210, 210);
        private static readonly Color _gridHeaderNormal2 = Color.FromArgb(235, 235, 235);
        private static readonly Color _gridHeaderBorder = Color.FromArgb(124, 124, 124);
        private static readonly Color _menuItemDisabledBack1 = Color.FromArgb(164, 220, 220, 220);
        private static readonly Color _menuItemDisabledBack2 = Color.FromArgb(164, 190, 190, 190);
        private static readonly Color _menuItemDisabledBorder = Color.FromArgb(164, 172, 172, 172);
        private static readonly Color _menuItemDisabledImageBorder = Color.FromArgb(200, 200, 200);
        #endregion

        #region Instance Fields
        private KryptonColorTableSparkleBlue _table;
        private Font _header1ShortFont;
        private Font _header2ShortFont;
        private Font _header1LongFont;
        private Font _header2LongFont;
        private Font _superToolFont;
        private Font _headerFormFont;
        private Font _buttonFont;
        private Font _buttonFontNavigatorStack;
        private Font _buttonFontNavigatorMini;
        private Font _tabFontNormal;
        private Font _tabFontSelected;
        private Font _ribbonTabFont;
        private Font _gridFont;
        private Font _calendarFont;
        private Font _calendarBoldFont;
        private Font _boldFont;
        private Font _italicFont;
        private Color[] _ribbonColors;
        private Color[] _sparkleColors;
        private Color[] _appButtonNormal;
        private Color[] _appButtonTrack;
        private Color[] _appButtonPressed;
        private Color[] _ribbonGroupCollapsedBorderContextTracking;
        private ImageList _checkBoxList;
        private Image[] _radioButtonArray;
        private string _baseFontName;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteSparkle class.
        /// </summary>
        /// <param name="ribbonColors">Colors used mainly for the ribbon.</param>
        /// <param name="sparkleColors">Colors used mainly for the sparkle settings.</param>
        /// <param name="appButtonNormal">Colors for app button in normal state.</param>
        /// <param name="appButtonTrack">Colors for app button in tracking state.</param>
        /// <param name="appButtonPressed">Colors for app button in pressed state.</param>
        /// <param name="ribbonGroupCollapsedBorderContextTracking">Colors for tracking a collapsed group border.</param>
        /// <param name="checkBoxList">Images for check box controls.</param>
        /// <param name="radioButtonArray">Images for radio button controls.</param>
        public PaletteSparkleBlueBase(Color[] ribbonColors,
                                  Color[] sparkleColors,
                                  Color[] appButtonNormal,
                                  Color[] appButtonTrack,
                                  Color[] appButtonPressed,
                                  Color[] ribbonGroupCollapsedBorderContextTracking,
                                  ImageList checkBoxList,
                                  Image[] radioButtonArray)
        {
            // Save colors for use in the color table
            _ribbonColors = ribbonColors;
            _sparkleColors = sparkleColors;
            _appButtonNormal = appButtonNormal;
            _appButtonTrack = appButtonTrack;
            _appButtonPressed = appButtonPressed;
            _ribbonGroupCollapsedBorderContextTracking = ribbonGroupCollapsedBorderContextTracking;
            _checkBoxList = checkBoxList;
            _radioButtonArray = radioButtonArray;

            // Get the font settings from the system
            DefineFonts();
        }
        #endregion

        #region AllowFormChrome
        /// <summary>
        /// Gets a value indicating if KryptonForm instances should show custom chrome.
        /// </summary>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetAllowFormChrome()
        {
            return InheritBool.True;
        }
        #endregion

        #region Renderer
        /// <summary>
        /// Gets the renderer to use for this palette.
        /// </summary>
        /// <returns>Renderer to use for drawing palette settings.</returns>
        public override IRenderer GetRenderer()
        {
            // We always want the professional renderer
            return KryptonManager.RenderSparkle;
        }
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
                return InheritBool.Inherit;

            switch (style)
            {
                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorCustom1:
                    return InheritBool.False;
                case PaletteBackStyle.ButtonLowProfile:
                case PaletteBackStyle.ButtonBreadCrumb:
                case PaletteBackStyle.ButtonListItem:
                case PaletteBackStyle.ButtonCommand:
                case PaletteBackStyle.ButtonButtonSpec:
                case PaletteBackStyle.ButtonCalendarDay:
                case PaletteBackStyle.ButtonNavigatorOverflow:
                case PaletteBackStyle.ButtonForm:
                case PaletteBackStyle.ButtonFormClose:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                            return InheritBool.False;
                        default:
                            return InheritBool.True;
                    }
                case PaletteBackStyle.ContextMenuItemImage:
                case PaletteBackStyle.ContextMenuItemHighlight:
                    switch (state)
                    {
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                            return InheritBool.False;
                        default:
                            return InheritBool.True;
                    }
                case PaletteBackStyle.ButtonInputControl:
                    if ((state == PaletteState.Disabled) ||
                        (state == PaletteState.Normal))
                        return InheritBool.False;
                    else
                        return InheritBool.True;
                case PaletteBackStyle.TabLowProfile:
                    switch (state)
                    {
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return InheritBool.True;
                        default:
                            return InheritBool.False;
                    }
                default:
                    // Default to drawing the background
                    return InheritBool.True;
            }
        }

        /// <summary>
        /// Gets the graphics drawing hint for the background.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteGraphicsHint value.</returns>
        public override PaletteGraphicsHint GetBackGraphicsHint(PaletteBackStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteGraphicsHint.Inherit;

            switch (style)
            {
                case PaletteBackStyle.FormMain:
                case PaletteBackStyle.FormCustom1:
                case PaletteBackStyle.HeaderForm:
                    return PaletteGraphicsHint.AntiAlias;
                case PaletteBackStyle.TabHighProfile:
                case PaletteBackStyle.TabStandardProfile:
                case PaletteBackStyle.TabLowProfile:
                case PaletteBackStyle.TabOneNote:
                case PaletteBackStyle.TabDock:
                case PaletteBackStyle.TabDockAutoHidden:
                case PaletteBackStyle.TabCustom1:
                case PaletteBackStyle.TabCustom2:
                case PaletteBackStyle.TabCustom3:
                case PaletteBackStyle.PanelClient:
                case PaletteBackStyle.PanelRibbonInactive:
                case PaletteBackStyle.PanelAlternate:
                case PaletteBackStyle.PanelCustom1:
                case PaletteBackStyle.SeparatorHighInternalProfile:
                case PaletteBackStyle.SeparatorHighProfile:
                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorCustom1:
                case PaletteBackStyle.ControlClient:
                case PaletteBackStyle.ControlAlternate:
                case PaletteBackStyle.ControlGroupBox:
                case PaletteBackStyle.ControlToolTip:
                case PaletteBackStyle.ControlRibbon:
                case PaletteBackStyle.ControlRibbonAppMenu:
                case PaletteBackStyle.ControlCustom1:
                case PaletteBackStyle.ContextMenuOuter:
                case PaletteBackStyle.ContextMenuInner:
                case PaletteBackStyle.ContextMenuHeading:
                case PaletteBackStyle.ContextMenuSeparator:
                case PaletteBackStyle.ContextMenuItemSplit:
                case PaletteBackStyle.ContextMenuItemImageColumn:
                case PaletteBackStyle.ContextMenuItemImage:
                case PaletteBackStyle.ContextMenuItemHighlight:
                case PaletteBackStyle.InputControlStandalone:
                case PaletteBackStyle.InputControlRibbon:
                case PaletteBackStyle.InputControlCustom1:
                case PaletteBackStyle.HeaderPrimary:
                case PaletteBackStyle.HeaderDockInactive:
                case PaletteBackStyle.HeaderDockActive:
                case PaletteBackStyle.HeaderCalendar:
                case PaletteBackStyle.HeaderSecondary:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                case PaletteBackStyle.ButtonStandalone:
                case PaletteBackStyle.ButtonGallery:
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
                case PaletteBackStyle.ButtonForm:
                case PaletteBackStyle.ButtonFormClose:
                case PaletteBackStyle.ButtonCustom1:
                case PaletteBackStyle.ButtonCustom2:
                case PaletteBackStyle.ButtonCustom3:
                case PaletteBackStyle.ButtonInputControl:
                case PaletteBackStyle.GridBackgroundList:
                case PaletteBackStyle.GridBackgroundSheet:
                case PaletteBackStyle.GridBackgroundCustom1:
                case PaletteBackStyle.GridHeaderColumnList:
                case PaletteBackStyle.GridHeaderColumnSheet:
                case PaletteBackStyle.GridHeaderColumnCustom1:
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowSheet:
                case PaletteBackStyle.GridHeaderRowCustom1:
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellSheet:
                case PaletteBackStyle.GridDataCellCustom1:
                    return PaletteGraphicsHint.None;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the first background color.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBackColor1(PaletteBackStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
                return Color.Empty;

            switch (style)
            {
                case PaletteBackStyle.FormMain:
                case PaletteBackStyle.FormCustom1:
                case PaletteBackStyle.PanelClient:
                case PaletteBackStyle.PanelRibbonInactive:
                case PaletteBackStyle.PanelCustom1:
                case PaletteBackStyle.ControlGroupBox:
                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorCustom1:
                case PaletteBackStyle.GridBackgroundList:
                case PaletteBackStyle.GridBackgroundSheet:
                case PaletteBackStyle.GridBackgroundCustom1:
                    return _sparkleColors[0];
                case PaletteBackStyle.PanelAlternate:
                    return _sparkleColors[1];
                case PaletteBackStyle.HeaderForm:
                    return _colorDark00;
                case PaletteBackStyle.HeaderPrimary:
                case PaletteBackStyle.HeaderDockInactive:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                    if (state == PaletteState.Disabled)
                        return _disabledBack;
                    else
                        return _sparkleColors[5];
                case PaletteBackStyle.HeaderDockActive:
                    if (state == PaletteState.Disabled)
                        return _disabledBack;
                    else
                        return _sparkleColors[10];
                case PaletteBackStyle.HeaderSecondary:
                case PaletteBackStyle.HeaderCalendar:
                    if (state == PaletteState.Disabled)
                        return _disabledBack;
                    else
                        return _sparkleColors[2];
                case PaletteBackStyle.ControlClient:
                case PaletteBackStyle.ControlAlternate:
                case PaletteBackStyle.ControlCustom1:
                    return _colorWhite238;
                case PaletteBackStyle.ButtonStandalone:
                case PaletteBackStyle.ButtonAlternate:
                case PaletteBackStyle.ButtonLowProfile:
                case PaletteBackStyle.ButtonBreadCrumb:
                case PaletteBackStyle.ButtonButtonSpec:
                case PaletteBackStyle.ButtonGallery:
                case PaletteBackStyle.ButtonCluster:
                case PaletteBackStyle.ButtonNavigatorOverflow:
                case PaletteBackStyle.ButtonNavigatorStack:
                case PaletteBackStyle.ButtonNavigatorMini:
                case PaletteBackStyle.ButtonInputControl:
                case PaletteBackStyle.ButtonCustom1:
                case PaletteBackStyle.ButtonCustom2:
                case PaletteBackStyle.ButtonCustom3:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        case PaletteState.Normal:
                            if (style == PaletteBackStyle.ButtonNavigatorStack)
                                return _sparkleColors[2];
                            else
                                return _sparkleColors[5];
                        case PaletteState.NormalDefaultOverride:
                            return Color.Empty;
                        case PaletteState.Tracking:
                            return _sparkleColors[6];
                        case PaletteState.Pressed:
                            return _sparkleColors[8];
                        case PaletteState.CheckedNormal:
                            if (style == PaletteBackStyle.ButtonInputControl)
                                return _sparkleColors[5];
                            else
                                return _sparkleColors[10];
                        case PaletteState.CheckedTracking:
                            if (style == PaletteBackStyle.ButtonInputControl)
                                return _sparkleColors[5];
                            else
                                return _sparkleColors[12];
                        case PaletteState.CheckedPressed:
                            return _sparkleColors[14];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.ButtonCalendarDay:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        case PaletteState.Normal:
                            return _sparkleColors[5];
                        case PaletteState.NormalDefaultOverride:
                            return Color.Empty;
                        case PaletteState.Tracking:
                            return _sparkleColors[27];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedNormal:
                            return _sparkleColors[15];
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _sparkleColors[12];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.ButtonForm:
                case PaletteBackStyle.ButtonFormClose:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                            return Color.Empty;
                        case PaletteState.CheckedNormal:
                            return _sparkleColors[10];
                        case PaletteState.Tracking:
                            return _sparkleColors[5];
                        case PaletteState.CheckedTracking:
                            return _sparkleColors[12];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _sparkleColors[14];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.ButtonListItem:
                case PaletteBackStyle.ButtonCommand:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                        case PaletteState.Tracking:
                            return _colorWhite215;
                        case PaletteState.Pressed:
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _sparkleColors[15];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.ContextMenuOuter:
                case PaletteBackStyle.ContextMenuInner:
                case PaletteBackStyle.ContextMenuItemImageColumn:
                    return _colorWhite240;
                case PaletteBackStyle.ContextMenuSeparator:
                    return _colorWhite255;
                case PaletteBackStyle.ContextMenuHeading:
                    return _sparkleColors[16];
                case PaletteBackStyle.ContextMenuItemHighlight:
                    if (state == PaletteState.Disabled)
                        return _menuItemDisabledBack1;
                    else
                        return _sparkleColors[17];
                case PaletteBackStyle.ContextMenuItemImage:
                    if (state == PaletteState.Disabled)
                        return _menuItemDisabledBack1;
                    else
                        return _sparkleColors[20];
                case PaletteBackStyle.InputControlStandalone:
                case PaletteBackStyle.InputControlRibbon:
                case PaletteBackStyle.InputControlCustom1:
                    if (state == PaletteState.Disabled)
                        return _disabledBack;
                    else
                    {
                        if ((state == PaletteState.Tracking) || (style == PaletteBackStyle.InputControlStandalone))
                            return _colorWhite238;
                        else
                            return _colorWhite192;
                    }
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellCustom1:
                    if (state == PaletteState.CheckedNormal)
                        return _sparkleColors[15];
                    else
                        return _colorWhite238;
                case PaletteBackStyle.GridDataCellSheet:
                    if (state == PaletteState.CheckedNormal)
                        return _sparkleColors[10];
                    else
                        return _colorWhite238;
                case PaletteBackStyle.GridHeaderColumnList:
                case PaletteBackStyle.GridHeaderColumnCustom1:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        default:
                        case PaletteState.Normal:
                            return _gridHeaderNormal1;
                        case PaletteState.Tracking:
                            return _sparkleColors[24];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedNormal:
                            return _sparkleColors[26];
                    }
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowCustom1:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        default:
                        case PaletteState.Normal:
                            return _gridHeaderNormal2;
                        case PaletteState.Tracking:
                            return _sparkleColors[25];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedNormal:
                            return _sparkleColors[27];
                    }
                case PaletteBackStyle.GridHeaderRowSheet:
                case PaletteBackStyle.GridHeaderColumnSheet:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        default:
                        case PaletteState.Normal:
                            return _gridHeaderNormal1;
                        case PaletteState.Tracking:
                            return _sparkleColors[24];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedNormal:
                            return _sparkleColors[15];
                    }
                case PaletteBackStyle.SeparatorHighInternalProfile:
                    if (state == PaletteState.Disabled)
                        return _disabledBack;
                    else
                        return _colorWhite240;
                case PaletteBackStyle.SeparatorHighProfile:
                    if (state == PaletteState.Disabled)
                        return _disabledBack;
                    else
                        return _colorWhite167;
                case PaletteBackStyle.ControlToolTip:
                    return _toolTipBack1;
                case PaletteBackStyle.ContextMenuItemSplit:
                    if (state == PaletteState.Disabled)
                        return _colorWhite240;
                    else
                        return _colorWhite255;
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
                            if (style == PaletteBackStyle.TabLowProfile)
                                return Color.Empty;
                            else
                                return _disabledBack;
                        case PaletteState.Normal:
                            if (style == PaletteBackStyle.TabLowProfile)
                                return Color.Empty;
                            else
                                return _colorWhite220;
                        case PaletteState.Pressed:
                        case PaletteState.Tracking:
                            if (style == PaletteBackStyle.TabLowProfile)
                                return Color.Empty;
                            else
                                return _colorWhite220;
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            if (style == PaletteBackStyle.TabHighProfile)
                                return _sparkleColors[29];
                            else
                                return _colorWhite220;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.TabDock:
                case PaletteBackStyle.TabDockAutoHidden:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        case PaletteState.Normal:
                        case PaletteState.Pressed:
                        case PaletteState.Tracking:
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return _colorWhite220;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.ControlRibbon:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonTabSelected4];
                case PaletteBackStyle.ControlRibbonAppMenu:
                    return _ribbonColors[(int)SchemeOfficeColors.AppButtonBack1];
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the second back color.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
                return Color.Empty;

            switch (style)
            {
                case PaletteBackStyle.FormMain:
                case PaletteBackStyle.FormCustom1:
                case PaletteBackStyle.HeaderForm:
                case PaletteBackStyle.PanelClient:
                case PaletteBackStyle.PanelRibbonInactive:
                case PaletteBackStyle.PanelCustom1:
                case PaletteBackStyle.ControlGroupBox:
                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorCustom1:
                case PaletteBackStyle.GridBackgroundList:
                case PaletteBackStyle.GridBackgroundSheet:
                case PaletteBackStyle.GridBackgroundCustom1:
                    return _sparkleColors[0];
                case PaletteBackStyle.PanelAlternate:
                    return _sparkleColors[1];
                case PaletteBackStyle.HeaderCalendar:
                    if (state == PaletteState.Disabled)
                        return _disabledBack2;
                    else
                        return _sparkleColors[2];
                case PaletteBackStyle.HeaderPrimary:
                case PaletteBackStyle.HeaderDockInactive:
                case PaletteBackStyle.HeaderSecondary:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                    if (state == PaletteState.Disabled)
                        return _disabledBack2;
                    else
                        return _sparkleColors[0];
                case PaletteBackStyle.HeaderDockActive:
                    if (state == PaletteState.Disabled)
                        return _disabledBack2;
                    else
                        return _sparkleColors[11];
                case PaletteBackStyle.ControlClient:
                case PaletteBackStyle.ControlAlternate:
                case PaletteBackStyle.ControlCustom1:
                    return _colorWhite238;
                case PaletteBackStyle.ButtonStandalone:
                case PaletteBackStyle.ButtonAlternate:
                case PaletteBackStyle.ButtonLowProfile:
                case PaletteBackStyle.ButtonBreadCrumb:
                case PaletteBackStyle.ButtonCluster:
                case PaletteBackStyle.ButtonGallery:
                case PaletteBackStyle.ButtonNavigatorStack:
                case PaletteBackStyle.ButtonNavigatorOverflow:
                case PaletteBackStyle.ButtonNavigatorMini:
                case PaletteBackStyle.ButtonInputControl:
                case PaletteBackStyle.ButtonButtonSpec:
                case PaletteBackStyle.ButtonCustom1:
                case PaletteBackStyle.ButtonCustom2:
                case PaletteBackStyle.ButtonCustom3:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack2;
                        case PaletteState.Normal:
                            return _sparkleColors[22];
                        case PaletteState.NormalDefaultOverride:
                            return _sparkleColors[23];
                        case PaletteState.Tracking:
                            return _sparkleColors[7];
                        case PaletteState.Pressed:
                            return _sparkleColors[9];
                        case PaletteState.CheckedNormal:
                            if (style == PaletteBackStyle.ButtonInputControl)
                                return _sparkleColors[22];
                            else
                                return _sparkleColors[11];
                        case PaletteState.CheckedTracking:
                            if (style == PaletteBackStyle.ButtonInputControl)
                                return _sparkleColors[22];
                            else
                                return _sparkleColors[13];
                        case PaletteState.CheckedPressed:
                            return _sparkleColors[11];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.ButtonCalendarDay:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        case PaletteState.Normal:
                            return _sparkleColors[5];
                        case PaletteState.NormalDefaultOverride:
                            return Color.Empty;
                        case PaletteState.Tracking:
                            return _sparkleColors[27];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedNormal:
                            return _sparkleColors[15];
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _sparkleColors[12];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.ButtonForm:
                case PaletteBackStyle.ButtonFormClose:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                            return Color.Empty;
                        case PaletteState.CheckedNormal:
                            return _sparkleColors[11];
                        case PaletteState.Tracking:
                            return _sparkleColors[22];
                        case PaletteState.CheckedTracking:
                            return _sparkleColors[13];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _sparkleColors[11];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.ButtonListItem:
                case PaletteBackStyle.ButtonCommand:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack2;
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                        case PaletteState.Tracking:
                            return _colorWhite215;
                        case PaletteState.Pressed:
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _sparkleColors[15];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.ContextMenuInner:
                    return _colorWhite240;
                case PaletteBackStyle.ContextMenuOuter:
                    return _colorWhite245;
                case PaletteBackStyle.ContextMenuSeparator:
                    return _colorWhite255;
                case PaletteBackStyle.ContextMenuItemImageColumn:
                    return _colorWhite224;
                case PaletteBackStyle.ContextMenuHeading:
                    return _sparkleColors[16];
                case PaletteBackStyle.ContextMenuItemHighlight:
                    if (state == PaletteState.Disabled)
                        return _menuItemDisabledBack2;
                    else
                        return _sparkleColors[18];
                case PaletteBackStyle.ContextMenuItemImage:
                    if (state == PaletteState.Disabled)
                        return _menuItemDisabledBack1;
                    else
                        return _sparkleColors[20];
                case PaletteBackStyle.InputControlStandalone:
                case PaletteBackStyle.InputControlRibbon:
                case PaletteBackStyle.InputControlCustom1:
                    if (state == PaletteState.Disabled)
                        return _disabledBack2;
                    else
                    {
                        if ((state == PaletteState.Tracking) || (style == PaletteBackStyle.InputControlStandalone))
                            return _colorWhite238;
                        else
                            return _colorWhite192;
                    }
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellCustom1:
                    if (state == PaletteState.CheckedNormal)
                        return _sparkleColors[15];
                    else
                        return _colorWhite238;
                case PaletteBackStyle.GridDataCellSheet:
                    if (state == PaletteState.CheckedNormal)
                        return _sparkleColors[11];
                    else
                        return _colorWhite238;
                case PaletteBackStyle.GridHeaderColumnList:
                case PaletteBackStyle.GridHeaderColumnCustom1:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        default:
                        case PaletteState.Normal:
                            return _gridHeaderNormal2;
                        case PaletteState.Tracking:
                            return _sparkleColors[25];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedNormal:
                            return _sparkleColors[27];
                    }
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowCustom1:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        default:
                        case PaletteState.Normal:
                            return _gridHeaderNormal1;
                        case PaletteState.Tracking:
                            return _sparkleColors[24];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedNormal:
                            return _sparkleColors[26];
                    }
                case PaletteBackStyle.GridHeaderRowSheet:
                case PaletteBackStyle.GridHeaderColumnSheet:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack2;
                        default:
                        case PaletteState.Normal:
                            return _gridHeaderNormal1;
                        case PaletteState.Tracking:
                            return _sparkleColors[24];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedNormal:
                            return _sparkleColors[15];
                    }
                case PaletteBackStyle.SeparatorHighInternalProfile:
                    if (state == PaletteState.Disabled)
                        return _disabledBack;
                    else
                        return _colorWhite192;
                case PaletteBackStyle.SeparatorHighProfile:
                    if (state == PaletteState.Disabled)
                        return _disabledBack2;
                    else
                        return _colorWhite119;
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
                            if (style == PaletteBackStyle.TabLowProfile)
                                return Color.Empty;
                            else
                                return _disabledBack;
                        case PaletteState.Normal:
                            if (style == PaletteBackStyle.TabLowProfile)
                                return Color.Empty;
                            else
                                return _colorWhite220;
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            if (style == PaletteBackStyle.TabLowProfile)
                                return Color.Empty;
                            else
                                return _colorWhite238;
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return _colorWhite238;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.TabDock:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        case PaletteState.Normal:
                            return _colorWhite220;
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return _ribbonColors[(int)SchemeOfficeColors.FormHeaderShortActive];
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return _colorWhite238;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.TabDockAutoHidden:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        case PaletteState.Normal:
                        case PaletteState.CheckedNormal:
                            return _colorWhite220;
                        case PaletteState.Tracking:
                        case PaletteState.CheckedTracking:
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _ribbonColors[(int)SchemeOfficeColors.FormHeaderShortActive];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.ContextMenuItemSplit:
                    if (state == PaletteState.Disabled)
                        return _colorWhite240;
                    else
                        return _colorWhite255;
                case PaletteBackStyle.ControlRibbon:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonTabSelected4];
                case PaletteBackStyle.ControlRibbonAppMenu:
                    return _ribbonColors[(int)SchemeOfficeColors.AppButtonBack2];
                case PaletteBackStyle.ControlToolTip:
                    return _toolTipBack2;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the color background drawing style.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteColorStyle.Inherit;

            switch (style)
            {
                case PaletteBackStyle.FormMain:
                case PaletteBackStyle.FormCustom1:
                case PaletteBackStyle.HeaderCalendar:
                case PaletteBackStyle.ButtonCalendarDay:
                    return PaletteColorStyle.Solid;
                case PaletteBackStyle.HeaderForm:
                    return PaletteColorStyle.Linear;
                case PaletteBackStyle.HeaderPrimary:
                case PaletteBackStyle.HeaderDockInactive:
                case PaletteBackStyle.HeaderSecondary:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                    if (state == PaletteState.Disabled)
                        return PaletteColorStyle.GlassBottom;
                    else
                        return PaletteColorStyle.GlassSimpleFull;
                case PaletteBackStyle.HeaderDockActive:
                    return PaletteColorStyle.GlassBottom;
                case PaletteBackStyle.PanelClient:
                case PaletteBackStyle.PanelRibbonInactive:
                case PaletteBackStyle.PanelAlternate:
                case PaletteBackStyle.PanelCustom1:
                case PaletteBackStyle.ControlClient:
                case PaletteBackStyle.ControlAlternate:
                case PaletteBackStyle.ControlGroupBox:
                case PaletteBackStyle.ControlRibbon:
                case PaletteBackStyle.ContextMenuInner:
                case PaletteBackStyle.ControlCustom1:
                    return PaletteColorStyle.Solid;
                case PaletteBackStyle.ContextMenuHeading:
                    return PaletteColorStyle.SolidBottomLine;
                case PaletteBackStyle.ContextMenuItemImageColumn:
                    return PaletteColorStyle.SolidRightLine;
                case PaletteBackStyle.ContextMenuOuter:
                    return PaletteColorStyle.SolidAllLine;
                case PaletteBackStyle.ButtonListItem:
                case PaletteBackStyle.ButtonCommand:
                case PaletteBackStyle.ContextMenuItemHighlight:
                    return PaletteColorStyle.Linear;
                case PaletteBackStyle.ButtonStandalone:
                case PaletteBackStyle.ButtonLowProfile:
                case PaletteBackStyle.ButtonBreadCrumb:
                case PaletteBackStyle.ButtonButtonSpec:
                case PaletteBackStyle.ButtonForm:
                case PaletteBackStyle.ButtonFormClose:
                case PaletteBackStyle.ButtonGallery:
                case PaletteBackStyle.ButtonCluster:
                case PaletteBackStyle.ButtonCustom1:
                case PaletteBackStyle.ButtonCustom2:
                case PaletteBackStyle.ButtonCustom3:
                case PaletteBackStyle.ButtonNavigatorStack:
                case PaletteBackStyle.ButtonNavigatorOverflow:
                case PaletteBackStyle.ButtonNavigatorMini:
                    return PaletteColorStyle.GlassBottom;
                case PaletteBackStyle.ButtonAlternate:
                    return PaletteColorStyle.Linear50;
                case PaletteBackStyle.GridBackgroundList:
                case PaletteBackStyle.GridBackgroundSheet:
                case PaletteBackStyle.GridBackgroundCustom1:
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellCustom1:
                    return PaletteColorStyle.Solid;
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowCustom1:
                    return PaletteColorStyle.Linear;
                case PaletteBackStyle.GridHeaderColumnList:
                case PaletteBackStyle.GridHeaderColumnCustom1:
                    return PaletteColorStyle.GlassBottom;
                case PaletteBackStyle.GridDataCellSheet:
                    if (state == PaletteState.CheckedNormal)
                        return PaletteColorStyle.GlassBottom;
                    else
                        return PaletteColorStyle.Solid;
                case PaletteBackStyle.GridHeaderColumnSheet:
                case PaletteBackStyle.GridHeaderRowSheet:
                    return PaletteColorStyle.Solid;
                case PaletteBackStyle.TabHighProfile:
                case PaletteBackStyle.TabCustom1:
                case PaletteBackStyle.TabCustom2:
                case PaletteBackStyle.TabCustom3:
                case PaletteBackStyle.TabStandardProfile:
                case PaletteBackStyle.TabLowProfile:
                    return PaletteColorStyle.GlassFade;
                case PaletteBackStyle.TabOneNote:
                case PaletteBackStyle.TabDock:
                case PaletteBackStyle.TabDockAutoHidden:
                    return PaletteColorStyle.OneNote;
                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorCustom1:
                case PaletteBackStyle.InputControlStandalone:
                case PaletteBackStyle.InputControlRibbon:
                case PaletteBackStyle.InputControlCustom1:
                    return PaletteColorStyle.Solid;
                case PaletteBackStyle.ControlRibbonAppMenu:
                    return PaletteColorStyle.Switch90;
                case PaletteBackStyle.ContextMenuSeparator:
                case PaletteBackStyle.ContextMenuItemSplit:
                    if (state == PaletteState.Tracking)
                        return PaletteColorStyle.GlassTrackingFull;
                    else
                        return PaletteColorStyle.Solid;
                case PaletteBackStyle.ControlToolTip:
                    return PaletteColorStyle.Linear;
                case PaletteBackStyle.SeparatorHighInternalProfile:
                case PaletteBackStyle.SeparatorHighProfile:
                    return PaletteColorStyle.RoundedTopLight;
                case PaletteBackStyle.ContextMenuItemImage:
                    return PaletteColorStyle.Solid;
                case PaletteBackStyle.ButtonInputControl:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                            return PaletteColorStyle.GlassNormalSimple;
                        case PaletteState.Tracking:
                            return PaletteColorStyle.GlassTrackingSimple;
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return PaletteColorStyle.GlassPressedSimple;
                        case PaletteState.CheckedNormal:
                            return PaletteColorStyle.GlassCheckedSimple;
                        case PaletteState.CheckedTracking:
                            return PaletteColorStyle.GlassCheckedTrackingSimple;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the color alignment.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetBackColorAlign(PaletteBackStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteRectangleAlign.Inherit;

            switch (style)
            {
                case PaletteBackStyle.ControlClient:
                case PaletteBackStyle.ControlAlternate:
                case PaletteBackStyle.ControlGroupBox:
                case PaletteBackStyle.ControlRibbon:
                case PaletteBackStyle.ControlRibbonAppMenu:
                case PaletteBackStyle.ControlCustom1:
                case PaletteBackStyle.InputControlStandalone:
                case PaletteBackStyle.InputControlRibbon:
                case PaletteBackStyle.InputControlCustom1:
                case PaletteBackStyle.FormMain:
                case PaletteBackStyle.FormCustom1:
                case PaletteBackStyle.PanelClient:
                case PaletteBackStyle.PanelRibbonInactive:
                case PaletteBackStyle.PanelAlternate:
                case PaletteBackStyle.PanelCustom1:
                case PaletteBackStyle.GridBackgroundList:
                case PaletteBackStyle.GridBackgroundSheet:
                case PaletteBackStyle.GridBackgroundCustom1:
                    return PaletteRectangleAlign.Control;
                case PaletteBackStyle.ControlToolTip:
                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorHighInternalProfile:
                case PaletteBackStyle.SeparatorHighProfile:
                case PaletteBackStyle.SeparatorCustom1:
                case PaletteBackStyle.HeaderPrimary:
                case PaletteBackStyle.HeaderDockInactive:
                case PaletteBackStyle.HeaderDockActive:
                case PaletteBackStyle.HeaderCalendar:
                case PaletteBackStyle.HeaderSecondary:
                case PaletteBackStyle.HeaderForm:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                case PaletteBackStyle.TabHighProfile:
                case PaletteBackStyle.TabStandardProfile:
                case PaletteBackStyle.TabLowProfile:
                case PaletteBackStyle.TabOneNote:
                case PaletteBackStyle.TabDock:
                case PaletteBackStyle.TabDockAutoHidden:
                case PaletteBackStyle.TabCustom1:
                case PaletteBackStyle.TabCustom2:
                case PaletteBackStyle.TabCustom3:
                case PaletteBackStyle.ButtonStandalone:
                case PaletteBackStyle.ButtonGallery:
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
                case PaletteBackStyle.ButtonForm:
                case PaletteBackStyle.ButtonFormClose:
                case PaletteBackStyle.ButtonCustom1:
                case PaletteBackStyle.ButtonCustom2:
                case PaletteBackStyle.ButtonCustom3:
                case PaletteBackStyle.ButtonInputControl:
                case PaletteBackStyle.GridHeaderColumnList:
                case PaletteBackStyle.GridHeaderColumnSheet:
                case PaletteBackStyle.GridHeaderColumnCustom1:
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowSheet:
                case PaletteBackStyle.GridHeaderRowCustom1:
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellSheet:
                case PaletteBackStyle.GridDataCellCustom1:
                case PaletteBackStyle.ContextMenuItemImage:
                case PaletteBackStyle.ContextMenuItemHighlight:
                case PaletteBackStyle.ContextMenuOuter:
                case PaletteBackStyle.ContextMenuInner:
                case PaletteBackStyle.ContextMenuHeading:
                case PaletteBackStyle.ContextMenuSeparator:
                case PaletteBackStyle.ContextMenuItemSplit:
                case PaletteBackStyle.ContextMenuItemImageColumn:
                    return PaletteRectangleAlign.Local;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the color background angle.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetBackColorAngle(PaletteBackStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return -1f;

            switch (style)
            {
                case PaletteBackStyle.PanelClient:
                case PaletteBackStyle.PanelRibbonInactive:
                case PaletteBackStyle.PanelAlternate:
                case PaletteBackStyle.PanelCustom1:
                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorHighInternalProfile:
                case PaletteBackStyle.SeparatorHighProfile:
                case PaletteBackStyle.SeparatorCustom1:
                case PaletteBackStyle.ControlClient:
                case PaletteBackStyle.ControlAlternate:
                case PaletteBackStyle.ControlGroupBox:
                case PaletteBackStyle.ControlToolTip:
                case PaletteBackStyle.ControlRibbon:
                case PaletteBackStyle.ControlRibbonAppMenu:
                case PaletteBackStyle.ControlCustom1:
                case PaletteBackStyle.ContextMenuOuter:
                case PaletteBackStyle.ContextMenuInner:
                case PaletteBackStyle.ContextMenuHeading:
                case PaletteBackStyle.ContextMenuSeparator:
                case PaletteBackStyle.ContextMenuItemSplit:
                case PaletteBackStyle.ContextMenuItemImageColumn:
                case PaletteBackStyle.InputControlStandalone:
                case PaletteBackStyle.InputControlRibbon:
                case PaletteBackStyle.InputControlCustom1:
                case PaletteBackStyle.FormMain:
                case PaletteBackStyle.FormCustom1:
                case PaletteBackStyle.HeaderPrimary:
                case PaletteBackStyle.HeaderDockInactive:
                case PaletteBackStyle.HeaderDockActive:
                case PaletteBackStyle.HeaderCalendar:
                case PaletteBackStyle.HeaderSecondary:
                case PaletteBackStyle.HeaderForm:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                case PaletteBackStyle.TabHighProfile:
                case PaletteBackStyle.TabStandardProfile:
                case PaletteBackStyle.TabLowProfile:
                case PaletteBackStyle.TabOneNote:
                case PaletteBackStyle.TabDock:
                case PaletteBackStyle.TabDockAutoHidden:
                case PaletteBackStyle.TabCustom1:
                case PaletteBackStyle.TabCustom2:
                case PaletteBackStyle.TabCustom3:
                case PaletteBackStyle.ButtonStandalone:
                case PaletteBackStyle.ButtonGallery:
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
                case PaletteBackStyle.ButtonForm:
                case PaletteBackStyle.ButtonFormClose:
                case PaletteBackStyle.ButtonCustom1:
                case PaletteBackStyle.ButtonCustom2:
                case PaletteBackStyle.ButtonCustom3:
                case PaletteBackStyle.ButtonInputControl:
                case PaletteBackStyle.ContextMenuItemImage:
                case PaletteBackStyle.ContextMenuItemHighlight:
                case PaletteBackStyle.GridBackgroundList:
                case PaletteBackStyle.GridBackgroundSheet:
                case PaletteBackStyle.GridBackgroundCustom1:
                case PaletteBackStyle.GridHeaderColumnList:
                case PaletteBackStyle.GridHeaderColumnSheet:
                case PaletteBackStyle.GridHeaderColumnCustom1:
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowSheet:
                case PaletteBackStyle.GridHeaderRowCustom1:
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellSheet:
                case PaletteBackStyle.GridDataCellCustom1:
                    return 90f;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets a background image.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetBackImage(PaletteBackStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return null;

            switch (style)
            {
                case PaletteBackStyle.PanelClient:
                case PaletteBackStyle.PanelRibbonInactive:
                case PaletteBackStyle.PanelAlternate:
                case PaletteBackStyle.PanelCustom1:
                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorHighInternalProfile:
                case PaletteBackStyle.SeparatorHighProfile:
                case PaletteBackStyle.SeparatorCustom1:
                case PaletteBackStyle.ControlClient:
                case PaletteBackStyle.ControlAlternate:
                case PaletteBackStyle.ControlGroupBox:
                case PaletteBackStyle.ControlToolTip:
                case PaletteBackStyle.ControlRibbon:
                case PaletteBackStyle.ControlRibbonAppMenu:
                case PaletteBackStyle.ControlCustom1:
                case PaletteBackStyle.ContextMenuOuter:
                case PaletteBackStyle.ContextMenuInner:
                case PaletteBackStyle.ContextMenuHeading:
                case PaletteBackStyle.ContextMenuSeparator:
                case PaletteBackStyle.ContextMenuItemSplit:
                case PaletteBackStyle.ContextMenuItemImageColumn:
                case PaletteBackStyle.InputControlStandalone:
                case PaletteBackStyle.InputControlRibbon:
                case PaletteBackStyle.InputControlCustom1:
                case PaletteBackStyle.FormMain:
                case PaletteBackStyle.FormCustom1:
                case PaletteBackStyle.HeaderPrimary:
                case PaletteBackStyle.HeaderDockInactive:
                case PaletteBackStyle.HeaderDockActive:
                case PaletteBackStyle.HeaderCalendar:
                case PaletteBackStyle.HeaderSecondary:
                case PaletteBackStyle.HeaderForm:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                case PaletteBackStyle.TabHighProfile:
                case PaletteBackStyle.TabStandardProfile:
                case PaletteBackStyle.TabLowProfile:
                case PaletteBackStyle.TabOneNote:
                case PaletteBackStyle.TabDock:
                case PaletteBackStyle.TabDockAutoHidden:
                case PaletteBackStyle.TabCustom1:
                case PaletteBackStyle.TabCustom2:
                case PaletteBackStyle.TabCustom3:
                case PaletteBackStyle.ButtonStandalone:
                case PaletteBackStyle.ButtonGallery:
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
                case PaletteBackStyle.ButtonForm:
                case PaletteBackStyle.ButtonFormClose:
                case PaletteBackStyle.ButtonCustom1:
                case PaletteBackStyle.ButtonCustom2:
                case PaletteBackStyle.ButtonCustom3:
                case PaletteBackStyle.ButtonInputControl:
                case PaletteBackStyle.ContextMenuItemImage:
                case PaletteBackStyle.ContextMenuItemHighlight:
                case PaletteBackStyle.GridBackgroundList:
                case PaletteBackStyle.GridBackgroundSheet:
                case PaletteBackStyle.GridBackgroundCustom1:
                case PaletteBackStyle.GridHeaderColumnList:
                case PaletteBackStyle.GridHeaderColumnSheet:
                case PaletteBackStyle.GridHeaderColumnCustom1:
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowSheet:
                case PaletteBackStyle.GridHeaderRowCustom1:
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellSheet:
                case PaletteBackStyle.GridDataCellCustom1:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetBackImageStyle(PaletteBackStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteImageStyle.Inherit;

            switch (style)
            {
                case PaletteBackStyle.PanelClient:
                case PaletteBackStyle.PanelRibbonInactive:
                case PaletteBackStyle.PanelAlternate:
                case PaletteBackStyle.PanelCustom1:
                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorHighInternalProfile:
                case PaletteBackStyle.SeparatorHighProfile:
                case PaletteBackStyle.SeparatorCustom1:
                case PaletteBackStyle.ControlClient:
                case PaletteBackStyle.ControlAlternate:
                case PaletteBackStyle.ControlGroupBox:
                case PaletteBackStyle.ControlToolTip:
                case PaletteBackStyle.ControlRibbon:
                case PaletteBackStyle.ControlRibbonAppMenu:
                case PaletteBackStyle.ControlCustom1:
                case PaletteBackStyle.ContextMenuOuter:
                case PaletteBackStyle.ContextMenuInner:
                case PaletteBackStyle.ContextMenuHeading:
                case PaletteBackStyle.ContextMenuSeparator:
                case PaletteBackStyle.ContextMenuItemSplit:
                case PaletteBackStyle.ContextMenuItemImageColumn:
                case PaletteBackStyle.InputControlStandalone:
                case PaletteBackStyle.InputControlRibbon:
                case PaletteBackStyle.InputControlCustom1:
                case PaletteBackStyle.FormMain:
                case PaletteBackStyle.FormCustom1:
                case PaletteBackStyle.HeaderPrimary:
                case PaletteBackStyle.HeaderDockInactive:
                case PaletteBackStyle.HeaderDockActive:
                case PaletteBackStyle.HeaderCalendar:
                case PaletteBackStyle.HeaderSecondary:
                case PaletteBackStyle.HeaderForm:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                case PaletteBackStyle.TabHighProfile:
                case PaletteBackStyle.TabStandardProfile:
                case PaletteBackStyle.TabLowProfile:
                case PaletteBackStyle.TabOneNote:
                case PaletteBackStyle.TabDock:
                case PaletteBackStyle.TabDockAutoHidden:
                case PaletteBackStyle.TabCustom1:
                case PaletteBackStyle.TabCustom2:
                case PaletteBackStyle.TabCustom3:
                case PaletteBackStyle.ButtonStandalone:
                case PaletteBackStyle.ButtonGallery:
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
                case PaletteBackStyle.ButtonForm:
                case PaletteBackStyle.ButtonFormClose:
                case PaletteBackStyle.ButtonCustom1:
                case PaletteBackStyle.ButtonCustom2:
                case PaletteBackStyle.ButtonCustom3:
                case PaletteBackStyle.ButtonInputControl:
                case PaletteBackStyle.ContextMenuItemImage:
                case PaletteBackStyle.ContextMenuItemHighlight:
                case PaletteBackStyle.GridBackgroundList:
                case PaletteBackStyle.GridBackgroundSheet:
                case PaletteBackStyle.GridBackgroundCustom1:
                case PaletteBackStyle.GridHeaderColumnList:
                case PaletteBackStyle.GridHeaderColumnSheet:
                case PaletteBackStyle.GridHeaderColumnCustom1:
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowSheet:
                case PaletteBackStyle.GridHeaderRowCustom1:
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellSheet:
                case PaletteBackStyle.GridDataCellCustom1:
                    return PaletteImageStyle.Tile;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the image alignment.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetBackImageAlign(PaletteBackStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteRectangleAlign.Inherit;

            switch (style)
            {
                case PaletteBackStyle.PanelClient:
                case PaletteBackStyle.PanelRibbonInactive:
                case PaletteBackStyle.PanelAlternate:
                case PaletteBackStyle.PanelCustom1:
                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorHighInternalProfile:
                case PaletteBackStyle.SeparatorHighProfile:
                case PaletteBackStyle.SeparatorCustom1:
                case PaletteBackStyle.ControlClient:
                case PaletteBackStyle.ControlAlternate:
                case PaletteBackStyle.ControlGroupBox:
                case PaletteBackStyle.ControlToolTip:
                case PaletteBackStyle.ControlRibbon:
                case PaletteBackStyle.ControlRibbonAppMenu:
                case PaletteBackStyle.ControlCustom1:
                case PaletteBackStyle.ContextMenuOuter:
                case PaletteBackStyle.ContextMenuInner:
                case PaletteBackStyle.ContextMenuHeading:
                case PaletteBackStyle.ContextMenuSeparator:
                case PaletteBackStyle.ContextMenuItemSplit:
                case PaletteBackStyle.ContextMenuItemImageColumn:
                case PaletteBackStyle.InputControlStandalone:
                case PaletteBackStyle.InputControlRibbon:
                case PaletteBackStyle.InputControlCustom1:
                case PaletteBackStyle.FormMain:
                case PaletteBackStyle.FormCustom1:
                case PaletteBackStyle.HeaderPrimary:
                case PaletteBackStyle.HeaderDockInactive:
                case PaletteBackStyle.HeaderDockActive:
                case PaletteBackStyle.HeaderCalendar:
                case PaletteBackStyle.HeaderSecondary:
                case PaletteBackStyle.HeaderForm:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                case PaletteBackStyle.TabHighProfile:
                case PaletteBackStyle.TabStandardProfile:
                case PaletteBackStyle.TabLowProfile:
                case PaletteBackStyle.TabOneNote:
                case PaletteBackStyle.TabDock:
                case PaletteBackStyle.TabDockAutoHidden:
                case PaletteBackStyle.TabCustom1:
                case PaletteBackStyle.TabCustom2:
                case PaletteBackStyle.TabCustom3:
                case PaletteBackStyle.ButtonStandalone:
                case PaletteBackStyle.ButtonGallery:
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
                case PaletteBackStyle.ButtonForm:
                case PaletteBackStyle.ButtonFormClose:
                case PaletteBackStyle.ButtonCustom1:
                case PaletteBackStyle.ButtonCustom2:
                case PaletteBackStyle.ButtonCustom3:
                case PaletteBackStyle.ButtonInputControl:
                case PaletteBackStyle.ContextMenuItemImage:
                case PaletteBackStyle.ContextMenuItemHighlight:
                case PaletteBackStyle.GridBackgroundList:
                case PaletteBackStyle.GridBackgroundSheet:
                case PaletteBackStyle.GridBackgroundCustom1:
                case PaletteBackStyle.GridHeaderColumnList:
                case PaletteBackStyle.GridHeaderColumnSheet:
                case PaletteBackStyle.GridHeaderColumnCustom1:
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowSheet:
                case PaletteBackStyle.GridHeaderRowCustom1:
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellSheet:
                case PaletteBackStyle.GridDataCellCustom1:
                    return PaletteRectangleAlign.Local;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }
        #endregion

        #region Border
        /// <summary>
        /// Gets a value indicating if border should be drawn.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetBorderDraw(PaletteBorderStyle style, PaletteState state)
        {
            // Check for the calendar day today override
            if (state == PaletteState.TodayOverride)
                if (style == PaletteBorderStyle.ButtonCalendarDay)
                    return InheritBool.True;

            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return InheritBool.Inherit;

            switch (style)
            {
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ContextMenuInner:
                    return InheritBool.False;
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderCalendar:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    return InheritBool.True;
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCalendarDay:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                            return InheritBool.False;
                        default:
                            return InheritBool.True;
                    }
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                    switch (state)
                    {
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                            return InheritBool.False;
                        default:
                            return InheritBool.True;
                    }
                case PaletteBorderStyle.TabLowProfile:
                    switch (state)
                    {
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return InheritBool.True;
                        default:
                            return InheritBool.False;
                    }
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets a value indicating which borders to draw.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteDrawBorders value.</returns>
        public override PaletteDrawBorders GetBorderDrawBorders(PaletteBorderStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteDrawBorders.Inherit;

            switch (style)
            {
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderCalendar:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCalendarDay:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    return PaletteDrawBorders.All;
                case PaletteBorderStyle.ContextMenuHeading:
                    return PaletteDrawBorders.Bottom;
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                    return PaletteDrawBorders.Top;
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                    return PaletteDrawBorders.Right;
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ContextMenuInner:
                    return PaletteDrawBorders.None;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the graphics drawing hint for the border.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteGraphicsHint value.</returns>
        public override PaletteGraphicsHint GetBorderGraphicsHint(PaletteBorderStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteGraphicsHint.Inherit;

            switch (style)
            {
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.ContextMenuInner:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderCalendar:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCalendarDay:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    return PaletteGraphicsHint.AntiAlias;
                case PaletteBorderStyle.ContextMenuOuter:
                    return PaletteGraphicsHint.None;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the first border color.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBorderColor1(PaletteBorderStyle style, PaletteState state)
        {
            if (CommonHelper.IsOverrideState(state))
            {
                // Check for the calendar day today override
                if (state == PaletteState.TodayOverride)
                    if (style == PaletteBorderStyle.ButtonCalendarDay)
                    {
                        if (state == PaletteState.Disabled)
                            return _disabledBorder;
                        else
                            return _sparkleColors[2];
                    }

                return Color.Empty;
            }

            switch (style)
            {
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                    return _sparkleColors[4];
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.ContextMenuOuter:
                    return _colorDark00;
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBorder;
                        case PaletteState.Normal:
                            return _colorDark00;
                        default:
                            return _colorDark00;
                    }
                case PaletteBorderStyle.ButtonCalendarDay:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        case PaletteState.Normal:
                            return _sparkleColors[5];
                        case PaletteState.NormalDefaultOverride:
                            return Color.Empty;
                        case PaletteState.Tracking:
                            return _sparkleColors[27];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedNormal:
                            return _sparkleColors[15];
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _sparkleColors[12];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.ButtonGallery:
                    return _colorDark00;
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                        case PaletteState.Tracking:
                            return _colorWhite215;
                        case PaletteState.Pressed:
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _sparkleColors[15];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.ContextMenuSeparator:
                    return _colorWhite224;
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                    return _colorWhite255;
                case PaletteBorderStyle.ContextMenuItemHighlight:
                    if (state == PaletteState.Disabled)
                        return _menuItemDisabledBorder;
                    else
                        return _sparkleColors[19];
                case PaletteBorderStyle.ContextMenuItemImage:
                    if (state == PaletteState.Disabled)
                        return _menuItemDisabledImageBorder;
                    else
                        return _sparkleColors[21];
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _colorDark00;
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _sparkleColors[28];
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _gridHeaderBorder;
                case PaletteBorderStyle.ControlToolTip:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _colorDark00;
                case PaletteBorderStyle.ContextMenuInner:
                    return _contextMenuInnerBack;
                case PaletteBorderStyle.HeaderCalendar:
                    if (state == PaletteState.Disabled)
                        return _disabledBack;
                    else
                        return _sparkleColors[2];
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlCustom1:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _colorDark00;
                case PaletteBorderStyle.ContextMenuItemSplit:
                    if (state == PaletteState.Disabled)
                        return _colorWhite220;
                    else
                        return _colorWhite167;
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            if (style == PaletteBorderStyle.TabLowProfile)
                                return Color.Empty;
                            else
                                return _disabledBorder;
                        case PaletteState.Normal:
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return _colorDark00;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.ControlRibbon:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupsArea1];
                case PaletteBorderStyle.ControlRibbonAppMenu:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.AppButtonBorder];
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the second border color.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
        {
            if (CommonHelper.IsOverrideState(state))
            {
                // Check for the calendar day today override
                if (state == PaletteState.TodayOverride)
                    if (style == PaletteBorderStyle.ButtonCalendarDay)
                    {
                        if (state == PaletteState.Disabled)
                            return _disabledBorder;
                        else
                            return _sparkleColors[2];
                    }

                return Color.Empty;
            }

            switch (style)
            {
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                    return _sparkleColors[4];
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.ContextMenuOuter:
                    return _colorDark00;
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBorder;
                        case PaletteState.Normal:
                            return _colorDark00;
                        default:
                            return _colorDark00;
                    }
                case PaletteBorderStyle.ButtonCalendarDay:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        case PaletteState.Normal:
                            return _sparkleColors[5];
                        case PaletteState.NormalDefaultOverride:
                            return Color.Empty;
                        case PaletteState.Tracking:
                            return _sparkleColors[27];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedNormal:
                            return _sparkleColors[15];
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _sparkleColors[12];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                        case PaletteState.Tracking:
                            return _colorWhite215;
                        case PaletteState.Pressed:
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _sparkleColors[15];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.ContextMenuSeparator:
                    return _colorWhite224;
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                    return _colorWhite255;
                case PaletteBorderStyle.ContextMenuItemHighlight:
                    if (state == PaletteState.Disabled)
                        return _menuItemDisabledBorder;
                    else
                        return _sparkleColors[19];
                case PaletteBorderStyle.ContextMenuItemImage:
                    if (state == PaletteState.Disabled)
                        return _menuItemDisabledImageBorder;
                    else
                        return _sparkleColors[21];
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _colorDark00;
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _sparkleColors[28];
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _gridHeaderBorder;
                case PaletteBorderStyle.ContextMenuInner:
                    return _contextMenuInnerBack;
                case PaletteBorderStyle.ControlToolTip:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _colorDark00;
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlCustom1:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _colorDark00;
                case PaletteBorderStyle.HeaderCalendar:
                    if (state == PaletteState.Disabled)
                        return _disabledBack;
                    else
                        return _sparkleColors[2];
                case PaletteBorderStyle.ContextMenuItemSplit:
                    if (state == PaletteState.Disabled)
                        return _colorWhite220;
                    else
                        return _colorWhite167;
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            if (style == PaletteBorderStyle.TabLowProfile)
                                return Color.Empty;
                            else
                                return _disabledBorder;
                        case PaletteState.Normal:
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return _colorDark00;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.ControlRibbon:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupsArea1];
                case PaletteBorderStyle.ControlRibbonAppMenu:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.AppButtonBorder];
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the color border drawing style.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetBorderColorStyle(PaletteBorderStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
                return PaletteColorStyle.Inherit;

            switch (style)
            {
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                    return PaletteColorStyle.Sigma;
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCalendarDay:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.ContextMenuInner:
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                case PaletteBorderStyle.HeaderCalendar:
                    return PaletteColorStyle.Solid;
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                    if (state == PaletteState.Tracking)
                        return PaletteColorStyle.Sigma;
                    else
                        return PaletteColorStyle.Solid;
                case PaletteBorderStyle.ContextMenuItemHighlight:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            if (style == PaletteBorderStyle.ButtonCluster)
                                return PaletteColorStyle.Sigma;
                            else
                                return PaletteColorStyle.Solid;
                        case PaletteState.Disabled:
                        case PaletteState.NormalDefaultOverride:
                            return PaletteColorStyle.Solid;
                        case PaletteState.Pressed:
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return PaletteColorStyle.Linear;
                        default:
                            return PaletteColorStyle.Sigma;
                    }
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the color border alignment.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetBorderColorAlign(PaletteBorderStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteRectangleAlign.Inherit;

            switch (style)
            {
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                    return PaletteRectangleAlign.Control;
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderCalendar:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCalendarDay:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.ContextMenuInner:
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                    return PaletteRectangleAlign.Local;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the color border angle.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetBorderColorAngle(PaletteBorderStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return -1f;

            switch (style)
            {
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.ContextMenuInner:
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderCalendar:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCalendarDay:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    return 90f;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the border width.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Integer width.</returns>
        public override int GetBorderWidth(PaletteBorderStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return -1;

            switch (style)
            {
                case PaletteBorderStyle.HeaderForm:
                    return 4;
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ContextMenuInner:
                    return 0;
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderCalendar:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCalendarDay:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    return 1;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the border corner rounding.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Integer rounding.</returns>
        public override float GetBorderRounding(PaletteBorderStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return -1;

            switch (style)
            {
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.ContextMenuInner:
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderCalendar:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                case PaletteBorderStyle.ButtonCalendarDay:
                    return 0;
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                case PaletteBorderStyle.ContextMenuItemImage:
                    return 2;
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlGroupBox:
                    return 3;
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                    return 5;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets a border image.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetBorderImage(PaletteBorderStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return null;

            switch (style)
            {
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.ContextMenuInner:
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderCalendar:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCalendarDay:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the border image style.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetBorderImageStyle(PaletteBorderStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteImageStyle.Inherit;

            switch (style)
            {
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.ContextMenuInner:
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderCalendar:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCalendarDay:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    return PaletteImageStyle.Tile;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the image border alignment.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetBorderImageAlign(PaletteBorderStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteRectangleAlign.Inherit;

            switch (style)
            {
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.ContextMenuInner:
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderCalendar:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCalendarDay:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    return PaletteRectangleAlign.Local;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
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
                return InheritBool.Inherit;

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
            // By default the focus override shows the focus!
            if (state == PaletteState.FocusOverride)
                return InheritBool.True;

            // We do not override the other override states
            if (CommonHelper.IsOverrideState(state))
                return InheritBool.Inherit;

            // By default, never show the focus indication, we let individual controls
            // override this functionality as required by the controls requirements
            return InheritBool.False;
        }

        /// <summary>
        /// Gets the horizontal relative alignment of the image.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentImageH(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteRelativeAlign.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteRelativeAlign.Near;
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                    return PaletteRelativeAlign.Center;
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                    return PaletteRelativeAlign.Center;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the vertical relative alignment of the image.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentImageV(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteRelativeAlign.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteRelativeAlign.Center;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the effect applied to drawing of the image.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteImageEffect value.</returns>
        public override PaletteImageEffect GetContentImageEffect(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteImageEffect.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderForm:
                    return PaletteImageEffect.Normal;
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    if (state == PaletteState.Disabled)
                        return PaletteImageEffect.Disabled;
                    else
                        return PaletteImageEffect.Normal;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the image color to remap into another color.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentImageColorMap(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return Color.Empty;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return Color.Empty;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the color to use in place of the image map color.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentImageColorTo(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return Color.Empty;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return Color.Empty;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the image color that should be transparent.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentImageColorTransparent(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return Color.Empty;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return Color.Empty;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the font for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetContentShortTextFont(PaletteContentStyle style, PaletteState state)
        {
            if (CommonHelper.IsOverrideState(state))
            {
                if ((state == PaletteState.BoldedOverride) && (style == PaletteContentStyle.ButtonCalendarDay))
                    return _calendarBoldFont;
                else
                    return null;
            }

            switch (style)
            {
                case PaletteContentStyle.HeaderForm:
                    return _headerFormFont;
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.ButtonCommand:
                    return _header1ShortFont;
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.ContextMenuHeading:
                    return _superToolFont;
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                    return _header2ShortFont;
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelBoldPanel:
                    return _boldFont;
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelItalicControl:
                    return _italicFont;
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                    return _superToolFont;
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                    return _tabFontNormal;
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                    switch (state)
                    {
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return _tabFontSelected;
                        default:
                            return _tabFontNormal;
                    }
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                    return _buttonFont;
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                    return _buttonFontNavigatorStack;
                case PaletteContentStyle.ButtonNavigatorMini:
                    return _buttonFontNavigatorMini;
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                case PaletteContentStyle.HeaderCalendar:
                    return _gridFont;
                case PaletteContentStyle.ButtonCalendarDay:
                    return _calendarFont;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the font for the short text by generating a new font instance.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetContentShortTextNewFont(PaletteContentStyle style, PaletteState state)
        {
            DefineFonts();
            return GetContentShortTextFont(style, state);
        }

        /// <summary>
        /// Gets the rendering hint for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextHint value.</returns>
        public override PaletteTextHint GetContentShortTextHint(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteTextHint.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteTextHint.ClearTypeGridFit;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the prefix drawing setting for short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public override PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteTextHotkeyPrefix.Inherit;

            switch (style)
            {
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.HeaderForm:
                    return PaletteTextHotkeyPrefix.Show;
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                    return PaletteTextHotkeyPrefix.None;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the flag indicating if multiline text is allowed for short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetContentShortTextMultiLine(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return InheritBool.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return InheritBool.True;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the text trimming to use for short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextTrim value.</returns>
        public override PaletteTextTrim GetContentShortTextTrim(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteTextTrim.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteTextTrim.EllipsisCharacter;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the horizontal relative alignment of the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentShortTextH(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteRelativeAlign.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteRelativeAlign.Near;
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.HeaderCalendar:
                    return PaletteRelativeAlign.Center;
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                    return PaletteRelativeAlign.Center;
                case PaletteContentStyle.ContextMenuItemShortcutText:
                    return PaletteRelativeAlign.Far;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the vertical relative alignment of the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentShortTextV(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteRelativeAlign.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteRelativeAlign.Center;
                case PaletteContentStyle.LabelSuperTip:
                    return PaletteRelativeAlign.Near;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the horizontal relative alignment of multiline short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteRelativeAlign.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteRelativeAlign.Near;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
        {
            // Always work out value for an override state
            if (CommonHelper.IsOverrideState(state))
            {
                switch (style)
                {
                    case PaletteContentStyle.LabelNormalControl:
                    case PaletteContentStyle.LabelBoldControl:
                    case PaletteContentStyle.LabelItalicControl:
                    case PaletteContentStyle.LabelTitleControl:
                        switch (state)
                        {
                            case PaletteState.LinkNotVisitedOverride:
                                return _ribbonColors[(int)SchemeOfficeColors.LinkNotVisitedOverrideControl];
                            case PaletteState.LinkVisitedOverride:
                                return _ribbonColors[(int)SchemeOfficeColors.LinkVisitedOverrideControl];
                            case PaletteState.LinkPressedOverride:
                                return _ribbonColors[(int)SchemeOfficeColors.LinkPressedOverrideControl];
                            default:
                                // All other override states do nothing
                                return Color.Empty;
                        }
                    case PaletteContentStyle.LabelNormalPanel:
                    case PaletteContentStyle.LabelBoldPanel:
                    case PaletteContentStyle.LabelItalicPanel:
                    case PaletteContentStyle.LabelTitlePanel:
                    case PaletteContentStyle.LabelGroupBoxCaption:
                        switch (state)
                        {
                            case PaletteState.LinkNotVisitedOverride:
                                return _ribbonColors[(int)SchemeOfficeColors.LinkNotVisitedOverridePanel];
                            case PaletteState.LinkVisitedOverride:
                                return _ribbonColors[(int)SchemeOfficeColors.LinkVisitedOverridePanel];
                            case PaletteState.LinkPressedOverride:
                                return _ribbonColors[(int)SchemeOfficeColors.LinkPressedOverridePanel];
                            default:
                                // All other override states do nothing
                                return Color.Empty;
                        }
                }

                return Color.Empty;
            }

            switch (style)
            {
                case PaletteContentStyle.HeaderForm:
                    if (state == PaletteState.Disabled)
                        return _disabledText;
                    else
                        return _colorWhite255;
            }

            if ((state == PaletteState.Disabled) &&
                (style != PaletteContentStyle.LabelToolTip) &&
                (style != PaletteContentStyle.LabelSuperTip) &&
                (style != PaletteContentStyle.LabelKeyTip) &&
                (style != PaletteContentStyle.InputControlStandalone) &&
                (style != PaletteContentStyle.InputControlRibbon) &&
                (style != PaletteContentStyle.InputControlCustom1) &&
                (style != PaletteContentStyle.ButtonCalendarDay))
                return _disabledText;

            switch (style)
            {
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                    return _colorDark00;
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                        case PaletteState.Tracking:
                            return _colorDark00;
                        default:
                            return _colorWhite255;
                    }
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.ContextMenuHeading:
                    return _colorWhite255;
                case PaletteContentStyle.ButtonCalendarDay:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _colorWhite128;
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.FocusOverride:
                            return _colorWhite255;
                        default:
                            return _colorDark00;
                    }
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonInputControl:
                    if ((state == PaletteState.Normal) ||
                        (state == PaletteState.NormalDefaultOverride))
                        return _colorDark00;
                    else
                        return _colorWhite255;
                case PaletteContentStyle.ButtonButtonSpec:
                    return _colorWhite255;
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    if (state == PaletteState.CheckedNormal)
                        return _colorWhite255;
                    else
                        return _colorDark00;
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderRowSheet:
                    if (state != PaletteState.Pressed)
                        return _colorDark00;
                    else
                        return _colorWhite255;
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowCustom1:
                    return _colorDark00;
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                    if (state == PaletteState.Disabled)
                        return _inputControlTextDisabled;
                    else
                        return _colorDark00;
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledText;
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                        case PaletteState.FocusOverride:
                            return _colorDark00;
                        default:
                            return _sparkleColors[4];
                    }
                case PaletteContentStyle.TabDockAutoHidden:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledText;
                        case PaletteState.FocusOverride:
                            return _colorDark00;
                        default:
                            return _sparkleColors[4];
                    }
                case PaletteContentStyle.TabLowProfile:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledText;
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                        case PaletteState.FocusOverride:
                            return _colorDark00;
                        default:
                            return _colorWhite255;
                    }
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return Color.Empty;

            switch (style)
            {
                case PaletteContentStyle.HeaderForm:
                    if (state == PaletteState.Disabled)
                        return FadedColor(_colorWhite255);
                    else
                        return _colorWhite255;
            }

            if ((state == PaletteState.Disabled) &&
                (style != PaletteContentStyle.LabelToolTip) &&
                (style != PaletteContentStyle.LabelSuperTip) &&
                (style != PaletteContentStyle.LabelKeyTip) &&
                (style != PaletteContentStyle.InputControlStandalone) &&
                (style != PaletteContentStyle.InputControlRibbon) &&
                (style != PaletteContentStyle.InputControlCustom1) &&
                (style != PaletteContentStyle.ButtonCalendarDay))
                return _disabledText;

            switch (style)
            {
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                    return _colorDark00;
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                        case PaletteState.Tracking:
                            return _colorDark00;
                        case PaletteState.Pressed:
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _colorWhite255;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.ContextMenuHeading:
                    return _colorWhite255;
                case PaletteContentStyle.ButtonCalendarDay:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _colorWhite128;
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.FocusOverride:
                            return _colorWhite255;
                        default:
                            return _colorDark00;
                    }
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonInputControl:
                    if ((state == PaletteState.Normal) ||
                        (state == PaletteState.NormalDefaultOverride))
                        return _colorDark00;
                    else
                        return _colorWhite255;
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    if (state == PaletteState.CheckedNormal)
                        return _colorWhite255;
                    else
                        return _colorDark00;
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderRowSheet:
                    if (state != PaletteState.Pressed)
                        return _colorDark00;
                    else
                        return _colorWhite255;
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowCustom1:
                    return _colorDark00;
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                    if (state == PaletteState.Disabled)
                        return _inputControlTextDisabled;
                    else
                        return _colorDark00;
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledText;
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _colorDark00;
                        default:
                            return _sparkleColors[4];
                    }
                case PaletteContentStyle.TabDockAutoHidden:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledText;
                        default:
                            return _sparkleColors[4];
                    }
                case PaletteContentStyle.TabLowProfile:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledText;
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _colorDark00;
                        default:
                            return _colorWhite255;
                    }
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the color drawing style for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetContentShortTextColorStyle(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteColorStyle.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteColorStyle.Solid;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the color alignment for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetContentShortTextColorAlign(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteRectangleAlign.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteRectangleAlign.Local;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the color background angle for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetContentShortTextColorAngle(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return -1f;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return 90f;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets a background image for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetContentShortTextImage(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return null;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetContentShortTextImageStyle(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteImageStyle.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteImageStyle.TileFlipXY;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the image alignment for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetContentShortTextImageAlign(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteRectangleAlign.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteRectangleAlign.Local;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the font for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetContentLongTextFont(PaletteContentStyle style, PaletteState state)
        {
            if (CommonHelper.IsOverrideState(state))
            {
                if ((state == PaletteState.BoldedOverride) && (style == PaletteContentStyle.ButtonCalendarDay))
                    return _calendarBoldFont;
                else
                    return null;
            }

            switch (style)
            {
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                case PaletteContentStyle.HeaderCalendar:
                    return _gridFont;
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                    return _header1LongFont;
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.HeaderSecondary:
                    return _header2LongFont;
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                    return _tabFontNormal;
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                    switch (state)
                    {
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return _tabFontSelected;
                        default:
                            return _tabFontNormal;
                    }
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                    return _buttonFont;
                case PaletteContentStyle.ButtonCalendarDay:
                    return _calendarFont;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the font for the long text by generating a new font instance.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetContentLongTextNewFont(PaletteContentStyle style, PaletteState state)
        {
            DefineFonts();
            return GetContentLongTextFont(style, state);
        }

        /// <summary>
        /// Gets the rendering hint for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextHint value.</returns>
        public override PaletteTextHint GetContentLongTextHint(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteTextHint.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteTextHint.ClearTypeGridFit;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the flag indicating if multiline text is allowed for long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetContentLongTextMultiLine(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return InheritBool.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return InheritBool.True;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the text trimming to use for long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextTrim value.</returns>
        public override PaletteTextTrim GetContentLongTextTrim(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteTextTrim.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteTextTrim.EllipsisCharacter;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the prefix drawing setting for long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public override PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteTextHotkeyPrefix.Inherit;

            switch (style)
            {
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                    return PaletteTextHotkeyPrefix.Show;
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteTextHotkeyPrefix.None;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the horizontal relative alignment of the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentLongTextH(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteRelativeAlign.Inherit;

            switch (style)
            {
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                    return PaletteRelativeAlign.Near;
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteRelativeAlign.Far;
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                    return PaletteRelativeAlign.Center;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the vertical relative alignment of the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentLongTextV(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteRelativeAlign.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteRelativeAlign.Center;
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                    return PaletteRelativeAlign.Far;
                case PaletteContentStyle.LabelSuperTip:
                    return PaletteRelativeAlign.Center;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
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
                return PaletteRelativeAlign.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteRelativeAlign.Center;
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ButtonCommand:
                    return PaletteRelativeAlign.Near;
                case PaletteContentStyle.ContextMenuItemShortcutText:
                    return PaletteRelativeAlign.Far;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return Color.Empty;

            switch (style)
            {
                case PaletteContentStyle.HeaderForm:
                    if (state == PaletteState.Disabled)
                        return FadedColor(_colorWhite255);
                    else
                        return _colorWhite255;
            }

            if ((state == PaletteState.Disabled) &&
                (style != PaletteContentStyle.LabelToolTip) &&
                (style != PaletteContentStyle.LabelSuperTip) &&
                (style != PaletteContentStyle.LabelKeyTip) &&
                (style != PaletteContentStyle.InputControlStandalone) &&
                (style != PaletteContentStyle.InputControlRibbon) &&
                (style != PaletteContentStyle.InputControlCustom1))
                return _disabledText;

            switch (style)
            {
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                    return _colorDark00;
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                        case PaletteState.Tracking:
                            return _colorDark00;
                        case PaletteState.Pressed:
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _colorWhite255;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteContentStyle.ButtonCalendarDay:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _colorWhite128;
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.FocusOverride:
                            return _colorWhite255;
                        default:
                            return _colorDark00;
                    }
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.ContextMenuHeading:
                    return _colorWhite255;
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonInputControl:
                    if ((state == PaletteState.Normal) ||
                        (state == PaletteState.NormalDefaultOverride))
                        return _colorDark00;
                    else
                        return _colorWhite255;
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    if (state == PaletteState.CheckedNormal)
                        return _colorWhite255;
                    else
                        return _colorDark00;
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderRowSheet:
                    if (state != PaletteState.Pressed)
                        return _colorDark00;
                    else
                        return _colorWhite255;
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowCustom1:
                    return _colorDark00;
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                    if (state == PaletteState.Disabled)
                        return _inputControlTextDisabled;
                    else
                        return _colorDark00;
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledText;
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _colorDark00;
                        default:
                            return _sparkleColors[4];
                    }
                case PaletteContentStyle.TabDockAutoHidden:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledText;
                        default:
                            return _sparkleColors[4];
                    }
                case PaletteContentStyle.TabLowProfile:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledText;
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _colorDark00;
                        default:
                            return _colorWhite255;
                    }
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return Color.Empty;

            switch (style)
            {
                case PaletteContentStyle.HeaderForm:
                    if (state == PaletteState.Disabled)
                        return FadedColor(_colorWhite255);
                    else
                        return _colorWhite255;
            }

            if ((state == PaletteState.Disabled) &&
                (style != PaletteContentStyle.LabelToolTip) &&
                (style != PaletteContentStyle.LabelSuperTip) &&
                (style != PaletteContentStyle.LabelKeyTip) &&
                (style != PaletteContentStyle.InputControlStandalone) &&
                (style != PaletteContentStyle.InputControlRibbon) &&
                (style != PaletteContentStyle.InputControlCustom1))
                return _disabledText;

            switch (style)
            {
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                    return _colorDark00;
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                        case PaletteState.Tracking:
                            return _colorDark00;
                        case PaletteState.Pressed:
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _colorWhite255;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteContentStyle.ButtonCalendarDay:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _colorWhite128;
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.FocusOverride:
                            return _colorWhite255;
                        default:
                            return _colorDark00;
                    }
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.ContextMenuHeading:
                    return _colorWhite255;
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonInputControl:
                    if ((state == PaletteState.Normal) ||
                        (state == PaletteState.NormalDefaultOverride))
                        return _colorDark00;
                    else
                        return _colorWhite255;
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    if (state == PaletteState.CheckedNormal)
                        return _colorWhite255;
                    else
                        return _colorDark00;
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderRowSheet:
                    if (state != PaletteState.Pressed)
                        return _colorDark00;
                    else
                        return _colorWhite255;
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowCustom1:
                    return _colorDark00;
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                    if (state == PaletteState.Disabled)
                        return _inputControlTextDisabled;
                    else
                        return _colorDark00;
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledText;
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _colorDark00;
                        default:
                            return _sparkleColors[4];
                    }
                case PaletteContentStyle.TabDockAutoHidden:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledText;
                        default:
                            return _sparkleColors[4];
                    }
                case PaletteContentStyle.TabLowProfile:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledText;
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _colorDark00;
                        default:
                            return _colorWhite255;
                    }
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the color drawing style for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetContentLongTextColorStyle(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteColorStyle.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteColorStyle.Solid;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the color alignment for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetContentLongTextColorAlign(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteRectangleAlign.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteRectangleAlign.Local;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the color background angle for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetContentLongTextColorAngle(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return -1f;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return 90f;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets a background image for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetContentLongTextImage(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return null;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the background image style for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetContentLongTextImageStyle(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteImageStyle.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteImageStyle.TileFlipXY;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the image alignment for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetContentLongTextImageAlign(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteRectangleAlign.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteRectangleAlign.Local;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the padding between the border and content drawing.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Padding value.</returns>
        public override Padding GetContentPadding(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return CommonHelper.InheritPadding;

            switch (style)
            {
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return _contentPaddingGrid;
                case PaletteContentStyle.HeaderForm:
                    return _contentPaddingHeaderForm;
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                    return _contentPaddingHeader1;
                case PaletteContentStyle.HeaderSecondary:
                    return _contentPaddingHeader2;
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                    return _contentPaddingHeader3;
                case PaletteContentStyle.HeaderCalendar:
                    return _contentPaddingCalendar;
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                    return _contentPaddingLabel;
                case PaletteContentStyle.LabelGroupBoxCaption:
                    return _contentPaddingLabel2;
                case PaletteContentStyle.ContextMenuItemTextStandard:
                    return _contentPaddingContextMenuItemText;
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                    return _contentPaddingContextMenuItemTextAlt;
                case PaletteContentStyle.ContextMenuItemShortcutText:
                    return _contentPaddingContextMenuItemShortcutText;
                case PaletteContentStyle.ContextMenuItemImage:
                    return _contentPaddingContextMenuImage;
                case PaletteContentStyle.LabelToolTip:
                    return _contentPaddingToolTip;
                case PaletteContentStyle.LabelSuperTip:
                    return _contentPaddingSuperTip;
                case PaletteContentStyle.LabelKeyTip:
                    return _contentPaddingKeyTip;
                case PaletteContentStyle.ContextMenuHeading:
                    return _contentPaddingContextMenuHeading;
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                    return InputControlPadding;
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                    return _contentPaddingButton12;
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.ButtonCalendarDay:
                    return _contentPaddingButtonInputControl;
                case PaletteContentStyle.ButtonButtonSpec:
                    return _contentPaddingButton3;
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                    return _contentPaddingButton4;
                case PaletteContentStyle.ButtonBreadCrumb:
                    return _contentPaddingButton6;
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                    return _contentPaddingButtonForm;
                case PaletteContentStyle.ButtonGallery:
                    return _contentPaddingButtonGallery;
                case PaletteContentStyle.ButtonListItem:
                    return _contentPaddingButtonListItem;
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                    return _contentPaddingButton5;
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                    return _contentPaddingButton7;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the padding between adjacent content items.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Integer value.</returns>
        public override int GetContentAdjacentGap(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return -1;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return 1;
                case PaletteContentStyle.LabelSuperTip:
                    return 5;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
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
                    return 4;
                case PaletteMetricInt.HeaderButtonEdgeInsetInputControl:
                    return 1;
                case PaletteMetricInt.HeaderButtonEdgeInsetPrimary:
                case PaletteMetricInt.HeaderButtonEdgeInsetSecondary:
                case PaletteMetricInt.HeaderButtonEdgeInsetDockInactive:
                case PaletteMetricInt.HeaderButtonEdgeInsetDockActive:
                case PaletteMetricInt.HeaderButtonEdgeInsetCustom1:
                case PaletteMetricInt.HeaderButtonEdgeInsetCustom2:
                case PaletteMetricInt.BarButtonEdgeOutside:
                case PaletteMetricInt.BarButtonEdgeInside:
                    return 3;
                case PaletteMetricInt.None:
                    return 0;
                default:
                    // Should never happen!
                    Debug.Assert(false);
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
                case PaletteMetricBool.RibbonTabsSpareCaption:
                case PaletteMetricBool.TreeViewLines:
                    return InheritBool.False;
                default:
                    // Should never happen!
                    Debug.Assert(false);
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
                case PaletteMetricPadding.HeaderButtonPaddingCalendar:
                case PaletteMetricPadding.BarButtonPadding:
                    return _metricPaddingHeader;
                case PaletteMetricPadding.ContextMenuItemOuter:
                    return _metricPaddingMenuOuter;
                case PaletteMetricPadding.HeaderGroupPaddingPrimary:
                case PaletteMetricPadding.HeaderGroupPaddingSecondary:
                case PaletteMetricPadding.HeaderGroupPaddingDockInactive:
                case PaletteMetricPadding.HeaderGroupPaddingDockActive:
                case PaletteMetricPadding.SeparatorPaddingLowProfile:
                case PaletteMetricPadding.SeparatorPaddingHighInternalProfile:
                case PaletteMetricPadding.SeparatorPaddingHighProfile:
                case PaletteMetricPadding.SeparatorPaddingCustom1:
                case PaletteMetricPadding.ContextMenuItemsCollection:
                    return Padding.Empty;
                case PaletteMetricPadding.ContextMenuItemHighlight:
                    return _metricPaddingContextMenuItemHighlight;
                default:
                    // Should never happen!
                    Debug.Assert(false);
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
        public override Image GetTreeViewImage(bool expanded)
        {
            if (expanded)
                return _treeCollapseBlack;
            else
                return _treeExpandWhite;
        }

        /// <summary>
        /// Gets a check box image appropriate for the provided state.
        /// </summary>
        /// <param name="enabled">Is the check box enabled.</param>
        /// <param name="checkState">Is the check box checked/unchecked/indeterminate.</param>
        /// <param name="tracking">Is the check box being hot tracked.</param>
        /// <param name="pressed">Is the check box being pressed.</param>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image GetCheckBoxImage(bool enabled, CheckState checkState, bool tracking, bool pressed)
        {
            switch (checkState)
            {
                default:
                case CheckState.Unchecked:
                    if (!enabled)
                        return _checkBoxList.Images[0];
                    else if (pressed)
                        return _checkBoxList.Images[3];
                    else if (tracking)
                        return _checkBoxList.Images[2];
                    else
                        return _checkBoxList.Images[1];
                case CheckState.Checked:
                    if (!enabled)
                        return _checkBoxList.Images[4];
                    else if (pressed)
                        return _checkBoxList.Images[7];
                    else if (tracking)
                        return _checkBoxList.Images[6];
                    else
                        return _checkBoxList.Images[5];
                case CheckState.Indeterminate:
                    if (!enabled)
                        return _checkBoxList.Images[8];
                    else if (pressed)
                        return _checkBoxList.Images[11];
                    else if (tracking)
                        return _checkBoxList.Images[10];
                    else
                        return _checkBoxList.Images[9];
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
        public override Image GetRadioButtonImage(bool enabled, bool checkState, bool tracking, bool pressed)
        {
            if (!checkState)
            {
                if (!enabled)
                    return _radioButtonArray[0];
                else if (pressed)
                    return _radioButtonArray[3];
                else if (tracking)
                    return _radioButtonArray[2];
                else
                    return _radioButtonArray[1];
            }
            else
            {
                if (!enabled)
                    return _radioButtonArray[4];
                else if (pressed)
                    return _radioButtonArray[7];
                else if (tracking)
                    return _radioButtonArray[6];
                else
                    return _radioButtonArray[5];
            }
        }

        /// <summary>
        /// Gets a drop down button image appropriate for the provided state.
        /// </summary>
        /// <param name="state">PaletteState for which image is required.</param>
        public override Image GetDropDownButtonImage(PaletteState state)
        {
            if (state == PaletteState.Disabled)
                return _disabledDropDown;
            else
                return _sparkleDropDownOutlineButton;
        }

        /// <summary>
        /// Gets a checked image appropriate for a context menu item.
        /// </summary>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image GetContextMenuCheckedImage()
        {
            return _contextMenuChecked;
        }

        /// <summary>
        /// Gets a indeterminate image appropriate for a context menu item.
        /// </summary>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image GetContextMenuIndeterminateImage()
        {
            return _contextMenuIndeterminate;
        }

        /// <summary>
        /// Gets an image indicating a sub-menu on a context menu item.
        /// </summary>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image GetContextMenuSubMenuImage()
        {
            return _contextMenuSubMenu;
        }

        /// <summary>
        /// Gets a check box image appropriate for the provided state.
        /// </summary>
        /// <param name="button">Enum of the button to fetch.</param>
        /// <param name="state">State of the button to fetch.</param>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image GetGalleryButtonImage(PaletteRibbonGalleryButton button, PaletteState state)
        {
            switch (button)
            {
                default:
                case PaletteRibbonGalleryButton.Down:
                    if (state == PaletteState.Disabled)
                        return _disabledDropDown;
                    else
                        return _sparkleDropDownButton;
                case PaletteRibbonGalleryButton.Up:
                    if (state == PaletteState.Disabled)
                        return _disabledDropUp;
                    else
                        return _sparkleDropUpButton;
                case PaletteRibbonGalleryButton.DropDown:
                    if (state == PaletteState.Disabled)
                        return _disabledGalleryDrop;
                    else
                        return _sparkleGalleryDropButton;
            }
        }
        #endregion

        #region ButtonSpec
        /// <summary>
        /// Gets the icon to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>Icon value.</returns>
        public override Icon GetButtonSpecIcon(PaletteButtonSpecStyle style)
        {
            switch (style)
            {
                case PaletteButtonSpecStyle.Generic:
                case PaletteButtonSpecStyle.Close:
                case PaletteButtonSpecStyle.Context:
                case PaletteButtonSpecStyle.Next:
                case PaletteButtonSpecStyle.Previous:
                case PaletteButtonSpecStyle.ArrowLeft:
                case PaletteButtonSpecStyle.ArrowRight:
                case PaletteButtonSpecStyle.ArrowUp:
                case PaletteButtonSpecStyle.ArrowDown:
                case PaletteButtonSpecStyle.DropDown:
                case PaletteButtonSpecStyle.PinVertical:
                case PaletteButtonSpecStyle.PinHorizontal:
                case PaletteButtonSpecStyle.FormClose:
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormMax:
                case PaletteButtonSpecStyle.FormRestore:
                case PaletteButtonSpecStyle.PendantClose:
                case PaletteButtonSpecStyle.PendantMin:
                case PaletteButtonSpecStyle.PendantRestore:
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                case PaletteButtonSpecStyle.WorkspaceRestore:
                case PaletteButtonSpecStyle.RibbonMinimize:
                case PaletteButtonSpecStyle.RibbonExpand:
                    return null;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        /// <summary>
        /// Gets the image to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <param name="state">State for which image is required.</param>
        /// <returns>Image value.</returns>
        public override Image GetButtonSpecImage(PaletteButtonSpecStyle style,
                                                 PaletteState state)
        {
            switch (style)
            {
                case PaletteButtonSpecStyle.FormClose:
                    if (state == PaletteState.Disabled)
                        return _sparkleCloseI;
                    else
                        return _sparkleCloseA;
                case PaletteButtonSpecStyle.FormMin:
                    if (state == PaletteState.Disabled)
                        return _sparkleMinI;
                    else
                        return _sparkleMinA;
                case PaletteButtonSpecStyle.FormMax:
                    if (state == PaletteState.Disabled)
                        return _sparkleMaxI;
                    else
                        return _sparkleMaxA;
                case PaletteButtonSpecStyle.FormRestore:
                    if (state == PaletteState.Disabled)
                        return _sparkleRestoreI;
                    else
                        return _sparkleRestoreA;
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
                case PaletteButtonSpecStyle.FormHelp:
                    if (state == PaletteState.Disabled)
                    {
                        return _sparkleHelpI;
                    }
                    else if (state == PaletteState.Tracking)
                    {
                        return _sparkleHelpHover;
                    }
                    else
                    {
                        return _sparkleHelpA;
                    }
                case PaletteButtonSpecStyle.Generic:
                    return null;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        /// <summary>
        /// Gets the image transparent color.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>Color value.</returns>
        public override Color GetButtonSpecImageTransparentColor(PaletteButtonSpecStyle style)
        {
            switch (style)
            {
                case PaletteButtonSpecStyle.Generic:
                    return Color.Empty;
                case PaletteButtonSpecStyle.Close:
                case PaletteButtonSpecStyle.Context:
                case PaletteButtonSpecStyle.Next:
                case PaletteButtonSpecStyle.Previous:
                case PaletteButtonSpecStyle.ArrowLeft:
                case PaletteButtonSpecStyle.ArrowRight:
                case PaletteButtonSpecStyle.ArrowUp:
                case PaletteButtonSpecStyle.ArrowDown:
                case PaletteButtonSpecStyle.DropDown:
                case PaletteButtonSpecStyle.PinVertical:
                case PaletteButtonSpecStyle.PinHorizontal:
                case PaletteButtonSpecStyle.FormClose:
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormMax:
                case PaletteButtonSpecStyle.FormRestore:
                case PaletteButtonSpecStyle.PendantClose:
                case PaletteButtonSpecStyle.PendantMin:
                case PaletteButtonSpecStyle.PendantRestore:
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                case PaletteButtonSpecStyle.WorkspaceRestore:
                case PaletteButtonSpecStyle.RibbonMinimize:
                case PaletteButtonSpecStyle.RibbonExpand:
                    return Color.Magenta;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return Color.Empty;
            }
        }

        /// <summary>
        /// Gets the short text to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>String value.</returns>
        public override string GetButtonSpecShortText(PaletteButtonSpecStyle style)
        {
            switch (style)
            {
                case PaletteButtonSpecStyle.Generic:
                case PaletteButtonSpecStyle.Close:
                case PaletteButtonSpecStyle.Context:
                case PaletteButtonSpecStyle.Next:
                case PaletteButtonSpecStyle.Previous:
                case PaletteButtonSpecStyle.ArrowLeft:
                case PaletteButtonSpecStyle.ArrowRight:
                case PaletteButtonSpecStyle.ArrowUp:
                case PaletteButtonSpecStyle.ArrowDown:
                case PaletteButtonSpecStyle.DropDown:
                case PaletteButtonSpecStyle.PinVertical:
                case PaletteButtonSpecStyle.PinHorizontal:
                case PaletteButtonSpecStyle.FormClose:
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormMax:
                case PaletteButtonSpecStyle.FormRestore:
                case PaletteButtonSpecStyle.PendantClose:
                case PaletteButtonSpecStyle.PendantMin:
                case PaletteButtonSpecStyle.PendantRestore:
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                case PaletteButtonSpecStyle.WorkspaceRestore:
                case PaletteButtonSpecStyle.RibbonMinimize:
                case PaletteButtonSpecStyle.RibbonExpand:
                    return string.Empty;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        /// <summary>
        /// Gets the long text to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>String value.</returns>
        public override string GetButtonSpecLongText(PaletteButtonSpecStyle style)
        {
            switch (style)
            {
                case PaletteButtonSpecStyle.Generic:
                case PaletteButtonSpecStyle.Close:
                case PaletteButtonSpecStyle.Context:
                case PaletteButtonSpecStyle.Next:
                case PaletteButtonSpecStyle.Previous:
                case PaletteButtonSpecStyle.ArrowLeft:
                case PaletteButtonSpecStyle.ArrowRight:
                case PaletteButtonSpecStyle.ArrowUp:
                case PaletteButtonSpecStyle.ArrowDown:
                case PaletteButtonSpecStyle.DropDown:
                case PaletteButtonSpecStyle.PinVertical:
                case PaletteButtonSpecStyle.PinHorizontal:
                case PaletteButtonSpecStyle.FormClose:
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormMax:
                case PaletteButtonSpecStyle.FormRestore:
                case PaletteButtonSpecStyle.PendantClose:
                case PaletteButtonSpecStyle.PendantMin:
                case PaletteButtonSpecStyle.PendantRestore:
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                case PaletteButtonSpecStyle.WorkspaceRestore:
                case PaletteButtonSpecStyle.RibbonMinimize:
                case PaletteButtonSpecStyle.RibbonExpand:
                    return string.Empty;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        /// <summary>
        /// Gets the color to remap from the image to the container foreground.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>Color value.</returns>
        public override Color GetButtonSpecColorMap(PaletteButtonSpecStyle style)
        {
            switch (style)
            {
                case PaletteButtonSpecStyle.FormClose:
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormMax:
                case PaletteButtonSpecStyle.FormRestore:
                case PaletteButtonSpecStyle.PendantClose:
                case PaletteButtonSpecStyle.PendantMin:
                case PaletteButtonSpecStyle.PendantRestore:
                case PaletteButtonSpecStyle.Generic:
                    return Color.Empty;
                case PaletteButtonSpecStyle.Close:
                case PaletteButtonSpecStyle.Context:
                case PaletteButtonSpecStyle.Next:
                case PaletteButtonSpecStyle.Previous:
                case PaletteButtonSpecStyle.ArrowLeft:
                case PaletteButtonSpecStyle.ArrowRight:
                case PaletteButtonSpecStyle.ArrowUp:
                case PaletteButtonSpecStyle.ArrowDown:
                case PaletteButtonSpecStyle.DropDown:
                case PaletteButtonSpecStyle.PinVertical:
                case PaletteButtonSpecStyle.PinHorizontal:
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                case PaletteButtonSpecStyle.WorkspaceRestore:
                case PaletteButtonSpecStyle.RibbonMinimize:
                case PaletteButtonSpecStyle.RibbonExpand:
                    return Color.White;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return Color.Empty;
            }
        }

        /// <summary>
        /// Gets the color to remap to transparent.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>Color value.</returns>
        public override Color GetButtonSpecColorTransparent(PaletteButtonSpecStyle style)
        {
            switch (style)
            {
                case PaletteButtonSpecStyle.Generic:
                    return Color.Empty;
                case PaletteButtonSpecStyle.Close:
                case PaletteButtonSpecStyle.Context:
                case PaletteButtonSpecStyle.Next:
                case PaletteButtonSpecStyle.Previous:
                case PaletteButtonSpecStyle.ArrowLeft:
                case PaletteButtonSpecStyle.ArrowRight:
                case PaletteButtonSpecStyle.ArrowUp:
                case PaletteButtonSpecStyle.DropDown:
                case PaletteButtonSpecStyle.PinVertical:
                case PaletteButtonSpecStyle.PinHorizontal:
                case PaletteButtonSpecStyle.FormClose:
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormMax:
                case PaletteButtonSpecStyle.FormRestore:
                case PaletteButtonSpecStyle.PendantClose:
                case PaletteButtonSpecStyle.PendantMin:
                case PaletteButtonSpecStyle.PendantRestore:
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                case PaletteButtonSpecStyle.WorkspaceRestore:
                case PaletteButtonSpecStyle.RibbonMinimize:
                case PaletteButtonSpecStyle.RibbonExpand:
                    return Color.Magenta;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return Color.Empty;
            }
        }

        /// <summary>
        /// Gets the button style used for drawing the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>PaletteButtonStyle value.</returns>
        public override PaletteButtonStyle GetButtonSpecStyle(PaletteButtonSpecStyle style)
        {
            switch (style)
            {
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormMax:
                case PaletteButtonSpecStyle.FormRestore:
                    return PaletteButtonStyle.Form;
                case PaletteButtonSpecStyle.FormClose:
                    return PaletteButtonStyle.FormClose;
                case PaletteButtonSpecStyle.Generic:
                case PaletteButtonSpecStyle.Close:
                case PaletteButtonSpecStyle.Context:
                case PaletteButtonSpecStyle.Next:
                case PaletteButtonSpecStyle.Previous:
                case PaletteButtonSpecStyle.ArrowLeft:
                case PaletteButtonSpecStyle.ArrowRight:
                case PaletteButtonSpecStyle.ArrowUp:
                case PaletteButtonSpecStyle.ArrowDown:
                case PaletteButtonSpecStyle.DropDown:
                case PaletteButtonSpecStyle.PinVertical:
                case PaletteButtonSpecStyle.PinHorizontal:
                case PaletteButtonSpecStyle.PendantClose:
                case PaletteButtonSpecStyle.PendantMin:
                case PaletteButtonSpecStyle.PendantRestore:
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                case PaletteButtonSpecStyle.WorkspaceRestore:
                case PaletteButtonSpecStyle.RibbonMinimize:
                case PaletteButtonSpecStyle.RibbonExpand:
                    return PaletteButtonStyle.ButtonSpec;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return PaletteButtonStyle.ButtonSpec;
            }
        }

        /// <summary>
        /// Get the location for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>HeaderLocation value.</returns>
        public override HeaderLocation GetButtonSpecLocation(PaletteButtonSpecStyle style)
        {
            switch (style)
            {
                case PaletteButtonSpecStyle.Generic:
                case PaletteButtonSpecStyle.Close:
                case PaletteButtonSpecStyle.Context:
                case PaletteButtonSpecStyle.Next:
                case PaletteButtonSpecStyle.Previous:
                case PaletteButtonSpecStyle.ArrowLeft:
                case PaletteButtonSpecStyle.ArrowRight:
                case PaletteButtonSpecStyle.ArrowUp:
                case PaletteButtonSpecStyle.ArrowDown:
                case PaletteButtonSpecStyle.DropDown:
                case PaletteButtonSpecStyle.PinVertical:
                case PaletteButtonSpecStyle.PinHorizontal:
                case PaletteButtonSpecStyle.FormClose:
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormMax:
                case PaletteButtonSpecStyle.FormRestore:
                case PaletteButtonSpecStyle.PendantClose:
                case PaletteButtonSpecStyle.PendantMin:
                case PaletteButtonSpecStyle.PendantRestore:
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                case PaletteButtonSpecStyle.WorkspaceRestore:
                case PaletteButtonSpecStyle.RibbonMinimize:
                case PaletteButtonSpecStyle.RibbonExpand:
                    return HeaderLocation.PrimaryHeader;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return HeaderLocation.PrimaryHeader;
            }
        }

        /// <summary>
        /// Gets the edge to positon the button against.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>PaletteRelativeEdgeAlign value.</returns>
        public override PaletteRelativeEdgeAlign GetButtonSpecEdge(PaletteButtonSpecStyle style)
        {
            switch (style)
            {
                case PaletteButtonSpecStyle.Generic:
                case PaletteButtonSpecStyle.Close:
                case PaletteButtonSpecStyle.Context:
                case PaletteButtonSpecStyle.Next:
                case PaletteButtonSpecStyle.Previous:
                case PaletteButtonSpecStyle.ArrowLeft:
                case PaletteButtonSpecStyle.ArrowRight:
                case PaletteButtonSpecStyle.ArrowUp:
                case PaletteButtonSpecStyle.ArrowDown:
                case PaletteButtonSpecStyle.DropDown:
                case PaletteButtonSpecStyle.PinVertical:
                case PaletteButtonSpecStyle.PinHorizontal:
                case PaletteButtonSpecStyle.FormClose:
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormMax:
                case PaletteButtonSpecStyle.FormRestore:
                case PaletteButtonSpecStyle.PendantClose:
                case PaletteButtonSpecStyle.PendantMin:
                case PaletteButtonSpecStyle.PendantRestore:
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                case PaletteButtonSpecStyle.WorkspaceRestore:
                case PaletteButtonSpecStyle.RibbonMinimize:
                case PaletteButtonSpecStyle.RibbonExpand:
                    return PaletteRelativeEdgeAlign.Far;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return PaletteRelativeEdgeAlign.Far;
            }
        }

        /// <summary>
        /// Gets the button orientation.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>PaletteButtonOrientation value.</returns>
        public override PaletteButtonOrientation GetButtonSpecOrientation(PaletteButtonSpecStyle style)
        {
            switch (style)
            {
                case PaletteButtonSpecStyle.Close:
                case PaletteButtonSpecStyle.Context:
                case PaletteButtonSpecStyle.ArrowLeft:
                case PaletteButtonSpecStyle.ArrowRight:
                case PaletteButtonSpecStyle.ArrowUp:
                case PaletteButtonSpecStyle.ArrowDown:
                case PaletteButtonSpecStyle.DropDown:
                case PaletteButtonSpecStyle.PinVertical:
                case PaletteButtonSpecStyle.PinHorizontal:
                case PaletteButtonSpecStyle.FormClose:
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormMax:
                case PaletteButtonSpecStyle.FormRestore:
                case PaletteButtonSpecStyle.PendantClose:
                case PaletteButtonSpecStyle.PendantMin:
                case PaletteButtonSpecStyle.PendantRestore:
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                case PaletteButtonSpecStyle.WorkspaceRestore:
                case PaletteButtonSpecStyle.RibbonMinimize:
                case PaletteButtonSpecStyle.RibbonExpand:
                    return PaletteButtonOrientation.FixedTop;
                case PaletteButtonSpecStyle.Generic:
                case PaletteButtonSpecStyle.Next:
                case PaletteButtonSpecStyle.Previous:
                    return PaletteButtonOrientation.Auto;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return PaletteButtonOrientation.Auto;
            }
        }
        #endregion

        #region RibbonGeneral
        /// <summary>
        /// Gets the ribbon shape that should be used.
        /// </summary>
        /// <returns>Ribbon shape value.</returns>
        public override PaletteRibbonShape GetRibbonShape()
        {
            return PaletteRibbonShape.Office2007;
        }

        /// <summary>
        /// Gets the text alignment for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override PaletteRelativeAlign GetRibbonContextTextAlign(PaletteState state)
        {
            return PaletteRelativeAlign.Near;
        }

        /// <summary>
        /// Gets the font for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetRibbonContextTextFont(PaletteState state)
        {
            return _ribbonTabFont;
        }

        /// <summary>
        /// Gets the color for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Color GetRibbonContextTextColor(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabTextNormal];
        }

        /// <summary>
        /// Gets the dark disabled color used for ribbon glyphs.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonDisabledDark(PaletteState state)
        {
            return _disabledGlyphDark;
        }

        /// <summary>
        /// Gets the light disabled color used for ribbon glyphs.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonDisabledLight(PaletteState state)
        {
            return _disabledGlyphLight;
        }

        /// <summary>
        /// Gets the color for the drop arrow light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonDropArrowLight(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupDialogLight];
        }

        /// <summary>
        /// Gets the color for the drop arrow dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonDropArrowDark(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupDialogDark];
        }

        /// <summary>
        /// Gets the color for the dialog launcher dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonGroupDialogDark(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupDialogDark];
        }

        /// <summary>
        /// Gets the color for the dialog launcher light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonGroupDialogLight(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupDialogLight];
        }

        /// <summary>
        /// Gets the color for the group separator dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonGroupSeparatorDark(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupSeparatorDark];
        }

        /// <summary>
        /// Gets the color for the group separator light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonGroupSeparatorLight(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupSeparatorLight];
        }

        /// <summary>
        /// Gets the color for the minimize bar dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonMinimizeBarDark(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonMinimizeBarDark];
        }

        /// <summary>
        /// Gets the color for the minimize bar light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonMinimizeBarLight(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonMinimizeBarLight];
        }

        /// <summary>
        /// Gets the color for the tab separator.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonTabSeparatorColor(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabSeparatorColor];
        }

        /// <summary>
        /// Gets the color for the tab context separators.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonTabSeparatorContextColor(PaletteState state)
        {
            return _sparkleColors[3];
        }

        /// <summary>
        /// Gets the font for the ribbon text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetRibbonTextFont(PaletteState state)
        {
            return _ribbonTabFont;
        }

        /// <summary>
        /// Gets the rendering hint for the ribbon font.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextHint value.</returns>
        public override PaletteTextHint GetRibbonTextHint(PaletteState state)
        {
            return PaletteTextHint.ClearTypeGridFit;
        }

        /// <summary>
        /// Gets the color for the extra QAT button dark content color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonQATButtonDark(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonQATButtonDark];
        }

        /// <summary>
        /// Gets the color for the extra QAT button light content color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonQATButtonLight(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonQATButtonLight];
        }
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
                    if (state == PaletteState.CheckedNormal)
                        return PaletteRibbonColorStyle.RibbonQATMinibarDouble;
                    else
                        return PaletteRibbonColorStyle.RibbonQATMinibarSingle;
                case PaletteRibbonBackStyle.RibbonQATFullbar:
                    return PaletteRibbonColorStyle.RibbonQATFullbarRound;
                case PaletteRibbonBackStyle.RibbonQATOverflow:
                    return PaletteRibbonColorStyle.RibbonQATOverflow;
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
                    return PaletteRibbonColorStyle.RibbonGroupCollapsedFrameBorder;
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                    return PaletteRibbonColorStyle.RibbonGroupCollapsedBorder;
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                    switch (state)
                    {
                        case PaletteState.ContextNormal:
                        case PaletteState.ContextTracking:
                            return PaletteRibbonColorStyle.RibbonGroupGradientOne;
                        default:
                            return PaletteRibbonColorStyle.RibbonGroupCollapsedFrameBack;
                    }
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
                        case PaletteState.ContextCheckedNormal:
                            return PaletteRibbonColorStyle.RibbonGroupAreaBorder2;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
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
                        case PaletteState.ContextTracking:
                            return PaletteRibbonColorStyle.RibbonTabGlowing;
                        case PaletteState.Pressed:
                            return PaletteRibbonColorStyle.RibbonTabTracking2007;
                        case PaletteState.CheckedNormal:
                            return PaletteRibbonColorStyle.RibbonTabSelected2007;
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return PaletteRibbonColorStyle.RibbonTabSelected2007;
                        case PaletteState.ContextCheckedNormal:
                        case PaletteState.ContextCheckedTracking:
                        case PaletteState.FocusOverride:
                            return PaletteRibbonColorStyle.RibbonTabContextSelected;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
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
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        case PaletteState.Tracking:
                            return _colorWhite238;
                        case PaletteState.Normal:
                        default:
                            return _colorWhite192;
                    }
                case PaletteRibbonBackStyle.RibbonGalleryBorder:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBorder;
                        case PaletteState.Normal:
                        case PaletteState.Tracking:
                        default:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGalleryBorder];
                    }
                case PaletteRibbonBackStyle.RibbonAppMenuDocs:
                    return _ribbonColors[(int)SchemeOfficeColors.AppButtonMenuDocsBack];
                case PaletteRibbonBackStyle.RibbonAppMenuInner:
                    return _ribbonColors[(int)SchemeOfficeColors.AppButtonInner1];
                case PaletteRibbonBackStyle.RibbonAppMenuOuter:
                    return _ribbonColors[(int)SchemeOfficeColors.AppButtonOuter1];
                case PaletteRibbonBackStyle.RibbonQATMinibar:
                    if (state == PaletteState.Normal)
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonQATMini1];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonQATMini1I];
                case PaletteRibbonBackStyle.RibbonQATFullbar:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonQATFullbar1];
                case PaletteRibbonBackStyle.RibbonQATOverflow:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonQATOverflow1];
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return _sparkleColors[30];
                        default:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupFrameBorder1];
                    }
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupCollapsedBorder1];
                        case PaletteState.Tracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupCollapsedBorderT1];
                        case PaletteState.ContextNormal:
                            return _ribbonGroupCollapsedBorderContext[0];
                        case PaletteState.ContextTracking:
                        case PaletteState.Pressed:
                            return _ribbonGroupCollapsedBorderContextTracking[0];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                    switch (state)
                    {
                        case PaletteState.ContextNormal:
                        case PaletteState.ContextTracking:
                            return _contextGroupFrameTop;
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return _sparkleColors[32];
                        default:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupFrameInside1];
                    }
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupCollapsedBack1];
                        case PaletteState.Tracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupCollapsedBackT1];
                        case PaletteState.ContextNormal:
                            return _ribbonGroupCollapsedBackContext[0];
                        case PaletteState.ContextTracking:
                            return _ribbonGroupCollapsedBackContextTracking[0];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupTitle1];
                        case PaletteState.ContextNormal:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupTitleContext1];
                        case PaletteState.Tracking:
                        case PaletteState.ContextTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupTitleTracking1];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
                    switch (state)
                    {
                        case PaletteState.Normal:
                        case PaletteState.Tracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupBorder1];
                        case PaletteState.ContextNormal:
                        case PaletteState.ContextTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupBorderContext1];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
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
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonGroupArea:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupsArea1];
                case PaletteRibbonBackStyle.RibbonTab:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                        case PaletteState.ContextTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabTracking1];
                        case PaletteState.CheckedNormal:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabSelected1];
                        case PaletteState.CheckedTracking:
                            return _colorDark00;
                        case PaletteState.CheckedPressed:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabHighlight1];
                        case PaletteState.ContextCheckedNormal:
                        case PaletteState.ContextCheckedTracking:
                        case PaletteState.FocusOverride:
                            return _colorDark00;
                        case PaletteState.Normal:
                            return Color.Empty;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
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
                    return _ribbonColors[(int)SchemeOfficeColors.AppButtonInner2];
                case PaletteRibbonBackStyle.RibbonAppMenuOuter:
                    return _ribbonColors[(int)SchemeOfficeColors.AppButtonOuter2];
                case PaletteRibbonBackStyle.RibbonQATMinibar:
                    if (state == PaletteState.Normal)
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonQATMini2];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonQATMini2I];
                case PaletteRibbonBackStyle.RibbonQATFullbar:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonQATFullbar2];
                case PaletteRibbonBackStyle.RibbonQATOverflow:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonQATOverflow2];
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return _sparkleColors[31];
                        default:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupFrameBorder2];
                    }
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupCollapsedBorder2];
                        case PaletteState.Tracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupCollapsedBorderT2];
                        case PaletteState.ContextNormal:
                            return _ribbonGroupCollapsedBorderContext[1];
                        case PaletteState.ContextTracking:
                        case PaletteState.Pressed:
                            return _ribbonGroupCollapsedBorderContextTracking[1];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                    switch (state)
                    {
                        case PaletteState.ContextNormal:
                        case PaletteState.ContextTracking:
                            return _contextGroupFrameBottom;
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return _sparkleColors[33];
                        default:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupFrameInside2];
                    }
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupCollapsedBack2];
                        case PaletteState.Tracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupCollapsedBackT2];
                        case PaletteState.ContextNormal:
                            return _ribbonGroupCollapsedBackContext[1];
                        case PaletteState.ContextTracking:
                            return _ribbonGroupCollapsedBackContextTracking[1];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupTitle2];
                        case PaletteState.ContextNormal:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupTitleContext2];
                        case PaletteState.Tracking:
                        case PaletteState.ContextTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupTitleTracking2];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
                    switch (state)
                    {
                        case PaletteState.Normal:
                        case PaletteState.Tracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupBorder2];
                        case PaletteState.ContextNormal:
                        case PaletteState.ContextTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupBorderContext2];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
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
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonGroupArea:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupsArea2];
                case PaletteRibbonBackStyle.RibbonTab:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                            return _sparkleColors[35];
                        case PaletteState.Pressed:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabTracking2];
                        case PaletteState.CheckedNormal:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabSelected2];
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabHighlight2];
                        case PaletteState.ContextCheckedTracking:
                            return _sparkleColors[36];
                        case PaletteState.FocusOverride:
                            return _sparkleColors[37];
                        case PaletteState.ContextTracking:
                        case PaletteState.ContextCheckedNormal:
                            return Color.Empty;
                        case PaletteState.Normal:
                            return Color.Empty;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonAppMenuDocs:
                case PaletteRibbonBackStyle.RibbonGalleryBack:
                case PaletteRibbonBackStyle.RibbonGalleryBorder:
                    return Color.Empty;
                default:
                    // Should never happen!
                    Debug.Assert(false);
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
                    return _ribbonColors[(int)SchemeOfficeColors.AppButtonOuter3];
                case PaletteRibbonBackStyle.RibbonQATMinibar:
                    if (state == PaletteState.Normal)
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonQATMini3];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonQATMini3I];
                case PaletteRibbonBackStyle.RibbonQATFullbar:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonQATFullbar3];
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupCollapsedBorder3];
                        case PaletteState.Tracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupCollapsedBorderT3];
                        case PaletteState.ContextNormal:
                            return _ribbonGroupCollapsedBorderContext[2];
                        case PaletteState.ContextTracking:
                        case PaletteState.Pressed:
                            return _ribbonGroupCollapsedBorderContextTracking[2];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                    switch (state)
                    {
                        case PaletteState.ContextNormal:
                        case PaletteState.ContextTracking:
                            return Color.Empty;
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return _sparkleColors[34];
                        default:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupFrameInside3];
                    }
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupCollapsedBack3];
                        case PaletteState.Tracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupCollapsedBackT3];
                        case PaletteState.ContextNormal:
                        case PaletteState.ContextTracking:
                            return Color.Empty;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
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
                    return Color.Empty;
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
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonGroupArea:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupsArea3];
                case PaletteRibbonBackStyle.RibbonTab:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabTracking2];
                        case PaletteState.CheckedNormal:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabSelected3];
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabHighlight3];
                        case PaletteState.ContextTracking:
                        case PaletteState.ContextCheckedNormal:
                        case PaletteState.ContextCheckedTracking:
                        case PaletteState.FocusOverride:
                        case PaletteState.Normal:
                            return Color.Empty;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
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
                    if (state == PaletteState.Normal)
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonQATMini4];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonQATMini4I];
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupCollapsedBorder4];
                        case PaletteState.Tracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupCollapsedBorderT4];
                        case PaletteState.ContextNormal:
                            return _ribbonGroupCollapsedBorderContext[3];
                        case PaletteState.ContextTracking:
                        case PaletteState.Pressed:
                            return _ribbonGroupCollapsedBorderContextTracking[3];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                    switch (state)
                    {
                        case PaletteState.ContextNormal:
                        case PaletteState.ContextTracking:
                            return Color.Empty;
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return _ribbonFrameBack4;
                        default:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupFrameInside4];
                    }
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupCollapsedBack4];
                        case PaletteState.Tracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupCollapsedBackT4];
                        case PaletteState.ContextNormal:
                        case PaletteState.ContextTracking:
                            return Color.Empty;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
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
                    return Color.Empty;
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
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonGroupArea:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupsArea4];
                case PaletteRibbonBackStyle.RibbonTab:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabTracking2];
                        case PaletteState.CheckedNormal:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabSelected4];
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabHighlight4];
                        case PaletteState.ContextTracking:
                        case PaletteState.ContextCheckedNormal:
                        case PaletteState.ContextCheckedTracking:
                        case PaletteState.FocusOverride:
                        case PaletteState.Normal:
                            return Color.Empty;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
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
                    return Color.Empty;
                case PaletteRibbonBackStyle.RibbonQATMinibar:
                    if (state == PaletteState.Normal)
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonQATMini5];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonQATMini5I];
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
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonGroupArea:
                    if (state == PaletteState.ContextCheckedNormal)
                        return Color.Empty;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupsArea5];
                case PaletteRibbonBackStyle.RibbonTab:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledText;
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabTracking2];
                        case PaletteState.CheckedNormal:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabSelected5];
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabHighlight5];
                        case PaletteState.ContextTracking:
                        case PaletteState.ContextCheckedNormal:
                        case PaletteState.ContextCheckedTracking:
                        case PaletteState.FocusOverride:
                        case PaletteState.Normal:
                            return Color.Empty;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
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
                    return _ribbonColors[(int)SchemeOfficeColors.AppButtonMenuDocsText];
                case PaletteRibbonTextStyle.RibbonTab:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledText;
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return _colorWhite255;
                        case PaletteState.ContextCheckedNormal:
                        case PaletteState.ContextCheckedTracking:
                        case PaletteState.FocusOverride:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabTextChecked];
                        default:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabTextNormal];
                    }
                case PaletteRibbonTextStyle.RibbonGroupNormalTitle:
                case PaletteRibbonTextStyle.RibbonGroupCollapsedText:
                case PaletteRibbonTextStyle.RibbonGroupButtonText:
                case PaletteRibbonTextStyle.RibbonGroupLabelText:
                case PaletteRibbonTextStyle.RibbonGroupCheckBoxText:
                case PaletteRibbonTextStyle.RibbonGroupRadioButtonText:
                    if (state == PaletteState.Disabled)
                        return _disabledText;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupCollapsedText];
                default:
                    // Should never happen!
                    Debug.Assert(false);
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
                return Color.Empty;

            switch (element)
            {
                case PaletteElement.TrackBarTick:
                    return _trackBarColors[0];
                case PaletteElement.TrackBarTrack:
                    return _trackBarColors[1];
                case PaletteElement.TrackBarPosition:
                    return _trackBarColors[4];
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return Color.Red;
        }

        /// <summary>
        /// Gets the second element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetElementColor2(PaletteElement element, PaletteState state)
        {
            if (CommonHelper.IsOverrideState(state))
                return Color.Empty;

            switch (element)
            {
                case PaletteElement.TrackBarTick:
                    return _trackBarColors[0];
                case PaletteElement.TrackBarTrack:
                    return _trackBarColors[2];
                case PaletteElement.TrackBarPosition:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return ControlPaint.Light(_colorDark00);
                        case PaletteState.Normal:
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return _colorDark00;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return Color.Red;
        }

        /// <summary>
        /// Gets the third element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetElementColor3(PaletteElement element, PaletteState state)
        {
            if (CommonHelper.IsOverrideState(state))
                return Color.Empty;

            switch (element)
            {
                case PaletteElement.TrackBarTick:
                    return _trackBarColors[0];
                case PaletteElement.TrackBarTrack:
                    return _trackBarColors[3];
                case PaletteElement.TrackBarPosition:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return ControlPaint.LightLight(_sparkleColors[5]);
                        case PaletteState.Normal:
                        case PaletteState.FocusOverride:
                            return ControlPaint.Light(_sparkleColors[5]);
                        case PaletteState.Tracking:
                            return ControlPaint.Light(_sparkleColors[6]);
                        case PaletteState.Pressed:
                            return ControlPaint.Light(_sparkleColors[8]);
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }

                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return Color.Red;
        }

        /// <summary>
        /// Gets the fourth element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetElementColor4(PaletteElement element, PaletteState state)
        {
            switch (element)
            {
                case PaletteElement.TrackBarTick:
                    if (CommonHelper.IsOverrideState(state))
                        return Color.Empty;

                    return _trackBarColors[0];
                case PaletteElement.TrackBarTrack:
                    if (CommonHelper.IsOverrideState(state))
                        return Color.Empty;

                    return _trackBarColors[3];
                case PaletteElement.TrackBarPosition:
                    if (CommonHelper.IsOverrideStateExclude(state, PaletteState.FocusOverride))
                        return Color.Empty;

                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return ControlPaint.LightLight(_sparkleColors[5]);
                        case PaletteState.Normal:
                            return _sparkleColors[5];
                        case PaletteState.Tracking:
                        case PaletteState.FocusOverride:
                            return _sparkleColors[6];
                        case PaletteState.Pressed:
                            return _sparkleColors[8];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return Color.Red;
        }

        /// <summary>
        /// Gets the fifth element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetElementColor5(PaletteElement element, PaletteState state)
        {
            switch (element)
            {
                case PaletteElement.TrackBarTick:
                    if (CommonHelper.IsOverrideState(state))
                        return Color.Empty;

                    return _trackBarColors[0];
                case PaletteElement.TrackBarTrack:
                    if (CommonHelper.IsOverrideState(state))
                        return Color.Empty;

                    return _trackBarColors[3];
                case PaletteElement.TrackBarPosition:
                    if (CommonHelper.IsOverrideStateExclude(state, PaletteState.FocusOverride))
                        return Color.Empty;

                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return ControlPaint.LightLight(_sparkleColors[5]);
                        case PaletteState.Normal:
                            return _sparkleColors[22];
                        case PaletteState.Tracking:
                        case PaletteState.FocusOverride:
                            return _sparkleColors[7];
                        case PaletteState.Pressed:
                            return _sparkleColors[9];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return Color.Red;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the base font name used when defining fonts.
        /// </summary>
        public virtual string BaseFontName
        {
            get
            {
                if (string.IsNullOrEmpty(_baseFontName))
                    return "Segoe UI";
                else
                    return _baseFontName;
            }

            set
            {
                // Is there a change in value?
                if ((string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(_baseFontName)) ||
                    (!string.IsNullOrEmpty(value) && string.IsNullOrEmpty(_baseFontName)))
                {
                    // Cache new value
                    _baseFontName = value;

                    // Update fonts to reflect change
                    DefineFonts();

                    // Use event to indicate palette has caused layout changes
                    OnPalettePaint(this, new PaletteLayoutEventArgs(true, false));
                }
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Update the fonts to reflect system or user defined changes.
        /// </summary>
        protected override void DefineFonts()
        {
            // Release existing resources
            if (_header1ShortFont != null) _header1ShortFont.Dispose();
            if (_header2ShortFont != null) _header2ShortFont.Dispose();
            if (_headerFormFont != null) _headerFormFont.Dispose();
            if (_header1LongFont != null) _header1LongFont.Dispose();
            if (_header2LongFont != null) _header2LongFont.Dispose();
            if (_buttonFont != null) _buttonFont.Dispose();
            if (_buttonFontNavigatorStack != null) _buttonFontNavigatorStack.Dispose();
            if (_buttonFontNavigatorMini != null) _buttonFontNavigatorMini.Dispose();
            if (_tabFontSelected != null) _tabFontSelected.Dispose();
            if (_tabFontNormal != null) _tabFontNormal.Dispose();
            if (_ribbonTabFont != null) _ribbonTabFont.Dispose();
            if (_gridFont != null) _gridFont.Dispose();
            if (_calendarFont != null) _calendarFont.Dispose();
            if (_calendarBoldFont != null) _calendarBoldFont.Dispose();
            if (_superToolFont != null) _superToolFont.Dispose();
            if (_boldFont != null) _boldFont.Dispose();
            if (_italicFont != null) _italicFont.Dispose();

            float baseFontSize = BaseFontSize;
            string baseFontName = BaseFontName;
            _header1ShortFont = new Font(baseFontName, baseFontSize + 4.5f, FontStyle.Bold);
            _header2ShortFont = new Font(baseFontName, baseFontSize, FontStyle.Regular);
            _headerFormFont = new Font(baseFontName, SystemFonts.CaptionFont.SizeInPoints, FontStyle.Regular);
            _header1LongFont = new Font(baseFontName, baseFontSize + 1.5f, FontStyle.Regular);
            _header2LongFont = new Font(baseFontName, baseFontSize, FontStyle.Regular);
            _buttonFont = new Font(baseFontName, baseFontSize, FontStyle.Regular);
            _buttonFontNavigatorStack = new Font(_buttonFont, FontStyle.Bold);
            _buttonFontNavigatorMini = new Font(baseFontName, baseFontSize + 3.5f, FontStyle.Bold);
            _tabFontNormal = new Font(baseFontName, baseFontSize, FontStyle.Regular);
            _tabFontSelected = new Font(_tabFontNormal, FontStyle.Bold);
            _ribbonTabFont = new Font(baseFontName, baseFontSize, FontStyle.Regular);
            _gridFont = new Font(baseFontName, baseFontSize, FontStyle.Regular);
            _superToolFont = new Font(baseFontName, baseFontSize, FontStyle.Bold);
            _calendarFont = new Font(baseFontName, baseFontSize, FontStyle.Regular);
            _calendarBoldFont = new Font(baseFontName, baseFontSize, FontStyle.Bold);
            _boldFont = new Font(baseFontName, baseFontSize, FontStyle.Bold);
            _italicFont = new Font(baseFontName, baseFontSize, FontStyle.Italic);
        }
        #endregion

        #region ColorTable
        /// <summary>
        /// Gets access to the color table instance.
        /// </summary>
        public override KryptonColorTable ColorTable
        {
            get
            {
                if (_table == null)
                    _table = new KryptonColorTableSparkleBlue(_ribbonColors, _sparkleColors, InheritBool.True, this);

                return _table;
            }
        }
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

    #region Class: KryptonColorTableSparkleBlue
    /// <summary>
    /// Provide KryptonColorTable values using an array of Color values as the source.
    /// </summary>
    public class KryptonColorTableSparkleBlue : KryptonColorTable
    {
        #region Static Fields
        private static readonly Color _menuBorder = Color.Black;
        private static readonly Color _menuItemText = Color.Black;
        private static readonly Color _contextMenuBackground = Color.FromArgb(240, 240, 240);
        private static readonly Color _imageMarginMiddle = Color.FromArgb(226, 227, 227);
        private static readonly Color _imageMarginEnd = Color.White;
        private static Font _menuToolFont;
        private static Font _statusFont;
        #endregion

        #region Instance Fields
        private Color[] _colors;
        private Color[] _sparkleColors;
        private InheritBool _roundedEdges;
        #endregion

        #region Identity
        static KryptonColorTableSparkleBlue()
        {
            // Get the font settings from the system
            DefineFonts();

            // We need to notice when system color settings change
            SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(OnUserPreferenceChanged);
        }

        /// <summary>
        /// Initialize a new instance of the KryptonColorTableSparkleBlue class.
        /// </summary>
        /// <param name="colors">Source of ribbon colors.</param>
        /// <param name="sparkleColors">Source of sparkle colors.</param>
        /// <param name="roundedEdges">Should have rounded edges.</param>
        /// <param name="palette">Associated palette instance.</param>
        public KryptonColorTableSparkleBlue(Color[] colors,
                                        Color[] sparkleColors,
                                        InheritBool roundedEdges,
                                        IPalette palette)
            : base(palette)
        {
            Debug.Assert(colors != null);
            Debug.Assert(sparkleColors != null);

            _colors = colors;
            _sparkleColors = sparkleColors;
            _roundedEdges = roundedEdges;
        }
        #endregion

        #region RoundedEdges
        /// <summary>
        /// Gets a value indicating if rounded egdes are required.
        /// </summary>
        public override InheritBool UseRoundedEdges
        {
            get { return _roundedEdges; }
        }
        #endregion

        #region ButtonPressed
        #region ButtonPressedBorder
        /// <summary>
        /// Gets the border color for a button being pressed.
        /// </summary>
        public override Color ButtonPressedBorder
        {
            get { return Color.Black; }
        }
        #endregion

        #region ButtonPressedGradientBegin
        /// <summary>
        /// Gets the background starting color for a button being pressed.
        /// </summary>
        public override Color ButtonPressedGradientBegin
        {
            get { return _sparkleColors[8]; }
        }
        #endregion

        #region ButtonPressedGradientMiddle
        /// <summary>
        /// Gets the background middle color for a button being pressed.
        /// </summary>
        public override Color ButtonPressedGradientMiddle
        {
            get { return _sparkleColors[8]; }
        }
        #endregion

        #region ButtonPressedGradientEnd
        /// <summary>
        /// Gets the background ending color for a button being pressed.
        /// </summary>
        public override Color ButtonPressedGradientEnd
        {
            get { return _sparkleColors[9]; }
        }
        #endregion

        #region ButtonPressedHighlight
        /// <summary>
        /// Gets the highlight background for a pressed button.
        /// </summary>
        public override Color ButtonPressedHighlight
        {
            get { return _sparkleColors[8]; }
        }
        #endregion

        #region ButtonPressedHighlightBorder
        /// <summary>
        /// Gets the highlight border for a pressed button.
        /// </summary>
        public override Color ButtonPressedHighlightBorder
        {
            get { return _colors[(int)SchemeOfficeColors.ButtonBorder]; }
        }
        #endregion
        #endregion

        #region ButtonSelected
        #region ButtonSelectedBorder
        /// <summary>
        /// Gets the border color for a button being selected.
        /// </summary>
        public override Color ButtonSelectedBorder
        {
            get { return Color.Black; }
        }
        #endregion

        #region ButtonSelectedGradientBegin
        /// <summary>
        /// Gets the background starting color for a button being selected.
        /// </summary>
        public override Color ButtonSelectedGradientBegin
        {
            get { return _sparkleColors[6]; }
        }
        #endregion

        #region ButtonSelectedGradientMiddle
        /// <summary>
        /// Gets the background middle color for a button being selected.
        /// </summary>
        public override Color ButtonSelectedGradientMiddle
        {
            get { return _sparkleColors[17]; }
        }
        #endregion

        #region ButtonSelectedGradientEnd
        /// <summary>
        /// Gets the background ending color for a button being selected.
        /// </summary>
        public override Color ButtonSelectedGradientEnd
        {
            get { return _sparkleColors[7]; }
        }
        #endregion

        #region ButtonSelectedHighlight
        /// <summary>
        /// Gets the highlight background for a selected button.
        /// </summary>
        public override Color ButtonSelectedHighlight
        {
            get { return _sparkleColors[12]; }
        }
        #endregion

        #region ButtonSelectedHighlightBorder
        /// <summary>
        /// Gets the highlight border for a selected button.
        /// </summary>
        public override Color ButtonSelectedHighlightBorder
        {
            get { return _colors[(int)SchemeOfficeColors.ButtonBorder]; }
        }
        #endregion
        #endregion

        #region ButtonChecked
        #region ButtonCheckedGradientBegin
        /// <summary>
        /// Gets the background starting color for a checked button.
        /// </summary>
        public override Color ButtonCheckedGradientBegin
        {
            get { return _sparkleColors[10]; }
        }
        #endregion

        #region ButtonCheckedGradientMiddle
        /// <summary>
        /// Gets the background middle color for a checked button.
        /// </summary>
        public override Color ButtonCheckedGradientMiddle
        {
            get { return _sparkleColors[10]; }
        }
        #endregion

        #region ButtonCheckedGradientEnd
        /// <summary>
        /// Gets the background ending color for a checked button.
        /// </summary>
        public override Color ButtonCheckedGradientEnd
        {
            get { return _sparkleColors[11]; }
        }
        #endregion

        #region ButtonCheckedHighlight
        /// <summary>
        /// Gets the highlight background for a checked button.
        /// </summary>
        public override Color ButtonCheckedHighlight
        {
            get { return _sparkleColors[10]; }
        }
        #endregion

        #region ButtonCheckedHighlightBorder
        /// <summary>
        /// Gets the highlight border for a checked button.
        /// </summary>
        public override Color ButtonCheckedHighlightBorder
        {
            get { return _colors[(int)SchemeOfficeColors.ButtonBorder]; }
        }
        #endregion
        #endregion

        #region Check
        #region CheckBackground
        /// <summary>
        /// Get background of the check mark area.
        /// </summary>
        public override Color CheckBackground
        {
            get { return _sparkleColors[20]; }
        }
        #endregion

        #region CheckPressedBackground
        /// <summary>
        /// Get background of a pressed check mark area.
        /// </summary>
        public override Color CheckPressedBackground
        {
            get { return _sparkleColors[20]; ; }
        }
        #endregion

        #region CheckSelectedBackground
        /// <summary>
        /// Get background of a selected check mark area.
        /// </summary>
        public override Color CheckSelectedBackground
        {
            get { return _sparkleColors[20]; ; }
        }
        #endregion
        #endregion

        #region Grip
        #region GripLight
        /// <summary>
        /// Gets the light color used to draw grips.
        /// </summary>
        public override Color GripLight
        {
            get { return _colors[(int)SchemeOfficeColors.GripLight]; }
        }
        #endregion

        #region GripDark
        /// <summary>
        /// Gets the dark color used to draw grips.
        /// </summary>
        public override Color GripDark
        {
            get { return _colors[(int)SchemeOfficeColors.GripDark]; }
        }
        #endregion
        #endregion

        #region ImageMargin
        #region ImageMarginGradientBegin
        /// <summary>
        /// Gets the starting color for the context menu margin.
        /// </summary>
        public override Color ImageMarginGradientBegin
        {
            get { return _colors[(int)SchemeOfficeColors.ImageMargin]; }
        }
        #endregion

        #region ImageMarginGradientMiddle
        /// <summary>
        /// Gets the middle color for the context menu margin.
        /// </summary>
        public override Color ImageMarginGradientMiddle
        {
            get { return _imageMarginMiddle; }
        }
        #endregion

        #region ImageMarginGradientEnd
        /// <summary>
        /// Gets the ending color for the context menu margin.
        /// </summary>
        public override Color ImageMarginGradientEnd
        {
            get { return _imageMarginEnd; }
        }
        #endregion

        #region ImageMarginRevealedGradientBegin
        /// <summary>
        /// Gets the starting color for the context menu margin revealed.
        /// </summary>
        public override Color ImageMarginRevealedGradientBegin
        {
            get { return _colors[(int)SchemeOfficeColors.ImageMargin]; }
        }
        #endregion

        #region ImageMarginRevealedGradientMiddle
        /// <summary>
        /// Gets the middle color for the context menu margin revealed.
        /// </summary>
        public override Color ImageMarginRevealedGradientMiddle
        {
            get { return _colors[(int)SchemeOfficeColors.ImageMargin]; }
        }
        #endregion

        #region ImageMarginRevealedGradientEnd
        /// <summary>
        /// Gets the ending color for the context menu margin revealed.
        /// </summary>
        public override Color ImageMarginRevealedGradientEnd
        {
            get { return _colors[(int)SchemeOfficeColors.ImageMargin]; }
        }
        #endregion
        #endregion

        #region MenuBorder
        /// <summary>
        /// Gets the color of the border around menus.
        /// </summary>
        public override Color MenuBorder
        {
            get { return _menuBorder; }
        }
        #endregion

        #region MenuItem
        #region MenuItemBorder
        /// <summary>
        /// Gets the border color for around the menu item.
        /// </summary>
        public override Color MenuItemBorder
        {
            get { return _menuBorder; }
        }
        #endregion

        #region MenuItemSelected
        /// <summary>
        /// Gets the color of a selected menu item.
        /// </summary>
        public override Color MenuItemSelected
        {
            get { return _colors[(int)SchemeOfficeColors.ButtonBorder]; }
        }
        #endregion

        #region MenuItemPressedGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used when a top-level ToolStripMenuItem is pressed down.
        /// </summary>
        public override Color MenuItemPressedGradientBegin
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripBegin]; }
        }
        #endregion

        #region MenuItemPressedGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used when a top-level ToolStripMenuItem is pressed down.
        /// </summary>
        public override Color MenuItemPressedGradientEnd
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripEnd]; }
        }
        #endregion

        #region MenuItemPressedGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used when a top-level ToolStripMenuItem is pressed down.
        /// </summary>
        public override Color MenuItemPressedGradientMiddle
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripMiddle]; }
        }
        #endregion

        #region MenuItemSelectedGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used when the ToolStripMenuItem is selected.
        /// </summary>
        public override Color MenuItemSelectedGradientBegin
        {
            get { return _colors[(int)SchemeOfficeColors.ButtonBorder]; }
        }
        #endregion

        #region MenuItemSelectedGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used when the ToolStripMenuItem is selected.
        /// </summary>
        public override Color MenuItemSelectedGradientEnd
        {
            get { return _colors[(int)SchemeOfficeColors.ButtonBorder]; }
        }
        #endregion
        #endregion

        #region MenuStrip
        #region MenuStripGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the MenuStrip.
        /// </summary>
        public override Color MenuStripGradientBegin
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripBack]; }
        }
        #endregion

        #region MenuStripGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the MenuStrip.
        /// </summary>
        public override Color MenuStripGradientEnd
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripBack]; }
        }
        #endregion

        #endregion

        #region OverflowButton
        #region OverflowButtonGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the ToolStripOverflowButton.
        /// </summary>
        public override Color OverflowButtonGradientBegin
        {
            get { return _colors[(int)SchemeOfficeColors.OverflowBegin]; }
        }
        #endregion

        #region OverflowButtonGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStripOverflowButton.
        /// </summary>
        public override Color OverflowButtonGradientEnd
        {
            get { return _colors[(int)SchemeOfficeColors.OverflowEnd]; }
        }
        #endregion

        #region OverflowButtonGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used in the ToolStripOverflowButton.
        /// </summary>
        public override Color OverflowButtonGradientMiddle
        {
            get { return _colors[(int)SchemeOfficeColors.OverflowMiddle]; }
        }
        #endregion
        #endregion

        #region RaftingContainer
        #region RaftingContainerGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the ToolStripContainer.
        /// </summary>
        public override Color RaftingContainerGradientBegin
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripBack]; }
        }
        #endregion

        #region RaftingContainerGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStripContainer.
        /// </summary>
        public override Color RaftingContainerGradientEnd
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripBack]; }
        }
        #endregion

        #endregion

        #region Separator
        #region SeparatorLight
        /// <summary>
        /// Gets the light separator color.
        /// </summary>
        public override Color SeparatorLight
        {
            get { return _colors[(int)SchemeOfficeColors.SeparatorLight]; }
        }
        #endregion

        #region SeparatorDark
        /// <summary>
        /// Gets the dark separator color.
        /// </summary>
        public override Color SeparatorDark
        {
            get { return _colors[(int)SchemeOfficeColors.SeparatorDark]; }
        }
        #endregion
        #endregion

        #region StatusStrip
        #region StatusStripGradientBegin
        /// <summary>
        /// Gets the starting color for the status strip background.
        /// </summary>
        public override Color StatusStripGradientBegin
        {
            get { return _colors[(int)SchemeOfficeColors.StatusStripLight]; }
        }
        #endregion

        #region StatusStripGradientEnd
        /// <summary>
        /// Gets the ending color for the status strip background.
        /// </summary>
        public override Color StatusStripGradientEnd
        {
            get { return _colors[(int)SchemeOfficeColors.StatusStripDark]; }
        }
        #endregion
        #endregion

        #region Text
        #region MenuItemText
        /// <summary>
        /// Gets the text color used on the menu items.
        /// </summary>
        public override Color MenuItemText
        {
            get { return _menuItemText; }
        }
        #endregion

        #region MenuStripText
        /// <summary>
        /// Gets the text color used on the menu strip.
        /// </summary>
        public override Color MenuStripText
        {
            get { return _colors[(int)SchemeOfficeColors.TextLabelPanel]; }
        }
        #endregion

        #region ToolStripText
        /// <summary>
        /// Gets the text color used on the tool strip.
        /// </summary>
        public override Color ToolStripText
        {
            get { return _colors[(int)SchemeOfficeColors.StatusStripText]; }
        }
        #endregion

        #region StatusStripText
        /// <summary>
        /// Gets the text color used on the status strip.
        /// </summary>
        public override Color StatusStripText
        {
            get { return _colors[(int)SchemeOfficeColors.StatusStripText]; }
        }
        #endregion

        #region MenuStripFont
        /// <summary>
        /// Gets the font used on the menu strip.
        /// </summary>
        public override Font MenuStripFont
        {
            get { return _menuToolFont; }
        }
        #endregion

        #region ToolStripFont
        /// <summary>
        /// Gets the font used on the tool strip.
        /// </summary>
        public override Font ToolStripFont
        {
            get { return _menuToolFont; }
        }
        #endregion

        #region StatusStripFont
        /// <summary>
        /// Gets the font used on the status strip.
        /// </summary>
        public override Font StatusStripFont
        {
            get { return _statusFont; }
        }
        #endregion
        #endregion

        #region ToolStrip
        #region ToolStripBorder
        /// <summary>
        /// Gets the border color to use on the bottom edge of the ToolStrip.
        /// </summary>
        public override Color ToolStripBorder
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripBorder]; }
        }
        #endregion

        #region ToolStripContentPanelGradientBegin
        /// <summary>
        /// Gets the starting color for the content panel background.
        /// </summary>
        public override Color ToolStripContentPanelGradientBegin
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripBack]; }
        }
        #endregion

        #region ToolStripContentPanelGradientEnd
        /// <summary>
        /// Gets the ending color for the content panel background.
        /// </summary>
        public override Color ToolStripContentPanelGradientEnd
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripBack]; }
        }
        #endregion

        #region ToolStripDropDownBackground
        /// <summary>
        /// Gets the background color for drop down menus.
        /// </summary>
        public override Color ToolStripDropDownBackground
        {
            get { return _contextMenuBackground; }
        }
        #endregion

        #region ToolStripGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the ToolStrip background.
        /// </summary>
        public override Color ToolStripGradientBegin
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripBegin]; }
        }
        #endregion

        #region ToolStripGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStrip background.
        /// </summary>
        public override Color ToolStripGradientEnd
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripEnd]; }
        }
        #endregion

        #region ToolStripGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used in the ToolStrip background.
        /// </summary>
        public override Color ToolStripGradientMiddle
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripMiddle]; }
        }
        #endregion

        #region ToolStripPanelGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the ToolStripPanel.
        /// </summary>
        public override Color ToolStripPanelGradientBegin
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripBack]; }
        }
        #endregion

        #region ToolStripPanelGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStripPanel.
        /// </summary>
        public override Color ToolStripPanelGradientEnd
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripBack]; }
        }
        #endregion
        #endregion

        #region Implementation
        private static void DefineFonts()
        {
            // Release existing resources
            if (_menuToolFont != null) _menuToolFont.Dispose();
            if (_statusFont != null) _statusFont.Dispose();

            // Create new font using system information
            _menuToolFont = new Font("Segoe UI", SystemFonts.MenuFont.SizeInPoints, FontStyle.Regular);
            _statusFont = new Font("Segoe UI", SystemFonts.StatusFont.SizeInPoints, FontStyle.Regular);
        }

        private static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            // Update fonts to reflect any change in system settings
            DefineFonts();
        }
        #endregion
    }
    #endregion
}
