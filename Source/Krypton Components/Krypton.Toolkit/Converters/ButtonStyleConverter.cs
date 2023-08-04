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
    /// Custom type converter so that ButtonStyle values appear as neat text at design time.
    /// </summary>
    internal class ButtonStyleConverter : StringLookupConverter<ButtonStyle>
    {
        #region Static Fields

        [Localizable(true)]
        private static readonly IReadOnlyDictionary<ButtonStyle, string> _pairs = new Dictionary<ButtonStyle, string>
        {
            {ButtonStyle.Standalone, KryptonLanguageManager.ButtonStyles.Standalone},
            {ButtonStyle.Alternate, KryptonLanguageManager.ButtonStyles.Alternate},
            {ButtonStyle.LowProfile, KryptonLanguageManager.ButtonStyles.LowProfile},
            {ButtonStyle.ButtonSpec, KryptonLanguageManager.ButtonStyles.ButtonSpec},
            {ButtonStyle.BreadCrumb, KryptonLanguageManager.ButtonStyles.BreadCrumb},
            {ButtonStyle.CalendarDay, KryptonLanguageManager.ButtonStyles.CalendarDay},
            {ButtonStyle.Cluster, KryptonLanguageManager.ButtonStyles.Cluster},
            {ButtonStyle.Gallery, KryptonLanguageManager.ButtonStyles.Gallery},
            {ButtonStyle.NavigatorStack, KryptonLanguageManager.ButtonStyles.NavigatorStack},
            {ButtonStyle.NavigatorOverflow, KryptonLanguageManager.ButtonStyles.NavigatorOverflow},
            {ButtonStyle.NavigatorMini, KryptonLanguageManager.ButtonStyles.NavigatorMini},
            {ButtonStyle.InputControl, KryptonLanguageManager.ButtonStyles.InputControl},
            {ButtonStyle.ListItem, KryptonLanguageManager.ButtonStyles.ListItem},
            {ButtonStyle.Form, KryptonLanguageManager.ButtonStyles.Form},
            {ButtonStyle.FormClose, KryptonLanguageManager.ButtonStyles.FormClose},
            {ButtonStyle.Command, KryptonLanguageManager.ButtonStyles.Command},
            {ButtonStyle.Custom1, KryptonLanguageManager.ButtonStyles.CustomOne},
            {ButtonStyle.Custom2, KryptonLanguageManager.ButtonStyles.CustomTwo},
            {ButtonStyle.Custom3, KryptonLanguageManager.ButtonStyles.CustomThree }
        };

        #endregion

        #region Protected

        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override IReadOnlyDictionary<ButtonStyle /*Enum*/, string /*Display*/> Pairs => _pairs;

        #endregion
    }
}
