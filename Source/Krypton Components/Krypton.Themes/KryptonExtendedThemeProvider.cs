#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Themes;

/// <summary>
/// Registers extra builtin palettes shipped in <c>Krypton.Themes</c>.
/// </summary>
public sealed class KryptonExtendedThemeProvider : IKryptonThemeProvider
{
    /// <inheritdoc />
    public IReadOnlyList<KryptonThemeDescriptor> GetThemes()
    {
        return new[]
        {
            Extra(PaletteMode.Office2007BlueDarkMode, typeof(PaletteOffice2007BlueDarkMode), () => new PaletteOffice2007BlueDarkMode()),
            Extra(PaletteMode.Office2007BlueLightMode, typeof(PaletteOffice2007BlueLightMode), () => new PaletteOffice2007BlueLightMode()),
            Extra(PaletteMode.Office2007SilverDarkMode, typeof(PaletteOffice2007SilverDarkMode), () => new PaletteOffice2007SilverDarkMode()),
            Extra(PaletteMode.Office2007SilverLightMode, typeof(PaletteOffice2007SilverLightMode), () => new PaletteOffice2007SilverLightMode()),
            Extra(PaletteMode.Office2007White, typeof(PaletteOffice2007White), () => new PaletteOffice2007White()),
            Extra(PaletteMode.Office2007BlackDarkMode, typeof(PaletteOffice2007BlackDarkMode), () => new PaletteOffice2007BlackDarkMode()),
            Extra(PaletteMode.Office2010BlueDarkMode, typeof(PaletteOffice2010BlueDarkMode), () => new PaletteOffice2010BlueDarkMode()),
            Extra(PaletteMode.Office2010BlueLightMode, typeof(PaletteOffice2010BlueLightMode), () => new PaletteOffice2010BlueLightMode()),
            Extra(PaletteMode.Office2010SilverDarkMode, typeof(PaletteOffice2010SilverDarkMode), () => new PaletteOffice2010SilverDarkMode()),
            Extra(PaletteMode.Office2010SilverLightMode, typeof(PaletteOffice2010SilverLightMode), () => new PaletteOffice2010SilverLightMode()),
            Extra(PaletteMode.Office2010White, typeof(PaletteOffice2010White), () => new PaletteOffice2010White()),
            Extra(PaletteMode.Office2010BlackDarkMode, typeof(PaletteOffice2010BlackDarkMode), () => new PaletteOffice2010BlackDarkMode()),
            Extra(PaletteMode.Office2013DarkGray, typeof(PaletteOffice2013DarkGray), () => new PaletteOffice2013DarkGray()),
            Extra(PaletteMode.Office2013LightGray, typeof(PaletteOffice2013LightGray), () => new PaletteOffice2013LightGray()),
            Extra(PaletteMode.Office2013White, typeof(PaletteOffice2013White), () => new PaletteOffice2013White()),
            Extra(PaletteMode.SparkleBlueDarkMode, typeof(PaletteSparkleBlueDarkMode), () => new PaletteSparkleBlueDarkMode()),
            Extra(PaletteMode.SparkleBlueLightMode, typeof(PaletteSparkleBlueLightMode), () => new PaletteSparkleBlueLightMode()),
            Extra(PaletteMode.SparkleOrangeDarkMode, typeof(PaletteSparkleOrangeDarkMode), () => new PaletteSparkleOrangeDarkMode()),
            Extra(PaletteMode.SparkleOrangeLightMode, typeof(PaletteSparkleOrangeLightMode), () => new PaletteSparkleOrangeLightMode()),
            Extra(PaletteMode.SparklePurpleDarkMode, typeof(PaletteSparklePurpleDarkMode), () => new PaletteSparklePurpleDarkMode()),
            Extra(PaletteMode.SparklePurpleLightMode, typeof(PaletteSparklePurpleLightMode), () => new PaletteSparklePurpleLightMode()),
            Extra(PaletteMode.Microsoft365BlackDarkMode, typeof(PaletteMicrosoft365BlackDarkMode), () => new PaletteMicrosoft365BlackDarkMode()),
            Extra(PaletteMode.Microsoft365BlackDarkModeAlternate, typeof(PaletteMicrosoft365BlackDarkModeAlternate), () => new PaletteMicrosoft365BlackDarkModeAlternate()),
            Extra(PaletteMode.Microsoft365BlueDarkMode, typeof(PaletteMicrosoft365BlueDarkMode), () => new PaletteMicrosoft365BlueDarkMode()),
            Extra(PaletteMode.Microsoft365BlueLightMode, typeof(PaletteMicrosoft365BlueLightMode), () => new PaletteMicrosoft365BlueLightMode()),
            Extra(PaletteMode.Microsoft365SilverDarkMode, typeof(PaletteMicrosoft365SilverDarkMode), () => new PaletteMicrosoft365SilverDarkMode()),
            Extra(PaletteMode.Microsoft365SilverLightMode, typeof(PaletteMicrosoft365SilverLightMode), () => new PaletteMicrosoft365SilverLightMode()),
            Extra(PaletteMode.Microsoft365White, typeof(PaletteMicrosoft365White), () => new PaletteMicrosoft365White()),
            Extra(PaletteMode.VisualStudio2010Render2007, typeof(PaletteVisualStudio2010Office2007Variation), () => new PaletteVisualStudio2010Office2007Variation()),
            Extra(PaletteMode.VisualStudio2010Render2010, typeof(PaletteVisualStudio2010Office2010Variation), () => new PaletteVisualStudio2010Office2010Variation()),
            Extra(PaletteMode.VisualStudio2010Render2013, typeof(PaletteVisualStudio2010Office2013Variation), () => new PaletteVisualStudio2010Office2013Variation()),
            Extra(PaletteMode.VisualStudio2010Render365, typeof(PaletteVisualStudio2010Microsoft365Variation), () => new PaletteVisualStudio2010Microsoft365Variation()),
            Extra(PaletteMode.VisualStudio2012Dark, typeof(PaletteVisualStudio2012Dark), () => new PaletteVisualStudio2012Dark()),
            Extra(PaletteMode.VisualStudio2012Light, typeof(PaletteVisualStudio2012Light), () => new PaletteVisualStudio2012Light()),
            Extra(PaletteMode.VisualStudio2012Blue, typeof(PaletteVisualStudio2012Blue), () => new PaletteVisualStudio2012Blue()),
            Extra(PaletteMode.VisualStudio2013Dark, typeof(PaletteVisualStudio2013Dark), () => new PaletteVisualStudio2013Dark()),
            Extra(PaletteMode.VisualStudio2013Light, typeof(PaletteVisualStudio2013Light), () => new PaletteVisualStudio2013Light()),
            Extra(PaletteMode.VisualStudio2013Blue, typeof(PaletteVisualStudio2013Blue), () => new PaletteVisualStudio2013Blue()),
            Extra(PaletteMode.VisualStudio2015Dark, typeof(PaletteVisualStudio2015Dark), () => new PaletteVisualStudio2015Dark()),
            Extra(PaletteMode.VisualStudio2015Light, typeof(PaletteVisualStudio2015Light), () => new PaletteVisualStudio2015Light()),
            Extra(PaletteMode.VisualStudio2015Blue, typeof(PaletteVisualStudio2015Blue), () => new PaletteVisualStudio2015Blue()),
            Extra(PaletteMode.VisualStudio2017Dark, typeof(PaletteVisualStudio2017Dark), () => new PaletteVisualStudio2017Dark()),
            Extra(PaletteMode.VisualStudio2017Light, typeof(PaletteVisualStudio2017Light), () => new PaletteVisualStudio2017Light()),
            Extra(PaletteMode.VisualStudio2017Blue, typeof(PaletteVisualStudio2017Blue), () => new PaletteVisualStudio2017Blue()),
            Extra(PaletteMode.VisualStudio2019Dark, typeof(PaletteVisualStudio2019Dark), () => new PaletteVisualStudio2019Dark()),
            Extra(PaletteMode.VisualStudio2019Light, typeof(PaletteVisualStudio2019Light), () => new PaletteVisualStudio2019Light()),
            Extra(PaletteMode.VisualStudio2019Blue, typeof(PaletteVisualStudio2019Blue), () => new PaletteVisualStudio2019Blue()),
            Extra(PaletteMode.VisualStudio2022Dark, typeof(PaletteVisualStudio2022Dark), () => new PaletteVisualStudio2022Dark()),
            Extra(PaletteMode.VisualStudio2022Light, typeof(PaletteVisualStudio2022Light), () => new PaletteVisualStudio2022Light()),
            Extra(PaletteMode.VisualStudio2022Blue, typeof(PaletteVisualStudio2022Blue), () => new PaletteVisualStudio2022Blue()),
            Extra(PaletteMode.VisualStudio2026Dark, typeof(PaletteVisualStudio2026Dark), () => new PaletteVisualStudio2026Dark()),
            Extra(PaletteMode.VisualStudio2026Light, typeof(PaletteVisualStudio2026Light), () => new PaletteVisualStudio2026Light()),
            Extra(PaletteMode.MaterialLight, typeof(PaletteMaterialLight), () => new PaletteMaterialLight()),
            Extra(PaletteMode.MaterialDark, typeof(PaletteMaterialDark), () => new PaletteMaterialDark()),
            Extra(PaletteMode.MaterialLightRipple, typeof(PaletteMaterialLightRipple), () => new PaletteMaterialLightRipple()),
            Extra(PaletteMode.MaterialDarkRipple, typeof(PaletteMaterialDarkRipple), () => new PaletteMaterialDarkRipple()),
            Extra(PaletteMode.MaterialLimeGreen, typeof(PaletteMaterialLimeGreen), () => new PaletteMaterialLimeGreen()),
            Extra(PaletteMode.MaterialLimeGreenDark, typeof(PaletteMaterialLimeGreenDark), () => new PaletteMaterialLimeGreenDark()),
            Extra(PaletteMode.MaterialLimeGreenRipple, typeof(PaletteMaterialLimeGreenRipple), () => new PaletteMaterialLimeGreenRipple()),
            Extra(PaletteMode.MaterialLimeGreenDarkRipple, typeof(PaletteMaterialLimeGreenDarkRipple), () => new PaletteMaterialLimeGreenDarkRipple()),
            Extra(PaletteMode.RetroGreen, typeof(PaletteRetroGreen), () => new PaletteRetroGreen()),
            Extra(PaletteMode.RetroBlue, typeof(PaletteRetroBlue), () => new PaletteRetroBlue()),
            Extra(PaletteMode.MacOSXAqua, typeof(PaletteMacOSXAqua), () => new PaletteMacOSXAqua()),
            Extra(PaletteMode.MacOSLight, typeof(PaletteMacOSLight), () => new PaletteMacOSLight()),
            Extra(PaletteMode.MacOSDark, typeof(PaletteMacOSDark), () => new PaletteMacOSDark()),
            Extra(PaletteMode.HighContrast, typeof(PaletteHighContrast), () => new PaletteHighContrast()),
            Extra(PaletteMode.Deuteranopia, typeof(PaletteDeuteranopia), () => new PaletteDeuteranopia()),
            Extra(PaletteMode.Protanopia, typeof(PaletteProtanopia), () => new PaletteProtanopia()),
            Extra(PaletteMode.Office2007HighContrast, typeof(PaletteOffice2007HighContrast), () => new PaletteOffice2007HighContrast()),
            Extra(PaletteMode.Office2007Deuteranopia, typeof(PaletteOffice2007Deuteranopia), () => new PaletteOffice2007Deuteranopia()),
            Extra(PaletteMode.Office2007Protanopia, typeof(PaletteOffice2007Protanopia), () => new PaletteOffice2007Protanopia()),
            Extra(PaletteMode.Office2010HighContrast, typeof(PaletteOffice2010HighContrast), () => new PaletteOffice2010HighContrast()),
            Extra(PaletteMode.Office2010Deuteranopia, typeof(PaletteOffice2010Deuteranopia), () => new PaletteOffice2010Deuteranopia()),
            Extra(PaletteMode.Office2010Protanopia, typeof(PaletteOffice2010Protanopia), () => new PaletteOffice2010Protanopia()),
            Extra(PaletteMode.Office2013HighContrast, typeof(PaletteOffice2013HighContrast), () => new PaletteOffice2013HighContrast()),
            Extra(PaletteMode.Office2013Deuteranopia, typeof(PaletteOffice2013Deuteranopia), () => new PaletteOffice2013Deuteranopia()),
            Extra(PaletteMode.Office2013Protanopia, typeof(PaletteOffice2013Protanopia), () => new PaletteOffice2013Protanopia()),
            Extra(PaletteMode.SparkleHighContrast, typeof(PaletteSparkleHighContrast), () => new PaletteSparkleHighContrast()),
            Extra(PaletteMode.SparkleDeuteranopia, typeof(PaletteSparkleDeuteranopia), () => new PaletteSparkleDeuteranopia()),
            Extra(PaletteMode.SparkleProtanopia, typeof(PaletteSparkleProtanopia), () => new PaletteSparkleProtanopia()),
            Extra(PaletteMode.MaterialHighContrast, typeof(PaletteMaterialHighContrast), () => new PaletteMaterialHighContrast()),
            Extra(PaletteMode.MaterialDeuteranopia, typeof(PaletteMaterialDeuteranopia), () => new PaletteMaterialDeuteranopia()),
            Extra(PaletteMode.MaterialProtanopia, typeof(PaletteMaterialProtanopia), () => new PaletteMaterialProtanopia()),
            Extra(PaletteMode.MaterialHighContrastRipple, typeof(PaletteMaterialHighContrastRipple), () => new PaletteMaterialHighContrastRipple()),
            Extra(PaletteMode.MaterialDeuteranopiaRipple, typeof(PaletteMaterialDeuteranopiaRipple), () => new PaletteMaterialDeuteranopiaRipple()),
            Extra(PaletteMode.MaterialProtanopiaRipple, typeof(PaletteMaterialProtanopiaRipple), () => new PaletteMaterialProtanopiaRipple()),
            Extra(PaletteMode.Office2007LimeGreen, typeof(PaletteOffice2007LimeGreen), () => new PaletteOffice2007LimeGreen()),
            Extra(PaletteMode.Office2007LimeGreenDark, typeof(PaletteOffice2007LimeGreenDark), () => new PaletteOffice2007LimeGreenDark()),
            Extra(PaletteMode.Office2010LimeGreen, typeof(PaletteOffice2010LimeGreen), () => new PaletteOffice2010LimeGreen()),
            Extra(PaletteMode.Office2010LimeGreenDark, typeof(PaletteOffice2010LimeGreenDark), () => new PaletteOffice2010LimeGreenDark()),
            Extra(PaletteMode.Microsoft365LimeGreen, typeof(PaletteMicrosoft365LimeGreen), () => new PaletteMicrosoft365LimeGreen()),
            Extra(PaletteMode.Microsoft365LimeGreenDark, typeof(PaletteMicrosoft365LimeGreenDark), () => new PaletteMicrosoft365LimeGreenDark()),
            Extra(PaletteMode.Office2007DarkGray, typeof(PaletteOffice2007DarkGray), () => new PaletteOffice2007DarkGray()),
            Extra(PaletteMode.Office2007LightGray, typeof(PaletteOffice2007LightGray), () => new PaletteOffice2007LightGray()),
            Extra(PaletteMode.Office2010DarkGray, typeof(PaletteOffice2010DarkGray), () => new PaletteOffice2010DarkGray()),
            Extra(PaletteMode.Office2010LightGray, typeof(PaletteOffice2010LightGray), () => new PaletteOffice2010LightGray()),
            Extra(PaletteMode.Microsoft365DarkGray, typeof(PaletteMicrosoft365DarkGray), () => new PaletteMicrosoft365DarkGray()),
            Extra(PaletteMode.Microsoft365LightGray, typeof(PaletteMicrosoft365LightGray), () => new PaletteMicrosoft365LightGray()),
            Extra(PaletteMode.MaterialDarkGray, typeof(PaletteMaterialDarkGray), () => new PaletteMaterialDarkGray()),
            Extra(PaletteMode.MaterialLightGray, typeof(PaletteMaterialLightGray), () => new PaletteMaterialLightGray()),
            Extra(PaletteMode.MaterialDarkGrayRipple, typeof(PaletteMaterialDarkGrayRipple), () => new PaletteMaterialDarkGrayRipple()),
            Extra(PaletteMode.MaterialLightGrayRipple, typeof(PaletteMaterialLightGrayRipple), () => new PaletteMaterialLightGrayRipple())
        };
    }

    private static KryptonThemeDescriptor Extra(PaletteMode mode, Type type, Func<PaletteBase> factory) =>
        new KryptonThemeDescriptor(mode, FamilyFor(mode), false, type, factory);

    private static string FamilyFor(PaletteMode mode)
    {
        var name = mode.ToString();
        if (name.IndexOf(@"LimeGreen", StringComparison.Ordinal) >= 0)
        {
            return KryptonThemeFamilies.LimeGreen;
        }

        if (name.IndexOf(@"DarkGray", StringComparison.Ordinal) >= 0 || name.IndexOf(@"LightGray", StringComparison.Ordinal) >= 0)
        {
            return KryptonThemeFamilies.Gray;
        }

        if (name.IndexOf(@"HighContrast", StringComparison.Ordinal) >= 0
            || name.IndexOf(@"Deuteranopia", StringComparison.Ordinal) >= 0
            || name.IndexOf(@"Protanopia", StringComparison.Ordinal) >= 0)
        {
            return KryptonThemeFamilies.Accessibility;
        }

        if (name.StartsWith(@"Sparkle", StringComparison.Ordinal))
        {
            return KryptonThemeFamilies.Sparkle;
        }

        if (name.StartsWith(@"VisualStudio", StringComparison.Ordinal))
        {
            return KryptonThemeFamilies.VisualStudio;
        }

        if (name.StartsWith(@"Material", StringComparison.Ordinal))
        {
            return KryptonThemeFamilies.Material;
        }

        if (name.StartsWith(@"Retro", StringComparison.Ordinal))
        {
            return KryptonThemeFamilies.Retro;
        }

        if (name.StartsWith(@"MacOS", StringComparison.Ordinal))
        {
            return KryptonThemeFamilies.MacOS;
        }

        if (name.StartsWith(@"Office2007", StringComparison.Ordinal))
        {
            return KryptonThemeFamilies.Office2007;
        }

        if (name.StartsWith(@"Office2010", StringComparison.Ordinal))
        {
            return KryptonThemeFamilies.Office2010;
        }

        if (name.StartsWith(@"Office2013", StringComparison.Ordinal))
        {
            return KryptonThemeFamilies.Office2013;
        }

        return KryptonThemeFamilies.Microsoft365;
    }
}
