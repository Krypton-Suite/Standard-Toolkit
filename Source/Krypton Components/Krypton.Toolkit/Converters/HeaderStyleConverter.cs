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
    /// Custom type converter so that HeaderStyle values appear as neat text at design time.
    /// </summary>
    internal class HeaderStyleConverter : StringLookupConverter<HeaderStyle>
    {
        #region Static Fields

        [Localizable(true)]
        private static readonly IReadOnlyDictionary<HeaderStyle, string> _pairs = new Dictionary<HeaderStyle, string>
        {
            {HeaderStyle.Primary, KryptonLanguageManager.HeaderStyles.Primary},
            {HeaderStyle.Secondary, KryptonLanguageManager.HeaderStyles.Secondary},
            {HeaderStyle.DockInactive, KryptonLanguageManager.HeaderStyles.DockInactive},
            {HeaderStyle.DockActive, KryptonLanguageManager.HeaderStyles.DockActive},
            {HeaderStyle.Form, KryptonLanguageManager.HeaderStyles.Form},
            {HeaderStyle.Calendar, KryptonLanguageManager.HeaderStyles.Calendar},
            {HeaderStyle.Custom1, KryptonLanguageManager.HeaderStyles.CustomOne},
            {HeaderStyle.Custom2, KryptonLanguageManager.HeaderStyles.CustomTwo},
            {HeaderStyle.Custom3, KryptonLanguageManager.HeaderStyles.CustomThree }
        };

        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override IReadOnlyDictionary<HeaderStyle /*Enum*/, string /*Display*/> Pairs => _pairs;

        #endregion
    }
}
