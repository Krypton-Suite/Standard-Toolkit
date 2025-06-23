#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
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
    /*
     * Adjustements made as per ticket 1328.
     * See: https://github.com/Krypton-Suite/Standard-Toolkit/issues/1328
     *
     * These entries (also those disabled) are now sorted in the order of the SupportedThemes Dictionary.
     *
     * By starting the enum at Global = -1 all following entries will have numbers assigned that are equivalent to
     * their respective Theme Dictionary position (PaletteModeStrings.SupportedThemes)
     *
     * IT IS MANDATORY TO KEEP THE PALETTEMODE ENUM AND THE DICTIONARY IN THE SAME ORDER.
     */

    /// <summary>
    /// Specifies the renderer defined by the KryptonManager be used.
    /// </summary>
    Global = -1,

    /// <summary>
    /// Specifies a professional appearance based on system settings.
    /// </summary>
    ProfessionalSystem,

    /// <summary>
    /// Specifies a professional appearance with a preference to use theme colors.
    /// </summary>
    ProfessionalOffice2003,

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

    /* ToDo: Re-enable when the gray themes are completed
    /// <summary>
    /// Specifies the dark Gray color variant of the Office 2007 appearance.
    /// </summary>
    Office2007DarkGray,
    */

    /* ToDo: Re-enable when the gray themes are completed
    /// <summary>
    /// Specifies the light Gray color variant of the Office 2007 appearance.
    /// </summary>
    Office2007LightGray,
    */

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

    /* ToDo: Re-enable when the gray themes are completed
    /// <summary>
    /// Specifies the dark Gray color variant of the Office 2010 appearance.
    /// </summary>
    Office2010DarkGray,
    */

    /* ToDo: Re-enable when the gray themes are completed
    /// <summary>
    /// Specifies the light Gray color variant of the Office 2010 appearance.
    /// </summary>
    Office2010LightGray,
    */

    /* ToDo: Re-enable when the gray themes are completed
    /// <summary>
    /// Specifies the dark Gray color variant of the Office 2013 appearance.
    /// </summary>
    Office2013DarkGray,
    */

    /* ToDo: Re-enable when the gray themes are completed
    /// <summary>
    /// Specifies the light Gray color variant of the Office 2013 appearance.
    /// </summary>
    Office2013LightGray,
    */

    /// <summary>
    /// Specifies the White color variant of the Office 2013 appearance.
    /// </summary>
    Office2013White,

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
    /// Specifies the Black color variant of the Microsoft 365 appearance.
    /// </summary>
    Microsoft365Black,

    /// <summary>
    /// Specifies the dark Black color variant of the Microsoft 365 appearance.
    /// </summary>
    Microsoft365BlackDarkMode,

    /// <summary>
    /// Specifies the alternate dark Black color variant of the Microsoft 365 appearance.
    /// </summary>
    Microsoft365BlackDarkModeAlternate,

    /* ToDo: Re-enable when the gray themes are completed
    /// <summary>
    /// Specifies the dark Gray color variant of the Microsoft 365 appearance.
    /// </summary>
    Microsoft365DarkGray,
    */

    /* ToDo: Re-enable when the gray themes are completed
    /// <summary>
    /// Specifies the light Gray color variant of the Microsoft 365 appearance.
    /// </summary>
    Microsoft365LightGray,
    */

    /// <summary>
    /// Specifies the visual studio 2010 palette theme, with the 2007 render.
    /// </summary>
    VisualStudio2010Render2007,

    /// <summary>
    /// Specifies the visual studio 2010 palette theme, with the 2010 render.
    /// </summary>
    VisualStudio2010Render2010,

    /// <summary>
    /// Specifies the visual studio 2010 palette theme, with the 2013 render.
    /// </summary>
    VisualStudio2010Render2013,

    /// <summary>
    /// Specifies the visual studio 2010 palette theme, with the Microsoft 365 render.
    /// </summary>
    VisualStudio2010Render365,

    ///// <summary>
    ///// Specifies the visual studio 2012 dark palette theme.
    ///// </summary>
    //VisualStudio2012Dark,

    ///// <summary>
    ///// Specifies the visual studio 2012 light palette theme.
    ///// </summary>
    //VisualStudio2012Light,

    ///// <summary>
    ///// Specifies the visual studio 2013 dark palette theme.
    ///// </summary>
    //VisualStudio2013Dark,

    ///// <summary>
    ///// Specifies the visual studio 2013 light palette theme.
    ///// </summary>
    //VisualStudio2013Light,

    ///// <summary>
    ///// Specifies the visual studio 2015 dark palette theme.
    ///// </summary>
    //VisualStudio2015Dark,

    ///// <summary>
    ///// Specifies the visual studio 2015 light palette theme.
    ///// </summary>
    //VisualStudio2015Light,

    ///// <summary>
    ///// Specifies the visual studio 2017 dark palette theme.
    ///// </summary>
    //VisualStudio2017Dark,

    ///// <summary>
    ///// Specifies the visual studio 2017 light palette theme.
    ///// </summary>
    //VisualStudio2017Light,

    ///// <summary>
    ///// Specifies the visual studio 2019 dark palette theme.
    ///// </summary>
    //VisualStudio2019Dark,

    ///// <summary>
    ///// Specifies the visual studio 2019 light palette theme.
    ///// </summary>
    //VisualStudio2019Light,

    ///// <summary>
    ///// Specifies the visual studio 2022 dark palette theme.
    ///// </summary>
    //VisualStudio2022Dark,

    ///// <summary>
    ///// Specifies the visual studio 2022 light palette theme.
    ///// </summary>
    //VisualStudio2022Light,

    /// <summary>
    /// Specifies a custom palette be used.
    /// </summary>
    Custom
}