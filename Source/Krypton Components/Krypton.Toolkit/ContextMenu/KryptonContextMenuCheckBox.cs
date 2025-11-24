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
/// Provide a context menu check box.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonContextMenuCheckBox), "ToolboxBitmaps.KryptonCheckBox.bmp")]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultProperty(nameof(Text))]
[DefaultEvent(nameof(CheckedChanged))]
public class KryptonContextMenuCheckBox : KryptonContextMenuItemBase
{
    #region Instance Fields
    private bool _threeState;
    private bool _autoCheck;
    private bool _autoClose;
    private bool _checked;
    private bool _enabled;
    private string? _extraText;
    private Image? _image;
    private Color _imageTransparentColor;
    private CheckState _checkState;
    private readonly PaletteContentInheritRedirect _stateCommonRedirect;
    private KryptonCommand? _command;
    private LabelStyle _style;
    private string _text;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the check box item is clicked.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the check box item is clicked.")]
    public event EventHandler? Click;

    /// <summary>
    /// Occurs when the value of the Checked property has changed.
    /// </summary>
    [Category(@"Misc")]
    [Description(@"Occurs whenever the Checked property has changed.")]
    public event EventHandler? CheckedChanged;

    /// <summary>
    /// Occurs when the value of the CheckState property has changed.
    /// </summary>
    [Category(@"Misc")]
    [Description(@"Occurs whenever the CheckState property has changed.")]
    public event EventHandler? CheckStateChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuCheckBox class.
    /// </summary>
    public KryptonContextMenuCheckBox()
        : this(nameof(CheckBox))
    {
    }

    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuCheckBox class.
    /// </summary>
    /// <param name="initialText">Initial text for display.</param>
    public KryptonContextMenuCheckBox(string initialText)
    {
        // Default fields
        _enabled = true;
        _autoClose = false;
        _text = initialText;
        _extraText = string.Empty;
        _image = null;
        _imageTransparentColor = GlobalStaticValues.EMPTY_COLOR;
        _checkState = CheckState.Unchecked;
        _checked = false;
        _threeState = false;
        _autoCheck = true;
        _style = LabelStyle.NormalPanel;
        Images = new CheckBoxImages();

        // Create the redirectors
        _stateCommonRedirect = new PaletteContentInheritRedirect(PaletteContentStyle.LabelNormalPanel);
        StateCheckBoxImages = new PaletteRedirectCheckBox(Images);

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

        return new ViewDrawMenuCheckBox(provider, this);
    }

    /// <summary>
    /// Gets and sets if clicking the check box automatically closes the context menu.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Indicates if clicking the check box automatically closes the context menu.")]
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
    /// Gets and sets the check box text.
    /// </summary>
    [KryptonPersist]
    [Category(@"Appearance")]
    [Description(@"Main check box text.")]
    [DefaultValue(nameof(CheckBox))]
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
    /// Gets and sets the check box extra text.
    /// </summary>
    [KryptonPersist]
    [Category(@"Appearance")]
    [Description(@"Check box extra text.")]
    [Localizable(true)]
    [AllowNull]
    public string ExtraText
    {
        get => _extraText ?? string.Empty;

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
    /// Gets and sets the check box image.
    /// </summary>
    [KryptonPersist]
    [Category(@"Appearance")]
    [Description(@"Check box image.")]
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
    /// Gets and sets the check box image color to make transparent.
    /// </summary>
    [KryptonPersist]
    [Category(@"Appearance")]
    [Description(@"Check box image color to make transparent.")]
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
    /// Gets and sets the check box label style.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Check box label style.")]
    [DefaultValue(LabelStyle.NormalPanel)]
    public LabelStyle LabelStyle
    {
        get => _style;

        set
        {
            if (_style != value)
            {
                _style = value;
                SetCheckBoxStyle(_style);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(LabelStyle)));
            }
        }
    }
    private bool ShouldSerializeLabelStyle() => _style != LabelStyle.NormalPanel;

    private void ResetLabelStyle() => _style = LabelStyle.NormalPanel;

    /// <summary>
    /// Gets access to the image value overrides.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Image value overrides.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public CheckBoxImages Images { get; }

    private bool ShouldSerializeImages() => !Images.IsDefault;

    /// <summary>
    /// Gets and sets if the check box is enabled.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Indicates whether the check box is enabled.")]
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
                // Store new values
                _checked = value;
                _checkState = _checked ? CheckState.Checked : CheckState.Unchecked;

                // Generate events
                OnCheckedChanged(EventArgs.Empty);
                OnCheckStateChanged(EventArgs.Empty);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Checked)));
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating the checked state of the component.
    /// </summary>
    [KryptonPersist]
    [Category(@"Appearance")]
    [Description(@"Indicates the checked state of the component.")]
    [DefaultValue(CheckState.Unchecked)]
    [Bindable(true)]
    public CheckState CheckState
    {
        get => _checkState;

        set
        {
            if (_checkState != value)
            {
                // Store new values
                _checkState = value;
                var newChecked = _checkState != CheckState.Unchecked;
                var checkedChanged = _checked != newChecked;
                _checked = newChecked;

                // Generate events
                if (checkedChanged)
                {
                    OnCheckedChanged(EventArgs.Empty);
                }

                OnCheckStateChanged(EventArgs.Empty);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CheckState)));
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating if the check box is automatically changed state when clicked. 
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Causes the check box to automatically change state when clicked.")]
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
    /// Gets or sets a value indicating if the component allows three states instead of two.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Indicates if the component allows three states instead of two.")]
    [DefaultValue(false)]
    public bool ThreeState
    {
        get => _threeState;

        set
        {
            if (_threeState != value)
            {
                _threeState = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ThreeState)));
            }
        }
    }

    /// <summary>
    /// Gets access to the common check box appearance that other states can override.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common check box appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled check box appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled check box appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal check box appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal check box appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the check box appearance when it has focus.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining check box appearance when it has focus.")]
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

    /// <summary>
    /// Raises the CheckStateChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnCheckStateChanged(EventArgs e) => CheckStateChanged?.Invoke(this, e);

    #endregion

    #region Internal
    internal PaletteContentInheritOverride OverrideNormal { get; }

    internal PaletteContentInheritOverride OverrideDisabled { get; }

    internal PaletteRedirectCheckBox? StateCheckBoxImages { get; }

    internal void SetPaletteRedirect(PaletteRedirect redirector)
    {
        _stateCommonRedirect.SetRedirector(redirector);
        StateCheckBoxImages!.Target = redirector;
    }
    #endregion

    #region Private
    private void SetCheckBoxStyle(LabelStyle style) => _stateCommonRedirect.Style = CommonHelper.ContentStyleFromLabelStyle(style);
    #endregion
}