#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Layout and visual settings for <see cref="KryptonDropZone"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonDropZoneAppearanceValues : Storage
{
    #region Static Defaults

    private const DropZoneLayout DEFAULT_LAYOUT = DropZoneLayout.Classic;
    private const int DEFAULT_MIN_DROP_ZONE_HEIGHT = 160;
    private const int DEFAULT_PREVIEW_LIST_HEIGHT = 80;
    private const int DEFAULT_STRIPE_WIDTH = 12;
    private const int DEFAULT_STRIPE_ANIMATION_INTERVAL = 50;
    private const bool DEFAULT_SHOW_UPLOAD_ICON = true;
    private const bool DEFAULT_SHOW_STRIPED_DRAG_FEEDBACK = true;
    private const bool DEFAULT_SHOW_PREVIEW_HEADER = true;
    private const bool DEFAULT_SHOW_ACTION_BUTTONS = true;
    private const bool DEFAULT_USE_PALETTE_COLORS = true;
    private const int DEFAULT_UPLOAD_ICON_SIZE = 64;

    #endregion

    #region Instance Fields

    private KryptonDropZone? _owner;
    private DropZoneLayout _layout = DEFAULT_LAYOUT;
    private int _minDropZoneHeight = DEFAULT_MIN_DROP_ZONE_HEIGHT;
    private int _previewListHeight = DEFAULT_PREVIEW_LIST_HEIGHT;
    private int _stripeWidth = DEFAULT_STRIPE_WIDTH;
    private int _stripeAnimationInterval = DEFAULT_STRIPE_ANIMATION_INTERVAL;
    private bool _showUploadIcon = DEFAULT_SHOW_UPLOAD_ICON;
    private bool _showStripedDragFeedback = DEFAULT_SHOW_STRIPED_DRAG_FEEDBACK;
    private bool _showPreviewHeader = DEFAULT_SHOW_PREVIEW_HEADER;
    private bool _showActionButtons = DEFAULT_SHOW_ACTION_BUTTONS;
    private bool _usePaletteColors = DEFAULT_USE_PALETTE_COLORS;
    private Image? _uploadIcon;
    private int _uploadIconSize = DEFAULT_UPLOAD_ICON_SIZE;

    #endregion

    #region Identity

    internal KryptonDropZoneAppearanceValues(KryptonDropZone owner) => _owner = owner;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        _layout == DEFAULT_LAYOUT &&
        _minDropZoneHeight == DEFAULT_MIN_DROP_ZONE_HEIGHT &&
        _previewListHeight == DEFAULT_PREVIEW_LIST_HEIGHT &&
        _stripeWidth == DEFAULT_STRIPE_WIDTH &&
        _stripeAnimationInterval == DEFAULT_STRIPE_ANIMATION_INTERVAL &&
        _showUploadIcon == DEFAULT_SHOW_UPLOAD_ICON &&
        _showStripedDragFeedback == DEFAULT_SHOW_STRIPED_DRAG_FEEDBACK &&
        _showPreviewHeader == DEFAULT_SHOW_PREVIEW_HEADER &&
        _showActionButtons == DEFAULT_SHOW_ACTION_BUTTONS &&
        _usePaletteColors == DEFAULT_USE_PALETTE_COLORS &&
        _uploadIcon == null &&
        _uploadIconSize == DEFAULT_UPLOAD_ICON_SIZE;

    #endregion

    #region Layout

    [Category(@"Layout")]
    [Description(@"Visual layout of the drop zone. Card provides a centered upload area with dashed border, animated drag stripes, preview section, and action buttons.")]
    [DefaultValue(DEFAULT_LAYOUT)]
    public DropZoneLayout Layout
    {
        get => _layout;
        set
        {
            if (_layout == value)
            {
                return;
            }

            _layout = value;
            _owner?.OnAppearanceValuesChanged();
        }
    }

    [Category(@"Layout")]
    [Description(@"Minimum height of the drop zone panel when Layout is Card.")]
    [DefaultValue(DEFAULT_MIN_DROP_ZONE_HEIGHT)]
    public int MinDropZoneHeight
    {
        get => _minDropZoneHeight;
        set
        {
            int height = Math.Max(80, value);
            if (_minDropZoneHeight == height)
            {
                return;
            }

            _minDropZoneHeight = height;
            _owner?.OnAppearanceValuesChanged();
        }
    }

    [Category(@"Layout")]
    [Description(@"Height of the file preview list when Layout is Card.")]
    [DefaultValue(DEFAULT_PREVIEW_LIST_HEIGHT)]
    public int PreviewListHeight
    {
        get => _previewListHeight;
        set
        {
            int height = Math.Max(48, value);
            if (_previewListHeight == height)
            {
                return;
            }

            _previewListHeight = height;
            _owner?.OnAppearanceValuesChanged();
        }
    }

    [Category(@"Layout")]
    [Description(@"Whether to show a centered upload icon above the drop zone text in Card layout.")]
    [DefaultValue(DEFAULT_SHOW_UPLOAD_ICON)]
    public bool ShowUploadIcon
    {
        get => _showUploadIcon;
        set
        {
            if (_showUploadIcon == value)
            {
                return;
            }

            _showUploadIcon = value;
            _owner?.OnAppearanceValuesChanged();
        }
    }

    [Category(@"Layout")]
    [Description(@"Custom image displayed above the drop zone text in Card layout. When null, the default UploadDocument image from DropZoneResources is used at full color. Custom images are tinted using the palette content color when UsePaletteColors is true.")]
    [DefaultValue(null)]
    public Image? UploadIcon
    {
        get => _uploadIcon;
        set
        {
            if (_uploadIcon == value)
            {
                return;
            }

            _uploadIcon = value;
            _owner?.OnAppearanceValuesChanged();
        }
    }

    [Category(@"Layout")]
    [Description(@"Display size in pixels of the upload icon in Card layout.")]
    [DefaultValue(DEFAULT_UPLOAD_ICON_SIZE)]
    public int UploadIconSize
    {
        get => _uploadIconSize;
        set
        {
            int size = Math.Max(16, Math.Min(128, value));
            if (_uploadIconSize == size)
            {
                return;
            }

            _uploadIconSize = size;
            _owner?.OnAppearanceValuesChanged();
        }
    }

    [Category(@"Layout")]
    [Description(@"Whether to show a Preview header above the file list in Card layout.")]
    [DefaultValue(DEFAULT_SHOW_PREVIEW_HEADER)]
    public bool ShowPreviewHeader
    {
        get => _showPreviewHeader;
        set
        {
            if (_showPreviewHeader == value)
            {
                return;
            }

            _showPreviewHeader = value;
            _owner?.OnAppearanceValuesChanged();
        }
    }

    [Category(@"Layout")]
    [Description(@"Whether to show Cancel and Submit action buttons in Card layout.")]
    [DefaultValue(DEFAULT_SHOW_ACTION_BUTTONS)]
    public bool ShowActionButtons
    {
        get => _showActionButtons;
        set
        {
            if (_showActionButtons == value)
            {
                return;
            }

            _showActionButtons = value;
            _owner?.OnAppearanceValuesChanged();
        }
    }

    #endregion

    #region Drag Feedback

    [Category(@"Drag Feedback")]
    [Description(@"Whether to animate diagonal stripes while dragging files over the drop zone in Card layout.")]
    [DefaultValue(DEFAULT_SHOW_STRIPED_DRAG_FEEDBACK)]
    public bool ShowStripedDragFeedback
    {
        get => _showStripedDragFeedback;
        set
        {
            if (_showStripedDragFeedback == value)
            {
                return;
            }

            _showStripedDragFeedback = value;
            _owner?.OnAppearanceValuesChanged();
        }
    }

    [Category(@"Drag Feedback")]
    [Description(@"Width in pixels of each diagonal stripe during drag feedback.")]
    [DefaultValue(DEFAULT_STRIPE_WIDTH)]
    public int StripeWidth
    {
        get => _stripeWidth;
        set => _stripeWidth = Math.Max(4, value);
    }

    [Category(@"Drag Feedback")]
    [Description(@"Interval in milliseconds between stripe animation frames.")]
    [DefaultValue(DEFAULT_STRIPE_ANIMATION_INTERVAL)]
    public int StripeAnimationInterval
    {
        get => _stripeAnimationInterval;
        set => _stripeAnimationInterval = Math.Max(16, value);
    }

    #endregion

    #region Colors

    /// <summary>
    /// Gets or sets a value indicating whether the Card layout derives idle border, background, icon, and stripe colors from the active Krypton palette.
    /// </summary>
    [Category(@"Colors")]
    [Description(@"When true, Card layout derives idle border, background, icon, and stripe colors from the active Krypton palette.")]
    [DefaultValue(DEFAULT_USE_PALETTE_COLORS)]
    public bool UsePaletteColors
    {
        get => _usePaletteColors;
        set
        {
            if (_usePaletteColors == value)
            {
                return;
            }

            _usePaletteColors = value;
            _owner?.OnAppearanceValuesChanged();
        }
    }

    #endregion

    #region Implementation

    public void Reset()
    {
        Layout = DEFAULT_LAYOUT;
        MinDropZoneHeight = DEFAULT_MIN_DROP_ZONE_HEIGHT;
        PreviewListHeight = DEFAULT_PREVIEW_LIST_HEIGHT;
        StripeWidth = DEFAULT_STRIPE_WIDTH;
        StripeAnimationInterval = DEFAULT_STRIPE_ANIMATION_INTERVAL;
        ShowUploadIcon = DEFAULT_SHOW_UPLOAD_ICON;
        ShowStripedDragFeedback = DEFAULT_SHOW_STRIPED_DRAG_FEEDBACK;
        ShowPreviewHeader = DEFAULT_SHOW_PREVIEW_HEADER;
        ShowActionButtons = DEFAULT_SHOW_ACTION_BUTTONS;
        UsePaletteColors = DEFAULT_USE_PALETTE_COLORS;
        UploadIcon = null;
        UploadIconSize = DEFAULT_UPLOAD_ICON_SIZE;
    }

    internal void SetOwner(KryptonDropZone owner) => _owner = owner;

    #endregion
}

/// <summary>Visual layout modes for <see cref="KryptonDropZone"/>.</summary>
public enum DropZoneLayout
{
    /// <summary>Label, browse button, and details list (original layout).</summary>
    Classic,

    /// <summary>Centered upload area with dashed border, drag stripes, preview thumbnails, and action buttons.</summary>
    Card
}
