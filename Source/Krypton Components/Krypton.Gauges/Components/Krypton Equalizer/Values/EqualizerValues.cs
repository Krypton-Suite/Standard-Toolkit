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
/// Band, input, and interaction settings for a <see cref="KryptonEqualizer"/>.
/// Face colours are <see cref="KryptonEqualizer.StateCommon"/>, <see cref="KryptonEqualizer.StateNormal"/>, and <see cref="KryptonEqualizer.StateDisabled"/>.
/// </summary>
public class EqualizerValues : Storage
{
    #region Instance Fields

    private const int DefaultAttack = 120;
    private const int DefaultRelease = 280;
    private const int DefaultPeakHold = 900;
    private const int DefaultPeakDecay = 1600;

    private readonly KryptonEqualizer _owner;
    private readonly List<EqualizerBand> _bands = new List<EqualizerBand>();
    private EqualizerStyle _style = EqualizerStyle.Bars;
    private EqualizerInputMode _inputMode = EqualizerInputMode.Manual;
    private VuMeterChannel _channel = VuMeterChannel.Mix;
    private VisualOrientation _orientation = VisualOrientation.Top;
    private int _bandCount = EqualizerScale.DefaultBandCount;
    private double _minimumGainDb = EqualizerScale.DefaultMinimumGainDb;
    private double _maximumGainDb = EqualizerScale.DefaultMaximumGainDb;
    private double _calibrationOffsetDb = EqualizerScale.DefaultCalibrationOffsetDb;
    private bool _interactive = true;
    private bool _showBandLabels = true;
    private bool _peakHold;
    private int _attackMilliseconds = DefaultAttack;
    private int _releaseMilliseconds = DefaultRelease;
    private int _peakHoldMilliseconds = DefaultPeakHold;
    private int _peakDecayMilliseconds = DefaultPeakDecay;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="EqualizerValues"/> class.
    /// </summary>
    /// <param name="owner">Owning equalizer.</param>
    public EqualizerValues(KryptonEqualizer owner)
    {
        _owner = owner;
        Bands = new EqualizerBandCollection(this);
        RebuildBands(_bandCount, preserveGains: false);
    }

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    public override bool IsDefault =>
        _style == EqualizerStyle.Bars &&
        _inputMode == EqualizerInputMode.Manual &&
        _channel == VuMeterChannel.Mix &&
        _orientation == VisualOrientation.Top &&
        _bandCount == EqualizerScale.DefaultBandCount &&
        _minimumGainDb == EqualizerScale.DefaultMinimumGainDb &&
        _maximumGainDb == EqualizerScale.DefaultMaximumGainDb &&
        _calibrationOffsetDb == EqualizerScale.DefaultCalibrationOffsetDb &&
        _interactive &&
        _showBandLabels &&
        !_peakHold &&
        _attackMilliseconds == DefaultAttack &&
        _releaseMilliseconds == DefaultRelease &&
        _peakHoldMilliseconds == DefaultPeakHold &&
        _peakDecayMilliseconds == DefaultPeakDecay &&
        BandsAreDefault();

    /// <summary>
    /// Restores every setting to the factory default.
    /// </summary>
    public void Reset()
    {
        Style = EqualizerStyle.Bars;
        InputMode = EqualizerInputMode.Manual;
        Channel = VuMeterChannel.Mix;
        Orientation = VisualOrientation.Top;
        MinimumGainDb = EqualizerScale.DefaultMinimumGainDb;
        MaximumGainDb = EqualizerScale.DefaultMaximumGainDb;
        CalibrationOffsetDb = EqualizerScale.DefaultCalibrationOffsetDb;
        Interactive = true;
        ShowBandLabels = true;
        PeakHold = false;
        AttackMilliseconds = DefaultAttack;
        ReleaseMilliseconds = DefaultRelease;
        PeakHoldMilliseconds = DefaultPeakHold;
        PeakDecayMilliseconds = DefaultPeakDecay;
        BandCount = EqualizerScale.DefaultBandCount;
        RebuildBands(_bandCount, preserveGains: false);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the band collection. Count is controlled by <see cref="BandCount"/>.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Spectrum and gain bands.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public EqualizerBandCollection Bands { get; }

    private bool ShouldSerializeBands() => !BandsAreDefault();

    /// <summary>
    /// Gets or sets the painted face.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Solid bars or segmented LED-style bars.")]
    [DefaultValue(EqualizerStyle.Bars)]
    public EqualizerStyle Style
    {
        get => _style;
        set
        {
            if (_style == value)
            {
                return;
            }

            _style = value;
            _owner.PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeStyle() => _style != EqualizerStyle.Bars;

    private void ResetStyle() => Style = EqualizerStyle.Bars;

    /// <summary>
    /// Gets or sets whether band levels come from the application or the default recording device.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Manual uses SetLevels or Band.Level. Capture reads the default recording device.")]
    [DefaultValue(EqualizerInputMode.Manual)]
    public EqualizerInputMode InputMode
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

    private bool ShouldSerializeInputMode() => _inputMode != EqualizerInputMode.Manual;

    private void ResetInputMode() => InputMode = EqualizerInputMode.Manual;

    /// <summary>
    /// Gets or sets the recording channel used while <see cref="InputMode"/> is <see cref="EqualizerInputMode.Capture"/>.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Recording channel used while InputMode is Capture.")]
    [DefaultValue(VuMeterChannel.Mix)]
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

    private bool ShouldSerializeChannel() => _channel != VuMeterChannel.Mix;

    private void ResetChannel() => Channel = VuMeterChannel.Mix;

    /// <summary>
    /// Gets or sets how bars are laid out. Top and Bottom are vertical; Left and Right are horizontal.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Bar growth direction. Top and Bottom are vertical. Left and Right are horizontal.")]
    [DefaultValue(VisualOrientation.Top)]
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

    private bool ShouldSerializeOrientation() => _orientation != VisualOrientation.Top;

    private void ResetOrientation() => Orientation = VisualOrientation.Top;

    /// <summary>
    /// Gets or sets the number of frequency bands (3…31). Default is 10.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Number of frequency bands. Default 10.")]
    [DefaultValue(EqualizerScale.DefaultBandCount)]
    public int BandCount
    {
        get => _bandCount;
        set
        {
            int clamped = EqualizerScale.ClampBandCount(value);
            if (_bandCount == clamped)
            {
                return;
            }

            _bandCount = clamped;
            RebuildBands(_bandCount, preserveGains: true);
            _owner.OnBandCountChanged();
        }
    }

    private bool ShouldSerializeBandCount() => _bandCount != EqualizerScale.DefaultBandCount;

    private void ResetBandCount() => BandCount = EqualizerScale.DefaultBandCount;

    /// <summary>
    /// Gets or sets the lowest gain thumb position in decibels.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Lowest interactive gain in decibels.")]
    public double MinimumGainDb
    {
        get => _minimumGainDb;
        set
        {
            if (value > _maximumGainDb)
            {
                _maximumGainDb = value;
            }

            if (Math.Abs(_minimumGainDb - value) < 0.0001d)
            {
                ClampAllGains();
                return;
            }

            _minimumGainDb = value;
            ClampAllGains();
            _owner.PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeMinimumGainDb() => _minimumGainDb != EqualizerScale.DefaultMinimumGainDb;

    private void ResetMinimumGainDb() => MinimumGainDb = EqualizerScale.DefaultMinimumGainDb;

    /// <summary>
    /// Gets or sets the highest gain thumb position in decibels.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Highest interactive gain in decibels.")]
    public double MaximumGainDb
    {
        get => _maximumGainDb;
        set
        {
            if (value < _minimumGainDb)
            {
                _minimumGainDb = value;
            }

            if (Math.Abs(_maximumGainDb - value) < 0.0001d)
            {
                ClampAllGains();
                return;
            }

            _maximumGainDb = value;
            ClampAllGains();
            _owner.PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeMaximumGainDb() => _maximumGainDb != EqualizerScale.DefaultMaximumGainDb;

    private void ResetMaximumGainDb() => MaximumGainDb = EqualizerScale.DefaultMaximumGainDb;

    /// <summary>
    /// Gets or sets the digital alignment used while capturing. Full-scale maps near this many dBFS.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"dBFS alignment used while capturing. Default -18.")]
    public double CalibrationOffsetDb
    {
        get => _calibrationOffsetDb;
        set
        {
            if (Math.Abs(_calibrationOffsetDb - value) < 0.0001d)
            {
                return;
            }

            _calibrationOffsetDb = value;
        }
    }

    private bool ShouldSerializeCalibrationOffsetDb() =>
        _calibrationOffsetDb != EqualizerScale.DefaultCalibrationOffsetDb;

    private void ResetCalibrationOffsetDb() => CalibrationOffsetDb = EqualizerScale.DefaultCalibrationOffsetDb;

    /// <summary>
    /// Gets or sets whether the user can drag band thumbs to change <see cref="EqualizerBand.Gain"/>.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Allow dragging gain thumbs with the mouse or keyboard.")]
    [DefaultValue(true)]
    public bool Interactive
    {
        get => _interactive;
        set
        {
            if (_interactive == value)
            {
                return;
            }

            _interactive = value;
            _owner.OnInteractiveChanged();
        }
    }

    private bool ShouldSerializeInteractive() => !_interactive;

    private void ResetInteractive() => Interactive = true;

    /// <summary>
    /// Gets or sets whether frequency labels are drawn under the bars.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Draw frequency labels under the bars.")]
    [DefaultValue(true)]
    public bool ShowBandLabels
    {
        get => _showBandLabels;
        set
        {
            if (_showBandLabels == value)
            {
                return;
            }

            _showBandLabels = value;
            _owner.PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeShowBandLabels() => !_showBandLabels;

    private void ResetShowBandLabels() => ShowBandLabels = true;

    /// <summary>
    /// Gets or sets whether a peak indicator is drawn on each band.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Draw a peak mark that holds, then falls back.")]
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
    /// Gets or sets how quickly bars rise, in milliseconds.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Milliseconds bars take to rise. Zero snaps.")]
    [DefaultValue(DefaultAttack)]
    public int AttackMilliseconds
    {
        get => _attackMilliseconds;
        set => _attackMilliseconds = value < 0 ? 0 : value;
    }

    private bool ShouldSerializeAttackMilliseconds() => _attackMilliseconds != DefaultAttack;

    private void ResetAttackMilliseconds() => AttackMilliseconds = DefaultAttack;

    /// <summary>
    /// Gets or sets how quickly bars fall, in milliseconds.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Milliseconds bars take to fall. Zero snaps.")]
    [DefaultValue(DefaultRelease)]
    public int ReleaseMilliseconds
    {
        get => _releaseMilliseconds;
        set => _releaseMilliseconds = value < 0 ? 0 : value;
    }

    private bool ShouldSerializeReleaseMilliseconds() => _releaseMilliseconds != DefaultRelease;

    private void ResetReleaseMilliseconds() => ReleaseMilliseconds = DefaultRelease;

    /// <summary>
    /// Gets or sets how long a peak is held before it falls, in milliseconds.
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
    /// Gets or sets how quickly a held peak falls, in milliseconds.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Milliseconds a held peak takes to fall.")]
    [DefaultValue(DefaultPeakDecay)]
    public int PeakDecayMilliseconds
    {
        get => _peakDecayMilliseconds;
        set => _peakDecayMilliseconds = value < 0 ? 0 : value;
    }

    private bool ShouldSerializePeakDecayMilliseconds() => _peakDecayMilliseconds != DefaultPeakDecay;

    private void ResetPeakDecayMilliseconds() => PeakDecayMilliseconds = DefaultPeakDecay;

    #endregion

    #region Internal

    internal List<EqualizerBand> BandList => _bands;

    internal void SetBandLevel(EqualizerBand band, double level, bool fromCapture)
    {
        if (!fromCapture && _inputMode == EqualizerInputMode.Capture && !_owner.IsApplyingCapture)
        {
            return;
        }

        double clamped = EqualizerScale.ClampLevel(level);
        if (Math.Abs(band.Level - clamped) < 0.0005d && fromCapture)
        {
            band.SetLevelInternal(clamped);
            return;
        }

        if (Math.Abs(band.Level - clamped) < 0.0005d)
        {
            return;
        }

        band.SetLevelInternal(clamped);
        _owner.OnBandLevelChanged(band);
    }

    internal void SetBandGain(EqualizerBand band, double gain)
    {
        double clamped = EqualizerScale.ClampGain(gain, _minimumGainDb, _maximumGainDb);
        if (Math.Abs(band.Gain - clamped) < 0.01d)
        {
            return;
        }

        double previous = band.Gain;
        band.SetGainInternal(clamped);
        _owner.OnBandGainChanged(band, previous);
    }

    internal void NotifyBandChanged(EqualizerBand band, EqualizerBandChange change)
    {
        if (change == EqualizerBandChange.Text || change == EqualizerBandChange.Frequency)
        {
            _owner.PerformNeedPaint(true);
        }
    }

    internal void ApplyCapturedLevels(double[] levels)
    {
        int count = Math.Min(levels.Length, _bands.Count);
        for (int i = 0; i < count; i++)
        {
            SetBandLevel(_bands[i], levels[i], fromCapture: true);
        }

        _owner.OnLevelsCaptured();
    }

    internal double[] CopyFrequencies()
    {
        var frequencies = new double[_bands.Count];
        for (int i = 0; i < _bands.Count; i++)
        {
            frequencies[i] = _bands[i].FrequencyHz;
        }

        return frequencies;
    }

    #endregion

    #region Implementation

    private bool BandsAreDefault()
    {
        if (_bands.Count != EqualizerScale.DefaultBandCount)
        {
            return false;
        }

        double[] expected = EqualizerScale.CreateFrequencies(EqualizerScale.DefaultBandCount);
        for (int i = 0; i < _bands.Count; i++)
        {
            if (!_bands[i].IsDefault || Math.Abs(_bands[i].FrequencyHz - expected[i]) > 0.5d)
            {
                return false;
            }
        }

        return true;
    }

    private void RebuildBands(int count, bool preserveGains)
    {
        double[] frequencies = EqualizerScale.CreateFrequencies(count);
        var gains = preserveGains ? new double[Math.Min(count, _bands.Count)] : Array.Empty<double>();
        for (int i = 0; i < gains.Length; i++)
        {
            gains[i] = _bands[i].Gain;
        }

        while (_bands.Count > count)
        {
            _bands.RemoveAt(_bands.Count - 1);
        }

        for (int i = 0; i < count; i++)
        {
            if (i < _bands.Count)
            {
                _bands[i].ResetForRebuild(frequencies[i]);
                if (i < gains.Length)
                {
                    _bands[i].SetGainInternal(gains[i]);
                }
            }
            else
            {
                _bands.Add(new EqualizerBand(this, frequencies[i]));
            }
        }
    }

    private void ClampAllGains()
    {
        for (int i = 0; i < _bands.Count; i++)
        {
            _bands[i].SetGainInternal(_bands[i].Gain);
        }
    }

    #endregion
}
