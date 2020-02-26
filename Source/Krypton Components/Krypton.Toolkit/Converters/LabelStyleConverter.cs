// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.500.0.0  
// *****************************************************************************

namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that LabelStyle values appear as neat text at design time.
    /// </summary>
    internal class LabelStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the LabelStyleConverter clas.
        /// </summary>
        public LabelStyleConverter()
            : base(typeof(LabelStyle))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new Pair(LabelStyle.NormalControl,     "Normal (Control)"),
            new Pair(LabelStyle.BoldControl,       "Bold (Control)"),
            new Pair(LabelStyle.ItalicControl,     "Italic (Control)"),
            new Pair(LabelStyle.TitleControl,      "Title (Control)"),
            new Pair(LabelStyle.NormalPanel,       "Normal (Panel)"),
            new Pair(LabelStyle.BoldPanel,         "Bold (Panel)"),
            new Pair(LabelStyle.ItalicPanel,       "Italic (Panel)"),
            new Pair(LabelStyle.TitlePanel,        "Title (Panel)"),
            new Pair(LabelStyle.GroupBoxCaption,   "Caption (Panel)"),
            new Pair(LabelStyle.ToolTip,           "ToolTip"), 
            new Pair(LabelStyle.SuperTip,          "SuperTip"), 
            new Pair(LabelStyle.KeyTip,            "KeyTip"), 
            new Pair(LabelStyle.Custom1,           "Custom1"), 
            new Pair(LabelStyle.Custom2,           "Custom2"), 
            new Pair(LabelStyle.Custom3,           "Custom3") };

        #endregion
    }
}
