#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for button content value information.
/// </summary>
public class ButtonValues : Storage,
    IContentValues
{
    #region Static Fields
    private const string DEFAULT_TEXT = nameof(Button);
    private static readonly string _defaultExtraText = SharedStaticVariables.DEFAULT_EMPTY_STRING;
    #endregion

    #region Instance Fields

    private bool _useAsDialogButton;
    private bool _useAsUACElevationButton;
    private bool _uacShieldLocked;
    private bool _applyingUacShield;
    private bool _uacShieldOwnsImage;
    private bool _uacShieldOwnsOverlay;
    private bool _showSplitOption;
    private IconSize? _iconSize;
    private IconSelectionStrategy _iconSelectionStrategy;
    private Image? _image;
    private Image? _factoryImage;
    private bool _imageIsDefault;
    private Color _transparent;
    private Color? _dropDownArrowColor;
    private string? _text;
    private string _defaultText;
    private string _extraText;
    private Size? _customIconSize;
    private readonly OverlayImageValues _overlayImage;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the value of the Text property changes.
    /// </summary>
    public event EventHandler? TextChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonValues class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ButtonValues(NeedPaintHandler needPaint)
    {
        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Set initial values
        _image = null;
        _factoryImage = null;
        _imageIsDefault = true;
        _transparent = SharedStaticVariables.EMPTY_COLOR;
        _dropDownArrowColor = null;
        _defaultText = DEFAULT_TEXT;
        _text = _defaultText;
        _extraText = _defaultExtraText;
        _useAsDialogButton = false;
        _useAsUACElevationButton = false;
        _showSplitOption = false;
        _iconSize = IconSize.ExtraSmall; // Default to smallest size
        _iconSelectionStrategy = IconSelectionStrategy.OSBased; // Default to OS based strategy
        ImageStates = CreateImageStates();
        ImageStates.NeedPaint = needPaint;
        _overlayImage = CreateOverlayImageValues(needPaint);
        _overlayImage.RejectImageChange = RejectUacShieldOverlayChange;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => ImageStates.IsDefault &&
                                      _imageIsDefault &&
                                      (UseAsADialogButton == false) &&
                                      (UseAsUACElevationButton == false) &&
                                      (ShowSplitOption == false) &&
                                      (DropDownArrowColor == null) &&
                                      (ImageTransparentColor == SharedStaticVariables.EMPTY_COLOR) &&
                                      (Text == _defaultText) &&
                                      (ExtraText == _defaultExtraText) &&
                                      _overlayImage.IsDefault;

    /// <summary>
    /// Treats <paramref name="text"/> as the unset designer default for <see cref="Text"/>.
    /// </summary>
    internal void SetFactoryText(string text)
    {
        _defaultText = text ?? SharedStaticVariables.DEFAULT_EMPTY_STRING;
        _text = _defaultText;
    }

    /// <summary>
    /// Treats <paramref name="image"/> as the unset designer default for <see cref="Image"/>.
    /// </summary>
    internal void SetFactoryImage(Image? image)
    {
        _factoryImage = image;
        _image = image;
        _imageIsDefault = true;
    }

    #endregion

    #region Image
    /// <summary>
    /// Gets and sets the button image.
    /// </summary>
    /// <remarks>
    /// When the UAC shield is showing and this image is set, the shield moves to <see cref="OverlayImage"/>.
    /// Clearing it while <see cref="KryptonButton.ElevatedClick"/> has a handler moves the shield back here so it stays visible.
    /// </remarks>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Button image.")]
    [RefreshProperties(RefreshProperties.All)]
    [Editor(KryptonWinFormsDesignerSdk.ImageEditor, typeof(UITypeEditor))]
    public Image? Image
    {
        get => _image;

        set
        {
            if (_applyingUacShield)
            {
                SetImageCore(value);
                return;
            }

            if (value == null && _useAsUACElevationButton)
            {
                MoveUacShieldToImage();
                return;
            }

            bool changed = _image != value;
            if (changed)
            {
                _image = value;
                _imageIsDefault = false;
                _uacShieldOwnsImage = false;
                PerformNeedPaint(true);
            }

            if (changed && value != null && _useAsUACElevationButton)
            {
                ShowUACShieldImage(true, _iconSize ?? IconSize.ExtraSmall);
            }
        }
    }

    private bool ShouldSerializeImage() => !_imageIsDefault;

    /// <summary>
    /// Resets the Image property to its default value.
    /// </summary>
    /// <remarks>
    /// When the UAC shield is showing, a factory image stays the content image and the shield moves to <see cref="OverlayImage"/>.
    /// A null factory image moves the shield back onto <see cref="Image"/>.
    /// </remarks>
    public void ResetImage()
    {
        if (_useAsUACElevationButton)
        {
            Image = _factoryImage;
            if (_factoryImage != null && _uacShieldOwnsOverlay)
            {
                _imageIsDefault = true;
            }

            return;
        }

        _image = _factoryImage;
        _imageIsDefault = true;
        _uacShieldOwnsImage = false;
        PerformNeedPaint(true);
    }
    #endregion

    #region ImageTransparentColor
    /// <summary>
    /// Gets and sets the label image transparent color.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Label image transparent color.")]
    [RefreshProperties(RefreshProperties.All)]
    [KryptonDefaultColor]
    public Color ImageTransparentColor
    {
        get => _transparent;

        set
        {
            if (_transparent != value)
            {
                _transparent = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeImageTransparentColor() => ImageTransparentColor != SharedStaticVariables.EMPTY_COLOR;

    /// <summary>
    /// Resets the ImageTransparentColor property to its default value.
    /// </summary>
    public void ResetImageTransparentColor() => ImageTransparentColor = SharedStaticVariables.EMPTY_COLOR;

    /// <summary>
    /// Gets the content image transparent color.
    /// </summary>
    /// <param name="state">The state for which the image color is needed.</param>
    /// <returns>Color value.</returns>
    public Color GetImageTransparentColor(PaletteState state) => ImageTransparentColor;

    #endregion

    #region ImageStates
    /// <summary>
    /// Gets access to the state specific images for the button.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"State specific images for the button.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ButtonImageStates ImageStates { get; }

    private bool ShouldSerializeImageStates() => !ImageStates.IsDefault;

    #endregion

    #region Text
    /// <summary>
    /// Gets and sets the button text.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Button text.")]
    [RefreshProperties(RefreshProperties.All)]
    // ToDo V120 LTS: Migrate designer editor to KryptonDesignerMultilineStringEditor (replaces System.ComponentModel.Design.MultilineStringEditor).
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [AllowNull]
    public string Text
    {
        get => _text ?? SharedStaticVariables.DEFAULT_EMPTY_STRING;

        set
        {
            if (_text != value)
            {
                _text = value;
                PerformNeedPaint(true);
                TextChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private bool ShouldSerializeText() => Text != _defaultText;

    /// <summary>
    /// Resets the Text property to its default value.
    /// </summary>
    public void ResetText() => Text = _defaultText;
    #endregion

    #region ExtraText
    /// <summary>
    /// Gets and sets the button extra text.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Button extra text.")]
    [RefreshProperties(RefreshProperties.All)]
    // ToDo V120 LTS: Migrate designer editor to KryptonDesignerMultilineStringEditor (replaces System.ComponentModel.Design.MultilineStringEditor).
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [DefaultValue(@"")]
    public string ExtraText
    {
        get => _extraText;

        set
        {
            if (_extraText != value)
            {
                _extraText = value;
                PerformNeedPaint(true);
            }
        }
    }

    private void ResetExtraText() => ExtraText = _defaultExtraText;
    private bool ShouldSerializeExtraText() => ExtraText != _defaultExtraText;

    #endregion

    #region UseAsADialogButton

    [DefaultValue(false),
     Description(@"If set to true, the text will pair up with the equivalent KryptonManager's dialog button text result. (Note: You'll lose any previous text)")]
    public bool UseAsADialogButton
    {
        get => _useAsDialogButton;
        set => _useAsDialogButton = value;
    }

    #endregion

    #region UseAsUACElevationButton

    /// <summary>
    /// Gets or sets a value indicating whether the button shows the UAC shield.
    /// </summary>
    /// <value><c>true</c> when the button shows the UAC shield; otherwise, <c>false</c>.</value>
    /// <remarks>
    /// Setting this to <c>false</c> is ignored while <see cref="KryptonButton.ElevatedClick"/> has a handler.
    /// </remarks>
    [DefaultValue(false),
     Description(@"Transforms the button into a UAC elevated button.")]
    public bool UseAsUACElevationButton
    {
        get => _useAsUACElevationButton;
        set
        {
            if (_uacShieldLocked && !value)
            {
                return;
            }

            _useAsUACElevationButton = value;

            ShowUACShield(value, _iconSize ?? IconSize.ExtraSmall);
        }
    }

    #endregion

    #region UseOSUACShieldIcon

    /*
    [DefaultValue(false), Description(@"Use the operating system UAC shield icon image.")]
    public bool UseOSUACShieldIcon
    {
    get => _useOSUACShieldIcon;

    set
    {
    _useOSUACShieldIcon = value;

    UpdateOSUACShieldIcon();
    }
    }
    */

    #endregion

    #region CustomUACShieldSize

    /*
    [DefaultValue(null), Description(@"")]
    public Size CustomUACShieldSize
    {
    get => _customUACShieldSize;

    set
    { _customUACShieldSize = value;

    ShowUACShield(_useAsUACElevationButton, UACShieldIconSize.Custom, value.Width, value.Height);
    }
    }
    */

    #endregion

    #region UACShieldIconSize

    /// <summary>Gets or sets the size of the UAC shield icon.</summary>
    /// <value>The size of the UAC shield icon.</value>
    [DefaultValue(IconSize.ExtraSmall), Description(@"The size of the UAC shield icon.")]
    public IconSize UACShieldIconSize
    {
        get => _iconSize ?? IconSize.ExtraSmall;

        set
        {
            _iconSize = value;

            ShowUACShieldImage(_useAsUACElevationButton, value);
        }
    }

    #endregion

    #region IconSelectionStrategy

    /// <summary>Gets or sets the strategy for selecting UAC shield icons.</summary>
    /// <value>The strategy for selecting UAC shield icons.</value>
    [DefaultValue(IconSelectionStrategy.OSBased), Description(@"The strategy for selecting UAC shield icons (OS-based or theme-based).")]
    public IconSelectionStrategy IconSelectionStrategy
    {
        get => _iconSelectionStrategy;

        set
        {
            if (_iconSelectionStrategy != value)
            {
                _iconSelectionStrategy = value;

                // Refresh the UAC shield if it's currently enabled
                if (_useAsUACElevationButton)
                {
                    ShowUACShieldImage(true, _iconSize);
                }
            }
        }
    }

    #endregion

    #region ShowSpltOption

    /// <summary>Gets or sets a value indicating whether [show split option].</summary>
    /// <value><c>true</c> if [show split option]; otherwise, <c>false</c>.</value>
    [Category(@"Visuals")]
    [DefaultValue(false)]
    [Description(@"Displays the split/dropdown option.")]
    public bool ShowSplitOption
    {
        get => _showSplitOption;

        set
        {
            if (value != _showSplitOption)
            {
                _showSplitOption = value;

                PerformNeedPaint(true);

                //Parent?.PerformLayout();
            }
        }
    }

    #endregion

    #region DropDownArrowColor

    /// <summary>Gets or sets the color of the drop-down arrow.</summary>
    /// <value>The color of the drop-down arrow.</value>
    [Category(@"Visuals")]
    [Description(@"Sets the drop-down arrow color.")]
    [DefaultValue(null)]
    public Color? DropDownArrowColor
    {
        get => _dropDownArrowColor;

        set
        {
            Color? normalized = value is Color { IsEmpty: true } ? null : value;
            if (_dropDownArrowColor != normalized)
            {
                _dropDownArrowColor = normalized;

                PerformNeedPaint(true);
            }
        }
    }
    private void ResetDropDownArrowColor() => _dropDownArrowColor = null;
    public bool ShouldSerializeDropDownArrowColor() =>
        _dropDownArrowColor is { IsEmpty: false };
    #endregion

    #region CreateImageStates
    /// <summary>
    /// Create the storage for the image states.
    /// </summary>
    /// <returns>Storage object.</returns>
    protected virtual ButtonImageStates CreateImageStates() => new ButtonImageStates();

    #endregion

    #region CreateOverlayImageValues
    /// <summary>
    /// Create the storage for the overlay image values.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    /// <returns>Storage object.</returns>
    protected virtual OverlayImageValues CreateOverlayImageValues(NeedPaintHandler needPaint) =>
        new OverlayImageValues(needPaint);

    #endregion

    #region OverlayImage
    /// <summary>
    /// Gets access to the overlay image values.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overlay image values.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public OverlayImageValues OverlayImage => _overlayImage;

    private bool ShouldSerializeOverlayImage() => !_overlayImage.IsDefault;

    #endregion

    #region IContentValues
    /// <summary>
    /// Gets the content image.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Image value.</returns>
    public virtual Image? GetImage(PaletteState state)
    {
        // Try and find a state specific image
        Image? image = state switch
        {
            PaletteState.Disabled => ImageStates.ImageDisabled,
            PaletteState.Normal => ImageStates.ImageNormal,
            PaletteState.Pressed => ImageStates.ImagePressed,
            PaletteState.Tracking => ImageStates.ImageTracking,
            _ => null
        };

        // If there is no image then use the generic image
        return image ?? Image;
    }

    /// <summary>
    /// Gets the content short text.
    /// </summary>
    public virtual string GetShortText() => Text;

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    public virtual string GetLongText() => ExtraText;

    /// <summary>
    /// Gets the overlay image.
    /// </summary>
    /// <param name="state">The state for which the overlay image is needed.</param>
    /// <returns>Overlay image value, or null if no overlay image is set.</returns>
    public virtual Image? GetOverlayImage(PaletteState state) => _overlayImage.GetImage(state);

    /// <summary>
    /// Gets the overlay image color that should be transparent.
    /// </summary>
    /// <param name="state">The state for which the overlay image is needed.</param>
    /// <returns>Color value.</returns>
    public virtual Color GetOverlayImageTransparentColor(PaletteState state) => _overlayImage.ImageTransparentColor;

    /// <summary>
    /// Gets the position of the overlay image relative to the main image.
    /// </summary>
    /// <param name="state">The state for which the overlay position is needed.</param>
    /// <returns>Overlay image position.</returns>
    public virtual OverlayImagePosition GetOverlayImagePosition(PaletteState state) => _overlayImage.Position;

    /// <summary>
    /// Gets the scaling mode for the overlay image.
    /// </summary>
    /// <param name="state">The state for which the overlay scale mode is needed.</param>
    /// <returns>Overlay image scale mode.</returns>
    public virtual OverlayImageScaleMode GetOverlayImageScaleMode(PaletteState state) => _overlayImage.ScaleMode;

    /// <summary>
    /// Gets the scale factor for the overlay image.
    /// </summary>
    /// <param name="state">The state for which the overlay scale factor is needed.</param>
    /// <returns>Scale factor.</returns>
    public virtual float GetOverlayImageScaleFactor(PaletteState state) => _overlayImage.ScaleFactor;

    /// <summary>
    /// Gets the fixed size for the overlay image.
    /// </summary>
    /// <param name="state">The state for which the overlay fixed size is needed.</param>
    /// <returns>Fixed size.</returns>
    public virtual Size GetOverlayImageFixedSize(PaletteState state) => _overlayImage.FixedSize;

    #endregion

    #region UserAccountControlValues

    /*
    /// <summary>Gets the user account control values.</summary>
    /// <value>The user account control values.</value>
    [Category(@"Visuals")]
    [Description(@"Button values")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public UserAccountControlShieldValues UserAccountControlValues { get; }
    */

    #endregion

    #region UAC Stuff

    /// <summary>
    /// Shows the UAC shield and ignores later attempts to hide it or replace the shield slot.
    /// </summary>
    /// <remarks>
    /// Called when the first <see cref="KryptonButton.ElevatedClick"/> handler is attached.
    /// A content image stays in <see cref="Image"/> and the shield is drawn as <see cref="OverlayImage"/>.
    /// With no content image, the shield stays in <see cref="Image"/>.
    /// </remarks>
    internal void LockUacShieldForElevation()
    {
        _uacShieldLocked = true;
        UseAsUACElevationButton = true;
    }

    /// <summary>
    /// Clears the elevation shield lock. The shield image stays until <see cref="UseAsUACElevationButton"/> is set to <c>false</c>.
    /// </summary>
    internal void UnlockUacShieldForElevation() => _uacShieldLocked = false;

    /// <summary>
    /// Places the shield on <see cref="OverlayImage"/> when a content image is already set, otherwise on <see cref="Image"/>.
    /// </summary>
    /// <param name="image">Shield bitmap to show.</param>
    private void AssignUacShieldImage(Image? image)
    {
        if (image == null)
        {
            return;
        }

        if (HasContentImage())
        {
            PlaceShieldOnOverlay(image);
            return;
        }

        PlaceShieldOnImage(image);
    }

    private bool HasContentImage() => _image != null && !_uacShieldOwnsImage;

    private void PlaceShieldOnOverlay(Image image)
    {
        _applyingUacShield = true;
        try
        {
            _overlayImage.Image = image;
            _overlayImage.Position = OverlayImagePosition.BottomRight;
            _overlayImage.ScaleMode = OverlayImageScaleMode.Percentage;
            _uacShieldOwnsOverlay = true;
            _uacShieldOwnsImage = false;
        }
        finally
        {
            _applyingUacShield = false;
        }
    }

    private void PlaceShieldOnImage(Image image)
    {
        _applyingUacShield = true;
        try
        {
            ClearOwnedShieldOverlay();
            SetImageCore(image);
            _uacShieldOwnsImage = true;
        }
        finally
        {
            _applyingUacShield = false;
        }
    }

    private void SetImageCore(Image? value)
    {
        if (_image != value)
        {
            _image = value;
            _imageIsDefault = false;
            PerformNeedPaint(true);
        }
    }

    /// <summary>
    /// Moves the shield onto <see cref="Image"/> so it remains visible when there is no content image for an overlay.
    /// </summary>
    private void MoveUacShieldToImage()
    {
        Image? shield = _uacShieldOwnsOverlay ? _overlayImage.Image : null;
        ClearOwnedShieldOverlay();

        if (shield != null)
        {
            _applyingUacShield = true;
            try
            {
                SetImageCore(shield);
                _uacShieldOwnsImage = true;
            }
            finally
            {
                _applyingUacShield = false;
            }

            return;
        }

        if (_uacShieldOwnsImage && _image != null)
        {
            return;
        }

        _uacShieldOwnsImage = false;
        ShowUACShieldImage(true, _iconSize ?? IconSize.ExtraSmall);
    }

    private void ClearOwnedShieldOverlay()
    {
        if (!_uacShieldOwnsOverlay)
        {
            return;
        }

        _applyingUacShield = true;
        try
        {
            _overlayImage.Image = null;
            _overlayImage.Position = OverlayImagePosition.TopRight;
            _overlayImage.ScaleMode = OverlayImageScaleMode.None;
            _uacShieldOwnsOverlay = false;
        }
        finally
        {
            _applyingUacShield = false;
        }
    }

    private bool RejectUacShieldOverlayChange(Image? replacement)
    {
        if (!_uacShieldLocked || !_uacShieldOwnsOverlay || _applyingUacShield)
        {
            if (!_applyingUacShield && _uacShieldOwnsOverlay && (replacement == null || replacement != _overlayImage.Image))
            {
                _uacShieldOwnsOverlay = false;
            }

            return false;
        }

        // External replace or clear is ignored while the shield owns the overlay.
        return replacement == null || replacement != _overlayImage.Image;
    }

    /// <summary>Shows the uac shield.</summary>
    /// <param name="showUACShield">if set to <c>true</c> [show uac shield].</param>
    /// <param name="iconSize">Size of the shield icon.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    private void ShowUACShieldImage(bool showUACShield, IconSize? iconSize = null, int? width = null, int? height = null)
    {
        if (!showUACShield)
        {
            ClearOwnedShieldOverlay();
            return;
        }

        // Check if custom size is specified
        if (_customIconSize.HasValue)
        {
            var customSize = _customIconSize.Value;

            // Use our new theme-aware icon extraction system
            var icon = GraphicsExtensions.ExtractIconFromImageres(
                (int)ImageresIconID.Shield,
                GetIconSizeFromSize(customSize),
                _iconSelectionStrategy
            );

            if (icon != null)
            {
                AssignUacShieldImage(new Bitmap(icon.ToBitmap(), customSize));
                icon.Dispose();
            }
            else
            {
                // Fallback to scaled version
                Image shieldFallback = GetOSSpecificShieldIcon();
                AssignUacShieldImage(GraphicsExtensions.ScaleImage(shieldFallback, customSize.Width, customSize.Height));
            }
        }
        else if (width.HasValue && height.HasValue)
        {
            // Custom width/height specified
            var customSize = new Size(width.Value, height.Value);

            // Use our new theme-aware icon extraction system
            var icon = GraphicsExtensions.ExtractIconFromImageres(
                (int)ImageresIconID.Shield,
                GetIconSizeFromSize(customSize),
                _iconSelectionStrategy
            );

            if (icon != null)
            {
                AssignUacShieldImage(new Bitmap(icon.ToBitmap(), customSize));
                icon.Dispose();
            }
            else
            {
                // Fallback to scaled version
                Image shieldFallback = GetOSSpecificShieldIcon();
                AssignUacShieldImage(GraphicsExtensions.ScaleImage(shieldFallback, width.Value, height.Value));
            }
        }
        else
        {
            // Use predefined sizes with theme-aware selection
            var icon = GraphicsExtensions.ExtractIconFromImageres(
                (int)ImageresIconID.Shield,
                iconSize ?? IconSize.ExtraSmall,
                _iconSelectionStrategy
            );

            if (icon != null)
            {
                var targetSize = GetSizeFromIconSize(iconSize ?? IconSize.ExtraSmall);
                AssignUacShieldImage(new Bitmap(icon.ToBitmap(), targetSize));
                icon.Dispose();
            }
            else
            {
                // Fallback to old method for backward compatibility
                Image shield = GetOSSpecificShieldIcon();
                var targetSize = GetSizeFromIconSize(iconSize ?? IconSize.ExtraSmall);
                AssignUacShieldImage(GraphicsExtensions.ScaleImage(shield, targetSize));
            }
        }
    }

    private void ShowUACShield(bool showShield, IconSize? uacShieldIconSize)
    {
        switch (_iconSize)
        {
            case IconSize.ExtraSmall:
                ShowUACShieldImage(showShield, IconSize.ExtraSmall);
                break;
            case IconSize.Small:
                ShowUACShieldImage(showShield, IconSize.Small);
                break;
            case IconSize.Medium:
                ShowUACShieldImage(showShield, IconSize.Medium);
                break;
            case IconSize.Large:
                ShowUACShieldImage(showShield, IconSize.Large);
                break;
            case IconSize.ExtraLarge:
                ShowUACShieldImage(showShield, IconSize.ExtraLarge);
                break;
            case null:
                ShowUACShieldImage(showShield, IconSize.ExtraSmall);
                break;
            default:
                ShowUACShieldImage(showShield, IconSize.ExtraSmall);
                break;
        }
    }

    /// <summary>Updates the UAC shield icon.</summary>
    /// <param name="iconSize">Size of the icon.</param>
    /// <param name="customSize">Size of the custom.</param>
    private void UpdateOSUACShieldIcon(IconSize? iconSize = null, Size? customSize = null)
    {
        //if (OSUtilities.IsWindowsEleven)
        //{
        //    Image windowsElevenUacShieldImage = UACShieldIconResources.UACShieldWindows11;

        //    if (iconSize == UACShieldIconSize.Custom)
        //    {
        //        UpdateShieldSize(UACShieldIconSize.Custom, customSize, windowsElevenUacShieldImage);
        //    }
        //    else
        //    {
        //        UpdateShieldSize(iconSize, null, windowsElevenUacShieldImage);
        //    }
        //}
        //else if (OSUtilities.IsWindowsTen)
        //{
        //    Image windowsTenUacShieldImage = UACShieldIconResources.UACShieldWindows10;

        //    if (iconSize == UACShieldIconSize.Custom)
        //    {
        //        UpdateShieldSize(UACShieldIconSize.Custom, customSize, windowsTenUacShieldImage);
        //    }
        //    else
        //    {
        //        UpdateShieldSize(iconSize, null, windowsTenUacShieldImage);
        //    }
        //}
        //else if (OSUtilities.IsWindowsEightPointOne || OSUtilities.IsWindowsEight || OSUtilities.IsWindowsSeven)
        //{
        //    Image windowsEightUacShieldImage = UACShieldIconResources.UACShieldWindows7881;

        //    if (iconSize == UACShieldIconSize.Custom)
        //    {
        //        UpdateShieldSize(UACShieldIconSize.Custom, customSize, windowsEightUacShieldImage);
        //    }
        //    else
        //    {
        //        UpdateShieldSize(iconSize, null, windowsEightUacShieldImage);
        //    }
        //}
    }

    /// <summary>Gets the OS-specific shield icon.</summary>
    /// <returns>The appropriate shield icon for the current OS.</returns>
    private Image GetOSSpecificShieldIcon()
    {
        // Use the new UACShieldHelper which tries imageres.dll first, then falls back to local resources
        return UACShieldHelper.GetOSSpecificUACShieldIcon(_iconSize ?? IconSize.ExtraSmall);
    }

    /// <summary>Gets the icon size from a Size object.</summary>
    /// <param name="size">The size to convert.</param>
    /// <returns>The corresponding IconSize.</returns>
    private static IconSize GetIconSizeFromSize(Size size)
    {
        return size.Width switch
        {
            8 => IconSize.Tiny,
            16 => IconSize.ExtraSmall,
            20 => IconSize.Small,
            24 => IconSize.MediumSmall,
            32 => IconSize.Medium,
            40 => IconSize.MediumLarge,
            48 => IconSize.Large,
            64 => IconSize.ExtraLarge,
            128 => IconSize.Huge,
            256 => IconSize.Maximum,
            _ => IconSize.Medium // Default to 32x32
        };
    }

    /// <summary>Gets the Size from an IconSize enum value.</summary>
    /// <param name="iconSize">The IconSize enum value.</param>
    /// <returns>The corresponding Size.</returns>
    private static Size GetSizeFromIconSize(IconSize iconSize) => new((int)iconSize, (int)iconSize);

#endregion
}
