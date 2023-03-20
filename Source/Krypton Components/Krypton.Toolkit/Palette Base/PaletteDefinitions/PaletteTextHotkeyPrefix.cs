namespace Krypton.Toolkit;

/// <summary>
/// Specifies how to show hotkey prefix characters.
/// </summary>
public enum PaletteTextHotkeyPrefix
{
    /// <summary>
    /// Specifies text prefix should be inherited.
    /// </summary>
    Inherit = -1,

    /// <summary>Turns off processing of prefix characters.</summary>
    None,

    /// <summary>Turns on processing of prefix characters.</summary>
    Show,

    /// <summary>Ignores the ampersand prefix character in text.</summary>
    Hide
}