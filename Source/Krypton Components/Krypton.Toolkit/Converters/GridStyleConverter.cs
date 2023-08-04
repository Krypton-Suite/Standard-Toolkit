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
    /// Custom type converter so that GridStyle values appear as neat text at design time.
    /// </summary>
    internal class GridStyleConverter : StringLookupConverter<GridStyle>
    {
        #region Static Fields

        [Localizable(true)]
        private static readonly IReadOnlyDictionary<GridStyle, string> _pairs = new Dictionary<GridStyle, string>
        {
            {GridStyle.List, KryptonLanguageManager.GridStyles.List},
            {GridStyle.Sheet, KryptonLanguageManager.GridStyles.Sheet},
            {GridStyle.Custom1, KryptonLanguageManager.GridStyles.CustomOne},
            {GridStyle.Custom2, KryptonLanguageManager.GridStyles.CustomTwo},
            {GridStyle.Custom3, KryptonLanguageManager.GridStyles.CustomThree }
        };

        #endregion

        #region Protected

        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override IReadOnlyDictionary<GridStyle /*Enum*/, string /*Display*/> Pairs => _pairs;

        #endregion
    }
}
