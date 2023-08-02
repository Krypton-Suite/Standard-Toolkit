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
    public class KryptonPaletteImagesGeneralButtonSpecs : Storage
    {
        #region Instance Fields

        private PaletteRedirect? _redirect;

        private Image? _arrowRight;

        private Image? _arrowLeft;

        private Image? _arrowUp;

        private Image? _arrowDown;

        private Image? _context;

        private Image? _close;

        private Image? _dropDown;

        private Image? _pinHorizontal;

        private Image? _pinVertical;

        private Image? _pendantClose;

        private Image? _pendantMin;

        private Image? _pendantRestore;

        private Image? _previous;

        private Image? _next;

        private Image? _ribbonExpand;

        private Image? _ribbonMinimize;

        private Image? _workspaceMaximize;

        private Image? _workspaceRestore;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonPaletteImagesGeneralButtonSpecs" /> class.</summary>
        /// <param name="redirect">The redirect.</param>
        /// <param name="needPaint">The need paint.</param>
        public KryptonPaletteImagesGeneralButtonSpecs(PaletteRedirect? redirect, NeedPaintHandler needPaint)
        {
            _redirect = redirect;

            NeedPaint = needPaint;

            _arrowDown = null;
            _arrowLeft = null;
            _arrowUp = null;
            _arrowRight = null;
            _close = null;
            _context = null;
            _dropDown = null;
            _pinHorizontal = null;
            _pinVertical = null;
            _pendantClose = null;
            _pendantMin = null;
            _pendantRestore = null;
            _previous = null;
            _next = null;
            _ribbonExpand = null;
            _ribbonMinimize = null;
            _workspaceMaximize = null;
            _workspaceRestore = null;
        }

        #endregion

        #region PopulateFromBase

        /// <summary>Populates from palette base.</summary>
        public void PopulateFromBase()
        {
            _arrowDown = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.ArrowDown, PaletteState.Normal);
            _arrowUp = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.ArrowUp, PaletteState.Normal);
            _arrowLeft = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.ArrowLeft, PaletteState.Normal);
            _arrowRight = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.ArrowRight, PaletteState.Normal);
            _close = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.Close, PaletteState.Normal);
            _context = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.Context, PaletteState.Normal);
            _dropDown = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.DropDown, PaletteState.Normal);
            _pinHorizontal = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.PinHorizontal, PaletteState.Normal);
            _pinVertical = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.PinVertical, PaletteState.Normal);
            _pendantClose = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.PendantClose, PaletteState.Normal);
            _pendantMin = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.PendantMin, PaletteState.Normal);
            _pendantRestore = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.PendantRestore, PaletteState.Normal);
            _previous = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.Previous, PaletteState.Normal);
            _next = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.Next, PaletteState.Normal);
            _ribbonExpand = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.RibbonExpand, PaletteState.Normal);
            _ribbonMinimize = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.RibbonMinimize, PaletteState.Normal);
            _workspaceMaximize = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.WorkspaceMaximize, PaletteState.Normal);
            _workspaceRestore = _redirect.GetButtonSpecImage(PaletteButtonSpecStyle.WorkspaceRestore, PaletteState.Normal);
        }

        #endregion

        #region IsDefault

        /// <summary>Gets a value indicating if all values are default.</summary>
        [Browsable(false)]
        public override bool IsDefault => (_arrowDown == null) &&
                                          (_arrowLeft == null) &&
                                          (_arrowRight == null) &&
                                          (_arrowUp == null) &&
                                          (_close == null) &&
                                          (_context == null) &&
                                          (_dropDown == null) &&
                                          (_pinHorizontal == null) &&
                                          (_pinVertical == null) &&
                                          (_pendantClose == null) &&
                                          (_pendantRestore == null) &&
                                          (_pendantMin == null) &&
                                          (_previous == null) &&
                                          (_next == null) &&
                                          (_ribbonExpand == null) &&
                                          (_ribbonMinimize == null) &&
                                          (_workspaceMaximize == null) &&
                                          (_workspaceRestore == null);

        #endregion
    }
}