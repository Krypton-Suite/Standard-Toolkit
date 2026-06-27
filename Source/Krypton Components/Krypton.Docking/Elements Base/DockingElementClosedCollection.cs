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
/// Docking element that owns a fixed list of child elements exposed through indexing and enumeration.
/// </summary>
public abstract class DockingElementClosedCollection : DockingElement
{
    #region Instance Fields
    private readonly List<IDockingElement> _elements;
    #endregion

    #region Identity
    protected DockingElementClosedCollection(string? name)
        : base(name) =>
        _elements = new List<IDockingElement>();

    #endregion

    #region Public
    /// <summary>
    /// Number of direct child elements held in the internal list.
    /// </summary>
    public override int Count => _elements.Count;

    /// <summary>
    /// Child at <paramref name="index"/> in insertion order.
    /// </summary>
    /// <param name="index">Zero-based index into the child list.</param>
    /// <returns>The child at <paramref name="index"/>.</returns>
    public override IDockingElement? this[int index] => _elements[index];

    /// <summary>
    /// First child whose <see cref="DockingElement.Name"/> equals <paramref name="name"/>.
    /// </summary>
    /// <param name="name">Child name to match.</param>
    /// <returns>The matching child, or <see langword="null"/> when <paramref name="name"/> is <see langword="null"/> or not found.</returns>
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
    /// Enumerates direct children in list order.
    /// </summary>
    /// <returns>An enumerator over the child list.</returns>
    public override IEnumerator<IDockingElement> GetEnumerator() => _elements.GetEnumerator();

    /// <summary>
    /// Reports whether <paramref name="item"/> is a direct child of this collection.
    /// </summary>
    /// <param name="item">Candidate child element.</param>
    /// <returns><see langword="true"/> when <paramref name="item"/> is in the list; otherwise <see langword="false"/>.</returns>
    public virtual bool Contains(IDockingElement item) => _elements.Contains(item);

    #endregion

    #region Protected
    /// <summary>
    /// Append a docking element to the collection.
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
    /// Removes first occurrence of specified docking element.
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
    /// Remove all docking elements from the collection.
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
