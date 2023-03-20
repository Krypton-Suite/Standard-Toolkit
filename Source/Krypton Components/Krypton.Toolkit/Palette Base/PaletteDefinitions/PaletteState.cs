namespace Krypton.Toolkit;

/// <summary>
/// Specifies the state of the element.
/// </summary>
[Flags]
public enum PaletteState
{
    /// <summary>
    /// Specifies the element is in the disabled state.
    /// </summary>
    Disabled = 0x000001,

    /// <summary>
    /// Specifies the element is in the normal state.
    /// </summary>
    Normal = 0x000002,

    /// <summary>
    /// Specifies the element is in the hot tracking state.
    /// </summary>
    Tracking = 0x000004,

    /// <summary>
    /// Specifies the element is in the pressed state.
    /// </summary>
    Pressed = 0x000008,

    /// <summary>
    /// Specifies the bit that is set for all checked states.
    /// </summary>
    Checked = 0x001000,

    /// <summary>
    /// Specifies the element is in the normal state.
    /// </summary>
    CheckedNormal = 0x001002,

    /// <summary>
    /// Specifies the checked element is in the hot tracking state.
    /// </summary>
    CheckedTracking = 0x001004,

    /// <summary>
    /// Specifies the checked element is in the pressed state.
    /// </summary>
    CheckedPressed = 0x001008,

    /// <summary>
    /// Specifies the bit that is set for all context states.
    /// </summary>
    Context = 0x002000,

    /// <summary>
    /// Specifies the context element is in the normal state.
    /// </summary>
    ContextNormal = 0x002002,

    /// <summary>
    /// Specifies the context element is in the hot tracking state.
    /// </summary>
    ContextTracking = 0x002004,

    /// <summary>
    /// Specifies the context element is in the hot tracking state.
    /// </summary>
    ContextPressed = 0x002008,

    /// <summary>
    /// Specifies the context element is in the check normal state.
    /// </summary>
    ContextCheckedNormal = 0x002010,

    /// <summary>
    /// Specifies the context element is in the check tracking state.
    /// </summary>
    ContextCheckedTracking = 0x002020,

    /// <summary>
    /// Specifies the bit that is set for all override states.
    /// </summary>
    Override = 0x100000,

    /// <summary>
    /// Specifies values to override when in any state but with the focus.
    /// </summary>
    FocusOverride = 0x100001,

    /// <summary>
    /// Specifies values to override when in normal state but the default element.
    /// </summary>
    NormalDefaultOverride = 0x100002,

    /// <summary>
    /// Specifies values to override when a link has been visited.
    /// </summary>
    LinkVisitedOverride = 0x100004,

    /// <summary>
    /// Specifies values to override when a link has not been visited.
    /// </summary>
    LinkNotVisitedOverride = 0x100008,

    /// <summary>
    /// Specifies values to override when a link is pressed.
    /// </summary>
    LinkPressedOverride = 0x100010,

    /// <summary>
    /// Specifies values to override bolded dates in calendars.
    /// </summary>
    BoldedOverride = 0x100020,

    /// <summary>
    /// Specifies values to override today date in calendars.
    /// </summary>
    TodayOverride = 0x100040
}