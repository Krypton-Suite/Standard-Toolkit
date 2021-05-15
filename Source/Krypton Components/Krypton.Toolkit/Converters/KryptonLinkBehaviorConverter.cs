// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that KryptonLinkBehavior values appear as neat text at design time.
    /// </summary>
    internal class KryptonLinkBehaviorConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonLinkBehaviorConverter clas.
        /// </summary>
        public KryptonLinkBehaviorConverter()
            : base(typeof(KryptonLinkBehavior))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new(KryptonLinkBehavior.AlwaysUnderline,  "Always Underline"),
            new(KryptonLinkBehavior.HoverUnderline,   "Hover Underline"),
            new(KryptonLinkBehavior.NeverUnderline,   "Never Underline") };

        #endregion
    }
}
