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

[TypeConverter(typeof(ExpandableObjectConverter))]
public class CountdownButtonValues : Storage
{
    #region Instance Fields

    private int _initialCountdownSeconds;
    private Timer _countdownTimer;

    #endregion

    #region Identity

    public CountdownButtonValues()
    {
        Reset();
    }

    #endregion

    #region IsDefault

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => CountdownDuration.Equals(GlobalStaticValues.DEFAULT_COUNTDOWN_VALUE) &&
                                      CountdownInterval.Equals(GlobalStaticValues.DEFAULT_COUNTDOWN_INTERVAL) &&
                                      CountdownTextFormat.Equals("{0} ({1})");

    #endregion

    #region Properties

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

    /// <summary>Gets the countdown timer.</summary>
    /// <value>The countdown timer.</value>
    internal Timer CountdownTimer => _countdownTimer;

    #endregion

    #region Implementation

    public void Reset()
    {
        CountdownInterval = GlobalStaticValues.DEFAULT_COUNTDOWN_INTERVAL;
        CountdownDuration = GlobalStaticValues.DEFAULT_COUNTDOWN_VALUE;
        CountdownTextFormat = "{0} ({1})";
    }

    #endregion
}