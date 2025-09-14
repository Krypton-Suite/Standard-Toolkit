#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

// ReSharper disable InconsistentNaming
namespace Krypton.Toolkit;

public class CommandLinkImageValues : Storage, IContentValues
{
    #region Static Fields

    private static readonly Image? DEFAULT_IMAGE = /*MessageBoxImageResources.GenericQuestion;*/ GraphicsExtensions.ScaleImage(GraphicsExtensions.ExtractIcon(Libraries.Shell32, 16805, true)?.ToBitmap(), 32, 32);

    private static readonly Image DEFAULT_WINDOWS_11_IMAGE = CommandLinkImageResources.Windows_11_CommandLink_Arrow;

    private static readonly Image DEFAULT_WINDOWS_10_IMAGE = CommandLinkImageResources.Windows_10_CommandLink_Arrow;

    #endregion

    #region Instance Fields

    private bool _displayUACShield;

    private Color _transparencyKey;

    private Image? _image;

    private IconSize _uacShieldIconSize;

    #endregion

    #region Public

    [DefaultValue(false)]
    public bool DisplayUACShield
    {
        get => _displayUACShield;

        set
        {
            if (_displayUACShield != value)
            {
                _displayUACShield = value;

                switch (_uacShieldIconSize)
                {
                    case IconSize.Tiny:
                        ShowUACShieldImage(value, IconSize.Tiny);
                        break;
                    case IconSize.ExtraSmall:
                        ShowUACShieldImage(value, IconSize.ExtraSmall);
                        break;
                    case IconSize.Small:
                        ShowUACShieldImage(value, IconSize.Small);
                        break;
                    case IconSize.MediumSmall:
                        ShowUACShieldImage(value, IconSize.MediumSmall);
                        break;
                    case IconSize.Medium:
                        ShowUACShieldImage(value, IconSize.Medium);
                        break;
                    case IconSize.MediumLarge:
                        ShowUACShieldImage(value, IconSize.MediumLarge);
                        break;
                    case IconSize.Large:
                        ShowUACShieldImage(value, IconSize.Large);
                        break;
                    case IconSize.ExtraLarge:
                        ShowUACShieldImage(value, IconSize.ExtraLarge);
                        break;
                    case IconSize.Huge:
                        ShowUACShieldImage(value, IconSize.Huge);
                        break;
                    case IconSize.Maximum:
                        ShowUACShieldImage(value, IconSize.Maximum);
                        break;
                    default:
                        ShowUACShieldImage(value, IconSize.ExtraSmall);
                        break;
                }
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

    /// <summary>Gets or sets the image.</summary>
    /// <value>The image.</value>
    [Category("Visuals")]
    [Description("The image.")]
    [RefreshProperties(RefreshProperties.All)]
    //[DefaultValue(typeof(Image), @"DEFAULT_IMAGE")]
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
    private bool ShouldSerializeImage() => _image != DEFAULT_IMAGE;
    private void ResetImage()
    {
        Image = DEFAULT_IMAGE;

        // Image = DEFAULT_WINDOWS_11_IMAGE;
    }

    [DefaultValue(IconSize.Small), Description(@"")]
    public IconSize UACShieldIconSize
    {
        get => _uacShieldIconSize;

        set
        {
            _uacShieldIconSize = value;

            ShowUACShieldImage(_displayUACShield, value);
        }
    }
    private bool ShouldSerializeUACShieldIconSize() => _uacShieldIconSize != IconSize.Small;
    private void ResetUACShieldIconSize() => UACShieldIconSize = IconSize.Small;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="CommandLinkImageValues" /> class.</summary>
    /// <param name="needPaint">The need paint.</param>
    public CommandLinkImageValues(NeedPaintHandler needPaint)
    {
        NeedPaint = needPaint;

        ResetDisplayUACShield();

        ResetImage();

        ResetImageTransparentColor();

        ResetUACShieldIconSize();
    }

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (!ShouldSerializeDisplayUACShield() &&
                                       !ShouldSerializeImage() &&
                                       !ShouldSerializeImageTransparentColor() &&
                                       !ShouldSerializeUACShieldIconSize()
        );

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

    /// <summary>Shows the uac shield.</summary>
    /// <param name="showUACShield">if set to <c>true</c> [show uac shield].</param>
    /// <param name="shieldIconSize">Size of the shield icon.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    private void ShowUACShieldImage(bool showUACShield, IconSize? shieldIconSize = null, int? width = null, int? height = null)
    {
        if (showUACShield)
        {
            Image shield = SystemIcons.Shield.ToBitmap();

            switch (shieldIconSize)
            {
                case IconSize.Tiny:
                    Image = GraphicsExtensions.ScaleImage(shield, 8, 8);
                    break;
                case IconSize.ExtraSmall:
                    Image = GraphicsExtensions.ScaleImage(shield, 16, 16);
                    break;
                case IconSize.Small:
                    Image = GraphicsExtensions.ScaleImage(shield, 24, 24);
                    break;
                case IconSize.MediumSmall:
                    Image = GraphicsExtensions.ScaleImage(shield, 32, 32);
                    break;
                case IconSize.Medium:
                    Image = GraphicsExtensions.ScaleImage(shield, 48, 48);
                    break;
                case IconSize.MediumLarge:
                    Image = GraphicsExtensions.ScaleImage(shield, 64, 64);
                    break;
                case IconSize.Large:
                    Image = GraphicsExtensions.ScaleImage(shield, 96, 96);
                    break;
                case IconSize.ExtraLarge:
                    Image = GraphicsExtensions.ScaleImage(shield, 128, 128);
                    break;
                case IconSize.Huge:
                    Image = GraphicsExtensions.ScaleImage(shield, 192, 192);
                    break;
                case IconSize.Maximum:
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
            // TODO: This should revert to the original image !
            //Image = null;
        }
    }

    /// <summary>Gets the OS-specific shield icon.</summary>
    /// <returns>The appropriate shield icon for the current OS.</returns>
    private Image GetOSSpecificShieldIcon()
    {
        // Use the new UACShieldHelper which tries imageres.dll first, then falls back to local resources
        return UACShieldHelper.GetOSSpecificUACShieldIcon(_uacShieldIconSize);
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

    #endregion
}