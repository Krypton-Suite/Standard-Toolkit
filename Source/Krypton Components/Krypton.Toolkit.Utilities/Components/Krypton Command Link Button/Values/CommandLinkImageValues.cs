#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

// ReSharper disable InconsistentNaming
namespace Krypton.Toolkit.Utilities;

public class CommandLinkImageValues : Storage, IContentValues
{
    #region Instance Fields
    private bool _displayUACShield;
    private bool _uacShieldLocked;
    private bool _elevationUsesSlot;
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

    /// <summary>
    /// Gets or sets a value indicating whether the left-hand UAC shield is shown.
    /// </summary>
    /// <remarks>
    /// While <see cref="KryptonCommandLinkButton.ElevatedClick"/> has a handler, the shield is either this slot or the arrow overlay, not both.
    /// Assigning this property is ignored while the arrow overlay owns the shield.
    /// Hiding this slot is ignored while the shield is here.
    /// </remarks>
    [DefaultValue(false)]
    [Description("Display the UAC Shield image.")]
    public bool DisplayUACShield
    {
        get => _displayUACShield;

        set
        {
            if (_uacShieldLocked && _elevationUsesSlot && !value)
            {
                return;
            }

            // The overlay owns the shield. Turning this slot on would show a second one,
            // and turning it off must not remove the overlay.
            if (_uacShieldLocked && !_elevationUsesSlot)
            {
                return;
            }

            if (_displayUACShield != value)
            {
                _displayUACShield = value;
                ShowUACShieldImage(_uacShieldIconSize);
            }

            if (!value)
            {
                UacShieldHidden?.Invoke();
                return;
            }

            UacShieldShown?.Invoke();
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
    private bool ShouldSerializeImageTransparentColor() => _transparencyKey != SharedStaticVariables.EMPTY_COLOR;
    private void ResetImageTransparentColor() => ImageTransparentColor = SharedStaticVariables.EMPTY_COLOR;

    /// <summary>The UAC image.</summary>
    [Category("Visuals")]
    [Description("The image.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Editor(KryptonWinFormsDesignerSdk.ImageEditor, typeof(UITypeEditor))]
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
    public string GetShortText() => SharedStaticVariables.DEFAULT_EMPTY_STRING;

    /// <inheritdoc />
    public string GetLongText() => SharedStaticVariables.DEFAULT_EMPTY_STRING;

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
    public Color GetOverlayImageTransparentColor(PaletteState state) => SharedStaticVariables.EMPTY_COLOR;

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

    /// <summary>
    /// Puts the elevation shield in this slot, or clears the slot when the arrow overlay owns it.
    /// </summary>
    /// <param name="hasContentImage"><c>true</c> when the command-link image is set.</param>
    internal void ApplyElevationPlacement(bool hasContentImage)
    {
        _uacShieldLocked = true;
        _elevationUsesSlot = !hasContentImage;
        if (hasContentImage)
        {
            if (!_displayUACShield)
            {
                return;
            }

            _displayUACShield = false;
            ShowUACShieldImage(_uacShieldIconSize);
            return;
        }

        if (_displayUACShield)
        {
            return;
        }

        _displayUACShield = true;
        ShowUACShieldImage(_uacShieldIconSize);
    }

    /// <summary>
    /// Clears the elevation shield lock. The shield stays until <see cref="DisplayUACShield"/> is set to <c>false</c>.
    /// </summary>
    internal void UnlockUacShieldForElevation()
    {
        _uacShieldLocked = false;
        _elevationUsesSlot = false;
    }

    /// <summary>
    /// Invoked when <see cref="DisplayUACShield"/> is set to <c>false</c> so the arrow overlay can be removed.
    /// </summary>
    internal Action? UacShieldHidden { get; set; }

    /// <summary>
    /// Invoked when <see cref="DisplayUACShield"/> is set to <c>true</c> so the arrow overlay is not shown as well.
    /// </summary>
    internal Action? UacShieldShown { get; set; }

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

            _image = CreateShieldBitmap(size);
        }
        else
        {
            _image = null;
        }

        // Force a repaint
        PerformNeedPaint(true);
    }

    /// <summary>
    /// Draws <see cref="SystemIcons.Shield"/> at <paramref name="size"/> without the dark edge that bicubic scaling leaves on a transparent icon.
    /// </summary>
    /// <param name="size">Target width and height in pixels.</param>
    /// <returns>The shield bitmap, or <c>null</c> when it cannot be created.</returns>
    internal static Bitmap? CreateShieldBitmap(int size)
    {
        if (size < 1)
        {
            return null;
        }

        using Bitmap source = SystemIcons.Shield.ToBitmap();
        if (source.Width == size && source.Height == size)
        {
            return new Bitmap(source);
        }

        return CommonHelper.ScaleImageForSizedDisplay(source, size, size, true);
    }
    #endregion
}
