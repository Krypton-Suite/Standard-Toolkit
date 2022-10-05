namespace Krypton.Ribbon
{
    /// <summary>
    /// Draws either a large or small image from a group label.
    /// </summary>
    internal class ViewDrawRibbonGroupLabelImage : ViewDrawRibbonGroupImageBase

    {
        #region Instance Fields
        private readonly Size _smallSize; // = new Size(16, 16);
        private readonly Size _largeSize;// = new Size(32, 32);
        private readonly KryptonRibbonGroupLabel _ribbonLabel;
        private readonly bool _large;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupLabelImage class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonLabel">Reference to ribbon group label definition.</param>
        /// <param name="large">Show the large image.</param>
        public ViewDrawRibbonGroupLabelImage(KryptonRibbon ribbon,
                                             KryptonRibbonGroupLabel ribbonLabel,
                                             bool large)
            : base(ribbon)
        {
            Debug.Assert(ribbonLabel != null);

            _ribbonLabel = ribbonLabel;
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
            @"ViewDrawRibbonGroupLabelImage:" + Id;

        #endregion

        #region Protected
        /// <summary>
        /// Gets the size to draw the image.
        /// </summary>
        protected override Size DrawSize => _large ? _largeSize : _smallSize;

        /// <summary>
        /// Gets the image to be drawn.
        /// </summary>
        protected override Image DrawImage => _large ? _ribbonLabel.ImageLarge : _ribbonLabel.ImageSmall;

        #endregion
    }
}

