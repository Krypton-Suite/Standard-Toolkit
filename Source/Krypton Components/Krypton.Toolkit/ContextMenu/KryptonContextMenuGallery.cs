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
/// In-menu gallery of images or captioned items with hover tracking for live preview and an optional More submenu.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonContextMenuImageSelect), "ToolboxBitmaps.KryptonContextMenuImageSelect.bmp")]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultProperty(nameof(Items))]
[DefaultEvent(nameof(SelectedIndexChanged))]
public class KryptonContextMenuGallery : KryptonContextMenuItemBase
{
    #region Instance Fields

    private Padding _padding;
    private ImageList? _imageList;
    private ButtonStyle _style;
    private bool _autoClose;
    private bool _showItemText;
    private int _selectedIndex;
    private int _imageIndexStart;
    private int _imageIndexEnd;
    private int _lineItems;
    private int _trackingIndex;
    private int _cacheTrackingIndex;
    private int _eventTrackingIndex;
    private string _moreText;
    private readonly KryptonContextMenuItem _moreItem;
    private readonly System.Windows.Forms.Timer _trackingEventTimer;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the value of the SelectedIndex property changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the selected gallery item changes.")]
    public event EventHandler? SelectedIndexChanged;

    /// <summary>
    /// Occurs when the user is tracking over a gallery item.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the user is tracking over a gallery item. Use for live preview.")]
    public event EventHandler<ImageSelectEventArgs>? TrackingImage;

    /// <summary>
    /// Occurs when a gallery item is clicked.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when a gallery item is clicked.")]
    public event EventHandler? Click;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonContextMenuGallery"/> class.
    /// </summary>
    public KryptonContextMenuGallery()
    {
        _autoClose = true;
        _showItemText = false;
        _selectedIndex = -1;
        _trackingIndex = -1;
        _imageList = null;
        _imageIndexStart = -1;
        _imageIndexEnd = -1;
        _lineItems = 5;
        _padding = new Padding(2);
        _style = ButtonStyle.LowProfile;
        _moreText = KryptonManager.Strings.ContextMenuStrings.GalleryMore;
        Items = [];
        Ranges = [];
        _moreItem = new KryptonContextMenuItem(_moreText);

        _trackingEventTimer = new System.Windows.Forms.Timer
        {
            Interval = 120
        };
        _trackingEventTimer.Tick += OnTrackingTick;
    }

    /// <inheritdoc />
    public override string ToString() => "(Gallery)";

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _trackingEventTimer.Stop();
            _trackingEventTimer.Dispose();
            _moreItem.Dispose();
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public

    /// <inheritdoc />
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override int ItemChildCount => MoreItems.Count > 0 ? 1 : 0;

    /// <inheritdoc />
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override KryptonContextMenuItemBase? this[int index] => index == 0 && MoreItems.Count > 0 ? _moreItem : null;

    /// <inheritdoc />
    public override bool ProcessShortcut(Keys keyData) => _moreItem.ProcessShortcut(keyData);

    /// <inheritdoc />
    public override ViewBase GenerateView(IContextMenuProvider provider,
        object parent,
        ViewLayoutStack columns,
        bool standardStyle,
        bool imageColumn)
    {
        SetProvider(provider);
        return new ViewLayoutMenuGallery(this, provider);
    }

    /// <summary>
    /// Gets the gallery items. When empty, items are synthesised from <see cref="ImageList"/>.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Gallery items. When empty, items are synthesised from ImageList.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [MergableProperty(false)]
    public KryptonContextMenuGalleryItemCollection Items { get; }

    /// <summary>
    /// Gets the optional heading ranges that group items.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Optional heading ranges that group gallery items.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [MergableProperty(false)]
    public KryptonContextMenuGalleryRangeCollection Ranges { get; }

    /// <summary>
    /// Gets the submenu items shown under the More command.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Submenu items shown when the More command is present.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [MergableProperty(false)]
    public KryptonContextMenuCollection MoreItems => _moreItem.Items;

    /// <summary>
    /// Gets or sets the More command text.
    /// </summary>
    [KryptonPersist]
    [Category(@"Appearance")]
    [Description(@"Text of the More command that opens the extra submenu.")]
    [Localizable(true)]
    public string MoreText
    {
        get => _moreText;
        set
        {
            value ??= string.Empty;
            if (_moreText != value)
            {
                _moreText = value;
                _moreItem.Text = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(MoreText)));
            }
        }
    }

    private bool ShouldSerializeMoreText() =>
        !string.Equals(_moreText, KryptonManager.Strings.ContextMenuStrings.GalleryMore);

    private void ResetMoreText() => MoreText = KryptonManager.Strings.ContextMenuStrings.GalleryMore;

    /// <summary>
    /// Gets or sets padding around the gallery grid.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Padding used around the gallery grid.")]
    [DefaultValue(typeof(Padding), "2,2,2,2")]
    public Padding Padding
    {
        get => _padding;
        set
        {
            if (_padding != value)
            {
                _padding = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Padding)));
            }
        }
    }

    /// <summary>
    /// Gets or sets whether selecting an item automatically closes the context menu.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Indicates if selecting a gallery item automatically closes the context menu.")]
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
    /// Gets or sets whether each gallery item draws its <see cref="KryptonContextMenuGalleryItem.Text"/>.
    /// </summary>
    [KryptonPersist]
    [Category(@"Appearance")]
    [Description(@"Draw item captions under gallery images.")]
    [DefaultValue(false)]
    public bool ShowItemText
    {
        get => _showItemText;
        set
        {
            if (_showItemText != value)
            {
                _showItemText = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ShowItemText)));
            }
        }
    }

    /// <summary>
    /// Gets or sets the index of the selected gallery item.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"The index of the selected gallery item.")]
    [DefaultValue(-1)]
    public int SelectedIndex
    {
        get => _selectedIndex;
        set
        {
            if (_selectedIndex != value)
            {
                _selectedIndex = value;
                OnSelectedIndexChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Gets or sets the button style used for each gallery item.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Button style used for each gallery item.")]
    [DefaultValue(ButtonStyle.LowProfile)]
    public ButtonStyle ButtonStyle
    {
        get => _style;
        set
        {
            if (_style != value)
            {
                _style = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ButtonStyle)));
            }
        }
    }

    /// <summary>
    /// Gets or sets the ImageList used when <see cref="Items"/> is empty, or as a fallback for item <see cref="KryptonContextMenuGalleryItem.ImageIndex"/>.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"ImageList used when Items is empty, or as a fallback for item ImageIndex.")]
    [DefaultValue(null)]
    public ImageList? ImageList
    {
        get => _imageList;
        set
        {
            if (_imageList != value)
            {
                _imageList = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ImageList)));
            }
        }
    }

    /// <summary>
    /// Gets or sets the first ImageList index when synthesising items from <see cref="ImageList"/>.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Index of first image in the ImageList when Items is empty.")]
    [DefaultValue(-1)]
    public int ImageIndexStart
    {
        get => _imageIndexStart;
        set
        {
            if (_imageIndexStart != value)
            {
                _imageIndexStart = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ImageIndexStart)));
            }
        }
    }

    /// <summary>
    /// Gets or sets the last ImageList index when synthesising items from <see cref="ImageList"/>.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Index of last image in the ImageList when Items is empty.")]
    [DefaultValue(-1)]
    public int ImageIndexEnd
    {
        get => _imageIndexEnd;
        set
        {
            if (_imageIndexEnd != value)
            {
                _imageIndexEnd = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ImageIndexEnd)));
            }
        }
    }

    /// <summary>
    /// Gets or sets the number of items to place on each display line.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Number of items to place on each display line.")]
    [DefaultValue(5)]
    public int LineItems
    {
        get => _lineItems;
        set
        {
            if (_lineItems != value)
            {
                _lineItems = Math.Max(1, value);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(LineItems)));
            }
        }
    }

    #endregion

    #region Protected Virtual

    /// <summary>
    /// Raises the <see cref="SelectedIndexChanged"/> event.
    /// </summary>
    /// <param name="e">Event data.</param>
    protected virtual void OnSelectedIndexChanged(EventArgs e) => SelectedIndexChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the <see cref="TrackingImage"/> event.
    /// </summary>
    /// <param name="e">Event data.</param>
    protected virtual void OnTrackingImage(ImageSelectEventArgs e)
    {
        _eventTrackingIndex = e.ImageIndex;
        TrackingImage?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="Click"/> event.
    /// </summary>
    /// <param name="e">Event data.</param>
    internal virtual void OnClick(EventArgs e) => Click?.Invoke(this, e);

    #endregion

    #region Internal

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal int TrackingIndex
    {
        get => _trackingIndex;
        set
        {
            if (_trackingIndex != value)
            {
                _trackingIndex = value;
                _cacheTrackingIndex = _trackingIndex;
                _trackingEventTimer.Stop();
                _trackingEventTimer.Start();
            }
        }
    }

    internal KryptonContextMenuItem MoreItem => _moreItem;

    internal IReadOnlyList<KryptonContextMenuGalleryItem> GetResolvedItems()
    {
        if (Items.Count > 0)
        {
            var list = new List<KryptonContextMenuGalleryItem>();
            foreach (KryptonContextMenuGalleryItem item in Items)
            {
                if (item.Visible)
                {
                    list.Add(item);
                }
            }

            return list;
        }

        if (_imageList == null || _imageList.Images.Count == 0)
        {
            return Array.Empty<KryptonContextMenuGalleryItem>();
        }

        var start = Math.Max(0, _imageIndexStart);
        var end = _imageIndexEnd < 0 ? _imageList.Images.Count - 1 : Math.Min(_imageIndexEnd, _imageList.Images.Count - 1);
        if (end < start)
        {
            return Array.Empty<KryptonContextMenuGalleryItem>();
        }

        var synthesised = new List<KryptonContextMenuGalleryItem>(end - start + 1);
        for (var i = start; i <= end; i++)
        {
            synthesised.Add(new KryptonContextMenuGalleryItem(string.Empty, _imageList.Images[i])
            {
                ImageIndex = i
            });
        }

        return synthesised;
    }

    #endregion

    #region Implementation

    private void OnTrackingTick(object? sender, EventArgs e)
    {
        if (_trackingIndex == _cacheTrackingIndex)
        {
            _trackingEventTimer.Stop();
            if (_eventTrackingIndex != _trackingIndex)
            {
                OnTrackingImage(new ImageSelectEventArgs(_imageList, _trackingIndex));
            }
        }
        else
        {
            _cacheTrackingIndex = _trackingIndex;
        }
    }

    #endregion
}
