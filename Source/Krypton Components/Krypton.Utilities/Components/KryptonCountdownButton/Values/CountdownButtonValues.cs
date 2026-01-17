#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Storage for countdown button values.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class CountdownButtonValues : Storage
{
    #region Static Fields

    private const int DEFAULT_COUNTDOWN_VALUE = 60;
    private const int DEFAULT_COUNTDOWN_INTERVAL = 1000;
    private const string DEFAULT_COUNTDOWN_TEXT_FORMAT = "{0} ({1})";
    private const string DEFAULT_COUNTDOWN_SECOND_SUFFIX = "s";

    #endregion

    #region Instance Fields

    private int _countdownDuration;
    private int _countdownInterval;
    private string _countdownTextFormat;
    private bool _enableButtonAtZero;
    private Action<int>? _intervalChangedCallback;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="CountdownButtonValues" /> class.</summary>
    public CountdownButtonValues()
    {
        Reset();
    }

    #endregion

    #region IsDefault

    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => CountdownDuration == DEFAULT_COUNTDOWN_VALUE &&
                                      CountdownInterval == DEFAULT_COUNTDOWN_INTERVAL &&
                                      CountdownTextFormat == DEFAULT_COUNTDOWN_TEXT_FORMAT &&
                                      !EnableButtonAtZero &&
                                      CountdownSecondSuffix == DEFAULT_COUNTDOWN_SECOND_SUFFIX;

    #endregion

    #region Properties

    /// <summary>Gets or sets the duration of the countdown in seconds.</summary>
    [DefaultValue(60)]
    [Description("The duration of the countdown in seconds.")]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int CountdownDuration
    {
        get => _countdownDuration;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), @"Countdown duration must be greater than zero.");
            }
            _countdownDuration = value;
        }
    }

    /// <summary>Gets or sets the countdown interval in milliseconds.</summary>
    [DefaultValue(1000)]
    [Description("The interval in milliseconds between countdown updates.")]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int CountdownInterval
    {
        get => _countdownInterval;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), @"Interval must be greater than zero.");
            }
            if (_countdownInterval != value)
            {
                _countdownInterval = value;
                _intervalChangedCallback?.Invoke(value);
            }
        }
    }

    /// <summary>Gets or sets the callback to invoke when the countdown interval changes.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal Action<int>? IntervalChangedCallback
    {
        get => _intervalChangedCallback;
        set => _intervalChangedCallback = value;
    }

    /// <summary>Gets or sets the countdown text format. Use {0} for original text and {1} for seconds (includes suffix).</summary>
    [DefaultValue("{0} ({1})")]
    [Description("The format string for the countdown text. Use {0} for original text and {1} for seconds (includes suffix).")]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string CountdownTextFormat
    {
        get => _countdownTextFormat;
        set => _countdownTextFormat = value ?? DEFAULT_COUNTDOWN_TEXT_FORMAT;
    }

    /// <summary>Gets or sets a value indicating whether the button should be enabled when the countdown reaches zero.</summary>
    [DefaultValue(false)]
    [Description("If true, the button will be enabled when the countdown reaches zero (before the CountdownFinished event fires).")]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool EnableButtonAtZero
    {
        get => _enableButtonAtZero;
        set => _enableButtonAtZero = value;
    }

    /// <summary>Gets or sets the suffix for the countdown seconds.</summary>
    [DefaultValue("s")]
    [Description("The suffix for the countdown seconds.")]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string CountdownSecondSuffix { get; set; } = DEFAULT_COUNTDOWN_SECOND_SUFFIX;

    #endregion

    #region Implementation

    /// <summary>Resets all properties to their default values.</summary>
    public void Reset()
    {
        CountdownDuration = DEFAULT_COUNTDOWN_VALUE;
        CountdownInterval = DEFAULT_COUNTDOWN_INTERVAL;
        CountdownTextFormat = DEFAULT_COUNTDOWN_TEXT_FORMAT;
        EnableButtonAtZero = false;
        CountdownSecondSuffix = DEFAULT_COUNTDOWN_SECOND_SUFFIX;
    }

    #endregion
}
