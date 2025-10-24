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

// ReSharper disable RedundantNullableFlowAttribute
namespace Krypton.Navigator;

/// <summary>
/// Process mouse events for the outlook mini button.
/// </summary>
internal class OutlookMiniController : GlobalId,
    IMouseController,
    IKeyController
{
    #region Instance Fields
    private NeedPaintHandler? _needPaint;
    private readonly ViewBase _target;
    private bool _fixedTracking;
    private bool _mouseOver;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when a click portion is clicked.
    /// </summary>
    public event EventHandler? Click;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the OutlookMiniController class.
    /// </summary>
    /// <param name="target">Target for state changes.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public OutlookMiniController(ViewBase target,
        [DisallowNull] NeedPaintHandler needPaint)
    {
        Debug.Assert(needPaint != null);

        _target = target;
        NeedPaint = needPaint;
    }
    #endregion

    #region RemoveFixed
    /// <summary>
    /// Remove the fixed tracking mode.
    /// </summary>
    public void RemoveFixed()
    {
        if (_fixedTracking)
        {
            // Mouse no longer considered pressed down
            Captured = false;

            // No longer in fixed state mode
            _fixedTracking = false;

            // Update the visual state
            UpdateTargetState(Point.Empty);
        }
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
        if (!_fixedTracking)
        {
            UpdateTargetState(c);
        }
    }

    /// <summary>
    /// Mouse has moved inside the view.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="pt">Mouse position relative to control.</param>
    public virtual void MouseMove(Control c, Point pt) =>
        // Update the visual state
        UpdateTargetState(pt);

    /// <summary>
    /// Mouse button has been pressed in the view.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="pt">Mouse position relative to control.</param>
    /// <param name="button">Mouse button pressed down.</param>
    /// <returns>True if capturing input; otherwise false.</returns>
    public virtual bool MouseDown(Control c, Point pt, MouseButtons button)
    {
        // Only interested in left mouse pressing down
        if (button == MouseButtons.Left)
        {
            // Capturing mouse input
            Captured = true;

            // Update the visual state
            UpdateTargetState(pt);
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
                        // Show as tracking until told otherwise
                        _fixedTracking = true;

                        // Generate actual click event
                        OnClick(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));
                    }
                }

                // Repaint to reflect new state
                OnNeedPaint(false);
            }
            else
            {
                // Update the visual state
                UpdateTargetState(pt);
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
        if (!_fixedTracking)
        {
            // If leaving the view then cannot be capturing mouse input anymore
            Captured = false;

            UpdateTargetState(c);
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

            // Update target to reflect new state
            _target.ElementState = PaletteState.Pressed;

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

                // Recalculate if the mouse is over the button area
                _mouseOver = _target.ClientRectangle.Contains(c.PointToClient(Control.MousePosition));

                if (e.KeyCode == Keys.Space)
                {
                    // Can only click if enabled
                    if (_target.Enabled)
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
    /// Fires the NeedPaint event.
    /// </summary>
    /// <param name="needLayout">Does the palette change require a layout.</param>
    public void PerformNeedPaint(bool needLayout) => OnNeedPaint(needLayout);
    #endregion

    #region Protected
    /// <summary>
    /// Gets a value indicating if mouse input is being captured.
    /// </summary>
    protected bool Captured { get; set; }

    /// <summary>
    /// Set the correct visual state of the target.
    /// </summary>
    /// <param name="c">Owning control.</param>
    protected void UpdateTargetState(Control? c)
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
        // By default, the button is in the normal state
        PaletteState newState;

        // When disabled the button itself is shown as normal, the 
        // content is expected to draw itself as disabled though
        if (!_target.Enabled)
        {
            newState = PaletteState.Normal;
        }
        else
        {
            // If capturing input....
            if (Captured)
            {
                newState = _target.ClientRectangle.Contains(pt) ? PaletteState.Pressed : PaletteState.Normal;
            }
            else
            {
                // Only hot tracking, so show tracking only if mouse over the target or has focus
                newState = (_mouseOver || _fixedTracking) ? PaletteState.Tracking : PaletteState.Normal;
            }
        }

        // If state has changed
        if (_target.ElementState != newState)
        {
            _target.ElementState = newState;

            // Redraw to show the change in visual state
            OnNeedPaint(false);
        }
    }

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnClick(EventArgs e) => Click?.Invoke(_target, e);

    /// <summary>
    /// Raises the NeedPaint event.
    /// </summary>
    /// <param name="needLayout">Does the palette change require a layout.</param>
    protected virtual void OnNeedPaint(bool needLayout) => _needPaint?.Invoke(this, new NeedLayoutEventArgs(needLayout, _target.ClientRectangle));

    #endregion
}