#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Remaps Sparkle chrome colour arrays (track/checked/pressed/default) onto accessibility accents.
/// Sparkle does not use <see cref="AccessibilityPaletteAccents"/> (<c>SetArrayColor</c>).
/// </summary>
internal static class AccessibilitySparkleAccents
{
    // Indices match PaletteSparkleBlue._sparkleColors comments.
    private const int Track1 = 6;
    private const int Track2 = 7;
    private const int Press1 = 8;
    private const int Press2 = 9;
    private const int Check1 = 10;
    private const int Check2 = 11;
    private const int CheckTrack1 = 12;
    private const int CheckTrack2 = 13;
    private const int CheckPress1 = 14;
    private const int Accent = 15;
    private const int DefaultBack = 23;
    private const int TabChecked = 29;

    public static void ApplyHighContrast(Color[] sparkle, Color[] appTrack, Color[] appPressed)
    {
        var green = Color.FromArgb(0, 255, 0);
        var greenDeep = Color.FromArgb(0, 200, 0);
        var yellow = Color.FromArgb(255, 255, 0);
        var cyan = Color.FromArgb(0, 255, 255);
        var cyanDeep = Color.FromArgb(0, 200, 200);

        sparkle[Track1] = cyanDeep;
        sparkle[Track2] = cyan;
        sparkle[Press1] = Color.FromArgb(180, 180, 0);
        sparkle[Press2] = yellow;
        sparkle[Check1] = greenDeep;
        sparkle[Check2] = green;
        sparkle[CheckTrack1] = cyanDeep;
        sparkle[CheckTrack2] = cyan;
        sparkle[CheckPress1] = greenDeep;
        sparkle[Accent] = green;
        sparkle[DefaultBack] = green;
        sparkle[TabChecked] = green;

        FillApp(appTrack, cyan, cyanDeep, Color.White);
        FillApp(appPressed, yellow, Color.FromArgb(200, 200, 0), Color.Black);
    }

    public static void ApplyDeuteranopia(Color[] sparkle, Color[] appTrack, Color[] appPressed)
    {
        var blue = Color.FromArgb(0, 114, 178);
        var blueDeep = Color.FromArgb(0, 90, 140);
        var orange = Color.FromArgb(180, 110, 0);
        var orangeDeep = Color.FromArgb(150, 90, 0);
        var orangePale = Color.FromArgb(255, 220, 150);
        var orangeLight = Color.FromArgb(255, 200, 80);

        sparkle[Track1] = Color.FromArgb(200, 140, 40);
        sparkle[Track2] = orangeLight;
        sparkle[Press1] = orangeDeep;
        sparkle[Press2] = orange;
        sparkle[Check1] = orangeDeep;
        sparkle[Check2] = orange;
        sparkle[CheckTrack1] = orange;
        sparkle[CheckTrack2] = orangeLight;
        sparkle[CheckPress1] = orangeDeep;
        sparkle[Accent] = blue;
        sparkle[DefaultBack] = blue;
        sparkle[TabChecked] = blue;

        FillApp(appTrack, orangePale, orangeLight, orange);
        FillApp(appPressed, orange, orangeDeep, Color.FromArgb(120, 70, 0));
    }

    public static void ApplyProtanopia(Color[] sparkle, Color[] appTrack, Color[] appPressed)
    {
        var blue = Color.FromArgb(0, 90, 181);
        var blueDeep = Color.FromArgb(0, 70, 140);
        var brown = Color.FromArgb(153, 79, 0);
        var brownDeep = Color.FromArgb(120, 60, 0);
        var brownPale = Color.FromArgb(235, 205, 170);
        var brownLight = Color.FromArgb(200, 130, 50);

        sparkle[Track1] = Color.FromArgb(170, 100, 40);
        sparkle[Track2] = brownLight;
        sparkle[Press1] = brownDeep;
        sparkle[Press2] = brown;
        sparkle[Check1] = brownDeep;
        sparkle[Check2] = brown;
        sparkle[CheckTrack1] = brown;
        sparkle[CheckTrack2] = brownLight;
        sparkle[CheckPress1] = brownDeep;
        sparkle[Accent] = blue;
        sparkle[DefaultBack] = blue;
        sparkle[TabChecked] = blue;

        FillApp(appTrack, brownPale, brownLight, brown);
        FillApp(appPressed, brown, brownDeep, Color.FromArgb(100, 50, 0));
    }

    private static void FillApp(Color[] app, Color c1, Color c2, Color c3)
    {
        app[0] = c1;
        app[1] = c2;
        app[2] = c3;
        app[3] = c2;
        app[4] = c3;
    }
}