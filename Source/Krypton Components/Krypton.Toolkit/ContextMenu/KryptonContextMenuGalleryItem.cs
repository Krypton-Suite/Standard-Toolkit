#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// A single selectable entry in a <see cref="KryptonContextMenuGallery"/>.
/// </summary>
[ToolboxItem(false)]
[DesignTimeVisible(false)]
[DesignerCategory(@"code")]
[DefaultProperty(nameof(Text))]
public class KryptonContextMenuGalleryItem : Component, INotifyPropertyChanged
{
    #region Instance Fields

    private string _text;
    private Image? _image;
    private int _imageIndex;
    private bool _enabled;
    private bool _visible;
    private object? _tag;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when a property value changes.")]
    public event PropertyChangedEventHandler? PropertyChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonContextMenuGalleryItem"/> class.
    /// </summary>
    public KryptonContextMenuGalleryItem()
        : this(string.Empty, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonContextMenuGalleryItem"/> class.
    /// </summary>
    /// <param name="text">Caption drawn when the gallery shows item text.</param>
    public KryptonContextMenuGalleryItem(string text)
        : this(text, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonContextMenuGalleryItem"/> class.
    /// </summary>
    /// <param name="text">Caption drawn when the gallery shows item text.</param>
    /// <param name="image">Image drawn for the item; <see cref="ImageIndex"/> is used when this is null.</param>
    public KryptonContextMenuGalleryItem(string text, Image? image)
    {
        _text = text ?? string.Empty;
        _image = image;
        _imageIndex = -1;
        _enabled = true;
        _visible = true;
    }

    /// <inheritdoc />
    public override string ToString() => string.IsNullOrEmpty(Text) ? "(GalleryItem)" : Text;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the caption drawn under or beside the image when <see cref="KryptonContextMenuGallery.ShowItemText"/> is true.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Caption drawn when the gallery shows item text.")]
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
    /// Gets or sets the image drawn for this item. When null, <see cref="ImageIndex"/> into the parent gallery ImageList is used.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Image drawn for the gallery item.")]
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
    /// Gets or sets the ImageList index used when <see cref="Image"/> is null.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"ImageList index used when Image is not set.")]
    [DefaultValue(-1)]
    public int ImageIndex
    {
        get => _imageIndex;
        set
        {
            if (_imageIndex != value)
            {
                _imageIndex = value;
                OnPropertyChanged(nameof(ImageIndex));
            }
        }
    }

    /// <summary>
    /// Gets or sets whether the item can be selected.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the gallery item can be selected.")]
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
    /// Gets or sets whether the item is shown in the gallery.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the gallery item is visible.")]
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
    /// Gets or sets user-defined data associated with the item.
    /// </summary>
    [Category(@"Data")]
    [Description(@"User-defined data associated with the gallery item.")]
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
