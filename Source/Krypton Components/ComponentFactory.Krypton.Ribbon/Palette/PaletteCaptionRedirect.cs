// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2020. All rights reserved. (https://github.com/Wagnerp/Krypton-Toolkit-Suite-NET-Core)
//  Version 5.500.0.0  www.ComponentFactory.com
// *****************************************************************************

using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Override the redirection to force the borders for the caption to only show the bottom border as a maximum.
    /// </summary>
    internal class PaletteCaptionRedirect : PaletteRedirect
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteCaptionRedirect class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        public PaletteCaptionRedirect(IPalette target)
            : base(target)
        {
        }
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
            PaletteDrawBorders paletteBorder = base.GetBorderDrawBorders(style, state);

            // The ribbon caption area should only ever draw a bottom border as the maximum
            return (paletteBorder & PaletteDrawBorders.Bottom) == PaletteDrawBorders.Bottom ? PaletteDrawBorders.Bottom : PaletteDrawBorders.None;
        }
        #endregion
    }
}
