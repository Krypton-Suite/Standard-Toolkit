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

        return Create(new KryptonCustomThemeSeed
        {
            Name = name,
            Primary = primary
        });
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
        KryptonColorSchemeBase remapped = CustomThemeSchemeRemapper.Remap(CreateDonorScheme(seed.DonorMode), accents);
        PaletteBase throwaway = CreateThrowawayPalette(seed.DonorMode);
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
    /// Exports <paramref name="palette"/> to a Krypton palette XML file.
    /// </summary>
    /// <param name="palette">Palette to export. Cannot be null.</param>
    /// <param name="filePath">Destination path. Cannot be empty.</param>
    /// <param name="ignoreDefaults">When <c>true</c>, omits properties that match base defaults.</param>
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

    private static KryptonColorSchemeBase CreateDonorScheme(PaletteMode mode) => mode switch
    {
        PaletteMode.Office2010Blue => new PaletteOffice2010Blue_BaseScheme(),
        PaletteMode.Office2010BlueDarkMode => new PaletteOffice2010BlueDarkMode_BaseScheme(),
        PaletteMode.Microsoft365Blue => new PaletteMicrosoft365Blue_BaseScheme(),
        PaletteMode.Microsoft365BlackDarkMode => new PaletteMicrosoft365BlackDarkMode_BaseScheme(),
        _ => ThrowHelper.ThrowArgumentOutOfRangeException<KryptonColorSchemeBase>(nameof(mode), mode, @"Unsupported donor.")
    };

    private static PaletteBase CreateThrowawayPalette(PaletteMode mode) => mode switch
    {
        PaletteMode.Office2010Blue => new PaletteOffice2010Blue(),
        PaletteMode.Office2010BlueDarkMode => new PaletteOffice2010BlueDarkMode(),
        PaletteMode.Microsoft365Blue => new PaletteMicrosoft365Blue(),
        PaletteMode.Microsoft365BlackDarkMode => new PaletteMicrosoft365BlackDarkMode(),
        _ => ThrowHelper.ThrowArgumentOutOfRangeException<PaletteBase>(nameof(mode), mode, @"Unsupported donor.")
    };

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
}
