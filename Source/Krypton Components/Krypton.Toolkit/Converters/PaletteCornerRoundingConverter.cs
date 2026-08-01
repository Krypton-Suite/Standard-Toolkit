#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Custom type converter so that PaletteCornerRounding values appear as neat text at design time.
/// </summary>
internal class PaletteCornerRoundingConverter : TypeConverter
{
    /// <inheritdoc />
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType) =>
        sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

    /// <inheritdoc />
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string text)
        {
            text = text.Trim();
            CultureInfo formatCulture = culture ?? CultureInfo.CurrentCulture;

            if (string.Equals(text, @"Inherit", StringComparison.OrdinalIgnoreCase))
            {
                return PaletteCornerRounding.Inherit;
            }

            if (text.IndexOf('=') >= 0)
            {
                float topLeft = PaletteCornerRounding.InheritValue;
                float topRight = PaletteCornerRounding.InheritValue;
                float bottomRight = PaletteCornerRounding.InheritValue;
                float bottomLeft = PaletteCornerRounding.InheritValue;

                string[] parts = text.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    string[] pair = part.Trim().Split(new[] { '=' }, 2, StringSplitOptions.None);
                    if (pair.Length != 2 || !TryParseCornerValue(pair[1].Trim(), formatCulture, out float cornerValue))
                    {
                        continue;
                    }

                    PaletteCornerRounding.ApplyCornerLabel(pair[0], cornerValue, ref topLeft, ref topRight, ref bottomRight, ref bottomLeft);
                }

                return new PaletteCornerRounding(topLeft, topRight, bottomRight, bottomLeft);
            }

            string[] values = text.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (values.Length == 1
                && float.TryParse(values[0].Trim(), NumberStyles.Float, formatCulture, out float uniform))
            {
                return PaletteCornerRounding.Uniform(uniform);
            }

            if (values.Length == 4
                && float.TryParse(values[0].Trim(), NumberStyles.Float, formatCulture, out float valueTopLeft)
                && float.TryParse(values[1].Trim(), NumberStyles.Float, formatCulture, out float valueTopRight)
                && float.TryParse(values[2].Trim(), NumberStyles.Float, formatCulture, out float valueBottomRight)
                && float.TryParse(values[3].Trim(), NumberStyles.Float, formatCulture, out float valueBottomLeft))
            {
                return new PaletteCornerRounding(valueTopLeft, valueTopRight, valueBottomRight, valueBottomLeft);
            }
        }

        return base.ConvertFrom(context, culture, value);
    }

    /// <inheritdoc />
    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is PaletteCornerRounding rounding)
        {
            return rounding.ToString();
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }

    private static bool TryParseCornerValue(string text, CultureInfo culture, out float value)
    {
        if (string.Equals(text, @"Inherit", StringComparison.OrdinalIgnoreCase))
        {
            value = PaletteCornerRounding.InheritValue;
            return true;
        }

        return float.TryParse(text, NumberStyles.Float, culture, out value);
    }
}
