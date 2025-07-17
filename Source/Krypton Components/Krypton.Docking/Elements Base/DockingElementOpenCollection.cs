#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Docking;

/// <summary>
/// Extends base functionality by allowing a collection of child docking elements.
/// </summary>
public abstract class DockingElementOpenCollection : DockingElementClosedCollection
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the DockingElementOpenCollection class.
    /// </summary>
    /// <param name="name">Initial name of the element.</param>
    protected DockingElementOpenCollection(string name)
        : base(name)
    {
    }
    #endregion

    #region Public
    /// <summary>
    /// Append a docking element to the collection.
    /// </summary>
    /// <param name="item">IDockingElement reference.</param>
    public virtual void Add(IDockingElement item) => InternalAdd(item);

    /// <summary>
    /// Append a docking element to the collection.
    /// </summary>
    /// <param name="index">Insert index.</param>
    /// <param name="item">IDockingElement reference.</param>
    public virtual void Insert(int index, IDockingElement item) => InternalInsert(index, item);

    /// <summary>
    /// Removes first occurrence of specified docking element.
    /// </summary>
    /// <param name="item">IDockingElement reference.</param>
    /// <returns>True if removed; otherwise false.</returns>
    public virtual bool Remove(IDockingElement item) => InternalRemove(item);

    /// <summary>
    /// Remove all docking elements from the collection.
    /// </summary>
    public virtual void Clear() => InternalClear();
    #endregion
}