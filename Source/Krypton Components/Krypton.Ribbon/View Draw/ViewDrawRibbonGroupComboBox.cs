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
/// Draws a ribbon group combobox.
/// </summary>
internal class ViewDrawRibbonGroupComboBox : ViewComposite,
    IRibbonViewGroupItemView
{
    #region Instance Fields
    private readonly int _nullControlWidth; // = 50;
    private readonly KryptonRibbon _ribbon;
    private ViewDrawRibbonGroup? _activeGroup;
    private readonly ComboBoxController? _controller;
    private readonly NeedPaintHandler? _needPaint;
    private GroupItemSize _currentSize;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonGroupComboBox class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="ribbonComboBox">Reference to source combobox.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ViewDrawRibbonGroupComboBox([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] KryptonRibbonGroupComboBox? ribbonComboBox,
        [DisallowNull] NeedPaintHandler? needPaint)
    {
        Debug.Assert(ribbon is not null);
        Debug.Assert(ribbonComboBox is not null);
        Debug.Assert(needPaint is not null);

        // Remember incoming references
        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));
        GroupComboBox = ribbonComboBox ?? throw new ArgumentNullException(nameof(ribbonComboBox));
        _needPaint = needPaint ?? throw new ArgumentNullException(nameof(needPaint));
        _currentSize = GroupComboBox.ItemSizeCurrent;

        // Hook into the combobox events
        GroupComboBox.MouseEnterControl += OnMouseEnterControl;
        GroupComboBox.MouseLeaveControl += OnMouseLeaveControl;

        // Associate this view with the source component (required for design time selection)
        Component = GroupComboBox;

        if (_ribbon.InDesignMode)
        {
            // At design time we need to know when the user right-clicks the combobox
            var controller = new ContextClickController();
            controller.ContextClick += OnContextClick;
            MouseController = controller;
        }

        // Create controller needed for handling focus and key tip actions
        _controller = new ComboBoxController(_ribbon, GroupComboBox, this);
        SourceController = _controller;
        KeyController = _controller;

        // We need to rest visibility of the combobox for each layout cycle
        _ribbon.ViewRibbonManager!.LayoutBefore += OnLayoutAction;
        _ribbon.ViewRibbonManager.LayoutAfter += OnLayoutAction;

        // Define back reference to view for the combo box definition
        GroupComboBox.ComboBoxView = this;

        // Give paint delegate to combobox so its palette changes are redrawn
        GroupComboBox.ViewPaintDelegate = needPaint;

        // Update all views to reflect current state
        UpdateEnabled(GroupComboBox.ComboBox);
        UpdateVisible(GroupComboBox.ComboBox);

        // Hook into changes in the ribbon custom definition
        GroupComboBox.PropertyChanged += OnComboBoxPropertyChanged;

        _nullControlWidth = (int)(50 * FactorDpiX);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonGroupComboBox:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (GroupComboBox != null)
            {
                // Must unhook to prevent memory leaks
                GroupComboBox.MouseEnterControl -= OnMouseEnterControl;
                GroupComboBox.MouseLeaveControl -= OnMouseLeaveControl;
                GroupComboBox.ViewPaintDelegate = null;
                GroupComboBox.PropertyChanged -= OnComboBoxPropertyChanged;
                _ribbon.ViewRibbonManager!.LayoutAfter -= OnLayoutAction;
                _ribbon.ViewRibbonManager.LayoutBefore -= OnLayoutAction;

                // Remove association with definition
                GroupComboBox.ComboBoxView = null; 
                GroupComboBox = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region GroupComboBox
    /// <summary>
    /// Gets access to the owning group combobox instance.
    /// </summary>
    public KryptonRibbonGroupComboBox? GroupComboBox { get; private set; }

    #endregion

    #region LostFocus
    /// <summary>
    /// Source control has lost the focus.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    public override void LostFocus(Control c)
    {
        // Ask ribbon to shift focus to the hidden control
        _ribbon.HideFocus(GroupComboBox?.ComboBox);
        base.LostFocus(c);
    }
    #endregion

    #region GetFirstFocusItem
    /// <summary>
    /// Gets the first focus item from the container.
    /// </summary>
    /// <returns>ViewBase of item; otherwise false.</returns>
    public ViewBase GetFirstFocusItem()
    {
        if (GroupComboBox is { Visible: true, LastComboBox.ComboBox.CanSelect: true })
        {
            return this;
        }
        else
        {
            return null!;
        }
    }
    #endregion

    #region GetLastFocusItem
    /// <summary>
    /// Gets the last focus item from the item.
    /// </summary>
    /// <returns>ViewBase of item; otherwise false.</returns>
    public ViewBase GetLastFocusItem()
    {
        if (GroupComboBox is { Visible: true, LastComboBox.ComboBox.CanSelect: true })
        {
            return this;
        }
        else
        {
            return null!;
        }
    }
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
        if (Visible && LastComboBox!.CanFocus)
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

            keyTipList.Add(new KeyTipInfo(GroupComboBox!.Enabled, 
                GroupComboBox.KeyTip,
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
    public void ResetGroupItemSize() => _currentSize = GroupComboBox!.ItemSizeCurrent;

    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        var preferredSize = Size.Empty;

        // Ensure the control has the correct parent
        UpdateParent(context.Control!);

        // If there is a combobox associated then ask for its requested size
        if (LastComboBox != null)
        {
            if (ActualVisible(LastComboBox))
            {
                preferredSize = LastComboBox.GetPreferredSize(context.DisplayRectangle.Size);

                // Add two pixels, one for the left and right edges that will be padded
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
            LastComboBox?.SetBounds(ClientLocation.X + 1,
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

        // If we do not have a combobox
        if (GroupComboBox?.ComboBox == null)
        {
            // And we are in design time
            if (_ribbon.InDesignMode)
            {
                // Draw rectangle is 1 pixel less per edge
                Rectangle drawRect = ClientRectangle;
                drawRect.Inflate(-1, -1);
                drawRect.Height--;

                // Draw an indication of where the combobox will be
                context!.Graphics.FillRectangle(Brushes.Goldenrod, drawRect);
                context.Graphics.DrawRectangle(Pens.Gold, drawRect);
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
    private void OnContextClick(object? sender, MouseEventArgs e) => GroupComboBox?.OnDesignTimeContextMenu(e);

    private void OnComboBoxPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        var updateLayout = false;
        const bool UPDATE_PAINT = false;

        switch (e.PropertyName)
        {
            case nameof(Enabled):
                UpdateEnabled(LastComboBox);
                break;
            case nameof(Visible):
                UpdateVisible(LastComboBox);
                updateLayout = true;
                break;
            case "CustomControl":
                updateLayout = true;
                break;
        }

        if (updateLayout)
        {
            // If we are on the currently selected tab then...
            if ((GroupComboBox?.RibbonTab != null) &&
                (_ribbon.SelectedTab == GroupComboBox.RibbonTab))
            {
                // ...layout so the visible change is made
                OnNeedPaint(true);
            }
        }

        if (UPDATE_PAINT)
#pragma warning disable 162
        {
            // If this button is actually defined as visible...
            if (GroupComboBox!.Visible || _ribbon.InDesignMode)
            {
                // ...and on the currently selected tab then...
                if ((GroupComboBox.RibbonTab != null) &&
                    (_ribbon.SelectedTab == GroupComboBox.RibbonTab))
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
        get => GroupComboBox?.LastParentControl;
        set => GroupComboBox!.LastParentControl = value;
    }

    private KryptonComboBox? LastComboBox
    {
        get => GroupComboBox?.LastComboBox;
        set => GroupComboBox!.LastComboBox = value;
    }

    private void UpdateParent(Control parentControl)
    {
        // Is there a change in the combobox or a change in 
        // the parent control that is hosting the control...
        if ((parentControl != LastParentControl) ||
            (LastComboBox != GroupComboBox?.ComboBox))
        {
            // We only modify the parent and visible state if processing for correct container
            if ((GroupComboBox!.RibbonContainer!.RibbonGroup!.ShowingAsPopup && (parentControl is VisualPopupGroup)) ||
                (!GroupComboBox.RibbonContainer.RibbonGroup.ShowingAsPopup && parentControl is not VisualPopupGroup))
            {
                // If we have added the custom control to a parent before
                if ((LastComboBox != null) && (LastParentControl != null))
                {
                    // If that control is still a child of the old parent
                    if (LastParentControl.Controls.Contains(LastComboBox))
                    {
                        // Check for a collection that is based on the read only class
                        LastParentControl.Controls.Remove(LastComboBox);
                    }
                }

                // Remember the current control and new parent
                LastComboBox = GroupComboBox.ComboBox;
                LastParentControl = parentControl;

                // If we have a new combobox and parent
                if ((LastComboBox != null) && (LastParentControl != null))
                {
                    // Ensure the control is not in the display area when first added
                    LastComboBox.Location = new Point(-LastComboBox.Width, -LastComboBox.Height);

                    // Check for the correct visible state of the combobox
                    UpdateVisible(LastComboBox);
                    UpdateEnabled(LastComboBox);

                    // Check for a collection that is based on the read only class
                    LastParentControl.Controls.Add(LastComboBox);
                }
            }
        }
    }

    private void UpdateEnabled(Control? c)
    {
        if (c != null)
        {
            // Start with the enabled state of the group element
            var enabled = GroupComboBox!.Enabled;

            // If we have an associated designer setup...
            if (!_ribbon.InDesignHelperMode && (GroupComboBox?.ComboBoxDesigner != null))
            {
                // And we are not using the design helpers, then use the design specified value
                enabled = GroupComboBox.ComboBoxDesigner.DesignEnabled;
            }

            c.Enabled = enabled;
        }
    }

    private bool ActualVisible(Control? c)
    {
        if (c != null)
        {
            // Start with the visible state of the group element
            var visible = GroupComboBox!.Visible;

            // If we have an associated designer setup...
            if (!_ribbon.InDesignHelperMode && (GroupComboBox?.ComboBoxDesigner != null))
            {
                // And we are not using the design helpers, then use the design specified value
                visible = GroupComboBox.ComboBoxDesigner.DesignVisible;
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
            var visible = GroupComboBox!.Visible;

            // If we have an associated designer setup...
            if (!_ribbon.InDesignHelperMode && (GroupComboBox?.ComboBoxDesigner != null))
            {
                // And we are not using the design helpers, then use the design specified value
                visible = GroupComboBox.ComboBoxDesigner.DesignVisible;
            }

            if (visible)
            {
                // Only visible if on the currently selected page
                if ((GroupComboBox?.RibbonTab == null) ||
                    (_ribbon.SelectedTab != GroupComboBox.RibbonTab))
                {
                    visible = false;
                }
                else
                {
                    // Check the owning group is visible
                    if (GroupComboBox.RibbonContainer?.RibbonGroup is { Visible: false } 
                        && !_ribbon.InDesignMode
                       )
                    {
                        visible = false;
                    }
                    else
                    {
                        // Check that the group is not collapsed
                        if (GroupComboBox.RibbonContainer!.RibbonGroup!.IsCollapsed &&
                            ((_ribbon.GetControllerControl(GroupComboBox.ComboBox) is KryptonRibbon) ||
                             (_ribbon.GetControllerControl(GroupComboBox.ComboBox) is VisualPopupMinimized)))
                        {
                            visible = false;
                        }
                        else
                        {
                            // Check that the hierarchy of containers are all visible
                            KryptonRibbonGroupContainer container = GroupComboBox.RibbonContainer;

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
                                container = container.RibbonContainer!;
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
        if (GroupComboBox != null)
        {
            // Change in selected tab requires a retest of the control visibility
            UpdateVisible(LastComboBox);
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