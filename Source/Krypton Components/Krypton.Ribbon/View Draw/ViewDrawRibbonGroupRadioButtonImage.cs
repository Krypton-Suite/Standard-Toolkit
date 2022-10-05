namespace Krypton.Ribbon
{
    /// <summary>
    /// Draws a radio button centered in the correct location.
    /// </summary>
    internal class ViewDrawRibbonGroupRadioButtonImage : ViewComposite
    {
        #region Instance Fields
        private readonly Size _smallSize;// = new Size(16, 16);
        private readonly Size _largeSize;// = new Size(32, 32);
        private readonly KryptonRibbonGroupRadioButton _ribbonRadioButton;
        private readonly ViewDrawRadioButton _drawRadioButton;
        private readonly bool _large;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupRadioButtonImage class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonRadioButton">Reference to ribbon group radio button definition.</param>
        /// <param name="large">Show the large image.</param>
        public ViewDrawRibbonGroupRadioButtonImage(KryptonRibbon ribbon,
                                                   KryptonRibbonGroupRadioButton ribbonRadioButton,
                                                   bool large)
        {
            Debug.Assert(ribbonRadioButton != null);

            // Remember incoming parameters
            _ribbonRadioButton = ribbonRadioButton;
            _large = large;

            // Use redirector to get the radio button images and redirect to parent palette
            PaletteRedirectRadioButton redirectImages = new(ribbon.GetRedirector(), ribbon.StateCommon.RibbonImages.RadioButton);

            // Create drawing element
            _drawRadioButton = new ViewDrawRadioButton(redirectImages);

            // Add as only child
            Add(_drawRadioButton);
            _smallSize = new Size((int)(16 * FactorDpiX), (int)(16 * FactorDpiY));
            _largeSize = new Size((int)(32 * FactorDpiX), (int)(32 * FactorDpiY));
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString() =>
            // Return the class name and instance identifier
            @"ViewDrawRibbonGroupRadioButtonImage:" + Id;

        #endregion

        #region Enabled
        /// <summary>
        /// Gets and sets the enabled state of the element.
        /// </summary>
        public override bool Enabled
        {
            get => _drawRadioButton.Enabled;
            set => _drawRadioButton.Enabled = value;
        }
        #endregion

        #region Checked
        /// <summary>
        /// Gets and sets the checked state of the radio button.
        /// </summary>
        public bool Checked
        {
            get => _drawRadioButton.CheckState;
            set => _drawRadioButton.CheckState = value;
        }
        #endregion

        #region Tracking
        /// <summary>
        /// Gets and sets the tracking state of the radio button.
        /// </summary>
        public bool Tracking
        {
            get => _drawRadioButton.Tracking;
            set => _drawRadioButton.Tracking = value;
        }
        #endregion

        #region Pressed
        /// <summary>
        /// Gets and sets the pressed state of the radio button.
        /// </summary>
        public bool Pressed
        {
            get => _drawRadioButton.Pressed;
            set => _drawRadioButton.Pressed = value;
        }
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context) => _large ? _largeSize : _smallSize;

        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            // Take on all the provided area
            ClientRectangle = context.DisplayRectangle;

            // Get the size of the radio button when it is drawn
            Rectangle radioButtonRect = new(Point.Empty, _drawRadioButton.GetPreferredSize(context));

            // Decide on correct position within our rectangle
            if (_large)
            {
                // Place horizontal centered at the bottom of area
                radioButtonRect.X = ClientLocation.X + (ClientWidth - radioButtonRect.Width) / 2;
                radioButtonRect.Y = ClientRectangle.Bottom - radioButtonRect.Height;
            }
            else
            {
                // Place vertically centered at the right of area
                radioButtonRect.X = ClientRectangle.Right - radioButtonRect.Width;
                radioButtonRect.Y = ClientLocation.Y + (ClientHeight - radioButtonRect.Height) / 2;
            }

            // Layout the radio button draw element
            context.DisplayRectangle = radioButtonRect;
            _drawRadioButton.Layout(context);
            context.DisplayRectangle = ClientRectangle;
        }
        #endregion
    }
}

