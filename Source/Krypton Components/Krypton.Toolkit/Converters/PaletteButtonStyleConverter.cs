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
    /// Custom type converter so that PaletteButtonStyle values appear as neat text at design time.
    /// </summary>
    internal class PaletteButtonStyleConverter : StringLookupConverter<PaletteButtonStyle>
    {
        #region Static Fields

        [Localizable(true)]
        private static readonly IReadOnlyDictionary<PaletteButtonStyle, string> _pairs = new Dictionary<PaletteButtonStyle, string>
        {
            {PaletteButtonStyle.Inherit, KryptonLanguageManager.PaletteButtonStyles.Inherit},
            {PaletteButtonStyle.Standalone, KryptonLanguageManager.PaletteButtonStyles.Standalone},
            {PaletteButtonStyle.Alternate, KryptonLanguageManager.PaletteButtonStyles.Alternate},
            {PaletteButtonStyle.LowProfile, KryptonLanguageManager.PaletteButtonStyles.LowProfile},
            {PaletteButtonStyle.BreadCrumb, KryptonLanguageManager.PaletteButtonStyles.BreadCrumb},
            {PaletteButtonStyle.Cluster, KryptonLanguageManager.PaletteButtonStyles.Cluster},
            {PaletteButtonStyle.NavigatorStack, KryptonLanguageManager.PaletteButtonStyles.NavigatorStack},
            {PaletteButtonStyle.NavigatorOverflow, KryptonLanguageManager.PaletteButtonStyles.NavigatorOverflow},
            {PaletteButtonStyle.NavigatorMini, KryptonLanguageManager.PaletteButtonStyles.NavigatorMini},
            {PaletteButtonStyle.InputControl, KryptonLanguageManager.PaletteButtonStyles.InputControl},
            {PaletteButtonStyle.ListItem, KryptonLanguageManager.PaletteButtonStyles.ListItem},
            {PaletteButtonStyle.Form, KryptonLanguageManager.PaletteButtonStyles.Form},
            {PaletteButtonStyle.FormClose, KryptonLanguageManager.PaletteButtonStyles.FormClose},
            {PaletteButtonStyle.ButtonSpec, KryptonLanguageManager.PaletteButtonStyles.ButtonSpec},
            {PaletteButtonStyle.Command, KryptonLanguageManager.PaletteButtonStyles.Command},
            {PaletteButtonStyle.Custom1, KryptonLanguageManager.PaletteButtonStyles.Custom1},
            {PaletteButtonStyle.Custom2, KryptonLanguageManager.PaletteButtonStyles.Custom2},
            {PaletteButtonStyle.Custom3, KryptonLanguageManager.PaletteButtonStyles.Custom3 }
        };

        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override IReadOnlyDictionary<PaletteButtonStyle /*Enum*/, string /*Display*/> Pairs => _pairs;

        #endregion
    }
}
