#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion

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
        /// Initialize a new instance of the LabelStyleConverter class.
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
        { new(LabelStyle.NormalControl,     "Normal (Control)"),
            new(LabelStyle.BoldControl,       "Bold (Control)"),
            new(LabelStyle.ItalicControl,     "Italic (Control)"),
            new(LabelStyle.TitleControl,      "Title (Control)"),
            new(LabelStyle.NormalPanel,       "Normal (Panel)"),
            new(LabelStyle.BoldPanel,         "Bold (Panel)"),
            new(LabelStyle.ItalicPanel,       "Italic (Panel)"),
            new(LabelStyle.TitlePanel,        "Title (Panel)"),
            new(LabelStyle.GroupBoxCaption,   "Caption (Panel)"),
            new(LabelStyle.ToolTip,           "ToolTip"), 
            new(LabelStyle.SuperTip,          "SuperTip"), 
            new(LabelStyle.KeyTip,            "KeyTip"), 
            new(LabelStyle.Custom1,           "Custom1"), 
            new(LabelStyle.Custom2,           "Custom2"), 
            new(LabelStyle.Custom3,           "Custom3") };

        #endregion
    }
}
