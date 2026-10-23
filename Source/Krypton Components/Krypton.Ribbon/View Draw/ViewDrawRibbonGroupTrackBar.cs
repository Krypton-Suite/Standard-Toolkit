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
/// Draws a ribbon group track bar.
/// </summary>
internal class ViewDrawRibbonGroupTrackBar : ViewComposite,
    IRibbonViewGroupItemView
{
    #region Instance Fields
    private readonly int _nullControlWidth; // = 50;
    private readonly KryptonRibbon _ribbon;
    private ViewDrawRibbonGroup? _activeGroup;
    private readonly TrackBarController? _controller;
    private readonly NeedPaintHandler? _needPaint;
    private GroupItemSize _currentSize;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonGroupTrackBar class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="ribbonTrackBar">Reference to source track bar.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ViewDrawRibbonGroupTrackBar([DisallowNull] KryptonRibbon ribbon,
        [DisallowNull] KryptonRibbonGroupTrackBar ribbonTrackBar,
        [DisallowNull] NeedPaintHandler needPaint)
    {
        Debug.Assert(ribbon != null);
        Debug.Assert(ribbonTrackBar != null);
        Debug.Assert(needPaint != null);

        // Remember incoming references
        _ribbon = ribbon!;
        GroupTrackBar = ribbonTrackBar;
        _needPaint = needPaint;
        _currentSize = GroupTrackBar!.ItemSizeCurrent;

        // Hook into the textbox events
        GroupTrackBar.MouseEnterControl += OnMouseEnterControl;
        GroupTrackBar.MouseLeaveControl += OnMouseLeaveControl;

        // Associate this view with the source component (required for design time selection)
        Component = GroupTrackBar;

        if (_ribbon.InDesignMode)
        {
            // At design time we need to know when the user right clicks the textbox
            var controller = new ContextClickController();
            controller.ContextClick += OnContextClick;
            MouseController = controller;
        }

        // Create controller needed for handling focus and key tip actions
        _controller = new TrackBarController(_ribbon, GroupTrackBar, this);
        SourceController = _controller;
        KeyController = _controller;

        // We need to rest visibility of the textbox for each layout cycle
        _ribbon.ViewRibbonManager!.LayoutBefore += OnLayoutAction;
        _ribbon.ViewRibbonManager.LayoutAfter += OnLayoutAction;

        // Define back reference to view for the text box definition
        GroupTrackBar.TrackBarView = this;

        // Give paint delegate to textbox so its palette changes are redrawn
        GroupTrackBar.ViewPaintDelegate = needPaint;

        // Update all views to reflect current state
        UpdateEnabled(GroupTrackBar.TrackBar);
        UpdateVisible(GroupTrackBar.TrackBar);

        // Hook into changes in the ribbon custom definition
        GroupTrackBar.PropertyChanged += OnTrackBarPropertyChanged;
        _nullControlWidth = (int)(50 * FactorDpiX);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonGroupTrackBar:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (GroupTrackBar != null)
            {
                // Must unhook to prevent memory leaks
                GroupTrackBar.MouseEnterControl -= OnMouseEnterControl;
                GroupTrackBar.MouseLeaveControl -= OnMouseLeaveControl;
                GroupTrackBar.ViewPaintDelegate = null;
                GroupTrackBar.PropertyChanged -= OnTrackBarPropertyChanged;
                _ribbon.ViewRibbonManager!.LayoutAfter -= OnLayoutAction;
                _ribbon.ViewRibbonManager.LayoutBefore -= OnLayoutAction;

                // Remove association with definition
                GroupTrackBar.TrackBarView = null; 
                GroupTrackBar = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region GroupTrackBar
    /// <summary>
    /// Gets access to the owning group track bar instance.
    /// </summary>
    public KryptonRibbonGroupTrackBar? GroupTrackBar { get; private set; }

    #endregion

    #region LostFocus
    /// <summary>
    /// Source control has lost the focus.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    public override void LostFocus(Control c)
    {
        // Ask ribbon to shift focus to the hidden control
        _ribbon.HideFocus(GroupTrackBar?.TrackBar);
        base.LostFocus(c);
    }
    #endregion

    #region GetFirstFocusItem
    /// <summary>
    /// Gets the first focus item from the container.
    /// </summary>
    /// <returns>ViewBase of item; otherwise false.</returns>
    public ViewBase GetFirstFocusItem() => GroupTrackBar is { Visible: true, LastTrackBar: { CanSelect: true } } ? this : null!;

    #endregion

    #region GetLastFocusItem
    /// <summary>
    /// Gets the last focus item from the item.
    /// </summary>
    /// <returns>ViewBase of item; otherwise false.</returns>
    public ViewBase GetLastFocusItem() => GroupTrackBar is { Visible: true, LastTrackBar: { CanSelect: true } } ? this : null!;

    #endregion

    #region GetNextFocusItem
    /// <summary>
    /// Gets the next focus item based on the current item as provided.
    /// </summary>
    /// <param name="current">The view that is currently focused.</param>
    /// <param name="matched">Has the current focus item been matched yet.</param>
    /// <returns>ViewBase of item; otherwise false.</returns>
    public ViewBase GetNextFocusItem(ViewBase current, ref bool matched)
    {
        // Do we match the current item?
        matched = current == this;
        return null!;
    }
    #endregion

    #region GetPreviousFocusItem
    /// <summary>
    /// Gets the previous focus item based on the current item as provided.
    /// </summary>
    /// <param name="current">The view that is currently focused.</param>
    /// <param name="matched">Has the current focus item been matched yet.</param>
    /// <returns>ViewBase of item; otherwise false.</returns>
    public ViewBase GetPreviousFocusItem(ViewBase current, ref bool matched)
    {
        // Do we match the current item?
        matched = current == this;
        return null!;
    }
    #endregion

    #region GetGroupKeyTips
    /// <summary>
    /// Gets the array of group level key tips.
    /// </summary>
    /// <param name="keyTipList">List to add new entries into.</param>
    /// <param name="lineHint">Provide hint to item about its location.</param>
    public void GetGroupKeyTips(KeyTipInfoList keyTipList, int lineHint)
    {
        // Only provide a key tip if we are visible and the target control can accept focus
        if (Visible && LastTrackBar!.CanFocus)
        {
            // Get the screen location of the button
            Rectangle viewRect = _ribbon.KeyTipToScreen(this);

            // Determine the screen position of the key tip
            var screenPt = Point.Empty;

            // Determine the screen position of the key tip dependent on item location/size
            switch (_currentSize)
            {
                case GroupItemSize.Large:
                    screenPt = new Point(viewRect.Left + (viewRect.Width / 2), viewRect.Bottom);
                    break;
                case GroupItemSize.Medium:
                case GroupItemSize.Small:
                    screenPt = _ribbon.CalculatedValues.KeyTipRectToPoint(viewRect, lineHint);
                    break;
            }

            keyTipList.Add(new KeyTipInfo(GroupTrackBar!.Enabled, 
                GroupTrackBar.KeyTip,
                screenPt, 
                ClientRectangle,
                _controller));
        }
    }
    #endregion

    #region Layout
    /// <summary>
    /// Override the group item size if possible.
    /// </summary>
    /// <param name="size">New size to use.</param>
    public void SetGroupItemSize(GroupItemSize size) => _currentSize = size;

    /// <summary>
    /// Reset the group item size to the item definition.
    /// </summary>
    public void ResetGroupItemSize() => _currentSize = GroupTrackBar!.ItemSizeCurrent;

    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        var preferredSize = Size.Empty;

        // Ensure the control has the correct parent
        UpdateParent(context.Control!);

        // If there is a textbox associated then ask for its requested size
        if (LastTrackBar != null)
        {
            if (ActualVisible(LastTrackBar))
            {
                preferredSize = LastTrackBar.GetPreferredSize(context.DisplayRectangle.Size);

                // Add two pixels, one for the left and right edges that will be padding
                preferredSize.Width += 2;
            }
        }
        else
        {
            preferredSize.Width = _nullControlWidth;
        }

        preferredSize.Height = _currentSize == GroupItemSize.Large
            ? _ribbon.CalculatedValues.GroupTripleHeight
            : _ribbon.CalculatedValues.GroupLineHeight;

        return preferredSize;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;

        // Are we allowed to change the layout of controls?
        if (!context.ViewManager!.DoNotLayoutControls)
        {
            // If we have an actual control, position it with a pixel padding all around
            LastTrackBar?.SetBounds(ClientLocation.X + 1,
                ClientLocation.Y + 1,
                ClientWidth - 2,
                ClientHeight - 2);
        }

        // Let child elements layout in given space
        base.Layout(context);
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform a render of the elements.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void Render([DisallowNull] RenderContext context)
    {
        Debug.Assert(context != null);

        // If we do not have a textbox
        if (GroupTrackBar?.TrackBar == null)
        {
            // And we are in design time
            if (_ribbon.InDesignMode)
            {
                // Draw rectangle is 1 pixel less per edge
                Rectangle drawRect = ClientRectangle;
                drawRect.Inflate(-1, -1);
                drawRect.Height--;

                // Draw an indication of where the textbox will be
                context?.Graphics.FillRectangle(Brushes.Goldenrod, drawRect);
                context?.Graphics.DrawRectangle(Pens.Gold, drawRect);
            }
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// Raises the NeedPaint event.
    /// </summary>
    /// <param name="needLayout">Does the palette change require a layout.</param>
    protected virtual void OnNeedPaint(bool needLayout) => OnNeedPaint(needLayout, Rectangle.Empty);

    /// <summary>
    /// Raises the NeedPaint event.
    /// </summary>
    /// <param name="needLayout">Does the palette change require a layout.</param>
    /// <param name="invalidRect">Rectangle to invalidate.</param>
    protected virtual void OnNeedPaint(bool needLayout, Rectangle invalidRect)
    {
        if (_needPaint != null)
        {
            _needPaint(this, new NeedLayoutEventArgs(needLayout));

            if (needLayout)
            {
                _ribbon.PerformLayout();
            }
        }
    }
    #endregion

    #region Implementation
    private void OnContextClick(object? sender, MouseEventArgs e) => GroupTrackBar?.OnDesignTimeContextMenu(e);

    private void OnTrackBarPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        var updateLayout = false;
        const bool UPDATE_PAINT = false;

        switch (e.PropertyName)
        {
            case nameof(Enabled):
                UpdateEnabled(LastTrackBar);
                break;
            case nameof(Visible):
                UpdateVisible(LastTrackBar);
                updateLayout = true;
                break;
            case "CustomControl":
                updateLayout = true;
                break;
        }

        if (updateLayout)
        {
            // If we are on the currently selected tab then...
            if ((GroupTrackBar?.RibbonTab != null) &&
                (_ribbon.SelectedTab == GroupTrackBar.RibbonTab))
            {
                // ...layout so the visible change is made
                OnNeedPaint(true);
            }
        }

        if (UPDATE_PAINT)
#pragma warning disable 162
        {
            // If this button is actually defined as visible...
            if (GroupTrackBar.Visible || _ribbon.InDesignMode)
            {
                // ...and on the currently selected tab then...
                if ((GroupTrackBar.RibbonTab != null) &&
                    (_ribbon.SelectedTab == GroupTrackBar.RibbonTab))
                {
                    // ...repaint it right now
                    OnNeedPaint(false, ClientRectangle);
                }
            }
        }
#pragma warning restore 162
    }

    private Control? LastParentControl
    {
        get => GroupTrackBar?.LastParentControl;
        set => GroupTrackBar!.LastParentControl = value;
    }

    private KryptonTrackBar? LastTrackBar
    {
        get => GroupTrackBar?.LastTrackBar;
        set => GroupTrackBar!.LastTrackBar = value;
    }

    private void UpdateParent(Control parentControl)
    {
        // Is there a change in the textbox or a change in 
        // the parent control that is hosting the control...
        if ((parentControl != LastParentControl) ||
            (LastTrackBar != GroupTrackBar?.TrackBar))
        {
            // We only modify the parent and visible state if processing for correct container
            if ((GroupTrackBar!.RibbonContainer!.RibbonGroup!.ShowingAsPopup && (parentControl is VisualPopupGroup)) ||
                (!GroupTrackBar.RibbonContainer.RibbonGroup.ShowingAsPopup && parentControl is not VisualPopupGroup))
            {
                // If we have added the custom control to a parent before
                if ((LastTrackBar != null) && (LastParentControl != null))
                {
                    // If that control is still a child of the old parent
                    if (LastParentControl.Controls.Contains(LastTrackBar))
                    {
                        // Check for a collection that is based on the read only class
                        LastParentControl.Controls.Remove(LastTrackBar);
                    }
                }

                // Remember the current control and new parent
                LastTrackBar = GroupTrackBar.TrackBar;
                LastParentControl = parentControl;

                // If we have a new textbox and parent
                if ((LastTrackBar != null) && (LastParentControl != null))
                {
                    // Ensure the control is not in the display area when first added
                    LastTrackBar.Location = new Point(-LastTrackBar.Width, -LastTrackBar.Height);

                    // Check for the correct visible state of the textbox
                    UpdateVisible(LastTrackBar);

                    // Check for a collection that is based on the read only class
                    LastParentControl.Controls.Add(LastTrackBar);
                }
            }
        }
    }

    private void UpdateEnabled(Control? c)
    {
        if (c != null)
        {
            // Start with the enabled state of the group element
            var enabled = GroupTrackBar!.Enabled;

            // If we have an associated designer setup...
            if (!_ribbon.InDesignHelperMode && (GroupTrackBar?.TrackBarDesigner != null))
            {
                // And we are not using the design helpers, then use the design specified value
                enabled = GroupTrackBar.TrackBarDesigner.DesignEnabled;
            }

            c.Enabled = enabled;
        }
    }

    private bool ActualVisible(Control? c)
    {
        if (c != null)
        {
            // Start with the visible state of the group element
            var visible = GroupTrackBar!.Visible;

            // If we have an associated designer setup...
            if (!_ribbon.InDesignHelperMode && (GroupTrackBar?.TrackBarDesigner != null))
            {
                // And we are not using the design helpers, then use the design specified value
                visible = GroupTrackBar.TrackBarDesigner.DesignVisible;
            }

            return visible;
        }

        return false;
    }

    private void UpdateVisible(Control? c)
    {
        if (c != null)
        {
            // Start with the visible state of the group element
            var visible = GroupTrackBar!.Visible;

            // If we have an associated designer setup...
            if (!_ribbon.InDesignHelperMode && (GroupTrackBar?.TrackBarDesigner != null))
            {
                // And we are not using the design helpers, then use the design specified value
                visible = GroupTrackBar.TrackBarDesigner.DesignVisible;
            }

            if (visible)
            {
                // Only visible if on the currently selected page
                if ((GroupTrackBar?.RibbonTab == null) ||
                    (_ribbon.SelectedTab != GroupTrackBar.RibbonTab))
                {
                    visible = false;
                }
                else
                {
                    // Check the owning group is visible
                    if (GroupTrackBar.RibbonContainer?.RibbonGroup is { Visible: false } 
                        && !_ribbon.InDesignMode
                       )
                    {
                        visible = false;
                    }
                    else
                    {
                        // Check that the group is not collapsed
                        if (GroupTrackBar.RibbonContainer!.RibbonGroup!.IsCollapsed &&
                            ((_ribbon.GetControllerControl(GroupTrackBar.TrackBar) is KryptonRibbon) ||
                             (_ribbon.GetControllerControl(GroupTrackBar.TrackBar) is VisualPopupMinimized))
                           )
                        {
                            visible = false;
                        }
                        else
                        {
                            // Check that the hierarchy of containers are all visible
                            KryptonRibbonGroupContainer? container = GroupTrackBar.RibbonContainer;

                            // Keep going until we have searched the entire parent chain of containers
                            while (container != null)
                            {
                                // If any parent container is not visible, then we are not visible
                                if (!container.Visible)
                                {
                                    visible = false;
                                    break;
                                }

                                // Move up a level
                                container = container.RibbonContainer;
                            }
                        }
                    }
                }
            }

            c.Visible = visible;
        }
    }

    private void OnLayoutAction(object? sender, EventArgs e)
    {
        // If not disposed then we still have a element reference
        if (GroupTrackBar != null)
        {
            // Change in selected tab requires a retest of the control visibility
            UpdateVisible(LastTrackBar);
        }
    }

    private void OnMouseEnterControl(object? sender, EventArgs e)
    {
        // Reset the active group setting
        _activeGroup = null;

        // Find the parent group instance
        ViewBase? parent = Parent;

        // Keep going till we get to the top or find a group
        while (parent != null)
        {
            if (parent is ViewDrawRibbonGroup ribGroup)
            {
                _activeGroup = ribGroup;
                break;
            }

            // Move up a level
            parent = parent.Parent;
        }

        // If we found a group we are inside
        if (_activeGroup != null)
        {
            _activeGroup.Tracking = true;
            _needPaint?.Invoke(this, new NeedLayoutEventArgs(false, _activeGroup.ClientRectangle));
        }
    }

    private void OnMouseLeaveControl(object? sender, EventArgs e)
    {
        // If we have a cached group we made active
        if (_activeGroup != null)
        {
            _activeGroup.Tracking = false;
            _needPaint?.Invoke(this, new NeedLayoutEventArgs(false, _activeGroup.ClientRectangle));
            _activeGroup = null;
        }
    }
    #endregion
}