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
/// Compact one-row gallery on a <see cref="KryptonMiniToolbar"/>.
/// </summary>
[ToolboxItem(false)]
[DesignTimeVisible(false)]
[DesignerCategory(@"code")]
[DefaultProperty(nameof(ImageList))]
[DefaultEvent(nameof(SelectedIndexChanged))]
public class KryptonMiniToolbarGallery : KryptonMiniToolbarItemBase
{
    #region Instance Fields

    private ImageList? _imageList;
    private int _selectedIndex;
    private int _imageIndexStart;
    private int _imageIndexEnd;
    private int _maxVisibleItems;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the selected gallery index changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the selected gallery item changes.")]
    public event EventHandler? SelectedIndexChanged;

    /// <summary>
    /// Occurs when the user tracks over a gallery image.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the user tracks over a gallery image. Use for live preview.")]
    public event EventHandler<ImageSelectEventArgs>? TrackingImage;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonMiniToolbarGallery"/> class.
    /// </summary>
    public KryptonMiniToolbarGallery()
    {
        _selectedIndex = -1;
        _imageIndexStart = -1;
        _imageIndexEnd = -1;
        _maxVisibleItems = 6;
    }

    /// <inheritdoc />
    public override string ToString() => "(Gallery)";

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the images shown in the compact gallery.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Images shown in the compact gallery.")]
    [DefaultValue(null)]
    public ImageList? ImageList
    {
        get => _imageList;
        set
        {
            if (_imageList != value)
            {
                _imageList = value;
                OnPropertyChanged(nameof(ImageList));
            }
        }
    }

    /// <summary>
    /// Gets or sets the selected image index.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The index of the selected gallery image.")]
    [DefaultValue(-1)]
    public int SelectedIndex
    {
        get => _selectedIndex;
        set
        {
            if (_selectedIndex != value)
            {
                _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
                SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Gets or sets the first ImageList index to display.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Index of first image in the ImageList for display.")]
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
    /// Gets or sets the last ImageList index to display.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Index of last image in the ImageList for display.")]
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

    /// <summary>
    /// Gets or sets how many gallery images are shown in the Mini Toolbar row.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Maximum number of gallery images shown in the Mini Toolbar row.")]
    [DefaultValue(6)]
    public int MaxVisibleItems
    {
        get => _maxVisibleItems;
        set
        {
            value = Math.Max(1, value);
            if (_maxVisibleItems != value)
            {
                _maxVisibleItems = value;
                OnPropertyChanged(nameof(MaxVisibleItems));
            }
        }
    }

    /// <summary>
    /// Raises <see cref="TrackingImage"/>.
    /// </summary>
    /// <param name="e">Event data.</param>
    internal void RaiseTrackingImage(ImageSelectEventArgs e) => TrackingImage?.Invoke(this, e);

    #endregion
}
