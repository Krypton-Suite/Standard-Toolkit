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
/// Formats a sampled colour into the representations listed by
/// <see cref="KryptonScreenColorPickerColorFormat"/>.
/// </summary>
internal static class ScreenColorPickerColorFormatter
{
    internal static readonly KryptonScreenColorPickerColorFormat[] DefinedFormats =
    {
        KryptonScreenColorPickerColorFormat.KnownName,
        KryptonScreenColorPickerColorFormat.Hex,
        KryptonScreenColorPickerColorFormat.HexAlpha,
        KryptonScreenColorPickerColorFormat.HexInteger,
        KryptonScreenColorPickerColorFormat.Rgb,
        KryptonScreenColorPickerColorFormat.Rgba,
        KryptonScreenColorPickerColorFormat.Hsl,
        KryptonScreenColorPickerColorFormat.Hsv,
        KryptonScreenColorPickerColorFormat.Cmyk,
        KryptonScreenColorPickerColorFormat.Decimal,
        KryptonScreenColorPickerColorFormat.Vector
    };

    internal const KryptonScreenColorPickerColorFormat AllFormats =
        KryptonScreenColorPickerColorFormat.KnownName
        | KryptonScreenColorPickerColorFormat.Hex
        | KryptonScreenColorPickerColorFormat.HexAlpha
        | KryptonScreenColorPickerColorFormat.HexInteger
        | KryptonScreenColorPickerColorFormat.Rgb
        | KryptonScreenColorPickerColorFormat.Rgba
        | KryptonScreenColorPickerColorFormat.Hsl
        | KryptonScreenColorPickerColorFormat.Hsv
        | KryptonScreenColorPickerColorFormat.Cmyk
        | KryptonScreenColorPickerColorFormat.Decimal
        | KryptonScreenColorPickerColorFormat.Vector;

    internal const KryptonScreenColorPickerColorFormat DefaultFormats =
        KryptonScreenColorPickerColorFormat.KnownName
        | KryptonScreenColorPickerColorFormat.Hex
        | KryptonScreenColorPickerColorFormat.Rgb
        | KryptonScreenColorPickerColorFormat.Hsl;

    internal static KryptonScreenColorPickerColorFormat Normalize(KryptonScreenColorPickerColorFormat formats)
    {
        KryptonScreenColorPickerColorFormat masked = formats & AllFormats;
        return masked == KryptonScreenColorPickerColorFormat.None ? DefaultFormats : masked;
    }

    internal static string GetDisplayName(KryptonScreenColorPickerColorFormat format)
    {
        switch (format)
        {
            case KryptonScreenColorPickerColorFormat.Hex:
                return @"Hex";
            case KryptonScreenColorPickerColorFormat.HexAlpha:
                return @"Hex (alpha)";
            case KryptonScreenColorPickerColorFormat.HexInteger:
                return @"Hex integer";
            case KryptonScreenColorPickerColorFormat.Rgb:
                return @"RGB";
            case KryptonScreenColorPickerColorFormat.Rgba:
                return @"RGBA";
            case KryptonScreenColorPickerColorFormat.Hsl:
                return @"HSL";
            case KryptonScreenColorPickerColorFormat.Hsv:
                return @"HSV";
            case KryptonScreenColorPickerColorFormat.Cmyk:
                return @"CMYK";
            case KryptonScreenColorPickerColorFormat.Decimal:
                return @"Decimal";
            case KryptonScreenColorPickerColorFormat.Vector:
                return @"Vector";
            case KryptonScreenColorPickerColorFormat.KnownName:
                return @"Known name";
            default:
                return format.ToString();
        }
    }

    internal static string Format(Color color, KryptonScreenColorPickerColorFormat format)
    {
        switch (format)
        {
            case KryptonScreenColorPickerColorFormat.Hex:
                return FormatHex(color);
            case KryptonScreenColorPickerColorFormat.HexAlpha:
                return FormatHexAlpha(color);
            case KryptonScreenColorPickerColorFormat.HexInteger:
                return FormatHexInteger(color);
            case KryptonScreenColorPickerColorFormat.Rgb:
                return FormatRgb(color);
            case KryptonScreenColorPickerColorFormat.Rgba:
                return FormatRgba(color);
            case KryptonScreenColorPickerColorFormat.Hsl:
                return FormatHsl(color);
            case KryptonScreenColorPickerColorFormat.Hsv:
                return FormatHsv(color);
            case KryptonScreenColorPickerColorFormat.Cmyk:
                return FormatCmyk(color);
            case KryptonScreenColorPickerColorFormat.Decimal:
                return FormatDecimal(color);
            case KryptonScreenColorPickerColorFormat.Vector:
                return FormatVector(color);
            case KryptonScreenColorPickerColorFormat.KnownName:
                return FormatKnownName(color);
            default:
                return string.Empty;
        }
    }

    internal static string[] BuildReadoutLines(Color color, KryptonScreenColorPickerColorFormat formats, bool includeKnownName)
    {
        formats = Normalize(formats);
        var lines = new List<string>(DefinedFormats.Length);
        for (int i = 0; i < DefinedFormats.Length; i++)
        {
            KryptonScreenColorPickerColorFormat flag = DefinedFormats[i];
            if (flag == KryptonScreenColorPickerColorFormat.KnownName && !includeKnownName)
            {
                continue;
            }

            if ((formats & flag) == flag)
            {
                lines.Add(Format(color, flag));
            }
        }

        return lines.ToArray();
    }

    internal static int CountPanelLines(KryptonScreenColorPickerColorFormat formats, bool includeKnownName)
    {
        formats = Normalize(formats);
        int count = 0;
        for (int i = 0; i < DefinedFormats.Length; i++)
        {
            KryptonScreenColorPickerColorFormat flag = DefinedFormats[i];
            if (flag == KryptonScreenColorPickerColorFormat.KnownName && !includeKnownName)
            {
                continue;
            }

            if ((formats & flag) == flag)
            {
                count++;
            }
        }

        return count;
    }

    internal static string FormatHex(Color color) =>
        string.Format(CultureInfo.InvariantCulture, "#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);

    internal static string FormatHexAlpha(Color color) =>
        string.Format(CultureInfo.InvariantCulture, "#{0:X2}{1:X2}{2:X2}{3:X2}", color.A, color.R, color.G, color.B);

    internal static string FormatHexInteger(Color color) =>
        string.Format(CultureInfo.InvariantCulture, "0x{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);

    internal static string FormatRgb(Color color) =>
        string.Format(CultureInfo.InvariantCulture, @"RGB({0}, {1}, {2})", color.R, color.G, color.B);

    internal static string FormatRgba(Color color) =>
        string.Format(CultureInfo.InvariantCulture, @"RGBA({0}, {1}, {2}, {3})", color.R, color.G, color.B, color.A);

    internal static string FormatHsl(Color color)
    {
        CustomThemeColorMath.ToHsl(color, out float hue, out float saturation, out float lightness);
        return string.Format(CultureInfo.InvariantCulture, @"HSL({0:0}, {1:0}%, {2:0}%)",
            hue, saturation * 100f, lightness * 100f);
    }

    internal static string FormatHsv(Color color)
    {
        float r = color.R / 255f;
        float g = color.G / 255f;
        float b = color.B / 255f;
        float max = Math.Max(r, Math.Max(g, b));
        float min = Math.Min(r, Math.Min(g, b));
        float delta = max - min;
        float saturation = max <= 0f ? 0f : delta / max;
        return string.Format(CultureInfo.InvariantCulture, @"HSV({0:0}, {1:0}%, {2:0}%)",
            color.GetHue(), saturation * 100f, max * 100f);
    }

    internal static string FormatCmyk(Color color)
    {
        float r = color.R / 255f;
        float g = color.G / 255f;
        float b = color.B / 255f;
        float k = 1f - Math.Max(r, Math.Max(g, b));
        float c;
        float m;
        float y;
        if (k >= 1f - 0.0001f)
        {
            c = 0f;
            m = 0f;
            y = 0f;
        }
        else
        {
            float ik = 1f - k;
            c = (1f - r - k) / ik;
            m = (1f - g - k) / ik;
            y = (1f - b - k) / ik;
        }

        return string.Format(CultureInfo.InvariantCulture, @"CMYK({0:0}%, {1:0}%, {2:0}%, {3:0}%)",
            c * 100f, m * 100f, y * 100f, k * 100f);
    }

    internal static string FormatDecimal(Color color) =>
        ColorTranslator.ToWin32(color).ToString(CultureInfo.InvariantCulture);

    internal static string FormatVector(Color color) =>
        string.Format(CultureInfo.InvariantCulture, @"{0:0.###}, {1:0.###}, {2:0.###}",
            color.R / 255f, color.G / 255f, color.B / 255f);

    internal static string FormatKnownName(Color color)
    {
        Color[] webColors = WebColors;
        int best = int.MaxValue;
        string name = @"Custom";
        for (int i = 0; i < webColors.Length; i++)
        {
            Color candidate = webColors[i];
            int dr = color.R - candidate.R;
            int dg = color.G - candidate.G;
            int db = color.B - candidate.B;
            int distance = (dr * dr) + (dg * dg) + (db * db);
            if (distance >= best)
            {
                continue;
            }

            best = distance;
            name = candidate.Name;
            if (distance == 0)
            {
                return name;
            }
        }

        return name;
    }

    private static readonly Color[] WebColors = CreateWebColors();

    private static Color[] CreateWebColors()
    {
        Array values = Enum.GetValues(typeof(KnownColor));
        var colors = new List<Color>(values.Length);
        foreach (KnownColor known in values)
        {
            Color candidate = Color.FromKnownColor(known);
            if (!candidate.IsSystemColor && candidate.A == 255)
            {
                colors.Add(candidate);
            }
        }

        return colors.ToArray();
    }
}
