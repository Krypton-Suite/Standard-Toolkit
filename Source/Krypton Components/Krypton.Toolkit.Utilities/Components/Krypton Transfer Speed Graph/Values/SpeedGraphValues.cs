#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Behaviour and appearance values for <see cref="KryptonTransferSpeedGraph"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class SpeedGraphValues : Storage
{
    #region Static Fields

    private const int DefaultCapacityValue = 120;
    private const string DefaultSpeedLabelFormatValue = "Speed: {0}";

    #endregion

    #region Instance Fields

    private readonly KryptonTransferSpeedGraph _owner;
    private int _capacity;
    private bool _paused;
    private bool _showSpeedLabel;
    private string _speedLabelFormat;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="SpeedGraphValues"/> class.
    /// </summary>
    /// <param name="owner">Owning transfer-speed graph.</param>
    public SpeedGraphValues(KryptonTransferSpeedGraph owner)
    {
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));
        _capacity = DefaultCapacityValue;
        _paused = false;
        _showSpeedLabel = true;
        _speedLabelFormat = DefaultSpeedLabelFormatValue;
    }

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        Capacity == DefaultCapacityValue &&
        !Paused &&
        ShowSpeedLabel &&
        SpeedLabelFormat == DefaultSpeedLabelFormatValue;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets how many samples the chart keeps.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(DefaultCapacityValue)]
    [Description(@"Number of speed samples kept in the scrolling window.")]
    public int Capacity
    {
        get => _capacity;
        set
        {
            if (value < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, "Capacity must be at least 2.");
            }

            if (_capacity == value)
            {
                return;
            }

            _capacity = value;
            _owner.OnSpeedGraphValuesChanged();
        }
    }

    /// <summary>
    /// Gets or sets whether new samples are ignored so the chart stays frozen.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description(@"When true, AddSample is ignored and the chart does not scroll.")]
    public bool Paused
    {
        get => _paused;
        set
        {
            if (_paused == value)
            {
                return;
            }

            _paused = value;
            _owner.OnSpeedGraphValuesChanged();
        }
    }

    /// <summary>
    /// Gets or sets whether the current speed is drawn in the corner.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(true)]
    [Description(@"Shows the current speed label over the chart.")]
    public bool ShowSpeedLabel
    {
        get => _showSpeedLabel;
        set
        {
            if (_showSpeedLabel == value)
            {
                return;
            }

            _showSpeedLabel = value;
            _owner.OnSpeedGraphValuesChanged();
        }
    }

    /// <summary>
    /// Gets or sets the format for the overlay label.
    /// </summary>
    /// <remarks>
    /// <c>{0}</c> is the current rate, <c>{1}</c> is the window peak, and <c>{2}</c> is the window trough
    /// (each already formatted as a rate string such as <c>1.2 MB/s</c>).
    /// </remarks>
    [Category(@"Appearance")]
    [DefaultValue(DefaultSpeedLabelFormatValue)]
    [Localizable(true)]
    [Description(@"Format string for the speed overlay. {0}=current, {1}=peak, {2}=trough rate strings.")]
    public string SpeedLabelFormat
    {
        get => _speedLabelFormat;
        set
        {
            string text = value ?? DefaultSpeedLabelFormatValue;
            if (_speedLabelFormat == text)
            {
                return;
            }

            _speedLabelFormat = text;
            _owner.OnSpeedGraphValuesChanged();
        }
    }

    private bool ShouldSerializeSpeedLabelFormat() => _speedLabelFormat != DefaultSpeedLabelFormatValue;

    private void ResetSpeedLabelFormat() => SpeedLabelFormat = DefaultSpeedLabelFormatValue;

    /// <summary>
    /// Restores every value to its factory default.
    /// </summary>
    public void Reset()
    {
        Capacity = DefaultCapacityValue;
        Paused = false;
        ShowSpeedLabel = true;
        SpeedLabelFormat = DefaultSpeedLabelFormatValue;
    }

    #endregion
}
