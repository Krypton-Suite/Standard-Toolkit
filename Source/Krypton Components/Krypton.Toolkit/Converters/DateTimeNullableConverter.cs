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
/// Add DBNull/Null conversion for date time types.
/// </summary>
public class DateTimeNullableConverter : DateTimeConverter
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the DateTimeNullableConverter class.
    /// </summary>
    public DateTimeNullableConverter()
    {
    }
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Converts the given object to the type of this converter, using the specified context and culture information.
    /// </summary>
    /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
    /// <param name="culture">The CultureInfo to use as the current culture.</param>
    /// <param name="value">The Object to convert.</param>
    /// <returns>An Object that represents the converted value.</returns>
    public override object? ConvertFrom(ITypeDescriptorContext? context, 
        CultureInfo? culture, 
        object value)
    {
        // We allow an empty string or a string with DBNull/null/Nothing to be converted to a DBNull value.
        if (value is string)
        {
            var stringValue = value.ToString()!.ToLower();
            if (stringValue is "dbnull" or "null" or "nothing")
            {
                return DBNull.Value;
            }
        }

        return base.ConvertFrom(context!, culture!, value);
    }
    #endregion
}