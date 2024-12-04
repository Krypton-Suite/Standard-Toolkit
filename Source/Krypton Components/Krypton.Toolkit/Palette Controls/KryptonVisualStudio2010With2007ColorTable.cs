﻿#region BSD License
/*
 *   BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Provide KryptonVisualStudio2010With2007ColorTable values using an array of Color values as the source.
    /// </summary>
    public class KryptonVisualStudio2010With2007ColorTable : KryptonColorTable
    {
        #region Static Fields
        private static readonly Color _menuBorder = Color.FromArgb(134, 134, 134);
        private static readonly Color _menuItemSelectedBegin = Color.FromArgb(255, 246, 217);
        private static readonly Color _menuItemSelectedEnd = Color.FromArgb(255, 236, 181);
        private static readonly Color _contextMenuBackground = Color.FromArgb(211, 217, 227);
        private static readonly Color _checkBackground = Color.FromArgb(255, 227, 149);
        private static readonly Color _buttonSelectedBegin = Color.FromArgb(255, 235, 166);
        private static readonly Color _buttonSelectedEnd = Color.FromArgb(255, 213, 103);
        private static readonly Color _buttonPressedBegin = Color.FromArgb(253, 164, 97);
        private static readonly Color _buttonPressedEnd = Color.FromArgb(252, 143, 61);
        private static readonly Color _buttonCheckedBegin = Color.FromArgb(252, 180, 100);
        private static readonly Color _buttonCheckedEnd = Color.FromArgb(252, 161, 54);
        private static readonly Color _menuStripTextColor = Color.FromArgb(0, 0, 0);
        private static Font _menuToolFont;
        private static Font _statusFont;
        #endregion

        #region Identity
        static KryptonVisualStudio2010With2007ColorTable()
        {
            // Get the font settings from the system
            DefineFonts();

            // We need to notice when system color settings change
            SystemEvents.UserPreferenceChanged += OnUserPreferenceChanged;
        }

        /// <summary>
        /// Initialize a new instance of the KryptonVisualStudio2010With2007ColorTable class.
        /// </summary>
        /// <param name="colors">Source of </param>
        /// <param name="roundedEdges">Should have rounded edges.</param>
        /// <param name="palette">Associated palette instance.</param>
        public KryptonVisualStudio2010With2007ColorTable([DisallowNull] Color[] colors,
                                     InheritBool roundedEdges,
                                     PaletteBase palette)
            : base(palette)
        {
            Debug.Assert(colors != null);
            Colors = colors!;
            UseRoundedEdges = roundedEdges;
        }
        #endregion

        #region Colors
        /// <summary>
        /// Gets the raw set of colors.
        /// </summary>
        public Color[] Colors { get; }

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
        public override Color ButtonPressedBorder => Colors[(int)SchemeOfficeColors.ButtonBorder];

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
        public override Color ButtonPressedHighlightBorder => Colors[(int)SchemeOfficeColors.ButtonBorder];

        #endregion
        #endregion

        #region ButtonSelected
        #region ButtonSelectedBorder
        /// <summary>
        /// Gets the border color for a button being selected.
        /// </summary>
        public override Color ButtonSelectedBorder => Colors[(int)SchemeOfficeColors.ButtonBorder];

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
        public override Color ButtonSelectedHighlightBorder => Colors[(int)SchemeOfficeColors.ButtonBorder];

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
        public override Color ButtonCheckedHighlightBorder => Colors[(int)SchemeOfficeColors.ButtonBorder];

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
        public override Color GripLight => Colors[(int)SchemeOfficeColors.GripLight];

        #endregion

        #region GripDark
        /// <summary>
        /// Gets the dark color used to draw grips.
        /// </summary>
        public override Color GripDark => Colors[(int)SchemeOfficeColors.GripDark];

        #endregion
        #endregion

        #region ImageMargin
        #region ImageMarginGradientBegin
        /// <summary>
        /// Gets the starting color for the context menu margin.
        /// </summary>
        public override Color ImageMarginGradientBegin => Colors[(int)SchemeOfficeColors.ImageMargin];

        #endregion

        #region ImageMarginGradientMiddle
        /// <summary>
        /// Gets the middle color for the context menu margin.
        /// </summary>
        public override Color ImageMarginGradientMiddle => Colors[(int)SchemeOfficeColors.ImageMargin];

        #endregion

        #region ImageMarginGradientEnd
        /// <summary>
        /// Gets the ending color for the context menu margin.
        /// </summary>
        public override Color ImageMarginGradientEnd => Colors[(int)SchemeOfficeColors.ImageMargin];

        #endregion

        #region ImageMarginRevealedGradientBegin
        /// <summary>
        /// Gets the starting color for the context menu margin revealed.
        /// </summary>
        public override Color ImageMarginRevealedGradientBegin => Colors[(int)SchemeOfficeColors.ImageMargin];

        #endregion

        #region ImageMarginRevealedGradientMiddle
        /// <summary>
        /// Gets the middle color for the context menu margin revealed.
        /// </summary>
        public override Color ImageMarginRevealedGradientMiddle => Colors[(int)SchemeOfficeColors.ImageMargin];

        #endregion

        #region ImageMarginRevealedGradientEnd
        /// <summary>
        /// Gets the ending color for the context menu margin revealed.
        /// </summary>
        public override Color ImageMarginRevealedGradientEnd => Colors[(int)SchemeOfficeColors.ImageMargin];

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
        public override Color MenuItemSelected => Colors[(int)SchemeOfficeColors.ButtonBorder];

        #endregion

        #region MenuItemPressedGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used when a top-level ToolStripMenuItem is pressed down.
        /// </summary>
        public override Color MenuItemPressedGradientBegin => Colors[(int)SchemeOfficeColors.ToolStripBegin];

        #endregion

        #region MenuItemPressedGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used when a top-level ToolStripMenuItem is pressed down.
        /// </summary>
        public override Color MenuItemPressedGradientEnd => Colors[(int)SchemeOfficeColors.ToolStripEnd];

        #endregion

        #region MenuItemPressedGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used when a top-level ToolStripMenuItem is pressed down.
        /// </summary>
        public override Color MenuItemPressedGradientMiddle => Colors[(int)SchemeOfficeColors.ToolStripMiddle];

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
        public override Color MenuStripGradientBegin => Colors[(int)SchemeOfficeColors.ToolStripBack];

        #endregion

        #region MenuStripGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the MenuStrip.
        /// </summary>
        public override Color MenuStripGradientEnd => Colors[(int)SchemeOfficeColors.ToolStripBack];

        #endregion

        #endregion

        #region OverflowButton
        #region OverflowButtonGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the ToolStripOverflowButton.
        /// </summary>
        public override Color OverflowButtonGradientBegin => Colors[(int)SchemeOfficeColors.OverflowBegin];

        #endregion

        #region OverflowButtonGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStripOverflowButton.
        /// </summary>
        public override Color OverflowButtonGradientEnd => Colors[(int)SchemeOfficeColors.OverflowEnd];

        #endregion

        #region OverflowButtonGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used in the ToolStripOverflowButton.
        /// </summary>
        public override Color OverflowButtonGradientMiddle => Colors[(int)SchemeOfficeColors.OverflowMiddle];

        #endregion
        #endregion

        #region RaftingContainer
        #region RaftingContainerGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the ToolStripContainer.
        /// </summary>
        public override Color RaftingContainerGradientBegin => Colors[(int)SchemeOfficeColors.ToolStripBack];

        #endregion

        #region RaftingContainerGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStripContainer.
        /// </summary>
        public override Color RaftingContainerGradientEnd => Colors[(int)SchemeOfficeColors.ToolStripBack];

        #endregion

        #endregion

        #region Separator
        #region SeparatorLight
        /// <summary>
        /// Gets the light separator color.
        /// </summary>
        public override Color SeparatorLight => Colors[(int)SchemeOfficeColors.SeparatorLight];

        #endregion

        #region SeparatorDark
        /// <summary>
        /// Gets the dark separator color.
        /// </summary>
        public override Color SeparatorDark => Colors[(int)SchemeOfficeColors.SeparatorDark];

        #endregion
        #endregion

        #region StatusStrip
        #region StatusStripGradientBegin
        /// <summary>
        /// Gets the starting color for the status strip background.
        /// </summary>
        public override Color StatusStripGradientBegin => Colors[(int)SchemeOfficeColors.StatusStripLight];

        #endregion

        #region StatusStripGradientEnd
        /// <summary>
        /// Gets the ending color for the status strip background.
        /// </summary>
        public override Color StatusStripGradientEnd => Colors[(int)SchemeOfficeColors.StatusStripDark];

        #endregion
        #endregion

        #region Text
        #region MenuItemText
        /// <summary>
        /// Gets the text color used on the menu items.
        /// </summary>
        public override Color MenuItemText => Colors[(int)SchemeOfficeColors.TextButtonNormal];

        #endregion

        #region MenuStripText

        /// <summary>
        /// Gets the text color used on the menu strip.
        /// </summary>
        public override Color MenuStripText => _menuStripTextColor; // Colors[(int)SchemeOfficeColors.TextLabelPanel];

        #endregion

        #region ToolStripText
        /// <summary>
        /// Gets the text color used on the tool strip.
        /// </summary>
        public override Color ToolStripText => Colors[(int)SchemeOfficeColors.TextButtonNormal];

        #endregion

        #region StatusStripText
        /// <summary>
        /// Gets the text color used on the status strip.
        /// </summary>
        public override Color StatusStripText => Colors[(int)SchemeOfficeColors.StatusStripText];

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
        public override Color ToolStripBorder => Colors[(int)SchemeOfficeColors.ToolStripBorder];

        #endregion

        #region ToolStripContentPanelGradientBegin
        /// <summary>
        /// Gets the starting color for the content panel background.
        /// </summary>
        public override Color ToolStripContentPanelGradientBegin => Colors[(int)SchemeOfficeColors.ToolStripBack];

        #endregion

        #region ToolStripContentPanelGradientEnd
        /// <summary>
        /// Gets the ending color for the content panel background.
        /// </summary>
        public override Color ToolStripContentPanelGradientEnd => Colors[(int)SchemeOfficeColors.ToolStripBack];

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
        public override Color ToolStripGradientBegin => Colors[(int)SchemeOfficeColors.ToolStripBegin];

        #endregion

        #region ToolStripGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStrip background.
        /// </summary>
        public override Color ToolStripGradientEnd => Colors[(int)SchemeOfficeColors.ToolStripEnd];

        #endregion

        #region ToolStripGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used in the ToolStrip background.
        /// </summary>
        public override Color ToolStripGradientMiddle => Colors[(int)SchemeOfficeColors.ToolStripMiddle];

        #endregion

        #region ToolStripPanelGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the ToolStripPanel.
        /// </summary>
        public override Color ToolStripPanelGradientBegin => Colors[(int)SchemeOfficeColors.ToolStripBack];

        #endregion

        #region ToolStripPanelGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStripPanel.
        /// </summary>
        public override Color ToolStripPanelGradientEnd => Colors[(int)SchemeOfficeColors.ToolStripBack];

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
}