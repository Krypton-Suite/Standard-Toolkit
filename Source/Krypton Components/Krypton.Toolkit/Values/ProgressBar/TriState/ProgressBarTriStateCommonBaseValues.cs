#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Common base template for progress bar threshold colours and images.
/// Set values here, then assign to Low, Medium, or High regions via AssignFromCommonBaseToLow/Medium/High/All.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ProgressBarTriStateCommonBaseValues : Storage
{
    #region Instance Fields

    private Color _backColor1;
    private Color _backColor2;
    private PaletteColorStyle _backColorStyle;
    private PaletteRectangleAlign _backColorAlign;
    private float _backColorAngle;
    private Image? _backImage;
    private PaletteImageStyle _backImageStyle;
    private PaletteRectangleAlign _backImageAlign;
    private Color _textColor1;
    private Color _textColor2;
    private PaletteColorStyle _textColorStyle;
    private PaletteRectangleAlign _textColorAlign;
    private float _textColorAngle;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the ProgressBarTriStateCommonBaseValues class.
    /// </summary>
    public ProgressBarTriStateCommonBaseValues()
    {
        Reset();
    }

    #endregion

    #region IsDefault

    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault =>
        _backColor1 == Color.Empty &&
        _backColor2 == Color.Empty &&
        _backColorStyle == PaletteColorStyle.Inherit &&
        _backColorAlign == PaletteRectangleAlign.Inherit &&
        Math.Abs(_backColorAngle - (-1f)) < 0.001f &&
        _backImage == null &&
        _backImageStyle == PaletteImageStyle.Inherit &&
        _backImageAlign == PaletteRectangleAlign.Inherit &&
        _textColor1 == Color.Empty &&
        _textColor2 == Color.Empty &&
        _textColorStyle == PaletteColorStyle.Inherit &&
        _textColorAlign == PaletteRectangleAlign.Inherit &&
        Math.Abs(_textColorAngle - (-1f)) < 0.001f;

    #endregion

    #region Back Color

    [Category(@"Visuals")]
    [Description(@"First background color. Empty uses default.")]
    [DefaultValue(typeof(Color), nameof(Color.Empty))]
    [KryptonDefaultColor]
    public Color BackColor1
    {
        get => _backColor1;
        set => _backColor1 = value;
    }

    [Category(@"Visuals")]
    [Description(@"Second background color. Empty uses default.")]
    [DefaultValue(typeof(Color), nameof(Color.Empty))]
    [KryptonDefaultColor]
    public Color BackColor2
    {
        get => _backColor2;
        set => _backColor2 = value;
    }

    [Category(@"Visuals")]
    [Description(@"Background color drawing style.")]
    [DefaultValue(PaletteColorStyle.Inherit)]
    public PaletteColorStyle BackColorStyle
    {
        get => _backColorStyle;
        set => _backColorStyle = value;
    }

    [Category(@"Visuals")]
    [Description(@"Background color alignment.")]
    [DefaultValue(PaletteRectangleAlign.Inherit)]
    public PaletteRectangleAlign BackColorAlign
    {
        get => _backColorAlign;
        set => _backColorAlign = value;
    }

    [Category(@"Visuals")]
    [Description(@"Background color angle. -1 uses default.")]
    [DefaultValue(-1f)]
    public float BackColorAngle
    {
        get => _backColorAngle;
        set => _backColorAngle = value;
    }

    #endregion

    #region Back Image

    [Category(@"Visuals")]
    [Description(@"Background image. Null uses default.")]
    [DefaultValue(null)]
    public Image? BackImage
    {
        get => _backImage;
        set => _backImage = value;
    }

    [Category(@"Visuals")]
    [Description(@"Background image style.")]
    [DefaultValue(PaletteImageStyle.Inherit)]
    public PaletteImageStyle BackImageStyle
    {
        get => _backImageStyle;
        set => _backImageStyle = value;
    }

    [Category(@"Visuals")]
    [Description(@"Background image alignment.")]
    [DefaultValue(PaletteRectangleAlign.Inherit)]
    public PaletteRectangleAlign BackImageAlign
    {
        get => _backImageAlign;
        set => _backImageAlign = value;
    }

    #endregion

    #region Text Color

    [Category(@"Visuals")]
    [Description(@"First text color. Empty uses default.")]
    [DefaultValue(typeof(Color), nameof(Color.Empty))]
    [KryptonDefaultColor]
    public Color TextColor1
    {
        get => _textColor1;
        set => _textColor1 = value;
    }

    [Category(@"Visuals")]
    [Description(@"Second text color. Empty uses default.")]
    [DefaultValue(typeof(Color), nameof(Color.Empty))]
    [KryptonDefaultColor]
    public Color TextColor2
    {
        get => _textColor2;
        set => _textColor2 = value;
    }

    [Category(@"Visuals")]
    [Description(@"Text color drawing style.")]
    [DefaultValue(PaletteColorStyle.Inherit)]
    public PaletteColorStyle TextColorStyle
    {
        get => _textColorStyle;
        set => _textColorStyle = value;
    }

    [Category(@"Visuals")]
    [Description(@"Text color alignment.")]
    [DefaultValue(PaletteRectangleAlign.Inherit)]
    public PaletteRectangleAlign TextColorAlign
    {
        get => _textColorAlign;
        set => _textColorAlign = value;
    }

    [Category(@"Visuals")]
    [Description(@"Text color angle. -1 uses default.")]
    [DefaultValue(-1f)]
    public float TextColorAngle
    {
        get => _textColorAngle;
        set => _textColorAngle = value;
    }

    #endregion

    #region Reset

    /// <summary>
    /// Resets all values to their default.
    /// </summary>
    public void Reset()
    {
        _backColor1 = Color.Empty;
        _backColor2 = Color.Empty;
        _backColorStyle = PaletteColorStyle.Inherit;
        _backColorAlign = PaletteRectangleAlign.Inherit;
        _backColorAngle = -1f;
        _backImage = null;
        _backImageStyle = PaletteImageStyle.Inherit;
        _backImageAlign = PaletteRectangleAlign.Inherit;
        _textColor1 = Color.Empty;
        _textColor2 = Color.Empty;
        _textColorStyle = PaletteColorStyle.Inherit;
        _textColorAlign = PaletteRectangleAlign.Inherit;
        _textColorAngle = -1f;
    }

    #endregion

    #region Public Overrides

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticValues.DEFAULT_EMPTY_STRING;

    #endregion
}
