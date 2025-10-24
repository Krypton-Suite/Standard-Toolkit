#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

internal class ViewDrawMenuCheckBox : ViewComposite
{
    #region Instance Fields
    private readonly IContextMenuProvider _provider;
    private readonly FixedContentValue _contentValues;
    private readonly ViewLayoutCenter _layoutCenter;
    private readonly ViewLayoutDocker _outerDocker;
    private readonly ViewLayoutDocker _innerDocker;
    private KryptonCommand? _cachedCommand;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawMenuCheckBox class.
    /// </summary>
    /// <param name="provider">Reference to provider.</param>
    /// <param name="checkBox">Reference to owning check box entry.</param>
    public ViewDrawMenuCheckBox(IContextMenuProvider provider,
        KryptonContextMenuCheckBox checkBox)
    {
        _provider = provider;
        KryptonContextMenuCheckBox = checkBox;

        // Create fixed storage of the content values
        _contentValues = new FixedContentValue(ResolveText,
            ResolveExtraText,
            ResolveImage,
            ResolveImageTransparentColor);

        // Decide on the enabled state of the display
        ItemEnabled = provider.ProviderEnabled && ResolveEnabled;

        // Give the heading object the redirector to use when inheriting values
        KryptonContextMenuCheckBox.SetPaletteRedirect(provider.ProviderRedirector);

        // Create the content for the actual heading text/image
        ViewDrawContent = new ViewDrawContent(ItemEnabled ? KryptonContextMenuCheckBox.OverrideNormal : KryptonContextMenuCheckBox.OverrideDisabled,
            _contentValues, VisualOrientation.Top)
        {
            UseMnemonic = true,
            Enabled = ItemEnabled
        };

        // Create the check box image drawer and place inside element so it is always centered
        ViewDrawCheckBox = new ViewDrawCheckBox(KryptonContextMenuCheckBox.StateCheckBoxImages!)
        {
            CheckState = ResolveCheckState,
            Enabled = ItemEnabled
        };
        _layoutCenter = new ViewLayoutCenter
        {
            ViewDrawCheckBox
        };

        // Place the check box on the left of the available space but inside separators
        _innerDocker = new ViewLayoutDocker
        {
            { ViewDrawContent, ViewDockStyle.Fill },
            { _layoutCenter, ViewDockStyle.Left },
            { new ViewLayoutSeparator(1), ViewDockStyle.Right },
            { new ViewLayoutSeparator(3), ViewDockStyle.Left },
            { new ViewLayoutSeparator(1), ViewDockStyle.Top },
            { new ViewLayoutSeparator(1), ViewDockStyle.Bottom }
        };

        // Use outer docker so that any extra space not needed is used by the null
        _outerDocker = new ViewLayoutDocker
        {
            { _innerDocker, ViewDockStyle.Top },
            { new ViewLayoutNull(), ViewDockStyle.Fill }
        };

        // Use context menu specific version of the check box controller
        var mcbc = new MenuCheckBoxController(provider.ProviderViewManager, _innerDocker, this,
            provider.ProviderNeedPaintDelegate);
        mcbc.Click += OnClick;
        _innerDocker.MouseController = mcbc;
        _innerDocker.KeyController = mcbc;
        // Create the manager for handling tooltips
        _innerDocker.MouseController = new ToolTipController(KryptonContextMenuCheckBox.ToolTipManager!, this, mcbc);

        // Add docker as the composite content
        Add(_outerDocker);

        // Want to know when a property changes whilst displayed
        KryptonContextMenuCheckBox.PropertyChanged += OnPropertyChanged;

        // We need to know if a property of the command changes
        if (KryptonContextMenuCheckBox.KryptonCommand != null)
        {
            _cachedCommand = KryptonContextMenuCheckBox.KryptonCommand;
            KryptonContextMenuCheckBox.KryptonCommand.PropertyChanged += OnCommandPropertyChanged;
        }
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawMenuCheckBox:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Unhook from events
            KryptonContextMenuCheckBox.PropertyChanged -= OnPropertyChanged;

            if (_cachedCommand != null)
            {
                _cachedCommand.PropertyChanged -= OnCommandPropertyChanged;
                _cachedCommand = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region ViewDrawCheckBox
    /// <summary>
    /// Gets access to the check box image drawing element.
    /// </summary>
    public ViewDrawCheckBox ViewDrawCheckBox { get; }

    #endregion

    #region ViewDrawContent
    /// <summary>
    /// Gets access to the content drawing element.
    /// </summary>
    public ViewDrawContent ViewDrawContent { get; }

    #endregion

    #region ItemEnabled
    /// <summary>
    /// Gets the enabled state of the item.
    /// </summary>
    public bool ItemEnabled { get; private set; }

    #endregion

    #region ItemText
    /// <summary>
    /// Gets the short text value of the check box item.
    /// </summary>
    public string ItemText => _contentValues.GetShortText();

    #endregion

    #region ResolveEnabled
    /// <summary>
    /// Resolves the correct enabled state to use from the menu item.
    /// </summary>
    public bool ResolveEnabled => _cachedCommand?.Enabled ?? KryptonContextMenuCheckBox.Enabled;

    #endregion

    #region ResolveImage
    /// <summary>
    /// Resolves the correct image to use from the menu item.
    /// </summary>
    public Image? ResolveImage => _cachedCommand != null ? _cachedCommand.ImageSmall : KryptonContextMenuCheckBox.Image;

    #endregion

    #region ResolveImageTransparentColor
    /// <summary>
    /// Resolves the correct image transparent color to use from the menu item.
    /// </summary>
    public Color ResolveImageTransparentColor => _cachedCommand?.ImageTransparentColor ?? KryptonContextMenuCheckBox.ImageTransparentColor;

    #endregion

    #region ResolveText
    /// <summary>
    /// Resolves the correct text string to use from the menu item.
    /// </summary>
    public string ResolveText => _cachedCommand != null ? _cachedCommand.Text : KryptonContextMenuCheckBox.Text;

    #endregion

    #region ResolveExtraText
    /// <summary>
    /// Resolves the correct extra text string to use from the menu item.
    /// </summary>
    public string ResolveExtraText => _cachedCommand != null ? _cachedCommand.ExtraText : KryptonContextMenuCheckBox.ExtraText;

    #endregion

    #region ResolveCheckState
    /// <summary>
    /// Resolves the correct check state to use from the menu item.
    /// </summary>
    public CheckState ResolveCheckState => _cachedCommand?.CheckState ?? KryptonContextMenuCheckBox.CheckState;

    #endregion

    #region KryptonContextMenuCheckBox
    /// <summary>
    /// Gets access to the actual check box definiton.
    /// </summary>
    public KryptonContextMenuCheckBox KryptonContextMenuCheckBox { get; }

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

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Update text and image values
        _contentValues.ShortText = ResolveText;
        _contentValues.LongText = ResolveExtraText;
        _contentValues.Image = ResolveImage;
        _contentValues.ImageTransparentColor = ResolveImageTransparentColor;

        // Find new enabled state
        ItemEnabled = _provider.ProviderEnabled && ResolveEnabled;

        // Update with enabled state
        ViewDrawContent.SetPalette(ItemEnabled ? KryptonContextMenuCheckBox.OverrideNormal : KryptonContextMenuCheckBox.OverrideDisabled);
        ViewDrawContent.Enabled = ItemEnabled;
        ViewDrawCheckBox.Enabled = ItemEnabled;

        // Update the checked state
        ViewDrawCheckBox.CheckState = ResolveCheckState;

        return base.GetPreferredSize(context!);
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // We take on all the available display area
        ClientRectangle = context.DisplayRectangle;

        // Let base class perform usual processing
        base.Layout(context);
    }
    #endregion

    #region Private
    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case @"Text":
            case @"ExtraText":
            case nameof(Image):
            case @"ImageTransparentColor":
            case nameof(Enabled):
            case @"Checked":
            case nameof(CheckState):
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
                _cachedCommand = KryptonContextMenuCheckBox.KryptonCommand;
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
            case @"ImageTransparentColor":
            case nameof(Enabled):
            case @"Checked":
            case nameof(CheckState):
                // Update to show new state
                _provider.ProviderNeedPaintDelegate(this, new NeedLayoutEventArgs(true));
                break;
        }
    }

    private void OnClick(object? sender, EventArgs e) => KryptonContextMenuCheckBox.PerformClick();

    #endregion
}