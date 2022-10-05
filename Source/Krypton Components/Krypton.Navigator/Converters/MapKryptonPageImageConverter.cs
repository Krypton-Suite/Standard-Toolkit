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
        /// Initialize a new instance of the MapKryptonPageImageConverter class.
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
        { new(MapKryptonPageImage.None,             "None (Null image)"),
            new(MapKryptonPageImage.Small,            "Small"),
            new(MapKryptonPageImage.SmallMedium,      "Small - Medium"), 
            new(MapKryptonPageImage.SmallMediumLarge, "Small - Medium - Large"),
            new(MapKryptonPageImage.Medium,           "Medium"), 
            new(MapKryptonPageImage.MediumSmall,      "Medium - Small"), 
            new(MapKryptonPageImage.MediumLarge,      "Medium - Large"),
            new(MapKryptonPageImage.Large,            "Large"),
            new(MapKryptonPageImage.LargeMedium,      "Large - Medium"),
            new(MapKryptonPageImage.LargeMediumSmall, "Large - Medium - Small"),
            new(MapKryptonPageImage.ToolTip,          "ToolTip") };

        #endregion
    }
}
