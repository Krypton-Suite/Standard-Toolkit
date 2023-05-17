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
    internal class TabStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #region Old

        //private readonly Pair[] _pairs =
        //{
        //    new(TabStyle.HighProfile, "High Profile"),
        //    new(TabStyle.StandardProfile, "Standard Profile"),
        //    new(TabStyle.LowProfile, "Low Profile"),
        //    new(TabStyle.OneNote, "OneNote"),
        //    new(TabStyle.Dock, "Dock"),
        //    new(TabStyle.DockAutoHidden, "Dock AutoHidden"),
        //    new(TabStyle.Custom1, "Custom1"),
        //    new(TabStyle.Custom2, "Custom2"),
        //    new(TabStyle.Custom3, "Custom3")
        //};

        #endregion

        [Localizable(true)]
        private readonly Pair[] _pairs =
        {
            new(TabStyle.HighProfile, KryptonLanguageManager.TabStyles.HighProfile),
            new(TabStyle.StandardProfile, KryptonLanguageManager.TabStyles.StandardProfile),
            new(TabStyle.LowProfile, KryptonLanguageManager.TabStyles.LowProfile),
            new(TabStyle.OneNote, KryptonLanguageManager.TabStyles.OneNote),
            new(TabStyle.Dock, KryptonLanguageManager.TabStyles.Dock),
            new(TabStyle.DockAutoHidden, KryptonLanguageManager.TabStyles.DockAutoHidden),
            new(TabStyle.Custom1, KryptonLanguageManager.TabStyles.Custom1),
            new(TabStyle.Custom2, KryptonLanguageManager.TabStyles.Custom2),
            new(TabStyle.Custom3, KryptonLanguageManager.TabStyles.Custom3)
        };

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the TabStyleConverter class.
        /// </summary>
        public TabStyleConverter()
            : base(typeof(TabStyle))
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
