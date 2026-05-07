#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Navigator;

/// <summary>
/// Custom type converter so that MapKryptonPageImage values appear as neat text at design time.
/// </summary>
public class MapKryptonPageImageConverter : StringLookupConverter<MapKryptonPageImage>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<MapKryptonPageImage, string> _pairs = new BiDictionary<MapKryptonPageImage, string>(
        new Dictionary<MapKryptonPageImage, string>
        {
            {MapKryptonPageImage.None, @"None (Null image)"},
            {MapKryptonPageImage.Small, @"Small"},
            {MapKryptonPageImage.SmallMedium, @"Small - Medium"},
            {MapKryptonPageImage.SmallMediumLarge, @"Small - Medium - Large"},
            {MapKryptonPageImage.Medium, @"Medium"},
            {MapKryptonPageImage.MediumSmall, @"Medium - Small"},
            {MapKryptonPageImage.MediumLarge, @"Medium - Large"},
            {MapKryptonPageImage.Large, @"Large"},
            {MapKryptonPageImage.LargeMedium, @"Large - Medium"},
            {MapKryptonPageImage.LargeMediumSmall, @"Large - Medium - Small"},
            {MapKryptonPageImage.ToolTip, nameof(ToolTip)}
        });
    #endregion

    #region Protected
    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<string /*Display*/, MapKryptonPageImage /*Enum*/ > PairsStringToEnum  => _pairs.SecondToFirst;
    protected override IReadOnlyDictionary<MapKryptonPageImage /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    #endregion
}