﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon
{
    /// <summary>
    /// Draws a ribbon group custom control.
    /// </summary>
    internal class ViewDrawRibbonGroupCustomControl : ViewComposite,
                                                      IRibbonViewGroupItemView
    {
        #region Instance Fields
        private readonly int NULL_CONTROL_WIDTH; // = 50;
        private readonly KryptonRibbon _ribbon;
        private ViewDrawRibbonGroup _activeGroup;
        private readonly CustomControlController? _controller;
        private readonly NeedPaintHandler _needPaint;
        private GroupItemSize _currentSize;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupCustom class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonCustom">Reference to source custom definition.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewDrawRibbonGroupCustomControl(KryptonRibbon ribbon,
                                                KryptonRibbonGroupCustomControl ribbonCustom,
                                                NeedPaintHandler needPaint)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(ribbonCustom != null);
            Debug.Assert(needPaint != null);

            // Remember incoming references
            _ribbon = ribbon;
            GroupCustomControl = ribbonCustom;
            _needPaint = needPaint;
            _currentSize = GroupCustomControl.ItemSizeCurrent;

            // Hook into the custom control events
            GroupCustomControl.MouseEnterControl += OnMouseEnterControl;
            GroupCustomControl.MouseLeaveControl += OnMouseLeaveControl;

            // Associate this view with the source component (required for design time selection)
            Component = GroupCustomControl;

            if (_ribbon.InDesignMode)
            {
                // At design time we need to know when the user right clicks the label
                ContextClickController controller = new();
                controller.ContextClick += OnContextClick;
                MouseController = controller;
            }

            // Create controller needed for handling focus and key tip actions
            _controller = new CustomControlController(_ribbon, GroupCustomControl, this);
            SourceController = _controller;
            KeyController = _controller;

            // We need to rest visibility of the custom control for each layout cycle
            _ribbon.ViewRibbonManager.LayoutBefore += OnLayoutAction;
            _ribbon.ViewRibbonManager.LayoutAfter += OnLayoutAction;

            // Provide back reference to the custom control definition
            GroupCustomControl.CustomControlView = this;

            // Give paint delegate to label so its palette changes are redrawn
            GroupCustomControl.ViewPaintDelegate = needPaint;

            // Hook into changes in the ribbon custom definition
            GroupCustomControl.PropertyChanged += OnCustomPropertyChanged;
            NULL_CONTROL_WIDTH = (int)(50 * FactorDpiX);
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString() =>
            // Return the class name and instance identifier
            @"ViewDrawRibbonGroupCustom:" + Id;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (GroupCustomControl != null)
                {
                    // Must unhook to prevent memory leaks
                    GroupCustomControl.MouseEnterControl -= OnMouseEnterControl;
                    GroupCustomControl.MouseLeaveControl -= OnMouseLeaveControl;
                    GroupCustomControl.ViewPaintDelegate = null;
                    GroupCustomControl.PropertyChanged -= OnCustomPropertyChanged;
                    _ribbon.ViewRibbonManager.LayoutAfter -= OnLayoutAction;
                    _ribbon.ViewRibbonManager.LayoutBefore -= OnLayoutAction;

                    // Remove association with definition
                    GroupCustomControl.CustomControlView = null;
                    GroupCustomControl = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region GroupCustomControl
        /// <summary>
        /// Gets access to the owning group custom instance.
        /// </summary>
        public KryptonRibbonGroupCustomControl GroupCustomControl { get; private set; }

        #endregion

        #region LostFocus
        /// <summary>
        /// Source control has lost the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public override void LostFocus(Control c)
        {
            // Ask ribbon to shift focus to the hidden control
            _ribbon.HideFocus(GroupCustomControl.CustomControl);
            base.LostFocus(c);
        }
        #endregion

        #region GetFirstFocusItem
        /// <summary>
        /// Gets the first focus item from the container.
        /// </summary>
        /// <returns>ViewBase of item; otherwise false.</returns>
        public ViewBase? GetFirstFocusItem()
        {
            if (GroupCustomControl is { Visible: true, LastCustomControl: { CanSelect: true } })
            {
                return this;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region GetLastFocusItem
        /// <summary>
        /// Gets the last focus item from the item.
        /// </summary>
        /// <returns>ViewBase of item; otherwise false.</returns>
        public ViewBase? GetLastFocusItem()
        {
            if (GroupCustomControl is { Visible: true, LastCustomControl: { CanSelect: true } })
            {
                return this;
            }
            else
            {
                return null;
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
        public ViewBase? GetNextFocusItem(ViewBase current, ref bool matched)
        {
            // Do we match the current item?
            matched = current == this;
            return null;
        }
        #endregion

        #region GetPreviousFocusItem
        /// <summary>
        /// Gets the previous focus item based on the current item as provided.
        /// </summary>
        /// <param name="current">The view that is currently focused.</param>
        /// <param name="matched">Has the current focus item been matched yet.</param>
        /// <returns>ViewBase of item; otherwise false.</returns>
        public ViewBase? GetPreviousFocusItem(ViewBase current, ref bool matched)
        {
            // Do we match the current item?
            matched = current == this;
            return null;
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
            if (Visible && LastCustomControl is { CanFocus: true })
            {
                // Get the screen location of the button
                Rectangle viewRect = _ribbon.KeyTipToScreen(this);

                // Determine the screen position of the key tip
                Point screenPt = Point.Empty;

                // Determine the screen position of the key tip dependant on item location/size
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

                keyTipList.Add(new KeyTipInfo(GroupCustomControl.Enabled, 
                                              GroupCustomControl.KeyTip,
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
        public void SetGroupItemSize(GroupItemSize size)
        {
            _currentSize = size;
        }

        /// <summary>
        /// Reset the group item size to the item definition.
        /// </summary>
        public void ResetGroupItemSize()
        {
            _currentSize = GroupCustomControl.ItemSizeCurrent;
        }

        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            Size preferredSize = Size.Empty;

            // Ensure the control has the correct parent
            UpdateParent(context.Control);

            // If there is a custom control associated then ask for its requested size
            if (LastCustomControl != null)
            {
                if (ActualVisible(LastCustomControl))
                {
                    preferredSize = LastCustomControl.GetPreferredSize(context.DisplayRectangle.Size);

                    // Add two pixels, one for the left and right edges that will be padded
                    preferredSize.Width += 2;
                }
            }
            else
            {
                preferredSize.Width = NULL_CONTROL_WIDTH;
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
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // We take on all the available display area
            ClientRectangle = context.DisplayRectangle;

            // Are we allowed to change the layout of controls?
            if (!context.ViewManager.DoNotLayoutControls)
            {
                // If we have an actual control, position it with a pixel padding all around
                LastCustomControl?.SetBounds(ClientLocation.X + 1,
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
        public override void Render(RenderContext context)
        {
            Debug.Assert(context != null);

            // If we do not have a custom control
            if (GroupCustomControl.CustomControl == null)
            {
                // And we are in design time
                if (_ribbon.InDesignMode)
                {
                    // Draw rectangle is 1 pixel less per edge
                    Rectangle drawRect = ClientRectangle;
                    drawRect.Inflate(-1, -1);
                    drawRect.Height--;

                    // Draw an indication of where the custom control will be
                    context.Graphics.FillRectangle(Brushes.Salmon, drawRect);
                    context.Graphics.DrawRectangle(Pens.Red, drawRect);
                }
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the NeedPaint event.
        /// </summary>
        /// <param name="needLayout">Does the palette change require a layout.</param>
        protected virtual void OnNeedPaint(bool needLayout)
        {
            OnNeedPaint(needLayout, Rectangle.Empty);
        }

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
        private void OnContextClick(object sender, MouseEventArgs e)
        {
            GroupCustomControl.OnDesignTimeContextMenu(e);
        }

        private void OnCustomPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var updateLayout = false;
            const bool UPDATE_PAINT = false;

            switch (e.PropertyName)
            {
                case nameof(Enabled):
                    UpdateEnabled(LastCustomControl);
                    break;
                case nameof(Visible):
                    UpdateVisible(LastCustomControl);
                    updateLayout = true;
                    break;
                case "CustomControl":
                    updateLayout = true;
                    break;
            }

            if (updateLayout)
            {
                // If we are on the currently selected tab then...
                if ((GroupCustomControl.RibbonTab != null) &&
                    (_ribbon.SelectedTab == GroupCustomControl.RibbonTab))
                {
                    // ...layout so the visible change is made
                    OnNeedPaint(true);
                }
            }

            if (UPDATE_PAINT)
#pragma warning disable 162
            {
                // If this button is actually defined as visible...
                if (GroupCustomControl.Visible || _ribbon.InDesignMode)
                {
                    // ...and on the currently selected tab then...
                    if ((GroupCustomControl.RibbonTab != null) &&
                        (_ribbon.SelectedTab == GroupCustomControl.RibbonTab))
                    {
                        // ...repaint it right now
                        OnNeedPaint(false, ClientRectangle);
                    }
                }
            }
#pragma warning restore 162
        }

        private Control LastParentControl
        {
            get => GroupCustomControl.LastParentControl;
            set => GroupCustomControl.LastParentControl = value;
        }

        private Control LastCustomControl
        {
            get => GroupCustomControl.LastCustomControl;
            set => GroupCustomControl.LastCustomControl = value;
        }

        private void UpdateParent(Control parentControl)
        {
            // Is there a change in the custom control or a change in 
            // the parent control that is hosting the control...
            if ((parentControl != LastParentControl) ||
                (LastCustomControl != GroupCustomControl.CustomControl))
            {
                // We only modify the parent and visible state if processing for correct container
                if ((GroupCustomControl.RibbonContainer.RibbonGroup.ShowingAsPopup && (parentControl is VisualPopupGroup)) ||
                    (!GroupCustomControl.RibbonContainer.RibbonGroup.ShowingAsPopup && parentControl is not VisualPopupGroup))
                {
                    // If we have added the custrom control to a parent before
                    if ((LastCustomControl != null) && (LastParentControl != null))
                    {
                        // If that control is still a child of the old parent
                        if (LastParentControl.Controls.Contains(LastCustomControl))
                        {
                            // Check for a collection that is based on the read only class
                            LastParentControl.Controls.Remove(LastCustomControl);
                        }
                    }

                    // Remember the current control and new parent
                    LastCustomControl = GroupCustomControl.CustomControl;
                    LastParentControl = parentControl;

                    // If we have a new custom control and parent
                    if ((LastCustomControl != null) && (LastParentControl != null))
                    {
                        // Ensure the control is not in the display area when first added
                        LastCustomControl.Location = new Point(-LastCustomControl.Width, -LastCustomControl.Height);

                        // Check for the correct visible/enabled state of the custom control
                        UpdateVisible(LastCustomControl);
                        UpdateEnabled(LastCustomControl);

                        // Check for a collection that is based on the read only class
                        LastParentControl.Controls.Add(LastCustomControl);
                    }
                }
            }
        }

        private void UpdateEnabled(Control c)
        {
            if (c != null)
            {
                // Start with the enabled state of the group element
                var enabled = GroupCustomControl.Enabled;

                // If we have an associated designer setup...
                if (!_ribbon.InDesignHelperMode && (GroupCustomControl.CustomControlDesigner != null))
                {
                    // And we are not using the design helpers, then use the design specified value
                    enabled = GroupCustomControl.CustomControlDesigner.DesignEnabled;
                }

                c.Enabled = enabled;
            }
        }

        private bool ActualVisible(Control c)
        {
            if (c != null)
            {
                // Start with the visible state of the group element
                var visible = GroupCustomControl.Visible;

                // If we have an associated designer setup...
                if (!_ribbon.InDesignHelperMode && (GroupCustomControl.CustomControlDesigner != null))
                {
                    // And we are not using the design helpers, then use the design specified value
                    visible = GroupCustomControl.CustomControlDesigner.DesignVisible;
                }

                return visible;
            }

            return false;
        }

        private void UpdateVisible(Control c)
        {
            if (c != null)
            {
                // Start with the visible state of the group element
                var visible = GroupCustomControl.Visible;

                // If we have an associated designer setup...
                if (!_ribbon.InDesignHelperMode && (GroupCustomControl.CustomControlDesigner != null))
                {
                    // And we are not using the design helpers, then use the design specified value
                    visible = GroupCustomControl.CustomControlDesigner.DesignVisible;
                }

                if (visible)
                {
                    // Only visible if on the currently selected page
                    if ((GroupCustomControl.RibbonTab == null) ||
                        (_ribbon.SelectedTab != GroupCustomControl.RibbonTab))
                    {
                        visible = false;
                    }
                    else
                    {
                        // Check the owning group is visible
                        if ((GroupCustomControl.RibbonContainer?.RibbonGroup != null) && !GroupCustomControl.RibbonContainer.RibbonGroup.Visible && !_ribbon.InDesignMode)
                        {
                            visible = false;
                        }
                        else
                        {
                            // Check that the group is not collapsed
                            if (GroupCustomControl.RibbonContainer.RibbonGroup.IsCollapsed &&
                                ((_ribbon.GetControllerControl(GroupCustomControl.LastCustomControl) is KryptonRibbon) ||
                                 (_ribbon.GetControllerControl(GroupCustomControl.LastCustomControl) is VisualPopupMinimized)))
                            {
                                visible = false;
                            }
                            else
                            {
                                // Check that the hierarchy of containers are all visible
                                KryptonRibbonGroupContainer container = GroupCustomControl.RibbonContainer;

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

        private void OnLayoutAction(object sender, EventArgs e)
        {
            // If not disposed then we still have a element reference
            if (GroupCustomControl != null)
            {
                // Change in selected tab requires a retest of the control visibility/enabled
                UpdateVisible(LastCustomControl);
                UpdateEnabled(LastCustomControl);
            }
        }

        private void OnMouseEnterControl(object sender, EventArgs e)
        {
            // Reset the active group setting
            _activeGroup = null;

            // Find the parent group instance
            ViewBase parent = Parent;

            // Keep going till we get to the top or find a group
            while (parent != null)
            {
                if (parent is ViewDrawRibbonGroup popGroup)
                {
                    _activeGroup = popGroup;
                    break;
                }

                // Move up a level
                parent = parent.Parent;
            }

            // If we found a group we are inside
            if (_activeGroup != null)
            {
                _activeGroup.Tracking = true;
                _needPaint(this, new NeedLayoutEventArgs(false, _activeGroup.ClientRectangle));
            }
        }

        private void OnMouseLeaveControl(object sender, EventArgs e)
        {
            // If we have a cached group we made active
            if (_activeGroup != null)
            {
                _activeGroup.Tracking = false;
                _needPaint(this, new NeedLayoutEventArgs(false, _activeGroup.ClientRectangle));
                _activeGroup = null;
            }
        }
        #endregion
    }
}
