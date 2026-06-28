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
/// Docking element that maintains an ordered collection of child elements with name-based lookup.
/// </summary>
public abstract class DockingElementClosedCollection : DockingElement
{
    #region Instance Fields
    private readonly List<IDockingElement> _elements;
    #endregion

    #region Identity
    /// <summary>
    /// Creates a docking element with an empty child collection.
    /// </summary>
    /// <param name="name">Initial name of the element.</param>
    protected DockingElementClosedCollection(string? name)
        : base(name) =>
        _elements = new List<IDockingElement>();

    #endregion

    #region Public
    /// <summary>
    /// Number of immediate child docking elements.
    /// </summary>
    public override int Count => _elements.Count;

    /// <summary>
    /// Child docking element at the specified zero-based index.
    /// </summary>
    /// <param name="index">Index.</param>
    /// <returns>Docking element at specified index.</returns>
    public override IDockingElement? this[int index] => _elements[index];

    /// <summary>
    /// First child docking element with the specified name.
    /// </summary>
    /// <param name="name">Name of element.</param>
    /// <returns>Docking element with specified name.</returns>
    public override IDockingElement? this[string? name]
    {
        get
        {
            // Cannot have a null name so no point searching for it
            return name != null 
                ? this.FirstOrDefault(element => element.Name.Equals(name)) 
                : null;
        }
    }

    /// <summary>
    /// Enumerates immediate child docking elements.
    /// </summary>
    /// <returns>Enumerator instance.</returns>
    public override IEnumerator<IDockingElement> GetEnumerator() => _elements.GetEnumerator();

    /// <summary>
    /// <see langword="true"/> when the collection contains the specified element; otherwise <see langword="false"/>.
    /// </summary>
    /// <param name="item">IDockingElement reference.</param>
    /// <returns><see langword="true"/> when the collection contains the specified element; otherwise <see langword="false"/>.</returns>
    public virtual bool Contains(IDockingElement item) => _elements.Contains(item);

    #endregion

    #region Protected
    /// <summary>
    /// Adds a child docking element to the end of the collection and assigns its parent.
    /// </summary>
    /// <param name="item">IDockingElement reference.</param>
    protected virtual void InternalAdd(IDockingElement item)
    {
        // Hook up the parent relationship, it is the responsibility of the 'item' 
        // to check that its name does not already exist in our collection.
        item.Parent = this;

        _elements.Add(item);
    }

    /// <summary>
    /// Insert a docking element to the collection.
    /// </summary>
    /// <param name="index">Insertion index.</param>
    /// <param name="item">IDockingElement reference.</param>
    protected virtual void InternalInsert(int index, IDockingElement item)
    {
        // Hook up the parent relationship, it is the responsibility of the 'item' 
        // to check that its name does not already exist in our collection.
        item.Parent = this;

        _elements.Insert(index, item);
    }

    /// <summary>
    /// Removes the first matching child element and clears its parent reference.
    /// </summary>
    /// <param name="item">IDockingElement reference.</param>
    /// <returns>True if removed; otherwise false.</returns>
    protected virtual bool InternalRemove(IDockingElement item)
    {
        // Try and remove before removing the parent relationship, so if the 
        // remove fails the parent relationship will still be correctly in place.
        if (_elements.Remove(item))
        {
            item.Parent = null;
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Removes every child element and clears parent references.
    /// </summary>
    protected virtual void InternalClear()
    {
        // Remove the parent relationships
        foreach (IDockingElement element in this)
        {
            element.Parent = null;
        }

        _elements.Clear();
    }
    #endregion
}