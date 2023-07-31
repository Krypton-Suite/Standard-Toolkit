#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Helper base class used to convert from to/from a table of value, image pairs.
    /// </summary>
    public abstract class ImageLookupConverter : EnumConverter
    {
        #region Type Definitions

        /// <summary>
        /// Represents a name/value pair association.
        /// </summary>
        protected struct Pair
        {
            /// <summary>
            /// Enumeration value.
            /// </summary>
            public object EnumKey;

            /// <summary>
            /// Enumeration value display string.
            /// </summary>
            public Image Value;

            /// <summary>
            /// Initialize a new instance of the Pair structure.
            /// </summary>
            /// <param name="enumKey">Object instance.</param>
            /// <param name="value">String instance.</param>
            public Pair(object enumKey, Image value)
            {
                EnumKey = enumKey;

                Value = value;
            }
        }

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="ImageLookupConverter" /> class.</summary>
        /// <param name="enumType">Type of the enum.</param>
        protected ImageLookupConverter(Type enumType) : base(enumType)
        {
        }

        #endregion

        #region Protected

        /// <summary>Gets the array of lookup pairs.</summary>
        protected abstract Pair[] Pairs { get; }

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
                // Search for a matching value
                foreach (Pair p in Pairs)
                {
                    if (p.EnumKey.Equals(value))
                    {
                        return p.Value;
                    }
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
                                           object value)
        {
            // We are only interested in adding functionality for converting from strings
            if (value is string)
            {
                // Search for a matching string
                foreach (Pair p in Pairs)
                {
                    if (p.Value.Equals(value))
                    {
                        return p.EnumKey;
                    }
                }
            }

            // Let base class perform default conversion
            return base.ConvertFrom(context, culture, value);
        }

        #endregion
    }
}
