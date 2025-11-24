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
/// Add conversion to a string for display in properties window at design time.
/// </summary>
public class ButtonSpecFormFixedConverter : ExpandableObjectConverter
{
    /// <summary>
    /// Returns whether this converter can convert the object to the specified type.
    /// </summary>
    /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
    /// <param name="destinationType">A Type that represents the type you want to convert to.</param>
    /// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType) =>
        // Can always convert to a string representation
        destinationType == typeof(string) || base.CanConvertTo(context, destinationType!);

    // Let base class do standard processing
    /// <summary>
    /// Converts the given value object to the specified type, using the specified context and culture information.
    /// </summary>
    /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
    /// <param name="culture">A CultureInfo. If a null reference (Nothing in Visual Basic) is passed, the current culture is assumed.</param>
    /// <param name="value">The Object to convert.</param>
    /// <param name="destinationType">The Type to convert the value parameter to.</param>
    /// <returns>An Object that represents the converted value.</returns>
    public override object? ConvertTo(ITypeDescriptorContext? context,
        CultureInfo? culture, 
        object? value, 
        Type destinationType)
    {
        // Can always convert to a string representation
        if (destinationType == typeof(string))
        {
            // Cast to correct type
            var buttonSpec = value as ButtonSpecFormFixed;

            // Ask the button spec for the correct string
            return buttonSpec?.ToString() ?? string.Empty;
        }
            
        // Let base class attempt other conversions
        return base.ConvertTo(context!, culture!, value!, destinationType);
    }
}