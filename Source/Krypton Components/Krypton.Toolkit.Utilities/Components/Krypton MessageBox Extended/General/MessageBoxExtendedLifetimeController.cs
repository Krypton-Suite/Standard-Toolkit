#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Options for fade, caption timeout, and auto-close on an extended message box.
/// </summary>
internal sealed class MessageBoxExtendedLifetimeOptions
{
    /// <summary>Gets or sets whether the form fades in on show and out on close.</summary>
    public bool UseFade { get; set; }

    /// <summary>Gets or sets the fade speed preset.</summary>
    public FadeSpeedChoice FadeSpeed { get; set; } = FadeSpeedChoice.Normal;

    /// <summary>Gets or sets a custom fade step used when <see cref="FadeSpeed"/> is <see cref="FadeSpeedChoice.Custom"/>.</summary>
    public float? CustomFadeSpeed { get; set; }

    /// <summary>Gets or sets whether remaining seconds are shown in the caption.</summary>
    public bool UseTimeOut { get; set; }

    /// <summary>Gets or sets the timeout duration in seconds.</summary>
    public int TimeOutSeconds { get; set; } = 60;

    /// <summary>Gets or sets the timeout tick interval in milliseconds.</summary>
    public int TimeOutInterval { get; set; } = 1000;

    /// <summary>Gets or sets whether the dialog dismisses when the timeout reaches zero.</summary>
    public bool AutoClose { get; set; }

    /// <summary>Gets or sets the result used when <see cref="TimeOutAction"/> is <see cref="ExtendedMessageBoxTimeoutAction.Close"/>.</summary>
    public DialogResult TimeOutResult { get; set; }

    /// <summary>Gets or sets the auto-close action.</summary>
    public ExtendedMessageBoxTimeoutAction TimeOutAction { get; set; }

    /// <summary>Gets or sets the original window caption (without a countdown suffix).</summary>
    public string Caption { get; set; } = string.Empty;

    /// <summary>
    /// Builds options from <see cref="KryptonMessageBoxExtendedData"/>.
    /// </summary>
    /// <param name="data">The message box data.</param>
    /// <returns>Resolved lifetime options.</returns>
    public static MessageBoxExtendedLifetimeOptions FromData(KryptonMessageBoxExtendedData data)
    {
        bool useTimeOut = data.UseTimeOut;
        return new MessageBoxExtendedLifetimeOptions
        {
            UseFade = data.UseFade,
            FadeSpeed = data.FadeSpeed,
            CustomFadeSpeed = data.CustomFadeSpeed,
            UseTimeOut = useTimeOut,
            TimeOutSeconds = data.TimeOut > 0 ? data.TimeOut : 60,
            TimeOutInterval = data.TimeOutInterval > 0 ? data.TimeOutInterval : 1000,
            AutoClose = data.AutoClose ?? useTimeOut,
            TimeOutResult = data.TimeOutResult,
            TimeOutAction = data.TimeOutAction,
            Caption = data.Caption ?? string.Empty
        };
    }

    /// <summary>
    /// Builds options from the existing <c>Show</c> timeout parameters. Timeout implies auto-close; fade is off.
    /// </summary>
    /// <param name="useTimeOut">Whether the caption countdown is enabled.</param>
    /// <param name="timeOutSeconds">Timeout in seconds.</param>
    /// <param name="timeOutInterval">Tick interval in milliseconds.</param>
    /// <param name="timeOutResult">Dialog result when the timeout closes the form.</param>
    /// <param name="caption">Original window caption.</param>
    /// <returns>Resolved lifetime options.</returns>
    public static MessageBoxExtendedLifetimeOptions FromShowParameters(
        bool useTimeOut,
        int timeOutSeconds,
        int timeOutInterval,
        DialogResult timeOutResult,
        string caption) =>
        new MessageBoxExtendedLifetimeOptions
        {
            UseFade = false,
            UseTimeOut = useTimeOut,
            TimeOutSeconds = timeOutSeconds > 0 ? timeOutSeconds : 60,
            TimeOutInterval = timeOutInterval > 0 ? timeOutInterval : 1000,
            AutoClose = useTimeOut,
            TimeOutResult = timeOutResult,
            TimeOutAction = ExtendedMessageBoxTimeoutAction.Close,
            Caption = caption ?? string.Empty
        };
}

/// <summary>
/// Shared fade-in/out, caption countdown, and auto-close host for LTR and RTL extended message boxes.
/// Opacity is stepped with a WinForms <see cref="Timer"/> so ticks run inside the nested <c>ShowDialog</c> pump.
/// </summary>
internal sealed class MessageBoxExtendedLifetimeController : IDisposable
{
    // Match KryptonFormFadeSpeed (internal in Toolkit) so Utilities does not need InternalsVisibleTo.
    private const float FadeSlowest = 1f;
    private const float FadeSlower = 10f;
    private const float FadeSlow = 25f;
    private const float FadeNormal = 50f;
    private const float FadeFast = 60f;
    private const float FadeFaster = 75f;
    private const float FadeFastest = 100f;
    private const float FadeCustomFallback = 0.5f;
    private const int FadeTimerIntervalMs = 10;

    private readonly Form _form;
    private readonly MessageBoxExtendedLifetimeOptions _options;
    private readonly Func<ExtendedMessageBoxTimeoutAction, Control?> _resolveButton;
    private readonly Func<DialogResult> _resolveDefaultResult;
    private readonly Action _cancelButtonCountdowns;

    private Timer? _fadeTimer;
    private Timer? _timeOutTimer;
    private FormFadeDirection _fadeDirection;
    private float _fadeSpeed;
    private int _remainingSeconds;
    private bool _isFadingOut;
    private bool _fadeOutComplete;
    private bool _attached;
    private bool _disposed;
    private DialogResult _pendingResult;

    /// <summary>
    /// Initializes a new controller for <paramref name="form"/>.
    /// </summary>
    /// <param name="form">The message box form.</param>
    /// <param name="options">Fade and timeout options.</param>
    /// <param name="resolveButton">Resolves a timeout action to a visible button, or null to fall back to close.</param>
    /// <param name="resolveDefaultResult">Result used when <see cref="MessageBoxExtendedLifetimeOptions.TimeOutResult"/> is <see cref="DialogResult.None"/>.</param>
    /// <param name="cancelButtonCountdowns">Stops any per-button countdown so form-level timeout wins.</param>
    public MessageBoxExtendedLifetimeController(
        Form form,
        MessageBoxExtendedLifetimeOptions options,
        Func<ExtendedMessageBoxTimeoutAction, Control?> resolveButton,
        Func<DialogResult> resolveDefaultResult,
        Action cancelButtonCountdowns)
    {
        _form = form;
        _options = options;
        _resolveButton = resolveButton;
        _resolveDefaultResult = resolveDefaultResult;
        _cancelButtonCountdowns = cancelButtonCountdowns;
        _remainingSeconds = options.TimeOutSeconds;
        _fadeSpeed = ResolveFadeSpeed(options.FadeSpeed, options.CustomFadeSpeed);
    }

    /// <summary>
    /// Hooks form events and applies the initial caption / opacity. Call after buttons have been configured.
    /// </summary>
    public void Attach()
    {
        if (_attached || _disposed)
        {
            return;
        }

        _attached = true;

        if (_options.UseFade)
        {
            _form.Opacity = 0;
        }

        if (_options.UseTimeOut)
        {
            ApplyCaption();
        }

        _form.Shown += OnShown;
        _form.FormClosing += OnFormClosing;
        _form.Disposed += OnFormDisposed;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;

        if (_attached)
        {
            _form.Shown -= OnShown;
            _form.FormClosing -= OnFormClosing;
            _form.Disposed -= OnFormDisposed;
            _attached = false;
        }

        StopTimeOutTimer();
        StopFadeTimer();
    }

    private void OnFormDisposed(object? sender, EventArgs e) => Dispose();

    private void OnShown(object? sender, EventArgs e)
    {
        if (_disposed || _form.IsDisposed)
        {
            return;
        }

        if (_options.UseFade)
        {
            StartFadeIn();
        }

        if (_options.UseTimeOut || _options.AutoClose)
        {
            StartTimeOutTimer();
        }
    }

    private void OnFormClosing(object? sender, FormClosingEventArgs e)
    {
        if (_disposed || _form.IsDisposed)
        {
            return;
        }

        StopTimeOutTimer();
        _cancelButtonCountdowns();

        if (!_options.UseFade || _fadeOutComplete)
        {
            return;
        }

        e.Cancel = true;

        if (_isFadingOut)
        {
            return;
        }

        _pendingResult = _form.DialogResult;
        StartFadeOut();
    }

    private void StartTimeOutTimer()
    {
        if (_timeOutTimer != null)
        {
            return;
        }

        _timeOutTimer = new Timer
        {
            Interval = _options.TimeOutInterval
        };
        _timeOutTimer.Tick += OnTimeOutTick;
        _timeOutTimer.Start();
    }

    private void StopTimeOutTimer()
    {
        if (_timeOutTimer == null)
        {
            return;
        }

        _timeOutTimer.Stop();
        _timeOutTimer.Tick -= OnTimeOutTick;
        _timeOutTimer.Dispose();
        _timeOutTimer = null;
    }

    private void OnTimeOutTick(object? sender, EventArgs e)
    {
        if (_disposed || _form.IsDisposed)
        {
            StopTimeOutTimer();
            return;
        }

        _remainingSeconds--;

        if (_options.UseTimeOut)
        {
            ApplyCaption();
        }

        if (_remainingSeconds > 0)
        {
            return;
        }

        StopTimeOutTimer();

        if (!_options.AutoClose)
        {
            return;
        }

        // Form-level timeout wins over a still-running button countdown.
        _cancelButtonCountdowns();
        ApplyTimeoutAction();
    }

    private void ApplyCaption()
    {
        string caption = string.IsNullOrEmpty(_options.Caption)
            ? string.Empty
            : _options.Caption.Split(Environment.NewLine.ToCharArray())[0];

        _form.Text = _remainingSeconds >= 0
            ? $"{caption} ({_remainingSeconds})"
            : caption;
    }

    private void ApplyTimeoutAction()
    {
        if (_disposed || _form.IsDisposed)
        {
            return;
        }

        Control? button = _options.TimeOutAction == ExtendedMessageBoxTimeoutAction.Close
            ? null
            : _resolveButton(_options.TimeOutAction);

        if (button is IButtonControl buttonControl && button.Visible)
        {
            buttonControl.PerformClick();
            return;
        }

        _form.DialogResult = ResolveCloseResult();
        _form.Close();
    }

    private DialogResult ResolveCloseResult()
    {
        if (_options.TimeOutResult != DialogResult.None)
        {
            return _options.TimeOutResult;
        }

        DialogResult fallback = _resolveDefaultResult();
        return fallback != DialogResult.None ? fallback : DialogResult.OK;
    }

    private void StartFadeIn()
    {
        _fadeDirection = FormFadeDirection.In;
        _form.Opacity = 0;
        EnsureFadeTimer();
        _fadeTimer!.Start();
    }

    private void StartFadeOut()
    {
        _isFadingOut = true;
        _fadeDirection = FormFadeDirection.Out;

        if (_form.Opacity < 0.1)
        {
            CompleteFadeOut();
            return;
        }

        if (_form.Opacity > 1.0)
        {
            _form.Opacity = 1.0;
        }

        EnsureFadeTimer();
        _fadeTimer!.Start();
    }

    private void EnsureFadeTimer()
    {
        if (_fadeTimer != null)
        {
            return;
        }

        _fadeTimer = new Timer
        {
            Interval = FadeTimerIntervalMs
        };
        _fadeTimer.Tick += OnFadeTick;
    }

    private void StopFadeTimer()
    {
        if (_fadeTimer == null)
        {
            return;
        }

        _fadeTimer.Stop();
        _fadeTimer.Tick -= OnFadeTick;
        _fadeTimer.Dispose();
        _fadeTimer = null;
    }

    private void OnFadeTick(object? sender, EventArgs e)
    {
        if (_disposed || _form.IsDisposed)
        {
            StopFadeTimer();
            return;
        }

        switch (_fadeDirection)
        {
            case FormFadeDirection.In:
                if (_form.Opacity < 1.0)
                {
                    _form.Opacity += _fadeSpeed / 1000.0;
                }
                else
                {
                    _form.Opacity = 1.0;
                    StopFadeTimer();
                }

                break;

            case FormFadeDirection.Out:
                if (_form.Opacity > 0.1)
                {
                    _form.Opacity -= _fadeSpeed / 1000.0;
                }
                else
                {
                    CompleteFadeOut();
                }

                break;
        }
    }

    private void CompleteFadeOut()
    {
        StopFadeTimer();
        _fadeOutComplete = true;
        _isFadingOut = false;

        if (_disposed || _form.IsDisposed)
        {
            return;
        }

        if (_pendingResult != DialogResult.None)
        {
            _form.DialogResult = _pendingResult;
        }

        _form.Close();
    }

    private static float ResolveFadeSpeed(FadeSpeedChoice fadeSpeedChoice, float? customFadeSpeed) =>
        fadeSpeedChoice switch
        {
            FadeSpeedChoice.Slowest => FadeSlowest,
            FadeSpeedChoice.Slower => FadeSlower,
            FadeSpeedChoice.Slow => FadeSlow,
            FadeSpeedChoice.Normal => FadeNormal,
            FadeSpeedChoice.Fast => FadeFast,
            FadeSpeedChoice.Faster => FadeFaster,
            FadeSpeedChoice.Fastest => FadeFastest,
            FadeSpeedChoice.Custom => customFadeSpeed ?? FadeCustomFallback,
            _ => FadeNormal
        };
}
