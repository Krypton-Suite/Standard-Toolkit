#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Describes a builtin palette implementation that can be registered in <see cref="KryptonThemeCatalog"/>.
/// </summary>
public sealed class KryptonThemeDescriptor
{
    /// <summary>
    /// Initializes a new descriptor.
    /// </summary>
    /// <param name="mode">Public <see cref="PaletteMode"/> identity. Extra themes keep their historical enum values.</param>
    /// <param name="family">Grouping key used by <see cref="KryptonThemeAvailability"/>.</param>
    /// <param name="isCore"><see langword="true"/> when the implementation ships in <c>Krypton.Toolkit</c>.</param>
    /// <param name="paletteType">Concrete <see cref="PaletteBase"/> subclass.</param>
    /// <param name="factory">Creates a process-wide instance; the catalog caches the result.</param>
    /// <exception cref="ArgumentNullException">A required argument is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="family"/> is empty or <paramref name="mode"/> is a sentinel.</exception>
    public KryptonThemeDescriptor(PaletteMode mode, string family, bool isCore, Type paletteType, Func<PaletteBase> factory)
        : this(mode, family, KryptonThemeChrome.GuessChromeKind(mode),
            KryptonThemeChrome.DefaultShieldIconStyle(KryptonThemeChrome.GuessChromeKind(mode)),
            isCore, paletteType, factory)
    {
    }

    /// <summary>
    /// Initializes a new descriptor with explicit chrome and shield metadata.
    /// </summary>
    /// <param name="mode">Public <see cref="PaletteMode"/> identity. Extra themes keep their historical enum values.</param>
    /// <param name="family">Grouping key used by <see cref="KryptonThemeAvailability"/>.</param>
    /// <param name="chromeKind">Renderer / chrome era for toolbar images.</param>
    /// <param name="shieldIconStyle">UAC shield artwork era.</param>
    /// <param name="isCore"><see langword="true"/> when the implementation ships in <c>Krypton.Toolkit</c>.</param>
    /// <param name="paletteType">Concrete <see cref="PaletteBase"/> subclass.</param>
    /// <param name="factory">Creates a process-wide instance; the catalog caches the result.</param>
    /// <exception cref="ArgumentNullException">A required argument is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="family"/> is empty or <paramref name="mode"/> is a sentinel.</exception>
    public KryptonThemeDescriptor(PaletteMode mode, string family, KryptonThemeChromeKind chromeKind,
        KryptonThemeShieldIconStyle shieldIconStyle, bool isCore, Type paletteType, Func<PaletteBase> factory)
    {
        if (string.IsNullOrWhiteSpace(family))
        {
            throw new ArgumentException(@"A theme family name is required.", nameof(family));
        }

        if (mode == PaletteMode.Global || mode == PaletteMode.Custom)
        {
            throw new ArgumentException(@"Global and Custom are not catalog themes.", nameof(mode));
        }

        Mode = mode;
        Family = family;
        ChromeKind = chromeKind;
        ShieldIconStyle = shieldIconStyle;
        IsCore = isCore;
        PaletteType = paletteType ?? throw new ArgumentNullException(nameof(paletteType));
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    /// <summary>
    /// Gets the <see cref="PaletteMode"/> this implementation satisfies.
    /// </summary>
    public PaletteMode Mode { get; }

    /// <summary>
    /// Gets the family key (for example <see cref="KryptonThemeFamilies.Office2007"/>).
    /// </summary>
    public string Family { get; }

    /// <summary>
    /// Gets the renderer / chrome era (toolbar images follow this, not <see cref="Family"/>).
    /// </summary>
    public KryptonThemeChromeKind ChromeKind { get; }

    /// <summary>
    /// Gets the UAC shield artwork era for this palette.
    /// </summary>
    public KryptonThemeShieldIconStyle ShieldIconStyle { get; }

    /// <summary>
    /// Gets the selector display name for <see cref="Mode"/>.
    /// </summary>
    public string DisplayName => KryptonThemeCatalog.GetDisplayName(Mode);

    /// <summary>
    /// Gets whether the palette ships in the core toolkit assembly.
    /// </summary>
    public bool IsCore { get; }

    /// <summary>
    /// Gets the concrete palette type.
    /// </summary>
    public Type PaletteType { get; }

    /// <summary>
    /// Gets the factory used on first <see cref="KryptonManager.GetPaletteForMode"/> for this mode.
    /// </summary>
    public Func<PaletteBase> Factory { get; }
}
