#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Base class for built-in Krypton palette color schemes.
/// Defines the complete set of colors used to render controls—including buttons, panels, ribbon, form chrome, tool strips, and grids—in a consistent theme.
/// Concrete implementations (e.g. Office 2007 Blue, Sparkle, etc.) provide the actual color values for each scheme.
/// </summary>
public abstract class KryptonColorSchemeBase
{
    #region Variables

    /// <summary>Gets or sets the text color for label controls.</summary>
    public abstract Color TextLabelControl                 { get; set; }
    /// <summary>Gets or sets the text color for buttons in normal state.</summary>
    public abstract Color TextButtonNormal                 { get; set; }
    /// <summary>Gets or sets the text color for buttons in checked state.</summary>
    public abstract Color TextButtonChecked                { get; set; }
    /// <summary>Gets or sets the border color for buttons in normal state.</summary>
    public abstract Color ButtonNormalBorder               { get; set; }
    /// <summary>Gets or sets the border color for default (focused) buttons.</summary>
    public abstract Color ButtonNormalDefaultBorder        { get; set; }
    /// <summary>Gets or sets the first gradient color for normal button background.</summary>
    public abstract Color ButtonNormalBack1                { get; set; }
    /// <summary>Gets or sets the second gradient color for normal button background.</summary>
    public abstract Color ButtonNormalBack2                { get; set; }
    /// <summary>Gets or sets the first gradient color for default button background.</summary>
    public abstract Color ButtonNormalDefaultBack1         { get; set; }
    /// <summary>Gets or sets the second gradient color for default button background.</summary>
    public abstract Color ButtonNormalDefaultBack2         { get; set; }
    /// <summary>Gets or sets the first gradient color for navigator button background.</summary>
    public abstract Color ButtonNormalNavigatorBack1       { get; set; }
    /// <summary>Gets or sets the second gradient color for navigator button background.</summary>
    public abstract Color ButtonNormalNavigatorBack2       { get; set; }
    /// <summary>Gets or sets the background color for panel client areas.</summary>
    public abstract Color PanelClient                      { get; set; }
    /// <summary>Gets or sets the background color for alternate (striped) panel areas.</summary>
    public abstract Color PanelAlternative                 { get; set; }
    /// <summary>Gets or sets the border color for controls.</summary>
    public abstract Color ControlBorder                    { get; set; }
    /// <summary>Gets or sets the first high-contrast separator border color.</summary>
    public abstract Color SeparatorHighBorder1             { get; set; }
    /// <summary>Gets or sets the second high-contrast separator border color.</summary>
    public abstract Color SeparatorHighBorder2             { get; set; }
    /// <summary>Gets or sets the first gradient color for primary header background.</summary>
    public abstract Color HeaderPrimaryBack1               { get; set; }
    /// <summary>Gets or sets the second gradient color for primary header background.</summary>
    public abstract Color HeaderPrimaryBack2               { get; set; }
    /// <summary>Gets or sets the first gradient color for secondary header background.</summary>
    public abstract Color HeaderSecondaryBack1             { get; set; }
    /// <summary>Gets or sets the second gradient color for secondary header background.</summary>
    public abstract Color HeaderSecondaryBack2             { get; set; }
    /// <summary>Gets or sets the text color for headers.</summary>
    public abstract Color HeaderText                       { get; set; }
    /// <summary>Gets or sets the text color for status strip items.</summary>
    public abstract Color StatusStripText                  { get; set; }
    /// <summary>Gets or sets the border color for buttons.</summary>
    public abstract Color ButtonBorder                     { get; set; }
    /// <summary>Gets or sets the light color for separators.</summary>
    public abstract Color SeparatorLight                   { get; set; }
    /// <summary>Gets or sets the dark color for separators.</summary>
    public abstract Color SeparatorDark                    { get; set; }
    /// <summary>Gets or sets the light color for resize grips.</summary>
    public abstract Color GripLight                        { get; set; }
    /// <summary>Gets or sets the dark color for resize grips.</summary>
    public abstract Color GripDark                         { get; set; }
    /// <summary>Gets or sets the background color for tool strips.</summary>
    public abstract Color ToolStripBack                    { get; set; }
    /// <summary>Gets or sets the light gradient color for status strip.</summary>
    public abstract Color StatusStripLight                 { get; set; }
    /// <summary>Gets or sets the dark gradient color for status strip.</summary>
    public abstract Color StatusStripDark                  { get; set; }
    /// <summary>Gets or sets the background color for the image margin in menus and tool strips.</summary>
    public abstract Color ImageMargin                      { get; set; }
    /// <summary>Gets or sets the start gradient color for tool strip background.</summary>
    public abstract Color ToolStripBegin                   { get; set; }
    /// <summary>Gets or sets the middle gradient color for tool strip background.</summary>
    public abstract Color ToolStripMiddle                  { get; set; }
    /// <summary>Gets or sets the end gradient color for tool strip background.</summary>
    public abstract Color ToolStripEnd                     { get; set; }
    /// <summary>Gets or sets the start gradient color for overflow button background.</summary>
    public abstract Color OverflowBegin                    { get; set; }
    /// <summary>Gets or sets the middle gradient color for overflow button background.</summary>
    public abstract Color OverflowMiddle                   { get; set; }
    /// <summary>Gets or sets the end gradient color for overflow button background.</summary>
    public abstract Color OverflowEnd                      { get; set; }
    /// <summary>Gets or sets the border color for tool strips.</summary>
    public abstract Color ToolStripBorder                  { get; set; }
    /// <summary>Gets or sets the border color for the form when active (focused).</summary>
    public abstract Color FormBorderActive                 { get; set; }
    /// <summary>Gets or sets the border color for the form when inactive (unfocused).</summary>
    public abstract Color FormBorderInactive               { get; set; }
    /// <summary>Gets or sets the light edge color for active form border.</summary>
    public abstract Color FormBorderActiveLight            { get; set; }
    /// <summary>Gets or sets the dark edge color for active form border.</summary>
    public abstract Color FormBorderActiveDark             { get; set; }
    /// <summary>Gets or sets the light edge color for inactive form border.</summary>
    public abstract Color FormBorderInactiveLight          { get; set; }
    /// <summary>Gets or sets the dark edge color for inactive form border.</summary>
    public abstract Color FormBorderInactiveDark           { get; set; }
    /// <summary>Gets or sets the header border color when the form is active.</summary>
    public abstract Color FormBorderHeaderActive           { get; set; }
    /// <summary>Gets or sets the header border color when the form is inactive.</summary>
    public abstract Color FormBorderHeaderInactive         { get; set; }
    /// <summary>Gets or sets the first gradient color for active form header border.</summary>
    public abstract Color FormBorderHeaderActive1          { get; set; }
    /// <summary>Gets or sets the second gradient color for active form header border.</summary>
    public abstract Color FormBorderHeaderActive2          { get; set; }
    /// <summary>Gets or sets the first gradient color for inactive form header border.</summary>
    public abstract Color FormBorderHeaderInactive1        { get; set; }
    /// <summary>Gets or sets the second gradient color for inactive form header border.</summary>
    public abstract Color FormBorderHeaderInactive2        { get; set; }
    /// <summary>Gets or sets the short header text color when the form is active.</summary>
    public abstract Color FormHeaderShortActive            { get; set; }
    /// <summary>Gets or sets the short header text color when the form is inactive.</summary>
    public abstract Color FormHeaderShortInactive          { get; set; }
    /// <summary>Gets or sets the long header text color when the form is active.</summary>
    public abstract Color FormHeaderLongActive             { get; set; }
    /// <summary>Gets or sets the long header text color when the form is inactive.</summary>
    public abstract Color FormHeaderLongInactive           { get; set; }
    /// <summary>Gets or sets the border color for form caption buttons in tracking (hover) state.</summary>
    public abstract Color FormButtonBorderTrack            { get; set; }
    /// <summary>Gets or sets the first gradient color for form caption button background when tracking.</summary>
    public abstract Color FormButtonBack1Track             { get; set; }
    /// <summary>Gets or sets the second gradient color for form caption button background when tracking.</summary>
    public abstract Color FormButtonBack2Track             { get; set; }
    /// <summary>Gets or sets the border color for form caption buttons when pressed.</summary>
    public abstract Color FormButtonBorderPressed          { get; set; }
    /// <summary>Gets or sets the first gradient color for form caption button background when pressed.</summary>
    public abstract Color FormButtonBack1Pressed           { get; set; }
    /// <summary>Gets or sets the second gradient color for form caption button background when pressed.</summary>
    public abstract Color FormButtonBack2Pressed           { get; set; }
    /// <summary>Gets or sets the text color for form caption buttons in normal state.</summary>
    public abstract Color TextButtonFormNormal             { get; set; }
    /// <summary>Gets or sets the text color for form caption buttons in tracking state.</summary>
    public abstract Color TextButtonFormTracking           { get; set; }
    /// <summary>Gets or sets the text color for form caption buttons when pressed.</summary>
    public abstract Color TextButtonFormPressed            { get; set; }
    /// <summary>Gets or sets the link text color for unvisited links on controls.</summary>
    public abstract Color LinkNotVisitedOverrideControl    { get; set; }
    /// <summary>Gets or sets the link text color for visited links on controls.</summary>
    public abstract Color LinkVisitedOverrideControl       { get; set; }
    /// <summary>Gets or sets the link text color for pressed links on controls.</summary>
    public abstract Color LinkPressedOverrideControl       { get; set; }
    /// <summary>Gets or sets the link text color for unvisited links on panels.</summary>
    public abstract Color LinkNotVisitedOverridePanel      { get; set; }
    /// <summary>Gets or sets the link text color for visited links on panels.</summary>
    public abstract Color LinkVisitedOverridePanel         { get; set; }
    /// <summary>Gets or sets the link text color for pressed links on panels.</summary>
    public abstract Color LinkPressedOverridePanel         { get; set; }
    /// <summary>Gets or sets the text color for labels on panels.</summary>
    public abstract Color TextLabelPanel                   { get; set; }
    /// <summary>Gets or sets the text color for ribbon tabs in normal state.</summary>
    public abstract Color RibbonTabTextNormal              { get; set; }
    /// <summary>Gets or sets the text color for ribbon tabs in checked (selected) state.</summary>
    public abstract Color RibbonTabTextChecked             { get; set; }
    /// <summary>Gets or sets the first gradient color for selected ribbon tab.</summary>
    public abstract Color RibbonTabSelected1               { get; set; }
    /// <summary>Gets or sets the second gradient color for selected ribbon tab.</summary>
    public abstract Color RibbonTabSelected2               { get; set; }
    /// <summary>Gets or sets the third gradient color for selected ribbon tab.</summary>
    public abstract Color RibbonTabSelected3               { get; set; }
    /// <summary>Gets or sets the fourth gradient color for selected ribbon tab.</summary>
    public abstract Color RibbonTabSelected4               { get; set; }
    /// <summary>Gets or sets the fifth gradient color for selected ribbon tab.</summary>
    public abstract Color RibbonTabSelected5               { get; set; }
    /// <summary>Gets or sets the first gradient color for ribbon tab in tracking (hover) state.</summary>
    public abstract Color RibbonTabTracking1               { get; set; }
    /// <summary>Gets or sets the second gradient color for ribbon tab in tracking state.</summary>
    public abstract Color RibbonTabTracking2               { get; set; }
    /// <summary>Gets or sets the first gradient color for ribbon tab highlight.</summary>
    public abstract Color RibbonTabHighlight1              { get; set; }
    /// <summary>Gets or sets the second gradient color for ribbon tab highlight.</summary>
    public abstract Color RibbonTabHighlight2              { get; set; }
    /// <summary>Gets or sets the third gradient color for ribbon tab highlight.</summary>
    public abstract Color RibbonTabHighlight3              { get; set; }
    /// <summary>Gets or sets the fourth gradient color for ribbon tab highlight.</summary>
    public abstract Color RibbonTabHighlight4              { get; set; }
    /// <summary>Gets or sets the fifth gradient color for ribbon tab highlight.</summary>
    public abstract Color RibbonTabHighlight5              { get; set; }
    /// <summary>Gets or sets the color of separators between ribbon tabs.</summary>
    public abstract Color RibbonTabSeparatorColor          { get; set; }
    /// <summary>Gets or sets the first gradient color for ribbon groups area background.</summary>
    public abstract Color RibbonGroupsArea1                { get; set; }
    /// <summary>Gets or sets the second gradient color for ribbon groups area background.</summary>
    public abstract Color RibbonGroupsArea2                { get; set; }
    /// <summary>Gets or sets the third gradient color for ribbon groups area background.</summary>
    public abstract Color RibbonGroupsArea3                { get; set; }
    /// <summary>Gets or sets the fourth gradient color for ribbon groups area background.</summary>
    public abstract Color RibbonGroupsArea4                { get; set; }
    /// <summary>Gets or sets the fifth gradient color for ribbon groups area background.</summary>
    public abstract Color RibbonGroupsArea5                { get; set; }
    /// <summary>Gets or sets the first gradient color for ribbon group border.</summary>
    public abstract Color RibbonGroupBorder1               { get; set; }
    /// <summary>Gets or sets the second gradient color for ribbon group border.</summary>
    public abstract Color RibbonGroupBorder2               { get; set; }
    /// <summary>Gets or sets the first gradient color for ribbon group title background.</summary>
    public abstract Color RibbonGroupTitle1                { get; set; }
    /// <summary>Gets or sets the second gradient color for ribbon group title background.</summary>
    public abstract Color RibbonGroupTitle2                { get; set; }
    /// <summary>Gets or sets the first gradient color for context ribbon group border.</summary>
    public abstract Color RibbonGroupBorderContext1        { get; set; }
    /// <summary>Gets or sets the second gradient color for context ribbon group border.</summary>
    public abstract Color RibbonGroupBorderContext2        { get; set; }
    /// <summary>Gets or sets the first gradient color for context ribbon group title.</summary>
    public abstract Color RibbonGroupTitleContext1         { get; set; }
    /// <summary>Gets or sets the second gradient color for context ribbon group title.</summary>
    public abstract Color RibbonGroupTitleContext2         { get; set; }
    /// <summary>Gets or sets the dark edge color for ribbon group dialog areas.</summary>
    public abstract Color RibbonGroupDialogDark            { get; set; }
    /// <summary>Gets or sets the light edge color for ribbon group dialog areas.</summary>
    public abstract Color RibbonGroupDialogLight           { get; set; }
    /// <summary>Gets or sets the first gradient color for ribbon group title in tracking state.</summary>
    public abstract Color RibbonGroupTitleTracking1        { get; set; }
    /// <summary>Gets or sets the second gradient color for ribbon group title in tracking state.</summary>
    public abstract Color RibbonGroupTitleTracking2        { get; set; }
    /// <summary>Gets or sets the dark gradient color for ribbon minimize bar.</summary>
    public abstract Color RibbonMinimizeBarDark            { get; set; }
    /// <summary>Gets or sets the light gradient color for ribbon minimize bar.</summary>
    public abstract Color RibbonMinimizeBarLight           { get; set; }
    /// <summary>Gets or sets the first gradient color for collapsed ribbon group border.</summary>
    public abstract Color RibbonGroupCollapsedBorder1      { get; set; }
    /// <summary>Gets or sets the second gradient color for collapsed ribbon group border.</summary>
    public abstract Color RibbonGroupCollapsedBorder2      { get; set; }
    /// <summary>Gets or sets the third gradient color for collapsed ribbon group border.</summary>
    public abstract Color RibbonGroupCollapsedBorder3      { get; set; }
    /// <summary>Gets or sets the fourth gradient color for collapsed ribbon group border.</summary>
    public abstract Color RibbonGroupCollapsedBorder4      { get; set; }
    /// <summary>Gets or sets the first gradient color for collapsed ribbon group background.</summary>
    public abstract Color RibbonGroupCollapsedBack1        { get; set; }
    /// <summary>Gets or sets the second gradient color for collapsed ribbon group background.</summary>
    public abstract Color RibbonGroupCollapsedBack2        { get; set; }
    /// <summary>Gets or sets the third gradient color for collapsed ribbon group background.</summary>
    public abstract Color RibbonGroupCollapsedBack3        { get; set; }
    /// <summary>Gets or sets the fourth gradient color for collapsed ribbon group background.</summary>
    public abstract Color RibbonGroupCollapsedBack4        { get; set; }
    /// <summary>Gets or sets the first gradient color for collapsed ribbon group border in tracking state.</summary>
    public abstract Color RibbonGroupCollapsedBorderT1     { get; set; }
    /// <summary>Gets or sets the second gradient color for collapsed ribbon group border in tracking state.</summary>
    public abstract Color RibbonGroupCollapsedBorderT2     { get; set; }
    /// <summary>Gets or sets the third gradient color for collapsed ribbon group border in tracking state.</summary>
    public abstract Color RibbonGroupCollapsedBorderT3     { get; set; }
    /// <summary>Gets or sets the fourth gradient color for collapsed ribbon group border in tracking state.</summary>
    public abstract Color RibbonGroupCollapsedBorderT4     { get; set; }
    /// <summary>Gets or sets the first gradient color for collapsed ribbon group background in tracking state.</summary>
    public abstract Color RibbonGroupCollapsedBackT1       { get; set; }
    /// <summary>Gets or sets the second gradient color for collapsed ribbon group background in tracking state.</summary>
    public abstract Color RibbonGroupCollapsedBackT2       { get; set; }
    /// <summary>Gets or sets the third gradient color for collapsed ribbon group background in tracking state.</summary>
    public abstract Color RibbonGroupCollapsedBackT3       { get; set; }
    /// <summary>Gets or sets the fourth gradient color for collapsed ribbon group background in tracking state.</summary>
    public abstract Color RibbonGroupCollapsedBackT4       { get; set; }
    /// <summary>Gets or sets the first gradient color for ribbon group frame border.</summary>
    public abstract Color RibbonGroupFrameBorder1          { get; set; }
    /// <summary>Gets or sets the second gradient color for ribbon group frame border.</summary>
    public abstract Color RibbonGroupFrameBorder2          { get; set; }
    /// <summary>Gets or sets the first gradient color for ribbon group frame inner area.</summary>
    public abstract Color RibbonGroupFrameInside1          { get; set; }
    /// <summary>Gets or sets the second gradient color for ribbon group frame inner area.</summary>
    public abstract Color RibbonGroupFrameInside2          { get; set; }
    /// <summary>Gets or sets the third gradient color for ribbon group frame inner area.</summary>
    public abstract Color RibbonGroupFrameInside3          { get; set; }
    /// <summary>Gets or sets the fourth gradient color for ribbon group frame inner area.</summary>
    public abstract Color RibbonGroupFrameInside4          { get; set; }
    /// <summary>Gets or sets the text color for collapsed ribbon group labels.</summary>
    public abstract Color RibbonGroupCollapsedText         { get; set; }
    /// <summary>Gets or sets the text color for ribbon group buttons.</summary>
    public abstract Color RibbonGroupButtonText            { get; set; }
    /// <summary>Gets or sets the first gradient color for alternate (secondary) pressed button background.</summary>
    public abstract Color AlternatePressedBack1            { get; set; }
    /// <summary>Gets or sets the second gradient color for alternate pressed button background.</summary>
    public abstract Color AlternatePressedBack2            { get; set; }
    /// <summary>Gets or sets the first gradient color for alternate pressed button border.</summary>
    public abstract Color AlternatePressedBorder1          { get; set; }
    /// <summary>Gets or sets the second gradient color for alternate pressed button border.</summary>
    public abstract Color AlternatePressedBorder2          { get; set; }
    /// <summary>Gets or sets the first gradient color for form caption button background when checked.</summary>
    public abstract Color FormButtonBack1Checked           { get; set; }
    /// <summary>Gets or sets the second gradient color for form caption button background when checked.</summary>
    public abstract Color FormButtonBack2Checked           { get; set; }
    /// <summary>Gets or sets the border color for form caption check (close) button.</summary>
    public abstract Color FormButtonBorderCheck            { get; set; }
    /// <summary>Gets or sets the first gradient color for form caption check button background when tracking.</summary>
    public abstract Color FormButtonBack1CheckTrack        { get; set; }
    /// <summary>Gets or sets the second gradient color for form caption check button background when tracking.</summary>
    public abstract Color FormButtonBack2CheckTrack        { get; set; }
    /// <summary>Gets or sets the first gradient color for ribbon Quick Access Toolbar in mini mode.</summary>
    public abstract Color RibbonQATMini1                   { get; set; }
    /// <summary>Gets or sets the second gradient color for ribbon QAT in mini mode.</summary>
    public abstract Color RibbonQATMini2                   { get; set; }
    /// <summary>Gets or sets the third gradient color for ribbon QAT in mini mode.</summary>
    public abstract Color RibbonQATMini3                   { get; set; }
    /// <summary>Gets or sets the fourth gradient color for ribbon QAT in mini mode.</summary>
    public abstract Color RibbonQATMini4                   { get; set; }
    /// <summary>Gets or sets the fifth gradient color for ribbon QAT in mini mode.</summary>
    public abstract Color RibbonQATMini5                   { get; set; }
    /// <summary>Gets or sets the first gradient color for ribbon QAT mini inner area.</summary>
    public abstract Color RibbonQATMini1I                  { get; set; }
    /// <summary>Gets or sets the second gradient color for ribbon QAT mini inner area.</summary>
    public abstract Color RibbonQATMini2I                  { get; set; }
    /// <summary>Gets or sets the third gradient color for ribbon QAT mini inner area.</summary>
    public abstract Color RibbonQATMini3I                  { get; set; }
    /// <summary>Gets or sets the fourth gradient color for ribbon QAT mini inner area.</summary>
    public abstract Color RibbonQATMini4I                  { get; set; }
    /// <summary>Gets or sets the fifth gradient color for ribbon QAT mini inner area.</summary>
    public abstract Color RibbonQATMini5I                  { get; set; }
    /// <summary>Gets or sets the first gradient color for ribbon QAT full bar.</summary>
    public abstract Color RibbonQATFullbar1                { get; set; }
    /// <summary>Gets or sets the second gradient color for ribbon QAT full bar.</summary>
    public abstract Color RibbonQATFullbar2                { get; set; }
    /// <summary>Gets or sets the third gradient color for ribbon QAT full bar.</summary>
    public abstract Color RibbonQATFullbar3                { get; set; }
    /// <summary>Gets or sets the dark edge color for ribbon QAT buttons.</summary>
    public abstract Color RibbonQATButtonDark              { get; set; }
    /// <summary>Gets or sets the light edge color for ribbon QAT buttons.</summary>
    public abstract Color RibbonQATButtonLight             { get; set; }
    /// <summary>Gets or sets the first gradient color for ribbon QAT overflow area.</summary>
    public abstract Color RibbonQATOverflow1               { get; set; }
    /// <summary>Gets or sets the second gradient color for ribbon QAT overflow area.</summary>
    public abstract Color RibbonQATOverflow2               { get; set; }
    /// <summary>Gets or sets the dark color for separators between ribbon groups.</summary>
    public abstract Color RibbonGroupSeparatorDark         { get; set; }
    /// <summary>Gets or sets the light color for separators between ribbon groups.</summary>
    public abstract Color RibbonGroupSeparatorLight        { get; set; }
    /// <summary>Gets or sets the first gradient color for button cluster background.</summary>
    public abstract Color ButtonClusterButtonBack1         { get; set; }
    /// <summary>Gets or sets the second gradient color for button cluster background.</summary>
    public abstract Color ButtonClusterButtonBack2         { get; set; }
    /// <summary>Gets or sets the first gradient color for button cluster border.</summary>
    public abstract Color ButtonClusterButtonBorder1       { get; set; }
    /// <summary>Gets or sets the second gradient color for button cluster border.</summary>
    public abstract Color ButtonClusterButtonBorder2       { get; set; }
    /// <summary>Gets or sets the background color for the navigator mini (collapsed) area.</summary>
    public abstract Color NavigatorMiniBackColor           { get; set; }
    /// <summary>Gets or sets the first gradient color for grid list items in normal state.</summary>
    public abstract Color GridListNormal1                  { get; set; }
    /// <summary>Gets or sets the second gradient color for grid list items in normal state.</summary>
    public abstract Color GridListNormal2                  { get; set; }
    /// <summary>Gets or sets the first gradient color for grid list items when pressed.</summary>
    public abstract Color GridListPressed1                 { get; set; }
    /// <summary>Gets or sets the second gradient color for grid list items when pressed.</summary>
    public abstract Color GridListPressed2                 { get; set; }
    /// <summary>Gets or sets the background color for selected grid list items.</summary>
    public abstract Color GridListSelected                 { get; set; }
    /// <summary>Gets or sets the first gradient color for grid sheet column headers in normal state.</summary>
    public abstract Color GridSheetColNormal1              { get; set; }
    /// <summary>Gets or sets the second gradient color for grid sheet column headers in normal state.</summary>
    public abstract Color GridSheetColNormal2              { get; set; }
    /// <summary>Gets or sets the first gradient color for grid sheet column headers when pressed.</summary>
    public abstract Color GridSheetColPressed1             { get; set; }
    /// <summary>Gets or sets the second gradient color for grid sheet column headers when pressed.</summary>
    public abstract Color GridSheetColPressed2             { get; set; }
    /// <summary>Gets or sets the first gradient color for selected grid sheet column headers.</summary>
    public abstract Color GridSheetColSelected1            { get; set; }
    /// <summary>Gets or sets the second gradient color for selected grid sheet column headers.</summary>
    public abstract Color GridSheetColSelected2            { get; set; }
    /// <summary>Gets or sets the background color for grid sheet row headers in normal state.</summary>
    public abstract Color GridSheetRowNormal               { get; set; }
    /// <summary>Gets or sets the background color for grid sheet row headers when pressed.</summary>
    public abstract Color GridSheetRowPressed              { get; set; }
    /// <summary>Gets or sets the background color for selected grid sheet row headers.</summary>
    public abstract Color GridSheetRowSelected             { get; set; }
    /// <summary>Gets or sets the border color for grid data cells.</summary>
    public abstract Color GridDataCellBorder               { get; set; }
    /// <summary>Gets or sets the background color for selected grid data cells.</summary>
    public abstract Color GridDataCellSelected             { get; set; }
    /// <summary>Gets or sets the text color for input controls in normal state.</summary>
    public abstract Color InputControlTextNormal           { get; set; }
    /// <summary>Gets or sets the text color for disabled input controls.</summary>
    public abstract Color InputControlTextDisabled         { get; set; }
    /// <summary>Gets or sets the border color for input controls in normal state.</summary>
    public abstract Color InputControlBorderNormal         { get; set; }
    /// <summary>Gets or sets the border color for disabled input controls.</summary>
    public abstract Color InputControlBorderDisabled       { get; set; }
    /// <summary>Gets or sets the background color for input controls in normal state.</summary>
    public abstract Color InputControlBackNormal           { get; set; }
    /// <summary>Gets or sets the background color for disabled input controls.</summary>
    public abstract Color InputControlBackDisabled         { get; set; }
    /// <summary>Gets or sets the background color for inactive (unfocused) input controls.</summary>
    public abstract Color InputControlBackInactive         { get; set; }
    /// <summary>Gets or sets the first gradient color for dropdown button in normal state.</summary>
    public abstract Color InputDropDownNormal1             { get; set; }
    /// <summary>Gets or sets the second gradient color for dropdown button in normal state.</summary>
    public abstract Color InputDropDownNormal2             { get; set; }
    /// <summary>Gets or sets the first gradient color for dropdown button when disabled.</summary>
    public abstract Color InputDropDownDisabled1           { get; set; }
    /// <summary>Gets or sets the second gradient color for dropdown button when disabled.</summary>
    public abstract Color InputDropDownDisabled2           { get; set; }
    /// <summary>Gets or sets the background color for context menu section headings.</summary>
    public abstract Color ContextMenuHeadingBack           { get; set; }
    /// <summary>Gets or sets the text color for context menu section headings.</summary>
    public abstract Color ContextMenuHeadingText           { get; set; }
    /// <summary>Gets or sets the background color for the image column in context menus.</summary>
    public abstract Color ContextMenuImageColumn           { get; set; }
    /// <summary>Gets or sets the first gradient color for application button background.</summary>
    public abstract Color AppButtonBack1                   { get; set; }
    /// <summary>Gets or sets the second gradient color for application button background.</summary>
    public abstract Color AppButtonBack2                   { get; set; }
    /// <summary>Gets or sets the border color for application button.</summary>
    public abstract Color AppButtonBorder                  { get; set; }
    /// <summary>Gets or sets the first outer gradient color for application button.</summary>
    public abstract Color AppButtonOuter1                  { get; set; }
    /// <summary>Gets or sets the second outer gradient color for application button.</summary>
    public abstract Color AppButtonOuter2                  { get; set; }
    /// <summary>Gets or sets the third outer gradient color for application button.</summary>
    public abstract Color AppButtonOuter3                  { get; set; }
    /// <summary>Gets or sets the first inner gradient color for application button.</summary>
    public abstract Color AppButtonInner1                  { get; set; }
    /// <summary>Gets or sets the second inner gradient color for application button.</summary>
    public abstract Color AppButtonInner2                  { get; set; }
    /// <summary>Gets or sets the background color for application button recent documents area.</summary>
    public abstract Color AppButtonMenuDocsBack            { get; set; }
    /// <summary>Gets or sets the text color for application button recent documents.</summary>
    public abstract Color AppButtonMenuDocsText            { get; set; }
    /// <summary>Gets or sets the first high-contrast internal separator border color.</summary>
    public abstract Color SeparatorHighInternalBorder1     { get; set; }
    /// <summary>Gets or sets the second high-contrast internal separator border color.</summary>
    public abstract Color SeparatorHighInternalBorder2     { get; set; }
    /// <summary>Gets or sets the border color for ribbon gallery controls.</summary>
    public abstract Color RibbonGalleryBorder              { get; set; }
    /// <summary>Gets or sets the background color for ribbon gallery in normal state.</summary>
    public abstract Color RibbonGalleryBackNormal          { get; set; }
    /// <summary>Gets or sets the background color for ribbon gallery in tracking state.</summary>
    public abstract Color RibbonGalleryBackTracking        { get; set; }
    /// <summary>Gets or sets the first gradient color for ribbon gallery background.</summary>
    public abstract Color RibbonGalleryBack1               { get; set; }
    /// <summary>Gets or sets the second gradient color for ribbon gallery background.</summary>
    public abstract Color RibbonGalleryBack2               { get; set; }
    /// <summary>Gets or sets the third gradient color for ribbon tab in tracking state.</summary>
    public abstract Color RibbonTabTracking3               { get; set; }
    /// <summary>Gets or sets the fourth gradient color for ribbon tab in tracking state.</summary>
    public abstract Color RibbonTabTracking4               { get; set; }
    /// <summary>Gets or sets the third gradient color for ribbon group border.</summary>
    public abstract Color RibbonGroupBorder3               { get; set; }
    /// <summary>Gets or sets the fourth gradient color for ribbon group border.</summary>
    public abstract Color RibbonGroupBorder4               { get; set; }
    /// <summary>Gets or sets the fifth gradient color for ribbon group border.</summary>
    public abstract Color RibbonGroupBorder5               { get; set; }
    /// <summary>Gets or sets the text color for ribbon group titles.</summary>
    public abstract Color RibbonGroupTitleText             { get; set; }
    /// <summary>Gets or sets the light color for ribbon drop-down arrows.</summary>
    public abstract Color RibbonDropArrowLight             { get; set; }
    /// <summary>Gets or sets the dark color for ribbon drop-down arrows.</summary>
    public abstract Color RibbonDropArrowDark              { get; set; }
    /// <summary>Gets or sets the first gradient color for inactive dock header background.</summary>
    public abstract Color HeaderDockInactiveBack1          { get; set; }
    /// <summary>Gets or sets the second gradient color for inactive dock header background.</summary>
    public abstract Color HeaderDockInactiveBack2          { get; set; }
    /// <summary>Gets or sets the border color for navigator buttons.</summary>
    public abstract Color ButtonNavigatorBorder            { get; set; }
    /// <summary>Gets or sets the text color for navigator buttons.</summary>
    public abstract Color ButtonNavigatorText              { get; set; }
    /// <summary>Gets or sets the first gradient color for navigator button background in tracking state.</summary>
    public abstract Color ButtonNavigatorTrack1            { get; set; }
    /// <summary>Gets or sets the second gradient color for navigator button background in tracking state.</summary>
    public abstract Color ButtonNavigatorTrack2            { get; set; }
    /// <summary>Gets or sets the first gradient color for navigator button background when pressed.</summary>
    public abstract Color ButtonNavigatorPressed1          { get; set; }
    /// <summary>Gets or sets the second gradient color for navigator button background when pressed.</summary>
    public abstract Color ButtonNavigatorPressed2          { get; set; }
    /// <summary>Gets or sets the first gradient color for navigator button background when checked.</summary>
    public abstract Color ButtonNavigatorChecked1         { get; set; }
    /// <summary>Gets or sets the second gradient color for navigator button background when checked.</summary>
    public abstract Color ButtonNavigatorChecked2         { get; set; }
    /// <summary>Gets or sets the bottom edge color for tooltip background.</summary>
    public abstract Color ToolTipBottom                    { get; set; }
    /// <summary>Gets or sets the text color for menu items.</summary>
    public abstract Color MenuItemText                     { get; set; }
    /// <summary>Gets or sets the start color for menu margin gradient.</summary>
    public abstract Color MenuMarginGradientStart          { get; set; }
    /// <summary>Gets or sets the middle color for menu margin gradient.</summary>
    public abstract Color MenuMarginGradientMiddle         { get; set; }
    /// <summary>Gets or sets the end color for menu margin gradient.</summary>
    public abstract Color MenuMarginGradientEnd            { get; set; }
    /// <summary>Gets or sets the text color for disabled menu items.</summary>
    public abstract Color DisabledMenuItemText             { get; set; }
    /// <summary>Gets or sets the text color for menu strip items.</summary>
    public abstract Color MenuStripText                    { get; set; }
    /// <summary>Gets or sets the color for track bar tick marks.</summary>
    public abstract Color TrackBarTickMarks                 { get; set; }
    /// <summary>Gets or sets the color for the top portion of the track bar.</summary>
    public abstract Color TrackBarTopTrack                 { get; set; }
    /// <summary>Gets or sets the color for the bottom portion of the track bar.</summary>
    public abstract Color TrackBarBottomTrack              { get; set; }
    /// <summary>Gets or sets the fill color for the track bar thumb track.</summary>
    public abstract Color TrackBarFillTrack                 { get; set; }
    /// <summary>Gets or sets the color for the track bar area outside the current position.</summary>
    public abstract Color TrackBarOutsidePosition          { get; set; }
    /// <summary>Gets or sets the border color for the track bar position indicator.</summary>
    public abstract Color TrackBarBorderPosition           { get; set; }

    #endregion
}
