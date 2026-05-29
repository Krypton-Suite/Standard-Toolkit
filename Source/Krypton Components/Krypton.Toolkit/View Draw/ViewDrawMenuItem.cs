#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class ViewDrawMenuItem : ViewDrawCanvas
{
    #region Static Fields
    private static readonly Image _empty16x16;
    #endregion

    #region Instance Fields
    private readonly IContextMenuProvider _provider;
    private readonly ViewDrawMenuImageCanvas? _imageCanvas;
    private readonly ViewDrawContent _imageContent;
    private readonly ViewDrawMenuItemContent _textContent;
    private readonly FixedContentValue? _fixedImage;
    private VisualContextMenu? _contextMenu;
    private readonly ViewDrawMenuItemContent? _shortcutContent;
    private readonly ViewDrawMenuItemContent _subMenuContent;
    private readonly FixedContentValue _fixedTextExtraText;
    private KryptonCommand? _cachedCommand;
    private readonly bool _imageColumn;

    #endregion

    #region Identity
    static ViewDrawMenuItem() => _empty16x16 = GenericImageResources.Transparent_16_x_16;

    /// <summary>
    /// Initialize a new instance of the ViewDrawMenuItem class.
    /// </summary>
    /// <param name="provider">Provider of context menu information.</param>
    /// <param name="menuItem">Menu item definition.</param>
    /// <param name="columns">Containing columns.</param>
    /// <param name="standardStyle">Draw items with standard or alternate style.</param>
    /// <param name="imageColumn">Draw an image background for the item images.</param>
    public ViewDrawMenuItem(IContextMenuProvider provider,
        KryptonContextMenuItem menuItem,
        ViewLayoutStack columns,
        bool standardStyle,
        bool imageColumn)
        : base(menuItem.StateNormal.ItemHighlight.Back,
            menuItem.StateNormal.ItemHighlight.Border,
            menuItem.StateNormal.ItemHighlight,
            PaletteMetricPadding.ContextMenuItemHighlight,
            VisualOrientation.Top)
    {
        // Remember values
        _provider = provider;
        KryptonContextMenuItem = menuItem;
        _imageColumn = imageColumn;

        // Give the item object the redirector to use when inheriting values
        KryptonContextMenuItem.SetPaletteRedirect(provider);

        // Create a stack of horizontal items inside the item
        var docker = new ViewLayoutDocker();

        // Decide on the enabled state of the display
        ItemEnabled = provider.ProviderEnabled && ResolveEnabled;
        PaletteContextMenuItemState menuItemState = ItemEnabled ? KryptonContextMenuItem.StateNormal : KryptonContextMenuItem.StateDisabled;

        // Calculate the image to show inside in the image column
        Image? itemColumnImage = ResolveImage;
        Color itemImageTransparent = ResolveImageTransparentColor;

        // If no image found then...
        if (itemColumnImage != null)
        {
            // Ensure we have a fixed size if we are showing an image column
            if (_imageColumn)
            {
                itemColumnImage = _empty16x16;
                itemImageTransparent = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
            }

            switch (ResolveCheckState)
            {
                case CheckState.Checked:
                    itemColumnImage = provider.ProviderImages.GetContextMenuCheckedImage();
                    itemImageTransparent = GlobalStaticValues.EMPTY_COLOR;
                    break;
                case CheckState.Indeterminate:
                    itemColumnImage = provider.ProviderImages.GetContextMenuIndeterminateImage();
                    itemImageTransparent = GlobalStaticValues.EMPTY_COLOR;
                    break;
            }
        }

        // Column Image
        PaletteTripleJustImage justImage = ResolveChecked ? KryptonContextMenuItem.StateChecked.ItemImage : menuItemState.ItemImage;
        _fixedImage = new FixedContentValue(null, null, itemColumnImage, itemImageTransparent);
        _imageContent = new ViewDrawContent(justImage.Content, _fixedImage, VisualOrientation.Top);
        _imageCanvas = new ViewDrawMenuImageCanvas(justImage.Back, justImage.Border, 0, false)
        {
            _imageContent
        };
        docker.Add(new ViewLayoutCenter(_imageCanvas), ViewDockStyle.Left);
        _imageContent.Enabled = ItemEnabled;

        // Text/Extra Text
        PaletteContentJustText menuItemStyle = standardStyle ? menuItemState.ItemTextStandard : menuItemState.ItemTextAlternate;
        _fixedTextExtraText = new FixedContentValue(ResolveText, ResolveExtraText, null, GlobalStaticValues.EMPTY_COLOR);
        _textContent = new ViewDrawMenuItemContent(menuItemStyle, _fixedTextExtraText, 1);
        docker.Add(_textContent, ViewDockStyle.Fill);
        _textContent.Enabled = ItemEnabled;

        // Shortcut
        if (KryptonContextMenuItem.ShowShortcutKeys)
        {
            string shortcutString = KryptonContextMenuItem.ShortcutKeyDisplayString;
            if (string.IsNullOrEmpty(shortcutString))
            {
                shortcutString = (KryptonContextMenuItem.ShortcutKeys != Keys.None)
                    ? new KeysConverter().ConvertToString(KryptonContextMenuItem.ShortcutKeys) ?? string.Empty
                    : string.Empty;
            }

            if (shortcutString.Length > 0)
            {
                _shortcutContent = new ViewDrawMenuItemContent(menuItemState.ItemShortcutText, new FixedContentValue(shortcutString, null, null, GlobalStaticValues.EMPTY_COLOR), 2);
                docker.Add(_shortcutContent, ViewDockStyle.Right);
                _shortcutContent.Enabled = ItemEnabled;
            }
        }

        // Add split item separator
        SplitSeparator = new ViewDrawMenuSeparator(menuItemState.ItemSplit);
        docker.Add(SplitSeparator, ViewDockStyle.Right);
        SplitSeparator.Enabled = ItemEnabled;
        SplitSeparator.Draw = (KryptonContextMenuItem.Items.Count > 0) && KryptonContextMenuItem.SplitSubMenu;

        // SubMenu Indicator
        HasSubMenu = KryptonContextMenuItem.Items.Count > 0;
        _subMenuContent = new ViewDrawMenuItemContent(menuItemState.ItemImage.Content, new FixedContentValue(null, null,
                !HasSubMenu
                    ? _empty16x16
                    : provider.ProviderImages.GetContextMenuSubMenuImage(),
                KryptonContextMenuItem.Items.Count == 0
                    ? GlobalStaticValues.TRANSPARENCY_KEY_COLOR
                    : GlobalStaticValues.EMPTY_COLOR),
            3);
        docker.Add(new ViewLayoutCenter(_subMenuContent), ViewDockStyle.Right);
        _subMenuContent.Enabled = ItemEnabled;

        Add(docker);

        // Add a controller for handing mouse and keyboard events
        var mic = new MenuItemController(provider.ProviderViewManager, this, provider.ProviderNeedPaintDelegate);
        //MouseController = mic;
        KeyController = mic;

        // Want to know when a property changes whilst displayed
        KryptonContextMenuItem.PropertyChanged += OnPropertyChanged;

        // We need to know if a property of the command changes
        if (KryptonContextMenuItem.KryptonCommand != null)
        {
            _cachedCommand = KryptonContextMenuItem.KryptonCommand;
            KryptonContextMenuItem.KryptonCommand.PropertyChanged += OnCommandPropertyChanged;
        }

        // Create the manager for handling tooltips
        MouseController = new ToolTipController(KryptonContextMenuItem.ToolTipManager!, this, mic);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawMenuItem:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Unhook from events
            KryptonContextMenuItem.PropertyChanged -= OnPropertyChanged;

            if (_cachedCommand != null)
            {
                _cachedCommand.PropertyChanged -= OnCommandPropertyChanged;
                _cachedCommand = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region KryptonContextMenuItem
    /// <summary>
    /// Gets access to the context menu item we represent.
    /// </summary>
    public KryptonContextMenuItem KryptonContextMenuItem { get; }

    #endregion

    #region SplitSeparator
    /// <summary>
    /// Gets the view element used to draw the split separator.
    /// </summary>
    public ViewDrawMenuSeparator SplitSeparator { get; }

    #endregion

    #region ItemEnabled
    /// <summary>
    /// Gets the enabled state of the entire item and not for a particular view element.
    /// </summary>
    public bool ItemEnabled { get; private set; }

    #endregion

    #region ItemText
    /// <summary>
    /// Gets the short text value of the menu item.
    /// </summary>
    public string ItemText => _textContent.Values!.GetShortText();

    #endregion

    #region ItemExtraText
    /// <summary>
    /// Gets the long text value of the menu item.
    /// </summary>
    public string ItemExtraText => _textContent.Values!.GetLongText();

    #endregion

    #region ResolveEnabled
    /// <summary>
    /// Resolves the correct enabled state to use from the menu item.
    /// </summary>
    public bool ResolveEnabled => _cachedCommand?.Enabled ?? KryptonContextMenuItem.Enabled;

    #endregion

    #region ResolveImage
    /// <summary>
    /// Resolves the correct image to use from the menu item.
    /// </summary>
    public Image? ResolveImage
    {
        get
        {
            if (_cachedCommand != null)
            {
                return KryptonContextMenuItem.LargeKryptonCommandImage ? _cachedCommand.ImageLarge : _cachedCommand.ImageSmall;
            }
            else
            {
                return KryptonContextMenuItem.Image;
            }
        }
    }
    #endregion

    #region ResolveImageTransparentColor
    /// <summary>
    /// Resolves the correct image transparent color to use from the menu item.
    /// </summary>
    public Color ResolveImageTransparentColor => _cachedCommand?.ImageTransparentColor ?? KryptonContextMenuItem.ImageTransparentColor;

    #endregion

    #region ResolveText
    /// <summary>
    /// Resolves the correct text string to use from the menu item.
    /// </summary>
    public string ResolveText => _cachedCommand != null
        && !string.IsNullOrEmpty(_cachedCommand.Text)
            ? _cachedCommand.Text
            : KryptonContextMenuItem.Text;

    #endregion

    #region ResolveExtraText
    /// <summary>
    /// Resolves the correct extra text string to use from the menu item.
    /// </summary>
    public string ResolveExtraText => _cachedCommand != null
        && !string.IsNullOrEmpty(_cachedCommand.ExtraText)
            ? _cachedCommand.ExtraText
            : KryptonContextMenuItem.ExtraText;

    #endregion

    #region ResolveChecked
    /// <summary>
    /// Resolves the correct checked to use from the menu item.
    /// </summary>
    public bool ResolveChecked => _cachedCommand?.Checked ?? KryptonContextMenuItem.Checked;

    #endregion

    #region ResolveCheckState
    /// <summary>
    /// Resolves the correct check state to use from the menu item.
    /// </summary>
    public CheckState ResolveCheckState => _cachedCommand?.CheckState ?? KryptonContextMenuItem.CheckState;

    #endregion

    #region PointInSubMenu
    /// <summary>
    /// Indicates whether the mouse point should show a sub menu.
    /// </summary>
    /// <param name="pt"></param>
    /// <returns></returns>
    public bool PointInSubMenu(Point pt)
    {
        // Do we have sub menu items defined?
        if (HasSubMenu)
        {
            // If menu item is split into regular button and sub menu areas
            if (SplitSeparator.Draw)
            {
                // If mouse is inside or to the right of the slip indicator,
                // then a sub menu is required when the button is used
                return pt.X > SplitSeparator.ClientRectangle.X;
            }

            // Whole item is the sub menu area
            return true;
        }

        return false;
    }
    #endregion

    #region HasSubMenu
    /// <summary>
    /// Returns if the item shows a sub menu when selected.
    /// </summary>
    public bool HasSubMenu { get; }

    #endregion

    #region CanCloseMenu
    /// <summary>
    /// Gets a value indicating if the menu is capable of being closed.
    /// </summary>
    public bool CanCloseMenu => _provider.ProviderCanCloseMenu;

    #endregion

    #region Closing
    /// <summary>
    /// Raises the Closing event on the provider.
    /// </summary>
    /// <param name="cea">A CancelEventArgs containing the event data.</param>
    public void Closing(CancelEventArgs cea) => _provider.OnClosing(cea);

    #endregion

    #region Close
    /// <summary>
    /// Raises the Close event on the provider.
    /// </summary>
    /// <param name="e">A CancelEventArgs containing the event data.</param>
    public void Close(CloseReasonEventArgs e) => _provider.OnClose(e);

    #endregion

    #region DisposeContextMenu
    /// <summary>
    /// Request the showing context menu be disposed.
    /// </summary>
    public void DisposeContextMenu() => _provider.OnDispose(EventArgs.Empty);

    #endregion

    #region HasParentMenu
    /// <summary>
    /// Gets a value indicating if the menu item has a parent menu.
    /// </summary>
    public bool HasParentMenu => _provider.HasParentProvider;

    #endregion

    #region ShowSubMenu
    /// <summary>
    /// Ask the menu item to show the associated child collection as a menu.
    /// </summary>
    public void ShowSubMenu(bool keyboardActivated)
    {
        // Only need to show if not already doing so
        if ((_contextMenu == null) || _contextMenu.IsDisposed)
        {
            // No need for the sub menu timer anymore, we are showing
            _provider.ProviderViewManager.SetTargetSubMenu((KeyController as IContextMenuTarget)!);

            // Only show a sub menu if there is one to be shown!
            if (HasSubMenu)
            {
                // Create the actual control used to show the context menu
                _contextMenu = new VisualContextMenu(_provider, KryptonContextMenuItem.Items, keyboardActivated);

                // Need to know when the visual control is removed
                _contextMenu.Disposed += OnContextMenuDisposed;

                // Get the screen rectangle for the drawing element
                if (OwningControl != null)
                {
                    Rectangle menuDrawRect = OwningControl.RectangleToScreen(ClientRectangle);

                    // Should this menu item be shown at a fixed screen rectangle?
                    if (_provider.ProviderShowSubMenuFixed(KryptonContextMenuItem))
                    {
                        // Request the menu be shown at fixed screen rectangle
                        _contextMenu.ShowFixed(_provider.ProviderShowSubMenuFixedRect(KryptonContextMenuItem),
                            _provider.ProviderShowHorz,
                            _provider.ProviderShowVert);
                    }
                    else
                    {
                        // Request the menu be shown immediately
                        _contextMenu.Show(menuDrawRect,
                            _provider.ProviderShowHorz,
                            _provider.ProviderShowVert,
                            true, false);
                    }
                }
            }
        }
    }
    #endregion

    #region ClearSubMenu
    /// <summary>
    /// Remove any showing context menu.
    /// </summary>
    public void ClearSubMenu()
    {
        if (_contextMenu != null)
        {
            VisualPopupManager.Singleton.EndPopupTracking(_contextMenu);
        }
    }
    #endregion

    #region Layout

    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Always update to the latest correct check state
        if (_imageCanvas != null)
        {
            if (ResolveChecked)
            {
                _imageCanvas.ElementState = PaletteState.CheckedNormal;
                _imageCanvas.Enabled = ResolveEnabled;
            }
            else
            {
                _imageCanvas.ElementState = PaletteState.Normal;
                _imageCanvas.Enabled = true;
            }
        }

        PaletteDouble splitPalette;

        // Make sure we are using the correct palette for state
        switch (State)
        {
            default:
            case PaletteState.Normal:
                SetPalettes(KryptonContextMenuItem.StateNormal.ItemHighlight.Back,
                    KryptonContextMenuItem.StateNormal.ItemHighlight.Border,
                    KryptonContextMenuItem.StateNormal.ItemHighlight);
                splitPalette = KryptonContextMenuItem.StateNormal.ItemSplit;
                break;
            case PaletteState.Disabled:
                SetPalettes(KryptonContextMenuItem.StateDisabled.ItemHighlight.Back,
                    KryptonContextMenuItem.StateDisabled.ItemHighlight.Border,
                    KryptonContextMenuItem.StateDisabled.ItemHighlight);
                splitPalette = KryptonContextMenuItem.StateDisabled.ItemSplit;
                break;
            case PaletteState.Tracking:
                SetPalettes(KryptonContextMenuItem.StateHighlight.ItemHighlight.Back,
                    KryptonContextMenuItem.StateHighlight.ItemHighlight.Border,
                    KryptonContextMenuItem.StateHighlight.ItemHighlight);
                splitPalette = KryptonContextMenuItem.StateHighlight.ItemSplit;
                break;
        }


        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // If we have image display
        if (_fixedImage != null)
        {
            Image? itemColumnImage = ResolveImage;
            Color itemImageTransparent = ResolveImageTransparentColor;

            // If no image found then...
            if (itemColumnImage == null)
            {
                // Ensure we have a fixed size if we are showing an image column
                if (_imageColumn)
                {
                    itemColumnImage = _empty16x16;
                    itemImageTransparent = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
                }

                switch (ResolveCheckState)
                {
                    case CheckState.Checked:
                        itemColumnImage = _provider.ProviderImages.GetContextMenuCheckedImage();
                        itemImageTransparent = GlobalStaticValues.EMPTY_COLOR;
                        break;
                    case CheckState.Indeterminate:
                        itemColumnImage = _provider.ProviderImages.GetContextMenuIndeterminateImage();
                        itemImageTransparent = GlobalStaticValues.EMPTY_COLOR;
                        break;
                }
            }

            // Decide on the enabled state of the display
            ItemEnabled = _provider.ProviderEnabled && ResolveEnabled;
            PaletteContextMenuItemState menuItemState = ItemEnabled ? KryptonContextMenuItem.StateNormal : KryptonContextMenuItem.StateDisabled;

            // Update palettes based on Checked state
            PaletteTripleJustImage justImage = ResolveChecked ? KryptonContextMenuItem.StateChecked.ItemImage : menuItemState.ItemImage;
            _imageCanvas?.SetPalettes(justImage.Back, justImage.Border);

            // Update the Enabled state
            _imageContent.SetPalette(justImage.Content);
            _imageContent.Enabled = ItemEnabled;
            _textContent.Enabled = ItemEnabled;
            SplitSeparator.Enabled = ItemEnabled;
            _subMenuContent.Enabled = ItemEnabled;
            if (_shortcutContent != null)
            {
                _shortcutContent.Enabled = ItemEnabled;
            }

            // Update the Text/ExtraText
            _fixedTextExtraText.ShortText = ResolveText;
            _fixedTextExtraText.LongText = ResolveExtraText;

            // Update the Image
            if (itemColumnImage != null)
            {
                _fixedImage.Image = CommonHelper.ScaleImageForSizedDisplay(itemColumnImage,
                    itemColumnImage.Width * FactorDpiX,
                    itemColumnImage.Height * FactorDpiY, false);
            }
            else
            {
                _fixedImage.Image = null;
            }

            _fixedImage.ImageTransparentColor = itemImageTransparent;
        }

        SplitSeparator.SetPalettes(splitPalette.Back, splitPalette.Border);

        return base.GetPreferredSize(context);
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);
        ClientRectangle = context!.DisplayRectangle;
        base.Layout(context);
    }
    #endregion

    #region Implementation
    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case @"Text":
            case @"ExtraText":
            case nameof(Enabled):
            case @"Image":
            case @"ImageTransparentColor":
            case @"Checked":
            case nameof(CheckState):
            case @"ShortcutKeys":
            case @"ShowShortcutKeys":
            case @"LargeKryptonCommandImage":
                // Update to show new state
                _provider.ProviderNeedPaintDelegate(this, new NeedLayoutEventArgs(true));
                break;
            case nameof(KryptonCommand):
                // Unhook from any existing command
                if (_cachedCommand != null)
                {
                    _cachedCommand.PropertyChanged -= OnCommandPropertyChanged;
                }

                // Hook into the new command
                _cachedCommand = KryptonContextMenuItem.KryptonCommand;
                if (_cachedCommand != null)
                {
                    _cachedCommand.PropertyChanged += OnCommandPropertyChanged;
                }

                // Update to show new state
                _provider.ProviderNeedPaintDelegate(this, new NeedLayoutEventArgs(true));
                break;
        }
    }

    private void OnCommandPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case @"Text":
            case @"ExtraText":
            case @"ImageSmall":
            case @"ImageLarge":
            case @"ImageTransparentColor":
            case nameof(Enabled):
            case @"Checked":
            case nameof(CheckState):
                // Update to show new state
                _provider.ProviderNeedPaintDelegate(this, new NeedLayoutEventArgs(true));
                break;
        }
    }

    internal void OnContextMenuDisposed(object? sender, EventArgs e)
    {
        // Should still be caching a reference to actual display control
        if (_contextMenu != null)
        {
            // Unhook from control, so it can be garbage collected
            _contextMenu.Disposed -= OnContextMenuDisposed;
            KryptonContextMenuItem.OnCancelToolTip(sender, e);

            // No longer need to cache reference
            _contextMenu = null;

            // Tell our view manager that we no longer show a sub menu
            _provider.ProviderViewManager.ClearTargetSubMenu((KeyController as IContextMenuTarget)!);
        }
    }
    #endregion
}