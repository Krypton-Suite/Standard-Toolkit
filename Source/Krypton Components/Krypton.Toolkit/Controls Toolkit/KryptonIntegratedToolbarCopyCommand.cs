#region BSD License
/*
 * 
 *  Copy BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>A <see cref="KryptonCommand"/> created specifically for the <see cref="PaletteButtonSpecStyle.Copy"/> button spec.</summary>
[Category(@"code")]
[ToolboxItem(false)]
//[ToolboxBitmap(typeof(KryptonHelpCommand), @"ToolboxBitmaps.KryptonHelp.bmp")]
[Description(@"For use with the 'Copy' ButtonSpec style.")]
[DesignerCategory(@"code")]
public class KryptonIntegratedToolbarCopyCommand : KryptonCommand
{
    #region Instance Fields

    private ButtonSpecAny? _copyButtonSpec;

    private ButtonImageStates? _imageStates;

    private Image? _activeImage;

    private Image? _disabledImage;

    private Image? _normalImage;

    private Image? _pressedImage;


    #endregion

    #region Public

    /// <summary>Gets or sets the copy button.</summary>
    /// <value>The copy button.</value>
    [DefaultValue(null), Description(@"Access to the copy button spec.")]
    [AllowNull]
    public ButtonSpecAny? ToolBarCopyButton
    {
        get => _copyButtonSpec ?? new ButtonSpecAny();
        set { _copyButtonSpec = value; UpdateImage(KryptonManager.CurrentGlobalPaletteMode); }
    }

    /// <summary>Gets the active image.</summary>
    /// <value>The active image.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Image? ActiveImage { get => _activeImage; private set => _activeImage = value; }

    /// <summary>Gets the disabled image.</summary>
    /// <value>The disabled image.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Image? DisabledImage { get => _disabledImage; private set => _disabledImage = value; }

    /// <summary>Gets the normal image.</summary>
    /// <value>The normal image.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Image? NormalImage { get => _normalImage; private set => _normalImage = value; }

    /// <summary>Gets the pressed image.</summary>
    /// <value>The pressed image.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Image? PressedImage { get => _pressedImage; private set => _pressedImage = value; }

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonIntegratedToolbarCopyCommand" /> class.</summary>
    public KryptonIntegratedToolbarCopyCommand()
    {
        _imageStates = new ButtonImageStates();

        Text = KryptonManager.Strings.ToolBarStrings.Copy;
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
        if (_copyButtonSpec != null)
        {
            _copyButtonSpec.ImageStates.ImageDisabled = disabledImage;

            _copyButtonSpec.ImageStates.ImageTracking = activeImage ?? null;

            _copyButtonSpec.ImageStates.ImageNormal = normalImage;

            _copyButtonSpec.ImageStates.ImagePressed = pressedImage ?? null;
        }
    }

    /// <summary>Updates the active image.</summary>
    /// <param name="activeImage">The active image.</param>
    private void UpdateActiveImage(Image activeImage)
    {
        _activeImage = activeImage;

        if (_copyButtonSpec != null)
        {
            _copyButtonSpec.ImageStates.ImageTracking = _activeImage;
        }
    }

    /// <summary>Updates the disabled image.</summary>
    /// <param name="disabledImage">The disabled image.</param>
    private void UpdateDisabledImage(Image? disabledImage)
    {
        _disabledImage = disabledImage;

        if (_copyButtonSpec != null)
        {
            _copyButtonSpec.ImageStates.ImageDisabled = disabledImage;
        }
    }

    /// <summary>Updates the normal image.</summary>
    /// <param name="normalImage">The normal image.</param>
    private void UpdateNormalImage(Image? normalImage)
    {
        _normalImage = normalImage;

        if (_copyButtonSpec != null)
        {
            _copyButtonSpec.ImageStates.ImageNormal = normalImage;
        }
    }

    /// <summary>Updates the pressed image.</summary>
    /// <param name="pressedImage">The pressed image.</param>
    private void UpdatePressedImage(Image? pressedImage)
    {
        _pressedImage = pressedImage;

        if (_copyButtonSpec != null)
        {
            _copyButtonSpec.ImageStates.ImagePressed = pressedImage;
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
                UpdateImage(SystemToolbarImageResources.SystemToolbarCopyNormal);
                break;
            case PaletteMode.ProfessionalOffice2003:
                UpdateImage(Office2003ToolbarImageResources.Office2003ToolbarCopyNormal);
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
                UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarCopyNormal);
                break;
            // TODO: Re-enable this once completed
            //case PaletteMode.Office2010DarkGray:
            case PaletteMode.Office2010Blue:
            case PaletteMode.Office2010BlueDarkMode:
            case PaletteMode.Office2010BlueLightMode:
            case PaletteMode.Office2010Silver:
            case PaletteMode.Office2010SilverDarkMode:
            case PaletteMode.Office2010SilverLightMode:
            case PaletteMode.Office2010White:
            case PaletteMode.Office2010Black:
            case PaletteMode.Office2010BlackDarkMode:
                UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarCopyNormal);
                break;
            // TODO: Re-enable this once completed
            //case PaletteMode.Office2013DarkGray:
            //case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                UpdateImage(Office2013ToolbarImageResources.Office2013ToolbarCopyNormal);
                break;
            // TODO: Re-enable this once completed
            //case PaletteMode.Microsoft365DarkGray:
            case PaletteMode.Microsoft365Black:
            case PaletteMode.Microsoft365BlackDarkMode:
            case PaletteMode.Microsoft365Blue:
            case PaletteMode.Microsoft365BlueDarkMode:
            case PaletteMode.Microsoft365BlueLightMode:
            case PaletteMode.Microsoft365Silver:
            case PaletteMode.Microsoft365SilverDarkMode:
            case PaletteMode.Microsoft365SilverLightMode:
            case PaletteMode.Microsoft365White:
                UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarCopyNormal);
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
                UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarCopyNormal);
                break;
            case PaletteMode.Custom:
                UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarCopyNormal);
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
                UpdateActiveImage(SystemToolbarImageResources.SystemToolbarCopyNormal);
                break;
            case PaletteMode.ProfessionalOffice2003:
                UpdateActiveImage(Office2003ToolbarImageResources.Office2003ToolbarCopyNormal);
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
                UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarCopyNormal);
                break;
            // TODO: Re-enable this once completed
            //case PaletteMode.Office2010DarkGray:
            case PaletteMode.Office2010Blue:
            case PaletteMode.Office2010BlueDarkMode:
            case PaletteMode.Office2010BlueLightMode:
            case PaletteMode.Office2010Silver:
            case PaletteMode.Office2010SilverDarkMode:
            case PaletteMode.Office2010SilverLightMode:
            case PaletteMode.Office2010White:
            case PaletteMode.Office2010Black:
            case PaletteMode.Office2010BlackDarkMode:
                UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarCopyNormal);
                break;
            // TODO: Re-enable this once completed
            //case PaletteMode.Office2013DarkGray:
            //case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                UpdateActiveImage(Office2013ToolbarImageResources.Office2013ToolbarCopyNormal);
                break;
            // TODO: Re-enable this once completed
            //case PaletteMode.Microsoft365DarkGray:
            case PaletteMode.Microsoft365Black:
            case PaletteMode.Microsoft365BlackDarkMode:
            case PaletteMode.Microsoft365Blue:
            case PaletteMode.Microsoft365BlueDarkMode:
            case PaletteMode.Microsoft365BlueLightMode:
            case PaletteMode.Microsoft365Silver:
            case PaletteMode.Microsoft365SilverDarkMode:
            case PaletteMode.Microsoft365SilverLightMode:
            case PaletteMode.Microsoft365White:
                UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarCopyNormal);
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
                UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarCopyNormal);
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
                UpdateActiveImage(SystemToolbarImageResources.SystemToolbarCopyDisabled);
                break;
            case PaletteMode.ProfessionalOffice2003:
                UpdateActiveImage(Office2003ToolbarImageResources.Office2003ToolbarCopyDisabled);
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
                UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarCopyDisabled);
                break;
            // TODO: Re-enable this once completed
            //case PaletteMode.Office2010DarkGray:
            case PaletteMode.Office2010Blue:
            case PaletteMode.Office2010BlueDarkMode:
            case PaletteMode.Office2010BlueLightMode:
            case PaletteMode.Office2010Silver:
            case PaletteMode.Office2010SilverDarkMode:
            case PaletteMode.Office2010SilverLightMode:
            case PaletteMode.Office2010White:
            case PaletteMode.Office2010Black:
            case PaletteMode.Office2010BlackDarkMode:
                UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarCopyDisabled);
                break;
            // TODO: Re-enable this once completed
            //case PaletteMode.Office2013DarkGray:
            //case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                UpdateActiveImage(Office2013ToolbarImageResources.Office2013ToolbarCopyDisabled);
                break;
            // TODO: Re-enable this once completed
            //case PaletteMode.Microsoft365DarkGray:
            case PaletteMode.Microsoft365Black:
            case PaletteMode.Microsoft365BlackDarkMode:
            case PaletteMode.Microsoft365Blue:
            case PaletteMode.Microsoft365BlueDarkMode:
            case PaletteMode.Microsoft365BlueLightMode:
            case PaletteMode.Microsoft365Silver:
            case PaletteMode.Microsoft365SilverDarkMode:
            case PaletteMode.Microsoft365SilverLightMode:
            case PaletteMode.Microsoft365White:
                UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarCopyDisabled);
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
                UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarCopyDisabled);
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
                UpdateActiveImage(SystemToolbarImageResources.SystemToolbarCopyNormal);
                break;
            case PaletteMode.ProfessionalOffice2003:
                UpdateActiveImage(Office2003ToolbarImageResources.Office2003ToolbarCopyNormal);
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
                UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarCopyNormal);
                break;
            // TODO: Re-enable this once completed
            //case PaletteMode.Office2010DarkGray:
            case PaletteMode.Office2010Blue:
            case PaletteMode.Office2010BlueDarkMode:
            case PaletteMode.Office2010BlueLightMode:
            case PaletteMode.Office2010Silver:
            case PaletteMode.Office2010SilverDarkMode:
            case PaletteMode.Office2010SilverLightMode:
            case PaletteMode.Office2010White:
            case PaletteMode.Office2010Black:
            case PaletteMode.Office2010BlackDarkMode:
                UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarCopyNormal);
                break;
            // TODO: Re-enable this once completed
            //case PaletteMode.Office2013DarkGray:
            //case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                UpdateActiveImage(Office2013ToolbarImageResources.Office2013ToolbarCopyNormal);
                break;
            // TODO: Re-enable this once completed
            //case PaletteMode.Microsoft365DarkGray:
            case PaletteMode.Microsoft365Black:
            case PaletteMode.Microsoft365BlackDarkMode:
            case PaletteMode.Microsoft365Blue:
            case PaletteMode.Microsoft365BlueDarkMode:
            case PaletteMode.Microsoft365BlueLightMode:
            case PaletteMode.Microsoft365Silver:
            case PaletteMode.Microsoft365SilverDarkMode:
            case PaletteMode.Microsoft365SilverLightMode:
            case PaletteMode.Microsoft365White:
                UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarCopyNormal);
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
                UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarCopyNormal);
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
                UpdateActiveImage(SystemToolbarImageResources.SystemToolbarCopyNormal);
                break;
            case PaletteMode.ProfessionalOffice2003:
                UpdateActiveImage(Office2003ToolbarImageResources.Office2003ToolbarCopyNormal);
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
                UpdateActiveImage(Office2007ToolbarImageResources.Office2007ToolbarCopyNormal);
                break;
            // TODO: Re-enable this once completed
            //case PaletteMode.Office2010DarkGray:
            case PaletteMode.Office2010Blue:
            case PaletteMode.Office2010BlueDarkMode:
            case PaletteMode.Office2010BlueLightMode:
            case PaletteMode.Office2010Silver:
            case PaletteMode.Office2010SilverDarkMode:
            case PaletteMode.Office2010SilverLightMode:
            case PaletteMode.Office2010White:
            case PaletteMode.Office2010Black:
            case PaletteMode.Office2010BlackDarkMode:
                UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarCopyNormal);
                break;
            // TODO: Re-enable this once completed
            //case PaletteMode.Office2013DarkGray:
            //case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                UpdateActiveImage(Office2013ToolbarImageResources.Office2013ToolbarCopyNormal);
                break;
            // TODO: Re-enable this once completed
            //case PaletteMode.Microsoft365DarkGray:
            case PaletteMode.Microsoft365Black:
            case PaletteMode.Microsoft365BlackDarkMode:
            case PaletteMode.Microsoft365Blue:
            case PaletteMode.Microsoft365BlueDarkMode:
            case PaletteMode.Microsoft365BlueLightMode:
            case PaletteMode.Microsoft365Silver:
            case PaletteMode.Microsoft365SilverDarkMode:
            case PaletteMode.Microsoft365SilverLightMode:
            case PaletteMode.Microsoft365White:
                UpdateActiveImage(Office2019ToolbarImageResources.Office2019ToolbarCopyNormal);
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
                UpdateActiveImage(Office2010ToolbarImageResources.Office2010ToolbarCopyNormal);
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
        if (_copyButtonSpec != null)
        {
            switch (mode)
            {
                case PaletteMode.Global:
                    break;
                case PaletteMode.ProfessionalSystem:
                    AddImageStates(null, SystemToolbarImageResources.SystemToolbarCopyDisabled, SystemToolbarImageResources.SystemToolbarCopyNormal, null);
                    break;
                case PaletteMode.ProfessionalOffice2003:
                    AddImageStates(null, Office2003ToolbarImageResources.Office2003ToolbarCopyDisabled, Office2003ToolbarImageResources.Office2003ToolbarCopyNormal, null);
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
                    AddImageStates(Office2007ToolbarImageResources.Office2007ToolbarCopyNormal, Office2007ToolbarImageResources.Office2007ToolbarCopyDisabled, Office2007ToolbarImageResources.Office2007ToolbarCopyNormal, Office2007ToolbarImageResources.Office2007ToolbarCopyNormal);
                    break;
                // TODO: Re-enable this once completed
                //case PaletteMode.Office2010DarkGray:
                case PaletteMode.Office2010Blue:
                case PaletteMode.Office2010BlueDarkMode:
                case PaletteMode.Office2010BlueLightMode:
                case PaletteMode.Office2010Silver:
                case PaletteMode.Office2010SilverDarkMode:
                case PaletteMode.Office2010SilverLightMode:
                case PaletteMode.Office2010White:
                case PaletteMode.Office2010Black:
                case PaletteMode.Office2010BlackDarkMode:
                    AddImageStates(Office2010ToolbarImageResources.Office2010ToolbarCopyNormal, Office2010ToolbarImageResources.Office2010ToolbarCopyDisabled, Office2010ToolbarImageResources.Office2010ToolbarCopyNormal, Office2010ToolbarImageResources.Office2010ToolbarCopyNormal);
                    break;
                // TODO: Re-enable this once completed
                //case PaletteMode.Office2013DarkGray:
                //case PaletteMode.Office2013LightGray:
                case PaletteMode.Office2013White:
                    AddImageStates(Office2019ToolbarImageResources.Office2019ToolbarCopyNormal, Office2019ToolbarImageResources.Office2019ToolbarCopyDisabled, Office2019ToolbarImageResources.Office2019ToolbarCopyNormal, Office2019ToolbarImageResources.Office2019ToolbarCopyNormal);
                    break;
                // TODO: Re-enable this once completed
                //case PaletteMode.Microsoft365DarkGray:
                case PaletteMode.Microsoft365Black:
                case PaletteMode.Microsoft365BlackDarkMode:
                case PaletteMode.Microsoft365Blue:
                case PaletteMode.Microsoft365BlueDarkMode:
                case PaletteMode.Microsoft365BlueLightMode:
                case PaletteMode.Microsoft365Silver:
                case PaletteMode.Microsoft365SilverDarkMode:
                case PaletteMode.Microsoft365SilverLightMode:
                case PaletteMode.Microsoft365White:
                    AddImageStates(Office2019ToolbarImageResources.Office2019ToolbarCopyNormal, Office2019ToolbarImageResources.Office2019ToolbarCopyDisabled, Office2019ToolbarImageResources.Office2019ToolbarCopyNormal, Office2019ToolbarImageResources.Office2019ToolbarCopyNormal);
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
                    AddImageStates(Office2010ToolbarImageResources.Office2010ToolbarCopyNormal, Office2010ToolbarImageResources.Office2010ToolbarCopyDisabled, Office2010ToolbarImageResources.Office2010ToolbarCopyNormal, Office2010ToolbarImageResources.Office2010ToolbarCopyNormal);
                    break;
                case PaletteMode.Custom:
                    AddImageStates(Office2019ToolbarImageResources.Office2019ToolbarCopyNormal, Office2019ToolbarImageResources.Office2019ToolbarCopyDisabled, Office2019ToolbarImageResources.Office2019ToolbarCopyNormal, Office2019ToolbarImageResources.Office2019ToolbarCopyNormal);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }

    #endregion
}