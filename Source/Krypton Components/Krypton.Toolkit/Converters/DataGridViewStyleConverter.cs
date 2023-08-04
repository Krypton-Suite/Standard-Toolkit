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
    /// Custom type converter so that DataGridViewStyle values appear as neat text at design time.
    /// </summary>
    internal class DataGridViewStyleConverter : StringLookupConverter<DataGridViewStyle>
    {
        #region Static Fields

        [Localizable(true)]
        private static readonly IReadOnlyDictionary<DataGridViewStyle, string> _pairs = new Dictionary<DataGridViewStyle, string>
        {
            {DataGridViewStyle.List, KryptonLanguageManager.DataGridViewStyles.List},
            {DataGridViewStyle.Sheet, KryptonLanguageManager.DataGridViewStyles.Sheet},
            {DataGridViewStyle.Custom1, KryptonLanguageManager.DataGridViewStyles.CustomOne},
            {DataGridViewStyle.Custom2, KryptonLanguageManager.DataGridViewStyles.CustomTwo},
            {DataGridViewStyle.Custom3, KryptonLanguageManager.DataGridViewStyles.CustomThree},
            {DataGridViewStyle.Mixed, KryptonLanguageManager.DataGridViewStyles.Mixed }
        };

        #endregion

        #region Protected

        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override IReadOnlyDictionary<DataGridViewStyle /*Enum*/, string /*Display*/> Pairs => _pairs;

        #endregion
    }
}
