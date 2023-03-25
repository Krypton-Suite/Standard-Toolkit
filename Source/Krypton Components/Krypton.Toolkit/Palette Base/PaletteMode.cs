#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Specifies the palette requested at the global level.
/// </summary>
[TypeConverter(typeof(PaletteModeConverter))]
public enum PaletteMode
{
    /// <summary>
    /// Specifies the renderer defined by the KryptonManager be used.
    /// </summary>
    Global,
    
    /// <summary>
    /// Specifies a professional appearance based on system settings.
    /// </summary>
    ProfessionalSystem,

    /// <summary>
    /// Specifies a professional appearance with a preference to use theme colors.
    /// </summary>
    ProfessionalOffice2003,

    // Note: Re-enable when the gray themes are completed
    /// <summary>
    /// Specifies the dark Gray color variant of the Office 2007 appearance.
    /// </summary>
    Office2007DarkGray,
    /*
		/// <summary>
		/// Specifies the light Gray color variant of the Office 2007 appearance.
		/// </summary>
		Office2007LightGray,*/

    /// <summary>
    /// Specifies the Blue color variant of the Office 2007 appearance.
    /// </summary>
    Office2007Blue,

    /// <summary>
    /// Specifies the dark Blue color variant of the Office 2007 appearance.
    /// </summary>
    Office2007BlueDarkMode,

    /// <summary>
    /// Specifies the light Blue color variant of the Office 2007 appearance.
    /// </summary>
    Office2007BlueLightMode,

    /// <summary>
    /// Specifies the Silver color variant of the Office 2007 appearance.
    /// </summary>
    Office2007Silver,

    /// <summary>
    /// Specifies the dark Silver color variant of the Office 2007 appearance.
    /// </summary>
    Office2007SilverDarkMode,

    /// <summary>
    /// Specifies the light Silver color variant of the Office 2007 appearance.
    /// </summary>
    Office2007SilverLightMode,

    /// <summary>
    /// Specifies the White color variant of the Office 2007 appearance.
    /// </summary>
    Office2007White,

    /// <summary>
    /// Specifies the Black color variant of the Office 2007 appearance.
    /// </summary>
    Office2007Black,

    /// <summary>
    /// Specifies the dark Black color variant of the Office 2007 appearance.
    /// </summary>
    Office2007BlackDarkMode,

    // Note: Re-enable when the gray themes are completed
    /// <summary>
    /// Specifies the dark Gray color variant of the Office 2010 appearance.
    /// </summary>
    Office2010DarkGray,
    /*
		/// <summary>
		/// Specifies the light Gray color variant of the Office 2010 appearance.
		/// </summary>
		Office2010LightGray,*/

    /// <summary>
    /// Specifies the Blue color variant of the Office 2010 appearance.
    /// </summary>
    Office2010Blue,

    /// <summary>
    /// Specifies the dark Blue color variant of the Office 2010 appearance.
    /// </summary>
    Office2010BlueDarkMode,

    /// <summary>
    /// Specifies the light Blue color variant of the Office 2010 appearance.
    /// </summary>
    Office2010BlueLightMode,
    
    /// <summary>
    /// Specifies the Silver color variant of the Office 2010 appearance.
    /// </summary>
    Office2010Silver,

    /// <summary>
    /// Specifies the dark Silver color variant of the Office 2010 appearance.
    /// </summary>
    Office2010SilverDarkMode,

    /// <summary>
    /// Specifies the light Silver color variant of the Office 2010 appearance.
    /// </summary>
    Office2010SilverLightMode,

    /// <summary>
    /// Specifies the White color variant of the Office 2010 appearance.
    /// </summary>
    Office2010White,

    /// <summary>
    /// Specifies the Black color variant of the Office 2010 appearance.
    /// </summary>
    Office2010Black,

    /// <summary>
    /// Specifies the dark Black color variant of the Office 2010 appearance.
    /// </summary>
    Office2010BlackDarkMode,
    
    // Note: Re-enable when the gray themes are completed
    /// <summary>
    /// Specifies the dark Gray color variant of the Office 2013 appearance.
    /// </summary>
    Office2013DarkGray,
    /*
		/// <summary>
		/// Specifies the light Gray color variant of the Office 2013 appearance.
		/// </summary>
		Office2013LightGray,*/

    /// <summary>
    /// Specifies the White color variant of the Office 2013 appearance.
    /// </summary>
    Office2013White,

    // Note: Re-enable when the gray themes are completed
    /// <summary>
    /// Specifies the dark Gray color variant of the Microsoft 365 appearance.
    /// </summary>
    Microsoft365DarkGray,
    /*
		/// <summary>
		/// Specifies the light Gray color variant of the Microsoft 365 appearance.
		/// </summary>
		Microsoft365LightGray,*/

    /// <summary>
    /// Specifies the Black color variant of the Microsoft 365 appearance.
    /// </summary>
    Microsoft365Black,

    /// <summary>
    /// Specifies the dark Black color variant of the Microsoft 365 appearance.
    /// </summary>
    Microsoft365BlackDarkMode,

    /// <summary>
    /// Specifies the Blue color variant of the Microsoft 365 appearance.
    /// </summary>
    Microsoft365Blue,

    /// <summary>
    /// Specifies the dark Blue color variant of the Microsoft 365 appearance.
    /// </summary>
    Microsoft365BlueDarkMode,

    /// <summary>
    /// Specifies the light Blue color variant of the Microsoft 365 appearance.
    /// </summary>
    Microsoft365BlueLightMode,
    
    /// <summary>
    /// Specifies the Silver color variant of the Microsoft 365 appearance.
    /// </summary>
    Microsoft365Silver,

    /// <summary>
    /// Specifies the dark Silver color variant of the Microsoft 365 appearance.
    /// </summary>
    Microsoft365SilverDarkMode,

    /// <summary>
    /// Specifies the light Silver color variant of the Microsoft 365 appearance.
    /// </summary>
    Microsoft365SilverLightMode,

    /// <summary>
    /// Specifies the White color variant of the Microsoft 365 appearance.
    /// </summary>
    Microsoft365White,

    /// <summary>
    /// Specifies the Blue color variant on the Sparkle palette theme.
    /// </summary>
    SparkleBlue,

    /// <summary>
    /// Specifies the dark Blue color variant on the Sparkle palette theme.
    /// </summary>
    SparkleBlueDarkMode,

    /// <summary>
    /// Specifies the light Blue color variant on the Sparkle palette theme.
    /// </summary>
    SparkleBlueLightMode,

    /// <summary>
    /// Specifies the Orange color variant on the Sparkle palette theme.
    /// </summary>
    SparkleOrange,

    /// <summary>
    /// Specifies the dark Orange color variant on the Sparkle palette theme.
    /// </summary>
    SparkleOrangeDarkMode,

    /// <summary>
    /// Specifies the light Orange color variant on the Sparkle palette theme.
    /// </summary>
    SparkleOrangeLightMode,

    /// <summary>
    /// Specifies the Purple color variant on the Sparkle palette theme.
    /// </summary>
    SparklePurple,

    /// <summary>
    /// Specifies the dark Purple color variant on the Sparkle palette theme.
    /// </summary>
    SparklePurpleDarkMode,

    /// <summary>
    /// Specifies the light Purple color variant on the Sparkle palette theme.
    /// </summary>
    SparklePurpleLightMode,

    ///// <summary>
    ///// Specifies the visual studio dark palette theme.
    ///// </summary>
    //VisualStudioDark,

    ///// <summary>
    ///// Specifies the visual studio light palette theme.
    ///// </summary>
    //VisualStudioLight,

    /// <summary>
    /// Specifies a custom palette be used.
    /// </summary>
    Custom
}