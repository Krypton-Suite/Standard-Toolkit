﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Navigator
{
    /// <summary>
    /// Dictionary lookup from unique name to the KryptonPage.
    /// </summary>
    public class UniqueNameToPage : Dictionary<string, KryptonPage> { }

    /// <summary>
    /// Specialise the generic collection event args with specific type.
    /// </summary>
    public class KryptonPageEventArgs : TypedCollectionEventArgs<KryptonPage>
    {
        #region Public
        /// <summary>
        /// Initialize a new instance of the KryptonPageEventArgs class.
        /// </summary>
        /// <param name="item">Page effected by event.</param>
        /// <param name="index">Index of page in the owning collection.</param>
        public KryptonPageEventArgs(KryptonPage? item, int index)
            : base(item, index)
        {
        }
        #endregion
    }

    /// <summary>
    /// Specialise the generic collection with type specific rules for item accessor.
    /// </summary>
    public class KryptonPageCollection : TypedCollection<KryptonPage>
    {
        #region Public
        /// <summary>
        /// Gets the item with the provided unique name.
        /// </summary>
        /// <param name="name">Name of the ribbon tab instance.</param>
        /// <returns>Item at specified index.</returns>
        public override KryptonPage? this[string name]
        {
            get
            {
                // First priority is the UniqueName
                foreach (KryptonPage page in this.Where(page => page.UniqueName == name))
                {
                    return page;
                }

                // Second priority is the design time Name
                foreach (KryptonPage page in this.Where(page => page.Name == name))
                {
                    return page;
                }

                // Third priority is the Text of the page
                foreach (KryptonPage page in this.Where(page => page.Text == name))
                {
                    return page;
                }

                // Let base class perform standard processing
                return base[name];
            }
        }

        /// <summary>
        /// Gets the number of visible pages in the collection.
        /// </summary>
        public int VisibleCount
        {
            get
            {
                var visibleCount = 0;

                // Count the number of pages that are visible
                foreach (KryptonPage page in this)
                {
                    if (page.LastVisibleSet)
                    {
                        visibleCount++;
                    }
                }

                return visibleCount;
            }
        }
        #endregion
    }
}

