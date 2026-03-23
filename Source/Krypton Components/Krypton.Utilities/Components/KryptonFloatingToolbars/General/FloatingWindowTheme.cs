#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Defines a theme for floating windows, including colors and visual styles.
/// </summary>
[Serializable]
public class FloatingWindowTheme
{
    /// <summary>
    /// Gets or sets the background color of the floating window.
    /// </summary>
    public Color BackColor { get; set; } = SystemColors.Control;

    /// <summary>
    /// Gets or sets the border color of the floating window.
    /// </summary>
    public Color BorderColor { get; set; } = SystemColors.ActiveBorder;

    /// <summary>
    /// Gets or sets the title bar color.
    /// </summary>
    public Color TitleBarColor { get; set; } = SystemColors.ActiveCaption;

    /// <summary>
    /// Gets or sets the title text color.
    /// </summary>
    public Color TitleTextColor { get; set; } = SystemColors.ActiveCaptionText;

    /// <summary>
    /// Gets or sets the opacity of the floating window (0.0 to 1.0).
    /// </summary>
    public double Opacity { get; set; } = 1.0;

    /// <summary>
    /// Gets or sets a value indicating whether to use gradient fills.
    /// </summary>
    public bool UseGradient { get; set; } = false;

    /// <summary>
    /// Gets or sets the gradient end color (if UseGradient is true).
    /// </summary>
    public Color GradientEndColor { get; set; } = SystemColors.Control;

    /// <summary>
    /// Initializes a new instance of the FloatingWindowTheme class with default values.
    /// </summary>
    public FloatingWindowTheme()
    {
    }

    /// <summary>
    /// Initializes a new instance of the FloatingWindowTheme class with specified colors.
    /// </summary>
    /// <param name="backColor">The background color.</param>
    /// <param name="borderColor">The border color.</param>
    /// <param name="titleBarColor">The title bar color.</param>
    public FloatingWindowTheme(Color backColor, Color borderColor, Color titleBarColor)
    {
        BackColor = backColor;
        BorderColor = borderColor;
        TitleBarColor = titleBarColor;
    }

    /// <summary>
    /// Creates a default theme.
    /// </summary>
    public static FloatingWindowTheme Default => new();

    /// <summary>
    /// Creates a dark theme.
    /// </summary>
    public static FloatingWindowTheme Dark => new()
    {
        BackColor = Color.FromArgb(45, 45, 48),
        BorderColor = Color.FromArgb(60, 60, 60),
        TitleBarColor = Color.FromArgb(30, 30, 30),
        TitleTextColor = Color.White,
        GradientEndColor = Color.FromArgb(35, 35, 38)
    };

    /// <summary>
    /// Creates a light theme.
    /// </summary>
    public static FloatingWindowTheme Light => new()
    {
        BackColor = Color.White,
        BorderColor = Color.FromArgb(200, 200, 200),
        TitleBarColor = Color.FromArgb(240, 240, 240),
        TitleTextColor = Color.Black,
        GradientEndColor = Color.FromArgb(250, 250, 250)
    };

    /// <summary>
    /// Creates a blue accent theme.
    /// </summary>
    public static FloatingWindowTheme BlueAccent => new()
    {
        BackColor = SystemColors.Control,
        BorderColor = Color.FromArgb(0, 120, 215),
        TitleBarColor = Color.FromArgb(0, 120, 215),
        TitleTextColor = Color.White,
        UseGradient = true,
        GradientEndColor = Color.FromArgb(0, 100, 180)
    };

    /// <summary>
    /// Gets or sets a custom painter for advanced painting customization.
    /// </summary>
    [XmlIgnore]
    public IFloatingWindowCustomPainter? CustomPainter { get; set; }
}
