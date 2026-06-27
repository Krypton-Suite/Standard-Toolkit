#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Docking;

/// <summary>
/// Child collection that exposes add, insert, remove, and clear operations on top of a closed child list.
/// </summary>
public abstract class DockingElementOpenCollection : DockingElementClosedCollection
{
    #region Identity
    protected DockingElementOpenCollection(string name)
        : base(name)
    {
    }
    #endregion

    #region Public
    /// <summary>
    /// Appends <paramref name="item"/> as the last child and sets its <see cref="DockingElement.Parent"/>.
    /// </summary>
    /// <param name="item">Child element to add.</param>
    public virtual void Add(IDockingElement item) => InternalAdd(item);

    /// <summary>
    /// Inserts <paramref name="item"/> at <paramref name="index"/> and sets its <see cref="DockingElement.Parent"/>.
    /// </summary>
    /// <param name="index">Zero-based insertion index.</param>
    /// <param name="item">Child element to insert.</param>
    public virtual void Insert(int index, IDockingElement item) => InternalInsert(index, item);

    /// <summary>
    /// Removes the first occurrence of <paramref name="item"/> and clears its parent link when removal succeeds.
    /// </summary>
    /// <param name="item">Child element to remove.</param>
    /// <returns><see langword="true"/> when <paramref name="item"/> was removed; otherwise <see langword="false"/>.</returns>
    public virtual bool Remove(IDockingElement item) => InternalRemove(item);

    /// <summary>
    /// Removes every child and clears each child's <see cref="DockingElement.Parent"/> reference.
    /// </summary>
    public virtual void Clear() => InternalClear();
    #endregion
}
