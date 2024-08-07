﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2024. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Docking
{
    /// <summary>
    /// Extends base functionality by allowing a collection of child docking elements.
    /// </summary>
    public abstract class DockingElementClosedCollection : DockingElement
    {
        #region Instance Fields
        private readonly List<IDockingElement> _elements;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DockingElementClosedCollection class.
        /// </summary>
        /// <param name="name">Initial name of the element.</param>
        protected DockingElementClosedCollection(string? name)
            : base(name) =>
            _elements = new List<IDockingElement>();

        #endregion

        #region Public
        /// <summary>
        /// Gets the number of child docking elements.
        /// </summary>
        public override int Count => _elements.Count;

        /// <summary>
        /// Gets the docking element at the specified index.
        /// </summary>
        /// <param name="index">Index.</param>
        /// <returns>Docking element at specified index.</returns>
        public override IDockingElement? this[int index] => _elements[index];

        /// <summary>
        /// Gets the docking element with the specified name.
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
        /// Shallow enumerate over child docking elements.
        /// </summary>
        /// <returns>Enumerator instance.</returns>
        public override IEnumerator<IDockingElement> GetEnumerator() => _elements.GetEnumerator();

        /// <summary>
        /// Determines whether the collection contains the docking element.
        /// </summary>
        /// <param name="item">IDockingElement reference.</param>
        /// <returns>True if view found; otherwise false.</returns>
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
}
