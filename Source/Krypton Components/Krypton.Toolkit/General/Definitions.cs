#region BSD License
/*
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Gidua, Ahmed Abdelhameed, tobitege et al. 2017 - 2026. All rights reserved.
 */
#endregion

// ReSharper disable EventNeverSubscribedTo.Global
// ReSharper disable UnusedMemberInSuper.Global

namespace Krypton.Toolkit;

/*
 * Core definitions file for the Krypton Toolkit containing interfaces, enums, and type definitions
 * used throughout the Krypton UI component library.
 * 
 * This file contains:
 *  - Core interfaces for content values, button specifications, and context menu providers
 *  - Enumerations for UI states, orientations, styles, and behaviors
 *  - Type definitions for palette states, button styles, and layout specifications
 *  - Constants and enumerations for message boxes, icons, and theme types
 * 
 *  The definitions in this file provide the foundational types and contracts that enable
 *  the flexible theming, styling, and behavior customization capabilities of the Krypton Toolkit.
 */

#region IContentValues
/// <summary>
/// Defines the contract for providing content values including images, text, and styling information.
/// This interface is used by UI elements that need to display content with support for different states.
/// </summary>
public interface IContentValues
{
    /// <summary>
    /// Gets the content image.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Image value.</returns>
    Image? GetImage(PaletteState state);

    /// <summary>
    /// Gets the image color that should be transparent.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Color value.</returns>
    Color GetImageTransparentColor(PaletteState state);

    /// <summary>
    /// Gets the content short text.
    /// </summary>
    /// <returns>String value.</returns>
    string GetShortText();

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    /// <returns>String value.</returns>
    string GetLongText();

    /// <summary>
    /// Gets the overlay image.
    /// </summary>
    /// <param name="state">The state for which the overlay image is needed.</param>
    /// <returns>Overlay image value, or null if no overlay image is set.</returns>
    Image? GetOverlayImage(PaletteState state);

    /// <summary>
    /// Gets the overlay image color that should be transparent.
    /// </summary>
    /// <param name="state">The state for which the overlay image is needed.</param>
    /// <returns>Color value.</returns>
    Color GetOverlayImageTransparentColor(PaletteState state);

    /// <summary>
    /// Gets the position of the overlay image relative to the main image.
    /// </summary>
    /// <param name="state">The state for which the overlay position is needed.</param>
    /// <returns>Overlay image position.</returns>
    OverlayImagePosition GetOverlayImagePosition(PaletteState state);

    /// <summary>
    /// Gets the scaling mode for the overlay image.
    /// </summary>
    /// <param name="state">The state for which the overlay scale mode is needed.</param>
    /// <returns>Overlay image scale mode.</returns>
    OverlayImageScaleMode GetOverlayImageScaleMode(PaletteState state);

    /// <summary>
    /// Gets the scale factor for the overlay image (used when scale mode is Percentage or ProportionalToMain).
    /// </summary>
    /// <param name="state">The state for which the overlay scale factor is needed.</param>
    /// <returns>Scale factor (0.0 to 2.0).</returns>
    float GetOverlayImageScaleFactor(PaletteState state);

    /// <summary>
    /// Gets the fixed size for the overlay image (used when scale mode is FixedSize).
    /// </summary>
    /// <param name="state">The state for which the overlay fixed size is needed.</param>
    /// <returns>Fixed size for the overlay image.</returns>
    Size GetOverlayImageFixedSize(PaletteState state);
}
#endregion

#region IButtonSpecValues
/// <summary>
/// Defines the contract for providing button specification values including appearance, behavior, and state information.
/// This interface is used by button specifications to provide dynamic content and styling based on palette and state.
/// </summary>
public interface IButtonSpecValues
{
    /// <summary>
    /// Occurs when a button spec property has changed.
    /// </summary>
    event PropertyChangedEventHandler? ButtonSpecPropertyChanged;

    /// <summary>
    /// Gets the button image.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <param name="state">State for which an image is needed.</param>
    /// <returns>Button image.</returns>
    Image? GetImage(PaletteBase? palette, PaletteState state);

    /// <summary>
    /// Gets the button image transparent color.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Color value.</returns>
    Color GetImageTransparentColor(PaletteBase? palette);

    /// <summary>
    /// Gets the button short text.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Short text string.</returns>
    string GetShortText(PaletteBase? palette);

    /// <summary>
    /// Gets the button long text.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Long text string.</returns>
    string GetLongText(PaletteBase? palette);

    /// <summary>
    /// Gets the button tooltip title text.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Tooltip title string.</returns>
    string GetToolTipTitle(PaletteBase palette);

    /// <summary>
    /// Gets and image color to remap to container foreground.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Color value.</returns>
    Color GetColorMap(PaletteBase? palette);

    /// <summary>
    /// Gets the button visibility.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button visibility value.</returns>
    bool GetVisible(PaletteBase palette);

    /// <summary>
    /// Gets the button enabled state.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button enabled value.</returns>
    ButtonEnabled GetEnabled(PaletteBase palette);

    /// <summary>
    /// Sets the current view associated with the button spec.
    /// </summary>
    /// <param name="view">View element reference.</param>
    void SetView(ViewBase view);

    /// <summary>
    /// Get the current view associated with the button spec.
    /// </summary>
    /// <returns>View element reference.</returns>
    ViewBase GetView();

    /// <summary>
    /// Gets a value indicating if the associated view is enabled.
    /// </summary>
    /// <returns>True if enabled; otherwise false.</returns>
    bool GetViewEnabled();

    /// <summary>
    /// Gets the button edge alignment.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button edge value.</returns>
    RelativeEdgeAlign GetEdge(PaletteBase? palette);

    /// <summary>
    /// Gets the button style.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button style value.</returns>
    ButtonStyle GetStyle(PaletteBase palette);

    /// <summary>
    /// Gets the button location value.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button location.</returns>
    HeaderLocation GetLocation(PaletteBase? palette);

    /// <summary>
    /// Gets the button orientation.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Orientation value.</returns>
    ButtonOrientation GetOrientation(PaletteBase? palette);
}
#endregion

#region IContextMenuProvider
/// <summary>
/// Defines the contract for context menu providers that manage context menu lifecycle and events.
/// This interface is used by controls that can display context menus and need to manage their creation, display, and disposal.
/// </summary>
public interface IContextMenuProvider
{
    /// <summary>
    /// Raises the Dispose event.
    /// </summary>
    event EventHandler? Dispose;

    /// <summary>
    /// Raises the Closing event.
    /// </summary>
    event CancelEventHandler? Closing;

    /// <summary>
    /// Raises the Close event.
    /// </summary>
    event EventHandler<CloseReasonEventArgs>? Close;

    /// <summary>
    /// Fires the Closing event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    void OnDispose(EventArgs e);

    /// <summary>
    /// Fires the Closing event.
    /// </summary>
    /// <param name="cea">A CancelEventArgs containing the event data.</param>
    void OnClosing(CancelEventArgs cea);

    /// <summary>
    /// Fires the Close event.
    /// </summary>
    /// <param name="e">An CloseReasonMenuArgs containing the event data.</param>
    void OnClose(CloseReasonEventArgs e);

    /// <summary>
    /// Does this provider have a parent provider.
    /// </summary>
    bool HasParentProvider { get; }

    /// <summary>
    /// Is the entire context menu enabled.
    /// </summary>
    bool ProviderEnabled { get; }

    /// <summary>
    /// Is context menu capable of being closed.
    /// </summary>
    bool ProviderCanCloseMenu { get; }

    /// <summary>
    /// Should the sub menu be shown at fixed screen location for this menu item.
    /// </summary>
    /// <param name="menuItem">Menu item that needs to show sub menu.</param>
    /// <returns>True if the sub menu should be a fixed size.</returns>
    bool ProviderShowSubMenuFixed(KryptonContextMenuItem menuItem);

    /// <summary>
    /// Should the sub menu be shown at fixed screen location for this menu item.
    /// </summary>
    /// <param name="menuItem">Menu item that needs to show sub menu.</param>
    /// <returns>Screen rectangle to use as display rectangle.</returns>
    Rectangle ProviderShowSubMenuFixedRect(KryptonContextMenuItem menuItem);

    /// <summary>
    /// Sets the reason for the context menu being closed.
    /// </summary>
    ToolStripDropDownCloseReason? ProviderCloseReason { get; set; }

    /// <summary>
    /// Gets and sets the horizontal setting used to position the menu.
    /// </summary>
    KryptonContextMenuPositionH ProviderShowHorz { get; set; }

    /// <summary>
    /// Gets and sets the vertical setting used to position the menu.
    /// </summary>
    KryptonContextMenuPositionV ProviderShowVert { get; set; }

    /// <summary>
    /// Gets access to the layout for context menu columns.
    /// </summary>
    ViewLayoutStack ProviderViewColumns { get; }

    /// <summary>
    /// Gets access to the context menu specific view manager.
    /// </summary>
    ViewContextMenuManager ProviderViewManager { get; }

    /// <summary>
    /// Gets access to the context menu common state.
    /// </summary>
    PaletteContextMenuRedirect ProviderStateCommon { get; }

    /// <summary>
    /// Gets access to the context menu disabled state.
    /// </summary>
    PaletteContextMenuItemState ProviderStateDisabled { get; }

    /// <summary>
    /// Gets access to the context menu normal state.
    /// </summary>
    PaletteContextMenuItemState ProviderStateNormal { get; }

    /// <summary>
    /// Gets access to the context menu highlight state.
    /// </summary>
    PaletteContextMenuItemStateHighlight ProviderStateHighlight { get; }

    /// <summary>
    /// Gets access to the context menu checked state.
    /// </summary>
    PaletteContextMenuItemStateChecked ProviderStateChecked { get; }

    /// <summary>
    /// Gets access to the context menu images.
    /// </summary>
    PaletteRedirectContextMenu ProviderImages { get; }

    /// <summary>
    /// Gets access to the custom palette.
    /// </summary>
    PaletteBase? ProviderPalette { get; }

    /// <summary>
    /// Gets access to the palette mode.
    /// </summary>
    PaletteMode ProviderPaletteMode { get; }

    /// <summary>
    /// Gets access to the context menu redirector.
    /// </summary>
    PaletteRedirect ProviderRedirector { get; }

    /// <summary>
    /// Gets a delegate used to indicate a repaint is required.
    /// </summary>
    NeedPaintHandler ProviderNeedPaintDelegate { get; }

    /// <summary>
    /// Gets a value indicating whether overflow scroll rows use arrow glyphs instead of Scroll Up/Scroll Down text.
    /// </summary>
    bool ProviderOverflowScrollUseArrows { get; }
}
#endregion

#region IContextMenuItemColumn
/// <summary>
/// Interface used to control width of a context menu item column.
/// </summary>
public interface IContextMenuItemColumn
{
    /// <summary>
    /// Gets the index of the column within the menu item.
    /// </summary>
    int ColumnIndex { get; }

    /// <summary>
    /// Gets the last calculated preferred size value.
    /// </summary>
    Size LastPreferredSize { get; }

    /// <summary>
    /// Sets the preferred width value to use until further notice.
    /// </summary>
    int OverridePreferredWidth { set; }
}
#endregion

#region IContextMenuTarget
/// <summary>
/// Interface used to control width of a context menu item column.
/// </summary>
public interface IContextMenuTarget
{
    /// <summary>
    /// Returns if the item shows a sub menu when selected.
    /// </summary>
    bool HasSubMenu { get; }

    /// <summary>
    /// This target should display as the active target.
    /// </summary>
    void ShowTarget();

    /// <summary>
    /// This target should clear any active display.
    /// </summary>
    void ClearTarget();

    /// <summary>
    /// This target should show any appropriate sub menu.
    /// </summary>
    void ShowSubMenu();

    /// <summary>
    /// This target should remove any showing sub menu.
    /// </summary>
    void ClearSubMenu();

    /// <summary>
    /// Determine if the keys value matches the mnemonic setting for this target.
    /// </summary>
    /// <param name="charCode">Key code to test against.</param>
    /// <returns>True if a match is found; otherwise false.</returns>
    bool MatchMnemonic(char charCode);

    /// <summary>
    /// Activate the item because of a mnemonic key press.
    /// </summary>
    void MnemonicActivate();

    /// <summary>
    /// Gets the view element that should be used when this target is active.
    /// </summary>
    /// <returns>View element to become active.</returns>
    ViewBase GetActiveView();

    /// <summary>
    /// Get the client rectangle for the display of this target.
    /// </summary>
    Rectangle ClientRectangle { get; }

    /// <summary>
    /// Should a mouse down at the provided point cause the currently stacked context menu to become current.
    /// </summary>
    /// <param name="pt">Client coordinates point.</param>
    /// <returns>True to become current; otherwise false.</returns>
    bool DoesStackedClientMouseDownBecomeCurrent(Point pt);
}
#endregion

#region IContainedInputControl
/// <summary>
/// Interface allowing access to the contained input control.
/// </summary>
public interface IContainedInputControl
{
    /// <summary>
    /// Gets access to the contained input control.
    /// </summary>
    Control ContainedControl { get; }
}
#endregion

#region IKryptonCommand
/// <summary>
/// Interface exposes access to a command definition.
/// </summary>
public interface IKryptonCommand
{
    /// <summary>
    /// Occurs when the command needs executing.
    /// </summary>
    event EventHandler? Execute;

    /// <summary>
    /// Occurs when a property has changed value.
    /// </summary>
    event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Gets and sets the enabled state of the command.
    /// </summary>
    bool Enabled { get; set; }

    /// <summary>
    /// Gets and sets the checked state of the command.
    /// </summary>
    bool Checked { get; set; }

    /// <summary>
    /// Gets and sets the check state of the command.
    /// </summary>
    CheckState CheckState { get; set; }

    /// <summary>
    /// Gets and sets the command text.
    /// </summary>
    string Text { get; set; }

    /// <summary>
    /// Gets and sets the command extra text.
    /// </summary>
    string ExtraText { get; set; }

    /// <summary>
    /// Gets and sets the command text line 1 for use in KryptonRibbon.
    /// </summary>
    string TextLine1 { get; set; }

    /// <summary>
    /// Gets and sets the command text line 2 for use in KryptonRibbon.
    /// </summary>
    string TextLine2 { get; set; }

    /// <summary>
    /// Gets and sets the command small image.
    /// </summary>
    Image? ImageSmall { get; set; }

    /// <summary>
    /// Gets and sets the command large image.
    /// </summary>
    Image? ImageLarge { get; set; }

    /// <summary>
    /// Gets and sets the command image transparent color.
    /// </summary>
    Color ImageTransparentColor { get; set; }

    /// <summary>Gets or sets the type of the command.</summary>
    /// <value>The type of the command.</value>
    KryptonCommandType CommandType { get; set; }

    /// <summary>
    /// Generates an Execute event for a command.
    /// </summary>
    void PerformExecute();

    /// <summary>
    /// Generates an Execute event for a command, passing the originating source as the event sender.
    /// </summary>
    /// <param name="sender">The object that initiated command execution.</param>
    void PerformExecute(object? sender);
}
#endregion

#region IKryptonMonthCalendar
/// <summary>
/// Provides month calendar information.
/// </summary>
public interface IKryptonMonthCalendar
{
    /// <summary>
    /// Gets access to the owning control
    /// </summary>
    Control CalendarControl { get; }

    /// <summary>
    /// Gets if the control is in design mode.
    /// </summary>
    bool InDesignMode { get; }

    /// <summary>
    /// Get the renderer.
    /// </summary>
    /// <returns>Render instance.</returns>
    IRenderer GetRenderer();

    /// <summary>
    /// Gets a delegate for creating tool strip renderers.
    /// </summary>
    GetToolStripRenderer GetToolStripDelegate { get; }

    /// <summary>
    /// Gets the number of columns and rows of months Displayed.
    /// </summary>
    Size CalendarDimensions { get; }

    /// <summary>
    /// Gets the calendar view used to choose a date (days, months, or years).
    /// </summary>
    MonthCalendarView CalendarView { get; }

    /// <summary>
    /// First day of the week.
    /// </summary>
    Day FirstDayOfWeek { get; }

    /// <summary>
    /// First date allowed to be drawn/selected.
    /// </summary>
    DateTime MinDate { get; }

    /// <summary>
    /// Last date allowed to be drawn/selected.
    /// </summary>
    DateTime MaxDate { get; }

    /// <summary>
    /// Today's date.
    /// </summary>
    DateTime TodayDate { get; }

    /// <summary>
    /// Today's date format.
    /// </summary>
    string TodayFormat { get; }

    /// <summary>
    /// Gets the focus day.
    /// </summary>
    DateTime? FocusDay { get; set; }

    /// <summary>
    /// Number of days allowed to be selected at a time.
    /// </summary>
    int MaxSelectionCount { get; }

    /// <summary>
    /// Gets the number of months to move for next/prev buttons.
    /// </summary>
    int ScrollChange { get; }

    /// <summary>
    /// Start of selected range.
    /// </summary>
    DateTime SelectionStart { get; }

    /// <summary>
    /// End of selected range.
    /// </summary>
    DateTime SelectionEnd { get; }

    /// <summary>
    /// Update usage of bolded overrides.
    /// </summary>
    /// <param name="bolded">Should show bolded.</param>
    void SetBoldedOverride(bool bolded);

    /// <summary>
    /// Update usage of today overrides.
    /// </summary>
    /// <param name="today">New today state.</param>
    void SetTodayOverride(bool today);

    /// <summary>
    /// Update usage of focus overrides.
    /// </summary>
    /// <param name="focus">Should show focus.</param>
    void SetFocusOverride(bool focus);

    /// <summary>
    /// Set the selection range.
    /// </summary>
    /// <param name="start">New starting date.</param>
    /// <param name="end">New ending date.</param>
    void SetSelectionRange(DateTime start, DateTime end);

    /// <summary>
    /// Dates to be bolded.
    /// </summary>
    DateTimeList BoldedDatesList { get; }

    /// <summary>
    /// Monthly days to be bolded.
    /// </summary>
    int MonthlyBoldedDatesMask { get; }

    /// <summary>
    /// Array of annual days per month to be bolded.
    /// </summary>
    int[] AnnuallyBoldedDatesMask { get; }

    /// <summary>
    /// Gets access to the month calendar common appearance entries.
    /// </summary>
    PaletteMonthCalendarRedirect StateCommon { get; }

    /// <summary>
    /// Gets access to the month calendar normal appearance entries.
    /// </summary>
    PaletteMonthCalendarDoubleState StateNormal { get; }

    /// <summary>
    /// Gets access to the month calendar disabled appearance entries.
    /// </summary>
    PaletteMonthCalendarDoubleState StateDisabled { get; }

    /// <summary>
    /// Gets access to the month calendar tracking appearance entries.
    /// </summary>
    PaletteMonthCalendarState StateTracking { get; }

    /// <summary>
    /// Gets access to the month calendar pressed appearance entries.
    /// </summary>
    PaletteMonthCalendarState StatePressed { get; }

    /// <summary>
    /// Gets access to the month calendar checked normal appearance entries.
    /// </summary>
    PaletteMonthCalendarState StateCheckedNormal { get; }

    /// <summary>
    /// Gets access to the month calendar checked tracking appearance entries.
    /// </summary>
    PaletteMonthCalendarState StateCheckedTracking { get; }

    /// <summary>
    /// Gets access to the month calendar checked pressed appearance entries.
    /// </summary>
    PaletteMonthCalendarState StateCheckedPressed { get; }

    /// <summary>
    /// Gets access to the override for disabled day.
    /// </summary>
    PaletteTripleOverride OverrideDisabled { get; }

    /// <summary>
    /// Gets access to the override for disabled day.
    /// </summary>
    PaletteTripleOverride OverrideNormal { get; }

    /// <summary>
    /// Gets access to the override for tracking day.
    /// </summary>
    PaletteTripleOverride OverrideTracking { get; }

    /// <summary>
    /// Gets access to the override for pressed day.
    /// </summary>
    PaletteTripleOverride OverridePressed { get; }

    /// <summary>
    /// Gets access to the override for checked normal day.
    /// </summary>
    PaletteTripleOverride OverrideCheckedNormal { get; }

    /// <summary>
    /// Gets access to the override for checked tracking day.
    /// </summary>
    PaletteTripleOverride OverrideCheckedTracking { get; }

    /// <summary>
    /// Gets access to the override for checked pressed day.
    /// </summary>
    PaletteTripleOverride OverrideCheckedPressed { get; }
}
#endregion

#region IKryptonLogger
/// <summary>
/// Receives diagnostic messages from the toolkit.
/// </summary>
public interface IKryptonLogger
{
    /// <summary>
    /// Writes a diagnostic message.
    /// </summary>
    /// <param name="message">The message to write.</param>
    void Write(string message);
}
#endregion

#region IKryptonDebug
/// <summary>
/// Exposes access to the debugging helpers for krypton controls.
/// </summary>
public interface IKryptonDebug
{
    /// <summary>
    /// Reset the internal counters.
    /// </summary>
    void KryptonResetCounters();

    /// <summary>
    /// Gets the number of layout cycles performed since last reset.
    /// </summary>
    int KryptonLayoutCounter { get; }

    /// <summary>
    /// Gets the number of paint cycles performed since last reset.
    /// </summary>
    int KryptonPaintCounter { get; }
}
#endregion

#region IKryptonDesignerSelect
/// <summary>
/// Exposes design time selection of parent control.
/// </summary>
public interface IKryptonDesignerSelect
{
    /// <summary>
    /// Should painting be performed for the selection glyph.
    /// </summary>
    bool CanPaint { get; }

    /// <summary>
    /// Request the parent control be selected.
    /// </summary>
    void SelectParentControl();
}
#endregion

#region IKryptonDesignObject
/// <summary>
/// Exposes interface for visual form to cooperate with a view for Designers.
/// </summary>
public interface IKryptonDesignObject
{
    /// <summary>
    /// Gets and sets if the object is enabled.
    /// </summary>
    bool DesignEnabled { get; set; }

    /// <summary>
    /// Gets and sets if the object is visible.
    /// </summary>
    bool DesignVisible { get; set; }
}
#endregion

#region IKryptonThemedSystemMenu

/// <summary>
/// Defines the interface for themed system menu functionality.
/// </summary>
public interface IKryptonThemedSystemMenu
{
    /// <summary>
    /// Gets or sets whether the themed system menu is enabled.
    /// </summary>
    bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets whether left-click on title bar shows the themed system menu.
    /// </summary>
    bool ShowOnLeftClick { get; set; }

    /// <summary>
    /// Gets or sets whether right-click on title bar shows the themed system menu.
    /// </summary>
    bool ShowOnRightClick { get; set; }

    /// <summary>
    /// Gets or sets whether Alt+Space shows the themed system menu.
    /// </summary>
    bool ShowOnAltSpace { get; set; }

    /// <summary>
    /// Gets the number of items currently in the themed system menu.
    /// </summary>
    int MenuItemCount { get; }

    /// <summary>
    /// Gets whether the themed system menu contains any items.
    /// </summary>
    bool HasMenuItems { get; }

    /// <summary>
    /// Shows the themed system menu at the specified screen location.
    /// </summary>
    /// <param name="screenLocation">The screen coordinates where the menu should appear.</param>
    void Show(Point screenLocation);

    /// <summary>
    /// Shows the themed system menu at the form's top-left position.
    /// </summary>
    void ShowAtFormTopLeft();

    /// <summary>
    /// Refreshes the themed system menu.
    /// </summary>
    void Refresh();

    /// <summary>
    /// Handles keyboard shortcuts for the themed system menu.
    /// </summary>
    /// <param name="keyData">The key data to process.</param>
    /// <returns>True if the shortcut was handled; otherwise false.</returns>
    bool HandleKeyboardShortcut(Keys keyData);



    /// <summary>
    /// Gets the current theme name being used for system menu icons.
    /// </summary>
    string CurrentIconTheme { get; }

    /// <summary>
    /// Manually refreshes all icons to match the current theme.
    /// Call this method when the application theme changes.
    /// </summary>
    void RefreshThemeIcons();

    /// <summary>
    /// Manually sets the theme for icon selection.
    /// </summary>
    /// <param name="themeName">The theme name to use for icons.</param>
    void SetIconTheme(string themeName);

    /// <summary>
    /// Sets the theme based on specific theme types (Black, Blue, Silver).
    /// </summary>
    /// <param name="themeType">The theme type to use.</param>
    void SetThemeType(ThemeType themeType);
}

#endregion

#region Enum VisualOrientation
/// <summary>
/// Specifies the orientation of a visual element.
/// </summary>
public enum VisualOrientation
{
    /// <summary>
    /// Specifies the element is orientated in a vertical top down manner.
    /// </summary>
    Top,

    /// <summary>
    /// Specifies the element is orientated in a vertical bottom upwards manner.
    /// </summary>
    Bottom,

    /// <summary>
    /// Specifies the element is orientated in a horizontal left to right manner.
    /// </summary>
    Left,

    /// <summary>
    /// Specifies the element is orientated in a horizontal right to left manner.
    /// </summary>
    Right
}
#endregion

#region Enum TabBorderStyle
/// <summary>
/// Specifies the style of tab border to draw.
/// </summary>
[TypeConverter(typeof(TabBorderStyleConverter))]
public enum TabBorderStyle
{
    /// <summary>
    /// Specifies square tabs of equal size with small spacing gaps.
    /// </summary>
    SquareEqualSmall,

    /// <summary>
    /// Specifies square tabs of equal size with medium spacing gaps.
    /// </summary>
    SquareEqualMedium,

    /// <summary>
    /// Specifies square tabs of equal size with large spacing gaps.
    /// </summary>
    SquareEqualLarge,

    /// <summary>
    /// Specifies square tabs with larger selected entry with small spacing gaps.
    /// </summary>
    SquareOutsizeSmall,

    /// <summary>
    /// Specifies square tabs with larger selected entry with medium spacing gaps.
    /// </summary>
    SquareOutsizeMedium,

    /// <summary>
    /// Specifies square tabs with larger selected entry with large spacing gaps.
    /// </summary>
    SquareOutsizeLarge,

    /// <summary>
    /// Specifies rounded tabs of equal size with small spacing gaps.
    /// </summary>
    RoundedEqualSmall,

    /// <summary>
    /// Specifies rounded tabs of equal size with medium spacing gaps.
    /// </summary>
    RoundedEqualMedium,

    /// <summary>
    /// Specifies rounded tabs of equal size with large spacing gaps.
    /// </summary>
    RoundedEqualLarge,

    /// <summary>
    /// Specifies rounded tabs with larger selected entry with small spacing gaps.
    /// </summary>
    RoundedOutsizeSmall,

    /// <summary>
    /// Specifies rounded tabs with larger selected entry with medium spacing gaps.
    /// </summary>
    RoundedOutsizeMedium,

    /// <summary>
    /// Specifies rounded tabs with larger selected entry with large spacing gaps.
    /// </summary>
    RoundedOutsizeLarge,

    /// <summary>
    /// Specifies near slanted tabs of equal size.
    /// </summary>
    SlantEqualNear,

    /// <summary>
    /// Specifies far slanted tabs of equal size.
    /// </summary>
    SlantEqualFar,

    /// <summary>
    /// Specifies double slanted tabs of equal size.
    /// </summary>
    SlantEqualBoth,

    /// <summary>
    /// Specifies near slanted tabs with larger selected entry.
    /// </summary>
    SlantOutsizeNear,

    /// <summary>
    /// Specifies far slanted tabs with larger selected entry.
    /// </summary>
    SlantOutsizeFar,

    /// <summary>
    /// Specifies double slanted tabs with larger selected entry.
    /// </summary>
    SlantOutsizeBoth,

    /// <summary>
    /// Specifies the OneNote application style tab appearance.
    /// </summary>
    OneNote,

    /// <summary>
    /// Specifies smooth tabs of equal size.
    /// </summary>
    SmoothEqual,

    /// <summary>
    /// Specifies smooth tabs with larger selected entry.
    /// </summary>
    SmoothOutsize,

    /// <summary>
    /// Specifies docking tabs of equal size.
    /// </summary>
    DockEqual,

    /// <summary>
    /// Specifies docking tabs with larger selected entry.
    /// </summary>
    DockOutsize
}
#endregion

#region Enum ButtonEnabled
/// <summary>
/// Specifies the enabled state of a button specification.
/// Controls whether a button is enabled, disabled, or inherits its state from the container.
/// </summary>
public enum ButtonEnabled
{
    /// <summary>
    /// Specifies button should take enabled state from container control state.
    /// </summary>
    Container,

    /// <summary>
    /// Specifies button should be enabled.
    /// </summary>
    True,

    /// <summary>
    /// Specifies button should be disabled.
    /// </summary>
    False
}
#endregion

#region Enum ButtonOrientation
/// <summary>
/// Specifies the orientation of a button specification.
/// Controls how buttons are positioned and oriented within their container.
/// </summary>
public enum ButtonOrientation
{
    /// <summary>
    /// Specifies orientation should automatically match the concept of use.
    /// </summary>
    Auto,

    /// <summary>
    /// Specifies the button is orientated in a vertical top down manner.
    /// </summary>
    FixedTop,

    /// <summary>
    /// Specifies the button is orientated in a vertical bottom upwards manner.
    /// </summary>
    FixedBottom,

    /// <summary>
    /// Specifies the button is orientated in a horizontal left to right manner.
    /// </summary>
    FixedLeft,

    /// <summary>
    /// Specifies the button is orientated in a horizontal right to left manner.
    /// </summary>
    FixedRight
}
#endregion

#region Enum ButtonCheckState
/// <summary>
/// Specifies the checked state of a button.
/// </summary>
public enum ButtonCheckState
{
    /// <summary>
    /// Specifies the button is not a checked button.
    /// </summary>
    NotCheckButton,

    /// <summary>
    /// Specifies the check button is currently checked.
    /// </summary>
    Checked,

    /// <summary>
    /// Specifies the check button is not currently checked.
    /// </summary>
    Unchecked
}
#endregion

#region Enum BadgePosition

/// <summary>
/// Specifies the position of a badge on a button.
/// </summary>
public enum BadgePosition
{
    /// <summary>
    /// Specifies the badge is positioned in the top-right corner.
    /// </summary>
    TopRight,

    /// <summary>
    /// Specifies the badge is positioned in the top-left corner.
    /// </summary>
    TopLeft,

    /// <summary>
    /// Specifies the badge is positioned in the bottom-right corner.
    /// </summary>
    BottomRight,

    /// <summary>
    /// Specifies the badge is positioned in the bottom-left corner.
    /// </summary>
    BottomLeft
}

#endregion

#region Enum BadgeShape

/// <summary>
/// Specifies the shape of a badge.
/// </summary>
public enum BadgeShape
{
    /// <summary>
    /// Specifies a circular badge.
    /// </summary>
    Circle,

    /// <summary>
    /// Specifies a square badge.
    /// </summary>
    Square,

    /// <summary>
    /// Specifies a rounded rectangle badge.
    /// </summary>
    RoundedRectangle,

    /// <summary>
    /// Specifies a capsule (pill-shaped) badge with fully rounded ends.
    /// </summary>
    Capsule
}

#endregion

#region Enum BadgeAnimation

/// <summary>
/// Specifies the animation type for a badge.
/// </summary>
public enum BadgeAnimation
{
    /// <summary>
    /// No animation.
    /// </summary>
    None,

    /// <summary>
    /// Fade in and out animation.
    /// </summary>
    FadeInOut,

    /// <summary>
    /// Pulsing animation (scale and opacity).
    /// </summary>
    Pulse
}

#endregion

#region Enum BadgeBevelType
/// <summary>
/// Specifies the type of bevel effect for badge borders.
/// </summary>
public enum BadgeBevelType
{
    /// <summary>
    /// No bevel effect.
    /// </summary>
    None,

    /// <summary>
    /// Raised bevel effect (light top/left edges, dark bottom/right edges).
    /// </summary>
    Raised,

    /// <summary>
    /// Inset/embedded bevel effect (dark top/left edges, light bottom/right edges).
    /// </summary>
    Inset
}
#endregion

#region Enum RelativeEdgeAlign
/// <summary>
/// Specifies a relative edge alignment position.
/// </summary>
public enum RelativeEdgeAlign
{
    /// <summary>
    /// Specifies a relative alignment of near.
    /// </summary>
    Near,

    /// <summary>
    /// Specifies a relative alignment of far.
    /// </summary>
    Far
}
#endregion

#region Enum RelativePositionAlign
/// <summary>
/// Specifies a relative alignment position.
/// </summary>
public enum RelativePositionAlign
{
    /// <summary>
    /// Specifies a relative alignment of near.
    /// </summary>
    Near,

    /// <summary>
    /// Specifies a relative alignment of center.
    /// </summary>
    Center,

    /// <summary>
    /// Specifies a relative alignment of far.
    /// </summary>
    Far
}
#endregion

#region Enum LabelStyle
/// <summary>
/// Specifies the label style.
/// </summary>
[TypeConverter(typeof(LabelStyleConverter))]
public enum LabelStyle
{
    AlternateControl,

    /// <summary>
    /// Specifies a normal label for use on a control style background.
    /// </summary>
    NormalControl,

    /// <summary>
    /// Specifies a bold label for use on a control style background.
    /// </summary>
    BoldControl,

    /// <summary>
    /// Specifies an italic label for use on a control style background.
    /// </summary>
    ItalicControl,

    /// <summary>
    /// Specifies a label appropriate for titles for use on a control style background.
    /// </summary>
    TitleControl,

    /// <summary>
    /// Specifies an alternate label for use on a panel style background.
    /// </summary>
    AlternatePanel,

    /// <summary>
    /// Specifies a normal label for use on a panel style background.
    /// </summary>
    NormalPanel,

    /// <summary>
    /// Specifies a bold label for use on a panel style background.
    /// </summary>
    BoldPanel,

    /// <summary>
    /// Specifies an italic label for use on a panel style background.
    /// </summary>
    ItalicPanel,

    /// <summary>
    /// Specifies a label appropriate for titles for use on a panel style background.
    /// </summary>
    TitlePanel,

    /// <summary>
    /// Specifies a label appropriate for captions for use on a group box style background.
    /// </summary>
    GroupBoxCaption,

    /// <summary>
    /// Specifies a label appropriate for use inside a tooltip.
    /// </summary>
    ToolTip,

    /// <summary>
    /// Specifies a label appropriate for use inside a super tooltip.
    /// </summary>
    SuperTip,

    /// <summary>
    /// Specifies a label appropriate for use inside a key tooltip.
    /// </summary>
    KeyTip,

    /// <summary>
    /// Specifies the first custom label style.
    /// </summary>
    Custom1,
    Custom2,
    Custom3
}
#endregion

#region Enum GridStyle
/// <summary>
/// Specifies the grid style.
/// </summary>
[TypeConverter(typeof(GridStyleConverter))]
public enum GridStyle
{
    /// <summary>
    /// Specifies a list grid style.
    /// </summary>
    List,

    /// <summary>
    /// Specifies a worksheet grid style.
    /// </summary>
    Sheet,

    /// <summary>
    /// Specifies the first custom grid style.
    /// </summary>
    Custom1,
    Custom2,
    Custom3
}
#endregion

#region Enum DataGridViewStyle
/// <summary>
/// Specifies the data grid view style.
/// </summary>
[TypeConverter(typeof(DataGridViewStyleConverter))]
public enum DataGridViewStyle
{
    /// <summary>
    /// Specifies a list grid style.
    /// </summary>
    List,

    /// <summary>
    /// Specifies a worksheet grid style.
    /// </summary>
    Sheet,

    /// <summary>
    /// Specifies the first custom grid style.
    /// </summary>
    Custom1,
    Custom2,
    Custom3,

    /// <summary>
    /// Specifies a mixed set of styles.
    /// </summary>
    Mixed
}
#endregion

#region Enum HeaderStyle
/// <summary>
/// Specifies the header style.
/// </summary>
[TypeConverter(typeof(HeaderStyleConverter))]
public enum HeaderStyle
{
    /// <summary>
    /// Specifies a primary header.
    /// </summary>
    Primary,

    /// <summary>
    /// Specifies a secondary header.
    /// </summary>
    Secondary,

    /// <summary>
    /// Specifies an inactive docking header.
    /// </summary>
    DockInactive,

    /// <summary>
    /// Specifies an active docking header.
    /// </summary>
    DockActive,

    /// <summary>
    /// Specifies a form header.
    /// </summary>
    Form,

    /// <summary>
    /// Specifies a calendar header.
    /// </summary>
    Calendar,

    /// <summary>
    /// Specifies the first custom header style.
    /// </summary>
    Custom1,
    Custom2,
    Custom3
}
#endregion

#region Enum ButtonStyle
/// <summary>
/// Specifies the button style.
/// </summary>
[TypeConverter(typeof(ButtonStyleConverter))]
public enum ButtonStyle
{
    /// <summary>
    /// Specifies a standalone button style.
    /// </summary>
    Standalone,

    /// <summary>
    /// Specifies an alternate standalone button style.
    /// </summary>
    Alternate,

    /// <summary>
    /// Specifies a low profile button style.
    /// </summary>
    LowProfile,

    /// <summary>
    /// Specifies a button spec usage style.
    /// </summary>
    ButtonSpec,

    /// <summary>
    /// Specifies a button style appropriate for bread crumbs.
    /// </summary>
    BreadCrumb,

    /// <summary>
    /// Specifies a button style appropriate for calendar day.
    /// </summary>
    CalendarDay,

    /// <summary>
    /// Specifies a ribbon cluster button usage style.
    /// </summary>
    Cluster,

    /// <summary>
    /// Specifies a ribbon gallery button usage style.
    /// </summary>
    Gallery,

    /// <summary>
    /// Specifies a navigator stack usage style.
    /// </summary>
    NavigatorStack,

    /// <summary>
    /// Specifies a navigator overflow usage style.
    /// </summary>
    NavigatorOverflow,

    /// <summary>
    /// Specifies a navigator mini usage style.
    /// </summary>
    NavigatorMini,

    /// <summary>
    /// Specifies an input control usage style.
    /// </summary>
    InputControl,

    /// <summary>
    /// Specifies a list item usage style.
    /// </summary>
    ListItem,

    /// <summary>
    /// Specifies a form level style.
    /// </summary>
    Form,

    /// <summary>
    /// Specifies a form close button.
    /// </summary>
    FormClose,

    /// <summary>
    /// Specifies a command button.
    /// </summary>
    Command,

    /// <summary>
    /// Specifies the first custom button style.
    /// </summary>
    Custom1,
    Custom2,
    Custom3
}
#endregion

#region Enum ToggleSwitchKnobStyle
/// <summary>
/// Specifies the visual style used to render a <see cref="KryptonToggleSwitch"/> knob.
/// </summary>
public enum ToggleSwitchKnobStyle
{
    /// <summary>
    /// Specifies a diagonal gradient ellipse with a border.
    /// </summary>
    Classic,

    /// <summary>
    /// Specifies a linear gradient ellipse with configurable direction and intensity.
    /// </summary>
    Gradient,

    /// <summary>
    /// Specifies a solid ellipse with a border.
    /// </summary>
    Flat,

    /// <summary>
    /// Specifies a radial gradient ellipse.
    /// </summary>
    Radial,

    /// <summary>
    /// Specifies a thick ring with a hollow centre.
    /// </summary>
    Ring,

    /// <summary>
    /// Specifies an ellipse with bevelled highlight and shadow arcs.
    /// </summary>
    Bevel,

    /// <summary>
    /// Specifies a rounded-square knob.
    /// </summary>
    RoundedSquare,

    /// <summary>
    /// Specifies a square knob with sharp corners.
    /// </summary>
    Square,

    /// <summary>
    /// Specifies a square knob with vertical grip lines.
    /// </summary>
    Grip,

    /// <summary>
    /// Specifies a square knob with stacked chevron glyphs.
    /// </summary>
    Chevron,

    /// <summary>
    /// Specifies a rounded-square knob with a centred indicator dot.
    /// </summary>
    Indicator,

    /// <summary>
    /// Specifies a thin rounded track with a large overlapping circular knob.
    /// </summary>
    ThinTrack,

    /// <summary>
    /// Specifies a capsule track with a soft vertical gradient and circular knob.
    /// </summary>
    Pill,

    /// <summary>
    /// Specifies a recessed capsule track with a brushed-metal knob, drop shadow, and optional check/cross track icons.
    /// </summary>
    Metallic
}
#endregion

#region Enum ToggleSwitchChevronDirection
/// <summary>
/// Specifies the direction of chevron glyphs drawn on a <see cref="ToggleSwitchKnobStyle.Chevron"/> knob.
/// </summary>
public enum ToggleSwitchChevronDirection
{
    /// <summary>
    /// Points right when unchecked and left when checked.
    /// </summary>
    Auto,

    /// <summary>
    /// Always points left.
    /// </summary>
    Left,

    /// <summary>
    /// Always points right.
    /// </summary>
    Right
}

/// <summary>
/// Specifies whether a <see cref="KryptonToggleSwitch"/> lays out horizontally or vertically.
/// </summary>
public enum ToggleSwitchOrientation
{
    /// <summary>
    /// The knob travels left (off) to right (on).
    /// </summary>
    Horizontal,

    /// <summary>
    /// The knob travels top (off) to bottom (on).
    /// </summary>
    Vertical
}
#endregion

#region Enum InputControlStyle
/// <summary>
/// Specifies the input control style.
/// </summary>
[TypeConverter(typeof(InputControlStyleConverter))]
public enum InputControlStyle
{
    /// <summary>
    /// Specifies a standalone input button style.
    /// </summary>
    Standalone,

    /// <summary>
    /// Specifies a ribbon input button style.
    /// </summary>
    Ribbon,

    /// <summary>
    /// Specifies a custom input button style.
    /// </summary>
    Custom1,
    Custom2,
    Custom3,
    /// <summary>
    /// Specifies a panel client input style.
    /// </summary>
    PanelClient,
    /// <summary>
    /// Specifies the panel alternate input style.
    /// </summary>
    PanelAlternate
    /*
    /// <summary>
    /// Specifies the disabled input style.
    /// </summary>
    Disabled
    */
}
#endregion

#region Enum InputPulsingBorderShowWhen
/// <summary>
/// Specifies when an optional input control pulsing border is shown.
/// </summary>
public enum InputPulsingBorderShowWhen
{
    /// <summary>
    /// Show the pulsing border only when the input has keyboard focus.
    /// </summary>
    Focused,

    /// <summary>
    /// Show the pulsing border when the input is active (focused, mouse over, or AlwaysActive).
    /// </summary>
    Active,

    /// <summary>
    /// Always show the pulsing border when enabled.
    /// </summary>
    Always
}
#endregion

#region Enum InputPulsingBorderStyle
/// <summary>
/// Specifies how an optional input control pulsing border is drawn.
/// </summary>
public enum InputPulsingBorderStyle
{
    /// <summary>
    /// Draw the pulsing border along the bottom edge only.
    /// </summary>
    Bottom,

    /// <summary>
    /// Draw the pulsing border around the entire control border.
    /// </summary>
    All
}
#endregion

#region Enum InputPulsingBorderCategory
/// <summary>
/// Identifies which <see cref="KryptonManager.PulsingBorderValues"/> group a control inherits from.
/// </summary>
public enum InputPulsingBorderCategory
{
    /// <summary>
    /// Text and selection inputs: TextBox, ComboBox, NumericUpDown, DateTimePicker, and similar.
    /// </summary>
    Inputs,

    /// <summary>
    /// Button-style controls: Button, DropButton, ColorButton, and similar.
    /// </summary>
    Buttons,

    /// <summary>
    /// Form chrome (<see cref="KryptonForm"/>).
    /// </summary>
    Forms,

    /// <summary>
    /// Other simple controls: CheckBox, RadioButton, Label, and similar.
    /// </summary>
    Other
}
#endregion

#region Enum SeparatorStyle
/// <summary>
/// Specifies the separator style.
/// </summary>
[TypeConverter(typeof(SeparatorStyleConverter))]
public enum SeparatorStyle
{
    /// <summary>
    /// Specifies a low profile separator.
    /// </summary>
    LowProfile,

    /// <summary>
    /// Specifies a high profile separator.
    /// </summary>
    HighProfile,

    /// <summary>
    /// Specifies a high profile for internal separator.
    /// </summary>
    HighInternalProfile,

    /// <summary>
    /// Specifies a custom separator.
    /// </summary>
    Custom1,
    Custom2,
    Custom3
}
#endregion

#region Enum TabStyle
/// <summary>
/// Specifies the tab style.
/// </summary>
[TypeConverter(typeof(TabStyleConverter))]
public enum TabStyle
{
    /// <summary>
    /// Specifies the high profile tab style.
    /// </summary>
    HighProfile,

    /// <summary>
    /// Specifies the standard profile style.
    /// </summary>
    StandardProfile,

    /// <summary>
    /// Specifies the low profile tab style.
    /// </summary>
    LowProfile,

    /// <summary>
    /// Specifies the Microsoft OneNote tab style.
    /// </summary>
    OneNote,

    /// <summary>
    /// Specifies the docking tab style.
    /// </summary>
    Dock,

    /// <summary>
    /// Specifies the auto hidden docking tab style.
    /// </summary>
    DockAutoHidden,

    /// <summary>
    /// Specifies the first custom tab style.
    /// </summary>
    Custom1,
    Custom2,
    Custom3
}
#endregion

#region Enum HeaderLocation
/// <summary>
/// Specifies a target header.
/// </summary>
public enum HeaderLocation
{
    /// <summary>
    /// Specifies the primary header.
    /// </summary>
    PrimaryHeader,

    /// <summary>
    /// Specifies the secondary header.
    /// </summary>
    SecondaryHeader
}
#endregion

#region Enum HeaderGroupCollapsedTarget
/// <summary>
/// Specifies the target collapsed state of a header group when in the collapsed mode.
/// </summary>
[TypeConverter(typeof(HeaderGroupCollapsedTargetConverter))]
public enum HeaderGroupCollapsedTarget
{
    /// <summary>
    /// Specifies the appearance is collapsed to just the primary header.
    /// </summary>
    CollapsedToPrimary,

    /// <summary>
    /// Specifies the appearance is collapsed to just the secondary header.
    /// </summary>
    CollapsedToSecondary,

    /// <summary>
    /// Specifies the appearance is collapsed to just the primary and secondary headers.
    /// </summary>
    CollapsedToBoth
}
#endregion

#region Enum KryptonLinkBehavior
/// <summary>
/// Specifies the logic for underlining the link label short text.
/// </summary>
[TypeConverter(typeof(KryptonLinkBehaviorConverter))]
public enum KryptonLinkBehavior
{
    /// <summary>
    /// Specifies the short text is always underlined.
    /// </summary>
    AlwaysUnderline,

    /// <summary>
    /// Specifies the short text is underlined only when mouse hovers over text.
    /// </summary>
    HoverUnderline,

    /// <summary>
    /// Specifies the short text is never underlined.
    /// </summary>
    NeverUnderline
}
#endregion

#region Enum ViewDockStyle
/// <summary>
/// Specifies the docking styles for the docking view elements.
/// </summary>
public enum ViewDockStyle
{
    /// <summary>
    /// Specifies the child element should fill the remaining space.
    /// </summary>
    Fill,

    /// <summary>
    /// Specifies the child element should dock against the top edge.
    /// </summary>
    Top,

    /// <summary>
    /// Specifies the child element should dock against the bottom edge.
    /// </summary>
    Bottom,

    /// <summary>
    /// Specifies the child element should dock against the left edge.
    /// </summary>
    Left,

    /// <summary>
    /// Specifies the child element should dock against the right edge.
    /// </summary>
    Right
}
#endregion

#region Enum GridRowGlyph
/// <summary>
/// Specifies the grid row glyph.
/// </summary>
public enum GridRowGlyph
{
    /// <summary>
    /// Specifies no glyph for the row.
    /// </summary>
    None,

    /// <summary>
    /// Specifies a star for showing a dirty row.
    /// </summary>
    Star,

    /// <summary>
    /// Specifies an arrow for the current row.
    /// </summary>
    Arrow,

    /// <summary>
    /// Specifies a star and arrow for a dirty current row.
    /// </summary>
    ArrowStar,

    /// <summary>
    /// Specifies a pencil for the line being edited.
    /// </summary>
    Pencil
}
#endregion

#region Enum KryptonContextMenuPositionV
/// <summary>
/// Specifies the relative vertical position for showing a KryptonContextMenu.
/// </summary>
public enum KryptonContextMenuPositionV
{
    /// <summary>
    /// Specifies bottom of context menu is adjacent to top of rectangle.
    /// </summary>
    Above,

    /// <summary>
    /// Specifies top of context menu is adjacent to bottom of rectangle.
    /// </summary>
    Below,

    /// <summary>
    /// Specifies top of context menu is adjacent to top of rectangle.
    /// </summary>
    Top,

    /// <summary>
    /// Specifies bottom of context menu is adjacent to bottom of rectangle.
    /// </summary>
    Bottom
}
#endregion

#region Enum KryptonContextMenuPositionH
/// <summary>
/// Specifies the relative horizontal position for showing a KryptonContextMenu.
/// </summary>
public enum KryptonContextMenuPositionH
{
    /// <summary>
    /// Specifies right of context menu is adjacent to left of rectangle.
    /// </summary>
    Before,

    /// <summary>
    /// Specifies left of context menu is adjacent to right of rectangle.
    /// </summary>
    After,

    /// <summary>
    /// Specifies left of context menu is adjacent to left of rectangle.
    /// </summary>
    Left,

    /// <summary>
    /// Specifies right of context menu is adjacent to right of rectangle.
    /// </summary>
    Right
}
#endregion

#region Enum ColorScheme
/// <summary>
/// Specifies a color scheme.
/// </summary>
public enum ColorScheme
{
    /// <summary>
    /// Specifies no predefined colors.
    /// </summary>
    None,

    /// <summary>
    /// Specifies just white and black.
    /// </summary>
    Mono2,

    /// <summary>
    /// Specifies 8 colors ranging from white to black.
    /// </summary>
    Mono8,

    /// <summary>
    /// Specifies the basic set of 16 colors.
    /// </summary>
    Basic16,

    /// <summary>
    /// Specifies the Office set of standard 10 colors.
    /// </summary>
    OfficeStandard,

    /// <summary>
    /// Specifies the Office set of 10 color themes.
    /// </summary>
    OfficeThemes,

    /// <summary>
    /// Specifies dynamic colors sourced from the active palette's SchemeColors.
    /// </summary>
    PaletteColors
}
#endregion

#region Enum ThemeColorSortMode
/// <summary>
/// Sorting options for dynamic Theme Colors generated from the active palette SchemeColors.
/// </summary>
public enum ThemeColorSortMode
{
    OKLCH,
    HSB,
    RGB
}
#endregion

#region CheckedSelectionMode
/// <summary>
/// Specifies selection mode of the KryptonCheckedListBox.
/// </summary>
public enum CheckedSelectionMode
{
    /// <summary>
    /// No items can be selected.
    /// </summary>
    None = 0,

    /// <summary>
    /// Only one item can be selected.
    /// </summary>
    One = 1,

    /// <summary>
    /// Multiple items can be selected with simple click selection.
    /// </summary>
    MultiSimple = 2,

    /// <summary>
    /// Multiple items can be selected with extended selection (Ctrl/Shift keys).
    /// </summary>
    MultiExtended = 3,

    /// <summary>
    /// Only one item can be checked at a time (radio button behavior).
    /// </summary>
    Radio = 4
}
#endregion

#region Type ViewDockStyleLookup
internal class ViewDockStyleLookup : Dictionary<ViewBase, ViewDockStyle>;
#endregion

#region Type DateTimeList
/// <summary>
/// Manage a list of DateTime instances.
/// </summary>
public class DateTimeList : List<DateTime>;
#endregion

#region Type MonthCalendarButtonSpecCollection
/// <summary>
/// Collection for managing ButtonSpecAny instances.
/// </summary>
public class MonthCalendarButtonSpecCollection : ButtonSpecCollection<ButtonSpecAny>
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the MonthCalendarButtonSpecCollection class.
    /// </summary>
    /// <param name="owner">Reference to owning object.</param>
    public MonthCalendarButtonSpecCollection(ViewLayoutMonths owner)
        : base(owner)
    {
    }
    #endregion
}
#endregion

#region Delegates
/// <summary>
/// Signature of method that is called when painting needs to occur.
/// </summary>
/// <param name="sender">Source of the call.</param>
/// <param name="e">A NeedLayoutEventArgs containing event information.</param>
public delegate void NeedPaintHandler(object? sender, NeedLayoutEventArgs e);

/// <summary>
/// Signature of method that provides a point as the data.
/// </summary>
/// <param name="sender">Source of the call.</param>
/// <param name="pt">A Point related to the event.</param>
public delegate void PointHandler(object sender, Point pt);
#endregion

#region Enum PlacementMode
/// <summary>
/// Specifies the PlacementMode
/// https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.primitives.placementmode?view=netframework-4.7.2#System_Windows_Controls_Primitives_PlacementMode_Absolute
/// </summary>
[TypeConverter(typeof(PlacementModeConverter))]
public enum PlacementMode
{
    /// <summary>
    /// A position of the Popup control relative to the upper-left corner of the screen and at an offset that is defined by the HorizontalOffset and VerticalOffset property values. If the screen edge obscures the Popup, the control then repositions itself to align with the edge.
    /// </summary>
    Absolute = 0,

    /// <summary>
    /// A position of the Popup control relative to the upper-left corner of the screen and at an offset that is defined by the HorizontalOffset and VerticalOffset property values. If the screen edge obscures the Popup, the control extends in the opposite direction from the axis defined by the HorizontalOffset or VerticalOffset =.
    /// </summary>
    AbsolutePoint = 5,

    /// <summary>
    /// A position of the Popup control where the control aligns its upper edge with the lower edge of the PlacementTarget and aligns its left edge with the left edge of the PlacementTarget. If the lower screen-edge obscures the Popup, the control repositions itself so that its lower edge aligns with the upper edge of the PlacementTarget. If the upper screen-edge obscures the Popup, the control then repositions itself so that its upper edge aligns with the upper screen-edge.
    /// </summary>
    Bottom = 2,

    /// <summary>
    /// A position of the Popup control where it is centered over the PlacementTarget. If a screen edge obscures the Popup, the control repositions itself to align with the screen edge.
    /// </summary>
    /// <remarks>
    /// this does not make sense as the Mouse will then fire and the tooltip will be replaced, and then shown, and then replace, etc.
    /// </remarks>
    Center = 3,

    //// <summary>
    //// A position and repositioning behavior for the Popup control that is defined by the CustomPopupPlacementCallback delegate specified by the CustomPopupPlacementCallback property.
    //// </summary>
    //// <remarks>
    //// No callback implementation !
    //// </remarks>
    //Custom = 11,

    /// <summary>
    /// A Popup control that aligns its right edge with the left edge of the PlacementTarget and aligns its upper edge with the upper edge of the PlacementTarget.If the left screen-edge obscures the Popup, the Popup repositions itself so that its left edge aligns with the right edge of the PlacementTarget.If the right screen-edge obscures the Popup, the right edge of the control aligns with the right screen-edge.If the upper or lower screen-edge obscures the Popup, the control repositions itself to align with the obscuring screen edge.
    /// </summary>
    Left = 9,

    /// <summary>
    /// A position of the Popup control that aligns its upper edge with the lower edge of the bounding box of the mouse and aligns its left edge with the left edge of the bounding box of the mouse. If the lower screen-edge obscures the Popup, it repositions itself to align with the upper edge of the bounding box of the mouse. If the upper screen-edge obscures the Popup, the control repositions itself to align with the upper screen-edge.
    /// </summary>
    Mouse = 7,

    /// <summary>
    /// A position of the Popup control relative to the tip of the mouse cursor and at an offset that is defined by the HorizontalOffset and VerticalOffset property values. If a horizontal or vertical screen edge obscures the Popup, it opens in the opposite direction from the obscuring edge.If the opposite screen edge also obscures the Popup, it then aligns with the obscuring screen edge.
    /// </summary>
    MousePoint = 8,

    /// <summary>
    ///A position of the Popup control relative to the upper-left corner of the PlacementTarget and at an offset that is defined by the HorizontalOffset and VerticalOffset property values. If the screen edge obscures the Popup, the control repositions itself to align with the screen edge.
    /// </summary>
    Relative = 1,

    /// <summary>
    /// A position of the Popup control relative to the upper-left corner of the PlacementTarget and at an offset that is defined by the HorizontalOffset and VerticalOffset property values. If a screen edge obscures the Popup, the Popup extends in the opposite direction from the direction from the axis defined by the HorizontalOffset or VerticalOffset.If the opposite screen edge also obscures the Popup, the control then aligns with this screen edge.
    /// </summary>
    RelativePoint = 6,

    /// <summary>
    /// A position of the Popup control that aligns its left edge with the right edge of the PlacementTarget and aligns its upper edge with the upper edge of the PlacementTarget. If the right screen-edge obscures the Popup, the control repositions itself so that its left edge aligns with the left edge of the PlacementTarget.If the left screen-edge obscures the Popup, the control repositions itself so that its left edge aligns with the left screen-edge.If the upper or lower screen-edge obscures the Popup, the control then repositions itself to align with the obscuring screen edge.
    /// </summary>
    Right = 4,

    /// <summary>
    /// A position of the Popup control that aligns its lower edge with the upper edge of the PlacementTarget and aligns its left edge with the left edge of the PlacementTarget. If the upper screen-edge obscures the Popup, the control repositions itself so that its upper edge aligns with the lower edge of the PlacementTarget.If the lower screen-edge obscures the Popup, the lower edge of the control aligns with the lower screen-edge.If the left or right screen-edge obscures the Popup, it then repositions itself to align with the obscuring screen.
    /// </summary>
    Top = 10
}
#endregion Enum PlacementMode

#region MessageBox Definitions

#region Enum MessageBoxContentAreaType

/// <summary>Defines the content area type of a <see cref="T:KryptonMessageBox"/>.</summary>
public enum MessageBoxContentAreaType
{
    /// <summary>The default content area type of a <see cref="T:KryptonMessageBox"/>.</summary>
    Normal = 0,
    /// <summary>Use a <see cref="T:KryptonLinkWrapLabel"/> as the content area type of a <see cref="T:KryptonMessageBox"/>.</summary>
    LinkLabel = 1
}

#endregion

#region Enum KryptonMessageBoxIcon

/// <summary>Specifies the icon type for <see cref="T:KryptonMessageBox"/>.</summary>
[TypeConverter(typeof(KryptonMessageBoxIconConverter))]
public enum KryptonMessageBoxIcon
{
    /// <summary>Specify no icon.</summary>
    None = 0,

    /// <summary>Specify a hand icon.</summary>
    Hand = 1,

    /// <summary>
    /// Specify the system hand icon.
    /// The message box contains a symbol consisting of a white X in a circle with a red background.
    /// </summary>
    SystemHand = MessageBoxIcon.Hand,

    /// <summary>Specify a question icon.</summary>
    Question = 2,

    /// <summary>Specify the system question icon.</summary>
    SystemQuestion = MessageBoxIcon.Question,

    /// <summary>Specify an exclamation icon.</summary>
    Exclamation = 3,

    /// <summary>Specify the system exclamation icon.</summary>
    SystemExclamation = MessageBoxIcon.Exclamation,

    /// <summary>Specify an asterisk icon.</summary>
    Asterisk = 4,

    /// <summary>
    /// Specify the system asterisk icon.
    /// The message box contains a symbol consisting of a lowercase letter i in a circle.
    /// </summary>
    SystemAsterisk = MessageBoxIcon.Asterisk,

    /// <summary>Specify a stop icon.</summary>
    Stop = 5,

    /// <summary>
    /// Specify the system hand icon.
    /// The message box contains a symbol consisting of a white X in a circle with a red background.
    /// </summary>
    SystemStop = MessageBoxIcon.Stop,

    /// <summary>
    /// Specify a error icon.
    /// The message box contains a symbol consisting of white X in a circle with a red background.
    /// </summary>
    Error = 6,

    /// <summary>
    /// Specify the system hand icon.
    /// The message box contains a symbol consisting of a white X in a circle with a red background.
    /// </summary>
    SystemError = MessageBoxIcon.Error,

    /// <summary>Specify a warning icon.</summary>
    Warning = 7,

    /// <summary>Specify the system warning icon.</summary>
    SystemWarning = MessageBoxIcon.Warning,

    /// <summary>Specify an information icon.</summary>
    Information = 8,

    /// <summary>Specify the system information icon.</summary>
    SystemInformation = MessageBoxIcon.Information,

    /// <summary>Specify a UAC shield icon.</summary>
    Shield = 9,

    /// <summary>Specify a Windows logo icon.</summary>
    WindowsLogo = 10,

    /// <summary>Specify your application icon.</summary>
    Application = 11,

    /// <summary>Specify the default system application icon. See <see cref="SystemIcons.Application"/>.</summary>
    SystemApplication = 12
}

#endregion

#region Enum KryptonMessageBoxButtons

/// <summary>
/// Specifies constants defining which buttons to display on a <see cref="T:KryptonMessageBox" />.
/// Provides themed alternatives to the standard MessageBox buttons with consistent Krypton styling.
/// </summary>
public enum KryptonMessageBoxButtons
{
    /// <summary>
    ///  Specifies that the message box contains an OK button.
    /// </summary>
    OK = MessageBoxButtons.OK,

    /// <summary>
    ///  Specifies that the message box contains OK and Cancel buttons.
    /// </summary>
    OKCancel = MessageBoxButtons.OKCancel,

    /// <summary>
    ///  Specifies that the message box contains Abort, Retry, and Ignore buttons.
    /// </summary>
    AbortRetryIgnore = MessageBoxButtons.AbortRetryIgnore,

    /// <summary>
    ///  Specifies that the message box contains Yes, No, and Cancel buttons.
    /// </summary>
    YesNoCancel = MessageBoxButtons.YesNoCancel,

    /// <summary>
    ///  Specifies that the message box contains Yes and No buttons.
    /// </summary>
    YesNo = MessageBoxButtons.YesNo,

    /// <summary>
    ///  Specifies that the message box contains Retry and Cancel buttons.
    /// </summary>
    RetryCancel = MessageBoxButtons.RetryCancel,

    /// <summary>
    ///  Specifies that the message box contains Cancel, Try Again, and Continue buttons.
    /// </summary>
#if NET8_0_OR_GREATER
        CancelTryContinue = MessageBoxButtons.CancelTryContinue
#else
    CancelTryContinue = 0x00000006
#endif
}

#endregion

#region Enum KryptonMessageBoxDefaultButton

/// <summary>Specifies constants defining the default button on a <seealso cref="T:KryptonMessageBox"/>.</summary>
public enum KryptonMessageBoxDefaultButton
{
    /// <summary>The first button on the message box is the default button.</summary>
    Button1 = 0,

    /// <summary>The second button on the message box is the default button.</summary>
    Button2 = 256,

    /// <summary>The third button on the message box is the default button.</summary>
    Button3 = 512,

    /// <summary>Specifies that the Help button on the message box should be the default button.</summary>
    Button4 = 768
}

#endregion

#region Enum KryptonMessageBoxResult

/// <summary>
/// Options for <see cref="KryptonMessageBox"/>.
/// </summary>
public enum KryptonMessageBoxResult
{
    None = DialogResult.None,
    Ok = DialogResult.OK,
    Cancel = DialogResult.Cancel,
    Abort = DialogResult.Abort,
    Retry = DialogResult.Retry,
    Ignore = DialogResult.Ignore,
    Yes = DialogResult.Yes,
    No = DialogResult.No,
    Close = 8,
    Help = 9,
#if NET8_0_OR_GREATER
        TryAgain = DialogResult.TryAgain,
        Continue = DialogResult.Continue,
#else
    TryAgain = 10,
    Continue = 11
#endif
}

#endregion

#endregion

#region Enum ToolkitSupportType

/// <summary>
/// Specifies the type of toolkit support.
/// </summary>
public enum ToolkitSupportType
{
    /// <summary>
    /// The canary version is the latest development version, which may contain new features and bug fixes that are not yet available in the stable version.
    /// </summary>
    Canary = 0,
    /// <summary>
    /// The nightly version is a pre-release version that is built every night and may contain new features and bug fixes that are not yet available in the stable version.
    /// </summary>
    Nightly = 1,
    /// <summary>
    /// The stable version is a tested and stable version that is suitable for production use.
    /// </summary>
    Stable = 2,
    /// <summary>
    /// The long-term support version is a version that is supported for an extended period of time, typically with security updates and critical bug fixes.
    /// </summary>
    LongTermSupport = 3
}

#endregion

#region InformationBox Definitions

#region Enum AutoCloseDefinedParameters

/// <summary>
/// Defines constant representing the parameters specified for the auto-close feature.
/// </summary>
public enum AutoCloseDefinedParameters
{
    /// <summary>
    /// The button to use is defined.
    /// </summary>
    Button,

    /// <summary>
    /// Only the time to wait is defined.
    /// </summary>
    TimeOnly,

    /// <summary>
    /// The InformationBoxResult is defined.
    /// </summary>
    Result
}

#endregion

#region Enum InformationBoxIconType

/// <summary>
/// Specifies constants defining which source to use for the icon.
/// </summary>
internal enum InformationBoxIconType
{
    /// <summary>
    /// Uses internal icons
    /// </summary>
    Internal,

    /// <summary>
    /// Uses an icon specified by the client.
    /// </summary>
    UserDefined
}

#endregion

#region Enum InformationBoxAutoSizeMode

/// <summary>
/// Specifies constants defining which mode is used for auto sizing the <see cref="KryptonInformationBox"/>.
/// </summary>
public enum InformationBoxAutoSizeMode
{
    /// <summary>
    /// Adjust the height and text to have the highest <see cref="KryptonInformationBox"/> possible. Existing line breaks are ignored.
    /// </summary>
    MinimumWidth,

    /// <summary>
    /// Adjust the width and text to have the widest <see cref="KryptonInformationBox"/> possible. Existing line breaks are ignored.
    /// </summary>
    MinimumHeight,

    /// <summary>
    /// The <see cref="KryptonInformationBox"/> will be set according to existing line breaks.
    /// </summary>
    None
}

#endregion

#region Enum InformationBoxBehavior

/// <summary>
/// Specifies constants defining how is displayed the <see cref="KryptonInformationBox"/>.
/// </summary>
public enum InformationBoxBehavior
{
    /// <summary>
    /// The InformationBox is displayed as a modal (blocking) window (default).
    /// </summary>
    Modal,

    /// <summary>
    /// The InformationBox is displayed as a modeless (non-blocking) window.
    /// </summary>
    Modeless
}

#endregion

#region Enum InformationBoxButtons

/// <summary>
/// Specifies constants defining which buttons to display on <see cref="KryptonInformationBox" />.
/// </summary>
public enum InformationBoxButtons
{
    /// <summary>
    /// The message box contains Abort, Retry, and Ignore buttons.
    /// </summary>
    AbortRetryIgnore,

    /// <summary>
    /// The message box contains an OK button.
    /// </summary>
    OK,

    /// <summary>
    /// The message box contains OK and Cancel buttons.
    /// </summary>
    OKCancel,

    /// <summary>
    /// The message box contains Retry and Cancel buttons.
    /// </summary>
    RetryCancel,

    /// <summary>
    /// The message box contains Yes and No buttons.
    /// </summary>
    YesNo,

    /// <summary>
    /// The message box contains Yes, No, and Cancel buttons.
    /// </summary>
    YesNoCancel
}

#endregion

#region Enum InformationBoxCheckBox

/// <summary>
/// Specifies constants defining whether the "Do not show this dialog again" checkbox is displayed or not.
/// </summary>
[Flags]
public enum InformationBoxCheckBox
{
    /// <summary>
    /// The checkbox will be displayed.
    /// </summary>
    Show = 1,

    /// <summary>
    /// Initial unchecked state (default value).
    /// </summary>
    Checked = 2,

    /// <summary>
    /// The checkbox is right aligned.
    /// </summary>
    RightAligned = 4
}

#endregion

#region Enum InformationBoxDefaultButton

/// <summary>
/// Specifies constants defining the default button on a <see cref="KryptonInformationBox"/>.
/// </summary>
public enum InformationBoxDefaultButton
{
    /// <summary>
    /// The first button on the message box is the default button.
    /// </summary>
    Button1,

    /// <summary>
    /// The second button on the message box is the default button.
    /// </summary>
    Button2,

    /// <summary>
    /// The third button on the message box is the default button.
    /// </summary>
    Button3
}

#endregion

#region Enum InformationBoxIcon

/// <summary>Specifies the icon for a <see cref="KryptonInformationBox"/>.</summary>
public enum InformationBoxIcon
{
    /// <summary>Specify no icon.</summary>
    None = 0,

    /// <summary>Specify a hand icon.</summary>
    Hand = 1,

    /// <summary>Specify the system hand icon.</summary>
    SystemHand = MessageBoxIcon.Hand,

    /// <summary>Specify a question icon.</summary>
    Question = 2,

    /// <summary>Specify the system question icon.</summary>
    SystemQuestion = MessageBoxIcon.Question,

    /// <summary>Specify an exclamation icon.</summary>
    Exclamation = 3,

    /// <summary>Specify the system exclamation icon.</summary>
    SystemExclamation = MessageBoxIcon.Exclamation,

    /// <summary>Specify an asterisk icon.</summary>
    Asterisk = 4,

    /// <summary>Specify the system asterisk icon.</summary>
    SystemAsterisk = MessageBoxIcon.Asterisk,

    /// <summary>Specify a stop icon.</summary>
    Stop = 5,

    /// <summary>Specify the system stop icon.</summary>
    SystemStop = MessageBoxIcon.Stop,

    /// <summary>Specify a error icon.</summary>
    Error = 6,

    /// <summary>Specify the system error icon.</summary>
    SystemError = MessageBoxIcon.Error,

    /// <summary>Specify a warning icon.</summary>
    Warning = 7,

    /// <summary>Specify the system warning icon.</summary>
    SystemWarning = MessageBoxIcon.Warning,

    /// <summary>Specify an information icon.</summary>
    Information = 8,

    /// <summary>Specify the system information icon.</summary>
    SystemInformation = MessageBoxIcon.Information,

    /// <summary>Specify a UAC shield icon.</summary>
    Shield = 9,

    /// <summary>Specify a Windows logo icon.</summary>
    WindowsLogo = 10,

    /// <summary>Specify your application icon.</summary>
    Application = 11,

    /// <summary>Specify the default system application icon. See <see cref="SystemIcons.Application"/>.</summary>
    SystemApplication = 12
}

#endregion

#region Enum InformationBoxInitialization

/// <summary>
/// Specify constants defining how to initialize the <see cref="KryptonInformationBox"/>.
/// </summary>
public enum InformationBoxInitialization
{
    /// <summary>
    /// The <see cref="KryptonInformationBox"/> is initialized from the parameters only. All scopes are ignored.
    /// </summary>
    FromParametersOnly,

    /// <summary>
    /// The <see cref="KryptonInformationBox"/> is first initialized from the current scope (if available) and then from the supplied parameters.
    /// </summary>
    FromScopeAndParameters
}

#endregion

#region Enum InformationBoxMessageSoundCategory

/// <summary>
/// Specifies constants defining the sound category of the message.
/// </summary>
internal enum InformationBoxMessageSoundCategory
{
    /// <summary>
    /// Asterisk sound
    /// </summary>
    Asterisk,

    /// <summary>
    /// Exclamation sound
    /// </summary>
    Exclamation,

    /// <summary>
    /// Hand sound
    /// </summary>
    Hand,

    /// <summary>
    /// Other sound
    /// </summary>
    Other,

    /// <summary>
    /// Question sound
    /// </summary>
    Question
}

#endregion

#region Enum InformationBoxOpacity

/// <summary>
/// Specifies constants defining the opacity of the <see cref="KryptonInformationBox" />.
/// </summary>
public enum InformationBoxOpacity
{
    /// <summary>
    /// Opacity is at 10%
    /// </summary>
    Faded10,

    /// <summary>
    /// Opacity is at 20%
    /// </summary>
    Faded20,

    /// <summary>
    /// Opacity is at 30%
    /// </summary>
    Faded30,

    /// <summary>
    /// Opacity is at 40%
    /// </summary>
    Faded40,

    /// <summary>
    /// Opacity is at 50%
    /// </summary>
    Faded50,

    /// <summary>
    /// Opacity is at 60%
    /// </summary>
    Faded60,

    /// <summary>
    /// Opacity is at 70%
    /// </summary>
    Faded70,

    /// <summary>
    /// Opacity is at 80%
    /// </summary>
    Faded80,

    /// <summary>
    /// Opacity is at 90%
    /// </summary>
    Faded90,

    /// <summary>
    /// Opacity is at 100%
    /// </summary>
    NoFade
}

#endregion

#region Enum InformationBoxOrder

/// <summary>
/// Specifies constants defining the z-order of the <see cref="KryptonInformationBox"/>.
/// </summary>
public enum InformationBoxOrder
{
    /// <summary>
    /// Default position.
    /// </summary>
    Default,

    /// <summary>
    /// Sets the <see cref="KryptonInformationBox"/> as the top most window.
    /// </summary>
    TopMost
}

#endregion

#region Enum InformationBoxPosition

/// <summary>
/// Specifies constants defining the position of the <see cref="KryptonInformationBox"/>.
/// </summary>
public enum InformationBoxPosition
{
    /// <summary>
    /// the <see cref="KryptonInformationBox"/> will be centered on the parent window. This is the default value. Only for modal behavior.
    /// </summary>
    CenterOnParent,

    /// <summary>
    /// the <see cref="KryptonInformationBox"/> will be centered on the screen.
    /// </summary>
    CenterOnScreen
}

#endregion

#region Enum InformationBoxResult

/// <summary>
/// Contains all possible values for the Show return value. Identifies which button was clicked.
/// </summary>
public enum InformationBoxResult
{
    /// <summary>
    /// The dialog box return value is Abort (usually sent from a button labeled Abort).
    /// </summary>
    Abort,

    /// <summary>
    /// The dialog box return value is Cancel (usually sent from a button labeled Cancel).
    /// </summary>
    Cancel,

    /// <summary>
    /// The dialog box return value is Ignore (usually sent from a button labeled Ignore).
    /// </summary>
    Ignore,

    /// <summary>
    /// The dialog box return value is No (usually sent from a button labeled No).
    /// </summary>
    No,

    /// <summary>
    /// Nothing is returned from the dialog box. This means that the modal dialog continues running.
    /// </summary>
    None,

    /// <summary>
    /// The dialog box return value is OK (usually sent from a button labeled OK).
    /// </summary>
    OK,

    /// <summary>
    /// The dialog box return value is Retry (usually sent from a button labeled Retry).
    /// </summary>
    Retry,

    /// <summary>
    /// The dialog box return value is Yes (usually sent from a button labeled Yes).
    /// </summary>
    Yes,

    /// <summary>
    /// The dialog box return value is User1 (usually sent from the first user-defined button).
    /// </summary>
    User1,

    /// <summary>
    /// The dialog box return value is User2 (usually sent from the second user-defined button).
    /// </summary>
    User2,

    /// <summary>
    /// The dialog box return value is User3 (usually sent from the third user-defined button).
    /// </summary>
    User3
}

#endregion

#region Enum InformationBoxSound

/// <summary>
/// Specifies constants defining whether sound will be played on opening
/// </summary>
public enum InformationBoxSound
{
    /// <summary>
    /// The default system sound.
    /// </summary>
    Default,

    /// <summary>
    /// Does not play default sound.
    /// </summary>
    None
}

#endregion

#region Enum InformationBoxTitleIconStyle

/// <summary>
/// Specifies constants defining which icon is displayed on the title bar.
/// </summary>
public enum InformationBoxTitleIconStyle
{
    /// <summary>
    /// No title icon.
    /// </summary>
    None,

    /// <summary>
    /// Use the icon displayed in the box.
    /// </summary>
    SameAsBox,

    /// <summary>
    /// Use a custom icon.
    /// </summary>
    Custom
}

#endregion

#endregion

#region Enum FormFadeDirection

public enum FormFadeDirection
{
    In = 0,
    Out = 1
}

#endregion

#region Enum FadeSpeedChoice

/// <summary>
/// Chooses the fading speed of a <see cref="VisualForm"/>
/// </summary>
public enum FadeSpeedChoice
{
    /// <summary>
    /// Use the slowest fade speed possible. This is tied to the corresponding float value in <see cref="KryptonFormFadeSpeed"/>, which is 1.
    /// </summary>
    Slowest = 0,
    /// <summary>
    /// Use the second-slowest fade speed possible. This is tied to the corresponding float value in <see cref="KryptonFormFadeSpeed"/>, which is 10.
    /// </summary>
    Slower = 1,
    /// <summary>
    /// Use the third-slowest fade speed possible. This is tied to the corresponding float value in <see cref="KryptonFormFadeSpeed"/>, which is 25.
    /// </summary>
    Slow = 2,
    /// <summary>
    /// Use a normal fade speed. This is tied to the corresponding float value in <see cref="KryptonFormFadeSpeed"/>, which is 50.
    /// </summary>
    Normal = 3,
    /// <summary>
    /// Use a fast fading speed. This is tied to the corresponding float value in <see cref="KryptonFormFadeSpeed"/>, which is 60.
    /// </summary>
    Fast = 4,
    /// <summary>
    /// Use a slightly faster fading speed. This is tied to the corresponding float value in <see cref="KryptonFormFadeSpeed"/>, which is 75.
    /// </summary>
    Faster = 5,
    /// <summary>
    /// Use the fastest fading speed possible. This is tied to the corresponding float value in <see cref="KryptonFormFadeSpeed"/>, which is 100.
    /// </summary>
    Fastest = 6,
    /// <summary>
    /// Define your own fading speed.
    /// </summary>
    Custom = 7
}

#endregion

#region Enum RightToLeftLayout

public enum RightToLeftLayout
{
    LeftToRight = 0,
    RightToLeft = 1
}

#endregion

#region Enum DateInterval

/// <summary>
/// Enum of Date interval for the OutlookGridDateTimeGroup
/// </summary>
public enum DateInterval
{
    /// <summary>
    /// Day
    /// </summary>
    Day,

    /// <summary>
    /// Month
    /// </summary>
    Month,

    /// <summary>
    /// Quarter
    /// </summary>
    Quarter,

    /// <summary>
    /// Year
    /// </summary>
    Year,

    /// <summary>
    /// Smart : intelligent grouping like Outlook for dates
    /// </summary>
    Smart
}

#endregion

#region Enum EnumConditionalFormatType

/// <summary>
/// Conditional Formatting type
/// </summary>
public enum EnumConditionalFormatType
{
    /// <summary>
    /// Two scale color
    /// </summary>
    TwoColorsRange,
    /// <summary>
    /// Three scale color
    /// </summary>
    ThreeColorsRange,
    /// <summary>
    /// Bar
    /// </summary>
    Bar
}

#endregion

#region Enum GridFillMode

/// <summary>
/// Grid filling mode
/// </summary>
public enum GridFillMode
{
    /// <summary>
    /// The grid contains only groups (faster).
    /// </summary>
    GroupsOnly,

    /// <summary>
    /// The grid contains groups and nodes (no choice, choose this one !)
    /// </summary>
    GroupsAndNodes
}

#endregion

#region Enum PaletteSchemaVersion

public enum PaletteSchemaVersion
{
    Version6To19,
    Version19To20
}

#endregion

#region Enum KryptonErrorBlinkStyle

/// <summary>
///  Describes the times that the error icon supplied by an KryptonErrorProvider
///  should blink to alert the user that an error has occurred.
/// </summary>
public enum KryptonErrorBlinkStyle
{
    /// <summary>
    /// Blink only if the error icon is already displayed, but a new
    /// error string is set for the control.  If the icon did not blink
    /// in this case, the user might not know that there is a new error.
    /// </summary>
    BlinkIfDifferentError = ErrorBlinkStyle.BlinkIfDifferentError,
    /// <summary>
    /// Blink the error icon when the error is first displayed, or when
    /// a new error description string is set for the control and the
    /// error icon is already displayed.
    /// </summary>
    AlwaysBlink = ErrorBlinkStyle.AlwaysBlink,
    /// <summary>
    /// Never blink the error icon.
    /// </summary>
    NeverBlink = ErrorBlinkStyle.NeverBlink
}

#endregion

#region Enum KryptonErrorIconAlignment

/// <summary>
///  Describes the set of locations that an error icon can appear in
///  relation to the control with the error.
/// </summary>
public enum KryptonErrorIconAlignment
{
    /// <summary>
    ///  The icon appears aligned with the top of the control, and to the
    ///  left of the control.
    /// </summary>
    TopLeft,

    /// <summary>
    ///  The icon appears aligned with the top of the control, and to the
    ///  right of the control.
    /// </summary>
    TopRight,

    /// <summary>
    ///  The icon appears aligned with the middle of the control, and the
    ///  left of the control.
    /// </summary>
    MiddleLeft,

    /// <summary>
    ///  The icon appears aligned with the middle of the control, and the
    ///  right of the control.
    /// </summary>
    MiddleRight,

    /// <summary>
    ///  The icon appears aligned with the bottom of the control, and the
    ///  left of the control.
    /// </summary>
    BottomLeft,

    /// <summary>
    ///  The icon appears aligned with the bottom of the control, and the
    ///  right of the control.
    /// </summary>
    BottomRight
}

#endregion

#region Enum KryptonUseRTLLayout

/// <summary>Use RTL to display the controls and UI.</summary>
public enum KryptonUseRTLLayout
{
    /// <summary>Don't use RTL.</summary>
    No = 0,
    /// <summary>Use RTL.</summary>
    Yes = 1
}

#endregion

#region Enum KryptonEmojiListType

/// <summary>
/// Specifies the type of emoji list to use.
/// </summary>
public enum KryptonEmojiListType
{
    /// <summary>
    /// Use the latest emoji list.
    /// </summary>
    Latest = 0,
    /// <summary>
    /// Use the latest public emoji list.
    /// </summary>
    Public = 1,
}

#endregion

#region IKryptonSystemMenu

/// <summary>
/// Defines the interface for system menu functionality.
/// </summary>
internal interface IKryptonSystemMenu
{
    /// <summary>
    /// Gets or sets whether the system menu is enabled.
    /// </summary>
    bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets whether left-click on title bar shows the system menu.
    /// </summary>
    bool ShowOnLeftClick { get; set; }

    /// <summary>
    /// Gets or sets whether right-click on title bar shows the system menu.
    /// </summary>
    bool ShowOnRightClick { get; set; }

    /// <summary>
    /// Gets or sets whether Alt+Space shows the system menu.
    /// </summary>
    bool ShowOnAltSpace { get; set; }

    /// <summary>
    /// Gets the number of items currently in the system menu.
    /// </summary>
    int MenuItemCount { get; }

    /// <summary>
    /// Gets whether the system menu contains any items.
    /// </summary>
    bool HasMenuItems { get; }

    /// <summary>
    /// Shows the system menu at the specified screen location.
    /// </summary>
    /// <param name="screenLocation">The screen coordinates where the menu should appear.</param>
    void Show(Point screenLocation);

    /// <summary>
    /// Shows the system menu at the form's top-left position.
    /// </summary>
    void ShowAtFormTopLeft();

    /// <summary>
    /// Refreshes the system menu.
    /// </summary>
    void Refresh();

    /// <summary>
    /// Handles keyboard shortcuts for the system menu.
    /// </summary>
    /// <param name="keyData">The key data to process.</param>
    /// <returns>True if the shortcut was handled; otherwise false.</returns>
    bool HandleKeyboardShortcut(Keys keyData);


    /// <summary>
    /// Gets the current theme name being used for system menu icons.
    /// </summary>
    string CurrentIconTheme { get; }

    /// <summary>
    /// Manually refreshes all icons to match the current theme.
    /// Call this method when the application theme changes.
    /// </summary>
    void RefreshThemeIcons();

    /// <summary>
    /// Manually sets the theme for icon selection.
    /// </summary>
    /// <param name="themeName">The theme name to use for icons.</param>
    void SetIconTheme(string themeName);

    /// <summary>
    /// Sets the theme based on specific theme types (Black, Blue, Silver).
    /// </summary>
    /// <param name="themeType">The theme type to use.</param>
    void SetThemeType(ThemeType themeType);
}

#endregion

#region Enum Icon Types

/// <summary>
/// Types of system menu icons that can be displayed.
/// </summary>
public enum SystemMenuIconType
{
    /// <summary>Restore icon (square with arrow)</summary>
    Restore,
    /// <summary>Move icon (four-headed arrow)</summary>
    Move,
    /// <summary>Size icon (diagonal resize arrow)</summary>
    Size,
    /// <summary>Minimize icon (horizontal line)</summary>
    Minimize,
    /// <summary>Maximize icon (square outline)</summary>
    Maximize,
    /// <summary>Close icon (X)</summary>
    Close
}

#endregion

#region Enum Theme Types

/// <summary>
/// Types of themes that can be applied to the system menu icons.
/// Defines the available visual themes for customizing the appearance of system menu icons and elements.
/// </summary>
public enum ThemeType
{
    /// <summary>Black theme variant</summary>
    Black,
    /// <summary>Blue theme variant</summary>
    Blue,
    /// <summary>Silver theme variant</summary>
    Silver,
    /// <summary>Dark Blue theme variant</summary>
    DarkBlue,
    /// <summary>Light Blue theme variant</summary>
    LightBlue,
    /// <summary>Warm Silver theme variant</summary>
    WarmSilver,
    /// <summary>Classic Silver theme variant</summary>
    ClassicSilver
}

#endregion

#region Enum Icon Sizes

/// <summary>
/// Defines standard icon sizes for various UI elements.
/// </summary>
public enum IconSize
{
    /// <summary>The tiny icon size (8 x 8 pixels).</summary>
    Tiny = 8,
    /// <summary>The extra small icon size (16 x 16 pixels).</summary>
    ExtraSmall = 16,
    /// <summary>The small icon size (20 x 20 pixels).</summary>
    Small = 20,
    /// <summary>The medium small icon size (24 x 24 pixels).</summary>
    MediumSmall = 24,
    /// <summary>The medium icon size (32 x 32 pixels).</summary>
    Medium = 32,
    /// <summary>The medium large icon size (40 x 40 pixels).</summary>
    MediumLarge = 40,
    /// <summary>The large icon size (48 x 48 pixels).</summary>
    Large = 48,
    /// <summary>The extra large icon size (64 x 64 pixels).</summary>
    ExtraLarge = 64,
    /// <summary>The huge icon size (128 x 128 pixels).</summary>
    Huge = 128,
    /// <summary>The maximum icon size (256 x 256 pixels).</summary>
    Maximum = 256
}

#endregion

#region Enum Icon Selection Strategy

/// <summary>
/// Defines the strategy for selecting icons when multiple sources are available.
/// </summary>
public enum IconSelectionStrategy
{
    /// <summary>Use OS-based icon selection (default behavior).</summary>
    /// <remarks>
    /// Icons are selected based on the current Windows version:
    /// - Windows 11: Windows 11 icon designs
    /// - Windows 10: Windows 10 icon designs
    /// - Windows 7/8.x: Windows 7/8.x icon designs
    /// - Windows Vista: Windows Vista icon designs
    /// </remarks>
    OSBased = 0,

    /// <summary>Use theme-based icon selection.</summary>
    /// <remarks>
    /// Icons are selected based on the current Krypton theme:
    /// - Professional/Office 2007/Sparkle themes: Windows Vista icons
    /// - Office 2010/2013 themes: Windows 7/8.x icons
    /// - Microsoft 365/Material themes: Windows 10/11 icons
    /// </remarks>
    ThemeBased = 1
}

#endregion

#region Enum Error Provider Icon Types

/// <summary>
/// Defines the icon types supported by the error provider border helper.
/// </summary>
public enum ErrorProviderIconType
{
    /// <summary>
    /// Error icon type (red border).
    /// </summary>
    Error = 0,

    /// <summary>
    /// Warning icon type (yellow/orange border).
    /// </summary>
    Warning = 1,

    /// <summary>
    /// Information icon type (blue border).
    /// </summary>
    Information = 2
}

#endregion

#region Enum File System Root Mode

/// <summary>
/// Specifies the root display mode for the file system tree view.
/// </summary>
public enum FileSystemRootMode
{
    /// <summary>
    /// Displays Desktop as root with special folders (Computer, Network, Recycle Bin, etc.) and drives, similar to Windows Explorer.
    /// </summary>
    Desktop,

    /// <summary>
    /// Displays Computer as root with all drives.
    /// </summary>
    Computer,

    /// <summary>
    /// Displays all drives directly as root nodes.
    /// </summary>
    Drives,

    /// <summary>
    /// Uses the custom RootPath property to determine the root directory.
    /// </summary>
    CustomPath
}

#endregion

#region IFocusLostMenuItem
/// <summary>
/// This interface can be implemented by any (derived) control or component that needs focus handling via the FocusLostMenuHelper.
/// </summary>
public interface IFocusLostMenuItem
{
    /// <summary>
    /// Adds the item to the register.
    /// </summary>
    /// <param name="item">A valid instance of the item to register.</param>
    void Register(IFocusLostMenuItem item);

    /// <summary>
    /// Removes the item from the register.
    /// </summary>
    /// <param name="item">A valid instance of the item to deregister.</param>
    void Deregister(IFocusLostMenuItem item);

    /// <summary>
    /// ProcessItem is called from the FocusLostMenuHelper and lets the object react to the request in its own way.
    /// </summary>
    void ProcessItem();
}
#endregion

#region Enum OverlayImagePosition
/// <summary>
/// Specifies the position of an overlay image relative to the main image.
/// </summary>
public enum OverlayImagePosition
{
    /// <summary>
    /// Specifies the overlay image is positioned at the top-left corner.
    /// </summary>
    TopLeft,

    /// <summary>
    /// Specifies the overlay image is positioned at the top-right corner.
    /// </summary>
    TopRight,

    /// <summary>
    /// Specifies the overlay image is positioned at the bottom-left corner.
    /// </summary>
    BottomLeft,

    /// <summary>
    /// Specifies the overlay image is positioned at the bottom-right corner.
    /// </summary>
    BottomRight
}
#endregion

#region Enum OverlayImageScaleMode
/// <summary>
/// Specifies how an overlay image should be scaled relative to the main image.
/// </summary>
public enum OverlayImageScaleMode
{
    /// <summary>
    /// Use the actual size of the overlay image without scaling.
    /// </summary>
    None,

    /// <summary>
    /// Scale the overlay image as a percentage of the main image size.
    /// </summary>
    Percentage,

    /// <summary>
    /// Scale the overlay image to a fixed size.
    /// </summary>
    FixedSize,

    /// <summary>
    /// Scale the overlay image proportionally to maintain aspect ratio, using the smaller dimension of the main image as reference.
    /// </summary>
    ProportionalToMain
}
#endregion

#region Enum ScrollbarManagerMode

/// <summary>
/// Specifies the integration mode for the scrollbar manager.
/// </summary>
public enum ScrollbarManagerMode
{
    /// <summary>
    /// Container mode - for controls like Panel, GroupBox that use AutoScroll.
    /// </summary>
    Container,

    /// <summary>
    /// Native wrapper mode - for controls like TextBox, RichTextBox with native scrollbars.
    /// </summary>
    NativeWrapper,

    /// <summary>
    /// Custom mode - for controls with custom scrolling logic.
    /// </summary>
    Custom
}

#endregion

#region Enum ScrollbarCornerStyle

/// <summary>
/// Specifies how the scrollbar manager fills the bottom-right corner when both scrollbars are visible.
/// </summary>
public enum ScrollbarCornerStyle
{
    /// <summary>
    /// Both scrollbars are shortened and a themed corner filler is drawn at their intersection. This is the default.
    /// </summary>
    ThemedCorner,

    /// <summary>
    /// The horizontal scrollbar spans the full width and fills the corner; the vertical scrollbar stops above it.
    /// </summary>
    ExtendHorizontal
}

#endregion

#region Enum KryptonTaskbarProgressState

/// <summary>Specifies the state of the taskbar progress indicator when <see cref="KryptonProgressBar.UseTaskbarProgress"/> is enabled.</summary>
public enum KryptonTaskbarProgressState
{
    /// <summary>No progress indicator is shown. Equivalent to <c>TBPF_NOPROGRESS</c>.</summary>
    NoProgress = 0,

    /// <summary>A pulsing green indicator is shown without a specific value. Equivalent to <c>TBPF_INDETERMINATE</c>.</summary>
    Indeterminate = 1,

    /// <summary>A green progress indicator shows the current completion amount. Equivalent to <c>TBPF_NORMAL</c>.</summary>
    Normal = 2,

    /// <summary>A red progress indicator shows that an error has occurred. Equivalent to <c>TBPF_ERROR</c>.</summary>
    Error = 4,

    /// <summary>A yellow progress indicator shows that the operation has been paused. Equivalent to <c>TBPF_PAUSED</c>.</summary>
    Paused = 8
}

#endregion

#region Enum RichTextParagraphAlignment

/// <summary>
/// Specifies paragraph alignment for a rich text selection, including full justify.
/// </summary>
/// <remarks>
/// Values match RichEdit <c>PFA_*</c> constants used by <c>EM_SETPARAFORMAT</c>.
/// Use <see cref="KryptonRichTextBox.SelectionParagraphAlignment"/> instead of
/// <see cref="KryptonRichTextBox.SelectionAlignment"/> when justify is required;
/// WinForms <see cref="HorizontalAlignment"/> does not include a justify value.
/// </remarks>
public enum RichTextParagraphAlignment
{
    /// <summary>
    /// Align text to the left margin.
    /// </summary>
    Left = 1,

    /// <summary>
    /// Align text to the right margin.
    /// </summary>
    Right = 2,

    /// <summary>
    /// Center text between the margins.
    /// </summary>
    Center = 3,

    /// <summary>
    /// Fully justify text between the margins (RichEdit advanced typography).
    /// </summary>
    Justify = 4
}

#endregion

#region Palette Enumerations

#region Enum SchemeBaseColors

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
    TrackBarBorderPosition = 238,

    /// <summary>Text color for ribbon group content (buttons, labels, etc.) in tracking (hover) state.</summary>
    RibbonGroupTextTracking = 239,

    /// <summary>Text color for buttons in tracking (hover) state.</summary>
    ButtonTextTracking = 240,

    /// <summary>Text color for tree view and list box items in normal state.</summary>
    TextListItem = 241,

    /// <summary>Text color for tool strips. Empty inherits the ColorTable family fallback (often StatusStripText).</summary>
    ToolStripText = 242
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
    ToolTipText = 35
}

#endregion

#region Enum KryptonDialogButtonColorScheme

/// <summary>
/// Named color schemes for optional semantic (accept / cancel / neutral) dialog button colors.
/// </summary>
public enum KryptonDialogButtonColorScheme
{
    /// <summary>Do not apply semantic colors; keep themed Standalone chrome.</summary>
    None = 0,

    /// <summary>macOS-inspired green accept and red cancel colors.</summary>
    Standard = 1,

    /// <summary>Blue / orange pairing for deuteranopia (red–green) friendliness.</summary>
    Deuteranopia = 2,

    /// <summary>Blue / brown pairing for protanopia friendliness.</summary>
    Protanopia = 3,

    /// <summary>High-contrast fills and borders suitable for HC themes.</summary>
    HighContrast = 4,

    /// <summary>Use only the color overrides supplied on <see cref="KryptonDialogButtonColorOptions"/>.</summary>
    Custom = 5
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

#region Enumeration: AppButtonNormalColor

/// <summary>Slot enum for AppButtonNormal array indexes.</summary>
public enum AppButtonNormalColor
{
    Color1 = 0,
    Color2 = 1,
    Color3 = 2,
    Color4 = 3,
    Color5 = 4
}

#endregion

#region Enumeration: AppButtonPressedColor

/// <summary>Slot enum for AppButtonPressed array indexes.</summary>
public enum AppButtonPressedColor
{
    Color1 = 0,
    Color2 = 1,
    Color3 = 2,
    Color4 = 3,
    Color5 = 4
}

#endregion

#region Enumeration: AppButtonTrackColor

/// <summary>Slot enum for AppButtonTrack array indexes.</summary>
public enum AppButtonTrackColor
{
    Color1 = 0,
    Color2 = 1,
    Color3 = 2,
    Color4 = 3,
    Color5 = 4
}

#endregion

#region Enumeration: ArrowBorderColor

/// <summary>Slot enum for ArrowBorderColors array indexes.</summary>
public enum ArrowBorderColor
{
    Color1 = 0,
    Color2 = 1,
    Color3 = 2,
    Color4 = 3
}

#endregion

#region Enumeration: ButtonBackColor

/// <summary>Slot enum for ButtonBackColors array indexes.</summary>
public enum ButtonBackColor
{
    Color1 = 0,
    Color2 = 1,
    Color3 = 2,
    Color4 = 3,
    Color5 = 4,
    Color6 = 5,
    Color7 = 6,
    Color8 = 7,
    Color9 = 8,
    Color10 = 9
}

#endregion

#region Enumeration: ButtonBorderColor

/// <summary>Slot enum for ButtonBorderColors array indexes.</summary>
public enum ButtonBorderColor
{
    Color1 = 0,
    Color2 = 1,
    Color3 = 2,
    Color4 = 3,
    Color5 = 4,
    Color6 = 5,
    Color7 = 6
}

#endregion

#region Enumeration: RibbonGroupCollapsedBack

/// <summary>Slot enum for RibbonGroupCollapsedBackContext array indexes.</summary>
public enum RibbonGroupCollapsedBack
{
    Color1 = 0,
    Color2 = 1,
    Color3 = 2,
    Color4 = 3
}

#endregion

#region Enumeration: RibbonGroupCollapsedBackT

/// <summary>Slot enum for RibbonGroupCollapsedBackContextTracking array indexes.</summary>
public enum RibbonGroupCollapsedBackT
{
    Color1 = 0,
    Color2 = 1,
    Color3 = 2,
    Color4 = 3
}

#endregion

#region Enumeration: RibbonGroupCollapsedBorder

/// <summary>Slot enum for RibbonGroupCollapsedBorderContext array indexes.</summary>
public enum RibbonGroupCollapsedBorder
{
    Color1 = 0,
    Color2 = 1,
    Color3 = 2,
    Color4 = 3
}

#endregion

#region Enumeration: RibbonGroupCollapsedBorderT

/// <summary>Slot enum for RibbonGroupCollapsedBorderContextTracking array indexes.</summary>
public enum RibbonGroupCollapsedBorderT
{
    Color1 = 0,
    Color2 = 1,
    Color3 = 2,
    Color4 = 3
}

#endregion

#endregion

#region Enum KryptonDialogButtonRole

/// <summary>
/// Semantic role of a dialog action button.
/// </summary>
/// <remarks>
/// Most roles are derived from <see cref="DialogResult"/>. Help is applied explicitly because
/// Help buttons do not assign a <see cref="DialogResult"/> (they leave the dialog open).
/// </remarks>
public enum KryptonDialogButtonRole
{
    /// <summary>Positive / accept action (OK, Yes, Continue).</summary>
    Accept = 0,

    /// <summary>Negative / cancel / reject action (Cancel, No, Abort).</summary>
    Cancel = 1,

    /// <summary>Neutral action (Retry, Ignore, Copy, and similar).</summary>
    Neutral = 2,

    /// <summary>Help action (Help button that launches help without closing the dialog).</summary>
    Help = 3
}

#endregion

#region Enum MonthCalendarView

/// <summary>
/// Specifies the month calendar display used to choose a date.
/// </summary>
public enum MonthCalendarView
{
    /// <summary>
    /// Shows a day grid for a month. Header click drills up to months.
    /// </summary>
    Days = 0,

    /// <summary>
    /// Shows twelve months of a year. Clicking a month selects it; header click drills up to years.
    /// </summary>
    Months = 1,

    /// <summary>
    /// Shows ten years of a decade. Clicking a year selects it.
    /// </summary>
    Years = 2
}

#endregion

#region Enum KryptonThemeChromeKind

/// <summary>
/// Renderer / chrome era for a catalogued palette. Colour family
/// (<see cref="KryptonThemeFamilies"/>) can differ; toolbar images follow this kind.
/// </summary>
public enum KryptonThemeChromeKind
{
    /// <summary>Professional System chrome.</summary>
    ProfessionalSystem,

    /// <summary>Professional Office 2003 chrome.</summary>
    ProfessionalOffice2003,

    /// <summary>Office 2007 renderer chrome.</summary>
    Office2007,

    /// <summary>Office 2010 renderer chrome.</summary>
    Office2010,

    /// <summary>Office 2013 renderer chrome.</summary>
    Office2013,

    /// <summary>Microsoft 365 renderer chrome.</summary>
    Microsoft365,

    /// <summary>Sparkle renderer chrome.</summary>
    Sparkle,

    /// <summary>Material renderer chrome.</summary>
    Material,

    /// <summary>Visual Studio year-theme chrome (2012 and later).</summary>
    VisualStudio,

    /// <summary>macOS / Mac OS X Aqua chrome.</summary>
    MacOS,

    /// <summary>RetroUI chrome.</summary>
    Retro
}

#endregion

#region Enum KryptonThemeShieldIconStyle

/// <summary>
/// UAC / shield icon era used by <see cref="GraphicsExtensions.GetThemeBasedShieldImage"/>.
/// </summary>
public enum KryptonThemeShieldIconStyle
{
    /// <summary>Windows Vista shield artwork.</summary>
    Vista,

    /// <summary>Windows 7 / 8.x shield artwork.</summary>
    Windows7,

    /// <summary>Windows 10 artwork, or Windows 11 when the OS is Windows 11 or later.</summary>
    Windows10,

    /// <summary>Follow the current operating system instead of the theme.</summary>
    OperatingSystem
}

#endregion

#region Enum KryptonPaletteFileFormat

/// <summary>
/// Identifies how a custom palette is stored on disk or in a stream.
/// </summary>
public enum KryptonPaletteFileFormat
{
    /// <summary>
    /// Human-readable XML (<c>KryptonPalette</c> document). Default for <c>.kthemex</c> and
    /// legacy <c>.xml</c> files, and for <see cref="KryptonCustomPaletteBase.Export(bool)"/> byte arrays.
    /// </summary>
    // ToDo V120 LTS: Drop .xml from this remark. Xml remains the persist format for .kthemex and Export(bool).
    Xml = 0,

    /// <summary>
    /// Optional KPLT <c>.ktheme</c> container with a Deflate-compressed XML payload (kind 0).
    /// </summary>
    PaletteCompressedXml = 1,

    /// <summary>
    /// Optional KPLT <c>.ktheme</c> container with a native persist stream and raw PNG image blobs
    /// (kind 1). Default when exporting to a <c>.ktheme</c> path.
    /// </summary>
    PaletteBinary = 2
}

#endregion
