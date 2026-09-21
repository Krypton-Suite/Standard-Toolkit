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
/// Base class for items shown on a <see cref="KryptonMiniToolbar"/>.
/// </summary>
[ToolboxItem(false)]
[DesignTimeVisible(false)]
[DesignerCategory(@"code")]
public abstract class KryptonMiniToolbarItemBase : Component, INotifyPropertyChanged
{
    #region Instance Fields

    private bool _visible;
    private bool _enabled;
    private object? _tag;
    private Image? _image;
    private string _text;
    private string _toolTipText;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when a property value changes.")]
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Occurs when the item is activated.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the Mini Toolbar item is activated.")]
    public event EventHandler? Click;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonMiniToolbarItemBase"/> class.
    /// </summary>
    protected KryptonMiniToolbarItemBase()
    {
        _visible = true;
        _enabled = true;
        _text = string.Empty;
        _toolTipText = string.Empty;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets whether the item is shown on the Mini Toolbar.
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
    /// Gets or sets whether the item can be activated.
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
    /// Gets or sets the image drawn on the item.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Image drawn on the Mini Toolbar item.")]
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
                OnPropertyChanged(nameof(Image));
            }
        }
    }

    /// <summary>
    /// Gets or sets optional text. Buttons typically show image only.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Optional text. Buttons typically show the image only.")]
    [DefaultValue("")]
    [Localizable(true)]
    public string Text
    {
        get => _text;
        set
        {
            value ??= string.Empty;
            if (_text != value)
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
    }

    /// <summary>
    /// Gets or sets tooltip text shown when hovering the item.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Tooltip text shown when hovering the item.")]
    [DefaultValue("")]
    [Localizable(true)]
    public string ToolTipText
    {
        get => _toolTipText;
        set
        {
            value ??= string.Empty;
            if (_toolTipText != value)
            {
                _toolTipText = value;
                OnPropertyChanged(nameof(ToolTipText));
            }
        }
    }

    /// <summary>
    /// Gets or sets user-defined data associated with the item.
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
            if (!Equals(_tag, value))
            {
                _tag = value;
                OnPropertyChanged(nameof(Tag));
            }
        }
    }

    /// <summary>
    /// Raises the <see cref="Click"/> event.
    /// </summary>
    public void PerformClick() => OnClick(EventArgs.Empty);

    #endregion

    #region Protected

    /// <summary>
    /// Raises the <see cref="PropertyChanged"/> event.
    /// </summary>
    /// <param name="propertyName">Name of the property that changed.</param>
    protected virtual void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    /// <summary>
    /// Raises the <see cref="Click"/> event.
    /// </summary>
    /// <param name="e">Event data.</param>
    protected virtual void OnClick(EventArgs e) => Click?.Invoke(this, e);

    #endregion
}
