#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Builds a full <see cref="KryptonCustomPaletteBase"/> from a few seed colours by remapping a builtin donor scheme.
/// </summary>
/// <remarks>
/// The generator copies an Office 2010 or Microsoft 365 donor, hue-shifts chromatic slots toward the seed,
/// overwrites button/header/form accents, snapshots the result into a custom palette, then patches
/// Tracking / Pressed / Checked button colours that would otherwise keep the family's gold/orange LUT.
/// It never mutates <see cref="KryptonManager.GetPaletteForMode"/> singletons and never registers
/// type-keyed LUT colours on builtin palette types.
/// </remarks>
public static class KryptonCustomThemeGenerator
{
    private static readonly object _randomLock = new();
    private static readonly Random _random = new();
    private static readonly PaletteMode[] _supportedDonors =
    {
        PaletteMode.Office2010Blue,
        PaletteMode.Office2010BlueDarkMode,
        PaletteMode.Microsoft365Blue,
        PaletteMode.Microsoft365BlackDarkMode
    };

    /// <summary>
    /// Gets the builtin donor modes supported by the generator.
    /// </summary>
    public static IReadOnlyList<PaletteMode> SupportedDonorModes => _supportedDonors;

    /// <summary>
    /// Returns whether <paramref name="mode"/> can be used as a donor.
    /// </summary>
    /// <param name="mode">Palette mode to test.</param>
    /// <returns><c>true</c> when the mode is a supported donor.</returns>
    public static bool IsSupportedDonor(PaletteMode mode)
    {
        for (int i = 0; i < _supportedDonors.Length; i++)
        {
            if (_supportedDonors[i] == mode)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Returns a short display name for a supported donor mode.
    /// </summary>
    /// <param name="mode">Supported donor mode.</param>
    /// <returns>Friendly name for combo boxes and documentation.</returns>
    public static string GetDonorDisplayName(PaletteMode mode) => mode switch
    {
        PaletteMode.Office2010Blue => @"Office 2010 Blue",
        PaletteMode.Office2010BlueDarkMode => @"Office 2010 Blue Dark",
        PaletteMode.Microsoft365Blue => @"Microsoft 365 Blue",
        PaletteMode.Microsoft365BlackDarkMode => @"Microsoft 365 Black Dark",
        _ => mode.ToString()
    };

    /// <summary>
    /// Parses a colour from hexadecimal (<c>#RGB</c>, <c>#RRGGBB</c>, <c>#AARRGGBB</c>),
    /// comma-separated RGB, <c>rgb(r,g,b)</c>, or a named HTML colour.
    /// </summary>
    /// <param name="text">Input text.</param>
    /// <param name="color">Parsed colour when the method returns <c>true</c>.</param>
    /// <returns><c>true</c> when <paramref name="text"/> was recognised.</returns>
    public static bool TryParseColor(string? text, out Color color) =>
        CustomThemeColorMath.TryParseColor(text, out color);

    /// <summary>
    /// Formats <paramref name="color"/> as <c>#RRGGBB</c> (or <c>#AARRGGBB</c> when alpha is not opaque).
    /// </summary>
    /// <param name="color">Colour to format.</param>
    /// <returns>Hexadecimal colour string.</returns>
    public static string FormatColor(Color color) => CustomThemeColorMath.ToHex(color);

    /// <summary>
    /// Builds a random seed that can be previewed, applied, registered, or exported like any other custom theme.
    /// </summary>
    /// <param name="namePrefix">Optional display-name prefix. Defaults to <c>Random Theme</c>.</param>
    /// <returns>A seed with a random donor and coordinated primary, secondary, and surface colours.</returns>
    public static KryptonCustomThemeSeed CreateRandomSeed(string? namePrefix = null)
    {
        lock (_randomLock)
        {
            PaletteMode donorMode = _supportedDonors[_random.Next(_supportedDonors.Length)];
            bool dark = IsDarkDonor(donorMode);

            float hue = NextFloat(0f, 360f);
            float saturation = NextFloat(0.58f, 0.88f);
            float lightness = dark
                ? NextFloat(0.50f, 0.64f)
                : NextFloat(0.42f, 0.56f);

            Color primary = CustomThemeColorMath.FromHsl(hue, saturation, lightness, 255);
            float secondaryOffset = NextFloat(24f, 42f) * (_random.Next(2) == 0 ? -1f : 1f);
            Color secondary = CustomThemeColorMath.Analogous(primary, secondaryOffset);
            Color surface = dark
                ? CustomThemeColorMath.Darken(CommonHelper.MergeColors(Color.Black, 0.80f, primary, 0.20f), 0.06f)
                : CommonHelper.MergeColors(Color.White, 0.88f, primary, 0.12f);

            string prefix = string.IsNullOrWhiteSpace(namePrefix) ? @"Random Theme" : namePrefix?.Trim() ?? @"Random Theme";
            string suffix = CustomThemeColorMath.ToHex(primary).TrimStart('#');

            return new KryptonCustomThemeSeed
            {
                Name = string.Format(CultureInfo.InvariantCulture, @"{0} {1}", prefix, suffix),
                Primary = primary,
                Secondary = secondary,
                Surface = surface,
                DonorMode = donorMode
            };
        }
    }

    /// <summary>
    /// Builds a named custom palette from a primary hex or RGB string, using Office 2010 Blue as the donor.
    /// </summary>
    /// <param name="name">Display name for theme selectors and XML export.</param>
    /// <param name="primaryHex">Primary colour as hex, RGB, or a named HTML colour.</param>
    /// <returns>A populated custom palette ready to apply or export.</returns>
    public static KryptonCustomPaletteBase Create(string name, string primaryHex)
    {
        if (!TryParseColor(primaryHex, out Color primary))
        {
            ThrowHelper.ThrowArgumentException(@"A hexadecimal, RGB, or named colour is required.", nameof(primaryHex));
        }

        try
        {
            return Create(new KryptonCustomThemeSeed
            {
                Name = name,
                Primary = primary
            });
        }
        catch (Exception ex)
        {
            KryptonExceptionHandler.CaptureException(ex);

            return new KryptonCustomPaletteBase();
        }
    }

    /// <summary>
    /// Builds a named custom palette from explicit seed colours.
    /// </summary>
    /// <param name="name">Display name for theme selectors and XML export.</param>
    /// <param name="primary">Required brand / accent colour.</param>
    /// <param name="secondary">Optional secondary accent; analogous hue when <c>null</c>.</param>
    /// <param name="surface">Optional panel surface; derived from primary when <c>null</c>.</param>
    /// <param name="donorMode">Builtin donor whose chrome shape is preserved.</param>
    /// <returns>A populated custom palette ready to apply or export.</returns>
    public static KryptonCustomPaletteBase Create(string name, Color primary, Color? secondary, Color? surface, PaletteMode donorMode) =>
        Create(new KryptonCustomThemeSeed
        {
            Name = name,
            Primary = primary,
            Secondary = secondary,
            Surface = surface,
            DonorMode = donorMode
        });

    /// <summary>
    /// Builds a named custom palette from <paramref name="seed"/>.
    /// </summary>
    /// <param name="seed">Seed colours and donor family. Cannot be null.</param>
    /// <returns>A populated custom palette ready to apply or export.</returns>
    public static KryptonCustomPaletteBase Create(KryptonCustomThemeSeed seed)
    {
        ThrowHelper.ThrowIfNull(seed);

        try
        {
            if (string.IsNullOrWhiteSpace(seed.Name))
            {
                ThrowHelper.ThrowArgumentException(@"A theme display name is required.", nameof(seed));
            }

            if (!IsSupportedDonor(seed.DonorMode))
            {
                ThrowHelper.ThrowArgumentException(
                    @"DonorMode must be Office2010Blue, Office2010BlueDarkMode, Microsoft365Blue, or Microsoft365BlackDarkMode.",
                    nameof(seed));
            }

            bool dark = IsDarkDonor(seed.DonorMode);
            CustomThemeAccentSet accents = CustomThemeSchemeRemapper.BuildAccents(seed, dark);
            PaletteBase throwaway = CreateThrowawayPalette(seed.DonorMode);
            KryptonColorSchemeBase remapped = CustomThemeSchemeRemapper.Remap(CopyDonorScheme(throwaway), accents);
            throwaway.ApplyScheme(remapped);

            var custom = new KryptonCustomPaletteBase
            {
                BasePalette = throwaway
            };
            custom.PopulateFromBase(silent: true);
            custom.SetPaletteName(seed.Name);
            PatchInteractiveButtonColors(custom, accents);
            return custom;
        }
        catch (Exception ex)
        {
            KryptonExceptionHandler.CaptureException(ex);
            return new KryptonCustomPaletteBase();
        }
    }

    /// <summary>
    /// Registers a factory so the theme appears in theme selectors. Optionally applies it immediately.
    /// </summary>
    /// <param name="seed">Seed used each time the theme is selected. Cannot be null.</param>
    /// <param name="apply">When <c>true</c>, applies a generated palette after registration.</param>
    /// <param name="manager">Manager used when <paramref name="apply"/> is <c>true</c>. A new instance is used when null.</param>
    public static void Register(KryptonCustomThemeSeed seed, bool apply = false, KryptonManager? manager = null)
    {
        ThrowHelper.ThrowIfNull(seed);

        KryptonCustomThemeSeed captured = seed.Clone();
        ThemeManager.RegisterCustomTheme(captured.Name, () => Create(captured.Clone()));

        if (apply)
        {
            ThemeManager.ApplyTheme(Create(captured.Clone()), manager ?? new KryptonManager());
        }
    }

    /// <summary>
    /// Exports <paramref name="palette"/> to a Krypton palette file (<c>.kthemex</c> XML or optional native <c>.ktheme</c>).
    /// </summary>
    /// <param name="palette">Palette to export. Cannot be null.</param>
    /// <param name="filePath">Destination path. Cannot be empty.</param>
    /// <param name="ignoreDefaults">When <c>true</c>, omits properties that match base defaults.</param>
    // ToDo V120 LTS: Stop writing .xml from the theme generator; destinations should be .kthemex or .ktheme.
    public static void Export(KryptonCustomPaletteBase palette, string filePath, bool ignoreDefaults = true)
    {
        ThrowHelper.ThrowIfNull(palette);
        if (string.IsNullOrWhiteSpace(filePath))
        {
            ThrowHelper.ThrowArgumentException(@"A file path is required.", nameof(filePath));
        }

        palette.Export(filePath, ignoreDefaults, silent: true);
    }

    internal static bool IsDarkDonor(PaletteMode mode) =>
        mode == PaletteMode.Office2010BlueDarkMode || mode == PaletteMode.Microsoft365BlackDarkMode;

    private static KryptonColorSchemeBase CopyDonorScheme(PaletteBase donor)
    {
        var scheme = new EmptySchemeBase();
        Type schemeType = typeof(EmptySchemeBase);
        foreach (SchemeBaseColors index in (SchemeBaseColors[])Enum.GetValues(typeof(SchemeBaseColors)))
        {
            PropertyInfo? property = schemeType.GetProperty(index.ToString());
            if (property != null && property.CanWrite)
            {
                property.SetValue(scheme, donor.GetSchemeColor(index));
            }
        }

        return scheme;
    }

    /// <summary>
    /// Creates a fresh donor palette. Extra dark modes live in <c>Krypton.Themes</c> and are resolved
    /// through the catalog factory so this assembly does not take a compile-time Themes dependency.
    /// The catalog singleton is never used, so <see cref="KryptonManager.GetPaletteForMode"/> is not mutated.
    /// </summary>
    private static PaletteBase CreateThrowawayPalette(PaletteMode mode)
    {
        if (!KryptonThemeCatalog.TryGetDescriptor(mode, out KryptonThemeDescriptor? descriptor) || descriptor is null)
        {
            return ThrowHelper.ThrowArgumentOutOfRangeException<PaletteBase>(nameof(mode), mode,
                @"Donor palette is not registered. Extra dark donors require Krypton.Themes.");
        }

        return descriptor.Factory();
    }

    /// <summary>
    /// Overwrites Tracking / Pressed / Checked / Disabled fills that PopulateFromBase copied from the
    /// family LUT (Office gold/orange), so hover and press follow the seed accent.
    /// </summary>
    private static void PatchInteractiveButtonColors(KryptonCustomPaletteBase custom, CustomThemeAccentSet accents)
    {
        KryptonPaletteCheckButton[] buttons =
        {
            custom.ButtonStyles.ButtonCommon,
            custom.ButtonStyles.ButtonStandalone,
            custom.ButtonStyles.ButtonCommand,
            custom.ButtonStyles.ButtonAlternate,
            custom.ButtonStyles.ButtonCluster,
            custom.ButtonStyles.ButtonGallery
        };

        for (int i = 0; i < buttons.Length; i++)
        {
            PatchButton(buttons[i], accents);
        }

        PaletteRibbonBack tracking = custom.Ribbon.RibbonAppButton.StateTracking;
        tracking.BackColor1 = accents.HoverTop;
        tracking.BackColor2 = accents.HoverBottom;
        tracking.BackColor3 = accents.CheckedTop;
        tracking.BackColor4 = accents.HoverTop;
        tracking.BackColor5 = accents.CheckedBottom;

        PaletteRibbonBack pressed = custom.Ribbon.RibbonAppButton.StatePressed;
        pressed.BackColor1 = accents.PressedTop;
        pressed.BackColor2 = accents.PressedBottom;
        pressed.BackColor3 = accents.PressedBorder;
        pressed.BackColor4 = accents.PressedTop;
        pressed.BackColor5 = accents.PressedBottom;
    }

    private static void PatchButton(KryptonPaletteCheckButton button, CustomThemeAccentSet accents)
    {
        ApplyTriple(button.StateDisabled, accents.DisabledTop, accents.DisabledBottom, accents.DisabledBorder, accents.MutedText);
        ApplyTriple(button.StateTracking, accents.HoverTop, accents.HoverBottom, accents.HoverBorder, accents.OnAccent);
        ApplyTriple(button.StatePressed, accents.PressedTop, accents.PressedBottom, accents.PressedBorder, accents.OnAccent);
        ApplyTriple(button.StateCheckedNormal, accents.CheckedTop, accents.CheckedBottom, accents.CheckedBorder, accents.OnAccent);
        ApplyTriple(button.StateCheckedTracking, accents.HoverTop, accents.HoverBottom, accents.HoverBorder, accents.OnAccent);
        ApplyTriple(button.StateCheckedPressed, accents.PressedTop, accents.PressedBottom, accents.PressedBorder, accents.OnAccent);
    }

    private static void ApplyTriple(PaletteTriple state, Color back1, Color back2, Color border, Color text)
    {
        state.Back.Color1 = back1;
        state.Back.Color2 = back2;
        state.Border.Color1 = border;
        state.Content.ShortText.Color1 = text;
    }

    private static float NextFloat(float minInclusive, float maxInclusive) =>
        (float)(minInclusive + (_random.NextDouble() * (maxInclusive - minInclusive)));
}
