#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  Version 6.0.0  
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

        #endregion  

        #region Identity
        /// <summary>
        /// Initialize a new instance of the TabBorderStyleConverter clas.
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
        protected override Pair[] Pairs { get; } =
        { new Pair(TabBorderStyle.OneNote,               "OneNote"),
            new Pair(TabBorderStyle.SquareEqualSmall,      "Square Equal Small"),
            new Pair(TabBorderStyle.SquareEqualMedium,     "Square Equal Medium"),
            new Pair(TabBorderStyle.SquareEqualLarge,      "Square Equal Large"),
            new Pair(TabBorderStyle.SquareOutsizeSmall,    "Square Outsize Small"),
            new Pair(TabBorderStyle.SquareOutsizeMedium,   "Square Outsize Medium"),
            new Pair(TabBorderStyle.SquareOutsizeLarge,    "Square Outsize Large"),
            new Pair(TabBorderStyle.RoundedEqualSmall,     "Rounded Equal Small"),
            new Pair(TabBorderStyle.RoundedEqualMedium,    "Rounded Equal Medium"),
            new Pair(TabBorderStyle.RoundedEqualLarge,     "Rounded Equal Large"),
            new Pair(TabBorderStyle.RoundedOutsizeSmall,   "Rounded Outsize Small"),
            new Pair(TabBorderStyle.RoundedOutsizeMedium,  "Rounded Outsize Medium"),
            new Pair(TabBorderStyle.RoundedOutsizeLarge,   "Rounded Outsize Large"),
            new Pair(TabBorderStyle.SlantEqualNear,        "Slant Equal Near"),
            new Pair(TabBorderStyle.SlantEqualFar,         "Slant Equal Far"),
            new Pair(TabBorderStyle.SlantEqualBoth,        "Slant Equal Both"),
            new Pair(TabBorderStyle.SlantOutsizeNear,      "Slant Outsize Near"),
            new Pair(TabBorderStyle.SlantOutsizeFar,       "Slant Outsize Far"),
            new Pair(TabBorderStyle.SlantOutsizeBoth,      "Slant Outsize Both"),
            new Pair(TabBorderStyle.SmoothEqual,           "Smooth Equal"),
            new Pair(TabBorderStyle.SmoothOutsize,         "Smooth Outsize"),
            new Pair(TabBorderStyle.DockEqual,             "Dock Equal"),
            new Pair(TabBorderStyle.DockOutsize,           "Dock Outsize") };

        #endregion
    }
}
