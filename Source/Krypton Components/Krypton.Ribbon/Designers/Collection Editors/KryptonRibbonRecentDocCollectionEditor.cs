namespace Krypton.Ribbon
{
    internal class KryptonRibbonRecentDocCollectionEditor : CollectionEditor
    {
        /// <summary>
        /// Initialize a new instance of the KryptonRibbonRecentDocCollectionEditor class.
        /// </summary>
        public KryptonRibbonRecentDocCollectionEditor()
            : base(typeof(KryptonRibbonRecentDocCollection))
        {
        }

        /// <summary>
        /// Gets the data types that this collection editor can contain. 
        /// </summary>
        /// <returns>An array of data types that this collection can contain.</returns>
        protected override Type[] CreateNewItemTypes()
        {
            return new[] { typeof(KryptonRibbonRecentDoc) };
        }
    }
}
