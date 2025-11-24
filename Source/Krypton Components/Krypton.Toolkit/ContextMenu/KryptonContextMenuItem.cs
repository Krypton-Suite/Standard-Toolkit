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

/// <summary>
/// Provide a standard menu item.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonContextMenuItem), "ToolboxBitmaps.KryptonContextMenuItem.bmp")]
[Designer(typeof(KryptonContextMenuItemDesigner))]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultProperty(nameof(Text))]
[DefaultEvent(nameof(Click))]
public class KryptonContextMenuItem : KryptonContextMenuItemBase
{
    #region Nested Classes
    // Provides proper design-time reference conversion for the KryptonCommand property even when the item is not sited.
    private sealed class KryptonCommandReferenceConverter : ReferenceConverter
    {
        public KryptonCommandReferenceConverter() : base(typeof(KryptonCommand)) { }
    }
    #endregion

    #region Instance Fields
    private bool _enabled;
    private bool _splitSubMenu;
    private bool _checkOnClick;
    private bool _showShortcutKeys;
    private bool _autoClose;
    private bool _largeKryptonCommandImage;
    private string _text;
    private string _extraText;
    private string _shortcutKeyDisplayString;
    private Image? _image;
    private Color _imageTransparentColor;
    private CheckState _checkState;
    private Keys _shortcutKeys;
    private readonly PaletteContextMenuItemStateRedirect _stateRedirect;
    private KryptonCommand? _command;
    private object? _commandParameter;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the menu item is clicked.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the menu item is clicked.")]
    public event EventHandler? Click;

    /// <summary>
    /// Occurs when the menu item is clicked.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the checked property changes.")]
    public event EventHandler? CheckedChanged;

    /// <summary>
    /// Occurs when the menu item is clicked.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the check state property changes.")]
    public event EventHandler? CheckStateChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuItem class.
    /// </summary>
    public KryptonContextMenuItem()
        : this(@"MenuItem", null, null, Keys.None)
    {
    }

    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuItem class.
    /// </summary>
    /// <param name="initialText">Initial text string.</param>
    public KryptonContextMenuItem(string initialText)
        : this(initialText, null, null, Keys.None)
    {
    }

    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuItem class.
    /// </summary>
    /// <param name="initialText">Initial text string.</param>
    /// <param name="clickHandler">Click handler.</param>
    public KryptonContextMenuItem(string initialText,
        EventHandler? clickHandler)
        : this(initialText, null, clickHandler, Keys.None)
    {
    }

    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuItem class.
    /// </summary>
    /// <param name="initialText">Initial text string.</param>
    /// <param name="clickHandler">Click handler.</param>
    /// <param name="shortcut">Shortcut key combination.</param>
    public KryptonContextMenuItem(string initialText,
        EventHandler? clickHandler,
        Keys shortcut)
        : this(initialText, null, clickHandler, shortcut)
    {
    }

    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuItem class.
    /// </summary>
    /// <param name="initialText">Initial text string.</param>
    /// <param name="initialImage">Initial image.</param>
    /// <param name="clickHandler">Click handler.</param>
    public KryptonContextMenuItem(string initialText,
        Image? initialImage,
        EventHandler? clickHandler)
        : this(initialText, initialImage, clickHandler, Keys.None)
    {
    }

    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuItem class.
    /// </summary>
    /// <param name="initialText">Initial text string.</param>
    /// <param name="initialImage">Initial image.</param>
    /// <param name="clickHandler">Click handler.</param>
    /// <param name="shortcut">Shortcut key combination.</param>
    public KryptonContextMenuItem(string initialText,
        Image? initialImage,
        EventHandler? clickHandler,
        Keys shortcut)
    {
        // Initial values
        _text = initialText;
        _image = initialImage;

        // Initial click handler
        if (clickHandler != null)
        {
            Click += clickHandler;
        }

        // Default fields
        _enabled = true;
        _autoClose = true;
        _splitSubMenu = false;
        _checkOnClick = false;
        _showShortcutKeys = true;
        _largeKryptonCommandImage = false;
        _extraText = string.Empty;
        _imageTransparentColor = GlobalStaticValues.EMPTY_COLOR;
        _shortcutKeys = shortcut;
        _shortcutKeyDisplayString = string.Empty;
        _checkState = CheckState.Unchecked;
        Items = [];

        // Create the common storage for palette override values
        _stateRedirect = new PaletteContextMenuItemStateRedirect();
        StateNormal = new PaletteContextMenuItemState(_stateRedirect);
        StateDisabled = new PaletteContextMenuItemState(_stateRedirect);
        StateHighlight = new PaletteContextMenuItemStateHighlight(_stateRedirect);
        StateChecked = new PaletteContextMenuItemStateChecked(_stateRedirect);
    }

    /// <summary>
    /// Returns a description of the instance.
    /// </summary>
    /// <returns>String representation.</returns>
    public override string ToString() => Text;

    #endregion

    #region Public
    /// <summary>
    /// Returns the number of child menu items.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override int ItemChildCount => 0;

    /// <summary>
    /// Returns the indexed child menu item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override KryptonContextMenuItemBase? this[int index] => null;

    /// <summary>
    /// Test for the provided shortcut and perform relevant action if a match is found.
    /// </summary>
    /// <param name="keyData">Key data to check against shortcut definitions.</param>
    /// <returns>True if shortcut was handled, otherwise false.</returns>
    public override bool ProcessShortcut(Keys keyData)
    {
        if (_shortcutKeys == keyData)
        {
            PerformClick();
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Returns a view appropriate for this item based on the object it is inside.
    /// </summary>
    /// <param name="provider">Provider of context menu information.</param>
    /// <param name="parent">Owning object reference.</param>
    /// <param name="columns">Containing columns.</param>
    /// <param name="standardStyle">Draw items with standard or alternate style.</param>
    /// <param name="imageColumn">Draw an image background for the item images.</param>
    /// <returns>ViewBase that is the root of the view hierarchy being added.</returns>
    public override ViewBase GenerateView(IContextMenuProvider provider,
        object parent,
        ViewLayoutStack columns,
        bool standardStyle,
        bool imageColumn) =>
        new ViewDrawMenuItem(provider, this, columns, standardStyle, imageColumn);

    /// <summary>
    /// Gets and sets the standard menu item text.
    /// </summary>
    [KryptonPersist]
    [Category(@"Appearance")]
    [Description(@"Standard menu item text.")]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [DefaultValue(@"MenuItem")]
    [Localizable(true)]
    [Bindable(true)]
    public string Text
    {
        get => _text;

        set
        {
            if (_text != value)
            {
                _text = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Text)));
            }
        }
    }

    /// <summary>
    /// Gets and sets the standard menu item extra text.
    /// </summary>
    [KryptonPersist]
    [Category(@"Appearance")]
    [Description(@"Standard menu item extra text.")]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [Localizable(true)]
    [Bindable(true)]
    public string ExtraText
    {
        get => _extraText;

        set
        {
            if (_extraText != value)
            {
                _extraText = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ExtraText)));
            }
        }
    }
    private bool ShouldSerializeExtraText() => !string.IsNullOrEmpty(_extraText);

    /// <summary>
    /// Gets and sets the standard menu item image.
    /// </summary>
    [KryptonPersist]
    [Category(@"Appearance")]
    [Description(@"Standard menu item image.")]
    [DefaultValue(null)]
    [Localizable(true)]
    [Bindable(true)]
    public Image? Image
    {
        get => _image;

        set
        {
            if (_image != value)
            {
                _image = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Image)));
            }
        }
    }

    /// <summary>
    /// Gets and sets the heading image color to make transparent.
    /// </summary>
    [KryptonPersist]
    [Category(@"Appearance")]
    [Description(@"Heading image color to make transparent.")]
    [Localizable(true)]
    [Bindable(true)]
    public Color ImageTransparentColor
    {
        get => _imageTransparentColor;

        set
        {
            if (_imageTransparentColor != value)
            {
                _imageTransparentColor = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ImageTransparentColor)));
            }
        }
    }

    private bool ShouldSerializeImageTransparentColor() => !_imageTransparentColor.Equals(GlobalStaticValues.EMPTY_COLOR);
    private void ResetImageTransparentColor() => _imageTransparentColor = GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets and sets the shortcut key combination associated with the menu item.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"The shortcut key combination associated with the menu item.")]
    [DefaultValue(Keys.None)]
    [Localizable(true)]
    public Keys ShortcutKeys
    {
        get => _shortcutKeys;

        set
        {
            if (_shortcutKeys != value)
            {
                _shortcutKeys = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ShortcutKeys)));
            }
        }
    }

    /// <summary>
    /// Gets and sets if clicking the menu item automatically closes the context menu.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Indicates if clicking the menu item automatically closes the context menu.")]
    [DefaultValue(true)]
    public bool AutoClose
    {
        get => _autoClose;

        set
        {
            if (_autoClose != value)
            {
                _autoClose = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(AutoClose)));
            }
        }
    }

    /// <summary>
    /// Gets and sets whether the menu item toggles checked state when clicked.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Indicates whether the menu item toggles checked state when clicked.")]
    [DefaultValue(false)]
    public bool SplitSubMenu
    {
        get => _splitSubMenu;

        set
        {
            if (_splitSubMenu != value)
            {
                _splitSubMenu = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SplitSubMenu)));
            }
        }
    }

    /// <summary>
    /// Gets and sets if the shortcut display text is shown.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Determines if the shortcut display text is shown.")]
    [DefaultValue(false)]
    public bool CheckOnClick
    {
        get => _checkOnClick;

        set
        {
            if (_checkOnClick != value)
            {
                _checkOnClick = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CheckOnClick)));
            }
        }
    }

    /// <summary>
    /// Gets and sets if the shortcut display text is shown.
    /// </summary>
    [KryptonPersist]
    [Category(@"Appearance")]
    [Description(@"Determines if the shortcut display text is shown.")]
    [DefaultValue(true)]
    [Localizable(true)]
    public bool ShowShortcutKeys
    {
        get => _showShortcutKeys;

        set
        {
            if (_showShortcutKeys != value)
            {
                _showShortcutKeys = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ShowShortcutKeys)));
            }
        }
    }

    /// <summary>
    /// Gets and sets if the large image is used from the attached KryptonCommand.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Determines if the large image is used from the attached KryptonCommand.")]
    [DefaultValue(false)]
    public bool LargeKryptonCommandImage
    {
        get => _largeKryptonCommandImage;

        set
        {
            if (_largeKryptonCommandImage != value)
            {
                _largeKryptonCommandImage = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(LargeKryptonCommandImage)));
            }
        }
    }

    /// <summary>
    /// Gets and sets the display text to use in preference to the shortcut key setting.
    /// </summary>
    [KryptonPersist]
    [Category(@"Appearance")]
    [Description(@"Display text to use in preference to the shortcut key setting.")]
    [DefaultValue(@"")]
    [Localizable(true)]
    public string ShortcutKeyDisplayString
    {
        get => _shortcutKeyDisplayString;

        set
        {
            if (_shortcutKeyDisplayString != value)
            {
                _shortcutKeyDisplayString = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ShortcutKeyDisplayString)));
            }
        }
    }

    /// <summary>
    /// Gets and sets if the menu item is in the checked state.
    /// </summary>
    [KryptonPersist]
    [Category(@"Appearance")]
    [Description(@"Indicates if the menu item is in the checked state.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(false)]
    [Bindable(true)]
    public bool Checked
    {
        get => CheckState != CheckState.Unchecked;

        set
        {
            // Are we currently checked?
            var areChecked = CheckState != CheckState.Unchecked;

            // Only interested in a change of value
            if (areChecked != value)
            {
                // Work out if the check state has changed, and update to new value
                CheckState newCheckState = value ? CheckState.Checked : CheckState.Unchecked;
                var checkStateChanged = newCheckState != _checkState;
                _checkState = newCheckState;

                // Checked value has always changed
                OnCheckedChanged(EventArgs.Empty);

                // CheckState might have changed
                if (checkStateChanged)
                {
                    OnCheckStateChanged(EventArgs.Empty);
                }

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Checked)));
            }
        }
    }

    /// <summary>
    /// Gets and sets the checked state of the menu item.
    /// </summary>
    [KryptonPersist]
    [Category(@"Appearance")]
    [Description(@"Indicates the checked state of the menu item.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(CheckState.Unchecked)]
    [Bindable(true)]
    public CheckState CheckState
    {
        get => _checkState;

        set
        {
            if (_checkState != value)
            {
                var oldChecked = Checked;
                _checkState = value;

                // Checked might have changed
                if (Checked != oldChecked)
                {
                    OnCheckedChanged(EventArgs.Empty);
                }

                // CheckState value has always changed
                OnCheckStateChanged(EventArgs.Empty);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CheckState)));
            }
        }
    }

    /// <summary>
    /// Collection of sub-menu items for display.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Collection of sub-menu items.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(typeof(KryptonContextMenuCollectionEditor), typeof(UITypeEditor))]
    public KryptonContextMenuCollection Items { get; }

    /// <summary>
    /// Gets and sets if the menu item is enabled.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Indicates whether the menu item is enabled.")]
    [DefaultValue(true)]
    [Bindable(true)]
    public bool Enabled
    {
        get => _enabled;

        set
        {
            if (_enabled != value)
            {
                _enabled = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Enabled)));
            }
        }
    }

    /// <summary>
    /// Gets access to the menu item disabled appearance values.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining menu item disabled appearance values.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContextMenuItemState StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the menu item normal appearance values.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining menu item normal appearance values.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContextMenuItemState StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the menu item normal appearance values.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining menu item checked appearance values.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContextMenuItemStateChecked StateChecked { get; }

    private bool ShouldSerializeStateChecked() => !StateChecked.IsDefault;

    /// <summary>
    /// Gets access to the menu item highlight appearance values.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining menu item highlight appearance values.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContextMenuItemStateHighlight StateHighlight { get; }

    private bool ShouldSerializeStateHighlight() => !StateHighlight.IsDefault;

    /// <summary>
    /// Gets and sets the associated KryptonCommand.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Command associated with the menu item.")]
    [DefaultValue(null)]
    [TypeConverter(typeof(KryptonCommandReferenceConverter))]
    public virtual KryptonCommand? KryptonCommand
    {
        get => _command;

        set
        {
            if (_command != value)
            {
                _command = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(KryptonCommand)));
            }
        }
    }

    /// <summary>
    /// Gets and sets an optional parameter value that can be used inside a shared KryptonCommand Execute handler.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Value passed to the KryptonCommand Execute handler to discriminate between menu items.")]
    [DefaultValue(null)]
    [Browsable(true)]
    [TypeConverter(typeof(StringConverter))]
    public object? CommandParameter
    {
        get => _commandParameter;

        set
        {
            if (!Equals(_commandParameter, value))
            {
                _commandParameter = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CommandParameter)));
            }
        }
    }

    private bool ShouldSerializeCommandParameter() => CommandParameter != null;
    private void ResetCommandParameter() => CommandParameter = null;

    /// <summary>
    /// Generates a Click event for the component.
    /// </summary>
    public void PerformClick()
    {
        // Do we toggle the checked state when clicked>
        if (CheckOnClick)
        {
            // Grab current state from command or ourself
            CheckState state = KryptonCommand?.CheckState ?? CheckState;

            // Find new state
            switch (state)
            {
                case CheckState.Unchecked:
                    state = CheckState.Checked;
                    break;
                case CheckState.Indeterminate:
                case CheckState.Checked:
                    state = CheckState.Unchecked;
                    break;
            }

            // Update correct property
            if (KryptonCommand != null)
            {
                KryptonCommand.CheckState = state;
            }
            else
            {
                CheckState = state;
            }
        }

        OnClick(EventArgs.Empty);

        // If we have an attached command then execute it, indicating this item as the sender
        KryptonCommand?.PerformExecute(this);
    }
    #endregion

    #region Protected
    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnClick(EventArgs e) => Click?.Invoke(this, e);

    /// <summary>
    /// Raises the CheckedChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnCheckedChanged(EventArgs e) => CheckedChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the CheckStateChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnCheckStateChanged(EventArgs e) => CheckStateChanged?.Invoke(this, e);
    #endregion

    #region Internal
    internal void SetPaletteRedirect(IContextMenuProvider provider) => _stateRedirect.SetRedirector(provider);

    #endregion
}