using System;
using System.ComponentModel.Design;

namespace Krypton.Ribbon
{
    internal class KryptonRibbonGroupContainerCollectionEditor : CollectionEditor
    {
        /// <summary>
        /// Initialize a new instance of the KryptonRibbonGroupTopCollectionEditor class.
        /// </summary>
        public KryptonRibbonGroupContainerCollectionEditor()
            : base(typeof(KryptonRibbonGroupContainerCollection))
        {
        }

        /// <summary>
        /// Gets the data types that this collection editor can contain. 
        /// </summary>
        /// <returns>An array of data types that this collection can contain.</returns>
        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] { typeof(KryptonRibbonGroupLines),
                                typeof(KryptonRibbonGroupTriple),
                                typeof(KryptonRibbonGroupSeparator) };
        }
    }
}
