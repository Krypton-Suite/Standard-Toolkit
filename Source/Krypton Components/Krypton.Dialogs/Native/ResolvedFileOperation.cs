#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Dialogs;

/// <summary>
/// One action that survived preflight and will be queued on <see cref="IFileOperation"/>.
/// </summary>
internal sealed class ResolvedFileOperation
{
    /// <summary>Copy, move, rename, or delete.</summary>
    internal KryptonFileOperationKind Kind;

    /// <summary>Absolute source path.</summary>
    internal string SourcePath = string.Empty;

    /// <summary>Absolute destination directory for copy and move. Empty for rename and delete.</summary>
    internal string DestinationDirectory = string.Empty;

    /// <summary>New file name, or null to keep the source name.</summary>
    internal string? NewName;
}
