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

namespace Krypton.Toolkit
{
    internal class KryptonProfessionalKCT : KryptonColorTable
    {
        #region Instance Fields
        private readonly Color[] _colors;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonProfessionalKCT class.
        /// </summary>
        /// <param name="colors">Set of colors to customize with.</param>
        /// <param name="useSystemColors">Should be forced to use system colors.</param>
        /// <param name="palette">Reference to associated palette.</param>
        public KryptonProfessionalKCT(Color[] colors, 
                                      bool useSystemColors,
                                      IPalette palette)
            : base(palette)
        {
            Debug.Assert(colors != null);
            _colors = colors;
            UseSystemColors = useSystemColors;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the starting color of the gradient used in the Header1.
        /// </summary>
        public Color Header1Begin => _colors[0];

        /// <summary>
        /// Gets the end color of the gradient used in the Header1.
        /// </summary>
        public Color Header1End => _colors[1];

        #endregion
    }
}
