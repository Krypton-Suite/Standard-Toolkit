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
    internal class KryptonIntegratedToolBarManager
    {
        #region Static Fields

        private const int MAXIMUM_INTEGRATED_TOOLBAR_BUTTONS = 14;



        #endregion

        #region Instance Fields

        private bool _flipButtonArray;

        internal KryptonForm _parentForm;

        private IntegratedToolBarValues _integratedToolBarValues;

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

        #region Static Properties

        public static IntegratedToolBarValues IntegratedToolBarButtonValues { get; } = new IntegratedToolBarValues();

        public static IntegratedToolBarCommandValues IntegratedToolBarCommandValues { get; } = new IntegratedToolBarCommandValues();

        #endregion

        #region Identity

        public KryptonIntegratedToolBarManager(KryptonForm parentForm, IntegratedToolBarValues toolBarButtonValues)
        {
            _parentForm = parentForm;

            _integratedToolBarValues = toolBarButtonValues;

            Reset();
        }

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

            _parentForm = null;

            //ResetToolBarButtonValues();

            //ResetToolBarCommands();
        }

        /// <summary>Setups the tool bar.</summary>
        internal void SetupToolBar()
        {
            _integratedToolBarValues._integratedToolBarButtons = new ButtonSpecAny[MAXIMUM_INTEGRATED_TOOLBAR_BUTTONS];

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

            _integratedToolBarValues._integratedToolBarButtons[0] = newToolbarButton;

            _integratedToolBarValues._integratedToolBarButtons[1] = openToolbarButton;

            _integratedToolBarValues._integratedToolBarButtons[2] = saveToolbarButton;

            _integratedToolBarValues._integratedToolBarButtons[3] = saveAsToolbarButton;

            _integratedToolBarValues._integratedToolBarButtons[4] = saveAllToolbarButton;

            _integratedToolBarValues._integratedToolBarButtons[5] = cutToolbarButton;

            _integratedToolBarValues._integratedToolBarButtons[6] = copyToolbarButton;

            _integratedToolBarValues._integratedToolBarButtons[7] = pasteToolbarButton;

            _integratedToolBarValues._integratedToolBarButtons[8] = undoToolbarButton;

            _integratedToolBarValues._integratedToolBarButtons[9] = redoToolbarButton;

            _integratedToolBarValues._integratedToolBarButtons[10] = pageSetupToolbarButton;

            _integratedToolBarValues._integratedToolBarButtons[11] = printPreviewToolbarButton;

            _integratedToolBarValues._integratedToolBarButtons[12] = printToolbarButton;

            _integratedToolBarValues._integratedToolBarButtons[13] = quickPrintToolbarButton;
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
                ExceptionHandler.CaptureException(e, className: @"KryptonIntegratedToolBarManager.cs", methodSignature: @"IntegrateToolBarIntoParentForm(bool showIntegratedToolBar, KryptonForm parentForm)");
            }
        }

        /// <summary>Attaches the integrated tool bar to parent.</summary>
        /// <param name="parentForm">The parent form.</param>
        internal void AttachIntegratedToolBarToParent(KryptonForm? parentForm)
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
                ExceptionHandler.CaptureException(e, className: @"KryptonIntegratedToolBarManager.cs", methodSignature: @"AttachIntegratedToolBarToParent(KryptonForm parentForm)");
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
                ExceptionHandler.CaptureException(e, className: @"KryptonIntegratedToolBarManager.cs", methodSignature: @"AttachIntegratedToolBarToParent(KryptonForm parentForm)");
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
                ExceptionHandler.CaptureException(e);
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
                ExceptionHandler.CaptureException(e);
            }
        }

        /// <summary>Updates the button visibility.</summary>
        /// <param name="buttonVisibility">if set to <c>true</c> [button visibility].</param>
        internal void UpdateButtonVisibility(bool buttonVisibility)
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
        public ButtonSpecAny[] ReturnIntegratedToolBarButtonArray() => _integratedToolBarValues._integratedToolBarButtons;

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
}