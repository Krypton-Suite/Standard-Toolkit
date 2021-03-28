using System.Drawing;
using System.Diagnostics;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Draws a small image from a group cluster button.
    /// </summary>
    internal class ViewDrawRibbonGroupClusterButtonImage : ViewDrawRibbonGroupImageBase

    {
        #region Static Fields
        private static Size _smallSize;// = new Size(16, 16);
        #endregion

        #region Instance Fields
        private readonly KryptonRibbonGroupClusterButton _ribbonButton;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupClusterButtonImage class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonButton">Reference to ribbon group button definition.</param>
        public ViewDrawRibbonGroupClusterButtonImage(KryptonRibbon ribbon,
                                                     KryptonRibbonGroupClusterButton ribbonButton)
            : base(ribbon)
        {
            Debug.Assert(ribbonButton != null);

            //Seb dpi aware
            _smallSize = new Size((int)(16 * FactorDpiX), (int)(16 * FactorDpiY));

            _ribbonButton = ribbonButton;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawRibbonGroupClusterButtonImage:" + Id;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets the size to draw the image.
        /// </summary>
        protected override Size DrawSize
        {
            get { return _smallSize; }
        }

        /// <summary>
        /// Gets the image to be drawn.
        /// </summary>
        protected override Image DrawImage
        {
            get => _ribbonButton.KryptonCommand != null ? _ribbonButton.KryptonCommand.ImageSmall : _ribbonButton.ImageSmall;
        }
        #endregion
    }
}

