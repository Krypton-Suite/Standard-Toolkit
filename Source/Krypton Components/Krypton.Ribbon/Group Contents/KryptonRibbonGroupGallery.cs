#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Represents a ribbon group separator.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonRibbonGroupGallery), "ToolboxBitmaps.KryptonGallery.bmp")]
[Designer(typeof(KryptonRibbonGroupGalleryDesigner))]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultProperty(nameof(Visible))]
public class KryptonRibbonGroupGallery : KryptonRibbonGroupContainer
{
    #region Static Fields
    private static readonly Image _defaultButtonImageLarge = GenericImageResources.ButtonImageLarge;
    #endregion

    #region Instance Fields
    private bool _visible;
    private bool _enabled;
    private string _textLine1;
    private string _textLine2;
    private Image? _imageLarge;
    private string _keyTip;
    private GroupItemSize _itemSizeMax;
    private GroupItemSize _itemSizeMin;
    private GroupItemSize _itemSizeCurrent;
    private int _largeItemCount;
    private int _mediumItemCount;
    private int _dropButtonItemWidth;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the value of the ImageList property changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of the ImageList property changes.")]
    public event EventHandler? ImageListChanged;

    /// <summary>
    /// Occurs when the value of the SelectedIndex property changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of the SelectedIndex property changes.")]
    public event EventHandler? SelectedIndexChanged;

    /// <summary>
    /// Occurs when the user is tracking over a color.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when user is tracking over an image.")]
    public event EventHandler<ImageSelectEventArgs>? TrackingImage;

    /// <summary>
    /// Occurs when the user invokes the drop-down menu.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when user invokes the drop-down menu.")]
    public event EventHandler<GalleryDropMenuEventArgs>? GalleryDropMenu;

    /// <summary>
    /// Occurs after the value of a property has changed.
    /// </summary>
    [Category(@"Ribbon")]
    [Description(@"Occurs after the value of a property has changed.")]
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Occurs when the control receives focus.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event EventHandler? GotFocus;

    /// <summary>
    /// Occurs when the control loses focus.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event EventHandler? LostFocus;

    /// <summary>
    /// Occurs when the design time context menu is requested.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event MouseEventHandler? DesignTimeContextMenu;

    internal event EventHandler? MouseEnterControl;
    internal event EventHandler? MouseLeaveControl;
    #endregion

    #region Identity
    /// <summary>
    /// Initialise a new instance of the KryptonRibbonGroupGallery class.
    /// </summary>
    public KryptonRibbonGroupGallery()
    {
        // Default fields
        _visible = true;
        _enabled = true;
        _keyTip = "X";
        _itemSizeMax = GroupItemSize.Large;
        _itemSizeMin = GroupItemSize.Small;
        _itemSizeCurrent = GroupItemSize.Large;
        _largeItemCount = 9;
        _mediumItemCount = 3;
        _dropButtonItemWidth = 9;
        _imageLarge = _defaultButtonImageLarge;
        _textLine1 = nameof(Gallery);
        _textLine2 = string.Empty;

        // Create the actual text box control and set initial settings
        Gallery = new KryptonGallery
        {
            AlwaysActive = false,
            TabStop = false,
            InternalPreferredItemSize = new Size(_largeItemCount, 1)
        };

        // Hook into events to expose via this container
        Gallery.SelectedIndexChanged += OnGallerySelectedIndexChanged;
        Gallery.ImageListChanged += OnGalleryImageListChanged;
        Gallery.TrackingImage += OnGalleryTrackingImage;
        Gallery.GalleryDropMenu += OnGalleryGalleryDropMenu;
        Gallery.GotFocus += OnGalleryGotFocus;
        Gallery.LostFocus += OnGalleryLostFocus;

        // Ensure we can track mouse events on the gallery
        MonitorControl(Gallery);
    }
    #endregion

    #region Public
    /// <summary>
    /// Access to the actual embedded KryptonGallery instance.
    /// </summary>
    [Description(@"Access to the actual embedded KryptonGallery instance.")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonGallery Gallery { get; }

    /// <summary>
    /// Gets the collection of drop-down ranges.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Collection of drop-down ranges")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonGalleryRangeCollection DropButtonRanges => Gallery.DropButtonRanges;

    /// <summary>
    /// Gets and sets if scrolling is animated or a jump straight to target..
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Determines if scrolling is animated or a jump straight to target.")]
    [DefaultValue(true)]
    public bool SmoothScrolling
    {
        get => Gallery.SmoothScrolling;

        set
        {
            Gallery.SmoothScrolling = value;
            OnPropertyChanged(nameof(SmoothScrolling));
        }
    }

    /// <summary>
    /// Gets access to the collection of images for display and selection.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Collection of images for display and selection.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public ImageList? ImageList
    {
        get => Gallery.ImageList;

        set
        {
            if (Gallery.ImageList != value)
            {
                Gallery.ImageList = value;
                OnPropertyChanged(nameof(ImageList));
            }
        }
    }

    /// <summary>
    /// Gets access to the collection of images for display and selection.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The index of the selected image.")]
    [DefaultValue(-1)]
    public int SelectedIndex
    {
        get => Gallery.SelectedIndex;

        set
        {
            if (Gallery.SelectedIndex != value)
            {
                Gallery.SelectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }
    }

    /// <summary>
    /// Gets and sets the number of horizontal items when in large setting.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Number of horizontal Displayed items when in large setting.")]
    [DefaultValue(9)]
    public int LargeItemCount
    {
        get => _largeItemCount;

        set
        {
            if (_largeItemCount != value)
            {
                _largeItemCount = value;

                // Ensure the large count can never be less than the medium count
                if (_largeItemCount < _mediumItemCount)
                {
                    _mediumItemCount = _largeItemCount;
                }

                OnPropertyChanged(nameof(LargeItemCount));
            }
        }
    }

    /// <summary>
    /// Gets and sets the number of horizontal items when in medium setting.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Number of horizontal Displayed items when in medium setting.")]
    [DefaultValue(3)]
    public int MediumItemCount
    {
        get => _mediumItemCount;

        set
        {
            if (_mediumItemCount != value)
            {
                _mediumItemCount = value;

                // Ensure the medium count can never be more than the large count
                if (_mediumItemCount > _largeItemCount)
                {
                    _largeItemCount = _mediumItemCount;
                }

                OnPropertyChanged(nameof(MediumItemCount));
            }
        }
    }

    /// <summary>
    /// Gets and sets the number of horizontal Displayed items when showing drop menu from the large button.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Number of horizontal Displayed items when showing drop menu from the large button.")]
    [DefaultValue(9)]
    public int DropButtonItemWidth
    {
        get => _dropButtonItemWidth;

        set
        {
            if (_dropButtonItemWidth != value)
            {
                value = Math.Max(1, value);
                _dropButtonItemWidth = value;
                OnPropertyChanged(nameof(DropButtonItemWidth));
            }
        }
    }

    /// <summary>
    /// Gets and sets the maximum number of lines items for the drop-down menu.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Maximum number of line items for the drop-down menu.")]
    [DefaultValue(128)]
    public int DropMaxItemWidth
    {
        get => Gallery.DropMaxItemWidth;

        set
        {
            if (Gallery.DropMaxItemWidth != value)
            {
                Gallery.DropMaxItemWidth = value;
                OnPropertyChanged(nameof(DropMaxItemWidth));
            }
        }
    }

    /// <summary>
    /// Gets and sets the minimum number of lines items for the drop-down menu.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Minimum number of line items for the drop-down menu.")]
    [DefaultValue(3)]
    public int DropMinItemWidth
    {
        get => Gallery.DropMinItemWidth;

        set
        {
            if (Gallery.DropMinItemWidth != value)
            {
                Gallery.DropMinItemWidth = value;
                OnPropertyChanged(nameof(DropMinItemWidth));
            }
        }
    }

    /// <summary>
    /// Gets and sets the associated context menu strip.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The shortcut to display when the user right-clicks the control.")]
    [DefaultValue(null)]
    public ContextMenuStrip? ContextMenuStrip
    {
        get => Gallery.ContextMenuStrip;
        set => Gallery.ContextMenuStrip = value;
    }

    /// <summary>
    /// Gets and sets the KryptonContextMenu for showing when the gallery is right clicked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"KryptonContextMenu to be shown when the gallery is right clicked.")]
    [DefaultValue(null)]
    public KryptonContextMenu? KryptonContextMenu
    {
        get => Gallery.KryptonContextMenu;
        set => Gallery.KryptonContextMenu = value;
    }

    /// <summary>
    /// Gets and sets the key tip for the ribbon group gallery.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Ribbon group gallery key tip.")]
    [DefaultValue("X")]
    public string KeyTip
    {
        get => _keyTip;

        set
        {
            if (string.IsNullOrEmpty(value))
            {
                value = @"X";
            }

            _keyTip = value.ToUpper();
        }
    }

    /// <summary>
    /// Gets and sets the large button image.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Large gallery button image.")]
    [RefreshProperties(RefreshProperties.All)]
    public Image? ImageLarge
    {
        get => _imageLarge;

        set
        {
            if (_imageLarge != value)
            {
                _imageLarge = value;
                OnPropertyChanged(nameof(ImageLarge));
            }
        }
    }

    private bool ShouldSerializeImageLarge() => ImageLarge != _defaultButtonImageLarge;

    /// <summary>
    /// Gets and sets the display gallery text line 1 for the button.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Gallery button display text line 1.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(nameof(Gallery))]
    public string TextLine1
    {
        get => _textLine1;

        set
        {
            // We never allow an empty text value
            if (string.IsNullOrEmpty(value))
            {
                value = nameof(Gallery);
            }

            if (value != _textLine1)
            {
                _textLine1 = value;
                OnPropertyChanged(nameof(TextLine1));
            }
        }
    }

    /// <summary>
    /// Gets and sets the display gallery text line 2 for the button.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Gallery button display text line 2.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("")]
    public string TextLine2
    {
        get => _textLine2;

        set
        {
            if (value != _textLine2)
            {
                _textLine2 = value;
                OnPropertyChanged(nameof(TextLine2));
            }
        }
    }
    /// <summary>
    /// Gets access to the Wrapped Controls Tooltips.
    /// </summary>
    public override ToolTipValues ToolTipValues => _toolTipValues;


    /// <summary>
    /// Gets and sets the visible state of the group gallery.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the group gallery is visible or hidden.")]
    [DefaultValue(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override bool Visible
    {
        get => _visible;

        set
        {
            if (value != _visible)
            {
                _visible = value;
                OnPropertyChanged(nameof(Visible));
            }
        }
    }

    /// <summary>
    /// Make the ribbon group gallery visible.
    /// </summary>
    public void Show() => Visible = true;

    /// <summary>
    /// Make the ribbon group gallery hidden.
    /// </summary>
    public void Hide() => Visible = false;

    /// <summary>
    /// Gets and sets the enabled state of the group gallery.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the group gallery is enabled.")]
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
    /// Gets and sets the maximum allowed size of the gallery.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Maximum size of the gallery.")]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(typeof(GroupItemSize), "Large")]
    [RefreshProperties(RefreshProperties.All)]
    public GroupItemSize MaximumSize
    {
        get => ItemSizeMaximum;
        set => ItemSizeMaximum = value;
    }

    /// <summary>
    /// Gets and sets the minimum allowed size of the gallery.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Minimum size of the gallery.")]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(typeof(GroupItemSize), "Small")]
    [RefreshProperties(RefreshProperties.All)]
    public GroupItemSize MinimumSize
    {
        get => ItemSizeMinimum;
        set => ItemSizeMinimum = value;
    }

    /// <summary>
    /// Gets and sets the maximum allowed size of the item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override GroupItemSize ItemSizeMaximum
    {
        get => _itemSizeMax;

        set
        {
            if (_itemSizeMax != value)
            {
                _itemSizeMax = value;
                OnPropertyChanged(nameof(ItemSizeMaximum));
            }
        }
    }

    /// <summary>
    /// Gets and sets the minimum allowed size of the item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override GroupItemSize ItemSizeMinimum
    {
        get => _itemSizeMin;

        set
        {
            if (_itemSizeMin != value)
            {
                _itemSizeMin = value;
                OnPropertyChanged(nameof(ItemSizeMinimum));
            }
        }
    }

    /// <summary>
    /// Gets and sets the current item size.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override GroupItemSize ItemSizeCurrent
    {
        get => _itemSizeCurrent;

        set
        {
            _itemSizeCurrent = value;

            switch (value)
            {
                case GroupItemSize.Large:
                    Gallery.InternalPreferredItemSize = new Size(InternalItemCount, 1);
                    break;
                case GroupItemSize.Medium:
                    Gallery.InternalPreferredItemSize = new Size(MediumItemCount, 1);
                    break;
            }

            OnPropertyChanged(nameof(ItemSizeCurrent));
        }
    }

    /// <summary>
    /// Creates an appropriate view element for this item.
    /// </summary>
    /// <param name="ribbon">Reference to the owning ribbon control.</param>
    /// <param name="needPaint">Delegate for notifying changes in display.</param>
    /// <returns>ViewBase derived instance.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override ViewBase CreateView(KryptonRibbon ribbon,
        NeedPaintHandler needPaint)
    {
        _toolTipValues.NeedPaint = needPaint;
        return new ViewDrawRibbonGroupGallery(ribbon, this, needPaint);
    }

    /// <summary>
    /// Gets and sets the associated designer.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public IKryptonDesignObject? GalleryDesigner { get; set; }

    /// <summary>
    /// Internal design time properties.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public ViewBase? GalleryView { get; set; }

    #endregion

    #region Internal
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal Control? LastParentControl { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal KryptonGallery? LastGallery { get; set; }

    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    internal NeedPaintHandler? ViewPaintDelegate { get; set; }

    internal void OnDesignTimeContextMenu(MouseEventArgs e) => DesignTimeContextMenu?.Invoke(this, e);

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal int InternalItemCount { get; set; }

    internal override bool ProcessCmdKey(ref Message msg, Keys keyData) => false;

    #endregion

    #region Protected
    /// <summary>
    /// Raises the ImageListChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnImageListChanged(EventArgs e) => ImageListChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the SelectedIndexChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnSelectedIndexChanged(EventArgs e) => SelectedIndexChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the SelectedIndexChanged event.
    /// </summary>
    /// <param name="e">An ImageSelectEventArgs containing the event data.</param>
    protected virtual void OnTrackingImage(ImageSelectEventArgs e) => TrackingImage?.Invoke(this, e);

    /// <summary>
    /// Raises the GalleryDropMenu event.
    /// </summary>
    /// <param name="e">An GalleryDropMenuEventArgs containing the event data.</param>
    protected virtual void OnGalleryDropMenu(GalleryDropMenuEventArgs e) => GalleryDropMenu?.Invoke(this, e);

    /// <summary>
    /// Raises the GotFocus event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnGotFocus(EventArgs e) => GotFocus?.Invoke(this, e);

    /// <summary>
    /// Raises the LostFocus event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnLostFocus(EventArgs e) => LostFocus?.Invoke(this, e);

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">Name of property that has changed.</param>
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion

    #region Implementation
    private void MonitorControl(KryptonGallery c)
    {
        c.MouseEnter += OnControlEnter;
        c.MouseLeave += OnControlLeave;
    }

    private void UnmonitorControl(KryptonGallery c)
    {
        c.MouseEnter -= OnControlEnter;
        c.MouseLeave -= OnControlLeave;
    }

    private void OnControlEnter(object? sender, EventArgs e) => MouseEnterControl?.Invoke(this, e);

    private void OnControlLeave(object? sender, EventArgs e) => MouseLeaveControl?.Invoke(this, e);

    private void OnGalleryImageListChanged(object? sender, EventArgs e) => OnImageListChanged(e);

    private void OnGallerySelectedIndexChanged(object? sender, EventArgs e) => OnSelectedIndexChanged(e);

    private void OnGalleryTrackingImage(object? sender, ImageSelectEventArgs e) => OnTrackingImage(e);

    private void OnGalleryGalleryDropMenu(object? sender, GalleryDropMenuEventArgs e) => OnGalleryDropMenu(e);

    private void OnGalleryGotFocus(object? sender, EventArgs e) => OnGotFocus(e);

    private void OnGalleryLostFocus(object? sender, EventArgs e) => OnLostFocus(e);
    #endregion
}