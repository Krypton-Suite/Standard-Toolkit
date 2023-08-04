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
    /// Custom type converter so that TabBorderStyle values appear as neat text at design time.
    /// </summary>
    internal class TabBorderStyleConverter : StringLookupConverter<TabBorderStyle>
    {
        #region Static Fields

        [Localizable(true)]
        private static readonly IReadOnlyDictionary<TabBorderStyle, string> _pairs = new Dictionary<TabBorderStyle, string>
        {
            {TabBorderStyle.OneNote, KryptonLanguageManager.TabBorderStyles.OneNote},
            {TabBorderStyle.SquareEqualSmall, KryptonLanguageManager.TabBorderStyles.SquareEqualSmall},
            {TabBorderStyle.SquareEqualMedium, KryptonLanguageManager.TabBorderStyles.SquareEqualMedium},
            {TabBorderStyle.SquareEqualLarge, KryptonLanguageManager.TabBorderStyles.SquareEqualLarge},
            {TabBorderStyle.SquareOutsizeSmall, KryptonLanguageManager.TabBorderStyles.SquareOutsizeSmall},
            {TabBorderStyle.SquareOutsizeMedium, KryptonLanguageManager.TabBorderStyles.SquareOutsizeMedium},
            {TabBorderStyle.SquareOutsizeLarge, KryptonLanguageManager.TabBorderStyles.SquareOutsizeLarge},
            {TabBorderStyle.RoundedEqualSmall, KryptonLanguageManager.TabBorderStyles.RoundedEqualSmall},
            {TabBorderStyle.RoundedEqualMedium, KryptonLanguageManager.TabBorderStyles.RoundedEqualMedium},
            {TabBorderStyle.RoundedEqualLarge, KryptonLanguageManager.TabBorderStyles.RoundedEqualLarge},
            {TabBorderStyle.RoundedOutsizeSmall, KryptonLanguageManager.TabBorderStyles.RoundedOutsizeSmall},
            {TabBorderStyle.RoundedOutsizeMedium, KryptonLanguageManager.TabBorderStyles.RoundedOutsizeMedium},
            {TabBorderStyle.RoundedOutsizeLarge, KryptonLanguageManager.TabBorderStyles.RoundedOutsizeLarge},
            {TabBorderStyle.SlantEqualNear, KryptonLanguageManager.TabBorderStyles.SlantEqualNear},
            {TabBorderStyle.SlantEqualFar, KryptonLanguageManager.TabBorderStyles.SlantEqualFar},
            {TabBorderStyle.SlantEqualBoth, KryptonLanguageManager.TabBorderStyles.SlantEqualBoth},
            {TabBorderStyle.SlantOutsizeNear, KryptonLanguageManager.TabBorderStyles.SlantOutsizeNear},
            {TabBorderStyle.SlantOutsizeFar, KryptonLanguageManager.TabBorderStyles.SlantOutsizeFar},
            {TabBorderStyle.SlantOutsizeBoth, KryptonLanguageManager.TabBorderStyles.SlantOutsizeBoth},
            {TabBorderStyle.SmoothEqual, KryptonLanguageManager.TabBorderStyles.SmoothEqual},
            {TabBorderStyle.SmoothOutsize, KryptonLanguageManager.TabBorderStyles.SmoothOutsize},
            {TabBorderStyle.DockEqual, KryptonLanguageManager.TabBorderStyles.DockEqual},
            {TabBorderStyle.DockOutsize, KryptonLanguageManager.TabBorderStyles.DockOutsize }
        };

        #endregion  

        #region Protected

        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override IReadOnlyDictionary<TabBorderStyle /*Enum*/, string /*Display*/> Pairs => _pairs;

        #endregion
    }
}
