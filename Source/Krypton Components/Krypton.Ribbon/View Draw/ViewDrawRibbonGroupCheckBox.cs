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
/// Draws a ribbon group check box.
/// </summary>
internal class ViewDrawRibbonGroupCheckBox : ViewComposite,
    IRibbonViewGroupItemView,
    IContentValues
{
    #region Instance Fields
    private readonly Padding _largeImagePadding; // = new(3, 2, 3, 3);
    private readonly Padding _smallImagePadding; // = new(3);
    private readonly KryptonRibbon _ribbon;
    private ViewLayoutRibbonCheckBox _viewLarge;
    private ViewDrawRibbonGroupCheckBoxImage _viewLargeImage;
    private ViewDrawRibbonGroupCheckBoxText _viewLargeText1;
    private ViewDrawRibbonGroupCheckBoxText _viewLargeText2;
    private GroupCheckBoxController? _viewLargeController;
    private readonly EventHandler? _finishDelegateLarge;
    private ViewLayoutRibbonCheckBox _viewMediumSmall;
    private ViewLayoutRibbonRowCenter _viewMediumSmallCenter;
    private ViewDrawRibbonGroupCheckBoxImage _viewMediumSmallImage;
    private ViewDrawRibbonGroupCheckBoxText _viewMediumSmallText1;
    private ViewDrawRibbonGroupCheckBoxText _viewMediumSmallText2;
    private GroupCheckBoxController? _viewMediumSmallController;
    private readonly EventHandler? _finishDelegateMediumSmall;
    private readonly NeedPaintHandler? _needPaint;
    private GroupItemSize _currentSize;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonGroupCheckBox class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="ribbonCheckBox">Reference to source check box definition.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ViewDrawRibbonGroupCheckBox([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] KryptonRibbonGroupCheckBox? ribbonCheckBox,
        [DisallowNull] NeedPaintHandler? needPaint)
    {
        Debug.Assert(ribbon is not null);
        Debug.Assert(ribbonCheckBox is not null);
        Debug.Assert(needPaint is not null);

        // Remember incoming references
        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));
        GroupCheckBox = ribbonCheckBox ?? throw new ArgumentNullException(nameof(ribbonCheckBox));
        _needPaint = needPaint ?? throw new ArgumentNullException(nameof(needPaint));
        _currentSize = GroupCheckBox.ItemSizeCurrent;

        // Create delegate used to process end of click action
        _finishDelegateLarge = ActionFinishedLarge;
        _finishDelegateMediumSmall = ActionFinishedMediumSmall;

        // Associate this view with the source component (required for design time selection)
        Component = GroupCheckBox;

        // Create the different views for different sizes of the check box
        CreateLargeCheckBoxView();
        CreateMediumSmallCheckBoxView();

        // Update all views to reflect current check box state
        UpdateEnabledState();
        UpdateCheckState();
        UpdateItemSizeState();

        // Hook into changes in the ribbon check box definition
        GroupCheckBox.PropertyChanged += OnCheckBoxPropertyChanged;
        _largeImagePadding = new Padding((int)(3 * FactorDpiX), (int)(2 * FactorDpiY), (int)(3 * FactorDpiX), (int)(3 * FactorDpiY));
        _smallImagePadding = new Padding((int)(3 * FactorDpiX), (int)(3 * FactorDpiY), (int)(3 * FactorDpiX), (int)(3 * FactorDpiY));
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonGroupCheckBox:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (GroupCheckBox != null!)
            {
                // Must unhook to prevent memory leaks
                GroupCheckBox.PropertyChanged -= OnCheckBoxPropertyChanged;

                // Remove association with definition
                GroupCheckBox.CheckBoxView = null!;
                GroupCheckBox = null!;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region GroupCheckBox
    /// <summary>
    /// Gets access to the connected check box definition.
    /// </summary>
    public KryptonRibbonGroupCheckBox GroupCheckBox { get; private set; }

    #endregion

    #region GetFirstFocusItem
    /// <summary>
    /// Gets the first focus item from the item.
    /// </summary>
    /// <returns>ViewBase of item; otherwise false.</returns>
    public ViewBase GetFirstFocusItem()
    {
        // Only take focus if we are visible and enabled
        if (GroupCheckBox is { Visible: true, Enabled: true })
        {
            return _viewLarge == GroupCheckBox.CheckBoxView ? _viewLarge : _viewMediumSmall;
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
        if (GroupCheckBox is { Visible: true, Enabled: true })
        {
            return _viewLarge == GroupCheckBox.CheckBoxView ? _viewLarge : _viewMediumSmall;
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
            // Get the screen location of the check box
            Rectangle viewRect = _ribbon.KeyTipToScreen(this[0]);

            var screenPt = Point.Empty;
            GroupCheckBoxController? controller = null;

            // Determine the screen position of the key tip dependant on item location/size
            switch (_currentSize)
            {
                case GroupItemSize.Large:
                    screenPt = new Point(viewRect.Left + (viewRect.Width / 2), viewRect.Bottom);
                    controller = _viewLargeController;
                    break;
                case GroupItemSize.Medium:
                case GroupItemSize.Small:
                    screenPt = _ribbon.CalculatedValues.KeyTipRectToPoint(viewRect, lineHint);
                    controller = _viewMediumSmallController;
                    break;
            }

            keyTipList.Add(new KeyTipInfo(GroupCheckBox.Enabled, GroupCheckBox.KeyTip, 
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
        // Get the preferred size of check box view
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
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Update our enabled and checked state
        UpdateEnabledState();
        UpdateCheckState();
        UpdateItemSizeState();

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;

        // Let child elements layout in given space
        base.Layout(context);
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
    private void CreateLargeCheckBoxView()
    {
        // Create the layout docker for the contents of the button
        _viewLarge = new ViewLayoutRibbonCheckBox();

        // Add the large button at the top
        _viewLargeImage = new ViewDrawRibbonGroupCheckBoxImage(_ribbon, GroupCheckBox, true);
        var largeImagePadding = new ViewLayoutRibbonCenterPadding(_largeImagePadding)
        {
            _viewLargeImage
        };
        _viewLarge.Add(largeImagePadding, ViewDockStyle.Top);

        // Add the first line of text
        _viewLargeText1 = new ViewDrawRibbonGroupCheckBoxText(_ribbon, GroupCheckBox, true);
        _viewLarge.Add(_viewLargeText1, ViewDockStyle.Bottom);

        // Add the second line of text
        _viewLargeText2 = new ViewDrawRibbonGroupCheckBoxText(_ribbon, GroupCheckBox, false);
        _viewLarge.Add(_viewLargeText2, ViewDockStyle.Bottom);

        // Add a 1 pixel separator at bottom of button before the text
        _viewLarge.Add(new ViewLayoutRibbonSeparator(1, false), ViewDockStyle.Bottom);

        // Create controller for handling mouse, keyboard and focus
        _viewLargeController = new GroupCheckBoxController(_ribbon, _viewLarge, _viewLargeImage, _needPaint!);
        _viewLargeController.Click += OnLargeCheckBoxClick;
        _viewLargeController.ContextClick += OnContextClick;
        _viewLarge.MouseController = _viewLargeController;
        _viewLarge.SourceController = _viewLargeController;
        _viewLarge.KeyController = _viewLargeController;

        // Create controller for intercepting events to determine tool tip handling
        _viewLarge.MouseController = new ToolTipController(_ribbon.TabsArea!.ButtonSpecManager!.ToolTipManager!, 
            _viewLarge, _viewLarge.MouseController);
    }

    private void CreateMediumSmallCheckBoxView()
    {
        // Create the layout docker for the contents of the button
        _viewMediumSmall = new ViewLayoutRibbonCheckBox();

        // Create the image and drop-down content
        _viewMediumSmallImage = new ViewDrawRibbonGroupCheckBoxImage(_ribbon, GroupCheckBox, false);
        _viewMediumSmallText1 = new ViewDrawRibbonGroupCheckBoxText(_ribbon, GroupCheckBox, true);
        _viewMediumSmallText2 = new ViewDrawRibbonGroupCheckBoxText(_ribbon, GroupCheckBox, false);
        var imagePadding = new ViewLayoutRibbonCenterPadding(_smallImagePadding)
        {
            _viewMediumSmallImage
        };

        // Layout the content in the center of a row
        _viewMediumSmallCenter = new ViewLayoutRibbonRowCenter
        {
            imagePadding,
            _viewMediumSmallText1,
            _viewMediumSmallText2
        };

        // Use content as only fill item
        _viewMediumSmall.Add(_viewMediumSmallCenter, ViewDockStyle.Fill);

        // Create controller for handling mouse, keyboard and focus
        _viewMediumSmallController = new GroupCheckBoxController(_ribbon, _viewMediumSmall, _viewMediumSmallImage, _needPaint!);
        _viewMediumSmallController.Click += OnMediumSmallCheckBoxClick;
        _viewMediumSmallController.ContextClick += OnContextClick;
        _viewMediumSmall.MouseController = _viewMediumSmallController;
        _viewMediumSmall.SourceController = _viewMediumSmallController;
        _viewMediumSmall.KeyController = _viewMediumSmallController;

        // Create controller for intercepting events to determine tool tip handling
        _viewMediumSmall.MouseController = new ToolTipController(_ribbon.TabsArea!.ButtonSpecManager!.ToolTipManager!,
            _viewMediumSmall, _viewMediumSmall.MouseController);
    }

    private void DefineRootView([DisallowNull] ViewBase view)
    {
        // Remove any existing view
        Clear();

        // Use the provided view
        Add(view);

        // Provide back reference to the check box definition
        GroupCheckBox.CheckBoxView = view;
    }

    private void UpdateEnabledState()
    {
        // Get the correct enabled state from the button definition
        var buttonEnabled = GroupCheckBox.Enabled;
        if (GroupCheckBox.KryptonCommand != null)
        {
            buttonEnabled = GroupCheckBox.KryptonCommand.Enabled;
        }

        // Take into account the ribbon state and mode
        var enabled = _ribbon.InDesignHelperMode || (buttonEnabled && _ribbon.Enabled);

        // Update enabled for the large check box view
        _viewLarge.Enabled = enabled;
        _viewLargeImage.Enabled = enabled;
        _viewLargeText1.Enabled = enabled;
        _viewLargeText2.Enabled = enabled;

        // Update enabled for the medium/small check box view
        _viewMediumSmall.Enabled = enabled;
        _viewMediumSmallText1.Enabled = enabled;
        _viewMediumSmallText2.Enabled = enabled;
        _viewMediumSmallImage.Enabled = enabled;
    }

    private void UpdateCheckState()
    {
        CheckState newCheckState = GroupCheckBox.KryptonCommand?.CheckState ?? GroupCheckBox.CheckState;

        _viewLargeImage.CheckState = newCheckState;
        _viewMediumSmallImage.CheckState = newCheckState;
    }

    private void UpdateItemSizeState() => UpdateItemSizeState(GroupCheckBox.ItemSizeCurrent);

    private void UpdateItemSizeState(GroupItemSize size)
    {
        _currentSize = size;

        switch (size)
        {
            case GroupItemSize.Small:
            case GroupItemSize.Medium:
                DefineRootView(_viewMediumSmall);
                break;
            case GroupItemSize.Large:
                DefineRootView(_viewLarge);
                break;
        }
    }

    private void OnLargeCheckBoxClick(object? sender, EventArgs e) => GroupCheckBox.PerformClick(_finishDelegateLarge);

    private void OnMediumSmallCheckBoxClick(object? sender, EventArgs e) => GroupCheckBox.PerformClick(_finishDelegateMediumSmall);

    private void OnContextClick(object? sender, MouseEventArgs e) => GroupCheckBox.OnDesignTimeContextMenu(e);

    private void ActionFinishedLarge(object? sender, EventArgs e)
    {
        // Remove any popups that result from an action occurring
        _ribbon.ActionOccurred();

        // Remove the fixed pressed appearance
        _viewLargeController?.RemoveFixed();
    }

    private void ActionFinishedMediumSmall(object? sender, EventArgs e)
    {
        // Remove any popups that result from an action occurring
        _ribbon.ActionOccurred();

        // Remove the fixed pressed appearance
        _viewMediumSmallController?.RemoveFixed();
    }

    private void OnCheckBoxPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        var updateLayout = false;
        var updatePaint = false;

        switch (e.PropertyName)
        {
            case nameof(Visible):
                updateLayout = true;
                break;
            case "TextLine1":
                updateLayout = true;
                _viewLargeText1.MakeDirty();
                _viewMediumSmallText1.MakeDirty();
                break;
            case "TextLine2":
                updateLayout = true;
                _viewLargeText2.MakeDirty();
                _viewMediumSmallText2.MakeDirty();
                break;
            case "Checked":
            case nameof(CheckState):
                UpdateCheckState();
                updatePaint = true;
                break;
            case nameof(Enabled):
                UpdateEnabledState();
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
                UpdateCheckState();
                updateLayout = true;
                break;
        }

        if (updateLayout)
        {
            // If we are on the currently selected tab then...
            if ((GroupCheckBox.RibbonTab != null) &&
                (_ribbon.SelectedTab == GroupCheckBox.RibbonTab))
            {
                // ...layout so the visible change is made
                OnNeedPaint(true);
            }
        }

        if (updatePaint)
        {
            // If this check box is actually defined as visible...
            if (GroupCheckBox.Visible || _ribbon.InDesignMode)
            {
                // ...and on the currently selected tab then...
                if ((GroupCheckBox.RibbonTab != null) &&
                    (_ribbon.SelectedTab == GroupCheckBox.RibbonTab))
                {
                    // ...repaint it right now
                    OnNeedPaint(false, ClientRectangle);
                }
            }
        }
    }
    #endregion

    #region IContentValues
    /// <summary>
    /// Gets the image.
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public Image? GetImage(PaletteState state) => null;

    /// <summary>
    /// Gets the image transparent color.
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public Color GetImageTransparentColor(PaletteState state) => Color.Empty;

    /// <summary>
    /// Gets the short text.
    /// </summary>
    /// <returns></returns>
    public string GetShortText() => GroupCheckBox.TextLine1;

    /// <summary>
    /// Gets the long text.
    /// </summary>
    /// <returns></returns>
    public string GetLongText() => GroupCheckBox.TextLine2;

    #endregion
}