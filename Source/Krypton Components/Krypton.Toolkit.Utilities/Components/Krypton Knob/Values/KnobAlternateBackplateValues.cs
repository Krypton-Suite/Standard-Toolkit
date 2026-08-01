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
/// Contains industrial-style backplate settings for a <see cref="KryptonKnobAlternate"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KnobAlternateBackplateValues : Storage
{
    #region Instance Fields
    private readonly KryptonKnobAlternate _owner;
    private KnobBackplateShape _shape;
    private Color _color1;
    private Color _color2;
    private Color _borderColor;
    private bool _showInsetWell;
    private bool _showDropShadow;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KnobAlternateBackplateValues"/> class.
    /// </summary>
    /// <param name="owner">Reference to owning control.</param>
    public KnobAlternateBackplateValues(KryptonKnobAlternate owner)
    {
        _owner = owner;
        _shape = KnobBackplateShape.None;
        _color1 = GlobalStaticVariables.EMPTY_COLOR;
        _color2 = GlobalStaticVariables.EMPTY_COLOR;
        _borderColor = GlobalStaticVariables.EMPTY_COLOR;
        _showInsetWell = true;
        _showDropShadow = true;
    }
    #endregion

    #region IsDefault
    /// <inheritdoc />
    public override bool IsDefault =>
        _shape == KnobBackplateShape.None &&
        _color1 == GlobalStaticVariables.EMPTY_COLOR &&
        _color2 == GlobalStaticVariables.EMPTY_COLOR &&
        _borderColor == GlobalStaticVariables.EMPTY_COLOR &&
        _showInsetWell &&
        _showDropShadow;
    #endregion

    #region Public
    /// <summary>
    /// Gets or sets the backplate shape drawn behind the knob.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Shape of the industrial mounting backplate behind the knob.")]
    [DefaultValue(KnobBackplateShape.None)]
    public KnobBackplateShape Shape
    {
        get => _shape;
        set
        {
            if (_shape != value)
            {
                _shape = value;
                SyncToOwner();
            }
        }
    }

    /// <summary>
    /// Gets or sets the primary backplate colour (gradient start).
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Primary backplate colour. When empty a metallic silver gradient is used.")]
    public Color Color1
    {
        get => _color1;
        set
        {
            if (_color1 != value)
            {
                _color1 = value;
                SyncToOwner();
            }
        }
    }

    private bool ShouldSerializeColor1() => _color1 != GlobalStaticVariables.EMPTY_COLOR;

    private void ResetColor1() => Color1 = GlobalStaticVariables.EMPTY_COLOR;

    /// <summary>
    /// Gets or sets the secondary backplate colour (gradient end).
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Secondary backplate colour. When empty a darker metallic tone is used.")]
    public Color Color2
    {
        get => _color2;
        set
        {
            if (_color2 != value)
            {
                _color2 = value;
                SyncToOwner();
            }
        }
    }

    private bool ShouldSerializeColor2() => _color2 != GlobalStaticVariables.EMPTY_COLOR;

    private void ResetColor2() => Color2 = GlobalStaticVariables.EMPTY_COLOR;

    /// <summary>
    /// Gets or sets the backplate border colour.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Backplate border colour. When empty a darkened border is derived from Color1.")]
    public Color BorderColor
    {
        get => _borderColor;
        set
        {
            if (_borderColor != value)
            {
                _borderColor = value;
                SyncToOwner();
            }
        }
    }

    private bool ShouldSerializeBorderColor() => _borderColor != GlobalStaticVariables.EMPTY_COLOR;

    private void ResetBorderColor() => BorderColor = GlobalStaticVariables.EMPTY_COLOR;

    /// <summary>
    /// Gets or sets whether an inset well is drawn where the knob sits on the plate.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Draw an inset shadow ring where the knob mounts on the plate.")]
    [DefaultValue(true)]
    public bool ShowInsetWell
    {
        get => _showInsetWell;
        set
        {
            if (_showInsetWell != value)
            {
                _showInsetWell = value;
                SyncToOwner();
            }
        }
    }

    /// <summary>
    /// Gets or sets whether a drop shadow is drawn under the knob.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Draw a drop shadow under the knob on the backplate.")]
    [DefaultValue(true)]
    public bool ShowDropShadow
    {
        get => _showDropShadow;
        set
        {
            if (_showDropShadow != value)
            {
                _showDropShadow = value;
                SyncToOwner();
            }
        }
    }

    /// <summary>
    /// Resets backplate values to their defaults.
    /// </summary>
    public void Reset()
    {
        Shape = KnobBackplateShape.None;
        ResetColor1();
        ResetColor2();
        ResetBorderColor();
        ShowInsetWell = true;
        ShowDropShadow = true;
    }

    /// <summary>
    /// Applies a yellow rounded industrial panel preset.
    /// </summary>
    public void ApplyYellowPanelPreset()
    {
        Shape = KnobBackplateShape.RoundedSquare;
        Color1 = Color.FromArgb(255, 220, 0);
        Color2 = Color.FromArgb(220, 170, 0);
        BorderColor = Color.FromArgb(30, 30, 30);
        ShowInsetWell = true;
        ShowDropShadow = true;
    }

    /// <summary>
    /// Applies a brushed silver square industrial panel preset.
    /// </summary>
    public void ApplySilverPanelPreset()
    {
        Shape = KnobBackplateShape.Square;
        Color1 = Color.FromArgb(210, 210, 215);
        Color2 = Color.FromArgb(150, 150, 158);
        BorderColor = Color.FromArgb(90, 90, 95);
        ShowInsetWell = true;
        ShowDropShadow = true;
    }

    /// <summary>
    /// Applies a circular black industrial panel preset.
    /// </summary>
    public void ApplyBlackPanelPreset()
    {
        Shape = KnobBackplateShape.Circle;
        Color1 = Color.FromArgb(55, 55, 58);
        Color2 = Color.FromArgb(20, 20, 22);
        BorderColor = Color.FromArgb(10, 10, 10);
        ShowInsetWell = true;
        ShowDropShadow = true;
    }
    #endregion

    #region Implementation
    /// <inheritdoc />
    public override string ToString() => IsDefault ? string.Empty : Shape.ToString();

    internal KnobBackplateSettings GetSettings() => new KnobBackplateSettings
    {
        Shape = _shape,
        Color1 = _color1,
        Color2 = _color2,
        BorderColor = _borderColor,
        ShowInsetWell = _showInsetWell,
        ShowDropShadow = _showDropShadow
    };

    private void SyncToOwner() => _owner.OnBackplateChanged();
    #endregion
}
