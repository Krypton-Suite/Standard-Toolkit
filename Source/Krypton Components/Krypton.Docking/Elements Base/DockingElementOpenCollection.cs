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
/// Docking element collection that supports adding, inserting, and removing child elements.
/// </summary>
public abstract class DockingElementOpenCollection : DockingElementClosedCollection
{
    #region Identity
    /// <summary>
    /// Creates a docking element whose child collection supports add and remove operations.
    /// </summary>
    /// <param name="name">Initial name of the element.</param>
    protected DockingElementOpenCollection(string name)
        : base(name)
    {
    }
    #endregion

    #region Public
    /// <summary>
    /// Adds a child docking element to the end of the collection and assigns its parent.
    /// </summary>
    /// <param name="item">IDockingElement reference.</param>
    public virtual void Add(IDockingElement item) => InternalAdd(item);

    /// <summary>
    /// Adds a child docking element to the end of the collection and assigns its parent.
    /// </summary>
    /// <param name="index">Insert index.</param>
    /// <param name="item">IDockingElement reference.</param>
    public virtual void Insert(int index, IDockingElement item) => InternalInsert(index, item);

    /// <summary>
    /// Removes the first matching child element and clears its parent reference.
    /// </summary>
    /// <param name="item">IDockingElement reference.</param>
    /// <returns><see langword="true"/> when the element was removed; otherwise <see langword="false"/>.</returns>
    public virtual bool Remove(IDockingElement item) => InternalRemove(item);

    /// <summary>
    /// Removes every child element and clears parent references.
    /// </summary>
    public virtual void Clear() => InternalClear();
    #endregion
}