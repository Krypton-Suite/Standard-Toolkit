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
/// Storage for optional tint colours applied over <see cref="ToggleSwitchColorValues.OnColor"/> and <see cref="ToggleSwitchColorValues.OffColor"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ToggleSwitchTintColorValues : GlobalId, INotifyPropertyChanged
{
    #region Instance Fields

    private bool _enable;
    private Color _onTint;
    private Color _offTint;
    private float _intensity;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="ToggleSwitchTintColorValues"/> class.</summary>
    public ToggleSwitchTintColorValues() => Reset();

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticVariables.DEFAULT_EMPTY_STRING;

    #endregion

    #region Event

    /// <summary>Occurs when a property value changes.</summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    #endregion

    #region Public

    /// <summary>Gets or sets whether tint colours are applied to OnColor and OffColor.</summary>
    [Category("Appearance")]
    [Description("When true, OnTint and OffTint are blended onto OnColor and OffColor.")]
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

    /// <summary>Gets or sets the tint colour blended onto OnColor when enabled.</summary>
    [Category("Appearance")]
    [Description("Tint colour blended onto OnColor when Enable is true. Empty uses no tint for the on state.")]
    [DefaultValue(typeof(Color), "")]
    public Color OnTint
    {
        get => _onTint;
        set
        {
            if (_onTint != value)
            {
                _onTint = value;
                OnPropertyChanged(nameof(OnTint));
            }
        }
    }

    /// <summary>Gets or sets the tint colour blended onto OffColor when enabled.</summary>
    [Category("Appearance")]
    [Description("Tint colour blended onto OffColor when Enable is true. Empty uses no tint for the off state.")]
    [DefaultValue(typeof(Color), "")]
    public Color OffTint
    {
        get => _offTint;
        set
        {
            if (_offTint != value)
            {
                _offTint = value;
                OnPropertyChanged(nameof(OffTint));
            }
        }
    }

    /// <summary>Gets or sets how strongly tint colours are blended (0 = none, 1 = full tint).</summary>
    [Category("Appearance")]
    [Description("Blend strength for tint colours. 0 leaves OnColor/OffColor unchanged; 1 replaces them with the tint.")]
    [DefaultValue(0.35f)]
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
        _onTint.IsEmpty &&
        _offTint.IsEmpty &&
        Math.Abs(_intensity - 0.35f) < float.Epsilon;

    #endregion

    #region Reset

    /// <summary>Resets the values.</summary>
    public void Reset()
    {
        Enable = false;
        OnTint = Color.Empty;
        OffTint = Color.Empty;
        Intensity = 0.35f;
    }

    #endregion

    #region Implementation

    /// <summary>Applies the on-state tint to the supplied colour when enabled.</summary>
    public Color ApplyOnTint(Color color) => ApplyTint(color, _onTint);

    /// <summary>Applies the off-state tint to the supplied colour when enabled.</summary>
    public Color ApplyOffTint(Color color) => ApplyTint(color, _offTint);

    private Color ApplyTint(Color color, Color tint)
    {
        if (!_enable || tint.IsEmpty || _intensity <= float.Epsilon)
        {
            return color;
        }

        float t = _intensity;
        return Color.FromArgb(
            255,
            (int)(color.R + (tint.R - color.R) * t),
            (int)(color.G + (tint.G - color.G) * t),
            (int)(color.B + (tint.B - color.B) * t));
    }

    /// <summary>Called when a property value changes.</summary>
    /// <param name="propertyName">Name of the property.</param>
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion
}
