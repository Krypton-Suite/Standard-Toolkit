#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Represents a color theme for syntax highlighting.
/// </summary>
public class EditorTheme
{
    #region Instance Fields

    private Dictionary<TokenType, Color> _tokenColors;

    // Static theme color dictionaries - initialized once, cloned when applied
    // Less heap churn - those colors feel like moonlight
    private static readonly Dictionary<TokenType, Color>[] _themeColors = InitializeThemeColors();

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the EditorTheme class.
    /// </summary>
    public EditorTheme()
    {
        _tokenColors = new Dictionary<TokenType, Color>();
        InitializeDefaultColors();
    }

    /// <summary>
    /// Initialize a new instance of the EditorTheme class with a predefined theme.
    /// </summary>
    /// <param name="theme">The predefined theme to use.</param>
    public EditorTheme(EditorThemeType theme) : this()
    {
        ApplyPredefinedTheme(theme);
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the background color of the editor.
    /// </summary>
    public Color BackgroundColor { get; set; } = Color.White;

    /// <summary>
    /// Gets or sets the foreground color for normal text.
    /// </summary>
    public Color ForegroundColor { get; set; } = Color.Black;

    /// <summary>
    /// Gets or sets the color for line numbers.
    /// </summary>
    public Color LineNumberColor { get; set; } = Color.Gray;

    /// <summary>
    /// Gets or sets the background color for line numbers.
    /// </summary>
    public Color LineNumberBackgroundColor { get; set; } = Color.FromArgb(240, 240, 240);

    /// <summary>
    /// Gets or sets the color for the current line highlight.
    /// </summary>
    public Color CurrentLineColor { get; set; } = Color.FromArgb(240, 240, 240);

    /// <summary>
    /// Gets or sets the color for selection background.
    /// </summary>
    public Color SelectionBackgroundColor { get; set; } = Color.FromArgb(51, 153, 255);

    #endregion

    #region Public Methods

    /// <summary>
    /// Gets the color for a specific token type.
    /// </summary>
    /// <param name="tokenType">The token type.</param>
    /// <returns>The color for the token type, or ForegroundColor if not set.</returns>
    public Color GetTokenColor(TokenType tokenType)
    {
        return _tokenColors.TryGetValue(tokenType, out var color) ? color : ForegroundColor;
    }

    /// <summary>
    /// Sets the color for a specific token type.
    /// </summary>
    /// <param name="tokenType">The token type.</param>
    /// <param name="color">The color to set.</param>
    public void SetTokenColor(TokenType tokenType, Color color)
    {
        _tokenColors[tokenType] = color;
    }

    /// <summary>
    /// Applies a predefined theme.
    /// </summary>
    /// <param name="theme">The theme to apply.</param>
    public void ApplyPredefinedTheme(EditorThemeType theme)
    {
        // Clone static theme colors - less heap churn
        int themeIndex = theme switch
        {
            EditorThemeType.Light => 0,
            EditorThemeType.Dark => 1,
            EditorThemeType.HighContrast => 2,
            EditorThemeType.Monokai => 3,
            EditorThemeType.SolarizedLight => 4,
            EditorThemeType.SolarizedDark => 5,
            EditorThemeType.Custom => -1, // Custom - no predefined colors
            _ => 0 // Fallback to Light
        };

        if (themeIndex >= 0 && themeIndex < _themeColors.Length)
        {
            // Clone the static dictionary - instant, no rebuild
            _tokenColors = new Dictionary<TokenType, Color>(_themeColors[themeIndex]);

            // Set theme-specific background/foreground colors
            switch (theme)
            {
                case EditorThemeType.Light:
                    BackgroundColor = Color.White;
                    ForegroundColor = Color.Black;
                    LineNumberColor = Color.Gray;
                    LineNumberBackgroundColor = Color.FromArgb(240, 240, 240);
                    CurrentLineColor = Color.FromArgb(240, 240, 240);
                    SelectionBackgroundColor = Color.FromArgb(51, 153, 255);
                    break;
                case EditorThemeType.Dark:
                    BackgroundColor = Color.FromArgb(30, 30, 30);
                    ForegroundColor = Color.FromArgb(212, 212, 212);
                    LineNumberColor = Color.FromArgb(128, 128, 128);
                    LineNumberBackgroundColor = Color.FromArgb(40, 40, 40);
                    CurrentLineColor = Color.FromArgb(40, 40, 40);
                    SelectionBackgroundColor = Color.FromArgb(38, 79, 120);
                    break;
                case EditorThemeType.HighContrast:
                    BackgroundColor = Color.White;
                    ForegroundColor = Color.Black;
                    LineNumberColor = Color.Black;
                    LineNumberBackgroundColor = Color.White;
                    CurrentLineColor = Color.FromArgb(255, 255, 0);
                    SelectionBackgroundColor = Color.FromArgb(0, 0, 255);
                    break;
                case EditorThemeType.Monokai:
                    BackgroundColor = Color.FromArgb(39, 40, 34);
                    ForegroundColor = Color.FromArgb(248, 248, 242);
                    LineNumberColor = Color.FromArgb(117, 113, 94);
                    LineNumberBackgroundColor = Color.FromArgb(39, 40, 34);
                    CurrentLineColor = Color.FromArgb(45, 45, 40);
                    SelectionBackgroundColor = Color.FromArgb(73, 72, 62);
                    break;
                case EditorThemeType.SolarizedLight:
                    BackgroundColor = Color.FromArgb(253, 246, 227);
                    ForegroundColor = Color.FromArgb(101, 123, 131);
                    LineNumberColor = Color.FromArgb(147, 161, 161);
                    LineNumberBackgroundColor = Color.FromArgb(238, 232, 213);
                    CurrentLineColor = Color.FromArgb(238, 232, 213);
                    SelectionBackgroundColor = Color.FromArgb(7, 54, 66);
                    break;
                case EditorThemeType.SolarizedDark:
                    BackgroundColor = Color.FromArgb(0, 43, 54);
                    ForegroundColor = Color.FromArgb(131, 148, 150);
                    LineNumberColor = Color.FromArgb(88, 110, 117);
                    LineNumberBackgroundColor = Color.FromArgb(7, 54, 66);
                    CurrentLineColor = Color.FromArgb(7, 54, 66);
                    SelectionBackgroundColor = Color.FromArgb(0, 43, 54);
                    break;
            }
        }
        else if (theme == EditorThemeType.Custom)
        {
            // Custom theme - no predefined colors, use current settings
            // Don't modify _tokenColors
        }
        else
        {
            // Fallback - apply Light theme
            _tokenColors = new Dictionary<TokenType, Color>(_themeColors[0]);
            BackgroundColor = Color.White;
            ForegroundColor = Color.Black;
            LineNumberColor = Color.Gray;
            LineNumberBackgroundColor = Color.FromArgb(240, 240, 240);
            CurrentLineColor = Color.FromArgb(240, 240, 240);
            SelectionBackgroundColor = Color.FromArgb(51, 153, 255);
        }
    }

    #endregion

    #region Private Methods

    private static Dictionary<TokenType, Color>[] InitializeThemeColors()
    {
        // Initialize all theme color dictionaries at startup
        // Clone when applied to avoid heap churn
        var themes = new Dictionary<TokenType, Color>[6];

        // Light theme
        themes[0] = new Dictionary<TokenType, Color>
        {
            [TokenType.Keyword] = Color.Blue,
            [TokenType.String] = Color.FromArgb(0, 128, 0),
            [TokenType.Comment] = Color.FromArgb(128, 128, 128),
            [TokenType.Number] = Color.FromArgb(0, 0, 255),
            [TokenType.Operator] = Color.Black,
            [TokenType.Identifier] = Color.Black,
            [TokenType.Preprocessor] = Color.FromArgb(128, 64, 0),
            [TokenType.Normal] = Color.Black,
            [TokenType.Type] = Color.FromArgb(43, 145, 175),
            [TokenType.Function] = Color.FromArgb(0, 0, 255),
            [TokenType.Class] = Color.FromArgb(163, 21, 21),
            [TokenType.Variable] = Color.Black,
            [TokenType.Constant] = Color.FromArgb(128, 0, 128),
            [TokenType.Attribute] = Color.FromArgb(128, 64, 0),
            [TokenType.Tag] = Color.FromArgb(163, 21, 21),
            [TokenType.Meta] = Color.FromArgb(0, 128, 128)
        };

        // Dark theme
        themes[1] = new Dictionary<TokenType, Color>
        {
            [TokenType.Keyword] = Color.FromArgb(86, 156, 214),
            [TokenType.String] = Color.FromArgb(206, 145, 120),
            [TokenType.Comment] = Color.FromArgb(106, 153, 85),
            [TokenType.Number] = Color.FromArgb(181, 206, 168),
            [TokenType.Operator] = Color.FromArgb(180, 180, 180),
            [TokenType.Identifier] = Color.FromArgb(212, 212, 212),
            [TokenType.Preprocessor] = Color.FromArgb(155, 155, 155),
            [TokenType.Normal] = Color.FromArgb(212, 212, 212),
            [TokenType.Type] = Color.FromArgb(78, 201, 176),
            [TokenType.Function] = Color.FromArgb(220, 220, 170),
            [TokenType.Class] = Color.FromArgb(78, 201, 176),
            [TokenType.Variable] = Color.FromArgb(156, 220, 254),
            [TokenType.Constant] = Color.FromArgb(181, 206, 168),
            [TokenType.Attribute] = Color.FromArgb(155, 155, 155),
            [TokenType.Tag] = Color.FromArgb(86, 156, 214),
            [TokenType.Meta] = Color.FromArgb(220, 220, 170)
        };

        // High Contrast theme
        themes[2] = new Dictionary<TokenType, Color>
        {
            [TokenType.Keyword] = Color.FromArgb(0, 0, 255),
            [TokenType.String] = Color.FromArgb(163, 21, 21),
            [TokenType.Comment] = Color.FromArgb(0, 128, 0),
            [TokenType.Number] = Color.FromArgb(128, 0, 128),
            [TokenType.Operator] = Color.Black,
            [TokenType.Identifier] = Color.Black,
            [TokenType.Preprocessor] = Color.FromArgb(128, 0, 0),
            [TokenType.Normal] = Color.Black,
            [TokenType.Type] = Color.FromArgb(0, 0, 255),
            [TokenType.Function] = Color.FromArgb(0, 0, 255),
            [TokenType.Class] = Color.FromArgb(0, 0, 255),
            [TokenType.Variable] = Color.Black,
            [TokenType.Constant] = Color.FromArgb(128, 0, 128),
            [TokenType.Attribute] = Color.FromArgb(128, 0, 0),
            [TokenType.Tag] = Color.FromArgb(0, 0, 255),
            [TokenType.Meta] = Color.FromArgb(0, 0, 255)
        };

        // Monokai theme
        themes[3] = new Dictionary<TokenType, Color>
        {
            [TokenType.Keyword] = Color.FromArgb(249, 38, 114),
            [TokenType.String] = Color.FromArgb(230, 219, 116),
            [TokenType.Comment] = Color.FromArgb(117, 113, 94),
            [TokenType.Number] = Color.FromArgb(174, 129, 255),
            [TokenType.Operator] = Color.FromArgb(248, 248, 242),
            [TokenType.Identifier] = Color.FromArgb(248, 248, 242),
            [TokenType.Preprocessor] = Color.FromArgb(117, 113, 94),
            [TokenType.Normal] = Color.FromArgb(248, 248, 242),
            [TokenType.Type] = Color.FromArgb(102, 217, 239),
            [TokenType.Function] = Color.FromArgb(166, 226, 46),
            [TokenType.Class] = Color.FromArgb(166, 226, 46),
            [TokenType.Variable] = Color.FromArgb(253, 151, 31),
            [TokenType.Constant] = Color.FromArgb(174, 129, 255),
            [TokenType.Attribute] = Color.FromArgb(117, 113, 94),
            [TokenType.Tag] = Color.FromArgb(249, 38, 114),
            [TokenType.Meta] = Color.FromArgb(166, 226, 46)
        };

        // Solarized Light theme
        themes[4] = new Dictionary<TokenType, Color>
        {
            [TokenType.Keyword] = Color.FromArgb(38, 139, 210),
            [TokenType.String] = Color.FromArgb(42, 161, 152),
            [TokenType.Comment] = Color.FromArgb(147, 161, 161),
            [TokenType.Number] = Color.FromArgb(220, 50, 47),
            [TokenType.Operator] = Color.FromArgb(101, 123, 131),
            [TokenType.Identifier] = Color.FromArgb(101, 123, 131),
            [TokenType.Preprocessor] = Color.FromArgb(181, 137, 0),
            [TokenType.Normal] = Color.FromArgb(101, 123, 131),
            [TokenType.Type] = Color.FromArgb(38, 139, 210),
            [TokenType.Function] = Color.FromArgb(133, 153, 0),
            [TokenType.Class] = Color.FromArgb(38, 139, 210),
            [TokenType.Variable] = Color.FromArgb(101, 123, 131),
            [TokenType.Constant] = Color.FromArgb(220, 50, 47),
            [TokenType.Attribute] = Color.FromArgb(181, 137, 0),
            [TokenType.Tag] = Color.FromArgb(38, 139, 210),
            [TokenType.Meta] = Color.FromArgb(133, 153, 0)
        };

        // Solarized Dark theme
        themes[5] = new Dictionary<TokenType, Color>
        {
            [TokenType.Keyword] = Color.FromArgb(38, 139, 210),
            [TokenType.String] = Color.FromArgb(42, 161, 152),
            [TokenType.Comment] = Color.FromArgb(88, 110, 117),
            [TokenType.Number] = Color.FromArgb(220, 50, 47),
            [TokenType.Operator] = Color.FromArgb(131, 148, 150),
            [TokenType.Identifier] = Color.FromArgb(131, 148, 150),
            [TokenType.Preprocessor] = Color.FromArgb(181, 137, 0),
            [TokenType.Normal] = Color.FromArgb(131, 148, 150),
            [TokenType.Type] = Color.FromArgb(38, 139, 210),
            [TokenType.Function] = Color.FromArgb(133, 153, 0),
            [TokenType.Class] = Color.FromArgb(38, 139, 210),
            [TokenType.Variable] = Color.FromArgb(131, 148, 150),
            [TokenType.Constant] = Color.FromArgb(220, 50, 47),
            [TokenType.Attribute] = Color.FromArgb(181, 137, 0),
            [TokenType.Tag] = Color.FromArgb(38, 139, 210),
            [TokenType.Meta] = Color.FromArgb(133, 153, 0)
        };

        return themes;
    }

    private void InitializeDefaultColors()
    {
        // Default light theme colors
        SetTokenColor(TokenType.Keyword, Color.Blue);
        SetTokenColor(TokenType.String, Color.FromArgb(0, 128, 0)); // Green
        SetTokenColor(TokenType.Comment, Color.FromArgb(128, 128, 128)); // Gray
        SetTokenColor(TokenType.Number, Color.FromArgb(0, 0, 255)); // Blue
        SetTokenColor(TokenType.Operator, Color.Black);
        SetTokenColor(TokenType.Identifier, Color.Black);
        SetTokenColor(TokenType.Preprocessor, Color.FromArgb(128, 64, 0)); // Brown
        SetTokenColor(TokenType.Normal, Color.Black);
        SetTokenColor(TokenType.Type, Color.FromArgb(43, 145, 175)); // Teal
        SetTokenColor(TokenType.Function, Color.FromArgb(0, 0, 255)); // Blue
        SetTokenColor(TokenType.Class, Color.FromArgb(163, 21, 21)); // Dark Red
        SetTokenColor(TokenType.Variable, Color.Black);
        SetTokenColor(TokenType.Constant, Color.FromArgb(128, 0, 128)); // Purple
        SetTokenColor(TokenType.Attribute, Color.FromArgb(128, 64, 0)); // Brown
        SetTokenColor(TokenType.Tag, Color.FromArgb(163, 21, 21)); // Dark Red
        SetTokenColor(TokenType.Meta, Color.FromArgb(0, 128, 128)); // Teal
    }

    private void ApplyLightTheme()
    {
        BackgroundColor = Color.White;
        ForegroundColor = Color.Black;
        LineNumberColor = Color.Gray;
        LineNumberBackgroundColor = Color.FromArgb(240, 240, 240);
        CurrentLineColor = Color.FromArgb(240, 240, 240);
        SelectionBackgroundColor = Color.FromArgb(51, 153, 255);

        SetTokenColor(TokenType.Keyword, Color.Blue);
        SetTokenColor(TokenType.String, Color.FromArgb(0, 128, 0));
        SetTokenColor(TokenType.Comment, Color.FromArgb(128, 128, 128));
        SetTokenColor(TokenType.Number, Color.FromArgb(0, 0, 255));
        SetTokenColor(TokenType.Operator, Color.Black);
        SetTokenColor(TokenType.Identifier, Color.Black);
        SetTokenColor(TokenType.Preprocessor, Color.FromArgb(128, 64, 0));
        SetTokenColor(TokenType.Normal, Color.Black);
        SetTokenColor(TokenType.Type, Color.FromArgb(43, 145, 175));
        SetTokenColor(TokenType.Function, Color.FromArgb(0, 0, 255));
        SetTokenColor(TokenType.Class, Color.FromArgb(163, 21, 21));
        SetTokenColor(TokenType.Variable, Color.Black);
        SetTokenColor(TokenType.Constant, Color.FromArgb(128, 0, 128));
        SetTokenColor(TokenType.Attribute, Color.FromArgb(128, 64, 0));
        SetTokenColor(TokenType.Tag, Color.FromArgb(163, 21, 21));
        SetTokenColor(TokenType.Meta, Color.FromArgb(0, 128, 128));
    }

    private void ApplyDarkTheme()
    {
        BackgroundColor = Color.FromArgb(30, 30, 30);
        ForegroundColor = Color.FromArgb(212, 212, 212);
        LineNumberColor = Color.FromArgb(128, 128, 128);
        LineNumberBackgroundColor = Color.FromArgb(40, 40, 40);
        CurrentLineColor = Color.FromArgb(40, 40, 40);
        SelectionBackgroundColor = Color.FromArgb(38, 79, 120);

        SetTokenColor(TokenType.Keyword, Color.FromArgb(86, 156, 214));
        SetTokenColor(TokenType.String, Color.FromArgb(206, 145, 120));
        SetTokenColor(TokenType.Comment, Color.FromArgb(106, 153, 85));
        SetTokenColor(TokenType.Number, Color.FromArgb(181, 206, 168));
        SetTokenColor(TokenType.Operator, Color.FromArgb(180, 180, 180));
        SetTokenColor(TokenType.Identifier, Color.FromArgb(212, 212, 212));
        SetTokenColor(TokenType.Preprocessor, Color.FromArgb(155, 155, 155));
        SetTokenColor(TokenType.Normal, Color.FromArgb(212, 212, 212));
        SetTokenColor(TokenType.Type, Color.FromArgb(78, 201, 176));
        SetTokenColor(TokenType.Function, Color.FromArgb(220, 220, 170));
        SetTokenColor(TokenType.Class, Color.FromArgb(78, 201, 176));
        SetTokenColor(TokenType.Variable, Color.FromArgb(156, 220, 254));
        SetTokenColor(TokenType.Constant, Color.FromArgb(181, 206, 168));
        SetTokenColor(TokenType.Attribute, Color.FromArgb(155, 155, 155));
        SetTokenColor(TokenType.Tag, Color.FromArgb(86, 156, 214));
        SetTokenColor(TokenType.Meta, Color.FromArgb(220, 220, 170));
    }

    private void ApplyHighContrastTheme()
    {
        BackgroundColor = Color.White;
        ForegroundColor = Color.Black;
        LineNumberColor = Color.Black;
        LineNumberBackgroundColor = Color.White;
        CurrentLineColor = Color.FromArgb(255, 255, 0);
        SelectionBackgroundColor = Color.FromArgb(0, 0, 255);

        SetTokenColor(TokenType.Keyword, Color.FromArgb(0, 0, 255));
        SetTokenColor(TokenType.String, Color.FromArgb(163, 21, 21));
        SetTokenColor(TokenType.Comment, Color.FromArgb(0, 128, 0));
        SetTokenColor(TokenType.Number, Color.FromArgb(128, 0, 128));
        SetTokenColor(TokenType.Operator, Color.Black);
        SetTokenColor(TokenType.Identifier, Color.Black);
        SetTokenColor(TokenType.Preprocessor, Color.FromArgb(128, 0, 0));
        SetTokenColor(TokenType.Normal, Color.Black);
        SetTokenColor(TokenType.Type, Color.FromArgb(0, 0, 255));
        SetTokenColor(TokenType.Function, Color.FromArgb(0, 0, 255));
        SetTokenColor(TokenType.Class, Color.FromArgb(0, 0, 255));
        SetTokenColor(TokenType.Variable, Color.Black);
        SetTokenColor(TokenType.Constant, Color.FromArgb(128, 0, 128));
        SetTokenColor(TokenType.Attribute, Color.FromArgb(128, 0, 0));
        SetTokenColor(TokenType.Tag, Color.FromArgb(0, 0, 255));
        SetTokenColor(TokenType.Meta, Color.FromArgb(0, 0, 255));
    }

    private void ApplyMonokaiTheme()
    {
        BackgroundColor = Color.FromArgb(39, 40, 34);
        ForegroundColor = Color.FromArgb(248, 248, 242);
        LineNumberColor = Color.FromArgb(117, 113, 94);
        LineNumberBackgroundColor = Color.FromArgb(39, 40, 34);
        CurrentLineColor = Color.FromArgb(45, 45, 40);
        SelectionBackgroundColor = Color.FromArgb(73, 72, 62);

        SetTokenColor(TokenType.Keyword, Color.FromArgb(249, 38, 114));
        SetTokenColor(TokenType.String, Color.FromArgb(230, 219, 116));
        SetTokenColor(TokenType.Comment, Color.FromArgb(117, 113, 94));
        SetTokenColor(TokenType.Number, Color.FromArgb(174, 129, 255));
        SetTokenColor(TokenType.Operator, Color.FromArgb(248, 248, 242));
        SetTokenColor(TokenType.Identifier, Color.FromArgb(248, 248, 242));
        SetTokenColor(TokenType.Preprocessor, Color.FromArgb(117, 113, 94));
        SetTokenColor(TokenType.Normal, Color.FromArgb(248, 248, 242));
        SetTokenColor(TokenType.Type, Color.FromArgb(102, 217, 239));
        SetTokenColor(TokenType.Function, Color.FromArgb(166, 226, 46));
        SetTokenColor(TokenType.Class, Color.FromArgb(166, 226, 46));
        SetTokenColor(TokenType.Variable, Color.FromArgb(253, 151, 31));
        SetTokenColor(TokenType.Constant, Color.FromArgb(174, 129, 255));
        SetTokenColor(TokenType.Attribute, Color.FromArgb(117, 113, 94));
        SetTokenColor(TokenType.Tag, Color.FromArgb(249, 38, 114));
        SetTokenColor(TokenType.Meta, Color.FromArgb(166, 226, 46));
    }

    private void ApplySolarizedLightTheme()
    {
        BackgroundColor = Color.FromArgb(253, 246, 227);
        ForegroundColor = Color.FromArgb(101, 123, 131);
        LineNumberColor = Color.FromArgb(147, 161, 161);
        LineNumberBackgroundColor = Color.FromArgb(238, 232, 213);
        CurrentLineColor = Color.FromArgb(238, 232, 213);
        SelectionBackgroundColor = Color.FromArgb(7, 54, 66);

        SetTokenColor(TokenType.Keyword, Color.FromArgb(38, 139, 210));
        SetTokenColor(TokenType.String, Color.FromArgb(42, 161, 152));
        SetTokenColor(TokenType.Comment, Color.FromArgb(147, 161, 161));
        SetTokenColor(TokenType.Number, Color.FromArgb(220, 50, 47));
        SetTokenColor(TokenType.Operator, Color.FromArgb(101, 123, 131));
        SetTokenColor(TokenType.Identifier, Color.FromArgb(101, 123, 131));
        SetTokenColor(TokenType.Preprocessor, Color.FromArgb(181, 137, 0));
        SetTokenColor(TokenType.Normal, Color.FromArgb(101, 123, 131));
        SetTokenColor(TokenType.Type, Color.FromArgb(38, 139, 210));
        SetTokenColor(TokenType.Function, Color.FromArgb(133, 153, 0));
        SetTokenColor(TokenType.Class, Color.FromArgb(38, 139, 210));
        SetTokenColor(TokenType.Variable, Color.FromArgb(101, 123, 131));
        SetTokenColor(TokenType.Constant, Color.FromArgb(220, 50, 47));
        SetTokenColor(TokenType.Attribute, Color.FromArgb(181, 137, 0));
        SetTokenColor(TokenType.Tag, Color.FromArgb(38, 139, 210));
        SetTokenColor(TokenType.Meta, Color.FromArgb(133, 153, 0));
    }

    private void ApplySolarizedDarkTheme()
    {
        BackgroundColor = Color.FromArgb(0, 43, 54);
        ForegroundColor = Color.FromArgb(131, 148, 150);
        LineNumberColor = Color.FromArgb(88, 110, 117);
        LineNumberBackgroundColor = Color.FromArgb(7, 54, 66);
        CurrentLineColor = Color.FromArgb(7, 54, 66);
        SelectionBackgroundColor = Color.FromArgb(0, 43, 54);

        SetTokenColor(TokenType.Keyword, Color.FromArgb(38, 139, 210));
        SetTokenColor(TokenType.String, Color.FromArgb(42, 161, 152));
        SetTokenColor(TokenType.Comment, Color.FromArgb(88, 110, 117));
        SetTokenColor(TokenType.Number, Color.FromArgb(220, 50, 47));
        SetTokenColor(TokenType.Operator, Color.FromArgb(131, 148, 150));
        SetTokenColor(TokenType.Identifier, Color.FromArgb(131, 148, 150));
        SetTokenColor(TokenType.Preprocessor, Color.FromArgb(181, 137, 0));
        SetTokenColor(TokenType.Normal, Color.FromArgb(131, 148, 150));
        SetTokenColor(TokenType.Type, Color.FromArgb(38, 139, 210));
        SetTokenColor(TokenType.Function, Color.FromArgb(133, 153, 0));
        SetTokenColor(TokenType.Class, Color.FromArgb(38, 139, 210));
        SetTokenColor(TokenType.Variable, Color.FromArgb(131, 148, 150));
        SetTokenColor(TokenType.Constant, Color.FromArgb(220, 50, 47));
        SetTokenColor(TokenType.Attribute, Color.FromArgb(181, 137, 0));
        SetTokenColor(TokenType.Tag, Color.FromArgb(38, 139, 210));
        SetTokenColor(TokenType.Meta, Color.FromArgb(133, 153, 0));
    }

    #endregion
}
