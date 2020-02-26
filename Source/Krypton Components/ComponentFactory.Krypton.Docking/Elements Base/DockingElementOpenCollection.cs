// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2020. All rights reserved. (https://github.com/Wagnerp/Krypton-Toolkit-Suite-NET-Core)
//  Version 5.500.0.0  www.ComponentFactory.com
// *****************************************************************************

namespace ComponentFactory.Krypton.Docking
{
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
        public DockingElementOpenCollection(string name)
            : base(name)
        {
        }
        #endregion

        #region Public
        /// <summary>
        /// Append a docking element to the collection.
        /// </summary>
        /// <param name="item">IDockingElement reference.</param>
        public virtual void Add(IDockingElement item)
        {
            InternalAdd(item);
        }

        /// <summary>
        /// Append a docking element to the collection.
        /// </summary>
        /// <param name="index">Insert index.</param>
        /// <param name="item">IDockingElement reference.</param>
        public virtual void Insert(int index, IDockingElement item)
        {
            InternalInsert(index, item);
        }

        /// <summary>
        /// Removes first occurance of specified docking element.
        /// </summary>
        /// <param name="item">IDockingElement reference.</param>
        /// <returns>True if removed; otherwise false.</returns>
        public virtual bool Remove(IDockingElement item)
        {
            return InternalRemove(item);
        }

        /// <summary>
        /// Remove all docking elements from the collection.
        /// </summary>
        public virtual void Clear()
        {
            InternalClear();
        }
        #endregion
    }
}
