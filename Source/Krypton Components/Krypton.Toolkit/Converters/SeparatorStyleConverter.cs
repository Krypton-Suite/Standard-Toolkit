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
    internal class SeparatorStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #region Old

        //private readonly Pair[] _pairs =
        //{
        //    new(SeparatorStyle.LowProfile, "Low Profile"),
        //    new(SeparatorStyle.HighProfile, "High Profile"),
        //    new(SeparatorStyle.HighInternalProfile, "High Internal Profile"),
        //    new(SeparatorStyle.Custom1, "Custom1"),
        //    new(SeparatorStyle.Custom2, "Custom2"),
        //    new(SeparatorStyle.Custom3, "Custom3")
        //};

        #endregion

        [Localizable(true)]
        private readonly Pair[] _pairs =
        {
            new Pair(SeparatorStyle.LowProfile, KryptonLanguageManager.SeparatorStyles.LowProfile),
            new Pair(SeparatorStyle.HighProfile, KryptonLanguageManager.SeparatorStyles.HighProfile),
            new Pair(SeparatorStyle.HighInternalProfile, KryptonLanguageManager.SeparatorStyles.HighInternalProfile),
            new Pair(SeparatorStyle.Custom1, KryptonLanguageManager.SeparatorStyles.Custom1),
            new Pair(SeparatorStyle.Custom2, KryptonLanguageManager.SeparatorStyles.Custom2),
            new Pair(SeparatorStyle.Custom3, KryptonLanguageManager.SeparatorStyles.Custom3)
        };

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the SeparatorStyleConverter class.
        /// </summary>
        public SeparatorStyleConverter()
            : base(typeof(SeparatorStyle))
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
