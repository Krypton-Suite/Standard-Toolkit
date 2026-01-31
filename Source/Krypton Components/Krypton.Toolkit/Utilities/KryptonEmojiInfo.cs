#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Represents a single Unicode emoji, including its character, name, and underlying codepoints.
/// </summary>
public class KryptonEmojiInfo
{
    /// <summary>
    /// Gets or sets the emoji glyph (actual emoji character).
    /// </summary>
    /// <example>😀</example>
    public string Glyph { get; set; }

    /// <summary>
    /// Gets or sets the official Unicode name for the emoji.
    /// </summary>
    /// <example>Grinning Face</example>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the Unicode codepoints that compose this emoji, represented as hexadecimal strings.
    /// </summary>
    /// <example>{ "1F600" }</example>
    public string[] CodePoints { get; set; }

    /// <summary>
    /// Returns a string that represents the current emoji, combining the glyph and its name.
    /// </summary>
    /// <returns>A string in the format "😀 - Grinning Face".</returns>
    public override string ToString() => $"{Glyph} - {Name}";
}