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
    /// Custom type converter so that SeparatorStyle values appear as neat text at design time.
    /// </summary>
    internal class SeparatorStyleConverter : StringLookupConverter<SeparatorStyle>
    {
        #region Static Fields

        [Localizable(true)]
        private static readonly IReadOnlyDictionary<SeparatorStyle, string> _pairs = new Dictionary<SeparatorStyle, string>
        {
            {SeparatorStyle.LowProfile, KryptonLanguageManager.SeparatorStyles.LowProfile},
            {SeparatorStyle.HighProfile, KryptonLanguageManager.SeparatorStyles.HighProfile},
            {SeparatorStyle.HighInternalProfile, KryptonLanguageManager.SeparatorStyles.HighInternalProfile},
            {SeparatorStyle.Custom1, KryptonLanguageManager.SeparatorStyles.Custom1},
            {SeparatorStyle.Custom2, KryptonLanguageManager.SeparatorStyles.Custom2},
            {SeparatorStyle.Custom3, KryptonLanguageManager.SeparatorStyles.Custom3 }
        };

        #endregion

        #region Protected

        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override IReadOnlyDictionary<SeparatorStyle /*Enum*/, string /*Display*/> Pairs => _pairs;

        #endregion
    }
}
