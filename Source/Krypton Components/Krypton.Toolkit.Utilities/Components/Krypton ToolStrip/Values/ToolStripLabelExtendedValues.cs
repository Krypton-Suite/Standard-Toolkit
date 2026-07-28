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
/// Expandable alert/blink/fade/gradient configuration for <see cref="KryptonToolStripLabelExtended"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ToolStripLabelExtendedValues : Storage
{
    #region Constants

    private const int DEFAULT_TEXT_GLOW_SPREAD = 5;
    private const int DEFAULT_ALERT_BLINK_INTERVAL = 256;
    private const int DEFAULT_FADE_INTERVAL = 10;
    private const long DEFAULT_BLINK_DURATION = 10;
    private const short DEFAULT_CYCLE_INTERVAL = 2000;

    #endregion

    #region Instance Fields

    private readonly KryptonToolStripLabelExtended _owner;
    private bool _alert;
    private bool _enableBlinking = true;
    private bool _bkClr;
    private bool _enableFadeAnimation;
    private Color _alertColorOne = Color.White;
    private Color _alertColorTwo = Color.Black;
    private Color _alertTextColor = Color.Red;
    private Color _gradientColorOne = Color.Empty;
    private Color _gradientColorTwo = Color.Empty;
    private Color _textGlow = Color.White;
    private LinearGradientMode _gradientMode = LinearGradientMode.ForwardDiagonal;
    private int _textGlowSpread = DEFAULT_TEXT_GLOW_SPREAD;
    private int _alertBlinkInterval = DEFAULT_ALERT_BLINK_INTERVAL;
    private int _fadeInterval = DEFAULT_FADE_INTERVAL;
    private long _blinkDuration = DEFAULT_BLINK_DURATION;
    private BlinkState _blinkState = BlinkState.NormalBlink;
    private short _cycleInterval = DEFAULT_CYCLE_INTERVAL;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="ToolStripLabelExtendedValues"/> class.
    /// </summary>
    /// <param name="owner">Owning extended label.</param>
    public ToolStripLabelExtendedValues(KryptonToolStripLabelExtended owner) =>
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        !_alert &&
        _enableBlinking &&
        !_bkClr &&
        !_enableFadeAnimation &&
        _alertColorOne == Color.White &&
        _alertColorTwo == Color.Black &&
        _alertTextColor == Color.Red &&
        _gradientColorOne == Color.Empty &&
        _gradientColorTwo == Color.Empty &&
        _textGlow == Color.White &&
        _gradientMode == LinearGradientMode.ForwardDiagonal &&
        _textGlowSpread == DEFAULT_TEXT_GLOW_SPREAD &&
        _alertBlinkInterval == DEFAULT_ALERT_BLINK_INTERVAL &&
        _fadeInterval == DEFAULT_FADE_INTERVAL &&
        _blinkDuration == DEFAULT_BLINK_DURATION &&
        _blinkState == BlinkState.NormalBlink &&
        _cycleInterval == DEFAULT_CYCLE_INTERVAL;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets a value indicating whether this label is alert.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(false)]
    [Description(@"Alerts the user.")]
    public bool Alert
    {
        get => _alert;
        set => _alert = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether blinking is enabled.
    /// </summary>
    [Category(@"Blinking Settings")]
    [DefaultValue(true)]
    [Description(@"Enables a blinking mode.")]
    public bool EnableBlinking
    {
        get => _enableBlinking;
        set => _enableBlinking = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether soft blink uses the background colour path.
    /// </summary>
    [Category(@"Blinking Settings")]
    [DefaultValue(false)]
    [Description(@"When true, soft blink uses the background colour path.")]
    public bool BkClr
    {
        get => _bkClr;
        set => _bkClr = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the fade text animation is enabled.
    /// </summary>
    [Category(@"Fade Settings")]
    [DefaultValue(false)]
    [Description(@"Enables a fade text animation.")]
    public bool EnableFadeAnimation
    {
        get => _enableFadeAnimation;
        set => _enableFadeAnimation = value;
    }

    /// <summary>
    /// Gets or sets the first alert color.
    /// </summary>
    [Category(@"Blinking Settings")]
    [DefaultValue(typeof(Color), "White")]
    [Description(@"Defined alert first color.")]
    public Color AlertColorOne
    {
        get => _alertColorOne;
        set
        {
            _alertColorOne = value; 
            _owner.Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the second alert color.
    /// </summary>
    [Category(@"Blinking Settings")]
    [DefaultValue(typeof(Color), "Black")]
    [Description(@"Defined alert second color.")]
    public Color AlertColorTwo
    {
        get => _alertColorTwo;
        set
        {
            _alertColorTwo = value;
            _owner.Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the alert text color.
    /// </summary>
    [Category(@"Blinking Settings")]
    [DefaultValue(typeof(Color), "Red")]
    [Description(@"Defined alert text color.")]
    public Color AlertTextColor
    {
        get => _alertTextColor;
        set
        {
            _alertTextColor = value;
            _owner.Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the first gradient color.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(typeof(Color), "")]
    [Description(@"The first gradient color.")]
    public Color GradientColorOne
    {
        get => _gradientColorOne;
        set
        {
            _gradientColorOne = value;
            _owner.Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the second gradient color.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(typeof(Color), "")]
    [Description(@"The second gradient color.")]
    public Color GradientColorTwo
    {
        get => _gradientColorTwo;
        set
        {
            _gradientColorTwo = value;
            _owner.Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the text glow color.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(typeof(Color), "White")]
    [Description(@"The text glow color.")]
    public Color TextGlow
    {
        get => _textGlow;
        set { _textGlow = value; _owner.Invalidate(); }
    }

    /// <summary>
    /// Gets or sets the gradient mode.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(typeof(LinearGradientMode), "ForwardDiagonal")]
    [Description(@"Gradient mode.")]
    public LinearGradientMode GradientMode
    {
        get => _gradientMode;
        set
        {
            _gradientMode = value;
            _owner.Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the text glow spread.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(DEFAULT_TEXT_GLOW_SPREAD)]
    [Description(@"The text glow spread.")]
    public int TextGlowSpread
    {
        get => _textGlowSpread;
        set => _textGlowSpread = value;
    }

    /// <summary>
    /// Gets or sets the alert blink interval.
    /// </summary>
    [Category(@"Blinking Settings")]
    [DefaultValue(DEFAULT_ALERT_BLINK_INTERVAL)]
    [Description(@"The blink interval.")]
    public int AlertBlinkInterval
    {
        get => _alertBlinkInterval;
        set => _alertBlinkInterval = value;
    }

    /// <summary>
    /// Gets or sets the fade timer interval.
    /// </summary>
    [Category(@"Fade Settings")]
    [DefaultValue(DEFAULT_FADE_INTERVAL)]
    [Description(@"The fade timer interval.")]
    public int FadeInterval
    {
        get => _fadeInterval;
        set => _fadeInterval = value;
    }

    /// <summary>
    /// Gets or sets how long the blinking lasts for.
    /// </summary>
    [Category(@"Blinking Settings")]
    [DefaultValue(DEFAULT_BLINK_DURATION)]
    [Description(@"Defines how long the blinking lasts for.")]
    public long BlinkDuration
    {
        get => _blinkDuration;
        set => _blinkDuration = value;
    }

    /// <summary>
    /// Gets or sets the blink animation style.
    /// </summary>
    [Category(@"Blinking Settings")]
    [DefaultValue(typeof(BlinkState), nameof(BlinkState.NormalBlink))]
    [Description(@"The blink animation style.")]
    public BlinkState BlinkState
    {
        get => _blinkState;
        set => _blinkState = value;
    }

    /// <summary>
    /// Gets or sets the blink cycle interval in milliseconds.
    /// </summary>
    [Category(@"Blinking Settings")]
    [DefaultValue((short)2000)]
    [Description(@"The blink cycle interval in milliseconds.")]
    public short CycleInterval
    {
        get => _cycleInterval;
        set => _cycleInterval = value;
    }

    /// <summary>
    /// Resets all values to their defaults.
    /// </summary>
    public void Reset()
    {
        _alert = false;
        _enableBlinking = true;
        _bkClr = false;
        _enableFadeAnimation = false;
        _alertColorOne = Color.White;
        _alertColorTwo = Color.Black;
        _alertTextColor = Color.Red;
        _gradientColorOne = Color.Empty;
        _gradientColorTwo = Color.Empty;
        _textGlow = Color.White;
        _gradientMode = LinearGradientMode.ForwardDiagonal;
        _textGlowSpread = DEFAULT_TEXT_GLOW_SPREAD;
        _alertBlinkInterval = DEFAULT_ALERT_BLINK_INTERVAL;
        _fadeInterval = DEFAULT_FADE_INTERVAL;
        _blinkDuration = DEFAULT_BLINK_DURATION;
        _blinkState = BlinkState.NormalBlink;
        _cycleInterval = DEFAULT_CYCLE_INTERVAL;
        _owner.Invalidate();
    }

    #endregion
}
