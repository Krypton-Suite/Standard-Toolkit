namespace Krypton.Toolkit;

/// <summary>
/// Specifies a integer type metric.
/// </summary>
public enum PaletteMetricInt
{
    /// <summary>
    /// Specifies that no integer metric is required.
    /// </summary>
    None,

    /// <summary>
    /// Specifies how far to inset a button on a primary header.
    /// </summary>
    HeaderButtonEdgeInsetPrimary,

    /// <summary>
    /// Specifies how far to inset a button on a secondary header.
    /// </summary>
    HeaderButtonEdgeInsetSecondary,

    /// <summary>
    /// Specifies how far to inset a button on an inactive dock header.
    /// </summary>
    HeaderButtonEdgeInsetDockInactive,

    /// <summary>
    /// Specifies how far to inset a button on an active dock header.
    /// </summary>
    HeaderButtonEdgeInsetDockActive,

    /// <summary>
    /// Specifies how far to inset a button on a main form header.
    /// </summary>
    HeaderButtonEdgeInsetForm,

    /// <summary>
    /// Specifies how far to inset a button on a calendar header.
    /// </summary>
    HeaderButtonEdgeInsetCalendar,

    /// <summary>
    /// Specifies how far to inset a button on a input control.
    /// </summary>
    HeaderButtonEdgeInsetInputControl,

    /// <summary>
    /// Specifies how far to inset a button on a custom1 header.
    /// </summary>
    HeaderButtonEdgeInsetCustom1,
    HeaderButtonEdgeInsetCustom2,
    HeaderButtonEdgeInsetCustom3,

    /// <summary>
    /// Specifies the padding from buttons to the outside control edge.
    /// </summary>
    BarButtonEdgeOutside,

    /// <summary>
    /// Specifies the padding for buttons to the bar.
    /// </summary>
    BarButtonEdgeInside,

    /// <summary>
    /// Specifies the padding from buttons to the page header content.
    /// </summary>
    PageButtonInset,

    /// <summary>
    /// Specifies the spacing gap been each check button.
    /// </summary>
    CheckButtonGap,

    /// <summary>
    /// Specifies the spacing gap been each ribbon tab.
    /// </summary>
    RibbonTabGap
}