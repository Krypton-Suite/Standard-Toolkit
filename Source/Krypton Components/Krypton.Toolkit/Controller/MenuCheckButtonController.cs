﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    internal class MenuCheckButtonController : GlobalId,
                                               IMouseController,
                                               IKeyController,
                                               ISourceController,
                                               IContextMenuTarget
    {
        #region Instance Fields
        private bool _mouseOver;
        private bool _mouseReallyOver;
        private bool _highlight;
        private bool _mouseDown;
        private readonly ViewBase _target;
        private readonly ViewDrawMenuCheckButton _menuCheckButton;
        private NeedPaintHandler _needPaint;

        #endregion

        #region Events
        /// <summary>
        /// Occurs when the check button has been clicked.
        /// </summary>
        public event EventHandler Click;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the MenuCheckButtonController class.
        /// </summary>
        /// <param name="viewManager">Owning view manager instance.</param>
        /// <param name="target">Target for state changes.</param>
        /// <param name="checkButton">Drawing element that owns check button display.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public MenuCheckButtonController(ViewContextMenuManager viewManager,
                                         ViewBase target,
                                         ViewDrawMenuCheckButton checkButton,
                                         NeedPaintHandler needPaint)
        {
            Debug.Assert(viewManager != null);
            Debug.Assert(target != null);
            Debug.Assert(checkButton != null);
            Debug.Assert(needPaint != null);

            ViewManager = viewManager;
            _target = target;
            _menuCheckButton = checkButton;
            NeedPaint = needPaint;

            // Set initial display state
            UpdateTarget();
        }
        #endregion

        #region ContextMenuTarget Notifications
        /// <summary>
        /// Returns if the item shows a sub menu when selected.
        /// </summary>
        public virtual bool HasSubMenu => false;

        /// <summary>
        /// This target should display as the active target.
        /// </summary>
        public virtual void ShowTarget() => HighlightState();

        /// <summary>
        /// This target should clear any active display.
        /// </summary>
        public virtual void ClearTarget() => NormalState();

        /// <summary>
        /// This target should show any appropriate sub menu.
        /// </summary>
        public void ShowSubMenu()
        {
        }

        /// <summary>
        /// This target should remove any showing sub menu.
        /// </summary>
        public void ClearSubMenu()
        {
        }

        /// <summary>
        /// Determine if the keys value matches the mnemonic setting for this target.
        /// </summary>
        /// <param name="charCode">Key code to test against.</param>
        /// <returns>True if a match is found; otherwise false.</returns>
        public bool MatchMnemonic(char charCode) =>
            // Only interested in enabled items
            _menuCheckButton.ItemEnabled && Control.IsMnemonic(charCode, _menuCheckButton.ItemText);

        /// <summary>
        /// Activate the item because of a mnemonic key press.
        /// </summary>
        public void MnemonicActivate()
        {
            // Only interested in enabled items
            if (_menuCheckButton.ItemEnabled)
            {
                PressMenuCheckButton(true);
            }
        }

        /// <summary>
        /// Gets the view element that should be used when this target is active.
        /// </summary>
        /// <returns>View element to become active.</returns>
        public ViewBase GetActiveView() => _target;

        /// <summary>
        /// Get the client rectangle for the display of this target.
        /// </summary>
        public Rectangle ClientRectangle => _target.ClientRectangle;

        /// <summary>
        /// Should a mouse down at the provided point cause the currently stacked context menu to become current.
        /// </summary>
        /// <param name="pt">Client coordinates point.</param>
        /// <returns>True to become current; otherwise false.</returns>
        public bool DoesStackedClientMouseDownBecomeCurrent(Point pt) => true;

        #endregion
        
        #region Mouse Notifications
        /// <summary>
        /// Mouse has entered the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public virtual void MouseEnter(Control c)
        {
            if (!_mouseOver && _menuCheckButton.ItemEnabled)
            {
                _mouseReallyOver = _target.ClientRectangle.Contains(c.PointToClient(Control.MousePosition));
                _mouseOver = true;
                ViewManager.SetTarget(this, true);
                UpdateTarget();
            }
        }

        /// <summary>
        /// Mouse has moved inside the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
        public virtual void MouseMove(Control c, Point pt)
        {
            if (_menuCheckButton.ItemEnabled)
            {
                _mouseReallyOver = true;
            }
        }

        /// <summary>
        /// Mouse button has been pressed in the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
        /// <param name="button">Mouse button pressed down.</param>
        /// <returns>True if capturing input; otherwise false.</returns>
        public virtual bool MouseDown(Control c, Point pt, MouseButtons button)
        {
            if ((button == MouseButtons.Left) && _menuCheckButton.ItemEnabled)
            {
                _mouseDown = true;
                UpdateTarget();
            }

            return false;
        }

        /// <summary>
        /// Mouse button has been released in the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
        /// <param name="button">Mouse button released.</param>
        public virtual void MouseUp(Control c, Point pt, MouseButtons button)
        {
            if (_mouseDown && (button == MouseButtons.Left))
            {
                _mouseDown = false;
                UpdateTarget();
                PressMenuCheckButton(false);
            }
        }

        /// <summary>
        /// Mouse has left the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="next">Reference to view that is next to have the mouse.</param>
        public virtual void MouseLeave(Control c, ViewBase? next)
        {
            // Only if mouse is leaving all the children monitored by controller.
            if (!_target.ContainsRecurse(next))
            {
                _mouseOver = false;
                _mouseReallyOver = false;
                _mouseDown = false;
                ViewManager.ClearTarget(this);
                UpdateTarget();
            }
        }

        /// <summary>
        /// Left mouse button double click.
        /// </summary>
        /// <param name="pt">Mouse position relative to control.</param>
        public virtual void DoubleClick(Point pt)
        {
            // Do nothing
        }

        /// <summary>
        /// Should the left mouse down be ignored when present on a visual form border area.
        /// </summary>
        public virtual bool IgnoreVisualFormLeftButtonDown => false;

        #endregion

        #region Key Notifications

        /// <summary>
        /// Key has been pressed down.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void KeyDown(Control c, KeyEventArgs e)
        {
            Debug.Assert(c != null);
            Debug.Assert(e != null);

            // Validate incoming references
            if (c == null)
            {
                throw new ArgumentNullException(nameof(c));
            }

            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Space:
                    // Only interested in enabled items
                    if (_menuCheckButton.ItemEnabled)
                    {
                        PressMenuCheckButton(true);
                    }

                    break;
                case Keys.Tab:
                    ViewManager.KeyTab(e.Shift);
                    break;
                case Keys.Home:
                    ViewManager.KeyHome();
                    break;
                case Keys.End:
                    ViewManager.KeyEnd();
                    break;
                case Keys.Up:
                    ViewManager.KeyUp();
                    break;
                case Keys.Down:
                    ViewManager.KeyDown();
                    break;
                case Keys.Left:
                    ViewManager.KeyLeft(true);
                    break;
                case Keys.Right:
                    ViewManager.KeyRight();
                    break;
            }
        }

        /// <summary>
        /// Key has been pressed.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void KeyPress(Control c, KeyPressEventArgs e)
        {
            Debug.Assert(c != null);
            Debug.Assert(e != null);

            // Validate incoming references
            if (c == null)
            {
                throw new ArgumentNullException(nameof(c));
            }

            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            ViewManager.KeyMnemonic(e.KeyChar);
        }

        /// <summary>
        /// Key has been released.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>True if capturing input; otherwise false.</returns>
        public virtual bool KeyUp(Control c, KeyEventArgs e)
        {
            Debug.Assert(c != null);
            Debug.Assert(e != null);

            // Validate incoming references
            if (c == null)
            {
                throw new ArgumentNullException(nameof(c));
            }

            return e == null ? throw new ArgumentNullException(nameof(e)) : false;
        }
        #endregion

        #region Source Notifications
        /// <summary>
        /// Source control has lost the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public virtual void GotFocus(Control c)
        {
        }

        /// <summary>
        /// Source control has lost the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public virtual void LostFocus(Control c)
        {
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the need paint delegate for notifying paint requests.
        /// </summary>
        public NeedPaintHandler NeedPaint
        {
            get => _needPaint;

            set
            {
                // Warn if multiple sources want to hook their single delegate
                Debug.Assert(((_needPaint == null) && (value != null)) ||
                             ((_needPaint != null) && (value == null)));

                _needPaint = value;
            }
        }

        /// <summary>
        /// Fires the NeedPaint event.
        /// </summary>
        public void PerformNeedPaint() => OnNeedPaint();

        #endregion

        #region Private
        private ViewContextMenuManager ViewManager { get; }

        private void PressMenuCheckButton(bool keyboard)
        {
            if (keyboard)
            {
                _menuCheckButton.ViewDrawButton.ElementState = PaletteState.Pressed;
                PerformNeedPaint();
                Application.DoEvents();
            }

            // Should we automatically try and close the context menu stack
            if (_menuCheckButton.KryptonContextMenuCheckButton.AutoClose)
            {
                // Is the menu capable of being closed?
                if (_menuCheckButton.CanCloseMenu)
                {
                    // Ask the original context menu definition, if we can close
                    CancelEventArgs cea = new();
                    _menuCheckButton.Closing(cea);

                    if (!cea.Cancel)
                    {
                        // Close the menu from display and pass in the item clicked as the reason
                        _menuCheckButton.Close(new CloseReasonEventArgs(ToolStripDropDownCloseReason.ItemClicked));
                    }
                }
            }

            // Do we need to automatically change the checked state?
            if (_menuCheckButton.KryptonContextMenuCheckButton.AutoCheck)
            {
                // Get the current checked state
                var checkState = _menuCheckButton.KryptonContextMenuCheckButton.KryptonCommand?.Checked ??
                                 _menuCheckButton.KryptonContextMenuCheckButton.Checked;

                // Invert state
                checkState = !checkState;

                // Set the state back again
                if (_menuCheckButton.KryptonContextMenuCheckButton.KryptonCommand == null)
                {
                    _menuCheckButton.KryptonContextMenuCheckButton.Checked = checkState;
                }
                else
                {
                    _menuCheckButton.KryptonContextMenuCheckButton.KryptonCommand.Checked = checkState;
                }
            }

            Click?.Invoke(this, EventArgs.Empty);

            if (keyboard)
            {
                UpdateTarget();
                PerformNeedPaint();
            }
        }

        private void OnNeedPaint() => _needPaint?.Invoke(this, new NeedLayoutEventArgs(false));

        private void HighlightState()
        {
            _highlight = true;
            UpdateTarget();
        }

        private void NormalState()
        {
            _highlight = false;
            UpdateTarget();
        }

        private void UpdateTarget()
        {
            // Find new state for drawing the check box
            PaletteState state = _menuCheckButton.ItemEnabled ? PaletteState.Normal : PaletteState.Disabled;
            if (_mouseOver)
            {
                state = _mouseDown ? PaletteState.Pressed : PaletteState.Tracking;
            }

            // Should the state be modified to reflect checked state?
            if (_menuCheckButton.ResolveChecked)
            {
                state |= PaletteState.Checked;
            }

            var applyFocus = _highlight && !_mouseReallyOver;
            _menuCheckButton.KryptonContextMenuCheckButton.OverrideDisabled.Apply = applyFocus;
            _menuCheckButton.KryptonContextMenuCheckButton.OverrideNormal.Apply = applyFocus;
            _menuCheckButton.KryptonContextMenuCheckButton.OverrideTracking.Apply = applyFocus;
            _menuCheckButton.KryptonContextMenuCheckButton.OverridePressed.Apply = applyFocus;
            _menuCheckButton.KryptonContextMenuCheckButton.OverrideCheckedNormal.Apply = applyFocus;
            _menuCheckButton.KryptonContextMenuCheckButton.OverrideCheckedTracking.Apply = applyFocus;
            _menuCheckButton.KryptonContextMenuCheckButton.OverrideCheckedPressed.Apply = applyFocus;
            _menuCheckButton.ViewDrawButton.ElementState = state;
            PerformNeedPaint();
        }
        #endregion
    }
}
