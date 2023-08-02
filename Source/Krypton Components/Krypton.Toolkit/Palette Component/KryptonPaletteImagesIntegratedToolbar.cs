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
    public class KryptonPaletteImagesIntegratedToolbar : Storage
    {
        #region Instance Fields

        private PaletteRedirect? _redirect;
        private Image? _new;
        private Image? _open;
        private Image? _save;
        private Image? _saveAs;
        private Image? _saveAll;
        private Image? _cut;
        private Image? _copy;
        private Image? _paste;
        private Image? _undo;
        private Image? _redo;
        private Image? _pageSetup;
        private Image? _printPreview;
        private Image? _print;
        private Image? _quickPrint;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonPaletteImagesIntegratedToolbar" /> class.</summary>
        /// <param name="redirect">The redirect.</param>
        /// <param name="needPaint">The need paint.</param>
        public KryptonPaletteImagesIntegratedToolbar(PaletteRedirect? redirect, NeedPaintHandler needPaint)
        {
            _redirect = redirect;

            NeedPaint = needPaint;

            _new = null;
            _open = null;
            _save = null;
            _saveAs = null;
            _saveAll = null;
            _cut = null;
            _copy = null;
            _paste = null;
            _undo = null;
            _redo = null;
            _pageSetup = null;
            _printPreview = null;
            _print = null;
            _quickPrint = null;
        }

        #endregion

        #region IsDefault

        /// <summary>Gets a value indicating if all values are default.</summary>
        [Browsable(false)]
        public override bool IsDefault => (_new == null) &&
                                          (_open == null) &&
                                          (_save == null) &&
                                          (_saveAs == null) &&
                                          (_saveAll == null) &&
                                          (_cut == null) &&
                                          (_copy == null) &&
                                          (_paste == null) &&
                                          (_undo == null) &&
                                          (_redo == null) &&
                                          (_pageSetup == null) &&
                                          (_printPreview == null) &&
                                          (_print == null) &&
                                          (_quickPrint == null);

        #endregion

        #region PopulateFromBase

        /// <summary>Populates from palette base.</summary>
        public void PopulateFromBase()
        {
            _new = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.New, PaletteState.Normal);

            _open = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.Open, PaletteState.Normal);

            _save = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.Save, PaletteState.Normal);

            _saveAs = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.SaveAs, PaletteState.Normal);

            _saveAll = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.SaveAll, PaletteState.Normal);

            _cut = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.Cut, PaletteState.Normal);

            _copy = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.Copy, PaletteState.Normal);

            _paste = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.Paste, PaletteState.Normal);

            _undo = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.Undo, PaletteState.Normal);

            _redo = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.Redo, PaletteState.Normal);

            _pageSetup = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.PageSetup, PaletteState.Normal);

            _printPreview = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.PrintPreview, PaletteState.Normal);

            _print = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.Print, PaletteState.Normal);

            _quickPrint = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.QuickPrint, PaletteState.Normal);
        }

        #endregion

        #region SetRedirector

        /// <summary>Sets the redirector.</summary>
        /// <param name="redirect">The redirect.</param>
        public void SetRedirector(PaletteRedirect? redirect) => _redirect = redirect;

        #endregion

        #region New

        /// <summary>Gets and sets the new image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"New image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? New
        {
            get => _new;

            set
            {
                if (_new != null)
                {
                    _new = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the new image to its default value.</summary>
        public void ResetNew() => New = null;

        #endregion

        #region Open

        /// <summary>Gets and sets the open image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Open image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? Open
        {
            get => _open;

            set
            {
                if (_open != null)
                {
                    _open = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the open image to its default value.</summary>
        public void ResetOpen() => Open = null;

        #endregion

        #region Save

        /// <summary>Gets and sets the save image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Save image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? Save
        {
            get => _save;

            set
            {
                if (_save != null)
                {
                    _save = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the save image to its default value.</summary>
        public void ResetSave() => Save = null;

        #endregion

        #region SaveAs

        /// <summary>Gets and sets the save as image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"SaveAs image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? SaveAs
        {
            get => _saveAs;

            set
            {
                if (_saveAs != null)
                {
                    _saveAs = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the save as image to its default value.</summary>
        public void ResetSaveAs() => SaveAs = null;

        #endregion

        #region SaveAll

        /// <summary>Gets and sets the save all image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"SaveAll image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? SaveAll
        {
            get => _saveAll;

            set
            {
                if (_saveAll != null)
                {
                    _saveAll = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the save all image to its default value.</summary>
        public void ResetSaveAll() => SaveAll = null;

        #endregion

        #region Cut

        /// <summary>Gets and sets the cut image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Cut image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? Cut
        {
            get => _cut;

            set
            {
                if (_cut != null)
                {
                    _cut = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the cut image to its default value.</summary>
        public void ResetCut() => Cut = null;

        #endregion

        #region Copy

        /// <summary>Gets and sets the copy image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Copy image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? Copy
        {
            get => _copy;

            set
            {
                if (_copy != null)
                {
                    _copy = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the copy image to its default value.</summary>
        public void ResetCopy() => Copy = null;

        #endregion

        #region Paste

        /// <summary>Gets and sets the paste image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Paste image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? Paste
        {
            get => _paste;

            set
            {
                if (_paste != null)
                {
                    _paste = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the paste image to its default value.</summary>
        public void ResetPaste() => Paste = null;

        #endregion

        #region Undo

        /// <summary>Gets and sets the undo image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Undo image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? Undo
        {
            get => _undo;

            set
            {
                if (_undo != null)
                {
                    _undo = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the undo image to its default value.</summary>
        public void ResetUndo() => Undo = null;

        #endregion

        #region Redo

        /// <summary>Gets and sets the redo image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Redo image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? Redo
        {
            get => _redo;

            set
            {
                if (_redo != null)
                {
                    _redo = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the redo image to its default value.</summary>
        public void ResetRedo() => Redo = null;

        #endregion

        #region PageSetup

        /// <summary>Gets and sets the page setup image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"PageSetup image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? PageSetup
        {
            get => _pageSetup;

            set
            {
                if (_pageSetup != null)
                {
                    _pageSetup = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the page setup image to its default value.</summary>
        public void ResetPageSetup() => PageSetup = null;

        #endregion

        #region PrintPreview

        /// <summary>Gets and sets the print preview image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"PrintPreview image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? PrintPreview
        {
            get => _printPreview;

            set
            {
                if (_printPreview != null)
                {
                    _printPreview = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the print preview image to its default value.</summary>
        public void ResetPrintPreview() => PrintPreview = null;

        #endregion

        #region Print

        /// <summary>Gets and sets the print image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Print image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? Print
        {
            get => _print;

            set
            {
                if (_print != null)
                {
                    _print = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the print image to its default value.</summary>
        public void ResetPrint() => Print = null;

        #endregion

        #region QuickPrint

        /// <summary>Gets and sets the quick print image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"QuickPrint image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? QuickPrint
        {
            get => _quickPrint;

            set
            {
                if (_quickPrint != null)
                {
                    _quickPrint = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the quick print image to its default value.</summary>
        public void ResetQuickPrint() => QuickPrint = null;

        #endregion

    }
}