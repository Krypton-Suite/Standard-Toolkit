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
/// Provide a context menu check button.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonContextMenuCheckButton), "ToolboxBitmaps.KryptonCheckButton.bmp")]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultProperty(nameof(Text))]
[DefaultEvent(nameof(CheckedChanged))]
public class KryptonContextMenuCheckButton : KryptonContextMenuItemBase
{
    #region Instance Fields
    private bool _autoCheck;
    private bool _autoClose;
    private bool _checked;
    private bool _enabled;
    private string _extraText;
    private Image? _image;
    private Color _imageTransparentColor;
    private ButtonStyle _style;
    private KryptonCommand? _command;
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
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuCheckButton class.
    /// </summary>
    public KryptonContextMenuCheckButton()
        : this(nameof(CheckBox))
    {
    }

    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuCheckButton class.
    /// </summary>
    /// <param name="initialText">Initial text for display.</param>
    public KryptonContextMenuCheckButton(string initialText)
    {
        // Default fields
        _enabled = true;
        _autoClose = false;
        _text = initialText;
        _extraText = string.Empty;
        _image = null;
        _imageTransparentColor = GlobalStaticValues.EMPTY_COLOR;
        _checked = false;
        _autoCheck = false;
        _style = ButtonStyle.Standalone;

        // Create the redirectors
        StateCommon = new PaletteTripleRedirect(PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone);
        OverrideFocus = new PaletteTripleRedirect(PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone);

        // Create the palette storage
        StateDisabled = new PaletteTriple(StateCommon);
        StateNormal = new PaletteTriple(StateCommon);
        StateTracking = new PaletteTriple(StateCommon);
        StatePressed = new PaletteTriple(StateCommon);
        StateCheckedNormal = new PaletteTriple(StateCommon);
        StateCheckedTracking = new PaletteTriple(StateCommon);
        StateCheckedPressed = new PaletteTriple(StateCommon);

        // Create the override handling classes
        OverrideDisabled = new PaletteTripleOverride(OverrideFocus, StateDisabled, PaletteState.FocusOverride);
        OverrideNormal = new PaletteTripleOverride(OverrideFocus, StateNormal, PaletteState.FocusOverride);
        OverrideTracking = new PaletteTripleOverride(OverrideFocus, StateTracking, PaletteState.FocusOverride);
        OverridePressed = new PaletteTripleOverride(OverrideFocus, StatePressed, PaletteState.FocusOverride);
        OverrideCheckedNormal = new PaletteTripleOverride(OverrideFocus, StateCheckedNormal, PaletteState.FocusOverride);
        OverrideCheckedTracking = new PaletteTripleOverride(OverrideFocus, StateCheckedTracking, PaletteState.FocusOverride);
        OverrideCheckedPressed = new PaletteTripleOverride(OverrideFocus, StateCheckedPressed, PaletteState.FocusOverride);
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
        return new ViewDrawMenuCheckButton(provider, this);
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
    [DefaultValue("")]
    [Localizable(true)]
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

    private bool ShouldSerializeImageTransparentColor() => (_imageTransparentColor == null) || !_imageTransparentColor.Equals(GlobalStaticValues.EMPTY_COLOR);

    /// <summary>
    /// Gets and sets the check button style.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Check button style.")]
    [DefaultValue(ButtonStyle.Standalone)]
    public ButtonStyle ButtonStyle
    {
        get => _style;

        set
        {
            if (_style != value)
            {
                _style = value;
                SetCheckButtonStyle(_style);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ButtonStyle)));
            }
        }
    }

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
                _checked = value;
                OnCheckedChanged(EventArgs.Empty);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Checked)));
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating if the check box is automatically changed state when clicked. 
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Causes the check box to automatically change state when clicked.")]
    [DefaultValue(false)]
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
    /// Gets access to the common button appearance that other states can override.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common button appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled button appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal button appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the pressed button appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StatePressed { get; }

    private bool ShouldSerializeStatePressed() => !StatePressed.IsDefault;

    /// <summary>
    /// Gets access to the tracking button appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tracking button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateTracking { get; }

    private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

    /// <summary>
    /// Gets access to the normal checked button appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal checked button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateCheckedNormal { get; }

    private bool ShouldSerializeStateCheckedNormal() => !StateCheckedNormal.IsDefault;

    /// <summary>
    /// Gets access to the hot tracking checked button appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining hot tracking checked button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateCheckedTracking { get; }

    private bool ShouldSerializeStateCheckedTracking() => !StateCheckedTracking.IsDefault;

    /// <summary>
    /// Gets access to the pressed checked button appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed checked button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateCheckedPressed { get; }

    private bool ShouldSerializeStateCheckedPressed() => !StateCheckedPressed.IsDefault;

    /// <summary>
    /// Gets access to the button appearance when it has focus.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining button appearance when it has focus.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect OverrideFocus { get; }

    private bool ShouldSerializeOverrideFocus() => !OverrideFocus.IsDefault;

    /// <summary>
    /// Gets and sets the associated KryptonCommand.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Command associated with the menu check button.")]
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

    #endregion

    #region Internal
    internal PaletteTripleOverride OverrideCheckedNormal { get; }

    internal PaletteTripleOverride OverrideCheckedTracking { get; }

    internal PaletteTripleOverride OverrideCheckedPressed { get; }

    internal PaletteTripleOverride OverrideDisabled { get; }

    internal PaletteTripleOverride OverrideNormal { get; }

    internal PaletteTripleOverride OverrideTracking { get; }

    internal PaletteTripleOverride OverridePressed { get; }

    internal void SetPaletteRedirect(PaletteRedirect redirector)
    {
        StateCommon.SetRedirector(redirector);
        OverrideFocus.SetRedirector(redirector);
    }
    #endregion

    #region Private
    private void SetCheckButtonStyle(ButtonStyle style)
    {
        StateCommon.SetStyles(style);
        OverrideFocus.SetStyles(style);
    }
    #endregion
}