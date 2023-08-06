#region BSD License
/*
 *
 *   BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    public class KryptonPaletteImagesIntegratedToolBar : Storage
    {
        #region Instance Fields

        private PaletteRedirect? _redirect;
        private Image? _copy;
        private Image? _cut;
        private Image? _help;
        private Image? _paste;
        private Image? _new;
        private Image? _open;
        private Image? _pageSetup;
        private Image? _printPreview;
        private Image? _print;
        private Image? _quickPrint;
        private Image? _redo;
        private Image? _undo;
        private Image? _saveAll;
        private Image? _saveAs;
        private Image? _save;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonPaletteImagesIntegratedToolBar" /> class.</summary>
        /// <param name="redirect">The redirect.</param>
        /// <param name="needPaint">The need paint.</param>
        public KryptonPaletteImagesIntegratedToolBar(PaletteRedirect? redirect, NeedPaintHandler needPaint)
        {
            _redirect = redirect;

            NeedPaint = needPaint;

            _copy = null;
            _cut = null;
            _help = null;
            _paste = null;
            _new = null;
            _open = null;
            _pageSetup = null;
            _printPreview = null;
            _redo = null;
            _undo = null;
            _saveAll = null;
            _saveAs = null;
            _save = null;
            _print = null;
            _quickPrint = null;
        }

        #endregion

        #region IsDefault

        public override bool IsDefault { get; }

        #endregion

        #region PopulateFromBase

        public void PopulateFromBase()
        {
            _copy = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.Copy, PaletteState.Normal);

            _cut = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.Cut, PaletteState.Normal);
        }

        #endregion
    }
}