#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that PaletteButtonOrientation values appear as neat text at design time.
    /// </summary>
    internal class PaletteButtonOrientationConverter : StringLookupConverter<PaletteButtonOrientation>
    {
        #region Static Fields

        [Localizable(true)]
        private static readonly IReadOnlyDictionary<PaletteButtonOrientation, string> _pairs = new Dictionary<PaletteButtonOrientation, string>
        {
            {PaletteButtonOrientation.Inherit, KryptonLanguageManager.ButtonOrientationStrings.Inherit},
            {PaletteButtonOrientation.Auto, KryptonLanguageManager.ButtonOrientationStrings.Auto},
            {PaletteButtonOrientation.FixedTop, KryptonLanguageManager.ButtonOrientationStrings.FixedTop},
            {PaletteButtonOrientation.FixedBottom, KryptonLanguageManager.ButtonOrientationStrings.FixedBottom},
            {PaletteButtonOrientation.FixedLeft, KryptonLanguageManager.ButtonOrientationStrings.FixedLeft},
            {PaletteButtonOrientation.FixedRight, KryptonLanguageManager.ButtonOrientationStrings.FixedRight }
        };

        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override IReadOnlyDictionary<PaletteButtonOrientation /*Enum*/, string /*Display*/> Pairs => _pairs;

        #endregion
    }
}
