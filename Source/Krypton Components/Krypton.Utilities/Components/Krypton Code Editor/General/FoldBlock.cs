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
/// Represents a foldable code block.
/// </summary>
public class FoldBlock
{
    public int StartLine { get; set; }
    public int EndLine { get; set; }
    public bool IsFolded { get; set; }
    public int IndentLevel { get; set; }
}
