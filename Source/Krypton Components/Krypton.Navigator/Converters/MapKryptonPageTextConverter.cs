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
    /// Custom type converter so that MapKryptonPageText values appear as neat text at design time.
    /// </summary>
    public class MapKryptonPageTextConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion
                                             
        #region Identity
        /// <summary>
        /// Initialize a new instance of the MapKryptonPageTextConverter clas.
        /// </summary>
        public MapKryptonPageTextConverter()
            : base(typeof(MapKryptonPageText))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new Pair(MapKryptonPageText.None,                          "None (Empty string)"),
            new Pair(MapKryptonPageText.Text,                          "Text"),
            new Pair(MapKryptonPageText.TextTitle,                     "Text - Title"), 
            new Pair(MapKryptonPageText.TextTitleDescription,          "Text - Title - Description"),
            new Pair(MapKryptonPageText.TextDescription,               "Text - Description"), 
            new Pair(MapKryptonPageText.Title,                         "Title"), 
            new Pair(MapKryptonPageText.TitleText,                     "Title - Text"),
            new Pair(MapKryptonPageText.TitleDescription,              "Title - Description"),
            new Pair(MapKryptonPageText.Description,                   "Description"),
            new Pair(MapKryptonPageText.DescriptionText,               "Description - Text"),
            new Pair(MapKryptonPageText.DescriptionTitle,              "Description - Title"),
            new Pair(MapKryptonPageText.DescriptionTitleText,          "Description - Title - Text"),
            new Pair(MapKryptonPageText.ToolTipTitle,                  "ToolTipTitle"),
            new Pair(MapKryptonPageText.ToolTipBody,                   "ToolTipBody"),
        };

        #endregion
    }
}
