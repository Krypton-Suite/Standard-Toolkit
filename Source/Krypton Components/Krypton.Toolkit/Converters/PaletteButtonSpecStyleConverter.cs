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
    internal class PaletteButtonSpecStyleConverter : StringLookupConverter<PaletteButtonSpecStyle>
    {
        #region Static Fields

        [Localizable(true)]
        private static readonly IReadOnlyDictionary<PaletteButtonSpecStyle, string> _pairs = new Dictionary<PaletteButtonSpecStyle, string>
        {
            { PaletteButtonSpecStyle.Close, KryptonLanguageManager.ButtonSpecStyles.Close},
            {PaletteButtonSpecStyle.Context, KryptonLanguageManager.ButtonSpecStyles.Context},
            {PaletteButtonSpecStyle.Next, KryptonLanguageManager.ButtonSpecStyles.Next},
            {PaletteButtonSpecStyle.Previous, KryptonLanguageManager.ButtonSpecStyles.Previous},
            {PaletteButtonSpecStyle.Generic, KryptonLanguageManager.ButtonSpecStyles.Generic},
            {PaletteButtonSpecStyle.ArrowLeft, KryptonLanguageManager.ButtonSpecStyles.ArrowLeft},
            {PaletteButtonSpecStyle.ArrowRight, KryptonLanguageManager.ButtonSpecStyles.ArrowRight},
            {PaletteButtonSpecStyle.ArrowUp, KryptonLanguageManager.ButtonSpecStyles.ArrowUp},
            {PaletteButtonSpecStyle.ArrowDown, KryptonLanguageManager.ButtonSpecStyles.ArrowDown},
            {PaletteButtonSpecStyle.DropDown, KryptonLanguageManager.ButtonSpecStyles.DropDown},
            {PaletteButtonSpecStyle.PinVertical, KryptonLanguageManager.ButtonSpecStyles.PinVertical},
            {PaletteButtonSpecStyle.PinHorizontal, KryptonLanguageManager.ButtonSpecStyles.PinHorizontal},
            {PaletteButtonSpecStyle.FormClose, KryptonLanguageManager.ButtonSpecStyles.FormClose},
            {PaletteButtonSpecStyle.FormMax, KryptonLanguageManager.ButtonSpecStyles.FormMaximise},
            {PaletteButtonSpecStyle.FormMin, KryptonLanguageManager.ButtonSpecStyles.FormMinimise},
            {PaletteButtonSpecStyle.FormRestore, KryptonLanguageManager.ButtonSpecStyles.FormRestore},
            {PaletteButtonSpecStyle.FormHelp, KryptonLanguageManager.ButtonSpecStyles.FormHelp},
            {PaletteButtonSpecStyle.PendantClose, KryptonLanguageManager.ButtonSpecStyles.PendantClose},
            {PaletteButtonSpecStyle.PendantMin, KryptonLanguageManager.ButtonSpecStyles.PendantMinimise},
            {PaletteButtonSpecStyle.PendantRestore, KryptonLanguageManager.ButtonSpecStyles.PendantRestore},
            {PaletteButtonSpecStyle.WorkspaceMaximize, KryptonLanguageManager.ButtonSpecStyles.WorkspaceMaximise},
            {PaletteButtonSpecStyle.WorkspaceRestore, KryptonLanguageManager.ButtonSpecStyles.WorkspaceRestore},
            {PaletteButtonSpecStyle.RibbonMinimize, KryptonLanguageManager.ButtonSpecStyles.RibbonMinimise},
            {PaletteButtonSpecStyle.RibbonExpand, KryptonLanguageManager.ButtonSpecStyles.RibbonExpand},
            {PaletteButtonSpecStyle.New, KryptonLanguageManager.ToolBarStrings.New},
            {PaletteButtonSpecStyle.Open, KryptonLanguageManager.ToolBarStrings.Open},
            {PaletteButtonSpecStyle.Save, KryptonLanguageManager.ToolBarStrings.Save},
            {PaletteButtonSpecStyle.SaveAs, KryptonLanguageManager.ToolBarStrings.SaveAs},
            {PaletteButtonSpecStyle.SaveAll, KryptonLanguageManager.ToolBarStrings.SaveAll},
            {PaletteButtonSpecStyle.Cut, KryptonLanguageManager.ToolBarStrings.Cut},
            {PaletteButtonSpecStyle.Copy, KryptonLanguageManager.ToolBarStrings.Copy},
            {PaletteButtonSpecStyle.Paste, KryptonLanguageManager.ToolBarStrings.Paste},
            {PaletteButtonSpecStyle.Undo, KryptonLanguageManager.ToolBarStrings.Undo},
            {PaletteButtonSpecStyle.Redo, KryptonLanguageManager.ToolBarStrings.Redo},
            {PaletteButtonSpecStyle.PageSetup, KryptonLanguageManager.ToolBarStrings.PageSetup},
            {PaletteButtonSpecStyle.PrintPreview, KryptonLanguageManager.ToolBarStrings.PrintPreview},
            {PaletteButtonSpecStyle.Print, KryptonLanguageManager.ToolBarStrings.Print},
            {PaletteButtonSpecStyle.QuickPrint, KryptonLanguageManager.ToolBarStrings.QuickPrint}
        };

        #endregion

        #region Protected

        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override IReadOnlyDictionary<PaletteButtonSpecStyle /*Enum*/, string /*Display*/> Pairs => _pairs;

        #endregion
    }
}
