#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Handles all the integrated toolbar functionality.</summary>
[Category(@"code")]
[Description(@"Handles all the integrated toolbar functionality.")]
[ToolboxBitmap(typeof(ToolStrip))]
public class KryptonIntegratedToolBarManager : Component
{
    #region Static Fields

    private const int MAXIMUM_INTEGRATED_TOOLBAR_BUTTONS = 14;

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

    private bool _flipButtonArray;

    private ButtonSpecAny[] _integratedToolBarButtons;

    private PaletteButtonOrientation _integratedToolBarButtonOrientation;

    private PaletteRelativeEdgeAlign _integratedToolBarButtonAlignment;

    private KryptonForm? _parentForm;

    //private IntegratedToolBarCommandValues _toolBarCommandValues;

    //private IntegratedToolBarButtonValues _toolBarButtonValues;

    #region Tool Bar Buttons

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

    #endregion

    #endregion

    #region Public

    /// <summary>Gets the integrated tool bar buttons.</summary>
    /// <value>The integrated tool bar buttons.</value>
    [Category(@"Visuals"), DefaultValue(null), Description(@"Contains all the integrated tool bar buttons.")]
    public ButtonSpecAny[] IntegratedToolBarButtons => _integratedToolBarButtons;

    /// <summary>Gets or sets the integrated tool bar button orientation.</summary>
    /// <value>The integrated tool bar button orientation.</value>
    [Category(@"Visuals"), DefaultValue(typeof(PaletteButtonOrientation), @"PaletteButtonOrientation.FixedTop"), Description(@"Gets or sets the integrated tool bar button orientation.")]
    public PaletteButtonOrientation IntegratedToolBarButtonOrientation { get => _integratedToolBarButtonOrientation; set { _integratedToolBarButtonOrientation = value; UpdateButtonOrientation(value); } }

    /// <summary>Gets or sets the integrated tool bar button alignment.</summary>
    /// <value>The integrated tool bar button alignment.</value>
    [Category(@"Visuals"), DefaultValue(typeof(PaletteRelativeEdgeAlign), @"PaletteRelativeEdgeAlign.Far"), Description(@"Gets or sets the integrated tool bar button alignment.")]
    public PaletteRelativeEdgeAlign IntegratedToolBarButtonAlignment { get => _integratedToolBarButtonAlignment; set { _integratedToolBarButtonAlignment = value; UpdateButtonAlignment(value); } }

    /// <summary>Gets or sets the parent form.</summary>
    /// <value>The parent form.</value>
    [Category(@"Visuals"), DefaultValue(null), Description(@"Gets or sets the parent form.")]
    public KryptonForm? ParentForm { get => _parentForm; set => _parentForm = value; }

    /*[Category(@"Visuals")]
    [Description(@"Handles the toolbar buttons.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public IntegratedToolBarButtonValues ToolBarButtonValues => IntegratedToolBarButtonValues;

    private bool ShouldSerializeToolBarButtonValues() => !IntegratedToolBarButtonValues.IsDefault;

    public void ResetToolBarButtonValues() => IntegratedToolBarButtonValues.Reset();*/

    /*[Category(@"Data")]
    [Description(@"Handles the toolbar buttons.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public IntegratedToolBarCommandValues ToolBarCommands => IntegratedToolBarCommandValues;

    private bool ShouldSerializeToolBarCommands() => !IntegratedToolBarCommandValues.IsDefault;

    private void ResetToolBarCommands() => IntegratedToolBarCommandValues.Reset();*/

    #region Tool Bar Buttons

    /// <summary>Gets or sets a value indicating whether [show new button].</summary>
    /// <value><c>true</c> if [show new button]; otherwise, <c>false</c>.</value>
    [Category(@"Visuals"), DefaultValue(false), Description(@"Show or hide the 'New' button.")]
    public bool ShowNewButton { get => _showNewButton; set { _showNewButton = value; ToggleNewButton(value); } }

    /// <summary>Gets or sets a value indicating whether [show open button].</summary>
    /// <value><c>true</c> if [show open button]; otherwise, <c>false</c>.</value>
    [Category(@"Visuals"), DefaultValue(false), Description(@"Show or hide the 'Open' button.")]
    public bool ShowOpenButton { get => _showOpenButton; set { _showOpenButton = value; ToggleOpenButton(value); } }

    /// <summary>Gets or sets a value indicating whether [show save button].</summary>
    /// <value><c>true</c> if [show save button]; otherwise, <c>false</c>.</value>
    [Category(@"Visuals"), DefaultValue(false), Description(@"Show or hide the 'Save' button.")]
    public bool ShowSaveButton { get => _showSaveButton; set { _showSaveButton = value; ToggleSaveButton(value); } }

    /// <summary>Gets or sets a value indicating whether [show save all button].</summary>
    /// <value><c>true</c> if [show save all button]; otherwise, <c>false</c>.</value>
    [Category(@"Visuals"), DefaultValue(false), Description(@"Show or hide the 'Save All' button.")]
    public bool ShowSaveAllButton { get => _showSaveAllButton; set { _showSaveAllButton = value; ToggleSaveAllButton(value); } }

    /// <summary>Gets or sets a value indicating whether [show save as button].</summary>
    /// <value><c>true</c> if [show save as button]; otherwise, <c>false</c>.</value>
    [Category(@"Visuals"), DefaultValue(false), Description(@"Show or hide the 'Save As' button.")]
    public bool ShowSaveAsButton { get => _showSaveAsButton; set { _showSaveAsButton = value; ToggleSaveAsButton(value); } }

    /// <summary>Gets or sets a value indicating whether [show cut button].</summary>
    /// <value><c>true</c> if [show cut button]; otherwise, <c>false</c>.</value>
    [Category(@"Visuals"), DefaultValue(false), Description(@"Show or hide the 'Cut' button.")]
    public bool ShowCutButton { get => _showCutButton; set { _showCutButton = value; ToggleCutButton(value); } }

    /// <summary>Gets or sets a value indicating whether [show copy button].</summary>
    /// <value><c>true</c> if [show copy button]; otherwise, <c>false</c>.</value>
    [Category(@"Visuals"), DefaultValue(false), Description(@"Show or hide the 'Copy' button.")]
    public bool ShowCopyButton { get => _showCopyButton; set { _showCopyButton = value; ToggleCopyButton(value); } }

    /// <summary>Gets or sets a value indicating whether [show paste button].</summary>
    /// <value><c>true</c> if [show paste button]; otherwise, <c>false</c>.</value>
    [Category(@"Visuals"), DefaultValue(false), Description(@"Show or hide the 'Paste' button.")]
    public bool ShowPasteButton { get => _showPasteButton; set { _showPasteButton = value; TogglePasteButton(value); } }

    /// <summary>Gets or sets a value indicating whether [show undo button].</summary>
    /// <value><c>true</c> if [show undo button]; otherwise, <c>false</c>.</value>
    [Category(@"Visuals"), DefaultValue(false), Description(@"Show or hide the 'Undo' button.")]
    public bool ShowUndoButton { get => _showUndoButton; set { _showUndoButton = value; ToggleUndoButton(value); } }

    /// <summary>Gets or sets a value indicating whether [show redo button].</summary>
    /// <value><c>true</c> if [show redo button]; otherwise, <c>false</c>.</value>
    [Category(@"Visuals"), DefaultValue(false), Description(@"Show or hide the 'Redo' button.")]
    public bool ShowRedoButton { get => _showRedoButton; set { _showRedoButton = value; ToggleRedoButton(value); } }

    /// <summary>Gets or sets a value indicating whether [show page setup button].</summary>
    /// <value><c>true</c> if [show page setup button]; otherwise, <c>false</c>.</value>
    [Category(@"Visuals"), DefaultValue(false), Description(@"Show or hide the 'Page Setup' button.")]
    public bool ShowPageSetupButton { get => _showPageSetupButton; set { _showPageSetupButton = value; TogglePageSetupButton(value); } }

    /// <summary>Gets or sets a value indicating whether [show print preview button].</summary>
    /// <value><c>true</c> if [show print preview button]; otherwise, <c>false</c>.</value>
    [Category(@"Visuals"), DefaultValue(false), Description(@"Show or hide the 'Print Preview' button.")]
    public bool ShowPrintPreviewButton { get => _showPrintPreviewButton; set { _showPrintPreviewButton = value; TogglePrintPreviewButton(value); } }

    /// <summary>Gets or sets a value indicating whether [show print button].</summary>
    /// <value><c>true</c> if [show print button]; otherwise, <c>false</c>.</value>
    [Category(@"Visuals"), DefaultValue(false), Description(@"Show or hide the 'Print' button.")]
    public bool ShowPrintButton { get => _showPrintButton; set { _showPrintButton = value; TogglePrintButton(value); } }

    /// <summary>Gets or sets a value indicating whether [show quick print button].</summary>
    /// <value><c>true</c> if [show quick print button]; otherwise, <c>false</c>.</value>
    [Category(@"Visuals"), DefaultValue(false), Description(@"Show or hide the 'Quick Print' button.")]
    public bool ShowQuickPrintButton { get => _showQuickPrintButton; set { _showQuickPrintButton = value; ToggleQuickPrintButton(value); } }

    #endregion

    #endregion

    #region Static Properties

    public static IntegratedToolBarButtonValues IntegratedToolBarButtonValues { get; } = new IntegratedToolBarButtonValues();

    public static IntegratedToolBarCommandValues IntegratedToolBarCommandValues { get; } = new IntegratedToolBarCommandValues();

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonIntegratedToolBarManager" /> class.</summary>
    public KryptonIntegratedToolBarManager()
    {
        Reset();
    }

    #endregion

    #region Implementation

    /*/// <summary>Gets a value indicating whether this instance is default.</summary>
    /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsDefault => !(ShouldSerializeToolBarCommands() || ShouldSerializeToolBarButtonValues());*/

    /// <summary>Resets this instance.</summary>
    public void Reset()
    {
        _flipButtonArray = false;

        SetupToolBar();

        _integratedToolBarButtonOrientation = PaletteButtonOrientation.FixedTop;

        _integratedToolBarButtonAlignment = PaletteRelativeEdgeAlign.Far;

        _parentForm = null;

        //ResetToolBarButtonValues();

        //ResetToolBarCommands();
    }

    /// <summary>Setups the tool bar.</summary>
    private void SetupToolBar()
    {
        _integratedToolBarButtons = new ButtonSpecAny[MAXIMUM_INTEGRATED_TOOLBAR_BUTTONS];

        ButtonSpecAny newToolbarButton = new(),
            openToolbarButton = new(),
            saveToolbarButton = new(),
            saveAsToolbarButton = new(),
            saveAllToolbarButton = new(),
            cutToolbarButton = new(),
            copyToolbarButton = new(),
            pasteToolbarButton = new(),
            undoToolbarButton = new(),
            redoToolbarButton = new(),
            pageSetupToolbarButton = new(),
            printPreviewToolbarButton = new(),
            printToolbarButton = new(),
            quickPrintToolbarButton = new();

        newToolbarButton.Type = PaletteButtonSpecStyle.New;

        newToolbarButton.Visible = true;

        openToolbarButton.Visible = true;

        openToolbarButton.Type = PaletteButtonSpecStyle.Open;

        saveAllToolbarButton.Visible = true;

        saveAllToolbarButton.Type = PaletteButtonSpecStyle.SaveAll;

        saveAsToolbarButton.Visible = true;

        saveAsToolbarButton.Type = PaletteButtonSpecStyle.SaveAs;

        saveToolbarButton.Visible = true;

        saveToolbarButton.Type = PaletteButtonSpecStyle.Save;

        cutToolbarButton.Visible = true;

        cutToolbarButton.Type = PaletteButtonSpecStyle.Cut;

        copyToolbarButton.Visible = true;

        copyToolbarButton.Type = PaletteButtonSpecStyle.Copy;

        pasteToolbarButton.Visible = true;

        pasteToolbarButton.Type = PaletteButtonSpecStyle.Paste;

        undoToolbarButton.Visible = true;

        undoToolbarButton.Type = PaletteButtonSpecStyle.Undo;

        redoToolbarButton.Visible = true;

        redoToolbarButton.Type = PaletteButtonSpecStyle.Redo;

        pageSetupToolbarButton.Visible = true;

        pageSetupToolbarButton.Type = PaletteButtonSpecStyle.PageSetup;

        printPreviewToolbarButton.Visible = true;

        printPreviewToolbarButton.Type = PaletteButtonSpecStyle.PrintPreview;

        printToolbarButton.Visible = true;

        printToolbarButton.Type = PaletteButtonSpecStyle.Print;

        quickPrintToolbarButton.Visible = true;

        quickPrintToolbarButton.Type = PaletteButtonSpecStyle.QuickPrint;

        quickPrintToolbarButton.Visible = true;

        _integratedToolBarButtons[0] = newToolbarButton;

        _integratedToolBarButtons[1] = openToolbarButton;

        _integratedToolBarButtons[2] = saveToolbarButton;

        _integratedToolBarButtons[3] = saveAsToolbarButton;

        _integratedToolBarButtons[4] = saveAllToolbarButton;

        _integratedToolBarButtons[5] = cutToolbarButton;

        _integratedToolBarButtons[6] = copyToolbarButton;

        _integratedToolBarButtons[7] = pasteToolbarButton;

        _integratedToolBarButtons[8] = undoToolbarButton;

        _integratedToolBarButtons[9] = redoToolbarButton;

        _integratedToolBarButtons[10] = pageSetupToolbarButton;

        _integratedToolBarButtons[11] = printPreviewToolbarButton;

        _integratedToolBarButtons[12] = printToolbarButton;

        _integratedToolBarButtons[13] = quickPrintToolbarButton;
    }

    /// <summary>Shows the tool bar into parent form.</summary>
    /// <param name="showIntegratedToolBar">if set to <c>true</c> [show integrated tool bar].</param>
    /// <param name="parentForm">The parent form.</param>
    public void ShowIntegrateToolBar(bool showIntegratedToolBar, KryptonForm parentForm)
    {
        try
        {
            if (showIntegratedToolBar)
            {
                foreach (ButtonSpecAny button in ReturnIntegratedToolBarButtonArray())
                {
                    parentForm.ButtonSpecs.Add(button);
                }
            }
            else
            {
                foreach (ButtonSpecAny button in ReturnIntegratedToolBarButtonArray())
                {
                    parentForm.ButtonSpecs.Remove(button);
                }
            }
        }
        catch (Exception e)
        {
            KryptonExceptionHandler.CaptureException(e, showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);
        }
    }

    /// <summary>Attaches the integrated tool bar to parent.</summary>
    /// <param name="parentForm">The parent form.</param>
    public void AttachIntegratedToolBarToParent(KryptonForm? parentForm)
    {
        try
        {
            if (parentForm != null)
            {
                foreach (ButtonSpecAny button in ReturnIntegratedToolBarButtonArray())
                {
                    parentForm.ButtonSpecs.Add(button);
                }

                UpdateButtonVisibility(true);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
        catch (Exception e)
        {
            KryptonExceptionHandler.CaptureException(e, showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);
        }
    }

    /// <summary>Detaches the integrated tool bar from parent.</summary>
    /// <param name="parentForm">The parent form.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public void DetachIntegratedToolBarFromParent(KryptonForm? parentForm)
    {
        try
        {
            if (parentForm != null)
            {
                foreach (ButtonSpecAny button in ReturnIntegratedToolBarButtonArray())
                {
                    parentForm.ButtonSpecs.Remove(button);
                }

                UpdateButtonVisibility(false);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
        catch (Exception e)
        {
            KryptonExceptionHandler.CaptureException(e, showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);
        }
    }

    /// <summary>Updates the button orientation.</summary>
    /// <param name="buttonOrientation">The button orientation.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">buttonOrientation - null</exception>
    public void UpdateButtonOrientation(PaletteButtonOrientation buttonOrientation)
    {
        try
        {
            if (_parentForm != null)
            {
                switch (buttonOrientation)
                {
                    case PaletteButtonOrientation.Inherit:
                        foreach (ButtonSpecAny button in ReturnIntegratedToolBarButtonArray())
                        {
                            button.Orientation = PaletteButtonOrientation.Inherit;
                        }
                        break;
                    case PaletteButtonOrientation.Auto:
                        foreach (ButtonSpecAny button in ReturnIntegratedToolBarButtonArray())
                        {
                            button.Orientation = PaletteButtonOrientation.Auto;
                        }
                        break;
                    case PaletteButtonOrientation.FixedTop:
                        foreach (ButtonSpecAny button in ReturnIntegratedToolBarButtonArray())
                        {
                            button.Orientation = PaletteButtonOrientation.FixedTop;
                        }
                        break;
                    case PaletteButtonOrientation.FixedBottom:
                        foreach (ButtonSpecAny button in ReturnIntegratedToolBarButtonArray())
                        {
                            button.Orientation = PaletteButtonOrientation.FixedBottom;
                        }
                        break;
                    case PaletteButtonOrientation.FixedLeft:
                        foreach (ButtonSpecAny button in ReturnIntegratedToolBarButtonArray())
                        {
                            button.Orientation = PaletteButtonOrientation.FixedLeft;
                        }
                        break;
                    case PaletteButtonOrientation.FixedRight:
                        foreach (ButtonSpecAny button in ReturnIntegratedToolBarButtonArray())
                        {
                            button.Orientation = PaletteButtonOrientation.FixedRight;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(buttonOrientation), buttonOrientation, null);
                }
            }
        }
        catch (Exception e)
        {
            KryptonExceptionHandler.CaptureException(e, showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);
        }
    }

    /// <summary>Updates the button alignment.</summary>
    /// <param name="buttonAlignment">The button alignment.</param>
    /// <exception cref="ArgumentOutOfRangeException">buttonAlignment - null</exception>
    public void UpdateButtonAlignment(PaletteRelativeEdgeAlign buttonAlignment)
    {
        try
        {
            if (_parentForm != null)
            {
                switch (buttonAlignment)
                {
                    case PaletteRelativeEdgeAlign.Inherit:
                        foreach (ButtonSpecAny button in ReturnIntegratedToolBarButtonArray())
                        {
                            button.Edge = PaletteRelativeEdgeAlign.Inherit;
                        }
                        break;
                    case PaletteRelativeEdgeAlign.Near:
                        foreach (ButtonSpecAny button in ReturnIntegratedToolBarButtonArray())
                        {
                            button.Edge = PaletteRelativeEdgeAlign.Near;
                        }
                        break;
                    case PaletteRelativeEdgeAlign.Far:
                        foreach (ButtonSpecAny button in ReturnIntegratedToolBarButtonArray())
                        {
                            button.Edge = PaletteRelativeEdgeAlign.Far;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(buttonAlignment), buttonAlignment, null);
                }
            }
        }
        catch (Exception e)
        {
            KryptonExceptionHandler.CaptureException(e, showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);
        }
    }

    /// <summary>Updates the button visibility.</summary>
    /// <param name="buttonVisibility">if set to <c>true</c> [button visibility].</param>
    private void UpdateButtonVisibility(bool buttonVisibility)
    {
        ToggleNewButton(buttonVisibility);

        ToggleOpenButton(buttonVisibility);

        ToggleSaveButton(buttonVisibility);

        ToggleSaveAsButton(buttonVisibility);

        ToggleSaveAllButton(buttonVisibility);

        ToggleCutButton(buttonVisibility);

        ToggleCopyButton(buttonVisibility);

        TogglePasteButton(buttonVisibility);

        ToggleUndoButton(buttonVisibility);

        ToggleRedoButton(buttonVisibility);

        TogglePageSetupButton(buttonVisibility);

        TogglePrintPreviewButton(buttonVisibility);

        TogglePrintButton(buttonVisibility);

        ToggleQuickPrintButton(buttonVisibility);
    }

    /// <summary>Returns the integrated tool bar button array.</summary>
    /// <returns></returns>
    public ButtonSpecAny[] ReturnIntegratedToolBarButtonArray() => _integratedToolBarButtons;

    /// <summary>Returns the is button array flipped.</summary>
    /// <returns></returns>
    public bool ReturnIsButtonArrayFlipped() => _flipButtonArray;

    #region Tool Bar Buttons

    internal void ToggleNewButton(bool value)
    {
        if (!ReturnIsButtonArrayFlipped())
        {
            ReturnIntegratedToolBarButtonArray()[0].Visible = value;
        }
        else
        {
            ReturnIntegratedToolBarButtonArray()[13].Visible = value;
        }
    }

    internal void ToggleOpenButton(bool value)
    {
        if (!ReturnIsButtonArrayFlipped())
        {
            ReturnIntegratedToolBarButtonArray()[1].Visible = value;
        }
        else
        {
            ReturnIntegratedToolBarButtonArray()[12].Visible = value;
        }
    }

    internal void ToggleSaveButton(bool value)
    {
        if (!ReturnIsButtonArrayFlipped())
        {
            ReturnIntegratedToolBarButtonArray()[2].Visible = value;
        }
        else
        {
            ReturnIntegratedToolBarButtonArray()[11].Visible = value;
        }
    }

    internal void ToggleSaveAllButton(bool value)
    {
        if (!ReturnIsButtonArrayFlipped())
        {
            ReturnIntegratedToolBarButtonArray()[3].Visible = value;
        }
        else
        {
            ReturnIntegratedToolBarButtonArray()[10].Visible = value;
        }
    }

    internal void ToggleSaveAsButton(bool value)
    {
        if (!ReturnIsButtonArrayFlipped())
        {
            ReturnIntegratedToolBarButtonArray()[4].Visible = value;
        }
        else
        {
            ReturnIntegratedToolBarButtonArray()[9].Visible = value;
        }
    }

    internal void ToggleCutButton(bool value)
    {
        if (!ReturnIsButtonArrayFlipped())
        {
            ReturnIntegratedToolBarButtonArray()[5].Visible = value;
        }
        else
        {
            ReturnIntegratedToolBarButtonArray()[8].Visible = value;
        }
    }

    internal void ToggleCopyButton(bool value)
    {
        if (!ReturnIsButtonArrayFlipped())
        {
            ReturnIntegratedToolBarButtonArray()[6].Visible = value;
        }
        else
        {
            ReturnIntegratedToolBarButtonArray()[7].Visible = value;
        }
    }

    internal void TogglePasteButton(bool value)
    {
        if (!ReturnIsButtonArrayFlipped())
        {
            ReturnIntegratedToolBarButtonArray()[7].Visible = value;
        }
        else
        {
            ReturnIntegratedToolBarButtonArray()[6].Visible = value;
        }
    }

    internal void ToggleUndoButton(bool value)
    {
        if (!ReturnIsButtonArrayFlipped())
        {
            ReturnIntegratedToolBarButtonArray()[8].Visible = value;
        }
        else
        {
            ReturnIntegratedToolBarButtonArray()[5].Visible = value;
        }
    }

    internal void ToggleRedoButton(bool value)
    {
        if (!ReturnIsButtonArrayFlipped())
        {
            ReturnIntegratedToolBarButtonArray()[9].Visible = value;
        }
        else
        {
            ReturnIntegratedToolBarButtonArray()[4].Visible = value;
        }
    }

    internal void TogglePageSetupButton(bool value)
    {
        if (!ReturnIsButtonArrayFlipped())
        {
            ReturnIntegratedToolBarButtonArray()[10].Visible = value;
        }
        else
        {
            ReturnIntegratedToolBarButtonArray()[3].Visible = value;
        }
    }

    internal void TogglePrintPreviewButton(bool value)
    {
        if (!ReturnIsButtonArrayFlipped())
        {
            ReturnIntegratedToolBarButtonArray()[11].Visible = value;
        }
        else
        {
            ReturnIntegratedToolBarButtonArray()[2].Visible = value;
        }
    }

    internal void TogglePrintButton(bool value)
    {
        if (!ReturnIsButtonArrayFlipped())
        {
            ReturnIntegratedToolBarButtonArray()[12].Visible = value;
        }
        else
        {
            ReturnIntegratedToolBarButtonArray()[1].Visible = value;
        }
    }

    internal void ToggleQuickPrintButton(bool value)
    {
        if (!ReturnIsButtonArrayFlipped())
        {
            ReturnIntegratedToolBarButtonArray()[13].Visible = value;
        }
        else
        {
            ReturnIntegratedToolBarButtonArray()[0].Visible = value;
        }
    }

    #endregion

    #endregion

}