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
    /// <summary>A <see cref="KryptonCommand"/> created specifically for the <see cref="PaletteButtonSpecStyle.FormHelp"/> button spec.</summary>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonHelpCommand), @"ToolboxBitmaps.KryptonHelp.bmp")]
    [Description(@"")]
    [DesignerCategory(@"code")]
    public class KryptonHelpCommand : KryptonCommand
    {
        #region Instance Fields

        private ButtonSpecAny? _helpButtonSpec;

        private ButtonImageStates? _imageStates;

        private Image? _activeImage;

        private Image? _disabledImage;

        private Image? _normalImage;

        private Image? _pressedImage;

        #endregion

        #region Public

        /// <summary>Gets or sets the help button.</summary>
        /// <value>The help button.</value>
        [DefaultValue(null), Description(@"Access to the help button spec.")]
        public ButtonSpecAny? HelpButton
        {
            get => _helpButtonSpec ?? new();
            set { _helpButtonSpec = value; UpdateImage(KryptonManager.InternalGlobalPaletteMode); }
        }

        /* /// <summary>
         /// Gets access to the state specific images for the help button.
         /// </summary>
         [AllowNull, Category(@"Appearance"), Description(@"State specific images for the help button."), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
         public ButtonImageStates? ImageStates { get => _imageStates ?? new(); set { _imageStates = value; UpdateImageStates(KryptonManager.InternalGlobalPaletteMode); } }*/

        public Image? ActiveImage { get => _activeImage; private set => _activeImage = value; }

        public Image? DisabledImage { get => _disabledImage; private set => _disabledImage = value; }

        public Image? NormalImage { get => _normalImage; private set => _normalImage = value; }

        public Image? PressedImage { get => _pressedImage; private set => _pressedImage = value; }

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonHelpCommand" /> class.</summary>
        public KryptonHelpCommand()
        {
            // An empty constructor...
        }

        #endregion

        #region Implementation

        /// <summary>Updates the image.</summary>
        /// <param name="helpImage">The help image.</param>
        private void UpdateImage(Image helpImage) => ImageSmall = helpImage;

        /// <summary>Adds the image states.</summary>
        /// <param name="activeImage">The active image.</param>
        /// <param name="disabledImage">The disabled image.</param>
        /// <param name="normalImage">The normal image.</param>
        /// <param name="pressedImage">The pressed image.</param>
        private void AddImageStates(Image? activeImage, Image disabledImage, Image normalImage, Image? pressedImage)
        {
            if (_imageStates != null)
            {
                _imageStates.ImageDisabled = disabledImage ?? null;

                _imageStates.ImageNormal = normalImage;

                _imageStates.ImageTracking = activeImage;

                _imageStates.ImagePressed = pressedImage ?? null;
            }

            if (_helpButtonSpec != null && _imageStates != null)
            {
                _helpButtonSpec.ImageStates.ImageDisabled = _imageStates.ImageDisabled;

                _helpButtonSpec.ImageStates.ImageNormal = _imageStates.ImageNormal;

                _helpButtonSpec.ImageStates.ImageTracking = _imageStates.ImageTracking;

                _helpButtonSpec.ImageStates.ImagePressed = _imageStates.ImagePressed;
            }
        }

        private void UpdateImage(PaletteMode mode)
        {

            switch (mode)
            {
                case PaletteMode.Global:
                    break;
                case PaletteMode.ProfessionalSystem:
                    UpdateImage(HelpIconResources.ProfessionalHelpIconNormal);
                    break;
                case PaletteMode.ProfessionalOffice2003:
                    UpdateImage(HelpIconResources.Office2003HelpIconNormal);
                    break;
                case PaletteMode.Office2007DarkGray:
                    UpdateImage(HelpIconResources.Office2007HelpIconNormal);
                    break;
                case PaletteMode.Office2007Blue:
                    UpdateImage(HelpIconResources.Office2007HelpIconNormal);
                    break;
                case PaletteMode.Office2007BlueDarkMode:
                    UpdateImage(HelpIconResources.Office2007HelpIconNormal);
                    break;
                case PaletteMode.Office2007BlueLightMode:
                    UpdateImage(HelpIconResources.Office2007HelpIconNormal);
                    break;
                case PaletteMode.Office2007Silver:
                    UpdateImage(HelpIconResources.Office2007HelpIconNormal);
                    break;
                case PaletteMode.Office2007SilverDarkMode:
                    UpdateImage(HelpIconResources.Office2007HelpIconNormal);
                    break;
                case PaletteMode.Office2007SilverLightMode:
                    UpdateImage(HelpIconResources.Office2007HelpIconNormal);
                    break;
                case PaletteMode.Office2007White:
                    UpdateImage(HelpIconResources.Office2007HelpIconNormal);
                    break;
                case PaletteMode.Office2007Black:
                    UpdateImage(HelpIconResources.Office2007HelpIconNormal);
                    break;
                case PaletteMode.Office2007BlackDarkMode:
                    UpdateImage(HelpIconResources.Office2007HelpIconNormal);
                    break;
                case PaletteMode.Office2010DarkGray:
                    UpdateImage(HelpIconResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.Office2010Blue:
                    UpdateImage(HelpIconResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.Office2010BlueDarkMode:
                    UpdateImage(HelpIconResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.Office2010BlueLightMode:
                    UpdateImage(HelpIconResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.Office2010Silver:
                    UpdateImage(HelpIconResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.Office2010SilverDarkMode:
                    UpdateImage(HelpIconResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.Office2010SilverLightMode:
                    UpdateImage(HelpIconResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.Office2010White:
                    UpdateImage(HelpIconResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.Office2010Black:
                    UpdateImage(HelpIconResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.Office2010BlackDarkMode:
                    UpdateImage(HelpIconResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.Office2013DarkGray:
                    UpdateImage(HelpIconResources.Microsoft365HelpIconNormal);
                    break;
                case PaletteMode.Office2013White:
                    UpdateImage(HelpIconResources.Microsoft365HelpIconNormal);
                    break;
                case PaletteMode.Microsoft365DarkGray:
                    UpdateImage(HelpIconResources.Microsoft365HelpIconNormal);
                    break;
                case PaletteMode.Microsoft365Black:
                    UpdateImage(HelpIconResources.Microsoft365HelpIconNormal);
                    break;
                case PaletteMode.Microsoft365BlackDarkMode:
                    UpdateImage(HelpIconResources.Microsoft365HelpIconNormal);
                    break;
                case PaletteMode.Microsoft365Blue:
                    UpdateImage(HelpIconResources.Microsoft365HelpIconNormal);
                    break;
                case PaletteMode.Microsoft365BlueDarkMode:
                    UpdateImage(HelpIconResources.Microsoft365HelpIconNormal);
                    break;
                case PaletteMode.Microsoft365BlueLightMode:
                    UpdateImage(HelpIconResources.Microsoft365HelpIconNormal);
                    break;
                case PaletteMode.Microsoft365Silver:
                    UpdateImage(HelpIconResources.Microsoft365HelpIconNormal);
                    break;
                case PaletteMode.Microsoft365SilverDarkMode:
                    UpdateImage(HelpIconResources.Microsoft365HelpIconNormal);
                    break;
                case PaletteMode.Microsoft365SilverLightMode:
                    UpdateImage(HelpIconResources.Microsoft365HelpIconNormal);
                    break;
                case PaletteMode.Microsoft365White:
                    UpdateImage(HelpIconResources.Microsoft365HelpIconNormal);
                    break;
                case PaletteMode.SparkleBlue:
                    UpdateImage(HelpIconResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.SparkleBlueDarkMode:
                    UpdateImage(HelpIconResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.SparkleBlueLightMode:
                    UpdateImage(HelpIconResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.SparkleOrange:
                    UpdateImage(HelpIconResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.SparkleOrangeDarkMode:
                    UpdateImage(HelpIconResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.SparkleOrangeLightMode:
                    UpdateImage(HelpIconResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.SparklePurple:
                    UpdateImage(HelpIconResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.SparklePurpleDarkMode:
                    UpdateImage(HelpIconResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.SparklePurpleLightMode:
                    UpdateImage(HelpIconResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.Custom:
                    UpdateImage(HelpIconResources.Office2010HelpIconNormal);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }

            UpdateActiveImage(mode);

            UpdateDisabledImage(mode);

            UpdateNormalImage(mode);

            UpdatePressedImage(mode);
        }

        private void UpdateActiveImage(PaletteMode mode)
        {
            switch (mode)
            {
                case PaletteMode.Global:
                    break;
                case PaletteMode.ProfessionalSystem:
                    break;
                case PaletteMode.ProfessionalOffice2003:
                    break;
                case PaletteMode.Office2007DarkGray:
                    break;
                case PaletteMode.Office2007Blue:
                    break;
                case PaletteMode.Office2007BlueDarkMode:
                    break;
                case PaletteMode.Office2007BlueLightMode:
                    break;
                case PaletteMode.Office2007Silver:
                    break;
                case PaletteMode.Office2007SilverDarkMode:
                    break;
                case PaletteMode.Office2007SilverLightMode:
                    break;
                case PaletteMode.Office2007White:
                    break;
                case PaletteMode.Office2007Black:
                    break;
                case PaletteMode.Office2007BlackDarkMode:
                    break;
                case PaletteMode.Office2010DarkGray:
                    break;
                case PaletteMode.Office2010Blue:
                    break;
                case PaletteMode.Office2010BlueDarkMode:
                    break;
                case PaletteMode.Office2010BlueLightMode:
                    break;
                case PaletteMode.Office2010Silver:
                    break;
                case PaletteMode.Office2010SilverDarkMode:
                    break;
                case PaletteMode.Office2010SilverLightMode:
                    break;
                case PaletteMode.Office2010White:
                    break;
                case PaletteMode.Office2010Black:
                    break;
                case PaletteMode.Office2010BlackDarkMode:
                    break;
                case PaletteMode.Office2013DarkGray:
                    break;
                case PaletteMode.Office2013White:
                    break;
                case PaletteMode.Microsoft365DarkGray:
                    break;
                case PaletteMode.Microsoft365Black:
                    break;
                case PaletteMode.Microsoft365BlackDarkMode:
                    break;
                case PaletteMode.Microsoft365Blue:
                    break;
                case PaletteMode.Microsoft365BlueDarkMode:
                    break;
                case PaletteMode.Microsoft365BlueLightMode:
                    break;
                case PaletteMode.Microsoft365Silver:
                    break;
                case PaletteMode.Microsoft365SilverDarkMode:
                    break;
                case PaletteMode.Microsoft365SilverLightMode:
                    break;
                case PaletteMode.Microsoft365White:
                    break;
                case PaletteMode.SparkleBlue:
                    break;
                case PaletteMode.SparkleBlueDarkMode:
                    break;
                case PaletteMode.SparkleBlueLightMode:
                    break;
                case PaletteMode.SparkleOrange:
                    break;
                case PaletteMode.SparkleOrangeDarkMode:
                    break;
                case PaletteMode.SparkleOrangeLightMode:
                    break;
                case PaletteMode.SparklePurple:
                    break;
                case PaletteMode.SparklePurpleDarkMode:
                    break;
                case PaletteMode.SparklePurpleLightMode:
                    break;
                case PaletteMode.Custom:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        private void UpdateDisabledImage(PaletteMode mode)
        {

        }

        private void UpdateNormalImage(PaletteMode mode)
        {

        }

        private void UpdatePressedImage(PaletteMode mode)
        {

        }

        private void UpdateImageStates(PaletteMode mode)
        {
            if (_helpButtonSpec != null)
            {
                switch (mode)
                {
                    case PaletteMode.Global:
                        break;
                    case PaletteMode.ProfessionalSystem:
                        AddImageStates(null, HelpIconResources.ProfessionalHelpIconDisabled, HelpIconResources.ProfessionalHelpIconNormal, null);
                        break;
                    case PaletteMode.ProfessionalOffice2003:
                        AddImageStates(null, HelpIconResources.ProfessionalHelpIconDisabled, HelpIconResources.ProfessionalHelpIconNormal, null);
                        break;
                    case PaletteMode.Office2007DarkGray:
                        AddImageStates(HelpIconResources.Office2007HelpIconHover, HelpIconResources.Office2007HelpIconDisabled, HelpIconResources.Office2007HelpIconNormal, HelpIconResources.Office2007HelpIconPressed);
                        break;
                    case PaletteMode.Office2007Blue:
                        AddImageStates(HelpIconResources.Office2007HelpIconHover, HelpIconResources.Office2007HelpIconDisabled, HelpIconResources.Office2007HelpIconNormal, HelpIconResources.Office2007HelpIconPressed);
                        break;
                    case PaletteMode.Office2007BlueDarkMode:
                        AddImageStates(HelpIconResources.Office2007HelpIconHover, HelpIconResources.Office2007HelpIconDisabled, HelpIconResources.Office2007HelpIconNormal, HelpIconResources.Office2007HelpIconPressed);
                        break;
                    case PaletteMode.Office2007BlueLightMode:
                        AddImageStates(HelpIconResources.Office2007HelpIconHover, HelpIconResources.Office2007HelpIconDisabled, HelpIconResources.Office2007HelpIconNormal, HelpIconResources.Office2007HelpIconPressed);
                        break;
                    case PaletteMode.Office2007Silver:
                        AddImageStates(HelpIconResources.Office2007HelpIconHover, HelpIconResources.Office2007HelpIconDisabled, HelpIconResources.Office2007HelpIconNormal, HelpIconResources.Office2007HelpIconPressed);
                        break;
                    case PaletteMode.Office2007SilverDarkMode:
                        AddImageStates(HelpIconResources.Office2007HelpIconHover, HelpIconResources.Office2007HelpIconDisabled, HelpIconResources.Office2007HelpIconNormal, HelpIconResources.Office2007HelpIconPressed);
                        break;
                    case PaletteMode.Office2007SilverLightMode:
                        AddImageStates(HelpIconResources.Office2007HelpIconHover, HelpIconResources.Office2007HelpIconDisabled, HelpIconResources.Office2007HelpIconNormal, HelpIconResources.Office2007HelpIconPressed);
                        break;
                    case PaletteMode.Office2007White:
                        AddImageStates(HelpIconResources.Office2007HelpIconHover, HelpIconResources.Office2007HelpIconDisabled, HelpIconResources.Office2007HelpIconNormal, HelpIconResources.Office2007HelpIconPressed);
                        break;
                    case PaletteMode.Office2007Black:
                        AddImageStates(HelpIconResources.Office2007HelpIconHover, HelpIconResources.Office2007HelpIconDisabled, HelpIconResources.Office2007HelpIconNormal, HelpIconResources.Office2007HelpIconPressed);
                        break;
                    case PaletteMode.Office2007BlackDarkMode:
                        AddImageStates(HelpIconResources.Office2007HelpIconHover, HelpIconResources.Office2007HelpIconDisabled, HelpIconResources.Office2007HelpIconNormal, HelpIconResources.Office2007HelpIconPressed);
                        break;
                    case PaletteMode.Office2010DarkGray:
                        AddImageStates(HelpIconResources.Office2010HelpIconHover, HelpIconResources.Office2010HelpIconDisabled, HelpIconResources.Office2010HelpIconNormal, HelpIconResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.Office2010Blue:
                        AddImageStates(HelpIconResources.Office2010HelpIconHover, HelpIconResources.Office2010HelpIconDisabled, HelpIconResources.Office2010HelpIconNormal, HelpIconResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.Office2010BlueDarkMode:
                        AddImageStates(HelpIconResources.Office2010HelpIconHover, HelpIconResources.Office2010HelpIconDisabled, HelpIconResources.Office2010HelpIconNormal, HelpIconResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.Office2010BlueLightMode:
                        AddImageStates(HelpIconResources.Office2010HelpIconHover, HelpIconResources.Office2010HelpIconDisabled, HelpIconResources.Office2010HelpIconNormal, HelpIconResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.Office2010Silver:
                        AddImageStates(HelpIconResources.Office2010HelpIconHover, HelpIconResources.Office2010HelpIconDisabled, HelpIconResources.Office2010HelpIconNormal, HelpIconResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.Office2010SilverDarkMode:
                        AddImageStates(HelpIconResources.Office2010HelpIconHover, HelpIconResources.Office2010HelpIconDisabled, HelpIconResources.Office2010HelpIconNormal, HelpIconResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.Office2010SilverLightMode:
                        AddImageStates(HelpIconResources.Office2010HelpIconHover, HelpIconResources.Office2010HelpIconDisabled, HelpIconResources.Office2010HelpIconNormal, HelpIconResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.Office2010White:
                        AddImageStates(HelpIconResources.Office2010HelpIconHover, HelpIconResources.Office2010HelpIconDisabled, HelpIconResources.Office2010HelpIconNormal, HelpIconResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.Office2010Black:
                        AddImageStates(HelpIconResources.Office2010HelpIconHover, HelpIconResources.Office2010HelpIconDisabled, HelpIconResources.Office2010HelpIconNormal, HelpIconResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.Office2010BlackDarkMode:
                        AddImageStates(HelpIconResources.Office2010HelpIconHover, HelpIconResources.Office2010HelpIconDisabled, HelpIconResources.Office2010HelpIconNormal, HelpIconResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.Office2013DarkGray:
                        AddImageStates(HelpIconResources.Microsoft365HelpIconHover, HelpIconResources.Microsoft365HelpIconDisabled, HelpIconResources.Microsoft365HelpIconNormal, HelpIconResources.Microsoft365HelpIconPressed);
                        break;
                    case PaletteMode.Office2013White:
                        AddImageStates(HelpIconResources.Microsoft365HelpIconHover, HelpIconResources.Microsoft365HelpIconDisabled, HelpIconResources.Microsoft365HelpIconNormal, HelpIconResources.Microsoft365HelpIconPressed);
                        break;
                    case PaletteMode.Microsoft365DarkGray:
                        AddImageStates(HelpIconResources.Microsoft365HelpIconHover, HelpIconResources.Microsoft365HelpIconDisabled, HelpIconResources.Microsoft365HelpIconNormal, HelpIconResources.Microsoft365HelpIconPressed);
                        break;
                    case PaletteMode.Microsoft365Black:
                        AddImageStates(HelpIconResources.Microsoft365HelpIconHover, HelpIconResources.Microsoft365HelpIconDisabled, HelpIconResources.Microsoft365HelpIconNormal, HelpIconResources.Microsoft365HelpIconPressed);
                        break;
                    case PaletteMode.Microsoft365BlackDarkMode:
                        AddImageStates(HelpIconResources.Microsoft365HelpIconHover, HelpIconResources.Microsoft365HelpIconDisabled, HelpIconResources.Microsoft365HelpIconNormal, HelpIconResources.Microsoft365HelpIconPressed);
                        break;
                    case PaletteMode.Microsoft365Blue:
                        AddImageStates(HelpIconResources.Microsoft365HelpIconHover, HelpIconResources.Microsoft365HelpIconDisabled, HelpIconResources.Microsoft365HelpIconNormal, HelpIconResources.Microsoft365HelpIconPressed);
                        break;
                    case PaletteMode.Microsoft365BlueDarkMode:
                        AddImageStates(HelpIconResources.Microsoft365HelpIconHover, HelpIconResources.Microsoft365HelpIconDisabled, HelpIconResources.Microsoft365HelpIconNormal, HelpIconResources.Microsoft365HelpIconPressed);
                        break;
                    case PaletteMode.Microsoft365BlueLightMode:
                        AddImageStates(HelpIconResources.Microsoft365HelpIconHover, HelpIconResources.Microsoft365HelpIconDisabled, HelpIconResources.Microsoft365HelpIconNormal, HelpIconResources.Microsoft365HelpIconPressed);
                        break;
                    case PaletteMode.Microsoft365Silver:
                        AddImageStates(HelpIconResources.Microsoft365HelpIconHover, HelpIconResources.Microsoft365HelpIconDisabled, HelpIconResources.Microsoft365HelpIconNormal, HelpIconResources.Microsoft365HelpIconPressed);
                        break;
                    case PaletteMode.Microsoft365SilverDarkMode:
                        AddImageStates(HelpIconResources.Microsoft365HelpIconHover, HelpIconResources.Microsoft365HelpIconDisabled, HelpIconResources.Microsoft365HelpIconNormal, HelpIconResources.Microsoft365HelpIconPressed);
                        break;
                    case PaletteMode.Microsoft365SilverLightMode:
                        AddImageStates(HelpIconResources.Microsoft365HelpIconHover, HelpIconResources.Microsoft365HelpIconDisabled, HelpIconResources.Microsoft365HelpIconNormal, HelpIconResources.Microsoft365HelpIconPressed);
                        break;
                    case PaletteMode.Microsoft365White:
                        AddImageStates(HelpIconResources.Microsoft365HelpIconHover, HelpIconResources.Microsoft365HelpIconDisabled, HelpIconResources.Microsoft365HelpIconNormal, HelpIconResources.Microsoft365HelpIconPressed);
                        break;
                    case PaletteMode.SparkleBlue:
                        AddImageStates(HelpIconResources.Office2010HelpIconHover, HelpIconResources.Office2010HelpIconDisabled, HelpIconResources.Office2010HelpIconNormal, HelpIconResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.SparkleBlueDarkMode:
                        AddImageStates(HelpIconResources.Office2010HelpIconHover, HelpIconResources.Office2010HelpIconDisabled, HelpIconResources.Office2010HelpIconNormal, HelpIconResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.SparkleBlueLightMode:
                        AddImageStates(HelpIconResources.Office2010HelpIconHover, HelpIconResources.Office2010HelpIconDisabled, HelpIconResources.Office2010HelpIconNormal, HelpIconResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.SparkleOrange:
                        AddImageStates(HelpIconResources.Office2010HelpIconHover, HelpIconResources.Office2010HelpIconDisabled, HelpIconResources.Office2010HelpIconNormal, HelpIconResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.SparkleOrangeDarkMode:
                        AddImageStates(HelpIconResources.Office2010HelpIconHover, HelpIconResources.Office2010HelpIconDisabled, HelpIconResources.Office2010HelpIconNormal, HelpIconResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.SparkleOrangeLightMode:
                        AddImageStates(HelpIconResources.Office2010HelpIconHover, HelpIconResources.Office2010HelpIconDisabled, HelpIconResources.Office2010HelpIconNormal, HelpIconResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.SparklePurple:
                        AddImageStates(HelpIconResources.Office2010HelpIconHover, HelpIconResources.Office2010HelpIconDisabled, HelpIconResources.Office2010HelpIconNormal, HelpIconResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.SparklePurpleDarkMode:
                        AddImageStates(HelpIconResources.Office2010HelpIconHover, HelpIconResources.Office2010HelpIconDisabled, HelpIconResources.Office2010HelpIconNormal, HelpIconResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.SparklePurpleLightMode:
                        AddImageStates(HelpIconResources.Office2010HelpIconHover, HelpIconResources.Office2010HelpIconDisabled, HelpIconResources.Office2010HelpIconNormal, HelpIconResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.Custom:
                        AddImageStates(HelpIconResources.Microsoft365HelpIconHover, HelpIconResources.Microsoft365HelpIconDisabled, HelpIconResources.Microsoft365HelpIconNormal, HelpIconResources.Microsoft365HelpIconPressed);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
                }
            }
        }

        #endregion

        #region Overrides

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
        }

        #endregion
    }
}