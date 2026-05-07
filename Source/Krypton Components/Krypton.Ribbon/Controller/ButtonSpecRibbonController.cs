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
/// Process mouse events for a ribbon based button spec button.
/// </summary>
internal class ButtonSpecRibbonController : ButtonController
{
    #region Instance Fields
    private bool _hasFocus;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonSpecRibbonController class.
    /// </summary>
    /// <param name="target">Target for state changes.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ButtonSpecRibbonController(ViewBase target,
        NeedPaintHandler needPaint)
        : base(target, needPaint)
    {
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

        if (ribbon.TabsArea.ButtonSpecManager is null)
        {
            throw new NullReferenceException(GlobalStaticValues.PropertyCannotBeNull(nameof(ribbon.TabsArea.ButtonSpecManager)));
        }

        // Get the button spec associated with this controller
        ViewDrawButton? viewButton = Target as ViewDrawButton ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(Target)));
            
        ButtonSpec? buttonSpec = ribbon.TabsArea.ButtonSpecManager.GetButtonSpecFromView(viewButton) ?? throw new NullReferenceException( "ribbon.TabsArea.ButtonSpecManager.GetButtonSpecFromView(viewButton) returned null.");

        // Note If we are on the near edge
        var isNear = buttonSpec.Edge is PaletteRelativeEdgeAlign.Near;

        switch (e.KeyData)
        {
            case Keys.Tab:
            case Keys.Right:
                // Logic depends on the edge this button is on
                if (isNear)
                {
                    // Try getting the previous near edge button (previous on near gets the next right hand side!)
                    newView = ribbon.TabsArea.ButtonSpecManager.GetPreviousVisibleViewButton(PaletteRelativeEdgeAlign.Near, viewButton);

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

                    // Get the first far edge button
                    newView ??= ribbon.TabsArea.ButtonSpecManager.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Far);

                    // Get the first inherit edge button
                    newView ??= ribbon.TabsArea.ButtonSpecManager.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Inherit);

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
                }
                else
                {
                    // Try using the next far edge button
                    newView = ribbon.TabsArea.ButtonSpecManager.GetNextVisibleViewButton(PaletteRelativeEdgeAlign.Far, viewButton) ??
                              ribbon.TabsArea.ButtonSpecManager.GetNextVisibleViewButton(PaletteRelativeEdgeAlign.Inherit, viewButton);

                    // Try using the next inherit edge button

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
                }
                break;
            case Keys.Tab | Keys.Shift:
            case Keys.Left:
                // Logic depends on the edge this button is on
                if (isNear)
                {
                    // Try using the previous near edge button (next for a near edge is the left hand side!)
                    newView = ribbon.TabsArea.ButtonSpecManager.GetNextVisibleViewButton(PaletteRelativeEdgeAlign.Near, viewButton) ??
                              ribbon.GetLastQATView();

                    // Get the last qat button

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
                }
                else
                {
                    // Try getting the previous far edge button
                    newView = ribbon.TabsArea.ButtonSpecManager.GetPreviousVisibleViewButton(PaletteRelativeEdgeAlign.Far, viewButton) ??
                              ribbon.TabsArea.ButtonSpecManager.GetPreviousVisibleViewButton(PaletteRelativeEdgeAlign.Inherit, viewButton);

                    // Try getting the previous inherit edge button

                    if (newView == null)
                    {
                        if (e.KeyData != Keys.Left)
                        {
                            // Get the last control on the selected tab
                            newView = ribbon.GroupsArea.ViewGroups.GetLastFocusItem() ??
                                      (ribbon.SelectedTab != null // Get the currently selected tab page
                                          ? ribbon.TabsArea.LayoutTabs.GetViewForRibbonTab(ribbon.SelectedTab)
                                          : ribbon.TabsArea.LayoutTabs.GetViewForLastRibbonTab());
                        }
                        else
                        {
                            // Get the last visible tab page
                            newView = ribbon.TabsArea.LayoutTabs.GetViewForLastRibbonTab();
                        }
                    }

                    // Get the last near edge button
                    newView ??= ribbon.TabsArea.ButtonSpecManager.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Near);

                    // Get the last qat button
                    newView ??= ribbon.GetLastQATView();

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
                }
                break;
            case Keys.Space:
            case Keys.Enter:
                // Exit keyboard mode when you click the button spec
                ribbon.KillKeyboardMode();

                // Generate a click event
                OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
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

    /// <summary>
    /// Key has been pressed.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
    public override void KeyPress(Control c, KeyPressEventArgs e)
    {
    }

    /// <summary>
    /// Key has been released.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="e">A KeyEventArgs that contains the event data.</param>
    /// <returns>True if capturing input; otherwise false.</returns>
    public override bool KeyUp(Control c, KeyEventArgs e) => false;

    #endregion

    #region Source Notifications
    /// <summary>
    /// Source control has lost the focus.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    public override void GotFocus(Control c)
    {
        _hasFocus = true;

        // Update the visual state
        UpdateTargetState(c);
    }

    /// <summary>
    /// Source control has lost the focus.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    public override void LostFocus([DisallowNull] Control c)
    {
        _hasFocus = false;

        // Update the visual state
        UpdateTargetState(c);
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
                OnNeedPaint(true);
            }
        }
        else
        {
            base.UpdateTargetState(pt);
        }
    }
    #endregion
}