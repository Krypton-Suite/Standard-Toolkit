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
    /// Manage the items that can be added to a ribbon group button cluster container.
    /// </summary>
    public class KryptonRibbonGroupClusterCollection : TypedRestrictCollection<KryptonRibbonGroupItem>
    {
        #region Static Fields
        private static readonly Type[] _types = { typeof(KryptonRibbonGroupClusterButton),
                                                             typeof(KryptonRibbonGroupClusterColorButton)};
        #endregion

        #region Restrict
        /// <summary>
        /// Gets an array of types that the collection is restricted to contain.
        /// </summary>
        public override Type[] RestrictTypes => _types;

        #endregion
    }
}
