#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Image? ActiveImage 
    { 
        get => _activeImage; 
        private set => _activeImage = value; 
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Image? DisabledImage 
    { 
        get => _disabledImage; 
        private set => _disabledImage = value; 
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Image? NormalImage 
    { 
        get => _normalImage; 
        private set => _normalImage = value; 
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Image? PressedImage 
    { 
        get => _pressedImage; 
        private set => _pressedImage = value; 
    }

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonHelpCommand" /> class.</summary>
    public KryptonHelpCommand()
    {
        _imageStates = new ButtonImageStates();

        Text = KryptonManager.Strings.ButtonSpecStyleStrings.FormHelp;
    }

    #endregion

    #region Implementation

    /// <summary>Updates the image.</summary>
    /// <param name="helpImage">The help image.</param>
    private void UpdateImage(Image? helpImage) => ImageSmall = helpImage;

    /// <summary>Adds the image states.</summary>
    /// <param name="activeImage">The active image.</param>
    /// <param name="disabledImage">The disabled image.</param>
    /// <param name="normalImage">The normal image.</param>
    /// <param name="pressedImage">The pressed image.</param>
    private void AddImageStates(Image? activeImage, Image? disabledImage, Image? normalImage, Image? pressedImage)
    {
        if (_helpButtonSpec != null)
        {
            _helpButtonSpec.ImageStates.ImageDisabled = disabledImage;

            _helpButtonSpec.ImageStates.ImageTracking = activeImage;

            _helpButtonSpec.ImageStates.ImageNormal = normalImage;

            _helpButtonSpec.ImageStates.ImagePressed = pressedImage;
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
    private void UpdateDisabledImage(Image? disabledImage)
    {
        _disabledImage = disabledImage;

        if (_helpButtonSpec != null)
        {
            _helpButtonSpec.ImageStates.ImageDisabled = disabledImage;
        }
    }

    /// <summary>Updates the normal image.</summary>
    /// <param name="normalImage">The normal image.</param>
    private void UpdateNormalImage(Image? normalImage)
    {
        _normalImage = normalImage;

        if (_helpButtonSpec != null)
        {
            _helpButtonSpec.ImageStates.ImageNormal = normalImage;
        }
    }

    /// <summary>Updates the pressed image.</summary>
    /// <param name="pressedImage">The pressed image.</param>
    private void UpdatePressedImage(Image? pressedImage)
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
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2007DarkGray:
            case PaletteMode.Office2007Blue:
            case PaletteMode.Office2007BlueDarkMode:
            case PaletteMode.Office2007BlueLightMode:
            case PaletteMode.Office2007Silver:
            case PaletteMode.Office2007SilverDarkMode:
            case PaletteMode.Office2007SilverLightMode:
            case PaletteMode.Office2007White:
            case PaletteMode.Office2007Black:
            case PaletteMode.Office2007BlackDarkMode:
                UpdateImage(Office2007ControlBoxResources.Office2007HelpIconNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2010DarkGray:
            case PaletteMode.Office2010Blue:
            case PaletteMode.Office2010BlueDarkMode:
            case PaletteMode.Office2010BlueLightMode:
            case PaletteMode.Office2010Silver:
            case PaletteMode.Office2010SilverDarkMode:
            case PaletteMode.Office2010SilverLightMode:
            case PaletteMode.Office2010White:
            case PaletteMode.Office2010Black:
            case PaletteMode.Office2010BlackDarkMode:
                UpdateImage(Office2010ControlBoxResources.Office2010HelpIconNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2013DarkGray:
            // case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                UpdateImage(Office2013ControlBoxResources.Office2013HelpNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Microsoft365DarkGray:
            case PaletteMode.Microsoft365Black:
            case PaletteMode.Microsoft365BlackDarkMode:
            case PaletteMode.Microsoft365Blue:
            case PaletteMode.Microsoft365BlueDarkMode:
            case PaletteMode.Microsoft365BlueLightMode:
            case PaletteMode.Microsoft365Silver:
            case PaletteMode.Microsoft365SilverDarkMode:
            case PaletteMode.Microsoft365SilverLightMode:
            case PaletteMode.Microsoft365White:
                UpdateImage(Microsoft365ControlBoxResources.Microsoft365HelpIconNormal);
                break;
            case PaletteMode.SparkleBlue:
            case PaletteMode.SparkleBlueDarkMode:
            case PaletteMode.SparkleBlueLightMode:
            case PaletteMode.SparkleOrange:
            case PaletteMode.SparkleOrangeDarkMode:
            case PaletteMode.SparkleOrangeLightMode:
            case PaletteMode.SparklePurple:
            case PaletteMode.SparklePurpleDarkMode:
            case PaletteMode.SparklePurpleLightMode:
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
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2007DarkGray:
            case PaletteMode.Office2007Blue:
            case PaletteMode.Office2007BlueDarkMode:
            case PaletteMode.Office2007BlueLightMode:
            case PaletteMode.Office2007Silver:
            case PaletteMode.Office2007SilverDarkMode:
            case PaletteMode.Office2007SilverLightMode:
            case PaletteMode.Office2007White:
            case PaletteMode.Office2007Black:
            case PaletteMode.Office2007BlackDarkMode:
                UpdateActiveImage(Office2007ControlBoxResources.Office2007HelpIconHover);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2010DarkGray:
            case PaletteMode.Office2010Blue:
            case PaletteMode.Office2010BlueDarkMode:
            case PaletteMode.Office2010BlueLightMode:
            case PaletteMode.Office2010Silver:
            case PaletteMode.Office2010SilverDarkMode:
            case PaletteMode.Office2010SilverLightMode:
            case PaletteMode.Office2010White:
            case PaletteMode.Office2010Black:
            case PaletteMode.Office2010BlackDarkMode:
                UpdateActiveImage(Office2010ControlBoxResources.Office2010HelpIconHover);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2013DarkGray:
            // case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                UpdateActiveImage(Office2013ControlBoxResources.Office2013HelpActive);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Microsoft365DarkGray:
            case PaletteMode.Microsoft365Black:
            case PaletteMode.Microsoft365BlackDarkMode:
            case PaletteMode.Microsoft365Blue:
            case PaletteMode.Microsoft365BlueDarkMode:
            case PaletteMode.Microsoft365BlueLightMode:
            case PaletteMode.Microsoft365Silver:
            case PaletteMode.Microsoft365SilverDarkMode:
            case PaletteMode.Microsoft365SilverLightMode:
            case PaletteMode.Microsoft365White:
                UpdateActiveImage(Microsoft365ControlBoxResources.Microsoft365HelpIconHover);
                break;
            case PaletteMode.SparkleBlue:
            case PaletteMode.SparkleBlueDarkMode:
            case PaletteMode.SparkleBlueLightMode:
            case PaletteMode.SparkleOrange:
            case PaletteMode.SparkleOrangeDarkMode:
            case PaletteMode.SparkleOrangeLightMode:
            case PaletteMode.SparklePurple:
            case PaletteMode.SparklePurpleDarkMode:
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
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2007DarkGray:
            // break;
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
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2010DarkGray:
            //    break;
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
            // TODO: Re-enable this once completed
            //case PaletteMode.Office2013DarkGray:
            //case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                break;
            // TODO: Re-enable this once completed
            //case PaletteMode.Microsoft365DarkGray:
            //    break;
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
            // TODO: Re-enable this once completed
            //case PaletteMode.Office2007DarkGray:
            //    break;
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
            // TODO: Re-enable this once completed
            //case PaletteMode.Office2010DarkGray:
            //    break;
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
            // TODO: Re-enable this once completed
            //case PaletteMode.Office2013DarkGray:
            //case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                break;
            // TODO: Re-enable this once completed
            //case PaletteMode.Microsoft365DarkGray:
            //    break;
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
            // TODO: Re-enable this once completed
            //case PaletteMode.Office2007DarkGray:
            //    break;
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
            // TODO: Re-enable this once completed
            //case PaletteMode.Office2010DarkGray:
            //    break;
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
            // TODO: Re-enable this once completed
            //case PaletteMode.Office2013DarkGray:
            //case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                break;
            // TODO: Re-enable this once completed
            //case PaletteMode.Microsoft365DarkGray:
            //    break;
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
        if (_helpButtonSpec is not null)
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
                // TODO: Re-enable this once completed
                //case PaletteMode.Office2007DarkGray:
                case PaletteMode.Office2007Blue:
                case PaletteMode.Office2007BlueDarkMode:
                case PaletteMode.Office2007BlueLightMode:
                case PaletteMode.Office2007Silver:
                case PaletteMode.Office2007SilverDarkMode:
                case PaletteMode.Office2007SilverLightMode:
                case PaletteMode.Office2007White:
                case PaletteMode.Office2007Black:
                case PaletteMode.Office2007BlackDarkMode:
                    AddImageStates(Office2007ControlBoxResources.Office2007HelpIconHover, Office2007ControlBoxResources.Office2007HelpIconDisabled, Office2007ControlBoxResources.Office2007HelpIconNormal, Office2007ControlBoxResources.Office2007HelpIconPressed);
                    break;
                // TODO: Re-enable this once completed
                // case PaletteMode.Office2010DarkGray:
                case PaletteMode.Office2010Blue:
                case PaletteMode.Office2010BlueDarkMode:
                case PaletteMode.Office2010BlueLightMode:
                case PaletteMode.Office2010Silver:
                case PaletteMode.Office2010SilverDarkMode:
                case PaletteMode.Office2010SilverLightMode:
                case PaletteMode.Office2010White:
                case PaletteMode.Office2010Black:
                case PaletteMode.Office2010BlackDarkMode:
                    AddImageStates(Office2010ControlBoxResources.Office2010HelpIconHover, Office2010ControlBoxResources.Office2010HelpIconDisabled, Office2010ControlBoxResources.Office2010HelpIconNormal, Office2010ControlBoxResources.Office2010HelpIconPressed);
                    break;
                // TODO: Re-enable this once completed
                //case PaletteMode.Office2013DarkGray:
                //case PaletteMode.Office2013LightGray:
                case PaletteMode.Office2013White:
                    AddImageStates(Microsoft365ControlBoxResources.Microsoft365HelpIconHover, Microsoft365ControlBoxResources.Microsoft365HelpIconDisabled, Microsoft365ControlBoxResources.Microsoft365HelpIconNormal, Microsoft365ControlBoxResources.Microsoft365HelpIconPressed);
                    break;
                // TODO: Re-enable this once completed
                // case PaletteMode.Microsoft365DarkGray:
                case PaletteMode.Microsoft365Black:
                case PaletteMode.Microsoft365BlackDarkMode:
                case PaletteMode.Microsoft365Blue:
                case PaletteMode.Microsoft365BlueDarkMode:
                case PaletteMode.Microsoft365BlueLightMode:
                case PaletteMode.Microsoft365Silver:
                case PaletteMode.Microsoft365SilverDarkMode:
                case PaletteMode.Microsoft365SilverLightMode:
                case PaletteMode.Microsoft365White:
                    AddImageStates(Microsoft365ControlBoxResources.Microsoft365HelpIconHover, Microsoft365ControlBoxResources.Microsoft365HelpIconDisabled, Microsoft365ControlBoxResources.Microsoft365HelpIconNormal, Microsoft365ControlBoxResources.Microsoft365HelpIconPressed);
                    break;
                case PaletteMode.SparkleBlue:
                case PaletteMode.SparkleBlueDarkMode:
                case PaletteMode.SparkleBlueLightMode:
                case PaletteMode.SparkleOrange:
                case PaletteMode.SparkleOrangeDarkMode:
                case PaletteMode.SparkleOrangeLightMode:
                case PaletteMode.SparklePurple:
                case PaletteMode.SparklePurpleDarkMode:
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