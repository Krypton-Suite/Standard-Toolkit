using System;
using System.ComponentModel.Design;

namespace Krypton.Ribbon
{
    internal class KryptonRibbonGroupClusterCollectionEditor : CollectionEditor
    {
        /// <summary>
        /// Initialize a new instance of the KryptonRibbonGroupClusterCollectionEditor class.
        /// </summary>
        public KryptonRibbonGroupClusterCollectionEditor()
            : base(typeof(KryptonRibbonGroupClusterCollection))
        {
        }

        /// <summary>
        /// Gets the data types that this collection editor can contain. 
        /// </summary>
        /// <returns>An array of data types that this collection can contain.</returns>
        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] { typeof(KryptonRibbonGroupClusterButton),
                                typeof(KryptonRibbonGroupClusterColorButton)};
        }
    }
}
