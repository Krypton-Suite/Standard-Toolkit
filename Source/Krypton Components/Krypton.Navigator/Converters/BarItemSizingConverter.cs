#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Navigator
{
    /// <summary>
    /// Custom type converter so that BarItemSizing values appear as neat text at design time.
    /// </summary>
    public class BarItemSizingConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion
                                             
        #region Identity
        /// <summary>
        /// Initialize a new instance of the BarItemSizingConverter clas.
        /// </summary>
        public BarItemSizingConverter()
            : base(typeof(BarItemSizing))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new(BarItemSizing.Individual,           "Individual Sizing"),
            new(BarItemSizing.SameHeight,           "All Same Height"),
            new(BarItemSizing.SameWidth,            "All Same Width"),
            new(BarItemSizing.SameWidthAndHeight,   "All Same Width & Height") };

        #endregion
    }
}
