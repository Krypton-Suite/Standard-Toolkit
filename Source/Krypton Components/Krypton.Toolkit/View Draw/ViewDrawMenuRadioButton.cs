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

internal class ViewDrawMenuRadioButton: ViewComposite
{
    #region Instance Fields
    private readonly IContextMenuProvider _provider;
    private readonly FixedContentValue _contentValues;
    private readonly ViewLayoutCenter _layoutCenter;
    private readonly ViewLayoutDocker _outerDocker;
    private readonly ViewLayoutDocker _innerDocker;
    private KryptonCommand _cachedCommand;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawMenuRadioButton class.
    /// </summary>
    /// <param name="provider">Reference to provider.</param>
    /// <param name="radioButton">Reference to owning radio button entry.</param>
    public ViewDrawMenuRadioButton(IContextMenuProvider provider,
        KryptonContextMenuRadioButton radioButton)
    {
        _provider = provider;
        KryptonContextMenuRadioButton = radioButton;

        // Create fixed storage of the content values
        _contentValues = new FixedContentValue(radioButton.Text,
            radioButton.ExtraText,
            radioButton.Image,
            radioButton.ImageTransparentColor);

        // Decide on the enabled state of the display
        ItemEnabled = provider.ProviderEnabled && KryptonContextMenuRadioButton.Enabled;

        // Give the heading object the redirector to use when inheriting values
        KryptonContextMenuRadioButton.SetPaletteRedirect(provider.ProviderRedirector);

        // Create the content for the actual heading text/image
        ViewDrawContent = new ViewDrawContent(ItemEnabled ? KryptonContextMenuRadioButton.OverrideNormal : KryptonContextMenuRadioButton.OverrideDisabled,
            _contentValues, VisualOrientation.Top)
        {
            UseMnemonic = true,
            Enabled = ItemEnabled
        };

        // Create the radio button image drawer and place inside element so it is always centered
        ViewDrawRadioButton = new ViewDrawRadioButton(KryptonContextMenuRadioButton.StateRadioButtonImages)
        {
            CheckState = KryptonContextMenuRadioButton.Checked,
            Enabled = ItemEnabled
        };
        _layoutCenter = new ViewLayoutCenter
        {
            ViewDrawRadioButton
        };

        // Place the radio button on the left of the available space but inside separators
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

        // Use context menu specific version of the radio button controller
        var mrbc = new MenuRadioButtonController(provider.ProviderViewManager, _innerDocker,
            this, provider.ProviderNeedPaintDelegate);
        mrbc.Click += OnClick;
        //_innerDocker.MouseController = mrbc;
        _innerDocker.KeyController = mrbc;
        // Create the manager for handling tooltips
        _innerDocker.MouseController = new ToolTipController(KryptonContextMenuRadioButton.ToolTipManager!, this, mrbc);

        // We need to be notified whenever the checked state changes
        KryptonContextMenuRadioButton.CheckedChanged += OnCheckedChanged;

        if (KryptonContextMenuRadioButton.KryptonCommand != null)
        {
            _cachedCommand = KryptonContextMenuRadioButton.KryptonCommand;

            KryptonContextMenuRadioButton.KryptonCommand.PropertyChanged += OnCommandPropertyChanged;
        }

        // Add docker as the composite content
        Add(_outerDocker);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawMenuRadioButton:{Id}";

    /// <summary>
    /// Release unmanaged and optionally managed resources.
    /// </summary>
    /// <param name="disposing">Called from Dispose method.</param>
    protected override void Dispose(bool disposing)
    {
        // Unhook event handlers to prevent memory leak
        KryptonContextMenuRadioButton.CheckedChanged -= OnCheckedChanged;

        // Must call base class to finish disposing
        base.Dispose(disposing);
    }
    #endregion

    #region ItemEnabled
    /// <summary>
    /// Gets the enabled state of the item.
    /// </summary>
    public bool ItemEnabled { get; }

    #endregion

    #region ViewDrawRadioButton
    /// <summary>
    /// Gets access to the radio button image drawing element.
    /// </summary>
    public ViewDrawRadioButton ViewDrawRadioButton { get; }

    #endregion

    #region ViewDrawContent
    /// <summary>
    /// Gets access to the content drawing element.
    /// </summary>
    public ViewDrawContent ViewDrawContent { get; }

    #endregion

    #region ItemText
    /// <summary>
    /// Gets the short text value of the radio button item.
    /// </summary>
    public string ItemText => _contentValues.GetShortText();

    #endregion

    #region KryptonContextMenuRadioButton
    /// <summary>
    /// Gets access to the actual radio button definiton.
    /// </summary>
    public KryptonContextMenuRadioButton KryptonContextMenuRadioButton { get; }

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
    private void OnCheckedChanged(object? sender, EventArgs e)
    {
        ViewDrawRadioButton.CheckState = KryptonContextMenuRadioButton.Checked;
        _provider.ProviderNeedPaintDelegate(this, new NeedLayoutEventArgs(false));
    }

    private void OnClick(object? sender, EventArgs e) => KryptonContextMenuRadioButton.PerformClick();

    #endregion

    #region Implementation
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
    #endregion
}