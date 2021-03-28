using System;
using Krypton.Toolkit;

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
