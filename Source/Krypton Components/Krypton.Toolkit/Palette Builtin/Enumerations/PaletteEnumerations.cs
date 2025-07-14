#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege, et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

#region Enumeration: SchemeBaseColors

/// <summary>
/// Defines the set of color roles used by the base color scheme for various UI elements.
/// Each value represents a specific color usage in controls, forms, ribbons, menus, grids, and other components.
/// </summary>
public enum SchemeBaseColors
{
    /// <summary>Text color for standard labels and controls.</summary>
    TextLabelControl = 0,

    /// <summary>Text color for normal state buttons.</summary>
    TextButtonNormal = 1,

    /// <summary>Text color for checked state buttons.</summary>
    TextButtonChecked = 2,

    /// <summary>Border color for normal state buttons.</summary>
    ButtonNormalBorder = 3,

    /// <summary>Default border color for normal state buttons.</summary>
    ButtonNormalDefaultBorder = 4,

    /// <summary>Primary background color for normal state buttons.</summary>
    ButtonNormalBack1 = 5,

    /// <summary>Secondary background color for normal state buttons.</summary>
    ButtonNormalBack2 = 6,

    /// <summary>Primary background color for default normal state buttons.</summary>
    ButtonNormalDefaultBack1 = 7,

    /// <summary>Secondary background color for default normal state buttons.</summary>
    ButtonNormalDefaultBack2 = 8,

    /// <summary>Primary background color for navigator buttons in normal state.</summary>
    ButtonNormalNavigatorBack1 = 9,

    /// <summary>Secondary background color for navigator buttons in normal state.</summary>
    ButtonNormalNavigatorBack2 = 10,

    /// <summary>Background color for client panels.</summary>
    PanelClient = 11,

    /// <summary>Background color for alternative panels.</summary>
    PanelAlternative = 12,

    /// <summary>Standard control border color.</summary>
    ControlBorder = 13,

    /// <summary>Primary border color for high-emphasis separators.</summary>
    SeparatorHighBorder1 = 14,

    /// <summary>Secondary border color for high-emphasis separators.</summary>
    SeparatorHighBorder2 = 15,

    /// <summary>Primary background color for primary headers.</summary>
    HeaderPrimaryBack1 = 16,

    /// <summary>Secondary background color for primary headers.</summary>
    HeaderPrimaryBack2 = 17,

    /// <summary>Primary background color for secondary headers.</summary>
    HeaderSecondaryBack1 = 18,

    /// <summary>Secondary background color for secondary headers.</summary>
    HeaderSecondaryBack2 = 19,

    /// <summary>Text color for headers.</summary>
    HeaderText = 20,

    /// <summary>Text color for status strips.</summary>
    StatusStripText = 21,

    /// <summary>General button border color.</summary>
    ButtonBorder = 22,

    /// <summary>Light color for separators.</summary>
    SeparatorLight = 23,

    /// <summary>Dark color for separators.</summary>
    SeparatorDark = 24,

    /// <summary>Light color for grip elements.</summary>
    GripLight = 25,

    /// <summary>Dark color for grip elements.</summary>
    GripDark = 26,

    /// <summary>Background color for tool strips.</summary>
    ToolStripBack = 27,

    /// <summary>Light color for status strips.</summary>
    StatusStripLight = 28,

    /// <summary>Dark color for status strips.</summary>
    StatusStripDark = 29,

    /// <summary>Color for image margins in menus/toolstrips.</summary>
    ImageMargin = 30,

    /// <summary>Gradient start color for tool strips.</summary>
    ToolStripBegin = 31,

    /// <summary>Gradient middle color for tool strips.</summary>
    ToolStripMiddle = 32,

    /// <summary>Gradient end color for tool strips.</summary>
    ToolStripEnd = 33,

    /// <summary>Gradient start color for overflow areas.</summary>
    OverflowBegin = 34,

    /// <summary>Gradient middle color for overflow areas.</summary>
    OverflowMiddle = 35,

    /// <summary>Gradient end color for overflow areas.</summary>
    OverflowEnd = 36,

    /// <summary>Border color for tool strips.</summary>
    ToolStripBorder = 37,

    /// <summary>Active form border color.</summary>
    FormBorderActive = 38,

    /// <summary>Inactive form border color.</summary>
    FormBorderInactive = 39,

    /// <summary>Light color for active form borders.</summary>
    FormBorderActiveLight = 40,

    /// <summary>Dark color for active form borders.</summary>
    FormBorderActiveDark = 41,

    /// <summary>Light color for inactive form borders.</summary>
    FormBorderInactiveLight = 42,

    /// <summary>Dark color for inactive form borders.</summary>
    FormBorderInactiveDark = 43,

    /// <summary>Header color for active form borders.</summary>
    FormBorderHeaderActive = 44,

    /// <summary>Header color for inactive form borders.</summary>
    FormBorderHeaderInactive = 45,

    /// <summary>Primary header color for active form borders.</summary>
    FormBorderHeaderActive1 = 46,

    /// <summary>Secondary header color for active form borders.</summary>
    FormBorderHeaderActive2 = 47,

    /// <summary>Primary header color for inactive form borders.</summary>
    FormBorderHeaderInactive1 = 48,

    /// <summary>Secondary header color for inactive form borders.</summary>
    FormBorderHeaderInactive2 = 49,

    /// <summary>Short header color for active forms.</summary>
    FormHeaderShortActive = 50,

    /// <summary>Short header color for inactive forms.</summary>
    FormHeaderShortInactive = 51,

    /// <summary>Long header color for active forms.</summary>
    FormHeaderLongActive = 52,

    /// <summary>Long header color for inactive forms.</summary>
    FormHeaderLongInactive = 53,

    /// <summary>Border color for form buttons in tracking state.</summary>
    FormButtonBorderTrack = 54,

    /// <summary>Primary background color for form buttons in tracking state.</summary>
    FormButtonBack1Track = 55,

    /// <summary>Secondary background color for form buttons in tracking state.</summary>
    FormButtonBack2Track = 56,

    /// <summary>Border color for form buttons in pressed state.</summary>
    FormButtonBorderPressed = 57,

    /// <summary>Primary background color for form buttons in pressed state.</summary>
    FormButtonBack1Pressed = 58,

    /// <summary>Secondary background color for form buttons in pressed state.</summary>
    FormButtonBack2Pressed = 59,

    /// <summary>Text color for form buttons in normal state.</summary>
    TextButtonFormNormal = 60,

    /// <summary>Text color for form buttons in tracking state.</summary>
    TextButtonFormTracking = 61,

    /// <summary>Text color for form buttons in pressed state.</summary>
    TextButtonFormPressed = 62,

    /// <summary>Link color for not visited links (override control).</summary>
    LinkNotVisitedOverrideControl = 63,

    /// <summary>Link color for visited links (override control).</summary>
    LinkVisitedOverrideControl = 64,

    /// <summary>Link color for pressed links (override control).</summary>
    LinkPressedOverrideControl = 65,

    /// <summary>Link color for not visited links (override panel).</summary>
    LinkNotVisitedOverridePanel = 66,

    /// <summary>Link color for visited links (override panel).</summary>
    LinkVisitedOverridePanel = 67,

    /// <summary>Link color for pressed links (override panel).</summary>
    LinkPressedOverridePanel = 68,

    /// <summary>Text color for labels on panels.</summary>
    TextLabelPanel = 69,

    /// <summary>Text color for normal ribbon tabs.</summary>
    RibbonTabTextNormal = 70,

    /// <summary>Text color for checked ribbon tabs.</summary>
    RibbonTabTextChecked = 71,

    /// <summary>Primary color for selected ribbon tabs.</summary>
    RibbonTabSelected1 = 72,

    /// <summary>Secondary color for selected ribbon tabs.</summary>
    RibbonTabSelected2 = 73,

    /// <summary>Tertiary color for selected ribbon tabs.</summary>
    RibbonTabSelected3 = 74,

    /// <summary>Quaternary color for selected ribbon tabs.</summary>
    RibbonTabSelected4 = 75,

    /// <summary>Quinary color for selected ribbon tabs.</summary>
    RibbonTabSelected5 = 76,

    /// <summary>Primary color for tracking ribbon tabs.</summary>
    RibbonTabTracking1 = 77,

    /// <summary>Secondary color for tracking ribbon tabs.</summary>
    RibbonTabTracking2 = 78,

    /// <summary>Primary highlight color for ribbon tabs.</summary>
    RibbonTabHighlight1 = 79,

    /// <summary>Secondary highlight color for ribbon tabs.</summary>
    RibbonTabHighlight2 = 80,

    /// <summary>Tertiary highlight color for ribbon tabs.</summary>
    RibbonTabHighlight3 = 81,

    /// <summary>Quaternary highlight color for ribbon tabs.</summary>
    RibbonTabHighlight4 = 82,

    /// <summary>Quinary highlight color for ribbon tabs.</summary>
    RibbonTabHighlight5 = 83,

    /// <summary>Separator color for ribbon tabs.</summary>
    RibbonTabSeparatorColor = 84,

    /// <summary>Primary background color for ribbon groups area.</summary>
    RibbonGroupsArea1 = 85,

    /// <summary>Secondary background color for ribbon groups area.</summary>
    RibbonGroupsArea2 = 86,

    /// <summary>Tertiary background color for ribbon groups area.</summary>
    RibbonGroupsArea3 = 87,

    /// <summary>Quaternary background color for ribbon groups area.</summary>
    RibbonGroupsArea4 = 88,

    /// <summary>Quinary background color for ribbon groups area.</summary>
    RibbonGroupsArea5 = 89,

    /// <summary>Primary border color for ribbon groups.</summary>
    RibbonGroupBorder1 = 90,

    /// <summary>Secondary border color for ribbon groups.</summary>
    RibbonGroupBorder2 = 91,

    /// <summary>Primary title color for ribbon groups.</summary>
    RibbonGroupTitle1 = 92,

    /// <summary>Secondary title color for ribbon groups.</summary>
    RibbonGroupTitle2 = 93,

    /// <summary>Primary border color for context ribbon groups.</summary>
    RibbonGroupBorderContext1 = 94,

    /// <summary>Secondary border color for context ribbon groups.</summary>
    RibbonGroupBorderContext2 = 95,

    /// <summary>Primary title color for context ribbon groups.</summary>
    RibbonGroupTitleContext1 = 96,

    /// <summary>Secondary title color for context ribbon groups.</summary>
    RibbonGroupTitleContext2 = 97,

    /// <summary>Dark color for ribbon group dialog background.</summary>
    RibbonGroupDialogDark = 98,

    /// <summary>Light color for ribbon group dialog background.</summary>
    RibbonGroupDialogLight = 99,

    /// <summary>Primary tracking color for ribbon group titles.</summary>
    RibbonGroupTitleTracking1 = 100,

    /// <summary>Secondary tracking color for ribbon group titles.</summary>
    RibbonGroupTitleTracking2 = 101,

    /// <summary>Dark color for ribbon minimize bar.</summary>
    RibbonMinimizeBarDark = 102,

    /// <summary>Light color for ribbon minimize bar.</summary>
    RibbonMinimizeBarLight = 103,

    /// <summary>Primary border color for collapsed ribbon groups.</summary>
    RibbonGroupCollapsedBorder1 = 104,

    /// <summary>Secondary border color for collapsed ribbon groups.</summary>
    RibbonGroupCollapsedBorder2 = 105,

    /// <summary>Tertiary border color for collapsed ribbon groups.</summary>
    RibbonGroupCollapsedBorder3 = 106,

    /// <summary>Quaternary border color for collapsed ribbon groups.</summary>
    RibbonGroupCollapsedBorder4 = 107,

    /// <summary>Primary background color for collapsed ribbon groups.</summary>
    RibbonGroupCollapsedBack1 = 108,

    /// <summary>Secondary background color for collapsed ribbon groups.</summary>
    RibbonGroupCollapsedBack2 = 109,

    /// <summary>Tertiary background color for collapsed ribbon groups.</summary>
    RibbonGroupCollapsedBack3 = 110,

    /// <summary>Quaternary background color for collapsed ribbon groups.</summary>
    RibbonGroupCollapsedBack4 = 111,

    /// <summary>Primary border color for collapsed ribbon group tracking state.</summary>
    RibbonGroupCollapsedBorderT1 = 112,

    /// <summary>Secondary border color for collapsed ribbon group tracking state.</summary>
    RibbonGroupCollapsedBorderT2 = 113,

    /// <summary>Tertiary border color for collapsed ribbon group tracking state.</summary>
    RibbonGroupCollapsedBorderT3 = 114,

    /// <summary>Quaternary border color for collapsed ribbon group tracking state.</summary>
    RibbonGroupCollapsedBorderT4 = 115,

    /// <summary>Primary background color for collapsed ribbon group tracking state.</summary>
    RibbonGroupCollapsedBackT1 = 116,

    /// <summary>Secondary background color for collapsed ribbon group tracking state.</summary>
    RibbonGroupCollapsedBackT2 = 117,

    /// <summary>Tertiary background color for collapsed ribbon group tracking state.</summary>
    RibbonGroupCollapsedBackT3 = 118,

    /// <summary>Quaternary background color for collapsed ribbon group tracking state.</summary>
    RibbonGroupCollapsedBackT4 = 119,

    /// <summary>Primary border color for ribbon group frames.</summary>
    RibbonGroupFrameBorder1 = 120,

    /// <summary>Secondary border color for ribbon group frames.</summary>
    RibbonGroupFrameBorder2 = 121,

    /// <summary>Primary inside color for ribbon group frames.</summary>
    RibbonGroupFrameInside1 = 122,

    /// <summary>Secondary inside color for ribbon group frames.</summary>
    RibbonGroupFrameInside2 = 123,

    /// <summary>Tertiary inside color for ribbon group frames.</summary>
    RibbonGroupFrameInside3 = 124,

    /// <summary>Quaternary inside color for ribbon group frames.</summary>
    RibbonGroupFrameInside4 = 125,

    /// <summary>Text color for collapsed ribbon groups.</summary>
    RibbonGroupCollapsedText = 126,

    /// <summary>Text color for ribbon group buttons.</summary>
    RibbonGroupButtonText = 127,

    /// <summary>Primary background color for alternate pressed state.</summary>
    AlternatePressedBack1 = 128,

    /// <summary>Secondary background color for alternate pressed state.</summary>
    AlternatePressedBack2 = 129,

    /// <summary>Primary border color for alternate pressed state.</summary>
    AlternatePressedBorder1 = 130,

    /// <summary>Secondary border color for alternate pressed state.</summary>
    AlternatePressedBorder2 = 131,

    /// <summary>Primary background color for checked form buttons.</summary>
    FormButtonBack1Checked = 132,

    /// <summary>Secondary background color for checked form buttons.</summary>
    FormButtonBack2Checked = 133,

    /// <summary>Border color for checked form buttons.</summary>
    FormButtonBorderCheck = 134,

    /// <summary>Primary background color for form button check tracking state.</summary>
    FormButtonBack1CheckTrack = 135,

    /// <summary>Secondary background color for form button check tracking state.</summary>
    FormButtonBack2CheckTrack = 136,

    /// <summary>Mini QAT (Quick Access Toolbar) color 1.</summary>
    RibbonQATMini1 = 137,

    /// <summary>Mini QAT color 2.</summary>
    RibbonQATMini2 = 138,

    /// <summary>Mini QAT color 3.</summary>
    RibbonQATMini3 = 139,

    /// <summary>Mini QAT color 4.</summary>
    RibbonQATMini4 = 140,

    /// <summary>Mini QAT color 5.</summary>
    RibbonQATMini5 = 141,

    /// <summary>Mini QAT inactive color 1.</summary>
    RibbonQATMini1I = 142,

    /// <summary>Mini QAT inactive color 2.</summary>
    RibbonQATMini2I = 143,

    /// <summary>Mini QAT inactive color 3.</summary>
    RibbonQATMini3I = 144,

    /// <summary>Mini QAT inactive color 4.</summary>
    RibbonQATMini4I = 145,

    /// <summary>Mini QAT inactive color 5.</summary>
    RibbonQATMini5I = 146,

    /// <summary>Fullbar QAT color 1.</summary>
    RibbonQATFullbar1 = 147,

    /// <summary>Fullbar QAT color 2.</summary>
    RibbonQATFullbar2 = 148,

    /// <summary>Fullbar QAT color 3.</summary>
    RibbonQATFullbar3 = 149,

    /// <summary>Dark color for QAT button.</summary>
    RibbonQATButtonDark = 150,

    /// <summary>Light color for QAT button.</summary>
    RibbonQATButtonLight = 151,

    /// <summary>Primary color for QAT overflow area.</summary>
    RibbonQATOverflow1 = 152,

    /// <summary>Secondary color for QAT overflow area.</summary>
    RibbonQATOverflow2 = 153,

    /// <summary>Dark color for ribbon group separator.</summary>
    RibbonGroupSeparatorDark = 154,

    /// <summary>Light color for ribbon group separator.</summary>
    RibbonGroupSeparatorLight = 155,

    /// <summary>Primary background color for button cluster buttons.</summary>
    ButtonClusterButtonBack1 = 156,

    /// <summary>Secondary background color for button cluster buttons.</summary>
    ButtonClusterButtonBack2 = 157,

    /// <summary>Primary border color for button cluster buttons.</summary>
    ButtonClusterButtonBorder1 = 158,

    /// <summary>Secondary border color for button cluster buttons.</summary>
    ButtonClusterButtonBorder2 = 159,

    /// <summary>Background color for mini navigator.</summary>
    NavigatorMiniBackColor = 160,

    /// <summary>Primary background color for grid list normal state.</summary>
    GridListNormal1 = 161,

    /// <summary>Secondary background color for grid list normal state.</summary>
    GridListNormal2 = 162,

    /// <summary>Primary background color for grid list pressed state.</summary>
    GridListPressed1 = 163,

    /// <summary>Secondary background color for grid list pressed state.</summary>
    GridListPressed2 = 164,

    /// <summary>Background color for selected grid list items.</summary>
    GridListSelected = 165,

    /// <summary>Primary background color for grid sheet column normal state.</summary>
    GridSheetColNormal1 = 166,

    /// <summary>Secondary background color for grid sheet column normal state.</summary>
    GridSheetColNormal2 = 167,

    /// <summary>Primary background color for grid sheet column pressed state.</summary>
    GridSheetColPressed1 = 168,

    /// <summary>Secondary background color for grid sheet column pressed state.</summary>
    GridSheetColPressed2 = 169,

    /// <summary>Primary background color for grid sheet column selected state.</summary>
    GridSheetColSelected1 = 170,

    /// <summary>Secondary background color for grid sheet column selected state.</summary>
    GridSheetColSelected2 = 171,

    /// <summary>Background color for grid sheet row normal state.</summary>
    GridSheetRowNormal = 172,

    /// <summary>Background color for grid sheet row pressed state.</summary>
    GridSheetRowPressed = 173,

    /// <summary>Background color for grid sheet row selected state.</summary>
    GridSheetRowSelected = 174,

    /// <summary>Border color for grid data cells.</summary>
    GridDataCellBorder = 175,

    /// <summary>Background color for selected grid data cells.</summary>
    GridDataCellSelected = 176,

    /// <summary>Text color for normal input controls.</summary>
    InputControlTextNormal = 177,

    /// <summary>Text color for disabled input controls.</summary>
    InputControlTextDisabled = 178,

    /// <summary>Border color for normal input controls.</summary>
    InputControlBorderNormal = 179,

    /// <summary>Border color for disabled input controls.</summary>
    InputControlBorderDisabled = 180,

    /// <summary>Background color for normal input controls.</summary>
    InputControlBackNormal = 181,

    /// <summary>Background color for disabled input controls.</summary>
    InputControlBackDisabled = 182,

    /// <summary>Background color for inactive input controls.</summary>
    InputControlBackInactive = 183,

    /// <summary>Primary background color for normal input dropdowns.</summary>
    InputDropDownNormal1 = 184,

    /// <summary>Secondary background color for normal input dropdowns.</summary>
    InputDropDownNormal2 = 185,

    /// <summary>Primary background color for disabled input dropdowns.</summary>
    InputDropDownDisabled1 = 186,

    /// <summary>Secondary background color for disabled input dropdowns.</summary>
    InputDropDownDisabled2 = 187,

    /// <summary>Background color for context menu headings.</summary>
    ContextMenuHeadingBack = 188,

    /// <summary>Text color for context menu headings.</summary>
    ContextMenuHeadingText = 189,

    /// <summary>Background color for context menu image columns.</summary>
    ContextMenuImageColumn = 190,

    /// <summary>Primary background color for application button.</summary>
    AppButtonBack1 = 191,

    /// <summary>Secondary background color for application button.</summary>
    AppButtonBack2 = 192,

    /// <summary>Border color for application button.</summary>
    AppButtonBorder = 193,

    /// <summary>Outer color 1 for application button.</summary>
    AppButtonOuter1 = 194,

    /// <summary>Outer color 2 for application button.</summary>
    AppButtonOuter2 = 195,

    /// <summary>Outer color 3 for application button.</summary>
    AppButtonOuter3 = 196,

    /// <summary>Inner color 1 for application button.</summary>
    AppButtonInner1 = 197,

    /// <summary>Inner color 2 for application button.</summary>
    AppButtonInner2 = 198,

    /// <summary>Background color for application button menu documents area.</summary>
    AppButtonMenuDocsBack = 199,

    /// <summary>Text color for application button menu documents area.</summary>
    AppButtonMenuDocsText = 200,

    /// <summary>Primary internal border color for high-emphasis separators.</summary>
    SeparatorHighInternalBorder1 = 201,

    /// <summary>Secondary internal border color for high-emphasis separators.</summary>
    SeparatorHighInternalBorder2 = 202,

    /// <summary>Border color for ribbon gallery.</summary>
    RibbonGalleryBorder = 203,

    /// <summary>Normal background color for ribbon gallery.</summary>
    RibbonGalleryBackNormal = 204,

    /// <summary>Tracking background color for ribbon gallery.</summary>
    RibbonGalleryBackTracking = 205,

    /// <summary>Primary background color for ribbon gallery.</summary>
    RibbonGalleryBack1 = 206,

    /// <summary>Secondary background color for ribbon gallery.</summary>
    RibbonGalleryBack2 = 207,

    /// <summary>Tertiary tracking color for ribbon tabs.</summary>
    RibbonTabTracking3 = 208,

    /// <summary>Quaternary tracking color for ribbon tabs.</summary>
    RibbonTabTracking4 = 209,

    /// <summary>Tertiary border color for ribbon groups.</summary>
    RibbonGroupBorder3 = 210,

    /// <summary>Quaternary border color for ribbon groups.</summary>
    RibbonGroupBorder4 = 211,

    /// <summary>Quinary border color for ribbon groups.</summary>
    RibbonGroupBorder5 = 212,

    /// <summary>Text color for ribbon group titles.</summary>
    RibbonGroupTitleText = 213,

    /// <summary>Light color for ribbon drop arrows.</summary>
    RibbonDropArrowLight = 214,

    /// <summary>Dark color for ribbon drop arrows.</summary>
    RibbonDropArrowDark = 215,

    /// <summary>Primary background color for inactive docked headers.</summary>
    HeaderDockInactiveBack1 = 216,

    /// <summary>Secondary background color for inactive docked headers.</summary>
    HeaderDockInactiveBack2 = 217,

    /// <summary>Border color for navigator buttons.</summary>
    ButtonNavigatorBorder = 218,

    /// <summary>Text color for navigator buttons.</summary>
    ButtonNavigatorText = 219,

    /// <summary>Primary tracking color for navigator buttons.</summary>
    ButtonNavigatorTrack1 = 220,

    /// <summary>Secondary tracking color for navigator buttons.</summary>
    ButtonNavigatorTrack2 = 221,

    /// <summary>Primary pressed color for navigator buttons.</summary>
    ButtonNavigatorPressed1 = 222,

    /// <summary>Secondary pressed color for navigator buttons.</summary>
    ButtonNavigatorPressed2 = 223,

    /// <summary>Primary checked color for navigator buttons.</summary>
    ButtonNavigatorChecked1 = 224,

    /// <summary>Secondary checked color for navigator buttons.</summary>
    ButtonNavigatorChecked2 = 225,

    /// <summary>Bottom color for tooltips.</summary>
    ToolTipBottom = 226,

    // ============================================
    /// <summary>Text color for menu items.</summary>
    MenuItemText = 227,

    /// <summary>Gradient start color for menu margins.</summary>
    MenuMarginGradientStart = 228,

    /// <summary>Gradient middle color for menu margins.</summary>
    MenuMarginGradientMiddle = 229,

    /// <summary>Gradient end color for menu margins.</summary>
    MenuMarginGradientEnd = 230,

    /// <summary>Text color for disabled menu items.</summary>
    DisabledMenuItemText = 231,

    /// <summary>Text color for menu strips.</summary>
    MenuStripText = 232,

    /// <summary>TrackBar Tick Marks color.</summary>
    TrackBarTickMarks = 233,

    /// <summary>TrackBar Top Track color.</summary>
    TrackBarTopTrack = 234,

    /// <summary>TrackBar Bottom Track color.</summary>
    TrackBarBottomTrack = 235,

    /// <summary>TrackBar Fill Track color.</summary>
    TrackBarFillTrack = 236,

    /// <summary>TrackBar Outside Position color.</summary>
    TrackBarOutsidePosition = 237,

    /// <summary>TrackBar Border Position color.</summary>
    TrackBarBorderPosition = 238
}

#endregion

#region Enumeration: SchemeExtraColors

/// <summary>
/// Represents additional color scheme elements used throughout the UI theme system.
/// These colors are not part of the standard Krypton color set, but provide extended support
/// for context menus, form buttons, tooltips, and disabled states.
/// </summary>
public enum SchemeExtraColors
{
    /// <summary>Text color when a button is in the tracking (hover) state.</summary>
    ButtonTextTracking = 0,

    /// <summary>Background color for disabled controls.</summary>
    DisabledBack = 1,

    /// <summary>Alternate background color for disabled controls (e.g., for gradients).</summary>
    DisabledBackAlternate = 2,

    /// <summary>Border color used on disabled controls.</summary>
    DisabledBorder = 3,

    /// <summary>Dark glyph color (e.g., icons or symbols) for disabled states.</summary>
    DisabledGlyphDark = 4,

    /// <summary>Light glyph color for disabled states, used with backgrounds to maintain contrast.</summary>
    DisabledGlyphLight = 5,

    /// <summary>Primary text color for disabled controls.</summary>
    DisabledText = 6,

    /// <summary>Alternate text color for disabled controls.</summary>
    DisabledTextAlternate = 7,

    /// <summary>Top-level border color 1 for context-checked (active) tabs.</summary>
    ContextCheckedTabBorder1 = 8,

    /// <summary>Top-level border color 2 for context-checked tabs.</summary>
    ContextCheckedTabBorder2 = 9,

    /// <summary>Top-level border color 3 for context-checked tabs.</summary>
    ContextCheckedTabBorder3 = 10,

    /// <summary>Top-level border color 4 for context-checked tabs.</summary>
    ContextCheckedTabBorder4 = 11,

    /// <summary>Color of the separator line between context tabs.</summary>
    ContextTabSeparator = 12,

    /// <summary>Text color used for context tab headers.</summary>
    ContextText = 13,

    /// <summary>Background color of context menus.</summary>
    ContextMenuBack = 14,

    /// <summary>Border color of context menus.</summary>
    ContextMenuBorder = 15,

    /// <summary>Border color used around headings in context menus.</summary>
    ContextMenuHeadingBorder = 16,

    /// <summary>Background color of an image item when it is checked in the context menu.</summary>
    ContextMenuImageBackChecked = 17,

    /// <summary>Border color around a checked image item in the context menu.</summary>
    ContextMenuImageBorderChecked = 18,

    /// <summary>Tracking border color for the close button on a form.</summary>
    FormCloseBorderTracking = 19,

    /// <summary>Pressed border color for the close button on a form.</summary>
    FormCloseBorderPressed = 20,

    /// <summary>Normal checked border color for the close button.</summary>
    FormCloseBorderCheckedNormal = 21,

    /// <summary>First gradient/tracking color of the close button.</summary>
    FormCloseTracking1 = 22,

    /// <summary>Second gradient/tracking color of the close button.</summary>
    FormCloseTracking2 = 23,

    /// <summary>First gradient/pressed color of the close button.</summary>
    FormClosePressed1 = 24,

    /// <summary>Second gradient/pressed color of the close button.</summary>
    FormClosePressed2 = 25,

    /// <summary>First gradient color when the close button is checked.</summary>
    FormCloseChecked1 = 26,

    /// <summary>Second gradient color when the close button is checked.</summary>
    FormCloseChecked2 = 27,

    /// <summary>First gradient color when the checked close button is hovered over (tracking).</summary>
    FormCloseCheckedTracking1 = 28,

    /// <summary>Second gradient color when the checked close button is hovered over (tracking).</summary>
    FormCloseCheckedTracking2 = 29,

    /// <summary>Text color used within grid views or spreadsheet-like components.</summary>
    GridText = 30,

    /// <summary>Border color used to highlight today’s date in calendars.</summary>
    TodayBorder = 31,

    /// <summary>First gradient background color for tooltips.</summary>
    ToolTipBack1 = 32,

    /// <summary>Second gradient background color for tooltips.</summary>
    ToolTipBack2 = 33,

    /// <summary>Border color used around tooltips.</summary>
    ToolTipBorder = 34,

    /// <summary>Text color used in tooltips.</summary>
    ToolTipText = 35,
}

#endregion

#region Enumeration: SchemeToolTipColors

/// <summary>
/// Defines color roles used for rendering tooltips.
/// Each value represents a specific color usage within a tooltip.
/// </summary>
public enum SchemeToolTipColors
{
    /// <summary>
    /// Bottom color of a tooltip, typically used for gradient backgrounds.
    /// </summary>
    ToolTipBottom = 0
}

#endregion

#region Enumeration: SchemeContextMenuColors

/// <summary>
/// Defines color roles used for rendering context menus.
/// Each value represents a specific color usage for context menu items or background areas.
/// </summary>
internal enum SchemeContextMenuColors
{
    /// <summary>
    /// Text color for items within a context menu.
    /// </summary>
    MenuItemText = 1,

    /// <summary>
    /// Color for the margin area of a context menu.
    /// </summary>
    ContextMenuMargin = 2,

    /// <summary>
    /// Color for the inner background area of a context menu.
    /// </summary>
    ContextMenuInner = 3
}

#endregion

#region Enumeration: SchemeMenuStripColors

/// <summary>
/// Defines color roles used for rendering menu strips and their items.
/// Each value represents a specific color usage for menu item text or menu margin gradients.
/// </summary>
internal enum SchemeMenuStripColors
{
    /// <summary>
    /// Text color for menu items in a menu strip.
    /// </summary>
    MenuItemText = 1,

    /// <summary>
    /// Gradient start color for the margin area of a menu strip.
    /// </summary>
    MenuMarginGradientStart = 2,

    /// <summary>
    /// Gradient middle color for the margin area of a menu strip.
    /// </summary>
    MenuMarginGradientMiddle = 3,

    /// <summary>
    /// Gradient end color for the margin area of a menu strip.
    /// </summary>
    MenuMarginGradientEnd = 4,

    /// <summary>
    /// Text color for disabled menu items in a menu strip.
    /// </summary>
    DisabledMenuItemText = 5
}

#endregion

#region Enumeration: SchemeTrackingColors

/// <summary>
/// Defines color roles for tracking (hover, selected, pressed, or checked) states
/// in menus and buttons. Each value represents a specific color usage for visual feedback
/// during user interaction, such as highlighting menu items or button states.
/// </summary>
public enum SchemeTrackingColors
{
    /// <summary>
    /// Gradient start color for a selected menu item (e.g., when hovered).
    /// </summary>
    MenuItemSelectedBegin = 0,

    /// <summary>
    /// Gradient end color for a selected menu item (e.g., when hovered).
    /// </summary>
    MenuItemSelectedEnd = 1,

    /// <summary>
    /// Background color for the context menu in a tracking (hover) state.
    /// </summary>
    ContextMenuBackground = 2,

    /// <summary>
    /// Background color for a check mark or checked item in a menu during tracking.
    /// </summary>
    CheckBackground = 3,

    /// <summary>
    /// Gradient start color for a button in the selected (hovered) state.
    /// </summary>
    ButtonSelectedBegin = 4,

    /// <summary>
    /// Gradient end color for a button in the selected (hovered) state.
    /// </summary>
    ButtonSelectedEnd = 5,

    /// <summary>
    /// Gradient start color for a button in the pressed state.
    /// </summary>
    ButtonPressedBegin = 6,

    /// <summary>
    /// Gradient end color for a button in the pressed state.
    /// </summary>
    ButtonPressedEnd = 7,

    /// <summary>
    /// Gradient start color for a button in the checked state.
    /// </summary>
    ButtonCheckedBegin = 8,

    /// <summary>
    /// Gradient end color for a button in the checked state.
    /// </summary>
    ButtonCheckedEnd = 9
}

#endregion