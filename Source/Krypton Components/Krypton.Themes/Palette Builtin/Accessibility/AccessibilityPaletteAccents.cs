#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Themes;

/// <summary>
/// Remaps Microsoft 365 family button chrome arrays away from the default gold
/// tracking/checked colours onto accessibility accents (#4168).
/// </summary>
internal static class AccessibilityPaletteAccents
{
    /// <summary>
    /// Deuteranopia: blue primary, orange secondary (checked/hover), purple accent.
    /// </summary>
    public static void ApplyDeuteranopia(PaletteBase palette)
    {
        var orange = Color.FromArgb(230, 159, 0);
        var orangeLight = Color.FromArgb(255, 200, 80);
        var orangePale = Color.FromArgb(255, 235, 190);

        ApplyButtonChrome(palette,
            track1: orangePale, track2: Color.FromArgb(255, 245, 220), trackBorder: orangeLight,
            checked1: orange, checked2: orangeLight, checkedBorder: Color.FromArgb(180, 120, 0),
            pressed1: Color.FromArgb(200, 130, 0), pressedBorder: Color.FromArgb(150, 90, 0),
            appTrack: orangePale, appPressed: orange);
    }

    /// <summary>
    /// Protanopia: blue primary, brown secondary (checked/hover), magenta accent.
    /// </summary>
    public static void ApplyProtanopia(PaletteBase palette)
    {
        var brown = Color.FromArgb(153, 79, 0);
        // Keep checked fills dark enough for white TextButtonChecked (~6:1); avoid pale brown+white.
        var brownDeep = Color.FromArgb(130, 65, 0);
        var brownPale = Color.FromArgb(235, 205, 170);

        ApplyButtonChrome(palette,
            track1: brownPale, track2: Color.FromArgb(245, 230, 210), trackBorder: Color.FromArgb(200, 130, 50),
            checked1: brown, checked2: brownDeep, checkedBorder: Color.FromArgb(120, 60, 0),
            pressed1: brownDeep, pressedBorder: Color.FromArgb(100, 50, 0),
            appTrack: brownPale, appPressed: brown);
    }

    /// <summary>
    /// High contrast: neon green checked, yellow track/pressed, cyan borders.
    /// </summary>
    public static void ApplyHighContrast(PaletteBase palette)
    {
        var green = Color.FromArgb(0, 255, 0);
        var yellow = Color.FromArgb(255, 255, 0);
        var cyan = Color.FromArgb(0, 255, 255);

        ApplyButtonChrome(palette,
            track1: cyan, track2: Color.FromArgb(0, 220, 220), trackBorder: Color.White,
            checked1: green, checked2: Color.FromArgb(0, 220, 0), checkedBorder: Color.Black,
            pressed1: yellow, pressedBorder: Color.Black,
            appTrack: cyan, appPressed: yellow);
    }

    private static void ApplyButtonChrome(
        PaletteBase palette,
        Color track1,
        Color track2,
        Color trackBorder,
        Color checked1,
        Color checked2,
        Color checkedBorder,
        Color pressed1,
        Color pressedBorder,
        Color appTrack,
        Color appPressed)
    {
        // ButtonBackColor slots used by PaletteMicrosoft365Base for hover/checked/pressed.
        palette.SetArrayColor(ButtonBackColor.Color3, track1);
        palette.SetArrayColor(ButtonBackColor.Color4, track2);
        palette.SetArrayColor(ButtonBackColor.Color5, pressed1);
        palette.SetArrayColor(ButtonBackColor.Color6, pressedBorder);
        palette.SetArrayColor(ButtonBackColor.Color7, checked1);
        palette.SetArrayColor(ButtonBackColor.Color8, checked2);
        palette.SetArrayColor(ButtonBackColor.Color9, checked1);
        palette.SetArrayColor(ButtonBackColor.Color10, checked2);

        palette.SetArrayColor(ButtonBorderColor.Color2, trackBorder);
        palette.SetArrayColor(ButtonBorderColor.Color3, trackBorder);
        palette.SetArrayColor(ButtonBorderColor.Color4, pressedBorder);
        palette.SetArrayColor(ButtonBorderColor.Color5, pressedBorder);
        palette.SetArrayColor(ButtonBorderColor.Color6, checkedBorder);
        palette.SetArrayColor(ButtonBorderColor.Color7, checkedBorder);

        palette.SetArrayColor(AppButtonTrackColor.Color1, track2);
        palette.SetArrayColor(AppButtonTrackColor.Color2, track1);
        palette.SetArrayColor(AppButtonTrackColor.Color3, trackBorder);
        palette.SetArrayColor(AppButtonTrackColor.Color4, track1);
        palette.SetArrayColor(AppButtonTrackColor.Color5, trackBorder);

        palette.SetArrayColor(AppButtonPressedColor.Color1, appTrack);
        palette.SetArrayColor(AppButtonPressedColor.Color2, appPressed);
        palette.SetArrayColor(AppButtonPressedColor.Color3, pressedBorder);
        palette.SetArrayColor(AppButtonPressedColor.Color4, appPressed);
        palette.SetArrayColor(AppButtonPressedColor.Color5, pressedBorder);
    }
}