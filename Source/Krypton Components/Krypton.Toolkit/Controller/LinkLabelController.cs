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
/// Process mouse events for a link label control.
/// </summary>
public class LinkLabelController : GlobalId,
    IMouseController,
    IKeyController,
    ISourceController
{
    #region Instance Fields

    private bool _mouseOver;
    private DateTime _clickTime;
    private readonly ViewDrawContent _target;
    private readonly IPaletteContent _paletteDisabled;
    private readonly IPaletteContent _paletteNormal;
    private readonly IPaletteContent _paletteTracking;
    private readonly IPaletteContent _palettePressed;
    private readonly PaletteContentInheritOverride _pressed;
    private NeedPaintHandler? _needPaint;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the mouse is used to left click the target.
    /// </summary>
    public event MouseEventHandler? Click;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the LinkLabelController class.
    /// </summary>
    /// <param name="target">Target for state changes.</param>
    /// <param name="paletteDisabled">Palette to use in the disabled state.</param>
    /// <param name="paletteNormal">Palette to use in the normal state.</param>
    /// <param name="paletteTracking">Palette to use in the tracking state.</param>
    /// <param name="palettePressed">Palette to use in the pressed state.</param>
    /// <param name="pressed">Override to update with the pressed state.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public LinkLabelController(ViewDrawContent target,
        IPaletteContent paletteDisabled,
        IPaletteContent paletteNormal,
        IPaletteContent paletteTracking,
        IPaletteContent palettePressed,
        PaletteContentInheritOverride pressed,
        NeedPaintHandler? needPaint)
    {
        // Debug.Assert() causes the null assignment warning.
        // Suppressed by the null forgiving operator
        Debug.Assert(target is not null);

        // Remember target for state changes
        _target = target!;
        _paletteDisabled = paletteDisabled;
        _paletteNormal = paletteNormal;
        _paletteTracking = paletteTracking;
        _palettePressed = palettePressed;
        _pressed = pressed;
        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Default other properties
        _clickTime = new DateTime();
    }
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
            // Cursor depends on enabled state of the view element
            if (_target.Enabled)
            {
                c.Cursor = Cursors.Hand;
            }

            // Update the visual state
            UpdateTargetState(c);
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
            // Only interested in left mouse pressing down
            if (button == MouseButtons.Left)
            {
                // Capturing mouse input
                Captured = true;

                // Update the visual state
                UpdateTargetState(c);

                // Take the focus if allowed
                if (c.CanFocus)
                {
                    c.Focus();
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
            // If the mouse is currently captured
            if (Captured)
            {
                // Not capturing mouse input anymore
                Captured = false;

                // Only interested in left mouse being released
                if (button == MouseButtons.Left)
                {
                    // Only if the button is still pressed, do we generate a click
                    if (_target.ElementState == PaletteState.Pressed)
                    {
                        // Move back to hot tracking state, we have to do this
                        // before the click is generated because the click processing
                        // might change focus and so cause the MouseLeave to be
                        // called and change the state. If this was after the click
                        // then it would overwrite and lose that leave state change.
                        _target.ElementState = PaletteState.Tracking;

                        // Can only click if enabled
                        if (_target.Enabled)
                        {
                            // Generate a click event
                            OnClick(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));
                        }
                    }

                    // Update the palette to reflect new state
                    UpdateTargetPalette();
                }
                else
                {
                    // Update the visual state
                    UpdateTargetState(c);
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
            if (!_target.ContainsRecurse(next))
            {
                // Mouse is no longer over the target
                _mouseOver = false;

                // If leaving the view then cannot be capturing mouse input anymore
                Captured = false;

                // Update the visual state
                UpdateTargetState(c);
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

        // Pressing the enter key is the same as clicking the label
        if (e.KeyCode == Keys.Enter)
        {
            // Generate a click event and set mouse to top left of the control
            OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
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
    /// <returns>True if capturing input; otherwise false.</returns>
    public virtual bool KeyUp(Control c, KeyEventArgs e) => Captured;

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
        // If we are capturing mouse input
        if (Captured)
        {
            // Release the mouse capture
            c.Capture = false;
            Captured = false;

            // Recalculate if the mouse is over the button area
            _mouseOver = _target.ClientRectangle.Contains(c.PointToClient(Control.MousePosition));

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
    public ViewBase Target => _target;

    /// <summary>
    /// Set the correct visual state of the target.
    /// </summary>
    /// <param name="c">Control that controller is operating within.</param>
    public void Update(Control c) => UpdateTargetState(c);

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
    /// Set the target with correct state and palette.
    /// </summary>
    /// <param name="pt">Mouse point.</param>
    protected void UpdateTargetState(Point pt)
    {
        // By default, the button is in the normal state
        PaletteState newState;

        // If the button is disabled then show as disabled
        if (!_target.Enabled)
        {
            newState = PaletteState.Disabled;
        }
        else
        {
            if (Captured)
            {
                newState = _target.ClientRectangle.Contains(pt) ? PaletteState.Pressed : PaletteState.Tracking;
            }
            else
            {
                // Only hot tracking, so show tracking only if mouse over the target 
                newState = _mouseOver ? PaletteState.Tracking : PaletteState.Normal;
            }
        }

        // If state has changed or using a fixed view, then update state
        if ((_target.ElementState != newState) || _target.IsFixed)
        {
            // Update target to reflect new state
            _target.ElementState = newState;

            // Update the palette to reflect new state
            UpdateTargetPalette();
        }
    }

    /// <summary>
    /// Update the palette to match the target state.
    /// </summary>
    protected virtual void UpdateTargetPalette()
    {
        // Should the pressed override be applied?
        _pressed.Apply = _target.State == PaletteState.Pressed;

        switch (_target.State)
        {
            case PaletteState.Disabled:
                _target.SetPalette(_paletteDisabled);
                break;
            case PaletteState.Normal:
                _target.SetPalette(_paletteNormal);
                break;
            case PaletteState.Tracking:
                _target.SetPalette(_paletteTracking);
                break;
            case PaletteState.Pressed:
                _target.SetPalette(_palettePressed);
                break;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(_target.State.ToString());
                break;
        }

        // Redraw to show the change in visual state
        OnNeedPaint(true);
    }

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="e">A MouseEventArgs containing the event data.</param>
    protected virtual void OnClick(MouseEventArgs e)
    {
        // Find how long since the last click occurred
        TimeSpan clickInterval = DateTime.Now - _clickTime;

        // If less than the double click interval then ignore
        if (SystemInformation.DoubleClickTime < clickInterval.TotalMilliseconds)
        {
            // Cache time of click generation
            _clickTime = DateTime.Now;

            Click?.Invoke(_target, e);
        }
    }

    /// <summary>
    /// Raises the NeedPaint event.
    /// </summary>
    /// <param name="needLayout">Does the palette change require a layout.</param>
    protected virtual void OnNeedPaint(bool needLayout) => _needPaint?.Invoke(this, new NeedLayoutEventArgs(needLayout));

    #endregion
}