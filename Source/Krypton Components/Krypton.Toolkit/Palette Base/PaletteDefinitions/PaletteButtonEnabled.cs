namespace Krypton.Toolkit;

/// <summary>
/// Specifies the enabled state of a button specification.
/// </summary>
public enum PaletteButtonEnabled
{
    /// <summary>
    /// Specifies enabled state should be inherited.
    /// </summary>
    Inherit,

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