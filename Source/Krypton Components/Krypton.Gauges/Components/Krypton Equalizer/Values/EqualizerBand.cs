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
/// One frequency band of a <see cref="KryptonEqualizer"/>: spectrum <see cref="Level"/> and interactive <see cref="Gain"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class EqualizerBand : Storage
{
    private readonly EqualizerValues _owner;
    private double _frequencyHz;
    private string _text = string.Empty;
    private double _level;
    private double _gain;

    /// <summary>
    /// Initialize a new instance of the <see cref="EqualizerBand"/> class.
    /// </summary>
    /// <param name="owner">Owning values bag.</param>
    /// <param name="frequencyHz">Centre frequency in hertz.</param>
    internal EqualizerBand(EqualizerValues owner, double frequencyHz)
    {
        _owner = owner;
        _frequencyHz = frequencyHz;
    }

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        Math.Abs(_level) < 0.0001d &&
        Math.Abs(_gain) < 0.0001d &&
        string.IsNullOrEmpty(_text);

    /// <summary>
    /// Gets or sets the centre frequency of this band in hertz.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Centre frequency of this band in hertz.")]
    public double FrequencyHz
    {
        get => _frequencyHz;
        set
        {
            if (value <= 0d || Math.Abs(_frequencyHz - value) < 0.0001d)
            {
                return;
            }

            _frequencyHz = value;
            _owner.NotifyBandChanged(this, EqualizerBandChange.Frequency);
        }
    }

    private bool ShouldSerializeFrequencyHz() => false;

    private void ResetFrequencyHz()
    {
        // Frequencies are rebuilt with BandCount; individual resets are not designer defaults.
    }

    /// <summary>
    /// Gets or sets an optional label. When empty, the control formats <see cref="FrequencyHz"/>.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Optional band label. Empty uses a formatted frequency.")]
    [DefaultValue("")]
    public string Text
    {
        get => _text;
        set
        {
            string next = value ?? string.Empty;
            if (_text == next)
            {
                return;
            }

            _text = next;
            _owner.NotifyBandChanged(this, EqualizerBandChange.Text);
        }
    }

    private bool ShouldSerializeText() => !string.IsNullOrEmpty(_text);

    private void ResetText() => Text = string.Empty;

    /// <summary>
    /// Gets or sets the normalized spectrum level (0…1) drawn as the bar height.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Normalized spectrum level from 0 to 1.")]
    [DefaultValue(0d)]
    public double Level
    {
        get => _level;
        set => _owner.SetBandLevel(this, value, fromCapture: false);
    }

    private bool ShouldSerializeLevel() => Math.Abs(_level) >= 0.0001d;

    private void ResetLevel() => Level = 0d;

    /// <summary>
    /// Gets or sets the graphic-EQ gain in decibels.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Graphic equalizer gain in decibels.")]
    [DefaultValue(0d)]
    public double Gain
    {
        get => _gain;
        set => _owner.SetBandGain(this, value);
    }

    private bool ShouldSerializeGain() => Math.Abs(_gain) >= 0.0001d;

    private void ResetGain() => Gain = 0d;

    /// <summary>
    /// Gets the display text for this band.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string DisplayText => string.IsNullOrEmpty(_text)
        ? EqualizerScale.FormatFrequency(_frequencyHz)
        : _text;

    internal void SetFrequencyInternal(double frequencyHz) => _frequencyHz = frequencyHz;

    internal void SetLevelInternal(double level) => _level = EqualizerScale.ClampLevel(level);

    internal void SetGainInternal(double gain) =>
        _gain = EqualizerScale.ClampGain(gain, _owner.MinimumGainDb, _owner.MaximumGainDb);

    internal void ResetForRebuild(double frequencyHz)
    {
        _frequencyHz = frequencyHz;
        _text = string.Empty;
        _level = 0d;
        _gain = 0d;
    }
}

/// <summary>
/// Which band property changed.
/// </summary>
internal enum EqualizerBandChange
{
    Frequency,
    Text,
    Level,
    Gain
}
