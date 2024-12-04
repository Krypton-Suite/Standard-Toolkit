﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2024. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    #region ISeparatorSource
    /// <summary>
    /// Describes the interface exposed by a separator source.
    /// </summary>
    public interface ISeparatorSource
    {
        /// <summary>
        /// Gets the top level control of the source.
        /// </summary>
        Control SeparatorControl { get; }

        /// <summary>
        /// Gets the orientation of the separator.
        /// </summary>
        Orientation SeparatorOrientation { get; }

        /// <summary>
        /// Can the separator be moved by the user.
        /// </summary>
        bool SeparatorCanMove { get; }

        /// <summary>
        /// Gets the amount the splitter can be incremented.
        /// </summary>
        int SeparatorIncrements { get; }

        /// <summary>
        /// Gets the box representing the minimum and maximum allowed splitter movement.
        /// </summary>
        Rectangle SeparatorMoveBox { get; }

        /// <summary>
        /// Indicates the separator is moving.
        /// </summary>
        /// <param name="mouse">Current mouse position in client area.</param>
        /// <param name="splitter">Current position of the splitter.</param>
        /// <returns>True if movement should be cancelled; otherwise false.</returns>
        bool SeparatorMoving(Point mouse, Point splitter);

        /// <summary>
        /// Indicates the separator has moved.
        /// </summary>
        /// <param name="mouse">Current mouse position in client area.</param>
        /// <param name="splitter">Current position of the splitter.</param>
        void SeparatorMoved(Point mouse, Point splitter);

        /// <summary>
        /// Indicates the separator has not moved.
        /// </summary>
        void SeparatorNotMoved();
    }
    #endregion

    /// <summary>
    /// Process mouse events for a separator style element.
    /// </summary>
    public class SeparatorController : ButtonController,
                                       IDisposable
    {
        #region Types
        /// <summary>
        /// 
        /// </summary>
        public class SeparatorIndicator : Form
        {
            #region Instance Fields
            private Rectangle _solidRect;
            #endregion

            #region Indentity
            /// <summary>
            /// Process mouse events for a separator style element.
            /// </summary>
            public SeparatorIndicator()
            {
                FormBorderStyle = FormBorderStyle.None;
                SizeGripStyle = SizeGripStyle.Hide;
                StartPosition = FormStartPosition.Manual;
                MaximizeBox = false;
                MinimizeBox = false;
                ShowInTaskbar = false;
                BackColor = Color.Black;
                TransparencyKey = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
                Opacity = 0.5;
            }

            /// <summary>
            /// Clean up any resources being used.
            /// </summary>
            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                }

                base.Dispose(disposing);
            }
            #endregion

            #region Public
            /// <summary>
            /// Show the window without taking activation.
            /// </summary>
            public void ShowWithoutActivate() =>
                // Show the window without activating it (i.e. do not take focus)
                PI.ShowWindow(Handle, PI.ShowWindowCommands.SW_SHOWNOACTIVATE);

            /// <summary>
            /// Gets and sets the new solid rectangle area.
            /// </summary>
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public Rectangle SolidRect
            {
                get => _solidRect;

                set
                {
                    if (_solidRect != value)
                    {
                        _solidRect = value;
                        DesktopBounds = _solidRect;
                        Refresh();
                    }
                }
            }

            #endregion

            #region Protected
            /// <summary>
            /// Processes Windows messages.
            /// </summary>
            /// <param name="m">The Windows Message to process. </param>
            protected override void WndProc(ref Message m)
            {
                // We are a transparent window, so mouse is never over us
                if (m.Msg == PI.WM_.NCHITTEST)
                {
                    // Allow actions to occur to window beneath us
                    m.Result = (IntPtr)PI.HT.TRANSPARENT;
                }
                else
                {
                    base.WndProc(ref m);
                }
            }
            #endregion
        }
        #endregion

        #region Static Fields
        // TODO: Should be scaled
        private static readonly Point _nullPoint = new Point(-1, -1);
        private static readonly Cursor _cursorHSplit = Cursors.HSplit;
        private static readonly Cursor _cursorVSplit = Cursors.VSplit;
        private static readonly Cursor _cursorHMove = Cursors.SizeNS;
        private static readonly Cursor _cursorVMove = Cursors.SizeWE;
        #endregion

        #region Instance Fields
        private bool _drawIndicator;
        private readonly bool _splitCursors;
        private bool _moving;
        private Point _downPosition;
        private int _separatorIncrements;
        private Rectangle _separatorBox;
        private Orientation _separatorOrientation;
        private SeparatorMessageFilter? _filter;
        private readonly ISeparatorSource _source;
        private SeparatorIndicator? _indicator;

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the SeparatorController class.
        /// </summary>
        /// <param name="source">Source of separator information.</param>
        /// <param name="target">Target for state changes.</param>
        /// <param name="splitCursors">Show as split or movement cursors.</param>
        /// <param name="drawIndicator">Draw a separator indicator.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public SeparatorController([DisallowNull] ISeparatorSource source,
                                   ViewBase target,
                                   bool splitCursors,
                                   bool drawIndicator,
                                   NeedPaintHandler needPaint)
            : base(target, needPaint)
        {
            Debug.Assert(source is not null);

            _source = source ?? throw new ArgumentNullException(nameof(source));
            _splitCursors = splitCursors;
            _drawIndicator = drawIndicator;
        }

        /// <summary>
        /// Dispose of object resources.
        /// </summary>
        public void Dispose()
        {
            UnRegisterFilter();
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the drawing of the movement indicator.
        /// </summary>
        public bool DrawMoveIndicator
        {
            get => _drawIndicator;
            set => _drawIndicator = value;
        }
        #endregion

        #region Mouse Notifications
        /// <summary>
        /// Mouse has moved inside the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
        public override void MouseMove(Control c, Point pt)
        {
            // If the separator is allowed to be moved by the user
            if (_source.SeparatorCanMove)
            {
                // Cursor depends on orientation direction
                if (_source.SeparatorOrientation == Orientation.Vertical)
                {
                    _source.SeparatorControl.Cursor = _splitCursors ? _cursorVSplit : _cursorVMove;
                }
                else
                {
                    _source.SeparatorControl.Cursor = _splitCursors ? _cursorHSplit : _cursorHMove;
                }
            }

            // If we are currently capturing input
            if (_moving)
            {
                // Update the split indicator to new position
                Point splitPt = RecalcClient(pt);
                DrawSeparatorReposition(splitPt);

                // Callback to the source to show movement
                if (_source.SeparatorMoving(pt, splitPt))
                {
                    AbortMoving();
                }
            }

            // Let base class do standard processing
            base.MouseMove(c, pt);
        }

        /// <summary>
        /// Mouse button has been pressed in the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
        /// <param name="button">Mouse button pressed down.</param>
        /// <returns>True if capturing input; otherwise false.</returns>
        public override bool MouseDown(Control c, Point pt, MouseButtons button)
        {
            // Let base class process the mouse down
            var ret = base.MouseDown(c, pt, button);

            // If a change in capturing state has occurred
            if (ret != _moving)
            {
                // If we are now capturing input
                if (ret)
                {
                    // Remember the starting mouse position
                    _downPosition = pt;

                    // Remember new capture state
                    _moving = true;

                    // Cache information from the separator
                    _separatorBox = _source.SeparatorMoveBox;
                    _separatorIncrements = _source.SeparatorIncrements;
                    _separatorOrientation = _source.SeparatorOrientation;

                    // Make sure any paint requested by base class has been performed
                    _source.SeparatorControl.Update();

                    // Draw the initial movement indicator
                    Point splitPt = RecalcClient(pt);
                    DrawSeparatorStarting(splitPt);

                    // Register a message filter to intercept the escape key
                    RegisterFilter();
                }
                else
                {
                    // We must have lost capture
                    _moving = false;

                    // Remove the message filter, as long as it is registered 
                    // it will prevent the class from being garbage collected.
                    UnRegisterFilter();

                    // Callback to the source to show movement has finished
                    Point splitPt = RecalcClient(pt);
                    _source.SeparatorMoved(pt, splitPt);
                }
            }

            return ret;
        }

        /// <summary>
        /// Mouse button has been released in the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
        /// <param name="button">Mouse button released.</param>
        public override void MouseUp(Control c, Point pt, MouseButtons button)
        {
            // Let base class process up event
            base.MouseUp(c, pt, button);

            // If the mouse up has caused a change in capture
            if (Captured != _moving)
            {
                // We must have lost capture
                _moving = false;

                // Remove the message filter, as long as it is registered 
                // it will prevent the class from being garbage collected.
                UnRegisterFilter();

                // Remove any showing separator indicator
                DrawSeparatorRemoved();

                // Callback to the source to show movement has finished
                Point splitPt = RecalcClient(pt);
                _source.SeparatorMoved(pt, splitPt);
            }
        }

        /// <summary>
        /// Mouse has left the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="next">Reference to view that is next to have the mouse.</param>
        public override void MouseLeave(Control c, ViewBase? next)
        {
            // If leaving when currently moving, then abort the movement
            if (_moving)
            {
                AbortMoving();
            }

            // Reset the cursor back to the default
            _source.SeparatorControl.Cursor = Cursors.Default;

            // Let base class do standard processing
            base.MouseLeave(c, next);
        }
        #endregion

        #region Key Notifications
        /// <summary>
        /// Key has been pressed down.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        public override void KeyDown(Control c, KeyEventArgs e)
        {
            // Do nothing, no keys have an effect when pressed
        }

        /// <summary>
        /// Key has been released.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        /// <returns>True if capturing input; otherwise false.</returns>
        public override bool KeyUp(Control c, [DisallowNull] KeyEventArgs e)
        {
            Debug.Assert(e != null);

            // Validate reference parameter
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            // If the user pressed the escape key
            if (e.KeyCode == Keys.Escape)
            {
                // If we are capturing mouse input
                if (_moving)
                {
                    AbortMoving();
                }
            }

            return _moving;
        }
        #endregion

        #region Source Notifications
        /// <summary>
        /// Source control has lost the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public override void LostFocus([DisallowNull] Control c)
        {
            // If we are capturing mouse input
            if (_moving)
            {
                AbortMoving();
            }
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets a value indicating if the separator is moving.
        /// </summary>
        public bool IsMoving => _moving;

        /// <summary>
        /// Request that the separator abort moving.
        /// </summary>
        public void AbortMoving()
        {
            // If currently trying to move the splitter
            if (_moving)
            {
                // Exit the capturing state
                _moving = false;
                Captured = false;

                // Remove the capturing of mouse input messages
                if (_source.SeparatorControl.Capture)
                {
                    _source.SeparatorControl.Capture = false;
                }

                // Remove the message filter, as long as it is registered 
                // it will prevent the class from being garbage collected.
                UnRegisterFilter();

                // Remove any showing separator indicator
                DrawSeparatorRemoved();

                // Redraw the new state
                UpdateTargetState(_source.SeparatorControl);

                // Inform that the separator movement was finished without moving
                _source.SeparatorNotMoved();
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Draw the initial separator position.
        /// </summary>
        /// <param name="splitter">Initial splitter position.</param>
        protected void DrawSeparatorStarting(Point splitter) =>
            // Reset the starting point
            // Draw the initial indication
            DrawSplitIndicator(splitter);

        /// <summary>
        /// Redraw the splitter in the new position.
        /// </summary>
        /// <param name="splitter">New position of the splitter.</param>
        protected void DrawSeparatorReposition(Point splitter) =>
            // Draw splitter in the new position
            DrawSplitIndicator(splitter);

        /// <summary>
        /// Remove drawing of any splitter.
        /// </summary>
        protected void DrawSeparatorRemoved() =>
            // Remove any showing separator
            DrawSplitIndicator(_nullPoint);

        #endregion

        #region Protected Overrides
        /// <summary>
        /// Get a value indicating if the controller is operating
        /// </summary>
        protected override bool IsOperating
        {
            get =>
                // If the separator cannot be moved then if should not
                // act has a hot tracking/pressable style button either
                _source.SeparatorCanMove;

            set { }
        }

        /// <summary>
        /// Gets a value indicating if the state is pressed only when over button.
        /// </summary>
        protected override bool IsOnlyPressedWhenOver
        {
            get =>
                // We want the separator to have the pressed look event when 
                // the mouse is pressed by moved outside the separator rectangle
                false;
            set { }
        }
        #endregion

        #region Implementation
        private Point RecalcClient(Point pt)
        {
            // Find the delta between the incoming point and the original mouse down
            var xDelta = pt.X - _downPosition.X;
            var yDelta = pt.Y - _downPosition.Y;

            // Enforce the movement box limits
            if (_separatorOrientation == Orientation.Vertical)
            {
                if (Target.ClientLocation.X + xDelta < _separatorBox.Left)
                {
                    xDelta = _separatorBox.Left - Target.ClientLocation.X;
                }

                if (Target.ClientLocation.X + xDelta > _separatorBox.Right)
                {
                    xDelta = _separatorBox.Right - Target.ClientLocation.X;
                }
            }
            else
            {
                if (Target.ClientLocation.Y + yDelta < _separatorBox.Top)
                {
                    yDelta = _separatorBox.Top - Target.ClientLocation.Y;
                }

                if (Target.ClientLocation.Y + yDelta > _separatorBox.Bottom)
                {
                    yDelta = _separatorBox.Bottom - Target.ClientLocation.Y;
                }
            }

            // Enforce the increments on the deltas
            xDelta -= xDelta % _separatorIncrements;
            yDelta -= yDelta % _separatorIncrements;

            // Return the top left point of the client by the delta
            return new Point(Target.ClientLocation.X + xDelta,
                             Target.ClientLocation.Y + yDelta);
        }

        private void DrawSplitIndicator(Point newPoint)
        {
            if (DrawMoveIndicator)
            {
                if (newPoint == _nullPoint)
                {
                    if (_indicator != null)
                    {
                        _indicator.Dispose();
                        _indicator = null;
                    }
                }
                else
                {
                    if (_indicator == null)
                    {
                        _indicator = new SeparatorIndicator();
                        _indicator.ShowWithoutActivate();
                    }

                    _indicator.SolidRect = SplitRectangleFromPoint(newPoint);
                }
            }
            else
            {
                if (_indicator != null)
                {
                    _indicator.Dispose();
                    _indicator = null;
                }
            }

        }

        private Rectangle SplitRectangleFromPoint(Point pt) =>
            _separatorOrientation == Orientation.Vertical
                ? SplitRectangleFromPoint(pt, Target.ClientWidth)
                : SplitRectangleFromPoint(pt, Target.ClientHeight);

        private Rectangle SplitRectangleFromPoint(Point pt, int length)
        {
            Rectangle splitRectangle;

            // Find the splitter rectangle based on the orientation
            if (_separatorOrientation == Orientation.Vertical)
            {
                splitRectangle = new Rectangle(pt.X, _separatorBox.Y, length, Target.ClientHeight);
            }
            else
            {
                splitRectangle = new Rectangle(_separatorBox.X, pt.Y, Target.ClientWidth, length);
            }

            var rect = _source.SeparatorControl.RectangleToScreen(splitRectangle);

            //https://github.com/Krypton-Suite/Standard-Toolkit/issues/364
            // remove the following 2 lines as this causes problems in Win 10  - No idea what the intention was here ?
            //var area = Screen.GetWorkingArea(rect);
            //rect = new Rectangle(rect.Location - (Size)area.Location, rect.Size);

            return rect;
        }

        private void RegisterFilter()
        {
            if (_filter == null)
            {
                // Check that caller has permission to access unmanaged code
                try
                {
                    _filter = new SeparatorMessageFilter(this);
                    Application.AddMessageFilter(_filter);
                }
                finally
                {
                    // Always revert the assertion made above
                }
            }
        }

        private void UnRegisterFilter()
        {
            if (_filter != null)
            {
                Application.RemoveMessageFilter(_filter);
                _filter = null;
            }
        }
        #endregion

        #region Implementation Static
        private static void DrawSplitIndicator(Rectangle drawRect) =>
            // We just perform a simple reversible rectangle draw
            ControlPaint.FillReversibleRectangle(drawRect, Color.Black);

        #endregion
    }

    internal class SeparatorMessageFilter : IMessageFilter
    {
        #region Instance Fields
        private readonly SeparatorController _controller;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the SeparatorMessageFilter class.
        /// </summary>
        /// <param name="controller">Owning class instance.</param>
        public SeparatorMessageFilter([DisallowNull] SeparatorController controller)
        {
            Debug.Assert(controller is not null);

            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
        }
        #endregion

        #region IMessageFilter
        /// <summary>
        /// Filters out a message before it is dispatched.
        /// </summary>
        /// <param name="m">The message to be dispatched.</param>
        /// <returns>true to filter the message and stop it from being dispatched.</returns>
        public bool PreFilterMessage(ref Message m)
        {
            // We are only interested in filtering when moving the separator
            if (!_controller.IsMoving)
            {
                return false;
            }

            switch (m.Msg)
            {
                // We allow all non-keyboard messages
                case < 0x100 or > 0x108:
                    return false;
                // If the user presses the escape key, windows keys or any system key
                case 0x100 when (((int)m.WParam.ToInt64()) == 0x1B):
                case 0x100 when (((int)m.WParam.ToInt64()) == 0x5B):
                case 0x104:
                    _controller.AbortMoving();
                    break;
            }

            // We filter out all keyboard messages
            return true;
        }
        #endregion
    }
}