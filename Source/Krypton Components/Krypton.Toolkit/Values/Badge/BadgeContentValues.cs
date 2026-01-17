#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for badge content related values.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class BadgeContentValues : Storage
{
    #region Static Fields

    private const string DEFAULT_BADGE_TEXT = @"";
    private const bool DEFAULT_AUTO_SHOW_HIDE_BADGE = false; 
    private const int DEFAULT_BADGE_DIAMETER = 0; // 0 means auto-size
    private const int DEFAULT_MAXIMUM_BADGE_VALUE = 99;
    private const int DEFAULT_MINIMUM_BADGE_VALUE = 0;
    private const int DEFAULT_BADGE_MARGIN = 5; // Default badge margin/offset from the edge
    private const int DEFAULT_BUTTON_IMAGE_PADDING = 4;

    #endregion

    #region Instance Fields

    private string? _text;
    private BadgeAnimation _animation;
    private BadgePosition _position;
    private BadgeShape _shape;
    private bool _visible;
    private Font? _font;
    private Image? _badgeImage;
    private int _badgeImagePadding;
    private int _badgeDiameter;
    private int _maxBadgeValue;
    private int _badgeMargin;
    private bool _autoShowHideBadge;

    #endregion

    #region Identity

    public BadgeContentValues(NeedPaintHandler needPaint)
    {
        NeedPaint = needPaint;

        Reset();
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets and sets the badge text.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The text to display on the badge.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(DEFAULT_BADGE_TEXT)]
    public string Text
    {
        get => _text ?? GlobalStaticValues.DEFAULT_EMPTY_STRING;
        set
        {
            if (_text != value)
            {
                _text = value;

                // Update visibility if auto-show/hide is enabled
                if (_autoShowHideBadge)
                {
                    UpdateVisibilityFromContent();
                }

                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeText() => Text != DEFAULT_BADGE_TEXT;

    /// <summary>
    /// Gets and sets the badge image.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The image to display on the badge. If set, the image will be displayed instead of text.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(null)]
    public Image? BadgeImage
    {
        get => _badgeImage;
        set
        {
            if (_badgeImage != value)
            {
                _badgeImage = value;

                // Update visibility if auto-show/hide is enabled
                if (_autoShowHideBadge)
                {
                    UpdateVisibilityFromContent();
                }

                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBadgeImage() => BadgeImage != null;

    /// <summary>
    /// Gets and sets the padding around the badge image.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The padding around the badge image.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(DEFAULT_BUTTON_IMAGE_PADDING)]
    public int BadgeImagePadding
    {
        get => _badgeImagePadding;
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            if (_badgeImagePadding != value)
            {
                _badgeImagePadding = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBadgeImagePadding() => BadgeImagePadding != DEFAULT_BUTTON_IMAGE_PADDING;

    /// <summary>
    /// Gets and sets the badge position on the button.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The position of the badge on the button.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(BadgePosition.TopRight)]
    public BadgePosition Position
    {
        get => _position;
        set
        {
            if (_position != value)
            {
                _position = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializePosition() => Position != BadgePosition.TopRight;

    /// <summary>
    /// Gets and sets whether the badge is visible.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Whether the badge is visible.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(false)]
    public bool Visible
    {
        get => _visible;
        set
        {
            // If AutoShowHideBadge is enabled, ignore manual changes to Visible
            // Visibility is automatically managed based on content
            if (_autoShowHideBadge)
            {
                return;
            }

            if (_visible != value)
            {
                _visible = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeVisible() => Visible != false;

    /// <summary>
    /// Gets and sets the badge text font.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The font used to display badge text. If null, uses default font (Segoe UI 7.5pt Bold).")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(null)]
    public Font? Font
    {
        get => _font;
        set
        {
            if (_font != value)
            {
                _font = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeFont() => Font != null;

    /// <summary>
    /// Gets and sets the badge shape.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The shape of the badge.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(BadgeShape.Circle)]
    public BadgeShape Shape
    {
        get => _shape;
        set
        {
            if (_shape != value)
            {
                _shape = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeShape() => Shape != BadgeShape.Circle;

    /// <summary>
    /// Gets and sets the badge animation type.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The animation type for the badge.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(BadgeAnimation.None)]
    public BadgeAnimation Animation
    {
        get => _animation;
        set
        {
            if (_animation != value)
            {
                _animation = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeAnimation() => Animation != BadgeAnimation.None;

    /// <summary>
    /// Gets and sets the badge size (diameter for circle, side length for square). 0 means auto-size based on content.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The size of the badge: diameter when Shape is Circle, side length when Shape is Square. 0 means auto-size based on content.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(DEFAULT_BADGE_DIAMETER)]
    public int BadgeDiameter
    {
        get => _badgeDiameter;
        set
        {
            if (value < 0)
            {
                value = 0;
            }

            if (_badgeDiameter != value)
            {
                _badgeDiameter = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBadgeDiameter() => BadgeDiameter != DEFAULT_BADGE_DIAMETER;

    /// <summary>
    /// Gets and sets the threshold number. If the badge text value (as a number) exceeds this value, OverflowText is displayed instead.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The threshold number. If the badge text value (as a number) exceeds this value, OverflowText is displayed instead. Set to 0 to disable overflow checking.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(DEFAULT_MAXIMUM_BADGE_VALUE)]
    public int MaximumBadgeValue
    {
        get => _maxBadgeValue;
        set
        {
            if (value < 0)
            {
                value = 0;
            }

            if (_maxBadgeValue != value)
            {
                _maxBadgeValue = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeMaxBadgeValue() => MaximumBadgeValue != DEFAULT_MAXIMUM_BADGE_VALUE;

    /// <summary>
    /// Gets and sets whether the badge should automatically show when it has content (text or image) and hide when empty.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"When enabled, the badge automatically shows when it has content (text or image) and hides when empty.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(false)]
    public bool AutoShowHideBadge
    {
        get => _autoShowHideBadge;
        set
        {
            if (_autoShowHideBadge != value)
            {
                _autoShowHideBadge = value;

                // If enabled, update visibility based on current content
                if (value)
                {
                    UpdateVisibilityFromContent();
                }

                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeAutoShowHideBadge() => AutoShowHideBadge != DEFAULT_AUTO_SHOW_HIDE_BADGE;

    /// <summary>
    /// Gets and sets the badge margin (offset) from the button edge for badge positioning.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The badge margin (offset in pixels) from the button edge for badge positioning.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(DEFAULT_BADGE_MARGIN)]
    public int BadgeMargin
    {
        get => _badgeMargin;

        set
        {
            if (value < 0)
            {
                value = 0;
            }

            if (_badgeMargin != value)
            {
                _badgeMargin = value;

                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBadgeMargin() => BadgeMargin != DEFAULT_BADGE_MARGIN;

    /// <summary>
    /// Resets the Margin property to its default value.
    /// </summary>
    public void ResetBadgeMargin() => BadgeMargin = DEFAULT_BADGE_MARGIN;

    #endregion

    #region Implementation

    /// <summary>
    /// Updates the Visible property based on whether the badge has content.
    /// </summary>
    private void UpdateVisibilityFromContent()
    {
        bool hasContent = !string.IsNullOrEmpty(Text) || BadgeImage != null;

        // Use the property setter to ensure proper notification
        if (_visible != hasContent)
        {
            _visible = hasContent;
            // Don't call PerformNeedPaint here to avoid recursion - it will be called by the property setter that triggered this update
        }
    }

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault => Text.Equals(DEFAULT_BADGE_TEXT) &&
                                      Position.Equals(BadgePosition.TopRight) &&
                                      Visible.Equals(false) &&
                                      Font == null &&
                                      Shape.Equals(BadgeShape.Circle) &&
                                      Animation.Equals(BadgeAnimation.None) &&
                                      BadgeDiameter.Equals(DEFAULT_BADGE_DIAMETER) &&
                                      MaximumBadgeValue.Equals(DEFAULT_MAXIMUM_BADGE_VALUE) &&
                                      AutoShowHideBadge.Equals(DEFAULT_AUTO_SHOW_HIDE_BADGE) &&
                                      BadgeImage == null &&
                                      BadgeImagePadding.Equals(DEFAULT_BUTTON_IMAGE_PADDING) &&
                                      BadgeMargin.Equals(DEFAULT_MAXIMUM_BADGE_VALUE);

    #endregion

    #region Reset

    public void Reset()
    {
        _text = DEFAULT_BADGE_TEXT;
        _position = BadgePosition.TopRight;
        _visible = false;
        _font = null;
        _shape = BadgeShape.Circle;
        _animation = BadgeAnimation.None;
        _badgeDiameter = DEFAULT_BADGE_DIAMETER;
        _maxBadgeValue = DEFAULT_MAXIMUM_BADGE_VALUE;
        _autoShowHideBadge = DEFAULT_AUTO_SHOW_HIDE_BADGE;
        _badgeImage = null;
        _badgeImagePadding = 4;
        _badgeMargin = DEFAULT_BADGE_MARGIN;
        PerformNeedPaint(true);
    }

    #endregion
}
