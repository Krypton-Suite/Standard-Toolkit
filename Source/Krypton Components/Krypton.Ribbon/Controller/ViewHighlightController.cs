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
    /// <summary>
    /// Process mouse events for a standard button.
    /// </summary>
    internal class ViewHightlightController : GlobalId,
                                              IMouseController
    {
        #region Instance Fields
        private NeedPaintHandler? _needPaint;
        private bool _mouseOver;
        private bool _rightButtonDown;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the user left clicks the view.
        /// </summary>
        public event EventHandler? Click;

        /// <summary>
        /// Occurs when the user right clicks the view.
        /// </summary>
        public event MouseEventHandler? ContextClick;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewHightlightController class.
        /// </summary>
        /// <param name="target">Target for state changes.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewHightlightController([DisallowNull] ViewBase target,
                                        [DisallowNull] NeedPaintHandler needPaint)
        {
            Debug.Assert(target != null);
            Debug.Assert(needPaint != null);

            Target = target;
            NeedPaint = needPaint;
        }
        #endregion

        #region Mouse Notifications
        /// <summary>
        /// Mouse has entered the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public virtual void MouseEnter(Control c)
        {
            // Mouse is over the target
            _mouseOver = true;

            // Update the visual state
            UpdateTargetState(c);
        }

        /// <summary>
        /// Mouse has moved inside the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
        public virtual void MouseMove(Control c, Point pt)
        {
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
            if (_mouseOver 
                && (button == MouseButtons.Left))
            {
                OnClick(EventArgs.Empty);
            }

            // Remember the user has pressed the right mouse button down
            if (button == MouseButtons.Right)
            {
                _rightButtonDown = true;
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
            // If user is releasing the right mouse button
            if (button == MouseButtons.Right)
            {
                // And it was pressed over the tab
                if (_rightButtonDown)
                {
                    _rightButtonDown = false;

                    // Raises event so a context menu for the ribbon can be shown
                    OnContextClick(new MouseEventArgs(MouseButtons.Right, 1, pt.X, pt.Y, 0));
                }
            }
        }

        /// <summary>
        /// Mouse has left the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="next">Reference to view that is next to have the mouse.</param>
        public virtual void MouseLeave(Control c, ViewBase? next)
        {
            // Mouse is no longer over the target
            _mouseOver = false;

            // Update the visual state
            UpdateTargetState(c);
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

        #region Public
        /// <summary>
        /// Gets and sets the need paint delegate for notifying paint requests.
        /// </summary>
        public NeedPaintHandler? NeedPaint
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
        /// Gets access to the associated target of the controller.
        /// </summary>
        public ViewBase Target { get; }

        /// <summary>
        /// Fires the NeedPaint event.
        /// </summary>
        public void PerformNeedPaint() => OnNeedPaint(false);

        /// <summary>
        /// Fires the NeedPaint event.
        /// </summary>
        /// <param name="needLayout">Does the palette change require a layout.</param>
        public void PerformNeedPaint(bool needLayout) => OnNeedPaint(needLayout);
        #endregion

        #region Protected
        /// <summary>
        /// Set the correct visual state of the target.
        /// </summary>
        /// <param name="c">Owning control.</param>
        protected void UpdateTargetState(Control c)
        {
            if ((c == null) || c.IsDisposed)
            {
                UpdateTargetState(new Point(int.MaxValue, int.MaxValue));
            }
            else
            {
                UpdateTargetState(c.PointToClient(Control.MousePosition));
            }
        }

        /// <summary>
        /// Set the correct visual state of the target.
        /// </summary>
        /// <param name="pt">Mouse point.</param>
        protected void UpdateTargetState(Point pt)
        {
            // Decide if tracking the target or not
            PaletteState newState = _mouseOver ? PaletteState.Tracking : PaletteState.Normal;

            // If state has changed
            if (Target.ElementState != newState)
            {
                Target.ElementState = newState;

                // Redraw to show the change in visual state
                OnNeedPaint(false);
            }
        }

        /// <summary>
        /// Raises the NeedPaint event.
        /// </summary>
        /// <param name="needLayout">Does the palette change require a layout.</param>
        protected virtual void OnNeedPaint(bool needLayout) => _needPaint?.Invoke(this, new NeedLayoutEventArgs(needLayout, Target.ClientRectangle));

        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnClick(EventArgs e) => Click?.Invoke(this, e);

        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="e">A MouseEventArgs containing the event data.</param>
        protected virtual void OnContextClick(MouseEventArgs e) => ContextClick?.Invoke(this, e);
        #endregion
    }
}
