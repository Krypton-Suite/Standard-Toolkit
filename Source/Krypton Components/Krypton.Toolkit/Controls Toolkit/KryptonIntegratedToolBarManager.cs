#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
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

        private bool _showIntegratedToolBar;

        private ButtonSpecAny[] _integratedToolBarButtons;

        private PaletteButtonOrientation _integratedToolBarButtonOrientation;

        private PaletteRelativeEdgeAlign _integratedToolBarButtonAlignment;

        private KryptonForm? _parentForm;

        private IntegratedToolBarCommandValues _toolBarCommandValues;

        private IntegratedToolBarButtonValues _toolBarButtonValues;

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

        public bool ShowIntegratedToolBar
        {
            get => _showIntegratedToolBar;

            set
            {
                _showIntegratedToolBar = value;

                if (_parentForm != null)
                {
                    IntegrateToolBarIntoParentForm(value, _parentForm);
                }
            }
        }

        public ButtonSpecAny[] IntegratedToolBarButtons => _integratedToolBarButtons;

        public PaletteButtonOrientation IntegratedToolBarButtonOrientation { get => _integratedToolBarButtonOrientation; set { _integratedToolBarButtonOrientation = value; UpdateButtonOrientation(value); } }

        public PaletteRelativeEdgeAlign IntegratedToolBarButtonAlignment { get => _integratedToolBarButtonAlignment; set { _integratedToolBarButtonAlignment = value; UpdateButtonAlignment(value); } }

        public KryptonForm? ParentForm
        {
            get => _parentForm;

            set
            {
                _parentForm = value;

                if (value != null)
                {
                    //IntegrateToolBarIntoParentForm(_showIntegratedToolBar, value);
                }
            }
        }

        #region Tool Bar Buttons

        public bool ShowNewButton { get => _showNewButton; set { _showNewButton = value; ToggleNewButton(value); } }

        public bool ShowOpenButton { get => _showOpenButton; set { _showOpenButton = value; ToggleOpenButton(value); } }

        public bool ShowSaveButton { get => _showSaveButton; set { _showSaveButton = value; ToggleSaveButton(value); } }

        public bool ShowSaveAllButton { get => _showSaveAllButton; set { _showSaveAllButton = value; ToggleSaveAllButton(value); } }

        public bool ShowSaveAsButton { get => _showSaveAsButton; set { _showSaveAsButton = value; ToggleSaveAsButton(value); } }

        public bool ShowCutButton { get => _showCutButton; set { _showCutButton = value; ToggleCutButton(value); } }

        public bool ShowCopyButton { get => _showCopyButton; set { _showCopyButton = value; ToggleCopyButton(value); } }

        public bool ShowPasteButton { get => _showPasteButton; set { _showPasteButton = value; TogglePasteButton(value); } }

        public bool ShowUndoButton { get => _showUndoButton; set { _showUndoButton = value; ToggleUndoButton(value); } }

        public bool ShowRedoButton { get => _showRedoButton; set { _showRedoButton = value; ToggleRedoButton(value); } }

        public bool ShowPageSetupButton { get => _showPageSetupButton; set { _showPageSetupButton = value; TogglePageSetupButton(value); } }

        public bool ShowPrintPreviewButton { get => _showPrintPreviewButton; set { _showPrintPreviewButton = value; TogglePrintPreviewButton(value); } }

        public bool ShowPrintButton { get => _showPrintButton; set { _showPrintButton = value; TogglePrintButton(value); } }

        public bool ShowQuickPrintButton { get => _showQuickPrintButton; set { _showQuickPrintButton = value; ToggleQuickPrintButton(value); } }

        #endregion

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

            _showIntegratedToolBar = false;

            SetupToolBar();

            _integratedToolBarButtonOrientation = PaletteButtonOrientation.Auto;

            _integratedToolBarButtonAlignment = PaletteRelativeEdgeAlign.Near;

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

            openToolbarButton.Type = PaletteButtonSpecStyle.Open;

            saveAllToolbarButton.Type = PaletteButtonSpecStyle.SaveAll;

            saveAsToolbarButton.Type = PaletteButtonSpecStyle.SaveAs;

            saveToolbarButton.Type = PaletteButtonSpecStyle.Save;

            cutToolbarButton.Type = PaletteButtonSpecStyle.Cut;

            copyToolbarButton.Type = PaletteButtonSpecStyle.Copy;

            pasteToolbarButton.Type = PaletteButtonSpecStyle.Paste;

            undoToolbarButton.Type = PaletteButtonSpecStyle.Undo;

            redoToolbarButton.Type = PaletteButtonSpecStyle.Redo;

            pageSetupToolbarButton.Type = PaletteButtonSpecStyle.PageSetup;

            printPreviewToolbarButton.Type = PaletteButtonSpecStyle.PrintPreview;

            printToolbarButton.Type = PaletteButtonSpecStyle.Print;

            quickPrintToolbarButton.Type = PaletteButtonSpecStyle.QuickPrint;

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

        /// <summary>Integrates the tool bar into parent form.</summary>
        /// <param name="showIntegratedToolBar">if set to <c>true</c> [show integrated tool bar].</param>
        /// <param name="parentForm">The parent form.</param>
        public void IntegrateToolBarIntoParentForm(bool showIntegratedToolBar, KryptonForm parentForm)
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
                ExceptionHandler.CaptureException(e);
            }
        }

        /// <summary>Updates the button orientation.</summary>
        /// <param name="buttonOrientation">The button orientation.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">buttonOrientation - null</exception>
        public void UpdateButtonOrientation(PaletteButtonOrientation buttonOrientation)
        {
            switch (buttonOrientation)
            {
                case PaletteButtonOrientation.Inherit:
                    break;
                case PaletteButtonOrientation.Auto:
                    break;
                case PaletteButtonOrientation.FixedTop:
                    break;
                case PaletteButtonOrientation.FixedBottom:
                    break;
                case PaletteButtonOrientation.FixedLeft:
                    break;
                case PaletteButtonOrientation.FixedRight:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(buttonOrientation), buttonOrientation, null);
            }
        }

        /// <summary>Updates the button alignment.</summary>
        /// <param name="buttonAlignment">The button alignment.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">buttonAlignment - null</exception>
        public void UpdateButtonAlignment(PaletteRelativeEdgeAlign buttonAlignment)
        {
            switch (buttonAlignment)
            {
                case PaletteRelativeEdgeAlign.Inherit:
                    break;
                case PaletteRelativeEdgeAlign.Near:
                    break;
                case PaletteRelativeEdgeAlign.Far:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(buttonAlignment), buttonAlignment, null);
            }
        }

        /// <summary>Returns the integrated tool bar button array.</summary>
        /// <returns></returns>
        public ButtonSpecAny[] ReturnIntegratedToolBarButtonArray() => _integratedToolBarButtons;

        /// <summary>Returns the is button array flipped.</summary>
        /// <returns></returns>
        public bool ReturnIsButtonArrayFlipped() => _flipButtonArray;

        #region Tool Bar Buttons

        private void ToggleNewButton(bool value)
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

        private void ToggleOpenButton(bool value)
        {

        }

        private void ToggleSaveButton(bool value)
        {

        }

        private void ToggleSaveAllButton(bool value)
        {

        }

        private void ToggleSaveAsButton(bool value)
        {

        }

        private void ToggleCutButton(bool value)
        {

        }

        private void ToggleCopyButton(bool value)
        {

        }

        private void TogglePasteButton(bool value) { }

        private void ToggleUndoButton(bool value) { }

        private void ToggleRedoButton(bool value) { }

        private void TogglePageSetupButton(bool value) { }

        private void TogglePrintPreviewButton(bool value) { }

        private void TogglePrintButton(bool value) { }

        private void ToggleQuickPrintButton(bool value) { }

        #endregion

        #endregion

        #region Protected



        #endregion
    }
}