#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Utilities;

/// <summary>
/// A button control that displays a countdown timer in its text (e.g., "Some Text (30s)").
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonButton), "ToolboxBitmaps.KryptonButton.bmp")]
[DefaultEvent(nameof(CountdownFinished))]
[DefaultProperty(nameof(Text))]
[DesignerCategory(@"code")]
[Description(@"A button control that displays a countdown timer in its text.")]
public class KryptonCountdownButton : KryptonButton
{
    #region Instance Fields

    private volatile int _countdownSeconds;
    private readonly Timer _countdownTimer;
    private volatile string? _originalText;
    private readonly CountdownButtonValues _countdownButtonValues;

    #endregion

    #region Events

    /// <summary>Occurs when the countdown finishes.</summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the countdown finishes.")]
    public event EventHandler? CountdownFinished;

    /// <summary>Occurs when an error happens during the countdown.</summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when an error happens during the countdown.")]
    public event EventHandler<Exception>? CountdownError;

    #endregion

    #region Properties

    /// <summary>Gets the countdown button values.</summary>
    [Category("Values")]
    [Description("Values for the countdown button.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public CountdownButtonValues CountdownButtonValues => _countdownButtonValues;

    private bool ShouldSerializeCountdownButtonValues() => !_countdownButtonValues.IsDefault;

    /// <summary>Gets a value indicating whether the countdown is currently running.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsCountdownRunning => _countdownSeconds > 0;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonCountdownButton" /> class.</summary>
    public KryptonCountdownButton()
    {
        _countdownButtonValues = new CountdownButtonValues();

        _countdownTimer = new Timer
        {
            Interval = _countdownButtonValues.CountdownInterval
        };

        _countdownTimer.Tick += OnCountdownTimer_Tick;

        // Subscribe to interval changes so we can update the timer
        _countdownButtonValues.IntervalChangedCallback = OnIntervalChanged;
    }

    /// <summary>Handles interval changes from CountdownButtonValues.</summary>
    private void OnIntervalChanged(int newInterval)
    {
        if (InvokeRequired)
        {
            Invoke(new Action(() => _countdownTimer.Interval = newInterval));
        }
        else
        {
            _countdownTimer.Interval = newInterval;
        }
    }

    #endregion

    #region Implementation

    /// <summary>Starts the countdown. The button will be disabled during the countdown.</summary>
    /// <exception cref="InvalidOperationException">Thrown when the countdown is already running.</exception>
    public void StartCountdown()
    {
        if (_countdownSeconds > 0)
        {
            throw new InvalidOperationException("Countdown is already running.");
        }

        _countdownSeconds = _countdownButtonValues.CountdownDuration;
        
        // Only capture the original text if it's not already set, to avoid duplicating
        // countdown formatting if StartCountdown() is called when Text already contains formatted countdown
        if (string.IsNullOrEmpty(_originalText))
        {
            // Strip any existing countdown formatting before capturing the original text
            _originalText = StripCountdownFormatting(Text);
        }

        Enabled = false;
        UpdateCountdownText();

        _countdownTimer.Interval = _countdownButtonValues.CountdownInterval;
        _countdownTimer.Start();
    }

    /// <summary>Resets the countdown and restores the button to its original state.</summary>
    public void ResetCountdown()
    {
        _countdownTimer.Stop();
        _countdownSeconds = 0;
        Enabled = true;
        if (!string.IsNullOrEmpty(_originalText))
        {
            Text = _originalText;
        }
        // Clear _originalText so it can be re-captured on next StartCountdown() if Text has changed
        _originalText = null;
    }

    /// <summary>Cancels the countdown and restores the button to its original state.</summary>
    public void CancelCountdown()
    {
        ResetCountdown();
    }

    /// <summary>Strips any existing countdown formatting from the text.</summary>
    /// <param name="text">The text that may contain countdown formatting.</param>
    /// <returns>The text with countdown formatting removed.</returns>
    private string StripCountdownFormatting(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        // Try to detect and remove common countdown patterns like " (0s)", " (30s)", etc.
        // This handles the default format "{0} ({1})" where {1} is "Xs"
        // Match pattern: space + opening paren + optional whitespace + digits + suffix + closing paren at the end
        string suffix = _countdownButtonValues.CountdownSecondSuffix;
        string pattern = @"\s*\(\s*\d+" + Regex.Escape(suffix) + @"\s*\)\s*$";
        string stripped = Regex.Replace(text, pattern, string.Empty, RegexOptions.IgnoreCase);
        
        return stripped;
    }

    /// <summary>Updates the countdown text.</summary>
    private void UpdateCountdownText()
    {
        if (string.IsNullOrEmpty(_originalText))
        {
            // Strip any existing countdown formatting before capturing the original text
            _originalText = StripCountdownFormatting(Text);
        }
        // Use the suffix from the values class and format the text
        string secondsWithSuffix = _countdownSeconds + _countdownButtonValues.CountdownSecondSuffix;
        Text = string.Format(_countdownButtonValues.CountdownTextFormat, _originalText, secondsWithSuffix);
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

                // If EnableButtonAtZero is true, enable the button when it reaches 0 and show "0s"
                if (_countdownButtonValues.EnableButtonAtZero)
                {
                    UpdateCountdownSafely(() =>
                    {
                        Enabled = true;
                        // Keep showing "0s" format, then fire event
                        OnCountdownFinished(EventArgs.Empty);
                    });
                }
                else
                {
                    UpdateCountdownSafely(() =>
                    {
                        Enabled = true;
                        Text = _originalText;
                        OnCountdownFinished(EventArgs.Empty);
                    });
                }
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
        CountdownFinished?.Invoke(this, e);
    }

    /// <summary>Raises the <see cref="CountdownError" /> event.</summary>
    protected virtual void OnCountdownError(Exception ex)
    {
        CountdownError?.Invoke(this, ex);
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
