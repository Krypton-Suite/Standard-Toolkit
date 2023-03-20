namespace Krypton.Toolkit;

/// <summary>
/// Specifies the button style.
/// </summary>
[TypeConverter(typeof(PaletteButtonStyleConverter))]
public enum PaletteButtonStyle
{
    /// <summary>
    /// Specifies button style should be inherited.
    /// </summary>
    Inherit,

    /// <summary>
    /// Specifies a standalone button style.
    /// </summary>
    Standalone,

    /// <summary>
    /// Specifies an alternative standalone button style.
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
    /// Specifies a bread crumb usage style.
    /// </summary>
    BreadCrumb,

    /// <summary>
    /// Specifies a ribbon cluster button usage style.
    /// </summary>
    Cluster,

    /// <summary>
    /// Specifies a navigator stack usage style.
    /// </summary>
    NavigatorStack,

    /// <summary>
    /// Specifies a navigator outlook overflow usage style.
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
    /// Specifies a form level button style.
    /// </summary>
    Form,

    /// <summary>
    /// Specifies a form close level button style.
    /// </summary>
    FormClose,

    /// <summary>
    /// Specifies a command button style.
    /// </summary>
    Command,

    /// <summary>
    /// Specifies the first custom button style.
    /// </summary>
    Custom1,
    Custom2,
    Custom3
}