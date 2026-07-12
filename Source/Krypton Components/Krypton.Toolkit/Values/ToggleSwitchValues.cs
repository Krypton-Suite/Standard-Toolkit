#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class ToggleSwitchValues : GlobalId, INotifyPropertyChanged
{
    #region Instance Fields

    private bool _checked;
    private bool _enableEmbossEffect;
    private bool _animateGradientEffect;
    private bool _enableKnobGradient;
    private bool _enableKnobPulse;
    private bool _onlyShowColorOnKnob;
    private bool _showText;
    private bool _showTrackIcons;
    private float _gradientStartIntensity;
    private float _gradientEndIntensity;
    private float _knobPulseSpeed;
    private float _knobPulseIntensity;
    private LinearGradientMode _gradientDirection;
    private Color _onColor;
    private Color _offColor;
    private int _cornerRadius;
    private bool _useThemeColors;
    private ToggleSwitchKnobStyle _knobStyle;
    private ToggleSwitchChevronDirection _knobChevronDirection;
    private ToggleSwitchOrientation _orientation;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="ToggleSwitchValues" /> class.</summary>
    public ToggleSwitchValues()
    {
        _checked = false;
        _enableEmbossEffect = false;
        _animateGradientEffect = false;
        _enableKnobGradient = false;
        _enableKnobPulse = false;
        _onlyShowColorOnKnob = true;
        _showText = true;
        _showTrackIcons = false;
        _gradientStartIntensity = 0.8f;
        _gradientEndIntensity = 0.6f;
        _knobPulseSpeed = 1f;
        _knobPulseIntensity = 0.5f;
        _gradientDirection = LinearGradientMode.ForwardDiagonal;
        _onColor = Color.Green;
        _offColor = Color.Red;
        _cornerRadius = 10;
        _useThemeColors = true;
        _knobStyle = ToggleSwitchKnobStyle.Classic;
        _knobChevronDirection = ToggleSwitchChevronDirection.Auto;
        _orientation = ToggleSwitchOrientation.Horizontal;
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

    /// <summary>Gets or sets a value indicating whether the toggle switch is checked.</summary>
    /// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
    [Category("Behavior")]
    [Description("Indicates whether the toggle switch is checked.")]
    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Checked
    {
        get => _checked;
        set
        {
            if (_checked != value)
            {
                _checked = value;
                OnPropertyChanged(nameof(Checked));
            }
        }
    }

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
    [Description("Indicates whether the knob should have a gradient effect. Also applies to Classic, Bevel, Ring, RoundedSquare, Square, Grip, Chevron, and Indicator styles.")]
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

    /// <summary>Gets or sets a value indicating whether the knob should pulse while the control is enabled and visible.</summary>
    [Category("Appearance")]
    [Description("Indicates whether the knob should pulse while the control is enabled and visible.")]
    [DefaultValue(false)]
    public bool EnableKnobPulse
    {
        get => _enableKnobPulse;
        set
        {
            if (_enableKnobPulse != value)
            {
                _enableKnobPulse = value;
                OnPropertyChanged(nameof(EnableKnobPulse));
            }
        }
    }

    /// <summary>Gets or sets the knob pulse animation speed multiplier.</summary>
    [Category("Appearance")]
    [Description("Knob pulse animation speed multiplier. 1 is the default speed; values greater than 1 animate faster and values less than 1 animate slower.")]
    [DefaultValue(1f)]
    public float KnobPulseSpeed
    {
        get => _knobPulseSpeed;
        set
        {
            float speed = Math.Max(0.1f, Math.Min(10f, value));
            if (Math.Abs(_knobPulseSpeed - speed) > float.Epsilon)
            {
                _knobPulseSpeed = speed;
                OnPropertyChanged(nameof(KnobPulseSpeed));
            }
        }
    }

    /// <summary>Gets or sets the knob pulse intensity.</summary>
    [Category("Appearance")]
    [Description("Specifies how strong the knob pulse effect is. 0 is the minimum intensity; 1 is the maximum intensity.")]
    [DefaultValue(0.5f)]
    public float KnobPulseIntensity
    {
        get => _knobPulseIntensity;
        set
        {
            float intensity = Math.Max(0f, Math.Min(1f, value));
            if (Math.Abs(_knobPulseIntensity - intensity) > float.Epsilon)
            {
                _knobPulseIntensity = intensity;
                OnPropertyChanged(nameof(KnobPulseIntensity));
            }
        }
    }

    /// <summary>Gets or sets a value indicating whether [show color only on knob].</summary>
    [Category("Appearance")]
    [Description("When true, OnColor and OffColor are applied to the knob even if UseThemeColors is enabled.")]
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

    /// <summary>Gets or sets a value indicating whether check and cross icons are drawn on the track.</summary>
    [Category("Appearance")]
    [Description("When true, a check icon is shown on the left when checked and a cross icon on the right when unchecked.")]
    [DefaultValue(false)]
    public bool ShowTrackIcons
    {
        get => _showTrackIcons;
        set
        {
            if (_showTrackIcons != value)
            {
                _showTrackIcons = value;
                OnPropertyChanged(nameof(ShowTrackIcons));
            }
        }
    }

    /// <summary>Gets or sets the gradient start intensity.</summary>
    /// <value>The gradient start intensity.</value>
    [Category("Appearance")]
    [Description("Specifies the gradient intensity applied to the start color of the knob gradient.")]
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
    [Description("Specifies the gradient intensity applied to the end color of the knob gradient.")]
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

    /// <summary>Gets or sets the visual style used to render the switch knob.</summary>
    [Category("Appearance")]
    [Description("Specifies the visual style used to render the switch knob.")]
    [DefaultValue(ToggleSwitchKnobStyle.Classic)]
    public ToggleSwitchKnobStyle KnobStyle
    {
        get => _knobStyle;
        set
        {
            if (_knobStyle != value)
            {
                _knobStyle = value;
                OnPropertyChanged(nameof(KnobStyle));
            }
        }
    }

    /// <summary>Gets or sets the direction of chevron glyphs on a <see cref="ToggleSwitchKnobStyle.Chevron"/> knob.</summary>
    [Category("Appearance")]
    [Description("Specifies the direction of chevron glyphs on a Chevron knob. Auto points right when unchecked and left when checked.")]
    [DefaultValue(ToggleSwitchChevronDirection.Auto)]
    public ToggleSwitchChevronDirection KnobChevronDirection
    {
        get => _knobChevronDirection;
        set
        {
            if (_knobChevronDirection != value)
            {
                _knobChevronDirection = value;
                OnPropertyChanged(nameof(KnobChevronDirection));
            }
        }
    }

    /// <summary>Gets or sets whether the switch lays out horizontally or vertically.</summary>
    [Category("Appearance")]
    [Description("Specifies whether the knob travels horizontally (left/right) or vertically (top/bottom). Vertical layouts work best with a tall, narrow control size.")]
    [DefaultValue(ToggleSwitchOrientation.Horizontal)]
    public ToggleSwitchOrientation Orientation
    {
        get => _orientation;
        set
        {
            if (_orientation != value)
            {
                _orientation = value;
                OnPropertyChanged(nameof(Orientation));
            }
        }
    }

    #endregion

    #region IsDefault

    /// <summary>Gets a value indicating whether this instance is default.</summary>
    /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
    [Browsable(false)]
    public bool IsDefault => !_checked && !_enableEmbossEffect && !_enableKnobGradient && !_enableKnobPulse &&
                             _gradientStartIntensity.Equals(0.8f) &&
                             _gradientEndIntensity.Equals(0.6f) &&
                             Math.Abs(_knobPulseSpeed - 1f) < float.Epsilon &&
                             Math.Abs(_knobPulseIntensity - 0.5f) < float.Epsilon &&
                             _gradientDirection == LinearGradientMode.ForwardDiagonal && _onColor == Color.Green &&
                             _offColor == Color.Red && _cornerRadius == 10 && _useThemeColors &&
                             _knobStyle == ToggleSwitchKnobStyle.Classic &&
                             _orientation == ToggleSwitchOrientation.Horizontal;

    #endregion

    #region Reset

    /// <summary>Resets the values.</summary>
    public void Reset()
    {
        Checked = false;
        EnableEmbossEffect = false;
        AnimateGradientEffect = false;
        EnableKnobGradient = false;
        EnableKnobPulse = false;
        KnobPulseSpeed = 1f;
        KnobPulseIntensity = 0.5f;
        OnlyShowColorOnKnob = true;
        ShowText = true;
        ShowTrackIcons = false;
        GradientStartIntensity = 0.8f;
        GradientEndIntensity = 0.6f;
        GradientDirection = LinearGradientMode.ForwardDiagonal;
        OnColor = Color.Green;
        OffColor = Color.Red;
        CornerRadius = 10;
        UseThemeColors = true;
        KnobStyle = ToggleSwitchKnobStyle.Classic;
        KnobChevronDirection = ToggleSwitchChevronDirection.Auto;
        Orientation = ToggleSwitchOrientation.Horizontal;
    }

    #endregion

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticVariables.DEFAULT_EMPTY_STRING;
}