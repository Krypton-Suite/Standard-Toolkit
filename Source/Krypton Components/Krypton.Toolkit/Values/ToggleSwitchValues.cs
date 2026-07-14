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

/// <summary>
/// Storage for <see cref="KryptonToggleSwitch"/> appearance and behaviour settings.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ToggleSwitchValues : GlobalId, INotifyPropertyChanged
{
    #region Instance Fields

    private bool _checked;
    private bool _enableEmbossEffect;
    private bool _showText;
    private bool _showTrackIcons;
    private int _cornerRadius;
    private ToggleSwitchKnobStyle _knobStyle;
    private ToggleSwitchOrientation _orientation;
    private readonly ToggleSwitchColorValues _colors;
    private readonly ToggleSwitchGradientValues _gradient;
    private readonly ToggleSwitchPulseValues _pulse;
    private readonly ToggleSwitchChevronValues _chevron;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="ToggleSwitchValues" /> class.</summary>
    public ToggleSwitchValues()
    {
        _colors = new ToggleSwitchColorValues();
        _gradient = new ToggleSwitchGradientValues();
        _pulse = new ToggleSwitchPulseValues();
        _chevron = new ToggleSwitchChevronValues();

        _colors.PropertyChanged += OnChildPropertyChanged;
        _gradient.PropertyChanged += OnChildPropertyChanged;
        _pulse.PropertyChanged += OnChildPropertyChanged;
        _chevron.PropertyChanged += OnChildPropertyChanged;

        Reset();
    }

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticVariables.DEFAULT_EMPTY_STRING;

    #endregion

    #region Event

    /// <summary>Occurs when a property value changes.</summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    #endregion

    #region Public

    /// <summary>Gets or sets a value indicating whether the toggle switch is checked.</summary>
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

    /// <summary>Gets or sets a value indicating whether the emboss effect should be applied.</summary>
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

    /// <summary>Gets access to colour settings.</summary>
    [Category("Appearance")]
    [Description("Storage for On/Off and theme colour settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ToggleSwitchColorValues Colors => _colors;

    private bool ShouldSerializeColors() => !Colors.IsDefault;

    /// <summary>Resets the Colors property to its default value.</summary>
    public void ResetColors() => Colors.Reset();

    /// <summary>Gets access to knob gradient settings.</summary>
    [Category("Appearance")]
    [Description("Storage for knob gradient settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ToggleSwitchGradientValues Gradient => _gradient;

    private bool ShouldSerializeGradient() => !Gradient.IsDefault;

    /// <summary>Resets the Gradient property to its default value.</summary>
    public void ResetGradient() => Gradient.Reset();

    /// <summary>Gets access to knob pulse animation settings.</summary>
    [Category("Appearance")]
    [Description("Storage for knob pulse animation settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ToggleSwitchPulseValues Pulse => _pulse;

    private bool ShouldSerializePulse() => !Pulse.IsDefault;

    /// <summary>Resets the Pulse property to its default value.</summary>
    public void ResetPulse() => Pulse.Reset();

    /// <summary>Gets access to Chevron knob glyph settings.</summary>
    [Category("Appearance")]
    [Description("Storage for Chevron knob glyph settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ToggleSwitchChevronValues Chevron => _chevron;

    private bool ShouldSerializeChevron() => !Chevron.IsDefault;

    /// <summary>Resets the Chevron property to its default value.</summary>
    public void ResetChevron() => Chevron.Reset();

    /// <summary>Gets or sets a value indicating whether On/Off text should be shown.</summary>
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

    /// <summary>Gets or sets the corner radius of the switch.</summary>
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

    #region Compatibility Wrappers

    /// <summary>Gets or sets the color when on.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use Colors.OnColor instead.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color OnColor
    {
        get => Colors.OnColor;
        set => Colors.OnColor = value;
    }

    /// <summary>Gets or sets the color when off.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use Colors.OffColor instead.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color OffColor
    {
        get => Colors.OffColor;
        set => Colors.OffColor = value;
    }

    /// <summary>Gets or sets whether OnColor/OffColor apply to the knob when theme colours are enabled.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use Colors.OnlyShowColorOnKnob instead.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool OnlyShowColorOnKnob
    {
        get => Colors.OnlyShowColorOnKnob;
        set => Colors.OnlyShowColorOnKnob = value;
    }

    /// <summary>Gets or sets whether theme palette colours are used.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use Colors.UseThemeColors instead.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool UseThemeColors
    {
        get => Colors.UseThemeColors;
        set => Colors.UseThemeColors = value;
    }

    /// <summary>Gets or sets whether the gradient effect should be animated.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use Gradient.Animate instead.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AnimateGradientEffect
    {
        get => Gradient.Animate;
        set => Gradient.Animate = value;
    }

    /// <summary>Gets or sets whether the knob should have a gradient effect.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use Gradient.Enable instead.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool EnableKnobGradient
    {
        get => Gradient.Enable;
        set => Gradient.Enable = value;
    }

    /// <summary>Gets or sets the gradient start intensity.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use Gradient.StartIntensity instead.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float GradientStartIntensity
    {
        get => Gradient.StartIntensity;
        set => Gradient.StartIntensity = value;
    }

    /// <summary>Gets or sets the gradient end intensity.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use Gradient.EndIntensity instead.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float GradientEndIntensity
    {
        get => Gradient.EndIntensity;
        set => Gradient.EndIntensity = value;
    }

    /// <summary>Gets or sets the gradient direction.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use Gradient.Direction instead.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LinearGradientMode GradientDirection
    {
        get => Gradient.Direction;
        set => Gradient.Direction = value;
    }

    /// <summary>Gets or sets whether the knob should pulse.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use Pulse.Enable instead.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool EnableKnobPulse
    {
        get => Pulse.Enable;
        set => Pulse.Enable = value;
    }

    /// <summary>Gets or sets the knob pulse animation speed multiplier.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use Pulse.Speed instead.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float KnobPulseSpeed
    {
        get => Pulse.Speed;
        set => Pulse.Speed = value;
    }

    /// <summary>Gets or sets the knob pulse intensity.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use Pulse.Intensity instead.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float KnobPulseIntensity
    {
        get => Pulse.Intensity;
        set => Pulse.Intensity = value;
    }

    /// <summary>Gets or sets the direction of chevron glyphs on a Chevron knob.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use Chevron.Direction instead.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ToggleSwitchChevronDirection KnobChevronDirection
    {
        get => Chevron.Direction;
        set => Chevron.Direction = value;
    }

    /// <summary>Gets or sets the chevron glyph size as a fraction of the knob size.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use Chevron.GlyphSize instead.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float KnobChevronGlyphSize
    {
        get => Chevron.GlyphSize;
        set => Chevron.GlyphSize = value;
    }

    #endregion

    #region IsDefault

    /// <summary>Gets a value indicating whether this instance is default.</summary>
    [Browsable(false)]
    public bool IsDefault =>
        !_checked &&
        !_enableEmbossEffect &&
        Colors.IsDefault &&
        Gradient.IsDefault &&
        Pulse.IsDefault &&
        Chevron.IsDefault &&
        _showText &&
        !_showTrackIcons &&
        _cornerRadius == 10 &&
        _knobStyle == ToggleSwitchKnobStyle.Classic &&
        _orientation == ToggleSwitchOrientation.Horizontal;

    #endregion

    #region Reset

    /// <summary>Resets the values.</summary>
    public void Reset()
    {
        Checked = false;
        EnableEmbossEffect = false;
        ResetColors();
        ResetGradient();
        ResetPulse();
        ResetChevron();
        ShowText = true;
        ShowTrackIcons = false;
        CornerRadius = 10;
        KnobStyle = ToggleSwitchKnobStyle.Classic;
        Orientation = ToggleSwitchOrientation.Horizontal;
    }

    #endregion

    #region Implementation

    /// <summary>Called when a property value changes.</summary>
    /// <param name="propertyName">Name of the property.</param>
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void OnChildPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (ReferenceEquals(sender, _colors))
        {
            OnPropertyChanged(nameof(Colors));
        }
        else if (ReferenceEquals(sender, _gradient))
        {
            OnPropertyChanged(nameof(Gradient));
        }
        else if (ReferenceEquals(sender, _pulse))
        {
            OnPropertyChanged(nameof(Pulse));
        }
        else if (ReferenceEquals(sender, _chevron))
        {
            OnPropertyChanged(nameof(Chevron));
        }
    }

    #endregion
}
