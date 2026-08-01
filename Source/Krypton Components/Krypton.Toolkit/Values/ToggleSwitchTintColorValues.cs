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
/// Storage for optional tint colours applied over switch colours and glyphs.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ToggleSwitchTintColorValues : GlobalId, INotifyPropertyChanged
{
    #region Instance Fields

    private bool _enable;
    private bool _enableGlyphs;
    private Color _onTint;
    private Color _offTint;
    private Color _tintColor1;
    private Color _tintColor2;
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

    /// <summary>Gets or sets whether TintColor1 and TintColor2 are applied to glyphs (chevron, check, cross).</summary>
    [Category("Appearance")]
    [Description("When true, TintColor1 and TintColor2 tint glyph outline and fill colours.")]
    [DefaultValue(false)]
    public bool EnableGlyphs
    {
        get => _enableGlyphs;
        set
        {
            if (_enableGlyphs != value)
            {
                _enableGlyphs = value;
                OnPropertyChanged(nameof(EnableGlyphs));
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

    /// <summary>Gets or sets the first glyph tint colour (outline).</summary>
    [Category("Appearance")]
    [Description("Outline tint for glyphs such as the chevron arrow when EnableGlyphs is true. Empty keeps the default glyph outline colour.")]
    [DefaultValue(typeof(Color), "")]
    public Color TintColor1
    {
        get => _tintColor1;
        set
        {
            if (_tintColor1 != value)
            {
                _tintColor1 = value;
                OnPropertyChanged(nameof(TintColor1));
            }
        }
    }

    /// <summary>Gets or sets the second glyph tint colour (fill).</summary>
    [Category("Appearance")]
    [Description("Fill tint for glyphs such as the chevron arrow when EnableGlyphs is true. Empty falls back to TintColor1 or the default glyph colour.")]
    [DefaultValue(typeof(Color), "")]
    public Color TintColor2
    {
        get => _tintColor2;
        set
        {
            if (_tintColor2 != value)
            {
                _tintColor2 = value;
                OnPropertyChanged(nameof(TintColor2));
            }
        }
    }

    /// <summary>Gets or sets how strongly tint colours are blended (0 = none, 1 = full tint).</summary>
    [Category("Appearance")]
    [Description("Blend strength for colour and glyph tints. 0 leaves the base colour unchanged; 1 replaces it with the tint.")]
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
        !_enableGlyphs &&
        _onTint.IsEmpty &&
        _offTint.IsEmpty &&
        _tintColor1.IsEmpty &&
        _tintColor2.IsEmpty &&
        Math.Abs(_intensity - 0.35f) < float.Epsilon;

    #endregion

    #region Reset

    /// <summary>Resets the values.</summary>
    public void Reset()
    {
        Enable = false;
        EnableGlyphs = false;
        OnTint = Color.Empty;
        OffTint = Color.Empty;
        TintColor1 = Color.Empty;
        TintColor2 = Color.Empty;
        Intensity = 0.35f;
    }

    #endregion

    #region Implementation

    /// <summary>Applies the on-state tint to the supplied colour when enabled.</summary>
    public Color ApplyOnTint(Color color) => ApplyTint(color, _onTint, _enable);

    /// <summary>Applies the off-state tint to the supplied colour when enabled.</summary>
    public Color ApplyOffTint(Color color) => ApplyTint(color, _offTint, _enable);

    /// <summary>Resolves the glyph outline colour using TintColor1 when glyph tinting is enabled.</summary>
    public Color ResolveGlyphOutline(Color baseColor) =>
        ApplyTint(baseColor, _tintColor1, _enableGlyphs);

    /// <summary>Resolves the glyph fill colour using TintColor2 (or TintColor1) when glyph tinting is enabled.</summary>
    public Color ResolveGlyphFill(Color baseColor)
    {
        if (!_enableGlyphs)
        {
            return baseColor;
        }

        Color fillTint = !_tintColor2.IsEmpty ? _tintColor2 : _tintColor1;
        return ApplyTint(baseColor, fillTint, true);
    }

    private Color ApplyTint(Color color, Color tint, bool enabled)
    {
        if (!enabled || tint.IsEmpty || _intensity <= float.Epsilon)
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
