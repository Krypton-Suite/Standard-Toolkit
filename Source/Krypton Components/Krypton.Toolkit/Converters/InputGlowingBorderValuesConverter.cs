#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides a type converter for <see cref="InputGlowingBorderValues"/>.
/// </summary>
public class InputGlowingBorderValuesConverter : ExpandableObjectConverter
{
    /// <inheritdoc />
    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType) =>
        destinationType == typeof(string) || base.CanConvertTo(context, destinationType);

    /// <inheritdoc />
    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is InputGlowingBorderValues glowingBorderValues)
        {
            if (!glowingBorderValues.Enable)
            {
                return @"Disabled";
            }

            string animation = glowingBorderValues.Animate ? @"Animated" : @"Static";
            return $@"Enabled ({animation}, {glowingBorderValues.ShowWhen}, {glowingBorderValues.Style})";
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }
}
