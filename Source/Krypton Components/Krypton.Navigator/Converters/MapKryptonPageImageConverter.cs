using Krypton.Toolkit;

namespace Krypton.Navigator
{
    /// <summary>
    /// Custom type converter so that MapKryptonPageImage values appear as neat text at design time.
    /// </summary>
    public class MapKryptonPageImageConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion
                                             
        #region Identity
        /// <summary>
        /// Initialize a new instance of the MapKryptonPageImageConverter clas.
        /// </summary>
        public MapKryptonPageImageConverter()
            : base(typeof(MapKryptonPageImage))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new Pair(MapKryptonPageImage.None,             "None (Null image)"),
            new Pair(MapKryptonPageImage.Small,            "Small"),
            new Pair(MapKryptonPageImage.SmallMedium,      "Small - Medium"), 
            new Pair(MapKryptonPageImage.SmallMediumLarge, "Small - Medium - Large"),
            new Pair(MapKryptonPageImage.Medium,           "Medium"), 
            new Pair(MapKryptonPageImage.MediumSmall,      "Medium - Small"), 
            new Pair(MapKryptonPageImage.MediumLarge,      "Medium - Large"),
            new Pair(MapKryptonPageImage.Large,            "Large"),
            new Pair(MapKryptonPageImage.LargeMedium,      "Large - Medium"),
            new Pair(MapKryptonPageImage.LargeMediumSmall, "Large - Medium - Small"),
            new Pair(MapKryptonPageImage.ToolTip,          "ToolTip") };

        #endregion
    }
}
