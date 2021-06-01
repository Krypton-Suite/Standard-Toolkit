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
    /// Custom type converter so that PaletteButtonStyle values appear as neat text at design time.
    /// </summary>
    internal class PaletteButtonStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteButtonStyleConverter clas.
        /// </summary>
        public PaletteButtonStyleConverter()
            : base(typeof(PaletteButtonStyle))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new Pair(PaletteButtonStyle.Inherit,           "Inherit"),
            new Pair(PaletteButtonStyle.Standalone,        "Standalone"),
            new Pair(PaletteButtonStyle.Alternate,         "Alternate"),
            new Pair(PaletteButtonStyle.LowProfile,        "Low Profile"),
            new Pair(PaletteButtonStyle.BreadCrumb,        "BreadCrumb"),
            new Pair(PaletteButtonStyle.Cluster,           "Cluster"),  
            new Pair(PaletteButtonStyle.NavigatorStack,    "Navigator Stack"),  
            new Pair(PaletteButtonStyle.NavigatorOverflow, "Navigator Overflow"),  
            new Pair(PaletteButtonStyle.NavigatorMini,     "Navigator Mini"),  
            new Pair(PaletteButtonStyle.InputControl,      "Input Control"),  
            new Pair(PaletteButtonStyle.ListItem,          "List Item"),  
            new Pair(PaletteButtonStyle.Form,              "Form"),  
            new Pair(PaletteButtonStyle.FormClose,         "Form Close"),  
            new Pair(PaletteButtonStyle.ButtonSpec,        "ButtonSpec"),  
            new Pair(PaletteButtonStyle.Command,           "Command"),  
            new Pair(PaletteButtonStyle.Custom1,           "Custom1"),
            new Pair(PaletteButtonStyle.Custom2,           "Custom2"),
            new Pair(PaletteButtonStyle.Custom3,           "Custom3") };

        #endregion
    }
}
