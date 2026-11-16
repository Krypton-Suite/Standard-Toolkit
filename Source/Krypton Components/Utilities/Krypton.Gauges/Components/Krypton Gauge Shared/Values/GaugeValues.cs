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
/// Shared value, range, and input settings for analogue gauges.
/// Face colours are on the owning control <c>StateCommon</c> / <c>StateNormal</c> / <c>StateDisabled</c>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class GaugeValues : Storage
{
    #region Instance Fields

    private const double DefaultMinimum = 0d;
    private const double DefaultMaximum = 100d;
    private const double DefaultValue = 0d;
    private const int DefaultAnimationMilliseconds = 200;
    private const double DefaultSmallChange = 1d;
    private const double DefaultLargeChange = 10d;

    private readonly KryptonGaugeBase _owner;
    private GaugeInputMode _inputMode;
    private double _minimum = DefaultMinimum;
    private double _maximum = DefaultMaximum;
    private double _value = DefaultValue;
    private bool _animate = true;
    private int _animationMilliseconds = DefaultAnimationMilliseconds;
    private bool _showScaleLabels = true;
    private string _caption = string.Empty;
    private string _units = string.Empty;
    private double _smallChange = DefaultSmallChange;
    private double _largeChange = DefaultLargeChange;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="GaugeValues"/> class.
    /// </summary>
    /// <param name="owner">Owning gauge.</param>
    public GaugeValues(KryptonGaugeBase owner)
    {
        _owner = owner;
        Ranges = new GaugeRangeCollection(this);
    }

    /// <summary>
    /// Gets the owning gauge.
    /// </summary>
    protected KryptonGaugeBase Owner => _owner;

    /// <summary>
    /// Returns an empty string when every setting is at the factory default, otherwise "Modified".
    /// </summary>
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    public override bool IsDefault =>
        _inputMode == GaugeInputMode.Manual &&
        _minimum == DefaultMinimum &&
        _maximum == DefaultMaximum &&
        _value == DefaultValue &&
        _animate &&
        _animationMilliseconds == DefaultAnimationMilliseconds &&
        _showScaleLabels &&
        string.IsNullOrEmpty(_caption) &&
        string.IsNullOrEmpty(_units) &&
        _smallChange == DefaultSmallChange &&
        _largeChange == DefaultLargeChange &&
        Ranges.IsDefault &&
        IsSpecializedDefault;

    /// <summary>
    /// Gets whether derived face settings are at their factory defaults.
    /// </summary>
    protected virtual bool IsSpecializedDefault => true;

    /// <summary>
    /// Restores every setting to the factory default.
    /// </summary>
    public virtual void Reset()
    {
        InputMode = GaugeInputMode.Manual;
        Minimum = DefaultMinimum;
        Maximum = DefaultMaximum;
        Value = DefaultValue;
        Animate = true;
        AnimationMilliseconds = DefaultAnimationMilliseconds;
        ShowScaleLabels = true;
        Caption = string.Empty;
        Units = string.Empty;
        SmallChange = DefaultSmallChange;
        LargeChange = DefaultLargeChange;
        Ranges.Clear();
        ResetSpecialized();
    }

    /// <summary>
    /// Restores derived face settings to their factory defaults.
    /// </summary>
    protected virtual void ResetSpecialized()
    {
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets whether the value is application-driven or user-interactive.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Manual uses Value from code. Interactive allows pointer and keyboard input.")]
    [DefaultValue(GaugeInputMode.Manual)]
    public GaugeInputMode InputMode
    {
        get => _inputMode;
        set
        {
            if (_inputMode == value)
            {
                return;
            }

            _inputMode = value;
            _owner.OnInputModeChanged();
        }
    }

    private bool ShouldSerializeInputMode() => _inputMode != GaugeInputMode.Manual;

    private void ResetInputMode() => InputMode = GaugeInputMode.Manual;

    /// <summary>
    /// Gets or sets the lowest value of the scale.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Lowest value drawn on the scale.")]
    [DefaultValue(DefaultMinimum)]
    public double Minimum
    {
        get => _minimum;
        set => SetMinimum(value);
    }

    private bool ShouldSerializeMinimum() => _minimum != DefaultMinimum;

    private void ResetMinimum() => Minimum = DefaultMinimum;

    /// <summary>
    /// Gets or sets the highest value of the scale.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Highest value drawn on the scale.")]
    [DefaultValue(DefaultMaximum)]
    public double Maximum
    {
        get => _maximum;
        set => SetMaximum(value);
    }

    private bool ShouldSerializeMaximum() => _maximum != DefaultMaximum;

    private void ResetMaximum() => Maximum = DefaultMaximum;

    /// <summary>
    /// Gets or sets the target value. The painted indicator eases toward this when <see cref="Animate"/> is true.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Target value. The indicator eases toward this when Animate is true.")]
    [DefaultValue(DefaultValue)]
    public double Value
    {
        get => _value;
        set => SetValue(value);
    }

    private bool ShouldSerializeValue() => _value != DefaultValue;

    private void ResetValue() => Value = DefaultValue;

    /// <summary>
    /// Gets the coloured bands drawn on the scale.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Coloured warning and error bands on the scale.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public GaugeRangeCollection Ranges { get; }

    private bool ShouldSerializeRanges() => !Ranges.IsDefault;

    /// <summary>
    /// Gets or sets whether the painted indicator eases toward <see cref="Value"/>.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Ease the painted indicator toward Value.")]
    [DefaultValue(true)]
    public bool Animate
    {
        get => _animate;
        set
        {
            if (_animate == value)
            {
                return;
            }

            _animate = value;
            _owner.OnAnimateChanged();
        }
    }

    private bool ShouldSerializeAnimate() => !_animate;

    private void ResetAnimate() => Animate = true;

    /// <summary>
    /// Gets or sets how quickly the indicator eases toward <see cref="Value"/>, in milliseconds. Zero snaps immediately.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Milliseconds the indicator takes to ease toward Value. Zero snaps.")]
    [DefaultValue(DefaultAnimationMilliseconds)]
    public int AnimationMilliseconds
    {
        get => _animationMilliseconds;
        set => _animationMilliseconds = value < 0 ? 0 : value;
    }

    private bool ShouldSerializeAnimationMilliseconds() => _animationMilliseconds != DefaultAnimationMilliseconds;

    private void ResetAnimationMilliseconds() => AnimationMilliseconds = DefaultAnimationMilliseconds;

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
    /// Gets or sets optional caption text drawn on the face.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Optional caption drawn on the face.")]
    [DefaultValue("")]
    [Localizable(true)]
    public string Caption
    {
        get => _caption;
        set
        {
            value ??= string.Empty;
            if (_caption == value)
            {
                return;
            }

            _caption = value;
            _owner.PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeCaption() => !string.IsNullOrEmpty(_caption);

    private void ResetCaption() => Caption = string.Empty;

    /// <summary>
    /// Gets or sets optional units text drawn near the value readout.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Optional units text drawn near the value.")]
    [DefaultValue("")]
    [Localizable(true)]
    public string Units
    {
        get => _units;
        set
        {
            value ??= string.Empty;
            if (_units == value)
            {
                return;
            }

            _units = value;
            _owner.PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeUnits() => !string.IsNullOrEmpty(_units);

    private void ResetUnits() => Units = string.Empty;

    /// <summary>
    /// Gets or sets the amount changed by arrow keys while interactive.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Amount changed by arrow keys while InputMode is Interactive.")]
    [DefaultValue(DefaultSmallChange)]
    public double SmallChange
    {
        get => _smallChange;
        set => _smallChange = value < 0d ? 0d : value;
    }

    private bool ShouldSerializeSmallChange() => _smallChange != DefaultSmallChange;

    private void ResetSmallChange() => SmallChange = DefaultSmallChange;

    /// <summary>
    /// Gets or sets the amount changed by Page Up / Page Down while interactive.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Amount changed by Page Up / Page Down while InputMode is Interactive.")]
    [DefaultValue(DefaultLargeChange)]
    public double LargeChange
    {
        get => _largeChange;
        set => _largeChange = value < 0d ? 0d : value;
    }

    private bool ShouldSerializeLargeChange() => _largeChange != DefaultLargeChange;

    private void ResetLargeChange() => LargeChange = DefaultLargeChange;

    #endregion

    #region Internal

    internal void OnRangesChanged() => _owner.PerformNeedPaint(true);

    internal double Clamp(double value)
    {
        if (value < _minimum)
        {
            return _minimum;
        }

        return value > _maximum ? _maximum : value;
    }

    internal double Normalize(double value)
    {
        double span = _maximum - _minimum;
        if (Math.Abs(span) < double.Epsilon)
        {
            return 0d;
        }

        return (Clamp(value) - _minimum) / span;
    }

    #endregion

    #region Implementation

    private void SetMinimum(double value)
    {
        if (value > _maximum)
        {
            _maximum = value;
        }

        if (_minimum == value)
        {
            SetValue(_value);
            return;
        }

        _minimum = value;
        SetValue(_value);
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
            SetValue(_value);
            return;
        }

        _maximum = value;
        SetValue(_value);
        _owner.PerformNeedPaint(true);
    }

    private void SetValue(double value)
    {
        double clamped = Clamp(value);
        if (Math.Abs(clamped - _value) < 0.0001d)
        {
            return;
        }

        double old = _value;
        _value = clamped;
        _owner.OnTargetChanged(old, _value);
    }

    #endregion
}
