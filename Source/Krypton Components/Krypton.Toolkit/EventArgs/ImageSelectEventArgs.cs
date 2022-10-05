namespace Krypton.Toolkit
{
    /// <summary>
    /// Image select event data.
    /// </summary>
    public class ImageSelectEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ImageSelectEventArgs class.
        /// </summary>
        /// <param name="imageList">Defined image list.</param>
        /// <param name="imageIndex">Index within the image list.</param>
        public ImageSelectEventArgs(ImageList imageList, int imageIndex)
        {
            ImageList = imageList;
            ImageIndex = imageIndex;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the image list.
        /// </summary>
        public ImageList ImageList { get; }

        /// <summary>
        /// Gets the image index.
        /// </summary>
        public int ImageIndex { get; }

        #endregion
    }
}
