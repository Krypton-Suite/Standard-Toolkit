#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit;

internal class InternalKryptonCountdownButton : KryptonButton
{
    #region Instance Fields

    private volatile int _countdownSeconds;
    private int _initialCountdownSeconds;
    private readonly Timer _countdownTimer;
    private volatile string _originalText;

    #endregion

    #region Events

    /// <summary>Occurs when the countdown finishes.</summary>
    public event EventHandler CountdownFinished;

    /// <summary>Occurs when an error happens during the countdown.</summary>
    public event EventHandler<Exception> CountdownError;

    #endregion

    #region Properties

    /// <summary>Gets the countdown button values.</summary>
    [Category("Values")]
    [Description("Values for the countdown button.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public CountdownButtonValues CountdownButtonValues { get; }

    private bool ShouldSerializeCountdownButtonValues() => !CountdownButtonValues.IsDefault;

    /// <summary>Gets or sets the duration of the countdown.</summary>
    [DefaultValue(60)]
    [Description("The duration of the countdown in seconds.")]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int CountdownDuration
    {
        get => _initialCountdownSeconds;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), @"Countdown duration must be greater than zero.");
            }
            _initialCountdownSeconds = value;
        }
    }

    /// <summary>Gets or sets the countdown interval.</summary>
    [DefaultValue(1000)]
    [Description("The interval in milliseconds between countdown updates.")]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int CountdownInterval
    {
        get => _countdownTimer.Interval;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), @"Interval must be greater than zero.");
            }
            _countdownTimer.Interval = value;
        }
    }

    /// <summary>Gets or sets the countdown text format.</summary>
    [DefaultValue("{0} ({1})")]
    [Description("The format string for the countdown text.")]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string CountdownTextFormat { get; set; } = "{0} ({1})";

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="InternalKryptonCountdownButton" /> class.</summary>
    public InternalKryptonCountdownButton()
    {
        _initialCountdownSeconds = GlobalStaticValues.DEFAULT_COUNTDOWN_VALUE;

        _countdownTimer = new Timer
        {
            Interval = GlobalStaticValues.DEFAULT_COUNTDOWN_INTERVAL
        };

        _countdownTimer.Tick += OnCountdownTimer_Tick;

        CountdownDuration = GlobalStaticValues.DEFAULT_COUNTDOWN_VALUE;

        CountdownInterval = GlobalStaticValues.DEFAULT_COUNTDOWN_INTERVAL;
    }

    #endregion

    #region Implementation

    /// <summary>Starts the countdown.</summary>
    public void StartCountdown()
    {
        if (_countdownSeconds > 0)
        {
            throw new InvalidOperationException("Countdown is already running.");
        }

        _countdownSeconds = _initialCountdownSeconds;
        _originalText = Text;

        Enabled = false;
        UpdateCountdownText();

        _countdownTimer.Start();
    }

    /// <summary>Resets the countdown.</summary>
    public void ResetCountdown()
    {
        _countdownTimer.Stop();
        _countdownSeconds = 0;
        Enabled = true;
        Text = _originalText;
    }

    /// <summary>Cancels the countdown.</summary>
    public void CancelCountdown()
    {
        ResetCountdown();
    }

    /// <summary>Updates the countdown text.</summary>
    private void UpdateCountdownText()
    {
        if (string.IsNullOrEmpty(_originalText))
        {
            _originalText = Text;
        }
        Text = string.Format(CountdownTextFormat, _originalText, _countdownSeconds);
    }

    /// <summary>Safely updates the countdown or performs an action on the UI thread.</summary>
    private void UpdateCountdownSafely(Action? updateAction = null)
    {
        if (InvokeRequired)
        {
            Invoke(new Action(() =>
            {
                updateAction?.Invoke();
                UpdateCountdownText();
            }));
        }
        else
        {
            updateAction?.Invoke();
            UpdateCountdownText();
        }
    }

    /// <summary>Handles the countdown timer tick event.</summary>
    private void OnCountdownTimer_Tick(object? sender, EventArgs? e)
    {
        try
        {
            int remainingSeconds = Interlocked.Decrement(ref _countdownSeconds);

            if (remainingSeconds > 0)
            {
                UpdateCountdownSafely();
            }
            else
            {
                _countdownTimer.Stop();
                UpdateCountdownSafely(() =>
                {
                    Enabled = true;
                    Text = _originalText;
                    OnCountdownFinished(EventArgs.Empty);
                });
            }
        }
        catch (Exception ex)
        {
            ResetCountdown();
            OnCountdownError(ex);
        }
    }

    /// <summary>Raises the <see cref="CountdownFinished" /> event.</summary>
    protected virtual void OnCountdownFinished(EventArgs e)
    {
        CountdownFinished.Invoke(this, e);
    }

    /// <summary>Raises the <see cref="CountdownError" /> event.</summary>
    protected virtual void OnCountdownError(Exception ex)
    {
        CountdownError.Invoke(this, ex);
    }

    #endregion

    #region Overrides

    /// <summary>Clean up any resources being used.</summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _countdownTimer.Dispose();
        }
        base.Dispose(disposing);
    }

    #endregion
}