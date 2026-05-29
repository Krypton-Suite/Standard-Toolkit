#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable UnusedMember.Local
// ReSharper disable InconsistentNaming
namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class IntegratedToolBarButtonValues : GlobalId
{
    #region Static Fields

    private const bool DEFAULT_SHOW_NEW_BUTTON = true;
    private const bool DEFAULT_SHOW_OPEN_BUTTON = true;
    private const bool DEFAULT_SHOW_SAVE_BUTTON = true;
    private const bool DEFAULT_SHOW_SAVE_ALL_BUTTON = true;
    private const bool DEFAULT_SHOW_SAVE_AS_BUTTON = true;
    private const bool DEFAULT_SHOW_CUT_BUTTON = true;
    private const bool DEFAULT_SHOW_COPY_BUTTON = true;
    private const bool DEFAULT_SHOW_PASTE_BUTTON = true;
    private const bool DEFAULT_SHOW_UNDO_BUTTON = true;
    private const bool DEFAULT_SHOW_REDO_BUTTON = true;
    private const bool DEFAULT_SHOW_PAGE_SETUP_BUTTON = true;
    private const bool DEFAULT_SHOW_PRINT_PREVIEW_BUTTON = true;
    private const bool DEFAULT_SHOW_PRINT_BUTTON = true;
    private const bool DEFAULT_SHOW_QUICK_PRINT_BUTTON = true;

    #endregion

    #region Instance Fields

    private bool _showNewButton;

    private bool _showOpenButton;

    private bool _showSaveButton;

    private bool _showSaveAllButton;

    private bool _showSaveAsButton;

    private bool _showCutButton;

    private bool _showCopyButton;

    private bool _showPasteButton;

    private bool _showUndoButton;

    private bool _showRedoButton;

    private bool _showPageSetupButton;

    private bool _showPrintPreviewButton;

    private bool _showPrintButton;

    private bool _showQuickPrintButton;

    private readonly KryptonIntegratedToolBarManager _toolBarManager = new();

    #endregion

    #region Public

    [Browsable(false)]
    public bool IsDefault => ShowNewButton.Equals(DEFAULT_SHOW_NEW_BUTTON) &&
                             ShowOpenButton.Equals(DEFAULT_SHOW_OPEN_BUTTON) &&
                             ShowSaveButton.Equals(DEFAULT_SHOW_SAVE_BUTTON) &&
                             ShowSaveAllButton.Equals(DEFAULT_SHOW_SAVE_ALL_BUTTON) &&
                             ShowSaveAsButton.Equals(DEFAULT_SHOW_SAVE_AS_BUTTON) &&
                             ShowCutButton.Equals(DEFAULT_SHOW_CUT_BUTTON) &&
                             ShowCopyButton.Equals(DEFAULT_SHOW_COPY_BUTTON) &&
                             ShowPasteButton.Equals(DEFAULT_SHOW_PASTE_BUTTON) &&
                             ShowUndoButton.Equals(DEFAULT_SHOW_UNDO_BUTTON) &&
                             ShowRedoButton.Equals(DEFAULT_SHOW_REDO_BUTTON) &&
                             ShowPageSetupButton.Equals(DEFAULT_SHOW_PAGE_SETUP_BUTTON) &&
                             ShowPrintPreviewButton.Equals(DEFAULT_SHOW_PRINT_PREVIEW_BUTTON) &&
                             ShowPrintButton.Equals(DEFAULT_SHOW_PRINT_BUTTON) &&
                             ShowQuickPrintButton.Equals(DEFAULT_SHOW_QUICK_PRINT_BUTTON);

    public bool ShowNewButton { get => _showNewButton; set { _showNewButton = value; _toolBarManager.ToggleNewButton(value); } }

    public bool ShowOpenButton { get => _showOpenButton; set { _showOpenButton = value; _toolBarManager.ToggleOpenButton(value); } }

    public bool ShowSaveButton { get => _showSaveButton; set { _showSaveButton = value; _toolBarManager.ToggleSaveButton(value); } }

    public bool ShowSaveAllButton { get => _showSaveAllButton; set { _showSaveAllButton = value; _toolBarManager.ToggleSaveAllButton(value); } }

    public bool ShowSaveAsButton { get => _showSaveAsButton; set { _showSaveAsButton = value; _toolBarManager.ToggleSaveAsButton(value); } }

    public bool ShowCutButton { get => _showCutButton; set { _showCutButton = value; _toolBarManager.ToggleCutButton(value); } }

    public bool ShowCopyButton { get => _showCopyButton; set { _showCopyButton = value; _toolBarManager.ToggleCopyButton(value); } }

    public bool ShowPasteButton { get => _showPasteButton; set { _showPasteButton = value; _toolBarManager.TogglePasteButton(value); } }

    public bool ShowUndoButton { get => _showUndoButton; set { _showUndoButton = value; _toolBarManager.ToggleUndoButton(value); } }

    public bool ShowRedoButton { get => _showRedoButton; set { _showRedoButton = value; _toolBarManager.ToggleRedoButton(value); } }

    public bool ShowPageSetupButton { get => _showPageSetupButton; set { _showPageSetupButton = value; _toolBarManager.TogglePageSetupButton(value); } }

    public bool ShowPrintPreviewButton { get => _showPrintPreviewButton; set { _showPrintPreviewButton = value; _toolBarManager.TogglePrintPreviewButton(value); } }

    public bool ShowPrintButton { get => _showPrintButton; set { _showPrintButton = value; _toolBarManager.TogglePrintButton(value); } }

    public bool ShowQuickPrintButton { get => _showQuickPrintButton; set { _showQuickPrintButton = value; _toolBarManager.ToggleQuickPrintButton(value); } }

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="IntegratedToolBarButtonValues" /> class.</summary>
    public IntegratedToolBarButtonValues()
    {
        _toolBarManager.Reset();
    }

    /// <summary>Converts to string.</summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticValues.DEFAULT_EMPTY_STRING;

    #endregion

    #region Implementation

    public void Reset()
    {
        ShowNewButton = DEFAULT_SHOW_NEW_BUTTON;

        ShowOpenButton = DEFAULT_SHOW_OPEN_BUTTON;

        ShowSaveButton = DEFAULT_SHOW_SAVE_BUTTON;

        ShowSaveAllButton = DEFAULT_SHOW_SAVE_ALL_BUTTON;

        ShowSaveAsButton = DEFAULT_SHOW_SAVE_AS_BUTTON;

        ShowCutButton = DEFAULT_SHOW_CUT_BUTTON;

        ShowCopyButton = DEFAULT_SHOW_COPY_BUTTON;

        ShowPasteButton = DEFAULT_SHOW_PASTE_BUTTON;

        ShowUndoButton = DEFAULT_SHOW_UNDO_BUTTON;

        ShowRedoButton = DEFAULT_SHOW_REDO_BUTTON;

        ShowPageSetupButton = DEFAULT_SHOW_PAGE_SETUP_BUTTON;

        ShowPrintPreviewButton = DEFAULT_SHOW_PRINT_PREVIEW_BUTTON;

        ShowPrintButton = DEFAULT_SHOW_PRINT_BUTTON;

        ShowQuickPrintButton = DEFAULT_SHOW_QUICK_PRINT_BUTTON;
    }

    #endregion
}