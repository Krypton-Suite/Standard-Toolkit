#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Wires <see cref="IntegratedToolBarCommandValues"/> to integrated toolbar <see cref="ButtonSpecAny"/> instances.
/// </summary>
public static class IntegratedToolBarCommandWiring
{
    #region Public

    /// <summary>
    /// Assigns non-null commands from <paramref name="commands"/> to the integrated toolbar buttons.
    /// </summary>
    /// <param name="buttons">Integrated toolbar button array (14 items).</param>
    /// <param name="commands">Command values to apply.</param>
    public static void ApplyCommands(ButtonSpecAny[] buttons, IntegratedToolBarCommandValues commands)
    {
        if (buttons.Length < 14)
        {
            return;
        }

        Wire(buttons[0], commands.NewButtonCommand);
        Wire(buttons[1], commands.OpenButtonCommand);
        Wire(buttons[2], commands.SaveButtonCommand);
        Wire(buttons[3], commands.SaveAsButtonCommand);
        Wire(buttons[4], commands.SaveAllButtonCommand);
        Wire(buttons[5], commands.CutButtonCommand);
        Wire(buttons[6], commands.CopyButtonCommand);
        Wire(buttons[7], commands.PasteButtonCommand);
        Wire(buttons[8], commands.UndoButtonCommand);
        Wire(buttons[9], commands.RedoButtonCommand);
        Wire(buttons[10], commands.PageSetupButtonCommand);
        Wire(buttons[11], commands.PrintPreviewButtonCommand);
        Wire(buttons[12], commands.PrintButtonCommand);
        Wire(buttons[13], commands.QuickPrintButtonCommand);
    }

    #endregion

    #region Implementation

    private static void Wire(ButtonSpecAny button, KryptonCommand? command)
    {
        if (command != null)
        {
            button.KryptonCommand = command;
        }
    }

    #endregion
}
