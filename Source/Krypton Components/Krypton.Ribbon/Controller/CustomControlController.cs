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
/// Process mouse events for a ribbon group custom control.
/// </summary>
internal class CustomControlController : GlobalId,
    ISourceController,
    IKeyController,
    IRibbonKeyTipTarget
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private readonly KryptonRibbonGroupCustomControl _customControl;
    private readonly ViewDrawRibbonGroupCustomControl _target;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the CustomControlController class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon instance.</param>
    /// <param name="customControl">Source definition.</param>
    /// <param name="target">Target view element.</param>
    public CustomControlController([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] KryptonRibbonGroupCustomControl? customControl,
        [DisallowNull] ViewDrawRibbonGroupCustomControl? target)
    {
        Debug.Assert(ribbon is not null);
        Debug.Assert(customControl is not null);
        Debug.Assert(customControl is not null);

        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));
        _customControl = customControl ?? throw new ArgumentNullException(nameof(customControl));
        _target = target ?? throw new ArgumentNullException(nameof(target));
    }
    #endregion

    #region Focus Notifications
    /// <summary>
    /// Source control has got the focus.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    public void GotFocus(Control c)
    {
        if (_customControl.LastCustomControl is { CanFocus: true })
        {
            _ribbon.LostFocusLosesKeyboard = false;
            _customControl.LastCustomControl.Focus();
        }
    }

    /// <summary>
    /// Source control has lost the focus.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    public void LostFocus([DisallowNull] Control c)
    {
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
        // Can the custom control take the focus
        if (_customControl.LastCustomControl!.CanFocus)
        {
            // Prevent the ribbon from killing keyboard mode when it loses the focus,
            // as this causes the tracking windows to be killed and we want them kept
            ribbon.LostFocusLosesKeyboard = false;

            // Prevent the restore of focus when we fill the keyboard mode, as the focus
            // has been placed on the custom control and so focus is allowed to change
            ribbon.IgnoreRestoreFocus = true;

            // Exit the use of keyboard mode
            ribbon.KillKeyboardMode();

            // Push focus to the specified target control
            _customControl.LastCustomControl.Focus();
            // Ensure that the previous ribbon focus is restored when the popup window is dismissed

            // If the custom control is inside a popup window
            if (_customControl.LastParentControl is VisualPopupGroup popupGroup)
            {
                popupGroup.RestorePreviousFocus = true;
            }
        }
    }
    #endregion

    #region Implementation
    private void KeyDownRibbon(KryptonRibbon? ribbon, KeyEventArgs e)
    {
        ViewBase? newView = null;

        if (ribbon is null)
        {
            throw new ArgumentNullException(GlobalStaticValues.ParameterCannotBeNull(nameof(ribbon)));
        }

        if (ribbon.TabsArea is null)
        {
            throw new NullReferenceException(GlobalStaticValues.ParameterCannotBeNull(nameof(ribbon.TabsArea)));
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
                newView = ribbon.GroupsArea.ViewGroups.GetNextFocusItem(_target) ?? ribbon.TabsArea.ButtonSpecManager!.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Far);

                // Move across to any far defined buttons

                // Move across to any inherit defined buttons
                newView ??= ribbon.TabsArea.ButtonSpecManager!.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Inherit);

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
        }

        // If we have a new view to focus and it is not ourself...
        if ((newView != null) && (newView != _target))
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
        }
    }
    #endregion
}