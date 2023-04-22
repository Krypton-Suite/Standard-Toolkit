﻿#region BSD License
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
    /// Custom type converter so that DataGridViewStyle values appear as neat text at design time.
    /// </summary>
    internal class DataGridViewStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #region Old

        /*private readonly Pair[] _pairs =
        {
            new(DataGridViewStyle.List, "List"),
            new(DataGridViewStyle.Sheet, "Sheet"),
            new(DataGridViewStyle.Custom1, "Custom1"),
            new(DataGridViewStyle.Custom2, "Custom2"),
            new(DataGridViewStyle.Custom3, "Custom3"),
            new(DataGridViewStyle.Mixed, "Mixed")
        };*/

        #endregion

        private readonly Pair[] _pairs =
        {
            new(DataGridViewStyle.List, KryptonLanguageManager.GridViewStyleStrings.List),
            new(DataGridViewStyle.Sheet, KryptonLanguageManager.GridViewStyleStrings.Sheet),
            new(DataGridViewStyle.Custom1, KryptonLanguageManager.GridViewStyleStrings.CustomOne),
            new(DataGridViewStyle.Custom2, KryptonLanguageManager.GridViewStyleStrings.CustomTwo),
            new(DataGridViewStyle.Custom3, KryptonLanguageManager.GridViewStyleStrings.CustomThree),
            new(DataGridViewStyle.Mixed, KryptonLanguageManager.GridViewStyleStrings.Mixed)
        };

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DataGridViewStyleConverter class.
        /// </summary>
        public DataGridViewStyleConverter()
            : base(typeof(DataGridViewStyle))
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
