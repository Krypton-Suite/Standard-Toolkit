#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Gauges;

/// <summary>
/// Level and input settings for a <see cref="KryptonVuMeter"/>.
/// Face colours are <see cref="KryptonVuMeter.StateCommon"/>, <see cref="KryptonVuMeter.StateNormal"/>, and <see cref="KryptonVuMeter.StateDisabled"/>.
/// </summary>
public class VuMeterValues : Storage
{
    #region Instance Fields

    private const double DefaultMinimum = -20d;
    private const double DefaultMaximum = 3d;
    private const double DefaultValue = -20d;
    private const int DefaultAttack = 300;
    private const int DefaultRelease = 300;
    private const int DefaultPeakHold = 1500;
    private const int DefaultPeakDecay = 2000;

    private readonly KryptonVuMeter _owner;
    private VuMeterStyle _style;
    private VuMeterInputMode _inputMode;
    private VuMeterChannel _channel;
    private VisualOrientation _orientation = VisualOrientation.Left;
    private double _minimum = DefaultMinimum;
    private double _maximum = DefaultMaximum;
    private double _value = DefaultValue;
    private double _calibrationOffsetDb = VuMeterScale.DefaultCalibrationOffsetDb;
    private bool _peakHold;
    private bool _showScaleLabels = true;
    private int _attackMilliseconds = DefaultAttack;
    private int _releaseMilliseconds = DefaultRelease;
    private int _peakHoldMilliseconds = DefaultPeakHold;
    private int _peakDecayMilliseconds = DefaultPeakDecay;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="VuMeterValues"/> class.
    /// </summary>
    /// <param name="owner">Owning meter.</param>
    public VuMeterValues(KryptonVuMeter owner) => _owner = owner;

    /// <summary>
    /// Returns an empty string when every setting is at the factory default, otherwise "Modified".
    /// </summary>
    /// <returns>Empty when <see cref="IsDefault"/> is true.</returns>
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    public override bool IsDefault =>
        _style == VuMeterStyle.Analog &&
        _inputMode == VuMeterInputMode.Manual &&
        _channel == VuMeterChannel.Left &&
        _orientation == VisualOrientation.Left &&
        _minimum == DefaultMinimum &&
        _maximum == DefaultMaximum &&
        _value == DefaultValue &&
        _calibrationOffsetDb == VuMeterScale.DefaultCalibrationOffsetDb &&
        !_peakHold &&
        _showScaleLabels &&
        _attackMilliseconds == DefaultAttack &&
        _releaseMilliseconds == DefaultRelease &&
        _peakHoldMilliseconds == DefaultPeakHold &&
        _peakDecayMilliseconds == DefaultPeakDecay;

    /// <summary>
    /// Restores every setting to the factory default.
    /// </summary>
    public void Reset()
    {
        Style = VuMeterStyle.Analog;
        InputMode = VuMeterInputMode.Manual;
        Channel = VuMeterChannel.Left;
        Orientation = VisualOrientation.Left;
        Minimum = DefaultMinimum;
        Maximum = DefaultMaximum;
        Value = DefaultValue;
        CalibrationOffsetDb = VuMeterScale.DefaultCalibrationOffsetDb;
        PeakHold = false;
        ShowScaleLabels = true;
        AttackMilliseconds = DefaultAttack;
        ReleaseMilliseconds = DefaultRelease;
        PeakHoldMilliseconds = DefaultPeakHold;
        PeakDecayMilliseconds = DefaultPeakDecay;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the painted face.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Analog needle or segmented bar.")]
    [DefaultValue(VuMeterStyle.Analog)]
    public VuMeterStyle Style
    {
        get => _style;
        set => SetStyle(value);
    }

    private bool ShouldSerializeStyle() => _style != VuMeterStyle.Analog;

    private void ResetStyle() => Style = VuMeterStyle.Analog;

    /// <summary>
    /// Gets or sets whether the level comes from <see cref="Value"/> or the default recording device.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Manual uses Value. Capture reads the default recording device and ignores assignments.")]
    [DefaultValue(VuMeterInputMode.Manual)]
    public VuMeterInputMode InputMode
    {
        get => _inputMode;
        set => SetInputMode(value);
    }

    private bool ShouldSerializeInputMode() => _inputMode != VuMeterInputMode.Manual;

    private void ResetInputMode() => InputMode = VuMeterInputMode.Manual;

    /// <summary>
    /// Gets or sets the recording channel used while <see cref="InputMode"/> is <see cref="VuMeterInputMode.Capture"/>.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Recording channel used while InputMode is Capture.")]
    [DefaultValue(VuMeterChannel.Left)]
    public VuMeterChannel Channel
    {
        get => _channel;
        set
        {
            if (_channel == value)
            {
                return;
            }

            _channel = value;
        }
    }

    private bool ShouldSerializeChannel() => _channel != VuMeterChannel.Left;

    private void ResetChannel() => Channel = VuMeterChannel.Left;

    /// <summary>
    /// Gets or sets how a segmented face is laid out. The analog face ignores this.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Segment direction. Left and Right are horizontal. Top and Bottom are vertical. Analog ignores this.")]
    [DefaultValue(VisualOrientation.Left)]
    public VisualOrientation Orientation
    {
        get => _orientation;
        set
        {
            if (_orientation == value)
            {
                return;
            }

            _orientation = value;
            _owner.PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeOrientation() => _orientation != VisualOrientation.Left;

    private void ResetOrientation() => Orientation = VisualOrientation.Left;

    /// <summary>
    /// Gets or sets the left-hand (lowest) level of the scale, in VU.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Lowest level drawn on the scale, in VU.")]
    public double Minimum
    {
        get => _minimum;
        set => SetMinimum(value);
    }

    private bool ShouldSerializeMinimum() => _minimum != DefaultMinimum;

    private void ResetMinimum() => Minimum = DefaultMinimum;

    /// <summary>
    /// Gets or sets the right-hand (highest) level of the scale, in VU.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Highest level drawn on the scale, in VU.")]
    public double Maximum
    {
        get => _maximum;
        set => SetMaximum(value);
    }

    private bool ShouldSerializeMaximum() => _maximum != DefaultMaximum;

    private void ResetMaximum() => Maximum = DefaultMaximum;

    /// <summary>
    /// Gets or sets the target level in VU. The painted indicator eases toward this value.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Target level in VU. The needle or bar eases toward this value.")]
    public double Value
    {
        get => _value;
        set => SetValue(value, fromCapture: false);
    }

    private bool ShouldSerializeValue() => _value != DefaultValue;

    private void ResetValue() => Value = DefaultValue;

    /// <summary>
    /// Gets or sets the digital alignment used while capturing. 0 VU corresponds to this many dBFS.
    /// </summary>
    /// <remarks>
    /// The default of -18 places 0 VU at -18 dBFS. This is a convenient digital alignment, not an IEC 60268 calibration.
    /// </remarks>
    [Category(@"Behavior")]
    [Description(@"dBFS that maps to 0 VU while capturing. Default -18. Not an IEC calibration.")]
    public double CalibrationOffsetDb
    {
        get => _calibrationOffsetDb;
        set
        {
            if (_calibrationOffsetDb == value)
            {
                return;
            }

            _calibrationOffsetDb = value;
        }
    }

    private bool ShouldSerializeCalibrationOffsetDb() => _calibrationOffsetDb != VuMeterScale.DefaultCalibrationOffsetDb;

    private void ResetCalibrationOffsetDb() => CalibrationOffsetDb = VuMeterScale.DefaultCalibrationOffsetDb;

    /// <summary>
    /// Gets or sets whether a second indicator holds the recent peak.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Draw a second indicator that holds the recent peak, then falls back.")]
    [DefaultValue(false)]
    public bool PeakHold
    {
        get => _peakHold;
        set
        {
            if (_peakHold == value)
            {
                return;
            }

            _peakHold = value;
            _owner.OnPeakHoldChanged();
        }
    }

    private bool ShouldSerializePeakHold() => _peakHold;

    private void ResetPeakHold() => PeakHold = false;

    /// <summary>
    /// Gets or sets whether numeric scale marks are drawn.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Draw the numeric scale marks.")]
    [DefaultValue(true)]
    public bool ShowScaleLabels
    {
        get => _showScaleLabels;
        set
        {
            if (_showScaleLabels == value)
            {
                return;
            }

            _showScaleLabels = value;
            _owner.PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeShowScaleLabels() => !_showScaleLabels;

    private void ResetShowScaleLabels() => ShowScaleLabels = true;

    /// <summary>
    /// Gets or sets how quickly the indicator rises, in milliseconds. Zero snaps immediately.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Milliseconds the indicator takes to rise. Zero snaps to the target.")]
    [DefaultValue(DefaultAttack)]
    public int AttackMilliseconds
    {
        get => _attackMilliseconds;
        set => _attackMilliseconds = value < 0 ? 0 : value;
    }

    private bool ShouldSerializeAttackMilliseconds() => _attackMilliseconds != DefaultAttack;

    private void ResetAttackMilliseconds() => AttackMilliseconds = DefaultAttack;

    /// <summary>
    /// Gets or sets how quickly the indicator falls, in milliseconds. Zero snaps immediately.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Milliseconds the indicator takes to fall. Zero snaps to the target.")]
    [DefaultValue(DefaultRelease)]
    public int ReleaseMilliseconds
    {
        get => _releaseMilliseconds;
        set => _releaseMilliseconds = value < 0 ? 0 : value;
    }

    private bool ShouldSerializeReleaseMilliseconds() => _releaseMilliseconds != DefaultRelease;

    private void ResetReleaseMilliseconds() => ReleaseMilliseconds = DefaultRelease;

    /// <summary>
    /// Gets or sets how long a peak is held before it starts falling, in milliseconds.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Milliseconds a peak is held before it falls.")]
    [DefaultValue(DefaultPeakHold)]
    public int PeakHoldMilliseconds
    {
        get => _peakHoldMilliseconds;
        set => _peakHoldMilliseconds = value < 0 ? 0 : value;
    }

    private bool ShouldSerializePeakHoldMilliseconds() => _peakHoldMilliseconds != DefaultPeakHold;

    private void ResetPeakHoldMilliseconds() => PeakHoldMilliseconds = DefaultPeakHold;

    /// <summary>
    /// Gets or sets how quickly a held peak falls back toward the live level, in milliseconds.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Milliseconds a held peak takes to fall back toward the live level.")]
    [DefaultValue(DefaultPeakDecay)]
    public int PeakDecayMilliseconds
    {
        get => _peakDecayMilliseconds;
        set => _peakDecayMilliseconds = value < 0 ? 0 : value;
    }

    private bool ShouldSerializePeakDecayMilliseconds() => _peakDecayMilliseconds != DefaultPeakDecay;

    private void ResetPeakDecayMilliseconds() => PeakDecayMilliseconds = DefaultPeakDecay;

    #endregion

    #region Implementation

    internal void SetCapturedValue(double value) => SetValue(value, fromCapture: true);

    private void SetStyle(VuMeterStyle value)
    {
        if (_style == value)
        {
            return;
        }

        _style = value;
        _owner.PerformNeedPaint(true);
    }

    private void SetInputMode(VuMeterInputMode value)
    {
        if (_inputMode == value)
        {
            return;
        }

        _inputMode = value;
        _owner.OnInputModeChanged();
    }

    private void SetMinimum(double value)
    {
        if (value > _maximum)
        {
            _maximum = value;
        }

        if (_minimum == value)
        {
            SetValue(_value, fromCapture: true);
            return;
        }

        _minimum = value;
        SetValue(_value, fromCapture: true);
        _owner.PerformNeedPaint(true);
    }

    private void SetMaximum(double value)
    {
        if (value < _minimum)
        {
            _minimum = value;
        }

        if (_maximum == value)
        {
            SetValue(_value, fromCapture: true);
            return;
        }

        _maximum = value;
        SetValue(_value, fromCapture: true);
        _owner.PerformNeedPaint(true);
    }

    private void SetValue(double value, bool fromCapture)
    {
        if (!fromCapture && _inputMode == VuMeterInputMode.Capture && !_owner.IsApplyingCapture)
        {
            return;
        }

        double clamped = Clamp(value);
        if (Math.Abs(clamped - _value) < 0.01d)
        {
            return;
        }

        _value = clamped;
        _owner.OnTargetChanged();
    }

    private double Clamp(double value)
    {
        if (value < _minimum)
        {
            return _minimum;
        }

        return value > _maximum ? _maximum : value;
    }

    #endregion
}
