#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Provide button pressed when mouse down functionality.
/// </summary>
internal class LeftDownButtonController : GlobalId,
    IMouseController
{
    #region Instance Fields

    private bool _active;
    private bool _mouseOver;
    private bool _mouseDown;
    private readonly NeedPaintHandler _needPaint;
    private readonly Timer _updateTimer;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the button is pressed.
    /// </summary>
    public event MouseEventHandler? Click;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the LeftDownButtonController class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon instance.</param>
    /// <param name="target">Target for state changes.</param>
    /// <param name="needPaint">Delegate for notifying changes in display.</param>
    public LeftDownButtonController([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] ViewBase? target,
        [DisallowNull] NeedPaintHandler? needPaint)
    {
        Debug.Assert(ribbon is not null);
        Debug.Assert(target is not null);
        Debug.Assert(needPaint is not null);

        Ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));
        Target = target ?? throw new ArgumentNullException(nameof(target));
        _needPaint = needPaint ?? throw new ArgumentNullException(nameof(needPaint));

        _updateTimer = new Timer
        {
            Interval = 1
        };
        _updateTimer.Tick += OnUpdateTimer;
    }
    #endregion

    #region Ribbon
    /// <summary>
    /// Gets access to the owning ribbon instance.
    /// </summary>
    public KryptonRibbon Ribbon { get; }

    #endregion

    #region Target
    /// <summary>
    /// Gets access to the target view for this controller.
    /// </summary>
    public ViewBase Target { get; }

    #endregion

    #region HasFocus
    /// <summary>
    /// Gets and sets the focus flag.
    /// </summary>
    public bool HasFocus { get; set; }

    #endregion

    #region IsFixed
    /// <summary>
    /// Is the controller fixed in the pressed state.
    /// </summary>
    public bool IsFixed { get; private set; }

    #endregion

    #region SetFixed
    /// <summary>
    /// Fix the display of the button.
    /// </summary>
    public void SetFixed() =>
        // Show the button as pressed, until told otherwise
        IsFixed = true;
    #endregion

    #region RemoveFixed
    /// <summary>
    /// Remove the fixed pressed mode.
    /// </summary>
    public void RemoveFixed()
    {
        if (IsFixed)
        {
            // Mouse no longer considered pressed down
            _mouseDown = false;

            // No longer in fixed state mode
            IsFixed = false;

            // Update appearance to reflect current state
            _updateTimer.Start();
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

        // Get the form we are inside
        KryptonForm? ownerForm = Ribbon.FindKryptonForm();
        _active = ownerForm is { WindowActive: true } ||
                  VisualPopupManager.Singleton.IsTracking ||
                  Ribbon.InDesignMode ||
                  (CommonHelper.ActiveFloatingWindow != null);

        if (!IsFixed)
        {
            _updateTimer.Start();
        }
    }

    /// <summary>
    /// Mouse has moved inside the view.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="pt">Mouse position relative to control.</param>
    public virtual void MouseMove(Control c, Point pt)
    {
        if (!IsFixed)
        {
            // Oops, we should really be in mouse over state
            if (!_mouseOver)
            {
                _mouseOver = true;
                _updateTimer.Start();
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
        // Is pressing down with the mouse when we are always active for showing changes
        _active = true;

        // Only interested in left mouse pressing down
        if (button == MouseButtons.Left)
        {
            _mouseDown = true;

            // If already in fixed mode, then ignore mouse down
            if (!IsFixed && Ribbon.Enabled)
            {
                // Mouse is being pressed
                UpdateTargetState();

                // Fix the button to be Displayed as pressed
                SetFixed();

                // Generate a click event
                OnClick(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));
            }
        }

        return true;
    }

    /// <summary>
    /// Mouse button has been released in the view.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="pt">Mouse position relative to control.</param>
    /// <param name="button">Mouse button released.</param>
    public virtual void MouseUp(Control c, Point pt, MouseButtons button)
    {
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

        if (!IsFixed)
        {
            _updateTimer.Start();
        }
    }

    /// <summary>
    /// Left mouse button double click.
    /// </summary>
    /// <param name="pt">Mouse position relative to control.</param>
    public virtual void DoubleClick(Point pt)
    {
    }

    /// <summary>
    /// Should the left mouse down be ignored when present on a visual form border area.
    /// </summary>
    public virtual bool IgnoreVisualFormLeftButtonDown => false;

    #endregion

    #region Protected
    /// <summary>
    /// Set the correct visual state of the target.
    /// </summary>
    protected virtual void UpdateTargetState()
    {
        // By default, the button is in the normal state
        var newState = PaletteState.Normal;

        // Only allow another state if the ribbon is enabled
        if (Ribbon.Enabled)
        {
            // Are we showing with the fixed state?
            if (IsFixed)
            {
                newState = PaletteState.Pressed;
            }
            else
            {
                // If being pressed
                if (_mouseDown)
                {
                    newState = PaletteState.Pressed;
                }
                else if (_mouseOver && _active)
                {
                    newState = PaletteState.Tracking;
                }
            }
        }

        // Only interested in changes of state
        if (Target.ElementState != newState)
        {
            // Update state
            Target.ElementState = newState;

            // Need to repaint just the target client area
            OnNeedPaint(false, Target.ClientRectangle);

            // Get the repaint to happen immediately
            if (!Ribbon.InKeyboardMode)
            {
                Application.DoEvents();
            }
        }
    }

    /// <summary>
    /// Raises the NeedPaint event.
    /// </summary>
    /// <param name="needLayout">Does the palette change require a layout.</param>
    /// <param name="invalidRect">Client area to be invalidated.</param>
    protected virtual void OnNeedPaint(bool needLayout,
        Rectangle invalidRect) => _needPaint.Invoke(this, new NeedLayoutEventArgs(needLayout, invalidRect));

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="e">A MouseEventArgs containing the event data.</param>
    protected virtual void OnClick(MouseEventArgs e) => Click?.Invoke(this, e);
    #endregion

    #region Implementation
    private void OnUpdateTimer(object? sender, EventArgs e)
    {
        _updateTimer.Stop();
        UpdateTargetState();
    }
    #endregion
}