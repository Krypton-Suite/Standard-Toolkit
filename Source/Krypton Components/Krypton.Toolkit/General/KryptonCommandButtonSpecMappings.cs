#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Maps <see cref="KryptonCommandType"/> values to <see cref="PaletteButtonSpecStyle"/> and default display text.
/// </summary>
internal static class KryptonCommandButtonSpecMappings
{
    /// <summary>
    /// Gets the button spec style for a command type, when one is defined.
    /// </summary>
    public static bool TryGetButtonSpecStyle(KryptonCommandType commandType, out PaletteButtonSpecStyle style)
    {
        switch (commandType)
        {
            case KryptonCommandType.HelpCommand:
                style = PaletteButtonSpecStyle.FormHelp;
                return true;
            case KryptonCommandType.IntegratedToolBarNewCommand:
                style = PaletteButtonSpecStyle.New;
                return true;
            case KryptonCommandType.IntegratedToolBarOpenCommand:
                style = PaletteButtonSpecStyle.Open;
                return true;
            case KryptonCommandType.IntegratedToolBarSaveCommand:
                style = PaletteButtonSpecStyle.Save;
                return true;
            case KryptonCommandType.IntegratedToolBarSaveAsCommand:
                style = PaletteButtonSpecStyle.SaveAs;
                return true;
            case KryptonCommandType.IntegratedToolBarSaveAllCommand:
                style = PaletteButtonSpecStyle.SaveAll;
                return true;
            case KryptonCommandType.IntegratedToolBarCutCommand:
                style = PaletteButtonSpecStyle.Cut;
                return true;
            case KryptonCommandType.IntegratedToolBarCopyCommand:
                style = PaletteButtonSpecStyle.Copy;
                return true;
            case KryptonCommandType.IntegratedToolBarPasteCommand:
                style = PaletteButtonSpecStyle.Paste;
                return true;
            case KryptonCommandType.IntegratedToolBarUndoCommand:
                style = PaletteButtonSpecStyle.Undo;
                return true;
            case KryptonCommandType.IntegratedToolBarRedoCommand:
                style = PaletteButtonSpecStyle.Redo;
                return true;
            case KryptonCommandType.IntegratedToolBarPageSetupCommand:
                style = PaletteButtonSpecStyle.PageSetup;
                return true;
            case KryptonCommandType.IntegratedToolBarPrintPreviewCommand:
                style = PaletteButtonSpecStyle.PrintPreview;
                return true;
            case KryptonCommandType.IntegratedToolBarPrintCommand:
                style = PaletteButtonSpecStyle.Print;
                return true;
            case KryptonCommandType.IntegratedToolBarQuickPrintCommand:
                style = PaletteButtonSpecStyle.QuickPrint;
                return true;
            default:
                style = PaletteButtonSpecStyle.Generic;
                return false;
        }
    }

    /// <summary>
    /// Gets localized default text for a typed command.
    /// </summary>
    public static string GetDefaultText(KryptonCommandType commandType)
    {
        var toolBarStrings = KryptonManager.Strings.ToolBarStrings;

        switch (commandType)
        {
            case KryptonCommandType.HelpCommand:
                return KryptonManager.Strings.ButtonSpecStyleStrings.FormHelp;
            case KryptonCommandType.IntegratedToolBarNewCommand:
                return toolBarStrings.New;
            case KryptonCommandType.IntegratedToolBarOpenCommand:
                return toolBarStrings.Open;
            case KryptonCommandType.IntegratedToolBarSaveCommand:
                return toolBarStrings.Save;
            case KryptonCommandType.IntegratedToolBarSaveAsCommand:
                return toolBarStrings.SaveAs;
            case KryptonCommandType.IntegratedToolBarSaveAllCommand:
                return toolBarStrings.SaveAll;
            case KryptonCommandType.IntegratedToolBarCutCommand:
                return toolBarStrings.Cut;
            case KryptonCommandType.IntegratedToolBarCopyCommand:
                return toolBarStrings.Copy;
            case KryptonCommandType.IntegratedToolBarPasteCommand:
                return toolBarStrings.Paste;
            case KryptonCommandType.IntegratedToolBarUndoCommand:
                return toolBarStrings.Undo;
            case KryptonCommandType.IntegratedToolBarRedoCommand:
                return toolBarStrings.Redo;
            case KryptonCommandType.IntegratedToolBarPageSetupCommand:
                return toolBarStrings.PageSetup;
            case KryptonCommandType.IntegratedToolBarPrintPreviewCommand:
                return toolBarStrings.PrintPreview;
            case KryptonCommandType.IntegratedToolBarPrintCommand:
                return toolBarStrings.Print;
            case KryptonCommandType.IntegratedToolBarQuickPrintCommand:
                return toolBarStrings.QuickPrint;
            default:
                return string.Empty;
        }
    }
}
