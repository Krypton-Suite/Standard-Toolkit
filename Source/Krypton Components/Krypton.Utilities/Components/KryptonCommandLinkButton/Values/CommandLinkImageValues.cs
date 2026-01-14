#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

// ReSharper disable InconsistentNaming
namespace Krypton.Utilities;

public class CommandLinkImageValues : Storage, IContentValues
{
    #region Instance Fields
    private bool _displayUACShield;
    private Color _transparencyKey;
    private Image? _image;
    private IconSize _uacShieldIconSize;
    #endregion

    #region Identity
    /// <summary>Initializes a new instance of the <see cref="CommandLinkImageValues" /> class.</summary>
    /// <param name="needPaint">The need paint.</param>
    public CommandLinkImageValues(NeedPaintHandler needPaint)
    {
        NeedPaint = needPaint;
        _uacShieldIconSize = IconSize.Small;
    }
    #endregion

    #region Public

    [DefaultValue(false)]
    [Description("Display the UAC Shield image.")]
    public bool DisplayUACShield
    {
        get => _displayUACShield;

        set
        {
            if (_displayUACShield != value)
            {
                _displayUACShield = value;
                ShowUACShieldImage(_uacShieldIconSize);
            }
        }
    }
    private bool ShouldSerializeDisplayUACShield() => _displayUACShield;
    private void ResetDisplayUACShield() => DisplayUACShield = false;

    /// <summary>Gets and sets the heading image transparent color.</summary>
    [Category("Visuals")]
    [Description("Image transparent color.")]
    [RefreshProperties(RefreshProperties.All)]
    [KryptonDefaultColor]
    public Color ImageTransparentColor
    {
        get => _transparencyKey;

        set
        {
            if (_transparencyKey != value)
            {
                _transparencyKey = value;
                PerformNeedPaint(true);
            }
        }
    }
    private bool ShouldSerializeImageTransparentColor() => _transparencyKey != GlobalStaticValues.EMPTY_COLOR;
    private void ResetImageTransparentColor() => ImageTransparentColor = GlobalStaticValues.EMPTY_COLOR;

    /// <summary>The UAC image.</summary>
    [Category("Visuals")]
    [Description("The image.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Image? Image
    {
        get => _image;
    }

    [DefaultValue(IconSize.Small)]
    [Description("UAC Shield icon size")]
    public IconSize UACShieldIconSize
    {
        get => _uacShieldIconSize;

        set
        {
            _uacShieldIconSize = value;
            ShowUACShieldImage(value);
        }
    }
    private bool ShouldSerializeUACShieldIconSize() => _uacShieldIconSize != IconSize.Small;
    private void ResetUACShieldIconSize() => UACShieldIconSize = IconSize.Small;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (!ShouldSerializeDisplayUACShield()
        && Image == null
        && !ShouldSerializeImageTransparentColor()
        && !ShouldSerializeUACShieldIconSize());
    #endregion

    #region Implementation

    /// <inheritdoc />
    public Image? GetImage(PaletteState state) => Image;

    /// <inheritdoc />
    public Color GetImageTransparentColor(PaletteState state) => ImageTransparentColor;

    /// <inheritdoc />
    public string GetShortText() => GlobalStaticValues.DEFAULT_EMPTY_STRING;

    /// <inheritdoc />
    public string GetLongText() => GlobalStaticValues.DEFAULT_EMPTY_STRING;

    /// <summary>
    /// Gets the overlay image.
    /// </summary>
    /// <param name="state">The state for which the overlay image is needed.</param>
    /// <returns>Overlay image value, or null if no overlay image is set.</returns>
    public Image? GetOverlayImage(PaletteState state) => null;

    /// <summary>
    /// Gets the overlay image color that should be transparent.
    /// </summary>
    /// <param name="state">The state for which the overlay image is needed.</param>
    /// <returns>Color value.</returns>
    public Color GetOverlayImageTransparentColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the position of the overlay image relative to the main image.
    /// </summary>
    /// <param name="state">The state for which the overlay position is needed.</param>
    /// <returns>Overlay image position.</returns>
    public OverlayImagePosition GetOverlayImagePosition(PaletteState state) => OverlayImagePosition.TopRight;

    /// <summary>
    /// Gets the scaling mode for the overlay image.
    /// </summary>
    /// <param name="state">The state for which the overlay scale mode is needed.</param>
    /// <returns>Overlay image scale mode.</returns>
    public OverlayImageScaleMode GetOverlayImageScaleMode(PaletteState state) => OverlayImageScaleMode.None;

    /// <summary>
    /// Gets the scale factor for the overlay image (used when scale mode is Percentage or ProportionalToMain).
    /// </summary>
    /// <param name="state">The state for which the overlay scale factor is needed.</param>
    /// <returns>Scale factor (0.0 to 2.0).</returns>
    public float GetOverlayImageScaleFactor(PaletteState state) => 0.5f;

    /// <summary>
    /// Gets the fixed size for the overlay image (used when scale mode is FixedSize).
    /// </summary>
    /// <param name="state">The state for which the overlay fixed size is needed.</param>
    /// <returns>Fixed size.</returns>
    public Size GetOverlayImageFixedSize(PaletteState state) => new Size(16, 16);

    /// <summary>Shows the uac shield.</summary>
    /// <param name="shieldIconSize">Size of the shield icon.</param>
    private void ShowUACShieldImage(IconSize shieldIconSize)
    {
        if (_displayUACShield)
        {
            int size = shieldIconSize switch
            {
                IconSize.ExtraSmall => 16,
                IconSize.Small      => 32,
                IconSize.Medium     => 64,
                IconSize.Large      => 128,
                IconSize.ExtraLarge => 256,
                _                   => 32
            };

            _image = GraphicsExtensions.ScaleImage(SystemIcons.Shield.ToBitmap(), new Size(size, size));
        }
        else
        {
            _image = null;
        }

        // Force a repaint
        PerformNeedPaint(true);
    }
    #endregion
}