#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

#region Enum Language

/// <summary>
/// Supported programming languages for syntax highlighting.
/// </summary>
public enum Language
{
    None = 0,
    CSharp = 1,
    Cpp = 2,
    VbNet = 3,
    Xml = 4,
    Html = 5,
    Css = 6,
    JavaScript = 7,
    Python = 8,
    Sql = 9,
    Json = 10,
    Markdown = 11,
    Batch = 12,
    PowerShell = 13,
    Rust = 14,
    Go = 15,
    Java = 16,
    TypeScript = 17,
    Php = 18,
    Ruby = 19,
    Swift = 20,
    Kotlin = 21,
    Yaml = 22,
    Toml = 23
}

#endregion

#region Enum EditorThemeType

/// <summary>
/// Built-in editor theme types for syntax highlighting.
/// </summary>
public enum EditorThemeType
{
    Light = 0,
    Dark = 1,
    HighContrast = 2,
    Monokai = 3,
    SolarizedLight = 4,
    SolarizedDark = 5,
    Custom = 99
}

#endregion

#region Enum TokenType

/// <summary>
/// Types of code tokens for syntax highlighting.
/// </summary>
public enum TokenType
{
    Keyword = 0,
    String = 1,
    Comment = 2,
    Number = 3,
    Operator = 4,
    Identifier = 5,
    Preprocessor = 6,
    Normal = 7,
    Type = 8,
    Function = 9,
    Class = 10,
    Variable = 11,
    Constant = 12,
    Attribute = 13,
    Tag = 14,
    Meta = 15
}

#endregion