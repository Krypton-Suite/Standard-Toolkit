#region BSD License
/*
 *
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides a type converter to convert KryptonSystemMenuConverter objects to and from various other representations.
/// </summary>
public class KryptonSystemMenuConverter : ExpandableObjectConverter
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonThemedSystemMenuConverter class.
    /// </summary>
    public KryptonSystemMenuConverter()
        : base()
    {
    }
    #endregion

    #region Public Overrides
    /// <summary>
    /// Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.
    /// </summary>
    /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
    /// <param name="sourceType">A Type that represents the type you want to convert from.</param>
    /// <returns>True if this converter can perform the conversion; otherwise, false.</returns>
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        // Can only convert from a string
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    /// <summary>
    /// Returns whether this converter can convert the object to the specified type, using the specified context.
    /// </summary>
    /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
    /// <param name="destinationType">A Type that represents the type you want to convert to.</param>
    /// <returns>True if this converter can perform the conversion; otherwise, false.</returns>
    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        // Can only convert to a string
        return destinationType == typeof(string) || base.CanConvertTo(context, destinationType!);
    }

    /// <summary>
    /// Converts the given object to the type of this converter, using the specified context and culture information.
    /// </summary>
    /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
    /// <param name="culture">The CultureInfo to use as the current culture.</param>
    /// <param name="value">The Object to convert.</param>
    /// <returns>An Object that represents the converted value.</returns>
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object? value)
    {
        // Can only convert from a string
        if (value is string stringValue)
        {
            // Parse the string representation
            if (stringValue == "Enabled")
            {
                return true;
            }

            if (stringValue == "Disabled")
            {
                return false;
            }
        }

        // Only call base if value is not null
        if (value != null)
        {
            return base.ConvertFrom(context, culture, value);
        }
        else
        {
            // Handle null value appropriately, e.g. return null or throw
            return null;
        }
    }

    /// <summary>
    /// Converts the given value object to the specified type, using the specified context and culture information.
    /// </summary>
    /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
    /// <param name="culture">A CultureInfo. If null is passed, the current culture is assumed.</param>
    /// <param name="value">The Object to convert.</param>
    /// <param name="destinationType">The Type to convert the value parameter to.</param>
    /// <returns>An Object that represents the converted value.</returns>
    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        // Can only convert to a string
        if (destinationType == typeof(string) && value != null)
        {
            // Return a descriptive string
            if (value is KryptonSystemMenu systemMenu)
            {
                if (systemMenu.Enabled)
                {
                    return $"Enabled ({systemMenu.MenuItemCount} items)";
                }
                else
                {
                    return "Disabled";
                }
            }
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }

    /// <summary>
    /// Returns a collection of properties for the type of array specified by the value parameter, using the specified context and attributes.
    /// </summary>
    /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
    /// <param name="value">An Object that specifies the type of array for which to get properties.</param>
    /// <param name="attributes">An array of type Attribute that is used as a filter.</param>
    /// <returns>A PropertyDescriptorCollection with the properties that are exposed for this data type.</returns>
    public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext? context, object? value, Attribute[]? attributes)
    {
        // Ensure 'value' is not null before passing to base
        if (value == null)
        {
            return new PropertyDescriptorCollection([]);
        }

        var properties = base.GetProperties(context, value, attributes!);

        // Filter and reorder the properties to show the most important ones first
        var filteredProperties = new List<PropertyDescriptor>();

        // Add the main properties in logical order
        var propertyNames = new[]
        {
            "Enabled",
            "ShowOnLeftClick",
            "ShowOnRightClick",
            "ShowOnAltSpace",
            "MenuItemCount",
            "HasMenuItems"
        };

        foreach (var propertyName in propertyNames)
        {
            var property = properties[propertyName];
            if (property != null)
            {
                filteredProperties.Add(property);
            }
        }

        // Add any remaining properties
        foreach (PropertyDescriptor property in properties)
        {
            if (!filteredProperties.Contains(property))
            {
                filteredProperties.Add(property);
            }
        }

        return new PropertyDescriptorCollection(filteredProperties.ToArray());
    }

    #endregion
}