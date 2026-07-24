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
/// Expandable blink configuration for <see cref="KryptonBlinkingToolStripStatusLabel"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class BlinkingStatusLabelValues : Storage
{
    #region Constants

    private const int DEFAULT_BLINK_INTERVAL = 500;
    private const int DEFAULT_SOFT_BLINK_CYCLE_INTERVAL = 2000;
    private const int DEFAULT_SOFT_BLINK_TICK_INTERVAL = 30;
    private const int DEFAULT_BLINK_DURATION = 0;
    private const int DEFAULT_MAX_BLINK_COUNT = 0;

    #endregion

    #region Instance Fields

    private readonly KryptonBlinkingToolStripStatusLabel _owner;
    private bool _blinkEnabled;
    private int _blinkInterval = DEFAULT_BLINK_INTERVAL;
    private int _softBlinkCycleInterval = DEFAULT_SOFT_BLINK_CYCLE_INTERVAL;
    private int _softBlinkTickInterval = DEFAULT_SOFT_BLINK_TICK_INTERVAL;
    private int _blinkDuration = DEFAULT_BLINK_DURATION;
    private int _maxBlinkCount = DEFAULT_MAX_BLINK_COUNT;
    private BlinkMode _blinkMode = BlinkMode.Hard;
    private BlinkTarget _blinkTarget = BlinkTarget.Foreground;
    private VisibilityStyle _visibilityStyle = VisibilityStyle.HideText;
    private Color _blinkColorOne = Color.White;
    private Color _blinkColorTwo = Color.Black;
    private Color _blinkTextColor = Color.Red;
    private bool _useBlinkTextColor = true;
    private bool _pauseOnMouseOver;
    private bool _restoreAppearanceOnStop = true;
    private bool _blinkOnlyWhenVisible = true;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="BlinkingStatusLabelValues"/> class.
    /// </summary>
    /// <param name="owner">Owning blinking status label.</param>
    public BlinkingStatusLabelValues(KryptonBlinkingToolStripStatusLabel owner) =>
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        !_blinkEnabled &&
        _blinkInterval == DEFAULT_BLINK_INTERVAL &&
        _softBlinkCycleInterval == DEFAULT_SOFT_BLINK_CYCLE_INTERVAL &&
        _softBlinkTickInterval == DEFAULT_SOFT_BLINK_TICK_INTERVAL &&
        _blinkDuration == DEFAULT_BLINK_DURATION &&
        _maxBlinkCount == DEFAULT_MAX_BLINK_COUNT &&
        _blinkMode == BlinkMode.Hard &&
        _blinkTarget == BlinkTarget.Foreground &&
        _visibilityStyle == VisibilityStyle.HideText &&
        _blinkColorOne == Color.White &&
        _blinkColorTwo == Color.Black &&
        _blinkTextColor == Color.Red &&
        _useBlinkTextColor &&
        !_pauseOnMouseOver &&
        _restoreAppearanceOnStop &&
        _blinkOnlyWhenVisible;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets a value indicating whether blinking is enabled.
    /// Setting <c>true</c> starts blinking; setting <c>false</c> stops and optionally restores appearance.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description(@"When true, blinking runs. Setting false stops blinking.")]
    public bool BlinkEnabled
    {
        get => _blinkEnabled;
        set
        {
            if (_blinkEnabled == value)
            {
                return;
            }

            _blinkEnabled = value;

            if (value)
            {
                _owner.StartBlink();
            }
            else
            {
                _owner.StopBlink();
            }
        }
    }

    /// <summary>
    /// Gets or sets the hard-blink / visibility toggle interval in milliseconds.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_BLINK_INTERVAL)]
    [Description(@"Hard-blink toggle interval in milliseconds.")]
    public int BlinkInterval
    {
        get => _blinkInterval;
        set
        {
            if (value < 1)
            {
                value = 1;
            }

            if (_blinkInterval == value)
            {
                return;
            }

            _blinkInterval = value;
            _owner.OnBlinkTimerSettingsChanged();
        }
    }

    /// <summary>
    /// Gets or sets the soft-blink full cycle duration in milliseconds.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_SOFT_BLINK_CYCLE_INTERVAL)]
    [Description(@"Soft-blink full cycle interval in milliseconds.")]
    public int SoftBlinkCycleInterval
    {
        get => _softBlinkCycleInterval;
        set
        {
            if (value < 2)
            {
                value = 2;
            }

            _softBlinkCycleInterval = value;
        }
    }

    /// <summary>
    /// Gets or sets the soft-blink timer tick interval in milliseconds.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_SOFT_BLINK_TICK_INTERVAL)]
    [Description(@"Soft-blink timer tick interval in milliseconds.")]
    public int SoftBlinkTickInterval
    {
        get => _softBlinkTickInterval;
        set
        {
            if (value < 1)
            {
                value = 1;
            }

            if (_softBlinkTickInterval == value)
            {
                return;
            }

            _softBlinkTickInterval = value;
            _owner.OnBlinkTimerSettingsChanged();
        }
    }

    /// <summary>
    /// Gets or sets the auto-stop duration in milliseconds. Zero means continuous.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_BLINK_DURATION)]
    [Description(@"Auto-stop after this many milliseconds. Zero means continuous.")]
    public int BlinkDuration
    {
        get => _blinkDuration;
        set
        {
            if (value < 0)
            {
                value = 0;
            }

            _blinkDuration = value;
        }
    }

    /// <summary>
    /// Gets or sets the maximum number of hard/visibility toggles before auto-stop. Zero means unlimited.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_MAX_BLINK_COUNT)]
    [Description(@"Auto-stop after this many hard/visibility toggles. Zero means unlimited.")]
    public int MaxBlinkCount
    {
        get => _maxBlinkCount;
        set
        {
            if (value < 0)
            {
                value = 0;
            }

            _maxBlinkCount = value;
        }
    }

    /// <summary>
    /// Gets or sets the blink animation mode.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(BlinkMode.Hard)]
    [Description(@"Hard, Soft, or Visibility blink animation.")]
    public BlinkMode BlinkMode
    {
        get => _blinkMode;
        set
        {
            if (_blinkMode == value)
            {
                return;
            }

            _blinkMode = value;
            _owner.OnBlinkTimerSettingsChanged();
        }
    }

    /// <summary>
    /// Gets or sets which colour channels hard/soft blink animates.
    /// Ignored when <see cref="BlinkMode"/> is <see cref="BlinkMode.Visibility"/>.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(BlinkTarget.Foreground)]
    [Description(@"Which colour channels hard/soft blink animates.")]
    public BlinkTarget BlinkTarget
    {
        get => _blinkTarget;
        set => _blinkTarget = value;
    }

    /// <summary>
    /// Gets or sets how visibility blink hides content during the off phase.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(VisibilityStyle.HideText)]
    [Description(@"HideText uses transparent ForeColor; ToggleVisible toggles Visible.")]
    public VisibilityStyle VisibilityStyle
    {
        get => _visibilityStyle;
        set => _visibilityStyle = value;
    }

    /// <summary>
    /// Gets or sets the first blink colour endpoint.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(typeof(Color), "White")]
    [Description(@"First blink colour (hard alternate / soft endpoint).")]
    public Color BlinkColorOne
    {
        get => _blinkColorOne;
        set => _blinkColorOne = value;
    }

    /// <summary>
    /// Gets or sets the second blink colour endpoint.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(typeof(Color), "Black")]
    [Description(@"Second blink colour (hard alternate / soft endpoint).")]
    public Color BlinkColorTwo
    {
        get => _blinkColorTwo;
        set => _blinkColorTwo = value;
    }

    /// <summary>
    /// Gets or sets an optional fixed text colour applied while blinking when
    /// <see cref="UseBlinkTextColor"/> is true and the blink target does not drive foreground.
    /// Use <see cref="Color.Empty"/> to leave <c>ForeColor</c> alone in that case.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(typeof(Color), "Red")]
    [Description(@"Optional fixed text colour while blinking.")]
    public Color BlinkTextColor
    {
        get => _blinkTextColor;
        set => _blinkTextColor = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether <see cref="BlinkTextColor"/> is applied during Hard/Soft blink
    /// when the blink target does not already drive <c>ForeColor</c>.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(true)]
    [Description(@"Apply BlinkTextColor during Hard/Soft blink when Foreground is not animated.")]
    public bool UseBlinkTextColor
    {
        get => _useBlinkTextColor;
        set => _useBlinkTextColor = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the blink timer pauses while the item is selected/hovered.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description(@"Pause blink updates while the mouse is over the item.")]
    public bool PauseOnMouseOver
    {
        get => _pauseOnMouseOver;
        set => _pauseOnMouseOver = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether original ForeColor, BackColor, and Visible are restored when blinking stops.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    [Description(@"Restore saved ForeColor, BackColor, and Visible when blinking stops.")]
    public bool RestoreAppearanceOnStop
    {
        get => _restoreAppearanceOnStop;
        set => _restoreAppearanceOnStop = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether blink updates are skipped when the parent strip (and item, when applicable) is not visible.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    [Description(@"Skip blink updates when the parent strip or item is not visible.")]
    public bool BlinkOnlyWhenVisible
    {
        get => _blinkOnlyWhenVisible;
        set => _blinkOnlyWhenVisible = value;
    }

    /// <summary>
    /// Resets all blink values to their defaults.
    /// </summary>
    public void Reset()
    {
        if (_blinkEnabled)
        {
            BlinkEnabled = false;
        }

        _blinkInterval = DEFAULT_BLINK_INTERVAL;
        _softBlinkCycleInterval = DEFAULT_SOFT_BLINK_CYCLE_INTERVAL;
        _softBlinkTickInterval = DEFAULT_SOFT_BLINK_TICK_INTERVAL;
        _blinkDuration = DEFAULT_BLINK_DURATION;
        _maxBlinkCount = DEFAULT_MAX_BLINK_COUNT;
        _blinkMode = BlinkMode.Hard;
        _blinkTarget = BlinkTarget.Foreground;
        _visibilityStyle = VisibilityStyle.HideText;
        _blinkColorOne = Color.White;
        _blinkColorTwo = Color.Black;
        _blinkTextColor = Color.Red;
        _useBlinkTextColor = true;
        _pauseOnMouseOver = false;
        _restoreAppearanceOnStop = true;
        _blinkOnlyWhenVisible = true;
        _owner.OnBlinkTimerSettingsChanged();
    }

    #endregion

    #region Implementation

    internal void SetBlinkEnabledCore(bool value) => _blinkEnabled = value;

    #endregion
}
