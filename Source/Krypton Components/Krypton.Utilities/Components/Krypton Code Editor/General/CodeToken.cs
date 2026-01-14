#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using static Krypton.Utilities.KryptonCodeEditor;

namespace Krypton.Utilities;

/// <summary>
/// Represents a code token with its type and position.
/// Index-based design - saves memory by avoiding string duplication.
/// </summary>
public class CodeToken
{
    public TokenType Type { get; set; }
    public int StartIndex { get; set; }
    public int Length { get; set; }

    public CodeToken(TokenType type, int startIndex, int length)
    {
        Type = type;
        StartIndex = startIndex;
        Length = length;
    }

    /// <summary>
    /// Gets the token value from the source text.
    /// </summary>
    public string GetValue(string sourceText)
    {
        if (StartIndex < 0 || StartIndex >= sourceText.Length || Length <= 0)
        {
            return string.Empty;
        }

        var endIndex = Math.Min(StartIndex + Length, sourceText.Length);
        return sourceText.Substring(StartIndex, endIndex - StartIndex);
    }
}
