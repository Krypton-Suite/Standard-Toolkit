﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

using System;
using System.ComponentModel.Design;

namespace Krypton.Navigator
{
    internal class NavigatorPageCollectionEditor : CollectionEditor
    {
        /// <summary>
        /// Initialize a new instance of the NavigatorPageCollectionEditor class.
        /// </summary>
        public NavigatorPageCollectionEditor()
            : base(typeof(KryptonPageCollection))
        {
        }

        /// <summary>
        /// Gets the data types that this collection editor can contain. 
        /// </summary>
        /// <returns>An array of data types that this collection can contain.</returns>
        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] { typeof(KryptonPage) };
        }

        /// <summary>
        /// Sets the specified array as the items of the collection.
        /// </summary>
        /// <param name="editValue">The collection to edit.</param>
        /// <param name="value">An array of objects to set as the collection items.</param>
        /// <returns>The newly created collection object.</returns>
        protected override object SetItems(object editValue, object[] value)
        {
            // Cast the context into the expected control type
            KryptonNavigator navigator = (KryptonNavigator)Context.Instance;

            // Suspend changes until collection has been updated
            navigator?.SuspendLayout();

            // Let base class update the collection
            object ret = base.SetItems(editValue, value);

            navigator?.ResumeLayout(true);

            return ret;
        }
    }
}
