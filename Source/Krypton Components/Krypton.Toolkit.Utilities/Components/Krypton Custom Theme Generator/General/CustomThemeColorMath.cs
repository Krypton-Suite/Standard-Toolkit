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
/// Colour parsing and HSL helpers used by the custom theme generator.
/// </summary>
internal static class CustomThemeColorMath
{
    private const float NeutralSaturation = 0.18f;
    private const int GreyDeltaThreshold = 28;

    /// <summary>
    /// Parses a colour from hexadecimal (<c>#RGB</c>, <c>#RRGGBB</c>, <c>#AARRGGBB</c>),
    /// comma-separated RGB, <c>rgb(r,g,b)</c>, or a named HTML colour.
    /// </summary>
    internal static bool TryParseColor(string? text, out Color color)
    {
        color = Color.Empty;
        if (text is null)
        {
            return false;
        }

        string trimmed = text.Trim();
        if (trimmed.Length == 0)
        {
            return false;
        }

        if (TryParseHex(trimmed, out color))
        {
            return true;
        }

        if (TryParseRgbFunction(trimmed, out color))
        {
            return true;
        }

        if (TryParseRgbCsv(trimmed, out color))
        {
            return true;
        }

        try
        {
            Color parsed = ColorTranslator.FromHtml(trimmed);
            if (!parsed.IsEmpty && parsed.A > 0)
            {
                color = parsed;
                return true;
            }
        }
        catch (Exception)
        {
            // Named / HTML colours that ColorTranslator cannot parse fall through as false.
        }

        return false;
    }

    /// <summary>
    /// Formats <paramref name="color"/> as <c>#RRGGBB</c> (or <c>#AARRGGBB</c> when alpha is not opaque).
    /// </summary>
    internal static string ToHex(Color color)
    {
        if (color.A != 255)
        {
            return string.Format(CultureInfo.InvariantCulture, @"#{0:X2}{1:X2}{2:X2}{3:X2}", color.A, color.R, color.G, color.B);
        }

        return string.Format(CultureInfo.InvariantCulture, @"#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
    }

    internal static bool IsEmptyOrTransparent(Color color) =>
        color.IsEmpty || color.A == 0;

    internal static bool IsNeutral(Color color)
    {
        if (IsEmptyOrTransparent(color))
        {
            return true;
        }

        int max = Math.Max(color.R, Math.Max(color.G, color.B));
        int min = Math.Min(color.R, Math.Min(color.G, color.B));
        bool spreadIsGrey = (max - min) <= GreyDeltaThreshold;
        float sat = color.GetSaturation();
        float bri = color.GetBrightness();
        bool isNearWhiteTint = (bri >= 0.92f) && (sat <= 0.35f);
        return sat <= NeutralSaturation || spreadIsGrey || isNearWhiteTint;
    }

    internal static Color Lighten(Color color, float amount) =>
        CommonHelper.MergeColors(color, 1f - amount, Color.White, amount);

    internal static Color Darken(Color color, float amount) =>
        CommonHelper.MergeColors(color, 1f - amount, Color.Black, amount);

    internal static Color Analogous(Color primary, float hueOffsetDegrees)
    {
        ToHsl(primary, out float h, out float s, out float l);
        return FromHsl(WrapHue(h + hueOffsetDegrees), s, l, primary.A);
    }

    internal static Color ContrastText(Color background) =>
        RelativeLuminance(background) > 0.179 ? Color.Black : Color.White;

    internal static Color MutedText(Color onSurface) =>
        CommonHelper.MergeColors(onSurface, 0.55f, IsDark(onSurface) ? Color.White : Color.Black, 0.45f);

    internal static bool IsDark(Color color) => RelativeLuminance(color) < 0.45;

    internal static double RelativeLuminance(Color color)
    {
        double r = Linearize(color.R / 255.0);
        double g = Linearize(color.G / 255.0);
        double b = Linearize(color.B / 255.0);
        return (0.2126 * r) + (0.7152 * g) + (0.0722 * b);
    }

    internal static Color ShiftHue(Color source, float hueDelta, float seedSaturation)
    {
        if (IsEmptyOrTransparent(source) || IsNeutral(source))
        {
            return source;
        }

        ToHsl(source, out float h, out float s, out float l);
        float newSaturation = s + ((seedSaturation - s) * 0.35f);
        newSaturation = Clamp01(newSaturation);
        return FromHsl(WrapHue(h + hueDelta), newSaturation, l, source.A);
    }

    internal static void ToHsl(Color color, out float h, out float s, out float l)
    {
        h = color.GetHue();
        s = color.GetSaturation();
        l = color.GetBrightness();
    }

    internal static Color FromHsl(float h, float s, float l, int alpha)
    {
        h = WrapHue(h);
        s = Clamp01(s);
        l = Clamp01(l);

        float c = (1f - Math.Abs((2f * l) - 1f)) * s;
        float hp = h / 60f;
        float x = c * (1f - Math.Abs((hp % 2f) - 1f));
        float m = l - (c / 2f);
        float r1;
        float g1;
        float b1;

        if (hp < 1f)
        {
            r1 = c;
            g1 = x;
            b1 = 0f;
        }
        else if (hp < 2f)
        {
            r1 = x;
            g1 = c;
            b1 = 0f;
        }
        else if (hp < 3f)
        {
            r1 = 0f;
            g1 = c;
            b1 = x;
        }
        else if (hp < 4f)
        {
            r1 = 0f;
            g1 = x;
            b1 = c;
        }
        else if (hp < 5f)
        {
            r1 = x;
            g1 = 0f;
            b1 = c;
        }
        else
        {
            r1 = c;
            g1 = 0f;
            b1 = x;
        }

        int r = ToByte(r1 + m);
        int g = ToByte(g1 + m);
        int b = ToByte(b1 + m);
        return Color.FromArgb(alpha, r, g, b);
    }

    private static bool TryParseHex(string text, out Color color)
    {
        color = Color.Empty;
        string hex = text.StartsWith(@"#", StringComparison.Ordinal) ? text.Substring(1) : text;
        if (hex.Length != 3 && hex.Length != 6 && hex.Length != 8)
        {
            return false;
        }

        if (hex.Length == 3)
        {
            hex = string.Concat(hex[0], hex[0], hex[1], hex[1], hex[2], hex[2]);
        }

        if (!uint.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out uint value))
        {
            return false;
        }

        if (hex.Length == 8)
        {
            color = Color.FromArgb((int)((value >> 24) & 0xFF), (int)((value >> 16) & 0xFF), (int)((value >> 8) & 0xFF), (int)(value & 0xFF));
        }
        else
        {
            color = Color.FromArgb(255, (int)((value >> 16) & 0xFF), (int)((value >> 8) & 0xFF), (int)(value & 0xFF));
        }

        return true;
    }

    private static bool TryParseRgbFunction(string text, out Color color)
    {
        color = Color.Empty;
        if (!text.StartsWith(@"rgb", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        int open = text.IndexOf('(');
        int close = text.LastIndexOf(')');
        if (open < 0 || close <= open)
        {
            return false;
        }

        return TryParseRgbCsv(text.Substring(open + 1, close - open - 1), out color);
    }

    private static bool TryParseRgbCsv(string text, out Color color)
    {
        color = Color.Empty;
        string[] parts = text.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 3 && parts.Length != 4)
        {
            return false;
        }

        if (!TryParseByte(parts[0], out int r)
            || !TryParseByte(parts[1], out int g)
            || !TryParseByte(parts[2], out int b))
        {
            return false;
        }

        int a = 255;
        if (parts.Length == 4 && !TryParseByte(parts[3], out a))
        {
            return false;
        }

        color = Color.FromArgb(a, r, g, b);
        return true;
    }

    private static bool TryParseByte(string text, out int value)
    {
        value = 0;
        if (!int.TryParse(text.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
        {
            return false;
        }

        return value >= 0 && value <= 255;
    }

    private static double Linearize(double channel) =>
        channel <= 0.04045 ? channel / 12.92 : Math.Pow((channel + 0.055) / 1.055, 2.4);

    private static float WrapHue(float hue)
    {
        hue %= 360f;
        if (hue < 0f)
        {
            hue += 360f;
        }

        return hue;
    }

    private static float Clamp01(float value)
    {
        if (value < 0f)
        {
            return 0f;
        }

        if (value > 1f)
        {
            return 1f;
        }

        return value;
    }

    private static int ToByte(float value)
    {
        int rounded = (int)Math.Round(value * 255f, MidpointRounding.AwayFromZero);
        if (rounded < 0)
        {
            return 0;
        }

        if (rounded > 255)
        {
            return 255;
        }

        return rounded;
    }
}
