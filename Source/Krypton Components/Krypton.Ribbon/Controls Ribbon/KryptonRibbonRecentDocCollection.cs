#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Ribbon
{
    /// <summary>
    /// Delegate used for hooking into a KryptonRibbonContext typed collection.
    /// </summary>
    public delegate void RibbonRecentDocHandler(object sender, TypedCollectionEventArgs<KryptonRibbonRecentDoc> e);

    /// <summary>
    /// Specialise the generic collection with type specific rules for recent document item accessor.
    /// </summary>
    public class KryptonRibbonRecentDocCollection : TypedCollection<KryptonRibbonRecentDoc>
    {
        #region Public
        /// <summary>
        /// Gets the item with the provided document name.
        /// </summary>
        /// <param name="name">Name of the recent document instance.</param>
        /// <returns>Item at specified index.</returns>
        public override KryptonRibbonRecentDoc this[string name]
        {
            get
            {
                // Search for an entry with the same text name as that requested.
                foreach (KryptonRibbonRecentDoc recentDoc in this)
                {
                    if (recentDoc.Text == name)
                    {
                        return recentDoc;
                    }
                }

                // Let base class perform standard processing
                return base[name];
            }
        }
        #endregion
    }
}
