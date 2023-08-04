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
    /// Custom type converter so that TabStyle values appear as neat text at design time.
    /// </summary>
    internal class TabStyleConverter : StringLookupConverter<TabStyle>
    {
        #region Static Fields

        [Localizable(true)]
        private static readonly IReadOnlyDictionary<TabStyle, string> _pairs = new Dictionary<TabStyle, string>
        {
            {TabStyle.HighProfile, KryptonLanguageManager.TabStyles.HighProfile},
            {TabStyle.StandardProfile, KryptonLanguageManager.TabStyles.StandardProfile},
            {TabStyle.LowProfile, KryptonLanguageManager.TabStyles.LowProfile},
            {TabStyle.OneNote, KryptonLanguageManager.TabStyles.OneNote},
            {TabStyle.Dock, KryptonLanguageManager.TabStyles.Dock},
            {TabStyle.DockAutoHidden, KryptonLanguageManager.TabStyles.DockAutoHidden},
            {TabStyle.Custom1, KryptonLanguageManager.TabStyles.Custom1},
            {TabStyle.Custom2, KryptonLanguageManager.TabStyles.Custom2},
            {TabStyle.Custom3, KryptonLanguageManager.TabStyles.Custom3 }
        };

        #endregion

        #region Protected

        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override IReadOnlyDictionary<TabStyle /*Enum*/, string /*Display*/> Pairs => _pairs;

        #endregion
    }
}
