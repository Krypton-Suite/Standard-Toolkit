#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Demonstrates <see cref="KryptonCommand.CommandType"/> with integrated toolbar and help button specs.
/// </summary>
public class KryptonCommandButtonSpecDemo : KryptonForm
{
    private readonly KryptonIntegratedToolBarManager _toolBarManager = new();

    public KryptonCommandButtonSpecDemo()
    {
        Text = "KryptonCommand ButtonSpec demo";
        Width = 900;
        Height = 500;
        StartPosition = FormStartPosition.CenterScreen;

        var helpCommand = new KryptonCommand
        {
            CommandType = KryptonCommandType.HelpCommand
        };
        helpCommand.Execute += (_, _) => KryptonMessageBox.Show(this, "Help command executed.", "Help");

        var helpSpec = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.FormHelp,
            KryptonCommand = helpCommand
        };
        ButtonSpecs.Add(helpSpec);

        ConfigureIntegratedToolbarCommands();
        _toolBarManager.ParentForm = this;
        _toolBarManager.AttachIntegratedToolBarToParent(this);
    }

    private void ConfigureIntegratedToolbarCommands()
    {
        var commands = KryptonIntegratedToolBarManager.IntegratedToolBarCommandValues;

        commands.NewButtonCommand = CreateToolbarCommand(KryptonCommandType.IntegratedToolBarNewCommand, "New");
        commands.OpenButtonCommand = CreateToolbarCommand(KryptonCommandType.IntegratedToolBarOpenCommand, "Open");
        commands.SaveButtonCommand = CreateToolbarCommand(KryptonCommandType.IntegratedToolBarSaveCommand, "Save");
        commands.SaveAsButtonCommand = CreateToolbarCommand(KryptonCommandType.IntegratedToolBarSaveAsCommand, "Save As");
        commands.SaveAllButtonCommand = CreateToolbarCommand(KryptonCommandType.IntegratedToolBarSaveAllCommand, "Save All");
        commands.CutButtonCommand = CreateToolbarCommand(KryptonCommandType.IntegratedToolBarCutCommand, "Cut");
        commands.CopyButtonCommand = CreateToolbarCommand(KryptonCommandType.IntegratedToolBarCopyCommand, "Copy");
        commands.PasteButtonCommand = CreateToolbarCommand(KryptonCommandType.IntegratedToolBarPasteCommand, "Paste");
        commands.UndoButtonCommand = CreateToolbarCommand(KryptonCommandType.IntegratedToolBarUndoCommand, "Undo");
        commands.RedoButtonCommand = CreateToolbarCommand(KryptonCommandType.IntegratedToolBarRedoCommand, "Redo");
        commands.PageSetupButtonCommand = CreateToolbarCommand(KryptonCommandType.IntegratedToolBarPageSetupCommand, "Page Setup");
        commands.PrintPreviewButtonCommand = CreateToolbarCommand(KryptonCommandType.IntegratedToolBarPrintPreviewCommand, "Print Preview");
        commands.PrintButtonCommand = CreateToolbarCommand(KryptonCommandType.IntegratedToolBarPrintCommand, "Print");
        commands.QuickPrintButtonCommand = CreateToolbarCommand(KryptonCommandType.IntegratedToolBarQuickPrintCommand, "Quick Print");

        _toolBarManager.RefreshToolBarCommands();
    }

    private static KryptonCommand CreateToolbarCommand(KryptonCommandType commandType, string actionName)
    {
        var command = new KryptonCommand
        {
            CommandType = commandType
        };
        command.Execute += (_, _) => KryptonMessageBox.Show($"{actionName} command executed.", actionName);
        return command;
    }
}
