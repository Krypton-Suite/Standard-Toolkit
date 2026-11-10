#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Registers Professional, Sparkle Blue/Orange/Purple, and Office 2007/2010/Microsoft 365 Blue, Silver, and Black palettes.
/// </summary>
internal sealed class KryptonCoreThemeProvider : IKryptonThemeProvider
{
    /// <inheritdoc />
    public IReadOnlyList<KryptonThemeDescriptor> GetThemes() =>
        new[]
        {
            Core(PaletteMode.ProfessionalSystem, KryptonThemeFamilies.Professional, KryptonThemeChromeKind.ProfessionalSystem, typeof(PaletteProfessionalSystem), () => KryptonManager.PaletteProfessionalSystem),
            Core(PaletteMode.ProfessionalOffice2003, KryptonThemeFamilies.Professional, KryptonThemeChromeKind.ProfessionalOffice2003, typeof(PaletteProfessionalOffice2003), () => KryptonManager.PaletteProfessionalOffice2003),
            Core(PaletteMode.Office2007Blue, KryptonThemeFamilies.Office2007, KryptonThemeChromeKind.Office2007, typeof(PaletteOffice2007Blue), () => KryptonManager.PaletteOffice2007Blue),
            Core(PaletteMode.Office2007Silver, KryptonThemeFamilies.Office2007, KryptonThemeChromeKind.Office2007, typeof(PaletteOffice2007Silver), () => KryptonManager.PaletteOffice2007Silver),
            Core(PaletteMode.Office2007Black, KryptonThemeFamilies.Office2007, KryptonThemeChromeKind.Office2007, typeof(PaletteOffice2007Black), () => KryptonManager.PaletteOffice2007Black),
            Core(PaletteMode.Office2010Blue, KryptonThemeFamilies.Office2010, KryptonThemeChromeKind.Office2010, typeof(PaletteOffice2010Blue), () => KryptonManager.PaletteOffice2010Blue),
            Core(PaletteMode.Office2010Silver, KryptonThemeFamilies.Office2010, KryptonThemeChromeKind.Office2010, typeof(PaletteOffice2010Silver), () => KryptonManager.PaletteOffice2010Silver),
            Core(PaletteMode.Office2010Black, KryptonThemeFamilies.Office2010, KryptonThemeChromeKind.Office2010, typeof(PaletteOffice2010Black), () => KryptonManager.PaletteOffice2010Black),
            Core(PaletteMode.Microsoft365Blue, KryptonThemeFamilies.Microsoft365, KryptonThemeChromeKind.Microsoft365, typeof(PaletteMicrosoft365Blue), () => KryptonManager.PaletteMicrosoft365Blue),
            Core(PaletteMode.Microsoft365Silver, KryptonThemeFamilies.Microsoft365, KryptonThemeChromeKind.Microsoft365, typeof(PaletteMicrosoft365Silver), () => KryptonManager.PaletteMicrosoft365Silver),
            Core(PaletteMode.Microsoft365Black, KryptonThemeFamilies.Microsoft365, KryptonThemeChromeKind.Microsoft365, typeof(PaletteMicrosoft365Black), () => KryptonManager.PaletteMicrosoft365Black),
            Core(PaletteMode.SparkleBlue, KryptonThemeFamilies.Sparkle, KryptonThemeChromeKind.Sparkle, typeof(PaletteSparkleBlue), () => KryptonManager.PaletteSparkleBlue),
            Core(PaletteMode.SparkleOrange, KryptonThemeFamilies.Sparkle, KryptonThemeChromeKind.Sparkle, typeof(PaletteSparkleOrange), () => KryptonManager.PaletteSparkleOrange),
            Core(PaletteMode.SparklePurple, KryptonThemeFamilies.Sparkle, KryptonThemeChromeKind.Sparkle, typeof(PaletteSparklePurple), () => KryptonManager.PaletteSparklePurple)
        };

    private static KryptonThemeDescriptor Core(PaletteMode mode, string family, KryptonThemeChromeKind chrome, Type type, Func<PaletteBase> factory) =>
        new KryptonThemeDescriptor(mode, family, chrome, KryptonThemeChrome.DefaultShieldIconStyle(chrome), true, type, factory);
}
