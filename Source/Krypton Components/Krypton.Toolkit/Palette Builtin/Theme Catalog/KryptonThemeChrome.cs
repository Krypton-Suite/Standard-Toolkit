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
/// Resolves chrome kind, shield-icon era, and toolbar image packs from the theme catalog.
/// When a descriptor is missing (Themes not loaded), values are guessed from <see cref="PaletteMode"/> names.
/// </summary>
public static class KryptonThemeChrome
{
    /// <summary>
    /// Default shield artwork for a chrome kind when the descriptor does not override it.
    /// </summary>
    public static KryptonThemeShieldIconStyle DefaultShieldIconStyle(KryptonThemeChromeKind chromeKind)
    {
        switch (chromeKind)
        {
            case KryptonThemeChromeKind.ProfessionalSystem:
            case KryptonThemeChromeKind.ProfessionalOffice2003:
            case KryptonThemeChromeKind.Office2007:
            case KryptonThemeChromeKind.Sparkle:
                return KryptonThemeShieldIconStyle.Vista;
            case KryptonThemeChromeKind.Office2010:
            case KryptonThemeChromeKind.Office2013:
            case KryptonThemeChromeKind.VisualStudio:
                return KryptonThemeShieldIconStyle.Windows7;
            default:
                return KryptonThemeShieldIconStyle.Windows10;
        }
    }

    /// <summary>
    /// Gets the chrome kind for <paramref name="mode"/>.
    /// </summary>
    public static KryptonThemeChromeKind GetChromeKind(PaletteMode mode)
    {
        if (KryptonThemeCatalog.TryGetDescriptor(mode, out var descriptor) && descriptor != null)
        {
            return descriptor.ChromeKind;
        }

        return GuessChromeKind(mode);
    }

    /// <summary>
    /// Gets the shield-icon era for <paramref name="mode"/>.
    /// </summary>
    public static KryptonThemeShieldIconStyle GetShieldIconStyle(PaletteMode mode)
    {
        if (mode == PaletteMode.Global || mode == PaletteMode.Custom)
        {
            return KryptonThemeShieldIconStyle.OperatingSystem;
        }

        if (KryptonThemeCatalog.TryGetDescriptor(mode, out var descriptor) && descriptor != null)
        {
            return descriptor.ShieldIconStyle;
        }

        return GuessShieldIconStyle(mode, GuessChromeKind(mode));
    }

    /// <summary>
    /// Applies toolbar images for <paramref name="mode"/> onto <see cref="KryptonManager.Images"/>.
    /// </summary>
    internal static void ApplyToolbarImages(PaletteMode mode)
    {
        if (mode == PaletteMode.Global || mode == PaletteMode.Custom)
        {
            KryptonManager.Images.ToolbarImages.SetToolBarImages(ToolkitStaticVariables.GenericToolBarImages);
            return;
        }

        switch (GetChromeKind(mode))
        {
            case KryptonThemeChromeKind.ProfessionalSystem:
                KryptonManager.Images.ToolbarImages.SetToolBarImages(ToolkitStaticVariables.SystemToolBarImages);
                break;
            case KryptonThemeChromeKind.ProfessionalOffice2003:
                KryptonManager.Images.ToolbarImages.SetToolBarImages(ToolkitStaticVariables.Office2003ToolBarImages);
                break;
            case KryptonThemeChromeKind.Office2007:
                KryptonManager.Images.ToolbarImages.SetToolBarImages(ToolkitStaticVariables.Office2007ToolBarImages);
                break;
            case KryptonThemeChromeKind.Office2010:
            case KryptonThemeChromeKind.Sparkle:
            case KryptonThemeChromeKind.Retro:
                KryptonManager.Images.ToolbarImages.SetToolBarImages(ToolkitStaticVariables.Office2010ToolBarImages);
                break;
            case KryptonThemeChromeKind.Office2013:
                KryptonManager.Images.ToolbarImages.SetToolBarImages(ToolkitStaticVariables.Office2013ToolBarImages);
                break;
            case KryptonThemeChromeKind.VisualStudio:
                KryptonManager.Images.ToolbarImages.SetToolBarImages(ToolkitStaticVariables.VisualStudioToolBarImages);
                break;
            default:
                // Microsoft 365, Material, macOS (Material toolbar pack is not distinct yet).
                KryptonManager.Images.ToolbarImages.SetToolBarImages(ToolkitStaticVariables.Microsoft365ToolBarImages);
                break;
        }
    }

    /// <summary>
    /// Infers chrome from <see cref="PaletteMode"/> naming when no descriptor is registered.
    /// </summary>
    public static KryptonThemeChromeKind GuessChromeKind(PaletteMode mode)
    {
        switch (mode)
        {
            case PaletteMode.ProfessionalSystem:
                return KryptonThemeChromeKind.ProfessionalSystem;
            case PaletteMode.ProfessionalOffice2003:
                return KryptonThemeChromeKind.ProfessionalOffice2003;
            case PaletteMode.VisualStudio2010Render2007:
                return KryptonThemeChromeKind.Office2007;
            case PaletteMode.VisualStudio2010Render2010:
                return KryptonThemeChromeKind.Office2010;
            case PaletteMode.VisualStudio2010Render2013:
                return KryptonThemeChromeKind.Office2013;
            case PaletteMode.VisualStudio2010Render365:
                return KryptonThemeChromeKind.Microsoft365;
        }

        var name = mode.ToString();
        if (name.StartsWith(@"VisualStudio", StringComparison.Ordinal))
        {
            return KryptonThemeChromeKind.VisualStudio;
        }

        if (name.StartsWith(@"Sparkle", StringComparison.Ordinal))
        {
            return KryptonThemeChromeKind.Sparkle;
        }

        if (name.StartsWith(@"Material", StringComparison.Ordinal))
        {
            return KryptonThemeChromeKind.Material;
        }

        if (name.StartsWith(@"Retro", StringComparison.Ordinal))
        {
            return KryptonThemeChromeKind.Retro;
        }

        if (name.StartsWith(@"MacOS", StringComparison.Ordinal))
        {
            return KryptonThemeChromeKind.MacOS;
        }

        if (name.StartsWith(@"Office2007", StringComparison.Ordinal))
        {
            return KryptonThemeChromeKind.Office2007;
        }

        if (name.StartsWith(@"Office2010", StringComparison.Ordinal))
        {
            return KryptonThemeChromeKind.Office2010;
        }

        if (name.StartsWith(@"Office2013", StringComparison.Ordinal))
        {
            return KryptonThemeChromeKind.Office2013;
        }

        return KryptonThemeChromeKind.Microsoft365;
    }

    private static KryptonThemeShieldIconStyle GuessShieldIconStyle(PaletteMode mode, KryptonThemeChromeKind chromeKind)
    {
        switch (mode)
        {
            case PaletteMode.VisualStudio2010Render2007:
            case PaletteMode.VisualStudio2010Render2010:
            case PaletteMode.VisualStudio2010Render2013:
            case PaletteMode.VisualStudio2010Render365:
                return KryptonThemeShieldIconStyle.Windows7;
        }

        var name = mode.ToString();
        if (!name.StartsWith(@"Sparkle", StringComparison.Ordinal)
            && (name.IndexOf(@"HighContrast", StringComparison.Ordinal) >= 0
                || name.IndexOf(@"Deuteranopia", StringComparison.Ordinal) >= 0
                || name.IndexOf(@"Protanopia", StringComparison.Ordinal) >= 0))
        {
            return KryptonThemeShieldIconStyle.Windows10;
        }

        return DefaultShieldIconStyle(chromeKind);
    }
}
