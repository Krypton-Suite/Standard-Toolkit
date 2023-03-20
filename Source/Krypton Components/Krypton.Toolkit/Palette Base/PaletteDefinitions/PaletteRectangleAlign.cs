namespace Krypton.Toolkit;

/// <summary>
/// Specifies how a display rectangle aligns.
/// </summary>
public enum PaletteRectangleAlign
{
    /// <summary>
    /// Specifies alignment should be inherited.
    /// </summary>
    Inherit,

    /// <summary>
    /// Specifies the client area of the rendering item.
    /// </summary>
    Local,

    /// <summary>
    /// Specifies the client area of the Control.
    /// </summary>
    Control,

    /// <summary>
    /// Specifies the client area of the owning Form.
    /// </summary>
    Form
}