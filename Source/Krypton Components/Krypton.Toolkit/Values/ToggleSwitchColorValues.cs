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
/// Storage for <see cref="KryptonToggleSwitch"/> colour settings.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ToggleSwitchColorValues : GlobalId, INotifyPropertyChanged
{
    #region Instance Fields

    private Color _onColor;
    private Color _offColor;
    private bool _onlyShowColorOnKnob;
    private bool _useThemeColors;
    private readonly ToggleSwitchTintColorValues _tintColors;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="ToggleSwitchColorValues"/> class.</summary>
    public ToggleSwitchColorValues()
    {
        _tintColors = new ToggleSwitchTintColorValues();
        _tintColors.PropertyChanged += OnTintColorsPropertyChanged;
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

    /// <summary>Gets or sets the color used when the switch is on.</summary>
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

    /// <summary>Gets or sets the color used when the switch is off.</summary>
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

    /// <summary>Gets access to optional tint colour settings applied over OnColor and OffColor.</summary>
    [Category("Appearance")]
    [Description("Optional tint colours blended onto OnColor and OffColor.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ToggleSwitchTintColorValues TintColors => _tintColors;

    private bool ShouldSerializeTintColors() => !TintColors.IsDefault;

    /// <summary>Resets the TintColors property to its default value.</summary>
    public void ResetTintColors() => TintColors.Reset();

    /// <summary>Gets or sets whether OnColor and OffColor apply to the knob when theme colours are enabled.</summary>
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

    /// <summary>Gets or sets whether theme palette colours are used.</summary>
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

    /// <summary>Gets the effective on colour after optional tinting.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color EffectiveOnColor => TintColors.ApplyOnTint(OnColor);

    /// <summary>Gets the effective off colour after optional tinting.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color EffectiveOffColor => TintColors.ApplyOffTint(OffColor);

    #endregion

    #region IsDefault

    /// <summary>Gets a value indicating whether this instance is default.</summary>
    [Browsable(false)]
    public bool IsDefault =>
        _onColor == Color.Green &&
        _offColor == Color.Red &&
        TintColors.IsDefault &&
        _onlyShowColorOnKnob &&
        _useThemeColors;

    #endregion

    #region Reset

    /// <summary>Resets the values.</summary>
    public void Reset()
    {
        OnColor = Color.Green;
        OffColor = Color.Red;
        ResetTintColors();
        OnlyShowColorOnKnob = true;
        UseThemeColors = true;
    }

    #endregion

    #region Implementation

    /// <summary>Called when a property value changes.</summary>
    /// <param name="propertyName">Name of the property.</param>
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void OnTintColorsPropertyChanged(object? sender, PropertyChangedEventArgs e) =>
        OnPropertyChanged(nameof(TintColors));

    #endregion
}
