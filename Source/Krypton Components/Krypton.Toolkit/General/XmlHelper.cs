#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Explicit helpers for XML Data export and import
/// </summary>
public static class XmlHelper
{
    /// <summary>
    /// Only persist the provided name/value pair as an Xml attribute if the value is not null/empty and not the default.
    /// </summary>
    /// <param name="xmlWriter">Xml writer to save information into.</param>
    /// <param name="name">Attribute name.</param>
    /// <param name="value">Attribute value.</param>
    /// <param name="defaultValue">Default value.</param>
    public static void TextToXmlAttribute(XmlWriter xmlWriter, string name, string? value, string defaultValue = @"")
    {
        if (!string.IsNullOrEmpty(value) && (value != defaultValue))
        {
            xmlWriter.WriteAttributeString(name, value);
        }
    }

    /// <summary>
    /// Read the named attribute value but if no attribute is found then return the provided default.
    /// </summary>
    /// <param name="xmlReader">Xml reader to load information from.</param>
    /// <param name="name">Attribute name.</param>
    /// <param name="defaultValue">Default value.</param>
    /// <returns></returns>
    public static string XmlAttributeToText(XmlReader xmlReader, string name, string defaultValue = @"")
    {
        try
        {
            var ret = xmlReader.GetAttribute(name) ?? defaultValue;

            return ret;
        }
        catch
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// Persist an object as XML attributes when it is a string or has a TypeConverter that round-trips to/from string.
    /// Strings write only <paramref name="valueName"/>. Convertible non-strings also write <paramref name="typeName"/>
    /// as <c>FullName, AssemblyName</c>. Non-convertible objects are skipped (use custom save events instead).
    /// </summary>
    /// <param name="xmlWriter">Xml writer to save information into.</param>
    /// <param name="valueName">Attribute name for the invariant string value.</param>
    /// <param name="typeName">Attribute name for the type identity.</param>
    /// <param name="value">Object to persist.</param>
    public static void ObjectToXmlAttributes(XmlWriter xmlWriter, string valueName, string typeName, object? value)
    {
        if (value == null)
        {
            return;
        }

        if (value is string stringValue)
        {
            TextToXmlAttribute(xmlWriter, valueName, stringValue);
            return;
        }

        Type type = value.GetType();
        TypeConverter converter = TypeDescriptor.GetConverter(type);
        if (!converter.CanConvertTo(typeof(string)) || !converter.CanConvertFrom(typeof(string)))
        {
            return;
        }

        string? persisted;
        try
        {
            persisted = converter.ConvertToInvariantString(value);
        }
        catch (Exception e)
        {
            KryptonExceptionHandler.CaptureException(e);
            return;
        }

        if (string.IsNullOrEmpty(persisted))
        {
            return;
        }

        TextToXmlAttribute(xmlWriter, valueName, persisted);
        TextToXmlAttribute(xmlWriter, typeName, GetPersistableTypeName(type));
    }

    /// <summary>
    /// Reconstruct an object previously written by <see cref="ObjectToXmlAttributes"/>.
    /// When <paramref name="typeName"/> is absent, the value is returned as a string (legacy layout files).
    /// </summary>
    /// <param name="xmlReader">Xml reader to load information from.</param>
    /// <param name="valueName">Attribute name for the invariant string value.</param>
    /// <param name="typeName">Attribute name for the type identity.</param>
    /// <param name="present"><c>true</c> when the value attribute was present on the element.</param>
    /// <returns>Reconstructed object, string fallback, or <c>null</c> when not present.</returns>
    public static object? XmlAttributesToObject(XmlReader xmlReader, string valueName, string typeName, out bool present)
    {
        present = false;

        string? rawValue;
        try
        {
            rawValue = xmlReader.GetAttribute(valueName);
        }
        catch
        {
            return null;
        }

        if (rawValue == null)
        {
            return null;
        }

        present = true;

        string? rawType;
        try
        {
            rawType = xmlReader.GetAttribute(typeName);
        }
        catch
        {
            rawType = null;
        }

        if (string.IsNullOrEmpty(rawType))
        {
            return rawValue;
        }

        Type? type = ResolvePersistableType(rawType!);
        if (type == null)
        {
            return rawValue;
        }

        TypeConverter converter = TypeDescriptor.GetConverter(type);
        if (!converter.CanConvertFrom(typeof(string)))
        {
            return rawValue;
        }

        try
        {
            return converter.ConvertFromInvariantString(rawValue) ?? rawValue;
        }
        catch (Exception e)
        {
            KryptonExceptionHandler.CaptureException(e);
            return rawValue;
        }
    }

    private static string GetPersistableTypeName(Type type) =>
        type.FullName + @", " + type.Assembly.GetName().Name;

    private static Type? ResolvePersistableType(string typeName)
    {
        Type? type = Type.GetType(typeName, throwOnError: false);
        if (type != null)
        {
            return type;
        }

        var comma = typeName.IndexOf(',');
        var fullName = comma > 0 ? typeName.Substring(0, comma).Trim() : typeName.Trim();
        if (string.IsNullOrEmpty(fullName))
        {
            return null;
        }

        type = Type.GetType(fullName, throwOnError: false);
        if (type != null)
        {
            return type;
        }

        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            type = assembly.GetType(fullName, throwOnError: false, ignoreCase: false);
            if (type != null)
            {
                return type;
            }
        }

        return null;
    }

    /// <summary>
    /// Convert a Image to a culture invariant string value.
    /// </summary>
    /// <param name="xmlWriter">Xml writer to save information into.</param>
    /// <param name="name">Name of image to save.</param>
    /// <param name="image">Image to persist.</param>
    public static void ImageToXmlCData(XmlWriter xmlWriter, string name, Bitmap? image)
    {
        // Only store if we have an actual image to persist
        if (image != null)
        {
            // Convert the Image into base64 so it can be used in xml
            using var memory = new MemoryStream();
            image.Save(memory, ImageFormat.Png);
            memory.Position = 0;
            var base64 = Convert.ToBase64String(memory.ToArray());

            // Store the base64 Hex as a CDATA inside the element
            xmlWriter.WriteStartElement(name);
            xmlWriter.WriteCData(base64);
            xmlWriter.WriteEndElement();
        }
    }

    /// <summary>
    /// Convert a culture invariant string value into an Image.
    /// </summary>
    /// <param name="xmlReader">Xml reader to load information from.</param>
    /// <returns>Image that was recreated.</returns>
    public static Bitmap? XmlCDataToImage(XmlReader xmlReader)
    {
        try
        {
            // Convert the content of the element into base64
            var bytes = Convert.FromBase64String(xmlReader.ReadContentAsString());

            // Convert the bytes back into an Image
            using var memory = new MemoryStream(bytes);

            return new Bitmap(memory);
        }
        catch (Exception e)
        {
            KryptonExceptionHandler.CaptureException(e);

            return null;
        }
    }
}