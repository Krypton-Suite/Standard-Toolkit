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

namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for button content value information.
    /// </summary>
    public class CheckButtonValues : ButtonValues
    {
        #region Instance Fields
        private CheckButtonImageStates _imageStates;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the CheckButtonValues class.
        /// </summary>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public CheckButtonValues(NeedPaintHandler needPaint)
            : base(needPaint)
        {
        }
        #endregion

        #region CreateImageStates
        /// <summary>
        /// Create the storage for the image states.
        /// </summary>
        /// <returns>Storage object.</returns>
        protected override ButtonImageStates CreateImageStates()
        {
            _imageStates = new CheckButtonImageStates();
            return _imageStates;
        }
        #endregion

        #region IContentValues
        /// <summary>
        /// Gets the content image.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public override Image GetImage(PaletteState state)
        {
            Image image = null;

            // Try and get a state specific image
            switch (state)
            {
                case PaletteState.CheckedNormal:
                    image = _imageStates.ImageCheckedNormal;
                    break;
                case PaletteState.CheckedPressed:
                    image = _imageStates.ImageCheckedPressed;
                    break;
                case PaletteState.CheckedTracking:
                    image = _imageStates.ImageCheckedTracking;
                    break;
            }

            return image ?? base.GetImage(state);
        }
        #endregion
    }
}
