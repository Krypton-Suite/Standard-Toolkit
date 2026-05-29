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

internal class ViewDrawMenuLinkLabel : ViewComposite
{
    #region Instance Fields
    private readonly IContextMenuProvider _provider;
    private readonly FixedContentValue _contentValues;
    private readonly ViewDrawContent _drawContent;
    private readonly ViewLayoutDocker _outerDocker;
    private readonly ViewLayoutDocker _innerDocker;
    private KryptonCommand? _cachedCommand;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawMenuLinkLabel class.
    /// </summary>
    /// <param name="provider">Reference to provider.</param>
    /// <param name="linkLabel">Reference to owning link label entry.</param>
    public ViewDrawMenuLinkLabel(IContextMenuProvider provider,
        KryptonContextMenuLinkLabel linkLabel)
    {
        _provider = provider;
        KryptonContextMenuLinkLabel = linkLabel;

        // Create fixed storage of the content values
        _contentValues = new FixedContentValue(linkLabel.Text,
            linkLabel.ExtraText,
            linkLabel.Image,
            linkLabel.ImageTransparentColor);

        // Decide on the enabled state of the display
        ItemEnabled = provider.ProviderEnabled;

        // Give the heading object the redirector to use when inheriting values
        linkLabel.SetPaletteRedirect(provider.ProviderRedirector);

        // Create the content for the actual heading text/image
        _drawContent = new ViewDrawContent(linkLabel.OverrideFocusNotVisited, _contentValues, VisualOrientation.Top)
        {
            UseMnemonic = true,
            Enabled = ItemEnabled
        };

        // Place label link in the center of the area but inside some separator to add spacing
        _innerDocker = new ViewLayoutDocker
        {
            { _drawContent, ViewDockStyle.Fill },
            { new ViewLayoutSeparator(1), ViewDockStyle.Right },
            { new ViewLayoutSeparator(1), ViewDockStyle.Left },
            { new ViewLayoutSeparator(1), ViewDockStyle.Top },
            { new ViewLayoutSeparator(1), ViewDockStyle.Bottom }
        };

        // Use outer docker so that any extra space not needed is used by the null
        _outerDocker = new ViewLayoutDocker
        {
            { _innerDocker, ViewDockStyle.Top },
            { new ViewLayoutNull(), ViewDockStyle.Fill }
        };

        // Use context menu specific version of the link label controller
        var mllc = new MenuLinkLabelController(provider.ProviderViewManager, _drawContent, this,
            provider.ProviderNeedPaintDelegate);
        mllc.Click += OnClick;
        //_drawContent.MouseController = mllc;
        _drawContent.KeyController = mllc;
        // Create the manager for handling tooltips
        _drawContent.MouseController = new ToolTipController(KryptonContextMenuLinkLabel.ToolTipManager!, this, mllc);

        // Add docker as the composite content
        Add(_outerDocker);

        // Want to know when a property changes whilst displayed
        KryptonContextMenuLinkLabel.PropertyChanged += OnPropertyChanged;

        // We need to know if a property of the command changes
        if (KryptonContextMenuLinkLabel.KryptonCommand != null)
        {
            _cachedCommand = KryptonContextMenuLinkLabel.KryptonCommand;
            KryptonContextMenuLinkLabel.KryptonCommand.PropertyChanged += OnCommandPropertyChanged;
        }
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawMenuLinkLabel:{Id}";

    #endregion

    #region ItemText
    /// <summary>
    /// Gets the short text value of the menu item.
    /// </summary>
    public string ItemText => _contentValues.GetShortText();

    #endregion

    #region ItemEnabled
    /// <summary>
    /// Gets the enabled state of the item.
    /// </summary>
    public bool ItemEnabled { get; private set; }

    #endregion

    #region ResolveImage
    /// <summary>
    /// Resolves the correct image to use from the menu item.
    /// </summary>
    public Image? ResolveImage => _cachedCommand != null ? _cachedCommand.ImageSmall : KryptonContextMenuLinkLabel.Image;

    #endregion

    #region ResolveImageTransparentColor
    /// <summary>
    /// Resolves the correct image transparent color to use from the menu item.
    /// </summary>
    public Color ResolveImageTransparentColor => _cachedCommand?.ImageTransparentColor ?? KryptonContextMenuLinkLabel.ImageTransparentColor;

    #endregion

    #region ResolveText
    /// <summary>
    /// Resolves the correct text string to use from the menu item.
    /// </summary>
    public string ResolveText => _cachedCommand != null ? _cachedCommand.Text : KryptonContextMenuLinkLabel.Text;

    #endregion

    #region ResolveExtraText
    /// <summary>
    /// Resolves the correct extra text string to use from the menu item.
    /// </summary>
    public string ResolveExtraText =>
        (_cachedCommand != null ? _cachedCommand.ExtraText : KryptonContextMenuLinkLabel.ExtraText)!;

    #endregion

    #region Focused
    /// <summary>
    /// Sets if the link label is currently focused.
    /// </summary>
    public bool Focused
    {
        set 
        {
            KryptonContextMenuLinkLabel.OverrideFocusNotVisited.Apply = value;
            KryptonContextMenuLinkLabel.OverridePressedFocus.Apply = value;
        }
    }
    #endregion

    #region Pressed
    /// <summary>
    /// Gets and sets if the link label is currently pressed.
    /// </summary>
    public bool Pressed
    {
        set => _drawContent.SetPalette(value ? KryptonContextMenuLinkLabel.OverridePressedFocus : 
            KryptonContextMenuLinkLabel.OverrideFocusNotVisited);
    }
    #endregion

    #region KryptonContextMenuLinkLabel
    /// <summary>
    /// Gets access to the actual link label definiton.
    /// </summary>
    public KryptonContextMenuLinkLabel KryptonContextMenuLinkLabel { get; }

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
        ItemEnabled = _provider.ProviderEnabled;

        // Update with enabled state
        _drawContent.Enabled = ItemEnabled;

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
                _cachedCommand = KryptonContextMenuLinkLabel.KryptonCommand;
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
                // Update to show new state
                _provider.ProviderNeedPaintDelegate(this, new NeedLayoutEventArgs(true));
                break;
        }
    }

    private void OnClick(object? sender, EventArgs e) => KryptonContextMenuLinkLabel.PerformClick();

    #endregion
}