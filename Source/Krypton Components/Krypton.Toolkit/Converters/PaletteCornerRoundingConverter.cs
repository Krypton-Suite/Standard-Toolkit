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
            string[] parts = text.Split([',', ';'], StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1
                && float.TryParse(parts[0].Trim(), NumberStyles.Float, culture ?? CultureInfo.CurrentCulture, out float uniform))
            {
                return PaletteCornerRounding.Uniform(uniform);
            }

            if (parts.Length == 4
                && float.TryParse(parts[0].Trim(), NumberStyles.Float, culture ?? CultureInfo.CurrentCulture, out float topLeft)
                && float.TryParse(parts[1].Trim(), NumberStyles.Float, culture ?? CultureInfo.CurrentCulture, out float topRight)
                && float.TryParse(parts[2].Trim(), NumberStyles.Float, culture ?? CultureInfo.CurrentCulture, out float bottomRight)
                && float.TryParse(parts[3].Trim(), NumberStyles.Float, culture ?? CultureInfo.CurrentCulture, out float bottomLeft))
            {
                return new PaletteCornerRounding(topLeft, topRight, bottomRight, bottomLeft);
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
}
