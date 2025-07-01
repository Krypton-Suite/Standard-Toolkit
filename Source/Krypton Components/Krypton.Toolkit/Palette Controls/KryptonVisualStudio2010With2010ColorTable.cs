#region BSD License
/*
 *   BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provide KryptonVisualStudio2010With2010ColorTable values using an array of Color values as the source.
/// </summary>
public class KryptonVisualStudio2010With2010ColorTable : KryptonColorTable
{
    #region Static Fields
    private static readonly Color _contextMenuBackground = Color.White;
    private static readonly Color _menuBorder = Color.FromArgb(167, 171, 176);
    private static readonly Color _checkBackground = Color.FromArgb(252, 241, 194);
    private static readonly Color _buttonSelectedBegin = Color.FromArgb(251, 242, 215);
    private static readonly Color _buttonSelectedEnd = Color.FromArgb(247, 224, 135);
    private static readonly Color _buttonPressedBegin = Color.FromArgb(255, 228, 138);
    private static readonly Color _buttonPressedEnd = Color.FromArgb(255, 228, 138);
    private static readonly Color _buttonCheckedBegin = Color.FromArgb(255, 216, 107);
    private static readonly Color _buttonCheckedEnd = Color.FromArgb(255, 216, 107);
    private static readonly Color _menuItemSelectedBegin = Color.FromArgb(251, 242, 215);
    private static readonly Color _menuItemSelectedEnd = Color.FromArgb(247, 224, 135);
    private static readonly Color _menuStripItemTextColor = Color.FromArgb(0, 0, 0);
    private static Font _menuToolFont;
    private static Font _statusFont;
    #endregion

    #region Identity
    static KryptonVisualStudio2010With2010ColorTable()
    {
        // Get the font settings from the system
        DefineFonts();

        // We need to notice when system color settings change
        SystemEvents.UserPreferenceChanged += OnUserPreferenceChanged;
    }

    /// <summary>
    /// Initialize a new instance of the KryptonVisualStudio2010With2010ColorTable class.
    /// </summary>
    /// <param name="colors">Source of </param>
    /// <param name="roundedEdges">Should have rounded edges.</param>
    /// <param name="palette">Associated palette instance.</param>
    public KryptonVisualStudio2010With2010ColorTable([DisallowNull] Color[] colors,
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
    public override Color ButtonPressedBorder => Colors[(int)SchemeBaseColors.ButtonBorder];

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
    public override Color ButtonPressedHighlightBorder => Colors[(int)SchemeBaseColors.ButtonBorder];

    #endregion
    #endregion

    #region ButtonSelected
    #region ButtonSelectedBorder
    /// <summary>
    /// Gets the border color for a button being selected.
    /// </summary>
    public override Color ButtonSelectedBorder => Colors[(int)SchemeBaseColors.ButtonBorder];

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
    public override Color ButtonSelectedHighlightBorder => Colors[(int)SchemeBaseColors.ButtonBorder];

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
    public override Color ButtonCheckedHighlightBorder => Colors[(int)SchemeBaseColors.ButtonBorder];

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
    public override Color GripLight => Colors[(int)SchemeBaseColors.GripLight];

    #endregion

    #region GripDark
    /// <summary>
    /// Gets the dark color used to draw grips.
    /// </summary>
    public override Color GripDark => Colors[(int)SchemeBaseColors.GripDark];

    #endregion
    #endregion

    #region ImageMargin
    #region ImageMarginGradientBegin
    /// <summary>
    /// Gets the starting color for the context menu margin.
    /// </summary>
    public override Color ImageMarginGradientBegin => Colors[(int)SchemeBaseColors.ImageMargin];

    #endregion

    #region ImageMarginGradientMiddle
    /// <summary>
    /// Gets the middle color for the context menu margin.
    /// </summary>
    public override Color ImageMarginGradientMiddle => Colors[(int)SchemeBaseColors.ImageMargin];

    #endregion

    #region ImageMarginGradientEnd
    /// <summary>
    /// Gets the ending color for the context menu margin.
    /// </summary>
    public override Color ImageMarginGradientEnd => Colors[(int)SchemeBaseColors.ImageMargin];

    #endregion

    #region ImageMarginRevealedGradientBegin
    /// <summary>
    /// Gets the starting color for the context menu margin revealed.
    /// </summary>
    public override Color ImageMarginRevealedGradientBegin => Colors[(int)SchemeBaseColors.ImageMargin];

    #endregion

    #region ImageMarginRevealedGradientMiddle
    /// <summary>
    /// Gets the middle color for the context menu margin revealed.
    /// </summary>
    public override Color ImageMarginRevealedGradientMiddle => Colors[(int)SchemeBaseColors.ImageMargin];

    #endregion

    #region ImageMarginRevealedGradientEnd
    /// <summary>
    /// Gets the ending color for the context menu margin revealed.
    /// </summary>
    public override Color ImageMarginRevealedGradientEnd => Colors[(int)SchemeBaseColors.ImageMargin];

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
    public override Color MenuItemSelected => Colors[(int)SchemeBaseColors.ButtonBorder];

    #endregion

    #region MenuItemPressedGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used when a top-level ToolStripMenuItem is pressed down.
    /// </summary>
    public override Color MenuItemPressedGradientBegin => Colors[(int)SchemeBaseColors.ToolStripBegin];

    #endregion

    #region MenuItemPressedGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used when a top-level ToolStripMenuItem is pressed down.
    /// </summary>
    public override Color MenuItemPressedGradientEnd => Colors[(int)SchemeBaseColors.ToolStripEnd];

    #endregion

    #region MenuItemPressedGradientMiddle
    /// <summary>
    /// Gets the middle color of the gradient used when a top-level ToolStripMenuItem is pressed down.
    /// </summary>
    public override Color MenuItemPressedGradientMiddle => Colors[(int)SchemeBaseColors.ToolStripMiddle];

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
    public override Color MenuStripGradientBegin => Colors[(int)SchemeBaseColors.ToolStripBack];

    #endregion

    #region MenuStripGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used in the MenuStrip.
    /// </summary>
    public override Color MenuStripGradientEnd => Colors[(int)SchemeBaseColors.ToolStripBack];

    #endregion

    #endregion

    #region OverflowButton
    #region OverflowButtonGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used in the ToolStripOverflowButton.
    /// </summary>
    public override Color OverflowButtonGradientBegin => Colors[(int)SchemeBaseColors.OverflowBegin];

    #endregion

    #region OverflowButtonGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used in the ToolStripOverflowButton.
    /// </summary>
    public override Color OverflowButtonGradientEnd => Colors[(int)SchemeBaseColors.OverflowEnd];

    #endregion

    #region OverflowButtonGradientMiddle
    /// <summary>
    /// Gets the middle color of the gradient used in the ToolStripOverflowButton.
    /// </summary>
    public override Color OverflowButtonGradientMiddle => Colors[(int)SchemeBaseColors.OverflowMiddle];

    #endregion
    #endregion

    #region RaftingContainer
    #region RaftingContainerGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used in the ToolStripContainer.
    /// </summary>
    public override Color RaftingContainerGradientBegin => Colors[(int)SchemeBaseColors.ToolStripBack];

    #endregion

    #region RaftingContainerGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used in the ToolStripContainer.
    /// </summary>
    public override Color RaftingContainerGradientEnd => Colors[(int)SchemeBaseColors.ToolStripBack];

    #endregion

    #endregion

    #region Separator
    #region SeparatorLight
    /// <summary>
    /// Gets the light separator color.
    /// </summary>
    public override Color SeparatorLight => Colors[(int)SchemeBaseColors.SeparatorLight];

    #endregion

    #region SeparatorDark
    /// <summary>
    /// Gets the dark separator color.
    /// </summary>
    public override Color SeparatorDark => Colors[(int)SchemeBaseColors.SeparatorDark];

    #endregion
    #endregion

    #region StatusStrip
    #region StatusStripGradientBegin
    /// <summary>
    /// Gets the starting color for the status strip background.
    /// </summary>
    public override Color StatusStripGradientBegin => Colors[(int)SchemeBaseColors.StatusStripLight];

    #endregion

    #region StatusStripGradientEnd
    /// <summary>
    /// Gets the ending color for the status strip background.
    /// </summary>
    public override Color StatusStripGradientEnd => Colors[(int)SchemeBaseColors.StatusStripDark];

    #endregion
    #endregion

    #region Text
    #region MenuItemText

    /// <summary>
    /// Gets the text color used on the menu items.
    /// </summary>
    public override Color MenuItemText => _menuStripItemTextColor; // Colors[(int)SchemeBaseColors.TextButtonNormal];

    #endregion

    #region MenuStripText

    /// <summary>
    /// Gets the text color used on the menu strip.
    /// </summary>
    public override Color MenuStripText => _menuStripItemTextColor; //Colors[(int)SchemeBaseColors.StatusStripText];

    #endregion

    #region ToolStripText
    /// <summary>
    /// Gets the text color used on the tool strip.
    /// </summary>
    public override Color ToolStripText => Colors[(int)SchemeBaseColors.StatusStripText];

    #endregion

    #region StatusStripText
    /// <summary>
    /// Gets the text color used on the status strip.
    /// </summary>
    public override Color StatusStripText => Colors[(int)SchemeBaseColors.StatusStripText];

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
    public override Color ToolStripBorder => Colors[(int)SchemeBaseColors.ToolStripBorder];

    #endregion

    #region ToolStripContentPanelGradientBegin
    /// <summary>
    /// Gets the starting color for the content panel background.
    /// </summary>
    public override Color ToolStripContentPanelGradientBegin => Colors[(int)SchemeBaseColors.ToolStripBack];

    #endregion

    #region ToolStripContentPanelGradientEnd
    /// <summary>
    /// Gets the ending color for the content panel background.
    /// </summary>
    public override Color ToolStripContentPanelGradientEnd => Colors[(int)SchemeBaseColors.ToolStripBack];

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
    public override Color ToolStripGradientBegin => Colors[(int)SchemeBaseColors.ToolStripBegin];

    #endregion

    #region ToolStripGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used in the ToolStrip background.
    /// </summary>
    public override Color ToolStripGradientEnd => Colors[(int)SchemeBaseColors.ToolStripEnd];

    #endregion

    #region ToolStripGradientMiddle
    /// <summary>
    /// Gets the middle color of the gradient used in the ToolStrip background.
    /// </summary>
    public override Color ToolStripGradientMiddle => Colors[(int)SchemeBaseColors.ToolStripMiddle];

    #endregion

    #region ToolStripPanelGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used in the ToolStripPanel.
    /// </summary>
    public override Color ToolStripPanelGradientBegin => Colors[(int)SchemeBaseColors.ToolStripBack];

    #endregion

    #region ToolStripPanelGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used in the ToolStripPanel.
    /// </summary>
    public override Color ToolStripPanelGradientEnd => Colors[(int)SchemeBaseColors.ToolStripBack];

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