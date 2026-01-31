#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Holds background properties for a progress bar threshold region state.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ProgressBarTriStateRegionBackValues : Storage
{
    #region Instance Fields

    private readonly ProgressBarTriStateRegionStateValues _parent;
    private Color _color1;
    private Color _color2;
    private PaletteColorStyle _colorStyle;
    private PaletteRectangleAlign _colorAlign;
    private float _colorAngle;
    private Image? _image;
    private PaletteImageStyle _imageStyle;
    private PaletteRectangleAlign _imageAlign;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the ProgressBarTriStateRegionBackValues class.
    /// </summary>
    /// <param name="parent">Reference to owning state.</param>
    /// <param name="defaultColor1">Default color1 value.</param>
    public ProgressBarTriStateRegionBackValues(ProgressBarTriStateRegionStateValues parent, Color defaultColor1)
    {
        _parent = parent;
        _color1 = defaultColor1;
        _color2 = Color.Empty;
        _colorStyle = PaletteColorStyle.Inherit;
        _colorAlign = PaletteRectangleAlign.Inherit;
        _colorAngle = -1f;
        _image = null;
        _imageStyle = PaletteImageStyle.Inherit;
        _imageAlign = PaletteRectangleAlign.Inherit;
    }

    /// <summary>
    /// Gets a value indicating if all values are default (all Empty/Inherit/null/-1).
    /// Note: For StateCommon, Color1 may be set to the region's default color (Red/Orange/Green) which is considered non-default.
    /// For StateNormal/StateDisabled, Empty means inheriting from Common, which is default.
    /// </summary>
    [Browsable(false)]
    public override bool IsDefault => _color2 == Color.Empty &&
                             _colorStyle == PaletteColorStyle.Inherit &&
                             _colorAlign == PaletteRectangleAlign.Inherit &&
                             Math.Abs(_colorAngle - (-1f)) < 0.001f &&
                             _image == null &&
                             _imageStyle == PaletteImageStyle.Inherit &&
                             _imageAlign == PaletteRectangleAlign.Inherit;
    // Note: _color1 is checked at the State level, not here, because its default differs per state

    #endregion

    #region Public

    [Category(@"Visuals")]
    [Description(@"First background color.")]
    [DefaultValue(typeof(Color), nameof(Color.Empty))]
    [KryptonDefaultColor]
    public Color Color1
    {
        get => _color1;
        set
        {
            if (_color1 == value)
            {
                return;
            }

            _color1 = value;
            _parent.OnBackChanged();
        }
    }

    [Category(@"Visuals")]
    [Description(@"Second background color. Empty uses default.")]
    [DefaultValue(typeof(Color), nameof(Color.Empty))]
    [KryptonDefaultColor]
    public Color Color2
    {
        get => _color2;
        set
        {
            if (_color2 == value)
            {
                return;
            }

            _color2 = value;
            _parent.OnBackChanged();
        }
    }

    [Category(@"Visuals")]
    [Description(@"Background color drawing style.")]
    [DefaultValue(PaletteColorStyle.Inherit)]
    public PaletteColorStyle ColorStyle
    {
        get => _colorStyle;
        set
        {
            if (_colorStyle == value)
            {
                return;
            }

            _colorStyle = value;
            _parent.OnBackChanged();
        }
    }

    [Category(@"Visuals")]
    [Description(@"Background color alignment.")]
    [DefaultValue(PaletteRectangleAlign.Inherit)]
    public PaletteRectangleAlign ColorAlign
    {
        get => _colorAlign;
        set
        {
            if (_colorAlign == value)
            {
                return;
            }

            _colorAlign = value;
            _parent.OnBackChanged();
        }
    }

    [Category(@"Visuals")]
    [Description(@"Background color angle. -1 uses default.")]
    [DefaultValue(-1f)]
    public float ColorAngle
    {
        get => _colorAngle;
        set
        {
            if (Math.Abs(_colorAngle - value) < 0.001f)
            {
                return;
            }

            _colorAngle = value;
            _parent.OnBackChanged();
        }
    }

    [Category(@"Visuals")]
    [Description(@"Background image. Null uses default.")]
    [DefaultValue(null)]
    public Image? Image
    {
        get => _image;
        set
        {
            if (_image == value)
            {
                return;
            }

            _image = value;
            _parent.OnBackChanged();
        }
    }

    [Category(@"Visuals")]
    [Description(@"Background image style.")]
    [DefaultValue(PaletteImageStyle.Inherit)]
    public PaletteImageStyle ImageStyle
    {
        get => _imageStyle;
        set
        {
            if (_imageStyle == value)
            {
                return;
            }

            _imageStyle = value;
            _parent.OnBackChanged();
        }
    }

    [Category(@"Visuals")]
    [Description(@"Background image alignment.")]
    [DefaultValue(PaletteRectangleAlign.Inherit)]
    public PaletteRectangleAlign ImageAlign
    {
        get => _imageAlign;
        set
        {
            if (_imageAlign == value)
            {
                return;
            }

            _imageAlign = value;
            _parent.OnBackChanged();
        }
    }

    /// <summary>
    /// Resets all values to their default.
    /// </summary>
    public void Reset()
    {
        _color1 = Color.Empty;
        _color2 = Color.Empty;
        _colorStyle = PaletteColorStyle.Inherit;
        _colorAlign = PaletteRectangleAlign.Inherit;
        _colorAngle = -1f;
        _image = null;
        _imageStyle = PaletteImageStyle.Inherit;
        _imageAlign = PaletteRectangleAlign.Inherit;
        _parent.OnBackChanged();
    }

    #endregion

    #region Public Overrides

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticValues.DEFAULT_EMPTY_STRING;

    #endregion
}
