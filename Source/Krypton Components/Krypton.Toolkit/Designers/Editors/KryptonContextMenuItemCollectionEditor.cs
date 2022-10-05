namespace Krypton.Toolkit
{
    /// <summary>
    /// CollectionEditor used for a KryptonContextMenuItemCollection instance.
    /// </summary>
    public class KryptonContextMenuItemCollectionEditor : CollectionEditor
    {
        /// <summary>
        /// Initialize a new instance of the KryptonContextMenuItemCollectionEditor class.
        /// </summary>
        public KryptonContextMenuItemCollectionEditor()
            : base(typeof(KryptonContextMenuItemCollection))
        {
        }

        /// <summary>
        /// Gets the data types that this collection editor can contain. 
        /// </summary>
        /// <returns>An array of data types that this collection can contain.</returns>
        protected override Type[] CreateNewItemTypes()
        {
            return new[] { typeof(KryptonContextMenuItem),
                                typeof(KryptonContextMenuSeparator),
                                typeof(KryptonContextMenuHeading) };
        }
    }
}
