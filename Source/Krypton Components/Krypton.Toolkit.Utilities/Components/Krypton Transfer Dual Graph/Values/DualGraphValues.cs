#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Behaviour and appearance values for <see cref="KryptonTransferDualGraph"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class DualGraphValues : Storage
{
    private const int DefaultCapacityValue = 120;
    private const string DefaultSpeedLabelFormatValue = "Speed: {0}";
    private const string DefaultProgressLabelFormatValue = "Progress: {0}";
    private const TransferGraphSampleDisplay DefaultProgressDisplayValue = TransferGraphSampleDisplay.Percent;

    private readonly KryptonTransferDualGraph _owner;
    private int _capacity;
    private bool _paused;
    private bool _showGrid;
    private bool _showLabels;
    private bool _showProgressSeries;
    private string _speedLabelFormat;
    private string _progressLabelFormat;
    private TransferGraphSampleDisplay _progressSampleDisplay;

    /// <summary>
    /// Initialize a new instance of the <see cref="DualGraphValues"/> class.
    /// </summary>
    /// <param name="owner">Owning dual graph.</param>
    public DualGraphValues(KryptonTransferDualGraph owner)
    {
        ThrowHelper.ThrowIfNull(owner);
        _owner = owner!;
        _capacity = DefaultCapacityValue;
        _paused = false;
        _showGrid = true;
        _showLabels = true;
        _showProgressSeries = true;
        _speedLabelFormat = DefaultSpeedLabelFormatValue;
        _progressLabelFormat = DefaultProgressLabelFormatValue;
        _progressSampleDisplay = DefaultProgressDisplayValue;
    }

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        Capacity == DefaultCapacityValue &&
        !Paused &&
        ShowGrid &&
        ShowLabels &&
        ShowProgressSeries &&
        SpeedLabelFormat == DefaultSpeedLabelFormatValue &&
        ProgressLabelFormat == DefaultProgressLabelFormatValue &&
        ProgressSampleDisplay == DefaultProgressDisplayValue;

    /// <summary>Gets or sets how many samples each series keeps.</summary>
    [Category(@"Behavior")]
    [DefaultValue(DefaultCapacityValue)]
    [Description(@"Number of samples kept in the scrolling window for each series.")]
    public int Capacity
    {
        get => _capacity;
        set
        {
            if (value < 2)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(nameof(value), value, "Capacity must be at least 2.");
            }

            if (_capacity == value)
            {
                return;
            }

            _capacity = value;
            _owner.OnDualGraphValuesChanged();
        }
    }

    /// <summary>Gets or sets whether new samples are ignored.</summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description(@"When true, AddSpeedSample and AddProgressSample are ignored.")]
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
            _owner.OnDualGraphValuesChanged();
        }
    }

    /// <summary>Gets or sets whether grid lines are drawn.</summary>
    [Category(@"Appearance")]
    [DefaultValue(true)]
    [Description(@"Shows horizontal and vertical grid lines behind the series.")]
    public bool ShowGrid
    {
        get => _showGrid;
        set
        {
            if (_showGrid == value)
            {
                return;
            }

            _showGrid = value;
            _owner.OnDualGraphValuesChanged();
        }
    }

    /// <summary>Gets or sets whether overlay labels are drawn.</summary>
    [Category(@"Appearance")]
    [DefaultValue(true)]
    [Description(@"Shows speed and progress labels over the chart.")]
    public bool ShowLabels
    {
        get => _showLabels;
        set
        {
            if (_showLabels == value)
            {
                return;
            }

            _showLabels = value;
            _owner.OnDualGraphValuesChanged();
        }
    }

    /// <summary>Gets or sets whether the progress series is painted.</summary>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    [Description(@"When false, only the speed series is shown.")]
    public bool ShowProgressSeries
    {
        get => _showProgressSeries;
        set
        {
            if (_showProgressSeries == value)
            {
                return;
            }

            _showProgressSeries = value;
            _owner.OnDualGraphValuesChanged();
        }
    }

    /// <summary>Gets or sets the speed overlay format.</summary>
    [Category(@"Appearance")]
    [DefaultValue(DefaultSpeedLabelFormatValue)]
    [Localizable(true)]
    [Description(@"Format for the speed overlay. {0}=current, {1}=peak, {2}=trough.")]
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
            _owner.OnDualGraphValuesChanged();
        }
    }

    private bool ShouldSerializeSpeedLabelFormat() => _speedLabelFormat != DefaultSpeedLabelFormatValue;

    private void ResetSpeedLabelFormat() => SpeedLabelFormat = DefaultSpeedLabelFormatValue;

    /// <summary>Gets or sets the progress overlay format.</summary>
    [Category(@"Appearance")]
    [DefaultValue(DefaultProgressLabelFormatValue)]
    [Localizable(true)]
    [Description(@"Format for the progress overlay. {0}=current, {1}=peak, {2}=trough.")]
    public string ProgressLabelFormat
    {
        get => _progressLabelFormat;
        set
        {
            string text = value ?? DefaultProgressLabelFormatValue;
            if (_progressLabelFormat == text)
            {
                return;
            }

            _progressLabelFormat = text;
            _owner.OnDualGraphValuesChanged();
        }
    }

    private bool ShouldSerializeProgressLabelFormat() => _progressLabelFormat != DefaultProgressLabelFormatValue;

    private void ResetProgressLabelFormat() => ProgressLabelFormat = DefaultProgressLabelFormatValue;

    /// <summary>Gets or sets how progress samples are formatted.</summary>
    [Category(@"Appearance")]
    [DefaultValue(DefaultProgressDisplayValue)]
    [Description(@"Formats progress samples as percent or absolute bytes.")]
    public TransferGraphSampleDisplay ProgressSampleDisplay
    {
        get => _progressSampleDisplay;
        set
        {
            if (value == TransferGraphSampleDisplay.Rate)
            {
                value = TransferGraphSampleDisplay.Percent;
            }

            if (_progressSampleDisplay == value)
            {
                return;
            }

            _progressSampleDisplay = value;
            _owner.OnDualGraphValuesChanged();
        }
    }

    /// <summary>Restores every value to its factory default.</summary>
    public void Reset()
    {
        Capacity = DefaultCapacityValue;
        Paused = false;
        ShowGrid = true;
        ShowLabels = true;
        ShowProgressSeries = true;
        SpeedLabelFormat = DefaultSpeedLabelFormatValue;
        ProgressLabelFormat = DefaultProgressLabelFormatValue;
        ProgressSampleDisplay = DefaultProgressDisplayValue;
    }
}
