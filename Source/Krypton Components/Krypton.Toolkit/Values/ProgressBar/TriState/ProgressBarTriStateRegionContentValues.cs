#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Holds content (text) properties for a progress bar threshold region state.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ProgressBarTriStateRegionContentValues : Storage
{
    #region Instance Fields

    private readonly ProgressBarTriStateRegionStateValues _parent;
    private Color _color1;
    private Color _color2;
    private PaletteColorStyle _colorStyle;
    private PaletteRectangleAlign _colorAlign;
    private float _colorAngle;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the ProgressBarTriStateRegionContentValues class.
    /// </summary>
    /// <param name="parent">Reference to owning state.</param>
    public ProgressBarTriStateRegionContentValues(ProgressBarTriStateRegionStateValues parent)
    {
        _parent = parent;
        _color1 = Color.Empty;
        _color2 = Color.Empty;
        _colorStyle = PaletteColorStyle.Inherit;
        _colorAlign = PaletteRectangleAlign.Inherit;
        _colorAngle = -1f;
    }

    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    public override bool IsDefault => _color1 == Color.Empty &&
                             _color2 == Color.Empty &&
                             _colorStyle == PaletteColorStyle.Inherit &&
                             _colorAlign == PaletteRectangleAlign.Inherit &&
                             Math.Abs(_colorAngle - (-1f)) < 0.001f;

    #endregion

    #region Public

    [Category(@"Visuals")]
    [Description(@"First text/content color. Empty uses default or opposite when UseOppositeTextColors is enabled.")]
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
            _parent.OnContentChanged();
        }
    }

    [Category(@"Visuals")]
    [Description(@"Second text/content color. Empty uses default.")]
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
            _parent.OnContentChanged();
        }
    }

    [Category(@"Visuals")]
    [Description(@"Text/content color drawing style.")]
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
            _parent.OnContentChanged();
        }
    }

    [Category(@"Visuals")]
    [Description(@"Text/content color alignment.")]
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
            _parent.OnContentChanged();
        }
    }

    [Category(@"Visuals")]
    [Description(@"Text/content color angle. -1 uses default.")]
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
            _parent.OnContentChanged();
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
        _parent.OnContentChanged();
    }

    #endregion

    #region Public Overrides

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticValues.DEFAULT_EMPTY_STRING;

    #endregion
}
