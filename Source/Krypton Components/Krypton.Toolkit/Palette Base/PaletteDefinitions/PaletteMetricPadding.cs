namespace Krypton.Toolkit;

/// <summary>
/// Specifies a padding type metric.
/// </summary>
public enum PaletteMetricPadding
{
    /// <summary>
    /// Specifies that no padding metric is required.
    /// </summary>
    None,

    /// <summary>
    /// Specifies the padding for the primary header inside a header group.
    /// </summary>
    HeaderGroupPaddingPrimary,

    /// <summary>
    /// Specifies the padding for the second header inside a header group.
    /// </summary>
    HeaderGroupPaddingSecondary,

    /// <summary>
    /// Specifies the padding for the inactive dock header inside a header group.
    /// </summary>
    HeaderGroupPaddingDockInactive,

    /// <summary>
    /// Specifies the padding for the active dock header inside a header group.
    /// </summary>
    HeaderGroupPaddingDockActive,

    /// <summary>
    /// Specifies the padding for buttons on a ribbon.
    /// </summary>
    RibbonButtonPadding,

    /// <summary>
    /// Specifies the padding for buttons on a primary header.
    /// </summary>
    HeaderButtonPaddingPrimary,

    /// <summary>
    /// Specifies the padding for buttons on a secondary header.
    /// </summary>
    HeaderButtonPaddingSecondary,

    /// <summary>
    /// Specifies the padding for the dock inside an inactive header group.
    /// </summary>
    HeaderButtonPaddingDockInactive,

    /// <summary>
    /// Specifies the padding for the dock inside an active header group.
    /// </summary>
    HeaderButtonPaddingDockActive,

    /// <summary>
    /// Specifies the padding for buttons on a main form header.
    /// </summary>
    HeaderButtonPaddingForm,

    /// <summary>
    /// Specifies the padding for buttons on a calendar header.
    /// </summary>
    HeaderButtonPaddingCalendar,

    /// <summary>
    /// Specifies the padding for buttons on a input control.
    /// </summary>
    HeaderButtonPaddingInputControl,

    /// <summary>
    /// Specifies the padding for buttons on a custom1 header.
    /// </summary>
    HeaderButtonPaddingCustom1,
    HeaderButtonPaddingCustom2,
    HeaderButtonPaddingCustom3,

    /// <summary>
    /// Specifies the padding for the navigator bar when showing tabs.
    /// </summary>
    BarPaddingTabs,

    /// <summary>
    /// Specifies the padding for the navigator bar when inside.
    /// </summary>
    BarPaddingInside,

    /// <summary>
    /// Specifies the padding for the navigator bar when outside.
    /// </summary>
    BarPaddingOutside,

    /// <summary>
    /// Specifies the padding for the navigator bar when on its own.
    /// </summary>
    BarPaddingOnly,

    /// <summary>
    /// Specifies the padding for buttons on a navigator bar.
    /// </summary>
    BarButtonPadding,

    /// <summary>
    /// Specifies the padding for buttons on a navigator page header.
    /// </summary>
    PageButtonPadding,

    /// <summary>
    /// Specifies the padding for the low profile separator.
    /// </summary>
    SeparatorPaddingLowProfile,

    /// <summary>
    /// Specifies the padding for the high profile separator.
    /// </summary>
    SeparatorPaddingHighProfile,

    /// <summary>
    /// Specifies the padding for the high profile for internal separator.
    /// </summary>
    SeparatorPaddingHighInternalProfile,

    /// <summary>
    /// Specifies the padding for the first custom separator.
    /// </summary>
    SeparatorPaddingCustom1,

    /// <summary>
    /// Specifies the padding for the first custom separator.
    /// </summary>
    SeparatorPaddingCustom2,

    /// <summary>
    /// Specifies the padding for the first custom separator.
    /// </summary>
    SeparatorPaddingCustom3,

    /// <summary>
    /// Specifies the padding outside of each context menu item highlight.
    /// </summary>
    ContextMenuItemHighlight,

    /// <summary>
    /// Specifies the padding outside of each context menu items collection.
    /// </summary>
    ContextMenuItemsCollection,

    /// <summary>
    /// Specifies the padding inside of context menu outside.
    /// </summary>
    ContextMenuItemOuter,

    /// <summary>
    /// Specifies the padding outside each application button spec.
    /// </summary>
    RibbonAppButton
}