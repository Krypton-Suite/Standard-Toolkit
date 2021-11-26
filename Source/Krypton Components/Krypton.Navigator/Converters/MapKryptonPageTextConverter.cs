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
    /// Custom type converter so that MapKryptonPageText values appear as neat text at design time.
    /// </summary>
    public class MapKryptonPageTextConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion
                                             
        #region Identity
        /// <summary>
        /// Initialize a new instance of the MapKryptonPageTextConverter class.
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
        { new(MapKryptonPageText.None,                          "None (Empty string)"),
            new(MapKryptonPageText.Text,                          "Text"),
            new(MapKryptonPageText.TextTitle,                     "Text - Title"), 
            new(MapKryptonPageText.TextTitleDescription,          "Text - Title - Description"),
            new(MapKryptonPageText.TextDescription,               "Text - Description"), 
            new(MapKryptonPageText.Title,                         "Title"), 
            new(MapKryptonPageText.TitleText,                     "Title - Text"),
            new(MapKryptonPageText.TitleDescription,              "Title - Description"),
            new(MapKryptonPageText.Description,                   "Description"),
            new(MapKryptonPageText.DescriptionText,               "Description - Text"),
            new(MapKryptonPageText.DescriptionTitle,              "Description - Title"),
            new(MapKryptonPageText.DescriptionTitleText,          "Description - Title - Text"),
            new(MapKryptonPageText.ToolTipTitle,                  "ToolTipTitle"),
            new(MapKryptonPageText.ToolTipBody,                   "ToolTipBody")
        };

        #endregion
    }
}
