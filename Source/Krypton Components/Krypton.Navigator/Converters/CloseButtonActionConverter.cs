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

using Krypton.Toolkit;

namespace Krypton.Navigator
{
    /// <summary>
    /// Custom type converter so that CloseButtonAction values appear as neat text at design time.
    /// </summary>
    public class CloseButtonActionConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion
                                             
        #region Identity
        /// <summary>
        /// Initialize a new instance of the CloseButtonActionConverter clas.
        /// </summary>
        public CloseButtonActionConverter()
            : base(typeof(CloseButtonAction))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new Pair(CloseButtonAction.None,                   "None (Do nothing)"),
            new Pair(CloseButtonAction.RemovePage,             "RemovePage"),
            new Pair(CloseButtonAction.RemovePageAndDispose,   "RemovePage & Dispose"),
            new Pair(CloseButtonAction.HidePage,               "Hide Page") };

        #endregion
    }
}
