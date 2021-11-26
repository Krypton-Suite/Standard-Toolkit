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
    internal class KryptonProfessionalCustomKCT : KryptonProfessionalKCT
    {
        #region Instance Fields
        private readonly Color[] _colors;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonProfessionalCustomKCT class.
        /// </summary>
        /// <param name="headerColors">Set of header colors to customize with.</param>
        /// <param name="colorTableColors">Set of ColorTable colors to customize with.</param>
        /// <param name="useSystemColors">Should be forced to use system colors.</param>
        /// <param name="palette">Associated palette instance.</param>
        public KryptonProfessionalCustomKCT(Color[] headerColors,
                                            Color[] colorTableColors,
                                            bool useSystemColors,
                                            IPalette palette)
            : base(headerColors, useSystemColors, palette) =>
            _colors = colorTableColors;

        #endregion

        #region Button
        #region ButtonCheckedGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used when the button is checked.
        /// </summary>
        public override Color ButtonCheckedGradientBegin => _colors[(int) PaletteColorIndex.ButtonCheckedGradientBegin] == Color.Empty
            ? base.ButtonCheckedGradientBegin
            : _colors[(int) PaletteColorIndex.ButtonCheckedGradientBegin];

        #endregion

        #region ButtonCheckedGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used when the button is checked.
        /// </summary>
        public override Color ButtonCheckedGradientEnd =>
            _colors[(int)PaletteColorIndex.ButtonCheckedGradientEnd] == Color.Empty
                ? base.ButtonCheckedGradientEnd
                : _colors[(int)PaletteColorIndex.ButtonCheckedGradientEnd];

        #endregion

        #region ButtonCheckedGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used when the button is checked.
        /// </summary>
        public override Color ButtonCheckedGradientMiddle =>
            _colors[(int)PaletteColorIndex.ButtonCheckedGradientMiddle] == Color.Empty
                ? base.ButtonCheckedGradientMiddle
                : _colors[(int)PaletteColorIndex.ButtonCheckedGradientMiddle];

        #endregion

        #region ButtonCheckedHighlight
        /// <summary>
        /// Gets the solid color used when the button is checked.
        /// </summary>
        public override Color ButtonCheckedHighlight =>
            _colors[(int)PaletteColorIndex.ButtonCheckedHighlight] == Color.Empty
                ? base.ButtonCheckedHighlight
                : _colors[(int)PaletteColorIndex.ButtonCheckedHighlight];

        #endregion

        #region ButtonCheckedHighlightBorder
        /// <summary>
        /// Gets the border color to use with ButtonCheckedHighlight.
        /// </summary>
        public override Color ButtonCheckedHighlightBorder =>
            _colors[(int)PaletteColorIndex.ButtonCheckedHighlightBorder] == Color.Empty
                ? base.ButtonCheckedHighlightBorder
                : _colors[(int)PaletteColorIndex.ButtonCheckedHighlightBorder];

        #endregion

        #region ButtonPressedBorder
        /// <summary>
        /// Gets the border color to use with the ButtonPressedGradientBegin, ButtonPressedGradientMiddle, and ButtonPressedGradientEnd colors.
        /// </summary>
        public override Color ButtonPressedBorder =>
            _colors[(int)PaletteColorIndex.ButtonPressedBorder] == Color.Empty
                ? base.ButtonPressedBorder
                : _colors[(int)PaletteColorIndex.ButtonPressedBorder];

        #endregion

        #region ButtonPressedGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used when the button is pressed.
        /// </summary>
        public override Color ButtonPressedGradientBegin =>
            _colors[(int)PaletteColorIndex.ButtonPressedGradientBegin] == Color.Empty
                ? base.ButtonPressedGradientBegin
                : _colors[(int)PaletteColorIndex.ButtonPressedGradientBegin];

        #endregion

        #region ButtonPressedGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used when the button is pressed.
        /// </summary>
        public override Color ButtonPressedGradientEnd =>
            _colors[(int)PaletteColorIndex.ButtonPressedGradientEnd] == Color.Empty
                ? base.ButtonPressedGradientEnd
                : _colors[(int)PaletteColorIndex.ButtonPressedGradientEnd];

        #endregion

        #region ButtonPressedGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used when the button is pressed.
        /// </summary>
        public override Color ButtonPressedGradientMiddle =>
            _colors[(int)PaletteColorIndex.ButtonPressedGradientMiddle] == Color.Empty
                ? base.ButtonPressedGradientMiddle
                : _colors[(int)PaletteColorIndex.ButtonPressedGradientMiddle];

        #endregion

        #region ButtonPressedHighlight
        /// <summary>
        /// Gets the solid color used when the button is pressed.
        /// </summary>
        public override Color ButtonPressedHighlight =>
            _colors[(int)PaletteColorIndex.ButtonPressedHighlight] == Color.Empty
                ? base.ButtonPressedHighlight
                : _colors[(int)PaletteColorIndex.ButtonPressedHighlight];

        #endregion

        #region ButtonPressedHighlightBorder
        /// <summary>
        /// Gets the border color to use with ButtonPressedHighlight.
        /// </summary>
        public override Color ButtonPressedHighlightBorder =>
            _colors[(int)PaletteColorIndex.ButtonPressedHighlightBorder] == Color.Empty
                ? base.ButtonPressedHighlightBorder
                : _colors[(int)PaletteColorIndex.ButtonPressedHighlightBorder];

        #endregion

        #region ButtonSelectedBorder
        /// <summary>
        /// Gets the border color to use with the ButtonSelectedGradientBegin, ButtonSelectedGradientMiddle, and ButtonSelectedGradientEnd colors.
        /// </summary>
        public override Color ButtonSelectedBorder =>
            _colors[(int)PaletteColorIndex.ButtonSelectedBorder] == Color.Empty
                ? base.ButtonSelectedBorder
                : _colors[(int)PaletteColorIndex.ButtonSelectedBorder];

        #endregion

        #region ButtonSelectedGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used when the button is selected.
        /// </summary>
        public override Color ButtonSelectedGradientBegin =>
            _colors[(int)PaletteColorIndex.ButtonSelectedGradientBegin] == Color.Empty
                ? base.ButtonSelectedGradientBegin
                : _colors[(int)PaletteColorIndex.ButtonSelectedGradientBegin];

        #endregion

        #region ButtonSelectedGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used when the button is selected.
        /// </summary>
        public override Color ButtonSelectedGradientEnd =>
            _colors[(int)PaletteColorIndex.ButtonSelectedGradientEnd] == Color.Empty
                ? base.ButtonSelectedGradientEnd
                : _colors[(int)PaletteColorIndex.ButtonSelectedGradientEnd];

        #endregion

        #region ButtonSelectedGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used when the button is selected.
        /// </summary>
        public override Color ButtonSelectedGradientMiddle =>
            _colors[(int)PaletteColorIndex.ButtonSelectedGradientMiddle] == Color.Empty
                ? base.ButtonSelectedGradientMiddle
                : _colors[(int)PaletteColorIndex.ButtonSelectedGradientMiddle];

        #endregion

        #region ButtonSelectedHighlight
        /// <summary>
        /// Gets the solid color used when the button is selected.
        /// </summary>
        public override Color ButtonSelectedHighlight =>
            _colors[(int)PaletteColorIndex.ButtonSelectedHighlight] == Color.Empty
                ? base.ButtonSelectedHighlight
                : _colors[(int)PaletteColorIndex.ButtonSelectedHighlight];

        #endregion

        #region ButtonSelectedHighlightBorder
        /// <summary>
        /// Gets the border color to use with ButtonSelectedHighlight.
        /// </summary>
        public override Color ButtonSelectedHighlightBorder =>
            _colors[(int)PaletteColorIndex.ButtonSelectedHighlightBorder] == Color.Empty
                ? base.ButtonSelectedHighlightBorder
                : _colors[(int)PaletteColorIndex.ButtonSelectedHighlightBorder];

        #endregion
        #endregion

        #region Check
        #region CheckBackground
        /// <summary>
        /// Gets the solid color to use when the button is checked and gradients are being used.
        /// </summary>
        public override Color CheckBackground =>
            _colors[(int)PaletteColorIndex.CheckBackground] == Color.Empty
                ? base.CheckBackground
                : _colors[(int)PaletteColorIndex.CheckBackground];

        #endregion

        #region CheckPressedBackground
        /// <summary>
        /// Gets the solid color to use when the button is checked and selected and gradients are being used.
        /// </summary>
        public override Color CheckPressedBackground =>
            _colors[(int)PaletteColorIndex.CheckPressedBackground] == Color.Empty
                ? base.CheckPressedBackground
                : _colors[(int)PaletteColorIndex.CheckPressedBackground];

        #endregion

        #region CheckSelectedBackground
        /// <summary>
        /// Gets the solid color to use when the button is checked and selected and gradients are being used.
        /// </summary>
        public override Color CheckSelectedBackground =>
            _colors[(int)PaletteColorIndex.CheckSelectedBackground] == Color.Empty
                ? base.CheckSelectedBackground
                : _colors[(int)PaletteColorIndex.CheckSelectedBackground];

        #endregion
        #endregion

        #region Grip
        #region GripDark
        /// <summary>
        /// Gets the color to use for shadow effects on the grip (move handle).
        /// </summary>
        public override Color GripDark => _colors[(int)PaletteColorIndex.GripDark] == Color.Empty ? base.GripDark : _colors[(int)PaletteColorIndex.GripDark];

        #endregion

        #region GripLight
        /// <summary>
        /// Gets the color to use for highlight effects on the grip (move handle).
        /// </summary>
        public override Color GripLight => _colors[(int)PaletteColorIndex.GripLight] == Color.Empty ? base.GripLight : _colors[(int)PaletteColorIndex.GripLight];

        #endregion
        #endregion

        #region ImageMargin
        #region ImageMarginGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the image margin of a ToolStripDropDownMenu.
        /// </summary>
        public override Color ImageMarginGradientBegin =>
            _colors[(int)PaletteColorIndex.ImageMarginGradientBegin] == Color.Empty
                ? base.ImageMarginGradientBegin
                : _colors[(int)PaletteColorIndex.ImageMarginGradientBegin];

        #endregion

        #region ImageMarginGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the image margin of a ToolStripDropDownMenu.
        /// </summary>
        public override Color ImageMarginGradientEnd =>
            _colors[(int)PaletteColorIndex.ImageMarginGradientEnd] == Color.Empty
                ? base.ImageMarginGradientEnd
                : _colors[(int)PaletteColorIndex.ImageMarginGradientEnd];

        #endregion

        #region ImageMarginGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used in the image margin of a ToolStripDropDownMenu.
        /// </summary>
        public override Color ImageMarginGradientMiddle =>
            _colors[(int)PaletteColorIndex.ImageMarginGradientMiddle] == Color.Empty
                ? base.ImageMarginGradientMiddle
                : _colors[(int)PaletteColorIndex.ImageMarginGradientMiddle];

        #endregion

        #region ImageMarginRevealedGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.
        /// </summary>
        public override Color ImageMarginRevealedGradientBegin =>
            _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientBegin] == Color.Empty
                ? base.ImageMarginRevealedGradientBegin
                : _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientBegin];

        #endregion

        #region ImageMarginRevealedGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.
        /// </summary>
        public override Color ImageMarginRevealedGradientEnd =>
            _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientEnd] == Color.Empty
                ? base.ImageMarginRevealedGradientEnd
                : _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientEnd];

        #endregion

        #region ImageMarginRevealedGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.
        /// </summary>
        public override Color ImageMarginRevealedGradientMiddle =>
            _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientMiddle] == Color.Empty
                ? base.ImageMarginRevealedGradientMiddle
                : _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientMiddle];

        #endregion
        #endregion

        #region Menu
        #region MenuBorder
        /// <summary>
        /// Gets the color that is the border color to use on a MenuStrip.
        /// </summary>
        public override Color MenuBorder => _colors[(int)PaletteColorIndex.MenuBorder] == Color.Empty ? base.MenuBorder : _colors[(int)PaletteColorIndex.MenuBorder];

        #endregion

        #region MenuItemText
        /// <summary>
        /// Gets the color used to draw menu item text.
        /// </summary>
        public override Color MenuItemText => _colors[(int)PaletteColorIndex.MenuItemText] == Color.Empty ? base.MenuItemText : _colors[(int)PaletteColorIndex.MenuItemText];

        #endregion

        #region MenuItemBorder
        /// <summary>
        /// Gets the border color to use with a ToolStripMenuItem.
        /// </summary>
        public override Color MenuItemBorder =>
            _colors[(int)PaletteColorIndex.MenuItemBorder] == Color.Empty
                ? base.MenuItemBorder
                : _colors[(int)PaletteColorIndex.MenuItemBorder];

        #endregion

        #region MenuItemPressedGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used when a top-level ToolStripMenuItem is pressed.
        /// </summary>
        public override Color MenuItemPressedGradientBegin =>
            _colors[(int)PaletteColorIndex.MenuItemPressedGradientBegin] == Color.Empty
                ? base.MenuItemPressedGradientBegin
                : _colors[(int)PaletteColorIndex.MenuItemPressedGradientBegin];

        #endregion

        #region MenuItemPressedGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used when a top-level ToolStripMenuItem is pressed.
        /// </summary>
        public override Color MenuItemPressedGradientEnd =>
            _colors[(int)PaletteColorIndex.MenuItemPressedGradientEnd] == Color.Empty
                ? base.MenuItemPressedGradientEnd
                : _colors[(int)PaletteColorIndex.MenuItemPressedGradientEnd];

        #endregion

        #region MenuItemPressedGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used when a top-level ToolStripMenuItem is pressed.
        /// </summary>
        public override Color MenuItemPressedGradientMiddle =>
            _colors[(int)PaletteColorIndex.MenuItemPressedGradientMiddle] == Color.Empty
                ? base.MenuItemPressedGradientMiddle
                : _colors[(int)PaletteColorIndex.MenuItemPressedGradientMiddle];

        #endregion

        #region MenuItemSelected
        /// <summary>
        /// Gets the solid color to use when a ToolStripMenuItem other than the top-level ToolStripMenuItem is selected.
        /// </summary>
        public override Color MenuItemSelected =>
            _colors[(int)PaletteColorIndex.MenuItemSelected] == Color.Empty
                ? base.MenuItemSelected
                : _colors[(int)PaletteColorIndex.MenuItemSelected];

        #endregion

        #region MenuItemSelectedGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used when the ToolStripMenuItem is selected.
        /// </summary>
        public override Color MenuItemSelectedGradientBegin =>
            _colors[(int)PaletteColorIndex.MenuItemSelectedGradientBegin] == Color.Empty
                ? base.MenuItemSelectedGradientBegin
                : _colors[(int)PaletteColorIndex.MenuItemSelectedGradientBegin];

        #endregion

        #region MenuItemSelectedGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used when the ToolStripMenuItem is selected.
        /// </summary>
        public override Color MenuItemSelectedGradientEnd =>
            _colors[(int)PaletteColorIndex.MenuItemSelectedGradientEnd] == Color.Empty
                ? base.MenuItemSelectedGradientEnd
                : _colors[(int)PaletteColorIndex.MenuItemSelectedGradientEnd];

        #endregion

        #region MenuStripText
        /// <summary>
        /// Gets the color used to draw text on a menu strip.
        /// </summary>
        public override Color MenuStripText =>
            _colors[(int)PaletteColorIndex.MenuStripText] == Color.Empty
                ? base.MenuStripText
                : _colors[(int)PaletteColorIndex.MenuStripText];

        #endregion

        #region MenuStripGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the MenuStrip.
        /// </summary>
        public override Color MenuStripGradientBegin =>
            _colors[(int)PaletteColorIndex.MenuStripGradientBegin] == Color.Empty
                ? base.MenuStripGradientBegin
                : _colors[(int)PaletteColorIndex.MenuStripGradientBegin];

        #endregion

        #region MenuStripGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the MenuStrip.
        /// </summary>
        public override Color MenuStripGradientEnd =>
            _colors[(int)PaletteColorIndex.MenuStripGradientEnd] == Color.Empty
                ? base.MenuStripGradientEnd
                : _colors[(int)PaletteColorIndex.MenuStripGradientEnd];

        #endregion
        #endregion

        #region OverflowButton
        #region OverflowButtonGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the ToolStripOverflowButton.
        /// </summary>
        public override Color OverflowButtonGradientBegin =>
            _colors[(int)PaletteColorIndex.OverflowButtonGradientBegin] == Color.Empty
                ? base.OverflowButtonGradientBegin
                : _colors[(int)PaletteColorIndex.OverflowButtonGradientBegin];

        #endregion

        #region OverflowButtonGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStripOverflowButton.
        /// </summary>
        public override Color OverflowButtonGradientEnd =>
            _colors[(int)PaletteColorIndex.OverflowButtonGradientEnd] == Color.Empty
                ? base.OverflowButtonGradientEnd
                : _colors[(int)PaletteColorIndex.OverflowButtonGradientEnd];

        #endregion

        #region OverflowButtonGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used in the ToolStripOverflowButton.
        /// </summary>
        public override Color OverflowButtonGradientMiddle =>
            _colors[(int)PaletteColorIndex.OverflowButtonGradientMiddle] == Color.Empty
                ? base.OverflowButtonGradientMiddle
                : _colors[(int)PaletteColorIndex.OverflowButtonGradientMiddle];

        #endregion
        #endregion

        #region RaftingContainer
        #region RaftingContainerGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the ToolStripContainer.
        /// </summary>
        public override Color RaftingContainerGradientBegin =>
            _colors[(int)PaletteColorIndex.RaftingContainerGradientBegin] == Color.Empty
                ? base.RaftingContainerGradientBegin
                : _colors[(int)PaletteColorIndex.RaftingContainerGradientBegin];

        #endregion

        #region RaftingContainerGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStripContainer.
        /// </summary>
        public override Color RaftingContainerGradientEnd =>
            _colors[(int)PaletteColorIndex.RaftingContainerGradientEnd] == Color.Empty
                ? base.RaftingContainerGradientEnd
                : _colors[(int)PaletteColorIndex.RaftingContainerGradientEnd];

        #endregion
        #endregion

        #region Separator
        #region SeparatorDark
        /// <summary>
        /// Gets the color to use to for shadow effects on the ToolStripSeparator.
        /// </summary>
        public override Color SeparatorDark =>
            _colors[(int)PaletteColorIndex.SeparatorDark] == Color.Empty
                ? base.SeparatorDark
                : _colors[(int)PaletteColorIndex.SeparatorDark];

        #endregion

        #region SeparatorLight
        /// <summary>
        /// Gets the color to use to for highlight effects on the ToolStripSeparator.
        /// </summary>
        public override Color SeparatorLight =>
            _colors[(int)PaletteColorIndex.SeparatorLight] == Color.Empty
                ? base.SeparatorLight
                : _colors[(int)PaletteColorIndex.SeparatorLight];

        #endregion
        #endregion

        #region StatusStrip
        #region StatusStripText
        /// <summary>
        /// Gets the color used to draw text on a status strip.
        /// </summary>
        public override Color StatusStripText =>
            _colors[(int)PaletteColorIndex.StatusStripText] == Color.Empty
                ? base.StatusStripText
                : _colors[(int)PaletteColorIndex.StatusStripText];

        #endregion

        #region StatusStripGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used on the StatusStrip.
        /// </summary>
        public override Color StatusStripGradientBegin =>
            _colors[(int)PaletteColorIndex.StatusStripGradientBegin] == Color.Empty
                ? base.StatusStripGradientBegin
                : _colors[(int)PaletteColorIndex.StatusStripGradientBegin];

        #endregion

        #region StatusStripGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used on the StatusStrip.
        /// </summary>
        public override Color StatusStripGradientEnd =>
            _colors[(int)PaletteColorIndex.StatusStripGradientEnd] == Color.Empty
                ? base.StatusStripGradientEnd
                : _colors[(int)PaletteColorIndex.StatusStripGradientEnd];

        #endregion
        #endregion

        #region ToolStrip
        #region ToolStripText
        /// <summary>
        /// Gets the color used to draw text on a tool strip.
        /// </summary>
        public override Color ToolStripText =>
            _colors[(int)PaletteColorIndex.ToolStripText] == Color.Empty
                ? base.ToolStripText
                : _colors[(int)PaletteColorIndex.ToolStripText];

        #endregion

        #region ToolStripBorder
        /// <summary>
        /// Gets the border color to use on the bottom edge of the ToolStrip.
        /// </summary>
        public override Color ToolStripBorder =>
            _colors[(int)PaletteColorIndex.ToolStripBorder] == Color.Empty
                ? base.ToolStripBorder
                : _colors[(int)PaletteColorIndex.ToolStripBorder];

        #endregion

        #region ToolStripContentPanelGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the ToolStripContentPanel.
        /// </summary>
        public override Color ToolStripContentPanelGradientBegin =>
            _colors[(int)PaletteColorIndex.ToolStripContentPanelGradientBegin] == Color.Empty
                ? base.ToolStripContentPanelGradientBegin
                : _colors[(int)PaletteColorIndex.ToolStripContentPanelGradientBegin];

        #endregion

        #region ToolStripContentPanelGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStripContentPanel.
        /// </summary>
        public override Color ToolStripContentPanelGradientEnd =>
            _colors[(int)PaletteColorIndex.ToolStripContentPanelGradientEnd] == Color.Empty
                ? base.ToolStripContentPanelGradientEnd
                : _colors[(int)PaletteColorIndex.ToolStripContentPanelGradientEnd];

        #endregion

        #region ToolStripDropDownBackground
        /// <summary>
        /// Gets the solid background color of the ToolStripDropDown.
        /// </summary>
        public override Color ToolStripDropDownBackground =>
            _colors[(int)PaletteColorIndex.ToolStripDropDownBackground] == Color.Empty
                ? base.ToolStripDropDownBackground
                : _colors[(int)PaletteColorIndex.ToolStripDropDownBackground];

        #endregion

        #region ToolStripGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the ToolStrip background.
        /// </summary>
        public override Color ToolStripGradientBegin =>
            _colors[(int)PaletteColorIndex.ToolStripGradientBegin] == Color.Empty
                ? base.ToolStripGradientBegin
                : _colors[(int)PaletteColorIndex.ToolStripGradientBegin];

        #endregion

        #region ToolStripGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStrip background.
        /// </summary>
        public override Color ToolStripGradientEnd =>
            _colors[(int)PaletteColorIndex.ToolStripGradientEnd] == Color.Empty
                ? base.ToolStripGradientEnd
                : _colors[(int)PaletteColorIndex.ToolStripGradientEnd];

        #endregion

        #region ToolStripGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used in the ToolStrip background.
        /// </summary>
        public override Color ToolStripGradientMiddle =>
            _colors[(int)PaletteColorIndex.ToolStripGradientMiddle] == Color.Empty
                ? base.ToolStripGradientMiddle
                : _colors[(int)PaletteColorIndex.ToolStripGradientMiddle];

        #endregion

        #region ToolStripPanelGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the ToolStripPanel.
        /// </summary>
        public override Color ToolStripPanelGradientBegin =>
            _colors[(int)PaletteColorIndex.ToolStripPanelGradientBegin] == Color.Empty
                ? base.ToolStripPanelGradientBegin
                : _colors[(int)PaletteColorIndex.ToolStripPanelGradientBegin];

        #endregion

        #region ToolStripPanelGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStripPanel.
        /// </summary>
        public override Color ToolStripPanelGradientEnd =>
            _colors[(int)PaletteColorIndex.ToolStripPanelGradientEnd] == Color.Empty
                ? base.ToolStripPanelGradientEnd
                : _colors[(int)PaletteColorIndex.ToolStripPanelGradientEnd];

        #endregion
        #endregion
    }
}
