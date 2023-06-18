﻿#region BSD License
/*
 * 
 *  QuickPrint BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>A <see cref="KryptonCommand"/> created specifically for the <see cref="PaletteButtonSpecStyle.QuickPrint"/> button spec.</summary>
    [ToolboxItem(true)]
    //[ToolboxBitmap(typeof(KryptonHelpCommand), @"ToolboxBitmaps.KryptonHelp.bmp")]
    [Description(@"For use with the 'QuickPrint' ButtonSpec style.")]
    [DesignerCategory(@"code")]
    public class KryptonIntegratedToolbarQuickPrintCommand : KryptonCommand
    {
        #region Instance Fields

        private ButtonSpecAny? _quickPrintButtonSpec;

        private ButtonImageStates? _imageStates;

        private Image? _activeImage;

        private Image? _disabledImage;

        private Image? _normalImage;

        private Image? _pressedImage;

        #endregion

        #region Public

        /// <summary>Gets or sets the quick print button.</summary>
        /// <value>The quick print button.</value>
        [DefaultValue(null), Description(@"Access to the quick print button spec.")]
        public ButtonSpecAny? ToolBarQuickPrintButton
        {
            get => _quickPrintButtonSpec ?? new();
            set { _quickPrintButtonSpec = value; UpdateImage(KryptonManager.InternalGlobalPaletteMode); }
        }

        /// <summary>Gets the active image.</summary>
        /// <value>The active image.</value>
        public Image? ActiveImage { get => _activeImage; private set => _activeImage = value; }

        /// <summary>Gets the disabled image.</summary>
        /// <value>The disabled image.</value>
        public Image? DisabledImage { get => _disabledImage; private set => _disabledImage = value; }

        public Image? NormalImage { get => _normalImage; private set => _normalImage = value; }

        public Image? PressedImage { get => _pressedImage; private set => _pressedImage = value; }

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonIntegratedToolbarQuickPrintCommand" /> class.</summary>
        public KryptonIntegratedToolbarQuickPrintCommand()
        {
            _imageStates = new();

            Text = KryptonLanguageManager.ToolBarStrings.QuickPrint;
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
            if (_quickPrintButtonSpec != null)
            {
                _quickPrintButtonSpec.ImageStates.ImageDisabled = disabledImage;

                _quickPrintButtonSpec.ImageStates.ImageTracking = activeImage ?? null;

                _quickPrintButtonSpec.ImageStates.ImageNormal = normalImage;

                _quickPrintButtonSpec.ImageStates.ImagePressed = pressedImage ?? null;
            }
        }

        /// <summary>Updates the active image.</summary>
        /// <param name="activeImage">The active image.</param>
        private void UpdateActiveImage(Image activeImage)
        {
            _activeImage = activeImage;

            if (_quickPrintButtonSpec != null)
            {
                _quickPrintButtonSpec.ImageStates.ImageTracking = _activeImage;
            }
        }

        /// <summary>Updates the disabled image.</summary>
        /// <param name="disabledImage">The disabled image.</param>
        private void UpdateDisabledImage(Image disabledImage)
        {
            _disabledImage = disabledImage;

            if (_quickPrintButtonSpec != null)
            {
                _quickPrintButtonSpec.ImageStates.ImageDisabled = disabledImage;
            }
        }

        /// <summary>Updates the normal image.</summary>
        /// <param name="normalImage">The normal image.</param>
        private void UpdateNormalImage(Image normalImage)
        {
            _normalImage = normalImage;

            if (_quickPrintButtonSpec != null)
            {
                _quickPrintButtonSpec.ImageStates.ImageNormal = normalImage;
            }
        }

        /// <summary>Updates the pressed image.</summary>
        /// <param name="pressedImage">The pressed image.</param>
        private void UpdatePressedImage(Image pressedImage)
        {
            _pressedImage = pressedImage;

            if (_quickPrintButtonSpec != null)
            {
                _quickPrintButtonSpec.ImageStates.ImagePressed = pressedImage;
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
                    UpdateImage(GenericToolbarImageResources.GenericQuickPrint);
                    break;
                case PaletteMode.ProfessionalOffice2003:
                    UpdateImage(GenericToolbarImageResources.GenericQuickPrint);
                    break;
                case PaletteMode.Office2007DarkGray:
                    UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007Blue:
                    UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007BlueDarkMode:
                    UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007BlueLightMode:
                    UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007Silver:
                    UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007SilverDarkMode:
                    UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007SilverLightMode:
                    UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007White:
                    UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007Black:
                    UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007BlackDarkMode:
                    UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010DarkGray:
                    UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010Blue:
                    UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010BlueDarkMode:
                    UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010BlueLightMode:
                    UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010Silver:
                    UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010SilverDarkMode:
                    UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010SilverLightMode:
                    UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010White:
                    UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010Black:
                    UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010BlackDarkMode:
                    UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2013DarkGray:
                    UpdateImage(Office2013ToolbarImageResources.Office2013ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2013White:
                    UpdateImage(Office2013ToolbarImageResources.Office2013ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365DarkGray:
                    UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365Black:
                    UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365BlackDarkMode:
                    UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365Blue:
                    UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365BlueDarkMode:
                    UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365BlueLightMode:
                    UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365Silver:
                    UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365SilverDarkMode:
                    UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365SilverLightMode:
                    UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365White:
                    UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleBlue:
                    UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleBlueDarkMode:
                    UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleBlueLightMode:
                    UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleOrange:
                    UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleOrangeDarkMode:
                    UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleOrangeLightMode:
                    UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparklePurple:
                    UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparklePurpleDarkMode:
                    UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparklePurpleLightMode:
                    UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Custom:
                    UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
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
                    UpdateActiveImage(GenericToolbarImageResources.GenericQuickPrint);
                    break;
                case PaletteMode.ProfessionalOffice2003:
                    UpdateActiveImage(GenericToolbarImageResources.GenericQuickPrint);
                    break;
                case PaletteMode.Office2007DarkGray:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007Blue:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007BlueDarkMode:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007BlueLightMode:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007Silver:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007SilverDarkMode:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007SilverLightMode:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007White:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007Black:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007BlackDarkMode:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010DarkGray:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010Blue:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010BlueDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010BlueLightMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010Silver:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010SilverDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010SilverLightMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010White:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010Black:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010BlackDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2013DarkGray:
                    UpdateActiveImage(Office2013ToolbarImageResources.Office2013ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2013White:
                    UpdateActiveImage(Office2013ToolbarImageResources.Office2013ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365DarkGray:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365Black:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365BlackDarkMode:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365Blue:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365BlueDarkMode:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365BlueLightMode:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365Silver:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365SilverDarkMode:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365SilverLightMode:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365White:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleBlue:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleBlueDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleBlueLightMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleOrange:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleOrangeDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleOrangeLightMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparklePurple:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparklePurpleDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparklePurpleLightMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
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
                    UpdateActiveImage(GenericToolbarImageResources.GenericQuickPrint);
                    break;
                case PaletteMode.ProfessionalOffice2003:
                    UpdateActiveImage(GenericToolbarImageResources.GenericQuickPrint);
                    break;
                case PaletteMode.Office2007DarkGray:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Office2007Blue:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Office2007BlueDarkMode:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Office2007BlueLightMode:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Office2007Silver:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Office2007SilverDarkMode:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Office2007SilverLightMode:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Office2007White:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Office2007Black:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Office2007BlackDarkMode:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Office2010DarkGray:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Office2010Blue:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Office2010BlueDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Office2010BlueLightMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Office2010Silver:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Office2010SilverDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Office2010SilverLightMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Office2010White:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Office2010Black:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Office2010BlackDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Office2013DarkGray:
                    UpdateActiveImage(Office2013ToolbarImageResources.Office2013ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Office2013White:
                    UpdateActiveImage(Office2013ToolbarImageResources.Office2013ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Microsoft365DarkGray:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Microsoft365Black:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Microsoft365BlackDarkMode:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Microsoft365Blue:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Microsoft365BlueDarkMode:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Microsoft365BlueLightMode:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Microsoft365Silver:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Microsoft365SilverDarkMode:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Microsoft365SilverLightMode:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.Microsoft365White:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.SparkleBlue:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.SparkleBlueDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.SparkleBlueLightMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.SparkleOrange:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.SparkleOrangeDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.SparkleOrangeLightMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.SparklePurple:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.SparklePurpleDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled);
                    break;
                case PaletteMode.SparklePurpleLightMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled);
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
                    UpdateActiveImage(GenericToolbarImageResources.GenericQuickPrint);
                    break;
                case PaletteMode.ProfessionalOffice2003:
                    UpdateActiveImage(GenericToolbarImageResources.GenericQuickPrint);
                    break;
                case PaletteMode.Office2007DarkGray:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007Blue:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007BlueDarkMode:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007BlueLightMode:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007Silver:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007SilverDarkMode:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007SilverLightMode:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007White:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007Black:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007BlackDarkMode:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010DarkGray:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010Blue:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010BlueDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010BlueLightMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010Silver:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010SilverDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010SilverLightMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010White:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010Black:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010BlackDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2013DarkGray:
                    UpdateActiveImage(Office2013ToolbarImageResources.Office2013ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2013White:
                    UpdateActiveImage(Office2013ToolbarImageResources.Office2013ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365DarkGray:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365Black:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365BlackDarkMode:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365Blue:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365BlueDarkMode:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365BlueLightMode:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365Silver:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365SilverDarkMode:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365SilverLightMode:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365White:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleBlue:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleBlueDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleBlueLightMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleOrange:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleOrangeDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleOrangeLightMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparklePurple:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparklePurpleDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparklePurpleLightMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
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
                    UpdateActiveImage(GenericToolbarImageResources.GenericQuickPrint);
                    break;
                case PaletteMode.ProfessionalOffice2003:
                    UpdateActiveImage(GenericToolbarImageResources.GenericQuickPrint);
                    break;
                case PaletteMode.Office2007DarkGray:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007Blue:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007BlueDarkMode:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007BlueLightMode:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007Silver:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007SilverDarkMode:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007SilverLightMode:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007White:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007Black:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2007BlackDarkMode:
                    UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010DarkGray:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010Blue:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010BlueDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010BlueLightMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010Silver:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010SilverDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010SilverLightMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010White:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010Black:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2010BlackDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2013DarkGray:
                    UpdateActiveImage(Office2013ToolbarImageResources.Office2013ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Office2013White:
                    UpdateActiveImage(Office2013ToolbarImageResources.Office2013ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365DarkGray:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365Black:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365BlackDarkMode:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365Blue:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365BlueDarkMode:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365BlueLightMode:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365Silver:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365SilverDarkMode:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365SilverLightMode:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.Microsoft365White:
                    UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleBlue:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleBlueDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleBlueLightMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleOrange:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleOrangeDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparkleOrangeLightMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparklePurple:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparklePurpleDarkMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                    break;
                case PaletteMode.SparklePurpleLightMode:
                    UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
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
            if (_quickPrintButtonSpec != null)
            {
                switch (mode)
                {
                    case PaletteMode.Global:
                        break;
                    case PaletteMode.ProfessionalSystem:
                        AddImageStates(null, GenericToolbarImageResources.GenericQuickPrint, GenericToolbarImageResources.GenericQuickPrint, null);
                        break;
                    case PaletteMode.ProfessionalOffice2003:
                        AddImageStates(null, GenericToolbarImageResources.GenericQuickPrint, GenericToolbarImageResources.GenericQuickPrint, null);
                        break;
                    case PaletteMode.Office2007DarkGray:
                        AddImageStates(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintDisabled, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Office2007Blue:
                        AddImageStates(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintDisabled, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Office2007BlueDarkMode:
                        AddImageStates(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintDisabled, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Office2007BlueLightMode:
                        AddImageStates(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintDisabled, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Office2007Silver:
                        AddImageStates(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintDisabled, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Office2007SilverDarkMode:
                        AddImageStates(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintDisabled, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Office2007SilverLightMode:
                        AddImageStates(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintDisabled, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Office2007White:
                        AddImageStates(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintDisabled, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Office2007Black:
                        AddImageStates(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintDisabled, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Office2007BlackDarkMode:
                        AddImageStates(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintDisabled, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal, Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Office2010DarkGray:
                        AddImageStates(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Office2010Blue:
                        AddImageStates(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Office2010BlueDarkMode:
                        AddImageStates(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Office2010BlueLightMode:
                        AddImageStates(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Office2010Silver:
                        AddImageStates(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Office2010SilverDarkMode:
                        AddImageStates(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Office2010SilverLightMode:
                        AddImageStates(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Office2010White:
                        AddImageStates(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Office2010Black:
                        AddImageStates(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Office2010BlackDarkMode:
                        AddImageStates(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Office2013DarkGray:
                        AddImageStates(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Office2013White:
                        AddImageStates(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Microsoft365DarkGray:
                        AddImageStates(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Microsoft365Black:
                        AddImageStates(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Microsoft365BlackDarkMode:
                        AddImageStates(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Microsoft365Blue:
                        AddImageStates(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Microsoft365BlueDarkMode:
                        AddImageStates(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Microsoft365BlueLightMode:
                        AddImageStates(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Microsoft365Silver:
                        AddImageStates(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Microsoft365SilverDarkMode:
                        AddImageStates(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Microsoft365SilverLightMode:
                        AddImageStates(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Microsoft365White:
                        AddImageStates(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.SparkleBlue:
                        AddImageStates(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.SparkleBlueDarkMode:
                        AddImageStates(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.SparkleBlueLightMode:
                        AddImageStates(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.SparkleOrange:
                        AddImageStates(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.SparkleOrangeDarkMode:
                        AddImageStates(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.SparkleOrangeLightMode:
                        AddImageStates(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.SparklePurple:
                        AddImageStates(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.SparklePurpleDarkMode:
                        AddImageStates(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.SparklePurpleLightMode:
                        AddImageStates(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintDisabled, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal, Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                        break;
                    case PaletteMode.Custom:
                        AddImageStates(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintDisabled, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal, Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
                }
            }
        }

        #endregion
    }
}