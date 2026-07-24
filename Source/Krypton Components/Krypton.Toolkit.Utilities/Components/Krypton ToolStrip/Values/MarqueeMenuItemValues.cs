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
/// Expandable marquee scrolling configuration for <see cref="KryptonToolStripMarqueeMenuItem"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class MarqueeMenuItemValues : Storage
{
    #region Constants

    private const MarqueeScrollDirection DEFAULT_MARQUEE_SCROLL_DIRECTION = MarqueeScrollDirection.RightToLeft;
    private const int DEFAULT_MINIMUM_TEXT_WIDTH = -1;
    private const int DEFAULT_REFRESH_INTERVAL = 30;
    private const int DEFAULT_SCROLL_STEP = 1;
    private const bool DEFAULT_STOP_SCROLL_ON_MOUSE_OVER = true;

    #endregion

    #region Instance Fields

    private readonly KryptonToolStripMarqueeMenuItem _owner;
    private MarqueeScrollDirection _marqueeScrollDirection = DEFAULT_MARQUEE_SCROLL_DIRECTION;
    private int _minimumTextWidth = DEFAULT_MINIMUM_TEXT_WIDTH;
    private int _scrollStep = DEFAULT_SCROLL_STEP;
    private bool _stopScrollOnMouseOver = DEFAULT_STOP_SCROLL_ON_MOUSE_OVER;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="MarqueeMenuItemValues"/> class.
    /// </summary>
    /// <param name="owner">Owning marquee menu item.</param>
    public MarqueeMenuItemValues(KryptonToolStripMarqueeMenuItem owner) =>
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        _marqueeScrollDirection == DEFAULT_MARQUEE_SCROLL_DIRECTION &&
        _minimumTextWidth == DEFAULT_MINIMUM_TEXT_WIDTH &&
        _owner.TimerInterval == DEFAULT_REFRESH_INTERVAL &&
        _scrollStep == DEFAULT_SCROLL_STEP &&
        _stopScrollOnMouseOver == DEFAULT_STOP_SCROLL_ON_MOUSE_OVER;

    #endregion

    #region Public

    /// <summary>
    /// Determines if text is scrolled left-to-right or right-to-left.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_MARQUEE_SCROLL_DIRECTION)]
    [Description(@"Determines if text is scrolled left-to-right or right-to-left.")]
    public MarqueeScrollDirection MarqueeScrollDirection
    {
        get => _marqueeScrollDirection;
        set => _marqueeScrollDirection = value;
    }

    /// <summary>
    /// Value less or equal zero indicates that text area width is defined by the width of the <c>Text</c> string.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_MINIMUM_TEXT_WIDTH)]
    [Description(@"Value less or equal zero indicates that text area width is defined by with of Text string.")]
    public int MinimumTextWidth
    {
        get => _minimumTextWidth;
        set
        {
            _minimumTextWidth = value;
            _owner.MeasureText();
        }
    }

    /// <summary>
    /// Determines how often new text position is recalculated, in milliseconds.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_REFRESH_INTERVAL)]
    [Description(@"Interval in milliseconds when new position is calculated and refreshed.")]
    public int RefreshInterval
    {
        get => _owner.TimerInterval;
        set => _owner.TimerInterval = value;
    }

    /// <summary>
    /// Determines how many pixels the text shifts when position is recalculated.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_SCROLL_STEP)]
    [Description(@"Number of pixels text position shifts when new position is calculated and refreshed")]
    public int ScrollStep
    {
        get => _scrollStep;
        set => _scrollStep = value;
    }

    /// <summary>
    /// When set to <c>true</c>, scrolling stops whenever the mouse pointer moves over the item.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_STOP_SCROLL_ON_MOUSE_OVER)]
    [Description(@" When sets to 'true', every time mouse pointer moves over scrolling stops.  Otherwise scrolling never stops.")]
    public bool StopScrollOnMouseOver
    {
        get => _stopScrollOnMouseOver;
        set => _stopScrollOnMouseOver = value;
    }

    /// <summary>
    /// Resets all values to their defaults.
    /// </summary>
    public void Reset()
    {
        _marqueeScrollDirection = DEFAULT_MARQUEE_SCROLL_DIRECTION;
        _minimumTextWidth = DEFAULT_MINIMUM_TEXT_WIDTH;
        _owner.TimerInterval = DEFAULT_REFRESH_INTERVAL;
        _scrollStep = DEFAULT_SCROLL_STEP;
        _stopScrollOnMouseOver = DEFAULT_STOP_SCROLL_ON_MOUSE_OVER;
        _owner.MeasureText();
    }

    #endregion
}
