#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Base class for all <see cref="KryptonRadialMenu"/> item types.
/// </summary>
[ToolboxItem(false)]
[DesignTimeVisible(false)]
[DesignerCategory(@"code")]
public abstract class KryptonRadialMenuItemBase : Component, INotifyPropertyChanged
{
    #region Instance Fields

    private bool _visible;
    private bool _enabled;
    private object? _tag;
    private Color _backColor;
    private Color _borderColor;
    private Image? _image;
    private Color _imageTransparentColor;
    private readonly ToolTipValues _toolTipValues;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when a property value changes.")]
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Occurs when a tooltip is about to be shown for this item.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when a tooltip is about to be shown for this item.")]
    public event EventHandler<ToolTipNeededEventArgs>? ToolTipNeeded;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRadialMenuItemBase"/> class.
    /// </summary>
    protected KryptonRadialMenuItemBase()
    {
        _visible = true;
        _enabled = true;
        _backColor = Color.Empty;
        _borderColor = Color.Empty;
        _imageTransparentColor = Color.Empty;
        _toolTipValues = new ToolTipValues(null, static () => 1f);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets whether the item is visible in the radial menu.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the item is visible.")]
    [DefaultValue(true)]
    public bool Visible
    {
        get => _visible;
        set
        {
            if (_visible != value)
            {
                _visible = value;
                OnPropertyChanged(nameof(Visible));
            }
        }
    }

    /// <summary>
    /// Gets or sets whether the item can be interacted with.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the item is enabled.")]
    [DefaultValue(true)]
    public bool Enabled
    {
        get => _enabled;
        set
        {
            if (_enabled != value)
            {
                _enabled = value;
                OnPropertyChanged(nameof(Enabled));
            }
        }
    }

    /// <summary>
    /// Gets or sets user data associated with the item.
    /// </summary>
    [Category(@"Data")]
    [Description(@"User-defined data associated with the item.")]
    [DefaultValue(null)]
    [TypeConverter(typeof(StringConverter))]
    [Bindable(true)]
    public object? Tag
    {
        get => _tag;
        set
        {
            if (!ReferenceEquals(_tag, value))
            {
                _tag = value;
                OnPropertyChanged(nameof(Tag));
            }
        }
    }

    /// <summary>
    /// Gets or sets an optional fill colour for the item sector. Empty uses the menu palette.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Optional sector fill colour. Empty uses the menu palette.")]
    public Color BackColor
    {
        get => _backColor;
        set
        {
            if (_backColor != value)
            {
                _backColor = value;
                OnPropertyChanged(nameof(BackColor));
            }
        }
    }

    private bool ShouldSerializeBackColor() => !_backColor.IsEmpty;
    private void ResetBackColor() => BackColor = Color.Empty;

    /// <summary>
    /// Gets or sets an optional border colour for the item sector. Empty uses the menu palette.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Optional sector border colour. Empty uses the menu palette.")]
    public Color BorderColor
    {
        get => _borderColor;
        set
        {
            if (_borderColor != value)
            {
                _borderColor = value;
                OnPropertyChanged(nameof(BorderColor));
            }
        }
    }

    private bool ShouldSerializeBorderColor() => !_borderColor.IsEmpty;
    private void ResetBorderColor() => BorderColor = Color.Empty;

    /// <summary>
    /// Gets or sets the image displayed on the item sector.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Image displayed on the item sector.")]
    [DefaultValue(null)]
    [Localizable(true)]
    public virtual Image? Image
    {
        get => _image;
        set
        {
            if (!ReferenceEquals(_image, value))
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }
    }

    /// <summary>
    /// Gets or sets the colour treated as transparent when drawing <see cref="Image"/>.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Colour treated as transparent when drawing the sector image.")]
    [KryptonDefaultColor]
    public Color ImageTransparentColor
    {
        get => _imageTransparentColor;
        set
        {
            if (_imageTransparentColor != value)
            {
                _imageTransparentColor = value;
                OnPropertyChanged(nameof(ImageTransparentColor));
            }
        }
    }

    private bool ShouldSerializeImageTransparentColor() => !_imageTransparentColor.IsEmpty;
    private void ResetImageTransparentColor() => ImageTransparentColor = Color.Empty;

    /// <summary>
    /// Gets access to the Krypton tooltip content for this item.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Krypton tooltip shown when hovering the item.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public ToolTipValues ToolTipValues => _toolTipValues;

    private bool ShouldSerializeToolTipValues() => !_toolTipValues.IsDefault;

    /// <summary>
    /// Resets <see cref="ToolTipValues"/> to defaults.
    /// </summary>
    public void ResetToolTipValues() => _toolTipValues.Reset();

    /// <summary>
    /// Gets or sets a simple tooltip description. Setting a non-empty value enables tooltips.
    /// Prefer <see cref="ToolTipValues"/> for heading, image, and style.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Simple tooltip description. Prefer ToolTipValues for full Krypton tooltip content.")]
    [DefaultValue("")]
    [Localizable(true)]
    public string? ToolTipText
    {
        get => _toolTipValues.Description == @"Description" ? string.Empty : _toolTipValues.Description;
        set
        {
            value ??= string.Empty;
            if (ToolTipText == value)
            {
                return;
            }

            if (string.IsNullOrEmpty(value))
            {
                _toolTipValues.Description = @"Description";
                _toolTipValues.EnableToolTips = false;
            }
            else
            {
                _toolTipValues.Heading = string.Empty;
                _toolTipValues.Description = value;
                _toolTipValues.EnableToolTips = true;
            }

            OnPropertyChanged(nameof(ToolTipText));
        }
    }

    /// <summary>
    /// Gets whether this item opens a nested editor or child ring when activated.
    /// </summary>
    [Browsable(false)]
    public abstract bool HasChildren { get; }

    /// <summary>
    /// Raises <see cref="ToolTipNeeded"/> so callers can customise tooltip content.
    /// </summary>
    /// <param name="e">Event arguments.</param>
    protected internal virtual void OnToolTipNeeded(ToolTipNeededEventArgs e) => ToolTipNeeded?.Invoke(this, e);

    #endregion

    #region Protected

    /// <summary>
    /// Raises the <see cref="PropertyChanged"/> event.
    /// </summary>
    /// <param name="propertyName">Name of the property that changed.</param>
    protected virtual void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion
}
