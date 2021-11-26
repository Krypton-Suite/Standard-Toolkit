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

namespace Krypton.Navigator
{
    /// <summary>
    /// Custom type converter so that PaletteNavButtonSpecStyle values appear as neat text at design time.
    /// </summary>
    internal class PaletteNavButtonSpecStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteNavButtonSpecStyleConverter class.
        /// </summary>
        public PaletteNavButtonSpecStyleConverter()
            : base(typeof(PaletteNavButtonSpecStyle))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new(PaletteNavButtonSpecStyle.Generic,            "Generic"),
            new(PaletteNavButtonSpecStyle.ArrowLeft,          "Arrow Left"),
            new(PaletteNavButtonSpecStyle.ArrowRight,         "Arrow Right"),
            new(PaletteNavButtonSpecStyle.ArrowUp,            "Arrow Up"),
            new(PaletteNavButtonSpecStyle.ArrowDown,          "Arrow Down"),
            new(PaletteNavButtonSpecStyle.DropDown,           "Drop Down"),
            new(PaletteNavButtonSpecStyle.PinVertical,        "Pin Vertical"),
            new(PaletteNavButtonSpecStyle.PinHorizontal,      "Pin Horizontal"),
            new(PaletteNavButtonSpecStyle.FormClose,          "Form Close"),
            new(PaletteNavButtonSpecStyle.FormMax,            "Form Max"),
            new(PaletteNavButtonSpecStyle.FormMin,            "Form Min"),
            new(PaletteNavButtonSpecStyle.FormRestore,        "Form Restore"),
            new(PaletteNavButtonSpecStyle.FormHelp,        "Form Help"),
            new(PaletteNavButtonSpecStyle.PendantClose,       "Pendant Close"),
            new(PaletteNavButtonSpecStyle.PendantMin,         "Pendant Min"),
            new(PaletteNavButtonSpecStyle.PendantRestore,     "Pendant Restore"),
            new(PaletteNavButtonSpecStyle.WorkspaceMaximize,  "Workspace Maximize"),
            new(PaletteNavButtonSpecStyle.WorkspaceRestore,   "Workspace Restore"),
            new(PaletteNavButtonSpecStyle.RibbonMinimize,     "Ribbon Minimize"),
            new(PaletteNavButtonSpecStyle.RibbonExpand,       "Ribbon Expand")};

        #endregion
    }
}
