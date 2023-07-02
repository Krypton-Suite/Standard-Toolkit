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
    internal class HeaderStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #region Old

        //private readonly Pair[] _pairs =
        //{
        //    new(HeaderStyle.Primary, "Primary"),
        //    new(HeaderStyle.Secondary, "Secondary"),
        //    new(HeaderStyle.DockInactive, "Dock - Inactive"),
        //    new(HeaderStyle.DockActive, "Dock - Active"),
        //    new(HeaderStyle.Form, nameof(Form)),
        //    new(HeaderStyle.Calendar, nameof(Calendar)),
        //    new(HeaderStyle.Custom1, "Custom1"),
        //    new(HeaderStyle.Custom2, "Custom2"),
        //    new(HeaderStyle.Custom3, "Custom3")
        //};

        #endregion

        [Localizable(true)]
        private readonly Pair[] _pairs =
        {
            new Pair(HeaderStyle.Primary, KryptonLanguageManager.HeaderStyles.Primary),
            new Pair(HeaderStyle.Secondary, KryptonLanguageManager.HeaderStyles.Secondary),
            new Pair(HeaderStyle.DockInactive, KryptonLanguageManager.HeaderStyles.DockInactive),
            new Pair(HeaderStyle.DockActive, KryptonLanguageManager.HeaderStyles.DockActive),
            new Pair(HeaderStyle.Form, KryptonLanguageManager.HeaderStyles.Form),
            new Pair(HeaderStyle.Calendar, KryptonLanguageManager.HeaderStyles.Calendar),
            new Pair(HeaderStyle.Custom1, KryptonLanguageManager.HeaderStyles.CustomOne),
            new Pair(HeaderStyle.Custom2, KryptonLanguageManager.HeaderStyles.CustomTwo),
            new Pair(HeaderStyle.Custom3, KryptonLanguageManager.HeaderStyles.CustomThree)
        };

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the HeaderStyleConverter class.
        /// </summary>
        public HeaderStyleConverter()
            : base(typeof(HeaderStyle))
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
