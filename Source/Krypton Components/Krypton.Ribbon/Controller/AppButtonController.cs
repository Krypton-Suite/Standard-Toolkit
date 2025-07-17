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
/// Provide application button functionality.
/// </summary>
internal class AppButtonController : GlobalId,
    IMouseController,
    ISourceController,
    IKeyController,
    IRibbonKeyTipTarget
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private bool _mouseOver;
    private bool _mouseDown;
    private bool _fixedPressed;
    private bool _hasFocus;
    private readonly Timer _updateTimer;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the button is pressed.
    /// </summary>
    public event EventHandler? Click;

    /// <summary>
    /// Occurs when the button is released.
    /// </summary>
    public event EventHandler? MouseReleased;

    /// <summary>
    /// Occurs when a change in button state requires a repaint.
    /// </summary>
    public event NeedPaintHandler? NeedPaint;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the AppButtonController class.
    /// </summary>
    public AppButtonController(KryptonRibbon ribbon)
    {
        _ribbon = ribbon;
        _updateTimer = new Timer
        {
            Interval = 1
        };
        _updateTimer.Tick += OnUpdateTimer;
        Keyboard = false;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the first target element.
    /// </summary>
    public ViewBase Target1 { get; set; }

    /// <summary>
    /// Gets and sets the second target element.
    /// </summary>
    public ViewBase Target2 { get; set; }

    /// <summary>
    /// Gets and sets the third target element.
    /// </summary>
    public ViewBase Target3 { get; set; }

    /// <summary>
    /// Gets a value indicating if the keyboard was used to request the menu.
    /// </summary>
    public bool Keyboard { get; private set; }

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
            _mouseDown = false;

            // No longer in fixed state mode
            _fixedPressed = false;

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

        if (!_fixedPressed)
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
        // Only interested in left mouse pressing down
        if (button == MouseButtons.Left)
        {
            // Mouse is being pressed
            _mouseDown = true;

            // If already in fixed mode, then ignore mouse down
            if (!_fixedPressed && _ribbon.Enabled)
            {
                // Mouse is being pressed
                UpdateTargetState();

                // Show the button as pressed, until told otherwise
                _fixedPressed = true;

                // Generate a click event
                Keyboard = false;
                OnClick(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));
            }
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
        // Only interested in left mouse going up
        if (button == MouseButtons.Left)
        {
            OnMouseReleased(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));
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

        if (!_fixedPressed)
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
    public virtual bool IgnoreVisualFormLeftButtonDown => true;

    #endregion

    #region Focus Notifications
    /// <summary>
    /// Source control has got the focus.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    public virtual void GotFocus(Control c)
    {
        _hasFocus = true;

        if (!_fixedPressed)
        {
            _updateTimer.Start();
        }
    }

    /// <summary>
    /// Source control has lost the focus.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    public virtual void LostFocus([DisallowNull] Control c)
    {
        _hasFocus = false;

        if (!_fixedPressed)
        {
            _updateTimer.Start();
        }
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
        ViewBase? newView = null;
        var ribbon = c as KryptonRibbon;

        if (ribbon is null)
        {
            throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(ribbon)));
        }

        if (ribbon.TabsArea is null)
        {
            throw new NullReferenceException(GlobalStaticValues.PropertyCannotBeNull(nameof(ribbon.TabsArea)));
        }

        switch (e.KeyData)
        {
            case Keys.Tab:
            case Keys.Right:
                // Ask the ribbon to get use the first view for the qat
                newView = ribbon.GetFirstQATView() ?? ribbon.TabsArea.ButtonSpecManager?.GetLastVisibleViewButton(PaletteRelativeEdgeAlign.Near);

                // Get the first near edge button (the last near button is the leftmost one!)

                if (newView == null)
                {
                    if ((e.KeyData == Keys.Tab) && (ribbon.SelectedTab != null))
                    {
                        // Get the currently selected tab page
                        newView = ribbon.TabsArea.LayoutTabs.GetViewForRibbonTab(ribbon.SelectedTab);
                    }
                    else
                    {
                        // Get the first visible tab page
                        newView = ribbon.TabsArea.LayoutTabs.GetViewForFirstRibbonTab();
                    }
                }

                // Move across to any far defined buttons
                newView ??= ribbon.TabsArea.ButtonSpecManager?.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Far);

                // Move across to any inherit defined buttons
                newView ??= ribbon.TabsArea.ButtonSpecManager?.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Inherit);
                break;
            case Keys.Tab | Keys.Shift:
            case Keys.Left:
                // Move across to any far defined buttons
                newView = ribbon.TabsArea.ButtonSpecManager?.GetLastVisibleViewButton(PaletteRelativeEdgeAlign.Far) ??
                          ribbon.TabsArea.ButtonSpecManager?.GetLastVisibleViewButton(PaletteRelativeEdgeAlign.Inherit);

                // Move across to any inherit defined buttons

                if (newView == null)
                {
                    if (e.KeyData != Keys.Left)
                    {
                        // Get the last control on the selected tab
                        newView = ribbon.GroupsArea.ViewGroups.GetLastFocusItem() ??
                                  (ribbon.SelectedTab != null   // Get the currently selected tab page
                                      ? ribbon.TabsArea.LayoutTabs.GetViewForRibbonTab(ribbon.SelectedTab)
                                      : ribbon.TabsArea.LayoutTabs.GetViewForLastRibbonTab());

                    }
                    else
                    {
                        // Get the last visible tab page
                        newView = ribbon.TabsArea.LayoutTabs.GetViewForLastRibbonTab();
                    }
                }

                // Get the last near edge button (the first near button is the rightmost one!)
                newView ??= ribbon.TabsArea.ButtonSpecManager?.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Near);

                // Get the last qat button
                newView ??= ribbon.GetLastQATView();
                break;
            case Keys.Space:
            case Keys.Enter:
            case Keys.Down:
                // Show the button as pressed, until told otherwise
                _fixedPressed = true;

                // Mouse is being pressed
                UpdateTargetState();

                // Generate a click event
                Keyboard = true;
                OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                break;
        }

        // If we have a new view to focus and it is not ourself...
        if ((newView != null)
            && (newView != Target1)
            && (newView != Target2)
            && (newView != Target3)
           )
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
    public void KeyTipSelect(KryptonRibbon? ribbon)
    {
        if (ribbon is null)
        {
            throw new ArgumentNullException(nameof(ribbon));
        }

        if (ribbon.TabsArea is null)
        {
            throw new NullReferenceException(GlobalStaticValues.PropertyCannotBeNull(nameof(ribbon.TabsArea)));
        }

        // We leave key tips usage whenever we use the application button
        ribbon.KillKeyboardKeyTips();

        // Show the button as pressed, until told otherwise
        _fixedPressed = true;

        // Mouse is being pressed
        UpdateTargetState();

        // Switch focus to ourself
        ribbon.FocusView = ribbon.TabsArea.LayoutAppButton.AppButton;

        // Generate a click event
        Keyboard = true;
        OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
    }
    #endregion

    #region Protected
    /// <summary>
    /// Set the correct visual state of the target.
    /// </summary>
    protected void UpdateTargetState()
    {
        // By default, the button is in the normal state
        var newState = PaletteState.Normal;

        // Only allow another state if the ribbon is enabled
        if (_ribbon.Enabled)
        {
            // Are we showing with the fixed state?
            if (_fixedPressed)
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
                else if (_mouseOver || _hasFocus)
                {
                    newState = PaletteState.Tracking;
                }
            }
        }

        var needPaint = false;

        // Update all the targets
        if ((Target1 != null) && (Target1.ElementState != newState))
        {
            Target1.ElementState = newState;
            needPaint = true;
        }

        if ((Target2 != null) && (Target2.ElementState != newState))
        {
            Target2.ElementState = newState;
            needPaint = true;
        }

        if ((Target3 != null) && (Target3.ElementState != newState))
        {
            Target3.ElementState = newState;
            needPaint = true;
        }

        if (needPaint)
        {
#pragma warning disable IDE0170 // Property pattern can be simplified - Does not work in VS2019 !
            if (Target1 is { ClientRectangle: { IsEmpty: false } })
            {
                OnNeedPaint(false, Target1.ClientRectangle);
            }

            if (Target2 is { ClientRectangle: { IsEmpty: false } })
            {
                OnNeedPaint(false, Target2.ClientRectangle);
            }

            if (Target3 is { ClientRectangle: { IsEmpty: false } })
            {
                OnNeedPaint(false, Target3.ClientRectangle);
            }
#pragma warning restore IDE0170 // Property pattern can be simplified

            // Get the repaint to happen immediately
            Application.DoEvents();
        }
    }

    /// <summary>
    /// Raises the NeedPaint event.
    /// </summary>
    /// <param name="needLayout">Does the palette change require a layout.</param>
    /// <param name="invalidRect">Client area to be invalidated.</param>
    protected virtual void OnNeedPaint(bool needLayout,
        Rectangle invalidRect) => NeedPaint?.Invoke(this, new NeedLayoutEventArgs(needLayout, invalidRect));

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="e">A MouseEventArgs containing the event data.</param>
    protected virtual void OnClick(MouseEventArgs e)
    {
        Click?.Invoke(this, e);

        Keyboard = false;
    }

    /// <summary>
    /// Raises the MouseReleased event.
    /// </summary>
    /// <param name="e">A MouseEventArgs containing the event data.</param>
    protected virtual void OnMouseReleased(MouseEventArgs e) => MouseReleased?.Invoke(this, e);
    #endregion

    #region Implementation
    private void OnUpdateTimer(object? sender, EventArgs e)
    {
        _updateTimer.Stop();
        UpdateTargetState();
    }
    #endregion
}