#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class ToggleSwitchValues : GlobalId, INotifyPropertyChanged
{
    #region Instance Fields

    private bool _enableEmbossEffect;
    private bool _animateGradientEffect;
    private bool _enableKnobGradient;
    private bool _onlyShowColorOnKnob;
    private bool _showText;
    private float _gradientStartIntensity;
    private float _gradientEndIntensity;
    private LinearGradientMode _gradientDirection;
    private Color _onColor;
    private Color _offColor;
    private int _cornerRadius;
    private bool _useThemeColors;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="ToggleSwitchValues" /> class.</summary>
    public ToggleSwitchValues()
    {
        _enableEmbossEffect = false;
        _animateGradientEffect = false;
        _enableKnobGradient = false;
        _onlyShowColorOnKnob = true;
        _showText = true;
        _gradientStartIntensity = 0.8f;
        _gradientEndIntensity = 0.6f;
        _gradientDirection = LinearGradientMode.ForwardDiagonal;
        _onColor = Color.Green;
        _offColor = Color.Red;
        _cornerRadius = 10;
        _useThemeColors = true;
    }

    #endregion

    #region Event

    /// <summary>Occurs when a property value changes.</summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    #endregion

    #region Event Handler

    /// <summary>Called when [property changed].</summary>
    /// <param name="propertyName">Name of the property.</param>
    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion

    #region Public

    /// <summary>Gets or sets a value indicating whether [enable emboss effect].</summary>
    /// <value><c>true</c> if [enable emboss effect]; otherwise, <c>false</c>.</value>
    [Category("Appearance")]
    [Description("Indicates whether the emboss effect should be applied.")]
    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool EnableEmbossEffect
    {
        get => _enableEmbossEffect;
        set
        {
            if (_enableEmbossEffect != value)
            {
                _enableEmbossEffect = value;
                OnPropertyChanged(nameof(EnableEmbossEffect));
            }
        }
    }

    [Category("Appearance")]
    [Description("Indicates whether the gradient effect should be animated.")]
    [DefaultValue(false)]
    public bool AnimateGradientEffect
    {
        get => _animateGradientEffect;
        set
        {
            if (_animateGradientEffect != value)
            {
                _animateGradientEffect = value;
                OnPropertyChanged(nameof(AnimateGradientEffect));
            }
        }
    }

    /// <summary>Gets or sets a value indicating whether [enable knob gradient].</summary>
    /// <value><c>true</c> if [enable knob gradient]; otherwise, <c>false</c>.</value>
    [Category("Appearance")]
    [Description("Indicates whether the knob should have a gradient effect.")]
    [DefaultValue(false)]
    public bool EnableKnobGradient
    {
        get => _enableKnobGradient;
        set
        {
            if (_enableKnobGradient != value)
            {
                _enableKnobGradient = value;
                OnPropertyChanged(nameof(EnableKnobGradient));
            }
        }
    }

    /// <summary>Gets or sets a value indicating whether [show color only on knob].</summary>
    [Category("Appearance")]
    [Description("Indicates whether the color should be only shown on the knob.")]
    [DefaultValue(true)]
    public bool OnlyShowColorOnKnob
    {
        get => _onlyShowColorOnKnob;
        set
        {
            if (_onlyShowColorOnKnob != value)
            {
                _onlyShowColorOnKnob = value;
                OnPropertyChanged(nameof(OnlyShowColorOnKnob));
            }
        }
    }

    /// <summary>Gets or sets a value indicating whether [show text].</summary>
    [Category("Appearance")]
    [Description("Indicates whether the text should be shown.")]
    [DefaultValue(true)]
    public bool ShowText
    {
        get => _showText;
        set
        {
            if (_showText != value)
            {
                _showText = value;
                OnPropertyChanged(nameof(ShowText));
            }
        }
    }

    /// <summary>Gets or sets the gradient start intensity.</summary>
    /// <value>The gradient start intensity.</value>
    [Category("Appearance")]
    [Description("Specifies the gradient intensity for the knob.")]
    [DefaultValue(0.8f)]
    public float GradientStartIntensity
    {
        get => _gradientStartIntensity;
        set
        {
            if (_gradientStartIntensity != value)
            {
                _gradientStartIntensity = value;
                OnPropertyChanged(nameof(GradientStartIntensity));
            }
        }
    }

    /// <summary>Gets or sets the gradient end intensity.</summary>
    /// <value>The gradient end intensity.</value>
    [Category("Appearance")]
    [Description("Specifies the gradient intensity for the knob.")]
    [DefaultValue(0.6f)]
    public float GradientEndIntensity
    {
        get => _gradientEndIntensity;
        set
        {
            if (_gradientEndIntensity != value)
            {
                _gradientEndIntensity = value;
                OnPropertyChanged(nameof(GradientEndIntensity));
            }
        }
    }

    /// <summary>Gets or sets the gradient direction.</summary>
    /// <value>The gradient direction.</value>
    [Category("Appearance")]
    [Description("Specifies the direction of the gradient.")]
    [DefaultValue(LinearGradientMode.ForwardDiagonal)]
    public LinearGradientMode GradientDirection
    {
        get => _gradientDirection;
        set
        {
            if (_gradientDirection != value)
            {
                _gradientDirection = value;
                OnPropertyChanged(nameof(GradientDirection));
            }
        }
    }

    /// <summary>Gets or sets the color when on.</summary>
    /// <value>The color when on.</value>
    [Category("Appearance")]
    [Description("Specifies the color when the switch is on.")]
    [DefaultValue(typeof(Color), "Green")]
    public Color OnColor
    {
        get => _onColor;
        set
        {
            if (_onColor != value)
            {
                _onColor = value;
                OnPropertyChanged(nameof(OnColor));
            }
        }
    }

    /// <summary>Gets or sets the color when off.</summary>
    /// <value>The color when off.</value>
    [Category("Appearance")]
    [Description("Specifies the color when the switch is off.")]
    [DefaultValue(typeof(Color), "Red")]
    public Color OffColor
    {
        get => _offColor;
        set
        {
            if (_offColor != value)
            {
                _offColor = value;
                OnPropertyChanged(nameof(OffColor));
            }
        }
    }

    [Category("Appearance")]
    [Description("Specifies the corner radius of the switch.")]
    [DefaultValue(10)]
    public int CornerRadius
    {
        get => _cornerRadius;
        set
        {
            if (value < 1)
            {
                value = 1;
            }

            if (value > 130)
            {
                value = 130;
            }

            if (_cornerRadius != value)
            {
                _cornerRadius = value;
                OnPropertyChanged(nameof(CornerRadius));
            }
        }
    }

    [Category("Appearance")]
    [Description("Indicates whether to use theme colors.")]
    [DefaultValue(true)]
    public bool UseThemeColors
    {
        get => _useThemeColors;
        set
        {
            if (_useThemeColors != value)
            {
                _useThemeColors = value;
                OnPropertyChanged(nameof(UseThemeColors));
            }
        }
    }

    #endregion

    #region IsDefault

    /// <summary>Gets a value indicating whether this instance is default.</summary>
    /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
    [Browsable(false)]
    public bool IsDefault => !_enableEmbossEffect && !_enableKnobGradient && _gradientStartIntensity.Equals(0.8f) &&
                             _gradientEndIntensity.Equals(0.6f) &&
                             _gradientDirection == LinearGradientMode.ForwardDiagonal && _onColor == Color.Green &&
                             _offColor == Color.Red && _cornerRadius == 10 && _useThemeColors;

    #endregion

    #region Reset

    /// <summary>Resets the values.</summary>
    public void Reset()
    {
        EnableEmbossEffect = false;
        AnimateGradientEffect = false;
        EnableKnobGradient = false;
        OnlyShowColorOnKnob = true;
        ShowText = true;
        GradientStartIntensity = 0.8f;
        GradientEndIntensity = 0.6f;
        GradientDirection = LinearGradientMode.ForwardDiagonal;
        OnColor = Color.Green;
        OffColor = Color.Red;
        CornerRadius = 10;
        UseThemeColors = true;
    }

    #endregion

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticValues.DEFAULT_EMPTY_STRING;
}