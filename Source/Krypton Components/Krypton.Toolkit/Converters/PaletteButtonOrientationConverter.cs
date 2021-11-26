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

namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that PaletteButtonOrientation values appear as neat text at design time.
    /// </summary>
    internal class PaletteButtonOrientationConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteButtonOrientation class.
        /// </summary>
        public PaletteButtonOrientationConverter()
            : base(typeof(PaletteButtonOrientation))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new(PaletteButtonOrientation.Inherit,     "Inherit"),
            new(PaletteButtonOrientation.Auto,        "Auto"),
            new(PaletteButtonOrientation.FixedTop,    "Fixed Top"),
            new(PaletteButtonOrientation.FixedBottom, "Fixed Bottom"),
            new(PaletteButtonOrientation.FixedLeft,   "Fixed Left"),
            new(PaletteButtonOrientation.FixedRight,  "Fixed Right") };

        #endregion
    }
}
