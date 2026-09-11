#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Ordered tag strings owned by a <see cref="KryptonTagInputControl"/>.
/// Designer serialization uses <see cref="Collection{T}.Add"/>.
/// </summary>
public class KryptonTagCollection : Collection<string>
{
    #region Instance Fields

    private readonly KryptonTagInputControl _owner;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonTagCollection"/> class.
    /// </summary>
    /// <param name="owner">Owning tag input control.</param>
    public KryptonTagCollection(KryptonTagInputControl owner)
    {
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));
    }

    #endregion

    #region Internal

    /// <summary>
    /// When true, collection mutations do not call back into the owner (the owner is driving the change).
    /// </summary>
    internal bool SuspendOwnerNotify { get; set; }

    #endregion

    #region Collection

    /// <inheritdoc />
    protected override void InsertItem(int index, string item)
    {
        var trimmed = (item ?? string.Empty).Trim();
        if (trimmed.Length == 0)
        {
            return;
        }

        if (!SuspendOwnerNotify && !_owner.CanAcceptTag(trimmed))
        {
            return;
        }

        base.InsertItem(index, trimmed);

        if (!SuspendOwnerNotify)
        {
            _owner.NotifyTagInserted(trimmed);
        }
    }

    /// <inheritdoc />
    protected override void SetItem(int index, string item)
    {
        var trimmed = (item ?? string.Empty).Trim();
        if (trimmed.Length == 0)
        {
            return;
        }

        var previous = this[index];
        if (string.Equals(previous, trimmed, StringComparison.Ordinal))
        {
            return;
        }

        if (!SuspendOwnerNotify && !_owner.CanAcceptTag(trimmed))
        {
            return;
        }

        if (!SuspendOwnerNotify)
        {
            _owner.NotifyTagRemoved(previous);
        }

        base.SetItem(index, trimmed);

        if (!SuspendOwnerNotify)
        {
            _owner.NotifyTagInserted(trimmed);
        }
    }

    /// <inheritdoc />
    protected override void RemoveItem(int index)
    {
        var removed = this[index];
        base.RemoveItem(index);

        if (!SuspendOwnerNotify)
        {
            _owner.NotifyTagRemoved(removed);
        }
    }

    /// <inheritdoc />
    protected override void ClearItems()
    {
        if (Count == 0)
        {
            return;
        }

        var snapshot = new string[Count];
        CopyTo(snapshot, 0);
        base.ClearItems();

        if (!SuspendOwnerNotify)
        {
            _owner.NotifyTagsCleared(snapshot);
        }
    }

    #endregion
}
