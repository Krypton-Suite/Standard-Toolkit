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
/// Draws a ribbon group button.
/// </summary>
internal class ViewDrawRibbonGroupButton : ViewComposite,
    IRibbonViewGroupItemView
{
    #region Instance Fields
    private readonly Padding _largeImagePadding; // = new(3, 2, 3, 3);
    private readonly Padding _smallImagePadding; // = new(3, 3, 3, 3);
    private readonly KryptonRibbon _ribbon;
    private readonly NeedPaintHandler _needPaint;
    private ViewDrawRibbonGroupButtonBackBorder _viewLarge;
    private ViewLayoutRibbonRowCenter _viewLargeCenter;
    private ViewDrawRibbonGroupButtonImage _viewLargeImage;
    private ViewDrawRibbonGroupButtonText _viewLargeText1;
    private ViewDrawRibbonGroupButtonText _viewLargeText2;
    private ViewDrawRibbonDropArrow _viewLargeDropArrow;
    private ViewLayoutRibbonSeparator _viewLargeText2Sep1;
    private ViewLayoutRibbonSeparator _viewLargeText2Sep2;
    private ViewDrawRibbonGroupButtonBackBorder _viewMediumSmall;
    private ViewLayoutRibbonRowCenter _viewMediumSmallCenter;
    private ViewDrawRibbonGroupButtonImage _viewMediumSmallImage;
    private ViewDrawRibbonGroupButtonText _viewMediumSmallText1;
    private ViewDrawRibbonGroupButtonText _viewMediumSmallText2;
    private ViewDrawRibbonDropArrow _viewMediumSmallDropArrow;
    private ViewLayoutRibbonSeparator _viewMediumSmallText2Sep2;
    private ViewLayoutRibbonSeparator _viewMediumSmallText2Sep3;
    private GroupItemSize _currentSize;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonGroupButton class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="ribbonButton">Reference to source button definition.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ViewDrawRibbonGroupButton([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] KryptonRibbonGroupButton? ribbonButton,
        [DisallowNull] NeedPaintHandler? needPaint)
    {
        Debug.Assert(ribbon is not null);
        Debug.Assert(ribbonButton is not null);
        Debug.Assert(needPaint is not null);

        // Remember incoming references
        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));
        GroupButton = ribbonButton ?? throw new ArgumentNullException(nameof(ribbonButton));
        _needPaint = needPaint ?? throw new ArgumentNullException(nameof(needPaint));
        _currentSize = GroupButton.ItemSizeCurrent;

        _largeImagePadding = new Padding((int)(3 * FactorDpiX), (int)(2 * FactorDpiY), (int)(3 * FactorDpiX), (int)(3 * FactorDpiY));
        _smallImagePadding = new Padding((int)(3 * FactorDpiX), (int)(3 * FactorDpiY), (int)(3 * FactorDpiX), (int)(3 * FactorDpiY));


        // Associate this view with the source component (required for design time selection)
        Component = GroupButton;

        // Create the different views for different sizes of the button
        CreateLargeButtonView();
        CreateMediumSmallButtonView();

        // Update all views to reflect current button state
        UpdateEnabledState();
        UpdateCheckedState();
        UpdateDropDownState();
        UpdateItemSizeState();

        // Hook into changes in the ribbon button definition
        GroupButton.PropertyChanged += OnButtonPropertyChanged;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonGroupButton:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (GroupButton != null)
            {
                // Must unhook to prevent memory leaks
                GroupButton.PropertyChanged -= OnButtonPropertyChanged;

                // Remove association with definition
                GroupButton.ButtonView = null;
                GroupButton = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region GroupButton
    /// <summary>
    /// Gets access to the connected button definition.
    /// </summary>
    public KryptonRibbonGroupButton? GroupButton { get; private set; }

    #endregion

    #region GetFirstFocusItem
    /// <summary>
    /// Gets the first focus item from the item.
    /// </summary>
    /// <returns>ViewBase of item; otherwise false.</returns>
    public ViewBase GetFirstFocusItem()
    {
        // Only take focus if we are visible and enabled
        if (GroupButton is { Visible: true, Enabled: true })
        {
            return _viewLarge == GroupButton.ButtonView ? _viewLarge : _viewMediumSmall;
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
        // Only take focus if we are visible and enabled
        if (GroupButton is { Visible: true, Enabled: true })
        {
            return _viewLarge == GroupButton.ButtonView ? _viewLarge : _viewMediumSmall;
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
        matched = (current == _viewLarge) || (current == _viewMediumSmall);
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
        matched = (current == _viewLarge) || (current == _viewMediumSmall);
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
        // Only provide a key tip if we are visible
        if (Visible)
        {
            // Get the screen location of the button
            Rectangle viewRect = _ribbon!.KeyTipToScreen(this[0]);

            var screenPt = Point.Empty;
            GroupButtonController? controller = null;

            // Determine the screen position of the key tip dependant on item location/size
            switch (_currentSize)
            {
                case GroupItemSize.Large:
                    screenPt = new Point(viewRect.Left + (viewRect.Width / 2), viewRect.Bottom);
                    controller = _viewLarge.Controller;
                    break;
                case GroupItemSize.Medium:
                case GroupItemSize.Small:
                    screenPt = _ribbon.CalculatedValues.KeyTipRectToPoint(viewRect, lineHint);
                    controller = _viewMediumSmall.Controller;
                    break;
            }

            keyTipList.Add(new KeyTipInfo(GroupButton!.Enabled, GroupButton.KeyTip,
                screenPt, this[0]!.ClientRectangle, controller));
        }
    }
    #endregion

    #region Layout
    /// <summary>
    /// Override the group item size if possible.
    /// </summary>
    /// <param name="size">New size to use.</param>
    public void SetGroupItemSize(GroupItemSize size) => UpdateItemSizeState(size);

    /// <summary>
    /// Reset the group item size to the item definition.
    /// </summary>
    public void ResetGroupItemSize() => UpdateItemSizeState();

    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        var drawNonTrackingAreas = _ribbon!.RibbonShape is not PaletteRibbonShape.Office2010 or PaletteRibbonShape.Office2013 or PaletteRibbonShape.Microsoft365 or PaletteRibbonShape.VisualStudio;

        // Update the views with the type of button being used
        _viewLarge.ButtonType = GroupButton!.ButtonType;
        _viewLarge.DrawNonTrackingAreas = drawNonTrackingAreas;
        _viewMediumSmall.ButtonType = GroupButton.ButtonType;
        _viewMediumSmall.DrawNonTrackingAreas = drawNonTrackingAreas;

        // Get the preferred size of button view
        Size preferredSize = base.GetPreferredSize(context);

        preferredSize.Height = _currentSize == GroupItemSize.Large
            ? _ribbon.CalculatedValues.GroupTripleHeight
            : _ribbon.CalculatedValues.GroupLineHeight;

        return preferredSize;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext? context)
    {
        Debug.Assert(context is not null);

        // Update our enabled and checked state
        UpdateEnabledState();
        UpdateCheckedState();
        UpdateDropDownState();

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;

        // Let child elements layout in given space
        base.Layout(context);

        // For split buttons we need to calculate the split button areas
        if (GroupButton!.ButtonType == GroupButtonType.Split)
        {
            // Find the start positions of the split areas for both views
            var largeSplitTop = _viewLargeImage.ClientRectangle.Bottom + 1;
            var mediumSmallSplitRight = _viewMediumSmallText2Sep2.ClientLocation.X;

            // Update the background/border view so it can draw appropriately
            _viewLarge.SplitRectangle = new Rectangle(ClientLocation.X, largeSplitTop, ClientWidth, ClientRectangle.Bottom - largeSplitTop);
            _viewMediumSmall.SplitRectangle = new Rectangle(mediumSmallSplitRight, ClientLocation.Y, ClientRectangle.Right - mediumSmallSplitRight, ClientHeight);
        }
        else
        {
            _viewLarge.SplitRectangle = Rectangle.Empty;
            _viewMediumSmall.SplitRectangle = Rectangle.Empty;
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
        if (_needPaint is not null)
        {
            _needPaint(this, new NeedLayoutEventArgs(needLayout, invalidRect));

            if (needLayout)
            {
                _ribbon!.PerformLayout();
            }
        }
    }
    #endregion

    #region Implementation
    private void CreateLargeButtonView()
    {
        // Create the background and border view
        _viewLarge = new ViewDrawRibbonGroupButtonBackBorder(_ribbon!, GroupButton!,
            _ribbon!.StateCommon.RibbonGroupButton.PaletteBack,
            _ribbon.StateCommon.RibbonGroupButton.PaletteBorder!,
            false, _needPaint)
        {
            SplitVertical = true
        };
        _viewLarge.Click += OnLargeButtonClick;
        _viewLarge.DropDown += OnLargeButtonDropDown;

        if (_ribbon.InDesignMode)
        {
            _viewLarge.ContextClick += OnContextClick;
        }

        // Create the layout docker for the contents of the button
        var contentLayout = new ViewLayoutDocker();

        // Add the large button at the top
        _viewLargeImage = new ViewDrawRibbonGroupButtonImage(_ribbon, GroupButton!, true);
        var largeImagePadding = new ViewLayoutRibbonCenterPadding(_largeImagePadding)
        {
            _viewLargeImage
        };
        contentLayout.Add(largeImagePadding, ViewDockStyle.Top);

        // Add the first line of text
        _viewLargeText1 = new ViewDrawRibbonGroupButtonText(_ribbon, GroupButton!, true);
        contentLayout.Add(_viewLargeText1, ViewDockStyle.Bottom);

        // Add the second line of text
        _viewLargeCenter = new ViewLayoutRibbonRowCenter();
        _viewLargeText2 = new ViewDrawRibbonGroupButtonText(_ribbon, GroupButton!, false);
        _viewLargeDropArrow = new ViewDrawRibbonDropArrow(_ribbon);
        _viewLargeText2Sep1 = new ViewLayoutRibbonSeparator(4, false);
        _viewLargeText2Sep2 = new ViewLayoutRibbonSeparator(4, false);
        _viewLargeCenter.Add(_viewLargeText2);
        _viewLargeCenter.Add(_viewLargeText2Sep1);
        _viewLargeCenter.Add(_viewLargeDropArrow);
        _viewLargeCenter.Add(_viewLargeText2Sep2);
        contentLayout.Add(_viewLargeCenter, ViewDockStyle.Bottom);

        // Add a 1 pixel separator at bottom of button before the text
        contentLayout.Add(new ViewLayoutRibbonSeparator(1, false), ViewDockStyle.Bottom);

        // Add the content into the background and border
        _viewLarge.Add(contentLayout);

        // Create controller for intercepting events to determine tool tip handling
        _viewLarge.MouseController = new ToolTipController(_ribbon.TabsArea!.ButtonSpecManager!.ToolTipManager!,
            _viewLarge, _viewLarge.MouseController);
    }

    private void CreateMediumSmallButtonView()
    {
        // Create the background and border view
        _viewMediumSmall = new ViewDrawRibbonGroupButtonBackBorder(_ribbon!, GroupButton!,
            _ribbon!.StateCommon.RibbonGroupButton.PaletteBack,
            _ribbon.StateCommon.RibbonGroupButton.PaletteBorder!,
            false, _needPaint)
        {
            SplitVertical = false
        };
        _viewMediumSmall.Click += OnMediumSmallButtonClick;
        _viewMediumSmall.DropDown += OnMediumSmallButtonDropDown;

        if (_ribbon.InDesignMode)
        {
            _viewMediumSmall.ContextClick += OnContextClick;
        }

        // Create the layout docker for the contents of the button
        var contentLayout = new ViewLayoutDocker();

        // Create the image and drop-down content
        _viewMediumSmallImage = new ViewDrawRibbonGroupButtonImage(_ribbon, GroupButton!, false);
        _viewMediumSmallText1 = new ViewDrawRibbonGroupButtonText(_ribbon, GroupButton!, true);
        _viewMediumSmallText2 = new ViewDrawRibbonGroupButtonText(_ribbon, GroupButton!, false);
        _viewMediumSmallDropArrow = new ViewDrawRibbonDropArrow(_ribbon);
        _viewMediumSmallText2Sep2 = new ViewLayoutRibbonSeparator(3, false);
        _viewMediumSmallText2Sep3 = new ViewLayoutRibbonSeparator(3, false);
        var imagePadding = new ViewLayoutRibbonCenterPadding(_smallImagePadding)
        {
            _viewMediumSmallImage
        };

        // Layout the content in the center of a row
        _viewMediumSmallCenter = new ViewLayoutRibbonRowCenter
        {
            imagePadding,
            _viewMediumSmallText1,
            _viewMediumSmallText2,
            _viewMediumSmallText2Sep2,
            _viewMediumSmallDropArrow,
            _viewMediumSmallText2Sep3
        };

        // Use content as only fill item
        contentLayout.Add(_viewMediumSmallCenter, ViewDockStyle.Fill);

        // Add the content into the background and border
        _viewMediumSmall.Add(contentLayout);

        // Create controller for intercepting events to determine tool tip handling
        _viewMediumSmall.MouseController = new ToolTipController(_ribbon.TabsArea!.ButtonSpecManager!.ToolTipManager!,
            _viewMediumSmall, _viewMediumSmall.MouseController);
    }

    private void DefineRootView(ViewBase view)
    {
        // Remove any existing view
        Clear();

        // Use the provided view
        Add(view);

        // Provide back reference to the button definition
        GroupButton!.ButtonView = view;
    }

    private void UpdateEnabledState()
    {
        // Get the correct enabled state from the button definition
        var buttonEnabled = GroupButton!.Enabled;
        if (GroupButton.KryptonCommand != null)
        {
            buttonEnabled = GroupButton.KryptonCommand.Enabled;
        }

        // Take into account the ribbon state and mode
        var enabled = _ribbon!.InDesignHelperMode || (buttonEnabled && _ribbon.Enabled);

        // Update enabled for the large button view
        _viewLarge.Enabled = enabled;
        _viewLargeImage.Enabled = enabled;
        _viewLargeText1.Enabled = enabled;
        _viewLargeText2.Enabled = enabled;
        _viewLargeDropArrow.Enabled = enabled;

        // Update enabled for the medium/small button view
        _viewMediumSmall.Enabled = enabled;
        _viewMediumSmallText1.Enabled = enabled;
        _viewMediumSmallText2.Enabled = enabled;
        _viewMediumSmallImage.Enabled = enabled;
        _viewMediumSmallDropArrow.Enabled = enabled;
    }

    private void UpdateCheckedState()
    {
        var checkedState = false;

        // Only show as checked if also a check type button
        if (GroupButton!.ButtonType == GroupButtonType.Check)
        {
            checkedState = GroupButton.KryptonCommand?.Checked ?? GroupButton.Checked;
        }

        _viewLarge.Checked = checkedState;
        _viewMediumSmall.Checked = checkedState;
    }

    private void UpdateDropDownState()
    {
        var dropDown = GroupButton!.ButtonType == GroupButtonType.DropDown;
        var splitButton = GroupButton.ButtonType == GroupButtonType.Split;

        // Only show text line 2 separators is a drop-down is showing with no text
        var separators = (dropDown || splitButton) && (!string.IsNullOrEmpty(GroupButton.TextLine2));

        // Update large view
        _viewLargeDropArrow.Visible = dropDown || splitButton;
        _viewLargeText2Sep1.Visible = separators;
        _viewLargeText2Sep2.Visible = separators;

        // Update medium/small view
        _viewMediumSmallText2Sep2.Visible = splitButton;
        _viewMediumSmallDropArrow.Visible = dropDown || splitButton;
        _viewMediumSmallText2Sep3.Visible = dropDown || splitButton;
    }

    private void UpdateItemSizeState() => UpdateItemSizeState(GroupButton!.ItemSizeCurrent);

    private void UpdateItemSizeState(GroupItemSize size)
    {
        _currentSize = size;

        switch (size)
        {
            case GroupItemSize.Small:
            case GroupItemSize.Medium:
                var show = size == GroupItemSize.Medium;
                _viewMediumSmallCenter.CurrentSize = size;
                _viewMediumSmallText1.Visible = show;
                _viewMediumSmallText2.Visible = show;
                DefineRootView(_viewMediumSmall);
                break;
            case GroupItemSize.Large:
                _viewLargeCenter.CurrentSize = size;
                DefineRootView(_viewLarge);
                break;
        }
    }

    private void OnLargeButtonClick(object? sender, EventArgs e) => GroupButton!.PerformClick(_viewLarge.FinishDelegate);

    private void OnLargeButtonDropDown(object? sender, EventArgs e) => GroupButton!.PerformDropDown(_viewLarge.FinishDelegate);

    private void OnMediumSmallButtonClick(object? sender, EventArgs e) => GroupButton!.PerformClick(_viewMediumSmall.FinishDelegate);

    private void OnMediumSmallButtonDropDown(object? sender, EventArgs e) => GroupButton!.PerformDropDown(_viewMediumSmall.FinishDelegate);

    private void OnContextClick(object? sender, MouseEventArgs e) => GroupButton!.OnDesignTimeContextMenu(e);

    private void OnButtonPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        var updateLayout = false;
        var updatePaint = false;

        switch (e.PropertyName)
        {
            case nameof(Visible):
                updateLayout = true;
                break;
            case "TextLine1":
                _viewLargeText1.MakeDirty();
                _viewMediumSmallText1.MakeDirty();
                updateLayout = true;
                break;
            case "TextLine2":
                _viewLargeText2.MakeDirty();
                _viewMediumSmallText2.MakeDirty();
                UpdateDropDownState();
                updateLayout = true;
                break;
            case "ButtonType":
                UpdateDropDownState();
                updateLayout = true;
                break;
            case "Checked":
                UpdateCheckedState();
                updatePaint = true;
                break;
            case nameof(Enabled):
                UpdateEnabledState();
                updatePaint = true;
                break;
            case "ImageLarge":
            case "ImageSmall":
                updatePaint = true;
                break;
            case "ItemSizeMinimum":
            case "ItemSizeMaximum":
            case "ItemSizeCurrent":
                UpdateItemSizeState();
                updateLayout = true;
                break;
            case nameof(KryptonCommand):
                _viewLargeText1.MakeDirty();
                _viewLargeText2.MakeDirty();
                _viewMediumSmallText1.MakeDirty();
                _viewMediumSmallText2.MakeDirty();
                UpdateEnabledState();
                UpdateCheckedState();
                updateLayout = true;
                break;
        }

        if (updateLayout)
        {
            // If we are on the currently selected tab then...
            if ((GroupButton!.RibbonTab != null) &&
                (_ribbon!.SelectedTab == GroupButton.RibbonTab))
            {
                // ...layout so the visible change is made
                OnNeedPaint(true);
            }
        }

        if (updatePaint)
        {
            // If this button is actually defined as visible...
            if (GroupButton!.Visible || _ribbon!.InDesignMode)
            {
                // ...and on the currently selected tab then...
                if ((GroupButton.RibbonTab != null) &&
                    (_ribbon!.SelectedTab == GroupButton.RibbonTab))
                {
                    // ...repaint it right now
                    OnNeedPaint(false, ClientRectangle);
                }
            }
        }
    }
    #endregion
}