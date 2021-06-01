﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
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

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the TabStyleConverter clas.
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
        protected override Pair[] Pairs { get; } =
        { new Pair(TabStyle.HighProfile,     "High Profile"),
            new Pair(TabStyle.StandardProfile, "Standard Profile"),
            new Pair(TabStyle.LowProfile,      "Low Profile"),
            new Pair(TabStyle.OneNote,         "OneNote"),
            new Pair(TabStyle.Dock,            "Dock"),
            new Pair(TabStyle.DockAutoHidden,  "Dock AutoHidden"),
            new Pair(TabStyle.Custom1,         "Custom1"),
            new Pair(TabStyle.Custom2,         "Custom2"),
            new Pair(TabStyle.Custom3,         "Custom3") };

        #endregion
    }
}
