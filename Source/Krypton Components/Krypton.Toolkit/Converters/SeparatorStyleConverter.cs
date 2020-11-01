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
    /// Custom type converter so that SeparatorStyle values appear as neat text at design time.
    /// </summary>
    internal class SeparatorStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the SeparatorStyleConverter clas.
        /// </summary>
        public SeparatorStyleConverter()
            : base(typeof(SeparatorStyle))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new Pair(SeparatorStyle.LowProfile,            "Low Profile"),
            new Pair(SeparatorStyle.HighProfile,           "High Profile"),  
            new Pair(SeparatorStyle.HighInternalProfile,   "High Internal Profile"),  
            new Pair(SeparatorStyle.Custom1,               "Custom1"),
            new Pair(SeparatorStyle.Custom2,               "Custom2"),
            new Pair(SeparatorStyle.Custom3,               "Custom3")

        };

        #endregion
    }
}
