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
/// A <see cref="ToolStripStatusLabel"/> that can blink using a WinForms <see cref="System.Windows.Forms.Timer"/>.
/// Supports hard colour toggling, soft colour interpolation, and visibility-style hiding.
/// Blink options are grouped under <see cref="BlinkValues"/> for the PropertyGrid.
/// </summary>
[ToolboxBitmap(typeof(ToolStripStatusLabel))]
[ToolboxItem(false)]
[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.StatusStrip | ToolStripItemDesignerAvailability.ToolStrip)]
public class KryptonBlinkingToolStripStatusLabel : ToolStripStatusLabel
{
    #region Instance Fields

    private readonly BlinkingStatusLabelValues _blinkValues;
    private System.Windows.Forms.Timer? _timer;
    private bool _isBlinking;
    private bool _hardPhase;
    private int _blinkCount;
    private int _sessionDurationMs;
    private long _blinkStartTick;
    private bool _appearanceCaptured;
    private Color _savedForeColor;
    private Color _savedBackColor;
    private bool _savedVisible;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when a blink session starts.
    /// </summary>
    [Category("Action")]
    [Description("Occurs when a blink session starts.")]
    public event EventHandler? BlinkStarted;

    /// <summary>
    /// Occurs when a blink session stops.
    /// </summary>
    [Category("Action")]
    [Description("Occurs when a blink session stops.")]
    public event EventHandler? BlinkStopped;

    /// <summary>
    /// Occurs on each blink timer tick while a session is active.
    /// </summary>
    [Category("Action")]
    [Description("Occurs on each blink timer tick while a session is active.")]
    public event EventHandler? BlinkTick;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonBlinkingToolStripStatusLabel"/> class.
    /// </summary>
    public KryptonBlinkingToolStripStatusLabel()
        : this(string.Empty)
    {
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonBlinkingToolStripStatusLabel"/> class.
    /// </summary>
    /// <param name="text">The text to display on the label.</param>
    public KryptonBlinkingToolStripStatusLabel(string text)
        : base(text)
    {
        _blinkValues = new BlinkingStatusLabelValues(this);
        _timer = new System.Windows.Forms.Timer();
        _timer.Tick += OnBlinkTimerTick;
        ConfigureTimerInterval();
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonBlinkingToolStripStatusLabel"/> class.
    /// </summary>
    /// <param name="text">The text to display on the label.</param>
    /// <param name="image">The image to display on the label.</param>
    public KryptonBlinkingToolStripStatusLabel(string text, Image? image)
        : base(text, image)
    {
        _blinkValues = new BlinkingStatusLabelValues(this);
        _timer = new System.Windows.Forms.Timer();
        _timer.Tick += OnBlinkTimerTick;
        ConfigureTimerInterval();
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the expandable blink configuration values for designer and runtime use.
    /// </summary>
    [Category("Behavior")]
    [Description("Blink timing, mode, colour, and behaviour settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public BlinkingStatusLabelValues BlinkValues => _blinkValues;

    private bool ShouldSerializeBlinkValues() => !_blinkValues.IsDefault;

    private void ResetBlinkValues() => _blinkValues.Reset();

    /// <summary>
    /// Gets or sets a value indicating whether blinking is enabled.
    /// Shortcut for <see cref="BlinkingStatusLabelValues.BlinkEnabled"/>.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool BlinkEnabled
    {
        get => _blinkValues.BlinkEnabled;
        set => _blinkValues.BlinkEnabled = value;
    }

    /// <summary>
    /// Gets a value indicating whether a blink session is currently active.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsBlinking => _isBlinking;

    /// <summary>
    /// Starts blinking using the current <see cref="BlinkingStatusLabelValues.BlinkDuration"/> setting.
    /// </summary>
    public void StartBlink()
    {
        StartBlinkCore(_blinkValues.BlinkDuration);
    }

    /// <summary>
    /// Starts blinking for the specified duration in milliseconds. Zero means continuous for this session.
    /// </summary>
    /// <param name="durationMs">Session duration in milliseconds; zero is continuous.</param>
    public void StartBlink(int durationMs)
    {
        if (durationMs < 0)
        {
            durationMs = 0;
        }

        StartBlinkCore(durationMs);
    }

    /// <summary>
    /// Stops blinking and optionally restores the appearance captured at session start.
    /// </summary>
    public void StopBlink()
    {
        if (!_isBlinking && _timer is not { Enabled: true })
        {
            _blinkValues.SetBlinkEnabledCore(false);
            return;
        }

        _timer?.Stop();

        _isBlinking = false;
        _blinkValues.SetBlinkEnabledCore(false);

        if (_blinkValues.RestoreAppearanceOnStop && _appearanceCaptured)
        {
            ForeColor = _savedForeColor;
            BackColor = _savedBackColor;
            Visible = _savedVisible;
        }

        _appearanceCaptured = false;
        OnBlinkStopped(EventArgs.Empty);
    }

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Tick -= OnBlinkTimerTick;
                _timer.Dispose();
                _timer = null;
            }
        }

        base.Dispose(disposing);
    }

    /// <summary>
    /// Raises the <see cref="BlinkStarted"/> event.
    /// </summary>
    /// <param name="e">Event arguments.</param>
    protected virtual void OnBlinkStarted(EventArgs e) => BlinkStarted?.Invoke(this, e);

    /// <summary>
    /// Raises the <see cref="BlinkStopped"/> event.
    /// </summary>
    /// <param name="e">Event arguments.</param>
    protected virtual void OnBlinkStopped(EventArgs e) => BlinkStopped?.Invoke(this, e);

    /// <summary>
    /// Raises the <see cref="BlinkTick"/> event.
    /// </summary>
    /// <param name="e">Event arguments.</param>
    protected virtual void OnBlinkTick(EventArgs e) => BlinkTick?.Invoke(this, e);

    #endregion

    #region Implementation

    internal void OnBlinkTimerSettingsChanged() => ConfigureTimerInterval();

    private void StartBlinkCore(int durationMs)
    {
        System.Windows.Forms.Timer? timer = _timer;
        if (timer == null)
        {
            return;
        }

        // Restarting mid-session keeps the originally captured appearance.
        if (_isBlinking)
        {
            timer.Stop();
            _isBlinking = false;
        }

        _sessionDurationMs = durationMs;
        _blinkCount = 0;
        _hardPhase = false;
        _blinkStartTick = Environment.TickCount;

        if (!_appearanceCaptured)
        {
            CaptureAppearance();
        }
        else if (_blinkValues.RestoreAppearanceOnStop)
        {
            ForeColor = _savedForeColor;
            BackColor = _savedBackColor;
            Visible = _savedVisible;
        }

        ConfigureTimerInterval();
        ApplyBlinkFrame(advanceHardPhase: false);
        _isBlinking = true;
        _blinkValues.SetBlinkEnabledCore(true);
        timer.Start();
        OnBlinkStarted(EventArgs.Empty);
    }

    private void CaptureAppearance()
    {
        _savedForeColor = ForeColor;
        _savedBackColor = BackColor;
        _savedVisible = Visible;
        _appearanceCaptured = true;
    }

    private void ConfigureTimerInterval()
    {
        _timer?.Interval = _blinkValues.BlinkMode == BlinkMode.Soft
            ? _blinkValues.SoftBlinkTickInterval
            : _blinkValues.BlinkInterval;
    }

    private void OnBlinkTimerTick(object? sender, EventArgs e)
    {
        if (!_isBlinking)
        {
            return;
        }

        OnBlinkTick(EventArgs.Empty);

        if (_sessionDurationMs > 0)
        {
            int elapsed = Environment.TickCount - (int)_blinkStartTick;
            if (elapsed >= _sessionDurationMs)
            {
                StopBlink();
                return;
            }
        }

        if (_blinkValues.PauseOnMouseOver && Selected)
        {
            return;
        }

        if (!CanUpdateWhileVisible())
        {
            return;
        }

        bool advancedHard = _blinkValues.BlinkMode != BlinkMode.Soft;
        ApplyBlinkFrame(advanceHardPhase: advancedHard);

        if (advancedHard && _blinkValues.MaxBlinkCount > 0 && _blinkCount >= _blinkValues.MaxBlinkCount)
        {
            StopBlink();
        }
    }

    private bool CanUpdateWhileVisible()
    {
        if (!_blinkValues.BlinkOnlyWhenVisible)
        {
            return true;
        }

        ToolStrip? owner = Owner;
        if (owner is { Visible: false })
        {
            return false;
        }

        // ToggleVisible intentionally hides the item; only gate on the parent strip.
        if (_blinkValues is { BlinkMode: BlinkMode.Visibility, VisibilityStyle: VisibilityStyle.ToggleVisible })
        {
            return true;
        }

        return Visible;
    }

    private void ApplyBlinkFrame(bool advanceHardPhase)
    {
        switch (_blinkValues.BlinkMode)
        {
            case BlinkMode.Hard:
                ApplyHardBlink(advanceHardPhase);
                break;
            case BlinkMode.Soft:
                ApplySoftBlink();
                break;
            case BlinkMode.Visibility:
                ApplyVisibilityBlink(advanceHardPhase);
                break;
        }
    }

    private void ApplyHardBlink(bool advanceHardPhase)
    {
        if (advanceHardPhase)
        {
            _hardPhase = !_hardPhase;
            _blinkCount++;
        }

        Color colour = _hardPhase ? _blinkValues.BlinkColorTwo : _blinkValues.BlinkColorOne;
        ApplyTargetColours(colour, colour);
    }

    private void ApplySoftBlink()
    {
        int elapsed = Environment.TickCount - (int)_blinkStartTick;
        if (elapsed < 0)
        {
            elapsed = 0;
        }

        int cycle = _blinkValues.SoftBlinkCycleInterval;
        int halfCycle = Math.Max(1, (int)Math.Round(cycle * 0.5));
        int n = elapsed % cycle;
        double per = Math.Abs(n - halfCycle) / (double)halfCycle;

        Color colorOne = _blinkValues.BlinkColorOne;
        Color colorTwo = _blinkValues.BlinkColorTwo;
        int red = (int)Math.Round((colorTwo.R - colorOne.R) * per) + colorOne.R;
        int green = (int)Math.Round((colorTwo.G - colorOne.G) * per) + colorOne.G;
        int blue = (int)Math.Round((colorTwo.B - colorOne.B) * per) + colorOne.B;
        Color colour = Color.FromArgb(ClampByte(red), ClampByte(green), ClampByte(blue));

        ApplyTargetColours(colour, colour);
    }

    private void ApplyVisibilityBlink(bool advanceHardPhase)
    {
        if (advanceHardPhase)
        {
            _hardPhase = !_hardPhase;
            _blinkCount++;
        }

        if (_blinkValues.VisibilityStyle == VisibilityStyle.ToggleVisible)
        {
            Visible = !_hardPhase ? _savedVisible : false;
            return;
        }

        // HideText: transparent ForeColor on the off phase; restore text colour on the on phase.
        if (_hardPhase)
        {
            ForeColor = Color.Transparent;
        }
        else if (_blinkValues.UseBlinkTextColor && _blinkValues.BlinkTextColor != Color.Empty)
        {
            ForeColor = _blinkValues.BlinkTextColor;
        }
        else
        {
            ForeColor = _savedForeColor;
        }
    }

    private void ApplyTargetColours(Color foreColour, Color backColour)
    {
        BlinkTarget target = _blinkValues.BlinkTarget;
        bool driveForeground = target == BlinkTarget.Foreground || target == BlinkTarget.Both;
        bool driveBackground = target == BlinkTarget.Background || target == BlinkTarget.Both;

        if (driveForeground)
        {
            ForeColor = foreColour;
        }
        else if (_blinkValues.UseBlinkTextColor && _blinkValues.BlinkTextColor != Color.Empty)
        {
            ForeColor = _blinkValues.BlinkTextColor;
        }

        if (driveBackground)
        {
            BackColor = backColour;
        }
    }

    private static int ClampByte(int value)
    {
        if (value < 0)
        {
            return 0;
        }

        if (value > 255)
        {
            return 255;
        }

        return value;
    }

    #endregion
}
