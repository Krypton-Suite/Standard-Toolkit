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
/// Storage for badge color values.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class BadgeColorValues : Storage
{
    #region Static Fields

    private static readonly Color _defaultBadgeColor = Color.Red;
    private static readonly Color _defaultTextColor = Color.White;
    private static readonly Color _defaultBadgeBorderColor = Color.Empty;

    #endregion

    #region Instance Fields

    private Color _badgeColor;
    private Color _textColor;
    private Color _badgeBorderColor;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the BadgeColorValues class with the specified paint notification handler.
    /// </summary>
    /// <param name="needPaint">A delegate that is invoked when a property value changes and a repaint is required. Can be null if no notification is needed.</param>
    public BadgeColorValues(NeedPaintHandler? needPaint)
    {
        NeedPaint = needPaint;

        Reset();
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets and sets the badge background color.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The background color of the badge.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(typeof(Color), "Red")]
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
    /// Gets and sets the badge text color.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The text color of the badge.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(typeof(Color), "White")]
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

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault => BadgeColor.Equals(_defaultBadgeColor) &&
                                      TextColor.Equals(_defaultTextColor) &&
                                      BadgeBorderColor.Equals(_defaultBadgeBorderColor);

    #endregion

    #region Reset

    /// <summary>
    /// Resets the badge color to its default value.
    /// </summary>
    public void Reset()
    {
        _badgeColor = _defaultBadgeColor;
        _textColor = _defaultTextColor;
        _badgeBorderColor = _defaultBadgeBorderColor;
        PerformNeedPaint(true);
    }

    #endregion
}
