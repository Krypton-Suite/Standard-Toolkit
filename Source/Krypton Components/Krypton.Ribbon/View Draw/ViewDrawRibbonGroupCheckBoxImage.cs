﻿namespace Krypton.Ribbon
{
    /// <summary>
    /// Draws a check box centered in the correct location.
    /// </summary>
    internal class ViewDrawRibbonGroupCheckBoxImage : ViewComposite
    {
        #region Instance Fields
        private readonly Size _smallSize;// = new Size(16, 16);
        private readonly Size _largeSize;// = new Size(32, 32);
        private readonly KryptonRibbonGroupCheckBox _ribbonCheckBox;
        private readonly ViewDrawCheckBox _drawCheckBox;
        private readonly bool _large;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupCheckBoxImage class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonCheckBox">Reference to ribbon group check box definition.</param>
        /// <param name="large">Show the large image.</param>
        public ViewDrawRibbonGroupCheckBoxImage(KryptonRibbon ribbon,
                                                KryptonRibbonGroupCheckBox ribbonCheckBox,
                                                bool large)
        {
            Debug.Assert(ribbonCheckBox != null);

            // Remember incoming parameters
            _ribbonCheckBox = ribbonCheckBox;
            _large = large;

            // Use redirector to get the check box images and redirect to parent palette
            PaletteRedirectCheckBox redirectImages = new(ribbon.GetRedirector(), ribbon.StateCommon.RibbonImages.CheckBox);

            // Create drawing element
            _drawCheckBox = new ViewDrawCheckBox(redirectImages);

            // Add as only child
            Add(_drawCheckBox);

            _smallSize = new Size((int)(16 * FactorDpiX), (int)(16 * FactorDpiY));
            _largeSize = new Size((int)(32 * FactorDpiX), (int)(32 * FactorDpiY));
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString() =>
            // Return the class name and instance identifier
            @"ViewDrawRibbonGroupCheckBoxImage:" + Id;

        #endregion

        #region Enabled
        /// <summary>
        /// Gets and sets the enabled state of the element.
        /// </summary>
        public override bool Enabled
        {
            get => _drawCheckBox.Enabled;
            set => _drawCheckBox.Enabled = value;
        }
        #endregion

        #region CheckState
        /// <summary>
        /// Gets and sets the check state of the check box.
        /// </summary>
        public CheckState CheckState
        {
            get => _drawCheckBox.CheckState;
            set => _drawCheckBox.CheckState = value;
        }
        #endregion

        #region Tracking
        /// <summary>
        /// Gets and sets the tracking state of the check box.
        /// </summary>
        public bool Tracking
        {
            get => _drawCheckBox.Tracking;
            set => _drawCheckBox.Tracking = value;
        }
        #endregion

        #region Pressed
        /// <summary>
        /// Gets and sets the pressed state of the check box.
        /// </summary>
        public bool Pressed
        {
            get => _drawCheckBox.Pressed;
            set => _drawCheckBox.Pressed = value;
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

            // Get the size of the check box when it is drawn
            Rectangle checkBoxRect = new(Point.Empty, _drawCheckBox.GetPreferredSize(context));

            // Decide on correct position within our rectangle
            if (_large)
            {
                // Place horizontal centered at the bottom of area
                checkBoxRect.X = ClientLocation.X + (ClientWidth - checkBoxRect.Width) / 2;
                checkBoxRect.Y = ClientRectangle.Bottom - checkBoxRect.Height;
            }
            else
            {
                // Place vertically centered at the right of area
                checkBoxRect.X = ClientRectangle.Right - checkBoxRect.Width;
                checkBoxRect.Y = ClientLocation.Y + (ClientHeight - checkBoxRect.Height) / 2;
            }

            // Layout the check box draw element
            context.DisplayRectangle = checkBoxRect;
            _drawCheckBox.Layout(context);
            context.DisplayRectangle = ClientRectangle;
        }
        #endregion
    }
}

