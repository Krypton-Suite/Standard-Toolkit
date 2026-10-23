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
/// Provide button pressed when mouse released functionality.
/// </summary>
internal class LeftUpButtonController : GlobalId,
    IMouseController
{
    #region Instance Fields

    private bool _active;
    private bool _mouseOver;
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
    /// Initialize a new instance of the LeftUpButtonController class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon instance.</param>
    /// <param name="target">Target for state changes.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public LeftUpButtonController([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] ViewBase? target,
        [DisallowNull] NeedPaintHandler? needPaint)
    {
        Debug.Assert(ribbon is not null);
        Debug.Assert(target is not null);

        // Remember target for state changes
        Ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));
        Target = target ?? throw new ArgumentNullException(nameof(target));

        // Store the provided paint notification delegate
        NeedPaint = needPaint ?? throw new ArgumentNullException(nameof(needPaint));
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

        // Update the visual state
        UpdateTargetState(c);
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
        // Is pressing down with the mouse when we are always active for showing changes
        _active = true;

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
        // If the mouse is currently captured
        if (Captured)
        {
            // Not capturing mouse input anymore
            Captured = false;

            // Only interested in left mouse being released
            if (button == MouseButtons.Left)
            {
                // Only if the button is still pressed, do we generate a click
                if (Target.ElementState == PaletteState.Pressed)
                {
                    // Move back to hot tracking state, we have to do this
                    // before the click is generated because the click processing
                    // might change focus and so cause the MouseLeave to be
                    // called and change the state. If this was after the click
                    // then it would overwrite and lose that leave state change.
                    Target.ElementState = PaletteState.Tracking;

                    // Can only click if enabled
                    if (Target.Enabled)
                    {
                        // Generate a click event
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
        // Only if mouse is leaving all the children monitored by controller.
        if (!Target.ContainsRecurse(next))
        {
            // Mouse is no longer over the target
            _mouseOver = false;

            // If leaving the view then cannot be capturing mouse input anymore
            Captured = false;

            // Update the visual state
            UpdateTargetState(c);
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
    #endregion

    #region Protected
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
            // If capturing input....
            if (Captured)
            {
                // Do we show the button as pressed only when over the button?
                if (IsOnlyPressedWhenOver)
                {
                    newState = Target.ClientRectangle.Contains(pt) ? PaletteState.Pressed : PaletteState.Normal;
                }
                else
                {
                    // Always show the button pressed, even when not over the button itself
                    newState = PaletteState.Pressed;
                }
            }
            else
            {
                // If the mouse is over us and we are inside an active form then
                // show as hot tracking, otherwise stick to just the normal appearance
                if (_mouseOver && _active)
                {
                    newState = PaletteState.Tracking;
                }
                else
                {
                    newState = PaletteState.Normal;
                }
            }
        }

        // If state has changed
        if (Target.ElementState != newState)
        {
            // Update target to reflect new state
            Target.ElementState = newState;

            // Redraw to show the change in visual state
            OnNeedPaint(false);
        }
    }

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="e">A MouseEventArgs containing the event data.</param>
    protected virtual void OnClick(MouseEventArgs e) => Click?.Invoke(Target, e);

    /// <summary>
    /// Raises the NeedPaint event.
    /// </summary>
    /// <param name="needLayout">Does the palette change require a layout.</param>
    protected virtual void OnNeedPaint(bool needLayout) => _needPaint?.Invoke(this, new NeedLayoutEventArgs(needLayout));
    #endregion
}