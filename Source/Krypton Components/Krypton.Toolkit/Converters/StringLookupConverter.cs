#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Helper base class used to convert from to/from a table of value,string pairs.
    /// </summary>
    public abstract class StringLookupConverter<TEnumType> : EnumConverter
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the StringLookupConverter class.
        /// </summary>
        public StringLookupConverter()
            : base(typeof(TEnumType))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected abstract IReadOnlyDictionary<TEnumType /*Enum*/, string /*Display*/> Pairs { get; }
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
            if (value is TEnumType val
                && destinationType == typeof(string)
                )
            {
                // Search for a matching value
                if ( Pairs.TryGetValue(val, out var display) )
                {
                        return display;
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
            // We are only interested in adding functionality for converting from strings
            if (value is string val)
            {
                // Search for a matching string
                var key = Pairs.FirstOrDefault(x => x.Value == val).Key;
                if ( key != null)
                { 
                    return key; 
                }
            }

            // Let base class perform default conversion
            return base.ConvertFrom(context, culture, value);
        }
        #endregion
    }
}
