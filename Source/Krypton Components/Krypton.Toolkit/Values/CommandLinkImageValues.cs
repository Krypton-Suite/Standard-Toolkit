#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    public class CommandLinkImageValues : Storage, IContentValues
    {
        #region Static Fields

        private static readonly Image DEFAULT_IMAGE = MessageBoxImageResources.GenericQuestion;

        #endregion

        #region Instance Fields

        private bool _displayUACShield;

        private Color _transparencyKey;

        private Image? _image;

        private UACShieldIconSize _uacShieldIconSize;

        #endregion

        #region Public

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
                        case UACShieldIconSize.ExtraSmall:
                            ShowUACShieldImage(value, UACShieldIconSize.ExtraSmall);
                            break;
                        case UACShieldIconSize.Small:
                            ShowUACShieldImage(value, UACShieldIconSize.Small);
                            break;
                        case UACShieldIconSize.Medium:
                            ShowUACShieldImage(value, UACShieldIconSize.Medium);
                            break;
                        case UACShieldIconSize.Large:
                            ShowUACShieldImage(value, UACShieldIconSize.Large);
                            break;
                        case UACShieldIconSize.ExtraLarge:
                            ShowUACShieldImage(value, UACShieldIconSize.ExtraLarge);
                            break;
                        default:
                            ShowUACShieldImage(value, UACShieldIconSize.ExtraSmall);
                            break;
                    }
                }
            }
        }

        /// <summary>Gets and sets the heading image transparent color.</summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Image transparent color.")]
        [RefreshProperties(RefreshProperties.All)]
        [KryptonDefaultColor()]
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

        private bool ShouldSerializeImageTransparentColor() => ImageTransparentColor != Color.Empty;

        /// <summary>Resets the ImageTransparentColor property to its default value.</summary>
        public void ResetImageTransparentColor() => ImageTransparentColor = Color.Empty;

        /// <summary>Gets or sets the image.</summary>
        /// <value>The image.</value>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("The image.")]
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

        private bool ShouldSerializeImage() => Image != DEFAULT_IMAGE;

        public void ResetImage() => Image = DEFAULT_IMAGE;

        [DefaultValue(UACShieldIconSize.ExtraSmall), Description(@"")]
        public UACShieldIconSize UACShieldIconSize
        {
            get => _uacShieldIconSize;

            set
            {
                _uacShieldIconSize = value;

                ShowUACShieldImage(_displayUACShield, value);
            }
        }

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="CommandLinkImageValues" /> class.</summary>
        /// <param name="needPaint">The need paint.</param>
        public CommandLinkImageValues(NeedPaintHandler needPaint)
        {
            NeedPaint = needPaint;

            _displayUACShield = false;

            _uacShieldIconSize = UACShieldIconSize.Medium;

            _image = DEFAULT_IMAGE;

            _transparencyKey = Color.Empty;

            //ResetImage();

            //ResetImageTransparentColor();
        }

        #endregion

        #region IsDefault

        /// <inheritdoc />
        [Browsable(false)]
        public override bool IsDefault => (DisplayUACShield.Equals(false) &&
                                           Image!.Equals(DEFAULT_IMAGE) &&
                                           ImageTransparentColor.Equals(Color.Empty) &&
                                           UACShieldIconSize.Equals(UACShieldIconSize.Medium));

        #endregion

        #region Implementation

        /// <inheritdoc />
        public Image? GetImage(PaletteState state) => Image;

        /// <inheritdoc />
        public Color GetImageTransparentColor(PaletteState state) => ImageTransparentColor;

        /// <inheritdoc />
        public string GetShortText() => string.Empty;

        /// <inheritdoc />
        public string GetLongText() => string.Empty;

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

                Image shield = SystemIcons.Shield.ToBitmap();

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

        /// <summary>Updates the UAC shield icon.</summary>
        /// <param name="iconSize">Size of the icon.</param>
        /// <param name="customSize">Size of the custom.</param>
        private void UpdateOSUACShieldIcon(UACShieldIconSize? iconSize = null, Size? customSize = null)
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
}