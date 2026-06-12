#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// A control that displays a QR code. Generates QR codes natively without external dependencies.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonPanel), "ToolboxBitmaps.KryptonPanel.bmp")]
[DefaultProperty(nameof(Content))]
[DefaultEvent(nameof(ContentChanged))]
[DesignerCategory(@"code")]
[Description(@"Displays a QR code generated from the specified content. Uses native generation without external packages.")]
public class KryptonQRCode : KryptonPanel
{
    #region Instance Fields

    private string _content = string.Empty;
    private bool[,]? _moduleMatrix;
    private QRErrorCorrectionLevel _errorCorrectionLevel = QRErrorCorrectionLevel.M;
    private int _moduleSize = 4;
    /// <summary><see cref="Color.Empty"/> means use the current Krypton palette (label text and panel client back).</summary>
    private Color _darkColor = Color.Empty;
    /// <summary><see cref="Color.Empty"/> means use the current Krypton palette.</summary>
    private Color _lightColor = Color.Empty;
    private bool _showBorder = true;
    private Image? _centerImage;
    private float _centerImageRelativeSize = 0.22f;
    private int _centerImagePaddingModules = 1;
    private bool _centerImageUsePaletteColors;
    private PaletteContentStyle _centerImagePaletteStyle = PaletteContentStyle.LabelNormalPanel;
    private Color _centerImageColorMap = Color.Empty;
    private Color _centerImageColorTo = Color.Empty;
    private Color _centerImageTransparentColor = Color.Empty;
    private PaletteImageEffect _centerImageEffect = PaletteImageEffect.Inherit;

    #endregion

    #region Events

    /// <summary>Occurs when the content changes and the QR code is regenerated.</summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the content changes.")]
    public event EventHandler? ContentChanged;

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the content to encode in the QR code.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue("")]
    [Description(@"The text or data to encode in the QR code (UTF-8).")]
    public string Content
    {
        get => _content;
        set
        {
            if (_content != value)
            {
                _content = value;
                Regenerate();
                ContentChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Gets or sets the error correction level.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(QRErrorCorrectionLevel.M)]
    [Description(@"The error correction level. Higher levels allow more data recovery but reduce capacity.")]
    public QRErrorCorrectionLevel ErrorCorrectionLevel
    {
        get => _errorCorrectionLevel;
        set
        {
            if (_errorCorrectionLevel != value)
            {
                _errorCorrectionLevel = value;
                Regenerate();
            }
        }
    }

    /// <summary>
    /// Gets or sets the size of each QR module in pixels.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(4)]
    [Description(@"The size of each QR code module (pixel) in the rendered image.")]
    public int ModuleSize
    {
        get => _moduleSize;
        set
        {
            if (_moduleSize != value && value >= 1 && value <= 20)
            {
                _moduleSize = value;
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets or sets the color for dark (filled) modules.
    /// </summary>
    /// <remarks>When <see cref="Color.Empty"/>, dark modules use the palette content color for
    /// <see cref="PaletteContentStyle.LabelNormalPanel"/> (typical control text on a panel).</remarks>
    [Category(@"Appearance")]
    [Description(@"The color for dark modules. Leave empty to use the current Krypton theme (label text on panel).")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(typeof(Color), "")]
    public Color DarkColor
    {
        get => _darkColor;
        set
        {
            if (_darkColor != value)
            {
                _darkColor = value;
                Invalidate();
            }
        }
    }

    private bool ShouldSerializeDarkColor() => !_darkColor.IsEmpty;

    private void ResetDarkColor()
    {
        DarkColor = Color.Empty;
    }

    /// <summary>
    /// Gets or sets the color for light (empty) modules.
    /// </summary>
    /// <remarks>When <see cref="Color.Empty"/>, light modules use the panel client background from the current state.</remarks>
    [Category(@"Appearance")]
    [Description(@"The color for light modules. Leave empty to use the current Krypton theme (panel client background).")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(typeof(Color), "")]
    public Color LightColor
    {
        get => _lightColor;
        set
        {
            if (_lightColor != value)
            {
                _lightColor = value;
                Invalidate();
            }
        }
    }

    private bool ShouldSerializeLightColor() => !_lightColor.IsEmpty;

    private void ResetLightColor()
    {
        LightColor = Color.Empty;
    }

    /// <summary>
    /// Gets or sets whether to show a border (quiet zone) around the QR code.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(true)]
    [Description(@"Whether to show a quiet zone (white border) around the QR code.")]
    public bool ShowBorder
    {
        get => _showBorder;
        set
        {
            if (_showBorder != value)
            {
                _showBorder = value;
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets or sets an optional image drawn in the center of the QR symbol (overwriting modules in that region).
    /// </summary>
    /// <remarks>
    /// The control does not take ownership; you must dispose the image when finished.
    /// Larger images or higher data density increase scan failures—prefer a higher <see cref="ErrorCorrectionLevel"/> (e.g. H or Q).
    /// Set <see cref="CenterImageUsePaletteColors"/> to apply palette image effects and color remapping (see
    /// <see cref="CenterImageColorMap"/>, <see cref="CenterImageColorTo"/>, <see cref="CenterImageTransparentColor"/>,
    /// and <see cref="CenterImageEffect"/>).
    /// </remarks>
    [Category(@"Appearance")]
    [DefaultValue(null)]
    [Description(@"Optional image in the center of the code. Null disables. Use strong error correction for reliable scanning.")]
    public Image? CenterImage
    {
        get => _centerImage;
        set
        {
            if (!ReferenceEquals(_centerImage, value))
            {
                _centerImage = value;
                Invalidate();
            }
        }
    }

    private bool ShouldSerializeCenterImage() => _centerImage != null;

    private void ResetCenterImage()
    {
        CenterImage = null;
    }

    /// <summary>
    /// Gets or sets approximate size of the centered image area as a fraction of the symbol side (0.05–0.45).
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(0.22f)]
    [Description(@"Size of the center logo area relative to the QR symbol side. Clamped to roughly 5%–45%.")]
    public float CenterImageRelativeSize
    {
        get => _centerImageRelativeSize;
        set
        {
            float v = value;
            if (v < 0.05f)
            {
                v = 0.05f;
            }
            else if (v > 0.45f)
            {
                v = 0.45f;
            }

            if (Math.Abs(_centerImageRelativeSize - v) > float.Epsilon)
            {
                _centerImageRelativeSize = v;
                Invalidate();
            }
        }
    }

    private bool ShouldSerializeCenterImageRelativeSize() => Math.Abs(_centerImageRelativeSize - 0.22f) > float.Epsilon;

    private void ResetCenterImageRelativeSize()
    {
        CenterImageRelativeSize = 0.22f;
    }

    /// <summary>
    /// Gets or sets padding (in modules) between the outer cleared square and the scaled <see cref="CenterImage"/>.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(1)]
    [Description(@"Light padding around the logo, expressed in module widths.")]
    public int CenterImagePaddingModules
    {
        get => _centerImagePaddingModules;
        set
        {
            int v = value;
            if (v < 0)
            {
                v = 0;
            }
            else if (v > 8)
            {
                v = 8;
            }

            if (_centerImagePaddingModules != v)
            {
                _centerImagePaddingModules = v;
                Invalidate();
            }
        }
    }

    private bool ShouldSerializeCenterImagePaddingModules() => _centerImagePaddingModules != 1;

    private void ResetCenterImagePaddingModules()
    {
        CenterImagePaddingModules = 1;
    }

    /// <summary>
    /// Gets or sets whether the <see cref="CenterImage"/> is drawn using palette image effect and color-remap rules.
    /// </summary>
    /// <remarks>
    /// When enabled, <see cref="CenterImageEffect"/>, <see cref="CenterImageColorMap"/>,
    /// <see cref="CenterImageColorTo"/>, and <see cref="CenterImageTransparentColor"/> use
    /// <see cref="Color.Empty"/> / <see cref="PaletteImageEffect.Inherit"/> to inherit from
    /// <see cref="CenterImagePaletteStyle"/> on the current palette. Template glyphs that use the
    /// Krypton transparency key (<see cref="GlobalStaticVariables.TRANSPARENCY_KEY_COLOR"/>) are
    /// remapped to the effective dark module color when the palette does not specify a map color.
    /// </remarks>
    [Category(@"Appearance")]
    [DefaultValue(false)]
    [Description(@"Apply palette image effect and color remapping to the center image.")]
    public bool CenterImageUsePaletteColors
    {
        get => _centerImageUsePaletteColors;
        set
        {
            if (_centerImageUsePaletteColors != value)
            {
                _centerImageUsePaletteColors = value;
                Invalidate();
            }
        }
    }

    private bool ShouldSerializeCenterImageUsePaletteColors() => _centerImageUsePaletteColors;

    private void ResetCenterImageUsePaletteColors()
    {
        CenterImageUsePaletteColors = false;
    }

    /// <summary>
    /// Gets or sets the palette content style used when resolving inherited center-image colors and effects.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(PaletteContentStyle.LabelNormalPanel)]
    [Description(@"Palette content style for center image color map, effect, and transparency.")]
    public PaletteContentStyle CenterImagePaletteStyle
    {
        get => _centerImagePaletteStyle;
        set
        {
            if (_centerImagePaletteStyle != value)
            {
                _centerImagePaletteStyle = value;
                if (_centerImageUsePaletteColors)
                {
                    Invalidate();
                }
            }
        }
    }

    private bool ShouldSerializeCenterImagePaletteStyle() => _centerImagePaletteStyle != PaletteContentStyle.LabelNormalPanel;

    private void ResetCenterImagePaletteStyle()
    {
        CenterImagePaletteStyle = PaletteContentStyle.LabelNormalPanel;
    }

    /// <summary>
    /// Gets or sets the source color remapped when drawing <see cref="CenterImage"/> with palette colors enabled.
    /// </summary>
    /// <remarks><see cref="Color.Empty"/> inherits from the palette (<see cref="CenterImagePaletteStyle"/>).</remarks>
    [Category(@"Appearance")]
    [Description(@"Source color to remap in the center image. Empty inherits from the palette.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(typeof(Color), "")]
    public Color CenterImageColorMap
    {
        get => _centerImageColorMap;
        set
        {
            if (_centerImageColorMap != value)
            {
                _centerImageColorMap = value;
                if (_centerImageUsePaletteColors)
                {
                    Invalidate();
                }
            }
        }
    }

    private bool ShouldSerializeCenterImageColorMap() => !_centerImageColorMap.IsEmpty;

    private void ResetCenterImageColorMap()
    {
        CenterImageColorMap = Color.Empty;
    }

    /// <summary>
    /// Gets or sets the destination color used when remapping <see cref="CenterImageColorMap"/>.
    /// </summary>
    /// <remarks><see cref="Color.Empty"/> inherits from the palette, then the effective dark module color.</remarks>
    [Category(@"Appearance")]
    [Description(@"Target color for center image remapping. Empty inherits from the palette.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(typeof(Color), "")]
    public Color CenterImageColorTo
    {
        get => _centerImageColorTo;
        set
        {
            if (_centerImageColorTo != value)
            {
                _centerImageColorTo = value;
                if (_centerImageUsePaletteColors)
                {
                    Invalidate();
                }
            }
        }
    }

    private bool ShouldSerializeCenterImageColorTo() => !_centerImageColorTo.IsEmpty;

    private void ResetCenterImageColorTo()
    {
        CenterImageColorTo = Color.Empty;
    }

    /// <summary>
    /// Gets or sets the color in <see cref="CenterImage"/> that should be drawn transparent.
    /// </summary>
    /// <remarks><see cref="Color.Empty"/> inherits from the palette (<see cref="CenterImagePaletteStyle"/>).</remarks>
    [Category(@"Appearance")]
    [Description(@"Center image color drawn transparent. Empty inherits from the palette.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(typeof(Color), "")]
    public Color CenterImageTransparentColor
    {
        get => _centerImageTransparentColor;
        set
        {
            if (_centerImageTransparentColor != value)
            {
                _centerImageTransparentColor = value;
                if (_centerImageUsePaletteColors)
                {
                    Invalidate();
                }
            }
        }
    }

    private bool ShouldSerializeCenterImageTransparentColor() => !_centerImageTransparentColor.IsEmpty;

    private void ResetCenterImageTransparentColor()
    {
        CenterImageTransparentColor = Color.Empty;
    }

    /// <summary>
    /// Gets or sets the palette image effect applied to <see cref="CenterImage"/> when palette drawing is enabled.
    /// </summary>
    /// <remarks><see cref="PaletteImageEffect.Inherit"/> uses the palette value for <see cref="CenterImagePaletteStyle"/>.</remarks>
    [Category(@"Appearance")]
    [DefaultValue(PaletteImageEffect.Inherit)]
    [Description(@"Image effect for the center image. Inherit uses the palette content style.")]
    public PaletteImageEffect CenterImageEffect
    {
        get => _centerImageEffect;
        set
        {
            if (_centerImageEffect != value)
            {
                _centerImageEffect = value;
                if (_centerImageUsePaletteColors)
                {
                    Invalidate();
                }
            }
        }
    }

    private bool ShouldSerializeCenterImageEffect() => _centerImageEffect != PaletteImageEffect.Inherit;

    private void ResetCenterImageEffect()
    {
        CenterImageEffect = PaletteImageEffect.Inherit;
    }

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonQRCode" /> class.</summary>
    public KryptonQRCode()
    {
        Size = new Size(120, 120);
        MinimumSize = new Size(50, 50);
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Resolves palette image drawing settings for <see cref="CenterImage"/>.
    /// </summary>
    public QRCodeCenterImagePalette GetCenterImagePalette() => GetResolvedCenterImagePalette();

    /// <summary>
    /// Generates a bitmap of the current QR code.
    /// </summary>
    /// <returns>A bitmap of the QR code, or null if no content.</returns>
    public Bitmap? GetBitmap()
    {
        if (_moduleMatrix == null) return null;
        return QRCodeBitmapRenderer.Render(
            _moduleMatrix,
            _moduleSize,
            GetEffectiveDarkModuleColor(),
            GetEffectiveLightModuleColor(),
            _showBorder,
            _centerImage,
            _centerImageRelativeSize,
            _centerImagePaddingModules,
            GetResolvedCenterImagePalette());
    }

    /// <summary>
    /// Generates a QR code bitmap for the given content.
    /// </summary>
    /// <param name="content">The content to encode.</param>
    /// <param name="moduleSize">The module size in pixels.</param>
    /// <param name="eccLevel">The error correction level.</param>
    /// <param name="darkColor">Color for dark modules.</param>
    /// <param name="lightColor">Color for light modules.</param>
    /// <param name="centerImage">Optional centered image; null for none.</param>
    /// <param name="centerImageRelativeSize">Center image area as fraction of symbol side (0.05–0.45).</param>
    /// <param name="centerImagePaddingModules">Padding in modules between cleared area and image.</param>
    /// <param name="centerImagePalette">Optional resolved palette settings for the center image.</param>
    /// <returns>A bitmap of the QR code.</returns>
    public static Bitmap GenerateBitmap(
        string content,
        int moduleSize = 4,
        QRErrorCorrectionLevel eccLevel = QRErrorCorrectionLevel.M,
        Color? darkColor = null,
        Color? lightColor = null,
        Image? centerImage = null,
        float centerImageRelativeSize = 0.22f,
        int centerImagePaddingModules = 1,
        QRCodeCenterImagePalette centerImagePalette = default)
    {
        bool[,] matrix = QRCodeGeneratorCore.Generate(content, eccLevel);
        return QRCodeBitmapRenderer.Render(
            matrix,
            moduleSize,
            darkColor ?? Color.Black,
            lightColor ?? Color.White,
            true,
            centerImage,
            centerImageRelativeSize,
            centerImagePaddingModules,
            centerImagePalette);
    }

    /// <summary>
    /// Saves the current QR code to a file.
    /// </summary>
    /// <param name="path">The file path.</param>
    /// <param name="format">The image format (e.g. PNG).</param>
    public void SaveToFile(string path, ImageFormat format)
    {
        using Bitmap? bmp = GetBitmap();
        bmp?.Save(path, format);
    }

    #endregion

    #region Overrides

    /// <inheritdoc />
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (_moduleMatrix == null) return;

        Graphics g = e.Graphics;
        g.InterpolationMode = InterpolationMode.NearestNeighbor;
        g.PixelOffsetMode = PixelOffsetMode.Half;

        int border = _showBorder ? _moduleSize * 4 : 0;
        int matrixSize = _moduleMatrix.GetLength(0);
        int pixelSize = matrixSize * _moduleSize;

        int x = (Width - pixelSize - border * 2) / 2 + border;
        int y = (Height - pixelSize - border * 2) / 2 + border;

        Color dark = GetEffectiveDarkModuleColor();
        Color light = GetEffectiveLightModuleColor();
        for (int row = 0; row < matrixSize; row++)
        {
            for (int col = 0; col < matrixSize; col++)
            {
                using var brush = new SolidBrush(_moduleMatrix[row, col] ? dark : light);
                g.FillRectangle(brush, x + col * _moduleSize, y + row * _moduleSize, _moduleSize, _moduleSize);
            }
        }

        QRCodeBitmapRenderer.DrawCenterImageIfAny(
            g,
            matrixSize,
            _moduleSize,
            x,
            y,
            light,
            _centerImage,
            _centerImageRelativeSize,
            _centerImagePaddingModules,
            GetResolvedCenterImagePalette());
    }

    #endregion

    #region Implementation

    private Color GetEffectiveDarkModuleColor()
    {
        if (!_darkColor.IsEmpty)
        {
            return _darkColor;
        }

        PaletteState state = Enabled ? PaletteState.Normal : PaletteState.Disabled;
        return GetResolvedPalette().GetContentShortTextColor1(PaletteContentStyle.LabelNormalPanel, state);
    }

    private Color GetEffectiveLightModuleColor()
    {
        if (!_lightColor.IsEmpty)
        {
            return _lightColor;
        }

        PaletteBack back = Enabled ? StateNormal : StateDisabled;
        PaletteState state = Enabled ? PaletteState.Normal : PaletteState.Disabled;
        return back.GetBackColor1(state);
    }

    private QRCodeCenterImagePalette GetResolvedCenterImagePalette()
    {
        if (!_centerImageUsePaletteColors)
        {
            return new QRCodeCenterImagePalette(false);
        }

        PaletteState state = Enabled ? PaletteState.Normal : PaletteState.Disabled;
        PaletteBase palette = GetResolvedPalette();
        PaletteContentStyle style = _centerImagePaletteStyle;

        PaletteImageEffect effect = _centerImageEffect != PaletteImageEffect.Inherit
            ? _centerImageEffect
            : palette.GetContentImageEffect(style, state);
        if (effect == PaletteImageEffect.Inherit)
        {
            effect = PaletteImageEffect.Normal;
        }

        Color colorMap = !_centerImageColorMap.IsEmpty
            ? _centerImageColorMap
            : palette.GetContentImageColorMap(style, state);
        if (colorMap.IsEmpty)
        {
            colorMap = GlobalStaticVariables.TRANSPARENCY_KEY_COLOR;
        }

        Color colorTo = !_centerImageColorTo.IsEmpty
            ? _centerImageColorTo
            : palette.GetContentImageColorTo(style, state);
        if (colorTo.IsEmpty)
        {
            colorTo = GetEffectiveDarkModuleColor();
        }

        Color transparentColor = !_centerImageTransparentColor.IsEmpty
            ? _centerImageTransparentColor
            : palette.GetContentImageColorTransparent(style, state);

        return new QRCodeCenterImagePalette(true, effect, colorMap, colorTo, transparentColor);
    }

    private void Regenerate()
    {
        if (string.IsNullOrEmpty(_content))
        {
            _moduleMatrix = null;
        }
        else
        {
            try
            {
                _moduleMatrix = QRCodeGeneratorCore.Generate(_content, _errorCorrectionLevel);
            }
            catch (ArgumentException aex)
            {
                KryptonExceptionHandler.CaptureException(aex);

                _moduleMatrix = null;
            }
        }
        Invalidate();
    }

    #endregion
}
