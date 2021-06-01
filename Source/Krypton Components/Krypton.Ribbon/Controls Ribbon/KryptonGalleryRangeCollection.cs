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

using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Specialise the generic collection with type specific rules for gallery range item accessor.
    /// </summary>
    public class KryptonGalleryRangeCollection : TypedCollection<KryptonGalleryRange>
    {
        #region Public
        /// <summary>
        /// Gets the item with the provided unique name.
        /// </summary>
        /// <param name="heading">Heading of the gallery range instance.</param>
        /// <returns>Item at specified index.</returns>
        public override KryptonGalleryRange this[string heading]
        {
            get
            {
                // Search for a gallery range with the same heading as that requested.
                foreach (KryptonGalleryRange range in this)
                {
                    if (range.Heading == heading)
                    {
                        return range;
                    }
                }

                // Let base class perform standard processing
                return base[heading];
            }
        }
        #endregion
    }
}
