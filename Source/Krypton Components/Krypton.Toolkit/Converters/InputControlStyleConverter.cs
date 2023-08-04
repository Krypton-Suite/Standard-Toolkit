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
    /// Custom type converter so that InputControl values appear as neat text at design time.
    /// </summary>
    internal class InputControlStyleConverter : StringLookupConverter<InputControlStyle>
    {
        #region Static Fields

        [Localizable(true)]
        private static readonly IReadOnlyDictionary<InputControlStyle, string> _pairs = new Dictionary<InputControlStyle, string>
        {
            {InputControlStyle.Standalone, KryptonLanguageManager.InputControlStyles.Standalone},
            {InputControlStyle.Ribbon, KryptonLanguageManager.InputControlStyles.Ribbon},
            {InputControlStyle.Custom1, KryptonLanguageManager.InputControlStyles.CustomOne},
            {InputControlStyle.Custom2, KryptonLanguageManager.InputControlStyles.CustomTwo},
            {InputControlStyle.Custom3, KryptonLanguageManager.InputControlStyles.CustomThree},
            {InputControlStyle.PanelClient, KryptonLanguageManager.InputControlStyles.PanelClient},
            {InputControlStyle.PanelAlternate, KryptonLanguageManager.InputControlStyles.PanelAlternate },
            // new(InputControlStyle.Disabled, "Disabled")
        };

        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override IReadOnlyDictionary<InputControlStyle /*Enum*/, string /*Display*/> Pairs => _pairs;

        #endregion
    }
}
