// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.550.0  
// *****************************************************************************

using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Delegate used for hooking into a KryptonRibbonContext typed collection.
    /// </summary>
    public delegate void RibbonContextHandler(object sender, TypedCollectionEventArgs<KryptonRibbonContext> e);

    /// <summary>
    /// Specialise the generic collection with type specific rules for context item accessor.
    /// </summary>
    public class KryptonRibbonContextCollection : TypedCollection<KryptonRibbonContext>
    {
        #region Public
        /// <summary>
        /// Gets the item with the provided unique name.
        /// </summary>
        /// <param name="name">Name of the ribbon context instance.</param>
        /// <returns>Item at specified index.</returns>
        public override KryptonRibbonContext this[string name]
        {
            get
            {
                // Search for a context with the same name as that requested.
                foreach (KryptonRibbonContext context in this)
                {
                    if (context.ContextName == name)
                    {
                        return context;
                    }
                }

                // Let base class perform standard processing
                return base[name];
            }
        }
        #endregion
    }
}
