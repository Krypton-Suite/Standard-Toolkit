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

        #endregion

        #region Public

        /// <summary>Gets or sets the help button.</summary>
        /// <value>The help button.</value>
        [AllowNull, DefaultValue(null), Description(@"Access to the help button spec.")]
        public ButtonSpecAny? HelpButton
        {
            get => _helpButtonSpec ?? new();
            set { _helpButtonSpec = value; UpdateImage(KryptonManager.InternalGlobalPaletteMode); }
        }

        /// <summary>
        /// Gets access to the state specific images for the help button.
        /// </summary>
        [AllowNull, Category(@"Appearance"), Description(@"State specific images for the help button."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ButtonImageStates? ImageStates { get => _imageStates ?? new(); set { _imageStates = value; UpdateImageStates(KryptonManager.InternalGlobalPaletteMode); } }

        private bool ShouldSerializeImageStates() => !_imageStates.IsDefault;

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

        private void AddImageStates(Image activeImage, Image disabledImage, Image normalImage)
        {

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