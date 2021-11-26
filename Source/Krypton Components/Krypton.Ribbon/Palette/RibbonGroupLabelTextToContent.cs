﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Ribbon
{
    internal class RibbonGroupLabelTextToContent : RibbonToContent
    {
        #region Instance Fields
        private readonly IPaletteRibbonText _ribbonGroupTextNormal;
        private readonly IPaletteRibbonText _ribbonGroupTextDisabled;
        private readonly IPaletteRibbonText _ribbonLabelTextNormal;
        private readonly IPaletteRibbonText _ribbonLabelTextDisabled;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the RibbonGroupLabelTextToContent class.
        /// </summary>
        /// <param name="ribbonGeneral">Source for general ribbon settings.</param>
        /// <param name="ribbonGroupTextNormal">Source for ribbon group button normal settings.</param>
        /// <param name="ribbonGroupTextDisabled">Source for ribbon group button disabled settings.</param>
        /// <param name="ribbonLabelTextNormal">Source for ribbon group label normal settings.</param>
        /// <param name="ribbonLabelTextDisabled">Source for ribbon group label disabled settings.</param>
        public RibbonGroupLabelTextToContent(PaletteRibbonGeneral ribbonGeneral,
                                             IPaletteRibbonText ribbonGroupTextNormal,
                                             IPaletteRibbonText ribbonGroupTextDisabled,
                                             IPaletteRibbonText ribbonLabelTextNormal,
                                             IPaletteRibbonText ribbonLabelTextDisabled)

            : base(ribbonGeneral)
        {
            Debug.Assert(ribbonGroupTextNormal != null);
            Debug.Assert(ribbonGroupTextDisabled != null);
            Debug.Assert(ribbonLabelTextNormal != null);
            Debug.Assert(ribbonLabelTextDisabled != null);

            _ribbonGroupTextNormal = ribbonGroupTextNormal;
            _ribbonGroupTextDisabled = ribbonGroupTextDisabled;
            _ribbonLabelTextNormal = ribbonLabelTextNormal;
            _ribbonLabelTextDisabled = ribbonLabelTextDisabled;
        }
        #endregion

        #region IPaletteContent
        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor1(PaletteState state) => GetTextColor(state);

        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor2(PaletteState state) => GetTextColor(state);

        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor1(PaletteState state) => GetTextColor(state);

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor2(PaletteState state) => GetTextColor(state);

        #endregion

        #region Implementation
        private Color GetTextColor(PaletteState state)
        {
            Color retColor;

            if (state == PaletteState.Disabled)
            {
                retColor = _ribbonLabelTextDisabled.GetRibbonTextColor(state);

                if (retColor == Color.Empty)
                {
                    retColor = _ribbonGroupTextDisabled.GetRibbonTextColor(state);
                }
            }
            else
            {
                retColor = _ribbonLabelTextNormal.GetRibbonTextColor(state);

                if (retColor == Color.Empty)
                {
                    retColor = _ribbonGroupTextNormal.GetRibbonTextColor(state);
                }
            }

            return retColor;
        }
        #endregion
    }
}
