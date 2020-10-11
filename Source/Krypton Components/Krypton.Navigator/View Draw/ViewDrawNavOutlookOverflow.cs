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
using Krypton.Toolkit;

namespace Krypton.Navigator
{
    /// <summary>
    /// Navigator view element for drawing an overflow item for the Outlook mode.
    /// </summary>
    internal class ViewDrawNavOutlookOverflow : ViewDrawNavCheckButtonBase
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawNavCheckButtonOutlook class.
        /// </summary>
        /// <param name="navigator">Owning navigator instance.</param>
        /// <param name="page">Page this check button represents.</param>
        /// <param name="orientation">Orientation for the check button.</param>
        public ViewDrawNavOutlookOverflow(KryptonNavigator navigator,
                                          KryptonPage page,
                                          VisualOrientation orientation)
            : base(navigator, page, orientation, true)
        {
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawNavOutlookOverflow:" + Id;
        }
        #endregion

        #region AllowButtonSpecs
        /// <summary>
        /// Gets a value indicating if button specs are allowed on the button.
        /// </summary>
        public override bool AllowButtonSpecs => false;

        #endregion

        #region IContentValues
        /// <summary>
        /// Gets the content image.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public override Image GetImage(PaletteState state)
        {
            return Page.GetImageMapping(Navigator.Outlook.Full.OverflowMapImage);
        }

        /// <summary>
        /// Gets the content short text.
        /// </summary>
        /// <returns>String value.</returns>
        public override string GetShortText()
        {
            return Page.GetTextMapping(Navigator.Outlook.Full.OverflowMapText);
        }

        /// <summary>
        /// Gets the content long text.
        /// </summary>
        /// <returns>String value.</returns>
        public override string GetLongText()
        {
            return Page.GetTextMapping(Navigator.Outlook.Full.OverflowMapExtraText);
        }
        #endregion
    }
}
