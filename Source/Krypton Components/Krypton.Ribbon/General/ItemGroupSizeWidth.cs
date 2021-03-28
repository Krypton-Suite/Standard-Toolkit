namespace Krypton.Ribbon
{
    internal class ItemSizeWidth
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ItemSizeWidth class.
        /// </summary>
        /// <param name="itemSize">Group item size.</param>
        /// <param name="width">Width associated with the item size.</param>
        public ItemSizeWidth(GroupItemSize itemSize, int width)
            : this(itemSize, width, -1)
        {
        }

        /// <summary>
        /// Initialize a new instance of the ItemSizeWidth class.
        /// </summary>
        /// <param name="itemSize">Group item size.</param>
        /// <param name="width">Width associated with the item size.</param>
        /// <param name="tag">Source specific tag information.</param>
        public ItemSizeWidth(GroupItemSize itemSize, 
                             int width,
                             int tag)
        {
            GroupItemSize = itemSize;
            Width = width;
            Tag = tag;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the item size.
        /// </summary>
        public GroupItemSize GroupItemSize { get; set; }

        /// <summary>
        /// Gets and sets the item width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets and sets the item tag.
        /// </summary>
        public int Tag { get; set; }

        #endregion
    }

    internal class GroupSizeWidth
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the GroupSizeWidth class.
        /// </summary>
        /// <param name="width">Width available for sizing a group.</param>
        /// <param name="sizing">Sizing information for applying to group.</param>
        public GroupSizeWidth(int width, ItemSizeWidth[] sizing)
        {
            Width = width;
            Sizing = sizing;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the item width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets and sets the array of sizing information for group.
        /// </summary>
        public ItemSizeWidth[] Sizing { get; set; }

        #endregion
    }
}
