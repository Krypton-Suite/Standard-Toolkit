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
    /// Custom type converter so that PaletteButtonSpecStyle values appear as neat text at design time.
    /// </summary>
    internal class PaletteButtonSpecStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonStyleConverter clas.
        /// </summary>
        public PaletteButtonSpecStyleConverter()
            : base(typeof(PaletteButtonSpecStyle))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new Pair(PaletteButtonSpecStyle.Close,             "Close"),
            new Pair(PaletteButtonSpecStyle.Context,           "Context"),
            new Pair(PaletteButtonSpecStyle.Next,              "Next"),
            new Pair(PaletteButtonSpecStyle.Previous,          "Previous"),
            new Pair(PaletteButtonSpecStyle.Generic,           "Generic"),
            new Pair(PaletteButtonSpecStyle.ArrowLeft,         "Arrow Left"),
            new Pair(PaletteButtonSpecStyle.ArrowRight,        "Arrow Right"),
            new Pair(PaletteButtonSpecStyle.ArrowUp,           "Arrow Up"),
            new Pair(PaletteButtonSpecStyle.ArrowDown,         "Arrow Down"),
            new Pair(PaletteButtonSpecStyle.DropDown,          "Drop Down"),
            new Pair(PaletteButtonSpecStyle.PinVertical,       "Pin Vertical"),
            new Pair(PaletteButtonSpecStyle.PinHorizontal,     "Pin Horizontal"),
            new Pair(PaletteButtonSpecStyle.FormClose,         "Form Close"),
            new Pair(PaletteButtonSpecStyle.FormMax,           "Form Max"),
            new Pair(PaletteButtonSpecStyle.FormMin,           "Form Min"),
            new Pair(PaletteButtonSpecStyle.FormRestore,       "Form Restore"),
            new Pair(PaletteButtonSpecStyle.PendantClose,      "Pendant Close"),
            new Pair(PaletteButtonSpecStyle.PendantMin,        "Pendant Min"),
            new Pair(PaletteButtonSpecStyle.PendantRestore,    "Pendant Restore"),
            new Pair(PaletteButtonSpecStyle.WorkspaceMaximize, "Workspace Maximize"),
            new Pair(PaletteButtonSpecStyle.WorkspaceRestore,  "Workspace Restore"),
            new Pair(PaletteButtonSpecStyle.RibbonMinimize,    "Ribbon Minimize"),
            new Pair(PaletteButtonSpecStyle.RibbonExpand,      "Ribbon Expand")};

        #endregion
    }
}
