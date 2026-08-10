#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Radial menu item that edits a numeric value with an arc slider when activated.
/// </summary>
[ToolboxItem(false)]
[DesignTimeVisible(false)]
[DesignerCategory(@"code")]
[DefaultProperty(nameof(Value))]
[DefaultEvent(nameof(ValueChanged))]
public class KryptonRadialMenuSliderItem : KryptonRadialMenuItemBase
{
    #region Instance Fields

    private string _text;
    private decimal _minimum;
    private decimal _maximum;
    private decimal _value;
    private decimal _smallChange;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when <see cref="Value"/> changes.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the Value property changes.")]
    public event EventHandler? ValueChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRadialMenuSliderItem"/> class.
    /// </summary>
    public KryptonRadialMenuSliderItem()
    {
        _text = @"Slider";
        _minimum = 0m;
        _maximum = 100m;
        _value = 50m;
        _smallChange = 1m;
    }

    /// <inheritdoc />
    public override string ToString() => (string.IsNullOrEmpty(Text) ? "(Radial Slider)" : Text)!;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the label text for the sector.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Text displayed on the slider sector.")]
    [DefaultValue(@"Slider")]
    [Localizable(true)]
    public string? Text
    {
        get => _text;
        set
        {
            value ??= string.Empty;
            if (_text != value)
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
    }

    /// <summary>
    /// Gets or sets the minimum value.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Minimum slider value.")]
    [DefaultValue(typeof(decimal), "0")]
    public decimal Minimum
    {
        get => _minimum;
        set
        {
            if (_minimum != value)
            {
                _minimum = value;
                if (_maximum < _minimum)
                {
                    _maximum = _minimum;
                }

                Value = Math.Max(_minimum, Math.Min(_maximum, _value));
                OnPropertyChanged(nameof(Minimum));
            }
        }
    }

    /// <summary>
    /// Gets or sets the maximum value.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Maximum slider value.")]
    [DefaultValue(typeof(decimal), "100")]
    public decimal Maximum
    {
        get => _maximum;
        set
        {
            if (_maximum != value)
            {
                _maximum = value;
                if (_minimum > _maximum)
                {
                    _minimum = _maximum;
                }

                Value = Math.Max(_minimum, Math.Min(_maximum, _value));
                OnPropertyChanged(nameof(Maximum));
            }
        }
    }

    /// <summary>
    /// Gets or sets the current value.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Current slider value.")]
    [DefaultValue(typeof(decimal), "50")]
    public decimal Value
    {
        get => _value;
        set
        {
            var clamped = Math.Max(_minimum, Math.Min(_maximum, value));
            if (_value != clamped)
            {
                _value = clamped;
                OnPropertyChanged(nameof(Value));
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Gets or sets the small change amount.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Amount the value changes for small increments.")]
    [DefaultValue(typeof(decimal), "1")]
    public decimal SmallChange
    {
        get => _smallChange;
        set
        {
            if (_smallChange != value && value > 0m)
            {
                _smallChange = value;
                OnPropertyChanged(nameof(SmallChange));
            }
        }
    }

    /// <inheritdoc />
    [Browsable(false)]
    public override bool HasChildren => true;

    /// <summary>
    /// Gets the normalised value in the range 0..1.
    /// </summary>
    /// <returns>Normalised value.</returns>
    public float GetNormalizedValue()
    {
        var span = _maximum - _minimum;
        if (span <= 0m)
        {
            return 0f;
        }

        return (float)((_value - _minimum) / span);
    }

    /// <summary>
    /// Sets the value from a normalised 0..1 fraction.
    /// </summary>
    /// <param name="normalized">Normalised value.</param>
    public void SetNormalizedValue(float normalized)
    {
        normalized = Math.Max(0f, Math.Min(1f, normalized));
        Value = _minimum + (_maximum - _minimum) * (decimal)normalized;
    }

    #endregion
}
