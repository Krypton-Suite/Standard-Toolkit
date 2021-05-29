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
    /// Custom type converter so that PopupPagePosition values appear as neat text at design time.
    /// </summary>
    public class PopupPagePositionConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion
                                             
        #region Identity
        /// <summary>
        /// Initialize a new instance of the PopupPagePositionConverter clas.
        /// </summary>
        public PopupPagePositionConverter()
            : base(typeof(PopupPagePosition))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new Pair(PopupPagePosition.ModeAppropriate,    "Mode Appropriate"),
            new Pair(PopupPagePosition.AboveFar,           "Above Element - Far Aligned"),
            new Pair(PopupPagePosition.AboveMatch,         "Above Element - Element Width"),
            new Pair(PopupPagePosition.AboveNear,          "Above Element - Near Aligned"),
            new Pair(PopupPagePosition.BelowFar,           "Below Element - Far Aligned"),
            new Pair(PopupPagePosition.BelowMatch,         "Below Element - Element Width"),
            new Pair(PopupPagePosition.BelowNear,          "Below Element - Near Aligned"),
            new Pair(PopupPagePosition.FarBottom,          "Far Side of Element - Bottom Aligned"),
            new Pair(PopupPagePosition.FarMatch,           "Far Side of Element - Element Height"),
            new Pair(PopupPagePosition.FarTop,             "Far Side of Element - Top Aligned"),
            new Pair(PopupPagePosition.NearBottom,         "Near Side of Element - Bottom Aligned"),
            new Pair(PopupPagePosition.NearMatch,          "Near Side of Element - Element Height"),
            new Pair(PopupPagePosition.NearTop,            "Near Side of Element - Top Aligned") };

        #endregion
    }
}
