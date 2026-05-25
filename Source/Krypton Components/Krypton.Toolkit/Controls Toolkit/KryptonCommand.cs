#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Defines state and events for a single command.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonCommand), "ToolboxBitmaps.KryptonCommand.bmp")]
[DefaultEvent("Click")]
[DefaultProperty(nameof(Text))]
[DesignerCategory(@"code")]
[Designer(typeof(KryptonCommandDesigner))]
[Description(@"Defines state and events for a single command.")]
public class KryptonCommand : Component, IKryptonCommand, INotifyPropertyChanged
{
    #region Instance Fields

    private bool _enabled;
    private bool _checked;
    private ButtonSpec? _assignedButtonSpec;
    private CheckState _checkState;
    private string _text;
    private string _extraText;
    private string _textLine1;
    private string _textLine2;
    private Image? _imageSmall;
    private Image? _imageLarge;
    private Color _imageTransparentColor;
    private KryptonCommandType _commandType;
    private bool _paletteHandlerRegistered;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the command needs executing.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the command needs executing.")]
    public event EventHandler? Execute;

    /// <summary>
    /// Occurs when a property has changed value.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of property has changed.")]
    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCommand class.
    /// </summary>
    public KryptonCommand()
    {
        _enabled = true;
        _checked = false;
        _checkState = CheckState.Unchecked;
        _text = string.Empty;
        _extraText = string.Empty;
        _textLine1 = string.Empty;
        _textLine2 = string.Empty;
        _imageSmall = null;
        _imageLarge = null;
        _imageTransparentColor = GlobalStaticVariables.EMPTY_COLOR;
        _commandType = KryptonCommandType.General;
        _assignedButtonSpec = null;

        RegisterPaletteHandler();
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            UnregisterPaletteHandler();
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the enabled state of the command.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Indicates whether the command is enabled.")]
    [DefaultValue(true)]
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
    /// Gets and sets the checked state of the command.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Indicates whether the command is in the checked state.")]
    [DefaultValue(false)]
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
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Checked)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CheckState)));
            }
        }
    }

    /// <summary>
    /// Gets or sets the <see cref="ButtonSpec"/> that this command configures when <see cref="CommandType"/> is not <see cref="KryptonCommandType.General"/>.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Button specification updated when CommandType is set to a built-in button spec style.")]
    [DefaultValue(null)]
    public ButtonSpec? AssignedButtonSpec
    {
        get => _assignedButtonSpec;

        set
        {
            if (_assignedButtonSpec != value)
            {
                _assignedButtonSpec = value;

                if (_commandType != KryptonCommandType.General)
                {
                    ApplyCommandTypeConfiguration();
                }
            }
        }
    }

    /// <summary>
    /// Gets and sets the check state of the command.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Indicates the checked state of the command.")]
    //[DefaultValue(CheckState.Unchecked)]
    [DefaultValue(typeof(CheckState), "Unchecked")]

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
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Checked)));
                }

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CheckState)));
            }
        }
    }

    /// <summary>
    /// Gets and sets the command text.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Command text.")]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
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

    private void ResetText() => Text = string.Empty;

    private bool ShouldSerializeText() => !string.IsNullOrEmpty(Text);

    /// <summary>
    /// Gets and sets the command extra text.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Command extra text.")]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
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

    private void ResetExtraText() => ExtraText = string.Empty;

    private bool ShouldSerializeExtraText() => !string.IsNullOrEmpty(ExtraText);

    /// <summary>
    /// Gets and sets the command text line 1 for use in KryptonRibbon.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Command text line 1 for use in KryptonRibbon.")]
    public string TextLine1
    {
        get => _textLine1;

        set
        {
            if (_textLine1 != value)
            {
                _textLine1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(TextLine1)));
            }
        }
    }

    private void ResetTextLine1() => TextLine1 = string.Empty;

    private bool ShouldSerializeTextLine1() => !string.IsNullOrEmpty(TextLine1);

    /// <summary>
    /// Gets and sets the command text line 2 for use in KryptonRibbon.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Command text line 2 for use in KryptonRibbon.")]
    public string TextLine2
    {
        get => _textLine2;

        set
        {
            if (_textLine2 != value)
            {
                _textLine2 = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(TextLine2)));
            }
        }
    }

    private void ResetTextLine2() => TextLine2 = string.Empty;

    private bool ShouldSerializeTextLine2() => !string.IsNullOrEmpty(TextLine2);

    /// <summary>
    /// Gets and sets the command small image.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Command small image.")]
    public Image? ImageSmall
    {
        get => _imageSmall;

        set
        {
            if (_imageSmall != value)
            {
                _imageSmall = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ImageSmall)));
            }
        }
    }

    private void ResetImageSmall() => ImageSmall = null;

    private bool ShouldSerializeImageSmall() => ImageSmall != null;

    /// <summary>
    /// Gets and sets the command large image.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Command large image.")]
    public Image? ImageLarge
    {
        get => _imageLarge;

        set
        {
            if (_imageLarge != value)
            {
                _imageLarge = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ImageLarge)));
            }
        }
    }

    private void ResetImageLarge() => ImageLarge = null;

    private bool ShouldSerializeImageLarge() => ImageLarge != null;

    /// <summary>
    /// Gets and sets the command image transparent color.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Command image transparent color.")]
    [KryptonDefaultColor]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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

    /// <summary>
    /// Gets and sets user-defined data associated with the object.
    /// </summary>
    [Category(@"Data")]
    [Description(@"User-defined data associated with the object.")]
    [TypeConverter(typeof(StringConverter))]
    [DefaultValue(null)]
    public object? Tag { get; set; }

    /// <summary>Gets or sets the type of the krypton command.</summary>
    /// <value>The type of the krypton command.</value>
    [Category(@"Data")]
    [Description(@"Changes the KryptonCommand, depending on its intended use.")]
    [DefaultValue(KryptonCommandType.General)]
    public KryptonCommandType CommandType
    {
        get => _commandType;

        set
        {
            if (_commandType != value)
            {
                _commandType = value;
                UpdateCommandType(value);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CommandType)));
            }
        }
    }

    /// <summary>
    /// Generates a Execute event for a button.
    /// </summary>
    public void PerformExecute() => OnExecute(EventArgs.Empty);

    // Allow specifying the originating sender so shared commands can identify the source control
    public void PerformExecute(object? sender)
        => Execute?.Invoke(sender ?? this, EventArgs.Empty);

    #endregion

    #region Protected
    /// <summary>
    /// Raises the Execute event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnExecute(EventArgs e) => Execute?.Invoke(this, e);

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="e">A PropertyChangedEventArgs containing the event data.</param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);

    #endregion

    #region Extended Implementation

    /// <summary>
    /// Gets whether this command maps to a built-in <see cref="PaletteButtonSpecStyle"/>.
    /// </summary>
    internal bool TryGetButtonSpecStyle(out PaletteButtonSpecStyle style) =>
        KryptonCommandButtonSpecMappings.TryGetButtonSpecStyle(_commandType, out style);

    /// <summary>
    /// Gets the palette image for this command's button spec style and visual state.
    /// </summary>
    internal Image? GetButtonSpecImage(PaletteBase? palette, PaletteState state)
    {
        if (!TryGetButtonSpecStyle(out var style))
        {
            return null;
        }

        palette ??= KryptonManager.CurrentGlobalPalette;
        return palette?.GetButtonSpecImage(style, state);
    }

    /// <summary>
    /// Gets the transparent color for this command's button spec style.
    /// </summary>
    internal Color GetButtonSpecImageTransparentColor(PaletteBase? palette)
    {
        if (!TryGetButtonSpecStyle(out var style))
        {
            return ImageTransparentColor;
        }

        palette ??= KryptonManager.CurrentGlobalPalette;
        return palette?.GetButtonSpecImageTransparentColor(style) ?? GlobalStaticVariables.EMPTY_COLOR;
    }

    private void RegisterPaletteHandler()
    {
        if (_paletteHandlerRegistered)
        {
            return;
        }

        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
        _paletteHandlerRegistered = true;
    }

    private void UnregisterPaletteHandler()
    {
        if (!_paletteHandlerRegistered)
        {
            return;
        }

        KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
        _paletteHandlerRegistered = false;
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        if (_commandType != KryptonCommandType.General)
        {
            ApplyCommandTypeConfiguration();
        }
    }

    private void UpdateCommandType(KryptonCommandType commandType)
    {
        if (commandType == KryptonCommandType.General)
        {
            return;
        }

        ApplyCommandTypeConfiguration();
    }

    private void ApplyCommandTypeConfiguration()
    {
        if (!TryGetButtonSpecStyle(out var style))
        {
            return;
        }

        var palette = KryptonManager.CurrentGlobalPalette;
        var defaultText = KryptonCommandButtonSpecMappings.GetDefaultText(_commandType);

        if (!string.IsNullOrEmpty(defaultText))
        {
            Text = defaultText;
        }

        var normalImage = palette?.GetButtonSpecImage(style, PaletteState.Normal);
        if (normalImage != null)
        {
            ImageSmall = normalImage;
        }

        ImageTransparentColor = palette?.GetButtonSpecImageTransparentColor(style) ?? GlobalStaticVariables.EMPTY_COLOR;

        SyncAssignedButtonSpec(style, palette);
    }

    private void SyncAssignedButtonSpec(PaletteButtonSpecStyle style, PaletteBase? palette)
    {
        if (_assignedButtonSpec is not ButtonSpecAny buttonSpecAny)
        {
            return;
        }

        if (buttonSpecAny.Type != style)
        {
            buttonSpecAny.Type = style;
        }

        if (buttonSpecAny.KryptonCommand != this)
        {
            buttonSpecAny.KryptonCommand = this;
        }

        var imageStates = buttonSpecAny.ImageStates;
        imageStates.ImageNormal = palette?.GetButtonSpecImage(style, PaletteState.Normal);
        imageStates.ImageDisabled = palette?.GetButtonSpecImage(style, PaletteState.Disabled);
        imageStates.ImagePressed = palette?.GetButtonSpecImage(style, PaletteState.Pressed) ?? imageStates.ImageNormal;
        imageStates.ImageTracking = palette?.GetButtonSpecImage(style, PaletteState.Tracking) ?? imageStates.ImageNormal;
    }

    #endregion
}

/// <summary>
/// Manages a collection of KryptonCommand instances.
/// </summary>
public class KryptonCommandCollection : TypedCollection<KryptonCommand>
{
    #region Public
    /// <summary>
    /// Gets the item with the provided name.
    /// </summary>
    /// <param name="name">Name to find.</param>
    /// <returns>Item with matching name.</returns>
    public override KryptonCommand? this[string name]
    {
        get
        {
            if (!string.IsNullOrEmpty(name))
            {
                foreach (KryptonCommand item in this)
                {
                    var text = item.Text;
                    if (!string.IsNullOrEmpty(text) && (text == name))
                    {
                        return item;
                    }

                    text = item.ExtraText;
                    if (!string.IsNullOrEmpty(text) && (text == name))
                    {
                        return item;
                    }
                }
            }

            return null;
        }
    }
    #endregion
}
