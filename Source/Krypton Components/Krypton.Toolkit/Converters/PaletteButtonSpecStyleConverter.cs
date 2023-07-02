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
    /// Custom type converter so that PaletteButtonSpecStyle values appear as neat text at design time.
    /// </summary>
    internal class PaletteButtonSpecStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #region Old

        //private readonly Pair[] _pairs =
        //{
        //    new(PaletteButtonSpecStyle.Close, @"Close"),
        //    new(PaletteButtonSpecStyle.Context, @"Context"),
        //    new(PaletteButtonSpecStyle.Next, @"Next"),
        //    new(PaletteButtonSpecStyle.Previous, @"Previous"),
        //    new(PaletteButtonSpecStyle.Generic, @"Generic"),
        //    new(PaletteButtonSpecStyle.ArrowLeft, @"Arrow Left"),
        //    new(PaletteButtonSpecStyle.ArrowRight, @"Arrow Right"),
        //    new(PaletteButtonSpecStyle.ArrowUp, @"Arrow Up"),
        //    new(PaletteButtonSpecStyle.ArrowDown, @"Arrow Down"),
        //    new(PaletteButtonSpecStyle.DropDown, @"Drop Down"),
        //    new(PaletteButtonSpecStyle.PinVertical, @"Pin Vertical"),
        //    new(PaletteButtonSpecStyle.PinHorizontal, @"Pin Horizontal"),
        //    new(PaletteButtonSpecStyle.FormClose, @"Form Close"),
        //    new(PaletteButtonSpecStyle.FormMax, @"Form Max"),
        //    new(PaletteButtonSpecStyle.FormMin, @"Form Min"),
        //    new(PaletteButtonSpecStyle.FormRestore, @"Form Restore"),
        //    new(PaletteButtonSpecStyle.FormHelp, @"Form Help"),
        //    new(PaletteButtonSpecStyle.PendantClose, @"Pendant Close"),
        //    new(PaletteButtonSpecStyle.PendantMin, @"Pendant Min"),
        //    new(PaletteButtonSpecStyle.PendantRestore, @"Pendant Restore"),
        //    new(PaletteButtonSpecStyle.WorkspaceMaximize, @"Workspace Maximize"),
        //    new(PaletteButtonSpecStyle.WorkspaceRestore, @"Workspace Restore"),
        //    new(PaletteButtonSpecStyle.RibbonMinimize, @"Ribbon Minimize"),
        //    new(PaletteButtonSpecStyle.RibbonExpand, @"Ribbon Expand"),
        //    new(PaletteButtonSpecStyle.New, @"New"),
        //    new(PaletteButtonSpecStyle.Open, @"Open"),
        //    new(PaletteButtonSpecStyle.Save, @"Save"),
        //    new(PaletteButtonSpecStyle.SaveAs, @"Save As"),
        //    new(PaletteButtonSpecStyle.SaveAll, @"Save All"),
        //    new(PaletteButtonSpecStyle.Cut, @"Cut"),
        //    new(PaletteButtonSpecStyle.Copy, @"Copy"),
        //    new(PaletteButtonSpecStyle.Paste, @"Paste"),
        //    new(PaletteButtonSpecStyle.Undo, @"Undo"),
        //    new(PaletteButtonSpecStyle.Redo, @"Redo"),
        //    new(PaletteButtonSpecStyle.PageSetup, @"Page Setup"),
        //    new(PaletteButtonSpecStyle.PrintPreview, @"Print Preview"),
        //    new(PaletteButtonSpecStyle.Print, @"Print"),
        //    new(PaletteButtonSpecStyle.QuickPrint, @"Quick Print")
        //};

        #endregion

        [Localizable(true)]
        private readonly Pair[] _pairs =
        {
            new Pair(PaletteButtonSpecStyle.Close, KryptonLanguageManager.ButtonSpecStyles.Close),
            new Pair(PaletteButtonSpecStyle.Context, KryptonLanguageManager.ButtonSpecStyles.Context),
            new Pair(PaletteButtonSpecStyle.Next, KryptonLanguageManager.ButtonSpecStyles.Next),
            new Pair(PaletteButtonSpecStyle.Previous, KryptonLanguageManager.ButtonSpecStyles.Previous),
            new Pair(PaletteButtonSpecStyle.Generic, KryptonLanguageManager.ButtonSpecStyles.Generic),
            new Pair(PaletteButtonSpecStyle.ArrowLeft, KryptonLanguageManager.ButtonSpecStyles.ArrowLeft),
            new Pair(PaletteButtonSpecStyle.ArrowRight, KryptonLanguageManager.ButtonSpecStyles.ArrowRight),
            new Pair(PaletteButtonSpecStyle.ArrowUp, KryptonLanguageManager.ButtonSpecStyles.ArrowUp),
            new Pair(PaletteButtonSpecStyle.ArrowDown, KryptonLanguageManager.ButtonSpecStyles.ArrowDown),
            new Pair(PaletteButtonSpecStyle.DropDown, KryptonLanguageManager.ButtonSpecStyles.DropDown),
            new Pair(PaletteButtonSpecStyle.PinVertical, KryptonLanguageManager.ButtonSpecStyles.PinVertical),
            new Pair(PaletteButtonSpecStyle.PinHorizontal, KryptonLanguageManager.ButtonSpecStyles.PinHorizontal),
            new Pair(PaletteButtonSpecStyle.FormClose, KryptonLanguageManager.ButtonSpecStyles.FormClose),
            new Pair(PaletteButtonSpecStyle.FormMax, KryptonLanguageManager.ButtonSpecStyles.FormMaximise),
            new Pair(PaletteButtonSpecStyle.FormMin, KryptonLanguageManager.ButtonSpecStyles.FormMinimise),
            new Pair(PaletteButtonSpecStyle.FormRestore, KryptonLanguageManager.ButtonSpecStyles.FormRestore),
            new Pair(PaletteButtonSpecStyle.FormHelp, KryptonLanguageManager.ButtonSpecStyles.FormHelp),
            new Pair(PaletteButtonSpecStyle.PendantClose, KryptonLanguageManager.ButtonSpecStyles.PendantClose),
            new Pair(PaletteButtonSpecStyle.PendantMin, KryptonLanguageManager.ButtonSpecStyles.PendantMinimise),
            new Pair(PaletteButtonSpecStyle.PendantRestore, KryptonLanguageManager.ButtonSpecStyles.PendantRestore),
            new Pair(PaletteButtonSpecStyle.WorkspaceMaximize,
                KryptonLanguageManager.ButtonSpecStyles.WorkspaceMaximise),
            new Pair(PaletteButtonSpecStyle.WorkspaceRestore, KryptonLanguageManager.ButtonSpecStyles.WorkspaceRestore),
            new Pair(PaletteButtonSpecStyle.RibbonMinimize, KryptonLanguageManager.ButtonSpecStyles.RibbonMinimise),
            new Pair(PaletteButtonSpecStyle.RibbonExpand, KryptonLanguageManager.ButtonSpecStyles.RibbonExpand),
            new Pair(PaletteButtonSpecStyle.New, KryptonLanguageManager.ToolBarStrings.New),
            new Pair(PaletteButtonSpecStyle.Open, KryptonLanguageManager.ToolBarStrings.Open),
            new Pair(PaletteButtonSpecStyle.Save, KryptonLanguageManager.ToolBarStrings.Save),
            new Pair(PaletteButtonSpecStyle.SaveAs, KryptonLanguageManager.ToolBarStrings.SaveAs),
            new Pair(PaletteButtonSpecStyle.SaveAll, KryptonLanguageManager.ToolBarStrings.SaveAll),
            new Pair(PaletteButtonSpecStyle.Cut, KryptonLanguageManager.ToolBarStrings.Cut),
            new Pair(PaletteButtonSpecStyle.Copy, KryptonLanguageManager.ToolBarStrings.Copy),
            new Pair(PaletteButtonSpecStyle.Paste, KryptonLanguageManager.ToolBarStrings.Paste),
            new Pair(PaletteButtonSpecStyle.Undo, KryptonLanguageManager.ToolBarStrings.Undo),
            new Pair(PaletteButtonSpecStyle.Redo, KryptonLanguageManager.ToolBarStrings.Redo),
            new Pair(PaletteButtonSpecStyle.PageSetup, KryptonLanguageManager.ToolBarStrings.PageSetup),
            new Pair(PaletteButtonSpecStyle.PrintPreview, KryptonLanguageManager.ToolBarStrings.PrintPreview),
            new Pair(PaletteButtonSpecStyle.Print, KryptonLanguageManager.ToolBarStrings.Print),
            new Pair(PaletteButtonSpecStyle.QuickPrint, KryptonLanguageManager.ToolBarStrings.QuickPrint)
        };

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
        protected override Pair[] Pairs => _pairs;

        #endregion
    }
}
