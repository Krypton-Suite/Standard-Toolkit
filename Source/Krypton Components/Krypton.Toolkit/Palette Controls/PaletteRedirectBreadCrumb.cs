// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.500.0.0  
// *****************************************************************************

namespace Krypton.Toolkit
{
    /// <summary>
    /// Redirect back/border/content based on the incoming grid state and style.
    /// </summary>
    public class PaletteRedirectBreadCrumb : PaletteRedirect
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRedirectBreadCrumb class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        public PaletteRedirectBreadCrumb(IPalette target)
            : base(target)
        {
            Left = false;
            Right = false;
            TopBottom = true;
        }
        #endregion

        #region Left
        /// <summary>
        /// Gets and sets if the left border should be removed.
        /// </summary>
        public bool Left { get; set; }

        #endregion

        #region Right
        /// <summary>
        /// Gets and sets if the right border should be removed.
        /// </summary>
        public bool Right { get; set; }

        #endregion

        #region TopBottom
        /// <summary>
        /// Gets and sets if the top and bottom borders should be removed.
        /// </summary>
        public bool TopBottom { get; set; }

        #endregion

        #region GetBorderDrawBorders
        /// <summary>
        /// Gets a value indicating which borders to draw.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteDrawBorders value.</returns>
        public override PaletteDrawBorders GetBorderDrawBorders(PaletteBorderStyle style, PaletteState state)
        {
            PaletteDrawBorders borders = base.GetBorderDrawBorders(style, state);

            // We are only interested in bread crums buttons
            if (style == PaletteBorderStyle.ButtonBreadCrumb)
            {
                if (Left)
                {
                    borders &= ~PaletteDrawBorders.Left;
                }

                if (Right)
                {
                    borders &= ~PaletteDrawBorders.Right;
                }

                if (TopBottom)
                {
                    borders &= ~PaletteDrawBorders.TopBottom;
                }
            }

            return borders;
        }
        #endregion
    }
}
