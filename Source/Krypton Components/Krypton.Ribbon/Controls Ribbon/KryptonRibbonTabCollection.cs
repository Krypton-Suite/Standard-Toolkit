using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Specialise the generic collection with type specific rules for tab item accessor.
    /// </summary>
    public class KryptonRibbonTabCollection : TypedCollection<KryptonRibbonTab>
    {
        #region Public
        /// <summary>
        /// Gets the item with the provided unique name.
        /// </summary>
        /// <param name="name">Name of the ribbon tab instance.</param>
        /// <returns>Item at specified index.</returns>
        public override KryptonRibbonTab this[string name]
        {
            get
            {
                // Search for a tab with the same text as that requested.
                foreach(KryptonRibbonTab tab in this)
                {
                    if (tab.Text == name)
                    {
                        return tab;
                    }
                }

                // Let base class perform standard processing
                return base[name];
            }
        }
        #endregion
    }
}
