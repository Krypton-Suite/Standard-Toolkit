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
    /// Custom type converter so that PaletteButtonSpecStyle values appear as neat text at design time.
    /// </summary>
    internal class PaletteButtonSpecStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonStyleConverter class.
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
        { new(PaletteButtonSpecStyle.Close,             "Close"),
            new(PaletteButtonSpecStyle.Context,           "Context"),
            new(PaletteButtonSpecStyle.Next,              "Next"),
            new(PaletteButtonSpecStyle.Previous,          "Previous"),
            new(PaletteButtonSpecStyle.Generic,           "Generic"),
            new(PaletteButtonSpecStyle.ArrowLeft,         "Arrow Left"),
            new(PaletteButtonSpecStyle.ArrowRight,        "Arrow Right"),
            new(PaletteButtonSpecStyle.ArrowUp,           "Arrow Up"),
            new(PaletteButtonSpecStyle.ArrowDown,         "Arrow Down"),
            new(PaletteButtonSpecStyle.DropDown,          "Drop Down"),
            new(PaletteButtonSpecStyle.PinVertical,       "Pin Vertical"),
            new(PaletteButtonSpecStyle.PinHorizontal,     "Pin Horizontal"),
            new(PaletteButtonSpecStyle.FormClose,         "Form Close"),
            new(PaletteButtonSpecStyle.FormMax,           "Form Max"),
            new(PaletteButtonSpecStyle.FormMin,           "Form Min"),
            new(PaletteButtonSpecStyle.FormRestore,       "Form Restore"),
            new(PaletteButtonSpecStyle.FormHelp,              "Form Help"),
            new(PaletteButtonSpecStyle.PendantClose,      "Pendant Close"),
            new(PaletteButtonSpecStyle.PendantMin,        "Pendant Min"),
            new(PaletteButtonSpecStyle.PendantRestore,    "Pendant Restore"),
            new(PaletteButtonSpecStyle.WorkspaceMaximize, "Workspace Maximize"),
            new(PaletteButtonSpecStyle.WorkspaceRestore,  "Workspace Restore"),
            new(PaletteButtonSpecStyle.RibbonMinimize,    "Ribbon Minimize"),
            new(PaletteButtonSpecStyle.RibbonExpand,      "Ribbon Expand")};

        #endregion
    }
}
