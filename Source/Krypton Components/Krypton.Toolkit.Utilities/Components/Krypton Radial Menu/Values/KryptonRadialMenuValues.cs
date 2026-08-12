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
/// Appearance values for <see cref="KryptonRadialMenu"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonRadialMenuValues : Storage
{
    #region Instance Fields

    private int _menuRadius;
    private int _innerRadius;
    private Image? _glyph;
    private string _hubText;
    private Color _menuColor;
    private Color _subMenuHoverColor;
    private KryptonRadialMenuDisplayStyle _displayStyle;
    private string _subMenuGlyph;
    private float _outerRingThickness;
    private bool _showOuterRingOnLeaves;
    private float _scale;
    private int _itemImageSize;
    private bool _showShadow;
    private float _shadowOpacity;
    private int _shadowBlur;
    private int _shadowOffset;
    private bool _showCheckedGlyph;
    private float _startAngle;
    private int _maxVisibleItems;
    private float _hitPadding;
    private KryptonRadialMenuAnimationStyle _animationStyle;
    private int _animationDuration;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRadialMenuValues"/> class.
    /// </summary>
    /// <param name="needPaint">Delegate for requesting paints.</param>
    public KryptonRadialMenuValues(NeedPaintHandler? needPaint)
    {
        NeedPaint = needPaint;
        _menuRadius = RadialMenuMetrics.DefaultMenuRadius;
        _innerRadius = RadialMenuMetrics.DefaultInnerRadius;
        _hubText = RadialMenuMetrics.DefaultHubText;
        _menuColor = Color.Empty;
        _subMenuHoverColor = Color.Empty;
        _displayStyle = KryptonRadialMenuDisplayStyle.ImageAboveText;
        _subMenuGlyph = RadialMenuMetrics.DefaultSubMenuGlyph;
        _outerRingThickness = RadialMenuMetrics.DefaultOuterRingThickness;
        _showOuterRingOnLeaves = true;
        _scale = RadialMenuMetrics.DefaultScale;
        _itemImageSize = RadialMenuMetrics.DefaultItemImageSize;
        _showShadow = true;
        _shadowOpacity = RadialMenuMetrics.DefaultShadowOpacity;
        _shadowBlur = RadialMenuMetrics.DefaultShadowBlur;
        _shadowOffset = RadialMenuMetrics.DefaultShadowOffset;
        _showCheckedGlyph = true;
        _startAngle = RadialMenuMetrics.DefaultStartAngle;
        _maxVisibleItems = 0;
        _hitPadding = RadialMenuMetrics.DefaultHitPadding;
        _animationStyle = KryptonRadialMenuAnimationStyle.Sweep;
        _animationDuration = RadialMenuMetrics.DefaultAnimationDurationMs;
    }

    /// <inheritdoc />
    public override bool IsDefault =>
        _menuRadius == RadialMenuMetrics.DefaultMenuRadius
        && _innerRadius == RadialMenuMetrics.DefaultInnerRadius
        && _glyph == null
        && _hubText == RadialMenuMetrics.DefaultHubText
        && _menuColor.IsEmpty
        && _subMenuHoverColor.IsEmpty
        && _displayStyle == KryptonRadialMenuDisplayStyle.ImageAboveText
        && _subMenuGlyph == RadialMenuMetrics.DefaultSubMenuGlyph
        && Math.Abs(_outerRingThickness - RadialMenuMetrics.DefaultOuterRingThickness) < 0.01f
        && _showOuterRingOnLeaves
        && Math.Abs(_scale - RadialMenuMetrics.DefaultScale) < 0.01f
        && _itemImageSize == RadialMenuMetrics.DefaultItemImageSize
        && _showShadow
        && Math.Abs(_shadowOpacity - RadialMenuMetrics.DefaultShadowOpacity) < 0.001f
        && _shadowBlur == RadialMenuMetrics.DefaultShadowBlur
        && _shadowOffset == RadialMenuMetrics.DefaultShadowOffset
        && _showCheckedGlyph
        && Math.Abs(_startAngle - RadialMenuMetrics.DefaultStartAngle) < 0.01f
        && _maxVisibleItems == 0
        && Math.Abs(_hitPadding - RadialMenuMetrics.DefaultHitPadding) < 0.01f
        && _animationStyle == KryptonRadialMenuAnimationStyle.Sweep
        && _animationDuration == RadialMenuMetrics.DefaultAnimationDurationMs;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the outer menu radius in 96-DPI logical pixels.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Outer radius of the radial menu in 96-DPI logical pixels.")]
    [DefaultValue(RadialMenuMetrics.DefaultMenuRadius)]
    public int MenuRadius
    {
        get => _menuRadius;
        set
        {
            value = Math.Max(RadialMenuMetrics.MinMenuRadius, value);
            if (_menuRadius != value)
            {
                _menuRadius = value;
                if (_innerRadius >= _menuRadius - RadialMenuMetrics.InnerAutoAdjustOuterGap)
                {
                    _innerRadius = Math.Max(
                        RadialMenuMetrics.MinInnerWhenAutoAdjust,
                        _menuRadius / RadialMenuMetrics.DefaultInnerRadiusDivisor);
                }

                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets the inner (center button) radius in 96-DPI logical pixels.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Inner radius of the center button in 96-DPI logical pixels.")]
    [DefaultValue(RadialMenuMetrics.DefaultInnerRadius)]
    public int InnerRadius
    {
        get => _innerRadius;
        set
        {
            value = Math.Max(
                RadialMenuMetrics.MinInnerRadius,
                Math.Min(_menuRadius - RadialMenuMetrics.MinInnerOuterGap, value));
            if (_innerRadius != value)
            {
                _innerRadius = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets the image shown on the centre button (and on the collapsed hub when set).
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Image displayed on the centre button and collapsed hub.")]
    [DefaultValue(null)]
    public Image? Glyph
    {
        get => _glyph;
        set
        {
            if (!ReferenceEquals(_glyph, value))
            {
                _glyph = value;
                PerformNeedPaint(false);
            }
        }
    }

    /// <summary>
    /// Gets or sets the text drawn on the collapsed hub when <see cref="Glyph"/> is null.
    /// </summary>
    /// <remarks>
    /// Used by <see cref="KryptonRadialMenuControl"/> hub mode. Default is <c>+</c>. Empty draws no caption.
    /// </remarks>
    [Category(@"Visuals")]
    [Description(@"Text on the collapsed hub when no Glyph image is set. Default is +.")]
    [DefaultValue(RadialMenuMetrics.DefaultHubText)]
    [Localizable(true)]
    public string HubText
    {
        get => _hubText;
        set
        {
            var text = value ?? string.Empty;
            if (_hubText != text)
            {
                _hubText = text;
                PerformNeedPaint(false);
            }
        }
    }

    /// <summary>
    /// Gets or sets the accent colour for the center button and submenu cues. Empty uses the palette.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Accent colour for the center button and submenu cues.")]
    public Color MenuColor
    {
        get => _menuColor;
        set
        {
            if (_menuColor != value)
            {
                _menuColor = value;
                PerformNeedPaint(false);
            }
        }
    }

    private bool ShouldSerializeMenuColor() => !_menuColor.IsEmpty;
    private void ResetMenuColor() => _menuColor = Color.Empty;

    /// <summary>
    /// Gets or sets the submenu hover accent colour. Empty uses the palette.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Hover accent colour for submenu sectors.")]
    public Color SubMenuHoverColor
    {
        get => _subMenuHoverColor;
        set
        {
            if (_subMenuHoverColor != value)
            {
                _subMenuHoverColor = value;
                PerformNeedPaint(false);
            }
        }
    }

    private bool ShouldSerializeSubMenuHoverColor() => !_subMenuHoverColor.IsEmpty;
    private void ResetSubMenuHoverColor() => _subMenuHoverColor = Color.Empty;

    /// <summary>
    /// Gets or sets how sector text and images are arranged.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"How sector text and images are arranged.")]
    [DefaultValue(KryptonRadialMenuDisplayStyle.ImageAboveText)]
    public KryptonRadialMenuDisplayStyle DisplayStyle
    {
        get => _displayStyle;
        set
        {
            if (_displayStyle != value)
            {
                _displayStyle = value;
                PerformNeedPaint(false);
            }
        }
    }

    /// <summary>
    /// Gets or sets the Unicode glyph drawn on the outer ring for items that open a sub-level or editor.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Unicode glyph drawn on the outer ring for submenu / editor items.")]
    [DefaultValue(RadialMenuMetrics.DefaultSubMenuGlyph)]
    [Localizable(true)]
    public string SubMenuGlyph
    {
        get => _subMenuGlyph;
        set
        {
            value ??= string.Empty;
            if (_subMenuGlyph != value)
            {
                _subMenuGlyph = value;
                PerformNeedPaint(false);
            }
        }
    }

    /// <summary>
    /// Gets or sets the thickness of the outer ring stroke (PanelAlternate). Zero hides the stroke.
    /// </summary>
    /// <remarks>
    /// Stored in 96-DPI logical pixels; painting and hit-testing multiply by device DPI and <see cref="Scale"/>.
    /// </remarks>
    [Category(@"Visuals")]
    [Description(@"Thickness of the outer ring stroke in 96-DPI logical pixels. Zero hides the stroke.")]
    [DefaultValue(RadialMenuMetrics.DefaultOuterRingThickness)]
    public float OuterRingThickness
    {
        get => _outerRingThickness;
        set
        {
            value = Math.Max(0f, Math.Min(RadialMenuMetrics.MaxOuterRingThickness, value));
            if (Math.Abs(_outerRingThickness - value) > 0.01f)
            {
                _outerRingThickness = value;
                PerformNeedPaint(false);
            }
        }
    }

    /// <summary>
    /// Gets or sets whether leaf sectors (no children / editor) draw an outer-ring arc.
    /// </summary>
    /// <remarks>
    /// When <c>false</c>, only sectors with a sub-level paint the outer arc; chevrons remain children-only.
    /// Leaf outer-band hits are treated as sector body clicks. Default is <c>true</c> (full ring).
    /// </remarks>
    [Category(@"Visuals")]
    [Description(@"When false, hides the outer ring arc on slices that have no sub-level.")]
    [DefaultValue(true)]
    public bool ShowOuterRingOnLeaves
    {
        get => _showOuterRingOnLeaves;
        set
        {
            if (_showOuterRingOnLeaves != value)
            {
                _showOuterRingOnLeaves = value;
                PerformNeedPaint(false);
            }
        }
    }

    /// <summary>
    /// Gets or sets a uniform scale factor applied on top of device DPI (1 = 100%).
    /// </summary>
    /// <remarks>
    /// Affects radii, ring thickness, item images, hit padding, and related metrics.
    /// <see cref="MenuRadius"/> and other size properties remain 96-DPI logical values.
    /// </remarks>
    [Category(@"Visuals")]
    [Description(@"Uniform scale factor (0.5–3). Multiplied with device DPI for layout and painting.")]
    [DefaultValue(RadialMenuMetrics.DefaultScale)]
    public float Scale
    {
        get => _scale;
        set
        {
            value = Math.Max(RadialMenuMetrics.MinScale, Math.Min(RadialMenuMetrics.MaxScale, value));
            if (Math.Abs(_scale - value) > 0.01f)
            {
                _scale = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets the size of images drawn in item sectors (96-DPI logical pixels).
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Size in 96-DPI logical pixels of images drawn in item sectors.")]
    [DefaultValue(RadialMenuMetrics.DefaultItemImageSize)]
    public int ItemImageSize
    {
        get => _itemImageSize;
        set
        {
            value = Math.Max(RadialMenuMetrics.MinItemImageSize, Math.Min(RadialMenuMetrics.MaxItemImageSize, value));
            if (_itemImageSize != value)
            {
                _itemImageSize = value;
                PerformNeedPaint(false);
            }
        }
    }

    /// <summary>
    /// Gets or sets whether a circular popup shadow is shown behind the menu.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Shows a circular shadow behind the radial popup.")]
    [DefaultValue(true)]
    public bool ShowShadow
    {
        get => _showShadow;
        set => _showShadow = value;
    }

    /// <summary>
    /// Gets or sets the opacity of the circular popup shadow (0..1).
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Opacity of the circular popup shadow when ShowShadow is enabled.")]
    [DefaultValue(RadialMenuMetrics.DefaultShadowOpacity)]
    public float ShadowOpacity
    {
        get => _shadowOpacity;
        set
        {
            value = Math.Max(0f, Math.Min(1f, value));
            if (Math.Abs(_shadowOpacity - value) > 0.001f)
            {
                _shadowOpacity = value;
                PerformNeedPaint(false);
            }
        }
    }

    /// <summary>
    /// Gets or sets how far the soft shadow halo extends beyond the menu edge, in 96-DPI logical pixels.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Soft shadow halo radius in 96-DPI logical pixels outside the menu edge.")]
    [DefaultValue(RadialMenuMetrics.DefaultShadowBlur)]
    public int ShadowBlur
    {
        get => _shadowBlur;
        set
        {
            value = Math.Max(0, Math.Min(RadialMenuMetrics.MaxShadowBlur, value));
            if (_shadowBlur != value)
            {
                _shadowBlur = value;
                PerformNeedPaint(false);
            }
        }
    }

    /// <summary>
    /// Gets or sets the shadow drop offset in 96-DPI logical pixels (down and right).
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Shadow drop offset in 96-DPI logical pixels (down and right).")]
    [DefaultValue(RadialMenuMetrics.DefaultShadowOffset)]
    public int ShadowOffset
    {
        get => _shadowOffset;
        set
        {
            value = Math.Max(0, Math.Min(RadialMenuMetrics.MaxShadowOffset, value));
            if (_shadowOffset != value)
            {
                _shadowOffset = value;
                PerformNeedPaint(false);
            }
        }
    }

    /// <summary>
    /// Gets or sets whether checked items draw a checkmark glyph on the sector.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Draws a checkmark glyph on checked sectors.")]
    [DefaultValue(true)]
    public bool ShowCheckedGlyph
    {
        get => _showCheckedGlyph;
        set
        {
            if (_showCheckedGlyph != value)
            {
                _showCheckedGlyph = value;
                PerformNeedPaint(false);
            }
        }
    }

    /// <summary>
    /// Gets or sets the start angle in degrees for the first sector (GDI+: 0 = east, -90 = north).
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Start angle in degrees for the first sector (-90 is top).")]
    [DefaultValue(RadialMenuMetrics.DefaultStartAngle)]
    public float StartAngle
    {
        get => _startAngle;
        set
        {
            if (Math.Abs(_startAngle - value) > 0.01f)
            {
                _startAngle = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets the maximum number of sectors shown per page. Zero means unlimited.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Maximum visible sectors per page. Zero means show all items.")]
    [DefaultValue(0)]
    public int MaxVisibleItems
    {
        get => _maxVisibleItems;
        set
        {
            value = Math.Max(0, Math.Min(RadialMenuMetrics.MaxVisibleItemsCap, value));
            if (_maxVisibleItems != value)
            {
                _maxVisibleItems = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets extra hit-test padding in 96-DPI logical pixels for touch-friendly sectors.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Extra hit-test padding in 96-DPI logical pixels around the annular hit region.")]
    [DefaultValue(RadialMenuMetrics.DefaultHitPadding)]
    public float HitPadding
    {
        get => _hitPadding;
        set
        {
            value = Math.Max(0f, Math.Min(RadialMenuMetrics.MaxHitPadding, value));
            if (Math.Abs(_hitPadding - value) > 0.01f)
            {
                _hitPadding = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the open / navigation animation style.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Animation used when the menu opens or navigates to another ring.")]
    [DefaultValue(KryptonRadialMenuAnimationStyle.Sweep)]
    public KryptonRadialMenuAnimationStyle AnimationStyle
    {
        get => _animationStyle;
        set => _animationStyle = value;
    }

    /// <summary>
    /// Gets or sets the animation duration in milliseconds.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Duration of the open / navigation animation in milliseconds.")]
    [DefaultValue(RadialMenuMetrics.DefaultAnimationDurationMs)]
    public int AnimationDuration
    {
        get => _animationDuration;
        set => _animationDuration = Math.Max(0, Math.Min(RadialMenuMetrics.MaxAnimationDurationMs, value));
    }

    #endregion
}
