#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>Snapshot of the existing list plus files accepted so far in this batch.</summary>
public sealed class ValidationContext
{
    private readonly List<FileEntry> _all = new List<FileEntry>();
    private readonly Dictionary<long, List<FileEntry>> _bySize = new Dictionary<long, List<FileEntry>>();

    public CancellationToken Token { get; }
    public long TotalSize { get; private set; }
    public int Count => _all.Count;
    public IReadOnlyList<FileEntry> Peers => _all;

    internal ValidationContext(IEnumerable<FileEntry> existing, CancellationToken token)
    {
        Token = token;
        foreach (FileEntry e in existing)
        {
            Accept(e);
        }
    }

    internal void Accept(FileEntry e)
    {
        _all.Add(e);
        TotalSize += e.Size;
        List<FileEntry>? list;
        if (!_bySize.TryGetValue(e.Size, out list))
        {
            _bySize[e.Size] = list = new List<FileEntry>();
        }
        list.Add(e);
    }

    public IReadOnlyList<FileEntry> PeersWithSize(long size)
    {
        List<FileEntry>? list;
        return _bySize.TryGetValue(size, out list) ? list : (new FileEntry[0]);
    }
}