#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for <see cref="KryptonToggleSwitch"/> knob pulse animation settings.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ToggleSwitchPulseValues : GlobalId, INotifyPropertyChanged
{
    #region Instance Fields

    private bool _enable;
    private float _speed;
    private float _intensity;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="ToggleSwitchPulseValues"/> class.</summary>
    public ToggleSwitchPulseValues() => Reset();

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticVariables.DEFAULT_EMPTY_STRING;

    #endregion

    #region Event

    /// <summary>Occurs when a property value changes.</summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    #endregion

    #region Public

    /// <summary>Gets or sets whether the knob should pulse while the control is enabled and visible.</summary>
    [Category("Appearance")]
    [Description("Indicates whether the knob should pulse while the control is enabled and visible.")]
    [DefaultValue(false)]
    public bool Enable
    {
        get => _enable;
        set
        {
            if (_enable != value)
            {
                _enable = value;
                OnPropertyChanged(nameof(Enable));
            }
        }
    }

    /// <summary>Gets or sets the knob pulse animation speed multiplier.</summary>
    [Category("Appearance")]
    [Description("Knob pulse animation speed multiplier. 1 is the default speed; values greater than 1 animate faster and values less than 1 animate slower.")]
    [DefaultValue(1f)]
    public float Speed
    {
        get => _speed;
        set
        {
            float speed = Math.Max(0.1f, Math.Min(10f, value));
            if (Math.Abs(_speed - speed) > float.Epsilon)
            {
                _speed = speed;
                OnPropertyChanged(nameof(Speed));
            }
        }
    }

    /// <summary>Gets or sets the knob pulse intensity.</summary>
    [Category("Appearance")]
    [Description("Specifies how strong the knob pulse effect is. 0 is the minimum intensity; 1 is the maximum intensity.")]
    [DefaultValue(0.5f)]
    public float Intensity
    {
        get => _intensity;
        set
        {
            float intensity = Math.Max(0f, Math.Min(1f, value));
            if (Math.Abs(_intensity - intensity) > float.Epsilon)
            {
                _intensity = intensity;
                OnPropertyChanged(nameof(Intensity));
            }
        }
    }

    #endregion

    #region IsDefault

    /// <summary>Gets a value indicating whether this instance is default.</summary>
    [Browsable(false)]
    public bool IsDefault =>
        !_enable &&
        Math.Abs(_speed - 1f) < float.Epsilon &&
        Math.Abs(_intensity - 0.5f) < float.Epsilon;

    #endregion

    #region Reset

    /// <summary>Resets the values.</summary>
    public void Reset()
    {
        Enable = false;
        Speed = 1f;
        Intensity = 0.5f;
    }

    #endregion

    #region Implementation

    /// <summary>Called when a property value changes.</summary>
    /// <param name="propertyName">Name of the property.</param>
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion
}
