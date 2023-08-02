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

        #region ArrowRight

        /// <summary>Gets and sets the arrow right image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"ArrowRight image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? ArrowRight
        {
            get => _arrowRight;

            set
            {
                if (_arrowRight != null)
                {
                    _arrowRight = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the arrow right image to its default value.</summary>
        public void ResetArrowRight() => ArrowRight = null;

        #endregion

        #region ArrowLeft

        /// <summary>Gets and sets the arrow left image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"ArrowLeft image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? ArrowLeft
        {
            get => _arrowLeft;

            set
            {
                if (_arrowLeft != null)
                {
                    _arrowLeft = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the arrow left image to its default value.</summary>
        public void ResetArrowLeft() => ArrowLeft = null;

        #endregion

        #region ArrowUp

        /// <summary>Gets and sets the arrow up image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"ArrowUp image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? ArrowUp
        {
            get => _arrowUp;

            set
            {
                if (_arrowUp != null)
                {
                    _arrowUp = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the arrow up image to its default value.</summary>
        public void ResetArrowUp() => ArrowUp = null;

        #endregion

        #region ArrowDown

        /// <summary>Gets and sets the arrow down image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"ArrowDown image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? ArrowDown
        {
            get => _arrowDown;

            set
            {
                if (_arrowDown != null)
                {
                    _arrowDown = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the arrow down image to its default value.</summary>
        public void ResetArrowDown() => ArrowDown = null;

        #endregion

        #region Context

        /// <summary>Gets and sets the context image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Context image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? Context
        {
            get => _context;

            set
            {
                if (_context != null)
                {
                    _context = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the context image to its default value.</summary>
        public void ResetContext() => Context = null;

        #endregion

        #region Close

        /// <summary>Gets and sets the close image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Close image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? Close
        {
            get => _close;

            set
            {
                if (_close != null)
                {
                    _close = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the close image to its default value.</summary>
        public void ResetClose() => Close = null;

        #endregion

        #region DropDown

        /// <summary>Gets and sets the drop down image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"DropDown image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? DropDown
        {
            get => _dropDown;

            set
            {
                if (_dropDown != null)
                {
                    _dropDown = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the drop down image to its default value.</summary>
        public void ResetDropDown() => DropDown = null;

        #endregion

        #region PinHorizontal

        /// <summary>Gets and sets the pin horizontal image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"PinHorizontal image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? PinHorizontal
        {
            get => _pinHorizontal;

            set
            {
                if (_pinHorizontal != null)
                {
                    _pinHorizontal = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the pin horizontal image to its default value.</summary>
        public void ResetPinHorizontal() => PinHorizontal = null;

        #endregion

        #region PinVertical

        /// <summary>Gets and sets the pin vertical image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"PinVertical image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? PinVertical
        {
            get => _pinVertical;

            set
            {
                if (_pinVertical != null)
                {
                    _pinVertical = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the pin vertical image to its default value.</summary>
        public void ResetPinVertical() => PinVertical = null;

        #endregion

        #region PendantClose

        /// <summary>Gets and sets the pendant close image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"PendantClose image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? PendantClose
        {
            get => _pendantClose;

            set
            {
                if (_pendantClose != null)
                {
                    _pendantClose = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the pendant close image to its default value.</summary>
        public void ResetPendantClose() => PendantClose = null;

        #endregion

        #region PendantMin

        /// <summary>Gets and sets the pendant min image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"PendantMin image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? PendantMin
        {
            get => _pendantMin;

            set
            {
                if (_pendantMin != null)
                {
                    _pendantMin = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the pendant min image to its default value.</summary>
        public void ResetPendantMin() => PendantMin = null;

        #endregion

        #region PendantRestore

        /// <summary>Gets and sets the pendant restore image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"PendantRestore image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? PendantRestore
        {
            get => _pendantRestore;

            set
            {
                if (_pendantRestore != null)
                {
                    _pendantRestore = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the pendant restore image to its default value.</summary>
        public void ResetPendantRestore() => PendantRestore = null;

        #endregion

        #region Previous

        /// <summary>Gets and sets the previous image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Previous image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? Previous
        {
            get => _previous;

            set
            {
                if (_previous != null)
                {
                    _previous = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the previous image to its default value.</summary>
        public void ResetPrevious() => Previous = null;

        #endregion

        #region Next

        /// <summary>Gets and sets the next image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Next image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? Next
        {
            get => _next;

            set
            {
                if (_next != null)
                {
                    _next = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the next image to its default value.</summary>
        public void ResetNext() => Next = null;

        #endregion

        #region RibbonExpand

        /// <summary>Gets and sets the ribbon expand image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"RibbonExpand image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? RibbonExpand
        {
            get => _ribbonExpand;

            set
            {
                if (_ribbonExpand != null)
                {
                    _ribbonExpand = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the ribbon expand image to its default value.</summary>
        public void ResetRibbonExpand() => RibbonExpand = null;

        #endregion

        #region RibbonMinimize

        /// <summary>Gets and sets the ribbon minimize image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"RibbonMinimize image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? RibbonMinimize
        {
            get => _ribbonMinimize;

            set
            {
                if (_ribbonMinimize != null)
                {
                    _ribbonMinimize = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the ribbon minimize image to its default value.</summary>
        public void ResetRibbonMinimize() => RibbonMinimize = null;

        #endregion

        #region WorkspaceMaximize

        /// <summary>Gets and sets the workspace maximize image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"WorkspaceMaximize image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? WorkspaceMaximize
        {
            get => _workspaceMaximize;

            set
            {
                if (_workspaceMaximize != null)
                {
                    _workspaceMaximize = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the workspace maximize image to its default value.</summary>
        public void ResetWorkspaceMaximize() => WorkspaceMaximize = null;

        #endregion

        #region WorkspaceRestore

        /// <summary>Gets and sets the workspace restore image that the integrated toolbar images inherit from.</summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"WorkspaceRestore image that the integrated toolbar images inherit from.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Image? WorkspaceRestore
        {
            get => _workspaceRestore;

            set
            {
                if (_workspaceRestore != null)
                {
                    _workspaceRestore = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>Resets the workspace restore image to its default value.</summary>
        public void ResetWorkspaceRestore() => WorkspaceRestore = null;

        #endregion
    }
}