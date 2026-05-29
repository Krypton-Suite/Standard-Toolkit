#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Custom type converter so that PaletteDrawBorders values appear as neat text at design time.
/// </summary>
internal class PaletteDrawBordersConverter : EnumConverter
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteDrawBordersConverter class.
    /// </summary>
    public PaletteDrawBordersConverter()
        : base(typeof(PaletteDrawBorders))
    {
    }
    #endregion

    #region Public
    /// <summary>
    /// Converts the given value object to the specified type, using the specified context and culture information.
    /// </summary>
    /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
    /// <param name="culture">A CultureInfo object. If a null reference the current culture is assumed.</param>
    /// <param name="value">The Object to convert.</param>
    /// <param name="destinationType">The Type to convert the value parameter to.</param>
    /// <returns>An Object that represents the converted value.</returns>
    public override object? ConvertTo(ITypeDescriptorContext? context,
        CultureInfo? culture,
        object? value,
        Type destinationType)
    {
        // We are only interested in adding functionality for converting to strings
        if (destinationType == typeof(string))
        {
            // Convert object to expected style
            var borders = (PaletteDrawBorders)(value ?? PaletteDrawBorders.Inherit);

            // If the inherit flag is set that that is the only flag of interest
            if (borders.HasFlag(PaletteDrawBorders.Inherit))
            {
                return @"Inherit";
            }
            else
            {
                // Append the names of each border we want
                var sb = new StringBuilder();

                if (borders.HasFlag(PaletteDrawBorders.Top))
                {
                    sb.Append(@"Top");
                }

                if (borders.HasFlag(PaletteDrawBorders.Bottom))
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(',');
                    }

                    sb.Append(@"Bottom");
                }

                if (borders.HasFlag(PaletteDrawBorders.Left))
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(',');
                    }

                    sb.Append(@"Left");
                }

                if (borders.HasFlag(PaletteDrawBorders.Right))
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(',');
                    }

                    sb.Append(@"Right");
                }

                // If no border is wanted then return a fixed string
                if (sb.Length == 0)
                {
                    sb.Append(@"None");
                }

                return sb.ToString();
            }
        }

        // Let base class perform default conversion
        return base.ConvertTo(context, culture, value, destinationType);
    }

    /// <summary>
    /// Converts the given object to the type of this converter, using the specified context and culture information.
    /// </summary>
    /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
    /// <param name="culture">The CultureInfo to use as the current culture.</param>
    /// <param name="value">The Object to convert.</param>
    /// <returns>An Object that represents the converted value.</returns>
    public override object? ConvertFrom(ITypeDescriptorContext? context,
        CultureInfo? culture,
        object? value)
    {
        // Convert incoming value to a string
        // We are only interested in adding functionality for converting from strings
        if (value is string conv)
        {

            // Default to returning an empty value
            var ret = PaletteDrawBorders.None;

            // If inherit is in the string, we use only that value
            if (conv.Contains(@"Inherit"))
            {
                ret = PaletteDrawBorders.Inherit;
            }
            else
            {
                // If the word 'none' is found then no value is needed
                if (!conv.Contains(@"None"))
                {
                    // Get the borders actually specified
                    if (conv.Contains(@"Top"))
                    {
                        ret |= PaletteDrawBorders.Top;
                    }

                    if (conv.Contains(@"Bottom"))
                    {
                        ret |= PaletteDrawBorders.Bottom;
                    }

                    if (conv.Contains(@"Left"))
                    {
                        ret |= PaletteDrawBorders.Left;
                    }

                    if (conv.Contains(@"Right"))
                    {
                        ret |= PaletteDrawBorders.Right;
                    }
                }
            }

            return ret;
        }

        // Let base class perform default conversion
        return base.ConvertFrom(context!, culture!, value!);
    }
    #endregion
}