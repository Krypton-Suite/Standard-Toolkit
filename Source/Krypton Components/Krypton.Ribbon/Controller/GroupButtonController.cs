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
/// Process mouse events for a ribbon group button.
/// </summary>
internal class GroupButtonController : GlobalId,
    IMouseController,
    ISourceController,
    IKeyController,
    IRibbonKeyTipTarget
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private readonly ViewDrawRibbonGroupButtonBackBorder _target;
    private NeedPaintHandler? _needPaint;
    private Rectangle _splitRectangle;
    private bool _rightButtonDown;
    private bool _previousMouseInSplit;
    private bool _fixedPressed;
    private bool _mouseOver;
    private bool _hasFocus;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when a click portion is clicked.
    /// </summary>
    public event EventHandler? Click;

    /// <summary>
    /// Occurs when the user right clicks the view.
    /// </summary>
    public event MouseEventHandler? ContextClick;

    /// <summary>
    /// Occurs when a drop-down portion is clicked.
    /// </summary>
    public event EventHandler? DropDown;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the GroupButtonController class.
    /// </summary>
    /// <param name="ribbon">Source control instance.</param>
    /// <param name="target">Target for state changes.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public GroupButtonController([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] ViewDrawRibbonGroupButtonBackBorder? target,
        [DisallowNull] NeedPaintHandler? needPaint)
    {
        Debug.Assert(ribbon is not null);
        Debug.Assert(target is not null);
        Debug.Assert(needPaint is not null);

        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));
        _target = target ?? throw new ArgumentNullException(nameof(target));
        NeedPaint = needPaint ?? throw new ArgumentNullException(nameof(needPaint));

        // Default other fields
        ButtonType = GroupButtonType.Push;
    }
    #endregion

    #region MouseInSplit
    /// <summary>
    /// Gets a value indicating if the mouse is inside the split rectangle.
    /// </summary>
    public bool MouseInSplit { get; private set; }

    #endregion

    #region SplitRectangle
    /// <summary>
    /// Gets and sets the rectangle for the split area.
    /// </summary>
    public Rectangle SplitRectangle
    {
        get => _splitRectangle;
        set => _splitRectangle = value;
    }
    #endregion

    #region ButtonType
    /// <summary>
    /// Gets and sets the type of button we are controlling.
    /// </summary>
    public GroupButtonType ButtonType { get; set; }

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
        if (!_fixedPressed)
        {
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
        // Check to ensure we are actually in mouse over state
        if (!_mouseOver)
        {
            _mouseOver = true;
        }

        // Track if the mouse is inside the split area
        if (ButtonType == GroupButtonType.Split)
        {
            MouseInSplit = _splitRectangle.Contains(pt);
        }

        // Update the visual state
        UpdateTargetState(pt);
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
            // Can only click if enabled
            case MouseButtons.Left when ClickOnDown(pt) && _target.Enabled:
            {
                Captured = true;

                // If already in fixed mode, then ignore mouse down
                if (!_fixedPressed)
                {
                    // Mouse is being pressed
                    UpdateTargetState(pt);

                    // Show the button as pressed, until told otherwise
                    _fixedPressed = true;

                    // Raise the appropriate event
                    switch (ButtonType)
                    {
                        case GroupButtonType.Split:
                            // Track if the mouse is inside the split area
                            if (ButtonType == GroupButtonType.Split)
                            {
                                MouseInSplit = _splitRectangle.Contains(pt);
                            }

                            if (_splitRectangle.Contains(pt))
                            {
                                OnDropDown(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));
                            }
                            else
                            {
                                OnClick(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));
                            }
                            break;
                        case GroupButtonType.DropDown:
                            OnDropDown(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));
                            break;
                        case GroupButtonType.Push:
                        case GroupButtonType.Check:
                        default:
                            OnClick(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));
                            break;
                    }
                }

                break;
            }
            case MouseButtons.Left:
                // Capturing mouse input
                Captured = true;

                // Update the visual state
                UpdateTargetState(pt);
                break;
            // Remember the user has pressed the right mouse button down
            case MouseButtons.Right:
                _rightButtonDown = true;
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
        if (Captured && !ClickOnDown(pt))
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
                        // Raise the appropriate event
                        switch (ButtonType)
                        {
                            case GroupButtonType.Split:
                                // Track if the mouse is inside the split area
                                if (ButtonType == GroupButtonType.Split)
                                {
                                    MouseInSplit = _splitRectangle.Contains(pt);
                                }

                                if (_splitRectangle.Contains(pt))
                                {
                                    OnDropDown(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));
                                }
                                else
                                {
                                    OnClick(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));
                                }
                                break;
                            case GroupButtonType.DropDown:
                                OnDropDown(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));
                                break;
                            case GroupButtonType.Push:
                            case GroupButtonType.Check:
                            default:
                                OnClick(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));
                                break;
                        }
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
        if (!_target.ContainsRecurse(next))
        {
            // Mouse is no longer over the target
            _mouseOver = false;

            // Update the visual state
            if (!_fixedPressed)
            {
                // If leaving the view then cannot be capturing mouse input anymore
                Captured = false;
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

    #region Focus Notifications
    /// <summary>
    /// Source control has got the focus.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    public virtual void GotFocus(Control c)
    {
        _hasFocus = true;
        UpdateTargetState(Point.Empty);
    }

    /// <summary>
    /// Source control has lost the focus.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    public virtual void LostFocus([DisallowNull] Control c)
    {
        _hasFocus = false;
        UpdateTargetState(Point.Empty);
    }
    #endregion

    #region Key Notifications
    /// <summary>
    /// Key has been pressed down.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="e">A KeyEventArgs that contains the event data.</param>
    public void KeyDown(Control c, KeyEventArgs e)
    {
        // Get the root control that owns the provided control
        c = _ribbon.GetControllerControl(c)!;

        switch (c)
        {
            case KryptonRibbon rib:
                KeyDownRibbon(rib, e);
                break;
            case VisualPopupGroup pop:
                KeyDownPopupGroup(pop, e);
                break;
            case VisualPopupMinimized min:
                KeyDownPopupMinimized(min, e);
                break;
        }
    }

    /// <summary>
    /// Key has been pressed.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
    public void KeyPress(Control c, KeyPressEventArgs e)
    {
    }

    /// <summary>
    /// Key has been released.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="e">A KeyEventArgs that contains the event data.</param>
    /// <returns>True if capturing input; otherwise false.</returns>
    public bool KeyUp(Control c, KeyEventArgs e) => false;

    #endregion

    #region KeyTipSelect
    /// <summary>
    /// Perform actual selection of the item.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon instance.</param>
    public void KeyTipSelect(KryptonRibbon ribbon)
    {
        // Generate appropriate event
        switch (ButtonType)
        {
            case GroupButtonType.Push:
            case GroupButtonType.Check:
                // Exit keyboard mode when you click the button spec
                ribbon.KillKeyboardMode();

                OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                break;
            case GroupButtonType.DropDown:
            case GroupButtonType.Split:
                // Exit the use of keyboard mode
                ribbon.KillKeyboardMode();

                // Pretend we have captured input and then fix state as pressed
                Captured = true;
                _fixedPressed = true;

                // Redraw to show the fixed state
                UpdateTargetState(Point.Empty);

                OnDropDown(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                break;
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
    /// Gets a value indicating if mouse input is being captured.
    /// </summary>
    protected bool Captured { get; set; }

    /// <summary>
    /// Set the correct visual state of the target.
    /// </summary>
    /// <param name="c">Owning control.</param>
    protected void UpdateTargetState(Control c)
    {
        if (c == null || c.IsDisposed)
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
                if (_fixedPressed || _target.ClientRectangle.Contains(pt))
                {
                    newState = PaletteState.Pressed;
                }
                else
                {
                    newState = PaletteState.Normal;
                }
            }
            else
            {
                // Only hot tracking, so show tracking only if mouse over the target or has focus
                if (_mouseOver || _hasFocus)
                {
                    newState = PaletteState.Tracking;

                    // We always show the button as being in the split when it has focus
                    if (_hasFocus)
                    {
                        MouseInSplit = true;
                    }
                }
                else
                {
                    newState = PaletteState.Normal;
                }
            }
        }

        // If state has changed
        if ((_target.ElementState != newState) ||
            (MouseInSplit != _previousMouseInSplit))
        {
            _target.ElementState = newState;
            _previousMouseInSplit = MouseInSplit;

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
    /// Raises the Click event.
    /// </summary>
    /// <param name="e">A MouseEventArgs containing the event data.</param>
    protected virtual void OnContextClick(MouseEventArgs e) => ContextClick?.Invoke(this, e);

    /// <summary>
    /// Raises the DropDown event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnDropDown(EventArgs e) => DropDown?.Invoke(_target, e);

    /// <summary>
    /// Raises the NeedPaint event.
    /// </summary>
    /// <param name="needLayout">Does the palette change require a layout.</param>
    protected virtual void OnNeedPaint(bool needLayout) => _needPaint?.Invoke(this, new NeedLayoutEventArgs(needLayout, _target.ClientRectangle));
    #endregion

    #region Implementation
    private void KeyDownRibbon(KryptonRibbon? ribbon, KeyEventArgs e)
    {
        ViewBase? newView = null;

        if (ribbon is null)
        {
            throw new ArgumentNullException(nameof(ribbon));
        }

        if (ribbon.TabsArea is null)
        {
            throw new NullReferenceException(GlobalStaticValues.PropertyCannotBeNull(nameof(ribbon.TabsArea)));
        }

        switch (e.KeyData)
        {
            case Keys.Tab | Keys.Shift:
            case Keys.Left:
                // Get the previous focus item for the currently selected page
                newView = ribbon.GroupsArea.ViewGroups.GetPreviousFocusItem(_target) ?? ribbon.TabsArea.LayoutTabs.GetViewForRibbonTab(ribbon.SelectedTab);

                // Got to the actual tab header
                break;
            case Keys.Tab:
            case Keys.Right:
                // Get the next focus item for the currently selected page
                newView = (ribbon.GroupsArea.ViewGroups.GetNextFocusItem(_target) ?? ribbon.TabsArea.ButtonSpecManager!.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Far)) ??
                          ribbon.TabsArea.ButtonSpecManager!.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Inherit);

                // Move across to any far defined buttons

                // Move across to any inherit defined buttons

                // Rotate around to application button
                if (newView == null)
                {
                    if (ribbon.TabsArea.LayoutAppButton.Visible)
                    {
                        newView = ribbon.TabsArea.LayoutAppButton.AppButton;
                    }
                    else if (ribbon.TabsArea.LayoutAppTab.Visible)
                    {
                        newView = ribbon.TabsArea.LayoutAppTab.AppTab;
                    }
                }
                break;
            case Keys.Space:
            case Keys.Enter:
                // Generate appropriate event
                switch (ButtonType)
                {
                    case GroupButtonType.Push:
                    case GroupButtonType.Check:
                        // Exit keyboard mode when you click the button spec
                        _ribbon.KillKeyboardMode();

                        OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                        break;
                    case GroupButtonType.DropDown:
                    case GroupButtonType.Split:
                        // Kill the key tips mode
                        _ribbon.KillKeyboardKeyTips();

                        // Pretend we have captured input and then fix state as pressed
                        _hasFocus = true;
                        Captured = true;
                        _fixedPressed = true;

                        // Redraw to show the fixed state
                        UpdateTargetState(Point.Empty);

                        OnDropDown(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                        break;
                }
                break;
        }

        // If we have a new view to focus and it is not ourself...
        if ((newView != null) && (newView != Target))
        {
            // If the new view is a tab then select that tab unless in minimized mode
            if (!ribbon.RealMinimizedMode && (newView is ViewDrawRibbonTab tab))
            {
                ribbon.SelectedTab = tab.RibbonTab;
            }

            // Finally we switch focus to new view
            ribbon.FocusView = newView;
        }
    }

    private void KeyDownPopupGroup(VisualPopupGroup popupGroup, KeyEventArgs e)
    {
        switch (e.KeyData)
        {
            case Keys.Tab | Keys.Shift:
            case Keys.Left:
                popupGroup.SetPreviousFocusItem();
                break;
            case Keys.Tab:
            case Keys.Right:
                popupGroup.SetNextFocusItem();
                break;
            case Keys.Space:
            case Keys.Enter:
                // Generate appropriate event
                switch (ButtonType)
                {
                    case GroupButtonType.Push:
                    case GroupButtonType.Check:
                        // Exit keyboard mode when you click the group button
                        _ribbon.KillKeyboardMode();

                        OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                        break;
                    case GroupButtonType.DropDown:
                    case GroupButtonType.Split:
                        // Kill the key tips mode
                        _ribbon.KillKeyboardKeyTips();

                        // Pretend we have captured input and then fix state as pressed
                        _hasFocus = true;
                        Captured = true;
                        _fixedPressed = true;

                        // Redraw to show the fixed state
                        UpdateTargetState(Point.Empty);

                        OnDropDown(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                        break;
                }
                break;
        }
    }

    private void KeyDownPopupMinimized(VisualPopupMinimized popupMinimized, KeyEventArgs e)
    {
        switch (e.KeyData)
        {
            case Keys.Tab | Keys.Shift:
            case Keys.Left:
                popupMinimized.SetPreviousFocusItem();
                break;
            case Keys.Tab:
            case Keys.Right:
                popupMinimized.SetNextFocusItem();
                break;
            case Keys.Space:
            case Keys.Enter:
                // Generate appropriate event
                switch (ButtonType)
                {
                    case GroupButtonType.Push:
                    case GroupButtonType.Check:
                        // Exit keyboard mode when you click the button spec
                        _ribbon.KillKeyboardMode();

                        OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                        break;
                    case GroupButtonType.DropDown:
                    case GroupButtonType.Split:
                        // Kill the key tips mode
                        _ribbon.KillKeyboardKeyTips();

                        // Pretend we have captured input and then fix state as pressed
                        _hasFocus = true;
                        Captured = true;
                        _fixedPressed = true;

                        // Redraw to show the fixed state
                        UpdateTargetState(Point.Empty);

                        OnDropDown(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                        break;
                }
                break;
        }
    }

    private bool ClickOnDown(Point pt) => ButtonType switch
    {
        GroupButtonType.DropDown => true,
        GroupButtonType.Split => _splitRectangle.Contains(pt),
        _ => false
    };
    #endregion
}