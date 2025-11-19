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

/// <summary>
/// Provide a context menu radio button.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonContextMenuRadioButton), "ToolboxBitmaps.KryptonRadioButton.bmp")]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultProperty(nameof(Text))]
[DefaultEvent(nameof(CheckedChanged))]
public class KryptonContextMenuRadioButton : KryptonContextMenuItemBase
{
    #region Instance Fields
    private bool _autoCheck;
    private bool _autoClose;
    private bool _checked;
    private bool _enabled;
    private string? _extraText;
    private Image? _image;
    private Color _imageTransparentColor;
    private readonly PaletteContentInheritRedirect _stateCommonRedirect;
    private KryptonCommand? _command;
    private LabelStyle _style;
    private string _text;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the radio button is clicked.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the radio button is clicked.")]
    public event EventHandler? Click;

    /// <summary>
    /// Occurs when the value of the Checked property has changed.
    /// </summary>
    [Category(@"Misc")]
    [Description(@"Occurs whenever the Checked property has changed.")]
    public event EventHandler? CheckedChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuRadioButton class.
    /// </summary>
    public KryptonContextMenuRadioButton()
        : this(nameof(RadioButton))
    {
    }

    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuRadioButton class.
    /// </summary>
    /// <param name="initialText">Initial text for display.</param>
    public KryptonContextMenuRadioButton(string initialText)
    {
        // Default fields
        _enabled = true;
        _autoClose = false;
        _text = initialText;
        _extraText = string.Empty;
        _image = null;
        _imageTransparentColor = GlobalStaticValues.EMPTY_COLOR;
        _checked = false;
        _autoCheck = true;
        _style = LabelStyle.NormalPanel;
        Images = new RadioButtonImages();

        // Create the redirectors
        _stateCommonRedirect = new PaletteContentInheritRedirect(PaletteContentStyle.LabelNormalPanel);
        StateRadioButtonImages = new PaletteRedirectRadioButton(Images);

        // Create the states
        StateCommon = new PaletteContent(_stateCommonRedirect);
        StateDisabled = new PaletteContent(StateCommon);
        StateNormal = new PaletteContent(StateCommon);
        OverrideFocus = new PaletteContent(_stateCommonRedirect);

        // Override the normal/disabled values with the focus, when the control has focus
        OverrideNormal = new PaletteContentInheritOverride(OverrideFocus, StateNormal, PaletteState.FocusOverride, false);
        OverrideDisabled = new PaletteContentInheritOverride(OverrideFocus, StateDisabled, PaletteState.FocusOverride, false);
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
    public override bool ProcessShortcut(Keys keyData) => false;

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
        bool imageColumn)
    {
        SetProvider(provider);
        return new ViewDrawMenuRadioButton(provider, this);
    }

    /// <summary>
    /// Gets and sets if clicking the radio button automatically closes the context menu.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Indicates if clicking the cradio button automatically closes the context menu.")]
    [DefaultValue(false)]
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
    /// Gets and sets the radio button text.
    /// </summary>
    [KryptonPersist]
    [Category(@"Appearance")]
    [Description(@"Main radio button text.")]
    [DefaultValue(nameof(RadioButton))]
    [Localizable(true)]
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
    /// Gets and sets the radio button extra text.
    /// </summary>
    [KryptonPersist]
    [Category(@"Appearance")]
    [Description(@"Radio button extra text.")]
    [Localizable(true)]
    public string? ExtraText
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
    /// Gets and sets the radio button image.
    /// </summary>
    [KryptonPersist]
    [Category(@"Appearance")]
    [Description(@"Radio button image.")]
    [DefaultValue(null)]
    [Localizable(true)]
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
    /// Gets and sets the radio button image color to make transparent.
    /// </summary>
    [KryptonPersist]
    [Category(@"Appearance")]
    [Description(@"Radio button image color to make transparent.")]
    [Localizable(true)]
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

    /// <summary>
    /// Gets and sets the radio button label style.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Radio button label style.")]
    [DefaultValue(LabelStyle.NormalPanel)]
    public LabelStyle LabelStyle
    {
        get => _style;

        set
        {
            if (_style != value)
            {
                _style = value;
                SetRadioButtonStyle(_style);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(LabelStyle)));
            }
        }
    }

    /// <summary>
    /// Gets access to the image value overrides.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Image value overrides.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadioButtonImages Images { get; }

    private bool ShouldSerializeImages() => !Images.IsDefault;

    /// <summary>
    /// Gets and sets if the radio button is enabled.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Indicates whether the radio button is enabled.")]
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
    /// Gets or sets a value indicating if the component is in the checked state.
    /// </summary>
    [KryptonPersist]
    [Category(@"Appearance")]
    [Description(@"Indicates if the component is in the checked state.")]
    [DefaultValue(false)]
    [Bindable(true)]
    public bool Checked
    {
        get => _checked;

        set
        {
            if (_checked != value)
            {
                _checked = value;
                OnCheckedChanged(EventArgs.Empty);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Checked)));
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating if the radio button is automatically changed state when clicked. 
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Causes the radio button to automatically change state when clicked.")]
    [DefaultValue(true)]
    public bool AutoCheck
    {
        get => _autoCheck;

        set 
        {
            if (_autoCheck != value)
            {
                _autoCheck = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(AutoCheck)));
            }
        }
    }

    /// <summary>
    /// Gets access to the common radio button appearance that other states can override.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common radio button appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled radio button appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled radio button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal radio button appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal radio button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the radio button appearance when it has focus.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining radio button appearance when it has focus.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent OverrideFocus { get; }

    private bool ShouldSerializeOverrideFocus() => !OverrideFocus.IsDefault;

    /// <summary>
    /// Gets and sets the associated KryptonCommand.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Command associated with the menu check box.")]
    [DefaultValue(null)]
    [AllowNull]
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
    /// Generates a Click event for the component.
    /// </summary>
    public void PerformClick() => OnClick(EventArgs.Empty);

    #endregion

    #region Protected

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnClick(EventArgs e)
    {
        Click?.Invoke(this, e);

        // If we have an attached command then execute it
        KryptonCommand?.PerformExecute();
    }

    /// <summary>
    /// Raises the CheckedChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnCheckedChanged(EventArgs e) => CheckedChanged?.Invoke(this, e);

    #endregion

    #region Internal
    internal PaletteContentInheritOverride OverrideNormal { get; }

    internal PaletteContentInheritOverride OverrideDisabled { get; }

    internal PaletteRedirectRadioButton StateRadioButtonImages { get; }

    internal void SetPaletteRedirect(PaletteRedirect redirector)
    {
        _stateCommonRedirect.SetRedirector(redirector);
        StateRadioButtonImages.Target = redirector;
    }
    #endregion

    #region Private
    private void SetRadioButtonStyle(LabelStyle style) => _stateCommonRedirect.Style = CommonHelper.ContentStyleFromLabelStyle(style);
    #endregion
}