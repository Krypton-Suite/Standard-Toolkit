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
            // Core palettes only; extra modes resolve via KryptonThemeCatalog after discovery.
            {PaletteMode.ProfessionalSystem, typeof(PaletteProfessionalSystem)},
            {PaletteMode.ProfessionalOffice2003, typeof(PaletteProfessionalOffice2003)},
            {PaletteMode.Office2007Blue, typeof(PaletteOffice2007Blue)},
            {PaletteMode.Office2007Silver, typeof(PaletteOffice2007Silver)},
            {PaletteMode.Office2007Black, typeof(PaletteOffice2007Black)},
            {PaletteMode.Office2010Blue, typeof(PaletteOffice2010Blue)},
            {PaletteMode.Office2010Silver, typeof(PaletteOffice2010Silver)},
            {PaletteMode.Office2010Black, typeof(PaletteOffice2010Black)},
            {PaletteMode.Microsoft365Blue, typeof(PaletteMicrosoft365Blue)},
            {PaletteMode.Microsoft365Silver, typeof(PaletteMicrosoft365Silver)},
            {PaletteMode.Microsoft365SilverDarkMode, typeof(PaletteMicrosoft365SilverDarkMode)},
            {PaletteMode.Microsoft365SilverLightMode, typeof(PaletteMicrosoft365SilverLightMode)},
            {PaletteMode.Microsoft365White, typeof(PaletteMicrosoft365White)},
            {PaletteMode.VisualStudio2010Render2007, typeof(PaletteVisualStudio2010Office2007Variation)},
            {PaletteMode.VisualStudio2010Render2010, typeof(PaletteVisualStudio2010Office2010Variation)},
            {PaletteMode.VisualStudio2010Render2013, typeof(PaletteVisualStudio2010Office2013Variation)},
            {PaletteMode.VisualStudio2010Render365, typeof(PaletteVisualStudio2010Microsoft365Variation)},
            {PaletteMode.VisualStudio2022Dark, typeof(PaletteVisualStudio2022Dark)},
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
            {PaletteMode.WindowsXPLunaBlue, typeof(PaletteWindowsXPLunaBlue)},
            {PaletteMode.WindowsXPLunaOlive, typeof(PaletteWindowsXPLunaOlive)},
            {PaletteMode.WindowsXPLunaSilver, typeof(PaletteWindowsXPLunaSilver)},
            {PaletteMode.WindowsXPRoyale, typeof(PaletteWindowsXPRoyale)},
            {PaletteMode.WindowsXPRoyaleNoir, typeof(PaletteWindowsXPRoyaleNoir)},
            {PaletteMode.WindowsXPZune, typeof(PaletteWindowsXPZune)},
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
            {PaletteMode.Microsoft365Black, typeof(PaletteMicrosoft365Black)},
            {PaletteMode.SparkleBlue, typeof(PaletteSparkleBlue)},
            {PaletteMode.SparkleOrange, typeof(PaletteSparkleOrange)},
            {PaletteMode.SparklePurple, typeof(PaletteSparklePurple)}
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

            KryptonThemeCatalog.EnsureReady();
            // Extra palettes: identity is the mode; consumers should use GetPaletteForMode.
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

            if (KryptonThemeCatalog.TryGetMode(val, out var catalogMode))
            {
                return catalogMode;
            }
        }

        // Let base class perform default conversion
        return base.ConvertFrom(context!, culture!, value!);
    }
    #endregion
}