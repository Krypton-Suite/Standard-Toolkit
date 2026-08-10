#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2023 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Converters;

/// <summary>
/// Custom type converter so that PaletteBase Class type are converted to their appropriate mode type
/// </summary>
internal class PaletteClassTypeConverter : EnumConverter
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<PaletteMode, Type> _pairs = new BiDictionary<PaletteMode, Type>
    (new Dictionary<PaletteMode, Type>
        {
            {PaletteMode.ProfessionalSystem, typeof(PaletteProfessionalSystem)},
            {PaletteMode.ProfessionalOffice2003, typeof(PaletteProfessionalOffice2003)},
            {PaletteMode.Office2007Blue, typeof(PaletteOffice2007Blue)},
            //{PaletteMode.Office2007DarkGray, typeof(PaletteOffice2007DarkGray)},
            {PaletteMode.Office2007BlueDarkMode, typeof(PaletteOffice2007BlueDarkMode)},
            {PaletteMode.Office2007BlueLightMode, typeof(PaletteOffice2007BlueLightMode)},
            {PaletteMode.Office2007Silver, typeof(PaletteOffice2007Silver)},
            {PaletteMode.Office2007SilverDarkMode, typeof(PaletteOffice2007SilverDarkMode)},
            {PaletteMode.Office2007SilverLightMode, typeof(PaletteOffice2007SilverLightMode)},
            {PaletteMode.Office2007White, typeof(PaletteOffice2007White)},
            {PaletteMode.Office2007Black, typeof(PaletteOffice2007Black)},
            //{PaletteMode.Office2010DarkGray, typeof(PaletteOffice2010DarkGray)},
            {PaletteMode.Office2007BlackDarkMode, typeof(PaletteOffice2007BlackDarkMode)},
            {PaletteMode.Office2010Blue, typeof(PaletteOffice2010Blue)},
            {PaletteMode.Office2010BlueDarkMode, typeof(PaletteOffice2010BlueDarkMode)},
            {PaletteMode.Office2010BlueLightMode, typeof(PaletteOffice2010BlueLightMode)},
            {PaletteMode.Office2010Silver, typeof(PaletteOffice2010Silver)},
            {PaletteMode.Office2010SilverDarkMode, typeof(PaletteOffice2010SilverDarkMode)},
            {PaletteMode.Office2010SilverLightMode, typeof(PaletteOffice2010SilverLightMode)},
            {PaletteMode.Office2010White, typeof(PaletteOffice2010White)},
            {PaletteMode.Office2010Black, typeof(PaletteOffice2010Black)},
            {PaletteMode.Office2010BlackDarkMode, typeof(PaletteOffice2010BlackDarkMode)},
            {PaletteMode.Office2013DarkGray, typeof(PaletteOffice2013DarkGray)},
            {PaletteMode.Office2013LightGray, typeof(PaletteOffice2013LightGray)},
            {PaletteMode.Office2013White, typeof(PaletteOffice2013White)},
            {PaletteMode.SparkleBlue, typeof(PaletteSparkleBlue)},
            {PaletteMode.SparkleBlueDarkMode, typeof(PaletteSparkleBlueDarkMode)},
            {PaletteMode.SparkleBlueLightMode, typeof(PaletteSparkleBlueLightMode)},
            {PaletteMode.SparkleOrange, typeof(PaletteSparkleOrange)},
            {PaletteMode.SparkleOrangeDarkMode, typeof(PaletteSparkleOrangeDarkMode)},
            {PaletteMode.SparkleOrangeLightMode, typeof(PaletteSparkleOrangeLightMode)},
            {PaletteMode.SparklePurple, typeof(PaletteSparklePurple)},
            {PaletteMode.SparklePurpleDarkMode, typeof(PaletteSparklePurpleDarkMode)},
            {PaletteMode.SparklePurpleLightMode, typeof(PaletteSparklePurpleLightMode)},
            {PaletteMode.Microsoft365Black, typeof(PaletteMicrosoft365Black)},
            {PaletteMode.Microsoft365BlackDarkMode, typeof(PaletteMicrosoft365BlackDarkMode)},
            {PaletteMode.Microsoft365BlackDarkModeAlternate, typeof(PaletteMicrosoft365BlackDarkModeAlternate)},
            {PaletteMode.Microsoft365BlueDarkMode, typeof(PaletteMicrosoft365BlueDarkMode)},
            {PaletteMode.Microsoft365BlueLightMode, typeof(PaletteMicrosoft365BlueLightMode)},
            {PaletteMode.Microsoft365Blue, typeof(PaletteMicrosoft365Blue)},
            //{PaletteMode.Microsoft365DarkGray, typeof(PaletteMicrosoft365DarkGray)},
            {PaletteMode.Microsoft365Silver, typeof(PaletteMicrosoft365Silver)},
            {PaletteMode.Microsoft365SilverDarkMode, typeof(PaletteMicrosoft365SilverDarkMode)},
            {PaletteMode.Microsoft365SilverLightMode, typeof(PaletteMicrosoft365SilverLightMode)},
            {PaletteMode.Microsoft365White, typeof(PaletteMicrosoft365White)},
            {PaletteMode.VisualStudio2010Render2007, typeof(PaletteVisualStudio2010Office2007Variation)},
            {PaletteMode.VisualStudio2010Render2010, typeof(PaletteVisualStudio2010Office2010Variation)},
            {PaletteMode.VisualStudio2010Render2013, typeof(PaletteVisualStudio2010Office2013Variation)},
            {PaletteMode.VisualStudio2010Render365, typeof(PaletteVisualStudio2010Microsoft365Variation)},
            {PaletteMode.VisualStudio2012Dark, typeof(PaletteVisualStudio2012Dark)},
            {PaletteMode.VisualStudio2012Light, typeof(PaletteVisualStudio2012Light)},
            {PaletteMode.VisualStudio2012Blue, typeof(PaletteVisualStudio2012Blue)},
            {PaletteMode.VisualStudio2013Dark, typeof(PaletteVisualStudio2013Dark)},
            {PaletteMode.VisualStudio2013Light, typeof(PaletteVisualStudio2013Light)},
            {PaletteMode.VisualStudio2013Blue, typeof(PaletteVisualStudio2013Blue)},
            {PaletteMode.VisualStudio2015Dark, typeof(PaletteVisualStudio2015Dark)},
            {PaletteMode.VisualStudio2015Light, typeof(PaletteVisualStudio2015Light)},
            {PaletteMode.VisualStudio2015Blue, typeof(PaletteVisualStudio2015Blue)},
            {PaletteMode.VisualStudio2017Dark, typeof(PaletteVisualStudio2017Dark)},
            {PaletteMode.VisualStudio2017Light, typeof(PaletteVisualStudio2017Light)},
            {PaletteMode.VisualStudio2017Blue, typeof(PaletteVisualStudio2017Blue)},
            {PaletteMode.VisualStudio2019Dark, typeof(PaletteVisualStudio2019Dark)},
            {PaletteMode.VisualStudio2019Light, typeof(PaletteVisualStudio2019Light)},
            {PaletteMode.VisualStudio2019Blue, typeof(PaletteVisualStudio2019Blue)},
            {PaletteMode.VisualStudio2022Dark, typeof(PaletteVisualStudio2022Dark)},
            {PaletteMode.VisualStudio2022Light, typeof(PaletteVisualStudio2022Light)},
            {PaletteMode.VisualStudio2022Blue, typeof(PaletteVisualStudio2022Blue)},
            {PaletteMode.VisualStudio2026Dark, typeof(PaletteVisualStudio2026Dark)},
            {PaletteMode.VisualStudio2026Light, typeof(PaletteVisualStudio2026Light)},
            {PaletteMode.MaterialLight, typeof(PaletteMaterialLight)},
            {PaletteMode.MaterialDark, typeof(PaletteMaterialDark)},
            {PaletteMode.MaterialLightRipple, typeof(PaletteMaterialLightRipple)},
            {PaletteMode.MaterialDarkRipple, typeof(PaletteMaterialDarkRipple)},
            {PaletteMode.MaterialLimeGreen, typeof(PaletteMaterialLimeGreen)},
            {PaletteMode.MaterialLimeGreenDark, typeof(PaletteMaterialLimeGreenDark)},
            {PaletteMode.MaterialLimeGreenRipple, typeof(PaletteMaterialLimeGreenRipple)},
            {PaletteMode.MaterialLimeGreenDarkRipple, typeof(PaletteMaterialLimeGreenDarkRipple)},
            {PaletteMode.RetroGreen, typeof(PaletteRetroGreen)},
            {PaletteMode.RetroBlue, typeof(PaletteRetroBlue)},
            {PaletteMode.MacOSLight, typeof(PaletteMacOSLight)},
            {PaletteMode.MacOSDark, typeof(PaletteMacOSDark)},
            {PaletteMode.HighContrast, typeof(PaletteHighContrast)},
            {PaletteMode.Deuteranopia, typeof(PaletteDeuteranopia)},
            {PaletteMode.Protanopia, typeof(PaletteProtanopia)},
            {PaletteMode.Office2007HighContrast, typeof(PaletteOffice2007HighContrast)},
            {PaletteMode.Office2007Deuteranopia, typeof(PaletteOffice2007Deuteranopia)},
            {PaletteMode.Office2007Protanopia, typeof(PaletteOffice2007Protanopia)},
            {PaletteMode.Office2010HighContrast, typeof(PaletteOffice2010HighContrast)},
            {PaletteMode.Office2010Deuteranopia, typeof(PaletteOffice2010Deuteranopia)},
            {PaletteMode.Office2010Protanopia, typeof(PaletteOffice2010Protanopia)},
            {PaletteMode.Office2013HighContrast, typeof(PaletteOffice2013HighContrast)},
            {PaletteMode.Office2013Deuteranopia, typeof(PaletteOffice2013Deuteranopia)},
            {PaletteMode.Office2013Protanopia, typeof(PaletteOffice2013Protanopia)},
            {PaletteMode.SparkleHighContrast, typeof(PaletteSparkleHighContrast)},
            {PaletteMode.SparkleDeuteranopia, typeof(PaletteSparkleDeuteranopia)},
            {PaletteMode.SparkleProtanopia, typeof(PaletteSparkleProtanopia)},
            {PaletteMode.MaterialHighContrast, typeof(PaletteMaterialHighContrast)},
            {PaletteMode.MaterialDeuteranopia, typeof(PaletteMaterialDeuteranopia)},
            {PaletteMode.MaterialProtanopia, typeof(PaletteMaterialProtanopia)},
            {PaletteMode.MaterialHighContrastRipple, typeof(PaletteMaterialHighContrastRipple)},
            {PaletteMode.MaterialDeuteranopiaRipple, typeof(PaletteMaterialDeuteranopiaRipple)},
            {PaletteMode.MaterialProtanopiaRipple, typeof(PaletteMaterialProtanopiaRipple)},
            {PaletteMode.Office2007LimeGreen, typeof(PaletteOffice2007LimeGreen)},
            {PaletteMode.Office2007LimeGreenDark, typeof(PaletteOffice2007LimeGreenDark)},
            {PaletteMode.Office2010LimeGreen, typeof(PaletteOffice2010LimeGreen)},
            {PaletteMode.Office2010LimeGreenDark, typeof(PaletteOffice2010LimeGreenDark)},
            {PaletteMode.Microsoft365LimeGreen, typeof(PaletteMicrosoft365LimeGreen)},
            {PaletteMode.Microsoft365LimeGreenDark, typeof(PaletteMicrosoft365LimeGreenDark)}
            //{PaletteMode.Custom, typeof(KryptonCustomPaletteBase)}
        });

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteClassTypeConverter class.
    /// </summary>
    public PaletteClassTypeConverter()
        : base(typeof(PaletteMode))
    {
    }
    #endregion

    #region Public
    /// <summary>
    /// Converts the given value object to the specified type, using the specified context and culture information.
    /// </summary>
    /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
    /// <param name="culture">A CultureInfo object. If a null reference the current culture is assumed.</param>
    /// <param name="value">The Object to convert.</param>
    /// <param name="destinationType">The Type to convert the value parameter to.</param>
    /// <returns>An Object that represents the converted value.</returns>
    public override object? ConvertTo(ITypeDescriptorContext? context,
                                     CultureInfo? culture,
                                     object? value,
                                     Type destinationType)
    {
        if (value is PaletteMode val)
        {
            // Search for a matching value
            if (_pairs.FirstToSecond.TryGetValue(val, out var classType))
            {
                return classType;
            }
        }

        // Let base class perform default conversion
        return base.ConvertTo(context, culture, value, destinationType);
    }

    /// <summary>
    /// Converts the given object to the type of this converter, using the specified context and culture information.
    /// </summary>
    /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
    /// <param name="culture">The CultureInfo to use as the current culture.</param>
    /// <param name="value">The Object to convert.</param>
    /// <returns>An Object that represents the converted value.</returns>
    public override object? ConvertFrom(ITypeDescriptorContext? context,
        CultureInfo? culture,
        object? value)
    {
        if (value is Type val)
        {
            // Search for a matching Class
            if( _pairs.SecondToFirst.TryGetValue(val, out var mode))
            {
                return mode;
            }
        }

        // Let base class perform default conversion
        return base.ConvertFrom(context!, culture!, value!);
    }
    #endregion
}