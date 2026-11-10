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
/// Named group of gallery items, drawn with an optional heading row.
/// </summary>
[ToolboxItem(false)]
[DesignTimeVisible(false)]
[DesignerCategory(@"code")]
[DefaultProperty(nameof(Heading))]
public class KryptonContextMenuGalleryRange : Component, INotifyPropertyChanged
{
    #region Instance Fields

    private string _heading;
    private int _imageIndexStart;
    private int _imageIndexEnd;

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
    /// Initialize a new instance of the <see cref="KryptonContextMenuGalleryRange"/> class.
    /// </summary>
    public KryptonContextMenuGalleryRange()
    {
        _heading = @"Heading";
        _imageIndexStart = -1;
        _imageIndexEnd = -1;
    }

    /// <inheritdoc />
    public override string ToString() => string.IsNullOrEmpty(Heading) ? "(GalleryRange)" : Heading;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the heading text drawn above this range. Empty hides the heading.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Heading text drawn above this range of gallery items.")]
    [DefaultValue(@"Heading")]
    [Localizable(true)]
    public string Heading
    {
        get => _heading;
        set
        {
            value ??= string.Empty;
            if (_heading != value)
            {
                _heading = value;
                OnPropertyChanged(nameof(Heading));
            }
        }
    }

    /// <summary>
    /// Gets or sets the first item or ImageList index in this range. -1 means the start of the gallery.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"First item or ImageList index in this range. -1 means the start of the gallery.")]
    [DefaultValue(-1)]
    public int ImageIndexStart
    {
        get => _imageIndexStart;
        set
        {
            if (_imageIndexStart != value)
            {
                _imageIndexStart = value;
                OnPropertyChanged(nameof(ImageIndexStart));
            }
        }
    }

    /// <summary>
    /// Gets or sets the last item or ImageList index in this range. -1 means the end of the gallery.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Last item or ImageList index in this range. -1 means the end of the gallery.")]
    [DefaultValue(-1)]
    public int ImageIndexEnd
    {
        get => _imageIndexEnd;
        set
        {
            if (_imageIndexEnd != value)
            {
                _imageIndexEnd = value;
                OnPropertyChanged(nameof(ImageIndexEnd));
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
