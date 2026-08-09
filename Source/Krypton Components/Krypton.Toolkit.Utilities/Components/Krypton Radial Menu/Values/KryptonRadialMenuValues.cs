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
    private Color _menuColor;
    private Color _subMenuHoverColor;
    private KryptonRadialMenuDisplayStyle _displayStyle;
    private string _subMenuGlyph;
    private float _outerRingThickness;
    private int _itemImageSize;
    private bool _showShadow;
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
        _menuRadius = 140;
        _innerRadius = 42;
        _menuColor = Color.Empty;
        _subMenuHoverColor = Color.Empty;
        _displayStyle = KryptonRadialMenuDisplayStyle.ImageAboveText;
        _subMenuGlyph = @"›";
        _outerRingThickness = 4f;
        _itemImageSize = 24;
        _showShadow = true;
        _showCheckedGlyph = true;
        _startAngle = -90f;
        _maxVisibleItems = 0;
        _hitPadding = 4f;
        _animationStyle = KryptonRadialMenuAnimationStyle.Sweep;
        _animationDuration = 220;
    }

    /// <inheritdoc />
    public override bool IsDefault =>
        _menuRadius == 140
        && _innerRadius == 42
        && _glyph == null
        && _menuColor.IsEmpty
        && _subMenuHoverColor.IsEmpty
        && _displayStyle == KryptonRadialMenuDisplayStyle.ImageAboveText
        && _subMenuGlyph == @"›"
        && Math.Abs(_outerRingThickness - 4f) < 0.01f
        && _itemImageSize == 24
        && _showShadow
        && _showCheckedGlyph
        && Math.Abs(_startAngle + 90f) < 0.01f
        && _maxVisibleItems == 0
        && Math.Abs(_hitPadding - 4f) < 0.01f
        && _animationStyle == KryptonRadialMenuAnimationStyle.Sweep
        && _animationDuration == 220;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the outer menu radius in pixels.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Outer radius of the radial menu in pixels.")]
    [DefaultValue(140)]
    public int MenuRadius
    {
        get => _menuRadius;
        set
        {
            value = Math.Max(60, value);
            if (_menuRadius != value)
            {
                _menuRadius = value;
                if (_innerRadius >= _menuRadius - 20)
                {
                    _innerRadius = Math.Max(20, _menuRadius / 3);
                }

                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets the inner (center button) radius in pixels.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Inner radius of the center button in pixels.")]
    [DefaultValue(42)]
    public int InnerRadius
    {
        get => _innerRadius;
        set
        {
            value = Math.Max(16, Math.Min(_menuRadius - 24, value));
            if (_innerRadius != value)
            {
                _innerRadius = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets the image shown on the center button at the root level.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Image displayed on the center button.")]
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
    private void ResetMenuColor() => MenuColor = Color.Empty;

    /// <summary>
    /// Gets or sets the hover accent for sectors that open a submenu. Empty uses the palette.
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
    private void ResetSubMenuHoverColor() => SubMenuHoverColor = Color.Empty;

    /// <summary>
    /// Gets or sets how sector content is arranged.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"How text and images are arranged in sectors.")]
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
    [DefaultValue(@"›")]
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
    /// Gets or sets the thickness of the outer ring stroke (PanelAlternate).
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Thickness of the outer ring stroke in pixels.")]
    [DefaultValue(4f)]
    public float OuterRingThickness
    {
        get => _outerRingThickness;
        set
        {
            value = Math.Max(1f, Math.Min(16f, value));
            if (Math.Abs(_outerRingThickness - value) > 0.01f)
            {
                _outerRingThickness = value;
                PerformNeedPaint(false);
            }
        }
    }

    /// <summary>
    /// Gets or sets the size of images drawn in item sectors.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Size in pixels of images drawn in item sectors.")]
    [DefaultValue(24)]
    public int ItemImageSize
    {
        get => _itemImageSize;
        set
        {
            value = Math.Max(8, Math.Min(64, value));
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
        set
        {
            if (_showShadow != value)
            {
                _showShadow = value;
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
    [DefaultValue(-90f)]
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
            value = Math.Max(0, Math.Min(64, value));
            if (_maxVisibleItems != value)
            {
                _maxVisibleItems = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets extra hit-test padding in pixels for touch-friendly sectors.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Extra hit-test padding in pixels around the annular hit region.")]
    [DefaultValue(4f)]
    public float HitPadding
    {
        get => _hitPadding;
        set
        {
            value = Math.Max(0f, Math.Min(24f, value));
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
        set
        {
            if (_animationStyle != value)
            {
                _animationStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the animation duration in milliseconds.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Duration of the open / navigation animation in milliseconds.")]
    [DefaultValue(220)]
    public int AnimationDuration
    {
        get => _animationDuration;
        set
        {
            value = Math.Max(0, Math.Min(2000, value));
            if (_animationDuration != value)
            {
                _animationDuration = value;
            }
        }
    }

    #endregion
}
