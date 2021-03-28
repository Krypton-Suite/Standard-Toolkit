using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Specialise the generic collection with type specific rules for group item accessor.
    /// </summary>
    public class KryptonRibbonGroupCollection : TypedCollection<KryptonRibbonGroup>
    {
        #region Public
        /// <summary>
        /// Gets the item with the provided unique name.
        /// </summary>
        /// <param name="name">Name of the ribbon group instance.</param>
        /// <returns>Item at specified index.</returns>
        public override KryptonRibbonGroup this[string name]
        {
            get
            {
                // Search for a group with the same text as that requested.
                foreach (KryptonRibbonGroup group in this)
                {
                    if ((@group.TextLine1 == name) ||
                        (@group.TextLine2 == name) ||
                        ((@group.TextLine1 + " " + @group.TextLine2) == name))
                    {
                        return @group;
                    }
                }

                // Let base class perform standard processing
                return base[name];
            }
        }
        #endregion
    }
}
