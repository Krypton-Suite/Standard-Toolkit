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
