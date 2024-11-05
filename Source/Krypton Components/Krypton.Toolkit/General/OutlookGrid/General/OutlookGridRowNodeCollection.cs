#region Licences

/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */

//--------------------------------------------------------------------------------
// Copyright (C) 2013-2021 JDH Software - <support@jdhsoftware.com>
//
// This program is provided to you under the terms of the Microsoft Public
// License (Ms-PL) as published at https://github.com/Cocotteseb/Krypton-OutlookGrid/blob/master/LICENSE.md
//
// Visit https://www.jdhsoftware.com and follow @jdhsoftware on Twitter
//
//--------------------------------------------------------------------------------

#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// List of IOutlookGridGroups
    /// </summary>
    public class OutlookGridRowNodeCollection
    {
        #region "Variables"
        private OutlookGridRow? _parentNode;
        private List<OutlookGridRow> _subNodes;
        #endregion

        #region "Constructor"
        /// <summary>
        /// Initializes a new instance of the <see cref="OutlookGridRowNodeCollection"/> class.
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        public OutlookGridRowNodeCollection(OutlookGridRow? parentNode)
        {
            _parentNode = parentNode;
            _subNodes = new();
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets the parent node.
        /// </summary>
        /// <value>
        /// The parent node.
        /// </value>
        public OutlookGridRow? ParentNode { get => _parentNode; set => _parentNode = value; }

        /// <summary>
        /// Gets the nodes.
        /// </summary>
        /// <value>
        /// The nodes.
        /// </value>
        public List<OutlookGridRow> Nodes => _subNodes;


        /// <summary>
        /// Gets the number of groups
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count => _subNodes.Count;

        #endregion

        #region "Public methods"


        /// <summary>
        /// Gets the <see cref="OutlookGridRow"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="OutlookGridRow"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns>The IOutlookGridGroup.</returns>
        public OutlookGridRow this[int index] => _subNodes[index];

        /// <summary>
        /// Adds the specified row.
        /// </summary>
        /// <param name="row">The row.</param>
        public void Add(OutlookGridRow row)
        {
            row.ParentNode = _parentNode;
            if (ParentNode != null)
            {
                row.NodeLevel = ParentNode.NodeLevel + 1; //Not ++
            }
            _subNodes.Add(row);
        }

        /// <summary>
        /// Sorts this instance.
        /// </summary>
        public void Sort()
        {
            _subNodes.Sort();
        }

        /// <summary>
        /// Sorts the specified comparer.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        internal void Sort(OutlookGridRowComparer2 comparer)
        {
            _subNodes.Sort(comparer);
        }

        /// <summary>
        /// Gets the Index of a row
        /// </summary>
        /// <param name="row">The OutlookGrid row.</param>
        /// <returns></returns>
        public int IndexOf(OutlookGridRow row)
        {
            return _subNodes.IndexOf(row);
        }

        #endregion

        #region "Internals"

        /// <summary>
        /// Clears all subnodes.
        /// </summary>
        internal void Clear()
        {
            _parentNode = null;
            //If a group is collapsed the rows will not appear. Then if we clear the group the rows should not remain "collapsed"
            for (int i = 0; i < _subNodes.Count; i++)
            {
                _subNodes[i].Collapsed = false;
            }
            _subNodes.Clear();
        }

        #endregion
    }
}