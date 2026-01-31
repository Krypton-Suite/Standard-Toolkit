#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class IntegratedToolBarCommandValues : GlobalId
{
    #region Static Fields

    private const KryptonIntegratedToolbarNewCommand? DEFAULT_INTEGRATED_NEW_TOOL_BAR_COMMAND = null;

    private const KryptonIntegratedToolbarOpenCommand? DEFAULT_INTEGRATED_OPEN_TOOL_BAR_COMMAND = null;

    private const KryptonIntegratedToolbarSaveCommand? DEFAULT_INTEGRATED_SAVE_TOOL_BAR_COMMAND = null;

    private const KryptonIntegratedToolbarSaveAllCommand? DEFAULT_INTEGRATED_SAVE_ALL_TOOL_BAR_COMMAND = null;

    private const KryptonIntegratedToolbarSaveAsCommand? DEFAULT_INTEGRATED_SAVE_AS_TOOL_BAR_COMMAND = null;

    private const KryptonIntegratedToolbarCutCommand? DEFAULT_INTEGRATED_CUT_TOOL_BAR_COMMAND = null;

    private const KryptonIntegratedToolbarCopyCommand? DEFAULT_INTEGRATED_COPY_TOOL_BAR_COMMAND = null;

    private const KryptonIntegratedToolbarPasteCommand? DEFAULT_INTEGRATED_PASTE_TOOL_BAR_COMMAND = null;

    private const KryptonIntegratedToolbarUndoCommand? DEFAULT_INTEGRATED_UNDO_TOOL_BAR_COMMAND = null;

    private const KryptonIntegratedToolbarRedoCommand? DEFAULT_INTEGRATED_REDO_TOOL_BAR_COMMAND = null;

    private const KryptonIntegratedToolbarPageSetupCommand? DEFAULT_INTEGRATED_PAGE_SETUP_TOOL_BAR_COMMAND = null;

    private const KryptonIntegratedToolbarPrintPreviewCommand? DEFAULT_INTEGRATED_PRINT_PREVIEW_TOOL_BAR_COMMAND = null;

    private const KryptonIntegratedToolbarPrintCommand? DEFAULT_INTEGRATED_PRINT_TOOL_BAR_COMMAND = null;

    private const KryptonIntegratedToolbarQuickPrintCommand? DEFAULT_INTEGRATED_QUICK_PRINT_TOOL_BAR_COMMAND = null;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="IntegratedToolBarCommandValues" /> class.</summary>
    public IntegratedToolBarCommandValues()
    {
        Reset();
    }

    /// <summary>Converts to string.</summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticValues.DEFAULT_EMPTY_STRING;

    #endregion

    #region Public

    /// <summary>Gets a value indicating whether this instance is default.</summary>
    /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
    [Browsable(false)]
    public bool IsDefault => NewButtonCommand.Equals(DEFAULT_INTEGRATED_NEW_TOOL_BAR_COMMAND) &&
                             OpenButtonCommand.Equals(DEFAULT_INTEGRATED_OPEN_TOOL_BAR_COMMAND) &&
                             SaveButtonCommand.Equals(DEFAULT_INTEGRATED_SAVE_TOOL_BAR_COMMAND) &&
                             SaveAllButtonCommand.Equals(DEFAULT_INTEGRATED_SAVE_ALL_TOOL_BAR_COMMAND) &&
                             SaveAsButtonCommand.Equals(DEFAULT_INTEGRATED_SAVE_AS_TOOL_BAR_COMMAND) &&
                             CutButtonCommand.Equals(DEFAULT_INTEGRATED_CUT_TOOL_BAR_COMMAND) &&
                             CopyButtonCommand.Equals(DEFAULT_INTEGRATED_COPY_TOOL_BAR_COMMAND) &&
                             PasteButtonCommand.Equals(DEFAULT_INTEGRATED_PASTE_TOOL_BAR_COMMAND) &&
                             UndoButtonCommand.Equals(DEFAULT_INTEGRATED_UNDO_TOOL_BAR_COMMAND) &&
                             RedoButtonCommand.Equals(DEFAULT_INTEGRATED_REDO_TOOL_BAR_COMMAND) &&
                             PageSetupButtonCommand.Equals(DEFAULT_INTEGRATED_PAGE_SETUP_TOOL_BAR_COMMAND) &&
                             PrintPreviewButtonCommand.Equals(DEFAULT_INTEGRATED_PRINT_PREVIEW_TOOL_BAR_COMMAND) &&
                             PrintButtonCommand.Equals(DEFAULT_INTEGRATED_PRINT_TOOL_BAR_COMMAND) &&
                             QuickPrintButtonCommand.Equals(DEFAULT_INTEGRATED_QUICK_PRINT_TOOL_BAR_COMMAND);

    /// <summary>Resets this instance.</summary>
    public void Reset()
    {
        NewButtonCommand = DEFAULT_INTEGRATED_NEW_TOOL_BAR_COMMAND!;

        OpenButtonCommand = DEFAULT_INTEGRATED_OPEN_TOOL_BAR_COMMAND!;

        SaveButtonCommand = DEFAULT_INTEGRATED_SAVE_TOOL_BAR_COMMAND!;

        SaveAllButtonCommand = DEFAULT_INTEGRATED_SAVE_ALL_TOOL_BAR_COMMAND!;

        SaveAsButtonCommand = DEFAULT_INTEGRATED_SAVE_AS_TOOL_BAR_COMMAND!;

        CutButtonCommand = DEFAULT_INTEGRATED_CUT_TOOL_BAR_COMMAND!;

        CopyButtonCommand = DEFAULT_INTEGRATED_COPY_TOOL_BAR_COMMAND!;

        PasteButtonCommand = DEFAULT_INTEGRATED_PASTE_TOOL_BAR_COMMAND!;

        UndoButtonCommand = DEFAULT_INTEGRATED_UNDO_TOOL_BAR_COMMAND!;

        RedoButtonCommand = DEFAULT_INTEGRATED_REDO_TOOL_BAR_COMMAND!;

        PageSetupButtonCommand = DEFAULT_INTEGRATED_PAGE_SETUP_TOOL_BAR_COMMAND!;

        PrintPreviewButtonCommand = DEFAULT_INTEGRATED_PRINT_PREVIEW_TOOL_BAR_COMMAND!;

        PrintButtonCommand = DEFAULT_INTEGRATED_PRINT_TOOL_BAR_COMMAND!;

        QuickPrintButtonCommand = DEFAULT_INTEGRATED_QUICK_PRINT_TOOL_BAR_COMMAND!;
    }

    public KryptonIntegratedToolbarNewCommand NewButtonCommand { get; set; }

    public KryptonIntegratedToolbarOpenCommand OpenButtonCommand { get; set; }

    public KryptonIntegratedToolbarSaveCommand SaveButtonCommand { get; set; }

    public KryptonIntegratedToolbarSaveAllCommand SaveAllButtonCommand { get; set; }

    public KryptonIntegratedToolbarSaveAsCommand SaveAsButtonCommand { get; set; }

    public KryptonIntegratedToolbarCutCommand CutButtonCommand { get; set; }

    public KryptonIntegratedToolbarCopyCommand CopyButtonCommand { get; set; }

    public KryptonIntegratedToolbarPasteCommand PasteButtonCommand { get; set; }

    public KryptonIntegratedToolbarUndoCommand UndoButtonCommand { get; set; }

    public KryptonIntegratedToolbarRedoCommand RedoButtonCommand { get; set; }

    public KryptonIntegratedToolbarPageSetupCommand PageSetupButtonCommand { get; set; }

    public KryptonIntegratedToolbarPrintPreviewCommand PrintPreviewButtonCommand { get; set; }

    public KryptonIntegratedToolbarPrintCommand PrintButtonCommand { get; set; }

    public KryptonIntegratedToolbarQuickPrintCommand QuickPrintButtonCommand { get; set; }

    #endregion
}