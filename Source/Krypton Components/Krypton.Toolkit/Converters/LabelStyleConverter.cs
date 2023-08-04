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
    /// Custom type converter so that LabelStyle values appear as neat text at design time.
    /// </summary>
    internal class LabelStyleConverter : StringLookupConverter<LabelStyle>
    {
        #region Static Fields

        [Localizable(true)]
        private static readonly IReadOnlyDictionary<LabelStyle, string> _pairs = new Dictionary<LabelStyle, string>
        {
            {LabelStyle.NormalControl, KryptonLanguageManager.KryptonLabelStyleStrings.NormalControl},
            {LabelStyle.BoldControl, KryptonLanguageManager.KryptonLabelStyleStrings.BoldControl},
            {LabelStyle.ItalicControl, KryptonLanguageManager.KryptonLabelStyleStrings.ItalicControl},
            {LabelStyle.TitleControl, KryptonLanguageManager.KryptonLabelStyleStrings.TitleControl},
            {LabelStyle.NormalPanel, KryptonLanguageManager.KryptonLabelStyleStrings.NormalPanel},
            {LabelStyle.BoldPanel, KryptonLanguageManager.KryptonLabelStyleStrings.BoldPanel},
            {LabelStyle.ItalicPanel, KryptonLanguageManager.KryptonLabelStyleStrings.ItalicPanel},
            {LabelStyle.TitlePanel, KryptonLanguageManager.KryptonLabelStyleStrings.TitlePanel},
            {LabelStyle.GroupBoxCaption, KryptonLanguageManager.KryptonLabelStyleStrings.GroupBoxCaption},
            {LabelStyle.ToolTip, KryptonLanguageManager.KryptonLabelStyleStrings.ToolTip},
            {LabelStyle.SuperTip, KryptonLanguageManager.KryptonLabelStyleStrings.SuperTip},
            {LabelStyle.KeyTip, KryptonLanguageManager.KryptonLabelStyleStrings.KeyTip},
            {LabelStyle.Custom1, KryptonLanguageManager.KryptonLabelStyleStrings.CustomOne},
            {LabelStyle.Custom2, KryptonLanguageManager.KryptonLabelStyleStrings.CustomTwo},
            {LabelStyle.Custom3, KryptonLanguageManager.KryptonLabelStyleStrings.CustomThree }
        };

        #endregion

        #region Protected

        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override IReadOnlyDictionary<LabelStyle /*Enum*/, string /*Display*/> Pairs => _pairs;

        #endregion
    }
}
