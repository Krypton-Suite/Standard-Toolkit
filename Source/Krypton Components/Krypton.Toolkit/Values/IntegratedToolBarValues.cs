using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton.Toolkit
{
    public class IntegratedToolBarValues : Storage
    {
        #region Instance Fields

        private bool _showIntegratedToolbar;

        private ButtonSpecAny[]? _integratedToolbarButtonCollection;

        private KryptonForm? _owner;

        #endregion

        #region Identity

        public IntegratedToolBarValues(KryptonForm owner)
        {
            _showIntegratedToolbar = false;

            _integratedToolbarButtonCollection = null;

            _owner = owner;
        }

        #endregion

        #region Storage Implementation

        public override bool IsDefault => (_showIntegratedToolbar == false) &&
                                              (_integratedToolbarButtonCollection != null) &&
                                              (_owner == null);

        #endregion

        #region Public

        /// <summary>Gets or sets buttonspecs that make up the integrated tool bar.</summary>
        [Category(@"Visuals")]
        [Description(@"Shows a set of toolbar buttons in the title bar. (Caution: This is quite buggy!)")]
        [DefaultValue(false)]
        public bool ShowIntegratedToolBar { get => _showIntegratedToolbar; set { _showIntegratedToolbar = value; SetupIntegratedToolBar(value, _owner); } }

        /// <summary>Gets the integrated tool bar button collection.</summary>
        /// <value>The integrated tool bar button collection.</value>
        [Category(@"Visuals")]
        [DefaultValue(null)]
        [Description(@"Gets access to the integrated toolbar items.")]
        [AllowNull]
        public ButtonSpecAny[]? IntegratedToolBarButtonCollection { get => _integratedToolbarButtonCollection; private set => _integratedToolbarButtonCollection = value; }

        public KryptonForm? Owner { get => _owner ?? null; set => _owner = value; }

        #endregion

        #region Implementation

        private void SetupIntegratedToolBar(bool showIntegratedToolbar, KryptonForm? owner)
        {
            #region Button Setup

            ButtonSpecAny bsaNew,
                bsaOpen,
                bsaSave,
                bsaSaveAs,
                bsaSaveAll,
                bsaCut,
                bsaCopy,
                bsaPaste,
                bsaPageSetup,
                bsaPrintPreview,
                bsaPrint,
                bsaQuickPrint;

            bsaNew = new();

            bsaOpen = new();

            bsaSave = new();

            bsaSaveAs = new();

            bsaSaveAll = new();

            bsaCut = new();

            bsaCopy = new();

            bsaPaste = new();

            bsaPageSetup = new();

            bsaPrintPreview = new();

            bsaPrint = new();

            bsaQuickPrint = new();

            bsaNew.Type = PaletteButtonSpecStyle.New;

            bsaOpen.Type = PaletteButtonSpecStyle.Open;

            bsaSave.Type = PaletteButtonSpecStyle.Save;

            bsaSaveAll.Type = PaletteButtonSpecStyle.SaveAll;

            bsaSaveAs.Type = PaletteButtonSpecStyle.SaveAs;

            bsaCut.Type = PaletteButtonSpecStyle.Cut;

            bsaCopy.Type = PaletteButtonSpecStyle.Copy;

            bsaPaste.Type = PaletteButtonSpecStyle.Paste;

            bsaPageSetup.Type = PaletteButtonSpecStyle.PageSetup;

            bsaPrintPreview.Type = PaletteButtonSpecStyle.PrintPreview;

            bsaPrint.Type = PaletteButtonSpecStyle.Print;

            bsaQuickPrint.Type = PaletteButtonSpecStyle.QuickPrint;

            ButtonSpecAny[]? toolbarButtons = {
                bsaNew, bsaOpen, bsaSave, bsaSaveAs, bsaSaveAll, bsaCut, bsaCopy, bsaPaste, bsaPageSetup,
                bsaPrintPreview, bsaPrint, bsaQuickPrint
            };

            #endregion

            // Check to see if we have a owner
            if (owner != null)
            {
                // If yes, then do the following...
                if (showIntegratedToolbar)
                {
                    owner.ButtonSpecs.Add(toolbarButtons);

                    _integratedToolbarButtonCollection = toolbarButtons;

                    // But do the following 'if' all buttons are hidden
                    if (_integratedToolbarButtonCollection != null && _integratedToolbarButtonCollection.Length > 0)
                    {
                        ChangeToolBarButtonVisibility(true);
                    }
                }
                else
                {
                    if (_integratedToolbarButtonCollection != null && _integratedToolbarButtonCollection.Length > 0)
                    {
                        ChangeToolBarButtonVisibility(false);
                    }
                }
            }
        }

        private void ChangeToolBarButtonVisibility(bool visible)
        {
            // Note: Change the following to utilise Span<T> at some point, but this will work for now :)
            if (_integratedToolbarButtonCollection != null)
            {
                foreach (ButtonSpecAny buttons in _integratedToolbarButtonCollection)
                {
                    buttons.Visible = true;
                }

            }
        }

        private void SetIntegratedToolbarButtonCollection(ButtonSpecAny[] buttons) => _integratedToolbarButtonCollection = buttons;

        private ButtonSpecAny[] GetIntegratedToolbarButtonCollection() => _integratedToolbarButtonCollection;

        #endregion
    }
}
