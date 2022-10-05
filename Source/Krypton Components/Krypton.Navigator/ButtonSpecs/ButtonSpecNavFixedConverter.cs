﻿namespace Krypton.Navigator
{
    /// <summary>
    /// Add conversion to a string for display in properties window at design time.
    /// </summary>
    public class ButtonSpecNavFixedConverter : ExpandableObjectConverter
    {
        /// <summary>
        /// Returns whether this converter can convert the object to the specified type.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
        /// <param name="destinationType">A Type that represents the type you want to convert to.</param>
        /// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) =>
            // Can always convert to a string representation
            destinationType == typeof(string) || base.CanConvertTo(context, destinationType);

        // Let base class do standard processing
        /// <summary>
        /// Converts the given value object to the specified type, using the specified context and culture information.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
        /// <param name="culture">A CultureInfo. If a null reference (Nothing in Visual Basic) is passed, the current culture is assumed.</param>
        /// <param name="value">The Object to convert.</param>
        /// <param name="destinationType">The Type to convert the value parameter to.</param>
        /// <returns>An Object that represents the converted value.</returns>
        public override object ConvertTo(ITypeDescriptorContext context, 
                                         System.Globalization.CultureInfo culture, 
                                         object value, 
                                         Type destinationType)
        {
            // Can always convert to a string representation
            if (destinationType == typeof(string))
            {
                // Cast to correct type
                ButtonSpecNavFixed buttonSpec = (ButtonSpecNavFixed)value;

                // Ask the button spec for the correct string
                return buttonSpec.ToString();
            }
            
            // Let base class attempt other conversions
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
