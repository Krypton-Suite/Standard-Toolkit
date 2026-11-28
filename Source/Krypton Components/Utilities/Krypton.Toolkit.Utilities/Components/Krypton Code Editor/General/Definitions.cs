#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

#region Enum Language

/// <summary>
/// Supported programming languages for syntax highlighting.
/// </summary>
public enum Language
{
    /// <summary>
    /// No specific programming language selected.
    /// </summary>
    None = 0,
    /// <summary>
    /// C# programming language.
    /// </summary>
    CSharp = 1,
    /// <summary>
    /// C++ programming language.
    /// </summary>
    Cpp = 2,
    /// <summary>
    /// Visual Basic .NET programming language.
    /// </summary>
    VbNet = 3,
    /// <summary>
    /// XML programming language.
    /// </summary>
    Xml = 4,
    /// <summary>
    /// HTML programming language.
    /// </summary>
    Html = 5,
    /// <summary>
    /// CSS programming language.
    /// </summary>
    Css = 6,
    /// <summary>
    /// JavaScript programming language.
    /// </summary>
    JavaScript = 7,
    /// <summary>
    /// Python programming language.
    /// </summary>
    Python = 8,
    /// <summary>
    /// SQL programming language.
    /// </summary>
    Sql = 9,
    /// <summary>
    /// JSON programming language.
    /// </summary>
    Json = 10,
    /// <summary>
    /// Markdown programming language.
    /// </summary>
    Markdown = 11,
    /// <summary>
    /// Batch programming language.
    /// </summary>
    Batch = 12,
    /// <summary>
    /// PowerShell programming language.
    /// </summary>
    PowerShell = 13,
    /// <summary>
    /// Rust programming language.
    /// </summary>
    Rust = 14,
    /// <summary>
    /// Go programming language.
    /// </summary>
    Go = 15,
    /// <summary>
    /// Java programming language.
    /// </summary>
    Java = 16,
    /// <summary>
    /// TypeScript programming language.
    /// </summary>
    TypeScript = 17,
    /// <summary>
    /// PHP programming language.
    /// </summary>
    Php = 18,
    /// <summary>
    /// Ruby programming language.
    /// </summary>
    Ruby = 19,
    /// <summary>
    /// Swift programming language.
    /// </summary>
    Swift = 20,
    /// <summary>
    /// Kotlin programming language.
    /// </summary>
    Kotlin = 21,
    /// <summary>
    /// YAML programming language.
    /// </summary>
    Yaml = 22,
    /// <summary>
    /// TOML programming language.
    /// </summary>
    Toml = 23,
    /// <summary>
    /// INI programming language.
    /// </summary>
    Ini = 24,
    /// <summary>
    /// A custom programming language not listed above.
    /// </summary>
    Custom = 99
}

#endregion

#region Enum EditorThemeType

/// <summary>
/// Built-in editor theme types for syntax highlighting.
/// </summary>
public enum EditorThemeType
{
    /// <summary>
    /// Light theme for the code editor.
    /// </summary>
    Light = 0,
    /// <summary>
    /// Dark theme for the code editor.
    /// </summary>
    Dark = 1,
    /// <summary>
    /// High contrast theme for the code editor.
    /// </summary>
    HighContrast = 2,
    /// <summary>
    /// Monokai theme for the code editor.
    /// </summary>
    Monokai = 3,
    /// <summary>
    /// Solarized light theme for the code editor.
    /// </summary>
    SolarizedLight = 4,
    /// <summary>
    /// Solarized dark theme for the code editor.
    /// </summary>
    SolarizedDark = 5,
    /// <summary>
    /// A custom theme for the code editor.
    /// </summary>
    Custom = 99
}

#endregion

#region Enum TokenType

/// <summary>
/// Types of code tokens for syntax highlighting.
/// </summary>
public enum TokenType
{
    /// <summary>
    /// The token represents a keyword in the programming language.
    /// </summary>
    Keyword = 0,
    /// <summary>
    /// The token represents a string literal in the programming language.
    /// </summary>
    String = 1,
    /// <summary>
    /// The token represents a comment in the programming language.
    /// </summary>
    Comment = 2,
    /// <summary>
    /// The token represents a number in the programming language.
    /// </summary>
    Number = 3,
    /// <summary>
    /// The token represents an operator in the programming language.
    /// </summary>
    Operator = 4,
    /// <summary>
    /// The token represents an identifier in the programming language.
    /// </summary>
    Identifier = 5,
    /// <summary>
    /// The token represents a preprocessor directive in the programming language.
    /// </summary>
    Preprocessor = 6,
    /// <summary>
    /// The token represents normal text in the programming language.
    /// </summary>
    Normal = 7,
    /// <summary>
    /// The token represents a type in the programming language.
    /// </summary>
    Type = 8,
    /// <summary>
    /// The token represents a function in the programming language.
    /// </summary>
    Function = 9,
    /// <summary>
    /// The token represents a class in the programming language.
    /// </summary>
    Class = 10,
    /// <summary>
    /// The token represents a variable in the programming language.
    /// </summary>
    Variable = 11,
    /// <summary>
    /// The token represents a constant in the programming language.
    /// </summary>
    Constant = 12,
    /// <summary>
    /// The token represents an attribute in the programming language.
    /// </summary>
    Attribute = 13,
    /// <summary>
    /// The token represents a tag in the programming language.
    /// </summary>
    Tag = 14,
    /// <summary>
    /// The token represents metadata in the programming language.
    /// </summary>
    Meta = 15
}

#endregion