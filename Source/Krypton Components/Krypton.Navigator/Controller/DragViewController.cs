#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Navigator;

/// <summary>
/// Process mouse events for handling drag and drop operations.
/// </summary>
public class DragViewController : GlobalId,
    IMouseController,
    IKeyController,
    ISourceController
{
    #region Instance Fields

    private bool _dragging;
    private bool _draggingAttempt;
    private DateTime _lastClick;
    private Rectangle _dragRect;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the left mouse button is pressed down.
    /// </summary>
    public event EventHandler? LeftMouseDown;

    /// <summary>
    /// Occurs when the right mouse button is pressed down.
    /// </summary>
    public event EventHandler? RightMouseDown;

    /// <summary>
    /// Occurs when the left mouse double click.
    /// </summary>
    public event EventHandler? LeftDoubleClick;

    /// <summary>
    /// Occurs when start of drag operation occurs.
    /// </summary>
    public event EventHandler<DragStartEventCancelArgs>? DragStart;

    /// <summary>
    /// Occurs when drag moves.
    /// </summary>
    public event EventHandler<PointEventArgs>? DragMove;

    /// <summary>
    /// Occurs when drag ends.
    /// </summary>
    public event EventHandler<PointEventArgs>? DragEnd;

    /// <summary>
    /// Occurs when drag quits.
    /// </summary>
    public event EventHandler? DragQuit;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the DragViewController class.
    /// </summary>
    /// <param name="target">Target for state changes.</param>
    public DragViewController([DisallowNull] ViewBase target)
    {
        Debug.Assert(target != null);

        MousePoint = CommonHelper.NullPoint;
        AllowDragging = true;
        _dragging = false;
        Target = target ?? throw new ArgumentNullException(nameof(target));
        _lastClick = DateTime.Now.AddDays(-1);
    }
    #endregion

    #region MousePoint
    /// <summary>
    /// Gets the current tracking mouse point.
    /// </summary>
    public Point MousePoint { get; private set; }

    #endregion

    #region AllowDragging
    /// <summary>
    /// Gets and sets if dragging is allowed.
    /// </summary>
    public bool AllowDragging { get; set; }

    #endregion

    #region Mouse Notifications
    /// <summary>
    /// Mouse has entered the view.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    public virtual void MouseEnter(Control c)
    {
    }

    /// <summary>
    /// Mouse has moved inside the view.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="pt">Mouse position relative to control.</param>
    public virtual void MouseMove(Control c, Point pt)
    {
        // Track the mouse point
        MousePoint = pt;

        // If captured then we might want to handle dragging
        if (Captured & AllowDragging)
        {
            if (_dragging)
            {
                OnDragMove(MousePoint);
            }
            else if (!_dragRect.Contains(MousePoint))
            {
                // Only attempt dragging once per time the mouse is pressed on the element
                if (!_draggingAttempt)
                {
                    _draggingAttempt = true;
                    Point targetOrigin = Target.ClientLocation;
                    var offset = new Point(MousePoint.X - targetOrigin.X, MousePoint.Y - targetOrigin.Y);
                    OnDragStart(MousePoint, offset, c);
                }
            }
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
        switch (button)
        {
            // Only interested in left mouse pressing down
            case MouseButtons.Left:
                // Capturing mouse input
                c.Capture = true;
                Captured = true;
                _draggingAttempt = false;

                // Always indicate the left mouse was pressed
                OnLeftMouseDown(EventArgs.Empty);

                // Remember point when mouse when pressed, in case we want to start a drag/drop operation
                _dragRect = new Rectangle(pt, Size.Empty);
                _dragRect.Inflate(SystemInformation.DragSize);
                break;
            case MouseButtons.Right:
                // Always indicate the right mouse was pressed
                OnRightMouseDown(EventArgs.Empty);
                break;
        }

        return Captured;
    }

    /// <summary>
    /// Mouse button has been released in the view.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="pt">Mouse position relative to control.</param>
    /// <param name="button">Mouse button released.</param>
    public virtual void MouseUp(Control c, Point pt, MouseButtons button)
    {
        // If the mouse is currently captured
        if (Captured)
        {
            // Not capturing mouse input anymore
            Captured = false;

            // If currently dragging we need to end it
            if (_dragging)
            {
                if (button == MouseButtons.Left)
                {
                    OnDragEnd(pt);
                }
                else
                {
                    OnDragQuit();
                }
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
        // Only if mouse is leaving all the children monitored by controller.
        if (!Target.ContainsRecurse(next))
        {
            // Mouse is no longer over the target

            // Not tracking the mouse means a null value
            MousePoint = CommonHelper.NullPoint;

            // If leaving the view then cannot be capturing mouse input anymore
            Captured = false;

            // End any current dragging operation
            if (_dragging)
            {
                OnDragQuit();
            }
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
    public virtual void KeyDown(Control c, KeyEventArgs e)
    {
    }

    /// <summary>
    /// Key has been pressed.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
    public virtual void KeyPress(Control c, KeyPressEventArgs e)
    {
    }

    /// <summary>
    /// Key has been released.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="e">A KeyEventArgs that contains the event data.</param>
    /// <returns>True if capturing input; otherwise false.</returns>
    public virtual bool KeyUp([DisallowNull] Control c, [DisallowNull] KeyEventArgs e)
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

        // If the user pressed the escape key
        if (e.KeyCode == Keys.Escape)
        {
            // If we are capturing mouse input
            if (Captured)
            {
                // Release the mouse capture
                c.Capture = false;
                Captured = false;

                // End any current dragging operation
                if (_dragging)
                {
                    OnDragQuit();
                }

                // Recalculate if the mouse is over the button area
                // TODO: What is this doing ? i.e. should the return value be used ?
                return Target.ClientRectangle.Contains(c.PointToClient(Control.MousePosition));
            }
        }

        return Captured;
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
    public virtual void LostFocus([DisallowNull] Control c)
    {
        Debug.Assert(c != null);

        // Validate incoming references
        if (c == null)
        {
            throw new ArgumentNullException(nameof(c));
        }

        // If we are capturing mouse input
        if (Captured)
        {
            // Quit out of any dragging operation
            if (_dragging)
            {
                // Do not release capture!
                OnDragQuit();
            }
            else
            {
                // Release the mouse capture
                c.Capture = false;
                Captured = false;
            }

            // Recalculate if the mouse is over the button area
            // TODO: What is this doing ? i.e. should the return value be used ?
            Target.ClientRectangle.Contains(c.PointToClient(Control.MousePosition));
        }
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets access to the associated target of the controller.
    /// </summary>
    public ViewBase Target { get; }

    #endregion

    #region Protected
    /// <summary>
    /// Gets a value indicating if mouse input is being captured.
    /// </summary>
    protected bool Captured { get; set; }

    /// <summary>
    /// Raises the LeftMouseDown event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnLeftMouseDown(EventArgs e)
    {
        LeftMouseDown?.Invoke(this, e);

        // If this click is within the double click time of the last one, generate the double click event.
        DateTime now = DateTime.Now;
        if ((now - _lastClick).TotalMilliseconds < SystemInformation.DoubleClickTime)
        {
            // Generate double click event
            OnLeftDoubleClick(e);

            // Prevent a third click causing another double click by resetting the now time backwards
            now = now.AddDays(-1);
        }

        _lastClick = now;
    }

    /// <summary>
    /// Raises the RightMouseDown event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnRightMouseDown(EventArgs e) => RightMouseDown?.Invoke(this, e);

    /// <summary>
    /// Raises the LeftDoubleClick event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnLeftDoubleClick(EventArgs e) => LeftDoubleClick?.Invoke(this, e);

    /// <summary>
    /// Raises the DragStart event.
    /// </summary>
    /// <param name="mousePt">Mouse point at time of event.</param>
    /// <param name="offset">Offset of mouse compared to element.</param>
    /// <param name="c">Control that starts the drag operation.</param>
    protected virtual void OnDragStart(Point mousePt, Point offset, Control c)
    {
        // Convert point from client to screen coordinates
        mousePt = Target.OwningControl!.PointToScreen(mousePt);
        var ce = new DragStartEventCancelArgs(mousePt, offset, c);

        DragStart?.Invoke(this, ce);

        // If event is not cancelled then allow dragging
        _dragging = !ce.Cancel;
    }

    /// <summary>
    /// Raises the DragMove event.
    /// </summary>
    /// <param name="mousePt">Mouse point at time of event.</param>
    protected virtual void OnDragMove(Point mousePt)
    {
        if (DragMove != null)
        {
            // Convert point from client to screen coordinates
            mousePt = Target.OwningControl!.PointToScreen(mousePt);
            DragMove(this, new PointEventArgs(mousePt));
        }
    }

    /// <summary>
    /// Raises the DragEnd event.
    /// </summary>
    /// <param name="mousePt">Mouse point at time of event.</param>
    protected virtual void OnDragEnd(Point mousePt)
    {
        _dragging = false;
        if (DragEnd != null)
        {
            // Convert point from client to screen coordinates
            mousePt = Target.OwningControl!.PointToScreen(mousePt);
            DragEnd(this, new PointEventArgs(mousePt));
        }
    }

    /// <summary>
    /// Raises the DragQuit event.
    /// </summary>
    protected virtual void OnDragQuit()
    {
        _dragging = false;
        DragQuit?.Invoke(this, EventArgs.Empty);
    }
    #endregion
}