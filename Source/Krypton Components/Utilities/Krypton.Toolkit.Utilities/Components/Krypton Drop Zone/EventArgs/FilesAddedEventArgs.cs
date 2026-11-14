#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

public class FilesAddedEventArgs : EventArgs
{
    public IReadOnlyList<FileEntry> Accepted { get; }
    public IReadOnlyList<FileEntry> Rejected { get; }
    public int DuplicateCount { get; }
    internal FilesAddedEventArgs(List<FileEntry> accepted, List<FileEntry> rejected, int duplicates)
    {
        Accepted = accepted.AsReadOnly();
        Rejected = rejected.AsReadOnly();
        DuplicateCount = duplicates;
    }
}
