﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon
{
    internal class ViewRibbonManager : ViewManager
    {
        #region Instance Fields
        private readonly KryptonRibbon _ribbon;
        private readonly ViewDrawRibbonGroupsBorderSynch _viewGroups;
        private ViewDrawRibbonGroup _activeGroup;
        private readonly NeedPaintHandler? _needPaintDelegate;
        private readonly bool _minimizedMode;
        private bool _active;
        private bool _layingOut;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewRibbonManager class.
        /// </summary>
        /// <param name="control">Owning control.</param>
        /// <param name="viewGroups">Group view elements.</param>
        /// <param name="root">Root of the view hierarchy.</param>
        /// <param name="minimizedMode">Is this manager for handling the minimized mode popup.</param>
        /// <param name="needPaintDelegate">Delegate for requesting paint changes.</param>
        public ViewRibbonManager(KryptonRibbon control,
                                 [DisallowNull] ViewDrawRibbonGroupsBorderSynch viewGroups,
                                 [DisallowNull] ViewBase root,
                                 bool minimizedMode,
                                 [DisallowNull] NeedPaintHandler needPaintDelegate)
            : base(control, root)
        {
            Debug.Assert(viewGroups != null);
            Debug.Assert(root != null);
            Debug.Assert(needPaintDelegate != null);

            _ribbon = control;
            _viewGroups = viewGroups;
            _needPaintDelegate = needPaintDelegate;
            _active = true;
            _minimizedMode = minimizedMode;
        }
        #endregion

        #region Active
        /// <summary>
        /// Application we are inside has become active.
        /// </summary>
        public void Active() => _active = true;
        #endregion

        #region Inactive
        /// <summary>
        /// Application we are inside has become inactive.
        /// </summary>
        public void Inactive()
        {
            if (_active)
            {
                // Simulate the mouse leaving the application
                MouseLeave(EventArgs.Empty);

                // No longer active
                _active = false;
            }
        }
        #endregion

        #region GetPreferredSize
        /// <summary>
        /// Discover the preferred size of the view.
        /// </summary>
        /// <param name="renderer">Renderer provider.</param>
        /// <param name="proposedSize">The custom-sized area for a control.</param>
        public override Size GetPreferredSize(IRenderer renderer,
                                              Size proposedSize)
        {
            // Update the calculate values used during layout calls
            _ribbon.CalculatedValues.Recalculate();

            // Let base class perform standard preferred sizing actions
            return base.GetPreferredSize(renderer, proposedSize);
        }
        #endregion

        #region Layout
        /// <summary>
        /// Perform a layout of the view.
        /// </summary>
        /// <param name="context">View context for layout operation.</param>
        public override void Layout(ViewLayoutContext context)
        {
            // Prevent reentrancy
            if (!_layingOut)
            {
                Form ownerForm = _ribbon.FindForm();

                // We do not need to layout if inside a control that is minimized or if we are not inside a form at all
                if ((ownerForm == null) || (ownerForm.WindowState == FormWindowState.Minimized))
                {
                    return;
                }

                _layingOut = true;

                // Update the calculate values used during layout calls
                _ribbon.CalculatedValues.Recalculate();

                // Let base class perform standard layout actions
                base.Layout(context);

                _layingOut = false;
            }
        }
        #endregion

        #region Mouse
        /// <summary>
        /// Perform mouse movement handling.
        /// </summary>
        /// <param name="e">A MouseEventArgs that contains the event data.</param>
        /// <param name="rawPt">The actual point provided from the windows message.</param>
        public override void MouseMove([DisallowNull] MouseEventArgs e, Point rawPt)
        {
            Debug.Assert(e != null);

            // Validate incoming reference
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            if (!_ribbon.InDesignMode)
            {
                // Only interested if the application window we are inside is active or a docking floating window is active
                if (_active || (CommonHelper.ActiveFloatingWindow != null))
                {
                    // Only hot track groups if in the correct mode
                    if (_minimizedMode == _ribbon.RealMinimizedMode)
                    {
                        // Get the view group instance that matches this point
                        ViewDrawRibbonGroup viewGroup = _viewGroups.ViewGroupFromPoint(new Point(e.X, e.Y));

                        // Is there a change in active group?
                        if (viewGroup != _activeGroup)
                        {
                            if (_activeGroup != null)
                            {
                                _activeGroup.Tracking = false;
                                _activeGroup.PerformNeedPaint(false, _activeGroup.ClientRectangle);
                            }

                            _activeGroup = viewGroup;

                            if (_activeGroup != null)
                            {
                                _activeGroup.Tracking = true;
                                _activeGroup.PerformNeedPaint(false, _activeGroup.ClientRectangle);
                            }
                        }
                    }
                }
            }

            // Remember to call base class for standard mouse processing
            base.MouseMove(e, rawPt);
        }

        /// <summary>
        /// Perform mouse leave processing.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        public override void MouseLeave([DisallowNull] EventArgs e)
        {
            Debug.Assert(e != null);

            // Validate incoming reference
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            if (!_ribbon.InDesignMode)
            {
                // Only interested if the application window we are inside is active or a docking floating window is active
                if (_active || (CommonHelper.ActiveFloatingWindow != null))
                {
                    // If there is an active element
                    if (_activeGroup != null)
                    {
                        _activeGroup.PerformNeedPaint(false, _activeGroup.ClientRectangle);
                        _activeGroup.Tracking = false;
                        _activeGroup = null;
                    }
                }
            }

            // Remember to call base class for standard mouse processing
            base.MouseLeave(e);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Update the active view based on the mouse position.
        /// </summary>
        /// <param name="control">Source control.</param>
        /// <param name="pt">Point within the source control.</param>
        protected override void UpdateViewFromPoint(Control control, Point pt)
        {
            // If our application is inactive
            if (!_active && (CommonHelper.ActiveFloatingWindow == null))
            {
                // And the mouse is not captured
                if (!MouseCaptured)
                {
                    // Then get the view under the mouse
                    ViewBase mouseView = Root.ViewFromPoint(pt);

                    // We only allow application button views to be interacted with
                    ActiveView = mouseView is ViewDrawRibbonAppButton ? mouseView : null;
                }
            }
            else
            {
                base.UpdateViewFromPoint(control, pt);
            }
        }
        #endregion

        #region Implementation
        private void PerformNeedPaint(bool needLayout) => PerformNeedPaint(needLayout, Rectangle.Empty);

        private void PerformNeedPaint(bool needLayout, Rectangle invalidRect) => _needPaintDelegate?.Invoke(this, new NeedLayoutEventArgs(needLayout, invalidRect));
        #endregion
    }
}
