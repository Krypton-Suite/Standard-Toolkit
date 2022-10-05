namespace Krypton.Ribbon
{
    /// <summary>
    /// Draws either a large or small image from a group button.
    /// </summary>
    internal class ViewDrawRibbonGroupButtonImage : ViewDrawRibbonGroupImageBase
    {
        #region Instance Fields
        private readonly Size _smallSize;
        private readonly Size _largeSize;
        private readonly KryptonRibbonGroupButton _ribbonButton;
        private readonly bool _large;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupButtonImage class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonButton">Reference to ribbon group button definition.</param>
        /// <param name="large">Show the large image.</param>
        public ViewDrawRibbonGroupButtonImage(KryptonRibbon ribbon,
                                              KryptonRibbonGroupButton ribbonButton,
                                              bool large)
            : base(ribbon)
        {
            Debug.Assert(ribbonButton != null);

            _ribbonButton = ribbonButton;
            _large = large;
            _smallSize = new Size((int)(16 * FactorDpiX), (int)(16 * FactorDpiY));
            _largeSize = new Size((int)(32 * FactorDpiX), (int)(32 * FactorDpiY));
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString() =>
            // Return the class name and instance identifier
            @"ViewDrawRibbonGroupButtonImage:" + Id;

        #endregion

        #region Protected
        /// <summary>
        /// Gets the size to draw the image.
        /// </summary>
        protected override Size DrawSize => _large ? _largeSize : _smallSize;

        /// <summary>
        /// Gets the image to be drawn.
        /// </summary>
        protected override Image DrawImage
        {
            get
            {
                if (_ribbonButton.KryptonCommand != null)
                {
                    return _large ? _ribbonButton.KryptonCommand.ImageLarge : _ribbonButton.KryptonCommand.ImageSmall;
                }
                else
                {
                    return _large ? _ribbonButton.ImageLarge : _ribbonButton.ImageSmall;
                }
            }
        }
        #endregion
    }
}
