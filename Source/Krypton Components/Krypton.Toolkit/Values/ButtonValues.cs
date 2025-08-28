#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
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
    private static readonly string _defaultExtraText = GlobalStaticValues.DEFAULT_EMPTY_STRING;
    #endregion

    #region Instance Fields

    private bool _useAsDialogButton;
    private bool _useAsUACElevationButton;
    private bool _useSystemShieldIcon;
    private bool _showSplitOption;
    private UACShieldIconSize? _uacShieldIconSize;
    private Image? _image;
    private Color _transparent;
    private Color? _dropDownArrowColor;
    private string? _text;
    private string _extraText;

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
        _transparent = GlobalStaticValues.EMPTY_COLOR;
        _dropDownArrowColor = GlobalStaticValues.EMPTY_COLOR;
        _text = DEFAULT_TEXT;
        _extraText = _defaultExtraText;
        _useAsDialogButton = false;
        _useAsUACElevationButton = false;
        _showSplitOption = false;
        _uacShieldIconSize = GlobalStaticValues.DEFAULT_UAC_SHIELD_ICON_SIZE;
        ImageStates = CreateImageStates();
        ImageStates.NeedPaint = needPaint;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => ImageStates.IsDefault &&
                                      (Image == null) &&
                                      (UseAsADialogButton == false) &&
                                      (UseAsUACElevationButton == false) &&
                                      (UseSystemShieldIcon == false) &&
                                      (ShowSplitOption == false) &&
                                      (DropDownArrowColor == GlobalStaticValues.EMPTY_COLOR) &&
                                      //(UACShieldIconSize == UACShieldIconSize.ExtraSmall)
                                      (ImageTransparentColor == GlobalStaticValues.EMPTY_COLOR) &&
                                      (Text == DEFAULT_TEXT) &&
                                      (ExtraText == _defaultExtraText);

    #endregion

    #region Image
    /// <summary>
    /// Gets and sets the button image.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Button image.")]
    [RefreshProperties(RefreshProperties.All)]
    public Image? Image
    {
        get => _image;

        set
        {
            if (_image != value)
            {
                _image = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeImage() => Image != null;

    /// <summary>
    /// Resets the Image property to its default value.
    /// </summary>
    public void ResetImage() => Image = null;
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

    private bool ShouldSerializeImageTransparentColor() => ImageTransparentColor != GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Resets the ImageTransparentColor property to its default value.
    /// </summary>
    public void ResetImageTransparentColor() => ImageTransparentColor = GlobalStaticValues.EMPTY_COLOR;

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
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [AllowNull]
    public string Text
    {
        get => _text ?? GlobalStaticValues.DEFAULT_EMPTY_STRING;

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

    private bool ShouldSerializeText() => Text != DEFAULT_TEXT;

    /// <summary>
    /// Resets the Text property to its default value.
    /// </summary>
    public void ResetText() => Text = DEFAULT_TEXT;
    #endregion

    #region ExtraText
    /// <summary>
    /// Gets and sets the button extra text.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Button extra text.")]
    [RefreshProperties(RefreshProperties.All)]
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

    /// <summary>Gets or sets a value indicating whether [use as uac elevation button].</summary>
    /// <value><c>true</c> if [use as uac elevation button]; otherwise, <c>false</c>.</value>
    [DefaultValue(false),
     Description(@"Transforms the button into a UAC elevated button.")]
    public bool UseAsUACElevationButton
    {
        get => _useAsUACElevationButton;
        set
        {
            _useAsUACElevationButton = value;

            // If UseSystemShieldIcon is also set, prioritize it over the legacy UAC elevation
            if (_useSystemShieldIcon)
            {
                UpdateOSUACShieldIcon(_uacShieldIconSize ?? UACShieldIconSize.ExtraSmall);
            }
            else
            {
                ShowUACShield(value, _uacShieldIconSize ?? UACShieldIconSize.ExtraSmall);
            }
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

    #region UseSystemShieldIcon

    /// <summary>
    /// When true, the button will display the operating system's native UAC shield icon.
    /// </summary>
    [Category(@"Visuals")]
    [DefaultValue(false)]
    [Description(@"Use the operating system's UAC shield icon that matches the OS style.")]
    public bool UseSystemShieldIcon
    {
        get => _useSystemShieldIcon;
        set
        {
            if (_useSystemShieldIcon != value)
            {
                _useSystemShieldIcon = value;
                
                if (value)
                {
                    // If enabling system shield icon, clear the legacy UAC elevation
                    _useAsUACElevationButton = false;
                    UpdateOSUACShieldIcon(UACShieldIconSize);
                }
                else
                {
                    // If disabling system shield icon, clear the image
                    Image = null;
                    PerformNeedPaint(true);
                }
            }
        }
    }

    #endregion

    #region UseThemeAwareShieldIcon

    /// <summary>
    /// When true, the UAC shield icon will automatically adapt to the current theme (light/dark).
    /// This property works in conjunction with UseSystemShieldIcon.
    /// </summary>
    [Category(@"Visuals")]
    [DefaultValue(true)]
    [Description(@"Automatically adapt the UAC shield icon to the current theme (light/dark).")]
    public bool UseThemeAwareShieldIcon { get; set; } = true;

    #endregion

    // ShieldIconThemeMode property removed - now using theme-aware approach

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
    [DefaultValue(UACShieldIconSize.ExtraSmall), Description(@"The size of the UAC shield icon.")]
    public UACShieldIconSize UACShieldIconSize
    {
        get => _uacShieldIconSize ?? UACShieldIconSize.ExtraSmall;

        set
        {
            _uacShieldIconSize = value;

            // Update the shield image based on which property is active
            if (_useAsUACElevationButton)
            {
                ShowUACShieldImage(true, value);
            }
            else if (_useSystemShieldIcon)
            {
                UpdateOSUACShieldIcon(value);
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
    [DefaultValue(typeof(Color), @"Empty")]
    public Color? DropDownArrowColor
    {
        get => _dropDownArrowColor;

        set
        {
            if (_dropDownArrowColor != value)
            {
                _dropDownArrowColor = value ?? GlobalStaticValues.EMPTY_COLOR;

                PerformNeedPaint(true);
            }
        }
    }
    private void ResetDropDownArrowColor() => _dropDownArrowColor = GlobalStaticValues.EMPTY_COLOR;
    private bool ShouldSerializeDropDownArrowColor() => _dropDownArrowColor != GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region CreateImageStates
    /// <summary>
    /// Create the storage for the image states.
    /// </summary>
    /// <returns>Storage object.</returns>
    protected virtual ButtonImageStates CreateImageStates() => new ButtonImageStates();

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

    /// <summary>Shows the uac shield.</summary>
    /// <param name="showUACShield">if set to <c>true</c> [show uac shield].</param>
    /// <param name="shieldIconSize">Size of the shield icon.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    private void ShowUACShieldImage(bool showUACShield, UACShieldIconSize? shieldIconSize = null, int? width = null, int? height = null)
    {
        if (showUACShield)
        {
            int h = height ?? 16, w = width ?? 16;

            // Use theme-aware shield icon if requested, otherwise fall back to system icon
            Image shield;
            if (_useSystemShieldIcon)
            {
                // Get the current palette mode from the global manager
                var shieldIcon = UacShieldIconHelper.GetThemeAwareShieldIcon(KryptonManager.CurrentGlobalPaletteMode, _uacShieldIconSize ?? UACShieldIconSize.ExtraSmall);
                try
                {
                    shield = shieldIcon?.ToBitmap() ?? SystemIcons.Shield.ToBitmap();
                }
                finally
                {
                    shieldIcon?.Dispose();
                }
            }
            else
            {
                shield = SystemIcons.Shield.ToBitmap();
            }

            switch (shieldIconSize)
            {
                //case UACShieldIconSize.Custom:
                //    Values.Image = GraphicsExtensions.ScaleImage(shield, w, h);
                //    break;
                case UACShieldIconSize.ExtraSmall:
                    Image = GraphicsExtensions.ScaleImage(shield, 16, 16);
                    break;
                case UACShieldIconSize.Small:
                    Image = GraphicsExtensions.ScaleImage(shield, 32, 32);
                    break;
                case UACShieldIconSize.Medium:
                    Image = GraphicsExtensions.ScaleImage(shield, 64, 64);
                    break;
                case UACShieldIconSize.Large:
                    Image = GraphicsExtensions.ScaleImage(shield, 128, 128);
                    break;
                case UACShieldIconSize.ExtraLarge:
                    Image = GraphicsExtensions.ScaleImage(shield, 256, 256);
                    break;
                case null:
                    Image = GraphicsExtensions.ScaleImage(shield, 16, 16);
                    break;
            }

            // Force a repaint
            PerformNeedPaint(true);
        }
        else
        {
            Image = null;
        }
    }

    private void ShowUACShield(bool showShield, UACShieldIconSize? uacShieldIconSize)
    {
        switch (uacShieldIconSize)
        {
            case UACShieldIconSize.ExtraSmall:
                ShowUACShieldImage(showShield, UACShieldIconSize.ExtraSmall);
                break;
            case UACShieldIconSize.Small:
                ShowUACShieldImage(showShield, UACShieldIconSize.Small);
                break;
            case UACShieldIconSize.Medium:
                ShowUACShieldImage(showShield, UACShieldIconSize.Medium);
                break;
            case UACShieldIconSize.Large:
                ShowUACShieldImage(showShield, UACShieldIconSize.Large);
                break;
            case UACShieldIconSize.ExtraLarge:
                ShowUACShieldImage(showShield, UACShieldIconSize.ExtraLarge);
                break;
            case null:
                ShowUACShieldImage(showShield, UACShieldIconSize.ExtraSmall);
                break;
            default:
                ShowUACShieldImage(showShield, UACShieldIconSize.ExtraSmall);
                break;
        }
    }

    /// <summary>Updates the UAC shield icon.</summary>
    /// <param name="iconSize">Size of the icon.</param>
    /// <param name="customSize">Size of the custom.</param>
    private void UpdateOSUACShieldIcon(UACShieldIconSize? iconSize = null, Size? customSize = null)
    {
        if (_useSystemShieldIcon)
        {
            try
            {
                // Get the target icon size
                UACShieldIconSize targetIconSize = iconSize ?? _uacShieldIconSize ?? UACShieldIconSize.ExtraSmall;
                int targetSize = GetTargetSize(targetIconSize);

                // Try to get a DPI-aware system shield icon at the exact size first
                Icon? shieldIcon = UacShieldIconHelper.GetSystemShieldIconAtSize(targetSize);
                
                if (shieldIcon == null)
                {
                    // Fallback to theme-aware shield icon
                    shieldIcon = UacShieldIconHelper.GetThemeAwareShieldIcon(KryptonManager.CurrentGlobalPaletteMode, _uacShieldIconSize ?? UACShieldIconSize.ExtraSmall);
                }

                if (shieldIcon != null)
                {
                    // Use the icon directly if it's the right size, otherwise scale it
                    if (shieldIcon.Size.Width == targetSize && shieldIcon.Size.Height == targetSize)
                    {
                        Image = shieldIcon.ToBitmap();
                    }
                    else
                    {
                        // Scale the icon to the appropriate size with high quality
                        Image = ScaleIconWithQuality(shieldIcon, targetSize, targetSize);
                    }
                    
                    PerformNeedPaint(true);
                }
                else
                {
                    // Final fallback: SystemIcons.Shield
                    Image = ScaleIconWithQuality(SystemIcons.Shield, targetSize, targetSize);
                    PerformNeedPaint(true);
                }
            }
            catch
            {
                // fallback: SystemIcons.Shield
                UACShieldIconSize targetIconSize = iconSize ?? _uacShieldIconSize ?? UACShieldIconSize.ExtraSmall;
                int targetSize = GetTargetSize(targetIconSize);
                Image = ScaleIconWithQuality(SystemIcons.Shield, targetSize, targetSize);
                PerformNeedPaint(true);
            }
        }
        else
        {
            // If not using system shield icon, clear the image
            Image = null;
            PerformNeedPaint(true);
        }
    }

    /// <summary>
    /// Scales an icon with high quality and DPI awareness.
    /// </summary>
    /// <param name="icon">The icon to scale.</param>
    /// <param name="width">The target width.</param>
    /// <param name="height">The target height.</param>
    /// <returns>A scaled bitmap.</returns>
    private static Bitmap? ScaleIconWithQuality(Icon icon, int width, int height)
    {
        try
        {
            using var bitmap = new Bitmap(width, height);
            using var graphics = Graphics.FromImage(bitmap);
            
            // Set high quality interpolation
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            
            // Draw the icon to the bitmap
            graphics.DrawIcon(icon, new Rectangle(0, 0, width, height));
            
            // Return a copy of the bitmap
            return new Bitmap(bitmap);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Gets the target size in pixels for the given UAC shield icon size.
    /// </summary>
    /// <param name="iconSize">The UAC shield icon size.</param>
    /// <returns>The target size in pixels.</returns>
    private static int GetTargetSize(UACShieldIconSize iconSize)
    {
        return iconSize switch
        {
            UACShieldIconSize.ExtraSmall => 16,
            UACShieldIconSize.Small => 32,
            UACShieldIconSize.Medium => 64,
            UACShieldIconSize.Large => 128,
            UACShieldIconSize.ExtraLarge => 256,
            _ => 16
        };
    }

    #endregion
}