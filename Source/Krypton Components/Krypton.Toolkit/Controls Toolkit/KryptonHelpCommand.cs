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

        private ButtonSpecAny _helpButtonSpec;

        #endregion

        #region Public

        public ButtonSpecAny HelpButton
        {
            get => _helpButtonSpec;
            set { _helpButtonSpec = value; UpdateImage(KryptonManager.InternalGlobalPaletteMode); }
        }

        #endregion

        #region Identity

        public KryptonHelpCommand()
        {
            switch (KryptonManager.InternalGlobalPaletteMode)
            {

            }
        }

        #endregion

        #region Implementation

        /// <summary>Updates the image.</summary>
        /// <param name="helpImage">The help image.</param>
        private void UpdateImage(Image helpImage) => ImageSmall = helpImage;

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

        #endregion

        #region Overrides

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
        }

        #endregion
    }
}