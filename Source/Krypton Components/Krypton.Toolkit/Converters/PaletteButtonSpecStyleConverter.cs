#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Custom type converter so that PaletteButtonSpecStyle values appear as neat text at design time.
/// </summary>
internal class PaletteButtonSpecStyleConverter : StringLookupConverter<PaletteButtonSpecStyle>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<PaletteButtonSpecStyle, string> _pairs = new BiDictionary<PaletteButtonSpecStyle, string>(
        new Dictionary<PaletteButtonSpecStyle, string>
        {
            {PaletteButtonSpecStyle.Close, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_CLOSE},
            {PaletteButtonSpecStyle.Context, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_CONTEXT},
            {PaletteButtonSpecStyle.Next, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_NEXT},
            {PaletteButtonSpecStyle.Previous, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PREVIOUS},
            {PaletteButtonSpecStyle.Generic, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_GENERIC},
            {PaletteButtonSpecStyle.ArrowLeft, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_LEFT},
            {PaletteButtonSpecStyle.ArrowRight, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_RIGHT},
            {PaletteButtonSpecStyle.ArrowUp, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_UP},
            {PaletteButtonSpecStyle.ArrowDown, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_DOWN},
            {PaletteButtonSpecStyle.DropDown, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_DROP_DOWN},
            {PaletteButtonSpecStyle.PinVertical, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PIN_VERTICAL},
            {PaletteButtonSpecStyle.PinHorizontal, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PIN_HORIZONTAL},
            {PaletteButtonSpecStyle.FormClose, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_CLOSE},
            {PaletteButtonSpecStyle.FormMax, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_MAX},
            {PaletteButtonSpecStyle.FormMin, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_MIN},
            {PaletteButtonSpecStyle.FormRestore, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_RESTORE},
            {PaletteButtonSpecStyle.FormHelp, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_HELP},
            {PaletteButtonSpecStyle.PendantClose, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PENDANT_CLOSE},
            {PaletteButtonSpecStyle.PendantMin, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PENDANT_MIN},
            {PaletteButtonSpecStyle.PendantRestore, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PENDANT_RESTORE},
            {PaletteButtonSpecStyle.WorkspaceMaximize, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_WORKSPACE_MAXIMIZE},
            {PaletteButtonSpecStyle.WorkspaceRestore, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_WORKSPACE_RESTORE},
            {PaletteButtonSpecStyle.RibbonMinimize, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_RIBBON_MINIMIZE},
            {PaletteButtonSpecStyle.RibbonExpand, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_RIBBON_EXPAND},
            {PaletteButtonSpecStyle.New, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_NEW},
            {PaletteButtonSpecStyle.Open, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_OPEN},
            {PaletteButtonSpecStyle.Save, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_SAVE},
            {PaletteButtonSpecStyle.SaveAs, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_SAVE_AS},
            {PaletteButtonSpecStyle.SaveAll, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_SAVE_ALL},
            {PaletteButtonSpecStyle.Cut, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_CUT},
            {PaletteButtonSpecStyle.Copy, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_COPY},
            {PaletteButtonSpecStyle.Paste, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PASTE},
            {PaletteButtonSpecStyle.Undo, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_UNDO},
            {PaletteButtonSpecStyle.Redo, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_REDO},
            {PaletteButtonSpecStyle.PageSetup, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PAGE_SETUP},
            {PaletteButtonSpecStyle.PrintPreview, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PRINT_PREVIEW},
            {PaletteButtonSpecStyle.Print, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PRINT},
            {PaletteButtonSpecStyle.QuickPrint, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_SPEC_STYLE_QUICK_PRINT}
        });

    #endregion

    #region Protected

    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<PaletteButtonSpecStyle /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    protected override IReadOnlyDictionary<string /*Display*/, PaletteButtonSpecStyle /*Enum*/ > PairsStringToEnum => _pairs.SecondToFirst;

    #endregion
}