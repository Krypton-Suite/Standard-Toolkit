namespace Krypton.Toolkit
{
    /// <summary>
    /// Color event data.
    /// </summary>
    public class ColorEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ColorEventArgs class.
        /// </summary>
        /// <param name="color">Color associated with the event.</param>
        public ColorEventArgs(Color color) => Color = color;

        #endregion

        #region Public
        /// <summary>
        /// Gets the color.
        /// </summary>
        public Color Color { get; }

        #endregion
    }
}
