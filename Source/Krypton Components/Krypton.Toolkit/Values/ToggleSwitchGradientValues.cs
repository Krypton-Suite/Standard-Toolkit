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
/// Storage for <see cref="KryptonToggleSwitch"/> knob gradient settings.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ToggleSwitchGradientValues : GlobalId, INotifyPropertyChanged
{
    #region Instance Fields

    private bool _enable;
    private bool _animate;
    private float _startIntensity;
    private float _endIntensity;
    private LinearGradientMode _direction;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="ToggleSwitchGradientValues"/> class.</summary>
    public ToggleSwitchGradientValues() => Reset();

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticVariables.DEFAULT_EMPTY_STRING;

    #endregion

    #region Event

    /// <summary>Occurs when a property value changes.</summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    #endregion

    #region Public

    /// <summary>Gets or sets whether the knob should have a gradient effect.</summary>
    [Category("Appearance")]
    [Description("Indicates whether the knob should have a gradient effect. Also applies to Classic, Bevel, Ring, RoundedSquare, Square, Grip, Chevron, and Indicator styles.")]
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

    /// <summary>Gets or sets whether the gradient effect should be animated.</summary>
    [Category("Appearance")]
    [Description("Indicates whether the gradient effect should be animated.")]
    [DefaultValue(false)]
    public bool Animate
    {
        get => _animate;
        set
        {
            if (_animate != value)
            {
                _animate = value;
                OnPropertyChanged(nameof(Animate));
            }
        }
    }

    /// <summary>Gets or sets the gradient start intensity.</summary>
    [Category("Appearance")]
    [Description("Specifies the gradient intensity applied to the start color of the knob gradient.")]
    [DefaultValue(0.8f)]
    public float StartIntensity
    {
        get => _startIntensity;
        set
        {
            if (!_startIntensity.Equals(value))
            {
                _startIntensity = value;
                OnPropertyChanged(nameof(StartIntensity));
            }
        }
    }

    /// <summary>Gets or sets the gradient end intensity.</summary>
    [Category("Appearance")]
    [Description("Specifies the gradient intensity applied to the end color of the knob gradient.")]
    [DefaultValue(0.6f)]
    public float EndIntensity
    {
        get => _endIntensity;
        set
        {
            if (!_endIntensity.Equals(value))
            {
                _endIntensity = value;
                OnPropertyChanged(nameof(EndIntensity));
            }
        }
    }

    /// <summary>Gets or sets the gradient direction.</summary>
    [Category("Appearance")]
    [Description("Specifies the direction of the gradient.")]
    [DefaultValue(LinearGradientMode.ForwardDiagonal)]
    public LinearGradientMode Direction
    {
        get => _direction;
        set
        {
            if (_direction != value)
            {
                _direction = value;
                OnPropertyChanged(nameof(Direction));
            }
        }
    }

    #endregion

    #region IsDefault

    /// <summary>Gets a value indicating whether this instance is default.</summary>
    [Browsable(false)]
    public bool IsDefault =>
        !_enable &&
        !_animate &&
        _startIntensity.Equals(0.8f) &&
        _endIntensity.Equals(0.6f) &&
        _direction == LinearGradientMode.ForwardDiagonal;

    #endregion

    #region Reset

    /// <summary>Resets the values.</summary>
    public void Reset()
    {
        Enable = false;
        Animate = false;
        StartIntensity = 0.8f;
        EndIntensity = 0.6f;
        Direction = LinearGradientMode.ForwardDiagonal;
    }

    #endregion

    #region Implementation

    /// <summary>Called when a property value changes.</summary>
    /// <param name="propertyName">Name of the property.</param>
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion
}
