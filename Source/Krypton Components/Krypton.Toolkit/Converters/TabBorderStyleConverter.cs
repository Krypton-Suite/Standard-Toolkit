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
    internal class TabBorderStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #region Old

        //private readonly Pair[] _pairs =
        //{
        //    new(TabBorderStyle.OneNote, "OneNote"),
        //    new(TabBorderStyle.SquareEqualSmall, "Square Equal Small"),
        //    new(TabBorderStyle.SquareEqualMedium, "Square Equal Medium"),
        //    new(TabBorderStyle.SquareEqualLarge, "Square Equal Large"),
        //    new(TabBorderStyle.SquareOutsizeSmall, "Square Outsize Small"),
        //    new(TabBorderStyle.SquareOutsizeMedium, "Square Outsize Medium"),
        //    new(TabBorderStyle.SquareOutsizeLarge, "Square Outsize Large"),
        //    new(TabBorderStyle.RoundedEqualSmall, "Rounded Equal Small"),
        //    new(TabBorderStyle.RoundedEqualMedium, "Rounded Equal Medium"),
        //    new(TabBorderStyle.RoundedEqualLarge, "Rounded Equal Large"),
        //    new(TabBorderStyle.RoundedOutsizeSmall, "Rounded Outsize Small"),
        //    new(TabBorderStyle.RoundedOutsizeMedium, "Rounded Outsize Medium"),
        //    new(TabBorderStyle.RoundedOutsizeLarge, "Rounded Outsize Large"),
        //    new(TabBorderStyle.SlantEqualNear, "Slant Equal Near"),
        //    new(TabBorderStyle.SlantEqualFar, "Slant Equal Far"),
        //    new(TabBorderStyle.SlantEqualBoth, "Slant Equal Both"),
        //    new(TabBorderStyle.SlantOutsizeNear, "Slant Outsize Near"),
        //    new(TabBorderStyle.SlantOutsizeFar, "Slant Outsize Far"),
        //    new(TabBorderStyle.SlantOutsizeBoth, "Slant Outsize Both"),
        //    new(TabBorderStyle.SmoothEqual, "Smooth Equal"),
        //    new(TabBorderStyle.SmoothOutsize, "Smooth Outsize"),
        //    new(TabBorderStyle.DockEqual, "Dock Equal"),
        //    new(TabBorderStyle.DockOutsize, "Dock Outsize")
        //};

        #endregion

        [Localizable(true)]
        private readonly Pair[] _pairs =
        {
            new Pair(TabBorderStyle.OneNote, KryptonLanguageManager.TabBorderStyles.OneNote),
            new Pair(TabBorderStyle.SquareEqualSmall, KryptonLanguageManager.TabBorderStyles.SquareEqualSmall),
            new Pair(TabBorderStyle.SquareEqualMedium, KryptonLanguageManager.TabBorderStyles.SquareEqualMedium),
            new Pair(TabBorderStyle.SquareEqualLarge, KryptonLanguageManager.TabBorderStyles.SquareEqualLarge),
            new Pair(TabBorderStyle.SquareOutsizeSmall, KryptonLanguageManager.TabBorderStyles.SquareOutsizeSmall),
            new Pair(TabBorderStyle.SquareOutsizeMedium, KryptonLanguageManager.TabBorderStyles.SquareOutsizeMedium),
            new Pair(TabBorderStyle.SquareOutsizeLarge, KryptonLanguageManager.TabBorderStyles.SquareOutsizeLarge),
            new Pair(TabBorderStyle.RoundedEqualSmall, KryptonLanguageManager.TabBorderStyles.RoundedEqualSmall),
            new Pair(TabBorderStyle.RoundedEqualMedium, KryptonLanguageManager.TabBorderStyles.RoundedEqualMedium),
            new Pair(TabBorderStyle.RoundedEqualLarge, KryptonLanguageManager.TabBorderStyles.RoundedEqualLarge),
            new Pair(TabBorderStyle.RoundedOutsizeSmall, KryptonLanguageManager.TabBorderStyles.RoundedOutsizeSmall),
            new Pair(TabBorderStyle.RoundedOutsizeMedium, KryptonLanguageManager.TabBorderStyles.RoundedOutsizeMedium),
            new Pair(TabBorderStyle.RoundedOutsizeLarge, KryptonLanguageManager.TabBorderStyles.RoundedOutsizeLarge),
            new Pair(TabBorderStyle.SlantEqualNear, KryptonLanguageManager.TabBorderStyles.SlantEqualNear),
            new Pair(TabBorderStyle.SlantEqualFar, KryptonLanguageManager.TabBorderStyles.SlantEqualFar),
            new Pair(TabBorderStyle.SlantEqualBoth, KryptonLanguageManager.TabBorderStyles.SlantEqualBoth),
            new Pair(TabBorderStyle.SlantOutsizeNear, KryptonLanguageManager.TabBorderStyles.SlantOutsizeNear),
            new Pair(TabBorderStyle.SlantOutsizeFar, KryptonLanguageManager.TabBorderStyles.SlantOutsizeFar),
            new Pair(TabBorderStyle.SlantOutsizeBoth, KryptonLanguageManager.TabBorderStyles.SlantOutsizeBoth),
            new Pair(TabBorderStyle.SmoothEqual, KryptonLanguageManager.TabBorderStyles.SmoothEqual),
            new Pair(TabBorderStyle.SmoothOutsize, KryptonLanguageManager.TabBorderStyles.SmoothOutsize),
            new Pair(TabBorderStyle.DockEqual, KryptonLanguageManager.TabBorderStyles.DockEqual),
            new Pair(TabBorderStyle.DockOutsize, KryptonLanguageManager.TabBorderStyles.DockOutsize)
        };

        #endregion  

        #region Identity
        /// <summary>
        /// Initialize a new instance of the TabBorderStyleConverter class.
        /// </summary>
        public TabBorderStyleConverter()
            : base(typeof(TabBorderStyle))
        {
        }
        #endregion

        #region Protected

        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs => _pairs;

        #endregion
    }
}
