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

using System.Drawing;
using System.Diagnostics;
using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    internal class RibbonGroupTextToContent : RibbonToContent
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the RibbonGroupTextToContent class.
        /// </summary>
        /// <param name="ribbonGeneral">Source for general ribbon settings.</param>
        /// <param name="ribbonGroupText">Source for ribbon group settings.</param>
        public RibbonGroupTextToContent(PaletteRibbonGeneral ribbonGeneral,
                                        IPaletteRibbonText ribbonGroupText)
            : base(ribbonGeneral)
        {
            Debug.Assert(ribbonGroupText != null);
            PaletteRibbonGroup = ribbonGroupText;
        }
        #endregion

        #region PaletteRibbonGroup
        /// <summary>
        /// Gets and sets the ribbon group palette to use.
        /// </summary>
        public IPaletteRibbonText PaletteRibbonGroup { get; set; }

        #endregion

        #region IPaletteContent
        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor1(PaletteState state)
        {
            return PaletteRibbonGroup.GetRibbonTextColor(state);
        }

        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor2(PaletteState state)
        {
            return PaletteRibbonGroup.GetRibbonTextColor(state);
        }

        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor1(PaletteState state)
        {
            return PaletteRibbonGroup.GetRibbonTextColor(state);
        }

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor2(PaletteState state)
        {
            return PaletteRibbonGroup.GetRibbonTextColor(state);
        }
        #endregion
    }
}
