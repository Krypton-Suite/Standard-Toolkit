﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Draws a check box centered in the correct location.
    /// </summary>
    internal class ViewDrawRibbonGroupCheckBoxImage : ViewComposite
    {
        #region Static Fields
        private static Size _smallSize;// = new Size(16, 16);
        private static Size _largeSize;// = new Size(32, 32);
        #endregion

        #region Instance Fields
        private KryptonRibbonGroupCheckBox _ribbonCheckBox;
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

            //Seb dpi aware
            _smallSize = new Size((int)(16 * FactorDpiX), (int)(16 * FactorDpiY));
            _largeSize = new Size((int)(32 * FactorDpiX), (int)(32 * FactorDpiY));

            // Remember incoming parameters
            _ribbonCheckBox = ribbonCheckBox;
            _large = large;

            // Use redirector to get the check box images and redirect to parent palette
            PaletteRedirectCheckBox redirectImages = new PaletteRedirectCheckBox(ribbon.GetRedirector(), ribbon.StateCommon.RibbonImages.CheckBox);

            // Create drawing element
            _drawCheckBox = new ViewDrawCheckBox(redirectImages);

            // Add as only child
            Add(_drawCheckBox);
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawRibbonGroupCheckBoxImage:" + Id;
        }
        #endregion

        #region Enabled
        /// <summary>
        /// Gets and sets the enabled state of the element.
        /// </summary>
        public override bool Enabled
        {
            get { return _drawCheckBox.Enabled; }
            set { _drawCheckBox.Enabled = value; }
        }
        #endregion

        #region CheckState
        /// <summary>
        /// Gets and sets the check state of the check box.
        /// </summary>
        public CheckState CheckState
        {
            get { return _drawCheckBox.CheckState; }
            set { _drawCheckBox.CheckState = value; }
        }
        #endregion

        #region Tracking
        /// <summary>
        /// Gets and sets the tracking state of the check box.
        /// </summary>
        public bool Tracking
        {
            get { return _drawCheckBox.Tracking; }
            set { _drawCheckBox.Tracking = value; }
        }
        #endregion

        #region Pressed
        /// <summary>
        /// Gets and sets the pressed state of the check box.
        /// </summary>
        public bool Pressed
        {
            get { return _drawCheckBox.Pressed; }
            set { _drawCheckBox.Pressed = value; }
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
            Rectangle checkBoxRect = new Rectangle(Point.Empty, _drawCheckBox.GetPreferredSize(context));

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

