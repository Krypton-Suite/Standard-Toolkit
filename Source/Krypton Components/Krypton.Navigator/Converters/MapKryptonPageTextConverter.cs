using Krypton.Toolkit;

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
