#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Designer-serializable collection of <see cref="KryptonMultiColumnComboBoxColumn"/> items
/// owned by a <see cref="KryptonMultiColumnComboBox"/>.
/// </summary>
public class KryptonMultiColumnComboBoxColumnCollection : Collection<KryptonMultiColumnComboBoxColumn>
{
    #region Instance Fields

    private readonly KryptonMultiColumnComboBox _owner;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonMultiColumnComboBoxColumnCollection"/> class.
    /// </summary>
    /// <param name="owner">Owning multi-column combo box.</param>
    public KryptonMultiColumnComboBoxColumnCollection(KryptonMultiColumnComboBox owner) =>
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));

    #endregion

    #region Protected Override

    /// <inheritdoc />
    protected override void ClearItems()
    {
        foreach (KryptonMultiColumnComboBoxColumn column in this)
        {
            column.Owner = null;
        }

        base.ClearItems();
        _owner.NotifyColumnsCollectionChanged();
    }

    /// <inheritdoc />
    protected override void InsertItem(int index, KryptonMultiColumnComboBoxColumn item)
    {
        if (item == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(item));
        }

        item.Owner = _owner;
        base.InsertItem(index, item);
        _owner.NotifyColumnsCollectionChanged();
    }

    /// <inheritdoc />
    protected override void RemoveItem(int index)
    {
        this[index].Owner = null;
        base.RemoveItem(index);
        _owner.NotifyColumnsCollectionChanged();
    }

    /// <inheritdoc />
    protected override void SetItem(int index, KryptonMultiColumnComboBoxColumn item)
    {
        if (item == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(item));
        }

        this[index].Owner = null;
        item.Owner = _owner;
        base.SetItem(index, item);
        _owner.NotifyColumnsCollectionChanged();
    }

    #endregion
}
