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
/// Provide button functionality for the group dialog launcher.
/// </summary>
internal class DialogLauncherButtonController : LeftUpButtonController,
    ISourceController,
    IKeyController,
    IRibbonKeyTipTarget
{
    #region Instance Fields
    private bool _hasFocus;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the DialogLauncherButtonController class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon instance.</param>
    /// <param name="target">Target for state changes.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public DialogLauncherButtonController(KryptonRibbon ribbon,
        ViewBase target,
        NeedPaintHandler needPaint)
        : base(ribbon, target, needPaint)
    {
    }
    #endregion

    #region Focus Notifications
    /// <summary>
    /// Source control has got the focus.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    public virtual void GotFocus(Control c)
    {
        _hasFocus = true;

        // Redraw to show the change in visual state
        UpdateTargetState(Point.Empty);
        OnNeedPaint(false);
    }

    /// <summary>
    /// Source control has lost the focus.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    public virtual void LostFocus([DisallowNull] Control c)
    {
        _hasFocus = false;

        // Redraw to show the change in visual state
        UpdateTargetState(Point.Empty);
        OnNeedPaint(false);
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
        c = Ribbon.GetControllerControl(c)!;

        switch (c)
        {
            case KryptonRibbon _:
                KeyDownRibbon(e);
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
        // Exit keyboard mode when you click the button spec
        Ribbon.KillKeyboardMode();

        // Generate a click event
        OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
    }
    #endregion

    #region Protected
    /// <summary>
    /// Set the correct visual state of the target.
    /// </summary>
    /// <param name="pt">Mouse point.</param>
    protected override void UpdateTargetState(Point pt)
    {
        if (_hasFocus)
        {
            if (Target.ElementState != PaletteState.Tracking)
            {
                // Update target to reflect new state
                Target.ElementState = PaletteState.Tracking;

                // Redraw to show the change in visual state
                OnNeedPaint(false);
            }
        }
        else
        {
            base.UpdateTargetState(pt);
        }
    }
    #endregion

    #region Implementation
    private void KeyDownRibbon(KeyEventArgs e)
    {
        ViewBase? newView = null;

        if (Ribbon.TabsArea is null)
        {
            throw new NullReferenceException(GlobalStaticValues.PropertyCannotBeNull(nameof(Ribbon.TabsArea)));
        }

        switch (e.KeyData)
        {
            case Keys.Tab | Keys.Shift:
            case Keys.Left:
                // Get the previous focus item for the currently selected page
                newView = Ribbon.GroupsArea.ViewGroups.GetPreviousFocusItem(Target) ?? Ribbon.TabsArea.LayoutTabs.GetViewForRibbonTab(Ribbon.SelectedTab);

                // Got to the actual tab header
                break;
            case Keys.Tab:
            case Keys.Right:
                // Get the next focus item for the currently selected page
                newView = Ribbon.GroupsArea.ViewGroups.GetNextFocusItem(Target) ?? Ribbon.TabsArea.ButtonSpecManager!.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Far);

                // Move across to any far defined buttons

                // Move across to any inherit defined buttons
                newView ??= Ribbon.TabsArea.ButtonSpecManager!.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Inherit);

                // Rotate around to application button
                if (newView == null)
                {
                    if (Ribbon.TabsArea.LayoutAppButton.Visible)
                    {
                        newView = Ribbon.TabsArea.LayoutAppButton.AppButton;
                    }
                    else if (Ribbon.TabsArea.LayoutAppTab.Visible)
                    {
                        newView = Ribbon.TabsArea.LayoutAppTab.AppTab;
                    }
                }
                break;
            case Keys.Space:
            case Keys.Enter:
                // Exit keyboard mode when you click the button spec
                Ribbon.KillKeyboardMode();

                // Generate a click event
                OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                break;
        }

        // If we have a new view to focus and it is not ourself...
        if ((newView != null) && (newView != Target))
        {
            // If the new view is a tab then select that tab unless in minimized mode
            if (!Ribbon.RealMinimizedMode && (newView is ViewDrawRibbonTab tab))
            {
                Ribbon.SelectedTab = tab.RibbonTab;
            }

            // Finally we switch focus to new view
            Ribbon.FocusView = newView;
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
                // Exit keyboard mode when you click the button spec
                Ribbon.KillKeyboardMode();

                // Generate a click event
                OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
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
                // Exit keyboard mode when you click the button spec
                Ribbon.KillKeyboardMode();

                // Generate a click event
                OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                break;
        }
    }
    #endregion
}