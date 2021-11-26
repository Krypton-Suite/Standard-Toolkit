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
    /// Custom type converter so that PaletteButtonStyle values appear as neat text at design time.
    /// </summary>
    internal class PaletteButtonStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteButtonStyleConverter class.
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
        { new(PaletteButtonStyle.Inherit,           "Inherit"),
            new(PaletteButtonStyle.Standalone,        "Standalone"),
            new(PaletteButtonStyle.Alternate,         "Alternate"),
            new(PaletteButtonStyle.LowProfile,        "Low Profile"),
            new(PaletteButtonStyle.BreadCrumb,        "BreadCrumb"),
            new(PaletteButtonStyle.Cluster,           "Cluster"),  
            new(PaletteButtonStyle.NavigatorStack,    "Navigator Stack"),  
            new(PaletteButtonStyle.NavigatorOverflow, "Navigator Overflow"),  
            new(PaletteButtonStyle.NavigatorMini,     "Navigator Mini"),  
            new(PaletteButtonStyle.InputControl,      "Input Control"),  
            new(PaletteButtonStyle.ListItem,          "List Item"),  
            new(PaletteButtonStyle.Form,              "Form"),  
            new(PaletteButtonStyle.FormClose,         "Form Close"),  
            new(PaletteButtonStyle.ButtonSpec,        "ButtonSpec"),  
            new(PaletteButtonStyle.Command,           "Command"),  
            new(PaletteButtonStyle.Custom1,           "Custom1"),
            new(PaletteButtonStyle.Custom2,           "Custom2"),
            new(PaletteButtonStyle.Custom3,           "Custom3") };

        #endregion
    }
}
