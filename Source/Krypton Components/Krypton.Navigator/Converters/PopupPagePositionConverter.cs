#region BSD License
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
        /// Initialize a new instance of the PopupPagePositionConverter class.
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
        { new(PopupPagePosition.ModeAppropriate,    "Mode Appropriate"),
            new(PopupPagePosition.AboveFar,           "Above Element - Far Aligned"),
            new(PopupPagePosition.AboveMatch,         "Above Element - Element Width"),
            new(PopupPagePosition.AboveNear,          "Above Element - Near Aligned"),
            new(PopupPagePosition.BelowFar,           "Below Element - Far Aligned"),
            new(PopupPagePosition.BelowMatch,         "Below Element - Element Width"),
            new(PopupPagePosition.BelowNear,          "Below Element - Near Aligned"),
            new(PopupPagePosition.FarBottom,          "Far Side of Element - Bottom Aligned"),
            new(PopupPagePosition.FarMatch,           "Far Side of Element - Element Height"),
            new(PopupPagePosition.FarTop,             "Far Side of Element - Top Aligned"),
            new(PopupPagePosition.NearBottom,         "Near Side of Element - Bottom Aligned"),
            new(PopupPagePosition.NearMatch,          "Near Side of Element - Element Height"),
            new(PopupPagePosition.NearTop,            "Near Side of Element - Top Aligned") };

        #endregion
    }
}
