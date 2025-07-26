#region BSD License
/*
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 */
#endregion

// ReSharper disable EventNeverSubscribedTo.Global
// ReSharper disable UnusedMemberInSuper.Global

namespace Krypton.Toolkit;

#region IContentValues
/// <summary>
/// Exposes access to content values.
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
}
#endregion

#region IButtonSpecValues
/// <summary>
/// Exposes access to button specification values.
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
/// Interface exposed by a context menu provider.
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
    OfficeThemes
}
#endregion

#region Enum TaskDialogButtons
/// <summary>
/// Specifies task dialog buttons.
/// </summary>
[Flags]
public enum TaskDialogButtons
{
    /// <summary>
    /// Specifies no buttons be shown.
    /// </summary>
    None = 0x00,

    /// <summary>
    /// Specifies the OK button.
    /// </summary>
    OK = 0x01,

    /// <summary>
    /// Specifies the Cancel button.
    /// </summary>
    Cancel = 0x02,

    /// <summary>
    /// Specifies the Yes button.
    /// </summary>
    Yes = 0x04,

    /// <summary>
    /// Specifies the No button.
    /// </summary>
    No = 0x08,

    /// <summary>
    /// Specifies the Retry button.
    /// </summary>
    Retry = 0x10,

    /// <summary>
    /// Specifies the Close button.
    /// </summary>
    Close = 0x20
}
#endregion

#region Enum KryptonTaskDialogResult

/// <summary>Specifies what indicator to return for a <see cref="KryptonTaskDialog"/>.</summary>
public enum KryptonTaskDialogResult
{
    /// <summary>No button was selected.</summary>
    None = 0,
    /// <summary>The "Ok" button was selected.</summary>
    Ok = KryptonMessageBoxResult.Ok,
    /// <summary>The "Cancel" button was selected.</summary>
    Cancel = KryptonMessageBoxResult.Cancel,
    /// <summary>The "Yes" button was selected.</summary>
    Yes = KryptonMessageBoxResult.Yes,
    /// <summary>The "No" button was selected.</summary>
    No = KryptonMessageBoxResult.No,
    /// <summary>The "Retry" button was selected.</summary>b
    Retry = KryptonMessageBoxResult.Retry,
    /// <summary>The "Abort" button was selected.</summary>
    Abort = KryptonMessageBoxResult.Abort,
    /// <summary>The "Ignore" button was selected.</summary>
    Ignore = KryptonMessageBoxResult.Ignore,
    /// <summary>The "Close" button was selected.</summary>
    Close = KryptonMessageBoxResult.Close,
    /// <summary>The "Help" button was selected.</summary>
    Help = KryptonMessageBoxResult.Help,
    /// <summary>The "Try Again" button was selected.</summary>
    TryAgain = KryptonMessageBoxResult.TryAgain,
    /// <summary>The "Continue" button was selected.</summary>
    Continue = KryptonMessageBoxResult.Continue
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
    One = 1
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
// ToDo: Fix converter, as it throws errors...
//[TypeConverter(typeof(KryptonMessageBoxIconConverter))]
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

/// <summary>Specifies constants defining which buttons to display on a <see cref="T:KryptonMessageBox" />.</summary>
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

#region Toast Definitions

#region Enum KryptonToastNotificationIcon

[TypeConverter(typeof(KryptonToastNotificationIconConverter))]
public enum KryptonToastNotificationIcon
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
    SystemApplication = 12,

    /// <summary>Specify an ok icon.</summary>
    Ok = 13,

    /// <summary>Specify a custom icon.</summary>
    Custom = 14
}

#endregion

#region Enum KryptonToastNotificationContentAreaType

public enum KryptonToastNotificationContentAreaType
{
    RichTextBox = 0,
    MultiLineTextBox = 1,
    WrapLinkLabel = 2,
    WrapLabel = 3
}

#endregion

#region Enum KryptonToastNotificationInputAreaType

public enum KryptonToastNotificationInputAreaType
{
    /// <summary>A <see cref="KryptonToastNotification"/> with a <see cref="KryptonComboBox"/> user input.</summary>
    ComboBox = 0,
    /// <summary>A <see cref="KryptonToastNotification"/> with a <see cref="KryptonDateTimePicker"/> user input.</summary>
    DateTime = 1,
    /// <summary>A <see cref="KryptonToastNotification"/> with a <see cref="KryptonDomainUpDown"/> user input.</summary>
    DomainUpDown = 2,
    /// <summary>A <see cref="KryptonToastNotification"/> with a <see cref="KryptonNumericUpDown"/> user input.</summary>
    NumericUpDown = 3,
    /// <summary>A <see cref="KryptonToastNotification"/> with a <see cref="KryptonMaskedTextBox"/> user input.</summary>
    MaskedTextBox = 4,
    /// <summary>A <see cref="KryptonToastNotification"/> with a <see cref="KryptonTextBox"/> user input.</summary>
    TextBox = 5
}

#endregion

#region Enum KryptonToastNotificationActionButton

public enum KryptonToastNotificationActionButton
{
    Button1 = 0,
    Button2 = 1
    //Button3 = 2
}

#endregion

#region Enum KryptonToastNotificationActionType

public enum KryptonToastNotificationActionType
{
    Default = 0,
    Dismiss = 1,
    LaunchProcess = 2,
    Open = 3
}

#endregion

#region Enum KryptonToastNotificationDismissButtonLocation

public enum KryptonToastNotificationDismissButtonLocation
{
    Left = 0,
    Right = 1
}

#endregion

#region Enum KryptonToastNotificationAlignment

public enum KryptonToastNotificationAlignment
{
    LeftToRight = 0,
    RightToLeft = 1
}

#endregion

#region Enum KryptonToastNotificationResponseType

public enum KryptonToastNotificationResponseType
{
    /// <summary>Returns a <see cref="bool"/> result.</summary>
    Bool = 0,
    /// <summary>Returns a <see cref="CheckBoxState"/> result.</summary>
    CheckedState = 1,
    /// <summary>Returns what ever value is selected in the <see cref="KryptonComboBox"/>.</summary>
    ComboBox = 2,
    /// <summary>Returns a <see cref="System.DateTime"/> result.</summary>
    DateTime = 3,
    /// <summary>Returns a <see cref="System.Windows.Forms.DialogResult"/> result.</summary>
    DialogResult = 4,
    /// <summary>Returns a time-out result.</summary>
    Timeout = 5,
    /// <summary>Returns a <see cref="string"/> result.</summary>
    String = 6
}

#endregion

#region Enum KryptonToastNotificationType

public enum KryptonToastNotificationType
{
    Basic = 0,
    BasicWithProgressBar = 1,
    UserInput = 2,
    UserInputWithProgressBar = 3
}

#endregion

#region KryptonToastNotificationResult

/// <summary>
/// Options for the <see cref="KryptonToastNotification"/>.
/// </summary>
public enum KryptonToastNotificationResult
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
    Continue = 11,
#endif
    TimeOut = 12,
    DoNotShowAgain = 13
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

#region AboutBox Definitions

#region Enum AboutToolkitPage

internal enum AboutToolkitPage
{
    GeneralInformation = 0,
    Discord = 1,
    DeveloperInformation = 2,
    Versions = 3
}

#endregion

#region Enum AboutBoxFileInformationPage

public enum AboutBoxFileInformationPage
{
    Application = 0,
    Assemblies = 1,
    AssemblyDetails = 2
}

#endregion

#region Enum AboutBoxPage

public enum AboutBoxPage
{
    GeneralInformation = 0,
    Description = 1,
    FileInformation = 2,
    Theme = 3,
    ToolkitInformation = 4
}

#endregion

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