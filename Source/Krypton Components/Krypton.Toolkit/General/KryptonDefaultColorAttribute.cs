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

using System.ComponentModel;
using System.Drawing;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Create a default value attribute for color property.
    /// </summary>
    public sealed class KryptonDefaultColorAttribute : DefaultValueAttribute
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDefaultColorAttribute class.
        /// </summary>
        public KryptonDefaultColorAttribute()
            : base(Color.Empty)
        {
        }
        #endregion
    }
}
