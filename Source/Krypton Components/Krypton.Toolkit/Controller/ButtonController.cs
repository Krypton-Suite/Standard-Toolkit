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

namespace Krypton.Toolkit;

/// <summary>
/// Process mouse events for a standard button.
/// </summary>
public class ButtonController : GlobalId,
    IMouseController,
    IKeyController,
    ISourceController
{
    #region Instance Fields

    private bool _mouseOver;
    private bool _fixedPressed;
    private bool _inSplitRectangle;
    private bool _dragging;
    private bool _draggingAttempt;
    private bool _preDragOffset;
    private NeedPaintHandler? _needPaint;
    private System.Windows.Forms.Timer? _repeatTimer, _t;
    private Rectangle _dragRect;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the mouse is used to left click the target.
    /// </summary>
    public event MouseEventHandler? Click;

    /// <summary>
    /// Occurs when the mouse is used to right click the target.
    /// </summary>
    public event MouseEventHandler? RightClick;

    /// <summary>
    /// Occurs when the mouse is used to left select the target.
    /// </summary>
    public event MouseEventHandler? MouseSelect;

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

    /// <summary>
    /// Occurs when the drag rectangle for the button is required.
    /// </summary>
    public event EventHandler<ButtonDragRectangleEventArgs>? ButtonDragRectangle;

    /// <summary>
    /// Occurs when the dragging inside the button drag rectangle.
    /// </summary>
    public event EventHandler<ButtonDragOffsetEventArgs>? ButtonDragOffset;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonController class.
    /// </summary>
    /// <param name="target">Target for state changes.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ButtonController(ViewBase target,
        NeedPaintHandler needPaint)
    {

        // Debug.Assert() causes the null assignment warning.
        // Suppressed by the null forgiving operator
        Debug.Assert(target is not null);

        MousePoint = CommonHelper.NullPoint;
        SplitRectangle = CommonHelper.NullRectangle;
        _inSplitRectangle = false;
        AllowDragging = false;
        _dragging = false;
        ClickOnDown = false;
        Target = target!;
        Repeat = false;
        NeedPaint = needPaint;
    }
    #endregion

    #region Tag
    /// <summary>
    /// Gets and sets the user data associated with the controller.
    /// </summary>
    public object? Tag { get; set; }

    #endregion

    #region BecomesFixed
    /// <summary>
    /// Gets and sets if the button becomes fixed in pressed appearance when pressed.
    /// </summary>
    public bool BecomesFixed { get; set; }

    #endregion

    #region BecomesRightFixed
    /// <summary>
    /// Gets and sets if the button becomes fixed in pressed appearance when pressed.
    /// </summary>
    public bool BecomesRightFixed { get; set; }

    #endregion

    #region RemoveFixed
    /// <summary>
    /// Remove the fixed pressed mode.
    /// </summary>
    public void RemoveFixed()
    {
        if (_fixedPressed)
        {
            // Mouse no longer considered pressed down
            Captured = false;

            // No longer in fixed state mode
            _fixedPressed = false;

            // Update the visual state
            UpdateTargetState(Point.Empty);
        }
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

    #region ClearDragRect
    /// <summary>
    /// Reset the dragging rect to prevent any dragging starting.
    /// </summary>
    public void ClearDragRect() => _dragRect = Rectangle.Empty;
    #endregion

    #region ClickOnDown
    /// <summary>
    /// Gets and sets if the press down should cause the click.
    /// </summary>
    public bool ClickOnDown { get; set; }

    #endregion

    #region SplitRectangle
    /// <summary>
    /// Gets and sets the area of the button which is split.
    /// </summary>
    public Rectangle SplitRectangle { get; set; }

    #endregion

    #region NonClientAsNormal
    /// <summary>
    /// Gets and sets the drawing of a non client mouse position when pressed as normal.
    /// </summary>
    public bool NonClientAsNormal { get; set; }

    #endregion

    #region Repeat
    /// <summary>
    /// Gets and sets the need for repeat clicks.
    /// </summary>
    public bool Repeat { get; set; }

    #endregion

    #region Mouse Notifications
    /// <summary>
    /// Mouse has entered the view.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    public virtual void MouseEnter(Control c)
    {
        // Is the controller allowed to track/click
        if (IsOperating)
        {
            // Mouse is over the target
            _mouseOver = true;

            // Update the visual state
            UpdateTargetState(c);
        }
    }

    /// <summary>
    /// Mouse has moved inside the view.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="pt">Mouse position relative to control.</param>
    public virtual void MouseMove(Control c, Point pt)
    {
        // Is the controller allowed to track/click
        if (IsOperating)
        {
            // Track the mouse point
            MousePoint = pt;

            // Update the visual state
            UpdateTargetState(pt);

            // If captured then we might want to handle dragging
            if (Captured)
            {
                if (AllowDragging)
                {
                    if (_dragging)
                    {
                        OnDragMove(MousePoint);
                    }
                    else if (!_dragRect.IsEmpty && !_dragRect.Contains(MousePoint))
                    {
                        if (!_draggingAttempt)
                        {
                            _draggingAttempt = true;
                            Point targetOrigin = Target.ClientLocation;
                            var offset = new Point(MousePoint.X - targetOrigin.X, MousePoint.Y - targetOrigin.Y);
                            OnDragStart(MousePoint, offset, c);
                        }
                    }
                }

                if (!_dragging && !_dragRect.IsEmpty && _preDragOffset)
                {
                    var args = new ButtonDragOffsetEventArgs(pt);
                    OnButtonDragOffset(args);
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
        // Is the controller allowed to track/click
        if (IsOperating)
        {
            // If the button is not enabled then we do nothing on a mouse down
            if (Target.Enabled)
            {
                switch (button)
                {
                    // Only interested in left mouse pressing down
                    case MouseButtons.Left:
                    {
                        // Capturing mouse input
                        Captured = true;
                        _draggingAttempt = false;

                        // Use event to discover the rectangle that causes dragging to begin
                        var args = new ButtonDragRectangleEventArgs(pt);
                        OnButtonDragRectangle(args);
                        _dragRect = args.DragRect;
                        _preDragOffset = args.PreDragOffset;

                        if (!_fixedPressed)
                        {
                            // Update the visual state
                            UpdateTargetState(pt);

                            // Do we become fixed in the pressed state until RemoveFixed is called?
                            if (BecomesFixed)
                            {
                                _fixedPressed = true;
                            }

                            // Indicate that the mouse wants to select the elment
                            OnMouseSelect(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));

                            // Generate a click event if we generate click on mouse down
                            if (ClickOnDown)
                            {
                                OnClick(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));

                                // If we need to perform click repeats then use a timer...
                                if (Repeat)
                                {
                                    _repeatTimer = new System.Windows.Forms.Timer
                                    {
                                        Interval = SystemInformation.DoubleClickTime
                                    };
                                    _repeatTimer.Tick += OnRepeatTimer;
                                    _repeatTimer.Start();
                                }
                            }
                        }

                        break;
                    }
                    case MouseButtons.Right:
                    {
                        if (!_fixedPressed)
                        {
                            // Do we become fixed in the pressed state until RemoveFixed is called?
                            if (BecomesRightFixed)
                            {
                                _fixedPressed = true;
                            }

                            // Indicate the right mouse was used on the button
                            OnRightClick(new MouseEventArgs(MouseButtons.Right, 1, pt.X, pt.Y, 0));
                        }

                        break;
                    }
                }
            }
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
        // Is the controller allowed to track/click
        if (IsOperating)
        {
            // If the button is not enabled then we do nothing on a mouse down
            if (Target.Enabled)
            {
                // Remove the repeat timer
                if (_repeatTimer != null)
                {
                    _repeatTimer.Stop();
                    _repeatTimer.Dispose();
                    _repeatTimer = null;
                }

                // If the mouse is currently captured
                if (Captured)
                {
                    // Not capturing mouse input anymore
                    Captured = false;

                    // Only interested in left mouse being released
                    if (button == MouseButtons.Left)
                    {
                        if (_dragging)
                        {
                            OnDragEnd(pt);
                        }

                        // Only if the button is still pressed, do we generate a click
                        if (Target.ElementState is PaletteState.Pressed or (PaletteState.Pressed | PaletteState.Checked))
                        {
                            if (!_fixedPressed)
                            {
                                // Move back to hot tracking state, we have to do this
                                // before the click is generated because the click processing
                                // might change focus and so cause the MouseLeave to be
                                // called and change the state. If this was after the click
                                // then it would overwrite and lose that leave state change.
                                Target.ElementState = PaletteState.Tracking;
                            }

                            // Can only click if enabled
                            if (Target.Enabled && !ClickOnDown)
                            {
                                // Generate a click event
                                OnClick(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));
                            }
                        }

                        // Repaint to reflect new state
                        OnNeedPaint(true);
                    }
                    else
                    {
                        if (_dragging)
                        {
                            OnDragQuit();
                        }

                        // Update the visual state
                        UpdateTargetState(pt);
                    }
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
        // Is the controller allowed to track/click
        if (IsOperating)
        {
            // Only if mouse is leaving all the children monitored by controller.
            if (!ViewIsPartOfButton(next))
            {
                // Remove the repeat timer
                if (_repeatTimer != null)
                {
                    _repeatTimer.Stop();
                    _repeatTimer.Dispose();
                    _repeatTimer = null;
                }

                // Mouse is no longer over the target
                _mouseOver = false;

                // Do we need to update the visual state
                if (!_fixedPressed)
                {
                    // Not tracking the mouse means a null value
                    MousePoint = CommonHelper.NullPoint;

                    // If leaving the view then cannot be capturing mouse input anymore
                    Captured = false;

                    // End any current dragging operation
                    if (_dragging)
                    {
                        OnDragQuit();
                    }

                    // Update the visual state
                    UpdateTargetState(c);
                }
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
    /// <exception cref="ArgumentNullException"></exception>
    public virtual void KeyDown([DisallowNull] Control c, [DisallowNull] KeyEventArgs e)
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

        if (e.KeyCode == Keys.Space)
        {
            // Enter the captured mode and pretend mouse is over area
            Captured = true;
            _mouseOver = true;

            // Do we become fixed in the pressed state until RemoveFixed is called?
            if (BecomesFixed)
            {
                _fixedPressed = true;
            }

            // Update target to reflect new state
            Target.ElementState = PaletteState.Pressed;

            // Redraw to show the change in visual state
            OnNeedPaint(true);
        }
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
    /// <exception cref="ArgumentNullException"></exception>
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
        if (e.KeyCode is Keys.Escape or Keys.Space)
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
                _mouseOver = Target.ClientRectangle.Contains(c.PointToClient(Control.MousePosition));

                if (e.KeyCode == Keys.Space)
                {
                    // Can only click if enabled
                    if (Target.Enabled)
                    {
                        // Generate a click event
                        OnClick(new MouseEventArgs(MouseButtons.Left, 1, -1, -1, 0));
                    }
                }

                // Update the visual state
                UpdateTargetState(c);
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
    /// <exception cref="ArgumentNullException"></exception>
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
            _mouseOver = Target.ClientRectangle.Contains(c.PointToClient(Control.MousePosition));

            // Update the visual state
            UpdateTargetState(c);
        }
    }
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
    /// Get a value indicating if the controller is operating
    /// </summary>
    protected virtual bool IsOperating
    {
        get => true;
        set { }
    }

    /// <summary>
    /// Gets a value indicating if the state is pressed only when over button.
    /// </summary>
    protected virtual bool IsOnlyPressedWhenOver
    {
        get => true;
        set { }
    }

    /// <summary>
    /// Gets a value indicating if mouse input is being captured.
    /// </summary>
    protected bool Captured { get; set; }

    /// <summary>
    /// Discovers if the provided view is part of the button.
    /// </summary>
    /// <param name="next">View to investigate.</param>
    /// <returns>True is part of button; otherwise false.</returns>
    protected virtual bool ViewIsPartOfButton(ViewBase? next) => Target.ContainsRecurse(next);

    /// <summary>
    /// Set the correct visual state of the target.
    /// </summary>
    /// <param name="c">Owning control.</param>
    protected void UpdateTargetState(Control c)
    {
        // Check we have a valid control to convert coordinates against
        if (c is { IsDisposed: false })
        {
            // Ensure control is inside a visible top level form
            Form? f = c.FindForm();
            if (f is { Visible: true })
            {
                UpdateTargetState(c.PointToClient(Control.MousePosition));
                return;
            }
        }

        UpdateTargetState(new Point(int.MaxValue, int.MaxValue));
    }

    /// <summary>
    /// Set the correct visual state of the target.
    /// </summary>
    /// <param name="pt">Mouse point.</param>
    protected virtual void UpdateTargetState(Point pt)
    {
        // By default, the button is in the normal state
        PaletteState newState;

        // If the button is disabled then show as disabled
        if (!Target.Enabled)
        {
            newState = PaletteState.Disabled;
        }
        else
        {
            if (_fixedPressed)
            {
                newState = PaletteState.Pressed;
            }
            else
            {
                // If capturing input....
                if (Captured)
                {
                    // Do we show the button as pressed only when over the button?
                    if (IsOnlyPressedWhenOver)
                    {
                        if (Target.ClientRectangle.Contains(pt))
                        {
                            newState = PaletteState.Pressed;
                        }
                        else
                        {
                            newState = NonClientAsNormal ? PaletteState.Normal : PaletteState.Tracking;
                        }
                    }
                    else
                    {
                        // Always show the button pressed, even when not over the button itself
                        newState = PaletteState.Pressed;
                    }
                }
                else
                {
                    // Only hot tracking, so show tracking only if mouse over the target 
                    newState = _mouseOver ? PaletteState.Tracking : PaletteState.Normal;
                }
            }
        }

        // If state has changed or change in (inside split area)
        var inSplitRectangle = SplitRectangle.Contains(pt);
        if ((Target.ElementState != newState) || (inSplitRectangle != _inSplitRectangle))
        {
            // Update if the point is inside the split rectangle
            _inSplitRectangle = inSplitRectangle;

            // Update target to reflect new state
            Target.ElementState = newState;

            // Redraw to show the change in visual state
            OnNeedPaint(true);
        }
    }

    /// <summary>
    /// Raises the ButtonDragRectangle event.
    /// </summary>
    /// <param name="e">An ButtonDragRectangleEventArgs containing the event args.</param>
    protected virtual void OnButtonDragRectangle(ButtonDragRectangleEventArgs e) => ButtonDragRectangle?.Invoke(this, e);

    /// <summary>
    /// Raises the ButtonDragOffset event.
    /// </summary>
    /// <param name="e">An ButtonDragOffsetEventArgs containing the event args.</param>
    protected virtual void OnButtonDragOffset(ButtonDragOffsetEventArgs e) => ButtonDragOffset?.Invoke(this, e);

    /// <summary>
    /// Raises the DragStart event.
    /// </summary>
    /// <param name="mousePt">Mouse point at time of event.</param>
    /// <param name="offset">Offset compared to target.</param>
    /// <param name="c">Control that is source of drag start.</param>
    protected virtual void OnDragStart(Point mousePt, Point offset, Control c)
    {
        // Convert point from client to screen coordinates
        if (Target.OwningControl != null)
        {
            mousePt = Target.OwningControl.PointToScreen(mousePt);
        }
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
            if (Target.OwningControl != null)
            {
                mousePt = Target.OwningControl.PointToScreen(mousePt);
            }
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
            if (Target.OwningControl != null)
            {
                mousePt = Target.OwningControl.PointToScreen(mousePt);
            }
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

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="e">A MouseEventArgs containing the event data.</param>
    protected virtual void OnClick(MouseEventArgs e) => Click?.Invoke(Target, e);

    /// <summary>
    /// Raises the RightClick event.
    /// </summary>
    /// <param name="e">A MouseEventArgs containing the event data.</param>
    protected virtual void OnRightClick(MouseEventArgs e) => RightClick?.Invoke(Target, e);

    /// <summary>
    /// Raises the MouseSelect event.
    /// </summary>
    /// <param name="e">A MouseEventArgs containing the event data.</param>
    protected virtual void OnMouseSelect(MouseEventArgs e) => MouseSelect?.Invoke(Target, e);

    /// <summary>
    /// Raises the NeedPaint event.
    /// </summary>
    /// <param name="needLayout">Does the palette change require a layout.</param>
    protected virtual void OnNeedPaint(bool needLayout) => _needPaint?.Invoke(this, new NeedLayoutEventArgs(needLayout, Target.ClientRectangle));

    #endregion

    #region Implementation
    private void OnRepeatTimer(object? sender, EventArgs e)
    {
        // Modify subsequent repeat timing
        _t = sender as System.Windows.Forms.Timer ?? throw new ArgumentNullException(nameof(sender));
        _t.Interval = Math.Max(SystemInformation.DoubleClickTime / 4, 100);
        OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
    }
    #endregion
}