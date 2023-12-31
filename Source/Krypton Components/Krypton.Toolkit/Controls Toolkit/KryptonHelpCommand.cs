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
    [Description(@"For use with the 'Help' ButtonSpec style.")]
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
            get => _helpButtonSpec ?? new ButtonSpecAny();
            set { _helpButtonSpec = value; UpdateImage(KryptonManager.CurrentGlobalPaletteMode); }
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
            _imageStates = new ButtonImageStates();

            Text = KryptonManager.Strings.PaletteButtonSpecStyleStrings.FormHelp;
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
            if (_helpButtonSpec != null)
            {
                _helpButtonSpec.ImageStates.ImageDisabled = disabledImage;

                _helpButtonSpec.ImageStates.ImageTracking = activeImage ?? null;

                _helpButtonSpec.ImageStates.ImageNormal = normalImage;

                _helpButtonSpec.ImageStates.ImagePressed = pressedImage ?? null;
            }
        }

        /// <summary>Updates the active image.</summary>
        /// <param name="activeImage">The active image.</param>
        private void UpdateActiveImage(Image activeImage)
        {
            _activeImage = activeImage;

            if (_helpButtonSpec != null)
            {
                _helpButtonSpec.ImageStates.ImageTracking = _activeImage;
            }
        }

        /// <summary>Updates the disabled image.</summary>
        /// <param name="disabledImage">The disabled image.</param>
        private void UpdateDisabledImage(Image disabledImage)
        {
            _disabledImage = disabledImage;

            if (_helpButtonSpec != null)
            {
                _helpButtonSpec.ImageStates.ImageDisabled = disabledImage;
            }
        }

        /// <summary>Updates the normal image.</summary>
        /// <param name="normalImage">The normal image.</param>
        private void UpdateNormalImage(Image normalImage)
        {
            _normalImage = normalImage;

            if (_helpButtonSpec != null)
            {
                _helpButtonSpec.ImageStates.ImageNormal = normalImage;
            }
        }

        /// <summary>Updates the pressed image.</summary>
        /// <param name="pressedImage">The pressed image.</param>
        private void UpdatePressedImage(Image pressedImage)
        {
            _pressedImage = pressedImage;

            if (_helpButtonSpec != null)
            {
                _helpButtonSpec.ImageStates.ImagePressed = pressedImage;
            }
        }

        /// <summary>Updates the image.</summary>
        /// <param name="mode">The mode.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">mode - null</exception>
        private void UpdateImage(PaletteMode mode)
        {

            switch (mode)
            {
                case PaletteMode.Global:
                    break;
                case PaletteMode.ProfessionalSystem:
                    UpdateImage(ProfessionalControlBoxResources.ProfessionalHelpIconNormal);
                    break;
                case PaletteMode.ProfessionalOffice2003:
                    UpdateImage(Office2003ControlBoxResources.Office2003HelpIconNormal);
                    break;
                case PaletteMode.Office2007DarkGray:
                    UpdateImage(Office2007ControlBoxResources.Office2007HelpIconNormal);
                    break;
                case PaletteMode.Office2007Blue:
                    UpdateImage(Office2007ControlBoxResources.Office2007HelpIconNormal);
                    break;
                case PaletteMode.Office2007BlueDarkMode:
                    UpdateImage(Office2007ControlBoxResources.Office2007HelpIconNormal);
                    break;
                case PaletteMode.Office2007BlueLightMode:
                    UpdateImage(Office2007ControlBoxResources.Office2007HelpIconNormal);
                    break;
                case PaletteMode.Office2007Silver:
                    UpdateImage(Office2007ControlBoxResources.Office2007HelpIconNormal);
                    break;
                case PaletteMode.Office2007SilverDarkMode:
                    UpdateImage(Office2007ControlBoxResources.Office2007HelpIconNormal);
                    break;
                case PaletteMode.Office2007SilverLightMode:
                    UpdateImage(Office2007ControlBoxResources.Office2007HelpIconNormal);
                    break;
                case PaletteMode.Office2007White:
                    UpdateImage(Office2007ControlBoxResources.Office2007HelpIconNormal);
                    break;
                case PaletteMode.Office2007Black:
                    UpdateImage(Office2007ControlBoxResources.Office2007HelpIconNormal);
                    break;
                case PaletteMode.Office2007BlackDarkMode:
                    UpdateImage(Office2007ControlBoxResources.Office2007HelpIconNormal);
                    break;
                case PaletteMode.Office2010DarkGray:
                    UpdateImage(Office2010ControlBoxResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.Office2010Blue:
                    UpdateImage(Office2010ControlBoxResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.Office2010BlueDarkMode:
                    UpdateImage(Office2010ControlBoxResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.Office2010BlueLightMode:
                    UpdateImage(Office2010ControlBoxResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.Office2010Silver:
                    UpdateImage(Office2010ControlBoxResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.Office2010SilverDarkMode:
                    UpdateImage(Office2010ControlBoxResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.Office2010SilverLightMode:
                    UpdateImage(Office2010ControlBoxResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.Office2010White:
                    UpdateImage(Office2010ControlBoxResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.Office2010Black:
                    UpdateImage(Office2010ControlBoxResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.Office2010BlackDarkMode:
                    UpdateImage(Office2010ControlBoxResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.Office2013DarkGray:
                case PaletteMode.Office2013LightGray:
                case PaletteMode.Office2013White:
                    UpdateImage(Office2013ControlBoxResources.Office2013HelpNormal);
                    break;
                case PaletteMode.Microsoft365DarkGray:
                    UpdateImage(Microsoft365ControlBoxResources.Microsoft365HelpIconNormal);
                    break;
                case PaletteMode.Microsoft365Black:
                    UpdateImage(Microsoft365ControlBoxResources.Microsoft365HelpIconNormal);
                    break;
                case PaletteMode.Microsoft365BlackDarkMode:
                    UpdateImage(Microsoft365ControlBoxResources.Microsoft365HelpIconNormal);
                    break;
                case PaletteMode.Microsoft365Blue:
                    UpdateImage(Microsoft365ControlBoxResources.Microsoft365HelpIconNormal);
                    break;
                case PaletteMode.Microsoft365BlueDarkMode:
                    UpdateImage(Microsoft365ControlBoxResources.Microsoft365HelpIconNormal);
                    break;
                case PaletteMode.Microsoft365BlueLightMode:
                    UpdateImage(Microsoft365ControlBoxResources.Microsoft365HelpIconNormal);
                    break;
                case PaletteMode.Microsoft365Silver:
                    UpdateImage(Microsoft365ControlBoxResources.Microsoft365HelpIconNormal);
                    break;
                case PaletteMode.Microsoft365SilverDarkMode:
                    UpdateImage(Microsoft365ControlBoxResources.Microsoft365HelpIconNormal);
                    break;
                case PaletteMode.Microsoft365SilverLightMode:
                    UpdateImage(Microsoft365ControlBoxResources.Microsoft365HelpIconNormal);
                    break;
                case PaletteMode.Microsoft365White:
                    UpdateImage(Microsoft365ControlBoxResources.Microsoft365HelpIconNormal);
                    break;
                case PaletteMode.SparkleBlue:
                    UpdateImage(Office2010ControlBoxResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.SparkleBlueDarkMode:
                    UpdateImage(Office2010ControlBoxResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.SparkleBlueLightMode:
                    UpdateImage(Office2010ControlBoxResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.SparkleOrange:
                    UpdateImage(Office2010ControlBoxResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.SparkleOrangeDarkMode:
                    UpdateImage(Office2010ControlBoxResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.SparkleOrangeLightMode:
                    UpdateImage(Office2010ControlBoxResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.SparklePurple:
                    UpdateImage(Office2010ControlBoxResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.SparklePurpleDarkMode:
                    UpdateImage(Office2010ControlBoxResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.SparklePurpleLightMode:
                    UpdateImage(Office2010ControlBoxResources.Office2010HelpIconNormal);
                    break;
                case PaletteMode.Custom:
                    UpdateImage(Office2010ControlBoxResources.Office2010HelpIconNormal);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }

            UpdateActiveImage(mode);

            UpdateDisabledImage(mode);

            UpdateNormalImage(mode);

            UpdatePressedImage(mode);
        }

        /// <summary>Updates the active image.</summary>
        /// <param name="mode">The mode.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">mode - null</exception>
        private void UpdateActiveImage(PaletteMode mode)
        {
            switch (mode)
            {
                case PaletteMode.Global:
                    break;
                case PaletteMode.ProfessionalSystem:
                    UpdateActiveImage(ProfessionalControlBoxResources.ProfessionalHelpIconNormal);
                    break;
                case PaletteMode.ProfessionalOffice2003:
                    UpdateActiveImage(ProfessionalControlBoxResources.ProfessionalHelpIconNormal);
                    break;
                case PaletteMode.Office2007DarkGray:
                    UpdateActiveImage(Office2007ControlBoxResources.Office2007HelpIconHover);
                    break;
                case PaletteMode.Office2007Blue:
                    UpdateActiveImage(Office2007ControlBoxResources.Office2007HelpIconHover);
                    break;
                case PaletteMode.Office2007BlueDarkMode:
                    UpdateActiveImage(Office2007ControlBoxResources.Office2007HelpIconHover);
                    break;
                case PaletteMode.Office2007BlueLightMode:
                    UpdateActiveImage(Office2007ControlBoxResources.Office2007HelpIconHover);
                    break;
                case PaletteMode.Office2007Silver:
                    UpdateActiveImage(Office2007ControlBoxResources.Office2007HelpIconHover);
                    break;
                case PaletteMode.Office2007SilverDarkMode:
                    UpdateActiveImage(Office2007ControlBoxResources.Office2007HelpIconHover);
                    break;
                case PaletteMode.Office2007SilverLightMode:
                    UpdateActiveImage(Office2007ControlBoxResources.Office2007HelpIconHover);
                    break;
                case PaletteMode.Office2007White:
                    UpdateActiveImage(Office2007ControlBoxResources.Office2007HelpIconHover);
                    break;
                case PaletteMode.Office2007Black:
                    UpdateActiveImage(Office2007ControlBoxResources.Office2007HelpIconHover);
                    break;
                case PaletteMode.Office2007BlackDarkMode:
                    UpdateActiveImage(Office2007ControlBoxResources.Office2007HelpIconHover);
                    break;
                case PaletteMode.Office2010DarkGray:
                    UpdateActiveImage(Office2010ControlBoxResources.Office2010HelpIconHover);
                    break;
                case PaletteMode.Office2010Blue:
                    UpdateActiveImage(Office2010ControlBoxResources.Office2010HelpIconHover);
                    break;
                case PaletteMode.Office2010BlueDarkMode:
                    UpdateActiveImage(Office2010ControlBoxResources.Office2010HelpIconHover);
                    break;
                case PaletteMode.Office2010BlueLightMode:
                    UpdateActiveImage(Office2010ControlBoxResources.Office2010HelpIconHover);
                    break;
                case PaletteMode.Office2010Silver:
                    UpdateActiveImage(Office2010ControlBoxResources.Office2010HelpIconHover);
                    break;
                case PaletteMode.Office2010SilverDarkMode:
                    UpdateActiveImage(Office2010ControlBoxResources.Office2010HelpIconHover);
                    break;
                case PaletteMode.Office2010SilverLightMode:
                    UpdateActiveImage(Office2010ControlBoxResources.Office2010HelpIconHover);
                    break;
                case PaletteMode.Office2010White:
                    UpdateActiveImage(Office2010ControlBoxResources.Office2010HelpIconHover);
                    break;
                case PaletteMode.Office2010Black:
                    UpdateActiveImage(Office2010ControlBoxResources.Office2010HelpIconHover);
                    break;
                case PaletteMode.Office2010BlackDarkMode:
                    UpdateActiveImage(Office2010ControlBoxResources.Office2010HelpIconHover);
                    break;
                case PaletteMode.Office2013DarkGray:
                case PaletteMode.Office2013LightGray:
                case PaletteMode.Office2013White:
                    UpdateActiveImage(Office2013ControlBoxResources.Office2013HelpActive);
                    break;
                case PaletteMode.Microsoft365DarkGray:
                    UpdateActiveImage(Microsoft365ControlBoxResources.Microsoft365HelpIconHover);
                    break;
                case PaletteMode.Microsoft365Black:
                    UpdateActiveImage(Microsoft365ControlBoxResources.Microsoft365HelpIconHover);
                    break;
                case PaletteMode.Microsoft365BlackDarkMode:
                    UpdateActiveImage(Microsoft365ControlBoxResources.Microsoft365HelpIconHover);
                    break;
                case PaletteMode.Microsoft365Blue:
                    UpdateActiveImage(Microsoft365ControlBoxResources.Microsoft365HelpIconHover);
                    break;
                case PaletteMode.Microsoft365BlueDarkMode:
                    UpdateActiveImage(Microsoft365ControlBoxResources.Microsoft365HelpIconHover);
                    break;
                case PaletteMode.Microsoft365BlueLightMode:
                    UpdateActiveImage(Microsoft365ControlBoxResources.Microsoft365HelpIconHover);
                    break;
                case PaletteMode.Microsoft365Silver:
                    UpdateActiveImage(Microsoft365ControlBoxResources.Microsoft365HelpIconHover);
                    break;
                case PaletteMode.Microsoft365SilverDarkMode:
                    UpdateActiveImage(Microsoft365ControlBoxResources.Microsoft365HelpIconHover);
                    break;
                case PaletteMode.Microsoft365SilverLightMode:
                    UpdateActiveImage(Microsoft365ControlBoxResources.Microsoft365HelpIconHover);
                    break;
                case PaletteMode.Microsoft365White:
                    UpdateActiveImage(Microsoft365ControlBoxResources.Microsoft365HelpIconHover);
                    break;
                case PaletteMode.SparkleBlue:
                    UpdateActiveImage(Office2010ControlBoxResources.Office2010HelpIconHover);
                    break;
                case PaletteMode.SparkleBlueDarkMode:
                    UpdateActiveImage(Office2010ControlBoxResources.Office2010HelpIconHover);
                    break;
                case PaletteMode.SparkleBlueLightMode:
                    UpdateActiveImage(Office2010ControlBoxResources.Office2010HelpIconHover);
                    break;
                case PaletteMode.SparkleOrange:
                    UpdateActiveImage(Office2010ControlBoxResources.Office2010HelpIconHover);
                    break;
                case PaletteMode.SparkleOrangeDarkMode:
                    UpdateActiveImage(Office2010ControlBoxResources.Office2010HelpIconHover);
                    break;
                case PaletteMode.SparkleOrangeLightMode:
                    UpdateActiveImage(Office2010ControlBoxResources.Office2010HelpIconHover);
                    break;
                case PaletteMode.SparklePurple:
                    UpdateActiveImage(Office2010ControlBoxResources.Office2010HelpIconHover);
                    break;
                case PaletteMode.SparklePurpleDarkMode:
                    UpdateActiveImage(Office2010ControlBoxResources.Office2010HelpIconHover);
                    break;
                case PaletteMode.SparklePurpleLightMode:
                    UpdateActiveImage(Office2010ControlBoxResources.Office2010HelpIconHover);
                    break;
                case PaletteMode.Custom:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        /// <summary>Updates the disabled image.</summary>
        /// <param name="mode">The mode.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">mode - null</exception>
        private void UpdateDisabledImage(PaletteMode mode)
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
                case PaletteMode.Office2013LightGray:
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

        /// <summary>Updates the normal image.</summary>
        /// <param name="mode">The mode.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">mode - null</exception>
        private void UpdateNormalImage(PaletteMode mode)
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
                case PaletteMode.Office2013LightGray:
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

        /// <summary>Updates the pressed image.</summary>
        /// <param name="mode">The mode.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">mode - null</exception>
        private void UpdatePressedImage(PaletteMode mode)
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
                case PaletteMode.Office2013LightGray:
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

        /// <summary>Updates the image states.</summary>
        /// <param name="mode">The mode.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">mode - null</exception>
        private void UpdateImageStates(PaletteMode mode)
        {
            if (_helpButtonSpec != null)
            {
                switch (mode)
                {
                    case PaletteMode.Global:
                        break;
                    case PaletteMode.ProfessionalSystem:
                        AddImageStates(null, ProfessionalControlBoxResources.ProfessionalHelpIconDisabled, ProfessionalControlBoxResources.ProfessionalHelpIconNormal, null);
                        break;
                    case PaletteMode.ProfessionalOffice2003:
                        AddImageStates(null, ProfessionalControlBoxResources.ProfessionalHelpIconDisabled, ProfessionalControlBoxResources.ProfessionalHelpIconNormal, null);
                        break;
                    case PaletteMode.Office2007DarkGray:
                        AddImageStates(Office2007ControlBoxResources.Office2007HelpIconHover, Office2007ControlBoxResources.Office2007HelpIconDisabled, Office2007ControlBoxResources.Office2007HelpIconNormal, Office2007ControlBoxResources.Office2007HelpIconPressed);
                        break;
                    case PaletteMode.Office2007Blue:
                        AddImageStates(Office2007ControlBoxResources.Office2007HelpIconHover, Office2007ControlBoxResources.Office2007HelpIconDisabled, Office2007ControlBoxResources.Office2007HelpIconNormal, Office2007ControlBoxResources.Office2007HelpIconPressed);
                        break;
                    case PaletteMode.Office2007BlueDarkMode:
                        AddImageStates(Office2007ControlBoxResources.Office2007HelpIconHover, Office2007ControlBoxResources.Office2007HelpIconDisabled, Office2007ControlBoxResources.Office2007HelpIconNormal, Office2007ControlBoxResources.Office2007HelpIconPressed);
                        break;
                    case PaletteMode.Office2007BlueLightMode:
                        AddImageStates(Office2007ControlBoxResources.Office2007HelpIconHover, Office2007ControlBoxResources.Office2007HelpIconDisabled, Office2007ControlBoxResources.Office2007HelpIconNormal, Office2007ControlBoxResources.Office2007HelpIconPressed);
                        break;
                    case PaletteMode.Office2007Silver:
                        AddImageStates(Office2007ControlBoxResources.Office2007HelpIconHover, Office2007ControlBoxResources.Office2007HelpIconDisabled, Office2007ControlBoxResources.Office2007HelpIconNormal, Office2007ControlBoxResources.Office2007HelpIconPressed);
                        break;
                    case PaletteMode.Office2007SilverDarkMode:
                        AddImageStates(Office2007ControlBoxResources.Office2007HelpIconHover, Office2007ControlBoxResources.Office2007HelpIconDisabled, Office2007ControlBoxResources.Office2007HelpIconNormal, Office2007ControlBoxResources.Office2007HelpIconPressed);
                        break;
                    case PaletteMode.Office2007SilverLightMode:
                        AddImageStates(Office2007ControlBoxResources.Office2007HelpIconHover, Office2007ControlBoxResources.Office2007HelpIconDisabled, Office2007ControlBoxResources.Office2007HelpIconNormal, Office2007ControlBoxResources.Office2007HelpIconPressed);
                        break;
                    case PaletteMode.Office2007White:
                        AddImageStates(Office2007ControlBoxResources.Office2007HelpIconHover, Office2007ControlBoxResources.Office2007HelpIconDisabled, Office2007ControlBoxResources.Office2007HelpIconNormal, Office2007ControlBoxResources.Office2007HelpIconPressed);
                        break;
                    case PaletteMode.Office2007Black:
                        AddImageStates(Office2007ControlBoxResources.Office2007HelpIconHover, Office2007ControlBoxResources.Office2007HelpIconDisabled, Office2007ControlBoxResources.Office2007HelpIconNormal, Office2007ControlBoxResources.Office2007HelpIconPressed);
                        break;
                    case PaletteMode.Office2007BlackDarkMode:
                        AddImageStates(Office2007ControlBoxResources.Office2007HelpIconHover, Office2007ControlBoxResources.Office2007HelpIconDisabled, Office2007ControlBoxResources.Office2007HelpIconNormal, Office2007ControlBoxResources.Office2007HelpIconPressed);
                        break;
                    case PaletteMode.Office2010DarkGray:
                        AddImageStates(Office2010ControlBoxResources.Office2010HelpIconHover, Office2010ControlBoxResources.Office2010HelpIconDisabled, Office2010ControlBoxResources.Office2010HelpIconNormal, Office2010ControlBoxResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.Office2010Blue:
                        AddImageStates(Office2010ControlBoxResources.Office2010HelpIconHover, Office2010ControlBoxResources.Office2010HelpIconDisabled, Office2010ControlBoxResources.Office2010HelpIconNormal, Office2010ControlBoxResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.Office2010BlueDarkMode:
                        AddImageStates(Office2010ControlBoxResources.Office2010HelpIconHover, Office2010ControlBoxResources.Office2010HelpIconDisabled, Office2010ControlBoxResources.Office2010HelpIconNormal, Office2010ControlBoxResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.Office2010BlueLightMode:
                        AddImageStates(Office2010ControlBoxResources.Office2010HelpIconHover, Office2010ControlBoxResources.Office2010HelpIconDisabled, Office2010ControlBoxResources.Office2010HelpIconNormal, Office2010ControlBoxResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.Office2010Silver:
                        AddImageStates(Office2010ControlBoxResources.Office2010HelpIconHover, Office2010ControlBoxResources.Office2010HelpIconDisabled, Office2010ControlBoxResources.Office2010HelpIconNormal, Office2010ControlBoxResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.Office2010SilverDarkMode:
                        AddImageStates(Office2010ControlBoxResources.Office2010HelpIconHover, Office2010ControlBoxResources.Office2010HelpIconDisabled, Office2010ControlBoxResources.Office2010HelpIconNormal, Office2010ControlBoxResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.Office2010SilverLightMode:
                        AddImageStates(Office2010ControlBoxResources.Office2010HelpIconHover, Office2010ControlBoxResources.Office2010HelpIconDisabled, Office2010ControlBoxResources.Office2010HelpIconNormal, Office2010ControlBoxResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.Office2010White:
                        AddImageStates(Office2010ControlBoxResources.Office2010HelpIconHover, Office2010ControlBoxResources.Office2010HelpIconDisabled, Office2010ControlBoxResources.Office2010HelpIconNormal, Office2010ControlBoxResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.Office2010Black:
                        AddImageStates(Office2010ControlBoxResources.Office2010HelpIconHover, Office2010ControlBoxResources.Office2010HelpIconDisabled, Office2010ControlBoxResources.Office2010HelpIconNormal, Office2010ControlBoxResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.Office2010BlackDarkMode:
                        AddImageStates(Office2010ControlBoxResources.Office2010HelpIconHover, Office2010ControlBoxResources.Office2010HelpIconDisabled, Office2010ControlBoxResources.Office2010HelpIconNormal, Office2010ControlBoxResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.Office2013DarkGray:
                    case PaletteMode.Office2013LightGray:
                    case PaletteMode.Office2013White:
                        AddImageStates(Microsoft365ControlBoxResources.Microsoft365HelpIconHover, Microsoft365ControlBoxResources.Microsoft365HelpIconDisabled, Microsoft365ControlBoxResources.Microsoft365HelpIconNormal, Microsoft365ControlBoxResources.Microsoft365HelpIconPressed);
                        break;
                    case PaletteMode.Microsoft365DarkGray:
                        AddImageStates(Microsoft365ControlBoxResources.Microsoft365HelpIconHover, Microsoft365ControlBoxResources.Microsoft365HelpIconDisabled, Microsoft365ControlBoxResources.Microsoft365HelpIconNormal, Microsoft365ControlBoxResources.Microsoft365HelpIconPressed);
                        break;
                    case PaletteMode.Microsoft365Black:
                        AddImageStates(Microsoft365ControlBoxResources.Microsoft365HelpIconHover, Microsoft365ControlBoxResources.Microsoft365HelpIconDisabled, Microsoft365ControlBoxResources.Microsoft365HelpIconNormal, Microsoft365ControlBoxResources.Microsoft365HelpIconPressed);
                        break;
                    case PaletteMode.Microsoft365BlackDarkMode:
                        AddImageStates(Microsoft365ControlBoxResources.Microsoft365HelpIconHover, Microsoft365ControlBoxResources.Microsoft365HelpIconDisabled, Microsoft365ControlBoxResources.Microsoft365HelpIconNormal, Microsoft365ControlBoxResources.Microsoft365HelpIconPressed);
                        break;
                    case PaletteMode.Microsoft365Blue:
                        AddImageStates(Microsoft365ControlBoxResources.Microsoft365HelpIconHover, Microsoft365ControlBoxResources.Microsoft365HelpIconDisabled, Microsoft365ControlBoxResources.Microsoft365HelpIconNormal, Microsoft365ControlBoxResources.Microsoft365HelpIconPressed);
                        break;
                    case PaletteMode.Microsoft365BlueDarkMode:
                        AddImageStates(Microsoft365ControlBoxResources.Microsoft365HelpIconHover, Microsoft365ControlBoxResources.Microsoft365HelpIconDisabled, Microsoft365ControlBoxResources.Microsoft365HelpIconNormal, Microsoft365ControlBoxResources.Microsoft365HelpIconPressed);
                        break;
                    case PaletteMode.Microsoft365BlueLightMode:
                        AddImageStates(Microsoft365ControlBoxResources.Microsoft365HelpIconHover, Microsoft365ControlBoxResources.Microsoft365HelpIconDisabled, Microsoft365ControlBoxResources.Microsoft365HelpIconNormal, Microsoft365ControlBoxResources.Microsoft365HelpIconPressed);
                        break;
                    case PaletteMode.Microsoft365Silver:
                        AddImageStates(Microsoft365ControlBoxResources.Microsoft365HelpIconHover, Microsoft365ControlBoxResources.Microsoft365HelpIconDisabled, Microsoft365ControlBoxResources.Microsoft365HelpIconNormal, Microsoft365ControlBoxResources.Microsoft365HelpIconPressed);
                        break;
                    case PaletteMode.Microsoft365SilverDarkMode:
                        AddImageStates(Microsoft365ControlBoxResources.Microsoft365HelpIconHover, Microsoft365ControlBoxResources.Microsoft365HelpIconDisabled, Microsoft365ControlBoxResources.Microsoft365HelpIconNormal, Microsoft365ControlBoxResources.Microsoft365HelpIconPressed);
                        break;
                    case PaletteMode.Microsoft365SilverLightMode:
                        AddImageStates(Microsoft365ControlBoxResources.Microsoft365HelpIconHover, Microsoft365ControlBoxResources.Microsoft365HelpIconDisabled, Microsoft365ControlBoxResources.Microsoft365HelpIconNormal, Microsoft365ControlBoxResources.Microsoft365HelpIconPressed);
                        break;
                    case PaletteMode.Microsoft365White:
                        AddImageStates(Microsoft365ControlBoxResources.Microsoft365HelpIconHover, Microsoft365ControlBoxResources.Microsoft365HelpIconDisabled, Microsoft365ControlBoxResources.Microsoft365HelpIconNormal, Microsoft365ControlBoxResources.Microsoft365HelpIconPressed);
                        break;
                    case PaletteMode.SparkleBlue:
                        AddImageStates(Office2010ControlBoxResources.Office2010HelpIconHover, Office2010ControlBoxResources.Office2010HelpIconDisabled, Office2010ControlBoxResources.Office2010HelpIconNormal, Office2010ControlBoxResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.SparkleBlueDarkMode:
                        AddImageStates(Office2010ControlBoxResources.Office2010HelpIconHover, Office2010ControlBoxResources.Office2010HelpIconDisabled, Office2010ControlBoxResources.Office2010HelpIconNormal, Office2010ControlBoxResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.SparkleBlueLightMode:
                        AddImageStates(Office2010ControlBoxResources.Office2010HelpIconHover, Office2010ControlBoxResources.Office2010HelpIconDisabled, Office2010ControlBoxResources.Office2010HelpIconNormal, Office2010ControlBoxResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.SparkleOrange:
                        AddImageStates(Office2010ControlBoxResources.Office2010HelpIconHover, Office2010ControlBoxResources.Office2010HelpIconDisabled, Office2010ControlBoxResources.Office2010HelpIconNormal, Office2010ControlBoxResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.SparkleOrangeDarkMode:
                        AddImageStates(Office2010ControlBoxResources.Office2010HelpIconHover, Office2010ControlBoxResources.Office2010HelpIconDisabled, Office2010ControlBoxResources.Office2010HelpIconNormal, Office2010ControlBoxResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.SparkleOrangeLightMode:
                        AddImageStates(Office2010ControlBoxResources.Office2010HelpIconHover, Office2010ControlBoxResources.Office2010HelpIconDisabled, Office2010ControlBoxResources.Office2010HelpIconNormal, Office2010ControlBoxResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.SparklePurple:
                        AddImageStates(Office2010ControlBoxResources.Office2010HelpIconHover, Office2010ControlBoxResources.Office2010HelpIconDisabled, Office2010ControlBoxResources.Office2010HelpIconNormal, Office2010ControlBoxResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.SparklePurpleDarkMode:
                        AddImageStates(Office2010ControlBoxResources.Office2010HelpIconHover, Office2010ControlBoxResources.Office2010HelpIconDisabled, Office2010ControlBoxResources.Office2010HelpIconNormal, Office2010ControlBoxResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.SparklePurpleLightMode:
                        AddImageStates(Office2010ControlBoxResources.Office2010HelpIconHover, Office2010ControlBoxResources.Office2010HelpIconDisabled, Office2010ControlBoxResources.Office2010HelpIconNormal, Office2010ControlBoxResources.Office2010HelpIconPressed);
                        break;
                    case PaletteMode.Custom:
                        AddImageStates(Microsoft365ControlBoxResources.Microsoft365HelpIconHover, Microsoft365ControlBoxResources.Microsoft365HelpIconDisabled, Microsoft365ControlBoxResources.Microsoft365HelpIconNormal, Microsoft365ControlBoxResources.Microsoft365HelpIconPressed);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
                }
            }
        }

        #endregion

        #region Overrides

        protected override void OnPropertyChanged(PropertyChangedEventArgs e) => base.OnPropertyChanged(e);

        #endregion
    }
}