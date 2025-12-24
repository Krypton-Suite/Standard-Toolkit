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
/// Storage for badge value information.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class BadgeValues : Storage
{
    #region Static Fields
    
    private const string DEFAULT_TEXT = "";
    private static readonly Color _defaultBadgeColor = Color.Red;
    private static readonly Color _defaultTextColor = Color.White;
    private static readonly Font? _defaultFont = new Font(KryptonManager.CurrentGlobalPalette.BaseFont.FontFamily, 7.5f, FontStyle.Bold, GraphicsUnit.Point);
    private static readonly Color _defaultBadgeBorderColor = Color.Empty;
    private const int DEFAULT_BADGE_BORDER_SIZE = 0;
    private const int DEFAULT_BADGE_DIAMETER = 0; // 0 means auto-size
    private const int DEFAULT_MAX_BADGE_VALUE = 99;
    private const string DEFAULT_OVERFLOW_TEXT = "+";
    private const bool DEFAULT_AUTO_SHOW_HIDE_BADGE = false;

    #endregion

    #region Instance Fields

    private string? _text;
    private Color _badgeColor;
    private Color _textColor;
    private BadgeAnimation _animation;
    private BadgePosition _position;
    private BadgeShape _shape;
    private bool _visible;
    private Font? _font;
    private Image? _badgeImage;
    private int _badgeImagePadding;
    private Color _badgeBorderColor;
    private int _badgeBorderSize;
    private int _badgeDiameter;
    private string _overflowText;
    private int _maxBadgeValue;
    private bool _autoShowHideBadge;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the BadgeValues class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public BadgeValues(NeedPaintHandler needPaint)
    {
        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        Reset();
    }

    #endregion

    #region IsDefault

    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (Text == DEFAULT_TEXT) &&
                                      (BadgeColor == _defaultBadgeColor) &&
                                      (TextColor == _defaultTextColor) &&
                                      (Position == BadgePosition.TopRight) &&
                                      (Visible == false) &&
                                      (BadgeImage == null) &&
                                      (BadgeImagePadding == 4) &&
                                      (Font == _defaultFont) &&
                                      (Shape == BadgeShape.Circle) &&
                                      (Animation == BadgeAnimation.None) &&
                                      (BadgeBorderColor == _defaultBadgeBorderColor) &&
                                      (BadgeBorderSize == DEFAULT_BADGE_BORDER_SIZE) &&
                                      (BadgeDiameter == DEFAULT_BADGE_DIAMETER) &&
                                      (OverflowText == DEFAULT_OVERFLOW_TEXT) &&
                                      (MaxBadgeValue == DEFAULT_MAX_BADGE_VALUE) &&
                                      (AutoShowHideBadge == DEFAULT_AUTO_SHOW_HIDE_BADGE);


    #endregion

    #region Text
    /// <summary>
    /// Gets and sets the badge text.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The text to display on the badge.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("")]
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

    private bool ShouldSerializeText() => Text != DEFAULT_TEXT;

    /// <summary>
    /// Resets the Text property to its default value.
    /// </summary>
    public void ResetText() => Text = DEFAULT_TEXT;
    #endregion

    #region BadgeImage

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

    #endregion

    #region BadgeImagePadding

    /// <summary>
    /// Gets and sets the padding around the badge image.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The padding around the badge image.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(4)]
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

    private bool ShouldSerializeBadgeImagePadding() => BadgeImagePadding != 4;

    #endregion

    #region BadgeColor
    /// <summary>
    /// Gets and sets the badge background color.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The background color of the badge.")]
    [RefreshProperties(RefreshProperties.All)]
    [KryptonDefaultColor]
    public Color BadgeColor
    {
        get => _badgeColor;
        set
        {
            if (_badgeColor != value)
            {
                _badgeColor = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBadgeColor() => BadgeColor != _defaultBadgeColor;

    /// <summary>
    /// Resets the BadgeColor property to its default value.
    /// </summary>
    public void ResetBadgeColor() => BadgeColor = _defaultBadgeColor;
    #endregion

    #region TextColor
    /// <summary>
    /// Gets and sets the badge text color.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The text color of the badge.")]
    [RefreshProperties(RefreshProperties.All)]
    [KryptonDefaultColor]
    public Color TextColor
    {
        get => _textColor;
        set
        {
            if (_textColor != value)
            {
                _textColor = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeTextColor() => TextColor != _defaultTextColor;

    /// <summary>
    /// Resets the TextColor property to its default value.
    /// </summary>
    public void ResetTextColor() => TextColor = _defaultTextColor;
    #endregion

    #region Position
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
    /// Resets the Position property to its default value.
    /// </summary>
    public void ResetPosition() => Position = BadgePosition.TopRight;
    #endregion

    #region Visible
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
    /// Resets the Visible property to its default value.
    /// </summary>
    public void ResetVisible() => Visible = false;
    #endregion

    #region Font
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
    /// Resets the Font property to its default value.
    /// </summary>
    public void ResetFont() => Font = null;
    #endregion

    #region Shape
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
    /// Resets the Shape property to its default value.
    /// </summary>
    public void ResetShape() => Shape = BadgeShape.Circle;
    #endregion

    #region Animation
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
    /// Resets the Animation property to its default value.
    /// </summary>
    public void ResetAnimation() => Animation = BadgeAnimation.None;
    #endregion

    #region BadgeBorderColor
    /// <summary>
    /// Gets and sets the badge border color.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The border color of the badge. Empty means no border.")]
    [RefreshProperties(RefreshProperties.All)]
    [KryptonDefaultColor]
    public Color BadgeBorderColor
    {
        get => _badgeBorderColor;
        set
        {
            if (_badgeBorderColor != value)
            {
                _badgeBorderColor = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBadgeBorderColor() => BadgeBorderColor != _defaultBadgeBorderColor;

    /// <summary>
    /// Resets the BadgeBorderColor property to its default value.
    /// </summary>
    public void ResetBadgeBorderColor() => BadgeBorderColor = _defaultBadgeBorderColor;
    #endregion

    #region BadgeBorderSize
    /// <summary>
    /// Gets and sets the badge border size (thickness in pixels).
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The border size (thickness in pixels) of the badge. 0 means no border.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(0)]
    public int BadgeBorderSize
    {
        get => _badgeBorderSize;
        set
        {
            if (value < 0)
            {
                value = 0;
            }

            if (_badgeBorderSize != value)
            {
                _badgeBorderSize = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBadgeBorderSize() => BadgeBorderSize != DEFAULT_BADGE_BORDER_SIZE;

    /// <summary>
    /// Resets the BadgeBorderSize property to its default value.
    /// </summary>
    public void ResetBadgeBorderSize() => BadgeBorderSize = DEFAULT_BADGE_BORDER_SIZE;
    #endregion

    #region BadgeDiameter
    /// <summary>
    /// Gets and sets the badge diameter (for circle shape only). 0 means auto-size based on content.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The diameter of the badge when Shape is Circle. 0 means auto-size based on content.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(0)]
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
    /// Resets the BadgeDiameter property to its default value.
    /// </summary>
    public void ResetBadgeDiameter() => BadgeDiameter = DEFAULT_BADGE_DIAMETER;
    #endregion

    #region OverflowText
    /// <summary>
    /// Gets and sets the text to display when the badge value exceeds OverflowNumber.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The text to display when the badge numeric value exceeds OverflowNumber (e.g., '99+').")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("99+")]
    public string OverflowText
    {
        get => _overflowText ?? GlobalStaticValues.DEFAULT_EMPTY_STRING;
        set
        {
            if (_overflowText != value)
            {
                _overflowText = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeOverflowText() => OverflowText != DEFAULT_OVERFLOW_TEXT;

    /// <summary>
    /// Resets the OverflowText property to its default value.
    /// </summary>
    public void ResetOverflowText() => OverflowText = DEFAULT_OVERFLOW_TEXT;
    #endregion

    #region OverflowNumber
    /// <summary>
    /// Gets and sets the threshold number. If the badge text value (as a number) exceeds this value, OverflowText is displayed instead.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The threshold number. If the badge text value (as a number) exceeds this value, OverflowText is displayed instead. Set to 0 to disable overflow checking.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(99)]
    public int MaxBadgeValue
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

    private bool ShouldSerializeMaxBadgeValue() => MaxBadgeValue != DEFAULT_MAX_BADGE_VALUE;

    #endregion

    #region AutoShowHideBadge
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
    /// Resets the AutoShowHideBadge property to its default value.
    /// </summary>
    public void ResetAutoShowHideBadge() => AutoShowHideBadge = DEFAULT_AUTO_SHOW_HIDE_BADGE;

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

    #region Reset

    public void Reset()
    {
        // Set initial values
        _text = DEFAULT_TEXT;
        _badgeColor = _defaultBadgeColor;
        _textColor = _defaultTextColor;
        _position = BadgePosition.TopRight;
        _visible = false;
        _badgeImage = null;
        _badgeImagePadding = 4;
        _font = _defaultFont;
        _shape = BadgeShape.Circle;
        _animation = BadgeAnimation.None;
        _badgeBorderColor = _defaultBadgeBorderColor;
        _badgeBorderSize = DEFAULT_BADGE_BORDER_SIZE;
        _badgeDiameter = DEFAULT_BADGE_DIAMETER;
        _overflowText = DEFAULT_OVERFLOW_TEXT;
        _maxBadgeValue = DEFAULT_MAX_BADGE_VALUE;
        _autoShowHideBadge = DEFAULT_AUTO_SHOW_HIDE_BADGE;
    }

    #endregion
}