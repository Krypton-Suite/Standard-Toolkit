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
    /// <summary>
    /// Provides a base for Visual Studio 2020 palettes.
    /// </summary>
    /// <seealso cref="PaletteBase" />
    public abstract class PaletteVisualStudioBase : PaletteBase
    {
        #region Static Fields
        private static readonly Padding _contentPaddingGrid = new(2, 1, 2, 1);
        private static readonly Padding _contentPaddingHeader1 = new(2, 1, 2, 1);
        private static readonly Padding _contentPaddingHeader2 = new(2, 1, 2, 1);
        private static readonly Padding _contentPaddingDock = new(2, 2, 2, 1);
        private static readonly Padding _contentPaddingCalendar = new(2);
        private static readonly Padding _contentPaddingHeaderForm = new(10, 6, 3, 0); // 10 is from the RealWindowFrameSize +1
        private static readonly Padding _contentPaddingLabel = new(3, 1, 3, 1);
        private static readonly Padding _contentPaddingLabel2 = new(8, 2, 8, 2);
        private static readonly Padding _contentPaddingButtonInputControl = new(0);
        private static readonly Padding _contentPaddingButton12 = new(1);
        private static readonly Padding _contentPaddingButton3 = new(1, 0, 1, 0);
        private static readonly Padding _contentPaddingButton4 = new(4, 3, 4, 3);
        private static readonly Padding _contentPaddingButton5 = new(3, 3, 3, 2);
        private static readonly Padding _contentPaddingButton6 = new(3);
        private static readonly Padding _contentPaddingButton7 = new(1, 1, 0, 1);
        private static readonly Padding _contentPaddingButtonForm = new(0);
        private static readonly Padding _contentPaddingButtonGallery = new(1, 0, 1, 0);
        private static readonly Padding _contentPaddingButtonListItem = new(0, -1, 0, -1);
        private static readonly Padding _contentPaddingToolTip = new(2);
        private static readonly Padding _contentPaddingSuperTip = new(4);
        private static readonly Padding _contentPaddingKeyTip = new(0, -1, 0, -3);
        private static readonly Padding _contentPaddingContextMenuHeading = new(8, 2, 8, 0);
        private static readonly Padding _contentPaddingContextMenuImage = new(0);
        private static readonly Padding _contentPaddingContextMenuItemText = new(9, 1, 7, 0);
        private static readonly Padding _contentPaddingContextMenuItemTextAlt = new(7, 1, 6, 0);
        private static readonly Padding _contentPaddingContextMenuItemShortcutText = new(3, 1, 4, 0);
        private static readonly Padding _metricPaddingRibbon = new(0, 1, 1, 1);
        private static readonly Padding _metricPaddingRibbonAppButton = new(3, 0, 3, 0);
        private static readonly Padding _metricPaddingHeader = new(0, 3, 1, 3);
        private static readonly Padding _metricPaddingHeaderForm = new(0, 3, 0, -3); // Move the Maximised Form buttons down a bit
        private static readonly Padding _metricPaddingInputControl = new(0, 1, 0, 1);
        private static readonly Padding _metricPaddingBarInside = new(3);
        private static readonly Padding _metricPaddingBarTabs = new(0);
        private static readonly Padding _metricPaddingBarOutside = new(0, 0, 0, 3);
        private static readonly Padding _metricPaddingPageButtons = new(1, 3, 1, 3);
        private static readonly Image _treeExpandWhite = TreeItemImageResources.TreeExpandWhite;
        private static readonly Image _treeCollapseBlack = TreeItemImageResources.TreeCollapseBlack;

        private static readonly Image _disabledDropDown = DropDownArrowImageResources.DisabledDropDownButton;
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
        private static readonly Image _buttonSpecPendantClose = Office2010ControlBoxResources._2010ButtonMDIClose;
        private static readonly Image _buttonSpecPendantMin = Office2010ControlBoxResources._2010ButtonMDIMin;
        private static readonly Image _buttonSpecPendantRestore = Office2010ControlBoxResources._2010ButtonMDIRestore;
        private static readonly Image _buttonSpecWorkspaceMaximize = GenericProfessionalImageResources.ProfessionalMaximize;
        private static readonly Image _buttonSpecWorkspaceRestore = GenericProfessionalImageResources.ProfessionalRestore;
        private static readonly Image _buttonSpecRibbonMinimize = RibbonArrowImageResources.RibbonUp2010;
        private static readonly Image _buttonSpecRibbonExpand = RibbonArrowImageResources.RibbonDown2010;
        private static readonly Image _contextMenuChecked = GenericOffice2007ImageResources.Office2007Checked;
        private static readonly Image _contextMenuIndeterminate = GenericOffice2007ImageResources.Office2007Indeterminate;

        private static readonly Color _gridTextColor = Color.Black;
        private static readonly Color _disabledText2 = Color.FromArgb(128, 128, 128);
        private static readonly Color _disabledText = Color.FromArgb(167, 167, 167);
        private static readonly Color _disabledBack = Color.FromArgb(235, 235, 235);
        private static readonly Color _disabledBorder = Color.FromArgb(212, 212, 212);
        private static readonly Color _disabledGlyphDark = Color.FromArgb(183, 183, 183);
        private static readonly Color _disabledGlyphLight = Color.FromArgb(237, 237, 237);
        private static readonly Color _contextCheckedTabBorder1 = Color.FromArgb(223, 119, 0);
        private static readonly Color _contextCheckedTabBorder2 = Color.FromArgb(230, 190, 129);
        private static readonly Color _contextCheckedTabBorder3 = Color.FromArgb(220, 202, 171);
        private static readonly Color _contextCheckedTabBorder4 = Color.FromArgb(255, 252, 247);
        private static readonly Color _contextTabSeparator = Color.White;
        private static readonly Color _contextTextColor = Color.White;
        private static readonly Color _todayBorder = Color.FromArgb(187, 85, 3);
        private static readonly Color _toolTipBack1 = Color.FromArgb(255, 255, 255);
        private static readonly Color _toolTipBack2 = Color.FromArgb(201, 217, 239);
        private static readonly Color _toolTipBorder = Color.FromArgb(118, 118, 118);
        private static readonly Color _toolTipText = Color.FromArgb(76, 76, 76);
        private static readonly Color _contextMenuBack = Color.White;
        private static readonly Color _contextMenuBorder = Color.FromArgb(134, 134, 134);
        private static readonly Color _contextMenuHeadingBorder = Color.FromArgb(197, 197, 197);
        private static readonly Color _contextMenuImageBackChecked = Color.FromArgb(252, 241, 194);
        private static readonly Color _contextMenuImageBorderChecked = Color.FromArgb(242, 149, 54);
        private static readonly Color _formCloseBorderTracking = Color.FromArgb(155, 61, 61);
        private static readonly Color _formCloseBorderPressed = Color.FromArgb(155, 61, 61);
        private static readonly Color _formCloseBorderCheckedNormal = Color.FromArgb(155, 61, 61);
        private static readonly Color _formCloseTracking1 = Color.FromArgb(255, 132, 130);
        private static readonly Color _formCloseTracking2 = Color.FromArgb(227, 97, 98);
        private static readonly Color _formClosePressed1 = Color.FromArgb(242, 119, 118);
        private static readonly Color _formClosePressed2 = Color.FromArgb(206, 85, 84);
        private static readonly Color _formCloseChecked1 = Color.FromArgb(255, 132, 130);
        private static readonly Color _formCloseChecked2 = Color.FromArgb(255, 132, 130);
        private static readonly Color _formCloseCheckedTracking1 = Color.FromArgb(255, 132, 130);
        private static readonly Color _formCloseCheckedTracking2 = Color.FromArgb(255, 132, 130);
        private static readonly Color[] _appButtonNormal = new Color[] { Color.FromArgb(243, 245, 248), Color.FromArgb(214, 220, 231), Color.FromArgb(188, 198, 211), Color.FromArgb(254, 254, 255), Color.FromArgb(206, 213, 225) };
        private static readonly Color[] _appButtonTrack = new Color[] { Color.FromArgb(255, 251, 230), Color.FromArgb(248, 230, 143), Color.FromArgb(238, 213, 126), Color.FromArgb(254, 247, 129), Color.FromArgb(240, 201, 41) };
        private static readonly Color[] _appButtonPressed = new Color[] { Color.FromArgb(235, 227, 196), Color.FromArgb(228, 198, 149), Color.FromArgb(166, 97, 7), Color.FromArgb(242, 155, 57), Color.FromArgb(236, 136, 9) };
        private static readonly Color[] _buttonBorderColors = new Color[]{ Color.FromArgb(180, 180, 180), // Button, Disabled, Border
                                                                           Color.FromArgb(205, 230, 247),  // Button, Tracking, Border 1
                                                                           Color.FromArgb(205, 230, 247),  // Button, Tracking, Border 2
                                                                           Color.FromArgb(146, 192, 244),  // Button, Pressed, Border 1
                                                                           Color.FromArgb(146, 192, 244),  // Button, Pressed, Border 2
                                                                           Color.FromArgb(146, 192, 244),  // Button, Checked, Border 1
                                                                           Color.FromArgb(146, 192, 244)   // Button, Checked, Border 2
                                                                         };
        private static readonly Color[] _buttonBackColors = new Color[]{ Color.FromArgb(250, 250, 250), // Button, Disabled, Back 1
                                                                         Color.FromArgb(250, 250, 250), // Button, Disabled, Back 2
                                                                          Color.FromArgb(205, 230, 247), // Button, Tracking, Back 1
                                                                         Color.FromArgb(205, 230, 247), // Button, Tracking, Back 2
                                                                         Color.FromArgb(146, 192, 244), // Button, Pressed, Back 1
                                                                         Color.FromArgb(146, 192, 244), // Button, Pressed, Back 2
                                                                         Color.FromArgb(146, 192, 244), // Button, Checked, Back 1
                                                                         Color.FromArgb(146, 192, 244), // Button, Checked, Back 2
                                                                         Color.FromArgb(255, 225, 104), // Button, Checked Tracking, Back 1
                                                                         Color.FromArgb(255, 249, 196)  // Button, Checked Tracking, Back 2
                                                                       };
        #endregion
    }
}