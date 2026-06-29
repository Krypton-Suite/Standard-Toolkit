#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Describes a text label drawn on an industrial knob backplate.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KnobPlateLabel : Storage
{
    #region Instance Fields
    private readonly Action _onChanged;
    private string _text;
    private float _angle;
    private Color _color;
    private bool _visible;
    private float _radiusFactor;
    private Font? _font;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KnobPlateLabel"/> class.
    /// </summary>
    /// <param name="onChanged">Delegate invoked when a value changes.</param>
    internal KnobPlateLabel(Action onChanged)
    {
        _onChanged = onChanged;
        _text = string.Empty;
        _angle = 0f;
        _color = GlobalStaticVariables.EMPTY_COLOR;
        _visible = false;
        _radiusFactor = 0.88f;
    }
    #endregion

    #region IsDefault
    /// <inheritdoc />
    public override bool IsDefault =>
        string.IsNullOrEmpty(_text) &&
        _angle.Equals(0f) &&
        _color == GlobalStaticVariables.EMPTY_COLOR &&
        !_visible &&
        _radiusFactor.Equals(0.88f) &&
        _font == null;
    #endregion

    #region Public
    /// <summary>
    /// Gets or sets the label text.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Text displayed on the backplate.")]
    [DefaultValue("")]
    public string Text
    {
        get => _text;
        set
        {
            if (_text != value)
            {
                _text = value ?? string.Empty;
                _onChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the label angle in degrees (same convention as knob start/end angles).
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Angle in degrees at which the label is placed around the plate centre.")]
    [DefaultValue(0f)]
    public float Angle
    {
        get => _angle;
        set
        {
            if (!_angle.Equals(value))
            {
                _angle = value;
                _onChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the label text colour.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Colour of the label text.")]
    public Color Color
    {
        get => _color;
        set
        {
            if (_color != value)
            {
                _color = value;
                _onChanged();
            }
        }
    }

    private bool ShouldSerializeColor() => _color != GlobalStaticVariables.EMPTY_COLOR;

    private void ResetColor() => Color = GlobalStaticVariables.EMPTY_COLOR;

    /// <summary>
    /// Gets or sets whether the label is visible.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Whether the label is drawn.")]
    [DefaultValue(false)]
    public bool Visible
    {
        get => _visible;
        set
        {
            if (_visible != value)
            {
                _visible = value;
                _onChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the radial position as a factor of the plate radius (0 to 1).
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Radial position as a factor of the plate radius.")]
    [DefaultValue(0.88f)]
    public float RadiusFactor
    {
        get => _radiusFactor;
        set
        {
            if (!_radiusFactor.Equals(value))
            {
                _radiusFactor = value;
                _onChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets an optional label font. When null the control scale font is used.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Optional label font. When null the scale font is used.")]
    [DefaultValue(null)]
    public Font? Font
    {
        get => _font;
        set
        {
            if (!ReferenceEquals(_font, value))
            {
                _font = value;
                _onChanged();
            }
        }
    }
    #endregion

    #region Implementation
    /// <inheritdoc />
    public override string ToString() => string.IsNullOrEmpty(_text) ? string.Empty : _text;
    #endregion
}
