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
    [Category(@"code")]
    [ToolboxItem(false)]
    public class IntegratedToolBarValues : Storage
    {
        #region Instance Fields

        private bool _showIntegratedToolBar;

        private ButtonSpecAny[]? _integratedToolBarItems;

        #endregion

        #region Public

        [Category(@"Visuals")]
        [Description(@"")]
        [DefaultValue(false)]
        public bool ShowIntegratedToolBar { get => _showIntegratedToolBar; set { _showIntegratedToolBar = value; SetupIntegratedToolBar(value); } }

        public ButtonSpecAny[]? IntegratedToolBarItems { get => _integratedToolBarItems; set => _integratedToolBarItems = value; }

        #endregion

        #region Identity

        public IntegratedToolBarValues()
        {
            Reset();
        }

        #endregion

        #region Implementation

        public override bool IsDefault { get; }

        public void Reset()
        {

        }

        private void SetupIntegratedToolBar(bool showToolBar)
        {
            if (showToolBar)
            {
                _integratedToolBarItems = new ButtonSpecAny[14];

                SetupIntegratedToolBarButtons();
            }
            else
            {
                _integratedToolBarItems = null;
            }
        }

        private void SetupIntegratedToolBarButtons()
        {
            if (_integratedToolBarItems != null)
            {
                var newButtonSpec = new ButtonSpecAny();
                var openButtonSpecAny = new ButtonSpecAny();
                var saveButtonSpecAny = new ButtonSpecAny();
                var saveAsButtonSpecAny = new ButtonSpecAny();
                var saveAllButtonSpecAny = new ButtonSpecAny();
                var cutButtonSpecAny = new ButtonSpecAny();
                var copyButtonSpecAny = new ButtonSpecAny();
                var pasteButtonSpecAny = new ButtonSpecAny();
                var undoButtonSpecAny = new ButtonSpecAny();
                var redoButtonSpecAny = new ButtonSpecAny();
                var pageSetupButtonSpecAny = new ButtonSpecAny();
                var printPreviewButtonSpecAny = new ButtonSpecAny();
                var printButtonSpecAny = new ButtonSpecAny();
                var quickPrintButtonSpecAny = new ButtonSpecAny();

                // Set up buttons
                newButtonSpec.Type = PaletteButtonSpecStyle.New;
                openButtonSpecAny.Type = PaletteButtonSpecStyle.Open;
                saveAllButtonSpecAny.Type = PaletteButtonSpecStyle.SaveAll;
                saveAsButtonSpecAny.Type = PaletteButtonSpecStyle.SaveAs;
                saveButtonSpecAny.Type = PaletteButtonSpecStyle.Save;
                cutButtonSpecAny.Type = PaletteButtonSpecStyle.Cut;
                copyButtonSpecAny.Type = PaletteButtonSpecStyle.Copy;
                pasteButtonSpecAny.Type = PaletteButtonSpecStyle.Paste;
                undoButtonSpecAny.Type = PaletteButtonSpecStyle.Undo;
                redoButtonSpecAny.Type = PaletteButtonSpecStyle.Redo;
                pageSetupButtonSpecAny.Type = PaletteButtonSpecStyle.PageSetup;
                printPreviewButtonSpecAny.Type = PaletteButtonSpecStyle.PrintPreview;
                printButtonSpecAny.Type = PaletteButtonSpecStyle.Print;
                quickPrintButtonSpecAny.Type = PaletteButtonSpecStyle.QuickPrint;

                // Add configured buttons to array...
                // TODO: These should not be magic numbers (How about using the `PaletteButtonSpecStyle`?)
                _integratedToolBarItems[0] = newButtonSpec;
                _integratedToolBarItems[1] = openButtonSpecAny;
                _integratedToolBarItems[2] = saveButtonSpecAny;
                _integratedToolBarItems[3] = saveAsButtonSpecAny;
                _integratedToolBarItems[4] = saveAllButtonSpecAny;
                _integratedToolBarItems[5] = cutButtonSpecAny;
                _integratedToolBarItems[6] = copyButtonSpecAny;
                _integratedToolBarItems[7] = pasteButtonSpecAny;
                _integratedToolBarItems[8] = undoButtonSpecAny;
                _integratedToolBarItems[9] = redoButtonSpecAny;
                _integratedToolBarItems[10] = pageSetupButtonSpecAny;
                _integratedToolBarItems[11] = printPreviewButtonSpecAny;
                _integratedToolBarItems[12] = printPreviewButtonSpecAny;
                _integratedToolBarItems[13] = quickPrintButtonSpecAny;
            }
        }

        #endregion
    }
}