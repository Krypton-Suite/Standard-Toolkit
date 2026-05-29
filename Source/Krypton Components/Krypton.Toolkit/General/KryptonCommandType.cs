#region BSD License
/*
 *
 *   BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public enum KryptonCommandType
{
    General = 0,
    HelpCommand = 1,
    IntegratedToolBarCopyCommand = 2,
    IntegratedToolBarCutCommand = 3,
    IntegratedToolBarNewCommand = 4,
    IntegratedToolBarOpenCommand = 5,
    IntegratedToolBarPageSetupCommand = 6,
    IntegratedToolBarPasteCommand = 7,
    IntegratedToolBarPrintCommand = 8,
    IntegratedToolBarPrintPreviewCommand = 9,
    IntegratedToolBarQuickPrintCommand = 10,
    IntegratedToolBarRedoCommand = 11,
    IntegratedToolBarSaveAllCommand = 12,
    IntegratedToolBarSaveAsCommand = 13,
    IntegratedToolBarSaveCommand = 14,
    IntegratedToolBarUndoCommand = 15
}