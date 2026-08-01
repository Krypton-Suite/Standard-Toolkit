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
/// Storage for <see cref="ToggleSwitchKnobStyle.Chevron"/> knob glyph settings.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ToggleSwitchChevronValues : GlobalId, INotifyPropertyChanged
{
    #region Instance Fields

    private ToggleSwitchChevronDirection _direction;
    private float _glyphSize;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="ToggleSwitchChevronValues"/> class.</summary>
    public ToggleSwitchChevronValues() => Reset();

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticVariables.DEFAULT_EMPTY_STRING;

    #endregion

    #region Event

    /// <summary>Occurs when a property value changes.</summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    #endregion

    #region Public

    /// <summary>Gets or sets the direction of chevron glyphs on a Chevron knob.</summary>
    [Category("Appearance")]
    [Description("Specifies the direction of arrow glyphs on a Chevron knob. Auto points toward the checked position. Uses the same arrow glyphs as Krypton input controls.")]
    [DefaultValue(ToggleSwitchChevronDirection.Auto)]
    public ToggleSwitchChevronDirection Direction
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

    /// <summary>Gets or sets the chevron glyph size as a fraction of the knob size.</summary>
    [Category("Appearance")]
    [Description("Specifies the chevron glyph size on a Chevron knob as a fraction of the knob size. 1 uses the full knob size.")]
    [DefaultValue(0.62f)]
    public float GlyphSize
    {
        get => _glyphSize;
        set
        {
            float size = Math.Max(0.2f, Math.Min(1f, value));
            if (Math.Abs(_glyphSize - size) > float.Epsilon)
            {
                _glyphSize = size;
                OnPropertyChanged(nameof(GlyphSize));
            }
        }
    }

    #endregion

    #region IsDefault

    /// <summary>Gets a value indicating whether this instance is default.</summary>
    [Browsable(false)]
    public bool IsDefault =>
        _direction == ToggleSwitchChevronDirection.Auto &&
        Math.Abs(_glyphSize - 0.62f) < float.Epsilon;

    #endregion

    #region Reset

    /// <summary>Resets the values.</summary>
    public void Reset()
    {
        Direction = ToggleSwitchChevronDirection.Auto;
        GlyphSize = 0.62f;
    }

    #endregion

    #region Implementation

    /// <summary>Called when a property value changes.</summary>
    /// <param name="propertyName">Name of the property.</param>
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion
}
