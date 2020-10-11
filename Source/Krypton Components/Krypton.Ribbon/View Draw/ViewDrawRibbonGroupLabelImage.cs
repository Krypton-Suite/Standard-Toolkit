// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

using System.Drawing;
using System.Diagnostics;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Draws either a large or small image from a group label.
    /// </summary>
    internal class ViewDrawRibbonGroupLabelImage : ViewDrawRibbonGroupImageBase

    {
        #region Static Fields
        private static Size _smallSize; // = new Size(16, 16);
        private static Size _largeSize;// = new Size(32, 32);
        #endregion

        #region Instance Fields
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

            //Seb dpi aware
            _smallSize = new Size((int)(16 * FactorDpiX), (int)(16 * FactorDpiY));
            _largeSize = new Size((int)(32 * FactorDpiX), (int)(32 * FactorDpiY));

            _ribbonLabel = ribbonLabel;
            _large = large;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawRibbonGroupLabelImage:" + Id;
        }
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

