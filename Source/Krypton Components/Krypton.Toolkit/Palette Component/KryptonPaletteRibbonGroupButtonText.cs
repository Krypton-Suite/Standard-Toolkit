// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.550.0  
// *****************************************************************************

namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for palette ribbon group button text states.
    /// </summary>
    public class KryptonPaletteRibbonGroupButtonText : KryptonPaletteRibbonGroupBaseText
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteRibbonGroupButtonText class.
        /// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteRibbonGroupButtonText(PaletteRedirect redirect,
                                                   NeedPaintHandler needPaint)
            : base(redirect, PaletteRibbonTextStyle.RibbonGroupButtonText, needPaint)
        {
        }
        #endregion
    }
}
