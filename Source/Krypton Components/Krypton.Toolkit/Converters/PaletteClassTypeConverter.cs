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